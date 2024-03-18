Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.ScriptManager
Imports System.Drawing.FontStyle
Partial Class ScheduleReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "ScheduleReport"
    Dim sSession As New AllSession
    Dim objclsReport As New clsReport
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Dim objclsOpeningBalance As New clsOpeningBalance
    Dim objgenfunc As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleReport As New clsScheduleReport
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objIndex As New clsIndexing
    Dim objclsSchduleNote As New clsScheduleNote
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsCashFlow As New clsCashFlow
    Private objclsAccRatios As New clsAccountingRatios
    Dim objPhyReport As New ClsFXAPhysicalReport
    Private objAccountpolicies As New clsAccountpolicies
    Private objclsCompanyDetails As New clsCompanyDetails
    Private objclsPartnerFund As New clsPartnerFund

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        ' imgbtnArchive.ImageUrl = "~/Images/Archive24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                'BindCompanytype()
                BindCompanyName()
                ddlCompanyName.SelectedValue = objclsSchduleReport.GetDefalutCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
                LoadExistingCustomer()
                BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustomerName.SelectedValue = sSession.CustomerID
                        ddlCustomerName_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustomerName.SelectedValue = sSession.CustomerID
                        If ddlCustomerName.SelectedIndex > 0 Then
                            ddlCustomerName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub BindCompanyName()
        Try
            ddlCompanyName.DataSource = objclsCompanyDetails.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyName.DataTextField = "Company_Name"
            ddlCompanyName.DataValueField = "Company_ID"
            ddlCompanyName.DataBind()
            ddlCompanyName.Items.Insert(0, "Select Company")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingCompanyName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub

    Private Sub binddirectorsandpartners()
        Dim sOrgType As String
        Dim dtstatue As New DataTable
        Try
            sOrgType = objclsSchduleReport.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            If sOrgType = "Partnership firms" Then
                dtstatue = objclsSchduleReport.Loadpartners(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lstPartsandDirectors.DataSource = dtstatue
                lstPartsandDirectors.DataTextField = "SSP_PartnerName"
                lstPartsandDirectors.DataValueField = "PartnerPkID"
                lstPartsandDirectors.DataBind()
                lblparanddir.Text = "Partners"
                lblparanddir.Visible = True
                lstPartsandDirectors.Visible = True
            Else
                dtstatue = objclsSchduleReport.LoadDirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                lstPartsandDirectors.DataSource = dtstatue
                lstPartsandDirectors.DataTextField = "SSD_DirectorName"
                lstPartsandDirectors.DataValueField = "SSD_Id"
                lstPartsandDirectors.DataBind()
                lblparanddir.Text = "Directors"
                lblparanddir.Visible = True
                lstPartsandDirectors.Visible = True
            End If
            For Each item As ListItem In lstPartsandDirectors.Items
                item.Selected = True
            Next
        Catch ex As Exception

        End Try
    End Sub



    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objUT.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw 
        End Try
    End Sub
    'Public Function BindCompanytype()
    '    Try
    '        ddlComptype.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "ORG")
    '        ddlComptype.DataTextField = "cmm_Desc"
    '        ddlComptype.DataValueField = "cmm_ID"
    '        ddlComptype.DataBind()
    '        ddlComptype.Items.Insert(0, "Select Organization Type")
    '    Catch ex As Exception

    '    End Try
    'End Function
    Public Sub LoadExistingCustomer()
        Try
            ddlCustomerName.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "Cust_Name"
            ddlCustomerName.DataValueField = "Cust_Id"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim dt As New DataTable

        Dim dt2 As New DataTable
        Dim dt3 As New DataTable
        Dim sToYear As String = ""
        Dim arr() As String
        Dim iOrgID As Integer = 0
        Dim Tempheadingid As Integer = 0
        Dim dts As New DataTable
        Dim dr As DataRow
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim l As Integer = 0
        Dim tempHeadingChange As String = ""
        Dim tempSubHeadingChange As String = ""
        Dim totalItems As Decimal = 0
        Dim SubheadingComplet As Boolean = True
        Dim headingComplet As Boolean = True
        Dim ItemComplete As Boolean = True
        Dim Headingtotal1 As Integer = 0
        Dim Headingtotal2 As Integer = 0
        Dim Headingtotal3 As Integer = 0
        Dim r As DataRow
        Dim temptoatal1 As Decimal = 0
        Dim temptotal2 As Decimal = 0
        Dim dt4 As DataTable
        Dim dt5 As DataTable
        Dim dtbranch As New DataTable
        Dim dtSubHeading As New DataTable
        Try
            lblError.Text = ""
            lblModalValidationMsg.Text = lblError.Text
            ReportViewer1.Reset()
            lstbranchSchedTemp.Visible = True
            If ddlCustomerName.SelectedIndex > 0 Then
                dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtbranch.Rows.Count > 0 Then
                    lstbranchSchedTemp.DataSource = dtbranch
                    lstbranchSchedTemp.DataTextField = "BranchName"
                    lstbranchSchedTemp.DataValueField = "Branchid"
                    lstbranchSchedTemp.DataBind()
                Else
                    lblModalValidationMsg.Text = "Customer should have atleast one Branch, Please add"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    lblError.Text = lblModalValidationMsg.Text
                    Exit Sub
                End If
                If ddlRepType.SelectedIndex = 2 Then
                    pnlScheduleNote.Visible = True
                Else
                    pnlScheduleNote.Visible = False
                End If
                If ddlRepType.SelectedIndex = 4 Then
                    'ddlPartners_SelectedIndexChanged(sender, e)
                Else
                    Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                    AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                    AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                    Dim AppCustomerID As Integer = ddlCustomerName.SelectedValue
                    AppAccesscodeCookie.Value = AppCustomerID
                    AppAccesscodeCookie.Secure = True
                    AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                    Response.Cookies.Add(AppAccesscodeCookie)
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    lblUnmappedData.Visible = True
                    dt = objclsSchduleReport.LoadNonLinkeddescrition(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue)
                    dgGeneral.DataSource = dt
                    dgGeneral.DataBind()

                    lstSubHeadings.DataSource = objclsSchduleReport.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, DdlScheduletype.SelectedValue, ddlCustomerName.SelectedValue)
                    lstSubHeadings.DataTextField = "SubheadingName"
                    lstSubHeadings.DataValueField = "SubheadingID"
                    lstSubHeadings.DataBind()

                    lstItems.DataSource = objclsSchduleReport.LoadItems(sSession.AccessCode, sSession.AccessCodeID, DdlScheduletype.SelectedValue, ddlCustomerName.SelectedValue)
                    lstItems.DataTextField = "ItemName"
                    lstItems.DataValueField = "ItemID"
                    lstItems.DataBind()
                End If
            Else
                pnlScheduleNote.Visible = False
                dgGeneral.DataSource = Nothing
                dgGeneral.DataBind()
            End If
            binddirectorsandpartners()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged")
        End Try
    End Sub
    Function RemoveDuplicates(dt As DataTable)
        Try
            dt = dt.DefaultView.ToTable(True, "Headingname")
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            If ddlRepType.SelectedValue = 5 Then
                LoadAccountPolicies()
            Else
                ddlPartners_SelectedIndexChanged(sender, e)
            End If

            'ddlscheduletype_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Private Sub ChkbxDistinguish_CheckedChanged(sender As Object, e As EventArgs) Handles ChkbxDistinguish.CheckedChanged
    '    Dim dt4 As DataTable
    '    Dim dt5 As DataTable
    '    Try
    '        lblError.Text = ""
    '        If DdlScheduletype.SelectedIndex = 0 Then
    '            lblError.Text = "Select Schedule Type"
    '            lblModalValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
    '            Exit Sub
    '        ElseIf ddlCustomerName.SelectedIndex = 0 Then
    '            lblError.Text = "Select Customer"
    '            lblModalValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
    '            Exit Sub
    '        End If
    '        If DdlScheduletype.SelectedValue = 3 Then
    '            If ChkbxDistinguish.Checked = True Then
    '                dt4 = objclsSchduleReport.getPAndLIncome(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                dt5 = objclsSchduleReport.getPAndLExpense(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                Dim rds As New ReportDataSource("Income", dt4)
    '                ReportViewer1.LocalReport.DataSources.Add(rds)
    '                Dim rds2 As New ReportDataSource("Expenses", dt5)
    '                ReportViewer1.LocalReport.DataSources.Add(rds2)
    '                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/PandL.rdlc")
    '                ReportViewer1.LocalReport.Refresh()
    '                Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "PROFIT And LOSS REPORT")}
    '                ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                ReportViewer1.LocalReport.SetParameters(CompName)
    '                Dim SchedType As ReportParameter() = New ReportParameter() {New ReportParameter("SchedType", DdlScheduletype.SelectedItem.Text)}
    '                ReportViewer1.LocalReport.SetParameters(SchedType)
    '                Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                ReportViewer1.LocalReport.SetParameters(ReportType)
    '            End If
    '        ElseIf DdlScheduletype.SelectedValue = 4 Then
    '            If ChkbxDistinguish.Checked = True Then
    '                dt4 = objclsSchduleReport.getBalAssetSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                dt5 = objclsSchduleReport.getBalLiabilitySheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                Dim rds As New ReportDataSource("Income", dt4)
    '                ReportViewer1.LocalReport.DataSources.Add(rds)
    '                Dim rds2 As New ReportDataSource("Expenses", dt5)
    '                ReportViewer1.LocalReport.DataSources.Add(rds2)
    '                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/PandL.rdlc")
    '                ReportViewer1.LocalReport.Refresh()
    '                Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "Balance Sheet")}
    '                ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                ReportViewer1.LocalReport.SetParameters(CompName)
    '                Dim SchedType As ReportParameter() = New ReportParameter() {New ReportParameter("SchedType", DdlScheduletype.SelectedItem.Text)}
    '                ReportViewer1.LocalReport.SetParameters(SchedType)
    '                Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                ReportViewer1.LocalReport.SetParameters(ReportType)
    '            Else
    '                ddlCustomerName_SelectedIndexChanged(sender, e)
    '            End If
    '        Else
    '            lblError.Text = "No data"
    '            lblModalValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
    '            Exit Sub
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Private Sub ddlscheduletype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdlScheduletype.SelectedIndexChanged
        Dim dtpartners As New DataTable
        Try
            ReportViewer1.Reset()
            lblError.Text = ""
            'BindSubheading()
            btnFreeze.Visible = False
            If ddlCustomerName.SelectedIndex > 0 Then
                If ddlRepType.SelectedIndex > 0 Then
                    If DdlScheduletype.SelectedIndex > 0 Then

                        'ddlPartners_SelectedIndexChanged(sender, e)  '---For direct report Loading
                    ElseIf DdlScheduletype.SelectedIndex = 0 Then
                        If ddlRepType.SelectedIndex = 3 Then
                            'ddlPartners_SelectedIndexChanged(sender, e)
                        Else
                            lblError.Text = "Select Schedule Type"
                            lblModalValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If
                Else
                    lblError.Text = "Select Report Type"
                    ddlRepType.Focus() : ddlCustomerName.SelectedIndex = 0
                    lblModalValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
                'ddlCustomerName_SelectedIndexChanged(sender, e)
                lstSubHeadings.DataSource = objclsSchduleReport.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, DdlScheduletype.SelectedValue, ddlCustomerName.SelectedValue)
                lstSubHeadings.DataTextField = "SubheadingName"
                lstSubHeadings.DataValueField = "SubheadingID"
                lstSubHeadings.DataBind()

                lstItems.DataSource = objclsSchduleReport.LoadItems(sSession.AccessCode, sSession.AccessCodeID, DdlScheduletype.SelectedValue, ddlCustomerName.SelectedValue)
                lstItems.DataTextField = "ItemName"
                lstItems.DataValueField = "ItemID"
                lstItems.DataBind()
            Else
                'lblError.Text = "Select Customer"
                'lblModalValidationMsg.Text = lblError.Text
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                'Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub CopyColumns(ByVal source As DataTable, ByVal dest As DataTable, ParamArray columns As String())
        For Each sourcerow As DataRow In source.Rows
            Dim destRow As DataRow = dest.NewRow()

            For Each colname As String In columns
                destRow(colname) = sourcerow(colname)
            Next

            dest.Rows.Add(destRow)
        Next
    End Sub
    'Public Sub BindSubheading()
    '    Dim dt As New DataTable
    '    Try
    '        If DdlScheduletype.SelectedIndex > 0 Then
    '            dt = objclsSchduleReport.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, DdlScheduletype.SelectedValue, ddlCustomerName.SelectedValue)
    '            ddlSubheading.DataSource = dt
    '            ddlSubheading.DataTextField = "SubheadingName"
    '            ddlSubheading.DataValueField = "SubheadingID"
    '            ddlSubheading.DataBind()
    '            ddlSubheading.Items.Insert(0, New ListItem("Select Heading"))
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
    '        'Throw
    '    End Try
    'End Sub

    Private Sub dgGeneral_PreRender(sender As Object, e As EventArgs) Handles dgGeneral.PreRender
        Try
            If dgGeneral.Rows.Count > 0 Then
                dgGeneral.UseAccessibleHeader = True
                dgGeneral.HeaderRow.TableSection = TableRowSection.TableHeader
                dgGeneral.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub dgGeneral_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgGeneral.RowCommand
        Try
            lblError.Text = ""
            If e.CommandName = "Navigate" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                Response.Redirect(String.Format("~/FIN Statement/UploadTrailbalanceSchedule.aspx?StatusID={0}", 1), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_RowCommand")
        End Try
    End Sub

    Private Sub dgGeneral_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgGeneral.RowDataBound
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlPartners_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPartners.SelectedIndexChanged
        Dim dtpartners As New DataTable
        Dim dtprev As New DataTable
        Dim dtdirectors As New DataTable
        Dim dt4 As DataTable
        Dim dtcustdetails As DataTable
        Dim dtcompanydetails As DataTable
        Dim iSelectedLocation As Integer
        Dim sSelectedBranches As String = ""
        Dim sSelectedSHeading As String = "0"
        Dim dtCusAmtType As DataTable
        Dim iAmtType As Integer = 0
        Dim iRoundOff As Integer = 0
        Dim iCustId As String = 0
        Dim dSubItemId As String
        Dim sSelectedSItems As String = "0"
        Dim dtstatue As New DataTable
        Dim objDBL As New DBHelper
        Dim sOrgType As String
        Dim iLedgerAmt As Integer = 0
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            pnlPnLAmt.Visible = False
            'ddlSubheading.Visible = False
            'lblSubheading.Visible = False
            lblError.Text = ""
            btnFreeze.Visible = False
            If ddlCustomerName.SelectedIndex > 0 Then
                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If
                If chkBxExcel.Checked = True Then
                    iLedgerAmt = 1
                Else
                    iLedgerAmt = 0
                End If
                If ddlRepType.SelectedIndex > 0 Then
                    If DdlScheduletype.SelectedIndex > 0 Then
                        For i = 0 To lstbranchSchedTemp.Items.Count - 1
                            If lstbranchSchedTemp.Items(i).Selected = True Then
                                iSelectedLocation = iSelectedLocation + 1
                                sSelectedBranches = sSelectedBranches & "," & lstbranchSchedTemp.Items(i).Value
                            End If
                        Next
                        If lstbranchSchedTemp.Items.Count = 0 Then
                            sSelectedBranches = "0"
                        End If
                        If sSelectedBranches.StartsWith(",") Then
                            sSelectedBranches = sSelectedBranches.Remove(0, 1)
                        End If
                        If sSelectedBranches.EndsWith(",") Then
                            sSelectedBranches = sSelectedBranches.Remove(Len(sSelectedBranches) - 1, 1)
                        End If
                        If sSelectedBranches = "" Then
                            lblError.Text = "Select Branch for Customer"
                            Exit Sub
                        End If

                        If ddlRepType.SelectedIndex = 2 Then
                            For i = 0 To lstSubHeadings.Items.Count - 1
                                If lstSubHeadings.Items(i).Selected = True Then
                                    sSelectedSHeading = sSelectedSHeading & "," & lstSubHeadings.Items(i).Value
                                End If
                            Next
                            If lstSubHeadings.Items.Count = 0 Then
                                sSelectedSHeading = "0"
                            End If
                            If sSelectedSHeading.StartsWith(",") Then
                                sSelectedSHeading = sSelectedSHeading.Remove(0, 1)
                            End If
                            If sSelectedSHeading.EndsWith(",") Then
                                sSelectedSHeading = sSelectedSHeading.Remove(Len(sSelectedSHeading) - 1, 1)
                            End If
                            For i = 0 To lstItems.Items.Count - 1
                                If lstItems.Items(i).Selected = True Then
                                    sSelectedSItems = sSelectedSItems & "," & lstItems.Items(i).Value
                                End If
                            Next
                            If lstItems.Items.Count = 0 Then
                                sSelectedSItems = "0"
                            End If
                            If sSelectedSItems.StartsWith(",") Then
                                sSelectedSItems = sSelectedSItems.Remove(0, 1)
                            End If
                            If sSelectedSHeading.EndsWith(",") Then
                                sSelectedSItems = sSelectedSItems.Remove(Len(sSelectedSItems) - 1, 1)
                            End If
                        Else
                            sSelectedSHeading = "0"
                            sSelectedSItems = "0"
                        End If

                        dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                        If ddlPartners.SelectedIndex > 0 Then
                            dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, ddlPartners.SelectedValue)
                        Else
                            dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)
                        End If


                        dtdirectors = objclsSchduleReport.LoadCustomerdirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)

                        Dim sSelecteddirectorsandPartner As String = ""
                        For i = 0 To lstPartsandDirectors.Items.Count - 1
                            If lstPartsandDirectors.Items(i).Selected = True Then
                                sSelecteddirectorsandPartner = sSelecteddirectorsandPartner & "," & lstPartsandDirectors.Items(i).Value
                            End If
                        Next
                        If sSelecteddirectorsandPartner.StartsWith(",") Then
                            sSelecteddirectorsandPartner = sSelecteddirectorsandPartner.Remove(0, 1)
                        End If
                        If sSelecteddirectorsandPartner = "" Then
                            dtstatue = New DataTable()
                        Else
                            sOrgType = objclsSchduleReport.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                            If sOrgType = "Partnership firms" Then
                                dtstatue = objclsSchduleReport.Loadpartner1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelecteddirectorsandPartner, "")
                            Else
                                dtstatue = objclsSchduleReport.LoadDirector1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelecteddirectorsandPartner, "")
                            End If
                        End If
                        dSubItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select CUSt_BranchId  from SAD_CUSTOMER_MASTER where CUST_Id=" & ddlCustomerName.SelectedValue & "")
                        dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)

                        If ddlRepType.SelectedValue = 1 Then
                            If DdlScheduletype.SelectedValue = 1 Then
                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getJEEntries(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                dtprev = objclsSchduleReport.getJEEntries(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                    For i = 0 To dt4.Rows.Count - 1
                                        dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                    Next
                                End If
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                'Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                'ReportViewer1.LocalReport.DataSources.Add(rds2)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptSchdeuleJs.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Journal Entry For The Year Ended" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)

                            ElseIf DdlScheduletype.SelectedValue = 2 Then
                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getClosingstock(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                dtprev = objclsSchduleReport.getClosingstock(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                    For i = 0 To dt4.Rows.Count - 1
                                        dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                    Next
                                End If
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                'Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                'ReportViewer1.LocalReport.DataSources.Add(rds2)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptScheduleSlosingStock.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Closing Stock Entry For The Year Ended" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)
                            ElseIf DdlScheduletype.SelectedValue = 3 Then
                                btnFreeze.Visible = True
                                ReportViewer1.Reset()
                                sSession.PandLAmount = 0
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                sSession.PandLAmount = objclsSchduleReport.getPnl()
                                Session("AllSession") = sSession
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                ReportViewer1.LocalReport.DataSources.Add(rds2)
                                Dim rds1 As New ReportDataSource("DataSet3", dtstatue)
                                ReportViewer1.LocalReport.DataSources.Add(rds1)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim sUDINNo As ReportParameter() = New ReportParameter() {New ReportParameter("sUDINNo", txtUDINNo.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDINNo)
                                Dim sUDIINDate As ReportParameter() = New ReportParameter() {New ReportParameter("sUDIINDate", txtUDINDate.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDIINDate)
                                Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
                                ReportViewer1.LocalReport.SetParameters(CompRegNo)
                                Dim CYear As ReportParameter() = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(CYear)
                                Dim PYear As ReportParameter() = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                ReportViewer1.LocalReport.SetParameters(PYear)

                                Dim sAmtIn As String
                                If ddlAmountConvert.SelectedIndex > 0 Then
                                    sAmtIn = ddlAmountConvert.SelectedItem.Text
                                Else
                                    sAmtIn = "In Rupees"
                                End If
                                Dim sCust_fontstyle As String = ""
                                Dim sCust_border As Integer = 0
                                Dim dtformat As New DataTable
                                dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                                If dtformat(0)("CF_ID") <> 0 Then
                                    sCust_fontstyle = dtformat(0)("CF_name")
                                    sCust_border = dtformat(0)("CUST_rptBorder")
                                End If
                                Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                                ReportViewer1.LocalReport.SetParameters(sFontstyle)
                                Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                                ReportViewer1.LocalReport.SetParameters(sborderstyle)
                                Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                                ReportViewer1.LocalReport.SetParameters(sAmountTxt)
                                Dim Current_year As ReportParameter() = New ReportParameter() {New ReportParameter("Current_year", ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Current_year)
                                Dim Prev_year As ReportParameter() = New ReportParameter() {New ReportParameter("Prev_year", objclsGeneralFunctions.Get2DigitFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                                ReportViewer1.LocalReport.SetParameters(Prev_year)

                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Statement of Profit and Loss as at 31st March" & " - " & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)

                                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Company_Code)
                                If ddlPartners.SelectedIndex > 0 Then
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Else
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    End If
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                Else
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                    CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                    ReportViewer1.LocalReport.SetParameters(CYear)
                                    PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                    ReportViewer1.LocalReport.SetParameters(PYear)
                                End If
                            ElseIf DdlScheduletype.SelectedValue = 4 Then
                                ReportViewer1.Reset()

                                'dt4 = objclsSchduleReport.getBalSheetNonpvt(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                pnlPnLAmt.Visible = True
                                lblPnlamt.Text = objclsSchduleReport.getPnl()

                                'If ddlRepType.SelectedIndex = 2 Then
                                '    dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
                                '    If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                '        For i = 0 To dt4.Rows.Count - 1
                                '            If dtprev.Rows.Count > i Then
                                '                If IsDBNull(dtprev(i)("PrevyearTotoal")) = False Then
                                '                    dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("PrevyearTotoal")
                                '                Else
                                '                    dt4.Rows(i)("PrevyearTotoal") = ""
                                '                End If
                                '            Else
                                '                dt4.Rows(i)("PrevyearTotoal") = ""
                                '            End If

                                '        Next
                                '    End If
                                'End If
                                Dim sFinYear As String = ""

                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                ReportViewer1.LocalReport.DataSources.Add(rds2)
                                Dim rds1 As New ReportDataSource("DataSet3", dtstatue)
                                ReportViewer1.LocalReport.DataSources.Add(rds1)

                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim sUDINNo As ReportParameter() = New ReportParameter() {New ReportParameter("sUDINNo", txtUDINNo.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDINNo)
                                Dim sUDIINDate As ReportParameter() = New ReportParameter() {New ReportParameter("sUDIINDate", txtUDINDate.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDIINDate)
                                Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
                                ReportViewer1.LocalReport.SetParameters(CompRegNo)
                                Dim CYear As ReportParameter() = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(CYear)
                                Dim PYear As ReportParameter() = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                ReportViewer1.LocalReport.SetParameters(PYear)

                                Dim sAmtIn As String
                                If ddlAmountConvert.SelectedIndex > 0 Then
                                    sAmtIn = ddlAmountConvert.SelectedItem.Text
                                Else
                                    sAmtIn = "In Rupees"
                                End If
                                Dim sCust_fontstyle As String = ""
                                Dim sCust_border As Integer = 0
                                Dim dtformat As New DataTable
                                dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                                If dtformat(0)("CF_ID") <> 0 Then
                                    sCust_fontstyle = dtformat(0)("CF_name")
                                    sCust_border = dtformat(0)("CUST_rptBorder")
                                End If
                                Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                                ReportViewer1.LocalReport.SetParameters(sFontstyle)
                                Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                                ReportViewer1.LocalReport.SetParameters(sborderstyle)
                                Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                                ReportViewer1.LocalReport.SetParameters(sAmountTxt)
                                Dim Current_year As ReportParameter() = New ReportParameter() {New ReportParameter("Current_year", ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Current_year)
                                Dim Prev_year As ReportParameter() = New ReportParameter() {New ReportParameter("Prev_year", objclsGeneralFunctions.Get2DigitFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                                ReportViewer1.LocalReport.SetParameters(Prev_year)
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Balance Sheet as at 31st March" & " -20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)
                                CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(CYear)
                                PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                ReportViewer1.LocalReport.SetParameters(PYear)



                                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Company_Code)

                                If ddlPartners.SelectedIndex > 0 Then
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Else
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    End If
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                Else
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                    CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                    ReportViewer1.LocalReport.SetParameters(CYear)
                                    PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                    ReportViewer1.LocalReport.SetParameters(PYear)
                                End If


                            End If
                        ElseIf ddlRepType.SelectedValue = 2 Then
                            'ddlSubheading.Visible = True
                            'lblSubheading.Visible = True
                            If DdlScheduletype.SelectedValue = 3 Then
                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                'dtprev = objclsSchduleReport.getPAndL(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                'If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                '    For i = 0 To dt4.Rows.Count - 1
                                '        If dtprev.Rows.Count > i Then
                                '            If IsDBNull(dtprev(i)("HeaderSLNo")) = False Then
                                '                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                '            Else
                                '                dt4.Rows(i)("PrevyearTotoal") = ""
                                '            End If
                                '        Else
                                '            dt4.Rows(i)("PrevyearTotoal") = ""
                                '        End If

                                '    Next
                                'End If
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                ReportViewer1.LocalReport.DataSources.Add(rds2)
                                Dim rds1 As New ReportDataSource("DataSet3", dtstatue)
                                ReportViewer1.LocalReport.DataSources.Add(rds1)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedulesDetails.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim sUDINNo As ReportParameter() = New ReportParameter() {New ReportParameter("sUDINNo", txtUDINNo.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDINNo)
                                Dim sUDIINDate As ReportParameter() = New ReportParameter() {New ReportParameter("sUDIINDate", txtUDINDate.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDIINDate)
                                Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
                                ReportViewer1.LocalReport.SetParameters(CompRegNo)
                                Dim CYear As ReportParameter() = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(CYear)
                                Dim PYear As ReportParameter() = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                ReportViewer1.LocalReport.SetParameters(PYear)

                                Dim sAmtIn As String
                                If ddlAmountConvert.SelectedIndex > 0 Then
                                    sAmtIn = ddlAmountConvert.SelectedItem.Text
                                Else
                                    sAmtIn = "In Rupees"
                                End If
                                Dim sCust_fontstyle As String = ""
                                Dim sCust_border As Integer = 0
                                Dim dtformat As New DataTable
                                dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                                If dtformat(0)("CF_ID") <> 0 Then
                                    sCust_fontstyle = dtformat(0)("CF_name")
                                    sCust_border = dtformat(0)("CUST_rptBorder")
                                End If
                                Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                                ReportViewer1.LocalReport.SetParameters(sFontstyle)
                                Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                                ReportViewer1.LocalReport.SetParameters(sborderstyle)
                                Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                                ReportViewer1.LocalReport.SetParameters(sAmountTxt)
                                Dim Current_year As ReportParameter() = New ReportParameter() {New ReportParameter("Current_year", ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Current_year)
                                Dim Prev_year As ReportParameter() = New ReportParameter() {New ReportParameter("Prev_year", objclsGeneralFunctions.Get2DigitFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                                ReportViewer1.LocalReport.SetParameters(Prev_year)
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Notes Forming Part Of Financial Statements as at 31st March" & " - " & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)

                                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Company_Code)
                                CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(CYear)
                                PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                ReportViewer1.LocalReport.SetParameters(PYear)


                                If ddlPartners.SelectedIndex > 0 Then
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Else
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    End If
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                Else
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                    CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                    ReportViewer1.LocalReport.SetParameters(CYear)
                                    PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                    ReportViewer1.LocalReport.SetParameters(PYear)


                                End If
                            ElseIf DdlScheduletype.SelectedValue = 4 Then
                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                'dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
                                'If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                '    For i = 0 To dt4.Rows.Count - 1
                                '        If dtprev.Rows.Count > i Then
                                '            If IsDBNull(dtprev(i)("HeaderSLNo")) = False Then
                                '                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                '            Else
                                '                dt4.Rows(i)("PrevyearTotoal") = ""
                                '            End If
                                '        Else
                                '            dt4.Rows(i)("PrevyearTotoal") = ""
                                '        End If

                                '    Next
                                'End If
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                ReportViewer1.LocalReport.DataSources.Add(rds2)
                                Dim rds1 As New ReportDataSource("DataSet3", dtstatue)
                                ReportViewer1.LocalReport.DataSources.Add(rds1)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedulesDetails.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim sUDINNo As ReportParameter() = New ReportParameter() {New ReportParameter("sUDINNo", txtUDINNo.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDINNo)
                                Dim sUDIINDate As ReportParameter() = New ReportParameter() {New ReportParameter("sUDIINDate", txtUDINDate.Text)}
                                ReportViewer1.LocalReport.SetParameters(sUDIINDate)
                                Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
                                ReportViewer1.LocalReport.SetParameters(CompRegNo)
                                Dim CYear As ReportParameter() = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(CYear)
                                Dim PYear As ReportParameter() = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                ReportViewer1.LocalReport.SetParameters(PYear)

                                Dim sAmtIn As String
                                If ddlAmountConvert.SelectedIndex > 0 Then
                                    sAmtIn = ddlAmountConvert.SelectedItem.Text
                                Else
                                    sAmtIn = "In Ruppees"
                                End If
                                Dim sCust_fontstyle As String = ""
                                Dim sCust_border As Integer = 0
                                Dim dtformat As New DataTable
                                dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                                If dtformat(0)("CF_ID") <> 0 Then
                                    sCust_fontstyle = dtformat(0)("CF_name")
                                    sCust_border = dtformat(0)("CUST_rptBorder")
                                End If
                                Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                                ReportViewer1.LocalReport.SetParameters(sFontstyle)
                                Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                                ReportViewer1.LocalReport.SetParameters(sborderstyle)
                                Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                                ReportViewer1.LocalReport.SetParameters(sAmountTxt)
                                Dim Current_year As ReportParameter() = New ReportParameter() {New ReportParameter("Current_year", ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Current_year)
                                Dim Prev_year As ReportParameter() = New ReportParameter() {New ReportParameter("Prev_year", objclsGeneralFunctions.Get2DigitFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                                ReportViewer1.LocalReport.SetParameters(Prev_year)
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Notes Forming Part Of Financial Statements as at 31st March" & " - " & ddlFinancialYear.SelectedValue)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)

                                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", ddlCompanyName.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(Company_Code)


                                If ddlPartners.SelectedIndex > 0 Then
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Else
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    End If
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                Else
                                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                    ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                    ReportViewer1.LocalReport.SetParameters(org_name)
                                    CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                                    ReportViewer1.LocalReport.SetParameters(CYear)
                                    PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                                    ReportViewer1.LocalReport.SetParameters(PYear)

                                End If

                            ElseIf DdlScheduletype.SelectedValue = 3 Then
                            End If
                        ElseIf ddlRepType.SelectedValue = 3 Then
                            ReportViewer1.Reset()

                            dt4 = objclsSchduleReport.getCashFlow(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
                            Dim rds As New ReportDataSource("DataSet1", dt4)
                            ReportViewer1.LocalReport.DataSources.Add(rds)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportCashFlow.rdlc")
                            ReportViewer1.LocalReport.Refresh()
                            Dim sAmtIn As String
                            If ddlAmountConvert.SelectedIndex > 0 Then
                                sAmtIn = ddlAmountConvert.SelectedItem.Text
                            Else
                                sAmtIn = "In Ruppees"
                            End If
                            Dim sCust_fontstyle As String = ""
                            Dim sCust_border As Integer = 0
                            Dim dtformat As New DataTable
                            dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                            If dtformat(0)("CF_ID") <> 0 Then
                                sCust_fontstyle = dtformat(0)("CF_name")
                                sCust_border = dtformat(0)("CUST_rptBorder")
                            End If
                            Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                            ReportViewer1.LocalReport.SetParameters(sFontstyle)
                            Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                            ReportViewer1.LocalReport.SetParameters(sborderstyle)
                            Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                            ReportViewer1.LocalReport.SetParameters(sAmountTxt)
                            Dim Current_year As ReportParameter() = New ReportParameter() {New ReportParameter("Current_year", ddlFinancialYear.SelectedItem.Text)}
                            ReportViewer1.LocalReport.SetParameters(Current_year)
                            Dim Prev_year As ReportParameter() = New ReportParameter() {New ReportParameter("Prev_year", objclsGeneralFunctions.Get2DigitFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                            ReportViewer1.LocalReport.SetParameters(Prev_year)
                        End If
                    ElseIf DdlScheduletype.SelectedIndex = 0 Then
                        dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                        If ddlRepType.SelectedIndex = 3 Then
                            ReportViewer1.Reset()
                            dt4 = objclsSchduleReport.getCashFlow(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 0, ddlRepType.SelectedValue, chkOnOff.Checked)
                            Dim rds As New ReportDataSource("DataSet1", dt4)
                            ReportViewer1.LocalReport.DataSources.Add(rds)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportCashFlow.rdlc")
                            Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Financial Statements of " & " - " & ddlFinancialYear.SelectedItem.Text)}
                            ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                            ReportViewer1.LocalReport.SetParameters(Comp_Name)
                            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                            ReportViewer1.LocalReport.SetParameters(Company_Address)
                            ReportViewer1.LocalReport.Refresh()
                        ElseIf ddlRepType.SelectedIndex = 4 Then
                            ReportViewer1.Reset()
                            'dt4 = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, 4, 0,)
                            'Dim rds As New ReportDataSource("DataSet3", dt4)
                            'ReportViewer1.LocalReport.DataSources.Add(rds)
                            'ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ScheduleExportOPB.rdlc")
                            'Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Financial Statements of " & " - " & ddlFinancialYear.SelectedItem.Text)}
                            'ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                            'Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                            'ReportViewer1.LocalReport.SetParameters(Comp_Name)
                            'Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                            'ReportViewer1.LocalReport.SetParameters(Company_Address)


                            'ReportViewer1.LocalReport.Refresh()
                        Else
                            lblError.Text = "Select Schedule Type"
                            lblModalValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If
                Else
                    lblError.Text = "Select Report Type"
                    ddlRepType.Focus() : ddlCustomerName.SelectedIndex = 0
                    lblModalValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
                'ddlCustomerName_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "Select Customer"
                lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Function LoadAccountPolicies()
        Dim iSelectedLocation As Integer
        Dim sSelectedBranches As String = ""
        Dim sSelectedSHeading As String = "0"
        Dim dtACP As New DataTable
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            pnlPnLAmt.Visible = False
            'ddlSubheading.Visible = False
            'lblSubheading.Visible = False
            lblError.Text = ""
            btnFreeze.Visible = False
            divAcountpolicies.Visible = False
            If ddlCustomerName.SelectedIndex > 0 Then
                For i = 0 To lstbranchSchedTemp.Items.Count - 1
                    If lstbranchSchedTemp.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedBranches = sSelectedBranches & "," & lstbranchSchedTemp.Items(i).Value
                    End If
                Next
                If lstbranchSchedTemp.Items.Count = 0 Then
                    sSelectedBranches = "0"
                End If
                If sSelectedBranches.StartsWith(",") Then
                    sSelectedBranches = sSelectedBranches.Remove(0, 1)
                End If
                If sSelectedBranches.EndsWith(",") Then
                    sSelectedBranches = sSelectedBranches.Remove(Len(sSelectedBranches) - 1, 1)
                End If
                If sSelectedBranches = "" Then
                    lblError.Text = "Select Branch for Customer"
                    Exit Function
                End If
                txtAccountpolicies.Text = objclsSchduleReport.GetAccountPoliciesDescription(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, ddlFinancialYear.SelectedValue)
                txtAccountpolicies.Text = "Rubypixels Private Limited ('the Company') was incorporated on October 28th,2016. The Company is primarily engaged in  business of designing, developing, producing, debugging, processing, implementing, marketing, buying, selling, importing, exporting, exchanging, altering, granting licenses, franchising and deal in the overarching field of software and hardware products/services for computers, laptops, mobiles, television, internet and devices. To carry on the business of software products which includes designing and developing novel system architecture, algorithms, code, websites, web services, database design and analytics."
                dtACP = objclsSchduleReport.GetAccountPoliciesdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, ddlFinancialYear.SelectedValue)
                divAcountpolicies.Visible = True
            Else
                lblError.Text = "Select Customer"
                lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Function
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Private Sub ddlSubheading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubheading.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Dim iHeadingid As Integer = 0
    '    Dim Orgtypeid As Integer = 0
    '    Dim dtpartners As New DataTable
    '    Dim dtprev As New DataTable
    '    Dim dtdirectors As New DataTable
    '    Dim dt4 As DataTable
    '    Dim dtcustdetails As DataTable
    '    Dim dtcompanydetails As DataTable
    '    Try
    '        If ddlSubheading.SelectedIndex > 0 And ddlCustomerName.SelectedIndex > 0 Then
    '            dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '            dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, ddlPartners.SelectedValue)
    '            dtdirectors = objclsSchduleReport.LoadCustomerdirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '            dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '            Orgtypeid = objclsSchduleReport.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '            iHeadingid = objclsSchduleReport.getHeadingID(sSession.AccessCode, sSession.AccessCodeID, Orgtypeid, ddlSubheading.SelectedValue)
    '            dt = objclsSchduleReport.LoadSubheadingdetais(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, iHeadingid, ddlSubheading.SelectedValue, ddlSubheading.SelectedItem.Text)
    '            dtprev = objclsSchduleReport.LoadSubheadingdetais(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, iHeadingid, ddlSubheading.SelectedValue, ddlSubheading.SelectedItem.Text)
    '            If dt.Rows.Count > 0 Then
    '                ReportViewer1.Reset()
    '                If dt.Rows.Count And dtprev.Rows.Count <> 0 Then
    '                    For i = 0 To dt.Rows.Count - 1
    '                        dt.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                    Next
    '                End If
    '                Dim rds As New ReportDataSource("DataSet1", dt)
    '                ReportViewer1.LocalReport.DataSources.Add(rds)
    '                Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
    '                ReportViewer1.LocalReport.DataSources.Add(rds2)
    '                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                ReportViewer1.LocalReport.Refresh()
    '                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "BalanceSheet Account For The Year Ended 31st March" & " - " & ddlFinancialYear.selectedvalue)}
    '                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
    '                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                ReportViewer1.LocalReport.SetParameters(Comp_Name)
    '                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
    '                ReportViewer1.LocalReport.SetParameters(Company_Address)
    '                Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
    '                ReportViewer1.LocalReport.SetParameters(Partners_Name)
    '                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
    '                ReportViewer1.LocalReport.SetParameters(companydetailsName)
    '                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
    '                ReportViewer1.LocalReport.SetParameters(Company_Code)
    '                If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
    '                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
    '                    ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                Else
    '                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
    '                    ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                End If
    '                Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
    '                ReportViewer1.LocalReport.SetParameters(org_name)
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
    Private Sub ddlRepType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRepType.SelectedIndexChanged
        Dim dtpartners As DataTable
        Try
            ReportViewer1.Reset()
            lblError.Text = ""
            pnlScheduleNote.Visible = False
            If ddlRepType.SelectedIndex = 1 Then
                divNotDesc.Visible = False
            ElseIf ddlRepType.SelectedIndex = 2 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    pnlScheduleNote.Visible = True
                Else
                    pnlScheduleNote.Visible = False
                End If
                divNotDesc.Visible = True
            ElseIf ddlRepType.SelectedIndex = 3 Then
                'pnlReport.Visible = False
            ElseIf ddlRepType.SelectedIndex = 4 Then
                divNotDesc.Visible = False
                btnLoad_Click(sender, e)
            End If
            dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)
            ddlPartners.DataSource = dtpartners
            ddlPartners.DataTextField = "Fullname"
            ddlPartners.DataValueField = "usr_Id"
            ddlPartners.DataBind()
            ddlPartners.Items.Insert(0, "Select Partners")
            If ddlCustomerName.SelectedIndex > 0 Then

                'ddlPartners_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub chkOnOff_CheckedChanged(sender As Object, e As EventArgs) Handles chkOnOff.CheckedChanged
        Try
            'ddlPartners_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ReportViewer1.Reset()
            lblError.Text = ""
            If ddlFinancialYear.SelectedIndex > 0 Then
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            End If
            '  btnLoad_Click(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnArchive_Click(sender As Object, e As EventArgs) Handles btnArchive.Click
        Try
            Dim mimeType As String = Nothing
            Dim dtpartners As New DataTable
            Dim dtprev As New DataTable
            Dim dtdirectors As New DataTable
            Dim dt4 As DataTable
            Dim dtcustdetails As DataTable
            Dim dtcompanydetails As DataTable
            Dim iSelectedLocation As Integer
            Dim sSelectedBranches As String = ""
            Dim sSelectedSHeading As String = ""
            Dim sSelectedSItems As String = "0"
            Dim dtCusAmtType As DataTable
            Dim iAmtType As Integer = 0
            Dim iRoundOff As Integer = 0
            Dim iLedgerAmt As Integer = 0

            'ddlSubheading.Visible = False
            'lblSubheading.Visible = False
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then

                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If

                If ddlRepType.SelectedIndex > 0 Then
                    If DdlScheduletype.SelectedIndex > 0 Then
                        For i = 0 To lstbranchSchedTemp.Items.Count - 1
                            If lstbranchSchedTemp.Items(i).Selected = True Then
                                iSelectedLocation = iSelectedLocation + 1
                                sSelectedBranches = sSelectedBranches & "," & lstbranchSchedTemp.Items(i).Value
                            End If
                        Next
                        If iSelectedLocation = 0 Then
                            lblModalValidationMsg.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                            lstbranchSchedTemp.Focus()
                            Exit Sub
                        End If
                        If sSelectedBranches.StartsWith(",") Then
                            sSelectedBranches = sSelectedBranches.Remove(0, 1)
                        End If
                        If sSelectedBranches.EndsWith(",") Then
                            sSelectedBranches = sSelectedBranches.Remove(Len(sSelectedBranches) - 1, 1)
                        End If

                        If ddlRepType.SelectedIndex > 0 Then
                            For i = 0 To lstSubHeadings.Items.Count - 1
                                If lstSubHeadings.Items(i).Selected = True Then
                                    sSelectedSHeading = sSelectedSHeading & "," & lstSubHeadings.Items(i).Value
                                End If
                            Next
                            If lstSubHeadings.Items.Count = 0 Then
                                sSelectedSHeading = "0"
                            End If
                            If sSelectedSHeading.StartsWith(",") Then
                                sSelectedSHeading = sSelectedSHeading.Remove(0, 1)
                            End If
                            If sSelectedSHeading.EndsWith(",") Then
                                sSelectedSHeading = sSelectedSHeading.Remove(Len(sSelectedSHeading) - 1, 1)
                            End If
                            For i = 0 To lstItems.Items.Count - 1
                                If lstItems.Items(i).Selected = True Then
                                    sSelectedSItems = sSelectedSItems & "," & lstItems.Items(i).Value
                                End If
                            Next
                            If lstItems.Items.Count = 0 Then
                                sSelectedSItems = "0"
                            End If
                            If sSelectedSItems.StartsWith(",") Then
                                sSelectedSItems = sSelectedSItems.Remove(0, 1)
                            End If
                            If sSelectedSItems.EndsWith(",") Then
                                sSelectedSItems = sSelectedSItems.Remove(Len(sSelectedSItems) - 1, 1)
                            End If
                        Else
                            sSelectedSHeading = "0"
                            sSelectedSItems = "0"
                        End If

                        dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                            If ddlPartners.SelectedIndex > 0 Then
                                dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, ddlPartners.SelectedValue)
                            Else
                                dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)
                            End If

                        dtdirectors = objclsSchduleReport.LoadCustomerdirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                            dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                            If ddlRepType.SelectedValue = 1 Then
                            If DdlScheduletype.SelectedValue = 1 Then
                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getJEEntries(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                dtprev = objclsSchduleReport.getJEEntries(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                    For i = 0 To dt4.Rows.Count - 1
                                        dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                    Next
                                End If
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                'Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                'ReportViewer1.LocalReport.DataSources.Add(rds2)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptSchdeuleJs.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Journal Entry For The Year Ended" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)

                            ElseIf DdlScheduletype.SelectedValue = 2 Then
                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getClosingstock(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                dtprev = objclsSchduleReport.getClosingstock(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                    For i = 0 To dt4.Rows.Count - 1
                                        dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                    Next
                                End If
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                'Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                'ReportViewer1.LocalReport.DataSources.Add(rds2)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptScheduleSlosingStock.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Closing Stock Entry For The Year Ended" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                ReportViewer1.LocalReport.SetParameters(Company_Address)
                            ElseIf DdlScheduletype.SelectedValue = 3 Then

                                ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                    ReportViewer1.LocalReport.DataSources.Add(rds)
                                    Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                    ReportViewer1.LocalReport.DataSources.Add(rds2)
                                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
                                    ReportViewer1.LocalReport.Refresh()
                                    Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Statement of Profit and Loass For The Year Ended" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                    ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                    Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                    ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                    Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Address)

                                    Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                    Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Code)
                                    If ddlPartners.SelectedIndex > 0 Then
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Else
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        End If
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    Else
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    End If
                                ElseIf DdlScheduletype.SelectedValue = 4 Then
                                    ReportViewer1.Reset()
                                'dt4 = objclsSchduleReport.getBalSheetNonpvt(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)

                                'If ddlRepType.SelectedIndex = 2 Then
                                '    dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
                                '    If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                '        For i = 0 To dt4.Rows.Count - 1
                                '            If dtprev.Rows.Count > i Then
                                '                If IsDBNull(dtprev(i)("PrevyearTotoal")) = False Then
                                '                    dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("PrevyearTotoal")
                                '                Else
                                '                    dt4.Rows(i)("PrevyearTotoal") = ""
                                '                End If
                                '            Else
                                '                dt4.Rows(i)("PrevyearTotoal") = ""
                                '            End If

                                '        Next
                                '    End If
                                'End If
                                Dim sFinYear As String = ""

                                    Dim rds As New ReportDataSource("DataSet1", dt4)
                                    ReportViewer1.LocalReport.DataSources.Add(rds)
                                    Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                    ReportViewer1.LocalReport.DataSources.Add(rds2)

                                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
                                    ReportViewer1.LocalReport.Refresh()
                                    Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Balance Sheet as at 31st March" & " - " & ddlFinancialYear.SelectedValue)}
                                    ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                    Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                    ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                    Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Address)

                                    Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                    Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Code)

                                    If ddlPartners.SelectedIndex > 0 Then
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Else
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        End If
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    Else
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    End If
                                End If
                            ElseIf ddlRepType.SelectedValue = 2 Then
                                'ddlSubheading.Visible = True
                                'lblSubheading.Visible = True
                                If DdlScheduletype.SelectedValue = 3 Then
                                    ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                    'dtprev = objclsSchduleReport.getPAndL(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
                                    'If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                    '    For i = 0 To dt4.Rows.Count - 1
                                    '        If dtprev.Rows.Count > i Then
                                    '            If IsDBNull(dtprev(i)("HeaderSLNo")) = False Then
                                    '                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                    '            Else
                                    '                dt4.Rows(i)("PrevyearTotoal") = ""
                                    '            End If
                                    '        Else
                                    '            dt4.Rows(i)("PrevyearTotoal") = ""
                                    '        End If

                                    '    Next
                                    'End If
                                    ReportViewer1.LocalReport.DataSources.Add(rds)
                                    Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                    ReportViewer1.LocalReport.DataSources.Add(rds2)
                                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
                                    ReportViewer1.LocalReport.Refresh()
                                    Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Notes Forming Part Of Financial Statements" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                    ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                    Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                    ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                    Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Address)

                                    Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                    Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Code)


                                    If ddlPartners.SelectedIndex > 0 Then
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Else
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        End If
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    Else
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    End If
                                ElseIf DdlScheduletype.SelectedValue = 4 Then
                                    ReportViewer1.Reset()
                                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                                'dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.selectedvalue - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
                                'If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
                                '    For i = 0 To dt4.Rows.Count - 1
                                '        If dtprev.Rows.Count > i Then
                                '            If IsDBNull(dtprev(i)("HeaderSLNo")) = False Then
                                '                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
                                '            Else
                                '                dt4.Rows(i)("PrevyearTotoal") = ""
                                '            End If
                                '        Else
                                '            dt4.Rows(i)("PrevyearTotoal") = ""
                                '        End If

                                '    Next
                                'End If
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                    ReportViewer1.LocalReport.DataSources.Add(rds)
                                    Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
                                    ReportViewer1.LocalReport.DataSources.Add(rds2)
                                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
                                    ReportViewer1.LocalReport.Refresh()
                                    Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Notes Forming Part Of Financial Statements" & " - " & ddlFinancialYear.SelectedItem.Text)}
                                    ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                                    Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                                    ReportViewer1.LocalReport.SetParameters(Comp_Name)
                                    Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Address)

                                    Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(companydetailsName)
                                    Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
                                    ReportViewer1.LocalReport.SetParameters(Company_Code)


                                    If ddlPartners.SelectedIndex > 0 Then
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Else
                                            Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                                            ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        End If
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    Else
                                        Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_Name)
                                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                                        ReportViewer1.LocalReport.SetParameters(Partners_MNum)
                                        Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                                        ReportViewer1.LocalReport.SetParameters(org_name)
                                    End If

                                ElseIf DdlScheduletype.SelectedValue = 3 Then
                                End If
                            ElseIf ddlRepType.SelectedValue = 3 Then
                                ReportViewer1.Reset()

                                dt4 = objclsSchduleReport.getCashFlow(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
                                Dim rds As New ReportDataSource("DataSet1", dt4)
                                ReportViewer1.LocalReport.DataSources.Add(rds)
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportCashFlow.rdlc")
                                ReportViewer1.LocalReport.Refresh()
                            End If
                        ElseIf DdlScheduletype.SelectedIndex = 0 Then
                            dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                        If ddlRepType.SelectedIndex = 3 Then
                            ReportViewer1.Reset()
                            dt4 = objclsSchduleReport.getCashFlow(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 0, ddlRepType.SelectedValue, chkOnOff.Checked)
                            Dim rds As New ReportDataSource("DataSet1", dt4)
                            ReportViewer1.LocalReport.DataSources.Add(rds)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportCashFlow.rdlc")
                            Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Financial Statements of " & " - " & ddlFinancialYear.SelectedItem.Text)}
                            ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                            ReportViewer1.LocalReport.SetParameters(Comp_Name)
                            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                            ReportViewer1.LocalReport.SetParameters(Company_Address)
                            ReportViewer1.LocalReport.Refresh()
                        ElseIf ddlRepType.SelectedIndex = 4 Then
                            ReportViewer1.Reset()
                            dt4 = objUT.GetCustCOADetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSession.YearID, 4, 0, lstbranchSchedTemp.SelectedValue)
                            Dim rds As New ReportDataSource("DataSet3", dt4)
                            ReportViewer1.LocalReport.DataSources.Add(rds)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ScheduleExportOPB.rdlc")
                            Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Financial Statements of " & " - " & ddlFinancialYear.SelectedItem.Text)}
                            ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
                            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                            ReportViewer1.LocalReport.SetParameters(Comp_Name)
                            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                            ReportViewer1.LocalReport.SetParameters(Company_Address)


                            ReportViewer1.LocalReport.Refresh()
                        Else
                            lblError.Text = "Select Schedule Type"
                            lblModalValidationMsg.Text = lblError.Text
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If
                Else
                    lblError.Text = "Select Report Type"
                    ddlRepType.Focus() : ddlCustomerName.SelectedIndex = 0
                    lblModalValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
                'ddlCustomerName_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "Select Customer"
                lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If


            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=" + ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text + ".pdf" + "")
            Response.BinaryWrite(RptViewer)
            Response.Flush()

            ' Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)
            Dim sImagePath As String
            sImagePath = objIndex.GetImagePath(sSession.AccessCode, sSession.AccessCodeID)
            sImagePath = sImagePath & "\Web\"
            If Directory.Exists(sImagePath) = False Then
                Directory.CreateDirectory(sImagePath)
            End If
            If Directory.Exists(sImagePath) Then
                For Each filepath As String In Directory.GetFiles(sImagePath)
                    File.Delete(filepath)
                Next
            End If
            Using Stream As New FileStream(sImagePath + "" + ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text + ".pdf", FileMode.Create)
                Stream.Write(RptViewer, 0, RptViewer.Length)
            End Using

            GetIndexing(sImagePath + "\" + ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text + ".pdf")
            Response.End()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    'Private Sub btnArchive_Click(sender As Object, e As EventArgs) Handles btnArchive.Click
    '    Try
    '        Dim mimeType As String = Nothing
    '        Dim dtpartners As New DataTable
    '        Dim dtprev As New DataTable
    '        Dim dtdirectors As New DataTable
    '        Dim dt4 As DataTable
    '        Dim dtcustdetails As DataTable
    '        Dim dtcompanydetails As DataTable

    '        If ddlCustomerName.SelectedIndex > 0 Then
    '            If ddlRepType.SelectedIndex > 0 Then
    '                If DdlScheduletype.SelectedIndex > 0 Then
    '                    dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '                    dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, ddlPartners.SelectedValue)
    '                    dtdirectors = objclsSchduleReport.LoadCustomerdirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '                    dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '                    If ddlRepType.SelectedValue = 1 Then
    '                        If DdlScheduletype.SelectedValue = 1 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getdetailsManufacturing(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            dtprev = objclsSchduleReport.getdetailsManufacturing(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            For i = 0 To dt4.Rows.Count - 1
    '                                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                            Next
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "Finalisation Of Accounts")}
    '                            ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                            Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(CompName)
    '                            Dim SchedType As ReportParameter() = New ReportParameter() {New ReportParameter("SchedType", DdlScheduletype.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(SchedType)
    '                            Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                            ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                            Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(ReportType)
    '                        ElseIf DdlScheduletype.SelectedValue = 2 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getdetailsTrading(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            dtprev = objclsSchduleReport.getdetailsTrading(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            For i = 0 To dt4.Rows.Count - 1
    '                                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                            Next
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "Finalisation Of Accounts")}
    '                            ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                            Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(CompName)
    '                            Dim SchedType As ReportParameter() = New ReportParameter() {New ReportParameter("SchedType", DdlScheduletype.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(SchedType)
    '                            Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                            ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                            Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(ReportType)
    '                        ElseIf DdlScheduletype.SelectedValue = 3 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getPAndL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            dtprev = objclsSchduleReport.getPAndL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
    '                            ReportViewer1.LocalReport.DataSources.Add(rds2)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "BalanceSheet Account For The Year Ended 31st March" & " - " & sSession.YearID)}
    '                            ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
    '                            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(Comp_Name)
    '                            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Company_Address)
    '                            Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Partners_Name)
    '                            Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(companydetailsName)
    '                            Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Company_Code)
    '                            If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
    '                                Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
    '                                ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                            Else
    '                                Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
    '                                ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                            End If
    '                            Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(org_name)
    '                        ElseIf DdlScheduletype.SelectedValue = 4 Then
    '                            ReportViewer1.Reset()
    '                            'dt4 = objclsSchduleReport.getBalSheetNonpvt(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
    '                            dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
    '                            If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
    '                                For i = 0 To dt4.Rows.Count - 1
    '                                    dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                                Next
    '                            End If
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
    '                            ReportViewer1.LocalReport.DataSources.Add(rds2)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "BalanceSheet Account For The Year Ended 31st March" & " - " & sSession.YearID)}
    '                            ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
    '                            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(Comp_Name)
    '                            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Company_Address)
    '                            Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Partners_Name)
    '                            Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(companydetailsName)
    '                            Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Company_Code)
    '                            If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
    '                                Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
    '                                ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                            Else
    '                                Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
    '                                ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                            End If
    '                            Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(org_name)
    '                        End If
    '                    ElseIf ddlRepType.SelectedValue = 2 Then
    '                        ddlSubheading.Visible = True
    '                        lblSubheading.Visible = True
    '                        If DdlScheduletype.SelectedValue = 1 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getdetailsManufacturing(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
    '                            For i = 0 To dt4.Rows.Count - 1
    '                                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                            Next
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "Finalisation Of Accounts")}
    '                            ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                            Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(CompName)
    '                            Dim SchedType As ReportParameter() = New ReportParameter() {New ReportParameter("SchedType", DdlScheduletype.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(SchedType)
    '                            Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                            ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                            Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(ReportType)
    '                        ElseIf DdlScheduletype.SelectedValue = 2 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getdetailsTrading(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
    '                            For i = 0 To dt4.Rows.Count - 1
    '                                dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                            Next
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "Finalisation Of Accounts")}
    '                            ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                            Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(CompName)
    '                            Dim SchedType As ReportParameter() = New ReportParameter() {New ReportParameter("SchedType", DdlScheduletype.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(SchedType)
    '                            Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                            ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                            Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(ReportType)
    '                        ElseIf DdlScheduletype.SelectedValue = 3 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getPAndL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            dtprev = objclsSchduleReport.getPAndL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue)
    '                            If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
    '                                For i = 0 To dt4.Rows.Count - 1
    '                                    dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                                Next
    '                            End If
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORTDEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORTDEF", "PROFIT And LOSS REPORT")}
    '                            ReportViewer1.LocalReport.SetParameters(REPORTDEF)
    '                            Dim CompName As ReportParameter() = New ReportParameter() {New ReportParameter("CompName", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            Dim CompanyType As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyType", "Proprioter's")}
    '                            ReportViewer1.LocalReport.SetParameters(CompanyType)
    '                            Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", ddlRepType.SelectedItem.Text)}
    '                            ReportViewer1.LocalReport.SetParameters(ReportType)
    '                        ElseIf DdlScheduletype.SelectedValue = 4 Then
    '                            ReportViewer1.Reset()
    '                            dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
    '                            dtprev = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID - 1, ddlCustomerName.SelectedValue, DdlScheduletype.SelectedValue, ddlRepType.SelectedValue, chkOnOff.Checked)
    '                            If dt4.Rows.Count And dtprev.Rows.Count <> 0 Then
    '                                For i = 0 To dt4.Rows.Count - 1
    '                                    dt4.Rows(i)("PrevyearTotoal") = dtprev.Rows(i)("HeaderSLNo")
    '                                Next
    '                            End If
    '                            Dim rds As New ReportDataSource("DataSet1", dt4)
    '                            ReportViewer1.LocalReport.DataSources.Add(rds)
    '                            Dim rds2 As New ReportDataSource("DataSet2", dtdirectors)
    '                            ReportViewer1.LocalReport.DataSources.Add(rds2)
    '                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/ReportSchedules.rdlc")
    '                            ReportViewer1.LocalReport.Refresh()
    '                            Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "BalanceSheet Account For The Year Ended 31st March" & " - " & sSession.YearID)}
    '                            ReportViewer1.LocalReport.SetParameters(REPORT_DEF)
    '                            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
    '                            ReportViewer1.LocalReport.SetParameters(Comp_Name)
    '                            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Company_Address)
    '                            Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Partners_Name)
    '                            Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(companydetailsName)
    '                            Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(Company_Code)
    '                            If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
    '                                Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
    '                                ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                            Else
    '                                Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
    '                                ReportViewer1.LocalReport.SetParameters(Partners_MNum)
    '                            End If
    '                            Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
    '                            ReportViewer1.LocalReport.SetParameters(org_name)
    '                        End If
    '                    End If

    '                ElseIf DdlScheduletype.SelectedIndex = 0 Then
    '                    lblError.Text = "Select Schedule Type"
    '                    lblModalValidationMsg.Text = lblError.Text
    '                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
    '                    Exit Sub
    '                End If
    '            Else
    '                lblError.Text = "Select Report Type"
    '                ddlRepType.Focus() : ddlCustomerName.SelectedIndex = 0
    '                lblModalValidationMsg.Text = lblError.Text
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            'ddlCustomerName_SelectedIndexChanged(sender, e)
    '        Else
    '            lblError.Text = "Select Company Type"
    '            lblModalValidationMsg.Text = lblError.Text
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
    '            Exit Sub
    '        End If


    '        Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ContentType = mimeType
    '        Response.AddHeader("content-disposition", "attachment; filename=" + ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text + ".pdf" + "")
    '        Response.BinaryWrite(RptViewer)
    '        Response.Flush()

    '        ' Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)
    '        Dim sImagePath As String
    '        sImagePath = objIndex.GetImagePath(sSession.AccessCode, sSession.AccessCodeID)
    '        sImagePath = sImagePath & "\Web\"
    '        If Directory.Exists(sImagePath) = False Then
    '            Directory.CreateDirectory(sImagePath)
    '        End If
    '        If Directory.Exists(sImagePath) Then
    '            For Each filepath As String In Directory.GetFiles(sImagePath)
    '                File.Delete(filepath)
    '            Next
    '        End If
    '        Using Stream As New FileStream(sImagePath + "" + ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text + ".pdf", FileMode.Create)
    '            Stream.Write(RptViewer, 0, RptViewer.Length)
    '        End Using

    '        GetIndexing(sImagePath + "\" + ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text + ".pdf")
    '        Response.End()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    Private Sub GetIndexing(ByVal sImagePath As String)
        Try
            Dim service As New traceapi()
            service.CreateCabinet(sSession.AccessCode, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, "G", "1", "0", "0", "0", "0", "0")
            service.CreateSubCabinet(sSession.AccessCode, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, sSession.YearName, "G", "1", "0", "0", "0", "0", "0", "0")
            service.CreateFolder(sSession.AccessCode, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, sSession.YearName, ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text, "G", "1", "0", "0", "0", "0", "0")
            service.FileDocumentINEdictNew(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, sSession.YearName, ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text, ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text, ddlRepType.SelectedItem.Text & "-" & DdlScheduletype.SelectedItem.Text, sImagePath)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkDownloadNoteDesc_Click(sender As Object, e As EventArgs) Handles lnkDownloadNoteDesc.Click
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Response.Redirect(String.Format("ScheduleNote.aspx?"), False)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkScheduleNotedownload_Click(sender As Object, e As EventArgs) Handles lnkScheduleNotedownload.Click
        Dim dt1 As DataTable, dt2 As DataTable, dt3 As DataTable, dt4 As DataTable, dt5 As DataTable, dt6 As DataTable
        Dim dtS1 As DataTable, dtS2 As DataTable, dtS3 As DataTable, dtS4 As DataTable
        Dim dtT1 As DataTable, dtT2 As DataTable, dtT3 As DataTable, dtT4 As DataTable, dtT5 As DataTable, dtT6 As DataTable
        Dim dtT7 As DataTable, dtT8 As DataTable, dtT9 As DataTable
        Dim mimeType As String = Nothing
        Try

            If ddlCustomerName.SelectedIndex > 0 Then
                ReportViewer1.Reset()
                dt1 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
                Dim rds1 As New ReportDataSource("DataSet1", dt1)
                ReportViewer1.LocalReport.DataSources.Add(rds1)

                dt2 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
                Dim rds2 As New ReportDataSource("DataSet2", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds2)

                dt3 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
                Dim rds3 As New ReportDataSource("DataSet3", dt3)
                ReportViewer1.LocalReport.DataSources.Add(rds3)

                dt4 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
                Dim rds4 As New ReportDataSource("DataSet4", dt4)
                ReportViewer1.LocalReport.DataSources.Add(rds4)

                dt5 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
                Dim rds5 As New ReportDataSource("DataSet5", dt5)
                ReportViewer1.LocalReport.DataSources.Add(rds5)

                dt6 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
                Dim rds6 As New ReportDataSource("DataSet6", dt6)
                ReportViewer1.LocalReport.DataSources.Add(rds6)


                dtS1 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SF")
                Dim rds7 As New ReportDataSource("ScheduleNote_Second1", dtS1)
                ReportViewer1.LocalReport.DataSources.Add(rds7)

                dtS2 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SS")
                Dim rds8 As New ReportDataSource("ScheduleNote_Second2", dtS2)
                ReportViewer1.LocalReport.DataSources.Add(rds8)

                dtS3 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "ST")
                Dim rds9 As New ReportDataSource("ScheduleNote_Second3", dtS3)
                ReportViewer1.LocalReport.DataSources.Add(rds9)

                dtS4 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SV")
                Dim rds10 As New ReportDataSource("ScheduleNote_Second4", dtS4)
                ReportViewer1.LocalReport.DataSources.Add(rds10)

                dtT1 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
                Dim rds11 As New ReportDataSource("ScheduleNote_Third1", dtT1)
                ReportViewer1.LocalReport.DataSources.Add(rds11)

                dtT2 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                Dim rds12 As New ReportDataSource("ScheduleNote_Third2", dtT2)
                ReportViewer1.LocalReport.DataSources.Add(rds12)

                dtT2 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "cEquity")
                Dim rds13 As New ReportDataSource("ScheduleNote_Desc", dtT2)
                ReportViewer1.LocalReport.DataSources.Add(rds13)

                dtT3 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "dPref")
                Dim rds14 As New ReportDataSource("ScheduleNote_Desc1", dtT3)
                ReportViewer1.LocalReport.DataSources.Add(rds14)

                dtT4 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEE")
                Dim rds15 As New ReportDataSource("ScheduleNote_Third3", dtT4)
                ReportViewer1.LocalReport.DataSources.Add(rds15)

                dtT5 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                Dim rds16 As New ReportDataSource("ScheduleNote_Third4", dtT5)
                ReportViewer1.LocalReport.DataSources.Add(rds16)

                dtT6 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "fShares")
                Dim rds17 As New ReportDataSource("ScheduleNote_Desc2", dtT6)
                ReportViewer1.LocalReport.DataSources.Add(rds17)

                dtT7 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
                Dim rds18 As New ReportDataSource("ScheduleNote_Fourth", dtT7)
                ReportViewer1.LocalReport.DataSources.Add(rds18)

                dtT8 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
                Dim rds19 As New ReportDataSource("ScheduleNote_Fourth1", dtT8)
                ReportViewer1.LocalReport.DataSources.Add(rds19)

                dtT9 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "footNote")
                Dim rds20 As New ReportDataSource("ScheduleNote_Desc3", dtT9)
                ReportViewer1.LocalReport.DataSources.Add(rds20)

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/rptSchduleNote.rdlc")
                Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=ScheduleNote" + ".pdf")
                Response.BinaryWrite(RptViewer)
                Response.Flush()
                Response.End()
                'ReportViewer1.LocalReport.Refresh()
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnFreeze_Click(sender As Object, e As EventArgs) Handles btnFreeze.Click
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                lblModalValidationMsg.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            ElseIf lstbranchSchedTemp.SelectedValue = 0 Then
                lblModalValidationMsg.Text = "Select Branch"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            lblfrz.Text = "Click 'Yes' to Add P&L Amount to Reserves and Surplus for next year Click, 'No' to Cancel "
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgTypefrz').addClass('alert alert-warning');$('#ModalExcelValidationfrz').modal('show');", True)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnFinalAuditReport_Click(sender As Object, e As EventArgs) Handles btnFinalAuditReport.Click
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                BindAuditTypes()
                BindAllTypeReports()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myFinalAuditReportModal').modal('show')", True)
            Else
                lblError.Text = "Select Customer" : lblModalValidationMsg.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub chkSelectAllTypeReports_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectReport As New CheckBox
        Try
            lblError.Text = "" : lblFinalAuditReportError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To gvAllTypeReports.Rows.Count - 1
                    chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                    chkSelectReport.Checked = True
                Next
            Else
                For i = 0 To gvAllTypeReports.Rows.Count - 1
                    chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                    chkSelectReport.Checked = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myFinalAuditReportModal').modal('show')", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllTypeReports_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAllTypeReports_PreRender(sender As Object, e As EventArgs) Handles gvAllTypeReports.PreRender
        Try
            If gvAllTypeReports.Rows.Count > 0 Then
                gvAllTypeReports.UseAccessibleHeader = True
                gvAllTypeReports.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAllTypeReports.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvYearMonth_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAuditTypes()
        Dim objclsAuditChecklist As New clsAuditChecklist
        Dim dt As New DataTable
        Try
            dt = objclsAuditChecklist.LoadScheduledAuditTypeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            ddlAuditType.DataSource = dt
            ddlAuditType.DataTextField = "Name"
            ddlAuditType.DataValueField = "PKID"
            ddlAuditType.DataBind()
            ddlAuditType.Items.Insert(0, "Select Audit Type")
            If dt.Rows.Count = 1 Then
                ddlAuditType.SelectedIndex = 1
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditTypes" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllTypeReports()
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("Name")

            dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "Engagement Letter according to the Task selected" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "2" : dr("Name") = "Profile / Information about the Auditee" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "3" : dr("Name") = "Audit Report according to the Task selected" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "Review Ledger report (Audit workpapers) according to the Task selected" : dt.Rows.Add(dr)
            gvAllTypeReports.DataSource = dt
            gvAllTypeReports.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click
        Dim iCheckTypeReport As Integer = 0
        Dim lblReportID As New Label
        Dim chkSelectReport As New CheckBox
        Try
            lblError.Text = "" : lblFinalAuditReportError.Text = ""
            If ddlAuditType.SelectedIndex = 0 Then
                lblFinalAuditReportError.Text = "Select Audit Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myFinalAuditReportModal').modal('show')", True)
                Exit Sub
            End If
            For i = 0 To gvAllTypeReports.Rows.Count - 1
                chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                If chkSelectReport.Checked = True Then
                    iCheckTypeReport = 1
                End If
            Next
            If iCheckTypeReport = 0 Then
                lblFinalAuditReportError.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myFinalAuditReportModal').modal('show')", True)
                Exit Try
            End If

            Dim mimeType As String = "application/pdf"
            Dim objCust As New clsCustDetails
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/FinalReport.rdlc")
            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)

            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text.ToString())}
            ReportViewer1.LocalReport.SetParameters(Customer)

            For i = 0 To gvAllTypeReports.Rows.Count - 1
                chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                lblReportID = gvAllTypeReports.Rows(i).FindControl("lblReportID")
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 1 Then
                    Dim dt As New DataTable
                    dt = objclsSchduleReport.GetLOEReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditType.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        Dim RefNo As ReportParameter() = New ReportParameter() {New ReportParameter("RefNo", dt.Rows(dt.Rows.Count - 1)("LOE_Name").ToString())}
                        ReportViewer1.LocalReport.SetParameters(RefNo)

                        Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", dt.Rows(dt.Rows.Count - 1)("CUST_NAME").ToString())}
                        ReportViewer1.LocalReport.SetParameters(Director)

                        Dim Function1 As ReportParameter() = New ReportParameter() {New ReportParameter("Function1", dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString())}
                        ReportViewer1.LocalReport.SetParameters(Function1)

                        Dim Year As ReportParameter() = New ReportParameter() {New ReportParameter("Year", dt.Rows(dt.Rows.Count - 1)("YMS_ID").ToString())}
                        ReportViewer1.LocalReport.SetParameters(Year)

                        Dim Fees As ReportParameter() = New ReportParameter() {New ReportParameter("Fees", dt.Rows(dt.Rows.Count - 1)("LOE_ProfessionalFees").ToString())}
                        ReportViewer1.LocalReport.SetParameters(Fees)

                        If dt.Rows(dt.Rows.Count - 1)("LOET_StdsInternalAudit").ToString() = "" Then
                            Dim ResponsibilitiesOftheAuditor As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOftheAuditor", " ")}
                            ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOftheAuditor)
                        Else
                            Dim ResponsibilitiesOftheAuditor As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOftheAuditor", dt.Rows(dt.Rows.Count - 1)("LOET_StdsInternalAudit").ToString())}
                            ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOftheAuditor)
                        End If

                        If dt.Rows(dt.Rows.Count - 1)("LOET_Deliverable").ToString() = "" Then
                            Dim ObjectiveAndScopeOfAudit As ReportParameter() = New ReportParameter() {New ReportParameter("ObjectiveAndScopeOfAudit", " ")}
                            ReportViewer1.LocalReport.SetParameters(ObjectiveAndScopeOfAudit)
                        Else
                            Dim ObjectiveAndScopeOfAudit As ReportParameter() = New ReportParameter() {New ReportParameter("ObjectiveAndScopeOfAudit", dt.Rows(dt.Rows.Count - 1)("LOET_Deliverable").ToString())}
                            ReportViewer1.LocalReport.SetParameters(ObjectiveAndScopeOfAudit)
                        End If

                        If dt.Rows(dt.Rows.Count - 1)("LOET_Responsibilities").ToString() = "" Then
                            Dim Reporting As ReportParameter() = New ReportParameter() {New ReportParameter("Reporting", " ")}
                            ReportViewer1.LocalReport.SetParameters(Reporting)
                        Else
                            Dim Reporting As ReportParameter() = New ReportParameter() {New ReportParameter("Reporting", dt.Rows(dt.Rows.Count - 1)("LOET_Responsibilities").ToString())}
                            ReportViewer1.LocalReport.SetParameters(Reporting)
                        End If

                        If dt.Rows(dt.Rows.Count - 1)("LOET_Infrastructure").ToString() = "" Then
                            Dim ResponsibilitiesOfManagement As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOfManagement", " ")}
                            ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOfManagement)
                        Else
                            Dim ResponsibilitiesOfManagement As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOfManagement", dt.Rows(dt.Rows.Count - 1)("LOET_Infrastructure").ToString())}
                            ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOfManagement)
                        End If

                        If dt.Rows(dt.Rows.Count - 1)("LOET_General").ToString() = "" Then
                            Dim General As ReportParameter() = New ReportParameter() {New ReportParameter("General", " ")}
                            ReportViewer1.LocalReport.SetParameters(General)
                        Else
                            Dim General As ReportParameter() = New ReportParameter() {New ReportParameter("General", dt.Rows(dt.Rows.Count - 1)("LOET_General").ToString())}
                            ReportViewer1.LocalReport.SetParameters(General)
                        End If

                        If dt.Rows(dt.Rows.Count - 1)("LOET_NDA").ToString() = "" Then
                            Dim NonDisclousure As ReportParameter() = New ReportParameter() {New ReportParameter("NonDisclousure", " ")}
                            ReportViewer1.LocalReport.SetParameters(NonDisclousure)
                        Else
                            Dim NonDisclousure As ReportParameter() = New ReportParameter() {New ReportParameter("NonDisclousure", dt.Rows(dt.Rows.Count - 1)("LOET_NDA").ToString())}
                            ReportViewer1.LocalReport.SetParameters(NonDisclousure)
                        End If
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 2 Then
                    Dim dt As New DataTable
                    dt = objclsSchduleReport.GetCustReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dt = objCust.LoadCustInformationAuditeeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearName, ddlCustomerName.SelectedValue, dt.Rows(dt.Rows.Count - 1)("CUST_NAME").ToString(), dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString(), objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(dt.Rows.Count - 1)("CUST_CommitmentDate"), "D"), dt.Rows(dt.Rows.Count - 1)("CDET_PRODUCTSMANUFACTURED").ToString())
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 3 Then
                    Dim objclsStandardAudit As New clsStandardAudit
                    Dim dt As New DataTable
                    Dim dt1 As New DataTable
                    Dim dt2 As New DataTable
                    dt = objclsSchduleReport.GetConductAuditReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditType.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dt1 = objclsStandardAudit.LoadStandardAuditConductAuditReport(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(dt.Rows.Count - 1)("SA_ID"))
                        dt2 = objclsStandardAudit.LoadStandardAuditConductAuditObservationsReport(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(dt.Rows.Count - 1)("SA_ID"))

                        Dim rds As New ReportDataSource("DataSet2", dt1)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                        Dim rds1 As New ReportDataSource("DataSet3", dt2)
                        ReportViewer1.LocalReport.DataSources.Add(rds1)

                        Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", dt.Rows(dt.Rows.Count - 1)("SA_AuditNo").ToString())}
                        ReportViewer1.LocalReport.SetParameters(AuditNo)
                        Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString())}
                        ReportViewer1.LocalReport.SetParameters(AuditType)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 4 Then
                    Dim dt As New DataTable
                    Dim dt1 As New DataTable
                    Dim dt2 As New DataTable
                    Dim obclsUL As New clsUploadLedger
                    Dim objclsStandardAudit As New clsStandardAudit

                    dt = obclsUL.LoadLedgerObservationsCommentsReports(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet4", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)
                    End If
                End If
            Next

            ReportViewer1.LocalReport.Refresh()
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=FinalAuitdReport.pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnAccountPolicies_Click(sender As Object, e As EventArgs) Handles btnAccountPolicies.Click
        Try
            Response.Redirect(String.Format("AccountPolicies.aspx?"), False)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim dtpartners As New DataTable
        Dim dtprev As New DataTable
        Dim dtdirectors As New DataTable
        Dim dt4 As DataTable
        Dim dtcustdetails As DataTable
        Dim dtcompanydetails As DataTable
        Dim iSelectedLocation As Integer
        Dim sSelectedBranches As String = ""
        Dim sSelectedSHeading As String = "0"
        Dim dtCusAmtType As DataTable
        Dim iAmtType As Integer = 0
        Dim iRoundOff As Integer = 0
        Dim iCustId As String = 0
        Dim dSubItemId As String
        Dim sSelectedSItems As String = "0"
        Dim dtstatue As New DataTable
        Dim objDBL As New DBHelper
        Dim sOrgType As String
        Dim dt As New System.Data.DataSet
        Dim dt1, dt2 As New DataTable
        Dim mimeType As String = Nothing
        Dim dtCash As New DataTable
        Dim dtCash1 As New DataTable
        Dim dtCash2 As New DataTable
        Dim dtCash3 As New DataTable
        Dim dtCash4 As New DataTable
        Dim CurrentAmmount As Decimal
        Dim TotaladjustmentsCurrent As Decimal = 0
        Dim TotaladjustmentsPrev As Decimal = 0
        Dim TotalCurrentOperativeActivities As Decimal = 0
        Dim TotalPrevOperativeActivities As Decimal = 0
        Dim TotalCurrentInvestingActivities As Decimal = 0
        Dim TotalPrevInvestingActivities As Decimal = 0
        Dim TotalCurrentFinanceActivities As Decimal = 0
        Dim TotalPrevFinanceActivities As Decimal = 0
        Dim TotalManualCurrent As Decimal = 0
        Dim TotalManualPrev As Decimal = 0
        Dim TotalCurrentABC As Decimal = 0
        Dim TotalPrevABC As Decimal = 0
        Dim TotalCurrentexchange_rate_changes As Decimal = 0
        Dim TotalPrevexchange_rate_changes As Decimal = 0
        Dim dtRatios As New DataTable
        Dim dtfixedAssettype1, dtfixedAssettype2, dtCA As New DataTable
        Dim dtS1 As DataTable, dtS2 As DataTable, dtS3 As DataTable, dtS4 As DataTable
        Dim dtT1 As DataTable, dtT2 As DataTable, dtT3 As DataTable, dtT4 As DataTable, dtT5 As DataTable, dtT6 As DataTable
        Dim dtT7 As DataTable, dtT8 As DataTable, dtT9 As DataTable, dtAcountingpolices As New DataTable
        Dim iLedgerAmt As Integer = 0
        Dim TotalCurrentbegining_of_the_year As Decimal = 0
        Dim TotalPrevbegining_of_the_year As Decimal = 0
        Try
            lblError.Text = ""
            ReportViewer2.Reset()
            pnlPnLAmt.Visible = False
            'ddlSubheading.Visible = False
            'lblSubheading.Visible = False
            lblError.Text = ""
            btnFreeze.Visible = False
            If ddlCustomerName.SelectedIndex > 0 Then
                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If
                For i = 0 To lstbranchSchedTemp.Items.Count - 1
                    If lstbranchSchedTemp.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedBranches = sSelectedBranches & "," & lstbranchSchedTemp.Items(i).Value
                    End If
                Next
                If lstbranchSchedTemp.Items.Count = 0 Then
                    sSelectedBranches = "0"
                End If
                If sSelectedBranches.StartsWith(",") Then
                    sSelectedBranches = sSelectedBranches.Remove(0, 1)
                End If
                If sSelectedBranches.EndsWith(",") Then
                    sSelectedBranches = sSelectedBranches.Remove(Len(sSelectedBranches) - 1, 1)
                End If
                If sSelectedBranches = "" Then
                    lblError.Text = "Select Branch for Customer"
                    Exit Sub
                End If
                If chkBxExcel.Checked = True Then
                    iLedgerAmt = 1
                Else
                    iLedgerAmt = 0
                End If
                dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If ddlPartners.SelectedIndex > 0 Then
                    dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, ddlPartners.SelectedValue)
                Else
                    dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)
                End If

                dtdirectors = objclsSchduleReport.LoadCustomerdirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                Dim sSelecteddirectorsandPartner As String = ""
                For i = 0 To lstPartsandDirectors.Items.Count - 1
                    If lstPartsandDirectors.Items(i).Selected = True Then
                        sSelecteddirectorsandPartner = sSelecteddirectorsandPartner & "," & lstPartsandDirectors.Items(i).Value
                    End If
                Next
                If sSelecteddirectorsandPartner.StartsWith(",") Then
                    sSelecteddirectorsandPartner = sSelecteddirectorsandPartner.Remove(0, 1)
                End If

                If sSelecteddirectorsandPartner = "" Then
                    dtstatue = New DataTable()
                Else
                    sOrgType = objclsSchduleReport.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If sOrgType = "Partnership firms" Then
                        dtstatue = objclsSchduleReport.Loadpartner1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelecteddirectorsandPartner, "")
                    Else
                        dtstatue = objclsSchduleReport.LoadDirector1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelecteddirectorsandPartner, "")
                    End If
                End If
                dSubItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select CUSt_BranchId  from SAD_CUSTOMER_MASTER where CUST_Id=" & ddlCustomerName.SelectedValue & "")
                dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)

                'Balance Sheet And PnL final Report
                btnFreeze.Visible = True
                ReportViewer2.Reset()
                sSession.PandLAmount = 0
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 4, 1, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                sSession.PandLAmount = objclsSchduleReport.getPnl()
                Session("AllSession") = sSession

                'If chkBxExcel.Checked = True Then
                '    Dim DVZRBADetailsBal As New DataView(dt4)
                '    DVZRBADetailsBal.RowFilter = "(HeaderSLNo = '0.00' And PrevyearTotoal = '0.00')"
                '    dt4 = DVZRBADetailsBal.ToTable
                '    If dt4.Rows.Count > 0 Then
                '        For i = 0 To dt4.Rows.Count - 1
                '            dt4.Rows(i)("SrNo") = i + 1
                '        Next
                '        dt4.AcceptChanges()
                '    End If
                'End If


                Dim rds1 As New ReportDataSource("DataSet1", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds1)
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 3, 1, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                'If chkBxExcel.Checked = True Then
                '    Dim DVZRBADetailsBal As New DataView(dt4)
                '    DVZRBADetailsBal.RowFilter = "((HeaderSLNo <> '0.00' And PrevyearTotoal <> '0.00'))"
                '    dt4 = DVZRBADetailsBal.ToTable
                '    If dt4.Rows.Count > 0 Then
                '        For i = 0 To dt4.Rows.Count - 1
                '            dt4.Rows(i)("SrNo") = i + 1
                '        Next
                '        dt4.AcceptChanges()
                '    End If
                'End If
                Dim rds2 As New ReportDataSource("DataSet2", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds2)
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 4, 2, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)


                Dim rds3 As New ReportDataSource("DataSet3", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds3)
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 3, 2, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                Dim rds4 As New ReportDataSource("DataSet4", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds4)


                'Cash Flow Report
                dtCash = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 1, ddlFinancialYear.SelectedValue)
                Dim PrevAmount As Decimal = 0
                Dim lblTotalCurrentcategory1 As New Label
                Dim lblTotalPrevcategory1 As New Label
                If dtCash.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblTotalCurrentcategory1.Text = CurrentAmmount
                    lblTotalPrevcategory1.Text = PrevAmount
                    dtCash(0)("Particulers") = "<b>A.Cash flow from operating activities</b>"
                    dtCash(2)("Particulers") = "<i>Adjustment for:</i>"
                End If
                Dim lblCurrentOpratingTotal As New Label
                Dim lblPrevOpratingTotal As New Label
                dtCash1 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 2, ddlFinancialYear.SelectedValue)
                If dtCash1.Rows.Count > 0 Then
                    dtCash1(0)("Particulers") = "<i>Changes in working capital:</i>"
                    dtCash1(1)("Particulers") = "<i>Adjustments for (increase) / decrease in operating assets:</i>"
                    For i = 0 To dtCash1.Rows.Count - 1
                        If IsDBNull(dtCash1.Rows(i)("CurrentAmmount")) = False Then
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + dtCash1.Rows(i)("CurrentAmmount")
                        Else
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + 0
                        End If
                        If IsDBNull(dtCash1.Rows(i)("PreviesAmount")) = False Then
                            TotaladjustmentsPrev = TotaladjustmentsPrev + dtCash1.Rows(i)("PreviesAmount")
                        Else
                            TotaladjustmentsPrev = TotaladjustmentsPrev + 0
                        End If
                        If dtCash1(i)("Particulers") = "Adjustments for increase / (decrease) in operating liabilities:" Then
                            dtCash1(i)("Particulers") = "<i>Adjustments for increase / (decrease) in operating liabilities:</i>"
                        End If
                        If dtCash1.Rows(i)("Particulers") = "Operating profit / (loss) after working capital changes" Then
                            dtCash1.Rows(i)("CurrentAmmount") = TotaladjustmentsCurrent + Val(lblTotalCurrentcategory1.Text)
                            dtCash1.Rows(i)("PreviesAmount") = TotaladjustmentsPrev + Val(lblTotalPrevcategory1.Text)
                        ElseIf dtCash1(i)("Particulers") = "Cash generated from operations" Then
                            TotalCurrentOperativeActivities = TotaladjustmentsCurrent + CurrentAmmount
                            TotalPrevOperativeActivities = TotaladjustmentsPrev + PrevAmount
                            dtCash1.Rows(i)("CurrentAmmount") = TotalCurrentOperativeActivities
                            dtCash1.Rows(i)("PreviesAmount") = TotalPrevOperativeActivities
                        ElseIf dtCash1(i)("Particulers") = "Net income tax (paid) / refunds (net)" Then
                            TotalManualCurrent = dtCash1.Rows(i)("CurrentAmmount")
                            TotalManualPrev = dtCash1.Rows(i)("PreviesAmount")
                            'ElseIf dt1(i)("Particulers") = "Net cash generated from/ (used in) operating activities" Then
                            '    dt1.Rows(i)("CurrentAmmount") = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                            '    dt1.Rows(i)("PreviesAmount") = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                        End If


                    Next
                    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                End If
                Dim lblCurrentinvestingactivities As New Label
                Dim lblprevinvestingactivities As New Label
                dtCash2 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 3, ddlFinancialYear.SelectedValue)
                If dtCash2.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash2.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash2.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentinvestingactivities.Text = CurrentAmmount
                    lblprevinvestingactivities.Text = PrevAmount
                End If

                Dim lblCurrentfinancingactivities As New Label
                Dim lblPrevfinancingactivities As New Label
                dtCash3 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 4, ddlFinancialYear.SelectedValue)
                If dtCash3.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash3.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash3.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentfinancingactivities.Text = CurrentAmmount
                    lblPrevfinancingactivities.Text = PrevAmount
                End If
                dtCash4 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 5, ddlFinancialYear.SelectedValue)
                If dtCash4.Rows.Count > 0 Then
                    For i = 0 To dtCash4.Rows.Count - 1
                        If dtCash4.Rows(i)("Particulers") = "Net increase (derease) in cash and cash equivalents befor effect of exchange rate changes" Then
                            TotalCurrentexchange_rate_changes = TotalCurrentOperativeActivities + TotalCurrentInvestingActivities + TotalCurrentFinanceActivities
                            TotalPrevexchange_rate_changes = TotalPrevOperativeActivities + TotalPrevInvestingActivities + TotalPrevFinanceActivities
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentexchange_rate_changes
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevexchange_rate_changes
                        ElseIf dtCash4(i)("Particulers") = "Net increase / (decrease) in Cash and cash equivalents (A+B+C)" Then
                            TotalCurrentABC = Math.Round(Convert.ToDouble(Val(lblCurrentOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentfinancingactivities.Text)), 2)
                            TotalPrevABC = Math.Round(Convert.ToDouble(Val(lblPrevOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblprevinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblPrevfinancingactivities.Text)), 2)
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentABC
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at begining of the year" Then
                            TotalCurrentbegining_of_the_year = Val(dtCash4.Rows(i)("CurrentAmmount")) + TotalCurrentABC
                            TotalPrevbegining_of_the_year = Val(dtCash4.Rows(i)("PreviesAmount")) + TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at Closing of the year" Then
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentbegining_of_the_year
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevbegining_of_the_year
                            'dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                            'dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)
                            'If dtIncome.Rows.Count > 0 Then
                            '    dCYProfiTAmt = dtIncome(0)("Dc1")
                            '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                            'Else
                            '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            'End If
                            'Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            '    Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        End If
                    Next
                End If

                'Partner Shares
                sOrgType = objclsSchduleReport.LoadOrgtypeId(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                If sOrgType = 71 Then
                    Dim lblptnr As New Label
                    Dim yearParts As String() = ddlFinancialYear.SelectedItem.Text.Split("-"c)

                    If yearParts.Length = 2 AndAlso yearParts(0).Length = 2 AndAlso yearParts(1).Length = 2 Then
                        ' Convert the year parts to integers
                        Dim year1 As Integer
                        Dim year2 As Integer
                        If Integer.TryParse(yearParts(0), year1) AndAlso Integer.TryParse(yearParts(1), year2) Then
                            ' Convert the two-digit years to four-digit years
                            Dim fullYear1 As String = If(year1 < 50, "20" & year1.ToString("00"), "19" & year1.ToString("00"))
                            Dim fullYear2 As String = If(year2 < 50, "20" & year2.ToString("00"), "19" & year2.ToString("00"))

                            ' Concatenate the full years with a "-" separator
                            Dim sFin2 As String = fullYear1 & "-" & fullYear2
                            Dim iFin1 As Integer = Val(sFin2)
                            Dim dtPartnerShares As DataTable
                            dtPartnerShares = objclsPartnerFund.LoadAllPartnershipFirms(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iFin1, sFin2, "Yes")
                            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAPDF.rdlc")
                            Dim rds32 As New ReportDataSource("DataSet20", dtPartnerShares)
                            ReportViewer2.LocalReport.DataSources.Add(rds32)
                        Else
                            lblError.Text = "Invalid year format."
                        End If
                    Else
                        lblError.Text = "Invalid year format."
                    End If
                Else
                    Dim dtPartnerShares As New DataTable
                    ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAPDF.rdlc")
                    Dim rds32 As New ReportDataSource("DataSet20", dtPartnerShares)
                    ReportViewer2.LocalReport.DataSources.Add(rds32)

                End If


                'Account Ratio
                dtRatios = objclsAccRatios.LoadAccRatio(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAPDF.rdlc")


                Dim rds5 As New ReportDataSource("DataSet5", dtCash)
                ReportViewer2.LocalReport.DataSources.Add(rds5)
                Dim rds6 As New ReportDataSource("DataSet6", dtCash1)
                ReportViewer2.LocalReport.DataSources.Add(rds6)
                Dim rds7 As New ReportDataSource("DataSet7", dtCash2)
                ReportViewer2.LocalReport.DataSources.Add(rds7)
                Dim rds8 As New ReportDataSource("DataSet8", dtCash3)
                ReportViewer2.LocalReport.DataSources.Add(rds8)
                Dim rds9 As New ReportDataSource("DataSet9", dtCash4)
                ReportViewer2.LocalReport.DataSources.Add(rds9)
                Dim rds10 As New ReportDataSource("DataSet10", dtRatios)
                ReportViewer2.LocalReport.DataSources.Add(rds10)

                'Schedule Notes
                Dim dt11 As New DataTable
                dt11 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
                Dim rds11 As New ReportDataSource("DataSet11", dt11)
                ReportViewer2.LocalReport.DataSources.Add(rds11)

                Dim dt12 As New DataTable
                dt12 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
                Dim rds12 As New ReportDataSource("DataSet12", dt12)
                ReportViewer2.LocalReport.DataSources.Add(rds12)
                Dim dt13 As New DataTable
                dt13 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
                Dim rds13 As New ReportDataSource("DataSet13", dt13)
                ReportViewer2.LocalReport.DataSources.Add(rds13)

                Dim dt14 As New DataTable
                dt4 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
                Dim rds14 As New ReportDataSource("DataSet14", dt14)
                ReportViewer2.LocalReport.DataSources.Add(rds14)

                Dim dt15 As New DataTable
                dt15 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
                Dim rds15 As New ReportDataSource("DataSet15", dt15)
                ReportViewer2.LocalReport.DataSources.Add(rds15)

                Dim dt16 As New DataTable
                dt16 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
                Dim rds16 As New ReportDataSource("DataSet16", dt16)
                ReportViewer2.LocalReport.DataSources.Add(rds16)

                dtS1 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SF")
                Dim rds17 As New ReportDataSource("DataSet17", dtS1)
                ReportViewer2.LocalReport.DataSources.Add(rds17)

                dtS2 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SS")
                Dim rds18 As New ReportDataSource("ScheduleNote_Second2", dtS2)
                ReportViewer2.LocalReport.DataSources.Add(rds18)

                dtS3 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "ST")
                Dim rds19 As New ReportDataSource("ScheduleNote_Second3", dtS3)
                ReportViewer2.LocalReport.DataSources.Add(rds19)

                dtS4 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SV")
                Dim rds20 As New ReportDataSource("ScheduleNote_Second4", dtS4)
                ReportViewer2.LocalReport.DataSources.Add(rds20)

                dtT1 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
                Dim rds21 As New ReportDataSource("ScheduleNote_Third1", dtT1)
                ReportViewer2.LocalReport.DataSources.Add(rds21)

                dtT2 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                Dim rds22 As New ReportDataSource("ScheduleNote_Third2", dtT2)
                ReportViewer2.LocalReport.DataSources.Add(rds22)

                dtT2 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "cEquity")
                Dim rds23 As New ReportDataSource("ScheduleNote_Desc", dtT2)
                ReportViewer2.LocalReport.DataSources.Add(rds23)

                dtT3 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "dPref")
                Dim rds24 As New ReportDataSource("ScheduleNote_Desc1", dtT3)
                ReportViewer2.LocalReport.DataSources.Add(rds24)

                dtT4 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEE")
                Dim rds25 As New ReportDataSource("ScheduleNote_Third3", dtT4)
                ReportViewer2.LocalReport.DataSources.Add(rds25)

                dtT5 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                Dim rds26 As New ReportDataSource("ScheduleNote_Third4", dtT5)
                ReportViewer2.LocalReport.DataSources.Add(rds26)

                dtT6 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "fShares")
                Dim rds27 As New ReportDataSource("ScheduleNote_Desc2", dtT6)
                ReportViewer2.LocalReport.DataSources.Add(rds27)

                dtT7 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
                Dim rds28 As New ReportDataSource("ScheduleNote_Fourth", dtT7)
                ReportViewer2.LocalReport.DataSources.Add(rds28)

                dtT8 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
                Dim rds29 As New ReportDataSource("ScheduleNote_Fourth1", dtT8)
                ReportViewer2.LocalReport.DataSources.Add(rds29)

                dtT9 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "footNote")
                Dim rds30 As New ReportDataSource("ScheduleNote_Desc3", dtT9)
                ReportViewer2.LocalReport.DataSources.Add(rds30)


                'Accounting policies
                dtAcountingpolices = objAccountpolicies.LoadReportContentToGrid(sSession.AccessCode, sSession.AccessCodeID, 1)

                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAPDF.rdlc")
                Dim rds31 As New ReportDataSource("DataSet18", dtAcountingpolices)
                ReportViewer2.LocalReport.DataSources.Add(rds31)

                Dim rdsstatue As New ReportDataSource("DataSet19", dtstatue)
                ReportViewer2.LocalReport.DataSources.Add(rdsstatue)


                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Audit Documentation For the Year" & ", " & objclsGeneralFunctions.Get4DigitCurrentFinancialYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue))}
                ReportViewer2.LocalReport.SetParameters(REPORT_DEF)
                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                ReportViewer2.LocalReport.SetParameters(Comp_Name)
                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                ReportViewer2.LocalReport.SetParameters(Company_Address)
                Dim TotalCurrentcategory1 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory1", lblTotalCurrentcategory1.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory1)
                Dim TotalPrevcategory1 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory1", lblTotalPrevcategory1.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory1)
                Dim TotalCurrentcategory2 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory2", lblCurrentOpratingTotal.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory2)
                Dim TotalPrevcategory2 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory2", lblPrevOpratingTotal.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory2)
                Dim TotalCurrentcategory3 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory3", lblCurrentinvestingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory3)
                Dim TotalPrevcategory3 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory3", lblprevinvestingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory3)
                Dim TotalCurrentcategory4 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory4", lblCurrentfinancingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory4)
                Dim TotalPrevcategory4 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory4", lblPrevfinancingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory4)
                Dim iFCurrentYear As ReportParameter() = New ReportParameter() {New ReportParameter("iFCurrentYear", objclsGeneralFunctions.Get4DigitCurrentFinancialYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue))}
                ReportViewer2.LocalReport.SetParameters(iFCurrentYear)
                Dim iFPrevYear As ReportParameter() = New ReportParameter() {New ReportParameter("iFPrevYear", objclsGeneralFunctions.Get4DigitCurrentFinancialYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                ReportViewer2.LocalReport.SetParameters(iFPrevYear)
                Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
                ReportViewer2.LocalReport.SetParameters(CompRegNo)
                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
                ReportViewer2.LocalReport.SetParameters(companydetailsName)
                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
                ReportViewer2.LocalReport.SetParameters(Company_Code)
                If ddlPartners.SelectedIndex > 0 Then
                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                    ReportViewer2.LocalReport.SetParameters(Partners_Name)
                    If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                        ReportViewer2.LocalReport.SetParameters(Partners_MNum)
                    Else
                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                        ReportViewer2.LocalReport.SetParameters(Partners_MNum)
                    End If
                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                    ReportViewer2.LocalReport.SetParameters(org_name)
                Else
                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                    ReportViewer2.LocalReport.SetParameters(Partners_Name)
                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                    ReportViewer2.LocalReport.SetParameters(Partners_MNum)
                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                    ReportViewer2.LocalReport.SetParameters(org_name)
                End If
                Dim sCust_fontstyle As String = ""
                Dim sCust_border As Integer = 0
                Dim dtformat As New DataTable
                dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtformat(0)("CF_ID") <> 0 Then
                    sCust_fontstyle = dtformat(0)("CF_name")
                    sCust_border = dtformat(0)("CUST_rptBorder")
                End If
                Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                ReportViewer2.LocalReport.SetParameters(sFontstyle)
                Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                ReportViewer2.LocalReport.SetParameters(sborderstyle)
                Dim sUDINNo As ReportParameter() = New ReportParameter() {New ReportParameter("sUDINNo", txtUDINNo.Text)}
                ReportViewer2.LocalReport.SetParameters(sUDINNo)
                Dim sUDIINDate As ReportParameter() = New ReportParameter() {New ReportParameter("sUDIINDate", txtUDINDate.Text)}
                ReportViewer2.LocalReport.SetParameters(sUDIINDate)
                Dim CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                ReportViewer2.LocalReport.SetParameters(CYear)
                Dim PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                ReportViewer2.LocalReport.SetParameters(PYear)


                'Dim Category As ReportParameter() = New ReportParameter() {New ReportParameter("Category", ddlcategory.SelectedItem.Text)}
                'ReportViewer2.LocalReport.SetParameters(Category)

                Dim sAmtIn As String
                sAmtIn = "In Rupees"
                Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                ReportViewer2.LocalReport.SetParameters(sAmountTxt)

                Dim datatable1 As New DataTable
                Dim DataTable2 As New DataTable
                Dim dataset As DataSet = objUT.GetTotal(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelectedBranches)
                'Dim dtTrade As DataTable = objUT.GetCTrDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlcategory.SelectedIndex, ddlbranchName.SelectedValue, ddlOthType.SelectedIndex)
                datatable1 = dataset.Tables(0)
                DataTable2 = dataset.Tables(1)
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAPDF.rdlc")

                Dim rds34 As New ReportDataSource("DataSet22", datatable1)
                ReportViewer2.LocalReport.DataSources.Add(rds34)
                Dim rds35 As New ReportDataSource("DataSet21", DataTable2)
                ReportViewer2.LocalReport.DataSources.Add(rds35)

                Dim pdfViewer As Byte() = ReportViewer2.LocalReport.Render("PDF")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=Final Audit Report  " + ddlCustomerName.SelectedItem.Text + " " + ddlFinancialYear.SelectedItem.Text + ".pdf")
                Response.BinaryWrite(pdfViewer)
                Response.Flush()
                Response.End()
                lblError.Text = "Select Report Type"
                ddlRepType.Focus() : ddlCustomerName.SelectedIndex = 0
                lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
                'ddlCustomerName_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "Select Customer"
                lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dtpartners As New DataTable
        Dim dtprev As New DataTable
        Dim dtdirectors As New DataTable
        Dim dt4 As DataTable
        Dim dtcustdetails As DataTable
        Dim dtcompanydetails As DataTable
        Dim iSelectedLocation As Integer
        Dim sSelectedBranches As String = ""
        Dim sSelectedSHeading As String = "0"
        Dim dtCusAmtType As DataTable
        Dim iAmtType As Integer = 0
        Dim iRoundOff As Integer = 0
        Dim iCustId As String = 0
        Dim dSubItemId As String
        Dim sSelectedSItems As String = "0"
        Dim dtstatue As New DataTable
        Dim objDBL As New DBHelper
        Dim sOrgType As String
        Dim dt As New System.Data.DataSet
        Dim dtfixedAssettype1, dtfixedAssettype2, dtCA As New DataTable
        Dim dt1, dt2 As New DataTable
        Dim mimeType As String = Nothing
        Dim dtCash As New DataTable
        Dim dtCash1 As New DataTable
        Dim dtCash2 As New DataTable
        Dim dtCash3 As New DataTable
        Dim dtCash4 As New DataTable
        Dim CurrentAmmount As Decimal
        Dim TotaladjustmentsCurrent As Decimal = 0
        Dim TotaladjustmentsPrev As Decimal = 0
        Dim TotalCurrentOperativeActivities As Decimal = 0
        Dim TotalPrevOperativeActivities As Decimal = 0
        Dim TotalCurrentInvestingActivities As Decimal = 0
        Dim TotalPrevInvestingActivities As Decimal = 0
        Dim TotalCurrentFinanceActivities As Decimal = 0
        Dim TotalPrevFinanceActivities As Decimal = 0
        Dim TotalManualCurrent As Decimal = 0
        Dim TotalManualPrev As Decimal = 0
        Dim TotalCurrentABC As Decimal = 0
        Dim TotalPrevABC As Decimal = 0
        Dim TotalCurrentexchange_rate_changes As Decimal = 0
        Dim TotalPrevexchange_rate_changes As Decimal = 0
        Dim dtRatios As New DataTable
        Dim dtS1 As DataTable, dtS2 As DataTable, dtS3 As DataTable, dtS4 As DataTable
        Dim dtT1 As DataTable, dtT2 As DataTable, dtT3 As DataTable, dtT4 As DataTable, dtT5 As DataTable, dtT6 As DataTable
        Dim dtT7 As DataTable, dtT8 As DataTable, dtT9 As DataTable, dtAcountingpolices As New DataTable
        Dim iLedgerAmt As Integer = 0
        Dim TotalCurrentbegining_of_the_year As Decimal = 0
        Dim TotalPrevbegining_of_the_year As Decimal = 0
        Try
            lblError.Text = ""
            ReportViewer2.Reset()
            pnlPnLAmt.Visible = False
            'ddlSubheading.Visible = False
            'lblSubheading.Visible = False
            lblError.Text = ""
            btnFreeze.Visible = False
            If ddlCustomerName.SelectedIndex > 0 Then
                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If
                For i = 0 To lstbranchSchedTemp.Items.Count - 1
                    If lstbranchSchedTemp.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedBranches = sSelectedBranches & "," & lstbranchSchedTemp.Items(i).Value
                    End If
                Next
                If lstbranchSchedTemp.Items.Count = 0 Then
                    sSelectedBranches = "0"
                End If
                If sSelectedBranches.StartsWith(",") Then
                    sSelectedBranches = sSelectedBranches.Remove(0, 1)
                End If
                If sSelectedBranches.EndsWith(",") Then
                    sSelectedBranches = sSelectedBranches.Remove(Len(sSelectedBranches) - 1, 1)
                End If
                If sSelectedBranches = "" Then
                    lblError.Text = "Select Branch for Customer"
                    Exit Sub
                End If
                If chkBxExcel.Checked = True Then
                    iLedgerAmt = 1
                Else
                    iLedgerAmt = 0
                End If
                dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If ddlPartners.SelectedIndex > 0 Then
                    dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, ddlPartners.SelectedValue)
                Else
                    dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)
                End If

                dtdirectors = objclsSchduleReport.LoadCustomerdirectors(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                Dim sSelecteddirectorsandPartner As String = ""
                For i = 0 To lstPartsandDirectors.Items.Count - 1
                    If lstPartsandDirectors.Items(i).Selected = True Then
                        sSelecteddirectorsandPartner = sSelecteddirectorsandPartner & "," & lstPartsandDirectors.Items(i).Value
                    End If
                Next
                If sSelecteddirectorsandPartner.StartsWith(",") Then
                    sSelecteddirectorsandPartner = sSelecteddirectorsandPartner.Remove(0, 1)
                End If

                If sSelecteddirectorsandPartner = "" Then
                    dtstatue = New DataTable()
                Else
                    sOrgType = objclsSchduleReport.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If sOrgType = "Partnership firms" Then
                        dtstatue = objclsSchduleReport.Loadpartner1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelecteddirectorsandPartner, "")
                    Else
                        dtstatue = objclsSchduleReport.LoadDirector1(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelecteddirectorsandPartner, "")
                    End If
                End If
                dSubItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select CUSt_BranchId  from SAD_CUSTOMER_MASTER where CUST_Id=" & ddlCustomerName.SelectedValue & "")
                dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)

                'Balance Sheet And PnL final Report
                btnFreeze.Visible = True
                ReportViewer2.Reset()
                sSession.PandLAmount = 0
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 4, 1, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                sSession.PandLAmount = objclsSchduleReport.getPnl()
                Session("AllSession") = sSession

                'If chkBxExcel.Checked = True Then
                '    Dim DVZRBADetailsBal As New DataView(dt4)
                '    DVZRBADetailsBal.RowFilter = "(HeaderSLNo = '0.00' And PrevyearTotoal = '0.00')"
                '    dt4 = DVZRBADetailsBal.ToTable
                '    If dt4.Rows.Count > 0 Then
                '        For i = 0 To dt4.Rows.Count - 1
                '            dt4.Rows(i)("SrNo") = i + 1
                '        Next
                '        dt4.AcceptChanges()
                '    End If
                'End If


                Dim rds1 As New ReportDataSource("DataSet1", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds1)
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 3, 1, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                'If chkBxExcel.Checked = True Then
                '    Dim DVZRBADetailsBal As New DataView(dt4)
                '    DVZRBADetailsBal.RowFilter = "((HeaderSLNo <> '0.00' And PrevyearTotoal <> '0.00'))"
                '    dt4 = DVZRBADetailsBal.ToTable
                '    If dt4.Rows.Count > 0 Then
                '        For i = 0 To dt4.Rows.Count - 1
                '            dt4.Rows(i)("SrNo") = i + 1
                '        Next
                '        dt4.AcceptChanges()
                '    End If
                'End If
                Dim rds2 As New ReportDataSource("DataSet2", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds2)
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 4, 2, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)


                Dim rds3 As New ReportDataSource("DataSet3", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds3)
                dt4 = objclsSchduleReport.getBalSheet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, 3, 2, chkOnOff.Checked, iAmtType, sSelectedBranches, iRoundOff, sSelectedSHeading, sSelectedSItems, iLedgerAmt)
                Dim rds4 As New ReportDataSource("DataSet4", dt4)
                ReportViewer2.LocalReport.DataSources.Add(rds4)
                'Fixed Asset report

                'dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                'If (dtCusAmtType.Rows.Count > 0) Then
                '    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                '    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                'Else
                'iAmtType = 0
                '    iRoundOff = 0
                'End If

                dtfixedAssettype1 = objPhyReport.LoadComnyAct(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "", 0, ddlCustomerName.SelectedValue, "", iAmtType, iRoundOff)

                dtCA = objPhyReport.GetCompanyNameCity(sSession.AccessCode, sSession.AccessCodeID)


                If dtfixedAssettype1.Rows.Count = 0 Then
                    ReportViewer2.LocalReport.Refresh()
                Else
                    ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAEXCL.rdlc")
                    Dim rds33 As New ReportDataSource("DataSet21", dtfixedAssettype1)
                    ReportViewer2.LocalReport.DataSources.Add(rds33)
                End If


                'Cash Flow Report
                dtCash = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 1, ddlFinancialYear.SelectedValue)
                Dim PrevAmount As Decimal = 0
                Dim lblTotalCurrentcategory1 As New Label
                Dim lblTotalPrevcategory1 As New Label
                If dtCash.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblTotalCurrentcategory1.Text = CurrentAmmount
                    lblTotalPrevcategory1.Text = PrevAmount
                    dtCash(0)("Particulers") = "<b>A.Cash flow from operating activities</b>"
                    dtCash(2)("Particulers") = "<i>Adjustment for:</i>"
                End If
                Dim lblCurrentOpratingTotal As New Label
                Dim lblPrevOpratingTotal As New Label
                dtCash1 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 2, ddlFinancialYear.SelectedValue)
                If dtCash1.Rows.Count > 0 Then
                    dtCash1(0)("Particulers") = "<i>Changes in working capital:</i>"
                    dtCash1(1)("Particulers") = "<i>Adjustments for (increase) / decrease in operating assets:</i>"
                    For i = 0 To dtCash1.Rows.Count - 1
                        If IsDBNull(dtCash1.Rows(i)("CurrentAmmount")) = False Then
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + dtCash1.Rows(i)("CurrentAmmount")
                        Else
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + 0
                        End If
                        If IsDBNull(dtCash1.Rows(i)("PreviesAmount")) = False Then
                            TotaladjustmentsPrev = TotaladjustmentsPrev + dtCash1.Rows(i)("PreviesAmount")
                        Else
                            TotaladjustmentsPrev = TotaladjustmentsPrev + 0
                        End If
                        If dtCash1(i)("Particulers") = "Adjustments for increase / (decrease) in operating liabilities:" Then
                            dtCash1(i)("Particulers") = "<i>Adjustments for increase / (decrease) in operating liabilities:</i>"
                        End If
                        If dtCash1.Rows(i)("Particulers") = "Operating profit / (loss) after working capital changes" Then
                            dtCash1.Rows(i)("CurrentAmmount") = TotaladjustmentsCurrent + Val(lblTotalCurrentcategory1.Text)
                            dtCash1.Rows(i)("PreviesAmount") = TotaladjustmentsPrev + Val(lblTotalPrevcategory1.Text)
                        ElseIf dtCash1(i)("Particulers") = "Cash generated from operations" Then
                            TotalCurrentOperativeActivities = TotaladjustmentsCurrent + CurrentAmmount
                            TotalPrevOperativeActivities = TotaladjustmentsPrev + PrevAmount
                            dtCash1.Rows(i)("CurrentAmmount") = TotalCurrentOperativeActivities
                            dtCash1.Rows(i)("PreviesAmount") = TotalPrevOperativeActivities
                        ElseIf dtCash1(i)("Particulers") = "Net income tax (paid) / refunds (net)" Then
                            TotalManualCurrent = dtCash1.Rows(i)("CurrentAmmount")
                            TotalManualPrev = dtCash1.Rows(i)("PreviesAmount")
                            'ElseIf dt1(i)("Particulers") = "Net cash generated from/ (used in) operating activities" Then
                            '    dt1.Rows(i)("CurrentAmmount") = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                            '    dt1.Rows(i)("PreviesAmount") = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                        End If


                    Next
                    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                End If
                Dim lblCurrentinvestingactivities As New Label
                Dim lblprevinvestingactivities As New Label
                dtCash2 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 3, ddlFinancialYear.SelectedValue)
                If dtCash2.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash2.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash2.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentinvestingactivities.Text = CurrentAmmount
                    lblprevinvestingactivities.Text = PrevAmount
                End If

                Dim lblCurrentfinancingactivities As New Label
                Dim lblPrevfinancingactivities As New Label
                dtCash3 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 4, ddlFinancialYear.SelectedValue)
                If dtCash3.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash3.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash3.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentfinancingactivities.Text = CurrentAmmount
                    lblPrevfinancingactivities.Text = PrevAmount
                End If
                dtCash4 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, sSelectedBranches, 5, ddlFinancialYear.SelectedValue)
                If dtCash4.Rows.Count > 0 Then
                    For i = 0 To dtCash4.Rows.Count - 1
                        If dtCash4.Rows(i)("Particulers") = "Net increase (derease) in cash and cash equivalents befor effect of exchange rate changes" Then
                            TotalCurrentexchange_rate_changes = TotalCurrentOperativeActivities + TotalCurrentInvestingActivities + TotalCurrentFinanceActivities
                            TotalPrevexchange_rate_changes = TotalPrevOperativeActivities + TotalPrevInvestingActivities + TotalPrevFinanceActivities
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentexchange_rate_changes
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevexchange_rate_changes
                        ElseIf dtCash4(i)("Particulers") = "Net increase / (decrease) in Cash and cash equivalents (A+B+C)" Then
                            TotalCurrentABC = Math.Round(Convert.ToDouble(Val(lblCurrentOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentfinancingactivities.Text)), 2)
                            TotalPrevABC = Math.Round(Convert.ToDouble(Val(lblPrevOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblprevinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblPrevfinancingactivities.Text)), 2)
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentABC
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at begining of the year" Then
                            TotalCurrentbegining_of_the_year = Val(dtCash4.Rows(i)("CurrentAmmount")) + TotalCurrentABC
                            TotalPrevbegining_of_the_year = Val(dtCash4.Rows(i)("PreviesAmount")) + TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at Closing of the year" Then
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentbegining_of_the_year
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevbegining_of_the_year
                            'dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                            'dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)
                            'If dtIncome.Rows.Count > 0 Then
                            '    dCYProfiTAmt = dtIncome(0)("Dc1")
                            '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                            'Else
                            '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            'End If
                            'Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            '    Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        End If
                    Next
                End If


                'Partner Shares
                sOrgType = objclsSchduleReport.LoadOrgtypeId(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                If sOrgType = 68 Then
                    Dim lblptnr As New Label
                    Dim yearParts As String() = ddlFinancialYear.SelectedItem.Text.Split("-"c)

                    If yearParts.Length = 2 AndAlso yearParts(0).Length = 2 AndAlso yearParts(1).Length = 2 Then
                        ' Convert the year parts to integers
                        Dim year1 As Integer
                        Dim year2 As Integer
                        If Integer.TryParse(yearParts(0), year1) AndAlso Integer.TryParse(yearParts(1), year2) Then
                            ' Convert the two-digit years to four-digit years
                            Dim fullYear1 As String = If(year1 < 50, "20" & year1.ToString("00"), "19" & year1.ToString("00"))
                            Dim fullYear2 As String = If(year2 < 50, "20" & year2.ToString("00"), "19" & year2.ToString("00"))

                            ' Concatenate the full years with a "-" separator
                            Dim sFin2 As String = fullYear1 & "-" & fullYear2
                            Dim iFin1 As Integer = Val(sFin2)
                            Dim dtPartnerShares As DataTable
                            dtPartnerShares = objclsPartnerFund.LoadAllPartnershipFirms(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iFin1, sFin2, "Yes")
                            ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAEXCL.rdlc")
                            Dim rds32 As New ReportDataSource("DataSet20", dtPartnerShares)
                            ReportViewer2.LocalReport.DataSources.Add(rds32)
                        Else
                            lblError.Text = "Invalid year format."
                        End If
                    Else
                        lblError.Text = "Invalid year format."
                    End If
                Else
                    Dim dtPartnerShares As New DataTable
                    ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAEXCL.rdlc")
                    Dim rds32 As New ReportDataSource("DataSet20", dtPartnerShares)
                    ReportViewer2.LocalReport.DataSources.Add(rds32)

                End If


                'Account Ratio
                dtRatios = objclsAccRatios.LoadAccRatio(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAEXCL.rdlc")


                Dim rds5 As New ReportDataSource("DataSet5", dtCash)
                ReportViewer2.LocalReport.DataSources.Add(rds5)
                Dim rds6 As New ReportDataSource("DataSet6", dtCash1)
                ReportViewer2.LocalReport.DataSources.Add(rds6)
                Dim rds7 As New ReportDataSource("DataSet7", dtCash2)
                ReportViewer2.LocalReport.DataSources.Add(rds7)
                Dim rds8 As New ReportDataSource("DataSet8", dtCash3)
                ReportViewer2.LocalReport.DataSources.Add(rds8)
                Dim rds9 As New ReportDataSource("DataSet9", dtCash4)
                ReportViewer2.LocalReport.DataSources.Add(rds9)
                Dim rds10 As New ReportDataSource("DataSet10", dtRatios)
                ReportViewer2.LocalReport.DataSources.Add(rds10)

                'Schedule Notes
                Dim dt11 As New DataTable
                dt11 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AU")
                Dim rds11 As New ReportDataSource("DataSet11", dt11)
                ReportViewer2.LocalReport.DataSources.Add(rds11)

                Dim dt12 As New DataTable
                dt12 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "IS")
                Dim rds12 As New ReportDataSource("DataSet12", dt12)
                ReportViewer2.LocalReport.DataSources.Add(rds12)
                Dim dt13 As New DataTable
                dt13 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "AI")
                Dim rds13 As New ReportDataSource("DataSet13", dt13)
                ReportViewer2.LocalReport.DataSources.Add(rds13)

                Dim dt14 As New DataTable
                dt4 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "BS")
                Dim rds14 As New ReportDataSource("DataSet14", dt14)
                ReportViewer2.LocalReport.DataSources.Add(rds14)

                Dim dt15 As New DataTable
                dt15 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "CC")
                Dim rds15 As New ReportDataSource("DataSet15", dt15)
                ReportViewer2.LocalReport.DataSources.Add(rds15)

                Dim dt16 As New DataTable
                dt16 = objclsSchduleNote.getScheduleNote_First(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FD")
                Dim rds16 As New ReportDataSource("DataSet16", dt16)
                ReportViewer2.LocalReport.DataSources.Add(rds16)

                dtS1 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SF")
                Dim rds17 As New ReportDataSource("DataSet17", dtS1)
                ReportViewer2.LocalReport.DataSources.Add(rds17)

                dtS2 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SS")
                Dim rds18 As New ReportDataSource("ScheduleNote_Second2", dtS2)
                ReportViewer2.LocalReport.DataSources.Add(rds18)

                dtS3 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "ST")
                Dim rds19 As New ReportDataSource("ScheduleNote_Second3", dtS3)
                ReportViewer2.LocalReport.DataSources.Add(rds19)

                dtS4 = objclsSchduleNote.getScheduleNote_Second(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "SV")
                Dim rds20 As New ReportDataSource("ScheduleNote_Second4", dtS4)
                ReportViewer2.LocalReport.DataSources.Add(rds20)

                dtT1 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBE")
                Dim rds21 As New ReportDataSource("ScheduleNote_Third1", dtT1)
                ReportViewer2.LocalReport.DataSources.Add(rds21)

                dtT2 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                Dim rds22 As New ReportDataSource("ScheduleNote_Third2", dtT2)
                ReportViewer2.LocalReport.DataSources.Add(rds22)

                dtT2 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "cEquity")
                Dim rds23 As New ReportDataSource("ScheduleNote_Desc", dtT2)
                ReportViewer2.LocalReport.DataSources.Add(rds23)

                dtT3 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "dPref")
                Dim rds24 As New ReportDataSource("ScheduleNote_Desc1", dtT3)
                ReportViewer2.LocalReport.DataSources.Add(rds24)

                dtT4 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TEE")
                Dim rds25 As New ReportDataSource("ScheduleNote_Third3", dtT4)
                ReportViewer2.LocalReport.DataSources.Add(rds25)

                dtT5 = objclsSchduleNote.getScheduleNote_Third(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "TBP")
                Dim rds26 As New ReportDataSource("ScheduleNote_Third4", dtT5)
                ReportViewer2.LocalReport.DataSources.Add(rds26)

                dtT6 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "fShares")
                Dim rds27 As New ReportDataSource("ScheduleNote_Desc2", dtT6)
                ReportViewer2.LocalReport.DataSources.Add(rds27)

                dtT7 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSC")
                Dim rds28 As New ReportDataSource("ScheduleNote_Fourth", dtT7)
                ReportViewer2.LocalReport.DataSources.Add(rds28)

                dtT8 = objclsSchduleNote.getScheduleNote_Fourth(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "FSP")
                Dim rds29 As New ReportDataSource("ScheduleNote_Fourth1", dtT8)
                ReportViewer2.LocalReport.DataSources.Add(rds29)

                dtT9 = objclsSchduleNote.getScheduleNote_cNote(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, "footNote")
                Dim rds30 As New ReportDataSource("ScheduleNote_Desc3", dtT9)
                ReportViewer2.LocalReport.DataSources.Add(rds30)


                'Accounting policies
                dtAcountingpolices = objAccountpolicies.LoadReportContentToGrid(sSession.AccessCode, sSession.AccessCodeID, 1)

                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAEXCL.rdlc")
                Dim rds31 As New ReportDataSource("DataSet18", dtAcountingpolices)
                ReportViewer2.LocalReport.DataSources.Add(rds31)

                Dim rdsstatue As New ReportDataSource("DataSet19", dtstatue)
                ReportViewer2.LocalReport.DataSources.Add(rdsstatue)


                Dim REPORT_DEF As ReportParameter() = New ReportParameter() {New ReportParameter("REPORT_DEF", "Audit Documentation For the Year" & ", " & objclsGeneralFunctions.Get4DigitCurrentFinancialYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue))}
                ReportViewer2.LocalReport.SetParameters(REPORT_DEF)
                Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Comp_Name", objclsReport.GetCustomers(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue))}
                ReportViewer2.LocalReport.SetParameters(Comp_Name)
                Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
                ReportViewer2.LocalReport.SetParameters(Company_Address)
                Dim TotalCurrentcategory1 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory1", lblTotalCurrentcategory1.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory1)
                Dim TotalPrevcategory1 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory1", lblTotalPrevcategory1.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory1)
                Dim TotalCurrentcategory2 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory2", lblCurrentOpratingTotal.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory2)
                Dim TotalPrevcategory2 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory2", lblPrevOpratingTotal.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory2)
                Dim TotalCurrentcategory3 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory3", lblCurrentinvestingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory3)
                Dim TotalPrevcategory3 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory3", lblprevinvestingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory3)
                Dim TotalCurrentcategory4 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalCurrentcategory4", lblCurrentfinancingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalCurrentcategory4)
                Dim TotalPrevcategory4 As ReportParameter() = New ReportParameter() {New ReportParameter("TotalPrevcategory4", lblPrevfinancingactivities.Text)}
                ReportViewer2.LocalReport.SetParameters(TotalPrevcategory4)
                Dim iFCurrentYear As ReportParameter() = New ReportParameter() {New ReportParameter("iFCurrentYear", objclsGeneralFunctions.Get4DigitCurrentFinancialYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue))}
                ReportViewer2.LocalReport.SetParameters(iFCurrentYear)
                Dim iFPrevYear As ReportParameter() = New ReportParameter() {New ReportParameter("iFPrevYear", objclsGeneralFunctions.Get4DigitCurrentFinancialYear(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue - 1))}
                ReportViewer2.LocalReport.SetParameters(iFPrevYear)
                Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
                ReportViewer2.LocalReport.SetParameters(CompRegNo)
                Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
                ReportViewer2.LocalReport.SetParameters(companydetailsName)
                Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
                ReportViewer2.LocalReport.SetParameters(Company_Code)
                If ddlPartners.SelectedIndex > 0 Then
                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", dtpartners.Rows(0)("Fullname").ToString)}
                    ReportViewer2.LocalReport.SetParameters(Partners_Name)
                    If dtpartners.Rows(0)("usr_PhoneNo").ToString = "" Or Nothing Then
                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "Not available")}
                        ReportViewer2.LocalReport.SetParameters(Partners_MNum)
                    Else
                        Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", dtpartners.Rows(0)("usr_PhoneNo").ToString)}
                        ReportViewer2.LocalReport.SetParameters(Partners_MNum)
                    End If
                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", dtpartners.Rows(0)("org_name").ToString)}
                    ReportViewer2.LocalReport.SetParameters(org_name)
                Else
                    Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "")}
                    ReportViewer2.LocalReport.SetParameters(Partners_Name)
                    Dim Partners_MNum As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_MNum", "")}
                    ReportViewer2.LocalReport.SetParameters(Partners_MNum)
                    Dim org_name As ReportParameter() = New ReportParameter() {New ReportParameter("org_name", "")}
                    ReportViewer2.LocalReport.SetParameters(org_name)
                End If
                Dim sCust_fontstyle As String = ""
                Dim sCust_border As Integer = 0
                Dim dtformat As New DataTable
                dtformat = objclsSchduleReport.getCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtformat(0)("CF_ID") <> 0 Then
                    sCust_fontstyle = dtformat(0)("CF_name")
                    sCust_border = dtformat(0)("CUST_rptBorder")
                End If
                Dim sFontstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sFontstyle", sCust_fontstyle)}
                ReportViewer2.LocalReport.SetParameters(sFontstyle)
                Dim sborderstyle As ReportParameter() = New ReportParameter() {New ReportParameter("sborderstyle", sCust_border)}
                ReportViewer2.LocalReport.SetParameters(sborderstyle)
                Dim sUDINNo As ReportParameter() = New ReportParameter() {New ReportParameter("sUDINNo", txtUDINNo.Text)}
                ReportViewer2.LocalReport.SetParameters(sUDINNo)
                Dim sUDIINDate As ReportParameter() = New ReportParameter() {New ReportParameter("sUDIINDate", txtUDINDate.Text)}
                ReportViewer2.LocalReport.SetParameters(sUDIINDate)
                Dim CYear = New ReportParameter() {New ReportParameter("CYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue)}
                ReportViewer2.LocalReport.SetParameters(CYear)
                Dim PYear = New ReportParameter() {New ReportParameter("PYear", "31st March" & " 20" & ddlFinancialYear.SelectedValue - 1)}
                ReportViewer2.LocalReport.SetParameters(PYear)


                Dim sAmtIn As String
                sAmtIn = "In Rupees"
                Dim sAmountTxt As ReportParameter() = New ReportParameter() {New ReportParameter("sAmountTxt", sAmtIn)}
                ReportViewer2.LocalReport.SetParameters(sAmountTxt)

                'Trade
                Dim datatable1 As New DataTable
                Dim DataTable2 As New DataTable
                Dim dataset As DataSet = objUT.GetTotal(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, sSelectedBranches)
                'Dim dtTrade As DataTable = objUT.GetCTrDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlcategory.SelectedIndex, ddlbranchName.SelectedValue, ddlOthType.SelectedIndex)
                datatable1 = dataset.Tables(0)
                DataTable2 = dataset.Tables(1)
                ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalAudit/RptFFOAEXCL.rdlc")

                Dim rds34 As New ReportDataSource("DataSet22", datatable1)
                ReportViewer2.LocalReport.DataSources.Add(rds34)
                Dim rds35 As New ReportDataSource("DataSet23", DataTable2)
                ReportViewer2.LocalReport.DataSources.Add(rds35)

                Dim ExcelViewer As Byte() = ReportViewer2.LocalReport.Render("Excel")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=Final Audit Report  " + ddlCustomerName.SelectedItem.Text + " " + ddlFinancialYear.SelectedItem.Text + ".xls")
                'Response.BinaryWrite(ExcelViewer)
                Dim fileStream As FileStream = Nothing
                ' Dim Bytes() As Byte = ReportViewer1.LocalReport.Render("PDF", "", Nothing, Nothing, Nothing, Nothing, Nothing)
                Dim sImagePath As String
                'sImagePath = objIndex.GetImagePath(sSession.AccessCode, sSession.AccessCodeID)
                sImagePath = Server.MapPath("~\TempRpt\")
                'sImagePath = sImagePath & "\Web\"
                If Directory.Exists(sImagePath) = False Then
                    Directory.CreateDirectory(sImagePath)
                End If

                File.WriteAllBytes(sImagePath & "Finalrpt1.xls", ExcelViewer)

                'Rename Excel file
                Dim excelFilePath As String = sImagePath & "Finalrpt1.xls"

                ' Create an Excel application object
                Dim excelApp As New Microsoft.Office.Interop.Excel.Application

                ' Open the Excel workbook
                Dim excelWorkbook As Microsoft.Office.Interop.Excel.Workbook = excelApp.Workbooks.Open(excelFilePath)

                ' Get the sheet you want to rename by index 


                Dim integerList As New List(Of String) From {"BS", "PL", "BS Summary", "PL Summary", "RPC Act.", "Partner Share", "Cash Flow", "Accounting Ratios", "Accounting Policies", "Notes", "Trade"}
                For Each excelSheet As Microsoft.Office.Interop.Excel.Worksheet In excelWorkbook.Sheets
                    Dim sheetName As String = excelSheet.Name
                    Dim newSheetName As String = integerList(excelSheet.Index - 1)
                    excelSheet.Name = newSheetName
                    If sOrgType <> 68 Then
                        If newSheetName = "Partner Share" Then
                            ' Set the visibility of the sheet to false
                            excelSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetHidden
                        End If
                    End If
                Next

                excelWorkbook.Save()
                excelWorkbook.Close()
                excelApp.Quit()
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)

                Response.TransmitFile(excelFilePath)
                Response.Flush()
                Response.End()


                ' Set the new name for the sheet

                ' Save the changes


            Else
                lblError.Text = "Select Customer"
                lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click")
            Throw
        End Try
    End Sub

    Private Sub btnfrz_ServerClick(sender As Object, e As EventArgs) Handles btnfrz.ServerClick
        Dim iSelectedLocation As Integer
        Dim sSelectedBranches As String = ""
        Try
            For i = 0 To lstbranchSchedTemp.Items.Count - 1
                If lstbranchSchedTemp.Items(i).Selected = True Then
                    iSelectedLocation = iSelectedLocation + 1
                    sSelectedBranches = sSelectedBranches & "," & lstbranchSchedTemp.Items(i).Value
                End If
            Next
            If lstbranchSchedTemp.Items.Count = 0 Then
                sSelectedBranches = "0"
            End If
            If sSelectedBranches.StartsWith(",") Then
                sSelectedBranches = sSelectedBranches.Remove(0, 1)
            End If
            If sSelectedBranches.EndsWith(",") Then
                sSelectedBranches = sSelectedBranches.Remove(Len(sSelectedBranches) - 1, 1)
            End If
            If sSelectedBranches = "" Then
                lblError.Text = "Select Branch for Customer"
                Exit Sub
            End If
            If sSession.PandLAmount <> "" Then
                objclsSchduleReport.UpdatePnL(sSession.AccessCode, sSession.AccessCodeID, sSession.PandLAmount, ddlCustomerName.SelectedValue, sSession.UserID, ddlFinancialYear.SelectedValue, sSelectedBranches)
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class
