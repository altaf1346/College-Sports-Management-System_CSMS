using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_tryal
{
    public partial class addsportevent : Page
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True;TrustServerCertificate=True");


        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["name"] == null)
            {
                Response.Redirect("login.aspx");
            }
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String sportname = TB1.Text;
            String sportdesc = TB2.Text;
            String maxplyr = TB3.Text;
            SqlCommand cmd = new SqlCommand("INSERT INTO sporteventtable (sportname, sportdesc, max_players) VALUES (@spn, @spd, @max)", con);
            cmd.Parameters.AddWithValue("@spn", sportname);
            cmd.Parameters.AddWithValue("@spd", sportdesc);
            cmd.Parameters.AddWithValue("@max", Convert.ToInt32(maxplyr));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MultiView1.ActiveViewIndex = 0;
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('SPORT ADDED!', '" + sportname + " successfully created!', 'success');", true);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void removesport_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'REMOVING A SPORT MAY RESULT IN REMOVING ALL THE PARTICIPANTS AND TOURNAMENT INFORMATION FOR THE PARTICULAR SPORT!');", true);
            BindData();
        }

        protected void addsport_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void Backsportview_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        public void BindData()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM sporteventtable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
            else
            {
                GridView3.DataSource = null;
                GridView3.DataBind();
            }
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView3.DataKeys[e.RowIndex].Value.ToString();
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT sportname FROM sporteventtable WHERE sportid = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                String sportname = (String)cmd.ExecuteScalar();

                SqlCommand cmd1 = new SqlCommand("DELETE FROM groupsporteventstudentlist WHERE token IN (SELECT token FROM groupsportdetail WHERE sportid = @id)", con);
                SqlCommand cmd2 = new SqlCommand("DELETE FROM groupsportdetail WHERE sportid = @id", con);
                SqlCommand cmd3 = new SqlCommand("DELETE FROM scoreboardtable WHERE sid = @id", con);
                SqlCommand cmd4 = new SqlCommand("DELETE FROM tournament_sport_table WHERE sid = @id", con);
                SqlCommand cmd5 = new SqlCommand("DELETE FROM sport_team_list WHERE sportid = @id", con);
                SqlCommand cmd6 = new SqlCommand("DELETE FROM sporteventtable WHERE sportid = @id", con);

                cmd1.Parameters.AddWithValue("@id", id);
                cmd2.Parameters.AddWithValue("@id", id);
                cmd3.Parameters.AddWithValue("@id", id);
                cmd4.Parameters.AddWithValue("@id", id);
                cmd5.Parameters.AddWithValue("@id", id);
                cmd6.Parameters.AddWithValue("@id", id);

                // Execute delete commands in the correct order
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                cmd6.ExecuteNonQuery();

                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('SPORT DELETED!', '" + sportname + " successfully deleted!', 'success');", true);
                MultiView1.ActiveViewIndex = 2;
                BindData();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void home_Click(object sender, EventArgs e)
        {
            if (Session["name"] == null)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Response.Redirect("admin_dashboard.aspx");
            }
        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Response.Redirect("sportslist.aspx");
        }
    }
}