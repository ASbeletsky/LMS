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

        public static object DeserializeToDictinary(string obj)
        {
            return JsonConvert.DeserializeObject<IDictionary<string, object>>(obj);
        }

        public static object DeserializeJSON(string obj)
        {
            try
            {
                var des = JsonConvert.DeserializeObject<IDictionary<string, object>>(obj);
                var som = des.GetEnumerator();
                
                if (des.Count == 1
                    && som.MoveNext())
                {
                    switch (som.Current.Key)
                    {
                        case "AnswerOptionId":
                            return new SingleAnswer(Convert.ToInt32(som.Current.Value));
                        case "Content":
                            return new OpenAnswer(som.Current.Value as string);
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

        public static bool CheckSerialization(string obj)
        {
            try
            {
                var des = JsonConvert.DeserializeObject<IDictionary<string, object>>(obj);
                var som = des.GetEnumerator();
                if (des.Count == 1)
                {
                    switch (som.Current.Key)
                    {
                        case "AnswerOptionId":
                            return som.Current.Value is int;
                        case "Content":
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }
    }
}
