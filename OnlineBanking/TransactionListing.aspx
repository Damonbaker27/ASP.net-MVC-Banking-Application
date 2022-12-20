<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransactionListing.aspx.cs" Inherits="OnlineBanking.TransactionListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="lblClientName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblAccountNumber" runat="server" Text="Account Number: "></asp:Label>
        <asp:Label ID="lblBalance" runat="server" Text="Balance"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" Height="52px" Width="619px">
            <Columns>
                <asp:BoundField DataField="DateCreated" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Date">
                <HeaderStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="TransactionType.Description" HeaderText="Transaction Type" >
                <HeaderStyle Wrap="False" />
                <ItemStyle Width="150px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Deposit" DataFormatString="{0:c}" HeaderText="Amount In">
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Right" Width="150px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Withdrawal" DataFormatString="{0:c}" HeaderText="Amount Out">
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Right" Width="150px" Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Notes" HeaderText="Details">
                <HeaderStyle Wrap="False" />
                <ItemStyle Width="300px" Wrap="False" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="Gray" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </p>
    <p>
        <asp:LinkButton ID="lbPayBills" runat="server" OnClick="lbPayBills_Click">Pay Bills and Transfer Funds</asp:LinkButton>
        <asp:LinkButton ID="lbReturnToAccounts" runat="server" OnClick="LinkButton1_Click">  Return to Account Listing</asp:LinkButton>
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
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
