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
            
            dataGridView1.DataSource = reserv.getAllReserv();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxReservID.Text = "";
            textBoxClientID.Text = "";
            comboBoxRoomNumber.SelectedIndex = 0;
            comboBoxRoomType.SelectedIndex = 0;
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
                comboBoxRoomNumber.DisplayMember = "roomNumber";
                comboBoxRoomNumber.ValueMember = "roomNumber";
            }
            catch (Exception ex)
            {
                //do nothing
            }

        }

        private void buttonAddReserv_Click(object sender, EventArgs e)
        {
            try
            {
                
                int clientId = Convert.ToInt32(textBoxClientID.Text);
                int roomType = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
                int roomNumber = Convert.ToInt32(comboBoxRoomNumber.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                if ( clientId == 0 || roomNumber == 0 )
                {
                    MessageBox.Show("Required Fields - Room Number, Room Type and Client ID", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //date in must be =>today date
                    //date out must be =>date in
                    if( DateTime.Compare(dateIn.Date,DateTime.Now.Date) < 0 )
                    {
                        MessageBox.Show("The Date In must be = or > To Today Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }else if ( DateTime.Compare(dateOut.Date, dateIn.Date) < 0)
                    {
                        MessageBox.Show("The Date Out must be = or > Date In", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Boolean insertRreserv = reserv.insertReserv(roomNumber, clientId, dateIn, dateOut);
                        if (insertRreserv = true)
                        {
                            dataGridView1.DataSource = reserv.getAllReserv();
                            //set the room column to no
                            room.setRoomFree(roomNumber, "No");
                            
                            MessageBox.Show("Reservation Added Successfully", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Reservation Not Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Add Reservation Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        private void buttonEditReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId = Convert.ToInt32(textBoxReservID.Text);
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
                    //date in must be =>today date
                    //date out must be =>date in
                    if (dateIn < DateTime.Now)
                    {
                        MessageBox.Show("The Date In must be = or > To Today Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (dateOut < dateIn)
                    {
                        MessageBox.Show("The Date Out must be = or > Date In", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Boolean editReserv = reserv.editReserv(reservId, roomNumber, clientId, dateIn, dateOut);
                        if (editReserv = true)
                        {
                            dataGridView1.DataSource = reserv.getAllReserv();
                            //set the room column to no
                            room.setRoomFree(roomNumber, "No");

                            MessageBox.Show("Reservation Added Successfully", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Reservation Not Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId = Convert.ToInt32(textBoxReservID.Text);
                int roomNumber = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                bool removeReserv = reserv.removeReserv(reservId);

                if (removeReserv = true)
                {

                    dataGridView1.DataSource = reserv.getAllReserv();
                    MessageBox.Show("Reservation Deleted Successfully", "Delete Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //after deletion of reservation we need to set the free column to "Yes"
                    room.setRoomFree(roomNumber, "Yes");

                    //clear all textboxes after the deletion
                    //by calling the clear button
                    buttonClear.PerformClick();
                    
                }
                else
                {
                    MessageBox.Show("ERROR - Reservation Not Deleted", "Delete Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reservation ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxReservID.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxClientID.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            //we need to select the combo for rooms type
            //first we need to know the type of the room

            //get the room id
            int roomId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value);

            //select the room type from the combobox
            comboBoxRoomType.SelectedValue = room.getRoomType(roomId);

            //select the room number from the combobox
            //if you need to set a room to a reservation
            //you need to set the free room column to "Yes"
            comboBoxRoomNumber.SelectedValue = roomId;
            

            dateTimePickerIN.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value.ToString());
            dateTimePickerOUT.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[4].Value.ToString());



        }
    }
}
//add foreign key for the client and the room

//ALTER TABLE rooms add CONSTRAINT fk_type_id FOREIGN KEY (roomType) REFERENCES rooms_category(category_id) on UPDATE CASCADE on DELETE CASCADE;
//ALTER TABLE reservations add CONSTRAINT fk_room_number FOREIGN KEY (roomNumber) REFERENCES rooms(roomNumber) on UPDATE CASCADE on DELETE CASCADE;
//ALTER TABLE reservations add CONSTRAINT fk_client_id FOREIGN KEY (clientId) REFERENCES clients(id) on UPDATE CASCADE on DELETE CASCADE;

//when you delete a room or client, all reservations associated with it will be deleted