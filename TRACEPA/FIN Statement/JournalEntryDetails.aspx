<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="JournalEntryDetails.aspx.vb" Inherits="TRACePA.JournalEntryDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

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
    </style>

    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>

    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlExistJE.ClientID%>').select2();
            $('#<%=ddlParty.ClientID%>').select2();
            $('#<%=ddlBillType.ClientID%>').select2();
            $('#<%=ddldbHead.ClientID%>').select2();
            $('#<%=ddldbGL.ClientID%>').select2();
            $('#<%=ddldbSubGL.ClientID%>').select2();
            $('#<%=ddlCrHead.ClientID%>').select2();
            $('#<%=ddlCrGL.ClientID%>').select2();
            $('#<%=ddlCrSubGL.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
        function CalculateBalance() {
            if (document.getElementById('<%=txtBillAmount.ClientID %>').value != "") {
                if (document.getElementById('<%=txtAdvancePayment.ClientID %>').value != "") {

                    var sBIllAmount = document.getElementById('<%=txtBillAmount.ClientID %>').value
                    var sAdvanceAmount = document.getElementById('<%=txtAdvancePayment.ClientID %>').value

                    document.getElementById('<%=txtBalanceAmount.ClientID %>').value = sBIllAmount - sAdvanceAmount
                }
            }
        }
    </script>
     <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px; left: 0px; top: 0px;">
        <div class="col-sm-12 col-md-12" style="left: 0px; top: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="loader"></div>
            <div class="card">
                <div runat="server" id="divCompheader" class="card-header">
            <div class="col-sm-9 col-md-9 pull-left">
                <h4><b>Journal Entry Transaction Details</b> </h4>
            </div>
                    
            <div class="col-sm-3 col-md-3">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnApprove" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" ValidationGroup="Validate" />
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" /></li>
                            </ul>
                    
                </div>
            </div>
        </div>
    </div>
            <div class="card">
    <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblParty" Text="* Customer" runat="server"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVParty" runat="server" ControlToValidate="ddlParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/Party." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <%-- <div class="col-sm-3 col-md-3">
                <asp:Label ID="lblscheduletype" Text="Schedule Type" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlscheduletype" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    <asp:ListItem Value="0">Select Scheduler type</asp:ListItem>
                    <asp:ListItem Value="1">Manufacturing</asp:ListItem>
                    <asp:ListItem Value="2">Trading</asp:ListItem>
                    <asp:ListItem Value="3">Balance Sheet</asp:ListItem>
                    <asp:ListItem Value="4">P & L</asp:ListItem>
                </asp:DropDownList>
            </div>--%>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label ID="lblYears" runat="server" Text="Financial Year"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label ID="LblBranch" runat="server" Text="* Branch Name"></asp:Label>
                    <asp:DropDownList ID="ddlBranch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-3 col-md-3">
                <br />
                <asp:Label ID="lblStatusH" runat="server" Text="Status : "></asp:Label>
                <asp:Label ID="lblStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
            </div>

            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <br />
                    <a href="#">
                        <div id="imgbtnHistory" runat="server" visible="false" data-toggle="modal" data-target="#myModalHistory"><b><i>History</i></b></div>
                    </a>
                </div>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Existing Journal Voucher"></asp:Label>
                <asp:DropDownList ID="ddlExistJE" runat="server" CssClass="aspxcontrols" AutoPostBack="True" ValidationGroup="Validate"></asp:DropDownList>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Transaction No"></asp:Label>
                <asp:TextBox ID="txtTransactionNo" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Bill Date"></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REFBillDate" runat="server" ControlToValidate="txtBillDate" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtBillDate" runat="server" CssClass="aspxcontrols" AutoCompleteType="Disabled"></asp:TextBox>
                <cc1:CalendarExtender ID="cclBillDate" runat="server" PopupButtonID="txtBillDate" PopupPosition="BottomLeft"
                    TargetControlID="txtBillDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                </cc1:CalendarExtender>
            </div>
            <%--  <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Customer/GL"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCustomerParty" runat="server" ControlToValidate="ddlCustomerParty" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select Customer/GL." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCustomerParty" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>--%>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="JE Type"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBillType" runat="server" ControlToValidate="ddlBillType" Display="Dynamic" SetFocusOnError="True"
                    ErrorMessage="Select JE Type." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlBillType" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Bill No." Visible="false"></asp:Label>
                <asp:TextBox ID="txtBillNo" runat="server" Visible="false" CssClass="aspxcontrols" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Bill Amount" Visible="false"></asp:Label>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEBillAmount" runat="server" ControlToValidate="txtBillAmount" Display="Dynamic"
                    SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                <asp:TextBox Visible="false" ID="txtBillAmount" runat="server" CssClass="aspxcontrols" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <div id="divAdvance" runat="server" class="col-sm-9 col-md-9" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblAdvance" runat="server" Text="* Advance Amount"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAdvance" runat="server" ControlToValidate="txtAdvancePayment" Display="Dynamic" SetFocusOnError="True"
                        ErrorMessage="Enter Advance Amount." ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVAdvance" runat="server" ControlToValidate="txtAdvancePayment" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtAdvancePayment" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblBalance" runat="server" Text="Balance Amount"></asp:Label>
                    <asp:TextBox ID="txtBalanceAmount" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                </div>
            </div>
            <div id="divPayment" runat="server" class="col-sm-9 col-md-9" style="padding: 0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label runat="server" Text="Net Amount"></asp:Label>
                    <asp:TextBox ID="txtNetAmount" runat="server" CssClass="aspxcontrolsdisable"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="col-sm-12 col-md-12" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <fieldset class="col-sm-12 col-md-12">
                    <legend class="legendbold">Debit Details</legend>
                </fieldset>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:DropDownList ID="ddldbHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVDbHead" runat="server" ControlToValidate="ddldbHead" Display="Dynamic" SetFocusOnError="True"
                        ValidationGroup="ValidateDebit"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddldbGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVdbGL" runat="server" ControlToValidate="ddldbGL" Display="Dynamic" SetFocusOnError="True"
                        ValidationGroup="ValidateDebit"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnAddGL" ImageUrl="~/Images/Add16.png" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add GL" CausesValidation="false" />
                </div>

                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="* Debit Amount"></asp:Label>
                    <asp:TextBox ID="txtDebitAmount" runat="server" CssClass="aspxcontrols" Width="70%" ValidationGroup="ValidateDebit" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVDebitAmount" runat="server" ControlToValidate="txtDebitAmount" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="ValidateDebit"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVDebitAmount" runat="server" ControlToValidate="txtDebitAmount" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="ValidateDebit"></asp:RegularExpressionValidator>
                    <asp:Button ID="btnAddDebit" runat="server" Text="Add" CssClass="btn-ok" ToolTip="Add Dedit" ValidationGroup="ValidateDebit" />
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Sub General Ledger" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddldbSubGL" runat="server" Visible="false" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <fieldset class="col-sm-12 col-md-12">
                    <legend class="legendbold">Credit Details</legend>
                </fieldset>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                    <asp:DropDownList ID="ddlCrHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVCrHead" runat="server" ControlToValidate="ddlCrHead" Display="Dynamic" SetFocusOnError="True"
                        ValidationGroup="ValidateCredit"></asp:RequiredFieldValidator>
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                    <asp:DropDownList ID="ddlCrGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVCrGL" runat="server" ControlToValidate="ddlCrGL" Display="Dynamic" SetFocusOnError="True"
                        ValidationGroup="ValidateCredit"></asp:RequiredFieldValidator>
                </div>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnGL" ImageUrl="~/Images/Add16.png" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add GL" CausesValidation="false" />
                </div>


                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="* Credit Amount."></asp:Label>
                    <asp:TextBox ID="txtCreditAmount" runat="server" CssClass="aspxcontrols" Width="70%" ValidationGroup="ValidateCredit" AutoCompleteType="Disabled"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVCreditAmount" runat="server" ControlToValidate="txtCreditAmount" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="ValidateCredit"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVCreditAmount" runat="server" ControlToValidate="txtCreditAmount" Display="Dynamic"
                        SetFocusOnError="True" ValidationGroup="ValidateCredit"></asp:RegularExpressionValidator>
                    <asp:Button ID="btnAddCredit" runat="server" Text="Add" CssClass="btn-ok" ToolTip="Add Credit" ValidationGroup="ValidateCredit" />
                </div>
                <div class="col-sm-2 col-md-2">
                    <asp:Label ID="lblHComments" runat="server" Text="* Comments"></asp:Label>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVComments" runat="server" ControlToValidate="txtComments" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVComments" runat="server" ControlToValidate="txtComments" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <asp:TextBox ID="txtComments" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="50px" MaxLength="5000"></asp:TextBox>
                </div>
                <div class="col-sm-3 col-md-3">
                    <asp:Label runat="server" Text="Sub General Ledger" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlCrSubGL" runat="server" Visible="false" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px;">
    </div>

    <div class="col-sm-12 col-md-12">
        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Narration" Visible="false"></asp:Label>
            <asp:TextBox ID="txtNarration" runat="server" CssClass="aspxcontrols" Height="140px" TextMode="MultiLine" Visible="false"></asp:TextBox>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgJEDetails" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gripagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.detID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundColumn DataField="PaymentID" HeaderText="PaymentID" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="3%" />
                <asp:BoundColumn DataField="Type" HeaderText="Type" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%" />
                <asp:BoundColumn DataField="GLDescription" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="18%" />
                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="7%" />
                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="15%" />
                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="12%" />
                <asp:BoundColumn DataField="Debit" HeaderText="Debit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%" />
                <asp:BoundColumn DataField="Credit" HeaderText="Credit" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%" />
                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="10%" />
                <asp:BoundColumn DataField="detID" HeaderText="HeadID" Visible="False" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" />
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CausesValidation="false" CssClass="hvr-bounce-in" CommandName="Delete" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnEdit" Enabled="true" CausesValidation="false" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>
    <div class="modal fade" id="ModalLedger" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">Ledger</h4>
                    <button type="button" class="close pull-right" data-dismiss="modal">&times;</button>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblledgererrormsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group">
                        <div class="col-sm-12 col-md-12 form-group">
                            <asp:Label runat="server" Text="Description:" Font-Bold="true"></asp:Label>
                            <asp:TextBox ID="txtdescription" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVDesc" runat="server" ControlToValidate="txtdescription" Display="Dynamic"
                                SetFocusOnError="True"></asp:RequiredFieldValidator>

                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btndescClear" runat="server" Text="Clear" CssClass="btn-ok" />
                    <asp:Button ID="btndescSave" runat="server" Text="Save" CssClass="btn-ok" />
                    <asp:Button ID="btndescClose" runat="server" Text="Close" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>
    <!-- Cheque Details -->
    <div class="modal fade" id="myModal" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog modal-md">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Cheque Details</h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Cheque No."></asp:Label>
                                <asp:TextBox ID="txtChequeNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Cheque Date"></asp:Label>
                                <asp:TextBox ID="txtChequeDate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="IFSC Code"></asp:Label>
                                <asp:TextBox ID="txtIFSC" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Bank Name"></asp:Label>
                                <asp:TextBox ID="txtBankName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <asp:Label runat="server" Text="Branch Name"></asp:Label>
                                <asp:TextBox ID="txtBranchName" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnNew" runat="server" Text="New" CssClass="btn-ok" />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn-ok" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>

    <div id="myModalHistory" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>History</b></h4>
                </div>
                <div class="modal-body">
                    <asp:DataGrid ID="dgHistory" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable">
                        <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                        <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDateTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Date") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="12%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User">
                                <ItemTemplate>
                                    <asp:Label ID="lblUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.User") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="22%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Comments">
                                <ItemTemplate>
                                    <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Comments") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="40%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" Width="23%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>
</div>
    <div id="ModalFASCompanyValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblCustomerValidationMsg" runat="server"></asp:Label>
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


