<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regesteredstudentlist.aspx.cs" Inherits="project_tryal.regesteredstudentlist" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student Registration Report</title>
    <style>
        body {
            background-color: #2f323a;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
            color: white;
        }

        .container {
            max-width: 800px;
            margin: 40px auto;
            padding: 20px;
            background-color: #3a3d45;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.3);
        }

        h2 {
            text-align: center;
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 20px;
            color: white;
        }

        .filter-container {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-bottom: 20px;
            gap: 10px;
        }

        .date-filter {
            display: flex;
            flex-direction: column;
            flex: 1;
            min-width: 150px;
        }

        label {
            font-weight: bold;
            margin-bottom: 5px;
        }

        .form-control {
            padding: 8px;
            border-radius: 5px;
            border: none;
            background-color: #1e1e1e;
            color: white;
        }

        .btn-primary,
        .btn-default,
        .btn-success {
            padding: 10px 20px;
            border-radius: 6px;
            font-weight: 700;
            cursor: pointer;
            font-size: 16px;
            border: none;
            transition: background-color 0.3s;
            margin-right: 10px;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .btn-primary:hover {
            background-color: #0069d9;
        }

        .btn-default {
            background-color: #6c757d;
            color: white;
        }

        .btn-default:hover {
            background-color: #5a6268;
        }

        .btn-success {
            background-color: #28a745;
            color: white;
        }

        .btn-success:hover {
            background-color: #218838;
        }

        .crystalReportViewer {
            margin-top: 20px;
            background-color: white;
            color: black;
            border-radius: 8px;
            padding: 10px;
        }

        /* Hide only the "To" date field and its calendar dropdown */
        #txtEndDate {
            display: none !important;
        }
        
        /* Hide the label for "To" date field */
        label[for="txtEndDate"] {
            display: none !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Student Registration Report</h2>

            <div class="filter-container">
                <div class="date-filter">
                    <label for="txtStartDate">From:</label>
                    <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="date-filter">
                    <label for="txtEndDate">To:</label>
                    <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                </div>
            </div>

            <div class="filter-container">
                <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilter_Click" CssClass="btn-primary" Visible="false" />
                <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" CssClass="btn-default" />
                <asp:Button ID="btnDownload" runat="server" Text="Download PDF" OnClick="btnDownload_Click" CssClass="btn-success" />
            </div>

            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                AutoDataBind="true" ToolPanelView="None" 
                HasCrystalLogo="False" HasToggleGroupTreeButton="False"
                CssClass="crystalReportViewer" />
        </div>
    </form>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            const startDate = document.getElementById('<%= txtStartDate.ClientID %>');
            const endDate = document.getElementById('<%= txtEndDate.ClientID %>');
            if (startDate && endDate) {
                startDate.addEventListener('change', function () {
                    this.value = this.value.split('T')[0];
                });
                endDate.addEventListener('change', function () {
                    this.value = this.value.split('T')[0];
                });
            }
        });
    </script>
</body>
</html>