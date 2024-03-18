Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Imports System.Web.UI.WebControls

Public Class DashboardAndScheduleDeatils
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_DashboardAndScheduleDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsStandardAudit As New clsStandardAudit
    Private objUT As New ClsUploadTailBal
    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iFYID As Integer
    Private Shared iCustID As Integer
    Private Shared iAuditStatusID As Integer
    Private Shared iCheckPointsAndTeamMemberPKID As Integer
    Private Shared sSelectedCheckPointsPKID As String
    Private objDBL As New DBHelper
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim iAuditID As Integer = 0
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                iCustID = 0 : iFYID = 0
                iCheckPointsAndTeamMemberPKID = 0 : sSelectedCheckPointsPKID = ""
                LoadCustomer() : BindPartners() : BindEmployees() : BindSATeamMembers("")
                divCheckPoint.Visible = False : ddlAuditType.Enabled = True
                btnSaveSchedule.Visible = True : btnUpdateSchedule.Visible = False
                If Request.QueryString("FinancialID") IsNot Nothing Then
                    iFYID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FinancialID")))
                    lblFY.Text = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iFYID)
                Else
                    iFYID = sSession.YearID
                    lblFY.Text = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iFYID)
                End If
                If Request.QueryString("CustID") IsNot Nothing Then
                    iCustID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustID")))
                    ddlCustName.SelectedValue = iCustID
                End If
                BindAuditNo(iCustID)
                BindAuditTypes(iFYID, iCustID, 0) : BindHeading(0)
                If Request.QueryString("AuditID") IsNot Nothing Then
                    iAuditID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                    If iAuditID > 0 Then
                        ddlAuditNo.SelectedValue = iAuditID
                        BindSelectedScheduleDetails(ddlAuditNo.SelectedValue)
                    End If
                ElseIf sSession.AuditCodeID > 0 Then
                    ddlAuditNo.SelectedValue = sSession.AuditCodeID
                    BindSelectedScheduleDetails(ddlAuditNo.SelectedValue)
                End If

                RFVCustName.InitialValue = "Select Customer Name" : RFVCustName.ErrorMessage = "Select Customer Name."
                RFVAuditType.InitialValue = "Select Audit Type" : RFVAuditType.ErrorMessage = "Select Audit Type."
                RFVHeading.InitialValue = "Select Heading" : RFVHeading.ErrorMessage = "Select Heading."
                RFVScopeOfAudit.ControlToValidate = "txtScopeOfAudit" : RFVScopeOfAudit.ErrorMessage = "Enter Scope Of Audit."
                REVScopeOfAudit.ErrorMessage = "Scope Of Audit exceeded maximum size(max 5000 characters)." : REVScopeOfAudit.ValidationExpression = "^[\s\S]{0,5000}$"

                RFVTimeLineStartDate.ControlToValidate = "txtTimeLineStartDate" : RFVTimeLineStartDate.ErrorMessage = "Enter Start Date."
                REVTimeLineStartDate.ErrorMessage = "Enter valid Start Date." : REVTimeLineStartDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVExpectedCompletionDate.ControlToValidate = "txtExpectedCompletionDate" : RFVExpectedCompletionDate.ErrorMessage = "Enter Expected Completion Date."
                REVExpectedCompletionDate.ErrorMessage = "Enter valid Expected Completion Date." : REVExpectedCompletionDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVTimeLineRptRvDate.ControlToValidate = "txtReportReviewDate" : RFVTimeLineRptRvDate.ErrorMessage = "Enter Report Review Date."
                REVTimeLineRptRvDate.ErrorMessage = "Enter valid Report Review Date." : REVTimeLineRptRvDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVReportFilingDate.ControlToValidate = "txtReportFilingDate" : RFVReportFilingDate.ErrorMessage = "Enter Report Filing Date."
                REVReportFilingDate.ErrorMessage = "Enter valid Report Filing Date." : REVReportFilingDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVTimeLineMRSDate.ControlToValidate = "txtDateForMRS" : RFVTimeLineMRSDate.ErrorMessage = "Enter Date For MRS."
                REVTimeLineMRSDate.ErrorMessage = "Enter valid Date For MRS." : REVTimeLineMRSDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                REVStartDate.ErrorMessage = "Enter valid Start Date." : REVStartDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                REVEndDate.ErrorMessage = "Enter valid End Date." : REVEndDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function ValidateTextBox(textBox As TextBox, errorMessage As String) As Boolean
        If String.IsNullOrEmpty(textBox.Text) Then
            lblError.Text = errorMessage : lblScheduleModalMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateTextBoxLength(textBox As TextBox, errorMessage As String, maxLength As Integer) As Boolean
        If Not ValidateTextBox(textBox, errorMessage) Then Return False
        If textBox.Text.Length > maxLength Then
            lblError.Text = $"{errorMessage} (max {maxLength} characters)." : lblScheduleModalMsg.Text = $"{errorMessage} (max {maxLength} characters)."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateGridViewNotEmpty(gridView As GridView, errorMessage As String) As Boolean
        If gridView.Rows.Count = 0 Then
            lblError.Text = errorMessage : lblScheduleModalMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateGridViewCheckBox(gridView As GridView, checkBoxID As String, errorMessage As String) As Boolean
        If Not gridView.Rows.Cast(Of GridViewRow)().Any(Function(row) DirectCast(row.FindControl(checkBoxID), CheckBox).Checked) Then
            lblError.Text = errorMessage : lblScheduleModalMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateDropDownListSelectedIndex(dropDownList As DropDownList, errorMessage As String) As Boolean
        If dropDownList.SelectedIndex = 0 Then
            lblError.Text = errorMessage : lblScheduleModalMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateListBoxSelection(listBox As ListBox, errorMessage As String) As Boolean
        If Not listBox.Items.Cast(Of ListItem)().Any(Function(item) item.Selected) Then
            lblError.Text = errorMessage : lblScheduleModalMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Private Function ValidateDateRange(startDate As DateTime, endDate As DateTime, errorMessage As String) As Boolean
        If startDate > endDate Then
            lblError.Text = errorMessage : lblScheduleModalMsg.Text = errorMessage
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
            Return False
        End If
        Return True
    End Function
    Public Sub LoadCustomer()
        Try
            ddlCustName.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataTextField = "Cust_Name"
            ddlCustName.DataValueField = "Cust_Id"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub BindAuditNo(ByVal iCustID As Integer)
        Try
            ddlAuditNo.DataSource = objclsStandardAudit.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, iFYID, iCustID, sSession.UserID, bLoginUserIsPartner)
            ddlAuditNo.DataTextField = "SA_AuditNo"
            ddlAuditNo.DataValueField = "SA_ID"
            ddlAuditNo.DataBind()
            ddlAuditNo.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditNo" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindHeading(ByVal iAuditTypeID As Integer)
        Try
            ddlHeading.DataSource = objclsAllActiveMaster.LoadAllAuditTypeHeadings(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID)
            ddlHeading.DataTextField = "ACM_Heading"
            ddlHeading.DataValueField = "ACM_ID"
            ddlHeading.DataBind()
            ddlHeading.Items.Insert(0, "Select Heading")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindHeading" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPartners()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            lstPartner.DataSource = dt
            lstPartner.DataTextField = "USr_FullName"
            lstPartner.DataValueField = "Usr_ID"
            lstPartner.DataBind()
            lstPartner.Items.Insert(0, "Select Partner")

            lstReviewPartner.DataSource = dt
            lstReviewPartner.DataTextField = "USr_FullName"
            lstReviewPartner.DataValueField = "Usr_ID"
            lstReviewPartner.DataBind()
            lstReviewPartner.Items.Insert(0, "Select Review Partner")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartners" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindEmployees()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)
            lstAdditionalSupportEmployee.DataSource = dt
            lstAdditionalSupportEmployee.DataTextField = "FullName"
            lstAdditionalSupportEmployee.DataValueField = "Usr_ID"
            lstAdditionalSupportEmployee.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindEmployees" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindSATeamMembers(ByVal sUserId As String)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadSASelectedEmployees(sSession.AccessCode, sSession.AccessCodeID, sUserId)
            ddlTeamMember.DataSource = dt
            ddlTeamMember.DataTextField = "FullName"
            ddlTeamMember.DataValueField = "Usr_ID"
            ddlTeamMember.DataBind()
            ddlTeamMember.Items.Insert(0, "Select Team Member")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSATeamMembers" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAuditTypes(ByVal iFYId As Integer, ByVal iCustId As Integer, ByVal iAuditTypeId As Integer)
        Try
            ddlAuditType.DataSource = objclsStandardAudit.LoadAuditTypeIsComplainceDetailsInSA(sSession.AccessCode, sSession.AccessCodeID, "AT", iFYId, iCustId, iAuditTypeId)
            ddlAuditType.DataTextField = "Name"
            ddlAuditType.DataValueField = "PKID"
            ddlAuditType.DataBind()
            ddlAuditType.Items.Insert(0, "Select Audit Type")
            If iAuditTypeId > 0 Then
                ddlAuditType.SelectedValue = iAuditTypeId
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditTypes" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            divCheckPoint.Visible = False : ddlAuditType.Enabled = True
            btnSaveSchedule.Visible = True : btnUpdateSchedule.Visible = False
            iCustID = 0 : iCheckPointsAndTeamMemberPKID = 0 : sSelectedCheckPointsPKID = ""
            BindPartners() : BindEmployees() : BindSATeamMembers("")
            txtTimeLineStartDate.Text = "" : txtExpectedCompletionDate.Text = ""
            txtDateForMRS.Text = "" : txtReportFilingDate.Text = ""
            txtReportReviewDate.Text = "" : txtScopeOfAudit.Text = ""
            BindHeading(0)
            ClearTeamMemberDetails()
            iCheckPointsAndTeamMemberPKID = 0 : sSelectedCheckPointsPKID = ""
            gvSAFinalCheckList.DataSource = Nothing
            gvSAFinalCheckList.DataBind()
            If ddlCustName.SelectedIndex > 0 Then
                iCustID = ddlCustName.SelectedValue
            End If
            BindAuditNo(iCustID)
            BindAuditTypes(iFYID, iCustID, 0)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAuditNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNo.SelectedIndexChanged
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            divCheckPoint.Visible = False : ddlAuditType.Enabled = True
            btnSaveSchedule.Visible = True : btnUpdateSchedule.Visible = False
            iCustID = 0 : iCheckPointsAndTeamMemberPKID = 0 : sSelectedCheckPointsPKID = ""
            BindPartners() : BindEmployees() : BindSATeamMembers("")
            txtTimeLineStartDate.Text = "" : txtExpectedCompletionDate.Text = ""
            txtDateForMRS.Text = "" : txtReportFilingDate.Text = ""
            txtReportReviewDate.Text = "" : txtScopeOfAudit.Text = ""
            BindHeading(0)
            ClearTeamMemberDetails()
            iCheckPointsAndTeamMemberPKID = 0 : sSelectedCheckPointsPKID = ""
            gvSAFinalCheckList.DataSource = Nothing
            gvSAFinalCheckList.DataBind()
            If ddlAuditNo.SelectedIndex > 0 Then
                Dim iAuditId As Integer = ddlAuditNo.SelectedValue
                BindSelectedScheduleDetails(ddlAuditNo.SelectedValue)
                BindAuditNo(ddlCustName.SelectedValue)
                ddlAuditNo.SelectedValue = iAuditId
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNo_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAuditType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditType.SelectedIndexChanged
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            iAuditStatusID = 0 : iCheckPointsAndTeamMemberPKID = 0
            gvSAFinalCheckList.DataSource = Nothing
            gvSAFinalCheckList.DataBind()
            If ddlAuditType.SelectedIndex > 0 Then
                BindHeading(ddlAuditType.SelectedValue)
                BindHeadingChecklistGrid(0, "", "")
            Else
                BindHeading(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditType_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindHeadingChecklistGrid(ByVal iAuditTypeID As Integer, ByVal sHeading As String, ByVal sCheckPointIds As String)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadAuditTypeCheckList(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iAuditTypeID, sHeading, sCheckPointIds)
            gvHeadingCheckList.DataSource = dt
            gvHeadingCheckList.DataBind()
            If dt.Rows.Count > 5 Then
                divHCL.Style.Item("Height") = "250px"
            Else
                divHCL.Style.Item("Height") = "auto"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditTypeChecklistGrid" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnNewScheduleAudit_Click(sender As Object, e As EventArgs) Handles btnNewScheduleAudit.Click
        Try
            ddlCustName_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewScheduleAudit_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnNewCheckPoint_Click(sender As Object, e As EventArgs) Handles btnNewCheckPoint.Click
        Dim oStatusID As New Object, oAuditTypeID As New Object
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            If Not ValidateDropDownListSelectedIndex(ddlAuditType, "Select Audit Type.") Then ddlAuditType.Focus() : Exit Try
            oStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(0))
            oAuditTypeID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlAuditType.SelectedValue))
            Response.Redirect(String.Format("~/Masters/AuditChecklistDetails.aspx?StatusID={0}&AuditTypeID={1}", oStatusID, oAuditTypeID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewCheckPoint_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSaveSchedule_Click(sender As Object, e As EventArgs) Handles btnSaveSchedule.Click
        Dim ArrAS() As String
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            If Not ValidateDropDownListSelectedIndex(ddlCustName, "Select Customer Name.") Then ddlCustName.Focus() : Exit Try
            If Not ValidateDropDownListSelectedIndex(ddlAuditType, "Select Audit Type.") Then ddlAuditType.Focus() : Exit Try
            If Not ValidateListBoxSelection(lstPartner, "Select Partner.") Then lstPartner.Focus() : Exit Try
            If Not ValidateListBoxSelection(lstReviewPartner, "Select Review Partner.") Then lstReviewPartner.Focus() : Exit Try
            If Not ValidateListBoxSelection(lstAdditionalSupportEmployee, "Select Additional Support Employee.") Then lstAdditionalSupportEmployee.Focus() : Exit Try
            If Not ValidateTextBox(txtTimeLineStartDate, "Enter Start Date.") Then txtTimeLineStartDate.Focus() : Exit Try
            If Not ValidateTextBox(txtExpectedCompletionDate, "Enter Expected Completion Date.") Then txtExpectedCompletionDate.Focus() : Exit Try
            If Not ValidateTextBox(txtDateForMRS, "Enter Date For MRS.") Then txtDateForMRS.Focus() : Exit Try
            If Not ValidateTextBox(txtReportFilingDate, "Enter Report Filing Date.") Then txtReportFilingDate.Focus() : Exit Try
            If Not ValidateTextBox(txtReportReviewDate, "Enter Report Review Date.") Then txtReportReviewDate.Focus() : Exit Try
            If Not ValidateTextBoxLength(txtScopeOfAudit, "Enter Scope Of Audit.", 5000) Then txtScopeOfAudit.Focus() : Exit Try

            Dim dTimeLineStartDate As DateTime = Date.ParseExact(txtTimeLineStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCompletionDate As DateTime = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dReportFilingDate As DateTime = Date.ParseExact(txtReportFilingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dReportReviewDate As DateTime = Date.ParseExact(txtReportReviewDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dDateForMRS As DateTime = Date.ParseExact(txtDateForMRS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If Not ValidateDateRange(dTimeLineStartDate, dCompletionDate, "Expected Completion Date should be greater than or equal to Audit TimeLine Start Date.") Then txtExpectedCompletionDate.Focus() : Exit Try
            If Not ValidateDateRange(dTimeLineStartDate, dReportReviewDate, "Report Review Date should be greater than or equal to Audit TimeLine Start Date.") Then txtReportReviewDate.Focus() : Exit Try
            If Not ValidateDateRange(dTimeLineStartDate, dReportFilingDate, "Report Filing Date should be greater than or equal to Audit TimeLine Start Date.") Then txtReportFilingDate.Focus() : Exit Try
            If Not ValidateDateRange(dTimeLineStartDate, dDateForMRS, "Date For MRS should be greater than or equal to Audit TimeLine Start Date.") Then txtDateForMRS.Focus() : Exit Try

            iCustID = ddlCustName.SelectedValue
            Dim objSA As New strStandardAudit_Schedule
            objSA.iSA_ID = 0
            objSA.sSA_AuditNo = ""
            objSA.iSA_CustID = iCustID
            objSA.iSA_YearID = iFYID
            objSA.iSA_AuditTypeID = ddlAuditType.SelectedValue
            objSA.sSA_PartnerID = String.Join(",", lstPartner.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
            objSA.sSA_ReviewPartnerID = String.Join(",", lstReviewPartner.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
            objSA.sSA_AdditionalSupportEmployeeID = String.Join(",", lstAdditionalSupportEmployee.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
            objSA.sSA_ScopeOfAudit = objclsGRACeGeneral.SafeSQL(txtScopeOfAudit.Text.ToString())
            objSA.iSA_Status = 1
            objSA.iSA_AttachID = 0
            objSA.dSA_StartDate = Date.ParseExact(txtTimeLineStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_ExpCompDate = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_RptRvDate = Date.ParseExact(txtReportReviewDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_RptFilDate = Date.ParseExact(txtReportFilingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_MRSDate = Date.ParseExact(txtDateForMRS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.iSA_CrBy = sSession.UserID
            objSA.sSA_IPAddress = sSession.IPAddress
            objSA.iSA_CompID = sSession.AccessCodeID
            ArrAS = objclsStandardAudit.SaveUpdateStandardAuditScheduleDetails(sSession.AccessCode, objSA, lblFY.Text)
            BindAuditNo(iCustID)
            ddlAuditNo.SelectedValue = ArrAS(1)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Schedule Audit", "Saved", iFYID, iCustID, ddlAuditType.SelectedValue, ArrAS(1), sSession.IPAddress)
            BindSelectedScheduleDetails(ddlAuditNo.SelectedValue)
            lblError.Text = "Successfully Saved." : lblScheduleModalMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSchedule_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnUpdateSchedule_Click(sender As Object, e As EventArgs) Handles btnUpdateSchedule.Click
        Dim ArrAS() As String
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            If iAuditStatusID > 1 Then
                lblError.Text = "Already Conduct Audit started, Modification is not allowed for selected Audit."
                Exit Try
            End If
            If Not ValidateDropDownListSelectedIndex(ddlCustName, "Select Customer Name.") Then ddlCustName.Focus() : Exit Try
            If Not ValidateDropDownListSelectedIndex(ddlAuditType, "Select Audit Type.") Then ddlAuditType.Focus() : Exit Try
            If Not ValidateListBoxSelection(lstPartner, "Select Partner.") Then lstPartner.Focus() : Exit Try
            If Not ValidateListBoxSelection(lstReviewPartner, "Select Review Partner.") Then lstReviewPartner.Focus() : Exit Try
            If Not ValidateListBoxSelection(lstAdditionalSupportEmployee, "Select Additional Support Employee.") Then lstAdditionalSupportEmployee.Focus() : Exit Try
            If Not ValidateTextBox(txtTimeLineStartDate, "Enter Start Date.") Then txtTimeLineStartDate.Focus() : Exit Try
            If Not ValidateTextBox(txtExpectedCompletionDate, "Enter Expected Completion Date.") Then txtExpectedCompletionDate.Focus() : Exit Try
            If Not ValidateTextBox(txtDateForMRS, "Enter Date For MRS.") Then txtDateForMRS.Focus() : Exit Try
            If Not ValidateTextBox(txtReportFilingDate, "Enter Report Filing Date.") Then txtReportFilingDate.Focus() : Exit Try
            If Not ValidateTextBox(txtReportReviewDate, "Enter Report Review Date.") Then txtReportReviewDate.Focus() : Exit Try
            If Not ValidateTextBoxLength(txtScopeOfAudit, "Enter Scope Of Audit.", 5000) Then txtScopeOfAudit.Focus() : Exit Try

            Dim dTimeLineStartDate As DateTime = Date.ParseExact(txtTimeLineStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCompletionDate As DateTime = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dReportFilingDate As DateTime = Date.ParseExact(txtReportFilingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dReportReviewDate As DateTime = Date.ParseExact(txtReportReviewDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dDateForMRS As DateTime = Date.ParseExact(txtDateForMRS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If Not ValidateDateRange(dTimeLineStartDate, dCompletionDate, "Expected Completion Date should be greater than or equal to Audit TimeLine Start Date.") Then txtExpectedCompletionDate.Focus() : Exit Try
            If Not ValidateDateRange(dTimeLineStartDate, dReportReviewDate, "Report Review Date should be greater than or equal to Audit TimeLine Start Date.") Then txtReportReviewDate.Focus() : Exit Try
            If Not ValidateDateRange(dTimeLineStartDate, dReportFilingDate, "Report Filing Date should be greater than or equal to Audit TimeLine Start Date.") Then txtReportFilingDate.Focus() : Exit Try
            If Not ValidateDateRange(dTimeLineStartDate, dDateForMRS, "Date For MRS should be greater than or equal to Audit TimeLine Start Date.") Then txtDateForMRS.Focus() : Exit Try

            iCustID = ddlCustName.SelectedValue
            Dim objSA As New strStandardAudit_Schedule
            objSA.iSA_ID = ddlAuditNo.SelectedValue
            objSA.sSA_AuditNo = ddlAuditNo.SelectedItem.Text
            objSA.iSA_CustID = iCustID
            objSA.iSA_YearID = iFYID
            objSA.iSA_AuditTypeID = ddlAuditType.SelectedValue
            objSA.sSA_PartnerID = String.Join(",", lstPartner.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
            objSA.sSA_ReviewPartnerID = String.Join(",", lstReviewPartner.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
            objSA.sSA_AdditionalSupportEmployeeID = String.Join(",", lstAdditionalSupportEmployee.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
            objSA.sSA_ScopeOfAudit = objclsGRACeGeneral.SafeSQL(txtScopeOfAudit.Text.ToString())
            objSA.iSA_Status = 1
            objSA.iSA_AttachID = 0
            objSA.dSA_StartDate = Date.ParseExact(txtTimeLineStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_ExpCompDate = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_RptRvDate = Date.ParseExact(txtReportReviewDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_RptFilDate = Date.ParseExact(txtReportFilingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.dSA_MRSDate = Date.ParseExact(txtDateForMRS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objSA.iSA_CrBy = sSession.UserID
            objSA.iSA_UpdatedBy = sSession.UserID
            objSA.sSA_IPAddress = sSession.IPAddress
            objSA.iSA_CompID = sSession.AccessCodeID
            ArrAS = objclsStandardAudit.SaveUpdateStandardAuditScheduleDetails(sSession.AccessCode, objSA, lblFY.Text)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Schedule Audit", "Updated", iFYID, iCustID, ddlAuditType.SelectedValue, ArrAS(1), sSession.IPAddress)
            BindSelectedScheduleDetails(ddlAuditNo.SelectedValue)
            lblError.Text = "Successfully Updated." : lblScheduleModalMsg.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateSchedule_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindSelectedScheduleDetails(ByVal iAuditID As Integer)
        Dim ds As New DataSet
        Dim dt As New DataTable, dt1 As New DataTable, dtHeading As New DataTable
        Dim chkSelectCheckList As New CheckBox, chkSelectMandatory As New CheckBox
        Dim lblCheckPointID As New Label
        Dim lblCheckDetailsId As New CheckBox
        Try
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            For i = 0 To dt.Rows.Count - 1
                divCheckPoint.Visible = True : ddlAuditType.Enabled = False
                btnSaveSchedule.Visible = False : btnUpdateSchedule.Visible = True
                iCustID = dt.Rows(0)("SA_CustID")
                ddlCustName.SelectedValue = iCustID
                iAuditStatusID = dt.Rows(0)("SA_Status")
                BindAuditTypes(iFYID, iCustID, dt.Rows(i)("SA_AuditTypeID"))
                BindHeading(ddlAuditType.SelectedValue)
                txtTimeLineStartDate.Text = If(IsDBNull(dt.Rows(i)("SA_StartDate")), "", dt.Rows(i)("SA_StartDate").ToString())
                txtExpectedCompletionDate.Text = If(IsDBNull(dt.Rows(i)("SA_ExpCompDate")), "", dt.Rows(i)("SA_ExpCompDate").ToString())
                txtReportReviewDate.Text = If(IsDBNull(dt.Rows(i)("SA_RptRvDate")), "", dt.Rows(i)("SA_RptRvDate").ToString())
                txtReportFilingDate.Text = If(IsDBNull(dt.Rows(i)("SA_RptFilDate")), "", dt.Rows(i)("SA_RptFilDate").ToString())
                txtDateForMRS.Text = If(IsDBNull(dt.Rows(i)("SA_MRSDate")), "", dt.Rows(i)("SA_MRSDate").ToString())
                txtScopeOfAudit.Text = If(IsDBNull(dt.Rows(i)("SA_ScopeOfAudit")), "", objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SA_ScopeOfAudit").ToString()))

                Dim sPartners As String = ""
                If Not IsDBNull(dt.Rows(i)("SA_PartnerID")) Then
                    sPartners = dt.Rows(i)("SA_PartnerID").ToString()
                    If Not String.IsNullOrEmpty(sPartners) Then
                        sPartners = If(sPartners.StartsWith(","), sPartners, "," & sPartners)
                        sPartners = If(sPartners.EndsWith(","), sPartners, sPartners & ",")
                        For Each partnerItem As ListItem In lstPartner.Items
                            If sPartners.Contains("," & partnerItem.Value & ",") Then
                                partnerItem.Selected = True
                            End If
                        Next
                    End If
                End If
                Dim sReviewPartners As String = ""
                If Not IsDBNull(dt.Rows(i)("SA_ReviewPartnerID")) Then
                    sReviewPartners = dt.Rows(i)("SA_ReviewPartnerID").ToString()
                    If Not String.IsNullOrEmpty(sReviewPartners) Then
                        sReviewPartners = If(sReviewPartners.StartsWith(","), sReviewPartners, "," & sReviewPartners)
                        sReviewPartners = If(sReviewPartners.EndsWith(","), sReviewPartners, sReviewPartners & ",")
                        For Each reviewPartnerItem As ListItem In lstReviewPartner.Items
                            If sReviewPartners.Contains("," & reviewPartnerItem.Value & ",") Then
                                reviewPartnerItem.Selected = True
                            End If
                        Next
                    End If
                End If
                Dim sAdditionalSupportEmployees As String = ""
                If Not IsDBNull(dt.Rows(i)("SA_AdditionalSupportEmployeeID")) Then
                    sAdditionalSupportEmployees = dt.Rows(i)("SA_AdditionalSupportEmployeeID").ToString()
                    If Not String.IsNullOrEmpty(sAdditionalSupportEmployees) Then
                        sAdditionalSupportEmployees = If(sAdditionalSupportEmployees.StartsWith(","), sAdditionalSupportEmployees, "," & sAdditionalSupportEmployees)
                        sAdditionalSupportEmployees = If(sAdditionalSupportEmployees.EndsWith(","), sAdditionalSupportEmployees, sAdditionalSupportEmployees & ",")
                        For Each supportEmployeeItem As ListItem In lstAdditionalSupportEmployee.Items
                            If sAdditionalSupportEmployees.Contains("," & supportEmployeeItem.Value & ",") Then
                                supportEmployeeItem.Selected = True
                            End If
                        Next
                    End If
                End If

                Dim RemoveLeadingTrailingComma As Func(Of String, String) = Function(s) If(String.IsNullOrEmpty(s), s, s.Trim(","c))
                sPartners = RemoveLeadingTrailingComma(sPartners) : sReviewPartners = RemoveLeadingTrailingComma(sReviewPartners) : sAdditionalSupportEmployees = RemoveLeadingTrailingComma(sAdditionalSupportEmployees)
                BindSATeamMembers(String.Join(",", {sPartners, sReviewPartners, sAdditionalSupportEmployees}.Where(Function(s) Not String.IsNullOrEmpty(s))))

                ClearTeamMemberDetails()
                BindSAAsignedCheckPointsAndTeamMembersGV(iAuditID)
                BindSAFinalCheckPointsGV(iAuditID)
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditTypeChecklistGrid" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindSAAsignedCheckPointsAndTeamMembersGV(ByVal iAuditID As Integer)
        Dim dt As DataTable
        Dim sHeading As String = ""
        Try
            sHeading = If(ddlHeading.SelectedIndex > 0, ddlHeading.SelectedItem.Text, "")
            dt = objclsStandardAudit.LoadSAAsignedCheckPointsAndTeamMembers(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCustID, sHeading)
            GvAssignDetails.DataSource = dt
            GvAssignDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSAAsignedCheckPointsAndTeamMembers" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindSAFinalCheckPointsGV(ByVal iAuditID As Integer)
        Dim dtTab As New DataTable
        Try
            dtTab = objclsStandardAudit.GetFinalAuditTypeHeadingCheckPoints(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            gvSAFinalCheckList.DataSource = dtTab
            gvSAFinalCheckList.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSAAsignedCheckPointsAndTeamMembers" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnResourceAvailability_Click(sender As Object, e As EventArgs) Handles btnResourceAvailability.Click
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.GetResourceAvailability(sSession.AccessCode, sSession.AccessCodeID)
            gvResourceAvailability.DataSource = dt
            gvResourceAvailability.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myResourceAvailabilityModal').modal('show')", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnResourceAvailability_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ClearTeamMemberDetails()
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = "" : btnAdd.Text = "Add Check Points"
            gvHeadingCheckList.DataSource = Nothing
            gvHeadingCheckList.DataBind()
            ddlTeamMember.SelectedIndex = 0 : ddlWorkType.SelectedIndex = 0
            txtHoursPerDay.Text = "" : txtStartDate.Text = "" : txtEndDate.Text = "" : txtTotalHrs.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearTeamMemberDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlHeading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHeading.SelectedIndexChanged
        Try
            ClearTeamMemberDetails()
            iCheckPointsAndTeamMemberPKID = 0 : sSelectedCheckPointsPKID = ""
            If ddlAuditType.SelectedIndex > 0 Then
                BindHeadingChecklistGrid(ddlAuditType.SelectedValue, ddlHeading.SelectedItem.Text.ToString(), "")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHeading_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAllCheckList_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            Dim chkAll As CheckBox = CType(sender, CheckBox)
            For Each row As GridViewRow In gvHeadingCheckList.Rows
                Dim chkSelectCheckList As CheckBox = TryCast(row.FindControl("chkSelectCheckList"), CheckBox)
                If chkSelectCheckList IsNot Nothing Then
                    chkSelectCheckList.Checked = chkAll.Checked
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllCheckList_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim ArrAS() As String
        Dim chkSelectCheckList As New CheckBox, chkSelectMandatory As New CheckBox
        Dim lblCheckPointID As New Label
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            'If Val(lblAuditId.Text) = 0 Then lblError.Text = "Save Schedule Audit & Audit Timeline Details first." : lblScheduleModalMsg.Text = "Save Schedule Audit & Audit Timeline Details first." : Exit Try
            If Not ValidateGridViewNotEmpty(gvHeadingCheckList, "No Check Point for selected Audit Type/Heading.") Then Exit Try
            If Not ValidateGridViewCheckBox(gvHeadingCheckList, "chkSelectCheckList", "Select Check Point.") Then Exit Try
            If Not ValidateDropDownListSelectedIndex(ddlHeading, "Select Heading.") Then Exit Try

            Dim dTimeLineStartDate As DateTime = Date.ParseExact(txtTimeLineStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dCompletionDate As DateTime = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dStartDate As DateTime = If(txtStartDate.Text <> "", Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), Nothing)
            Dim dEndDate As DateTime = If(txtEndDate.Text <> "", Date.ParseExact(txtEndDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture), Nothing)

            If txtStartDate.Text <> "" AndAlso Not ValidateDateRange(dTimeLineStartDate, dStartDate, "Start Date should be greater than or equal to Audit TimeLine Start Date.") Then txtStartDate.Focus() : Exit Try
            If txtStartDate.Text <> "" AndAlso Not ValidateDateRange(dStartDate, dCompletionDate, "Start Date should be less than or equal to Expected Completion Date.") Then txtStartDate.Focus() : Exit Try
            If txtEndDate.Text <> "" AndAlso Not ValidateDateRange(dStartDate, dEndDate, "End Date should be greater than or equal to Start Date.") Then txtEndDate.Focus() : Exit Try
            If txtEndDate.Text <> "" AndAlso Not ValidateDateRange(dEndDate, dCompletionDate, "End Date should be less than or equal to Expected Completion Date.") Then txtEndDate.Focus() : Exit Try

            If txtHoursPerDay.Text <> "" AndAlso Not IsNumeric(txtHoursPerDay.Text) Then lblError.Text = "Enter valid Hours Per Day." : lblScheduleModalMsg.Text = "Enter valid Hours Per Day." : txtHoursPerDay.Focus() : ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True) : Exit Try
            If txtHoursPerDay.Text <> "" AndAlso Val(txtHoursPerDay.Text) > 24 Then lblError.Text = "Hours Per Day should be less than 24." : lblScheduleModalMsg.Text = "Hours Per Day should be less than 24." : txtHoursPerDay.Focus() : ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True) : Exit Try
            If txtTotalHrs.Text <> "" AndAlso Not IsNumeric(txtTotalHrs.Text) Then lblError.Text = "Enter valid Total No.of hours." : lblScheduleModalMsg.Text = "Enter valid Total No.of hours." : txtTotalHrs.Focus() : ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True) : Exit Try
            If txtHoursPerDay.Text <> "" AndAlso Val(txtHoursPerDay.Text) > Val(txtTotalHrs.Text) Then lblError.Text = "Total No.of hours should be greater than or equal to Hours Per Day." : lblScheduleModalMsg.Text = "Total No.of hours should be greater than or equal to Hours Per Day." : txtHoursPerDay.Focus() : ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalScheduleValidation').modal('show');", True) : Exit Try

            If ddlAuditNo.SelectedIndex = 0 Then
                If Not ValidateDropDownListSelectedIndex(ddlCustName, "Select Customer Name.") Then ddlCustName.Focus() : Exit Try
                If Not ValidateDropDownListSelectedIndex(ddlAuditType, "Select Audit Type.") Then ddlAuditType.Focus() : Exit Try
                If Not ValidateListBoxSelection(lstPartner, "Select Partner.") Then lstPartner.Focus() : Exit Try
                If Not ValidateListBoxSelection(lstReviewPartner, "Select Review Partner.") Then lstReviewPartner.Focus() : Exit Try
                If Not ValidateListBoxSelection(lstAdditionalSupportEmployee, "Select Additional Support Employee.") Then lstAdditionalSupportEmployee.Focus() : Exit Try
                If Not ValidateTextBox(txtTimeLineStartDate, "Enter Start Date.") Then txtTimeLineStartDate.Focus() : Exit Try
                If Not ValidateTextBox(txtExpectedCompletionDate, "Enter Expected Completion Date.") Then txtExpectedCompletionDate.Focus() : Exit Try
                If Not ValidateTextBox(txtDateForMRS, "Enter Date For MRS.") Then txtDateForMRS.Focus() : Exit Try
                If Not ValidateTextBox(txtReportFilingDate, "Enter Report Filing Date.") Then txtReportFilingDate.Focus() : Exit Try
                If Not ValidateTextBox(txtReportReviewDate, "Enter Report Review Date.") Then txtReportReviewDate.Focus() : Exit Try
                If Not ValidateTextBoxLength(txtScopeOfAudit, "Enter Scope Of Audit.", 5000) Then txtScopeOfAudit.Focus() : Exit Try


                Dim dReportFilingDate As DateTime = Date.ParseExact(txtReportFilingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dReportReviewDate As DateTime = Date.ParseExact(txtReportReviewDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dDateForMRS As DateTime = Date.ParseExact(txtDateForMRS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                If Not ValidateDateRange(dTimeLineStartDate, dCompletionDate, "Expected Completion Date should be greater than or equal to Start Date.") Then txtExpectedCompletionDate.Focus() : Exit Try
                If Not ValidateDateRange(dTimeLineStartDate, dReportReviewDate, "Report Review Date should be greater than or equal to Start Date.") Then txtReportReviewDate.Focus() : Exit Try
                If Not ValidateDateRange(dTimeLineStartDate, dReportFilingDate, "Report Filing Date should be greater than or equal to Start Date.") Then txtReportFilingDate.Focus() : Exit Try
                If Not ValidateDateRange(dTimeLineStartDate, dDateForMRS, "Date For MRS should be greater than or equal to Start Date.") Then txtDateForMRS.Focus() : Exit Try

                iCustID = ddlCustName.SelectedValue
                Dim objSA As New strStandardAudit_Schedule
                objSA.iSA_ID = 0
                objSA.sSA_AuditNo = ""
                objSA.iSA_CustID = iCustID
                objSA.iSA_YearID = iFYID
                objSA.iSA_AuditTypeID = ddlAuditType.SelectedValue
                objSA.sSA_PartnerID = String.Join(",", lstPartner.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
                objSA.sSA_ReviewPartnerID = String.Join(",", lstReviewPartner.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
                objSA.sSA_AdditionalSupportEmployeeID = String.Join(",", lstAdditionalSupportEmployee.Items.Cast(Of ListItem)().Where(Function(item) item.Selected).Select(Function(item) item.Value))
                objSA.sSA_ScopeOfAudit = objclsGRACeGeneral.SafeSQL(txtScopeOfAudit.Text.ToString())
                objSA.iSA_Status = 1
                objSA.iSA_AttachID = 0
                objSA.dSA_StartDate = Date.ParseExact(txtTimeLineStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSA.dSA_ExpCompDate = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSA.dSA_RptRvDate = Date.ParseExact(txtReportReviewDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSA.dSA_RptFilDate = Date.ParseExact(txtReportFilingDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSA.dSA_MRSDate = Date.ParseExact(txtDateForMRS.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSA.iSA_CrBy = sSession.UserID
                objSA.sSA_IPAddress = sSession.IPAddress
                objSA.iSA_CompID = sSession.AccessCodeID
                ArrAS = objclsStandardAudit.SaveUpdateStandardAuditScheduleDetails(sSession.AccessCode, objSA, lblFY.Text)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Schedule Audit", "Saved", iFYID, iCustID, ddlAuditType.SelectedValue, ArrAS(1), sSession.IPAddress)
                BindAuditNo(iCustID)
                ddlAuditNo.SelectedValue = ArrAS(1)
            End If

            Dim objSACLD As New strStandardAudit_Checklist_Details
            objSACLD.iSACD_ID = iCheckPointsAndTeamMemberPKID
            objSACLD.iSACD_CustId = ddlCustName.SelectedValue
            objSACLD.iSACD_AuditId = ddlAuditNo.SelectedValue
            objSACLD.iSACD_AuditType = ddlAuditType.SelectedValue
            objSACLD.sSACD_Heading = If(ddlHeading.SelectedIndex >= 0, ddlHeading.SelectedItem?.Text, "")
            objSACLD.sSACD_CheckpointId = String.Join(",", gvHeadingCheckList.Rows.Cast(Of GridViewRow)().Where(Function(row) DirectCast(row.FindControl("chkSelectCheckList"), CheckBox).Checked).Select(Function(row) DirectCast(row.FindControl("lblCheckPointID"), Label).Text))
            objSACLD.iSACD_EmpId = If(ddlTeamMember.SelectedIndex > 0, ddlTeamMember.SelectedValue, 0)
            objSACLD.iSACD_WorkType = If(ddlWorkType.SelectedIndex > 0, ddlWorkType.SelectedValue, 0)
            objSACLD.dSACD_StartDate = If(String.IsNullOrEmpty(txtStartDate.Text), "01/01/1900", txtStartDate.Text)
            objSACLD.dSACD_EndDate = If(String.IsNullOrEmpty(txtEndDate.Text), "01/01/1900", txtEndDate.Text)
            objSACLD.sSACD_HrPrDay = If(String.IsNullOrEmpty(txtHoursPerDay.Text), 0, txtHoursPerDay.Text)
            objSACLD.sSACD_TotalHr = If(String.IsNullOrEmpty(txtTotalHrs.Text), 0, txtTotalHrs.Text)
            objSACLD.iSACD_CRBY = sSession.UserID
            objSACLD.sSACD_IPAddress = sSession.IPAddress
            objSACLD.iSACD_CompId = sSession.AccessCodeID
            objclsStandardAudit.SaveUpdateStandardAuditChecklistDetails(sSession.AccessCode, objSACLD)

            Dim sMsg As String = "Added."
            If iCheckPointsAndTeamMemberPKID > 0 And sSelectedCheckPointsPKID <> "" Then
                sMsg = "Updated."
                objclsStandardAudit.DeleteFinalCheckPointsDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, sSelectedCheckPointsPKID)
            End If
            For i = 0 To gvHeadingCheckList.Rows.Count - 1
                chkSelectCheckList = gvHeadingCheckList.Rows(i).FindControl("chkSelectCheckList")
                chkSelectMandatory = gvHeadingCheckList.Rows(i).FindControl("chkSelectMandatory")
                lblCheckPointID = gvHeadingCheckList.Rows(i).FindControl("lblCheckPointID")
                If chkSelectCheckList.Checked = True Then
                    Dim objSAC As New strStandardAudit_ScheduleCheckPointList
                    objSAC.iSAC_ID = 0
                    objSAC.iSAC_SA_ID = ddlAuditNo.SelectedValue
                    objSAC.iSAC_CheckPointID = Val(lblCheckPointID.Text)
                    objSAC.iSAC_Mandatory = If(chkSelectMandatory.Checked, 1, 0)
                    objSAC.iSAC_Status = 0
                    objSAC.iSAC_AttachID = 0
                    objSAC.iSAC_CrBy = sSession.UserID
                    objSAC.sSAC_IPAddress = sSession.IPAddress
                    objSAC.iSAC_CompID = sSession.AccessCodeID
                    objclsStandardAudit.SaveStandardAuditScheduleCheckPointListDetails(sSession.AccessCode, objSAC)
                End If
            Next
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Standard Audit", "Dashboard And Schedule", "Schedule Updated", iFYID, iCustID, ddlAuditType.SelectedValue, ddlAuditNo.SelectedValue, sSession.IPAddress)
            BindSelectedScheduleDetails(ddlAuditNo.SelectedValue)
            lblError.Text = "Successfully " & sMsg : lblScheduleModalMsg.Text = "Successfully " & sMsg
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub GvAssignDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GvAssignDetails.RowCommand
        Dim lblCheckPointIds As New Label, lblDBPkID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblScheduleModalMsg.Text = ""
            sSelectedCheckPointsPKID = "" : iCheckPointsAndTeamMemberPKID = 0
            btnAdd.Text = "Add Check Points"
            gvChkpoints.DataSource = Nothing
            gvChkpoints.DataBind()
            If e.CommandName = "VIEW" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCheckPointIds = CType(clickedRow.FindControl("lblCheckPointIds"), Label)
                dt = objclsStandardAudit.GetSelectedCheckPoints(sSession.AccessCode, sSession.AccessCodeID, lblCheckPointIds.Text)
                gvChkpoints.DataSource = dt
                gvChkpoints.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSelectedCheckPoints').modal('show');", True)
            End If
            If e.CommandName = "REMOVE" Then
                If iAuditStatusID > 1 Then
                    lblError.Text = "Already Conduct Audit started, Modification is not allowed for selected Audit."
                    Exit Try
                End If
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBPkID = CType(clickedRow.FindControl("lblDBPkID"), Label)
                lblCheckPointIds = CType(clickedRow.FindControl("lblCheckPointIds"), Label)
                objclsStandardAudit.DeleteSelectedCheckPointsAndTeamMembers(sSession.AccessCode, sSession.AccessCodeID, ddlAuditNo.SelectedValue, iCustID, Val(lblDBPkID.Text), lblCheckPointIds.Text)
                BindSAAsignedCheckPointsAndTeamMembersGV(ddlAuditNo.SelectedValue)
                BindSAFinalCheckPointsGV(ddlAuditNo.SelectedValue)
            End If
            If e.CommandName = "UPDATEAD" Then
                If iAuditStatusID > 1 Then
                    lblError.Text = "Already Conduct Audit started, Modification is not allowed for selected Audit."
                    Exit Try
                End If
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblDBPkID = CType(clickedRow.FindControl("lblDBPkID"), Label)
                iCheckPointsAndTeamMemberPKID = Val(lblDBPkID.Text)
                BindSAAsignedCheckPointsAndTeamMembersDetails(ddlAuditNo.SelectedValue, iCustID, Val(lblDBPkID.Text))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAssignDetails_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindSAAsignedCheckPointsAndTeamMembersDetails(ByVal iAuditId As Integer, ByVal iCustID As Integer, ByVal iDBPkID As Integer)
        Dim dt As DataTable
        Try
            ClearTeamMemberDetails()
            dt = objclsStandardAudit.LoadSelectedSAAsignedCheckPointsAndTeamMembers(sSession.AccessCode, sSession.AccessCodeID, iAuditId, iCustID, iDBPkID)
            If dt.Rows.Count = 1 Then
                btnAdd.Text = "Update Check Points"
                ddlHeading.SelectedIndex = ddlHeading.Items.IndexOf(ddlHeading.Items.FindByText(Trim(dt.Rows(0).Item("SACD_Heading"))))
                If Not IsDBNull(dt.Rows(0)("SACD_EmpId")) AndAlso dt.Rows(0)("SACD_EmpId") > 0 Then ddlTeamMember.SelectedValue = dt.Rows(0)("SACD_EmpId")
                If Not IsDBNull(dt.Rows(0)("SACD_WorkType")) AndAlso dt.Rows(0)("SACD_WorkType") > 0 Then ddlWorkType.SelectedValue = dt.Rows(0)("SACD_WorkType")
                If Not IsDBNull(dt.Rows(0)("SACD_TotalHr")) AndAlso dt.Rows(0)("SACD_TotalHr") > 0 Then txtTotalHrs.Text = dt.Rows(0)("SACD_TotalHr")
                If Not IsDBNull(dt.Rows(0)("SACD_HrPrDay")) AndAlso dt.Rows(0)("SACD_HrPrDay") > 0 Then txtHoursPerDay.Text = dt.Rows(0)("SACD_HrPrDay")
                If Not IsDBNull(dt.Rows(0)("SACD_StartDate")) AndAlso dt.Rows(0)("SACD_StartDate") <> "01/01/1900" Then txtStartDate.Text = dt.Rows(0)("SACD_StartDate")
                If Not IsDBNull(dt.Rows(0)("SACD_EndDate")) AndAlso dt.Rows(0)("SACD_EndDate") <> "01/01/1900" Then txtEndDate.Text = dt.Rows(0)("SACD_EndDate")
                If Not IsDBNull(dt.Rows(0)("SACD_CheckpointId")) Then sSelectedCheckPointsPKID = dt.Rows(0)("SACD_CheckpointId")
                BindHeadingChecklistGrid(ddlAuditType.SelectedValue, ddlHeading.SelectedItem.Text.ToString(), sSelectedCheckPointsPKID)

                Dim chkSelectCheckList As New CheckBox, chkSelectMandatory As New CheckBox
                Dim dt1 As New DataTable : Dim lblCheckPointID As New Label
                dt1 = objclsStandardAudit.GetSelectedScheduleCheckPointListDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditId, iCustID, sSelectedCheckPointsPKID)
                For i = 0 To gvHeadingCheckList.Rows.Count - 1
                    chkSelectCheckList = gvHeadingCheckList.Rows(i).FindControl("chkSelectCheckList")
                    chkSelectMandatory = gvHeadingCheckList.Rows(i).FindControl("chkSelectMandatory")
                    lblCheckPointID = gvHeadingCheckList.Rows(i).FindControl("lblCheckPointID")
                    For j = 0 To dt1.Rows.Count - 1
                        If dt1.Rows(j)("SAC_CheckPointID") = lblCheckPointID.Text Then
                            chkSelectCheckList.Checked = True
                            If dt1.Rows(j)("SAC_Mandatory") = 1 Then
                                chkSelectMandatory.Checked = True
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSAAsignedCheckPointsAndTeamMembers" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class