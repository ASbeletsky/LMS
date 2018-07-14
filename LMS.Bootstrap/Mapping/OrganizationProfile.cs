using AutoMapper;
using LMS.Dto;
using LMS.Entities;

namespace LMS.Bootstrap.Mapping
{
    public class MappingsContainer : Profile
    {
        public MappingsContainer()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<TaskType, TaskTypeDTO>();
            CreateMap<TaskTypeDTO, TaskType>();

            CreateMap<TaskDTO, Task>();
            CreateMap<Task, TaskDTO>();
        }
    }
}
