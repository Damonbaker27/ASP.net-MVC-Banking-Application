<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateTransaction.aspx.cs" Inherits="OnlineBanking.CreateTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="lable1" runat="server" Text="Account Number:"></asp:Label>
        <asp:Label ID="lblAccountNumber" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lable2" runat="server" Text="Balance:"></asp:Label>
        <asp:Label ID="lblBalance" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblTransactionType" runat="server" Text="Transaction Type:"></asp:Label>
        <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTransactionType_SelectedIndexChanged">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="lblAmount" runat="server" Text="Amount:"></asp:Label>
        <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount" Display="Dynamic" ErrorMessage="Amount is required."></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rvAmount" runat="server" ControlToValidate="txtAmount" Display="Dynamic" Enabled="False" ErrorMessage="Amount must be between 0.01 and 10,000." MaximumValue="10000" MinimumValue="0.01"></asp:RangeValidator>
    </p>
    <p>
        <asp:Label ID="lblTo" runat="server" Text="To:"></asp:Label>
        <asp:DropDownList ID="ddlToPayee" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </p>
    <p>
        <asp:LinkButton ID="lbCompleteTransaction" runat="server" OnClick="lbCompleteTransaction_Click">Complete Transaction</asp:LinkButton>
        <asp:LinkButton ID="lbReturnToAccountListing" runat="server" CausesValidation="False" OnClick="lbReturnToAccountListing_Click"> Return to Account Listing</asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="lblExceptionMessage" runat="server" Text="Label" Visible="False"></asp:Label>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
