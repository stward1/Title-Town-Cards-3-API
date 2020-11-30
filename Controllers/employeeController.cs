using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Models.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeeController : ControllerBase
    {
        // POST: api/employee
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] Employee value)
        {
            IInsertEmployee insertObj = new InsertEmployeeData();
            insertObj.SaveEmployee(value);
        }

        // GET: api/employee
        [EnableCors("AnotherPolicy")]        
        [HttpPut]
        public int SignInEmployee([FromBody] Employee value)
        {         
            ILogInEmployee loginObj = new LogInEmployee();
            return loginObj.FindEmployee(value);
        }

        // [EnableCors("AnotherPolicy")]
        // [HttpPost("register/{username}/{password}/{firstName}/{lastName}/{address}/{ssn}/{email}/{empID}/{birthDate}")]
        // public void SaveEmployee(string username, string password, string firstName, string lastName, string email, string address, int ssn, string birthDate, int empID)
        // {
        //     var user = new Employee() { Username = username, Password = password, FirstName = firstName, LastName = lastName, Address = address, SSN = ssn, BirthDate = birthDate, EmployeeID = empID};

        //     IInsertEmployee insertObj = new InsertEmployeeData();
        //     insertObj.SaveUser(username, password, firstName, lastName, email, address, ssn, birthDate, empID);
        // }
    }
}