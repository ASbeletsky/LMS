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
                        TaskTypeIds = entity.TaskTypes.Select(t => t.TaskTypeId).ToList(),
                        CategoryIds = entity.Categories.Select(t => t.CategoryId).ToList()
                    }));
            CreateMap<TestTemplateLevelDTO, TestTemplateLevel>()
                .ForMember(m => m.TestTemplateId, m => m.Ignore())
                .ForMember(m => m.MinComplexity, m => m.MapFrom(l => l.Filter.MinComplexity))
                .ForMember(m => m.MaxComplexity, m => m.MapFrom(l => l.Filter.MaxComplexity))
                .ForMember(m => m.Categories, m => m.ResolveUsing(l =>
                    l.Filter.CategoryIds.Select(id => new LevelCategory
                    {
                        CategoryId = id,
                        TestTemplateLevelId = l.Id
                    })))
                .ForMember(m => m.TaskTypes, m => m.ResolveUsing(l =>
                    l.Filter.TaskTypeIds.Select(id => new LevelTaskType
                    {
                        TaskTypeId = id,
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
