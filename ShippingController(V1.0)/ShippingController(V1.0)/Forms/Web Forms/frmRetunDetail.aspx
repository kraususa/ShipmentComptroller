<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmRetunDetail.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmRetunDetail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Release"/>
    <script src="../../Themes/js/jquery-1.5.1.min.js"></script>
    <script src="../../Themes/js/highcharts.js"></script>
    <script type="text/javascript">
        window.onload = function () {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("dvShippingInfo").scrollTop = strPos;
            }
        }
        function SetDivPosition() {
            var intY = document.getElementById("dvShippingInfo").scrollTop;
            document.cookie = "yPos=!~" + intY + "~!";
        }
    </script>
    <style>
        .ExportExcel {
            background-image: url(../../Themes/Images/excel_icon.png);
            background-repeat: no-repeat;
            background-position:left;
            height: 32px;
            width: 165px;
            border-color: #ff6a00;
            border-radius: 10px;
            border-width: thin;
            border-style: groove;
            font-weight: 700;
            font-family: Arial;
            font-size: 14px;
            text-align:right
        }
             .lblVeriables {
            color: #000;
            font-family: Arial;
            font-size: 14px;
            font-weight: bold;
        }

        .tdStrip {
            text-align: center;
            color: #d5a111;
        }
        .Test {
            height:44px;
            width:95px;
            background-image:url("../../Themes/Images/Arrow.gif");
            background-size:contain;
            background-position:left;
            background-repeat:no-repeat;
            vertical-align:central;
            text-align:center;
            align-content:center;
            font-size:smaller;
            color:black;
        }
    </style>
   
     <table id="tblMain" style="width:100%">
        <tr>
            <td class="TitleStrip">Return Details Information (RMA)
            </td>
        </tr>
         <tr>
             <td>
                 <div id="dvIDonly" runat="server">
                     <asp:Accordion
                         ID="Accordion1"
                         runat="Server"
                         SelectedIndex="0"
                         HeaderCssClass="accordionHeader"
                         HeaderSelectedCssClass="accordionHeaderSelected"
                         ContentCssClass="accordionContent"
                         AutoSize="None"
                         FadeTransitions="true"
                         TransitionDuration="250"
                         FramesPerSecond="40"
                         RequireOpenedPane="false"
                         SuppressHeaderPostbacks="true" Width="100%" Height="112px">
                         <Panes>
                             <asp:AccordionPane runat="server" ID="AccordionPane1"
                                 HeaderCssClass="accordionHeader"
                                 ContentCssClass="accordionContent">
                                 <Header>&nbsp;∇∇&nbsp;Basic Search</Header>
                                 <Content>
                                     <table style="width: 100%; border-bottom-color: #0094ff; border-bottom-width: medium; border-bottom-style: groove;">
                                         <tr>
                                             <td class="tdRight">
                                                 <asp:Label ID="Label2" runat="server" Text="ShipmentID :" CssClass="lbl"></asp:Label>
                                             </td>
                                             <td class="tdLeft">
                                                 <asp:TextBox CssClass="txt" ID="txtShipmentID" runat="server" AutoPostBack="true"></asp:TextBox>
                                                 <asp:AutoCompleteExtender ID="txtShipmentID_AutoCompleteExtender" runat="server"
                                                     ServiceMethod="SearchpackingID"
                                                     MinimumPrefixLength="1"
                                                     ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                                                     CompletionInterval="100"
                                                     EnableCaching="true"
                                                     CompletionSetCount="20"
                                                     TargetControlID="txtShipmentID">
                                                 </asp:AutoCompleteExtender>
                                             </td>
                                             <td class="tdRight">
                                                 <asp:Label ID="lblOrderNum" runat="server" Text="Order Number :" CssClass="lbl"></asp:Label>
                                             </td>
                                             <td class="tdLeft">
                                                 <asp:TextBox CssClass="txt" ID="txtOrderNumber" runat="server" AutoPostBack="true"></asp:TextBox>
                                                 <asp:AutoCompleteExtender ID="AutoCompleteExtender5" runat="server"
                                                     ServiceMethod="serachBoxNumber"
                                                     MinimumPrefixLength="1"
                                                     ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                                                     CompletionInterval="100"
                                                     EnableCaching="true"
                                                     CompletionSetCount="20"
                                                     TargetControlID="txtOrderNumber">
                                                 </asp:AutoCompleteExtender>
                                             </td>
                                             <td class="tdRight">
                                                 <asp:Label ID="lblPONumber" runat="server" Text="PO Number :" CssClass="lbl"></asp:Label>
                                             </td>
                                             <td class="tdLeft">
                                                 <asp:TextBox CssClass="txt" ID="txtPoNum" runat="server" AutoPostBack="true"></asp:TextBox>
                                                 <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                     ServiceMethod="SearchTrackingNumber"
                                                     MinimumPrefixLength="1"
                                                     ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                                                     CompletionInterval="100"
                                                     EnableCaching="true"
                                                     CompletionSetCount="20"
                                                     TargetControlID="txtPoNum">
                                                 </asp:AutoCompleteExtender>
                                             </td>
                                             <td class="tdLeft" style="width: 30%; text-align: right;">
                                                 <asp:Button ID="btnRefresh2" runat="server" Text="Reset" CssClass="btn" />
                                             </td>
                                         </tr>
                                     </table>
                                 </Content>
                             </asp:AccordionPane>
                         </Panes>
                         <Panes>
                             <asp:AccordionPane runat="server" ID="AccordionPane2"
                                 HeaderCssClass="accordionHeader"
                                 ContentCssClass="accordionContent">
                                 <Header>&nbsp;∇∇&nbsp;Advance Search</Header>
                                 <Content>
                                     <div id="dvAllinfo" runat="server" class="border">
                                         <table style="width: 100%;">
                                             <tr>
                                                 <td class="tdRight">
                                                     <asp:Label ID="lblUserName" runat="server" Text="Customer Name :" CssClass="lbl"></asp:Label>
                                                 </td>
                                                 <td class="tdLeft">
                                                     <asp:DropDownList ID="ddlCustomerName" runat="server" Width="150px" AutoPostBack="True">
                                                     </asp:DropDownList>
                                                 </td>
                                                 <td class="tdRight">
                                                     <asp:Label ID="Label1" runat="server" Text="Vendor Number :" CssClass="lbl"></asp:Label>
                                                 </td>
                                                 <td class="tdLeft">
                                                     <asp:DropDownList ID="ddlVendorName" runat="server" Width="100px" AutoPostBack="True">
                                                     </asp:DropDownList>
                                                 </td>

                                                 <td class="tdRight">
                                                     <asp:Label ID="lblPoNnumber" runat="server" Text="PO Number:" CssClass="lbl"></asp:Label>
                                                 </td>
                                                 <td class="tdLeft">
                                                     <asp:TextBox CssClass="txt" ID="txtPoNumber" runat="server"  AutoPostBack="True"></asp:TextBox>
                                                 </td>
                                             </tr>
                                             <tr>
                                                 <td class="tdRight">
                                                     <asp:Label ID="lblFromDate" runat="server" Text="From Date :" CssClass="lbl"></asp:Label>
                                                 </td>
                                                 <td class="tdLeft">
                                                     <asp:TextBox CssClass="txt" ID="dtpFromDate" runat="server"  AutoPostBack="True"></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="dtpFromDate" runat="server" Format="MMM dd, yyyy"></asp:CalendarExtender>
                                                 </td>
                                                 <td class="tdRight">
                                                     <asp:Label ID="lblTodate" runat="server" Text="To Date :" CssClass="lbl"></asp:Label>
                                                 </td>
                                                 <td class="tdLeft">
                                                     <asp:TextBox CssClass="txt" ID="dtpToDate" runat="server" AutoPostBack="True"></asp:TextBox>
                                                     <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="dtpToDate" runat="server" Format="MMM dd, yyyy"></asp:CalendarExtender>
                                                 </td>
                                                 <td class="tdRight">&nbsp;&nbsp;&nbsp;
                                                 </td>
                                                 <td>&nbsp;
                                                 </td>
                                                 <td class="tdRight" colspan="2">
                                                     <asp:Button ID="btnExport" runat="server" Text="Export Manifest" CssClass="ExportExcel" />
                                                     &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnRefresh" runat="server" Text="Reset" CssClass="btn" />
                                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                                                 </td>
                                             </tr>

                                         </table>
                                     </div>
                                 </Content>
                             </asp:AccordionPane>
                         </Panes>
                         <HeaderTemplate>ASX</HeaderTemplate>
                         <ContentTemplate>asdfasdfasdf</ContentTemplate>
                     </asp:Accordion>
                 </div>
             </td>
         </tr>
         <tr>
             <td>
                 <div id="Div3" runat="server">
                 
                 </div>
             </td>
         </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
