<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Schedules.aspx.vb" Inherits="TRACePA.Schedules" %>

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
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlComptype.ClientID%>').select2();
            $('#<%=ddlscheduletype.ClientID%>').select2();
            $('#<%=ddlAccheadType.ClientID%>').select2();
            $('#<%=ddlHeading.ClientID%>').select2();
            $('#<%=ddlsubheading.ClientID%>').select2();
            $('#<%=ddlItems.ClientID%>').select2();
            $('#<%=ddlSUbItems.ClientID%>').select2();

            $('#<%=GvScheduleTemplate.ClientID%>').DataTable({
                iDisplayLength: 20,
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
    <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="card">
            <div  class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label3" CssClass="form-label" Font-Bold="true" Text="Schedule Templates" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="ImgbtnAddNew" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Sectuon" TabIndex="4" />
                    <asp:ImageButton ID="imgbtnsaveSchedule" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Sectuon" TabIndex="4" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Visible="false" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="lblscheduletype" Text="Schedule Type *" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlscheduletype" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                    <asp:ListItem Value="0">Select Scheduler type</asp:ListItem>
                                    <asp:ListItem Value="3">P & L </asp:ListItem>
                                    <asp:ListItem Value="4">Balance Sheet</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="lblComptype" Text="Organization Type*" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlComptype" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                            <div class="pull-left divmargin col-sm- 3 col-md-3 col-lg-3">
                                <asp:Label ID="Label2" Text="Account Head *" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlAccheadType" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                            <div class="pull-left divmargin col-sm-2 col-md-2 col-lg-2">
                                <br />
                                <asp:RadioButton ID="rdbtnStandard" runat="server" Text="Standard" TextAlign="Right" Checked="true" GroupName="TemplateType" />
                                <asp:RadioButton ID="rdbtnCustomise" runat="server" Text="Customise" TextAlign="Right" GroupName="TemplateType" />
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="Label7" Text="Heading" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlHeading" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgbtnAddHeadng" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Heading" CausesValidation="false" />
                                <asp:ImageButton ID="imgbtnEditHeadng" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Heading" CausesValidation="false" />
                            </div>
                            <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="Label8" Text="Sub Heading Name" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlsubheading" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgbtnAddSubHeadng" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Sub Heading" CausesValidation="false" />
                                <asp:ImageButton ID="imgbtnEditSubHeadng" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Sub Heading" CausesValidation="false" />
                            </div>
                            <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="Label9" Text="Item Under Sub-Heading" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlItems" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgbtnItems" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Items" CausesValidation="false" />
                                <asp:ImageButton ID="imgbtnEditItems" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Items" CausesValidation="false" />
                            </div>
                            <div class="form-group  pull-left divmargin col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="Label10" Text="Sub items under items" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlSUbItems" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                                <asp:ImageButton ID="imgbtnSubItems" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Heading" CausesValidation="false" />
                                <asp:ImageButton ID="imgbtnEditSubItems" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Items" CausesValidation="false" />
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="GvScheduleTemplate" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SGL Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblheadingID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Headingid") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="HeadingName" HeaderText="Heading" ItemStyle-Width="6%" />
                                    <asp:BoundField DataField="SubheadingID" Visible="false" HeaderText="Sub HeadingID" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Subheadingname" HeaderText="Sub Headings" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Itemid" HeaderText="ID" Visible="false" />
                                    <asp:BoundField DataField="itemname" HeaderText="Items" ItemStyle-Width="6%" />
                                    <asp:BoundField DataField="subitemid" Visible="false" HeaderText="Section Description" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="subitemname" HeaderText="Sub Items" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="AccHeadId" Visible="false" HeaderText="Report Type ID" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="AccHeadName" Visible="false" HeaderText="Report Type" ItemStyle-Width="20%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <%--Heading--%>
    <div id="Modalheading" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <asp:Label Font-Italic="true" runat="server" Font-Bold="true" ID="lblHeading"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4>
                        <asp:Label ID="lblheadingtext" runat="server" CssClass="modal-title" Font-Bold="true"></asp:Label></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12">
                    <div class="col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:Label ID="lblname" runat="server" Text="* Heading Name" Width="100%"></asp:Label>
                            <asp:TextBox ID="txtname" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblNotes" runat="server" Text="* Notes Number" Width="100%"></asp:Label>
                            <asp:TextBox ID="txtNotes" autocomplete="off" TextMode="Number" runat="server" CssClass="aspxcontrols" Width="100%" />
                            <asp:Label ID="Label1" runat="server" Text="Alias" Width="100%"></asp:Label>
                            <asp:TextBox ID="txtgrpalias" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>



                            <asp:ImageButton ID="imgbtnGrpAlias" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add alias" TabIndex="4" />
                        </div>

                    </div>
                    <div class="col-sm-6 col-md-6">
                        <div class="form-group">
                            <asp:GridView ID="gvAlias" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Alias Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                            <asp:Label ID="lblEmpID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.EmpID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblheadingID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Headingid") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Alias" HeaderText="Heading" ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" ToolTip="Delete" CommandName="Deleterow" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Clear" class="btn-ok" ID="btnClear"></asp:Button>
                        <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSavedetails" ValidationGroup="ValidateSection"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>
        </div>
    <div id="ModalScheduleValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
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

    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
