<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmUserDetails.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmUserDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" runat="server" style="width:100%">
        <div id="dvSearch" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <table id="tblmain" runat="server" style="width: 100%;border: thin groove #FF9933;">
                <tr>
                    <td colspan="8" class="TitleStrip">User Information</td>
                </tr>
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="lblUserName" runat="server" Text="User Name:" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtUserName" runat="server" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label3" runat="server" Text="User Full Name :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtUserFullName" runat="server" OnTextChanged="txtUserFullName_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label1" runat="server" Text="Role :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtRoleName" runat="server" OnTextChanged="txtRoleName_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Button ID="btnShowReport" runat="server" CssClass="btn" Text="Filter" OnClick="btnShowReport_Click" />
                    </td>
                    <td class="tdLeft" rowspan="2">&nbsp;</td>
                </tr>
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="Label4" runat="server" Text="Joinig Date From:" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtJoiningDateFrom" runat="server" OnTextChanged="txtJoiningDateFrom_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJoiningDateFrom"></asp:CalendarExtender>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label5" runat="server" Text="Joining Date To :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtJoiningDateTo" runat="server" OnTextChanged="txtJoiningDateTo_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtJoiningDateTo"></asp:CalendarExtender>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="Label2" runat="server" Text="Address :" CssClass="lbl"></asp:Label>
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox CssClass="txt" ID="txtAddress" runat="server" OnTextChanged="txtAddress_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Button ID="btnRefresh" runat="server" CssClass="btn" Text="Reset" OnClick="btnRefresh_Click" />
                    </td>

                </tr>
            </table>      
        </div>
        <div id="dvGrid" runat="server" style="padding-top:10px;  Height:600px ">
            <asp:Panel ID="pnlUserInformation" runat="server" Width="100%" BorderStyle="Groove" BorderColor="#FF9933" BorderWidth="2px" ScrollBars="Auto">
            <asp:GridView ID="gvUserInformation" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC"
                 BorderColor="#FF9933" BorderStyle="Groove" BorderWidth="2px" CellPadding="4" CellSpacing="2" ForeColor="Black"
                Width="100%" >
                <Columns>
                    <asp:CommandField HeaderText="Select" ShowSelectButton="True" >
                    <ItemStyle ForeColor="#003399" />
                    </asp:CommandField>
                    <asp:BoundField HeaderText="User ID" DataField="UserID" />
                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                    <asp:BoundField HeaderText="User Full Name" DataField="UserFullName"/>
                    <asp:BoundField HeaderText="Address" DataField="UserAddress"/>
                    <asp:BoundField HeaderText="Role" DataField="RoleName"/>
                    <asp:BoundField HeaderText="Joining Date" DataFormatString="{0:MMM dd, yyyy}" DataField="JoiningDate"/>
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
