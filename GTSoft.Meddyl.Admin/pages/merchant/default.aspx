<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.merchant._default"MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    
    <table style="padding:2%;width:100%">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Merchant Id" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblMerchantId" runat="server" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Company Name" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblCompanyName" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label12" runat="server" Text="Industry" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblIndustry" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label13" runat="server" Text="Description" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblDescription" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Address" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblAddress1" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblAddress2" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label15" runat="server" Text="Phone" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblPhone" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" Text="Website" CssClass="label_1"></asp:Label>
            </td>
        </tr> 
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblWebsite" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:Image ID="imgLogo" runat="server" CssClass="image_1" />
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <table class="inner_table">
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Merchant Status" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:DropDownList ID="ddlMerchantStatus" runat="server" CssClass="text_box_1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnMerchantStatus" runat="server" Text="Update" OnClick="btnMerchantStatus_Click"  CssClass="button_1"  BackColor="LightBlue" />
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
                            <asp:Label ID="Label2" runat="server" Text="Max Active Deals" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:TextBox ID="txtMaxActiveDeals" runat="server" CssClass="text_box_1" placeholder="Max Active Deals"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Yelp Id" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:TextBox ID="txtYelpId" runat="server" CssClass="text_box_1" placeholder="Yelp Business Id"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Rating (stars)" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:DropDownList ID="ddlRating" runat="server" CssClass="text_box_1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnMerchantUpdate" runat="server" Text="Update" OnClick="btnMerchantUpdate_Click"  CssClass="button_1"  BackColor="LightBlue" />
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
                            <asp:Label ID="Label9" runat="server" Text="Deals" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gdvDeals" runat="server"
                                AutoGenerateColumns = "false" Font-Names = "Arial"
                                Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
                                HeaderStyle-BackColor = "green" AllowPaging ="false" HeaderStyle-Height="30px" 
                                PageSize = "10" CssClass="myGridClass" HeaderStyle-CssClass="grid_item" >
                    
                                <Columns>
                                    <asp:hyperlinkfield ItemStyle-Width="20%" ItemStyle-CssClass="grid_item" headertext="Deal Id" datatextfield="Deal Id" DataNavigateUrlFields="Deal Id" datanavigateurlformatstring="/pages/deal/default.aspx?deal_id={0}" />
                                    <asp:BoundField ItemStyle-Width = "80%" ItemStyle-CssClass="grid_item" DataField = "Deal" HeaderText = "Deal" />
                                </Columns>
                            </asp:GridView>                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label6" runat="server" Text="Merchant Contact Id" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <asp:Label ID="lblMerchantContactId" runat="server" CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label21" runat="server" Text="Contact Name"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:Label ID="lblContactName" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Text="Email"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:Label ID="lblEmail" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Text="Phone"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:Label ID="lblContactPhone" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label19" runat="server" Text="Title"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder">
            <td>
                <asp:Label ID="lblTitle" runat="server"  CssClass="label_1"></asp:Label>
            </td>
        </tr>
        <tr class="spaceUnder3">
            <td>
                <table class="inner_table">
                    <tr>
                        <td>
                            <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click"  CssClass="button_1"  BackColor="Green" />
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
                            <asp:Label ID="Label8" runat="server" Text="Merchant Contact Status" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:DropDownList ID="ddlMerchantContactStatus" runat="server" CssClass="text_box_1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnMerchantContactStatus" runat="server" Text="Update" OnClick="btnMerchantContactStatus_Click"  CssClass="button_1"  BackColor="LightBlue" />
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
                            <asp:Label ID="Label11" runat="server" Text="Credit Cards" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
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
                    <tr>
                        <td>
                            <asp:Button ID="btnCreditCardAdd" runat="server" Text="Add Credit Card" OnClick="btnCreditCardAdd_Click"  CssClass="button_1"  BackColor="LightBlue" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

