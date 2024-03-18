<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmUploadTBExcel.aspx.vb" Inherits="TRACePA.frmUploadTBExcel" %>

<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <style>
        /* .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }*/

        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: white;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>
    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
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

            $('#<%=dgGeneral.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Excel Upload</b></h2>
            </div>
            <div class="pull-right col-sm-3 col-md-3">
                <div class="pull-right ">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="ImgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
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
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                <a href="#">
                    <div id="divcollapseRRIT" runat="server" data-toggle="collapse" data-target="#collapseRRIT"><b><i>Click here to view Sample Format...</i></b></div>
                </a>
            </div>
            <div class="col-sm-6 col-md-6 pull-right">
                <div class="form-group pull-right">
                    <a href="#">
                        <asp:LinkButton ID="lnkDownload" runat="server"><b><i>Click here to Download TRACe TB Format</i></b></asp:LinkButton></a>
                </div>
            </div>
        </div>
        <div class="form-group divmargin"></div>
        <div id="collapseRRIT" class="collapse">
            <div class="col-sm-12 col-md-12" style="padding: 0px; border-style: none; border-color: inherit; border-width: medium; overflow: auto">
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
                    <%--<asp:Label ID="lblHFY" Text="Financial Year" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>--%>
                </div>
            </div>
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                        <asp:RadioButton ID="rboCurrentFY" runat="server" Text="Current Year" GroupName="Year" Checked="True" AutoPostBack="True" />
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:RadioButton ID="rboPreviousFY" runat="server" Text="Previous Year" GroupName="Year" AutoPostBack="True" />
                    </div>
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
                    <asp:GridView ID="dgGeneral" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblDescription" CommandName="EditRow" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Font-Bold="true" Font-Italic="true" Width="150px"></asp:LinkButton>
                                    <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Debit" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpeningDebit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.OpeningDebit") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Opening Credit" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblOpeningCredit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.OpeningCredit") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tr Debit" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrDebit") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tr Credit" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCredit") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Debit" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClosingDebit") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing Credit" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClosingCredit") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
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
