using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevator_RESTApi.Models;

namespace Rocket_Elevator_RESTApi.Controllers
{

    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly InformationContext _context;  

        public CustomersController(InformationContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            return await _context.customers.ToListAsync();
        }

        // ========== Get all the infos about a customer (buildings, batteries, columns, elevators) using the customer_id ==========
        // GET: api/Customers/cindy@client.com
        [HttpGet("{email}")]
        public async Task<ActionResult<Customer>> GetCustomer(string email)
        {
            var customer = await _context.customers.Include("Buildings.Batteries.Columns.Elevators")
                                                .Where(c => c.cpy_contact_email == email)
                                                .FirstOrDefaultAsync();  

            // customer = await _context.customers.Include("Buildings.Addresses")
            //                                     .Where(c => c.cpy_contact_email == email)
            //                                     .FirstOrDefaultAsync();          

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        } 

        // ========== Verify email for register at the Customer's Portal =========================================================================
        // GET: api/Customers/verify/cindy@client.com
        [HttpGet("verify/{email}")]
        public async Task<ActionResult> VerifyEmail(string email)
        {
            var customer = await _context.customers.Include("Buildings.Batteries.Columns.Elevators")
                                                .Where(c => c.cpy_contact_email == email)
                                                .FirstOrDefaultAsync();            

            if (customer == null)
            {
                return NotFound();
            }

            return Ok();
        } 

        // ========== Put for update the customer infos =========================================================================
        // PUT: api/Customers/cindy@client.com
        [HttpPut]
        [EnableCors("MyPolicy")]
        public async Task<ActionResult<Customer>> PutCustomer(Customer customer)
        {
            var customerToUpdate = await _context.customers
                                                .Where(c => c.cpy_contact_email == customer.cpy_contact_email)
                                                .FirstOrDefaultAsync(); 

            if (customerToUpdate == null)
            {
                return NotFound();
            }

            customerToUpdate.company_name = customer.company_name;
            customerToUpdate.cpy_contact_name = customer.cpy_contact_name;
            customerToUpdate.cpy_contact_phone = customer.cpy_contact_phone;
            customerToUpdate.sta_name = customer.sta_name;
            customerToUpdate.sta_phone = customer.sta_phone;
            customerToUpdate.sta_mail = customer.sta_mail;
            customerToUpdate.cpy_description = customer.cpy_description;

            await _context.SaveChangesAsync();

            return customerToUpdate;
        }
    }
}