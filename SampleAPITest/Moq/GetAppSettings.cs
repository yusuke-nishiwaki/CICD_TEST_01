using Microsoft.Extensions.Configuration;

namespace SampleAPITest.Moq
{
    public class GetAppSettings
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
                return config;
        }
    }
}