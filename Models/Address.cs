using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Address
    {

         ///////Basic attributes needed for the request
        public int id { get; set;}
        public string type_address { get; set;}
        public string address { get; set;}
        public string full_street_address { get; set;}
        public string city { get; set;}
        public string country { get; set;}

        
        // public virtual ICollection<Building> Building { get; set;}

        public  Building Building { get; set; }
    }
}