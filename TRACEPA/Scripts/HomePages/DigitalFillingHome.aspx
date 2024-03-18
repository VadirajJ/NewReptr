<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="Digital_AuditOfficeHome.aspx.vb" Inherits="TRACePA.Digital_AuditOfficeHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <div>
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div class="pull-left divmargin ">
                <asp:Label runat="server" Font-Names="serif pro" Font-Bold="true" ForeColor="#063970" Text="* Customer Name"></asp:Label>
                <asp:DropDownList ID="ddlCustomerName"  runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="pull-right divmargin ">
                <asp:Label ID="lblHeadingFY" Text="Financial year" Font-Names="serif pro" Font-Bold="true" ForeColor="#063970" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
    </div>

</asp:Content>