<%@ Page Title="" Language="C#" MasterPageFile="~/super/Admin.master" AutoEventWireup="true" CodeFile="user.aspx.cs" Inherits="user" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
       <style>
        .MainSearchBar {
            color: #333333;
            padding: 3px;
            margin-right: 4px;
            margin-bottom: 8px;
            font-family: tahoma, arial, sans-serif;
            background-image: url(images/SearchImg.jpg);
            background-repeat: repeat-x;
            border: 1px solid #d2d2ce;    z-index: 1000;
        }

        .AutoComplite {
            width: 326px;
            background-color: white;
            margin: 0;
            padding: 0;
            color: #A4A4A4;    z-index: 1000;
        }

        .AutoCompliteItem {
            font-size: 12px;
            height: 25px;
            background-color: white;
            width: 320px;
            overflow: hidden;
            color: Black;
            border-top-style: dotted;
            border-right-style: groove;
            border-bottom-style: dotted;
            border-left-style: solid;
            border-color: #d2d2d2;    z-index: 1000;
            border-width: 1px;
        }

        .AutoCompliteSelectedItem {
            font-size: 12px;
            height: 25px;
            color: #E80732;
            font-weight: bold;
            background-color: #eaeaea;
            width: 320px;
            overflow: hidden;
            padding-top: 5px;    z-index: 1000;
        }
    </style>

    <style type="text/css">
        .messagealert {
            width: 100%;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
         .ajax__calendar_container {
            z-index: 1000;
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

    <style>
        label {
  display: inline-block;
  max-width: 100%;
  margin-bottom: 0px;
  font-weight: 700;
}
        .form-group {
  margin-bottom: 2px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container-fluid" style="margin-top:-40px">
       
        <div class="row">

        
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Customer Forms
                    </div>
                   
                            <div class="panel-body"  style="margin-top:-25px">
                        <form role="form">
                             <div class="col-md-2" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Loan Date</label>
                                   <asp:TextBox ID="txtloandate" runat="server" class="form-control"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtloandate" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                  
                                </div>
                            </div>
                           
                           
                            <div class="col-md-12">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Name</label>
                                    <asp:TextBox ID="txtname" runat="server" class="form-control"  AutoComplete="off"  Style="font-size:17px;text-transform: uppercase;"></asp:TextBox>

                                </div>
                            </div>
                         
                            <div class="col-md-2" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Mobile Number</label>
                                    <asp:TextBox ID="txtmobile" runat="server" MaxLength="10" onkeypress='return ((event.charCode >= 48 && event.charCode <= 57)||event.keyCode == 8||event.keyCode == 9)' class="form-control"></asp:TextBox>

                                </div>
                            </div>
                            
                         
                           
                           
                              
                      
                            <div class="col-md-2" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Loan Amount (Rs.)</label>
                                    <asp:TextBox ID="txtloanamount"  Text="00" runat="server" onkeypress='return ((event.charCode >= 48 && event.charCode <= 57)||event.keyCode == 8||event.keyCode == 9)' class="form-control"></asp:TextBox>

                                </div>
                            </div>
                             <div class="col-md-2" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Kist Amount</label>
                                    <asp:TextBox ID="txtkistamount"   Text=""   runat="server" onkeypress='return ((event.charCode >= 48 && event.charCode <= 57)||event.keyCode == 8||event.keyCode == 9)' class="form-control" ></asp:TextBox>
                                  
                                </div>
                            </div>
                              <div class="col-md-1" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Interest Rate</label>
                                    <asp:TextBox ID="txtinterestrate"     runat="server"  class="form-control" ></asp:TextBox>
                                   
                                </div>
                            </div>
                            <div class="col-md-1" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Kist Number</label>
                                    <asp:TextBox ID="txtKistNumber"    runat="server" onkeypress='return ((event.charCode >= 48 && event.charCode <= 57)||event.keyCode == 8||event.keyCode == 9)' class="form-control"></asp:TextBox>
                                  
                                </div>
                            </div>
                          
                         
                         
                          

                             
                                <div class="col-md-12">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Description</label>
                                    <asp:TextBox ID="txtdescription"  runat="server" class="form-control" Style="font-size:17px;text-transform: uppercase;" TextMode="MultiLine" Rows="35"></asp:TextBox>

                                </div>
                            </div>
                           <div class="col-md-12">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Address</label>
                                    <asp:TextBox ID="txtaddress"  runat="server" class="form-control" Style="font-size:17px;text-transform: uppercase;" TextMode="MultiLine"  Rows="35"></asp:TextBox>

                                </div>
                            </div>
                               
                            
                             <div class="col-md-3" runat="server" visible="true">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Photo</label>
                                    <asp:FileUpload ID="customerphoto" class="form-control" runat="server" />

                                </div>
                            </div>
                             <div class="col-md-3" runat="server" visible="true">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Photo Other</label>
                                    <asp:FileUpload ID="aadharphoto" class="form-control" runat="server" />

                                </div>
                            </div>
                          
                              <div class="col-md-8"  runat="server" visible="false">

                                    <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover">
                                    
                                       
                                            <thead>
                                                <tr>

                                                    <td>
                                                        <b>Id</b>
                                                    </td>
                                                    <td>
                                                        <b>Relative Name</b>
                                                    </td>
                                                    <td>
                                                        <b>Mobile No.</b>
                                                    </td>                                              
                                                </tr>
                                                <tr>

                                                    <td>
                                                        <b>1</b>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:TextBox ID="txtname2" runat="server" class="form-control" placeholder="Enter Relative Name " ></asp:TextBox>
                                                        </b>
                                                    </td>
                                                    <td>
                                                         <b>
                                                            <asp:TextBox ID="txtmobile2" runat="server" class="form-control" placeholder="Enter Relative Mobile No. " ></asp:TextBox>
                                                        </b>
                                                    </td>                                              
                                                </tr>
                                                <tr>

                                                    <td>
                                                        <b>2</b>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:TextBox ID="txtname3" runat="server" class="form-control" placeholder="Enter Relative Name " ></asp:TextBox>
                                                        </b>
                                                    </td>
                                                    <td>
                                                         <b>
                                                            <asp:TextBox ID="txtmobile3" runat="server" class="form-control" placeholder="Enter Relative Mobile No. " ></asp:TextBox>
                                                        </b>
                                                    </td>                                              
                                                </tr>

                                                <tr>

                                                    <td>
                                                        <b>3</b>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:TextBox ID="txtname4" runat="server" class="form-control" placeholder="Enter Relative Name " ></asp:TextBox>
                                                        </b>
                                                    </td>
                                                    <td>
                                                         <b>
                                                            <asp:TextBox ID="txtmobile4" runat="server" class="form-control" placeholder="Enter Relative Mobile No. " ></asp:TextBox>
                                                        </b>
                                                    </td>                                              
                                                </tr>

                                                <tr>

                                                    <td>
                                                        <b>4</b>
                                                    </td>
                                                    <td>
                                                        <b>
                                                            <asp:TextBox ID="txtname5" runat="server" class="form-control" placeholder="Enter Relative Name " ></asp:TextBox>
                                                        </b>
                                                    </td>
                                                    <td>
                                                         <b>
                                                            <asp:TextBox ID="txtmobile5" runat="server" class="form-control" placeholder="Enter Relative Mobile No. " ></asp:TextBox>
                                                        </b>
                                                    </td>                                              
                                                </tr>
                                            </thead>
                                        

                                        
                                    
                                    
                                </table>

                            </div>
                               </div>
                            <div class="col-md-4"></div>


                              <div class="col-md-4">
                            <asp:Label ID="LblErr" runat="server" ForeColor="Red"></asp:Label><br />
                            <asp:Button ID="Button1" runat="server"  Class="btn btn-danger" Text="Submit"  Style="border-style: none;background-color:#2c8bb6; padding: 5px" OnClick="Button1_Click" />
                                  </div>

                        </form>
                        <hr />


                    </div>
                       
                    
                </div>
           
            <asp:HiddenField ID="LblId" runat="server" />

        </div>
        
    </div>
</asp:Content>

