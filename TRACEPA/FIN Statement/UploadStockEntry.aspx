<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UploadStockEntry.aspx.vb" Inherits="TRACePA.UploadStockEntry" %>

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
    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
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
        <%--$(document).ready(function () {
            $('#<%=gvddlSubitem.ClientID%>').select2();
            $('#<%=gvddlitem.ClientID%>').select2();
            $('#<%=gvddlSubheading.ClientID%>').select2();
            $('#<%=gvddlheading.ClientID%>').select2();
        });--%>
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Stock Entry</b></h2>
            </div>
            <div class="pull-right col-sm-3 col-md-3">
                <div class="pull-right ">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />
                    <asp:ImageButton ID="ImgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgLinkageForYear" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Copy the Linkage for this Year" />
                </div>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
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
                        <asp:LinkButton ID="lnkDownload" runat="server"><b><i>Click here to Download Uploadable Excel</i></b></asp:LinkButton></a>
                </div>
            </div>
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
            <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblCustName" runat="server" Text="* Customer Name"></asp:Label>
                            <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="true" onchange="showProgress()"></asp:DropDownList>
                            <asp:Label ID="lblMsg" runat="server" />
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlCustName" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div class="overlay">
                        <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="/Images/pageloader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
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
                    <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" Visible="false" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-12" style="padding-left: 0; padding-right: 0">
            <asp:Label CssClass="divmargin" runat="server" ID="Label1" Text="Total:" Font-Bold="true" Font-Size="Large"></asp:Label>
            <asp:Label runat="server" ID="lblTotal"></asp:Label>
            <div id="div1" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; width: 100%;">
                <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                    <div id="div2" runat="server" style="overflow-y: auto; width: 100%;">
                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="dgGeneral" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />   
                                <columns>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                            <itemtemplate>
                                                <asp:UpdatePanel runat="server" ID="UpdatePanel_1">
                                                    <contenttemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True"
                                                            OnCheckedChanged="chkSelect_CheckedChanged" />
                                                    </contenttemplate>
                                                    <triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="chkSelect" />
                                                    </triggers>
                                                </asp:UpdatePanel>
                                                <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                                            </itemtemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10px" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
                                            <itemtemplate>
                                                <asp:Label ID="Description" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                            </itemtemplate>
                                            <headerstyle font-bold="True" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" />
                                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Classification" ItemStyle-Width="10%">
                                            <itemtemplate>
                                                <asp:Label ID="Itemclassification" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Itemclassification") %>'></asp:Label>
                                            </itemtemplate>
                                            <headerstyle font-bold="True" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" />
                                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit price" ItemStyle-Width="10%">
                                            <itemtemplate>
                                                <asp:Label ID="UP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UP") %>'></asp:Label>
                                            </itemtemplate>
                                            <headerstyle font-bold="True" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" />
                                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="8%">
                                            <itemtemplate>
                                                <asp:Label ID="Quantity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>'></asp:Label>
                                            </itemtemplate>
                                            <headerstyle font-bold="True" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" />
                                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM" ItemStyle-Width="10%">
                                            <itemtemplate>
                                                <asp:Label ID="UOM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UOM") %>'></asp:Label>
                                            </itemtemplate>
                                            <headerstyle font-bold="True" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" />
                                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" wrap="true" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="5px">
                                            <itemtemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>' Width="20px"></asp:Label>
                                            </itemtemplate>
                                            <headerstyle font-bold="True" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" />
                                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False" font-underline="False" horizontalalign="Left" verticalalign="Top" wrap="true" />
                                        </asp:TemplateField>
                                    </columns>
                                    <emptydatatemplate>No Records Available</emptydatatemplate>
                                </asp:GridView>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <div id="ModalJEItems" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>JE transactions for Finalisation of Accounts</b></h4>
                </div>
                <div class="modalmsg-body">
                    <asp:GridView ID="gvJeitems" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="footable" ShowHeaderWhenEmpty="true">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Itemid") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField Visible="false" HeaderText="SrNo" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:BoundField HeaderText="Credit" DataField="Credit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:BoundField HeaderText="Debit" DataField="Debit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:BoundField HeaderText="Transaction Type" DataField="TrType" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            <asp:BoundField HeaderText="Transaction Date" DataField="TransactionDate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                    </asp:GridView>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button2">
                        OK
                    </button>
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
