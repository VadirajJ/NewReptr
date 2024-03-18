<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="AssetTransactionAddition.aspx.vb" Inherits="TRACePA.AssetTransactionAddition" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
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
      /*   div {
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
            $('#<%=ddlLocatn.ClientID%>').select2();
            $('#<%=ddlDivision.ClientID%>').select2();
            $('#<%=ddlDeptmnt.ClientID%>').select2();
            $('#<%=ddlBay.ClientID%>').select2();
            $('#<%=drpAstype.ClientID%>').select2();
            $('#<%=txtbxItmCode.ClientID%>').select2();

$('#<%=ddlTrTypes.ClientID%>').select2();
$('#<%=ddlExtTrnNo.ClientID%>').select2();
        });

       $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

        function Validate() {

            if (document.getElementById('<%=ddlAssetTrnfr.ClientID %>').selectedIndex == 0) {
                alert('Select Asset Transfer.');
                document.getElementById('<%=ddlAssetTrnfr.ClientID%>').focus()
                return false
            }
            if (document.getElementById('<%=ddlTrTypes.ClientID %>').selectedIndex == 0) {
                alert('Select Transaction Type.');
                document.getElementById('<%=ddlTrTypes.ClientID%>').focus()
                return false
            }
        <%--    if (document.getElementById('<%=ddlSupplier.ClientID %>').selectedIndex == 0) {
                alert('Select Supplier Name.');
                document.getElementById('<%=ddlSupplier.ClientID%>').focus()
                return false
            }--%>
            if (document.getElementById('<%=drpAstype.ClientID %>').selectedIndex == 0) {
                alert('Select Asset Type.');
                document.getElementById('<%=drpAstype.ClientID%>').focus()
                return false
            }
        }
    </script>
    <%--   <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=drpAstype.ClientID%>').select2();
            $('#<%=txtbxItmCode.ClientID%>').select2();
        });
    </script>--%>

    <div class="loader"></div>
                   <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Asset Addition" Font-Size="Small"></asp:Label>
               <div class="pull-right">
                    <asp:ImageButton ID="ImgBtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnsave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="Imgbtnphyvrfn" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Physical Verification" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnAttachment" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Attachment" Visible="false" Style="padding-right: 0px;"></asp:ImageButton><span class="badge"><asp:Label ID="lblBadgeCount" Visible="false" runat="server" Text="0"></asp:Label></span>
                    <asp:ImageButton ID="imgbtnView" CssClass="activeIcons hvr-bounce-out" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="View" CausesValidation="false" />
                    <asp:ImageButton ID="ImgbtnActivate" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" Visible="false" title="Approve" ValidationGroup="ValidateApprove" />
                    <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                </div>
            </div>
            </div>
    <div class="card">
    <div class="col-sm-12 col-md-12 row" >
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="clearfix divmargin"></div>

  <div class="col-sm-12 col-md-12 row">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Customer Name"></asp:Label>
                <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>
      <div class="col-sm-3 col-md-3">
          <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
          <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
          </asp:DropDownList>
      </div>
        <br />
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Voucher No :"></asp:Label>
                <asp:DropDownList ID="ddlExtTrnNo" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Font-Bold="true" Enabled="false"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Status :"></asp:Label>
                <asp:Label ID="lblstatus" runat="server" Text="Open" CssClass="Label" Font-Bold="true"></asp:Label>
            </div>
        </div>

        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:LinkButton ID="AdditionalDtls" runat="server" ForeColor="#009900" Font-Bold="true" Font-Underline="true"
                    PostBackUrl="~/FixedAsset/AssetAdditionExcelUpload.aspx">Excel Upload</asp:LinkButton>
            </div>
        </div>
    </div>

    <%-- <div class="col-sm-12 col-md-12 form-group"></div>--%>

    <div class="col-sm-12 col-md-12 row">
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Location"></asp:Label>
                <asp:DropDownList ID="ddlLocatn" Enabled="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator7" ControlToValidate="ddlLocatn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the Location" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Division"></asp:Label>
                <asp:DropDownList ID="ddlDivision" Enabled="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Department"></asp:Label>
                <asp:DropDownList ID="ddlDeptmnt" Enabled="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Bay"></asp:Label>
                <asp:DropDownList ID="ddlBay" Enabled="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <%--<asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator19" ControlToValidate="ddlDeptmnt" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="please select the department" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 row">
        <%--  <<%--div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="* Asset Transfer" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddlAssetTrnfr" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
        </asp:DropDownList>
        <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAssetTrnfr" ControlToValidate="ddlAssetTrnfr" Display="Dynamic" SetFocusOnError="True" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        <%-- </%--div>--%>
        <%--      <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Currency Types" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddlCurencyType" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
        </asp:DropDownList>
        <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCurencyType" ControlToValidate="ddlCurencyType" Display="Dynamic" SetFocusOnError="True" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        <%--</div>--%>
        <%-- <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Amount" Visible="false"></asp:Label>
        <asp:TextBox ID="txtCurency" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
        </asp:TextBox>
        <%-- </div>--%>
        <%-- <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Date Of Addition" Visible="false"></asp:Label>
        <asp:TextBox ID="txtDtAddtn" runat="server" CssClass="aspxcontrols" AutoComplete="off" Visible="false"></asp:TextBox>
        <%--   <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDtAddtn" TargetControlID="txtDtAddtn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>--%>
        <%--  <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtDtAddtn" ControlToValidate="txtDtAddtn" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVtxtDtAddtn" runat="server" ControlToValidate="txtDtAddtn" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
        <%--    </div>--%>

        <%--<div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Depreciation Rate" Visible="false"></asp:Label>
        <asp:TextBox ID="txtDeprcn" runat="server" CssClass="aspxcontrols" AutoPostBack="true" Visible="false">
        </asp:TextBox>
        <%--   </div>--%>


        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Asset Class"></asp:Label>
                <asp:DropDownList ID="drpAstype" Enabled="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdrpAstype" ControlToValidate="drpAstype" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Asset"></asp:Label>
                <asp:DropDownList ID="txtbxItmCode" Enabled="false" runat="server" CssClass="aspxcontrols" AutoPostBack="true" autocomplete="off"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmCode" ControlToValidate="txtbxItmCode" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Description" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="* Transaction Type"></asp:Label>
                <asp:DropDownList ID="ddlTrTypes" Enabled="false" runat="server" CssClass="aspxcontrols" Font-Size ="X-Small" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVTRType" ControlToValidate="ddlTrTypes" Display="Dynamic" runat="server" ValidationGroup="Validatesave"></asp:RequiredFieldValidator>
            </div>
        </div>
           <div class="col-sm-3 col-md-3">
                <div class="form-group">       
                    <br />
                    <asp:CheckBox ID="ChkAddDep" Visible="false" runat="server" AutoPostBack="true" Style="direction: ltr; text-align: right" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="myCheckbox"></asp:CheckBox>
                    <asp:Label runat="server" ID="chkpoint" Visible="false" Text="Additional 20% Depreciation for Plant and Equipment"></asp:Label>
                </div>
            </div>
 

        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label runat="server" Text="Asset Location" Visible="false"></asp:Label>
                <asp:TextBox ID="txtLocID" runat="server" Visible="false" CssClass="aspxcontrols" autocomplete="off"></asp:TextBox>
                <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVLocation" runat="server" ControlToValidate="txtLocID" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
        <%--    <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="AssetNo" Visible="false"></asp:Label>
        <asp:TextBox ID="txtAssetNo" runat="server" CssClass="aspxcontrolsdisable" Visible="false"></asp:TextBox>
        <%--</div>--%>


        <%-- <div class="col-sm-2 col-md-2">--%>
        <asp:Label runat="server" Text="Useful Life(yrs)" Visible="false"></asp:Label>
        <asp:TextBox ID="txtbxAstAge" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
        <%-- </div>--%>


        <%--      <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Asset Class Description" Visible="false"></asp:Label>
        <asp:TextBox ID="txtbxDscrptn" runat="server" CssClass="aspxcontrols" autocomplete="off" Visible="false"></asp:TextBox>
        <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDscrptn" ControlToValidate="txtbxDscrptn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Description" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        <%--  </div>--%>

        <%--   <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Asset Description" Visible="false"></asp:Label>
        <asp:TextBox ID="txtbxItmDecrtn" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
        <%--      <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxItmDecrtn" ControlToValidate="txtbxItmDecrtn" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter the ItemDescription" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>--%>

        <%-- <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="Quantity" Visible="false"></asp:Label>
        <asp:TextBox ID="txtbxQty" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
        <%--   <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxQty" ControlToValidate="txtbxQty" Display="Dynamic" SetFocusOnError="True" runat="server" ErrorMessage="Enter The Quantity" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        <%-- </div>--%>
    </div>

    <%--  <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
     </div>--%>

    <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">

        <%--  <div class="col-sm-3 col-md-3">--%>
        <asp:Label runat="server" Text="* Asset RefNo" Visible="false"></asp:Label>
        <asp:TextBox ID="txtAstNOSup" runat="server" CssClass="aspxcontrols" autocomplete="off" Visible="false"></asp:TextBox>
        <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAstRefNo" ControlToValidate="txtAstNOSup" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
        <%--      </div>--%>



        <%--   <div class="col-sm-2 col-md-2">--%>

        <%-- <cc1:CalendarExtender ID="txtbxDteofPurchase_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteofPurchase" TargetControlID="txtbxDteofPurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteofPurchase1" runat="server" ControlToValidate="txtbxDteofPurchase" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
        <%--  </div>--%>
        <%--<div class="col-sm-2 col-md-2">--%>
        <asp:Label runat="server" Text="Date of Put to use" Visible="false"></asp:Label>
        <asp:TextBox ID="txtbxDteCmmunictn" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
        <%--  <cc1:CalendarExtender ID="txtbxDteCmmunictn_CalendarExtender" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteCmmunictn" TargetControlID="txtbxDteCmmunictn" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="RFVtxtbxDteCmmunictn1" runat="server" ControlToValidate="txtbxDteCmmunictn" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
        <%--</div>--%>
        <%--<div class="col-sm-1 col-md-1">
            <asp:Label runat="server" Text="Amount"></asp:Label>
            <asp:TextBox ID="txtbxamount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVamount" runat="server" ControlToValidate="txtbxamount" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVamount" ControlToValidate="txtbxamount" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>--%>
    </div>

    <asp:Panel runat="server" ID="PnlAssetOpngValue" Visible="false">


        <h4><b>Asset Opening Value.</b></h4>
        <div class="clearfix divmargin"></div>
        <div class="col-sm-12 col-md-12 row">
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Date Of Purchase"></asp:Label>
                    <asp:TextBox ID="txtbxDteofPurchase" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtbxDteofPurchase" TargetControlID="txtbxDteofPurchase" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Original Cost"></asp:Label>
                    <asp:TextBox ID="txtbxamount" autocomplete="off" runat="server" Style="direction: ltr; text-align: right" CssClass="aspxcontrols"></asp:TextBox>
                    <%--<asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVamount" runat="server" ControlToValidate="txtbxamount" Display="Dynamic" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVamount" ControlToValidate="txtbxamount" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="WDV Opening Value"></asp:Label>
                    <asp:TextBox ID="txtOpeningBal" runat="server" Style="direction: ltr; text-align: right" CssClass="aspxcontrols" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Dep. for the Period"></asp:Label>
                    <asp:TextBox ID="txtDepreciableAmount" Enabled="false" runat="server" Style="direction: ltr; text-align: right" AutoPostBack="false" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label runat="server" Text="Depreciable Amount" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtDepAmount" runat="server" Style="direction: ltr; text-align: right" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel runat="server" ID="pnlForExchange" Visible="false">
        <h4 style="padding-left:10px"><b>Foreign Exchange</b></h4>
            <div class="col-sm-12 col-md-12 row">
                      <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Date"></asp:Label>
                    <asp:TextBox ID="txtExchdate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtExchdate" TargetControlID="txtExchdate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            </div>
                         <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Description"></asp:Label>
                    <asp:TextBox ID="txtExchdesc" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Doc No"></asp:Label>
                    <asp:TextBox ID="txtExchDocNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Amount"></asp:Label>
                    <asp:TextBox ID="txtExchAmount" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
                   <div class="col-sm-2 col-md-2">
                       <br />
                <div class="form-group">
                  <asp:ImageButton ID="imgbtnExchAdd" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Add24.png" runat="server" data-toggle="tooltip" data-placement="bottom" ValidationGroup="ValidateApprove" />
                </div>
            </div>
                </div>
    </asp:Panel>


    <asp:Panel runat="server" ID="pnlAddDetails" Visible="false">
        <h4 style="padding-left:10px"><b>Asset Addition Details.</b></h4>
        <div class="clearfix divmargin"></div>
        <div class="col-sm-12 col-md-12 row">

            <%--<div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="* Supplier Name"></asp:Label>
                <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSupplierName" ControlToValidate="ddlSupplier" Display="Dynamic" runat="server" ValidationGroup="Validate"></asp:RequiredFieldValidator>
            </div>--%>

            <%--<div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Supplier Code"></asp:Label>
            <asp:TextBox runat="server" autocomplete="off" CssClass="aspxcontrolsdisable" ID="txtSprCode"></asp:TextBox>
            </div>--%>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label runat="server" Text="Supplier Name"></asp:Label>
                    <asp:TextBox ID="txtSupplierName" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label runat="server" Text="Asset Description"></asp:Label>
                    <asp:TextBox ID="txtParticular" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label runat="server" Text="Doc No"></asp:Label>
                    <asp:TextBox ID="txtDocNo" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label runat="server" Text="Doc Date"></asp:Label>
                    <asp:TextBox ID="txtDocDate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="txtCDocDate" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDocDate" TargetControlID="txtDocDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            </div>
            
        </div>
      
        <div class="col-sm-12 col-md-12 row">

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Basic Cost"></asp:Label>
                    <asp:TextBox ID="txtBasicCost" Style="direction: ltr; text-align: right" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
            <%--  <div class="col-sm-1 col-md-1">
                <br />
                <asp:Label runat="server" Text="Cost"></asp:Label>
                <asp:CheckBox ID="chkCost" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="myCheckbox"></asp:CheckBox>
            </div>--%>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Tax Amount"></asp:Label>
                    &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp
                <asp:Label runat="server" Text="Cost"></asp:Label>
                    <asp:CheckBox ID="chkCost" runat="server" AutoPostBack="true" Style="direction: ltr; text-align: right" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="myCheckbox"></asp:CheckBox>

                    <asp:TextBox ID="txtTaxAmount" autocomplete="off" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-3 col-md-3">
                <div class="form-group">
                    <asp:Label runat="server" Text="Total"></asp:Label>
                    <asp:TextBox ID="txtTotal" runat="server" Style="direction: ltr; text-align: right" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Asset Value"></asp:Label>
                    <asp:TextBox ID="txtAssetValue" runat="server" Style="direction: ltr; text-align: right" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-1 col-md-1">
                <div class="form-group">
                    <br />
                    <asp:ImageButton ID="ImgBtnAddDetails" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Details" />
                </div>
            </div>
        </div>
    </asp:Panel>

    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="divPendingAssignment" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow-y: scroll; overflow-x: unset">
        <asp:GridView ID="dgAddtionalDetails" Enabled="false" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
            <Columns>
                <asp:TemplateField HeaderText="" ItemStyle-Width="0%" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPKID" runat="Server" Visible="False" Text='<%#DataBinder.Eval(Container, "DataItem.PKID")%>'></asp:Label>
                        <asp:Label ID="lblMasID" runat="Server" Visible="False" Text='<%#DataBinder.Eval(Container, "DataItem.MasID")%>'></asp:Label>
                        <asp:Label ID="lblParticulars" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Particulars") %>'></asp:Label>
                        <asp:Label ID="lblSupplierName" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SupplierName") %>'></asp:Label>
                        <asp:Label ID="lblDocNo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocNo") %>'></asp:Label>
                        <asp:Label ID="lblDocDate" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DocDate") %>'></asp:Label>
                        <%--  <asp:Label ID="lblchkCost" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.chkCost") %>'></asp:Label>--%>
                        <asp:Label ID="lblBasicCost" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BasicCost") %>'></asp:Label>
                        <asp:Label ID="lblTaxAmount" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TaxAmount") %>'></asp:Label>
                        <asp:Label ID="lblTotal" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Total") %>'></asp:Label>
                        <asp:Label ID="lblAssetValue" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AssetValue") %>'></asp:Label>
<%--                        <asp:Label ID="lblExchId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TypeId") %>'></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="Type" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtType" runat="server" ItemStyle-Width="15%" Text='<%# DataBinder.Eval(Container, "DataItem.Type") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Supplier Name" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtSupplierName" runat="server" ItemStyle-Width="15%" Text='<%# DataBinder.Eval(Container, "DataItem.SupplierName") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Asset Description" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtParticulars" runat="server" ItemStyle-Width="15%" Text='<%# DataBinder.Eval(Container, "DataItem.Particulars") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="DocNo" ItemStyle-Width="8%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDocNo" runat="server" ItemStyle-Width="8%" Text='<%# DataBinder.Eval(Container, "DataItem.DocNo") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DocDate" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtDocDate" runat="server" ItemStyle-Width="10%" Text='<%# DataBinder.Eval(Container, "DataItem.DocDate") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Basic Cost" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtBasicCost" runat="server" ItemStyle-Width="12%" Text='<%# DataBinder.Eval(Container, "DataItem.BasicCost") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Tax Amount" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTaxAmount" runat="server" AutoPostBack="true" ItemStyle-Width="12%" Text='<%# DataBinder.Eval(Container, "DataItem.TaxAmount") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" ItemStyle-Width="12%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtTotal" runat="server" ItemStyle-Width="12%" Text='<%# DataBinder.Eval(Container, "DataItem.Total") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AssetValue" ItemStyle-Width="15%">
                    <ItemTemplate>
                        <asp:TextBox ID="txtAssetValue" runat="server" ItemStyle-Width="15%" Text='<%# DataBinder.Eval(Container, "DataItem.AssetValue") %>' SelectionMode="Multiple" CssClass="aspxcontrols"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--  <asp:TemplateField ItemStyle-Width="5%">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CssClass="activeIcons hvr-bounce-out"  Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Edit" Style="padding-right: 0px;"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                <asp:TemplateField ItemStyle-Width="5%">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CssClass="activeIcons hvr-bounce-out" Visible="true" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" Style="padding-right: 0px;"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
        <div style="text-align:right;padding-right:80px;font-size:12px">
             <asp:Label ID="lblAddTotal" runat="server" Text="Total:-" Font-Bold="true"></asp:Label>
        <asp:Label ID="lblAdditionTotal" runat="server"></asp:Label>
            </div>
    <asp:Panel runat="server" Visible="false">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
            <fieldset class="col-sm-12 col-md-12">
                <legend class="legendbold">Asset Amount As per Income Tax.</legend>
            </fieldset>
            <div class="col-sm-3 col-md-3">
                <asp:Label runat="server" Text="Amount"></asp:Label>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
            <div class="col-sm-1 col-md-1">
                <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add" />
            </div>
        </div>
    </asp:Panel>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
    </div>
    <div id="myModalAttchment" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body">
                    <div class="row ">
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12 pull-left">
                            <asp:Label ID="lblBrowse" runat="server" Text="Click Browse and Select a File."></asp:Label>
                            <asp:Label ID="lblSize" runat="server" Font-Bold="True" Text=""></asp:Label>
                        </div>
                        <div class="col-sm-12 col-md-12">
                            <div class="col-sm-4 col-md-4" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:FileUpload ID="txtfile" runat="server" CssClass="btn-ok" Width="95%" AllowMultiple="true" />
                                </div>
                            </div>
                            <div class="col-sm-2 col-md-2" style="padding: 0px;">
                                <div class="form-group">
                                    <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                                    <asp:Button ID="btnScan" runat="server" Text="Scan" CssClass="btn-ok" />
                                </div>
                            </div>
                            <div class="col-sm-6 col-md-6" style="padding: 0px">
                                <div class="form-group">
                                    <asp:Label ID="lblHeadingDescription" runat="server" Text="Description" Visible="false"></asp:Label>
                                    <asp:TextBox autocomplete="off" ID="txtDescription" runat="server" CssClass="aspxcontrols"
                                        Visible="false" Width="300px"></asp:TextBox>
                                    <asp:Button ID="btnAddDesc" CssClass="btn-ok" Text="Add/Update" Visible="false" Font-Overline="False"
                                        runat="server"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--    <div id="myModalAttch" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Attachment</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblMsg1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12 pull-left">
                        <asp:Label ID="lblBrowse1" runat="server" Text="Click Browse and Select a File."></asp:Label>
                        <asp:Label ID="lblSize1" runat="server" Font-Bold="True" Text=""></asp:Label>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-4 col-md-4" style="padding: 0px;">
                            <div class="form-group">
                                <asp:FileUpload ID="txtfile1" runat="server" CssClass="btn-ok" Width="95%" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2" style="padding: 0px;">
                            <div class="form-group">
                                <asp:Button ID="btnAddAttch1" runat="server" Text="Add" CssClass="btn-ok" />
                                <asp:Button ID="btnRemoteIndex" runat="server" Text="Index" CssClass="btn-ok" />
                            </div>
                        </div>
                    </div>
                    <br />
                    &nbsp;&nbsp;

                     <div class="col-sm-12 col-md-12">
                         <div class="form-group">
                             <asp:GridView ID="gvattach1" runat="server" AutoGenerateColumns="False" class="footable" ShowHeaderWhenEmpty="true" EmptyDataText="No Records Available">
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-Width="1%">
                                         <HeaderTemplate>
                                             <asp:CheckBox ID="chkSelectAll1" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged1" />
                                         </HeaderTemplate>
                                         <ItemTemplate>
                                             <asp:CheckBox ID="chkSelect1" runat="server" CssClass="hvr-bounce-in" />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                                         <ItemTemplate>
                                             <asp:Label ID="lblPath1" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath1") %>' />
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="FileName1" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                                     <asp:BoundField DataField="Extension1" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                                     <asp:BoundField DataField="CreatedOn1" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
                                 </Columns>
                             </asp:GridView>
                         </div>
                     </div>
                </div>
                <div class="col-md-12">
                </div>
            </div>
        </div>
    </div>--%>


    <%-- <div class="col-sm-12 col-md-12">
        <div class="col-sm-2 col-md-2" style="padding: 0px;">
     <asp:Button ID="btnRemoteIndex" runat="server" visible="false" Text="Index" CssClass="btn-ok" />
</div>
</div>
    <br />&nbsp;&nbsp;--%>

    <%--<div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvattach1" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
            <Columns>
                <asp:TemplateField HeaderStyle-Width="1%">
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll1" runat="server" AutoPostBack="True" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged1" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect1" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderStyle-Width="1%" HeaderText="File Path" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblPath1" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath1") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FileName1" HeaderText="File Name" HeaderStyle-Width="40%"></asp:BoundField>
                <asp:BoundField DataField="Extension1" HeaderText="Extension" HeaderStyle-Width="30%"></asp:BoundField>
                <asp:BoundField DataField="CreatedOn1" HeaderText="Created On" HeaderStyle-Width="10%"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>--%>

    <%--    <div class="col-md-12 form-group">
        <div id="divcollapseAttachments" runat="server" visible="false" data-toggle="collapse" data-target="#collapseAttachments"><a href="#"><b><i>Click here to view Attachments...</i></b></a></div>
    </div>
    <div id="collapseAttachments" class="col-sm-12 col-md-12 collapse form-group">
        <div class="col-sm-6 col-md-6" style="max-height: 138px; padding-left: 0px; padding-right: 0px;">
            <asp:DataGrid ID="dgAttach" runat="server" AutoGenerateColumns="False" PageSize="1000" Width="100%" class="footable" OnRowDataBound="PickColor_RowDataBound">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr.No">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" Width="4%" />
                    </asp:BoundColumn>

                    <asp:TemplateColumn HeaderText="File Name">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="28%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAtchDocID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "AtchID") %>'></asp:Label>
                            <asp:Label ID="lblExt" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Ext") %>'></asp:Label>
                            <asp:Label ID="lblFile" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:Label>
                            <asp:LinkButton ID="File" CommandName="OPENPAGE" Font-Italic="true" runat="server" Visible="false" Font-Bold="False" Text='<%# DataBinder.Eval(Container.DataItem, "FName") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Description">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblFDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDescription") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Created">
                        <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" Width="30%"></HeaderStyle>
                        <ItemTemplate>
                            <b>By :</b>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedBy") %>'></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <b>On : </b>
                            <asp:Label ID="lblCreatedOn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedOn") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnView" data-toggle="tooltip" data-placement="bottom" title="VIEW" runat="server" CommandName="VIEW" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnAdd" data-toggle="tooltip" data-placement="bottom" title="Add Description" CommandName="ADDDESC" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnDownload" data-toggle="tooltip" data-placement="bottom" title="DownLoad" CommandName="OPENPAGE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" VerticalAlign="Top"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="imgbtnRemove" data-toggle="tooltip" data-placement="bottom" title="Remove" CommandName="REMOVE" runat="server" CssClass="hvr-bounce-in" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" Width="2%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <div class="col-sm-3 col-md-3">
            <asp:Image ID="imgView" runat="server" Width="250px" Height="200px" />
        </div>
    </div>--%>


    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-9 col-md-9" style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel1" Visible="false" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Debit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDrOtherHead" runat="server" ControlToValidate="ddlDrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="*General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherGL" runat="server" ControlToValidate="ddlDbOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlDbOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%--    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlDbOtherSubGL" runat="server" ControlToValidate="ddlDbOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select General Ledger" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlDbOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>

                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVtxtOtherDAmount" runat="server" ControlToValidate="txtOtherDAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateDBAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherDAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlDrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDbOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnDADD" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateDBAdd" />
                </div>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <asp:UpdatePanel ID="UpdatePanel2" Visible="false" runat="server">
                    <ContentTemplate>
                        <fieldset class="col-sm-12 col-md-12">
                            <legend class="legendbold">Credit Details</legend>
                        </fieldset>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* Head of Accounts"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherHead" runat="server" ControlToValidate="ddlCrOtherHead" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherHead" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="* General Ledger"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherGL" runat="server" ControlToValidate="ddlCrOtherGL" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Select General Ledger" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:DropDownList ID="ddlCrOtherGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label runat="server" Text="Sub General Ledger"></asp:Label>
                            <%-- <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVddlCrOtherSubGL" runat="server" ControlToValidate="ddlCrOtherSubGL" Display="Dynamic" SetFocusOnError="True"
                            ErrorMessage="Select Head of Accounts" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>--%>
                            <asp:DropDownList ID="ddlCrOtherSubGL" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        </div>
                        <div class="col-sm-2 col-md-2">
                            <asp:Label runat="server" Text="Amount"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCRAmount" runat="server" ControlToValidate="txtOtherCAmount" Display="Dynamic" SetFocusOnError="True"
                                ErrorMessage="Enter Amount" ValidationGroup="ValidateCRAdd"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtOtherCAmount" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherHead" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherGL" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlCrOtherSubGL" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="col-sm-1 col-md-1">
                    <br />
                    <asp:ImageButton ID="imgbtnOtherCADD" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" ValidationGroup="ValidateCRAdd" />
                </div>
            </div>
        </div>
    </div>


    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPaymentDetails" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>


                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="DebitOrCredit" HeaderText="DebitOrCredit" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete1" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="DELETE" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>



    <div class="col-sm-12 col-md-12 pull-left ">
        <div class="col-sm-3 col-md-3 pull-left ">
            <asp:LinkButton ID="lnkBtnPrvsTrans" Visible="false" runat="server"><h5><b><u>Previous Transaction</u></b></h5></asp:LinkButton>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 pull-right ">
    </div>
    <div class="col-sm-12 col-md-12">
        <asp:DataGrid ID="dgPrevTransDetails" Visible="false" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="20" Width="100%" class="footable">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <%-- <asp:TemplateColumn HeaderText="ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:BoundColumn DataField="HeadID" HeaderText="HeadID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLID" HeaderText="GLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLID" HeaderText="SubGLID" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                --%>

                <asp:BoundColumn DataField="AssetNo" HeaderText="Transaction No">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLCode" HeaderText="GL Code">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="GLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGL" HeaderText="SubGL">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="SubGLDescription" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="20%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="OpeningBalance" HeaderText="Opening Balance">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Debit" HeaderText="Debit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <asp:BoundColumn DataField="Credit" HeaderText="Credit">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>

                <%--      <asp:BoundColumn DataField="Balance" HeaderText="Balance" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <%--                <asp:BoundColumn DataField="DebitOrCredit" HeaderText="DebitOrCredit" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="10%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" VerticalAlign="Top" />
                </asp:BoundColumn>--%>

                <%--  <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgbtnDelete1" CssClass="hvr-bounce-in" data-toggle="tooltip" title="Delete" CommandName="DELETE" data-placement="bottom" runat="server" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="7%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>--%>
            </Columns>
        </asp:DataGrid>
    </div>


    <div id="myModalPhyvrn" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content row">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Physical Verification details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblVerfdby" runat="server" Text="VerifiedBy"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblVerfiedon" runat="server" Text="VeriedOn"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblApprobedby" runat="server" Text="ApprovedBy"></asp:Label>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:Label ID="lblapprovedon" runat="server" Text="ApprovedOn"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtVrfdby" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtVerfiedon" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                            <cc1:CalendarExtender ID="CalenderVerfiedon1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtVerfiedon" TargetControlID="txtVerfiedon" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtappedby" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="col-sm-3 col-md-3">
                            <asp:TextBox ID="txtapprovedon" autocomplete="off" runat="server" CssClass="aspxcontrols" />
                            <cc1:CalendarExtender ID="Calendarapprovedon1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtapprovedon" TargetControlID="txtapprovedon" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6">
                            <asp:Label ID="lblRemark" runat="server" Text="Remarks"></asp:Label>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:Label ID="lblremarks" runat="server" Text="Remarks"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-6 col-md-6">
                            <asp:TextBox ID="txtvrfremark" Height="50px" autocomplete="off" TextMode="MultiLine" runat="server" CssClass="aspxcontrols" />
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <asp:TextBox ID="txtAppremarks" autocomplete="off" Height="50px" TextMode="MultiLine" runat="server" CssClass="aspxcontrols" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="pull-right">
                        <asp:Button runat="server" Text="Update" ID="btnUpdatePhyvrn" autopostback="true" ValidationGroup="ValidateApprove"></asp:Button>
                        <button data-dismiss="modal" runat="server" class="btn-ok" id="Button1">
                            No
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 divmargin">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblTab" runat="server" Visible="False"></asp:Label>
        </div>
    </div>
    <div id="ModalAdditionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe pa</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssetAdditionValidationMsg" runat="server"></asp:Label></strong>
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
                                <asp:Label ID="Label1" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>


