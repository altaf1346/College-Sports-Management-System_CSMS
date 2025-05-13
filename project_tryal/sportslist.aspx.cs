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
    public partial class sportslist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-EN5BMMJI\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from sporteventtable", cn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, "sporteventtable");
            CrystalReport4 rpt = new CrystalReport4();
            rpt.SetDataSource(ds);
            // rpt.SetParameterValue("ad", TextBox1.Text);
            CrystalReportViewer1.ReportSource = rpt;
            //crystalReportViewer1.ReportSource = rpt;
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "sporteventtable");
            cn.Close();
        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

        }
    }
}