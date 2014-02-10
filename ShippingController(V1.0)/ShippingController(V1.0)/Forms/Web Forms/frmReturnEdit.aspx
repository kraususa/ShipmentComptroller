<%@ Page Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmReturnEdit.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmReturnEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div style="width:100%;height:560px">
    <table style="width:100%">
          <tr>
            <td class="TitleStrip">Return Details Information Update (RMA)
            </td>
        </tr>
        <tr>
            <td>
                <div class="border">
                    <table id="tblmain" runat="server" style="width: 100%; padding: 2px;" class="border">
                        <tr>
                            <td class="tdRight" style="width:10%">
                                 <asp:Label ID="lblRGAnumber" runat="server" Text="RGA Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtrganumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                            <td style="width:20%" class="tdRight">
                                <asp:Label ID="lblRMANumber" runat="server" Text="RMA Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtRMAnumber" runat="server" ReadOnly="true"></asp:TextBox>
                             </td>
                            <td class="tdRight" style="width:20%">
                                 <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtcustomerName" runat="server" Enabled="false"></asp:TextBox>
                                </td>
                        </tr>
                        <tr>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="lblRMAstatus" runat="server" Text="RMA Status" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddlstatus" runat="server" Width="127px" AutoPostBack="True">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                    <asp:ListItem Value="2">Pending</asp:ListItem>
                                    <asp:ListItem Value="3">Canceled</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblshipment" runat="server" Text="Shipment Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtshipmentnumber" runat="server"  Enabled="false"></asp:TextBox>
                            </td>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="lblVendorname" runat="server" Text="Vendor Name" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtvendorName" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="Label3" runat="server" Text="RMA Decision" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:DropDownList ID="ddldecision" runat="server" Width="127px" AutoPostBack="True" >
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                     <asp:ListItem Value="1">Approved</asp:ListItem>
                                    <asp:ListItem Value="2">Pending</asp:ListItem>
                                    <asp:ListItem Value="3">Canceled</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td style="width:20%" class="tdRight">
                                 <asp:Label ID="Label2" runat="server" Text="PO Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                 <asp:TextBox CssClass="txt" ID="txtponumber" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblordernumber" runat="server" Text="Order Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtordernumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%" class="tdRight">
                                <asp:Label ID="Label1" runat="server" Text="Return Date" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreturndate"></asp:CalendarExtender>
                                <asp:TextBox CssClass="txt" ID="txtreturndate" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblorderdate" runat="server" Text="Order Date" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtorderdate" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                            <td class="tdRight" style="width:20%">
                                <asp:Label ID="lblvendornumber" runat="server" Text="Vendor Number" CssClass="lbl" ></asp:Label>
                            </td>
                            <td style="width:20%">
                                <asp:TextBox CssClass="txt" ID="txtvendornumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </div>

            </td>

        </tr>
        <tr>
            <td>
                <div class="border" id="Div2" style="height: 250px; overflow: scroll" onscroll="SetDivPosition()">
                    <asp:Panel ID="panel1" runat="server" Height="300px">
                                            <asp:GridView ID="gvReturnDetails" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                                                BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                                                ForeColor="Black" AllowSorting="true" 
                                               
                                                 >
                                                
                                                <Columns>
                                                     <asp:TemplateField HeaderText="Return Detail Number" >
                                                       <ItemTemplate >
                                                           <asp:TextBox Enabled="false" ID="txtRGANumberID" runat="server" Text='<%# Eval("RGADROWID") %>'/>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SKU" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSKU" runat="server" Text='<%# Eval("SKUNumber") %>' OnTextChanged="txtSKU_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                                                                ServiceMethod="SearchSKUNumber"
                                                                MinimumPrefixLength="1"
                                                                ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                                                                CompletionInterval="100"
                                                                EnableCaching="true"
                                                                CompletionSetCount="10"
                                                                TargetControlID="txtSKU">
                                                            </asp:AutoCompleteExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pruduct Name">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtproductame" runat="server" Text='<%# Eval("ProductName") %>' Width="200"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivered Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtdeliveredquantity" runat="server" Text='<%#Eval("DeliveredQty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Return Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtreturnquantity"  runat="server" Text='<%#Eval("ReturnQty") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Product Return Reasons">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReasons" runat="server" Text='<%#Eval("ReturnReasons")%>' />
                                                            <asp:Label ID="lblreason" runat="server" Text=" Reasons" />
                                                             <asp:LinkButton ID="txtreasons" runat="server" Text="[ Edit ]" OnClick="txtreasons_Click"></asp:LinkButton>
                                                            <asp:HiddenField ID="hfReasonsID" runat="server" Value='<%#Eval("ReasonIDs")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCCCC" />
                                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                                                <RowStyle BackColor="White" />
                                                <SelectedRowStyle BackColor="#0099cc" Font-Bold="True" ForeColor="Black" />
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
            <td>
                <div class="border" style="height:50px;margin-top:5px">
                    <table style="width:100%">
                        <tr>
                            <td class="tdLeft">
                                  <asp:LinkButton ID="LinkButton1" Text="<< Back To RMA Return Detail"  runat="server" PostBackUrl="~/Forms/Web Forms/frmRetunDetail.aspx" ForeColor="White"></asp:LinkButton>
                            </td>
                            <td class="tdRight">
                                <asp:Button ID="btnupdate" runat="server" Text="Update" CssClass="btn" OnClick="btnupdate_Click" />
                            </td>

                        </tr>
                    </table>
                  
                </div>

            </td>

        </tr>
    </table>

    </div>
    </asp:Content>