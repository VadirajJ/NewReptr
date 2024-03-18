<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerUserMaster.Master" CodeBehind="AuditorIssue.aspx.vb" Inherits="TRACePA.AuditorIssue" %>

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
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Query / Issues raised by Auditor</b></h2>
            </div>
            <div class="pull-right">
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
                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
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
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHLedger" runat="server" Text="* Ledger"></asp:Label>
                <asp:DropDownList ID="ddlLedger" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="form-group">
            <asp:GridView ID="gvLedgerComments" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:BoundField DataField="LedgerName" HeaderText="Ledger Name" ItemStyle-Width="22%" />
                    <asp:BoundField DataField="AuditorName" HeaderText="Auditor Name" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="AuditorComments" HeaderText="Auditor Comments" ItemStyle-Width="30%" />
                    <asp:TemplateField HeaderText="Customer Comments" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCustomerComments" runat="server" MaxLength="2000" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerComments") %>' Width="100%" TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>
                            <asp:Label ID="lblLedgerId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.LedgerId") %>'></asp:Label>
                            <asp:Label ID="lblAuditorCommentsId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditorCommentsId") %>'></asp:Label>
                            <asp:Label ID="lblCustomerCommentsId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerCommentsId") %>'></asp:Label>
                            <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                            <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment24.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                            <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" CommandName="Save" title="Save" Style="padding-right: 0px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHCheckPoint" runat="server" Text="* Check Point"></asp:Label>
                <asp:DropDownList ID="ddlCheckPoint" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12">
        <div class="form-group">
            <asp:GridView ID="gvCheckPoint" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="CheckPointName" HeaderText="Check Point Name" ItemStyle-Width="22%" />
                    <asp:BoundField DataField="AuditorName" HeaderText="Auditor/Reviewer Name" ItemStyle-Width="10%" />
                    <asp:BoundField DataField="AuditorComments" HeaderText="Auditor/Reviewer Comments" ItemStyle-Width="30%" />
                    <asp:TemplateField HeaderText="Customer Comments" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtCustomerComments" runat="server" MaxLength="2000" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerComments") %>' Width="100%" TextMode="MultiLine"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Action">
                        <ItemTemplate>                            
                            <asp:Label ID="lblConductAuditCheckPointId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ConductAuditCheckPointId") %>'></asp:Label>
                            <asp:Label ID="lblCheckPointId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CheckPointId") %>'></asp:Label>
                            <asp:Label ID="lblAuditorCommentsId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditorCommentsId") %>'></asp:Label>
                            <asp:Label ID="lblCustomerCommentsId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerCommentsId") %>'></asp:Label>
                            <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                            <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment24.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                            <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" CommandName="Save" title="Save" Style="padding-right: 0px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="ModalCUValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCUValidationMsg" runat="server"></asp:Label>
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
    <div id="myModalMainAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
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
                    <div class="row">
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
                                    <asp:Label ID="lblHDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:DataGrid ID="dgMainAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="table table-bordered" OnRowDataBound="PickColor_RowDataBound">
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
</asp:Content>
