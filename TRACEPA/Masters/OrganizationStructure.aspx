<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="OrganizationStructure.aspx.vb" Inherits="TRACePA.OrganizationStructure" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="wtv" Namespace="PowerUp.Web.UI.WebTree" Assembly="PowerUp.Web.UI.WebTree" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />
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
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>

    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>

    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Organisation Structure" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Activate" />
                    <asp:ImageButton ID="imgbtnDeActivate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="De-Activate" />
                    <asp:ImageButton ID="imgbtnWaiting" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Approve" />
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
            <br />
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-5 col-md-5">
                    <asp:Label ID="Label1" Text="Search by" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlSearch" runat="server" CssClass="aspxcontrols" Width="140px">
                    </asp:DropDownList>
                    <asp:TextBox autocomplete="off" ID="txtSearch" runat="server" Width="140px" CssClass="aspxcontrols" />
                    <asp:ImageButton ID="imgbtnSearch" runat="server" CssClass="hvr-bounce-out" data-toggle="tooltip" data-placement="bottom" title="Search" ValidationGroup="Search" />
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Search by." SetFocusOnError="True" ControlToValidate="ddlSearch" ValidationGroup="Search" Display="Static" InitialValue="Select"></asp:RequiredFieldValidator>
                </div>
            </div>

            <div class="col-sm-12 col-md-12" style="padding: 10px">
                <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblHeadingMM" runat="server" Text="Selected Path :-" Style="padding-left: 0px"></asp:Label>
                        <asp:Label ID="lblPath" runat="server" Text="Selected path" CssClass="aspxlabelbold"></asp:Label>
                    </div>
                    <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblAppStrength" runat="server" Text="Approved Strength"></asp:Label>
                            <asp:TextBox ID="txtAppStrength" autocomplete="off" runat="server" CssClass="aspxcontrols" MaxLength="4" data-toggle="tooltip" data-placement="top" title="Only Numbers"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-sm-6 col-md-6" style="padding-left: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblIRDACode" runat="server" Text="* Code"></asp:Label>
                            <asp:TextBox ID="txtIRDACode" autocomplete="off" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVIRDACode" runat="server" ControlToValidate="txtIRDACode" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <%--<div class="col-sm-3 col-md-3" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label ID="lblSalesUnitCode" runat="server" Text="* Sales Unit Code" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtSalesUnitCode" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSalesUnitCode" runat="server" ControlToValidate="txtSalesUnitCode" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>--%>

                    <%--<div class="col-sm-3 col-md-3" style="padding-left: 0px">
                <div class="form-group">
                    <asp:Label ID="lblBranchCode" runat="server" Text="* Branch Code" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtBranchCode" autocomplete="off" runat="server" CssClass="aspxcontrols" Visible="false"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVBranchCode" runat="server" ControlToValidate="txtBranchCode" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </div>--%>
                    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
                        <div class="form-group">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVName" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtName" autocomplete="off" runat="server" Text="Selected path" CssClass="aspxcontrols"></asp:TextBox>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVName" runat="server" ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVAppStrength" runat="server" ControlToValidate="txtAppStrength" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVIRDACode" runat="server" ControlToValidate="txtIRDACode" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            <%--<asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVSalesUnitCode" runat="server" ControlToValidate="txtSalesUnitCode" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVBranchCode" runat="server" ControlToValidate="txtBranchCode" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>--%>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-6" style="padding: 0px">
                    <div class="form-group">
                        <asp:Label ID="Label" runat="server" Text="Status :-"></asp:Label>
                        <asp:Label ID="lblCurrentStatus" runat="server" CssClass="aspxlabelbold"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label2" runat="server" Text="Note/Address/Remarks"></asp:Label>
                        <asp:TextBox ID="txtNote" autocomplete="off" runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="82px" MaxLength="2000"> </asp:TextBox>
                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVNote" runat="server" ControlToValidate="txtNote" Display="Dynamic" SetFocusOnError="True"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div id="divProcess" runat="server" style="padding-left: 0px; border-style: solid; border-color: inherit; border-width: medium; overflow: auto; width: 100%; height: 365px; background-color: #FFFFFF;">
                <wtv:TreeView ID="TVOrgStructure" runat="server" ForeColor="Black" AutoPostBack="True"
                    NodeImageUrl="../Images/TreeImages/FOLDER.GIF" NodeImageUrlExpanded="../Images/TreeImages/folderopen.gif"
                    NodeImageUrlEmpty="../Images/TreeImages/greenfolder.gif" CssFile="../Styles/TreeStyles.css"
                    NodeCssClassLoading="MyNodeLoading" NodeCssClass="MyNode" NodeCssClassOver="MyNodeOver"
                    NodeCssClassSelected="MyNodeSelected" PathToImagesValidate="False" PopulateOnDemand="true"
                    NodeWrap="True" PathToImages="../Images/TreeImages" Animate="False" Style="z-index: -100;"
                    Height="300px" Width="100%">
                </wtv:TreeView>
            </div>
            <div>
                <asp:TextBox autocomplete="off" ID="txtCurrentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                <asp:TextBox autocomplete="off" ID="txtDepthID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                <asp:TextBox autocomplete="off" ID="txtParentID" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                <asp:TextBox autocomplete="off" ID="txtOrgStrCurrentLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                <asp:TextBox autocomplete="off" ID="txtOrgStrNextLvlName" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
                <asp:TextBox autocomplete="off" ID="txtSaveOrUpdate" runat="server" CssClass="TextBox" Width="50px" Visible="false"></asp:TextBox>
            </div>

        </div>
    </div>
     
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
