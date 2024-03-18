Imports System
Imports System.Data
Imports BusinesLayer
Partial Class EmpAssignmentSubTask
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_EmpAssignmentSubTask"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsAttachments As New clsAttachments
    Private sSession As AllSession
    Private Shared dtEmpSubTask As New DataTable
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        btnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnBack.Visible = False
                lnkbtnAddUpdateSubTask.Visible = False
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomers() : BindPartners()
                BindTasks() : BindWorkStatus()
                BindEmployees()
                If Request.QueryString("FinancialYearID") IsNot Nothing Then
                    ddlFinancialYear.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FinancialYearID")))
                End If
                BindScheduledAssignment()
                If Request.QueryString("AssignmentID") IsNot Nothing Then
                    ddlAssignmentNo.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AssignmentID")))
                    lblAsgID.Text = ddlAssignmentNo.SelectedValue
                    LoadScheduledAssignmentDetails()
                    imgbtnBack.Visible = True
                End If

                RFVWorkStatus.InitialValue = "Select Work Status" : RFVWorkStatus.ErrorMessage = "Select Work Status."
                txtDate.Text = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                RFVDate.ControlToValidate = "txtDate" : RFVDate.ErrorMessage = "Enter Date."
                REVDate.ErrorMessage = "Enter valid Date." : REVDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVComments.ControlToValidate = "txtComments" : RFVComments.ErrorMessage = "Enter Comments."
                REVComments.ErrorMessage = "Description exceeded maximum size(max 2000 characters)." : REVComments.ValidationExpression = "^[\s\S]{0,2000}$"

                lblSize.Text = "(Max " & sSession.FileSize & "MB)"
                'btnAttachment.Attributes.Add("OnClick", "$('#myModalMainAttchment').modal('show');return false;")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oFinancialYearID As New Object, oCustomerID As New Object, oPartnerID As New Object, oTaskID As New Object, oEmpID As New Object, oWorkStatusID As New Object, oComplianceID As New Object
        Dim iFinancialYearID As Integer = 0, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iTaskID As Integer = 0, iEmpID As Integer = 0
        Dim sWorkStatusID As String = "", sComplianceID As String = ""
        Dim iBackID As Integer = 0
        Try
            lblError.Text = ""
            If Request.QueryString("FinancialYearID") IsNot Nothing Then
                iFinancialYearID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FinancialYearID")))
            End If
            If Request.QueryString("CustomerID") IsNot Nothing Then
                iCustomerID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustomerID")))
            End If
            If Request.QueryString("PartnerID") IsNot Nothing Then
                iPartnerID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("PartnerID")))
            End If
            If Request.QueryString("TaskID") IsNot Nothing Then
                iTaskID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("TaskID")))
            End If
            If Request.QueryString("EmpID") IsNot Nothing Then
                iEmpID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("EmpID")))
            End If
            If Request.QueryString("WorkStatusID") IsNot Nothing Then
                sWorkStatusID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("WorkStatusID")))
            End If
            If Request.QueryString("ComplianceID") IsNot Nothing Then
                sComplianceID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ComplianceID")))
            End If
            If Request.QueryString("BackID") IsNot Nothing Then
                iBackID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("BackID")))
            End If
            oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iFinancialYearID))
            oCustomerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCustomerID))
            oPartnerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iPartnerID))
            oTaskID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iTaskID))
            oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iEmpID))
            oWorkStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sWorkStatusID))
            oComplianceID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sComplianceID))
            If iBackID = 0 Then
                Response.Redirect(String.Format("~/Assignment/AssignmentsDashboard.aspx?FinancialYearID={0}&CustomerID={1}&PartnerID={2}&TaskID={3}&EmpID={4}&WorkStatusID={5}&ComplianceID={6}", oFinancialYearID, oCustomerID, oPartnerID, oTaskID, oEmpID, oWorkStatusID, oComplianceID), False)
            Else
                Response.Redirect("~/HomePages/Home.aspx", False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindScheduledAssignment()
        Dim iFinancialYearID As Integer, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iTaskID As Integer = 0
        Try
            iFinancialYearID = ddlFinancialYear.SelectedValue
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlCustomerName.SelectedValue
            End If
            If ddlPartner.SelectedIndex > 0 Then
                iPartnerID = ddlPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                ddlAssignmentNo.DataSource = objclsAuditAssignment.LoadScheduledAssignment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, 0, 0)
            Else
                ddlAssignmentNo.DataSource = objclsAuditAssignment.LoadScheduledAssignment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, sSession.UserID, 0)
            End If
            ddlAssignmentNo.DataTextField = "AAS_AssignmentNo"
            ddlAssignmentNo.DataValueField = "AAS_ID"
            ddlAssignmentNo.DataBind()
            ddlAssignmentNo.Items.Insert(0, "Select Assignment No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledAssignment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsAuditAssignment.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
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
    Public Sub BindPartners()
        Try
            ddlPartner.DataSource = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlPartner.DataTextField = "USr_FullName"
            ddlPartner.DataValueField = "USR_ID"
            ddlPartner.DataBind()
            ddlPartner.Items.Insert(0, "Select Partner")
            If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                'ddlPartner.SelectedValue = sSession.UserID
                lnkbtnAddUpdateSubTask.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartners" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindTasks()
        Try
            ddlTask.DataSource = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlTask.DataTextField = "Name"
            ddlTask.DataValueField = "PKID"
            ddlTask.DataBind()
            ddlTask.Items.Insert(0, "Select Assignment/Task")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTasks" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindEmployees()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)
            lstAssistedByEmployees.DataSource = dt
            lstAssistedByEmployees.DataTextField = "FullName"
            lstAssistedByEmployees.DataValueField = "Usr_ID"
            lstAssistedByEmployees.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindEmployees" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssignmentSubTask_PreRender(sender As Object, e As EventArgs) Handles gvAssignmentSubTask.PreRender
        Dim dt As New DataTable
        Try
            If gvAssignmentSubTask.Rows.Count > 0 Then
                gvAssignmentSubTask.UseAccessibleHeader = True
                gvAssignmentSubTask.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssignmentSubTask.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignmentSubTask_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvAssignmentSubTask_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim chkSelectAsgSubTask As New CheckBox
        Dim lnkSubTask As New LinkButton, lblSubTask As New Label, lblEmployeeId As New Label, lblAssistedBy As New Label, lblDueDate As New Label, lblClosed As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                chkSelectAsgSubTask = CType(e.Row.FindControl("chkSelectAsgSubTask"), CheckBox)
                lnkSubTask = CType(e.Row.FindControl("lnkSubTask"), LinkButton)
                lblSubTask = CType(e.Row.FindControl("lblSubTask"), Label)
                lblEmployeeId = CType(e.Row.FindControl("lblEmployeeId"), Label)
                lblAssistedBy = CType(e.Row.FindControl("lblAssistedBy"), Label)
                lblDueDate = CType(e.Row.FindControl("lblDueDate"), Label)
                lblClosed = CType(e.Row.FindControl("lblClosed"), Label)
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    chkSelectAsgSubTask.Enabled = True : lnkSubTask.Visible = True : lblSubTask.Visible = False
                ElseIf Val(lblEmployeeId.Text) = sSession.UserID Or lblAssistedBy.Text.Contains("," & sSession.UserID & ",") = True Then
                    chkSelectAsgSubTask.Enabled = True : lnkSubTask.Visible = True : lblSubTask.Visible = False
                Else
                    chkSelectAsgSubTask.Enabled = False : lnkSubTask.Visible = False : lblSubTask.Visible = True
                End If
                If Val(lblClosed.Text) = 1 Then
                    chkSelectAsgSubTask.Enabled = False
                End If
                Dim dDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dDueDate As DateTime = Date.ParseExact(lblDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim d1 As Integer = DateDiff(DateInterval.Day, dDueDate, dDate)
                If d1 < 0 Then
                    chkSelectAsgSubTask.Enabled = False : lnkSubTask.Visible = False : lblSubTask.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignmentSubTask_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadAssignmentSubTaskDetails()
        Dim iFinancialYearID As Integer, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iTaskID As Integer = 0
        Try
            iFinancialYearID = ddlFinancialYear.SelectedValue
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlCustomerName.SelectedValue
            End If
            If ddlPartner.SelectedIndex > 0 Then
                iPartnerID = ddlPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            gvAssignmentSubTask.DataSource = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, 0, iFinancialYearID, iCustomerID, iPartnerID, iTaskID)
            gvAssignmentSubTask.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAssignmentSubTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindWorkStatus()
        Try
            ddlWorkStatus.DataSource = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
            ddlWorkStatus.DataTextField = "Name"
            ddlWorkStatus.DataValueField = "PKID"
            ddlWorkStatus.DataBind()
            ddlWorkStatus.Items.Insert(0, "Select Work Status")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadScheduledAssignmentDetails()
        Dim dt As New DataTable
        Try
            If ddlAssignmentNo.SelectedIndex > 0 Then
                dt = objclsAuditAssignment.GetScheduledAssignmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    ddlFinancialYear.SelectedValue = dt.Rows(0).Item("AAS_YearID")
                    ddlCustomerName.SelectedValue = dt.Rows(0).Item("AAS_CustID")
                    ddlPartner.SelectedValue = dt.Rows(0).Item("AAS_PartnerID")
                    ddlTask.SelectedValue = dt.Rows(0).Item("AAS_TaskID")
                    If dt.Rows(0).Item("AAS_AdvancePartialBilling") = 1 Then
                        chckAdvancePartialBilling.Checked = True
                    End If
                    If IsDBNull(dt.Rows(0)("AAS_FolderPath")) = False Then
                        txtFolderPath.Text = dt.Rows(0)("AAS_FolderPath")
                    End If
                    If IsDBNull(dt.Rows(0)("AAS_AttachID")) = False Then
                        btnAttachment.Visible = True : lblBadgeCount.Visible = True
                        iAttachID = dt.Rows(0)("AAS_AttachID")
                        BindAllAttachments(sSession.AccessCode, iAttachID)
                    End If
                    If IsDBNull(dt.Rows(0)("AAS_Status")) = False Then
                        lblBrowse.Visible = True : lblSize.Visible = True : txtfile.Visible = True : btnAddAttch.Visible = True
                        If dt.Rows(0)("AAS_Status") = 2 Then
                            lblBrowse.Visible = False : lblSize.Visible = False : txtfile.Visible = False : btnAddAttch.Visible = False
                        End If
                    End If
                    For i = 0 To lstAssistedByEmployees.Items.Count - 1
                        lstAssistedByEmployees.Items(i).Selected = False
                    Next
                    Dim sAssistedByEmployees As String = objclsAuditAssignment.GetScheduledAsgAssistedByEmpDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue)
                    If sAssistedByEmployees <> "" Then
                        If sAssistedByEmployees.StartsWith(",") = False Then
                            sAssistedByEmployees = "," & sAssistedByEmployees
                        End If
                        If sAssistedByEmployees.EndsWith(",") = False Then
                            sAssistedByEmployees = sAssistedByEmployees & ","
                        End If
                        For j = 0 To lstAssistedByEmployees.Items.Count - 1
                            If sAssistedByEmployees.Contains("," & lstAssistedByEmployees.Items(j).Value & ",") = True Then
                                lstAssistedByEmployees.Items(j).Selected = True
                            End If
                        Next
                    End If
                    btnLoadEmpDetails.Visible = True
                End If
                gvAssignmentSubTask.DataSource = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, 0, 0, 0, 0)
                gvAssignmentSubTask.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadScheduledAssignmentDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlAssignmentNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssignmentNo.SelectedIndexChanged
        Try
            ddlCustomerName.SelectedIndex = 0 : ddlPartner.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            If ddlAssignmentNo.SelectedIndex > 0 Then
                lblAsgID.Text = ddlAssignmentNo.SelectedValue
                LoadScheduledAssignmentDetails()
                gvAssignmentSubTask.DataSource = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, 0, 0, 0, 0)
                gvAssignmentSubTask.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAssignmentNo_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            'ddlCustomerName.SelectedIndex = 0 : ddlPartner.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            BindScheduledAssignment()
            LoadAssignmentSubTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            'ddlAssignmentNo.SelectedIndex = 0 : ddlPartner.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            BindScheduledAssignment()
            LoadAssignmentSubTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlPartner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPartner.SelectedIndexChanged
        Try
            'ddlAssignmentNo.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            BindScheduledAssignment()
            LoadAssignmentSubTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPartner_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlTask_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTask.SelectedIndexChanged
        Try
            'ddlAssignmentNo.SelectedIndex = 0
            Clear()
            BindScheduledAssignment()
            LoadAssignmentSubTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTask_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblError.Text = ""
            chckAdvancePartialBilling.Checked = False
            txtFolderPath.Text = ""
            gvAssignmentSubTask.DataSource = Nothing
            gvAssignmentSubTask.DataBind()
            imgbtnSave.Visible = False
            divEmpDetails.Visible = False
            gvEmpAssignmentSubTask.DataSource = Nothing
            gvEmpAssignmentSubTask.DataBind()
            btnLoadEmpDetails.Visible = False
            chkClose.Checked = False
            chkReview.Checked = False
            txtComments.Text = ""
            lblTaskSubTaskId.Text = "" : lblSubTaskName.Text = "" : lblEmployeeName.Text = ""
            lblFrequencyName.Text = "" : lblCreatedByName.Text = "" : lblCreatedDate.Text = ""
            btnAttachment.Visible = False : lblBadgeCount.Visible = False
            iAttachID = 0 : lblBadgeCount.Text = 0
            dgMainAttach.DataSource = Nothing
            dgMainAttach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lstAssistedByEmployees_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstAssistedByEmployees.SelectedIndexChanged
        Dim i As Integer
        Dim sAssistedByEmployeesIDs As String = ""
        Dim iCheck As Integer = 0
        Try
            lblError.Text = ""
            If ddlAssignmentNo.SelectedIndex > 0 Then
                For i = 0 To lstAssistedByEmployees.Items.Count - 1
                    If lstAssistedByEmployees.Items(i).Selected = True Then
                        sAssistedByEmployeesIDs = sAssistedByEmployeesIDs & "," & lstAssistedByEmployees.Items(i).Value
                    End If
                Next
                sAssistedByEmployeesIDs = sAssistedByEmployeesIDs & ","
                objclsAuditAssignment.UpdateScheduledAsgAssistedByEmployeesDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, sAssistedByEmployeesIDs)
                lblError.Text = "Successfully updated Assisted By Employee details." : lblAAValidationMsg.Text = "Successfully updated Assisted By Employee details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
            Else
                lblError.Text = "Select Assignment No." : lblAAValidationMsg.Text = "Select Assignment No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
                ddlAssignmentNo.Focus()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstAssistedByEmployees_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssignmentSubTask_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAssignmentSubTask.RowCommand
        Dim lblAssignmentID As New Label, lblDBpkId As New Label, lblWorkStatusId As New Label, lblEmployee As New Label, lnkSubTask As New LinkButton
        Dim lblClosed As New Label, lblReview As New Label, lblFrequency As New Label, lblCreatedBy As New Label, lblCreatedOn As New Label
        Try
            For j = 0 To gvAssignmentSubTask.Rows.Count - 1
                lnkSubTask = gvAssignmentSubTask.Rows(j).FindControl("lnkSubTask")
                lnkSubTask.Attributes.Add("style", "text-decoration: none;")
            Next

            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblAssignmentID = CType(clickedRow.FindControl("lblAssignmentID"), Label)
            lblDBpkId = CType(clickedRow.FindControl("lblDBpkId"), Label)
            lblWorkStatusId = CType(clickedRow.FindControl("lblWorkStatusId"), Label)
            lblEmployee = CType(clickedRow.FindControl("lblEmployee"), Label)
            lnkSubTask = CType(clickedRow.FindControl("lnkSubTask"), LinkButton)
            lblClosed = CType(clickedRow.FindControl("lblClosed"), Label)
            lblReview = CType(clickedRow.FindControl("lblReview"), Label)
            lblFrequency = CType(clickedRow.FindControl("lblFrequency"), Label)
            lblCreatedBy = CType(clickedRow.FindControl("lblCreatedBy"), Label)
            lblCreatedOn = CType(clickedRow.FindControl("lblCreatedOn"), Label)
            If e.CommandName = "Select" Then
                imgbtnSave.Visible = True : divEmpDetails.Visible = True
                lblAsgID.Text = Val(lblAssignmentID.Text)
                lblTaskSubTaskId.Text = Val(lblDBpkId.Text)
                lblEmployeeName.Text = sSession.UserFullName
                lblSubTaskName.Text = lnkSubTask.Text
                ddlWorkStatus.SelectedValue = Val(lblWorkStatusId.Text)
                If Val(lblClosed.Text) = 1 Then
                    imgbtnSave.Visible = False : divEmpDetails.Visible = False
                End If
                If Val(lblReview.Text) = 1 Then
                    chkReview.Checked = True
                Else
                    chkReview.Checked = False
                End If
                lblFrequencyName.Text = lblFrequency.Text
                lblCreatedByName.Text = lblCreatedBy.Text
                lblCreatedDate.Text = lblCreatedOn.Text
                LoadEmpAssignmentSubTaskDetails()
                txtComments.Focus()
                lnkSubTask.Attributes.Add("style", "text-decoration: underline;")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignmentSubTask_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function SaveEmployeeSubTaskDetails() As Array
        Dim objAAEST As New strAuditAssignment_EmpSubTask
        Dim Arr() As String
        Dim sArray As Array
        Try
            sArray = lblTaskSubTaskId.Text.Split(",")
            For i = 0 To sArray.Length - 1
                If sArray(i) <> "" Then
                    objAAEST.iAAEST_ID = 0
                    objAAEST.iAAEST_AAS_ID = lblAsgID.Text
                    objAAEST.iAAEST_AAST_ID = Val(sArray(i))
                    objAAEST.iAAEST_WorkStatusID = ddlWorkStatus.SelectedValue
                    If chkClose.Checked = True Then
                        objAAEST.iAAST_Closed = 1
                    Else
                        objAAEST.iAAST_Closed = 0
                    End If
                    If chkReview.Checked = True Then
                        objAAEST.iAAST_Review = 1
                    Else
                        objAAEST.iAAST_Review = 0
                    End If
                    objAAEST.sAAEST_Comments = txtComments.Text
                    objAAEST.iAAEST_AttachID = 0
                    objAAEST.iAAEST_CrBy = sSession.UserID
                    objAAEST.dAAEST_CrOn = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objAAEST.sAAEST_IPAddress = sSession.IPAddress
                    objAAEST.iAAEST_CompID = sSession.AccessCodeID
                    Arr = objclsAuditAssignment.SaveEmployeeSubTaskDetails(sSession.AccessCode, objAAEST)
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignment", "Task Assignments", "Saved", Arr(1), lblAsgID.Text, Val(sArray(i)), ddlWorkStatus.SelectedValue, sSession.IPAddress)
                    If chkClose.Checked = True Then
                        objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, lblAsgID.Text, 1)
                    End If
                    objclsAuditAssignment.UpdateScheduledStatusAndFolderPath(sSession.AccessCode, sSession.AccessCodeID, txtFolderPath.Text, lblAsgID.Text, iAttachID)
                    objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, lblAsgID.Text)
                End If
            Next
            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeSubTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim Array() As String
        Dim sMessage As String
        Try
            If chkClose.Checked = True And ddlWorkStatus.SelectedItem.Text.ToLower() <> "completed" Then
                lblError.Text = "Please select the Work Status to 'Completed' Or uncheck the 'Close' checkbox, because the 'Close' checkbox is selected."
                lblAAValidationMsg.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                chkClose.Checked = True
            End If
            Array = SaveEmployeeSubTaskDetails()
            lblError.Text = "Successfully Saved." : lblAAValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)

            If ddlAssignmentNo.SelectedIndex > 0 Then
                gvAssignmentSubTask.DataSource = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, 0, 0, 0, 0)
                gvAssignmentSubTask.DataBind()
            Else
                LoadAssignmentSubTaskDetails()
            End If
            LoadEmpAssignmentSubTaskDetails()

            If chkClose.Checked = True Then
                imgbtnSave.Visible = False : divEmpDetails.Visible = False
            End If

            '' Web service calling
            Dim service As New traceapi()
            service.CreateCabinet(sSession.AccessCode, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, "G", "1", "0", "0", "0", "0", "0")
            service.CreateSubCabinet(sSession.AccessCode, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, ddlFinancialYear.SelectedItem.Text, "G", "1", "0", "0", "0", "0", "0", "0")
            service.CreateFolder(sSession.AccessCode, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, ddlFinancialYear.SelectedItem.Text, ddlAssignmentNo.SelectedItem.Text, "G", "1", "0", "0", "0", "0", "0")

            Dim sPaths As String
            'sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

            'If sPaths.EndsWith("\") = True Then
            '    sPaths = sPaths & "Uploads\"
            'Else
            '    sPaths = sPaths & "\Uploads\"
            'End If

            'Dim lnkFile As New LinkButton
            'Dim sFilePath As String, sFileName As String
            'For i = 0 To dgMainAttach.Items.Count - 1
            '    sFilePath = "" : sFileName = ""
            '    lnkFile = dgMainAttach.Items(i).FindControl("File")
            '    sFilePath = sPaths & lnkFile.Text
            '    sFileName = System.IO.Path.GetFileNameWithoutExtension(sFilePath)
            '    sMessage = service.FileDocumentINEdictNew(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, ddlFinancialYear.SelectedItem.Text, ddlAssignmentNo.SelectedItem.Text, sFileName, sFileName, sFilePath)
            '    If sMessage <> "" Then
            '        ClientScript.RegisterStartupScript(Me.[GetType](), "myalert", "alert('" & sMessage & "');", True)
            '        Exit Sub
            '    End If
            'Next

            Dim lnkFile As New LinkButton
            Dim sFilePath As String, sFileName As String, sDestFilePath As String
            Dim lblAtchDocID As New Label, lblFDescription As New Label
            For i = 0 To dgMainAttach.Items.Count - 1
                sFilePath = "" : sFileName = ""
                lblAtchDocID = dgMainAttach.Items(i).FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                sFileName = System.IO.Path.GetFileNameWithoutExtension(sDestFilePath)
                sMessage = service.FileDocumentINEdictNew(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, ddlCustomerName.SelectedItem.Text, ddlFinancialYear.SelectedItem.Text, ddlAssignmentNo.SelectedItem.Text, sFileName, sFileName, sDestFilePath)
                If sMessage <> "" Then
                    ClientScript.RegisterStartupScript(Me.[GetType](), "myalert", "alert('" & sMessage & "');", True)
                    Exit Sub
                End If
            Next

            chkClose.Checked = False : txtComments.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvEmpAssignmentSubTask_PreRender(sender As Object, e As EventArgs) Handles gvEmpAssignmentSubTask.PreRender
        Dim dt As New DataTable
        Try
            If gvEmpAssignmentSubTask.Rows.Count > 0 Then
                gvEmpAssignmentSubTask.UseAccessibleHeader = True
                gvEmpAssignmentSubTask.HeaderRow.TableSection = TableRowSection.TableHeader
                gvEmpAssignmentSubTask.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmpAssignmentSubTask_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadEmpAssignmentSubTaskDetails()
        Try
            gvEmpAssignmentSubTask.DataSource = objclsAuditAssignment.LoadEmpAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, Val(lblAsgID.Text), lblTaskSubTaskId.Text)
            gvEmpAssignmentSubTask.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadEmpAssignmentSubTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            lblBadgeCount.Text = dgMainAttach.Items.Count
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
    Protected Sub btnAttachment_Click(sender As Object, e As EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalMainAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckPwd_Click")
        End Try
    End Sub
    Protected Sub chckAdvancePartialBilling_CheckedChanged(sender As Object, e As EventArgs) Handles chckAdvancePartialBilling.CheckedChanged
        Dim iAdvancePartialBilling As Integer = 0
        Try
            lblError.Text = ""
            If ddlAssignmentNo.SelectedIndex > 0 Then
                If chckAdvancePartialBilling.Checked = True Then
                    iAdvancePartialBilling = 1
                End If
                objclsAuditAssignment.UpdateScheduledAsgAdvancePartialBillingDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, iAdvancePartialBilling)
                lblError.Text = "Successfully updated Advance/Partial Billing details." : lblAAValidationMsg.Text = "Successfully updated Advance/Partial Billing details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
            Else
                lblError.Text = "Select Assignment No." : lblAAValidationMsg.Text = "Select Assignment No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
                ddlAssignmentNo.Focus()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chckAdvancePartialBilling_CheckedChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAllAsgSubTask_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectAsgSubTask As New CheckBox
        Dim lblEmployeeId As New Label, lblDueDate As New Label, lblClosed As New Label
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To gvAssignmentSubTask.Rows.Count - 1
                    chkSelectAsgSubTask = gvAssignmentSubTask.Rows(i).FindControl("chkSelectAsgSubTask")
                    lblEmployeeId = gvAssignmentSubTask.Rows(i).FindControl("lblEmployeeId")
                    lblEmployeeId = gvAssignmentSubTask.Rows(i).FindControl("lblEmployeeId")
                    lblClosed = gvAssignmentSubTask.Rows(i).FindControl("lblClosed")
                    If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                        chkSelectAsgSubTask.Checked = True
                    ElseIf Val(lblEmployeeId.Text) = sSession.UserID Then
                        chkSelectAsgSubTask.Checked = True
                    Else
                        chkSelectAsgSubTask.Checked = False
                    End If
                    lblDueDate = gvAssignmentSubTask.Rows(i).FindControl("lblDueDate")
                    Dim dDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim dDueDate As DateTime = Date.ParseExact(lblDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim d1 As Integer = DateDiff(DateInterval.Day, dDueDate, dDate)
                    If d1 < 0 Then
                        chkSelectAsgSubTask.Checked = False
                    End If
                    If Val(lblClosed.Text) = 1 Then
                        chkSelectAsgSubTask.Enabled = False
                    End If
                Next
            Else
                For i = 0 To gvAssignmentSubTask.Rows.Count - 1
                    chkSelectAsgSubTask = gvAssignmentSubTask.Rows(i).FindControl("chkSelectAsgSubTask")
                    chkSelectAsgSubTask.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllAsgSubTask_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnLoadEmpDetails_Click(sender As Object, e As EventArgs) Handles btnLoadEmpDetails.Click
        Dim chkSelectAsgSubTask As New CheckBox
        Dim iCheck As Integer = 0, iCheckAllClosed As Integer = 0
        Dim lblAssignmentID As New Label, lblDBpkId As New Label, lblWorkStatusId As New Label, lblEmployee As New Label, lnkSubTask As New LinkButton, lblClosed As New Label
        Dim lblFrequency As New Label, lblCreatedBy As New Label, lblCreatedOn As New Label
        Try
            lblTaskSubTaskId.Text = "" : lblSubTaskName.Text = "" : lblEmployeeName.Text = ""
            lblFrequencyName.Text = "" : lblCreatedByName.Text = "" : lblCreatedDate.Text = ""
            For j = 0 To gvAssignmentSubTask.Rows.Count - 1
                chkSelectAsgSubTask = gvAssignmentSubTask.Rows(j).FindControl("chkSelectAsgSubTask")
                If chkSelectAsgSubTask.Checked = True Then
                    iCheck = iCheck + 1
                    lnkSubTask = gvAssignmentSubTask.Rows(j).FindControl("lnkSubTask")
                    lblAssignmentID = gvAssignmentSubTask.Rows(j).FindControl("lblAssignmentID")
                    lblDBpkId = gvAssignmentSubTask.Rows(j).FindControl("lblDBpkId")
                    lblWorkStatusId = gvAssignmentSubTask.Rows(j).FindControl("lblWorkStatusId")
                    lblEmployee = gvAssignmentSubTask.Rows(j).FindControl("lblEmployee")
                    lnkSubTask = gvAssignmentSubTask.Rows(j).FindControl("lnkSubTask")
                    lblClosed = gvAssignmentSubTask.Rows(j).FindControl("lblClosed")
                    lblFrequency = gvAssignmentSubTask.Rows(j).FindControl("lblFrequency")
                    lblCreatedBy = gvAssignmentSubTask.Rows(j).FindControl("lblCreatedBy")
                    lblCreatedOn = gvAssignmentSubTask.Rows(j).FindControl("lblCreatedOn")

                    imgbtnSave.Visible = True : divEmpDetails.Visible = True
                    lblAsgID.Text = Val(lblAssignmentID.Text)
                    lblTaskSubTaskId.Text = lblTaskSubTaskId.Text & "," & Val(lblDBpkId.Text)
                    If lblEmployeeName.Text.Contains(lblEmployee.Text) = False Then
                        lblEmployeeName.Text = lblEmployeeName.Text & "," & lblEmployee.Text
                    End If
                    lblSubTaskName.Text = lblSubTaskName.Text & "," & lnkSubTask.Text
                    ddlWorkStatus.SelectedValue = Val(lblWorkStatusId.Text)
                    If Val(lblClosed.Text) = 0 Then
                        iCheckAllClosed = 1
                    End If
                    If lblFrequencyName.Text.Contains(lblFrequency.Text) = False Then
                        lblFrequencyName.Text = lblFrequencyName.Text & "," & lblFrequency.Text
                    End If
                    If lblCreatedByName.Text.Contains(lblCreatedBy.Text) = False Then
                        lblCreatedByName.Text = lblCreatedByName.Text & "," & lblCreatedBy.Text
                    End If
                    If lblCreatedDate.Text.Contains(lblCreatedOn.Text) = False Then
                        lblCreatedDate.Text = lblCreatedDate.Text & "," & lblCreatedOn.Text
                    End If
                    If iCheckAllClosed = 0 Then
                        imgbtnSave.Visible = False : divEmpDetails.Visible = False
                    End If
                End If
                If iCheck > 1 Then
                    ddlWorkStatus.SelectedIndex = 0
                End If
            Next
            If lblTaskSubTaskId.Text.StartsWith(",") Then
                lblTaskSubTaskId.Text = lblTaskSubTaskId.Text.Remove(0, 1)
            End If
            If lblTaskSubTaskId.Text.EndsWith(",") Then
                lblTaskSubTaskId.Text = lblTaskSubTaskId.Text.Remove(Len(lblTaskSubTaskId.Text) - 1, 1)
            End If

            If lblEmployeeName.Text.StartsWith(",") Then
                lblEmployeeName.Text = lblEmployeeName.Text.Remove(0, 1)
            End If
            If lblEmployeeName.Text.EndsWith(",") Then
                lblEmployeeName.Text = lblEmployeeName.Text.Remove(Len(lblEmployeeName.Text) - 1, 1)
            End If

            If lblSubTaskName.Text.StartsWith(",") Then
                lblSubTaskName.Text = lblSubTaskName.Text.Remove(0, 1)
            End If
            If lblSubTaskName.Text.EndsWith(",") Then
                lblSubTaskName.Text = lblSubTaskName.Text.Remove(Len(lblSubTaskName.Text) - 1, 1)
            End If

            If lblFrequencyName.Text.StartsWith(",") Then
                lblFrequencyName.Text = lblFrequencyName.Text.Remove(0, 1)
            End If
            If lblFrequencyName.Text.EndsWith(",") Then
                lblFrequencyName.Text = lblFrequencyName.Text.Remove(Len(lblFrequencyName.Text) - 1, 1)
            End If

            If lblCreatedByName.Text.StartsWith(",") Then
                lblCreatedByName.Text = lblCreatedByName.Text.Remove(0, 1)
            End If
            If lblCreatedByName.Text.EndsWith(",") Then
                lblCreatedByName.Text = lblCreatedByName.Text.Remove(Len(lblCreatedByName.Text) - 1, 1)
            End If

            If lblCreatedDate.Text.StartsWith(",") Then
                lblCreatedDate.Text = lblCreatedDate.Text.Remove(0, 1)
            End If
            If lblCreatedDate.Text.EndsWith(",") Then
                lblCreatedDate.Text = lblCreatedDate.Text.Remove(Len(lblCreatedDate.Text) - 1, 1)
            End If
            If lblTaskSubTaskId.Text = "" Then
                lblError.Text = ""
                imgbtnSave.Visible = False
                divEmpDetails.Visible = False
                gvEmpAssignmentSubTask.DataSource = Nothing
                gvEmpAssignmentSubTask.DataBind()
                chkClose.Checked = False
                txtComments.Text = ""
                lblTaskSubTaskId.Text = "" : lblEmployeeName.Text = ""
            Else
                LoadEmpAssignmentSubTaskDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLoadEmpDetails_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAddUpdateSubTaskGrid(ByVal iTaskID As Integer)
        Dim dtSubTask As New DataTable
        Try
            dtSubTask = objclsAdminMaster.LoadAuditAssignmentSubTask(sSession.AccessCode, sSession.AccessCodeID, iTaskID)
            If dtSubTask.Rows.Count > 4 Then
                divST.Style.Item("Height") = "185px"
            Else
                divST.Style.Item("Height") = "auto"
            End If
            gvSubTask.DataSource = dtSubTask
            gvSubTask.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSubTaskGrid" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvSubTask_PreRender(sender As Object, e As EventArgs) Handles gvSubTask.PreRender
        Try
            If gvSubTask.Rows.Count > 0 Then
                gvSubTask.UseAccessibleHeader = True
                gvSubTask.HeaderRow.TableSection = TableRowSection.TableHeader
                gvSubTask.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSubTask_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvSubTask_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSubTask.RowDataBound
        Dim ddlSTWorkStatus As New DropDownList
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                ddlSTWorkStatus = CType(e.Row.FindControl("ddlSTWorkStatus"), DropDownList)
                ddlSTWorkStatus.DataSource = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
                ddlSTWorkStatus.DataTextField = "Name"
                ddlSTWorkStatus.DataValueField = "PKID"
                ddlSTWorkStatus.DataBind()
                ddlSTWorkStatus.Items.Insert(0, "Select Work Status")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignment_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnAddUpdateSubTask_Click(sender As Object, e As EventArgs) Handles lnkbtnAddUpdateSubTask.Click
        Dim dt As New DataTable
        Dim chkSelectSubTask As New CheckBox
        Dim lblgvSubTaskID As New Label, lblgvDBpkId As New Label, lblgvDueDate As New Label, lblgvExpectedCompletionDate As New Label
        Dim lblgvFrequencyId As New Label, lblgvYearOrMonthId As New Label, lblgvEmployeeId As New Label, lblgvDescription As New Label, lblgvWorkStatusId As New Label
        Dim ddlSTWorkStatus As New DropDownList
        Dim iCount As Integer = 0
        Try
            lblSTError.Text = ""
            If ddlAssignmentNo.SelectedIndex > 0 And ddlTask.SelectedIndex > 0 Then
                BindAddUpdateSubTaskGrid(ddlTask.SelectedValue)
                dt = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, 0, 0, 0, 0)
                For i = 0 To gvSubTask.Rows.Count - 1
                    chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                    lblgvSubTaskID = gvSubTask.Rows(i).FindControl("lblgvSubTaskID")
                    ddlSTWorkStatus = gvSubTask.Rows(i).FindControl("ddlSTWorkStatus")
                    lblgvDBpkId = gvSubTask.Rows(i).FindControl("lblgvDBpkId")
                    lblgvWorkStatusId = gvSubTask.Rows(i).FindControl("lblgvWorkStatusId")

                    lblgvDueDate = gvSubTask.Rows(0).FindControl("lblgvDueDate")
                    lblgvExpectedCompletionDate = gvSubTask.Rows(0).FindControl("lblgvExpectedCompletionDate")
                    lblgvFrequencyId = gvSubTask.Rows(0).FindControl("lblgvFrequencyId")
                    lblgvYearOrMonthId = gvSubTask.Rows(0).FindControl("lblgvYearOrMonthId")
                    lblgvEmployeeId = gvSubTask.Rows(0).FindControl("lblgvEmployeeId")
                    lblgvDescription = gvSubTask.Rows(0).FindControl("lblgvDescription")
                    For j = 0 To dt.Rows.Count - 1
                        If dt.Rows(j)("SubTaskId") = lblgvSubTaskID.Text Then
                            lblgvDBpkId.Text = dt.Rows(j)("DBpkId")
                            chkSelectSubTask.Checked = True : chkSelectSubTask.Enabled = False
                            ddlSTWorkStatus.SelectedValue = dt.Rows(j)("WorkStatusId")
                            lblgvWorkStatusId.Text = dt.Rows(j)("WorkStatusId")
                            If iCount = 0 Then
                                lblgvDueDate.Text = dt.Rows(j)("DueDate")
                                lblgvExpectedCompletionDate.Text = dt.Rows(j)("ExpectedCompletionDate")
                                lblgvFrequencyId.Text = dt.Rows(j)("FrequencyId")
                                lblgvYearOrMonthId.Text = dt.Rows(j)("YearOrMonthID")
                                lblgvEmployeeId.Text = dt.Rows(j)("EmployeeId")
                                lblgvDescription.Text = dt.Rows(j)("Description")
                            End If
                            iCount = 1
                        End If
                    Next
                Next
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAddUpdateSubTaskModal').modal('show')", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAddUpdateSubTask_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnAddUpdateSubTask_Click(sender As Object, e As EventArgs) Handles btnAddUpdateSubTask.Click
        Dim objAAEST As New strAuditAssignment_EmpSubTask
        Dim Arr() As String
        Dim ArrAAST() As String
        Dim iDBpkId As Integer
        Dim lblgvSubTaskID As New Label, lblgvDBpkId As New Label, lblgvDueDate As New Label, lblgvExpectedCompletionDate As New Label
        Dim lblgvFrequencyId As New Label, lblgvYearOrMonthId As New Label, lblgvEmployeeId As New Label, lblgvDescription As New Label, lblgvWorkStatusId As New Label
        Dim chkSelectSubTask As New CheckBox
        Dim ddlSTWorkStatus As New DropDownList
        Try
            For x = 0 To gvSubTask.Rows.Count - 1
                chkSelectSubTask = gvSubTask.Rows(x).FindControl("chkSelectSubTask")
                ddlSTWorkStatus = gvSubTask.Rows(x).FindControl("ddlSTWorkStatus")
                If chkSelectSubTask.Checked = True And ddlSTWorkStatus.SelectedIndex = 0 Then
                    lblSTError.Text = "Select Work Status for selected Sub Task."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAddUpdateSubTaskModal').modal('show')", True)
                    Exit Try
                End If
            Next

            For i = 0 To gvSubTask.Rows.Count - 1
                lblgvDueDate = gvSubTask.Rows(0).FindControl("lblgvDueDate")
                lblgvExpectedCompletionDate = gvSubTask.Rows(0).FindControl("lblgvExpectedCompletionDate")
                lblgvFrequencyId = gvSubTask.Rows(0).FindControl("lblgvFrequencyId")
                lblgvYearOrMonthId = gvSubTask.Rows(0).FindControl("lblgvYearOrMonthId")
                lblgvEmployeeId = gvSubTask.Rows(0).FindControl("lblgvEmployeeId")
                lblgvDescription = gvSubTask.Rows(0).FindControl("lblgvDescription")

                chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                If chkSelectSubTask.Checked = True Then
                    lblgvDBpkId = gvSubTask.Rows(i).FindControl("lblgvDBpkId")
                    iDBpkId = Val(lblgvDBpkId.Text)
                    ddlSTWorkStatus = gvSubTask.Rows(i).FindControl("ddlSTWorkStatus")
                    lblgvSubTaskID = gvSubTask.Rows(i).FindControl("lblgvSubTaskID")
                    lblgvWorkStatusId = gvSubTask.Rows(i).FindControl("lblgvWorkStatusId")

                    Dim dDueDate As DateTime = Date.ParseExact(lblgvDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim dExpectedCompletionDate As DateTime = Date.ParseExact(lblgvExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    If iDBpkId = 0 Then
                        Dim objAAST As New strAuditAssignment_SubTask
                        objAAST.iAAST_AAS_ID = Val(lblAsgID.Text)
                        objAAST.iAAST_SubTaskID = Val(lblgvSubTaskID.Text)
                        objAAST.iAAST_EmployeeID = Val(lblgvEmployeeId.Text)
                        objAAST.sAAST_AssistedByEmployeesID = ","
                        objAAST.sAAST_Desc = objclsGRACeGeneral.SafeSQL(lblgvDescription.Text.Trim)
                        objAAST.iAAST_FrequencyID = Val(lblgvFrequencyId.Text)
                        objAAST.iAAST_YearOrMonthID = Val(lblgvYearOrMonthId.Text)
                        objAAST.dAAST_DueDate = dDueDate
                        objAAST.dAAST_ExpectedCompletionDate = dExpectedCompletionDate
                        objAAST.iAAST_WorkStatusID = ddlSTWorkStatus.SelectedValue
                        objAAST.iAAST_CrBy = sSession.UserID
                        objAAST.sAAST_IPAddress = sSession.IPAddress
                        objAAST.iAAST_CompID = sSession.AccessCodeID
                        ArrAAST = objclsAuditAssignment.SaveAuditAssignmentEmpSubTask(sSession.AccessCode, objAAST)
                        iDBpkId = ArrAAST(1)
                    End If
                    If (Val(lblgvWorkStatusId.Text) = 0 And iDBpkId > 0) Or (Val(lblgvWorkStatusId.Text) > 0 And (Val(lblgvWorkStatusId.Text) <> ddlSTWorkStatus.SelectedValue)) Then
                        objAAEST.iAAEST_ID = 0
                        objAAEST.iAAEST_AAS_ID = Val(lblAsgID.Text)
                        objAAEST.iAAEST_AAST_ID = iDBpkId
                        objAAEST.iAAEST_WorkStatusID = ddlSTWorkStatus.SelectedValue
                        If ddlSTWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                            objAAEST.iAAST_Closed = 1
                        Else
                            objAAEST.iAAST_Closed = 0
                        End If
                        objAAEST.iAAST_Review = 0
                        objAAEST.sAAEST_Comments = "Changes done by Partner"
                        objAAEST.iAAEST_AttachID = 0
                        objAAEST.iAAEST_CrBy = sSession.UserID
                        objAAEST.dAAEST_CrOn = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        objAAEST.sAAEST_IPAddress = sSession.IPAddress
                        objAAEST.iAAEST_CompID = sSession.AccessCodeID
                        Arr = objclsAuditAssignment.SaveEmployeeSubTaskDetails(sSession.AccessCode, objAAEST)
                        objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignment", "Task Assignments", "Saved", Arr(1), lblAsgID.Text, Val(lblgvDBpkId.Text), ddlSTWorkStatus.SelectedValue, sSession.IPAddress)
                        If chkClose.Checked = True Then
                            objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, lblAsgID.Text, 1)
                        Else
                            If ddlSTWorkStatus.SelectedItem.Text.ToLower() <> "completed" Then
                                objclsAuditAssignment.UpdateAsgSubTaskClosedDetails(sSession.AccessCode, sSession.AccessCodeID, iDBpkId)
                            End If
                        End If
                        objclsAuditAssignment.UpdateScheduledStatusAndFolderPath(sSession.AccessCode, sSession.AccessCodeID, txtFolderPath.Text, lblAsgID.Text, iAttachID)
                        objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, lblAsgID.Text)
                    End If
                End If
            Next
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAddUpdateSubTaskModal').modal('hide')", True)

            lblError.Text = "Successfully Modified Sub Task." : lblAAValidationMsg.Text = "Successfully Modified Sub Task."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAAValidation').modal('show');", True)
            ddlAssignmentNo_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddUpdateSubTask_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class