<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.customer._default"MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    
    <table style="padding:2%;width:100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Customer Id" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblCustomerId" runat="server" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Customer Name" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblCustomerName" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Email" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblEmail" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <table class="inner_table">
                    <tr>
                        <td>
                            <asp:Label ID="lblCustomerStatusLabel" runat="server" Text="Customer Status" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:DropDownList ID="ddlCustomerStatus" runat="server" CssClass="text_box_1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnCustomerStatus" runat="server" Text="Update" OnClick="btnCustomerStatus_Click"  CssClass="button_1"  BackColor="LightBlue" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <table class="inner_table">
                    <tr>
                        <td>
                            <asp:Label ID="lblCertificatesLabel" runat="server" Text="Certificates" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvCertificates" runat="server"
                                AutoGenerateColumns = "false" Font-Names = "Arial"
                                Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
                                HeaderStyle-BackColor = "green" AllowPaging ="false" HeaderStyle-Height="30px" 
                                PageSize = "10" CssClass="myGridClass" HeaderStyle-CssClass="grid_item" >
                    
                                <Columns>
                                    <asp:hyperlinkfield ItemStyle-Width="20%" ItemStyle-CssClass="grid_item" headertext="Certificate Id" datatextfield="certificate_id" DataNavigateUrlFields="certificate_id" datanavigateurlformatstring="/pages/certificate/default.aspx?certificate_id={0}" />
                                    <asp:BoundField ItemStyle-Width = "40%" ItemStyle-CssClass="grid_item" DataField = "company_name" HeaderText = "Company Name" />
                                    <asp:BoundField ItemStyle-Width = "40%" ItemStyle-CssClass="grid_item" DataField = "deal" HeaderText = "Deal" />
                                </Columns>
                            </asp:GridView>                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <table class="inner_table">
                    <tr>
                        <td>
                            <asp:Label ID="lblCreditCardsLabel" runat="server" Text="Credit Cards" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvCreditCards" runat="server"
                                AutoGenerateColumns = "false" Font-Names = "Arial"
                                Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
                                HeaderStyle-BackColor = "green" AllowPaging ="false" HeaderStyle-Height="30px" 
                                PageSize = "10" CssClass="myGridClass" HeaderStyle-CssClass="grid_item" >
                    
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width = "40%" ItemStyle-CssClass="grid_item" DataField = "card_number" HeaderText = "Credit Card Number" />
                                    </Columns>
                            </asp:GridView>                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <table class="inner_table">
                    <tr>
                        <td>
                            <asp:Label ID="lblPromotionLabel" runat="server" Text="Promotions" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvPromotions" runat="server"
                                AutoGenerateColumns = "false" Font-Names = "Arial"
                                Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
                                HeaderStyle-BackColor = "green" AllowPaging ="false" HeaderStyle-Height="30px" 
                                PageSize = "10" CssClass="myGridClass" HeaderStyle-CssClass="grid_item" >
                    
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width = "25%" ItemStyle-CssClass="grid_item" DataField = "promotion_code" HeaderText = "Code" />
                                        <asp:BoundField ItemStyle-Width = "25%" ItemStyle-CssClass="grid_item" DataField = "description" HeaderText = "Description" />
                                        <asp:BoundField ItemStyle-Width = "25%" ItemStyle-CssClass="grid_item" DataField = "expiration_date" HeaderText = "Exp. Date" />
                                        <asp:BoundField ItemStyle-Width = "25%" ItemStyle-CssClass="grid_item" DataField = "redeemed_date" HeaderText = "Redeemed Date" />
                                    </Columns>
                            </asp:GridView>                         
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtPromotionCode" runat="server" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnPromotionAdd" runat="server" Text="Add Promotion" OnClick="btnPromotionAdd_Click"  CssClass="button_1"  BackColor="LightBlue" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

