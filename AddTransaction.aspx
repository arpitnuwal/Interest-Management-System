<%@ Page Title="Add Transaction" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddTransaction.aspx.cs" Inherits="AddTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>

<script>
    $(function () {
        $("#<%= txtDate.ClientID %>").datepicker({ dateFormat: "yy-mm-dd" });
    });
</script>

<div class="container mt-4">

    <h3>Add Transaction</h3>

    <div class="row mb-3">
        <div class="col-md-4">
            <label>Party</label>
            <asp:DropDownList ID="ddlParty" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>
        <div class="col-md-4">
            <label>Type</label>
            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                <asp:ListItem Text="Debit (Diya)" Value="1"></asp:ListItem>
                <asp:ListItem Text="Credit (Liya)" Value="2"></asp:ListItem>
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
            <asp:TextBox ID="txtInterest" runat="server" CssClass="form-control" Text="2"></asp:TextBox>
        </div>
        <div class="col-md-4">
            <label>Transaction Date</label>
            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    </div>

    <asp:Button ID="btnSave" runat="server" Text="Save Transaction" CssClass="btn btn-success" OnClick="btnSave_Click" />

    <br /><br />

    <asp:Label ID="lblMsg" runat="server" CssClass="text-success"></asp:Label>

</div>

</asp:Content>