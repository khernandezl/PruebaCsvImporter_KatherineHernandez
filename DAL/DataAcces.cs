using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAcces : DbContext
    {
        public DataAcces()
        {
            //var objectContext = (this as IObjectContextAdapter).ObjectContext;

            //objectContext.CommandTimeout = 120;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
            var root = builder.Build();
            optionsBuilder.UseSqlServer(root.GetConnectionString("CsvImporter"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DataAcces(DbContextOptions<DataAcces> options) : base(options) { }

        public DbSet<Importer> Importers { get; set; }
    }
}
