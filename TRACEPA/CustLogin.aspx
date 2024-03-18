<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CustLogin.aspx.vb" Inherits="TRACePA.CustLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" />
    <title>MMCSPL</title>
     <link rel="stylesheet" href="StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="StyleSheet/login.css" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css ">
  <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css ">
    <style>
        .input-icons i {
            position: absolute;
        }

        .input-icons {
            width: 100%;
            text-align:left;
        }

        .icon {
            padding: 18px 0px 0px 40px;
            min-width: fit-content;
        }

     .input-field {
            width: auto;
            text-align: left;
            padding-left: 40px;   
            margin:auto;
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
            $('#imgbtnLogin').click(function () {
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
                return true;
            })

            $('#btnOk').click(function () {
                if ($("#txtUserName").val() == "") {
                    $("#txtUserName").focus();
                    return true;
                }
                if ($("#txtPassword").val() == "") {
                    $("#txtPassword").focus();
                    return true;
                }

            })
        });
    </script>
</head>
<body class="sb-nav-fixed">
    <div id="container" class="col-sm-12 col-md-12 col-lg-12  login">
        <div class="col-md-3 col-md-offset-4 col-sm-4 col-sm-offset-3">
            <form role="form" runat="server"  autocomplete="off">
                <div class="card item-card card-block">
                <div class="panel panel-info" >

                    <div class="panel-heading">
                        <h5 style="text-align: center; color: #4f7942">Login</h5>
                    </div>

                    <div class="panel-body" style="padding-top: 20px" id="loginform">
                        <div class="input-group" style="margin-bottom: 15px">
                            <span class="input-group-addon"> <i class="fa fa-user-circle-o icon fa-xl" style="color:#4f7942" aria-hidden="true"></i> </span>
                            <asp:TextBox autocomplete="off" ID="txtUserName" style="background-color:white"  runat="server" class="form-control input-field" placeholder="User Name" value=""  onpaste="return false" oncopy="return false" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                        <div class="input-group" style="margin-bottom: 15px">
                            <span class="input-group-addon"> <i class="fa fa-eye-slash icon fa-xl" style="color:#4f7942" aria-hidden="true"></i></span>
                            <asp:TextBox autocomplete="off" ID="txtPassword" style="background-color:white" runat="server" placeholder="Password" class="form-control input-field" TextMode="Password" onpaste="return false" oncopy="return false" onkeyup="nospaces(this)"></asp:TextBox>
                        </div>
                        <div style="margin-top: 15px;">
                          <%--  <asp:ImageButton ID="imgbtnLogin"  runat="server" Width="280px" />--%>
                             <asp:Button ID="imgbtnLogin" BackColor="#4f7942" ForeColor="#ffffff" runat="server" CssClass="btn btn-primary btn-lg"  Text="Login" style="text-align:center"/>
                            <div class="clearfix"></div>
                        </div>
                    </div>
                </div>
                     </div>
                <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog" data-background="static">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>MMCSPL</b></h4>
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
                <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content row">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4 class="modal-title">Server Details</h4>
                            </div>
                            <div class="modal-body row">
                                <div class="col-sm-12 col-md-12">
                                    <div class="pull-left">
                                        <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                    </div>
                                </div>
                                <div class=" col-sm-12 col-md-12 form-group">
                                    <asp:Label ID="lblServerName" runat="server" Text="* SQL Server Name"></asp:Label>
                                    <asp:TextBox ID="txtServerName" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                </div>
                                <div class="col-sm-12 col-md-12 form-group">
                                    <asp:Label ID="lblLogin" runat="server" Text="* Login"></asp:Label>
                                    <asp:TextBox ID="txtLogin" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                </div>
                                <div class="col-sm-12 col-md-12 form-group">
                                    <asp:Label ID="lblPassword" runat="server" Text="* Password"></asp:Label>
                                    <asp:TextBox ID="txtsPassword" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                </div>
                                <div class="col-sm-12 col-md-12 form-group">
                                    <asp:Label ID="Label1" runat="server" Text="* Database Name"></asp:Label>
                                    <asp:TextBox ID="txtDatabase" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="pull-right">
                                    <asp:Button runat="server" Text="Connect" class="btn-ok" ID="btnDescNew"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    </div>
</body>
</html>
