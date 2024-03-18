Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.Mail

Public Class ConductAudit
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_ConductAudit"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditChecklist As New clsAuditChecklist
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsAttachments As New clsAttachments
    Private objclsDRLLog As New clsDRLLog
    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iSelectedCheckPointID As Integer
    Private Shared iAttachID As Integer
    Private Shared iPartnerRemarksType As Integer
    Private Shared iReviewPartnerRemarksType As Integer
    Private Shared iAdditionalEmpRemarksType As Integer
    Private Shared iDocID As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnSubmit.ImageUrl = "~/Images/Submit24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                imgbtnBack.Visible = False
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomers() : BindAuditNo(0)
                If Request.QueryString("AuditID") IsNot Nothing Then
                    imgbtnBack.Visible = True
                    ddlAuditNo.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                    If ddlAuditNo.SelectedIndex > 0 Then
                        ddlAuditNo_SelectedIndexChanged(sender, e)
                    End If
                ElseIf sSession.AuditCodeID > 0 Then
                    ddlAuditNo.SelectedValue = sSession.AuditCodeID
                    If ddlAuditNo.SelectedIndex > 0 Then
                        ddlAuditNo_SelectedIndexChanged(sender, e)
                    End If
                End If
                RFVAuditNo.InitialValue = "Select Audit No" : RFVAuditNo.ErrorMessage = "Select Audit No."

                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oCustID As Object
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                oCustID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlCustomerName.SelectedValue))
            Else
                oCustID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            End If
            Response.Redirect(String.Format("~/StandardAudit/DashboardAndSchedule.aspx?CustID={0}", oCustID))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindCustomers()
        Try
            ddlCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "CUST_Name"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAuditNo(ByVal iCustID As Integer)
        Try
            ddlAuditNo.DataSource = objclsStandardAudit.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, sSession.UserID, bLoginUserIsPartner)
            ddlAuditNo.DataTextField = "SA_AuditNo"
            ddlAuditNo.DataValueField = "SA_ID"
            ddlAuditNo.DataBind()
            ddlAuditNo.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditNo" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim iCustID As Integer = 0
        Try
            lblError.Text = "" : lblAuditType.Text = ""
            iAttachID = 0 : iSelectedCheckPointID = 0
            gvCheckPoint.DataSource = Nothing
            gvCheckPoint.DataBind()
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustID = ddlCustomerName.SelectedValue
            End If
            BindAuditNo(iCustID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Try
            lblError.Text = "" : lblAuditType.Text = ""
            iAttachID = 0 : iSelectedCheckPointID = 0
            gvCheckPoint.DataSource = Nothing
            gvCheckPoint.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                BindScheduledDetails(ddlAuditNo.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindScheduledDetails(ByVal iAuditID As Integer)
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            iPartnerRemarksType = 0 : iReviewPartnerRemarksType = 0 : iAdditionalEmpRemarksType = 0
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            If dt.Rows.Count = 1 Then
                ddlCustomerName.SelectedValue = dt.Rows(0)("SA_CustID")
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
                Dim sPartnerID As String = If(Not dt.Rows(0)("SA_PartnerID").ToString().StartsWith(","), ",", "") & dt.Rows(0)("SA_PartnerID").ToString() & If(Not dt.Rows(0)("SA_PartnerID").ToString().EndsWith(","), ",", "")
                If sPartnerID.Contains("," & sSession.UserID & ",") Then
                    iPartnerRemarksType = 1
                End If
                Dim sAdditionalSupportEmployeeID As String = If(Not dt.Rows(0)("SA_AdditionalSupportEmployeeID").ToString().StartsWith(","), ",", "") & dt.Rows(0)("SA_AdditionalSupportEmployeeID").ToString() & If(Not dt.Rows(0)("SA_AdditionalSupportEmployeeID").ToString().EndsWith(","), ",", "")
                If sAdditionalSupportEmployeeID.Contains("," & sSession.UserID & ",") Then
                    iAdditionalEmpRemarksType = 1
                End If
                Dim sReviewPartnerID As String = If(Not dt.Rows(0)("SA_ReviewPartnerID").ToString().StartsWith(","), ",", "") & dt.Rows(0)("SA_ReviewPartnerID").ToString() & If(Not dt.Rows(0)("SA_ReviewPartnerID").ToString().EndsWith(","), ",", "")
                If sReviewPartnerID.Contains("," & sSession.UserID & ",") Then
                    iReviewPartnerRemarksType = 1
                End If
            End If
            dt1 = objclsStandardAudit.LoadSelectedStandardAuditCheckPointDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID, sSession.UserID, bLoginUserIsPartner)
            gvCheckPoint.DataSource = dt1
            gvCheckPoint.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCheckPoint_PreRender(sender As Object, e As EventArgs) Handles gvCheckPoint.PreRender
        Try
            If gvCheckPoint.Rows.Count > 0 Then
                gvCheckPoint.UseAccessibleHeader = True
                gvCheckPoint.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCheckPoint.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCheckPoint_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvCheckPoint_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCheckPoint.RowDataBound
        Dim lblAttachmentID As New Label, lblBadgeCount As New Label, lblTestResult As New Label, lblCheckPointID As New Label
        Dim imgbtnUploadObservations As New ImageButton, ddlTestResult As New DropDownList
        Dim chkAnnexure As New CheckBox
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblCheckPointID = CType(e.Row.FindControl("lblCheckPointID"), Label)
                lblAttachmentID = CType(e.Row.FindControl("lblAttachmentID"), Label)
                lblBadgeCount = CType(e.Row.FindControl("lblBadgeCount"), Label)
                chkAnnexure = CType(e.Row.FindControl("chkAnnexure"), CheckBox)
                imgbtnUploadObservations = CType(e.Row.FindControl("imgbtnUploadObservations"), ImageButton)
                imgbtnUploadObservations.Visible = False

                lblTestResult = CType(e.Row.FindControl("lblTestResult"), Label)
                ddlTestResult = CType(e.Row.FindControl("ddlTestResult"), DropDownList)
                If Val(lblTestResult.Text) > 0 Then
                    ddlTestResult.SelectedValue = Val(lblTestResult.Text)
                End If
                If chkAnnexure.Checked = True Then
                    imgbtnUploadObservations.Visible = True
                    imgbtnUploadObservations.ImageUrl = "~/Images/ExcelSA.jpg"
                End If
                lblBadgeCount.Text = 0
                If Val(lblAttachmentID.Text) > 0 Then
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, Val(lblAttachmentID.Text))
                    lblBadgeCount.Text = iCount
                Else
                    Dim iRowAttachID As Integer = objclsStandardAudit.GetDRLAttachmentID(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text))
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, iRowAttachID)
                    lblBadgeCount.Text = iCount
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignmentSubTask_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCheckPoint_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCheckPoint.RowCommand
        Dim lblAuditID As New Label, lblCheckPointID As New Label, lblAttachmentID As New Label, lblCheckPoint As New Label
        Dim lblRemarks As New Label, lblReviewerRemarks As New Label, lblConductAuditCheckPointPKId As New Label
        Dim oFYID As Object, oAuditID As Object, oCheckPointID As Object
        Dim txtRemarks As New TextBox, txtReviewerRemarks As New TextBox, ddlTestResult As DropDownList
        Dim chkAnnexure As New CheckBox, chkAuditorSendMail As New CheckBox, chkReviewerSendMail As New CheckBox
        Dim iAnnexure As Integer
        Try
            lblError.Text = ""
            If e.CommandName = "Upload" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                oFYID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlFinancialYear.SelectedValue))
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAuditID.Text)))
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblCheckPointID.Text)))
                Response.Redirect(String.Format("~/StandardAudit/UploadObservations.aspx?FYID={0}&AuditID={1}&CheckPointID={2}", oFYID, oAuditID, oCheckPointID), False)
                Exit Try
            End If
            'If e.CommandName = "ReviewLedger" Then
            '    Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            '    lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
            '    oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAuditID.Text)))
            '    lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
            '    oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblCheckPointID.Text)))
            '    Response.Redirect(String.Format("~/StandardAudit/frmAuditLedgerUpload.aspx?AuditID={0}&CheckPointID={1}", oAuditID, oCheckPointID), False)
            '    Exit Try
            'End If
            If e.CommandName = "DRL" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAuditID.Text)))
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblCheckPointID.Text)))
                Response.Redirect(String.Format("~/StandardAudit/DRLSampling.aspx?AuditID={0}&CheckPointID={1}", oAuditID, oCheckPointID), False)
                Exit Try
            End If
            If e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                iSelectedCheckPointID = 0 : lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                iAttachID = Val(lblAttachmentID.Text)
                iSelectedCheckPointID = Val(lblCheckPointID.Text)
                If iAttachID = 0 Then
                    iAttachID = objclsStandardAudit.GetDRLAttachmentID(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iSelectedCheckPointID)
                End If
                BindAllAttachments(Val(lblAuditID.Text), iAttachID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
            End If
            If e.CommandName = "Save" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                ddlTestResult = CType(clickedRow.FindControl("ddlTestResult"), DropDownList)
                txtRemarks = CType(clickedRow.FindControl("txtRemarks"), TextBox)
                txtReviewerRemarks = CType(clickedRow.FindControl("txtReviewerRemarks"), TextBox)
                chkAnnexure = CType(clickedRow.FindControl("chkAnnexure"), CheckBox)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                lblCheckPoint = CType(clickedRow.FindControl("lblCheckPoint"), Label)
                lblConductAuditCheckPointPKId = CType(clickedRow.FindControl("lblConductAuditCheckPointPKId"), Label)
                lblRemarks = CType(clickedRow.FindControl("lblRemarks"), Label)
                lblReviewerRemarks = CType(clickedRow.FindControl("lblReviewerRemarks"), Label)
                chkAuditorSendMail = CType(clickedRow.FindControl("chkAuditorSendMail"), CheckBox)
                chkReviewerSendMail = CType(clickedRow.FindControl("chkReviewerSendMail"), CheckBox)
                iAnnexure = 0
                If chkAnnexure.Checked Then
                    iAnnexure = 1
                End If
                Dim iRemarksType As Integer = 0 : Dim sRemarks As String = objclsGRACeGeneral.SafeSQL(txtRemarks.Text)
                If iPartnerRemarksType > 0 And iAdditionalEmpRemarksType = 0 Then
                    iRemarksType = 1
                ElseIf iPartnerRemarksType > 0 And iAdditionalEmpRemarksType > 0 Then
                    iRemarksType = 1
                ElseIf iPartnerRemarksType = 0 And iAdditionalEmpRemarksType > 0 Then
                    iRemarksType = 2
                End If
                If iReviewPartnerRemarksType > 0 Then
                    iRemarksType = 3
                    sRemarks = objclsGRACeGeneral.SafeSQL(txtReviewerRemarks.Text)
                End If
                If (iRemarksType = 1 Or iRemarksType = 2) And txtRemarks.Text.Trim = "" Then
                    lblError.Text = "Please enter Remarks." : lblConductValidationMsg.Text = "Please enter Remarks."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                    Exit Try
                End If
                If (iRemarksType = 3) And txtReviewerRemarks.Text.Trim = "" Then
                    lblError.Text = "Please enter Remarks." : lblConductValidationMsg.Text = "Please enter Reviewer Remarks."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                    Exit Try
                End If
                If iRemarksType = 0 Then
                    lblError.Text = "You are not a part of this Audit." : lblConductValidationMsg.Text = "You are not a part of this Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                    Exit Try
                End If

                Dim iIsIssueRaised As Integer = 0
                If chkReviewerSendMail.Checked = True Or chkAuditorSendMail.Checked = True Then
                    iIsIssueRaised = 1
                End If
                Dim sEmail As String = objclsDRLLog.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If sEmail = "" AndAlso iIsIssueRaised = 1 Then
                    lblError.Text = "There is no customer user to send mail." : lblConductValidationMsg.Text = "There is no customer user to send mail."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
                    Exit Try
                End If

                If (iRemarksType = 1 Or iRemarksType = 2) Then
                    If sRemarks = objclsGRACeGeneral.SafeSQL(lblRemarks.Text) And chkAuditorSendMail.Checked = False Then
                        lblError.Text = "Successfully Updated." : lblConductValidationMsg.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalConductValidation').modal('show');", True)
                        Exit Sub
                    End If
                ElseIf iRemarksType = 3 Then
                    If sRemarks = objclsGRACeGeneral.SafeSQL(lblReviewerRemarks.Text) And chkReviewerSendMail.Checked = False Then
                        lblError.Text = "Successfully Updated." : lblConductValidationMsg.Text = "Successfully Updated."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalConductValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If

                objclsStandardAudit.UpdateScheduleCheckPointRemarksAnnexure(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblConductAuditCheckPointPKId.Text), Val(lblCheckPointID.Text), iRemarksType, sRemarks, iAnnexure, ddlTestResult.SelectedIndex, sSession.UserID, sSession.IPAddress, iIsIssueRaised, sEmail)
                BindScheduledDetails(ddlAuditNo.SelectedValue)
                lblError.Text = "Successfully Updated." : lblConductValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalConductValidation').modal('show');", True)
                objclsStandardAudit.UpdateStandardAuditStatus(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, 4)
                gvCheckPoint.DataSource = objclsStandardAudit.LoadSelectedStandardAuditCheckPointDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, sSession.UserID, bLoginUserIsPartner)
                gvCheckPoint.DataBind()

                If iIsIssueRaised = 1 Then
                    SendMail(lblAuditType.Text, lblCheckPoint.Text, sRemarks, "sujatha@mmcspl.com;rashmi@mmcspl.com")
                End If
            End If
            If e.CommandName = "HistoryAR" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                Dim dt As New DataTable
                dt = objclsStandardAudit.LoadSelectedStandardAuditCheckPointRemarksHistoryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), 1)

                If dt.Rows.Count > 0 Then
                    gvHistory.DataSource = dt
                    gvHistory.DataBind()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myHistoryModal').modal('show')", True)
                Else
                    lblError.Text = "History not available." : lblConductValidationMsg.Text = "History not available."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                End If
            End If
            If e.CommandName = "HistoryRR" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                Dim dt As New DataTable
                dt = objclsStandardAudit.LoadSelectedStandardAuditCheckPointRemarksHistoryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCheckPointID.Text), 2)

                If dt.Rows.Count > 0 Then
                    gvHistory.DataSource = dt
                    gvHistory.DataBind()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myHistoryModal').modal('show')", True)
                Else
                    lblError.Text = "History not available." : lblConductValidationMsg.Text = "History not available."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCheckPoint_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgMainAttach.CurrentPageIndex = 0
            dgMainAttach.PageSize = 1000
            ds = objclsAttachments.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            If ds.Tables(0).Rows.Count > dgMainAttach.PageSize Then
                dgMainAttach.AllowPaging = True
            Else
                dgMainAttach.AllowPaging = False
            End If
            dgMainAttach.DataSource = ds
            dgMainAttach.DataBind()
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachments" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
                    Exit Sub
                End If

                'Added By Steffi
                Dim lnkFile As New LinkButton
                Dim sFilePath As String, sFileName As String
                For i = 0 To dgMainAttach.Items.Count - 1
                    sFilePath = "" : sFileName = ""
                    lnkFile = dgMainAttach.Items(i).FindControl("File")

                    If txtfile.PostedFile.FileName = lnkFile.Text Then
                        lblMsg.Text = "File already Exists."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
                        Exit Try
                    End If
                Next

                lblHDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                ' objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iAttachID > 0 Then
                        BindAllAttachments(sSession.AccessCode, iAttachID)
                    End If
                    objclsStandardAudit.SaveDRLConductAuditAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iAttachID, ddlAuditNo.SelectedValue, iSelectedCheckPointID, "CA")
                    BindScheduledDetails(ddlAuditNo.SelectedValue)
                Else
                    lblMsg.Text = "No file to Attach."
                End If
            Else
                lblMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnaddAttch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnAddDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDesc.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If txtDescription.Text.Trim.Length > 1000 Then
                lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
                Exit Try
            End If
            objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
            lblHDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
            iDocID = 0
            BindAllAttachments(sSession.AccessCode, iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgMainAttach_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgMainAttach.ItemDataBound
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMainAttach_ItemDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub dgMainAttach_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMainAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                'DownloadMyFile(sDestFilePath)
                sDestFilePath = "https://" & sDestFilePath.Substring(48)
                iframeview.Src = sDestFilePath
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
                lblHDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMainAttach_ItemCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            lblError.Text = ""
            iAttachID = 0 : iSelectedCheckPointID = 0
            If gvCheckPoint.Rows.Count = 0 Then
                lblError.Text = "There is no checkpoint for the selected Audit." : lblConductValidationMsg.Text = "There is no checkpoint for the selected Audit."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                Exit Try
            End If
            dt = objclsStandardAudit.LoadStandardAuditConductAuditReport(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            dt1 = objclsStandardAudit.LoadStandardAuditConductAuditObservationsReport(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dt1)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/ConductAudit.rdlc")
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", ddlAuditNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditNo)
            Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", lblAuditType.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditType)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Conduct Audit", "PDF", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, ddlAuditNo.SelectedValue, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ConductAudit" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            lblError.Text = ""
            iAttachID = 0 : iSelectedCheckPointID = 0
            If gvCheckPoint.Rows.Count = 0 Then
                lblError.Text = "There is no checkpoint for the selected Audit." : lblConductValidationMsg.Text = "There is no checkpoint for the selected Audit."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                Exit Try
            End If
            dt = objclsStandardAudit.LoadStandardAuditConductAuditReport(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            dt1 = objclsStandardAudit.LoadStandardAuditConductAuditObservationsReport(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dt1)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/ConductAudit.rdlc")
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", ddlAuditNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditNo)
            Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", lblAuditType.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditType)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Conduct Audit", "Excel", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, ddlAuditNo.SelectedValue, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ConductAudit" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub SendMail(ByVal sAuditType As String, ByVal sCheckPoint As String, ByVal sComments As String, ByVal sToMailIds As String)
        Dim sBody As String
        Dim sSubject As String
        Try
            sSubject = "Audit Observation for " & sAuditType & " Audit Procedure - " & sCheckPoint & ""
            sBody = sComments

            Dim myMail As New System.Web.Mail.MailMessage()
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "Trace@mmcspl.com")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "TRjune@23")
            myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
            myMail.From = "Trace@mmcspl.com"

            myMail.Bcc = sToMailIds
            myMail.Subject = sSubject
            myMail.BodyFormat = MailFormat.Html
            myMail.Body = sBody
            System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
            System.Web.Mail.SmtpMail.Send(myMail)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub imgbtnSubmit_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSubmit.Click
        Dim lblCheckPoint As New Label, lblTestResult As New Label, lblMandatory As New Label, lblRemarks As New Label, lblReviewerRemarks As New Label
        Try
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblConductValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                ddlAuditNo.Focus()
                Exit Try
            End If
            If gvCheckPoint.Rows.Count = 0 Then
                lblError.Text = "There is no checkpoint for the selected Audit." : lblConductValidationMsg.Text = "There is no checkpoint for the selected Audit."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                Exit Try
            End If
            For i = 0 To gvCheckPoint.Rows.Count - 1
                lblCheckPoint = gvCheckPoint.Rows(i).FindControl("lblCheckPoint")
                lblTestResult = gvCheckPoint.Rows(i).FindControl("lblTestResult")
                lblMandatory = gvCheckPoint.Rows(i).FindControl("lblMandatory")
                lblRemarks = gvCheckPoint.Rows(i).FindControl("lblRemarks")
                lblReviewerRemarks = gvCheckPoint.Rows(i).FindControl("lblReviewerRemarks")

                If lblMandatory.Text.ToLower.ToString() = "yes" Then
                    If Val(lblTestResult.Text) = 0 Then
                        lblError.Text = "Please select the Test Result & Save data for Audit Procedure (Line no. : " & i + 1 & ") - " & lblCheckPoint.Text & "" : lblConductValidationMsg.Text = "Please select the Test Result for Audit Procedure (Line no. : " & i + 1 & ") - " & lblCheckPoint.Text & ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                        Exit Try
                    End If
                    If lblRemarks.Text = "" & lblReviewerRemarks.Text = "" Then
                        lblError.Text = "Please enter remarks & Save data for Audit Procedure (Line no. : " & i + 1 & ") - " & lblCheckPoint.Text & "" : lblConductValidationMsg.Text = "Please enter remarks & Save data for Audit Procedure (Line no. : " & i + 1 & ") - " & lblCheckPoint.Text & ""
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalConductValidation').modal('show');", True)
                        Exit Try
                    End If
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSubmit_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class