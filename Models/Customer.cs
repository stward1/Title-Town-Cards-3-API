using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Customer
    {
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        //email is the primary key for customers; this obviously cannot be auto-incremented
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int RewardsPoints { get; set; }
    }
}