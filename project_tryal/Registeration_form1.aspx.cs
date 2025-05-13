using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_tryal
{
    public partial class Registeration_form1 : Page
    {
        private readonly string connectionString = "Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            }
        }

        protected void Textbox11_onTextchanged(object sender, EventArgs e)
        {
            string roll = TextBox11.Text;
            string query = "SELECT * FROM dummystudenttable WHERE studentid=@roll";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@roll", roll);
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                TextBox2.Text = dr["studentname"].ToString();
                                TextBox3.Text = dr["dob"].ToString();
                                TextBox4.Text = dr["mobile"].ToString();
                                TextBox5.Text = dr["courseid"].ToString();

                                TextBox2.Enabled = false;
                                TextBox3.Enabled = false;
                                TextBox4.Enabled = false;
                                TextBox5.Enabled = false;
                            }
                        }
                        else
                        {
                            TextBox2.Text = "";
                            TextBox3.Text = "";
                            TextBox4.Text = "";
                            TextBox5.Text = "";

                            TextBox2.Enabled = true;
                            TextBox3.Enabled = true;
                            TextBox4.Enabled = true;
                            TextBox5.Enabled = true;
                        }
                    }
                }
            }
        }

        protected void back_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string tid = DDL1.SelectedValue.ToString();
            string roll = TextBox11.Text;
            int count = 0;

            foreach (ListItem list in CheckBoxList1.Items)
            {
                if (list.Selected)
                {
                    count++;
                    List<string> stid = new List<string>();

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        string selectQuery = "SELECT studentid FROM scoreboardtable";
                        using (SqlDataAdapter sd = new SqlDataAdapter(selectQuery, con))
                        {
                            DataSet ds = new DataSet();
                            sd.Fill(ds, "id");
                            foreach (DataRow row in ds.Tables["id"].Rows)
                            {
                                stid.Add(row["studentid"].ToString());
                            }
                        }
                    }

                    if (!stid.Contains(roll))
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                string insertQuery = "INSERT INTO scoreboardtable (tid, studentid, sid) VALUES (@tid, @studentid, @sid)";
                                using (SqlCommand cmd2 = new SqlCommand(insertQuery, con))
                                {
                                    cmd2.Parameters.AddWithValue("@tid", tid);
                                    cmd2.Parameters.AddWithValue("@studentid", roll);
                                    cmd2.Parameters.AddWithValue("@sid", list.Value);
                                    con.Open();
                                    cmd2.ExecuteNonQuery();
                                }
                            }
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('SUCCESS!', 'Successfully registered');", true);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'Already exist for an individual event');", true);
                    }
                }
            }

            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'Please select a sport event to continue');", true);
            }
        }

        protected void DDL1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataSource1.SelectCommand = "SELECT sportid, sportname FROM sporteventtable JOIN tournament_sport_table ON sporteventtable.sportid=tournament_sport_table.sid WHERE sporteventtable.max_players=1 AND tournament_sport_table.tid=@tid";
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("tid", DDL1.SelectedValue.ToString());
        }

        protected void individual_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT tournamentid, tname FROM dbo.addtournamenttable";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            DDL1.DataSource = dr;
                            DDL1.DataTextField = "tname";
                            DDL1.DataValueField = "tournamentid";
                            DDL1.DataBind();
                        }
                    }
                }
                DDL1.Items.Insert(0, new ListItem("--Select tournament--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Group_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
            MultiView2.ActiveViewIndex = 0;
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT courseid, coursename FROM dbo.coursetable";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            Deptlist.DataSource = dr;
                            Deptlist.DataTextField = "coursename";
                            Deptlist.DataValueField = "courseid";
                            Deptlist.DataBind();
                        }
                    }
                }
                Deptlist.Items.Insert(0, new ListItem("--Select department--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void Deptlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT tournamentid, tname FROM dbo.addtournamenttable";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        tourlist.DataSource = dr;
                        tourlist.DataTextField = "tname";
                        tourlist.DataValueField = "tournamentid";
                        tourlist.DataBind();
                    }
                }
            }
            tourlist.Items.Insert(0, new ListItem("--Select tournament--", "0"));
            tourlist.Enabled = true;
        }

        protected void tourlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT tournament_sport_table.sid, sporteventtable.sportname, sporteventtable.max_players FROM tournament_sport_table JOIN sporteventtable ON sporteventtable.sportid = tournament_sport_table.sid AND sporteventtable.max_players > 1 WHERE tournament_sport_table.tid=@tid";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tid", tourlist.SelectedValue.ToString());
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        toursportlist.DataSource = dr;
                        toursportlist.DataTextField = "sportname";
                        toursportlist.DataValueField = "sid";
                        toursportlist.DataBind();
                    }
                }
            }
            toursportlist.Items.Insert(0, new ListItem("--Select sport--", "0"));
            toursportlist.Enabled = true;
        }

        protected void toursportlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            if (!checkidexist())
            {
                switch (toursportlist.SelectedItem.ToString())
                {
                    case "FOOTBALL":
                        MultiView2.ActiveViewIndex = 1;
                        break;
                    case "CRICKET":
                        MultiView2.ActiveViewIndex = 2;
                        break;
                    case "BASKETBALL":
                        MultiView2.ActiveViewIndex = 3;
                        break;
                    case "VOLLEYBALL":
                        MultiView2.ActiveViewIndex = 4;
                        break;
                    case "THROWBALL":
                        MultiView2.ActiveViewIndex = 5;
                        break;
                    case "HOCKEY":
                        MultiView2.ActiveViewIndex = 6;
                        break;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', '" + Deptlist.SelectedItem.ToString() + " has already been registered. please choose another DEPT');", true);
                tourlist.ClearSelection();
                Deptlist.ClearSelection();
                toursportlist.ClearSelection();
                tourlist.Enabled = false;
                toursportlist.Enabled = false;
            }
        }

        // Checking if rollno exist for group registration
        protected void p1_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p1);
        }

        protected void p2_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p2);
        }

        protected void p3_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p3);
        }

        protected void p4_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p4);
        }

        protected void p5_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p5);
        }

        protected void p6_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p6);
        }

        protected void p7_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p7);
        }

        protected void p8_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p8);
        }

        protected void p9_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p9);
        }

        protected void p10_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p10);
        }

        protected void p11_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p11);
        }

        protected void p12_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p12);
        }

        protected void p13_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p13);
        }

        protected void p14_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p14);
        }

        protected void p15_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(p15);
        }

        private void CheckRollNoExist(TextBox textBox)
        {
            string query = "SELECT COUNT(*) FROM dummystudenttable WHERE studentid=@roll AND courseid=@courseid";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@roll", textBox.Text);
                    cmd.Parameters.AddWithValue("@courseid", Deptlist.SelectedValue.ToString());
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    textBox.BorderColor = count > 0 ? Color.Green : Color.Red;
                }
            }
        }

        // Group sport confirmation
        protected void grpbtnconfirmp1_Click(object sender, EventArgs e)
        {
            ConfirmGroupSport(p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15);
        }

        protected void grpbtnconfirmpc1_Click(object sender, EventArgs e)
        {
            ConfirmGroupSport(pc1, pc2, pc3, pc4, pc5, pc6, pc7, pc8, pc9, pc10, pc11, pc12, pc13, pc14, pc15);
        }

        protected void grpbtnconfirmpb1_Click(object sender, EventArgs e)
        {
            ConfirmGroupSport(pb1, pb2, pb3, pb4, pb5, pb6, pb7, pb8, pb9, pb10);
        }

        protected void grpbtnconfirmpv1_Click(object sender, EventArgs e)
        {
            ConfirmGroupSport(pv1, pv2, pv3, pv4, pv5, pv6, pv7, pv8, pv9, pv10);
        }

        protected void grpbtnconfirmpt1_Click(object sender, EventArgs e)
        {
            ConfirmGroupSport(pt1, pt2, pt3, pt4, pt5, pt6, pt7, pt8, pt9, pt10);
        }

        private void ConfirmGroupSport(params TextBox[] textBoxes)
        {
            bool allGreen = true;
            HashSet<string> uniqueRollNos = new HashSet<string>();

            foreach (TextBox textBox in textBoxes)
            {
                if (textBox.BorderColor != Color.Green || !uniqueRollNos.Add(textBox.Text))
                {
                    allGreen = false;
                    break;
                }
            }

            if (allGreen)
            {
                int token;
                string teamName = teamname.Text;
                string sportId = toursportlist.SelectedValue.ToString();
                string courseId = Deptlist.SelectedValue.ToString();
                string tournamentId = tourlist.SelectedValue.ToString();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT teamname FROM groupsportdetail";
                    using (SqlDataAdapter sd = new SqlDataAdapter(query, con))
                    {
                        DataSet ds = new DataSet();
                        sd.Fill(ds, "TNAME");
                        List<string> tname = new List<string>();
                        foreach (DataRow row in ds.Tables["TNAME"].Rows)
                        {
                            tname.Add(row["teamname"].ToString());
                        }

                        if (!tname.Contains(teamName))
                        {
                            string insertGroupQuery = "INSERT INTO groupsportdetail (teamname, sportid, courseid, tournamentid) VALUES (@teamname, @sportid, @courseid, @tournamentid)";
                            using (SqlCommand cmd = new SqlCommand(insertGroupQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@teamname", teamName);
                                cmd.Parameters.AddWithValue("@sportid", sportId);
                                cmd.Parameters.AddWithValue("@courseid", courseId);
                                cmd.Parameters.AddWithValue("@tournamentid", tournamentId);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }

                            string selectTokenQuery = "SELECT token FROM groupsportdetail WHERE teamname=@teamname";
                            using (SqlCommand cmd = new SqlCommand(selectTokenQuery, con))
                            {
                                cmd.Parameters.AddWithValue("@teamname", teamName);
                                con.Open();
                                token = (int)cmd.ExecuteScalar();
                                con.Close();
                            }

                            string insertStudentQuery = "INSERT INTO groupsporteventstudentlist (token, studentid) VALUES (@token, @studentid)";
                            foreach (TextBox textBox in textBoxes)
                            {
                                using (SqlCommand cmd = new SqlCommand(insertStudentQuery, con))
                                {
                                    cmd.Parameters.AddWithValue("@token", token);
                                    cmd.Parameters.AddWithValue("@studentid", textBox.Text);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }

                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('SUCCESS!', 'Successfully registered');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'Team name already taken');", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "swal('WARNING!', 'Roll no. should be unique and valid');", true);
            }
        }

        public Boolean checkidexist()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM groupsportdetail WHERE courseid=@courseid AND sportid=@sportid";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@courseid", Deptlist.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@sportid", toursportlist.SelectedValue.ToString());
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
            }
        }

        // Missing method added
        protected void grpbtnbackp1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        // Missing method added
        protected void pc1_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc1);
        }

        // Missing method added
        protected void pc2_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc2);
        }

        // Missing method added
        protected void pc3_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc3);
        }

        // Missing method added
        protected void pc4_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc4);
        }

        // Add remaining methods
        protected void pc5_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc5);
        }

        protected void pc6_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc6);
        }

        protected void pc7_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc7);
        }

        protected void pc8_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc8);
        }

        protected void pc9_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc9);
        }

        protected void pc10_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc10);
        }

        protected void pc11_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc11);
        }

        protected void pc12_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc12);
        }

        protected void pc13_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc13);
        }

        protected void pc14_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc14);
        }

        protected void pc15_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pc15);
        }

        protected void pb1_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb1);
        }

        protected void pb2_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb2);
        }

        protected void pb3_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb3);
        }

        protected void pb4_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb4);
        }

        protected void pb5_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb5);
        }

        protected void pb6_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb6);
        }

        protected void pb7_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb7);
        }

        protected void pb8_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb8);
        }

        protected void pb9_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb9);
        }

        protected void pb10_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pb10);
        }

        protected void pv1_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv1);
        }

        protected void pv2_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv2);
        }

        protected void pv3_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv3);
        }

        protected void pv4_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv4);
        }

        protected void pv5_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv5);
        }

        protected void pv6_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv6);
        }

        protected void pv7_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv7);
        }

        protected void pv8_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv8);
        }

        protected void pv9_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv9);
        }

        protected void pv10_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pv10);
        }

        protected void pt1_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt1);
        }

        protected void pt2_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt2);
        }

        protected void pt3_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt3);
        }

        protected void pt4_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt4);
        }

        protected void pt5_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt5);
        }

        protected void pt6_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt6);
        }

        protected void pt7_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt7);
        }

        protected void pt8_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt8);
        }

        protected void pt9_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt9);
        }

        protected void pt10_onTextchanged(object sender, EventArgs e)
        {
            CheckRollNoExist(pt10);
        }
        protected void grpbtnbackpb1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void grpbtnbackpv1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void grpbtnbackpt1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }
        protected void grpbtnbackpc1_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }


    }
}


