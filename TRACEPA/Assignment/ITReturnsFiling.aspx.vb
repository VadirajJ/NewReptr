Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class ITReturnsFiling
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_ITReturnsFiling"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsITReturnsFiling As New clsITReturnsFiling
    Private objclsCompanyDetails As New clsCompanyDetails
    Private sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadFinalcialYear(sSession.AccessCode) : BindAssessmentYear()
                BindBillingEntity() : BindClients() : BindAssignto() : BindStatus()
                BindAllITReturnFiling()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsAuditAssignment.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
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
    Private Sub BindBillingEntity()
        Try
            ddlCompanyName.DataSource = objclsCompanyDetails.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyName.DataTextField = "Company_Name"
            ddlCompanyName.DataValueField = "Company_ID"
            ddlCompanyName.DataBind()
            ddlCompanyName.Items.Insert(0, "Select Billing Entity")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindBillingEntity" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAssessmentYear()
        Try
            ddlAssessmentYear.DataSource = objclsAuditAssignment.GetPreviousFinancialYears(sSession.AccessCode, sSession.AccessCodeID, 1)
            ddlAssessmentYear.DataTextField = "Name"
            ddlAssessmentYear.DataValueField = "ID"
            ddlAssessmentYear.DataBind()
            ddlAssessmentYear.SelectedValue = ddlFinancialYear.SelectedValue + 1
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAssessmentYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindClients()
        Try
            ddlClientName.DataSource = objclsITReturnsFiling.LoadITRClients(sSession.AccessCode, sSession.AccessCodeID)
            ddlClientName.DataTextField = "ITR_ClientName"
            ddlClientName.DataValueField = "ITR_ID"
            ddlClientName.DataBind()
            ddlClientName.Items.Insert(0, "Select Client Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindClient" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAssignto()
        Try
            ddlAssignto.DataSource = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)
            ddlAssignto.DataTextField = "FullName"
            ddlAssignto.DataValueField = "Usr_ID"
            ddlAssignto.DataBind()
            ddlAssignto.Items.Insert(0, "Select Assign to")
            ddlAssignto.SelectedValue = sSession.UserID
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAssignto" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStatus()
        Try
            ddlStatus.Items.Add(New ListItem("Select Status", "0"))
            ddlStatus.Items.Add(New ListItem("Assigned", "1"))
            ddlStatus.Items.Add(New ListItem("Completed", "2"))
            ddlStatus.Items.Add(New ListItem("Invoice Raised", "3"))
            ddlStatus.Items.Add(New ListItem("Paid", "4"))
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblError.Text = "" : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            lblHITRNo.Visible = False : lblITRNo.Visible = False : lblITRNoId.Text = "0"
            txtClientName.Enabled = True : txtClientName.Text = "" : txtPAN.Text = ""
            txtAadhaar.Text = "" : txtDOB.Text = "" : txtPhone.Text = "" : txtEmail.Text = ""
            ddlAssignto.SelectedIndex = 0 : txtITLoginId.Text = "" : txtITPassword.Text = ""
            txtServiceChargeINR.Text = "" : ddlStatus.SelectedIndex = 0 : chkInvoiceEmail.Checked = False
            ddlCompanyName.SelectedIndex = 0 : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            BindAllITReturnFiling()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            ddlClientName.SelectedIndex = 0
            txtClientName.Text = "" : ddlFinancialYear.SelectedIndex = 0 : ddlAssessmentYear.SelectedIndex = 0
            Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            Clear()
            ddlAssessmentYear.SelectedValue = ddlFinancialYear.SelectedValue + 1
            BindExistingITReturnFiling()
            BindAllITReturnFiling()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlClientName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlClientName.SelectedIndexChanged
        Try
            Clear()
            BindExistingITReturnFiling()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlClientName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindExistingITReturnFiling()
        Dim dt As New DataTable
        Try
            If ddlClientName.SelectedIndex > 0 Then
                dt = objclsITReturnsFiling.GetSelectedITRFilingClientDetails(sSession.AccessCode, sSession.AccessCodeID, ddlClientName.SelectedValue, ddlFinancialYear.SelectedValue, ddlAssessmentYear.SelectedValue)
                If dt.Rows.Count = 1 Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    lblHITRNo.Visible = True : lblITRNo.Visible = True
                    lblITRNoId.Text = dt.Rows(0)("ITRFD_ID")
                    lblITRNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITRFD_ITRNo").ToString())
                    txtClientName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_ClientName").ToString())
                    txtClientName.Enabled = False
                    txtClientName.Text = ddlClientName.SelectedItem.Text
                    txtPAN.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_PAN").ToString())
                    txtAadhaar.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_Aadhaar").ToString())
                    txtDOB.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0)("ITR_DOB"), "D")
                    txtPhone.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_Phone").ToString())
                    txtEmail.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_Email").ToString())
                    ddlAssignto.SelectedIndex = dt.Rows(0)("ITRFD_AssignTo")
                    ddlCompanyName.SelectedValue = dt.Rows(0)("ITRFD_BillingEntityId")
                    txtITLoginId.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_ITLoginId").ToString())
                    txtITPassword.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ITR_ITPassword").ToString())
                    If dt.Rows(0)("ITRFD_ServiceChargeInINR") <> "0.00" Then
                        txtServiceChargeINR.Text = dt.Rows(0)("ITRFD_ServiceChargeInINR")
                    End If
                    ddlStatus.SelectedIndex = dt.Rows(0)("ITRFD_Status")
                    If dt.Rows(0)("ITRFD_InvoiceMail") = 1 Then
                        chkInvoiceEmail.Checked = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingITReturnFiling" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAllITReturnFiling()
        Dim dt As New DataTable
        Try
            gvITR.DataSource = objclsITReturnsFiling.LoadAllITRFilingClientDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlFinancialYear.SelectedValue, ddlAssessmentYear.SelectedValue)
            gvITR.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllITReturnFiling" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim Arr1() As String
        Dim dServiceChargeINR As Decimal
        Try
            lblError.Text = ""
            If txtClientName.Text.Trim = "" Then
                txtClientName.Focus()
                lblError.Text = "Enter Client Name." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtClientName.Text.Trim.Length > 500 Then
                txtClientName.Focus()
                lblError.Text = "Client Name exceeded maximum size(Max 500 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If (objclsITReturnsFiling.CheckITRClientName(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtClientName.Text.Trim()), 0) = True) Then
                lblError.Text = "Client Name already Exists." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalITValidation').modal('show');", True)
                Return
            End If
            If txtPAN.Text.Trim = "" Then
                txtPAN.Focus()
                lblError.Text = "Enter PAN." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtPAN.Text.Trim.Length > 25 Then
                txtPAN.Focus()
                lblError.Text = "PAN exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAadhaar.Text.Trim = "" Then
                txtAadhaar.Focus()
                lblError.Text = "Enter Aadhaar." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAadhaar.Text.Trim.Length > 25 Then
                txtAadhaar.Focus()
                lblError.Text = "Aadhaar exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            Dim dDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dDOBDate As DateTime = Date.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim d As Integer
            d = DateDiff(DateInterval.Year, dDate.AddYears(-10), dDOBDate)
            If d > 0 Then
                lblError.Text = "DOB Date should be less than or equal to " + dDate.AddYears(-10).Year.ToString() : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalITValidation').modal('show');", True)
                txtDOB.Focus()
                Exit Try
            End If
            If txtPhone.Text.Trim = "" Then
                txtPhone.Focus()
                lblError.Text = "Enter Phone." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtPhone.Text.Trim.Length > 25 Then
                txtPhone.Focus()
                lblError.Text = "Phone exceeded maximum size(Max 100 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtEmail.Text.Trim = "" Then
                txtEmail.Focus()
                lblError.Text = "Enter Email." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtEmail.Text.Trim.Length > 100 Then
                txtEmail.Focus()
                lblError.Text = "Email exceeded maximum size(Max 100 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlAssignto.SelectedIndex = 0 Then
                lblError.Text = "Select Assign to." : lblITValidationMsg.Text = lblError.Text
                ddlAssignto.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            'If txtITLoginId.Text.Trim = "" Then
            '    txtITLoginId.Focus()
            '    lblError.Text = "Enter IT Login Id." : lblITValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If txtITLoginId.Text.Trim.Length > 100 Then
                txtITLoginId.Focus()
                lblError.Text = "IT Login Id exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            'If txtITPassword.Text.Trim = "" Then
            '    txtITPassword.Focus()
            '    lblError.Text = "Enter IT Password." : lblITValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If txtITPassword.Text.Trim.Length > 100 Then
                txtITPassword.Focus()
                lblError.Text = "IT Password exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            'If ddlFinancialYear.SelectedIndex = 0 Then
            '    lblError.Text = "Select Financial Year." : lblITValidationMsg.Text = lblError.Text
            '    ddlFinancialYear.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            'If ddlAssessmentYear.SelectedIndex = 0 Then
            '    ddlAssessmentYear.Text = "Select Assessment Year." : lblITValidationMsg.Text = lblError.Text
            '    ddlFinancialYear.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If ddlStatus.SelectedIndex = 0 Then
                ddlStatus.Text = "Select Status." : lblITValidationMsg.Text = lblError.Text
                ddlFinancialYear.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlStatus.SelectedIndex >= 2 Then
                If txtServiceChargeINR.Text.Trim = "" Then
                    txtServiceChargeINR.Focus()
                    lblError.Text = "Enter Service charge in INR." : lblITValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If txtServiceChargeINR.Text.Trim() <> "" And IsNumeric(txtServiceChargeINR.Text) = False Then
                lblError.Text = "Enter valid numeric values for Service charge in INR." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If

            Dim objITR As New strITReturns_Client
            objITR.iITR_ID = 0
            objITR.sITR_ClientName = objclsGRACeGeneral.SafeSQL(txtClientName.Text.Trim())
            objITR.sITR_PAN = objclsGRACeGeneral.SafeSQL(txtPAN.Text.Trim())
            objITR.sITR_Aadhaar = objclsGRACeGeneral.SafeSQL(txtAadhaar.Text.Trim())
            objITR.dITR_DOB = dDOBDate
            objITR.sITR_Phone = objclsGRACeGeneral.SafeSQL(txtPhone.Text.Trim())
            objITR.sITR_Email = objclsGRACeGeneral.SafeSQL(txtEmail.Text.Trim())
            objITR.sITR_ITLoginId = objclsGRACeGeneral.SafeSQL(txtITLoginId.Text.Trim())
            objITR.sITR_ITPassword = objclsGRACeGeneral.SafeSQL(txtITPassword.Text.Trim())
            objITR.iITR_CrBy = sSession.UserID
            objITR.sITR_IPAddress = sSession.IPAddress
            objITR.iITR_CompID = sSession.AccessCodeID
            Arr = objclsITReturnsFiling.SaveITReturnsClientDetails(sSession.AccessCode, objITR)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "IT Returns Filing", "IT Returns Saved", sSession.YearID, ddlClientName.SelectedValue, ddlFinancialYear.SelectedValue, Arr(1), sSession.IPAddress)

            Dim objITRFD As New strITReturnsFiling_Details
            objITRFD.iITRFD_ID = 0
            objITRFD.iITRFD_ITR_ID = Arr(1)
            objITRFD.iITRFD_FinancialYearID = ddlFinancialYear.SelectedValue
            objITRFD.iITRFD_AssessmentYearID = ddlAssessmentYear.SelectedValue
            If txtServiceChargeINR.Text.Trim() = "" Then dServiceChargeINR = 0 Else dServiceChargeINR = txtServiceChargeINR.Text.Trim()
            objITRFD.dITRFD_ServiceChargeInINR = String.Format("{0:0.00}", Convert.ToDecimal(dServiceChargeINR))
            objITRFD.iITRFD_Status = ddlStatus.SelectedIndex
            If chkInvoiceEmail.Checked = True Then
                objITRFD.iITRFD_InvoiceMail = 1
            Else
                objITRFD.iITRFD_InvoiceMail = 0
            End If
            objITRFD.iITRFD_AssignTo = ddlAssignto.SelectedValue
            objITRFD.iITRFD_BillingEntityId = ddlCompanyName.SelectedValue
            objITRFD.iITRFD_CrBy = sSession.UserID
            objITRFD.sITRFD_IPAddress = sSession.IPAddress
            objITRFD.iITRFD_CompID = sSession.AccessCodeID
            Arr1 = objclsITReturnsFiling.SaveITReturnsClientFilingDetails(sSession.AccessCode, objITRFD, ddlFinancialYear.SelectedItem.Text)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "IT Returns Filing", "IT Returns Filing Saved", sSession.YearID, ddlClientName.SelectedValue, ddlFinancialYear.SelectedValue, Arr1(1), sSession.IPAddress)

            BindClients()
            ddlClientName.SelectedValue = Arr(1)
            ddlClientName_SelectedIndexChanged(sender, e)

            lblError.Text = "Successfully Saved." : lblITValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalITValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Dim Arr1() As String
        Dim dServiceChargeINR As Decimal
        Try
            lblError.Text = ""
            If ddlClientName.SelectedIndex = 0 Then
                ddlClientName.Focus()
                lblError.Text = "Select Client Name." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If lblITRNoId.Text = "0" Then
                lblError.Text = "No data to update." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtClientName.Text.Trim = "" Then
                txtClientName.Focus()
                lblError.Text = "Enter Client Name." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtClientName.Text.Trim.Length > 500 Then
                txtClientName.Focus()
                lblError.Text = "Client Name exceeded maximum size(Max 500 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If (objclsITReturnsFiling.CheckITRClientName(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtClientName.Text.Trim()), ddlClientName.SelectedValue) = True) Then
                lblError.Text = "Client Name already Exists." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalITValidation').modal('show');", True)
                Return
            End If
            If txtPAN.Text.Trim = "" Then
                txtPAN.Focus()
                lblError.Text = "Enter PAN." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtPAN.Text.Trim.Length > 25 Then
                txtPAN.Focus()
                lblError.Text = "PAN exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAadhaar.Text.Trim = "" Then
                txtAadhaar.Focus()
                lblError.Text = "Enter Aadhaar." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAadhaar.Text.Trim.Length > 25 Then
                txtAadhaar.Focus()
                lblError.Text = "Aadhaar exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            Dim dDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dDOBDate As DateTime = Date.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim d As Integer
            d = DateDiff(DateInterval.Year, dDate.AddYears(-10), dDOBDate)
            If d > 0 Then
                lblError.Text = "DOB Date should be less than or equal to " + dDate.AddYears(-10).Year.ToString() : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalITValidation').modal('show');", True)
                txtDOB.Focus()
                Exit Try
            End If
            If txtPhone.Text.Trim = "" Then
                txtPhone.Focus()
                lblError.Text = "Enter Phone." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtPhone.Text.Trim.Length > 25 Then
                txtPhone.Focus()
                lblError.Text = "Phone exceeded maximum size(Max 100 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtEmail.Text.Trim = "" Then
                txtEmail.Focus()
                lblError.Text = "Enter Email." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If txtEmail.Text.Trim.Length > 100 Then
                txtEmail.Focus()
                lblError.Text = "Email exceeded maximum size(Max 100 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlAssignto.SelectedIndex = 0 Then
                lblError.Text = "Select Assign to." : lblITValidationMsg.Text = lblError.Text
                ddlAssignto.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            'If txtITLoginId.Text.Trim = "" Then
            '    txtITLoginId.Focus()
            '    lblError.Text = "Enter IT Login Id." : lblITValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If txtITLoginId.Text.Trim.Length > 100 Then
                txtITLoginId.Focus()
                lblError.Text = "IT Login Id exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            'If txtITPassword.Text.Trim = "" Then
            '    txtITPassword.Focus()
            '    lblError.Text = "Enter IT Password." : lblITValidationMsg.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If txtITPassword.Text.Trim.Length > 100 Then
                txtITPassword.Focus()
                lblError.Text = "IT Password exceeded maximum size(Max 25 characters)." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            'If ddlFinancialYear.SelectedIndex = 0 Then
            '    lblError.Text = "Select Financial Year." : lblITValidationMsg.Text = lblError.Text
            '    ddlFinancialYear.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            'If ddlAssessmentYear.SelectedIndex = 0 Then
            '    ddlAssessmentYear.Text = "Select Assessment Year." : lblITValidationMsg.Text = lblError.Text
            '    ddlFinancialYear.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
            '    Exit Sub
            'End If
            If ddlStatus.SelectedIndex = 0 Then
                ddlStatus.Text = "Select Status." : lblITValidationMsg.Text = lblError.Text
                ddlFinancialYear.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlStatus.SelectedIndex >= 2 Then
                If txtServiceChargeINR.Text.Trim = "" Then
                    txtServiceChargeINR.Focus()
                    lblError.Text = "Enter Service charge in INR." : lblITValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If txtServiceChargeINR.Text.Trim() <> "" And IsNumeric(txtServiceChargeINR.Text) = False Then
                lblError.Text = "Enter valid numeric values for Service charge in INR." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalITValidation').modal('show');", True)
                Exit Sub
            End If

            Dim objITR As New strITReturns_Client
            objITR.iITR_ID = ddlClientName.SelectedValue
            objITR.sITR_ClientName = objclsGRACeGeneral.SafeSQL(txtClientName.Text.Trim())
            objITR.sITR_PAN = objclsGRACeGeneral.SafeSQL(txtPAN.Text.Trim())
            objITR.sITR_Aadhaar = objclsGRACeGeneral.SafeSQL(txtAadhaar.Text.Trim())
            objITR.dITR_DOB = dDOBDate
            objITR.sITR_Phone = objclsGRACeGeneral.SafeSQL(txtPhone.Text.Trim())
            objITR.sITR_Email = objclsGRACeGeneral.SafeSQL(txtEmail.Text.Trim())
            objITR.sITR_ITLoginId = objclsGRACeGeneral.SafeSQL(txtITLoginId.Text.Trim())
            objITR.sITR_ITPassword = objclsGRACeGeneral.SafeSQL(txtITPassword.Text.Trim())
            objITR.iITR_CrBy = sSession.UserID
            objITR.iITR_UpdatedBy = sSession.UserID
            objITR.sITR_IPAddress = sSession.IPAddress
            objITR.iITR_CompID = sSession.AccessCodeID
            Arr = objclsITReturnsFiling.SaveITReturnsClientDetails(sSession.AccessCode, objITR)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "IT Returns Filing", "IT Returns Updated", sSession.YearID, ddlClientName.SelectedValue, ddlFinancialYear.SelectedValue, Arr(1), sSession.IPAddress)

            Dim objITRFD As New strITReturnsFiling_Details
            objITRFD.iITRFD_ID = Val(lblITRNoId.Text)
            objITRFD.iITRFD_ITR_ID = Arr(1)
            objITRFD.iITRFD_FinancialYearID = ddlFinancialYear.SelectedValue
            objITRFD.iITRFD_AssessmentYearID = ddlAssessmentYear.SelectedValue
            If txtServiceChargeINR.Text.Trim() = "" Then dServiceChargeINR = 0 Else dServiceChargeINR = txtServiceChargeINR.Text.Trim()
            objITRFD.dITRFD_ServiceChargeInINR = String.Format("{0:0.00}", Convert.ToDecimal(dServiceChargeINR))
            objITRFD.iITRFD_Status = ddlStatus.SelectedIndex
            If chkInvoiceEmail.Checked = True Then
                objITRFD.iITRFD_InvoiceMail = 1
            Else
                objITRFD.iITRFD_InvoiceMail = 0
            End If
            objITRFD.iITRFD_AssignTo = ddlAssignto.SelectedValue
            objITRFD.iITRFD_BillingEntityId = ddlCompanyName.SelectedValue
            objITRFD.iITRFD_CrBy = sSession.UserID
            objITRFD.iITRFD_UpdatedBy = sSession.UserID
            objITRFD.sITRFD_IPAddress = sSession.IPAddress
            objITRFD.iITRFD_CompID = sSession.AccessCodeID
            Arr1 = objclsITReturnsFiling.SaveITReturnsClientFilingDetails(sSession.AccessCode, objITRFD, ddlFinancialYear.SelectedItem.Text)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "IT Returns Filing", "IT Returns Filing Updated", sSession.YearID, ddlClientName.SelectedValue, ddlFinancialYear.SelectedValue, Arr1(1), sSession.IPAddress)

            BindClients()
            ddlClientName.SelectedValue = Arr(1)
            ddlClientName_SelectedIndexChanged(sender, e)

            lblError.Text = "Successfully Updated." : lblITValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalITValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvITR_PreRender(sender As Object, e As EventArgs) Handles gvITR.PreRender
        Dim dt As New DataTable
        Try
            If gvITR.Rows.Count > 0 Then
                gvITR.UseAccessibleHeader = True
                gvITR.HeaderRow.TableSection = TableRowSection.TableHeader
                gvITR.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvITR_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvITR_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvITR.RowCommand
        Dim lblITRFID As New Label, lblClientID As New Label
        Try
            lblError.Text = ""
            Clear()
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblClientID = CType(clickedRow.FindControl("lblClientID"), Label)
                ddlClientName.SelectedValue = Val(lblClientID.Text)
                BindExistingITReturnFiling()
            End If
            If e.CommandName = "Download" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblITRFID = CType(clickedRow.FindControl("lblITRFID"), Label)
                lblClientID = CType(clickedRow.FindControl("lblClientID"), Label)
                GenerateInvoice(Val(lblITRFID.Text), Val(lblClientID.Text))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvITR_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvITR_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvITR.RowDataBound
        Dim imgbtnDownload As New ImageButton
        Dim lblStatusID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnDownload = CType(e.Row.FindControl("imgbtnDownload"), ImageButton)
                lblStatusID = CType(e.Row.FindControl("lblStatusID"), Label)
                If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                    imgbtnDownload.ImageUrl = "~/Images/Download16.png"
                    imgbtnDownload.Visible = False
                    If Val(lblStatusID.Text) >= 2 Then
                        imgbtnDownload.Visible = True
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvITR_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub GenerateInvoice(ByVal iITRFID As Integer, ByVal iITRClientID As Integer)
        Dim mimeType As String = Nothing
        Dim dtCompany As New DataTable, dtClient As New DataTable, dtITRFDDetails As New DataTable
        Dim sReportNo As String, sReportName As String
        Try
            dtITRFDDetails = objclsITReturnsFiling.LoadClientITRFDDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iITRFID)
            Dim iBillingEntity As Integer = objclsITReturnsFiling.GetEntityIDFormClientDetails(sSession.AccessCode, sSession.AccessCodeID, iITRFID)
            Dim sAmount As String = objclsITReturnsFiling.GetAmountFormClientDetails(sSession.AccessCode, sSession.AccessCodeID, iITRFID)
            Dim sLogoName As String = "" : Dim imageBase64DataLogoString As String = ""
            Dim objclsCompanyDetails As New clsCompanyDetails
            sLogoName = objclsCompanyDetails.getCompanyImageName(sSession.AccessCode, iBillingEntity, "A")
            If sLogoName <> "" And sLogoName <> "." Then
                Dim imageDataURL As String = Server.MapPath("~/Images/" + sLogoName)
                If System.IO.File.Exists(imageDataURL) = True Then
                    Dim logoInBytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                    imageBase64DataLogoString = Convert.ToBase64String(logoInBytes)
                End If
            End If
            Dim imageBase64DataSignatureString As String = ""
            Dim iSignatureID As Integer = objclsAuditAssignment.GetUserSignatureID(sSession.AccessCode, sSession.AccessCodeID, dtITRFDDetails.Rows(0)("PricePerUnit"))
            If iSignatureID > 0 Then
                Dim objclsEProfile As New clsEProfile
                Dim objclsAttachments As New clsAttachments
                Dim iSignatureDocID As Integer = objclsEProfile.GetPhotoDocID(sSession.AccessCode, sSession.AccessCodeID, iSignatureID)
                Dim sPaths As String = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                Dim sDestFilePath As String = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iSignatureID, iSignatureDocID)
                If System.IO.Directory.Exists(sPaths) = True And System.IO.File.Exists(sDestFilePath) = True Then
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sDestFilePath)
                    imageBase64DataSignatureString = Convert.ToBase64String(bytes)
                End If
            End If

            dtCompany = objclsAuditAssignment.LoadCompanyLogoSignatureDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iBillingEntity, imageBase64DataLogoString, imageBase64DataSignatureString)
            dtClient = objclsITReturnsFiling.LoadClientDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iITRClientID)
            If dtCompany.Rows.Count = 0 Then
                lblError.Text = "Please update Company details in Master module." : lblITValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Visible = True
            ReportViewer1.Reset()
            Dim rdCompany As New ReportDataSource("DataSet1", dtCompany)
            Dim rdCustomer As New ReportDataSource("DataSet2", dtClient)
            Dim rdITRFDDetails As New ReportDataSource("DataSet3", dtITRFDDetails)
            ReportViewer1.LocalReport.DataSources.Add(rdCompany)
            ReportViewer1.LocalReport.DataSources.Add(rdCustomer)
            ReportViewer1.LocalReport.DataSources.Add(rdITRFDDetails)

            sReportNo = dtITRFDDetails.Rows(0)("ItemName")
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Assignment/ITReturns.rdlc")

            Dim ReportTitle As ReportParameter()
            Dim InvoiceNo As ReportParameter()

            ReportTitle = New ReportParameter() {New ReportParameter("ReportTitle", "ITR Filing Invoice")}
            InvoiceNo = New ReportParameter() {New ReportParameter("InvoiceNo", "ITR No: " & sReportNo)}
            sReportName = "ITR Filing Invoice"

            Dim FinalTotal As ReportParameter() = New ReportParameter() {New ReportParameter("FinalTotal", "₹" & String.Format("{0:0.00}", sAmount))}
            Dim AmountInWords As ReportParameter() = New ReportParameter() {New ReportParameter("AmountInWords", objclsAuditAssignment.NumberToWord(String.Format("{0:0.00}", sAmount)) & " Only")}

            ReportViewer1.LocalReport.SetParameters(ReportTitle)
            ReportViewer1.LocalReport.SetParameters(InvoiceNo)
            ReportViewer1.LocalReport.SetParameters(FinalTotal)
            ReportViewer1.LocalReport.SetParameters(AmountInWords)

            Dim CurrentDate As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentDate", "Date:" & objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode))}
            Dim SubTotal As ReportParameter() = New ReportParameter() {New ReportParameter("SubTotal", "₹" & String.Format("{0:0.00}", sAmount))}
            Dim Total As ReportParameter() = New ReportParameter() {New ReportParameter("Total", "₹" & String.Format("{0:0.00}", sAmount))}
            ReportViewer1.LocalReport.SetParameters(CurrentDate)
            ReportViewer1.LocalReport.SetParameters(SubTotal)
            ReportViewer1.LocalReport.SetParameters(Total)
            ReportViewer1.LocalReport.Refresh()

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "ITReturns", "PDF", sSession.YearID, "", iITRFID, "", sSession.IPAddress)
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = mimeType
            Dim sFileName As String = Regex.Replace(sReportName, " \ s", "")
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & "_" & iITRFID & ".pdf")
            HttpContext.Current.Response.BinaryWrite(RptViewer)
            HttpContext.Current.Response.Flush()
            'HttpContext.Current.Response.End()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateInvoice" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class