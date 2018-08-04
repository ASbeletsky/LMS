using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LMS.AnswerModels
{
    [DataContract]
    public class SingleAnswer
    {
        [DataMember]
        public int AnswerOptionId { get; set; }
        public SingleAnswer(int newAnswer)
        {
            AnswerOptionId = newAnswer;
        }
    }
}
