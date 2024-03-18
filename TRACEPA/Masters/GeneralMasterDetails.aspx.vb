Imports System
Imports System.Data
Imports BusinesLayer
Partial Class GeneralMasterDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_GeneralMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    Private Shared sTypeName As String
    Private Shared sMasterName As String
    Private Shared iMasterIDFromDB As Integer
    Private Shared sTableName As String
    'Private Shared sGMSave As String
    Private Shared sGMFlag As String
    Private Shared sGMBackStatus As String
    Private arrListDesignation As New ArrayList() From {"admin", "partner", "audit assistant"}
    Private arrListRole As New ArrayList() From {"admin", "partner", "audit assistant"}
    Private arrListWS As New ArrayList() From {"yet to start", "work in progress", "wip", "completed"}
    Private arrListFRE As New ArrayList() From {"fortnightly", "monthly", "bimonthly", "quarterly", "half yearly", "yearly"}
    Private arrListIND As New ArrayList() From {"banking", "consultation", "hospitality", "insurance", "it", "manufacturing", "service", "others"}
    Private arrListORG As New ArrayList() From {"association", "bank", "llp", "partnership firms", "private limited", "trust"}
    Private arrListDRL As New ArrayList() From {"voucher details", "transaction details", "confirmation of balances"}
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddAct.ImageUrl = "~/Images/Add16.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Dim iMaxID As Integer
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                'sGMSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MSGM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True
                '        sGMSave = "YES"
                '    End If
                'End If
                lblHAct.Visible = False : ddlAct.Visible = False : imgbtnAddAct.Visible = False
                lblHeadingHSN.Visible = False : txtHSN.Visible = False
                pnlTask.Visible = False
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sGMBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("MasterName") IsNot Nothing Then
                    sMasterName = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterName")))
                    If UCase(sMasterName) = "AP" Then
                        iMasterIDFromDB = 1
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Audit Check Point "
                        lblDesc.Text = "* Audit Check Point" : sTypeName = "Audit Check Point"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "AT" Then
                        iMasterIDFromDB = 2
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Audit Task/Assignments"
                        lblDesc.Text = "* Audit Task/Assignments" : sTypeName = "Audit Task/Assignments"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                        lblHAct.Visible = True : ddlAct.Visible = True : imgbtnAddAct.Visible = True
                        lblHeadingHSN.Visible = True : txtHSN.Visible = True
                        pnlTask.Visible = True
                    ElseIf UCase(sMasterName) = "ASF" Then
                        iMasterIDFromDB = 3
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Audit Sign Off"
                        lblDesc.Text = "* Audit Sign Off" : sTypeName = "Audit Sign Off"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "BT" Then
                        iMasterIDFromDB = 4
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Bill Type"
                        lblDesc.Text = "* Bill Type" : sTypeName = "Bill Type"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "WPC" Then
                        iMasterIDFromDB = 5
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Conclusion"
                        lblDesc.Text = "* Conclusion" : sTypeName = "Conclusion"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "DESG" Then
                        iMasterIDFromDB = 6
                        sTableName = "SAD_GRPDESGN_GENERAL_MASTER"
                        lblMasterHead.Text = "Existing Designation"
                        lblDesc.Text = "* Designation" : sTypeName = "Designation"
                    ElseIf UCase(sMasterName) = "DRL" Then
                        iMasterIDFromDB = 7
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Document Request List"
                        lblDesc.Text = "* Document Request List" : sTypeName = "Document Request List"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "EC" Then
                        iMasterIDFromDB = 8
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Expense Charges"
                        lblDesc.Text = "* Expense Charges" : sTypeName = "Expense Charges"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "FRE" Then
                        iMasterIDFromDB = 9
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Frequency"
                        lblDesc.Text = "* Frequency" : sTypeName = "Frequency"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "IND" Then
                        iMasterIDFromDB = 10
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Industry Type"
                        lblDesc.Text = "* Industry Type" : sTypeName = "Industry Type"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "MNG" Then
                        iMasterIDFromDB = 11
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Management"
                        lblDesc.Text = "* Management" : sTypeName = "Management"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "NAT" Then
                        iMasterIDFromDB = 12
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Non-Audit Task"
                        lblDesc.Text = "* Non-Audit Task" : sTypeName = "Non-Audit Task"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "ORG" Then
                        iMasterIDFromDB = 13
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Organization Type"
                        lblDesc.Text = "* Organization Type" : sTypeName = "Organization Type"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "OE" Then
                        iMasterIDFromDB = 14
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Other Expenses"
                        lblDesc.Text = "* Other Expenses" : sTypeName = "Other Expenses"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf sMasterName = "LE" Then
                        iMasterIDFromDB = 15
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Reimbursement"
                        lblDesc.Text = "* Reimbursement" : sTypeName = "Reimbursement"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "ROLE" Then
                        iMasterIDFromDB = 16
                        sTableName = "SAD_GRPORLVL_GENERAL_MASTER"
                        lblMasterHead.Text = "Existing Role"
                        lblDesc.Text = "* Role" : sTypeName = "Role"
                    ElseIf UCase(sMasterName) = "TOR" Then
                        iMasterIDFromDB = 17
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Type of Report"
                        lblDesc.Text = "* Type of Report" : sTypeName = "Type of Report"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "TOT" Then
                        iMasterIDFromDB = 18
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Type of Test"
                        lblDesc.Text = "* Type of Test" : sTypeName = "Type of Test"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "WS" Then
                        iMasterIDFromDB = 19
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Work Status"
                        lblDesc.Text = "* Work Status" : sTypeName = "Work Status"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "TM" Then
                        iMasterIDFromDB = 20
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Tax Master"
                        lblDesc.Text = "* Tax Master" : sTypeName = "Tax Master"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                        pnlRate.Visible = True
                    ElseIf UCase(sMasterName) = "UM" Then
                        iMasterIDFromDB = 21
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Unit of Measurement"
                        lblDesc.Text = "* Unit of Measurement" : sTypeName = "Unit of Measurement"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        'txtCode.Enabled = False
                        'pnlRate.Visible = True
                    ElseIf UCase(sMasterName) = "JE" Then
                        iMasterIDFromDB = 22
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing JE Type"
                        lblDesc.Text = "* JE Type" : sTypeName = "JE Type"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    ElseIf UCase(sMasterName) = "MR" Then
                        iMasterIDFromDB = 23
                        sTableName = "Content_Management_Master"
                        lblMasterHead.Text = "Existing Management Representations" : lblNotes.Text = "Description"
                        lblDesc.Text = "* Heading" : sTypeName = "Heading"
                        iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                        txtCode.Text = sMasterName & "_" & iMaxID
                        txtCode.Enabled = False
                    End If
                    BindDescDetails()
                End If
                BindActDetails()

                If Request.QueryString("MasterID") IsNot Nothing Then
                    ddlDesc.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterID")))
                    ddlDesc_SelectedIndexChanged(sender, e)
                End If
                REVDescName.ValidationGroup = False
                RFVCode.ValidationGroup = False

                RFVDescName.ValidationGroup = True : RFVCode.ValidationGroup = True
                RFVDescName.ErrorMessage = "Enter " & sTypeName & " Name."
                RFVDescName.ValidationGroup = "Validate" : REVDescName.ValidationGroup = "Validate"
                RFVCode.ValidationGroup = "Validate" : REVNotes.ValidationGroup = "Validate"
                RFVDescName.ControlToValidate = "txtDesc" : REVDescName.ValidationExpression = "^[\s\S]{0,100}$" : REVDescName.ErrorMessage = sTypeName & " exceeded maximum size(max 100 characters)."
                RFVCode.ControlToValidate = "txtCode" : RFVCode.ErrorMessage = "Enter Code."
                REVNotes.ControlToValidate = "txtNotes" : REVNotes.ValidationExpression = "^[\s\S]{0,100}$" : REVNotes.ErrorMessage = "Notes exceeded maximum size(max 100 characters)."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub BindActDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAct(sSession.AccessCode, sSession.AccessCodeID)
            ddlAct.DataSource = dt
            ddlAct.DataTextField = "CMM_Act"
            ddlAct.DataBind()
            ddlAct.Items.Insert(0, "Select")
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub BindDescDetails()
        Dim dt As New DataTable
        Try
            If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                dt = objclsAdminMaster.LoadAdminMasterDesgRoleDetails(sSession.AccessCode, sSession.AccessCodeID, sTableName)
            Else
                dt = objclsAdminMaster.LoadAllAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, sMasterName)
            End If
            ddlDesc.DataSource = dt
            ddlDesc.DataTextField = "Name"
            ddlDesc.DataValueField = "PKID"
            ddlDesc.DataBind()
            ddlDesc.Items.Insert(0, "Select " & sTypeName & "")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesc.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            lblGeneralMasterStatus.Text = "" : txtCode.Enabled = False
            txtCode.Text = "" : txtDesc.Text = "" : txtNotes.Text = "" : txtRate.Text = "" : chkComplianceTask.Checked = False : txtHSN.Text = "" : ddlAct.SelectedIndex = 0
            'If sGMSave = "YES" Then
            imgbtnSave.Visible = True
            'Else
            '    imgbtnSave.Visible = False
            'End If
            If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                txtCode.Enabled = True
            End If
            If ddlDesc.SelectedIndex > 0 Then
                imgbtnSave.Visible = False
                'If sGMSave = "YES" Then
                imgbtnUpdate.Visible = True
                'Else
                '    imgbtnUpdate.Visible = False
                'End If
                txtCode.Enabled = False
                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    dt = objclsAdminMaster.GetAdminMasterDesgRoleDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sTableName)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0).Item("Mas_Description")) = False Then
                            txtDesc.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Description")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("Mas_Code")) = False Then
                            txtCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Code")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("Mas_Notes")) = False Then
                            txtNotes.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Notes")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("Mas_DelFlag")) = False Then
                            sGMFlag = dt.Rows(0).Item("Mas_DelFlag")
                        End If
                    End If
                Else
                    dt = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0).Item("CMM_Desc")) = False Then
                            txtDesc.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("CMM_Desc")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("CMM_Code")) = False Then
                            txtCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("CMM_Code")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("CMS_Remarks")) = False Then
                            txtNotes.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("CMS_Remarks")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("CMM_DelFlag")) = False Then
                            sGMFlag = dt.Rows(0).Item("CMM_DelFlag")
                        End If
                        If IsDBNull(dt.Rows(0).Item("CMM_Rate")) = False Then
                            txtRate.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("CMM_Rate")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("CMM_HSNSAC")) = False Then
                            txtHSN.Text = objclsGRACeGeneral.ReplaceSafeSQL(Trim(dt.Rows(0).Item("CMM_HSNSAC")))
                        End If
                        If IsDBNull(dt.Rows(0).Item("CMS_KeyComponent")) = False Then
                            If dt.Rows(0).Item("CMS_KeyComponent") = 1 Then
                                chkComplianceTask.Checked = True
                            Else
                                chkComplianceTask.Checked = False
                            End If
                        End If
                        If IsDBNull(dt.Rows(0).Item("cmm_ID")) = False Then
                            ddlAct.Items.Clear()
                            ' ddlAct.SelectedValue = dt.Rows(0).Item("cmm_ID")
                            BindActDetails()
                            If ddlDesc.SelectedIndex >= 1 Then
                                ' ddlAct.SelectedIndex = 0
                                Dim dts As DataTable = objclsAdminMaster.LoadActselected(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue)
                                If dts.Rows.Count > 0 Then
                                    If IsDBNull(dts.Rows(0).Item("CMM_Act")) = False Then
                                        ddlAct.SelectedItem.Text = dts.Rows(0).Item("CMM_Act")
                                    End If
                                Else
                                    ddlAct.SelectedIndex = 0
                                End If
                            End If
                        End If
                    End If
                End If
                If sGMFlag = "W" Then
                    lblGeneralMasterStatus.Text = "Waiting for Approval"
                    'If sGMSave = "YES" Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    'End If
                ElseIf sGMFlag = "D" Then
                    lblGeneralMasterStatus.Text = "De-Activated"
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                Else
                    lblGeneralMasterStatus.Text = "Activated"
                    'If sGMSave = "YES" Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    'End If
                End If

                Dim result As Boolean = False
                If UCase(sMasterName) = "DESG" Then
                    result = arrListDesignation.Contains(txtDesc.Text.ToLower())
                ElseIf UCase(sMasterName) = "ROLE" Then
                    result = arrListRole.Contains(txtDesc.Text.ToLower())
                ElseIf UCase(sMasterName) = "WS" Then
                    result = arrListWS.Contains(txtDesc.Text.ToLower())
                ElseIf UCase(sMasterName) = "FRE" Then
                    result = arrListFRE.Contains(txtDesc.Text.ToLower())
                ElseIf UCase(sMasterName) = "IND" Then
                    result = arrListIND.Contains(txtDesc.Text.ToLower())
                ElseIf UCase(sMasterName) = "ORG" Then
                    result = arrListORG.Contains(txtDesc.Text.ToLower())
                ElseIf UCase(sMasterName) = "DRL" Then
                    result = arrListDRL.Contains(txtDesc.Text.ToLower())
                End If
                If result = True Then
                    imgbtnUpdate.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDesc_SelectedIndexChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As EventArgs) Handles imgbtnAdd.Click
        Dim iMaxID As Integer
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnBack.Visible = True : imgbtnUpdate.Visible = False
            'If sGMSave = "YES" Then
            imgbtnSave.Visible = True
            'Else
            '    imgbtnSave.Visible = False
            'End If
            'pnlAct.Visible = False

            ddlDesc.SelectedIndex = 0 : lblGeneralMasterStatus.Text = "" : txtCode.Enabled = True : txtHSN.Text = ""
            txtCode.Text = "" : txtDesc.Text = "" : txtNotes.Text = "" : txtRate.Text = "" : chkComplianceTask.Checked = False
            If (sMasterName = "AP" Or sMasterName = "AT" Or sMasterName = "ASF" Or sMasterName = "BT" Or sMasterName = "WPC" Or sMasterName = "DRL" Or sMasterName = "EC" Or sMasterName = "FRE" Or sMasterName = "IND" Or
                  sMasterName = "MNG" Or sMasterName = "NAT" Or sMasterName = "ORG" Or sMasterName = "OE" Or sMasterName = "LE" Or sMasterName = "TOR" Or sMasterName = "TOT" Or sMasterName = "WS" Or sMasterName = "TM" Or sMasterName = "UM" Or sMasterName = "JE" Or sMasterName = "MR") Then
                iMaxID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                txtCode.Text = sMasterName & "_" & iMaxID
                txtCode.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If txtDesc.Text.Trim = "" Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter " & sTypeName & "." : lblError.Text = "Enter " & sTypeName & "."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDesc.Text.Trim.Length > 100 Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = sTypeName & " exceeded maximum size(Max 100 characters)." : lblError.Text = sTypeName & " exceeded maximum size(Max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim = "" Then
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Code." : lblError.Text = "Enter Code."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim.Length > 10 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Code exceeded maximum size(max 10 characters)." : lblError.Text = "Code exceeded maximum size(max 10 characters)."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtNotes.Text.Trim.Length > 100 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Note exceeded maximum size(max 100 characters)." : lblError.Text = "Note exceeded maximum size(max 100 characters)."
                txtNotes.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDesc.SelectedIndex > 0 Then
                'To check Description Deleted or not
                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "Mas_Description", ddlDesc.SelectedValue, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "CMM_Desc", ddlDesc.SelectedValue, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated description cannot be updated." : lblError.Text = "De-Activated description cannot be updated."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "Mas_Code", ddlDesc.SelectedValue, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "CMM_Code", ddlDesc.SelectedValue, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "Mas_Description", ddlDesc.SelectedValue, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "CMM_Desc", ddlDesc.SelectedValue, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered " & sTypeName & " Name already exist." : lblError.Text = "Entered " & sTypeName & " Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "Mas_Code", 0, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "CMM_Code", 0, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "Mas_Description", 0, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "CMM_Desc", 0, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered " & sTypeName & " Name already exist." : lblError.Text = "Entered " & sTypeName & " Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                Arr = objclsAdminMaster.SaveOrUpdateDtls(sSession.AccessCode, sSession.AccessCodeID, 0, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), objclsGRACeGeneral.SafeSQL(txtNotes.Text.Trim), sTableName, sSession.UserID, sSession.IPAddress)
            Else
                If ddlDesc.SelectedIndex = 0 Then
                    objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                    objclsAdminMaster.sCode = sMasterName & "_" & objclsAdminMaster.iID
                    objclsAdminMaster.iID = 0
                Else
                    objclsAdminMaster.iID = ddlDesc.SelectedValue
                    objclsAdminMaster.sCode = objclsGRACeGeneral.SafeSQL(txtCode.Text)
                End If
                objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim)
                objclsAdminMaster.sCategory = sMasterName
                objclsAdminMaster.iRiskCategory = 0
                objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(txtNotes.Text.Trim)
                If chkComplianceTask.Checked = True Then
                    objclsAdminMaster.iKeyComponent = 1
                Else
                    objclsAdminMaster.iKeyComponent = 0
                End If
                objclsAdminMaster.sModule = "A"
                objclsAdminMaster.sDelflag = "W"
                objclsAdminMaster.sStatus = "C"
                If txtRate.Text <> "" Then
                    objclsAdminMaster.dcmmRate = txtRate.Text
                Else
                    objclsAdminMaster.dcmmRate = "0.0"
                End If

                If ddlAct.SelectedIndex >= 1 Then
                    objclsAdminMaster.sCMMAct = ddlAct.SelectedItem.Text
                Else
                    objclsAdminMaster.sCMMAct = ""
                End If

                objclsAdminMaster.sCMMHSNSAC = txtHSN.Text

                objclsAdminMaster.iCrBy = sSession.UserID
                objclsAdminMaster.iUpdatedBy = sSession.UserID
                objclsAdminMaster.sIpAddress = sSession.IPAddress
                objclsAdminMaster.iCompId = sSession.AccessCodeID
                Arr = objclsAdminMaster.SaveMasterDetails(sSession.AccessCode, objclsAdminMaster)
            End If
            BindDescDetails()
            ddlDesc.SelectedValue = Arr(1)
            ddlDesc_SelectedIndexChanged(sender, e)
            If Arr(0) = "3" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Saved", 0, sTypeName, Arr(1), objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sSession.IPAddress)
                lblGeneralMasterDetailsValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                sGMBackStatus = 2
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As EventArgs) Handles imgbtnUpdate.Click
        Dim iMasterID As Integer
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            If txtDesc.Text.Trim = "" Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = "Enter " & sTypeName & "." : lblError.Text = "Enter " & sTypeName & "."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDesc.Text.Trim.Length > 100 Then
                txtDesc.Focus()
                lblGeneralMasterDetailsValidationMsg.Text = sTypeName & " exceeded maximum size(Max 100 characters)." : lblError.Text = sTypeName & " exceeded maximum size(Max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim = "" Then
                lblGeneralMasterDetailsValidationMsg.Text = "Enter Code." : lblError.Text = "Enter Code."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtCode.Text.Trim.Length > 10 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Code exceeded maximum size(max 10 characters)." : lblError.Text = "Code exceeded maximum size(max 10 characters)."
                txtCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            If txtNotes.Text.Trim.Length > 100 Then
                lblGeneralMasterDetailsValidationMsg.Text = "Note exceeded maximum size(max 100 characters)." : lblError.Text = "Note exceeded maximum size(max 100 characters)."
                txtNotes.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDesc.SelectedIndex > 0 Then
                iMasterID = ddlDesc.SelectedValue
                'To check Description Deleted or not
                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "Mas_Description", ddlDesc.SelectedValue, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckDeleteorNot(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "CMM_Desc", ddlDesc.SelectedValue, "OTHERS")
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "De-Activated description cannot be updated." : lblError.Text = "De-Activated description cannot be updated."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "Mas_Code", ddlDesc.SelectedValue, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "CMM_Code", ddlDesc.SelectedValue, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "Mas_Description", ddlDesc.SelectedValue, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "CMM_Desc", ddlDesc.SelectedValue, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered " & sTypeName & " Name already exist." : lblError.Text = "Entered " & sTypeName & " Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                iMasterID = 0
                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "Mas_Code", 0, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), sTableName, "CMM_Code", 0, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "This Code already exist." : lblError.Text = "This Code already exist."
                    txtCode.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "Mas_Description", 0, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sTableName, "CMM_Desc", 0, sMasterName)
                End If
                If bCheck = True Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Entered " & sTypeName & " Name already exist." : lblError.Text = "Entered " & sTypeName & " Name already exist."
                    txtDesc.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If UCase(sMasterName) = "DESG" Or UCase(sMasterName) = "ROLE" Then 'Designation & Role
                Arr = objclsAdminMaster.SaveOrUpdateDtls(sSession.AccessCode, sSession.AccessCodeID, iMasterID, objclsGRACeGeneral.SafeSQL(txtCode.Text.Trim), objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), objclsGRACeGeneral.SafeSQL(txtNotes.Text.Trim), sTableName, sSession.UserID, sSession.IPAddress)
            Else
                If iMasterID = 0 Then
                    objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                    objclsAdminMaster.sCode = sMasterName & "_" & objclsAdminMaster.iID
                    objclsAdminMaster.iID = 0
                Else
                    objclsAdminMaster.iID = ddlDesc.SelectedValue
                    objclsAdminMaster.sCode = objclsGRACeGeneral.SafeSQL(txtCode.Text)
                End If
                objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim)
                objclsAdminMaster.sCategory = sMasterName
                objclsAdminMaster.iRiskCategory = 0
                objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(txtNotes.Text.Trim)
                If chkComplianceTask.Checked = True Then
                    objclsAdminMaster.iKeyComponent = 1
                Else
                    objclsAdminMaster.iKeyComponent = 0
                End If
                objclsAdminMaster.sModule = "A"
                objclsAdminMaster.sDelflag = "W"
                objclsAdminMaster.sStatus = "U"
                If txtRate.Text <> "" Then
                    objclsAdminMaster.dcmmRate = txtRate.Text
                Else
                    objclsAdminMaster.dcmmRate = "0.0"
                End If

                If ddlAct.SelectedIndex >= 1 Then
                    objclsAdminMaster.sCMMAct = ddlAct.SelectedItem.Text
                Else
                    objclsAdminMaster.sCMMAct = ""
                End If

                objclsAdminMaster.sCMMHSNSAC = txtHSN.Text
                objclsAdminMaster.iCrBy = sSession.UserID
                objclsAdminMaster.iUpdatedBy = sSession.UserID
                objclsAdminMaster.sIpAddress = sSession.IPAddress
                objclsAdminMaster.iCompId = sSession.AccessCodeID
                Arr = objclsAdminMaster.SaveMasterDetails(sSession.AccessCode, objclsAdminMaster)
            End If
            BindDescDetails()
            ddlDesc.SelectedValue = Arr(1)
            ddlDesc_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Updated", 0, sTypeName, Arr(1), objclsGRACeGeneral.SafeSQL(txtDesc.Text.Trim), sSession.IPAddress)
                If sGMFlag = "W" Then
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                Else
                    lblGeneralMasterDetailsValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatusID As Object, oMasterID As Object
        Try
            lblError.Text = ""
            oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sGMBackStatus))
            oMasterID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iMasterIDFromDB))
            Response.Redirect(String.Format("~/Masters/GeneralMaster.aspx?StatusID={0}&MasterID={1}", oStatusID, oMasterID), False) 'Masters/GeneralMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnAddAct_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddAct.Click
        Try
            lblError.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAct').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnSavedetails_Click(sender As Object, e As EventArgs) Handles btnSavedetails.Click
        Dim dtAct As New DataTable
        Try
            lblError.Text = ""
            lblAct.Text = txtname.Text
            ddlAct.Items.Add(New ListItem(lblAct.Text))
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class


