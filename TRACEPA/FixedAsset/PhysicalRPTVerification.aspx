<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="PhysicalRPTVerification.aspx.vb" Inherits="TRACePA.PhysicalRPTVerification" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <style>
        /*  div{
            color:black;
               }*/    
      </style>
 <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/bootstrap-multiselect.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/bootstrap-multiselect.js"></script>

      <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-8 col-md-8 ">
                <h2><b>Physical Verification Report</b></h2>
            </div>     
            <div class="col-sm-3 col-md-3 pull-right">
                <div class="pull-right">                                                                                                 
                  <asp:ImageButton ID="imgbtnRefresh" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Refresh" CausesValidation="false" />
                 <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>                    
                </div>
            </div>
        </div>
    </div>
       <div class="col-sm-12 col-md-12" style="padding-left: 0px">        
       <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
    </div>
     <div class="clearfix divmargin"></div>

     <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
          <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Branch"></asp:Label>
            <asp:DropDownList ID="ddlAccBrnch" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccBrnch" runat="server" ControlToValidate="ddlAccBrnch" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>  
         <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Asset Type"></asp:Label>
            <asp:DropDownList ID="drpAstype" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVdrpAstype" runat="server" ControlToValidate="drpAstype" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div> 
         <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Area"></asp:Label>
            <asp:DropDownList ID="ddlAccArea" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccArea" runat="server" ControlToValidate="ddlAccArea" Display="Dynamic"  ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div> 
           <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Region"></asp:Label>
            <asp:DropDownList ID="ddlAccRgn" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccRgn" runat="server" ControlToValidate="ddlAccRgn" Display="Dynamic"  ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>               
        <div class="col-sm-2 col-md-2">
            <asp:Label runat="server" Text="Zone"></asp:Label>
            <asp:DropDownList ID="ddlAccZone" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
            </asp:DropDownList>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVAccZone" runat="server" ControlToValidate="ddlAccZone" Display="Dynamic" ValidationGroup="Validate"></asp:RequiredFieldValidator>
        </div>                       
    </div> 
   
     <div class="col-sm-12 col-md-12" style="padding: 0px; overflow: auto"">
            <asp:GridView ID="GvExcelView" runat="server" CssClass="footable" AutoGenerateColumns="False">
              <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                <Columns> 
                      <asp:TemplateField visible="false" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="2%">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" visible="false" runat="server" CssClass="hvr-bounce-in" />
                    </ItemTemplate>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkSelectAll" visible="false" runat="server" AutoPostBack="true" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged1" />
                    </HeaderTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slno">
                        <ItemTemplate>
                            <asp:Label ID="lblSrNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Slno") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AssetTransfer">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetTransfer" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetTransfer") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                      
                      <asp:TemplateField HeaderText="Actual Location">
                        <ItemTemplate>
                            <asp:Label ID="lblActualLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ActualLocation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                
                      <asp:TemplateField HeaderText="Addition Reason">
                        <ItemTemplate>
                            <asp:Label ID="lblTransactionType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Supplier Name">
                        <ItemTemplate>
                            <asp:Label ID="lblSupplierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SupplierName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                     <asp:TemplateField HeaderText="Asset Type">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                       <asp:TemplateField HeaderText="Asset No">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                                             
                     <asp:TemplateField HeaderText="Asset RefNo">
                        <ItemTemplate>
                            <asp:Label ID="lblAssetRefNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AssetRefNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                                    
                     <asp:TemplateField HeaderText="Date of Purchase">
                        <ItemTemplate>
                            <asp:Label ID="lblDateofPurchase" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DateofPurchase") %>'></asp:Label>                         
                        </ItemTemplate>
                    </asp:TemplateField>                                
                      <asp:TemplateField HeaderText="Basic Value">
                        <ItemTemplate>
                            <asp:Label ID="lblAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Depreciation">
                        <ItemTemplate>
                            <asp:Label ID="lblDeprcn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Depreciation") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                                            
                  </Columns>
            </asp:GridView>
        </div>
     <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0; padding-right: 0">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
    </div>

     <div id="ModalFASFXDOpExcelView" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>FAS</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblFXOPBalExcelViewMsg" runat="server"></asp:Label>
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
</asp:Content>

