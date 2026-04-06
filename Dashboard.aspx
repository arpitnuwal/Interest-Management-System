<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h3 class="mb-4">Dashboard</h3>

<div class="row">

    <div class="col-md-4">
        <div class="bg-primary text-white p-3 rounded">
            <h5>Total Given</h5>
            <asp:Label ID="lblDebit" runat="server"></asp:Label>
        </div>
    </div>

    <div class="col-md-4">
        <div class="bg-success text-white p-3 rounded">
            <h5>Total Received</h5>
            <asp:Label ID="lblCredit" runat="server"></asp:Label>
        </div>
    </div>

    <div class="col-md-4">
        <div class="bg-warning text-white p-3 rounded">
            <h5>Total Interest</h5>
            <asp:Label ID="lblInterest" runat="server"></asp:Label>
        </div>
    </div>

</div>

</asp:Content>