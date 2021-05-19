using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace ETCMemberTest.Moq
{
    public static class CreateHostBuilder
    {
        public static IHostBuilder Create()
        {
            return new HostBuilder()
            .ConfigureWebHost(webHost =>
            {
                // Add TestServer
                webHost.UseTestServer();
                webHost.UseStartup<TestStartup>();
            });
        }
    }
}
