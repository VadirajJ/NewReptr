<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="Home.aspx.vb" Inherits="TRACePA.Home1" %>

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

        .line {
            display: block;
            height: 1px;
            border: 0;
            border-top: 1px solid #ccc;
            margin: 1em 0;
            padding: 0;
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
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            //$('[data-toggle="tooltip"]').tooltip();            
            $('#<%=ddlAuditNos.ClientID%>').select2();
        });
        $(document).ready(function () {
            $.noConflict();
            $('#<%=gvPendingAssignment.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvPendingAssignment.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 10,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvReviewAssignment.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvReviewAssignment.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvCompletionAssignment.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvCompletionAssignment.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvUnbilledAssignment.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvUnbilledAssignment.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvUserTimeline.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvUserTimeline.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0,] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=GVCustremarks.ClientID%>').DataTable({
                searching: true,
                iDisplayLength: 10,
                aLengthMenu: [[10, 20, 30, 40, 50, 100, 500, -1], [10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1, 8] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

       <%-- $(document).keydown(function (event) {
            var pressedKey = String.fromCharCode(event.keyCode).toLowerCase();
            if (event.ctrlKey && event.altKey && pressedKey == "u") {
                __doPostBack('<%= imgbtnSKUser.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "c") {
                __doPostBack('<%= imgbtnSKCustomer.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "d") {
                __doPostBack('<%= imgbtnSKAsgDashboard.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "s") {
                __doPostBack('<%= imgbtnSKSchedule.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "t") {
                __doPostBack('<%= imgbtnSKTasks.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "r") {
                __doPostBack('<%= imgbtnSKReports.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "l") {
                __doPostBack('<%= imgbtnSKCompliance.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "i") {
                __doPostBack('<%= imgbtnSKInvoice.UniqueID%>', '');
                return true;
            }
        });--%>
    </script>

    <%--Amcharts--%>
    <script src="https://cdn.amcharts.com/lib/4/core.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/charts.js"></script>
    <script src="https://cdn.amcharts.com/lib/4/themes/animated.js"></script>

    <%--=============PendingTask===================--%>
    <%--<script>
        am4core.ready(function () {

            // Themes begin
            am4core.useTheme(am4themes_animated);
            // Themes end

            // Create chart instance
            var chart = am4core.create("PendingChartdiv", am4charts.XYChart);
            chart.logo.height = -1115;
            chart.exporting.menu = new am4core.ExportMenu();
            chart.exporting.menu.verticalAlign = "bottom";
            chart.exporting.menu.align = "left"
            chart.dataSource.url = "../Json/PendingTask/jsonPendingBARChart.json";

            var categoryAxis = chart.xAxes.push(new am4charts.CategoryAxis());
            categoryAxis.dataFields.category = "Month Name";
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
                series.dataFields.categoryX = "Month Name";
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

            createSeries("PendingTasks", "PendingTasks");
            // Legend
            chart.legend = new am4charts.Legend();

        }); // end am4core.ready()
    </script>--%>
    <%-- <style>
        #PendingChartdiv {
            width: 100%;
            height: min-content;
        }
    </style>--%>
    <%--======================Financial Audit Status========================--%>

    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <style>
        @charset "UTF-8";

        .multi-steps > li.is-active ~ li:before,
        .multi-steps > li.is-active:before {
            content: counter(stepNum);
            font-family: inherit;
            font-weight: 700;
        }

        .multi-steps > li.is-active ~ li:after,
        .multi-steps > li.is-active:after {
            background-color: #27AE60;
        }

        .multi-steps {
            display: table;
            table-layout: fixed;
            width: 100%;
        }

            .multi-steps > li {
                counter-increment: stepNum;
                text-align: center;
                display: table-cell;
                position: relative;
                color: #000;
            }

                .multi-steps > li:before {
                    content: "";
                    content: "✓;";
                    content: "𐀃";
                    content: "𐀄";
                    content: "✓";
                    display: block;
                    margin: 0 auto 4px;
                    background-color: #27AE60;
                    width: 36px;
                    height: 36px;
                    line-height: 32px;
                    text-align: center;
                    font-weight: bold;
                    border-width: 2px;
                    border-style: solid;
                    border-color: #27AE60;
                    border-radius: 50%;
                }

                .multi-steps > li:after {
                    content: "";
                    height: 2px;
                    width: 100%;
                    background-color: #27AE60;
                    position: absolute;
                    top: 16px;
                    left: 50%;
                    z-index: -1;
                }

                .multi-steps > li:last-child:after {
                    display: none;
                }

                .multi-steps > li.is-active:before {
                    background-color: #27AE60;
                    border-color: #27AE60;
                    color: white
                }

                .multi-steps > li.is-active ~ li {
                    color: #808080;
                }

                    .multi-steps > li.is-active ~ li:before {
                        background-color: whitesmoke;
                        border-color: whitesmoke;
                    }

        .line {
            height: 6px;
            background: red;
        }

        .blink_me {
            background-color: red;
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <%--=========================DashBoard===========================================--%>
    <div class="col-sm-12 col-md-12 col-lg-12  divmargin">
        <div class="col-sm-12 col-md-12 divmargin">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="reportDetailsMN">
            <div class="sectionTitleMn">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <%--<div class="line"></div>--%>
                    <div class="col-sm-9 col-md-9" style="padding: 0px">
                        <div class="pull-left">
                            <h2><b>Dashboard</b></h2>
                        </div>
                    </div>
                    <div class="col-sm-3 col-md-3" style="padding: 0px">
                        <div class="pull-right">
                            <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100px">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--================================Pending Task Grid, Pending Chart & Employee Monthly Performance================================--%>
    <div class="col-sm-12 col-md-12 col-lg-12  divmargin">
        <div class="col-sm-6 col-md-6 divmargin">
            <div class="card">
                <div class="card-header ">
                    <asp:Label runat="server" ID="Label4" CssClass="form-label" Font-Bold="true" Text="Pending Tasks till today" Font-Size="Small"></asp:Label>
                </div>
                <div id="PendingTask">
                    <div class="card-body">
                        <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div5" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-x: scroll;">
                            <asp:GridView ID="gvPendingAssignment" ShowHeader="true" CssClass="table bs" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="28%" />
                                    <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="13%" />
                                    <asp:TemplateField HeaderText="Task" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                            <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                            <asp:LinkButton ID="lnkTask" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Task") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="DueDate" HeaderText="Due Date" ItemStyle-Width="16%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--============================Charts====================================--%>
        <%--<div class="col-sm-5 col-md-5 divmargin">
            <div class="col-sm-12 col-md-12 col-lg-12  divmargin">
                <div class="card">
                    <div class="card-header ">
                        <asp:Label runat="server" ID="Label6" CssClass="form-label" Font-Bold="true" Text="Pending Tasks till today" Font-Size="Small"></asp:Label>
                    </div>
                    <div id="PendingTaskChart">
                        <div class="card-body">
                            <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div8" runat="server" style="border-style: none; border-color: inherit; border-width: medium;">
                                <div id="PendingChartdiv"></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="col-sm-6 col-md-6 divmargin">
            <div class="card">
                <div class="card-header">
                    <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Email Notifications" Font-Size="Small"></asp:Label>
                </div>
                <div class="card-body">
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="ddlAuditNos" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-12 col-md-12" style="overflow-x: scroll; max-height: 300px">
                        <asp:GridView ID="GVCustremarks" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="Notification" HeaderText="Notification" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="AuditNo" HeaderText="Audit No " ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Description" HeaderText="Checkpont/Ledger/Query" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Observations" HeaderText="Observations" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Comments_by" HeaderText="User name" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Role" HeaderText="Role" ItemStyle-Width="15%" />
                                <asp:TemplateField HeaderText="Comments Type" ItemStyle-Width="10%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommentsby" Text='<%# DataBinder.Eval(Container, "DataItem.Role") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblnotification" class="blink_me badge badge-primary text-uppercase">New
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 col-lg-12  divmargin">
        <div class="col-md-6" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" CssClass="table table-bordered" OnRowDataBound="PickColor_RowDataBound">
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
                            <b>By-</b>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                            <b>On-</b>
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
        <div class="card">
            <div class="card-header ">
                <asp:Label runat="server" ID="Label1" CssClass="form-label" Font-Bold="true" Text="Pending Tasks till today" Font-Size="Small"></asp:Label>
            </div>
            <div id="StandardAuditStatus">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-x: scroll;">
                        <asp:GridView ID="gvAuditStatus" ShowHeader="true" CssClass="table bs" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="28%" />
                                <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="13%" />
                                <asp:TemplateField HeaderText="Task" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkTask" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Task") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="DueDate" HeaderText="Due Date" ItemStyle-Width="16%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--========================================================Financial Status========================================================--%>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="card">
            <div class="card-header ">
                <asp:Label runat="server" ID="lblFinancialStatus" CssClass="form-label" Font-Bold="true" Text="Financial Audit Status" Font-Size="Small"></asp:Label>
            </div>
            <div id="Financial Status">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="divPendingTaskt" runat="server" style="border-style: none; border-color: inherit; border-width: medium;">

                        <div class="clearfix"></div>
                        <div class="col-sm-12 col-md-12 col-lg-12 divmargin">
                            <asp:Label ID="Label5" runat="server" CssClass="ErrorMsgLeft"></asp:Label>

                        </div>
                        <div class="container-fluid">
                            <ul runat="server" visible="false" id="UlProgressbas" class="list-unstyled multi-steps">
                                <li runat="server" id="liCustAssgn">Customer Creation with Industry Type</li>
                                <li runat="server" id="liRpyFormat" class="is-active">Report/Schedule Report creation/checking</li>
                                <li runat="server" id="LiUpdate" visible="false">Update</li>
                                <li runat="server" id="liUpload">Excel Uplaod Report/Schedule Mapping</li>
                                <li runat="server" id="lirptgen">Report Generation</li>
                                <li runat="server" id="lirptJe">Je Entries</li>
                                <li runat="server" id="lirptDownload">Report Save And Download</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--=======================================================UnbilledAssignment=======================================================--%>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="card">
            <div class="card-header collapsed" data-toggle="collapse" data-target="#UnbilledAssignment" aria-expanded="false">
                <span class="title"><i class="fas fa-angle-down rotate-icon"></i>
                    <asp:Label CssClass="form-label" Font-Italic="true" runat="server" Font-Size="Small" Text="Unbilled Tasks" Style="padding-left: 10px"></asp:Label></span>
                <asp:Label runat="server" ID="lblUnBilledTask" CssClass="form-label" Font-Bold="true" Text="Pending Tasks till today" Font-Size="Small"></asp:Label>
            </div>
            <div id="UnbilledAssignment" class="collapse" data-parent="#UnbilledAssignment">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="divUnbilledAssignment" runat="server" style="border-style: none; border-color: inherit; border-width: medium;">
                        <asp:GridView ID="gvUnbilledAssignment" CssClass="table bs" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="28%" />
                                <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="13%" />
                                <asp:TemplateField HeaderText="Task" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                        <asp:Label ID="lblTask" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Task") %>'></asp:Label>
                                        <%--<asp:LinkButton ID="lnkTask" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Task") %>'></asp:LinkButton>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="CompletionDate" HeaderText="Completion Date" ItemStyle-Width="26%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--=======================================================Waiting For Review=======================================================--%>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="card">
            <div class="card-header collapsed" data-toggle="collapse" data-target="#WaitingAssignment" aria-expanded="false">
                <span class="title"><i class="fas fa-angle-down rotate-icon"></i>
                    <asp:Label CssClass="form-label" Font-Italic="true" runat="server" Font-Size="Small" Text="Waiting for Review" Style="padding-left: 10px"></asp:Label></span>
                <asp:Label runat="server" ID="lblWIP" CssClass="form-label" Font-Bold="true" Text="Pending Tasks till today" Font-Size="Small"></asp:Label>
            </div>
            <div id="WaitingAssignment" class="collapse" data-parent="#WaitingAssignment">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div1" runat="server" style="border-style: none; border-color: inherit; border-width: medium;">
                        <asp:GridView ID="gvReviewAssignment" CssClass="table bs" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="28%" />
                                <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="13%" />
                                <asp:TemplateField HeaderText="Task" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkTask" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Task") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="DateOfRequest" HeaderText="Date of Request" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="16%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--======================================================Completed Assignment======================================================--%>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="card">
            <div class="card-header collapsed" data-toggle="collapse" data-target="#CompletedAssignment" aria-expanded="false">
                <span class="title"><i class="fas fa-angle-down rotate-icon"></i>
                    <asp:Label CssClass="form-label" Font-Italic="true" runat="server" Font-Size="Small" Text="Completed Assignment" Style="padding-left: 10px"></asp:Label></span>
                <asp:Label runat="server" ID="lblCompAssgn" CssClass="form-label" Font-Bold="true" Text="Pending Tasks till today" Font-Size="Small"></asp:Label>
            </div>
            <div id="CompletedAssignment" class="collapse" data-parent="#CompletedAssignment">
                <div class="card-body">
                    <div class="col-sm-6 col-md-6 divmargin">
                        <asp:TextBox ID="txtCompletionDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10" Width="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompletionDate" runat="server" ControlToValidate="txtCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompletionDate" runat="server" ControlToValidate="txtCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="cclCompletionDate" runat="server" PopupButtonID="txtCompletionDate" PopupPosition="BottomLeft" TargetControlID="txtCompletionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                        <asp:Button ID="btnLoad" runat="server" CssClass="btn-ok" Text="Load" Font-Bold="true" data-toggle="tooltip" data-placement="top" ValidationGroup="Validate"></asp:Button>
                    </div>
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="divCompletionAssignment" runat="server" style="border-style: none; border-color: inherit; border-width: medium;">
                        <asp:GridView ID="gvCompletionAssignment" CssClass="table bs" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="false" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" ItemStyle-Width="28%" />
                                <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="13%" />
                                <asp:TemplateField HeaderText="Task" ItemStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkTask" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "Task") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="DueDate" HeaderText="Due Date" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="DateOfCompletion" HeaderText="Date of Completion" ItemStyle-Width="16%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--=========================================================User Time line=========================================================--%>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divUsrTimeline">
        <div class="card">
            <div class="card-header  collapsed" data-toggle="collapse" data-target="#UsrTimeline" aria-expanded="true" aria-controls="divAssignment">
                <span class="title"><i class="fas fa-angle-down rotate-icon"></i>
                    <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                    <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                    <asp:Label runat="server" CssClass="form-label" Font-Italic="true" Text="User Time Line" Font-Size="Small" Style="padding-left: 10px"></asp:Label></span>
                <asp:Label runat="server" ID="lblUsertimeln" Font-Bold="true" CssClass="form-label" Font-Size="Small"></asp:Label>
            </div>
            <div id="UsrTimeline" class="collapse" aria-labelledby="headingOne" data-parent="#divUsrTimeline">
                <div class="card-body">
                    <div class="col-sm-6 col-md-6 divmargin">
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="aspxcontrols" MaxLength="10" AutoPostBack="true" Width="100px"></asp:TextBox>
                        <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomRight"
                            TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div4" runat="server" style="border-style: none; border-color: inherit; border-width: medium;">
                        <asp:GridView ID="gvUserTimeline" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                                <asp:BoundField DataField="usr_FullName" HeaderText="User" ItemStyle-Width="52%" />
                                <asp:BoundField DataField="Created" HeaderText="Task's Created" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="InProgress" HeaderText="Updated" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Completed" HeaderText="Completed" ItemStyle-Width="15%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
