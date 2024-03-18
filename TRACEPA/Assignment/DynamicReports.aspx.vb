Imports System
Imports System.Data
Imports System.Drawing
Imports System.IO
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class DynamicReports
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_DynamicReports"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsCompanyDetails As New clsCompanyDetails
    Public Shared strarray As Array = {(0), (1)}
    Private Shared iYearID As Integer
    Private sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnLoad.ImageUrl = "~/Images/Load24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = False Then
                    lblMsg.Text = sSession.UserFullName & " does not have permission to view this page."
                    divEmp.Visible = True : divPartner.Visible = False : imgbtnLoad.Visible = False : imgbtnReport.Visible = False
                    Exit Sub
                End If

                divEmp.Visible = False : divPartner.Visible = True : imgbtnReport.Visible = True
                BindTypes() : BindInvoiceTypes() : BindBillingEntity()
                BindCustomers() : BindPartner() : BindEmployees() : BindTasks() : BindWorkStatus()
                LoadFinalcialYear(sSession.AccessCode) : BindRevenueMonths() : BindMonthlyPerformanceMonths()
                RFVType.InitialValue = "0" : RFVType.ErrorMessage = "Select Type."

                divResourceAvailability.Visible = False : gvResourceAvailability.Visible = False
                RFVFromDate1.ValidationGroup = False : REVFromDate1.ValidationGroup = False
                RFVToDate1.ValidationGroup = False : REVToDate1.ValidationGroup = False

                divResourceStatus.Visible = False : gvResourceStatus.Visible = False
                RFVFromDate2.ValidationGroup = False : REVFromDate2.ValidationGroup = False
                RFVToDate2.ValidationGroup = False : REVToDate2.ValidationGroup = False

                divInvoiceReports.Visible = False : gvInvoiceReports.Visible = False
                RFVFromDate3.ValidationGroup = False : REVFromDate3.ValidationGroup = False
                RFVToDate3.ValidationGroup = False : REVToDate3.ValidationGroup = False

                divAssignments.Visible = False : gvAssignment.Visible = False
                RFVFromDate4.ValidationGroup = False : REVFromDate4.ValidationGroup = False
                RFVToDate4.ValidationGroup = False : REVToDate4.ValidationGroup = False

                divRevenue.Visible = False : gvRevenue.Visible = False
                divMonthMonthlyPerformance.Visible = False : gvMonthlyPerformance.Visible = False : chartdiv.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindTypes()
        Try
            ddlType.Items.Add(New ListItem("Select Type", "0"))
            ddlType.Items.Add(New ListItem("Resource Availability", "1"))
            ddlType.Items.Add(New ListItem("Resource Status", "2"))
            ddlType.Items.Add(New ListItem("Invoice Reports", "3"))
            ddlType.Items.Add(New ListItem("Assignments", "4"))
            ddlType.Items.Add(New ListItem("Employee Monthly Performance", "5"))
            ddlType.Items.Add(New ListItem("Revenue", "6"))
            ddlType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTypes" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindInvoiceTypes()
        Try
            ddlTaxType3.Items.Add(New ListItem("Select Tax Type", "0"))
            ddlTaxType3.Items.Add(New ListItem("Proforma Invoice", "1"))
            ddlTaxType3.Items.Add(New ListItem("Tax Invoice", "2"))
            ddlTaxType3.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTypes" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindBillingEntity()
        Try
            ddlEntity3.DataSource = objclsCompanyDetails.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlEntity3.DataTextField = "Company_Name"
            ddlEntity3.DataValueField = "Company_ID"
            ddlEntity3.DataBind()
            ddlEntity3.Items.Insert(0, "Select Billing Entity")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingCompanyName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomers()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomer3.DataSource = dt
            ddlCustomer3.DataTextField = "CUST_Name"
            ddlCustomer3.DataValueField = "CUST_ID"
            ddlCustomer3.DataBind()
            ddlCustomer3.Items.Insert(0, "Select Customer")

            ddlCustomer4.DataSource = dt
            ddlCustomer4.DataTextField = "CUST_Name"
            ddlCustomer4.DataValueField = "CUST_ID"
            ddlCustomer4.DataBind()
            ddlCustomer4.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPartner()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlPartner1.DataSource = dt
            ddlPartner1.DataTextField = "USr_FullName"
            ddlPartner1.DataValueField = "USR_ID"
            ddlPartner1.DataBind()
            ddlPartner1.Items.Insert(0, "Select Partner")

            ddlPartner2.DataSource = dt
            ddlPartner2.DataTextField = "USr_FullName"
            ddlPartner2.DataValueField = "USR_ID"
            ddlPartner2.DataBind()
            ddlPartner2.Items.Insert(0, "Select Partner")

            ddlPartner3.DataSource = dt
            ddlPartner3.DataTextField = "USr_FullName"
            ddlPartner3.DataValueField = "USR_ID"
            ddlPartner3.DataBind()
            ddlPartner3.Items.Insert(0, "Select Partner")

            ddlPartner4.DataSource = dt
            ddlPartner4.DataTextField = "USr_FullName"
            ddlPartner4.DataValueField = "USR_ID"
            ddlPartner4.DataBind()
            ddlPartner4.Items.Insert(0, "Select Partner")
            'If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
            '    ddlPartner.SelectedValue = sSession.UserID
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartner" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindTasks()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlTask3.DataSource = dt
            ddlTask3.DataTextField = "Name"
            ddlTask3.DataValueField = "PKID"
            ddlTask3.DataBind()
            ddlTask3.Items.Insert(0, "Select Assignment/Task")

            ddlTask4.DataSource = dt
            ddlTask4.DataTextField = "Name"
            ddlTask4.DataValueField = "PKID"
            ddlTask4.DataBind()
            ddlTask4.Items.Insert(0, "Select Assignment/Task")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTasks" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindEmployees()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)
            ddlEmployee1.DataSource = dt
            ddlEmployee1.DataTextField = "FullName"
            ddlEmployee1.DataValueField = "Usr_ID"
            ddlEmployee1.DataBind()
            ddlEmployee1.Items.Insert(0, "Select Employee")

            ddlEmployee3.DataSource = dt
            ddlEmployee3.DataTextField = "FullName"
            ddlEmployee3.DataValueField = "Usr_ID"
            ddlEmployee3.DataBind()
            ddlEmployee3.Items.Insert(0, "Select Employee")

            ddlEmployee4.DataSource = dt
            ddlEmployee4.DataTextField = "FullName"
            ddlEmployee4.DataValueField = "Usr_ID"
            ddlEmployee4.DataBind()
            ddlEmployee4.Items.Insert(0, "Select Employee")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindEmployees" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindWorkStatus()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
            ddlWorkStatus2.DataSource = dt
            ddlWorkStatus2.DataTextField = "Name"
            ddlWorkStatus2.DataValueField = "PKID"
            ddlWorkStatus2.DataBind()
            ddlWorkStatus2.Items.Insert(0, "Select Work Status")

            ddlWorkstatus3.DataSource = dt
            ddlWorkstatus3.DataTextField = "Name"
            ddlWorkstatus3.DataValueField = "PKID"
            ddlWorkstatus3.DataBind()
            ddlWorkstatus3.Items.Insert(0, "Select Work Status")

            ddlWorkstatus4.DataSource = dt
            ddlWorkstatus4.DataTextField = "Name"
            ddlWorkstatus4.DataValueField = "PKID"
            ddlWorkstatus4.DataBind()
            ddlWorkstatus4.Items.Insert(0, "Select Work Status")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvResourceAvailability_PreRender(sender As Object, e As EventArgs) Handles gvResourceAvailability.PreRender
        Try
            If gvResourceAvailability.Rows.Count > 0 Then
                gvResourceAvailability.UseAccessibleHeader = True
                gvResourceAvailability.HeaderRow.TableSection = TableRowSection.TableHeader
                gvResourceAvailability.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResourceAvailability_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function BindResourceAvailability() As DataTable
        Dim dFromDate As Date, dToDate As Date
        Dim dt As New DataTable
        Dim iPartnerID As Integer = 0, iEmployeeID As Integer = 0
        Try
            dFromDate = Date.ParseExact(txtFromDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dToDate = Date.ParseExact(txtToDate1.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If ddlPartner1.SelectedIndex > 0 Then
                iPartnerID = ddlPartner1.SelectedValue
            End If
            If ddlEmployee1.SelectedIndex > 0 Then
                iEmployeeID = ddlEmployee1.SelectedValue
            End If
            dt = objclsAuditAssignment.LoadResourceAvailability(sSession.AccessCode, sSession.AccessCodeID, dFromDate, dToDate, iPartnerID, iEmployeeID)
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSelectedDateScheduledAssignmentEmpWise" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            Throw
        End Try
    End Function
    Private Sub gvResourceStatus_PreRender(sender As Object, e As EventArgs) Handles gvResourceStatus.PreRender
        Try
            If gvResourceStatus.Rows.Count > 0 Then
                gvResourceStatus.UseAccessibleHeader = True
                gvResourceStatus.HeaderRow.TableSection = TableRowSection.TableHeader
                gvResourceStatus.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResourceStatus_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvResourceStatus_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex Mod 2 = 0 Then
                    e.Row.BackColor = ColorTranslator.FromHtml("#FCFCFC")
                Else
                    e.Row.BackColor = ColorTranslator.FromHtml("#F5F5F5")
                End If
                For i = 2 To e.Row.Cells.Count - 1
                    e.Row.Cells(i).Attributes.Add("align", "right")
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResourceStatus_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function BindResourceStatus() As DataTable
        Dim dFromDate As Date, dToDate As Date
        Dim dt As New DataTable
        Dim iPartnerID As Integer = 0, iWorkStatusID As Integer = 0
        Try
            dFromDate = Date.ParseExact(txtFromDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dToDate = Date.ParseExact(txtToDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If ddlPartner2.SelectedIndex > 0 Then
                iPartnerID = ddlPartner2.SelectedValue
            End If
            If ddlWorkStatus2.SelectedIndex > 0 Then
                iWorkStatusID = ddlWorkStatus2.SelectedValue
            End If
            dt = objclsAuditAssignment.LoadResourceStatus(sSession.AccessCode, sSession.AccessCodeID, dFromDate, dToDate, iPartnerID, iWorkStatusID)
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSelectedDateScheduledAssignmentEmpWise" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub gvInvoiceReports_PreRender(sender As Object, e As EventArgs) Handles gvInvoiceReports.PreRender
        Try
            If gvInvoiceReports.Rows.Count > 0 Then
                gvInvoiceReports.UseAccessibleHeader = True
                gvInvoiceReports.HeaderRow.TableSection = TableRowSection.TableHeader
                gvInvoiceReports.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvInvoiceReports_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvInvoiceReports_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvInvoiceReports.RowCommand
        Dim lblInvoiceID As New Label, lnkInvoiceNo As New LinkButton
        Try
            For j = 0 To gvInvoiceReports.Rows.Count - 1
                lnkInvoiceNo = gvInvoiceReports.Rows(j).FindControl("lnkInvoiceNo")
                lnkInvoiceNo.Attributes.Add("style", "text-decoration: none;")
            Next
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblInvoiceID = CType(clickedRow.FindControl("lblInvoiceID"), Label)
            lnkInvoiceNo = CType(clickedRow.FindControl("lnkInvoiceNo"), LinkButton)
            If e.CommandName = "Select" Then
                GenerateInvoice(Val(lblInvoiceID.Text))
                lnkInvoiceNo.Attributes.Add("style", "text-decoration: underline;")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvInvoiceReports_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function BindInvoiceReports() As DataTable
        Dim dFromDate As Date, dToDate As Date
        Dim dt As New DataTable
        Dim iTaxTypeID As Integer = 0, iEntityID As Integer = 0, iCustomerID As Integer = 0, iPartnerID As Integer = 0
        Dim iEmployeeID As Integer = 0, iTaskID As Integer = 0, iWorkstatusID As Integer = 0
        Try
            dFromDate = Date.ParseExact(txtFromDate3.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dToDate = Date.ParseExact(txtToDate3.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).AddDays(1)
            If ddlTaxType3.SelectedIndex > 0 Then
                iTaxTypeID = ddlTaxType3.SelectedValue
            End If
            If ddlEntity3.SelectedIndex > 0 Then
                iEntityID = ddlEntity3.SelectedValue
            End If
            If ddlCustomer3.SelectedIndex > 0 Then
                iCustomerID = ddlCustomer3.SelectedValue
            End If
            If ddlPartner3.SelectedIndex > 0 Then
                iPartnerID = ddlPartner3.SelectedValue
            End If
            If ddlEmployee3.SelectedIndex > 0 Then
                iEmployeeID = ddlEmployee3.SelectedValue
            End If
            If ddlTask3.SelectedIndex > 0 Then
                iTaskID = ddlTask3.SelectedValue
            End If
            If ddlWorkstatus3.SelectedIndex > 0 Then
                iWorkstatusID = ddlWorkstatus3.SelectedValue
            End If
            dt = objclsAuditAssignment.LoadInvoiceReports(sSession.AccessCode, sSession.AccessCodeID, dFromDate, dToDate, txtInvoiceNo3.Text.Trim(), iTaxTypeID, iEntityID, iCustomerID, iPartnerID, iEmployeeID, iTaskID, iWorkstatusID)
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSelectedDateScheduledAssignmentEmpWise" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            Throw
        End Try
    End Function
    Private Function BindAllScheduledAssignment()
        Dim dFromDate As Date, dToDate As Date
        Dim dt As New DataTable
        Dim iCustomerID As Integer = 0, iPartnerID As Integer = 0
        Dim iEmployeeID As Integer = 0, iTaskID As Integer = 0, iWorkstatusID As Integer = 0
        Dim sWorkStatusID As String = ""
        Try
            dFromDate = Date.ParseExact(txtFromDate4.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dToDate = Date.ParseExact(txtToDate4.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).AddDays(1)
            If ddlCustomer4.SelectedIndex > 0 Then
                iCustomerID = ddlCustomer4.SelectedValue
            End If
            If ddlPartner4.SelectedIndex > 0 Then
                iPartnerID = ddlPartner4.SelectedValue
            End If
            If ddlEmployee4.SelectedIndex > 0 Then
                iEmployeeID = ddlEmployee4.SelectedValue
            End If
            If ddlTask4.SelectedIndex > 0 Then
                iTaskID = ddlTask4.SelectedValue
            End If
            If ddlWorkstatus4.SelectedIndex > 0 Then
                sWorkStatusID = ddlWorkstatus4.SelectedValue
            End If
            dt = objclsAuditAssignment.LoadScheduledAssignmentDynamicReport(sSession.AccessCode, sSession.AccessCodeID, dFromDate, dToDate, iCustomerID, iPartnerID, iTaskID, iEmployeeID, sWorkStatusID, "", True, sSession.UserID)
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllScheduledAssignment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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

    Protected Sub ddlType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlType.SelectedIndexChanged
        Try
            lblError.Text = ""
            divResourceAvailability.Visible = False : gvResourceAvailability.Visible = False
            RFVFromDate1.ValidationGroup = False : REVFromDate1.ValidationGroup = False
            RFVToDate1.ValidationGroup = False : REVToDate1.ValidationGroup = False
            txtFromDate1.Text = "" : txtToDate1.Text = ""

            divResourceStatus.Visible = False : gvResourceStatus.Visible = False
            RFVFromDate2.ValidationGroup = False : REVFromDate2.ValidationGroup = False
            RFVToDate2.ValidationGroup = False : REVToDate2.ValidationGroup = False
            ddlPartner2.SelectedIndex = 0 : ddlWorkStatus2.SelectedIndex = 0
            txtFromDate2.Text = "" : txtToDate2.Text = ""

            divInvoiceReports.Visible = False : gvInvoiceReports.Visible = False
            RFVFromDate3.ValidationGroup = False : REVFromDate3.ValidationGroup = False
            RFVToDate3.ValidationGroup = False : REVToDate3.ValidationGroup = False
            txtFromDate3.Text = "" : txtToDate3.Text = "" : txtInvoiceNo3.Text = "" : ddlTaxType3.SelectedIndex = 0
            ddlEntity3.SelectedIndex = 0 : ddlCustomer3.SelectedIndex = 0 : ddlPartner3.SelectedIndex = 0
            ddlEmployee3.SelectedIndex = 0 : ddlTask3.SelectedIndex = 0 : ddlWorkstatus3.SelectedIndex = 0

            divAssignments.Visible = False : gvAssignment.Visible = False
            RFVFromDate4.ValidationGroup = False : REVFromDate4.ValidationGroup = False
            RFVToDate4.ValidationGroup = False : REVToDate4.ValidationGroup = False
            txtFromDate4.Text = "" : txtToDate4.Text = ""
            ddlCustomer4.SelectedIndex = 0 : ddlPartner4.SelectedIndex = 0
            ddlEmployee4.SelectedIndex = 0 : ddlTask4.SelectedIndex = 0 : ddlWorkstatus4.SelectedIndex = 0

            divRevenue.Visible = False : gvRevenue.Visible = False
            divMonthMonthlyPerformance.Visible = False : gvMonthlyPerformance.Visible = False : chartdiv.Visible = False

            If ddlType.SelectedIndex = 1 Then
                divResourceAvailability.Visible = True
                RFVFromDate1.ValidationGroup = True : RFVFromDate1.ValidationGroup = "Validate"
                REVFromDate1.ValidationGroup = True : REVFromDate1.ValidationGroup = "Validate"
                RFVToDate1.ValidationGroup = True : RFVToDate1.ValidationGroup = "Validate"
                REVToDate1.ValidationGroup = True : REVToDate1.ValidationGroup = "Validate"

                RFVFromDate1.ControlToValidate = "txtFromDate1" : RFVFromDate1.ErrorMessage = "Enter From Date."
                REVFromDate1.ErrorMessage = "Enter valid Date." : REVFromDate1.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVToDate1.ControlToValidate = "txtToDate1" : RFVToDate1.ErrorMessage = "Enter To Date."
                REVToDate1.ErrorMessage = "Enter valid Date." : REVToDate1.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            ElseIf ddlType.SelectedIndex = 2 Then
                divResourceStatus.Visible = True
                RFVFromDate2.ValidationGroup = True : RFVFromDate2.ValidationGroup = "Validate"
                REVFromDate2.ValidationGroup = True : REVFromDate2.ValidationGroup = "Validate"
                RFVToDate2.ValidationGroup = True : RFVToDate2.ValidationGroup = "Validate"
                REVToDate2.ValidationGroup = True : REVToDate2.ValidationGroup = "Validate"

                RFVFromDate2.ControlToValidate = "txtFromDate2" : RFVFromDate2.ErrorMessage = "Enter From Date."
                REVFromDate2.ErrorMessage = "Enter valid Date." : REVFromDate2.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVToDate2.ControlToValidate = "txtToDate2" : RFVToDate2.ErrorMessage = "Enter To Date."
                REVToDate2.ErrorMessage = "Enter valid Date." : REVToDate2.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            ElseIf ddlType.SelectedIndex = 3 Then
                divInvoiceReports.Visible = True
                RFVFromDate3.ValidationGroup = True : RFVFromDate3.ValidationGroup = "Validate"
                REVFromDate3.ValidationGroup = True : REVFromDate3.ValidationGroup = "Validate"
                RFVToDate3.ValidationGroup = True : RFVToDate3.ValidationGroup = "Validate"
                REVToDate3.ValidationGroup = True : REVToDate3.ValidationGroup = "Validate"

                RFVFromDate3.ControlToValidate = "txtFromDate3" : RFVFromDate3.ErrorMessage = "Enter From Date."
                REVFromDate3.ErrorMessage = "Enter valid Date." : REVFromDate3.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVToDate3.ControlToValidate = "txtToDate3" : RFVToDate3.ErrorMessage = "Enter To Date."
                REVToDate3.ErrorMessage = "Enter valid Date." : REVToDate3.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            ElseIf ddlType.SelectedIndex = 4 Then
                divAssignments.Visible = True
                RFVFromDate4.ValidationGroup = True : RFVFromDate4.ValidationGroup = "Validate"
                REVFromDate4.ValidationGroup = True : REVFromDate4.ValidationGroup = "Validate"
                RFVToDate4.ValidationGroup = True : RFVToDate4.ValidationGroup = "Validate"
                REVToDate4.ValidationGroup = True : REVToDate4.ValidationGroup = "Validate"

                RFVFromDate4.ControlToValidate = "txtFromDate4" : RFVFromDate4.ErrorMessage = "Enter From Date."
                REVFromDate4.ErrorMessage = "Enter valid Date." : REVFromDate4.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVToDate4.ControlToValidate = "txtToDate4" : RFVToDate4.ErrorMessage = "Enter To Date."
                REVToDate4.ErrorMessage = "Enter valid Date." : REVToDate4.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            ElseIf ddlType.SelectedIndex = 5 Then
                divMonthMonthlyPerformance.Visible = True
            ElseIf ddlType.SelectedIndex = 6 Then
                divRevenue.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlType_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnLoad_Click(sender As Object, e As EventArgs) Handles imgbtnLoad.Click
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            gvResourceAvailability.Visible = False : gvResourceStatus.Visible = False : gvInvoiceReports.Visible = False : gvAssignment.Visible = False : gvMonthlyPerformance.Visible = False : chartdiv.Visible = False : gvRevenue.Visible = False
            If ddlType.SelectedIndex = 1 Then
                dt = BindResourceAvailability()
                gvResourceAvailability.Visible = True
                gvResourceAvailability.DataSource = dt
                gvResourceAvailability.DataBind()
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Resource Availability", sSession.YearID, txtFromDate1.Text, 0, txtToDate1.Text, sSession.IPAddress)
            ElseIf ddlType.SelectedIndex = 2 Then
                Dim dFromDate As DateTime = Date.ParseExact(txtFromDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dToDate As DateTime = Date.ParseExact(txtToDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim d1 As Integer
                d1 = DateDiff(DateInterval.Day, dFromDate, dToDate)
                If d1 < 0 Then
                    lblError.Text = "To Date should be greater than or equal to From Date."
                    lblDynamicReportsValidationMsg.Text = "To Date should be greater than or equal to From Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalDynamicReportsValidation').modal('show');", True)
                    txtToDate2.Focus()
                    Exit Try
                End If

                dt = BindResourceStatus()
                gvResourceStatus.Visible = True
                gvResourceStatus.DataSource = dt
                gvResourceStatus.DataBind()
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Resource Status", sSession.YearID, txtFromDate2.Text, 0, txtToDate2.Text, sSession.IPAddress)
            ElseIf ddlType.SelectedIndex = 3 Then
                dt = BindInvoiceReports()
                gvInvoiceReports.Visible = True
                gvInvoiceReports.DataSource = dt
                gvInvoiceReports.DataBind()
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Invoice Report", sSession.YearID, txtFromDate3.Text, 0, txtToDate3.Text, sSession.IPAddress)
            ElseIf ddlType.SelectedIndex = 4 Then
                dt = BindAllScheduledAssignment()
                gvAssignment.Visible = True
                gvAssignment.DataSource = dt
                gvAssignment.DataBind()
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Assignments", sSession.YearID, txtFromDate3.Text, 0, txtToDate3.Text, sSession.IPAddress)
            ElseIf ddlType.SelectedIndex = 5 Then
                dt = objclsAuditAssignment.LoadMonthlyPerformanceAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFYMonthlyPerformance.SelectedValue, 0, ddlMonthlyPerformance.SelectedIndex + 1, True)
                gvMonthlyPerformance.Visible = True
                gvMonthlyPerformance.DataSource = dt
                gvMonthlyPerformance.DataBind()
                chartdiv.Visible = True
                Dim sFilePath As String = ""
                Try
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
                    strarray = New String() {ddlMonthlyPerformance.SelectedItem.Text}
                Catch ex As Exception
                    Throw
                End Try
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Monthly Performance", sSession.YearID, ddlFYMonthlyPerformance.SelectedItem.Text, 0, ddlMonthlyPerformance.SelectedIndex + 1, sSession.IPAddress)
            ElseIf ddlType.SelectedIndex = 6 Then
                dt = objclsAuditAssignment.LoadRevenueAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFYRevenue.SelectedValue, 0, ddlMonthRevenue.SelectedIndex + 1, True)
                gvRevenue.Visible = True
                gvRevenue.DataSource = dt
                gvRevenue.DataBind()
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Revenue", sSession.YearID, ddlFYRevenue.SelectedItem.Text, 0, ddlMonthRevenue.SelectedIndex + 1, sSession.IPAddress)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnLoad_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
    '    Try
    '        lblDynamicReportsValidationMsg.Text = "Too many columns! unable to generate. Download Excel File." : lblError.Text = "Too many columns! unable to generate. Download Excel File."
    '        lblError.Text = "Too many columns! unable to generate. Download Excel File." : lblError.Text = "Too many columns! unable to generate. Download Excel File."
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDynamicReportsValidation').modal('show');", True)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dtdetails As New DataTable
        Dim sFileName As String = "DynamicReport"
        Try
            lblError.Text = ""
            If ddlType.SelectedIndex = 1 Then
                sFileName = "ResourceAvailability"
                dtdetails = BindResourceAvailability()
            ElseIf ddlType.SelectedIndex = 2 Then
                sFileName = "ResourceStatus"
                Dim dFromDate As DateTime = Date.ParseExact(txtFromDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dToDate As DateTime = Date.ParseExact(txtToDate2.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim d1 As Integer
                d1 = DateDiff(DateInterval.Day, dFromDate, dToDate)
                If d1 < 0 Then
                    lblError.Text = "To Date should be greater than or equal to From Date."
                    lblDynamicReportsValidationMsg.Text = "To Date should be greater than or equal to From Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalDynamicReportsValidation').modal('show');", True)
                    txtToDate2.Focus()
                    Exit Try
                End If
                dtdetails = BindResourceStatus()
                If dtdetails.Rows.Count = 0 Then
                    lblDynamicReportsValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDynamicReportsValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If dtdetails.Rows.Count = 0 And ddlType.SelectedIndex = 3 Then
            ElseIf dtdetails.Rows.Count = 0 And ddlType.SelectedIndex <> 3 Then
                lblDynamicReportsValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDynamicReportsValidation').modal('show');", True)
                Exit Sub
            Else
                ExportoExcel(dtdetails, sFileName)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", sFileName + " Downloaded Excel", sSession.YearID, "", 0, "", sSession.IPAddress)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ExportoExcel(ByVal dt As DataTable, ByVal sFileName As String)
        Dim excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0, rowIndex As Integer = 0
        Dim sPath As String, strFileNameFullPath As String, strFileNamePath As String, sExcelFileName As String
        Dim i As Integer
        Try
            If dt.Rows.Count > 0 Then
                sPath = Server.MapPath("../") & "ExcelUploads\Excel.xlsx"
                wBook = excel.Workbooks.Add(sPath)
                wSheet = wBook.ActiveSheet()
                For i = 0 To dt.Columns.Count - 1
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dt.Columns(i).ColumnName
                    excel.Cells(1, colIndex).Font.Bold = True
                Next
                For Each dr In dt.Rows
                    rowIndex = rowIndex + 1
                    colIndex = 0
                    For i = 0 To dt.Columns.Count - 1
                        colIndex = colIndex + 1
                        excel.Cells(rowIndex + 1, colIndex) = dr(dt.Columns(i).ColumnName)
                    Next
                Next
                wSheet.Columns.AutoFit()
                strFileNamePath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sExcelFileName = sFileName + ".xlsx"
                If strFileNamePath.EndsWith("\") = False Then
                    strFileNameFullPath = strFileNamePath & "\" & sExcelFileName
                Else
                    strFileNameFullPath = strFileNamePath & sExcelFileName
                End If

                Dim blnFileOpen As Boolean = False
                Try
                    If System.IO.File.Exists(strFileNameFullPath) Then
                        System.IO.File.Delete(strFileNameFullPath)
                    End If
                    Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileNameFullPath)
                    fileTemp.Close()
                Catch ex As Exception
                    blnFileOpen = False
                End Try
                If System.IO.File.Exists(strFileNameFullPath) Then
                    System.IO.File.Delete(strFileNameFullPath)
                End If
                wBook.SaveAs(strFileNameFullPath)
                wBook.Close()
                excel.Quit()
                excel = Nothing
                DownloadFile(strFileNameFullPath)
            Else
                lblDynamicReportsValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub DownloadFile(ByVal pstrFileNameAndPath As String)
        Dim Extn As String, pstrContentType As String, sFileName As String, sFullName As String
        Dim myFileInfo As IO.FileInfo
        Dim StartPos As Long = 0, FileSize As Long, EndPos As Long
        Try
            If IO.File.Exists(pstrFileNameAndPath) Then
                myFileInfo = New IO.FileInfo(pstrFileNameAndPath)
                FileSize = myFileInfo.Length
                EndPos = FileSize
                Web.HttpContext.Current.Response.Clear()
                Web.HttpContext.Current.Response.ClearHeaders()
                Web.HttpContext.Current.Response.ClearContent()
                Extn = objclsGRACeGeneral.GetFileExt(pstrFileNameAndPath)
                sFileName = System.IO.Path.GetFileNameWithoutExtension(pstrFileNameAndPath)
                sFullName = sFileName & "." & Extn
                pstrContentType = "application/x-msexcel"
                Dim Range As String = Web.HttpContext.Current.Request.Headers("Range")
                If Not ((Range Is Nothing) Or (Range = "")) Then
                    Dim StartEnd As Array = Range.Substring(Range.LastIndexOf("=") + 1).Split("-")
                    If Not StartEnd(0) = "" Then
                        StartPos = CType(StartEnd(0), Long)
                    End If
                    If StartEnd.GetUpperBound(0) >= 1 And Not StartEnd(1) = "" Then
                        EndPos = CType(StartEnd(1), Long)
                    Else
                        EndPos = FileSize - StartPos
                    End If
                    If EndPos > FileSize Then
                        EndPos = FileSize - StartPos
                    End If
                    System.Web.HttpContext.Current.Response.StatusCode = 206
                    System.Web.HttpContext.Current.Response.StatusDescription = "Partial Content"
                    System.Web.HttpContext.Current.Response.AppendHeader("Content-Range", "bytes " & StartPos & "-" & EndPos & "/" & FileSize)
                End If
                System.Web.HttpContext.Current.Response.ContentType = pstrContentType
                System.Web.HttpContext.Current.Response.AppendHeader("Content-disposition", "attachment; filename=" & sFullName & "")
                System.Web.HttpContext.Current.Response.WriteFile(Server.HtmlEncode(pstrFileNameAndPath), StartPos, EndPos)
                System.Web.HttpContext.Current.Response.Flush()
                System.Web.HttpContext.Current.Response.End()
            End If
        Catch ex As Exception
        End Try
    End Sub
    Protected Sub GenerateInvoice(ByVal iPKID As Integer)
        Dim mimeType As String = Nothing
        Dim dtInvoice As New DataTable, dtCompany As New DataTable, dtCustomer As New DataTable, dtSubTasks As New DataTable, dtTaxType1 As New DataTable, dtTaxType2 As New DataTable
        Dim dTotal As Decimal, dTaxType1 As Decimal, dTaxType2 As Decimal, dFinalTotal As Decimal, sReportNo As String, sReportName As String
        Try
            dtInvoice = objclsAuditAssignment.GetInvoiceDetailsForReport(sSession.AccessCode, sSession.AccessCodeID, iPKID)
            dTotal = objclsAuditAssignment.GetInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID)
            Dim sLogoName As String = "" : Dim imageBase64DataLogoString As String = ""
            Dim objclsCompanyDetails As New clsCompanyDetails
            sLogoName = objclsCompanyDetails.getCompanyImageName(sSession.AccessCode, dtInvoice.Rows(0)("AAI_BillingEntity_ID"), "A")
            If sLogoName <> "" And sLogoName <> "." Then
                Dim imageDataURL As String = Server.MapPath("~/Images/" + sLogoName)
                If System.IO.File.Exists(imageDataURL) = True Then
                    Dim logoInBytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                    imageBase64DataLogoString = Convert.ToBase64String(logoInBytes)
                End If
            End If
            Dim imageBase64DataSignatureString As String = ""
            Dim iSignatureID As Integer = objclsAuditAssignment.GetUserSignatureID(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0).Item("AAI_AuthorizedSignatory"))
            If iSignatureID > 0 Then
                Dim objclsEProfile As New clsEProfile
                Dim objclsAttachments As New clsAttachments
                Dim iSignatureDocID As Integer = objclsEProfile.GetPhotoDocID(sSession.AccessCode, sSession.AccessCodeID, iSignatureID)
                Dim sPaths As String = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                Dim sDestFilePath As String = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iSignatureID, iSignatureDocID)
                If System.IO.Directory.Exists(sPaths) = True And System.IO.File.Exists(sDestFilePath) = True Then
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sDestFilePath)
                    imageBase64DataSignatureString = Convert.ToBase64String(bytes)
                End If
            End If
            dtCompany = objclsAuditAssignment.LoadCompanyLogoSignatureDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_BillingEntity_ID"), imageBase64DataLogoString, imageBase64DataSignatureString)
            dtCustomer = objclsAuditAssignment.LoadCustomerDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_Cust_ID"))
            dtSubTasks = objclsAuditAssignment.LoadSubTaskDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iPKID)
            If dtCompany.Rows.Count = 0 Then
                lblDynamicReportsValidationMsg.Text = "Please update Company details in Master module." : lblError.Text = "Please update Company details in Master module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDynamicReportsValidation').modal('show');", True)
                Exit Sub
            End If
            If dtCustomer.Rows.Count = 0 Then
                lblDynamicReportsValidationMsg.Text = "Please update Customer details in Master module." : lblError.Text = "Please update Customer details in Master module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalDynamicReportsValidation').modal('show');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            Dim rdCompany As New ReportDataSource("DataSet1", dtCompany)
            Dim rdCustomer As New ReportDataSource("DataSet2", dtCustomer)
            Dim rdSubTasks As New ReportDataSource("DataSet3", dtSubTasks)
            ReportViewer1.LocalReport.DataSources.Add(rdCompany)
            ReportViewer1.LocalReport.DataSources.Add(rdCustomer)
            ReportViewer1.LocalReport.DataSources.Add(rdSubTasks)

            sReportNo = dtInvoice.Rows(0)("AAI_InvoiceNo")
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Assignment/TaxInvoice.rdlc")

            Dim ReportTitle As ReportParameter()
            Dim InvoiceNo As ReportParameter()

            If dtInvoice.Rows(0)("AAI_InvoiceTypeID") = 1 Then
                ReportTitle = New ReportParameter() {New ReportParameter("ReportTitle", "Proforma Invoice")}
                InvoiceNo = New ReportParameter() {New ReportParameter("InvoiceNo", "PI No: " & sReportNo)}
                sReportName = "ProformaInvoice"
            Else
                ReportTitle = New ReportParameter() {New ReportParameter("ReportTitle", "Tax Invoice")}
                InvoiceNo = New ReportParameter() {New ReportParameter("InvoiceNo", "Invoice No: " & sReportNo)}
                sReportName = "TaxInvoice"
            End If

            Dim TaxType1 As ReportParameter()
            Dim TaxType1Details As ReportParameter()
            If dtInvoice.Rows(0)("AAI_TaxType1") > 0 Then
                dTaxType1 = objclsAuditAssignment.GetTaxInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID, dtInvoice.Rows(0)("AAI_TaxType1Percentage"))
                TaxType1 = New ReportParameter() {New ReportParameter("TaxType1", "₹" & String.Format("{0:0.00}", dTaxType1))}
                dtTaxType1 = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_TaxType1"), "TM")
                TaxType1Details = New ReportParameter() {New ReportParameter("TaxType1Details", (dtTaxType1.Rows(0)("CMM_Desc") & "@" & dtTaxType1.Rows(0)("CMM_Rate") & "%").ToString())}
            Else
                TaxType1 = New ReportParameter() {New ReportParameter("TaxType1", " ")}
                TaxType1Details = New ReportParameter() {New ReportParameter("TaxType1Details", " ")}
            End If

            Dim TaxType2 As ReportParameter()
            Dim TaxType2Details As ReportParameter()
            If dtInvoice.Rows(0)("AAI_TaxType2") > 0 Then
                dTaxType2 = objclsAuditAssignment.GetTaxInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID, dtInvoice.Rows(0)("AAI_TaxType2Percentage"))
                TaxType2 = New ReportParameter() {New ReportParameter("TaxType2", "₹" & String.Format("{0:0.00}", dTaxType2))}
                dtTaxType2 = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_TaxType2"), "TM")
                TaxType2Details = New ReportParameter() {New ReportParameter("TaxType2Details", (dtTaxType2.Rows(0)("CMM_Desc") & "@" & dtTaxType2.Rows(0)("CMM_Rate") & "%").ToString())}
            Else
                TaxType2 = New ReportParameter() {New ReportParameter("TaxType2", " ")}
                TaxType2Details = New ReportParameter() {New ReportParameter("TaxType2Details", " ")}
            End If

            dFinalTotal = objclsAuditAssignment.GetFinalInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID, dtInvoice.Rows(0)("AAI_TaxType1Percentage"), dtInvoice.Rows(0)("AAI_TaxType2Percentage"))
            Dim FinalTotal As ReportParameter() = New ReportParameter() {New ReportParameter("FinalTotal", "₹" & String.Format("{0:0.00}", dFinalTotal))}
            Dim AmountInWords As ReportParameter() = New ReportParameter() {New ReportParameter("AmountInWords", objclsAuditAssignment.NumberToWord(String.Format("{0:0.00}", dFinalTotal)) & " Only")}
            Dim Notes As ReportParameter()
            If dtInvoice.Rows(0)("AAI_Notes").ToString() = "" Then
                Notes = New ReportParameter() {New ReportParameter("Notes", " ")}
            Else
                Notes = New ReportParameter() {New ReportParameter("Notes", dtInvoice.Rows(0)("AAI_Notes").ToString())}
            End If

            ReportViewer1.LocalReport.SetParameters(ReportTitle)
            ReportViewer1.LocalReport.SetParameters(InvoiceNo)
            ReportViewer1.LocalReport.SetParameters(TaxType1)
            ReportViewer1.LocalReport.SetParameters(TaxType2)
            ReportViewer1.LocalReport.SetParameters(FinalTotal)
            ReportViewer1.LocalReport.SetParameters(AmountInWords)
            ReportViewer1.LocalReport.SetParameters(TaxType1Details)
            ReportViewer1.LocalReport.SetParameters(TaxType2Details)
            ReportViewer1.LocalReport.SetParameters(Notes)

            Dim CurrentDate As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentDate", "Date: " & objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode))}
            Dim SubTotal As ReportParameter() = New ReportParameter() {New ReportParameter("SubTotal", "₹" & String.Format("{0:0.00}", dTotal))}
            Dim Total As ReportParameter() = New ReportParameter() {New ReportParameter("Total", "₹" & String.Format("{0:0.00}", dTotal))}
            ReportViewer1.LocalReport.SetParameters(CurrentDate)
            ReportViewer1.LocalReport.SetParameters(SubTotal)
            ReportViewer1.LocalReport.SetParameters(Total)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")

            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dynamic Reports", "Downloaded Invoice", sSession.YearID, txtFromDate3.Text, iPKID, txtToDate3.Text, sSession.IPAddress)
            HttpContext.Current.Response.Buffer = True
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Response.ContentType = mimeType
            Dim sFileName As String = Regex.Replace(sReportName, " \ s", "")
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & "_" & iPKID & ".pdf")
            HttpContext.Current.Response.BinaryWrite(RptViewer)
            HttpContext.Current.Response.Flush()
            'HttpContext.Current.Response.End()
            HttpContext.Current.Response.SuppressContent = True
            HttpContext.Current.ApplicationInstance.CompleteRequest()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateInvoice" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Try
            ddlFYRevenue.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFYRevenue.DataTextField = "YMS_ID"
            ddlFYRevenue.DataValueField = "YMS_YearID"
            ddlFYRevenue.DataBind()

            ddlFYMonthlyPerformance.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFYMonthlyPerformance.DataTextField = "YMS_ID"
            ddlFYMonthlyPerformance.DataValueField = "YMS_YearID"
            ddlFYMonthlyPerformance.DataBind()

            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFYRevenue.SelectedValue = iYearID
                        ddlFYMonthlyPerformance.SelectedValue = iYearID
                    Else
                        ddlFYRevenue.SelectedIndex = 0
                        ddlFYMonthlyPerformance.SelectedIndex = 0
                    End If
                Else
                    ddlFYRevenue.SelectedValue = sSession.YearID
                    ddlFYRevenue.SelectedValue = sSession.YearID
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindRevenueMonths()
        Dim dDate As DateTime
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim iCurrentMonth As Integer = 3
        Try
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If ddlFYRevenue.SelectedItem.Text.Substring(0, 4) = dDate.Year And iYearID = ddlFYRevenue.SelectedValue Then
                iCurrentMonth = dDate.Month
            End If

            dt.Columns.Add("ID")
            dt.Columns.Add("Name")
            If iCurrentMonth = 4 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 5 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 6 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 7 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 8 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 9 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 10 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 11 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 12 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 1 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "January-" + ddlFYRevenue.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 2 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "January-" + ddlFYRevenue.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "2" : dr("Name") = "February-" + ddlFYRevenue.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 3 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYRevenue.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "January-" + ddlFYRevenue.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "2" : dr("Name") = "February-" + ddlFYRevenue.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "3" : dr("Name") = "March-" + ddlFYRevenue.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If

            ddlMonthRevenue.DataSource = dt
            ddlMonthRevenue.DataTextField = "Name"
            ddlMonthRevenue.DataValueField = "ID"
            ddlMonthRevenue.DataBind()
            ddlMonthRevenue.SelectedValue = dDate.Month
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub

    Private Sub BindMonthlyPerformanceMonths()
        Dim dDate As DateTime
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim iCurrentMonth As Integer = 3
        Try
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) = dDate.Year And iYearID = ddlFYMonthlyPerformance.SelectedValue Then
                iCurrentMonth = dDate.Month
            End If

            dt.Columns.Add("ID")
            dt.Columns.Add("Name")
            If iCurrentMonth = 4 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 5 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 6 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 7 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 8 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 9 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 10 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 11 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 12 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 1 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "January-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 2 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "January-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "2" : dr("Name") = "February-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 3 Then
                dr = dt.NewRow() : dr("ID") = "4" : dr("Name") = "April-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "5" : dr("Name") = "May-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "6" : dr("Name") = "June-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "7" : dr("Name") = "July-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "8" : dr("Name") = "August-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "9" : dr("Name") = "September-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "1" : dr("Name") = "January-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "2" : dr("Name") = "February-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "3" : dr("Name") = "March-" + ddlFYMonthlyPerformance.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If

            ddlMonthlyPerformance.DataSource = dt
            ddlMonthlyPerformance.DataTextField = "Name"
            ddlMonthlyPerformance.DataValueField = "ID"
            ddlMonthlyPerformance.DataBind()
            ddlMonthlyPerformance.SelectedValue = dDate.Month
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvMonthlyPerformance_PreRender(sender As Object, e As EventArgs) Handles gvMonthlyPerformance.PreRender
        Dim dt As New DataTable
        Try
            If gvMonthlyPerformance.Rows.Count > 0 Then
                gvMonthlyPerformance.UseAccessibleHeader = True
                gvMonthlyPerformance.HeaderRow.TableSection = TableRowSection.TableHeader
                gvMonthlyPerformance.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvMonthlyPerformance_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvRevenue_PreRender(sender As Object, e As EventArgs) Handles gvRevenue.PreRender
        Dim dt As New DataTable
        Try
            If gvRevenue.Rows.Count > 0 Then
                gvRevenue.UseAccessibleHeader = True
                gvRevenue.HeaderRow.TableSection = TableRowSection.TableHeader
                gvRevenue.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvRevenue_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Protected Sub ddlFYMonthlyPerformance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFYMonthlyPerformance.SelectedIndexChanged
        Try
            BindMonthlyPerformanceMonths()
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFYMonthlyPerformance_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFYRevenue_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFYRevenue.SelectedIndexChanged
        Try
            BindRevenueMonths()
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFYRevenue_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class