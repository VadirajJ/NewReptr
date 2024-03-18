<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ScheduleNote.aspx.vb" Inherits="TRACePA.ScheduleNote" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript">
                
    </script>
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
    </script>
    <script>
function myFunction() {
  const element = document.getElementById("FootNote");
  element.scrollIntoView();
}
    </script>
    <br />
    <div class="card">
        <div runat="server" id="divAssignmentheader" class="card-header">
            <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Notes" Font-Size="Small"></asp:Label>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />
                <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                    <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" visible="true" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
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
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            </div>

        </div>
        <div class="col-md-12">
            <div class="col-md-4" style="padding-left: 0px">
                <div class="form-group ">
                    <asp:Label ID="Label6" runat="server" Text="* Customer Name"></asp:Label>
                    <%--    <asp:RequiredFieldValidator ID="RFVFunction" runat="server" ControlToValidate="ddlReportType" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                    <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-4" style="padding-left: 0px">
                <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <div class="col-md-4 pull-right" style="padding-right: 0px">
                <asp:Panel runat="server" ID="pnlScheduleReport">
                    <div class="form-group pull-right">
                        <a href="#">
                            <asp:LinkButton ID="lnkScheduleReport" ForeColor="Blue" runat="server"><b><i><u>Click here to View Schedule Report.</u></i></b></asp:LinkButton></a><br />
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" runat="server">
            <div id="div2" runat="server">
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li id="liShchedulenotes" class="active" runat="server">
                        <asp:LinkButton ID="lnkbtnSchedulenoteDetails" Text=" Basic Notes" runat="server" Font-Bold="true" /></li>
                    <li id="liShchedulepredefinedNotes" runat="server">
                        <asp:LinkButton ID="lnkbtnShchedulepredefinedNotes" Text=" Pre-Defined Notes" runat="server" Font-Bold="true" /></li>
                </ul>
            </div>
            <!-- Tab panes -->
            <div class="tab-content divmargin">
                <div runat="server" role="tabpanel" class="tab-pane active" id="divEmpBasic">
                    <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                        <asp:Label ID="lblEmpBasicDetails" runat="server" Text="Description for Note Number" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-4" style="padding-left: 0px">
                            <div class="form-group ">
                                <asp:Label ID="Label1" Text="Sub Heading Name" runat="server"></asp:Label>
                                <asp:DropDownList ID="ddlsubheading" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-left: 0px">
                        <div class="col-md-9" style="padding-right: 0px">
                            <div class="form-group">
                                <asp:Label ID="lblDescription" runat="server" Text="* Description"></asp:Label>
                                <asp:TextBox runat="server" Height="300px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtEnterDescription"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <div class="form-group">
                                <br />
                                <asp:ImageButton ID="ImgBtnAddDetails" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Add24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Details" />
                            </div>
                        </div>
                        <asp:Label runat="server" ID="lblId" Visible="false"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvReportContentMaster" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                           <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />

                                <asp:TemplateField HeaderText="SubHeading" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSubHeading" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubHeading") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" ItemStyle-Width="31%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divSchedulePredefinednotes">
                    <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                        <asp:Label ID="Label2" runat="server" Text="Equity" Font-Size="Large" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-md-12 padT40">
                        <%--First--%>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label13" Font-Bold="true" Font-Size="12px" Text="Share Capital" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label3" Font-Bold="true" Text="Particulars" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label4" Font-Bold="true" Text="Authorised Share Capital" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFAuthorised"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label5" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblCAu" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFAuthorisedCYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label8" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPAu" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFAuthorisedPYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <br />
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnFAuthorised" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFAuthorised" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="6%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAthorisedDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFACYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" Width="20px" CommandName="Delete" runat="server" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Label runat="server" ID="Label9" Visible="false"></asp:Label>
                    </div>

                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label10" Font-Bold="true" Text="Issued, Subscribed and Fully Paid up Share Capital" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFIssuedSubscribed"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label11" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblCIs" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFISubscribedCYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label12" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPIs" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFISubscribedPYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <br />
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnFISubscribed" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFISubscribed" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFISid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFIAthorisedDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFICYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFIPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" Width="20px" CommandName="Delete" runat="server" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="col-md-12 padT40">
                        <%--      <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label15" Font-Bold="true" Font-Size="12px" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label16" Font-Bold="true" Text="(A)Issued" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFAIssued"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label15" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblIssCurrent" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFAIssuedCYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label14" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblIssPrev" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFAIssuedPYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <br />
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnFAIssued" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFAIssued" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="0%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAISid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAIssuedDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAIssuedCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFAIssuedPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" Width="20px" CommandName="Delete" runat="server" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="col-md-12 padT40">
                        <%--   <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label19" Font-Bold="true" Font-Size="12px" Text="(B)Subscribed and Paid-up" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label20" Font-Bold="true" Text="(B)Subscribed and Paid-up" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFBSubscribed"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label21" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblCSub" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFBSubCYAmt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label22" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPSub" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFBSubPYAmt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <br />
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnFBSubscribed" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFBSubscribed" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFBid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFBDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFBCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFBPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" Width="20px" CommandName="Delete" runat="server" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>


                    <div class="col-md-12 padT40">
                        <%--                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label23" Font-Bold="true" Font-Size="12px" Text="(C)Calls Unpaid" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label24" Font-Bold="true" Text="(C)Calls Unpaid" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFCCUnpaid"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label25" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblCCalls" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFCCUnpaidCYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label26" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPCalls" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFCCUnpaidPYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <br />
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnFCCUnpaid" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFCCUnpaid" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblccid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCCDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCCCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCCPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" Width="20px" CommandName="Delete" runat="server" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="col-md-12 padT40">
                        <%--   <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label27" Font-Bold="true" Font-Size="12px" Text="(D)Forfeited Shares" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>--%>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label28" Font-Bold="true" Text="(D)Forfeited Shares" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFDFS"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label29" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblCFor" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFDFSCYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label30" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPFor" runat="server"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFDFSPYamt"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <br />
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnFDFS" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFDFS" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDFSid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="50%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDFSDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDFSCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFDFSPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnEdit" CssClass="hvr-bounce-in" CommandName="Edit" runat="server" ToolTip="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in" Width="20px" CommandName="Delete" runat="server" ToolTip="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <%--Second--%>
                    <div class="col-md-12 padT40">

                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label36" Font-Bold="true" Font-Size="12px" Text="(a)Reconcillation of the number of shares outstanding at the beginning and at the end of the reporting period" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-11">
                            <div class="form-group ">
                                <asp:Label ID="Label40" Font-Bold="true" Text="i.Equity Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Button runat="server" ID="btnSFSchedules" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label35" Font-Bold="true" Text="Particulars" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label56" Text="No. of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label57" Text="value" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label58" Text="No. of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label59" Text="value" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label39" Font-Bold="true" Text="At the beginning of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFBegCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFBegCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFBegPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFBegPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label37" Font-Bold="true" Text="Add: During the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFAddCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFAddCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFAddPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFAddPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label38" Font-Bold="true" Text="At the end of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFEndCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFEndCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFEndPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSFEndPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold"></legend>
                        </fieldset>
                        <div class="col-md-11">
                            <div class="form-group ">
                                <asp:Label ID="Label43" Font-Bold="true" Text="ii.Preference Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Button runat="server" ID="btnSSSchedules" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label44" Font-Bold="true" Text="At the beginning of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSBegCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSBegCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSBegPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSBegPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label45" Font-Bold="true" Text="Add: During the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSAddCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSAddCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSAddPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSAddPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label46" Font-Bold="true" Text="At the end of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSEndCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSEndCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSEndPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSSEndPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold"></legend>
                        </fieldset>

                        <div class="col-md-11">
                            <div class="form-group ">
                                <asp:Label ID="Label41" Font-Bold="true" Text="iii. Equity Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Button runat="server" ID="btnSTSchedules" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label42" Font-Bold="true" Text="At the beginning of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTBegCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTBegCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTBegPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTBegPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label47" Font-Bold="true" Text="Add: During the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTAddCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTAddCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTAddPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTAddPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label48" Font-Bold="true" Text="At the end of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTEndCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTEndCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTEndPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSTEndPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold"></legend>
                        </fieldset>
                        <div class="col-md-11">
                            <div class="form-group ">
                                <asp:Label ID="Label49" Font-Bold="true" Text="iv.Preference Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <asp:Button runat="server" ID="btnSVSchedules" Text="Add" CssClass="btn-ok" />
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label50" Font-Bold="true" Text="At the beginning of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVBegCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVBegCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVBegPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVBegPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label51" Font-Bold="true" Text="Add: During the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVAddCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVAddCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVAddPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVAddPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label52" Font-Bold="true" Text="At the end of the year" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVEndCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVEndCYValues"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVEndPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtSVEndPYValues"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                    </div>
                    <%--Third--%>
                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label54" Font-Bold="true" Font-Size="12px" Text="(b) Details of the shareholders holding more than 5% shares in the company" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label55" Font-Bold="true" Text=" Shares held by Promoters at the end of the year" runat="server"></asp:Label>
                                <asp:Label ID="lblCShares" Font-Bold="true" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label64" Font-Bold="true" Text="Equity Share Capital" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label65" Text="No of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label66" Text="% holding" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label67" Text="No of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    &nbsp;
                                    <asp:Label ID="Label68" Text="% holding" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBEquityDesc"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBEquity_CYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBEquity_CYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBEquity_PYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBEquity_PYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnTBEquity" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label17" Font-Bold="true" Text="Preference Share Capital" runat="server"></asp:Label><br />
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBPrefDesc"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBPref_CYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBPref_CYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBPref_PYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTBPref_PYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnTBPref" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvTBEquity" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBEquityid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBEquityDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBEquityCYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBEquityCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBEquityPYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBEquityPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvTBPref" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="" ItemStyle-Width="4%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBPrefid" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Preference Share Capital" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBPrefDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBPrefCYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBPrefCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBPrefPYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTBPrefPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label70" Font-Size="12px" Font-Bold="true" Text="(c) Terms/rights attached to equity shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group ">
                                <asp:TextBox runat="server" Height="50px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtDescCTermsEquity"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group ">
                                <asp:Button runat="server" ID="btnDescCTermsEquity" Text="Add/Edit" CssClass="btn-ok" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label71" Font-Size="12px" Font-Bold="true" Text="(d) Terms/Rights attached to preference shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group ">
                                <asp:TextBox runat="server" Height="50px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtDescDTermsPref"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnDescDtermsPref" Text="Add/Edit" CssClass="btn-ok" />
                        </div>
                    </div>
                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label72" Font-Size="12px" Font-Bold="true" Text="(e) Details of the shares held by the Holding Company" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label73" Font-Bold="true" Text="Particulars" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label74" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblCdetails" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label75" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label76" Font-Bold="true" Text="As at" runat="server"></asp:Label>&nbsp;
                                    <asp:Label ID="lblPdetails" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label77" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label78" Font-Bold="true" Text="Name of the sharholder" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label79" Text="No of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label80" Text="% holding" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label81" Text="No of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label82" Text="% holding" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEEquityDesc"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEEquity_CYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEEquity_CYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEEquity_PYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEEquity_PYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnTEEquity" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvTEEquity" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEEquityId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEEquityDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEEquityCYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEEquityCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEEquityPYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEEquityPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label84" Font-Bold="true" Text="Preference Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-3" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEPrefDesc"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEPref_CYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEPref_CYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEPref_PYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtTEPref_PYAmount"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-1" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Button runat="server" ID="btnTEPref" Text="Add" CssClass="btn-ok" />
                                </div>
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvTEPref" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEPrefId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Particulars" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEPrefDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEPrefCYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Amount" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEPrefCYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEPrefPYear_Shares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Shares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Previous Year Amount" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTEPrefPYear_Amount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PYear_Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label85" Font-Size="12px" Font-Bold="true" Text="(f) Shares allotted as fully paid up pursuant to contract(s) without payment being received in cash (during 5 years immediately preceding)" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group ">
                                <asp:TextBox runat="server" Height="50px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtDescFShares"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnDescFShares" Text="Add/Edit" CssClass="btn-ok" />
                        </div>
                    </div>
                    <%--Fourth share current year--%>
                    <div class="col-md-12 padT40">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label86" Font-Size="12px" Font-Bold="true" Text=" Shares held by promoters As at " runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label97" Font-Bold="true" Text="Equity Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label92" Font-Bold="true" Text="Promoter name" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label93" Text="No of shares" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label94" Text="% total shares" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:Label ID="Label96" Text="% change during the year" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSCYEquityDesc"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSCYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSCYTotShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSCYChangedShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnFSCYShares" Text="Add" CssClass="btn-ok" />
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFSCYShares" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                               <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSCYId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Promoter Name" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSCYPromoterName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PromoterName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSCYCYShares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYShares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSCYTotShares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotShares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Changed Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSCYChangedShares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChangedShares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label98" Font-Bold="true" Text="Preference Shares" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="col-md-4" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSPYREFDesc"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSPYShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSPYTotShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2" style="padding-left: 0px">
                                <div class="form-group ">
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFSPYChangedShares"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:Button runat="server" ID="btnFSpYpREF" Text="Add" CssClass="btn-ok" />
                            </div>
                        </div>

                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvFSPYREF" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="90%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="2%" Visible="false" />
                                    <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />
                                    <asp:TemplateField HeaderText="id" ItemStyle-Width="15%" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSpYId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Promoter Name" ItemStyle-Width="30%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSpYPromoterName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PromoterName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSpYCYShares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CYShares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Year Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSpYTotShares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotShares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="=Changed Shares" ItemStyle-Width="15%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFSpYChangedShares" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ChangedShares") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-12 padT40" id="FootNote" runat="server">
                        <div class="col-md-12">
                            <div class="form-group ">
                                <asp:Label ID="Label31" Font-Size="11px" Font-Bold="true" Text="Foot Note" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-10">
                            <div class="form-group ">
                                <asp:TextBox runat="server" Height="50px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtFootNote"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:Button runat="server" ID="btnFootNote" Text="Add/Edit" CssClass="btn-ok" />
                        </div>
                    </div>
                </div>
                <%--Ending predefined notes--%>
            </div>
        </div>

    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; width: 100%">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="539px" PageCountMode="Actual"></rsweb:ReportViewer>
        </div>
    </div>

    <div id="ModalEmpMasterValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModalValidationMsg" runat="server"></asp:Label></strong>
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
