using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocket_Elevator_RESTApi.Models;

namespace Rocket_Elevator_RESTApi.Controllers
{
    [Route("api/Interventions")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly InformationContext _context;

        public InterventionsController(InformationContext context)
        {
            _context = context;
        }

        // ******* This GET returns all fields of all Interventions records that do not have a start date and are in "Pending" status. *******
        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetIntervention()
        {            
            var queryInterventions = from intervention in _context.interventions
                                        where intervention.start_date_time == null && intervention.status == "Pending"
                                        select intervention;

            return await queryInterventions.ToListAsync();        
        }

        // ******* This PUT change the status of the intervention request to "InProgress" and add a start date and time (Timestamp). *******
        // PUT: api/Interventions/5/start-progress
        [HttpPut("{id}/start-progress")]
        public async Task<ActionResult<Intervention>> PutInterventionStart(int id)
        {
           var existingIntervention = await _context.interventions.Where(i => i.id == id)
                                                    .FirstOrDefaultAsync<Intervention>();

            if(existingIntervention == null)
            {
                return NotFound();
            }

            existingIntervention.start_date_time = DateTime.Now;
            existingIntervention.updated_at = DateTime.Now;
            existingIntervention.status = "InProgress";

            _context.SaveChanges();

            return existingIntervention;
        }
        
        // ******* This PUT change the status of the request for action to "Completed" and add an end date and time (Timestamp). *******
        // PUT: api/Interventions/1/complete-progress
        [HttpPut("{id}/complete-progress")]
        public async Task<ActionResult<Intervention>> PutInterventionEnd(int id)
        {
           var existingIntervention = await _context.interventions.Where(i => i.id == id)
                                                    .FirstOrDefaultAsync<Intervention>();

            if(existingIntervention == null)
            {
                return NotFound();
            }

            existingIntervention.end_date_time = DateTime.Now;
            existingIntervention.updated_at = DateTime.Now;
            existingIntervention.status = "Completed";

            _context.SaveChanges();

            return existingIntervention;
        }
    }
}
