//JAY AMISHKUMAR PATEL - 1001357017
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//to do the databse operation with local mysql database
namespace Mul_Con_server
{
    static class db
    {
        /// <summary>
        ///define variable for connection string 
        /// </summary>
        private const string Server = "127.0.0.1";
        private const string DB = "LAB1";
        private const string UID = "root";
        private const string PASS = "";

        /// <summary>
        /// declare the mysqlconnection variables
        /// </summary>
        private static MySqlConnection dbconInsert, dbconGet, dbconDel, dbconDelAll;

        /// <summary>
        /// initilization of databse by preparing connection string
        /// </summary>
        public static void InitializeDB()
        {
            //prepare connection string
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = Server;
            builder.UserID = UID;
            builder.Password = PASS;
            builder.Database = DB;
            string connectionString = builder.ToString();

            //initialize the declated connections
            dbconInsert = new MySqlConnection(connectionString);
            dbconGet = new MySqlConnection(connectionString);
            dbconDel = new MySqlConnection(connectionString);
            dbconDelAll = new MySqlConnection(connectionString);

            //call this method to delete the users when applications runs for first time
            AllUserDelete();
        }

        /// <summary>
        /// insert the username and online status in database
        /// </summary>
        /// <param name="data">username</param>
        /// <param name="sts">if client wants to show its online status or not (0/1)</param>
        public static void InsertData(string data, string sts)
        {
            dbconInsert.Open();
            string query = String.Format("INSERT INTO users(UName,Status) VALUES('{0}','{1}')", data, sts);
            MySqlCommand cmd = new MySqlCommand(query, dbconInsert);
            cmd.ExecuteNonQuery();
            dbconInsert.Close();
        }

        /// <summary>
        /// retrive the online clients apart from the current client which is calling the method
        /// 
        /// </summary>
        /// <param name="name">client name who is requesting this call  </param>
        /// <returns></returns>
        public static string GetOnlineUsers(string name)
        {
            dbconGet.Open();
            string lst = "A";
            string query = String.Format("SELECT UName FROM users where Status='1' AND UName!='{0}'", name);
            MySqlCommand cmd = new MySqlCommand(query, dbconGet);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string s = reader["UName"].ToString();
                lst += s + ",";
            }
            if(lst.Length>1)
                lst = lst.Remove(lst.Length - 1);
            Console.WriteLine("db: "+lst);
            dbconGet.Close();
            
            //returns the list of clients in listbox to the requesting clients
            return lst;
        }

        /// <summary>
        /// this method is called when the client disconnets. 
        /// It helps to delete the record of that client from the database
        /// </summary>
        public static void UserDelete(string name)
        {
            dbconDel.Open();
            string query = String.Format("DELETE FROM users WHERE UName='{0}'", name);
            MySqlCommand cmd = new MySqlCommand(query, dbconDel);
            cmd.ExecuteNonQuery();
            dbconDel.Close();
        }

        /// <summary>
        /// to delete the all users at first start of the application
        /// </summary>
        public static void AllUserDelete()
        {
            dbconDelAll.Open();
            string query = String.Format("DELETE FROM users");
            MySqlCommand cmd = new MySqlCommand(query, dbconDelAll);
            cmd.ExecuteNonQuery();
            dbconDelAll.Close();
        }

    }
}
