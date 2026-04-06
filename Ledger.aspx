<%@ Page Title="Ledger" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Ledger.aspx.cs" Inherits="Ledger" %>
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
    <h3 class="mb-3 text-center">Client Ledger Balance Sheet</h3>
        <div class="container">
            <div class="row">

            
         <div class="col-lg-3">
    <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>
              <cc1:AutoCompleteExtender ID="AutoCompleteExtProduct" runat="server" ServicePath="~/sellproduct.asmx"
                                            ServiceMethod="Searchbyname" CompletionInterval="10" CompletionListItemCssClass="AutoCompliteItem"
                                            CompletionListCssClass="AutoComplite" CompletionListHighlightedItemCssClass="AutoCompliteSelectedItem"
                                            FirstRowSelected="true" MinimumPrefixLength="1" TargetControlID="txtname">
                                        </cc1:AutoCompleteExtender>
</div>

<div class="col-lg-3">
    <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control"></asp:TextBox>
</div>

            <div class="col-lg-3">

            <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-danger"  OnClick="Button1_Click"/>

            <asp:Label ID="lblpid" runat="server" Visible="false"></asp:Label>

        </div></div>
        </div>
   <br /><br />


        

        
    <asp:GridView ID="gvLedger" runat="server"
        CssClass="table table-bordered table-hover text-center"
        AutoGenerateColumns="false"
        AllowPaging="true"
        PageSize="10"
    
      
     
        DataKeyNames="ID">

        <HeaderStyle CssClass="table-dark" />
        <RowStyle CssClass="align-middle" />

        <Columns>

            <asp:BoundField DataField="TransactionFromDate"
                HeaderText="Date"
                DataFormatString="{0:dd-MMM-yyyy}" />

            <asp:BoundField DataField="TypeName"
                HeaderText="Particular" />

            <asp:TemplateField HeaderText="Debit (DR)">
                <ItemTemplate>
                    <%# Eval("TypeName").ToString() == "Debit"
                        ? "₹ " + Convert.ToDecimal(Eval("Amount")).ToString("0.00")
                        : "-" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Credit (CR)">
                <ItemTemplate>
                    <%# Eval("TypeName").ToString() == "Credit"
                        ? "₹ " + Convert.ToDecimal(Eval("Amount")).ToString("0.00")
                        : "-" %>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="InterestAmount"
                HeaderText="Interest"
                DataFormatString="₹ {0:0.00}" />

        

           

        </Columns>
    </asp:GridView>

    <div class="card mt-3 p-3 shadow-sm">
        <asp:Label ID="lblClosingBalance" runat="server"
            CssClass="fw-bold fs-5 text-primary"></asp:Label>

        <br />

        <asp:Button ID="btnDepositBalance" runat="server"
            CssClass="btn btn-success mt-2"
            Text="Deposit Closing Balance"
            OnClick="btnDepositBalance_Click" />
    </div>

    <asp:Label ID="lblMsg" runat="server"
        CssClass="fw-bold text-success mt-3 d-block"></asp:Label>
</div>
</asp:Content>