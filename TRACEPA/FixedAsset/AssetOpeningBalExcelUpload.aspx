<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="AssetOpeningBalExcelUpload.aspx.vb" Inherits="TRACePA.AssetOpeningBalExcelUpload" %>

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
        /* div {
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
            $('#<%=ddlSheetName.ClientID%>').select2();
        });

        $('#<%=GvOPExcel.ClientID%>').DataTable({
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
            <div class="col-sm-8 col-md-8">
                <h2><b>Asset Creation Excel Upload</b></h2>
            </div>
            &nbsp;&nbsp
            <div class="pull-right">
                <div class="pull-right">
                    <asp:ImageButton ID="ImgBtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtUpload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save Details" Visible="false" />
                    <asp:ImageButton ID="ImgbtnUpload" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save Details" />
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>

    <%--    <div class="clearfix divmargin"></div>--%>

    <div class="col-sm-12 col-md-12 row">
        <%--<asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>--%>
        <div class="col-sm-6 col-md-6">
            <div class="pull-left">
                <asp:Label ID="Label2" runat="server" Text="Red - * Mandatory" CssClass="ErrorMsgLeft" ForeColor="Red"></asp:Label>
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


    <asp:Panel runat="server" Visible="false">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="Zone"></asp:Label>
                <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                </asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="Region"></asp:Label>
                <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="Area"></asp:Label>
                <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
            <div class="col-sm-2 col-md-2">
                <asp:Label runat="server" Text="Branch"></asp:Label>
                <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
        </div>
    </asp:Panel>

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

    <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: scroll; overflow-x: unset">
        <asp:GridView ID="GvOPExcel" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
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
                        <asp:Label ID="lblAssetType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetClass") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--  <asp:TemplateField HeaderText="Asset Code" Visible ="false" >
                        <ItemTemplate>
                            <asp:Label ID="lblAssetCode" runat="server" Visible ="false" Text='<%# DataBinder.Eval(Container.DataItem, "AssetCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>    --%>

                <%-- <asp:TemplateField HeaderText="Asset Class Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="Asset Code">
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetCode") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Asset Description">
                    <ItemTemplate>
                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetDescription") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date Of Commission(Put to Use)">
                    <ItemTemplate>
                        <asp:Label ID="lblDateOfCommission" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateOfCommission") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Units of Measurement">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitsofMeasurement" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UnitsofMeasurement") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="Date of Purchase">
                        <ItemTemplate>
                            <asp:Label ID="lblDateofPurchase" Visible ="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateofPurchase") %>'></asp:Label>                         
                        </ItemTemplate>
                    </asp:TemplateField>--%>


                <asp:TemplateField HeaderText="Useful life of Asset">
                    <ItemTemplate>
                        <asp:Label ID="lblAssetAge" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetAge") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <%--<asp:TemplateField HeaderText="Amount" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>
    </div>


    <div id="ModalFASFXDOpExcel" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblFXOPBalExcelMsg" runat="server"></asp:Label>
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

