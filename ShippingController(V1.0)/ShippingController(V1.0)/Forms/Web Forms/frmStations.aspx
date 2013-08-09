<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmStations.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmStations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../Themes/js/jquery-1.5.1.min.js"></script>
    <script src="../../Themes/js/highcharts.js"></script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <div style="width: 100%">
       <div id="dvGrid" runat="server" style="width: 21%; float: left; vertical-align: top; border: medium groove #0099CC;">
            <asp:GridView Width="100%" ID="gvStationInfo" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                <Columns>
                    <asp:BoundField DataField="StationName" HeaderText="Station Name" />
                    <asp:BoundField DataField="TotalPacked" HeaderText="Packed " />
                    <asp:BoundField DataField="PartiallyPacked"  HeaderText="Under Packing" />
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
        <div style="border: medium groove #0094ff; width:78%; float:right">
            <asp:Literal ID="ltrChart" runat="server" />
        </div>
    </div>
</asp:Content>
