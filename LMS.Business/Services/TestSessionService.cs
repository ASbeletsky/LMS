using System;
using System.Linq;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class TestSessionService : BaseService
    {
        public TestSessionService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
        }

        public TestSessionDTO GetById(int id)
        {
            var template = unitOfWork.TestSessions.Get(id);
            if (template == null)
            {
                throw new EntityNotFoundException<TestSession>(id);
            }

            return mapper.Map<TestSession, TestSessionDTO>(template);
        }

        public Task DeleteByIdAsync(int id)
        {
            unitOfWork.TestSessions.Delete(id);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateAsync(TestSessionDTO testSessionDTO)
        {
            if (testSessionDTO == null)
            {
                throw new ArgumentNullException(nameof(testSessionDTO));
            }
            if (!testSessionDTO.TestIds.Any())
            {
                throw new ArgumentException("Test session should contains at least one test");
            }
            if (!testSessionDTO.MemberIds.Any())
            {
                throw new ArgumentException("Test session shoul contains at least one examenee");
            }

            var testSession = mapper.Map<TestSessionDTO, TestSession>(testSessionDTO);

            unitOfWork.TestSessions.Update(testSession);

            return unitOfWork.SaveAsync();
        }

        public Task CreateAsync(TestSessionDTO testSessionDTO)
        {
            if (testSessionDTO == null)
            {
                throw new ArgumentNullException(nameof(testSessionDTO));
            }
            if (!testSessionDTO.TestIds.Any())
            {
                throw new ArgumentException("Test session should contains at least one test");
            }
            if (!testSessionDTO.MemberIds.Any())
            {
                throw new ArgumentException("Test session shoul contains at least one examenee");
            }

            var testSession = mapper.Map<TestSessionDTO, TestSession>(testSessionDTO);
            foreach(var user in testSession.Members)
            {
                user.Code = GenerateCode();
            }
            unitOfWork.TestSessions.Create(testSession);

            return unitOfWork.SaveAsync();
        }

        public IEnumerable<TestSessionDTO> GetAll()
        {
            return mapper
                .Map<IEnumerable<TestSession>, IEnumerable<TestSessionDTO>>(
                    unitOfWork.TestSessions.GetAll());
        }
        public string GenerateCode()
        {
            double previous=0;
            double generator = Math.Pow(13, 11);
            string possibleChars = "ACEFHJKMNPRTUVWXY123456789";
            double modulus = Math.Pow(7, possibleChars.Length); //int might be too small
            previous = (previous + generator) % modulus;
            string output = "";
            double temp = previous;

            for (int i = 0; i < 8; i++) {
                output += possibleChars[Convert.ToInt32(temp % possibleChars.Length)];
                temp = temp / possibleChars.Length;
            }

            return output;
        }
    }
}
