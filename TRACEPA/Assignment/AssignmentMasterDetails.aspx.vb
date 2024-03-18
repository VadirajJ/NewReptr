Imports System
Imports System.Data
Imports BusinesLayer
Public Class AssignmentMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "AssignmentMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    Private Shared iAuditAssignmentID As Integer
    'Private Shared sGMSave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Dim iAuditAssignmentSubTaskID As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                'sGMSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MSGM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/Digital_AuditOfficePermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '        sGMSave = "YES"
                '    End If
                'End If

                If Request.QueryString("StatusID") IsNot Nothing Then
                    sGMBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                txtCode.Text = "AAST_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditAssignmentSubTask_Master", "AM_ID", "AM_CompId")
                iAuditAssignmentID = 0
                If Request.QueryString("AuditAssignmentID") IsNot Nothing Then
                    iAuditAssignmentID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditAssignmentID")))
                    lblAuditAssigment.Text = objclsAdminMaster.GetAuditAssignmentName(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID)
                End If
                BindDescDetails(iAuditAssignmentID) : BindBillingType()
                If Request.QueryString("AuditAssignmentSubTaskID") IsNot Nothing Then
                    iAuditAssignmentSubTaskID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditAssignmentSubTaskID")))
                    ddlDesc.SelectedValue = iAuditAssignmentSubTaskID
                    ddlDesc_SelectedIndexChanged(sender, e)
                End If
                REVDescName.ValidationGroup = False
                RFVCode.ValidationGroup = False

                RFVDescName.ValidationGroup = True : RFVCode.ValidationGroup = True
                RFVDescName.ErrorMessage = "Enter Assignment Sub Task Name."
                RFVDescName.ValidationGroup = "Validate" : REVDescName.ValidationGroup = "Validate"
                RFVCode.ValidationGroup = "Validate" : REVNotes.ValidationGroup = "Validate"
                RFVDescName.ControlToValidate = "txtDesc" : REVDescName.ValidationExpression = "^[\s\S]{0,100}$" : REVDescName.ErrorMessage = "Assignment Sub Task exceeded maximum size(max 100 characters)."
                RFVCode.ControlToValidate = "txtCode" : RFVCode.ErrorMessage = "Enter Code."
                REVNotes.ControlToValidate = "txtNotes" : REVNotes.ValidationExpression = "^[\s\S]{0,100}$" : REVNotes.ErrorMessage = "Notes exceeded maximum size(max 100 characters)."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub BindDescDetails(ByVal iAuditAssignmentID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAllAuditAssignmentSTDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID)
            ddlDesc.DataSource = dt
            ddlDesc.DataTextField = "AM_Name"
            ddlDesc.DataValueField = "AM_ID"
            ddlDesc.DataBind()
            ddlDesc.Items.Insert(0, "Select Assignment Sub Task")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindBillingType()
        Try
            ddlBillingType.Items.Insert(0, "Billable")
            ddlBillingType.Items.Insert(1, "Non Billable")
            ddlBillingType.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesc.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            lblGeneralMasterStatus.Text = ""
            txtDesc.Text = "" : txtNotes.Text = ""
            'If sGMSave = "YES" Then
            imgbtnSave.Visible = True
            'Else
            '    imgbtnSave.Visible = False
            'End If
            imgbtnUpdate.Visible = False
            If ddlDesc.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                'If sGMSave = "YES" Then
                imgbtnUpdate.Visible = True
                'Else
                '    imgbtnUpdate.Visible = False
                'End If
                dt = objclsAdminMaster.GetAuditAssignmentSTDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("AM_Name")) = False Then
                        txtDesc.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_Name")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("AM_CODE")) = False Then
                        txtCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_CODE")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("AM_Desc")) = False Then
                        txtNotes.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_Desc")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("AM_BillingTypeID")) = False Then
                        ddlBillingType.SelectedIndex = dt.Rows(0).Item("AM_BillingTypeID")
                    End If
                    If IsDBNull(dt.Rows(0).Item("AM_DELFLG")) = False Then
                        sGMFlag = dt.Rows(0).Item("AM_DELFLG")
                    End If
                End If
                If sGMFlag = "W" Then
                    lblGeneralMasterStatus.Text = "Waiting for Approval"
                    'If sGMSave = "YES" Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    'End If
                ElseIf sGMFlag = "D" Then
                    lblGeneralMasterStatus.Text = "De-Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                Else
                    lblGeneralMasterStatus.Text = "Activated"
                    'If sGMSave = "YES" Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    'End If
                End If
            Else
                txtCode.Text = "AAST_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditAssignmentSubTask_Master", "AM_ID", "AM_CompId")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDesc_SelectedIndexChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnBack.Visible = True : imgbtnUpdate.Visible = False
            'If sGMSave = "YES" Then
            imgbtnSave.Visible = True
            'Else
            '    imgbtnSave.Visible = False
            'End If
            ddlDesc.SelectedIndex = 0 : lblGeneralMasterStatus.Text = ""
            txtDesc.Text = "" : txtNotes.Text = ""
            txtCode.Text = "AAST_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditAssignmentSubTask_Master", "AM_ID", "AM_CompId")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If txtDesc.Text.Trim = "" Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Assignment Sub Task." : lblError.Text = "Enter Assignment Sub Task."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDesc.Text.Trim.Length > 100 Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Assignment Sub Task exceeded maximum size(Max 100 characters)." : lblError.Text = "Assignment Sub Task exceeded maximum size(Max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim = "" Then
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Code." : lblError.Text = "Enter Code."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim.Length > 10 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Code exceeded maximum size(max 10 characters)." : lblError.Text = "Code exceeded maximum size(max 10 characters)."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtNotes.Text.Trim.Length > 100 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Note exceeded maximum size(max 100 characters)." : lblError.Text = "Note exceeded maximum size(max 100 characters)."
                txtNotes.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDesc.SelectedIndex > 0 Then
                'To check Description Deleted or not
                bCheck = objclsAdminMaster.CheckAuditAssignmentSTDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), ddlDesc.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated description cannot be updated." : lblError.Text = "De-Activated description cannot be updated."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), "AM_CODE", ddlDesc.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), "AM_Name", ddlDesc.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Assignment Sub Task Name already exist." : lblError.Text = "Entered Assignment Sub Task Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), "AM_CODE", 0)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), "AM_Name", 0)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Assignment Sub Task Name already exist." : lblError.Text = "Entered Assignment Sub Task Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If

            If ddlDesc.SelectedIndex = 0 Then
                objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditAssignmentSubTask_Master", "AM_ID", "AM_CompId")
                objclsAdminMaster.sCode = "AAST_" & objclsAdminMaster.iID
                objclsAdminMaster.iID = 0
            Else
                objclsAdminMaster.iID = ddlDesc.SelectedValue
                objclsAdminMaster.sCode = objclsGRACeGeneral.SafeSQL(txtCode.Text)
            End If
            objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim)
            objclsAdminMaster.iAuditAssignment = iAuditAssignmentID
            objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(txtNotes.Text.Trim)
            objclsAdminMaster.iBillingType = ddlBillingType.SelectedIndex
            objclsAdminMaster.sDelflag = "W"
            objclsAdminMaster.sStatus = "C"
            objclsAdminMaster.iCrBy = sSession.UserID
            objclsAdminMaster.iUpdatedBy = sSession.UserID
            objclsAdminMaster.sIpAddress = sSession.IPAddress
            objclsAdminMaster.iCompId = sSession.AccessCodeID
            Arr = objclsAdminMaster.SaveAssignmentSubTaskMasterDetails(sSession.AccessCode, objclsAdminMaster)

            BindDescDetails(iAuditAssignmentID)
            ddlDesc.SelectedValue = Arr(1)
            ddlDesc_SelectedIndexChanged(sender, e)
            If Arr(0) = "3" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Saved", iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(lblAuditAssigment.Text), Arr(1), objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sSession.IPAddress)
                lblGeneralMasterDetailsValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                sGMBackStatus = 2
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim iMasterID As Integer
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If txtDesc.Text.Trim = "" Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Assignment Sub Task." : lblError.Text = "Enter Assignment Sub Task."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDesc.Text.Trim.Length > 100 Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Assignment Sub Task exceeded maximum size(Max 100 characters)." : lblError.Text = "Assignment Sub Task exceeded maximum size(Max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim = "" Then
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Code." : lblError.Text = "Enter Code."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim.Length > 10 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Code exceeded maximum size(max 10 characters)." : lblError.Text = "Code exceeded maximum size(max 10 characters)."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtNotes.Text.Trim.Length > 100 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Note exceeded maximum size(max 100 characters)." : lblError.Text = "Note exceeded maximum size(max 100 characters)."
                txtNotes.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDesc.SelectedIndex > 0 Then
                iMasterID = ddlDesc.SelectedValue
                'To check Description Deleted or not
                bCheck = objclsAdminMaster.CheckAuditAssignmentSTDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), ddlDesc.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated description cannot be updated." : lblError.Text = "De-Activated description cannot be updated."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), "AM_CODE", ddlDesc.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), "AM_Name", ddlDesc.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Assignment Sub Task Name already exist." : lblError.Text = "Entered Assignment Sub Task Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                iMasterID = 0
                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), "AM_CODE", 0)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAdminMaster.CheckAuditAssignmentSTExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), "AM_Name", 0)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Assignment Sub Task Name already exist." : lblError.Text = "Entered Assignment Sub Task Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If iMasterID = 0 Then
                objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditAssignmentSubTask_Master", "AM_ID", "AM_CompId")
                objclsAdminMaster.sCode = "AAST_" & objclsAdminMaster.iID
                objclsAdminMaster.iID = 0
            Else
                objclsAdminMaster.iID = ddlDesc.SelectedValue
                objclsAdminMaster.sCode = objclsGRACeGeneral.SafeSQL(txtCode.Text)
            End If
            objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim)
            objclsAdminMaster.sCategory = "AAST"
            objclsAdminMaster.iAuditAssignment = iAuditAssignmentID
            objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(txtNotes.Text.Trim)
            objclsAdminMaster.iBillingType = ddlBillingType.SelectedIndex
            objclsAdminMaster.sDelflag = "W"
            objclsAdminMaster.sStatus = "U"
            objclsAdminMaster.iCrBy = sSession.UserID
            objclsAdminMaster.iUpdatedBy = sSession.UserID
            objclsAdminMaster.sIpAddress = sSession.IPAddress
            objclsAdminMaster.iCompId = sSession.AccessCodeID
            Arr = objclsAdminMaster.SaveAssignmentSubTaskMasterDetails(sSession.AccessCode, objclsAdminMaster)

            BindDescDetails(iAuditAssignmentID)
            ddlDesc.SelectedValue = Arr(1)
            ddlDesc_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Updated", iAuditAssignmentID, objclsGRACeGeneral.SafeSQL(lblAuditAssigment.Text), Arr(1), objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sSession.IPAddress)
                If sGMFlag = "W" Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                Else
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oAuditAssignmentID As Object
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sGMBackStatus))
            oAuditAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iAuditAssignmentID))
            Response.Redirect(String.Format("~/Assignment/AssignmentMaster.aspx?StatusID={0}&AuditAssignmentID={1}", oStatusID, oAuditAssignmentID), False) 'Masters/GeneralMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class

