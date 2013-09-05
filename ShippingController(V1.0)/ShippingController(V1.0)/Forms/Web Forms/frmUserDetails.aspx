<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmUserDetails.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmUserDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" runat="server" style="width:100%">
        <div id="dvSearch" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table id="tblmain" runat="server" style="width: 100%">
                <tr>
                    <td colspan="8" class="TitleStrip">User Information</td>
                </tr>
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtUserName" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label3" runat="server" Text="User Full Name :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtUserFullName" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label1" runat="server" Text="Role :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtRoleName" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Button ID="btnShowReport" runat="server" CssClass="btn" Text="Filter" />
                    </td>
                    <td class="tdLeft" rowspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="Label4" runat="server" Text="Joinig Date From:" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtJoiningDateFrom" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJoiningDateFrom"></asp:CalendarExtender>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label5" runat="server" Text="Joining Date To :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtJoiningDateTo" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtJoiningDateTo"></asp:CalendarExtender>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label2" runat="server" Text="Address :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtAddress" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Button ID="btnRefresh" runat="server" CssClass="btn" Text="Refresh" />
                    </td>

                </tr>
            </table>      
        </div>
        <div id="dvGrid" runat="server">
            <asp:Panel ID="pnlUserInformation" runat="server" Height="600px" Width="60%" ScrollBars="Auto">
            <asp:GridView ID="gvUserInformation" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC"
                 BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black"
                Width="100%" >
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                    <asp:BoundField HeaderText="User ID" DataField="UserID" />
                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                    <asp:BoundField HeaderText="User Full Name" DataField="UserFullName"/>
                    <asp:BoundField HeaderText="Address" DataField="UserAddress"/>
                    <asp:BoundField HeaderText="Role" DataField="RoleName"/>
                    <asp:BoundField HeaderText="Joining Date" DataField="JoiningDate"/>
                </Columns>
                <FooterStyle BackColor="#CCCCCC" />
                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                <RowStyle BackColor="White" />
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#808080" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#383838" />
            </asp:GridView>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
