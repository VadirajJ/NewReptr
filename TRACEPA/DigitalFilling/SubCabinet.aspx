<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SubCabinet.aspx.vb" Inherits="TRACePA.SubCabinet" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
            $('#<%=dgSubCabinet.ClientID%>').DataTable({
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
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Sub Cabinet" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New Sub Cabinet" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />

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
        <div class="card">
            <div class="clearfix divmargin"></div>
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3" style="padding-left: 0px">
                    <asp:Label ID="lblSubcab" runat="server" Text="* Cabinet"></asp:Label>
                    <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">

                    <asp:Label ID="lblSubCabinet" runat="server" CssClass="ErrorMsgRight" Visible="false"></asp:Label>
                </div>
            </div>

            <div class="clearfix divmargin"></div>
            <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto;">
                <div class="form-group">
                    <asp:GridView ID="dgSubCabinet" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                                    <asp:Label ID="lblCBN_NODE" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CBN_ID") %>'></asp:Label>
                                    <asp:Label ID="lblCBN_NAME" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CBN_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="CBN_ID" HeaderText="ID" Visible="false" />
                            <asp:BoundField DataField="CBN_Department" HeaderText="Clients" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="22%" />
                            <asp:TemplateField HeaderText="Sub Cabinet Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="22%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkSubCabinetName" runat="server" CommandName="SelectSubCabinet" Text='<%# DataBinder.Eval(Container, "DataItem.CBN_NAME") %>' Font-Bold="true" Font-Italic="true"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="CBN_FolderCount" HeaderText="Folders" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%" />

                            <asp:BoundField DataField="CBN_CreatedBy" HeaderText="Created By" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%"></asp:BoundField>
                            <asp:BoundField DataField="CBN_CreatedOn" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="13%" />
                            <asp:BoundField DataField="CBN_DelFlag" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="14%"></asp:BoundField>
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

                            <h4 class="modal-title"><b>Sub-Cabinet Details</b></h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body row">
                            <div class="col-sm-12 col-md-12">
                                <div class="pull-left">
                                    <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <div class="form-group">
                                    <asp:Label ID="lblCab" runat="server" Text="Cabinet Name: "></asp:Label>
                                    <asp:Label ID="lblCabinetName" runat="server" CssClass="aspxlabelbold "></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblSubCabName" runat="server" Text="* Sub-Cabinet Name"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSubCabName" runat="server" SetFocusOnError="True" ControlToValidate="txtSubCabName" Display="Dynamic" ValidationGroup="ValidateSubCabinet"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSubCabName" runat="server" ControlToValidate="txtSubCabName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSubCabinet"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtSubCabName" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblSubCabDept" runat="server" Text="Department"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSubCabDept" runat="server" SetFocusOnError="True" ControlToValidate="ddlDepartment" Display="Dynamic" ValidationGroup="ValidateSubCabinet"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblSubCabNotes" runat="server" Text="Notes"></asp:Label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSubCabNotes" runat="server" ControlToValidate="txtSubCabNotes" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateSubCabinet"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtSubCabNotes" autocomplete="off" TextMode="MultiLine" runat="server" Height="82px" CssClass="aspxcontrols" />
                                    </div>
                                </div>
                            </div>
                            <asp:Panel ID="pnlSubCab" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
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
                                                        <asp:DropDownList ID="ddlPermissionUser" runat="server" AutoPostBack="True" CssClass="aspxcontrols">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 col-md-6 form-group">
                                                    <asp:Label ID="lblChkPermission" runat="server" Text="Permission"></asp:Label>
                                                    <asp:CheckBoxList ID="chkPermission" runat="server" CssClass="aspxcontrols" Height="140px" AutoPostBack="true"></asp:CheckBoxList>
                                                </div>
                                                <div class="col-sm-6 col-md-6 pull-right">
                                                    <asp:CheckBox ID="CBLAssignP" Text="Inherit to Group" runat="server" Visible="false"></asp:CheckBox>
                                                    <asp:CheckBox ID="ChkFC" Text="Entire File Plan" runat="server"></asp:CheckBox>
                                                    <%-- <asp:CheckBox ID="ChkSC" Text="Sub Cabinets" runat="server"></asp:CheckBox>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlPermissionLevel" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlPermissionDep" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlPermissionUser" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right">
                                <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDescNew"></asp:Button>
                                <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDescSave" ValidationGroup="ValidateSubCabinet"></asp:Button>
                                <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDescUpdate" ValidationGroup="ValidateSubCabinet"></asp:Button>
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
