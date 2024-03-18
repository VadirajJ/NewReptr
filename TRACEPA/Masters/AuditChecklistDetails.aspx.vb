Imports System
Imports System.Data
Imports BusinesLayer
Public Class AuditChecklistDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "AuditChecklistDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAuditChecklist As New clsAuditChecklist

    Private Shared sSession As AllSession
    Private Shared iAuditTypeID As Integer
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Private Shared iHeadingTypeID As Integer
    Private Shared sOldHeading As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddHeading.ImageUrl = "~/Images/Add16.png"
        imgbtnEditHeading.ImageUrl = "~/Images/Update24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iAuditChecklistID As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sGMBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                txtCode.Text = "ACP_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditType_Checklist_Master", "ACM_ID", "ACM_ID")
                iAuditTypeID = 0
                If Request.QueryString("AuditTypeID") IsNot Nothing Then
                    iAuditTypeID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditTypeID")))
                    lblAuditType.Text = objclsAuditChecklist.GetAuditTypeName(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID)
                End If
                BindHeadingDetails()
                BindAuditChecklistDetails(iAuditTypeID)
                If Request.QueryString("AuditChecklistID") IsNot Nothing Then
                    iAuditChecklistID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditChecklistID")))
                    ddlAuditChecklist.SelectedValue = iAuditChecklistID
                    ddlAuditChecklist_SelectedIndexChanged(sender, e)
                End If

                REVHeading.ValidationExpression = "^[\s\S]{0,2000}$" : REVHeading.ErrorMessage = "Heading exceeded maximum size(max 2000 characters)."
                RFVCheckpoint.ErrorMessage = "Enter Checkpoint Name."
                REVCheckpoint.ValidationExpression = "^[\s\S]{0,8000}$" : REVCheckpoint.ErrorMessage = "Checkpoint exceeded maximum size(max 8000 characters)."
                RFVHeading.InitialValue = "0" : RFVHeading.ErrorMessage = "Select Heading."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAuditChecklistDetails(ByVal iAuditTypeID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsAuditChecklist.LoadAllAuditTypeChecklistDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID)
            ddlAuditChecklist.DataSource = dt
            ddlAuditChecklist.DataTextField = "ACM_Checkpoint"
            ddlAuditChecklist.DataValueField = "ACM_ID"
            ddlAuditChecklist.DataBind()
            ddlAuditChecklist.Items.Insert(0, "Select Audit Checklist")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub BindHeadingDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAuditChecklist.LoadHeading(sSession.AccessCode, sSession.AccessCodeID)
            ddlHeading.Items.Clear()
            ddlHeading.DataSource = dt
            ddlHeading.DataTextField = "ACM_Heading"
            ddlHeading.DataBind()
            ddlHeading.Items.Insert(0, New ListItem("Select", "0"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlAuditChecklist_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditChecklist.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblModelError.Text = ""
            lblStatus.Text = ""
            ddlHeading.SelectedIndex = 0
            txtHeading.Text = "" : txtCheckpoint.Text = ""
            imgbtnSave.Visible = True
            imgbtnUpdate.Visible = False
            If ddlAuditChecklist.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                imgbtnUpdate.Visible = True
                dt = objclsAuditChecklist.GetAuditTypeChecklistDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditChecklist.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("ACM_CODE")) = False Then
                        txtCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("ACM_CODE")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("ACM_Checkpoint")) = False Then
                        txtCheckpoint.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("ACM_Checkpoint")))
                    End If
                    If IsDBNull(dt.Rows(0).Item("ACM_Heading")) = False Then
                        ddlHeading.Items.Clear()
                        BindHeadingDetails()
                        If ddlAuditChecklist.SelectedIndex >= 1 Then
                            ddlHeading.SelectedIndex = ddlHeading.Items.IndexOf(ddlHeading.Items.FindByText(Trim(dt.Rows(0).Item("ACM_Heading"))))
                        End If
                    End If
                    If IsDBNull(dt.Rows(0).Item("ACM_DELFLG")) = False Then
                        sGMFlag = dt.Rows(0).Item("ACM_DELFLG")
                    End If
                End If
                If sGMFlag = "W" Then
                    lblStatus.Text = "Waiting for Approval"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                ElseIf sGMFlag = "D" Then
                    lblStatus.Text = "De-Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                Else
                    lblStatus.Text = "Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                End If
            Else
                txtCode.Text = "ACP_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditType_Checklist_Master", "ACM_ID", "ACM_CompId")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditChecklist_SelectedIndexChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : lblModelError.Text = ""
            imgbtnAdd.Visible = True : imgbtnBack.Visible = True : imgbtnUpdate.Visible = False
            imgbtnSave.Visible = True
            ddlAuditChecklist.SelectedIndex = 0 : lblStatus.Text = ""
            ddlHeading.SelectedIndex = 0
            txtHeading.Text = "" : txtCheckpoint.Text = ""
            txtCode.Text = "ACP_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditType_Checklist_Master", "ACM_ID", "ACM_CompId")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If txtCheckpoint.Text.Trim = "" Then
                txtCheckpoint.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Checkpoint." : lblError.Text = "Enter Checkpoint."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCheckpoint.Text.Trim.Length > 8000 Then
                txtCheckpoint.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Checkpoint exceeded maximum size(Max 8000 characters)." : lblError.Text = "Checkpoint exceeded maximum size(Max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlHeading.SelectedIndex = 0 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Select Heading." : lblError.Text = "Select Heading."
                ddlHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlAuditChecklist.SelectedIndex > 0 Then
                bCheck = objclsAuditChecklist.CheckAuditTypeChecklistDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), ddlAuditChecklist.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated Checkpoint cannot be updated." : lblError.Text = "De-Activated Checkpoint cannot be updated."
                    txtCheckpoint.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                bCheck = objclsAuditChecklist.CheckAuditTypeChecklistExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID, objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), "ACM_Checkpoint", ddlAuditChecklist.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Checkpoint Name already exist." : lblError.Text = "Entered Checkpoint Name already exist."
                    txtCheckpoint.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                bCheck = objclsAuditChecklist.CheckAuditTypeChecklistExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID, objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), "ACM_Checkpoint", 0)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Checkpoint Name already exist." : lblError.Text = "Entered Checkpoint Name already exist."
                    txtCheckpoint.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If

            If ddlAuditChecklist.SelectedIndex = 0 Then
                objclsAuditChecklist.iID = 0
                objclsAuditChecklist.sCode = "ACP_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditType_Checklist_Master", "ACM_ID", "ACM_CompId")
            Else
                objclsAuditChecklist.iID = ddlAuditChecklist.SelectedValue
                objclsAuditChecklist.sCode = objclsGRACeGeneral.SafeSQL(txtCode.Text)
            End If
            objclsAuditChecklist.iAuditTypeID = iAuditTypeID
            If ddlHeading.SelectedIndex >= 1 Then
                objclsAuditChecklist.sHeading = objclsGRACeGeneral.SafeSQL(ddlHeading.SelectedItem.Text)
            Else
                objclsAuditChecklist.sHeading = ""
            End If
            objclsAuditChecklist.sCheckpoint = objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim)
            objclsAuditChecklist.sDelflag = "W"
            objclsAuditChecklist.sStatus = "C"
            objclsAuditChecklist.iCrBy = sSession.UserID
            objclsAuditChecklist.iUpdatedBy = sSession.UserID
            objclsAuditChecklist.sIpAddress = sSession.IPAddress
            objclsAuditChecklist.iCompId = sSession.AccessCodeID
            Arr = objclsAuditChecklist.SaveAuditTypeChecklistMasterDetails(sSession.AccessCode, objclsAuditChecklist)

            BindAuditChecklistDetails(iAuditTypeID)
            ddlAuditChecklist.SelectedValue = Arr(1)
            ddlAuditChecklist_SelectedIndexChanged(sender, e)
            If Arr(0) = "3" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklis Master", "Saved", iAuditTypeID, objclsGRACeGeneral.SafeSQL(lblAuditType.Text), Arr(1), objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), sSession.IPAddress)
                lblGeneralMasterDetailsValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                sGMBackStatus = 2
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim iMasterID As Integer
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""

            If txtCheckpoint.Text.Trim = "" Then
                txtCheckpoint.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Checkpoint." : lblError.Text = "Enter Checkpoint."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCheckpoint.Text.Trim.Length > 8000 Then
                txtCheckpoint.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Checkpoint exceeded maximum size(Max 8000 characters)." : lblError.Text = "Checkpoint exceeded maximum size(Max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlHeading.SelectedIndex = 0 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Select Heading." : lblError.Text = "Select Heading."
                ddlHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlAuditChecklist.SelectedIndex > 0 Then
                iMasterID = ddlAuditChecklist.SelectedValue
                'To check Description Deleted or not
                bCheck = objclsAuditChecklist.CheckAuditTypeChecklistDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), ddlAuditChecklist.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated Checkpoint cannot be updated." : lblError.Text = "De-Activated Checkpoint cannot be updated."
                    txtCheckpoint.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
                bCheck = objclsAuditChecklist.CheckAuditTypeChecklistExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID, objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), "ACM_Checkpoint", ddlAuditChecklist.SelectedValue)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Checkpoint Name already exist." : lblError.Text = "Entered Checkpoint Name already exist."
                    txtCheckpoint.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                iMasterID = 0
                bCheck = objclsAuditChecklist.CheckAuditTypeChecklistExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID, objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), "ACM_Checkpoint", 0)
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered Checkpoint Name already exist." : lblError.Text = "Entered Checkpoint Name already exist."
                    txtCheckpoint.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If iMasterID = 0 Then
                objclsAuditChecklist.iID = 0
                objclsAuditChecklist.sCode = "AAST_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditType_Checklist_Master", "ACM_ID", "ACM_CompId")
            Else
                objclsAuditChecklist.iID = ddlAuditChecklist.SelectedValue
                objclsAuditChecklist.sCode = objclsGRACeGeneral.SafeSQL(txtCode.Text)
            End If
            objclsAuditChecklist.iAuditTypeID = iAuditTypeID
            If ddlHeading.SelectedIndex >= 1 Then
                objclsAuditChecklist.sHeading = objclsGRACeGeneral.SafeSQL(ddlHeading.SelectedItem.Text)
            Else
                objclsAuditChecklist.sHeading = ""
            End If
            objclsAuditChecklist.sCheckpoint = objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim)
            objclsAuditChecklist.sDelflag = "W"
            objclsAuditChecklist.sStatus = "U"
            objclsAuditChecklist.iCrBy = sSession.UserID
            objclsAuditChecklist.iUpdatedBy = sSession.UserID
            objclsAuditChecklist.sIpAddress = sSession.IPAddress
            objclsAuditChecklist.iCompId = sSession.AccessCodeID
            Arr = objclsAuditChecklist.SaveAuditTypeChecklistMasterDetails(sSession.AccessCode, objclsAuditChecklist)

            BindAuditChecklistDetails(iAuditTypeID)
            ddlAuditChecklist.SelectedValue = Arr(1)
            ddlAuditChecklist_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklis Master", "Updated", iAuditTypeID, objclsGRACeGeneral.SafeSQL(lblAuditType.Text), Arr(1), objclsGRACeGeneral.SafeSQL(txtCheckpoint.Text.Trim), sSession.IPAddress)
                If sGMFlag = "W" Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                Else
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterDetailsValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oAuditTypeID As Object
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sGMBackStatus))
            oAuditTypeID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iAuditTypeID))
            Response.Redirect(String.Format("~/Masters/AuditChecklist.aspx?StatusID={0}&AuditTypeID={1}", oStatusID, oAuditTypeID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAddHeading_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddHeading.Click
        Try
            lblError.Text = "" : lblModelError.Text = ""
            txtHeading.Text = ""
            iHeadingTypeID = 0
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalHeading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddHeading_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditHeading_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditHeading.Click
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlHeading.SelectedIndex = 0 Then
                lblError.Text = "Select Heading." : lblModelError.Text = "Select Heading."
                ddlHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalHeading').modal('show');", True)
                Exit Sub
            End If
            txtHeading.Text = ddlHeading.SelectedItem.Text
            sOldHeading = ddlHeading.SelectedItem.Text
            iHeadingTypeID = 1
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalHeading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditHeading_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSavedetails_Click(sender As Object, e As EventArgs) Handles btnSavedetails.Click
        Dim dtAct As New DataTable
        Try
            lblError.Text = ""
            If txtHeading.Text.Trim.Length = 0 Then
                lblError.Text = "Enter Heading." : lblModelError.Text = "Enter Heading."
                txtHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalHeading').modal('show');", True)
                Exit Sub
            End If
            If txtHeading.Text.Trim.Length > 2000 Then
                lblError.Text = "Heading exceeded maximum size(max 2000 characters)." : lblModelError.Text = "Heading exceeded maximum size(max 2000 characters)."
                txtHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalHeading').modal('show');", True)
                Exit Sub
            End If
            If iHeadingTypeID = 0 Then
                ddlHeading.Items.Add(New ListItem(txtHeading.Text.Trim))
                ddlHeading.SelectedIndex = ddlHeading.Items.IndexOf(ddlHeading.Items.FindByText(objclsGRACeGeneral.ReplaceSafeSQL(txtHeading.Text.Trim)))
            Else
                objclsAuditChecklist.UpdateHeading(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.ReplaceSafeSQL(txtHeading.Text.Trim), objclsGRACeGeneral.ReplaceSafeSQL(sOldHeading.Trim))
                BindHeadingDetails()
                ddlHeading.SelectedIndex = ddlHeading.Items.IndexOf(ddlHeading.Items.FindByText(objclsGRACeGeneral.ReplaceSafeSQL(txtHeading.Text.Trim)))
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class