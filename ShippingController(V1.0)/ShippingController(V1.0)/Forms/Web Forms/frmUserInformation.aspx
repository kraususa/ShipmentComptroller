<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmUserInformation.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmHomeIcon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta http-equiv="refresh" content="60;frmUserInformation.aspx"  />
    <style >
        .lblVeriables {
            color:#2a8011;
            font-family:Arial;
            font-size:20px;
            font-weight:bold;
            
        }
        .tdStrip {
            text-align:center;
            color:#d5a111;
        }
        .lblConst {
            color:#fff;
            font-family:Arial;
            font-size:20px;
            font-weight:bold;
        }
    </style>
    <div>
        <table id="tblmainall" class="tblmain">
            <tr>
                <td class="TitleStrip" colspan="2">
                    <h3>User Information</h3>
                </td>
            </tr>
            <tr>
                <td style="width: 30%; vertical-align: top;">
                    <div style="margin: 0px auto;">
                        <table style="width: 100%; border: medium groove #0099CC;">
                            <tr style="border-bottom: thick solid #808080;">
                                <td class="TitleStrip">
                                    <h3>Counters</h3>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Label ID="Label3" runat="server" CssClass="lblConst" Text="Active Users: "></asp:Label>
                                    <asp:Label ID="lblCActiveUsers" runat="server" CssClass="lblVeriables" Text="00 "></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" CssClass="lblConst" Text="In-Active: "></asp:Label>
                                    <asp:Label ID="lblCInactiveUsers" runat="server" CssClass="lblVeriables" Text="00"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTotalUsers" runat="server" CssClass="lblConst" Text="Total Users: "></asp:Label>
                                    <asp:Label ID="lblCTotalUsers" runat="server" CssClass="lblVeriables" Text="0"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 70%">
                    <div style="border: medium groove #0099CC; float: right; text-align: center; width:100%">
                      
                        <table id="Table1" style="width:100%">
                            <tr>
                                <td class="TitleStrip">
                                    <h3>
                                        <asp:Label ID="lblActive" runat="server" Text="Active Users"></asp:Label>
                                    </h3>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panel1" runat="server" ScrollBars="Auto" Width="100%" Height="200px">
                            <asp:GridView HorizontalAlign="Center" Width="100%" VerticalAlign="Top" ID="gvLatestLogin" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Style="margin-left: 0px">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="True" />
                                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                    <asp:BoundField HeaderText="Station Name" DataField="StationName" />
                                    <asp:BoundField HeaderText="Station Login Time" DataField="Datetime" />
                                    <asp:BoundField HeaderText="Device ID" DataField="DeviceID" />
                                    <asp:BoundField HeaderText="Packed" DataField="Packed" />
                                    <asp:BoundField HeaderText="Current Shipment" DataField="CurrentPackingShipmentID" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                <HeaderStyle BackColor="#333333" ForeColor="White" CssClass="fixedHeader " />
                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                <SortedDescendingHeaderStyle BackColor="#242121" />
                            </asp:GridView>
                            
                        </asp:Panel>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
