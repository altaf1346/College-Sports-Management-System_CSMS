using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace project_tryal
{
    public partial class addsportteam : Page
    {
        private readonly string connectionString = "Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True;TrustServerCertificate=True";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["name"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                MultiView1.ActiveViewIndex = 0;
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            }
        }

        protected void addteam_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            BindSportDropdown(DDL25);
        }

        protected void removeteam_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            BindSportDropdown(DDL26);
        }

        private void BindSportDropdown(DropDownList ddl)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT sportid, sportname FROM sporteventtable", con);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    ddl.DataSource = dr;
                    ddl.DataTextField = "sportname";
                    ddl.DataValueField = "sportid";
                    ddl.DataBind();
                    con.Close();
                    ddl.Items.Insert(0, new ListItem("--Select sport--", "0"));
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error in BindSportDropdown: {ex.Message}");
            }
        }

        private void BindStudentData(GridView gridView, string query)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gridView.DataSource = dt;
                        gridView.DataBind();
                    }
                    else
                    {
                        gridView.DataSource = null;
                        gridView.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"Error in BindStudentData: {ex.Message}");
            }
        }

        public void BindData()
        {
            string query = "SELECT dummystudenttable.studentid, dummystudenttable.studentname FROM dummystudenttable WHERE dummystudenttable.studentid NOT IN (SELECT sport_team_list.studentid FROM sport_team_list)";
            BindStudentData(GridView1, query);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string str = "";

            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (CheckBox.Checked)
                    {
                        string id = row.Cells[1].Text;
                        try
                        {
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd = new SqlCommand("INSERT INTO sport_team_list (sportid, studentid) VALUES (@sportid, @studentid)", con);
                                cmd.Parameters.AddWithValue("@sportid", DDL25.SelectedValue);
                                cmd.Parameters.AddWithValue("@studentid", id);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            str += $"{id} ADDED TO {DDL25.SelectedItem.Text}\\n";
                        }
                        catch (Exception ex)
                        {
                            Response.Write($"Error in Button1_Click (Insert): {ex.Message}");
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'NO SELECTIONS WERE MADE!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"swal('SPORT TEAM ADDED!', '{str}', 'success');", true);
            }
            BindData();
        }

        protected void DDL25_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        public void BindDataT()
        {
            string query = $"SELECT dummystudenttable.studentid, dummystudenttable.studentname FROM dummystudenttable JOIN sport_team_list ON dummystudenttable.studentid = sport_team_list.studentid WHERE sport_team_list.sportid = '{DDL26.SelectedValue}'";
            BindStudentData(GridView7, query);
        }

        protected void DDL26_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataT();
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            string str = "";
            foreach (GridViewRow row in GridView7.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (CheckBox.Checked)
                    {
                        string id = row.Cells[1].Text;
                        try
                        {
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                SqlCommand cmd = new SqlCommand("DELETE FROM sport_team_list WHERE studentid = @studentid", con);
                                cmd.Parameters.AddWithValue("@studentid", id);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            str += $"{id} REMOVED FROM {DDL26.SelectedItem.Text}\\n";
                        }
                        catch (Exception ex)
                        {
                            Response.Write($"Error in Button7_Click (Delete): {ex.Message}");
                        }
                    }
                }
            }
            if (string.IsNullOrEmpty(str))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'NO SELECTIONS WERE MADE!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", $"swal('SPORT TEAM REMOVED!', '{str}', 'success');", true);
            }
            BindDataT();
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

        protected void paymentpage_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            try
            {
                System.Diagnostics.Debug.WriteLine("paymentpage_Click started"); //debug line
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM paymenttable", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                System.Diagnostics.Debug.WriteLine("paymentpage_Click finished"); //debug line
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in paymentpage_Click: {ex.Message}"); //debug line
                Response.Write($"Error in paymentpage_Click: {ex.Message}");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("paidstudentlist.aspx");
        }
    }
}