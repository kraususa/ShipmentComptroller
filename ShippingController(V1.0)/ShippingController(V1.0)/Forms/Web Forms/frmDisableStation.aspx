<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmDisableStation.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmDisableStation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvmain" style="width:100%; margin:auto">
        <table id="tblMaintblDeactivate" style="width:100%;vertical-align:central; text-align:center; float:none; border:groove; background-color: #333333;">
            <tr>
                <td class="TitleStrip" colspan="2">
                   <h1> <span > InActive/Active Stations</span></h1>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:left">
                    <asp:GridView HorizontalAlign="Center" ID="gvStations" runat="server" AutoGenerateColumns="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" ForeColor="Black" OnSelectedIndexChanged="gvStations_SelectedIndexChanged" OnLoad="gvStations_Load" CellSpacing="2">
                        <Columns>
                            <asp:CommandField  SelectText="InActive/Activate" ShowSelectButton="True">
                                <ItemStyle ForeColor="Black" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="Station Name" DataField="StationName">
                                <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Active Status" DataField="ActiveStatus">
                                <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Device ID" DataField="DeviceID">
                                <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Requested User" DataField="RequestedUserName">
                                <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Request date" DataField="RequestedDate">
                                <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" Font-Names="Arial"  />
                        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" />
                        <SelectedRowStyle BackColor=" #3399FF" Font-Bold="True" ForeColor="Black" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />
                    </asp:GridView>
                  
                    
                </td>
                

            </tr>
            <tr>
                
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>

            </tr>
            
            
        </table>

    </div>
</asp:Content>
