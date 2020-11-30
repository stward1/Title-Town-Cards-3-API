using System.Data;
using API.Models.Interfaces;
using System.IO;
using System.Data.SQLite;
using System;

namespace API.Models
{
    public class LogInEmployee : ILogInEmployee
    {
        //returning the employee's id or -1 if not found
        public int FindEmployee(Employee value)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            string stm = @"SELECT `Employee ID` FROM Employee WHERE `Username` = @username AND `Password` = @password;";
            using var cmd = new SQLiteCommand(stm, con);
            cmd.Parameters.AddWithValue("@username", value.Username);
            cmd.Parameters.AddWithValue("@password", value.Password);
            cmd.Prepare();

            using SQLiteDataReader rdr = cmd.ExecuteReader();
            rdr.Read();

            //try to return the id of the employee
            try {
                return Convert.ToInt32(rdr[0]);
            } catch {
                //if the employee doesn't exist, an exception will be thrown, leading to -1 being returned
                return -1;
            }
        }
    }
}