<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmUserInformation.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmHomeIcon" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <table id="tblmainall" class="tblmain">
            <tr>
                <td class="TitleStrip">
                    <h3>User Information</h3>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="width:30%; float: left;">

                    </div>
                    <div class="Center" style="border: medium groove #0099CC; float: right; text-align: center; width: 70%">
                        <table id="Table1" style="width: 100%">
                            <tr>
                                <td class="TitleStrip">
                                    <h3>Active Users</h3>
                                </td>
                            </tr>
                        </table>
                        <asp:Panel ID="panel1" runat="server" ScrollBars="Auto" Height="200px">
                            <asp:GridView HorizontalAlign="Center" VerticalAlign="Top" ID="gvLatestLogin" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Style="margin-left: 0px">
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

            <tr>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>
