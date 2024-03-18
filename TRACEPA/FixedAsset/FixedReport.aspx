<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FixedReport.aspx.vb" Inherits="TRACePA.FixedReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />

    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
  
<script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstLocation.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });
        });

<%--        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=ddlReportType.ClientID%>').select2();
        });--%>

    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=lstLocDynamic.ClientID%>').multiselect({
                maxHeight: 200,
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });

            $('#<%=lstDivision.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });

            $('#<%=lstDepartment.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });

            $('#<%=lstBay.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });
        });

<%--        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=ddlpAstype.ClientID%>').select2();
            $('#<%=ddlTransType.ClientID%>').select2();
            $('#<%=ddlYear.ClientID%>').select2();
        });

        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }--%>

    </script>


    <div class="loader"></div>


    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>



    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                   
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Report" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" />
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" ValidationGroup="Validate" />


                </div>
            </div>
            </div>
        <div class="card">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-2 col-md-2">
                    <asp:Label ID="lblLocation" runat="server" Visible="false" Text=" Location"></asp:Label>
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <asp:ListBox ID="lstLocation" runat="server" Visible="false" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2 col-md-2" style="visibility: hidden;">
                    <div class="form-group">
                        <asp:Label ID="Label4" Text="Amount in" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlAmountConvert" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="In Hundreds" Value="100"></asp:ListItem>
                            <asp:ListItem Text="In Thousands" Value="1000"></asp:ListItem>
                            <asp:ListItem Text="In Lakhs" Value="100000"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <%--  <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                <div class="col-sm-2 col-md-2">
                    <asp:Label ID="lblLocation" runat="server" Visible="false" Text=" Location"></asp:Label>
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <asp:ListBox ID="lstLocation" runat="server" Visible="false" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <br>
                <div class="col-sm-1 col-md-1" style="padding-left: 0%">
                    <asp:Button ID="btnGo" CssClass="btn-ok" Text="Ok" OnClientClick="showProgress()" Font-Overline="False" runat="server"></asp:Button>
                </div>
            </div>--%>
        

    <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server">
        <div id="tabFix" runat="server">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li id="liEmpBasic" class="active" runat="server">
                    <asp:LinkButton ID="lnkbtnEmpBasicDetails" Text="Report" runat="server" Font-Size="11px" Font-Bold="true" /></li>
                <li id="liEmpMaster" runat="server">
                    <asp:LinkButton ID="lnkbtnEmpMaster" Text="Dynamic Report" runat="server" Font-Size="11px" Font-Bold="true" /></li>
                <li id="liEmpDetails" runat="server">
                    <asp:LinkButton ID="lnkbtnEmpDetails" Text="AssetWise Report" runat="server" Font-Size="11px" Font-Bold="true" /></li>

            </ul>
        </div>
        <!-- Tab panes -->





        <div class="tab-content divmargin">
            <div runat="server" role="tabpanel" class="tab-pane active" id="divEmpBasic">
                 <div class="col-sm-12 col-md-12 form-group">
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Report Type"></asp:Label>
                    <asp:DropDownList ID="ddlReportType" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        <asp:ListItem Value="0">Select Type</asp:ListItem>
                        <asp:ListItem Value="1">As Per Company Act.</asp:ListItem>
                        <asp:ListItem Value="2">As Per Income Tax.</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label ID="Label1" Text="Note No" runat="server"></asp:Label>
                        <asp:TextBox ID="txtNoteNumber" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:TextBox>
                    </div>
                </div>
                     <div class="col-sm-3 col-md-3">
                          <br />
                 <asp:Button ID="btnGoReport" CssClass="btn-ok" Text="Ok" OnClientClick="showProgress()" Font-Overline="False" runat="server"></asp:Button>
                         </div>
                     </div>
                <div class="col-sm-12 col-md-12 divmargin">
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
                </div>
            
                </div>





            <div runat="server" role="tabpanel" class="tab-pane" id="divEmpMaster">
               <asp:Panel ID="pnlLocation" runat="server" Visible="true">
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

                        <div class="col-sm-2 col-md-2">
                            <asp:Label ID="Label2" runat="server" Text=" Location"></asp:Label>
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <asp:ListBox ID="lstLocDynamic" AutoPostBack="true" runat="server" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label ID="Label3" runat="server" Text=" Division"></asp:Label>
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <asp:ListBox ID="lstDivision" runat="server" AutoPostBack="true" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label ID="Label5" runat="server" Text=" Department"></asp:Label>
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <asp:ListBox ID="lstDepartment" runat="server" AutoPostBack="true" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>

                                </div>
                            </div>
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label ID="Label6" runat="server" Text=" Bay"></asp:Label>
                            <div class="row">
                                <div class="col-sm-12 col-md-12">
                                    <asp:ListBox ID="lstBay" runat="server" AutoPostBack="true" SelectionMode="Multiple" CssClass="aspxcontrols"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
              <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Asset Class"></asp:Label>
                        <asp:DropDownList ID="ddlpAstype" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3 col-lg-3">
                        <asp:Panel ID="pnlTrans" runat="server">
                            <asp:Label runat="server" Text="* Transaction Type"></asp:Label>
                            <asp:DropDownList ID="ddlTransType" runat="server" CssClass="aspxcontrols">
                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Opening Balance" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Addition" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Deletion" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                   <div class="col-sm-1 col-md-1">
     <br />
     <asp:Button runat="server" CssClass="btn-ok" data-toggle="tooltip" data-placement="bottom" Text="Go" title="Go" ID="btnGoDynamic" />
 </div>
                    <div class="col-sm-3 col-md-3" style="visibility: hidden;">
                        <div class="form-group">
                            <asp:Label ID="Label8" Text="Amount in" runat="server"></asp:Label>
                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="In Hundreds" Value="100"></asp:ListItem>
                                <asp:ListItem Text="In Thousands" Value="1000"></asp:ListItem>
                                <asp:ListItem Text="In Lakhs" Value="100000"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                           
                            </div>
                 
                <div class="col-sm-12 col-md-12 divmargin">
    <rsweb:ReportViewer ID="ReportViewer2" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
</div>
</div>

            <div runat="server" role="tabpanel" class="tab-pane" id="divEmpDetails">
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="* Location"></asp:Label>
                        <asp:DropDownList ID="ddlLocatn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Division"></asp:Label>
                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Department"></asp:Label>
                        <asp:DropDownList ID="ddlDeptmnt" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Bay"></asp:Label>
                        <asp:DropDownList ID="ddlBay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Asset Class"></asp:Label>
                        <asp:DropDownList ID="ddlpAstypeInv" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Asset"></asp:Label>
                        <asp:DropDownList ID="ddlAssetItem" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2 col-md-2" style="visibility: hidden;">
                        <div class="form-group">
                            <asp:Label ID="Label9" Text="Amount in" runat="server"></asp:Label>
                            <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="In Hundreds" Value="100"></asp:ListItem>
                                <asp:ListItem Text="In Thousands" Value="1000" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="In Lakhs" Value="100000"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                            <div class="col-sm-1 col-md-1">
                                <br />
                                <asp:Button runat="server" data-toggle="tooltip" CssClass="btn-ok" OnClientClick="showProgress()" data-placement="bottom" Text="Go" title="Go" ID="btnInv" />
                            </div>


                    <div class="col-sm-3 col-md-3 col-lg-3">
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <asp:Label runat="server" Text="* Transaction Type"></asp:Label>
                            <asp:DropDownList ID="DropDownList5" runat="server" CssClass="aspxcontrols">
                                <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Opening Balance" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Addition" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Deletion" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
                    <rsweb:ReportViewer ID="ReportViewer3" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </div>
    </div>
</div>

    <div id="ModalReportValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgUploadType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblReportValidation" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" Text="Ok" class="btn-ok" ID="btnOk"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <button type="button" class="close" id="btnClose" data-dismiss="modal">&times;</button>
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModal" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn-ok" />
                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
