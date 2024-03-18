<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ConductAudit.aspx.vb" Inherits="TRACePA.ConductAudit" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" />
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

        .multiselect-container {
            width: 100% !important;
        }

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px
        }

        input[type=checkbox] {
            vertical-align: middle;
            position: relative;
            bottom: 1px;
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
            //$('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlAuditNo.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Conduct Audit" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnSubmit" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Submit" ValidationGroup="Validate" />
                    <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                        <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                    <ul class="dropdown-menu">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" ValidationGroup="Validate" /></li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" ValidationGroup="Validate" /></li>
                    </ul>
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                </div>
            </div>
        </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHCustomerName" runat="server" Text="Customer Name"></asp:Label>
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
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px;">
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblHAuditNo" runat="server" Text="* Audit No."></asp:Label>
                                    <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFVAuditNo" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
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
                        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:GridView ID="gvCheckPoint" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr No." />
                                    <%--<asp:BoundField DataField="Heading" HeaderText="Heading" ItemStyle-Width="20%" />--%>
                                    <asp:BoundField DataField="CheckPoint" HeaderText="Audit Procedure" ItemStyle-Width="27%" />
                                    <asp:BoundField DataField="Mandatory" HeaderText="Mandatory" />
                                    <asp:TemplateField HeaderText="Test Result">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlTestResult" runat="server" CssClass="aspxcontrols">
                                                <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Yes"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="No"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="NA"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Auditor Remarks" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" MaxLength="2000" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>' Width="100%" TextMode="MultiLine"></asp:TextBox>
                                            <asp:LinkButton ID="lnkbtnRemarks" runat="server" Text='Click here to view history' CommandName="HistoryAR" Font-Underline="true"></asp:LinkButton>
                                            <asp:CheckBox ID="chkAuditorSendMail" Text="Raise a query" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reviewer Remarks" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtReviewerRemarks" runat="server" MaxLength="2000" Text='<%# DataBinder.Eval(Container, "DataItem.ReviewerRemarks") %>' Width="100%" TextMode="MultiLine"></asp:TextBox>
                                            <asp:LinkButton ID="lnkbtnReviewerRemarks" runat="server" Text='Click here to view history' CommandName="HistoryRR" Font-Underline="true"></asp:LinkButton>
                                            <asp:CheckBox ID="chkReviewerSendMail" Text="Raise a query" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Annexure">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAnnexure" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.Annexure")) %>' />
                                            <asp:ImageButton ID="imgbtnUploadObservations" data-toggle="tooltip" data-placement="bottom" title="Upload Observations" Width="15px" CommandName="Upload" runat="server" CssClass="hvr-bounce-in" /><br />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="ConductedBy" HeaderText="Conducted By" />
                                    <asp:BoundField DataField="LastUpdatedOn" HeaderText="Last Updated On" />--%>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:Label ID="lblConductAuditCheckPointPKId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ConductAuditCheckPointPKId") %>'></asp:Label>
                                            <asp:Label ID="lblAuditID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditID") %>'></asp:Label>
                                            <asp:Label ID="lblCheckPointID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CheckPointID") %>'></asp:Label>
                                            <asp:Label ID="lblCheckPoint" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CheckPoint") %>'></asp:Label>
                                            <asp:Label ID="lblAttachmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AttachmentID") %>'></asp:Label>
                                            <asp:Label ID="lblTestResult" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.TestResult") %>'></asp:Label>
                                            <asp:Label ID="lblRemarks" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'></asp:Label>
                                            <asp:Label ID="lblReviewerRemarks" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ReviewerRemarks") %>'></asp:Label>
                                            <asp:Label ID="lblMandatory" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Mandatory") %>'></asp:Label>
                                            <div class="col-sm-12 col-md-12">
                                                <asp:ImageButton ID="btnAttachment" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Attachment16.png" runat="server" data-toggle="tooltip" data-placement="bottom" CommandName="Attachment" title="Attachment" Style="padding: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                                                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save16.png" runat="server" data-toggle="tooltip" CommandName="Save" title="Save" Style="padding-right: 0px;" />
                                            </div>
                                            <div class="col-sm-12 col-md-12">
                                                <asp:LinkButton ID="lnkbtnDRL" runat="server" Text='Doc Request' Font-Underline="true" CommandName="DRL" data-toggle="tooltip" title="Document Request Log"></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalConductValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblConductValidationMsg" runat="server"></asp:Label>
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
    <div id="myHistoryModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false" style="width: 100%;">
        <div class="modal-dialog">
            <div class="modal-content" style="width: 150%;">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Remarks History</b></h4>
                    <button runat="server" type="button" class="close" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body" style="height: 425px">
                    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                        <div id="divHistory" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: auto; height: auto;">
                            <asp:GridView ID="gvHistory" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <%--<asp:BoundField DataField="RemarksType" HeaderText="Type" ItemStyle-Width="10%" />--%>
                                    <asp:BoundField DataField="RemarksBy" HeaderText="By" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" ItemStyle-Width="40%" />
                                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
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
                            <iframe runat="server" id="iframeview" name="iframe_a" height="500px" width="100%" title="Iframe Example"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
