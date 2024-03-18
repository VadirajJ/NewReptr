<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DynamicReports.aspx.vb" Inherits="TRACePA.DynamicReports" %>

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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#<%=ddlType.ClientID%>').select2();
            $('#<%=ddlPartner1.ClientID%>').select2();
            $('#<%=ddlEmployee1.ClientID%>').select2();
            $('#<%=ddlPartner2.ClientID%>').select2();
            $('#<%=ddlEntity3.ClientID%>').select2();
            $('#<%=ddlCustomer3.ClientID%>').select2();
            $('#<%=ddlTask3.ClientID%>').select2();
            $('#<%=ddlPartner3.ClientID%>').select2();
            $('#<%=ddlEmployee3.ClientID%>').select2();
            $('#<%=ddlCustomer4.ClientID%>').select2();
            $('#<%=ddlTask4.ClientID%>').select2();
            $('#<%=ddlPartner4.ClientID%>').select2();
            $('#<%=ddlEmployee4.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <%--Amcharts--%>
    <script src="https://cdn.amcharts.com/lib/4/core.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/charts.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/themes/animated.js"></script>
    <%--=====================Monthly Performance=======================--%>
    <script>
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            // Create chart instance
            var chart = am4core.create("chartdiv", am4charts.XYChart);
            chart.logo.height = -1115;
            chart.exporting.menu = new am4core.ExportMenu();
            chart.exporting.menu.verticalAlign = "bottom";
            chart.exporting.menu.align = "left"
            //chart.dataSource.url = "../Json/PendingTask/jsonPendingBARChart.json";
            chart.dataSource.url = "../Json/jsonBARChart.json";

            var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
            //categoryAxis.dataFields.category = "Month Name";
            categoryAxis.dataFields.category = "Employee";
            categoryAxis.renderer.grid.template.location = 0;


            var valueAxis = chart.yAxes.push(new am4charts.ValueAxis());
            valueAxis.renderer.inside = true;
            valueAxis.renderer.labels.template.disabled = true;
            valueAxis.min = 0;

            // Create series
            function createSeries(field, name) {

                // Set up series
                var series = chart.series.push(new am4charts.ColumnSeries());
                series.name = name;
                series.dataFields.valueY = field;
                //series.dataFields.categoryX = "Month Name";
                series.dataFields.categoryX = "Employee";
                series.sequencedInterpolation = true;

                // Make it stacked
                series.stacked = true;

                // Configure columns
                series.columns.template.width = am4core.percent(60);
                series.columns.template.tooltipText = "[bold]{name}[/]\n[font-size:14px]{categoryX}: {valueY}";

                // Add label
                var labelBullet = series.bullets.push(new am4charts.LabelBullet());
                labelBullet.label.text = "{valueY}";
                labelBullet.locationY = 0.5;
                labelBullet.label.hideOversized = true;

                return series;

            }

            //createSeries("PendingTasks", "PendingTasks");
            createSeries("TasksCompleted", "TasksCompleted");
            // Legend
            chart.legend = new am4charts.Legend();
            //// Add horizotal scrollbar
            //chart.scrollbarX = new am4core.Scrollbar();

        }); // end am4core.ready()
    </script>
    <style>
        #chartdiv {
            width: 100%;
            height: min-content;
        }
    </style>
    <div class="loader"></div>
    <div id="divEmp" runat="server" visible="false" style="margin-top: 30%; margin-left: 35%;">
        <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Dynamic Reports" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnLoad" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Load" ValidationGroup="Validate" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <%--<li>
                                <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" ValidationGroup="Validate" />
                            </li>
                            <li role="separator" class="divider"></li>--%>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" ValidationGroup="Validate" />
                                </li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblType" Text="* Type" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVType" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>


            <div id="divPartner" runat="server" visible="false">
                <div class="col-sm-12 col-md-12 divmargin">
                    <div class="pull-left">
                        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                </div>

                <div id="divResourceAvailability" runat="server" class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-2 col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblHFromDate1" runat="server" Text="* From Date"></asp:Label>
                            <asp:TextBox ID="txtFromDate1" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate1" runat="server" ControlToValidate="txtFromDate1" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFromDate1" runat="server" ControlToValidate="txtFromDate1" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <cc1:CalendarExtender ID="cclFromDate1" runat="server" PopupButtonID="txtFromDate1" PopupPosition="BottomRight" TargetControlID="txtFromDate1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                            </cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblHToDate1" runat="server" Text="* To Date"></asp:Label>
                            <asp:TextBox ID="txtToDate1" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate1" runat="server" ControlToValidate="txtToDate1" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVToDate1" runat="server" ControlToValidate="txtToDate1" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <cc1:CalendarExtender ID="cclToDate1" runat="server" PopupButtonID="txtToDate1" PopupPosition="BottomRight" TargetControlID="txtToDate1" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                            </cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblHPartner1" runat="server" Text="Partner"></asp:Label>
                            <asp:DropDownList ID="ddlPartner1" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Label ID="lblHEmployee1" runat="server" Text="Employee"></asp:Label>
                            <asp:DropDownList ID="ddlEmployee1" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div id="divResourceStatus" runat="server" class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHFromDate2" runat="server" Text="* From Date"></asp:Label>
                                <asp:TextBox ID="txtFromDate2" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate2" runat="server" ControlToValidate="txtFromDate2" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFromDate2" runat="server" ControlToValidate="txtFromDate2" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclFromDate2" runat="server" PopupButtonID="txtFromDate2" PopupPosition="BottomRight" TargetControlID="txtFromDate2" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHToDate2" runat="server" Text="* To Date"></asp:Label>
                                <asp:TextBox ID="txtToDate2" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate2" runat="server" ControlToValidate="txtToDate2" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVToDate2" runat="server" ControlToValidate="txtToDate2" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclToDate2" runat="server" PopupButtonID="txtToDate2" PopupPosition="BottomRight" TargetControlID="txtToDate2" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHPartner2" runat="server" Text="Partner"></asp:Label>
                                <asp:DropDownList ID="ddlPartner2" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHWorkStatus2" runat="server" Text="Work Status"></asp:Label>
                                <asp:DropDownList ID="ddlWorkStatus2" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divInvoiceReports" runat="server" class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHFromDate3" runat="server" Text="* From Date"></asp:Label>
                                <asp:TextBox ID="txtFromDate3" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate3" runat="server" ControlToValidate="txtFromDate3" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFromDate3" runat="server" ControlToValidate="txtFromDate3" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclFromDate3" runat="server" PopupButtonID="txtFromDate3" PopupPosition="BottomRight" TargetControlID="txtFromDate3" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHToDate3" runat="server" Text="* To Date"></asp:Label>
                                <asp:TextBox ID="txtToDate3" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate3" runat="server" ControlToValidate="txtToDate3" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVToDate3" runat="server" ControlToValidate="txtToDate3" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclToDate3" runat="server" PopupButtonID="txtToDate3" PopupPosition="BottomRight" TargetControlID="txtToDate3" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHTaxType3" runat="server" Text="Tax Type"></asp:Label>
                                <asp:DropDownList ID="ddlTaxType3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHInvoiceNo3" runat="server" Text="Invoice No"></asp:Label>
                                <asp:TextBox ID="txtInvoiceNo3" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHEntity3" runat="server" Text="Billing Entity"></asp:Label>
                                <asp:DropDownList ID="ddlEntity3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHCust3" runat="server" Text="Customer"></asp:Label>
                                <asp:DropDownList ID="ddlCustomer3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHPartner3" runat="server" Text="Partner"></asp:Label>
                                <asp:DropDownList ID="ddlPartner3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHEmployee3" runat="server" Text="Employee"></asp:Label>
                                <asp:DropDownList ID="ddlEmployee3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHTask3" runat="server" Text="Assignment/Task"></asp:Label>
                                <asp:DropDownList ID="ddlTask3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHWorkstatus3" runat="server" Text="Work Status"></asp:Label>
                                <asp:DropDownList ID="ddlWorkstatus3" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divAssignments" runat="server" class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHFromDate4" runat="server" Text="* From Date"></asp:Label>
                                <asp:TextBox ID="txtFromDate4" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate4" runat="server" ControlToValidate="txtFromDate4" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFromDate4" runat="server" ControlToValidate="txtFromDate4" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclFromDate4" runat="server" PopupButtonID="txtFromDate4" PopupPosition="BottomRight" TargetControlID="txtFromDate4" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHToDate4" runat="server" Text="* To Date"></asp:Label>
                                <asp:TextBox ID="txtToDate4" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate4" runat="server" ControlToValidate="txtToDate4" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVToDate4" runat="server" ControlToValidate="txtToDate4" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclToDate4" runat="server" PopupButtonID="txtToDate4" PopupPosition="BottomRight" TargetControlID="txtToDate4" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHCust4" runat="server" Text="Customer"></asp:Label>
                                <asp:DropDownList ID="ddlCustomer4" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHPartner4" runat="server" Text="Partner"></asp:Label>
                                <asp:DropDownList ID="ddlPartner4" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHEmployee4" runat="server" Text="Employee"></asp:Label>
                                <asp:DropDownList ID="ddlEmployee4" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHTask4" runat="server" Text="Assignment/Task"></asp:Label>
                                <asp:DropDownList ID="ddlTask4" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHWorkstatus4" runat="server" Text="Work Status"></asp:Label>
                                <asp:DropDownList ID="ddlWorkstatus4" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                    <div class="form-group">
                        <asp:GridView ID="gvResourceAvailability" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="4%" />
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="16%" />
                                <asp:BoundField DataField="Task" HeaderText="Task" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="DueDate" HeaderText="Start Date" ItemStyle-Width="9%" />
                                <asp:BoundField DataField="ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="16%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                    <div class="form-group">
                        <asp:GridView ID="gvResourceStatus" CssClass="table table-bordered" runat="server" AutoGenerateColumns="true" Width="100%" OnRowDataBound="gvResourceStatus_RowDataBound">
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                    <div class="form-group">
                        <asp:GridView ID="gvInvoiceReports" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                           <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="3%" />
                                <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="13%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblInvoiceID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InvoiceID") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkInvoiceNo" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="InvoiceType" HeaderText="Invoice Type" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="BillingEntity" HeaderText="Billing Entity" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="12%" />
                                <asp:BoundField DataField="CustomerGSTNo" HeaderText="GST No." ItemStyle-Width="7%" />
                                <asp:BoundField DataField="BeforeTax" HeaderText="Total Amount before Tax" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="TaxType1" HeaderText="Tax Type1 Amount" ItemStyle-Width="6%" />
                                <asp:BoundField DataField="TaxType2" HeaderText="Tax Type2 Amount" ItemStyle-Width="6%" />
                                <asp:BoundField DataField="TotalTax" HeaderText="Total Tax Amount" ItemStyle-Width="5%" />
                                <asp:BoundField DataField="AfterTax" HeaderText="Total Amount after Tax" ItemStyle-Width="7%" />
                                <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="6%" />
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="6%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                    <div class="form-group">
                        <asp:GridView ID="gvAssignment" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAssignment_RowDataBound">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Assignment No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblClosed" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Closed") %>'></asp:Label>
                                        <asp:Label ID="lblCustomerFullName" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName") %>'></asp:Label>
                                        <asp:Label ID="lnkAssignmentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignmentNo") %>'></asp:Label>
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


            <div id="divMonthMonthlyPerformance" runat="server" class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; padding: 0px">
                <div class="form-group">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Financial Year"></asp:Label>
                                <asp:DropDownList ID="ddlFYMonthlyPerformance" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Revenue"></asp:Label>
                                <asp:DropDownList ID="ddlMonthlyPerformance" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvMonthlyPerformance" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="80%" />
                                <asp:BoundField DataField="TasksCompleted" HeaderText="Tasks Completed" ItemStyle-Width="17%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div runat="server" id="chartdiv"></div>
                    </div>
                </div>
            </div>
            <div id="divRevenue" runat="server" class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; padding: 0px">
                <div class="form-group">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Financial Year"></asp:Label>
                                <asp:DropDownList ID="ddlFYRevenue" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label runat="server" Text="Revenue"></asp:Label>
                                <asp:DropDownList ID="ddlMonthRevenue" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvRevenue" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="28%" />
                                <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="19%" />
                                <asp:TemplateField HeaderText="Task" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                        <asp:Label ID="lblTask" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Task") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="DateOfCompletion" HeaderText="Completion Date" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="BillAmount" HeaderText="Bill Amount" ItemStyle-Width="10%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div id="ModalDynamicReportsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDynamicReportsValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
