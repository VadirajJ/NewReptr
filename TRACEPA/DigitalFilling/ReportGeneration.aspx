<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ReportGeneration.aspx.vb" Inherits="TRACePA.ReportGeneration" ValidateRequest="false" %>

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
    <script type="text/javascript" src="../tinymce/jscripts/tiny_mce/tiny_mce.js"></script>
    <script type="text/javascript">
        tinyMCE.init({
            editor_deselector: "NoEditor",
            mode: "textareas",
            theme: "advanced",
            plugins: "safari,spellchecker,pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,imagemanager,filemanager",
            theme_advanced_buttons1: "save,|,bold,italic,underline,strikethrough,|,justifyleft,justifycenter,justifyright,justifyfull,|,styleselect,formatselect,fontselect,fontsizeselect",
            theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor",
            theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen",
            theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,styleprops,spellchecker,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,template,blockquote,pagebreak,|,insertfile,insertimage",
            theme_advanced_toolbar_location: "top",
            theme_advanced_toolbar_align: "left",
            theme_advanced_statusbar_location: "bottom",
            theme_advanced_resizing: false
        });
    </script>
    <div class="loader"></div>
    <div class="col-sm-12 col-md-12">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>
    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divComp">
        <div class="card">
            <div runat="server" id="divCompheader" class="card-header ">
                <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Audit Report" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnAdd" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Add New" CausesValidation="false" />
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save/Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnUpdate" CssClass="activeIcons hvr-bounce-out" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Update" ValidationGroup="Validate" />
                    <asp:ImageButton ID="imgbtnReport" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Report" ValidationGroup="Validate" />
                </div>
            </div>
        </div>
        <div class="card">
            <div id="divComplianceBody" runat="server" clientidmode="Static">
                <div class="card-body">
                    <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" id="div2" runat="server" style="border-style: none; border-color: inherit; border-width: medium; padding: 0px;">
                        <div class="col-md-12" style="padding-left: 0px">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>* Customers</label>
                                    <asp:RequiredFieldValidator ID="RFVCustomers" runat="server" ControlToValidate="ddlCustomers" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlCustomers" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>* Audit No.</label>
                                    <asp:RequiredFieldValidator ID="RFVAuditNo" runat="server" CssClass="ErrorMsgRight" ControlToValidate="ddlAuditNo" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlAuditNo" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group ">
                                    <label>* Year</label>
                                    <asp:RequiredFieldValidator ID="RFVFYear" runat="server" ControlToValidate="ddlFYear" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding-left: 0px">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>* Report Type</label>
                                    <asp:RequiredFieldValidator ID="RFVReportType" runat="server" ControlToValidate="ddlReportType" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Heading</label>
                                    <asp:DropDownList ID="ddlHeading" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12" style="padding-left: 0px">
                            <div class="col-md-12">
                                <div class="form-group ">
                                    <label>* Description</label>
                                    <%-- <asp:TextBox runat="server" CssClass="aspxcontrols" Font-Size="X-Small" TextMode="MultiLine" Height="144px" ID="txtDescription"></asp:TextBox>--%>
                                    <textarea id="txtDescription" runat="server" rows="15" cols="80" style="width: 100%; height: 300px;"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12" data-bs-parent="#sidenavAccordionPages" runat="server" style="border-style: none; border-color: inherit; border-width: medium; overflow: auto">
                            <asp:GridView ID="gvReportGeneration" CssClass="table table-bordered" runat="server" AutoGenerateColumns="False" Width="100%">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Customer" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCustomer" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Customer") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPKID" runat="Server" Visible="False" Text='<%#DataBinder.Eval(Container, "DataItem.PKID")%>'></asp:Label>
                                            <asp:Label ID="lblHeadingid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Headingid") %>'></asp:Label>
                                            <asp:Label ID="lblReportID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ReportID") %>'></asp:Label>
                                            <asp:Label ID="lblReportTypeID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ReportTypeID") %>'></asp:Label>
                                            <asp:Label ID="lblSignedby" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.Signedby") %>'></asp:Label>
                                            <asp:Label ID="lblModuleID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ModuleID") %>'></asp:Label>
                                            <asp:Label ID="lblCustomerID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.CustomerID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Report Type" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReportType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ReportType") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Heading" ItemStyle-Width="20%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHeading" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Heading") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description" ItemStyle-Width="60%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>'></asp:Label>
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
    <div id="ModelReportGenerationValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblReportGenerationValidationMsg" runat="server"></asp:Label></strong>
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
