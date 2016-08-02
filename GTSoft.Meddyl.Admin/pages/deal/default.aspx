<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.deal._default" MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    
    <table style="padding:2%;width:100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Deal Id" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblDealId" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Company Name" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:HyperLink ID="hplCompanyName" runat="server" CssClass="hyperlink_2"></asp:HyperLink>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Deal" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblDeal" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Fine Print" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblFinePrint" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Expiration Date" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblExpirationDate" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label10" runat="server" Text="Certificates Issued" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblCertificatesIssued" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Image ID="imgDeal" runat="server" CssClass="image_1" />
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
                            <asp:Label ID="Label4" runat="server" Text="Status" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:DropDownList ID="ddlDealStatus" runat="server" CssClass="text_box_1">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnStatus" runat="server" Text="Update" OnClick="btnStatus_Click"  CssClass="button_1"  BackColor="LightBlue" />
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
                            <asp:Label ID="Label6" runat="server" Text="Ranking" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:TextBox ID="txtRanking" runat="server" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnRanking" runat="server" Text="Update" OnClick="btnRanking_Click"  CssClass="button_1"  BackColor="LightBlue" />
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
                            <asp:Label ID="Label7" runat="server" Text="Promotion" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:TextBox ID="txtPromotion" runat="server" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnPromotion" runat="server" Text="Update" OnClick="btnPromotion_Click"  CssClass="button_1"  BackColor="LightBlue" />
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
                            <asp:Label ID="Label11" runat="server" Text="Deal Ranking (1-100)" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:TextBox ID="txtApproveRanking" runat="server" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
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
                            <asp:Label ID="Label2" runat="server" Text="Reject Message" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder">
                        <td>
                            <asp:TextBox ID="txtRejectDesc" runat="server" CssClass="text_box_1" TextMode="MultiLine" Height="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="btnReject_Click"  CssClass="button_1"  BackColor="Red" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="inner_table">      
                    <tr class="spaceUnder">
                        <td>
                            <asp:DropDownList ID="ddlCreditCards" runat="server" CssClass="text_box_1">
                            </asp:DropDownList>
                        </td>
                    </tr>           
                    <tr>
                        <td>
                            <asp:Button ID="btnPayment" runat="server" Text="Payment" OnClick="btnPayment_Click"  CssClass="button_1"  BackColor="Green" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

</asp:Content>

