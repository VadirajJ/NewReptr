Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WebForms
Public Class AssignmentMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "AssignmentMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    'Private Shared sSGMSave As String
    'Private Shared sSGMAD As String
    'Private Shared sSGMRpt As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Dim iAuditAssignmentID As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                'sSGMSave = "NO" : sSGMAD = "NO" : sSGMRpt = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MSGM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/Digital_AuditOfficePermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        sSGMSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Approve/Activate/De-Activate,") = True Then
                '        sSGMAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        sSGMRpt = "YES"
                '    End If
                'End If

                BindAuditAssignments() : BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("AuditAssignmentID") IsNot Nothing Then
                    iAuditAssignmentID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditAssignmentID")))
                    ddlAuditAssignment.SelectedValue = iAuditAssignmentID
                    ddlAuditAssignment_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindAuditAssignments()
        Try
            ddlAuditAssignment.DataSource = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlAuditAssignment.DataTextField = "Name"
            ddlAuditAssignment.DataValueField = "PKID"
            ddlAuditAssignment.DataBind()
            ddlAuditAssignment.Items.Insert(0, "Select Audit Task/Assignments")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Waiting for Approval")
            ddlStatus.Items.Insert(3, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindAuditAssignmentsMasterGridDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal iAuditAssignmentID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAuditAssignmentSTMasterGridDetails(sSession.AccessCode, sSession.AccessCodeID, iStatus, iAuditAssignmentID, "")
            gvGeneralMaster.DataSource = dt
            gvGeneralMaster.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlAuditAssignment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditAssignment.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
            If ddlAuditAssignment.SelectedIndex > 0 Then
                If ddlAuditAssignment.SelectedIndex > 0 Then
                    'If sSGMRpt = "YES" Then
                    imgbtnReport.Visible = True
                    'End If
                    If ddlStatus.SelectedIndex = 0 Then
                        'If sSGMAD = "YES" Then
                        imgbtnDeActivate.Visible = True
                        'End If
                    ElseIf ddlStatus.SelectedIndex = 1 Then
                        'If sSGMAD = "YES" Then
                        imgbtnActivate.Visible = True
                        'End If
                    ElseIf ddlStatus.SelectedIndex = 2 Then
                        'If sSGMAD = "YES" Then
                        imgbtnWaiting.Visible = True
                        'End If
                    End If
                End If
            End If
            gvGeneralMaster.DataSource = Nothing
            gvGeneralMaster.DataBind()
            If ddlAuditAssignment.SelectedIndex > 0 Then
                BindAuditAssignmentsMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditAssignments_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As New Object, oAuditAssignmentID As New Object
        Try
            lblError.Text = ""
            If ddlAuditAssignment.SelectedIndex = 0 Then
                ddlAuditAssignment.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Audit Task/Assignments." : lblError.Text = "Select Audit Task/Assignments."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(3))
            End If
            oAuditAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditAssignment.SelectedValue))
            Response.Redirect(String.Format("~/Assignment/AssignmentMasterDetails.aspx?StatusID={0}&AuditAssignmentID={1}", oStatusID, oAuditAssignmentID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            If ddlAuditAssignment.SelectedIndex > 0 Then
                sMainMaster = ddlAuditAssignment.SelectedValue
            Else
                ddlAuditAssignment.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Audit Task/Assignments." : lblError.Text = "Select Audit Task/Assignments."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ddlAuditAssignment_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvGeneralMaster.Rows.Count - 1
                    chkField = gvGeneralMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvGeneralMaster.Rows.Count - 1
                    chkField = gvGeneralMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvGeneralMaster_PreRender(sender As Object, e As EventArgs) Handles gvGeneralMaster.PreRender
        Dim dt As New DataTable
        Try
            If gvGeneralMaster.Rows.Count > 0 Then
                gvGeneralMaster.UseAccessibleHeader = True
                gvGeneralMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvGeneralMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvGeneralMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvGeneralMaster.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                'If sSGMAD = "YES" Then
                gvGeneralMaster.Columns(0).Visible = True
                'End If
                gvGeneralMaster.Columns(6).Visible = False
                gvGeneralMaster.Columns(7).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sSGMAD = "YES" Then
                    gvGeneralMaster.Columns(6).Visible = True
                    'End If
                    gvGeneralMaster.Columns(7).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sSGMAD = "YES" Then
                    gvGeneralMaster.Columns(6).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sSGMAD = "YES" Then
                    gvGeneralMaster.Columns(6).Visible = True
                    'End If
                    gvGeneralMaster.Columns(7).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    gvGeneralMaster.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvGeneralMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvGeneralMaster.RowCommand
        Dim oStatusID As Object, oAuditAssignmentID As Object, oAuditAssignmentSubTaskID As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            If ddlAuditAssignment.SelectedIndex > 0 Then
                sMainMaster = ddlAuditAssignment.SelectedValue
            End If
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            If e.CommandName = "EditRow" Then
                oAuditAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditAssignment.SelectedValue))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                oAuditAssignmentSubTaskID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblDescID.Text)))
                Response.Redirect(String.Format("~/Assignment/AssignmentMasterDetails.aspx?StatusID={0}&AuditAssignmentID={1}&AuditAssignmentSubTaskID={2}", oStatusID, oAuditAssignmentID, oAuditAssignmentSubTaskID), False)
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, "D")
                    lblGeneralMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "De-Activated", ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, "A")
                    lblGeneralMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Activated", ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, "W")
                    lblGeneralMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Approved", ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                BindAuditAssignmentsMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvGeneralMaster.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to Activate." : lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
            End If
NextSave:   For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                lblDescID = gvGeneralMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, "A")
                    lblGeneralMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Activated", ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            BindAuditAssignmentsMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer = 0
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvGeneralMaster.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to De-Activate." : lblError.Text = "Select Name to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                lblDescID = gvGeneralMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, "D")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "De-Activated", ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            BindAuditAssignmentsMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvGeneralMaster.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to Approve." : lblError.Text = "No data To Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to Approve." : lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                lblDescID = gvGeneralMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, sSession.UserID, sSession.IPAddress, "W")
                    lblGeneralMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Approved", ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            BindAuditAssignmentsMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            dtdetails = objclsAdminMaster.LoadAuditAssignmentSTMasterReportDetails(sSession.AccessCode, sSession.AccessCodeID, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text)
            If dtdetails.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("GM(" + ddlAuditAssignment.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            dtdetails = objclsAdminMaster.LoadAuditAssignmentSTMasterReportDetails(sSession.AccessCode, sSession.AccessCodeID, ddlStatus.SelectedIndex, ddlAuditAssignment.SelectedValue, ddlAuditAssignment.SelectedItem.Text)
            If dtdetails.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("GM(" + ddlAuditAssignment.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterID As Object
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            oMasterID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
            Response.Redirect(String.Format("~/Masters/GeneralMaster.aspx?StatusID={0}&MasterID={1}", oStatusID, oMasterID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class
