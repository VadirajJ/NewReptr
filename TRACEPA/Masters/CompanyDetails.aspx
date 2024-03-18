<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CompanyDetails.aspx.vb" Inherits="TRACePA.CompanyDetails" %>

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
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>


    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Company Details" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnAddBranch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSaveUpdateBranch" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" ValidationGroup="ValidateComp" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                                </li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" Visible="false" />
                                </li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <br />
            <div id="Tabs" role="tabpanel" class="col-sm-12 col-md-12 pull-left">
                <ul class="nav nav-tabs" role="tablist">
                    <li id="liCompanyDetails" runat="server">
                        <asp:LinkButton ID="lnkbtnCompanyDetails" Text="Company Details" runat="server" Font-Bold="true" /></li>
                    <li id="liBranchDetails" runat="server">
                        <asp:LinkButton ID="lnkbtnBranchDetails" Text="Branch Offices" runat="server" Font-Bold="true" /></li>
                </ul>
                <div class="tab-content" style="padding-top: 5px">
                    <div runat="server" role="tabpanel" class="tab-pane active" id="divCompanyDetails">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHExistingCompanyName" runat="server" Text="Existing Company Name"></asp:Label>
                                    <asp:DropDownList ID="ddlExistingCompanyName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblCompanyName" runat="server" Text="* Name"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyName" runat="server" ControlToValidate="txtCompanyName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtCompanyName" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyCode" runat="server" Text="* Firm’s registration number"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyCode" runat="server" ControlToValidate="txtCompanyCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyCode" runat="server" ControlToValidate="txtCompanyCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyCode" runat="server" CssClass="aspxcontrols" MaxLength="25"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyEmail" runat="server" Text="* Email"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyEmail" runat="server" ControlToValidate="txtCompanyEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyEmail" runat="server" ControlToValidate="txtCompanyEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyEmail" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyContactNo1" runat="server" Text="* Company Contact No 1"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyContactNo1" runat="server" ControlToValidate="txtCompanyContactNo1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyContactNo1" runat="server" ControlToValidate="txtCompanyContactNo1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyContactNo1" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblEstablishmentDate" runat="server" Text="* Establishment Date"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEstablishmentDate" runat="server" ControlToValidate="txtEstablishmentDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtEstablishmentDate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                        <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtEstablishmentDate" PopupPosition="TopRight"
                                            TargetControlID="txtEstablishmentDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblWebSite" runat="server" Text="Web Site"></asp:Label>
                                        <asp:TextBox ID="txtWebSite" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyContactNo2" runat="server" Text=" Company Contact No 2"></asp:Label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyContactNo2" runat="server" ControlToValidate="txtCompanyContactNo2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyContactNo2" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblCompanyAddress" runat="server" Text="* Head Office Address"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyAddress" runat="server" ControlToValidate="txtCompanyAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtCompanyAddress" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyCity" runat="server" Text="* City"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyCity" runat="server" ControlToValidate="txtCompanyCity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyCity" runat="server" ControlToValidate="txtCompanyCity" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyCity" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyCountry" runat="server" Text="* Country"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCountry" runat="server" ControlToValidate="txtCompanyCountry" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCountry" runat="server" ControlToValidate="txtCompanyCountry" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyCountry" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblContactPerson" runat="server" Text="* Contact Person"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVContactPerson" runat="server" ControlToValidate="txtContactPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactPerson" runat="server" ControlToValidate="txtContactPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtContactPerson" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblMoblieNo" runat="server" Text="* Moblie No"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMoblieNo" runat="server" ControlToValidate="txtMoblieNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMoblieNo" runat="server" ControlToValidate="txtMoblieNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtMoblieNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyState" runat="server" Text="* State"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVState" runat="server" ControlToValidate="txtCompanyState" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVState" runat="server" ControlToValidate="txtCompanyState" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyState" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblCompanyPinCode" runat="server" Text="* Pin Code"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyPinCode" runat="server" ControlToValidate="txtCompanyPinCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompanyPinCode" runat="server" ControlToValidate="txtCompanyPinCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCompanyPinCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblContactMail" runat="server" Text="* Contact Mail"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVContactMail" runat="server" ControlToValidate="txtContactMail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactMail" runat="server" ControlToValidate="txtContactMail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtContactMail" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblTelephoneNo" runat="server" Text="Telephone No"></asp:Label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTelephoneNo" runat="server" ControlToValidate="txtTelephoneNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtTelephoneNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Invoice Details</legend>
                        </fieldset>
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" Text="Account Holder Name"></asp:Label>
                                    <asp:TextBox ID="txtAccountHolderName" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" Text="Account No"></asp:Label>
                                    <asp:TextBox ID="txtAccountNo" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Text="Bank name"></asp:Label>
                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="Label4" runat="server" Text="Branch"></asp:Label>
                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label5" runat="server" Text="Conditions"></asp:Label>
                                    <asp:TextBox ID="txtConditions" TextMode="MultiLine" Height="50px" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="Label6" runat="server" Text="Payment terms"></asp:Label>
                                    <asp:TextBox ID="txtPaymentterms" TextMode="MultiLine" Height="50px" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlsignature">
                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                <div class="col-sm-3 col-md-3">
                                    <div class="form-group">
                                        <asp:Image ID="myLogoComp" runat="server" Width="80px" Height="50px" AlternateText="Logo" />
                                        <br />
                                        <asp:FileUpload ID="CompanyLogoUpload" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>

                    <div runat="server" role="tabpanel" class="tab-pane" id="divBranchDetails">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-md-8">
                                <div class="col-md-6 " style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Existing Company Name</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExistingCompanyBranch" runat="server" ControlToValidate="ddlExistingCompanyBranch" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlExistingCompanyBranch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label>Contact Mobile number</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchContactMobileNo" runat="server" ControlToValidate="txtBranchContactMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" MaxLength="10" ID="txtBranchContactMobileNo"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Contact LandLine number</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchContactLandLineNo" runat="server" ControlToValidate="txtBranchContactLandLineNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" MaxLength="15" ID="txtBranchContactLandLineNo" TabIndex="49"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" style="padding-right: 0px;">

                                    <div class="form-group">
                                        <label>* Branch Office Name</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchName" runat="server" ControlToValidate="txtBranchName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtBranchName"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Designation</label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtBranchDesignation"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>E-Mail</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBranchContactEmail" runat="server" ControlToValidate="txtBranchContactEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" TabIndex="50" ID="txtBranchContactEmail"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12" style="padding: 0px;">
                                    <div class="form-group">
                                        <label>* Address</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchAddress" runat="server" ControlToValidate="txtBranchAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtBranchAddress" Height="55px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4 ">
                                <div class="form-group">
                                    <label>* Contact Person</label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchContactPerson" runat="server" ControlToValidate="txtBranchContactPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtBranchContactPerson"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Exisiting Branch</label>
                                    <asp:ListBox runat="server" CssClass="aspxcontrols" AutoPostBack="True" Height="170px" ID="lstboxBranch"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>




    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
