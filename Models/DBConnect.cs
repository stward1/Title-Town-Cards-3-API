using System;
using MySql.Data.MySqlClient;
namespace API.Models
{
    //this class is for managing our connections to the database
    public class DBConnect
    {
        //these are the pieces of data we'll need to connect to the database
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;

        public DBConnect()
        {
            //we'll just send the work to a better-named method
            Initialize();
        }

        //this method is going to create the database connection for us
        private void Initialize()
        {
            //this is from the server email, don't mess with this stuff
            this.server = "sql9.freemysqlhosting.net";
            this.database = "sql9379243";
            this.user = "sql9379243";
            this.password = "SSIzb9EgZ5";
            this.port = "3306";

            //this is the connection string based on the variables; we make the connection immediately after this
            string cs = $"server={server};user={user};database={database};port={port};password={password};";
            connection = new MySqlConnection(cs);
        }

        //this method is the actual connection opener
        public bool OpenConnection()
        {
            try
            {
                //attempts to open the connection and return true to indicate success
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                //if that fails, we log the message
                if (e.Number == 0)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Cannot connect");
                }
                else
                {
                    //this is the specific invalid username/pw code
                    if (e.Number == 1045)
                    {
                        Console.WriteLine("Invalid username/password");
                    }
                }
            }
            
            //return false after failing to indicate that the connection failed
            return false;
        }

        //this is the method to close the connection to the database; the logic is the exact same as above just for closing
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
            }
            
            return false;
        }

        public MySqlConnection GetCon()
        {
            return connection;
        }
    }
}