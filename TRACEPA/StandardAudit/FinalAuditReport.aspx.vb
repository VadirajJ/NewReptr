Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Public Class FinalAuditReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_ConductAudit"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsScheduleReport As New clsScheduleReport
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsStandardAudit As New clsStandardAudit

    Private sSession As AllSession
    Private Shared iAuditTypeID As Integer
    Private Shared bLoginUserIsPartner As Boolean
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                LoadFinalcialYear(sSession.AccessCode)
                BindCustomers() : BindAuditNo(0) : BindSignedBy()
                If Request.QueryString("AuditID") IsNot Nothing Then
                    ddlAuditNo.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                    If ddlAuditNo.SelectedIndex > 0 Then
                        ddlAuditNo_SelectedIndexChanged(sender, e)
                    End If
                ElseIf sSession.AuditCodeID > 0 Then
                    ddlAuditNo.SelectedValue = sSession.AuditCodeID
                    If ddlAuditNo.SelectedIndex > 0 Then
                        ddlAuditNo_SelectedIndexChanged(sender, e)
                    End If
                End If
                RFVAuditNo.InitialValue = "Select Audit No" : RFVAuditNo.ErrorMessage = "Select Audit No."
                RFVCustomerName.InitialValue = "Select Customer" : RFVCustomerName.ErrorMessage = "Select Customer."
                RFVSignedby.ErrorMessage = "Select Signed By" : RFVSignedby.InitialValue = "Select Signed By"
                RFVUDIN.ControlToValidate = "txtUDIN" : RFVUDIN.ErrorMessage = "Enter UDIN."
                REVUDIN.ErrorMessage = "UDIN exceeded maximum size(max 100 characters)." : REVUDIN.ValidationExpression = "^[\s\S]{0,100}$"
                RFVUDINDate.ControlToValidate = "txtUDINDate" : RFVUDINDate.ErrorMessage = "Enter Start Date."
                REVUDINDate.ErrorMessage = "Enter valid UDIN Date." : REVUDINDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsStandardAudit.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.YearID
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomers()
        Try
            ddlCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "CUST_Name"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAuditNo(ByVal iCustID As Integer)
        Try
            ddlAuditNo.DataSource = objclsStandardAudit.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, sSession.UserID, bLoginUserIsPartner)
            ddlAuditNo.DataTextField = "SA_AuditNo"
            ddlAuditNo.DataValueField = "SA_ID"
            ddlAuditNo.DataBind()
            ddlAuditNo.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditNo" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindSignedBy()
        Try
            ddlSignedby.DataSource = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlSignedby.DataTextField = "USR_FullName"
            ddlSignedby.DataValueField = "USR_ID"
            ddlSignedby.DataBind()
            ddlSignedby.Items.Insert(0, "Select Signed By")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSignedBy" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim iCustID As Integer = 0
        Try
            lblError.Text = "" : lblAuditType.Text = ""
            gvAllTypeReports.DataSource = Nothing
            gvAllTypeReports.DataBind()
            ddlSignedby.SelectedIndex = 0 : txtUDIN.Text = "" : txtUDINDate.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustID = ddlCustomerName.SelectedValue
            End If
            BindAuditNo(iCustID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Try
            lblError.Text = "" : lblAuditType.Text = ""
            gvAllTypeReports.DataSource = Nothing
            gvAllTypeReports.DataBind()
            ddlSignedby.SelectedIndex = 0 : txtUDIN.Text = "" : txtUDINDate.Text = ""
            If ddlAuditNo.SelectedIndex > 0 Then
                BindScheduledDetails(ddlAuditNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ddlCustomerName_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindScheduledDetails(ByVal iAuditID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            If dt.Rows.Count = 1 Then
                ddlCustomerName.SelectedValue = dt.Rows(0)("SA_CustID")
                iAuditTypeID = dt.Rows(0)("SA_AuditTypeID")
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
                If IsDBNull(dt.Rows(0)("SA_SignedBy")) = False Then
                    If dt.Rows(0)("SA_SignedBy") > 0 Then
                        ddlSignedby.SelectedValue = dt.Rows(0)("SA_SignedBy")
                    End If
                End If
                If IsDBNull(dt.Rows(0)("SA_UDIN")) = False Then
                    txtUDIN.Text = dt.Rows(0)("SA_UDIN")
                End If
                If IsDBNull(dt.Rows(0)("SA_UDINdate")) = False Then
                    txtUDINDate.Text = dt.Rows(0)("SA_UDINdate")
                End If
            End If
            BindAllTypeReports()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllTypeReports()
        Dim dt As New DataTable
        Dim dr As DataRow
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("Name")

            dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "Engagement Letter" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "2" : dr("Name") = "Profile / Information about the Auditee" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "3" : dr("Name") = "Audit Report" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "Conduct Audit" : dt.Rows.Add(dr)
            dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "Trial Balance Review Report" : dt.Rows.Add(dr)
            gvAllTypeReports.DataSource = dt
            gvAllTypeReports.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCheckPoint_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAllTypeReports_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectReport As New CheckBox
        Try
            lblError.Text = "" : lblFRValidationMsg.Text = ""
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
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllTypeReports_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr As Array
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblFRValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                ddlCustomerName.Focus()
                Exit Sub
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblFRValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                ddlAuditNo.Focus()
                Exit Sub
            End If
            If ddlSignedby.SelectedIndex = 0 Then
                lblError.Text = "Select Signed by." : lblFRValidationMsg.Text = "Select Signed by."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                ddlSignedby.Focus()
                Exit Sub
            End If
            If txtUDIN.Text.ToString().Trim = "" Then
                lblError.Text = "Enter UDIN." : lblFRValidationMsg.Text = "Enter UDIN."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                txtUDIN.Focus()
                Exit Sub
            End If
            If txtUDINDate.Text.ToString().Trim = "" Then
                lblError.Text = "Enter UDIN Date." : lblFRValidationMsg.Text = "Enter UDIN Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                txtUDINDate.Focus()
                Exit Sub
            End If

            objclsStandardAudit.UpdateSignedByUDINInAudit(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlSignedby.SelectedValue, objclsGRACeGeneral.SafeSQL(txtUDIN.Text.ToString()), Date.ParseExact(txtUDINDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture))
            lblError.Text = "Successfully Saved/Updated." : lblFRValidationMsg.Text = "Successfully Saved/Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim iCheckTypeReport As Integer = 0
        Dim lblReportID As New Label
        Dim chkSelectReport As New CheckBox
        Try
            lblError.Text = "" : lblFRValidationMsg.Text = ""
            For i = 0 To gvAllTypeReports.Rows.Count - 1
                chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                If chkSelectReport.Checked = True Then
                    iCheckTypeReport = 1
                    GoTo Report
                End If
            Next
            If iCheckTypeReport = 0 Then
                lblError.Text = "Select Report Type." : lblFRValidationMsg.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                Exit Try
            End If

Report:     Dim iCount As Integer = objclsStandardAudit.CheckIsAuditReportCompleted(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
            If iCount = 0 Then
                lblError.Text = "Please save the Audit Report data before generating the report." : lblFRValidationMsg.Text = "Please save the Audit Report data before generating the report."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                Exit Try
            End If

            Dim mimeType As String = "application/pdf"
            Dim objCust As New clsCustDetails
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/FinalReport.rdlc")
            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)

            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text.ToString())}
            ReportViewer1.LocalReport.SetParameters(Customer)

            For i = 0 To gvAllTypeReports.Rows.Count - 1
                chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                lblReportID = gvAllTypeReports.Rows(i).FindControl("lblReportID")
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 1 Then
                    Dim dt As New DataTable
                    dt = objclsScheduleReport.GetLOEReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iAuditTypeID)
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
                    dt = objclsScheduleReport.GetCustReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dt = objCust.LoadCustInformationAuditeeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearName, ddlCustomerName.SelectedValue, dt.Rows(dt.Rows.Count - 1)("CUST_NAME").ToString(), dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString(), objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(dt.Rows.Count - 1)("CUST_CommitmentDate"), "D"), dt.Rows(dt.Rows.Count - 1)("CDET_PRODUCTSMANUFACTURED").ToString())
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet7", GetReportTitleNameToDS("Profile / Information about the Auditee"))
                        ReportViewer1.LocalReport.DataSources.Add(rds1)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 3 Then
                    Dim dtReport As New DataTable, dtTab As New DataTable, dtEmp As New DataTable, dtBranchDetails As New DataTable
                    Dim objclsReports As New clsReports
                    Dim iCustId As Integer = 0, iAuditId As Integer = 0, iSignedby As Integer = 0, iHeadingId As Integer = 0
                    iCustId = If(ddlCustomerName.SelectedIndex > 0, ddlCustomerName.SelectedValue, iCustId)
                    iAuditId = If(ddlAuditNo.SelectedIndex > 0, ddlAuditNo.SelectedValue, iAuditId)

                    dtReport = objclsReports.LoadAuditReportInAuditCompletionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustId, iAuditId)
                    dtTab = objclsReports.GetCompanyNameCity(sSession.AccessCode, sSession.AccessCodeID)
                    If dtReport.Rows.Count = 0 Or dtTab.Rows.Count = 0 Then
                    Else
                        Dim sCompanyName As String = "", sAddress As String = "", sCity As String = "", spincode As String = "", smob As String = "", sContactNo As String = "", sEmail As String = "", swebsite As String = "", sRegno As String = ""
                        sCompanyName = dtTab.Rows(0).Item("Company_Name")
                        sAddress = dtTab.Rows(0).Item("Company_Address")
                        sCity = dtTab.Rows(0).Item("Company_City")
                        spincode = dtTab.Rows(0).Item("Company_PinCode")
                        smob = dtTab.Rows(0).Item("Company_MobileNo")
                        sContactNo = dtTab.Rows(0).Item("Company_TelephoneNo")
                        sEmail = dtTab.Rows(0).Item("Company_EmailID")
                        swebsite = dtTab.Rows(0).Item("Company_WebSite")
                        sRegno = dtTab.Rows(0).Item("Company_Code")

                        dtBranchDetails = objclsReports.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID)
                        Dim Branch_Name As String = "", Branch_Address As String = "", Contact_Person As String = "", Contact_MobileNo As String = "", Contact_LandLineNo As String = "", Contact_Email As String = "", Designation As String = ""
                        If dtBranchDetails.Rows.Count > 0 Then
                            Branch_Name = dtBranchDetails.Rows(0).Item("Company_Branch_Name")
                            Branch_Address = dtBranchDetails.Rows(0).Item("Company_Branch_Address")
                            Contact_Person = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_Person")
                            Contact_MobileNo = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_MobileNo")
                            Contact_LandLineNo = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_LandLineNo")
                            Contact_Email = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_Email")
                            Designation = dtBranchDetails.Rows(0).Item("Company_Branch_Designation")
                        End If

                        Dim smemberno As String = "", sUDINno As String = "", sPartner As String = ""
                        'dtEmp = objclsReports.GetEmployeedetails(sSession.AccessCode, sSession.AccessCodeID, 0)
                        smemberno = " " 'dtEmp.Rows(0).Item("usr_OfficePhone")
                        sUDINno = txtUDIN.Text.ToString() + " Date : " + txtUDINDate.Text.ToString()
                        sPartner = " " 'dtEmp.Rows(0).Item("usr_FullName")

                        Dim City As ReportParameter() = New ReportParameter() {New ReportParameter("City", sCity)}
                        ReportViewer1.LocalReport.SetParameters(City)

                        Dim ARCompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("ARCompanyName", sCompanyName)}
                        ReportViewer1.LocalReport.SetParameters(ARCompanyName)

                        Dim Regno As ReportParameter() = New ReportParameter() {New ReportParameter("Regno", "Firm’s registration number :" + " " + sRegno)}
                        ReportViewer1.LocalReport.SetParameters(Regno)

                        Dim Partner As ReportParameter() = New ReportParameter() {New ReportParameter("ARPartner", sPartner)}
                        ReportViewer1.LocalReport.SetParameters(Partner)


                        Dim MemberNo As ReportParameter() = New ReportParameter() {New ReportParameter("MemberNo", "Membership Number :" + " " + smemberno)}
                        ReportViewer1.LocalReport.SetParameters(MemberNo)

                        Dim UDIN As ReportParameter() = New ReportParameter() {New ReportParameter("UDIN", "UDIN :" + " " + sUDINno)}
                        ReportViewer1.LocalReport.SetParameters(UDIN)

                        Dim Address As ReportParameter() = New ReportParameter() {New ReportParameter("Address", sAddress + " " + sCity + " " + spincode)}
                        ReportViewer1.LocalReport.SetParameters(Address)

                        Dim Mob As ReportParameter() = New ReportParameter() {New ReportParameter("Mob", "Mob :" + smob + "/" + sContactNo + " " + "Email :" + sEmail + "/" + swebsite)}
                        ReportViewer1.LocalReport.SetParameters(Mob)

                        If dtBranchDetails.Rows.Count > 0 Then
                            Dim BranchAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BranchAddress", Branch_Name + " : " + Contact_Person + "," + Designation + " , " + Branch_Address)}
                            ReportViewer1.LocalReport.SetParameters(BranchAddress)
                            Dim BranchLabel As ReportParameter() = New ReportParameter() {New ReportParameter("BranchLabel", "Branch Offices:")}
                            ReportViewer1.LocalReport.SetParameters(BranchLabel)
                            Dim ContactEmail As ReportParameter() = New ReportParameter() {New ReportParameter("ContactEmail", "Mob :" + Contact_MobileNo + " , " + Contact_LandLineNo + " , " + "Email :" + Contact_Email)}
                            ReportViewer1.LocalReport.SetParameters(ContactEmail)
                        Else
                            Dim BranchAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BranchAddress", " ")}
                            ReportViewer1.LocalReport.SetParameters(BranchAddress)
                            Dim BranchLabel As ReportParameter() = New ReportParameter() {New ReportParameter("BranchLabel", " ")}
                            ReportViewer1.LocalReport.SetParameters(BranchLabel)
                            Dim ContactEmail As ReportParameter() = New ReportParameter() {New ReportParameter("ContactEmail", " ")}
                            ReportViewer1.LocalReport.SetParameters(ContactEmail)
                        End If
                        Dim rds As New ReportDataSource("DataSet5", dtReport)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet8", GetReportTitleNameToDS("Audit Report"))
                        ReportViewer1.LocalReport.DataSources.Add(rds1)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 4 Then
                    Dim objclsStandardAudit As New clsStandardAudit
                    Dim dt As New DataTable
                    Dim dt1 As New DataTable
                    Dim dt2 As New DataTable
                    dt = objclsScheduleReport.GetConductAuditReportForCustAuditId(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dt1 = objclsStandardAudit.LoadStandardAuditConductAuditReport(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(dt.Rows.Count - 1)("SA_ID"))
                        dt2 = objclsStandardAudit.LoadStandardAuditConductAuditObservationsReport(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(dt.Rows.Count - 1)("SA_ID"))

                        Dim rds As New ReportDataSource("DataSet2", dt1)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet3", dt2)
                        ReportViewer1.LocalReport.DataSources.Add(rds1)

                        Dim rds2 As New ReportDataSource("DataSet6", GetReportTitleNameToDS("Conduct Audit"))
                        ReportViewer1.LocalReport.DataSources.Add(rds2)

                        Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", dt.Rows(dt.Rows.Count - 1)("SA_AuditNo").ToString())}
                        ReportViewer1.LocalReport.SetParameters(AuditNo)
                        Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString())}
                        ReportViewer1.LocalReport.SetParameters(AuditType)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 5 Then
                    Dim dt As New DataTable
                    Dim obclsUL As New clsUploadLedger
                    dt = obclsUL.LoadLedgerObservationsCommentsReports(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet4", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet9", GetReportTitleNameToDS("Trial Balance Review"))
                        ReportViewer1.LocalReport.DataSources.Add(rds1)
                    End If
                End If
            Next

            ReportViewer1.LocalReport.Refresh()
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AuditCompletion.pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub lnkbtnWord_Click(sender As Object, e As EventArgs) Handles lnkbtnWord.Click
        Dim iCheckTypeReport As Integer = 0
        Dim lblReportID As New Label
        Dim chkSelectReport As New CheckBox
        Try
            lblError.Text = "" : lblFRValidationMsg.Text = ""
            For i = 0 To gvAllTypeReports.Rows.Count - 1
                chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                If chkSelectReport.Checked = True Then
                    iCheckTypeReport = 1
                    GoTo Report
                End If
            Next
            If iCheckTypeReport = 0 Then
                lblError.Text = "Select Report Type." : lblFRValidationMsg.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                Exit Try
            End If

Report:     Dim iCount As Integer = objclsStandardAudit.CheckIsAuditReportCompleted(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
            If iCount = 0 Then
                lblError.Text = "Please save the Audit Report data before generating the report." : lblFRValidationMsg.Text = "Please save the Audit Report data before generating the report."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalFRValidation').modal('show')", True)
                Exit Try
            End If

            Dim mimeType As String = Nothing
            Dim objCust As New clsCustDetails
            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/FinalReport.rdlc")
            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)

            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text.ToString())}
            ReportViewer1.LocalReport.SetParameters(Customer)

            For i = 0 To gvAllTypeReports.Rows.Count - 1
                chkSelectReport = gvAllTypeReports.Rows(i).FindControl("chkSelectReport")
                lblReportID = gvAllTypeReports.Rows(i).FindControl("lblReportID")
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 1 Then
                    Dim dt As New DataTable
                    dt = objclsScheduleReport.GetLOEReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iAuditTypeID)
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
                    dt = objclsScheduleReport.GetCustReportForCustAuditType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dt = objCust.LoadCustInformationAuditeeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearName, ddlCustomerName.SelectedValue, dt.Rows(dt.Rows.Count - 1)("CUST_NAME").ToString(), dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString(), objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(dt.Rows.Count - 1)("CUST_CommitmentDate"), "D"), dt.Rows(dt.Rows.Count - 1)("CDET_PRODUCTSMANUFACTURED").ToString())
                        Dim rds As New ReportDataSource("DataSet1", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet7", GetReportTitleNameToDS("Profile / Information about the Auditee"))
                        ReportViewer1.LocalReport.DataSources.Add(rds1)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 3 Then
                    Dim dtReport As New DataTable, dtTab As New DataTable, dtEmp As New DataTable, dtBranchDetails As New DataTable
                    Dim objclsReports As New clsReports
                    Dim iCustId As Integer = 0, iAuditId As Integer = 0, iSignedby As Integer = 0, iHeadingId As Integer = 0
                    iCustId = If(ddlCustomerName.SelectedIndex > 0, ddlCustomerName.SelectedValue, iCustId)
                    iAuditId = If(ddlAuditNo.SelectedIndex > 0, ddlAuditNo.SelectedValue, iAuditId)

                    dtReport = objclsReports.LoadAuditReportInAuditCompletionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustId, iAuditId)
                    dtTab = objclsReports.GetCompanyNameCity(sSession.AccessCode, sSession.AccessCodeID)
                    If dtReport.Rows.Count = 0 Or dtTab.Rows.Count = 0 Then
                    Else
                        Dim sCompanyName As String = "", sAddress As String = "", sCity As String = "", spincode As String = "", smob As String = "", sContactNo As String = "", sEmail As String = "", swebsite As String = "", sRegno As String = ""
                        sCompanyName = dtTab.Rows(0).Item("Company_Name")
                        sAddress = dtTab.Rows(0).Item("Company_Address")
                        sCity = dtTab.Rows(0).Item("Company_City")
                        spincode = dtTab.Rows(0).Item("Company_PinCode")
                        smob = dtTab.Rows(0).Item("Company_MobileNo")
                        sContactNo = dtTab.Rows(0).Item("Company_TelephoneNo")
                        sEmail = dtTab.Rows(0).Item("Company_EmailID")
                        swebsite = dtTab.Rows(0).Item("Company_WebSite")
                        sRegno = dtTab.Rows(0).Item("Company_Code")

                        dtBranchDetails = objclsReports.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID)
                        Dim Branch_Name As String = "", Branch_Address As String = "", Contact_Person As String = "", Contact_MobileNo As String = "", Contact_LandLineNo As String = "", Contact_Email As String = "", Designation As String = ""
                        If dtBranchDetails.Rows.Count > 0 Then
                            Branch_Name = dtBranchDetails.Rows(0).Item("Company_Branch_Name")
                            Branch_Address = dtBranchDetails.Rows(0).Item("Company_Branch_Address")
                            Contact_Person = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_Person")
                            Contact_MobileNo = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_MobileNo")
                            Contact_LandLineNo = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_LandLineNo")
                            Contact_Email = dtBranchDetails.Rows(0).Item("Company_Branch_Contact_Email")
                            Designation = dtBranchDetails.Rows(0).Item("Company_Branch_Designation")
                        End If

                        Dim smemberno As String = "", sUDINno As String = "", sPartner As String = ""
                        'dtEmp = objclsReports.GetEmployeedetails(sSession.AccessCode, sSession.AccessCodeID, 0)
                        smemberno = " " 'dtEmp.Rows(0).Item("usr_OfficePhone")
                        sUDINno = txtUDIN.Text.ToString() + " Date : " + txtUDINDate.Text.ToString()
                        sPartner = " " 'dtEmp.Rows(0).Item("usr_FullName")

                        Dim City As ReportParameter() = New ReportParameter() {New ReportParameter("City", sCity)}
                        ReportViewer1.LocalReport.SetParameters(City)

                        Dim ARCompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("ARCompanyName", sCompanyName)}
                        ReportViewer1.LocalReport.SetParameters(ARCompanyName)

                        Dim Regno As ReportParameter() = New ReportParameter() {New ReportParameter("Regno", "Firm’s registration number :" + " " + sRegno)}
                        ReportViewer1.LocalReport.SetParameters(Regno)

                        Dim Partner As ReportParameter() = New ReportParameter() {New ReportParameter("ARPartner", sPartner)}
                        ReportViewer1.LocalReport.SetParameters(Partner)


                        Dim MemberNo As ReportParameter() = New ReportParameter() {New ReportParameter("MemberNo", "Membership Number :" + " " + smemberno)}
                        ReportViewer1.LocalReport.SetParameters(MemberNo)

                        Dim UDIN As ReportParameter() = New ReportParameter() {New ReportParameter("UDIN", "UDIN :" + " " + sUDINno)}
                        ReportViewer1.LocalReport.SetParameters(UDIN)

                        Dim Address As ReportParameter() = New ReportParameter() {New ReportParameter("Address", sAddress + " " + sCity + " " + spincode)}
                        ReportViewer1.LocalReport.SetParameters(Address)

                        Dim Mob As ReportParameter() = New ReportParameter() {New ReportParameter("Mob", "Mob :" + smob + "/" + sContactNo + " " + "Email :" + sEmail + "/" + swebsite)}
                        ReportViewer1.LocalReport.SetParameters(Mob)

                        If dtBranchDetails.Rows.Count > 0 Then
                            Dim BranchAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BranchAddress", Branch_Name + " : " + Contact_Person + "," + Designation + " , " + Branch_Address)}
                            ReportViewer1.LocalReport.SetParameters(BranchAddress)
                            Dim BranchLabel As ReportParameter() = New ReportParameter() {New ReportParameter("BranchLabel", "Branch Offices:")}
                            ReportViewer1.LocalReport.SetParameters(BranchLabel)
                            Dim ContactEmail As ReportParameter() = New ReportParameter() {New ReportParameter("ContactEmail", "Mob :" + Contact_MobileNo + " , " + Contact_LandLineNo + " , " + "Email :" + Contact_Email)}
                            ReportViewer1.LocalReport.SetParameters(ContactEmail)
                        Else
                            Dim BranchAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BranchAddress", " ")}
                            ReportViewer1.LocalReport.SetParameters(BranchAddress)
                            Dim BranchLabel As ReportParameter() = New ReportParameter() {New ReportParameter("BranchLabel", " ")}
                            ReportViewer1.LocalReport.SetParameters(BranchLabel)
                            Dim ContactEmail As ReportParameter() = New ReportParameter() {New ReportParameter("ContactEmail", " ")}
                            ReportViewer1.LocalReport.SetParameters(ContactEmail)
                        End If
                        Dim rds As New ReportDataSource("DataSet5", dtReport)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet8", GetReportTitleNameToDS("Audit Report"))
                        ReportViewer1.LocalReport.DataSources.Add(rds1)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 4 Then
                    Dim objclsStandardAudit As New clsStandardAudit
                    Dim dt As New DataTable
                    Dim dt1 As New DataTable
                    Dim dt2 As New DataTable
                    dt = objclsScheduleReport.GetConductAuditReportForCustAuditId(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        dt1 = objclsStandardAudit.LoadStandardAuditConductAuditReport(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(dt.Rows.Count - 1)("SA_ID"))
                        dt2 = objclsStandardAudit.LoadStandardAuditConductAuditObservationsReport(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(dt.Rows.Count - 1)("SA_ID"))

                        Dim rds As New ReportDataSource("DataSet2", dt1)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet3", dt2)
                        ReportViewer1.LocalReport.DataSources.Add(rds1)

                        Dim rds2 As New ReportDataSource("DataSet6", GetReportTitleNameToDS("Conduct Audit"))
                        ReportViewer1.LocalReport.DataSources.Add(rds2)

                        Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", dt.Rows(dt.Rows.Count - 1)("SA_AuditNo").ToString())}
                        ReportViewer1.LocalReport.SetParameters(AuditNo)
                        Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", dt.Rows(dt.Rows.Count - 1)("cmm_Desc").ToString())}
                        ReportViewer1.LocalReport.SetParameters(AuditType)
                    End If
                End If
                If chkSelectReport.Checked = True And Val(lblReportID.Text) = 5 Then
                    Dim dt As New DataTable
                    Dim obclsUL As New clsUploadLedger
                    dt = obclsUL.LoadLedgerObservationsCommentsReports(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        Dim rds As New ReportDataSource("DataSet4", dt)
                        ReportViewer1.LocalReport.DataSources.Add(rds)

                        Dim rds1 As New ReportDataSource("DataSet9", GetReportTitleNameToDS("Trial Balance Review"))
                        ReportViewer1.LocalReport.DataSources.Add(rds1)
                    End If
                End If
            Next

            ReportViewer1.LocalReport.Refresh()
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("WORD")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AuditCompletion.doc")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
        End Try
    End Sub
    Public Function GetReportTitleNameToDS(ByVal sType As String) As DataTable
        Dim dt As New DataTable
        Try
            dt.Columns.Add("Title")
            dt.Rows.Add(sType)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class