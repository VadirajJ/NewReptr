<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Trade.aspx.vb" Inherits="TRACePA.Traid" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/select2.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/dataTables.bootstrap.min.css" type="text/css" />
    <style>
        /* .loader {
            position: fixed;
            left: 0px;
            top: 0px;
            width: 100%;
            height: 100%;
            z-index: 9999;
            background: url('../Images/pageloader.gif') 50% 50% no-repeat rgb(249,249,249);
        }*/

        #overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: white;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
    </style>
    <style type="text/css">
        .switch {
            position: relative;
            display: inline-block;
            width: 50px;
            height: 24px;
        }

            .switch input {
                opacity: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 16px;
                width: 16px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript">
                
    </script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JavaScripts/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="../JavaScripts/dataTables.bootstrap.min.js"></script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        $(window).load(function () {
            $(".loader").fadeOut("slow");
        })
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlCustomerName.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlFinancialYear.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlbranchName.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlOthType.ClientID%>').select2();
        });
  $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
            $('#<%=ddlcategory.ClientID%>').select2();
        });
    </script>
    <script lang="javascript" type="text/javascript">
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();

            $('#<%=dgGeneralSchedTemp.ClientID%>').DataTable({
                iDisplayLength: 500,
                aLengthMenu: [[5, 10, 20, 30, 40, 50, 100, 500, -1], [5, 10, 20, 30, 40, 50, 100, 500, "All"]],
                order: [],
                columnDefs: [{ orderable: false, targets: [0, 1] }],
                //bPaginate: false,
                //bLengthChange: false,
            });
        });
        <%--$(document).ready(function () {
            $('#<%=gvddlSubitem.ClientID%>').select2();
            $('#<%=gvddlitem.ClientID%>').select2();
            $('#<%=gvddlSubheading.ClientID%>').select2();
            $('#<%=gvddlheading.ClientID%>').select2();
        });--%>
    </script>
    <div class="col-sm-12 col-md-12 col-lg-12">
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
    </div>
    <div class="card">
        <div runat="server" id="divCompheader" class="card-header">
            <div class="sectionTitleMn">
                <div class="col-sm-6 col-md-6 pull-left">
                    <h4><b>Trade</b></h4>
                </div>
                 <div class="col-sm-6 col-md-6">
                    <div class="pull-right">
                        <asp:ImageButton ID="imgbtnBack" CssClass="activeIcons hvr-bounce-out" runat="server" data-toggle="tooltip" data-placement="bottom" title="Back" ValidationGroup="Validate" />
                       <a href="#" data-toggle="dropdown" runat="server"><span>
                         <img class="dropdown-toggle hvr-bounce-out" id="imgbtnReport" runat="server" data-toggle="tooltip" data-placement="top" title="Final Schedule Report" /></span></a>
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
        </div>

    </div>
     <div class="card">
    <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
       
          <div class="col-sm-12 col-md-12 pull-right">
                        <div class="form-group pull-right">
                            <asp:LinkButton ID="lnkDownload" runat="server"><b><i>Click here to Download Uploadable Excel</i></b></asp:LinkButton>
                        </div>
                    </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="Label6" runat="server" Text="* Customer Name"></asp:Label>
                <%--    <asp:RequiredFieldValidator ID="RFVFunction" runat="server" ControlToValidate="ddlReportType" CssClass="ErrorMsgRight" Display="Dynamic" SetFocusOnError="True" ValidationGroup="Validate"></asp:RequiredFieldValidator>--%>
                <asp:DropDownList ID="ddlCustomerName" runat="server" CssClass="aspxcontrols" AutoPostBack="True"></asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="Label7" runat="server" Text="Financial Year"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols">
                </asp:DropDownList>
            </div>
        </div>

        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="LblBranchName" runat="server" Text="* Branch Name"></asp:Label>
                <asp:DropDownList ID="ddlbranchName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
            </div>
        </div>

        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Category"></asp:Label>
                <asp:DropDownList ID="ddlcategory" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                    <asp:ListItem Value="0">Select Category Type</asp:ListItem>
                    <asp:ListItem Value="1">Trade receivables</asp:ListItem>
                    <asp:ListItem Value="2">Trade Payables</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
             </div>
         <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
               <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Other Types"></asp:Label>
                <asp:DropDownList ID="ddlOthType" runat="server" CssClass="aspxcontrols" AutoPostBack="true">
                    <asp:ListItem Value="0">Select Category Type</asp:ListItem>
                    <asp:ListItem Value="1">MSME</asp:ListItem>
                    <asp:ListItem Value="2">Others</asp:ListItem>
                    <asp:ListItem Value="3">Dispute dues-MSME</asp:ListItem>
                    <asp:ListItem Value="4">Dispute dues</asp:ListItem>
                    <asp:ListItem Value="5">Others</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-3 col-md-3">
            <div class="form-group">
                <asp:Label ID="lblSelectFileSchedTemp" runat="server" Text="Select Excel File(Trail Balance)"></asp:Label>
                <asp:FileUpload ID="FULoadSchedTemp" CssClass="aspxcontrols" value="Browse" name="avatar" runat="server" />
            </div>
            <asp:TextBox ID="txtPathSchedTemp" runat="server" CssClass="TextBox" ReadOnly="True" Visible="False" />
        </div>
        <div class="col-sm-3 col-md-3 col-lg-3">
            <div class="form-group">
                <asp:Label ID="Label14" runat="server" Text="Upload Excel File"></asp:Label><br />
                <button id="btnOkSchedTemp" bordercolor="Blue" forecolor="Green" class="btn-ok" runat="server" style="width: 31%"><i class="fa-solid fa-arrow-up-from-bracket fa-lg"></i></button>
            </div>
        </div>
</div>
    <div class="col-sm-3 col-md-3" style="display: none">
        <div class="form-group">
            <asp:Label ID="lblSheetNameSchedTemp" runat="server" Text="Sheet Name" Visible="false"></asp:Label>
            <asp:DropDownList ID="ddlSheetNameSchedTemp" runat="server" AutoPostBack="true" Visible="false" CssClass="aspxcontrols">
            </asp:DropDownList>
        </div>
    </div>


    <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
        <asp:GridView ID="gvAccRatio" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" Width="100%">
            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
        </asp:GridView>
    </div>
    <div class="col-sm-12 col-md-12 col-lg-12" style="padding-left: 0; padding-right: 0; padding-top: 5px; word-break: break-all">
        <div class="col-sm-12 col-md-12" style="padding-left: 0; padding-right: 0; word-break: break-all">
            <asp:GridView ID="gvAccRatioFormula" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="true" Width="100%">
                <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
            </asp:GridView>
        </div>
    </div>
 
    <div class="col-md-12" style="padding-left: 0; padding-right: 0">
        <div id="div1" runat="server" style="border-style: none; border-color: inherit; border-width: medium; width: 100%;">
            <div class="col-sm-12 col-md-12 form-group" style="padding-left: 0px">
                <div id="div2" runat="server" style="overflow-y: auto; height: 400px; width: 100%;">
                    <asp:GridView ID="dgGeneralSchedTemp" CssClass="table bs" RowHeaderColumn="fixed" HeaderStyle-CssClass="FixedHeader" Style="white-space: nowrap;" runat="server" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%" ShowFooter="true">
                        <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                        <Columns>
                            <asp:BoundField HeaderText="Slno" DataField="SrNo" HeaderStyle-Width="5px" HeaderStyle-VerticalAlign="Top" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" Visible="true"></asp:BoundField>
                            <asp:TemplateField HeaderText="Name" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="LabelName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_Name") %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="right" VerticalAlign="Top" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Less than 6 months" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' Width="250px"></asp:Label>--%>
                                   <asp:Label ID="lblLessmonth" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_Less_than_six_Month") %>' Width="85px"></asp:Label>
                                </ItemTemplate>
  <FooterTemplate>
                                <asp:Label ID="lblTotalLessmonth" runat="server"></asp:Label>
                            </FooterTemplate>
                            
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="More than 6 months" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <%--<asp:Label ID="lblProcessID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container, "DataItem.ProcessID") %>'></asp:Label>--%>
                                    <asp:Label ID="lblmorethen6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_More_than_six_Month") %>' Width="85px"></asp:Label>
                                </ItemTemplate>

                                 <FooterTemplate>
                                <asp:Label ID="lblTotalmorethen6" runat="server"></asp:Label>
                            </FooterTemplate>
                            
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="1 year" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl1year" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_One_Year") %>' Width="85px"></asp:Label>
                                    <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                </ItemTemplate>
                                  <FooterTemplate>
                                <asp:Label ID="lblTotal1year" runat="server"></asp:Label>
                            </FooterTemplate>
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="2 year" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl2year" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_Two_Year") %>' Width="85px"></asp:Label>

                                </ItemTemplate>
                               <FooterTemplate>
                                <asp:Label ID="lblTotal2year" runat="server"></asp:Label>
                            </FooterTemplate>
                            
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="3 year" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lbl3year" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_Three_Year") %>' Width="85px"></asp:Label>

                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lblTotal3year" runat="server"></asp:Label>
                            </FooterTemplate>
                            
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="More than 3 Year" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblMorethen" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_More_than") %>' Width="85px"></asp:Label>
                                    <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                </ItemTemplate>
                                <FooterTemplate>
                                <asp:Label ID="lblTotalMorethen" runat="server"></asp:Label>
                            </FooterTemplate>
                            
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="5px">
                                <ItemTemplate>
                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ATU_Total_Amount") %>' Width="85px"></asp:Label>
                                    <%-- <asp:Label ID="lblSubProcessID" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SubProcessID") %>' Visible="false"></asp:Label>--%>
                                </ItemTemplate>
                                 <FooterTemplate>
                                <asp:Label ID="lblfinalTotalAmount" runat="server"></asp:Label>
                            </FooterTemplate>
                            
                             <FooterStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top" Font-Bold="true" />
                             <HeaderStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="left" VerticalAlign="Top" />
                              <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
                                 HorizontalAlign="Right" VerticalAlign="Top"  />
                            </asp:TemplateField>                        
                        </Columns>
                        <EmptyDataTemplate>No Records Available</EmptyDataTemplate>
                    </asp:GridView>

                </div>
            </div>
        </div>
    </div>
      <div id="ModalValidation" class="modalmsg fade" data-backdrop="static" data-keyboard="false" role="dialog">
        <div class="modalmsg-dialog">
            <div class="modalmsg-content">
                <div class="modalmsg-header">
                    <h4 class="modal-title"><b>TRACe</b></h4>
                </div>
                <div class="modalmsg-body">
                    <div id="divExcelMsgType" class="alert alert-info">
                        <p>
                            <strong>
                                <asp:Label ID="lblModalValidationMsg" runat="server"></asp:Label>
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
        </div>
          <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="99%" Height="10px" Visible="false" PageCountMode="Actual"></rsweb:ReportViewer>
</asp:Content>
