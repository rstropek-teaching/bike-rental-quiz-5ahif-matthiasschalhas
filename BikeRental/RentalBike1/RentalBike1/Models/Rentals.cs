using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentalBike1.Models
{
    public class Rentals
    {
        public int Id { get; set; }

        public Customers Customer { get; set; }

        public Bikes Bike { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public double TotalCost { get; set; }
        public Boolean Paid { get; set; }
    }
}
