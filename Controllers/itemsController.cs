using System.Linq.Expressions;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Models.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class itemsController : ControllerBase
    {
        // GET: api/items
        [EnableCors("AnotherPolicy")]
        [HttpGet]
        public List<Item> ReadItems()
        {
            IGetAllItems readObject = new ReadItemData();
            return readObject.GetAllItems();
        }

        // GET: api/items/5
        [EnableCors("AnotherPolicy")]        
        [HttpGet("{id}")]
        public Item Get(int id)
        {         
            IGetItem readObject = new ReadItemData();
            return readObject.GetItem(id);
        }

        [EnableCors("AnotherPolicy")]
        [HttpGet("cartinfo/{items}")]
        public CartInfo GetCartInfo(string items)
        {
            IGetItem readObject = new ReadItemData();
            CartInfo r = new CartInfo();

            var split = items.Split(',');

            //SubTotal
            double subtotal = 0.0;
            foreach (var item in split)
            {
                //Change this to parse
                var data = readObject.GetItem(Convert.ToInt32(item));
                r.Items.Add(data);
                subtotal += data.ItemPrice;
            }
            r.Subtotal = subtotal;

            //Discount
            //if(user.rewards >= 5)
            //{
                    var newTotal = (r.Subtotal * (1 - 0.1));
            //}

            //Tax
            r.Tax = Math.Round((subtotal * 0.1), 2);

            //Rewards
            r.Rewards = (int) ((r.Subtotal - (r.Subtotal % 10)) / 10);
            
            return r;
        }

        // POST: api/items
        [EnableCors("AnotherPolicy")]
        [HttpPost]
        public void Post([FromBody] Item value)
        {
            IInsertItem saveObj = new InsertItemData();
            saveObj.AddItem(value);
        }

        // PUT: api/items/5
        [EnableCors("AnotherPolicy")]
        [HttpPut("{id}")]
        public void Put([FromBody] Item value)
        {
            IUpdateItem updateObj = new UpdateItemData();
            updateObj.UpdateItem(value);
        }

        // DELETE: api/items/5
        [EnableCors("AnotherPolicy")]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            IDeleteItem deleteObj = new DeleteItemData();
            deleteObj.DeleteItem(id);
        }
    }
}
