using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RentalBike1.Models;

namespace RentalBike1.Controllers
{
    [Route("api/rentals")]
    public class RentalController : Controller
    {
        public readonly BikeRentalContext Context = new BikeRentalContext();
        

      

        [HttpGet]
        [Route("start")]
        public IActionResult StartRental(int idCustomer, int idBike)
        {
            //Check if Customer and Bike are valid
            if (!CheckId(idCustomer, idBike))
            {
                return BadRequest("Ids are not valid");
            }

            Rentals neu = new Rentals()
            {
                Customer = Context.Customers.First(a => a.Id == idCustomer),
                Bike = Context.Bikes.First(a => a.Id == idBike),
                BeginTime = DateTime.Now,
                /*EndTime = null,*/ //does not work
                EndTime = new DateTime(2000, 1, 1),
                Paid = false,
                TotalCost = 0
            };

            Context.Rentals.Add(neu);
            Context.SaveChanges();

            return Ok("Rental has start" + neu);
        }
        public Boolean CheckId(int idC, int idB)
        {
            if(!Context.Customers.Any(a => a.Id == idC) || !Context.Bikes.Any(a => a.Id == idB))
            {
                return false;
            }
            if (Context.Rentals.Any(a => a.Customer.Id == idC))
            {
                return false;
            }
            if (Context.Rentals.Any(a=>a.Bike.Id == idB))
            {
                return false;
            }
            return true;
        }


        [HttpGet]
        [Route("stop")]
        public IActionResult StopRental(int idCustomer, int idBike)
        {
            var rent = Context.Rentals.First(a => a.Customer.Id == idCustomer & a.Bike.Id == idBike);
            rent.EndTime = DateTime.Now;
            

            //Calculate
            double cost = calculateCost(rent,idBike);
            rent.TotalCost = cost;
            Context.SaveChanges();
            
            return Ok(rent);
        }

        public double calculateCost(Rentals rent, int idBike)
        {
            TimeSpan t = rent.EndTime - rent.BeginTime;
            double min = t.TotalMinutes;
            double cost = 0;

            if(min <= 15)
            {
                return 0;
            }

            cost += Context.Bikes.First(a=>a.Id == idBike).PriceFirstHour;
            min -= 60;
            //The first hour is over

            while(min > 0)
            {
                cost += Context.Bikes.First(a => a.Id == idBike).PriceAddHour;
                min -= 60;
            }

            return cost;
        }


        [HttpGet]
        [Route("paid")]
        public IActionResult MarkPaid(int idCustomer, int idBike)
        {
            var rent = Context.Rentals.First(a => a.Customer.Id == idCustomer & a.Bike.Id == idBike);
            if(rent.TotalCost > 0)
            {
                rent.Paid = true;
            }
            else
            {
                return BadRequest("Customer has not paid");
            }
            Context.SaveChanges();
            return Ok("Customer (id="+idCustomer + "), Bike (id=" + idBike + ") Paid");
        }


        [HttpGet]
        [Route("unpaid")]
        public IActionResult GetUnpaid()
        {
            return Ok(Context.Rentals.Where(a=>a.Paid == false && a.EndTime > a.BeginTime && a.EndTime != null && a.TotalCost > 0).SelectMany(a=> new Object[] { a.Customer.Id, a.Customer.FirstName, a.Customer.LastName,a.Id, a.BeginTime,a.EndTime,a.TotalCost  }));
        }


    }
}