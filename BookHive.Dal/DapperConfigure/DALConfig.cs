using Microsoft.Extensions.Configuration;
using System.Data;

namespace BookHive.Dal.DapperConfigure
{
    public class DALConfig
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
        public IDbConnection GetDbConnection()
        {
            var settings = GetConfig();
            var connectionString = settings["ConnectionStrings:connection"];
            return new System.Data.SqlClient.SqlConnection(connectionString);
        }
    }
}
