<%@ Page Title="Profit / Loss" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ProfitLoss.aspx.cs" Inherits="ProfitLoss" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="https://code.jquery.com/ui/1.13.2/themes/base/jquery-ui.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script src="https://code.jquery.com/ui/1.13.2/jquery-ui.min.js"></script>
    <script>
        function printDiv() {
            var divContent = document.getElementById("printDiv").innerHTML;
            var originalContent = document.body.innerHTML;

            document.body.innerHTML = divContent;
            window.print();
            document.body.innerHTML = originalContent;
        }
</script>
<script>
    $(function () {
        $("#<%= txtDate.ClientID %>").datepicker({ dateFormat: "dd-mm-yy" });
    });

    $(function () {
        $("#<%= txtCloseDate.ClientID %>").datepicker({ dateFormat: "dd-mm-yy" });
    });
</script>
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
    <h3>Profit / Loss Detailed Report</h3>

    <!-- Party Selection -->
     <div class="container">
            <div class="row">
                  <asp:Label ID="lbllastbalance" runat="server" Visible="false" ></asp:Label>
            
         <div class="col-lg-3">
    <asp:TextBox ID="txtname" runat="server" CssClass="form-control" placeholder="Enter Name"></asp:TextBox>
              <cc1:AutoCompleteExtender ID="AutoCompleteExtProduct" runat="server" ServicePath="~/sellproduct.asmx"
                                            ServiceMethod="Searchbyname" CompletionInterval="10" CompletionListItemCssClass="AutoCompliteItem"
                                            CompletionListCssClass="AutoComplite" CompletionListHighlightedItemCssClass="AutoCompliteSelectedItem"
                                            FirstRowSelected="true" MinimumPrefixLength="1" TargetControlID="txtname">
                                        </cc1:AutoCompleteExtender>
</div>

<div class="col-lg-3">
    <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
</div>

            <div class="col-lg-3">

            <asp:Button ID="Button1" runat="server" Text="Search" class="btn btn-danger"  OnClick="Button1_Click"/>

                <button onclick="printDiv()" class="btn btn-danger">Print Div</button>
            <asp:Label ID="lblpid" runat="server" Visible="false"></asp:Label>
                 <asp:Label ID="lbltid" runat="server" Visible="false"></asp:Label>

        </div></div>
        </div>
    <asp:Label ID="lblrate" runat="server" Visible="false"></asp:Label>
    <asp:Panel ID="pnlLedger" runat="server" Visible="false">

        <div id="printDiv">
        <h4>Party: <asp:Label ID="lblPartyName" runat="server" Text=""></asp:Label></h4>

        <!-- Ledger Table -->
        <table class="table table-bordered mt-2">
            <thead class="table-dark">
                <tr>
                    <th colspan="3" class="text-center">Debit (udhar diya)</th>
                    <th colspan="3" class="text-center">Credit (Vapis Jama)</th>
                </tr>
                <tr>
                 
                    <th>Amount</th>
                       <th>Date</th>
                    <th>Interest</th>
                   
                    <th>Amount</th>
                     <th>Date</th>
                    <th>Interest</th>
                </tr>
            </thead>
            <tbody id="ledgerBody" runat="server"></tbody>
            <tfoot>
                <tr class="table-secondary">
                  
                    <th>Total:<asp:Label ID="lblTotalDebit" runat="server" Text="₹ 0.00"></asp:Label></th>
                      <th></th>
                    <th><asp:Label ID="lblTotalDebitInterest" runat="server" Text="₹ 0.00"></asp:Label></th>
                   
                    <th>Total :<asp:Label ID="lblTotalCredit" runat="server" Text="₹ 0.00"></asp:Label></th>
                     <th></th>
                    <th><asp:Label ID="lblTotalCreditInterest" runat="server" Text="₹ 0.00"></asp:Label></th>
                </tr>
                <tr class="table-info">
                    <th colspan="6">Profit / Loss: <asp:Label ID="lblProfitLoss" runat="server" Text="₹ 0.00"></asp:Label></th>
                </tr>
                <tr class="table-warning">
                    <th colspan="6">Closing Balance: ₹ <asp:Label ID="lblClosingBalance" runat="server" Text="₹ 0.00"></asp:Label>/-
                        <asp:Label ID="lblslosingdate" runat="server" ></asp:Label>


                    </th>
                </tr>
            </tfoot>
        </table>


            </div>



    <div class="row mb-3">
     
        <div class="col-md-4">
            <label>New Session Transaction Date</label>
            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
           <div class="col-md-4">
            <label> New Session  Transaction Close Date</label>
            <asp:TextBox ID="txtCloseDate" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
         <div class="col-md-4">
               <label style="color:transparent"> New Session  Transaction Close Date</label>
                <asp:Button ID="btnCloseAccount" runat="server" CssClass="btn btn-danger mt-2"
            Text="Change Session" OnClick="btnCloseAccount_Click" />
       
             </div>
    </div>


 <asp:Label ID="lblCloseMsg" runat="server" CssClass="ms-2 fw-bold"></asp:Label>




        <!-- Close Account Button -->
     
    </asp:Panel>
</div>

</asp:Content>