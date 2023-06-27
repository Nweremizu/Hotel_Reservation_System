using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // MySQL Database Connector

namespace Hotel_Reservation_System
{
    public partial class Register : Form
    {
        private MySqlConnection connection;
        private string server;
        private string username;
        private string server_password;
        private string database;
        public Register()
        {
            server = "localhost";
            username = "root";
            server_password = "";
            database = "hotel_reservation_system";
            InitializeComponent();
        }


        private bool validate()
        {
            if (fname_box.Text.Trim() == String.Empty || lname_box.Text.Trim() == String.Empty || phone_box.Text.Trim() == String.Empty || pass_box.Text.Trim() == String.Empty)
                return false;
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate())
            {
                MessageBox.Show("Please fill in all the fields");
                return;

            }
            else
            {
                string name = fname_box.Text;
                string user = lname_box.Text;
                string phone = phone_box.Text;
                string password = pass_box.Text;

                string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={server_password};";
                connection = new MySqlConnection(connectionString);
                string query = $"INSERT INTO users (UserID,username,FullName,Phone,Password ) VALUES ('',@user,@name, @phone, @password)";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@password", password);

                try
                {
                    connection.Open();

                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show($"{name} registered Successfully");
                        this.Close();
                        Form1 login = new Form1();
                        login.Show();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            
        }

            

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }
    }
}
