<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="InvoiceDetails.aspx.vb" Inherits="TRACePA.InvoiceDetails" %>

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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Invoice</b></h2>
            </div>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <asp:Label ID="lblPKID" runat="server" Visible="false" Text="0"></asp:Label>
            <asp:Label ID="lblTotalBeforeWithTaxValue" runat="server" Visible="false" Text="0"></asp:Label>
            <asp:Label ID="lblTotalBeforeWithOutTaxValue" runat="server" Visible="false" Text="0"></asp:Label>
            <asp:Label ID="lblTotalAfterWithTaxValue" runat="server" Visible="false" Text="0"></asp:Label>
            <asp:Label ID="lblTotalAfterWithOutTaxValue" runat="server" Visible="false" Text="0"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHFY" runat="server" Text="Financial Year : "></asp:Label>
                <asp:Label ID="lblFY" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHCompanyName" Text="Billing Entity : " runat="server"></asp:Label>
                <asp:Label ID="lblCompanyName" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHCustomerName" runat="server" Text="Customer : "></asp:Label>
                <asp:Label ID="lblCustomerName" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
        <div class="col-sm-6 col-md-6">
            <div class="form-group">
                <asp:Label ID="lblHIT" Text="Invoice Type : " runat="server"></asp:Label>
                <asp:Label ID="lblIT" runat="server" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" id="divInvoiceDetails" runat="server" visible="false">
        <br />
        <div class="form-group" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
            <asp:GridView ID="gvInvoiceDetails" ShowFooter="True" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvInvoiceDetails_RowDataBound">
                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns>
                    <asp:TemplateField HeaderText="SrNo" ItemStyle-Width="02%">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>'></asp:Label>
                            <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                            <asp:Label ID="lblCustomerID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerID") %>'></asp:Label>
                            <asp:Label ID="lblIsTaxable" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsTaxable") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Taxable" ItemStyle-Width="03%">
                        <ItemTemplate>
                            <div style="text-align: center;">
                                <asp:CheckBox ID="chkIsTaxable" runat="server" CssClass="hvr-bounce-in" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="15%" />
                    <asp:BoundField DataField="Task" HeaderText="Task" ItemStyle-Width="10%" />
                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="27%">
                        <ItemTemplate>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="HSN/SAC" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblHSNSAC" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.HSNSAC") %>'></asp:Label>
                            <%--<asp:TextBox ID="txtHSNSAC" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.HSNSAC") %>'></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVHSNSAC" runat="server" ControlToValidate="txtHSNSAC" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid HSN/SAC." ValidationGroup="ValidateIV" ValidationExpression="^[0-9]{0,15}$"></asp:RegularExpressionValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="8%">
                        <HeaderTemplate>
                            <div style="text-align: right;">Quantity</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Quantity") %>' Style="text-align: right"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVQuantity" runat="server" ControlToValidate="txtQuantity" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Quantity." ValidationGroup="ValidateIV" ValidationExpression="^[0-9]{0,7}$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="8%">
                        <HeaderTemplate>
                            <div style="text-align: right;">Price Per Unit</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPricePerUnit" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.PricePerUnit") %>' Style="text-align: right"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPricePerUnit" runat="server" ControlToValidate="txtPricePerUnit" Display="Dynamic" SetFocusOnError="True" ErrorMessage="Enter Valid Price/Unit." ValidationGroup="ValidateIV" ValidationExpression="^[1-9]\d*(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="text-align: right;">
                                <asp:Button ID="btnCalculate" runat="server" CssClass="btn-ok" Text="Calculate Total" Font-Bold="true" data-toggle="tooltip" data-placement="top" title="Calculate Total" ValidationGroup="ValidateIV" OnClick="btnCalculate_Click"></asp:Button>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-Width="16%">
                        <HeaderTemplate>
                            <div style="text-align: right;">Amount</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div style="text-align: right;">
                                <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'></asp:Label>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="text-align: right;">
                                <asp:Label ID="lblTotalAmount" runat="server" Font-Bold="True"></asp:Label>
                            </div>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div id="divTaxes" runat="server" style="padding: 0px" visible="false">
        <div class="col-sm-8 col-md-8" style="padding-right: 0px">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-2 col-md-2" style="padding: 0px">
                    <div class="form-group" style="text-align: left">
                        <asp:Label ID="lblHNotes" Text="Notes to Invoice" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-10 col-md-10" style="padding: 0px">
                    <div class="form-group" style="text-align: left">
                        <asp:TextBox ID="txtNotes" runat="server" CssClass="aspxcontrols"> </asp:TextBox>
                        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNotes" runat="server" SetFocusOnError="True" ControlToValidate="txtNotes" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px 0px 5px 0px;">
                <div class="col-sm-2 col-md-2" style="padding: 0px">
                    <div class="form-group" style="text-align: left">
                        <asp:Label ID="lblHConditions" Text="Conditions" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-10 col-md-10" style="padding: 0px">
                    <div class="form-group" style="text-align: left">
                        <asp:Label ID="lblConditions" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px 0px 5px 0px;">
                <div class="col-sm-2 col-md-2" style="padding: 0px">
                    <div class="form-group" style="text-align: left">
                        <asp:Label ID="lblHPaymentTerms" Text="Payment Terms" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-10 col-md-10" style="padding: 0px">
                    <div class="form-group" style="text-align: left">
                        <asp:Label ID="lblPaymentTerms" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-2 col-md-2" style="padding: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblHSignature" Text="* Authorized Signatory" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-3 col-md-3" style="padding: 0px">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlSignature" runat="server" CssClass="aspxcontrols">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVSignature" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlSignature" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-sm-4 col-md-4">
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right; padding: 0px 0px 5px 0px;">
                        <asp:Label ID="lblHTotalBeforeTax" Text="Total Before Tax" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right; padding: 0px 0px 5px 0px;">
                        <asp:Label ID="lblTotalBeforeTaxValue" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right;">
                        <asp:Label ID="lblHTax1" Text="Tax Type 1" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlTaxType1" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="125px"></asp:DropDownList>
                        <%--<asp:RequiredFieldValidator ID="RFVTaxType1" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlTaxType1" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblTax1Name" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblTax1Percentage" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right;">
                        <asp:Label ID="lblHTax2" Text="Tax Type 2" runat="server"></asp:Label>
                        <asp:DropDownList ID="ddlTaxType2" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="125px" Enabled="false"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RFVTaxType2" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlTaxType2" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblTax2Name" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblTax2Percentage" runat="server" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblHTotalAfterTax" Text="Total After Tax" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblTotalAfterTaxValue" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblHAdvancePaid" Text="Advance Paid" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblAdvancePaid" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblHBalance" Text="Balance" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group" style="text-align: right">
                        <asp:Label ID="lblBalance" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-10 col-md-10">
        </div>
        <div class="col-sm-2 col-md-2" style="text-align: right;">
            <div class="form-group">
                <asp:Button ID="btnSavePreviewReport" runat="server" CssClass="btn-ok" Text="Save & Preview Report" Font-Bold="true" data-toggle="tooltip" data-placement="top" title="Save & Preview Report" Visible="false" ValidationGroup="Validate"></asp:Button>
                <%--<asp:Button ID="btnGenerateReport" runat="server" CssClass="btn-ok" Text="Generate Report" Font-Bold="true" data-toggle="tooltip" data-placement="top" title="Generate Report" Visible="false"></asp:Button>--%>
            </div>
        </div>
    </div>
    <div id="ModalInvoiceValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblInvoiceValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="450px" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
