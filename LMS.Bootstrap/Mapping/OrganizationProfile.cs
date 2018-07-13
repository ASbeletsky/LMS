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

            CreateMap<QuestionType, QuestionTypeDTO>();
            CreateMap<QuestionTypeDTO, QuestionType>();

            CreateMap<QuestionDTO, Question>();
            CreateMap<Question, QuestionDTO>();
        }
    }
}
