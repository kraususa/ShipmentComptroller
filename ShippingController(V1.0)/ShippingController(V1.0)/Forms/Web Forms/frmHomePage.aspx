<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmHomePage.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmHomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta http-equiv="refresh" content="100"/>
    <div id="MainDiv" style="float:none; ">
        <table style="width:82%; height:100%;float:none;margin-left:100px">
            <tr>
                <td style="width:35%">
                    <div class="Center" style="border: medium groove #0099CC; float: none; text-align: center">
                        <table style="width:100%" id="tblFrmMain" runat="server">
                            <tr>
                                <td class="TitleStrip">
                                    <h3><span>Shipments Under Packing</span> </h3>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Panel HorizontalAlign="Center" ID="panelContainer" runat="server" Height="250px" ScrollBars="Vertical" >
                                    <asp:GridView  HorizontalAlign="Right" ID="gvShipmentPacking" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Style="margin-left: 0px">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:BoundField HeaderText="ShipmentID" DataField="PackingID" />
                                                <asp:BoundField HeaderText="Location" DataField="ShipmentLocation" />
                                                <asp:BoundField HeaderText="User Name" DataField="UserName" />
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
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width:70%" >
                    <div class="Center" style="border: medium groove #0099CC; float: none; text-align: center">
                        <table style="width:100%" id="Table1" runat="server">
                            <tr>
                                <td class="TitleStrip">
                                    <h3><span>Users logged Today</span> </h3>
                                </td>
                            </tr>
                            <tr>
                                <td >
                                    <asp:Panel HorizontalAlign="Center" ID="panel1" runat="server" Height="250px" ScrollBars="Vertical" >
                                        <asp:GridView  HorizontalAlign="Right" ID="gvLatestLogin" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Style="margin-left: 0px">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" />
                                                <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                                <asp:BoundField HeaderText="Station Name" DataField="StationName" />
                                                <asp:BoundField HeaderText="Station Login Time"  DataField="Datetime" />
                                                <asp:BoundField HeaderText="Device ID" DataField="DeviceID" />
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
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>


            </tr>
            <tr>
                <td></td>
                <td></td>
                

            </tr>
        </table>
    </div>
</asp:Content>
