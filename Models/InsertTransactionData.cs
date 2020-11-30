using API.Models.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;
using System;
using api.Models.Interfaces;
using System.IO;

namespace API.Models
{
	public class InsertTransactionData : IInsertTransaction
	{
		public void SaveTransaction(string PaymentType, int EmployeeID, string CustomerEmail, List<int> ItemIDs, double AmtDiscounted, DateTime TransactionDate)
		{
			//opening the database
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
			using var con = new SQLiteConnection(cs);
			con.Open();

			using var cmd = new SQLiteCommand(con);

			//inserting the transaction
			cmd.CommandText = @"INSERT INTO Transact(`Transaction Date`, `Amount Discounted`, `Payment Type`, `Employee ID`, `Customer Email`) VALUES(@TransactionDate, @AmountDiscounted, @PaymentType, @EmployeeID, @CustomerEmail)";
			cmd.Parameters.AddWithValue("@TransactionDate", TransactionDate);
			cmd.Parameters.AddWithValue("@AmountDiscounted", AmtDiscounted);
			cmd.Parameters.AddWithValue("@PaymentType", PaymentType);
			cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
			cmd.Parameters.AddWithValue("@CustomerEmail", CustomerEmail);
			cmd.Prepare();

			cmd.ExecuteNonQuery();

			cmd.CommandText = @"SELECT `Transaction ID` FROM Transact ORDER BY `Transaction Date` DESC LIMIT 1";
			cmd.Prepare();

			var transactionID = cmd.ExecuteScalar();

			foreach (int itemId in ItemIDs)
			{
				cmd.CommandText = @"UPDATE Item SET `Transaction ID` = @TransactionID, `Is Purchased` = 'true' WHERE `Item ID` = @ItemID";
				cmd.Parameters.AddWithValue("@TransactionID", transactionID);
				cmd.Parameters.AddWithValue("@ItemID", itemId);
				cmd.Prepare();

				cmd.ExecuteNonQuery();
			}
		}
	}
}
