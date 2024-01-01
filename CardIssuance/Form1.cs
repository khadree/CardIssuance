using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CardIssuance
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=CardIssuance;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {  // To save data in the database
                con.Open();
                string query = "Insert into Cardtable values(@FirstName, @LastName, @Matric_No, @Levels, @College, @Department, @Hostel, @Phone_No, @Year ) ";
                var command = new SqlCommand(query, con);
                command.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = txtFirstName.Text;
                command.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                command.Parameters.Add("@Matric_No", SqlDbType.VarChar).Value = txtMatric.Text;
                command.Parameters.Add("@Levels", SqlDbType.VarChar).Value = cmbLevel.Text;
                command.Parameters.Add("@College", SqlDbType.VarChar).Value = cmbCollege.Text;
                command.Parameters.Add("@Department", SqlDbType.VarChar).Value = cmbDepartment.Text;
                command.Parameters.Add("@Hostel", SqlDbType.VarChar).Value = cmbHostel.Text;
                command.Parameters.Add("@Phone_No", SqlDbType.VarChar).Value = txtPhone.Text;
                command.Parameters.Add("@Year", SqlDbType.Int).Value = int.Parse(cmbYear.Text);
                command.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Successfully Saved....", "Data Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFirstName.Clear();
                txtLastName.Clear();
                txtMatric.Clear();
                txtPhone.Clear();
                cmbCollege.SelectedIndex = 0;
                cmbDepartment.SelectedIndex = 0;
                cmbHostel.SelectedIndex = 0;
                cmbYear.SelectedIndex = 0;
                cmbLevel.SelectedIndex = 0;  
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
        }
        // To Update the database
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query1 = "Update Cardtable set FirstName = @FirstName, LastName = @LastName, Matric_No = @Matric_No, Levels = @Levels, College = @College, Department = @Department, Hostel = @Hostel, @Phone_No = @Phone_No,Year = @Year  Where Matric_No = @Matric_No";
                var Command1 = new SqlCommand(query1, con);
                Command1.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = txtFirstName.Text;
                Command1.Parameters.Add("@LastName", SqlDbType.VarChar).Value = txtLastName.Text;
                Command1.Parameters.Add("@Matric_No", SqlDbType.VarChar).Value = txtMatric.Text;
                Command1.Parameters.Add("@Levels", SqlDbType.VarChar).Value = cmbLevel.Text;
                Command1.Parameters.Add("@College", SqlDbType.VarChar).Value = cmbCollege.Text;
                Command1.Parameters.Add("@Department", SqlDbType.VarChar).Value = cmbDepartment.Text;
                Command1.Parameters.Add("@Hostel", SqlDbType.VarChar).Value = cmbHostel.Text;
                Command1.Parameters.Add("@Phone_No", SqlDbType.VarChar).Value = txtPhone.Text;
                Command1.Parameters.Add("@Year", SqlDbType.Int).Value = int.Parse(cmbYear.Text);
                Command1.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Updated Successfully ....", "Data Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);          
            }
            catch (Exception error)
            {

                MessageBox.Show(error.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string read = serialPort1.ReadLine();
            txtFirstName.Text = read;
        }
        // To Open Connection with serial port
        public void openConnection()
        {
            bool error = false;
            try
            {
                serialPort1.PortName = "COM1";
                serialPort1.BaudRate = 115200;
                serialPort1.Open();
                //serialPort1.DataReceived += serialPort1_DataReceived;
                btnRead.Enabled = true;
                btnWrite.Enabled = true;
                lblStatus.Text = "CONNECTED";
            }

            catch (UnauthorizedAccessException) { error = true; }
            catch (System.IO.IOException) { error = true; }
            catch (ArgumentException) { error = true; }
            if (error) MessageBox.Show(this, "The System USB is not Connected. Most likely it has been removed, or it is unavailable.", "System USB unavailable", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        public void startport()
        {
            lblStatus.Text = "DISCONNECTED";
            btnRead.Enabled = false;
            btnWrite.Enabled = false;
        }
        private void closeConnection()
        {
            serialPort1.Close();
            btnRead.Enabled = false;
            btnWrite.Enabled = false;
            lblStatus.Text = "DISCONNECTED";

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            startport();
            CheckForIllegalCrossThreadCalls = false;
            // openConnection();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            startport();
            if (serialPort1.IsOpen)
            {
                closeConnection();
            }
            else
            {
                openConnection();
            }
           
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            Application.Exit();
        }
        //To read data through serialPort into Andriuno
        private void btnRead_Click(object sender, EventArgs e)
        {
              try
            {
                serialPort1.Write("a");
                string read = serialPort1.ReadLine();
                txtFirstName.Text = read;

                serialPort1.Write("b");
                string read1 = serialPort1.ReadLine();
                txtLastName.Text = read1;

                serialPort1.Write("c");
                string read2 = serialPort1.ReadLine();
                txtMatric.Text = read2;

                serialPort1.Write("d");
                string read3 = serialPort1.ReadLine();
                cmbLevel.Text = read3;

                serialPort1.Write("e");
                string read4 = serialPort1.ReadLine();
                cmbCollege.Text = read4;

                serialPort1.Write("f");
                string read5 = serialPort1.ReadLine();
                cmbDepartment.Text = read5;

                serialPort1.Write("g");
                string read6 = serialPort1.ReadLine();
                cmbHostel.Text = read6;

                serialPort1.Write("h");
                string read7 = serialPort1.ReadLine();
                txtPhone.Text = read7;

                serialPort1.Write("i");
                string read8 = serialPort1.ReadLine();
                cmbYear.Text = read8;
            }
            catch (Exception error1)
            {

                MessageBox.Show(error1.Message);
            }
            

        }
        // To delete from the database
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                string query2 = "Delete from Cardtable Where Matric_No = @Matric_No";
                var Command2 = new SqlCommand(query2, con);
                Command2.Parameters.Add("@Matric_No", SqlDbType.VarChar).Value = txtMatric.Text;
                Command2.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Successfully Deleted....", "Data Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception err1)
            {
                MessageBox.Show(err1.Message);
            }
        }
        // To search
        private void btnSearch_Click(object sender, EventArgs e)
        {
                try
                {
                    con.Open();
                    string query3 = "Select * from Cardtable Where Matric_No = @Matric_No";
                    var Command3 = new SqlCommand(query3, con);
                    Command3.Parameters.Add("@Matric_No", SqlDbType.VarChar).Value = txtMatric.Text;
                    var adapter = new SqlDataAdapter(Command3);
                    var table = new DataTable();
                    adapter.Fill(table);
                    if(table.Rows.Count == 1)
                    {
                        txtFirstName.Text = table.Rows[0][1].ToString();
                        txtLastName.Text = table.Rows[0][2].ToString();
                        txtMatric.Text = table.Rows[0][3].ToString();
                        cmbLevel.Text = table.Rows[0][4].ToString();
                        cmbCollege.Text = table.Rows[0][5].ToString();
                        cmbDepartment.Text = table.Rows[0][6].ToString();
                        cmbHostel.Text = table.Rows[0][7].ToString();
                        txtPhone.Text = table.Rows[0][8].ToString();
                        cmbYear.Text = table.Rows[0][9].ToString();
                        con.Close();
                    }
                    
                }
                catch (Exception err)
                {

                    MessageBox.Show(err.Message);
                }
            
        }
    }
}
