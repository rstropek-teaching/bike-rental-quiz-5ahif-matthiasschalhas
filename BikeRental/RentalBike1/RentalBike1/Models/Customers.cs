using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentalBike1.Models
{
    public class Customers
    {
        public int Id { get; set; }

        public Gender Gender { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(75)]
        public string LastName { get; set; }

        
        public DateTime Birthday { get; set; }

        [MaxLength(75)]
        public string Street { get; set; }


        public int HouseNumber { get; set; }

        public int ZipCode { get; set; }

        [MaxLength(75)]
        public string Town { get; set; }
    }
}
