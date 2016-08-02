<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.certificate._default" MasterPageFile="~/pages/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    
    <table style="padding:2%;width:100%">
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Certificate Id" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblCertificateId" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Certificate Code" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblCertificateCode" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Text="Customer Name" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:Label ID="lblCustomerName" runat="server" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="Label11" runat="server" Text="Customer Email" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder3">
                        <td>
                            <asp:HyperLink ID="hplCustomerEmail" runat="server" CssClass="hyperlink_2"></asp:HyperLink>
                        </td>
                    </tr>
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
                            <asp:DropDownList ID="ddlCertificateStatus" runat="server" CssClass="text_box_1">
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
    </table>

</asp:Content>

