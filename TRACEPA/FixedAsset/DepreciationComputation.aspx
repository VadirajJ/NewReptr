<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="DepreciationComputation.aspx.vb" Inherits="TRACePA.DepreciationComputation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />

    <%--  <link href="../css/StyleSheet1.css" rel="stylesheet" />--%>
    <%--    <link href="../css/styles.css" rel="stylesheet" />--%>

    <style>
        .loader {
            position: relative;
            left: 0px;
            top: 0px;
            width: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
        /*tr:nth-child(even) {
            background-color: white;
        }*/
        tr:nth-child(even) {
            background-color: whitesmoke;
        }

        tr {
            padding: 0px;
            margin: 0px
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

            $('#<%=ddlCustomerName.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
            $('#<%=ddlMethod.ClientID%>').select2();
            $('#<%=ddlDepBasis.ClientID%>').select2();
        });

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgDepComp.ClientID%>').DataTable({
                iDisplayLength: 15,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });

      <%--  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=dgItComp.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
            });
        });--%>

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        function showProgress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }

    </script>

    <div class="loader"></div>

    <div class="col-sm-12 col-md-12 row">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>


    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa-solid fa-clover" style="font-size: large"></i>&nbsp;
               
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Depreciation Computation" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <%--<asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />--%>
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" />
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
            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Depreciation Basis"></asp:Label>
                        <asp:DropDownList ID="ddlDepBasis" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Companies Act" Value="1"></asp:ListItem>
                            <asp:ListItem Text="IT Act" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-3 col-md-3">
                    <div class="form-group">
                        <asp:Label runat="server" Text="* Method of Depreciation"></asp:Label>
                        <asp:DropDownList ID="ddlMethod" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-sm-3 col-md-3">
                    <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                    </asp:DropDownList>
                </div>
            </div>
            <asp:Panel ID="PnlCompanyAct" runat="server" Visible="false">

                <div class="col-sm-12 col-md-12 ">
                    <div class="col-sm-6 col-md-6 row" style="font: bold">
                        <asp:Label runat="server" Text="Select Duration"></asp:Label><br />
                        <br />
                        <asp:RadioButtonList Font-Bold="true" OnSelectedIndexChanged="OnRadio_Changed" Font-Names="serif pro" ForeColor="#063970" ID="rbtDuration" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" Width="400px" AutoPostBack="true">
                            <asp:ListItem Text=" Yearly" Value="1"></asp:ListItem>
                            <asp:ListItem Text=" Half Yearly" Value="2"></asp:ListItem>
                            <asp:ListItem Text=" Quarterly" Value="3"></asp:ListItem>
                            <asp:ListItem Text=" Monthly" Value="4"></asp:ListItem>
                            <%-- <asp:ListItem Text=" Weekly" Value="5"></asp:ListItem>--%>
                            <asp:ListItem Text=" Customised" Value="5"></asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                    </div>

                    <div class="col-sm-1 col-md-1" style="margin-top: 35px">
                        <asp:UpdatePanel runat="server" ID="updatepanel1" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="col-sm-1 col-md-1">
                                    <asp:Button ID="btnGo" runat="server" Font-Bold="true" Text="GO" CssClass="btn-ok" OnClientClick="showProgress()" AutoPostBack="true" Visible="true" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnGo" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-sm-5 col-md-5" style="margin-top: 35px">
                        <asp:Label ID="lblOpeningBalance" runat="server" ForeColor="#ff0066" Font-Bold="true" Font-Size="Medium"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Label ID="lblAddition" runat="server" ForeColor="#cc0066" Font-Bold="true" Font-Size="Medium"></asp:Label>
                    </div>
                </div>
                <div class="clearfix divmargin"></div>
                <asp:Panel ID="PnlDurationMonthly" runat="server" Visible="false">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Months"></asp:Label>
                        <asp:DropDownList ID="ddlDurationmonth" runat="server" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div class="clearfix divmargin"></div>
                <asp:Panel ID="pnlQuarterly" runat="server" Visible="false">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Quarterly"></asp:Label>
                        <asp:DropDownList ID="ddlDurationQuarter" runat="server" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div class="clearfix divmargin"></div>
                <asp:Panel ID="pnlHalfYearly" runat="server" Visible="false">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Half-yearly"></asp:Label>
                        <asp:DropDownList ID="ddlDurationhalfyear" runat="server" CssClass="aspxcontrols">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div class="clearfix divmargin"></div>
                <asp:Panel ID="pnlyear" runat="server" Visible="false">
                    <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Financial-year" Visible="true"></asp:Label>
                        <asp:DropDownList ID="ddlDurationYear" runat="server" CssClass="aspxcontrols" Visible="true" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>
                <div class="clearfix divmargin"></div>
                <asp:Panel ID="pnlBankDaybook" runat="server" Visible="false">
                    <div class="col-sm-2 col-md-2">
                        <asp:Label ID="lblFromdate" runat="server" Text="* From Date"></asp:Label>
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="cclFromDate" runat="server" PopupButtonID="txtFromDate" PopupPosition="BottomLeft" TargetControlID="txtFromDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                    </div>
                    <div class="col-sm-2 col-md-2">
                        <asp:Label ID="lblTodate" runat="server" Text="* To Date"></asp:Label>
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="aspxcontrols" placeholder="dd/MM/yyyy" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                        <cc1:CalendarExtender ID="cclToDate" runat="server" PopupButtonID="txtToDate" PopupPosition="BottomLeft" TargetControlID="txtToDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"></cc1:CalendarExtender>
                    </div>
                </asp:Panel>
            </asp:Panel>

            <%--<div class="col-sm-1 col-md-1">
        <br />
        <asp:Button ID="btnGo1" runat="server" Font-Bold="true" Text="GO" CssClass="aspxcontrols" AutoPostBack="true" Visible="true" />
    </div>--%>



            <asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="updatepanel1">
                <ProgressTemplate>
                    <div class="overlay">
                        <div style="z-index: 1000; margin-left: 350px; margin-top: 0px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="/Images/pageloader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <div class="col-sm-12 col-md-12" runat="server" style="border-style: none; overflow:scroll; border-color: inherit; border-width: medium">
                <asp:GridView ID="dgDepComp" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader"
                    Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" ShowFooter="true">
                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderText="AssetTypeID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetClassID" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetClassID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="AssetID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LocationID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblLocationID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LocationID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DivisionID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblDivisionID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DivisionID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="DepartmentID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartmentID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepartmentID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="BayID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblBayID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BayID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date of put to use" HeaderStyle-Width="7%">
                            <ItemTemplate>
                                <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Asset Code" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetCode") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Location" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Location") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Division" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblDivision" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Division") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Department" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Department") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bay" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblBay" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Bay") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Class of Asset" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAssettype" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Assettype") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Asset" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblItem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Item") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Original Cost" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblOrignalCost" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.OrignalCost") %>'></asp:Label>
                                <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFOrignalCost" runat="server"></asp:Label>
                            </FooterTemplate>
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Residual %" HeaderStyle-Width="2%">
                            <ItemTemplate>
                                <asp:Label ID="lblRsdulvalue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Rsdulvalue") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="center" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="center" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Residual Value" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblSalvageValue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SalvageValue") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFSalvageValue" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Usefull Life in years" HeaderStyle-Width="4%">
                            <ItemTemplate>
                                <asp:Label ID="lblAssetAge" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetAge") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="center" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="center" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rate of Dep." HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblDepreciationRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepreciationRate") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="center" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="center" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WDV Opening Value." HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblOPBForYR" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OPBForYR") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFOPBForYR" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Addition During the year" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAddtnAmt" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AddtnAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dep. for the Period" HeaderStyle-Width="6%">
                            <ItemTemplate>
                                <asp:Label ID="lblDepreciationforFY" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DepreciationforFY") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFDepreciationforFY" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WDV Closing Value" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblwrtnvalue" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "wrtnvalue") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFwrtnvalue" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Transaction Type" Visible="false" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblTrType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TrType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="No of Days" Visible="false" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblNoOfDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "NoOfDays") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

            <style>
                .gridview th {
    background-color: #223f65;
    color: white;
    font-weight: bold;
    text-align: center;
}
         .gridview th:nth-child(4) {
        border-left: 1px solid white; /* Adjust border color and style as needed */
    }
          .gridview th:nth-child(7) {
        border-left: 1px solid white; /* Adjust border color and style as needed */
    }
          .gridview th:nth-child(14) {
        border-left: 1px solid white; /* Adjust border color and style as needed */
    }
            </style>
            <script>
   function addHeaderRow() {
    // Get a reference to the GridView
    var gridView = document.getElementById('<%= dgDepAsperITAct.ClientID %>');

    // Insert a new header row at index 0
    var headerRow = gridView.insertRow(0);

    // Create a cell for the spanned header
    var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = 'Addition';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

  var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);


    // Create a cell for the spanned header
    var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

   var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = 'Depreciation';
    headerRow.appendChild(headerCell);

   var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

   var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

   var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);

   var headerCell = document.createElement('th');
    headerCell.colSpan = 1; // Adjust the colspan based on the number of columns to span
    headerCell.innerHTML = '';
    headerRow.appendChild(headerCell);


}

// Call the function when the page is loaded
window.onload = function () {
    addHeaderRow();
 
};
            </script> 

           <div class="col-sm-12 col-md-12" runat="server" style="border-style: none;overflow:scroll; border-color: inherit; border-width: medium;">
                <asp:GridView ID="dgDepAsperITAct" CssClass="table bs gridview" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader"
                    Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" ShowFooter="true">

                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>

                        <asp:TemplateField HeaderText="Del Amount" Visible="false" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblDelAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DelAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asset Class ID" Visible="false" HeaderStyle-Width="0px">
                            <ItemTemplate>
                                <asp:Label ID="lblClassID" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssetClassID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Class of Asset" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblClassofAsset" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ClassofAsset") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rate of Dep." HeaderStyle-Width="3%">
                            <ItemTemplate>
                                <asp:Label ID="lblRateofDep" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RateofDep") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WDV Opening Value" HeaderStyle-Width="5%" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <asp:Label ID="lblWDVOpeningValue" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WDVOpeningValue") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFWDVOpeningValue" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Before 180days Addition" Visible="true" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblBfrQtrAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BfrQtrAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="After 180days Addition" Visible="true" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblAftQtrAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AftQtrAmount") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Addition in Year" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblAdditionDuringtheYear" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AdditionDuringtheYear") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFAdditionDuringtheYear" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WDVOpeningDepreciation" Visible="true" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblWDVOpeningDepreciation" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WDVOpeningDepreciation") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Before 180days Dep" Visible="true" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblBfrQtrDep" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BfrQtrDep") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="After 180days Dep" Visible="true" HeaderStyle-Width="1%">
                            <ItemTemplate>
                                <asp:Label ID="lblAftQtrDep" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AftQtrDep") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Initial Depreciation Amount" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblInitDepAmt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.InitDepAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFInitDepAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Prev Yr Initial Depreciation Amount" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblPrevInitDepAmt" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PrevInitDepAmt") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFPrevInitDepAmt" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NextYrCarry" Visible="true" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblNextYrCarry" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NextYrCarry") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFNextYrCarry" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dep for the period" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblDepfortheperiod" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Depfortheperiod") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFDepfortheperiod" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="WDV Closing Value" HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="lblWDVClosingValue" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.WDVClosingValue") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblFWDVClosingValue" runat="server"></asp:Label>
                            </FooterTemplate>
                            <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

<%--    <asp:GridView ID="YourGridView" runat="server" AutoGenerateColumns="true">
    <!-- Your GridView columns and other properties -->
</asp:GridView>--%>




    <div id="ModalDepreciationValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblPaymentMasterValidationMsg" runat="server"></asp:Label></strong>
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
</asp:Content>

