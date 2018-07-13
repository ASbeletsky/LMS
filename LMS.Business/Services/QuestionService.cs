using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LMS.Dto;
using LMS.Entities;
using LMS.Interfaces;

namespace LMS.Business.Services
{
    public class QuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public QuestionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public Task MarkAsDeletedByIdAsync(int questionId)
        {
            if (unitOfWork.Questions.Get(questionId) is Question question)
            {
                question.IsVisible = false;

                return unitOfWork.SaveAsync();
            }

            return Task.CompletedTask;
        }

        public QuestionDTO GetById(int questionId)
        {
            var question = unitOfWork.Questions.Get(questionId);
            if (question == null)
            {
                throw new EntityNotFoundException<Question>(questionId);
            }

            return mapper.Map<Question, QuestionDTO>(question);
        }

        public Task CreateAsync(QuestionDTO question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            if (string.IsNullOrEmpty(question.Content))
            {
                throw new ArgumentException($"{nameof(Question)}.{nameof(Question.Content)} cannot be null or empty");
            }

            var entry = mapper.Map<QuestionDTO, Question>(question);
            entry.IsVisible = true;

            unitOfWork.Questions.Create(entry);

            return unitOfWork.SaveAsync();
        }
        
        public Task UpdateAsync(QuestionDTO questionDto)
        {
            if (questionDto == null)
            {
                throw new ArgumentNullException(nameof(questionDto));
            }

            var entry = mapper.Map<QuestionDTO, Question>(questionDto);
            
            if (unitOfWork.Questions.Get(entry.Id) is Question oldQuestion)
            {
                if (oldQuestion.Content == entry.Content
                    && oldQuestion.Complexity == entry.Complexity
                    && oldQuestion.CategoryId == entry.CategoryId
                    && oldQuestion.TypeId == entry.TypeId)
                {
                    return Task.CompletedTask;
                }

                entry.IsVisible = true;
                entry.Id = 0;

                oldQuestion.IsVisible = false;

                unitOfWork.Questions.Update(oldQuestion);
                unitOfWork.Questions.Create(entry);
            }
            else
            {
                unitOfWork.Questions.Create(entry);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<QuestionDTO> GetAll(bool includeInvisible = false)
        {
            var questions = unitOfWork.Questions
                .GetAll();

            if (!includeInvisible)
                questions = questions.Where(q => q.IsVisible);

            return mapper.Map<IEnumerable<Question>, IEnumerable<QuestionDTO>>(questions);
        }
    }
}
