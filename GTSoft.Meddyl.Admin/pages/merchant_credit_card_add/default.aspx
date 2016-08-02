<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.merchant_credit_card_add._default"MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    
    <table style="padding:10px;width:100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Merchant Contact Id" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblMerchantContactId" runat="server" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtCardHolderName" runat="server" CssClass="text_box_1" placeholder="Name"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:TextBox ID="txtCardNumber" runat="server" CssClass="text_box_1" placeholder="Card Number"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:TextBox ID="txtExpDate" runat="server" CssClass="text_box_1" placeholder="Exp Date"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td>
                <asp:TextBox ID="txtZipCode" runat="server" CssClass="text_box_1" placeholder="Zip Code"></asp:TextBox>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Button ID="btnAddCard" runat="server" Text="Update" OnClick="btnAddCard_Click"  CssClass="button_1"  BackColor="LightBlue" />
            </td>
        </tr>
    </table>

</asp:Content>

