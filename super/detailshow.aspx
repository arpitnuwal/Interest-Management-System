<%@ Page Title="" Language="C#" MasterPageFile="~/super/Admin.master" AutoEventWireup="true" CodeFile="detailshow.aspx.cs" Inherits="detailshow" %>


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
            background-color: #E4E7ED;
            color: red;
            border: 1px solid #E4E7ED;
            -webkit-transition: 0.2s all;
            transition: 0.2s all;
        }

        .btn-btn-success:hover {
            -webkit-transition: 0.2s all;
            transition: 0.2s all;
            background-color: #E4E7ED;
            color: #D10024;
        }

        .btn-btn-success .active {
            background-color: #D10024;
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
            background-color: white;
            margin: 0;
            padding: 0;
            color: #A4A4A4;  z-index: 1000;
        }

        .AutoCompliteItem {
            font-size: 12px;
            height: 25px;
            background-color: white;
            width: 556px;
            overflow: hidden;
            color: Black;
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
            color: #E80732;
            font-weight: bold;
            background-color: #eaeaea;
          width: 556px;
            overflow: hidden;
            padding-top: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="messagealert" id="alert_container">
    </div>



    <div class="container-fluid">
        <div class="row">
            <div class="col-md-12">
                <h1 class="page-head-line">Customer List </h1>
            </div>
        </div>
        <br />
        <br />
        <br />
        <div class="row">

            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        Customer List
                    </div>
                    <div class="panel-body">

                        <div class="row">


                               <div class="col-md-6">
                                <div class="form-group has-success">
                                    <label class="control-label" for="success">Customer Name</label>
                                    <asp:TextBox ID="txtname" runat="server" class="form-control" AutoPostBack="true" OnTextChanged="txtname_TextChanged"></asp:TextBox>
                                      <cc1:AutoCompleteExtender ID="AutoCompleteExtProduct" runat="server" ServicePath="~/sellproduct.asmx"
                                            ServiceMethod="Searchbyname" CompletionInterval="10" CompletionListItemCssClass="AutoCompliteItem"
                                            CompletionListCssClass="AutoComplite" CompletionListHighlightedItemCssClass="AutoCompliteSelectedItem"
                                            FirstRowSelected="true" MinimumPrefixLength="1" TargetControlID="txtname">
                                        </cc1:AutoCompleteExtender>
                                </div>
                            </div>
                            
                            
                            <div class="col-md-6">
                             
                            </div>
                         
                            <div class="col-md-3">
                                
                            </div>
                            <div class="col-md-3">
                            </div>
                        </div>

                        
                        <hr />
                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>

