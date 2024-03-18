<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EmployeeMaster.aspx.vb" Inherits="TRACePA.EmployeeMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
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
        /*tr:nth-child(even) {
            background-color: white;
        }*/
        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px;
            margin: 0px
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#gvEmployeeDetails').DataTable({
                fixedHeader: true
            });
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlStatus.ClientID%>').select2();
            $('#gvEmployeeDetails').DataTable({
                responsive: true
            });
            $('#<%=gvEmployeeDetails.ClientID%>').DataTable({
                //initComplete: function () {
                //    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                //},
                iDisplayLength: 10,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>

    <div class="col-sm-12 col-md-12" style="padding-left: 10px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Employee Master" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnUnBlock" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Unblock" />
                    <asp:ImageButton ID="imgbtnUnLock" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Unlock" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
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
            <div class="col-sm-12 col-md-12" style="padding-left: 10px">
                <div class="col-sm-4 col-md-4 divmargin" style="padding-left: 10px">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="275px">
                    </asp:DropDownList>
                </div>

            </div>


            <div class="col-sm-12 col-md-12" style="padding: 10px; overflow-x: scroll;">
                <asp:GridView ID="gvEmployeeDetails" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                   <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                <asp:Label ID="lblEmpID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmpID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="SAPCode" HeaderText="EMP Code" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="LoginName" HeaderText="Login Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Module" HeaderText="Module(Role)" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="LastLogin" HeaderText="LL Date" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Zone" HeaderText="Zone" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Region" HeaderText="Region" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Area" HeaderText="Area" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Branch" HeaderText="Branch" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Left" />
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <div id="ModalEmpMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
                <div class="modalmsg-dialog">
                    <div class="modalmsg-content">
                        <div class="modalmsg-header">
                            <h4 class="modal-title"><b>TRACe</b></h4>
                        </div>
                        <div class="modalmsg-body">
                            <div id="divMsgType" class="alert alert-info">
                                <p>
                                    <strong>
                                        <asp:Label ID="lblEmpMasterValidationMsg" runat="server"></asp:Label></strong>
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
        </div>
    </div>




    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
