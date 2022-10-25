using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Csharp_Hotel_Management
{
    /*
     *this class will make the connection between our app and mysql database
     *first you need to download the mysql connector and add it to your project
     *download link -> 
     */

    internal class CONNECT
    {
        private MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=csharp_hotel_db");

        //create a function to return our connection
        public MySqlConnection getConnection()
        {
            return connection;
        }

        //create function to open the connection
        public void openConnection()
        {
            if(connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        //create function to close the connection
        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}
