<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServerError.aspx.vb" Inherits="TRACePA.ServerError" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TRACe</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table align="center">
            <tr>
                <td colspan="3">
                    <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl="~/Images/logo.png"
                        Width="390px" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblMsg1" runat="server" Text="Can't connect to the server at this time. Please "
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:LinkButton ID="lnkMsg" Font-Underline="false" runat="server" PostBackUrl="~/LoginPage.aspx">click</asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="lblMsg2" runat="server" Text=" here to login again." Font-Bold="True"
                        Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblNote" runat="server" Font-Bold="True" Text="Note: " Font-Size="Small"></asp:Label>
                    <asp:Label ID="lblMsg3" runat="server" Font-Size="Small" Text="If you encountered same problem. Please contact system admin."></asp:Label>
                </td>
            </tr>
        </table>
        <center>
            <br />
        </center>
    </div>
    </form>
</body>
</html>
