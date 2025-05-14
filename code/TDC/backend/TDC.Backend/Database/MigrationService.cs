using System.Diagnostics;
using EvolveDb;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace TDC.Backend.Database
{
    public static class MigrationService
    {
        public static void UseEvolveMigration(string connectionString)
        {
            try
            {
                var workingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location);
                var location = Path.Combine(workingDir, "Database", "migrations");
                var connection = new SqlConnection(connectionString);

                var evolve = new EvolveDb.Evolve(connection, msg => Debug.WriteLine(msg))
                {
                    Locations = [location],
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine("Migration failed: " + e.Message);
                throw;
            }
        }
    }
}
