<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Home.Master" AutoEventWireup="true" CodeBehind="frmCurrentPackingShipments.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmCurrentPackingShipments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Center" style="float:right; margin-right:30px; text-align:center">
        <table id="tblFrmMain" runat="server" >
            <tr>
                <td>
                    <h1><span>Shipments Under Packing</span> </h1>
                    <p></p>
                </td>
            </tr>
            <tr >
                <td >
                    <div style=" overflow:auto; height:500px">
                        <asp:GridView HorizontalAlign="Center"   ID="gvShipmentPacking" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="Black" GridLines="Horizontal" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" style="margin-left: 0px">
                            <Columns>
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:BoundField HeaderText="ShipmentID" DataField="PackingID" />
                                <asp:BoundField HeaderText="Location" DataField="ShipmentLocation" />
                                <asp:BoundField HeaderText="User Name" DataField="UserName" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                            <HeaderStyle BackColor="#333333" ForeColor="White" Font-Size="15pt" />
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
