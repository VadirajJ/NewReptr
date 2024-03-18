Imports System
Imports System.Data
Imports BusinesLayer
Imports System.IO
Imports Microsoft.Reporting.WebForms
Public Class ReportGeneration
    Inherits System.Web.UI.Page
    Private sFormName As String = "Digital_AuditOffice_ReportGeneration"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsReports As New clsReports
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private sSession As AllSession
    Private Shared iPkId As Integer
    Private Shared bLoginUserIsPartner As Boolean
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.ImageUrl = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sModule As String, sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                LoadFinalcialYear()
                BindCustomerName() : BindAuditNo(0)
                BindReportType() : BindHeading(0)
                If sSession.AuditCodeID > 0 Then
                    ddlAuditNo.SelectedValue = sSession.AuditCodeID
                    If ddlAuditNo.SelectedIndex > 0 Then
                        ddlAuditNo_SelectedIndexChanged(sender, e)
                    End If
                End If
                RFVCustomers.ErrorMessage = "Select Customer Name." : RFVCustomers.InitialValue = "Select Customer Name"
                RFVAuditNo.InitialValue = "Select Audit No" : RFVAuditNo.ErrorMessage = "Select Audit No."
                RFVReportType.ErrorMessage = "Select Report Type" : RFVReportType.InitialValue = "0"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear()
        Dim iYearID As Integer
        Try
            ddlFYear.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sSession.AccessCode, sSession.AccessCodeID, 0)
            ddlFYear.DataTextField = "YMS_ID"
            ddlFYear.DataValueField = "YMS_YearID"
            ddlFYear.DataBind()
            Try
                If sSession.YearID > 0 Then
                    ddlFYear.SelectedValue = sSession.YearID
                Else
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFYear.SelectedValue = iYearID
                    Else
                        ddlFYear.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindCustomerName()
        Try
            ddlCustomers.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomers.DataTextField = "CUST_Name"
            ddlCustomers.DataValueField = "CUST_ID"
            ddlCustomers.DataBind()
            ddlCustomers.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomerName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub BindAuditNo(ByVal iCustID As Integer)
        Try
            ddlAuditNo.DataSource = objclsStandardAudit.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFYear.SelectedValue, iCustID, sSession.UserID, bLoginUserIsPartner)
            ddlAuditNo.DataTextField = "SA_AuditNo"
            ddlAuditNo.DataValueField = "SA_ID"
            ddlAuditNo.DataBind()
            ddlAuditNo.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditNo" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindReportType()
        Try
            ddlReportType.Items.Add(New ListItem("Select Report Type", "0"))
            ddlReportType.Items.Add(New ListItem("Report on the standalone Financial Statements", "1"))
            ddlReportType.Items.Add(New ListItem("Independent Auditor's Report", "2"))
            ddlReportType.Items.Add(New ListItem("Annexure A to the Independent Auditor's Report", "3"))
            ddlReportType.Items.Add(New ListItem("Annexure B to the Independent Auditor's Report", "4"))
            ddlReportType.Items.Add(New ListItem("LOE and Information about the Auditee Report", "5"))
            ddlReportType.Items.Add(New ListItem("Management Representation Letter1", "6"))
            ddlReportType.Items.Add(New ListItem("Management Representation Letter2", "7"))
            ddlReportType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFunction" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Private Sub BindHeading(ByVal iReportTypeId As Integer)
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            dt = objclsReports.LoadHeading(sSession.AccessCode, sSession.AccessCodeID, 0, iReportTypeId)
            If dt.Rows.Count > 0 Then
                dt1 = objclsReports.LoadHeading1(sSession.AccessCode, sSession.AccessCodeID, 0, dt.Rows(0).Item("TEM_ContentId"), iReportTypeId)
            End If
            ddlHeading.DataSource = dt1
            ddlHeading.DataTextField = "RCM_Heading"
            ddlHeading.DataValueField = "RCM_Id"
            ddlHeading.DataBind()
            ddlHeading.Items.Insert(0, "Select Heading")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindHeading" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub ddlFYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFYear.SelectedIndexChanged
        Dim iCustId As Integer = 0
        Try
            lblError.Text = ""
            iPkId = 0
            gvReportGeneration.DataSource = Nothing
            gvReportGeneration.DataBind()
            ddlReportType.SelectedIndex = 0 : BindHeading(0) : txtDescription.Value = ""
            iCustId = If(ddlCustomers.SelectedIndex > 0, ddlCustomers.SelectedValue, iCustId)
            BindAuditNo(iCustId)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomers.SelectedIndexChanged
        Dim iCustId As Integer = 0
        Try
            lblError.Text = ""
            iPkId = 0
            gvReportGeneration.DataSource = Nothing
            gvReportGeneration.DataBind()
            ddlReportType.SelectedIndex = 0 : BindHeading(0) : txtDescription.Value = ""
            iCustId = If(ddlCustomers.SelectedIndex > 0, ddlCustomers.SelectedValue, iCustId)
            BindAuditNo(iCustId)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomers_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            iPkId = 0
            gvReportGeneration.DataSource = Nothing
            gvReportGeneration.DataBind()
            ddlReportType.SelectedIndex = 0 : BindHeading(0)
            txtDescription.Value = ""
            If ddlAuditNo.SelectedIndex > 0 Then
                dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 1 Then ddlCustomers.SelectedValue = dt.Rows(0)("SA_CustID")
                'BindDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedIndexChanged
        Try
            lblError.Text = ""
            iPkId = 0
            txtDescription.Value = ""
            gvReportGeneration.DataSource = Nothing
            gvReportGeneration.DataBind()
            BindHeading(0)
            If ddlCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblReportGenerationValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlCustomers.Focus()
                Exit Sub
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblReportGenerationValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Sub
            End If
            BindDetails()
            BindHeading(ddlReportType.SelectedIndex)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReportType_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlHeading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHeading.SelectedIndexChanged
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            lblError.Text = ""
            txtDescription.Value = ""
            iPkId = 0
            If ddlCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblReportGenerationValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlCustomers.Focus()
                Exit Sub
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblReportGenerationValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Sub
            End If
            If ddlReportType.SelectedIndex = 0 Then
                lblError.Text = "Select Report Type." : lblReportGenerationValidationMsg.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlReportType.Focus()
                Exit Sub
            End If
            If ddlHeading.SelectedIndex > 0 Then
                dt1 = objclsReports.LoadDescriptionfromRepot(sSession.AccessCode, sSession.AccessCodeID, ddlHeading.SelectedValue, 0, ddlCustomers.SelectedValue, ddlAuditNo.SelectedValue, ddlReportType.SelectedIndex, ddlFYear.SelectedValue)
                If dt1.Rows.Count > 0 Then
                    txtDescription.Value = dt1.Rows(0).Item("RG_Description")
                    Exit Sub
                End If

                dt = objclsReports.LoadDescription(sSession.AccessCode, sSession.AccessCodeID, ddlHeading.SelectedValue)
                If dt.Rows.Count > 0 Then txtDescription.Value = dt.Rows(0).Item("RCM_Description")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHeading_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr As Array
        Try
            lblError.Text = ""
            If ddlCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblReportGenerationValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlCustomers.Focus()
                Exit Sub
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblReportGenerationValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Sub
            End If
            If ddlReportType.SelectedIndex = 0 Then
                lblError.Text = "Select Report Type." : lblReportGenerationValidationMsg.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlReportType.Focus()
                Exit Sub
            End If
            If ddlHeading.SelectedIndex = 0 Then
                lblError.Text = "Select Heading." : lblReportGenerationValidationMsg.Text = "Select Heading."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlHeading.Focus()
                Exit Sub
            End If

            objclsReports.RG_Id = If(iPkId = 0, 0, iPkId)
            objclsReports.RG_CustomerId = If(ddlCustomers.SelectedIndex > 0, ddlCustomers.SelectedValue, 0)
            objclsReports.RG_Signedby = 0
            objclsReports.RG_YearId = ddlFYear.SelectedValue
            objclsReports.RG_ReportType = If(ddlReportType.SelectedIndex > 0, ddlReportType.SelectedIndex, 0)
            objclsReports.RG_Module = 0
            objclsReports.RG_Report = 0
            objclsReports.RG_Heading = If(ddlHeading.SelectedIndex > 0, ddlHeading.SelectedValue, 0)
            objclsReports.RG_Description = If(txtDescription.Value <> "", objclsGRACeGeneral.SafeSQL(txtDescription.Value), "")
            objclsReports.RG_CrBy = sSession.UserID
            objclsReports.RG_CrOn = DateTime.Today
            objclsReports.RG_UpdatedBy = sSession.UserID
            objclsReports.RG_UpdatedOn = DateTime.Today
            objclsReports.RG_IPAddress = sSession.IPAddress
            objclsReports.RG_Compid = sSession.AccessCodeID
            objclsReports.RG_FinancialYear = sSession.YearID
            objclsReports.RG_AuditId = ddlAuditNo.SelectedValue
            objclsReports.RG_UDIN = ""
            objclsReports.RG_UDINdate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Arr = objclsReports.SaveReportGeneration(sSession.AccessCode, sSession.AccessCodeID, objclsReports)
            iPkId = Arr(1)
            BindDetails()
            objclsStandardAudit.UpdateStandardAuditStatus(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, 5)
            If Arr(0) = "2" Then
                lblReportGenerationValidationMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModelReportGenerationValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblReportGenerationValidationMsg.Text = "Successfully Saved"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModelReportGenerationValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindDetails()
        Dim dt As New DataTable
        Dim iCustId As Integer = 0, iAuditId As Integer = 0
        Try
            iCustId = If(ddlCustomers.SelectedIndex > 0, ddlCustomers.SelectedValue, iCustId)
            iAuditId = If(ddlAuditNo.SelectedIndex > 0, ddlAuditNo.SelectedValue, iAuditId)
            gvReportGeneration.DataSource = Nothing
            gvReportGeneration.DataBind()
            dt = objclsReports.BinALLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFYear.SelectedValue, iCustId, iAuditId, ddlReportType.SelectedIndex, 0)
            If dt.Rows.Count > 0 Then
                gvReportGeneration.DataSource = dt
                gvReportGeneration.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            Response.Redirect(String.Format("~/DigitalFilling/ReportGeneration.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvReportGeneration_PreRender(sender As Object, e As EventArgs) Handles gvReportGeneration.PreRender
        Dim dt As New DataTable
        Try
            If gvReportGeneration.Rows.Count > 0 Then
                gvReportGeneration.UseAccessibleHeader = True
                gvReportGeneration.HeaderRow.TableSection = TableRowSection.TableHeader
                gvReportGeneration.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReportGeneration_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvReportGeneration_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvReportGeneration.RowCommand
        Dim lblPKID As New Label, lblHeadingid As New Label, lblReportTypeID As New Label, lblAuditID As New Label, lblCustomerID As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPKID = DirectCast(clickedRow.FindControl("lblPKID"), Label)
                iPkId = lblPKID.Text

                lblHeadingid = DirectCast(clickedRow.FindControl("lblHeadingid"), Label)
                lblReportTypeID = DirectCast(clickedRow.FindControl("lblReportTypeID"), Label)
                lblAuditID = DirectCast(clickedRow.FindControl("lblAuditID"), Label)
                lblCustomerID = DirectCast(clickedRow.FindControl("lblCustomerID"), Label)

                ddlCustomers.SelectedValue = lblCustomerID.Text
                ddlAuditNo.SelectedValue = lblAuditID.Text
                ddlReportType.SelectedIndex = lblReportTypeID.Text

                BindHeading(ddlReportType.SelectedIndex)
                ddlHeading.SelectedValue = lblHeadingid.Text
                ddlHeading_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReportGeneration_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvReportGeneration_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvReportGeneration.RowDataBound

    End Sub
    Private Sub gvReportGeneration_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvReportGeneration.RowEditing

    End Sub
    Private Sub imgbtnReport_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnReport.Click
        Dim sTempPath As String = "", sFileName As String = "Audit Report", sReportType As String = "", sReport As String = ""
        Dim dtReport As New DataTable, dt As New DataTable, empdt As New DataTable, dtbranchdetails As New DataTable
        Dim mimeType As String = Nothing
        Try
            If ddlCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblReportGenerationValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlCustomers.Focus()
                Exit Sub
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblReportGenerationValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Sub
            End If
            If ddlReportType.SelectedIndex = 0 Then
                lblError.Text = "Select Report Type." : lblReportGenerationValidationMsg.Text = "Select Report Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                ddlReportType.Focus()
                Exit Sub
            End If

            Dim iCustId As Integer = 0, iAuditId As Integer = 0, iReporttypeId As Integer = 0, iReportId As Integer = 0, iSignedby As Integer = 0, iHeadingId As Integer = 0
            iCustId = If(ddlCustomers.SelectedIndex > 0, ddlCustomers.SelectedValue, iCustId)
            iAuditId = If(ddlAuditNo.SelectedIndex > 0, ddlAuditNo.SelectedValue, iAuditId)
            iReporttypeId = ddlReportType.SelectedIndex
            iReportId = 0
            sReportType = ddlReportType.SelectedItem.Text
            iHeadingId = If(ddlHeading.SelectedIndex > 0, ddlHeading.SelectedValue, iHeadingId)

            dtReport = objclsReports.LoadDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFYear.SelectedValue, iCustId, iAuditId, iReporttypeId, iReportId)
            If dtReport.Rows.Count = 0 Then
                lblError.Text = "Please save the data before generating the report." : lblReportGenerationValidationMsg.Text = "Please save the data before generating the report."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                Exit Sub
            End If

            dt = objclsReports.GetCompanyNameCity(sSession.AccessCode, sSession.AccessCodeID)
            If dt.Rows.Count = 0 Then
                lblError.Text = "Company details are not available in the Master." : lblReportGenerationValidationMsg.Text = "Company details are not available in the Master."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
                Exit Sub
            End If

            Dim sCompanyName As String = "", sAddress As String = "", sCity As String = "", spincode As String = "", smob As String = "", sContactNo As String = "", sEmail As String = "", swebsite As String = "", sRegno As String = ""
            sCompanyName = dt.Rows(0).Item("Company_Name")
            sAddress = dt.Rows(0).Item("Company_Address")
            sCity = dt.Rows(0).Item("Company_City")
            spincode = dt.Rows(0).Item("Company_PinCode")
            smob = dt.Rows(0).Item("Company_MobileNo")
            sContactNo = dt.Rows(0).Item("Company_TelephoneNo")
            sEmail = dt.Rows(0).Item("Company_EmailID")
            swebsite = dt.Rows(0).Item("Company_WebSite")
            sRegno = dt.Rows(0).Item("Company_Code")

            dtbranchdetails = objclsReports.GetBranchDetails(sSession.AccessCode, sSession.AccessCodeID)
            'If dtbranchdetails.Rows.Count = 0 Then
            '    lblError.Text = "Company Branch details are not available in the Master." : lblReportGenerationValidationMsg.Text = "Company Branch details are not available in the Master."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModelReportGenerationValidation').modal('show');", True)
            '    Exit Sub
            'End If
            Dim Branch_Name As String = "", Branch_Address As String = "", Contact_Person As String = "", Contact_MobileNo As String = "", Contact_LandLineNo As String = "", Contact_Email As String = "", Designation As String = ""
            If dtbranchdetails.Rows.Count > 0 Then
                Branch_Name = dtbranchdetails.Rows(0).Item("Company_Branch_Name")
                Branch_Address = dtbranchdetails.Rows(0).Item("Company_Branch_Address")
                Contact_Person = dtbranchdetails.Rows(0).Item("Company_Branch_Contact_Person")
                Contact_MobileNo = dtbranchdetails.Rows(0).Item("Company_Branch_Contact_MobileNo")
                Contact_LandLineNo = dtbranchdetails.Rows(0).Item("Company_Branch_Contact_LandLineNo")
                Contact_Email = dtbranchdetails.Rows(0).Item("Company_Branch_Contact_Email")
                Designation = dtbranchdetails.Rows(0).Item("Company_Branch_Designation")
            End If


            Dim sLogoName As String = "" : Dim imageBase64DataLogoString As String = ""
            Dim objclsCompanyDetails As New clsCompanyDetails
            sLogoName = objclsCompanyDetails.getCompanyImageName(sSession.AccessCode, sSession.AccessCodeID, "A")
            If sLogoName <> "" And sLogoName <> "." Then
                Dim imageDataURL As String = Server.MapPath("~/Images/" + sLogoName)
                If System.IO.File.Exists(imageDataURL) = True Then
                    Dim logoInBytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                    imageBase64DataLogoString = Convert.ToBase64String(logoInBytes)
                End If
            End If

            Dim smemberno As String = "", sUDINno As String = "", sPartner As String = ""
            'empdt = objclsReports.GetEmployeedetails(sSession.AccessCode, sSession.AccessCodeID, 0)
            smemberno = "" 'empdt.Rows(0).Item("usr_OfficePhone")
            sUDINno = "" 'empdt.Rows(0).Item("usr_PhoneNo")
            sPartner = "" 'empdt.Rows(0).Item("usr_FullName")

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtReport)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/ReportGeneration.rdlc")

            'Dim imagePath As String = New Uri(Server.MapPath("~/Images/Attachment16.png")).AbsoluteUri
            'Dim parameter As New ReportParameter("ImagePath", imagePath)
            Dim Logo As ReportParameter() = New ReportParameter() {New ReportParameter("Logo", imageBase64DataLogoString)}
            ReportViewer1.LocalReport.SetParameters(Logo)

            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", sCompanyName)}
            ReportViewer1.LocalReport.SetParameters(CompanyName)

            Dim ReportType As ReportParameter() = New ReportParameter() {New ReportParameter("ReportType", sReportType)}
            ReportViewer1.LocalReport.SetParameters(ReportType)

            Dim Report As ReportParameter() = New ReportParameter() {New ReportParameter("Report", sReport)}
            ReportViewer1.LocalReport.SetParameters(Report)

            Dim Address As ReportParameter() = New ReportParameter() {New ReportParameter("Address", sAddress + " " + sCity + " " + spincode)}
            ReportViewer1.LocalReport.SetParameters(Address)

            Dim Mob As ReportParameter() = New ReportParameter() {New ReportParameter("Mob", "Mob :" + smob + "/" + sContactNo + " " + "Email :" + sEmail + "/" + swebsite)}
            ReportViewer1.LocalReport.SetParameters(Mob)

            Dim City As ReportParameter() = New ReportParameter() {New ReportParameter("City", sCity)}
            ReportViewer1.LocalReport.SetParameters(City)

            Dim Regno As ReportParameter() = New ReportParameter() {New ReportParameter("Regno", "Firm’s registration number :" + " " + sRegno)}
            ReportViewer1.LocalReport.SetParameters(Regno)

            Dim MemberNo As ReportParameter() = New ReportParameter() {New ReportParameter("MemberNo", "Membership Number :" + " " + smemberno)}
            ReportViewer1.LocalReport.SetParameters(MemberNo)

            Dim UDIN As ReportParameter() = New ReportParameter() {New ReportParameter("UDIN", "UDIN :" + " " + sUDINno)}
            ReportViewer1.LocalReport.SetParameters(UDIN)

            Dim Partner As ReportParameter() = New ReportParameter() {New ReportParameter("Partner", sPartner)}
            ReportViewer1.LocalReport.SetParameters(Partner)

            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", "TO THE MEMBERS OF" + vbNewLine + " " + ddlCustomers.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)

            'Branch Details
            'Dim BranchName As ReportParameter() = New ReportParameter() {New ReportParameter("BranchName", Branch_Name)}
            'ReportViewer1.LocalReport.SetParameters(BranchName)

            'Dim ContactPerson As ReportParameter() = New ReportParameter() {New ReportParameter("ContactPerson", Contact_Person)}
            'ReportViewer1.LocalReport.SetParameters(ContactPerson)

            'Dim Designations As ReportParameter() = New ReportParameter() {New ReportParameter("Designations", Designation)}
            'ReportViewer1.LocalReport.SetParameters(Designations)
            If dtbranchdetails.Rows.Count > 0 Then
                Dim BranchAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BranchAddress", Branch_Name + " : " + Contact_Person + "," + Designation + " , " + Branch_Address)}
                ReportViewer1.LocalReport.SetParameters(BranchAddress)
                Dim BranchLabel As ReportParameter() = New ReportParameter() {New ReportParameter("BranchLabel", "Branch Offices:")}
                ReportViewer1.LocalReport.SetParameters(BranchLabel)
                Dim ContactEmail As ReportParameter() = New ReportParameter() {New ReportParameter("ContactEmail", "Mob :" + Contact_MobileNo + " , " + Contact_LandLineNo + " , " + "Email :" + Contact_Email)}
                ReportViewer1.LocalReport.SetParameters(ContactEmail)
            Else
                Dim BranchAddress As ReportParameter() = New ReportParameter() {New ReportParameter("BranchAddress", "")}
                ReportViewer1.LocalReport.SetParameters(BranchAddress)
                Dim BranchLabel As ReportParameter() = New ReportParameter() {New ReportParameter("BranchLabel", "")}
                ReportViewer1.LocalReport.SetParameters(BranchLabel)
                Dim ContactEmail As ReportParameter() = New ReportParameter() {New ReportParameter("ContactEmail", "")}
                ReportViewer1.LocalReport.SetParameters(ContactEmail)
            End If

            'Dim ContactMobileNo As ReportParameter() = New ReportParameter() {New ReportParameter("ContactMobileNo", Contact_MobileNo)}
            'ReportViewer1.LocalReport.SetParameters(ContactMobileNo)

            'Dim ContactLandLineNo As ReportParameter() = New ReportParameter() {New ReportParameter("ContactLandLineNo", Contact_LandLineNo)}
            'ReportViewer1.LocalReport.SetParameters(ContactLandLineNo)

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("WORD")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Audit", "Report Generation", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=DigitalFilling_ReportGeneration" + ".doc")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnReport_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class