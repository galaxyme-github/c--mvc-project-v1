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
    public class AAccess
    {
        [Display(Name = "ID")]
        public int id { set; get; }
        [Display(Name = "Monday")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Monday { set; get; }
        [Display(Name = "Tuesday")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Tuesday { set; get; }
        [Display(Name = "Wednesday")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Wednesday { set; get; }
        [Display(Name = "Thursday")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Thursday { set; get; }
        [Display(Name = "Friday")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Friday { set; get; }
        [Display(Name = "Saturday")]
        [Required]
        [RegularExpression(@"^[0-9]*(?:\.[0-9]*)?$")]
        public float Saturday { set; get; }

        private MySqlConnection connection;
        private string server { set; get; }
        private string database { set; get; }
        private string uid { set; get; }
        private string pass { set; get; }

        public AAccess()
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
        public List<string> GetById(int id)
        {
            string query = "SELECT * FROM all_acceess_fee WHERE id = 1";

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
                    list.Add(dataReader["id"] + "");
                    list.Add(dataReader["Monday"] + "");
                    list.Add(dataReader["Tuesday"] + "");
                    list.Add(dataReader["Wednesday"] + "");
                    list.Add(dataReader["Thursday"] + "");
                    list.Add(dataReader["Friday"] + "");
                    list.Add(dataReader["Saturday"] + "");
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
        public void Update(string monday, string tuesday, string wednesday, string thursday, string friday, string saturday, int id)
        {
            string query = "UPDATE all_acceess_fee SET Monday='" + monday + "', Tuesday='" + tuesday + "', Wednesday='" + wednesday + "', Thursday='" + thursday + "', Friday='" + friday + "', Saturday='" + saturday + "' WHERE id='" + id + "'";

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
        public float getTodayEventFee()
        {
            float fee = 0;

            if (this.OpenConnection() == true)
            {
                string query = "SELECT `" + DateTime.Now.ToString("dddd") + "` as week FROM limit_acceess_fee";
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                var i = 0;
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    fee = float.Parse(dataReader["week"] + "");
                    i++;
                }

                /*if (Int32.Parse(DateTime.Now.ToString("HH")) < 9)
                {
                    query = "SELECT `early_fee` FROM early_fee WHERE id = 1";
                    cmd = new MySqlCommand(query, connection);
                    dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        fee -= float.Parse(dataReader["early_fee"] + "");
                    }
                }*/

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return fee;
            }
            else
            {
                return 0;
            }
        }


    }
}