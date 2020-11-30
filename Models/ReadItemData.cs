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
    public class ReadItemData : IGetAllItems, IGetItem, IGetTransactionItems
    {
        public List<Item> GetAllItems()
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM Item";
            using var cmd = new SQLiteCommand(stm, con);
            
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            List<Item> items = new List<Item>();
            while(rdr.Read())
            {
                items.Add(ParseItemFromRdr(rdr));
            }
            return items;
        }
        public Item GetItem(int id)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM Item WHERE \"Item ID\" = @id;";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            rdr.Read();
            return ParseItemFromRdr(rdr);
        }

        public List<Item> GetTransactionItems(int id)
        {
            List<Item> transItems = new List<Item>();

            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = "SELECT * FROM Item WHERE `Transaction ID` = @transID";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@transID", id);
            cmd.Prepare();
            
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                transItems.Add(ParseItemFromRdr(rdr));
            }
            return transItems;
        }

        //this method takes in a sqlite reader and returns an item; the potentially null fields make this logic worth encapsulating
        public Item ParseItemFromRdr(SQLiteDataReader rdr)
        {
            //this is the purchase status boolean
            Boolean isPurchased = true;
            //these are the fields that may be null; cards won't have a description and memorabilia won't have the other three
            string desc = "";
            string condition = "";
            string sport = "";
            string team = "";

            //the integers below refer to the index of the column (ie the sixth column, or subscript 5, is the purchase status column)

            //5 is the purchase status
            if (rdr.GetString(5) == "false")
            {
                isPurchased = false;
            }

            //6 is the transaction id, which we don't need to return with items (transaction id is used for finding items when reporting on transactions)

            //7 is card condition
            if (rdr[7].GetType() != typeof(DBNull))
            {
                condition = rdr.GetString(7);
            }

            //8 is the sport
            if (rdr[8].GetType() != typeof(DBNull))
            {
                sport = rdr.GetString(8);
            }

            //9 is the team
            if (rdr[9].GetType() != typeof(DBNull))
            {
                team = rdr.GetString(9);
            }
            
            //10 is the description
            if (rdr[10].GetType() != typeof(DBNull))
            {
                desc = rdr.GetString(10);
            }       

            //returning a new item based on the values from the reader plus the results of the null-checking statements above
            return new Item(){ItemID = rdr.GetInt32(0), ItemName = rdr.GetString(1), ItemPrice = rdr.GetDouble(2), ItemYear = rdr.GetInt32(3), ItemCost = rdr.GetDouble(4), ItemIsPurchased = isPurchased, ItemCardCondition = condition, ItemCardSport = sport, ItemCardTeam = team, ItemMemorabiliaDescription = desc};     
        }
    }
}