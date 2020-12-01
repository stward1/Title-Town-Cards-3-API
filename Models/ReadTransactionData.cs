using System.IO.Enumeration;
using System.IO;
using System.Security.Cryptography;
using System.Security.AccessControl;
using API.Models.Interfaces;
using API.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace API.Models
{
    public class ReadTransactionData : IGetAllTransactions
    {
        public List<Transaction> GetAllTransactions()
        {
            //making an item selector up front
            IGetTransactionItems readObj = new ReadItemData();
            //making an empty list of transactions            
            List<Transaction> transactions = new List<Transaction>();

			//connecting to and opening the database
			DBConnect db = new DBConnect();
			bool isOpen = db.OpenConnection();

			if (isOpen)
			{
				//if the open succeeded, we proceed with the sql commands
				MySqlConnection con = db.GetCon();

                string stm = "SELECT * FROM Transact";
                MySqlCommand cmd = new MySqlCommand(stm, con);
                
                using (var rdr = cmd.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        Transaction newTrans = new Transaction() {TransactionID = rdr.GetInt32(0), TransactionDate = DateTime.Parse(rdr.GetString(1)), AmtDiscounted = rdr.GetDouble(2), PaymentType = rdr.GetString(3), EmployeeID = rdr.GetInt32(4), CustomerEmail = rdr.GetString(5)};

                        List<Item> transItems = readObj.GetTransactionItems(newTrans.TransactionID);
                        newTrans.ItemIDs = GetItemIDs(transItems);
                        newTrans.Subtotal = GetSubtotal(transItems);

                        transactions.Add(newTrans);   
                    }
                }

                db.CloseConnection();

                return transactions;
            }

            return transactions;
        }

        public List<int> GetItemIDs(List<Item> transItems)
        {
            List<int> ids = new List<int>();

            foreach (Item item in transItems)
            {
                ids.Add(item.ItemID);
            }

            return ids;
        }

        public double GetSubtotal(List<Item> transItems)
        {
            double subtotal = 0;

            foreach (Item item in transItems)
            {
                subtotal += item.ItemPrice;
            }

            return subtotal;
        }
    }
}