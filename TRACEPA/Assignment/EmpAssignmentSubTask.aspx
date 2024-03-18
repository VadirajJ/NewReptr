<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmpAssignmentSubTask.aspx.vb" Inherits="TRACePA.EmpAssignmentSubTask" %>

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
            $('#<%=ddlAssignmentNo.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlPartner.ClientID%>').select2();
            $('#<%=ddlTask.ClientID%>').select2();
            $('#<%=lstAssistedByEmployees.ClientID%>').multiselect({
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
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Task Assignments</b></h2>
            </div>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" Visible="false" title="Save" ValidationGroup="Validate" />
                <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" Visible="false" title="Attachment" OnClick="btnAttachment_Click" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0" Visible="false"></asp:Label></span>
                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        <asp:Label ID="lblAsgID" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="lblTaskSubTaskId" runat="server" Visible="false"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3" style="padding-right: 0px">
            <div class="form-group">
                <asp:Label ID="lblHAssignmentNo" runat="server" Text="Assignment No"></asp:Label>
                <asp:DropDownList ID="ddlAssignmentNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <br />
                <asp:CheckBox ID="chckAdvancePartialBilling" CssClass="aspxradiobutton" runat="server" Text="Advance/Partial Billing" AutoPostBack="true"></asp:CheckBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding-right: 0px;">
            <div class="form-group">
                <asp:Label ID="lblHFolderPath" runat="server" Text="Folder Path"></asp:Label>
                <asp:TextBox ID="txtFolderPath" runat="server" CssClass="aspxcontrols" MaxLength="499">
                </asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding-right: 0px;">
            <div class="form-group">
                <asp:Label ID="lblHAssistedByEmployees" runat="server" Text="Assisted By Employee"></asp:Label>
                <br />
                <asp:ListBox ID="lstAssistedByEmployees" runat="server" Width="100%" Font-Size="10px" AutoPostBack="true" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-3 col-md-3" style="padding-right: 0px">
            <div class="form-group">
                <asp:Label ID="lblHFY" Text="Financial Year" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding-right: 0px;">
            <div class="form-group">
                <asp:Label ID="lblHCustomerName" runat="server" Text="Customer"></asp:Label>
                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding-right: 0px">
            <div class="form-group">
                <asp:Label ID="lblHPartner" runat="server" Text="Partner"></asp:Label>
                <asp:DropDownList ID="ddlPartner" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3" style="padding-right: 0px">
            <div class="form-group">
                <asp:Label ID="lblHTask" runat="server" Text="Assignment/Task"></asp:Label>
                <asp:DropDownList ID="ddlTask" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <asp:LinkButton ID="lnkbtnAddUpdateSubTask" runat="server">Add/Update Sub Task</asp:LinkButton>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
            <asp:GridView ID="gvAssignmentSubTask" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvAssignmentSubTask_RowDataBound">
               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkSelectAllAsgSubTask" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelectAllAsgSubTask_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelectAsgSubTask" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                    <asp:BoundField DataField="CustomerName" HeaderText="Customer" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="07%" />
                    <asp:BoundField DataField="Task" HeaderText="Assignment/Task" ItemStyle-Width="13%" />
                    <asp:TemplateField HeaderText="Sub Task" ItemStyle-Width="14%">
                        <ItemTemplate>
                            <asp:Label ID="lblDBpkId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DBpkId") %>'></asp:Label>
                            <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                            <asp:Label ID="lblSubTaskId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubTaskId") %>'></asp:Label>
                            <asp:Label ID="lblWorkStatusId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WorkStatusId") %>'></asp:Label>
                            <asp:LinkButton ID="lnkSubTask" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "SubTask") %>'></asp:LinkButton>
                            <asp:Label ID="lblSubTask" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubTask") %>'></asp:Label>
                            <asp:Label ID="lblClosed" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Closed") %>'></asp:Label>
                            <asp:Label ID="lblReview" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Review") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Employee" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblEmployeeId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmployeeId") %>'></asp:Label>
                            <asp:Label ID="lblEmployee" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Employee") %>'></asp:Label>
                            <asp:Label ID="lblAssistedBy" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssistedBy") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Start Date" ItemStyle-Width="06%">
                        <ItemTemplate>
                            <asp:Label ID="lblDueDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DueDate") %>'></asp:Label>
                            <asp:Label ID="lblFrequency" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Frequency") %>'></asp:Label>
                            <asp:Label ID="lblCreatedBy" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedBy") %>'></asp:Label>
                            <asp:Label ID="lblCreatedOn" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="12%" />
                    <asp:BoundField DataField="WorkStatus" HeaderText="Work Status" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-Width="15%" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="form-group">
            <asp:Button ID="btnLoadEmpDetails" runat="server" CssClass="btn-ok" Text="Load" Font-Bold="true" data-toggle="tooltip" data-placement="top" Visible="false"></asp:Button>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" id="divEmpDetails" runat="server" visible="false" style="padding: 0px">
        <div class="col-sm-12 col-md-12">
            <div class="form-group">
                <asp:Label ID="lblHSubTask" runat="server" Text="Sub Task : "></asp:Label>
                <asp:Label ID="lblSubTaskName" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
            <div class="form-group">
                <asp:Label ID="lblHEmployee" runat="server" Text="Employee : "></asp:Label>
                <asp:Label ID="lblEmployeeName" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblHWorkStatus" runat="server" Text="* Work Status"></asp:Label>
                        <asp:DropDownList ID="ddlWorkStatus" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVWorkStatus" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlWorkStatus" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblHDate" runat="server" Text="* Date"></asp:Label>
                        <asp:TextBox ID="txtDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtDate" PopupPosition="TopRight" TargetControlID="txtDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblHComments" runat="server" Text="* Comments"></asp:Label>
                    <asp:TextBox ID="txtComments" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="75px">
                    </asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVComments" runat="server" SetFocusOnError="True" ControlToValidate="txtComments" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVComments" runat="server" ControlToValidate="txtComments" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <br />
                    <div class="col-sm-5 col-md-5" style="padding: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblHFrequency" runat="server" Text="Frequency : "></asp:Label>
                            <asp:Label ID="lblFrequencyName" runat="server" CssClass="aspxlabelbold"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-5 col-md-5" style="padding: 0px">
                        <div class="form-group">
                            <asp:CheckBox ID="chkReview" runat="server" Text="Review request for Partner"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="col-sm-2 col-md-2" style="padding: 0px; visibility: hidden;">
                        <div class="form-group">
                            <asp:CheckBox ID="chkClose" runat="server" Text="Close" Visible="false"></asp:CheckBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblHCreatedBy" runat="server" Text="Created By : "></asp:Label>
                        <asp:Label ID="lblCreatedByName" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblHCreatedOn" runat="server" Text="Created On : "></asp:Label>
                        <asp:Label ID="lblCreatedDate" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
            <asp:GridView ID="gvEmpAssignmentSubTask" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="03%" />
                    <asp:BoundField DataField="SubTask" HeaderText="Sub Task" ItemStyle-Width="20%" />
                    <asp:BoundField DataField="Employee" HeaderText="Employee" ItemStyle-Width="16%" />
                    <asp:BoundField DataField="WorkStatus" HeaderText="Work Status" ItemStyle-Width="12%" />
                    <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="50%" />
                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="09%" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
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
    <div id="myModalMainAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Attachment</b></h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
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
                                            <b>By :</b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On :</b>
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
    <div id="myAddUpdateSubTaskModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Add/Update Sub Task</b></h4>
                    <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body" style="height: auto">
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <asp:Label ID="lblSTError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div id="divST" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto; height: auto;">
                            <asp:GridView ID="gvSubTask" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelectSubTask" runat="server" CssClass="hvr-bounce-in" />
                                            <asp:Label ID="lblgvSubTaskID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AM_ID") %>'></asp:Label>
                                            <asp:Label ID="lblgvDBpkId" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvDescription" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvDueDate" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvExpectedCompletionDate" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvEmployeeId" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvFrequencyId" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvYearOrMonthId" Visible="false" runat="server"></asp:Label>
                                            <asp:Label ID="lblgvWorkStatusId" Visible="false" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AM_Name" HeaderText="Sub Task" ItemStyle-Width="65%" />
                                    <asp:TemplateField HeaderText="Work Status">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlSTWorkStatus" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" Text="Add/Update Sub Task" class="btn-ok" ID="btnAddUpdateSubTask"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

