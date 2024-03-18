<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportContentMaster.aspx.vb" Inherits="TRACePA.ReportContentMaster" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa fa-pencil-square" style="font-size: large"></i>&nbsp;
                 
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Report Content Master" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Reresh24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" Visible="false" ImageUrl="~/Images/Save24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                    <%--     <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Update24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />--%>
                            <a href="#" data-toggle="dropdown" style="padding: 0px;"><span>
                                <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Report" /></span></a>
                            <ul class="dropdown-menu">
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnPDF" Text="Download PDF" Style="margin: 0px;" />
                                </li>
                                <li role="separator" class="divider"></li>
                                <li>
                                    <asp:LinkButton runat="server" ID="lnkbtnExcel" Text="Download Excel" Style="margin: 0px;" />
                                </li>
                            </ul>
                </div>
            </div>
            </div>
        <div class="card">
            <div class="col-md-12">
                <div class="col-md-6" style="padding-left: 0px">
                    <div class="form-group ">
                        <asp:Label ID="lblReportType" runat="server" Text="* Report Type"></asp:Label>
                        <%--    <asp:RequiredFieldValidator ID="RFVFunction" runat="server" ControlToValidate="ddlReportType" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                        <asp:DropDownList ID="ddlReportType" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-5" style="padding-left: 0px">
                    <div class="form-group ">
                        <asp:Label ID="lblHeading" runat="server" Text="* Heading"></asp:Label>
                        <%--                <asp:RequiredFieldValidator ID="RFVHeading" runat="server" ControlToValidate="txtEnterHeading" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVHeading" runat="server" SetFocusOnError="True" Display="Dynamic" CssClass="ErrorMsgRight" ControlToValidate="txtEnterHeading" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                        <asp:TextBox runat="server" CssClass="aspxcontrols" TextMode="MultiLine" ID="txtEnterHeading"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="col-md-12" style="padding-left: 0px">
                <%-- <div class="col-md-6" style="padding-left: 0px">
            <%--<div class="form-group ">
                <asp:Label ID="lblHeading" runat="server" Text="* Heading"></asp:Label>
<%--                <asp:RequiredFieldValidator ID="RFVHeading" runat="server" ControlToValidate="txtEnterHeading" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVHeading" runat="server" SetFocusOnError="True" Display="Dynamic" CssClass="ErrorMsgRight" ControlToValidate="txtEnterHeading" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                <%--    <asp:TextBox runat="server" CssClass="aspxcontrols" TextMode="MultiLine" Height="100px" ID="txtEnterHeading"></asp:TextBox>
            </div>--%>
                <%-- </div>--%>
                <div class="col-md-9" style="padding-right: 0px">
                    <div class="form-group">
                        <asp:Label ID="lblDescription" runat="server" Text="* Description"></asp:Label>
                        <%--  <asp:RequiredFieldValidator ID="RFVDescription" runat="server" ControlToValidate="txtEnterDescription" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="REVDescription" runat="server" SetFocusOnError="True" Display="Dynamic" CssClass="ErrorMsgRight" ControlToValidate="txtEnterDescription" ValidationGroup="Validate"></asp:RegularExpressionValidator>--%>
                        <asp:TextBox runat="server" Height="300px" TextMode="MultiLine" CssClass="aspxcontrols" ID="txtEnterDescription"></asp:TextBox>
                    </div>
                </div>

                <div class="col-sm-1 col-md-1">
                    <div class="form-group">
                        <br />
                        <asp:ImageButton ID="ImgBtnAddDetails" CssClass="activeIcons hvr-bounce-out" ImageUrl="~/Images/Add24.png" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add Details" />
                    </div>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:GridView ID="gvReportContentMaster" CssClass="table bs" runat="server" AutoGenerateColumns="False" Width="100%">
                   <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:BoundField DataField="SrNo" HeaderText="Sr.No" ItemStyle-Width="2%" />

                        <asp:TemplateField HeaderText="Report Type" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblReportType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReportType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Heading" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblPKID" runat="Server" Visible="False" Text='<%#DataBinder.Eval(Container, "DataItem.PKID")%>'></asp:Label>
                                <asp:LinkButton ID="lblHeading" runat="server" CommandName="Select" Text='<%# DataBinder.Eval(Container, "DataItem.Heading") %>'></asp:LinkButton>
                                <asp:Label ID="lblReportID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ReportID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" ItemStyle-Width="31%">
                            <ItemTemplate>
                                <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>








    <div id="ModalReportValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>

                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblReportValidationMsg" runat="server"></asp:Label></strong>
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
