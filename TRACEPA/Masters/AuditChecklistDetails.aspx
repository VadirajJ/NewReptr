<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AuditChecklistDetails.aspx.vb" Inherits="TRACePA.AuditChecklistDetails" EnableEventValidation="false"%>

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
            $('#<%=ddlAuditChecklist.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Audit Checklist Master Details</b></h2>
            </div>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblMasterHead" runat="server" Text="Existing Audit Checklist"></asp:Label>
                <asp:DropDownList ID="ddlAuditChecklist" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <br />
                <asp:Label ID="Label1" runat="server" Text="Audit Type : "></asp:Label>
                <asp:Label ID="lblAuditType" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <br />
                <asp:Label ID="Label2" runat="server" Text="Status : "></asp:Label>
                <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8">
            <div class="form-group">
                <asp:Label ID="lblNotes" runat="server" Text="* Checkpoint"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCheckpoint" runat="server" ControlToValidate="txtCheckpoint" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCheckpoint" runat="server" ControlToValidate="txtCheckpoint" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtCheckpoint" runat="server" TextMode="MultiLine" MaxLength="8000" CssClass="aspxcontrols" Height="85px"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblCode" runat="server" Text="Code"></asp:Label>
                <asp:TextBox ID="txtCode" runat="server" CssClass="aspxcontrols" MaxLength="10" Enabled="False"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <div class="col-sm-9 col-md-9">
                <div class="form-group">
                    <asp:Label ID="lblHHeading" Text="Heading" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlHeading" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFVHeading" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlHeading" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateHeading"></asp:RequiredFieldValidator>
                </div>
            </div>
            <br />
            <div class="col-sm-3 col-md-3" style="padding: 0px">
                <div class="form-group">
                    <asp:ImageButton ID="imgbtnAddHeading" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New Heading" />
                    <asp:ImageButton ID="imgbtnEditHeading" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update Heading" style="height: 15px;" ValidationGroup="ValidateHeading" />
                </div>
            </div>
        </div>
    </div>
    <div id="ModalAuditTypeMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblGeneralMasterDetailsValidationMsg" runat="server"></asp:Label>
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
    <div id="ModalHeading" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Add/Update Heading</b></h4>
                    <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblHSaveHeading" runat="server" Text="* Heading"></asp:Label>
                        <asp:TextBox ID="txtHeading" runat="server" CssClass="aspxcontrols" MaxLength="2000"></asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVHeading" runat="server" ControlToValidate="txtHeading" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateHeading"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSavedetails" ValidationGroup="ValidateHeading"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
