<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- jQuery library -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <style>
        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: red;
            color: white;
            text-align: center;
        }
    </style>

        <script type = "text/javascript" >
            function preventBack() { window.history.forward(); }
            setTimeout("preventBack()", 0);
            window.onunload = function () { null };
    </script>

     <script type = "text/javascript" >
         function ShowMessage() {
             var totalCount = 5;
            
               
                 var num = Math.ceil(Math.random() * totalCount);
          //  document.body.background = 'bg/' + num + '.jpg';
              document.body.background = 'https://picsum.photos/964/643?random=2';
             
                 document.body.style.backgroundRepeat = "repeat";// Background repeat
          
         }

         window.onload = ShowMessage;
            </script>

</head>

<body  onload="ShowMessage()" style=" background-size: cover;">
    <form id="form1" runat="server">
        <div style="">


            <br />
            <br />
            <br />
            <br />
            <br />
            <div class="content-wrapper">
                <div class="container">
                    <div class="row">
                    </div>
                    <div class="row">
                        <div class="col-md-4"></div>
                        <div class="col-md-4">

                            <div class="panel panel-default" style="border-top: solid 1px #ff6a00; border-bottom: solid 1px #ffd800; background-color: rgba(11, 15, 47,0.4)">
                                <div class="panel-heading" style="background-color: rgba(11, 15, 47,0.2); color: white">
                                    <h2 style="text-align: center">Admin Login</h2>
                                </div>
                                <div class="panel-body">
                                    <form>
                                        <div class="form-group">
                                            <label for="exampleInputEmail1" style="color: white">User Name</label>
                                            <asp:TextBox ID="Txtusername" runat="server" class="form-control" placeholder="Enter Username"></asp:TextBox>

                                        </div>
                                        <div class="form-group">
                                            <label for="exampleInputPassword1" style="color: white">Password</label>
                                            <asp:TextBox ID="Txtpassword" runat="server" type="password" class="form-control" placeholder="Enter Password"></asp:TextBox>

                                        </div>



                                        <div style="text-align: center">
                                            <asp:Label ID="LblErr" runat="server" ForeColor="red"></asp:Label>
                                            <br />
                                            <asp:Button ID="Button1" runat="server" Text="Log In" class="btn btn-danger" Style="text-align: center !important" OnClick="Button1_Click" />
                                           
                                        </div>

                                    </form>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
