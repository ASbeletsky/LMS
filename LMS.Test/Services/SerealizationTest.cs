using System;
using System.Linq;
using LMS.Dto;
using LMS.Interfaces;
using LMS.Business.Services;
using Moq;
using Xunit;
using Task = System.Threading.Tasks.Task;
using LMS.AnswerModels;


namespace LMS.Test.Services
{
    public class SerealizationTest
    {
        [Fact]
        public void serializeTest()
        {
            var str = "{\"answer\":2}";
            SerializeControler.DeserializeJSON(str);
            Assert.

        }
    }
}
