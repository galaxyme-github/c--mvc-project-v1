using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.IO;

namespace mvc.Models
{
    public class EarlyFee
    {
        [Display(Name = "ID")]
        public int id { set; get; }
        [Display(Name = "Early Fee")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float early_fee { set; get; }

        private MySqlConnection connection;
        private string server { set; get; }
        private string database { set; get; }
        private string uid { set; get; }
        private string pass { set; get; }

        public EarlyFee()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "db_event_place";
            uid = "root";
            pass = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        //Select statement
        public List<string> GetById()
        {
            string query = "SELECT * FROM early_fee WHERE id = 1";

            //Create a list to store the result
            List<string> list = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                var i = 0;
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list = new List<string>();
                    list.Add(dataReader["id"].ToString());
                    list.Add(dataReader["early_fee"].ToString());
                    i++;
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
        }

        //Update statement
        public void Update(float early_fee, int id)
        {
            string query = "UPDATE early_fee SET early_fee='" + early_fee + "' WHERE id='" + id + "'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

    }
}