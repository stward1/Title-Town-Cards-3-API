using System.IO.Enumeration;
using System.IO;
using System.Security.Cryptography;
using System.Security.AccessControl;
using API.Models.Interfaces;
using API.Models;
using System.Collections.Generic;
using System.Data.SQLite;
using System;

namespace API.Models
{
    public class ReadTransactionData : IGetAllTransactions
    {
        public List<Transaction> GetAllTransactions()
        {
            //making an item selector up front
            IGetTransactionItems readObj = new ReadItemData();

            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM Transact";
            using var cmd = new SQLiteCommand(stm, con);
            
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            List<Transaction> transactions = new List<Transaction>();
            while(rdr.Read())
            {
                Transaction newTrans = new Transaction() {TransactionID = rdr.GetInt32(0), TransactionDate = DateTime.Parse(rdr.GetString(1)), AmtDiscounted = rdr.GetDouble(2), PaymentType = rdr.GetString(3), EmployeeID = rdr.GetInt32(4), CustomerEmail = rdr.GetString(5)};

                List<Item> transItems = readObj.GetTransactionItems(newTrans.TransactionID);
                newTrans.ItemIDs = GetItemIDs(transItems);
                newTrans.Subtotal = GetSubtotal(transItems);

                transactions.Add(newTrans);   
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