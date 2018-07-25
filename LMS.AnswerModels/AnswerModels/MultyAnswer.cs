using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace LMS.AnswerModels
{
    [DataContract]
    public class MultyAnswer
    {
        [DataMember]
        public List<int> Answer { get; set; }
        public MultyAnswer(List<int> newAnswers)
        {
            Answer= newAnswers;
        }
    }
}
