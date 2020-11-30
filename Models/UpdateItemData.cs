using API;
using API.Models.Interfaces;
using System.Data.SQLite;
using System.IO;

namespace API.Models
{
    public class UpdateItemData : IUpdateItem
    {
        public void UpdateItem(Item updatedItem)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            //defining the string
            string stm = @"UPDATE Item SET `Item Name` = @ItemName, `Item Price` = @ItemPrice, `Item Year` = @ItemYear, `Card Condition` = @CardCondition, `Card Sport` = @CardSport, `Card Team` = @CardTeam, `Memorabilia Description` = @MemorabiliaDescription WHERE `Item ID` = @ItemID";

            //making new command
            using var cmd = new SQLiteCommand(stm, con);

            //preparing the command and executing it
            cmd.Parameters.AddWithValue("@ItemID", updatedItem.ItemID);
            cmd.Parameters.AddWithValue("@ItemName", updatedItem.ItemName);
            cmd.Parameters.AddWithValue("@ItemPrice", updatedItem.ItemPrice);
            cmd.Parameters.AddWithValue("@ItemYear", updatedItem.ItemYear);
            cmd.Parameters.AddWithValue("@CardCondition", updatedItem.ItemCardCondition);
            cmd.Parameters.AddWithValue("@CardSport", updatedItem.ItemCardSport);
            cmd.Parameters.AddWithValue("@CardTeam", updatedItem.ItemCardTeam);
            cmd.Parameters.AddWithValue("@MemorabiliaDescription", updatedItem.ItemMemorabiliaDescription);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}