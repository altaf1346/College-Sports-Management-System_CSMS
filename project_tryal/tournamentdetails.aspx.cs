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
    public partial class tournamentdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(@"Data Source=LAPTOP-EN5BMMJI\SQLEXPRESS;Initial Catalog=newdb;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select * from addtournamenttable where tstartdate  = '" + DropDownList1.SelectedItem.Text + "'", cn); SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            ad.Fill(ds, "addtournamenttable");
            CrystalReport5 rpt = new CrystalReport5();
            rpt.SetDataSource(ds);
            // rpt.SetParameterValue("ad", TextBox1.Text);
            CrystalReportViewer1.ReportSource = rpt;
            //crystalReportViewer1.ReportSource = rpt;
            rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "addtournamenttable");
            cn.Close();
        }
    }
}