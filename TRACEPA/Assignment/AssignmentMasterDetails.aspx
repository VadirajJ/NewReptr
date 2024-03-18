<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssignmentMasterDetails.aspx.vb" Inherits="TRACePA.AssignmentMasterDetails" %>

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
            $('#<%=ddlDesc.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Assignment Master Details</b></h2>
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
                <asp:Label ID="lblMasterHead" runat="server" Text="Existing Assignment Sub Task"></asp:Label>
                <asp:DropDownList ID="ddlDesc" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
         <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <br />
                <asp:Label ID="Label1" runat="server" Text="Audit Assignment : "></asp:Label>
                <asp:Label ID="lblAuditAssigment" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <br />
                <asp:Label ID="Label2" runat="server" Text="Status : "></asp:Label>
                <asp:Label ID="lblGeneralMasterStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8">
            <div class="form-group">
                <asp:Label ID="lblDesc" runat="server" Text="* Assignment Sub Task"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescName" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescName" runat="server" ControlToValidate="txtDesc" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtDesc" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblCode" runat="server" Text="* Code"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCode" runat="server" ControlToValidate="txtCode" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtCode" runat="server" CssClass="aspxcontrols" MaxLength="10" Enabled="False"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8">
            <div class="form-group">
                <asp:Label ID="lblNotes" runat="server" Text="Notes"></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNotes" runat="server" ControlToValidate="txtNotes" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" MaxLength="100" CssClass="aspxcontrols" Height="52px"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Billing Type"></asp:Label>
                <asp:DropDownList ID="ddlBillingType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div id="ModalGeneralMasterDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
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
</asp:Content>