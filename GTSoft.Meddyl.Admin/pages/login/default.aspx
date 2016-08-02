<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="GTSoft.Meddyl.Admin.pages.login._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link id="lnkCSS" href="~/css/basic_style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br /><br />
        <table style="padding-left:2px;width:100%">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="User Name" CssClass="label_1"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="text_box_1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Password" CssClass="label_1"></asp:Label>
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="text_box_1" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogIn" runat="server" Text="Log In" OnClick="btnLogIn_Click"  CssClass="button_1" />
                </td>
            </tr>
            <tr class="spaceUnder">
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="Red" CssClass="label_1"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
