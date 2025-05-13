<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="project_tryal.login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login | CSMS</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />

    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    
    <!-- Custom Styles -->
    <style>
        body {
            background: linear-gradient(to right, #232526, #414345);
            font-family: 'Arial', sans-serif;
        }
        .login-container {
            max-width: 400px;
            background: #fff;
            padding: 30px;
            border-radius: 12px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.2);
            text-align: center;
            animation: fadeIn 1s ease-in-out;
        }
        .form-control {
            border-radius: 30px;
            padding: 12px;
            font-size: 16px;
        }
        .btn-login {
            border-radius: 30px;
            font-size: 18px;
            font-weight: bold;
            background: #28a745;
            border: none;
            transition: 0.3s ease-in-out;
        }
        .btn-login:hover {
            background: #218838;
        }
        .home-link {
            display: block;
            margin-top: 10px;
            text-decoration: none;
            color: #007bff;
            font-weight: bold;
            transition: 0.3s ease-in-out;
        }
        .home-link:hover {
            color: #0056b3;
        }
        @keyframes fadeIn {
            from {
                opacity: 0;
                transform: translateY(-20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }
    </style>
</head>
<body class="d-flex justify-content-center align-items-center vh-100">

    <form id="form1" runat="server">
        <div class="login-container">
            <!-- Logo & Title -->
            <img src="images/csmslogo.png" class="rounded-circle mb-3" alt="CSMS" height="80" width="80" />
            <h3 class="mb-4">Admin Login</h3>

            <!-- Username Field -->
            <div class="mb-3">
                <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server" placeholder="Enter Username"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="TextBox1" ErrorMessage="Username is required" ForeColor="Red" />
            </div>

            <!-- Password Field -->
            <div class="mb-3">
                <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server" TextMode="Password" placeholder="Enter Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="TextBox2" ErrorMessage="Password is required" ForeColor="Red" />
            </div>

            <!-- Login Button -->
            <asp:Button ID="Button1" CssClass="btn btn-login btn-block w-100" runat="server" Text="Login" OnClick="Button1_Click" />

            <!-- Home Link -->
            <a href="Mlogin.aspx" class="home-link">← Back to Login</a>
        </div>
    </form>

    <!-- Bootstrap 5 JS -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
