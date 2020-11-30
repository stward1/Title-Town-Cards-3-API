using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
        public int SSN { get; set; }
    }
}