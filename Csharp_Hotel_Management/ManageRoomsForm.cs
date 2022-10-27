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
    public partial class ManageRoomsForm : Form
    {
        public ManageRoomsForm()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        ROOM room = new ROOM();
        private void ManageRoomsForm_Load(object sender, EventArgs e)
        {
            comboBoxRoomType.DataSource = room.roomTypeList();
            comboBoxRoomType.DisplayMember = "label";
            comboBoxRoomType.ValueMember = "category_id";
            radioButtonYES.Checked = true;

            dataGridView1.DataSource = room.getRooms();
        }

        private void buttonAddRoom_Click(object sender, EventArgs e)
        {
            int number = Convert.ToInt32(textBoxNumber.Text);
            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            string phone = textBoxPhone.Text;

            if (number == 0 || type == 0 || phone.Trim().Equals(""))
            {
                MessageBox.Show("Required Fields - Room Number, Room Type and Phone", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                Boolean insertRoom = room.insertRoom(number, type, phone, "Yes");
                if (insertRoom = true)
                {
                    dataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("Room Added Successfully", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonEditRoom_Click(object sender, EventArgs e)
        {
            int roomType = Convert.ToInt32(comboBoxRoomType.SelectedIndex.ToString());
            String phone = textBoxPhone.Text;
            String free = "";

            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);
                if (radioButtonYES.Checked)
                {
                    free = "Yes";
                }
                else
                {
                    free = "No";
                }

                if (number == 0 || roomType == 0)
                {
                    MessageBox.Show("Required Fields - Room Number and Room Type", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {

                    Boolean editRoom = room.editRoom(number, roomType, phone, free);

                    if (editRoom = true)
                    {
                        dataGridView1.DataSource = room.getRooms();
                        MessageBox.Show("Room Updated Successfully", "Edit Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Room Not Updated", "Edit Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonRemoveRoom_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);
                bool removeRoom = room.removeRoom(number);

                if (removeRoom = true)
                {

                    dataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("Room Deleted Successfully", "Delete Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //clear all textboxes after the deletion
                    //by calling the clear button
                    buttonClear.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Room Not Deleted", "Delete Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            textBoxPhone.Text = "";
            radioButtonYES.Checked = true;
        }

        private void radioButtonNO_CheckedChanged(object sender, EventArgs e)
        {

        }

        //display selected row from datagridview1 to textboxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxNumber.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBoxRoomType.SelectedValue = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

            string free = dataGridView1.CurrentRow.Cells[3].Value.ToString();

            if (free.Equals("Yes"))
            {
                radioButtonYES.Checked = true;
            }else if (free.Equals("No"))
            {
                radioButtonNO.Checked = true;
            }
        }

        private void comboBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
