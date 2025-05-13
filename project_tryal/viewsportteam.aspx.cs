using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace project_tryal
{
    public partial class viewsportteam : Page
    {
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-EN5BMMJI\\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                //BindData();
                con.Open();
                SqlCommand cmd = new SqlCommand("select sportid,sportname from sporteventtable", con);
                SqlDataReader dr = cmd.ExecuteReader();
                DDL25.DataSource = dr;
                DDL25.DataTextField = "sportname";
                DDL25.DataValueField = "sportid";
                DDL25.DataBind();
                con.Close();
                DDL25.Items.Insert(0, new ListItem("--Select sport--", "0"));

            }
        }
        public void BindData()
        {
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select dummystudenttable.studentid, dummystudenttable.studentname from dummystudenttable join sport_team_list on dummystudenttable.studentid = sport_team_list.studentid where sport_team_list.sportid='" + DDL25.SelectedValue.ToString() + "'", con);

            DataTable dt = new DataTable();

            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                DataTable de = new DataTable();
                GridView1.DataSource = de;
                GridView1.DataBind();
            }

            con.Close();
        }

        protected void DDL25_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}