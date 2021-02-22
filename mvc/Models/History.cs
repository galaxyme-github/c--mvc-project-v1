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
    public class History
    {
        [Display(Name = "ID")]
        public int id { set; get; }
        [Display(Name = "GuestID")]
        public int guest_id { set; get; }
        [Display(Name = "Party Date")]
        [Required]
        [DataType(DataType.Date)]
        [StringLength(64)]
        public string partydate { set; get; }
        [Display(Name = "Event Fee")]
        [Required]
        public string eventfee { set; get; }
        [Display(Name = "CheckIn Name")]
        [Required]
        [StringLength(64)]
        public string checkin_name { set; get; }
        [Display(Name = "Party Time")]
        [Required]
        [DataType(DataType.Time)]
        [StringLength(64)]
        public string partytime { set; get; }
        [Display(Name = "Guest Agreement")]
        [Required]
        [StringLength(255)] 
        public string agreement { set; get; }
        [Display(Name = "Rejected Reason")]
        [Required]
        public string reason { set; get; }


        private MySqlConnection connection;
        private string server { set; get; }
        private string database { set; get; }
        private string uid { set; get; }
        private string pass { set; get; }

        public History()
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
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + pass + "; convert zero datetime=True";

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

        public void Insert(string guest_id, string checkin_name, string partydate, string party_time, string event_fee, string GuestAgreement, int reject, string reason)
        {
            string query = "INSERT INTO partyhistory (`guest_id`,`checkin_name`,`partydate`,`party_time`,`event_fee`,`GuestAgreement`,`TermsRejected`,`reason`) VALUES('" + guest_id + "', '" + checkin_name + "', '" + partydate + "', '" + party_time + "', '" + event_fee + "', '" + GuestAgreement + "', '" + reject + "', '" + reason + "')";
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

        //Select statement
        public List<string>[] Select()
        {
            string query = "SELECT * FROM partyhistory ORDER BY `partydate`, `guest_id` ASC";

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
                while (dataReader.Read())
                {
                    list[i] = new List<string>();
                    list[i].Add(dataReader["guest_id"] + "");
                    list[i].Add(dataReader["partydate"] + "");
                    list[i].Add(dataReader["party_time"] + "");
                    list[i].Add(dataReader["event_fee"] + "");
                    list[i].Add(dataReader["GuestAgreement"] + "");
                    list[i].Add(dataReader["checkin_name"] + "");
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
        public List<string>[] GetByGuestId(int id)
        {
            string query = "SELECT partyhistory.*, guests.firstname, guests.lastname FROM partyhistory LEFT JOIN guests ON guests.guest_id = partyhistory.guest_id AND guests.`guest_id` = '"+id+"'";

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
                while (dataReader.Read())
                {
                    list[i] = new List<string>();
                    list[i].Add(dataReader["guest_id"] + "");
                    list[i].Add(dataReader["partydate"] + "");
                    list[i].Add(dataReader["party_time"] + "");
                    list[i].Add(dataReader["event_fee"] + "");
                    list[i].Add(dataReader["GuestAgreement"] + "");
                    list[i].Add(dataReader["checkin_name"] + "");
                    list[i].Add(dataReader["TermsRejected"] + "");
                    list[i].Add(dataReader["reason"] + "");
                    list[i].Add(dataReader["firstname"] + "");
                    list[i].Add(dataReader["lastname"] + "");
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
    }
}