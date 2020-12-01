using API.Models.Interfaces;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace API.Models
{
	public class InsertEmployeeData : IInsertEmployee
	{
		public void SaveEmployee(Employee value)
        {
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

				string stm = @"INSERT INTO Employee(`Employee First Name`, `Employee Last Name`, `Employee Address`, `Employee SSN`, `Employee Birth Date`, `Username`, `Password`) VALUES(@EmployeeFirstName, @EmployeeLastName, @EmployeeAddress, @EmployeeSSN, @EmployeeBirthDate, @Username, @Password)";
				MySqlCommand cmd = new MySqlCommand(stm, con);

				cmd.Parameters.AddWithValue("@EmployeeFirstName", value.FirstName);
				cmd.Parameters.AddWithValue("@EmployeeLastName", value.LastName);
				cmd.Parameters.AddWithValue("@EmployeeAddress", value.Address);
				cmd.Parameters.AddWithValue("@EmployeeSSN", value.SSN);
				cmd.Parameters.AddWithValue("@EmployeeBirthDate", value.BirthDate);
				cmd.Parameters.AddWithValue("@Username", value.Username);
				cmd.Parameters.AddWithValue("@Password", value.Password);
				cmd.Prepare();
				cmd.ExecuteNonQuery();

				db.CloseConnection();
			}
		}
	}
}
