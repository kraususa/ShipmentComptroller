<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmDisableStation.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmDisableStation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="dvmain" style="width:100%; margin:auto">
        <table id="tblMaintblDeactivate" style="width:100%;vertical-align:central; text-align:center; float:none; border:groove; background-color: #333333;">
            <tr>
                <td style="text-align:center" colspan="2">
                    <h1>Deactivate Station</h1>
                </td>
            </tr>
            <tr>
                
                <td colspan="2">
                  
                    
                    <asp:GridView ID="gvStations" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                        <Columns>
                            <asp:CommandField SelectText="Deactivate/Activate" ShowSelectButton="True">
                            <ItemStyle ForeColor="Black" />
                            </asp:CommandField>
                            <asp:BoundField HeaderText="Station Name" DataField="StationName">
                            <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Active Status" DataField="ActiveStatus">
                                 <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField  HeaderText="Device ID" DataField="DeviceID" >
                                 <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Requested User" DataField="RequestedUserName">
                                 <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Request date" DataField="RequestedDate" >
                                 <ItemStyle ForeColor="Black" />
                            </asp:BoundField>
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
