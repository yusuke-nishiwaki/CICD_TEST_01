using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleAPI.Database;
using SampleAPI.Models;
using SampleAPITest.Moq;

namespace SampleAPITest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>(options =>
                    options.UseInMemoryDatabase(databaseName: "MemberTest")
            );

            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DBContext>()
            .AddDefaultTokenProviders();
        }
    }
}