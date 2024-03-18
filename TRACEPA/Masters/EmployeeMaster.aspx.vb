Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer
Partial Class EmployeeMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_EmployeeMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    'Private Shared sEMPAD As String
    'Private Shared sEMPBL As String
    Private Shared dtEmpDetails As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/AddUser24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnActivate.ImageUrl = "~/Images/Activate24.png"
        imgbtnUnLock.ImageUrl = "~/Images/Unlock24.png"
        imgbtnDeActivate.ImageUrl = "~/Images/DeActivate24.png"
        imgbtnUnBlock.ImageUrl = "~/Images/CheckedUser24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                imgbtnUnLock.Visible = False : imgbtnUnBlock.Visible = False
                'sEMPAD = "NO" : sEMPBL = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPEMP", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Approve/Activate/De-Activate,") = True Then
                '        sEMPAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",UnLock/UnBlock,") = True Then
                '        sEMPBL = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If

                BindStatus()
                imgbtnReport.Visible = True
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                dtEmpDetails = objclsEmployeeMaster.LoadAllEmpDetails(sSession.AccessCode, sSession.AccessCodeID)
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-09-2019
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Insert(0, "Activated")
            ddlStatus.Items.Insert(1, "De-Activated")
            ddlStatus.Items.Insert(2, "Locked")
            ddlStatus.Items.Insert(3, "Blocked")
            ddlStatus.Items.Insert(4, "Waiting for Approval")
            ddlStatus.Items.Insert(5, "All")
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oEmpID As New Object, oStatusID As New Object
        Try
            lblError.Text = ""
            oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 4 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
            ElseIf ddlStatus.SelectedIndex = 5 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(5))
            End If
            Response.Redirect(String.Format("~/Masters/EProfile.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'EmployeeMasterDetails
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Function LoadAllEmpDeatils() As DataTable
        Dim dt As New DataTable
        Dim sSearchText As String = "", sStatus As String = ""
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnUnLock.Visible = False : imgbtnUnBlock.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                'If sEMPAD = "YES" Then
                imgbtnDeActivate.Visible = True 'Activate
                'End If
            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                'If sEMPAD = "YES" Then
                imgbtnActivate.Visible = True 'De-Activate
                'End If
            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Lock"
                'If sEMPBL = "YES" Then
                imgbtnUnLock.Visible = True 'Lock
                'End If
            ElseIf ddlStatus.SelectedIndex = 3 Then
                sStatus = "Block"
                'If sEMPBL = "YES" Then
                imgbtnUnBlock.Visible = True 'Block
                'End If
            ElseIf ddlStatus.SelectedIndex = 4 Then
                sStatus = "Waiting for Approval"
                'If sEMPAD = "YES" Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
                'End If
            End If

            If ddlStatus.SelectedIndex <= 4 Then
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtEmpDetails)
                DVZRBADetails.RowFilter = "Status='" & sStatus & "'"
                DVZRBADetails.Sort = "EmployeeName ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            Else
                dt = Nothing
                Dim DVZRBADetails As New DataView(dtEmpDetails)
                DVZRBADetails.Sort = "EmployeeName ASC"
                dt = DVZRBADetails.ToTable
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        dt.Rows(i)("SrNo") = i + 1
                    Next
                    dt.AcceptChanges()
                End If
            End If
            gvEmployeeDetails.DataSource = dt
            gvEmployeeDetails.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllEmpDeatils" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Function

    Private Sub gvEmployeeDetails_PreRender(sender As Object, e As EventArgs) Handles gvEmployeeDetails.PreRender
        Dim dt As New DataTable
        Try
            If gvEmployeeDetails.Rows.Count > 0 Then
                gvEmployeeDetails.UseAccessibleHeader = True
                gvEmployeeDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvEmployeeDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmployeeDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEmployeeDetails.RowCommand
        Dim oEmpID As Object, oStatusID As Object
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblEmpID As Label = DirectCast(clickedRow.FindControl("lblEmpID"), Label)

            If e.CommandName.Equals("EditRow") Then
                oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblEmpID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Or ddlStatus.SelectedIndex = 3 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 4 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(4))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                Response.Redirect(String.Format("~/Masters/EProfile.aspx?EmpID={0}&StatusID={1}", oEmpID, oStatusID), False) 'EmployeeMasterDetails
            End If

            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    'If objclsCheckMasterIsInUse.CheckEmployeeNameIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblEmpID.Text) = True Then
                    '    lblEmpMasterValidationMsg.Text = "Already tag to some User, can't be De-Activate" : lblError.Text = "Already tag to some User, can't be De-Activate"
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                    '    Exit Sub
                    'End If
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "DeActivated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "De-Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Activated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Unlock
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnLock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Unlocked." : lblError.Text = "Successfully Unlocked."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unlocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 3 Then 'Unblock
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnBlock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Unblocked." : lblError.Text = "Successfully Unblocked."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unblocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 4 Then 'Waiting for Approval
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Created")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                    lblEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Approved", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
                End If
                LoadAllEmpDeatils()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmployeeDetails_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvEmployeeDetails.RowCreated
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim imgbtnStatus As ImageButton = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                Dim imgbtnedit As ImageButton = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnedit.ImageUrl = "~/Images/Edit16.png"

                'If sEMPAD = "YES" Then
                gvEmployeeDetails.Columns(0).Visible = True
                'End If

                gvEmployeeDetails.Columns(13).Visible = False
                gvEmployeeDetails.Columns(14).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sEMPAD = "YES" Then
                    gvEmployeeDetails.Columns(13).Visible = True
                    'End If
                    gvEmployeeDetails.Columns(14).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sEMPAD = "YES" Then
                    gvEmployeeDetails.Columns(13).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Unlock16.png" : imgbtnStatus.ToolTip = "Unlock"
                    'If sEMPBL = "YES" Then
                    gvEmployeeDetails.Columns(13).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    imgbtnStatus.ImageUrl = "~/Images/CheckedUser16.png" : imgbtnStatus.ToolTip = "Unblock"
                    'If sEMPBL = "YES" Then
                    gvEmployeeDetails.Columns(13).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 4 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sEMPAD = "YES" Then
                    gvEmployeeDetails.Columns(13).Visible = True
                    'End If
                    gvEmployeeDetails.Columns(14).Visible = True
                End If

                If ddlStatus.SelectedIndex = 5 Then
                    gvEmployeeDetails.Columns(0).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeDetails_RowCreated" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmployeeDetails_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvEmployeeDetails.RowEditing
    End Sub
    Private Sub gvHolidays_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvEmployeeDetails.RowDeleting
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Employee to Activate." : lblError.Text = "Select Employee to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Activated")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                End If
            Next
            lblEmpMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Employee to De-Activate." : lblError.Text = "Select Employee to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    If objclsCheckMasterIsInUse.CheckEmployeeNameIsInUse(sSession.AccessCode, sSession.AccessCodeID, lblEmpID.Text) = False Then
                        objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "DeActivated")
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "De-Activated", lblEmpID.Text, "", 0, "", sSession.IPAddress)
                        DVZRBADetails.Sort = "EmpID"
                        Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                        DVZRBADetails(iIndex)("Status") = "De-Activated"
                        dtEmpDetails = DVZRBADetails.ToTable
                    Else
                        iCheck = 1
                    End If
                End If
            Next
            If iCheck = 0 Then
                lblEmpMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            Else
                lblEmpMasterValidationMsg.Text = "Already tagged to other forms, can't be De-Activate." : lblError.Text = "Already tagged to other forms, can't be De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
            End If
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnUnBlock_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUnBlock.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Unblock." : lblError.Text = "No data to Unblock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Employee to Unblock." : lblError.Text = "Select Employee to Unblock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnBlock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                End If
            Next
            lblEmpMasterValidationMsg.Text = "Successfully Unblocked." : lblError.Text = "Successfully Unblocked."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unblocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUnBlock_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnUnLock_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUnLock.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Unlock." : lblError.Text = "No data to Unlock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Employee to Unlock." : lblError.Text = "Select Employee to Unlock."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "UnLock")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                End If
            Next
            lblEmpMasterValidationMsg.Text = "Successfully Unlocked." : lblError.Text = "Successfully Unlocked."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Unlocked", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUnLock_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblEmpID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtEmpDetails)
        Try
            lblError.Text = ""
            If gvEmployeeDetails.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No data to Approve." : lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblEmpMasterValidationMsg.Text = "Select Employee to Approve." : lblError.Text = "Select Employee to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvEmployeeDetails.Rows.Count - 1
                chkSelect = gvEmployeeDetails.Rows(i).FindControl("chkSelect")
                lblEmpID = gvEmployeeDetails.Rows(i).FindControl("lblEmpID")
                If chkSelect.Checked = True Then
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblEmpID.Text, sSession.IPAddress, "Created")
                    DVZRBADetails.Sort = "EmpID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblEmpID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtEmpDetails = DVZRBADetails.ToTable
                End If
            Next
            lblEmpMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Approved", lblEmpID.Text, "", 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterValidation').modal('show');", True)
            LoadAllEmpDeatils()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvEmployeeDetails.Rows.Count - 1
                    chkField = gvEmployeeDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvEmployeeDetails.Rows.Count - 1
                    chkField = gvEmployeeDetails.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllEmpDeatils()
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/EmployeeMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=EmployeeMaster" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            dt = LoadAllEmpDeatils()
            If dt.Rows.Count = 0 Then
                lblEmpMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/EmployeeMaster.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=EmployeeMaster" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class
