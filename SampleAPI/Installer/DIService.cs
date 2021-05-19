using Microsoft.Extensions.DependencyInjection;
using SampleAPI.Models;
using SampleAPI.Repository;

namespace SampleAPI.Installer
{
    public static class DIService
    {
        public static void DIInstallService(IServiceCollection services)
        {
            services.AddSingleton<IRandomStrings, RandomStrings>();
            services.AddSingleton<IGenerateRandomId, GenerateRandomId>();
            services.AddTransient<IAuthentication, Authentication>();
            services.AddTransient<ICheckMember, CheckMember>();
        }
    }
}