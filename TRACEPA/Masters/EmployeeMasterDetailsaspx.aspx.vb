Imports System
Imports System.Data
Imports BusinesLayer
Partial Class EmployeeMasterDetailsaspx
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_EmployeeMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    Private Shared sEMDSave As String
    Private Shared sEMDFlag As String
    Private Shared sEMDBackStatus As String
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
        Dim iEmpID As Integer = 0, iStatusID As Integer = 0
        Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                sEMDSave = "NO"
                sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPEMP", 1)
                If sFormButtons = "False" Or sFormButtons = "" Then
                    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                    Exit Sub
                Else
                    If sFormButtons.Contains(",Save/Update,") = True Then
                        imgbtnSave.Visible = True
                        sEMDSave = "YES"
                    End If
                End If

                BindZone() : BindDesignationDB()
                BindRoleDB() : BindPermission() : BindModule()
                BindExistingEmployeeDB(0, 0, 0, 0, "")
                RFVZone.ErrorMessage = "Select Zone." : RFVZone.InitialValue = "Select Zone"

                RFVSAPCode.ErrorMessage = "Enter SAP Code." : REVSAPCode.ValidationExpression = "^[a-zA-Z0-9'@&amp;amp;#.\s]{0,10}$" : REVSAPCode.ErrorMessage = "Enter valid SAP Code."

                RFVEmpName.ErrorMessage = "Enter Employee Name." : REVEmpName.ErrorMessage = "Enter valid Employee Name." : REVEmpName.ValidationExpression = "^(.{0,50})$"

                RFVLoginName.ErrorMessage = "Enter Login Name." : REVLoginName.ErrorMessage = "Enter valid Login Name." : REVLoginName.ValidationExpression = "^[a-zA-Z0-9'@&amp;amp;#.\s]{0,25}$"

                RFVPasssword.ErrorMessage = "Enter Password." : RFVConfirmPassword.ErrorMessage = "Enter Confirm Password." : CVPassword.ErrorMessage = "Passwords does not match."

                RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" '"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

                REVOffice.ErrorMessage = "Enter valid Office Phone No." : REVOffice.ValidationExpression = "^[0-9]{0,15}$"

                REVMobile.ErrorMessage = "Enter valid 10 digit Mobile No." : REVMobile.ValidationExpression = "^[0-9]{10}$"

                REVResidence.ErrorMessage = "Enter valid Residence Phone No." : REVResidence.ValidationExpression = "^[0-9]{0,15}$"

                RFVDesignation.ErrorMessage = "Select Designation." : RFVDesignation.InitialValue = "Select Designation"

                RFVModule.ErrorMessage = "Select Module." : RFVModule.InitialValue = "Select Module"

                RFVRole.ErrorMessage = "Select Role." : RFVRole.InitialValue = "Select Role"

                If Request.QueryString("EmpID") IsNot Nothing Then
                    iEmpID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("EmpID")))
                    If iEmpID > 0 Then
                        Dim liEmpID As ListItem = ddlExistingEmpName.Items.FindByValue(iEmpID)
                        If IsNothing(liEmpID) = False Then
                            ddlExistingEmpName.SelectedValue = iEmpID
                            ddlExistingEmpName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sEMDBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindRoleDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
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
            ddlDesignation.Items.Insert(0, "Select Designation")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDesignationDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindExistingEmployeeDB(ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, iBranchID As Integer, ByVal sSearch As String)
        Try
            ddlExistingEmpName.DataSource = objclsEmployeeMaster.LoadExistingEmployee(sSession.AccessCode, sSession.AccessCodeID, iZoneID, iRegionID, iAreaID, iBranchID, sSearch)
            ddlExistingEmpName.DataTextField = "FullName"
            ddlExistingEmpName.DataValueField = "Usr_ID"
            ddlExistingEmpName.DataBind()
            ddlExistingEmpName.Items.Insert(0, "Select Existing Employee")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingEmployeeDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindZone()
        Dim dt As New DataTable
        Try
            dt = objclsEmployeeMaster.LoadZoneMaster(sSession.AccessCode, sSession.AccessCodeID)
            ddlZone.DataSource = dt
            ddlZone.DataTextField = "Org_Name"
            ddlZone.DataValueField = "org_node"
            ddlZone.DataBind()
            ddlZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindZone" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindModule()
        Try
            ddlGroup.Items.Insert(0, "Select Module")
            ddlGroup.Items.Insert(1, "Master")
            ddlGroup.Items.Insert(2, "Audit")
            ddlGroup.Items.Insert(3, "Risk")
            ddlGroup.Items.Insert(4, "Compliance")
            ddlGroup.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindModule" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindPermission()
        Try
            ddlPermission.Items.Insert(0, "Role based")
            ddlPermission.Items.Insert(1, "User based")
            ddlPermission.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPermission" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            lblError.Text = "" : sEMDFlag = "" : txtSearch.Text = "" : chkChangeLevel.Checked = False : chkChangeLevel.Visible = False : lblChangeLevel.Visible = False
            txtSAPCode.Text = "" : txtEmployeeName.Text = "" : txtLoginName.Text = ""
            ddlDesignation.SelectedIndex = 0 : ddlRole.SelectedIndex = 0 : ddlGroup.SelectedIndex = 0 : ddlPermission.SelectedIndex = 0
            txtEmail.Text = "" : txtOffice.Text = "" : txtMobile.Text = "" : txtResidence.Text = ""
            txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
            txtPassword.Text = "" : txtConfirmPassword.Text = ""
            txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If sEMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
            chkSendMail.Checked = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Protected Sub ddlExistingEmpName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingEmpName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim iGetZoneID As Integer, iGetRegionID As Integer, iGetAreaID As Integer
        Try
            ClearAll()
            ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            If ddlExistingEmpName.SelectedIndex > 0 Then
                chkChangeLevel.Visible = True : lblChangeLevel.Visible = True
                dt = objclsEmployeeMaster.LoadExistingEmployeeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmpName.SelectedValue)
                If dt.Rows.Count > 0 Then
                    txtSAPCode.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Code")) = False Then
                        txtSAPCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_Code").ToString())
                    End If

                    txtEmployeeName.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_FullName")) = False Then
                        txtEmployeeName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_FullName").ToString())
                    End If

                    ddlDesignation.SelectedIndex = 0
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

                    ddlRole.SelectedIndex = 0
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
                        sEMDFlag = dt.Rows(0).Item("Usr_Delflag")
                        If dt.Rows(0).Item("Usr_Delflag").ToString() = "W" Then
                            lblError.Text = "Waiting for Approval."
                            If sEMDSave = "YES" Then
                                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                            Else
                                imgbtnUpdate.Visible = False
                            End If
                        ElseIf dt.Rows(0).Item("Usr_Delflag").ToString() = "D" Then
                            lblError.Text = "De-Activated."
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                        Else
                            If sEMDSave = "YES" Then
                                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                            Else
                                imgbtnUpdate.Visible = False
                            End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(0).Item("Usr_Node")) = False Then
                        'Zone Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "1" Then
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If

                        'Region Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "2" Then
                            iGetZoneID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("Usr_OrgnID").ToString())
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(iGetZoneID))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = iGetZoneID

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")

                                Dim liRegionID As ListItem = ddlRegion.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                                If IsNothing(liRegionID) = False Then
                                    ddlRegion.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")

                                    ddlArea.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                                    ddlArea.DataTextField = "Org_Name"
                                    ddlArea.DataValueField = "org_node"
                                    ddlArea.DataBind()
                                    ddlArea.Items.Insert(0, "Select Area")
                                Else
                                    ddlRegion.SelectedIndex = 0 : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                                End If
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If

                        'Area Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "3" Then
                            iGetRegionID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("Usr_OrgnID").ToString())
                            iGetZoneID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, iGetRegionID)
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(iGetZoneID))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = iGetZoneID

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")

                                Dim liRegionID As ListItem = ddlRegion.Items.FindByValue(Val(iGetRegionID))
                                If IsNothing(liRegionID) = False Then
                                    ddlRegion.SelectedValue = iGetRegionID

                                    ddlArea.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                                    ddlArea.DataTextField = "Org_Name"
                                    ddlArea.DataValueField = "org_node"
                                    ddlArea.DataBind()
                                    ddlArea.Items.Insert(0, "Select Area")

                                    Dim liAreaID As ListItem = ddlArea.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                                    If IsNothing(liAreaID) = False Then
                                        ddlArea.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")

                                        ddlBranch.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlArea.SelectedValue)
                                        ddlBranch.DataTextField = "Org_Name"
                                        ddlBranch.DataValueField = "org_node"
                                        ddlBranch.DataBind()
                                        ddlBranch.Items.Insert(0, "Select Branch")
                                    Else
                                        ddlArea.SelectedIndex = 0 : ddlBranch.Items.Clear()
                                    End If
                                Else
                                    ddlRegion.SelectedIndex = 0 : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                                End If
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If

                        'Branch Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "4" Then
                            iGetAreaID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("Usr_OrgnID").ToString())
                            iGetRegionID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, iGetAreaID)
                            iGetZoneID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, iGetRegionID)
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(iGetZoneID))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = iGetZoneID

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")

                                Dim liRegionID As ListItem = ddlRegion.Items.FindByValue(Val(iGetRegionID))
                                If IsNothing(liRegionID) = False Then
                                    ddlRegion.SelectedValue = iGetRegionID

                                    ddlArea.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                                    ddlArea.DataTextField = "Org_Name"
                                    ddlArea.DataValueField = "org_node"
                                    ddlArea.DataBind()
                                    ddlArea.Items.Insert(0, "Select Area")

                                    Dim liAreaID As ListItem = ddlArea.Items.FindByValue(Val(iGetAreaID))
                                    If IsNothing(liAreaID) = False Then
                                        ddlArea.SelectedValue = iGetAreaID

                                        ddlBranch.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlArea.SelectedValue)
                                        ddlBranch.DataTextField = "Org_Name"
                                        ddlBranch.DataValueField = "org_node"
                                        ddlBranch.DataBind()
                                        ddlBranch.Items.Insert(0, "Select Branch")

                                        Dim liBranchID As ListItem = ddlBranch.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                                        If IsNothing(liBranchID) = False Then
                                            ddlBranch.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")
                                        Else
                                            ddlBranch.SelectedIndex = 0
                                        End If
                                    Else
                                        ddlArea.SelectedIndex = 0 : ddlBranch.Items.Clear()
                                    End If
                                Else
                                    ddlRegion.SelectedIndex = 0 : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                                End If
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If
                    End If
                End If
            Else
                BindExistingEmployeeDB(0, 0, 0, 0, "")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingEmpName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlZone.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            If ddlZone.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                ddlRegion.DataSource = dt
                ddlRegion.DataTextField = "Org_Name"
                ddlRegion.DataValueField = "org_node"
                ddlRegion.DataBind()
                ddlRegion.Items.Insert(0, "Select Region")
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(ddlZone.SelectedValue, 0, 0, 0, "")
                End If
            Else
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(0, 0, 0, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlZone_SelectedIndexChanged" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : sEMDFlag = "" : chkChangeLevel.Checked = False : chkChangeLevel.Visible = False : lblChangeLevel.Visible = False
            txtSearch.Text = "" : txtLoginName.Text = "" : txtSAPCode.Text = "" : txtEmployeeName.Text = ""
            ddlExistingEmpName.SelectedIndex = 0 : ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            ddlDesignation.SelectedIndex = 0 : ddlRole.SelectedIndex = 0 : ddlGroup.SelectedIndex = 0 : ddlPermission.SelectedIndex = 0
            txtEmail.Text = "" : txtOffice.Text = "" : txtMobile.Text = "" : txtResidence.Text = ""
            txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
            txtPassword.Text = "" : txtConfirmPassword.Text = ""
            txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If sEMDSave = "YES" Then
                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            If ddlRegion.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                ddlArea.DataSource = dt
                ddlArea.DataTextField = "Org_Name"
                ddlArea.DataValueField = "org_node"
                ddlArea.DataBind()
                ddlArea.Items.Insert(0, "Select Area")
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(0, ddlRegion.SelectedValue, 0, 0, "")
                End If
            Else
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(ddlZone.SelectedValue, 0, 0, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRegion_SelectedIndexChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlArea.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlBranch.Items.Clear()
            If ddlArea.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlArea.SelectedValue)
                ddlBranch.DataSource = dt
                ddlBranch.DataTextField = "Org_Name"
                ddlBranch.DataValueField = "org_node"
                ddlBranch.DataBind()
                ddlBranch.Items.Insert(0, "Select Branch")
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(0, 0, ddlArea.SelectedValue, 0, "")
                End If
            Else
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(0, ddlRegion.SelectedValue, 0, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlArea_SelectedIndexChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Try
            lblError.Text = ""
            If chkChangeLevel.Checked = False Then
                If ddlBranch.SelectedIndex > 0 Then
                    ClearAll()
                    BindExistingEmployeeDB(0, 0, 0, ddlBranch.SelectedValue, "")
                Else
                    BindExistingEmployeeDB(0, 0, ddlArea.SelectedValue, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ibSearch_Click(sender As Object, e As ImageClickEventArgs) Handles ibSearch.Click
        Dim iCheckZRAB As Integer
        Dim sSearch As String
        Try
            lblError.Text = ""
            If ddlZone.SelectedIndex > 0 Then
                iCheckZRAB = 1
            End If
            If ddlRegion.SelectedIndex > 0 Then
                iCheckZRAB = 2
            End If
            If ddlArea.SelectedIndex > 0 Then
                iCheckZRAB = 3
            End If
            If ddlBranch.SelectedIndex > 0 Then
                iCheckZRAB = 4
            End If
            sSearch = objclsGRACeGeneral.SafeSQL(txtSearch.Text.Trim)
            ClearAll()
            If iCheckZRAB = 1 Then
                BindExistingEmployeeDB(ddlZone.SelectedValue, 0, 0, 0, sSearch)
            ElseIf iCheckZRAB = 2 Then
                BindExistingEmployeeDB(0, ddlRegion.SelectedValue, 0, 0, sSearch)
            ElseIf iCheckZRAB = 3 Then
                BindExistingEmployeeDB(0, 0, ddlArea.SelectedValue, 0, sSearch)
            ElseIf iCheckZRAB = 4 Then
                BindExistingEmployeeDB(0, 0, 0, ddlBranch.SelectedValue, sSearch)
            Else
                BindExistingEmployeeDB(0, 0, 0, 0, sSearch)
            End If
            txtSearch.Text = sSearch
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ibSearch_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(sEMDBackStatus)))
            Response.Redirect(String.Format("~/Masters/EmployeeMaster.aspx?StatusID={0}", oStatus), False) 'Masters/EmployeeMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sChangedPwd As String, iIsPasswordReset As Integer = 0
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer
        Try
            lblError.Text = ""
            If ddlExistingEmpName.SelectedIndex = 0 Then
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSAPCode.Text)) = True Then
                    lblEmpMasterDetailsValidationMsg.Text = "SAP Code already exist." : lblError.Text = "SAP Code already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtSAPCode.Focus()
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, UCase(txtLoginName.Text)) = True Then
                    lblEmpMasterDetailsValidationMsg.Text = "Login Name already exist." : lblError.Text = "Login Name already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtLoginName.Focus()
                    Exit Sub
                End If
            Else
                sChangedPwd = objclsGRACeGeneral.DecryptPassword(objclsEmployeeMaster.GetUesrPassword(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmpName.SelectedValue))
                If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then '
                    lblEmpMasterDetailsValidationMsg.Text = "Entered Password and Confirm Password does not match." : lblError.Text = "Entered Password and Confirm Password does not match."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
                    txtPassword.Text = "" : txtConfirmPassword.Text = ""
                    txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
                    Exit Sub
                ElseIf ((Trim(txtPassword.Text) <> sChangedPwd) And Trim(txtConfirmPassword.Text) <> sChangedPwd) Then
                    iIsPasswordReset = 1
                End If
            End If

            If txtSAPCode.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter SAP Code." : lblError.Text = "Enter SAP Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If
            If txtSAPCode.Text.Trim.Length > 10 Then
                lblEmpMasterDetailsValidationMsg.Text = "SAP Code exceeded maximum size(max 10 characters)." : lblError.Text = "SAP Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If

            If txtEmployeeName.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Employee Name." : lblError.Text = "Enter Employee Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If
            If txtEmployeeName.Text.Trim.Length > 50 Then
                lblEmpMasterDetailsValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters)." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If

            If txtLoginName.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Login Name." : lblError.Text = "Enter Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If
            If txtLoginName.Text.Trim.Length > 25 Then
                lblEmpMasterDetailsValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters)." : lblError.Text = "Login Name exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Password." : lblError.Text = "Enter Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtPassword.Focus()
                Exit Sub
            End If

            If txtConfirmPassword.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Confirm Password." : lblError.Text = "Enter Confirm Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                lblEmpMasterDetailsValidationMsg.Text = " Entered Password & Confirm Password doesn't match." : lblError.Text = " Entered Password & Confirm Password doesn't match."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtEmail.Text.Trim.Length > 50 Then
                lblEmpMasterDetailsValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters)." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtEmail.Focus()
                Exit Sub
            End If

            If txtOffice.Text.Trim.Length > 15 Then
                lblEmpMasterDetailsValidationMsg.Text = "Office Phone No. exceeded maximum size(max 20 numbers)." : lblError.Text = "Office Phone No. exceeded maximum size(max 20 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtOffice.Focus()
                Exit Sub
            End If

            If txtMobile.Text.Trim <> "" Then
                If txtMobile.Text.Trim.Length > 10 Then
                    lblEmpMasterDetailsValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers).'" : lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If

                If txtMobile.Text.Trim.Length <> 10 Then
                    lblEmpMasterDetailsValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If
            End If

            If txtResidence.Text.Trim.Length > 15 Then
                lblEmpMasterDetailsValidationMsg.Text = "Residence No. exceeded maximum size(max 15 numbers)." : lblError.Text = "Residence No. exceeded maximum size(max 15 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtResidence.Focus()
                Exit Sub
            End If

            If ddlDesignation.SelectedIndex = 0 Then
                lblEmpMasterDetailsValidationMsg.Text = "Select Designation." : lblError.Text = "Select Designation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                ddlDesignation.Focus()
                Exit Sub
            End If

            If ddlRole.SelectedIndex = 0 Then
                lblEmpMasterDetailsValidationMsg.Text = "Select Role." : lblError.Text = "Select Role."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                ddlRole.Focus()
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                lblEmpMasterDetailsValidationMsg.Text = "Select Module." : lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                ddlGroup.Focus()
                Exit Sub
            End If

            If iIsPasswordReset = 1 Then
                objclsEmployeeMaster.UpdatePasswordReset(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmpName.SelectedValue, 1)
            End If
            Arr = SaveEmployeeDetails()
            BindExistingEmployeeDB(iZoneID, iRegionID, iAreaID, iBranchID, "")
            ddlExistingEmpName.SelectedValue = Arr(1)
            ddlExistingEmpName_SelectedIndexChanged(sender, e)
            If Arr(0) = "3" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile", "Saved", Arr(1), txtEmployeeName.Text.Trim, 0, "", sSession.IPAddress)
                lblEmpMasterDetailsValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                sEMDBackStatus = 4
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Function SaveEmployeeDetails() As Array
        Dim Arr() As String
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer
        Try
            If ddlZone.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlZone.SelectedValue
                objclsEmployeeMaster.iUsrNode = 1
                iZoneID = ddlZone.SelectedValue
            End If

            If ddlRegion.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlRegion.SelectedValue
                objclsEmployeeMaster.iUsrNode = 2
                iZoneID = 0
                iRegionID = ddlRegion.SelectedValue
            End If

            If ddlArea.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlArea.SelectedValue
                objclsEmployeeMaster.iUsrNode = 3
                iZoneID = 0 : iRegionID = 0
                iAreaID = ddlArea.SelectedValue
            End If

            If ddlBranch.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlBranch.SelectedValue
                objclsEmployeeMaster.iUsrNode = 4
                iZoneID = 0 : iRegionID = 0 : iAreaID = 0
                iBranchID = ddlBranch.SelectedValue
            End If

            If ddlExistingEmpName.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUserID = ddlExistingEmpName.SelectedValue
                objclsEmployeeMaster.sUsrStatus = "U"
            Else
                objclsEmployeeMaster.iUserID = 0
                objclsEmployeeMaster.sUsrStatus = "C"
            End If
            If chkSendMail.Checked = True Then
                objclsEmployeeMaster.iUsrSentMail = 1
            Else
                objclsEmployeeMaster.iUsrSentMail = 0
            End If
            objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(txtSAPCode.Text.Trim)
            objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(txtEmployeeName.Text.Trim)
            objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(txtLoginName.Text.Trim)
            objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword(txtPassword.Text)
            objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(txtEmail.Text.Trim)

            objclsEmployeeMaster.sUsrDutyStatus = "W"
            objclsEmployeeMaster.sUsrPhoneNo = objclsGRACeGeneral.SafeSQL(txtResidence.Text.Trim)
            objclsEmployeeMaster.sUsrMobileNo = objclsGRACeGeneral.SafeSQL(txtMobile.Text.Trim)
            objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(txtOffice.Text.Trim)
            objclsEmployeeMaster.sUsrOffPhExtn = ""
            objclsEmployeeMaster.iUsrDesignation = ddlDesignation.SelectedValue
            objclsEmployeeMaster.iUsrCompanyID = 0
            objclsEmployeeMaster.iUsrRole = ddlRole.SelectedValue
            objclsEmployeeMaster.iUsrLevelGrp = ddlGroup.SelectedIndex
            objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = ddlPermission.SelectedIndex
            objclsEmployeeMaster.sUsrFlag = "W"
            objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
            objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
            objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
            objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
            objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
            objclsEmployeeMaster.iUsrBCMmodule = 0

            objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
            objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
            objclsEmployeeMaster.iUsrBCMRole = 0
            If ddlGroup.SelectedIndex = 1 Then 'Master
                objclsEmployeeMaster.iUsrMasterModule = 1
                objclsEmployeeMaster.iUsrMasterRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 2 Then 'Audit
                objclsEmployeeMaster.iUsrAuditModule = 1
                objclsEmployeeMaster.iUsrAuditRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 3 Then 'Risk
                objclsEmployeeMaster.iUsrRiskModule = 1
                objclsEmployeeMaster.iUsrRiskRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 4 Then 'Compliance
                objclsEmployeeMaster.iUsrComplianceModule = 1
                objclsEmployeeMaster.iUsrComplianceRole = ddlRole.SelectedValue
            End If

            objclsEmployeeMaster.dusrDOB = "01/01/1900"
            objclsEmployeeMaster.susrBloodGroup = ""
            objclsEmployeeMaster.iusrGender = 0
            objclsEmployeeMaster.iusrMaritalStatus = 0
            objclsEmployeeMaster.iusrNoOfChildren = 0
            objclsEmployeeMaster.iusrResume = 0
            objclsEmployeeMaster.iusrPhoto = 0
            objclsEmployeeMaster.iusrSignature = 0
            objclsEmployeeMaster.iusrCurWrkAddId = 0
            objclsEmployeeMaster.iusrPermAddId = 0
            objclsEmployeeMaster.iusrResAddId = 0
            objclsEmployeeMaster.iusrOfficialAddId = 0
            Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeDetails" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Function
    Protected Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Dim sChangedPwd As String, iIsPasswordReset As Integer = 0
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer
        Try
            lblError.Text = ""
            If ddlExistingEmpName.SelectedIndex = 0 Then
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, txtSAPCode.Text) = True Then
                    lblEmpMasterDetailsValidationMsg.Text = "SAP Code already exist." : lblError.Text = "SAP Code already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtSAPCode.Focus()
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, txtLoginName.Text) = True Then
                    lblEmpMasterDetailsValidationMsg.Text = "Login Name already exist." : lblError.Text = "Login Name already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtLoginName.Focus()
                    Exit Sub
                End If
            Else
                sChangedPwd = objclsGRACeGeneral.DecryptPassword(objclsEmployeeMaster.GetUesrPassword(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmpName.SelectedValue))
                If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then '
                    lblEmpMasterDetailsValidationMsg.Text = "Entered Password and Confirm Password does not match." : lblError.Text = "Entered Password and Confirm Password does not match."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
                    txtPassword.Text = "" : txtConfirmPassword.Text = ""
                    txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
                    Exit Sub
                ElseIf ((Trim(txtPassword.Text) <> sChangedPwd) And Trim(txtConfirmPassword.Text) <> sChangedPwd) Then
                    iIsPasswordReset = 1
                End If
            End If

            If txtSAPCode.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter SAP Code." : lblError.Text = "Enter SAP Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If
            If txtSAPCode.Text.Trim.Length > 10 Then
                lblEmpMasterDetailsValidationMsg.Text = "SAP Code exceeded maximum size(max 10 characters)." : lblError.Text = "SAP Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If

            If txtEmployeeName.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Employee Name." : lblError.Text = "Enter Employee Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If
            If txtEmployeeName.Text.Trim.Length > 50 Then
                lblEmpMasterDetailsValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters)." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If

            If txtLoginName.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Login Name." : lblError.Text = "Enter Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If
            If txtLoginName.Text.Trim.Length > 25 Then
                lblEmpMasterDetailsValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters)." : lblError.Text = "Login Name exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Password." : lblError.Text = "Enter Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtPassword.Focus()
                Exit Sub
            End If

            If txtConfirmPassword.Text.Trim = "" Then
                lblEmpMasterDetailsValidationMsg.Text = "Enter Confirm Password." : lblError.Text = "Enter Confirm Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                lblEmpMasterDetailsValidationMsg.Text = " Entered Password & Confirm Password doesn't match." : lblError.Text = " Entered Password & Confirm Password doesn't match."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtEmail.Text.Trim.Length > 50 Then
                lblEmpMasterDetailsValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters)." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtEmail.Focus()
                Exit Sub
            End If

            If txtOffice.Text.Trim.Length > 15 Then
                lblEmpMasterDetailsValidationMsg.Text = "Office Phone No. exceeded maximum size(max 20 numbers)." : lblError.Text = "Office Phone No. exceeded maximum size(max 20 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtOffice.Focus()
                Exit Sub
            End If

            If txtMobile.Text.Trim <> "" Then
                If txtMobile.Text.Trim.Length > 10 Then
                    lblEmpMasterDetailsValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers).'" : lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If

                If txtMobile.Text.Trim.Length <> 10 Then
                    lblEmpMasterDetailsValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If
            End If

            If txtResidence.Text.Trim.Length > 15 Then
                lblEmpMasterDetailsValidationMsg.Text = "Residence No. exceeded maximum size(max 15 numbers)." : lblError.Text = "Residence No. exceeded maximum size(max 15 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                txtResidence.Focus()
                Exit Sub
            End If

            If ddlDesignation.SelectedIndex = 0 Then
                lblEmpMasterDetailsValidationMsg.Text = "Select Designation." : lblError.Text = "Select Designation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                ddlDesignation.Focus()
                Exit Sub
            End If

            If ddlRole.SelectedIndex = 0 Then
                lblEmpMasterDetailsValidationMsg.Text = "Select Role." : lblError.Text = "Select Role."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                ddlRole.Focus()
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                lblEmpMasterDetailsValidationMsg.Text = "Select Module." : lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
                ddlGroup.Focus()
                Exit Sub
            End If

            If iIsPasswordReset = 1 Then
                objclsEmployeeMaster.UpdatePasswordReset(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmpName.SelectedValue, 1)
            End If
            Arr = SaveEmployeeDetails()
            BindExistingEmployeeDB(iZoneID, iRegionID, iAreaID, iBranchID, "")
            ddlExistingEmpName.SelectedValue = Arr(1)
            ddlExistingEmpName_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile", "Updated", Arr(1), txtEmployeeName.Text.Trim, 0, "", sSession.IPAddress)
                If sEMDFlag = "W" Then
                    lblEmpMasterDetailsValidationMsg.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                Else
                    lblEmpMasterDetailsValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpMasterDetailsValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class
