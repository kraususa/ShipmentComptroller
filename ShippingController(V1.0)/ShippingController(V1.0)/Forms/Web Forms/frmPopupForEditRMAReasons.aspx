<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPopupForEditRMAReasons.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmPopupForEditRMAReasons" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnModelPopup" runat="server"  >
            <table>
                <tr>
                    <td colspan="2">
                        <asp:CheckBoxList ID="chkreasons" runat="server" Height="45px" Width="389px"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                   <asp:Button ID="btnAdd" runat="server" Text="Add"  Style="margin-left: 100px" OnClick="btnAdd_Click1"
                     />
                    </td>
                    <td>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel"  onclientclick="window.close();" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
