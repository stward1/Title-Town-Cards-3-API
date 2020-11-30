using System.Collections.Generic;
using System;

namespace API.Models
{
    public class Transaction
    {
        public int TransactionID {get; set;}
        public string PaymentType { get; set;}
        public int EmployeeID { get; set; }
        public string CustomerEmail { get; set; }
        public List<int> ItemIDs {get; set;}
        public double Subtotal {get; set;}
        public double AmtDiscounted {get; set;}
        public DateTime TransactionDate { get; set; }

        public Transaction()
        {
            this.ItemIDs = new List<int>();
        }

        // public string CalculateTransTotal(List<string> ItemIDs)
        // {
        //     int TransactionTotal;
        //     foreach(var id in ItemIDs)
        //     {
        //         IGetItem readObject = new ReadItemData();
        //         Item temp = readObject.GetItem(id);
        //         Console.WriteLine(temp);
        //     }
        // }
    }
}