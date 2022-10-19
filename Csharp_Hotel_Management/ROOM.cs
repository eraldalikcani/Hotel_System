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
     *Create a new class ROOM to add a new room
     *                        to edit a room
     *                        to remove a room
     *                        to get all rooms
     *We also need to create a table room_type & rooms                        
     */
    internal class ROOM
    {
        CONNECT conn = new CONNECT();
        //create a function to get a list of room's type
        public DataTable roomTypeList()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `rooms_category`", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        //create a function to add a new room

        public bool insertRoom(int roomNumber, int roomType, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "INSERT INTO `rooms`(`roomNumber`, `roomType`, `phone`, `free`) VALUES (@rno,@rty,@pho,@fre)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();

            //@rno,@rty,@pho,@fre
            command.Parameters.Add("@rno", MySqlDbType.Int32).Value = roomNumber;
            command.Parameters.Add("@rty", MySqlDbType.Int32).Value = roomType;
            command.Parameters.Add("@pho", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@fre", MySqlDbType.VarChar).Value = free;

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

        //create a function to get a list of all rooms
        public DataTable getRooms()
        {
            MySqlCommand command = new MySqlCommand("SELECT * FROM `rooms`", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;

        }

        //create a function to edit the selected room
        public bool editRoom(int roomNumber, int roomType, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "UPDATE `rooms` SET `roomType`=@rty, `phone`=@pho, `free`=@fre WHERE `roomNumber`=@rno";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            //@rno,@rty,@pho,@fre
            command.Parameters.Add("@rty", MySqlDbType.VarChar).Value = roomType;
            command.Parameters.Add("@pho", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@fre", MySqlDbType.VarChar).Value = free;

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
    }
}
