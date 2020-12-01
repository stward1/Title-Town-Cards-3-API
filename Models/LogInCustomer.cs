using System.Data;
using API.Models.Interfaces;
using System.IO;
using MySql.Data.MySqlClient;
using System;

namespace API.Models
{
    public class LogInCustomer : ILogInCustomer
    {
        //the idea behind logging in a customer is to return their rewards points: -1 means the customer wasn't found, anything else means they're signed in
        public int FindCustomer(Customer value)
        {
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                int temp = -1;

                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

                string stm = @"SELECT `Customer Email`, `Rewards Points` FROM Customer WHERE `Customer Email` = @email AND `Password` = @password;";
                MySqlCommand cmd = new MySqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@email", value.Email);
                cmd.Parameters.AddWithValue("@password", value.Password);
                cmd.Prepare();

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //try to return the rewards points from the customer
                        try {
                            temp = Convert.ToInt32(rdr[1]);
                        } catch {
                            db.CloseConnection();
                            //if the customer doesn't exist, this will throw an exception; if the customer doesn't exist, return -1 as rewards points
                            return temp;
                        }
                    }
                }
                
                db.CloseConnection();

                return temp;
            }

            //this sentinel value will indicate that the connection failed, which might be helpful on the front-end
            return -2;
        }
    }
}