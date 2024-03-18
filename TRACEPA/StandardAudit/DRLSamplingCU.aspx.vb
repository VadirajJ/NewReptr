Imports System
Imports System.Data
Imports BusinesLayer
Partial Class DRLSamplingCU
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_DRLSamplingCU"
    Private sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAttachments As New clsAttachments
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsDRLLog As New clsDRLLog
    Private objclsStandardAudit As New clsStandardAudit
    Private Shared iAttachID As Integer
    Private Shared sEmail As String
    Private Shared iDocID As Integer
    Private Shared iCustID As Integer = 0
    Private Shared iCheckPointID As Integer = 0
    Private Shared iSelectedCheckPointID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iAttachID = 0
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomerName(sender, e) : BindAllAuditNo() : BindStatus()
                DRLClientSideValidation()
                txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                txtRequestedOn.Enabled = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindScheduledDetails(ByVal iAuditID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            If dt.Rows.Count = 1 Then
                If ddlAuditNo.SelectedIndex = 0 Then
                    ddlAuditNo.SelectedValue = iAuditID
                End If
                ddlFinancialYear.SelectedValue = dt.Rows(0)("SA_YearID")
                iCustID = dt.Rows(0)("SA_CustID")
                ddlCustomerName.SelectedValue = iCustID
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsStandardAudit.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.YearID
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ddlCustomerName_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub DRLClientSideValidation()
        Try
            RFVAuditNo.InitialValue = "Select Audit No" : RFVAuditNo.ErrorMessage = "Select Audit No."
            RFVRequestedOn.ControlToValidate = "txtRequestedOn" : RFVRequestedOn.ErrorMessage = "Enter Received On"
            RFVStatus.InitialValue = "0" : RFVStatus.ErrorMessage = "Select Status."
            REVRequestedOn.ErrorMessage = "Enter valid Date." : REVRequestedOn.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            RFVComment.ControlToValidate = "txtComment" : RFVComment.ErrorMessage = "Enter Received Comments."
            REVComment.ErrorMessage = "Received Comment exceeded maximum size(max 5000 characters)." : REVComment.ValidationExpression = "^[\s\S]{0,5000}$"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DRLClientSideValidation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindAllAuditNo()
        Dim iCustID As Integer = 0
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustID = ddlCustomerName.SelectedValue
            End If
            ddlAuditNo.DataSource = objclsStandardAudit.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, sSession.UserID, True)
            ddlAuditNo.DataTextField = "SA_AuditNo"
            ddlAuditNo.DataValueField = "SA_ID"
            ddlAuditNo.DataBind()
            ddlAuditNo.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAuditNo" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindCustomerName(sender As Object, e As EventArgs)
        Try
            ddlCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "CUST_Name"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer Name")
            If sSession.UserLoginCustID > 0 Then
                ddlCustomerName.SelectedValue = sSession.UserLoginCustID
                ddlCustomerName.Enabled = False
                ddlCustomerName_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomerName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub BindStatus()
        Try
            ddlStatus.Items.Add(New ListItem("Select Status", "0"))
            ddlStatus.Items.Add(New ListItem("Outstanding", "1"))
            ddlStatus.Items.Add(New ListItem("Acceptable", "2"))
            ddlStatus.Items.Add(New ListItem("Partially", "3"))
            ddlStatus.Items.Add(New ListItem("No", "4"))
            ddlStatus.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnUpdate.Visible = False
            lblAuditType.Text = ""
            lblCheckPointId.Text = 0
            lblCheckPoint.Text = ""
            lblDocumentRequestedListId.Text = 0
            lblDocumentRequestedList.Text = ""
            txtComment.Text = "" : txtRequestedOn.Text = ""
            txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
            txtRequestedOn.Enabled = False
            ddlStatus.SelectedIndex = 0
            gvDRLLog.DataSource = Nothing
            gvDRLLog.DataBind()
            BindAllAuditNo()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Dim iDocumentRequestedList As Integer = 0
        Try
            lblError.Text = ""
            imgbtnUpdate.Visible = True
            lblAuditType.Text = ""
            lblCheckPointId.Text = 0
            lblCheckPoint.Text = ""
            lblDocumentRequestedListId.Text = 0
            lblDocumentRequestedList.Text = ""
            txtComment.Text = "" : txtRequestedOn.Text = ""
            txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
            txtRequestedOn.Enabled = False
            ddlStatus.SelectedIndex = 0
            gvDRLLog.DataSource = Nothing
            gvDRLLog.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                BindScheduledDetails(ddlAuditNo.SelectedValue)
                BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadDRLDetails(ByVal iCustID As Integer, ByVal iTaskID As Integer, ByVal iChkPointID As Integer, ByVal iDRListID As Integer)
        Dim dt As New DataTable
        Try
            txtComment.Text = "" : txtRequestedOn.Text = ""
            txtRequestedOn.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
            txtRequestedOn.Enabled = False
            DRLClientSideValidation()
            dt = objclsDRLLog.GetDRLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iTaskID, iChkPointID, iCustID, iDRListID)
            If dt.Rows.Count > 0 Then
                imgbtnUpdate.Visible = True
                lblCheckPointId.Text = 0 : lblCheckPoint.Text = "Others"
                lblDocumentRequestedListId.Text = 0 : lblDocumentRequestedList.Text = "NA"

                DRLClientSideValidation()
                If IsDBNull(dt.Rows(0)("ADRL_FunID")) = False Then
                    lblCheckPointId.Text = dt.Rows(0)("ADRL_FunID")
                End If
                If IsDBNull(dt.Rows(0)("ACM_Checkpoint")) = False Then
                    lblCheckPoint.Text = dt.Rows(0)("ACM_Checkpoint")
                End If
                If IsDBNull(dt.Rows(0)("CMM_Desc")) = False Then
                    lblDocumentRequestedList.Text = dt.Rows(0)("CMM_Desc")
                End If
                If IsDBNull(dt.Rows(0)("ADRL_RequestedListID")) = False Then
                    lblDocumentRequestedListId.Text = dt.Rows(0)("ADRL_RequestedListID")
                End If
                If IsDBNull(dt.Rows(0)("ADRL_EmailID")) = False Then
                    sEmail = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_EmailID"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_ReceivedComments")) = False Then
                    txtComment.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_ReceivedComments"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_ReceivedOn")) = False Then
                    txtRequestedOn.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("ADRL_ReceivedOn"))
                End If
                If IsDBNull(dt.Rows(0)("ADRL_LogStatus")) = False Then
                    ddlStatus.SelectedValue = dt.Rows(0)("ADRL_LogStatus")
                End If
                If IsDBNull(dt.Rows(0)("ADRL_TimlinetoResOn")) = False Then
                    txttimeline.Text = dt.Rows(iCheckPointID)("ADRL_TimlinetoResOn")
                End If
                iAttachID = 0 : lblBadgeCount.Text = 0
                dgAttach.DataSource = Nothing
                dgAttach.DataBind()
                If IsDBNull(dt.Rows(0).Item("ADRL_AttachID")) = False Then
                    iAttachID = dt.Rows(0).Item("ADRL_AttachID")
                    BindAllAttachments(sSession.AccessCode, iAttachID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDRLDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindDRLLogDetails(ByVal iCustID As Integer, ByVal iAuditNo As Integer)
        Dim dt As DataTable
        Try
            dt = objclsDRLLog.LoadDRLdg(sSession.AccessCode, sSession.AccessCodeID, iAuditNo, "", iCustID, ddlFinancialYear.SelectedValue, 0, 1)
            gvDRLLog.DataSource = dt
            gvDRLLog.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDRLLogDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim objDRLLog As New str_DRLLog
        Dim Array() As String
        Dim iDRLPKID As Integer = 0
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblDRLLogDetailsValidationMsg.Text = "Select Customer Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                ddlCustomerName.Focus()
                Exit Try
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblDRLLogDetailsValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Try
            End If

            If objclsDRLLog.CheckCheckPointDRL(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, Val(lblCheckPointId.Text), Val(lblDocumentRequestedListId.Text)) > 0 Then
                iDRLPKID = objclsDRLLog.GetDRLPKID(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue, Val(lblCheckPointId.Text), Val(lblDocumentRequestedListId.Text), 0)
                If iDRLPKID = 0 Then
                    objDRLLog.iADRL_ID = 0
                    objDRLLog.sADRL_Comments = objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim)
                Else
                    objDRLLog.iADRL_ID = iDRLPKID
                    objDRLLog.sADRL_Comments = objclsDRLLog.GetComment(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iDRLPKID)
                End If
                objDRLLog.iADRL_YearID = ddlFinancialYear.SelectedValue
                objDRLLog.iADRL_AuditNo = ddlAuditNo.SelectedValue
                objDRLLog.iADRL_FunID = Val(lblCheckPointId.Text)
                objDRLLog.iADRL_CustID = ddlCustomerName.SelectedValue
                objDRLLog.iADRL_RequestedListID = Val(lblDocumentRequestedListId.Text)
                objDRLLog.iADRL_RequestedTypeID = 0
                objDRLLog.sADRL_RequestedOn = txtRequestedOn.Text
                objDRLLog.sADRL_TimlinetoResOn = txttimeline.Text
                objDRLLog.sADRL_EmailID = objclsGRACeGeneral.SafeSQL(sEmail)
                objDRLLog.iADRL_CrBy = sSession.UserID
                objDRLLog.iADRL_UpdatedBy = sSession.UserID
                objDRLLog.sADRL_IPAddress = sSession.IPAddress
                objDRLLog.iADRL_CompID = sSession.AccessCodeID

                Array = objclsDRLLog.SaveDRLLogReceivedList_Details(sSession.AccessCode, objDRLLog)
                objclsDRLLog.UpdateDRLLogDetails(sSession.AccessCode, sSession.AccessCodeID, iDRLPKID, ddlFinancialYear.SelectedValue, iAttachID, txtRequestedOn.Text, ddlStatus.SelectedValue, objclsGRACeGeneral.SafeSQL(txtComment.Text.Trim), sSession.UserID)
                objclsDRLLog.updateStandardAudit_ConductAudit_RemarksHistory(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, objDRLLog.iADRL_FunID, txtComment.Text, sSession.UserID, sSession.IPAddress, objDRLLog.sADRL_EmailID)
                objclsDRLLog.updateStandardAudit_Audit_DRLLog_RemarksUserHistory(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, objDRLLog.iADRL_FunID, txtComment.Text, sSession.UserID, sSession.IPAddress, objDRLLog.sADRL_EmailID, ddlFinancialYear.SelectedValue)
                If Array(0) = 2 Then
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Audit", "DRL Log", "Updated", ddlAuditNo.SelectedValue, ddlAuditNo.SelectedItem.Text, 0, "", sSession.IPAddress)
                    lblError.Text = "Successfully Updated." : lblDRLLogDetailsValidationMsg.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalDRLLogDetailsValidation').modal('show');", True)
                End If
            End If
            BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDRLLog_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDRLLog.RowDataBound
        Dim imgbtnAttachment As New ImageButton
        Dim lblAttachID As New Label, lblDocumentRequestedList As New Label
        Dim lnkDocumentRequestedList As New LinkButton
        Dim ds As DataTable
        Dim lblCheckPoint As New Label, lblCheckPointId As New Label
        Dim lblBadgeCountgv As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnAttachment = CType(e.Row.FindControl("imgbtnAttachment"), ImageButton)
                lblCheckPointId = CType(e.Row.FindControl("lblCheckPointId"), Label)
                lblAttachID = CType(e.Row.FindControl("lblAttachID"), Label)
                lblCheckPoint = CType(e.Row.FindControl("lblCheckPoint"), Label)
                lblBadgeCountgv = CType(e.Row.FindControl("lblBadgeCountgv"), Label)
                lblBadgeCountgv.Text = 0
                If Val(lblAttachID.Text) > 0 Then
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, lblAttachID.Text)
                    lblBadgeCountgv.Text = iCount
                Else
                    Dim iRowAttachID As Integer = objclsStandardAudit.GetDRLAttachmentID(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointId.Text))
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, iRowAttachID)
                    lblBadgeCount.Text = iCount
                End If
                If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                    imgbtnAttachment.ImageUrl = "~/Images/Attachment16.png"
                    imgbtnAttachment.Visible = True
                    If lblAttachID.Text > 0 Then
                        lnkDocumentRequestedList.Visible = False
                        lblDocumentRequestedList.Visible = True
                    End If
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLLog_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnAttachment_Click(sender As Object, e As EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAttachment_Click")
        End Try
    End Sub
    Private Sub gvDRLLog_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDRLLog.RowCommand
        Dim lblDRLID As New Label, lblAttachID As New Label, lblCPID As New Label, lblDocID As New Label, lblDocReqListID As New Label, lblStatus As New Label
        Dim lblCheckPointID As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                imgbtnUpdate.Visible = True
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCPID = CType(clickedRow.FindControl("lblCPID"), Label)
                lblDocReqListID = CType(clickedRow.FindControl("lblDocReqListID"), Label)
                lblDocumentRequestedListId.Text = Val(lblDocReqListID.Text)
                If ddlAuditNo.SelectedIndex > 0 And ddlCustomerName.SelectedIndex > 0 Then
                    lblCheckPointId.Text = Val(lblCPID.Text)
                    LoadDRLDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, Val(lblCheckPointId.Text), Val(lblDocumentRequestedListId.Text))
                    txtRequestedOn.Enabled = False
                    imgbtnAttachment.Visible = True : lblBadgeCount.Visible = True : btnAddAttch.Visible = True : txtfile.Visible = True : lblBrowse.Visible = True : lblSize.Visible = True
                    iDocID = 0 : lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                End If
            End If
            If e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAttachID = CType(clickedRow.FindControl("lblAttachID"), Label)
                lblCPID = CType(clickedRow.FindControl("lblCPID"), Label)
                lblCheckPointID.Text = Val(lblCPID.Text)
                iSelectedCheckPointID = 0 : iSelectedCheckPointID = Val(lblCheckPointID.Text)
                lblDocReqListID = CType(clickedRow.FindControl("lblDocReqListID"), Label)
                lblDocumentRequestedListId.Text = Val(lblDocReqListID.Text)
                If iAttachID = 0 Then
                    iAttachID = objclsStandardAudit.GetConductAuditAttachmentID(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iSelectedCheckPointID)
                End If
                If ddlCustomerName.SelectedIndex > 0 And ddlAuditNo.SelectedIndex > 0 And Val(lblCheckPointID.Text) > 0 Then
                    LoadDRLDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), Val(lblDocumentRequestedListId.Text))
                    txtComment.Text = ""
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
            End If
            If e.CommandName = "HistoryAR" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                Dim dt As New DataTable
                dt = objclsStandardAudit.LoadSelectedStandardAuditCheckPointRemarksHistoryUserDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), 1)
                If dt.Rows.Count > 0 Then
                    gvHistory.DataSource = dt
                    gvHistory.DataBind()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myHistoryModal').modal('show')", True)
                Else
                    lblError.Text = "History not available." : lblDRLLogDetailsValidationMsg.Text = "History not available."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLLog_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            dgAttach.PageSize = 1000
            ds = objclsAttachments.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
                dgAttach.AllowPaging = True
            Else
                dgAttach.AllowPaging = False
            End If
            dgAttach.DataSource = ds
            dgAttach.DataBind()
            lblBadgeCount.Text = dgAttach.Items.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub btnaddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblMsg.Text = "" : iDocID = 0
            If Not (txtfile.PostedFile Is Nothing) And txtfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                    Exit Sub
                End If
                lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtfile.PostedFile.FileName)

                sFullFilePath = sPaths & sFilesNames
                txtfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iAttachID > 0 Then
                        BindAllAttachments(sSession.AccessCode, iAttachID)
                    End If
                    objclsStandardAudit.SaveDRLConductAuditAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iAttachID, ddlAuditNo.SelectedValue, iSelectedCheckPointID, "DRL")
                Else
                    lblMsg.Text = "No file to Attach."
                    Exit Try
                End If
            Else
                lblMsg.Text = "No file to Attach."
                Exit Try
            End If
            BindDRLLogDetails(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnaddAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgAttach.ItemDataBound
        Dim lblStatus As New Label
        Dim imgbtnAdd As New ImageButton, imgbtnRemove As New ImageButton
        Try
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnAdd = CType(e.Item.FindControl("imgbtnAdd"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnRemove = CType(e.Item.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub dgAttach_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
            End If
            If e.CommandName = "ADDDESC" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                lblFDescription = e.Item.FindControl("lblFDescription")
                lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub btnAddDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDesc.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If txtDescription.Text.Trim.Length > 1000 Then
                lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                Exit Try
            End If
            objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
            lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
            iDocID = 0
            BindAllAttachments(sSession.AccessCode, iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDRLLog_PreRender(sender As Object, e As EventArgs) Handles gvDRLLog.PreRender
        Dim dt As New DataTable
        Try
            If gvDRLLog.Rows.Count > 0 Then
                gvDRLLog.UseAccessibleHeader = True
                gvDRLLog.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDRLLog.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLLog_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub gvDRLLog_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles gvDRLLog.RowCancelingEdit

    End Sub

    Private Sub btnAddAttch_Command(sender As Object, e As CommandEventArgs) Handles btnAddAttch.Command

    End Sub
End Class