<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FinalAuditReport.aspx.vb" Inherits="TRACePA.FinalAuditReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" />
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

        .multiselect-container {
            width: 100% !important;
        }

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            //$('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlAuditNo.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Audit Completion" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save/Update" ValidationGroup="Validate" />
                    <a href="#" data-toggle="dropdown" style="padding: 0px;">
                        <span>
                            <img ID="imgbtnReport" class="dropdown-toggle hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" ValidationGroup="Validate"/>
                        </span>
                    </a>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                        </li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" />
                        </li>
                    </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="RFVCustomerName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustomerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblHCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols" ValidationGroup="Validate"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblHFY" Text="Financial Year" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px;">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:RequiredFieldValidator ID="RFVAuditNo" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:Label ID="lblHAuditNo" runat="server" Text="* Audit No."></asp:Label>
                                    <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <br />
                                    <asp:Label ID="lblHAuditType" runat="server" Text="Audit Type : "></asp:Label>
                                    <asp:Label ID="lblAuditType" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px;">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <label>* Signed BY</label>
                                    <asp:RequiredFieldValidator ID="RFVSignedby" runat="server" ControlToValidate="ddlSignedby" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSignedby" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <label>* UDIN</label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVUDIN" runat="server" SetFocusOnError="True" ControlToValidate="txtUDIN" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVUDIN" runat="server" ControlToValidate="txtUDIN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtUDIN" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <label>* UDIN Date</label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVUDINDate" runat="server" ControlToValidate="txtUDINDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVUDINDate" runat="server" ControlToValidate="txtUDINDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="cclExpectedCompletionDate" runat="server" PopupButtonID="txtUDINDate" PopupPosition="TopRight" TargetControlID="txtUDINDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                    <asp:TextBox ID="txtUDINDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:GridView ID="gvAllTypeReports" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAllTypeReports" Checked="true" Enabled="false" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAllTypeReports_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectReport" runat="server" CssClass="hvr-bounce-in" Checked="true" Enabled="false" />
                                            <asp:Label ID="lblReportID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="100%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalFRValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblFRValidationMsg" runat="server"></asp:Label>
                            </strong>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
