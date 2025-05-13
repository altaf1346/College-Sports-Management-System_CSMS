<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="project_tryal.register" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tournament Registration</title>
    <!-- Bootstrap 5 -->
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- FontAwesome -->
    <link href="fontawesome/css/all.css" rel="stylesheet" />
    <style>
        body {
            display: flex;
            align-items: center;
            justify-content: center;
            min-height: 100vh;
            margin: 0;
            background-color: #424242;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .registration-container {
            width: 380px;
            padding: 30px;
            background-color: #f5f5f5;
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.3);
            border: 1px solid #333;
        }

        .scoreboard-header {
            color: #2E7D32;
            text-align: center;
            font-size: 26px;
            font-weight: 700;
            margin-bottom: 30px;
            padding-bottom: 12px;
            border-bottom: 3px solid #4CAF50;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .form-control {
            margin-bottom: 20px;
            border: 2px solid #ddd;
            padding: 12px;
            border-radius: 6px;
            font-size: 15px;
        }

        .btn-primary {
            background-color: #4CAF50;
            border: none;
            padding: 12px 20px;
            font-weight: 600;
            letter-spacing: 0.5px;
            border-radius: 6px;
            width: 100%;
        }

        .btn-primary:hover {
            background-color: #388E3C;
            transform: translateY(-1px);
        }

        .home-link {
            color: #FFD700;
            text-decoration: none;
            font-size: 14px;
            transition: color 0.3s;
            font-weight: 500;
            display: block;
            margin-top: 20px;
            text-align: center;
        }

        .home-link:hover {
            color: #FFC000;
            text-decoration: underline;
        }

        .fa-arrow-left {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <div class="registration-container">
        <form id="form1" runat="server">
            <div class="scoreboard-header">Student Register</div>
            
            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
            <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
            <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
            <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="Re-enter Password" TextMode="Password"></asp:TextBox>
            
            <asp:Button ID="Button1" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="Button1_Click" />
            
            <a href="Mlogin.aspx" class="home-link">
                <i class="fas fa-arrow-left"></i>Back to Login
            </a>
        </form>
    </div>

    <script src="bootstrap/js/bootstrap.bundle.min.js"></script>
</body>
</html>