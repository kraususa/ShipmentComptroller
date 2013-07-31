<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmShippingInfo.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmShippingInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvmain" runat="server" style="width: 100%">
        <table id="tblMain" style="width: 100%">
            <tr>
                <td  class="TitleStrip">
                    <h1>Shipping Information
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    </h1>
                </td>
            </tr>
            <tr style="width:100%">
                <td>
                    
                </td>
            </tr>
        </table>
    </div>
   
</asp:Content>
