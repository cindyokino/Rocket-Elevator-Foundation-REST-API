using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Rocket_Elevator_RESTApi.Models
{
    public class Employee
    {
         ///////Basic attributes needed for the request
        public int id { get; set;}
        public string first_name { get; set;}
        public string last_name { get; set;}
        public string title { get; set;}
        public int user_id { get; set;}
    }
}