using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Csharp_Hotel_Management
{
    /*
     * 
     *Create a new class RESERVATION to add a reservation
     *                               to edit a reservation
     *                               to remove a reservation
     *                               to get all reservations
     *We also need to create a table reservations                       
     */
    internal class RESERVATION
    {
        CONNECT conn = new CONNECT();
        //create a function to get all reservations
        public DataTable getAllReserv()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `reservations`", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //create a function to add a reservation
        public bool insertReserv(int roomNumber, int clientId, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `reservations`(`roomNumber`, `clientId`, `dateIn`, `dateOut`) VALUES (@rno,@cid,@din,@dout)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();

            //@rno,@cid,@din,@dout
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = roomNumber;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientId;
            command.Parameters.Add("@din", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.openConnection();
                return false;
            }

        }

        //create a function to edit the selected reservation
        public bool editReserv(int reservId, int roomNumber, int clientId, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `reservations` SET `roomNumber`=@rno, `clientId`=@cid, `dateIn`=@din, `dateOut`=@dout WHERE `reservId`=@rid";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            //@rid,@rno,@cid,@din,@dout
            command.Parameters.Add("@rid", MySqlDbType.Int32).Value = reservId;
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = roomNumber;
            command.Parameters.Add("@cid", MySqlDbType.Int32).Value = clientId;
            command.Parameters.Add("@din", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dout", MySqlDbType.Date).Value = dateOut;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return false;
            }
            else
            {
                conn.openConnection();
                return true;
            }

        }

        //create a function to delete the selected reservation
        //we only need the reservId

        public bool removeReserv(int reservId)
        {
            MySqlCommand command = new MySqlCommand();
            String removeQuery = "DELETE FROM `reservations` WHERE `reservId`=@rid";
            command.CommandText = removeQuery;
            command.Connection = conn.getConnection();

            //@rid
            command.Parameters.Add("@rid", MySqlDbType.Int32).Value = reservId;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.openConnection();
                return false;
            }
        }
    }
}
