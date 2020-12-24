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

    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly InformationContext _context;  

        public EmployeesController(InformationContext context)
        {
            _context = context;
        }


        // ========== Get all the infos about a employee ==========
        // GET: api/Employees/cindy@employee.com
        [HttpGet("{email}")]
        public async Task<ActionResult<Employee>> GetEmployee(string email)
        {
            var employee = await _context.employees.FromSqlRaw("select employees.* from employees join users on users.id = employees.user_id where email = {0}", email)
                                                .FirstOrDefaultAsync();  


            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        } 
    }
}