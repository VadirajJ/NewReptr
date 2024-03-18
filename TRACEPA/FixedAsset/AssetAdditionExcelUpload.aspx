<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="AssetAdditionExcelUpload.aspx.vb" Inherits="TRACePA.AssetAdditionExcelUpload" %>

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
            $('#<%=ddlTransactionType.ClientID%>').select2();
            $('#<%=ddlSheetName.ClientID%>').select2();
        });
        $('#<%=GvAdditionExcel.ClientID%>').DataTable({
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
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-7 col-md-7 ">
                <h2><b>Asset Addition Excel Upload</b></h2>
            </div>
            <br />
            <div class="pull-right">
                <asp:ImageButton ID="ImgBtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                <asp:ImageButton ID="imgbtUpload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save Details" Visible="false" />
                <asp:ImageButton ID="ImgbtnUpload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save Details" />
                <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <%--  <div class="clearfix divmargin"></div>--%>

    <div class="col-sm-12 col-md-12 row">
        <%--<asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>--%>
        <div class="col-sm-6 col-md-6">
            <div class="pull-left">
                <asp:Label ID="Label1" runat="server" Text="Red - * Mandatory" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Black - * Not Mandatory" CssClass="ErrorMsgLeft" ForeColor="Black"></asp:Label>
            </div>
        </div>

        <div class="col-sm-6 col-md-6">
            <div class="pull-right">
                <asp:LinkButton ID="lnDown" ForeColor="Green" runat="server">Download sample excel file</asp:LinkButton>
            </div>
        </div>
    </div>

    <div class="clearfix divmargin"></div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="* Customer Name"></asp:Label>
            <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
            <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
        <%--<div class="col-sm-2 col-md-2 pull-right" style="padding: 0px">
            <asp:Label ID="Label1" runat="server" Text="Red - * Mandatory" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="Black - * Not Mandatory" CssClass="ErrorMsgLeft" ForeColor="Black"></asp:Label>
        </div>
         <div class="col-sm-2 col-md-2 pull-right ">
            <asp:LinkButton ID="lnDown" ForeColor ="Green" runat="server">Download sample excel file</asp:LinkButton>
        </div>--%>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0">
        <div class="col-sm-3 col-md-3 ">
            <asp:Label ID="Label2" runat="server" Text="* Transaction Type"></asp:Label>
            <asp:DropDownList ID="ddlTransactionType" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>

    </div>

    <div class="col-sm-3 col-md-3">
        <div class="form-group">
            <br />
            <asp:Label ID="lblSelectFile" runat="server" Text=""></asp:Label>
            <asp:FileUpload ID="FULoad" CssClass="aspxcontrols" runat="server" />
        </div>
        <asp:TextBox ID="txtPath" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" Style="height: 21px" />
    </div>
    <div class="col-sm-1 col-md-1">
        <div class="form-group">
            <div style="margin-top: 20px;"></div>
            <asp:Button ID="btnOk" runat="server" Text="Validate" OnClick="btnOk_Click" />
        </div>
    </div>
    <div class="col-sm-3 col-md-3 pull-center" style="padding-right: 0">
        <div class="form-group">
            <asp:Label ID="lblSheetName" runat="server" Text="Sheet Name"></asp:Label>
            <asp:DropDownList ID="ddlSheetName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
    </div>
    <%--       <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto"">
            <asp:GridView ID="GvAdditionExcel" runat="server" CssClass="footable" AutoGenerateColumns="False">--%>

    <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: scroll; overflow-x: unset">
        <asp:GridView ID="GvAdditionExcel" Visible ="false"  CssClass="table bs"   RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
           <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
            <Columns>
                <asp:TemplateField HeaderText="Slno">
                    <ItemTemplate>
                        <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Slno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Location">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Division">
                    <ItemTemplate>
                        <asp:Label ID="lblDivision" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Division") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Bay">
                    <ItemTemplate>
                        <asp:Label ID="lblBay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Bay") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asset Class">
                    <ItemTemplate>
                        <asp:Label ID="lblAssetClass" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetClass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asset">
                    <ItemTemplate>
                        <asp:Label ID="lblAsset" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Asset") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Asset Location">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetLocation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Date Of Purchase">
                    <ItemTemplate>
                        <asp:Label ID="lblDateOfPurchase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfPurchase") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Original Cost">
                    <ItemTemplate>
                        <asp:Label ID="lblOriginalCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OriginalCost") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="WDV Opening Value">
                    <ItemTemplate>
                        <asp:Label ID="lblWDVOpeningValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WDVOpeningValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Dep. For the Period">
                    <ItemTemplate>
                        <asp:Label ID="lblDepForthePeriod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepForthePeriod") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>


    <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: scroll; overflow-x: unset">
        <asp:GridView ID="gvAssetAddition"  Visible ="false"  CssClass="table bs"  RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
            <Columns>
                <asp:TemplateField HeaderText="Slno">
                    <ItemTemplate>
                        <asp:Label ID="lblSlno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Slno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Location">
                    <ItemTemplate>
                        <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Division">
                    <ItemTemplate>
                        <asp:Label ID="lblDivision" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Division") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Bay">
                    <ItemTemplate>
                        <asp:Label ID="lblBay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Bay") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asset Class">
                    <ItemTemplate>
                        <asp:Label ID="lblAssetClass" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetClass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asset">
                    <ItemTemplate>
                        <asp:Label ID="lblAsset" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Asset") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%-- <asp:TemplateField HeaderText="Asset Location">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetLocation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Supplier Name">
                    <ItemTemplate>
                        <asp:Label ID="lblSupplierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Particulars">
                    <ItemTemplate>
                        <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Particulars") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Doc No">
                    <ItemTemplate>
                        <asp:Label ID="lblDocNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DocNo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Doc Date">
                    <ItemTemplate>
                        <asp:Label ID="lblDocDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DocDate") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Basic Cost">
                    <ItemTemplate>
                        <asp:Label ID="lblBasicCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BasicCost") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Tax Amount">
                    <ItemTemplate>
                        <asp:Label ID="lblTaxAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TaxAmount") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="AssetValue">
                    <ItemTemplate>
                        <asp:Label ID="lblAssetValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetValue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>


    <div id="ModalFASFXDAdditionExcel" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblFXAdditionExcelMsg" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                        OK
                    </button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
