<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Web.merchant.pw._default" MasterPageFile="~/pages/merchant/master/Main.Master" %>



<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <fieldset style="border:none">
 
	<table style="padding:2%;width:100%">
        <tr class="spaceUnder" align="center">
            <td>
                <asp:Label ID="Label1" runat="server" Text="Choose a New Password" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr align="center">
            <td style="padding:3%;padding-bottom:3em;">
                <asp:Label ID="Label2" runat="server" Text="Your password must be at least 5 characters" CssClass="label_2"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder" align="center">
            <td>
                <asp:TextBox ID="txtPassword" runat="server" placeholder="New Password" CssClass="text_box_1" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr class="spaceUnder" align="center">
            <td>
                <asp:TextBox ID="txtPasswordConfirm" runat="server" placeholder="Confirm Password" CssClass="text_box_1" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr class="spaceUnder" align="center">
            <td>
                <asp:Button ID="btnUpdatePassword" runat="server" OnClick="btnUpdatePassword_Click" Text="Update Password" CssClass="button_1" />
            </td>
        </tr>
        <tr class="spaceUnder" align="center">
            <td>
                <asp:Label ID="lblError" runat="server" ForeColor="#CC3300" CssClass="label_2"></asp:Label>
            </td>
        </tr>
    </table>
        
    </ContentTemplate>            
</asp:UpdatePanel>

</asp:Content>

