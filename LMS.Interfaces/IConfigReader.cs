namespace LMS.Interfaces
{
    public interface IConfigReader
    {
        // T Get<T>(string key);
        string GetConnectionString(string name);
    }
}
