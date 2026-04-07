<%@ Page Title="Transaction List" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TransactionList.aspx.cs" Inherits="TransactionList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
<div class="container mt-4">
    <h3>Transaction List</h3>
      <div class="container">
            <div class="row">

            
         <div class="col-lg-3">
    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
              <cc1:AutoCompleteExtender ID="AutoCompleteExtProduct" runat="server" ServicePath="~/sellproduct.asmx"
                                            ServiceMethod="Searchbyname" CompletionInterval="10" CompletionListItemCssClass="AutoCompliteItem"
                                            CompletionListCssClass="AutoComplite" CompletionListHighlightedItemCssClass="AutoCompliteSelectedItem"
                                            FirstRowSelected="true" MinimumPrefixLength="1" TargetControlID="txtname">
                                        </cc1:AutoCompleteExtender>
</div>

<div class="col-lg-3">
    <asp:TextBox ID="txtmobile" runat="server"  Visible="false" CssClass="form-control"></asp:TextBox>
</div>

            <div class="col-lg-3">

            <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-danger"  OnClick="Button1_Click"/>

            <asp:Label ID="lblpid" runat="server" Visible="false"></asp:Label>

        </div></div>
        </div>
    <asp:GridView ID="gvTransactions" runat="server" CssClass="table table-bordered table-striped"
        AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
        OnPageIndexChanging="gvTransactions_PageIndexChanging"
  
        OnRowDeleting="gvTransactions_RowDeleting"
        DataKeyNames="ID">

        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="true" Visible="false" />
            <asp:BoundField DataField="Name" HeaderText="Party Name" />
            <asp:BoundField DataField="TypeText" HeaderText="Type" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="₹ {0:0.00}" />
            <asp:BoundField DataField="InterestAmount" HeaderText="Interest" DataFormatString="₹ {0:0.00}" />
            <asp:BoundField DataField="TransactionFromDate" HeaderText="Date" DataFormatString="{0:dd-MMM-yyyy}" />
               <asp:BoundField DataField="IsYearEndstatus" HeaderText="Status" />
            <asp:CommandField  ShowDeleteButton="true" />
        </Columns>
    </asp:GridView>

    <div class="mt-3" runat="server" visible="false">
        <strong>Total Debit (Principal+Interest):</strong> <asp:Label ID="lblDebit" runat="server"></asp:Label><br />
        <strong>Total Credit (Received):</strong> <asp:Label ID="lblCredit" runat="server"></asp:Label><br />
        <strong>Total Interest:</strong> <asp:Label ID="lblInterest" runat="server"></asp:Label><br />
        <strong>Profit / Loss:</strong> <asp:Label ID="lblProfit" runat="server"></asp:Label>
    </div>
</div>

</asp:Content>