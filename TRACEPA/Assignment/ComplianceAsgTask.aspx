<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ComplianceAsgtask.aspx.vb" Inherits="TRACePA.ComplianceAsgtasknew" %>

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
            $('#<%=ddlComplianceCustomerName.ClientID%>').select2();
            $('#<%=ddlCompliancePartner.ClientID%>').select2();
            $('#<%=ddlComplainceEmployee.ClientID%>').select2();
            $('#<%=ddlAct.ClientID%>').select2();
            $('#<%=gvUnAssigned.ClientID%>').DataTable({
                initComplete: function () {
                    //$(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 10,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvAssigned.ClientID%>').DataTable({
                initComplete: function () {
                    //$(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 10,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Compliance Task" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAssign" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Assign" Visible="false" />
                </div>
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:RadioButton ID="rboUnAssigned" runat="server" Text="UnAssigned" GroupName="Assigned" Checked="True" AutoPostBack="True" />
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:RadioButton ID="rboAssigned" runat="server" Text="Assigned" GroupName="Assigned" AutoPostBack="True" />
                            </div>
                        </div>
                        <asp:Label ID="lblSelectedTaskID" Visible="false" runat="server" Visbile="False"></asp:Label>
                        <asp:Label ID="lblSelectedCustID" Visible="false" runat="server" Visbile="False"></asp:Label>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label3" Text="Financial Year" runat="server"></asp:Label>
                                <asp:DropDownList Width="100%" ID="ddlComplianceFinancialYear" runat="server" CssClass="aspxcontrols" AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHComplianceCustomer" runat="server" Text="Customer"></asp:Label>
                                <asp:DropDownList Width="100%" ID="ddlComplianceCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblHFrequency" runat="server" Text="* Frequency"></asp:Label>
                                <asp:DropDownList ID="ddlFrequency" Width="100%" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <div class="form-group">
                                <div runat="server" id="divFrequenctDetails" visible="False">
                                    <asp:Label ID="lblHFrequenctDetails" runat="server"></asp:Label>
                                    <asp:DropDownList Width="100%" ID="ddlFrequencyDetails" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-2 col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="lblHAct" runat="server" Text="Act"></asp:Label>
                                    <asp:DropDownList Width="100%" ID="ddlAct" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div runat="server" id="divAssigned">
                                <div class="col-sm-2 col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblHPartner" runat="server" Text="Partner"></asp:Label>
                                        <asp:DropDownList Width="100%" ID="ddlCompliancePartner" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="lblHEmployee" runat="server" Text="Employee"></asp:Label>
                                        <asp:DropDownList Width="100%" ID="ddlComplainceEmployee" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: scroll">
                            <div class="form-group">
                                <asp:GridView ID="gvUnAssigned" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="3%">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelectAllTask" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectTask_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectTask" runat="server" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="4%" />
                                        <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="Act" HeaderText="Act" ItemStyle-Width="30%" />
                                        <asp:BoundField DataField="Task" HeaderText="Assignment/Task" ItemStyle-Width="30%" />
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="3%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerID") %>'></asp:Label>
                                                <asp:Label ID="lblTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaskID") %>'></asp:Label>
                                                <asp:ImageButton ID="imgbtnEdit" ToolTip="Assign" CommandName="AssignRow" runat="server" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="gvAssigned" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="2%" />
                                        <asp:BoundField DataField="AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="15%" />
                                        <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="25%" />
                                        <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="8%" />
                                        <asp:BoundField DataField="Act" HeaderText="Act" ItemStyle-Width="8%" />
                                        <asp:BoundField DataField="Task" HeaderText="Assignment/Task" ItemStyle-Width="15%" />
                                        <asp:BoundField DataField="Employee" HeaderText="Assigned To" ItemStyle-Width="9%" />
                                        <asp:BoundField DataField="DueDate" HeaderText="Start Date" ItemStyle-Width="6%" />
                                        <asp:BoundField DataField="ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="12%" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myAssignModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Assign Task</b></h4>
                    <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body" style="height: auto">
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <asp:Label ID="lblATError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHTaskPartner" runat="server" Text="* Partner"></asp:Label>
                                <asp:DropDownList ID="ddlTaskPartner" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVTaskPartner" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlTaskPartner" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6" style="padding-right: 0px;">
                            <div class="form-group">
                                <asp:Label ID="lblHTaskEmployee" runat="server" Text="* Employee"></asp:Label>
                                <asp:DropDownList ID="ddlTaskEmployee" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVTaskEmployee" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlTaskEmployee" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHDueDate" runat="server" Text="* Start Date"></asp:Label>
                                <asp:TextBox ID="txtDueDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDueDate" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDueDate" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclDueDate" runat="server" PopupButtonID="txtDueDate" PopupPosition="TopRight" TargetControlID="txtDueDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblHExpectedCompletionDate" runat="server" Text="* Expected Completion Date"></asp:Label>
                                <asp:TextBox ID="txtExpectedCompletionDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVExpectedCompletionDate" runat="server" ControlToValidate="txtExpectedCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVExpectedCompletionDate" runat="server" ControlToValidate="txtExpectedCompletionDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="cclExpectedCompletionDate" runat="server" PopupButtonID="txtExpectedCompletionDate" PopupPosition="TopRight" TargetControlID="txtExpectedCompletionDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" Text="* Work Status"></asp:Label>
                                <asp:DropDownList ID="ddlTaskWorkStatus" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVTaskWorkStatus" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlTaskWorkStatus" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AssignTask"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6" style="padding-right: 0px;">
                            <div class="form-group">
                                <div id="divAssistedByEmployees" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                                    <asp:GridView ID="gvAssistedByEmployees" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
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
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div id="divST" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; height: auto;">
                            <asp:GridView ID="gvSubTask" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
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
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Assign" class="btn-ok" ID="btnAssign" ValidationGroup="AssignTask" OnClick="btnAssignTask_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalComplianceAsgTaskValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblComplianceAsgTaskValidationMsg" runat="server"></asp:Label>
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
