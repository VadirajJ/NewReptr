<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerUser.Master" CodeBehind="UploadDocument.aspx.vb" Inherits="TRACePA.UploadDocument" %>

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

        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px
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
            $('#<%=gvUploadedDocument.ClientID%>').DataTable({
                //initComplete: function () {
                //    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                //},
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            
            $('#<%=gvSharedDocuments.ClientID%>').DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=gvSharedIndexDocuments.ClientID%>').DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=gvActivity.ClientID%>').DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
            $('#<%=gvDigitalFilingDashboard.ClientID%>').DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>

    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="col-sm-7 col-md-7 pull-left" style="padding: 0px">
                    <%--<h2><b>Digital Office Dashboard</b></h2>--%>
                   <h2><b> Uploaded Documents </b></h2>
                </div>
                <div class="col-sm-5 col-md-5 pull-right" style="padding: 0px">
                    <div class="pull-right">
                        <asp:ImageButton CssClass="activeIcons hvr-bounce-out" ID="imgbtnUploadDocuments" runat="server" data-toggle="tooltip" data-placement="bottom" title="Upload Documents"></asp:ImageButton>
                        <ul class="nav navbar-nav navbar-right logoutDropdown">
                            <li class="dropdown">
                                <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                    <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" visible="false" /></span></a>
                                <ul class="dropdown-menu">
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                                    </li>
                                    <li role="separator" class="divider"></li>
                                    <li>
                                        <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" />
                                    </li>
                                </ul>
                            </li>
                        </ul>
                    </div>
                    <%-- <div class="pull-right">
                        <asp:ImageButton ID="imgbtnIndex" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index File" />
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
    <div class="clearfix divmargin"></div>
    <div class="col-sm-12 col-md-12" style="padding-right: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
    <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server" visible="false">
        <div id="div2" runat="server">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li id="liUploadedDocuments" runat="server" visible="false">
                    <asp:LinkButton ID="lnkbtnUploadedDocuments" Text="Uploaded Documents" runat="server" Font-Bold="true" /></li>
                <li id="liSharedDocuments" runat="server" visible="false">
                    <asp:LinkButton ID="lnkbtnSharedDocuments" Text="Shared/Indexed Documents" runat="server" Font-Bold="true" /></li>
                <li id="liActivity" runat="server" visible="false">
                    <asp:LinkButton ID="lnkbtnActivity" Text="Activity" runat="server" Font-Bold="true" /></li>
                <div runat="server" class="pull-right form-group" style="visibility:hidden">
                    <asp:Label ID="lblCustomer" runat="server" Text="Customers: " Font-Bold="true"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlCustomer" AutoPostBack="true">
                    </asp:DropDownList>
                </div>
            </ul>
        </div>

        <!-- Tab panes -->
        <div class="tab-content divmargin">
            <%--<div class="col-sm-12 col-md-12" style="visibility:hidden;">
                <asp:GridView ID="gvattach" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader"
                    ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvattach_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                                <asp:Label ID="lblAtchDocID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                <asp:Label ID="lblDFAttachID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "DFAttachID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Path" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblPath" runat="server" Visible="True" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File Name">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFileName" runat="server" CommandName="View" Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="16%"></asp:BoundField>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>--%>


            <%--Uploaded Documents Tab--%>
            <div runat="server" role="tabpanel" class="tab-pane active" id="divUploadedDocuments" >
                <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                    <div class="col-sm-10 col-md-10 pull-left" style="padding: 0px">
                        <asp:Label ID="lblUploadedDocuments" runat="server" Text="Uploaded Documents" CssClass="h5" Font-Bold="true" Visible="false"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 col-md-12">
                    <asp:GridView ID="gvUploadedDocument" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblAtchDocID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                    <asp:Label ID="lblDFAttachID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "DFAttachID") %>'></asp:Label>
                                    <asp:Label ID="lblPath" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                </ItemTemplate>

<HeaderStyle Width="1%"></HeaderStyle>
                            </asp:TemplateField>
                          
                            <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="20%">
<HeaderStyle Width="20%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="15%">
<HeaderStyle Width="15%"></HeaderStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-Width="20%">
<ItemStyle Width="20%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="16%">
<HeaderStyle Width="16%"></HeaderStyle>
                            </asp:BoundField>
                            <%--<asp:TemplateField>
                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnShareDocument" data-toggle="tooltip" data-placement="bottom" title="Share Document" CommandName="ShareDocument" runat="server" CssClass="hvr-bounce-in" /><br />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField Visible="true">
                                <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Index" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" ImageUrl="~/Images/Edit16.png" /><br />
                                    <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" ImageUrl="~/Images/Trash16.png" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                            </asp:TemplateField>
                           
                        </Columns>
                        <EmptyDataTemplate>No Records Available</EmptyDataTemplate>

<HeaderStyle CssClass="FixedHeader"></HeaderStyle>
                    </asp:GridView>
                </div>
                 
            </div>
             

            <%--Shared Documents Tab--%>
            <div runat="server" role="tabpanel" class="tab-pane" id="divSharedDocuments">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblSharedDocuments" runat="server" Text="Shared Documents" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <asp:GridView ID="gvSharedDocuments" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="DocumentType" HeaderText="Document Type" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="FileName" HeaderText="File Name" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="SharedBy" HeaderText="Shared By" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="SharedOn" HeaderText="Shared On" ItemStyle-Width="25%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                </asp:GridView>
                <br />
                <br />
                <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
                    <asp:Label ID="lblSharedIndexDocuments" runat="server" Text="Indexed Documents" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <asp:GridView ID="gvSharedIndexDocuments" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="FileName" HeaderText="File Name" ItemStyle-Width="25%" />
                        <asp:BoundField DataField="Cabinet" HeaderText="Cabinet" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="SubCabinet" HeaderText="Sub Cabinet" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="Folder" HeaderText="Folder" ItemStyle-Width="10%" />
                        <asp:BoundField DataField="IndexedBy" HeaderText="Indexed By" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="IndexedOn" HeaderText="Indexed On" ItemStyle-Width="10%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                </asp:GridView>
            </div>

            <%--Activity Tab--%>
            <div runat="server" role="tabpanel" class="tab-pane" id="divActivity">
                <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="lblActivity" runat="server" Text="Activity" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>
                <asp:GridView ID="gvActivity" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" EmptyDataText="No records Found">
                    <Columns>
                        <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="DocumentType" HeaderText="Document Type" ItemStyle-Width="20%" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="SharedBy" HeaderText="Shared By" ItemStyle-Width="15%" />
                        <asp:BoundField DataField="SharedOn" HeaderText="Shared On" ItemStyle-Width="15%" />
                    </Columns>
                    <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; visibility: hidden;">
        <div class="form-group">
            <asp:GridView ID="gvDigitalFilingDashboard" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:BoundField DataField="SlNo" HeaderText="Sr.No">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="2%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Cabinet" HeaderText="Cabinet" />
                    <asp:BoundField DataField="SubCabinet" HeaderText="Sub Cabinet" />
                    <asp:BoundField DataField="Folder" HeaderText="Folder" />
                    <asp:TemplateField HeaderText="Documents">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDocumentType" Font-Italic="true" runat="server" CommandName="Document" Text='<%# DataBinder.Eval(Container, "DataItem.DocumentType") %>'></asp:LinkButton>
                            <asp:Label ID="lblCabinetID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CabinetID") %>'></asp:Label>
                            <asp:Label ID="lblSubCabinetID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubCabinetID") %>'></asp:Label>
                            <asp:Label ID="lblFolderID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.FolderID") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="14%" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div id="myModalUploadDocuments" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attach Documents</b></h4>
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
                            <div class="col-sm-4 col-md-4" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtFile" runat="server" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Index Details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="pull-left">
                                <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblcabinet" runat="server" Text="Cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="DescriptorID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptorID" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptorID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Descriptor" HeaderText="Descriptor" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Value") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="100%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Key") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div>

    <div id="ModalDigitalFilingDashboardValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDigitalFilingDashboardValidationMsg" runat="server"></asp:Label>
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

    <div id="mySendMail" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="pull-left">
                            <h4 class="modal-title"><b>Send Mail</b></h4>
                        </div>
                    </div>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblSendMailModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmailFrom" runat="server" Text="* From"></asp:Label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmailFrom" runat="server" ControlToValidate="txtEmailFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtEmailFrom" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmailFrom" runat="server" ControlToValidate="txtEmailFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblPassword" runat="server" Text="* Password"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVPassword" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtPassword" autocomplete="off" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblEmailTo" runat="server" Text="* To"></asp:Label>
                                <asp:TextBox ID="txtEmailTo" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmailTo" runat="server" ControlToValidate="txtEmailTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblUsers" runat="server" Text="* Users"></asp:Label>
                                <br />
                                <asp:ListBox ID="lstUsers" runat="server" Width="100%" SelectionMode="Multiple" CssClass="aspxcontrols1"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblSubject" runat="server" Text="* Subject"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVSubject" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtSubject" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVSubject" runat="server" ControlToValidate="txtSubject" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <asp:Label ID="lblBody" runat="server" Text="* Body"></asp:Label>
                            <asp:RequiredFieldValidator ID="RFVBody" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtBody" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtBody" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVBody" runat="server" ControlToValidate="txtBody" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Send Mail" class="btn-ok" ID="btnSendMail" ValidationGroup="Validate"></asp:Button>
                        <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCancelMail"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>


