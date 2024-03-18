<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Search.aspx.vb" Inherits="TRACePA.Search" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
            $('#<%=lstDesc.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
            });
        });
    </script>

    <div class="loader"></div>

    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa-solid fa-magnifying-glass" style="font-size: large"></i>&nbsp;
             
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Search" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAddToCollation" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add to Collation" CausesValidation="false" Visible="false" />
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnCorrespondance" CssClass="activeIcons hvr-bounce-out" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Send to Correspondence" />
                </div>
            </div>
            </div>
        <div class="card">
            <br />
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-4 col-md-4" style="padding: 0px">
                    <asp:Label ID="lblIndex" runat="server" Text="Index Of" Font-Bold="true"></asp:Label>
                    <asp:DropDownList ID="ddlIndex" runat="server" CssClass="aspxcontrols" TabIndex="2" AutoPostBack="true">
                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Cabinets"></asp:ListItem>
                        <asp:ListItem Value="2" Text="SubCabinets"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Folders"></asp:ListItem>
                        <asp:ListItem Value="4" Text="DocumentTypes"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Keywords"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Descriptors"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Format"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Created by"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label ID="lblDesc" runat="server" Text="Description" Font-Bold="true" Width="250px"></asp:Label>
                    <div class="row">
                        <div class="col-sm-12 col-md-12">
                            <asp:ListBox ID="lstDesc" runat="server" SelectionMode="Multiple" CssClass="aspxcontrols" Width="500px"></asp:ListBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-5 col-md-5">
                    <br />
                    <asp:Button runat="server" Text="Search" class="btn-ok" ID="btnAddQuery" Width="100px"></asp:Button>
                    <asp:Button runat="server" Text="Reset" class="btn-ok" ID="btnReset" Width="100px"></asp:Button>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto;">

                <div class="col-sm-4 col-md-4" style="padding: 0px">
                    <asp:GridView ID="dgParam" runat="server" ShowHeader="true" CssClass="table bs" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" PageSize="5000">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="Fields" HeaderText="Fields"></asp:BoundField>
                            <asp:TemplateField HeaderText="Add Parameter">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtParam" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container.DataItem, "SelectedName") %>'></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="SelectedID" HeaderText="SelectedID" Visible="False"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-8 col-md-8">
                    <asp:GridView ID="dgViewSearchData" runat="server" ShowHeader="true" CssClass="table bs" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" PageSize="5000">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField DataField="DetailsId" HeaderText="DetailsID" Visible="False"></asp:BoundField>
                            <asp:BoundField DataField="BaseID" Visible="False" HeaderText="BaseID"></asp:BoundField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="48%">
                                <ItemTemplate>
                                    <asp:Label ID="lblBaseName" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DetailsId") %>'></asp:Label>
                                    <asp:Label ID="lblDetailsID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.BaseID") %>'></asp:Label>
                                    <asp:LinkButton ID="lnkTitle" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.Title") %>' CssClass="aspxlabelbold"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cabinet" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="CabName" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.CabName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sub Cabinet" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="SubCabName" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.SubCabName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Folder" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="FolName" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.FolName") %>'></asp:Label>
                                    <asp:Label ID="FoldID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.FoldID") %>' CssClass="aspxlabelbold"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="CabName" HeaderText="Cabinet" Visible="false"></asp:BoundField>--%>
                            <%--<asp:BoundField DataField="SubCabName" HeaderText="Sub Cabinet" Visible="false"></asp:BoundField>--%>
                            <%--<asp:BoundField DataField="FolName" HeaderText="Folder" Visible="false"></asp:BoundField>--%>
                            <asp:BoundField DataField="DocType" HeaderText="Document Types" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="48%"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>

            </div>
            <div id="myModalSelectedData" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content row">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                        <div class="modal-body row">
                            <div class="col-sm-12 col-md-12">
                                <div class="pull-left">
                                    <asp:Label ID="lblModelErrorSelectedData" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <asp:GridView ID="dgSelectedData" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundField DataField="Details" HeaderText="Details" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField DataField="Descriptors" HeaderText="Descriptors" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField DataField="KeyWords" HeaderText="KeyWords" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%"></asp:BoundField>
                                        <asp:BoundField DataField="ScanDocument" HeaderText="Scan Document" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="25%"></asp:BoundField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="myModal" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content row">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                            <h4 class="modal-title"><b>Collation list</b></h4>
                        </div>
                        <div class="modal-body row">
                            <div class="col-sm-12 col-md-12">
                                <div class="pull-left">
                                    <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <asp:DataGrid ID="dgCollation" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable">
                                    <HeaderStyle Font-Bold="true" BackColor="#ddeaf9" />
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCollationSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblColId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CLT_COLLATENO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="clt_collateref" HeaderText="Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="clt_group" HeaderText="Group" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="usr_fullname" HeaderText="Created By" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="clt_createdon" HeaderText="Created On" Visible="false"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="clt_comment" HeaderText="Note" Visible="false"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="pull-right">
                                <asp:Button runat="server" Text="New" class="btn-ok" ID="btnCollationNew"></asp:Button>
                                <asp:Button runat="server" Text="OK" class="btn-ok" ID="btnCollationSave" ValidationGroup="ValidateCollation"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>






    <div id="ModalSearchValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSearchValidationMsg" runat="server"></asp:Label></strong>
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
    <asp:Label ID="lblTitle" runat="server"></asp:Label>
    <div id="ModalSearchLinkValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgLinkType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSearchLinkValidationMsg" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" Text="Ok" class="btn-ok" ID="btnSearchLinkMsgOk" OnClick="btnSearchLinkMsgOk_Click"></asp:Button>
                    <asp:Button runat="server" Text="Cancel" class="btn-ok" ID="btnCancel" OnClick="btnCancel_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
