<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DashboardAndSchedule.aspx.vb" Inherits="TRACePA.DashboardAndSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.js"></script>
    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/themes/redmond/jquery-ui.css">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.10.4/jquery-ui.js"></script>
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
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">        
        $(document).ready(function () {
            //$('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=gvDashboard.ClientID%>').DataTable({
                searching: true,
                iDisplayLength: 10,
                aLengthMenu: [[10, 20, 30, 40, 50, 100, 500, -1], [10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1, 8,9] }],
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
            <div runat="server" id="divCompheader" class="card-header ">
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Dashboard And Schedule Audit" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAddSchedule" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Schedule" ValidationGroup="Validate" />
                </div>
            </div>
        </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblHCustomerName" runat="server" Text="* Customer Name "></asp:Label><asp:ImageButton ID="imgbtnAddCust" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" ToolTip="Add New Customer" data-placement="bottom" Style="width: 20px;" />
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100%"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFVCustomerName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustomerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblHFY" Text="Financial Year" runat="server" Width="100%"></asp:Label>
                                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:GridView ID="gvDashboard" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvDashboard_RowDataBound">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnAuditId" runat="server" CommandName="SelectAudit" ToolTip="Select" CssClass="hvr-bounce-in" Text='<%# DataBinder.Eval(Container, "DataItem.AuditID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr No."/>
                                    <asp:TemplateField HeaderText="Audit No." ItemStyle-Width="18%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAuditNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AuditNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomerShortName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerShortName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="15%" />
                                    <asp:BoundField DataField="AuditType" HeaderText="Audit Type" ItemStyle-Width="14%" />
                                    <asp:BoundField DataField="AuditDate" HeaderText="Audit Timeline" ItemStyle-Width="12%" />
                                    <asp:BoundField DataField="AuditStatus" HeaderText="Audit Status" ItemStyle-Width="10%" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustID") %>'></asp:Label>
                                            <asp:Label ID="lblAuditID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditID") %>'></asp:Label>
                                            <asp:Label ID="lblStatusID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.StatusID") %>'></asp:Label>
                                            <asp:Label ID="lblCustomerFullName" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerName") %>'></asp:Label>
                                            <asp:ImageButton ID="imgbtnUpdate" data-toggle="tooltip" data-placement="bottom" title="Update Schedule" CommandName="SheduleUpdate" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Time booking">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnTimebooking" data-toggle="tooltip" data-placement="bottom" title="Time Booking" CommandName="Timeline" runat="server" CssClass="hvr-bounce-in" />
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
    <div id="ModalTimebooking" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%; height: 550px; overflow: auto">
                <div class="modal-header">
                    <div class="col-sm-7 col-md-7">
                        <h4 class="modal-title"><b>Time Booking</b></h4>
                    </div>
                    <div class="col-sm-5 col-md-5">
                        <button runat="server" type="button" class="close pull-right" data-dismiss="modal">&times;</button>
                        <asp:Label ID="lblheadusername" runat="server" Text="Resource Logged in: "></asp:Label>
                        <asp:Label runat="server" Font-Bold="true" ID="lblUser"></asp:Label>
                    </div>
                </div>
                <div class="modal-body" style="height: auto">
                    <div class="col-sm-12 col-md-12">
                        <asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" Text="* Customer name"></asp:Label>
                                <asp:TextBox ID="txtcustname" Enabled="false" runat="server" Font-Bold="true" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" Text="* Audit no"></asp:Label>
                                <asp:TextBox ID="txtAuditNo" runat="server" Enabled="false" Font-Bold="true" Width="100%"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="overflow: auto; max-height: 200px; overflow: auto; padding: 0px">
                        <asp:GridView ID="gvTimeleine" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Heading" ItemStyle-Width="35%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblHeading" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Heading") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="NoCheckpoints" HeaderText="No. Check Points" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="NoEmployee" HeaderText="No. Employee" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Timeline" HeaderText="Timeline" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Working_Hours" HeaderText="Working Hours" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="TotalHoursBooked" HeaderText="Total Booked Hours" ItemStyle-Width="20%" />
                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%" Visible="true">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtntimelineEdit" runat="server" ToolTip="EditTime" CommandName="EditTime" CssClass="hvr-bounce-in" />
                                        <asp:Label ID="lblCustID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustID") %>'></asp:Label>
                                        <asp:Label ID="lblAuditID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditID") %>'></asp:Label>
                                        <asp:Label ID="lblheadingid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Headingid") %>'></asp:Label>
                                        <asp:Label ID="lblCheckpointids" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Checkpointids") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="accordion" runat="server" id="divhistory">
                        <div class="card col-sm-8 col-md-8" style="padding: 0px">
                            <div class="card-header" id="headingTwo">
                                <h5>
                                    <a data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">Click to View Comments History
                        <br />
                                        <asp:Label runat="server" Text="Heaing Name:" Font-Bold="true"></asp:Label>
                                        <asp:Label runat="server" ID="lblHeadingname"></asp:Label>
                                    </a>
                                </h5>
                            </div>
                            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordion">
                                <div class="card-body">
                                    <div style="overflow: auto; max-height: 200px">
                                        <asp:GridView ID="grdcommentsHistory" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                            <Columns>
                                                <asp:BoundField DataField="Startdate" HeaderText="Start date" ItemStyle-Width="40%" />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ItemStyle-Width="20%" />
                                                <asp:BoundField DataField="TotalHrs" HeaderText="Total Hrs" ItemStyle-Width="20%" />
                                                <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="20%" />
                                                <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnCommentsEdit" runat="server" ToolTip="EditTime" CommandName="EditTime" CssClass="hvr-bounce-in" />
                                                        <asp:Label ID="lblCustID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustID") %>'></asp:Label>
                                                        <asp:Label ID="lblAuditID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditID") %>'></asp:Label>
                                                        <asp:Label ID="lblCommntsid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.pkid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4" runat="server" id="divtimelineEdit1" visible="false">
                        <div class="form-group">
                            <asp:Label runat="server" Text="Start date"></asp:Label>
                            <asp:TextBox ID="txtstartdate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols disable_past_dates" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExpectedCompletionDate" runat="server" ControlToValidate="txtstartdate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExpectedCompletionDate" runat="server" ControlToValidate="txtstartdate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            <cc1:CalendarExtender Enabled="True" ID="cclstartdate" runat="server" PopupButtonID="txtstartdate" PopupPosition="TopRight" TargetControlID="txtstartdate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="End date"></asp:Label>
                            <asp:TextBox ID="txtEnddate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnddate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEnddate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtEnddate" PopupPosition="TopRight" TargetControlID="txtEnddate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                            </cc1:CalendarExtender>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="Total Hours Spent"></asp:Label>
                            <asp:TextBox runat="server" ID="txttotalHours" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" Text="Comments"></asp:Label>
                            <asp:TextBox TextMode="MultiLine" ID="txtComments" Height="50px" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <asp:Button runat="server" ID="btnTimelineupdate" class="btn-ok" Text="Update" />
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="New Check Point" class="btn-ok" ID="Button1" Visible="false"></asp:Button>
                    <asp:Button runat="server" Text="Save Schedule" class="btn-ok" ID="Button2" Visible="false" ValidationGroup="ValidateSchedule"></asp:Button>
                    <asp:Button runat="server" Text="Update Schedule" class="btn-ok" ID="Button3" Visible="false" ValidationGroup="ValidateSchedule"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalDashboardValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDashboardValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button ID="btnOk" runat="server" Text="ok" class="btn-ok" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
