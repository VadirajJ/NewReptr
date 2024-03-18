Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WebForms
Imports System.Web.Mail
Imports System.Drawing

Public Class frmAuditLedgerUpload
    Inherits System.Web.UI.Page
    Private sFormName As String = "frmAuditLedgerUpload"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsStandardAudit As New clsStandardAudit
    Private obclsUL As New clsUploadLedger
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsDRLLog As New clsDRLLog
    Private objclsAttachments As New clsAttachments
    Private Shared iAuditTypeID As Integer = 0
    Private Shared iCheckPointID As Integer = 0
    Private Shared sAuditProcedure As String
    Private Shared sLedger As String
    Private Shared sHeading As String
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared iSelectedLedgerID As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        'imgLinkageForYear.ImageUrl = "~/Images/Submit24.png"
        ImgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnExcelUpload.ImageUrl = "~/Images/ExcelSA.jpg"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                ImgbtnApprove.Visible = False
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomers() : BindAuditNo(0)
                If Request.QueryString("AuditID") IsNot Nothing Then
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
                ddlFinancialYear.SelectedValue = dt.Rows(0)("SA_YearID")
                ddlCustomerName.SelectedValue = dt.Rows(0)("SA_CustID")
                BindAuditNo(ddlCustomerName.SelectedValue)
                ddlAuditNo.SelectedValue = dt.Rows(0)("SA_ID").ToString()
                iAuditTypeID = dt.Rows(0)("SA_AuditTypeID").ToString()
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
            End If
            sAuditProcedure = objclsStandardAudit.GetSelectedScheduleCheckPointDetails(sSession.AccessCode, sSession.AccessCodeID, iCheckPointID)
            sHeading = objclsStandardAudit.GetSelectedScheduleHeadingDetails(sSession.AccessCode, sSession.AccessCodeID, iCheckPointID)
            If sHeading = "" Then
                sHeading = "-"
            End If
            BindReviewLedgerDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/StandardAudit/ConductAudit.aspx?AuditID={0}", HttpUtility.UrlDecode(ddlAuditNo.SelectedValue)))
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
            iCheckPointID = 0
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
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
            iCheckPointID = 0
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                BindScheduledDetails(ddlAuditNo.SelectedValue)
                BindReviewLedgerDetails()
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
    Private Sub dgGeneral_PreRender(sender As Object, e As EventArgs) Handles dgGeneral.PreRender
        Try
            If dgGeneral.Rows.Count > 0 Then
                dgGeneral.UseAccessibleHeader = True
                dgGeneral.HeaderRow.TableSection = TableRowSection.TableHeader
                dgGeneral.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgGeneral_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgGeneral.RowDataBound
        Dim lblObservationCount As New Label, lblAttachmentID As New Label, lblBadgeCount As New Label
        Try
            If e.Row.RowType = ListItemType.Item Then
                lblObservationCount = (TryCast(e.Row.FindControl("lblObservationCount"), Label))
                If IsNothing(lblObservationCount.Text) = False Then
                    If Val(lblObservationCount.Text) > 0 Then
                        e.Row.BackColor = ColorTranslator.FromHtml("#FFD700")
                    End If
                End If

                lblAttachmentID = CType(e.Row.FindControl("lblAttachmentID"), Label)
                lblBadgeCount = CType(e.Row.FindControl("lblBadgeCount"), Label)
                If Val(lblAttachmentID.Text) > 0 Then
                    Dim iCount As Integer = objclsAttachments.GetAttachmentCount(sSession.AccessCode, sSession.AccessCodeID, Val(lblAttachmentID.Text))
                    lblBadgeCount.Text = iCount
                Else
                    lblBadgeCount.Text = 0
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindReviewLedgerDetails()
        Dim dt As New DataTable
        Dim iPYCount As Integer = 0, iCYCount As Integer = 0
        Try
            lblError.Text = ""
            imgbtnSave.Visible = True
            iPYCount = obclsUL.getCustTBPreviousYearcount(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue)
            If iPYCount = 0 Then
                lblError.Text = "Please upload and approve the trial balance for previous year."
                Exit Sub
            End If

            iCYCount = obclsUL.getCustTBCurrentYearcount(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue)
            If iCYCount = 0 Then
                lblError.Text = "Please upload and approve the trial balance for selected year."
                'Exit Sub
            End If

            If ddlCustomerName.SelectedIndex > 0 And ddlFinancialYear.SelectedValue > 0 Then
                dt = obclsUL.getCustTBSelectedYear(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    dgGeneral.Visible = True
                    dgGeneral.DataSource = dt
                    dgGeneral.DataBind()
                    imgbtnSave.Visible = True
                    ImgbtnApprove.Visible = True
                Else
                    dgGeneral.Visible = False
                    imgbtnSave.Visible = False
                    ImgbtnApprove.Visible = False
                    lblError.Text = "No Data Found. Upload Trail Balance For this Customer"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindReviewLedgerDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnExcelUpload_Click(sender As Object, e As EventArgs) Handles imgbtnExcelUpload.Click
        Dim oFYID As Object, oAuditID As Object, oCheckPointID As Object
        Try
            oFYID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlFinancialYear.SelectedValue))
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditNo.SelectedValue))
            oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            Response.Redirect(String.Format("~/StandardAudit/frmUploadTBExcel.aspx?FYID={0}&AuditID={1}&CheckPointID={2}", oFYID, oAuditID, oCheckPointID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnExcelUpload_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgGeneral_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgGeneral.RowCommand
        Dim lblDescription As New Label, lblDescID As New Label, lblAttachmentID As New Label
        Try
            lblLedgerId.Text = 0 : lblOCId.Text = 0
            lblLedger.Text = "" : lblModelHeading.Text = "Observations/Comments Details" : lblHObservationsComments.Text = "* Observations"
            lblObservationError.Text = "" : txtObservationsComments.Text = ""
            btnSendIssuetoClient.Visible = False
            If e.CommandName = "Comments" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDescription = CType(clickedRow.FindControl("lblDescription"), Label)
                lblDescID = CType(clickedRow.FindControl("lblDescID"), Label)
                lblOCId.Text = 0 : lblLedgerId.Text = lblDescID.Text : lblLedger.Text = lblDescription.Text
                BindObservationsCommentsGrid(Val(lblLedgerId.Text))
                rboAuditor.Checked = True : rboReviewer.Checked = False : rboClient.Checked = False
                rboAuditor_CheckedChanged(sender, e)
            End If

            If e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDescID = CType(clickedRow.FindControl("lblDescID"), Label)
                iSelectedLedgerID = 0 : lblLedgerId.Text = lblDescID.Text
                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                iSelectedLedgerID = Val(lblLedgerId.Text)
                iAttachID = Val(lblAttachmentID.Text)
                BindAllAttachments(Val(lblLedgerId.Text), iAttachID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboAuditor_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboAuditor.CheckedChanged
        Try
            lblError.Text = "" : lblOCId.Text = 1
            lblObservationError.Text = "" : txtObservationsComments.Text = ""
            lblModelHeading.Text = "Auditor Observation Details" : lblHObservationsComments.Text = "* Auditor Observations"
            RFVObservationsComments.ControlToValidate = "txtObservationsComments" : RFVObservationsComments.ErrorMessage = "Enter Observations."
            REVObservationsComments.ErrorMessage = "Observations exceeded maximum size(max 8000 characters)." : REVObservationsComments.ValidationExpression = "^[\s\S]{0,8000}$"
            btnSendIssuetoClient.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboAuditor_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboReviewer_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboReviewer.CheckedChanged
        Try
            lblError.Text = "" : lblOCId.Text = 2
            lblObservationError.Text = "" : txtObservationsComments.Text = ""
            lblModelHeading.Text = "Reviewer Observation Details" : lblHObservationsComments.Text = "* Reviewer Observations"
            RFVObservationsComments.ControlToValidate = "txtObservationsComments" : RFVObservationsComments.ErrorMessage = "Enter Reviewer Observations."
            REVObservationsComments.ErrorMessage = "Reviewer Observations exceeded maximum size(max 8000 characters)." : REVObservationsComments.ValidationExpression = "^[\s\S]{0,8000}$"
            btnSendIssuetoClient.Visible = False
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboReviewer_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboClient_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboClient.CheckedChanged
        Try
            lblError.Text = "" : lblOCId.Text = 3
            lblObservationError.Text = "" : txtObservationsComments.Text = ""
            lblModelHeading.Text = "Client Comment Details" : lblHObservationsComments.Text = "* Client Comments"
            RFVObservationsComments.ControlToValidate = "txtObservationsComments" : RFVObservationsComments.ErrorMessage = "Enter Client Comments."
            REVObservationsComments.ErrorMessage = "Client Comments exceeded maximum size(max 8000 characters)." : REVObservationsComments.ValidationExpression = "^[\s\S]{0,8000}$"
            btnSendIssuetoClient.Visible = False
            BindObservationsCommentsGrid(Val(lblLedgerId.Text))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboClient_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindObservationsCommentsGrid(ByVal iLedgerID As Integer)
        Dim dt As New DataTable
        Try
            dt = obclsUL.LoadLedgerObservationsComments(sSession.AccessCode, sSession.AccessCodeID, iLedgerID)
            If dt.Rows.Count > 5 Then
                divOC.Style.Item("Height") = "172px"
            Else
                divOC.Style.Item("Height") = "auto"
            End If
            gvObservations.DataSource = dt
            gvObservations.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditTypeChecklistGrid" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSaveObservationsComments_Click(sender As Object, e As EventArgs)
        Try
            If txtObservationsComments.Text.ToString() = "" Then
                If Val(lblOCId.Text) = 1 Then
                    lblObservationError.Text = "Enter Observations."
                ElseIf Val(lblOCId.Text) = 2 Then
                    lblObservationError.Text = "Enter Reviewer Observations."
                ElseIf Val(lblOCId.Text) = 3 Then
                    lblObservationError.Text = "Enter Client Comment."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
                Exit Try
            End If
            If txtObservationsComments.Text.Length.ToString() > 8000 Then
                If Val(lblOCId.Text) = 1 Then
                    lblObservationError.Text = "Observations exceeded maximum size(max 8000 characters)."
                ElseIf Val(lblOCId.Text) = 2 Then
                    lblObservationError.Text = "Reviewer Observations exceeded maximum size(max 8000 characters)."
                ElseIf Val(lblOCId.Text) = 3 Then
                    lblObservationError.Text = "Client Comments exceeded maximum size(max 8000 characters)."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
                Exit Try
            End If

            obclsUL.SaveLedgerObservationsComments(sSession.AccessCode, sSession.AccessCodeID, Val(lblLedgerId.Text), Val(lblOCId.Text), sSession.UserID, objclsGRACeGeneral.SafeSQL(txtObservationsComments.Text.ToString()), sSession.IPAddress, 0, "", ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, iAuditTypeID, "")

            If Val(lblOCId.Text) = 1 Then
                lblObservationError.Text = "Successfully Saved Observations."
            ElseIf Val(lblOCId.Text) = 2 Then
                lblObservationError.Text = "Successfully Saved Reviewer Observations."
            ElseIf Val(lblOCId.Text) = 3 Then
                lblObservationError.Text = "Successfully Saved Client Comments."
            End If
            BindReviewLedgerDetails()
            BindObservationsCommentsGrid(Val(lblLedgerId.Text))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveObservationComments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSendIssuetoClient_Click(sender As Object, e As EventArgs)
        Try
            If txtObservationsComments.Text.ToString() = "" Then
                If Val(lblOCId.Text) = 1 Then
                    lblObservationError.Text = "Enter Observations."
                ElseIf Val(lblOCId.Text) = 2 Then
                    lblObservationError.Text = "Enter Reviewer Observations."
                ElseIf Val(lblOCId.Text) = 3 Then
                    lblObservationError.Text = "Enter Client Comment."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
                Exit Try
            End If
            If txtObservationsComments.Text.Length.ToString() > 8000 Then
                If Val(lblOCId.Text) = 1 Then
                    lblObservationError.Text = "Observations exceeded maximum size(max 8000 characters)."
                ElseIf Val(lblOCId.Text) = 2 Then
                    lblObservationError.Text = "Reviewer Observations exceeded maximum size(max 8000 characters)."
                ElseIf Val(lblOCId.Text) = 3 Then
                    lblObservationError.Text = "Client Comments exceeded maximum size(max 8000 characters)."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
                Exit Try
            End If
            Dim sEmail As String = objclsDRLLog.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            If sEmail = "" Then
                lblObservationError.Text = "There is no customer user to send mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
                Exit Try
            End If
            obclsUL.SaveLedgerObservationsComments(sSession.AccessCode, sSession.AccessCodeID, Val(lblLedgerId.Text), Val(lblOCId.Text), sSession.UserID, objclsGRACeGeneral.SafeSQL(txtObservationsComments.Text.ToString()), sSession.IPAddress, 1, sEmail, ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, iAuditTypeID, "")
            If Val(lblOCId.Text) = 1 Then
                lblObservationError.Text = "Successfully Saved Observations."
            ElseIf Val(lblOCId.Text) = 2 Then
                lblObservationError.Text = "Successfully Saved Reviewer Observations."
            ElseIf Val(lblOCId.Text) = 3 Then
                lblObservationError.Text = "Successfully Saved Client Comments."
            End If
            BindReviewLedgerDetails()
            BindObservationsCommentsGrid(Val(lblLedgerId.Text))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myObservationModal').modal('show');", True)
            SendMail(lblAuditType.Text, lblLedger.Text, txtObservationsComments.Text.ToString(), "sujatha@mmcspl.com;rashmi@mmcspl.com")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSendIssuetoClient_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Public Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable, dt1 As New DataTable
        Try
            lblError.Text = ""
            If dgGeneral.Rows.Count = 0 Then
                lblError.Text = "No Trail Balance for this Customer." : lblExcelValidationMsg.Text = "No Trail Balance for this Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalConductValidation').modal('show');", True)
                Exit Try
            End If
            dt = obclsUL.LoadLedgerObservationsCommentsReports(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/ReviewLedger.rdlc")
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", ddlAuditNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditNo)
            Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", lblAuditType.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditType)
            Dim Heading As ReportParameter() = New ReportParameter() {New ReportParameter("Heading", sHeading)}
            ReportViewer1.LocalReport.SetParameters(Heading)
            Dim AuditProcedure As ReportParameter() = New ReportParameter() {New ReportParameter("AuditProcedure", sAuditProcedure)}
            ReportViewer1.LocalReport.SetParameters(AuditProcedure)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Upload and Review Ledger", "PDF", ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, iAuditTypeID, sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ReviewLedger" + ".pdf")
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
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If dgGeneral.Rows.Count = 0 Then
                lblError.Text = "No Trail Balance for this Customer." : lblExcelValidationMsg.Text = "No Trail Balance for this Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalConductValidation').modal('show');", True)
                Exit Try
            End If
            dt = obclsUL.LoadLedgerObservationsCommentsReports(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/ReviewLedger.rdlc")
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", ddlAuditNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditNo)
            Dim AuditType As ReportParameter() = New ReportParameter() {New ReportParameter("AuditType", lblAuditType.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditType)
            Dim Heading As ReportParameter() = New ReportParameter() {New ReportParameter("Heading", sHeading)}
            ReportViewer1.LocalReport.SetParameters(Heading)
            Dim AuditProcedure As ReportParameter() = New ReportParameter() {New ReportParameter("AuditProcedure", sAuditProcedure)}
            ReportViewer1.LocalReport.SetParameters(AuditProcedure)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Upload and Review Ledger", "Excel", ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, iAuditTypeID, sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=ReviewLedger" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
                    objclsStandardAudit.SaveTrialBalanceReviewAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iAttachID, ddlAuditNo.SelectedValue, iSelectedLedgerID)
                    BindScheduledDetails(ddlAuditNo.SelectedValue)
                    BindReviewLedgerDetails()
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
                'Components.AppException.LogError(sSession.AccessCode, sDestFilePath, sFormName, "dgMainAttach_ItemCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex1) & "'" & "")
            End If
            If e.CommandName = "REMOVE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
                BindReviewLedgerDetails()
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message & sDestFilePath, sFormName, "dgMainAttach_ItemCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub SendMail(ByVal sAuditType As String, ByVal sLedger As String, ByVal sComments As String, ByVal sToMailIds As String)
        Dim sBody As String
        Dim sSubject As String
        Try
            sSubject = "Audit Observation for " & sAuditType & " Audit Type - " & sLedger & ""
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
End Class
