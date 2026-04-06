<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Login</title>

    <!-- Bootstrap -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            background: linear-gradient(to right, #4facfe, #00f2fe);
            height: 100vh;
        }
        .login-box {
            margin-top: 100px;
            padding: 30px;
            background: white;
            border-radius: 10px;
            box-shadow: 0px 0px 10px gray;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-md-4 login-box">

                    <h3 class="text-center mb-4">Admin Login</h3>

                    <div class="mb-3">
                        <asp:TextBox ID="txtUser" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <asp:TextBox ID="txtPass" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password"></asp:TextBox>
                    </div>

                    <div class="d-grid">
                        <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
                    </div>

                    <br />

                    <asp:Label ID="lblMsg" runat="server" CssClass="text-danger"></asp:Label>

                </div>
            </div>
        </div>
    </form>
</body>
</html>