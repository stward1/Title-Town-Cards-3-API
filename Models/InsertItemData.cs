using API.Models.Interfaces;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace API.Models
{
	public class InsertItemData : IInsertItem
	{
		public void AddItem(Item value)
		{
            //establishing a connection string
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";

            //building a connection using the string and opening it
            using var con = new SQLiteConnection(cs);
            con.Open();

            //making a command with the connection
            using var cmd = new SQLiteCommand(con);

            //inserting a new row into the item table
            cmd.CommandText = @"INSERT INTO Item (`Item Name`, `Item Price`, `Item Year`, `Item Cost`, `Is Purchased`, `Card Condition`, `Card Sport`, `Card Team`, `Memorabilia Description`) VALUES(@ItemName, @ItemPrice, @ItemYear, @ItemCost, @IsPurchased, @CardCondition, @CardSport, @CardTeam, @MemorabiliaDescription)";

            //adding sample data
            cmd.Parameters.AddWithValue("@ItemName", value.ItemName);
            cmd.Parameters.AddWithValue("@ItemPrice", value.ItemPrice);
            cmd.Parameters.AddWithValue("@ItemYear", value.ItemYear);
            cmd.Parameters.AddWithValue("@ItemCost", value.ItemCost);
            cmd.Parameters.AddWithValue("@IsPurchased", "false");
            cmd.Parameters.AddWithValue("@CardCondition", value.ItemCardCondition);
            cmd.Parameters.AddWithValue("@CardSport", value.ItemCardSport);
            cmd.Parameters.AddWithValue("@CardTeam", value.ItemCardTeam);
            cmd.Parameters.AddWithValue("@MemorabiliaDescription", value.ItemMemorabiliaDescription);

            //preparing the command string before touching the database
            cmd.Prepare();

            //actually executing the command to insert a new item
            cmd.ExecuteNonQuery();    
		}
	}
}
