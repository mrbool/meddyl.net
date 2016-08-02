﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs"  Inherits="GTSoft.Meddyl.Admin.pages.merchant_search._default" MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
	<table style="padding:2%;width:100%">
        <tr>
            <td>
                <asp:Label ID="lblSearch" runat="server" Text="Merchant Search" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtSearch" runat="server" CssClass="text_box_1"></asp:TextBox>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="button_1" />
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:GridView ID="gdvResults" runat="server"
                    AutoGenerateColumns = "false" Font-Names = "Arial"
                    Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
                    HeaderStyle-BackColor = "green" AllowPaging ="false" HeaderStyle-Height="30px" 
                    PageSize = "10" CssClass="myGridClass" HeaderStyle-CssClass="grid_item" >
                    
                        <Columns>
                            <asp:hyperlinkfield ItemStyle-Width="20%" ItemStyle-CssClass="grid_item" headertext="Contact Id" datatextfield="Contact Id" DataNavigateUrlFields="Contact Id" datanavigateurlformatstring="/pages/merchant/default.aspx?merchant_contact_id={0}" />
                            <asp:BoundField ItemStyle-Width = "40%" ItemStyle-CssClass="grid_item" DataField = "Merchant" HeaderText = "Merchant" />
                        </Columns>
                </asp:GridView>  
            </td>
        </tr>
    </table>
</asp:Content>

