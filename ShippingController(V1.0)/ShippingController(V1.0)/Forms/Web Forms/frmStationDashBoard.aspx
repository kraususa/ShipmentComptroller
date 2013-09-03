<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmStationDashBoard.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmStationDashBoard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .main {
            height: 600px;
            overflow: auto;
            vertical-align: top;
            background-color: rgba(128, 128, 128, 0.40);
            width: 99%;
            border: medium groove #0094ff;
        }
    </style>
    <div id="MainDiv" runat="server" class="main">
    </div>
</asp:Content>
