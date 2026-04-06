<%@ Page Title="Year-End Update" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="YearEndUpdate.aspx.cs" Inherits="YearEndUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="container mt-4">
    <h3>Year-End Update (31 December)</h3>

     <asp:Button ID="btnYearEnd" runat="server" Text="Year End Process" 
    OnClick="btnYearEnd_Click" CssClass="btn btn-primary" />

    <br /><br />
    <asp:Label ID="lblMsg" runat="server" CssClass="fw-bold text-success"></asp:Label>
</div>

  
     <asp:GridView ID="gvClients" runat="server" AutoGenerateColumns="false" 
    CssClass="table table-bordered table-striped">
    <Columns>
       
        <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
          <asp:BoundField DataField="TotalDR" HeaderText="Total DR" />
        <asp:BoundField DataField="TotalCR" HeaderText="Total CR" />
      
        <asp:BoundField DataField="DRInterest" HeaderText="DR Interest" />
        <asp:BoundField DataField="CRInterest" HeaderText="CR Interest" />
        <asp:BoundField DataField="FinalTotal" HeaderText="Final Total" />
    </Columns>
</asp:GridView>
</asp:Content>