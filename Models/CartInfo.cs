using System.Collections.Generic;

namespace API.Models
{
    public class CartInfo
    {
        public List<Item> Items {get; set;}
        public double Subtotal {get; set;}
        public double Tax {get; set;}
        public double Total { get 
            {
                return Subtotal + Tax;
            } 
        }
        public int Rewards {get; set;}
        
        public CartInfo() {
            this.Items = new List<Item>();
        }
    }
}