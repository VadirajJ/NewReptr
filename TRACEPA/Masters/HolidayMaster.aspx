<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="HolidayMaster.aspx.vb" Inherits="TRACePA.HolidayMaster" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />
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
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlFinancialYear.ClientID%>').select2();

            $('#<%=gvHolidays.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [3] }, { bSearchable: false, aTargets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>

    <div class="col-sm-12 col-md-12">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Calender Master" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAddDays" CssClass="activeIcons hvr-bounce-out" data-toggle="tooltip" data-target="#myModal" data-placement="bottom" title="Add Holiday" runat="server" CausesValidation="False" />
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
            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblYears" runat="server" Text="Financial Year"></asp:Label>
                        <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblHeadingFromDate" runat="server" Text="From Date"></asp:Label>
                        <asp:TextBox ID="txtFromDate" runat="server" ReadOnly="true" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="lblHeadingTodate" runat="server" Text="To Date"></asp:Label>
                        <asp:TextBox ID="txtToDate" runat="server" ReadOnly="true" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3" style="padding-right: 0px;">
                    <div class="clearfix divmargin"></div>
                    <asp:CheckBox ID="chkCurrentYear" runat="server" TextAlign="Right" AutoPostBack="true" /><asp:Label ID="lblCurrentYear" runat="server" Text="Current Year"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lnkWeeklyOff" runat="server" OnClick="lnkWeeklyOff_Click" Font-Italic="true">Add Weekly off</asp:LinkButton>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium;">
                <div class="form-group">
                    <asp:GridView ID="gvHolidays" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="SrNo" HeaderText="Sl.No." ItemStyle-Width="7%" />
                            <asp:BoundField DataField="HolidayDate" HeaderText="Date" ItemStyle-Width="20%" />
                            <asp:BoundField DataField="Occasion" HeaderText="Occasion" ItemStyle-Width="70%" />
                            <asp:TemplateField HeaderText="" ItemStyle-Width="3%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" ToolTip="Delete Date" CssClass="hvr-bounce-in" />
                                    <asp:Label ID="lblDate" Visible="false" runat="server" Text='<%#Eval("HDFormat") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group pull-right">
                <asp:Label ID="lblFromDate" runat="server" Text="" Visible="false"></asp:Label>
                <asp:Label ID="lblToDate" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div id="ModalDeleteconfirmation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
                <div class="modalmsg-dialog">
                    <div class="modalmsg-content">
                        <div class="modalmsg-header">
                            <h4 class="modal-title"><b>TRACe</b></h4>
                        </div>
                        <div class="modalmsg-body">
                            <div id="divDeleteConfirm" class="alert alert-info">
                                <p>
                                    <strong>
                                        <asp:Label ID="lblConfirmDelete" runat="server"></asp:Label></strong>
                                </p>
                            </div>
                        </div>
                        <div class="modalmsg-footer">
                            <div class="modal-footer">
                                <div class="pull-right">
                                    <asp:Button runat="server" Text="Yes" class="btn-ok" ID="btnConfirmDelete" OnClick="btnConfirmDelete_Click"></asp:Button>
                                    <asp:Button runat="server" Text="No" class="btn-ok" ID="btnCancelDelete" data-dismiss="modal"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="ModalHoliday" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title"><b>Add Holiday Details</b></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group ">
                                <asp:Label ID="lblHolidayDate" runat="server" Text="* Select Holiday Date"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSelDate" runat="server" ControlToValidate="txtSelDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtSelDate" runat="server" CssClass="aspxcontrols" MaxLength="10" placeholder="dd/MM/yyyy"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSelDate" runat="server" ControlToValidate="txtSelDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtTargetDate" PopupPosition="BottomLeft"
                                    TargetControlID="txtSelDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label ID="lblOccasion" runat="server" Text="* Occasion"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOccasion" runat="server" ControlToValidate="txtOccasion" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtOccasion" runat="server" CssClass="aspxcontrols" MaxLength="500"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVOccasion" runat="server" ControlToValidate="txtOccasion" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Details"></asp:RegularExpressionValidator>
                            </div>
                            <br />
                            <div class="form-group">
                                <asp:Label ID="lblHMError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="form-group pull-right">
                                <asp:Button runat="server" Text="Add Holiday" class="btn-ok" ID="btnSaveHolidays" ValidationGroup="Details" OnClick="btnSaveHolidays_Click"></asp:Button>
                                <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCancel" OnClick="btnCancel_Click" ValidationGroup="false"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
     
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
