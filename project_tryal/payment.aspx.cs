using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Diagnostics;

namespace project_tryal
{
    public partial class payment : Page
    {
        public int pricet;

        // Update the connection string to use SQL Server 2022
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                MultiView1.ActiveViewIndex = 0;
                datalist();
                divtag.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        void datalist()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT pname, pid FROM dbo.producttable", con);
                SqlDataReader dr = cmd.ExecuteReader();
                DDL26.DataSource = dr;
                DDL26.DataTextField = "pname";
                DDL26.DataValueField = "pid";
                DDL26.DataBind();
                con.Close();
                DDL26.Items.Insert(0, new ListItem("--Select product--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error loading product data: " + ex.Message + "');</script>");
            }
        }

        protected void DDL26_SelectedIndexChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT pprice FROM producttable WHERE pid=@pid", con);
            cmd1.Parameters.AddWithValue("@pid", DDL26.SelectedValue.ToString());
            pricet = Convert.ToInt32(cmd1.ExecuteScalar());
            Session.Add("money", pricet);
            Button1.Text = "PAY ₹ " + pricet.ToString();
            Button2.Text = "PAY ₹ " + pricet.ToString();
            con.Close();
        }

        protected void fname_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT dummystudenttable.studentname, dummystudenttable.studentid FROM dummystudenttable JOIN sport_team_list ON sport_team_list.studentid=dummystudenttable.studentid", con);
            SqlDataReader sda = cmd1.ExecuteReader();
            if (sda.HasRows)
            {
                while (sda.Read())
                {
                    if (fname.Text == sda["studentname"].ToString())
                    {
                        fname.BorderColor = Color.Green;
                        divtag.Visible = true;
                        Session.RemoveAll();
                        Session.Add("id", sda["studentid"].ToString());
                        break;
                    }
                    else
                    {
                        fname.BorderColor = Color.Red;
                    }
                }
            }
            else
            {
                fname.BorderColor = Color.Red;
            }
            con.Close();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into paymenttable values(@bf,@amt,@bfid,@item)", con);
            cmd.Parameters.AddWithValue("bf", fname.Text);
            cmd.Parameters.AddWithValue("bfid", Session["id"].ToString());
            cmd.Parameters.AddWithValue("amt", Session["money"].ToString());
            cmd.Parameters.AddWithValue("item", DDL26.SelectedItem.ToString());
            cmd.ExecuteNonQuery();
            con.Close();

            // Set receipt values
            lblStudentName.Text = fname.Text;
            lblStudentID.Text = Session["id"].ToString();
            lblProduct.Text = DDL26.SelectedItem.ToString();
            lblAmount.Text = Session["money"].ToString();
            lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            MultiView1.ActiveViewIndex = 2; // Show receipt view
            Session.RemoveAll();
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}