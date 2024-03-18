Imports System
Imports System.Data
Imports BusinesLayer
Partial Class MRIssue
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_MRIssue"
    Private sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAttachments As New clsAttachments
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsSAAuditSummary As New clsSAAuditSummary
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared iCustID As Integer = 0
    Private Shared iMRPKID As Integer
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

                RFVResponsesReceivedDate.ControlToValidate = "txtResponsesReceivedDate" : RFVResponsesReceivedDate.ErrorMessage = "Enter Received Date."
                RFEResponsesReceivedDate.ErrorMessage = "Enter valid Received Date." : RFEResponsesReceivedDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVResponsesDetails.ControlToValidate = "txtResponsesDetails" : RFVResponsesDetails.ErrorMessage = "Enter Responses Details."
                REVResponsesDetails.ErrorMessage = "Responses Details exceeded maximum size(max 8000 characters)." : REVResponsesDetails.ValidationExpression = "^[\s\S]{0,8000}$"
                RFVResponsesRemarks.ControlToValidate = "txtResponsesRemarks" : RFVResponsesRemarks.ErrorMessage = "Enter Responses Remarks."
                REVResponsesRemarks.ErrorMessage = "Responses Remarks exceeded maximum size(max 8000 characters)." : REVResponsesRemarks.ValidationExpression = "^[\s\S]{0,8000}$"
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
            iCustID = 0 : iMRPKID = 0 : iAttachID = 0
            dgMRdetails.DataSource = Nothing
            dgMRdetails.DataBind()
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
            iCustID = 0 : iMRPKID = 0 : iAttachID = 0
            dgMRdetails.DataSource = Nothing
            dgMRdetails.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                iAuditId = ddlAuditNo.SelectedValue
                BindScheduledDetails(iAuditId)
                BindStandardAuditASMRdetails(iAuditId)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindManagementRepresentations(ByVal iFYId As Integer, ByVal iAsgId As Integer, ByVal iMRPKID As Integer)
        Try
            ddlManagementRepresentations.DataSource = objclsSAAuditSummary.LoadManagementRepresentations(sSession.AccessCode, sSession.AccessCodeID, "MR", iFYId, iAsgId, iMRPKID)
            ddlManagementRepresentations.DataTextField = "Name"
            ddlManagementRepresentations.DataValueField = "PKID"
            ddlManagementRepresentations.DataBind()
            ddlManagementRepresentations.Items.Insert(0, "Select Management Representations")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditTypes" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgMRdetails_PreRender(sender As Object, e As EventArgs) Handles dgMRdetails.PreRender
        Try
            If dgMRdetails.Rows.Count > 0 Then
                dgMRdetails.UseAccessibleHeader = True
                dgMRdetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgMRdetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMRdetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStandardAuditASMRdetails(ByVal iAuditId As Integer)
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            dgMRdetails.DataSource = dtTab : dgMRdetails.DataBind()
            dt = objclsSAAuditSummary.LoadStandardAuditMR(sSession.AccessCode, sSession.AccessCodeID, iAuditId)
            dgMRdetails.DataSource = dt
            dgMRdetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStandardAuditASMRdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ClearMRResponsesDetails()
        Try
            ddlManagementRepresentations.Enabled = False : lblEmailID.Enabled = False : lblMRDueDateReceiveDocs.Enabled = False : lblMRRRemarks.Enabled = False
            txtResponsesReceivedDate.Text = "" : txtResponsesDetails.Text = "" : txtResponsesRemarks.Text = ""
            lblHResponsesReceivedDate.Visible = True : txtResponsesReceivedDate.Visible = True
            lblHResponsesDetails.Visible = True : txtResponsesDetails.Visible = True
            lblHResponsesRemarks.Visible = True : txtResponsesRemarks.Visible = True
            btnSaveMRResponses.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgMRdetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgMRdetails.RowDataBound
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMRdetails_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgMRdetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgMRdetails.RowCommand
        Dim lblDescription As New Label, lblDescID As New Label, lblAttachmentID As New Label
        Dim lblDBpkId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Comments" Then
                ClearMRResponsesDetails()
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iMRPKID = Val(lblDBpkId.Text)
                BindStandardAuditASMRselectedDetails(iMRPKID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myMRRModal').modal('show');", True)
            End If
            If e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iMRPKID = Val(lblDBpkId.Text)

                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iAttachID = Val(lblAttachmentID.Text)
                BindAllAttachments(iAttachID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgMRdetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSaveMRResponses_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblAuditSummaryValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlCustomerName').focus();", True)
                ddlCustomerName.Focus()
                Exit Sub
            End If
            If ddlAuditNo.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblAuditSummaryValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                ddlAuditNo.Focus()
                Exit Sub
            End If
            If ddlManagementRepresentations.SelectedIndex = 0 Then
                lblError.Text = "Select Audit No." : lblAuditSummaryValidationMsg.Text = "Select Management Representations."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlManagementRepresentations').focus();", True)
                ddlManagementRepresentations.Focus()
                Exit Sub
            End If
            If txtResponsesDetails.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Responses Details." : lblError.Text = "Enter Responses Details."
                txtResponsesDetails.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtResponsesDetails').focus();", True)
                Exit Sub
            End If
            If txtResponsesDetails.Text.Trim.Length > 8000 Then
                lblAuditSummaryValidationMsg.Text = "Responses Details exceeded maximum size(max 5000 characters)." : lblError.Text = "Remarks exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtResponsesDetails').focus();", True)
                txtResponsesDetails.Focus()
                Exit Sub
            End If
            If txtResponsesRemarks.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Responses Remarks." : lblError.Text = "Enter Responses Remarks."
                txtResponsesRemarks.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtResponsesRemarks').focus();", True)
                Exit Sub
            End If
            If txtResponsesRemarks.Text.Trim.Length > 8000 Then
                lblAuditSummaryValidationMsg.Text = "Responses Remarks exceeded maximum size(max 5000 characters)." : lblError.Text = "Remarks exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtResponsesRemarks').focus();", True)
                txtResponsesRemarks.Focus()
                Exit Sub
            End If

            objclsSAAuditSummary.UpdateStandardAuditASMRdetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iMRPKID, Date.ParseExact(lblMRDueDateReceiveDocs.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), objclsGRACeGeneral.SafeSQL(txtResponsesDetails.Text.ToString()), objclsGRACeGeneral.SafeSQL(txtResponsesRemarks.Text.ToString()))
            BindStandardAuditASMRdetails(ddlAuditNo.SelectedValue)
            lblError.Text = "Successfully Saved." : lblAuditSummaryValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditSummaryValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveMRResponses_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStandardAuditASMRselectedDetails(ByVal iMRPKID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsSAAuditSummary.LoadStandardAuditASMRselectedDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iMRPKID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("SAMR_MRID")) = False Then
                    BindManagementRepresentations(ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue, dt.Rows(0)("SAMR_MRID"))
                    ddlManagementRepresentations.SelectedValue = dt.Rows(0)("SAMR_MRID")
                End If
                If IsDBNull(dt.Rows(0)("Heading")) = False Then
                    lblMRRHeading.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Heading").ToString())
                End If
                If IsDBNull(dt.Rows(0)("Description")) = False Then
                    lblMRRDescription.Text = dt.Rows(0)("Description")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_RequestedDate")) = False Then
                    lblMRRequestedDate.Text = dt.Rows(0)("SAMR_RequestedDate")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_RequestedByPerson")) = False Then
                    lblMRRequestedByPerson.Text = dt.Rows(0)("SAMR_RequestedByPerson")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_DueDateReceiveDocs")) = False Then
                    lblMRDueDateReceiveDocs.Text = dt.Rows(0)("SAMR_DueDateReceiveDocs")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_RequestedRemarks")) = False Then
                    lblMRRRemarks.Text = dt.Rows(0)("SAMR_RequestedRemarks")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_EmailIds")) = False Then
                    lblEmailID.Text = dt.Rows(0)("SAMR_EmailIds")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_ResponsesReceivedDate")) = False Then
                    txtResponsesReceivedDate.Text = dt.Rows(0)("SAMR_ResponsesReceivedDate")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_ResponsesDetails")) = False Then
                    txtResponsesDetails.Text = dt.Rows(0)("SAMR_ResponsesDetails")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_ResponsesRemarks")) = False Then
                    txtResponsesRemarks.Text = dt.Rows(0)("SAMR_ResponsesRemarks")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStandardAuditASMRselectedDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal iAttachID As Integer)
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
                        BindAllAttachments(iAttachID)
                    End If
                    objclsSAAuditSummary.UpdateStandardAuditASMRAttachmentdetails(sSession.AccessCode, sSession.AccessCodeID, iMRPKID, iAttachID)
                    BindStandardAuditASMRdetails(ddlAuditNo.SelectedValue)
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
            BindAllAttachments(iAttachID)
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
                BindAllAttachments(iAttachID)
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