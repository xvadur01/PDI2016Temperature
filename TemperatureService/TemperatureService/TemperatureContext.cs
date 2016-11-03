using System;
using System.Data.Common;
using System.Data.Entity;
using MySql.Data.Entity;

namespace TemperatureService
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class TemperatureContext : DbContext
    {
        public TemperatureContext() : base("DefaultConnectionString")
        {
            Database.SetInitializer<TemperatureContext>(new DropCreateDatabaseIfModelChanges<TemperatureContext>());
        }
        public DbSet<TemperatureMessage> TemperatureMessages { get; set; }        
    }
}