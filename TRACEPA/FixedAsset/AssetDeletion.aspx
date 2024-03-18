<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="AssetDeletion.aspx.vb" Inherits="TRACePA.AssetDeletion" %>

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
        /* div {
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
            $('#<%=ddlAssetClass.ClientID%>').select2();
            $('#<%=ddlAsset.ClientID%>').select2();
            $('#<%=ddlToLocation.ClientID%>').select2();
            $('#<%=ddlToDivision.ClientID%>').select2();
            $('#<%=ddlToDepartment.ClientID%>').select2();
            $('#<%=ddlToBay.ClientID%>').select2();
        });

        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })

    </script>

    <div class="loader"></div>
              <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Asset Deletion" Font-Size="Small"></asp:Label>
                <div class="pull-right">
                <asp:ImageButton ID="imgbtnDelete" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Delete" ValidationGroup="Validate" />
                <%--    <asp:ImageButton ID="imgbtnsave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="save" ValidationGroup="Validate" />--%>
                <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Checkmark24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
                <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                <asp:ImageButton ID="ImgBtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
            </div>
            </div>
            </div>
    <div class="card">
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" Font-Size="Larger" CssClass="ErrorMsgLeft"></asp:Label>
        <asp:Label runat="server" ID="lblSalValue" Visible="false" Text=""></asp:Label>
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
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Transaction no"></asp:Label>
                <asp:TextBox ID="txtDelTransNo" autocomplete="off" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:TextBox>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">

                <asp:Label runat="server" Text="Status :"></asp:Label>
                <asp:Label ID="lblstatus" runat="server" Text="Open" CssClass="Label" Font-Bold="true"></asp:Label>
            </div>
        </div>
    </div>

    <%--    <div class="col-sm-12 col-md-12 form-group"></div>--%>

    <fieldset class="col-sm-12 col-md-12">
        <legend class="legendbold"><b style="font-family: Georgia; color: darkcyan; font-size: 12px">Asset Details.</b></legend>
    </fieldset>

    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Location"></asp:Label>
            <asp:DropDownList ID="ddlLocatn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Division"></asp:Label>
            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Department"></asp:Label>
            <asp:DropDownList ID="ddlDeptmnt" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Bay"></asp:Label>
            <asp:DropDownList ID="ddlBay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Asset Class"></asp:Label>
            <asp:DropDownList ID="ddlAssetClass" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="* Asset"></asp:Label>
            <asp:DropDownList ID="ddlAsset" runat="server" CssClass="aspxcontrols" AutoPostBack="true" autocomplete="off"></asp:DropDownList>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group"></div>

    <fieldset class="col-sm-12 col-md-12">
        <legend class="legendbold"><b style="font-family: Georgia; color: darkcyan; font-size: 12px">Transaction Details.</b></legend>
    </fieldset>

    <div class="col-sm-12 col-md-12 row">
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Transaction Type"></asp:Label>
                <asp:DropDownList ID="ddlDeletion" runat="server" CssClass="aspxcontrols" Font-Size ="X-Small" AutoPostBack="true">
                    <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                    <asp:ListItem Text="Sold" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Transfer" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Stolen" Value="3"></asp:ListItem>
                    <asp:ListItem Text="Destroyed" Value="4"></asp:ListItem>
                    <asp:ListItem Text="Obsolete" Value="5"></asp:ListItem>
                    <asp:ListItem Text="Govt Subsidy" Value="6"></asp:ListItem>
                    <%-- <asp:ListItem Text="Repair" Value="6"></asp:ListItem>--%>
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <br />
                <asp:RadioButton ID="rboPartial" Text="Partial" AutoPostBack="true" GroupName="Select" runat="server" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     
                <asp:RadioButton ID="rboFully" Text="Fully" AutoPostBack="true" GroupName="Select" runat="server" />
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Transaction Date"></asp:Label>
                <asp:TextBox ID="txtdeletionDate" autocomplete="off" runat="server" Width="70%" CssClass="aspxcontrols" placeholder="dd/MM/yyyy"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender4" CssClass="cal_Theme1" runat="server" PopupButtonID="txtdeletionDate" TargetControlID="txtdeletionDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Original Quantity"></asp:Label>
                <asp:TextBox ID="txtOrigQuantity" autocomplete="off" Width="70%" runat="server"  Enabled="false" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Original Cost"></asp:Label>
                <asp:TextBox ID="txtOriginalCost" autocomplete="off" Width="70%" Style="direction: ltr; text-align: right" CssClass="aspxcontrols" runat="server" Enabled="false"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Dep.Till Date"></asp:Label>
                <asp:TextBox ID="txtDepAmount" autocomplete="off" Width="70%" runat="server" Style="direction: ltr; text-align: right" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
    </div>

<%--    <div class="col-sm-12 col-md-12 form-group"></div>--%>

    <fieldset class="col-sm-12 col-md-12">
        <legend class="legendbold"><b style="font-family: Georgia; color: darkcyan; font-size: 12px">Deletion/Removal Details.</b></legend>
    </fieldset>

    <div class="col-sm-12 col-md-12 row">
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Description"></asp:Label>
                <asp:TextBox ID="txtdeldesc" runat="server" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <asp:Label runat="server" Text="Quantity"></asp:Label>
                <asp:TextBox ID="txtQuantity" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Sales Price"></asp:Label>
                <asp:TextBox ID="txtSalesPrice" runat="server" CssClass="aspxcontrols" Style="direction: ltr; text-align: right" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Transaction Amount"></asp:Label>
            <asp:TextBox ID="txtDeletionAmount" autocomplete="off" runat="server" Style="direction: ltr; text-align: right" CssClass="aspxcontrols"></asp:TextBox>
        </div>

        <div class="col-sm-1 col-md-1">
            <div class="form-group">
                <br />
                <asp:Button ID="btnCalculate" runat="server" Font-Bold="true" Text="OK" CssClass="btn-ok" AutoPostBack="true" Visible="true" />
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Depreciation"></asp:Label>
                <asp:TextBox ID="txtDepreciation" runat="server" CssClass="aspxcontrols" Style="direction: ltr; text-align: right" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="WDV Value"></asp:Label>
                <asp:TextBox ID="txtWDVValue" runat="server" CssClass="aspxcontrols" Style="direction: ltr; text-align: right" TextMode="MultiLine"></asp:TextBox>
            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12" style="padding-left: 0px; width: 90%">

        <div class="col-sm-3 col-md-3">
            <asp:Label runat="server" Text="Remarks"></asp:Label>
            <asp:TextBox ID="txtRemarks" runat="server" CssClass="aspxcontrols" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div class="col-sm-2 col-md-2">
            <br />
            <asp:Label ID="lblPorL" runat="server" CssClass="Label"></asp:Label>
            <asp:Label ID="lblAmount" runat="server" CssClass="Label"></asp:Label>
        </div>
    </div>
    <%--    </div>--%>

    <%--   <div class="col-sm-12 col-md-12 form-group"></div>--%>

    <asp:Panel ID="PnlSold" runat="server" Visible="false">
        <%--   <div class="col-sm-12 col-md-12" style="padding-left: 0px">--%>
        <div class="col-sm-3 col-md-3">
            <asp:Label ID="lblPaymenttype" runat="server" Text="Payment Type"></asp:Label>
            <asp:DropDownList ID="ddlPaymenttype" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                <asp:ListItem Text="Payment Types" Value="0"></asp:ListItem>
                <asp:ListItem Text="Cheque" Value="1"></asp:ListItem>
                <asp:ListItem Text="Cash" Value="2"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <%--  </div>--%>
    </asp:Panel>

    <%--    <div class="col-sm-12 col-md-12 form-group"></div>--%>

    <fieldset class="col-sm-12 col-md-12">
        <legend class="legendbold"><b style="font-family: Georgia; color: darkcyan; font-size: 12px">Continued Value of Org.Asset after Deletion.</b></legend>
    </fieldset>



    <%--    <div class="col-sm-12 col-md-12 form-group"></div>
    <div class="col-sm-12 col-md-12 form-group"></div--%>

    <div class="col-sm-12 col-md-12 row">

        <asp:Panel ID="PnlTransfer" runat="server" Visible="false">
            <%-- <div class="col-sm-12 col-md-12" style="padding-left: 0px">--%>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="Date of Received"></asp:Label>
                    <asp:TextBox ID="txtDateofReceived" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDateofReceived" TargetControlID="txtDateofReceived" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="* To Location"></asp:Label>
                    <asp:DropDownList ID="ddlToLocation" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="To Division"></asp:Label>
                    <asp:DropDownList ID="ddlToDivision" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="To Department"></asp:Label>
                    <asp:DropDownList ID="ddlToDepartment" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-2 col-md-2">
                <div class="form-group">
                    <asp:Label runat="server" Text="To Bay"></asp:Label>
                    <asp:DropDownList ID="ddlToBay" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="col-sm-12 col-md-12 row">
                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Cost of Transport"></asp:Label>
                        <asp:TextBox ID="txtCostofTransport" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>

                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Installation Cost"></asp:Label>
                        <asp:TextBox ID="txtInstallationCost" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                    </div>
                </div>

                <div class="col-sm-2 col-md-2">
                    <div class="form-group">
                        <asp:Label runat="server" Text="Date of Initiate"></asp:Label>
                        <asp:TextBox ID="txtDateofInitiate" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" CssClass="cal_Theme1" runat="server" PopupButtonID="txtDateofInitiate" TargetControlID="txtDateofInitiate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>


    <%--    <div class="col-sm-12 col-md-12 form-group"></div>--%>

    <div class="col-sm-12 col-md-12 row">
        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Asset Value"></asp:Label>
                <asp:TextBox ID="txtContValue" runat="server" Enabled="false" Style="direction: ltr; text-align: right" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="Depreciation"></asp:Label>
                <asp:TextBox ID="txtContDep" runat="server" Enabled="false" Style="direction: ltr; text-align: right" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>

        <div class="col-sm-2 col-md-2">
            <div class="form-group">
                <asp:Label runat="server" Text="WDV Value"></asp:Label>
                <asp:TextBox ID="txtContWDVValue" runat="server" Enabled="false" Style="direction: ltr; text-align: right" CssClass="aspxcontrols"></asp:TextBox>
            </div>
        </div>
    </div>


    <div class="col-md-12">
        <div id="divInsuranceDetails" runat="server" data-toggle="collapse" data-target="#collapseInsuranceDetails"><a href="#"><b><i>Click here to Insurance Details...</i></b></a></div>
    </div>
   <%-- <div class="col-md-12">
        <br />
    </div>--%>
    <div class="col-md-12" style="padding: 0px">
        <div id="collapseInsuranceDetails" class="collapse">
            <div class="col-sm-12 col-md-12 form-group">
                <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Claimed Number"></asp:Label>
                    <asp:TextBox ID="txtClaimedNo"  runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Amount Claimed"></asp:Label>
                    <asp:TextBox ID="txtAmtClaimed" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Claimed Date"></asp:Label>
                    <asp:TextBox ID="txtClaimedDate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" CssClass="cal_Theme1" runat="server" PopupButtonID="txtClaimedDate" TargetControlID="txtClaimedDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            <%--</div>--%>
    <%--        <div class="col-sm-12 col-md-12 form-group">--%>
                <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Amount Received"></asp:Label>
                    <asp:TextBox ID="txtAmtRecved" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Reference No."></asp:Label>
                    <asp:TextBox ID="txtAmtRefNo" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                </div>
                <div class="col-sm-2 col-md-2" style="padding-left: 0px">
                    <asp:Label runat="server" Text="Received Date."></asp:Label>
                    <asp:TextBox ID="txtReceivedDate" runat="server" autocomplete="off" CssClass="aspxcontrols"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender5" CssClass="cal_Theme1" runat="server" PopupButtonID="txtReceivedDate" TargetControlID="txtReceivedDate" Format="dd/MM/yyyy" PopupPosition="BottomRight"></cc1:CalendarExtender>
                </div>
            </div>
        </div>
    </div>
        </div>
    <div id="ModalDeletionValidation1" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType1" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblAssetdeletionValidationMsg1" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" ID="BtnYES" autopostback="true" Text="YES"></asp:Button>
                    <asp:Button runat="server" class="btn-OK" ID="BtnNo" Text="NO"></asp:Button>
                </div>
            </div>
        </div>
    </div>

    <div id="ModalDeletionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblDeletionValidationMsg" runat="server"></asp:Label></strong>
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
                    <asp:Button ID="btnCustYes" runat="server" Text="Yes" CssClass="btn-ok" />
                    <asp:Button ID="btnCustNo" runat="server" Text="No" CssClass="btn-ok" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

