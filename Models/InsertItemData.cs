using API.Models.Interfaces;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.IO;

namespace API.Models
{
      public class InsertItemData : IInsertItem
	{
            public void AddItem(Item value)
		{
                  //connecting to and opening the database
                  DBConnect db = new DBConnect();
                  bool isOpen = db.OpenConnection();

                  if (isOpen)
                  {
                        //if the open succeeded, we proceed with the sql commands
                        MySqlConnection con = db.GetCon();

                        string stm = @"INSERT INTO Item (`Item Name`, `Item Price`, `Item Year`, `Item Cost`, `Is Purchased`, `Card Condition`, `Card Sport`, `Card Team`, `Memorabilia Description`) VALUES(@ItemName, @ItemPrice, @ItemYear, @ItemCost, @IsPurchased, @CardCondition, @CardSport, @CardTeam, @MemorabiliaDescription)";
                        //making a command with the connection
                        MySqlCommand cmd = new MySqlCommand(stm, con);

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

                        db.CloseConnection();
                  }
		}
	}
}
