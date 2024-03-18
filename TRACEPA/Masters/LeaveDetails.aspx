<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="LeaveDetails.aspx.vb" Inherits="TRACePA.LeaveDetails" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
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
            $('#<%=gvLeaveDetails.ClientID%>').DataTable({
                iDisplayLength: 20,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
    <div class="loader"></div>
        <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Leave Particulars</b></h2>
            </div>
            <div class="col-sm-6 col-md-6">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" />
                     <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />                  
                    <ul class="nav navbar-nav navbar-right logoutDropdown">
                        <li class="dropdown">
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" /></li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" /></li>
                            </ul>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" style="padding-left: 0px">
        <div class="col-sm-12 col-md-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding:0px">
         <div class="col-sm-3 col-md-3">
             <div class="form-group">
                 <asp:Label ID="lblUser" runat="server" Text="* Existing Employee"></asp:Label>
                 <asp:RequiredFieldValidator ID="RFVEmployee" CssClass="ErrorMsgRight" runat="server" ValidationGroup="Validate" ControlToValidate="ddlEmployee" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="aspxcontrols" AutoPostBack="True" TabIndex="1">
                 </asp:DropDownList>
             </div>
         </div>
         <div class="col-sm-3 col-md-3">
             <div class="form-group">
                 <br />
                 <asp:Label ID="lblHSapCode" runat="server" Text="Emp Code :"></asp:Label> 
                 <asp:Label ID="lblSapCode" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>                          
             </div>
         </div>
         <div class="col-sm-6 col-md-6"></div>
     </div>
    <div class="col-sm-12 col-md-12">
        <div class="form-group">
            <h4><b><asp:Label ID="lblLeaveAppled" runat="server" Text="Details of Leave Applied For"></asp:Label></b></h4>
        </div>
    </div>
    <div class="col-sm-12 col-md-12 form-group" style="padding:0px">
        <div class="col-sm-6 col-md-6 form-group" style="padding:0px">
            <div class="col-sm-12 col-md-12 form-group" style="padding:0px">
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblFrom" runat="server" Text="* From"></asp:Label>
                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtFrom"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVFrom" runat="server" ControlToValidate="txtFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVFrom" runat="server" ControlToValidate="txtFrom" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtFrom" PopupPosition="BottomLeft" TargetControlID="txtFrom" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblTo" runat="server" Text="* To"></asp:Label>
                    <asp:TextBox runat="server" placeholder="dd/MM/yyyy" CssClass="aspxcontrols" ID="txtTo"></asp:TextBox>
                    <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVTo" runat="server" ControlToValidate="txtTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVTo" runat="server" ControlToValidate="txtTo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="txtDOB" PopupPosition="BottomLeft" TargetControlID="txtTo" Format="dd/MM/yyyy" CssClass="cal_Theme1" />
                </div>
                <div class="col-sm-4 col-md-4">
                    <asp:Label ID="lblHNoofDays" runat="server" Text="* No of Days"></asp:Label>
                    <asp:TextBox ID="txtNoDays" runat="server" CssClass="aspxcontrols" data-toggle="tooltip" data-placement="bottom" title="Only numbers" onkeyup="nospaces(this)"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFVDays" runat="server" CssClass="ErrorMsgLeft" ControlToValidate="txtNoDays" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator CssClass="ErrorMsgLeft" ID="REVDays" runat="server" ControlToValidate="txtNoDays" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
                </div>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <asp:Label ID="lblPurpose" runat="server" Text="* Purpose"></asp:Label>
                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtPurpose" Height="42px"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVPurpose" runat="server" ControlToValidate="txtPurpose" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVPurpose" runat="server" ControlToValidate="txtPurpose" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            </div>
        </div>
        <div class="col-sm-6 col-md-6" style="padding:0px" id="divPermLeave" runat="server">
            <div class="col-sm-12 col-md-12 form-group">
                <br />
                <asp:Label ID="lblApp" runat="server" Text="Approved Or Not" Width="140px"></asp:Label>
                <asp:RadioButton ID="rboApproved" runat="server" CssClass="aspxradiobutton" Text="Approved" GroupName="rboApproved" Checked="true" />
                <asp:RadioButton ID="rboNotApproved" runat="server" CssClass="aspxradiobutton" Text="Not Approved" GroupName="rboApproved" />                
                <br />
            </div>
            <div class="col-sm-12 col-md-12 form-group" style="padding-top:9px">
                <asp:Label ID="lblRemarks" runat="server" Text="* Remarks"></asp:Label>
                <asp:TextBox runat="server" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtRemarks" Height="42px"></asp:TextBox>
                <asp:RequiredFieldValidator CssClass="ErrorMsgLeft" ID="RFVRemarks" runat="server" ControlToValidate="txtRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVRemarks" runat="server" ControlToValidate="txtRemarks" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RegularExpressionValidator>
            </div>
        </div>        
    </div>
    
    <div class="col-sm-12 col-md-12">
        <asp:GridView ID="gvLeaveDetails" CssClass="footable" runat="server" AutoGenerateColumns="False" Width="100%">
           <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
            <Columns>
                <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="4%" />
                <asp:TemplateField HeaderText="Purpose" ItemStyle-Width="60%">
                    <ItemTemplate>
                        <asp:Label ID="lblID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'></asp:Label>                        
                        <asp:LinkButton ID="lnkPurpose" runat="server" CommandName="Select" Font-Italic="true" Font-Bold="False" Width="180px" Text='<%# DataBinder.Eval(Container.DataItem, "LeavePurpose") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="FromDate" HeaderText="From Date" ItemStyle-Width="10%" />  
                <asp:BoundField DataField="ToDate" HeaderText="To Date" ItemStyle-Width="10%" />                 
                <asp:BoundField DataField="NoDays" HeaderText="No of Days" ItemStyle-Width="10%" />              
                <asp:TemplateField HeaderText="Status" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' Width="100%"></asp:Label>                        
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="ModalLeaveValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblLeaveValidationMsg" runat="server"></asp:Label>
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
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>


