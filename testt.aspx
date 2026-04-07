<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testt.aspx.cs" Inherits="testt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:TextBox ID="txtFromDate" runat="server" placeholder="dd-MM-yyyy"></asp:TextBox>
<asp:TextBox ID="txtToDate" runat="server" placeholder="dd-MM-yyyy"></asp:TextBox>

<asp:Button ID="btnCalculate" runat="server" Text="Calculate Months" 
    OnClick="btnCalculate_Click" />

<asp:Label ID="lblResult" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
