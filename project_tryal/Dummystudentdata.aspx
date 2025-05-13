<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dummystudentdata.aspx.cs" Inherits="project_tryal.WebForm1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Report</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap & External Scripts -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js"></script>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <!-- Dark Theme Custom CSS -->
    <style>
        body {
            background-color: #2c2f36;
            color: white;
            font-family: 'Segoe UI', sans-serif;
            padding-top: 50px;
            min-height: 100vh;
        }

        .registration-container {
            width: 100%;
            max-width: 700px;
            margin: auto;
            background-color: #2c2f36;
            padding: 40px;
            border-radius: 8px;
        }

        h2 {
            text-align: center;
            margin-bottom: 30px;
            font-weight: bold;
            color: white;
            text-transform: uppercase;
        }

        .form-group label {
            font-weight: 500;
            color: #ffffff;
        }

        .form-control {
            background-color: black;
            color: white;
            border: 1px solid #444;
        }

        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            color: white;
            font-weight: bold;
            text-transform: uppercase;
            width: 100%;
        }

        .btn-primary:hover {
            background-color: #006fe6;
            border-color: #006fe6;
        }

        .crystal-container {
            margin-top: 30px;
            background-color: #1e1f23;
            padding: 15px;
            border-radius: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="registration-container">
            <h2>Student List</h2>

            <div class="form-group">
                <label for="DropDownList1">Select Course</label>
                <asp:DropDownList ID="DropDownList1" runat="server"
                    DataSourceID="SqlDataSource1"
                    DataTextField="courseid"
                    DataValueField="courseid"
                    CssClass="form-control">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Button ID="Button1" runat="server"
                    Text="View Report"
                    CssClass="btn btn-primary"
                    OnClick="Button1_Click1" />
            </div>

            <div class="crystal-container">
                <CR:CrystalReportViewer ID="CrystalReportViewer1"
                    runat="server"
                    AutoDataBind="true"
                    OnInit="CrystalReportViewer1_Init"
                    ToolPanelView="None"
                    HasCrystalLogo="False"
                    CssClass="w-100" />
            </div>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:newdbConnectionString %>"
                SelectCommand="SELECT DISTINCT [courseid] FROM [dummystudenttable]">
            </asp:SqlDataSource>
        </div>
    </form>
</body>
</html>
