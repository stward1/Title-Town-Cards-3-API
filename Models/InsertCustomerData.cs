using API.Models.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;
using System;
using System.IO;

namespace API.Models
{
	public class InsertCustomerData : IInsertCustomer, IUpdateCustomer
	{
        public void SaveCustomer(string FirstName, string LastName, string Email, string Password, string PhoneNumber, int RewardsPoints)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
			using var con = new SQLiteConnection(cs);
			con.Open();

			using var cmd = new SQLiteCommand(con);

			cmd.CommandText = @"INSERT INTO Customer(`Customer Email`, `Customer First Name`, `Customer Last Name`, `Customer Phone Number`, `Rewards Points`, `Password`) VALUES(@CustomerEmail, @CustomerFirstName, @CustomerLastName, @CustomerPhoneNumber, @RewardsPoints, @Password)";
			cmd.Parameters.AddWithValue("@CustomerEmail", Email);
			cmd.Parameters.AddWithValue("@CustomerFirstName", FirstName);
			cmd.Parameters.AddWithValue("@CustomerLastName", LastName);
			cmd.Parameters.AddWithValue("@CustomerPhoneNumber", PhoneNumber);
			cmd.Parameters.AddWithValue("@RewardsPoints", RewardsPoints);
			cmd.Parameters.AddWithValue("@Password", Password);
			cmd.Prepare();
			cmd.ExecuteNonQuery();
        }

		public int UpdateRewardsPoints(Customer value)
		{
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
			using var con = new SQLiteConnection(cs);
			con.Open();

			using var cmd = new SQLiteCommand(con);

			cmd.CommandText = @"UPDATE Customer SET `Rewards Points` = @RewardsPoints WHERE `Customer Email` = @CustomerEmail";
			cmd.Parameters.AddWithValue("@CustomerEmail", value.Email);
			cmd.Parameters.AddWithValue("@RewardsPoints", value.RewardsPoints);
			cmd.Prepare();
			cmd.ExecuteNonQuery();			

			return value.RewardsPoints;
		}
	}
}
