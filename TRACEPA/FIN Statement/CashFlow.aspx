<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CashFlow.aspx.vb" Inherits="TRACePA.CashFlow" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=Gvcategory1.ClientID%>').DataTable({
                iDisplayLength: 500,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=GrdviewTotalAmount.ClientID%>').DataTable({
                iDisplayLength: 500,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });

            $('#<%=grdCategory3.ClientID%>').DataTable({
                iDisplayLength: 500,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=grdCategory4.ClientID%>').DataTable({
                iDisplayLength: 500,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=grdCategory5.ClientID%>').DataTable({
                iDisplayLength: 500,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
    </script>
    <script>
        (function (apiKey) {
            (function (p, e, n, d, o) {
                var v, w, x, y, z; o = p[d] = p[d] || {}; o._q = o._q || [];
                v = ['initialize', 'identify', 'updateOptions', 'pageLoad', 'track']; for (w = 0, x = v.length; w < x; ++w)(function (m) {
                    o[m] = o[m] || function () { o._q[m === v[0] ? 'unshift' : 'push']([m].concat([].slice.call(arguments, 0))); };
                })(v[w]);
                y = e.createElement(n); y.async = !0; y.src = 'https://cdn.eu.pendo.io/agent/static/' + apiKey + '/pendo.js';
                z = e.getElementsByTagName(n)[0]; z.parentNode.insertBefore(y, z);
            })(window, document, 'script', 'pendo');

            // This function creates visitors and accounts in Pendo
            // You will need to replace <visitor-id-goes-here> and <account-id-goes-here> with values you use in your app
            // Please use Strings, Numbers, or Bools for value types.
            pendo.initialize({
                visitor: {
                    id: "Trdemo", // Required if user is logged in
                    email: "trdemo@mmcspl.com"        // Recommended if using Pendo Feedback, or NPS Email
                full_name: "Trace demo"    // Recommended if using Pendo Feedback
                 role: "Admin"         // Optional

                    // You can add any additional visitor level key-values here,
                    // as long as it's not one of the above reserved names.
                },

                account: {
                    id: "1101" // Highly recommended, required if using Pendo Feedback
                 name: "Name"   // Optional
                    // is_paying:    // Recommended if using Pendo Feedback
                    // monthly_value:// Recommended if using Pendo Feedback
                    // planLevel:    // Optional
                    // planPrice:    // Optional
                    // creationDate: // Optional

                    // You can add any additional account level key-values here,
                    // as long as it's not one of the above reserved names.
                }
            });
        })('0fb8c855-668b-48b2-4642-d699206920ba');
    </script>
    <div id="BodyContent" runat="server">
        <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
            <div class="card">
                <div runat="server" id="divCompheader" class="card-header">
                    <%--<i class="fa fa-hourglass" style="color: #83ace2"></i>--%>
                    <%--<i class="fas fa-hourglass-end" style="color: #83ace2"></i>--%>
                    <asp:Label runat="server" ID="Label3" CssClass="form-label" Font-Bold="true" Text="Cash Flow Statement" Font-Size="Small"></asp:Label>
                    <div class="pull-right" style="padding-right: 15px;">
                        <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />
                        <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                            <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                        <ul class="dropdown-menu">
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                            <li role="separator" class="divider"></li>
                            <li>
                                <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="card">
                <div id="divComplianceBody" runat="server" clientidmode="Static">
                    <div class="card-body">
                        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 divmargin">
                            <div class="col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="lblCustName" runat="server" Text="* Customer Name"></asp:Label>
                                <asp:DropDownList ID="ddlCustomers" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblBranch" runat="server" Text="* Branch Name"></asp:Label>
                                    <asp:DropDownList ID="ddlbranch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-sm-3 col-md-3 col-lg-3" style="padding-left: 0px">
                                <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
                                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <fieldset>
                                <legend class="legendbold"></legend>
                            </fieldset>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                                <asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                            <div class="legendbold">Cash Flow</div>
                            <div class=" col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="lblscheduletype" Text="Particulars" runat="server"></asp:Label>
                                <asp:TextBox ID="txtParticularsCategory1" runat="server" CssClass="aspxcontrols">               
                                </asp:TextBox>
                            </div>
                            <div class=" col-sm-3 col-md-3 col-lg-3">
                                <asp:Label ID="lblComptype" Text="Current Year" runat="server"></asp:Label>
                                <asp:TextBox ID="txtCurrentAmountCategory1" runat="server" TextMode="Number" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                            <br />
                            <div class="col-sm- 2 col-md-2 col-lg-2">
                                <asp:Button ID="btnAddcategory1" CssClass="btn-ok hvr-bounce-out" Style="height: min-content;" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" CausesValidation="false" Text="Add" />
                            </div>
                            <div class="col-sm- 3 col-md-3 col-lg-3" style="visibility: hidden">
                                <asp:Label ID="Label2" Text="Previous Year" runat="server"></asp:Label>
                                <asp:TextBox ID="txtPreviesAmountCategory1" runat="server" TextMode="Number" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                        </div>
                        <%--    Cash Flow Grid--%>
                        <div class="col-sm-12 col-md-12 col-lg-12 padT40">
                            <asp:GridView ID="Gvcategory1" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpkid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACF_pkid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Particulers" ItemStyle-Width="70%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulers") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCurrentAmmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentAmmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPreviesAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PreviesAmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CausesValidation="false" CssClass="hvr-bounce-in" CommandName="Delete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                            <div class="pull-left divmargin col-sm- 8 col-md-8 col-lg-8">
                                <asp:Label ID="lblBfOperation" Visible="true" runat="server" Font-Bold="true" Text='Operating profit / (loss) before working capital changes'></asp:Label>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="lblTotalCurrentcategory1" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 70px"></asp:Label>
                                <asp:Label ID="lblTotalPrevcategory1" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 60px"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 ">
                            <asp:Button ID="btnUpdateGvcategory1" CssClass="btn-ok hvr-bounce-out pull-right" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" CausesValidation="false" Text="Save Changes" />
                        </div>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold"></legend>
                        </fieldset>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="legendbold">A. Cash flows from operating activities:</div>
                        </div>
                        <div class="legendbold"></div>
                        <div class="col-sm-12 col-md-12 padT40">
                            <asp:GridView ID="GrdviewTotalAmount" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpkid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACF_pkid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="70%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulers") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCurrentAmmount" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentAmmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPreviesAmount" runat="server" Enabled="false" Text='<%# DataBinder.Eval(Container, "DataItem.PreviesAmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                            <div class="pull-left divmargin col-sm- 8 col-md-8 col-lg-8">
                                <asp:Label ID="lblOpratingTotal" Visible="true" runat="server" Font-Bold="true" Text='Net cash flow from / (used in) operating activities (A)'></asp:Label>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="lblCurrentOpratingTotal" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 80px"></asp:Label>
                                <asp:Label ID="lblPrevOpratingTotal" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 80px"></asp:Label>
                            </div>
                        </div>
                       <%-- <div class=" col-sm-12 col-md-12 col-lg-12">
                            <asp:Button ID="btnSavecategory2" CssClass="btn-ok hvr-bounce-out pull-right" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" CausesValidation="false" Text="Save Changess" />
                        </div>--%>
                        <div class="col-sm-12 col-md-12">
                            <fieldset>
                                <legend class="legendbold"></legend>
                            </fieldset>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="legendbold">B. Cash flows from investing activities</div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="Label6" Text="Particulars" runat="server"></asp:Label>
                                <asp:TextBox ID="txtParticularsCategory3" runat="server" CssClass="aspxcontrols">               
                                </asp:TextBox>
                            </div>
                            <div class="col-sm- 3 col-md-3 col-lg-3">
                                <asp:Label ID="Label8" Text="Current Year" runat="server"></asp:Label>
                                <asp:TextBox ID="txtCurrentAmountCategory3" TextMode="Number" runat="server" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                            <br />
                            <div class="col-sm- 2 col-md-2 col-lg-2 ">
                                <asp:Button ID="bntCategory3" CssClass="btn-ok hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" CausesValidation="false" Text="Add" />
                            </div>
                            <div class="col-sm- 3 col-md-3 col-lg-3" style="visibility: hidden">
                                <asp:Label ID="Label9" Text="Previous Year" runat="server"></asp:Label>
                                <asp:TextBox ID="txtPreviesAmountCategory3" TextMode="Number" runat="server" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 padT40">
                            <asp:GridView ID="grdCategory3" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpkid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACF_pkid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="70%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulers") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCurrentAmmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentAmmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPreviesAmount" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PreviesAmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CausesValidation="false" CssClass="hvr-bounce-in" CommandName="Delete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                            <div class="pull-left divmargin col-sm- 8 col-md-8 col-lg-8">
                                <asp:Label ID="lblInvstingTotal" runat="server" Font-Bold="true" Text="Net cash flow from / (used in) investing activities (B)"></asp:Label>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="lblCurrentinvestingactivities" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 80px"></asp:Label>
                                <asp:Label ID="lblprevinvestingactivities" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 70px"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <asp:Button ID="btnsaveCategory3" CssClass="btn-ok hvr-bounce-out pull-right" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" CausesValidation="false" Text="Save Changess" />
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <fieldset>
                                <legend class="legendbold"></legend>
                            </fieldset>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <div class="legendbold">C. Cash flows from financing activities</div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="Label12" Text="Particulars" runat="server"></asp:Label>
                                <asp:TextBox ID="txtParticularsCategory4" runat="server" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                            <div class="col-sm- 3 col-md-3 col-lg-3">
                                <asp:Label ID="Label13" Text="Current Year" runat="server"></asp:Label>
                                <asp:TextBox ID="txtCurrentAmountCategory4" TextMode="Number" runat="server" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                            <br />
                            <div class="col-sm-2 col-md-2 col-lg-2">
                                <asp:Button ID="btnAddCategory4" CssClass="btn-ok hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" CausesValidation="false" Text="Add" />
                            </div>
                            <div class="col-sm- 3 col-md-3 col-lg-3" style="visibility: hidden">
                                <asp:Label ID="Label14" Text="Previous Year" runat="server"></asp:Label>
                                <asp:TextBox ID="txtPreviesAmountCategory4" TextMode="Number" runat="server" CssClass="aspxcontrols">
                                </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12 padT40">
                            <asp:GridView ID="grdCategory4" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpkid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACF_pkid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="70%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulers") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCurrentAmmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentAmmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Year" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPreviesAmount" Enabled="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PreviesAmount") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CausesValidation="false" CssClass="hvr-bounce-in" CommandName="Delete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                            <div class="pull-left divmargin col-sm- 8 col-md-8 col-lg-8">
                                <asp:Label ID="lblFinanceTotal" runat="server" Font-Bold="true" Text="Net cash flow from / (used in) financing activities (C)"></asp:Label>
                            </div>
                            <div class="col-sm-4 col-md-4 col-lg-4">
                                <asp:Label ID="lblCurrentfinancingactivities" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 70px"></asp:Label>
                                <asp:Label ID="lblPrevfinancingactivities" Visible="true" runat="server" Font-Bold="true" Style="margin-left: 60px"></asp:Label>
                            </div>
                            <br />
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12 ">
                            <asp:Button ID="btnsaveCategory4" CssClass="btn-ok hvr-bounce-out pull-right" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" CausesValidation="false" Text="Save Changes" />
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-12">
                            <fieldset>
                                <legend class="legendbold"></legend>
                            </fieldset>
                        </div>
                        <div class="col-sm-12 col-md-12 padT40">
                            <asp:GridView ID="grdCategory5" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpkid" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACF_pkid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="60%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblParticulars" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulers") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrentAmmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CurrentAmmount") %>' TextMode="Number"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Prev Year" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPreviesAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PreviesAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="5%" Visible="false">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CausesValidation="false" CssClass="hvr-bounce-in" CommandName="Delete" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<asp:GridView ID="gvCountries" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" Width="100%"></asp:GridView>--%>


    <div id="ModalScheduleValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblExcelValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
