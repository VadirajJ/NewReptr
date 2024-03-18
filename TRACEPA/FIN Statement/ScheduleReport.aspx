<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ScheduleReport.aspx.vb" Inherits="TRACePA.ScheduleReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
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

        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript">
                
    </script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
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
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=dgGeneral.ClientID%>').DataTable({
                iDisplayLength: 10,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstbranchSchedTemp.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
buttonWidth: '100%'
            });
        });
    $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstPartsandDirectors.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
buttonWidth: '100%'
            });
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstSubHeadings.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });
        });
 $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlRepType.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=DdlScheduletype.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCompanyName.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlPartners.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustomerName.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstItems.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label9" CssClass="form-label" Font-Bold="true" Text="Schedule Template Report" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:Button ID="btnAccountPolicies" CssClass="btn-ok" Text="Accounting Policies" runat="server"></asp:Button>                    
                     <a href="#" data-toggle="dropdown" runat="server"><span>
                         <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Final Schedule Report" /></span></a>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                    </ul>
                    <asp:ImageButton ID="imgbtnAssign" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Assign" Visible="false" />
                </div>
            </div>
        </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                            <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="Label3" Text="Report Type" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlRepType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select Report type</asp:ListItem>
                                        <asp:ListItem Value="1">Summary Report</asp:ListItem>
                                        <asp:ListItem Value="2">Detailed Report</asp:ListItem>
                                        <asp:ListItem Value="3">Cash Flow</asp:ListItem>
                                        <asp:ListItem Value="4">Export Opening Balance</asp:ListItem>
                                        <asp:ListItem Value="5">Accounting Policies</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LblBranchNameSchedTemp" runat="server" Text="* Branch Name" Width="100%"></asp:Label>
                                    <asp:ListBox ID="lstbranchSchedTemp" runat="server" CssClass="aspxcontrols" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                                
                            </div>
                            
                       
                            <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="Label1" Text="Schedule Type" runat="server"></asp:Label>
                                    <asp:DropDownList ID="DdlScheduletype" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                        <asp:ListItem Value="0">Select Scheduler type</asp:ListItem>
                                        <asp:ListItem Value="3">P & L </asp:ListItem>
                                        <asp:ListItem Value="4">Balance Sheet</asp:ListItem>
                                        <asp:ListItem Value="5">Closing Stock Inventory</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            
                                  <div class="form-group">
                                    <asp:Label ID="lblHExistingCompanyName" runat="server" Text="Company Name"></asp:Label>
                                    <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                </div>
                                       <div class="form-group">
                                    <asp:Label ID="Label4" Text="Partners*" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlPartners" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                    </asp:DropDownList>
                                </div>
                            </div>
                                
                                  
                            <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="Label2" Text="Company Name" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                    </asp:DropDownList>
                                </div>
                                    <div class="form-group">
                                    <asp:Label ID="lblUDIN" Text="Enter UDIN No" runat="server"></asp:Label><br />
                                    <asp:TextBox runat="server" ID="txtUDINNo" Text="" CssClass="aspxcontrols" />
                                </div>
                                   <div class="form-group">
                                    <asp:Label ID="lblparanddir" runat="server" Visible="false" Width="100%"></asp:Label>
                                    <asp:ListBox ID="lstPartsandDirectors" runat="server" Visible="false" CssClass="aspxcontrols" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                                </div>

                            </div>
                        
                            <div class="col-sm-3 col-md-3" style="padding-left: 0px;">
                                <div class="form-group">
                                    <div class="form-group">
                                        <asp:Label ID="lblYears" runat="server" Text="Financial Year"></asp:Label>
                                        <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                              
                                  
                                  <div class="form-group">
                                    <asp:Label ID="Label6" Text="Enter UDIN Date" runat="server"></asp:Label><br />
                                    <asp:TextBox runat="server" ID="txtUDINDate" Text="" CssClass="aspxcontrols" />
                                    <cc1:CalendarExtender ID="cclUDINDate" runat="server" PopupButtonID="txtUDINDate" PopupPosition="BottomLeft"
                                        TargetControlID="txtUDINDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                <div class="col-sm-6 col-md-6 col-lg-6" style="padding:0;">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" Text="* Sub Headings" Width="100%"></asp:Label><br />
                                    <asp:ListBox ID="lstSubHeadings" runat="server" CssClass="aspxcontrols" Width="80%" SelectionMode="Multiple"></asp:ListBox>
                                </div>

                            </div>

                             <div class="col-sm-6 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <asp:Label ID="Label8" runat="server" Text="* Item" Width="100%"></asp:Label><br />
                                    <asp:ListBox ID="lstItems" runat="server" CssClass="aspxcontrols" Width="60%" SelectionMode="Multiple"></asp:ListBox>
                                </div>
                            </div> 
                            </div>
                        </div>
                                 
                            <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                           <div class=" col-sm-4 col-md-3 col-lg-3" style="padding:0;">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnFinalAuditReport" ForeColor="Black" Text="Final Audit Report" CssClass="btn-ok" Visible="false" />
                                    <asp:Button runat="server" ID="btnLoad" BorderColor="Gray" Width="70px" Text="Load" CssClass="btn-ok"/>
                                    <asp:Button runat="server" ID="btnArchive" BorderColor="Gray" Width="70px" Text="Archive" CssClass="btn-ok" />
                                    <asp:Button runat="server" ID="btnFreeze" BorderColor="Gray" Visible="false" Text="Freeze" CssClass="btn-ok" />
                                </div>
                                    </div>
<div runat="server" class="col-sm-6 col-md-6" style="padding-left: 0;">&nbsp;
                                <div runat="server" id="divNotDesc" visible="false">
                                    <label class="switch">
                                        <asp:CheckBox ID="chkOnOff" AutoPostBack="true" runat="server" Checked="false" />
                                        <span class="slider round"></span>
                                    </label>
                                    <asp:Label runat="server" Text=" Note Description"></asp:Label>&nbsp;&nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
                                 <label class="switch " runat="server">
                                <asp:CheckBox ID="chkBxExcel" runat="server" Checked="false" />
                                <span class="slider round"></span>
                            </label>
                            <asp:Label runat="server" Text="Disable to get the GL having '0.00' amount"></asp:Label>
                            </div>
    </div>
                             
                            <div class="pull-left divmargin col-sm-3 col-md-3 col-lg-3" style="display:none;">
                                <asp:Label ID="Label5" Text="Amount in" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlAmountConvert" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="In Hundreds" Value="100"></asp:ListItem>
                                    <asp:ListItem Text="In Thousands" Value="1000"></asp:ListItem>
                                    <asp:ListItem Text="In Lakhs" Value="100000"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            
  
                        </div>
                        <div class="col-sm-6 col-md-6" id="divAcountpolicies" runat="server" visible="false" style="padding-left: 0px">
                            <div class="form-group">
                                <asp:Label ID="Label11" Text="Description" runat="server"></asp:Label><br />
                                <asp:TextBox TextMode="MultiLine" Height="100px" runat="server" ID="txtAccountpolicies" CssClass="aspxcontrols" />
                            </div>
                        </div>
                        <div class="col-sm-8 col-md-8 form-group" style="padding: 0px;">
                            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="650px" Width="99%" PageCountMode="Actual"></rsweb:ReportViewer>
                            
                        </div>
                        <asp:Panel runat="server" ID="pnlScheduleNote" Visible="false">
                            <div class="form-group pull-left">
                                <a href="#">
                                    <asp:LinkButton ID="lnkDownloadNoteDesc" ForeColor="Blue" runat="server"><b><i><u>Click here to View Schedule Note</u></i></b></asp:LinkButton></a><br />
                                <asp:LinkButton ID="lnkScheduleNotedownload" ForeColor="Blue" runat="server"><b><i><u>Click here to download Schedule Note</u></i></b></asp:LinkButton></a>              
                            </div>
                        </asp:Panel>

                        <div class="col-sm-4 col-md-4 form-group" style="margin: 0px; padding: 0px">
                            <asp:Label runat="server" ID="lblUnmappedData" Visible="false" Font-Bold="true" Text="Unmapped Descriptions"></asp:Label>
                            <asp:GridView ID="dgGeneral" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Width="250px"></asp:Label>--%>
                                            <asp:LinkButton ID="lnkDescription" CommandName="Navigate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Font-Bold="true" Font-Italic="true" Width="150px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <%--                    <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>--%>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                            <asp:Panel runat="server" ID="pnlPnLAmt" Visible="false">
                                <div class="form-group pull-left">
                                    <asp:Label runat="server" ID="lblPnltxt" ForeColor="Blue" Font-Bold="true" Text="P&L Amount :"></asp:Label>
                                    <asp:Label runat="server" ID="lblPnlamt" ForeColor="Blue" Font-Bold="true" Text=""></asp:Label>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                <asp:Label ID="Label10" Text="Sub items under items" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlSUbItems" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
                <asp:ImageButton ID="imgbtnSubItems" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Heading" CausesValidation="false" />
            </div>--%>
    <div id="myFinalAuditReportModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Final Audit Report</b></h4>
                    <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body" style="height: 250px">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblFinalAuditReportError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="lblHAuditType" runat="server" Text="* Audit Type"></asp:Label>
                        <asp:DropDownList ID="ddlAuditType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div id="divAllTypeReports" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                            <asp:GridView ID="gvAllTypeReports" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAllTypeReports" Checked="true" Enabled="false" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAllTypeReports_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectReport" runat="server" CssClass="hvr-bounce-in" Checked="true" Enabled="false" />
                                            <asp:Label ID="lblReportID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="100%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Download" class="btn-ok" ID="btnDownload"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModalValidationMsg" runat="server"></asp:Label>
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
       <div id="ModalExcelValidationfrz" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modalmsg-dialog">
                <div class="modalmsg-content">
                    <div class="modalmsg-header">

                        <h4 class="modal-title"><b>TRACe</b></h4>
                    </div>
                    <div class="modalmsg-body">
                        <div id="divExcelMsgTypefrz" class="alert alert-info">
                            <p>
                                <strong>
                                    <asp:Label ID="lblfrz" runat="server"></asp:Label>
                                </strong>
                            </p>
                        </div>
                    </div>
                    <div class="modalmsg-footer">
                         <button id="btnfrz" runat="server" class="btn-ok">
                            Yes
                        </button>
                        <button data-dismiss="modal" runat="server" class="btn-ok" id="Button4">
                            No
                        </button>
                    </div>
                </div>
            </div>
        </div>
    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Visible="false" Width="99%" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>


