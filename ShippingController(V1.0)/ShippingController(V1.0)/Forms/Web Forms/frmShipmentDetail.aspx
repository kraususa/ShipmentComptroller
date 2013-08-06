<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmShipmentDetail.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmShipmentDetail" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style >
        .lblVeriables {
            color:#2a8011;
            font-family:Arial;
            font-size:14px;
            font-weight:bold;
        }
        .tdStrip {
            text-align:center;
            color:#d5a111;
        }
    </style>
    <table id="tblMain" style="width: 100%">
        <tr>
            <td class="TitleStrip">
                <h1>Shipment Information<asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                </h1>
            </td>
        </tr>
         <tr>
            <td>
                <div id="dvIDonly" runat="server">
                    <table style="width: 100%; border-bottom-color: #0094ff; border-bottom-width: medium; border-bottom-style: groove;">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="Label2" runat="server" Text="ShipmentID :" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox CssClass="txt" ID="txtShipmentID" runat="server"></asp:TextBox>
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

                            <td class="tdLeft" style="width: 60%; text-align: right;">
                                <asp:Button ID="btnShowShipmentInfoID" runat="server" Text="Show Report" CssClass="btn" OnClick="btnShowShipmentInfoID_Click" />
                            </td>


                        </tr>

                    </table>

                </div>
            </td>
        </tr>
        <tr>
            
                <td>
                    <div id="dvAllinfo" runat="server">
                    <table style="width: 100%; border-bottom-color: #0094ff; border-bottom-width: medium; border-bottom-style: groove;">
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="lblUserName" runat="server" Text="UserName :" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlUserName" runat="server" Width="200px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="Label1" runat="server" Text="Packing Status :" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlpackingStatus" runat="server" Width="150px" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                            <td></td>

                        </tr>
                        <tr>
                            <td class="tdRight">
                                <asp:Label ID="lblFromDate" runat="server" Text="From Date :" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox CssClass="txt" ID="dtpFromDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="dtpFromDate" runat="server" Format="MMM dd, yyyy"></asp:CalendarExtender>
                            </td>
                            <td class="tdRight">
                                <asp:Label ID="lblTodate" runat="server" Text="To Date :" CssClass="lbl"></asp:Label>
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox CssClass="txt" ID="dtpToDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="dtpToDate" runat="server" Format="MMM dd, yyyy"></asp:CalendarExtender>
                            </td>
                            <td colspan="2" class="tdRight">
                                <asp:Button ID="btnShowReport" runat="server" Text="Show Report" CssClass="btn" OnClick="btnShowReport_Click" />
                            </td>
                        </tr>
                    </table>
                        </div>
                </td>
            
        </tr>
       
        <tr >
            <td>
                <div id="dvInfo" runat="server" style="width:100%">
                    <div id="dvLeft" runat="server" style="float:right; width:20%;" >
                        <h3 style="text-align:center;"><span style=" color:#d5a111;">Shipment List</span> </h3>
                        <asp:GridView HorizontalAlign="Center"  ID="gvShipmentList" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="gvShipmentList_SelectedIndexChanged">
                            <Columns>
                                <asp:CommandField SelectText="Show&lt;&lt;" ShowSelectButton="True" />
                                <asp:BoundField HeaderText="Shipment No." DataField="ShippingNumber"/>
                                 <asp:BoundField HeaderText="" Visible="false" DataField ="PackingID" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F7F7F7" />
                            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                            <SortedDescendingCellStyle BackColor="#E5E5E5" />
                            <SortedDescendingHeaderStyle BackColor="#242121" />
                        </asp:GridView>
                    </div>
                    <div id="dvRight" runat="server" style="float: left; width: 80%">
                        <script lang="javascript" type="text/javascript">
                            function CallPrint(strid) {
                                var prtContent = document.getElementById(strid);
                                var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=100,toolbar=0,scrollbars=0,status=0,dir=ltr');
                                WinPrint.document.write(prtContent.innerHTML);
                                WinPrint.document.close();
                                WinPrint.focus();
                                WinPrint.print();
                                WinPrint.close();
                                prtContent.innerHTML = strOldOne;
                            }
                        </script>
                                                 
                        <div style="width: 100%">
                            <table style="width: 100%">
                                <tr>
                                    <td colspan="4" class="tdStrip" >
                                        <table id="tblPrint" runat="server" style="width:100%">
                                            <tr>
                                                <td style="width:80%">
                                        <h3>Shipment Detail Information</h3>
                                                </td>
                                                <td style="text-align: center">
                                                    <img class="btn" src="../../Images/document-print.png" style="height: 37px; width: 50px" onclick="javascript:CallPrint('dvRight');"/>
                                                    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdRight">
                                        <asp:Label ID="lblShipmentID" runat="server" Text="Shipment ID :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblCShipmentID" runat="server" Text="SH000xx" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                    <td class="tdRight">
                                        <asp:Label ID="lblUserFullName" runat="server" Text="User Name :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblCUserName" runat="server" Text="Avinash Patil" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdRight">
                                        <asp:Label ID="lblStatus" runat="server" Text="Shipment Status :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblCStatus" runat="server" Text="Partially Packed" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                    <td class="tdRight">
                                        <asp:Label ID="lblTime" runat="server" Text="Time Spend :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblCTime" runat="server" Text="00:00:00" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="tdRight">
                                        <asp:Label ID="lblSkuQty" runat="server" Text="Total Sku Quantity :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblCSkuQty" runat="server" Text="50" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                    <td class="tdRight">
                                        <asp:Label ID="lblLocation" runat="server" Text="Location :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblCLocation" runat="server" Text="NYWH" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                </tr>
                                  <tr>
                                    <td class="tdRight">
                                        <asp:Label ID="Label3" runat="server" Text="Override Type :" CssClass="lbl"></asp:Label>
                                    </td>
                                    <td class="tdLeft">
                                        <asp:Label ID="lblcOverrideMode" runat="server" Text="No" CssClass="lblVeriables"></asp:Label>
                                    </td>
                                    <td class="tdRight">
                                    </td>
                                    <td class="tdLeft">
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%">
                            <asp:GridView HorizontalAlign="Center" ID="gvShipmentDetail" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
                                <Columns>
                                    <asp:BoundField HeaderText="SKU Name" DataField="SKUNumber" />
                                    <asp:BoundField HeaderText="Qty." DataField="SKUQuantity"/>
                                    <asp:BoundField HeaderText="Start Time" DataField="PackingDetailStartDateTime" DataFormatString="{0:MMM dd, yyyy hh:mm:ss tt}"/>
                                    <asp:BoundField HeaderText="Box Qty." DataField="BoxQuantity"/>
                                    <asp:BoundField HeaderText="Location" DataField="ShipmentLocation"/>
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
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
