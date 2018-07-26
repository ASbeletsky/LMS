using System.ComponentModel.DataAnnotations;

namespace LMS.Dto
{
    public class ExameneeDTO
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Test")]
        public TestDTO Test { get; set; }

        public int TestId { get; set; }

        public TestSessionDTO Session { get; set; }

        public int SessionId { get; set; }
    }
}
