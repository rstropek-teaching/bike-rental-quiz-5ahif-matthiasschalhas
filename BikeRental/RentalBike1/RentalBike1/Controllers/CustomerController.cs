using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalBike1.Models;

namespace RentalBike1.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        
        public readonly BikeRentalContext Context;
        public CustomerController(BikeRentalContext context1)
        {
            Context = context1;
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            return Ok(Context.Customers);
        }

        [HttpPut]
        public IActionResult CreateCustomers([FromBody]Customers cust)
        {
            if(cust == null)
            {
                return BadRequest("Error");
            }
            Context.Customers.Add(cust);
            Context.SaveChanges();
            return Ok("Customer created");
        }

        [Route("{index}")]
        [HttpPut]
        public IActionResult UpdateCustomers(int index,[FromBody] Customers cust)
        {
            if(!Context.Customers.Any(a=>a.Id == index))
            {
                return BadRequest("No Customer found");
            }

            var customer = Context.Customers.Where(a => a.Id == index).FirstOrDefault();
            customer.Gender = cust.Gender;
            customer.FirstName = cust.FirstName;
            customer.LastName = cust.LastName;
            customer.Street = cust.Street;
            customer.Birthday = cust.Birthday;
            customer.HouseNumber = cust.HouseNumber;
            customer.Town = cust.Town;
            customer.ZipCode = cust.ZipCode;
            Context.SaveChanges();

            return Ok("Customer updated");
        }

        [Route("{index}")]
        [HttpDelete]
        public IActionResult DeleteCustomers(int index)
        {
            if (!Context.Customers.Any(a => a.Id == index))
            {
                return BadRequest("No Customer found");
            }

            if (Context.Rentals.Any(a => a.Customer.Id == index))
            {
                return BadRequest("Delete not Possible: Customer has rented a bike ");
            }

            Context.Customers.Remove(Context.Customers.Where(a=>a.Id ==index).FirstOrDefault());
            Context.SaveChanges();


            return Ok("Customer deleted");
        }

        [Route("{index}")]
        [HttpGet]
        public IActionResult GetRentalsCustomers(int index)
        {
            if (!Context.Customers.Any(a => a.Id == index))
            {
                return BadRequest("No Customer found");
            }

            return Ok(Context.Rentals.Where(a => a.Customer.Id == index));
        }
    }
}
