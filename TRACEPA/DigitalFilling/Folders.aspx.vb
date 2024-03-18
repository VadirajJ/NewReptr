Imports System
Imports System.Data
Imports BusinesLayer
Imports BusinesLayer.clsFolders
Imports Microsoft.Reporting.WebForms
Partial Class Folders
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFiling_Folders"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Dim objclsSubCabinet As New clsSubCabinet
    Dim objclsFolders As New clsFolders
    Dim objCab As New clsCabinet
    Private Shared iFol_Id As Integer = 0
    Private Shared dtFol As DataTable
    Private objclsPermission As New clsAccessRights
    'Private Shared sDESGSave As String
    'Private Shared sDESGAD As String
    Private Shared iCabinetID As Integer = 0
    Private Shared iSubCabID As Integer = 0
    Private Shared iBackID As Integer = 0
    'Private Shared sDESGReprot As String
    Private Shared iEFP_NODE As Integer = 0
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim iMemType As Integer
    Dim iUsrType As Integer
    Dim dt As DataTable
    Dim sPermission As String = ""
    Dim sArray() As String
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
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                'ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : chkPermission.Enabled = False : ddlPermissionLevel.Enabled = False : btnDescUpdate.Visible = False
                'pnlFolder.Visible = False : imgbtnBack.Visible = False : imgbtnReport.Visible = False

                'sDESGSave = "NO" : sDESGAD = "NO" : sDESGReprot = "NO"
                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DFF")
                'If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,," Then
                '    Response.Redirect("~/Permission/DigitalFillingPermission.aspx", False) 'Permissions/DigitalFillingPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",View,") = True Then
                '    End If
                '    If sFormButtons.Contains(",SaveOrUpdate,") = True Then
                '        sDESGSave = "YES"
                '        imgbtnAdd.Visible = True : btnDescSave.Visible = True : btnDescUpdate.Visible = True
                '    End If
                '    If sFormButtons.Contains(",ActiveOrDeactive,") = True Then
                '        sDESGAD = "YES"
                '        imgbtnActivate.Visible = True : imgbtnDeActivate.Visible = True : imgbtnWaiting.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Report") = True Then
                '        sDESGReprot = "YES"
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '        sDESGSave = "YES" : sDESGAD = "YES"
                '        imgbtnAdd.Visible = True : btnDescSave.Visible = True
                '    End If
                'End If


                RFVFolName.ControlToValidate = "txtFolName" : RFVFolName.ErrorMessage = "Enter Folder Name."
                REVFolName.ErrorMessage = "Folder Name exceeded maximum size(max 100 characters)." : REVFolName.ValidationExpression = "^[\s\S]{0,100}$"
                REVFolNotes.ErrorMessage = "Folder Notes exceeded maximum size(max 255 characters)." : REVFolNotes.ValidationExpression = "^[\s\S]{0,255}$"

                BindStatus() : BindexistingCabinet() : BindPermissionLevel() : BindPermissionDept() : BindPermissionUser() : BindChkBoxList()

                If Request.QueryString("CabinetID") IsNot Nothing Then
                    iCabinetID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CabinetID")))
                    ddlCabinet.SelectedValue = iCabinetID
                    ddlCabinet_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("SubCabID") IsNot Nothing Then
                    iSubCabID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("SubCabID")))
                    ddlSubCabinet.SelectedValue = iSubCabID
                    ddlSubCabinet_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("BackID") IsNot Nothing Then
                    imgbtnBack.Visible = True
                    iBackID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("BackID")))
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Add(New ListItem("Activated", 0))
            ddlStatus.Items.Add(New ListItem("De-Activated", 1))
            ddlStatus.Items.Add(New ListItem("Waiting for Approval", 2))
            ddlStatus.Items.Add(New ListItem("All", 3))
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindexistingCabinet()
        Dim objCab As New clsCabinet
        Dim sDeptID As String = "", sDept As String = ""
        Dim dtDept As New DataTable
        Try
            sDeptID = "" : sDept = ""
            dtDept = objCab.LoadUserOtherDepartment(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            For i = 0 To dtDept.Rows.Count - 1
                sDept = sDept & "," & dtDept.Rows(i).Item("Org_Node")
            Next
            If dtDept.Rows.Count > 0 Then
                sDeptID = sDept.Remove(0, 1)
            End If
            ddlCabinet.DataSource = objCab.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID, sDeptID, sSession.UserID)
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_ID"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindexistingCabinet" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindexistingSubCab()
        Try
            ddlSubCabinet.DataSource = objclsFolders.LoadSubCab(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue)
            ddlSubCabinet.DataTextField = "CBN_NAME"
            ddlSubCabinet.DataValueField = "CBN_ID"
            ddlSubCabinet.DataBind()
            ddlSubCabinet.Items.Insert(0, "Select Sub Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindexistingSubCab" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPermissionLevel()
        Try
            ddlPermissionLevel.Items.Add(New ListItem("Select Permission Level", 0))
            ddlPermissionLevel.Items.Add(New ListItem("User", 1))
            ddlPermissionLevel.Items.Add(New ListItem("Group", 2))
            ddlPermissionLevel.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermissionLevel" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPermissionDept()
        Try
            ddlPermissionDep.DataSource = objCab.LoadDepartment(sSession.AccessCode)
            ddlPermissionDep.DataTextField = "Org_Name"
            ddlPermissionDep.DataValueField = "Org_Node"
            ddlPermissionDep.DataBind()
            ddlPermissionDep.Items.Insert(0, "Select Department")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermissionDept" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPermissionUser()
        Try
            If ddlPermissionDep.SelectedIndex > 0 Then
                ddlPermissionUser.DataSource = objclsSubCabinet.LoadCabUserPer(sSession.AccessCode, ddlPermissionDep.SelectedValue)
                ddlPermissionUser.DataTextField = "Usr_LoginName"
                ddlPermissionUser.DataValueField = "Usr_Id"
                ddlPermissionUser.DataBind()
                ddlPermissionUser.Items.Insert(0, "Select")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermissionUser" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindChkBoxList()
        Try
            'chkPermission.Items.Add("Modify Folder")
            'chkPermission.Items.Add("De-Activate Folder")
            'chkPermission.Items.Add("View Folder")

            ''chkPermission.Items.Add("Modify Document") Vijeth
            ''chkPermission.Items.Add("Delete Document")
            ''chkPermission.Items.Add("Create Documnet")

            'chkPermission.Items.Add("Search")
            'chkPermission.Items.Add("Index")
            ' chkPermission.Items.Add("Export")

            chkPermission.Items.Add("Index")
            chkPermission.Items.Add("Search")
            chkPermission.Items.Add("Modify Folder")
            chkPermission.Items.Add("De-Activate Folder")
            chkPermission.Items.Add("View Folder")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChkBoxList" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False : imgbtnAdd.Visible = False
            If ddlStatus.SelectedIndex = 0 And ddlCabinet.SelectedIndex > 0 Then
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 And ddlCabinet.SelectedIndex > 0 Then
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 And ddlCabinet.SelectedIndex > 0 Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            Dim dt As New DataTable
            If ddlSubCabinet.SelectedIndex > 0 Then
                'If sDESGSave = "YES" Then
                imgbtnAdd.Visible = True
                'End If
                dt = BindFolders(ddlSubCabinet.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "No Data to Display"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False : imgbtnAdd.Visible = False
            imgbtnReport.Visible = False


            dgFolders.DataSource = Nothing
            dgFolders.DataBind()

            If ddlCabinet.SelectedIndex > 0 Then
                lblCabinetName.Text = ddlCabinet.SelectedItem.Text
                BindexistingSubCab()
            Else
                ddlSubCabinet.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlSubCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSubCabinet.SelectedIndexChanged
        Try
            lblError.Text = ""
            dgFolders.Visible = False

            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False : imgbtnAdd.Visible = False
            imgbtnReport.Visible = False

            If ddlSubCabinet.SelectedIndex > 0 Then
                'If sDESGAD = "YES" Then
                'End If
                'If sDESGSave = "YES" Then
                imgbtnAdd.Visible = True
                'End If

                'If sDESGReprot = "YES" Then
                imgbtnReport.Visible = True
                'End If

                lblSubCabinetName.Text = ddlSubCabinet.SelectedItem.Text : dgFolders.Visible = True
                BindFolders(ddlSubCabinet.SelectedValue)
                If dtFol.Rows.Count = 0 Then
                    lblError.Text = "No data to display."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSubCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindFolders(ByVal iSubCabId As Integer) As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            If ddlSubCabinet.SelectedIndex > 0 Then
                ' dtFol = objclsFolders.LoadFolders(sSession.AccessCode, sSession.AccessCodeID, iSubCabId, sSession.UserID)
                If ddlStatus.SelectedIndex = 0 Then
                    sStatus = "Activated"
                    'If sDESGAD = "YES" Then
                    imgbtnDeActivate.Visible = True 'Activate
                    'End If
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    sStatus = "De-Activated"
                    'If sDESGAD = "YES" Then
                    imgbtnActivate.Visible = True 'De-Activate
                    'End If
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    sStatus = "Waiting for Approval"
                    'If sDESGAD = "YES" Then
                    imgbtnWaiting.Visible = True 'Waiting for Approval
                    'End If
                End If
                If ddlStatus.SelectedIndex = 0 Then
                    dt = Nothing
                    dtFol = objclsFolders.LoadFolders(sSession.AccessCode, sSession.AccessCodeID, iSubCabId, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtFol)
                    DVFunctionStatus.RowFilter = "Fol_DelFlag='" & sStatus & "'"
                    DVFunctionStatus.Sort = "Fol_Name Asc"
                    dt = DVFunctionStatus.ToTable
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    dt = Nothing
                    dtFol = objclsFolders.GetAllCab(sSession.AccessCode, sStatus, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtFol)
                    DVFunctionStatus.RowFilter = "Fol_DelFlag='" & sStatus & "'"
                    DVFunctionStatus.Sort = "Fol_Name Asc"
                    dt = DVFunctionStatus.ToTable
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    dt = Nothing
                    dtFol = objclsFolders.GetAllCab(sSession.AccessCode, sStatus, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtFol)
                    DVFunctionStatus.RowFilter = "Fol_DelFlag='" & sStatus & "'"
                    DVFunctionStatus.Sort = "Fol_Name Asc"
                    dt = DVFunctionStatus.ToTable
                Else
                    dt = Nothing
                    dtFol = objclsFolders.GetAllCab(sSession.AccessCode, sStatus, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtFol)
                    DVFunctionStatus.Sort = "Fol_Name Asc"
                    dt = DVFunctionStatus.ToTable
                End If
                dgFolders.DataSource = dt
                dgFolders.DataBind()
            End If
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFolders" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgFolders.Rows.Count - 1
                    chkField = dgFolders.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgFolders.Rows.Count - 1
                    chkField = dgFolders.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgFolders_PreRender(sender As Object, e As EventArgs) Handles dgFolders.PreRender
        Try
            If dgFolders.Rows.Count > 0 Then
                dgFolders.UseAccessibleHeader = True
                dgFolders.HeaderRow.TableSection = TableRowSection.TableHeader
                dgFolders.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgFolders_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgFolders.RowDataBound
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lnkDocuments As New LinkButton
        Dim lblDocuments As New Label
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                lnkDocuments = CType(e.Row.FindControl("lnkDocuments"), LinkButton)
                lblDocuments = CType(e.Row.FindControl("lblDocuments"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                If lblDocuments.Text = "0" Then
                    lblDocuments.Visible = True
                    lnkDocuments.Visible = False
                Else
                    lnkDocuments.Visible = True
                End If

                dgFolders.Columns(0).Visible = False : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                'If sDESGAD = "YES" Then
                dgFolders.Columns(9).Visible = True
                'End If
                'If sDESGSave = "YES" Then
                dgFolders.Columns(10).Visible = True
                'End If


                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgFolders.Columns(0).Visible = True : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgFolders.Columns(9).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgFolders.Columns(10).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgFolders.Columns(0).Visible = True : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgFolders.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgFolders.Columns(0).Visible = True : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgFolders.Columns(9).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgFolders.Columns(0).Visible = False : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblFOL_FOLID As New Label
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Activate','', 'info');", True)
                Exit Sub
            End If

NextSave:   For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                lblFOL_FOLID = dgFolders.Rows(i).FindControl("lblFOL_FOLID")
                If chkSelect.Checked = True Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "A", lblFOL_FOLID.Text, "A", sSession.UserID)
                    lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "Activated", lblFOL_FOLID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindFolders(ddlSubCabinet.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblFOL_FOLID As New Label
        Dim ExtraP As Integer
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to De-Activate.','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                lblFOL_FOLID = dgFolders.Rows(i).FindControl("lblFOL_FOLID")
                If chkSelect.Checked = True Then
                    ExtraP = objclsPermission.ExtraPermissionsToFolder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblFOL_FOLID.Text, "EFP_DEL_FOLDER") 'vijeth
                    If ExtraP <> 0 Then
                        objclsFolders.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "D", lblFOL_FOLID.Text, "D", sSession.UserID)
                        lblError.Text = "Successfully De-Activated."
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "De-Activated", lblFOL_FOLID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Folder Permission is not Assigned','', 'info');", True)
                    End If 'vijeth
                End If
            Next
            ddlStatus.SelectedIndex = 1
            BindFolders(ddlSubCabinet.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblFOL_FOLID As New Label
        Try
            lblError.Text = ""
            If dgFolders.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblError.Text = "Select to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select to Approve','', 'info');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To dgFolders.Rows.Count - 1
                chkSelect = dgFolders.Rows(i).FindControl("chkSelect")
                lblFOL_FOLID = dgFolders.Rows(i).FindControl("lblFOL_FOLID")
                If chkSelect.Checked = True Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "W", lblFOL_FOLID.Text, "A", sSession.UserID)
                    objclsFolders.UpdateFolderCount(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue)
                    lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "Approved", lblFOL_FOLID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindFolders(ddlSubCabinet.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSeimgbtnWaiting_Clickarch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgFolders_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgFolders.RowCommand
        Dim chkSelectAll As New CheckBox
        Dim lblFOL_FOLID As New Label, lblDescName As New Label, lblPGE_CABINET As New Label, lblPGE_SubCABINET As New Label, lblPGE_FOLDER As New Label, lblFolderName As New Label
        Dim sMainMaster As String
        Dim oDescID As New Object, oPGE_FOLDERName As New Object
        Dim dt As New DataTable()
        Dim oPGE_CABINET As New Object, oPGE_SubCABINET As New Object, oPGE_FOLDER As New Object
        Dim ExtraP As Integer
        CBLAssignP.Checked = False 'Vijeth
        Try
            lblError.Text = "" : sMainMaster = ""
            If e.CommandName = "EditRow" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblFOL_FOLID = DirectCast(clickedRow.FindControl("lblFOL_FOLID"), Label)
                If (lblFOL_FOLID.Text >= 0) Then
                    If (objclsFolders.GetFinalFolPermissions(lblFOL_FOLID.Text, sSession.UserID, sSession.AccessCode, "MFD", 2) <> 0) Then
                        'ExtraP = objclsPermission.ExtraPermissionsToFolder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblFOL_FOLID.Text, "EFP_MOD_FOLDER") 'vijeth
                        'If ExtraP <> 0 Then
                        btnDescSave.Visible = False : btnDescUpdate.Visible = True : ddlPermissionLevel.Enabled = True : pnlFolder.Visible = True
                        oDescID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblFOL_FOLID.Text)))
                        iEFP_NODE = Val(lblFOL_FOLID.Text)
                        BindFolderDetails(Val(lblFOL_FOLID.Text))
                        lblModelError.Text = "" : lblPrmError.Text = "" : ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0
                        For i = 0 To chkPermission.Items.Count - 1
                            chkPermission.Items(i).Selected = False
                        Next
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)

                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Folder Permission is not Assigned','', 'info');", True)
                    End If 'vijeth
                End If
            End If
            If e.CommandName = "Status" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblFOL_FOLID = DirectCast(clickedRow.FindControl("lblFOL_FOLID"), Label)
                If ddlStatus.SelectedIndex = 0 Then
                    ExtraP = objclsPermission.ExtraPermissionsToFolder(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblFOL_FOLID.Text, "EFP_DEL_FOLDER") 'vijeth
                    If ExtraP <> 0 Then
                        objclsFolders.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "D", lblFOL_FOLID.Text, "D", sSession.UserID)
                        lblError.Text = "Successfully De-Activated."
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "De-Activated", lblFOL_FOLID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Folder Permission is not Assigned','', 'info');", True)
                    End If 'vijeth
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "A", lblFOL_FOLID.Text, "A", sSession.UserID)
                    lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "Activated", lblFOL_FOLID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objclsFolders.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "W", lblFOL_FOLID.Text, "A", sSession.UserID)
                    objclsFolders.UpdateFolderCount(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue)
                    lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "Approved", lblFOL_FOLID.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
                ddlStatus.SelectedIndex = 0
                BindFolders(ddlSubCabinet.SelectedValue)
            End If
            If e.CommandName = "Document" Then
                Dim clickedItem As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPGE_CABINET = DirectCast(clickedItem.FindControl("lblPGE_CABINET"), Label)
                lblPGE_FOLDER = DirectCast(clickedItem.FindControl("lblPGE_FOLDER"), Label)
                lblFolderName = DirectCast(clickedItem.FindControl("lblFolderName"), Label)

                oPGE_CABINET = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblPGE_CABINET.Text)))
                oPGE_SubCABINET = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(0)))
                oPGE_FOLDER = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblPGE_FOLDER.Text)))

                oPGE_FOLDERName = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(lblFolderName.Text))
                Response.Redirect(String.Format("~/DigitalFilling/Search.aspx?PGE_CABINET={0}&PGE_SUBCABINET={1}&PGE_FOLDER={2}&PGE_FOLDERNAME={3}", oPGE_CABINET, oPGE_SubCABINET, oPGE_FOLDER, oPGE_FOLDERName, False))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim ExtraP2 As Integer
        Try
            ExtraP2 = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlSubCabinet.SelectedValue, "CBP_Create") 'Vijeth
            If ExtraP2 <> 0 Then
                lblError.Text = "" : lblModelError.Text = ""
                btnDescSave.Visible = False : btnDescUpdate.Visible = False : pnlFolder.Visible = False
                'If sDESGAD = "YES" Then
                'End If
                'If sDESGSave = "YES" Then
                btnDescSave.Visible = True
                'End If

                lblError.Text = ""
                txtFolName.Text = "" : txtFolNotes.Text = ""
                ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('SubCabinet Permission is not Assigned','', 'info');", True)
            End If 'vijeth
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            btnDescSave.Visible = False : btnDescUpdate.Visible = False
            'If sDESGSave = "YES" Then
            btnDescSave.Visible = True
            'End If
            pnlFolder.Visible = False
            lblModelError.Text = "" : lblPrmError.Text = ""
            ddlPermissionLevel.Enabled = False : ddlPermissionDep.Enabled = False
            txtFolName.Text = "" : txtFolNotes.Text = ""
            ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0 : lblUser.Visible = True : ddlPermissionUser.Visible = True
            For i = 0 To chkPermission.Items.Count - 1
                chkPermission.Items(i).Selected = False
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindFolderDetails(ByVal iFolderID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsFolders.LoadFolderDetails(sSession.AccessCode, sSession.AccessCodeID, iFolderID)
            If dt.Rows.Count > 0 Then
                iFol_Id = dt.Rows(0)("Fol_FolId")

                If IsDBNull(dt.Rows(0)("Fol_NAME")) = False Then
                    txtFolName.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("Fol_NAME"))
                Else
                    txtFolName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Fol_Note")) = False Then
                    txtFolNotes.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("Fol_Note"))
                Else
                    txtFolNotes.Text = ""
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFolderDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionLevel.SelectedIndexChanged
        Try
            lblModelError.Text = "" : lblPrmError.Text = "" : ddlPermissionDep.SelectedIndex = 0
            If ddlPermissionLevel.SelectedIndex = 0 Then
                ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : chkPermission.Enabled = False
            ElseIf ddlPermissionLevel.SelectedIndex = 1 Then
                ddlPermissionDep.Enabled = True : ddlPermissionUser.Enabled = True : chkPermission.Enabled = True : ddlPermissionUser.Visible = True : lblUser.Visible = True : CBLAssignP.Visible = True 'Vijeth
            ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                ddlPermissionDep.Enabled = True : ddlPermissionUser.Visible = False : chkPermission.Enabled = True : lblUser.Visible = False : CBLAssignP.Visible = False 'Vijeth
            End If
            For i = 0 To chkPermission.Items.Count - 1
                chkPermission.Items(i).Selected = False
            Next
            CBLAssignP.Checked = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermissionLevel_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionDep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionDep.SelectedIndexChanged
        Try
            iUsrType = objCab.GetUserType(sSession.AccessCode, sSession.UserID)
            iMemType = objCab.GetMemberType(sSession.AccessCode, sSession.UserID)
            lblModelError.Text = "" : lblPrmError.Text = ""
            For i = 0 To chkPermission.Items.Count - 1
                chkPermission.Items(i).Selected = False
            Next
            BindPermissionUser()
            If ddlPermissionLevel.SelectedIndex = 2 Then
                If (iUsrType = 1 Or iMemType = 1) Then
                    BindChkBoxPermission()
                Else
                    dt = objclsFolders.BindCheckPermissionFolDep(iEFP_NODE, ddlPermissionDep.SelectedValue, sSession.AccessCode)
                    If (dt.Rows.Count > 0) Then
                        If (dt.Rows(0)("EFP_Other") = 0) Then
                            CBLAssignP.Checked = True
                        Else
                            CBLAssignP.Checked = False
                        End If
                        For i = 0 To dt.Rows.Count - 1
                            If (dt.Rows(i)("EFP_INDEX") = 1) Then
                                sPermission = sPermission & "1"
                            Else
                                sPermission = sPermission & "0"
                            End If
                            If (dt.Rows(i)("EFP_SEARCH") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("EFP_MOD_FOLDER") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("EFP_DEL_FOLDER") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("EFP_VIEW_FOL") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        Next

                        sArray = sPermission.Split(",")
                        For i = 0 To chkPermission.Items.Count - 1
                            If sArray(i) = 1 Then
                                chkPermission.Items(i).Selected = True
                            Else
                                chkPermission.Items(i).Selected = False
                            End If
                        Next
                    Else
                        lblPrmError.Text = "No Permissions Assigned"
                        For i = 0 To chkPermission.Items.Count - 1
                            chkPermission.Items(i).Selected = False
                        Next
                    End If
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermissionDep_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionUser.SelectedIndexChanged
        Try
            iUsrType = objCab.GetUserType(sSession.AccessCode, ddlPermissionUser.SelectedValue)
            iMemType = objCab.GetMemberType(sSession.AccessCode, ddlPermissionUser.SelectedValue)
            If ddlPermissionLevel.SelectedIndex = 1 Then
                If (iUsrType = 1 Or iMemType = 1) Then
                    For i = 0 To chkPermission.Items.Count - 1
                        chkPermission.Items(i).Selected = True
                    Next
                    'BindChkBoxPermission()    'vijeth  24/01/19
                Else
                    dt = objclsFolders.BindCheckPermissionFolUser(iEFP_NODE, ddlPermissionDep.SelectedValue, sSession.AccessCode, ddlPermissionUser.SelectedValue)
                    If (dt.Rows.Count > 0) Then
                        If (dt.Rows(0)("EFP_Other") = 0) Then
                            CBLAssignP.Checked = True
                        Else
                            CBLAssignP.Checked = False
                        End If
                        For i = 0 To dt.Rows.Count - 1
                            If (dt.Rows(i)("EFP_INDEX") = 1) Then
                                sPermission = sPermission & "1"
                            Else
                                sPermission = sPermission & "0"
                            End If
                            If (dt.Rows(i)("EFP_SEARCH") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("EFP_MOD_FOLDER") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("EFP_DEL_FOLDER") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("EFP_VIEW_FOL") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        Next
                        sArray = sPermission.Split(",")
                        For i = 0 To chkPermission.Items.Count - 1
                            If sArray(i) = 1 Then
                                chkPermission.Items(i).Selected = True
                            Else
                                chkPermission.Items(i).Selected = False
                            End If
                        Next
                    Else
                        lblPrmError.Text = "No Permissions Assigned"
                        For i = 0 To chkPermission.Items.Count - 1
                            chkPermission.Items(i).Selected = False
                        Next
                    End If
                End If
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermissionUser_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindChkBoxPermission()
        Dim dt As New DataTable
        Dim sArray() As String
        Dim sPermission As String = ""
        Dim i As Integer
        Try
            If ddlPermissionDep.SelectedValue = "Select Department" Then 'Vijeth
                For i = 0 To chkPermission.Items.Count - 1
                    chkPermission.Items(i).Selected = False
                Next
                Exit Sub
            End If
            If (ddlPermissionLevel.SelectedIndex = 1) Then ' vijaylakshmi
                If ddlPermissionUser.SelectedValue = "Select" Then  'Vijeth
                    For i = 0 To chkPermission.Items.Count - 1
                        chkPermission.Items(i).Selected = False
                    Next
                    Exit Sub
                End If
            End If
            lblPrmError.Text = "" : lblError.Text = ""
            If ddlPermissionLevel.SelectedIndex = 1 Then
                LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, iFol_Id)
            ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, iFol_Id)
            End If

            'If dt.Rows.Count > 0 Then

            '    If dt.Rows(i)("EFP_Other").ToString = 0 Then  'Vijeth
            '        CBLAssignP.Checked = True
            '    Else
            '        CBLAssignP.Checked = False
            '    End If

            '    If dt.Rows(i)("EFP_Mod_Folder").ToString = 1 Then
            '        sPermission = sPermission & "1"
            '    Else
            '        sPermission = sPermission & "0"
            '    End If
            '    If dt.Rows(i)("EFP_DEL_FOLDER").ToString = 1 Then
            '        sPermission = sPermission & "," & "1"
            '    Else
            '        sPermission = sPermission & "," & "0"
            '    End If
            '    If dt.Rows(i)("EFP_View_Fol").ToString = 1 Then
            '        sPermission = sPermission & "," & "1"
            '    Else
            '        sPermission = sPermission & "," & "0"
            '    End If
            '    'If dt.Rows(i)("EFP_MOD_Doc").ToString = 1 Then
            '    '    sPermission = sPermission & "," & "1"
            '    'Else
            '    '    sPermission = sPermission & "," & "0"
            '    'End If
            '    'If dt.Rows(i)("EFP_DEL_Doc").ToString = 1 Then
            '    '    sPermission = sPermission & "," & "1"
            '    'Else
            '    '    sPermission = sPermission & "," & "0"
            '    'End If
            '    'If dt.Rows(i)("EFP_CRT_Doc").ToString = 1 Then
            '    '    sPermission = sPermission & "," & "1"
            '    'Else
            '    '    sPermission = sPermission & "," & "0"
            '    'End If
            '    If dt.Rows(i)("EFP_Search").ToString = 1 Then
            '        sPermission = sPermission & "," & "1"
            '    Else
            '        sPermission = sPermission & "," & "0"
            '    End If
            '    If dt.Rows(i)("EFP_INDEX").ToString = 1 Then
            '        sPermission = sPermission & "," & "1"
            '    Else
            '        sPermission = sPermission & "," & "0"
            '    End If
            '    'If dt.Rows(i)("EFP_Export").ToString = 1 Then
            '    '    sPermission = sPermission & "," & "1"
            '    'Else
            '    '    sPermission = sPermission & "," & "0"
            '    'End If
            '    sArray = sPermission.Split(",")
            '    For i = 0 To chkPermission.Items.Count - 1
            '        If sArray(i) = 1 Then
            '            chkPermission.Items(i).Selected = True
            '        Else
            '            chkPermission.Items(i).Selected = False
            '        End If
            '    Next
            '    If dt.Rows(0).Item("EFP_other") = 0 Then
            '        CBLAssignP.Checked = True
            '    Else
            '        CBLAssignP.Checked = False
            '    End If
            'Else
            '    lblPrmError.Text = "No Permissions Assigned"
            '    For i = 0 To chkPermission.Items.Count - 1
            '        chkPermission.Items(i).Selected = False
            '    Next
            'End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChkBoxPermission" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click 'Checked
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = ""
            iRet = objclsFolders.CheckFoldersName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(txtFolName.Text), ddlSubCabinet.SelectedValue, iFol_Id)
            If iRet = 0 Then
                If IsDBNull(txtFolName.Text) = False Then
                    objclsFolders.sFol_Name = objclsEDICTGeneral.SafeSQL(txtFolName.Text)
                Else
                    objclsFolders.sFol_Name = ""
                End If

                If IsDBNull(txtFolNotes.Text) = False Then
                    objclsFolders.sFol_Notes = objclsEDICTGeneral.SafeSQL(txtFolNotes.Text)
                Else
                    objclsFolders.sFol_Notes = ""
                End If

                objclsFolders.iFol_Id = 0
                objclsFolders.iFol_Cab = ddlSubCabinet.SelectedValue
                objclsFolders.sFol_Delflag = "W"
                objclsFolders.sFol_Status = "C"
                objclsFolders.iFol_Crby = sSession.UserID
                objclsFolders.iFol_UpdatedBy = sSession.UserID
                objclsFolders.iFol_CompId = sSession.AccessCodeID

                Arr = objclsFolders.SaveFolderDetails(sSession.AccessCode, sSession.AccessCodeID, objclsFolders)
                objclsFolders.UpdateFolderCount(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue)

                If Arr(0) = "3" Then
                    objclsFolders.iEFP_Id = 0
                    objclsFolders.iEFP_GPID = objclsFolders.GetDepID(sSession.AccessCode, ddlSubCabinet.SelectedValue)
                    objclsFolders.iEFP_FolId = objclsFolders.GetFolID(sSession.AccessCode)
                    Try
                        objclsFolders.SaveDefaultPermission(sSession.AccessCode, objclsFolders)  'Vijeth 29/01/19[Defalut Group Permission]
                    Catch ex As Exception
                    End Try

                    lblError.Text = "Successfully Saved & Waiting for Approval."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Saved & Waiting for Approval."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "Saved", "0", sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    ddlStatus.SelectedIndex = 2
                    ddlStatus_SelectedIndexChanged(sender, e)
                End If
            Else
                lblModelError.Text = "Folder Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtFolName.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Dim iCabId As Integer
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click  'CHecked
        Dim Arr() As String, Arry() As String
        Dim sPermission As String = ""
        Dim sArray As Array
        Dim bRet
        Dim objSrtPer As New SrtFolPer
        Try
            lblModelError.Text = ""
            If IsDBNull(txtFolName.Text) = False Then
                objclsFolders.sFol_Name = objclsEDICTGeneral.SafeSQL(txtFolName.Text)
            Else
                objclsFolders.sFol_Name = ""
            End If

            If IsDBNull(txtFolNotes.Text) = False Then
                objclsFolders.sFol_Notes = objclsEDICTGeneral.SafeSQL(txtFolNotes.Text)
            Else
                objclsFolders.sFol_Notes = ""
            End If

            objclsFolders.iFol_Id = iFol_Id
            objclsFolders.iFol_Cab = ddlSubCabinet.SelectedValue
            objclsFolders.sFol_Delflag = "A"
            objclsFolders.sFol_Status = "U"
            objclsFolders.iFol_Crby = sSession.UserID
            objclsFolders.iFol_UpdatedBy = sSession.UserID
            objclsFolders.iFol_CompId = sSession.AccessCodeID


            objclsFolders.UpdateFolderCount(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, ddlSubCabinet.SelectedValue)
            Arr = objclsFolders.SaveFolderDetails(sSession.AccessCode, sSession.AccessCodeID, objclsFolders)

            If ddlPermissionLevel.SelectedIndex > 0 Then

                If ddlPermissionDep.SelectedIndex = 0 Then
                    lblModelError.Text = "Select Department."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    ddlPermissionDep.Focus()
                    Exit Sub
                End If

                objclsFolders.iEFP_Id = 0
                objclsFolders.iEFP_Other = 0
                objclsFolders.iEFP_FolId = iFol_Id
                objclsFolders.iEFP_GPID = ddlPermissionDep.SelectedValue

                For i = 0 To chkPermission.Items.Count - 1
                    If chkPermission.Items(i).Selected = True Then
                        sPermission = sPermission & "," & "1"
                    Else
                        sPermission = sPermission & "," & "0"
                    End If
                Next

                If ddlPermissionLevel.SelectedIndex = 1 Then

                    If ddlPermissionUser.SelectedIndex = 0 Then
                        lblModelError.Text = "Select User."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                        ddlPermissionUser.Focus()
                        Exit Sub
                    End If

                    objclsFolders.sEFP_Ptype = "U"
                    If CBLAssignP.Checked = True Then    'Vijeth
                        objclsFolders.iEFP_Other = 0
                        objSrtPer.iOther = 0
                    Else
                        objclsFolders.iEFP_Other = 1
                        objSrtPer.iOther = 1
                    End If
                    If ddlPermissionUser.SelectedIndex > 0 Then
                        objclsFolders.iEFP_USRID = ddlPermissionUser.SelectedValue
                        'objclsFolders.DeleteFolPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, iFol_Id)
                    Else
                        objclsFolders.iEFP_USRID = 0
                        ' objclsFolders.DeleteFolPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, iFol_Id)
                    End If
                ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                    objclsFolders.sEFP_Ptype = "G"
                    objclsFolders.iEFP_Other = 0
                    objSrtPer.iOther = 0
                    objclsFolders.iEFP_USRID = 0
                    'objclsFolders.DeleteFolPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, iFol_Id)
                End If

                If (UCase(ddlPermissionLevel.SelectedItem.ToString) = "GROUP") Then
                    If (ddlPermissionDep.SelectedValue = 0) Then
                        'MessageBox.Show("Please select" & " " & sGroupCaption, AppName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    objSrtPer.iGrpId = ddlPermissionDep.SelectedValue
                    objSrtPer.iUsrId = 0
                    objSrtPer.cLvlType = "G"
                ElseIf (UCase(ddlPermissionLevel.SelectedItem.ToString) = "USER") Then
                    If (ddlPermissionDep.SelectedValue = 0) Then
                        'MessageBox.Show("Please select" & " " & sGroupCaption, AppName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    If (ddlPermissionUser.SelectedValue = 0) Then
                        'MessageBox.Show("Please select user", AppName, MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        Exit Sub
                    End If
                    objSrtPer.iGrpId = ddlPermissionDep.SelectedValue
                    objSrtPer.iUsrId = ddlPermissionUser.SelectedValue
                    objSrtPer.cLvlType = "U"
                End If
                If (ddlPermissionDep.SelectedValue = -1) Then
                    objSrtPer.iGrpId = 0
                    objSrtPer.iUsrId = 0
                    objSrtPer.cLvlType = "E"
                End If
                objSrtPer.iCabId = objclsFolders.iFol_Cab
                If (iFol_Id = 0) Then
                    'objSrtPer.iFolId = CType(lblMsg.Text, Integer)
                Else
                    objSrtPer.iFolId = iFol_Id
                End If


                sArray = sPermission.Split(",")
                For i = 0 To chkPermission.Items.Count - 1
                    If sArray(0) = "" Then
                    End If
                    If sArray(1) = 1 Then
                        chkPermission.Items(0).Selected = True
                        objSrtPer.iIndex = 1
                    Else
                        chkPermission.Items(0).Selected = False
                    End If
                    If sArray(2) = 1 Then
                        chkPermission.Items(1).Selected = True
                        objSrtPer.iSearch = 1
                    Else
                        chkPermission.Items(1).Selected = False
                    End If
                    If sArray(3) = 1 Then
                        chkPermission.Items(2).Selected = True
                        objSrtPer.iModFol = 1
                    Else
                        chkPermission.Items(2).Selected = False

                    End If
                    If sArray(4) = 1 Then
                        chkPermission.Items(3).Selected = True
                        objSrtPer.iDelDoc = 1
                    Else
                        chkPermission.Items(3).Selected = False
                    End If
                    If sArray(5) = 1 Then
                        chkPermission.Items(4).Selected = True
                        objSrtPer.iViewFol = 1
                    Else
                        chkPermission.Items(4).Selected = False
                    End If
                Next


                bRet = objclsFolders.CheckForCabPerm(objSrtPer, sSession.AccessCode, "F", ddlSubCabinet.SelectedValue)
                If (bRet = False) Then
                    lblPrmError.Text = "Please give these permissions to the  parent level"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    Exit Sub
                End If

                sArray = sPermission.Split(",")
                Arry = objclsFolders.SavePermission(sSession.AccessCode, objclsFolders, sArray)
                lblPrmError.Text = "Permissions Successfully Assigned"
            End If
            If Arr(0) = "2" Then
                lblModelError.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Folder", "Updated", iFol_Id, sSession.YearName, 0, "", sSession.IPAddress)
            End If
            pnlFolder.Visible = True
            btnDescSave.Visible = False : btnDescUpdate.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescUpdate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            ReportViewer1.Reset()
            dtFol = objclsFolders.LoadFolders(sSession.AccessCode, sSession.AccessCodeID, ddlSubCabinet.SelectedValue, sSession.UserID)
            dt = BindFolders(ddlSubCabinet.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/Folders.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Folders" + ".xls")
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
            dtFol = objclsFolders.LoadFolders(sSession.AccessCode, sSession.AccessCodeID, ddlSubCabinet.SelectedValue, sSession.UserID)
            dt = BindFolders(ddlSubCabinet.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/Folders.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Folders" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oCabID As New Object, oBackID As New Object
        Try
            oCabID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
            oBackID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(iBackID))
            If iBackID = 0 Then
                Response.Redirect(String.Format("~/DigitalFilling/SubCabinet.aspx?CabinetID={0}", oCabID), False) 'DigitalFiling/SubCabinet
            Else
                Response.Redirect(String.Format("~/DigitalFilling/SubCabinet.aspx?CabinetID={0}&BackID={1}", oCabID, oBackID), False) 'DigitalFiling/SubCabinet
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgFolders_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dgFolders.RowCreated
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lnkDocuments As New LinkButton
        Dim lblDocuments As New Label
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                lnkDocuments = CType(e.Row.FindControl("lnkDocuments"), LinkButton)
                lblDocuments = CType(e.Row.FindControl("lblDocuments"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                If lblDocuments.Text = "0" Then
                    lblDocuments.Visible = True
                    lnkDocuments.Visible = False
                Else
                    lnkDocuments.Visible = True
                End If

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgFolders.Columns(0).Visible = True : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgFolders.Columns(9).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgFolders.Columns(10).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgFolders.Columns(0).Visible = True : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgFolders.Columns(9).Visible = True
                    'dgFolders.Columns(10).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgFolders.Columns(0).Visible = True : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgFolders.Columns(9).Visible = True
                    'dgFolders.Columns(10).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgFolders.Columns(0).Visible = False : dgFolders.Columns(9).Visible = False : dgFolders.Columns(10).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgFolders_RowCreated" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadPermission(ByVal sNameSpace As String, ByVal iEFP_GrpID As Integer, ByVal iEFP_USRID As Integer, ByVal iEFP_FolID As Integer) As DataTable
        Dim sSql As String
        Dim PermDt As DataTable
        Dim i As Int16
        Dim ht As Hashtable
        Dim objFolDis As New clsFolders
        Dim sLevel As String
        Dim sPermission As String = ""
        Dim sArray() As String
        Dim objColl As System.Collections.IDictionaryEnumerator
        Dim Keys As System.Collections.ICollection
        Try
            If (iEFP_USRID <> 0) Then
                If (iEFP_FolID = 0) Then
                    ht = New Hashtable
                    ht = objFolDis.GetFinalFolPermissions(iEFP_FolID, iEFP_USRID, sNameSpace, "ALL", 2)
                    'PermDt = objClsCab.RetrievePermissions(lblMsg.Text, iGrpId, iUsrId)
                Else
                    'PermDt = objClsCab.RetrievePermissions(iCabId, iGrpId, iUsrId)
                    ht = New Hashtable
                    ht = objFolDis.GetFinalFolPermissions(iEFP_FolID, iEFP_USRID, sNameSpace, "ALL", 2)
                End If
                If ht Is Nothing Then
                    Exit Function
                End If
                'Assign the Level of Permission to the Label
                sLevel = ht("Level")
                Select Case UCase(sLevel)
                    'Case "PG"
                    '    lblPerm.Text = "Group Level Permissions"
                    'Case "GH"
                    '    lblPerm.Text = "Group Level Permissions"
                    'Case "PU"
                    '    lblPerm.Text = "POWER USER"
                    'Case "G"
                    '    lblPerm.Text = "Group Level Permissions"
                    'Case "U"
                    '    lblPerm.Text = "User Level Permissions"
                    'Case "E"
                    '    lblPerm.Text = "Permissions given to EveryOne"
                End Select
                objColl = ht.GetEnumerator()
                objColl.Reset()

                Keys = ht.Keys
                For i = 0 To Keys.Count - 1
                    objColl.MoveNext()
                    Select Case UCase(objColl.Key.ToString)

                        Case "INDEX"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        Case "SEARCH"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        Case "FMODIFY"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "1"
                            Else
                                sPermission = sPermission & "0"
                            End If
                        '
                        Case "FDELETE"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        Case "FVIEW"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If


                            'Case "DMODIFY"
                            '    If (objColl.Value = 1) Then
                            '        sPermission = sPermission & "," & "1"
                            '    Else
                            '        sPermission = sPermission & "," & "0"
                            '    End If
                            'Case "DDELETE"
                            '    If (objColl.Value = 1) Then
                            '        sPermission = sPermission & "," & "1"
                            '    Else
                            '        sPermission = sPermission & "," & "0"
                            '    End If
                            'Case "DCREATE"
                            '    If (objColl.Value = 1) Then
                            '        sPermission = sPermission & "," & "1"
                            '    Else
                            '        sPermission = sPermission & "," & "0"
                            '    End If


                            'Case "EXPORT"
                            '    If (objColl.Value = 1) Then
                            '        sPermission = sPermission & "," & "1"
                            '    Else
                            '        sPermission = sPermission & "," & "0"
                            '    End If

                    End Select
                Next
                sArray = sPermission.Split(",")
                For i = 0 To chkPermission.Items.Count - 1
                    If sArray(i) = 1 Then
                        chkPermission.Items(i).Selected = True
                    Else
                        chkPermission.Items(i).Selected = False
                    End If
                Next
            Else
                If (iEFP_FolID = 0) Then
                    PermDt = objFolDis.RetrievePermissions(sNameSpace, iEFP_FolID, iEFP_GrpID, iEFP_USRID)
                Else
                    PermDt = objFolDis.RetrievePermissions(sNameSpace, iEFP_FolID, iEFP_GrpID, iEFP_USRID)
                End If
                If PermDt.Rows.Count > 0 Then
                    For i = 0 To PermDt.Rows.Count - 1
                        Select Case PermDt.Rows(i).Item("PerName")
                            Case "IND"
                                If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                    sPermission = sPermission & "1"
                                Else
                                    sPermission = sPermission & "0"
                                End If
                            Case "SRH"
                                If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
                            Case "MFD"
                                If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
                            Case "DFD"
                                If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
                            Case "VFD"
                                If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If


                                'Case "MDC"
                                '    If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                '        sPermission = sPermission & "," & "1"
                                '    Else
                                '        sPermission = sPermission & "," & "0"
                                '    End If
                                'Case "DDC"
                                '    If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                '        sPermission = sPermission & "," & "1"
                                '    Else
                                '        sPermission = sPermission & "," & "0"
                                '    End If
                                'Case "CDC"
                                '    If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                '        sPermission = sPermission & "," & "1"
                                '    Else
                                '        sPermission = sPermission & "," & "0"
                                '    End If

                                'Case "EXP"
                                '    If (PermDt.Rows(i).Item("PerValue") = 1) Then
                                '        sPermission = sPermission & "," & "1"
                                '    Else
                                '        sPermission = sPermission & "," & "0"
                                '    End If
                        End Select
                    Next
                    sArray = sPermission.Split(",")
                    For i = 0 To chkPermission.Items.Count - 1
                        If sArray(i) = 1 Then
                            chkPermission.Items(i).Selected = True
                        Else
                            chkPermission.Items(i).Selected = False
                        End If
                    Next
                Else
                    lblPrmError.Text = "No Permissions Assigned"
                    For i = 0 To chkPermission.Items.Count - 1
                        chkPermission.Items(i).Selected = False
                    Next
                End If
            End If
            'sSql = "Select * from edt_Folder_Permission where EFP_GrpID='" & iEFP_GrpID & "' and EFP_USRID='" & iEFP_USRID & "' and EFP_FolID='" & iEFP_FolID & "' "
            'Return objDb.SQLExecuteDataTable(sNameSpace, sSql)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPermission" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub chkPermission_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPermission.SelectedIndexChanged
        Try
            If (chkPermission.SelectedIndex > 0) Then
                chkPermission.Items(4).Selected = True
            Else
                If (chkPermission.Items(4).Selected = False) Then
                    For i = 0 To chkPermission.Items.Count - 1
                        chkPermission.Items(i).Selected = False
                    Next
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkPermission_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try

    End Sub
End Class
