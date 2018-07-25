using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LMS.AnswerModels
{
    [DataContract]
    public class OpenAnswer
    {
        [DataMember]
        public string Answer { get; set; }
        public OpenAnswer(string newAnswers)
        {
            Answer = newAnswers;
        }
    }
}
