Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WebForms
Public Class AuditChecklist
    Inherits System.Web.UI.Page
    Private sFormName As String = "AuditChecklist"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAuditChecklist As New clsAuditChecklist
    Private Shared sSession As AllSession
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
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iAuditTypeID As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                BindAuditTypes() : BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("AuditTypeID") IsNot Nothing Then
                    iAuditTypeID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditTypeID")))
                    ddlAuditType.SelectedValue = iAuditTypeID
                    ddlAuditType_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAuditTypes()
        Try
            ddlAuditType.DataSource = objclsAuditChecklist.LoadAuditTypeIsComplainceDetails(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlAuditType.DataTextField = "Name"
            ddlAuditType.DataValueField = "PKID"
            ddlAuditType.DataBind()
            ddlAuditType.Items.Insert(0, "Select Audit Type")
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
    Public Sub BindAuditTypesMasterGridDetails(ByVal iPageIndex As Integer, ByVal iStatus As Integer, ByVal iAuditTypeID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsAuditChecklist.LoadAuditTypeChecklistMasterGridDetails(sSession.AccessCode, sSession.AccessCodeID, iStatus, iAuditTypeID, "")
            gvAuditChecklistMaster.DataSource = dt
            gvAuditChecklistMaster.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlAuditType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditType.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
            If ddlAuditType.SelectedIndex > 0 Then
                If ddlAuditType.SelectedIndex > 0 Then
                    imgbtnReport.Visible = True
                    If ddlStatus.SelectedIndex = 0 Then
                        imgbtnDeActivate.Visible = True
                    ElseIf ddlStatus.SelectedIndex = 1 Then
                        imgbtnActivate.Visible = True
                    ElseIf ddlStatus.SelectedIndex = 2 Then
                        imgbtnWaiting.Visible = True
                    End If
                End If
            End If
            gvAuditChecklistMaster.DataSource = Nothing
            gvAuditChecklistMaster.DataBind()
            If ddlAuditType.SelectedIndex > 0 Then
                BindAuditTypesMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditTypes_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As New Object, oAuditTypeID As New Object
        Try
            lblError.Text = ""
            If ddlAuditType.SelectedIndex = 0 Then
                ddlAuditType.Focus()
                lblAuditTypeMasterValidationMsg.Text = "Select Audit Type." : lblError.Text = "Select Audit Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterValidation').modal('show');", True)
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
            oAuditTypeID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditType.SelectedValue))
            Response.Redirect(String.Format("~/Masters/AuditChecklistDetails.aspx?StatusID={0}&AuditTypeID={1}", oStatusID, oAuditTypeID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlAuditType.SelectedIndex = 0 Then
                ddlAuditType.Focus()
                lblAuditTypeMasterValidationMsg.Text = "Select Audit Type." : lblError.Text = "Select Audit Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ddlAuditType_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvAuditChecklistMaster.Rows.Count - 1
                    chkField = gvAuditChecklistMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvAuditChecklistMaster.Rows.Count - 1
                    chkField = gvAuditChecklistMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAuditChecklistMaster_PreRender(sender As Object, e As EventArgs) Handles gvAuditChecklistMaster.PreRender
        Dim dt As New DataTable
        Try
            If gvAuditChecklistMaster.Rows.Count > 0 Then
                gvAuditChecklistMaster.UseAccessibleHeader = True
                gvAuditChecklistMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAuditChecklistMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAuditChecklistMaster_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAuditChecklistMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAuditChecklistMaster.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                gvAuditChecklistMaster.Columns(0).Visible = True
                gvAuditChecklistMaster.Columns(5).Visible = False
                gvAuditChecklistMaster.Columns(6).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    gvAuditChecklistMaster.Columns(5).Visible = True
                    gvAuditChecklistMaster.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    gvAuditChecklistMaster.Columns(5).Visible = True
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    gvAuditChecklistMaster.Columns(5).Visible = True
                    gvAuditChecklistMaster.Columns(6).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    gvAuditChecklistMaster.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAuditChecklistMaster_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAuditChecklistMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAuditChecklistMaster.RowCommand
        Dim oStatusID As Object, oAuditTypeID As Object, oAuditChecklistID As Object
        Dim lblAuditChecklistID As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblAuditChecklistID = DirectCast(clickedRow.FindControl("lblAuditChecklistID"), Label)
            If e.CommandName = "EditRow" Then
                oAuditTypeID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditType.SelectedValue))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                oAuditChecklistID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAuditChecklistID.Text)))
                Response.Redirect(String.Format("~/Masters/AuditChecklistDetails.aspx?StatusID={0}&AuditTypeID={1}&AuditChecklistID={2}", oStatusID, oAuditTypeID, oAuditChecklistID), False)
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, lblAuditChecklistID.Text, sSession.UserID, sSession.IPAddress, "D")
                    lblAuditTypeMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "De-Activated", ddlAuditType.SelectedValue, ddlAuditType.SelectedItem.Text, lblAuditChecklistID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, lblAuditChecklistID.Text, sSession.UserID, sSession.IPAddress, "A")
                    lblAuditTypeMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "Activated", ddlAuditType.SelectedValue, ddlAuditType.SelectedItem.Text, lblAuditChecklistID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, lblAuditChecklistID.Text, sSession.UserID, sSession.IPAddress, "W")
                    lblAuditTypeMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "Approved", ddlAuditType.SelectedValue, ddlAuditType.SelectedItem.Text, lblAuditChecklistID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                End If
                BindAuditTypesMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAuditChecklistMaster_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblAuditChecklistID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvAuditChecklistMaster.Rows.Count = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvAuditChecklistMaster.Rows.Count - 1
                chkSelect = gvAuditChecklistMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "Select Checkpoint to Activate." : lblError.Text = "Select Checkpoint to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterValidation').modal('show');", True)
            End If
NextSave:   For i = 0 To gvAuditChecklistMaster.Rows.Count - 1
                chkSelect = gvAuditChecklistMaster.Rows(i).FindControl("chkSelect")
                lblAuditChecklistID = gvAuditChecklistMaster.Rows(i).FindControl("lblAuditChecklistID")
                If chkSelect.Checked = True Then
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, lblAuditChecklistID.Text, sSession.UserID, sSession.IPAddress, "A")
                    lblAuditTypeMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "Activated", ddlAuditType.SelectedValue, ddlAuditType.SelectedItem.Text, lblAuditChecklistID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                End If
            Next
            BindAuditTypesMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer = 0
        Dim lblAuditChecklistID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvAuditChecklistMaster.Rows.Count = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvAuditChecklistMaster.Rows.Count - 1
                chkSelect = gvAuditChecklistMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "Select Checkpoint to De-Activate." : lblError.Text = "Select Checkpoint to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvAuditChecklistMaster.Rows.Count - 1
                chkSelect = gvAuditChecklistMaster.Rows(i).FindControl("chkSelect")
                lblAuditChecklistID = gvAuditChecklistMaster.Rows(i).FindControl("lblAuditChecklistID")
                If chkSelect.Checked = True Then
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, lblAuditChecklistID.Text, sSession.UserID, sSession.IPAddress, "D")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "De-Activated", ddlAuditType.SelectedValue, ddlAuditType.SelectedItem.Text, lblAuditChecklistID.Text, "", sSession.IPAddress)
                    lblAuditTypeMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                End If
            Next
            BindAuditTypesMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblAuditChecklistID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvAuditChecklistMaster.Rows.Count = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "No data to Approve." : lblError.Text = "No data To Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvAuditChecklistMaster.Rows.Count - 1
                chkSelect = gvAuditChecklistMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "Select Checkpoint to Approve." : lblError.Text = "Select Checkpoint to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvAuditChecklistMaster.Rows.Count - 1
                chkSelect = gvAuditChecklistMaster.Rows(i).FindControl("chkSelect")
                lblAuditChecklistID = gvAuditChecklistMaster.Rows(i).FindControl("lblAuditChecklistID")
                If chkSelect.Checked = True Then
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, lblAuditChecklistID.Text, sSession.UserID, sSession.IPAddress, "W")
                    lblAuditTypeMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "Approved", ddlAuditType.SelectedValue, ddlAuditType.SelectedItem.Text, lblAuditChecklistID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditTypeMasterValidation').modal('show');", True)
                End If
            Next
            BindAuditTypesMasterGridDetails(0, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            dtdetails = objclsAuditChecklist.LoadAuditTypeChecklistMasterReportDetails(sSession.AccessCode, sSession.AccessCodeID, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
            If dtdetails.Rows.Count = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAuditTypeMasterValidation').modal('show');", True)
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
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("AuditType(" + ddlAuditType.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            dtdetails = objclsAuditChecklist.LoadAuditTypeChecklistMasterReportDetails(sSession.AccessCode, sSession.AccessCodeID, ddlStatus.SelectedIndex, ddlAuditType.SelectedValue)
            If dtdetails.Rows.Count = 0 Then
                lblAuditTypeMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAuditTypeMasterValidation').modal('show');", True)
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
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklist Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("AuditType(" + ddlAuditType.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
