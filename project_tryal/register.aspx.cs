using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  // Required for SQL operations
using System.Configuration;  // Required for reading the connection string from Web.config
using System.Security.Cryptography;  // Required for hashing
using System.Text.RegularExpressions;  // Required for regex validation

namespace project_tryal
{
    public partial class register : Page
    {
        // Connection string retrieved from Web.config
        string ConnectingString = ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = TextBox1.Text.Trim();
                string lastName = TextBox2.Text.Trim();
                string username = TextBox3.Text.Trim();
                string email = TextBox4.Text.Trim();
                string password = TextBox5.Text;
                string confirmPassword = TextBox6.Text;

                // Validate inputs
                string validationMessage;
                if (!IsValidInput(firstName, lastName, username, email, password, confirmPassword, out validationMessage))
                {
                    ShowAlert(validationMessage);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(ConnectingString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO reglogin (fname, lname, username, email, pass, registration_date) " +
                                       "VALUES (@fname, @lname, @username, @email, @pass, @regDate)", conn))
                    {
                        cmd.Parameters.AddWithValue("@fname", firstName);
                        cmd.Parameters.AddWithValue("@lname", lastName);
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@email", email);

                        string hashedPassword = HashPassword(password);
                        cmd.Parameters.AddWithValue("@pass", hashedPassword);
                        cmd.Parameters.AddWithValue("@regDate", DateTime.Now); // Add this line

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        ShowAlert("Registered Successfully!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                ShowAlert("An error occurred while processing your request. Please try again later.");
            }
        }

        private bool IsValidInput(string firstName, string lastName, string username, string email, string password, string confirmPassword, out string validationMessage)
        {
            validationMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(firstName))
            {
                validationMessage = "First Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                validationMessage = "Last Name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                validationMessage = "Username is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                validationMessage = "A valid Email is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                validationMessage = "Password is required and must be at least 6 characters long.";
                return false;
            }

            if (password != confirmPassword)
            {
                validationMessage = "Passwords do not match.";
                return false;
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            const string emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void ShowAlert(string message)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Scripts", $"<script>alert('{message}');</script>");
        }

        private void LogError(Exception ex)
        {
            // Log the error to a file, database, or logging service
            System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
        }
    }
}