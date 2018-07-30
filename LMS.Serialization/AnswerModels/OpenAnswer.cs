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
        public string Content { get; set; }
        public OpenAnswer(string newAnswers)
        {
            Content = newAnswers;
        }
    }
}
