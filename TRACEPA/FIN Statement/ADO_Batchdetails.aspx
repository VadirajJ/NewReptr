<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="ADO_Batchdetails.aspx.vb" Inherits="TRACePA.ADO_Batchdetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">&nbsp;&nbsp;
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <style>
        .btn:hover, .btn:focus {
            color: #fff;
            outline: 0;
        }

        .third {
            border-color: #3498db;
            color: #fff;
            box-shadow: 0 0 40px 40px #3498db inset, 0 0 0 0 #3498db;
            -webkit-transition: all 150ms ease-in-out;
            transition: all 150ms ease-in-out;
        }

            .third:hover {
                box-shadow: 0 0 10px 0 #3498db inset, 0 0 10px 4px #3498db;
            }
    </style>
    <script src="../JavaScripts/jquery-1.12.4.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(document).ready(function ($) {
            $.fn.wPaint.menus.main = {
                pencil: {
                    index: 21,
                    callback: (function () {
                        this.setMode('pencil');
                    }
                )
                }
            }

            $.fn.wPaint.extend({
                undoCurrent: -1,
                undoArray: [],
                setUndoFlag: true,

                _drawPencilDown: (function (e) {
                    this.ctx.lineJoin = 'round';
                    this.ctx.lineCap = 'round';
                    this.ctx.strokeStyle = this.options.strokeStyle;
                    this.ctx.fillStyle = this.options.strokeStyle;
                    this.ctx.lineWidth = this.options.lineWidth;

                    //draw single dot in case of a click without a move
                    this.ctx.beginPath();
                    this.ctx.arc(e.pageX, e.pageY, this.options.lineWidth / 2, 0, Math.PI * 2, true);
                    this.ctx.closePath();
                    this.ctx.fill();

                    //start the path for a drag
                    this.ctx.beginPath();
                    this.ctx.moveTo(e.pageX, e.pageY);
                }),

                _drawPencilMove: (function (e) {
                    this.ctx.lineTo(e.pageX, e.pageY);
                    this.ctx.stroke();
                }),

                _drawPencilUp: (function () {
                    this.ctx.closePath();
                    this._addUndo();
                })
            });
        })(jQuery);
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Digital Vouching - Batch</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
     <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div class="col-sm-3 col-md-3" style="padding-right: 0px">
        <asp:Label ID="lblcabinet" runat="server" Text="* Customer/Cabinet"></asp:Label>
        <asp:DropDownList ID="ddlcabinet" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
    </div>
    <div class="col-sm-3 col-md-3" style="padding-right: 0px">
        <asp:Label ID="Label1" runat="server" Text="* Fiancial Year"></asp:Label>
        <asp:DropDownList ID="ddlTransactiontype"  AutoPostBack ="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
    </div>
    <div class="col-sm-3 col-md-3" style="padding-right: 0px">
        <asp:Label ID="lblbatchNo" runat="server"  Text="* Transaction Type"></asp:Label>
        <asp:DropDownList ID="ddlBatch" runat="server" AutoPostBack ="true" CssClass="aspxcontrols"></asp:DropDownList>
    </div>
    <div class="col-sm-3 col-md-3" style="padding-right: 0px">
        <asp:Label ID="lblNoTransactions" runat="server"  Text="No Transactions"></asp:Label>
        <asp:TextBox ID="txtTransactions" runat="server" Enabled ="false"  CssClass="aspxcontrols"></asp:TextBox>
    </div>
     <div class="clearfix divmargin"></div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="col-sm-12 col-md-12" style="text-align: left">

                <asp:ImageButton ID="imgbtnNavDocFastRewind" Enabled="false" CssClass="activeIcons hvr-bounce-in" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNavDoc" CssClass="hvr-bounce-in" runat="server" AutoPostBack="true" data-toggle="tooltip" data-placement="bottom" title="Previous" Style="margin-right: 10px;" CausesValidation="false" />
                <asp:TextBox ID="txtNavDoc" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNavDoc" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNavDoc" CssClass="activeIcons hvr-bounce-in" runat="server" AutoPostBack="true" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnNavDocFastForword" Enabled="false" CssClass="activeIcons hvr-bounce-in" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                |
                <asp:ImageButton ID="imgbtnFastRewind" Enabled="false" AutoPostBack="true" CssClass="activeIcons hvr-bounce-in" runat="server" Style="margin-left: 10px;" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNav" CssClass="hvr-bounce-in" AutoPostBack="true" runat="server" data-toggle="tooltip" Style="margin-right: 10px;" data-placement="bottom" title="Previous" CausesValidation="false" />
                <asp:TextBox ID="txtNav" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNav" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNav" CssClass="activeIcons hvr-bounce-in" runat="server" AutoPostBack="true" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnFastForword" Enabled="false" CssClass="activeIcons hvr-bounce-in" AutoPostBack="true" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" />
            </div>

            <div class="row"></div>
            <div class="col-sm-6 col-md-6 pull-left" style="padding: 10px">
                <asp:Image ID="documentViewer" runat="server" Width="90%" Style="border-radius: 10px" Height="90%" AlternateText="Logo" />
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-sm-6 col-md-6 pull-left" style="padding: 0px">
                <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="true">
                    <div id="div2" runat="server">
                        <!-- Nav tabs -->
                        <ul class="nav nav-tabs" role="tablist">
                            <li id="liVoucherReciept" runat="server">
                                <asp:LinkButton ID="lnkVoucherReciept"  Text="Voucher Reciept" runat="server" Font-Bold="true" /></li>
                            <li id="liVoucherReciept2" visible="false" runat="server">
                                <asp:LinkButton ID="lnkVoucherReciept2" Text="Voucher Reciept1" runat="server" Font-Bold="true" /></li>
                        </ul>
                    </div>

                    <!-- Tab panes -->
                    <div class="tab-content divmargin">
                        <%--Uploaded Documents Tab--%>
                        <div runat="server" role="tabpanel" class="tab-pane active" id="divVoucherReciept">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <div class="col-sm-12 col-md-12 pull-left" style="padding: 0px">
                                    <asp:Label ID="lblVoucherReciept" runat="server" Visible="false" ForeColor="#408080"  Text="Voucher Reciept" CssClass="h5" Font-Bold="true"></asp:Label>
                                </div>
                                <br />
                                <br />
                                <div class="col-sm-12 col-md-12">
                                    <div class=" col-sm-4 col-md-4 form-group">
                                        <asp:Label runat="server"  Text="Voucher No"></asp:Label>
                                        <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtVocherno" ValidateRequestMode="Disabled" autoComplete="off"></asp:TextBox>
                                    </div>
                                    <div class=" col-sm-4 col-md-4 form-group">
                                        <asp:Label runat="server"  Text="* Date"></asp:Label>
                                        <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtdate" ValidateRequestMode="Disabled" autoComplete="off"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtdate" PopupPosition="BottomLeft"
                                            TargetControlID="txtdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-4 col-md-4 form-group">
                                        <asp:Label runat="server" Text="* Comments"></asp:Label>
                                        <asp:TextBox CssClass="aspxcontrols" runat="server" ID="txtComment" ValidateRequestMode="Disabled" autoComplete="off"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix divmargin"></div>
                                <div class="col-sm-12 col-md-12" style="margin-left: 30%">
                                    <div class="col-sm-3 col-md-3 form-group">
                                        <asp:RadioButton ID="rblAccept" runat="server"  Text="Accepted" GroupName="rdb" />
                                    </div>
                                    <div class="col-sm-3 col-md-3 form-group">
                                        <asp:RadioButton ID="rblReject" runat="server" Text="Rejected" GroupName="rdb" />
                                    </div>
                                </div>
                                <div class="clearfix divmargin"></div>
                                <div class="col-sm-12 col-md-12" style="margin-left: 40%">
                                    <button runat="server" id="btnsumbit" class="btn third" style="text-align: center; margin: 0; width: 13%; height: 30px; background: #2c3e50; font-family: 'Montserrat', sans-serif; font-size: 12px; display: -webkit-box; display: flex; flex-wrap: wrap; justify-content: space-around; -webkit-box-align: center; align-items: center; align-content: center;">
                                        Submit</button>
                                </div>
                            </div>
                        </div>
                        <%--Shared Documents Tab--%>
                        <div runat="server" role="tabpanel" class="tab-pane" id="divVoucherReciept2">
                            <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                                <asp:Label ID="lblVoucherReciept2" runat="server" Text="Voucher Reciept2" CssClass="h5" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="myModalUploadDocuments" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attach Documents</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-5 col-md-5" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtFile" runat="server" AllowMultiple="true" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

     <div id="ModalADOValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblADOValidationMsg" runat="server"></asp:Label>
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


