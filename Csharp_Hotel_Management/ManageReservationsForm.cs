using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Csharp_Hotel_Management
{
    public partial class ManageReservationsForm : Form
    {
        public ManageReservationsForm()
        {
            InitializeComponent();
        }
        ROOM room = new ROOM();
        RESERVATION reserv = new RESERVATION();

        private void ManageReservationsForm_Load(object sender, EventArgs e)
        {
            //display room type
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";

            
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxClientID.Text = "";
            comboBoxRoomNumber.SelectedIndex = 0;
            dateTimePickerIN.Value = DateTime.Now;
            dateTimePickerOUT.Value = DateTime.Now;
        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                //display room number depending on the selected type
                int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                comboBoxRoomNumber.DataSource = room.roomByType(type);
                comboBoxRoomNumber.DisplayMember = "number";
                comboBoxRoomNumber.ValueMember = "number";
            }
            catch(Exception ex)
            {
                //do nothing
            }
        }

        private void buttonAddReserv_Click(object sender, EventArgs e)
        {
            int clientId = Convert.ToInt32(textBoxClientID.Text);
            int roomType = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            int roomNumber = Convert.ToInt32(comboBoxRoomNumber.SelectedValue);
            DateTime dateIn = dateTimePickerIN.Value;
            DateTime dateOut = dateTimePickerOUT.Value;

            if (clientId == 0 || roomType == 0)
            {
                MessageBox.Show("Required Fields - Room Number, Room Type and Client ID", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertRreserv = reserv.insertReserv(roomNumber,clientId,dateIn,dateOut);
                if (insertRreserv = true)
                {
                    dataGridView1.DataSource = reserv.getAllReserv();
                    MessageBox.Show("Reservation Added Successfully", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Reservation Not Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
//add foreign key for the client and the room

//ALTER TABLE rooms add CONSTRAINT fk_type_id FOREIGN KEY (roomType) REFERENCES rooms_category(category_id) on UPDATE CASCADE on DELETE CASCADE;
//ALTER TABLE reservations add CONSTRAINT fk_room_number FOREIGN KEY (roomNumber) REFERENCES rooms(roomNumber) on UPDATE CASCADE on DELETE CASCADE;
//ALTER TABLE reservations add CONSTRAINT fk_client_id FOREIGN KEY (clientId) REFERENCES clients(id) on UPDATE CASCADE on DELETE CASCADE;
