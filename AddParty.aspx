<%@ Page Title="Add Party" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AddParty.aspx.cs" Inherits="AddParty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="container mt-4">

    <h3 class="mb-3">Add Party</h3>

    <div class="row mb-3">
        <div class="col-md-4">
            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
        </div>

        <div class="col-md-4">
            <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Mobile"></asp:TextBox>
        </div>

        <div class="col-md-4">
            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address"></asp:TextBox>
        </div>
    </div>

    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />

    <br /><br />

    <asp:Label ID="lblMsg" runat="server" CssClass="text-success"></asp:Label>

    <hr />

    <h4>Party List</h4>

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"
        CssClass="table table-bordered table-striped"
        DataKeyNames="PartyID"
        AllowPaging="true" PageSize="5"

        OnPageIndexChanging="GridView1_PageIndexChanging"
        OnRowEditing="GridView1_RowEditing"
        OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowUpdating="GridView1_RowUpdating"
        OnRowDeleting="GridView1_RowDeleting">

        <Columns>
            <asp:BoundField DataField="PartyID" HeaderText="ID" ReadOnly="true" />

            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
            <asp:BoundField DataField="Address" HeaderText="Address" />

            <asp:CommandField ShowEditButton="true" ShowDeleteButton="true" />

        </Columns>

    </asp:GridView>

</div>

</asp:Content>