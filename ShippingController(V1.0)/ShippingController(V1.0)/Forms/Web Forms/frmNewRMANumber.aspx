<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmNewRMANumber.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmNewRMANumber" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script>
        function CheckOtherIsCheckedByGVID(RadioButton1) {
            var isChecked = RadioButton1.checked;
            var row = RadioButton1.parentNode.parentNode;
            if (isChecked) {
                row.style.backgroundColor = '#B6C4DE';
                row.style.color = 'black';
            }
            var currentRdbID = RadioButton1.id;
            parent = document.getElementById("<%= gvReturnDetails.ClientID %>");
            var items = parent.getElementsByTagName('input');

            for (i = 0; i < items.length; i++) {
                if (items[i].id != currentRdbID && items[i].type == "radio") {
                    if (items[i].checked) {
                        items[i].checked = false;
                        items[i].parentNode.parentNode.style.backgroundColor = 'white';
                        items[i].parentNode.parentNode.style.color = '#696969';
                    }
                }
            }
        }
    </script>
    <script language="javascript" type="text/javascript">
        function window.confirm(str)
        {
            execScript('n = msgbox("'+str+'","4132")', "vbscript");
            return(n == 6);
        }
    </script>


    <div style="width: 100%; height: 800px">
        <table style="width: 1350px;">
            <tr>
                <td class="TitleStrip">Return Details
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <%--<asp:Label ID="Label5" runat="server" Text="Last Modified By"></asp:Label>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                 <asp:Label ID="lblUserName" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>

                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                 <asp:Label ID="lblLastTime" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>--%>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                  <asp:Button ID="Button2" runat="server" Text="Email" CssClass="btn" OnClick="btnEmail_Click" />&nbsp&nbsp&nbsp&nbsp
                                   
                    <asp:Button ID="Button3" runat="server" Text="Cancel" CssClass="btn" OnClientClick="javascript:return confirm('You want to exit without saving the records');" OnClick="btnOk_Click" />&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="Button1" runat="server" Text="Update" CssClass="btn" OnClick="btnupdate_Click" />

                </td>
            </tr>
            <tr>

                <td style="width: 50%" align="center">
                    <asp:Label ID="lblMassege" runat="server" Text="" Font-Bold="True" Font-Size="20px" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <div class="border">
                        <asp:UpdatePanel ID="updatePanelbtnComment" runat="server">
                            <ContentTemplate>
                                <table id="tblmain" runat="server" style="width: 70%; padding: 2px;" class="border">
                                    <tr>
                                        <td class="tdRight" style="width: 10%">
                                            <asp:Label ID="lblRGAnumber" runat="server" Text="RGA Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtrganumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%" class="tdRight">
                                            <asp:Label ID="lblRMANumber" runat="server" Text="RMA Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtRMAnumber" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <%--<td style="width:20%">
<asp:TextBox CssClass="txt" ID="TextBox3" runat="server" ReadOnly="true"></asp:TextBox>
</td>--%>
                                        <td class="auto-style1">
                                            <asp:Label ID="Label7" runat="server" Text="Comment" CssClass="lbl"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 20%" class="tdRight">
                                            <asp:Label ID="lblRMAstatus" runat="server" Text="RMA Status" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddlstatus" runat="server" Width="127px" AutoPostBack="True">
                                                <asp:ListItem Value="0">Incomplete</asp:ListItem>
                                                <asp:ListItem Value="1">Complete</asp:ListItem>
                                                <asp:ListItem Value="2">Wrong RMA</asp:ListItem>
                                                <asp:ListItem Value="3">To Process</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdRight" style="width: 20%">
                                            <asp:Label ID="lblshipment" runat="server" Text="Shipment Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtshipmentnumber" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td rowspan="4" class="auto-style1">
                                            <asp:TextBox ID="txtcomment" runat="server" TextMode="MultiLine" Height="80"></asp:TextBox>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td class="tdRight" style="width: 20%">
                                            <asp:Label ID="Label3" runat="server" Text="RMA Decision" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:DropDownList ID="ddldecision" runat="server" Width="127px" AutoPostBack="True">
                                                <asp:ListItem Value="0">Pending</asp:ListItem>
                                                <asp:ListItem Value="1">Deny</asp:ListItem>
                                                <asp:ListItem Value="2">Full Refund</asp:ListItem>
                                                <asp:ListItem Value="3">Partial-Refund</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20%" class="tdRight">
                                            <asp:Label ID="Label2" runat="server" Text="PO Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtponumber" runat="server" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="width: 20%" class="tdRight">
                                            <asp:Label ID="Label1" runat="server" Text="Return Date" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreturndate"></asp:CalendarExtender>
                                            <asp:TextBox ID="txtreturndate" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="tdRight" style="width: 20%">
                                            <asp:Label ID="lblorderdate" runat="server" Text="Order Date" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox  ID="txtorderdate" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="tdRight" style="width: 20%">
                                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtcustomerName" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td style="width: 20%" class="tdRight">
                                            <asp:Label ID="lblVendorname" runat="server" Text="Vendor Name" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 20%">
                                            <asp:TextBox ID="txtvendorName" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="tdRight" style="width: 10%">
                                            <asp:Label ID="lblordernumber" runat="server" Text="Order Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:TextBox ID="txtordernumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="tdRight" style="width: 10%">
                                            <asp:Label ID="lblvendornumber" runat="server" Text="Vendor Number" CssClass="lbl"></asp:Label>
                                            <br />
                                        </td>
                                        <td style="width: 10%">
                                            <asp:TextBox ID="txtvendornumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <%--<td style="width:10%">
<asp:TextBox CssClass="txt" ID="TextBox5" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
</td>--%>

                                        <td class="auto-style1">
                                            <asp:Button ID="btnComment" runat="server" Text="Add Comment" CssClass="btn" Visible="true" Width="100" OnClick="btnComment_Click" />&nbsp
                                   <%-- <asp:Button ID="btnPrevious" CssClass="btn" runat="server" Text="Previous" Width="100" OnClick="btnPrevious_Click" />--%>
                                        </td>

                                    </tr>

                                    <tr>
                                        <td colspan="3" height="50">
                                            <asp:Label ID="Label4" runat="server" Text="Call tag" CssClass="lbl"></asp:Label>
                                            &nbsp&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtCalltag" runat="server" Width="340px"></asp:TextBox>
                                            &nbsp&nbsp&nbsp&nbsp
                                <asp:CheckBox ID="chkflag" Text="Flag" Font-Bold="true" Font-Size="20" runat="server" ForeColor="#0A0909" />
                                        </td>
                                        <td colspan="2">&nbsp&nbsp&nbsp&nbsp
                               
                                 &nbsp&nbsp&nbsp&nbsp
                                
                            &nbsp;
                                        </td>

                                    </tr>

                                    <tr>
                                        <td id="Td1" align="center" style="width: 10%" colspan="2" runat="server"></td>

                                        <td id="Td2" style="width: 10%" colspan="2" runat="server" align="center">
                                            <%-- <asp:Label ID="Label8" runat="server" Text="Vendor Number" CssClass="lbl"></asp:Label>--%>
                                    
                                        </td>


                                        <td id="Td3" align="center" runat="server" style="width: 250px">
                                            <%--<asp:Button ID="Button1" runat="server" Text="Add Comment" OnClick="btnComment_Click" />--%>
                                    
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnComment" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </td>

            </tr>
            <tr>
                <td colspan="5" class="TitleStrip">Return Details 

                 <asp:Button ID="btnaddnew" runat="server" Text="Add new product" CssClass="btn" OnClick="btnaddnew_Click" Width="135px" />
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:TextBox ID="txtNewItem" runat="server" Visible="false"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                        ServiceMethod="SearchSKUNumber"
                        MinimumPrefixLength="1"
                        ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                        CompletionInterval="100"
                        EnableCaching="true"
                        CompletionSetCount="10"
                        TargetControlID="txtNewItem">
                    </asp:AutoCompleteExtender>


                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                <asp:Button ID="BtnAddNewItem" runat="server" Text="Add" Visible="false" CssClass="btn" OnClick="BtnAddNewItem_Click" />

                </td>
            </tr>
            <tr>
                <td>
                    <div class="border" id="Div2" style="height: 400px; overflow: scroll" onscroll="SetDivPosition()">
                        <asp:Panel ID="panel1" runat="server" Height="400px">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvReturnDetails" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                                        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                                        ForeColor="Black" AllowSorting="True" OnSelectedIndexChanged="gvReturnDetails_SelectedIndexChanged">

                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <%-- <asp:RadioButton ID="rdbselect" runat="server" OnCheckedChanged="RadioButton1_CheckedChanged" />--%>
                                                    <asp:RadioButton ID="RadioButton1" GroupName="test" AutoPostBack="true" OnCheckedChanged="RadioButton1_CheckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                                        runat="server" />
                                                </ItemTemplate>
                                                <ControlStyle Width="50px" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Return Detail Number">
                                                <ItemTemplate>
                                                    <asp:TextBox Enabled="false" ID="txtRGANumberID" runat="server" Text='<%# Eval("RGADROWID") %>' />
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SKU">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSKU" runat="server" Text='<%# Eval("SKUNumber") %>' OnTextChanged="txtSKU_TextChanged" AutoPostBack="True" Width="25"></asp:TextBox>
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
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSKU_Qty_Seq" runat="server" Text='<%#Eval("SKU_Qty_Seq") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSKU_Status" runat="server" Enabled="false" Text='<%#Eval("SKU_Status") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="ProductID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtProductID" runat="server" Enabled="false" Text='<%#Eval("ProductID") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="SKU Sequence" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSKU_Sequence" runat="server" Text='<%#Eval("SKU_Sequence") %>' Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="70px" />
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField HeaderText="ProductID">
<ItemTemplate>
<asp:TextBox ID="txtProductID" runat="server" Text='<%#Eval("ProductID") %>'></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Sales Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSalesPrice" runat="server" Text='<%#Eval("SalesPrice") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="70px" />
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No. of images">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="txtImageCount" runat="server" Text='<%#Eval("NoofImages") %>' OnClick="txtImageCount_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ControlStyle Width="70px" />
                                                <ItemStyle Width="70px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload Images">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="FileUpload1" runat="server" OnLoad="FileUpload1_Load" AllowMultiple="true" />
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Upload Image" OnClick="btnUpdate_Click1" Enabled="false" />
                                                    <div style="width: 10%; height: 50%">
                                                        <asp:Label ID="lblImagesName" runat="server" Height="50%" Width="10%" ForeColor="Red" Text='<%# Eval("ImageName") %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <ControlStyle Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LT" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLineType" Enabled="false" runat="server" Text='<%#Eval("LineType") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SL" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtShipmentLines" runat="server" Enabled="false" Text='<%#Eval("ShipmentLines") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="RL" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReturnLines" Enabled="false" runat="server" Text='<%#Eval("ReturnLines") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Guid" Visible="false">
                                                <ItemTemplate>
                                                    <%--<asp:TextBox ID="txtReturnLines" runat="server" Text='<%#Eval("ReturnLines") %>'></asp:TextBox>--%>
                                                    <asp:Label ID="lblguid" Enabled="false" runat="server" Text='<%#Eval("ReturnDetailID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Product Return Reasons">
<ItemTemplate>
<asp:LinkButton ID="txtreasons" runat="server" Text="[ Edit Reasons]" OnClick="txtreasons_Click"></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>--%>
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

                                </ContentTemplate>
                                <Triggers>

                                    <%-- <asp:AsyncPostBackTrigger ControlID = "btnAsyncUpload"

          EventName = "Click" />--%>

                                    <asp:PostBackTrigger ControlID="gvReturnDetails" />

                                </Triggers>
                            </asp:UpdatePanel>
                        </asp:Panel>


                    </div>

                </td>

            </tr>
            <tr>
                <td>
                    <div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                            <ContentTemplate>

                                <table id="Table1" style="width: 100%" runat="server" name="tblm">
                                    <tr>
                                        <td colspan="5">
                                            <table style="width: 50%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="lblitemNew" Text="Item is New" runat="server" CssClass="lbl" />
                                                    </td>
                                                    <td style="width: 30%">
                                                        <%--<asp:CheckBox ID="chkitemordered" Text="Incorrect item ordered." runat="server" CssClass="lbl" />--%>

                                                        <asp:RadioButtonList ID="brdItemNew" runat="server" RepeatDirection="Horizontal" CssClass="lbl" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="brdItemNew_SelectedIndexChanged">
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </td>
                                                    <%--<td style="width:30%">
                            <asp:CheckBox ID="chkwrongitem" Text="Received wrong item." runat="server" CssClass="lbl"/>
                        </td>--%>
                                                </tr>

                                            </table>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <table style="width: 50%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="lblInstalled" Text="Installed" runat="server" CssClass="lbl" />
                                                    </td>
                                                    <td style="width: 30%">
                                                        <%--<asp:CheckBox ID="CheckBox1" Text="Incorrect item ordered." runat="server" CssClass="lbl" />--%>
                                                        <asp:RadioButtonList ID="brdInstalled" runat="server" RepeatDirection="Horizontal" Width="300px" CssClass="lbl" AutoPostBack="true" OnSelectedIndexChanged="brdInstalled_SelectedIndexChanged">
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </td>
                                                    <%--   <td style="width:30%">
                            <asp:CheckBox ID="CheckBox2" Text="Received wrong item." runat="server" CssClass="lbl"/>
                        </td>--%>
                                                </tr>

                                            </table>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <table style="width: 50%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="lblstatus" Text="Chip/Bended/Scratch/Broken" runat="server" CssClass="lbl" />
                                                    </td>
                                                    <td style="width: 30%">
                                                        <%--<asp:CheckBox ID="CheckBox3" Text="Incorrect item ordered." runat="server" CssClass="lbl" />--%>

                                                        <asp:RadioButtonList ID="brdstatus" runat="server" RepeatDirection="Horizontal" Width="300px" CssClass="lbl" AutoPostBack="true" OnSelectedIndexChanged="brdstatus_SelectedIndexChanged">
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:RadioButtonList>

                                                    </td>
                                                    <%-- <td style="width:30%">
                           <asp:CheckBox ID="CheckBox4" Text="Received wrong item." runat="server" CssClass="lbl"/>
                        </td>--%>
                                                </tr>

                                            </table>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <table style="width: 50%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="lblManifacturerDefective" Text="Manufacturer Defective" runat="server" CssClass="lbl" />
                                                    </td>
                                                    <td style="width: 30%">
                                                        <%-- <asp:CheckBox ID="CheckBox5" Text="Incorrect item ordered." runat="server" CssClass="lbl" />--%>
                                                        <asp:RadioButtonList ID="brdManufacturer" runat="server" RepeatDirection="Horizontal" Width="300px" CssClass="lbl" AutoPostBack="true" OnSelectedIndexChanged="brdManufacturer_SelectedIndexChanged">
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:RadioButtonList>


                                                    </td>
                                                    <%--  <td style="width:30%">
                            <asp:CheckBox ID="CheckBox6" Text="Received wrong item." runat="server" CssClass="lbl"/>
                        </td>--%>
                                                </tr>

                                            </table>

                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <table style="width: 50%">
                                                <tr>
                                                    <td style="width: 30%">
                                                        <asp:Label ID="lblDefectintransite" Text="Defect in Transite" runat="server" CssClass="lbl" />
                                                    </td>
                                                    <td style="width: 30%">
                                                        <%--<asp:CheckBox ID="chkduplicate" Text="Duplicate Shipment." runat="server" CssClass="lbl"/>--%>
                                                        <asp:RadioButtonList ID="brdDefecttransite" runat="server" RepeatDirection="Horizontal" Width="300px" CssClass="lbl" AutoPostBack="true" OnSelectedIndexChanged="brdDefecttransite_SelectedIndexChanged">
                                                            <asp:ListItem>Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>

                                                </tr>
                                            </table>
                                        </td>
                                    </tr>



                                    <tr>
                                        <td>
                                            <asp:Label ID="lblotherreasons" runat="server" Text="Enter Other Reasons  :" CssClass="lbl"></asp:Label>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            
                 <asp:TextBox CssClass="txt" ID="txtotherreasons" runat="server" Width="242px"></asp:TextBox>
                                            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            
                <asp:DropDownList ID="ddlotherreasons" runat="server" Style="width: 20%" AutoPostBack="True" OnSelectedIndexChanged="ddlotherreasons_SelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                    </tr>


                                </table>

                                <table style="width: 50%">
                                    <tr>
                                        <td colspan="5">

                                            <tr>
                                                <td style="width: 30%">
                                                    <%-- <asp:Label ID="Label5" Text="Defect in Transite." runat="server" CssClass="lbl"/>--%>
                                                </td>
                                                <td style="width: 30%">
                                                    <%--<asp:CheckBox ID="chkduplicate" Text="Duplicate Shipment." runat="server" CssClass="lbl"/>--%>
                                                    <%-- <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Width="300px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>--%>

                                                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn" OnClick="btnsubmit_Click" Enabled="false" />


                                                </td>

                                            </tr>

                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
            </tr>

            <tr>
                <td>
                    <div class="border" style="height: 50px; margin-top: 5px">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 100%" align="right">
                                    <%-- <asp:LinkButton ID="LinkButton1" Text="<< Back To RMA Return Detail" runat="server" PostBackUrl="~/Forms/Web Forms/frmRetunDetail.aspx" ForeColor="Blue"></asp:LinkButton>--%>



                                    <asp:Button ID="btnEmail" runat="server" Text="Email" CssClass="btn" OnClick="btnEmail_Click" />
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                                                
                                    <asp:Button ID="btnCancle" runat="server" Text="Cancel" CssClass="btn" OnClientClick="javascript:return confirm('You want to exit without saving the records');" OnClick="btnOk_Click" />
                                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                             
                                    <asp:Button ID="btnupdate" runat="server" Text="Save" CssClass="btn" OnClick="btnupdate_Click" />
                            </tr>
                        </table>

                    </div>

                </td>

            </tr>
        </table>
        <asp:Label ID="lblresult" runat="server" Font-Bold="True" ForeColor="Blue" Font-Size="20" />
        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
            CancelControlID="lnkSaveCont" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="400px" Style="display: none">
            <table width="100%" style="border: Solid 2px #D46900; background-color: white; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr style="background-image: url(Images/header.gif)">
                    <td style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger; font-family: Calibri" align="Left">Confirm Box</td>
                    <td style="color: White; font-weight: bold; padding: 3px; font-size: larger" align="Right">
                        <a href="javascript:void(0)" onclick="closepopup()">
                            <img src="../../images/close.jpg" style="border: 0px" align="right" /></a>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri">
                        <asp:Label ID="lblUser" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td></td>
                    <td align="right" style="padding-right: 15px" backcolor="White">
                        <asp:LinkButton ID="lnkSaveCont" runat="server" Font-Bold="True" Font-Size="15px" PostBackUrl="~/Forms/Web Forms/frmReturnEdit.aspx">Save&Continue</asp:LinkButton>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
   <asp:LinkButton ID="lnkSaveex" runat="server" Font-Bold="True" Font-Size="15px" PostBackUrl="~/Forms/Web Forms/frmRetunDetail.aspx">Save&Exit</asp:LinkButton>
                        <%--<a id="lnkSaveExt" href="frmRetunDetail.aspx" style="font-size: 15px; text-decoration: underline; color: #0000FF">Save&Exit </a>--%>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
