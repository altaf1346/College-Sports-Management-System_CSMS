using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_tryal
{
    public partial class removeplayers : Page
    {
        private readonly string connectionString = "Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["name"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                BindData();
                LoadTournamentDropDown();
            }
        }

        private void LoadTournamentDropDown()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT tournamentid, tname FROM addtournamenttable", con);
                SqlDataReader dr = cmd.ExecuteReader();
                DDLN.DataSource = dr;
                DDLN.Items.Clear();
                DDLN.DataTextField = "tname";
                DDLN.DataValueField = "tournamentid";
                DDLN.DataBind();
                con.Close();
                DDLN.Items.Insert(0, new ListItem("--Select tournament--", "0"));
            }
        }

        protected void DDL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStudentDropDown();
        }

        private void LoadStudentDropDown()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT dummystudenttable.studentid, dummystudenttable.studentname FROM scoreboardtable JOIN dummystudenttable ON scoreboardtable.studentid = dummystudenttable.studentid WHERE tid = @tid", con);
                cmd.Parameters.AddWithValue("@tid", DDLN.SelectedValue.ToString());
                SqlDataAdapter dr = new SqlDataAdapter(cmd);

                SqlCommand cmd1 = new SqlCommand("SELECT groupsporteventstudentlist.studentid, dummystudenttable.studentname FROM groupsporteventstudentlist INNER JOIN dummystudenttable ON groupsporteventstudentlist.studentid = dummystudenttable.studentid INNER JOIN groupsportdetail ON groupsporteventstudentlist.token = groupsportdetail.token WHERE groupsportdetail.tournamentid = @tid", con);
                cmd1.Parameters.AddWithValue("@tid", DDLN.SelectedValue.ToString());
                SqlDataAdapter dr1 = new SqlDataAdapter(cmd1);

                DataSet ds = new DataSet();
                dr.Fill(ds);
                dr1.Fill(ds);

                DDLN1.DataSource = ds;
                DDLN1.Items.Clear();
                DDLN1.DataTextField = "studentname";
                DDLN1.DataValueField = "studentid";
                DDLN1.DataBind();
                con.Close();
                DDLN1.Items.Insert(0, new ListItem("--please select STUDENT--", "0"));
            }
        }

        protected void DDL2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
            BindData2();
        }

        public void BindData()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT scoreboardtable.studentid, dummystudenttable.studentname, sporteventtable.sportname, sporteventtable.sportid FROM scoreboardtable INNER JOIN dummystudenttable ON dummystudenttable.studentid = scoreboardtable.studentid INNER JOIN sporteventtable ON scoreboardtable.sid = sporteventtable.sportid WHERE scoreboardtable.tid = @tid AND scoreboardtable.studentid = @studentid", con);
                sda.SelectCommand.Parameters.AddWithValue("@tid", DDLN.SelectedValue.ToString());
                sda.SelectCommand.Parameters.AddWithValue("@studentid", DDLN1.SelectedValue.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                con.Close();
            }
        }

        public void BindData2()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT groupsporteventstudentlist.studentid, dummystudenttable.studentname, sporteventtable.sportid, sporteventtable.sportname, groupsportdetail.token FROM groupsporteventstudentlist INNER JOIN dummystudenttable ON groupsporteventstudentlist.studentid = dummystudenttable.studentid INNER JOIN groupsportdetail ON groupsporteventstudentlist.token = groupsportdetail.token INNER JOIN sporteventtable ON sporteventtable.sportid = groupsportdetail.sportid WHERE groupsportdetail.tournamentid = @tid AND dummystudenttable.studentid = @studentid", con);
                sda.SelectCommand.Parameters.AddWithValue("@tid", DDLN.SelectedValue.ToString());
                sda.SelectCommand.Parameters.AddWithValue("@studentid", DDLN1.SelectedValue.ToString());
                DataTable dt = new DataTable();
                sda.Fill(dt);
                GridView2.DataSource = dt;
                GridView2.DataBind();
                con.Close();
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView2.DataKeys[e.RowIndex].Value.ToString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM groupsporteventstudentlist WHERE token = @token AND studentid = @studentid", con);
                cmd.Parameters.AddWithValue("@token", id);
                cmd.Parameters.AddWithValue("@studentid", DDLN1.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('DELETED!', 'Successfully deleted!', 'success');", true);
            BindData2();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView1.DataKeys[e.RowIndex].Value.ToString();
            string name = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT studentname FROM dummystudenttable WHERE studentid = @studentid", con);
                cmd1.Parameters.AddWithValue("@studentid", id);
                name = (string)cmd1.ExecuteScalar();

                SqlCommand cmd = new SqlCommand("DELETE FROM scoreboardtable WHERE tid = @tid AND sid = @sid AND studentid = @studentid", con);
                cmd.Parameters.AddWithValue("@tid", DDLN.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@sid", id);
                cmd.Parameters.AddWithValue("@studentid", DDLN1.SelectedValue.ToString());
                cmd.ExecuteNonQuery();
                con.Close();
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", string.Format("swal('DELETED!', '{0} successfully deleted!', 'success');", name), true);
            BindData();
        }

        protected void Button1_Click(object sender, EventArgs e)
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
    }
}