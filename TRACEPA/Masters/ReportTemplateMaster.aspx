<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportTemplateMaster.aspx.vb" Inherits="TRACePA.ReportTemplateMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />
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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
           <%-- $('#<%=ddlReportType.ClientID%>').select2();
            $('#<%=ddlModules.ClientID%>').select2();
            $('#<%=ddlReport.ClientID%>').select2();--%>
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Report Template Master" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <div class="col-sm-12 col-md-12">
                <div class="col-md-6" style="padding-left: 0px">
                    <div class="form-group ">
                        <asp:Label ID="lblModule" runat="server" Text="* Module"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVModules" runat="server" ControlToValidate="ddlModules" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlModules" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                            <asp:ListItem Value="0" Text="Select Module"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Financial Audit"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6" style="padding-left: 0px">
                    <div class="form-group ">
                        <asp:Label ID="lblFunction" runat="server" Text="* Report Type"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVFunction" runat="server" ControlToValidate="ddlReportType" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlReportType" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <asp:GridView ID="gvReport" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkReoprt" runat="server" CssClass="hvr-bounce-in" />
                                <asp:Label ID="lblDRLID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.RCM_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RCM_Id" HeaderText="DRLID" ReadOnly="True" Visible="false" />
                        <asp:TemplateField HeaderText="List" ItemStyle-Width="100%">
                            <ItemTemplate>
                                <asp:Label ID="lblList" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RCM_Heading") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-md-12 divmargin" style="padding-right: 0px">
                <div class="col-md-1">
                    <asp:Button ID="btnAdd" runat="server" CssClass="btn-ok" Visible="false" Text="Add" />
                </div>
            </div>
            <div class="col-md-12" style="padding-left: 0px">
                <div class="col-md-10">
                    <asp:ListBox ID="lstDes" runat="server" CssClass="ListBox" Height="95px" Visible="false" Width="100%"></asp:ListBox>
                </div>
                <div class="col-md-1">
                    <div>
                        <asp:ImageButton ID="btnUpArrow" Visible="false" runat="server" />
                    </div>
                    <br />
                    <br />
                    <div>
                        <asp:ImageButton ID="btnDownArrow" Visible="false" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="ModelReportTemplateValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModelReportTemplateValidationMsg" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="btnOk">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
