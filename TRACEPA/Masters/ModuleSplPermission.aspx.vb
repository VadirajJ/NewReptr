Imports System
Imports System.Data
Imports BusinesLayer
Public Class ModuleSplPermission
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ModuleSplPermission"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsModulePermission As New clsModulePermission
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private Shared sSession As AllSession
    Private Shared sMSPSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        ibSearch.ImageUrl = "~/Images/Search16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.Visible = False : sMSPSave = "No"
                sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPMLP", 1)
                If sFormButtons = "False" Or sFormButtons = "" Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",Save/Update,") = True Then
                        imgbtnSave.Visible = True
                        sMSPSave = "Yes"
                    End If
                End If

                RFVModule.ErrorMessage = "Select Module." : RFVModule.InitialValue = "Select Module"
                RFVRole.ErrorMessage = "Select Role." : RFVRole.InitialValue = "Select Role"
                RFVUsers.ErrorMessage = "Select Employees/Users." : RFVUsers.InitialValue = "Select Employees/Users"

                BindModuleDDL() : BindRoleDDL()
                BindUserDDL(sSession.AccessCode, sSession.AccessCodeID, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Public Sub BindRoleDDL()
        Try
            ddlRole.DataSource = objclsAllActiveMaster.LoadActiveRole(sSession.AccessCode, sSession.AccessCodeID)
            ddlRole.DataTextField = "Mas_Description"
            ddlRole.DataValueField = "Mas_ID"
            ddlRole.DataBind()
            ddlRole.Items.Insert(0, "Select Role")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindRoleDDL" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Public Sub BindUserDDL(ByVal sAC As String, ByVal iACID As Integer, ByVal iModuleID As Integer)
        Try
            ddlUsers.DataSource = objclsModulePermission.LoadAllModuleUsers(sAC, iACID, iModuleID)
            ddlUsers.DataTextField = "usr_FullName"
            ddlUsers.DataValueField = "usr_Id"
            ddlUsers.DataBind()
            ddlUsers.Items.Insert(0, "Select Employees/Users")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindUserDDL" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Public Sub BindModuleDDL()
        Try
            ddlModule.Items.Insert(0, "Select Module")
            ddlModule.Items.Insert(1, "Master")
            ddlModule.Items.Insert(2, "Audit")
            ddlModule.Items.Insert(3, "Risk")
            ddlModule.Items.Insert(4, "Compliance")
            ddlModule.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindModuleDDL" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Protected Sub ddlModule_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModule.SelectedIndexChanged
        Try
            lblError.Text = "" : txtSearch.Text = "" : dgModulePermission.Visible = False
            If ddlModule.SelectedIndex > 0 Then
                dgModulePermission.Visible = True
                BindUserDDL(sSession.AccessCode, sSession.AccessCodeID, ddlModule.SelectedIndex)
                BindUserPermissionDetails(ddlModule.SelectedIndex, 0, "Yes")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlModule_SelectedIndexChanged" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Public Sub BindUserPermissionDetails(ByVal iModuleID As Integer, ByVal iPageIndex As Integer, ByVal sPageType As String)
        Dim dt As New DataTable
        Try
            dt = objclsModulePermission.LoadUserPermissionDetails(sSession.AccessCode, sSession.AccessCodeID, iModuleID)
            If (sPageType = "Yes") Then
                dgModulePermission.CurrentPageIndex = iPageIndex
                If dt.Rows.Count > dgModulePermission.PageSize Then
                    dgModulePermission.AllowPaging = True
                Else
                    dgModulePermission.AllowPaging = False
                End If
            End If
            dgModulePermission.DataSource = dt
            dgModulePermission.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindUserPermissionDetails" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Private Sub dgModulePermission_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgModulePermission.PageIndexChanged
        Try
            lblError.Text = ""
            BindUserPermissionDetails(ddlModule.SelectedIndex, e.NewPageIndex, "Yes")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgModulePermission_PageIndexChanged" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Private Sub ibSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibSearch.Click
        Try
            lblError.Text = ""
            ddlUsers.DataSource = objclsModulePermission.LoadModuleUsers(sSession.AccessCode, sSession.AccessCodeID, ddlModule.SelectedIndex, objclsGRACeGeneral.SafeSQL(txtSearch.Text.Trim))
            ddlUsers.DataTextField = "usr_FullName"
            ddlUsers.DataValueField = "usr_Id"
            ddlUsers.DataBind()
            ddlUsers.Items.Insert(0, "Select Employees/Users")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibSearch_Click" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Private Sub dgModulePermission_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgModulePermission.ItemCommand
        Try
            lblError.Text = ""
            If e.CommandName = "Delete" Then
                objclsModulePermission.RemoveUserFromModule(sSession.AccessCode, sSession.AccessCodeID, ddlModule.SelectedIndex, e.Item.Cells(1).Text)
                lblModuleSplPermisssionValidationMsg.Text = "Successfully Removed." : lblError.Text = "Successfully Removed."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Module Level Permissions", "Removed", 0, ddlModule.SelectedItem.Text, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#lblModuleSplPermisssionValidationMsg').modal('show');", True)
            End If
            BindUserDDL(sSession.AccessCode, sSession.AccessCodeID, ddlModule.SelectedIndex)
            BindUserPermissionDetails(ddlModule.SelectedIndex, 0, "Yes")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgModulePermission_ItemCommand" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Private Sub dgModulePermission_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgModulePermission.ItemDataBound
        Dim imgbtnTrash As ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnTrash = CType(e.Item.FindControl("btnTrash"), ImageButton)
                imgbtnTrash.ImageUrl = "~/Images/Trash16.png"
                imgbtnTrash.ToolTip = "Remove"
                dgModulePermission.Columns(7).Visible = False
                If sMSPSave = "Yes" Then
                    dgModulePermission.Columns(7).Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgModulePermission_ItemDataBound" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            lblError.Text = "" : txtSearch.Text = ""
            If ddlModule.SelectedIndex > 0 Then
                objclsModulePermission.UpdateModuleToUser(sSession.AccessCode, sSession.AccessCodeID, ddlModule.SelectedIndex, ddlUsers.SelectedValue, ddlRole.SelectedValue)
                BindUserDDL(sSession.AccessCode, sSession.AccessCodeID, ddlModule.SelectedIndex)
                BindUserPermissionDetails(ddlModule.SelectedIndex, 0, "Yes")
                ddlRole.SelectedIndex = 0
                lblModuleSplPermisssionValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Module Level Permissions", "Saved", 0, ddlModule.SelectedItem.Text, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#lblModuleSplPermisssionValidationMsg').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'Changes 07-08-2019
        End Try
    End Sub
End Class