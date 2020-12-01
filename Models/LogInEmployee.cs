using System.Data;
using API.Models.Interfaces;
using System.IO;
using MySql.Data.MySqlClient;
using System;

namespace API.Models
{
    public class LogInEmployee : ILogInEmployee
    {
        //returning the employee's id or -1 if not found
        public int FindEmployee(Employee value)
        {
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                int temp = -1;

                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

                string stm = @"SELECT `Employee ID` FROM Employee WHERE `Username` = @username AND `Password` = @password;";
                MySqlCommand cmd = new MySqlCommand(stm, con);
                cmd.Parameters.AddWithValue("@username", value.Username);
                cmd.Parameters.AddWithValue("@password", value.Password);
                cmd.Prepare();

                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        //try to return the rewards points from the customer
                        try {
                            temp = Convert.ToInt32(rdr[0]);
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