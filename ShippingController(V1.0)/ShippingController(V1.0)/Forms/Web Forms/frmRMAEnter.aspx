<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmRMAEnter.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmRMAEnter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modalBackground
        {
            position: absolute;
            top: 0px;
            left: 0px;
            filter: alpha(opacity=60);
            -moz-opacity: 0.6;
            opacity: 0.6;
        }
        .popup
        {
            background-color: #ddd;
            margin: 0px auto;
            width: 330px;
            position: relative;
            border: Gray 2px inset;
        }
    </style>
    <style type="text/css">
    .hiddencol
        {
            display: none;
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Border" class="border" style="width:80% ; float:none">
     <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table style="width:100%">
        <tr>
            <td colspan="5" class="TitleStrip">Return Details Information (RMA) 
                </td>
        </tr>
        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lblRMAnumber" runat="server" Text="RMA Number  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtrmanumber" runat="server" ReadOnly="true" Text="Generate after saving this information." ForeColor="Red" Width="218px"></asp:TextBox>
            </td>
            <td style="width:10%" class="tdRight">
                <asp:Label ID="lblvendernumber" runat="server" Text="Vender Number  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtvendernumber" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lblstatus" runat="server" Text="Status  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:DropDownList ID="ddlstatus" runat="server" Width="127px" AutoPostBack="True">
                                    <asp:ListItem Value="0">New</asp:ListItem>
                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                    <asp:ListItem Value="2">Pending</asp:ListItem>
                                    <asp:ListItem Value="3">Canceled</asp:ListItem>
                                </asp:DropDownList>
            </td>
            <td style="width:10%" class="tdRight">
                <asp:Label ID="lblvendername" runat="server" Text="Vender Name  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtvendername" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lbldecisision" runat="server" Text="Decision  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:DropDownList ID="ddldecision" runat="server" Width="127px" AutoPostBack="True">
                                    <asp:ListItem Value="0">New</asp:ListItem>
                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                    <asp:ListItem Value="2">Pending</asp:ListItem>
                                    <asp:ListItem Value="3">Canceled</asp:ListItem>
                                </asp:DropDownList>
            </td>
            <td style="width:10%" class="tdRight">
                <asp:Label ID="lblrequestdate" runat="server" Text="Request Date  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtrequestdate" runat="server" ></asp:TextBox>
                <asp:CalendarExtender ID="calredusetdate" runat="server" TargetControlID="txtrequestdate" Format="MMM dd, yyyy"></asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="TitleStrip">PO/Order Detail (RMA) 
               </td>
        </tr>

        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lblponumber" runat="server" Text="PO Number  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtponumber" runat="server" ></asp:TextBox>
            </td>
            <td style="width:10%" class="tdRight">
                <asp:Label ID="lblcustomername" runat="server" Text="Customer Name  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtcustomername" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lblcustomeraddress" runat="server" Text="Customer Address  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtcustomeraddress" runat="server" ></asp:TextBox>
            </td>
            <td style="width:10%" class="tdRight">
                <asp:Label ID="lblcity" runat="server" Text="City  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtcity" runat="server"  ></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lblstate" runat="server" Text="State/Province  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtstate" runat="server" ></asp:TextBox>
            </td>
            <td style="width:10%" class="tdRight">
                <asp:Label ID="lblzipcode" runat="server" Text="Zip/Postal Code  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtzipcode" runat="server" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:10%" class="tdRight">
                 <asp:Label ID="lblcountry" runat="server" Text="Country  :" CssClass="lbl" ></asp:Label>
            </td>
            <td style="width:20%">
                <asp:TextBox CssClass="txt" ID="txtcountry" runat="server" ></asp:TextBox>
            </td>
            <td style="width:10%" class="tdRight" colspan="2">
               
            </td>
            
        </tr>
         <tr>
            <td colspan="5" class="TitleStrip">Return Details 

                 <asp:Button ID="btnaddnew" runat="server" Text="Add new product" CssClass="btn"  OnClick="btnaddnew_Click" Width="135px"  />
               </td>
        </tr>
        <tr>
            <td colspan="5" align="center" > 
               
                                     
                <asp:GridView ID="gvReturnDetails" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                    ForeColor="Black" AllowSorting="true" RowStyle-Height="30%" RowStyle-VerticalAlign="Top">
                    <Columns>
                        <asp:TemplateField HeaderText="SKU">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSKU" runat="server" Text='<%# Eval("SKU") %>' AutoPostBack="True" OnTextChanged="txtSKU_TextChanged" >

                                </asp:TextBox>
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
                        <asp:TemplateField HeaderText="Product Name">
                            <ItemTemplate>
                                <asp:TextBox ID="txtproductname" runat="server" Text='<%# Eval("ProductName") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:TextBox ID="txtquantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reasons">
                            <ItemTemplate>
                                <asp:LinkButton ID="txtreasons" runat="server" Text='<%# Eval("Reasons") %>' OnClick="txtreasons_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:TextBox ID="txtcategory" runat="server" Text='<%# Eval("Category") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SKU" ItemStyle-CssClass="hiddencol" HeaderStyle-CssClass="hiddencol">
                            <ItemTemplate>
                                <asp:TextBox ID="txtskureasons" runat="server" Text='<%# Eval("SKUID") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Upload Images">
                            <ItemTemplate>
                                <asp:FileUpload ID="FileUpload1" runat="server" OnLoad="FileUpload1_Load"/>
                                <asp:Button ID="btnUpdate" runat="server" Text="Upload Image" OnClick="btnUpdate_Click" Enabled="false"/>
                                <div style="width: 10%; height: 50%">
                                    <asp:Label ID="lblImagesName" runat="server" Height="50%" Width="10%" ForeColor="Red" Text='<%# Eval("ImageName") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" OnClick="lnkDelete_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>

         <tr>
            <td colspan="5" class="TitleStrip">Reason(s) for Return 
               </td>
        </tr>
        <tr>
            <td colspan="5">
                <table style="width:90%">
                    <tr>
                        <td style="width:30%" >
                            <asp:CheckBox ID="chkitemdamaged" Text="Item Damaged." runat="server"  CssClass="chekbox"/>
                        </td >
                        <td style="width:30%" >
                            <asp:CheckBox ID="chkitemordered" Text="Incorrect item ordered." runat="server" CssClass="chekbox" />
                        </td>
                        <td style="width:30%">
                            <asp:CheckBox ID="chkwrongitem" Text="Received wrong item." runat="server" CssClass="chekbox"/>
                        </td>
                    </tr>

                </table>

            </td>

        </tr>
        <tr>
            <td colspan="5">
                <table style="width:90%">
                    <tr>
                        <td style="width:30%">
                            <asp:CheckBox ID="chkitemdifferent" Text="Item is different from displayed on web." runat="server" CssClass="chekbox"/>
                        </td >
                        <td style="width:30%" >
                            <asp:CheckBox ID="chkduplicate" Text="Duplicate Shipment." runat="server" CssClass="chekbox"/>
                        </td>
                        <td style="width:30%">
                            <asp:CheckBox ID="chknotsatisfied" Text="Not satisfied with item." runat="server" CssClass="chekbox" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr >
            <td class="tdRight">
                 <asp:Label ID="lblotherreasons" runat="server" Text="Enter Other Reasons  :" CssClass="lbl" ></asp:Label>
            </td>
            <td>
                 <asp:TextBox CssClass="txt" ID="txtotherreasons" runat="server" Width="242px" ></asp:TextBox>
            </td>
            <td colspan="2">
                <asp:DropDownList ID="ddlotherreasons" runat="server" style="width:50%" AutoPostBack="True" OnSelectedIndexChanged="ddlotherreasons_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="5">
                <table style="width:100%">
                    <tr>
                        <td style="width:25%">

                        </td>
                        <td style="width:25%" align="center">
                            <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" OnClick="btnsave_Click"  />
                        </td>
                        <td style="width:25%" align="center">
                            <asp:Button ID="btncancle" runat="server" Text="Cancel" CssClass="btn" OnClick="btncancle_Click"  />
                        </td>
                        <td style="width:25%">

                        </td>
                    </tr>
                </table>
            </td>
        </tr>

    </table>
        </div>
     
     <div>
        <asp:Panel ID="pnModelPopup" runat="server" CssClass="popup" Visible="false">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:CheckBoxList ID="chkreasons" runat="server" Height="45px" Width="193px"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                   <asp:Button ID="btnAdd" runat="server" Text="Add"  Style="margin-left: 100px" OnClick="btnAdd_Click"
                     />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        
      
    </div>
      </div>
</asp:Content>
