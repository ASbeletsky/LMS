using LMS.Entities;
using LMS.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LMS.Business.Services
{
    public class QuestionService
    {
        private readonly IUnitOfWork unitOfWork;
        public QuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Task DeleteQuestion(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            unitOfWork.Questions.Delete(question.Id);
            return unitOfWork.SaveAsync();
        }

        public Task NewQuestion(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }
            if (question.Type == null)
            {
                throw new ArgumentException($"{nameof(Question)}.{nameof(Question.Type)} cannot be null");
            }
            if (string.IsNullOrEmpty(question.Content))
            {
                throw new ArgumentException($"{nameof(Question)}.{nameof(Question.Content)} cannot be null or empty");
            }

            unitOfWork.Questions.Create(question);
            return unitOfWork.SaveAsync();
        }

        public Task UpdateQuestion(Question question)
        {
            if (question == null)
            {
                throw new ArgumentNullException(nameof(question));
            }

            var parentTest = unitOfWork.Tests.Find(t => t.Questions.Any(q => q.Id == question.Id));
            if (parentTest == null)
            {
                unitOfWork.Questions.Update(question);
            }
            else
            {
                question.IsVisible = false;
                unitOfWork.Questions.Update(question);

                unitOfWork.Questions.Create(question);
            }
            return unitOfWork.SaveAsync();
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return unitOfWork.Questions.GetAll();
        }

        public IEnumerable<Question> GetAllQuestionsByTest(int testId)
        {
            return unitOfWork.Tests.Get(testId).Questions;
        }
    }
}
