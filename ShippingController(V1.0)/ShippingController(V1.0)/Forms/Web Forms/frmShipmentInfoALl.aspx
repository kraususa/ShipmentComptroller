<%@ Page Title="" Language="C#"  MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmShipmentInfoALl.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmShipmentInfoALl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <meta http-equiv="refresh" content="60;frmShipmentInfoALl.aspx" />
    <div id="dvMain" runat="server">
        <asp:AutoCompleteExtender ID="txtShipmentID_AutoCompleteExtender" runat="server"
            ServiceMethod="SearchpackingID"
            MinimumPrefixLength="1"
            ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
            CompletionInterval="100"
            EnableCaching="true"
            CompletionSetCount="20"
            TargetControlID="txtShipmentID">
        </asp:AutoCompleteExtender>
        <table id="tblMainTop" runat="server" style="width: 100%; margin: 0px auto;">
            <tr class="TitleStrip">
                <td>
                    <h3>All Shipment Information</h3>
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                    </asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; border-bottom-color: #0094ff; border-bottom-width: medium; border-bottom-style: groove;">
                        <tr style="text-align: left; vertical-align: text-bottom; height: 23px">
                            <td style="width: 2%; text-align: left;">
                                <asp:TextBox CssClass="txt" ID="txtShipmentID" runat="server" AutoPostBack="True" OnTextChanged="txtShipmentID_TextChanged" Height="22px" ></asp:TextBox>
                                <%--<asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                                    TargetControlID="txtShipmentID" WatermarkText="Please type Shipment ID">
                                </asp:TextBoxWatermarkExtender>--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dvLeft" runat="server">
                        <asp:Panel ID="panel1" runat="server" Height="300px" ScrollBars="Auto">

                            <asp:GridView ID="gvShipmentInformation" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" OnSelectedIndexChanged="gvShipmentInformation_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                                    <asp:BoundField HeaderText="ShipmentID" DataField="ShipmentID">
                                        <ItemStyle Font-Underline="True" ForeColor="#3366CC" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Location" DataField="Location" />
                                    <asp:BoundField HeaderText="Who" DataField="UserName" />
                                    <asp:BoundField HeaderText="Start Time" DataField="StartTime" />
                                    <asp:BoundField HeaderText="Time Spent" DataField="TimeSpent" />
                                    <asp:BoundField HeaderText="Status" DataField="PackingStatus" />
                                    <asp:BoundField HeaderText="Override" DataField="ManagerOVerride" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                <RowStyle BackColor="White" />
                                <SelectedRowStyle BackColor="Brown" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:HiddenField ID="PosY" runat="server" Value="0" />
                        <asp:HiddenField ID="PosX" runat="server" Value="0" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
