<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ScheduleAssignments.aspx.vb" Inherits="TRACePA.ScheduleAssignments" %>

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
            $('#<%=ddlAssignmentNo.ClientID%>').select2();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlPartner.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=ddlTask.ClientID%>').select2();
            $('#<%=ddlEmployee.ClientID%>').select2();
            $('#<%=ddlFrequency.ClientID%>').select2();
            $('#<%=ddlWorkStatus.ClientID%>').select2();
            $('#<%=lstAY.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: false,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
            <%--$('#<%=gvEmployeeSubTask.ClientID%>').DataTable({
                order: [],
                columnDefs: [{ orderable: false, targets: [] }],
                bPaginate: false,
                bLengthChange: false,
                bFilter: false,
                bInfo: false,
                bAutoWidth: false
            });--%>
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });

        $(document).keydown(function (event) {
            var pressedKey = String.fromCharCode(event.keyCode).toLowerCase();
            if (event.ctrlKey && event.altKey && pressedKey == "z") {
                __doPostBack('<%= imgbtnAdd.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "s") {
                __doPostBack('<%= imgbtnSave.UniqueID%>', '');
                return true;
            }
            if (event.ctrlKey && event.altKey && pressedKey == "q") {
                __doPostBack('<%= imgbtnBack.UniqueID%>', '');
                return true;
            }
        });
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Schedule Assignments" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save (ctrl + alt + s)" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back (ctrl + alt + q)" CausesValidation="false" />
                </div>
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHAssignmentNo" runat="server" Text="Assignment No"></asp:Label>
                                <asp:DropDownList ID="ddlAssignmentNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHPartner" runat="server" Text="** Partner"></asp:Label>
                                <asp:DropDownList ID="ddlPartner" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVPartner" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlPartner" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHFY" Text="** Financial Year" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHAY" Text="Assessment Year" runat="server"></asp:Label>
                                <br />
                                <asp:ListBox ID="lstAY" runat="server" Width="100%" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHCustomerName" runat="server" Text="** Customer Name"></asp:Label><asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New Customer (ctrl + alt + z)" Style="width: 25px;" />
                                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVCustomerName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustomerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHTask" runat="server" Text="** Assignment/Task"></asp:Label>
                                <asp:DropDownList ID="ddlTask" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVTask" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlTask" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHEmployee" runat="server" Text="* Employee"></asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVEmployee" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlEmployee" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHFrequency" runat="server" Text="* Frequency"></asp:Label>
                                <asp:DropDownList ID="ddlFrequency" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVFrequency" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlFrequency" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHDueDate" runat="server" Text="* Start Date"></asp:Label>
                                <asp:TextBox ID="txtDueDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDueDate" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDueDate" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclDueDate" runat="server" PopupButtonID="txtDueDate" PopupPosition="TopRight" TargetControlID="txtDueDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHExpectedCompletionDate" runat="server" Text="* Expected Completion Date"></asp:Label>
                                <asp:TextBox ID="txtExpectedCompletionDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExpectedCompletionDate" runat="server" ControlToValidate="txtExpectedCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExpectedCompletionDate" runat="server" ControlToValidate="txtExpectedCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclExpectedCompletionDate" runat="server" PopupButtonID="txtExpectedCompletionDate" PopupPosition="TopRight" TargetControlID="txtExpectedCompletionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHWorkStatus" runat="server" Text="* Work Status"></asp:Label>
                                <asp:DropDownList ID="ddlWorkStatus" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVWorkStatus" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlWorkStatus" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:Label ID="lblHDescription" runat="server" Text="* Description"></asp:Label>
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="97px">
                                </asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescription" runat="server" SetFocusOnError="True" ControlToValidate="txtDescription" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDescription" runat="server" ControlToValidate="txtDescription" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group">
                                <asp:CheckBox ID="chckAdvancePartialBilling" CssClass="aspxradiobutton" runat="server" Text="Advance/Partial Billing" AutoPostBack="false"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <div id="divAssistedByEmployees" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                                    <asp:GridView ID="gvAssistedByEmployees" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%" OnRowDataBound="OnRowDataBound">
                                        <HeaderStyle Font-Bold="True" BackColor="#cfd1d0" ForeColor="black" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAllAssistedByEmployees" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAssistedByEmployees_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectAssistedByEmployees" runat="server" CssClass="hvr-bounce-in" />
                                                    <asp:Label ID="lblAssistedByEmployeeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Usr_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FullName" HeaderText="Assisted By Employee" ItemStyle-Width="100%" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <div id="divYearMonth" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                                    <asp:GridView ID="gvYearMonth" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%" OnRowDataBound="OnRowDataBound">
                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAllYearMonth" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAllYearMonth_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectYearMonth" runat="server" CssClass="hvr-bounce-in" />
                                                    <asp:Label ID="lblYearMonthID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                                    <asp:Label ID="lblYearMonth" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Year/Month" ItemStyle-Width="100%" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <div id="divST" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                                    <asp:GridView ID="gvSubTask" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAllSubTask" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAllSubTask_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectSubTask" runat="server" CssClass="hvr-bounce-in" />
                                                    <asp:Label ID="lblSubTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AM_ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AM_Name" HeaderText="Sub Task" ItemStyle-Width="100%" />
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

    <%--<div class="col-sm-12 col-md-12">
        <div class="form-group">
            <asp:GridView ID="gvEmployeeSubTask" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="Task" HeaderText="Assignment/Task" ItemStyle-Width="19%" />
                    <asp:BoundField DataField="SubTask" HeaderText="Sub Task" ItemStyle-Width="19%" />
                    <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="12%" />
                    <asp:BoundField DataField="DueDate" HeaderText="Start Date" ItemStyle-Width="6%" />
                    <asp:BoundField DataField="ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="12%" />
                    <asp:TemplateField HeaderText="Work Status" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblWorkStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkStatus") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                            <asp:Label ID="lblSubTaskId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubTaskId") %>'></asp:Label>
                            <asp:Label ID="lblDBpkId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DBpkId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" ItemStyle-Width="2%" DeleteImageUrl="~/Images/Trash16.png" ControlStyle-CssClass="hvr-bounce-in" />
                </Columns>
            </asp:GridView>
        </div>
    </div>--%>
    <div id="ModalAAValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAAValidationMsg" runat="server"></asp:Label>
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
