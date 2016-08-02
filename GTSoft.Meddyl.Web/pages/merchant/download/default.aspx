<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Web.merchant.download._default"  MasterPageFile="~/pages/merchant/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
        
	<table style="padding:2%;width:100%">
        <tr align="center" class="spaceUnder4">
            <td>
                <asp:Label ID="Label2" runat="server" CssClass="label_1"  Text="Download the app today!" ForeColor="GrayText"></asp:Label>
            </td>
        </tr>
        <tr align="center" class="spaceUnder">
            <td>
                <asp:ImageButton ID="imbGoogle" runat="server" ImageUrl="~/images/google_play.png" CssClass="logo_apps" />
            </td>
        </tr>
        <tr align="center" class="spaceUnder">
            <td>
                <asp:ImageButton ID="imbApple" runat="server" ImageUrl="~/images/apple_store.png"  CssClass="logo_apps" />
            </td>
        </tr>
    </table>

</asp:Content>




