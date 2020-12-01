using API;
using API.Models.Interfaces;
using MySql.Data.MySqlClient;
using System.IO;

namespace API.Models
{
    public class UpdateItemData : IUpdateItem
    {
        public void UpdateItem(Item updatedItem)
        {
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

                //defining the string
                string stm = @"UPDATE Item SET `Item Name` = @ItemName, `Item Price` = @ItemPrice, `Item Year` = @ItemYear, `Card Condition` = @CardCondition, `Card Sport` = @CardSport, `Card Team` = @CardTeam, `Memorabilia Description` = @MemorabiliaDescription WHERE `Item ID` = @ItemID";

                //making new command
                MySqlCommand cmd = new MySqlCommand(stm, con);

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

                db.CloseConnection();
            }
            //if the open fails, the api should just do nothing; hopefully this helps prevent some crashes
        }
    }
}