<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/CustomerUserMaster.Master" CodeBehind="Cust_user_Homepage.aspx.vb" Inherits="TRACePA.Cust_user_Homepage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <link rel="stylesheet" href="../StyleSheet/bootstrap.min.css" type="text/css" />
    <link rel="stylesheet" href="../StyleSheet/custom.css" type="text/css" />

    <script src="../JavaScripts/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/bootstrap.min.js" type="text/javascript"></script>
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

        .line {
            height: 6px;
            background: red;
        }

        .blink_me {
            background-color: red;
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }
    </style>
    <div>
        <div class="col-sm-12 col-md-12 divmargin">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorMsgLeft"></asp:Label>
        </div>
        <div class="col-sm-12 col-md-12 col-lg-12">
            <div class="pull-right divmargin ">
                <asp:Label ID="lblHeadingFY" Text="Financial year" runat="server"></asp:Label>
                <asp:DropDownList ID="ddlFinancialYear" runat="server" AutoPostBack="true" CssClass="aspxcontrols" Width="100px">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-sm-6 col-md-6 divmargin">
            <div class="card">
                <div class="card-header ">
                    <asp:Label runat="server" ID="Label2" CssClass="form-label" Font-Bold="true" Text="Email Notifications" Font-Size="Small"></asp:Label>
                </div>
                <div class="card-body">
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="ddlCustName" runat="server" CssClass="aspxcontrols" AutoPostBack="true"></asp:DropDownList>
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
                    <div class="col-sm-6 col-md-6">
                        <asp:DropDownList ID="ddlAuditNos" runat="server" AutoPostBack="true" CssClass="aspxcontrols"></asp:DropDownList>
                    </div>
                    <div style="overflow-x: scroll; max-height: 300px">
                        <asp:GridView ID="GVCustremarks" ShowHeader="true" CssClass="table bs" runat="server" HeaderStyle-CssClass="FixedHeader" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" Width="100%">
                            <HeaderStyle Font-Bold="True" BackColor="#223f65" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Top" />
                            <Columns>
                                <asp:BoundField DataField="Notification" HeaderText="Notification" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="AuditNo" HeaderText="Audit No " ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Description" HeaderText="Checkpont/Ledger/Query" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Date" HeaderText="Remarks Recieved Date" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="20%" />
                                <asp:BoundField DataField="Comments_by" HeaderText="Comments by" ItemStyle-Width="15%" />
                                <asp:BoundField DataField="Role" HeaderText="Comments by" ItemStyle-Width="15%" />
                                <asp:TemplateField HeaderText="Comments Type" ItemStyle-Width="10%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblCommentsby" Text='<%# DataBinder.Eval(Container, "DataItem.Role") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblnotification" class="blink_me badge badge-primary text-uppercase">New
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
