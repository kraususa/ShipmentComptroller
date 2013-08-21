<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmStationDashBoard.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmStationDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .float {
            float:left;
            width:330px;
          margin-right:50px;
           background-color:black;
        }
    </style>
    <div id="MainDiv" runat="server" style="vertical-align: top; overflow:auto; height:600px; width: 99%; background: #fff; border: medium groove #0094ff;">
    </div>
    <%--<table style="width: 100%">
        <tr>
            <td style="text-align: center; font-size: 60px; color: darkgreen; border-bottom-style: groove;">" 40 "</td>
        </tr>
        <tr >
            <td style="text-align: center; font-size: 40px; color: black">Packed</td>
        </tr>
    </table>--%>
</asp:Content>
