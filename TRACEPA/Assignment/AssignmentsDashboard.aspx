<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AssignmentsDashboard.aspx.vb" Inherits="TRACePA.AssignmentsDashboard" %>

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

        .grid-container {
            display: grid;
            grid-template-columns: auto auto auto auto auto auto;
            column-gap: 50px;
            padding: 15px 15px 0px 15px;
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
            $('#<%=ddlAsignmentCustomerName.ClientID%>').select2();
            $('#<%=ddlAsignmentPartner.ClientID%>').select2();
            $('#<%=ddlTask.ClientID%>').select2();
            $('#<%=ddlAssignmentEmployee.ClientID%>').select2();
            $('#<%=gvAssignment.ClientID%>').DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: true, targets: [] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=lstComplianceType.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
            $('#<%=lstWorkStatus.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).keydown(function (event) {
            var pressedKey = String.fromCharCode(event.keyCode).toLowerCase();
            if (event.ctrlKey && event.altKey && pressedKey == "n") {
                __doPostBack('<%= imgbtnAdd.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "l") {
                __doPostBack('<%= imgbtnLoad.UniqueID%>', '');
                return true;
            }
        });
    </script>
    <script type="text/javascript">        
        function OverDueTasksformServerAction() {
            __doPostBack('<%= btnOverDueTasks.UniqueID%>', '');
            return true;
        }
        function TodayTasksformServerAction() {
            __doPostBack('<%= btnTodayTasks.UniqueID%>', '');
            return true;
        }
        function OpenTasksformServerAction() {
            __doPostBack('<%= btnOpenTasks.UniqueID%>', '');
            return true;
        }
        function ClosedTasksformServerAction() {
            __doPostBack('<%= btnClosedTasks.UniqueID%>', '');
            return true;
        }
        function MyOpenTasksformServerAction() {
            __doPostBack('<%= btnMyOpenTasks.UniqueID%>', '');
            return true;
        }
        function MyOverDueTasksformServerAction() {
            __doPostBack('<%= btnMyOverDueTasks.UniqueID%>', '');
            return true;
        }
    </script>
    <%--============== Partner chart============================--%>
    <!-- Styles -->
    <style>
        #chartdiv {
            width: 100%;
            height: 500px;
        }
    </style>

    <!-- Resources -->
   
    <%--=================--%>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b></b></h2>
            </div>

        </div>
    </div>
    <div id="divEmp" runat="server" visible="false" style="margin-top: 30%; margin-left: 35%;">
        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
    </div>
    <div id="divPartner" runat="server" visible="false">
        <div class="col-sm-12 col-md-12">
            <div class="pull-left">
                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
            <div class="card">
                <div runat="server" id="divAssignmentheader" class="card-header">
                    <i class="fa fa-pencil-square" style="font-size: large"></i>
                    <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                    <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Assignments Dashboard" Font-Size="Small"></asp:Label>
                    <div class="pull-right" style="padding-right: 15px;">
                        <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Schedule Assignments (ctrl + alt + n)" />
                        <asp:ImageButton ID="imgbtnLoad" ClientIDMode="Static" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Load (ctrl + alt + l)" />
                                <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                    <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                                <ul class="dropdown-menu">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                    <li role="separator" class="divider"></li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                                </ul>
                    </div>
                </div>
                </div>
            <div class="card">
                <div id="divAssignmentBody" runat="server" clientidmode="Static" aria-labelledby="headingOne">
                    <div class="card-body">
                        <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                            <div class="col-sm-3 col-xl-8 col-md-3">
                                <div class="card hvr-bounce-in" id="divOverDueTasks" onclick="OverDueTasksformServerAction()">
                                    <div class="card-header bg-danger" data-toggle="collapse" aria-expanded="false">
                                        <span class="title"><%--<i class="fa fa-exclamation-triangle fa-beat"></i>--%>
                                            <i class="fal fa-clock fa-fw fa-xl margin-right-md fa-spin" style="color: var(--white); --fa-animation-duration: 2s;"></i>
                                            <asp:Label runat="server" CssClass="form-label" ForeColor="White" Text="Over Due Tasks" Font-Size="Small" Style="padding-left: 10px"></asp:Label></span>
                                        <asp:Label CssClass="form-label text-center" ForeColor="White" Font-Size="Medium" ID="lblOverDueTasks" runat="server" Text="0" Font-Bold="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-xl-8 col-md-3">
                                <div class="card hvr-bounce-in" id="divTodayTasks" onclick="TodayTasksformServerAction()">
                                    <div class="card-header bg-Greenery" data-toggle="collapse" aria-expanded="false">
                                        <span class="title"><%--<i class="fa fa-list-ol"></i>--%>
                                            <i class="fal fa-clock fa-fw fa-xl margin-right-md fa-spin" style="color: var(--white); --fa-animation-duration: 2s;"></i>
                                            <asp:Label runat="server" CssClass="form-label" ForeColor="White" Text="Today Tasks" Font-Size="Small" Style="padding-left: 10px"></asp:Label></span>
                                        <asp:Label CssClass="form-label text-center" ForeColor="White" Font-Size="Medium" ID="lblTodayTasks" runat="server" Text="0" Font-Bold="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-xl-8 col-md-3">
                                <div class="card hvr-bounce-in" id="divOpenTasks" onclick="OpenTasksformServerAction()">
                                    <div class="card-header bg-warning" data-toggle="collapse" aria-expanded="false">
                                        <span class="title"><%--<i class="fa fa-pencil-square"></i>--%>
                                            <%--<i class="fal fa-clock fa-fw fa-xl margin-right-md fa-spin" style="color: var(--white); --fa-animation-duration: 2s;"></i>--%>
                                            <asp:Label runat="server" CssClass="form-label" ForeColor="White" Text="Open Tasks" Font-Size="Small" Style="padding-left: 10px"></asp:Label></span>
                                        <asp:Label ID="lblOpenTasks" CssClass="form-label  text-center" ForeColor="White" Font-Size="Medium" runat="server" Font-Bold="false" Text="0"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-xl-8 col-md-3">
                                <div class="card hvr-bounce-in" id="divClosedTasks" onclick="ClosedTasksformServerAction()">
                                    <div class="card-header bg-success" data-toggle="collapse" aria-expanded="false">
                                        <span class="title"><%--<i class="fa fa fa-check-circle"></i>--%>
                                            <asp:Label runat="server" CssClass="form-label" ForeColor="White" Text="Closed Task" Font-Size="Small" Style="padding-left: 10px"></asp:Label>
                                        </span>
                                        <asp:Label CssClass="form-label text-center" ForeColor="White" ID="lblClosedTasks" runat="server" Text="0" Font-Size="Medium" Font-Bold="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-xl-8 col-md-3" runat="server" style="visibility: hidden">
                                <div class="card hvr-bounce-in" id="divMyOpenTasks" onclick="MyOpenTasksformServerAction()">
                                    <div class="card-header bg-warning" data-toggle="collapse" aria-expanded="false">
                                        <span class="title"><%--<i class="fa fa-pencil-square"></i>--%>
                                            <%--<i class="fal fa-clock fa-fw fa-xl margin-right-md fa-spin" style="color: var(--white); --fa-animation-duration: 2s;"></i>--%>
                                            <asp:Label runat="server" CssClass="form-label" Text="My Open Taks" Font-Size="Small" ForeColor="White" Style="padding-left: 10px"></asp:Label></span>
                                        <asp:Label CssClass="form-label text-center" ID="lblMyOpenTasks" ForeColor="White" Font-Size="Medium" runat="server" Text="0" Font-Bold="false"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-3 col-xl-8 col-md-3" runat="server" style="visibility: hidden">
                                <div class="card hvr-bounce-in" id="divMyOverDueTasks" onclick="MyOverDueTasksformServerAction()">
                                    <div class="card-header bg-danger" data-toggle="collapse" aria-expanded="false">
                                        <span class="title"><%--<i class="fa fa-exclamation-triangle fa-beat"></i>--%>
                                            <%--<i class="fal fa-clock fa-fw fa-xl margin-right-md fa-spin" style="color: var(--white); --fa-animation-duration: 2s;"></i>--%>
                                            <asp:Label runat="server" CssClass="form-label" ForeColor="White" Text="My Over Dues" Font-Size="Small" Style="padding-left: 10px"></asp:Label></span>
                                        <asp:Label CssClass="form-label text-center" ForeColor="White" Font-Size="Large" ID="lblMyOverDueTasks" runat="server" Text="0" Font-Bold="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblHFY" Text="FY" runat="server"></asp:Label>
                                <asp:DropDownList Width="100%" ID="ddlAsignmentFinancialYear" runat="server" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblHComplianceType" runat="server" Text="Type"></asp:Label>
                                <br />
                                <asp:ListBox ID="lstComplianceType" runat="server" Width="100%" Font-Size="10px" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:DropDownList Width="100%" ID="ddlAsignmentCustomerName" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:DropDownList Width="100%" ID="ddlAsignmentPartner" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:DropDownList Width="100%" ID="ddlTask" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <br />
                                <asp:DropDownList Width="100%" ID="ddlAssignmentEmployee" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHWorkStatus" runat="server" Text="Work Status"></asp:Label>
                                <asp:ListBox ID="lstWorkStatus" runat="server" Width="100%" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="divPendingAssignment" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow: scroll">
                            <asp:GridView ID="gvAssignment" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAssignment_RowDataBound">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Assignment No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AssignmentID") %>'></asp:Label>
                                            <asp:Label ID="lblClosed" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Closed") %>'></asp:Label>
                                            <asp:Label ID="lblCustomerFullName" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName") %>'></asp:Label>
                                            <asp:LinkButton ID="lnkAssignmentNo" runat="server" Font-Size="9px" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "AssignmentNo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Customer" ItemStyle-Width="12%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="9%" />
                                    <%-- <asp:BoundField DataField="FinancialYear" HeaderText="FY" ItemStyle-Width="5%" />--%>
                                    <asp:BoundField DataField="Task" HeaderText="Assignment/Task" ItemStyle-Width="11%" />
                                    <%--<asp:BoundField DataField="SubTask" HeaderText="Sub Task" ItemStyle-Width="7%" />--%>
                                    <asp:BoundField DataField="Employee" HeaderText="Assigned To" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-Width="6%" />
                                    <asp:BoundField DataField="DueDate" HeaderText="Start Date" ItemStyle-Width="6%" />
                                    <asp:BoundField DataField="ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="10%" />
                                    <%--<asp:BoundField DataField="TimeTaken" HeaderText="Time taken" ItemStyle-Width="5%" />--%>
                                    <asp:TemplateField HeaderText="Work Status" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WorkStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="BillingStatus" HeaderText="Billing status" ItemStyle-Width="5%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalAssignmentDashboardValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssignmentDashboardValidationMsg" runat="server"></asp:Label>
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
    <div style="display: none;">
        <asp:Button ID="btnOverDueTasks" runat="server" Text="OverDueTasks" />
        <asp:Button ID="btnTodayTasks" runat="server" Text="TodayTasks" />
        <asp:Button ID="btnOpenTasks" runat="server" Text="OpenTasks" />
        <asp:Button ID="btnClosedTasks" runat="server" Text="ClosedTasks" />
        <asp:Button ID="btnMyOpenTasks" runat="server" Text="MyOpenTasks" />
        <asp:Button ID="btnMyOverDueTasks" runat="server" Text="MyOverDueTasks" />
        <asp:Label ID="lblOverDueTaskIds" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblTodayTaskIds" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblOpenTaskIds" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblClosedTaskIds" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblMyOpenTaskIds" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblMyOverDueTaskIds" runat="server" Text=""></asp:Label>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
