Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports BusinesLayer.clsCabinet
Partial Class SubCabinet
    Inherits System.Web.UI.Page

    Private sFormName As String = "DigitalFilling_SubCabinet"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Dim objSubCab As New clsSubCabinet
    Dim objcab As New clsCabinet
    Private objclsPermission As New clsAccessRights
    Private Shared dtSubCab As DataTable
    'Private Shared sDESGSave As String
    'Private Shared sDESGAD As String
    Private Shared iCBN_NODE As Integer = 0
    Private Shared iCabinetID As Integer = 0
    Private Shared iBackID As Integer = 0
    'Private Shared sDESGReport As String

    Private Shared sDeptID As String
    Dim iUsrType As Integer
    Dim iMemType As Integer
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
                iBackID = 0
                'imgbtnBack.Visible = False : imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False : imgbtnWaiting.Visible = False
                'ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : chkPermission.Enabled = False : ddlPermissionLevel.Enabled = False : btnDescUpdate.Visible = False
                'pnlSubCab.Visible = False
                'btnDescUpdate.Visible = False : btnDescSave.Visible = False

                'sDESGSave = "NO" : sDESGAD = "NO" : sDESGReport = "NO"
                'sFormButtons = objclsPermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "DFSC")
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
                '    End If

                '    If sFormButtons.Contains(",Report") = True Then
                '        sDESGReport = "YES"
                '    End If
                '    If sFormButtons = ",View,SaveOrUpdate,ActiveOrDeactive,Report" Then
                '        sDESGSave = "YES" : sDESGAD = "YES" : sDESGReport = "YES"
                '        imgbtnAdd.Visible = True : btnDescSave.Visible = True
                '    End If
                'End If

                dtSubCab = objcab.LoadSubCabGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, 0)
                'dtSubCab = objcab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)
                'imgbtnAdd.Attributes.Add("OnClick", "$('#myModal').modal('show');return false;")


                RFVSubCabName.ControlToValidate = "txtSubCabName" : RFVSubCabName.ErrorMessage = "Enter Sub-Cabinet Name."
                REVSubCabName.ErrorMessage = "Sub-Cabinet Name exceeded maximum size(max 100 characters)." : REVSubCabName.ValidationExpression = "^[\s\S]{0,100}$"
                REVSubCabNotes.ErrorMessage = "Sub-Cabinet Notes exceeded maximum size(max 255 characters)." : REVSubCabNotes.ValidationExpression = "^[\s\S]{0,255}$"
                RFVSubCabDept.InitialValue = "Select Department" : RFVSubCabDept.ErrorMessage = "Select Department."

                BindStatus() : BindexistingCabinet()
                BindDepartment() : BindPermissionLevel() : BindPermissionDept() : BindPermissionUser() : BindChkBoxList()


                If Request.QueryString("CabinetID") IsNot Nothing Then
                    iCabinetID = objclsEDICTGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CabinetID")))
                    ddlCabinet.SelectedValue = iCabinetID
                    ddlCabinet_SelectedIndexChanged(sender, e)
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
            ddlCabinet.DataSource = objSubCab.LoadCabinet(sSession.AccessCode, sSession.AccessCodeID, sDeptID, "", sSession.UserID)
            ddlCabinet.DataTextField = "CBN_NAME"
            ddlCabinet.DataValueField = "CBN_ID"
            ddlCabinet.DataBind()
            ddlCabinet.Items.Insert(0, "Select Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindexistingCabinet" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Public Sub BindDepartment()
        Try
            ddlDepartment.DataSource = objSubCab.LoadDepartment(sSession.AccessCode)
            ddlDepartment.DataTextField = "Org_Name"
            ddlDepartment.DataValueField = "Org_node"
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "Select Department")
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
            ddlPermissionDep.DataSource = objSubCab.LoadDepartment(sSession.AccessCode)
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
            ddlPermissionUser.DataSource = objSubCab.LoadCabUserPer(sSession.AccessCode, ddlPermissionDep.SelectedValue)
            ddlPermissionUser.DataTextField = "Usr_LoginName"
            ddlPermissionUser.DataValueField = "Usr_Id"
            ddlPermissionUser.DataBind()
            ddlPermissionUser.Items.Insert(0, "Select")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermissionUser" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindChkBoxList()
        Try
            chkPermission.Items.Add("Create Folder")
            chkPermission.Items.Add("Modify Sub Cabinet")
            chkPermission.Items.Add("De-Activate Sub Cabinet")
            chkPermission.Items.Add("Search")
            chkPermission.Items.Add("Index")
            chkPermission.Items.Add("View Sub Cabinet")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChkBoxList" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindSubCabinet() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False
            imgbtnReport.Visible = False
            If ddlCabinet.SelectedIndex > 0 Then
                'If sDESGReport = "YES" Then
                imgbtnReport.Visible = True
                'End If
                dtSubCab = objcab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID, ddlCabinet.SelectedValue)
                'dtSubCab = objcab.LoadSubCabGrid(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCabinet.SelectedValue)
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
                    Dim DVFunctionStatus As New DataView(dtSubCab)
                    DVFunctionStatus.RowFilter = "cbn_DelFlag='" & sStatus & "'"
                    DVFunctionStatus.Sort = "CBN_NAME Asc"
                    dt = DVFunctionStatus.ToTable
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    dt = Nothing
                    dtSubCab = objSubCab.GetAllCab(sSession.AccessCode, sStatus, ddlCabinet.SelectedValue, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtSubCab)
                    DVFunctionStatus.RowFilter = "cbn_DelFlag='" & sStatus & "'"
                    DVFunctionStatus.Sort = "CBN_NAME Asc"
                    dt = DVFunctionStatus.ToTable
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    dt = Nothing
                    dtSubCab = objSubCab.GetAllCab(sSession.AccessCode, sStatus, ddlCabinet.SelectedValue, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtSubCab)
                    DVFunctionStatus.RowFilter = "cbn_DelFlag='" & sStatus & "'"
                    DVFunctionStatus.Sort = "CBN_NAME Asc"
                    dt = DVFunctionStatus.ToTable
                Else
                    dt = Nothing
                    dtSubCab = objSubCab.GetAllCab(sSession.AccessCode, sStatus, ddlCabinet.SelectedValue, sSession.UserID)
                    Dim DVFunctionStatus As New DataView(dtSubCab)
                    DVFunctionStatus.Sort = "CBN_NAME Asc"
                    dt = DVFunctionStatus.ToTable
                End If
                dgSubCabinet.DataSource = dt
                dgSubCabinet.DataBind()
            End If
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSubCabinet" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To dgSubCabinet.Rows.Count - 1
                    chkField = dgSubCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To dgSubCabinet.Rows.Count - 1
                    chkField = dgSubCabinet.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgSubCabinet_PreRender(sender As Object, e As EventArgs) Handles dgSubCabinet.PreRender
        Dim dt As New DataTable
        Try
            If dgSubCabinet.Rows.Count > 0 Then
                dgSubCabinet.UseAccessibleHeader = True
                dgSubCabinet.HeaderRow.TableSection = TableRowSection.TableHeader
                dgSubCabinet.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSubCabinet_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgSubCabinet_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgSubCabinet.RowCommand
        Dim chkSelectAll As New CheckBox
        Dim lblCBN_NODE As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Dim oCabID As New Object, oSubCabID As New Object, oBackID As New Object
        Dim dt As New DataTable()
        Dim DVCabinet As New DataView(dtSubCab)
        Dim ExtraP As Integer
        CBLAssignP.Checked = False 'Vijeth
        Try
            lblError.Text = "" : sMainMaster = ""

            iUsrType = objcab.GetUserType(sSession.AccessCode, sSession.UserID)

            If e.CommandName.Equals("EditRow") Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                'ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblCBN_NODE.Text, "CBP_Modify") 'vijeth
                If (lblCBN_NODE.Text >= 0) Then
                    If (objcab.GetFinalPermissions(iUsrType, lblCBN_NODE.Text, sSession.UserID, sSession.AccessCode, "MCB", 2) <> 0) Then
                        'If ExtraP <> 0 Then
                        btnDescSave.Visible = False : btnDescUpdate.Visible = True : ddlPermissionLevel.Enabled = True
                        pnlSubCab.Visible = True

                        oSubCabID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                        lblSubCabinet.Text = Val(lblCBN_NODE.Text)
                        BindSubCabDetails(Val(lblCBN_NODE.Text))
                        lblModelError.Text = "" : lblPrmError.Text = "" : ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0  'ddlPermissionUser.SelectedIndex = 0
                        For i = 0 To chkPermission.Items.Count - 1
                            chkPermission.Items(i).Selected = False
                        Next
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    Else
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('SubCabinet Permission is not Assigned','', 'info');", True)
                        lblCabinetEmpMasterValidationMsg.Text = "SubCabinet Permission is not Assigned."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    End If 'vijeth
                End If
            End If
            If e.CommandName = "Status" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)
                If ddlStatus.SelectedIndex = 0 Then
                    ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblCBN_NODE.Text, "CBP_Delete") 'vijeth
                    If ExtraP <> 0 Then
                        objSubCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "D", lblCBN_NODE.Text, "D", sSession.UserID)
                        DVCabinet.Sort = "CBN_ID"
                        Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                        DVCabinet(iIndex)("CBN_DELFLAG") = "De-Activated"
                        dtSubCab = DVCabinet.ToTable
                        lblError.Text = "Successfully De-Activated."
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                        lblCabinetEmpMasterValidationMsg.Text = "Successfully De-Activated."
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "De-Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    Else
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('SubCabinet Permission is not Assigned','', 'info');", True)
                        lblCabinetEmpMasterValidationMsg.Text = "SubCabinet Permission is not Assigned."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    End If 'vijeth
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    objSubCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "A", lblCBN_NODE.Text, "A", sSession.UserID)
                    DVCabinet.Sort = "CBN_ID"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("CBN_DELFLAG") = "Activated"
                    dtSubCab = DVCabinet.ToTable
                    lblError.Text = "Successfully Activated."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    ddlStatus.SelectedIndex = 0
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    objSubCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "W", lblCBN_NODE.Text, "A", sSession.UserID)
                    DVCabinet.Sort = "CBN_ID"
                    Dim iIndex As Integer = DVCabinet.Find(lblCBN_NODE.Text)
                    DVCabinet(iIndex)("CBN_DELFLAG") = "Activated"
                    dtSubCab = DVCabinet.ToTable
                    objSubCab.UpdateSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCabinet.SelectedValue)

                    lblError.Text = "Successfully Approved."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Approved", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    ddlStatus.SelectedIndex = 0
                End If
                BindSubCabinet()
            End If
            If e.CommandName = "SelectSubCabinet" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCBN_NODE = DirectCast(clickedRow.FindControl("lblCBN_NODE"), Label)

                oSubCabID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(Val(lblCBN_NODE.Text)))
                'If iCabinetID = 0 Then
                iCabinetID = ddlCabinet.SelectedValue
                'End If

                oCabID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))

                If iBackID = 0 Then
                    oBackID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(0))
                Else
                    oBackID = HttpUtility.UrlEncode(objclsEDICTGeneral.EncryptQueryString(1))
                End If
                Response.Redirect(String.Format("~/DigitalFilling/Folders.aspx?CabinetID={0}&SubCabID={1}&BackID={2}", oCabID, oSubCabID, oBackID), False) 'DigitalFilling/Folder             
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSubCabinet_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgSubCabinet_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgSubCabinet.RowDataBound
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lblCBNID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgSubCabinet.Columns(0).Visible = False : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False
                'If sDESGAD = "YES" Then
                '    dgSubCabinet.Columns(8).Visible = True
                'End If
                'If sDESGSave = "YES" Then
                '    dgSubCabinet.Columns(9).Visible = True
                'End If


                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgSubCabinet.Columns(0).Visible = True : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                    'If sDESGAD = "YES" Then
                    dgSubCabinet.Columns(8).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgSubCabinet.Columns(9).Visible = True
                    'End If

                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgSubCabinet.Columns(0).Visible = True : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                    'If sDESGAD = "YES" Then
                    dgSubCabinet.Columns(8).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgSubCabinet.Columns(9).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgSubCabinet.Columns(0).Visible = True : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                    'If sDESGAD = "YES" Then
                    dgSubCabinet.Columns(8).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgSubCabinet.Columns(9).Visible = False
                    '    End If
                End If
                If ddlStatus.SelectedIndex = 3 Then
                    dgSubCabinet.Columns(0).Visible = False : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSubCabinet_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim sStatus As String = ""
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            imgbtnDeActivate.Visible = False : imgbtnActivate.Visible = False : imgbtnWaiting.Visible = False : imgbtnReport.Visible = False

            dt = BindSubCabinet()
            If ddlCabinet.SelectedIndex > 0 Then
                'If sDESGSave = "YES" Then
                imgbtnAdd.Visible = True
                'End If

                If dt.Rows.Count = 0 Then
                    lblError.Text = "No Data to Display"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                End If
            Else
                imgbtnAdd.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtSubCab As New DataTable
        Dim DVSubCab As New DataView(dtSubCab)
        dtSubCab = Session("dtSubDep")
        Try
            lblError.Text = ""
            If dgSubCabinet.Rows.Count = 0 Then
                lblError.Text = "No data to activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
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

NextSave:   For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgSubCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objSubCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "A", lblCBN_NODE.Text, "A", sSession.UserID)

                    dtSubCab = DVSubCab.ToTable
                    lblError.Text = "Successfully Activated."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Activated','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindSubCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtSubCab As New DataTable
        Dim DVSubCab As New DataView(dtSubCab)
        dtSubCab = Session("dtSubCab")
        Try
            lblError.Text = ""
            If dgSubCabinet.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgSubCabinet.Rows(i).FindControl("lblCBN_NODE")
                Dim ExtraP As Integer
                ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, lblCBN_NODE.Text, "CBP_Delete") 'vijeth
                If ExtraP <> 0 Then
                    If chkSelect.Checked = True Then
                        objSubCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "D", lblCBN_NODE.Text, "D", sSession.UserID)
                        dtSubCab = DVSubCab.ToTable
                        lblError.Text = "Successfully De-Activated."
                        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully De-Activated','', 'success');", True)
                        lblCabinetEmpMasterValidationMsg.Text = "Successfully De-Activated."
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "De-Activated", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                    End If
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Sub Cabinet Permission is not Assigned','', 'info');", True)
                End If 'vijeth
            Next
            BindSubCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblCBN_NODE As New Label
        Dim dtSubCab As New DataTable
        Dim DVSubCab As New DataView(dtSubCab)
        dtSubCab = Session("dtSubCab")
        Try
            lblError.Text = ""
            If dgSubCabinet.Rows.Count = 0 Then
                lblError.Text = "No data to De-Activate"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to De-Activate','', 'info');", True)
                Exit Sub
            End If

            For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
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
NextSave:   For i = 0 To dgSubCabinet.Rows.Count - 1
                chkSelect = dgSubCabinet.Rows(i).FindControl("chkSelect")
                lblCBN_NODE = dgSubCabinet.Rows(i).FindControl("lblCBN_NODE")
                If chkSelect.Checked = True Then
                    objSubCab.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, "W", lblCBN_NODE.Text, "A", sSession.UserID)
                    objSubCab.UpdateSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCabinet.SelectedValue)
                    dtSubCab = DVSubCab.ToTable
                    lblError.Text = "Successfully Approved."
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'success');", True)
                    lblCabinetEmpMasterValidationMsg.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Approved", lblCBN_NODE.Text, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Next
            BindSubCabinet()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim ExtraP As Integer
        Try
            ExtraP = objclsPermission.ExtraPermissionsToCabinet(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCabinet.SelectedValue, "CBP_Create") 'vijeth
            If ExtraP = 1 Then
                lblError.Text = "" : lblModelError.Text = ""
                btnDescSave.Visible = False : btnDescUpdate.Visible = False
                'If sDESGAD = "YES" Then
                'End If
                'If sDESGSave = "YES" Then
                btnDescSave.Visible = True
                'End If
                If ddlCabinet.SelectedIndex > 0 Then
                    lblError.Text = "" : pnlSubCab.Visible = False
                    ddlDepartment.SelectedValue = objSubCab.DepartmentId(sSession.AccessCode, ddlCabinet.SelectedValue)
                    ddlDepartment.Enabled = False
                    txtSubCabName.Text = "" : txtSubCabNotes.Text = ""
                    ddlPermissionDep.SelectedIndex = 0 : ddlPermissionLevel.SelectedIndex = 0
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                Else
                    lblCabinetEmpMasterValidationMsg.Text = "Select Cabinet"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#CabinetMasterValidation').modal('show');", True)
                End If
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Cabinet Permission is not Assigned','', 'info');", True)
            End If 'vijeth
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnDescNew_Click(sender As Object, e As EventArgs) Handles btnDescNew.Click
        Try
            lblError.Text = ""
            btnDescUpdate.Visible = False
            'If sDESGSave = "YES" Then
            btnDescSave.Visible = True
            'End If

            pnlSubCab.Visible = False
            lblModelError.Text = "" : lblPrmError.Text = ""
            ddlDepartment.SelectedValue = objSubCab.DepartmentId(sSession.AccessCode, ddlCabinet.SelectedValue)
            ddlDepartment.Enabled = False
            ddlPermissionLevel.Enabled = False : ddlPermissionDep.Enabled = False : ddlPermissionUser.Enabled = False : lblUser.Visible = True : ddlPermissionUser.Visible = True
            txtSubCabName.Text = "" : txtSubCabNotes.Text = ""
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
    Public Sub BindSubCabDetails(ByVal iSubCab As Integer)
        Dim dt As New DataTable

        dt = objcab.GetCabBindDetails(sSession.AccessCode, iSubCab)
        Try
            If dt.Rows.Count > 0 Then

                If IsDBNull(dt.Rows(0)("CBN_NAME").ToString()) = False Then
                    txtSubCabName.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_NAME").ToString())
                Else
                    txtSubCabName.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("CBN_Note").ToString()) = False Then
                    txtSubCabNotes.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_Note").ToString())
                Else
                    txtSubCabNotes.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("CBN_Department").ToString()) = False Then
                    ddlDepartment.SelectedValue = (dt.Rows(0)("CBN_Department").ToString())
                Else
                    ddlDepartment.SelectedIndex = 0
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSubCabDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")

        End Try

        'Try
        'dt = objSubCab.LoadSubCabGrid(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, iSubCab)

        '    If IsDBNull(dt.Rows(0)("CBN_NAME").ToString()) = False Then
        '        txtSubCabName.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_NAME").ToString())
        '    Else
        '        txtSubCabName.Text = ""
        '    End If

        '    If IsDBNull(dt.Rows(0)("CBN_Note").ToString()) = False Then
        '        txtSubCabNotes.Text = objclsEDICTGeneral.ReplaceSafeSQL(dt.Rows(0)("CBN_Note").ToString())
        '    Else
        '        txtSubCabNotes.Text = ""
        '    End If

        '    If IsDBNull(dt.Rows(0)("Org_node").ToString()) = False Then
        '        ddlDepartment.SelectedValue = (dt.Rows(0)("Org_Node").ToString())
        '    Else
        '        ddlDepartment.SelectedIndex = 0
        '    End If

        'Catch ex As Exception
        '    Throw
        'End Try
    End Sub
    Private Sub ddlCabinet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCabinet.SelectedIndexChanged
        Try
            dgSubCabinet.DataSource = Nothing
            dgSubCabinet.DataBind()
            If ddlCabinet.SelectedIndex > 0 Then
                'If sDESGAD = "YES" Then
                'End If
                'If sDESGSave = "YES" Then
                imgbtnAdd.Visible = True : imgbtnReport.Visible = True
                'End If
                lblCabinetName.Text = ddlCabinet.SelectedItem.Text
                ' ddlDepartment.SelectedValue = objSubCab.DepartmentId(sSession.AccessCode, ddlCabinet.SelectedValue)
                ddlDepartment.Enabled = False
                BindSubCabinet()
            Else
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False
                BindSubCabinet()
            End If

            If dtSubCab.Rows.Count = 0 Then
                lblError.Text = "No Data to Display"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCabinet_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            ChkFC.Checked = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermissionLevel_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionDep_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionDep.SelectedIndexChanged
        Try
            lblModelError.Text = "" : lblPrmError.Text = ""
            For i = 0 To chkPermission.Items.Count - 1
                chkPermission.Items(i).Selected = False
            Next
            BindPermissionUser()
            If ddlPermissionLevel.SelectedIndex = 2 Then
                BindChkBoxPermission()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPermissionDep_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlPermissionUser_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPermissionUser.SelectedIndexChanged
        Try
            BindChkBoxPermission()
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
                iUsrType = objcab.GetUserType(sSession.AccessCode, ddlPermissionUser.SelectedValue)
                iMemType = objcab.GetMemberType(sSession.AccessCode, ddlPermissionUser.SelectedValue)
                If (iUsrType = 1 Or iMemType = 1) Then
                    For i = 0 To chkPermission.Items.Count - 1
                        chkPermission.Items(i).Selected = True
                    Next
                    Exit Sub
                Else
                    dt = objSubCab.LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, lblSubCabinet.Text)
                End If
            ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                dt = objSubCab.LoadPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, lblSubCabinet.Text)
            End If

            If dt.Rows.Count > 0 Then

                If dt.Rows(i)("CBP_Other").ToString = 0 Then  'Vijeth
                    CBLAssignP.Checked = True
                Else
                    CBLAssignP.Checked = False
                End If

                If dt.Rows(i)("CBP_Create").ToString = 1 Then
                    sPermission = sPermission & "1"
                Else
                    sPermission = sPermission & "0"
                End If
                If dt.Rows(i)("CBP_Modify").ToString = 1 Then
                    sPermission = sPermission & "," & "1"
                Else
                    sPermission = sPermission & "," & "0"
                End If
                If dt.Rows(i)("CBP_Delete").ToString = 1 Then
                    sPermission = sPermission & "," & "1"
                Else
                    sPermission = sPermission & "," & "0"
                End If
                'If dt.Rows(i)("CBP_CreateFolder").ToString = 1 Then
                '    sPermission = sPermission & "," & "1"
                'Else
                '    sPermission = sPermission & "," & "0"
                'End If
                If dt.Rows(i)("CBP_Search").ToString = 1 Then
                    sPermission = sPermission & "," & "1"
                Else
                    sPermission = sPermission & "," & "0"
                End If
                If dt.Rows(i)("CBP_Index").ToString = 1 Then
                    sPermission = sPermission & "," & "1"
                Else
                    sPermission = sPermission & "," & "0"
                End If
                If dt.Rows(i)("CBP_View").ToString = 1 Then
                    sPermission = sPermission & "," & "1"
                Else
                    sPermission = sPermission & "," & "0"
                End If
                sArray = sPermission.Split(",")
                For i = 0 To chkPermission.Items.Count - 1
                    If sArray(i) = 1 Then
                        chkPermission.Items(i).Selected = True
                    Else
                        chkPermission.Items(i).Selected = False
                    End If
                Next
                If dt.Rows(0).Item("CBP_other") = 0 Then
                    CBLAssignP.Checked = True
                Else
                    CBLAssignP.Checked = False
                End If
            Else
                lblPrmError.Text = "No Permissions Assigned"
                For i = 0 To chkPermission.Items.Count - 1
                    chkPermission.Items(i).Selected = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindChkBoxPermission" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnDescSave_Click(sender As Object, e As EventArgs) Handles btnDescSave.Click
        Dim Arr() As String
        Dim iRet As Integer
        Try
            lblModelError.Text = ""
            iRet = objSubCab.CheckSubCabName(sSession.AccessCode, objclsEDICTGeneral.SafeSQL(txtSubCabName.Text), 0, ddlCabinet.SelectedValue)
            If iRet <> 0 Then
                lblModelError.Text = "Sub Cabinet Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtSubCabName.Focus()
                Exit Sub
            End If

            objSubCab.iCBN_ID = 0
            objSubCab.sCBN_Name = objclsEDICTGeneral.SafeSQL(txtSubCabName.Text)
            objSubCab.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtSubCabNotes.Text)
            objSubCab.iCBN_Parent = ddlCabinet.SelectedValue
            objSubCab.iCBN_UserID = sSession.UserID
            objSubCab.iCBN_Department = ddlDepartment.SelectedValue
            objSubCab.iCBN_SubCabCount = 0
            objSubCab.iCBN_FolderCount = 0
            objSubCab.iCBN_CreatedBy = sSession.UserID
            objSubCab.sCBN_Status = "C"
            objSubCab.sCBN_DelFlag = "W"
            Arr = objSubCab.SaveSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, objSubCab)
            objSubCab.UpdateSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDepartment.SelectedValue, ddlCabinet.SelectedValue)

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Updated", ddlCabinet.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)

            ElseIf Arr(0) = "3" Then
                objSubCab.iCBP_ID = 0
                objSubCab.iCBP_Cabinet = objcab.GetCabID(sSession.AccessCode)
                objSubCab.iCBP_Department = ddlDepartment.SelectedValue
                Try
                    objSubCab.SaveDefaultPermission(sSession.AccessCode, objSubCab) 'Vijeth 29/01/19[Defalut Group Permission]
                Catch ex As Exception
                End Try

                lblError.Text = "Successfully Saved & Waiting for Approval."
                ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Saved & Waiting for Approval','', 'success');", True)
                lblCabinetEmpMasterValidationMsg.Text = "Successfully Saved & Waiting for Approval."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Saved", ddlCabinet.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#CabinetMasterValidation').modal('show');", True)
                ddlStatus.SelectedIndex = 2
                ddlStatus_SelectedIndexChanged(sender, e)
            End If
            pnlSubCab.Visible = True
            ddlStatus.SelectedIndex = 2
            BindSubCabinet()
            ddlPermissionLevel.Enabled = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnDescSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Dim sCabSub As Char = "S"
    Dim iCabId As Integer
    Dim iSubCabId As Integer
    Protected Sub btnDescUpdate_Click(sender As Object, e As EventArgs) Handles btnDescUpdate.Click
        Dim Arr() As String, Arry() As String
        Dim sArray As Array
        Dim sPermission As String = ""
        Dim iRet As Integer
        Dim objSrtPer As New SrtCabPer
        Dim icount As Integer
        Try
            lblModelError.Text = ""

            objSubCab.iCBN_ID = lblSubCabinet.Text
            objSubCab.sCBN_Name = objclsEDICTGeneral.SafeSQL(txtSubCabName.Text)
            objSubCab.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtSubCabNotes.Text)

            iRet = objSubCab.CheckSubCabName(sSession.AccessCode, objclsEDICTGeneral.SafeSQL(txtSubCabName.Text), lblSubCabinet.Text, ddlCabinet.SelectedValue)
            If iRet <> 0 Then
                lblModelError.Text = "Sub Cabinet Name already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                txtSubCabName.Focus()
                Exit Sub
            End If
            objSubCab.iCBN_Parent = ddlCabinet.SelectedValue
            objSubCab.iCBN_UserID = sSession.UserID
            objSubCab.iCBN_Department = ddlDepartment.SelectedValue
            objSubCab.iCBN_SubCabCount = 0
            objSubCab.iCBN_FolderCount = 0
            objSubCab.iCBN_CreatedBy = sSession.UserID
            objSubCab.iCBN_UpdatedBy = sSession.UserID
            objSubCab.sCBN_Status = "U"
            objSubCab.sCBN_DelFlag = "A"
            Arr = objSubCab.SaveSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, objSubCab)
            objSubCab.UpdateSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDepartment.SelectedValue, ddlCabinet.SelectedValue)

            If ddlPermissionLevel.SelectedIndex > 0 Then
                If ddlPermissionDep.SelectedIndex = 0 Then
                    lblModelError.Text = "Select Department."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                    ddlPermissionDep.Focus()
                    Exit Sub
                End If
                objSubCab.iCBP_ID = 0
                objSubCab.iCBP_Cabinet = lblSubCabinet.Text
                objSubCab.iCBP_Department = ddlPermissionDep.SelectedValue

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

                    objSubCab.sCBP_PermissionType = "U"
                    objSubCab.iCBP_User = ddlPermissionUser.SelectedValue
                    If CBLAssignP.Checked = True Then    'Vijeth
                        objSubCab.iCBP_Others = 0
                        objSrtPer.iOther = 0
                    Else
                        objSubCab.iCBP_Others = 1
                        objSrtPer.iOther = 1
                    End If

                    '  objSubCab.DeleteCabPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, ddlPermissionUser.SelectedValue, lblSubCabinet.Text)'Vijeth
                ElseIf ddlPermissionLevel.SelectedIndex = 2 Then
                    objSubCab.sCBP_PermissionType = "G"
                    objSubCab.iCBP_User = 0
                    'If CBLAssignP.Checked = True Then    'Vijeth
                    objSubCab.iCBP_Others = 0
                    objSrtPer.iOther = 0
                    'Else
                    'objSubCab.iCBP_Others = 1
                    'End If
                    '  objSubCab.DeleteCabPermission(sSession.AccessCode, ddlPermissionDep.SelectedValue, 0, lblSubCabinet.Text)'Vijeth
                End If

                sArray = sPermission.Split(",")
                'icount = objSubCab.GetCabinetInfo(sSession.AccessCode, lblSubCabinet.Text, ddlCabinet.SelectedValue)
                ' If (icount > 0) Then
                Arry = objSubCab.SavePermission(sSession.AccessCode, objSubCab, sArray)
                lblPrmError.Text = "Permissions Successfully Assigned"
                'Else
                'lblPrmError.Text = "First Assign Permission to the Parent Level"
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
                'Exit Sub
                'End If
            End If

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
                        objSrtPer.iCabId = lblSubCabinet.Text
                    Else
                        objSrtPer.iCabId = iCabId
                    End If
                ElseIf (sCabSub = "S") Then
                    If (iSubCabId = 0) Then
                        objSrtPer.iCabId = lblSubCabinet.Text
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
                        objSrtPer.iModCab = 1
                    Else
                        chkPermission.Items(1).Selected = False
                    End If
                    If sArray(3) = 1 Then
                        chkPermission.Items(2).Selected = True
                        objSrtPer.iDelCab = 1
                    Else
                        chkPermission.Items(2).Selected = False
                    End If
                    If sArray(4) = 1 Then
                        chkPermission.Items(3).Selected = True
                        objSrtPer.iSearch = 1
                    Else
                        chkPermission.Items(3).Selected = False
                    End If
                    If sArray(5) = 1 Then
                        chkPermission.Items(4).Selected = True
                        objSrtPer.iIndex = 1
                    Else
                        chkPermission.Items(4).Selected = False
                    End If
                    If sArray(6) = 1 Then
                        chkPermission.Items(5).Selected = True
                        objSrtPer.iView = 1
                    Else
                        chkPermission.Items(5).Selected = False
                    End If
                Next
            End If

            If (ChkFC.Checked = True) Then
                objcab.ExtendPermissions(objSrtPer, sCabSub, sSession.AccessCode, objSrtPer.cLvlType)
                ' ChkSC.Checked = False
            End If
            'If (ChkSC.Checked = True) Then
            '    objcab.ExtendPermissions(objSrtPer, sCabSub, sSession.AccessCode, objcab.sCBP_PermissionType, "S")
            'End If '/// till here

            If Arr(0) = "2" Then
                lblModelError.Text = "Successfully Updated."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Digital Filling", "Sub Cabinet", "Updated", ddlCabinet.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            End If
            ddlStatus.SelectedIndex = 0
            BindSubCabinet()
            ' CBLAssignP.Checked = False 'Vijeth
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
            dtSubCab = objSubCab.LoadSubCabGrid(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, 0)
            dt = BindSubCabinet()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/SubCabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=SubCabinet" + ".xls")
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
            dtSubCab = objSubCab.LoadSubCabGrid(sSession.AccessCode, sSession.AccessCodeID, ddlCabinet.SelectedValue, 0)
            dt = BindSubCabinet()
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/DigitalFilling/SubCabinet.rdlc")
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=SubCabinet" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/DigitalFilling/Cabinet.aspx"), False) 'Cabinet     
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub dgSubCabinet_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles dgSubCabinet.RowCreated
        Dim imgbtnEdit As New ImageButton, imgbtnStatus As New ImageButton
        Dim lblCBNID As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                dgSubCabinet.Columns(0).Visible = False : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    dgSubCabinet.Columns(0).Visible = True : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                    'If sDESGAD = "YES" Then
                    dgSubCabinet.Columns(8).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgSubCabinet.Columns(9).Visible = True
                    'End If

                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    dgSubCabinet.Columns(0).Visible = True : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                    'If sDESGAD = "YES" Then
                    dgSubCabinet.Columns(8).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgSubCabinet.Columns(9).Visible = False
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    dgSubCabinet.Columns(0).Visible = True : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False

                    'If sDESGAD = "YES" Then
                    dgSubCabinet.Columns(8).Visible = True
                    'End If
                    'If sDESGSave = "YES" Then
                    dgSubCabinet.Columns(9).Visible = False
                    'End If
                End If
                If ddlStatus.SelectedIndex = 3 Then
                    dgSubCabinet.Columns(0).Visible = False : dgSubCabinet.Columns(8).Visible = False : dgSubCabinet.Columns(9).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgSubCabinet_RowCreated" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub


    Public Sub subcabview()
        'Try
        '    sPerm = objGenBsLayer.GetUsrOrGrpPermission(sSession.UserID, "SCAB", 0) 'View
        '    If Val(sPerm) = 1 Then
        '        If (ddlCabinet.SelectedValue <> 0) Then
        '            LoadSubCabinets(ddlCabinet.SelectedValue)
        '            ddlCabinet.SelectedIndex = BSGeneral.GetComboBoxIndex(ddlCabinet, ddlCabinet.SelectedValue)
        '        Else
        '            LoadSubCabinets()
        '        End If
        '        ' lblSp.Text = "Sub Cabinet Details"
        '    Else
        '        'MessageBox.Show("Current User does not have permission to Cabinet", AppName, MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Dim dtCab As DataTable
    Private Sub LoadSubCabinets(Optional ByVal iCabId As Integer = 0)
        'Dim RefDt As DataTable
        'Try
        '    ' lblCabSub.Text = "S"
        '    BindexistingCabinet()
        '    If (iCabId <> 0) Then
        '        dtCab = objcab.LoadCabinetGrid(sSession.AccessCode, sSession.AccessCodeID, 0, sDeptID, sSession.UserID)
        '        dtCab = GeneralInfo.SetRows(dtCab)
        '        dtCab = ConDateFormat(dtCab, "CabCrOn")
        '    Else
        '        dtCab = objCls.BuildPermTable()
        '        dtCab = AddBlankRows(dtCab)
        '    End If

        '    'Alter Controls
        '    grdCab.Columns("SubCabNo").Visible = False
        '    ConMenuStrip.Items(1).Visible = False
        '    grdCab.Columns("CabName").HeaderText = "Sub Cabinet Name"

        '    grdCab.DataSource = dtCab
        '    'grdCab.Rows(0).Selected = True
        '    grdCab.AllowUserToResizeColumns = True
        '    grdCab.AutoResizeColumn(0)
        '    grdCab.Columns(0).Width = 190
        '    grdCab.AutoResizeColumn(1)
        '    grdCab.Columns(1).Width = 190
        '    grdCab.AutoResizeColumn(2)
        '    grdCab.Columns(2).Width = 190
        '    grdCab.AutoResizeColumn(4)
        '    grdCab.Columns(4).Width = 500
        'Catch ex As Exception
        '    Throw
        'End Try
    End Sub
    Private Sub chkPermission_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPermission.SelectedIndexChanged
        Try
            If (chkPermission.SelectedIndex > 0) Then
                chkPermission.Items(5).Selected = True
            Else
                If (chkPermission.Items(5).Selected = False) Then
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
