using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CRUD_NET5.Data.EF
{
    public class crudDbContextFactory : IDesignTimeDbContextFactory<crudDbContext>
    {
        public crudDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsetting.json")
                 .Build();
           
            var conectionString = configuration.GetConnectionString("crudNET5Db");
            var optionBuilder = new DbContextOptionsBuilder<crudDbContext>();

            optionBuilder.UseSqlServer(conectionString);

            return new crudDbContext(optionBuilder.Options);
        }
    }
}
