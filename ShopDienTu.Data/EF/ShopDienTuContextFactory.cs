using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ShopDienTu.Utilities.Constants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ShopDienTu.Data.EF
{
   public class ShopDienTuContextFactory : IDesignTimeDbContextFactory<ShopDienTuDbContext>
    {
        public ShopDienTuDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            var connectionString = configuration.GetConnectionString(SystemConstants.MainConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<ShopDienTuDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ShopDienTuDbContext(optionsBuilder.Options);
        }
    }
}
