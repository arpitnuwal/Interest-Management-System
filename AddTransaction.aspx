<%@ Page Title="Add Transaction" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddTransaction.aspx.cs" Inherits="AddTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
       <style>
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
    </style>
<script>
    $(function () {
        $("#<%= txtDate.ClientID %>").datepicker({ dateFormat: "dd-mm-yy" });
    });

    $(function () {
        $("#<%= txtCloseDate.ClientID %>").datepicker({ dateFormat: "dd-mm-yy" });
      });
</script>
    
<div class="container mt-4">
  
    <h3>Add Transaction</h3>

    <div class="row mb-3">
        <div class="col-md-4">
            <label>Party</label>
            <asp:Label ID="lblpid" runat="server" Text="Label"></asp:Label>
            <asp:TextBox ID="txtname" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtname_TextChanged" placeholder="Enter Name"></asp:TextBox>
              <cc1:AutoCompleteExtender ID="AutoCompleteExtProduct" runat="server" ServicePath="~/sellproduct.asmx"
                                            ServiceMethod="Searchbyname" CompletionInterval="10" CompletionListItemCssClass="AutoCompliteItem"
                                            CompletionListCssClass="AutoComplite" CompletionListHighlightedItemCssClass="AutoCompliteSelectedItem"
                                            FirstRowSelected="true" MinimumPrefixLength="1" TargetControlID="txtname">
                                        </cc1:AutoCompleteExtender>
       
        </div>
        <div class="col-md-4">
            <label>Type</label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                <asp:ListItem Text="Debit (udhar diya)" Value="1"></asp:ListItem>
                <asp:ListItem Text="Credit (Vapis Jama)" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label>Amount</label>
            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-4">
            <label>Interest % (Monthly)</label>
            <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control" ></asp:TextBox>
        </div>
        <div class="col-md-4">
            <label>Transaction Date</label>
            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
           <div class="col-md-4">
               <asp:Label ID="lblclosedateitle" runat="server" Text="Transaction Close Date"></asp:Label>
           
            <asp:TextBox ID="txtCloseDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <asp:Button ID="btnSave" runat="server" Text="Save Transaction" CssClass="btn btn-success" OnClick="btnSave_Click" />

    <br /><br />

    <asp:Label ID="lblMsg" runat="server" CssClass="text-success"></asp:Label>

</div>

</asp:Content>