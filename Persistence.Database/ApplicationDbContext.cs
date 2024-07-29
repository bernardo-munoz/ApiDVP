using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Database.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<Persons> Persons { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<TypeDocuments> TypeDocuments{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ModelConfig(modelBuilder);
        }

        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new PersonsConfiguration(modelBuilder.Entity<Persons>());
            new UsersConfiguration(modelBuilder.Entity<Users>());
            new TypeDocumentsConfiguration(modelBuilder.Entity<TypeDocuments>());
        }

    }
}
