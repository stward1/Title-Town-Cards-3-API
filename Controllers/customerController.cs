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
    public class customerController : ControllerBase
    {
        // POST: api/customer
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] Customer value)
        {
            IInsertCustomer insertObj = new InsertCustomerData();
            insertObj.SaveCustomer(value.FirstName, value.LastName, value.Email, value.Password, value.PhoneNumber, value.RewardsPoints);
        }

        // GET: api/customer
        [EnableCors("AnotherPolicy")]        
        [HttpPut]
        public int SignInCustomer([FromBody] Customer value)
        {         
            ILogInCustomer loginObj = new LogInCustomer();
            return loginObj.FindCustomer(value);
        }

        // GET: api/customer/email
        [EnableCors("AnotherPolicy")]        
        [HttpPut("{id}")]
        public int UpdatePoints([FromBody] Customer value)
        {         
            IUpdateCustomer updateObj = new InsertCustomerData();
            return updateObj.UpdateRewardsPoints(value);
        }

        // [EnableCors("AnotherPolicy")]
        // [HttpPost("register/{customername}/{password}/{firstName}/{lastName}/{address}/{ssn}/{email}/{empID}/{birthDate}")]
        // public void SaveEmployee(string customername, string password, string firstName, string lastName, string email, string address, int ssn, string birthDate, int empID)
        // {
        //     var customer = new Employee() { customername = customername, Password = password, FirstName = firstName, LastName = lastName, Address = address, SSN = ssn, BirthDate = birthDate, EmployeeID = empID};

        //     IInsertEmployee insertObj = new InsertEmployeeData();
        //     insertObj.Savecustomer(customername, password, firstName, lastName, email, address, ssn, birthDate, empID);
        // }
    }
}