<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UploadObservations.aspx.vb" Inherits="TRACePA.UploadObservations" %>

<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
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

        .multiselect-container {
            width: 100% !important;
        }

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px
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
            $('#<%=ddlSheetName.ClientID%>').select2();
        });
    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Observation Excel Upload</b></h2>
            </div>
            <div class="pull-right col-sm-3 col-md-3">
                <div class="pull-right ">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0;">
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="form-group divmargin"></div>
        <div id="collapseRRIT" class="collapse">
            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                <div class="form-group">
                    <asp:DataGrid ID="dgSampleFormat" runat="server" AutoGenerateColumns="true" Width="100%" class="footable">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
        <div class="divmargin "></div>
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblHCustomerName" runat="server" Text="Customer Name : "></asp:Label>
                    <asp:Label ID="lblCustomerName" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblHFY" Text="Financial Year : " runat="server"></asp:Label>
                    <asp:Label ID="lblFY" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px;">
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblHAuditNo" runat="server" Text="Audit No. : "></asp:Label>
                    <asp:Label ID="lblAuditNo" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="form-group">
                    <asp:Label ID="lblHAuditType" runat="server" Text="Audit Type : "></asp:Label>
                    <asp:Label ID="lblAuditType" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px;">
            <div class="col-sm-12 col-md-12">
                <div class="form-group">
                    <asp:Label ID="lblHAuditProcedure" runat="server" Text="Audit Procedure : "></asp:Label>
                    <asp:Label ID="lblAuditProcedure" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px;">
            <div class="col-sm-5 col-md-5">
                <div class="form-group">
                    <asp:Label ID="lblSelectFile" runat="server" Text="Select a file"></asp:Label>
                    <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
                </div>
                <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
            </div>
            <div class="col-sm-1 col-md-1">
                <div class="form-group">
                    <div style="margin-top: 20px;"></div>
                    <asp:Button ID="btnOk" runat="server" Text="Ok" />
                </div>
            </div>
            <div class="col-sm-6 col-md-6 pull-right" style="padding-right: 0">
                <div class="form-group">
                    <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" Visible="false" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12" style="padding-left: 0; padding-right: 0">
        <div id="div1" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; width: 100%;">
            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                <div id="div2" runat="server" style="overflow-y: auto; width: 100%;">
                    <asp:GridView ID="dgGeneral" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" Width="100%">
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalExcelValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblExcelValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
