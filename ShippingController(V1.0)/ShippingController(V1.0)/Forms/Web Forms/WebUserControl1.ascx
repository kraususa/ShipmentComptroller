<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WebUserControl1.ascx.cs" Inherits="ShippingController_V1._0_.Forms.Web_Forms.WebUserControl1" %>
<asp:Table ID="AccordionTable" runat="server" CellPadding="0" CellSpacing="0" Width="100%">
 
    <asp:TableRow Width="100%" Height="200px">
        <%-- SLIDER 1 --%>
        <asp:TableCell CssClass="Border">
            <asp:Panel ID="Slide1Panel" runat="server" Style="display:block">
                <p>Panel 1 content.</p>
            </asp:Panel>
        </asp:TableCell>
        <asp:TableCell CssClass="Border" Width="20px">
            <asp:LinkButton ID="LinkButton_1" runat="server" Text="Header 1" CssClass="VerticalText" OnClick="Slide_Click" />
        </asp:TableCell>
        <%-- SLIDER 2 --%>
        <asp:TableCell CssClass="Border">
            <asp:Panel ID="Slide2Panel" runat="server" Style="display:none">
                <p>Panel 2 content.</p>
            </asp:Panel>
        </asp:TableCell>
        <asp:TableCell CssClass="Border" Width="20px">
            <asp:LinkButton ID="LinkButton_2" runat="server" Text="Header 2" CssClass="VerticalText" OnClick="Slide_Click" />
        </asp:TableCell>
        <%-- SLIDER 3 --%>
        <asp:TableCell CssClass="Border">
            <asp:Panel ID="Slide3Panel" runat="server" Style="display:none">
                <p>Panel 3 content.</p>
            </asp:Panel>
        </asp:TableCell>
        <asp:TableCell CssClass="Border" Width="20px">
            <asp:LinkButton ID="LinkButton_3" runat="server" Text="Header 3" CssClass="VerticalText" OnClick="Slide_Click" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>