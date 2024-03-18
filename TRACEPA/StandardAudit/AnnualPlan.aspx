<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="AnnualPlan.aspx.vb" Inherits="TRACePA.AnnualPlan" %>

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
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Annual Plan" Font-Size="Small"></asp:Label>
               
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblHFY" Text="Financial Year" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2">
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:GridView ID="gvPlanShedule" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvPlanShedule_RowDataBound">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer" ItemStyle-Width="35%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAuditID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AuditID") %>'></asp:Label>
                                            <asp:Label ID="lblCustomerID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerID") %>'></asp:Label>
                                            <asp:Label ID="lblCheckPointID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CheckPointID") %>'></asp:Label>
                                            <asp:LinkButton ID="lnkCustomerName" Font-Italic="true" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AuditNo" HeaderText="Previous/Current Audit No." ItemStyle-Width="25%" />
                                    <asp:BoundField DataField="AuditType" HeaderText="Audit Type" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="AuditDate" HeaderText="Audit Date" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="AuditStatus" HeaderText="Audit Status" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
