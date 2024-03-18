<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="GeneralMasterDetails.aspx.vb" Inherits="TRACePA.GeneralMasterDetails" %>


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
                <h2><b>General Master Details</b></h2>
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
        <div class="col-sm-8 col-md-8">
            <div class="form-group">
                <asp:Label ID="lblMasterHead" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlDesc" textmode="MultiLine" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <br />
                <asp:Label ID="Label2" runat="server" Text="Status :-"></asp:Label>
                <asp:Label ID="lblGeneralMasterStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-8 col-md-8">
            <div class="form-group">
                <asp:Label ID="lblDesc" runat="server"></asp:Label>
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
        <br />
        <asp:Panel runat="server" ID="pnlRate" Visible="false">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblRate" runat="server" Text="* Rate in (%)"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRate" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="txtRate" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlAct" runat="server">
            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblHAct" Text="Act" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlAct" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="col-sm-1 col-md-1" style="padding: 0px">
                <div class="form-group">
                    <asp:ImageButton ID="imgbtnAddAct" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
                </div>
            </div>
        </asp:Panel>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-4 col-md-4">
            <div class="form-group">
                <asp:Label ID="lblHeadingHSN" Text="HSN/SAC" runat="server"></asp:Label>
                <asp:TextBox ID="txtHSN" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
        <br />
        <asp:Panel runat="server" ID="pnlTask" Visible="false">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:CheckBox ID="chkComplianceTask" runat="server" Text=" Is Compliance Task"></asp:CheckBox>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:Label ID="lblAct" Visible="false" runat="server" Text="" Width="100%"></asp:Label>
    <div id="ModalAct" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <asp:Label Font-Italic="true" Font-Names="serif pro" Font-Bold="true" Visible="false" ForeColor="#063970" runat="server" ID="lblHeading"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblid" Font-Names="serif pro" Font-Bold="true" ForeColor="#063970" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 ">
                    <div class="col-sm-5 col-md-5">
                        <asp:Label ID="lblname" Font-Names="serif pro" Font-Bold="true" ForeColor="#063970" runat="server" Text="* Act Name" Width="100%"></asp:Label>
                        <asp:TextBox ID="txtname" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSavedetails" ValidationGroup="ValidateSection"></asp:Button>
                    </div>
                </div>
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