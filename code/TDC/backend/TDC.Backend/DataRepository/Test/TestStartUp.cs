using DataRepository;
using Microsoft.Data.SqlClient;
using System.Reflection;
using TDC.Backend.DataRepository.Config;
using TDC.Backend.DataRepository.Helper;

namespace TDC.Backend.DataRepository.Test
{
    public class TestStartUp
    {
        public static ConnectionFactory GetConnectionFactory()
        {
            var connectionStrings = GetConnectionStrings();
            UseEvolveMigration(connectionStrings);

            return new ConnectionFactory(connectionStrings);
        }

        public static ConnectionStrings GetConnectionStrings()
        {
            var connectionString = new ConnectionStrings();

            var config = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.dbtest.json")
                         .Build();
            config.Bind("ConnectionStrings", connectionString);

            return connectionString;
        }

        public static void CleanTables(IEnumerable<BaseRepository> repositories)
        {
            foreach (var repository in repositories)
            {
                repository.CleanTable();
            }
        }

        private static void UseEvolveMigration(ConnectionStrings connectionStrings)
        {
            var workingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
            var location = Path.Combine(workingDir, "Database", "migrations");
            var connection = new SqlConnection(connectionStrings.Sql);

            var evolve = new EvolveDb.Evolve(connection, message => Console.WriteLine(message))
            {
                Locations = [location],
                IsEraseDisabled = true,
            };

            evolve.Migrate();
        }
    }
}
