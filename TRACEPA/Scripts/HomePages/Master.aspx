<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.master" CodeBehind="Master.aspx.vb" Inherits="TRACePA.Master1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <div>
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div class="pull-right divmargin ">
                <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
    </div>
</asp:Content>
