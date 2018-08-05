using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System;
using LMS.Entities;
using Task = System.Threading.Tasks.Task;
using System.Threading.Tasks;
using LMS.Dto;
using LMS.Business.Services;
using LMS.Interfaces;

namespace LMS.Identity
{
   public class IdentityService : BaseService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager; // allows  to authenticate a user and install or delete his cookies
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ExamineeService _examineeService;
        private readonly IMapper _mapper;
        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IUnitOfWork unitOfWork, ExamineeService examineeService)
            :base(unitOfWork, mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _examineeService = examineeService;
        }

        public IEnumerable<IdentityRole> GetAllRoles()
        {
            return _roleManager.Roles;
        }

        public async Task<IEnumerable<UserSummary>> GetAllUsers()
        {
            var result = _userManager.Users.Select(u => new UserSummary { UserName = u.UserName, Id = u.Id, Name = u.Name }).ToArray();
            var roles = await Task.WhenAll(result.Select(u => GetUserRoles(u.Id)));
          
            for(int i =0;i < result.Length;i++)   
                result[i].Roles = string.Join(", ", roles[i]);
            
            return result;
        }

        public async Task Register(UserDTO model)
        {
            var user = _mapper.Map<UserDTO, User>(model);
            var examinee = _mapper.Map<ExamineeDTO, Examinee>(model.Examinee);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                examinee.UserId = (await _userManager.FindByNameAsync(user.UserName)).Id;
                unitOfWork.Examinee.Create(examinee);
                await _userManager.AddToRolesAsync(user, model.Roles); // add role
            }
            else
                throw new AggregateException(result.Errors.Select(error => new Exception(error.Description)));
        }

        public async Task LogIn(string userName, string password, bool rememberMe)
        {
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new Exception("The username or password provided is incorrect.");
            var currentUserRole = await _userManager.GetRolesAsync(user);
            if (currentUserRole.Contains(Roles.Admin) 
                || currentUserRole.Contains(Roles.Moderator) 
                || currentUserRole.Contains(Roles.Reviewer))
            {
                var result =
                    await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);
                if(!result.Succeeded)
                    throw new Exception("The username or password provided is incorrect.");
            }
            else
                throw new Exception("Your role does not allow you to enter.");
        }

        public async Task LogInClient(string userId)
        {
            User user = await _userManager.FindByNameAsync(userId);
            if (user == null)
                throw new Exception("The username provided is incorrect.");
            await _signInManager.SignInAsync(user,false,null);
        }

        public async Task<IEnumerable<User>> GetAllAsync(string roleName)
        {
            return await _userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task Logout()
        {
            // delete cookies
            await _signInManager.SignOutAsync();
        }

        public async Task DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if(RoleInitializer.adminUserName == user.UserName)
                    throw new Exception("Sorry, but you can`t delete admin.");

                unitOfWork.Examinee.Delete(
                    unitOfWork.Examinee.Find(c => c.UserId == id).Id);
                IdentityResult result = await _userManager.DeleteAsync(user);
                
            }
        }

        public async Task<UserDTO> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            var userDTO = _mapper.Map<User, UserDTO>(user);
            userDTO.Examinee = _mapper.Map<Examinee, ExamineeDTO>(unitOfWork.Examinee.Find(c => c.UserId == id));
            userDTO.Roles = await GetUserRoles(id);
            
            return userDTO;

        }

        public async Task<ICollection<string>> GetUserRoles(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            return await _userManager.GetRolesAsync(user);
        }

        public async Task UpdateAsync(UserDTO userNew)
        {
            User userOld = await _userManager.FindByIdAsync(userNew.Id);
            if (userOld == null)
                throw new ArgumentNullException(nameof(userOld));
            if (!userNew.Roles.Any())
                throw new ArgumentException("User should have at least one role");

            userNew.ConcurrencyStamp = userOld.ConcurrencyStamp;
            _mapper.Map(userNew, userOld);
            await _userManager.AddPasswordAsync(userOld, userNew.Password);
            await _userManager.UpdateAsync(userOld);
            
            userNew.Examinee.UserId = userOld.Id;
            unitOfWork.Examinee.Update(_mapper.Map<ExamineeDTO, Examinee>(userNew.Examinee));
            await unitOfWork.SaveAsync();

            var oldUserRoles  = await GetUserRoles(userOld.Id);
            await _userManager.RemoveFromRolesAsync(userOld, oldUserRoles);
            await _userManager.AddToRolesAsync(userOld, userNew.Roles);
           
        }

        public UserDTO GetDefaultRegisterModel()
        {
            return new UserDTO { Examinee = _examineeService.GetDefaultExaminee() };
        }
    }
}
