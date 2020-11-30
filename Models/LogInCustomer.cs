using System.Data;
using API.Models.Interfaces;
using System.IO;
using System.Data.SQLite;
using System;

namespace API.Models
{
    public class LogInCustomer : ILogInCustomer
    {
        //the idea behind logging in a customer is to return their rewards points: -1 means the customer wasn't found, anything else means they're signed in
        public int FindCustomer(Customer value)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = @"SELECT `Customer Email`, `Rewards Points` FROM Customer WHERE `Customer Email` = @email AND `Password` = @password;";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@email", value.Email);
            cmd.Parameters.AddWithValue("@password", value.Password);
            cmd.Prepare();

            using SQLiteDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            //try to return the rewards points from the customer
            try {
                return Convert.ToInt32(rdr[1]);
            } catch {
                //if the customer doesn't exist, this will throw an exception; if the customer doesn't exist, return -1 as rewards points
                return -1;
            }
        }
    }
}