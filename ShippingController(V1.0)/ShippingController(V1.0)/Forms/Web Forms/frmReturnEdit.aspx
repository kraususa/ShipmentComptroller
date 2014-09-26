<%@ Page Language="C#" MasterPageFile="~/Forms/Master Forms/Admin.Master" AutoEventWireup="true" CodeBehind="frmReturnEdit.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmReturnEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function SelectItem(id) {
            var button = document.getElementById(id);
            button.click();
            // window.open(imagename, 'popUpWindow', 'scrollbars=no,width=900,height=900,toolbars=no');
        }



    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
    <script>
        function demoDisplay()
        {
            document.getElementById("myP").style.display="none";
            $("#tblmg").empty();
        }
    </script>
    <style>
        button.image1 {
            background-image: url(Themes/Images/close.jpg);
            background-repeat: no-repeat;
        }
    </style>
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
        <table style="width: 1460px;">
            <tr>
                <td>
                    <asp:LinkButton ID="lkbtnPath2" runat="server" Text="Return Details Edit" BackColor="white" CssClass="link" Style="color: black" BorderColor="blue"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td class="TitleStrip">&nbsp;&nbsp;&nbsp;
              
                <asp:Label ID="Label5" runat="server" Text="Last Modified By"></asp:Label>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                 <asp:Label ID="lblUserName" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>

                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                 <asp:Label ID="lblLastTime" runat="server" Text="" Font-Bold="true" ForeColor="White"></asp:Label>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button ID="Button16" runat="server" Text="Print" OnClick="btnPrint_Click" CssClass="btn" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button17" runat="server" Text="Reprint Label" CssClass="btn" OnClick="btnReprint_Click" />
                    &nbsp&nbsp&nbsp
               <%--  <a href="mailto:customerservice@kraususa.com"> <asp:Button ID="Button2" runat="server" Text="Email" CssClass="btn"  /></a>--%> &nbsp&nbsp&nbsp&nbsp
                    <a href="mailto:Name@Domain.com" class="btn">Email</a>
                 <%--   <a href='mailto:name@domain.com?Subject=SubjTxt&Body=Bod_Txt&Attachment=""C:\file.txt"" '>--%>

                   <%-- <a href="mailto:customerservice@kraususa.com">customerservice@kraususa.com</a>--%>
                                   
                    <asp:Button ID="Button3" runat="server" Text="Cancel" CssClass="btn" OnClientClick="javascript:return confirm('You want to exit without saving the records');" OnClick="btnOk_Click" style="margin-left:10px;"/>&nbsp&nbsp&nbsp&nbsp
                    <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn" OnClick="btnupdate_Click" />

                </td>
            </tr>
            <tr>

                <td style="width: 50%" align="center">
                    <asp:Label ID="lblMassege" runat="server" Text="" Font-Bold="True" Font-Size="20px" ForeColor="white"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <div class="border">
                        <asp:UpdatePanel ID="updatePanelbtnComment" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <table id="tblmain" runat="server" style="width: 100%; padding: 2px;" class="border">
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblRGAnumber" runat="server" Text="RGA Number " CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtrganumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblRMANumber" runat="server" Text="Vendor Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtvendornumber" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="auto-style1" colspan="2">
                                            <asp:Label ID="lblcomments" style="margin-left:220px" runat="server" Text="Comment" CssClass="lbl"></asp:Label>
                                        </td>
                                        <%--<td style="width:20%">
<asp:TextBox CssClass="txt" ID="TextBox3" runat="server" ReadOnly="true"></asp:TextBox>
</td>--%>
                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblRMAstatus" runat="server" Text="PO Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtponumber" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblshipment" runat="server" Text="Vendor Name" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtvendorName" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td rowspan="4" class="auto-style1">
                                            <asp:TextBox ID="txtcomment" style="margin-left:30px;" runat="server" TextMode="MultiLine" Height="80"></asp:TextBox>
                                        </td>
                                        <td rowspan="6" class="auto-style1">
                                            <div style="width: 100%; overflow: auto; height:180px">
                                               <asp:Repeater ID="Repeater1" runat="server">

                                                   <ItemTemplate>
                                                       <hr />

                                                       <div style="background-color: #3399FF">
                                                          <asp:Label ID="Label1" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                                                           <asp:Label ID="Label2" runat="server" Text='<%# Eval("Time") %>'></asp:Label>
                                                       </div>

                                                       <div  >
                                                          <%-- <asp:Literal ID="lit" runat="server" Text='<%# Eval("Content") %>' Mode="Transform" />--%>
                                                           <asp:Label ID="Label8" style="color:red;background-color:transparent;" runat="server" Text='<%# Eval("Content") %>'></asp:Label>                                                           
                                                       </div>
                                                   </ItemTemplate>
                                               </asp:Repeater>
                                           </div>
                                        </td>


                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="Label3" runat="server" Text="RMA Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRMAnumber" runat="server" ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="Label2" runat="server" Text="Customer Name" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcustomerName" runat="server" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="Label1" runat="server" Text="Shipment Number" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtreturndate"></asp:CalendarExtender>
                                            <asp:TextBox ID="txtshipmentnumber" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblorderdate" runat="server" Text="Address" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerAddress" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblCustomerName" runat="server" Text="Return Date" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtreturndate" runat="server"></asp:TextBox>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblVendorname" runat="server" Text="City" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerCity" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblordernumber" runat="server" Text="RMA Status" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlstatus" runat="server" Width="127px" AutoPostBack="True">
                                                <asp:ListItem Value="0">Incomplete</asp:ListItem>
                                                <asp:ListItem Value="1">Complete</asp:ListItem>
                                                <asp:ListItem Value="2">Wrong RMA</asp:ListItem>
                                                <asp:ListItem Value="3">To Process</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="lblvendornumber" runat="server" Text="State" CssClass="lbl"></asp:Label>
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerState" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td class="auto-style1">
                                            <asp:Button ID="btnComment" style="margin-left:50px;" runat="server" CssClass="btn" Visible="true" Width="120" Text="Add Comment" OnClick="btnComment_Click" />
                                        </td>
                                        <%--<td style="width:10%">
<asp:TextBox CssClass="txt" ID="TextBox5" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
</td>--%>
                                    </tr>
                                    <tr>
                                        <td class="tdLeft">
                                            <asp:Label ID="Label6" runat="server" Text="RMA Decision" CssClass="lbl"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddldecision" runat="server" Width="127px" AutoPostBack="True">
                                                <asp:ListItem Value="0">Pending</asp:ListItem>
                                                <asp:ListItem Value="1">Deny</asp:ListItem>
                                                <asp:ListItem Value="2">Full Refund</asp:ListItem>
                                                <asp:ListItem Value="3">Partial-Refund</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="tdLeft">
                                            <asp:Label ID="Label7" runat="server" Text="ZIP" CssClass="lbl"></asp:Label>
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCustomerZip" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
                                        </td>

                                        <%--<td style="width:10%">
<asp:TextBox CssClass="txt" ID="TextBox5" runat="server" ReadOnly="true" Enabled="false"></asp:TextBox>
</td>--%>
                                    </tr>

                                    <tr>
                                        <td colspan="5" height="50">
                                            <asp:Label ID="Label4" runat="server" Text="Call tag" CssClass="lbl"></asp:Label>
                                            &nbsp&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtCalltag" runat="server" Width="350px"></asp:TextBox>
                                            &nbsp&nbsp&nbsp&nbsp
                                <asp:CheckBox ID="chkflag" Text="Flag" Font-Bold="true" Font-Size="20" runat="server" ForeColor="Black" />
                                        </td>
                                        <%--<td colspan="3" >
                                <asp:Label ID="Label6" runat="server" Text="Comment" CssClass="lbl" ></asp:Label>
                                   &nbsp&nbsp&nbsp&nbsp
                                <asp:TextBox ID="txtcomment" runat="server" Width="300px"></asp:TextBox>
                                 &nbsp&nbsp&nbsp&nbsp
                                 <asp:Button ID="btnComment"  runat="server" Text="Add Comment" OnClick="btnComment_Click" />
                            &nbsp;
                                 <asp:Label ID="lblcomments" runat="server" Text="" Font-Size="10"></asp:Label>
                            </td>--%>
                                    </tr>


                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnComment" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                </td>

            </tr>
        </table>


        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
            <ContentTemplate>

                <table style="width: 1460px;">
                    <tr>
                        <td colspan="5" class="TitleStrip">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Details 

                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                 <asp:Button ID="btnaddnew" runat="server" Text="Add new product" CssClass="btn" OnClick="btnaddnew_Click" Width="135px" />
                            &nbsp&nbsp&nbsp&nbsp&nbsp
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
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnaddnew" />
                 <asp:AsyncPostBackTrigger ControlID="BtnAddNewItem" />
            </Triggers>
        </asp:UpdatePanel>


        <table style="width: 1460px;">
            <tr>
                <td>
                    <div class="border" id="Div2" style="height: 400px; width: 900px; overflow: scroll" onscroll="SetDivPosition()">
                        <asp:Panel ID="panel1" runat="server" Height="00px">

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvReturnDetails" Width="100%" runat="server" HorizontalAlign="Center" AutoGenerateColumns="False"
                                        BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2"
                                        ForeColor="Black" AllowSorting="True" OnSelectedIndexChanged="gvReturnDetails_SelectedIndexChanged">

                                        <Columns>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>

                                                     <asp:UpdatePanel ID="Updateforrdobutton" runat="server" UpdateMode="Always">
                                                           <ContentTemplate>    

                                                    <%-- <asp:RadioButton ID="rdbselect" runat="server" OnCheckedChanged="RadioButton1_CheckedChanged" />--%>
                                                    <asp:RadioButton ID="RadioButton1" GroupName="test" AutoPostBack="true" OnCheckedChanged="RadioButton1_CheckedChanged" onclick="javascript:CheckOtherIsCheckedByGVID(this);"
                                                        runat="server" />

                                                                 </ContentTemplate>

                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="RadioButton1"/>
                                                                </Triggers>

                                                                </asp:UpdatePanel>

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
                                            <asp:TemplateField HeaderText="Qty" ItemStyle-Width="23px">
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
                                                  <%--  <asp:UpdatePanel ID="updtForImages" runat="server" UpdateMode="Always">
                                                        <ContentTemplate>--%>
                                                    <asp:LinkButton ID="txtImageCount" runat="server" Text='<%#Eval("NoofImages") %>' OnClick="txtImageCount_Click"></asp:LinkButton>
                                                  <%--  <asp:LinkButton ID="lnkDownload" Text = "Download" runat="server" OnClick = "lnkDownload_Click1"></asp:LinkButton>--%>
                                                            <%--</ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtImageCount" />
                                                            <asp:AsyncPostBackTrigger ControlID="lnkDownload" />
                                                        </Triggers>
                                                        </asp:UpdatePanel>--%>
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
                <td style="width: 80%;">







                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <asp:Label ID="Label8" runat="server" Text="Product Decision " Font-Bold="True" Font-Size="14px" ForeColor="Black" CssClass="product"></asp:Label>


                            <table id="Table1" class="border" style="width: 100%" runat="server" name="tblm">
                                <tr>
                                    <td colspan="5">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
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
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
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
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
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
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
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
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
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
                                    </td>

                                </tr>
                                <tr>
                                    <td>
                 <asp:TextBox CssClass="txt" ID="txtotherreasons" runat="server" Width="242px"></asp:TextBox>

                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                <asp:DropDownList ID="ddlotherreasons" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlotherreasons_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>

                                    <td style="width: 109%" align="center">
                                        <%-- <asp:Label ID="Label5" Text="Defect in Transite." runat="server" CssClass="lbl"/>--%>

                                        <%--<asp:CheckBox ID="chkduplicate" Text="Duplicate Shipment." runat="server" CssClass="lbl"/>--%>
                                        <%-- <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Width="300px">
                                <asp:ListItem>Yes</asp:ListItem>
                                <asp:ListItem>No</asp:ListItem>
                            </asp:RadioButtonList>--%>


                                        <asp:UpdateProgress ID="uprupnlSubmit" AssociatedUpdatePanelID="upnlSubmit" runat="server">
                                            <ProgressTemplate>
                                                <div id="imageDivSubmit" align="center" valign="middle" runat="server" style="position: absolute; visibility: visible; vertical-align: middle; border-style: none; border-color: black; background-color: transparent;">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Themes/Images/progress.gif" />Loading... 
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>

                                        <asp:UpdatePanel runat="server" ID="upnlSubmit">
                                            <ContentTemplate>

                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="btn" OnClick="btnsubmit_Click" Enabled="false" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </td>

                                </tr>

                                </td>
                                    </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>

        <div style="height: 50px; margin-top: 5px; width: 1500px" align="center">


            <%-- <asp:LinkButton ID="LinkButton1" Text="<< Back To RMA Return Detail" runat="server" PostBackUrl="~/Forms/Web Forms/frmRetunDetail.aspx" ForeColor="Blue"></asp:LinkButton>--%>

            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn" OnClick="btnPrint_Click" />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                        <asp:Button ID="btnReprint" runat="server" Text="Reprint Label" CssClass="btn" OnClick="btnReprint_Click" />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                   <%-- <asp:Button ID="btnEmail" runat="server" Text="Email" CssClass="btn" OnClick="btnEmail_Click" />--%>
            <a href="mailto:Name@Domain.com" class="btn">Email</a>
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                                                
                                    <asp:Button ID="btnCancle" runat="server" Text="Cancel" CssClass="btn" OnClientClick="javascript:return confirm('You want to exit without saving the records');" OnClick="btnOk_Click" />
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp

                             
                                    <asp:Button ID="btnupdate" runat="server" Text="Save" CssClass="btn" OnClick="btnupdate_Click" />


        </div>



        <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
             BackgroundCssClass="modalBackground">
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
                       <%-- <asp:LinkButton ID="lnkSaveCont" runat="server" Font-Bold="True" Font-Size="15px" PostBackUrl="~/Forms/Web Forms/frmReturnEdit.aspx">Save&Continue</asp:LinkButton>
                        &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp--%>
   <asp:LinkButton ID="lnkSaveex" runat="server" Font-Bold="True" Font-Size="15px" PostBackUrl="~/Forms/Web Forms/DemoGrid.aspx">Save&Exit</asp:LinkButton>
                        <%--<a id="lnkSaveExt" href="frmRetunDetail.aspx" style="font-size: 15px; text-decoration: underline; color: #0000FF">Save&Exit </a>--%>
                    </td>
                </tr>
            </table>
        </asp:Panel>


        <asp:Button ID="Button4" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForAddYes" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopUpForAddYes" runat="server" Text="SKU is Added successfully."></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForAddYes" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForAddYes" runat="server" PopupControlID="pnlPopupForAddYes"
            Enabled="True" TargetControlID="Button4" OkControlID="btnOkForAddYes">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button5" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForAddNo" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForAddNo" runat="server" Text="SKU is Not Added. Please Click Add Button after selecting proper SKU from Add New Product textfield."></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForAddNo" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForAddNo" runat="server" PopupControlID="pnlPopupForAddNo"
            Enabled="True" TargetControlID="Button5" OkControlID="btnOkForAddNo">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForCommentYes" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForCommentYes" runat="server" Text="Comment Added successfully. Go Ahead."></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForCommentYes" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForCommentYes" runat="server" PopupControlID="pnlPopupForCommentYes"
            Enabled="True" TargetControlID="Button6" OkControlID="btnOkForCommentYes">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForImageYes" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForImageYes" runat="server" Text="Image is Uploaded successfully. "></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForImageYes" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForImageYes" runat="server" PopupControlID="pnlPopupForImageYes"
            Enabled="True" TargetControlID="Button7" OkControlID="btnOkForImageYes">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button8" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForImageNo" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForImageNo" runat="server" Text="SKU Not Added. Please Try Again!!!!!!"></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForImageNo" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForImageNo" runat="server" PopupControlID="pnlPopupForImageNo"
            Enabled="True" TargetControlID="Button8" OkControlID="btnOkForImageNo">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button9" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForSubmitYes" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForSubmitYes" runat="server" Text="Your Information is Submitted successfully. After all changes are done Please Click Save Button to See Your Information."></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForSubmitYes" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForSubmitYes" runat="server" PopupControlID="pnlPopupForSubmitYes"
            Enabled="True" TargetControlID="Button9" OkControlID="btnOkForSubmitYes">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button10" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForSubmitNo" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForSubmitNo" runat="server" Text="SKU Not Added. Please Try Again!!!!!!"></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForSubmitNo" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForSubmitNo" runat="server" PopupControlID="pnlPopupForSubmitNo"
            Enabled="True" TargetControlID="Button10" OkControlID="btnOkForSubmitNo">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button11" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForSaveYes" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForSaveYes" runat="server" Text="Your Information is Saved successfully. Please Click Ok to See Your Information."></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForSaveYes" runat="server" Text="Ok" OnClick="btnOkForSaveYes_Click" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForSaveYes" runat="server" PopupControlID="pnlPopupForSaveYes"
            Enabled="True" TargetControlID="Button11" >
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button12" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlPopupForSaveNo" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblPopupForSaveNo" runat="server" Text="SKU Not Added. Please Try Again!!!!!!"></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForSaveNo" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpePopupForSaveNo" runat="server" PopupControlID="pnlPopupForSaveNo"
            Enabled="True" TargetControlID="Button12" OkControlID="btnOkForSaveNo">
        </cc1:ModalPopupExtender>


        <asp:Button ID="Button13" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlForCancel" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Enter PO Number
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblForCancel" runat="server" Text="SKU Not Added. Please Try Again!!!!!!"></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnYesForCancel" runat="server" Text="Yes" />
                <%--<asp:Button ID="btnNoPO" runat="server" Text="No" OnClick="btnNoPO" />--%>
                <asp:Button ID="btnNoForCancel" runat="server" Text="No" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpeForCancel" runat="server" PopupControlID="pnlForCancel"
            Enabled="True" TargetControlID="Button13" CancelControlID="btnNoForCancel">
        </cc1:ModalPopupExtender>


         <asp:Button ID="Button14" runat="server" Text="Button" Style="display: none" />
        <asp:Panel ID="pnlForLineType" runat="server" CssClass="modalPopup" Style="display: none">
            <div class="header">
                Message Box
            </div>
            <div class="body" style="color: red">
                <asp:Label ID="lblForLineType" runat="server" Text="Can not add comment/parent sku for combination item."></asp:Label>
            </div>
            <div class="footer" align="center">
                <asp:Button ID="btnOkForLineType" runat="server" Text="Ok" />
            </div>
        </asp:Panel>
        <cc1:ModalPopupExtender ID="mpeForLineType" runat="server" PopupControlID="pnlForLineType"
            Enabled="True" TargetControlID="Button14" OkControlID="btnOkForLineType">
        </cc1:ModalPopupExtender>


    </div>
</asp:Content>
