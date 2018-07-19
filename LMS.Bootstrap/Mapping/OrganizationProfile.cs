using System.Linq;
using System.Collections.Generic;
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

            CreateMap<LevelTaskType, TaskTypeDTO>()
                .ConstructUsing((entity, context) => context.Mapper.Map<TaskType, TaskTypeDTO>(entity.TaskType));
            CreateMap<LevelCategory, CategoryDTO>()
                .ConstructUsing((entity, context) => context.Mapper.Map<Category, CategoryDTO>(entity.Category));
            
            CreateMap<TestTemplateLevel, TestTemplateLevelDTO>()
                .ForMember(m => m.ValidTaskCount, m => m.Ignore())
                .ForMember(m => m.Filter, m => m.ResolveUsing((entity, dto, _, context) =>
                    new TaskFilterDTO
                    {
                        MinComplexity = entity.MinComplexity,
                        MaxComplexity = entity.MaxComplexity,
                        TaskTypes = context.Mapper.Map<IEnumerable<LevelTaskType>, IEnumerable<TaskTypeDTO>>(
                            entity.TaskTypes).ToList(),
                        Categories = context.Mapper.Map<IEnumerable<LevelCategory>, IEnumerable<CategoryDTO>>(
                            entity.Categories).ToList()
                    }));
            CreateMap<TestTemplateLevelDTO, TestTemplateLevel>()
                .ForMember(m => m.MinComplexity, m => m.MapFrom(l => l.Filter.MinComplexity))
                .ForMember(m => m.MaxComplexity, m => m.MapFrom(l => l.Filter.MaxComplexity))
                .ForMember(m => m.Categories, m => m.ResolveUsing(l =>
                    l.Filter.Categories.Select(c => new LevelCategory
                    {
                        CategoryId = c.Id,
                        TestTemplateLevelId = l.Id
                    })))
                .ForMember(m => m.TaskTypes, m => m.ResolveUsing(l =>
                    l.Filter.TaskTypes.Select(c => new LevelTaskType
                    {
                        TaskTypeId = c.Id,
                        TestTemplateLevelId = l.Id
                    })));

            CreateMap<TestTemplate, TestTemplateDTO>()
                .ForMember(m => m.AvgComplexity, m => m.Ignore());
            CreateMap<TestTemplateDTO, TestTemplate>();

            CreateMap<TestTemplate, TestTemplateListItemDTO>()
                .ForMember(m => m.AvgComplexity, m => m.Ignore())
                .ForMember(m => m.Categories, m => m.ResolveUsing(entity =>
                    entity.Levels.SelectMany(l => l.Categories).GroupBy(c => c.CategoryId).Select(c => c.First())))
                .ForMember(m => m.Tasks, m => m.Ignore());
        }
    }
}
