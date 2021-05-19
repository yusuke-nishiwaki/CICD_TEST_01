
using Microsoft.EntityFrameworkCore;
using SampleAPI.Database;

namespace SampleAPITest.Moq
{
    public class GetDbContext
    {
        public static DbContextOptions<DBContext> Do()
        {
            return new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "MemberTest")
                .Options;

            //var config = GetAppSettings.InitConfiguration();

            //return new DbContextOptionsBuilder<DBContext>()
            //        .UseSqlServer(config["ConnectionStrings:DbConnection"])
            //        .Options;
        }
    }
}
