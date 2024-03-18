Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WebForms
Partial Class GeneralMaster
    Inherits System.Web.UI.Page
    Private sFormName As String = "GeneralMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsCheckMasterIsInUse As New clsCheckMasterIsInUse
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    'Private Shared sSGMSave As String
    'Private Shared sSGMAD As String
    'Private Shared sSGMRpt As String
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
                imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
                imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
                'sSGMSave = "NO" : sSGMAD = "NO" : sSGMRpt = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MSGM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        sSGMSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Approve/Activate/De-Activate,") = True Then
                '        sSGMAD = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        sSGMRpt = "YES"
                '    End If
                'End If

                BindMainMaster() : BindStatus()
                If Request.QueryString("StatusID") IsNot Nothing Then
                    ddlStatus.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                If Request.QueryString("MasterID") IsNot Nothing Then
                    ddlMainMaster.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("MasterID")))
                    ddlMainMaster_SelectedIndexChanged(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindMainMaster()
        Try
            ddlMainMaster.Items.Add(New ListItem("Select Master", "0"))
            ddlMainMaster.Items.Add(New ListItem("Audit Check Point", "AP"))
            ddlMainMaster.Items.Add(New ListItem("Audit Task/Assignments", "AT")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Audit Sign Off", "ASF"))
            ddlMainMaster.Items.Add(New ListItem("Bill Type", "BT")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Conclusion", "WPC"))
            ddlMainMaster.Items.Add(New ListItem("Designation", "DESG")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Document Request List", "DRL")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Expenses Charges", "EC"))
            ddlMainMaster.Items.Add(New ListItem("Frequency", "FRE")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Industry Type", "IND")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Management", "MNG")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Non-Audit Task", "NAT"))
            ddlMainMaster.Items.Add(New ListItem("Organization Type", "ORG")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Other Expenses", "OE")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Reimbursement", "LE")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Role", "ROLE")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Type of Report", "TOR")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Type of Test", "TOT")) 'Used
            ddlMainMaster.Items.Add(New ListItem("Work Status", "WS"))
            ddlMainMaster.Items.Add(New ListItem("Tax Master", "TM"))
            ddlMainMaster.Items.Add(New ListItem("Unit of Measurement", "UM"))
            ddlMainMaster.Items.Add(New ListItem("Types of JE Transactions", "JE")) 'Journal Entry
            ddlMainMaster.Items.Add(New ListItem("Management Representations", "MR"))
            ddlMainMaster.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindMaster" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindGeneralMasterGridDetails(ByVal iPageIndex As Integer, ByVal sTableName As String, ByVal iStatus As Integer, ByVal sType As String)
        Dim dt As New DataTable
        Try
            If ddlMainMaster.SelectedValue = "DESG" Or ddlMainMaster.SelectedValue = "ROLE" Then
                dt = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, sTableName, iStatus, "")
            Else
                dt = objclsAdminMaster.LoadGeneralMasterOTHERGridDetails(sSession.AccessCode, sSession.AccessCodeID, sTableName, iStatus, "", sType)
            End If
            If ddlMainMaster.SelectedValue = "AT" Then
                gvGeneralMaster.Columns(4).Visible = True
                gvGeneralMaster.Columns(5).Visible = True
            Else
                gvGeneralMaster.Columns(4).Visible = False
                gvGeneralMaster.Columns(5).Visible = False
            End If
            gvGeneralMaster.DataSource = dt
            gvGeneralMaster.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindGeneralMasteGridDtails" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlMainMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMainMaster.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnWaiting.Visible = False
            imgbtnActivate.Visible = False : imgbtnDeActivate.Visible = False
            If ddlMainMaster.SelectedIndex > 0 Then
                If ddlMainMaster.SelectedIndex > 0 Then
                    'If sSGMRpt = "YES" Then
                    imgbtnReport.Visible = True
                    'End If
                    If ddlStatus.SelectedIndex = 0 Then
                        'If sSGMAD = "YES" Then
                        imgbtnDeActivate.Visible = True
                        'End If
                    ElseIf ddlStatus.SelectedIndex = 1 Then
                        'If sSGMAD = "YES" Then
                        imgbtnActivate.Visible = True
                        'End If
                    ElseIf ddlStatus.SelectedIndex = 2 Then
                        'If sSGMAD = "YES" Then
                        imgbtnWaiting.Visible = True
                        'End If
                    End If
                End If
            End If
            gvGeneralMaster.DataSource = Nothing
            gvGeneralMaster.DataBind()
            If ddlMainMaster.SelectedIndex > 0 Then
                If ddlMainMaster.SelectedValue = "DESG" Then
                    BindGeneralMasterGridDetails(0, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
                ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                    BindGeneralMasterGridDetails(0, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
                Else
                    BindGeneralMasterGridDetails(0, "Content_Management_Master", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMainMaster_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim oStatusID As New Object, oMasterName As New Object
        Try
            lblError.Text = ""
            If ddlMainMaster.SelectedIndex = 0 Then
                ddlMainMaster.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Master Type." : lblError.Text = "Select Master Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlStatus.SelectedIndex = 0 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            ElseIf ddlStatus.SelectedIndex = 1 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
            ElseIf ddlStatus.SelectedIndex = 2 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
            ElseIf ddlStatus.SelectedIndex = 3 Then
                oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(3))
            End If
            oMasterName = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlMainMaster.SelectedValue))
            Response.Redirect(String.Format("~/Masters/GeneralMasterDetails.aspx?StatusID={0}&MasterName={1}", oStatusID, oMasterName), False) 'GeneralMasterDetails
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStatus.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            If ddlMainMaster.SelectedIndex > 0 Then
                sMainMaster = ddlMainMaster.SelectedValue
            Else
                ddlMainMaster.Focus()
                lblGeneralMasterValidationMsg.Text = "Select Master Type." : lblError.Text = "Select Master Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ddlMainMaster_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim lblDescName As New Label
        Dim iIndx As Integer
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvGeneralMaster.Rows.Count - 1
                    chkField = gvGeneralMaster.Rows(iIndx).FindControl("chkSelect")
                    lblDescName = gvGeneralMaster.Rows(iIndx).FindControl("lblDescName")
                    Dim result As Boolean = False
                    If ddlMainMaster.SelectedValue = "DESG" Then
                        result = arrListDesignation.Contains(lblDescName.Text.ToLower())
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        result = arrListRole.Contains(lblDescName.Text.ToLower())
                    ElseIf ddlMainMaster.SelectedValue = "WS" Then
                        result = arrListWS.Contains(lblDescName.Text.ToLower())
                    ElseIf ddlMainMaster.SelectedValue = "FRE" Then
                        result = arrListFRE.Contains(lblDescName.Text.ToLower())
                    ElseIf ddlMainMaster.SelectedValue = "IND" Then
                        result = arrListIND.Contains(lblDescName.Text.ToLower())
                    ElseIf ddlMainMaster.SelectedValue = "ORG" Then
                        result = arrListORG.Contains(lblDescName.Text.ToLower())
                    ElseIf ddlMainMaster.SelectedValue = "DRL" Then
                        result = arrListDRL.Contains(lblDescName.Text.ToLower())
                    End If
                    If result = False Then
                        chkField.Checked = True
                    End If
                Next
            Else
                For iIndx = 0 To gvGeneralMaster.Rows.Count - 1
                    chkField = gvGeneralMaster.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvGeneralMaster_PreRender(sender As Object, e As EventArgs) Handles gvGeneralMaster.PreRender
        Dim dt As New DataTable
        Try
            If gvGeneralMaster.Rows.Count > 0 Then
                gvGeneralMaster.UseAccessibleHeader = True
                gvGeneralMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvGeneralMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvGeneralMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvGeneralMaster.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Dim chkSelect As New CheckBox
        Dim lblDescName As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                chkSelect = CType(e.Row.FindControl("chkSelect"), CheckBox)
                lblDescName = CType(e.Row.FindControl("lblDescName"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"

                'If sSGMAD = "YES" Then
                gvGeneralMaster.Columns(0).Visible = True
                'End If
                gvGeneralMaster.Columns(7).Visible = False
                gvGeneralMaster.Columns(8).Visible = False

                If ddlStatus.SelectedIndex = 0 Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                    'If sSGMAD = "YES" Then
                    gvGeneralMaster.Columns(7).Visible = True
                    'End If
                    gvGeneralMaster.Columns(8).Visible = True
                End If

                If ddlStatus.SelectedIndex = 1 Then
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                    'If sSGMAD = "YES" Then
                    gvGeneralMaster.Columns(7).Visible = True
                    'End If
                End If

                If ddlStatus.SelectedIndex = 2 Then
                    imgbtnStatus.ImageUrl = "~/Images/Checkmark16.png" : imgbtnStatus.ToolTip = "Approve"
                    'If sSGMAD = "YES" Then
                    gvGeneralMaster.Columns(7).Visible = True
                    'End If
                    gvGeneralMaster.Columns(8).Visible = True
                End If

                If ddlStatus.SelectedIndex = 3 Then
                    gvGeneralMaster.Columns(0).Visible = False
                End If

                Dim result As Boolean = False
                If ddlMainMaster.SelectedValue = "DESG" Then
                    result = arrListDesignation.Contains(lblDescName.Text.ToLower())
                ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                    result = arrListRole.Contains(lblDescName.Text.ToLower())
                ElseIf ddlMainMaster.SelectedValue = "WS" Then
                    result = arrListWS.Contains(lblDescName.Text.ToLower())
                ElseIf ddlMainMaster.SelectedValue = "FRE" Then
                    result = arrListFRE.Contains(lblDescName.Text.ToLower())
                ElseIf ddlMainMaster.SelectedValue = "IND" Then
                    result = arrListIND.Contains(lblDescName.Text.ToLower())
                ElseIf ddlMainMaster.SelectedValue = "ORG" Then
                    result = arrListORG.Contains(lblDescName.Text.ToLower())
                ElseIf ddlMainMaster.SelectedValue = "DRL" Then
                    result = arrListDRL.Contains(lblDescName.Text.ToLower())
                End If
                If result = True Then
                    chkSelect.Enabled = False
                    imgbtnStatus.Enabled = False
                    imgbtnEdit.Enabled = False
                Else
                    chkSelect.Enabled = True
                    imgbtnStatus.Enabled = True
                    imgbtnEdit.Enabled = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvGeneralMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvGeneralMaster.RowCommand
        Dim oStatusID As Object, oMasterID As Object, oMasterName As Object
        Dim lblDescID As New Label, lblDescName As New Label
        Dim sMainMaster As String
        Try
            lblError.Text = "" : sMainMaster = ""
            If ddlMainMaster.SelectedIndex > 0 Then
                sMainMaster = ddlMainMaster.SelectedValue
            End If
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblDescID = DirectCast(clickedRow.FindControl("lblDescID"), Label)
            If e.CommandName = "EditRow" Then
                oMasterID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblDescID.Text)))
                If ddlStatus.SelectedIndex = 0 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                ElseIf ddlStatus.SelectedIndex = 1 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                ElseIf ddlStatus.SelectedIndex = 2 Then
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(2))
                Else
                    oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
                End If
                oMasterID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblDescID.Text)))
                oMasterName = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlMainMaster.SelectedValue))
                Response.Redirect(String.Format("~/Masters/GeneralMasterDetails.aspx?StatusID={0}&MasterID={1}&MasterName={2}", oStatusID, oMasterID, oMasterName), False) 'GeneralMasterDetails
            End If
            If e.CommandName = "Status" Then
                If ddlStatus.SelectedIndex = 0 Then
                    If ddlMainMaster.SelectedItem.Text = "Designation" Then
                        If objclsCheckMasterIsInUse.CheckGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Sad_UserDetails", "usr_Designation", "Usr_CompId") = True Then
                            lblGeneralMasterValidationMsg.Text = "Already tag to some User, can't be De-Activated" : lblError.Text = "Already tag to some User, can't be De-Activate"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                            Exit Sub
                        End If
                    ElseIf ddlMainMaster.SelectedItem.Text = "Role" Then
                        If objclsCheckMasterIsInUse.CheckGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Sad_UserDetails", "usr_levelgrp", "Usr_CompId") = True Then
                            lblGeneralMasterValidationMsg.Text = "Already tag to some User, can't be De-Activate." : lblError.Text = "Already tag to some User, can't be De-Activate"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                            Exit Sub
                        End If
                    End If

                    If ddlMainMaster.SelectedValue = "DESG" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPDESGN_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "D", "DESGROLE")
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPORLVL_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "D", "DESGROLE")
                    Else
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "D", "OTHERS")
                    End If
                    lblGeneralMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "De-Activated", ddlMainMaster.SelectedIndex, ddlMainMaster.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 1 Then
                    If ddlMainMaster.SelectedValue = "DESG" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPDESGN_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "A", "DESGROLE")
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPORLVL_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "A", "DESGROLE")
                    Else
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "A", "OTHERS")
                    End If
                    lblGeneralMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Activated", ddlMainMaster.SelectedIndex, ddlMainMaster.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlStatus.SelectedIndex = 2 Then
                    If ddlMainMaster.SelectedValue = "DESG" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPDESGN_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "W", "DESGROLE")
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPORLVL_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "W", "DESGROLE")
                    Else
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "W", "OTHERS")
                    End If
                    lblGeneralMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Approved", ddlMainMaster.SelectedIndex, ddlMainMaster.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
                If ddlMainMaster.SelectedValue = "DESG" Then
                    BindGeneralMasterGridDetails(0, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
                ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                    BindGeneralMasterGridDetails(0, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
                Else
                    BindGeneralMasterGridDetails(0, "Content_Management_Master", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvGeneralMaster.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to Activate." : lblError.Text = "No data to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to Activate." : lblError.Text = "Select Name to Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
            End If
NextSave:   For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                lblDescID = gvGeneralMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    If ddlMainMaster.SelectedValue = "DESG" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPDESGN_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "A", "DESGROLE")
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPORLVL_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "A", "DESGROLE")
                    Else
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "A", "OTHERS")
                    End If
                    lblGeneralMasterValidationMsg.Text = "Successfully Activated." : lblError.Text = "Successfully Activated."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Activated", ddlMainMaster.SelectedIndex, ddlMainMaster.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            If ddlMainMaster.SelectedValue = "DESG" Then
                BindGeneralMasterGridDetails(0, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                BindGeneralMasterGridDetails(0, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            Else
                BindGeneralMasterGridDetails(0, "Content_Management_Master", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnDeActivate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDeActivate.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer, iCheck As Integer = 0
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvGeneralMaster.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to De-Activate." : lblError.Text = "No data to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to De-Activate." : lblError.Text = "Select Name to De-Activate."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                lblDescID = gvGeneralMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    If ddlMainMaster.SelectedValue = "DESG" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPDESGN_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "D", "DESGROLE")
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPORLVL_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "D", "DESGROLE")
                    Else
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "D", "OTHERS")
                    End If
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "De-Activated", ddlMainMaster.SelectedIndex, ddlMainMaster.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    lblGeneralMasterValidationMsg.Text = "Successfully De-Activated." : lblError.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            If ddlMainMaster.SelectedValue = "DESG" Then
                BindGeneralMasterGridDetails(0, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                BindGeneralMasterGridDetails(0, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            Else
                BindGeneralMasterGridDetails(0, "Content_Management_Master", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDeActivate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If gvGeneralMaster.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No data to Approve." : lblError.Text = "No data To Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                If chkSelect.Checked = True Then
                    iCount = 1
                    GoTo NextSave
                End If
            Next
            If iCount = 0 Then
                lblGeneralMasterValidationMsg.Text = "Select Name to Approve." : lblError.Text = "Select Name to Approve."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
NextSave:   For i = 0 To gvGeneralMaster.Rows.Count - 1
                chkSelect = gvGeneralMaster.Rows(i).FindControl("chkSelect")
                lblDescID = gvGeneralMaster.Rows(i).FindControl("lblDescID")
                If chkSelect.Checked = True Then
                    If ddlMainMaster.SelectedValue = "DESG" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPDESGN_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "W", "DESGROLE")
                    ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "SAD_GRPORLVL_GENERAL_MASTER", sSession.UserID, sSession.IPAddress, "W", "DESGROLE")
                    Else
                        objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, lblDescID.Text, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "W", "OTHERS")
                    End If
                    lblGeneralMasterValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Approved", ddlMainMaster.SelectedIndex, ddlMainMaster.SelectedItem.Text, lblDescID.Text, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterValidation').modal('show');", True)
                End If
            Next
            If ddlMainMaster.SelectedValue = "DESG" Then
                BindGeneralMasterGridDetails(0, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                BindGeneralMasterGridDetails(0, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            Else
                BindGeneralMasterGridDetails(0, "Content_Management_Master", ddlStatus.SelectedIndex, ddlMainMaster.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            If ddlMainMaster.SelectedValue = "DESG" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            ElseIf ddlMainMaster.SelectedValue = "DRL" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDRLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlStatus.SelectedIndex, "", ddlMainMaster.SelectedValue)
            Else
                dtdetails = objclsAdminMaster.LoadGeneralMasterOTHERGridDetails(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", ddlStatus.SelectedIndex, "", ddlMainMaster.SelectedValue)
            End If
            If dtdetails.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            If ddlMainMaster.SelectedValue = "DRL" Then
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMasterDRL.rdlc")
            Else
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMaster.rdlc")
            End If
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("GM(" + ddlMainMaster.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            If ddlMainMaster.SelectedValue = "DESG" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPDESGN_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            ElseIf ddlMainMaster.SelectedValue = "ROLE" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDESGROLEGridDetails(sSession.AccessCode, sSession.AccessCodeID, "SAD_GRPORLVL_GENERAL_MASTER", ddlStatus.SelectedIndex, "")
            ElseIf ddlMainMaster.SelectedValue = "DRL" Then
                dtdetails = objclsAdminMaster.LoadGeneralMasterDRLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlStatus.SelectedIndex, "", ddlMainMaster.SelectedValue)
            Else
                dtdetails = objclsAdminMaster.LoadGeneralMasterOTHERGridDetails(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", ddlStatus.SelectedIndex, "", ddlMainMaster.SelectedValue)
            End If
            If dtdetails.Rows.Count = 0 Then
                lblGeneralMasterValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalGeneralMasterValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            If ddlMainMaster.SelectedValue = "DRL" Then
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMasterDRL.rdlc")
            Else
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GeneralMaster.rdlc")
            End If
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("GM(" + ddlMainMaster.SelectedItem.Text + ")", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class
