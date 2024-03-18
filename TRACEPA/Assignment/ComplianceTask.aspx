<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ComplianceTask.aspx.vb" Inherits="TRACePA.ComplianceTask" %>
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
        .grid-container {
            display: grid;
            grid-template-columns: auto auto auto auto auto;
            gap: 15px;
            padding: 15px 15px 0px 15px;
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
            $('#<%=ddlCustomer.ClientID%>').select2();
            $('#<%=ddlPartner.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Compliance Task</b></h2>
            </div>
            <div class="pull-right">
                 <asp:ImageButton ID="imgbtnLoad" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Load" ValidationGroup="Validate" />
                <ul class="nav navbar-nav navbar-right logoutDropdown">
                    <li class="dropdown">
                        <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                            <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                            <li role="separator" class="divider"></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 grid-container">
        <div style="background-color: white; border-color: #444444; border: solid 1px; border-radius: 5px; text-align: center;">
            <div>
                <h2><b><asp:Label ID="lblAllTasks" runat="server" Text="0"></asp:Label></b></h2>
            </div>
            <div>
                <h5><b><asp:Label runat="server" ForeColor="#95B589" Text="All"></asp:Label></b></h5>
            </div>            
        </div>
        <div style="background-color: white; border-color: #444444; border: solid 1px; border-radius: 5px; text-align: center;">
            <div>
                <h2><b><asp:Label ID="lblYetToStartTasks" runat="server" Text="0"></asp:Label></b></h2>
            </div>
            <div>
                <h5><b><asp:Label runat="server" ForeColor="#95B589" Text="Yet To Start"></asp:Label></b></h5>
            </div>            
        </div>
        <div style="background-color: white; border-color: #444444; border: solid 1px; border-radius: 5px; text-align: center;">
           <div>
                <h2><b><asp:Label ID="lblInProgressTasks" runat="server" Text="0"></asp:Label></b></h2>
            </div>
            <div>
                <h5><b><asp:Label runat="server" ForeColor="#95B589" Text="In Progress"></asp:Label></b></h5>
            </div>            
        </div>
        <div style="background-color: white; border-color: #444444; border: solid 1px; border-radius: 5px; text-align: center;">
            <div>
                <h2><b><asp:Label ID="lblCompletedTasks" runat="server" Text="0"></asp:Label></b></h2>
            </div>
            <div>
                <h5><b><asp:Label runat="server" ForeColor="#95B589" Text="Completed"></asp:Label></b></h5>
            </div>            
        </div>
        <div style="background-color: white; border-color: #444444; border: solid 1px; border-radius: 5px; text-align: center;">
            <div>
                <h2><b><asp:Label ID="lblOverDueTasks" runat="server" Text="0"></asp:Label></b></h2>
            </div>
            <div>
                <h5><b><asp:Label runat="server" ForeColor="#95B589" Text="Overdue"></asp:Label></b></h5>
            </div>            
        </div>
        <br />
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <br />
                <asp:RadioButton ID="rboOverdue" runat="server" Text="Overdue" GroupName="Task" Checked="True" AutoPostBack="True" Width="80px" />
                <asp:RadioButton ID="rboUpcoming" runat="server" Text="Upcoming" GroupName="Task" AutoPostBack="True" Width="80px" />
                <asp:RadioButton ID="rboCompletedTasks" runat="server" Text="Completed" GroupName="Task" AutoPostBack="True" Width="80px" />
                <asp:RadioButton ID="rboWIP" runat="server" Text="WIP" GroupName="Task" AutoPostBack="True"/>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblHCustomer" runat="server" Text="Customer Name"></asp:Label>
                <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label ID="lblHPartner" runat="server" Text="Partner Name"></asp:Label>
                <asp:DropDownList ID="ddlPartner" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label ID="lblHFromDate" runat="server" Text="From Date"></asp:Label>
                <asp:TextBox ID="txtFromDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFromDate" runat="server" ControlToValidate="txtFromDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomRight" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label ID="lblHToDate" runat="server" Text="To Date"></asp:Label>
                <asp:TextBox ID="txtToDate" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVToDate" runat="server" ControlToValidate="txtToDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <cc1:CalendarExtender ID="cclToDate" runat="server" PopupButtonID="txtToDate" PopupPosition="BottomRight" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium;">
        <div class="form-group">
            <asp:GridView ID="gvDetails" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="3%" />
                    <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="22%" />
                    <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="12%" />
                    <asp:BoundField DataField="AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="18%" />
                    <asp:BoundField DataField="SubTask" HeaderText="Sub Task" ItemStyle-Width="22%" />
                    <asp:BoundField DataField="DueDate" HeaderText="Start Date" ItemStyle-Width="8%" />
                    <asp:TemplateField HeaderText="Assignment No" ItemStyle-Width="15%">
                        <HeaderTemplate>
                            <asp:Label ID="lblHExpectedCompletionDate" runat="server"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblExpectedCompletionDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ExpectedCompletionDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px;">
        <div class="col-sm-8 col-md-8" style="padding-right: 0px;">
            <div class="form-group">
                <asp:Chart ID="ComplianceStackChart" runat="server" Width="900px" Height="350px" ToolTip="Compliance - Monthly" BorderlineColor="Gray">
                    <Legends>
                        <asp:Legend Name="LegendStack" Docking="Top" Alignment="Center" TitleSeparator="Line" Title="Customer Compliance - Monthly" TitleFont="TimesNewRoman" LegendStyle="Table" LegendItemOrder="SameAsSeriesOrder"/>
                    </Legends>
                    <Series>
                        <asp:Series Name="On-Time Compliance" IsValueShownAsLabel="false" ChartType="StackedColumn" Color="#4BC0C0"></asp:Series>
                        <asp:Series Name="Delayed Compliance" IsValueShownAsLabel="false" ChartType="StackedColumn" Color="#87C7F2"></asp:Series>
                        <asp:Series Name="Non Compliance" IsValueShownAsLabel="false" ChartType="StackedColumn" Color="#FF6D80"></asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                            <AxisX>
                                <MajorGrid Enabled="false" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-right: 0px;">
            <asp:Chart ID="CompliancePieChart" runat="server" Height="350px" ToolTip="Compliance - Yearly" BorderlineColor="Gray">
                <Legends>
                     <asp:Legend Name="LegendPie" Docking="Top" Alignment="Center" TitleSeparator="Line" Title="Customer Compliance - Yearly" TitleFont="TimesNewRoman" LegendStyle="Table" LegendItemOrder="SameAsSeriesOrder"/>
                </Legends>    
                <Series>
                        <asp:Series Name="Default" IsValueShownAsLabel="false"></asp:Series>
                    </Series>
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1" BorderWidth="0" />
                </ChartAreas>
            </asp:Chart>
        </div>
    </div>
    <div id="ModalComplianceTaskValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblComplianceTaskValidationMsg" runat="server"></asp:Label>
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