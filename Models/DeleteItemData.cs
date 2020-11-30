using API.Models.Interfaces;
using System.IO;
using System.Data.SQLite;

namespace API.Models
{
    public class DeleteItemData : IDeleteItem
    {
        public void DeleteItem(int id)
        {
            string cs = $"URI=file:{Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "Database\\TitleTownCardsDatabase.db")}";
            using var con = new SQLiteConnection(cs);
            con.Open();

            //defining the string
            string stm = @"DELETE FROM Item WHERE `Item ID` = @id";

            //making new command
            using var cmd = new SQLiteCommand(stm, con);

            //preparing the command and executing it; this deletes the record with the right id
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}