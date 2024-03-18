<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ITReturnsFiling.aspx.vb" Inherits="TRACePA.ITReturnsFiling" %>

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
            $('#<%=ddlClientName.ClientID%>').select2();
            $('#<%=ddlAssignto.ClientID%>').select2();
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
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="IT Returns Filing" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" Visible="false" />
                </div>
            </div>
            </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" style="padding: 0px; margin-top: 10px;">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHDDLClientName" runat="server" Text="Client Name"></asp:Label>
                                <asp:DropDownList ID="ddlClientName" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100%"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHCompanyName" Text="* Billing Entity" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlCompanyName" runat="server" CssClass="aspxcontrols">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVCompanyName" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCompanyName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" InitialValue="Select Billing Entity" ErrorMessage="SelectBilling Entity."></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblHITRNo" runat="server" Text="ITR No : " Font-Bold="true" Visible="false"></asp:Label>
                                <asp:Label ID="lblITRNo" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="lblITRNoId" runat="server" Text="0" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px;">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHClientName" runat="server" Text="* Client Name"></asp:Label>
                                <asp:TextBox ID="txtClientName" runat="server" CssClass="aspxcontrols" MaxLength="500"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVClientName" runat="server" SetFocusOnError="True" ControlToValidate="txtClientName" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter Client Name."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVClientName" runat="server" ControlToValidate="txtClientName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[\s\S]{0,500}$" ErrorMessage="Client Name exceeded maximum size(max 500 characters)."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHFY" runat="server" Text="* Financial Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFVFinancialYear" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlFinancialYear" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" InitialValue="Select Financial Year" ErrorMessage="Select Financial Year."></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHAY" runat="server" Text="* Assessment Year"></asp:Label>
                                    <asp:DropDownList ID="ddlAssessmentYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Enabled="false"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFVAssessmentYear" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAssessmentYear" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" InitialValue="Select Assessment Year" ErrorMessage="Select Assessment Year."></asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHPAN" runat="server" Text="* PAN"></asp:Label>
                                <asp:TextBox ID="txtPAN" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPAN" runat="server" SetFocusOnError="True" ControlToValidate="txtPAN" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter PAN."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPAN" runat="server" ControlToValidate="txtPAN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[\s\S]{0,10}$" ErrorMessage="PAN exceeded maximum size(max 10 characters)."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHAadhaar" runat="server" Text="* Aadhaar"></asp:Label>
                                <asp:TextBox ID="txtAadhaar" runat="server" CssClass="aspxcontrols" MaxLength="15"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAadhaar" runat="server" SetFocusOnError="True" ControlToValidate="txtAadhaar" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter Aadhaar."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAadhaar" runat="server" ControlToValidate="txtAadhaar" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[\s\S]{0,15}$" ErrorMessage="Aadhaar exceeded maximum size(max 14 characters)."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHDOB" runat="server" Text="* DOB"></asp:Label>
                                <asp:TextBox ID="txtDOB" placeholder="dd/MM/yyyy" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDOB" runat="server" ControlToValidate="txtDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter DOB."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDOB" runat="server" ControlToValidate="txtDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter valid DOB." ValidationExpression="(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"></asp:RegularExpressionValidator>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDOB" PopupPosition="TopRight" TargetControlID="txtDOB" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                </cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHPhone" runat="server" Text="* Phone"></asp:Label>
                                <asp:TextBox ID="txtPhone" runat="server" CssClass="aspxcontrols" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPhone" runat="server" SetFocusOnError="True" ControlToValidate="txtPhone" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter Phone."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPhone" runat="server" ControlToValidate="txtPhone" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[0-9]{10}$" ErrorMessage="Enter valid Phone."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHEmail" runat="server" Text="* Email"></asp:Label>
                                <asp:TextBox ID="txtEmail" runat="server" CssClass="aspxcontrols" MaxLength="100"></asp:TextBox>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVEmail" runat="server" SetFocusOnError="True" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter Email."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ErrorMessage="Enter valid E-Mail." ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHAssignto" runat="server" Text="* Assign to"></asp:Label>
                                <asp:DropDownList ID="ddlAssignto" runat="server" CssClass="aspxcontrols" AutoPostBack="false" Width="100%">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVAssignto" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAssignto" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" InitialValue="Select Assign to" ErrorMessage="Select Assign to."></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHITLoginIdord" runat="server" Text="IT Login Id"></asp:Label>
                                <asp:TextBox ID="txtITLoginId" runat="server" CssClass="aspxcontrols" MaxLength="100"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVITLoginId" runat="server" SetFocusOnError="True" ControlToValidate="txtITLoginId" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter IT Login Id."></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVITLoginId" runat="server" ControlToValidate="txtITLoginId" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="IT Login Id exceeded maximum size(max 100 characters)."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHITPassword" runat="server" Text="IT Password"></asp:Label>
                                <asp:TextBox ID="txtITPassword" runat="server" CssClass="aspxcontrols" MaxLength="100"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVITPassword" runat="server" SetFocusOnError="True" ControlToValidate="txtITPassword" Display="Dynamic" ValidationGroup="Validate" ErrorMessage="Enter Password."></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVITPassword" runat="server" ControlToValidate="txtITPassword" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[\s\S]{0,100}$" ErrorMessage="IT Password exceeded maximum size(max 100 characters)."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblServiceChargeINR" runat="server" Text="Service charge in INR"></asp:Label>
                                <asp:TextBox ID="txtServiceChargeINR" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVServiceChargeINR" runat="server" SetFocusOnError="True" ControlToValidate="txtServiceChargeINR" Display="Dynamic" ValidationGroup="Validate" ErrorMessage = "Enter Service charge in INR."></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVServiceChargeINR" runat="server" ControlToValidate="txtServiceChargeINR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" ValidationExpression="^[1-9]\d*(\.\d{1,2})?$" ErrorMessage="Enter valid Service charge in INR."></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblHStatus" runat="server" Text="* Status"></asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFVStatus" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlStatus" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate" InitialValue="0" ErrorMessage="Select Status."></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-12 col-md-12">
                            <div class="form-group">
                                <br />
                                <asp:CheckBox ID="chkInvoiceEmail" CssClass="aspxradiobutton" runat="server" Text="Automatic generation of Invoice and mailing" AutoPostBack="false"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
                        <div class="col-sm-12 col-md-12" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:GridView ID="gvITR" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
<HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ITR No" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblITRFID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ITRFID") %>'></asp:Label>
                                            <asp:Label ID="lblClientID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClientID") %>'></asp:Label>
                                            <asp:Label ID="lblStatusID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.StatusID") %>'></asp:Label>
                                            <asp:LinkButton ID="lnkITRNo" runat="server" Font-Bold="True" Font-Italic="True" CommandName="Select" Text='<%# DataBinder.Eval(Container.DataItem, "ITRNo") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ClientNamePAN" HeaderText="Client Name (PAN)" ItemStyle-Width="35%" />
                                    <asp:BoundField DataField="Age" HeaderText="Age" ItemStyle-Width="10%" />
                                    <asp:BoundField DataField="Aadhaar" HeaderText="Aadhaar" ItemStyle-Width="20%" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="20%" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDownload" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Download Invoice" CommandName="Download" runat="server" />
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

    <div id="ModalITValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblITValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
