<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DashboardAndScheduleDeatils.aspx.vb" Inherits="TRACePA.DashboardAndScheduleDeatils" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {        
            $('#<%=lstPartner.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
            $('#<%=lstReviewPartner.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });

            $('#<%=lstAdditionalSupportEmployee.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
        });     
    </script>
    <style>
        legend {
            margin-bottom: 5px;
            font-size: 14px;
            color: #919191;
        }
    </style>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Schedule Audit" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:Button runat="server" Text="New Schedule" class="btn-ok" ID="btnNewScheduleAudit"></asp:Button>
                    <asp:Button runat="server" Text="New Check Point" class="btn-ok" ID="btnNewCheckPoint"></asp:Button>
                    <asp:Button runat="server" Text="Resource Availability" class="btn-ok" ID="btnResourceAvailability"></asp:Button>
                </div>
            </div>
        </div>
        <div class="card">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <br />
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblHCustName" runat="server" Text="* Customer Name"></asp:Label>
                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVCustName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblHAuditNo" runat="server" Text="Audit No."></asp:Label>
                        <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4" style="padding-left: 35px;">
                    <br />
                    <asp:Label ID="lblHFY" runat="server">Financial Year : </asp:Label>
                    <asp:Label ID="lblFY" Font-Bold="true" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-8 col-md-8" style="padding: 0px">
                    <fieldset style="padding: 20px"><legend><b>Schedule Audit Details</b></legend></fieldset>
                    <div class="col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblHAuditType" runat="server" Text="* Audit Type"></asp:Label>
                            <asp:DropDownList ID="ddlAuditType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RFVAuditType" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblHReviewPartner" runat="server" Text="* Review Partner"></asp:Label>
                            <br />
                            <asp:ListBox ID="lstReviewPartner" runat="server" Width="100%" Font-Size="10px" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblHAdditionalSupportEmployee" runat="server" Text="* Select Team Member"></asp:Label>
                            <br />
                            <asp:ListBox ID="lstAdditionalSupportEmployee" runat="server" Width="100%" Font-Size="10px" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblHPartner" runat="server" Text="* Partner"></asp:Label>
                            <br />
                            <asp:ListBox ID="lstPartner" runat="server" Width="100%" Font-Size="10px" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblHScopeOfAudit" runat="server" Text="* Scope Of Audit"></asp:Label>
                            <asp:TextBox ID="txtScopeOfAudit" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="85px"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVScopeOfAudit" runat="server" SetFocusOnError="True" ControlToValidate="txtScopeOfAudit" Display="Dynamic" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVScopeOfAudit" runat="server" ControlToValidate="txtScopeOfAudit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4 col-md-4">
                    <fieldset style="padding: 20px"><legend><b>Audit TimeLine</b></legend></fieldset>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHDueDate" runat="server" Text="* Start Date"></asp:Label>
                                <asp:TextBox ID="txtTimeLineStartDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtTimeLineStartDate" PopupPosition="TopRight" TargetControlID="txtTimeLineStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTimeLineStartDate" runat="server" ControlToValidate="txtTimeLineStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTimeLineStartDate" runat="server" ControlToValidate="txtTimeLineStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclDueDate" runat="server" PopupButtonID="txtTimeLineStartDate" PopupPosition="TopRight" TargetControlID="txtTimeLineStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHExpectedCompletionDate" runat="server" Text="* Expected Completion Date"></asp:Label>
                                <asp:TextBox ID="txtExpectedCompletionDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtExpectedCompletionDate" PopupPosition="TopRight" TargetControlID="txtExpectedCompletionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExpectedCompletionDate" runat="server" ControlToValidate="txtExpectedCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExpectedCompletionDate" runat="server" ControlToValidate="txtExpectedCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender8" runat="server" PopupButtonID="txtExpectedCompletionDate" PopupPosition="TopRight" TargetControlID="txtExpectedCompletionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHReportReviewDate" runat="server" Text="* Report Review Date"></asp:Label>
                                <asp:TextBox ID="txtReportReviewDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" PopupButtonID="txtReportReviewDate" PopupPosition="TopRight" TargetControlID="txtReportReviewDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTimeLineRptRvDate" runat="server" ControlToValidate="txtReportReviewDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTimeLineRptRvDate" runat="server" ControlToValidate="txtReportReviewDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender9" runat="server" PopupButtonID="txtReportReviewDate" PopupPosition="TopRight" TargetControlID="txtReportReviewDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHReportFilingDate" runat="server" Text="* Report Filing Date"></asp:Label>
                                <asp:TextBox ID="txtReportFilingDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server" PopupButtonID="txtReportFilingDate" PopupPosition="TopRight" TargetControlID="txtReportFilingDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVReportFilingDate" runat="server" ControlToValidate="txtReportFilingDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVReportFilingDate" runat="server" ControlToValidate="txtReportFilingDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender10" runat="server" PopupButtonID="txtReportFilingDate" PopupPosition="TopRight" TargetControlID="txtReportFilingDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHDateForMRS" runat="server" Text="* Date For MRS"></asp:Label>
                                <asp:TextBox ID="txtDateForMRS" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server" PopupButtonID="txtDateForMRS" PopupPosition="TopRight" TargetControlID="txtDateForMRS" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTimeLineMRSDate" runat="server" ControlToValidate="txtDateForMRS" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTimeLineMRSDate" runat="server" ControlToValidate="txtDateForMRS" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSchedule"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender11" runat="server" PopupButtonID="txtDateForMRS" PopupPosition="TopRight" TargetControlID="txtDateForMRS" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding-left: 10px">
                <div class="pull-left">
                    <asp:Button runat="server" Text="Save Schedule" class="btn-ok" ID="btnSaveSchedule" ValidationGroup="ValidateSchedule"></asp:Button>
                    <asp:Button runat="server" Text="Update Schedule" class="btn-ok" ID="btnUpdateSchedule" ValidationGroup="ValidateSchedule"></asp:Button>
                </div>
                <br />
            </div>
            <div id="divCheckPoint" runat="server">
                <fieldset style="padding: 20px"><legend><b>Audit Check Point & Team Members Details</b></legend></fieldset>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="col-sm-4 col-md-4" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="form-group">
                                <asp:Label ID="lblHHeading" runat="server" Text="* Heading"></asp:Label>
                                <asp:DropDownList ID="ddlHeading" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVHeading" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlHeading" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateCheckPoint"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHTeamMember" runat="server" Text="Assign To Team Member"></asp:Label>
                                    <asp:DropDownList ID="ddlTeamMember" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHWorkType" runat="server" Text="Full/Part Time"></asp:Label>
                                    <asp:DropDownList ID="ddlWorkType" runat="server" CssClass="aspxcontrols">
                                        <asp:ListItem Text="Select Work Type" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Part Time" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Full Time" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHStartDate" runat="server" Text="Start Date"></asp:Label>
                                    <asp:TextBox ID="txtStartDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender6" runat="server" PopupButtonID="txtDueDate" PopupPosition="TopRight" TargetControlID="txtStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateCheckPoint"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHEndDate" runat="server" Text="End Date"></asp:Label>
                                    <asp:TextBox ID="txtEndDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender7" runat="server" PopupButtonID="txtDueDate" PopupPosition="TopRight" TargetControlID="txtEndDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEndDate" runat="server" ControlToValidate="txtEndDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateCheckPoint"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHHoursPerDay" runat="server" Text="Hours/Day"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtHoursPerDay" Text="" CssClass="aspxcontrols" />
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVHoursPerDay" runat="server" ControlToValidate="txtHoursPerDay" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Hours/Day." ValidationGroup="ValidateCheckPoint" ValidationExpression="^[1-9]\d*(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHTotalHrs" Text="Total No.of hours" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" ID="txtTotalHrs" CssClass="aspxcontrols" />
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTotalHrs" runat="server" ControlToValidate="txtTotalHrs" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Total No.of hours." ValidationGroup="ValidateCheckPoint" ValidationExpression="^[1-9]\d*(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-8 col-md-8">
                        <div class="form-group">
                            <div id="divHCL" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto;">
                                <asp:GridView ID="gvHeadingCheckList" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAllCheckList" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAllCheckList_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectCheckList" runat="server" CssClass="hvr-bounce-in" />
                                                <asp:Label ID="lblCheckPointID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACM_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ACM_Checkpoint" HeaderText="Check Point" ItemStyle-Width="83%" />
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectMandatory" runat="server" Text="Mandatory" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6">
                    <div class="form-group">
                        <asp:Button runat="server" Text="Add Check Points" class="btn-ok" ID="btnAdd" ValidationGroup="ValidateCheckPoint"></asp:Button>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                    <asp:GridView ID="GvAssignDetails" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="false" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField HeaderText="Heading" DataField="SACD_Heading" ItemStyle-Width="35%"></asp:BoundField>
                            <asp:BoundField HeaderText="Team Member" DataField="Employee"></asp:BoundField>
                            <asp:TemplateField HeaderText="Total No. of Check Point">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkCheckPoint" Font-Italic="true" runat="server" CommandName="VIEW" Text='<%# DataBinder.Eval(Container, "DataItem.NoCheckpoints") %>'></asp:LinkButton>
                                    <asp:Label ID="lblCheckPointIds" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SACD_CheckpointId") %>'></asp:Label>
                                    <asp:Label ID="lblDBPkID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SACD_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Timeline" DataField="Timeline"></asp:BoundField>
                            <asp:BoundField HeaderText="Working Hours" DataField="Working_Hours" ItemStyle-Width="8%"></asp:BoundField>
                            <asp:TemplateField ItemStyle-Width="7%">
                               <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" data-toggle="tooltip" data-placement="bottom" title="Edit" CommandName="UPDATEAD" runat="server" CssClass="hvr-bounce-in" ImageUrl = "~/Images/Edit16.png"/>
                                    <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" ImageUrl = "~/Images/Trash16.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                    <div id="divAT" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto;">
                        <asp:GridView ID="gvSAFinalCheckList" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="ACM_Heading" HeaderText="Heading" ItemStyle-Width="35%" />
                                <asp:BoundField DataField="ACM_Checkpoint" HeaderText="Check Point" ItemStyle-Width="50%" />
                                <asp:BoundField DataField="SAC_Mandatory" HeaderText="Is Mandatory" ItemStyle-Width="15%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalSelectedCheckPoints" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modal-lg" style="margin-left: 35%; margin-top: 4%; width: 500px">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-sm-11 col-md-11">
                            <h4 class="modal-title" style="text-align: center;"><b>Check Points</b></h4>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row ">
                            <div class="col-sm-12 col-md-12 pull-left">
                                <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                            <div class="col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                                <asp:GridView ID="gvChkpoints" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="true" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myResourceAvailabilityModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;"><b>Resource Availability</b></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button runat="server" type="button" class="close pull-right" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body" style="height: 425px">
                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvResourceAvailability" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" Height="300px" AutoGenerateColumns="false" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="usr_FullName" HeaderText="Resource Name" ItemStyle-Width="30%" />
                                <asp:BoundField DataField="AAST_ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="30%" />
                                <asp:BoundField DataField="AAS_AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="40%" />
                                <asp:BoundField DataField="RowNo" HeaderText="RowNo" ItemStyle-Width="0%" Visible="false" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <div id="ModalScheduleValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblScheduleModalMsg" runat="server"></asp:Label>
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
</asp:Content>
