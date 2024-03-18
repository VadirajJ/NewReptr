Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports BusinesLayer.clsCabinet

Partial Class Cabinet
    Inherits System.Web.UI.Page
    Private sFormName As String = "DigitalFiling_Cabinet"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Dim objCab As New clsCabinet
    Private objclsPermission As New clsAccessRights
    Private Shared dtCab As DataTable
    'Private Shared sDESGSave As String
    'Private Shared sDESGAD As String
    Private Shared iCBN_NODE As Integer = 0
    Private Shared sDeptID As String
    Dim iMemType As Integer
    Dim iUsrType As Integer
    Dim dt As DataTable
    Dim sPermission As String = ""
    Dim sArray() As String
    Private objclsOrgStructure As New clsOrgStructure
    Private objclsGRACeGeneral As New clsGRACeGeneral
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
                'imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                'ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : chkPermission.Enabled = False : ddlPermissionLevel.Enabled = False : btnDescUpdate.Visible = False
                'pnlPermission.Visible = False
                'btnDescUpdate.Visible = False : btnDescSave.Visible = False : ddlNewDepartment.Visible = False : lblNewDepartment.Visible = False
                'btnNewDepartment.Visible = False
                'sDESGSave = "NO" : sDESGAD = "NO"
                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DFC")
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
                '        imgbtnReport.Visible = True
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '        sDESGSave = "YES" : sDESGAD = "YES"
                '        imgbtnAdd.Visible = True : btnDescSave.Visible = True
                '    End If
                'End If

                BindStatus() : BindDepartment() : BindPermissionLevel() : BindPermissionDept() : BindPermissionUser()
                BindChkBoxList()

                dtCab = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)
                'dtCab.Merge(dtCab)
                'imgbtnAdd.Attributes.Add("OnClick", "$('#myModal').modal('show');return false;")

                BindCabinet()

                RFVCabName.ControlToValidate = "txtCabName" : RFVCabName.ErrorMessage = "Enter Cabinet Name."
                REVCabName.ErrorMessage = "Cabinet Name exceeded maximum size(max 100 characters)." : REVCabName.ValidationExpression = "^[\s\S]{0,100}$"
                REVCabNotes.ErrorMessage = "Cabinet Notes exceeded maximum size(max 255 characters)." : REVCabNotes.ValidationExpression = "^[\s\S]{0,255}$"
                RFVCabDept.InitialValue = "Select Department" : RFVCabDept.ErrorMessage = "Select Department."

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
    Public Sub BindDepartment()
        Dim dtDept As New DataTable
        Dim sDept As String = ""
        Try
            sDeptID = "" : sDept = ""
            dtDept = objCab.LoadUserOtherDepartment(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            For i = 0 To dtDept.Rows.Count - 1
                sDept = sDept & "," & dtDept.Rows(i).Item("Org_Node")
            Next
            sDeptID = sDept.Remove(0, 1)
            ddlCabDept.DataSource = dtDept
            ddlCabDept.DataTextField = "Org_Name"
            ddlCabDept.DataValueField = "Org_node"
            ddlCabDept.DataBind()
            ddlCabDept.Items.Insert(0, "Select Department")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDepartment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            ddlPermissionDep.DataValueField = "Org_node"
            ddlPermissionDep.DataBind()
            ddlPermissionDep.Items.Insert(0, "Select Department")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermissionDept" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPermissionUser()
        Try
            ddlPermissionUser.DataSource = objCab.LoadCabUserPer(sSession.AccessCode, ddlPermissionDep.SelectedValue)
            ddlPermissionUser.DataTextField = "Usr_LoginName"
            ddlPermissionUser.DataValueField = "Usr_Id"
            ddlPermissionUser.DataBind()
            ddlPermissionUser.Items.Insert(0, "Select Users")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermissionUser" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindChkBoxList()
        Try
            'chkPermission.Items.Add("Create Sub Cabinet") '1
            'chkPermission.Items.Add("Modify Cabinet")    '2
            ''chkPermission.Items.Add("Delete Cabinet")   'Vijeth
            ''chkPermission.Items.Add("Create Folder")
            'chkPermission.Items.Add("Search")            '3
            'chkPermission.Items.Add("Index")             '4
            'chkPermission.Items.Add("View Cabinet")      '5

            chkPermission.Items.Add("Create Sub Cabinet")
            chkPermission.Items.Add("View Cabinet")
            chkPermission.Items.Add("Modify Cabinet")
            chkPermission.Items.Add("Index")
            chkPermission.Items.Add("Search")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChkBoxList" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindCabinet() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
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
                dtCab = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)
                Dim DVFunctionStatus As New DataView(dtCab)
                DVFunctionStatus.RowFilter = "cbn_DelFlag='" & sStatus & "'"
                DVFunctionStatus.Sort = "CBN_NAME Asc"
                dt = DVFunctionStatus.ToTable
            ElseIf ddlStatus.SelectedIndex = 1 Then
                dt = Nothing
                dtCab = objCab.GetAllCab(sSession.AccessCode, sStatus, sSession.UserID)
                Dim DVFunctionStatus As New DataView(dtCab)
                DVFunctionStatus.RowFilter = "cbn_DelFlag='" & sStatus & "'"
                DVFunctionStatus.Sort = "CBN_NAME Asc"
                dt = DVFunctionStatus.ToTable
            ElseIf ddlStatus.SelectedIndex = 2 Then
                dt = Nothing
                dtCab = objCab.GetAllCab(sSession.AccessCode, sStatus, sSession.UserID)
                Dim DVFunctionStatus As New DataView(dtCab)
                DVFunctionStatus.RowFilter = "cbn_DelFlag='" & sStatus & "'"
                DVFunctionStatus.Sort = "CBN_NAME Asc"
                dt = DVFunctionStatus.ToTable
            Else
                dt = Nothing
                dtCab = objCab.GetAllCab(sSession.AccessCode, sStatus, sSession.UserID)
                Dim DVFunctionStatus As New DataView(dtCab)
                DVFunctionStatus.Sort = "CBN_NAME Asc"
                dt = DVFunctionStatus.ToTable
            End If
            dgCabinet.DataSource = dt
            dgCabinet.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCabinet" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgCabinet.Rows.Count - 1
                    chkField = dgCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgCabinet.Rows.Count - 1
                    chkField = dgCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgCabinet_PreRender(sender As Object, e As EventArgs) Handles dgCabinet.PreRender
        Dim dt As New DataTable
        Try
            If dgCabinet.Rows.Count > 0 Then
                dgCabinet.UseAccessibleHeader = True
                dgCabinet.HeaderRow.TableSection = TableRowSection.TableHeader
                dgCabinet.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgCabinet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgCabinet.RowCommand
        Dim chkSelectAll As New CheckBox
        Dim lblCBN_NODE As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Dim oDescID As New Object, oCBN_NODE As New Object, oBackID As New Object
        Dim dt As New DataTable()
        Dim DVCabinet As New DataView(dtCab)
        Dim ExtraP As Integer
        CBLAssignP.Checked = False 'Vijeth
        Try

            lblError.Text = "" : sMainMaster = ""
            iUsrType = objCab.GetUserType(sSession.AccessCode, sSession.UserID)
            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                'ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblCBN_NODE.Text, "CBP_Modify") 'vijeth
                If (lblCBN_NODE.Text >= 0) Then
                    If (objCab.GetFinalPermissions(iUsrType, lblCBN_NODE.Text, sSession.UserID, sSession.AccessCode, "MCB", 2) <> 0) Then
                        'If ExtraP <> 0 Then
                        btnDescSave.Visible = False : btnDescUpdate.Visible = True : ddlPermissionLevel.Enabled = True
                        pnlPermission.Visible = True
                        oDescID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                        iCBN_NODE = Val(lblCBN_NODE.Text)

                        BindCabDetails(Val(lblCBN_NODE.Text))
                        lblModelError.Text = "" : lblPrmError.Text = "" : ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0 : ddlPermissionUser.Items.Clear()
                        For i = 0 To chkPermission.Items.Count - 1
                            chkPermission.Items(i).Selected = False
                        Next
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Cabinet Permission is not Assigned','', 'info');", True)
                    End If 'vijeth
                End If
            End If
            If e.CommandName = "Status" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                If ddlStatus.SelectedIndex = 0 Then
                    objCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "D", lblCBN_NODE.Text, "D", sSession.UserID)
                    DVCabinet.Sort = "CBN_ID"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("CBN_DELFLAG") = "De-Activated"
                    dtCab = DVCabinet.ToTable
                    lblError.Text = "Successfully De-Activated."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "De-Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "A", lblCBN_NODE.Text, "A", sSession.UserID)

                    DVCabinet.Sort = "CBN_ID"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("CBN_DELFLAG") = "Activated"
                    dtCab = DVCabinet.ToTable

                    lblError.Text = "Successfully Activated."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    ddlStatus.SelectedIndex = 0
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    objCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "W", lblCBN_NODE.Text, "A", sSession.UserID)

                    DVCabinet.Sort = "CBN_ID"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("CBN_DELFLAG") = "Activated"
                    dtCab = DVCabinet.ToTable

                    lblError.Text = "Successfully Approved."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    ddlStatus.SelectedIndex = 0

                End If
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
            If e.CommandName = "SelectCabinet" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                oCBN_NODE = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                oBackID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(1))
                Response.Redirect(String.Format("~/DigitalFilling/SubCabinet.aspx?CabinetID={0}&BackID={1}", oCBN_NODE, oBackID), False) 'DigitalFiling/SubCabinet               
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_ItemCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : lblModelError.Text = ""
            'btnDescSave.Visible = False : btnDescUpdate.Visible = False : pnlPermission.Visible = False
            'If sDESGAD = "YES" Then
            'End If
            'If sDESGSave = "YES" Then
            '    btnDescSave.Visible = True
            'End If
            txtCabName.Text = "" : txtCabNotes.Text = "" : ddlCabDept.SelectedIndex = 0
            ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            'btnDescSave.Visible = False : btnDescUpdate.Visible = False
            'If sDESGSave = "YES" Then
            '    btnDescSave.Visible = True
            'End If
            pnlPermission.Visible = False
            lblModelError.Text = "" : lblPrmError.Text = ""
            ddlPermissionLevel.Enabled = False : ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : lblUser.Visible = True : ddlPermissionUser.Visible = True
            txtCabName.Text = "" : txtCabNotes.Text = "" : ddlCabDept.SelectedIndex = 0
            ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0
            For i = 0 To chkPermission.Items.Count - 1
                chkPermission.Items(i).Selected = False
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescNew_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgCabinet_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgCabinet.RowDataBound
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lblCBNID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"


                dgCabinet.Columns(0).Visible = False : dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                'If sDESGAD = "YES" Then
                '    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = False
                'End If
                'If sDESGSave = "YES" Then
                '    dgCabinet.Columns(10).Visible = True
                'End If

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'dgCabinet.Columns(0).Visible = True : dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    '    dgCabinet.Columns(10).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'dgCabinet.Columns(0).Visible = True : dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = False
                    'End If
                    'If sDESGSave = "YES" Then
                    '    dgCabinet.Columns(10).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'dgCabinet.Columns(0).Visible = True : dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = False
                    'End If
                    'If sDESGSave = "YES" Then
                    dgCabinet.Columns(10).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgCabinet.Columns(0).Visible = False : dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            If ddlStatus.SelectedIndex = 0 Then
                imgbtnDeActivate.Visible = True 'Activate
            ElseIf ddlStatus.SelectedIndex = 1 Then
                imgbtnActivate.Visible = True 'De-Activate
            ElseIf ddlStatus.SelectedIndex = 2 Then
                imgbtnWaiting.Visible = True 'Waiting for Approval
            End If
            dt = BindCabinet()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindCabDetails(ByVal iCabinet As Integer)
        Dim dt As New DataTable
        Dim dept As DataSet
        Dim dtDept As New DataTable
        Dim sDept As String = ""

        ' dt = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, iCabinet, sDeptID, sSession.UserID)
        'If dt.Rows.Count > 0 Then

        '    If IsDBNull(dt.Rows(0)("CBN_NAME").ToString()) = False Then
        '        txtCabName.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_NAME").ToString())
        '    Else
        '        txtCabName.Text = ""
        '    End If

        '    If IsDBNull(dt.Rows(0)("CBN_Note").ToString()) = False Then
        '        txtCabNotes.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_Note").ToString())
        '    Else
        '        txtCabNotes.Text = ""
        '    End If

        '    If IsDBNull(dt.Rows(0)("Org_node").ToString()) = False Then
        '        ddlCabDept.SelectedValue = (dt.Rows(0)("Org_Node").ToString())
        '    Else
        '        ddlCabDept.SelectedIndex = 0
        '    End If

        'End If
        dt = objCab.GetCabBindDetails(sSession.AccessCode, iCabinet)
        Try
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("CBN_NAME").ToString()) = False Then
                    txtCabName.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_NAME").ToString())
                Else
                    txtCabName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("CBN_Note").ToString()) = False Then
                    txtCabNotes.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_Note").ToString())
                Else
                    txtCabNotes.Text = ""
                End If
                ' ddlCabDept.SelectedValue = ""
                If IsDBNull(dt.Rows(0)("CBN_Department").ToString()) = False Then

                    dept = objCab.getcabinetdept(dt.Rows(0)("CBN_Department"), sSession.AccessCode)
                    'dtDept = objCab.LoadUserOtherDepartment(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                    ddlCabDept.DataSource = dept
                    ddlCabDept.DataTextField = "Org_Name"
                    ddlCabDept.DataValueField = "Org_node"
                    ddlCabDept.DataBind()
                    ddlCabDept.Items.Insert(0, "Select Department")
                    ddlCabDept.SelectedIndex = 1
                Else
                    ddlCabDept.SelectedIndex = 0
                End If


            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCabDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim DVCabinet As New DataView(dtCab)
        Try
            lblError.Text = ""
            If dgCabinet.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
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

NextSave:   For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "A", lblCBN_NODE.Text, "A", sSession.UserID)

                    DVCabinet.Sort = "CBN_id"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("CBN_delflag") = "Activated"
                    dtCab = DVCabinet.ToTable

                    lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 0
            BindCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim DVCabinet As New DataView(dtCab)
        Try
            lblError.Text = ""
            If dgCabinet.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "D", lblCBN_NODE.Text, "D", sSession.UserID)
                    DVCabinet.Sort = "cbn_id"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("cbn_delflag") = "De-Activated"
                    dtCab = DVCabinet.ToTable
                    lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "De-Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                End If
            Next
            ddlStatus.SelectedIndex = 1
            BindCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim DVCabinet As New DataView(dtCab)
        Try
            lblError.Text = ""
            If dgCabinet.Rows.Count = 0 Then
                lblError.Text = "No data to Approve"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to Approve','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgCabinet.Rows.Count - 1
                chkSelect = dgCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then

                    objCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "W", lblCBN_NODE.Text, "A", sSession.UserID)

                    DVCabinet.Sort = "cbn_id"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("cbn_delflag") = "Activated"
                    dtCab = DVCabinet.ToTable

                    lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Approved", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                End If
            Next
            BindCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionLevel_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionLevel.SelectedIndexChanged
        Try
            lblModelError.Text = "" : lblPrmError.Text = "" : ddlPermissionDep.SelectedIndex = 0
            If ddlPermissionLevel.SelectedIndex = 0 Then
                ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : chkPermission.Enabled = False
            ElseIf ddlPermissionLevel.SelectedIndex = 1 Then
                ddlPermissionDep.Enabled = True : ddlPermissionUser.Enabled = True : chkPermission.Enabled = True : ddlPermissionUser.Visible = True : lblUser.Visible = True : CBLAssignP.Visible = True  'Vijeth
            ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                ddlPermissionDep.Enabled = True : ddlPermissionUser.Visible = False : chkPermission.Enabled = True : lblUser.Visible = False : CBLAssignP.Visible = False 'Vijeth
            End If
            CBLAssignP.Visible = False
            For i = 0 To chkPermission.Items.Count - 1
                chkPermission.Items(i).Selected = False
            Next
            CBLAssignP.Checked = False
            ChkFC.Checked = False
            ChkSC.Checked = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermissionLevel_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionDep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionDep.SelectedIndexChanged
        Try
            If ddlPermissionDep.SelectedValue = "Select Department" Then 'Vijeth
                For i = 0 To chkPermission.Items.Count - 1
                    chkPermission.Items(i).Selected = False
                Next
                Exit Sub
            End If
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
                    dt = objCab.BindCheckPermissionDep(iCBN_NODE, ddlPermissionDep.SelectedValue, sSession.AccessCode)
                    If (dt.Rows.Count > 0) Then
                        For i = 0 To dt.Rows.Count - 1
                            If (dt.Rows(i)("CBP_CREATE") = 1) Then
                                sPermission = sPermission & "1"
                            Else
                                sPermission = sPermission & "0"
                            End If
                            If (dt.Rows(i)("CBP_VIEW") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("CBP_MODIFY") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("CBP_INDEX") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("CBP_SEARCH") = 1) Then
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
        CBLAssignP.Visible = True
        Try
            iUsrType = objCab.GetUserType(sSession.AccessCode, ddlPermissionUser.SelectedValue) 'ddlPermissionUser.SelectedValue
            iMemType = objCab.GetMemberType(sSession.AccessCode, ddlPermissionUser.SelectedValue)
            If ddlPermissionUser.SelectedValue = "Select Users" Then  'Vijeth
                For i = 0 To chkPermission.Items.Count - 1
                    chkPermission.Items(i).Selected = False
                Next
                Exit Sub
            End If
            If ddlPermissionLevel.SelectedIndex = 1 Then
                If (iUsrType = 1 Or iMemType = 1) Then
                    For i = 0 To chkPermission.Items.Count - 1
                        chkPermission.Items(i).Selected = True
                    Next
                    'BindChkBoxPermission()
                Else
                    dt = objCab.BindCheckPermissionUser(iCBN_NODE, ddlPermissionDep.SelectedValue, sSession.AccessCode, ddlPermissionUser.SelectedValue)
                    If (dt.Rows.Count > 0) Then
                        If (dt.Rows(0)("CBP_Other") = 0) Then
                            CBLAssignP.Checked = True
                        Else
                            CBLAssignP.Checked = False
                        End If

                        For i = 0 To dt.Rows.Count - 1
                            If (dt.Rows(i)("CBP_CREATE") = 1) Then
                                sPermission = sPermission & "1"
                            Else
                                sPermission = sPermission & "0"
                            End If
                            If (dt.Rows(i)("CBP_VIEW") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("CBP_MODIFY") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("CBP_INDEX") = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                            If (dt.Rows(i)("CBP_SEARCH") = 1) Then
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
    Dim sCabSub As Char = "C"
    Dim iCabId As Integer
    Dim iSubCabId As Integer
    Public Sub BindChkBoxPermission()
        Dim dt As New DataTable
        Dim sArray() As String
        Dim sPermission As String = ""
        Dim i As Integer
        Try

            lblPrmError.Text = "" : lblError.Text = ""
            If ddlPermissionLevel.SelectedIndex = 1 Then
                'dt = objCab.LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, iCBN_NODE)
                LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, iCBN_NODE)
            ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                ' dt = objCab.LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, iCBN_NODE)
                LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, iCBN_NODE)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChkBoxPermission" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = "" : pnlPermission.Visible = False
            iRet = objCab.CheckCabName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(txtCabName.Text), 0, ddlCabDept.SelectedValue)
            If iRet <> 0 Then
                lblModelError.Text = "Cabinet Name already exists."
                lblCabinetEmpMasterValidationMsg.Text = "Cabinet Name already exists."
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                txtCabName.Focus()
                Exit Sub
            End If


            objCab.iCBN_ID = 0
            objCab.sCBN_Name = objclsEDICTGeneral.SafeSQL(txtCabName.Text)
            objCab.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtCabNotes.Text)
            objCab.iCBN_Parent = "-1"
            objCab.iCBN_UserID = sSession.UserID
            objCab.iCBN_Department = ddlCabDept.SelectedValue
            objCab.iCBN_SubCabCount = 0
            objCab.iCBN_FolderCount = 0
            objCab.iCBN_CreatedBy = sSession.UserID
            objCab.sCBN_Status = "C"
            objCab.sCBN_DelFlag = "W"
            Arr = objCab.SaveCabDetails(sSession.AccessCode, sSession.AccessCodeID, objCab)

            'objCab.SaveDefaultPermission(sSession.AccessCode, objCab) 'Add default group permission[updated by vijeth 13/03/18]
            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                lblCabinetEmpMasterValidationMsg.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Updated", "0", sSession.YearName, 0, "", sSession.IPAddress)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                objCab.iCBP_ID = 0
                objCab.iCBP_Cabinet = objCab.GetCabID(sSession.AccessCode)
                objCab.iCBP_Department = ddlCabDept.SelectedValue
                Try
                    objCab.SaveDefaultPermission(sSession.AccessCode, objCab) 'Vijeth 29/01/19[Defalut Group Permission]
                Catch ex As Exception
                End Try

                lblError.Text = "Successfully Saved & Waiting for Approval."
                lblCabinetEmpMasterValidationMsg.Text = "Successfully Saved & Waiting for Approval."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Saved", "0", sSession.YearName, 0, "", sSession.IPAddress)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
            End If
            dtCab = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)

            BindCabinet()
            ddlPermissionLevel.Enabled = True
            ddlStatus.SelectedIndex = 2
            ddlStatus_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Dim Arr() As String, Arry() As String
        Dim sPermission As String = ""
        Dim iRet As Integer
        Dim sArray As Array
        Dim iCreate As Integer = 0, iModify As Integer = 0
        Dim objSrtPer As New SrtCabPer
        Dim i As Int16

        Try
            lblModelError.Text = ""
            objCab.iCBN_ID = iCBN_NODE
            objCab.sCBN_Name = objclsEDICTGeneral.SafeSQL(txtCabName.Text)
            objCab.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtCabNotes.Text)
            objCab.iCBN_Department = ddlCabDept.SelectedValue
            iRet = objCab.CheckCabName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(txtCabName.Text), iCBN_NODE, 0)
            If iRet <> 0 Then
                lblModelError.Text = "Cabinet Name already exists."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                lblCabinetEmpMasterValidationMsg.Text = "Cabinet Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                txtCabName.Focus()
                Exit Sub
            End If
            objCab.sCBN_Status = "U"
            objCab.sCBN_DelFlag = "A"
            objCab.iCBN_Parent = "-1"
            objCab.iCBN_UserID = sSession.UserID
            objCab.iCBN_CreatedBy = sSession.UserID
            objCab.iCBN_UpdatedBy = sSession.UserID
            Arr = objCab.SaveCabDetails(sSession.AccessCode, sSession.AccessCodeID, objCab)

            If ddlPermissionLevel.SelectedIndex > 0 Then

                If ddlPermissionDep.SelectedIndex = 0 Then
                    lblModelError.Text = "Select Department."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Select Department."

                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    ddlPermissionDep.Focus()
                    Exit Sub
                End If

                objCab.iCBP_ID = 0
                objCab.iCBP_Cabinet = iCBN_NODE
                objCab.iCBP_Department = ddlPermissionDep.SelectedValue

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

                    objCab.sCBP_PermissionType = "U"
                    objCab.iCBP_User = ddlPermissionUser.SelectedValue
                    If CBLAssignP.Checked = True Then    'Vijeth
                        objCab.iCBP_Others = 0
                        objSrtPer.iOther = 0
                    Else
                        objCab.iCBP_Others = 1
                        objSrtPer.iOther = 1
                    End If
                    ' objCab.DeleteCabPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, iCBN_NODE) 'Vijeth
                ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                    objCab.sCBP_PermissionType = "G"
                    objCab.iCBP_User = 0
                    'If CBLAssignP.Checked = True Then    'Vijeth
                    objCab.iCBP_Others = 0
                    objSrtPer.iOther = 0
                    'Else
                    'objCab.iCBP_Others = 1
                    'End If
                    'objCab.DeleteCabPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, iCBN_NODE) 'Vijeth

                End If

                sArray = sPermission.Split(",")
                Arry = objCab.SavePermission(sSession.AccessCode, objCab, sArray)
                lblPrmError.Text = "Permissions Successfully Assigned"
            End If

            objCab.UpdateCabDetails(sSession.AccessCode, ddlCabDept.SelectedValue, iCBN_NODE)


            '/// code for Entire file plan and subcabinet
            If (ddlPermissionLevel.SelectedIndex <> 0) Then
                If (ddlPermissionLevel.SelectedIndex = 2) Then
                    If (ddlPermissionDep.SelectedValue = 0) Then
                        Exit Sub
                    End If
                    objSrtPer.iGrpId = ddlPermissionDep.SelectedValue
                    objSrtPer.iUsrId = 0
                    objSrtPer.cLvlType = "G"

                ElseIf (ddlPermissionLevel.SelectedIndex = 1) Then
                    If (ddlPermissionDep.SelectedValue = 0) Then

                        Exit Sub
                    End If
                    If (ddlPermissionUser.SelectedValue = 0) Then

                        Exit Sub
                    End If
                    objSrtPer.iGrpId = ddlPermissionDep.SelectedValue
                    objSrtPer.iUsrId = ddlPermissionUser.SelectedValue
                    objSrtPer.cLvlType = "U"
                End If
                If (sCabSub = "C") Then
                    If (iCabId = 0) Then
                        objSrtPer.iCabId = iCBN_NODE
                    Else
                        objSrtPer.iCabId = iCabId
                    End If
                ElseIf (sCabSub = "S") Then
                    If (iSubCabId = 0) Then
                        objSrtPer.iCabId = iCBN_NODE
                    Else
                        objSrtPer.iCabId = iSubCabId
                    End If
                End If

                sArray = sPermission.Split(",")
                For i = 0 To chkPermission.Items.Count - 1
                    If sArray(0) = "" Then
                    End If
                    If sArray(1) = 1 Then
                        chkPermission.Items(0).Selected = True
                        objSrtPer.iCrSubCab = 1
                    Else
                        chkPermission.Items(0).Selected = False
                    End If
                    If sArray(2) = 1 Then
                        chkPermission.Items(1).Selected = True
                        objSrtPer.iView = 1
                    Else
                        chkPermission.Items(1).Selected = False
                    End If
                    If sArray(3) = 1 Then
                        chkPermission.Items(2).Selected = True
                        objSrtPer.iModCab = 1
                    Else
                        chkPermission.Items(2).Selected = False
                    End If
                    If sArray(4) = 1 Then
                        chkPermission.Items(3).Selected = True
                        objSrtPer.iIndex = 1
                    Else
                        chkPermission.Items(3).Selected = False
                    End If
                    If sArray(5) = 1 Then
                        chkPermission.Items(4).Selected = True
                        objSrtPer.iSearch = 1
                    Else
                        chkPermission.Items(4).Selected = False
                    End If
                Next

            End If
            If (ChkFC.Checked = True) Then
                objCab.ExtendPermissions(objSrtPer, sCabSub, sSession.AccessCode, objCab.sCBP_PermissionType)
                ChkSC.Checked = False
            End If
            If (ChkSC.Checked = True) Then
                objCab.ExtendPermissions(objSrtPer, sCabSub, sSession.AccessCode, objCab.sCBP_PermissionType, "S")
            End If '/// till here

            If (chkPermission.SelectedValue <> Nothing) Then
                If (objCab.chkRemFlagCabPerm(objCab.sCBP_PermissionType, objSrtPer.iCabId, objCab.iCBP_User, ddlPermissionDep.SelectedValue, sSession.AccessCode) = True) Then
                    lblPrmError.Text = "Permission are removed"
                Else
                    lblPrmError.Text = "No Permissions Assigned"
                End If

            Else
                lblModelError.Text = ""
            End If


            If Arr(0) = "2" Then
                lblModelError.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Cabinet", "Updated", "0", sSession.YearName, 0, "", sSession.IPAddress)
                lblPrmError.Text = "Permissions Successfully Assigned"
            End If
            dtCab = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)

            ' CBLAssignP.Checked = False 'Vijeth
            BindCabinet()
            btnDescSave.Visible = False : btnDescUpdate.Visible = True
            ddlStatus.SelectedIndex = 0
            ddlStatus_SelectedIndexChanged(sender, e)
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
            dtCab = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)
            dt = BindCabinet()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/Cabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Cabinet" + ".xls")
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
            dtCab = objCab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)
            dt = BindCabinet()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/Cabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Cabinet" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgCabinet_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dgCabinet.RowCreated
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lblCBNID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgCabinet.Columns(0).Visible = True
                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    'dgCabinet.Columns(10).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = False
                    'End If
                    'If sDESGSave = "YES" Then
                    dgCabinet.Columns(10).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                    'If sDESGAD = "YES" Then
                    dgCabinet.Columns(9).Visible = True : dgCabinet.Columns(10).Visible = False
                    'End If
                    'If sDESGSave = "YES" Then
                    '    dgCabinet.Columns(10).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    dgCabinet.Columns(0).Visible = False : dgCabinet.Columns(9).Visible = False : dgCabinet.Columns(10).Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCabinet_RowCreated" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadPermission(ByVal sNamespace As String, ByVal iGrpId As Integer, ByVal iUsrId As Integer, Optional ByVal iCabId As Int16 = 0)
        Dim PermDt As DataTable
        Dim i As Int16
        Dim ht As Hashtable, ht1 As Hashtable
        'Dim objDis As New clsCabDisplay
        Dim objColl As System.Collections.IDictionaryEnumerator
        Dim Keys As System.Collections.ICollection
        Dim sLevel As String
        Dim sPermission As String = ""
        Dim sArray() As String

        Try
            'First clear all the items in Checked Items
            iUsrType = objCab.GetUserType(sNamespace, iUsrId)

            If (iUsrId <> 0) Then
                If (iCabId = 0) Then
                    ht = New Hashtable
                    ' PermDt = objCab.GetFinalPermissions(iUsrType, iCabId, iUsrId, sSession.AccessCode, "ALL", 2)
                    ht = objCab.GetFinalPermissions(iUsrType, iCabId, iUsrId, sSession.AccessCode, "ALL", 2)

                Else
                    'PermDt = objClsCab.RetrievePermissions(iCabId, iGrpId, iUsrId)
                    ht = New Hashtable
                    'PermDt = objCab.GetFinalPermissions(iUsrType, iCabId, iUsrId, sSession.AccessCode, "ALL", 2)
                    ht = objCab.GetFinalPermissions(iUsrType, iCabId, iUsrId, sSession.AccessCode, "ALL", 2)
                End If
                If ht Is Nothing Then

                End If
                'Assign the Level of Permission to the Label
                sLevel = ht("Level")
                'Select Case UCase(sLevel)
                '    Case "PG"
                '        lblPLvl.Text = "Group Level Permissions"
                '    Case "GH"
                '        lblPLvl.Text = "Group Level Permissions"
                '    Case "PU"
                '        lblPLvl.Text = "POWER USER"
                '    Case "G"
                '        lblPerm.Text = "Group Level Permissions"
                '    Case "U"
                '        lblPerm.Text = "User Level Permissions"
                '    Case "E"
                '        lblPerm.Text = "Permissions given to EveryOne"
                'End Select

                objColl = ht.GetEnumerator()
                objColl.Reset()

                Keys = ht.Keys
                For i = 0 To Keys.Count - 1
                    objColl.MoveNext()
                    Select Case UCase(objColl.Key.ToString)
                        Case "CCREATE"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "1"
                            Else
                                sPermission = sPermission & "0"
                            End If
                        Case "CMODIFY"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        'Case "CDELETE" 
                        '    If (objColl.Value = 1) Then
                        '        ' chkListCab.SetItemChecked(2, True)
                        '        chkPermission.SelectedIndex = True
                        '    End If
                        Case "CSEARCH"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If

                        'Case "FCREATE"
                        '    If (objColl.Value = 1) Then
                        '        chkListOtr.SetItemChecked(0, True)
                        '    End If
                        Case "CINDEX"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
                        Case "CVIEW"
                            If (objColl.Value = 1) Then
                                sPermission = sPermission & "," & "1"
                            Else
                                sPermission = sPermission & "," & "0"
                            End If
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
                If (iCabId = 0) Then
                    PermDt = objCab.RetrievePermissions(sSession.AccessCode, iCabId, iGrpId, iUsrId)
                Else
                    PermDt = objCab.RetrievePermissions(sSession.AccessCode, iCabId, iGrpId, iUsrId)
                End If

                If PermDt.Rows.Count > 0 Then
                    For i = 0 To PermDt.Rows.Count - 1
                        Select Case PermDt.Rows(i).Item("PerName")
                            Case "CSC"
                                If PermDt.Rows(i)("PerValue").ToString = 1 Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
                            Case "VCB"
                                If PermDt.Rows(i)("PerValue").ToString = 1 Then
                                    sPermission = sPermission & "1"
                                Else
                                    sPermission = sPermission & "0"
                                End If
                            Case "MCB"
                                If PermDt.Rows(i)("Pervalue").ToString = 1 Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
                            Case "IDX"
                                If PermDt.Rows(i)("PerValue").ToString = 1 Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
                            Case "SRH"
                                If PermDt.Rows(i)("PerValue").ToString = 1 Then
                                    sPermission = sPermission & "," & "1"
                                Else
                                    sPermission = sPermission & "," & "0"
                                End If
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
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPermission" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub chkPermission_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPermission.SelectedIndexChanged
        Try
            If (chkPermission.SelectedIndex > 0) Then
                chkPermission.Items(1).Selected = True
            Else
                If (chkPermission.Items(1).Selected = False) Then
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

    Protected Sub btnNewDepartment_Click(sender As Object, e As EventArgs)
        Dim Arr() As String
        Try
            objclsOrgStructure.iOrgnode = 0
            objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(ddlNewDepartment.SelectedValue)
            objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(ddlNewDepartment.SelectedItem.Text)
            objclsOrgStructure.sOrgSalesUnitCode = ""
            objclsOrgStructure.sOrgBranchCode = ""
            objclsOrgStructure.iOrgAppStrength = 0
            objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(ddlNewDepartment.SelectedItem.Text)
            objclsOrgStructure.iOrgParent = 92
            objclsOrgStructure.iOrgLevelCode = 3
            objclsOrgStructure.sOrgDelflag = "A"
            objclsOrgStructure.sOrgStatus = "A"
            objclsOrgStructure.iOrgCreatedBy = sSession.UserID
            objclsOrgStructure.dOrgCreatedOn = Date.Today
            objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
            Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
            BindDepartment()
            ddlNewDepartment.Visible = False
            lblNewDepartment.Visible = False
            btnNewDepartment.Visible = False
            txtDepartment.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewDepartment_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs)
        Dim dtDept As New DataTable
        Try
            dtDept = objCab.CheckCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, txtDepartment.Text)
            If dtDept.Rows.Count > 0 Then
                ddlNewDepartment.DataSource = dtDept
                ddlNewDepartment.DataTextField = "CUST_Name"
                ddlNewDepartment.DataValueField = "CUST_Code"
                ddlNewDepartment.DataBind()
                ddlNewDepartment.Items.Insert(0, "Select New Department")
                ddlNewDepartment.Visible = True
                lblNewDepartment.Visible = True
                btnNewDepartment.Visible = True
            Else
                ddlNewDepartment.Visible = False
                lblNewDepartment.Visible = False
                btnNewDepartment.Visible = False
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Customer Name doesnot exists.','', 'info');", True)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSearch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
