using AutoMapper;
using LMS.Dto;
using LMS.Entities;

namespace LMS.Bootstrap.Mapping
{
    public class OrganizationProfile : Profile
    {
        public OrganizationProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();
        }
    }
}
