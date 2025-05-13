using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace project_tryal
{
    public partial class regesteredstudentlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set default date range
                txtStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
                txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");
            }
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtStartDate.Text, out DateTime startDate) &&
                DateTime.TryParse(txtEndDate.Text, out DateTime endDate))
            {
                if (endDate < startDate)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('End date must be after start date.');", true);
                    return;
                }

                // Load filtered data and view in report viewer
                LoadReport(startDate, endDate, viewOnly: true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                    "alert('Please enter valid dates in yyyy-MM-dd format.');", true);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtStartDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtEndDate.Text = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd");

            // Load all data
            LoadReport(null, null, viewOnly: true);
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DateTime? startDate = null;
            DateTime? endDate = null;

            if (DateTime.TryParse(txtStartDate.Text, out DateTime sDate) &&
                DateTime.TryParse(txtEndDate.Text, out DateTime eDate))
            {
                startDate = sDate;
                endDate = eDate;
            }

            // Load and export report as PDF
            LoadReport(startDate, endDate, viewOnly: false);
        }

        private void LoadReport(DateTime? startDate, DateTime? endDate, bool viewOnly)
        {
            string connectionString = @"Data Source=LAPTOP-EN5BMMJI\SQLEXPRESS;Initial Catalog=simple;Integrated Security=True";
            string query;

            SqlCommand cmd;

            if (startDate.HasValue && endDate.HasValue)
            {
                query = "SELECT * FROM reglogin WHERE new_registration_date BETWEEN @startDate AND @endDate";
                cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@startDate", startDate.Value);
                cmd.Parameters.AddWithValue("@endDate", endDate.Value);
            }
            else
            {
                query = "SELECT * FROM reglogin";
                cmd = new SqlCommand(query);
            }

            using (SqlConnection cn = new SqlConnection(connectionString))
            {
                cmd.Connection = cn;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                ad.Fill(ds, "reglogin");

                if (ds.Tables["reglogin"].Rows.Count > 0)
                {
                    CrystalReport1 rpt = new CrystalReport1();
                    rpt.SetDataSource(ds);

                    string rangeText = startDate.HasValue && endDate.HasValue
                        ? $"From {startDate:yyyy-MM-dd} To {endDate:yyyy-MM-dd}"
                        : "All Records";

                    // Ensure your CrystalReport1.rpt has a string parameter named "DateRange"
                    rpt.SetParameterValue("DateRange", rangeText);

                    if (viewOnly)
                    {
                        CrystalReportViewer1.ReportSource = rpt;
                        CrystalReportViewer1.DataBind();
                    }
                    else
                    {
                        rpt.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Registered_Students");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        "alert('No data found for the selected range.');", true);
                }
            }
        }
    }
}
