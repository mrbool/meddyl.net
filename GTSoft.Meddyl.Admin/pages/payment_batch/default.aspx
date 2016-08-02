<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.payment_batch._default" MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    <table style="padding:2%;width:100%">
        <tr>
            <td>
                <asp:Button ID="btnPayment" runat="server" Text="Process" OnClick="btnPayment_Click"  CssClass="button_1" />
            </td>
         </tr>
    </table>
</asp:Content>