using API.Models.Interfaces;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace API.Models
{
    public class DeleteItemData : IDeleteItem
    {
        public void DeleteItem(int id)
        {
            //connecting to and opening the database
            DBConnect db = new DBConnect();
            bool isOpen = db.OpenConnection();

            if (isOpen)
            {
                //if the open succeeded, we proceed with the sql commands
                MySqlConnection con = db.GetCon();

                //defining the string
                string stm = @"DELETE FROM Item WHERE `Item ID` = @id";

                //making new command
                MySqlCommand cmd = new MySqlCommand(stm, con);

                //preparing the command and executing it; this deletes the record with the right id
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                db.CloseConnection();
            }
        }
    }
}