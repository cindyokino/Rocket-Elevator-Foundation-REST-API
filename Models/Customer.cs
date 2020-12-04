using System;
using System.Collections.Generic;


namespace Rocket_Elevator_RESTApi.Models
{
    public class Customer
    {
        public int id { get; }
        public DateTime date_create { get; set; }
        public string company_name { get; set; }
        public string cpy_contact_name { get; set; }
        public string cpy_contact_phone { get; set; }
        public string cpy_contact_email { get; set; }  // This email is the key, like an Id 
        public string cpy_description { get; set; } 
        public string sta_name { get; set; }
        public string sta_phone { get; set; }
        public string sta_mail { get; set; }       
        public int user_id { get; set; }
        public int address_id { get; set; }

        public virtual ICollection<Building> Buildings { get; set; }
        
    }
}