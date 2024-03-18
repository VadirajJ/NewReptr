<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerUserMaster.Master" CodeBehind="DigitalVouching.aspx.vb" Inherits="TRACePA.DigitalVouching" %>

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
    <link href="../StyleSheet/sweetalert.css" rel="stylesheet" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script src="../JavaScripts/sweetalert-dev.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=gvUploadedDocument.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 4] }],
            });
        });
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Digital Vouching Dashboard</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <asp:ImageButton CssClass="activeIcons hvr-bounce-out" ID="imgbtnUploadDocuments" runat="server" data-toggle="tooltip" data-placement="bottom" title="Upload Documents"></asp:ImageButton>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-6 col-md-6">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="pull-right">
                <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                <asp:DropDownList ID="ddlCustomer" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group pull-left" style="padding: 0px">
        <div class="col-sm-10 col-md-10 pull-left">
            <asp:Label ID="lblUploadedDocuments" runat="server" Text="Uploaded Documents" CssClass="h5"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto;">
        <asp:GridView ID="gvUploadedDocument" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" Width="100%" class="table bs">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblAtchDocID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                        <asp:Label ID="lblCabId" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "cbn_id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SrNo" HeaderText="SrNo" HeaderStyle-Width="2%"></asp:BoundField>
                <asp:BoundField DataField="Cabinet" HeaderText="Cabinet/Customer" HeaderStyle-Width="20%"></asp:BoundField>
                <asp:BoundField DataField="Trtype" HeaderText="Sub Cabinet" HeaderStyle-Width="15%"></asp:BoundField>
                <asp:BoundField DataField="Batch" HeaderText="Transaction Type" ItemStyle-Width="20%"></asp:BoundField>
                <asp:BoundField DataField="NFT" HeaderText="No of Transactions" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="Cr_By" HeaderText="Created By" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:BoundField DataField="BT_Date" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                <asp:TemplateField Visible="false">
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnShareDocument" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Share Document" CommandName="ShareDocument" runat="server" CssClass="hvr-bounce-in" /><br />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="View" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" /><br />
                        <asp:ImageButton ID="imgbtnRemove" Visible="false" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
        </asp:GridView>
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
                            <div class="col-sm-5 col-md-5" style="padding: 0px; width: 30%;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtFile" runat="server" AllowMultiple="true" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Visible="false" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnIndex" runat="server" Text="Index" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2" style="padding: 0px;">
                                <div class="form-group">
                                    <br />
                                    <asp:Label ID="lblDate" runat="server" Visible="false" Text="Date"></asp:Label>
                                    <asp:Label ID="lblDateDisplay" runat="server" Visible="false" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <div class="col-sm-3 col-md-3">
                                    <asp:Label ID="lblcabinet" runat="server" Text="* Customer/Cabinet"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlCabinet" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCabinet" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label ID="lblSubcabinet" runat="server" Text="* Sub Cabinet"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RFVSubCabinet" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlSubcabinet" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="col-sm-3 col-md-3">
                                    <asp:Label ID="lblFolder" runat="server" Text="* Transaction Type"></asp:Label>
                                    <asp:DropDownList ID="ddlfolder" AutoPostBack="true" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                               <%-- <div class="col-sm-3 col-md-3">
                                    <asp:Label ID="lblFolder" runat="server" Text="Batch/Folder"></asp:Label>
                                    <asp:DropDownList ID="ddlfolder" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>--%>
                                <div class="col-sm-2 col-md-2">
                                    <asp:Label ID="lblNFT" runat="server" Text="No. of Transactions"></asp:Label>
                                    <asp:TextBox ID="txtNFT" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <asp:Label ID="lbldebit" Visible="false" runat="server" Text="Debit note"></asp:Label>
                                    <asp:TextBox ID="txtdebit" Visible="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                                <div class="col-sm-2 col-md-2">
                                    <asp:Label ID="lblcredit" Visible="false" runat="server" Text="Credit note"></asp:Label>
                                    <asp:TextBox ID="txtcredit" Visible="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCabinet" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFolder" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlSubCabinet" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalGeneralMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblADOValidationMsg" runat="server"></asp:Label>
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
