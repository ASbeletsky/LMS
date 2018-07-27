using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class TaskClientDTO
    {

        public TaskClientDTO()
        {
            AnswerOptions = new List<TaskAnswerOptionDTO>();
        }

        public int Id { get; set; }

        public int Complexity { get; set; }

        public string Content { get; set; }

        public TaskTypeDTO Type { get; set; }

        [Display(Name = "Type")]
        public int TypeId { get; set; }

        public CategoryDTO Category { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IList<TaskAnswerOptionDTO> AnswerOptions { get; set; }
    }
}
