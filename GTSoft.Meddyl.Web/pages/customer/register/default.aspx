<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Web.customer.register._default" MasterPageFile="~/pages/customer/master/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderId="cpMain" runat="server">
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset style="border:none">

	            <table style="padding:2%;width:100%">
                    <tr class="spaceUnder3">
                        <td align="center">
                            <asp:Label ID="Label1" runat="server" Text="SIGN UP FOR GREAT DEALS" CssClass="label_2"  ForeColor="GrayText"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label2" runat="server" Text="Enter a promo code" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:TextBox ID="txtPromoCode" runat="server" placeholder="Promo Code" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="spaceUnder4">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label4" runat="server" Text="Sign up quickly using Facebook" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:TextBox ID="txtFacebookZipCode" runat="server" placeholder="*  Zip Code" CssClass="text_box_1" MaxLength="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblFacebookError" runat="server" ForeColor="#CC3300" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:ImageButton ID="imbFacebookSignUp" runat="server" ImageUrl="~/images/facebook_signin.jpg" CssClass="fb_1" onclick="imbFacebookSignUp_Click" />
                        </td>
                    </tr>
                    <tr class="spaceUnder4">
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Label ID="Label5" runat="server" Text="Or sign up with your email" CssClass="label_1"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" placeholder="* First Name" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" placeholder="* Last Name" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" placeholder="* Email" CssClass="text_box_1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:TextBox ID="txtPassword" runat="server" placeholder="* Password (At least 5 characters)" CssClass="text_box_1" TextMode="Password"></asp:TextBox>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:TextBox ID="txtZipCode" runat="server" placeholder="* Zip Code" CssClass="text_box_1" MaxLength="5" TextMode="Number"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:Label ID="lblError" runat="server" ForeColor="#CC3300" CssClass="label_2"></asp:Label>
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                            <asp:Button ID="btnSignUp" runat="server" OnClick="btnSignUp_Click" Text="Create Account" CssClass="button_1" />
                        </td>
                    </tr>
                    <tr class="spaceUnder" align="center">
                        <td>
                             <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>            
        </asp:UpdatePanel>
    
</asp:Content>

