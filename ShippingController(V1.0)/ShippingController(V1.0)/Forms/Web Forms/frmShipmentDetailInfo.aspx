<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Home.Master" AutoEventWireup="true" CodeBehind="frmShipmentDetailInfo.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmShipmentDetailInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvMain" style="vertical-align: central">
        <table id="tblMain" runat="server" style="width: 100%">
            <tr>
                <td style="text-align:center">
                    <div id="dvTitle">
                        <h1>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            Shipment Details</h1>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvTop" style="vertical-align: central">
                        <table id="tblShipmentIDInput" runat="server" style="width:100%">
                            <tr>
                                <td style="text-align:right">
                                    <asp:Label ID="lblShipmentID" runat="server" Text="Enter Shipment ID : "></asp:Label>
                                </td>
                                <td style="text-align:left; width:15%">
                                    <asp:TextBox ID="txtShipmentId" Width="150px" runat="server"></asp:TextBox>
                                </td>
                                <td style="text-align:left">
                                    <asp:Button ID="btnShipmentSearch" runat="server" Text="Search" Font-Bold="True" Width="100px" Height="30px" OnClick="btnShipmentSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align:center"></td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvBottem" style="vertical-align: central">
                        <asp:GridView HorizontalAlign="Center" ID="grdSkuInfo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField HeaderText="SKU Name" DataField="SkuName" />
                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
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

                </td>
            </tr>
        </table>

    </div>
</asp:Content>
