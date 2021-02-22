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
    public enum Colour
    {
        Blue,
        Brown,
        Gray,
        Green,
        Hazel,
        Red
    }

    public enum AgeCheck
    {
        GOOD,
        VIP,
        BAD
    }

    public enum Sex
    {
        Male,
        Female
    }

    public enum Rule
    {
        AA,
        LA
    }
    public class Guests
    {
        [Display(Name = "GuestID")]
        [RegularExpression(@"^(0|[1-9]\d*)$")]
        public int guest_id { set; get; }
        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^([A-Za-z]\w*)$")]
        [StringLength(64)]
        public string firstname { set; get; }
        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^([A-Za-z]\w*)$")]
        [StringLength(64)]
        public string lastname { set; get; }
        [Display(Name = "Birthdate")]
        [Required]
        [DataType(DataType.Date)]
        public string birthdate { set; get; }
        [Display(Name = "Middle Name")]
        [Required]
        [RegularExpression(@"^([A-Za-z]\w*)$")]
        [StringLength(64)]
        public string middlename { set; get; }
        [Display(Name = "Street Address")]
        [RegularExpression(@"^([A-Za-z]\w*)$")]
        [StringLength(64)]
        public string streetaddress { set; get; }
        [Display(Name = "City")]
        [Required]
        [RegularExpression(@"^([A-Za-z]\w*)$")]
        public string city { set; get; }
        [Display(Name = "State")]
        [RegularExpression(@"^([A-Za-z]\w*)$")]
        [StringLength(64)]
        public string state { set; get; }
        [Display(Name = "Driver License Number")]
        [StringLength(64, MinimumLength = 4, ErrorMessage = "Maximum 4 characters")]
        [Required]
        public string driverlicensenumber { set; get; }
        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        [StringLength(64)]
        public string expirationdate { set; get; }
        [Display(Name = "Sex")]
        [Required]
        public Sex sex { set; get; }
        [Display(Name = "Height")]
        [Required]
        [RegularExpression(@"^([0-9]\d*)$")]
        public float height { set; get; }
        [Display(Name = "Weight")]
        [Required]
        [RegularExpression(@"^([0-9]\d*)$")]
        public float weight { set; get; }
        [Display(Name = "Eyes")]
        [Required]
        public Colour eyes { set; get; }
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^([0-9-]\d*)$")]
        [StringLength(20)]
        public string phonenumber { set; get; }
        [Display(Name = "Email Address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [StringLength(64)]
        public string emailaddress { set; get; }
        [Display(Name = "Guest ID")]
        [StringLength(10)]
        public string guestID { set; get; }



        private MySqlConnection connection;
        private string server { set; get; }
        private string database { set; get; }
        private string uid { set; get; }
        private string pass { set; get; }

        public Guests()
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

        //Insert statement
        public void Insert(string firstname, string lastname, string birthdate, string middlename, string streetaddress, string city, string state, string driverlicensenumber, string expirationdate, Sex sex, string height, string weight, Colour eyes, string phonenumber, string emailaddress, AgeCheck standing, Rule rule)
        {
            string guestid = firstname.Substring(0, 1) + lastname.Substring(0, 1) + driverlicensenumber.Substring(driverlicensenumber.Length - 4, 4);
            string query = "INSERT INTO guests (`firstname`,`lastname`,`birthdate`,`middlename`,`streetaddress`,`city`, `state`, `driverlicensenumber`,`expirationdate`,`sex`,`height`,`weight`,`eyes`,`phonenumber`,`emailaddress`, `guestID`, `LoyaltyPoints`, `gueststanding`, `access_rule`) VALUES('" + firstname + "', '" + lastname + "', '" + birthdate + "', '" + middlename + "', '" + streetaddress + "', '" + city + "', '" + state + "', '" + driverlicensenumber + "', '" + expirationdate + "', '" + sex + "', '" + height + "', '" + weight + "', '" + eyes + "', '" + phonenumber + "', '" + emailaddress + "', '"+ guestid + "', 1, '" + standing + "', '" + rule + "')";
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
        public void Update(string firstname, string lastname, string birthdate, string middlename, string streetaddress, string city, string state, string driverlicensenumber, string expirationdate, Sex sex, string height, string weight, Colour eyes, string phonenumber, string emailaddress, string guestid, int id)
        {
            string query = "UPDATE guests SET firstname='" + firstname + "', lastname='" + lastname + "', birthdate='" + birthdate + "', middlename='" + middlename + "', streetaddress='" + streetaddress + "', city='" + city + "', state='" + state + "', driverlicensenumber='" + driverlicensenumber + "', expirationdate='" + expirationdate + "', height='" + height + "', weight='" + weight + "', eyes='" + eyes + "', emailaddress='" + emailaddress + "', phonenumber='" + phonenumber + "', guestID='" + guestid + "' WHERE guest_id='" + id + "'";

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
            string query = "DELETE FROM guests WHERE guest_id='" + id + "'";

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
            string query = "SELECT * FROM guests ORDER BY `guest_id` ASC";

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
                    list[i].Add(dataReader["firstname"] + "");
                    list[i].Add(dataReader["lastname"] + "");
                    list[i].Add(dataReader["birthdate"] + "");
                    list[i].Add(dataReader["middlename"] + "");
                    list[i].Add(dataReader["streetaddress"] + "");
                    list[i].Add(dataReader["city"] + "");
                    list[i].Add(dataReader["state"] + "");
                    list[i].Add(dataReader["driverlicensenumber"] + "");
                    list[i].Add(dataReader["expirationdate"] + "");
                    list[i].Add(dataReader["sex"] + "");
                    list[i].Add(dataReader["height"] + "");
                    list[i].Add(dataReader["weight"] + "");
                    list[i].Add(dataReader["eyes"] + "");
                    list[i].Add(dataReader["phonenumber"] + "");
                    list[i].Add(dataReader["emailaddress"] + "");
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
            string query = "SELECT * FROM guests WHERE guest_id = " + id + " ORDER BY `guest_id` ASC";

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
                    list.Add(dataReader["guest_id"] + "");
                    list.Add(dataReader["firstname"] + "");
                    list.Add(dataReader["lastname"] + "");
                    list.Add(dataReader["birthdate"] + "");
                    list.Add(dataReader["middlename"] + "");
                    list.Add(dataReader["streetaddress"] + "");
                    list.Add(dataReader["city"] + "");
                    list.Add(dataReader["state"] + "");
                    list.Add(dataReader["driverlicensenumber"] + "");
                    list.Add(dataReader["expirationdate"] + "");
                    list.Add(dataReader["sex"] + "");
                    list.Add(dataReader["height"] + "");
                    list.Add(dataReader["weight"] + "");
                    list.Add(dataReader["eyes"] + "");
                    list.Add(dataReader["phonenumber"] + "");
                    list.Add(dataReader["emailaddress"] + "");
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

        public bool GetByGuestId(string guestid)
        {
            string query = "SELECT * FROM guests WHERE guestID = '" + guestid + "' ORDER BY `guest_id` ASC";

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
                    i++;
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                if(i >= 1)
                {
                    return true;
                }
                else{
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public int GetIdByGuestId(string guestid)
        {
            string query = "SELECT guest_id FROM guests WHERE guestID = '" + guestid + "' ORDER BY `guest_id` ASC";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                var id = 0;
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    id = Int32.Parse(dataReader["guest_id"] + "");                    
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                return id;

            }
            else
            {
                return 0;
            }
        }

        public void LoyaltyPointsIncrease(string guestid)
        {
            string query = "";
            if (DateTime.Now.ToString("ddd") == "Sun" && DateTime.Now.ToString("H") == "6")
            {
                query = "UPDATE guests SET `LoyaltyPoints`=0 WHERE guestID='" + guestid + "'";
            }
            else {
                query = "UPDATE guests SET `LoyaltyPoints`=`LoyaltyPoints`+1 WHERE guestID='" + guestid + "'";
            }

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                return;

            }
            else
            {
                return;
            }
        }

        public List<string> getlast_id()
        {
            List<string> list = new List<string>();
            string query = "SELECT MAX(guest_id) as lastid FROM guests";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create Command
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list.Add(dataReader["lastid"] + "");
                    break;
                }
                this.CloseConnection();
            }
            return list;
        }

        public AgeCheck GuestStatusCheck(string guestID)
        {
            string query = "SELECT gueststanding FROM guests WHERE guestID = " + guestID;
            AgeCheck value = 0;
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create Command
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();
                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    value = (AgeCheck)Enum.Parse(typeof(AgeCheck), (dataReader["gueststanding"] + ""), true);
                    break;
                }
                this.CloseConnection();
            }
            return value;
        }


        public List<string>[] getByRule(string rule)
        {
            string query = "SELECT * FROM guests WHERE access_rule = '"+rule+"' ORDER BY `guest_id` ASC";

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
                    list[i].Add(dataReader["firstname"] + "");
                    list[i].Add(dataReader["lastname"] + "");
                    list[i].Add(dataReader["birthdate"] + "");
                    list[i].Add(dataReader["middlename"] + "");
                    list[i].Add(dataReader["streetaddress"] + "");
                    list[i].Add(dataReader["city"] + "");
                    list[i].Add(dataReader["state"] + "");
                    list[i].Add(dataReader["driverlicensenumber"] + "");
                    list[i].Add(dataReader["expirationdate"] + "");
                    list[i].Add(dataReader["sex"] + "");
                    list[i].Add(dataReader["height"] + "");
                    list[i].Add(dataReader["weight"] + "");
                    list[i].Add(dataReader["eyes"] + "");
                    list[i].Add(dataReader["phonenumber"] + "");
                    list[i].Add(dataReader["emailaddress"] + "");
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

        public List<string>[] TransactionFromAAccess(int year, int month, int day)
        {
            string query = "SELECT G.firstname, G.lastname, P.event_fee, P.partydate, P.party_time, G.guest_id FROM partyhistory AS P LEFT JOIN guests AS G ON (P.guest_id = G.guest_id) WHERE G.access_rule = 'AA' AND P.partydate = '" + year + "-" + month + "-" + day + "' ORDER BY P.partydate ASC";

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
                    list[i].Add(dataReader["firstname"] + "");
                    list[i].Add(dataReader["lastname"] + "");
                    list[i].Add(dataReader["event_fee"] + "");
                    list[i].Add(dataReader["partydate"] + "");
                    list[i].Add(dataReader["party_time"] + "");
                    list[i].Add(dataReader["guest_id"] + "");
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

        public List<string>[] TransactionFromLAccess(int year, int month, int day)
        {
            string query = "SELECT G.firstname, G.lastname, P.event_fee, P.partydate, P.party_time, G.guest_id FROM partyhistory AS P LEFT JOIN guests AS G ON (P.guest_id = G.guest_id) WHERE G.access_rule = 'LA' AND P.partydate = '" + year + "-" + month + "-" + day + "' ORDER BY P.partydate ASC";

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
                    list[i].Add(dataReader["firstname"] + "");
                    list[i].Add(dataReader["lastname"] + "");
                    list[i].Add(dataReader["event_fee"] + "");
                    list[i].Add(dataReader["partydate"] + "");
                    list[i].Add(dataReader["party_time"] + "");
                    list[i].Add(dataReader["guest_id"] + "");
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