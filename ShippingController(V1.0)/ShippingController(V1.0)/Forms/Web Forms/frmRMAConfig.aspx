﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmRMAConfig.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmRMAConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table id="tblMain" style="width:100%">
    <tr>
        <td class="TitleStrip">
            RMA Configuration Setting:
        </td>
    </tr>
    <tr>
        <td>
            <div style="width: 100%">
                <div style="width: 100%; float: left" class="border">
                    <table style="margin: 2px; width: 99%; height: 200px">
                        <tr style="height: 30px">
                            <td colspan="6" style="font-size: 15px; font-weight: bold; color: #0094ff; background-color: black; font-family: Arial;">Return Reasons Setting</td>
                        </tr>
                        <tr>
                            <td colspan="6" style="vertical-align: top">
                                <div class="border" style="width: 100%;">
                                    <asp:Panel ID="Panel3" runat="server" ScrollBars="Auto" Height="180">
                                        <asp:GridView
                                            HorizontalAlign="Center"
                                            ID="gvReasons"
                                            runat="server"
                                            Width="100%"
                                            AutoGenerateColumns="False"
                                            BackColor="#CCCCCC"
                                            BorderColor="#999999"
                                            BorderStyle="Solid"
                                            BorderWidth="3px"
                                            CellPadding="4"
                                            CellSpacing="2"
                                            ForeColor="Black">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Reason">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnRGANumberID" CommandName="Select" runat="server" Text='<%# Eval("Reason1") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Point" DataField="ReasonPoints" />
                                                <asp:BoundField HeaderText="Category" DataField="Category" />
                                                <asp:BoundField HeaderText="ID" DataField="ReasonID" />
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                            <RowStyle BackColor="White" />
                                            <SelectedRowStyle BackColor="#0099cc" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr>
                          <td colspan="6" style="vertical-align: top; width: 50%">
                                <div class="border" style="width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table style="width: 99%;">
                                                    <tr>
                                                        <td style="width: 5%">
                                                            <label id="Label2" class="lbl">Reason</label>
                                                        </td>
                                                        <td colspan="3">
                                                            <asp:TextBox ID="txtReason" Width="100%" Height="40px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="tdRight">
                                                            <label id="Label3" class="lbl">Point:</label>
                                                        </td>
                                                        <td style="width:10%">
                                                            <asp:TextBox ID="txtPoint" Width="150" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td style="width:5%" class="tdRight">
                                                             <label id="Label4" class="lbl">Category:</label>
                                                        </td>
                                                        <td>
                                                             <asp:TextBox ID="txtCategory" Width="150" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>
                                                </table>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnSave" runat="server" Text="Add" CssClass="btn" OnClick="btnSave_Click" />&nbsp;
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                            </tr>
                    </table>

                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <table style="margin: 2px; width: 99%; height: 200px">
                <tr style="height: 30px">
                    <td colspan="6" style="font-size: 15px; font-weight: bold; color: #0094ff; background-color: black; font-family: Arial;">
                        Image Server string 
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align:top;height: 40px;">
                        <asp:TextBox ID="txtImageServer" runat="server" Width="800px"></asp:TextBox>
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnUpdateImageServer" runat="server" Text="Update" CssClass="btn" OnClick="btnUpdateImageServer_Click" />
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <asp:Label ID="Label1" Font-Size="Medium" runat="server" ForeColor="#ff6a00" Text="Image server string must contains '#{ImageName}#' part. Which will be replaced by Image name."></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>
