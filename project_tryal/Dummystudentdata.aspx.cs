using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
namespace project_tryal
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }

        protected void Button1_Click1(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-EN5BMMJI\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from dummystudenttable where courseid = '" + DropDownList1.SelectedItem.Text + "'", cn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, "dummystudenttable");
            CrystalReport2 rpt = new CrystalReport2();
            rpt.SetDataSource(ds);
            // rpt.SetParameterValue("ad", TextBox1.Text);
            CrystalReportViewer1.ReportSource = rpt;
            //crystalReportViewer1.ReportSource = rpt;
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "dummystudenttable");
            cn.Close();
        }
    }
}