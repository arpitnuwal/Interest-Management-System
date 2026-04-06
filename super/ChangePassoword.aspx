<%@ Page Title="" Language="C#" MasterPageFile="Admin.master" AutoEventWireup="true" CodeFile="ChangePassoword.aspx.cs" Inherits="ChangePassoword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <style type="text/css">
        .messagealert
        {
            width: 100%;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');
        }
    </script>
    <script type="text/javascript">
        window.setTimeout(function () {
            $("#alert_container").fadeTo(500, 0).slideUp(500, function () {
                $(this).remove();
            });
        }, 4000);
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="messagealert" id="alert_container">
    </div>

    

     <div class="row">
            <div class="col-md-12">
                <h1 class="page-head-line">Change Password</h1>
            </div>
        </div>
 <br />
    <div class="row">
                <div class="col-lg-6">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Password Form Elements
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <form role="form">
                                       <div class="form-group has-success">
                                            <label class="control-label" for="inputSuccess">Old Password</label>
                                           <asp:TextBox ID="Txtold" runat="server" class="form-control" required="required" Enabled="false"></asp:TextBox>
                                       
                                        </div>
                                        <div class="form-group has-success">
                                            <label class="control-label" for="inputSuccess">New Password</label>
                                           <asp:TextBox ID="Txtnews" runat="server" class="form-control"></asp:TextBox>
                                       
                                        </div>
                                      
                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" class="btn btn-success" OnClick="btnsubmit_Click"/>
                                          <asp:Button ID="btncanel" runat="server" Text="Reset" class="btn btn-danger"/>
                                      
                                    </form>
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                                
                                <!-- /.col-lg-6 (nested) -->
                            </div>
                            <!-- /.row (nested) -->
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <!-- /.col-lg-12 -->

        <div class="col-lg-6">
            
            </div> 
         </div>
</asp:Content>

