<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ModulePermission.aspx.vb" Inherits="TRACePA.ModulePermission" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
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
            $('#<%=ddlModules.ClientID%>').select2();
            $('#<%=ddlPermission.ClientID%>').select2();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
    </script>
      <div class="loader"></div>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-5 col-md-5 pull-left">
                <h2><b>Form Level Permission</b></h2>
            </div>
            <div class="col-sm-7 col-md-7">
                <div class="pull-right">
                    <asp:ImageButton ID="imgbtnSave" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Save" ValidationGroup="Validate" />
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="form-group divmargin">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding: 0px">
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblModule" runat="server" Text="Module"></asp:Label>

                <asp:DropDownList ID="ddlModules" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <div class="form-group">
                <br />
                <asp:Label ID="lblPermissionType" runat="server" Text="Permission Type:- " Width="110px"></asp:Label>
                <asp:RadioButton ID="rboRole" Text="Role Based" GroupName="Select" Checked="true" runat="server" AutoPostBack="true" Width="90px" />
                <asp:RadioButton ID="rboUser" Text="User Based" GroupName="Select" runat="server" Width="90px" AutoPostBack="true" />
            </div>
        </div>
        <div class="col-sm-4 col-md-4" style="padding-left: 0px">
            <div class="form-group">
                <asp:Label ID="lblName" runat="server" Text="* Role"></asp:Label>
                <asp:RequiredFieldValidator CssClass="ErrorMsgRight" ID="RFVRole" runat="server" ControlToValidate="ddlPermission" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlPermission" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="col-sm-12 col-md-12" style="padding-left: 0px">
        <div class="form-group">
            <asp:DataGrid ID="dgPermission" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" class="footable" onrowdatabound="PickColor_RowDataBound">
                <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
                <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="ID" Visible="false">
                        <ItemStyle HorizontalAlign="Left" Font-Italic="False" Font-Strikeout="False" Font-Underline="False"
                            Font-Overline="False" Font-Bold="False" VerticalAlign="Top"></ItemStyle>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="SLNo">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Module" HeaderText="Module">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="35%" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <ItemTemplate>
                            <asp:CheckBoxList ID="chkOperation" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                            </asp:CheckBoxList>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="65%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkAll_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="IbChk" runat="server" ImageUrl="../Images/chk.jpg" CommandName="Select" CssClass="aspxradiobutton hvr-bounce-in"></asp:ImageButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="Navigation" HeaderText="Navigation" Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </div>
    
      <div class="clearfix divmargin"></div>
     <div class="col-sm-12 col-md-12" style="padding: 0px">
        <asp:DataGrid ID="dgAccessRgt" runat="server" AutoGenerateColumns="False" AllowPaging="False" Width="100%" 
            class="footable" OnItemCommand="dgAccessRgt_ItemCommand">
            <HeaderStyle Font-Bold="true" BackColor="#e5e5e5" />
            <PagerStyle CssClass="gridpagination" Mode="NumericPages" />
            <Columns>
                <asp:BoundColumn DataField="Mod_Id" Visible="false">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Mod_Description" HeaderText="Description">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="25%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Permission" >
                    <ItemTemplate>
                        <asp:CheckBox ID="chkView" Text="View" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="8%" />
                        <asp:CheckBox ID="chkSaveOrUpdate" Text="Save/Update" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="20%" />
                        <asp:CheckBox ID="chkActiveOrDeActive" Text="Active/DeActive" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="20%" />
                        <asp:CheckBox ID="chkReport" Text="Report" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="15%" />
                        <asp:CheckBox ID="chkDownload" Text="Download" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="15%" />
                        <asp:CheckBox ID="chkAnnotation" Text="Annotation" runat="server" CssClass="aspxradiobutton hvr-bounce-in" Width="15%" />
                       <%-- <asp:CheckBox ID="chkDgAll" Text="All" ForeColor="Green" runat="server" AutoPostBack="True" OnCheckedChanged="chkDgAll_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in"></asp:CheckBox>--%>
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="75%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                </asp:TemplateColumn>


                <asp:BoundColumn DataField="mod_Function" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="Mod_Buttons" Visible="False">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Width="5%" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                </asp:BoundColumn>

                  <asp:TemplateColumn>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkDAOAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkDAOAll_CheckedChanged" CssClass="aspxradiobutton hvr-bounce-in"></asp:CheckBox>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="IbdgChk" runat="server" ImageUrl="../Images/chk.jpg" CommandName="Select" CssClass="aspxradiobutton hvr-bounce-in"></asp:ImageButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                    </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
    </div>

    <div id="ModalModulePermissionValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModulePermissionValidationMsg" runat="server"></asp:Label></strong>
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
</asp:Content>
