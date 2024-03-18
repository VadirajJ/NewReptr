<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="ScheduleDashboard.aspx.vb" Inherits="TRACePA.ScheduleDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />
    <style>
        @charset "UTF-8";

        .multi-steps > li.is-active ~ li:before,
        .multi-steps > li.is-active:before {
            content: counter(stepNum);
            font-family: inherit;
            font-weight: 700;
        }

        .multi-steps > li.is-active ~ li:after,
        .multi-steps > li.is-active:after {
            background-color: #27AE60;
        }

        .multi-steps {
            display: table;
            table-layout: fixed;
            width: 100%;
        }

            .multi-steps > li {
                counter-increment: stepNum;
                text-align: center;
                display: table-cell;
                position: relative;
                color: #000;
            }

                .multi-steps > li:before {
                    content: "";
                    content: "✓;";
                    content: "𐀃";
                    content: "𐀄";
                    content: "✓";
                    display: block;
                    margin: 0 auto 4px;
                    background-color: #27AE60;
                    width: 36px;
                    height: 36px;
                    line-height: 32px;
                    text-align: center;
                    font-weight: bold;
                    border-width: 2px;
                    border-style: solid;
                    border-color: #27AE60;
                    border-radius: 50%;
                }

                .multi-steps > li:after {
                    content: "";
                    height: 2px;
                    width: 100%;
                    background-color: #27AE60;
                    position: absolute;
                    top: 16px;
                    left: 50%;
                    z-index: -1;
                }

                .multi-steps > li:last-child:after {
                    display: none;
                }

                .multi-steps > li.is-active:before {
                    background-color: #27AE60;
                    border-color: #27AE60;
                    color: white
                }

                .multi-steps > li.is-active ~ li {
                    color: #808080;
                }

                    .multi-steps > li.is-active ~ li:before {
                        background-color: whitesmoke;
                        border-color: whitesmoke;
                    }
                                .line{
    height: 6px;
    background: red;
   }
    </style>
    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
    <div class="reportDetailsMN">
        <div class="sectionTitleMn">
            <div class="col-sm-6 col-md-6 pull-left">
                <h2><b>Financial Audit Status</b></h2>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
    <div class="container-fluid divmargin">
        <div class="col-sm-12 col-md-12 col-lg-12">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="pull-right divmargin ">
                    <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server"></asp:Label>
                    <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100px">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-4 col-md-4">
                    <div class="form-group">
                        <asp:Label ID="lblCustName" runat="server" Text="* Customer Name"></asp:Label>
                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                </div>
            </div>
        </div>
        <div class="container-fluid">
            <ul runat="server" visible="false" id="UlProgressbas" class="list-unstyled multi-steps">
                <li runat="server" id="liCustAssgn">Customer Creation with Industry Type</li>                
                <li runat="server" id="liRpyFormat" class="is-active">Report/Schedule Report creation/checking</li>
                <li runat="server" id="LiUpdate" visible="false">Update</li>
                <li runat="server" id="liUpload">Excel Uplaod Report/Schedule Mapping</li>
                <li runat="server" id="lirptgen">Report Generation</li>
                <li runat="server" id="lirptJe">Je Entries</li>
                <li runat="server" id="lirptDownload">Report Save And Download</li>
            </ul>
        </div>

    </div>
</asp:Content>
