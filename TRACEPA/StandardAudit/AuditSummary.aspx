<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AuditSummary.aspx.vb" Inherits="TRACePA.AuditSummary" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" />
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

        input[type=checkbox] {
            vertical-align: middle;
            position: relative;
            bottom: 1px;
        }

        legend {
            margin-bottom: 5px;
            font-size: 14px;
            color: #919191;
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            //$('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlAuditNo.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=lstIFCExcelColumns.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" class="card-header ">
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Audit Summary" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSaveIFC" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                        <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;"/></li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="card">
            <div runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                            <div class="col-sm-5 col-md-5">
                                <div class="form-group">
                                    <asp:Label ID="lblHCustomerName" runat="server" Text="Customer Name"></asp:Label>
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-5 col-md-5">
                                <div class="form-group">
                                    <asp:Label ID="lblHAuditNo" runat="server" Text="* Audit No."></asp:Label>
                                    <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFVAuditNo" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
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
                        <div id="Tabs" role="tabpanel" class="col-sm-12 col-md-12 pull-left">
                            <!-- Nav tabs -->
                            <ul class="nav nav-tabs" role="tablist">
                                <li id="liAuditDetails" runat="server">
                                    <asp:LinkButton ID="lnkbtnAuditDetails" class="nav-link" role="tab" Text="Audit Details" runat="server" Font-Bold="true" />
                                </li>
                                <li id="liIFC" runat="server">
                                    <asp:LinkButton ID="lnkbtnIFC" class="nav-link" role="tab" Text="IFC" runat="server" Font-Bold="true" />
                                </li>
                                <li id="liKAM" runat="server">
                                    <asp:LinkButton ID="lnkbtnKAM" class="nav-link" Text="KAM" runat="server" Font-Bold="true" />
                                </li>
                                <li id="liMR" runat="server">
                                    <asp:LinkButton ID="lnkbtnMR" class="nav-link" Text="MR" runat="server" Font-Bold="true" />
                                </li>
                            </ul>

                            <!-- Tab panes -->
                            <div class="tab-content" style="padding-top: 5px">
                                <div runat="server" role="tabpanel" class="tab-pane fade" id="divAuditDetails">
                                    <div class="col-sm-12 col-md-12 divmargin">
                                        <fieldset>
                                            <asp:LinkButton ID="lnkAuditDetails" runat="server" Text="Audit Details" Style="color: #919191; font-size: 16px; font-weight: bold;" OnClick="lnkAuditDetails_Click"></asp:LinkButton>
                                            <legend></legend>
                                            <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                                                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; padding: 0px;">
                                                    <asp:GridView ID="gvDashboard" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:BoundField DataField="CustomerName" HeaderText="Customer" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="14%" />
                                                            <asp:BoundField DataField="Team" HeaderText="Team" ItemStyle-Width="20%" />
                                                            <asp:BoundField DataField="AuditType" HeaderText="Audit Type" ItemStyle-Width="21%" />
                                                            <asp:BoundField DataField="AuditDate" HeaderText="Audit Date" ItemStyle-Width="10%" />
                                                            <asp:BoundField DataField="AuditStatus" HeaderText="Audit Status" ItemStyle-Width="10%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-sm-12 col-md-12 divmargin">
                                        <fieldset>
                                            <asp:LinkButton ID="lnkDRL" runat="server" Text="Document Request Log Summary" Style="color: #919191; font-size: 16px; font-weight: bold;" OnClick="lnkDRL_Click"></asp:LinkButton>
                                            <legend></legend>
                                            <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                                                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; padding: 0px;">
                                                    <asp:GridView ID="gvDRLSummary" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:BoundField DataField="TotalCheckpoints" HeaderText="Total No. of Checkpoints (Including Others)" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="DocumentsRequested" HeaderText="Documents requested" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="Received" HeaderText="Received against the Checkpoints" ItemStyle-Width="50%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-sm-12 col-md-12 divmargin">
                                        <fieldset>
                                            <asp:LinkButton ID="lnkObservationQuerySummarys" runat="server" Text="Observations / Query Summary" Style="color: #919191; font-size: 16px; font-weight: bold;" OnClick="lnkObservationQuerySummarys_Click"></asp:LinkButton>
                                            <legend></legend>
                                            <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                                                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; padding: 0px;">
                                                    <asp:GridView ID="gvObservationsQuerySummary" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:BoundField DataField="TotalLedger" HeaderText="No. of Ledger" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="TotalObservations" HeaderText="No. of observations" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="TotalIssueRaised" HeaderText="No. of Issue Raised" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="ClientResponse" HeaderText="Response from client" ItemStyle-Width="25%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <div class="col-sm-12 col-md-12 divmargin">
                                        <fieldset>
                                            <asp:LinkButton ID="lnkCheckpointSummary" runat="server" Text="Checkpoint Summary" Style="color: #919191; font-size: 16px; font-weight: bold;" OnClick="lnkCheckpointSummary_Click"></asp:LinkButton>
                                            <legend></legend>
                                            <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                                                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; padding: 0px;">
                                                    <asp:GridView ID="gvCheckpointSummary" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                        <Columns>
                                                            <asp:BoundField DataField="TotalCheckpoints" HeaderText="Total No. of Checkpoints" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="TotalObservations" HeaderText="Total No. of Observations" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="Mandatory" HeaderText="Mandatory" ItemStyle-Width="25%" />
                                                            <asp:BoundField DataField="Tested" HeaderText="Tested" ItemStyle-Width="25%" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <div runat="server" role="tabpanel" class="tab-pane fade" id="divIFC">
                                    <div class="col-sm-12 col-md-12" style="padding: 0px;">
                                        <div class="col-sm-6 col-md-6" style="padding: 0px;">
                                            <div class="col-sm-6 col-md-6">
                                                <div class="form-group">
                                                    <label>* Report Date</label>
                                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFCReportDate" runat="server" ControlToValidate="txtIFCReportDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFCReportDate" runat="server" ControlToValidate="txtIFCReportDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                                    <cc1:CalendarExtender ID="cclExpectedCompletionDate" runat="server" PopupButtonID="txtIFCReportDate" PopupPosition="TopRight" TargetControlID="txtIFCReportDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </cc1:CalendarExtender>
                                                    <asp:TextBox ID="txtIFCReportDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-md-6">
                                                <div class="form-group">
                                                    <label>* Report By</label>
                                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFCReportBy" runat="server" SetFocusOnError="True" ControlToValidate="txtIFCReportBy" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFCReportBy" runat="server" ControlToValidate="txtIFCReportBy" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                                    <asp:TextBox ID="txtIFCReportBy" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-5 col-md-5">
                                                <div class="form-group">
                                                    <asp:Label ID="lblSelectFile" runat="server" Text="Select a file"></asp:Label>
                                                    <asp:FileUpload ID="FUIFCLoad" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
                                                </div>
                                                <asp:TextBox ID="txtIFCPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
                                            </div>
                                            <div class="col-sm-1 col-md-1">
                                                <div class="form-group">
                                                    <div style="margin-top: 20px;"></div>
                                                    <asp:Button ID="btnOk" runat="server" Text="Ok" />
                                                </div>
                                            </div>
                                            <div class="col-sm-6 col-md-6 pull-right">
                                                <div class="form-group">
                                                    <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name" Visible="false"></asp:Label>
                                                    <asp:DropDownList ID="ddlIFCSheetName" runat="server" AutoPostBack="true" Visible="false" CssClass="aspxcontrols">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-6">
                                            <div class="form-group">
                                                <label>* Comments</label>
                                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFCComments" runat="server" SetFocusOnError="True" ControlToValidate="txtIFCComments" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFCComments" runat="server" ControlToValidate="txtIFCComments" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="txtIFCComments" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="80px"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                                        <div class="col-sm-5 col-md-5">
                                            <div class="form-group">
                                                <asp:Label ID="lblHIFCExcelColumns" runat="server" Text="Select up to 6 columns from selected excel sheet" Visible="false"></asp:Label>
                                                <asp:ListBox ID="lstIFCExcelColumns" runat="server" Width="100%" Font-Size="10px" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-7 col-md-7">
                                            <div class="form-group">
                                                <div style="margin-top: 20px;"></div>
                                                <asp:Button ID="btnOkIFCExcelColumns" runat="server" Text="Load" />
                                                <asp:Button ID="btnConfirmIFCExcelColumns" runat="server" Text="Confirm & Save" />
                                                <asp:Label ID="lblIFCNote" runat="server" Font-Bold="true" Text="Note : Only the first 50 rows will be considered in the selected Excel sheet."></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                                        <asp:GridView ID="dgExcelIFC" AllowSorting="true" ShowHeader="true" Class="table table-bordered table-striped fs--1 mb-0" runat="server" AutoGenerateColumns="true" Width="100%">
                                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        </asp:GridView>
                                        <%--<asp:GridView ID="dgExcelIFCDetails" AllowSorting="true" ShowHeader="true" Class="table table-bordered table-striped fs--1 mb-0" runat="server" AutoGenerateColumns="true" Width="100%">
                                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnComments" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Addnote16.png" runat="server" data-toggle="tooltip" CommandName="Comments" title="Add Testing Details" Style="padding-right: 0px;" />                                                        
                                                        <asp:Label ID="lblDBpkId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DBpkId") %>'></asp:Label>
                                                        <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>                                                        
                                                        <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment16.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
                                        <asp:GridView ID="dgExcelIFCDetails" AllowSorting="true" ShowHeader="true" Class="table table-bordered table-striped fs--1 mb-0" runat="server" AutoGenerateColumns="false" Width="100%">
                                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="SrNo" HeaderText="Sr No." ItemStyle-Width="5%" />
                                                <asp:BoundField DataField="SAIFCD_Column1" HeaderText="SAIFCD_Column1" />
                                                <asp:BoundField DataField="SAIFCD_Column2" HeaderText="SAIFCD_Column2" />
                                                <asp:BoundField DataField="SAIFCD_Column3" HeaderText="SAIFCD_Column3" />
                                                <asp:BoundField DataField="SAIFCD_Column4" HeaderText="SAIFCD_Column4" />
                                                <asp:BoundField DataField="SAIFCD_Column5" HeaderText="SAIFCD_Column5" />
                                                <asp:BoundField DataField="SAIFCD_Column6" HeaderText="SAIFCD_Column6" />
                                                <asp:BoundField DataField="TestingDetails" HeaderText="Testing Details" />
                                                <asp:BoundField DataField="Conclusion" HeaderText="Conclusion" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnComments" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Addnote16.png" runat="server" data-toggle="tooltip" CommandName="Comments" title="Add Testing Details" Style="padding-right: 0px;" />
                                                        <asp:Label ID="lblDBpkId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DBpkId") %>'></asp:Label>
                                                        <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                                                        <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment16.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div runat="server" role="tabpanel" class="tab-pane fade" id="divKAM">
                                    <br />
                                    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                                        <asp:GridView ID="dgKAMdetails" Class="table table-bordered table-striped fs--1 mb-0" runat="server" AutoGenerateColumns="false" Width="100%">
                                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="SrNo" HeaderText="Sr No." ItemStyle-Width="5%" />
                                                <asp:BoundField DataField="Source" HeaderText="Source" />
                                                <asp:BoundField DataField="KAM" HeaderText="Key Audit Matters" />
                                                <asp:BoundField DataField="DescriptionOrReasonForSelectionAsKAM" HeaderText="Description or Reason for selection as KAM" />
                                                <asp:BoundField DataField="AuditProcedureUndertakenToAddressTheKAM" HeaderText="Audit Procedure undertaken to address the KAM" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnComments" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Addnote16.png" runat="server" data-toggle="tooltip" CommandName="Comments" title="Add KAM Details" Style="padding-right: 0px;" />
                                                        <asp:Label ID="lblDBpkId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DBpkId") %>'></asp:Label>
                                                        <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                                                        <asp:Label ID="lblIFCDpkId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.IFCDpkId") %>'></asp:Label>
                                                        <asp:Label ID="lblKAM" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.KAM") %>'></asp:Label>
                                                        <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment16.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div runat="server" role="tabpanel" class="tab-pane fade" id="divMR">
                                    <br />
                                    <asp:Button runat="server" class="btn-ok" ID="btnNewMRR" Text="Add MR Details" OnClick="btnNewMRR_Click"></asp:Button>
                                    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                                        <br />
                                        <asp:GridView ID="dgMRdetails" Class="table table-bordered table-striped fs--1 mb-0" runat="server" AutoGenerateColumns="false" Width="100%">
                                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="SrNo" HeaderText="Sr No." ItemStyle-Width="5%" />
                                                <asp:BoundField DataField="Heading" HeaderText="MR Heading" />
                                                <asp:BoundField DataField="Description" HeaderText="MR Description" />
                                                <asp:BoundField DataField="RequestedDat" HeaderText="Requested Date" />
                                                <asp:BoundField DataField="RequestedByPerson" HeaderText="Requested by Person" />
                                                <asp:BoundField DataField="DueDateReceiveDocs" HeaderText="Due Date to Receive the Docs" />
                                                <asp:BoundField DataField="ResponsesReceivedDate" HeaderText="Responses Received Date" />
                                                <asp:BoundField DataField="ResponsesDetails" HeaderText="Details" />
                                                <asp:BoundField DataField="ResponsesRemarks" HeaderText="Remarks" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnComments" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Addnote16.png" runat="server" data-toggle="tooltip" CommandName="Comments" title="Add MR Details" Style="padding-right: 0px;" />
                                                        <asp:Label ID="lblDBpkId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DBpkId") %>'></asp:Label>
                                                        <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                                                        <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment16.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
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
    <div id="myIFCObservationModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;">
                            <asp:Label ID="lblIFCHeading" runat="server" Font-Bold="true" Text="Testing Details"></asp:Label></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12" style="padding: 0px;">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <label>*Date Of Testing</label>
                                <asp:TextBox ID="txtIFCDateOfTesting" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFCDateOfTesting" runat="server" ControlToValidate="txtIFCDateOfTesting" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateIFC"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFCDateOfTesting" runat="server" ControlToValidate="txtIFCDateOfTesting" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateIFC"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtIFCDateOfTesting" PopupPosition="TopRight" TargetControlID="txtIFCDateOfTesting" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <label>* Sample Size Used</label>
                                <asp:TextBox ID="txtIFCSampleSizeUsed" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFCSampleSizeUsed" runat="server" SetFocusOnError="True" ControlToValidate="txtIFCSampleSizeUsed" Display="Dynamic" ValidationGroup="ValidateIFC"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFCSampleSizeUsed" runat="server" ControlToValidate="txtIFCSampleSizeUsed" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateIFC"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblIFCConclusion" runat="server" Text="* Conclusion"></asp:Label>
                                <asp:DropDownList ID="ddlIFCConclusion" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVIFCConclusion" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlIFCConclusion" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateIFC"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <label>* Testing Details</label>
                            <asp:TextBox ID="txtIFCTestingDetails" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIFCTestingDetails" runat="server" SetFocusOnError="True" ControlToValidate="txtIFCTestingDetails" Display="Dynamic" ValidationGroup="ValidateIFC"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVIFCTestingDetails" runat="server" ControlToValidate="txtIFCTestingDetails" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateIFC"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" class="btn-ok" ID="btnSaveIFCObservationsComments" Text="Save" ValidationGroup="ValidateIFC" OnClick="btnSaveIFCObservationsComments_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="myKAMObservationModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;">
                            <asp:Label ID="lblKAMHeading" runat="server" Font-Bold="true" Text="Key Audit Matters Details"></asp:Label></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblHKAM" runat="server" Text="KAM : "></asp:Label>
                            <asp:Label ID="lblKAMDesc" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <label>* Description or Reason for selection as KAM</label>
                            <asp:TextBox ID="txtDescriptionOrReasonForSelectionAsKAM" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescriptionOrReasonForSelectionAsKAM" runat="server" SetFocusOnError="True" ControlToValidate="txtDescriptionOrReasonForSelectionAsKAM" Display="Dynamic" ValidationGroup="ValidateKAMC"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescriptionOrReasonForSelectionAsKAM" runat="server" ControlToValidate="txtDescriptionOrReasonForSelectionAsKAM" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateKAMC"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <label>* Audit Procedure undertaken to address the KAM</label>
                            <asp:TextBox ID="txtAuditProcedureUndertakenToAddressTheKAM" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="80px"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAuditProcedureUndertakenToAddressTheKAM" runat="server" SetFocusOnError="True" ControlToValidate="txtAuditProcedureUndertakenToAddressTheKAM" Display="Dynamic" ValidationGroup="ValidateKAMC"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAuditProcedureUndertakenToAddressTheKAM" runat="server" ControlToValidate="txtAuditProcedureUndertakenToAddressTheKAM" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateKAMC"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" class="btn-ok" ID="btnSaveKAMObservationsComments" Text="Save" ValidationGroup="ValidateKAMC" OnClick="btnSaveKAMObservationsComments_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalAuditSummaryValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAuditSummaryValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="btnModalOk">
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
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myMRRModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;">
                            <asp:Label ID="lblHMRR" runat="server" Font-Bold="true" Text="Management Representations Details"></asp:Label></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-10 col-md-10">
                            <div class="form-group">
                                <asp:Label ID="lblHManagementRepresentations" runat="server" Text="* Management Representations"></asp:Label>
                                <asp:DropDownList ID="ddlManagementRepresentations" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVManagementRepresentations" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlManagementRepresentations" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRRequest"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:CheckBox ID="chkSendMail" Text="Send Mail" runat="server"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblHMRRHeading" runat="server" Text="Management Representations Heading : "></asp:Label>
                            <asp:Label ID="lblMRRHeading" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblHMRRDescription" runat="server" Text="Management Representations Description : "></asp:Label>
                            <asp:Label ID="lblMRRDescription" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHMRRequestedDate" runat="server" Text="Requested Date : "></asp:Label>
                                <asp:Label ID="lblMRRequestedDate" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblHMRRequestedByPerson" runat="server" Text="Requested by Person : "></asp:Label>
                                <asp:Label ID="lblMRRequestedByPerson" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHEmailID" runat="server" Text="* Email ID (Multiple Emails with semicolon[;])"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmail" runat="server" ControlToValidate="txtEmailID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRRequest"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtEmailID" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmailID" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRRequest"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <label>* Due Date to Receive the Docs</label>
                                <asp:TextBox ID="txtMRDueDateReceiveDocs" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMRDueDateReceiveDocs" runat="server" ControlToValidate="txtMRDueDateReceiveDocs" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRRequest"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMRDueDateReceiveDocs" runat="server" ControlToValidate="txtMRDueDateReceiveDocs" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRRequest"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtMRDueDateReceiveDocs" PopupPosition="TopRight" TargetControlID="txtMRDueDateReceiveDocs" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblHResponsesReceivedDate" runat="server" Text="* Responses Received Date"></asp:Label>
                                <asp:TextBox ID="txtResponsesReceivedDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVResponsesReceivedDate" runat="server" ControlToValidate="txtResponsesReceivedDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRResponses"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFEResponsesReceivedDate" runat="server" ControlToValidate="txtResponsesReceivedDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRResponses"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtResponsesReceivedDate" PopupPosition="TopRight" TargetControlID="txtResponsesReceivedDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHMRRRemarks" runat="server" Text="* Requested Remarks"></asp:Label>
                                <asp:TextBox ID="txtMRRRemarks" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="85px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVMRRRemarks" runat="server" SetFocusOnError="True" ControlToValidate="txtMRRRemarks" Display="Dynamic" ValidationGroup="ValidateMRRequest"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVMRRRemarks" runat="server" ControlToValidate="txtMRRRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRRequest"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHResponsesDetails" runat="server" Text="* Responses Details"></asp:Label>
                                <asp:TextBox ID="txtResponsesDetails" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="85px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVResponsesDetails" runat="server" SetFocusOnError="True" ControlToValidate="txtResponsesDetails" Display="Dynamic" ValidationGroup="ValidateMRResponses"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVResponsesDetails" runat="server" ControlToValidate="txtResponsesDetails" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRResponses"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHResponsesRemarks" runat="server" Text="* Responses Remarks"></asp:Label>
                                <asp:TextBox ID="txtResponsesRemarks" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="85px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVResponsesRemarks" runat="server" SetFocusOnError="True" ControlToValidate="txtResponsesRemarks" Display="Dynamic" ValidationGroup="ValidateMRResponses"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVResponsesRemarks" runat="server" ControlToValidate="txtResponsesRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateMRResponses"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" class="btn-ok" ID="btnSaveMRRequest" Text="Save" ValidationGroup="ValidateMRRequest" OnClick="btnSaveMRRequest_Click"></asp:Button>
                    <asp:Button runat="server" class="btn-ok" ID="btnSaveMRResponses" Text="Save" ValidationGroup="ValidateMRResponses" OnClick="btnSaveMRResponses_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
