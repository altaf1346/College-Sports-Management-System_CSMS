using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_tryal
{
    public partial class addtournament : Page
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True;TrustServerCertificate=True");



        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                if (Session["name"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                MultiView1.ActiveViewIndex = 0;
            }
        }

        // Change views
        protected void addtournamentviewbutton_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            Calendar1.Visible = false;
            Calendar2.Visible = false;
        }

        protected void edittournamentviewbutton_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            BindDatae();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT tournamentid, tname FROM dbo.addtournamenttable", con);
            SqlDataReader dr = cmd.ExecuteReader();
            TTN.DataSource = dr;
            TTN.Items.Clear();
            TTN.DataTextField = "tname";
            TTN.DataValueField = "tournamentid";
            TTN.DataBind();
            con.Close();
            TTN.Items.Insert(0, new ListItem("--Select tournament--", "0"));
        }

        protected void deletetournamentviewbutton_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;
            BindData();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'REMOVING A TOURNAMENT RESULT IN REMOVING ALL THE PARTICIPANTS AND EVENTS FOR THE PARTICULAR TOURNAMENT!!');", true);
        }

        // Add tournament methods
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Calendar1.Visible = !Calendar1.Visible;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox3.Text = Calendar1.SelectedDate.ToString("yyyy-MM-dd");
            Calendar1.Visible = false;
            TextBox4.Text = "";
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Calendar2.Visible = !Calendar2.Visible;
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            TextBox4.Text = Calendar2.SelectedDate.ToString("yyyy-MM-dd");
            Calendar2.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string tname = TextBox1.Text;
            string tdesc = TextBox2.Text;
            DateTime tsd = DateTime.Parse(TextBox3.Text);
            DateTime ted = DateTime.Parse(TextBox4.Text);

            SqlCommand cmd = new SqlCommand("INSERT INTO addtournamenttable (tname, tstartdate, tenddate, tdesc) VALUES (@tn, @tsd, @ted, @td)", con);
            cmd.Parameters.AddWithValue("@tn", tname);
            cmd.Parameters.AddWithValue("@tsd", tsd);
            cmd.Parameters.AddWithValue("@ted", ted);
            cmd.Parameters.AddWithValue("@td", tdesc);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex) { Response.Write(ex); }

            string tid = "";
            try
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("SELECT tournamentid FROM addtournamenttable WHERE tname=@tn", con);
                cmd1.Parameters.AddWithValue("@tn", tname);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    tid = dr["tournamentid"].ToString();
                }
                dr.Close();
                foreach (ListItem list in CheckBoxList1.Items)
                {
                    if (list.Selected)
                    {
                        SqlCommand cmd2 = new SqlCommand("INSERT INTO tournament_sport_table (tid, sid) VALUES (@tid, @sid)", con);
                        cmd2.Parameters.AddWithValue("@tid", tid);
                        cmd2.Parameters.AddWithValue("@sid", list.Value);
                        cmd2.ExecuteNonQuery();
                    }
                }
                con.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('" + tname + "', 'successfully added tournament!', 'success');", true);
            }
            catch (Exception ex) { Response.Write(ex); }
            MultiView1.ActiveViewIndex = 0;
        }

        protected void backinadtour_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        // Edit tournament methods
        protected void TTN_SelectedIndexChanged(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(TTN.SelectedIndex.ToString());
            if (n == 0)
            {
                DataTable de = new DataTable();
                GV1.DataSource = de;
                GV1.DataBind();
                GV2.DataSource = de;
                GV2.DataBind();
                edittournamentmultiview.ActiveViewIndex = -1;
            }
            else
            {
                con.Open();
                SqlCommand smd = new SqlCommand("SELECT tstartdate, tenddate FROM addtournamenttable WHERE tournamentid=@tid", con);
                smd.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
                SqlDataReader sd = smd.ExecuteReader();
                if (sd.HasRows)
                {
                    while (sd.Read())
                    {
                        DATESTART.Text = sd["tstartdate"].ToString();
                        ENDDATE.Text = sd["tenddate"].ToString();
                    }
                }
                else
                {
                    DATESTART.Text = "";
                    ENDDATE.Text = "";
                }
                con.Close();

                BindDatae();
                Bindsport();
                edittournamentmultiview.ActiveViewIndex = 0;
            }
        }

        public void Bindsport()
        {
            con.Open();
            SqlDataAdapter sda1 = new SqlDataAdapter("SELECT sportname, sportid FROM sporteventtable WHERE sporteventtable.sportid NOT IN (SELECT sid FROM tournament_sport_table WHERE tid=@tid)", con);
            sda1.SelectCommand.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                GV1.DataSource = dt1;
                GV1.DataBind();
            }
            else
            {
                DataTable de = new DataTable();
                GV1.DataSource = de;
                GV1.DataBind();
            }
            con.Close();
        }

        public void BindDatae()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT sporteventtable.sportname, sporteventtable.sportid FROM tournament_sport_table JOIN sporteventtable ON tournament_sport_table.sid=sporteventtable.sportid WHERE tournament_sport_table.tid=@tid", con);
            sda.SelectCommand.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GV2.DataSource = dt;
                GV2.DataBind();
            }
            else
            {
                DataTable de = new DataTable();
                GV2.DataSource = de;
                GV2.DataBind();
            }
            con.Close();
        }

        protected void GV1_OnCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            checkbox.Checked = true;
            GridViewRow row = (GridViewRow)checkbox.NamingContainer;
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT sportname FROM sporteventtable WHERE sportid=@sid", con);
            cmd.Parameters.AddWithValue("@sid", row.Cells[2].Text);
            SqlCommand cmd2 = new SqlCommand("INSERT INTO tournament_sport_table (tid, sid) VALUES (@tid, @sid)", con);
            cmd2.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
            cmd2.Parameters.AddWithValue("@sid", row.Cells[2].Text);
            string sportname = (string)cmd.ExecuteScalar();
            cmd2.ExecuteNonQuery();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('SPORT ADDED!', '" + sportname + " successfully added to tournament!', 'success');", true);
            con.Close();
            Bindsport();
            BindDatae();
        }

        protected void GV2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GV2.DataKeys[e.RowIndex].Value.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT sportname FROM sporteventtable WHERE sportid=@sid", con);
            cmd.Parameters.AddWithValue("@sid", id);
            string sportname = (string)cmd.ExecuteScalar();

            SqlCommand cmd1 = new SqlCommand("DELETE FROM groupsporteventstudentlist WHERE EXISTS (SELECT token FROM groupsportdetail WHERE sportid=@sid AND tournamentid=@tid)", con);
            SqlCommand cmd2 = new SqlCommand("DELETE FROM groupsportdetail WHERE sportid=@sid AND tournamentid=@tid", con);
            SqlCommand cmd3 = new SqlCommand("DELETE FROM tournament_sport_table WHERE tid=@tid AND sid=@sid", con);
            cmd1.Parameters.AddWithValue("@sid", id);
            cmd1.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
            cmd2.Parameters.AddWithValue("@sid", id);
            cmd2.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
            cmd3.Parameters.AddWithValue("@sid", id);
            cmd3.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());

            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('SPORT REMOVED!', '" + sportname + " successfully removed from tournament!', 'success');", true);
            con.Close();
            BindDatae();
            Bindsport();
        }

        protected void B2_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            con.Open();
            SqlCommand cmd = new SqlCommand("UPDATE addtournamenttable SET tstartdate=@tsd, tenddate=@ted WHERE tournamentid=@tid", con);
            cmd.Parameters.AddWithValue("@tsd", DATESTART.Text);
            cmd.Parameters.AddWithValue("@ted", ENDDATE.Text);
            cmd.Parameters.AddWithValue("@tid", TTN.SelectedValue.ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('UPDATES!', 'successfully updated changes for the tournament!', 'success');", true);
        }

        // Delete tournament methods
        public void BindData()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM addtournamenttable", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView3.DataSource = dt;
                GridView3.DataBind();
            }
            con.Close();
        }

        protected void GridView3_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = GridView3.DataKeys[e.RowIndex].Value.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT tname FROM addtournamenttable WHERE tournamentid=@tid", con);
            cmd.Parameters.AddWithValue("@tid", id);

            SqlCommand cmd1 = new SqlCommand("DELETE FROM groupsporteventstudentlist WHERE EXISTS (SELECT token FROM groupsportdetail WHERE tournamentid=@tid)", con);
            SqlCommand cmd2 = new SqlCommand("DELETE FROM groupsportdetail WHERE tournamentid=@tid", con);
            SqlCommand cmd3 = new SqlCommand("DELETE FROM scoreboardtable WHERE tid=@tid", con);
            SqlCommand cmd4 = new SqlCommand("DELETE FROM tournament_sport_table WHERE tid=@tid", con);
            SqlCommand cmd5 = new SqlCommand("DELETE FROM addtournamenttable WHERE tournamentid=@tid", con);

            cmd1.Parameters.AddWithValue("@tid", id);
            cmd2.Parameters.AddWithValue("@tid", id);
            cmd3.Parameters.AddWithValue("@tid", id);
            cmd4.Parameters.AddWithValue("@tid", id);
            cmd5.Parameters.AddWithValue("@tid", id);

            string tour = (string)cmd.ExecuteScalar();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();

            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('" + tour + "', ' successfully removed from tournament!', 'success');", true);
            con.Close();
            BindData();
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

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
        {
            if (Calendar1.SelectedDate != DateTime.MinValue)
            {
                if (e.Day.Date <= Calendar1.SelectedDate || e.Day.Date >= Calendar1.SelectedDate.AddMonths(3))
                {
                    e.Day.IsSelectable = false;
                    e.Cell.ForeColor = System.Drawing.Color.Gray;
                }
            }
            else
            {
                if (e.Day.Date < DateTime.Today || e.Day.Date >= DateTime.Today.AddMonths(3))
                {
                    e.Day.IsSelectable = false;
                    e.Cell.ForeColor = System.Drawing.Color.Gray;
                }
            }
        }

    }
}