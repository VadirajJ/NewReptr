Imports System
Imports System.Data
Imports BusinesLayer
Partial Class UserMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_UserMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsEProfile As New clsEProfile

    Private Shared sSession As AllSession
    'Private Shared sUMDSave As String
    Private Shared sUMDFlag As String
    Private Shared sUMDBackStatus As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        ibSearch.ImageUrl = "~/Images/Search16.png"
        imgbtnAdd.ImageUrl = "~/Images/AddUser24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iUserID As Integer = 0, iStatusID As Integer = 0
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                'sUMDSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPUSR", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '        sUMDSave = "YES"
                '    End If
                'End If

                imgbtnUpdate.Visible = False
                BindCompanyDB() : BindDesignationDB()
                BindRoleDB() : BindModule() : BindPermission()
                BindExistingEmployeeDB(0, 0, 0, 0, "")
                ClientSideValidationUserMasterDetails()
                If Request.QueryString("UserID") IsNot Nothing Then
                    iUserID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("UserID")))
                    If iUserID > 0 Then
                        Dim liEmpID As ListItem = ddlExistingUserName.Items.FindByValue(iUserID)
                        If IsNothing(liEmpID) = False Then
                            ddlExistingUserName.SelectedValue = iUserID
                            ddlExistingUserName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sUMDBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub ClientSideValidationUserMasterDetails()
        Try
            RFVCompanyName.ErrorMessage = "Select Customer." : RFVCompanyName.InitialValue = "Select Customer"
            RFVSAPCode.ErrorMessage = "Enter EMP Code." : REVSAPCode.ErrorMessage = "Enter valid EMP Code." : REVSAPCode.ValidationExpression = "[a-zA-Z0-9'@&amp;amp;#.\s]{0,10}$"
            RFVEmpName.ErrorMessage = "Enter User Name." : REVEmpName.ErrorMessage = "Enter valid User Name." : REVEmpName.ValidationExpression = "^(.{0,50})$"
            RFVLoginName.ErrorMessage = "Enter Login Name." : REVLoginName.ErrorMessage = "Enter valid Login Name." : REVLoginName.ValidationExpression = "^[a-zA-Z0-9'@&amp;amp;#.\s]{0,25}$"
            RFVPasssword.ErrorMessage = "Enter Password." : RFVConfirmPassword.ErrorMessage = "Enter Confirm Password." : CVPassword.ErrorMessage = "Passwords does not match."
            RFVEmail.ErrorMessage = "Enter E-Mail." : REVEMail.ErrorMessage = "Enter valid E-Mail." : REVEMail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" '"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            REVOffice.ErrorMessage = "Enter valid Office Phone No." : REVOffice.ValidationExpression = "^[0-9]{0,15}$"
            REVMobile.ErrorMessage = "Enter valid 10 digit Mobile No." : REVMobile.ValidationExpression = "^[0-9]{10}$"
            REVResidence.ErrorMessage = "Enter valid Residence Phone No." : REVResidence.ValidationExpression = "^[0-9]{0,15}$"
            RFVDesignation.ErrorMessage = "Select Designation." : RFVDesignation.InitialValue = "Select Designation"
            RFVRole.ErrorMessage = "Select Role." : RFVRole.InitialValue = "Select Role"
            RFVModule.ErrorMessage = "Select Module." : RFVModule.InitialValue = "Select Module"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClientSideValidationUserMasterDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindCompanyDB()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyName.DataSource = dt
            ddlCompanyName.DataTextField = "Cust_Name"
            ddlCompanyName.DataValueField = "Cust_Id"
            ddlCompanyName.DataBind()
            ddlCompanyName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCompanyDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindRoleDB()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveRole(sSession.AccessCode, sSession.AccessCodeID)
            ddlRole.DataSource = dt
            ddlRole.DataTextField = "Mas_Description"
            ddlRole.DataValueField = "Mas_ID"
            ddlRole.DataBind()
            ddlRole.Items.Insert(0, "Select Role")
            ddlRole.SelectedValue = 101
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindRoleDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindDesignationDB()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveDesignation(sSession.AccessCode, sSession.AccessCodeID)
            ddlDesignation.DataSource = dt
            ddlDesignation.DataTextField = "Mas_Description"
            ddlDesignation.DataValueField = "Mas_ID"
            ddlDesignation.DataBind()
            ' ddlDesignation.Items.Insert(0, "Select Designation")
            ddlDesignation.SelectedValue = 101
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDesignationDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    'Public Sub BindModule()
    '    Try
    '        ddlGroup.Items.Insert(0, "Select Module")
    '        ddlGroup.Items.Insert(1, "Master")
    '        ddlGroup.Items.Insert(2, "Audit")
    '        ddlGroup.SelectedIndex = 0
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindModule" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
    '    End Try
    'End Sub

    Public Sub BindModule()
        Try
            ddlGroup.Items.Insert(0, "Select Module")
            ddlGroup.Items.Insert(1, "Assignments")
            ddlGroup.Items.Insert(2, "Finalisation of Account")
            ddlGroup.Items.Insert(3, "Fixed Assets")
            ddlGroup.Items.Insert(4, "Standard Audit")
            ddlGroup.Items.Insert(5, "Digital Office")
            ddlGroup.Items.Insert(6, "Master")
            ddlGroup.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindModule" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Public Sub BindPermission()
        Try
            ddlPermission.Items.Insert(0, "Role based")
            ddlPermission.Items.Insert(1, "User based")
            ddlPermission.SelectedIndex = 0
            ddlPermission.SelectedValue = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermission" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub BindExistingEmployeeDB(ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, iBranchID As Integer, ByVal sSearch As String)
        Try
            ddlExistingUserName.DataSource = objclsEmployeeMaster.LoadExistingUsers(sSession.AccessCode, sSession.AccessCodeID, iZoneID, iRegionID, iAreaID, iBranchID, sSearch)
            ddlExistingUserName.DataTextField = "FullName"
            ddlExistingUserName.DataValueField = "Usr_ID"
            ddlExistingUserName.DataBind()
            ddlExistingUserName.Items.Insert(0, "Select Existing User")
            txtSAPCode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingEmployeeDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            lblError.Text = "" : sUMDFlag = "" : txtSearch.Text = ""
            txtSAPCode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID) : txtUserName.Text = "" : txtLoginName.Text = ""
            ddlDesignation.SelectedValue = 101 : ddlCompanyName.SelectedIndex = 0 : ddlRole.SelectedValue = 101 : ddlGroup.SelectedIndex = 0 : ddlPermission.SelectedIndex = 0
            txtEmail.Text = "" : txtOffice.Text = "" : txtMobile.Text = "" : txtResidence.Text = ""
            txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
            txtPassword.Text = "" : txtConfirmPassword.Text = ""
            txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            'If sUMDSave = "YES" Then
            imgbtnSave.Visible = True
            'End If
            chkSendMail.Checked = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub ddlExistingUserName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingUserName.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            ClearAll()
            If ddlExistingUserName.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadExistingEmployeeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingUserName.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtSAPCode.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Code")) = False Then
                        txtSAPCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_Code").ToString())
                    End If

                    txtUserName.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_FullName")) = False Then
                        txtUserName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_FullName").ToString())
                    End If

                    ddlDesignation.SelectedValue = 101
                    If IsDBNull(dt.Rows(0).Item("Usr_Designation")) = False Then
                        Dim liDesignationID As ListItem = ddlDesignation.Items.FindByValue(Val(dt.Rows(0).Item("Usr_Designation")))
                        If IsNothing(liDesignationID) = False Then
                            ddlDesignation.SelectedValue = Val(dt.Rows(0).Item("Usr_Designation"))
                        End If
                    End If

                    txtOffice.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_OfficePhone")) = False Then
                        txtOffice.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_OfficePhone").ToString())
                    End If

                    txtMobile.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_MobileNo").ToString()) = False Then
                        txtMobile.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_MobileNo").ToString())
                    End If

                    txtResidence.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_PhoneNo")) = False Then
                        txtResidence.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_PhoneNo").ToString())
                    End If

                    txtEmail.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Email")) = False Then
                        txtEmail.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_Email").ToString())
                    End If

                    chkSendMail.Checked = False
                    If IsDBNull(dt.Rows(0).Item("Usr_Category")) = False Then
                        If dt.Rows(0).Item("Usr_Category") = 1 Then
                            chkSendMail.Checked = True
                        End If
                    End If

                    ddlCompanyName.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_CompanyID")) = False Then
                        Dim liCompanyID As ListItem = ddlCompanyName.Items.FindByValue(Val(dt.Rows(0).Item("Usr_CompanyID")))
                        If IsNothing(liCompanyID) = False Then
                            ddlCompanyName.SelectedValue = Val(dt.Rows(0).Item("Usr_CompanyID"))
                        End If
                    End If

                    ddlRole.SelectedValue = 101
                    If IsDBNull(dt.Rows(0).Item("Usr_Role")) = False Then
                        Dim liRoleID As ListItem = ddlRole.Items.FindByValue(Val(dt.Rows(0).Item("Usr_Role")))
                        If IsNothing(liRoleID) = False Then
                            ddlRole.SelectedValue = Val(dt.Rows(0).Item("Usr_Role"))
                        End If
                    End If

                    ddlGroup.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_LevelGrp")) = False Then
                        If ddlGroup.Items.Count >= dt.Rows(0).Item("Usr_LevelGrp") Then
                            ddlGroup.SelectedIndex = Val(dt.Rows(0).Item("Usr_LevelGrp"))
                        End If
                    End If

                    txtLoginName.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_LoginName")) = False Then
                        txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_LoginName").ToString())
                    End If

                    txtPassword.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Password")) = False Then
                        txtPassword.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(dt.Rows(0).Item("Usr_Password").ToString()))
                        txtPassword.TextMode = TextBoxMode.SingleLine : txtPassword.TextMode = TextBoxMode.Password
                    End If

                    txtConfirmPassword.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Password")) = False Then
                        txtConfirmPassword.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(dt.Rows(0).Item("Usr_Password").ToString()))
                        txtConfirmPassword.TextMode = TextBoxMode.SingleLine : txtConfirmPassword.TextMode = TextBoxMode.Password
                    End If

                    ddlPermission.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_GrpOrUserLvlPerm")) = False Then
                        If ddlPermission.Items.Count >= dt.Rows(0).Item("Usr_GrpOrUserLvlPerm") Then
                            ddlPermission.SelectedIndex = Val(dt.Rows(0).Item("Usr_GrpOrUserLvlPerm"))
                        End If
                    End If

                    If IsDBNull(dt.Rows(0).Item("Usr_Delflag")) = False Then
                        sUMDFlag = dt.Rows(0).Item("Usr_Delflag")
                        If dt.Rows(0).Item("Usr_Delflag").ToString() = "W" Then
                            lblError.Text = "Waiting for Approval"
                            'If sUMDSave = "YES" Then
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                            'Else
                            '    imgbtnUpdate.Visible = False
                            'End If
                        ElseIf dt.Rows(0).Item("Usr_Delflag").ToString() = "D" Then
                            lblError.Text = "De-Activated"
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                        Else
                            'If sUMDSave = "YES" Then
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                            'Else
                            '    imgbtnUpdate.Visible = False
                            'End If
                        End If
                    End If
                End If
            Else
                BindExistingEmployeeDB(0, 0, 0, 0, "")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingUserName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : sUMDFlag = "" : txtSearch.Text = ""
            ddlExistingUserName.SelectedIndex = 0 : txtSAPCode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID) : txtUserName.Text = "" : txtLoginName.Text = ""
            ddlDesignation.SelectedValue = 101 : ddlCompanyName.SelectedIndex = 0 : ddlRole.SelectedValue = 101 : ddlGroup.SelectedIndex = 0 : ddlPermission.SelectedIndex = 0
            txtEmail.Text = "" : txtOffice.Text = "" : txtMobile.Text = "" : txtResidence.Text = ""
            txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
            txtPassword.Text = "" : txtConfirmPassword.Text = ""
            txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            chkSendMail.Checked = False
            'If sUMDSave = "YES" Then
            imgbtnSave.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub ibSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibSearch.Click
        Try
            lblError.Text = ""
            BindExistingEmployeeDB(0, 0, 0, 0, objclsGRACeGeneral.SafeSQL(txtSearch.Text.Trim))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibSearch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(sUMDBackStatus)))
            Response.Redirect(String.Format("~/Masters/UserMaster.aspx?StatusID={0}", oStatus), False) 'Masters/UserMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sChangedPwd As String, iIsPasswordReset As Integer = 0
        Try
            lblError.Text = ""
            If ddlExistingUserName.SelectedIndex = 0 Then
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSAPCode.Text)) = True Then
                    lblError.Text = "EMP Code already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('EMP Code already exist','', 'warning');", True)
                    txtSAPCode.Focus()
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, UCase(txtLoginName.Text)) = True Then
                    lblError.Text = "Login Name already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Login Name already exist','', 'error');", True)
                    txtLoginName.Focus()
                    Exit Sub
                End If
            Else
                sChangedPwd = objclsGRACeGeneral.DecryptPassword(objclsEmployeeMaster.GetUesrPassword(sSession.AccessCode, sSession.AccessCodeID, ddlExistingUserName.SelectedValue))
                If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then '
                    lblError.Text = "Entered Password and Confirm Password does not match."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Entered Password and Confirm Password does not match','', 'error');", True)
                    txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
                    txtPassword.Text = "" : txtConfirmPassword.Text = ""
                    txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
                    Exit Sub
                ElseIf ((Trim(txtPassword.Text) <> sChangedPwd) And Trim(txtConfirmPassword.Text) <> sChangedPwd) Then
                    iIsPasswordReset = 1
                End If
            End If
            If objclsEmployeeMaster.GetTRACeCustomerUserCount(sSession.AccessCode, sSession.AccessCodeID) >= sSession.NumberOfUsers Then
                lblError.Text = "User/Employee limit exceeded in TRACe application. Please contact Administrator."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('User/Employee limit exceeded in TRACe application. Please contact Administrator.','', 'warning');", True)
                Exit Sub
            End If
            If txtSAPCode.Text.Trim = "" Then
                lblError.Text = "Enter EMP Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter EMP Code','', 'warning');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If
            If txtSAPCode.Text.Trim.Length > 10 Then
                lblError.Text = "EMP Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('EMP Code exceeded maximum size(max 10 characters)','', 'error');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If

            If txtUserName.Text.Trim = "" Then
                lblError.Text = "Enter User Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter User Name','', 'warning');", True)
                txtUserName.Focus()
                Exit Sub
            End If
            If txtUserName.Text.Trim.Length > 50 Then
                lblError.Text = "User Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('User Name exceeded maximum size(max 50 characters)','', 'error');", True)
                txtUserName.Focus()
                Exit Sub
            End If

            If txtLoginName.Text.Trim = "" Then
                lblError.Text = "Enter Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Login Name','', 'warning');", True)
                txtLoginName.Focus()
                Exit Sub
            End If
            If txtLoginName.Text.Trim.Length > 25 Then
                lblError.Text = "Login Name exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Login Name exceeded maximum size(max 25 characters)','', 'error');", True)
                txtLoginName.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                lblError.Text = "Enter Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Password','', 'warning');", True)
                txtPassword.Focus()
                Exit Sub
            End If

            If txtConfirmPassword.Text.Trim = "" Then
                lblError.Text = "Enter Confirm Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Confirm Password','', 'warning');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                lblError.Text = " Entered Password & Confirm Password doesn't match."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Entered Password & Confirm Password doesn't match','', 'error');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Module','', 'warning');", True)
                ddlGroup.Focus()
                Exit Sub
            End If

            If txtEmail.Text.Trim.Length > 50 Then
                lblError.Text = "E-Mail exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('E-Mail exceeded maximum size(max 50 characters)','', 'error');", True)
                txtEmail.Focus()
                Exit Sub
            End If

            If txtOffice.Text.Trim.Length > 15 Then
                lblError.Text = "Office Phone No. exceeded maximum size(max 20 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Office Phone No. exceeded maximum size(max 20 numbers))','', 'error');", True)
                txtOffice.Focus()
                Exit Sub
            End If

            If txtMobile.Text.Trim <> "" Then
                If txtMobile.Text.Trim.Length > 10 Then
                    lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Mobile No. exceeded maximum size(max 10 numbers)','', 'error');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If

                If txtMobile.Text.Trim.Length <> 10 Then
                    lblError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid 10 digits Mobile No','', 'error');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If
            End If

            If txtResidence.Text.Trim.Length > 15 Then
                lblError.Text = "Residence No. exceeded maximum size(max 15 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Residence No. exceeded maximum size(max 15 numbers)','', 'error');", True)
                txtResidence.Focus()
                Exit Sub
            End If

            If ddlDesignation.SelectedIndex = 0 Then
                lblError.Text = "Select Designation." : lblError.Text = "Select Designation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Designation','', 'warning');", True)
                ddlDesignation.Focus()
                Exit Sub
            End If

            If ddlRole.SelectedIndex = 0 Then
                lblError.Text = "Select Role." : lblError.Text = "Select Role."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Role','', 'warning');", True)
                ddlRole.Focus()
                Exit Sub
            End If

            If iIsPasswordReset = 1 Then
                objclsEmployeeMaster.UpdatePasswordReset(sSession.AccessCode, sSession.AccessCodeID, ddlExistingUserName.SelectedValue, 1)
            End If
            Arr = SaveEmployeeDetails()
            SaveUserInOtherDep(Arr(1))
            BindExistingEmployeeDB(0, 0, 0, 0, "")
            ddlExistingUserName.SelectedValue = Arr(1)
            ddlExistingUserName_SelectedIndexChanged(sender, e)
            If Arr(0) = "3" Then
                lblError.Text = "Successfully Saved & Waiting for Approval."
                lblCustomerValidationMsg.Text = "Successfully Saved & Waiting for Approval."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Saved", Arr(1), txtUserName.Text.Trim, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                sUMDBackStatus = 4
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Private Function SaveUserInOtherDep(ByVal iUserID As Integer) As Array
        Dim Arr() As String
        Try
            objclsEmployeeMaster.iSUO_UserID = iUserID
            objclsEmployeeMaster.iSUO_DeptId = objclsEmployeeMaster.GetDepartmentID(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyName.SelectedValue, ddlCompanyName.SelectedItem.Text)
            objclsEmployeeMaster.iSUO_IsDeptHead = 0
            objclsEmployeeMaster.iSUO_CreatedBy = sSession.UserID
            objclsEmployeeMaster.sSUO_IPAddress = sSession.IPAddress
            objclsEmployeeMaster.iSUO_CompID = sSession.AccessCodeID
            Arr = objclsEmployeeMaster.SaveUserInOtherDep(sSession.AccessCode, objclsEmployeeMaster)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Private Function SaveEmployeeDetails() As Array
        Dim Arr() As String
        Try
            If ddlExistingUserName.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUserID = ddlExistingUserName.SelectedValue
                objclsEmployeeMaster.sUsrStatus = "U"
            Else
                objclsEmployeeMaster.iUserID = 0
                objclsEmployeeMaster.sUsrStatus = "C"
            End If
            objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(txtSAPCode.Text.Trim)
            objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(txtUserName.Text.Trim)
            objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(txtLoginName.Text.Trim)
            objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword(txtPassword.Text)
            objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(txtEmail.Text.Trim)
            objclsEmployeeMaster.iUsrLevelGrp = ddlGroup.SelectedIndex
            objclsEmployeeMaster.sUsrDutyStatus = "W"
            objclsEmployeeMaster.sUsrPhoneNo = objclsGRACeGeneral.SafeSQL(txtResidence.Text.Trim)
            objclsEmployeeMaster.sUsrMobileNo = objclsGRACeGeneral.SafeSQL(txtMobile.Text.Trim)
            objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(txtOffice.Text.Trim)
            objclsEmployeeMaster.sUsrOffPhExtn = ""
            objclsEmployeeMaster.iUsrDesignation = ddlDesignation.SelectedValue
            objclsEmployeeMaster.iUsrCompanyID = ddlCompanyName.SelectedValue
            objclsEmployeeMaster.iUsrRole = ddlRole.SelectedValue
            objclsEmployeeMaster.sUsrFlag = "W"
            objclsEmployeeMaster.sUsrType = "C"
            objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
            If chkSendMail.Checked = True Then
                objclsEmployeeMaster.iUsrSentMail = 1
            Else
                objclsEmployeeMaster.iUsrSentMail = 0
            End If
            objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = ddlPermission.SelectedIndex
            objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
            objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
            objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
            objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
            objclsEmployeeMaster.iUsrBCMmodule = 0
            objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
            objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
            objclsEmployeeMaster.iUsrBCMRole = 0


            If ddlGroup.SelectedIndex = 6 Then 'Masters
                objclsEmployeeMaster.iUsrMasterModule = 1
                objclsEmployeeMaster.iUsrMasterRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 4 Then 'Standard Audit
                objclsEmployeeMaster.iUsrAuditModule = 1
                objclsEmployeeMaster.iUsrAuditRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 1 Then 'Assignments
                objclsEmployeeMaster.iUsrRiskModule = 1
                objclsEmployeeMaster.iUsrRiskRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 2 Then 'Finalisation of Account
                objclsEmployeeMaster.iUsrComplianceModule = 1
                objclsEmployeeMaster.iUsrComplianceRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 3 Then 'Fixed Assets
                objclsEmployeeMaster.iUsrBCMmodule = 1
                objclsEmployeeMaster.iUsrBCMRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 5 Then 'Digital Office
                objclsEmployeeMaster.iUsrDigitalOfficeModule = 1
                objclsEmployeeMaster.iUsrDigitalOfficeRole = ddlRole.SelectedValue
            End If


            objclsEmployeeMaster.dusrDOB = "01/01/1900"
            objclsEmployeeMaster.susrBloodGroup = ""
            objclsEmployeeMaster.iusrGender = 1
            objclsEmployeeMaster.iusrMaritalStatus = 1
            objclsEmployeeMaster.iusrNoOfChildren = 0
            objclsEmployeeMaster.iusrResume = 0
            objclsEmployeeMaster.iusrPhoto = 0
            objclsEmployeeMaster.iusrSignature = 0
            objclsEmployeeMaster.iusrCurWrkAddId = 0
            objclsEmployeeMaster.iusrPermAddId = 0
            objclsEmployeeMaster.iusrResAddId = 0
            objclsEmployeeMaster.iusrOfficialAddId = 0
            objclsEmployeeMaster.iusr_IsSuperuser = 0
            objclsEmployeeMaster.iUSR_DeptID = objclsEmployeeMaster.GetDepartmentID(sSession.AccessCode, sSession.AccessCodeID, ddlCompanyName.SelectedValue, ddlCompanyName.SelectedItem.Text)
            Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Function
    Protected Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Dim sChangedPwd As String, iIsPasswordReset As Integer = 0
        Try
            lblError.Text = ""
            If ddlExistingUserName.SelectedIndex = 0 Then
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSAPCode.Text)) = True Then
                    lblError.Text = "EMP Code already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('EMP Code already exist','', 'error');", True)
                    txtSAPCode.Focus()
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, UCase(txtLoginName.Text)) = True Then
                    lblError.Text = "Login Name already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Login Name already exist','', 'error');", True)
                    txtLoginName.Focus()
                    Exit Sub
                End If
            Else
                sChangedPwd = objclsGRACeGeneral.DecryptPassword(objclsEmployeeMaster.GetUesrPassword(sSession.AccessCode, sSession.AccessCodeID, ddlExistingUserName.SelectedValue))
                If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then '
                    lblError.Text = "Entered Password and Confirm Password does not match."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Entered Password and Confirm Password does not match','', 'error');", True)
                    txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
                    txtPassword.Text = "" : txtConfirmPassword.Text = ""
                    txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
                    Exit Sub
                ElseIf ((Trim(txtPassword.Text) <> sChangedPwd) And Trim(txtConfirmPassword.Text) <> sChangedPwd) Then
                    iIsPasswordReset = 1
                End If
            End If

            If txtSAPCode.Text.Trim = "" Then
                lblError.Text = "Enter EMP Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter EMP Code','', 'warning');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If
            If txtSAPCode.Text.Trim.Length > 10 Then
                lblError.Text = "EMP Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('EMP Code exceeded maximum size(max 10 characters)','', 'error');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If

            If txtUserName.Text.Trim = "" Then
                lblError.Text = "Enter User Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter User Name','', 'warning');", True)
                txtUserName.Focus()
                Exit Sub
            End If
            If txtUserName.Text.Trim.Length > 50 Then
                lblError.Text = "User Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('User Name exceeded maximum size(max 50 characters)','', 'error');", True)
                txtUserName.Focus()
                Exit Sub
            End If

            If txtLoginName.Text.Trim = "" Then
                lblError.Text = "Enter Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Login Name','', 'warning');", True)
                txtLoginName.Focus()
                Exit Sub
            End If
            If txtLoginName.Text.Trim.Length > 25 Then
                lblError.Text = "Login Name exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Login Name exceeded maximum size(max 25 characters)','', 'error');", True)
                txtLoginName.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                lblError.Text = "Enter Password." : lblError.Text = "Enter Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Password','', 'warning');", True)
                txtPassword.Focus()
                Exit Sub
            End If

            If txtConfirmPassword.Text.Trim = "" Then
                lblError.Text = "Enter Confirm Password." : lblError.Text = "Enter Confirm Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Confirm Password','', 'warning');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                lblError.Text = "Entered Password & Confirm Password doesn't match."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Login Name exceeded maximum size(max 25 characters)','', 'error');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                lblError.Text = "Select Module." : lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Module','', 'waring');", True)
                ddlGroup.Focus()
                Exit Sub
            End If

            If txtEmail.Text.Trim.Length > 50 Then
                lblError.Text = "E-Mail exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('E-Mail exceeded maximum size(max 50 characters)','', 'error');", True)
                txtEmail.Focus()
                Exit Sub
            End If

            If txtOffice.Text.Trim.Length > 15 Then
                lblError.Text = "Office Phone No. exceeded maximum size(max 20 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Office Phone No. exceeded maximum size(max 20 numbers)','', 'error');", True)
                txtOffice.Focus()
                Exit Sub
            End If

            If txtMobile.Text.Trim <> "" Then
                If txtMobile.Text.Trim.Length > 10 Then
                    lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Mobile No. exceeded maximum size(max 10 numbers)','', 'error');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If

                If txtMobile.Text.Trim.Length <> 10 Then
                    lblError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid 10 digits Mobile No','', 'error');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If
            End If

            If txtResidence.Text.Trim.Length > 15 Then
                lblError.Text = "Residence No. exceeded maximum size(max 15 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Residence No. exceeded maximum size(max 15 numbers)','', 'error');", True)
                txtResidence.Focus()
                Exit Sub
            End If

            If ddlDesignation.SelectedIndex < 0 Then
                lblError.Text = "Select Designation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Designation','', 'warning');", True)
                ddlDesignation.Focus()
                Exit Sub
            End If

            If ddlRole.SelectedIndex = 0 Then
                lblError.Text = "Select Role."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Role','', 'warning');", True)
                ddlRole.Focus()
                Exit Sub
            End If

            If iIsPasswordReset = 1 Then
                objclsEmployeeMaster.UpdatePasswordReset(sSession.AccessCode, sSession.AccessCodeID, ddlExistingUserName.SelectedValue, 1)
            End If
            Arr = SaveEmployeeDetails()
            BindExistingEmployeeDB(0, 0, 0, 0, "")
            ddlExistingUserName.SelectedValue = Arr(1)
            ddlExistingUserName_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer User Master", "Updated", Arr(1), txtUserName.Text.Trim, 0, "", sSession.IPAddress)
                If sUMDFlag = "W" Then
                    lblError.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                    lblCustomerValidationMsg.Text = "Successfully Updated & Waiting for Approval."
                    ' ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated & Waiting for Approval','', 'success');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                Else
                    lblError.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                    lblCustomerValidationMsg.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
End Class
