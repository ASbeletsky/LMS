using Newtonsoft.Json;

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
            var des = JsonConvert.DeserializeObject(obj);
            switch (des)
            {
                case SinglAnswer singl:
                    return singl;
                case MultyAnswer multy:
                    return multy;
                case OpenAnswer open:
                    return open;
                default:
                    return null;
            }
        }
    }
}
