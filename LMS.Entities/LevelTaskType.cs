using System;

namespace LMS.Entities
{
    public class LevelTaskType : IEquatable<LevelTaskType>
    {
        public TestTemplateLevel TestTemplateLevel { get; set; }
        public int TestTemplateLevelId { get; set; }
        
        public TaskType TaskType { get; set; }
        public int TaskTypeId { get; set; }

        public bool Equals(LevelTaskType other)
        {
            return other != null
                && other.TestTemplateLevelId == TestTemplateLevelId
                && other.TaskTypeId == TaskTypeId;
        }
    }
}
