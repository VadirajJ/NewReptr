<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SampleSelection.aspx.vb" Inherits="TRACePA.SampleSelection" %>

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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlColumns.ClientID%>').select2();
            $('#<%=ddlExcelSheet.ClientID%>').select2();
            $('#<%=ddlFilter.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 pull-left">
                <h2><b>Sample Selection</b></h2>
            </div>
            <div class="col-sm-5 col-md-5">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnFinalSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Final Grid Save"></asp:ImageButton>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div>
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblSheetName" Text="Sheet Name :" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlExcelSheet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHeading" Text="Document Name :" runat="server"></asp:Label>
                <asp:Label ID="lblDocName" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
            </div>
            <div class="form-group">
                <asp:Label ID="lblRows" Text="Total No of Rows :" runat="server"></asp:Label>
                <asp:Label ID="lblNoofRows" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="col-md-6">
            <div id="divcollapseSS" runat="server" data-toggle="collapse" data-target="#collapseSS"><a href="#"><b><i>Click here to get Non Statistical Techinque</i></b></a></div>
        </div>
    </div>
    <div id="collapseSS" class="collapse in">
        <%--<class="collapse">--%>
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-4 col-md-4">
                <div class="form-group">
                    <asp:Label ID="lblColumns" Text="Cloumn Names :" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlColumns" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFilter" Text="Filter :" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlFilter" AutoPostBack="True" runat="server" CssClass="aspxcontrols">
                        <asp:ListItem Value="0">Select Filter Type</asp:ListItem>
                        <asp:ListItem Value="1">-ve Value</asp:ListItem>
                        <asp:ListItem Value="2">5 From High Value</asp:ListItem>
                        <asp:ListItem Value="3">High Value Between</asp:ListItem>
                        <asp:ListItem Value="4">Top 10 Values</asp:ListItem>
                        <asp:ListItem Value="5">Least 10 Values</asp:ListItem>
                        <asp:ListItem Value="6">Greater than or Equal</asp:ListItem>
                        <asp:ListItem Value="7">Equal</asp:ListItem>
                        <asp:ListItem Value="8">Not Equal</asp:ListItem>
                        <asp:ListItem Value="9">Less than or Equal</asp:ListItem>
                        <asp:ListItem Value="10">Missing Numbers</asp:ListItem>
                        <asp:ListItem Value="11">Selected Value</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblFromValue" Visible="false" runat="server"></asp:Label>
                    <asp:TextBox ID="txtFrmVal" Visible="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:DropDownList ID="ddlSelValue" Visible="false" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblTo" Text="To" Visible="false" runat="server"></asp:Label>
                    <asp:TextBox ID="txtTo" Visible="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <br />
                <div class="form-group">
                    <asp:ImageButton ID="imgbtnNSTFilter" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="ADD" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnNSTAddToFinal" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="ADD" CausesValidation="false" />
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-12 col-md-12">
                <asp:Label ID="lblMissingNos" Visible="True" runat="server" CssClass="aspxlabelbold"></asp:Label>
                <div id="divNST" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto;">
                    <asp:DataGrid ID="dgNST" runat="server" AutoGenerateColumns="True" PageSize="1000" Width="100%" CssClass="table table-bordered" onrowdatabound="PickColor_RowDataBound">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                        <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkNST" Visible="True" runat="server" commandName="Select"></asp:CheckBox>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkall" runat="server" CssClass="CheckBox" TextAlign="Left" AutoPostBack="True" />
                                </HeaderTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin"></div>
    <div class="col-sm-12 col-md-12">
        <div class="col-md-6">
            <div id="divcollapseST" runat="server" data-toggle="collapse" data-target="#collapseST"><a href="#"><b><i>Click here to get Statistical Techinque</i></b></a></div>
        </div>
    </div>
    <div id="collapseST" class="collapse in">
        <%--<class="collapse">--%>
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:RadioButton ID="rboSystematic" Text="Systematic Sampling" GroupName="rbo" AutoPostBack="true" runat="server"></asp:RadioButton>
                    <br />
                    <br />
                    <asp:RadioButton ID="rboSatisfied" Text="Stratified Sampling" GroupName="rbo" AutoPostBack="true" runat="server"></asp:RadioButton>
                </div>
            </div>
            <div class="col-sm-10 col-md-10">
                <div class="form-group">
                    <br />
                    <asp:Label ID="lblDesc1" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    <asp:TextBox ID="txtRows" Visible="False" runat="server" CssClass="aspxcontrols" Width="50px"></asp:TextBox>
                    <asp:Label ID="lblDesc2" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    <asp:ImageButton ID="imgbtnSTFilter" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="ADD" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSTAddToFinal" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="ADD" CausesValidation="false" />
                </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-12 col-md-12">
                <div id="divST" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto;">
                    <asp:DataGrid ID="dgST" runat="server" AutoGenerateColumns="True" PageSize="1000" Width="100%" CssClass="table table-bordered" onrowdatabound="PickColor_RowDataBound">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                        <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="Chk" runat="server" AutoPostBack="True" Visible="True" CssClass="CheckBox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="ChkST" runat="server" AutoPostBack="True" Visible="True" CssClass="CheckBox" />
                                </ItemTemplate>

                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin"></div>
    <div class="col-sm-12 col-md-12">
        <div class="col-md-6">
            <div id="divcollapseRR" runat="server" data-toggle="collapse" data-target="#collapseRR"><a href="#"><b><i>Click here to get Random Rows</i></b></a></div>
        </div>
    </div>
    <div id="collapseRR" class="collapse in">
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-12 col-md-12">
                <div class="form-group">
                    <asp:Label ID="lblFrom" Text="From Rows:" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    <asp:TextBox ID="txtFrom" runat="server" CssClass="aspxcontrols" Width="50px"></asp:TextBox>
                    <asp:Label ID="lblToRR" Text="To Rows:" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    <asp:TextBox ID="txtRRTO" runat="server" CssClass="aspxcontrols" Width="50px"></asp:TextBox>
                    <asp:Label ID="lblSS" Text="Sample Size:" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    <asp:TextBox ID="txtSS" runat="server" CssClass="aspxcontrols" Width="50px"></asp:TextBox>
                    <asp:ImageButton ID="imgbtnRR" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Get Random Rows" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnRRFilter" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="ADD" CausesValidation="false" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div id="divRandom" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto;">
                    <asp:DataGrid ID="dgRandom" runat="server" AutoGenerateColumns="True" PageSize="1000" Width="100%" CssClass="table table-bordered" onrowdatabound="PickColor_RowDataBound">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                        <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkRRALL" runat="server" AutoPostBack="False" TextAlign="Left" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkRR" runat="server" AutoPostBack="False" Visible="True" CssClass="CheckBox" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
   <div class="col-sm-12 col-md-12 divmargin"></div>
    <div class="col-sm-12 col-md-12">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <div id="divFinal" runat="server" data-toggle="collapse" data-target="#collapseFinal"><a href="#"><b><i>Click here to See Final Values</i></b></a></div>
            </div>
        </div>
    </div>
    <div id="collapseFinal" class="collapse">
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-12 col-md-12">
                <div class="form-group">
                    <asp:ImageButton ID="imgbtnCheckDuplicate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="REMOVE DUPLICATE" CausesValidation="false" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div id="divFinalData" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto;">
                    <asp:DataGrid ID="dgFinalData" runat="server" AutoGenerateColumns="True" PageSize="1000" Width="100%" CssClass="table table-bordered" onrowdatabound="PickColor_RowDataBound">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                        <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
   <div class="col-sm-12 col-md-12 divmargin"></div>
    <div class="col-sm-12 col-md-12">
        <div class="col-sm-6 col-md-6">
            <div id="divAttach" runat="server" data-toggle="collapse" data-target="#collapseAttachment"><a href="#"><b><i>Click here to See Saved Samples</i></b></a></div>
        </div>
    </div>
    <div id="collapseAttachment" class="collapse">
        <div class="col-sm-12 col-md-12">
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" CssClass="table table-bordered" onrowdatabound="PickColor_RowDataBound">
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
                                <b>By : </b>
                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                <b>On : </b>
                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </div>
    </div>
    <div id="ModaISS" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSampleMsg" runat="server"></asp:Label>
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
