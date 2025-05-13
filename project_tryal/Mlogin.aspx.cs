using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;  // Required for SQL operations
using System.Configuration;  // Required for retrieving connection string
using System.Security.Cryptography;  // Required for hashing

namespace project_tryal
{
    public partial class Mlogin : Page
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
                string username = TextBox1.Text.Trim();
                string password = TextBox2.Text.Trim();

                using (SqlConnection conn = new SqlConnection(ConnectingString))
                {
                    string query = "SELECT username, pass FROM reglogin WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);

                        conn.Open();
                        SqlDataReader sdr = cmd.ExecuteReader();

                        if (sdr.Read())
                        {
                            string storedHash = sdr["pass"].ToString();
                            if (VerifyPassword(password, storedHash))
                            {
                                // Successful login
                                Response.Redirect("index.aspx");
                            }
                            else
                            {
                                ShowAlert("Login Failed!!! Invalid Username or Password");
                            }
                        }
                        else
                        {
                            ShowAlert("Login Failed!!! Invalid Username or Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                ShowAlert("An error occurred while processing your request. Please try again later.");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            // Redirect to registration page
            Response.Redirect("Register.aspx"); //Replace Register.aspx with your registration page's name.
        }

        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(enteredPassword));
                string hash = Convert.ToBase64String(bytes);
                return hash == storedHash;
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