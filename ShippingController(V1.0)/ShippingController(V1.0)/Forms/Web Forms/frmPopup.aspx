<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPopup.aspx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.frmPopup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function SetName()
        {
            if (window.opener != null && !window.opener.closed)
            {
                var txtName = window.opener.document.getElementById("txtskureasons");
                txtName.value = document.getElementById("chkreasons").value;
            }
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>

<asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:Panel ID="pnModelPopup" runat="server"  >
            <table>
                <tr>
                    <td colspan="2">
                        <asp:CheckBoxList ID="chkreasons" runat="server" Height="45px" Width="389px"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                   <asp:Button ID="btnAdd" runat="server" Text="Add"  Style="margin-left: 100px" OnClick="btnAdd_Click"
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
