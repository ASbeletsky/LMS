using Microsoft.Extensions.Configuration;
using LMS.Interfaces;

namespace LMS.Bootstrap
{
    public class AspNetConfigReader : IConfigReader
    {
        private readonly IConfiguration configuration;

        public AspNetConfigReader(IConfiguration config)
        {
            configuration = config;
        }

        public string GetConnectionString(string name)
        {
            return configuration.GetConnectionString(name);
        }
    }
}
