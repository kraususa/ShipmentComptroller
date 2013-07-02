<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Home.Master" AutoEventWireup="true" CodeBehind="frmLogin.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmLogin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvmain" style="width:40%; margin:auto">
        <table id="tblMaintblLogin" style="width:100%;vertical-align:central; text-align:center; float:none; border:groove; background-color: #333333;">
            <tr>
                <td id="TitleStrip" colspan="4">
                    <h1>Login</h1>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name :"></asp:Label>
                </td>
                <td style="text-align:left">
                    <asp:TextBox ID="txtUserName" runat="server" Width="150px"></asp:TextBox>
                </td>
                <td></td>
                <td></td>

            </tr>
            <tr>
                <td style="text-align:right">
                    <asp:Label ID="Label1" runat="server" Text="Password :"></asp:Label>
                </td>
                <td style="text-align:left">
                    <asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" ></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>

            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align:right">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" Width="100px" ForeColor="White" BackColor="#333333" BorderColor="#0099FF" BorderWidth="2px" OnClick="btnLogin_Click"/>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>

            </tr>
        </table>

    </div>
</asp:Content>
