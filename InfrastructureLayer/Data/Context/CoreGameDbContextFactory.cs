using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfrastructureLayer.Data.Context
{
    public class CoreGameDbContextFactory : IDesignTimeDbContextFactory<CoreGameDbContext>
    {
        public CoreGameDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CoreGameDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                "Server=(localdb)\\mssqllocaldb;Database=CoreGameDb;Trusted_Connection=true;MultipleActiveResultSets=true";

            optionsBuilder.UseSqlServer(connectionString);

            return new CoreGameDbContext(optionsBuilder.Options);
        }
    }
}
