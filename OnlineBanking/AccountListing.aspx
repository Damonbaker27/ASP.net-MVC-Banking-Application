<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccountListing.aspx.cs" Inherits="OnlineBanking.AccountListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="lblClient" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="gvAccounts" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnSelectedIndexChanged="gvAccounts_SelectedIndexChanged" Width="350px">
            <Columns>
                <asp:BoundField DataField="AccountNumber" HeaderText="Account Number">
                <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:BoundField DataField="Notes" HeaderText="Account Notes">
                <ItemStyle Width="10000px" />
                </asp:BoundField>
                <asp:BoundField DataField="Balance" HeaderText="Balance" DataFormatString="{0:c}">
                <ItemStyle HorizontalAlign="Right" Width="1000px" />
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
        <asp:Label ID="lblExceptionMessage" runat="server" Text="Label" Visible="False" style="font-weight: 700"></asp:Label>
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
