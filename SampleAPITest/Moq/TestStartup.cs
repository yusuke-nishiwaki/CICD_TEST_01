using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleAPI;
using SampleAPI.Database;
using SampleAPI.Installer;
using SampleAPI.Models;
using SampleAPI.Repository;
using System.Reflection;
using System.Threading;

namespace ETCMemberTest.Moq
{
    public class TestStartup : Startup {
        private IConfiguration _config { get; }

        public TestStartup(IConfiguration config) : base(config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public override void ConfigureServices(IServiceCollection services) {
            //TestServerでContextがNullになる問題への対処
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            DIService.DIInstallService(services);

            services.AddDbContext<DBContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "MemberTest")
            );

            //.NetIdentity用
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DBContext>()
            .AddDefaultTokenProviders();

            //AddApplicationPartがないとAPIが動作しない
            services.AddControllers().AddApplicationPart(Assembly.Load(new AssemblyName("SampleAPI")));;
        }
    }

    public class HttpContextAccessor : IHttpContextAccessor
    {
        private static AsyncLocal<HttpContext> _httpContextCurrent = new AsyncLocal<HttpContext>();

        public HttpContext HttpContext
        {
            get => _httpContextCurrent.Value;
            set => _httpContextCurrent.Value = value;
        }
    }
}
