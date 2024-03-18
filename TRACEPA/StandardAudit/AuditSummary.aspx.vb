Imports BusinesLayer
Imports System.IO
Imports Microsoft.Reporting.WebForms

Public Class AuditSummary
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_AuditSummary"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsSAAuditSummary As New clsSAAuditSummary
    Dim objclsAttachments As New clsAttachments
    Private sSession As AllSession
    Private Shared sFile As String = ""
    Private Shared dtOriginalExcel As New DataTable
    Private Shared dtSlectedColumns As New DataTable
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iIFCcolumnCount As Integer = 0
    Private Shared iIFCDPKID As Integer = 0
    Private Shared iKAMDPKID As Integer = 0
    Private Shared iMRPKID As Integer = 0
    Private Shared iTabID As Integer = 1
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnSaveIFC.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If
                imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False : imgbtnReport.Visible = False
                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                liAuditDetails.Attributes.Add("class", "active") : divAuditDetails.Attributes.Add("class", "tab-pane active")
                lblMRRequestedDate.Text = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                lblMRRequestedByPerson.Text = sSession.UserFullName
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomers() : BindAuditNo(0) : BindConclusion()
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
                RFVIFCReportBy.ControlToValidate = "txtIFCReportBy" : RFVIFCReportBy.ErrorMessage = "Enter Report By."
                REVIFCReportBy.ErrorMessage = "Report By exceeded maximum size(max 100 characters)." : REVIFCReportBy.ValidationExpression = "^[\s\S]{0,100}$"
                RFVIFCReportDate.ControlToValidate = "txtIFCReportDate" : RFVIFCReportDate.ErrorMessage = "Enter Report Date."
                REVIFCReportDate.ErrorMessage = "Enter valid Report Date." : REVIFCReportDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVIFCComments.ControlToValidate = "txtIFCComments" : RFVIFCComments.ErrorMessage = "Enter Comments."
                REVIFCComments.ErrorMessage = "Comments exceeded maximum size(max 5000 characters)." : REVIFCComments.ValidationExpression = "^[\s\S]{0,5000}$"

                RFVIFCDateOfTesting.ControlToValidate = "txtIFCDateOfTesting" : RFVIFCDateOfTesting.ErrorMessage = "Enter Date Of Testing."
                REVIFCDateOfTesting.ErrorMessage = "Enter valid Date Of Testing." : REVIFCDateOfTesting.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVIFCSampleSizeUsed.ControlToValidate = "txtIFCSampleSizeUsed" : RFVIFCSampleSizeUsed.ErrorMessage = "Enter Sample Size Used."
                REVIFCSampleSizeUsed.ErrorMessage = "Sample Size Used exceeded maximum size(max 8000 characters)." : REVIFCSampleSizeUsed.ValidationExpression = "^[\s\S]{0,500}$"
                RFVIFCConclusion.InitialValue = "0" : RFVIFCConclusion.ErrorMessage = "Select Conclusion."
                RFVIFCTestingDetails.ControlToValidate = "txtIFCTestingDetails" : RFVIFCTestingDetails.ErrorMessage = "Enter Testing Details."
                REVIFCTestingDetails.ErrorMessage = "Testing Details exceeded maximum size(max 8000 characters)." : REVIFCTestingDetails.ValidationExpression = "^[\s\S]{0,8000}$"

                RFVDescriptionOrReasonForSelectionAsKAM.ControlToValidate = "txtDescriptionOrReasonForSelectionAsKAM" : RFVDescriptionOrReasonForSelectionAsKAM.ErrorMessage = "Enter Description or Reason for selection as KAM."
                REVDescriptionOrReasonForSelectionAsKAM.ErrorMessage = "Description or Reason for selection as KAM exceeded maximum size(max 8000 characters)." : REVDescriptionOrReasonForSelectionAsKAM.ValidationExpression = "^[\s\S]{0,8000}$"
                RFVAuditProcedureUndertakenToAddressTheKAM.ControlToValidate = "txtAuditProcedureUndertakenToAddressTheKAM" : RFVAuditProcedureUndertakenToAddressTheKAM.ErrorMessage = "Enter Audit Procedure undertaken to address the KAM."
                REVAuditProcedureUndertakenToAddressTheKAM.ErrorMessage = "Audit Procedure undertaken to address the KAM exceeded maximum size(max 8000 characters)." : REVAuditProcedureUndertakenToAddressTheKAM.ValidationExpression = "^[\s\S]{0,8000}$"

                RFVManagementRepresentations.InitialValue = "Select Management Representations" : RFVManagementRepresentations.ErrorMessage = "Select Management Representations."
                RFVMRDueDateReceiveDocs.ControlToValidate = "txtMRDueDateReceiveDocs" : RFVMRDueDateReceiveDocs.ErrorMessage = "Enter Due Date."
                REVMRDueDateReceiveDocs.ErrorMessage = "Enter valid Due Date." : REVMRDueDateReceiveDocs.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-\.]+)+([;]([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9\-]+\.)+([a-zA-Z0-9\-\.]+))*$"
                RFVMRRRemarks.ControlToValidate = "txtMRRRemarks" : RFVMRRRemarks.ErrorMessage = "Enter Requested Remarks."
                REVMRRRemarks.ErrorMessage = "Requested Remarks exceeded maximum size(max 5000 characters)." : REVMRRRemarks.ValidationExpression = "^[\s\S]{0,5000}$"

                RFVResponsesReceivedDate.ControlToValidate = "txtResponsesReceivedDate" : RFVResponsesReceivedDate.ErrorMessage = "Enter Received Date."
                RFEResponsesReceivedDate.ErrorMessage = "Enter valid Received Date." : RFEResponsesReceivedDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVResponsesDetails.ControlToValidate = "txtResponsesDetails" : RFVResponsesDetails.ErrorMessage = "Enter Responses Details."
                REVResponsesDetails.ErrorMessage = "Responses Details exceeded maximum size(max 8000 characters)." : REVResponsesDetails.ValidationExpression = "^[\s\S]{0,8000}$"
                RFVResponsesRemarks.ControlToValidate = "txtResponsesRemarks" : RFVResponsesRemarks.ErrorMessage = "Enter Responses Remarks."
                REVResponsesRemarks.ErrorMessage = "Responses Remarks exceeded maximum size(max 8000 characters)." : REVResponsesRemarks.ValidationExpression = "^[\s\S]{0,8000}$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Public Sub BindConclusion()
        Try
            ddlIFCConclusion.Items.Add(New ListItem("Select Conclusion", "0"))
            ddlIFCConclusion.Items.Add(New ListItem("KAM", "1"))
            ddlIFCConclusion.Items.Add(New ListItem("Audit Observation", "2"))
            ddlIFCConclusion.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
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
    Private Sub ClearAuditDetails()
        Try
            imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False
            gvDashboard.DataSource = Nothing
            gvDashboard.DataBind()
            gvDRLSummary.DataSource = Nothing
            gvDRLSummary.DataBind()
            gvObservationsQuerySummary.DataSource = Nothing
            gvObservationsQuerySummary.DataBind()
            gvCheckpointSummary.DataSource = Nothing
            gvCheckpointSummary.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearIFCbasicDetails()
        Try
            lblSelectFile.Visible = True : FUIFCLoad.Visible = True : btnOk.Visible = True
            imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False
            iIFCcolumnCount = 0 : txtIFCReportDate.Text = "" : txtIFCReportBy.Text = "" : txtIFCComments.Text = ""
            dgExcelIFC.DataSource = Nothing : dgExcelIFC.DataBind()
            dgExcelIFCDetails.DataSource = Nothing : dgExcelIFCDetails.DataBind()
            lblHIFCExcelColumns.Visible = False : btnOkIFCExcelColumns.Visible = False
            lblSheetName.Visible = False : ddlIFCSheetName.Visible = False
            lstIFCExcelColumns.DataSource = Nothing : lstIFCExcelColumns.DataBind()
            lstIFCExcelColumns.Visible = False : lstIFCExcelColumns.DataSource = Nothing : lstIFCExcelColumns.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearIFCDetails()
        Try
            txtIFCDateOfTesting.Text = "" : txtIFCSampleSizeUsed.Text = "" : txtIFCTestingDetails.Text = "" : ddlIFCConclusion.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearMRRequestDetails()
        Try
            ddlManagementRepresentations.Enabled = True : txtEmailID.Enabled = True : txtMRDueDateReceiveDocs.Enabled = True : txtMRRRemarks.Enabled = True
            txtResponsesReceivedDate.Text = "" : txtResponsesDetails.Text = "" : txtResponsesRemarks.Text = ""
            lblHResponsesReceivedDate.Visible = False : txtResponsesReceivedDate.Visible = False
            lblHResponsesDetails.Visible = False : txtResponsesDetails.Visible = False
            lblHResponsesRemarks.Visible = False : txtResponsesRemarks.Visible = False
            btnSaveMRRequest.Visible = True : btnSaveMRResponses.Visible = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearMRResponsesDetails()
        Try
            ddlManagementRepresentations.Enabled = False : txtEmailID.Enabled = False : txtMRDueDateReceiveDocs.Enabled = False : txtMRRRemarks.Enabled = False
            txtResponsesReceivedDate.Text = "" : txtResponsesDetails.Text = "" : txtResponsesRemarks.Text = ""
            lblHResponsesReceivedDate.Visible = True : txtResponsesReceivedDate.Visible = True
            lblHResponsesDetails.Visible = True : txtResponsesDetails.Visible = True
            lblHResponsesRemarks.Visible = True : txtResponsesRemarks.Visible = True
            btnSaveMRRequest.Visible = False : btnSaveMRResponses.Visible = True
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub SetActiveTabs(ByVal sTab As String)
        Try
            liAuditDetails.Attributes.Remove("class") : divAuditDetails.Attributes.Add("class", "tab-pane")
            liIFC.Attributes.Remove("class") : divIFC.Attributes.Add("class", "tab-pane")
            liKAM.Attributes.Remove("class") : divKAM.Attributes.Add("class", "tab-pane")
            liMR.Attributes.Remove("class") : divMR.Attributes.Add("class", "tab-pane")
            If sTab = "AuditDetails" Then
                liAuditDetails.Attributes.Add("class", "active") : divAuditDetails.Attributes.Add("class", "tab-pane active")
            End If
            If sTab = "IFC" Then
                liIFC.Attributes.Add("class", "active") : divIFC.Attributes.Add("class", "tab-pane active")
            End If
            If sTab = "KAM" Then
                liKAM.Attributes.Add("class", "active") : divKAM.Attributes.Add("class", "tab-pane active")
            End If
            If sTab = "MR" Then
                liMR.Attributes.Add("class", "active") : divMR.Attributes.Add("class", "tab-pane active")
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim iCustID As Integer = 0
        Try
            lblError.Text = ""
            ClearAuditDetails() : ClearIFCbasicDetails() : ClearIFCDetails()
            SetActiveTabs("AuditDetails")
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
            lblError.Text = ""
            ClearAuditDetails() : ClearIFCbasicDetails() : ClearIFCDetails()
            SetActiveTabs("AuditDetails")
            If ddlAuditNo.SelectedIndex > 0 Then
                BindScheduledDetails(ddlAuditNo.SelectedValue)
                BindSAAuditSummaryDashboard(ddlCustomerName.SelectedValue)
                BindSADRLSummary() : BindSAObservationsQuerySummary() : BindSADRLdetails()
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
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            If dt.Rows.Count = 1 Then
                ddlCustomerName.SelectedValue = dt.Rows(0)("SA_CustID")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDashboard_PreRender(sender As Object, e As EventArgs) Handles gvDashboard.PreRender
        Try
            If gvDashboard.Rows.Count > 0 Then
                gvDashboard.UseAccessibleHeader = True
                gvDashboard.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDashboard.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCheckPoint_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindSAAuditSummaryDashboard(ByVal iCustomerID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadSAAuditSummaryDashboard(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustomerID, ddlAuditNo.SelectedValue, sSession.UserID, bLoginUserIsPartner)
            gvDashboard.DataSource = dt
            gvDashboard.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSAAuditSummaryDashboard" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDRLSummary_PreRender(sender As Object, e As EventArgs) Handles gvDRLSummary.PreRender
        Try
            If gvDRLSummary.Rows.Count > 0 Then
                gvDRLSummary.UseAccessibleHeader = True
                gvDRLSummary.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDRLSummary.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDRLSummary_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindSADRLSummary()
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadSADRLSummary(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            gvDRLSummary.DataSource = dt
            gvDRLSummary.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSADRLSummary" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvObservationsQuerySummary_PreRender(sender As Object, e As EventArgs) Handles gvObservationsQuerySummary.PreRender
        Try
            If gvObservationsQuerySummary.Rows.Count > 0 Then
                gvObservationsQuerySummary.UseAccessibleHeader = True
                gvObservationsQuerySummary.HeaderRow.TableSection = TableRowSection.TableHeader
                gvObservationsQuerySummary.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvObservationsQuerySummary_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindSAObservationsQuerySummary()
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadSAObservationsQuerySummary(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            gvObservationsQuerySummary.DataSource = dt
            gvObservationsQuerySummary.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSAObservationsQuerySummary" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCheckpointSummary_PreRender(sender As Object, e As EventArgs) Handles gvCheckpointSummary.PreRender
        Try
            If gvCheckpointSummary.Rows.Count > 0 Then
                gvCheckpointSummary.UseAccessibleHeader = True
                gvCheckpointSummary.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCheckpointSummary.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCheckpointSummary_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindSADRLdetails()
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadSACheckpointSummary(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            gvCheckpointSummary.DataSource = dt
            gvCheckpointSummary.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSADRLdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnAuditDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnAuditDetails.Click
        Try
            lblError.Text = ""
            imgbtnReport.Visible = False
            SetActiveTabs("AuditDetails")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAuditDetails_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnIFC_Click(sender As Object, e As EventArgs) Handles lnkbtnIFC.Click
        Try
            lblError.Text = ""
            iTabID = 1
            imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False : imgbtnReport.Visible = True
            ClearIFCbasicDetails() : ClearIFCDetails()
            If ddlAuditNo.SelectedIndex > 0 Then
                SetActiveTabs("IFC")
                BindStandardAuditASIFCbasicDetails()
                imgbtnReport.Visible = True
            Else
                SetActiveTabs("AuditDetails")
                lblError.Text = "Select Audit No." : lblAuditSummaryValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                ddlAuditNo.Focus()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnIFC_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnKAM_Click(sender As Object, e As EventArgs) Handles lnkbtnKAM.Click
        Try
            lblError.Text = ""
            iTabID = 2
            imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False : imgbtnReport.Visible = True
            If ddlAuditNo.SelectedIndex > 0 Then
                SetActiveTabs("KAM")
                BindStandardAuditASKAMbasicDetails()
                imgbtnReport.Visible = True
            Else
                SetActiveTabs("AuditDetails")
                lblError.Text = "Select Audit No." : lblAuditSummaryValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                ddlAuditNo.Focus()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnKAM_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnMR_Click(sender As Object, e As EventArgs) Handles lnkbtnMR.Click
        Try
            lblError.Text = ""
            iTabID = 3
            imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False : imgbtnReport.Visible = True
            If ddlAuditNo.SelectedIndex > 0 Then
                SetActiveTabs("MR")
                BindStandardAuditASMRdetails()
                imgbtnReport.Visible = True
            Else
                SetActiveTabs("AuditDetails")
                lblError.Text = "Select Audit No." : lblAuditSummaryValidationMsg.Text = "Select Audit No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                ddlAuditNo.Focus()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnMR_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Function MSAccessOpenConnection(ByVal sFile As String) As OleDb.OleDbConnection
        Dim con As New OleDb.OleDbConnection
        Try
            con.ConnectionString = "Provider=Microsoft.Jet.OLEDB.8.0;Data Source=" & sFile & ";Extended Properties=Excel 8.0;"
            con.Open()
            Return con
        Catch ex As Exception
        End Try
        Try
            con.ConnectionString = "Data Source=" & sFile & ";Provider=Microsoft.ACE.OLEDB.12.0; Extended Properties=Excel 12.0;"
            con.Open()
            Return con
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MSAccessOpenConnection" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            dgExcelIFC.DataSource = Nothing : dgExcelIFC.DataBind()
            dgExcelIFCDetails.DataSource = Nothing : dgExcelIFCDetails.DataBind()
            lblSheetName.Visible = False : ddlIFCSheetName.Visible = False
            lblHIFCExcelColumns.Visible = False : btnOkIFCExcelColumns.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False
            lstIFCExcelColumns.Visible = False : lstIFCExcelColumns.DataSource = Nothing : lstIFCExcelColumns.DataBind()
            imgbtnSaveIFC.Visible = False
            If FUIFCLoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlIFCSheetName.Visible = True
                sExt = IO.Path.GetExtension(FUIFCLoad.PostedFile.FileName)
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FUIFCLoad.PostedFile.FileName)
                    sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If
                    FUIFCLoad.PostedFile.SaveAs(sFile)
                    ddlIFCSheetName.Items.Clear()
                    dt = ExcelSheetNames(sFile)
                    ddlIFCSheetName.DataSource = dt
                    ddlIFCSheetName.DataTextField = "Name"
                    ddlIFCSheetName.DataValueField = "ID"
                    ddlIFCSheetName.DataBind()
                    ddlIFCSheetName.Items.Insert(0, "Select Sheet")
                Else
                    lblError.Text = "Select Excel file only." : lblAuditSummaryValidationMsg.Text = "Select Excel file only."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAuditSummaryValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblAuditSummaryValidationMsg.Text = "Select Excel file."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAuditSummaryValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function ExcelSheetNames(ByVal sPath As String) As DataTable
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            XLCon = MSAccessOpenConnection(sPath)
            dt = XLCon.GetOleDbSchemaTable(OleDb.OleDbSchemaGuid.Tables, New Object() {Nothing, Nothing, Nothing, "TABLE"})
            If dt.Rows.Count > 0 Then
                dtTab.Columns.Add("ID")
                dtTab.Columns.Add("Name")
                For i = 0 To dt.Rows.Count - 1
                    drow = dtTab.NewRow
                    drow("ID") = i + 1
                    drow("Name") = dt.Rows(i)(2)
                    dtTab.Rows.Add(drow)
                Next
            End If
            XLCon.Close()
            Return dtTab
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExcelSheetNames" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub ddlIFCSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlIFCSheetName.SelectedIndexChanged
        Dim objDBL As New DBHelper
        Try
            lblError.Text = ""
            dgExcelIFC.DataSource = Nothing : dgExcelIFC.DataBind()
            dgExcelIFCDetails.DataSource = Nothing : dgExcelIFCDetails.DataBind()
            lblHIFCExcelColumns.Visible = False : btnOkIFCExcelColumns.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False
            lstIFCExcelColumns.Visible = False : lstIFCExcelColumns.DataSource = Nothing : lstIFCExcelColumns.DataBind()
            imgbtnSaveIFC.Visible = False
            If ddlIFCSheetName.SelectedIndex > 0 Then
                If File.Exists(sFile) = True Then
                    dtOriginalExcel = objDBL.ReadExcel("Select * from [" & Trim(ddlIFCSheetName.SelectedItem.Text) & "] ", sFile)

                    lblHIFCExcelColumns.Visible = True : btnOkIFCExcelColumns.Visible = True : lstIFCExcelColumns.Visible = True
                    lstIFCExcelColumns.DataSource = LoadExcelColumns(dtOriginalExcel)
                    lstIFCExcelColumns.DataTextField = "HeaderName"
                    lstIFCExcelColumns.DataValueField = "HeaderID"
                    lstIFCExcelColumns.DataBind()

                    If lstIFCExcelColumns.Items.Count <= 6 Then
                        For Each item In lstIFCExcelColumns.Items
                            item.Selected = True
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlIFCSheetName.SelectedIndex = 0
                lblError.Text = "Invalid Excel format in selected sheet." : lblAuditSummaryValidationMsg.Text = "Invalid Excel format in selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAuditSummaryValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlIFCSheetName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadExcelColumns(ByVal dt As DataTable) As DataTable
        Dim i As Integer
        Dim drow As DataRow
        Dim dtExcelColumns As New DataTable
        Try
            dtExcelColumns = New DataTable
            dtExcelColumns.Columns.Add("HeaderID")
            dtExcelColumns.Columns.Add("HeaderName")
            If dt.Columns.Count > 0 Then
                For i = 0 To dt.Columns.Count - 1
                    If Trim(dt.Columns(i).ColumnName) <> "&nbsp;" Then
                        drow = dtExcelColumns.NewRow
                        drow("HeaderID") = i
                        drow("HeaderName") = dt.Columns(i).ColumnName
                        dtExcelColumns.Rows.Add(drow)
                    End If
                Next
            End If
            Return dtExcelColumns
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub btnOkIFCExcelColumns_Click(sender As Object, e As EventArgs) Handles btnOkIFCExcelColumns.Click
        Dim dt As New DataTable
        Dim selectedColumnCount As Integer = 0
        Try
            lblError.Text = ""
            dgExcelIFC.DataSource = Nothing : dgExcelIFC.DataBind()
            dgExcelIFCDetails.DataSource = Nothing : dgExcelIFCDetails.DataBind()
            Dim selectedColumnsIndices As New List(Of Integer)()
            For i As Integer = 0 To lstIFCExcelColumns.Items.Count - 1
                If lstIFCExcelColumns.Items(i).Selected Then
                    selectedColumnCount += 1
                    selectedColumnsIndices.Add(lstIFCExcelColumns.Items(i).Value)
                End If
                If selectedColumnCount > 6 Then
                    lblError.Text = "Select max 6 columns from selected excel sheet." : lblAuditSummaryValidationMsg.Text = "Select max 6 columns from selected excel sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAuditSummaryValidation').modal('show');", True)
                    Exit Sub
                End If
            Next
            If selectedColumnCount = 0 Then
                lblError.Text = "Select atleast 1 columns from selected excel sheet." : lblAuditSummaryValidationMsg.Text = "Select atleast 1 columns from selected excel sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalAuditSummaryValidation').modal('show');", True)
                Exit Sub
            End If
            imgbtnSaveIFC.Visible = True : btnConfirmIFCExcelColumns.Visible = True : lblIFCNote.Visible = True : iIFCcolumnCount = selectedColumnCount
            Dim selectedColumnsIndicesArray As Integer() = selectedColumnsIndices.ToArray()
            dgExcelIFC.DataSource = SelectColumnsByIndex(dtOriginalExcel, selectedColumnsIndicesArray)
            dgExcelIFC.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOkIFCExcelColumns_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnConfirmIFCExcelColumns_Click(sender As Object, e As EventArgs) Handles btnConfirmIFCExcelColumns.Click
        Dim dt As New DataTable
        Dim sColumn1 As String = "", sColumn2 As String = "", sColumn3 As String = "", sColumn4 As String = "", sColumn5 As String = "", sColumn6 As String = ""
        Try
            lblError.Text = ""
            dgExcelIFC.DataSource = dt : dgExcelIFC.DataBind()
            dgExcelIFCDetails.DataSource = dt : dgExcelIFCDetails.DataBind()
            If dtSlectedColumns.Rows.Count = 0 Then
                lblError.Text = "No Data."
                Exit Sub
            End If
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
            If txtIFCReportDate.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Report Date." : lblError.Text = "Enter Report Date."
                txtIFCReportDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCReportDate').focus();", True)
                Exit Sub
            End If
            If txtIFCReportBy.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Report By." : lblError.Text = "Enter Report By."
                txtIFCReportBy.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCReportBy').focus();", True)
                Exit Sub
            End If
            If txtIFCReportBy.Text.Trim.Length > 100 Then
                lblAuditSummaryValidationMsg.Text = "Report By exceeded maximum size(max 100 characters)." : lblError.Text = "Report By exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCReportBy').focus();", True)
                txtIFCReportBy.Focus()
                Exit Sub
            End If
            If txtIFCComments.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Comments." : lblError.Text = "Enter Comments."
                txtIFCComments.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCComments').focus();", True)
                Exit Sub
            End If
            If txtIFCComments.Text.Trim.Length > 5000 Then
                lblAuditSummaryValidationMsg.Text = "Comments exceeded maximum size(max 5000 characters)." : lblError.Text = "Comments exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCComments').focus();", True)
                txtIFCComments.Focus()
                Exit Sub
            End If

            Dim objSAIFC As New strStandardAudit_AuditSummary_IFC
            objSAIFC.iSAIFC_PKID = 0
            objSAIFC.iSAIFC_SA_ID = ddlAuditNo.SelectedValue
            objSAIFC.iSAIFC_CustID = ddlCustomerName.SelectedValue
            objSAIFC.iSAIFC_YearID = ddlFinancialYear.SelectedValue
            objSAIFC.dSAIFC_ReportDate = Date.ParseExact(txtIFCReportDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSAIFC.sSAIFC_ReportBy = objclsGRACeGeneral.SafeSQL(txtIFCReportBy.Text.ToString())
            objSAIFC.sSAIFC_Comments = objclsGRACeGeneral.SafeSQL(txtIFCComments.Text.ToString())
            objSAIFC.iSAIFC_ColumnCount = iIFCcolumnCount
            objSAIFC.iSAIFC_AttachID = 0
            objSAIFC.iSAIFC_CrBy = sSession.UserID
            objSAIFC.sSAIFC_IPAddress = sSession.IPAddress
            objSAIFC.iSAIFC_CompID = sSession.AccessCodeID
            Dim ArrIFC() As String = objclsSAAuditSummary.SaveUpdateStandardAuditASIFC(sSession.AccessCode, objSAIFC)
            Dim iIFCPKID As Integer = ArrIFC(1)
            For columnIndex As Integer = 0 To Math.Min(iIFCcolumnCount, dtSlectedColumns.Columns.Count - 1)
                Dim headerText As String = dtSlectedColumns.Columns(columnIndex).ColumnName
                Select Case columnIndex
                    Case 0
                        sColumn1 = headerText.ToString().Trim()
                    Case 1
                        sColumn2 = headerText.ToString().Trim()
                    Case 2
                        sColumn3 = headerText.ToString().Trim()
                    Case 3
                        sColumn4 = headerText.ToString().Trim()
                    Case 4
                        sColumn5 = headerText.ToString().Trim()
                    Case 5
                        sColumn6 = headerText.ToString().Trim()
                End Select
            Next

            Dim objSAIFCD1 As New strStandardAudit_AuditSummary_IFCDetails
            objSAIFCD1.iSAIFCD_PKID = 0
            objSAIFCD1.iSAIFCD_SAIFC_PKID = iIFCPKID
            objSAIFCD1.iSAIFCD_ColumnRowType = 0
            objSAIFCD1.sSAIFCD_Column1 = sColumn1
            objSAIFCD1.sSAIFCD_Column2 = sColumn2
            objSAIFCD1.sSAIFCD_Column3 = sColumn3
            objSAIFCD1.sSAIFCD_Column4 = sColumn4
            objSAIFCD1.sSAIFCD_Column5 = sColumn5
            objSAIFCD1.sSAIFCD_Column6 = sColumn6
            objclsSAAuditSummary.SaveStandardAuditASIFCdetails(sSession.AccessCode, objSAIFCD1)

            For i = 0 To dtSlectedColumns.Rows.Count - 1
                sColumn1 = "" : sColumn2 = "" : sColumn3 = "" : sColumn4 = "" : sColumn5 = "" : sColumn6 = ""
                For columnIndex As Integer = 0 To Math.Min(iIFCcolumnCount, dtSlectedColumns.Columns.Count - 1)
                    Dim cellText As String = ""
                    If IsDBNull(dtSlectedColumns.Rows(i)(columnIndex)) = False Then
                        cellText = objclsGRACeGeneral.SafeSQL(dtSlectedColumns.Rows(i)(columnIndex))
                    End If
                    Select Case columnIndex
                        Case 0
                            sColumn1 = cellText.ToString().Trim()
                        Case 1
                            sColumn2 = cellText.ToString().Trim()
                        Case 2
                            sColumn3 = cellText.ToString().Trim()
                        Case 3
                            sColumn4 = cellText.ToString().Trim()
                        Case 4
                            sColumn5 = cellText.ToString().Trim()
                        Case 5
                            sColumn6 = cellText.ToString().Trim()
                    End Select
                Next

                Dim objSAIFCD As New strStandardAudit_AuditSummary_IFCDetails
                objSAIFCD.iSAIFCD_PKID = 0
                objSAIFCD.iSAIFCD_SAIFC_PKID = iIFCPKID
                objSAIFCD.iSAIFCD_ColumnRowType = 1
                objSAIFCD.sSAIFCD_Column1 = sColumn1
                objSAIFCD.sSAIFCD_Column2 = sColumn2
                objSAIFCD.sSAIFCD_Column3 = sColumn3
                objSAIFCD.sSAIFCD_Column4 = sColumn4
                objSAIFCD.sSAIFCD_Column5 = sColumn5
                objSAIFCD.sSAIFCD_Column6 = sColumn6
                objclsSAAuditSummary.SaveStandardAuditASIFCdetails(sSession.AccessCode, objSAIFCD)
            Next
            BindStandardAuditASIFCbasicDetails()

            lblError.Text = "Successfully Saved." : lblAuditSummaryValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditSummaryValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnConfirmIFCExcelColumns_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function SelectColumnsByIndex(originalTable As DataTable, selectedColumnsIndices As Integer()) As DataTable
        Try
            dtSlectedColumns = New DataTable()
            For Each colIndex As Integer In selectedColumnsIndices
                If colIndex >= 0 AndAlso colIndex < originalTable.Columns.Count Then
                    dtSlectedColumns.Columns.Add(originalTable.Columns(colIndex).ColumnName, originalTable.Columns(colIndex).DataType)
                End If
            Next

            Dim count As Integer = 0
            For Each originalRow As DataRow In originalTable.Rows
                Dim newRow As DataRow = dtSlectedColumns.NewRow()
                For Each newCol As DataColumn In dtSlectedColumns.Columns
                    Dim originalColIndex As Integer = originalTable.Columns.IndexOf(newCol.ColumnName)
                    If originalColIndex >= 0 Then
                        newRow(newCol.ColumnName) = originalRow(originalColIndex)
                    End If
                Next
                dtSlectedColumns.Rows.Add(newRow)
                count += 1
                If count >= 50 Then ' Break the loop after adding 50 rows
                    Exit For
                End If
            Next
            Return dtSlectedColumns
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SelectColumnsByIndex" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub dgExcelIFC_PreRender(sender As Object, e As EventArgs) Handles dgExcelIFC.PreRender
        Try
            If dgExcelIFC.Rows.Count > 0 Then
                dgExcelIFC.UseAccessibleHeader = True
                dgExcelIFC.HeaderRow.TableSection = TableRowSection.TableHeader
                dgExcelIFC.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExcelIFC_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgExcelIFCDetails_PreRender(sender As Object, e As EventArgs) Handles dgExcelIFCDetails.PreRender
        Try
            If dgExcelIFCDetails.Rows.Count > 0 Then
                dgExcelIFCDetails.UseAccessibleHeader = True
                dgExcelIFCDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgExcelIFCDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExcelIFCDetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSaveIFC_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveIFC.Click
        Dim dt As New DataTable
        Dim sColumn1 As String = "", sColumn2 As String = "", sColumn3 As String = "", sColumn4 As String = "", sColumn5 As String = "", sColumn6 As String = ""
        Try
            lblError.Text = ""
            dgExcelIFC.DataSource = dt : dgExcelIFC.DataBind()
            dgExcelIFCDetails.DataSource = dt : dgExcelIFCDetails.DataBind()
            If dtSlectedColumns.Rows.Count = 0 Then
                lblError.Text = "No Data."
                Exit Sub
            End If
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
            If txtIFCReportDate.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Report Date." : lblError.Text = "Enter Report Date."
                txtIFCReportDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCReportDate').focus();", True)
                Exit Sub
            End If
            If txtIFCReportBy.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Report By." : lblError.Text = "Enter Report By."
                txtIFCReportBy.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCReportBy').focus();", True)
                Exit Sub
            End If
            If txtIFCReportBy.Text.Trim.Length > 100 Then
                lblAuditSummaryValidationMsg.Text = "Report By exceeded maximum size(max 100 characters)." : lblError.Text = "Report By exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCReportBy').focus();", True)
                txtIFCReportBy.Focus()
                Exit Sub
            End If
            If txtIFCComments.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Comments." : lblError.Text = "Enter Comments."
                txtIFCComments.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCComments').focus();", True)
                Exit Sub
            End If
            If txtIFCComments.Text.Trim.Length > 5000 Then
                lblAuditSummaryValidationMsg.Text = "Comments exceeded maximum size(max 5000 characters)." : lblError.Text = "Comments exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCComments').focus();", True)
                txtIFCComments.Focus()
                Exit Sub
            End If

            Dim objSAIFC As New strStandardAudit_AuditSummary_IFC
            objSAIFC.iSAIFC_PKID = 0
            objSAIFC.iSAIFC_SA_ID = ddlAuditNo.SelectedValue
            objSAIFC.iSAIFC_CustID = ddlCustomerName.SelectedValue
            objSAIFC.iSAIFC_YearID = ddlFinancialYear.SelectedValue
            objSAIFC.dSAIFC_ReportDate = Date.ParseExact(txtIFCReportDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSAIFC.sSAIFC_ReportBy = objclsGRACeGeneral.SafeSQL(txtIFCReportBy.Text.ToString())
            objSAIFC.sSAIFC_Comments = objclsGRACeGeneral.SafeSQL(txtIFCComments.Text.ToString())
            objSAIFC.iSAIFC_ColumnCount = iIFCcolumnCount
            objSAIFC.iSAIFC_AttachID = 0
            objSAIFC.iSAIFC_CrBy = sSession.UserID
            objSAIFC.sSAIFC_IPAddress = sSession.IPAddress
            objSAIFC.iSAIFC_CompID = sSession.AccessCodeID
            Dim ArrIFC() As String = objclsSAAuditSummary.SaveUpdateStandardAuditASIFC(sSession.AccessCode, objSAIFC)
            Dim iIFCPKID As Integer = ArrIFC(1)
            For columnIndex As Integer = 0 To Math.Min(iIFCcolumnCount, dtSlectedColumns.Columns.Count - 1)
                Dim headerText As String = dtSlectedColumns.Columns(columnIndex).ColumnName
                Select Case columnIndex
                    Case 0
                        sColumn1 = headerText.ToString().Trim()
                    Case 1
                        sColumn2 = headerText.ToString().Trim()
                    Case 2
                        sColumn3 = headerText.ToString().Trim()
                    Case 3
                        sColumn4 = headerText.ToString().Trim()
                    Case 4
                        sColumn5 = headerText.ToString().Trim()
                    Case 5
                        sColumn6 = headerText.ToString().Trim()
                End Select
            Next

            Dim objSAIFCD1 As New strStandardAudit_AuditSummary_IFCDetails
            objSAIFCD1.iSAIFCD_PKID = 0
            objSAIFCD1.iSAIFCD_SAIFC_PKID = iIFCPKID
            objSAIFCD1.iSAIFCD_ColumnRowType = 0
            objSAIFCD1.sSAIFCD_Column1 = sColumn1
            objSAIFCD1.sSAIFCD_Column2 = sColumn2
            objSAIFCD1.sSAIFCD_Column3 = sColumn3
            objSAIFCD1.sSAIFCD_Column4 = sColumn4
            objSAIFCD1.sSAIFCD_Column5 = sColumn5
            objSAIFCD1.sSAIFCD_Column6 = sColumn6
            objclsSAAuditSummary.SaveStandardAuditASIFCdetails(sSession.AccessCode, objSAIFCD1)

            For i = 0 To dtSlectedColumns.Rows.Count - 1
                sColumn1 = "" : sColumn2 = "" : sColumn3 = "" : sColumn4 = "" : sColumn5 = "" : sColumn6 = ""
                For columnIndex As Integer = 0 To Math.Min(iIFCcolumnCount, dtSlectedColumns.Columns.Count - 1)
                    Dim cellText As String = ""
                    If IsDBNull(dtSlectedColumns.Rows(i)(columnIndex)) = False Then
                        cellText = objclsGRACeGeneral.SafeSQL(dtSlectedColumns.Rows(i)(columnIndex))
                    End If
                    Select Case columnIndex
                        Case 0
                            sColumn1 = cellText.ToString().Trim()
                        Case 1
                            sColumn2 = cellText.ToString().Trim()
                        Case 2
                            sColumn3 = cellText.ToString().Trim()
                        Case 3
                            sColumn4 = cellText.ToString().Trim()
                        Case 4
                            sColumn5 = cellText.ToString().Trim()
                        Case 5
                            sColumn6 = cellText.ToString().Trim()
                    End Select
                Next

                Dim objSAIFCD As New strStandardAudit_AuditSummary_IFCDetails
                objSAIFCD.iSAIFCD_PKID = 0
                objSAIFCD.iSAIFCD_SAIFC_PKID = iIFCPKID
                objSAIFCD.iSAIFCD_ColumnRowType = 1
                objSAIFCD.sSAIFCD_Column1 = sColumn1
                objSAIFCD.sSAIFCD_Column2 = sColumn2
                objSAIFCD.sSAIFCD_Column3 = sColumn3
                objSAIFCD.sSAIFCD_Column4 = sColumn4
                objSAIFCD.sSAIFCD_Column5 = sColumn5
                objSAIFCD.sSAIFCD_Column6 = sColumn6
                objclsSAAuditSummary.SaveStandardAuditASIFCdetails(sSession.AccessCode, objSAIFCD)
            Next
            BindStandardAuditASIFCbasicDetails()

            lblError.Text = "Successfully Saved." : lblAuditSummaryValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditSummaryValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStandardAuditASIFCbasicDetails()
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            imgbtnSaveIFC.Visible = False : btnConfirmIFCExcelColumns.Visible = False : lblIFCNote.Visible = False
            ClearIFCbasicDetails() : ClearIFCDetails()
            dt = objclsSAAuditSummary.LoadStandardAuditASIFCbasicDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            If dt.Rows.Count > 0 Then
                lblSelectFile.Visible = False : FUIFCLoad.Visible = False : btnOk.Visible = False
                If IsDBNull(dt.Rows(0)("SAIFC_ColumnCount")) = False Then
                    iIFCcolumnCount = dt.Rows(0)("SAIFC_ColumnCount")
                End If
                If IsDBNull(dt.Rows(0)("SAIFC_ReportDate")) = False Then
                    txtIFCReportDate.Text = dt.Rows(0)("SAIFC_ReportDate")
                End If
                If IsDBNull(dt.Rows(0)("SAIFC_ReportBy")) = False Then
                    txtIFCReportBy.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SAIFC_ReportBy").ToString())
                End If
                If IsDBNull(dt.Rows(0)("SAIFC_Comments")) = False Then
                    txtIFCComments.Text = dt.Rows(0)("SAIFC_Comments")
                End If

                dtTab = objclsSAAuditSummary.LoadStandardAuditASIFCdetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                dgExcelIFCDetails.DataSource = dtTab
                dgExcelIFCDetails.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSADRLdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStandardAuditASIFCselectedDetails(ByVal iIFCDPKID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsSAAuditSummary.LoadStandardAuditASIFCselectedDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iIFCDPKID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("SAIFCD_DateOfTesting")) = False Then
                    txtIFCDateOfTesting.Text = dt.Rows(0)("SAIFCD_DateOfTesting")
                End If
                If IsDBNull(dt.Rows(0)("SAIFCD_TypeOfTestingDetails")) = False Then
                    txtIFCTestingDetails.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SAIFCD_TypeOfTestingDetails").ToString())
                End If
                If IsDBNull(dt.Rows(0)("SAIFCD_SampleSizeUsed")) = False Then
                    txtIFCSampleSizeUsed.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SAIFCD_SampleSizeUsed"))
                End If
                If IsDBNull(dt.Rows(0)("SAIFCD_Conclusion")) = False Then
                    ddlIFCConclusion.SelectedIndex = dt.Rows(0)("SAIFCD_Conclusion")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSADRLdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgExcelIFCDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgExcelIFCDetails.RowDataBound
        Dim lblAttachmentID As New Label, lblBadgeCount As New Label
        Try
            If e.Row.RowType = DataControlRowType.Header Then
                Dim iColumnCount As Integer = objclsSAAuditSummary.GetStandardAuditASIFCcolumnCount(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                For i As Integer = 1 To 6
                    dgExcelIFCDetails.Columns(i).Visible = False
                    If i <= iColumnCount Then
                        dgExcelIFCDetails.Columns(i).Visible = True
                        If dgExcelIFCDetails.Columns(i).HeaderText.Contains("SAIFCD_Column") = True Then
                            dgExcelIFCDetails.Columns(i).HeaderText = objclsSAAuditSummary.GetStandardAuditASIFCcolumnHeaderName(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, dgExcelIFCDetails.Columns(i).HeaderText)
                        End If
                    End If
                Next
            End If
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExcelIFCDetails_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgExcelIFCDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgExcelIFCDetails.RowCommand
        Dim lblDescription As New Label, lblDescID As New Label, lblAttachmentID As New Label
        Dim lblDBpkId As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Comments" Then
                ClearIFCDetails()
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iIFCDPKID = Val(lblDBpkId.Text)
                BindStandardAuditASIFCselectedDetails(iIFCDPKID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myIFCObservationModal').modal('show');", True)
            End If
            If e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iIFCDPKID = Val(lblDBpkId.Text)

                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iAttachID = Val(lblAttachmentID.Text)
                BindAllAttachments(iAttachID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgExcelIFCDetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSaveIFCObservationsComments_Click(sender As Object, e As EventArgs)
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
            If txtIFCSampleSizeUsed.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Sample Size Used." : lblError.Text = "Enter Sample Size Used."
                txtIFCSampleSizeUsed.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCSampleSizeUsed').focus();", True)
                Exit Sub
            End If
            If ddlIFCConclusion.SelectedIndex = 0 Then
                lblError.Text = "Select Conclusion." : lblAuditSummaryValidationMsg.Text = "Select Conclusion."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlIFCConclusion').focus();", True)
                ddlIFCConclusion.Focus()
                Exit Sub
            End If
            If txtIFCSampleSizeUsed.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Sample Size Used." : lblError.Text = "Enter Sample Size Used."
                txtIFCSampleSizeUsed.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCSampleSizeUsed').focus();", True)
                Exit Sub
            End If
            If txtIFCSampleSizeUsed.Text.Trim.Length > 500 Then
                lblAuditSummaryValidationMsg.Text = "Sample Size Used exceeded maximum size(max 500 characters)." : lblError.Text = "Sample Size Used exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCSampleSizeUsed').focus();", True)
                txtIFCSampleSizeUsed.Focus()
                Exit Sub
            End If
            If txtIFCTestingDetails.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Testing Details." : lblError.Text = "Enter Testing Details."
                txtIFCTestingDetails.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCTestingDetails').focus();", True)
                Exit Sub
            End If
            If txtIFCTestingDetails.Text.Trim.Length > 8000 Then
                lblAuditSummaryValidationMsg.Text = "Testing Details exceeded maximum size(max 8000 characters)." : lblError.Text = "Testing Details exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCTestingDetails').focus();", True)
                txtIFCTestingDetails.Focus()
                Exit Sub
            End If

            objclsSAAuditSummary.UpdateStandardAuditASIFCdetails(sSession.AccessCode, sSession.AccessCodeID, iIFCDPKID, Date.ParseExact(txtIFCDateOfTesting.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), objclsGRACeGeneral.SafeSQL(txtIFCTestingDetails.Text.Trim), objclsGRACeGeneral.SafeSQL(txtIFCSampleSizeUsed.Text), ddlIFCConclusion.SelectedIndex, sSession.UserID)
            BindStandardAuditASIFCbasicDetails()
            lblError.Text = "Successfully Saved." : lblAuditSummaryValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditSummaryValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveIFCObservationsComments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgKAMdetails_PreRender(sender As Object, e As EventArgs) Handles dgKAMdetails.PreRender
        Try
            If dgKAMdetails.Rows.Count > 0 Then
                dgKAMdetails.UseAccessibleHeader = True
                dgKAMdetails.HeaderRow.TableSection = TableRowSection.TableHeader
                dgKAMdetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgKAMdetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStandardAuditASKAMbasicDetails()
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            dgKAMdetails.DataSource = dtTab : dgKAMdetails.DataBind()
            dt = objclsSAAuditSummary.LoadStandardAuditIFCtoKAM(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            dgKAMdetails.DataSource = dt
            dgKAMdetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSADRLdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindStandardAuditASKAMselectedDetails(ByVal iIFCDPKID As Integer, ByVal iKAMDPKID As Integer)
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            dt = objclsSAAuditSummary.LoadStandardAuditASKAMselectedDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iIFCDPKID, iKAMDPKID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("SAIFCD_TypeOfTestingDetails")) = False Then
                    lblKAMDesc.Text = dt.Rows(0)("SAIFCD_TypeOfTestingDetails")
                End If
                If IsDBNull(dt.Rows(0)("SAKAM_DescriptionOrReasonForSelectionAsKAM")) = False Then
                    txtDescriptionOrReasonForSelectionAsKAM.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SAKAM_DescriptionOrReasonForSelectionAsKAM").ToString())
                End If
                If IsDBNull(dt.Rows(0)("SAKAM_AuditProcedureUndertakenToAddressTheKAM")) = False Then
                    txtAuditProcedureUndertakenToAddressTheKAM.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SAKAM_AuditProcedureUndertakenToAddressTheKAM").ToString())
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSADRLdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgKAMdetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgKAMdetails.RowDataBound
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgKAMdetails_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgKAMdetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgKAMdetails.RowCommand
        Dim lblDescription As New Label, lblDescID As New Label, lblAttachmentID As New Label
        Dim lblDBpkId As New Label, lblIFCDpkId As New Label, lblKAM As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "Comments" Then
                ClearIFCDetails()
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                lblIFCDpkId = CType(clickedRow.FindControl("lblIFCDpkId"), Label)
                lblKAM = CType(clickedRow.FindControl("lblKAM"), Label)
                lblKAMDesc.Text = lblKAM.Text
                iKAMDPKID = Val(lblDBpkId.Text)
                iIFCDPKID = Val(lblIFCDpkId.Text)
                BindStandardAuditASKAMselectedDetails(iIFCDPKID, iKAMDPKID)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myKAMObservationModal').modal('show');", True)
            End If
            If e.CommandName = "Attachment" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iKAMDPKID = Val(lblDBpkId.Text)
                iAttachID = 0 : lblAttachmentID = CType(clickedRow.FindControl("lblAttachmentID"), Label)
                lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
                iAttachID = Val(lblAttachmentID.Text)
                If iKAMDPKID > 0 Then
                    BindAllAttachments(iAttachID)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
                Else
                    lblAuditSummaryValidationMsg.Text = "Please save KAM details before adding attachments." : lblError.Text = "Please save KAM details before adding attachments."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtIFCTestingDetails').focus();", True)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgKAMdetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSaveKAMObservationsComments_Click(sender As Object, e As EventArgs)
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
            If txtDescriptionOrReasonForSelectionAsKAM.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Description or Reason for selection as KAM." : lblError.Text = "Enter Description or Reason for selection as KAM."
                txtDescriptionOrReasonForSelectionAsKAM.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtDescriptionOrReasonForSelectionAsKAM').focus();", True)
                Exit Sub
            End If
            If txtDescriptionOrReasonForSelectionAsKAM.Text.Trim.Length > 8000 Then
                lblAuditSummaryValidationMsg.Text = "Description or Reason for selection as KAM exceeded maximum size(max 8000 characters)." : lblError.Text = "Description or Reason for selection as KAM exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtDescriptionOrReasonForSelectionAsKAM').focus();", True)
                txtDescriptionOrReasonForSelectionAsKAM.Focus()
                Exit Sub
            End If
            If txtAuditProcedureUndertakenToAddressTheKAM.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Audit Procedure undertaken to address the KAM." : lblError.Text = "Enter Audit Procedure undertaken to address the KAM."
                txtAuditProcedureUndertakenToAddressTheKAM.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtAuditProcedureUndertakenToAddressTheKAM').focus();", True)
                Exit Sub
            End If
            If txtAuditProcedureUndertakenToAddressTheKAM.Text.Trim.Length > 8000 Then
                lblAuditSummaryValidationMsg.Text = "Audit Procedure undertaken to address the KAM exceeded maximum size(max 8000 characters)." : lblError.Text = "Audit Procedure undertaken to address the KAM exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtAuditProcedureUndertakenToAddressTheKAM').focus();", True)
                txtAuditProcedureUndertakenToAddressTheKAM.Focus()
                Exit Sub
            End If

            objclsSAAuditSummary.UpdateStandardAuditASKAMdetails(sSession.AccessCode, sSession.AccessCodeID, iIFCDPKID, iKAMDPKID, objclsGRACeGeneral.SafeSQL(txtDescriptionOrReasonForSelectionAsKAM.Text.Trim), objclsGRACeGeneral.SafeSQL(txtAuditProcedureUndertakenToAddressTheKAM.Text.Trim), sSession.UserID)
            BindStandardAuditASKAMbasicDetails()

            lblError.Text = "Successfully Saved." : lblAuditSummaryValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditSummaryValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveKAMObservationsComments_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
                    If iTabID = 1 Then
                        objclsSAAuditSummary.UpdateStandardAuditASIFCAttachmentdetails(sSession.AccessCode, sSession.AccessCodeID, iIFCDPKID, iAttachID)
                        Dim dtTab As DataTable = objclsSAAuditSummary.LoadStandardAuditASIFCdetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                        dgExcelIFCDetails.DataSource = dtTab
                        dgExcelIFCDetails.DataBind()
                    ElseIf iTabID = 2 Then
                        objclsSAAuditSummary.UpdateStandardAuditASKAMAttachmentdetails(sSession.AccessCode, sSession.AccessCodeID, iKAMDPKID, iAttachID)
                        BindStandardAuditASKAMbasicDetails()
                    ElseIf iTabID = 3 Then
                        objclsSAAuditSummary.UpdateStandardAuditASMRAttachmentdetails(sSession.AccessCode, sSession.AccessCodeID, iMRPKID, iAttachID)
                        BindStandardAuditASMRdetails()
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
    Protected Sub ddlManagementRepresentations_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManagementRepresentations.SelectedIndexChanged
        Dim iCustID As Integer = 0
        Try
            lblError.Text = ""
            lblMRRHeading.Text = "" : lblMRRDescription.Text = ""
            If ddlManagementRepresentations.SelectedIndex > 0 Then
                lblMRRHeading.Text = ddlManagementRepresentations.SelectedItem.Text
                lblMRRDescription.Text = objclsSAAuditSummary.GetManagementRepresentationsDesc(sSession.AccessCode, sSession.AccessCodeID, ddlManagementRepresentations.SelectedValue)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myMRRModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlManagementRepresentations_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnNewMRR_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            lblMRRHeading.Text = "" : lblMRRDescription.Text = ""
            txtMRDueDateReceiveDocs.Text = "" : txtMRRRemarks.Text = ""
            txtEmailID.Text = objclsSAAuditSummary.GetCustAllUserEmails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            ddlManagementRepresentations.Enabled = True
            ClearMRRequestDetails()
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
            BindManagementRepresentations(ddlFinancialYear.SelectedValue, ddlAuditNo.SelectedValue, 0)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myMRRModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewMRR_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSaveMRRequest_Click(sender As Object, e As EventArgs)
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
            If txtEmailID.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Email ID." : lblError.Text = "Enter Email ID."
                txtEmailID.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtEmailID').focus();", True)
                Exit Sub
            End If
            If txtEmailID.Text.Trim.Length > 500 Then
                lblAuditSummaryValidationMsg.Text = "Email ID exceeded maximum size(max 5000 characters)." : lblError.Text = "Email ID exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtEmailID').focus();", True)
                txtEmailID.Focus()
                Exit Sub
            End If
            If txtMRRRemarks.Text = "" Then
                lblAuditSummaryValidationMsg.Text = "Enter Request Remarks." : lblError.Text = "Enter Request Remarks."
                txtMRRRemarks.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtDescriptionOrReasonForSelectionAsKAM').focus();", True)
                Exit Sub
            End If
            If txtMRRRemarks.Text.Trim.Length > 5000 Then
                lblAuditSummaryValidationMsg.Text = "Request Remarks exceeded maximum size(max 5000 characters)." : lblError.Text = "Request Remarks exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#txtDescriptionOrReasonForSelectionAsKAM').focus();", True)
                txtMRRRemarks.Focus()
                Exit Sub
            End If

            Dim objSAMR As New strStandardAudit_AuditSummary_MRDetails
            objSAMR.iSAMR_PKID = 0
            objSAMR.iSAMR_SA_PKID = ddlAuditNo.SelectedValue
            objSAMR.iSAMR_CustID = ddlCustomerName.SelectedValue
            objSAMR.iSAMR_YearID = ddlFinancialYear.SelectedValue
            objSAMR.iSAMR_MRID = ddlManagementRepresentations.SelectedValue
            objSAMR.dSAMR_RequestedDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSAMR.sSAMR_RequestedByPerson = sSession.UserFullName
            objSAMR.sSAMR_RequestedRemarks = objclsGRACeGeneral.SafeSQL(txtMRRRemarks.Text.ToString())
            objSAMR.dSAMR_DueDateReceiveDocs = Date.ParseExact(txtMRDueDateReceiveDocs.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSAMR.sSAMR_EmailIds = txtEmailID.Text
            objSAMR.iSAMR_CrBy = sSession.UserID
            objSAMR.sSAMR_IPAddress = sSession.IPAddress
            objSAMR.iSAMR_CompID = sSession.AccessCodeID
            Dim ArrMR() As String = objclsSAAuditSummary.SaveStandardAuditASMRdetails(sSession.AccessCode, objSAMR)
            BindStandardAuditASMRdetails()
            lblError.Text = "Successfully Saved." : lblAuditSummaryValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAuditSummaryValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveMRRequest_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindStandardAuditASMRdetails()
        Dim dt As New DataTable, dtTab As New DataTable
        Try
            dgMRdetails.DataSource = dtTab : dgMRdetails.DataBind()
            dt = objclsSAAuditSummary.LoadStandardAuditMR(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
            dgMRdetails.DataSource = dt
            dgMRdetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStandardAuditASMRdetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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

            objclsSAAuditSummary.UpdateStandardAuditASMRdetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iMRPKID, Date.ParseExact(txtMRDueDateReceiveDocs.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), objclsGRACeGeneral.SafeSQL(txtResponsesDetails.Text.ToString()), objclsGRACeGeneral.SafeSQL(txtResponsesRemarks.Text.ToString()))
            BindStandardAuditASMRdetails()
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
                    txtMRDueDateReceiveDocs.Text = dt.Rows(0)("SAMR_DueDateReceiveDocs")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_RequestedRemarks")) = False Then
                    txtMRRRemarks.Text = dt.Rows(0)("SAMR_RequestedRemarks")
                End If
                If IsDBNull(dt.Rows(0)("SAMR_EmailIds")) = False Then
                    txtEmailID.Text = dt.Rows(0)("SAMR_EmailIds")
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
    Protected Sub lnkAuditDetails_Click(sender As Object, e As EventArgs) Handles lnkAuditDetails.Click
        Dim oCustomerID As Object
        Try
            oCustomerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlCustomerName.SelectedValue))
            Response.Redirect(String.Format("~/StandardAudit/DashboardAndSchedule.aspx?CustID={0}", oCustomerID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkAuditDetails_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkDRL_Click(sender As Object, e As EventArgs) Handles lnkDRL.Click
        Dim oAuditID As Object
        Try
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditNo.SelectedValue))
            Response.Redirect(String.Format("~/StandardAudit/DRLSampling.aspx?AuditID={0}", oAuditID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkDRL_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkObservationQuerySummarys_Click(sender As Object, e As EventArgs) Handles lnkObservationQuerySummarys.Click
        Dim oAuditID As Object
        Try
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditNo.SelectedValue))
            Response.Redirect(String.Format("~/StandardAudit/frmAuditLedgerUpload.aspx?AuditID={0}", oAuditID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkObservationQuerySummarys_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkCheckpointSummary_Click(sender As Object, e As EventArgs) Handles lnkCheckpointSummary.Click
        Dim oAuditID As Object
        Try
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditNo.SelectedValue))
            Response.Redirect(String.Format("~/StandardAudit/ConductAudit.aspx?AuditID={0}", oAuditID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkCheckpointSummary_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable, dt1 As New DataTable
        Dim sReportName As String = "AuditSummary"
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            If iTabID = 1 Then
                If dgExcelIFCDetails.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Try
                End If
                dt = objclsSAAuditSummary.LoadStandardAuditASIFCdetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/IFC.rdlc")

                Dim iColumnCount As Integer = objclsSAAuditSummary.GetStandardAuditASIFCcolumnCount(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                For i As Integer = 1 To 6
                    Dim sColumnName As String = "NULL"
                    If i <= iColumnCount Then
                        sColumnName = objclsSAAuditSummary.GetStandardAuditASIFCcolumnHeaderName(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, "SAIFCD_Column" & i)
                    End If

                    Dim paramName As String = "SAIFCD_Column" & i
                    Dim parameters() As ReportParameter = {New ReportParameter(paramName, sColumnName)}
                    ReportViewer1.LocalReport.SetParameters(parameters)
                Next
                sReportName = "IFC"
            ElseIf iTabID = 2 Then
                If dgKAMdetails.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Try
                End If
                dt = objclsSAAuditSummary.LoadStandardAuditIFCtoKAM(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/KAM.rdlc")
                sReportName = "KAM"
            ElseIf iTabID = 3 Then
                If dgMRdetails.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Try
                End If
                dt = objclsSAAuditSummary.LoadStandardAuditMR(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/MR.rdlc")
                sReportName = "MR"
            End If

            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", ddlAuditNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditNo)

            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Conduct Audit", "PDF", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, ddlAuditNo.SelectedValue, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=" & sReportName & "" + ".pdf")
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
        Dim sReportName As String = "AuditSummary"
        Try
            lblError.Text = ""
            ReportViewer1.Reset()
            If iTabID = 1 Then
                If dgExcelIFCDetails.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Try
                End If
                dt = objclsSAAuditSummary.LoadStandardAuditASIFCdetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/IFC.rdlc")
                sReportName = "IFC"
            ElseIf iTabID = 2 Then
                If dgKAMdetails.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Try
                End If
                dt = objclsSAAuditSummary.LoadStandardAuditIFCtoKAM(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/KAM.rdlc")
                sReportName = "KAM"
            ElseIf iTabID = 3 Then
                If dgMRdetails.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Try
                End If
                dt = objclsSAAuditSummary.LoadStandardAuditMR(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue)
                If dt.Rows.Count = 0 Then
                    lblError.Text = "There is no data for the selected Audit." : lblAuditSummaryValidationMsg.Text = "There is no data for the selected Audit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAuditSummaryValidation').modal('show'); $('#ddlAuditNo').focus();", True)
                    Exit Sub
                End If
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/StandardAudit/MR.rdlc")
                sReportName = "MR"
            End If

            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim AuditNo As ReportParameter() = New ReportParameter() {New ReportParameter("AuditNo", ddlAuditNo.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(AuditNo)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Conduct Audit", "Excel", ddlFinancialYear.SelectedValue, ddlFinancialYear.SelectedItem.Text, ddlAuditNo.SelectedValue, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=" & sReportName & "" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class