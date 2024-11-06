using PatikaWeek14Homework2.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PatikaWeek14Homework2.Data.Context
{
    public class JwtDbContext: DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext>  options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"server=LAPTOP-748G4HT2;database=IdentityData;Trusted_Connection=true;TrustServerCertificate=true");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      
            modelBuilder.ApplyConfiguration(new UserConfiguration());      

           


            base.OnModelCreating(modelBuilder);
        }
      
        public DbSet<UserEntity> Users => Set<UserEntity>();
       
    }
}
