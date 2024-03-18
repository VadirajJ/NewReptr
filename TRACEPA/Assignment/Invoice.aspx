<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Invoice.aspx.vb" Inherits="TRACePA.Invoice" %>

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
        $(document).ready(function () {
            $('#<%=ddlCompanyName.ClientID%>').select2();
            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12 divmargin">
        <div class="pull-left">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Invoice" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAssign" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Assign" Visible="false" />
                </div>
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHFY" Text="* Financial Year" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHCompanyName" Text="* Billing Entity" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVCompanyName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCompanyName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHWorkStatus" runat="server" Text="* Work Status"></asp:Label>
                                <asp:DropDownList ID="ddlWorkStatus" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVWorkStatus" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlWorkStatus" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHMonth" runat="server" Text="Month"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblHCustomerName" runat="server" Text="Customer"></asp:Label>
                                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ID="RFVCustomerName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCustomerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHIT" Text="* Invoice Type" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlInvoiceType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVInvoiceType" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlInvoiceType" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnGo" runat="server" CssClass="btn-ok" Text="Go" Font-Bold="true" data-toggle="tooltip" data-placement="top" ValidationGroup="Validate"></asp:Button>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                        <asp:GridView ID="gvAssignmentDetails" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="02%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAsg" runat="server" CssClass="hvr-bounce-in" />
                                        <asp:Label ID="lblPKID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PKID") %>'></asp:Label>
                                        <asp:Label ID="lblCustomerID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerID") %>'></asp:Label>
                                        <asp:Label ID="lblAssignmentID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblAssignmentNo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssignmentNo") %>'></asp:Label>
                                        <asp:Label ID="lblInvoiceTypeID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InvoiceTypeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SrNo" HeaderText="SrNo" ItemStyle-Width="02%" />
                                <asp:BoundField DataField="Customer" HeaderText="Customer" ItemStyle-Width="25%" />
                                <asp:BoundField DataField="AssignmentNo" HeaderText="Assignment No" ItemStyle-Width="18%" />
                                <asp:BoundField DataField="Task" HeaderText="Task" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Invoice" HeaderText="Invoice" ItemStyle-Width="13%" />
                                <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="9%" />
                                <asp:TemplateField ItemStyle-Width="10%">
                                    <HeaderTemplate>
                                        <div style="text-align: right;">Amount</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div style="text-align: right;">
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Amount") %>'></asp:Label>
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
</asp:Content>
