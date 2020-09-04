using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfolioItems>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<Owner>().HasData(

                new Owner()
                {
                    Id = Guid.NewGuid(),
                    Avatar = "Avarat.jpg",
                    FullName = "Raji Alshawa A",
                    Profil = "MCSD.NET Professional"
                });

            modelBuilder.Entity<AboutMe>().HasData(
                new AboutMe()
                {
                    Id = Guid.NewGuid(),
                    Section1 = "This text to be filled in section 1",
                    Section2 = "This text to be filled in section 2"
                });
        }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<PortfolioItems> PortfolioItems { get; set; }
        public DbSet<AboutMe> AboutMe { get; set; }
        public DbSet<ContactMe> ContactMe { get; set; }
    }
}
