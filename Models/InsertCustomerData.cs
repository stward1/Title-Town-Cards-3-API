using API.Models.Interfaces;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;
using System.IO;

namespace API.Models
{
	public class InsertCustomerData : IInsertCustomer, IUpdateCustomer
	{
        public void SaveCustomer(string FirstName, string LastName, string Email, string Password, string PhoneNumber, int RewardsPoints)
        {
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

				string stm = @"INSERT INTO Customer(`Customer Email`, `Customer First Name`, `Customer Last Name`, `Customer Phone Number`, `Rewards Points`, `Password`) VALUES(@CustomerEmail, @CustomerFirstName, @CustomerLastName, @CustomerPhoneNumber, @RewardsPoints, @Password)";
				MySqlCommand cmd = new MySqlCommand(stm, con);

				cmd.Parameters.AddWithValue("@CustomerEmail", Email);
				cmd.Parameters.AddWithValue("@CustomerFirstName", FirstName);
				cmd.Parameters.AddWithValue("@CustomerLastName", LastName);
				cmd.Parameters.AddWithValue("@CustomerPhoneNumber", PhoneNumber);
				cmd.Parameters.AddWithValue("@RewardsPoints", RewardsPoints);
				cmd.Parameters.AddWithValue("@Password", Password);
				cmd.Prepare();
				cmd.ExecuteNonQuery();

				db.CloseConnection();
			}
        }

		public int UpdateRewardsPoints(Customer value)
		{
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

				string stm = @"UPDATE Customer SET `Rewards Points` = @RewardsPoints WHERE `Customer Email` = @CustomerEmail";
				MySqlCommand cmd = new MySqlCommand(stm, con);

				cmd.Parameters.AddWithValue("@CustomerEmail", value.Email);
				cmd.Parameters.AddWithValue("@RewardsPoints", value.RewardsPoints);
				cmd.Prepare();
				cmd.ExecuteNonQuery();			

				db.CloseConnection();

				return value.RewardsPoints;
			}

			//-1 is returned if something goes wrong; -1 is a recognizable sentinel value across the system
			return -1;
		}
	}
}
