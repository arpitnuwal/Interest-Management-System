<%@ Page Title="" Language="C#" MasterPageFile="~/super/Admin.master" AutoEventWireup="true" CodeFile="onlynamesearch.aspx.cs" Inherits="onlynamesearch" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .messagealert {
            width: 100%;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <style>
        .mycheckbox input[type="checkbox"] {
            margin-right: 9px;
        }

        /*--------------Dynamic Show-----------*/
        .normal {
            color: #000000;
        }

        .red {
            color: #D10024;
        }

        .yellowRow {
            color: #F0770E;
        }

        .btn-btn-success {
            display: inline-block;
            width: 40px;
            height: 40px;
            line-height: 40px;
            text-align: center;
            background-color: darkgray;
            color: white;
            border: 1px solid #E4E7ED;
            -webkit-transition: 0.2s all;
            transition: 0.2s all;
        }

        .btn-btn-success11 {
            display: inline-block;
            width: 40px;
            height: 40px;
            line-height: 40px;
            text-align: center;
            background-color: #000000;
            color: red;
            border: 1px solid #E4E7ED;
            -webkit-transition: 0.2s all;
            transition: 0.2s all;
        }

        .btn-btn-success:hover {
            -webkit-transition: 0.2s all;
            transition: 0.2s all;
            background-color: #000000;
            color: #D10024;
        }

        .btn-btn-success .active {
            background-color: #000000;
            border-color: #000;
            color: #F0770E;
            font-weight: 500;
            cursor: default;
        }

        .btn-btn-success a {
            display: block;
        }

        .ajax__calendar_container {
            z-index: 1000;
        }
    </style>
    <script>
        function getNextElement(field) {
            var form = field.form;
            for (var e = 0; e < form.elements.length; e++) {
                if (field == form.elements[e]) {
                    break;
                }
            }
            return form.elements[++e % form.elements.length];
        }

        function tabOnEnter(field, evt) {
            if (evt.keyCode === 13) {
                if (evt.preventDefault) {
                    evt.preventDefault();
                } else if (evt.stopPropagation) {
                    evt.stopPropagation();
                } else {
                    evt.returnValue = false;
                }
                getNextElement(field).focus();
                return false;
            } else {
                return true;
            }
        }</script>
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
        }, 6000);
    </script><style>
        .MainSearchBar {
            color: #333333;
            padding: 3px;
            margin-right: 4px;
            margin-bottom: 8px;
            font-family: tahoma, arial, sans-serif;
            background-image: url(images/SearchImg.jpg);
            background-repeat: repeat-x;
            border: 1px solid #d2d2ce;
        }

        .AutoComplite {
            width: 556px;
            background-color: #000000;
            margin: 0;
            padding: 0;
            color: #ffffff;  z-index: 1000;
        }

        .AutoCompliteItem {
            font-size: 12px;
            height: 25px;
            background-color: #000000;
            width: 556px;
            overflow: hidden;
            color: white;
            border-top-style: dotted;
            border-right-style: groove;
            border-bottom-style: dotted;
            border-left-style: solid;
            border-color: #d2d2d2;
            border-width: 1px;
        }

        .AutoCompliteSelectedItem {
            font-size: 12px;
            height: 25px;
            color: #ffffff;
            font-weight: bold;
            background-color: #000000;
          width: 556px;
            overflow: hidden;
            padding-top: 5px;
        }
    </style>  <style type="text/css">
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

     <script language="javascript">
         function PrintMe(DivID) {
             var disp_setting = "toolbar=yes,location=no,";
             disp_setting += "directories=yes,menubar=yes,";
             disp_setting += "scrollbars=yes,width=800, height=600, left=100, top=25";
             var content_vlue = document.getElementById(DivID).innerHTML;
             var docprint = window.open("", "", disp_setting);
             docprint.document.open();
             docprint.document.write('<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN"');
             docprint.document.write('"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">');
             docprint.document.write('<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">');
             docprint.document.write('<head>');
             docprint.document.write('<link rel=\"stylesheet\" type=\"text/css\" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.1/dist/css/bootstrap.min.css\" />');

             docprint.document.write('<style type="text/css">');
             docprint.document.write(' body { font-size: 17px !important; font-weight:500;}table,.table, tr, td,th { border: 1px solid black; text-align: center; }  td {vertical-align: middle;} .table>:not(caption)>*>* { padding: 10px 2px; }');

             docprint.document.write('</style>');
             docprint.document.write('</head><body onLoad="self.print()"><center>');
             docprint.document.write(content_vlue);
             docprint.document.write('</center></body></html>');
             docprint.document.close();
             docprint.focus();
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="messagealert" id="alert_container">
    </div>



    <div class="container-fluid">
       
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-default">
                 
                    <div class="panel-body">

                        <div class="row">


                               <div class="col-md-10">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Name</label>
                                    <asp:TextBox ID="txtname" runat="server" class="form-control" Style="text-transform: uppercase;"></asp:TextBox>
                                      <cc1:AutoCompleteExtender ID="AutoCompleteExtProduct" runat="server" ServicePath="~/sellproduct.asmx"
                                            ServiceMethod="Searchbyname" CompletionInterval="10" CompletionListItemCssClass="AutoCompliteItem"
                                            CompletionListCssClass="AutoComplite" CompletionListHighlightedItemCssClass="AutoCompliteSelectedItem"
                                            FirstRowSelected="true" MinimumPrefixLength="1" TargetControlID="txtname">
                                        </cc1:AutoCompleteExtender>
                                </div>
                            </div>
                            
                            
                            <div class="col-md-4" runat="server" visible="false">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Mobile No.</label>
                                    <asp:TextBox ID="txtmobile" runat="server" class="form-control"></asp:TextBox>

                                </div>
                            </div>
                         
                            <div class="col-md-2">
                                <div class="form-group has-success">
                                    <br />
                                    <asp:Button runat="server" ID="BtnSearch" Text="Search" class="btn btn-success" OnClick="BtnSearch_Click" Style="margin-top: 5px; background-color: #2c8bb6; border-style: none;" />
                                </div>
                            </div>
                          
                                                            <%--<input type="button" name="btnprint" value="Print" onclick="PrintMe('divall')" style="background-color: #b13c00; color: white; padding: 5px; border: none" />--%>

                          
                        </div>
                         <div id="divall">
                        <div class="row" runat="server" id="imglist" style="margin-left:55px">
                            <div class="table-responsive">
                                <table class="table table-striped table-bordered table-hover">
                                    <asp:ListView ID="LstOrder" runat="server" OnItemCommand="LstOrder_ItemCommand" OnPagePropertiesChanging="LstOrder_PagePropertiesChanging"
                                        OnItemDataBound="LstOrder_ItemDataBound" OnItemDeleting="LstOrder_ItemDeleting">
                                        <LayoutTemplate>
                                            <thead>
                                                <tr>


                                                    <td>
                                                        <b>Customer Name</b>
                                                    </td>
                                                   
                                                    




                                                </tr>
                                            </thead>
                                            <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                                        </LayoutTemplate>

                                        <ItemTemplate>
                                            <tbody>
                                                <asp:Label ID="lblid" runat="server" Visible="false" Text='<%#Eval("id") %>'></asp:Label>

                                                <tr class="<%#Convert.ToString(setclass(Convert.ToBoolean(Eval("status")))) %>">


                                                   


                                                    <td style="font-size:16px">
                                                        <%# Eval("Name")%>
                                                    </td>
                                                   
                                                   



                                                    <%--    <td>
                                                        <a href="viewinstallment.aspx?userid=<%# Eval("id") %>" font-bold="true" class="btn-danger" style="padding: 4px 6px; text-decoration: none; background-color: #284160; border-radius: 5px; font-family: 'Times New Roman'"><i class="fa fa-eye"></i>View</a><br />
                                                        <br />


                                                    </td>--%>

                                                      <td>
                                                        <asp:LinkButton ID="LinkButton5" runat="server" CommandName="delete" OnClientClick="return confirm('delete this Customer?');" Font-Bold="true" CssClass="btn-success" Style="padding: 4px 6px; text-decoration: none; background-color: #018636; border-radius: 5px; font-family: 'Times New Roman'">Delete <i class="fa fa-trash"></i></asp:LinkButton>
                                                    </td>
                                                   
                                                    <td>
                                                        <a href="userupdate.aspx?id=<%#Eval("id") %>">Edit</a>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </ItemTemplate>
                                    </asp:ListView>
                                    <tr>
                                        <td colspan="12">
                                            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="LstOrder" PageSize="15">
                                                <Fields>
                                                    <asp:NumericPagerField NumericButtonCssClass="btn-btn-success" ButtonType="Button"
                                                        CurrentPageLabelCssClass="btn-btn-success11" NextPreviousButtonCssClass="btn-btn-success"
                                                        ButtonCount="10" NextPageText="Next &raquo;" PreviousPageText="&laquo; Previous" />
                                                    <%--<asp:NextPreviousPagerField ButtonType="Link" ShowNextPageButton="true" ShowLastPageButton="false" ShowPreviousPageButton="false" />--%>
                                                </Fields>
                                            </asp:DataPager>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
  </div>



                        <hr />
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>

