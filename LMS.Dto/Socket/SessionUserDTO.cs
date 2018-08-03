using System;

namespace LMS.Dto
{
    public class SessionUserDTO
    {
        public string Id { get; set; }

        public DateTimeOffset? StartTime { get; set; }
        public TimeSpan? Duration { get; set; }

        public TestTasksStateDTO TasksState { get; set; }
    }
}
