<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="frmAuditLedgerUpload.aspx.vb" Inherits="TRACePA.frmAuditLedgerUpload" %>

<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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

        .multiselect-container {
            width: 100% !important;
        }

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px
        }

        input[type=radio] {
            vertical-align: middle;
            position: relative;
            bottom: 1px;
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
           <%-- $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlSheetName.ClientID%>').select2();--%>
        });
    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            <%--$('#<%=dgGeneral.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });--%>
        });
        <%--$(document).ready(function () {
            $('#<%=gvddlSubitem.ClientID%>').select2();
            $('#<%=gvddlitem.ClientID%>').select2();
            $('#<%=gvddlSubheading.ClientID%>').select2();
            $('#<%=gvddlheading.ClientID%>').select2();
        });--%>
    </script>
    <div class="col-sm-12 col-md-12" style="word-break: break-all">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Trial Balance Upload and Review" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="ImgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnExcelUpload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Excel Upload" ValidationGroup="Validate" Style="width: 30px; height: 25px;" />
                    <%--<asp:ImageButton ID="imgLinkageForYear" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Copy the Linkage for this Year" />--%>
                    <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                        <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" ValidationGroup="Validate" /></li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" ValidationGroup="Validate" /></li>
                    </ul>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" Visible="false" />
                </div>
            </div>
        </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div3" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0;">
                            <div class="divmargin "></div>
                            <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblHCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblHFY" Text="Financial Year" runat="server"></asp:Label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblHAuditNo" runat="server" Text="* Audit No."></asp:Label>
                                        <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RFVAuditNo" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <br />
                                        <asp:Label ID="lblHAuditType" runat="server" Text="Audit Type : "></asp:Label>
                                        <asp:Label ID="lblAuditType" runat="server" Font-Bold="true"></asp:Label>
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
                                                <asp:TemplateField HeaderText="Ledger" HeaderStyle-Width="100%">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Width="250px"></asp:Label>--%>
                                                        <asp:Label ID="lblDescription" CommandName="EditRow" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Font-Bold="true" Font-Italic="true" Width="200px"></asp:Label>
                                                        <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                                                        <asp:Label ID="lblObservationCount" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ObservationCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" PY Op.Debit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblProcessID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ProcessID") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblPYOpeningDebit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.PYOpeningDebit") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PY Op.Credit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPYOpeningCredit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.PYOpeningCredit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PY Tr.Debit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPYTrDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYTrDebit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PY Tr.Credit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPYTrCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYTrCredit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PY Cl.Debit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPYClosingDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYClosingDebit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PY Cl.Credit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPYClosingCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYClosingCredit") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" CY Op.Debit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <%--<asp:Label ID="lblProcessID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ProcessID") %>'></asp:Label>--%>
                                                        <asp:Label ID="lblCYOpeningDebit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.CYOpeningDebit") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CY Op.Credit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCYOpeningCredit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.CYOpeningCredit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CY Tr.Debit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCYTrDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYTrDebit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CY Tr.Credit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCYTrCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYTrCredit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CY Cl.Debit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCYClosingDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYClosingDebit") %>' Width="50px"></asp:Label>
                                                        <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CY Cl.Credit" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCYClosingCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYClosingCredit") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Varience" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVarience" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Varience") %>' Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnComments" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Addnote16.png" runat="server" data-toggle="tooltip" CommandName="Comments" title="Comments" Style="padding-right: 0px;" />
                                                        <%--<asp:LinkButton ID="lnkbtnObservations" runat="server" Style="padding-right: 15px;" Font-Underline="false" Font-Bold="true" Font-Size="Large" Text='O' CommandName="Observations" data-toggle="tooltip" title="Observations"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnReviewerObservations" runat="server" Font-Underline="false" Style="padding-right: 15px;" Font-Bold="true" Font-Size="Large" Text='RO' CommandName="ReviewerObservations" data-toggle="tooltip" title="Reviewer Observations"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnClientComments" runat="server" Font-Underline="false" Style="padding-right: 15px;" Font-Bold="true" Font-Size="Large" Text='CC' CommandName="ClientComments" data-toggle="tooltip" title="Client Comments"></asp:LinkButton>--%>
                                                        <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                                                        <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment16.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
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
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myObservationModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;">
                            <asp:Label ID="lblModelHeading" runat="server" Font-Bold="true"></asp:Label></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblObservationError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                            <asp:RadioButton ID="rboAuditor" runat="server" Text="Auditor" GroupName="Observation" Checked="True" AutoPostBack="True" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:RadioButton ID="rboReviewer" runat="server" Text="Reviewer" GroupName="Observation" AutoPostBack="True" />
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <asp:RadioButton ID="rboClient" runat="server" Text="Client" GroupName="Observation" AutoPostBack="True" />
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblHLedger" runat="server" Text="Ledger : "></asp:Label>
                            <asp:Label ID="lblLedger" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblLedgerId" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="lblOCId" runat="server" Visible="false"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblHObservationsComments" runat="server"></asp:Label>
                            <asp:TextBox ID="txtObservationsComments" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVObservationsComments" runat="server" SetFocusOnError="True" ControlToValidate="txtObservationsComments" Display="Dynamic" ValidationGroup="ValidateOC"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVObservationsComments" runat="server" ControlToValidate="txtObservationsComments" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateOC"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="form-group">
                            <div class="col-sm-12 col-md-12">
                                <div id="divOC" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                                    <asp:GridView ID="gvObservations" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <Columns>
                                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="14%" />
                                            <asp:BoundField DataField="Role" HeaderText="Role" ItemStyle-Width="10%" />
                                            <asp:BoundField DataField="Observations" HeaderText="Observations" ItemStyle-Width="33%" />
                                            <asp:BoundField DataField="ClientComments" HeaderText="Client Comments" ItemStyle-Width="33%" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" class="btn-ok" ID="btnSendIssuetoClient" Text="Raise a query" ValidationGroup="ValidateOC" OnClick="btnSendIssuetoClient_Click"></asp:Button>
                    <asp:Button runat="server" class="btn-ok" ID="btnSaveObservationsComments" Text="Save" ValidationGroup="ValidateOC" OnClick="btnSaveObservationsComments_Click"></asp:Button>
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
    <div id="myModalMainAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;"><b>Attachment</b></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-7 col-md-7" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:DataGrid ID="dgMainAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="table table-bordered" OnRowDataBound="PickColor_RowDataBound">
                                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                                <Columns>
                                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="File Name">
                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Width="40%"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                            <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Description">
                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Width="28%"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Created">
                                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" Width="23%"></HeaderStyle>
                                        <ItemTemplate>
                                            <b>By : </b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On : </b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" /><br />
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                            <iframe runat="server" id="iframeview" name="iframe_a" height="500px" width="100%" title="Iframe Example"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
