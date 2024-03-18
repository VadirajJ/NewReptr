<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DocumentType.aspx.vb" Inherits="TRACePA.DocumentType" ValidateRequest="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
            $('#<%=dgDocTypeDashBoard.ClientID%>').DataTable({
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
        })
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Document Type" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                                </li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" />
                                </li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <br />
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="275px">
                    </asp:DropDownList>
                </div>

            </div>
            <div class="clearfix divmargin"></div>
            <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto;">
                <div class="form-group">
                    <asp:GridView ID="dgDocTypeDashBoard" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                    <asp:Label ID="lblDocTypeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocTypeID") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="Name" HeaderText="Name" HeaderStyle-Width="28%"></asp:BoundField>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="Department" HeaderText="Department" HeaderStyle-Width="28%"></asp:BoundField>
                            <asp:BoundField DataField="Note" HeaderText="Note" Visible="false"></asp:BoundField>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="CrBy" HeaderText="Created By" HeaderStyle-Width="12%"></asp:BoundField>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="CrOn" HeaderText="Created On" HeaderStyle-Width="12%"></asp:BoundField>
                            <asp:BoundField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" DataField="Status" HeaderText="Status" HeaderStyle-Width="14%"></asp:BoundField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnStatus" CssClass="hvr-bounce-in" CommandName="Status" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" ToolTip="Edit" CssClass="hvr-bounce-in" CommandName="EditRow" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
            <div id="myModal" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content row">
                        <div class="modal-header">

                            <h4 class="modal-title"><b>Document Type details</b></h4>
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body row">
                            <div class="col-sm-12 col-md-12">
                                <div class="pull-left">
                                    <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <asp:Label ID="lblDocType" runat="server" Text="* Document Type"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDocType" runat="server" SetFocusOnError="True" ControlToValidate="txtDocType" Display="Dynamic" ValidationGroup="ValidateDocType"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDocType" runat="server" ControlToValidate="txtDocType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDocType"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtDocType" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <asp:Label ID="lblDepartment" runat="server" Text="* Department"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDepartment" runat="server" SetFocusOnError="True" ControlToValidate="ddlDepartment" Display="Dynamic" ValidationGroup="ValidateDocType"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDepartment" AutoPostBack="false" runat="server" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <asp:Label ID="lblNote" runat="server" Text="Notes"></asp:Label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNote" runat="server" ControlToValidate="txtNote" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDocType"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtNote" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px;">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>

                            <div class="col-sm-12 col-md-12">
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <asp:Label ID="lblDescriptor" runat="server" Text="Descriptor"></asp:Label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDescriptor" runat="server" SetFocusOnError="True" ControlToValidate="ddlDescriptor" Display="Dynamic" ValidationGroup="ValidateDesc"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlDescriptor" runat="server" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-right: 0px;">
                                    <br />
                                    <div class="form-group">
                                        <asp:Button ID="btnAdd" runat="server" CssClass="btn-ok" Text="Add Descriptor" ValidationGroup="ValidateDesc" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <div class="form-group">
                                    <asp:GridView ID="dgDisplay" runat="server" Width="100%" CssClass="table bs" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Available">
                                       <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="DescId" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DescId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Descriptor">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descriptor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DataType">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDataType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DataType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Size") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mandatory">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectMandatory" runat="server" CssClass="hvr-bounce-in" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Values">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Values") %>'></asp:TextBox>
                                                    <asp:Panel ID="pnlCalendar" runat="server">
                                                        <cc1:CalendarExtender ID="cclValues" runat="server" PopupButtonID="imgValues"
                                                            TargetControlID="txtValues" Format="dd/MM/yyyy" PopupPosition="TopLeft">
                                                        </cc1:CalendarExtender>
                                                        <asp:ImageButton ID="imgValues" runat="server" Height="15px" ImageUrl="~/Images/Calendar.gif" Width="15px" />
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Validator">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelectValidator" runat="server" CssClass="hvr-bounce-in" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12 form-group" id="divPermDetails" runat="server" style="padding: 0px">
                                <fieldset class="col-sm-12 col-md-12">
                                    <legend class="legendbold">Permission Details</legend>
                                </fieldset>
                                <div class="col-sm-12 col-md-12" style="padding: 0px">
                                    <div class="col-sm-6 col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblPermissionLevel" runat="server" Text="Permission Level"></asp:Label>
                                            <asp:DropDownList ID="ddlPermission" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="GROUP"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="USER"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblGroup" runat="server" Text="Department"></asp:Label>
                                            <asp:DropDownList ID="ddlAllDept" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblUsers" runat="server" Text="Users"></asp:Label>
                                            <asp:DropDownList ID="ddlUsers" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="lblPermission" runat="server" Text="Permission"></asp:Label>
                                            <asp:CheckBoxList ID="chkdocument" runat="server" CssClass="aspxcontrols">
                                                <asp:ListItem Value="0" Text="Create Document"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Modify Document"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Delete Document"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Modify Document Type"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                        <div class="form-group">
                                            <asp:CheckBoxList ID="chkDocumentPermission" runat="server" CssClass="aspxcontrols">
                                                <asp:ListItem Value="0" Text="Index"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Search"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right">
                                <asp:Button runat="server" Text="New" class="btn-ok" ID="btnDocTypeNew"></asp:Button>
                                <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnDocTypeSave" ValidationGroup="ValidateDocType"></asp:Button>
                                <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnDocTypeUpdate" ValidationGroup="ValidateDocType"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
