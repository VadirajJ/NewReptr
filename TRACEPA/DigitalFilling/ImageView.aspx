<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ImageView.aspx.vb" Inherits="TRACePA.ImageView" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link href="../StyleSheet/font-awesome.css" rel="stylesheet" />
    <link href="../StyleSheet/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="../StyleSheet/sweetalert.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/sweetalert-dev.js"></script>
    <script src="../JavaScripts/wPaint.menu.main.js"></script>
    


    <%--  <script>  '''Dk
          var div = document.getElementById("documentViewer1");
          div.onload = function () {
              div.style.height =
                  div.contentWindow.document.body.scrollHeight + 'px';
          }
      </script>--%>
<%--    <script type='text/javascript'>  '''Dk
        var myPDF = new PDFObject({
            url: 'ConferenceGuide.pdf',
            pdfOpenParams: {
                view: 'Fit',
                scrollbars: '0',
                toolbar: '0',
                statusbar: '0',
                navpanes: '0'
            }
        }).embed('pdf1');
    </script>--%>
<%--    <style>
#documentViewer1 {

    border: 0;
    -ms-transform: scale(0.25);
    -moz-transform: scale(0.25);
    -o-transform: scale(0.25);
    -webkit-transform: scale(0.25);
    transform: scale(0.25);

    -ms-transform-origin: 0 0;
    -moz-transform-origin: 0 0;
    -o-transform-origin: 0 0;
    -webkit-transform-origin: 0 0;
    transform-origin: 0 0;
}
        </style>--%>
    <script lang="javascript" type="text/javascript">      

        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $(document).ready(function ($) {
            $.fn.wPaint.menus.main = {
                pencil: {
                    index: 21,
                    callback: (function () {
                        this.setMode('pencil');
                    }
                )
                }
            }

            $.fn.wPaint.extend({
                undoCurrent: -1,
                undoArray: [],
                setUndoFlag: true,

                _drawPencilDown: (function (e) {
                    this.ctx.lineJoin = 'round';
                    this.ctx.lineCap = 'round';
                    this.ctx.strokeStyle = this.options.strokeStyle;
                    this.ctx.fillStyle = this.options.strokeStyle;
                    this.ctx.lineWidth = this.options.lineWidth;

                    //draw single dot in case of a click without a move
                    this.ctx.beginPath();
                    this.ctx.arc(e.pageX, e.pageY, this.options.lineWidth / 2, 0, Math.PI * 2, true);
                    this.ctx.closePath();
                    this.ctx.fill();

                    //start the path for a drag
                    this.ctx.beginPath();
                    this.ctx.moveTo(e.pageX, e.pageY);
                }),

                _drawPencilMove: (function (e) {
                    this.ctx.lineTo(e.pageX, e.pageY);
                    this.ctx.stroke();
                }),

                _drawPencilUp: (function () {
                    this.ctx.closePath();
                    this._addUndo();
                })
            });
        })(jQuery);
    </script>
   
    <script type="text/javascript">
        function documentViewerDownloading(sender, e) {
            var documentViewer = sender;

            logEvent(e);
        }

        function documentViewerPrinted(sender, e) {
            var documentViewer = sender;

            logEvent(e);
        }

       <%-- function logEvent(e) {
           document.getElementById("<%=btnDownloadAndPrint.ClientID %>").click();
        }--%>
      
    </script>
    <script type="text/javascript">
        document.onmousedown = disableRightclick;  
        function disableRightclick() {          
                return false;        
        }
    </script>

    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-3 col-md-3">
                <h2><b>Document Viewer</b></h2>
            </div>
            <div class="col-sm-8 col-md-8">
                <asp:ImageButton ID="imgbtnNavDocFastRewind" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNavDoc" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Previous" Style="margin-right: 10px;" CausesValidation="false" />
                <asp:TextBox ID="txtNavDoc" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNavDoc" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNavDoc" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnNavDocFastForword" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                |
                <asp:ImageButton ID="imgbtnFastRewind" CssClass="activeIcons hvr-bounce-in" runat="server" Style="margin-left: 10px;" data-toggle="tooltip" data-placement="bottom" title="Backword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnPreviousNav" CssClass="hvr-bounce-in" runat="server" data-toggle="tooltip" Style="margin-right: 10px;" data-placement="bottom" title="Previous" CausesValidation="false" />
                <asp:TextBox ID="txtNav" runat="server" Enabled="false" Width="50px" CssClass="aspxcontrols"></asp:TextBox>
                <asp:Label ID="lblNav" runat="server" Width="30px" CssClass="aspxlabelbold"></asp:Label>
                <asp:ImageButton ID="imgbtnNextNav" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Next" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnFastForword" CssClass="activeIcons hvr-bounce-in" runat="server" data-toggle="tooltip" data-placement="bottom" title="Forword" CausesValidation="false" />
                <asp:ImageButton ID="imgbtnAnnotation" CssClass="activeIcons hvr-bounce-in" Visible="false" runat="server" data-toggle="tooltip" data-placement="bottom" title="Annotation" CausesValidation="false" />
                <asp:Label ID="lblHVersion" runat="server" Visible="false" Text="Version:"></asp:Label>
                <asp:DropDownList ID="ddlAnnotationVersion" runat="server" Visible="false" CssClass="aspxcontrols" Width="250px" AutoPostBack="true"></asp:DropDownList>
            </div>
            <div class="col-sm-1 col-md-1">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnAdd" Visible="false" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom"
                        ValidationGroup="ValidateCabinet" title="Add Files" />
                    <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" CausesValidation="false" />
                   <div class="pull-right" id="btnOverlap" style="position:relative">
                        <asp:Button ID="btnDownloadAndPrint" runat="server" OnClick="DownloadAndPrint" BorderStyle="None" BackColor="White"/>
                        <asp:Button runat="server" ID="btnover" style="position:absolute; right:1px" Enabled="false" BackColor="White" BorderStyle="None" Width="20px"/>
                    </div>  
                </div>

            </div>
        </div>
    </div>

    <div class="col-sm-12 col-md-12 form-group" style="padding: 0px">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft" Visible="false"></asp:Label>
        <asp:TextBox ID="txtDetID" runat="server" Style="display: none;"></asp:TextBox>
    </div>

    <div class="col-sm-2 col-md-2" style="padding: 0px">
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px;">
            <asp:Label ID="lblCabinet" runat="server" Text="Cabinet"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVCabinet" runat="server" SetFocusOnError="True" ControlToValidate="ddlCabinet" Display="Dynamic" ValidationGroup="ValidateCabinet"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlCabinet" runat="server" AutoPostBack="true" Enabled="false" CssClass="aspxcontrols" />
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px;">
            <asp:Label ID="lblSubcabinet" runat="server" Text="Sub Cabinet"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVSubCabinet" runat="server" SetFocusOnError="True" ControlToValidate="ddlCabinet" Display="Dynamic" ValidationGroup="ValidateCabinet"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlSubCabinet" runat="server" AutoPostBack="true" Enabled="false" CssClass="aspxcontrols" />
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px;">
            <asp:Label ID="lblFolder" runat="server" Text="Folder"></asp:Label>
            <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVFolder" runat="server" SetFocusOnError="True" ControlToValidate="ddlCabinet" Display="Dynamic" ValidationGroup="ValidateCabinet"></asp:RequiredFieldValidator>
            <asp:DropDownList ID="ddlFolder" runat="server" AutoPostBack="true" Enabled="false" CssClass="aspxcontrols" />
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px;">
            <asp:Label Visible="false" ID="lblDocumentTypeId" runat="server" Text="Document Type"></asp:Label>
             <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVDocumentType" runat="server" SetFocusOnError="True" ControlToValidate="ddlDocumentTypeId" Display="Dynamic" ValidationGroup="ValidateCabinet"></asp:RequiredFieldValidator>
            <asp:DropDownList Visible="false" ID="ddlDocumentTypeId" runat="server" AutoPostBack="true" CssClass="aspxcontrols" />
        </div>

        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px;">
            <div class="col-sm-6 col-md-6" style="padding: 0px">
                <asp:CheckBox ID="Checkin" runat="server" Text="Check In" Visible="false" AutoPostBack="true" />
            </div>
            <div class="col-sm-6 col-md-6" style="padding: 0px;">
                <asp:LinkButton ID="OpenDocument" runat="server" Visible="false" Text="Open Document" AutoPostBack="true" />
            </div>
        </div>
        <div class="col-sm-12 col-md-12 form-group" style="padding: 0px; max-height: 300px; overflow: auto;">
            <asp:GridView ID="gvVersionInfo" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <Columns>
                    <asp:TemplateField HeaderText="Remove">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgRemove" ImageUrl="~/Images/Trash16.png" CssClass="centerButton" CommandName="RemoveRow" runat="server" ToolTip="Remove" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Version Info">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkVersionInfo" runat="server" CssClass="centerButton" CommandName="Version" Text='<%# DataBinder.Eval(Container, "DataItem.VRS_VersionName") %>' />
                             <asp:Label ID="lblVersionID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.VRS_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="col-sm-8 col-md-8" style="padding: 0px; height:100%;">
        <div runat="server" id="divSearch" class="col-sm-12 col-md-12" style="height:1000px;">
          <%--  <asp:Image ID="documentImgViewer" runat="server"  Width="850px" style="border-radius:10px" Height="530px" AlternateText="Logo" />--%>
             <%--<asp:Image ID="Image2" runat="server"   ImageUrl="http://localhost/MMCS/TRACEPA/TRACEPA/TempImage/1_12.png"  />--%>
           <%-- <asp:Image ID="documentImgViewer" runat="server" Width="810px" Height="930px" style="border-radius:10px;vertical-align:middle"    />--%>

             <asp:Image ID="documentImgViewer" runat="server" Width="90%" Height="90%" style="border-radius:10px;vertical-align:middle;"     />
            <%--<iframe id ="documentiFrameViewer" runat="server" oncontextmenu="fncsave" style="vertical-align:middle; position: absolute; overflow:no-display; border-radius:10px;height:530px;width:810px;Height:930px;object-fit:fill;   background-attachment: fixed;" name="iframe1" ></iframe>--%>

            <iframe id ="documentiFrameViewer" runat="server" oncontextmenu="fncsave" style="vertical-align:middle; position: absolute; overflow:no-display; border-radius:10px;Width:90%; Height:90%;object-fit:fill;   background-attachment: fixed;" name="iframe1" ></iframe>

            
           
          <%--  <GleamTech:DocumentViewerControl ID="documentViewer" runat="server" Height="500"
                Resizable="False">--%>

            <%--   <Watermarks>
                    <GleamTech:TextWatermark
                        Text="MMCSPL | MMCSPL  MMCSPL | MMCSPL | MMCSPL | MMCSPL" 
                        HorizontalAlignment="center"
                        VerticalAlignment="center"
                        Rotation="0"
                        Opacity="50"
                        FontColor="Red"
                        Width="100"
                        Height="2"
                        SizeIsPercentage="True" />

                    <GleamTech:ImageWatermark
                        ImageFile="~/Images/WaterMark/zoo.png"
                        HorizontalAlignment="center"
                        VerticalAlignment="center"
                        Opacity="50"
                        PageRange="all" />
                </Watermarks>--%>
           <%-- </GleamTech:DocumentViewerControl>--%>
        </div>
    </div>


    <div class="col-sm-2 col-md-2" style="padding: 0px">
        <asp:Panel ID="pnlDocView" runat="server" CssClass="col-sm-12 col-md-12" Style="padding: 0px">
            <div class="col-sm-12 col-md-12 form-group">
                <asp:Label ID="lblDoucmentType" runat="server" Font-Bold="true" CssClass="aspxlabelbold" Visible="false"></asp:Label>
            </div>
            <div class="col-sm-12 col-md-12 form-group">
                <div class="form-group">
                    <asp:Label ID="lblHFileName" runat="server" Text="File Name :"></asp:Label>
                    <asp:Label ID="lblFileName" runat="server" Font-Bold="true" CssClass="aspxlabelbold" Visible="false"></asp:Label>
                    <%--<asp:LinkButton ID="lnkOpenDocument" Font-Bold="true" runat="server" Font-Italic="true" Visible="false"></asp:LinkButton>--%>
                    <asp:Label ID="lblOpenDocument" Font-Bold="true" ForeColor="Black" runat="server" Font-Italic="true" Visible="false"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblFileTypeH" runat="server" Text="File Type :"></asp:Label>
                    <asp:Label ID="lblFileType" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCreatedByH" runat="server" Text="Created By :"></asp:Label>
                    <asp:Label ID="lblCreatedBy" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblCreatedOnH" runat="server" Text="Created On :"></asp:Label>
                    <asp:Label ID="lblCreatedOn" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblSizeH" runat="server" Text="Size :"></asp:Label>
                    <asp:Label ID="lblSize" runat="server" Font-Bold="true" CssClass="aspxlabelbold"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 col-md-12">
                <asp:DataGrid ID="dgIndex" runat="server" AutoGenerateColumns="False" Width="100%" class="footable" PageSize="5000">
                    <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                    <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="">
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblHDescriptor" runat="server" Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Label ID="lblDescriptor" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Descriptor") %>' Width="100%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="100%" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </asp:Panel>
    </div>
    <div id="ModalVersion" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"><b>Version Details</b></h4>
                </div>
                <div class="modal-body row">
                    <div class="col-sm-12 col-md-12">
                        <div class="pull-left">
                            <asp:Label ID="VersionError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:FileUpload ID="FileUpload1" runat="server" Width="95%" CssClass="btn-ok" AllowMultiple="true" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <strong>
                                <asp:Label ID="lblVFileName" runat="server" Text="File Name"></asp:Label></strong>
                            <strong>
                                <asp:TextBox ID="txtVFileName" runat="server" CssClass="aspxcontrols"></asp:TextBox></strong>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <strong>
                                <asp:Label ID="lblVRevisedBy" runat="server" Text="Revised By"></asp:Label></strong>
                            <strong>
                                <asp:TextBox ID="txtVRevisedBy" runat="server" CssClass="aspxcontrols"></asp:TextBox></strong>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <strong>
                                    <asp:Label ID="lblVRevisedOn" runat="server" Text="Revised On"></asp:Label></strong>
                                <strong>
                                    <asp:TextBox ID="txtVRevisedOn" runat="server" CssClass="aspxcontrols"></asp:TextBox></strong>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <strong>
                                    <asp:CheckBox ID="CBVNewVersionInfo" runat="server" Text="New Version Info" AutoPostBack="true" /></strong>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-12">
                        <div class="form-group">
                            <strong>
                                <asp:Label ID="lblVRemarks" runat="server" Text="Remarks"></asp:Label></strong>
                            <strong>
                                <asp:TextBox ID="txtVRemarks" runat="server" CssClass="aspxcontrols"></asp:TextBox></strong>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--<button data-dismiss="modal" runat="server" class="btn-ok" id="btnSaveVersion">--%>
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="btnSaveVersion">Save</button>
                    <button data-dismiss="modal" runat="server" class="btn-ok" id="btnCancelVersion">
                        Cancel</button>
                </div>
            </div>
        </div>
    </div>
    <div id="ModalAddImage" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title"><b>Document Indexing</b></h4>
                </div>
                <div class="modal-body row">

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-4 col-md-4">
                            <div class="form-group">
                                <asp:FileUpload ID="txtfile" runat="server" Width="90%" CssClass="btn-ok" AllowMultiple="true" />
                            </div>
                        </div>
                        <div class="col-sm-1 col-md-1">
                            <asp:Button ID="btnAddAttch" runat="server" Text="Add" CssClass="btn-ok" />
                        </div>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-5 col-md-5">
                            <asp:GridView ID="gvattach" runat="server" AutoGenerateColumns="False" Width="100%" class="footable">
                                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                                <Columns>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="1%">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                                            <%--                       <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" />--%>
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
                        <div class="col-sm-7 col-md-7">
                            <asp:Panel ID="Panel1" runat="server" Visible="false">
                                <asp:LinkButton ID="lnkImage" runat="server" ForeColor="SlateBlue" Font-Bold="true"></asp:LinkButton>
                                <asp:Label ID="lblSelectPath" runat="server" Visible="false"></asp:Label>
                                <asp:Image ID="Image1" runat="server" Height="400px" Width="600px" />
                            </asp:Panel>
                        </div>
                    </div>

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
                                        <asp:Label ID="lblMcabinet" runat="server" Text="* Cabinet"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVMcabinet" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlMCabinet" ValidationGroup="MSave"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlMcabinet" runat="server" AutoPostBack="True" CssClass="aspxcontrols" Enabled="false"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblMSubcabinet" runat="server" Text="* Sub cabinet"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVMSubCabinet" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlMSubcabinet" ValidationGroup="MSave"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlMSubcabinet" AutoPostBack="True" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-6 col-md-6">

                                    <div class="form-group">
                                        <asp:Label ID="lblMFolder" runat="server" Text="* Folder"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVMFolder" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlMFolder" ValidationGroup="MSave"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlMFolder" AutoPostBack="True" runat="server" CssClass="aspxcontrols" Enabled="false"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="lblMTitle" runat="server" Text="* Title"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RFVTitle" CssClass="ErrorMsgRight" runat="server" ControlToValidate="txtTitle" ValidationGroup="MSave"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtTitle" runat="server" CssClass="aspxcontrols"></asp:TextBox>
                                        <asp:RegularExpressionValidator CssClass="ErrorMsgRight" ID="REVTitle" runat="server" ControlToValidate="txtTitle" Display="Dynamic" SetFocusOnError="True" ValidationGroup="MSave"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlMCabinet" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMFolder" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-sm-12 col-md-12" style="padding: 0px">
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <asp:Label ID="lblDocumentType" runat="server" Text="* Document Type"></asp:Label>
                                <asp:RequiredFieldValidator ID="RFVType" CssClass="ErrorMsgRight" runat="server" ControlToValidate="ddlType" ValidationGroup="MSave"></asp:RequiredFieldValidator>
                                <asp:DropDownList ID="ddlType" AutoPostBack="True" runat="server" CssClass="aspxcontrols"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="form-group">
                                <br />
                                <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                <asp:Label ID="lblDateDisplay" runat="server" CssClass="aspxlabelbold"></asp:Label>
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
                    <asp:ImageButton ID="imgbtnIndexSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" ValidationGroup="MSave" data-placement="bottom" title="Index" />
                </div>
            </div>
        </div>
    </div>
    <asp:TextBox ID="txtID" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtPreId" runat="server" Visible="false"></asp:TextBox>
    <asp:ListBox ID="lstDocument" runat="server" Visible="false"></asp:ListBox>
    <asp:ListBox ID="lstFiles" runat="server" Visible="false"></asp:ListBox>
    <asp:Label ID="lblDocID" runat="server" Visible="false"></asp:Label>
    <asp:Label ID="lblFileID" runat="server" Visible="false"></asp:Label>
    

    <div id="ModalSearchImageViewValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblSearchImageViewValidationMsg" runat="server"></asp:Label></strong>
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
    <div id="ModalRemoveValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgLinkType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblRemoveValidationMsg" runat="server"></asp:Label></strong>
                        </p>
                    </div>
                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" Text="YES" class="btn-ok" ID="btnYesMsgOk" OnClick="btnYesMsgOk_Click"></asp:Button>
                    <asp:Button runat="server" Text="NO" class="btn-ok" ID="btnNo" OnClick="btnNo_Click"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    
    <script>
        window.onbeforeunload = function (evt) {

            $.ajax({
                type: "POST",
                url: "ImageView.aspx/zxa",
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

