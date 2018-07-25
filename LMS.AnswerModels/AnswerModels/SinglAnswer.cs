using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LMS.AnswerModels
{
    [DataContract]
    public class SinglAnswer
    {
        [DataMember]
        public int Answer { get; set; }
        public SinglAnswer(int newAnswer)
        {
            Answer = newAnswer;
        }
    }
}
