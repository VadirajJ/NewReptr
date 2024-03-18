<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UserMasterDetails.aspx.vb" Inherits="TRACePA.UserMasterDetails" %>

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
            $('#<%=ddlCompanyName.ClientID%>').select2();
            $('#<%=ddlDesignation.ClientID%>').select2();
            $('#<%=ddlExistingUserName.ClientID%>').select2();
            $('#<%=ddlExistingUserName.ClientID%>').select2();
            $('#<%=ddlGroup.ClientID%>').select2();
            $('#<%=ddlPermission.ClientID%>').select2();
            $('#<%=ddlRole.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
                         <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Customer User Master Details" Font-Size="Small"></asp:Label>
     <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
            </div>
   <div class="card">

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-5 col-md-5">
            <div class="form-group">
                <asp:Label ID="lblHeadingUserName" runat="server" Text="Existing Users name"></asp:Label>
                <asp:DropDownList ID="ddlExistingUserName" TabIndex="1" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblCompanyname" runat="server" Text="* Customer"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompanyName" runat="server" SetFocusOnError="True" ControlToValidate="ddlCompanyName" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCompanyName" runat="server" TabIndex="16" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblSAPCode" runat="server" Text="* EMP Code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSAPCode" runat="server" ControlToValidate="txtSAPCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSAPCode" runat="server" ControlToValidate="txtSAPCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtSAPCode" runat="server" TabIndex="5" CssClass="aspxcontrols" MaxLength="10" Enabled="false" BackColor="LightGray"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmpName" runat="server" Text="* User Name"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmpName" runat="server" ControlToValidate="txtUserName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmpName" runat="server" ControlToValidate="txtUserName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox autocomplete="off" ID="txtUserName" runat="server" TabIndex="6" CssClass="aspxcontrols" MaxLength="50"></asp:TextBox>
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
                <asp:TextBox autocomplete="off" ID="txtPassword" runat="server" TextMode="Password" TabIndex="8" onpaste="return false" oncopy="return false" CssClass="aspxcontrols" ValidationGroup="Validate" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblconfirmpassword" runat="server" Text="* Confirm Password"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:CompareValidator CssClass="ErrorMsgRight" runat="server" ID="CVPassword" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" Operator="Equal" Type="String" ValidationGroup="Validate" />
                <asp:TextBox autocomplete="off" ID="txtConfirmPassword" runat="server" TextMode="Password" TabIndex="9" onpaste="return false" oncopy="return false" CssClass="aspxcontrols" onkeyup="nospaces(this)"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblModule" runat="server" Text="* Module"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVModule" runat="server" ControlToValidate="ddlGroup" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlGroup" runat="server" TabIndex="18" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <br />
                <asp:CheckBox ID="chkSendMail" runat="server" TextAlign="Right" />
                <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
            </div>
        </div>
        <div class="col-sm-5 col-md-5">
            <div class="form-group">
                <asp:Label ID="lblSearch" runat="server" Text="Search by User Name or EMP Code"></asp:Label>
                <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" CssClass="aspxcontrols" TabIndex="100" Width="95%"></asp:TextBox>
                <asp:ImageButton ID="ibSearch" runat="server" CssClass="hvr-bounce-in" data-toggle="tooltip" data-placement="bottom" title="Search" CausesValidation="False" />
            </div>
            <div class="form-group">
                <asp:Label ID="lblEmail" runat="server" Text="* E-Mail"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEMail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
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
                <asp:TextBox autocomplete="off" ID="txtResidence" runat="server" data-toggle="tooltip" data-placement="bottom" title="Only numbers" CssClass="aspxcontrols" MaxLength="15"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="lblDesignation" runat="server" Text="* Designation"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDesignation" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="ddlDesignation" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlDesignation" runat="server" TabIndex="15" CssClass="aspxcontrols" Enabled="false">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblRole" runat="server" Text="* Role"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRole" runat="server" ControlToValidate="ddlRole" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlRole" runat="server" TabIndex="17" CssClass="aspxcontrols" Enabled="false">
                </asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label ID="lblPermission" runat="server" Text="* Permission"></asp:Label>
                <asp:DropDownList ID="ddlPermission" runat="server" TabIndex="19" CssClass="aspxcontrols" Enabled="false">
                </asp:DropDownList>
            </div>
            <div class="form-group">
            </div>
        </div>
    </div>
       </div>
     <div id="ModaCustomerValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerValidationMsg" runat="server"></asp:Label>
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


