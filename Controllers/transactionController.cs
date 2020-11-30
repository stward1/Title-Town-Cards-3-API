using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class transactionController : ControllerBase
    {
        // GET: api/transaction
        [EnableCors("AnotherPolicy")] 
        [HttpGet]
        public List<Transaction> GetTransactions()
        {
            IGetAllTransactions readObject = new ReadTransactionData();
            return readObject.GetAllTransactions();
        }

        // GET: api/transaction/5
        [EnableCors("AnotherPolicy")]        
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/transaction
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] Transaction value)
        {
            value.TransactionDate = DateTime.Now;

            IInsertTransaction insertObj = new InsertTransactionData();
            insertObj.SaveTransaction(value.PaymentType, value.EmployeeID, value.CustomerEmail, value.ItemIDs, value.AmtDiscounted, value.TransactionDate);

            //var result = await _userManager.CreateAsync(user, password);
            //return Ok(result.Succeeded);
        }

        public void Register(Employee temp)
        //public void SaveEmployee(string username, string password, string firstName, string lastName, string address, int ssn, string email, int empID, string birthDate)
        {
            //var user = new Employee() { Username = username, Password = password, FirstName = firstName, LastName = lastName, Address = address, SSN = ssn, BirthDate =birthDate, EmployeeID = empID};

            /* TEMPORARY COMMENT 
            IInsertEmployee insertObj = new InsertEmployeeData();
            insertObj.SaveUser(temp); */
        }

        // PUT: api/transaction/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
