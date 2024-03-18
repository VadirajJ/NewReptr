Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Data
Imports Microsoft.Reporting.WebForms
Imports System.Globalization
Partial Class DocumentType
    Inherits System.Web.UI.Page
    Private sFormName As String = "Master DocumentType"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsEDICTGeneral As New clsEDICTGeneral
    Private objclsDocumentType As New clsDocumentType
    Private objclsPermission As New clsAccessRights
    Private Shared sSession As AllSession
    Private Shared iDocTypeID As Integer = 0
    Private Shared iDescID As Integer = 0
    Private Shared dtDocType As DataTable
    Private Shared dtDescriptor As DataTable
    Private Shared ObjStr As StrDocType
    Private Shared iConDocId As Integer = 0
    'Private Shared sDOCSave As String
    'Private Shared sDOCAD As String
    Private objclsSearch As New clsSearch
    Private Shared dtTable As New DataTable
    Private Shared sDeptID As String
    Private objclsGeneralFunctions As New clsGeneralFunctions
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
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnAdd.Visible = False : btnDocTypeSave.Visible = False : btnDocTypeUpdate.Visible = False : imgbtnWaiting.Visible = False : imgbtnActivate.Visible = False
                'imgbtnDeActivate.Visible = False : ddlPermission.Enabled = False : ddlAllDept.Enabled = False : lblUsers.Visible = False : ddlUsers.Visible = False
                'chkDocumentPermission.Enabled = False : chkdocument.Enabled = False : imgbtnReport.Visible = False

                'sDOCSave = "NO" : sDOCAD = "NO"
                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MDT")
                'If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,," Then
                '    Response.Redirect("~/Permission/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else

                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                '        sDOCSave = "YES"
                '        imgbtnAdd.Visible = True : btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = True
                '    End If
                '    If sFormButtons.Contains(",ActiveOrDeactive,") = True Then
                '        sDOCAD = "YES"
                '        imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = True : imgbtnWaiting.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Report") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '        sDOCAD = "YES" : sDOCAD = "YES"
                '        imgbtnAdd.Visible = True : btnDocTypeSave.Visible = True
                '    End If
                'End If

                BindStatus() : BindDepartment() : BindPermDepartment() : BindDescriptor() : LoadDescriptorDetails()
                dtDocType = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, sDeptID)
                LoadDocTypeDashboard()

                RFVDocType.ControlToValidate = "txtDocType" : RFVDocType.ErrorMessage = "Enter Document Type."
                REVDocType.ErrorMessage = "Document Type exceeded maximum size(max 400 characters)." : REVDocType.ValidationExpression = "^[\s\S]{0,400}$"
                RFVDepartment.InitialValue = "Select Department" : RFVDepartment.ErrorMessage = "Select Department."
                REVNote.ErrorMessage = "Note exceeded maximum size(max 600 characters)." : REVNote.ValidationExpression = "^[\s\S]{0,600}$"
                RFVDescriptor.InitialValue = "Select Descriptor" : RFVDescriptor.ErrorMessage = "Select Descriptor."
                If sSession.UserID = 1 Then ' Or GeneralInfo.SuperUser = "P"
                    ddlPermission.Enabled = True
                Else
                    ddlPermission.Enabled = False
                End If
                iConDocId = iDocTypeID
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDepartment()
        Dim dtDept As New DataTable
        Dim sDept As String = ""
        Try
            sDeptID = "" : sDept = ""
            dtDept = objclsDocumentType.LoadUserOtherDepartment(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            For i = 0 To dtDept.Rows.Count - 1
                sDept = sDept & "," & dtDept.Rows(i).Item("Org_Node")
            Next
            If dtDept.Rows.Count > 0 Then
                sDeptID = sDept.Remove(0, 1)
            End If
            ddlDepartment.DataSource = dtDept
            ddlDepartment.DataTextField = "Org_Name"
            ddlDepartment.DataValueField = "Org_Node"
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "Select Department")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDepartment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPermDepartment()
        Try
            ddlAllDept.DataSource = objclsDocumentType.LoadAllDepartment(sSession.AccessCode, sSession.AccessCodeID)
            ddlAllDept.DataTextField = "Org_Name"
            ddlAllDept.DataValueField = "Org_Node"
            ddlAllDept.DataBind()
            ddlAllDept.Items.Insert(0, "Every One")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermDepartment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindUsers()
        Dim iDeptID As Integer = 0
        Try
            If ddlAllDept.SelectedIndex > 0 Then
                iDeptID = ddlAllDept.SelectedValue
            End If
            ddlUsers.DataSource = objclsDocumentType.LoadUsersFromDept(sSession.AccessCode, iDeptID)
            ddlUsers.DataTextField = "usr_fullname"
            ddlUsers.DataValueField = "usr_id"
            ddlUsers.DataBind()
            ddlUsers.Items.Insert(0, "Select User")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindUsers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDescriptor()
        Try
            ddlDescriptor.DataSource = objclsDocumentType.LoadAllDescriptor(sSession.AccessCode)
            ddlDescriptor.DataTextField = "DESC_NAME"
            ddlDescriptor.DataValueField = "DES_ID"
            ddlDescriptor.DataBind()
            ddlDescriptor.Items.Insert(0, "Select Descriptor")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDescriptor" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadDocTypeDashboard()
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            imgbtnWaiting.Visible = False : imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                sStatus = "Activated"
                'If sDOCAD = "YES" Then
                imgbtnDeActivate.Visible = True 'Activate
                'End If

            ElseIf ddlStatus.SelectedIndex = 1 Then
                sStatus = "De-Activated"
                'If sDOCAD = "YES" Then
                imgbtnActivate.Visible = True 'De-Activate
                'End If

            ElseIf ddlStatus.SelectedIndex = 2 Then
                sStatus = "Waiting for Approval"
                'If sDOCAD = "YES" Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
                'End If
            End If
            dtDocType = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, sDeptID)
            If ddlStatus.SelectedIndex <= 2 Then
                dt = Nothing
                Dim DVFunctionStatus As New DataView(dtDocType)
                DVFunctionStatus.RowFilter = "Status='" & sStatus & "'"
                DVFunctionStatus.Sort = "Name Asc"
                dt = DVFunctionStatus.ToTable
            Else
                dt = Nothing
                Dim DVFunctionStatus As New DataView(dtDocType)
                DVFunctionStatus.Sort = "Name Asc"
                dt = DVFunctionStatus.ToTable
            End If
            dgDocTypeDashBoard.DataSource = dt
            dgDocTypeDashBoard.DataBind()
            Return dt

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDocTypeDashboard" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Public Sub LoadDescriptorDetails()
        Dim sIsRequired As String = "", sValidate As String = ""
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Try
            dtDescriptor = objclsDocumentType.LoadDescDetails(sSession.AccessCode, iDocTypeID)
            dgDisplay.DataSource = dtDescriptor
            dgDisplay.DataBind()
            If dtDescriptor.Rows.Count > 0 Then
                For j = 0 To dgDisplay.Rows.Count - 1
                    sIsRequired = dtDescriptor.Rows(j).Item("Mandatory")
                    sValidate = dtDescriptor.Rows(j).Item("Validator")
                    chkSelectMandatory = dgDisplay.Rows(j).FindControl("chkSelectMandatory")
                    chkSelectValidator = dgDisplay.Rows(j).FindControl("chkSelectValidator")
                    If sIsRequired = "Y" Then
                        chkSelectMandatory.Checked = True
                    Else
                        chkSelectMandatory.Checked = False
                    End If
                    If sValidate = "Y" Then
                        chkSelectValidator.Checked = True
                    Else
                        chkSelectValidator.Checked = False
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDescriptorDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadDocTypeDetails(ByVal iID As Integer)
        Dim dtDesc As New DataTable
        Dim sStatus As String = ""
        Try
            ClearAll()
            dtDesc = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, iID, sDeptID)
            iDocTypeID = dtDesc.Rows(0)("DocTypeID")
            If IsDBNull(dtDesc.Rows(0)("Name")) = False Then
                txtDocType.Text = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Name"))
            Else
                txtDocType.Text = ""
            End If
            ddlDepartment.SelectedIndex = 0
            If IsDBNull(dtDesc.Rows(0).Item("DepartmentID")) = False Then
                Dim liDesignationID As ListItem = ddlDepartment.Items.FindByValue(Val(dtDesc.Rows(0).Item("DepartmentID")))
                If IsNothing(liDesignationID) = False Then
                    ddlDepartment.SelectedValue = Val(dtDesc.Rows(0).Item("DepartmentID"))
                End If
            End If
            If IsDBNull(dtDesc.Rows(0)("Note")) = False Then
                txtNote.Text = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Note"))
            Else
                txtNote.Text = ""
            End If

            If IsDBNull(dtDesc.Rows(0)("Status")) = False Then
                sStatus = objclsEDICTGeneral.ReplaceSafeSQL(dtDesc.Rows(0)("Status"))
                btnDocTypeSave.Visible = False : btnDocTypeUpdate.Visible = False
                If sStatus = "Activated" Then
                    'If sDOCSave = "YES" Then
                    btnDocTypeSave.Visible = False : btnDocTypeUpdate.Visible = True
                    'End If

                ElseIf sStatus = "De-Activated" Then
                    'If sDOCSave = "YES" Then
                    btnDocTypeSave.Visible = False : btnDocTypeUpdate.Visible = False
                    'End If
                ElseIf sStatus = "Waiting for Approval" Then
                    'If sDOCSave = "YES" Then
                    btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = False
                    'End If

                End If
            End If
            LoadDescriptorDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDocTypeDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = LoadDocTypeDashboard()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No data to display."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermission_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermission.SelectedIndexChanged
        Try
            If ddlPermission.SelectedIndex > 0 Then
                If ddlAllDept.SelectedIndex > 0 Then
                    ddlAllDept_SelectedIndexChanged(sender, e)
                End If
                If ddlPermission.SelectedValue = 1 Or ddlAllDept.SelectedIndex = 0 Then
                    ddlAllDept.Enabled = True : ddlUsers.Visible = False : lblUsers.Visible = False
                    chkdocument.Enabled = True : chkDocumentPermission.Enabled = True
                ElseIf ddlPermission.SelectedValue = 2 Or ddlAllDept.SelectedIndex <> 0 Then
                    ddlAllDept.Enabled = True : ddlUsers.Visible = True : lblUsers.Visible = True
                    chkdocument.Enabled = True : chkDocumentPermission.Enabled = True
                End If
            Else
                ddlAllDept.Enabled = False : ddlUsers.Visible = False : lblUsers.Visible = False : chkdocument.Enabled = False : chkDocumentPermission.Enabled = False
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermission_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAllDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAllDept.SelectedIndexChanged
        Dim iDTL As Integer
        Try
            lblError.Text = "" : lblModelError.Text = ""
            chkdocument.ClearSelection() : chkDocumentPermission.ClearSelection()
            If ddlPermission.SelectedIndex > 0 Then
                If ddlPermission.SelectedValue = 2 Then
                    If ddlAllDept.SelectedIndex = 0 Then
                        lblUsers.Visible = False : ddlUsers.Visible = False
                    Else
                        lblUsers.Visible = True : ddlUsers.Visible = True
                    End If
                    BindUsers()

                    If ddlAllDept.SelectedIndex > 0 Then
                        If ddlUsers.SelectedIndex > 0 Then
                            LoadDocumentPermissions()
                        End If
                    Else
                        LoadDocumentPermissions()
                    End If
                Else
                    If ddlPermission.SelectedValue <> 1 Then
                        lblUsers.Visible = True : ddlUsers.Visible = True
                    End If
                    BindUsers()

                    If ddlPermission.SelectedIndex = 1 Then
                        LoadDocumentPermissions()
                    End If
                End If

                If ddlPermission.SelectedIndex = 2 Then
                    If (ChkAllDTPerm() = True) Then
                        If iDocTypeID = 0 Then
                            iDTL = iConDocId
                        Else
                            iDTL = iDocTypeID
                        End If
                        If ddlUsers.SelectedIndex > 0 And ddlAllDept.SelectedIndex > 0 Then
                            If (objclsDocumentType.chkRemFlagDTPerm(sSession.AccessCode, ddlPermission.SelectedItem.Text, iDTL, ddlUsers.SelectedValue, ddlAllDept.SelectedValue) = True) Then
                                lblModelError.Text = "Permission are removed"
                            Else
                                lblModelError.Text = "No Permissions Assigned"
                            End If
                        End If
                    End If
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAllDept_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUsers.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If iDocTypeID > 0 Then
                chkdocument.ClearSelection() : chkDocumentPermission.ClearSelection()
                If ddlPermission.Enabled = True And ddlAllDept.SelectedIndex > 0 And ddlUsers.SelectedIndex > 0 Then
                    LoadDocumentPermissions()
                End If
            End If
            If ddlUsers.SelectedValue > 0 Then
                If (ChkAllDTPerm() = True) Then
                    Dim iDTL As Integer
                    If iDocTypeID = 0 Then
                        iDTL = iConDocId
                    Else
                        iDTL = iDocTypeID
                    End If
                    If (objclsDocumentType.chkRemFlagDTPerm(sSession.AccessCode, ddlPermission.SelectedItem.ToString, iDTL, ddlUsers.SelectedValue, ddlAllDept.SelectedValue) = True) Then
                        lblModelError.Text = "Permission are removed"
                    Else
                        lblModelError.Text = "No Permissions Assigned"
                    End If
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlUsers_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadDocumentPermissions()
        Dim sPtype As String
        Dim iGrpid As Integer
        Dim objStrDocType As New StrDocType
        Dim myListItem As ListItem
        Try
            If ddlPermission.SelectedIndex = 2 And ddlAllDept.SelectedIndex > 0 Then
                LoadPermissionsForUser()
                If objStrDocType.sPtype = "G" Then
                    sPtype = "G"
                    objStrDocType = objclsDocumentType.LoadDocumentPermissionDetails(sSession.AccessCode, (iDocTypeID), Val(ddlAllDept.SelectedValue), sPtype)
                End If
            ElseIf ddlPermission.SelectedIndex = 0 And ddlAllDept.SelectedIndex > 0 Then
                If ddlAllDept.SelectedValue = 1 Then
                    sPtype = "E"
                    objStrDocType = objclsDocumentType.LoadDocumentPermissionDetails(sSession.AccessCode, (iDocTypeID), 0, sPtype)
                End If
            ElseIf ddlPermission.SelectedIndex = 1 And ddlAllDept.SelectedIndex = 0 Then
                sPtype = "E"
                iGrpid = 0
                objStrDocType = objclsDocumentType.LoadDocumentPermissionDetails(sSession.AccessCode, (iDocTypeID), iGrpid, sPtype)
            Else
                sPtype = "G"
                objStrDocType = objclsDocumentType.LoadDocumentPermissionDetails(sSession.AccessCode, (iDocTypeID), Val(ddlAllDept.SelectedValue), sPtype)
            End If
            If objStrDocType.iOthers = 1 Then
                myListItem = chkdocument.Items.FindByValue(0)
                myListItem.Selected = True
                lblModelError.Text = "Group Level Permissions"
            End If
            If objStrDocType.iMdFDoc = 1 Then
                myListItem = chkdocument.Items.FindByValue(3)
                myListItem.Selected = True
                lblModelError.Text = "Group Level Permissions"
            End If
            If objStrDocType.iDeldoc = 1 Then
                myListItem = chkdocument.Items.FindByValue(2)
                myListItem.Selected = True
                lblModelError.Text = "Group Level Permissions"
            End If
            If objStrDocType.iMdFType = 1 Then
                myListItem = chkdocument.Items.FindByValue(1)
                myListItem.Selected = True
                lblModelError.Text = "Group Level Permissions"
            End If
            If objStrDocType.iIndex = 1 Then
                myListItem = chkDocumentPermission.Items.FindByValue(0)
                myListItem.Selected = True
                lblModelError.Text = "Group Level Permissions"
            End If
            If objStrDocType.iSearch = 1 Then
                myListItem = chkDocumentPermission.Items.FindByValue(1)
                myListItem.Selected = True
                lblModelError.Text = "Group Level Permissions"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDocumentPermissions" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function LoadPermissionsForUser() As Object
        Dim ObjHT As Hashtable
        Dim obj As System.Collections.IDictionaryEnumerator
        Dim sLevel As String
        Dim myListItem As ListItem
        Try
            If ddlUsers.SelectedIndex > 0 Then
                ObjHT = objclsSearch.GetFinalDTPermissions(sSession.AccessCode, iDocTypeID, ddlUsers.SelectedValue, "ALL")
                obj = ObjHT.GetEnumerator
                obj.Reset()
                obj.MoveNext()
                If ObjHT("DINDEX") = 1 Then
                    myListItem = chkDocumentPermission.Items.FindByValue(0)
                    myListItem.Selected = True
                End If
                obj.MoveNext()
                If ObjHT("MDOCTYPE") = 1 Then
                    myListItem = chkdocument.Items.FindByValue(3)
                    myListItem.Selected = True
                End If
                obj.MoveNext()
                If ObjHT("MDOC") = 1 Then
                    myListItem = chkdocument.Items.FindByValue(1)
                    myListItem.Selected = True
                End If
                obj.MoveNext()
                If ObjHT("DDOC") = 1 Then
                    myListItem = chkdocument.Items.FindByValue(2)
                    myListItem.Selected = True
                End If
                obj.MoveNext()
                If ObjHT("CDOC") = 1 Then
                    myListItem = chkdocument.Items.FindByValue(0)
                    myListItem.Selected = True
                End If
                obj.MoveNext()
                If ObjHT("DSEARCH") = 1 Then
                    myListItem = chkDocumentPermission.Items.FindByValue(1)
                    myListItem.Selected = True
                End If
                obj.MoveNext()
                sLevel = ObjHT("Level")
                Select Case UCase(sLevel)
                    Case "PG"
                        lblModelError.Text = "Group Level Permissions"
                    Case "GH"
                        lblModelError.Text = "Group Level Permissions"
                    Case "PU"
                        lblModelError.Text = "POWER USER"
                    Case "G"
                        lblModelError.Text = "Group Level Permissions"
                    Case "U"
                        lblModelError.Text = "User Level Permissions"
                    Case "E"
                        lblModelError.Text = "Permissions given to EveryOne"
                End Select
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPermissionsForUser" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function ChkAllDTPerm() As Boolean
        Dim iRet As Int16
        Try
            iRet = GetEntireChkListPerm(chkdocument, False)
            If (iRet = 1) Then
                iRet = GetEntireChkListPerm(chkDocumentPermission, False)
                If (iRet = 1) Then
                    Return True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ChkAllDTPerm" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Shared Function GetEntireChkListPerm(ByVal chk As CheckBoxList, ByVal bVal As Boolean) As Int16
        Dim i As Int16
        Dim iRet As Int16 = 1
        Try
            For i = 0 To chk.Items.Count - 1
                If (chk.Items(i).Selected = Not bVal) Then
                    iRet = 0
                End If
            Next
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Sub dgDocTypeDashBoard_PreRender(sender As Object, e As EventArgs) Handles dgDocTypeDashBoard.PreRender
        Try
            If dgDocTypeDashBoard.Rows.Count > 0 Then
                dgDocTypeDashBoard.UseAccessibleHeader = True
                dgDocTypeDashBoard.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDocTypeDashBoard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDocTypeDashBoard_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDisplay_PreRender(sender As Object, e As EventArgs) Handles dgDisplay.PreRender
        Try
            If dgDisplay.Rows.Count > 0 Then
                dgDisplay.UseAccessibleHeader = True
                dgDisplay.HeaderRow.TableSection = TableRowSection.TableHeader
                dgDisplay.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDisplay_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDocTypeDashBoard_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgDocTypeDashBoard.RowCommand
        Dim lblDocTypeID As New Label, lblStatus As New Label
        Dim oDescID As Object
        Try
            lblError.Text = "" : lblModelError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDocTypeID = DirectCast(clickedRow.FindControl("lblDocTypeID"), Label)
            If e.CommandName.Equals("Status") Then
                If ddlStatus.SelectedIndex = 0 Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblDocTypeID.Text, "De-Activated")
                    lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "De-Activated", lblDocTypeID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblDocTypeID.Text, "Activated")
                    lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "Activated", lblDocTypeID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then 'Waiting for Approval
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblDocTypeID.Text, "Created")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "Approved", lblDocTypeID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                    ddlStatus.SelectedIndex = 0
                    ddlStatus_SelectedIndexChanged(sender, e)
                    lblError.Text = "Successfully Approved."
                End If
            End If
            If e.CommandName.Equals("EditRow") Then
                oDescID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblDocTypeID.Text)))
                iConDocId = Val(lblDocTypeID.Text)
                LoadDocTypeDetails(Val(lblDocTypeID.Text))
                '  PermissionVisibleTrue() Vijeth
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            End If
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDocTypeDashBoard_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDocTypeDashBoard_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDocTypeDashBoard.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgDocTypeDashBoard.Columns(0).Visible = False
                dgDocTypeDashBoard.Columns(7).Visible = False
                dgDocTypeDashBoard.Columns(8).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sDOCAD = "YES" Then
                    dgDocTypeDashBoard.Columns(0).Visible = True : dgDocTypeDashBoard.Columns(7).Visible = True
                    'End If
                    'If sDOCSave = "YES" Then
                    dgDocTypeDashBoard.Columns(0).Visible = True : dgDocTypeDashBoard.Columns(8).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sDOCAD = "YES" Then
                    dgDocTypeDashBoard.Columns(0).Visible = True : dgDocTypeDashBoard.Columns(7).Visible = True
                    'End If
                    'If sDOCSave = "YES" Then
                    dgDocTypeDashBoard.Columns(0).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sDOCAD = "YES" Then
                    dgDocTypeDashBoard.Columns(0).Visible = True : dgDocTypeDashBoard.Columns(7).Visible = True
                    'End If
                    'If sDOCSave = "YES" Then
                    dgDocTypeDashBoard.Columns(0).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgDocTypeDashBoard.Columns(0).Visible = False : dgDocTypeDashBoard.Columns(7).Visible = False : dgDocTypeDashBoard.Columns(8).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDocTypeDashBoard_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgDocTypeDashBoard.Rows.Count - 1
                    chkField = dgDocTypeDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgDocTypeDashBoard.Rows.Count - 1
                    chkField = dgDocTypeDashBoard.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDocTypeID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDocType)
        Try
            lblError.Text = ""
            If dgDocTypeDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Activate','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to Activate','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                lblDocTypeID = dgDocTypeDashBoard.Rows(i).FindControl("lblDocTypeID")
                If chkSelect.Checked = True Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblDocTypeID.Text, "Activated")
                    DVZRBADetails.Sort = "DocTypeID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDocTypeID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtDocType = DVZRBADetails.ToTable
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "Activated", lblDocTypeID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
            ddlStatus.SelectedIndex = 0
            ddlStatus_SelectedIndexChanged(sender, e)
            lblError.Text = "Successfully Activated."
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDocTypeID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDocType)
        Try
            lblError.Text = ""
            If dgDocTypeDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activated','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Name to De-Activated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to De-Activated','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                lblDocTypeID = dgDocTypeDashBoard.Rows(i).FindControl("lblDocTypeID")
                If chkSelect.Checked = True Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblDocTypeID.Text, "De-Activated")
                    DVZRBADetails.Sort = "DocTypeID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDocTypeID.Text)
                    DVZRBADetails(iIndex)("Status") = "De-Activated"
                    dtDocType = DVZRBADetails.ToTable
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "De-Activated", lblDocTypeID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Deactivated','', 'success');", True)
            ddlStatus.SelectedIndex = 0
            ddlStatus_SelectedIndexChanged(sender, e)
            lblError.Text = "Successfully De-Activated."
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDocTypeID As New Label
        Dim dt As New DataTable
        Dim DVZRBADetails As New DataView(dtDocType)
        Try
            lblError.Text = ""
            If dgDocTypeDashBoard.Rows.Count = 0 Then
                lblError.Text = "No data to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If
            For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Name to Approve','', 'warning');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgDocTypeDashBoard.Rows.Count - 1
                chkSelect = dgDocTypeDashBoard.Rows(i).FindControl("chkSelect")
                lblDocTypeID = dgDocTypeDashBoard.Rows(i).FindControl("lblDocTypeID")
                If chkSelect.Checked = True Then
                    objclsDocumentType.DocTypeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblDocTypeID.Text, "Created")
                    DVZRBADetails.Sort = "DocTypeID"
                    Dim iIndex As Integer = DVZRBADetails.Find(lblDocTypeID.Text)
                    DVZRBADetails(iIndex)("Status") = "Activated"
                    dtDocType = DVZRBADetails.ToTable
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "Approved", lblDocTypeID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                End If
            Next

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
            ddlStatus.SelectedIndex = 0
            ddlStatus_SelectedIndexChanged(sender, e)
            lblError.Text = "Successfully Approved."
            LoadDocTypeDashboard()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            lblError.Text = "" : lblModelError.Text = "" : btnDocTypeSave.Visible = True : btnDocTypeUpdate.Visible = False
            txtDocType.Text = "" : txtNote.Text = ""
            iDocTypeID = 0  'divPermDetails.Visible = False
            ddlDescriptor.SelectedIndex = 0 : ddlDepartment.SelectedIndex = 0
            LoadDescriptorDetails()
            dgDisplay.DataSource = Nothing
            dgDisplay.DataBind()
            'Permission
            divPermDetails.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub PermissionVisibleTrue()
        Try
            divPermDetails.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "PermissionVisibleTrue" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            ClearAll()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnDocTypeNew_Click(sender As Object, e As EventArgs) Handles btnDocTypeNew.Click
        Try
            ClearAll()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDocTypeNew_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'For giving prmissions
    Private Function GetValues() As StrDocType
        Try
            ObjStr.iDocTypeId = iDocTypeID
            ObjStr.iUsrid = sSession.UserID
            ObjStr.iGrpId = ddlDepartment.SelectedValue
            ObjStr.sPtype = "U"
            ObjStr.iDeldoc = 1
            ObjStr.iMdFType = 1
            ObjStr.iOthers = 1
            ObjStr.iMdFDoc = 1
            ObjStr.iIndex = 1
            ObjStr.iSearch = 1
            Return ObjStr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetValues" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function GetValuesForEveryOne() As StrDocType
        Try
            ObjStr.iDocTypeId = iDocTypeID
            ObjStr.iUsrid = 0
            ObjStr.iGrpId = 0
            ObjStr.sPtype = "E"
            ObjStr.iDeldoc = 0
            ObjStr.iMdFType = 0
            ObjStr.iOthers = 0
            ObjStr.iMdFDoc = 0
            ObjStr.iIndex = 0
            ObjStr.iSearch = 1
            ObjStr.iGlobal = 0
            Return ObjStr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetValuesForEveryOne" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function GetValuesForGroup() As StrDocType
        Try
            ObjStr.iDocTypeId = iDocTypeID
            ObjStr.iUsrid = 0
            ObjStr.iGrpId = ddlDepartment.SelectedValue
            ObjStr.sPtype = "G"
            ObjStr.iDeldoc = 0
            ObjStr.iMdFType = 0
            ObjStr.iOthers = 0
            ObjStr.iMdFDoc = 0
            ObjStr.iIndex = 1
            ObjStr.iSearch = 1
            Return ObjStr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetValuesForGroup" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub btnDocTypeSave_Click(sender As Object, e As EventArgs) Handles btnDocTypeSave.Click
        Dim iGlobal As Integer, iUserid As Integer, iDocID As Integer
        Dim ddlDescriptor As New DropDownList
        Dim ObjStr As New StrDocType
        Dim objstrDocTypeDetails As New strDocType_Details
        Dim ObjstrEDT_DOCUMENT_TYPE As New strEDT_DOCUMENT_TYPE
        Dim ObjstrEDT_DOCTYPE_LINK As New strEDT_DOCTYPE_LINK
        Dim Arr() As String, PermArr() As String
        Dim sGrp As String
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Dim lblDescId As Label, lblDescriptor As New Label, lblDataType As Label, lblSize As New Label
        Dim txtValues As New TextBox
        Dim sDescriptor As String, sSearch As String, sSearchNumber As String
        Dim dateVal As Date
        Dim lblData As New Label, lblDesc As New Label
        Try
            lblError.Text = "" : lblModelError.Text = ""
            iDocID = iDocTypeID
            If objclsDocumentType.CheckAvailability(sSession.AccessCode, objclsEDICTGeneral.SafeSQL(Trim(txtDocType.Text)), 0, ddlDepartment.SelectedValue) = True Then

                iGlobal = 0

                For iRowCount = 0 To dgDisplay.Rows.Count - 1
                    txtValues = dgDisplay.Rows(iRowCount).FindControl("txtValues")
                    lblData = dgDisplay.Rows(iRowCount).FindControl("lblDataType")
                    lblSize = dgDisplay.Rows(iRowCount).FindControl("lblSize")
                    lblDesc = dgDisplay.Rows(iRowCount).FindControl("lblDesc")
                    sSearch = "Date"
                    sSearchNumber = "Number"
                    sDescriptor = lblData.Text
                    If txtValues.Text <> "" Then
                        If (sDescriptor.IndexOf(sSearch) <> -1) Then
                            If Date.TryParseExact(txtValues.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.None, dateVal) = False Then
                                lblModelError.Text = "Enter valid values for " & lblDesc.Text & "."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                                txtValues.Focus()
                                Exit Sub
                            End If
                        ElseIf (sDescriptor.IndexOf(sSearchNumber) <> -1) Then
                            If IsNumeric(txtValues.Text) = False Then
                                lblModelError.Text = "Enter valid numeric values  for " & lblDesc.Text & "."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                                txtValues.Focus()
                                Exit Sub
                            End If
                        End If
                        If txtValues.Text.Length > lblSize.Text Then
                            lblModelError.Text = "Value for " & lblDesc.Text & " exceeded maximum size (" & lblSize.Text & ")."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                            txtValues.Focus()
                            Exit Sub
                        End If
                    End If
                Next


                ObjstrEDT_DOCUMENT_TYPE.iDOCTYPEID = 0
                ObjstrEDT_DOCUMENT_TYPE.sDOCNAME = objclsEDICTGeneral.SafeSQL(txtDocType.Text.Trim)
                ObjstrEDT_DOCUMENT_TYPE.sNOTE = objclsEDICTGeneral.SafeSQL(txtNote.Text.Trim)
                ObjstrEDT_DOCUMENT_TYPE.iPGROUP = ddlDepartment.SelectedValue
                ObjstrEDT_DOCUMENT_TYPE.iCRBY = sSession.UserID
                ObjstrEDT_DOCUMENT_TYPE.iDOTUPDATEDBY = sSession.UserID
                ObjstrEDT_DOCUMENT_TYPE.sOperation = "I"
                ObjstrEDT_DOCUMENT_TYPE.iOperationby = sSession.UserID
                ObjstrEDT_DOCUMENT_TYPE.iIsGlobal = iGlobal
                ObjstrEDT_DOCUMENT_TYPE.iDOTCompId = sSession.AccessCodeID
                ObjstrEDT_DOCUMENT_TYPE.sDOTIPAddress = sSession.IPAddress
                ObjstrEDT_DOCUMENT_TYPE.sDOCTYPEStatus = "C"
                ObjstrEDT_DOCUMENT_TYPE.sDOCTYPEFlag = "W"


                Arr = objclsDocumentType.SaveDocTypeDetails(sSession.AccessCode, ObjstrEDT_DOCUMENT_TYPE)
                iDocTypeID = Arr(1)
                For iRowCount = 0 To dgDisplay.Rows.Count - 1
                    lblDescId = dgDisplay.Rows(iRowCount).FindControl("lblDescId")
                    lblDataType = dgDisplay.Rows(iRowCount).FindControl("lblDataType")
                    lblSize = dgDisplay.Rows(iRowCount).FindControl("lblSize")
                    txtValues = dgDisplay.Rows(iRowCount).FindControl("txtValues")
                    chkSelectMandatory = dgDisplay.Rows(iRowCount).FindControl("chkSelectMandatory")
                    chkSelectValidator = dgDisplay.Rows(iRowCount).FindControl("chkSelectValidator")

                    ObjstrEDT_DOCTYPE_LINK.iPkID = 0
                    ObjstrEDT_DOCTYPE_LINK.iDOCTYPEID = iDocTypeID
                    ObjstrEDT_DOCTYPE_LINK.iDPTRID = Val(lblDescId.Text)
                    If chkSelectMandatory.Checked = True Then
                        ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "Y"
                    Else
                        ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "N"
                    End If
                    ObjstrEDT_DOCTYPE_LINK.iSize = lblSize.Text
                    If txtValues.Text <> "" Then
                        ObjstrEDT_DOCTYPE_LINK.sVALUES = objclsEDICTGeneral.SafeSQL(txtValues.Text)
                    Else
                        ObjstrEDT_DOCTYPE_LINK.sVALUES = ""
                    End If
                    If chkSelectValidator.Checked = True Then
                        ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "Y"
                    Else
                        ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "N"
                    End If
                    ObjstrEDT_DOCTYPE_LINK.iEDDCRBY = sSession.UserID
                    ObjstrEDT_DOCTYPE_LINK.iEDDUPDATEDBY = sSession.UserID
                    ObjstrEDT_DOCTYPE_LINK.iEDDCompId = sSession.AccessCodeID
                    ObjstrEDT_DOCTYPE_LINK.sEDDIPAddress = sSession.IPAddress
                    Arr = objclsDocumentType.SavePermissionDetails(sSession.AccessCode, ObjstrEDT_DOCTYPE_LINK)
                Next
                If iDocID = 0 Then
                    'SetPermissions By Default'
                    ObjStr = GetValues()
                    ObjStr.iEDPCRBY = sSession.UserID
                    ObjStr.iEDPUPDATEDBY = sSession.UserID
                    ObjStr.iEDPCompId = sSession.AccessCodeID
                    ObjStr.sEDPIPAddress = sSession.IPAddress
                    objclsDocumentType.SaveDocPermissions(sSession.AccessCode, ObjStr)

                    ObjStr = GetValuesForEveryOne()
                    ObjStr.iEDPCRBY = sSession.UserID
                    ObjStr.iEDPUPDATEDBY = sSession.UserID
                    ObjStr.iEDPCompId = sSession.AccessCodeID
                    ObjStr.sEDPIPAddress = sSession.IPAddress
                    objclsDocumentType.SaveDocPermissions(sSession.AccessCode, ObjStr)

                    ObjStr = GetValuesForGroup()
                    ObjStr.iEDPCRBY = sSession.UserID
                    ObjStr.iEDPUPDATEDBY = sSession.UserID
                    ObjStr.iEDPCompId = sSession.AccessCodeID
                    ObjStr.sEDPIPAddress = sSession.IPAddress
                    objclsDocumentType.SaveDocPermissions(sSession.AccessCode, ObjStr)

                    'To insert edp_when flg
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "Saved", "0", sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved and Waiting for Approval','', 'success');", True)
                    ddlStatus.SelectedIndex = 2
                    ddlStatus_SelectedIndexChanged(sender, e)
                    lblError.Text = "Successfully Saved and Waiting for Approval."
                End If
                iUserid = objclsDocumentType.CheckGrpHead(sSession.AccessCode, sSession.UserID, ddlDepartment.SelectedValue)
                'If iUserid = 1 Then 'Or GeneralInfo.SuperUser = "P" 
                '    ddlPermission.Enabled = True
                'Else
                '    ddlPermission.Enabled = False
                'End If
            Else
                lblModelError.Text = "Document Type already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Exit Sub
            End If

            If ddlPermission.SelectedIndex > 0 Then
                ObjStr.iDocTypeId = iDocTypeID
                If ddlAllDept.SelectedIndex > 0 Then
                    ObjStr.iGrpId = ddlAllDept.SelectedValue
                Else
                    ObjStr.iGrpId = 0
                End If
                If ddlUsers.SelectedIndex > 0 Then
                    ObjStr.iUsrid = ddlUsers.SelectedValue
                Else
                    ObjStr.iUsrid = 0
                End If
                sGrp = ddlPermission.SelectedItem.ToString
                ObjStr.iEdpid = 0
                If sGrp = "GROUP" Then
                    If ddlAllDept.SelectedIndex > 0 Then
                        ObjStr.sPtype = "G"
                        lblUsers.Visible = False : ddlUsers.Visible = False
                    Else
                        ObjStr.sPtype = "E"
                    End If
                ElseIf sGrp = "USER" And ddlAllDept.SelectedIndex = 0 Then
                    ObjStr.sPtype = "E"
                Else
                    ObjStr.sPtype = "U"
                    lblUsers.Visible = True : ddlUsers.Visible = True
                End If
                ObjStr.iDeldoc = 0
                ObjStr.iMdFType = 0
                ObjStr.iOthers = 0
                ObjStr.iMdFDoc = 0
                ObjStr.iIndex = 0
                ObjStr.iSearch = 0

                If chkdocument.Items.Count > 0 Then
                    For m = 0 To chkdocument.Items.Count - 1
                        If chkdocument.Items(m).Selected = True Then
                            sGrp = chkdocument.Items(m).Value
                            Select Case (sGrp)
                                Case "0"
                                    ObjStr.iOthers = 1
                                Case "1"
                                    ObjStr.iMdFDoc = 1
                                Case "2"
                                    ObjStr.iDeldoc = 1
                                Case "3"
                                    ObjStr.iMdFType = 1
                            End Select
                        End If
                    Next
                End If

                If chkDocumentPermission.Items.Count > 0 Then
                    For n = 0 To chkDocumentPermission.Items.Count - 1
                        If chkDocumentPermission.Items(n).Selected = True Then
                            sGrp = chkdocument.Items(n).Value
                            Select Case (sGrp)
                                Case "0"
                                    ObjStr.iIndex = 1
                                Case "1"
                                    ObjStr.iSearch = 1
                            End Select
                        End If
                    Next
                End If
                PermArr = objclsDocumentType.SaveDocPermissions(sSession.AccessCode, ObjStr)
                If Arr(0) = "3" Then
                    lblModelError.Text = "Successfully Permissions Saved and Waiting for Approval."
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDocTypeSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnDocTypeUpdate_Click(sender As Object, e As EventArgs) Handles btnDocTypeUpdate.Click
        Dim iGlobal As Integer, iDocID As Integer
        Dim ddlDescriptor As New DropDownList
        Dim objstrDocTypeDetails As New strDocType_Details
        Dim ObjstrEDT_DOCUMENT_TYPE As New strEDT_DOCUMENT_TYPE
        Dim ObjstrEDT_DOCTYPE_LINK As New strEDT_DOCTYPE_LINK
        Dim Arr() As String, PermArr() As String
        Dim sGrp As String
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Dim lblDescId As Label, lblDescriptor As New Label, lblDataType As Label, lblSize As New Label
        Dim txtValues As New TextBox
        Dim sDescriptor As String, sSearch As String, sSearchNumber As String
        Dim dateVal As Date
        Dim lblData As New Label, lblDesc As New Label
        Try
            lblError.Text = "" : lblModelError.Text = ""
            iDocID = iDocTypeID
            If objclsDocumentType.CheckAvailability(sSession.AccessCode, objclsEDICTGeneral.SafeSQL(Trim(txtDocType.Text)), iDocID, ddlDepartment.SelectedValue) = True Then
                If iDocTypeID > 0 Then
                    objclsDocumentType.DeletePermission(sSession.AccessCode, iDocID)
                    iGlobal = 0

                    For iRowCount = 0 To dgDisplay.Rows.Count - 1
                        txtValues = dgDisplay.Rows(iRowCount).FindControl("txtValues")
                        lblData = dgDisplay.Rows(iRowCount).FindControl("lblDataType")
                        lblSize = dgDisplay.Rows(iRowCount).FindControl("lblSize")
                        lblDesc = dgDisplay.Rows(iRowCount).FindControl("lblDesc")
                        sSearch = "Date"
                        sSearchNumber = "Number"
                        sDescriptor = lblData.Text
                        If txtValues.Text <> "" Then
                            If (sDescriptor.IndexOf(sSearch) <> -1) Then
                                If Date.TryParseExact(txtValues.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture, DateTimeStyles.None, dateVal) = False Then
                                    lblModelError.Text = "Enter valid values for " & lblDesc.Text & "."
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                                    txtValues.Focus()
                                    Exit Sub
                                End If
                            ElseIf (sDescriptor.IndexOf(sSearchNumber) <> -1) Then
                                If IsNumeric(txtValues.Text) = False Then
                                    lblModelError.Text = "Enter valid numeric values  for " & lblDesc.Text & "."
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                                    txtValues.Focus()
                                    Exit Sub
                                End If
                            End If
                            If txtValues.Text.Length > lblSize.Text Then
                                lblModelError.Text = "Value for " & lblDesc.Text & " exceeded maximum size (" & lblSize.Text & ")."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                                txtValues.Focus()
                                Exit Sub
                            End If
                        End If
                    Next



                    ObjstrEDT_DOCUMENT_TYPE.iDOCTYPEID = iDocID
                    ObjstrEDT_DOCUMENT_TYPE.sDOCNAME = objclsEDICTGeneral.SafeSQL(txtDocType.Text.Trim)
                    ObjstrEDT_DOCUMENT_TYPE.sNOTE = objclsEDICTGeneral.SafeSQL(txtNote.Text.Trim)
                    ObjstrEDT_DOCUMENT_TYPE.iPGROUP = ddlDepartment.SelectedValue
                    ObjstrEDT_DOCUMENT_TYPE.iCRBY = sSession.UserID
                    ObjstrEDT_DOCUMENT_TYPE.iDOTUPDATEDBY = sSession.UserID
                    ObjstrEDT_DOCUMENT_TYPE.sOperation = "I"
                    ObjstrEDT_DOCUMENT_TYPE.iOperationby = sSession.UserID
                    ObjstrEDT_DOCUMENT_TYPE.iIsGlobal = iGlobal
                    ObjstrEDT_DOCUMENT_TYPE.iDOTCompId = sSession.AccessCodeID
                    ObjstrEDT_DOCUMENT_TYPE.sDOTIPAddress = sSession.IPAddress
                    ObjstrEDT_DOCUMENT_TYPE.sDOCTYPEStatus = "U"
                    ObjstrEDT_DOCUMENT_TYPE.sDOCTYPEFlag = "A"

                    Arr = objclsDocumentType.SaveDocTypeDetails(sSession.AccessCode, ObjstrEDT_DOCUMENT_TYPE)
                    iDocTypeID = Arr(1)

                    For iRowCount = 0 To dgDisplay.Rows.Count - 1
                        lblDescId = dgDisplay.Rows(iRowCount).FindControl("lblDescId")
                        lblDataType = dgDisplay.Rows(iRowCount).FindControl("lblDataType")
                        lblSize = dgDisplay.Rows(iRowCount).FindControl("lblSize")
                        txtValues = dgDisplay.Rows(iRowCount).FindControl("txtValues")
                        chkSelectMandatory = dgDisplay.Rows(iRowCount).FindControl("chkSelectMandatory")
                        chkSelectValidator = dgDisplay.Rows(iRowCount).FindControl("chkSelectValidator")

                        iDescID = Val(lblDescId.Text)
                        ObjstrEDT_DOCTYPE_LINK.iPkID = 0
                        ObjstrEDT_DOCTYPE_LINK.iDOCTYPEID = iDocTypeID
                        ObjstrEDT_DOCTYPE_LINK.iDPTRID = Val(lblDescId.Text)
                        If chkSelectMandatory.Checked = True Then
                            ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "Y"
                        Else
                            ObjstrEDT_DOCTYPE_LINK.sISREQUIRED = "Q"
                        End If
                        ObjstrEDT_DOCTYPE_LINK.iSize = lblSize.Text
                        If txtValues.Text <> "" Then
                            ObjstrEDT_DOCTYPE_LINK.sVALUES = objclsEDICTGeneral.SafeSQL(txtValues.Text)
                        Else
                            ObjstrEDT_DOCTYPE_LINK.sVALUES = ""
                        End If
                        If chkSelectValidator.Checked = True Then
                            ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "Y"
                        Else
                            ObjstrEDT_DOCTYPE_LINK.sVALIDATE = "N"
                        End If
                        ObjstrEDT_DOCTYPE_LINK.iEDDCRBY = sSession.UserID
                        ObjstrEDT_DOCTYPE_LINK.iEDDUPDATEDBY = sSession.UserID
                        ObjstrEDT_DOCTYPE_LINK.iEDDCompId = sSession.AccessCodeID
                        ObjstrEDT_DOCTYPE_LINK.sEDDIPAddress = sSession.IPAddress
                        Arr = objclsDocumentType.SavePermissionDetails(sSession.AccessCode, ObjstrEDT_DOCTYPE_LINK)
                    Next
                    If iDocID > 0 Then
                        ObjStr = GetValuesForEveryOne()
                        ObjStr.iEDPCRBY = sSession.UserID
                        ObjStr.iEDPUPDATEDBY = sSession.UserID
                        ObjStr.iEDPCompId = sSession.AccessCodeID
                        ObjStr.sEDPIPAddress = sSession.IPAddress
                        objclsDocumentType.SaveDocPermissions(sSession.AccessCode, ObjStr)
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Document Type", "Updated", iDocID, sSession.YearName, 0, "", sSession.IPAddress)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated.','', 'success');", True)
                        lblModelError.Text = "Successfully Updated."
                    End If
                End If
            Else
                lblModelError.Text = "Document Type already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Exit Sub
            End If

            If ddlPermission.SelectedIndex > 0 Then
                ObjStr.iDocTypeId = iDocTypeID
                If ddlAllDept.SelectedIndex > 0 Then
                    ObjStr.iGrpId = ddlAllDept.SelectedValue
                Else
                    ObjStr.iGrpId = 0
                End If
                If ddlUsers.SelectedIndex > 0 Then
                    ObjStr.iUsrid = ddlUsers.SelectedValue
                Else
                    ObjStr.iUsrid = 0
                End If
                sGrp = ddlPermission.SelectedItem.ToString
                ObjStr.iEdpid = 0
                If sGrp = "GROUP" Then
                    If ddlAllDept.SelectedIndex > 0 Then
                        ObjStr.sPtype = "G"
                        lblUsers.Visible = False : ddlUsers.Visible = False
                    Else
                        ObjStr.sPtype = "E"
                    End If
                ElseIf sGrp = "USER" And ddlAllDept.SelectedIndex = 0 Then
                    ObjStr.sPtype = "E"
                Else
                    ObjStr.sPtype = "U"
                    lblUsers.Visible = True : ddlUsers.Visible = True
                End If
                ObjStr.iDeldoc = 0
                ObjStr.iMdFType = 0
                ObjStr.iOthers = 0
                ObjStr.iMdFDoc = 0
                ObjStr.iIndex = 0
                ObjStr.iSearch = 0

                If chkdocument.Items.Count > 0 Then
                    For m = 0 To chkdocument.Items.Count - 1
                        If chkdocument.Items(m).Selected = True Then
                            sGrp = chkdocument.Items(m).Value
                            Select Case (sGrp)
                                Case "0"
                                    ObjStr.iOthers = 1
                                Case "1"
                                    ObjStr.iMdFDoc = 1
                                Case "2"
                                    ObjStr.iDeldoc = 1
                                Case "3"
                                    ObjStr.iMdFType = 1
                            End Select
                        End If
                    Next
                End If

                If chkDocumentPermission.Items.Count > 0 Then
                    For n = 0 To chkDocumentPermission.Items.Count - 1
                        If chkDocumentPermission.Items(n).Selected = True Then
                            sGrp = chkdocument.Items(n).Value
                            Select Case (sGrp)
                                Case "0"
                                    ObjStr.iIndex = 1
                                Case "1"
                                    ObjStr.iSearch = 1
                            End Select
                        End If
                    Next
                End If

                PermArr = objclsDocumentType.SaveDocPermissions(sSession.AccessCode, ObjStr)
                If Arr(0) = "2" Then
                    lblModelError.Text = "Permissions are successfully updated."
                End If
            End If
            LoadDocTypeDashboard() : LoadDescriptorDetails()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDocTypeUpdate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim dt As New DataTable
        Dim dRow, drDesc As DataRow
        Dim chkSelectMandatory As New CheckBox, chkSelectValidator As New CheckBox
        Dim sIsRequired As String = "", sValidate As String = ""
        Try
            lblError.Text = "" : lblModelError.Text = ""
            If ddlDescriptor.SelectedIndex > 0 Then
                Dim DVDescriptorDetails As New DataView(dtDescriptor)
                DVDescriptorDetails.RowFilter = "DescId=" & ddlDescriptor.SelectedValue & ""
                dt = DVDescriptorDetails.ToTable
                If dt.Rows.Count > 0 Then
                    lblModelError.Text = "Descriptor Name already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    Exit Sub
                Else
                    dt = objclsDocumentType.LoadDescriptorGrid(sSession.AccessCode, ddlDescriptor.SelectedValue)
                    For Each dRow In dt.Rows
                        drDesc = dtDescriptor.NewRow
                        drDesc("DescId") = ddlDescriptor.SelectedValue
                        drDesc("Descriptor") = dRow("DESC_NAME")
                        drDesc("DataType") = dRow("Dt_Name")
                        drDesc("Size") = dRow("Desc_Size")
                        drDesc("Mandatory") = "Q"
                        drDesc("Values") = ""
                        drDesc("Validator") = "Q"
                        dtDescriptor.Rows.Add(drDesc)
                    Next
                End If
            End If
            dgDisplay.DataSource = dtDescriptor
            dgDisplay.DataBind()

            For i = 0 To dgDisplay.Rows.Count - 1
                sIsRequired = dtDescriptor.Rows(i).Item("Mandatory")
                sValidate = dtDescriptor.Rows(i).Item("Validator")
                chkSelectMandatory = dgDisplay.Rows(i).FindControl("chkSelectMandatory")
                chkSelectValidator = dgDisplay.Rows(i).FindControl("chkSelectValidator")
                If sIsRequired = "Y" Then
                    chkSelectMandatory.Checked = True
                Else
                    chkSelectMandatory.Checked = False
                End If
                If sValidate = "Y" Then
                    chkSelectValidator.Checked = True
                Else
                    chkSelectValidator.Checked = False
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgDisplay_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgDisplay.RowDataBound
        Dim lblDesc As New Label
        Dim imgValues As New ImageButton
        Dim sDescriptor As String, sSearch As String
        Try
            Dim pnlCalendar As New Panel
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblDesc = CType(e.Row.FindControl("lblDataType"), Label)
                imgValues = CType(e.Row.FindControl("imgValues"), ImageButton)
                pnlCalendar = CType(e.Row.FindControl("pnlCalendar"), Panel)
                sSearch = "Date"
                sDescriptor = lblDesc.Text
                If (sDescriptor.IndexOf(sSearch) <> -1) Then
                    pnlCalendar.Visible = True
                Else
                    pnlCalendar.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgDisplay_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dtDocType = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, sDeptID)
            dt = LoadDocTypeDashboard()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/DocumentType.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=DocumentType" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dtDocType = objclsDocumentType.GetDocTypeDetails(sSession.AccessCode, 0, sDeptID)
            dt = LoadDocTypeDashboard()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/DocumentType.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=DocumentType" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
