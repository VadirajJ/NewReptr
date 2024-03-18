Imports System
Imports System.Data
Imports BusinesLayer
Partial Class AuditorIssue
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_AuditorIssue"
    Private sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAttachments As New clsAttachments
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsUploadLedger As New clsUploadLedger
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared iCustID As Integer = 0
    Private Shared iLedgerId As Integer = 0
    Private Shared iCheckPointId As Integer = 0
    Private Shared iAuditTypeID As Integer = 0
    Private Shared iSelectedLedgerID As Integer
    Private Shared iSelectedCheckPointId As Integer
    Private Shared iIsLedgerCheckPointId As Integer

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iAttachID = 0
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomerName(sender, e) : BindAllAuditNo()
                BindLedgerDetails(0, 0)
                BindCheckPointDetails(0, 0)
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
                iAuditTypeID = dt.Rows(0)("SA_AuditTypeID")
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
    Private Sub BindAllAuditNo()
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
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblAuditType.Text = ""
            iCustID = 0 : iAuditTypeID = 0 : iLedgerId = 0 : iCheckPointId = 0
            iAttachID = 0 : iSelectedLedgerID = 0 : iSelectedCheckPointId = 0 : iIsLedgerCheckPointId = 0
            gvLedgerComments.DataSource = Nothing
            gvLedgerComments.DataBind()
            BindAllAuditNo()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Dim iAuditId As Integer = 0
        Try
            lblError.Text = ""
            lblAuditType.Text = ""
            iCustID = 0 : iAuditTypeID = 0 : iLedgerId = 0 : iCheckPointId = 0
            iAttachID = 0 : iSelectedLedgerID = 0 : iSelectedCheckPointId = 0 : iIsLedgerCheckPointId = 0
            gvLedgerComments.DataSource = Nothing
            gvLedgerComments.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                iAuditId = ddlAuditNo.SelectedValue
                BindScheduledDetails(iAuditId)
                BindLedgerAuditorIssues(iCustID, iAuditId, 0)
                BindCheckPointAuditorIssues(iCustID, iAuditId, 0)
            End If
            BindLedgerDetails(iCustID, iAuditId)
            BindCheckPointDetails(iCustID, iAuditId)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindLedgerDetails(ByVal iCustID As Integer, ByVal iAuditID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsUploadLedger.LoadCULedgerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, iAuditID, iAuditTypeID)
            ddlLedger.DataSource = dt
            ddlLedger.DataTextField = "LedgerName"
            ddlLedger.DataValueField = "LedgerId"
            ddlLedger.DataBind()
            ddlLedger.Items.Insert(0, "Select Ledger Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLedgerDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlLedger_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLedger.SelectedIndexChanged
        Dim iLedgerId As Integer = 0
        Try
            If ddlLedger.SelectedIndex > 0 Then
                iLedgerId = ddlLedger.SelectedValue
            End If
            BindLedgerAuditorIssues(iCustID, ddlAuditNo.SelectedValue, iLedgerId)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLedger_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindLedgerAuditorIssues(ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iLedgerId As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsUploadLedger.LoadCULedgerCustomerIssues(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, iAuditID, iAuditTypeID, iLedgerId)
            gvLedgerComments.DataSource = dt
            gvLedgerComments.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLedgerAuditorIssues" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvLedgerComments_PreRender(sender As Object, e As EventArgs) Handles gvLedgerComments.PreRender
        Try
            If gvLedgerComments.Rows.Count > 0 Then
                gvLedgerComments.UseAccessibleHeader = True
                gvLedgerComments.HeaderRow.TableSection = TableRowSection.TableHeader
                gvLedgerComments.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLedgerComments_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvLedgerComments_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvLedgerComments.RowDataBound
        Dim lblAttachmentID As New Label, lblBadgeCount As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLedgerComments_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvLedgerComments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLedgerComments.RowCommand
        Dim lblLedgerId As New Label, lblAuditorCommentsId As New Label, lblCustomerCommentsId As New Label
        Dim lblAttachmentID As New Label, txtCustomerComments As New TextBox
        Try
            lblError.Text = ""
            If e.CommandName = "Attachment" Then
                iIsLedgerCheckPointId = 1
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                iSelectedLedgerID = 0 : lblLedgerId = CType(clickedRow.FindControl("lblLedgerId"), Label)
                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                iSelectedLedgerID = Val(lblLedgerId.Text)
                iAttachID = Val(lblAttachmentID.Text)
                BindAllAttachments(sSession.AccessCode, iAttachID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
            End If
            If e.CommandName = "Save" Then
                iIsLedgerCheckPointId = 1
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblLedgerId = CType(clickedRow.FindControl("lblLedgerId"), Label)
                lblAuditorCommentsId = CType(clickedRow.FindControl("lblAuditorCommentsId"), Label)
                lblCustomerCommentsId = CType(clickedRow.FindControl("lblCustomerCommentsId"), Label)
                lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                txtCustomerComments = CType(clickedRow.FindControl("txtCustomerComments"), TextBox)
                If txtCustomerComments.Text.Trim = "" Then
                    lblError.Text = "Please enter Comments." : lblCUValidationMsg.Text = "Please enter Comments."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalCUValidation').modal('show');", True)
                    Exit Try
                End If
                Dim obclsUL As New clsUploadLedger
                If Val(lblCustomerCommentsId.Text > 0) Then
                    obclsUL.UpdateLedgerObservationsComments(sSession.AccessCode, sSession.AccessCodeID, Val(lblCustomerCommentsId.Text), sSession.UserID, objclsGRACeGeneral.SafeSQL(txtCustomerComments.Text.ToString()), sSession.IPAddress)
                Else
                    obclsUL.SaveLedgerObservationsComments(sSession.AccessCode, sSession.AccessCodeID, Val(lblLedgerId.Text), 3, sSession.UserID, objclsGRACeGeneral.SafeSQL(txtCustomerComments.Text.ToString()), sSession.IPAddress, Val(lblAuditorCommentsId.Text), "", ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, iAuditTypeID, "")
                End If
                BindLedgerAuditorIssues(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, iLedgerId)
                lblError.Text = "Successfully Saved/Updated." : lblCUValidationMsg.Text = "Successfully Saved/Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalCUValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLedgerComments_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCheckPointDetails(ByVal iCustID As Integer, ByVal iAuditID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsUploadLedger.LoadCUCheckPointDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            ddlCheckPoint.DataSource = dt
            ddlCheckPoint.DataTextField = "CheckPointName"
            ddlCheckPoint.DataValueField = "CheckPointID"
            ddlCheckPoint.DataBind()
            ddlCheckPoint.Items.Insert(0, "Select Check Point")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCheckPointDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCheckPoint_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCheckPoint.SelectedIndexChanged
        Dim iCheckPointId As Integer = 0
        Try
            If ddlLedger.SelectedIndex > 0 Then
                iCheckPointId = ddlLedger.SelectedValue
            End If
            BindCheckPointAuditorIssues(iCustID, ddlAuditNo.SelectedValue, iCheckPointId)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCheckPoint_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCheckPointAuditorIssues(ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iCheckPointId As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsUploadLedger.LoadCUCheckPointAuditorIssues(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointId)
            gvCheckPoint.DataSource = dt
            gvCheckPoint.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLedgerAuditorIssues" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
        Dim lblAttachmentID As New Label, lblBadgeCount As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCheckPoint_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCheckPoint_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCheckPoint.RowCommand
        Dim lblCheckPointId As New Label, lblAuditorCommentsId As New Label, lblCustomerCommentsId As New Label
        Dim lblAttachmentID As New Label, txtCustomerComments As New TextBox, lblConductAuditCheckPointId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Attachment" Then
                iIsLedgerCheckPointId = 2
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                iSelectedCheckPointId = 0 : lblCheckPointId = CType(clickedRow.FindControl("lblCheckPointId"), Label)
                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                iSelectedCheckPointId = Val(lblCheckPointId.Text)
                iAttachID = Val(lblAttachmentID.Text)
                BindAllAttachments(sSession.AccessCode, iAttachID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
            End If
            If e.CommandName = "Save" Then
                iIsLedgerCheckPointId = 2
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblConductAuditCheckPointId = CType(clickedRow.FindControl("lblConductAuditCheckPointId"), Label)
                lblCheckPointId = CType(clickedRow.FindControl("lblCheckPointId"), Label)
                lblAuditorCommentsId = CType(clickedRow.FindControl("lblAuditorCommentsId"), Label)
                lblCustomerCommentsId = CType(clickedRow.FindControl("lblCustomerCommentsId"), Label)
                lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                txtCustomerComments = CType(clickedRow.FindControl("txtCustomerComments"), TextBox)
                If txtCustomerComments.Text.Trim = "" Then
                    lblError.Text = "Please enter Comments." : lblCUValidationMsg.Text = "Please enter Comments."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalCUValidation').modal('show');", True)
                    Exit Try
                End If
                Dim obclsUL As New clsUploadLedger
                If Val(lblCustomerCommentsId.Text > 0) Then
                    obclsUL.UpdateCUCheckPointCustomerIssues(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblCustomerCommentsId.Text), objclsGRACeGeneral.SafeSQL(txtCustomerComments.Text.ToString()), sSession.UserID, sSession.IPAddress)
                Else
                    obclsUL.SaveCUCheckPointCustomerIssues(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, Val(lblConductAuditCheckPointId.Text), Val(lblCheckPointId.Text), 4, objclsGRACeGeneral.SafeSQL(txtCustomerComments.Text.ToString()), sSession.UserID, sSession.IPAddress, Val(lblAuditorCommentsId.Text), "")
                End If
                BindLedgerAuditorIssues(ddlCustomerName.SelectedValue, ddlAuditNo.SelectedValue, iCheckPointId)
                lblError.Text = "Successfully Saved/Updated." : lblCUValidationMsg.Text = "Successfully Saved/Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalCUValidation').modal('show');", True)
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
                    If iIsLedgerCheckPointId = 1 Then
                        objclsStandardAudit.SaveTrialBalanceReviewAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iAttachID, ddlAuditNo.SelectedValue, iSelectedLedgerID)
                        BindLedgerAuditorIssues(iCustID, ddlAuditNo.SelectedValue, iLedgerId)
                    ElseIf iIsLedgerCheckPointId = 2 Then
                        objclsStandardAudit.SaveDRLConductAuditAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iAttachID, ddlAuditNo.SelectedValue, iSelectedCheckPointId, "CA")
                        BindCheckPointAuditorIssues(iCustID, ddlAuditNo.SelectedValue, iCheckPointId)
                    End If
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
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
                If iIsLedgerCheckPointId = 1 Then
                    BindLedgerAuditorIssues(iCustID, ddlAuditNo.SelectedValue, iLedgerId)
                ElseIf iIsLedgerCheckPointId = 2 Then
                    BindCheckPointAuditorIssues(iCustID, ddlAuditNo.SelectedValue, iCheckPointId)
                End If
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
End Class