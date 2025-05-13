<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tournamentdetails.aspx.cs" Inherits="project_tryal.tournamentdetails" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tournament Details</title>
    <style type="text/css">
        body {
            font-family: Arial, sans-serif;
            background-color: #2b2b2b; /* Dark gray background */
            margin: 0;
            padding: 20px;
            color: white;
        }
        
        .report-header {
            color: white;
            text-align: center;
            font-size: 28px;
            font-weight: 800;
            margin: 30px 0;
            text-transform: uppercase;
            letter-spacing: 2px;
            text-shadow: 1px 1px 3px rgba(0,0,0,0.3);
        }
        
        .selection-container {
            width: 80%;
            max-width: 600px;
            margin: 0 auto;
            background-color: #333333; /* Slightly lighter gray */
            padding: 25px;
            border-radius: 8px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
            text-align: center;
        }
        
        .selection-label {
            display: block;
            margin-bottom: 15px;
            font-weight: 600;
            color: white;
            font-size: 16px;
        }
        
        .dropDownList {
            padding: 10px 15px;
            border-radius: 6px;
            border: 2px solid #444444;
            background-color: #2b2b2b; /* Matching body background */
            color: white;
            width: 70%;
            max-width: 350px;
            margin-bottom: 25px;
            font-size: 16px;
            font-weight: 500;
        }
        
        #Button1 {
            background-color: #3498db;
            color: white;
            border: none;
            padding: 12px 30px;
            border-radius: 6px;
            font-weight: 700;
            cursor: pointer;
            text-transform: uppercase;
            font-size: 16px;
            letter-spacing: 1px;
            transition: all 0.3s;
        }
        
        #Button1:hover {
            background-color: #2980b9;
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        }
        
        .crystalReportViewer {
            margin: 40px auto;
            width: 90%;
            background-color: #333333;
            border-radius: 8px;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.2);
            border: 2px solid #444444;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="report-header">TOURNAMENT DETAILS</div>
        
        <div class="selection-container">
            <span class="selection-label">Select Tournament Date:</span>
            <asp:DropDownList ID="DropDownList1" runat="server" 
                DataSourceID="SqlDataSource1" 
                DataTextField="tstartdate" 
                DataValueField="tstartdate" 
                CssClass="dropDownList">
            </asp:DropDownList>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="VIEW REPORT" />
        </div>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:newdbConnectionString %>" 
            SelectCommand="SELECT DISTINCT [tstartdate] FROM [addtournamenttable]">
        </asp:SqlDataSource>
        
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true" 
            OnInit="CrystalReportViewer1_Init" 
            CssClass="crystalReportViewer" />
    </form>
</body>
</html>