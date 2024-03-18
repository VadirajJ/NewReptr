<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="AssetMaster.aspx.vb" Inherits="TRACePA.AssetMaster" %>

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
        /*div {
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
            $('#<%=drpAstype.ClientID%>').select2();
            $('#<%=ddlUnits.ClientID%>').select2();
            $('#<%=ddlSuplierName.ClientID%>').select2();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
       });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>

    <div class="card">
        <div runat="server" id="divAssignmentheader" class="card-header">
            <i class="fa-regular fa-registered" style="font-size: large"></i>&nbsp;
                <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Asset Creation" Font-Size="Small"></asp:Label>
            <div class="pull-right">
                <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" Style="height: 16px" />
                <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="New" />
                <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validatesave" />
                <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" Visible="false" title="Approve" />
                <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Visible="false" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" Visible="false" runat="server" Text="0"></asp:Label></span>
                <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" Visible="false" CausesValidation="false" />
                <asp:ImageButton ID="ImgBtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />

            </div>
        </div>
    </div>
    <div class="card">


        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
             <asp:Label runat="server" ID="lblPkId" Visible="false"></asp:Label>
        </div>

        <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
            <br />
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Font-Bold="true" Text="Status : "></asp:Label>
                <asp:Label ID="lblstatus" runat="server" font-family="serif" Font-Bold="true" Text="Open"></asp:Label>
                
            </div>
            <div class="col-sm-3 col-md-3">
                <div class="pull-left">&nbsp;&nbsp;&nbsp;
                <button id="btnchangeclass" runat="server" class="btn-ok">Change Class/Useful life</button>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton ID="lnkExcelUpload" runat="server" Text="Excel Upload" PostBackUrl="~/FixedAsset/AssetOpeningBalExcelUpload.aspx" ForeColor="#009900"
                        Font-Bold="true" Font-Underline="true"></asp:LinkButton>
                </div>
            </div>
        </div>

        <%-- <div class="clearfix divmargin"></div>--%>
        <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Asset Class"></asp:Label>
                <asp:DropDownList ID="drpAstype" runat="server" Enabled="false" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdrpAstype" ControlToValidate="drpAstype" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Assettype" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>

            <%--   <div class="col-sm-3 col-md-3">--%>
            <asp:Label runat="server" Text="Existing Asset Description" Visible="false"></asp:Label>
            <asp:DropDownList ID="DrpItemCode" runat="server" Enabled="false" CssClass="aspxcontrols" AutoPostBack="true" Visible="false"></asp:DropDownList>
            <%-- </div>--%>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Asset No" autocomplete="off"></asp:Label>
                <asp:TextBox ID="txtbxAstCode" Enabled="false" runat="server" Font-Size="X-Small" CssClass="aspxcontrols"></asp:TextBox>
                <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxAstCode" ControlToValidate="txtbxAstCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the AssetCode" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
            <%--   <div class="col-sm-3 col-md-3">--%>
            <asp:Label runat="server" Text="* Asset Class Description" Visible="false"></asp:Label>
            <asp:TextBox ID="txtbxDscrptn" runat="server" Enabled="false" CssClass="aspxcontrols" autocomplete="off" Visible="false"></asp:TextBox>
            <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDscrptn" ControlToValidate="txtbxDscrptn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Description" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            <%-- </div>--%>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text=" Asset Code"></asp:Label>
                <asp:TextBox ID="txtbxItmCode" runat="server" Enabled="false" CssClass="aspxcontrols" Font-Size="X-Small" autocomplete="off"></asp:TextBox>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmCode" ControlToValidate="txtbxItmCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Asset Code" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Asset Description"></asp:Label>
                <asp:TextBox ID="txtbxItmDecrtn" runat="server" Enabled="false" autocomplete="off" Font-Size="X-Small" CssClass="aspxcontrols"></asp:TextBox>
                <%--    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmDecrtn" ControlToValidate="txtbxItmDecrtn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Asset Description" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
        </div>


        <div class="col-sm-12 col-md-12 " style="padding: 0px">
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Quantity"></asp:Label>
                <asp:TextBox ID="txtbxQty" autocomplete="off" Enabled="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxQty" ControlToValidate="txtbxQty" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Quantity" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Units of Measurement"></asp:Label>
                <asp:DropDownList ID="ddlUnits" runat="server" Enabled="false" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>

            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Useful life of Asset"></asp:Label>
                <asp:TextBox ID="txtbxAstAge" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" ControlToValidate="txtbxQty" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Useful life of Asset" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Date of Put to use"></asp:Label>
                <asp:TextBox ID="txtbxDteCmmunictn" runat="server" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
                <cc1:CalendarExtender ID="txtbxDteCmmunictn_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteCmmunictn" TargetControlID="txtbxDteCmmunictn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the date of Commission" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn1" runat="server" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
            </div>
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">

            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Date Of Purchase" Visible="false"></asp:Label>
                <asp:TextBox ID="txtbxDteofPurchase" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                <%--  <cc1:CalendarExtender ID="txtbxDteofPurchase_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteofPurchase" TargetControlID="txtbxDteofPurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>--%>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the date of purchase" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase1" runat="server" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
            </div>

            <asp:Panel runat="server" Visible="false">
                <div class="col-sm-2 col-md-2">
                    <asp:Label runat="server" Text="Amount"></asp:Label>
                    <asp:TextBox ID="txtbxamount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtbxamount" Display="Dynamic" ErrorMessage="please enter the Amount" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" ControlToValidate="txtbxamount" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Amount" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
                </div>
            </asp:Panel>
        </div>

        <%--<div class="col-sm-12 col-md-12 pre-scrollableborder form-group">--%>
        <%-- <div class="col-sm-12 col-md-12">--%>
        <%--<fieldset class="col-sm-12 col-md-12">--%>
        <%--<legend class="legendbold">Insurance Details</legend>--%>
        <%--</fieldset>--%>
        <%--  <fieldset class="col-sm-12 col-md-12">
            <legend class="legendbold"><b style="font-family: Georgia; color:darkcyan" >Insurance Details.</b></legend>
        </fieldset>--%>

        <%-- <div class="col-sm-2 col-md-2">
            <asp:Label runat="server"  Text="Policy No"></asp:Label>
            <asp:TextBox ID="txtbxPlyNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator3" ControlToValidate="txtbxPlyNo" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Polycy Number" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        <%-- </div>--%>
        <%-- <div class="col-sm-2 col-md-2">
            <asp:Label runat="server"  Text="Amount"></asp:Label>
            <asp:TextBox ID="txtbxAmt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbxAmt" Display="Dynamic" ErrorMessage="please enter the Amount" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
        <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" ControlToValidate="txtbxAmt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Amount" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        <%--        </div>--%>

        <%-- <div class="col-sm-2 col-md-2">
            <asp:Label runat="server"   Text="BrokerName"></asp:Label>
            <asp:TextBox ID="txtbxBrkName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator5" ControlToValidate="txtbxBrkName" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the BrokerName" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtbxBrkName" Display="Dynamic" ErrorMessage="please enter the BrokerName" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,25}"></asp:RegularExpressionValidator>--%>
        <%--   </div>--%>
        <%--   <div class="col-sm-2 col-md-2">
            <asp:Label runat="server"  Text="Company Name"></asp:Label>
            <asp:TextBox ID="txtCmpName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator6" ControlToValidate="txtCmpName" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Company Name" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCmpName" Display="Dynamic" ErrorMessage="please the Company Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>
        <%--</div>--%>
        <%--    <div class="col-sm-2 col-md-2">
            <asp:Label runat="server"  Text="From Date"></asp:Label>
            <asp:TextBox ID="txtbxfrmDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxfrmDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxfrmDate" TargetControlID="txtbxfrmDate" Format="dd/MM/yyyy" PopupPosition="Bottomright"></cc1:CalendarExtender>
            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator4" ControlToValidate="txtbxfrmDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
        <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtbxfrmDate" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
        <%-- </div>--%>
        <%--<div class="col-sm-2 col-md-2">
            <asp:Label runat="server"  Text="To Date"></asp:Label>
            <asp:TextBox ID="txtbxtoDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <cc1:CalendarExtender ID="txtbxtoDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxtoDate" TargetControlID="txtbxtoDate" Format="dd/MM/yyyy" PopupPosition="Bottomright"></cc1:CalendarExtender>
        </div>--%>
        <%--   </div>--%>

        <div id="Tabs" class="col-sm-12 col-md-12" role="tabpanel" font-bold="true" runat="server" visible="True">
            <div id="div2" runat="server">
                <ul class="nav nav-tabs row-cols-5" role="tablist" style="padding: 0px">
                    <li id="lisupplier_Detls" runat="server">
                        <asp:LinkButton ID="lnkbtnSupDtls" Text="Supplier" Font-Bold="true" runat="server" /></li>

                    <li id="lialortment_Detls" runat="server">
                        <asp:LinkButton ID="lnkbtnAlrtDtls" Text="Location" Font-Bold="true" runat="server" /></li>

                    <li id="liWarantyAMC_Detls" runat="server">
                        <asp:LinkButton ID="lnkWrntyAMCDtls" Text="Warranty/AMC" Font-Bold="true" runat="server" /></li>

                    <li id="liAsset_detetion" visible="false" runat="server">
                        <asp:LinkButton ID="lnkbtnDeletion" Text="Asset Deletion" Font-Bold="true" runat="server" /></li>

                    <li id="liAsset_Loan" runat="server">
                        <asp:LinkButton ID="lnkbtnLoanAsst" Text="Loan Against Asset" Font-Bold="true" runat="server" /></li>

                    <li id="liAsset_InsuranceDetails" runat="server">
                        <asp:LinkButton ID="lnkbtnInsuranceDetails" Text="Insurance" Font-Bold="true" runat="server" /></li>
                </ul>
            </div>
            <div class="tab-content divmargin row">

                <div runat="server" role="tabpanel" class="tab-pane" id="divSupDtls">
                    <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                        <asp:Label ID="lblSsipdtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 row ">
                        <div class="col-sm-3 col-md-3 col-lg-3">
                            <asp:Label runat="server" Text="Suplier Name"></asp:Label>
                            <asp:DropDownList ID="ddlSuplierName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator11" ControlToValidate="txtbxSname" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Suplier Name" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtbxSname" Display="Dynamic" ErrorMessage="please enter Suplier Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>

                            <%--  <div class="col-sm-1 col-md-1">--%>
                            <asp:ImageButton ID="imgbtnAddSuplier" Width="27px" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Supplier" CausesValidation="false" />
                            <asp:ImageButton ID="imgbtnEditSuplier" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit Suplier" CausesValidation="false" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Contact Person"></asp:Label>
                            <asp:TextBox ID="txtbxConPerson" autocomplete="off" runat="server" Enabled="false" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator12" ControlToValidate="txtbxConPerson" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Contact Person" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtbxConPerson" Display="Dynamic" ErrorMessage="please enter Contact Person" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Address"></asp:Label>
                            <asp:TextBox ID="txtbxAddress" autocomplete="off" runat="server" Enabled="false" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator13" ControlToValidate="txtbxAddress" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Address" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtbxAddress" Display="Dynamic" ErrorMessage="please enter Address" SetFocusOnError="True" ValidationExpression="^[0-9'.\s a-zA-Z'.\s]{1,200}"></asp:RegularExpressionValidator>--%>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Phone No"></asp:Label>
                            <asp:TextBox ID="txtbxPhoneNo" autocomplete="off" runat="server" Enabled="false" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator14" ControlToValidate="txtbxPhoneNo" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter PhoneNo" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtbxPhoneNo" Display="Dynamic" ErrorMessage="please enter  PhoneNo" SetFocusOnError="True" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>--%>
                        </div>


                        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="FAX"></asp:Label>
                                <asp:TextBox ID="txtbxFax" autocomplete="off" runat="server" Enabled="false" CssClass="aspxcontrols"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator16" ControlToValidate="txtbxFax" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter FaxNo" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtbxFax" Display="Dynamic" ErrorMessage="please enter FaxNo" SetFocusOnError="True" ValidationExpression="[0-9]{6}"></asp:RegularExpressionValidator>--%>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Email"></asp:Label>
                                <asp:TextBox ID="txtbxEmail" autocomplete="off" runat="server" Enabled="false" CssClass="aspxcontrols"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator17" ControlToValidate="txtbxEmail" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter correct mailid" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator16" CssClass="ErrorMsgRight" runat="server" ErrorMessage="Enter the correct mailid" ControlToValidate="txtbxEmail" SetFocusOnError="True" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Text="Enter the correct mailid"></asp:RegularExpressionValidator>--%>
                            </div>
                            <div class="col-sm-3 col-md-3">
                                <asp:Label runat="server" Text="Website"></asp:Label>
                                <asp:TextBox ID="txtbxwebsite" autocomplete="off" runat="server" Enabled="false" CssClass="aspxcontrols"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator18" ControlToValidate="txtbxwebsite" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter correct website" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>

                    </div>
                </div>

                <div id="Modalheading" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog">
                        <div class="modal-content row">
                            <div class="modal-header">
                                <asp:Label Font-Italic="true" runat="server" ID="lblHeading"></asp:Label>
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                <%-- <h4>
                            <asp:Label ID="lblheadingtext"  runat="server" CssClass="modal-title"></asp:Label></h4>--%>
                            </div>
                            <div class="modal-body row">
                                <div class="col-sm-12 col-md-12">
                                    <div class="pull-left">
                                        <asp:Label ID="Label5" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblid" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-4 col-md-4 pull-left row">
                                <div class="form-group">
                                    <asp:Label ID="lblSupplierName" runat="server" Text="* Supplier Name" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtSupplierName" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblContactPerson" runat="server" Text="Contact Person" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtContactPerson" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblAddress" runat="server" Text="Address" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtAddress" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblPhoneNo" runat="server" Text="Phone No" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtPhoneNo" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblFAX" runat="server" Text="FAX" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtFAX" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblEmail" runat="server" Text="Email" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtEmail" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-sm-4 col-md-4">
                                <div class="form-group">
                                    <asp:Label ID="lblWebsite" runat="server" Text="Website" Width="100%"></asp:Label>
                                    <asp:TextBox ID="txtWebsite" autocomplete="off" runat="server" CssClass="aspxcontrols" Width="100%" />
                                    <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSectName" runat="server" SetFocusOnError="True" ControlToValidate="txtname" Display="Dynamic" ValidationGroup="ValidateSection"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <div class="pull-right">
                                    <br />
                                    <asp:Button runat="server" Text="Clear" class="btn-ok" ID="btnClear"></asp:Button>
                                    <asp:Button runat="server" Text="Save" class="btn-ok" ID="btnSavedetails" ValidationGroup="ValidateSection"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div runat="server" role="tabpanel" class="tab-pane active " id="divAplertDtls">
                    <%--<div class="col-sm-12 col-md-12 form-group pull-left ">
                    <asp:Label ID="lblAlrtdtls" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>--%>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Location"></asp:Label>
                            <asp:DropDownList ID="ddlLocatn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator7" ControlToValidate="ddlLocatn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the Location" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Division"></asp:Label>
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Department"></asp:Label>
                            <asp:DropDownList ID="ddlDeptmnt" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Bay"></asp:Label>
                            <asp:DropDownList ID="ddlBay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Employee Name"></asp:Label>
                            <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator10" ControlToValidate="txtEmpCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the employee Code" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Employee Code"></asp:Label>
                            <asp:TextBox ID="txtEmpCode" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator10" ControlToValidate="txtEmpCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the employee Code" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-sm-1 col-md-1">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Font-Bold="true" Text="Generate" CssClass="btn-ok" AutoPostBack="true" Visible="true" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Code"></asp:Label>
                            <asp:TextBox ID="txtCode" Enabled="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator10" ControlToValidate="txtEmpCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the employee Code" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane active" id="divWrntyAMCDtls">
                    <%--  <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label1" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>--%>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Warranty Description"></asp:Label>
                            <asp:TextBox ID="txtWrntyDesc" autocomplete="off" runat="server" CssClass="aspxcontrols" TextMode="multiline"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator21" ControlToValidate="txtWrntyDesc" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Warranty Description" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator18" CssClass="ErrorMsgRight" runat="server" ErrorMessage="Enter the correct mailid" ControlToValidate="txtWrntyDesc" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter the Warranty Description"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Contact Person"></asp:Label>
                            <asp:TextBox ID="txtContperson" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator22" ControlToValidate="txtContperson" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Contact Person" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator19" CssClass="ErrorMsgRight" runat="server" ErrorMessage="please enter Contact Person" ControlToValidate="txtContperson" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="Enter the Warranty Description"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="AMC CompanyName"></asp:Label>
                            <asp:TextBox ID="txtbxAMCompname" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator23" ControlToValidate="txtbxAMCompname" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter AMC CompanyName" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator20" CssClass="ErrorMsgRight" runat="server" ErrorMessage="please enter AMC CompanyName" ControlToValidate="txtbxAMCompname" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="please enter AMC CompanyName"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="AMC From Date"></asp:Label>
                            <asp:TextBox ID="txtbxAMCfrmDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtbxAMCfrmDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxAMCfrmDate" TargetControlID="txtbxAMCfrmDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator24" ControlToValidate="txtbxAMCfrmDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter From Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin row">

                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="AMC To Date"></asp:Label>
                            <asp:TextBox ID="txtbxAMCtoDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtbxAMCtoDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxAMCtoDate" TargetControlID="txtbxAMCtoDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator25" ControlToValidate="txtbxAMCtoDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter AMC To Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="AMC Contact Person"></asp:Label>
                            <asp:TextBox ID="txtbxContprsn" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator26" ControlToValidate="txtbxContprsn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter AMC Contact Person" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator21" CssClass="ErrorMsgRight" runat="server" ErrorMessage="please enter AMC Contact Person" ControlToValidate="txtbxContprsn" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}" Text="please enter AMC Contact Person"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Phone"></asp:Label>
                            <asp:TextBox ID="txtbxPhno" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator27" ControlToValidate="txtbxPhno" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter Phone" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator22" runat="server" CssClass="ErrorMsgRight" ControlToValidate="txtbxPhno" Display="Dynamic" ErrorMessage="please enter  Phone" SetFocusOnError="True" ValidationExpression="[0-9]{10}"></asp:RegularExpressionValidator>--%>
                        </div>

                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane active " id="divAssetDeletion">
                    <%--  <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label2" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>--%>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Asset Deletion"></asp:Label>
                            <asp:DropDownList ID="ddlDeletion" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtDlnDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDlnDate" TargetControlID="txtDlnDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator9" ControlToValidate="txtDlnDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Asset Deletion Date"></asp:Label>
                            <asp:TextBox ID="txtdeletionDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtdeletionDate" TargetControlID="txtdeletionDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator8" ControlToValidate="txtdeletionDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sale/Scrap Valve"></asp:Label>
                            <asp:TextBox ID="txtbxValue" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <%--  <div class="col-sm-3 col-md-3">
                        <asp:Label runat="server" Text="Reason for Deletion"></asp:Label>
                        <asp:dropdownlist ID="ddlReason" autocomplete="off" runat="server" autopostback="true" CssClass="aspxcontrols">                          
                        </asp:dropdownlist>
                    </div>--%>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Remarks"></asp:Label>
                            <asp:TextBox ID="txtremark" autocomplete="off" runat="server" Height="80px" Width="600px" TextMode="MultiLine" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div runat="server" role="tabpanel" class="tab-pane active " id="divLoanAsst">
                    <%-- <div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label3" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>--%>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="From whome"></asp:Label>
                            <asp:TextBox ID="txtloanWhome" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Address"></asp:Label>
                            <asp:TextBox ID="txtLoanAddress" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:TextBox ID="txtloanAmount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Agreement No"></asp:Label>
                            <asp:TextBox ID="txtloanAgrmnt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtloandate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtloandate_CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtloandate" TargetControlID="txtloandate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Imported foreign currency type"></asp:Label>
                            <asp:DropDownList ID="ddlCurrencytypeloan" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Width="100%"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Exchange Date"></asp:Label>
                            <asp:TextBox ID="txtLoanExcngDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtLoanExcngDate_CalendarExtender4" CssClass="cal_Theme1" runat="server" PopupButtonID="txtLoanExcngDate" TargetControlID="txtLoanExcngDate" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Exchange Amount" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtLnExchgeAmt" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount in Ruppees" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtLnAmtRs" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Repaid" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtLnRepaid" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Balance to Date" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtBlnceTpaid" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" CssClass="cal_Theme1" runat="server" PopupButtonID="txtBlnceTpaid" TargetControlID="txtBlnceTpaid" Format="dd/MM/yyyy" PopupPosition="TopRight"></cc1:CalendarExtender>
                        </div>
                    </div>
                </div>

                <div runat="server" role="tabpanel" class="tab-pane active " id="divInsuranceDetails">
                    <%--<div class="col-sm-12 col-md-12 form-group pull-left " style="padding: 0px">
                    <asp:Label ID="Label4" runat="server" Text="" CssClass="h5" Font-Bold="true"></asp:Label>
                </div>--%>
                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Policy No"></asp:Label>
                            <asp:TextBox ID="txtbxPlyNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator3" ControlToValidate="txtbxPlyNo" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Polycy Number" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:TextBox ID="txtbxAmt" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtbxAmt" Display="Dynamic" ErrorMessage="please enter the Amount" SetFocusOnError="True" ValidationExpression="^[0-9]+(\.[0-9]{1,2})?$"></asp:RegularExpressionValidator>--%>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator2" ControlToValidate="txtbxAmt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Amount" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                        </div>

                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="BrokerName"></asp:Label>
                            <asp:TextBox ID="txtbxBrkName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator5" ControlToValidate="txtbxBrkName" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the BrokerName" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtbxBrkName" Display="Dynamic" ErrorMessage="please enter the BrokerName" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,25}"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Company Name"></asp:Label>
                            <asp:TextBox ID="txtCmpName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator6" ControlToValidate="txtCmpName" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please enter the Company Name" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCmpName" Display="Dynamic" ErrorMessage="please the Company Name" SetFocusOnError="True" ValidationExpression="^[a-zA-Z'.\s]{1,50}"></asp:RegularExpressionValidator>--%>
                        </div>

                    </div>

                    <div class="col-sm-12 col-md-12 divmargin row">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="From Date"></asp:Label>
                            <asp:TextBox ID="txtbxfrmDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtbxfrmDate_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxfrmDate" TargetControlID="txtbxfrmDate" Format="dd/MM/yyyy" PopupPosition="Bottomright"></cc1:CalendarExtender>
                            <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator4" ControlToValidate="txtbxfrmDate" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please Select the Date" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtbxfrmDate" Display="Dynamic" ErrorMessage="please enter the date in dd/mm/yy formate" SetFocusOnError="True" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"></asp:RegularExpressionValidator>--%>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="To Date"></asp:Label>
                            <asp:TextBox ID="txtbxtoDate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtbxtoDate_CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxtoDate" TargetControlID="txtbxtoDate" Format="dd/MM/yyyy" PopupPosition="Bottomright"></cc1:CalendarExtender>
                        </div>

                    </div>
                </div>
            </div>
        </div>
                             <div class="col-sm-6 col-md-6" style="padding-left:15px">
        <div id="divcollapse" runat="server" data-toggle="collapse" visible="false" data-target="#collapseRRIT"><b><i style="cursor:pointer;">Changed Details</i></b></div>
            </div>
        <div id="collapseRRIT" class="collapse">
             <asp:Panel runat="server" ID="pnlGovt">
                       <div class="col-sm-12 col-md-12" style="padding: 10px; overflow-x: scroll;">
                <asp:GridView ID="GvChangeddetails" Visible="false" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="60%">
                   <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField DataField="usr_FullName" HeaderText="Name" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="old" HeaderText="Old Asset Type" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="New" HeaderText="New Asset Type" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField DataField="AFAM_TrAssetAge" HeaderText="Old Useful life of Asset" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFAM_AssetAge" HeaderText="New Useful life of Asset" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="AFAM_YearID" HeaderText="Financial Year" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                </asp:GridView>
            </div>
             </asp:Panel>
            </div>
        <asp:TextBox ID="txtmasterid" autocomplete="off" runat="server" Visible="false"></asp:TextBox>


        <div class="col-sm-12 col-md-12 divmargin">
            <div class="col-sm-12 col-md-12">
                <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <div class="col-sm-8 col-md-8">
                <div class="form-group">
                    <asp:Label ID="lblCurrentStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
                </div>
            </div>
        </div>
        <div id="ModalAdditionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
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


        <div class=" modal fade" id="myAttchment" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Attachment</h4>
                    </div>
                    <div class="modal-body row">
                        <div class="col-sm-12 col-md-12">
                            <asp:Label ID="lblTax" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-5 col-md-5" style="padding-left: 0px">
                                <div class="form-group">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:Button ID="btnAttch" runat="server" Text="Add" CssClass="btn-ok" />
                            </div>
                            <div class="col-sm-1 col-md-1">
                                <asp:Button ID="btnIndex" runat="server" Text="Index" CssClass="btn-ok" />
                            </div>
                        </div>


                        <div class="col-sm-12 col-md-12" runat="server">
                            <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-Width="1%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" CssClass="hvr-bounce-in" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPath" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField  HeaderStyle-Width="40%" HeaderText="File Name">
                                        <ItemTemplate>
                                             <asp:LinkButton ID="lblFilename" runat="server" CommandName="OPENPAGE" Font-Bold="False"  Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                    <asp:BoundField DataField="Extension" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                    <asp:BoundField DataField="CreatedOn" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="modal-footer">
                    </div>


                </div>
            </div>
        </div>

        <div id="ModalChangeClass" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content row">
                    <div class="modal-header">
                        <h4 class="modal-title"><b>Change Class</b></h4>
                         <asp:Label runat="server" id="lblmodalError" CssClass="ErrorMsgRight"></asp:Label>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                       
                    </div>
                    <div class="modal-body">

                        <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="* Asset Class"></asp:Label>
                            <asp:DropDownList ID="ddlAssClass" runat="server" Enabled="false" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="* Asset No" autocomplete="off"></asp:Label>
                            <asp:TextBox ID="txtAssNo" Enabled="false" runat="server" Font-Size="X-Small" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text=" Asset Code"></asp:Label>
                            <asp:TextBox ID="txtAssCode" runat="server" CssClass="aspxcontrols" Enabled="false" Font-Size="X-Small" autocomplete="off"></asp:TextBox>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="* Asset Description"></asp:Label>
                            <asp:TextBox ID="txtAssDesc" runat="server" autocomplete="off" Enabled="false" Font-Size="X-Small" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                         <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="* Useful life of Asset"></asp:Label>
                            <asp:TextBox ID="txtbxAstAgeOld" runat="server" autocomplete="off" Enabled="false" Font-Size="X-Small" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <fieldset class="col-sm-12 col-md-12">
                            <hr />
                        </fieldset>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="* Change Asset Class"></asp:Label>
                            <asp:DropDownList ID="ddlChangeClass" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                        </div>
                           <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="Change Useful life of Asset"></asp:Label>
                            <asp:TextBox ID="txtchangeAstAge" runat="server" autocomplete="off"  Font-Size="X-Small" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label runat="server" Text="Remark"></asp:Label>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="aspxcontrols" Height="100px" TextMode="MultiLine" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="pull-right">
                            <asp:Button runat="server" Text="Update" class="btn-ok" ID="btnUpdateClass" ValidationGroup="Validateheading"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><b>Index Details</b></h4>
                    </div>
                    <div class="modal-body row">
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-12 col-md-12">
                                <div class="pull-left">
                                    <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <asp:Label ID="lblcabinet" runat="server" Text="Cabinet"></asp:Label>
                                    <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblSubcabinet" runat="server" Text="Sub cabinet"></asp:Label>
                                    <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
                                    <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblDocumentType" runat="server" Text="Document Type"></asp:Label>
                                    <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <div class="form-group">
                                    <br />
                                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                    <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="lblTitle" runat="server" Text="Title"></asp:Label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                </div>

                            </div>

                        </div>
                        <div class="col-sm-12 col-md-12" style="padding: 0px">
                            <div class="col-sm-6 col-md-6">
                                <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                    <Columns>

                                        <asp:TemplateField HeaderStyle-Width="1%" HeaderText="DescriptorID" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescriptorID" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DescriptorID") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Descriptor" HeaderText="Descriptor" HeaderStyle-Width="40%"></asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-6 col-md-6">
                                <asp:GridView ID="gvKeywords" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="100%">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtKeywords" runat="server" CssClass="aspxcontrols" Text='<%# DataBinder.Eval(Container, "DataItem.Key") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index" />
                    </div>
                </div>
            </div>
        </div>

    </div>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>

