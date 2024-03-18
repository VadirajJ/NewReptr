<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ExcelUpload.aspx.vb" Inherits="TRACePA.ExcelUpload" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link href="css/styles.css" rel="stylesheet" />
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


        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px;
            line-height: 1px
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
            $('#<%=ddlMasterName.ClientID%>').select2();
            $('#<%=ddlSheetName.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Excel Upload" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />

                </div>
            </div>
            </div>
        <div class="card">

            <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0;">

                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                        <a href="#">
                            <div id="divcollapseRRIT" runat="server" data-toggle="collapse" data-target="#collapseRRIT"><b><i>Click here to view Sample Format...</i></b></div>
                        </a>
                    </div>
                    <div class="col-sm-6 col-md-6 pull-right">
                        <div class="form-group pull-right">
                            <asp:LinkButton ID="lnkDownload" runat="server"><b><i>Click here to Download Uploadable Excel</i></b></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="form-group divmargin"></div>
                <div id="collapseRRIT" class="collapse">
                    <div class="col-sm-12 col-md-12" style="padding: 10px;overflow:scroll">
                        <div class="form-group">
                            <asp:DataGrid ID="dgSampleFormat" class="table bs" runat="server" AutoGenerateColumns="true" Width="100%">
                                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"/>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
                <div class="divmargin "></div>
                <div class="col-sm-12 col-md-12" style="padding: 10px">
                    <div class="col-sm-4 col-md-4" style="padding-left: 0;">
                        <div class="form-group">
                            <label>Master Type</label>
                            <asp:DropDownList ID="ddlMasterName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-3 col-md-3">
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
                    <div class="col-sm-4 col-md-4 pull-right" style="padding-right: 0">
                        <div class="form-group">
                            <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name"></asp:Label>
                            <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                    <div class="col-sm-4 col-md-4" style="padding-left: 0;">
                        <asp:Label ID="lblCustName" runat="server" Text="* Customer Name"></asp:Label>
                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-12" style="padding-left: 0; padding-right: 0">
                    <div id="divExcel" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; width: 100%;">
                        <asp:DataGrid CssClass="table bs" ID="dgGeneral" runat="server" AutoGenerateColumns="true" AllowPaging="false" class="footable">
                            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:DataGrid>
                    </div>
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
    <div id="ModalOpeningBalanceValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgOBType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblOpeningBalanceValidation" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" Text="Ok" class="btn-ok" ID="btnOBOk"></asp:Button>
                    <asp:Button runat="server" Text="Close" class="btn-ok" ID="btnClose"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

