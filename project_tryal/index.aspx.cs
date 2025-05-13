using System;
using System.Web.UI;

namespace webapp_sportmanagement
{
    public partial class index : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add any Page_Load logic here
        }

        protected void login_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("tournamentdetails.aspx");
        }
    }
}