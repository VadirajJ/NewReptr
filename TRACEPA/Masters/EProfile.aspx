<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EProfile.aspx.vb" Inherits="TRACePA.EProfile" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
            opacity: 1;
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
           <%-- $('#<%=ddlAPMonthExam.ClientID%>').select2();--%>
            $('#<%=ddlArea.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlBloodGroup.ClientID%>').select2();
            $('#<%=ddlBranch.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlDesignation.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlExistingEmployee.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });

            $('#<%=ddlGroup.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlMVEmailSelection.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlMVEmailSelection.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlPermission.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlRegion.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlRole.ClientID%>').select2(
                {
                    width: '100%' // need to override the changed default
                });
            $('#<%=ddlZone.ClientID%>').select2(
            {
                width: '100%' // need to override the changed default
            });
            $('#<%=gvEmpQualification.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvCourse.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvProfessionalExperience.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvAssestsLoan.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvEmpDetailsAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvPerformanceAssessments.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvAcademicProgress.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvSpecialMentions.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvHRDetailsAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvTransferswithintheFirm.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvParticularsofArticles.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvArticleAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvEQAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvECSAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvProfessionalExperienceAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvAsstesLoanAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvPAAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });

            $('#<%=gvAcademicProgressAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvSMAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvTransferFirmAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvPOAAttach.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader" style="opacity: 1"></div>

    <script lang="javascript" type="text/javascript">
        function CopyAddress() {
            if (document.getElementById('<%=chkCAToPA.ClientID %>').checked == true) {
                document.getElementById('<%=txtPAAddress.ClientID %>').value = document.getElementById('<%=txtCAAddress.ClientID %>').value;
                document.getElementById('<%=txtPAAddress1.ClientID %>').value = document.getElementById('<%=txtCAAddress1.ClientID %>').value;
                document.getElementById('<%=txtPAAddress2.ClientID %>').value = document.getElementById('<%=txtCAAddress2.ClientID %>').value;
                document.getElementById('<%=txtPAPincode.ClientID %>').value = document.getElementById('<%=txtCAPincode.ClientID %>').value;
                document.getElementById('<%=txtPATelephoneNo.ClientID %>').value = document.getElementById('<%=txtCATelephoneNo.ClientID %>').value;
                document.getElementById('<%=txtPAMobileNo.ClientID %>').value = document.getElementById('<%=txtCAMobileNo.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtPAAddress.ClientID %>').value = "";
                document.getElementById('<%=txtPAAddress1.ClientID %>').value = "";
                document.getElementById('<%=txtPAAddress2.ClientID %>').value = "";
                document.getElementById('<%=txtPAPincode.ClientID %>').value = "";
                document.getElementById('<%=txtPAPincode.ClientID %>').value = "";
                document.getElementById('<%=txtPATelephoneNo.ClientID %>').value = "";
                document.getElementById('<%=txtPAMobileNo.ClientID %>').value = "";
            }
        }
        function SelectRelation() {
            if (document.getElementById('<%=rboSingle.ClientID %>').checked == true) {
                document.getElementById('<%=ddlMVEmailSelection.ClientID %>').value = 1;
            }
            else if (document.getElementById('<%=rboMarried.ClientID %>').checked == true) {
                document.getElementById('<%=ddlMVEmailSelection.ClientID %>').value = 3;
            }
            else {
                document.getElementById('<%=ddlMVEmailSelection.ClientID %>').value = 1;
            }
        }

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        var PhotoFile = function (event) {
            var PhotoUpload = new FileReader();
            PhotoUpload.onload = function () {
                document.getElementById('<%=RetrievePhotoUpload.ClientID %>').remove();
                var PhotoUploadOutput = document.getElementById('PhotoUploadOutput');
                PhotoUploadOutput.style.cssText = "height:140px;width:140px";
                PhotoUploadOutput.src = PhotoUpload.result;
            };
            PhotoUpload.readAsDataURL(event.target.files[0]);
        };

        var SignatureFile = function (event) {

            var SignatureUpload = new FileReader();
            SignatureUpload.onload = function () {
                document.getElementById('<%=RetrieveSignatureUpload.ClientID %>').remove();
                var SignatureUploadOutput = document.getElementById('SignatureUploadOutput');
                SignatureUploadOutput.style.cssText = "height:140px;width:140px";
                SignatureUploadOutput.src = SignatureUpload.result;
            };
            SignatureUpload.readAsDataURL(event.target.files[0]);
        };

       <%-- var SignatureFile1 = function (event) {

            var SignatureUpload1 = new FileReader();
            SignatureUpload1.onload = function () {
                document.getElementById('<%=RetrieveSignatureUpload1.ClientID %>').remove();
                var SignatureUploadOutput1 = document.getElementById('SignatureUploadOutput1');
                SignatureUploadOutput1.style.cssText = "height:140px;width:140px";
                SignatureUploadOutput1.src = SignatureUpload1.result;
            };
            SignatureUpload1.readAsDataURL(event.target.files[0]);
        };--%>



    </script>
    <style>
        legend {
            margin-bottom: 5px;
            font-size: 14px;
            color: #919191;
        }

        dropdown {
            height: 40px;
        }

        fieldset {
            border: none;
        }
    </style>
             <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="E-Profile" Font-Size="Small"></asp:Label>
               <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnEmpMasterSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateEmpMaster" />
                    <asp:ImageButton ID="imgbtnEmpMasterUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateEmpMaster" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                                </li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" />
                                </li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
            </div>
    <div class="card">
    <%--<div class="col-sm-12 col-md-12 divmargin">--%>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <%--</div>--%>
    <div class="col-sm-12 col-md-12">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblExistingEmployee" runat="server" Text="Existing Employee"></asp:Label>
                <asp:DropDownList ID="ddlExistingEmployee" runat="server" AutoPostBack="true" TabIndex="1" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
    </div>
  <%--  <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="false">
        <div id="div2" runat="server">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li id="liEmpBasic" runat="server">
                    <asp:LinkButton ID="lnkbtnEmpBasicDetails" Text="Employee Basic Details" runat="server" Font-Bold="true" /></li>
                <li id="liEmpMaster" runat="server">
                    <asp:LinkButton ID="lnkbtnEmpMaster" Text="Employee Master" runat="server" Font-Bold="true" /></li>
                <li id="liEmpDetails" runat="server">
                    <asp:LinkButton ID="lnkbtnEmpDetails" Text="Employee Additional Details" runat="server" Font-Bold="true" /></li>
                <li id="liHRDetails" runat="server">
                    <asp:LinkButton ID="lnkbtnHRDetails" Text="HR Details" runat="server" Font-Bold="true" /></li>
                <li id="liArticleClerck" runat="server">
                    <asp:LinkButton ID="lnkbtnArticleClerck" Text="Articled Clerks" runat="server" Font-Bold="true" /></li>
            </ul>
        </div>
        <!-- Tab panes -->

        <div class="tab-content divmargin">
            <div runat="server" role="tabpanel" class="tab-pane active" id="divEmpBasic">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblEmpBasicDetails" runat="server" Text="Employee Basic Details" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblZone1" runat="server" Text="* Zone"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVZone" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="ddlZone" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlZone" runat="server" AutoPostBack="true" TabIndex="1" CssClass="aspxcontrols">
                            </asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblRegion1" runat="server" Text="Region"></asp:Label>
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
                    </div>
                    <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblSAPCode" runat="server" Text="* EMP Code"></asp:Label>
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
                            <asp:CompareValidator CssClass="ErrorMsgRight" runat="server" ID="CVPassword" ControlToValidate="txtPassword" Display="Dynamic" ControlToCompare="txtConfirmPassword" Operator="Equal" Type="String" ValidationGroup="Validate" />
                            <asp:TextBox autocomplete="off" ID="txtConfirmPassword" runat="server" TextMode="Password" TabIndex="9" onpaste="return false" oncopy="return false" CssClass="aspxcontrols" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEmail" runat="server" Text="* E-Mail"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            <asp:TextBox autocomplete="off" ID="txtEmail" runat="server" CssClass="aspxcontrols" TabIndex="10" MaxLength="50" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4" style="padding-left: 0px">
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
                            <asp:Label ID="lblOfficePhoneNo" runat="server" Text="Membership Number"></asp:Label>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVOffice" runat="server" ControlToValidate="txtOffice" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            <asp:TextBox autocomplete="off" ID="txtOffice" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="bottom" title="Only numbers" TabIndex="11" MaxLength="15" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No. (+91)"></asp:Label>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMobile" runat="server" ControlToValidate="txtMobile" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            <asp:TextBox autocomplete="off" ID="txtMobile" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="bottom" title="Only numbers" MaxLength="10" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblResidencephoneno" runat="server" Text="UDIN"></asp:Label>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVResidence" runat="server" ControlToValidate="txtResidence" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            <asp:TextBox autocomplete="off" ID="txtResidence" runat="server" data-toggle="tooltip" data-placement="bottom" title="Only numbers" CssClass="aspxcontrols" MaxLength="15" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <br />
                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                <div class="col-sm-4 col-md-4" style="padding: 0px">
                                    <asp:CheckBox ID="chkIsPartner" runat="server" TextAlign="Right" />
                                    <asp:Label ID="lblIsPartner" runat="server" Text="Is Partner"></asp:Label>
                                </div>
                                <div class="col-sm-5 col-md-5" style="padding: 0px">
                                    <asp:CheckBox ID="chkChangeLevel" runat="server" TextAlign="Right" Visible="False" />
                                    <asp:Label ID="lblChangeLevel" runat="server" Text="Change Level" Visible="False"></asp:Label>
                                </div>
                                <div class="col-sm-3 col-md-3" style="padding: 0px">
                                    <asp:CheckBox ID="chkSendMail" runat="server" TextAlign="Right" />
                                    <asp:Label ID="lblSendMail" runat="server" Text="Send Mail"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <asp:Panel runat="server" ID="pnlsignature">
                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                <div class="col-sm-6 col-md-6" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                                        <h5><b>Signature Upload</b></h5>
                                    </div>
                                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                                        <asp:Label ID="lblSelectSignatureUploadFile" runat="server" Text="Select a file"></asp:Label>
                                        <asp:FileUpload ID="fuSignatureUpload" runat="server" onchange="SignatureFile(event)" />
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding: 0px">
                                    <img id="SignatureUploadOutput" alt="" /><br />
                                    <asp:Image ID="RetrieveSignatureUpload" runat="server" Width="140px" Height="90px" /><br />
                                </div>
                            </div>
                            <%--<div class="form-group">
                                <div class="col-sm-12 col-md-12" style="padding: 0px">
                                    <div class="col-sm-6 col-md-6" style="padding: 0px">
                                        <asp:Label ID="lblSelectSignatureUploadFile" runat="server" Text="Select a file"></asp:Label>
                                        <asp:FileUpload ID="fuSignatureUpload" runat="server" onchange="SignatureFile(event)" />
                                    </div>
                                </div>
                            </div>--%>
                        </asp:Panel>
                        <%--      <div class="form-group">
                            <div class="col-sm-12 col-md-12" style="padding: 0px">

                                <div class="col-sm-6 col-md-6" style="padding: 0px">
                                    <asp:Label runat="server" Text="Upload Signature"></asp:Label>
                                    <asp:FileUpload ID="txtfile1" runat="server" Width="100%" TabIndex="20"  CssClass="btn-ok" AllowMultiple="true" />
                                </div>
                                <div class="col-sm-5 col-md-5" style="padding: 0px">
                                    <asp:Image ID="ImgSignature" runat="server" Width="100px" Height="50px" />
                                </div>
                                <div class="col-sm-1 col-md-1" style="padding: 0px">
                                   
                                </div>
                            </div>
                        </div>--%>
                        <%--<div class="col-sm-12 col-md-12" style="padding: 0px">
                                <h5><b>Signature Upload</b></h5>
                            </div>
                             <div class="form-group">
                                <img id="SignatureUploadOutput1" alt="" /><br />
                                <asp:Image ID="RetrieveSignatureUpload1" runat="server" Width="140px" Height="140px" /><br />
                                <asp:Label ID="lblSelectSignatureUploadFile1" runat="server" Text="Select a file"></asp:Label>
                                <asp:FileUpload ID="fuSignatureUpload1" runat="server" onchange="SignatureFile1(event)" />
                            </div>--%>
                    </div>
                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane" id="divEmpMaster">
                    <asp:Label ID="lblEmpDetails" runat="server" Text="Employee Details" CssClass="h5" Font-Bold="true"></asp:Label>
                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; overflow: auto">
                    <div class="col-sm-4 col-md-4" style="padding-left: 0px; padding-right: 0px">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblDOB" runat="server" Text="* Date of Birth"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtDOB" Width="130px"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVDOB" runat="server" ControlToValidate="txtDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVDOB" runat="server" ControlToValidate="txtDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDOB" PopupPosition="BottomLeft" TargetControlID="txtDOB" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group pull-left">
                                    <asp:Label ID="lblGender" runat="server" Text="* Gender" Width="250px"></asp:Label>
                                    <asp:RadioButton ID="rboMale" runat="server" CssClass="aspxradiobutton" Text=" Male" GroupName="rboGender" Checked="true" />
                                    <asp:RadioButton ID="rboFemale" runat="server" CssClass="aspxradiobutton" Text=" Female" GroupName="rboGender" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <br />
                            <h5><b>Contact Address</b></h5>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                            <asp:Label ID="lblCAAddress" runat="server" Text="* Address"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCAAddress"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVCAAddress" runat="server" ControlToValidate="txtCAAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCAAddress" runat="server" ControlToValidate="txtCAAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                            <asp:Label ID="lblCAAddress1" runat="server" Text="Address 1"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCAAddress1"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCAAddress1" runat="server" ControlToValidate="txtCAAddress1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                            <asp:Label ID="lblCAAddress2" runat="server" Text="Address 2"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCAAddress2"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCAAddress2" runat="server" ControlToValidate="txtCAAddress2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblCAPincode" runat="server" Text="* Pincode"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCAPincode" MaxLength="6" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVCAPincode" runat="server" ControlToValidate="txtCAPincode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCAPincode" runat="server" ControlToValidate="txtCAPincode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblCAMobileNo" runat="server" Text="* Mobile No."></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCAMobileNo" MaxLength="13" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVCAMobileNo" runat="server" ControlToValidate="txtCAMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCAMobileNo" runat="server" ControlToValidate="txtCAMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblCATelephoneNo" runat="server" Text="Telephone No."></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCATelephoneNo" MaxLength="15" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCATelephoneNo" runat="server" ControlToValidate="txtCATelephoneNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px; width: 5%">
                    </div>
                    <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblBloodGroup" runat="server" Text="Blood Group"></asp:Label>
                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="aspxcontrols" Width="150px">
                                        <asp:ListItem Selected="True" Text="Select Blood Group" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="O+" Value="O+"></asp:ListItem>
                                        <asp:ListItem Text="O-" Value="O-"></asp:ListItem>
                                        <asp:ListItem Text="A+" Value="A+"></asp:ListItem>
                                        <asp:ListItem Text="A-" Value="A-"></asp:ListItem>
                                        <asp:ListItem Text="B+" Value="B+"></asp:ListItem>
                                        <asp:ListItem Text="B-" Value="B-"></asp:ListItem>
                                        <asp:ListItem Text="AB+" Value="AB+"></asp:ListItem>
                                        <asp:ListItem Text="AB-" Value="AB-"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblMaritalStatus" runat="server" Text="* Marital Status" Width="250px"></asp:Label>
                                    <asp:RadioButton ID="rboSingle" runat="server" CssClass="aspxradiobutton" Text=" Single" GroupName="rbo" onclick="SelectRelation()" Checked="true" />
                                    <asp:RadioButton ID="rboMarried" runat="server" CssClass="aspxradiobutton" Text=" Married" GroupName="rbo" onclick="SelectRelation()" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                            <br />
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <h5><b>Permanent Address</b></h5>
                            </div>
                            <div class="col-sm-6 col-md-6 pull-right" style="padding-top: 10px; padding-left: 0px">
                                <asp:CheckBox Text="Same as Contact Address" ID="chkCAToPA" onclick="CopyAddress()" runat="server" Font-Size="12px" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                            <asp:Label ID="lblPAAddress" runat="server" Text="* Address"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAAddress"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAAddress" runat="server" ControlToValidate="txtPAAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAAddress" runat="server" ControlToValidate="txtPAAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                            <asp:Label ID="lblPAAddress1" runat="server" Text="Address 1"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAAddress1"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAAddress1" runat="server" ControlToValidate="txtPAAddress1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                            <asp:Label ID="lblPAAddress2" runat="server" Text="Address 2"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAAddress2"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAAddress2" runat="server" ControlToValidate="txtPAAddress2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                            <div class="col-sm-6 col-md-6" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPAPincode" runat="server" Text="* Pincode"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAPincode" MaxLength="6" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAPincode" runat="server" ControlToValidate="txtPAPincode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAPincode" runat="server" ControlToValidate="txtPAPincode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblPAMobileNo" runat="server" Text="* Mobile No."></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAMobileNo" MaxLength="13"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAMobileNo" runat="server" ControlToValidate="txtPAMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAMobileNo" runat="server" ControlToValidate="txtPAMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblPATelephoneNo" runat="server" Text="Telephone No."></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPATelephoneNo" MaxLength="15" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPATelephoneNo" runat="server" ControlToValidate="txtPATelephoneNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px; width: 5%">
                    </div>
                    <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblChildrenCount" runat="server" Text="No of Children" Width="250px"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtChildrenCount" Width="130px"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVChildrenCount" runat="server" ControlToValidate="txtChildrenCount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <br />
                            <h5><b>Photo Upload</b></h5>
                        </div>
                        <div class="form-group" style="padding-left: 0px">
                            <img id="PhotoUploadOutput" alt="" /><br />
                            <asp:Image ID="RetrievePhotoUpload" runat="server" Width="140px" Height="140px" /><br />
                            <asp:Label ID="lblSelectPhotoUploadFile" runat="server" Text="Select a file"></asp:Label>
                            <asp:FileUpload ID="fuPhotoUpload" runat="server" onchange="PhotoFile(event)" />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <br />
                                <h5><b>Emergency Contact</b></h5>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblECName" runat="server" Text="* Name"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECName"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECName" runat="server" ControlToValidate="txtECName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECName" runat="server" ControlToValidate="txtECName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblECAddress" runat="server" Text="* Address"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECAddress"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECAddress" runat="server" ControlToValidate="txtECAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECAddress" runat="server" ControlToValidate="txtECAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblECAddress1" runat="server" Text="Address 1"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECAddress1"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECAddress1" runat="server" ControlToValidate="txtECAddress1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblECAddress2" runat="server" Text="Address 2"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECAddress2"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECAddress2" runat="server" ControlToValidate="txtECAddress2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblECPinCode" runat="server" Text="* Pincode"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECPinCode" MaxLength="6" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECPinCode" runat="server" ControlToValidate="txtECPinCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECPinCode" runat="server" ControlToValidate="txtECPinCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblECMobileNo" runat="server" Text="* Mobile No."></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECMobileNo" MaxLength="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECMobileNo" runat="server" ControlToValidate="txtECMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECMobileNo" runat="server" ControlToValidate="txtECMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                    <asp:Label ID="lblECTelephoneNo" runat="server" Text="Telephone No."></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECTelephoneNo" MaxLength="15" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECTelephoneNo" runat="server" ControlToValidate="txtECTelephoneNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblECEmailID" runat="server" Text="E-Mail"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECEmailID"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECEmailID" runat="server" ControlToValidate="txtECEmailID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblECRelation" runat="server" Text="* Relation"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECRelation"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECRelation" runat="server" ControlToValidate="txtECRelation" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECRelation" runat="server" ControlToValidate="txtECRelation" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1" style="padding-left: 0px; width: 5%">
                        </div>
                        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <br />
                                <h5><b>Father's/Mother's/Wife's Contact Details</b></h5>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblMVName" runat="server" Text="* Name"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVName"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVMVName" runat="server" ControlToValidate="txtMVName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVName" runat="server" ControlToValidate="txtMVName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblMVAddress" runat="server" Text="* Address"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVAddress"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVMVAddress" runat="server" ControlToValidate="txtMVAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVAddress" runat="server" ControlToValidate="txtMVAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblMVAddress1" runat="server" Text="Address 1"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVAddress1"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVAddress1" runat="server" ControlToValidate="txtMVAddress1" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                <asp:Label ID="lblMVAddress2" runat="server" Text="Address 2"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVAddress2"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVAddress2" runat="server" ControlToValidate="txtMVAddress2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblMVPinCode" runat="server" Text="* Pincode"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVPinCode" MaxLength="6" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVMVPinCode" runat="server" ControlToValidate="txtMVPinCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVPinCode" runat="server" ControlToValidate="txtMVPinCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblMVMobileNo" runat="server" Text="* Mobile No"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVMobileNo" MaxLength="13"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVMVMobileNo" runat="server" ControlToValidate="txtMVMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVMobileNo" runat="server" ControlToValidate="txtMVMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                    <asp:Label ID="lblMVTelephoneNo" runat="server" Text="Telephone No"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVTelephoneNo" MaxLength="15" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVTelephoneNo" runat="server" ControlToValidate="txtMVTelephoneNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblMVEmailID" runat="server" Text="E-Mail"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtMVEmailID"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVMVEmailID" runat="server" ControlToValidate="txtMVEmailID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblMVEmailSelection" runat="server" Text="Relation Type"></asp:Label>
                                        <asp:DropDownList ID="ddlMVEmailSelection" runat="server" CssClass="aspxcontrols">
                                            <asp:ListItem Value="1">Father</asp:ListItem>
                                            <asp:ListItem Value="2">Mother</asp:ListItem>
                                            <asp:ListItem Value="3">Wife</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVMVEmailSelection" runat="server" ControlToValidate="ddlMVEmailSelection" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEmpMaster"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1" style="padding-left: 0px; width: 5%">
                        </div>
                        <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                            <br />
                            <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                                <h5><b>Resume Upload</b></h5>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSelectResumeUploadFile" runat="server" Text="Select a file"></asp:Label>
                                <asp:FileUpload ID="txtfile" runat="server" />
                                <asp:TextBox ID="txtResumeUploadPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
                                <asp:Button runat="server" Text="Add Resume" class="btn-ok" ID="imgbtnAddResume"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <asp:GridView ID="gvResumeAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                <asp:TemplateField HeaderText="File Name" ItemStyle-Width="96%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                        <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane" id="divEmpDetails">
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding-left: 0px">
                        <asp:Label ID="lblEQ" runat="server" Text="Employee Qualification" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                        <asp:LinkButton ID="lnkQualification" runat="server" data-toggle="modal" data-target="#myEmpQualificationModal" Font-Italic="true">Add Qualification</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgbtnEQAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" 
                                title="Attachment" OnClick="imgbtnEQAttachment_Click" Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblEQBadgeCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvEmpQualification" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="Education" ItemStyle-Width="26%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEducation" Font-Italic="true" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmpEducation") %>'></asp:LinkButton>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID") %>'></asp:Label>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpUniversity" HeaderText="University" ItemStyle-Width="18%" />
                            <asp:BoundField DataField="EmpCollege" HeaderText="College" ItemStyle-Width="22%" />
                            <asp:BoundField DataField="EmpYear" HeaderText="Year" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="EmpMarks" HeaderText="Marks" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="EmpRemarks" HeaderText="Remarks" ItemStyle-Width="10%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding-left: 0px">
                        <br />
                        <asp:Label ID="lblECS" runat="server" Text="Conferences/Courses Sponsered" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                        <br />
                        <asp:LinkButton ID="lnkCourse" runat="server" data-toggle="modal" data-target="#myEmpCourseModal" Font-Italic="true">Add Conferences/Courses</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px">
                        <div class="pull-right">
                            <br />
                            <asp:ImageButton ID="imgbtnECSAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" title="Attachment" OnClick="imgbtnECSAttachment_Click" Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblECSBadgeCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvCourse" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="Subject" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubject" Font-Italic="true" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ECSSubject") %>'></asp:LinkButton>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID") %>'></asp:Label>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ECSDate" HeaderText="Date" ItemStyle-Width="6%" />
                            <asp:BoundField DataField="ECSDescription" HeaderText="Description" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="PapersPresented" HeaderText="Papers Presented" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="ConductedBy" HeaderText="Conducted By" ItemStyle-Width="8%" />
                            <asp:BoundField DataField="FeesPaidEmployer" HeaderText="Fees Paid Employer" ItemStyle-Width="11%" />
                            <asp:BoundField DataField="FeesPaidEmployee" HeaderText="Fees Paid Employee" ItemStyle-Width="11%" />
                            <asp:BoundField DataField="CPEPoints" HeaderText="CPE Points" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="FeedBack" HeaderText="FeedBack" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="10%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding-left: 0px">
                        <br />
                        <asp:Label ID="lblPE" runat="server" Text="Past Professional Experience" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                        <br />
                        <asp:LinkButton ID="lnkProfessionalExperience" runat="server" data-toggle="modal" data-target="#myEmpProfessionalExperienceModal" Font-Italic="true">Add Professional Experience</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px">
                        <div class="pull-right">
                            <br />
                            <asp:ImageButton ID="imgbtnPEAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" title="Attachment" OnClick="imgbtnPEAttachment_Click" Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgePECount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvProfessionalExperience" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="Employer" ItemStyle-Width="16%">
                                <ItemTemplate>
                                    <asp:Label ID="lblPEPKID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.PEPKID") %>'></asp:Label>
                                    <asp:LinkButton ID="lnkAssignment" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Assignment") %>' Font-Italic="true"></asp:LinkButton>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReportingTo" HeaderText="Reporting To" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="From" HeaderText="From" ItemStyle-Width="8%" />
                            <asp:BoundField DataField="To" HeaderText="To" ItemStyle-Width="11%" />
                            <asp:BoundField DataField="SalaryPerAnnum" HeaderText="Salary Per Annum" ItemStyle-Width="11%" />
                            <asp:BoundField DataField="PositionHeld" HeaderText="Position Held" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="17%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding-left: 0px">
                        <br />
                        <asp:Label ID="lblAL" runat="server" Text="Assests Obtained On Loan" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                        <br />
                        <asp:LinkButton ID="lnkAssests" runat="server" data-toggle="modal" data-target="#myEmpAssestsModal" Font-Italic="true">Add Assests Obtained On Loan</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px">
                        <div class="pull-right">
                            <br />
                            <asp:ImageButton ID="imgbtnALAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" OnClick="imgbtnALAttachment_Click"
                                title="Attachment" Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeALCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvAssestsLoan" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="Type Of Asset" ItemStyle-Width="16%">
                                <ItemTemplate>
                                    <asp:Label ID="lblALPKID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ALPKID") %>'></asp:Label>
                                    <asp:LinkButton ID="lnkTypeOfAsset" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TypeOfAsset") %>' Font-Italic="true"></asp:LinkButton>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SerialNo" HeaderText="Serial No" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="ApproxValue" HeaderText="Approx Value" ItemStyle-Width="8%" />
                            <asp:BoundField DataField="IssueDate" HeaderText="Issue Date" ItemStyle-Width="6%" />
                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" ItemStyle-Width="5%" />
                            <asp:BoundField DataField="RecievedDate" HeaderText="Recieved Date" ItemStyle-Width="6%" />
                            <asp:BoundField DataField="ConditionWhenIssued" HeaderText="Condition When Issued" ItemStyle-Width="13%" />
                            <asp:BoundField DataField="ConditionOnReceipt" HeaderText="Condition On Receipt" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="16%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-12 col-md-12 pull-left" style="padding-left: 0px">
                        <br />
                        <h5><b>All Attachments</b></h5>
                    </div>
                    <br />
                    <asp:GridView ID="gvEmpDetailsAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="36%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                    <asp:LinkButton ID="File" Font-Italic="true" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="38%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <b>By : </b>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                    <b>On : </b>
                                    <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="left" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane" id="divEmpHRDetails">
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding: 0px">
                        <asp:Label ID="lblPA" runat="server" Text="Performance Assessments" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px;">
                        <asp:LinkButton ID="lnkEmpPerformanceAssessments" runat="server" data-toggle="modal" data-target="#myEmpPerformanceAssessmentsModal" Font-Italic="true">Add Performance Assessments</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-right: 0px">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgbtnPAAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" OnClick="imgbtnPAAttachment_Click"
                                title="Attachment" Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblPABadgeCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <asp:GridView ID="gvPerformanceAssessments" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                           <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                <asp:BoundField DataField="AssessmentDate" HeaderText="Assessment Date" ItemStyle-Width="10%" />
                                <asp:TemplateField HeaderText="Rating" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkRating" Font-Italic="true" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PARating") %>'></asp:LinkButton>
                                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                        <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID") %>'></asp:Label>
                                        <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="PerformanceAwardPaid" HeaderText="Performance Award Paid" ItemStyle-Width="18%" />
                                <asp:BoundField DataField="GradePromotedFrom" HeaderText="Grade Promoted From" ItemStyle-Width="18%" />
                                <asp:BoundField DataField="GradePromotedTo" HeaderText="Grade Promoted To" ItemStyle-Width="19%" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="20%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding: 0px">
                        <br />
                        <asp:Label ID="lblAP" runat="server" Text="Academic Progress" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding: 0px">
                        <br />
                        <asp:LinkButton ID="lnlAcademicProgress" runat="server" data-toggle="modal" data-target="#myEmpAcademicProgressModal" Font-Italic="true">Add Academic Progress</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding: 0px">
                        <div class="pull-right">
                            <br />
                            <asp:ImageButton ID="imgbtnAPAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" OnClick="imgbtnAPAttachment_Click"
                                title="Attachment" Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeAPCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvAcademicProgress" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:BoundField DataField="ExamTaken" HeaderText="Exam Taken" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="NoOfDaysLeave" HeaderText="No Of Days Leave" ItemStyle-Width="10%" />
                            <asp:BoundField DataField="MonthOfExam" HeaderText="Month Of Exam" ItemStyle-Width="18%" />
                            <asp:TemplateField HeaderText="Groups" ItemStyle-Width="19%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAPPKID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.APPKID") %>'></asp:Label>
                                    <asp:LinkButton ID="lnkGroups" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Groups") %>' Font-Italic="true"></asp:LinkButton>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                    <asp:Label ID="lblMonthOfExamID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MonthOfExamID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Result" HeaderText="Result" ItemStyle-Width="19%" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="20%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding-left: 0px">
                        <br />
                        <asp:Label ID="lblSM" runat="server" Text="Special Mentions" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                        <br />
                        <asp:LinkButton ID="lnkSpecialMentions" runat="server" data-toggle="modal" data-target="#myEmpSpecialMentionsModal" Font-Italic="true">Add Special Mentions</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px">
                        <div class="pull-right">
                            <br />
                            <asp:ImageButton ID="imgbtnSMAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" title="Attachment" OnClick="imgbtnSMAttachment_Click"
                                Style="padding-left: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblSMBadgeCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <asp:GridView ID="gvSpecialMentions" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                <asp:TemplateField HeaderText="Special Mentions" ItemStyle-Width="38%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkSpecialMentions" Font-Italic="true" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SpecialMentions") %>'></asp:LinkButton>
                                        <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                        <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID") %>'></asp:Label>
                                        <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SMDate" HeaderText="Date" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="SMParticulars" HeaderText="Particulars" ItemStyle-Width="31%" />
                                <asp:BoundField DataField="SMHowDealtWith" HeaderText="How Dealt With" ItemStyle-Width="20%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-12 col-md-12 pull-left" style="padding-left: 0px">
                        <br />
                        <h5><b>All Attachments</b></h5>
                    </div>
                    <br />
                    <asp:GridView ID="gvHRDetailsAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="45%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                    <asp:LinkButton ID="File" runat="server" Font-Italic="true" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="31%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created" ItemStyle-Width="18%">
                                <ItemTemplate>
                                    <b>By : </b>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                    <b>On : </b>
                                    <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="left" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div runat="server" role="tabpanel" class="tab-pane" id="divEmpArticleClerck">
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding-left: 0px">
                        <asp:Label ID="lblTF" runat="server" Text="Transfers within the Firm" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                        <asp:LinkButton ID="lnkTransferswithintheFirm" runat="server" data-toggle="modal" data-target="#myEmpTransferswithintheFirmModal" Font-Italic="true">Add Transfers within the Firm</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding-left: 0px">
                        <div class="pull-right">
                            <asp:ImageButton ID="imgbtnTFAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" OnClick="imgbtnTFAttachment_Click"
                                title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeTFCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvTransferswithintheFirm" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="Earlier Principal" ItemStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTFPKID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.TFPKID") %>'></asp:Label>
                                    <asp:LinkButton ID="lnkEarlierPrinciple" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EarlierPrinciple") %>' Font-Italic="true"></asp:LinkButton>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NewPrinciple" HeaderText="New Principal" ItemStyle-Width="13%" />
                            <asp:BoundField DataField="DateofTransfer" HeaderText="Date of Transfer" ItemStyle-Width="14%" />
                            <asp:BoundField DataField="DurationWithNewPrinciple" HeaderText="Duration With New Principal" ItemStyle-Width="16%" />
                            <asp:BoundField DataField="CompletionDate" HeaderText="Completion Date" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="ExtendedTo" HeaderText="Extended To" ItemStyle-Width="17%" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="11%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-8 col-md-8 pull-left" style="padding: 0px">
                        <br />
                        <asp:Label ID="lblPOA" runat="server" Text="Particulars of Articles" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding: 0px">
                        <br />
                        <asp:LinkButton ID="lnkParticularsofArticles" runat="server" data-toggle="modal" data-target="#myEmpParticularsofArticlesModal" Font-Italic="true">Add Particulars of Articles</asp:LinkButton>
                    </div>
                    <div class="col-sm-1 col-md-1" style="padding: 0px">
                        <div class="pull-right">
                            <br />
                            <asp:ImageButton ID="imgbtnPOAAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="left" OnClick="imgbtnPOAAttachment_Click"
                                title="Attachment" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblPOABadgeCount" runat="server" Text="0"></asp:Label></span>
                        </div>
                    </div>
                    <asp:GridView ID="gvParticularsofArticles" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:BoundField DataField="NameOfThePrinciple" HeaderText="Name Of The Principal" ItemStyle-Width="13%" />
                            <asp:TemplateField HeaderText="Article Registration No" ItemStyle-Width="13%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" Font-Italic="true" CommandName="Select" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ArticleRegistrationNo") %>'></asp:LinkButton>
                                    <asp:Label ID="lblID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    <asp:Label ID="lblUserID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UserID") %>'></asp:Label>
                                    <asp:Label ID="lblAttachID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CertificateOfParticleNo" HeaderText="Certificate Of Particle No" ItemStyle-Width="14%" />
                            <asp:BoundField DataField="PeriodOfArticlesFrom" HeaderText="Period Of Articles From" ItemStyle-Width="16%" />
                            <asp:BoundField DataField="PeriodOfArticlesTo" HeaderText="Period Of Articles To" ItemStyle-Width="12%" />
                            <asp:BoundField DataField="PeriodOfArticlesExtendedTo" HeaderText="Period Of Articles Extended To" ItemStyle-Width="17%" />
                            <asp:BoundField DataField="PeriodOfArticlesRemarks" HeaderText="Remarks" ItemStyle-Width="11%" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-12 col-md-12 pull-left" style="padding: 0px">
                        <br />
                        <h5><b>All Attachments</b></h5>
                    </div>
                    <br />
                    <asp:GridView ID="gvArticleAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                            <asp:TemplateField HeaderText="File Name" ItemStyle-Width="44%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                    <asp:LinkButton ID="File" runat="server" Font-Italic="true" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" ItemStyle-Width="24%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created" ItemStyle-Width="26%">
                                <ItemTemplate>
                                    <b>By : </b>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                    <b>On : </b>
                                    <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="left" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
          <%-- </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
        <ProgressTemplate>
            <div class="loader">
                <div style="z-index: 1000; margin-left: 350px; margin-top: 0px; opacity: 1; -moz-opacity: 1;">
                    <img alt="" src="/Images/pageloader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
    <div id="myEmpQualificationModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Educational Qualifications</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpQualificationModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblEQEducation" runat="server" Text="* Education"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEQEducation"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVEQEducation" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtEQEducation" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEQEducation" runat="server" ControlToValidate="txtEQEducation" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEQBoard" runat="server" Text="* University/Board"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEQBoard"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVEQBoard" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtEQBoard" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEQBoard" runat="server" ControlToValidate="txtEQBoard" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEQSchool" runat="server" Text="* School/College"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEQSchool"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RFVEQSchool" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtEQSchool" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEQSchool" runat="server" ControlToValidate="txtEQSchool" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEQYear" runat="server" Text="* Year"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEQYear" MaxLength="4" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVEQYear" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtEQYear" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEQYear" runat="server" ControlToValidate="txtEQYear" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEQMarks" runat="server" Text="* Marks In %"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEQMarks" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFVEQMarks" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtEQMarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEQMarks" runat="server" ControlToValidate="txtEQMarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEQRemarks" runat="server" Text="Remarks"></asp:Label>
                            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtEQRemarks" Height="50px"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEQRemarks" runat="server" ControlToValidate="txtEQRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEQ"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnEQNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnEQSave" ValidationGroup="ValidateEQ"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnEQUpdate" ValidationGroup="ValidateEQ" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnEQCancel" OnClientClick="CancelEQ()"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpCourseModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Conferences/Courses Sponsered</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpCourseModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblECSDate" runat="server" Text="* Date"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtECSDate"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSDate" runat="server" ControlToValidate="txtECSDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSDate" runat="server" ControlToValidate="txtECSDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtECSDate" PopupPosition="TopRight" TargetControlID="txtECSDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblECSSubject" runat="server" Text="* Subject"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSubject"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVSubject" runat="server" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVSubject" runat="server" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblECSFPEmployer" runat="server" Text="* Fees Paid Employer"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECSFPEmployer"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSFPEmployer" runat="server" ControlToValidate="txtECSFPEmployer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSFPEmployer" runat="server" ControlToValidate="txtECSFPEmployer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblECSFPEmployee" runat="server" Text="* Fees Paid Employee"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECSFPEmployee"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSFPEmployee" runat="server" ControlToValidate="txtECSFPEmployee" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSFPEmployee" runat="server" ControlToValidate="txtECSFPEmployee" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblECSConductedBy" runat="server" Text="* Conducted By"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECSConductedBy"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSConductedBy" runat="server" ControlToValidate="txtECSConductedBy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSConductedBy" runat="server" ControlToValidate="txtECSConductedBy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblECSCPEPoints" runat="server" Text="* CPE Points"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtECSCPEPoints"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSCPEPoints" runat="server" ControlToValidate="txtECSCPEPoints" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSCPEPoints" runat="server" ControlToValidate="txtECSCPEPoints" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblECSPapers" runat="server" Text="* Papers Presented"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtECSPapers" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSPapers" runat="server" ControlToValidate="txtECSPapers" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSPapers" runat="server" ControlToValidate="txtECSPapers" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblECSBriefDesc" runat="server" Text="* Brief Description"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtECSBriefDesc" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSBriefDesc" runat="server" ControlToValidate="txtECSBriefDesc" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSBriefDesc" runat="server" ControlToValidate="txtECSBriefDesc" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblECSFeedBack" runat="server" Text="* FeedBack Report Reference"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtECSFeedBack" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVECSFeedBack" runat="server" ControlToValidate="txtECSFeedBack" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSFeedBack" runat="server" ControlToValidate="txtECSFeedBack" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblECSRemarks" runat="server" Text="Remarks"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtECSRemarks" Height="50px"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVECSRemarks" runat="server" ControlToValidate="txtECSRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateECS"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnECSNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnECSSave" ValidationGroup="ValidateECS"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnECSUpdate" ValidationGroup="ValidateECS" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnECSCancel"></asp:Button>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpProfessionalExperienceModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Professional Experience</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpProfessionalExperienceModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEPEAssignment" runat="server" Text="* Employer"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEPEAssignment"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPEAssignment" runat="server" ControlToValidate="txtEPEAssignment" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPEAssignment" runat="server" ControlToValidate="txtEPEAssignment" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEPEReportingTo" runat="server" Text="* Reporting To"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEPEReportingTo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPEReportingTo" runat="server" ControlToValidate="txtEPEReportingTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPEReportingTo" runat="server" ControlToValidate="txtEPEReportingTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEPEFrom" runat="server" Text="* From"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEPEFrom" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPEFrom" runat="server" ControlToValidate="txtEPEFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPEFrom" runat="server" ControlToValidate="txtEPEFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEPETo" runat="server" Text="* To"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEPETo" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPETo" runat="server" ControlToValidate="txtEPETo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPETo" runat="server" ControlToValidate="txtEPETo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEPESalaryPerAnnum" runat="server" Text="* Salary Per Annum"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEPESalaryPerAnnum"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPESalaryPerAnnum" runat="server" ControlToValidate="txtEPESalaryPerAnnum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPESalaryPerAnnum" runat="server" ControlToValidate="txtEPESalaryPerAnnum" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEPEPositionHeld" runat="server" Text="* Position Held"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEPEPositionHeld"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPEPositionHeld" runat="server" ControlToValidate="txtEPEPositionHeld" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPEPositionHeld" runat="server" ControlToValidate="txtEPEPositionHeld" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEPERemarks" runat="server" Text="* Remarks"></asp:Label>
                            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtEPERemarks" Height="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEPERemarks" runat="server" ControlToValidate="txtEPERemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEPERemarks" runat="server" ControlToValidate="txtEPERemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEPE"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnEPENew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnEPESave" ValidationGroup="ValidateEPE"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnEPEUpdate" ValidationGroup="ValidateEPE" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnEPECancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpAssestsModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Assests Obtained On Loan</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpAssetsModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEATypeOfAsset" runat="server" Text="* Type Of Asset"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEATypeOfAsset"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEATypeOfAsset" runat="server" ControlToValidate="txtEATypeOfAsset" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEATypeOfAsset" runat="server" ControlToValidate="txtEATypeOfAsset" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEASerialNo" runat="server" Text="* Serial No"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEASerialNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEASerialNo" runat="server" ControlToValidate="txtEASerialNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEASerialNo" runat="server" ControlToValidate="txtEASerialNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEAApproValue" runat="server" Text="* Approximate Value"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEAApproValue" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEAApproValue" runat="server" ControlToValidate="txtEAApproValue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEAApproValue" runat="server" ControlToValidate="txtEAApproValue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEAIssueDate" runat="server" Text="* Issue Date"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtEAIssueDate"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtEAIssueDate" PopupPosition="TopRight" TargetControlID="txtEAIssueDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEAIssueDate" runat="server" ControlToValidate="txtEAIssueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEAIssueDate" runat="server" ControlToValidate="txtEAIssueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblEADueDate" runat="server" Text="* Due Date"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtEADueDate"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtEADueDate" PopupPosition="TopRight" TargetControlID="txtEADueDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEADueDate" runat="server" ControlToValidate="txtEADueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEADueDate" runat="server" ControlToValidate="txtEADueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <asp:Label ID="Label1" runat="server" Text="Received Date"></asp:Label>
                                <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtEARecievedDate"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender13" runat="server" PopupButtonID="txtEARecievedDate" PopupPosition="TopRight" TargetControlID="txtEARecievedDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEARecievedDate" runat="server" ControlToValidate="txtEARecievedDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEAConditionIssue" runat="server" Text="* Condition When Issued"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEAConditionIssue"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVEAConditionIssue" runat="server" ControlToValidate="txtEAConditionIssue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVEAConditionIssue" runat="server" ControlToValidate="txtEAConditionIssue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblEAConditionReceipt" runat="server" Text="Condition On Receipt"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtEAConditionReceipt"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEAConditionReceipt" runat="server" ControlToValidate="txtEAConditionReceipt" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblEARemarks" runat="server" Text="Remarks"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtEARemarks" Height="50px"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEARemarks" runat="server" ControlToValidate="txtEARemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateEA"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnALNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnALSave" ValidationGroup="ValidateEA"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnALUpdate" ValidationGroup="ValidateEA" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnALCancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpPerformanceAssessmentsModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Performance Assessment</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpPerformanceAssessmentModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPAAssessmentDate" runat="server" Text="* Issue Date"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtPAAssessmentDate"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAAssessmentDate" runat="server" ControlToValidate="txtPAAssessmentDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAAssessmentDate" runat="server" ControlToValidate="txtPAAssessmentDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtPAAssessmentDate" PopupPosition="TopRight" TargetControlID="txtPAAssessmentDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPARating" runat="server" Text="* Rating"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPARating"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPARating" runat="server" ControlToValidate="txtPARating" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPARating" runat="server" ControlToValidate="txtPARating" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPAPerformanceAwardPaid" runat="server" Text="* Performance Award Paid"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAPerformanceAwardPaid"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAPerformanceAwardPaid" runat="server" ControlToValidate="txtPAPerformanceAwardPaid" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAPerformanceAwardPaid" runat="server" ControlToValidate="txtPAPerformanceAwardPaid" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPAGradesPromotedFrom" runat="server" Text="* Grade Promoted From"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAGradesPromotedFrom"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAGradesPromotedFrom" runat="server" ControlToValidate="txtPAGradesPromotedFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAGradesPromotedFrom" runat="server" ControlToValidate="txtPAGradesPromotedFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblPAGradesPromotedTo" runat="server" Text="* Grade Promoted To"></asp:Label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPAGradesPromotedTo"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPAGradesPromotedTo" runat="server" ControlToValidate="txtPAGradesPromotedTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPAGradesPromotedTo" runat="server" ControlToValidate="txtPAGradesPromotedTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblPARemarks" runat="server" Text="Remarks"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtPARemarks" Height="50px"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPARemarks" runat="server" ControlToValidate="txtPARemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePA"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnPANew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnPASave" ValidationGroup="ValidatePA"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnPAUpdate" ValidationGroup="ValidatePA" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnPACancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpAcademicProgressModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Academic Progress</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpAcademicProgressModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblAPExamTaken" runat="server" Text="* Examination Taken"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtAPExamTaken" placeholder="dd/MM/yyyy" CssClass="aspxcontrols"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender14" runat="server" PopupButtonID="txtAPExamTaken" PopupPosition="TopRight" TargetControlID="txtAPExamTaken" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVAPExamTaken" runat="server" ControlToValidate="txtAPExamTaken" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVAPExamTaken" runat="server" ControlToValidate="txtAPExamTaken" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblAPLeaveGranted" runat="server" Text="* No. of Days Leave Granted"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtAPLeaveGranted" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)" MaxLength="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVAPLeaveGranted" runat="server" ControlToValidate="txtAPLeaveGranted" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVAPLeaveGranted" runat="server" ControlToValidate="txtAPLeaveGranted" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblAPMonthExam" runat="server" Text="* Month of Exam"></asp:Label>
                                    <asp:DropDownList ID="ddlAPMonthExam" runat="server" CssClass="aspxcontrols">
                                        <asp:ListItem Value="0">Select Month</asp:ListItem>
                                        <asp:ListItem Value="1">January</asp:ListItem>
                                        <asp:ListItem Value="2">February</asp:ListItem>
                                        <asp:ListItem Value="3">March</asp:ListItem>
                                        <asp:ListItem Value="4">April</asp:ListItem>
                                        <asp:ListItem Value="5">May</asp:ListItem>
                                        <asp:ListItem Value="6">June</asp:ListItem>
                                        <asp:ListItem Value="7">July</asp:ListItem>
                                        <asp:ListItem Value="8">August</asp:ListItem>
                                        <asp:ListItem Value="9">September</asp:ListItem>
                                        <asp:ListItem Value="10">October</asp:ListItem>
                                        <asp:ListItem Value="11">November</asp:ListItem>
                                        <asp:ListItem Value="12">December</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVAPMonthExam" runat="server" ControlToValidate="ddlAPMonthExam" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblAPGroups" runat="server" Text="* Groups"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtAPGroups"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVAPGroups" runat="server" ControlToValidate="txtAPGroups" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVAPGroups" runat="server" ControlToValidate="txtAPGroups" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblAPResult" runat="server" Text="* Result"></asp:Label>
                            <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtAPResult"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVAPResult" runat="server" ControlToValidate="txtAPResult" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVAPResult" runat="server" ControlToValidate="txtAPResult" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblAPRemarks" runat="server" Text="* Remarks"></asp:Label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtAPRemarks" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVAPRemarks" runat="server" ControlToValidate="txtAPRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVAPRemarks" runat="server" ControlToValidate="txtAPRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateAP"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnAPNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnAPSave" ValidationGroup="ValidateAP"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnAPUpdate" ValidationGroup="ValidateAP" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnAPCancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpSpecialMentionsModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Special Mentions</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpSpecialMentionsModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblSMSpecialMention" runat="server" Text="* Special Mentions"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSMSpecialMention"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVSMSpecialMention" runat="server" ControlToValidate="txtSMSpecialMention" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVSMSpecialMention" runat="server" ControlToValidate="txtSMSpecialMention" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblSMDate" runat="server" Text="* Date"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtSMDate"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtSMDate" PopupPosition="BottomRight" TargetControlID="txtSMDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVSMDate" runat="server" ControlToValidate="txtSMDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVSMDate" runat="server" ControlToValidate="txtSMDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblSMParticulars" runat="server" Text="* Particulars"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSMParticulars"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVSMParticulars" runat="server" ControlToValidate="txtSMParticulars" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVSMParticulars" runat="server" ControlToValidate="txtSMParticulars" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblSMDealtWith" runat="server" Text="* How Dealt With"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSMDealtWith"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVSMDealtWith" runat="server" ControlToValidate="txtSMDealtWith" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVSMDealtWith" runat="server" ControlToValidate="txtSMDealtWith" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSM"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnSMNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSMSave" ValidationGroup="ValidateSM"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnSMUpdate" ValidationGroup="ValidateSM" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnSMCancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpTransferswithintheFirmModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Transfers Within The Firm</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpTransferFirmModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblTFEarlierPrinciple" runat="server" Text="* Earlier Principal"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTFEarlierPrinciple"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTFEarlierPrinciple" runat="server" ControlToValidate="txtTFEarlierPrinciple" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTFEarlierPrinciple" runat="server" ControlToValidate="txtTFEarlierPrinciple" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblTENewPrinciple" runat="server" Text="* New Principal"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTENewPrinciple"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTENewPrinciple" runat="server" ControlToValidate="txtTENewPrinciple" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTENewPrinciple" runat="server" ControlToValidate="txtTENewPrinciple" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblTFDateTransfer" runat="server" Text="* Date of Transfer"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtTFDateTransfer"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtTFDateTransfer" PopupPosition="TopRight" TargetControlID="txtTFDateTransfer" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTFDateTransfer" runat="server" ControlToValidate="txtTFDateTransfer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTFDateTransfer" runat="server" ControlToValidate="txtTFDateTransfer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblTFDurationArticle" runat="server" Text="* Duration of Article with new Principal"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTFDurationArticle"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTFDurationArticle" runat="server" ControlToValidate="txtTFDurationArticle" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTFDurationArticle" runat="server" ControlToValidate="txtTFDurationArticle" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblTFCompletionDate" runat="server" Text="* Completion Date"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtTFCompletionDate"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtTFCompletionDate" PopupPosition="TopRight" TargetControlID="txtTFCompletionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTFCompletionDate" runat="server" ControlToValidate="txtTFCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTFCompletionDate" runat="server" ControlToValidate="txtTFCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblTFExtendedTo" runat="server" Text="* Extended To"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtTFExtendedTo"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="txtTFExtendedTo" PopupPosition="TopRight" TargetControlID="txtTFExtendedTo" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTFExtendedTo" runat="server" ControlToValidate="txtTFExtendedTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTFExtendedTo" runat="server" ControlToValidate="txtTFExtendedTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblTFRemarks" runat="server" Text="* Remarks"></asp:Label>
                            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtTFRemarks" Height="50px"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTFRemarks" runat="server" ControlToValidate="txtTFRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTFRemarks" runat="server" ControlToValidate="txtTFRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateTF"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnTFNew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnTFSave" ValidationGroup="ValidateTF"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnTFUpdate" ValidationGroup="ValidateTF" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnTFCancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myEmpParticularsofArticlesModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Particulars of Articles</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblEmpParticularsofArticlesModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPOAPrincipleName" runat="server" Text="* Name of the Principal"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPOAPrincipleName"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPOAPrincipleName" runat="server" ControlToValidate="txtPOAPrincipleName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOAPrincipleName" runat="server" ControlToValidate="txtPOAPrincipleName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPOAArticleRegNo" runat="server" Text="* Article Registration No"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPOAArticleRegNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPOAArticleRegNo" runat="server" ControlToValidate="txtPOAArticleRegNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOAArticleRegNo" runat="server" ControlToValidate="txtPOAArticleRegNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPOAPracticeNo" runat="server" Text="* Certificate of Practice No."></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPOAPracticeNo"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPOAPracticeNo" runat="server" ControlToValidate="txtPOAPracticeNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOAPracticeNo" runat="server" ControlToValidate="txtPOAPracticeNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPOAArticlesFrom" runat="server" Text="* Period of Articles From"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtPOAArticlesFrom"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender10" runat="server" PopupButtonID="txtPOAArticlesFrom" PopupPosition="TopRight" TargetControlID="txtPOAArticlesFrom" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPOAArticlesFrom" runat="server" ControlToValidate="txtPOAArticlesFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOAArticlesFrom" runat="server" ControlToValidate="txtPOAArticlesFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPOAArticlesTo" runat="server" Text="* Period of Articles To"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtPOAArticlesTo"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender11" runat="server" PopupButtonID="txtPOAArticlesTo" PopupPosition="TopRight" TargetControlID="txtPOAArticlesTo" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPOAArticlesTo" runat="server" ControlToValidate="txtPOAArticlesTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOAArticlesTo" runat="server" ControlToValidate="txtPOAArticlesTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblPOAArticlesExtendedTo" runat="server" Text="* Period of Articles Extended To"></asp:Label>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtPOAArticlesExtendedTo"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender12" runat="server" PopupButtonID="txtPOAArticlesExtendedTo" PopupPosition="TopRight" TargetControlID="txtPOAArticlesExtendedTo" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPOAArticlesExtendedTo" runat="server" ControlToValidate="txtPOAArticlesExtendedTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOAArticlesExtendedTo" runat="server" ControlToValidate="txtPOAArticlesExtendedTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblPOARemarks" runat="server" Text="Remarks"></asp:Label>
                            <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtPOARemarks" Height="50px"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVPOARemarks" runat="server" ControlToValidate="txtPOARemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePOA"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="New" class="btn-ok" ID="btnPOANew"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnPOASave" ValidationGroup="ValidatePOA"></asp:Button>
                        <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnPOAUpdate" ValidationGroup="ValidatePOA" Visible="false"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnPOACancel"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalEQAttachment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblEQMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblEQBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblEQSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtEQfile" runat="server" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddEQAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvEQAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalECSAttachment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblECSMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblECSBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblECSSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtECSfile" runat="server" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddECSAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvECSAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalProfessionalExperienceAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblPEMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblPEBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblPESize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtPEfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddPEAttach" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvProfessionalExperienceAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalAsstesLoanAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblALMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblALBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblALSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtALfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddALAttach" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvAsstesLoanAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalPAAttachment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblPAMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblPABrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblPASize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtPAfile" runat="server" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddPAAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvPAAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalAcademicProgressAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblAPMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblAPBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblAPSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtAPfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAPAttach" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvAcademicProgressAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalSMAttachment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblSMMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblSMBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSMSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtSMfile" runat="server" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddSMAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvSMAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalTransferFirmAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblTFMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblTFBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblTFSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtTFfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddTFAttach" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvTransferFirmAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myModalPOAAttachment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblPOAMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblPOABrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblPOASize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtPOAfile" runat="server" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddPOAAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <asp:GridView ID="gvPOAAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" Font-Names="true" CommandName="OPENPAGE" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="4%">
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </div>
    <div id="ModalEmpProfileValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblEmpProfileValidationMsg" runat="server"></asp:Label>
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
    <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

