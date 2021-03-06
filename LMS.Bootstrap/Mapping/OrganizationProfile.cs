﻿using System.Linq;
using System.Collections.Generic;
using AutoMapper;
using LMS.Dto;
using LMS.Entities;
using LMS.AnswerModels;

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
            CreateMap<Task, TaskDTO>()
                .ForMember(m => m.MaxScore, m => m.Ignore());

            CreateMap<TaskAnswerOption, TaskAnswerOptionDTO>();
            CreateMap<TaskAnswerOptionDTO, TaskAnswerOption>();

            CreateMap<TaskAnswer, TaskAnswerDTO>()
                .ForMember(m => m.Content, m => m.ResolveUsing(a => SerializeControler.DeserializeJSON(a.Content)));
            CreateMap<TaskAnswerDTO, TaskAnswer>()
                .ForMember(m => m.Content, m => m.ResolveUsing(a => SerializeControler.SerializeToJSON(a.Content)));

            CreateMap<LevelTaskType, TaskTypeDTO>()
                .ConstructUsing((entity, context) => context.Mapper.Map<TaskType, TaskTypeDTO>(entity.TaskType));
            CreateMap<LevelCategory, CategoryDTO>()
                .ConstructUsing((entity, context) => context.Mapper.Map<Category, CategoryDTO>(entity.Category));

            CreateMap<TestTemplateLevel, TestTemplateLevelDTO>()
                .ForMember(m => m.ValidTaskCount, m => m.Ignore())
                .ForMember(m => m.Filter, m => m.ResolveUsing(entity =>
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

            CreateMap<TestTemplate, TestTemplateSummary>()
                .ForMember(m => m.AvgComplexity, m => m.Ignore())
                .ForMember(m => m.Categories, m => m.ResolveUsing(entity =>
                    entity.Levels
                      .SelectMany(l => l.Categories)
                      .GroupBy(l => l.CategoryId)
                      .Select(l => l.First().Category.Title)
                      .ToList()))
                .ForMember(m => m.Levels, m => m.Ignore());

            CreateMap<TestLevelTask, TaskDTO>()
                .ConstructUsing((entity, context) => context.Mapper.Map<Task, TaskDTO>(entity.Task));

            CreateMap<TestLevel, TestLevelDTO>()
                .ForMember(m => m.RequiredTasksCount, m => m.Ignore())
                .ForMember(m => m.AvailableTasks, m => m.Ignore())
                .ForMember(m => m.TaskIds, m => m.Ignore());
            CreateMap<TestLevelDTO, TestLevel>()
                .ForMember(m => m.Tasks, m => m.ResolveUsing(dto =>
                    dto.Tasks.Select(t => new TestLevelTask
                    {
                        LevelId = dto.Id,
                        TaskId = t.Id
                    })));

            CreateMap<Test, TestDTO>();
            CreateMap<TestDTO, Test>();

            CreateMap<TestSessionTest, TestDTO>()
                .ConstructUsing((entity, context) => context.Mapper.Map<Test, TestDTO>(entity.Test));

            CreateMap<TestSession, TestSessionDTO>()
                .ForMember(m => m.TestTemplateId, m => m.ResolveUsing(entity =>
                    entity.Tests.FirstOrDefault()?.Test?.TestTemplateId ?? 0))
                .ForMember(m => m.TestIds, m => m.ResolveUsing(entity =>
                    entity.Tests.Select(t => t.TestId).ToList()))
                .ForMember(m => m.MemberIds, m => m.ResolveUsing(entity =>
                    entity.Members.Select(t => t.UserId).ToList()));
            CreateMap<TestSessionDTO, TestSession>()
                .ForMember(m => m.Tests, m => m.ResolveUsing(dto =>
                    dto.TestIds.Select(id => new TestSessionTest
                    {
                        SessionId = dto.Id,
                        TestId = id
                    })))
                .ForMember(m => m.Members, m => m.ResolveUsing(dto =>
                    dto.MemberIds.Select(id => new TestSessionUser
                    {
                        SessionId = dto.Id,
                        UserId = id
                    })));


            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(m => m.Roles, m => m.Ignore())
                .ForMember(m=> m.Examinee, m => m.Ignore());

            CreateMap<ExamineeDTO, Examinee>();
            CreateMap<Examinee, ExamineeDTO>();

            CreateMap<TestSessionUser, ExameneeResultDTO>()
                .ForMember(m => m.SessionId, m => m.MapFrom(u => u.SessionId))
                .ForMember(m => m.LastReviewerName, m => m.MapFrom(u => u.LastReviewer.Name))
                .ForMember(m => m.TestTitle, m => m.MapFrom(u => u.Test.Title))
                .ForMember(m => m.UserName, m => m.MapFrom(u => u.User.Name))
                .ForMember(m => m.TotalScore, m => m.ResolveUsing(u => u.Answers.Sum(a => a.Score)));

            CreateMap<TestSession, TestSessionResultsDTO>()
                .ForMember(m => m.ExameneeResults, m => m.MapFrom(u => u.Members))
                .ForMember(m => m.EndTime, m => m.ResolveUsing(u => u.StartTime + u.Duration));
        }
    }
}
