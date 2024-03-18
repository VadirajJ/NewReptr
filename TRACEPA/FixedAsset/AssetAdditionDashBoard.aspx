<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="AssetAdditionDashBoard.aspx.vb" Inherits="TRACePA.AssetAdditionDashBoard" %>

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
        /*  div {
            color: black;
        }*/
    </style>

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlStatus.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });
        $('#<%=GvAsserDetails.ClientID%>').DataTable({
            iDisplayLength: 20,
            aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
            order: [],
            columnDefs: [{ orderable: false, targets: [0] }],
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        });
    </script>

    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-right: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa-solid fa-radiation" style="font-size: large"></i>&nbsp;
     
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Asset Addition DashBoard" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                     <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Visible="false" Text="Download PDF" Style="margin: 0px;" /></li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">


            <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                    <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                    <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label ID="lblOpeningBalance" runat="server" ForeColor="DarkGreen" Font-Bold="true"></asp:Label><br />
                    <asp:Label ID="lblAddition" runat="server" ForeColor="DarkGreen" Font-Bold="true"></asp:Label>
                </div>
            </div>

            <div class="clearfix divmargin"></div>

            <%--<div class="col-sm-12 col-md-12" >
        <asp:GridView ID="GvAsserDetails" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">--%>
            <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium">
                <asp:GridView ID="GvAsserDetails" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                   <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                            <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                 <asp:Label ID="lblDescID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AFAA_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDescName" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AFAA_AssetNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%"></asp:BoundField>
                        <asp:BoundField DataField="AFAA_AssetNo" HeaderText="Voucher No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%"></asp:BoundField>
                        <asp:BoundField DataField="AssetType" HeaderText="Asset Class" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="AssetItemCode" HeaderText="Asset" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="AssetRefNo" HeaderText="Asset Transaction ReferenceNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="false" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="TransactionType" HeaderText="Transaction Type" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="AFAA_ADDAmount" HeaderText="Amount" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="20%"></asp:BoundField>
                        <asp:BoundField DataField="PurchaseDate" HeaderText="PurchaseDate" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%"></asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" ></asp:BoundField>
                         <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit1" runat="server" ToolTip="Edit" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>

        </div>
    </div>






    <div id="ModalasserAddnValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAdditionValidationMsg" runat="server"></asp:Label></strong>
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
    <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <button type="button" class="close" id="btnClose" data-dismiss="modal">&times;</button>
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModal" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button ID="btnYes" runat="server" Text="Yes" CssClass="btn-ok" />
                    <asp:Button ID="btnNo" runat="server" Text="No" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
