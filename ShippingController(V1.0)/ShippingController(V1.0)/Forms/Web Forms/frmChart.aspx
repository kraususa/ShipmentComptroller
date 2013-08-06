<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmChart.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmChart" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script src="../../Themes/js/jquery-1.5.1.min.js"></script>
    <script src="../../Themes/js/highcharts.js"></script>
    <div style="border:groove medium #0094ff">
        <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
    </div>
</asp:Content>
