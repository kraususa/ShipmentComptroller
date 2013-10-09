<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmRoles.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmRoles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" class="border">
        <div id="dvTitle" class="TitleStrip" style="height: 30px;"> Role Information</div>
        <div id="dvGrid" runat="server" style="padding-top: 10px; Height: 120px">
            <asp:Panel ID="pnlUserInformation" Width="70%" runat="server" BorderStyle="Groove" BorderColor="#FF9933" BorderWidth="2px" ScrollBars="Auto">
                <asp:GridView ID="gvUserInformation" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC"
                   Width="100%" CellPadding="4" CellSpacing="2" ForeColor="Black">
                    <Columns>
                        <asp:BoundField HeaderText="Role ID" DataField="RoleId" />
                        <asp:BoundField HeaderText="Role" DataField="Name" />
                        <asp:BoundField HeaderText="Actions" DataField="Permission" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" />
                    <SelectedRowStyle BackColor="#0099cc" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
