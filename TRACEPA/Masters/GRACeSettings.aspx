<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GRACeSettings.aspx.vb" Inherits="TRACePA.GRACeSettings" %>

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
            $('#<%=ddlDateFormat.ClientID%>').select2();
            $('#<%=ddlCurrency.ClientID%>').select2();
            $('#<%=ddlFilesDB.ClientID%>').select2();
            $('#<%=ddlFileSize.ClientID%>').select2();
            $('#<%=ddlSessionTimeOut.ClientID%>').select2();
            $('#<%=ddlSessionTimeOutWarning.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <ssdiv class="loader"></ssdiv>
    <script lang="javascript" type="text/javascript">
        function ValidateFilesDB() {
            if (document.getElementById('<%=ddlFilesDB.ClientID %>').selectedIndex == "0") {
                document.getElementById('<%=lblAttachmentfilepath.ClientID %>').innerHTML = 'Attachment File Path'
                document.getElementById('<%=txtFileInDBPath.ClientID%>').disabled = true;
                document.getElementById('<%=txtFileInDBPath.ClientID %>').value = '';
                return false;
            }
            if (document.getElementById('<%=ddlFilesDB.ClientID %>').selectedIndex == "1") {
                document.getElementById('<%=txtFileInDBPath.ClientID%>').disabled = false;
                document.getElementById('<%=txtFileInDBPath.ClientID %>').value = ''
                document.getElementById('<%=lblAttachmentfilepath.ClientID %>').innerHTML = '* Attachment File Path'
                document.getElementById('<%=txtFileInDBPath.ClientID %>').focus();
                return false;
            }
            return true;
        }

    </script>

    <div class="col-sm-12 col-md-12">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        <asp:CompareValidator CssClass="ErrorMsgLeft" runat="server" ID="CVMaxNoPwdChar" ControlToValidate="txtMaxNoPwdChar" ControlToCompare="txtMinNoPwdChar" Operator="GreaterThan" SetFocusOnError="true" Type="Integer" ValidationGroup="pwd" />
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="TRACe Settings" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 10px;">
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" TabIndex="18" ValidationGroup="Validate" />
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
            </DIV>
        <div class="card">
            <h4><b>&nbsp;Application Settings</b></h4>
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblImagepath" runat="server" Text="Image Path"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVImagePath" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtImgPath" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtImgPath" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblErrorlogpath" runat="server" Text="Error Log Path"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVErrorLog" runat="server" ControlToValidate="txtErrorLog" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtErrorLog" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblApptempdirectory" runat="server" Text="Application Temp Directory"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVApplicationtempDirectory" runat="server" ControlToValidate="txtExcelPath" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtExcelPath" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblFTPServer" runat="server" Text="FTP Server"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVFTP" runat="server" ControlToValidate="txtFTPServer" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtFTPServer" runat="server" autocomplete="off" TabIndex="-1" CssClass="aspxcontrolsdisable"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblMaxfilesize" runat="server" Text="Max File Size"></asp:Label>
                        <asp:DropDownList ID="ddlFileSize" runat="server" TabIndex="-1" CssClass="aspxcontrols" Visible="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblCurrencyType" runat="server" Text="Currency Type"></asp:Label>
                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="aspxcontrols" TabIndex="2">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblSessiontimeout" runat="server" Text="Session Time Out"></asp:Label>
                        <asp:DropDownList ID="ddlSessionTimeOut" runat="server" TabIndex="3" CssClass="aspxcontrols" Visible="true">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblTimeoutwarningbefore" runat="server" Text="Time Out Warning Before"></asp:Label>
                        <asp:DropDownList ID="ddlSessionTimeOutWarning" runat="server" TabIndex="4" CssClass="aspxcontrols" Visible="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblHTP" runat="server" Text="HTTP"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVHTP" runat="server" ControlToValidate="txtHTP" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="txtHTP" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblDateFormat" runat="server" Text="Date Format"></asp:Label>
                        <asp:DropDownList ID="ddlDateFormat" runat="server" CssClass="aspxcontrolsdisable" TabIndex="-1">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblFileinDB" runat="server" Text="File in DB"></asp:Label>
                        <asp:DropDownList ID="ddlFilesDB" runat="server" CssClass="aspxcontrols" TabIndex="5">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblAttachmentfilepath" runat="server" Text="Attachment File Path"></asp:Label>
                        <asp:TextBox ID="txtFileInDBPath" runat="server" autocomplete="off" CssClass="aspxcontrols" TabIndex="6" Enabled="False" MaxLength="105"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="REVFileInDBPath" runat="server" ControlToValidate="txtFileInDBPath" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="Out Look"></asp:Label>
                        <asp:TextBox ID="txtOutLook" runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" TabIndex="-1"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 row">
                <h4><b>&nbsp;Password Management</b></h4>
            </div>
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblMinPasswordCharacter" runat="server" Text="* Min Password Character"></asp:Label>
                        <asp:TextBox ID="txtMinNoPwdChar" runat="server" TabIndex="7" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVMinNoPwdChar" runat="server" ControlToValidate="txtMinNoPwdChar" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVMinNoPwdChar" runat="server" SetFocusOnError="True" Display="Dynamic" CssClass="ErrorMsgRight" ControlToValidate="txtMinNoPwdChar" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblMaxPasswordCharacter" runat="server" Text="* Max Password Character"></asp:Label>
                        <asp:TextBox ID="txtMaxNoPwdChar" runat="server" TabIndex="8" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVMaxNoPwdChar" runat="server" ControlToValidate="txtMaxNoPwdChar" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVMaxNoPwdChar" runat="server" ControlToValidate="txtMaxNoPwdChar" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblNoofRecoveryAttempts" runat="server" Text="* No. of Recovery Attempts"></asp:Label>
                        <asp:TextBox ID="txtRecovryAttempts" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2" TabIndex="9"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVRecovryAttempts" runat="server" ControlToValidate="txtRecovryAttempts" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVRecovryAttempts" runat="server" ControlToValidate="txtRecovryAttempts" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblUnsuccessfulAttempts" runat="server" Text="* Unsuccessful Attempts"></asp:Label>
                        <asp:TextBox ID="txtUnSuccAttempt" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2" TabIndex="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVUnSuccAttempt" runat="server" ControlToValidate="txtUnSuccAttempt" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVUnSuccAttempt" runat="server" ControlToValidate="txtUnSuccAttempt" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblPasswordexpirydays" runat="server" Text="* Password Expiry Days"></asp:Label>
                        <asp:TextBox ID="txtPasswordExpiry" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="3" TabIndex="11"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPasswordExpiry" runat="server" ControlToValidate="txtPasswordExpiry" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVPasswordExpiry" runat="server" ControlToValidate="txtPasswordExpiry" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblPasswordExpiryAlertdays" runat="server" Text="* Password Expiry Alert Days"></asp:Label>
                        <asp:TextBox ID="txtAlertDays" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2" TabIndex="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVAlertDays" runat="server" ControlToValidate="txtAlertDays" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVAlertDays" runat="server" ControlToValidate="txtAlertDays" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblDormantdays" runat="server" Text="* Dormant(Not Login) Days"></asp:Label>
                        <asp:TextBox ID="txtNumberofLogin" runat="server" autocomplete="off" data-toggle="tooltip" data-placement="top" title="Only numbers" CssClass="aspxcontrols" MaxLength="2" TabIndex="13"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVNumberofLogin" runat="server" ControlToValidate="txtNumberofLogin" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVNumberofLogin" runat="server" ControlToValidate="txtNumberofLogin" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Password Contains"></asp:Label>
                        <asp:CheckBoxList ID="ChkPasswordContains" runat="server" Enabled="False" RepeatColumns="2" CssClass="myCheckbox">
                        </asp:CheckBoxList>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 row pull-left">
                <h4><b>&nbsp;E-Mail Settings</b></h4>
            </div>
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblSMTPAddress" runat="server" Text="* SMTP Address"></asp:Label>
                        <asp:TextBox ID="txtIPAddress" runat="server" autocomplete="off" MaxLength="15" CssClass="aspxcontrols" TabIndex="14"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVIPAddress" runat="server" ControlToValidate="txtIPAddress" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVIPAddress" runat="server" ControlToValidate="txtIPAddress" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblEMailID" runat="server" Text="* Sender E-Mail ID"></asp:Label>
                        <asp:TextBox ID="txtSenerEID" runat="server" autocomplete="off" CssClass="aspxcontrols" TabIndex="15" MaxLength="200"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVSenerEID" runat="server" ControlToValidate="txtSenerEID" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVSenerEID" runat="server" ControlToValidate="txtSenerEID" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblPortnumber" runat="server" Text="* Port Number"></asp:Label>
                        <asp:TextBox ID="txtPort" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="4" data-toggle="tooltip" data-placement="top" title="Only numbers" TabIndex="16"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVPort" runat="server" ControlToValidate="txtPort" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVPort" runat="server" ControlToValidate="txtPort" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblSMS" runat="server" Text="* SMS Sender ID"></asp:Label>
                        <asp:TextBox ID="txtSMS" runat="server" autocomplete="off" CssClass="aspxcontrols" MaxLength="15" TabIndex="17"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RFVSMS" runat="server" ControlToValidate="txtSMS" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="REVSMS" runat="server" ControlToValidate="txtSMS" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
