<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/DigiOffice.Master" CodeBehind="DigitalDocument.aspx.vb" Inherits="TRACePA.DigitalDocument" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">

</script>
    <%-- <style>
        .carousel-inner > .item > img,
        .carousel-inner > .item > a > img {
            width: 100%;
            height: 50%;
        }
    </style>--%>
        <script type="text/javascript">
            function Closepopup() {
                $('#ModalBillAdjusment').modal('hide');
            }
    </script>
    <div>
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                    <li data-target="#myCarousel" data-slide-to="3"></li>
                </ol>
            </div>
        </div>
        <div class="reportDetailsMN">
            <div class="sectionTitleMn">
                <div class="col-sm-7 col-md-7 pull-left">
                    <h2><b>Documents</b></h2>
                </div>
                        <div class="col-sm-5 col-md-5 ">
                            <div class="pull-right">
                    <asp:ImageButton ID="imgbtnIndex" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Indexing" />
                    <asp:ImageButton ID="imgbtnWorkFlow" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Work Flow"  />
                                </div>
                            </div>
            </div>
        </div>
        <div class="col-sm-12 col-md-12" style="padding-right: 0px">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgRight"></asp:Label>
        </div>
        <div class="col-sm-12 col-md-12 divmargin" style="padding: 0px">          
                <h4><b>Document</b></h4>
                <asp:GridView ID="gvDocuments" runat="server" Width="100%" class="footable" AutoGenerateColumns="False">
                    <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%">
                                <HeaderTemplate>
                                <asp:CheckBox ID="chkSelectAll" AutoPostBack="True" runat="server" CssClass="aspxradiobutton hvr-bounce-in" OnCheckedChanged="chkSelectAll_CheckedChanged" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="1%" />                                    
                                <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.fileID") %>'></asp:Label>
                                <asp:Label ID="lblfilepath" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.filePath") %>'></asp:Label>
                                       <asp:Label ID="lblfilename" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fileName") %>'></asp:Label>
                                       <asp:Label ID="lblfileInfo" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fileInfo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                           <asp:BoundField DataField="fileID" HeaderText="Sr No" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="5%"></asp:BoundField>
                        <asp:BoundField DataField="fileName" HeaderText="Document Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="40%"></asp:BoundField>
                        <asp:BoundField DataField="fileInfo" HeaderText="Created On" HeaderStyle-HorizontalAlign="Left" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-Width="45%"></asp:BoundField>
                        <asp:TemplateField HeaderStyle-Width="5%" FooterStyle-VerticalAlign="Middle">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnView" CssClass="hvr-bounce-in" ImageAlign="Middle" CommandName="View"  runat="server" ToolTip="View" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-Width="5%">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnDelete" CssClass="hvr-bounce-in"  ImageAlign="Middle" CommandName="Delete" runat="server" ToolTip="Delete" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>         
        </div>


        <div id="myModalDocumentViewer" class="modal fade" data-backdrop="static" data-keyboard="false" role="dialog">
            <div class="modal-lg" style="margin-left: 18%; margin-top: 4%;">
                <div class="modal-content">
                                     
                    <div class="modal-header">
                          <button type="button" class="close" style="width:10px; height:8px;" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><b>Document Viewer</b> </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row ">
                            <div class="col-sm-12 col-md-12 pull-left">
                                <asp:Label ID="lblMsg" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
                            </div>                     
                            <div class="col-sm-12 col-md-12">
                                <asp:Panel ID="pnlImgViewer" runat="server" Visible="false">
                                    <asp:Image ID="documentImgViewer" runat="server" Width="850px" Height="400px" Style="border-radius: 10px; vertical-align: middle" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-12">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
          <div id="ModalDeletionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>Image VIewer</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>                            <strong>
                                <asp:Label ID="lblImgdeletionValidationMsg" runat="server"></asp:Label></strong>
                        </p>                    </div>                </div>
                <div class="modalmsg-footer">
                    <asp:Button runat="server" class="btn-OK" ID="BtnYES" CssClass="aspxcontrols" autopostback="true" Text="YES"></asp:Button>
                    <asp:Button runat="server" class="btn-OK" CssClass="aspxcontrols" ID="BtnNo" Text="NO"></asp:Button>
                </div>
            </div>
        </div>
    </div>
          <div id="ModalMSGValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>Document Viewer</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType1" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblValidationMsg" runat="server"></asp:Label>
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
    </div>

</asp:Content>
