Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.DataVisualization.Charting
Imports System.IO
Imports System.Web.Services
Partial Class AssignmentsDashboard
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_AssignmentsDashboard"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAdminMaster As New clsAdminMaster
    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
    Public Shared sFilePath As String = ""
    Public Shared strarray As Array = {(0), (1), (2), (3)}
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnLoad.ImageUrl = "~/Images/Load24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sWSIDs As String = "", sComplianceTypeID As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                    'lblMsg.Text = sSession.UserFullName & " does not have permission to view this page."
                    'divEmp.Visible = True : divPartner.Visible = False : imgbtnReport.Visible = False
                    'Exit Sub
                End If

                divEmp.Visible = False : divPartner.Visible = True : imgbtnReport.Visible = True
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomers() : BindPartner() : BindTasks() : BindEmployees() : BindWorkStatus() : BindComplianceType()
                If Request.QueryString("FinancialYearID") IsNot Nothing Then
                    ddlAsignmentFinancialYear.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FinancialYearID")))
                End If
                If Request.QueryString("PartnerID") IsNot Nothing Then
                    ddlAsignmentPartner.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("PartnerID")))
                End If
                If Request.QueryString("CustomerID") IsNot Nothing Then
                    ddlAsignmentCustomerName.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustomerID")))
                End If
                If Request.QueryString("TaskID") IsNot Nothing Then
                    ddlTask.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("TaskID")))
                End If
                If Request.QueryString("EmpID") IsNot Nothing Then
                    ddlAssignmentEmployee.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("EmpID")))
                End If
                If Request.QueryString("WorkStatusID") IsNot Nothing Then
                    For i = 0 To lstWorkStatus.Items.Count - 1
                        lstWorkStatus.Items(i).Selected = False
                    Next

                    sWSIDs = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("WorkStatusID")))
                    If sWSIDs.StartsWith(",") = False Then
                        sWSIDs = "," & sWSIDs
                    End If
                    If sWSIDs.EndsWith(",") = False Then
                        sWSIDs = sWSIDs & ","
                    End If
                    For j = 0 To lstWorkStatus.Items.Count - 1
                        If sWSIDs.Contains("," & lstWorkStatus.Items(j).Value & ",") = True Then
                            lstWorkStatus.Items(j).Selected = True
                        End If
                    Next
                End If
                If Request.QueryString("ComplianceID") IsNot Nothing Then
                    For i = 0 To lstComplianceType.Items.Count - 1
                        lstComplianceType.Items(i).Selected = False
                    Next

                    sComplianceTypeID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("ComplianceID")))
                    If sComplianceTypeID.StartsWith(",") = False Then
                        sComplianceTypeID = "," & sComplianceTypeID
                    End If
                    If sComplianceTypeID.EndsWith(",") = False Then
                        sComplianceTypeID = sComplianceTypeID & ","
                    End If
                    For j = 0 To lstComplianceType.Items.Count - 1
                        If sComplianceTypeID.Contains("," & lstComplianceType.Items(j).Value & ",") = True Then
                            lstComplianceType.Items(j).Selected = True
                        End If
                    Next
                End If
                BindAllScheduledAssignment()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlAsignmentFinancialYear.DataSource = objclsAuditAssignment.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlAsignmentFinancialYear.DataTextField = "YMS_ID"
            ddlAsignmentFinancialYear.DataValueField = "YMS_YearID"
            ddlAsignmentFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlAsignmentFinancialYear.SelectedValue = iYearID
                    Else
                        ddlAsignmentFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlAsignmentFinancialYear.SelectedValue = sSession.YearID
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
            ddlAsignmentCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlAsignmentCustomerName.DataTextField = "CUST_Name"
            ddlAsignmentCustomerName.DataValueField = "CUST_ID"
            ddlAsignmentCustomerName.DataBind()
            ddlAsignmentCustomerName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPartner()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlAsignmentPartner.DataSource = dt
            ddlAsignmentPartner.DataTextField = "USr_FullName"
            ddlAsignmentPartner.DataValueField = "USR_ID"
            ddlAsignmentPartner.DataBind()
            ddlAsignmentPartner.Items.Insert(0, "Select Partner")

            'If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
            '    ddlAsignmentPartner.SelectedValue = sSession.UserID
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartner" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            ddlAssignmentEmployee.DataSource = dt
            ddlAssignmentEmployee.DataTextField = "FullName"
            ddlAssignmentEmployee.DataValueField = "Usr_ID"
            ddlAssignmentEmployee.DataBind()
            ddlAssignmentEmployee.Items.Insert(0, "Select Employee")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindEmployees" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindWorkStatus()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
            lstWorkStatus.DataSource = dt
            lstWorkStatus.DataTextField = "Name"
            lstWorkStatus.DataValueField = "PKID"
            lstWorkStatus.DataBind()
            Dim sWStext As String = ",yet to start,wip,work in progress,"

            For j = 0 To lstWorkStatus.Items.Count - 1
                If sWStext.Contains("," & lstWorkStatus.Items(j).Text.ToLower.ToString() & ",") = True Then
                    lstWorkStatus.Items(j).Selected = True
                End If
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindComplianceType()
        Try
            lstComplianceType.Items.Add(New ListItem("Assignment", "0"))
            lstComplianceType.Items.Add(New ListItem("Compliance", "1"))
            lstComplianceType.DataBind()
            lstComplianceType.Items(0).Selected = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCompliance" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Function GetSelectedWorkStatus() As String
        Dim i As Integer
        Dim sWSIDs As String = ""
        Try
            For i = 0 To lstWorkStatus.Items.Count - 1
                If lstWorkStatus.Items(i).Selected = True Then
                    sWSIDs = sWSIDs & "," & lstWorkStatus.Items(i).Value
                End If
            Next
            If sWSIDs.StartsWith(",") Then
                sWSIDs = sWSIDs.Remove(0, 1)
            End If
            If sWSIDs.EndsWith(",") Then
                sWSIDs = sWSIDs.Remove(Len(sWSIDs) - 1, 1)
            End If
            Return sWSIDs
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSelectedResourceIDs" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function GetSelectedComplianceType() As String
        Dim i As Integer
        Dim sComplianceTypeIDs As String = ""
        Try
            For i = 0 To lstComplianceType.Items.Count - 1
                If lstComplianceType.Items(i).Selected = True Then
                    sComplianceTypeIDs = sComplianceTypeIDs & "," & lstComplianceType.Items(i).Value
                End If
            Next
            If sComplianceTypeIDs.StartsWith(",") Then
                sComplianceTypeIDs = sComplianceTypeIDs.Remove(0, 1)
            End If
            If sComplianceTypeIDs.EndsWith(",") Then
                sComplianceTypeIDs = sComplianceTypeIDs.Remove(Len(sComplianceTypeIDs) - 1, 1)
            End If
            Return sComplianceTypeIDs
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSelectedResourceIDs" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("ScheduleAssignments.aspx"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnLoad_Click(sender As Object, e As EventArgs) Handles imgbtnLoad.Click
        Try
            lblError.Text = ""
            divAssignmentheader.Attributes.Remove("class")
            divAssignmentBody.Attributes.Remove("class")
            divAssignmentheader.Attributes.Add("class", "card-header collapsing-show")
            divAssignmentBody.Attributes.Add("class", "collapsing-show active")
            BindAllScheduledAssignment()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnLoad_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssignment_PreRender(sender As Object, e As EventArgs) Handles gvAssignment.PreRender
        Try
            If gvAssignment.Rows.Count > 0 Then
                gvAssignment.UseAccessibleHeader = True
                gvAssignment.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssignment.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignment_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllScheduledAssignment()
        Dim iFinancialYearID As Integer = 0, iCustomerID As Integer = 0, iPartnerID As Integer = 0
        Dim iTaskID As Integer = 0, iEmpID As Integer = 0, sWorkStatusID As String = "", sComplianceTypeID As String = ""
        Dim dt As New DataTable
        Try
            iFinancialYearID = ddlAsignmentFinancialYear.SelectedValue
            If ddlAsignmentCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlAsignmentCustomerName.SelectedValue
            End If
            If ddlAsignmentPartner.SelectedIndex > 0 Then
                iPartnerID = ddlAsignmentPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            If ddlAssignmentEmployee.SelectedIndex > 0 Then
                iEmpID = ddlAssignmentEmployee.SelectedValue
            End If
            sWorkStatusID = GetSelectedWorkStatus()
            sComplianceTypeID = GetSelectedComplianceType()
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, iTaskID, iEmpID, sWorkStatusID, sComplianceTypeID, bLoginUserIsPartner, sSession.UserID)
            gvAssignment.DataBind()

            dt = objclsAuditAssignment.LoadDashboardScheduledAssignmentCounts(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, iTaskID, iEmpID, sWorkStatusID, sComplianceTypeID, bLoginUserIsPartner, sSession.UserID)
            If dt.Rows.Count > 0 Then
                lblOpenTasks.Text = 0 : lblClosedTasks.Text = 0 : lblOverDueTasks.Text = 0
                lblTodayTasks.Text = 0 ': lblMyOpenTasks.Text = 0 : lblMyOverDueTasks.Text = 0
                If IsDBNull(dt.Rows(0)("OpenTasks")) = False Then
                    lblOpenTasks.Text = dt.Rows(0)("OpenTasks")
                End If
                If IsDBNull(dt.Rows(0)("ClosedTasks")) = False Then
                    lblClosedTasks.Text = dt.Rows(0)("ClosedTasks")
                End If
                If IsDBNull(dt.Rows(0)("OverDueTasks")) = False Then
                    lblOverDueTasks.Text = dt.Rows(0)("OverDueTasks")
                End If
                If IsDBNull(dt.Rows(0)("MyOpenTasks")) = False Then
                    'lblMyOpenTasks.Text = dt.Rows(0)("MyOpenTasks")
                End If
                If IsDBNull(dt.Rows(0)("MyOverDueTasks")) = False Then
                    'lblMyOverDueTasks.Text = dt.Rows(0)("MyOverDueTasks")
                End If
                If IsDBNull(dt.Rows(0)("TodayTasks")) = False Then
                    lblTodayTasks.Text = dt.Rows(0)("TodayTasks")
                End If

                lblOpenTaskIds.Text = 0 : lblClosedTaskIds.Text = 0 : lblOverDueTaskIds.Text = 0
                lblMyOpenTaskIds.Text = 0 : lblMyOverDueTaskIds.Text = 0 : lblTodayTaskIds.Text = 0
                If IsDBNull(dt.Rows(0)("OpenTaskIds")) = False Then
                    lblOpenTaskIds.Text = dt.Rows(0)("OpenTaskIds")
                End If
                If IsDBNull(dt.Rows(0)("ClosedTaskIds")) = False Then
                    lblClosedTaskIds.Text = dt.Rows(0)("ClosedTaskIds")
                End If
                If IsDBNull(dt.Rows(0)("OverDueTaskIds")) = False Then
                    lblOverDueTaskIds.Text = dt.Rows(0)("OverDueTaskIds")
                End If
                If IsDBNull(dt.Rows(0)("MyOpenTaskIds")) = False Then
                    lblMyOpenTaskIds.Text = dt.Rows(0)("MyOpenTaskIds")
                End If
                If IsDBNull(dt.Rows(0)("MyOverDueTaskIds")) = False Then
                    lblMyOverDueTaskIds.Text = dt.Rows(0)("MyOverDueTaskIds")
                End If
                If IsDBNull(dt.Rows(0)("TodayTaskIds")) = False Then
                    lblTodayTaskIds.Text = dt.Rows(0)("TodayTaskIds")
                End If
            End If

            If bLoginUserIsPartner = True Then
                gvAssignment.Columns(10).Visible = True
            Else
                gvAssignment.Columns(10).Visible = False
            End If

            'Dim dtt As New DataTable
            'dtt = objclsAuditAssignment.LoadDashboardAllPartnerScheduledAssignmentCounts(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, iTaskID, iEmpID, sWorkStatusID, bLoginUserIsPartner, sSession.UserID)
            If dt.Rows.Count > 0 Then
                Try
                    dt.Columns.Add("usr_FullName")
                    dt.Rows(0)("usr_FullName") = sSession.UserFullName
                    dt.AcceptChanges()

                    Dim sFilePath As String = ""
                    sFilePath = Server.MapPath("../") & "Json"
                    If Directory.Exists(sFilePath) = False Then
                        Directory.CreateDirectory(sFilePath)
                    End If

                    Dim files() As String = Directory.GetFileSystemEntries(sFilePath)
                    For Each element As String In files
                        If System.IO.File.Exists(element) = True Then
                            Try
                                File.Delete(element)
                            Catch ex As Exception
                            End Try
                        End If
                    Next
                    Dim fs As FileStream = File.Create(sFilePath + "/jsonBARChart.json")
                    Dim jsonstring As String = DataTableToJSONWithStringBuilder(dt)
                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(jsonstring.ToString)
                    fs.Write(info, 0, info.Length)
                    fs.Close()
                    'strarray = New String() {dtt.Rows(0)("usr_FullName"), dtt.Rows(0)("OpenTasks"), dtt.Rows(0)("ClosedTasks"), dtt.Rows(0)("OverDueTasks")}
                Catch ex As Exception
                    Throw
                End Try
            Else
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllScheduledAssignment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvAssignment_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim chkSelectAsgSubTask As New CheckBox
        Dim lblClosed As New Label, lblWorkStatus As New Label, lblCustomerName As New Label, lblCustomerFullName As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblClosed = CType(e.Row.FindControl("lblClosed"), Label)
                lblWorkStatus = CType(e.Row.FindControl("lblWorkStatus"), Label)
                lblWorkStatus.Font.Bold = False
                If Val(lblClosed.Text) = 1 Then
                    lblWorkStatus.Font.Bold = True
                    lblWorkStatus.ForeColor = Drawing.Color.Green
                End If
                lblCustomerName = CType(e.Row.FindControl("lblCustomerName"), Label)
                lblCustomerFullName = CType(e.Row.FindControl("lblCustomerFullName"), Label)
                lblCustomerName.ToolTip = lblCustomerFullName.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignment_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssignment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAssignment.RowCommand
        Dim oAssignmentID As New Object, oFinancialYearID As New Object, oCustomerID As New Object
        Dim oPartnerID As New Object, oTaskID As New Object, oEmpID As New Object, oWorkStatusID As New Object, oComplianceID As New Object
        Dim lblAssignmentID As New Label
        Dim iFinancialYearID As Integer = 0, iCustomerID As Integer = 0, iPartnerID As Integer = 0
        Dim iTaskID As Integer = 0, iEmpID As Integer = 0, sWorkStatusID As String = "", sComplianceID As String = ""
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblAssignmentID = CType(clickedRow.FindControl("lblAssignmentID"), Label)
            If ddlAsignmentFinancialYear.SelectedIndex > 0 Then
                iFinancialYearID = ddlAsignmentFinancialYear.SelectedValue
            End If
            If ddlAsignmentCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlAsignmentCustomerName.SelectedValue
            End If
            If ddlAsignmentPartner.SelectedIndex > 0 Then
                iPartnerID = ddlAsignmentPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            If ddlAssignmentEmployee.SelectedIndex > 0 Then
                iEmpID = ddlAssignmentEmployee.SelectedValue
            End If
            sWorkStatusID = GetSelectedWorkStatus()
            If e.CommandName = "Select" Then
                oAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAssignmentID.Text)))
                oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iFinancialYearID))
                oCustomerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCustomerID))
                oPartnerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iPartnerID))
                oTaskID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iTaskID))
                oEmpID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iEmpID))
                oWorkStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sWorkStatusID))
                oComplianceID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(GetSelectedComplianceType()))
                Response.Redirect(String.Format("~/Assignment/EmpAssignmentSubTask.aspx?AssignmentID={0}&FinancialYearID={1}&CustomerID={2}&PartnerID={3}&TaskID={4}&EmpID={5}&WorkStatusID={6}&ComplianceID={7}", oAssignmentID, oFinancialYearID, oCustomerID, oPartnerID, oTaskID, oEmpID, oWorkStatusID, oComplianceID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignment_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Dim iFinancialYearID As Integer = 0, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iTaskID As Integer = 0, iEmpID As Integer = 0
        Dim sWorkStatusID As String = "", sComplianceTypeID As String = ""
        Try
            If ddlAsignmentFinancialYear.SelectedIndex > 0 Then
                iFinancialYearID = ddlAsignmentFinancialYear.SelectedValue
            End If
            If ddlAsignmentCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlAsignmentCustomerName.SelectedValue
            End If
            If ddlAsignmentPartner.SelectedIndex > 0 Then
                iPartnerID = ddlAsignmentPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            If ddlAssignmentEmployee.SelectedIndex > 0 Then
                iEmpID = ddlAssignmentEmployee.SelectedValue
            End If
            sWorkStatusID = GetSelectedWorkStatus()
            sComplianceTypeID = GetSelectedComplianceType()
            dtdetails = objclsAuditAssignment.LoadDashboardScheduledAssignment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, iTaskID, iEmpID, sWorkStatusID, sComplianceTypeID, bLoginUserIsPartner, sSession.UserID)
            If dtdetails.Rows.Count = 0 Then
                lblAssignmentDashboardValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAssignmentDashboardValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Assignment/AuditAssignment.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dashboard", "PDF", ddlAsignmentFinancialYear.SelectedValue, ddlAsignmentFinancialYear.SelectedItem.Text, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("AssignmentDashboard", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".pdf")
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
        Dim dtdetails As New DataTable
        Dim iFinancialYearID As Integer = 0, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iTaskID As Integer = 0, iEmpID As Integer = 0
        Dim sWorkStatusID As String = "", sComplianceTypeID As String = ""
        Try
            If ddlAsignmentFinancialYear.SelectedIndex > 0 Then
                iFinancialYearID = ddlAsignmentFinancialYear.SelectedValue
            End If
            If ddlAsignmentCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlAsignmentCustomerName.SelectedValue
            End If
            If ddlAsignmentPartner.SelectedIndex > 0 Then
                iPartnerID = ddlAsignmentPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            If ddlAssignmentEmployee.SelectedIndex > 0 Then
                iEmpID = ddlAssignmentEmployee.SelectedValue
            End If
            sWorkStatusID = GetSelectedWorkStatus()
            sComplianceTypeID = GetSelectedComplianceType()
            dtdetails = objclsAuditAssignment.LoadDashboardScheduledAssignment(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, iTaskID, iEmpID, sWorkStatusID, sComplianceTypeID, bLoginUserIsPartner, sSession.UserID)
            If dtdetails.Rows.Count = 0 Then
                lblAssignmentDashboardValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalAssignmentDashboardValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtdetails)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Assignment/AuditAssignment.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dashboard", "Excel", ddlAsignmentFinancialYear.SelectedValue, ddlAsignmentFinancialYear.SelectedItem.Text, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("AssignmentDashboard", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function DataTableToJSONWithStringBuilder(ByVal table As DataTable) As String
        Dim JSONString = New StringBuilder()

        If table.Rows.Count > 0 Then
            JSONString.Append("[")

            For i As Integer = 0 To table.Rows.Count - 1
                JSONString.Append("{")

                For j As Integer = 0 To table.Columns.Count - 1

                    If j < table.Columns.Count - 1 Then
                        JSONString.Append("""" & table.Columns(j).ColumnName.ToString() & """:" & """" + table.Rows(i)(j).ToString() & """,")
                    ElseIf j = table.Columns.Count - 1 Then
                        JSONString.Append("""" & table.Columns(j).ColumnName.ToString() & """:" & """" + table.Rows(i)(j).ToString() & """")
                    End If
                Next

                If i = table.Rows.Count - 1 Then
                    JSONString.Append("}")
                Else
                    JSONString.Append("},")
                End If
            Next

            JSONString.Append("]")
        End If

        Return JSONString.ToString()
    End Function
    Protected Sub btnOverDueTasks_Click(sender As Object, e As EventArgs) Handles btnOverDueTasks.Click
        Try
            lblError.Text = ""
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignmentFromId(sSession.AccessCode, sSession.AccessCodeID, lblOverDueTaskIds.Text)
            gvAssignment.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOverDueTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnTodayTasks_Click(sender As Object, e As EventArgs) Handles btnTodayTasks.Click
        Try
            lblError.Text = ""
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignmentFromId(sSession.AccessCode, sSession.AccessCodeID, lblTodayTaskIds.Text)
            gvAssignment.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnTodayTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnOpenTasks_Click(sender As Object, e As EventArgs) Handles btnOpenTasks.Click
        Try
            lblError.Text = ""
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignmentFromId(sSession.AccessCode, sSession.AccessCodeID, lblOpenTaskIds.Text)
            gvAssignment.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOpenTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnClosedTasks_Click(sender As Object, e As EventArgs) Handles btnClosedTasks.Click
        Try
            lblError.Text = ""
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignmentFromId(sSession.AccessCode, sSession.AccessCodeID, lblClosedTaskIds.Text)
            gvAssignment.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnClosedTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnMyOpenTasks_Click(sender As Object, e As EventArgs) Handles btnMyOpenTasks.Click
        Try
            lblError.Text = ""
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignmentFromId(sSession.AccessCode, sSession.AccessCodeID, lblMyOpenTaskIds.Text)
            gvAssignment.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnMyOpenTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnMyOverDueTasks_Click(sender As Object, e As EventArgs) Handles btnMyOverDueTasks.Click
        Try
            lblError.Text = ""
            gvAssignment.DataSource = objclsAuditAssignment.LoadDashboardScheduledAssignmentFromId(sSession.AccessCode, sSession.AccessCodeID, lblMyOverDueTaskIds.Text)
            gvAssignment.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnMyOverDueTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class