<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mlogin.aspx.cs" Inherits="project_tryal.Mlogin" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tournament Login</title>
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
            background-color: #424242; /* Dark gray background */
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .login-container {
            width: 350px;
            padding: 30px;
            background-color: #f5f5f5; /* Light gray container */
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.3);
            border: 1px solid #333;
        }

        .login-container h2 {
            color: #2E7D32; /* Dark green */
            text-align: center;
            font-size: 26px;
            font-weight: 700;
            margin-bottom: 30px;
            padding-bottom: 12px;
            border-bottom: 3px solid #4CAF50; /* Bright green */
            text-transform: uppercase;
        }

        .form-control {
            margin-bottom: 20px;
            border: 2px solid #ddd;
            padding: 12px;
            border-radius: 6px;
            font-size: 15px;
        }

        .btn-primary {
            background-color: #4CAF50; /* Green */
            border: none;
            padding: 12px 20px;
            font-weight: 600;
            border-radius: 6px;
        }

        .btn-primary:hover {
            background-color: #388E3C; /* Darker green */
        }

        .btn-secondary {
            background-color: #616161; /* Medium gray */
            border: none;
            padding: 12px 20px;
            font-weight: 600;
            border-radius: 6px;
        }

        .btn-secondary:hover {
            background-color: #424242; /* Dark gray */
        }

        .admin-link {
            color: #FFD700; /* Yellow */
            text-decoration: none;
            font-size: 14px;
            font-weight: 500;
        }

        .admin-link:hover {
            color: #FFC000; /* Darker yellow */
            text-decoration: underline;
        }

        .btn-container {
            display: flex;
            justify-content: space-between;
        }

        .fa-user-shield {
            margin-right: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="login-container">
        <h2>Student Login</h2>
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
        
        <div class="btn-container">
            <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Login" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="btn btn-secondary" Text="Register" OnClick="Button2_Click" />
        </div>
        
        <div style="text-align: right; margin-top: 20px;">
            <a href="login.aspx" class="admin-link">
                <i class="fas fa-user-shield"></i>Admin Login
            </a>
        </div>
    </form>

    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>