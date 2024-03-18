<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmployeeMasterDetailsaspx.aspx.vb" Inherits="TRACePA.EmployeeMasterDetailsaspx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
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

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlArea.ClientID%>').select2();
            $('#<%=ddlBranch.ClientID%>').select2();
            $('#<%=ddlDesignation.ClientID%>').select2();
            $('#<%=ddlExistingEmpName.ClientID%>').select2();
            $('#<%=ddlGroup.ClientID%>').select2();
            $('#<%=ddlPermission.ClientID%>').select2();
            $('#<%=ddlRegion.ClientID%>').select2();
            $('#<%=ddlRole.ClientID%>').select2();
            $('#<%=ddlZone.ClientID%>').select2();
        });
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-9 col-md-9 pull-left">
                <h2><b>Employee Master Details</b> </h2>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-5 col-md-5">
            <div class="form-group">
                <asp:Label ID="lblHeadingEmpName" runat="server" Text="Existing Employee"></asp:Label>
                <asp:DropDownList ID="ddlExistingEmpName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblZone" runat="server" Text="* Zone"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVZone" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="ddlZone" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" TabIndex="1" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblRegion" runat="server" Text="Region"></asp:Label>
                <asp:DropDownList ID="ddlRegion" runat="server" AutoPostBack="true" TabIndex="2" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblArea" runat="server" Text="Area"></asp:Label>
                <asp:DropDownList ID="ddlArea" runat="server" AutoPostBack="true" TabIndex="3" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblBranch" runat="server" Text="Branch"></asp:Label>
                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" TabIndex="4" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblSAPCode" runat="server" Text="* SAP Code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSAPCode" runat="server" ControlToValidate="txtSAPCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSAPCode" runat="server" ControlToValidate="txtSAPCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtSAPCode" runat="server" TabIndex="5" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmpName" runat="server" Text="* Employee Name"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmpName" runat="server" ControlToValidate="txtEmployeeName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmpName" runat="server" ControlToValidate="txtEmployeeName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtEmployeeName" runat="server" TabIndex="6" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblLoginName" runat="server" Text="* Login Name"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLoginName" runat="server" ControlToValidate="txtLoginName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVLoginName" runat="server" ControlToValidate="txtLoginName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtLoginName" runat="server" TabIndex="7" CssClass="aspxcontrols" MaxLength="25" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPassword" runat="server" Text="* Password"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPasssword" runat="server" ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:TextBox autocomplete="off" ID="txtPassword" runat="server" TextMode="Password" TabIndex="8" onpaste="return false" oncopy="return false" CssClass="aspxcontrols" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblconfirmpassword" runat="server" Text="* Confirm Password"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:CompareValidator CssClass="ErrorMsgRight" runat="server" ID="CVPassword" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" Operator="Equal" Type="String" ValidationGroup="Validate" />
                <asp:TextBox autocomplete="off" ID="txtConfirmPassword" runat="server" TextMode="Password" TabIndex="9" onpaste="return false" oncopy="return false" CssClass="aspxcontrols" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-5 col-md-5">
            <div class="form-group">
                <asp:Label ID="lblSearch" runat="server" Text="Search by Employee Name or SAP Code"></asp:Label>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="90%"></asp:TextBox>
                <asp:ImageButton ID="ibSearch" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="False" ValidationGroup="Validate" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="* E-Mail"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtEmail" runat="server" CssClass="aspxcontrols" TabIndex="10" MaxLength="50" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblOfficePhoneNo" runat="server" Text="Office Phone No."></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVOffice" runat="server" ControlToValidate="txtOffice" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtOffice" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="bottom" title="Only numbers" TabIndex="11" MaxLength="15" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No. (+91)"></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtMobile" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="bottom" title="Only numbers" MaxLength="10" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblResidencephoneno" runat="server" Text="Residence Phone No."></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVResidence" runat="server" ControlToValidate="txtResidence" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtResidence" runat="server" data-toggle="tooltip" data-placement="bottom" title="Only numbers" CssClass="aspxcontrols" MaxLength="15" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblDesignation" runat="server" Text="* Designation"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDesignation" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="ddlDesignation" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlDesignation" runat="server" TabIndex="15" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="lblRole" runat="server" Text="* Role"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRole" runat="server" ErrorMessage="Select role." ControlToValidate="ddlRole" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlRole" runat="server" TabIndex="17" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblModule" runat="server" Text="* Module"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVModule" runat="server" ControlToValidate="ddlGroup" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlGroup" runat="server" TabIndex="18" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPermission" runat="server" Text="* Permission"></asp:Label>
                <asp:DropDownList ID="ddlPermission" runat="server" TabIndex="19" CssClass="aspxcontrols" controltovalidate="ddlPermission">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <br />
                <div class="pull-left">
                    <asp:CheckBox ID="chkChangeLevel" runat="server" TextAlign="Right" Visible="False" />
                    <asp:Label ID="lblChangeLevel" runat="server" Text="Change Level" Visible="False"></asp:Label>
                </div>
                <div class="pull-right">
                    <asp:CheckBox ID="chkSendMail" runat="server" TextAlign="Right" />
                    <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalEmpMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblEmpMasterDetailsValidationMsg" runat="server"></asp:Label>
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
</asp:Content>

