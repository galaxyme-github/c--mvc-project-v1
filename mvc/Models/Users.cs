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
    public enum Level
    {
        Level1,
        Level2,
        AdminLevel
    }
    public class Users
    {
        [Display(Name = "User ID")]
        public int user_id { set; get; }
        [Display(Name = "User Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$")]
        [StringLength(64)]
        public string username { set; get; }
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$")]
        [StringLength(64)]
        public string firstname { set; get; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^[a-zA-Z]*$")]
        [StringLength(64)]
        public string lastname { set; get; }
        [Display(Name = "User Email")]
        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [StringLength(64)]
        public string useremail { set; get; }
        [Display(Name = "User Password")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$")]
        [StringLength(64)]
        public string password { set; get; }
        [Display(Name = "User Level")]
        [Required]
        public Level userlevel { set; get; }


        private MySqlConnection connection;
        private string server { set; get; }
        private string database { set; get; }
        private string uid { set; get; }
        private string pass { set; get; }

        //Constructor
        public Users()
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

        //open connection to database
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

        public List<string> GetByEmailAndPassword(string emailaddr, string passwordstr)
        {
            string query = "SELECT * FROM users WHERE (useremail = '" + emailaddr + "' OR username = '" + emailaddr + "') AND  password = '" + passwordstr + "'";

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
                    list.Add(dataReader["user_id"] + "");
                    list.Add(dataReader["username"] + "");
                    list.Add(dataReader["firstname"] + "");
                    list.Add(dataReader["lastname"] + "");
                    list.Add(dataReader["password"] + "");
                    list.Add(dataReader["useremail"] + "");
                    list.Add(dataReader["userlevel"] + "");
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

        //Insert statement
        public void Insert(string username, string firstname, string lastname, string password, string useremail, Level userlevel)
        {
            string query = "INSERT INTO users (username, firstname, lastname, password, useremail, userlevel) VALUES('"+username+ "', '" + firstname + "', '" + lastname + "', '" + password + "', '" + useremail + "', '" + userlevel + "')";

            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string username, string firstname, string lastname, string password, string useremail, Level userlevel, int id)
        {
            string query = "UPDATE users SET firstname='" + firstname + "', lastname='" + lastname + "', username='" + username + "', password='" + password + "', useremail='" + useremail + "', userlevel='" + userlevel + "' WHERE user_id='" + id + "'";

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

        //Delete statement
        public void Delete(int id)
        {
            string query = "DELETE FROM users WHERE user_id='" + id + "'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }


        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM users ORDER BY `user_id` ASC";

            //Create a list to store the result
            List<string>[] list = new List<string>[10000];
                        
            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                var i = 0;
                //Read the data and store them in the list
                while (dataReader.Read()) { 
                    list[i] = new List<string>();
                    list[i].Add(dataReader["user_id"] + "");
                    list[i].Add(dataReader["username"] + "");
                    list[i].Add(dataReader["firstname"] + "");
                    list[i].Add(dataReader["lastname"] + "");
                    list[i].Add(dataReader["password"] + "");
                    list[i].Add(dataReader["useremail"] + "");
                    list[i].Add(dataReader["userlevel"] + "");
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

        //Select statement
        public List<string> GetById(int id)
        {
            string query = "SELECT * FROM users WHERE user_id = " + id + " ORDER BY `user_id` ASC";

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
                    list.Add(dataReader["user_id"] + "");
                    list.Add(dataReader["username"] + "");
                    list.Add(dataReader["firstname"] + "");
                    list.Add(dataReader["lastname"] + "");
                    list.Add(dataReader["password"] + "");
                    list.Add(dataReader["useremail"] + "");
                    list.Add(dataReader["userlevel"] + "");
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

        //Count statement
        public int Count()
        {
            string query = "SELECT Count(*) FROM users";
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        //Backup
        public void Backup()
        {
            try
            {
                DateTime Time = DateTime.Now;
                int year = Time.Year;
                int month = Time.Month;
                int day = Time.Day;
                int hour = Time.Hour;
                int minute = Time.Minute;
                int second = Time.Second;
                int millisecond = Time.Millisecond;

                //Save file to C:\ with the current date as a filename
                string path;
                path = "D:\\MySqlBackup" + year + "-" + month + "-" + day +
            "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);


                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error , unable to backup!");
            }
        }

        //Restore
        public void Restore()
        {
            try
            {
                //Read file from C:\
                string path;
                path = "D:\\MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}",
                    uid, password, server, database);
                psi.UseShellExecute = false;


                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error , unable to Restore!");
            }
        }


    }
}