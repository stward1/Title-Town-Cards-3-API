using API.Models.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;
using System;
using System.IO;

namespace API.Models
{
	public class InsertEmployeeData : IInsertEmployee
	{
		//public void SaveUser(Employee emp)
		//public void SaveUser(Employee temp)
		public void SaveEmployee(Employee value)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
			using var con = new SQLiteConnection(cs);
			con.Open();

			using var cmd = new SQLiteCommand(con);

			cmd.CommandText = @"INSERT INTO Employee(`Employee First Name`, `Employee Last Name`, `Employee Address`, `Employee SSN`, `Employee Birth Date`, `Username`, `Password`) VALUES(@EmployeeFirstName, @EmployeeLastName, @EmployeeAddress, @EmployeeSSN, @EmployeeBirthDate, @Username, @Password)";
			cmd.Parameters.AddWithValue("@EmployeeFirstName", value.FirstName);
			cmd.Parameters.AddWithValue("@EmployeeLastName", value.LastName);
			cmd.Parameters.AddWithValue("@EmployeeAddress", value.Address);
			cmd.Parameters.AddWithValue("@EmployeeSSN", value.SSN);
			cmd.Parameters.AddWithValue("@EmployeeBirthDate", value.BirthDate);
			cmd.Parameters.AddWithValue("@Username", value.Username);
			cmd.Parameters.AddWithValue("@Password", value.Password);
			cmd.Prepare();
			cmd.ExecuteNonQuery();
		}
	}
}
