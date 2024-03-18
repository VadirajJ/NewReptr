<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AuditLog.aspx.vb" Inherits="TRACePA.AuditLog" %>


<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="css/styles.css" rel="stylesheet" />
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

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px;
            line-height: 1px
        }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlMaster.ClientID%>').select2();
            $('#<%=ddlUsers.ClientID%>').select2();

            $('#<%=gvAuditLog.ClientID%>').DataTable({
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
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>


    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Log Report" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Search24.png" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" src="~/Images/Download24.png" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
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

            <div class="col-sm-12 col-md-12" style="padding-left: 0px">

                <div class="col-sm-2 col-md-2">
                    <br />
                    <asp:RadioButton ID="rboModule" Text="User Log Details" AutoPostBack="true" GroupName="Select" runat="server" />
                </div>

                <div class="col-sm-2 col-md-2">
                    <br />
                    <asp:RadioButton ID="rboMaster" Text="Sub Module Operations" AutoPostBack="true" GroupName="Select" runat="server" />
                </div>

                <panel id="pnlMaster" runat="server" visible="false">
                    <div class="col-sm-2 col-md-2">
                        <div class="form-group">
                            <asp:Label ID="lblCustomerName" runat="server" Text="* Operations"></asp:Label>
                            <asp:DropDownList ID="ddlMaster" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                    </div>
                </panel>

                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text=" Users"></asp:Label>
                        <asp:DropDownList ID="ddlUsers" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblFromDate" runat="server" Text="From Date"></asp:Label>
                        <asp:TextBox ID="txtFromDate" placeholder="dd/MM/yyyy" runat="server" AutoCompleteType="Disabled" CssClass="aspxcontrols" MaxLength="10" AutoPostBack="true">   </asp:TextBox>
                        <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomRight"
                            TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="To Date"></asp:Label>
                        <asp:TextBox ID="txtToDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" AutoCompleteType="Disabled" MaxLength="10" AutoPostBack="true">   </asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtToDate" PopupPosition="BottomRight"
                            TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </cc1:CalendarExtender>
                    </div>
                </div>
            </div>

            <%--  <div class="col-sm-12 col-md-12">
       
    </div>--%>

            <div class="col-sm-12 col-md-12">
                <asp:GridView ID="gvAuditLog" ShowHeader="true" CssClass="table bs" Visible="false" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                        <asp:BoundField DataField="ModuleOperation" HeaderText="Module Operation" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="Activity" HeaderText="Activity" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="User" HeaderText="User" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="16%" />
                        <asp:BoundField DataField="AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="TaskName" HeaderText="Task Name" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="25%" />
                    </Columns>
                </asp:GridView>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:GridView ID="gvGeneral" ShowHeader="true" CssClass="table bs" runat="server" Visible="false" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" AllowPaging="false" Width="100%"></asp:GridView>
            </div>
        </div>
    </div> 
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

