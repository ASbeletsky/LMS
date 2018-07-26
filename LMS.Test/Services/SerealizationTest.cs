using Xunit;
using LMS.AnswerModels;
using System.Collections.Generic;

namespace LMS.Test.Services
{
    public class SerealizationTest
    {
        [Fact]
        public void Serialize_Singl_Test()
        {
            var som = new SinglAnswer(2);
            var ser = SerializeControler.SerializeToJSON(som);
            Assert.Equal("{\"Answer\":2}", ser);
            var answer = SerializeControler.DeserializeJSON(ser);
            Assert.IsType<SinglAnswer>(answer);
            Assert.Equal(som.ToString(), answer.ToString());
        }
        [Fact]
        public void Serialize_Multy_Test()
        {
            var som = new MultyAnswer(new List<int> {1,2,3,4 });
            var ser = SerializeControler.SerializeToJSON(som);
            Assert.Equal("{\"Answer\":[1,2,3,4]}", ser);
            var answer = SerializeControler.DeserializeJSON(ser);
            Assert.IsType<MultyAnswer>(answer);
            Assert.Equal(som.ToString(),answer.ToString());
        }
        [Fact]
        public void Serialize_Open_Test()
        {
            var som = new OpenAnswer("Some text");
            var ser = SerializeControler.SerializeToJSON(som);
            Assert.Equal("{\"Answer\":\"Some text\"}", ser);
            var answer = SerializeControler.DeserializeJSON(ser);
            Assert.IsType<OpenAnswer>(answer);
            Assert.Equal(som.ToString(), answer.ToString());
        }
    }
}
