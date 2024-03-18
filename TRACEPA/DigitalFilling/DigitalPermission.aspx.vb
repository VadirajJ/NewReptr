Imports System
Imports System.Data
Imports BusinesLayer
Public Class DigitalPermission
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ModulePermission"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsModulePermission As New clsModulePermission
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private Shared sSession As AllSession
    Private Shared dtAccess As New DataTable
    Private Shared dtTable As New DataTable
    Dim objAccessRyt As New clsAccessRights
    Private Shared sPerm As String = ""
    Private Shared sPerSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnSave.Visible = False
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPFLP", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '    End If
                'End If
                imgbtnSave.Visible = True
                If rboRole.Checked = True Then
                    RFVRole.ErrorMessage = "Select Role. " : RFVRole.InitialValue = "Select Role"
                Else
                    RFVRole.ErrorMessage = "Select User. " : RFVRole.InitialValue = "Select User"
                End If
                BindModuleDDL() : BindRoleDDL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindModuleDDL()
        Try
            ddlModules.DataSource = objclsModulePermission.LoadDigitalFillingModules(sSession.AccessCode, sSession.AccessCodeID)
            ddlModules.DataTextField = "Mod_Description"
            ddlModules.DataValueField = "Mod_id"
            ddlModules.DataBind()
            ddlModules.Items.Insert(0, "All Modules")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindModuleDDL" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindRoleDDL()
        Try
            ddlPermission.DataSource = objclsAllActiveMaster.LoadActiveRole(sSession.AccessCode, sSession.AccessCodeID)
            ddlPermission.DataTextField = "Mas_Description"
            ddlPermission.DataValueField = "Mas_ID"
            ddlPermission.DataBind()
            ddlPermission.Items.Insert(0, "Select Role")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindRoleDDL" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindUserDDL()
        Try
            ddlPermission.DataSource = objclsModulePermission.LoadUserDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlPermission.DataTextField = "FullName"
            ddlPermission.DataValueField = "usr_Id"
            ddlPermission.DataBind()
            ddlPermission.Items.Insert(0, "Select User")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindUserDDL" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Protected Sub ddlModules_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlModules.SelectedIndexChanged
        Try
            lblError.Text = ""
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            Session("Count") = 0
            If ddlPermission.SelectedIndex > 0 Then
                If ddlModules.SelectedIndex > 0 Then
                    BindAllSubModules(sSession.AccessCode, ddlModules.SelectedValue)
                Else
                    BindAllSubModules(sSession.AccessCode, 0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlModules_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindAllSubModules(ByVal sAC As String, ByVal iMoudle As Integer)
        Dim dt As New DataTable, dtAR As New DataTable, dtAccessRights As New DataTable
        Try

            If (iMoudle = 95) Then  ' digital Office
                dt.Columns.Add("Mod_Id")
                dt.Columns.Add("Mod_Description")
                dt.Columns.Add("mod_Function")
                dt.Columns.Add("Mod_Buttons")
                objAccessRyt.GetAllModule(sSession.AccessCode, sSession.AccessCodeID, iMoudle, dt)
                dgAccessRgt.DataSource = dt
                dgAccessRgt.DataBind()
                dtAR = dt.Copy()
                dtAccess = objAccessRyt.CopyDataNewCol(dtAR)
                dtAccessRights = GetReportDetails(dtAccess)
                If dtAccessRights.Rows.Count > 0 Then
                    dtTable = objAccessRyt.GetAccessRightsDetails(dtAccess, dtAccessRights)
                End If
                dgPermission.Visible = False
                dgAccessRgt.Visible = True
            Else
                dt.Columns.Add("ID")
                dt.Columns.Add("Module")
                dt.Columns.Add("Navigation")
                dt.Columns.Add("SLNo")
                objclsModulePermission.GetAllModule(sSession.AccessCode, sSession.AccessCodeID, iMoudle, dt)
                dgPermission.DataSource = dt
                dgPermission.DataBind()
                dgAccessRgt.Visible = False
                dgPermission.Visible = True
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllSubModules" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub

    Public Function GetReportDetails(ByVal RefDt As DataTable) As DataTable
        Dim dt As New DataTable
        Dim sChk As String, sPermission As String = ""
        Try
            If rboRole.Checked = True Then
                sChk = "R"
            Else
                sChk = "U"
            End If
            If dtAccess.Rows.Count > 0 Then
                sPermission = sPerm.Remove(0, 1)
                dt = objAccessRyt.GetPermission(sSession.AccessCode, sSession.AccessCodeID, sPermission, ddlPermission.SelectedValue, sChk)
            End If
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetReportDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub rboRole_CheckedChanged(sender As Object, e As EventArgs) Handles rboRole.CheckedChanged
        Try
            lblError.Text = ""
            lblName.Text = "Role"
            BindRoleDDL()
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            If rboRole.Checked = True Then
                RFVRole.ErrorMessage = "Select Role. " : RFVRole.InitialValue = "Select Role"
            Else
                RFVRole.ErrorMessage = "Select User. " : RFVRole.InitialValue = "Select User"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboRole_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub rboUser_CheckedChanged(sender As Object, e As EventArgs) Handles rboUser.CheckedChanged
        Try
            lblError.Text = ""
            lblName.Text = "User list"
            BindUserDDL()
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            If rboRole.Checked = True Then
                RFVRole.ErrorMessage = "Select Role. " : RFVRole.InitialValue = "Select Role"
            Else
                RFVRole.ErrorMessage = "Select User. " : RFVRole.InitialValue = "Select User"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboUser_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ddlPermission_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermission.SelectedIndexChanged
        Try
            lblError.Text = ""
            dgPermission.DataSource = Nothing
            dgPermission.DataBind()
            Session("Count") = 0
            If ddlPermission.SelectedIndex > 0 Then
                If ddlModules.SelectedIndex > 0 Then
                    BindAllSubModules(sSession.AccessCode, ddlModules.SelectedValue)
                Else
                    BindAllSubModules(sSession.AccessCode, 0)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermission_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub chkAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox
        Dim chkField As New CheckBoxList
        Dim IbChk As New ImageButton
        Dim i As Integer, j As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To dgPermission.Items.Count - 1
                    chkField = dgPermission.Items(i).FindControl("chkOperation")
                    For j = 0 To chkField.Items.Count - 1
                        chkField.Items(j).Selected = True
                    Next
                    IbChk = CType(dgPermission.Items.Item(i).FindControl("IbChk"), ImageButton)
                    IbChk.ImageUrl = "../Images/chkSelect.jpg"
                Next
            Else
                For i = 0 To dgPermission.Items.Count - 1
                    chkField = dgPermission.Items(i).FindControl("chkOperation")
                    For j = 0 To chkField.Items.Count - 1
                        chkField.Items(j).Selected = False
                    Next
                    IbChk = CType(dgPermission.Items.Item(i).FindControl("IbChk"), ImageButton)
                    IbChk.ImageUrl = "../Images/chk.jpg"
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub dgPermission_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPermission.ItemCommand
        Dim chkItem As New CheckBoxList, chkField As New CheckBoxList
        Dim IbChk As New ImageButton
        Dim i As Integer
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                IbChk = CType(e.Item.FindControl("IbChk"), ImageButton)
                If IbChk.ImageUrl = "../Images/chkSelect.jpg" = True Then
                    IbChk = CType(e.Item.FindControl("IbChk"), ImageButton)
                    IbChk.ImageUrl = "../Images/chk.jpg"
                    chkItem = e.Item.FindControl("chkOperation")
                    For i = 0 To chkItem.Items.Count - 1
                        chkItem.Items(i).Selected = False
                    Next
                Else
                    IbChk = CType(e.Item.FindControl("IbChk"), ImageButton)
                    IbChk.ImageUrl = "../Images/chkSelect.jpg"
                    chkItem = e.Item.FindControl("chkOperation")
                    For i = 0 To chkItem.Items.Count - 1
                        chkItem.Items(i).Selected = True
                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPermission_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub dgPermission_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgPermission.ItemDataBound
        Dim chk As New CheckBoxList, chkItem As New CheckBoxList
        Dim IbChk As New ImageButton
        Dim ds As New DataSet
        Dim sChk As String
        Dim i As Integer, j As Integer
        Try
            If rboRole.Checked = True Then
                sChk = "R"
            Else
                sChk = "U"
            End If
            If e.Item.ItemType <> ListItemType.Header Then
                If e.Item.ItemType <> ListItemType.Footer Then
                    chk = e.Item.FindControl("chkOperation")
                    chk.DataSource = objclsModulePermission.GetOperation(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text)
                    chk.DataTextField = "Op_OperationName"
                    chk.DataValueField = "op_PkID"
                    chk.DataBind()
                    If ddlPermission.SelectedIndex > 0 Then
                        ds = objclsModulePermission.GetCheckPermission(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlPermission.SelectedValue, sChk)
                        For i = 0 To ds.Tables(0).Rows.Count - 1
                            chkItem = e.Item.Cells(3).FindControl("chkOperation")
                            For j = 0 To chkItem.Items.Count - 1
                                If chkItem.Items(j).Value = ds.Tables(0).Rows(i)("perm_OpPKID") Then
                                    chkItem.Items(j).Selected = True
                                End If
                            Next
                        Next
                        If e.Item.Cells(5).Text = "H" Then
                            e.Item.Cells(2).Font.Bold = True
                            e.Item.Cells(2).ForeColor = Drawing.Color.FromName("#95B589")
                            e.Item.Cells(2).Font.Underline = True
                            IbChk = CType(e.Item.Cells(4).FindControl("IbChk"), ImageButton)
                            IbChk.Visible = False
                        End If
                        If e.Item.Cells(5).Text = "N" Then
                            e.Item.Cells(2).Font.Bold = True
                            e.Item.Cells(2).ForeColor = Drawing.Color.OrangeRed
                            e.Item.Cells(2).Font.Underline = True
                            IbChk = CType(e.Item.Cells(4).FindControl("IbChk"), ImageButton)
                            IbChk.Visible = False
                        End If
                        If e.Item.Cells(5).Text = "FN" Then
                            e.Item.Cells(2).Font.Bold = False
                            e.Item.Cells(2).ForeColor = Drawing.Color.Black
                            e.Item.Cells(2).Font.Underline = False
                            IbChk = CType(e.Item.Cells(4).FindControl("IbChk"), ImageButton)
                            IbChk.Visible = True
                            e.Item.Cells(1).Text = Val(Session("Count")) + 1
                            Session("Count") = e.Item.Cells(1).Text
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPermission_ItemDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim sChk As String
        Dim chkItem As CheckBoxList
        Dim i As Integer
        Dim blnCheck As Boolean
        Dim sSelIDs As String = String.Empty
        Dim objPerm As New clsAccessRights
        Dim iRowCount As Integer, iRet As Integer
        Dim chkSelCreate As New CheckBox, chkSelPermit As New CheckBox, chkSelDelete As New CheckBox,
            chkSelModify As New CheckBox, chkSelView As New CheckBox, chkSelPrint As New CheckBox, chkSelApprove As New CheckBox
        Dim chkAnnotation As New CheckBox, chkDownload As New CheckBox
        Dim Arr() As String
        Try

            lblError.Text = ""
            If ddlModules.SelectedValue = "95" Then 'Digital Office
                'If ddlPermission.SelectedIndex = 0 Then
                '    If rboRole.Checked = True Then
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Designation.','', 'info');", True)
                '        Exit Sub
                '    ElseIf rboUser.Checked = True Then
                '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Users.','', 'info');", True)
                '        Exit Sub
                '    End If
                'End If

                iRowCount = dgAccessRgt.Items.Count
                If rboUser.Checked = True Then
                    objPerm.sSGP_LevelGroup = "U"
                    objPerm.iSGP_LevelGroupID = ddlPermission.SelectedValue
                Else
                    objPerm.sSGP_LevelGroup = "R"
                    objPerm.iSGP_LevelGroupID = ddlPermission.SelectedValue
                End If
                iRet = objAccessRyt.CheckAvailability(sSession.AccessCode, objPerm.sSGP_LevelGroup, objPerm.iSGP_LevelGroupID)

                For i = 0 To dgAccessRgt.Items.Count - 1
                    objPerm.iSGP_ID = 0
                    objPerm.iSGP_ModID = dgAccessRgt.Items(i).Cells(0).Text
                    If objAccessRyt.IsPermissionSet(sSession.AccessCode, sSession.AccessCodeID, objPerm.sSGP_LevelGroup, ddlPermission.SelectedValue, objPerm.iSGP_ModID) = True Then
                        objAccessRyt.DeletePermission(sSession.AccessCode, sSession.AccessCodeID, objPerm.sSGP_LevelGroup, ddlPermission.SelectedValue, objPerm.iSGP_ModID)
                    End If

                    chkSelPermit = dgAccessRgt.Items(i).FindControl("chkView")
                    If chkSelPermit.Checked = True Then
                        objPerm.iSGP_View = 1
                    Else
                        objPerm.iSGP_View = 0
                    End If

                    chkSelCreate = dgAccessRgt.Items(i).FindControl("chkSaveOrUpdate")
                    If chkSelCreate.Checked = True Then
                        objPerm.iSGP_SaveOrUpdate = 1
                    Else
                        objPerm.iSGP_SaveOrUpdate = 0
                    End If

                    chkSelModify = dgAccessRgt.Items(i).FindControl("chkActiveOrDeActive")
                    If chkSelModify.Checked = True Then
                        objPerm.iSGP_ActiveOrDeactive = 1
                    Else
                        objPerm.iSGP_ActiveOrDeactive = 0
                    End If

                    chkSelDelete = dgAccessRgt.Items(i).FindControl("chkReport")
                    If chkSelDelete.Checked = True Then
                        objPerm.iSGP_Report = 1
                    Else
                        objPerm.iSGP_Report = 0
                    End If


                    chkDownload = dgAccessRgt.Items(i).FindControl("chkDownload")
                    If chkDownload.Checked = True Then
                        objPerm.iSGP_Download = 1
                    Else
                        objPerm.iSGP_Download = 0
                    End If

                    chkAnnotation = dgAccessRgt.Items(i).FindControl("chkAnnotation")
                    If chkAnnotation.Checked = True Then
                        objPerm.iSGP_Annotation = 1
                    Else
                        objPerm.iSGP_Annotation = 0
                    End If

                    objPerm.iSGP_CreatedBy = sSession.UserID
                    objPerm.iSGP_ApprovedBy = sSession.UserID
                    objPerm.sSGP_Status = "U"
                    objPerm.sSGP_DelFlag = "A"
                    objPerm.iSGP_CompID = sSession.AccessCodeID
                    Arr = objAccessRyt.SaveOrUpdatePermission(sSession.AccessCode, objPerm)
                Next
                ddlPermission_SelectedIndexChanged(sender, e)
                If Arr(0) = "2" Then
                    lblError.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

                ElseIf Arr(0) = "3" Then
                    lblError.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
                End If
            Else
                If rboRole.Checked = True Then
                    sChk = "R"
                Else
                    sChk = "U"
                End If
                For i = 0 To dgPermission.Items.Count - 1
                    objclsModulePermission.DeletePermission(sSession.AccessCode, sSession.AccessCodeID, sChk, ddlPermission.SelectedValue, dgPermission.Items(i).Cells(0).Text)
                    chkItem = dgPermission.Items(i).Cells(3).FindControl("chkOperation")
                    sSelIDs = ""
                    For Each items As ListItem In chkItem.Items
                        If items.Selected Then
                            sSelIDs += items.Value + ";"
                            blnCheck = True
                        End If
                    Next
                    If blnCheck = True Then
                        objclsModulePermission.SaveOrUpdatePermission(sSession.AccessCode, sSession.AccessCodeID, sChk, ddlPermission.SelectedValue, dgPermission.Items(i).Cells(0).Text, sSelIDs, sSession.UserID, sSession.IPAddress)
                        blnCheck = False
                    End If
                Next
                If dgPermission.Items.Count > 0 Then
                    lblModulePermissionValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Form Level Permissions", "Saved", 0, ddlModules.SelectedItem.Text, ddlPermission.SelectedValue, ddlPermission.SelectedItem.Text, sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalModulePermissionValidation').modal('show');", True)
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Private Sub dgAccessRgt_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAccessRgt.ItemDataBound
        Dim chkView As New CheckBox, chkSaveOrUpdate As New CheckBox, chkActiveOrDeActive As New CheckBox, chkReport As New CheckBox
        Dim chkDownload As New CheckBox, chkAnnotation As New CheckBox, chkDAOAll As New CheckBox
        Dim sChk As String
        Dim dt As New DataTable
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                If rboRole.Checked = True Then
                    sChk = "R"
                Else
                    sChk = "U"
                End If
                chkView = e.Item.FindControl("chkView")
                chkSaveOrUpdate = e.Item.FindControl("chkSaveOrUpdate")
                chkActiveOrDeActive = e.Item.FindControl("chkActiveOrDeActive")
                chkReport = e.Item.FindControl("chkReport")
                chkDownload = e.Item.FindControl("chkDownload")
                chkAnnotation = e.Item.FindControl("chkAnnotation")
                ' chkDAOAll = e.Item.FindControl("chkDAOAll")
                If e.Item.Cells(3).Text = "H" Then
                    e.Item.Cells(1).Font.Bold = True
                    e.Item.Cells(1).ForeColor = Drawing.Color.OrangeRed
                    e.Item.Cells(1).Font.Underline = True
                    chkView.Visible = False
                    chkSaveOrUpdate.Visible = False
                    chkActiveOrDeActive.Visible = False
                    chkReport.Visible = False
                    chkDownload.Visible = False
                    chkAnnotation.Visible = False
                    ' chkDAOAll.Visible = False
                End If
                If e.Item.Cells(3).Text = "N" Then
                    e.Item.Cells(1).Font.Bold = True
                    e.Item.Cells(1).ForeColor = Drawing.Color.OrangeRed
                    e.Item.Cells(1).Font.Underline = True

                    chkView.Visible = False
                    chkSaveOrUpdate.Visible = False
                    chkActiveOrDeActive.Visible = False
                    chkReport.Visible = False
                    chkDownload.Visible = False
                    chkAnnotation.Visible = False
                    ' chkDAOAll.Visible = False
                End If

                If e.Item.Cells(3).Text = "FN" Then
                    e.Item.Cells(1).Font.Bold = False
                    e.Item.Cells(1).ForeColor = Drawing.Color.Black
                    e.Item.Cells(1).Font.Underline = False

                    If e.Item.Cells(4).Text = "View" Then
                        chkView.Visible = True
                        chkSaveOrUpdate.Visible = False
                        chkActiveOrDeActive.Visible = False
                        chkReport.Visible = False
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                    End If

                    If e.Item.Cells(4).Text = "View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                        chkView.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkActiveOrDeActive.Visible = True
                        chkReport.Visible = True
                        chkDownload.Visible = False
                        chkAnnotation.Visible = False
                    End If
                    If e.Item.Cells(4).Text = "View,SaveOrUpdate,ActiveOrDeactive,Report,Annotation,Download" Then
                        chkView.Visible = True
                        chkSaveOrUpdate.Visible = True
                        chkActiveOrDeActive.Visible = True
                        chkReport.Visible = True
                        chkDownload.Visible = True
                        chkAnnotation.Visible = True
                    End If
                    If e.Item.Cells(4).Text = "View,Report,Annotation,Download" Then
                        chkView.Visible = True
                        chkSaveOrUpdate.Visible = False
                        chkActiveOrDeActive.Visible = False
                        chkReport.Visible = True
                        chkDownload.Visible = True
                        chkAnnotation.Visible = True
                    End If


                    If ddlPermission.SelectedIndex > 0 Then
                        sPerm = sPerm & "," & e.Item.Cells(0).Text
                        dt = objAccessRyt.GetCheckPermission(sSession.AccessCode, sSession.AccessCodeID, e.Item.Cells(0).Text, ddlPermission.SelectedValue, sChk)
                        If dt.Rows.Count > 0 Then
                            If dt.Rows(0)("SGP_View") = "1" Then
                                chkView.Checked = True
                            End If
                            If dt.Rows(0)("SGP_SaveOrUpdate") = "1" Then
                                chkSaveOrUpdate.Checked = True
                            End If
                            If dt.Rows(0)("SGP_ActiveOrDeactive") = "1" Then
                                chkActiveOrDeActive.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Report") = "1" Then
                                chkReport.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Download") = "1" Then
                                chkDownload.Checked = True
                            End If
                            If dt.Rows(0)("SGP_Annotaion") = "1" Then
                                chkAnnotation.Checked = True
                            End If
                        End If

                        If dt.Rows.Count > 0 Then
                            If sPerSave = "YES" Then
                                imgbtnSave.Visible = True : imgbtnSave.Visible = False
                            End If

                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAccessRgt_ItemDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub chkDAOAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkDAOAll As New CheckBox
        Dim chkView As New CheckBox, chkSaveOrUpdate As New CheckBox
        Dim chkActiveOrDeActive As New CheckBox, chkReport As New CheckBox
        Dim chkDownload As New CheckBox, chkAnnotation As New CheckBox
        Dim IbdgChk As New ImageButton
        Dim i As Integer, j As Integer
        Try
            lblError.Text = ""
            chkDAOAll = CType(sender, CheckBox)
            If chkDAOAll.Checked = True Then
                For i = 0 To dgAccessRgt.Items.Count - 1
                    'chkField = dgPermission.Items(i).FindControl("chkOperation")
                    'For j = 0 To chkField.Items.Count - 1
                    '    chkField.Items(j).Selected = True
                    'Next
                    chkView = dgAccessRgt.Items(i).FindControl("chkView")
                    chkView.Checked = True

                    chkSaveOrUpdate = dgAccessRgt.Items(i).FindControl("chkSaveOrUpdate")
                    chkSaveOrUpdate.Checked = True

                    chkActiveOrDeActive = dgAccessRgt.Items(i).FindControl("chkActiveOrDeActive")
                    chkActiveOrDeActive.Checked = True

                    chkReport = dgAccessRgt.Items(i).FindControl("chkReport")
                    chkReport.Checked = True

                    chkDownload = dgAccessRgt.Items(i).FindControl("chkDownload")
                    chkDownload.Checked = True

                    chkAnnotation = dgAccessRgt.Items(i).FindControl("chkAnnotation")
                    chkAnnotation.Checked = True

                    IbdgChk = CType(dgAccessRgt.Items.Item(i).FindControl("IbdgChk"), ImageButton)
                    IbdgChk.ImageUrl = "../Images/chkSelect.jpg"
                Next
            Else
                For i = 0 To dgAccessRgt.Items.Count - 1
                    'chkField = dgPermission.Items(i).FindControl("chkOperation")
                    'For j = 0 To chkField.Items.Count - 1
                    '    chkField.Items(j).Selected = False
                    'Next

                    chkView = dgAccessRgt.Items(i).FindControl("chkView")
                    chkView.Checked = False

                    chkSaveOrUpdate = dgAccessRgt.Items(i).FindControl("chkSaveOrUpdate")
                    chkSaveOrUpdate.Checked = False

                    chkActiveOrDeActive = dgAccessRgt.Items(i).FindControl("chkActiveOrDeActive")
                    chkActiveOrDeActive.Checked = False

                    chkReport = dgAccessRgt.Items(i).FindControl("chkReport")
                    chkReport.Checked = False

                    chkDownload = dgAccessRgt.Items(i).FindControl("chkDownload")
                    chkDownload.Checked = False

                    chkAnnotation = dgAccessRgt.Items(i).FindControl("chkAnnotation")
                    chkAnnotation.Checked = False

                    IbdgChk = CType(dgAccessRgt.Items.Item(i).FindControl("IbdgChk"), ImageButton)
                    IbdgChk.ImageUrl = "../Images/chk.jpg"
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkDAOAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Protected Sub dgAccessRgt_ItemCommand(source As Object, e As DataGridCommandEventArgs)
        Dim chkView As New CheckBox, chkSaveOrUpdate As New CheckBox
        Dim chkActiveOrDeActive As New CheckBox, chkReport As New CheckBox
        Dim chkDownload As New CheckBox, chkAnnotation As New CheckBox
        Dim IbdgChk As New ImageButton

        Try
            ' lblError.Text = ""
            If e.CommandName = "Select" Then
                IbdgChk = CType(e.Item.FindControl("IbdgChk"), ImageButton)
                If IbdgChk.ImageUrl = "../Images/chkSelect.jpg" = True Then
                    IbdgChk = CType(e.Item.FindControl("IbdgChk"), ImageButton)
                    IbdgChk.ImageUrl = "../Images/chk.jpg"

                    chkView = e.Item.FindControl("chkView")
                    chkView.Checked = False

                    chkSaveOrUpdate = e.Item.FindControl("chkSaveOrUpdate")
                    chkSaveOrUpdate.Checked = False

                    chkActiveOrDeActive = e.Item.FindControl("chkActiveOrDeActive")
                    chkActiveOrDeActive.Checked = False

                    chkReport = e.Item.FindControl("chkReport")
                    chkReport.Checked = False

                    chkDownload = e.Item.FindControl("chkDownload")
                    chkDownload.Checked = False

                    chkAnnotation = e.Item.FindControl("chkSaveOrUpdate")
                    chkAnnotation.Checked = False
                Else
                    IbdgChk = CType(e.Item.FindControl("IbdgChk"), ImageButton)
                    IbdgChk.ImageUrl = "../Images/chkSelect.jpg"

                    chkView = e.Item.FindControl("chkView")
                    chkView.Checked = True

                    chkSaveOrUpdate = e.Item.FindControl("chkSaveOrUpdate")
                    chkSaveOrUpdate.Checked = True

                    chkActiveOrDeActive = e.Item.FindControl("chkActiveOrDeActive")
                    chkActiveOrDeActive.Checked = True

                    chkReport = e.Item.FindControl("chkReport")
                    chkReport.Checked = True

                    chkDownload = e.Item.FindControl("chkDownload")
                    chkDownload.Checked = True

                    chkAnnotation = e.Item.FindControl("chkSaveOrUpdate")
                    chkAnnotation.Checked = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgPermission_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
End Class