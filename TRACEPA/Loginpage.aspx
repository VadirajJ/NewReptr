<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Loginpage.aspx.vb" Inherits="TRACePA.Loginpage" ViewStateEncryptionMode="Always" EnableEventValidation="false" %>

<!doctype html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <title>TRACe</title>
    <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/login.css" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css">
    <style>
        .input-icons i {
            position: absolute;
        }

        .input-icons {
            width: 100%;
        }

        .icon {
            padding: 18px 0px 0px 40px;
            min-width: fit-content;
        }

        .input-field {
            width: auto;
            text-align: left;
            margin: auto;
        }
    </style>
    
    <script src="JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="JavaScripts/aes.js" type="text/javascript"></script>
    <script src="JavaScripts/html5shiv.js" type="text/javascript"></script>
    <script src="JavaScripts/respond.min.js" type="text/javascript"></script>

    <script type="text/jscript">
        $(document).ready(function () {
            $("#txtUserName").focus();
            $('#btnOk').click(function () {
                $('#ModalValidation').modal('hide');
                $('#ModalYesNo').modal('hide');
                $('#ModalChangePassword').modal('hide');
                $('#ModalOKtoCP').modal('hide');
                $('#ModalPEAYesNo').modal('hide');
                $('#ModalForgotPassword').modal('hide');

                if ($("#txtAccessCode").val() == "") {
                    $("#txtAccessCode").focus();
                    return false;
                }
                if ($("#txtUserName").val() == "") {
                    $("#txtUserName").focus();
                    return false;
                }
                if ($("#txtPassword").val() == "") {
                    $("#txtPassword").focus();
                    return false;
                }
            })

            $('#lnkbtnForgotPassword').click(function () {
                $('#ModalYesNo').modal('hide');
                $('#ModalChangePassword').modal('hide');
                $('#ModalOKtoCP').modal('hide');
                $('#ModalPEAYesNo').modal('hide');
                $('#ModalForgotPassword').modal('hide');

                if ($("#txtAccessCode").val() == "") {
                    $('#lblValidationMsg').html("Enter access code.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#txtUserName").val() == "") {
                    $('#lblValidationMsg').html("Enter user name.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
            })

            $('#imgbtnLogin').click(function () {
                document.getElementById('<%=txtScreenWidth.ClientID %>').value = $(window).width();
                document.getElementById('<%=txtScreenHeight.ClientID %>').value = $(window).height();
                $('#ModalYesNo').modal('hide');
                $('#ModalChangePassword').modal('hide');
                $('#ModalOKtoCP').modal('hide');
                $('#ModalPEAYesNo').modal('hide');
                $('#ModalForgotPassword').modal('hide');

                if ($("#txtAccessCode").val() == "") {
                    $('#lblValidationMsg').html("Enter access code.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#txtUserName").val() == "") {
                    $('#lblValidationMsg').html("Enter user name.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }
                if ($("#txtPassword").val() == "") {
                    $('#lblValidationMsg').html("Enter password.");
                    $("#divMsgType").addClass("alert alert-warning");
                    $('#ModalValidation').modal('show');
                    return false;
                }

                var key = CryptoJS.enc.Utf8.parse('8080808080808080');
                var iv = CryptoJS.enc.Utf8.parse('8080808080808080');
                var encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse($("#txtPassword").val()), key,
                    {
                        keySize: 128,
                        iv: iv,
                        mode: CryptoJS.mode.CBC,
                        padding: CryptoJS.pad.Pkcs7
                    });
                document.getElementById('<%=txtActualPassword.ClientID %>').value = encrypted
                document.getElementById('<%=txtPassword.ClientID %>').value = ''
                document.getElementById('<%=txtPassword.ClientID %>').type = 'text'
                return true;
            })
        });
        function CheckUserLoginSystem() {
            alert('Your account was logged into from other browser/system. If you click ok then previous session will be closed.')
            __doPostBack('<%=lnkbtnHomepage.UniqueID %>', "Click")
        }
    </script>
</head>
<body class="sb-nav-fixed">
    <div id="container" class="col-sm-12 col-md-12 col-lg-12  login">
        <div class="col-md-3 col-md-offset-4 col-sm-4 col-sm-offset-3">
            <form role="form" runat="server" autocomplete="off">
                <div class="card item-card card-block">
                    <div class="panel panel-info">
                        <div class="panel-heading">
                            <asp:ImageButton ID="imgbtnLoginLog" runat="server" Height="40" Style="margin: 0px 0px 0px 0px; text-align: center" Visible="false" />
                            <%--<h2 style=" text-align:center;">TRACepa</h2>--%>

                            <%--<asp:ImageButton ID="imgbtnLoginLog" runat="server" Height="55" Style="margin: 0px 0px 0px 0px; text-align:center" Visible="true" />--%>
                            <h5 style="text-align: center; color: #83ace2">Login</h5>
                        </div>
                        <div class="panel-body" style="padding: 10px; text-align: center" id="loginform">
                            <div class="form-group input-icons" style="margin-bottom: 15px">
                                <i class="fa fa-key icon fa-xl" style="color: #223f65;" aria-hidden="true"></i>
                                <asp:TextBox autocomplete="off" ID="txtAccessCode" runat="server" Style="border: none; box-shadow: none; border-bottom: 2px solid; border-radius: 0px" placeholder="Access Code" value="" class="input-field form-control"></asp:TextBox>
                            </div>
                            <div class="form-group input-icons" style="margin-bottom: 15px">
                                <i class="fa fa-user-circle-o icon fa-xl" style="color: #223f65" aria-hidden="true"></i>
                                <asp:TextBox autocomplete="off" ID="txtUserName" Style="border: none; box-shadow: none; border-bottom: 2px solid; border-radius: 0px" runat="server" ToolTip="Enter User name" placeholder="User name" value="" class="input-field form-control" onpaste="return false" oncopy="return false" onkeyup="nospaces(this)"></asp:TextBox>
                            </div>
                            <div class="form-group input-icons" style="margin-bottom: 15px">
                                <i class="fa fa-eye-slash icon fa-xl" style="color: #223f65" aria-hidden="true"></i>
                                <%--<span class="input-group-addon"><i class="glyphicon glyphicon-password"></i></span>--%>
                                <asp:TextBox autocomplete="off" ID="txtPassword" Style="border: none; box-shadow: none; border-bottom: 2px solid; border-radius: 0px" runat="server" placeholder="Password" class="form-control input-field" TextMode="Password" onpaste="return false" oncopy="return false" onkeyup="nospaces(this)"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="imgbtnLogin" BackColor="#223f65" ForeColor="#83ace2" runat="server" CssClass="btn btn-primary btn-lg" OnClick="btnLogin_Click" Text="Login" Style="text-align: center" />
                                <asp:LinkButton ForeColor="#83ace2" Font-Size="Small" CssClass="btn-sm" runat="server" ID="lnkbtnForgotPassword" Text="Forgot password?" OnClick="lnkbtnForgotPassword_Click"></asp:LinkButton>
                                <%--<asp:ImageButton ID="imgbtnLogin" Width="100%" runat="server" Text="Login" OnClick="btnLogin_Click"/>--%>
                                <div class="clearfix"></div>
                            </div>
                            <div class="forgotPwd pull-right">
                            </div>
                            <div>
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div>
                                <asp:HiddenField ID="txtActualPassword" runat="server" />
                                <asp:HiddenField ID="txtScreenWidth" runat="server" />
                                <asp:HiddenField ID="txtScreenHeight" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>TRACe</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divMsgType" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblValidationMsg" runat="server"></asp:Label></strong>
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

                <div id="ModalOKtoCP" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>TRACe</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divOKtoCP" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblOKtoCP" runat="server"></asp:Label></strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <button data-dismiss="modal" runat="server" class="btn-ok" id="btnOKtoCP">
                                    OK
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalYesNo" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>TRACe</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divYesNoMsgType" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblYesNoMsg" runat="server"></asp:Label></strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <button runat="server" class="btn-ok" id="btnYES">
                                    Yes
                                </button>
                                <button data-dismiss="modal" runat="server" class="btn-ok" id="btnNO">
                                    No
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalPEAYesNo" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>TRACe</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divPEAYesNoMsgType" class="alert alert-warning">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblPEAYesNoMsg" runat="server"></asp:Label></strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <asp:Button runat="server" Text="Yes" class="btn-ok" ID="btnPEAYes" OnClick="btnPEAYes_Click"></asp:Button>
                                <asp:Button runat="server" Text="No" class="btn-ok" ID="btnPEANo" OnClick="btnPEANo_Click"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalChangePassword" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title"><b>Change Password</b></h4>
                                <asp:Label ID="lblCPError" runat="server" data-backdrop="static" data-keyboard="false" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:Label ID="lblCurrentPasssword" runat="server" Text="* Old Password"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCurrentPasssword" runat="server" ControlToValidate="txtCurrentPasssword" Display="Static" ErrorMessage="Enter Old Password." SetFocusOnError="True" ValidationGroup="pwd"></asp:RequiredFieldValidator>
                                    <asp:TextBox autocomplete="off" ID="txtCurrentPasssword" runat="server" CssClass="aspxcontrols" TextMode="Password" onpaste="return false" />
                                    <asp:CompareValidator CssClass="ErrorMsgLeft" runat="server" ID="CVCurrentPasssword" ControlToValidate="txtCurrentPasssword" Operator="Equal" Type="String" ErrorMessage="Invalid Old Password." ValidationGroup="pwd" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblNewPassword" runat="server" Text="New Password"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNewPasssword" runat="server" ControlToValidate="txtNewPassword" Display="Static" ErrorMessage="Enter New Password." SetFocusOnError="True" ValidationGroup="pwd"></asp:RequiredFieldValidator>
                                    <asp:TextBox autocomplete="off" ID="txtNewPassword" runat="server" CssClass="aspxcontrols" TextMode="Password" onpaste="return false" />
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="RegExpNewPwd" ControlToValidate="txtNewPassword" ValidationExpression="sRegExpNewPwd" runat="server" ErrorMessage="Follow Password policy." ValidationGroup="pwd"></asp:RegularExpressionValidator>
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword" Display="Static" ErrorMessage="Confirm Password." SetFocusOnError="True" ValidationGroup="pwd"></asp:RequiredFieldValidator>
                                    <asp:TextBox autocomplete="off" ID="txtConfirmPassword" runat="server" CssClass="aspxcontrols" TextMode="Password" onpaste="return false" />
                                    <asp:CompareValidator CssClass="ErrorMsgLeft" runat="server" ID="CVConfirmPassword" ControlToValidate="txtNewPassword" ControlToCompare="txtConfirmPassword" Operator="Equal" Type="String" ErrorMessage="Passwords does not match." ValidationGroup="pwd" />
                                </div>
                                <br />
                                <div class="form-group">
                                    <asp:Label ID="lblCPNoteHeading" runat="server" Text="Note : "></asp:Label>
                                    <asp:Label ID="lblCONote" runat="server" Text="" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="form-group pull-right">
                                    <asp:Button runat="server" Text="Change" class="btn-ok" ID="btnCPUpdate" ValidationGroup="pwd" OnClick="btnCPUpdate_Click"></asp:Button>
                                    <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCPCancel" OnClick="btnCPCancel_Click"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalForgotPassword" class="modal" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title"><b>Password Retrive</b></h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <asp:Label ID="lblFPNoteHeading" runat="server" CssClass="aspxlabelbold" Text="If you've forgotten the password to your account, please confirm your security answer and we will provide your password."></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadinglogin" runat="server" Text="Login Name : "></asp:Label>
                                    <asp:Label ID="lblFPLogin" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingQuestion" runat="server" Text="Security Question : "></asp:Label>
                                    <asp:Label ID="lblQue" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingAnswer" runat="server" Text="Security Answer"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAnswer" runat="server" ControlToValidate="txtAnswer" Display="Dynamic" ErrorMessage="Enter valid Answer." SetFocusOnError="True" ValidationGroup="Ans"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator CssClass="ErrorMsgRight" runat="server" ID="CVAnswer" ControlToValidate="txtAnswer" Type="String" ErrorMessage="Invalid answer." ValidationGroup="Ans" />
                                    <asp:TextBox autocomplete="off" ID="txtAnswer" runat="server" CssClass="aspxcontrols" onpaste="return false"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Button ID="btnGetPassword" runat="server" CssClass="btn-ok pull-right" Text="Get Password" ValidationGroup="Ans"></asp:Button>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingPassword" runat="server" Text="Your Password : "></asp:Label>
                                    <asp:Label ID="lblPWD" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:LinkButton ID="lnkbtnHomepage" runat="server" Visible="false">Go To Homepage</asp:LinkButton>
            </form>
        </div>
    </div>
</body>
</html>
