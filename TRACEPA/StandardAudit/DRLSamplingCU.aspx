<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerUserMaster.Master" CodeBehind="DRLSamplingCU.aspx.vb" Inherits="TRACePA.DRLSamplingCU" %>

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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlAuditNo.ClientID%>').select2();
            $('#<%=gvDRLLog.ClientID%>').DataTable({
                searching: false,
                iDisplayLength: 10,
                aLengthMenu: [[10, 20, 30, 40, 50, 100, 500, -1], [10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [8] }],
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Collection of Data</b></h2>
            </div>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Upload Details" Visible="false" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Enabled="false"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label ID="lblHFY" Text="Financial Year" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblTaskCode" runat="server" Text="* Audit No"></asp:Label>
                <asp:RequiredFieldValidator ID="RFVAuditNo" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <br />
                <asp:Label ID="lblHAuditType" runat="server" Text="Audit Type : "></asp:Label>
                <asp:Label ID="lblAuditType" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-6 col-md-6" style="padding: 0px">
            <div class="col-sm-12 col-md-12">
                <div class="form-group">
                    <asp:Label ID="lblHCheckPoint" runat="server" Text="Check Point : "></asp:Label>
                    <asp:Label ID="lblCheckPoint" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblCheckPointId" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="form-group">
                    <asp:Label ID="lblHDocumentRequestedList" runat="server" Text="Document Requested List : "></asp:Label>
                    <asp:Label ID="lblDocumentRequestedList" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblDocumentRequestedListId" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label ID="lblRequestedOn" runat="server" Text="* Received On"></asp:Label>
                    <asp:TextBox ID="txtRequestedOn" Enabled="false" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRequestedOn" runat="server" ControlToValidate="txtRequestedOn" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRequestedOn" runat="server" ControlToValidate="txtRequestedOn" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtTentativeStartDate" PopupPosition="TopRight"
                        TargetControlID="txtRequestedOn" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblHStatus" runat="server" Text="* Status"></asp:Label>
                        <asp:RequiredFieldValidator ID="RFVStatus" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlStatus" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                </div>
                <br />
                <div class="col-sm-2 col-md-2 pull-right" style="padding-right: 0px">
                    <asp:ImageButton ID="imgbtnAttachment" OnClick="imgbtnAttachment_Click" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;" CausesValidation="false"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4" style="padding-left: 0px">
                    <asp:Label ID="Label1" runat="server" Text="* TimeLine"></asp:Label>
                    <asp:TextBox ID="txttimeline" Enabled="false" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRequestedOn" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtRequestedOn" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtTentativeStartDate" PopupPosition="TopRight"
                        TargetControlID="txtRequestedOn" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                    </cc1:CalendarExtender>
                </div>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHComment" runat="server" Text="* Received Comments"></asp:Label>
                <asp:RequiredFieldValidator ID="RFVComment" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtComment" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtComment" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="85px"></asp:TextBox>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVComment" runat="server" ControlToValidate="txtComment" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
        <div class="form-group">
            <asp:GridView ID="gvDRLLog" CssClass="table bs dataTable no-footer" runat="server" AutoGenerateColumns="False" Width="100%">
                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:BoundField HeaderText="Check Point" DataField="CheckPoint"></asp:BoundField>
                    <asp:TemplateField HeaderText="Document Requested List">
                        <ItemTemplate>
                            <asp:Label ID="lblDRLID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DRLID") %>'></asp:Label>
                            <asp:Label ID="lblCheckPointId" runat="server" Font-Bold="true" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CheckPointID") %>'></asp:Label>
                            <asp:Label ID="lblCPID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CheckPointID") %>'></asp:Label>
                            <asp:Label ID="lblDocReqListID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentRequestedListID") %>'></asp:Label>
                            <asp:Label ID="lblAttachID" Visible="false" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.AttachID") %>'></asp:Label>
                            <asp:Label ID="lblDocID" Visible="false" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.DocID") %>'></asp:Label>
                            <asp:Label ID="lblDocumentRequestedList" Visible="false" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentRequestedList") %>'></asp:Label>
                            <asp:LinkButton ID="lnkDocumentRequestedList" Font-Italic="true" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentRequestedList") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Email ID" DataField="EmailID"></asp:BoundField>
                    <asp:BoundField HeaderText="Requested On" DataField="RequestedOn"></asp:BoundField>
                    <asp:TemplateField HeaderText="Comments" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblremarks" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Comments") %>' Width="100%"></asp:Label>
                            <asp:LinkButton ID="lnkbtnRemarks" runat="server" Text='Click here to view history' CommandName="HistoryAR" Font-Underline="true"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Received Comments" Visible="false" DataField="ReceivedComments"></asp:BoundField>
                    <asp:BoundField HeaderText="Received On" DataField="ReceivedOn"></asp:BoundField>
                    <asp:BoundField HeaderText="Status" DataField="Status"></asp:BoundField>
                    <asp:BoundField HeaderText="Timeline To Respond" DataField="TimlinetoResOn"></asp:BoundField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnAttachment" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Attachment" CommandName="Attachment" runat="server" /><span class="badge"><asp:Label ID="lblBadgeCountgv" runat="server" Text="0"></asp:Label></span>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="col-sm-11 col-md-11">
                        <h4 class="modal-title" style="text-align: center;"><b>Attachment</b></h4>
                    </div>
                    <div class="col-sm-1 col-md-1">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-7 col-md-7" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" CssClass="table table-bordered" OnRowDataBound="PickColor_RowDataBound">
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
                                            <b>By-</b>
                                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label><br />
                                            <b>On-</b>
                                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" /><br />
                                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="myHistoryModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Coments History</b></h4>
                    <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body" style="height: 425px">
                    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                        <div id="divHistory" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                            <asp:GridView ID="gvHistory" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <%--<asp:BoundField DataField="RemarksType" HeaderText="Type" ItemStyle-Width="10%" />--%>
                                    <asp:BoundField DataField="Role" HeaderText="Role" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="RemarksBy" HeaderText="Auditor Name" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="25%" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Timeline" HeaderText="TimeLine" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Comments" HeaderText="Comments Type" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalDRLLogDetailsValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDRLLogDetailsValidationMsg" runat="server"></asp:Label>
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
