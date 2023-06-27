using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // MySQL Database Connector
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Hotel_Reservation_System
{
    public partial class Form1 : Form
    {
        //make a public static variable to store the user id and make it accessible from other forms
        public static string user_id;


        // MySQL Database Connection
        private MySqlConnection connection;
        private string server;
        private string username;
        private string server_password;
        private string database;
        public Form1()
        {
            server = "localhost";
            username = "root";
            server_password = "";
            database = "hotel_reservation_system";
            InitializeComponent();
        }

        private bool validate()
        {
            if (user_box.Text.Trim() == String.Empty || password_box.Text.Trim() == String.Empty)
                return false;
            return true;
        }

        private void login_btn_Click_1(object sender, EventArgs e)
        {
            if (!validate())
            {
                MessageBox.Show("Please fill in all the fields");
                return;
            }
            else
            {
                string user = user_box.Text;
                string password = password_box.Text;

                string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={server_password};";
                connection = new MySqlConnection(connectionString);
                string checkquery = $"SELECT * FROM users WHERE username = '{user}' AND Password = '{password}'";
                MySqlCommand cmd = new MySqlCommand(checkquery, connection);
                MySqlDataReader reader;
                try
                {
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        MessageBox.Show("Login Successful");
                        // store the user id in the public static variable
                        user_id = reader.GetString("UserID");

                        this.Hide();
                        Hotel_reserve hotel = new Hotel_reserve();
                        hotel.Show();
                        
                    }
                    else
                    {
                        MessageBox.Show("Invalid Username or Password");
                    }
                 }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }   
            }
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }
    }
}
