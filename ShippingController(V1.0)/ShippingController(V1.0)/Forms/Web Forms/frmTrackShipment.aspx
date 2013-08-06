<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmTrackShipment.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmTrackShipment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../../Themes/js/jquery-1.5.1.min.js"></script>
    <script src="../../Themes/js/highcharts.js"></script>
    <div  >
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <table id="tblShippingnumber" runat="server" style="width: 100%">
            <tr>
                <td class="tdRight">
                    <asp:Label ID="lblShippingNumber" runat="server" Text="Enter Shipping Number : "></asp:Label>
                </td>
                <td class="tdLeft">
                    <asp:TextBox ID="txtShippingNumber" runat="server" Width="150" AutoPostBack="True" OnTextChanged="txtShippingNumber_TextChanged"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtShipmentID_AutoCompleteExtender" runat="server"
                        ServiceMethod="SearchpackingID"
                        ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                        MinimumPrefixLength="1"
                        CompletionInterval="100"
                        EnableCaching="true"
                        CompletionSetCount="20"
                        TargetControlID="txtShippingNumber">
                    </asp:AutoCompleteExtender>
                </td>
            </tr>
           <%-- <tr>
                <td colspan="2">
                    <asp:GridView HorizontalAlign="Center" ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField DataField="PackageID" HeaderText="PackingID" />
                            <asp:BoundField DataField="ShippingNum" HeaderText="ShippingNumber" />
                            <asp:BoundField DataField="Location" HeaderText="Location" />
                            <asp:BoundField DataField="ShippinStatus" HeaderText="Status" />
                            <asp:BoundField DataField="ShippingCompletedInt" HeaderText="Complete %" />
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
                </td>
            </tr>--%>
            <tr >
                <td colspan="2">
                    <div style="border:medium groove #0094ff">
                    <asp:Literal ID="ltrChart" runat="server" />
                        </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
