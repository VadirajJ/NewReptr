<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PartnersFund.aspx.vb" Inherits="TRACePA.PartnersFund" %>


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
            $('#<%=ddlCustNameSchedTemp.ClientID%>').select2();
            $('#<%=ddlCustPartner.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0;">
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <asp:Label ID="lblPartnershipFirmId" runat="server" Text="0" Visible="false"></asp:Label>
        </div>
    </div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="pull-left">
                        <h2><b>Partnership Firms</b></h2>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="pull-right">
                        <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                        <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                        <ul class="nav navbar-nav navbar-right logoutDropdown">
                            <li class="dropdown">
                                <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                    <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                                <ul class="dropdown-menu">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                    <li role="separator" class="divider"></li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                                </ul>
                            </li>
                        </ul>
                        <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCustName" runat="server" Text="* Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlCustNameSchedTemp" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="100%"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFVCustNameSchedTemp" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustNameSchedTemp" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblYears" runat="server" Text="Financial Year"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYearSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100%">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblCustPartner" runat="server" Text="* Partner Name"></asp:Label><asp:ImageButton ID="imgbtnCustPartner" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" ToolTip="Add New Partner" data-placement="bottom" Style="width: 20px;" ImageUrl = "~/Images/Add24.png"/>
                    <asp:DropDownList ID="ddlCustPartner" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="100%"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFVCustPartner" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustPartner" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" runat="server" id="divCustPartner" visible="false">
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label1" runat="server" Text="Opening Balance"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtOpeningBalance" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOpeningBalance" runat="server" ControlToValidate="txtOpeningBalance" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter Opening Balance."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVOpeningBalance" runat="server" ControlToValidate="txtOpeningBalance" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Opening Balance." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label2" runat="server" Text="Capital Introduced - Unsecured Loan treated as Capital"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtUnsecuredLoanTreatedAsCapital" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVUnsecuredLoanTreatedAsCapital" runat="server" ControlToValidate="txtUnsecuredLoanTreatedAsCapital" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter Capital Introduced - Unsecured Loan treated as Capital."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVUnsecuredLoanTreatedAsCapital" runat="server" ControlToValidate="txtUnsecuredLoanTreatedAsCapital" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Capital Introduced - Unsecured Loan treated as Capital." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label3" runat="server" Text="Interest on Capital"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtInterestOnCapital" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVInterestOnCapital" runat="server" ControlToValidate="txtInterestOnCapital" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter Interest on Capital."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInterestOnCapital" runat="server" ControlToValidate="txtInterestOnCapital" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Interest on Capital." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label4" runat="server" Text="Partner's salary"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtPartnersSalary" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnersSalary" runat="server" ControlToValidate="txtPartnersSalary" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter Partner's salary."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnersSalary" runat="server" ControlToValidate="txtPartnersSalary" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Partner's salary." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label5" runat="server" Text="Share of Profit"></asp:Label><asp:Label ID="lblShareOfProfitPercentage" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtShareOfprofit" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVShareOfprofit" runat="server" ControlToValidate="txtShareOfprofit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter Share of profit(30%)."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVShareOfprofit" runat="server" ControlToValidate="txtShareOfprofit" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Share of profit(30%)." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label8" runat="server" Text="Others"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtAddOthers" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAddOthers" runat="server" ControlToValidate="txtAddOthers" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter Others."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAddOthers" runat="server" ControlToValidate="txtAddOthers" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Others." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Button runat="server" Text="Calculate" class="btn-ok" ID="btnAddCalculate" ValidationGroup="Validate"></asp:Button>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group" style="padding-top: 10px;">
                        <asp:Label ID="lblAddTotal" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label6" runat="server" Text="Transfer to Fixed Capital"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtTransferToFixedCapital" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTransferToFixedCapital" runat="server" ControlToValidate="txtTransferToFixedCapital" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateT" ErrorMessage="Enter Transfer to Fixed Capital."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTransferToFixedCapital" runat="server" ControlToValidate="txtTransferToFixedCapital" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Transfer to Fixed Capital." ValidationGroup="ValidateT" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label7" runat="server" Text="Drawings"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtDrawings" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDrawings" runat="server" ControlToValidate="txtDrawings" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateT" ErrorMessage="Enter Drawings."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDrawings" runat="server" ControlToValidate="txtDrawings" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Drawings." ValidationGroup="ValidateT" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="Label9" runat="server" Text="Others"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtLessOthers" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLessOthers" runat="server" ControlToValidate="txtLessOthers" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateT" ErrorMessage="Enter Others."></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVLessOthers" runat="server" ControlToValidate="txtLessOthers" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Others." ValidationGroup="Validate" ValidationExpression="^[+-]?\d{1,18}(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
 
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Button runat="server" Text="Calculate" class="btn-ok" ID="btnLessCalculate" ValidationGroup="ValidateT"></asp:Button>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group" style="padding-top: 10px;">
                        <asp:Label ID="lblLessTotal" runat="server" Text="" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
                         <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group pull-right">
                        <asp:Label ID="lblCapitalAmount" runat="server" Text="Capital Amount"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtCapitalAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
            <asp:GridView ID="gvPartnershipFirms" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" ShowHeader="false" ShowFooter="false">
                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <div class="col-sm-12 col-md-12">
                                <div class="col-sm-1 col-md-1">
                                    <asp:Label ID="lblSlNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SlNo") %>'></asp:Label>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label ID="lblPARTICULARS" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PARTICULARS") %>'></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <div class="pull-right">
                                        <asp:Label ID="lblFYCData" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FYCData") %>'></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <div class="pull-right">
                                        <asp:Label ID="lblFYPData" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FYPData") %>'></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModal" runat="server"></asp:Label>
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
    <div id="myPartnerModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;">
                            <asp:Label ID="lblModelHeading" runat="server" Font-Bold="true">Partner Details</asp:Label></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblPartnerError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label>* Name</label>
                                <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtPartnerName" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerName" runat="server" ControlToValidate="txtPartnerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerName" runat="server" ControlToValidate="txtPartnerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>* PAN</label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtPartnerPAN" MaxLength="25"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerPAN" runat="server" ControlToValidate="txtPartnerPAN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerPAN" runat="server" ControlToValidate="txtPartnerPAN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>* Date of Joining</label>
                                <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtPartnerDOJ"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerDOJ" runat="server" ControlToValidate="txtPartnerDOJ" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerDOJ" runat="server" ControlToValidate="txtPartnerDOJ" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtPartnerDOJ" PopupPosition="TopRight" TargetControlID="txtPartnerDOJ" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>* Share Of Profit</label>
                                <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtPartnerShareOfProfit" MaxLength="5"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerShareOfProfit" runat="server" ControlToValidate="txtPartnerShareOfProfit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerShareOfProfit" runat="server" ControlToValidate="txtPartnerShareOfProfit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>* Capital Amount</label>
                                <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtPartnerCapitalAmount" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerCapitalAmount" runat="server" ControlToValidate="txtPartnerCapitalAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerCapitalAmount" runat="server" ControlToValidate="txtPartnerCapitalAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvPartner" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartnerPkID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartnerPkID") %>'></asp:Label>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPAN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PAN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DOJ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDOJ" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DOJ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Share Of Profit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblShareOfProfit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ShareOfProfit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Capital Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCapitalAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CapitalAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" ToolTip="EditRow" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="New" ID="btnNewPartner" CssClass="btn-ok"></asp:Button>
                    <asp:Button runat="server" ValidationGroup="ValidatePartner" Text="Save Partner Details" ID="btnSavePartner" CssClass="btn-ok"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
