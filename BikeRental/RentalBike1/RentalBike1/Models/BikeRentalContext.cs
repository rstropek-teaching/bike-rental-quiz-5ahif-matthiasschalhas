using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalBike1.Models
{
    public class BikeRentalContext : DbContext
    {
       

        public BikeRentalContext(DbContextOptions<BikeRentalContext> opt) : base(opt)
        {

        }

        public BikeRentalContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RentalBikeDB1;Trusted_Connection=True;ConnectRetryCount=0");
        }

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
    }
}
