using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace project_tryal
{
    public partial class admin_dashboard : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["name"] == null)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    logout.Text = Session["name"].ToString() + " logout";
                }
            }


        }

        protected void sporteventbutton_Click(object sender, EventArgs e)
        {
            Response.Redirect("addsportevent.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("addtournament.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("addscoreboard.aspx");
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("removeplayers.aspx");
        }

        protected void Button7_Click(object sender, EventArgs e)
        {
            Response.Redirect("addsportteam.aspx");
        }
        protected void logout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Mlogin.aspx");

        }
        protected void StudentsList_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dummystudentdata.aspx");
        }

        protected void RegesteredStudents_Click(object sender, EventArgs e)
        {
            Response.Redirect("regesteredstudentlist.aspx");
        }

       
    }
}