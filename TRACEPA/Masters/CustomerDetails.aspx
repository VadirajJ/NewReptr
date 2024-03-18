<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CustomerDetails.aspx.vb" Inherits="TRACePA.CustomerDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />
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
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            //$('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustName.ClientID%>').select2();
            $('#<%=cboReExp.ClientID%>').select2();
            $('#<%=cboCatList.ClientID%>').select2();
            $('#<%=ddlCat.ClientID%>').select2();
            $('#<%=ddlExistingLOE.ClientID%>').select2();
            $('#<%=ddlFrequency.ClientID%>').select2();
            $('#<%=ddlFrequency.ClientID%>').select2();
            $('#<%=ddlFunction.ClientID%>').select2();
            $('#<%=ddlGroup.ClientID%>').select2();
            $('#<%=ddlIndustry.ClientID%>').select2();
            $('#<%=ddlLocationCust.ClientID%>').select2();
            $('#<%=ddlLOECustomers.ClientID%>').select2();
            $('#<%=ddlLOELocation.ClientID%>').select2();
            $('#<%=ddlManagement.ClientID%>').select2();
            $('#<%=ddlOrganization.ClientID%>').select2();
            $('#<%=ddlOtherDetailsCust.ClientID%>').select2();
            $('#<%=ddlTask.ClientID%>').select2();
            $('#<%=ddlYear.ClientID%>').select2();
            $('#<%=ddlCompExistingCustomer.ClientID%>').select2();
            $('#<%=lstFinancialYear.ClientID%>').multiselect({
                includeSelectAllOption: true,
                allSelectedText: 'No option left ...',
                enableFiltering: true,
                filterPlaceholder: 'Search...',
                buttonWidth: '100%'
            });
            $('#<%=gvAssignment.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvAssignment.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 5,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            <%--$('#<%=gvStatutoryRef.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvStatutoryRef.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false }],
                //bPaginate: false,
                //bLengthChange: false,
            });--%>
            $('#<%=gvLOEDetails.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvLOEDetails.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvDet.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvDet.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvResource.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvResource.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvCatRes.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvCatRes.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvReAmbess.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvReAmbess.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvAttach.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvAttach.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            <%--$('#<%=gvCompliance.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvCompliance.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [11, 12] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
            $('#<%=gvDirector.ClientID%>').prepend($("<thead></thead>").append($("#<%=gvDirector.ClientID%>").find("tr:first"))).DataTable({
                initComplete: function () {
                    $(this.api().table().container()).find('input').parent().wrap('<form>').parent().attr('autocomplete', 'off');
                },
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [6, 7] }],
                //bPaginate: false,
                //bLengthChange: false,
            });--%>
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
    <script lang="javascript" type="text/javascript">
        function CopyAddress() {
            if (document.getElementById('<%=chkSameAddress.ClientID %>').checked == true) {
                document.getElementById('<%=txtOffAdd.ClientID %>').value = document.getElementById('<%=txtCommAdd.ClientID %>').value;
                document.getElementById('<%=txtCity.ClientID %>').value = document.getElementById('<%=txtCommCity.ClientID %>').value;
                document.getElementById('<%=txtPin.ClientID %>').value = document.getElementById('<%=txtCommPin.ClientID %>').value;
                document.getElementById('<%=txtState.ClientID %>').value = document.getElementById('<%=txtCommState.ClientID %>').value;
                document.getElementById('<%=txtCountry.ClientID %>').value = document.getElementById('<%=txtCommCountry.ClientID %>').value;
                document.getElementById('<%=txtFax.ClientID %>').value = document.getElementById('<%=txtCommFax.ClientID %>').value;
                document.getElementById('<%=txtTele.ClientID %>').value = document.getElementById('<%=txtCommTele.ClientID %>').value;
                document.getElementById('<%=txtEmailId.ClientID %>').value = document.getElementById('<%=txtCommEmail.ClientID %>').value;
            }
            else {
                document.getElementById('<%=txtOffAdd.ClientID %>').value = "";
                document.getElementById('<%=txtCity.ClientID %>').value = "";
                document.getElementById('<%=txtPin.ClientID %>').value = "";
                document.getElementById('<%=txtState.ClientID %>').value = "";
                document.getElementById('<%=txtCountry.ClientID %>').value = "";
                document.getElementById('<%=txtFax.ClientID %>').value = "";
                document.getElementById('<%=txtTele.ClientID %>').value = "";
                document.getElementById('<%=txtEmailId.ClientID %>').value = "";
            }

        }
        function ValidateGroup() {
            document.getElementById('<%=txtGroupName.ClientID%>').disabled = true;
            document.getElementById('<%=txtGroupName.ClientID %>').value = ''
            document.getElementById('<%=ddlExistingGroup.ClientID%>').disabled = true;
            document.getElementById('<%=ddlExistingGroup.ClientID %>').selectedIndex = "0";
            if (document.getElementById('<%=ddlGroup.ClientID %>').selectedIndex == "1") {
                document.getElementById('<%=txtGroupName.ClientID%>').disabled = false;
                document.getElementById('<%=ddlExistingGroup.ClientID%>').disabled = false;
                return false;
            }
            return true;
        }
        function ValidateExistingGroup() {
            if (document.getElementById('<%=ddlExistingGroup.ClientID %>').selectedIndex > 0) {
                var ddlExistingGroup = document.getElementById("<%= ddlExistingGroup.ClientID %>");
                document.getElementById('<%=txtGroupName.ClientID %>').value = ddlExistingGroup.options[ddlExistingGroup.selectedIndex].innerHTML;
                return false;
            }
            if (document.getElementById('<%=ddlExistingGroup.ClientID %>').selectedIndex == "0") {
                document.getElementById('<%=txtGroupName.ClientID %>').value = ''
                return false;
            }
            return true;
        }
    </script>
    <style>
        legend {
            margin-bottom: 5px;
            font-size: 14px;
            color: #919191;
        }

        dropdown {
            height: 40px;
        }
    </style>

    <div class="card">
        <div runat="server" id="divAssignmentheader" class="card-header">
            <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Customer Master Details" Font-Size="Small"></asp:Label>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnSaveOther" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate2" />
                <asp:ImageButton ID="imgbtnSaveLocation" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate3" />
                <asp:ImageButton ID="imgbtnSaveLOE" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate4" />
                <asp:ImageButton ID="imgbtnSaveLOETemp" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate5" />
                <asp:ImageButton ID="imgbtnSaveCompliance" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="ValidateComp" />
                <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                <asp:ImageButton ID="imgbtnUpdateLoction" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate3" />
                <asp:ImageButton ID="imgbtnUpdateLOE" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate4" />
                <asp:ImageButton ID="imgbtnUpdateCompliance" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="ValidateComp" />
                <div class="dropdown" style="display: inline-block;">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="padding: 0px; color: transparent; text-decoration: none;">
                        <span>
                            <img class="hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="LOE Report" />
                        </span>
                    </a>
                    <ul class="dropdown-menu" role="menu" style="right: 0">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                        </li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnWord" Text="Download Word" Style="margin: 0px;" />
                        </li>
                    </ul>
                </div>
                <div class="dropdown" style="display: inline-block;">
                    <a href="#" class="dropdown-toggle" data-toggle="dropdown" style="padding: 0px; color: transparent; text-decoration: none;">
                        <span>
                            <img class="hvr-bounce-out" id="imgbtnCustReport" runat="server" data-toggle="tooltip" data-placement="top" title="Customer Report" />
                        </span>
                    </a>
                    <ul class="dropdown-menu" role="menu" style="right: 0">
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnCustPDF" Text="Download PDF" Style="margin: 0px;" />
                        </li>
                        <li role="separator" class="divider"></li>
                        <li>
                            <asp:LinkButton runat="server" ID="lnkbtnCustWord" Text="Download Word" Style="margin: 0px;" />
                        </li>
                    </ul>
                </div>
                <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
            </div>
        </div>
    </div>
    <div class="card">
        <div class="col-sm-12 col-md-12" style="margin-top: 5px; padding-left: 0px">
            <div class="col-sm-12 col-md-12">
                <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            </div>
        </div>
        <div id="Tabs" role="tabpanel" class="col-sm-12 col-md-12 pull-left">
            <!-- Nav tabs -->
            <ul class="nav nav-tabs" role="tablist">
                <li id="liCust" runat="server">
                    <asp:LinkButton ID="lnkbtnCustomer" Text="Customer Details" runat="server" Font-Bold="true" />
                </li>
                <li id="liOther" runat="server">
                    <asp:LinkButton ID="lnkbtnOtherDetails" Text="Other Details" runat="server" Font-Bold="true" />
                </li>
                <li id="liLocations" runat="server">
                    <asp:LinkButton ID="lnkbtnLocations" Text="Locations" runat="server" Font-Bold="true" />
                </li>
                <li id="liLOE" runat="server">
                    <asp:LinkButton ID="lnkbtnLOE" Text="LOE" runat="server" Font-Bold="true" />
                </li>
                <li id="liLOETemplater" runat="server">
                    <asp:LinkButton ID="lnkbtnLOETemplate" Text="LOE Template" runat="server" Font-Bold="true" />
                </li>
                <li id="liCompliance" runat="server">
                    <asp:LinkButton ID="lnkbtnCompliance" Text="Statutory References" runat="server" Font-Bold="true" />
                </li>
                <li id="liAssignment" runat="server">
                    <asp:LinkButton ID="lnkbtnAssignment" Text="Assignments" runat="server" Font-Bold="true" />
                </li>
            </ul>

            <!-- Tab panes -->
            <div class="tab-content" style="padding-top: 5px">
                <div runat="server" role="tabpanel" class="tab-pane active" id="divCustomerDetails">
                    <div class="col-md-12">
                        <div class="col-md-3" style="padding-left: 0px">
                            <div class="form-group">
                                <label>Existing Customer</label>
                                <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>* Customer Name</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVICustomerName" runat="server" ControlToValidate="txtCustName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" TabIndex="1" ID="txtCustName"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>* Customer Code</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVICustomerCode" runat="server" ControlToValidate="txtCustCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCustCode" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>* Industry Type</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIndustry" runat="server" ControlToValidate="ddlIndustry" Display="Static" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlIndustry"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="padding-left: 0px">
                            <div class="form-group">
                                <label>Company URL</label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCompanyURL"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Company E-Mail</label>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEMail" runat="server" ControlToValidate="txtEMail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" TabIndex="4" ID="txtEMail"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>* Business Reltn. Start Date</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDate" runat="server" ControlToValidate="txtDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtDate"></asp:TextBox>
                                <cc1:CalendarExtender ID="cclFromtxtDate" runat="server" PopupButtonID="txtDate" PopupPosition="TopRight" TargetControlID="txtDate" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                            </div>
                            <div class="form-group">
                                <label>* Registration No</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="REVCustRegNo" runat="server" ControlToValidate="txtCustomerRegistrationNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtCustomerRegistrationNo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3" style="padding-left: 0px">
                            <div class="col-md-6" style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Group</label>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGroup" runat="server" ControlToValidate="ddlGroup" Display="Static" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                    <asp:DropDownList ID="ddlGroup" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6" style="padding: 0px;">
                                <div class="form-group">
                                    <label>Existing Group</label>
                                    <asp:DropDownList ID="ddlExistingGroup" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Group Name</label>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtGroupName" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>* Organization Type</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVOrganization" runat="server" ControlToValidate="ddlOrganization" Display="Static" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlOrganization"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Management</label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVManagement" runat="server" ControlToValidate="ddlManagement" Display="Static" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlManagement"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3" style="padding-right: 0px">
                            <div class="form-group">
                                <label>* Professional Services Offered</label>
                                <asp:Panel ID="Panel1" CssClass="panel panel-default" runat="server" Height="80px" ScrollBars="Vertical">
                                    <asp:CheckBoxList ID="chkboxTask" CssClass="aspxcontrols" runat="server"></asp:CheckBoxList>
                                </asp:Panel>
                            </div>
                            <div class="form-group">
                                <label>Board of Directors/Partners</label>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBoardOfDirectors" runat="server" ControlToValidate="txtBoardOfDirectors" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="80px" ID="txtBoardOfDirectors"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                    <%--<div class="col-sm-12 col-md-12 form-group">
                    <fieldset>
                        <legend class="legendbold">FixedAsset Setting</legend>
                    </fieldset>
                    <div class="col-sm-2 col-md-2" style="padding: 0px">
                        <asp:Label runat="server" Text="* Depreciation Method"></asp:Label>
                        <asp:DropDownList ID="ddlMethod" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="SLM" Value="1"></asp:ListItem>
                            <asp:ListItem Text="WDV" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                </div>--%>

                    <div class="col-md-12">
                        <div class="col-md-6" style="padding: 0px">
                            <fieldset>
                                <legend><b>Contact Address</b></legend>

                                <div class="col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID="txtCommAdd" CssClass="aspxcontrols" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>City</label>
                                        <asp:TextBox ID="txtCommCity" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>State</label>
                                        <asp:TextBox ID="txtCommState" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Country</label>
                                        <asp:TextBox ID="txtCommCountry" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Amount Type</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCommEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:DropDownList ID="ddlAmountConvert" runat="server" AutoPostBack="false" CssClass="aspxcontrols">
                                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="In Hundreds" Value="100"></asp:ListItem>
                                            <asp:ListItem Text="In Thousands" Value="1000"></asp:ListItem>
                                            <asp:ListItem Text="In Lakhs" Value="100000"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label></label>
                                    </div>
                                </div>
                                <div class="col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Postal Code</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCommPin" runat="server" ControlToValidate="txtCommPin" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCommPin" MaxLength="6" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Fax</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCommFax" runat="server" ControlToValidate="txtCommFax" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCommFax" MaxLength="15" CssClass="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Telephone</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCommTele" runat="server" ControlToValidate="txtCommTele" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCommTele" MaxLength="15" CssClass="aspxcontrols " TextMode="Phone" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Email</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCommEmail" runat="server" ControlToValidate="txtCommEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtCommEmail" CssClass="aspxcontrols " runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Round Off</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCommEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtRoundOff" CssClass="aspxcontrols " runat="server" TextMode="Number" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label></label>
                                    </div>
                                </div>

                            </fieldset>
                        </div>
                        <div class="col-md-6" role="form" style="padding: 0px">
                            <fieldset>
                                <legend><b>Registered Office Address</b>&nbsp;&nbsp;
                                <asp:CheckBox ID="chkSameAddress" onclick="CopyAddress()" runat="server" Font-Size="12px" Text="Same as Contact Address" />
                                </legend>

                                <div class="col-md-6" style="padding-left: 0px">
                                    <div class="form-group">
                                        <label>Address</label>
                                        <asp:TextBox ID="txtOffAdd" class="aspxcontrols" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>City</label>
                                        <asp:TextBox ID="txtCity" class="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>State</label>
                                        <asp:TextBox ID="txtState" class="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Country</label>
                                        <asp:TextBox ID="txtCountry" class="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Font style</label>
                                        <asp:DropDownList ID="ddlcustomFontstyle" runat="server" AutoPostBack="false" CssClass="aspxcontrols">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-6" style="padding-right: 0px">
                                    <div class="form-group">
                                        <label>Postal Code</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPin" runat="server" ControlToValidate="txtPin" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtPin" MaxLength="15" class="aspxcontrols" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Fax</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVFax" runat="server" ControlToValidate="txtFax" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtFax" MaxLength="15" class="aspxcontrols" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Telephone</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTele" runat="server" ControlToValidate="txtTele" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtTele" MaxLength="15" class="aspxcontrols " TextMode="Phone" runat="server"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <label>Email</label>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVEmailId" runat="server" ControlToValidate="txtEmailId" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtEmailId" class="aspxcontrols " TextMode="Email" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divOther">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Existing Customer</label>
                            <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlOtherDetailsCust"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding: 0px">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Legal Advisors</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="55px" ID="txtLglAdvisor"></asp:TextBox>
                            </div>

                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Turnover</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txturnover"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Profitability</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtProfit"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Products Manufactured</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtProdManufactured"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-12" style="padding: 0px">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Services Offered</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtServiceOff"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Standing In Industry</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtStandingInIndustry"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Foreign Collaboration</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtForeignCollaboration"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Employee Strength</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="55px" ID="txtEmpStrength"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <fieldset>
                            <legend><b>Knowledge </b></legend>
                            <div class="col-md-12" style="padding: 0px">
                                <div class="form-group">
                                    <label>File No</label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtFile"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" style="padding-left: 0px">
                                <div class="form-group">
                                    <label>Gathered by the Audit Firm</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtGatheredByFirm"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Public Perception of the Org.</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtPerceptionInPublic"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" style="padding-right: 0px">
                                <div class="form-group">
                                    <label>Major Litigation Issues if any</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtlegalIssues"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Government Perception of the Org.</label>
                                    <asp:TextBox runat="server" TextMode="MultiLine" class="aspxcontrols" Height="55px" ID="txtPerceptionInGovt"></asp:TextBox>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divLocation">
                    <div class="col-md-12">
                        <div class="col-md-8" style="padding-left: 0px;">
                            <div class="col-md-6 " style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Existing Customer</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlLocationCust"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>Contact Mobile number</label>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactMobileNo" runat="server" ControlToValidate="txtContactMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" MaxLength="10" ID="txtContactMobileNo"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Contact LandLine number</label>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactLandLineNo" runat="server" ControlToValidate="txtContactLandLineNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" MaxLength="15" ID="txtContactLandLineNo" TabIndex="49"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6" style="padding-right: 0px;">
                                <div class="form-group">
                                    <label>* Contact Person</label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVContactPerson" runat="server" ControlToValidate="txtContactPerson" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate3"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtContactPerson"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>Designation</label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtDesignation"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label>E-Mail</label>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVContactEmail" runat="server" ControlToValidate="txtContactEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" TabIndex="50" ID="txtContactEmail"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding: 0px;">
                                <div class="form-group">
                                    <label>* Address</label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLocationAddress" runat="server" ControlToValidate="txtLocationAddress" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate3"></asp:RequiredFieldValidator>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtLocationAddress" Height="55px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 " style="padding-right: 0px;">
                            <div class="form-group">
                                <label>* Location Name</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLocationName" runat="server" ControlToValidate="txtLocationName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate3"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtLocationName"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Exisiting Location</label>
                                <asp:ListBox runat="server" CssClass="aspxcontrols" AutoPostBack="True" Height="170px" ID="lstboxLocation"></asp:ListBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <fieldset>
                            <legend><b>Statutory References</b></legend>
                            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                                <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                                    <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlStatutoryReferences"></asp:DropDownList>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStatutoryReferences" runat="server" ControlToValidate="ddlStatutoryReferences" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateStatutory"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                                    <asp:TextBox ID="txtStatutoryValue" runat="server" placeholder="Reference" class="aspxcontrols"></asp:TextBox>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStatutoryValue" runat="server" ControlToValidate="txtStatutoryValue" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateStatutory"></asp:RequiredFieldValidator>
                                </div>
                                <div class="col-sm-1 col-md-1" style="padding: 0px">
                                    <asp:Button ID="btnStatutoryAdd" runat="server" Text="Add" class="btn-ok" ValidationGroup="ValidateStatutory" />
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding: 0px">
                                <asp:GridView ID="gvStatutoryRef" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundField DataField="StatutoryName" HeaderText="Name" ItemStyle-Width="50%" />
                                        <asp:BoundField DataField="StatutoryValue" HeaderText="Reference" ItemStyle-Width="50%" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnStatutoryRefDelete" runat="server" CommandName="DeleteRow" ToolTip="Delete" />
                                                <asp:Label ID="lblCustLOEPKID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Cust_PKID") %>'></asp:Label>
                                                <asp:Label ID="lblCustLOEStatutoryRefAttachmentPKID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Cust_AttchID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnAttachmentStatutoryReferences" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Attachment" CommandName="Attachment" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divLOE">
                    <div class="col-md-12">
                        <div class="col-md-6" style="padding-left: 0px;">
                            <div class="col-md-6" style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Existing Customer</label>
                                    <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlLOECustomers"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>* Types Of Service\Tasks</label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTask" runat="server" ControlToValidate="ddlTask" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlTask" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6 ">
                                <div class="col-md-12">
                                    <div class="col-md-9" style="padding:0px;">
                                        <div class="form-group">
                                            <label>Existing LOE</label>
                                            <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlExistingLOE"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group" style="padding-top: 10px;">
                                            <asp:Button runat="server" Text="Add LOE" ID="btnNewLOE" CssClass="btn-ok"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>*Frequency</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFrequency" runat="server" ControlToValidate="ddlFrequency" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlFrequency"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Nature of Services</label>
                                    <asp:TextBox runat="server" CssClass="aspxcontrols" ID="txtNS" Height="42px" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6" style="padding-left: 0px;">
                            <div class="col-md-6 " style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Financial Year</label>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlYear"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>* Start date </label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStartDate" runat="server" ControlToValidate="txtStartDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtStartDate"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cclStartDate" runat="server" PopupButtonID="txtStartDate" PopupPosition="bottomRight"
                                        TargetControlID="txtStartDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-6 " style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Locations</label>
                                    <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlLOELocation"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label>* Due date for Report </label>
                                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDueDate" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDueDate" runat="server" ControlToValidate="txtDueDate" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtDueDate"></asp:TextBox>
                                    <cc1:CalendarExtender ID="cclDueDate" runat="server" PopupButtonID="txtDueDate" PopupPosition="bottomRight"
                                        TargetControlID="txtDueDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                    </cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-12 " style="padding-left: 0px;">
                                <div class="form-group">
                                    <label>Milestones</label>
                                    <asp:TextBox runat="server" Height="42px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtMs"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-6" style="padding-left: 0px;">
                            <fieldset>
                                <legend><b>Scope Of Work</b></legend>
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>* Assignments/Tasks</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlFunction" runat="server" ControlToValidate="ddlFunction" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlFunction">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <label>Sub Tasks</label>
                                    <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSubFunction" runat="server" ControlToValidate="lstSubFunction" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSubFunction2" runat="server" ControlToValidate="lstSubFunction" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate4"></asp:RequiredFieldValidator>--%>
                                    <asp:ListBox runat="server" SelectionMode="Multiple" Height="50px" CssClass="aspxcontrols" ID="lstSubFunction"></asp:ListBox>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-6" style="padding-left: 0px;">
                            <fieldset>
                                <legend data-toggle="modal" data-target="#ReAmbessModal"><i><a href="#">Reimbursement</a></i></legend>
                                <div class="col-md-7" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Reimbursement </label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ErrorMessage="Select Reimbursement" InitialValue=" Select Reimbursement " runat="server" ControlToValidate="cboReExp" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddRe"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="cboReExp">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <label>Amount</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ErrorMessage="Enter Amount" runat="server" ControlToValidate="txtReAmt" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddRe"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVReAmt" runat="server" ControlToValidate="txtReAmt" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddRe"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" MaxLength="9" CssClass="aspxcontrols" ID="txtReAmt"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1" style="padding-right: 0px;">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <asp:Button runat="server" Text="Add" ValidationGroup="AddRe" ID="btnReAdd" CssClass="btn-ok"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="col-md-6" style="padding-left: 0px;">
                            <fieldset>
                                <legend data-toggle="modal" data-target="#otherModal"><i><a href="#">Other Expenses</a></i></legend>
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Other Expenses</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCatList" runat="server" ControlToValidate="cboCatList" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddOther"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="cboCatList"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Code</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCatCode" runat="server" ControlToValidate="txtCatCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddOther"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCatCode" runat="server" ControlToValidate="txtCatCode" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddOther"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" MaxLength="5" CssClass="aspxcontrols" ID="txtCatCode"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Amount</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVResources" runat="server" ControlToValidate="txtResources" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddOther"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVResources" runat="server" ControlToValidate="txtResources" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddOther"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" MaxLength="9" CssClass="aspxcontrols" ID="txtResources"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-1" style="padding-right: 0px;">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <asp:Button runat="server" ValidationGroup="AddOther" Text="Add" ID="btnAddCatList" CssClass="btn-ok"></asp:Button>
                                    </div>
                                </div>
                            </fieldset>
                        </div>
                        <div class="col-md-6" style="padding-left: 0px;">
                            <fieldset>
                                <legend id="legCategory" data-toggle="modal" data-target="#catModal"><i><a href="#">Category</a></i></legend>
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Category</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCat" runat="server" ControlToValidate="ddlCat" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddCat"></asp:RequiredFieldValidator>
                                        <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlCat"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-6" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <label>No. of Days</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdays" runat="server" ControlToValidate="txtdays" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddCat"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVdays" runat="server" ControlToValidate="txtdays" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddCat"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" MaxLength="9" CssClass="aspxcontrols" ID="txtdays"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>No. of Resources</label>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVNR" runat="server" ControlToValidate="txtNR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddCat"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNR" runat="server" ControlToValidate="txtNR" Display="Dynamic" SetFocusOnError="True" ValidationGroup="AddCat"></asp:RegularExpressionValidator>
                                        <asp:TextBox runat="server" MaxLength="9" CssClass="aspxcontrols" ID="txtNR"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-1" style="padding-right: 0px;">
                                    <label>&nbsp;</label>
                                    <div class="form-group">
                                        <asp:Button runat="server" ValidationGroup="AddCat" Text="Add" ID="btnAdd" CssClass="btn-ok"></asp:Button>
                                    </div>
                                </div>
                                <div class="col-md-5 pull-right" style="padding-right: 0px;">
                                    <br />
                                    <br />
                                    <asp:LinkButton ID="lnkbtnLoadGrid" runat="server"><b>Load Grid</b></asp:LinkButton>
                                    <div data-toggle="modal" data-target="#gridModal" runat="server"></div>
                                </div>
                            </fieldset>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <fieldset>
                            <legend>
                                <asp:LinkButton ID="lnkbtnFee" runat="server"><b>Calculate Fee</b></asp:LinkButton></legend>

                            <div class="col-md-6" style="padding-left: 0px;">
                                <div class="col-md-4" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Professional Fee</label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="aspxcontrols" ID="txtPFee"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Other Expenses</label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="aspxcontrols" ID="txtPExp"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4" style="padding-right: 0px;">
                                    <div class="form-group">
                                        <label>Reimbursement Expenses</label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="aspxcontrols" ID="txtReambessFee"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-6" style="padding-left: 0px;">
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Service Tax</label>
                                        <asp:TextBox runat="server" MaxLength="9" CssClass="aspxcontrols" ID="txtServiceTax"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" style="padding-left: 0px;">
                                    <div class="form-group">
                                        <label>Total</label>
                                        <asp:TextBox runat="server" Enabled="false" CssClass="aspxcontrols" ID="txtTotalAmt"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvLOEDetails" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Type of Service/Task" ItemStyle-Width="35%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLOEID" runat="server" CommandName="Select" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.LOE_Id") %>'></asp:Label>
                                        <asp:LinkButton ID="lnkDocumentRequestedType" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.LOE_ServiceTypeId") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LOE_Frequency" HeaderText="Frequency" ItemStyle-Width="30%" />
                                <asp:BoundField DataField="LOE_TimeSchedule" HeaderText="Start Date" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="LOE_ReportDueDate" HeaderText="Due Date of Report" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="LOE_Total" HeaderText="Total" ItemStyle-Width="10%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divLOETemplate">
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="col-md-3" style="padding-left: 0px;">
                            <div class="form-group">
                                <label>Existing Customer</label>
                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlLOETemplateCustomers"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Existing LOE</label>
                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlExistingLOETemplate"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-6" style="padding-left: 0px;">
                            <div class="col-md-11">
                                <div class="form-group">
                                    <label>Assignments/Tasks : </label>
                                    <asp:Label runat="server" ID="lblLOETemplateFunName" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-1 pull-right" style="padding-right: 0px">
                                <asp:ImageButton ID="imgbtnAttachment" OnClick="imgbtnAttachment_Click" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Style="padding-right: 0px;" CausesValidation="false" ImageUrl="~/Images/Attachment24.png"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" runat="server" Text="0"></asp:Label></span>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Frequency : </label>
                                    <asp:Label runat="server" ID="lblLOETemplateFrequency" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Professional Fees : </label>
                                    <asp:Label runat="server" TextMode="MultiLine" ID="lblLOETemplateProfessionalFee" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="col-md-6" style="padding-left: 0px;">
                            <div class="form-group">
                                <label>Sope Of Work</label>
                                <asp:ListBox runat="server" Height="80px" SelectionMode="Multiple" CssClass="aspxcontrols" AutoPostBack="True" ID="lstScopeSubFun"></asp:ListBox>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Responsibilities of the Auditor</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="80px" ID="txtStdIntAudit"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVStdIntAudit" runat="server" ControlToValidate="txtStdIntAudit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="col-md-6" style="padding-left: 0px;">
                            <div class="form-group">
                                <label>* The objective and scope of the audit</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDeliverable" runat="server" ControlToValidate="txtDeliverable" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" Height="80px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtDeliverable"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDeliverable" runat="server" ControlToValidate="txtDeliverable" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Reporting</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="80px" ID="txtRoles"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRoles" runat="server" ControlToValidate="txtRoles" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="col-md-6" style="padding-left: 0px;">
                            <div class="form-group">
                                <label>The responsibilities of management and identification of the applicable financial reporting framework</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="80px" ID="txtInfrastructure"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVInfrastructure" runat="server" ControlToValidate="txtInfrastructure" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>* General</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVGeneral" runat="server" ControlToValidate="txtGeneral" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RequiredFieldValidator>
                                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" Height="80px" ID="txtGeneral"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVGeneral" runat="server" ControlToValidate="txtGeneral" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-left: 0px;">
                        <div class="form-group">
                            <label>Non Disclosure Of Confidential Information</label>
                            <asp:TextBox TextMode="MultiLine" CssClass="aspxcontrols" Height="80px" ID="txtConfidential" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVConfidential" runat="server" ControlToValidate="txtConfidential" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate5"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divCompliance">
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Existing Customer</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompExistingCustomer" runat="server" ControlToValidate="ddlCompExistingCustomer" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlCompExistingCustomer"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>* Types Of Service/Tasks</label>
                                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCompTask" runat="server" ControlToValidate="ddlCompTask" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlCompTask"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>* Frequency</label>
                                <asp:RequiredFieldValidator ID="RFVCompFrequency" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlCompFrequency" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RequiredFieldValidator>
                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlCompFrequency"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="col-md-12">
                        <fieldset>
                            <legend><b>Login Credentials</b></legend>
                        </fieldset>
                    </div>
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Login Name</label>
                                <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtCompLoginName"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Password</label>
                                <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtCompPassword"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Email</label>
                                <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtCompEmail"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompEmailId" runat="server" ControlToValidate="txtCompEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Mobile No</label>
                                <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtCompMobileNo"></asp:TextBox>
                                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCompMobileNo" runat="server" ControlToValidate="txtCompMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateComp"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding: 0px;">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Account Details</label>
                                <asp:DropDownList runat="server" CssClass="aspxcontrols" ID="ddlCompAccountDetails"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Aadhaar Authentication</label>
                                <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtCompAadhaarAuthen"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Reg. No.</label>
                                <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtCompRegNo"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Remarks</label>
                                <asp:TextBox runat="server" TextMode="MultiLine" autocomplete="off" class="aspxcontrols" ID="txtRemarks"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <asp:GridView ID="gvCompliance" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompPkID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompPkID") %>'></asp:Label>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Act">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAct" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Act") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Service/Tasks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceTaskId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTaskId") %>'></asp:Label>
                                        <asp:Label ID="lblServiceTasks" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ServiceTask") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reg. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRegNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.RegNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frequency">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrequencyId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.FrequencyId") %>'></asp:Label>
                                        <asp:Label ID="lblFrequency" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Frequency") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Login Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLoginName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.LoginName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Password">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPassword" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Password") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MobileNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Account Details">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccountDetailId" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.AccountDetailID") %>'></asp:Label>
                                        <asp:Label ID="lblAccountDetailYesNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AccountDetailYesNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Aadhaar Authentication">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAadhaarAuthen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AadhaarAuthentication") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnEdit" ToolTip="EditRow" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div>
                        <br />
                        <br />
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-md-6" style="padding: 0px">
                            <div class="col-sm-12 col-md-12">
                                <fieldset>
                                    <legend><b>Director Details</b></legend>
                                </fieldset>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>* Name</label>
                                        <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtDirectorName"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDirectorName" runat="server" ControlToValidate="txtDirectorName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDirector"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>* DOB</label>
                                        <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtDirectorDOB"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDirectorDOB" runat="server" ControlToValidate="txtDirectorDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDirector"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDirectorDOB" runat="server" ControlToValidate="txtDirectorDOB" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDirector"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="ccltxtDirectorDOB" runat="server" PopupButtonID="txtDirectorDOB" PopupPosition="TopRight" TargetControlID="txtDirectorDOB" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>DIN</label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtDirectorDIN"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mobile No</label>
                                        <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtDirectorMobileNo"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDirectorMobileNo" runat="server" ControlToValidate="txtDirectorMobileNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDirector"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding: 0px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Email</label>
                                        <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtDirectorEmail"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVDirectorEmailId" runat="server" ControlToValidate="txtDirectorEmail" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidateDirector"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtDirectorRemarks"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12" style="padding-right: 0px;">
                                <div class="form-group">
                                    <asp:Button runat="server" Text="New" ID="btnNewDirector" CssClass="btn-ok"></asp:Button>
                                    <asp:Button runat="server" ValidationGroup="ValidateDirector" Text="Save Director Details" ID="btnSaveDirector" CssClass="btn-ok"></asp:Button>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <asp:GridView ID="gvDirector" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDirectorPkID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DirectorPkID") %>'></asp:Label>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOB">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOB" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DOB") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DIN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDIN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mobile No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MobileNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Remarks") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnEdit" ToolTip="EditRow" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-md-6" style="padding: 0px">
                            <div class="col-md-12">
                                <fieldset>
                                    <legend><b>Partner Details</b></legend>
                                </fieldset>
                            </div>
                            <div class="col-md-12" style="padding: 0px;">
                                <div class="col-md-8">
                                    <div class="form-group">
                                        <label>* Name</label>
                                        <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtPartnerName" MaxLength="100"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerName" runat="server" ControlToValidate="txtPartnerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerName" runat="server" ControlToValidate="txtPartnerName" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>* PAN</label>
                                        <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrols" ID="txtPartnerPAN" MaxLength="25"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerPAN" runat="server" ControlToValidate="txtPartnerPAN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerPAN" runat="server" ControlToValidate="txtPartnerPAN" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding: 0px;">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>* Date of Joining</label>
                                        <asp:TextBox runat="server" autocomplete="off" class="aspxcontrols" ID="txtPartnerDOJ"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVPartnerDOJ" runat="server" ControlToValidate="txtPartnerDOJ" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPartnerDOJ" runat="server" ControlToValidate="txtPartnerDOJ" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtPartnerDOJ" PopupPosition="TopRight" TargetControlID="txtPartnerDOJ" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>* Share Of Profit</label>
                                        <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtShareOfProfit" MaxLength="5"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVShareOfProfit" runat="server" ControlToValidate="txtShareOfProfit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVShareOfProfit" runat="server" ControlToValidate="txtShareOfProfit" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>* Capital Amount</label>
                                        <asp:TextBox runat="server" class="aspxcontrols" autocomplete="off" ID="txtCapitalAmount" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCapitalAmount" runat="server" ControlToValidate="txtCapitalAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVCapitalAmount" runat="server" ControlToValidate="txtCapitalAmount" Display="Dynamic" SetFocusOnError="True" ValidationGroup="ValidatePartner"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding-right: 0px;">
                                <div class="form-group">
                                    <asp:Button runat="server" Text="New" ID="btnNewPartner" CssClass="btn-ok"></asp:Button>
                                    <asp:Button runat="server" ValidationGroup="ValidatePartner" Text="Save Partner Details" ID="btnSavePartner" CssClass="btn-ok"></asp:Button>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <asp:GridView ID="gvPartner" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartnerPkID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PartnerPkID") %>'></asp:Label>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPAN" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PAN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DOJ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDOJ" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DOJ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Share Of Profit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShareOfProfit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ShareOfProfit") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Capital Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCapitalAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CapitalAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnStatus" CommandName="Status" runat="server" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnEdit" ToolTip="EditRow" CommandName="EditRow" runat="server" CssClass="hvr-bounce-in" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane" id="divAssignment">
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Existing Customer</label>
                                <asp:DropDownList runat="server" AutoPostBack="True" CssClass="aspxcontrols" ID="ddlAsgExistingCustomer"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <div class="form-group">
                                    <asp:Label ID="lblHFinancialYear" runat="server" Text="Financial Year"></asp:Label>
                                    <br />
                                    <asp:ListBox ID="lstFinancialYear" runat="server" Width="100%" Font-Size="10px" SelectionMode="Multiple" CssClass="aspxcontrols1" Style="width: 100%;"></asp:ListBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <br />
                                <asp:CheckBox ID="chkInvoice" CssClass="aspxradiobutton" runat="server" Text="With Invoice"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group pull-right">
                                <br />
                                <asp:ImageButton ID="imgbtnLoad" ClientIDMode="Static" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" />
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12" style="padding-left: 0px; padding-right: 0px;">
                        <asp:GridView ID="gvAssignment" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAssignment_RowDataBound">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderText="Assignment No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignmentID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AssignmentID") %>'></asp:Label>
                                        <asp:Label ID="lblClosed" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Closed") %>'></asp:Label>
                                        <asp:Label ID="lblCustomerFullName" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerName") %>'></asp:Label>
                                        <asp:Label ID="lblAssignmentNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssignmentNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Customer" ItemStyle-Width="12%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CustomerShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Partner" HeaderText="Partner" ItemStyle-Width="9%" />
                                <%-- <asp:BoundField DataField="FinancialYear" HeaderText="FY" ItemStyle-Width="5%" />--%>
                                <asp:BoundField DataField="Task" HeaderText="Assignment/Task" ItemStyle-Width="11%" />
                                <%--<asp:BoundField DataField="SubTask" HeaderText="Sub Task" ItemStyle-Width="7%" />--%>
                                <asp:BoundField DataField="Employee" HeaderText="Assigned To" ItemStyle-Width="9%" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-Width="6%" />
                                <asp:BoundField DataField="DueDate" HeaderText="Start Date" ItemStyle-Width="6%" />
                                <asp:BoundField DataField="ExpectedCompletionDate" HeaderText="Expected Completion Date" ItemStyle-Width="10%" />
                                <%--<asp:BoundField DataField="TimeTaken" HeaderText="Time taken" ItemStyle-Width="5%" />--%>
                                <asp:TemplateField HeaderText="Work Status" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WorkStatus") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="BillingStatus" HeaderText="Billing status" ItemStyle-Width="5%" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <%--<asp:UpdateProgress ID="updateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
            <ProgressTemplate>
                <div class="loader">
                    <div style="z-index: 1000; margin-left: 350px; margin-top: 0px; opacity: 1; -moz-opacity: 1;">
                        <img alt="" src="/Images/pageloader.gif" />
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>--%>
        </div>
        <div id="ModaCustomerValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modalmsg-dialog">
                <div class="modalmsg-content">
                    <div class="modalmsg-header">
                        <h4 class="modal-title"><b>TRACe</b></h4>
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
        <div id="gridModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-lg">
                <div class="modal-content row">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><b>Details</b></h4>
                    </div>
                    <div class="modal-body">
                        <div class="col-sm-12 col-md-12">
                            <asp:GridView ID="gvDet" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:BoundField DataField="CId" Visible="False" />
                                    <asp:BoundField DataField="Task" HeaderText="Type of Service" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="NServ" HeaderText="Nature of Service" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="Loc" HeaderText="Locations" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="MileStone" HeaderText="MileStones" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="STime" HeaderText="Time Schedule" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="DDate" HeaderText="Due Date" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="PFee" HeaderText="Professional Fee" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="PExp" HeaderText="Other Expense" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="Tax" HeaderText="Service Tax" ItemStyle-Width="9%" />
                                    <asp:BoundField DataField="Resource" HeaderText="No. Of Resources" ItemStyle-Width="10%" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="catModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><b>Details</b></h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvResource" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Cat") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No.of Resource">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResource" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Res") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No. of Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCharges" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.days") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Charges Per Day">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Charge") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" CssClass="hvr-bounce-in" ToolTip="Delete" runat="server" CommandName="DeleteRow" ImageUrl="~/Images/Trash16.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div id="otherModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><b>Details</b></h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvCatRes" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other Expenses">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CatCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CatRes") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDeleteCa" CssClass="hvr-bounce-in" ToolTip="Delete" runat="server" CommandName="DeleteRow" ImageUrl="~/Images/Trash16.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div id="ReAmbessModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><b>Details</b></h4>
                    </div>
                    <div class="modal-body">
                        <asp:GridView ID="gvReAmbess" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reimbursement">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReambersment" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Reambersment") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDeleteRe" runat="server" CssClass="hvr-bounce-in" ToolTip="Delete" CommandName="DeleteRow" ImageUrl="~/Images/Trash16.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="col-sm-11 col-md-11">
                            <h4 class="modal-title" style="text-align: center;"><b>Attachment</b></h4>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <div class="row ">
                            <div class="col-sm-12 col-md-12 pull-left">
                                <asp:Label ID="lblMsg" runat="server" Width="502px" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>
                            <div class="col-sm-12 col-md-12 pull-left">
                                <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                                <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                            </div>
                            <div class="col-sm-12 col-md-12">
                                <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                    <div class="form-group">
                                        <asp:FileUpload ID="txtfileAttach" runat="server" CssClass="btn-ok" Width="95%" />
                                    </div>
                                </div>
                                <div class="col-sm-1 col-md-1" style="padding: 0px;">
                                    <div class="form-group">
                                        <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                    </div>
                                </div>
                                <div class="col-sm-7 col-md-7" style="padding: 0px">
                                    <div class="form-group">
                                        <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                        <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                            Visible="false" Width="300px">
                                        </asp:TextBox>
                                        <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                            runat="server"></asp:Button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <asp:GridView ID="gvAttach" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>
                                        <asp:BoundField DataField="SrNo" HeaderText="Sr.No" />
                                        <asp:TemplateField HeaderText="File Name" ItemStyle-Width="40%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                                                <asp:LinkButton ID="File" runat="server" CommandName="OPENPAGE" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="28%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created" ItemStyle-Width="23%">
                                            <ItemTemplate>
                                                <b>By : </b>
                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label>
                                                <br />
                                                <b>On : </b>
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="4%">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" />
                                                <br />
                                                <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
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
    </div>
    <div id="ModalAddConfirmation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divAddConfirm" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblConfirmAdd" runat="server"></asp:Label>
                            </strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <div class="modal-footer">
                        <div class="pull-right">
                            <asp:Button runat="server" Text="Yes" class="btn-ok" ID="btnConfirmAdd" OnClick="btnConfirmAdd_Click"></asp:Button>
                            <asp:Button runat="server" Text="No" class="btn-ok" ID="btnCancelAdd" data-dismiss="modal"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label ID="lblStatus" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblScopeSubFun" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblFunId" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
    <rsweb:ReportViewer ID="ReportViewer1" Visible="false" runat="server"></rsweb:ReportViewer>
</asp:Content>
