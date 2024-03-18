<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DynamicReport.aspx.vb" Inherits="TRACePA.DynamicReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="css/styles.css" rel="stylesheet" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
         tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px;
            line-height: 1px
        }
    </style>
    

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=gvGeneral.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 4] }],
            });
        });
    </script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-4 col-md-4 pull-left">
                <h2><b>Dynamic Report</b></h2>
            </div>
            <div class="col-sm-2 col-md-2 pull-right">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Search24.png" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" src="~/Images/Download24.png" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblModule" runat="server" Text="* Module"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVModule" runat="server" SetFocusOnError="True" ControlToValidate="ddlModule" Display="Dynamic" ValidationGroup="Search"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlModule" runat="server" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblUsers" runat="server" Text="Users"></asp:Label>
            <asp:DropDownList ID="ddlUsers" runat="server" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblFrom" runat="server" Text="From"></asp:Label>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFromDate" runat="server" ControlToValidate="txtFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Search"></asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFrom" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
            <cc1:CalendarExtender ID="cclFromtxtDate" runat="server" PopupButtonID="txtFrom" PopupPosition="BottomLeft" TargetControlID="txtFrom" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
        </div>
        <div class="col-sm-4 col-md-4">
            <asp:Label ID="lblTo" runat="server" Text="To"></asp:Label>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVToDate" runat="server" ControlToValidate="txtTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Search"></asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTo" AutoCompleteType="Disabled" MaxLength="10"></asp:TextBox>
            <cc1:CalendarExtender ID="cclTotxtDate" runat="server" PopupButtonID="txtTo" PopupPosition="BottomLeft" TargetControlID="txtTo" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvGeneral" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" AllowPaging="false" Width="100%"></asp:GridView>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

