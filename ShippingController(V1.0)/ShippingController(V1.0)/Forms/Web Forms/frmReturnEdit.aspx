<%@ Page Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmReturnEdit.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmReturnEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%">
        <tr>
            <td>
                <div class="border">
                    <table id="tblmain" runat="server" style="width: 100%; padding: 2px;" class="border">
                        <tr>
                            <td class="tdRight" style="width:10%">
                                <asp:Label ID="lblRMANumber" runat="server" Text="RMA Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtRMAnumber" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="lblRMAstatus" runat="server" Text="RMA Status" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlstatus" runat="server" Width="127px" ></asp:DropDownList>
                             </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="Label3" runat="server" Text="RMA Decision" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="DropDownList1" runat="server" Width="127px" ></asp:DropDownList>
                                </td>
                        </tr>
                        <tr>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtcustomerName" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblshipment" runat="server" Text="Shipment Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtshipmentnumber" runat="server"></asp:TextBox>
                            </td>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="lblVendorname" runat="server" Text="Vendor Name" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtvendorName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="Label1" runat="server" Text="Return Date" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtreturndate" runat="server"></asp:TextBox>
                            </td>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="Label2" runat="server" Text="PO Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtponumber" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblordernumber" runat="server" Text="Order Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtordernumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="lblRGAnumber" runat="server" Text="RGA Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtrganumber" runat="server" ReadOnly="true"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblorderdate" runat="server" Text="Order Date" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtorderdate" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblvendornumber" runat="server" Text="Vendor Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtvendornumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </div>

            </td>

        </tr>
        <tr>
            <td>
                <div class="border"></div>

            </td>

        </tr>
        <tr>
            <td>
                <div class="border"></div>

            </td>

        </tr>
    </table>
    </asp:Content>