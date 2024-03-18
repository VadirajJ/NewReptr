<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.master" CodeBehind="Indexing.aspx.vb" Inherits="TRACePA.Indexing" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <%--   <style>
        .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }
    </style>--%>

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

    <div class="col-sm-12 col-md-12 divmargin">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
    </div>


    <div class="col-sm-12 col-md-12 divmargin" runat="server" id="divAssignment">
        <div class="card">
            <div runat="server" id="divAssignmentheader" class="card-header">
                <i class="fa-regular fa-address-book" style="font-size: large"></i>&nbsp;
               
                    <asp:Label runat="server" ID="lblpendingtaskcount" CssClass="form-label" Font-Bold="true" Text="Document Indexing" Font-Size="Small"></asp:Label>
                <div class="pull-right" style="padding-right: 15px;">
                    <asp:ImageButton ID="imgbtnIndex" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Index File" />
                </div>
            </div>
            </div>
        <div class="card">
            <br />
            <div class="col-sm-12 col-md-12">
                <div class="col-sm-5 col-md-5" style="padding: 0px">
                    <div class="form-group">
                        <div class="col-sm-10 col-md-10" style="padding: 0px">
                            <div class="form-group">
                                <asp:FileUpload ID="txtfile" runat="server" Width="90%" CssClass="btn-ok" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2" style="padding: 0px">
                            <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:GridView ID="gvattach" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                           <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />
                                        <asp:Label ID="lblID" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Path" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPath" runat="server" Visible="True" Text='<%# DataBinder.Eval(Container, "DataItem.FilePath") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File Name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkFileName" runat="server" CommandName="View" Text='<%# DataBinder.Eval(Container, "DataItem.FileName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>

                <%-- <div class="col-sm-7 col-md-7">
            <GleamTech:DocumentViewer ID="documentViewer" Visible="false" runat="server" Width="600" Height="480"
                Resizable="False" /> 
        </div>--%>
            </div>
            <div class="col-sm-12 col-md-12" style="padding: 0px">
                <div class="col-sm-7 col-md-7">
                    <%--  <div class="pull-left">
                <asp:ImageButton ID="imgbtnWidth" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Width" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnHeight" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Height" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnFitScreen" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Fit Screen" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnZoomOut" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Zoom Out" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnZoomIn" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Zoom In" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnRotate90" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rotate 90°" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnRotate180" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rotate 180°" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnRotate270" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Rotate 270°" CausesValidation="false" />
            </div>--%>
                    <%--<asp:Panel ID="pnlImageView" runat="server" Visible="false">
                <asp:LinkButton ID="lnkImage" runat="server" ForeColor="SlateBlue" Font-Bold="true"></asp:LinkButton>
                <asp:Label ID="lblSelectPath" runat="server" visible="false"></asp:Label>
                <asp:Image ID="RetrieveImage" runat="server" Height="400px" Width="600px" />
            </asp:Panel>--%>

                    <%--<GleamTech:ExampleFileSelector ID="exampleFileSelector" runat="server"
                InitialFile="Default.pdf" />--%>
                </div>
            </div>
        </div>
    </div>


    <div id="ModalValidation" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label></strong>
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

    <div id="myModalIndex" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">

                    <h4 class="modal-title"><b>Index Details</b></h4>
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label ID="lblDate" runat="server" Text="Date" Visible="false"></asp:Label>
                    <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold" Visible="false"></asp:Label>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="col-sm-12 col-md-12">
                                    <div class="pull-left">
                                        <asp:Label ID="lblModelError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblcabinet" runat="server" Text="* Cabinet"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVcabinet" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlCabinet" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblSubcabinet" runat="server" Text="* Sub cabinet"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVSubCabinet" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlSubcabinet" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="lblFolder" runat="server" Text="* Folder"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVFolder" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlFolder" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblTitle" runat="server" Text="* Title"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVTitle" CssClass="ErrorMsgRight" runat="server" ControlToValidate="txtTitle" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTitle" runat="server" ControlToValidate="txtTitle" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlCabinet" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlFolder" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="* Document Type"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVType" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlType" ValidationGroup="Save"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblRFID" Visible="false" runat="server" Text="RFID"></asp:Label>
                                <asp:TextBox ID="txtRFID" Visible="false" runat="server" CssClass="aspxcontrols"></asp:TextBox>
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
                                            <asp:Label ID="lblMandatory" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.Mandatory") %>' />
                                            <asp:Label ID="lblValidator" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.Validator") %>' />
                                            <asp:Label ID="lblSize" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.Size") %>' />
                                            <asp:Label ID="lblDataType" runat="server" CssClass="hvr-bounce-in" Visible="False" Text='<%# DataBinder.Eval(Container, "DataItem.DataType") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="40%" HeaderText="Descriptor">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDescriptor" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descriptor") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtValues" runat="server" CssClass="aspxcontrols" autocomplete="off" Width="80%" Text='<%# DataBinder.Eval(Container, "DataItem.Values") %>' />
                                            <asp:Panel ID="pnlCalendar" runat="server">
                                                <cc1:CalendarExtender ID="cclValues" runat="server" PopupButtonID="imgValues"
                                                    TargetControlID="txtValues" Format="dd/MM/yyyy" PopupPosition="TopLeft">
                                                </cc1:CalendarExtender>
                                                <asp:ImageButton ID="imgValues" runat="server" Height="15px" ImageUrl="~/Images/Calendar.gif" Width="15px" />
                                            </asp:Panel>
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
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" ValidationGroup="Save" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div>
    <script>
        window.onbeforeunload = function (evt) {

            $.ajax({
                type: "POST",
                url: "Indexing.aspx/zxa",
                data: "{ firstNumber: '" + parseInt(1) + "',secondNumber: '" + parseInt(2) + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: "true",
                cache: "false",
                success: onSucceed,
                Error: onError
            });
        }
        // On Success
        function onSucceed(results, currentContext, methodName) {
            if (results !== null && results.d !== null) {
                document.getElementById('lblError').innerHTML = results.d;
            }
        }
        // On Errors
        function onError(results, currentContext, methodName) {
            document.getElementById('lblError').innerHTML = results.d;
            console.log(results);
        }
    </script>
</asp:Content>

