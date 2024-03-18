<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConcurrentLogin.aspx.vb" Inherits="TRACePA.ConcurrentLogin1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
    TRACe - Governance,Risk,Audit & Compliance
    </title>
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
                <td colspan="3" align="center">              
                    <asp:Image ID="Image1" runat="server" Height="100px" ImageUrl="Images/TRACe.jpg"
                        Width="600px" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Label ID="lblMsg1" runat="server" Text="Your account was logged into from other browser/system. Now this system session will be closed. "
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
                <td>
                    <asp:LinkButton ID="lnkMsg" Font-Underline="false" runat="server" PostBackUrl="~/LoginPage.aspx">Click</asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="lblMsg2" runat="server" Text=" here to login again." Font-Bold="True"
                        Font-Size="Small"></asp:Label>
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
