<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="UploadTrailbalanceSchedule.aspx.vb" Inherits="TRACePA.UploadTrailbalanceSchedule" %>

<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <style>
        /* .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }*/

        #overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: white;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>
    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
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
            $('#<%=ddlSheetNameSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlUploadType.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlSheetName.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustName.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustNameSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlscheduletypeSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlFinancialYearSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=DdlbranchSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlHeadingSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlsubheadingSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlitemsSchedTemp.ClientID%>').select2();
        });
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlSUbItemsSchedTemp.ClientID%>').select2();
        });
        $(window).load(function () {
            // PAGE IS FULLY LOADED  
            // FADE OUT YOUR OVERLAYING DIV
            $('#overlay').fadeOut();
            var updateProgress = $get("<%= BodyContent.ClientID %>");
            updateProgress.style.display = "block";
        });
    </script>

    <script type="text/javascript">
        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=dgGeneralSchedTemp.ClientID%>').DataTable({
               
            });
        });
        <%--$(document).ready(function () {
            $('#<%=gvddlSubitem.ClientID%>').select2();
            $('#<%=gvddlitem.ClientID%>').select2();
            $('#<%=gvddlSubheading.ClientID%>').select2();
            $('#<%=gvddlheading.ClientID%>').select2();
        });--%>
    </script>
    <div id="BodyContent" runat="server">
        <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
            <div class="card">
                <div runat="server" id="divCompheader" class="card-header">
                    <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                    <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                    <asp:Label runat="server" ID="Label3" CssClass="form-label" Font-Bold="true" Text="Schedule Template Mapping" Font-Size="Small"></asp:Label>
                    <div class="pull-right" style="padding-right: 15px;">
                        <asp:Button runat="server" ID="btnViewVersion" Text="View Version" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok " Visible="false" />
                        <asp:Button runat="server" ID="btnVersion" Text="Add Version" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok " Visible="false" />
                        <asp:Button runat="server" ID="btnPartner" Text="Partner's Share" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok " Visible="false" />
                        <asp:Button runat="server" ID="btnCashflow" Visible="false" Text="Cash Flow" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok" />
                        <asp:Button runat="server" ID="btnTrade" Text="Trade" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok " />
                        <asp:Button runat="server" ID="btnAccRatio" Text="Accounting Ratios" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok " />
                        <asp:Button runat="server" ID="btnAddNote" Text="Add Note" BorderColor="Yellow" ForeColor="Green" CssClass="btn-ok" />
                        <asp:ImageButton ID="imgbtnSaveSchedTemp" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                        <asp:ImageButton ID="imgbtnBackSchedTemp" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />
                        <asp:ImageButton ID="ImgbtnApproveSchedTemp" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                        <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                            <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnPDF" Visible="false" Text="Download PDF" Style="margin: 0px;" /></li>
                            <li role="separator" class="divider"></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="DataExport.xlsx" Style="margin: 0px;" /></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnExcelData" Text="Uploadable_Data_Sheet.xlsx" Style="margin: 0px;" /></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="card">
                <div id="divComplianceBody" runat="server" clientidmode="Static">
                    <div class="card-body">
                        <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div6" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                            <div class="col-sm-12 col-md-12 divmargin visually-hidden">
                                <div class="pull-left divmargin col-sm-4 col-md-4 col-lg-4">
                                    <asp:DropDownList ID="ddlUploadType" runat="server" Visible="false" AutoPostBack="true" Font-Bold="true" CssClass="aspxcontrols">
                                        <asp:ListItem Value="0">Select Upload type</asp:ListItem>
                                        <asp:ListItem Value="1">Upload trial balance for Schedule</asp:ListItem>
                                        <asp:ListItem Value="2">Upload closing stock Entry</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:Panel runat="server" ID="pnlSchedTemp" Visible="false">
                                <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                                    <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                    <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                                </div>
                                <div class="col-sm-12 col-md-12" style="padding: 0px">
                                    <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                                        <a href="#">
                                            <div id="divcollapseRRIT" visible="false" runat="server" data-toggle="collapse" data-target="#collapseRRIT"><b><i>Click here to view Sample Format...</i></b></div>
                                        </a>
                                    </div>

                                </div>
                                <div class="form-group divmargin"></div>
                                <div id="collapseRRIT" class="collapse">
                                    <div class="col-sm-12 col-md-12" style="padding: 0px;">
                                        <div class="form-group">
                                            <asp:DataGrid ID="dgSampleFormatSchedTemp" runat="server" AutoGenerateColumns="true" Width="100%" class="footable">
                                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:DataGrid>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                                    <div class="col-sm-3 col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lblCustName" runat="server" Text="* Customer Name"></asp:Label>
                                            <asp:DropDownList ID="ddlCustNameSchedTemp" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="lblMsg" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lblYears" runat="server" Text="Financial Year"></asp:Label>
                                            <asp:DropDownList ID="ddlFinancialYearSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6 pull-right">
                                        <div class="form-group pull-right">
                                            <a href="#">
                                                <asp:LinkButton ID="lnkDownloadSchedTemp" runat="server"><b><i>Click here to Download Uploadable Excel</i></b></asp:LinkButton></a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 col-md-12" style="padding: 0px; padding-right: 0; word-break: break-all">
                                    <div class="col-sm-3 col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="LblBranchNameSchedTemp" runat="server" Text="* Branch Name"></asp:Label>
                                            <asp:DropDownList ID="DdlbranchSchedTemp" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3" style="padding: 0px;">
                                        <div class="col-sm-7 col-md-7 col-lg-7">
                                            <div class="form-group">
                                                <asp:Label ID="lblscheduletypeSchedTemp" Text="Schedule Type" runat="server" Enabled="false"></asp:Label>
                                                <asp:DropDownList ID="ddlscheduletypeSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Enabled="false">
                                                    <asp:ListItem Value="0">Select Scheduler type</asp:ListItem>
                                                    <asp:ListItem Value="3">P & L </asp:ListItem>
                                                    <asp:ListItem Value="4">Balance Sheet</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-5 col-md-5" style="padding-left: 15px">
                                            <asp:Label runat="server" Text="Unmapped Excel"></asp:Label><br />
                                            &nbsp;&nbsp;&nbsp;
                                            <label class="switch" runat="server">
                                                <asp:CheckBox ID="chkBxExcel" runat="server" Checked="false" />
                                                <span class="slider round"></span>
                                            </label>
                                        </div>
                                    </div>

                                    <div class="col-sm-3 col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="lblSelectFileSchedTemp" runat="server" Text="Select Excel File(Trail Balance)"></asp:Label>
                                            <asp:FileUpload ID="FULoadSchedTemp" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
                                        </div>
                                        <asp:TextBox ID="txtPathSchedTemp" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" runat="server" Text="Upload Excel File"></asp:Label><br />
                                            <button id="btnOkSchedTemp" bordercolor="Blue" forecolor="Green" class="btn-ok" runat="server" style="width: 31%"><i class="fa-solid fa-arrow-up-from-bracket fa-lg"></i></button>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-3 col-md-3" style="display: none">
                                    <div class="form-group">
                                        <asp:Label ID="lblSheetNameSchedTemp" runat="server" Text="Sheet Name" Visible="false"></asp:Label>
                                        <asp:DropDownList ID="ddlSheetNameSchedTemp" runat="server" AutoPostBack="true" Visible="false" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-12 col-md-12 col-lg-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" Text="Heading" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlHeadingSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" Text="Sub Heading Name" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlsubheadingSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" Text="Item Under Sub-Heading" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlitemsSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3 col-lg-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" Text="Sub items under items" runat="server"></asp:Label>
                                            <asp:DropDownList ID="ddlSUbItemsSchedTemp" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div id="overlay">
                                    <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                                        <img alt="" src="/Images/pageloader.gif" />
                                    </div>
                                </div>
                                <div class="col-sm-3 col-md-3 col-lg-3">
                                    <label class="switch">
                                        <asp:CheckBox ID="chkOnOffSchedTemp" AutoPostBack="true" runat="server" Checked="false" />
                                        <span class="slider round"></span>
                                    </label>
                                    <asp:Label runat="server" Text="Unmapped Description"></asp:Label>
                                </div>
                                <div class="col-sm-6 col-md-6 col-lg-">
                                    <asp:GridView ID="GrdviewTotalAmount" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" Width="100%">
                                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:GridView>
                                </div>
                                <asp:Panel ID="pnlFreeze" runat="server" Visible="false">
                                    <div class="pull-right divmargin col-sm-3 col-md-3 col-lg-3" style="padding-left: 0px; margin-right: 0px">
                                        <center>
                                            <asp:LinkButton ID="lnkBtnFreeze" class="pull-right" runat="server"><b><i>Freeze for Next Year</i></b></asp:LinkButton>
                                        </center>
                                    </div>
                                    <div class="pull-right divmargin col-sm-3 col-md-3 col-lg-3" style="padding-left: 0px; margin-right: 0px">
                                        <asp:LinkButton ID="lnkBtnFreezePrev" Visible="false" class="pull-right" runat="server"><b><i>Freeze for Prev Year</i></b></asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <div class="col-md-12" style="padding-left: 0; padding-right: 0">
                                    <div id="div1" runat="server" style="border-style: none; border-color: inherit; border-width: medium; width: 100%;">
                                        <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                                            <div id="div2" runat="server" style="overflow-y: auto; height: 400px; width: 100%;">
                                                <asp:GridView ID="dgGeneralSchedTemp" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:UpdatePanel runat="server" ID="UpdatePanel_1">
                                                                    <ContentTemplate>
                                                                        <asp:CheckBox ID="chkSelectSchedTemp" runat="server" AutoPostBack="True"
                                                                            OnCheckedChanged="chkSelectSchedTemp_CheckedChanged" />
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="chkSelectSchedTemp" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <asp:Label ID="lblDescdetails" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescDetailsID") %>'></asp:Label>
                                                                <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                                                                <asp:Label ID="lblStatus" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                                                <asp:Label ID="lblScheduleType" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ScheduleType") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Description Code" HeaderStyle-Width="5px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescriptionCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptionCode") %>' Width="20px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Width="250px"></asp:Label>--%>
                                                                <asp:LinkButton ID="lblDescription" CommandName="EditRow" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Font-Bold="true" Font-Italic="true" Width="150px"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Opening Debit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblProcessID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ProcessID") %>'></asp:Label>--%>
                                                                <asp:Label ID="lblOpeningDebit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.OpeningDebit") %>' Width="85px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Opening Credit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOpeningCredit" runat="server" Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.OpeningCredit") %>' Width="85px"></asp:Label>
                                                                <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tr Debit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrDebit") %>' Width="85px"></asp:Label>
                                                                <asp:Label ID="lblTrDebittrUploaded" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrDebittrUploaded") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tr Credit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTrCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCredit") %>' Width="85px"></asp:Label>
                                                                <asp:Label ID="lblTrCredittrUploaded" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TrCredittrUploaded") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Closing Debit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClosingDebit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClosingDebit") %>' Width="85px"></asp:Label>
                                                                <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Closing Credit" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblClosingCredit" Style="text-align: right" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClosingCredit") %>' Width="85px"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <%--SubItem heading--%>
                                                        <asp:TemplateField HeaderText="SubItem Heading">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubitemname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ASSI_Name") %>'></asp:Label>
                                                                <asp:Label ID="lblSubitem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Subitemid") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Of Sub Item" Visible="false" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblitemTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemTotal") %>' Width="85px"></asp:Label>--%>
                                                                <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <%--Item heading--%>
                                                        <asp:TemplateField HeaderText="Item Heading">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ASI_Name") %>'></asp:Label>
                                                                <%--<asp:DropDownList ID="gvddlitem" runat="server" CssClass="aspxcontrols" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="gvdddlItem_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:Label ID="lblitem" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.itemid") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Of Item" Visible="false" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblitemTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemTotal") %>' Width="85px"></asp:Label>--%>
                                                                <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <%--Subheading--%>
                                                        <asp:TemplateField HeaderText="Sub Heading">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubheadingname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ASSH_name") %>'></asp:Label>
                                                                <%--<asp:DropDownList ID="gvddlSubheading" runat="server" CssClass="aspxcontrols" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="gvddlSubheading_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:Label ID="lblSubheading" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.subheadingid") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Of Sub heading" Visible="false" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblSubheadingTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.subheadingTotal") %>' Width="85px"></asp:Label>--%>
                                                                <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <%--Headings--%>
                                                        <asp:TemplateField HeaderText="Heading">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblheadingname" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ASH_Name") %>'></asp:Label>
                                                                <%--<asp:DropDownList ID="gvddlheading" runat="server" CssClass="aspxcontrols" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="gvddlheading_SelectedIndexChanged"></asp:DropDownList>--%>
                                                                <asp:Label ID="lblheading" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.headingid") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Of Sub Heading" Visible="false" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblSubGroupTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.headingTotal") %>' Width="85px"></asp:Label>--%>
                                                                <%--<asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div id="ModalJEItemsSchedTemp" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
                            <div class="modalmsg-dialog">
                                <div class="modalmsg-content">
                                    <div class="modalmsg-header">
                                        <h4 class="modal-title"><b>transactions for Finalisation of Accounts</b></h4>
                                    </div>
                                    <div class="modalmsg-body">
                                        <asp:GridView ID="gvJeitemsSchedTemp" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="footable" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Itemid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField Visible="false" HeaderText="SrNo" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                                <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                                <asp:BoundField HeaderText="Credit" DataField="Credit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                                <asp:BoundField HeaderText="Debit" DataField="Debit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                                <asp:BoundField HeaderText="Transaction Type" DataField="TrType" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                                <asp:BoundField HeaderText="Transaction Date" DataField="TransactionDate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                            </Columns>
                                            <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <div class="modalmsg-footer">
                                        <button data-dismiss="modal" runat="server" class="btn-ok" id="Button2">
                                            OK
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlClosingSTock" Visible="false">
        <div class="reportDetailsMN">
            <div class="sectionTitleMn">
                <div class="col-sm-6 col-md-6 pull-left">
                    <h2><b>Stock Entry</b></h2>
                </div>
                <div class="pull-right col-sm-3 col-md-3">
                    <div class="pull-right ">
                        <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                        <asp:ImageButton ID="imgbtnBack" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />
                        <asp:ImageButton ID="ImgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                        <asp:ImageButton ID="imgLinkageForYear" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Copy the Linkage for this Year" />
                    </div>
                </div>
            </div>
            <div class="clearfix"></div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:Label ID="Label5" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <asp:Label ID="Label6" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
        </div>
        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-6 col-md-6 form-group pull-left" style="padding: 0px">
                <a href="#">
                    <div id="div3" runat="server" data-toggle="collapse" data-target="#collapseRRIT"><b><i>Click here to view Sample Format...</i></b></div>
                </a>
            </div>
            <div class="col-sm-6 col-md-6 pull-right">
                <div class="form-group pull-right">
                    <a href="#">
                        <asp:LinkButton ID="lnkDownload" runat="server"><b><i>Click here to Download Uploadable Excel</i></b></asp:LinkButton></a>
                </div>
            </div>
        </div>
        <div class="form-group divmargin"></div>
        <div id="collapseRRIT" class="collapse">
            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                <div class="form-group">
                    <asp:DataGrid ID="dgSampleFormat" runat="server" AutoGenerateColumns="true" Width="100%" class="footable">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" HorizontalAlign="Left" VerticalAlign="Top" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
        <div class="divmargin "></div>
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Label ID="Label8" runat="server" Text="* Customer Name"></asp:Label>
                            <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="true" onchange="showProgress()"></asp:DropDownList>
                            <asp:Label ID="Label9" runat="server" />
                        </div>
                    </div>
                    <div class="col-sm-4 col-md-4">
                        <div class="form-group">
                            <asp:Label ID="Label12" runat="server" Text="Financial Year"></asp:Label>
                            <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                            </asp:DropDownList>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="ddlCustName" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div class="overlay">
                        <div style="z-index: 1000; margin-left: 350px; margin-top: 200px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="/Images/pageloader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="lblSelectFile" runat="server" Text="Select a file"></asp:Label>
                    <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
                </div>
                <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
            </div>
            <div class="col-sm-1 col-md-1">
                <div class="form-group">
                    <div style="margin-top: 20px;"></div>
                    <asp:Button ID="btnOk" runat="server" Text="Ok" />
                </div>
            </div>
            <div class="col-sm-4 col-md-4 pull-right" style="padding-right: 0">
                <div class="form-group">
                    <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" Visible="false" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="col-md-12" style="padding-left: 0; padding-right: 0">
            <asp:Label CssClass="divmargin" runat="server" ID="Label11" Text="Total:" Font-Bold="true" Font-Size="Large"></asp:Label>
            <asp:Label runat="server" ID="lblTotal"></asp:Label>
            <div id="div4" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; width: 100%;">
                <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                    <div id="div5" runat="server" style="overflow-y: auto; width: 100%;">
                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="dgGeneral" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="10px">
                                        <ItemTemplate>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                                <ContentTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="chkSelect_CheckedChanged" />
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="chkSelect" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:Label ID="Label4" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10px" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="Description" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Classification" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="Itemclassification" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Itemclassification") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit price" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="UP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UP") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="8%">
                                        <ItemTemplate>
                                            <asp:Label ID="Quantity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" ItemStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:Label ID="UOM" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UOM") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-Width="5px">
                                        <ItemTemplate>
                                            <asp:Label ID="Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="ModalJEItems" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modalmsg-dialog">
                <div class="modalmsg-content">
                    <div class="modalmsg-header">
                        <h4 class="modal-title"><b>JE transactions for Finalisation of Accounts</b></h4>
                    </div>
                    <div class="modalmsg-body">
                        <asp:GridView ID="gvJeitems" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="footable" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Itemid") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" HeaderText="SrNo" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                <asp:BoundField HeaderText="Description" DataField="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                <asp:BoundField HeaderText="Credit" DataField="Credit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                <asp:BoundField HeaderText="Debit" DataField="Debit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                <asp:BoundField HeaderText="Transaction Type" DataField="TrType" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                                <asp:BoundField HeaderText="Transaction Date" DataField="TransactionDate" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="modalmsg-footer">
                        <button data-dismiss="modal" runat="server" class="btn-ok" id="Button3">
                            OK
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <div id="ModalExcelValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">

                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblExcelValidationMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalExcelValidationfrz" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">

                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgTypefrz" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblfrz" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button id="btnfrz" runat="server" class="btn-ok">
                        Yes
                    </button>
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button4">
                        No
                    </button>
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
                    <div id="divMsgType" class="alert alert-info">
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
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>



