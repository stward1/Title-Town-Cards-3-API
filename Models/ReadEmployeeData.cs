using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace API.Models
{
    public class ReadEmployeeData
    {
        public List<Employee> GetAllEmployees()
        {   
            List<Employee> employees = new List<Employee>();
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using (var con = new SQLiteConnection(cs)) 
            {
                con.Open();

                string stm = "SELECT * FROM Employee;";
                using (var cmd = new SQLiteCommand(stm, con))
                {                    
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            employees.Add(new Employee(){EmployeeID = rdr.GetInt32(0), FirstName = rdr.GetString(1), LastName = rdr.GetString(2), Address = rdr.GetString(3), SSN = rdr.GetInt32(4), BirthDate = rdr.GetString(5), Username = rdr.GetString(6), Password = rdr.GetString(7)});
                        }
                    }
                }
            }            
        return employees;
        }

        public Employee GetEmployee(int id)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM Employee WHERE \"Employee ID\" = @id;";
                using (var cmd = new SQLiteCommand(stm, con))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Prepare();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        return new Employee(){EmployeeID = rdr.GetInt32(0), FirstName = rdr.GetString(1), LastName = rdr.GetString(2), Address = rdr.GetString(3), SSN = rdr.GetInt32(4), BirthDate = rdr.GetString(5), Username = rdr.GetString(6), Password = rdr.GetString(7)};
                    }
                }
            }
        }

        public int ValidateEmployee(string username, string password)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                string stm = "SELECT * FROM Employee WHERE \"Username\" = @username AND \"Password\" = @password;";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Prepare();
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        try {
                        return rdr.GetInt32(0);
                        } catch {
                        return -1;
                        }
                    }
                }
            }
        }
    }
}