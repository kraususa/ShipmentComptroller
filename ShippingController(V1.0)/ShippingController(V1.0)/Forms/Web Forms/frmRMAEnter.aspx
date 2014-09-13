<%@ Page Title="" Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmRMAEnter.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmRMAEnter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modalBackground {
            position: absolute;
            top: 0px;
            left: 0px;
            filter: alpha(opacity=60);
            -moz-opacity: 0.6;
            opacity: 0.6;
        }

        .popup {
            background-color: #ddd;
            margin: 0px auto;
            width: 330px;
            position: relative;
            border: Gray 2px inset;
            top: -2px;
            left: 0px;
        }
    </style>
    <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function Confirm() {
            var confirm_valuefor_delete = document.createElement("INPUT");
            confirm_valuefor_delete.type = "hidden";
            confirm_valuefor_delete.name = "confirm_valuefor_delete";
            if (confirm("Are you sure want to delete?")) {
                confirm_valuefor_delete.value = "Yes";
            } else {
                confirm_valuefor_delete.value = "No";
            }
            document.forms[0].appendChild(confirm_valuefor_delete);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="Border" class="border" style="width: 80%; float: none">
        <div>
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
            <table style="width: 100%">
                <%-- <tr>
                <td class="TitleStrip">Return Details Information Update (RMA)
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:Label ID="Label5" runat="server" Text="The Last User is"></asp:Label>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:Label ID="lblUserName" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>
                   

                </td>
            </tr>--%>
                <%--<tr>

                <td style="width: 50%" align="center">
                    <asp:Label ID="lblMassege" runat="server" Text="" Font-Bold="True" Font-Size="20px" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>--%>
                <tr>
                    <td colspan="5" class="TitleStrip">
                        <asp:Label ID="lblMassege" runat="server" Text="" Font-Bold="True" Font-Size="20px" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="TitleStrip">Return Details Information (RMA) 
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblRMAnumber" runat="server" Text="RMA Number  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtrmanumber" runat="server" ReadOnly="true" Text="Generated after saving this information." ForeColor="Red" Width="218px"></asp:TextBox>
                    </td>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblvendernumber" runat="server" Text="Vender Number  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtvendernumber" runat="server" AutoPostBack="true" OnTextChanged="txtvendernumber_TextChanged">
                        </asp:TextBox>
                        <asp:AutoCompleteExtender runat="server" ID="autoVenderNumber"
                            ServiceMethod="SearchVenderNumber"
                            MinimumPrefixLength="1"
                            ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                            CompletionInterval="100"
                            EnableCaching="true"
                            CompletionSetCount="10"
                            TargetControlID="txtvendernumber">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblstatus" runat="server" Text="Status  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:DropDownList ID="ddlstatus" runat="server" Width="127px" AutoPostBack="True">
                            <asp:ListItem Value="0">New</asp:ListItem>
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Value="2">Pending</asp:ListItem>
                            <asp:ListItem Value="3">Canceled</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblvendername" runat="server" Text="Vender Name  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtvendername" runat="server" AutoPostBack="true" OnTextChanged="txtvendername_TextChanged" Width="250px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender000" runat="server"
                            ServiceMethod="SearchVen"
                            MinimumPrefixLength="1"
                            ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                            CompletionInterval="100"
                            EnableCaching="true"
                            CompletionSetCount="10"
                            TargetControlID="txtvendername">
                        </asp:AutoCompleteExtender>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lbldecisision" runat="server" Text="Decision  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:DropDownList ID="ddldecision" runat="server" Width="127px" AutoPostBack="True">
                            <asp:ListItem Value="0">New</asp:ListItem>
                            <asp:ListItem Value="1">Approved</asp:ListItem>
                            <asp:ListItem Value="2">Pending</asp:ListItem>
                            <asp:ListItem Value="3">Canceled</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblrequestdate" runat="server" Text="Request Date  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtrequestdate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="calredusetdate" runat="server" TargetControlID="txtrequestdate" Format="MMM dd, yyyy"></asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="TitleStrip">PO/Order Detail (RMA) 
                    </td>
                </tr>

                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblponumber" runat="server" Text="PO Number  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtponumber" runat="server" AutoPostBack="true" OnTextChanged="txtponumber_TextChanged">
                        </asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server"
                            ServiceMethod="SearchPONumber"
                            MinimumPrefixLength="1"
                            ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                            CompletionInterval="100"
                            EnableCaching="true"
                            CompletionSetCount="10"
                            TargetControlID="txtponumber">
                        </asp:AutoCompleteExtender>

                    </td>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblcustomername" runat="server" Text="Customer Name  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtcustomername" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblcustomeraddress" runat="server" Text="Customer Address  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtcustomeraddress" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblcity" runat="server" Text="City  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtcity" runat="server"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblstate" runat="server" Text="State/Province  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtstate" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblzipcode" runat="server" Text="Zip/Postal Code  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtzipcode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 10%" class="tdRight">
                        <asp:Label ID="lblcountry" runat="server" Text="Country  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:TextBox ID="txtcountry" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 10%" class="tdRight" colspan="2"></td>

                </tr>
                <tr>
                    <td colspan="5">
                          <asp:Label ID="Label4" runat="server" Text="Call tag" CssClass="lbl"></asp:Label>
                                    &nbsp&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtCalltag" runat="server" Width="500px"></asp:TextBox>
                                    &nbsp&nbsp&nbsp&nbsp
                                <asp:CheckBox ID="chkflag" Text="Flag" Font-Bold="true" Font-Size="20" runat="server" ForeColor="White" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="TitleStrip">Return Details 

                 <%--<asp:Button ID="btnaddnew" runat="server" Text="Add new product" CssClass="btn"  OnClick="btnaddnew_Click" Width="135px"  />--%>
                        <asp:Button ID="btnaddnew" runat="server" Text="Add new product" CssClass="btn" OnClick="btnaddnew_Click1" Width="135px" />
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                <asp:TextBox ID="txtNewItem" runat="server" Visible="false"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                            ServiceMethod="SearchSKUNumber"
                            MinimumPrefixLength="1"
                            ServicePath="~/Forms/Web Forms/AutoCompleteService.aspx"
                            CompletionInterval="100"
                            EnableCaching="true"
                            CompletionSetCount="10"
                            TargetControlID="txtNewItem">
                        </asp:AutoCompleteExtender>


                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                <asp:Button ID="BtnAddNewItem" runat="server" Text="Add" Visible="false" OnClick="BtnAddNewItem_Click" />

                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="center">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvReturnDetails" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                                    ForeColor="Black" AllowSorting="True">

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
                                        <%-- <asp:TemplateField HeaderText="Return Detail Number">
                                                <ItemTemplate>
                                                    <asp:TextBox Enabled="false" ID="txtRGANumberID" runat="server" Text='<%# Eval("RGADROWID") %>' />
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>--%>
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
                                        <%-- <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSKU_Status" runat="server" Enabled="false" Text='<%#Eval("SKU_Status") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>--%>


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
                                        <asp:TemplateField HeaderText="LT" Visible="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLineType" Enabled="false" runat="server" Text='<%#Eval("LineType") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SL" Visible="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtShipmentLines" runat="server" Enabled="false" Text='<%#Eval("ShipmentLines") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RL" Visible="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReturnLines" Enabled="false" runat="server" Text='<%#Eval("ReturnLines") %>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ControlStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>

                                        <%--  <asp:TemplateField HeaderText="Guid" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReturnLines" runat="server" Text='<%#Eval("ReturnLines") %>'></asp:TextBox>
                                                    <asp:Label ID="lblguid" Enabled="false" runat="server" Text='<%#Eval("ReturnDetailID") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ControlStyle Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </asp:TemplateField>--%>

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

                        <%--<asp:GridView ID="gvReturnDetails" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                    BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                    ForeColor="Black" AllowSorting="true" RowStyle-Height="40px" RowStyle-VerticalAlign="Top"
                    
                    >
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
                                <asp:TextBox ID="txtproductname" runat="server" Text='<%# Eval("ProductName") %>' ReadOnly="true"></asp:TextBox>
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
                                <asp:LinkButton ID="lnkDelete" Text="Delete" runat="server" OnClick="lnkDelete_Click" OnClientClick="Confirm()"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="White" />
                </asp:GridView>--%>
                    </td>
                </tr>

                <tr>
                    <td colspan="5" class="TitleStrip">Reason(s) for Return 
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <div>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <table id="Table1" style="width: 100%" runat="server" name="tblm">
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblitemNew" Text="Item is new" runat="server" CssClass="lbl" />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:RadioButtonList ID="brdItemNew" ForeColor="Black" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="brdItemNew_SelectedIndexChanged">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>
                                            <%-- <td style="width: 30%">

                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblInstalled" Text="Installed" runat="server" CssClass="lbl" />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:RadioButtonList ID="brdInstalled" ForeColor="Black" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="brdInstalled_SelectedIndexChanged">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>
                                            <%-- <td style="width: 30%">

                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblReasonstatus" Text="Chip/Bended/Scratch/Broken" runat="server" CssClass="lbl" />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:RadioButtonList ID="brdstatus" ForeColor="Black" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="brdReasonstatus_SelectedIndexChanged">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>
                                            <%-- <td style="width: 30%">

                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblManifacturerDefective" Text="Manufacturer Defective" runat="server" CssClass="lbl" />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:RadioButtonList ID="brdManufacturer" ForeColor="Black" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="brdManufacturer_SelectedIndexChanged">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>
                                            <%-- <td style="width: 30%">

                                            </td>--%>
                                        </tr>
                                        <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblDefectintransite" Text="Defect in Transite" runat="server" CssClass="lbl" />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:RadioButtonList ID="brdDefecttransite" ForeColor="Black" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true" OnSelectedIndexChanged="brdDefecttransite_SelectedIndexChanged">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>
                                            <%-- <td style="width: 30%">

                                            </td>--%>
                                        </tr>
                                       <%-- <tr>
                                            <td style="width: 30%">
                                                <asp:Label ID="lblNotSatisfied" Text="Not Satisfied with Item" runat="server" CssClass="lbl" />
                                            </td>
                                            <td style="width: 30%">
                                                <asp:RadioButtonList ID="brdNotSatisfied" ForeColor="Black" runat="server" RepeatDirection="Horizontal" Width="300px" AutoPostBack="true">
                                                    <asp:ListItem>Yes</asp:ListItem>
                                                    <asp:ListItem>No</asp:ListItem>
                                                </asp:RadioButtonList>

                                            </td>--%>
                                            <%-- <td style="width: 30%">

                                            </td>--%>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
                <%-- <tr>
                    <td colspan="5">
                        <table style="width: 90%">
                            <tr>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chkitemdamaged" Text="Item Damaged." runat="server" CssClass="lbl" />
                                </td>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chkitemordered" Text="Incorrect item ordered." runat="server" CssClass="lbl" />
                                </td>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chkwrongitem" Text="Received wrong item." runat="server" CssClass="lbl" />
                                </td>
                            </tr>

                        </table>

                    </td>

                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 90%">
                            <tr>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chkitemdifferent" Text="Item is different from displayed on web." runat="server" CssClass="lbl" />
                                </td>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chkduplicate" Text="Duplicate Shipment." runat="server" CssClass="lbl" />
                                </td>
                                <td style="width: 30%">
                                    <asp:CheckBox ID="chknotsatisfied" Text="Not satisfied with item." runat="server" CssClass="lbl" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>--%>
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="lblotherreasons" runat="server" Text="Enter Other Reasons  :" CssClass="lbl"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox CssClass="txt" ID="txtotherreasons" runat="server" Width="242px"></asp:TextBox>
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlotherreasons" runat="server" Style="width: 50%" AutoPostBack="True" OnSelectedIndexChanged="ddlotherreasons_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                         <table style="width: 50%">
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

                                                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClick="btnsubmit_Click" Enabled="true" />


                                                    </td>

                                                </tr>
                                            </table>

                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 25%"></td>
                                <td style="width: 25%" align="center">
                                    <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btn" OnClick="btnsave_Click" />
                                </td>
                                <td style="width: 25%" align="center">
                                    <asp:Button ID="btncancle" runat="server" Text="Cancel" CssClass="btn" OnClick="btncancle_Click" />
                                </td>
                                <td style="width: 25%"></td>
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
                            <asp:Button ID="btnAdd" runat="server" Text="Add" Style="margin-left: 100px" OnClick="btnAdd_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>


        </div>
    </div>
</asp:Content>
