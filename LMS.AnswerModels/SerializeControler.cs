using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace LMS.AnswerModels
{
    public static class SerializeControler
    {
        public static string SerializeToJSON<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static object DeserializeJSON(string obj)
        {
            try
            {
                var des = JsonConvert.DeserializeObject<IDictionary<string, object>>(obj);
                if (des.Count == 1)
                {
                    switch (des["Answer"])
                    {
                        case long singl:
                            return new SinglAnswer(Convert.ToInt32(singl));
                        case JArray multy:
                            List<int> result = new List<int>();
                            foreach (var item in multy.Children())
                            {
                                result.Add(int.Parse(item.ToString()));
                            }
                            return new MultyAnswer(result);
                        case string open:
                            return new OpenAnswer(open);
                        default:
                            return null;
                    }
                }
                return null;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
