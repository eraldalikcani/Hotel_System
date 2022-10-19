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
    public partial class ManageClientsForm : Form
    {
        CLIENT client = new CLIENT();
        public ManageClientsForm()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBoxId.Text = "";
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxPhone.Text = "";
            textBoxCountry.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;

            if(fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals(""))
            {
                MessageBox.Show("Required Fields - First & Last Name + Phone Number", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Boolean insertClient = client.insertClient(fname, lname, phone, country);

                if (insertClient)
                {
                    dataGridView1.DataSource = client.getClients();
                    MessageBox.Show("New Client Inserted Successfully", "Add Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("ERROR - Client Not Inserted", "Add Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void ManageClientsForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = client.getClients();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id;
            String fname = textBoxFirstName.Text;
            String lname = textBoxLastName.Text;
            String phone = textBoxPhone.Text;
            String country = textBoxCountry.Text;

            try
            {
                id = Convert.ToInt32(textBoxId.Text);

                if (fname.Trim().Equals("") || lname.Trim().Equals("") || phone.Trim().Equals(""))
                {
                    MessageBox.Show("Required Fields - First & Last Name + Phone Number", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Boolean editClient = client.editClient(id, fname, lname, phone, country);

                    if (editClient)
                    {
                        dataGridView1.DataSource = client.getClients();
                        MessageBox.Show("Client Updated Successfully", "Edit Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("ERROR - Client Not Updated", "Edit Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //display the selected data from datagridview to textboxes
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxId.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBoxFirstName.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBoxLastName.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBoxPhone.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBoxCountry.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBoxId.Text);

                if (client.removeClient(id)){

                    dataGridView1.DataSource = client.getClients();
                    MessageBox.Show("Client Deleted Successfully", "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //clear all textboxes after the deletion
                    //by calling the clear button
                    button4.PerformClick();
                }
                else
                {
                    MessageBox.Show("ERROR - Client Not Deleted", "Delete Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ID Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
