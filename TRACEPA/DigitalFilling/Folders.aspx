<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Folders.aspx.vb" Inherits="TRACePA.Folders" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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

        .FixedHeader {
            position: relative;
            font-weight: bold;
            background: #fff;
        }

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px;
            line-height: 1px
        }
    </style>

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
            $('#<%=dgFolders.ClientID%>').DataTable({

                //initComplete: function () {
                //    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                //},
                //iDisplayLength: 20,
                //aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                //order: [],
                //columnDefs: [{ orderable: false, targets: [0] }],

                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
            });
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
        function Setting() {
            $('#myModal').modal('show');
        }
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Folder" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New Folder" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
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
            <div class="clearfix divmargin"></div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label ID="Label1" runat="server" Text="* Cabinet"></asp:Label>
                    <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label ID="lblSubcab" runat="server" Text="* Sub Cabinet"></asp:Label>
                    <asp:DropDownList ID="ddlSubCabinet" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>

            </div>
            <div class="clearfix divmargin"></div>
            <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto;">
                <div class="form-group">
                    <asp:GridView ID="dgFolders" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                                    <asp:Label ID="lblFOL_FOLID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOL_FOLID") %>'></asp:Label>
                                    <asp:Label ID="lblFOL_NAME" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOL_NAME") %>'></asp:Label>
                                    <asp:Label ID="lblPGE_CABINET" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PGE_CABINET") %>'></asp:Label>
                                    <asp:Label ID="lblPGE_FOLDER" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOL_FOLID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="FOL_FOLID" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="CBN_Department" HeaderText="Clients" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%" />
                            <asp:TemplateField HeaderText="Folder Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="16%">
                                <ItemTemplate>
                                    <asp:Label ID="lblFolderName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FOL_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CBN_Name" HeaderText="Sub Cabinet" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%" />
                            <%-- <asp:TemplateField HeaderText="Documents" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDocuments" Font-Italic="true" runat="server" CommandName="Document" Text='<%# DataBinder.Eval(Container, "DataItem.PGE_DETAILS_ID") %>'></asp:LinkButton>
                        <asp:label ID="lblDocuments" Font-Italic="true" runat="server" Visible ="false"  CommandName="Document" Text='<%# DataBinder.Eval(Container, "DataItem.PGE_DETAILS_ID") %>'></asp:label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Documents" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDocuments" Font-Italic="true" runat="server" CommandName="Document" Text='<%# DataBinder.Eval(Container, "DataItem.Pagecount") %>'></asp:LinkButton>
                                    <asp:Label ID="lblDocuments" Font-Italic="true" runat="server" Visible="false" CommandName="Document" Text='<%# DataBinder.Eval(Container, "DataItem.Pagecount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="FOL_CreatedBy" HeaderText="Created By" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%"></asp:BoundField>
                            <asp:BoundField DataField="FOL_CreatedOn" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%" />
                            <asp:BoundField DataField="FOL_DelFlag" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="14%" />
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnStatus" CssClass="hvr-bounce-in" CommandName="Status" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="EditRow" runat="server" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                <div class="modal-dialog">
                    <div class="modal-content row">
                        <div class="modal-header">

                            <h4 class="modal-title"><b>Folder Details</b></h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body row">
                            <div class="col-sm-12 col-md-12 form-group">
                                <div class="pull-left">
                                    <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblCabinet" runat="server" Text="Cabinet Name: "></asp:Label>
                                    <asp:Label ID="lblCabinetName" runat="server" CssClass="aspxlabelbold "></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblSubCabinet" runat="server" Text="Sub-Cabinet Name: "></asp:Label>
                                    <asp:Label ID="lblSubCabinetName" runat="server" CssClass="aspxlabelbold "></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblFolName" runat="server" Text="* Folder Name"></asp:Label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFolName" runat="server" SetFocusOnError="True" ControlToValidate="txtFolName" Display="Dynamic" ValidationGroup="ValidateFolder"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFolName" runat="server" ControlToValidate="txtFolName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateFolder"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtFolName" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblFolNotes" runat="server" Text="Notes"></asp:Label>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFolNotes" runat="server" ControlToValidate="txtFolNotes" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateFolder"></asp:RegularExpressionValidator>
                                    <asp:TextBox ID="txtFolNotes" autocomplete="off" TextMode="MultiLine" runat="server" Height="84px" CssClass="aspxcontrols" />
                                </div>
                            </div>
                            <asp:Panel ID="pnlFolder" runat="server">
                                <div class="col-sm-12 col-md-12" style="padding: 0px">
                                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                        <fieldset class="col-sm-12 col-md-12">
                                            <legend class="legendbold">Permission Details</legend>
                                        </fieldset>
                                        <div class="col-sm-12 col-md-12">
                                            <div class="pull-left">
                                                <asp:Label ID="lblPrmError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-6">
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Permission Level"></asp:Label>
                                                <asp:DropDownList ID="ddlPermissionLevel" runat="server" AutoPostBack="True" CssClass="aspxcontrols">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label runat="server" Text="Department"></asp:Label>
                                                <asp:DropDownList ID="ddlPermissionDep" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="lblUser" runat="server" Text="User"></asp:Label>
                                                <asp:DropDownList ID="ddlPermissionUser" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 col-md-6 form-group">
                                            <asp:Label ID="lblChkPermission" runat="server" Text="Permission"></asp:Label>
                                            <asp:CheckBoxList ID="chkPermission" runat="server" CssClass="aspxcontrols" Height="140px"></asp:CheckBoxList>
                                        </div>
                                        <div class="col-sm-6 col-md-6 pull-right">
                                            <asp:CheckBox ID="CBLAssignP" Text="Inherit to Group" runat="server"></asp:CheckBox>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right">
                                <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNew"></asp:Button>
                                <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSave" ValidationGroup="ValidateFolder"></asp:Button>
                                <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdate" ValidationGroup="ValidateFolder"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <div id="CabinetMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCabinetEmpMasterValidationMsg" runat="server"></asp:Label></strong>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

