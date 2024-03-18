<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="LocationSetUp.aspx.vb" Inherits="TRACePA.LocationSetUp" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        /*     div {
            color: black;
        }*/
    </style>

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlLocation.ClientID%>').select2();
            $('#<%=ddlDivision.ClientID%>').select2();
            $('#<%=ddlDepartment.ClientID%>').select2();
            $('#<%=ddlBayi.ClientID%>').select2();
            $('#<%=ddlHeading.ClientID%>').select2();
            $('#<%=ddlsubheading.ClientID%>').select2();
            $('#<%=ddlItems.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })


    </script>
    <div class="loader"></div>


    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">

        <div class="col-sm-12 col-md-12 col-lg-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>


    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa-regular fa-compass" style="font-size: large"></i>
        
                &nbsp;
                <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Asset SetUp" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Approve" />

                </div>
            </div>
            </div>
        <div class="card">
            <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
                <div class="col-sm-3 col-md-3 col-lg-3">
                    <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>


            <panel id="LocationSetUp" runat="server">


                <asp:TextBox ID="txtAsstid" autocomplete="off" runat="server" Visible="false"></asp:TextBox>

                <div class="col-sm-12 col-md-12 row ">

                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label7" Text="Location" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlLocation" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnAddLocation" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Location" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditLocation" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Location" CausesValidation="false" />
                    </div>
                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label9" Text="Division" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlDivision" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnDivision" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Division" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditDivision" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Division" CausesValidation="false" />
                    </div>
                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label8" Text="Department" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnAddDepartment" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Department" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditDepartment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Department" CausesValidation="false" />
                    </div>

                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label1" Text="Bay" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlBayi" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnBayi" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Bay" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditBayi" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Bay" CausesValidation="false" />
                    </div>

                    <%--   <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
        </div>--%>
                </div>

                <br />
                <br />

                <div id="Modalheading" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog">
                        <div class="modal-content row">
                            <div class="modal-header">
                                <asp:Label Font-Italic="true" Font-Names="serif pro" Font-Bold="true" Visible="false" ForeColor="#063970" runat="server" ID="lblHeading"></asp:Label>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4>
                                    <asp:Label ID="lblheadingtext" Font-Names="serif pro" Font-Bold="true" ForeColor="#063970" runat="server" CssClass="modal-title"></asp:Label></h4>
                            </div>
                            <div class="modal-body row">
                                <div class="col-sm-12 col-md-12">
                                    <div class="pull-left">
                                        <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 ">
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label ID="lblname" runat="server" Text="* Location Name" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtname" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-5 col-md-5">
                                    <asp:Label ID="lblCode" runat="server" Text="* Location Code" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtCode" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="pull-right">
                                    <asp:Button runat="server" Text="Clear" class="btn-ok" ID="btnClear"></asp:Button>
                                    <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSavedetails" ValidationGroup="ValidateSection"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="ModalLocationSetupValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
                    <div class="modalmsg-dialog">
                        <div class="modalmsg-content">
                            <div class="modalmsg-header">
                                <h4 class="modal-title"><b>TRACe</b></h4>
                            </div>
                            <div class="modalmsg-body">
                                <div id="divMsgType" class="alert alert-info">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblLocationSetupValidationMsg" runat="server"></asp:Label>
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
            </panel>

            <panel id="AssetClassification" runat="server">

                <div class="col-sm-12 col-md-12 ">
                    <h4><b>Asset Classification</b></h4>
                </div>

                <%--   <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
          <div class="col-sm-9 col-md-9 col-lg-9">
                <asp:Label ID="Label2" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            </div>

            <div class="col-sm-3 col-md-3 col-lg-3">
                <asp:Label runat="server" Font-Bold="true" Text="Status : "></asp:Label>
                <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </div>
    </div>--%>

                <%--<div class="col-sm-12 col-md-12 row">
        <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
            <asp:Label runat="server"  Text="* Customer Name"></asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
    </div>--%>

                <asp:TextBox ID="TextBox1" autocomplete="off" runat="server" Visible="false"></asp:TextBox>

                <div class="col-sm-12 col-md-12 row">
                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label4" Text="Heading" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlHeading" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnAddHeadng" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Heading" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditHeadng" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Heading" CausesValidation="false" />
                    </div>
                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label5" Text="Sub Heading Name" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlsubheading" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnAddSubHeadng" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Sub Heading" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditSubHeadng" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Sub Heading" CausesValidation="false" />
                    </div>
                    <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                        <asp:Label ID="Label6" Text="Asset Class Under Sub-Heading" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlItems" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:ImageButton ID="imgbtnItems" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Items" CausesValidation="false" />
                        <asp:ImageButton ID="imgbtnEditItems" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Items" CausesValidation="false" />
                    </div>
                    <div class="col-sm-3 col-md-3 col-lg-3">
                        <asp:Label runat="server" Font-Bold="true" Text="Status : "></asp:Label>
                        <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <br />
                <br />

                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <asp:Panel runat="server" ID="pnlRate" Visible="false">
                        <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblWDV" runat="server" Text="Opening WDV Amount as per IT Act"></asp:Label>
                                <%--     <asp:RegularExpressionValidator ID="RFVWDVAmountITAct" runat="server" ControlToValidate="txtWDVAmountITAct" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtWDVAmountITAct" runat="server" AutoCompleteType="Disabled" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblItRate" runat="server" Text="* Depreciation Rate Per IncomeTax(%)"></asp:Label>
                                <%-- <asp:RegularExpressionValidator ID="RFVIncmTax" runat="server" ControlToValidate="TxtIncmTax" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="TxtIncmTax" runat="server" AutoCompleteType="Disabled" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                            <div class="form-group">
                                <asp:Label ID="lblResidualValue" runat="server" Text="* Residual Value(%)"></asp:Label>
                                <%--  <asp:RegularExpressionValidator ID="RFVResidualValue" runat="server" ControlToValidate="txtResidualValue" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                                <asp:TextBox ID="txtResidualValue" runat="server" Text="5" AutoCompleteType="Disabled" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="form-group  pull-left divmargin col-sm-1 col-md-3 col-lg-1">
                            <asp:ImageButton ID="ImgbtnAdd" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="" CausesValidation="false" />
                        </div>
                    </asp:Panel>
                </div>


                <div id="Modalheadings" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog">
                        <div class="modal-content row">
                            <div class="modal-header">
                                <asp:Label Font-Italic="true" Visible="false" runat="server" ID="lblHeadings"></asp:Label>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <h4>
                                    <asp:Label ID="lblheadingtexts" runat="server" CssClass="modal-title"></asp:Label></h4>
                            </div>
                            <div class="modal-body row">
                                <div class="col-sm-12 col-md-12">
                                    <div class="pull-left">
                                        <asp:Label ID="lblModelErrors" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblids" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6 pull-left">
                                <div class="form-group">
                                    <asp:Label ID="lblnames" runat="server" Text="* Heading Name" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtnames" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectNames" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="pull-right">
                                    <asp:Button runat="server" Text="Clear" class="btn-ok" ID="btnClearClassi"></asp:Button>
                                    <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSaveClass" ValidationGroup="Validateheading"></asp:Button>
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
                                <div id="divMsgTypes" class="alert alert-info">
                                    <p>
                                        <strong>
                                            <asp:Label ID="lblGeneralMasterDetailsValidationMsg" runat="server"></asp:Label>
                                        </strong>
                                    </p>
                                </div>
                            </div>
                            <div class="modalmsg-footer">
                                <button data-dismiss="modal" runat="server" class="btn-ok" id="Button3">
                                    OK
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </panel>
        </div>
    </div>







    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <button type="button" class="close" id="btnClose" data-dismiss="modal">&times;</button>
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModal" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn-ok" />
                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>



