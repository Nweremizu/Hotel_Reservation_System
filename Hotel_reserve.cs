using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using  MySql.Data.MySqlClient; // MySQL Database Connector

namespace Hotel_Reservation_System
{
    public partial class Hotel_reserve : Form
    {
        public static string MyName;

        // MySQL Database Connection
        private MySqlConnection connection;
        private string server;
        private string username;
        private string server_password;
        private string database;
        private int price;

        //list of room types
        List<string> room_types = new List<string>()
        {
            "Single",
            "Double",
            "Suite",
            "Family",
            "Standard",
            "Deluxe",
        };

        public Hotel_reserve()
        {
            server = "localhost";
            username = "root";
            server_password = "";
            database = "hotel_reservation_system";
            InitializeComponent();
        }

        // when the form loads
        private void Hotel_reserve_Load(object sender, EventArgs e)
        {
            // Add the room types to the room type combo box
            foreach (string room_type in room_types)
            {
                room_type_box.Items.Add(room_type);
            }

            //get the user id from the login form
            string user_id = Form1.user_id;
            //get the user name from the database
            string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={server_password};";
            connection = new MySqlConnection(connectionString);
            string checkquery = $"SELECT * FROM users WHERE UserID = '{user_id}'";
            MySqlCommand cmd = new MySqlCommand(checkquery, connection);
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    // Display the user name in the user name labe
                    username_label.Text = $"Welcome, {reader.GetString("username")}";
                    name_label.Text = reader.GetString("FullName");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        // when the room type combo box is changed
        private void room_type_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Collect the room number that is available from the database and display it in the room number combo box

            string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={server_password};";
            connection = new MySqlConnection(connectionString);
            // Query to get the room number that is available with the selected room type and selected capacity
            string checkquery = $"SELECT RoomNO FROM rooms WHERE Type = '{room_type_box.Text}' AND Avaliablity = 1";
            MySqlCommand cmd = new MySqlCommand(checkquery, connection);
            // Trys to open the connection and read the data from the database
            MySqlDataReader reader;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                room_number_box.Items.Clear();
                while (reader.Read())
                {
                    // Add the room number to the room number combo box
                    room_number_box.Items.Add(reader.GetString("RoomNO"));
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void room_number_box_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Get the capacity and price of the room number from the database and display it in the capacity and price text boxes
            string checkquery2 = $"SELECT Capacity, Price FROM rooms WHERE RoomNO = '{room_number_box.Text}'";
            MySqlCommand cmd2 = new MySqlCommand(checkquery2, connection);
            MySqlDataReader reader2;
            try
            {
                connection.Open();
                reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    // Display the capacity and price of the room number in the capacity and price text boxes
                    int num_of_days = CalculateDaysBetweenDates(date_in_box.Text, date_out_box.Text);
                    price = int.Parse(reader2.GetString("Price")) * num_of_days;
                    capacity_box.Text = reader2.GetString("Capacity");
                    price_box.Text = price.ToString("C");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void submi_btn_Click(object sender, EventArgs e)
        {
            // Check if the user has entered all the required fields
            if (!validate())
            {
                MessageBox.Show("Please fill all the required fields");
                return;
            }

            // Insert the data into the database
            try
            {
                string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={server_password};";
                using (connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Insert customer details and room number into the customers table
                    string insertCustomerQuery = $"INSERT INTO customers (Name, Address, State, Country, RoomNO) VALUES ('{name_label.Text}', '{address_box.Text}', '{state_box.Text}', '{country_box.Text}', '{room_number_box.Text}')";
                    using (MySqlCommand cmd = new MySqlCommand(insertCustomerQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Save the customer id, room id, check-in date, and check-out date in the reservations table
                    string insertReservationQuery = $"INSERT INTO reservation (RoomID, customerID, CheckInDate, CheckOutDate, TotalPrice) VALUES ((SELECT RoomID FROM rooms WHERE RoomNO = '{room_number_box.Text}'),(SELECT CustomerID FROM customers WHERE Name = '{name_label.Text}' AND RoomNO = '{room_number_box.Text}'), '{date_in_box.Text}', '{date_out_box.Text}', '{price}')";
                    using (MySqlCommand cmd2 = new MySqlCommand(insertReservationQuery, connection))
                    {
                        cmd2.ExecuteNonQuery();
                    }

                    // Update the room availability to 0
                    string updateRoomQuery = $"UPDATE rooms SET Avaliablity = 0 WHERE RoomNO = '{room_number_box.Text}'";
                    using (MySqlCommand cmd3 = new MySqlCommand(updateRoomQuery, connection))
                    {
                        cmd3.ExecuteNonQuery();
                    }
                    
                    
                }
                

                //Show all Registered details in Messagebox
                MessageBox.Show("Customer Name: " + name_label.Text + "\n" + "Address: " + address_box.Text + "\n" + "State: " + state_box.Text + "\n" + "Country: " + country_box.Text + "\n" + "Room Type: " + room_type_box.Text + "\n" + "Room Number: " + room_number_box.Text + "\n" + "Capacity: " + capacity_box.Text + "\n" + "Price: " + price_box.Text + "\n" + "Check In Date: " + date_in_box.Text + "\n" + "Check Out Date: " + date_out_box.Text, "Registered Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear all the fields
                name_label.Text = "";
                address_box.Text = "";
                state_box.Text = "";
                country_box.Text = "";
                room_type_box.Text = "";
                room_number_box.Text = "";
                capacity_box.Text = "";
                price_box.Text = "";
                date_in_box.Text = "";
                date_out_box.Text = "";

                
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                // if an error occured, delete the customer details from the customers table
                string connectionString = $"SERVER={server};DATABASE={database};UID={username};PASSWORD={server_password};";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteCustomerQuery = $"DELETE FROM customers WHERE Name = '{name_label.Text}' AND RoomNO = '{room_number_box.Text}'";
                    using (MySqlCommand cmd = new MySqlCommand(deleteCustomerQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                // if an error occured, delete the reservation details from the reservation table
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string deleteReservationQuery = $"DELETE FROM reservation WHERE CheckInDate = '{date_in_box.Text}' AND CheckOutDate = '{date_out_box.Text}'";
                    using (MySqlCommand cmd = new MySqlCommand(deleteReservationQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                // if an error occured, update the room availability to 1
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string updateRoomQuery = $"UPDATE rooms SET Avaliablity = 1 WHERE RoomNO = '{room_number_box.Text}'";
                    using (MySqlCommand cmd = new MySqlCommand(updateRoomQuery, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private bool validate()
        {
            if (name_label.Text.Trim() == String.Empty || address_box.Text.Trim() == String.Empty || state_box.Text.Trim() == String.Empty || country_box.Text.Trim() == String.Empty || room_type_box.Text.Trim() == String.Empty || room_number_box.Text.Trim() == String.Empty || capacity_box.Text.Trim() == String.Empty || price_box.Text.Trim() == String.Empty || date_in_box.Text.Trim() == String.Empty || date_out_box.Text.Trim() == String.Empty)
            {
                // if the check in box is not in format yyyy-mm-dd
                if (!date_in_box.Text.Trim().Contains("-") || !date_out_box.Text.Trim().Contains("-"))
                {
                    MessageBox.Show("Please enter the check in date in the format yyyy-mm-dd");
                    return false;
                }
                return false;
            }
                
            return true;
        }

        static int CalculateDaysBetweenDates(string startDateString, string endDateString)
        {
            // Convert the date strings to DateTime objects
            DateTime startDate = DateTime.ParseExact(startDateString, "yyyy-MM-dd", null);
            DateTime endDate = DateTime.ParseExact(endDateString, "yyyy-MM-dd", null);

            // Calculate the number of days between the two dates
            int numberOfDays = (endDate - startDate).Days;

            return numberOfDays;
        }

        // Close Button
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
