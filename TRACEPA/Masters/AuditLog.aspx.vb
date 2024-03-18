Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class AuditLog
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_AuditLog"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAuditGeneral As New clsAuditGeneral
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditLog As New clsAuditLog
    Private sSession As AllSession
    Private objclsDynamicReport As New clsDynamicReport
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
                BindMaster() : LoadUsers()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Public Sub LoadUsers()
        Try
            ddlUsers.DataSource = objclsAllActiveMaster.LoadActiveEmployees(sSession.AccessCode, sSession.AccessCodeID)
            ddlUsers.DataTextField = "FullName"
            ddlUsers.DataValueField = "Usr_Id"
            ddlUsers.DataBind()
            ddlUsers.Items.Insert(0, "Select Users")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadUsers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindMaster()
        Try
            ddlMaster.Items.Add(New ListItem("Select Operations", "0"))

            'Master
            ddlMaster.Items.Add(New ListItem("Master - TRACe Settings", "TRACe Settings"))
            ddlMaster.Items.Add(New ListItem("Master - Organisation Structure", "Organisation Structure"))
            ddlMaster.Items.Add(New ListItem("Master - Holiday Master", "Year Master"))
            ddlMaster.Items.Add(New ListItem("Master - Company Details", "Company Details"))
            ddlMaster.Items.Add(New ListItem("Master - Employee Master", "Employee Master"))
            ddlMaster.Items.Add(New ListItem("Master - Customer Master", "Customer Master"))
            ddlMaster.Items.Add(New ListItem("Master - Customer User Master", "Customer User Master"))
            ddlMaster.Items.Add(New ListItem("Master - General Master", "General Master"))
            ddlMaster.Items.Add(New ListItem("Master - Excel Upload", "Excel Upload"))
            ddlMaster.Items.Add(New ListItem("Master - Assignment Master", "Assignment Master"))
            'ddlMaster.Items.Add(New ListItem("Master - Audit Log", "Audit Log"))
            'ddlMaster.Items.Add(New ListItem("Master - Dynamic Report", "Dynamic Report"))
            ddlMaster.Items.Add(New ListItem("Master - Report Content Master", "Report Content Master"))
            ddlMaster.Items.Add(New ListItem("Master - Report Template Master", "Report Template Master"))

            'Fixed Asset
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Location Setup", "Location Setup"))
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Asset Classification", "Asset Classification"))
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Asset Creation", "Asset Creation"))
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Asset Addition/Revalution", "Asset Addition/Revalution"))
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Asset Deletion", "Asset Deletion"))
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Excel Upload", "Excel Upload"))
            ddlMaster.Items.Add(New ListItem("Fixed Asset - Depreciation Computation", "Depreciation Computation"))
            'ddlMaster.Items.Add(New ListItem("Fixed Asset - Report", "Report"))
            'ddlMaster.Items.Add(New ListItem("Fixed Asset - Dynamic Report", "Dynamic Report"))
            'ddlMaster.Items.Add(New ListItem("Fixed Asset - Assetwise Report", "Assetwise Report"))

            'DigitalFiling
            ddlMaster.Items.Add(New ListItem("DigitalOffice - Cabinet", "Cabinet"))
            ddlMaster.Items.Add(New ListItem("DigitalOffice - SubCabinet", "SubCabinet"))
            ddlMaster.Items.Add(New ListItem("DigitalOffice - Folders", "Folders"))
            ddlMaster.Items.Add(New ListItem("DigitalOffice - Descriptor", "Descriptor"))
            ddlMaster.Items.Add(New ListItem("DigitalOffice - Document Type", "Document Type"))
            ddlMaster.Items.Add(New ListItem("DigitalOffice - File Upload", "File Upload"))

            'Assignments
            ddlMaster.Items.Add(New ListItem("Assignments - Dashboard", "Dashboard"))
            ddlMaster.Items.Add(New ListItem("Assignments - Schedule", "Schedule"))
            ddlMaster.Items.Add(New ListItem("Assignments - Dynamic Reports", "Dynamic Reports"))
            ddlMaster.Items.Add(New ListItem("Assignments - Invoice", "Invoice"))
            ddlMaster.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
            'Throw
        End Try
    End Sub
    Private Sub ddlMaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMaster.SelectedIndexChanged
        'Try
        '    If ddlMaster.SelectedIndex > 0 Then
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, 0, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    End If
        '    ddlUsers.SelectedIndex = 0
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMaster_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        'End Try
    End Sub
    Private Sub ddlUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUsers.SelectedIndexChanged
        'Try
        '    If ddlMaster.SelectedIndex > 0 And ddlUsers.SelectedIndex > 0 Then
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, ddlUsers.SelectedValue, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    Else
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, 0, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlUsers_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        'End Try
    End Sub
    Private Sub txtFromDate_TextChanged(sender As Object, e As EventArgs) Handles txtFromDate.TextChanged
        'Try
        '    If ddlMaster.SelectedIndex > 0 And ddlUsers.SelectedIndex > 0 Then
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, ddlUsers.SelectedValue, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    ElseIf ddlUsers.SelectedIndex = 0 Then
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, 0, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtFromDate_TextChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        'End Try
    End Sub
    Private Sub txtToDate_TextChanged(sender As Object, e As EventArgs) Handles txtToDate.TextChanged
        'Try
        '    If ddlMaster.SelectedIndex > 0 And ddlUsers.SelectedIndex > 0 Then
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, ddlUsers.SelectedValue, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    ElseIf ddlUsers.SelectedIndex = 0 Then
        '        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, 0, txtFromDate.Text.Trim, txtToDate.Text.Trim)
        '        gvAuditLog.DataBind()
        '    End If
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtToDate_TextChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        'End Try
    End Sub
    Private Sub gvAuditLog_PreRender(sender As Object, e As EventArgs) Handles gvAuditLog.PreRender
        Dim dt As New DataTable
        Try
            If gvAuditLog.Rows.Count > 0 Then
                gvAuditLog.UseAccessibleHeader = True
                gvAuditLog.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAuditLog.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAuditLog_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub rboMaster_CheckedChanged(sender As Object, e As EventArgs) Handles rboMaster.CheckedChanged
        Try
            lblError.Text = ""
            gvAuditLog.Visible = True
            gvGeneral.Visible = False
            If rboMaster.Checked = True Then
                pnlMaster.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboMaster_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub rboModule_CheckedChanged(sender As Object, e As EventArgs) Handles rboModule.CheckedChanged
        Try
            lblError.Text = ""
            gvAuditLog.Visible = False
            gvGeneral.Visible = True
            If rboModule.Checked = True Then
                pnlMaster.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboModule_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Private Sub gvGeneral_PreRender(sender As Object, e As EventArgs) Handles gvGeneral.PreRender
        Dim dt As New DataTable
        Try
            If gvGeneral.Rows.Count > 0 Then
                gvGeneral.UseAccessibleHeader = True
                gvGeneral.HeaderRow.TableSection = TableRowSection.TableHeader
                gvGeneral.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneral_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSearch_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSearch.Click
        Dim dt As New DataTable
        Try
            lblError.Text = ""

            If rboModule.Checked = True Then
                If rboModule.Checked = False Then
                    lblError.Text = "Select Module."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Module','', 'info');", True)
                    rboModule.Focus()
                    ddlUsers.SelectedIndex = 0 : txtFromDate.Text = "" : txtToDate.Text = ""
                    gvGeneral.DataSource = Nothing
                    gvGeneral.DataBind()
                    Exit Sub
                End If
                If txtFromDate.Text.Trim() = "" Then
                    lblError.Text = "Enter From Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter To Date','', 'info');", True)
                    txtFromDate.Focus()
                    Exit Sub
                End If
                If txtToDate.Text.Trim() = "" Then
                    lblError.Text = "Enter To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter To Date','', 'info');", True)
                    txtToDate.Focus()
                    Exit Sub
                End If
                If (txtFromDate.Text.Trim() <> "" And txtToDate.Text.Trim() <> "") Then
                    Dim dFDate As Date = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim dTDate As Date = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim l As Integer
                    l = DateDiff(DateInterval.Day, dFDate, dTDate)
                    If l < 0 Then
                        lblError.Text = "To Date(" & txtToDate.Text & ") should be greater than or equal to From Date(" & txtFromDate.Text & ")."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('To Date(" & txtToDate.Text & ") should be greater than or equal to From Date(" & txtFromDate.Text & ").','', 'error');", True)
                        txtToDate.Focus()
                        Exit Sub
                    End If

                    dt = LoadDynamicDetails()
                    If dt.Rows.Count = 0 Then
                        lblError.Text = "No data to display."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No data to display','', 'info');", True)
                    End If
                Else
                    lblError.Text = "Please Select From Date and To Date"
                End If
            End If

            If rboMaster.Checked = True Then

                If ddlMaster.SelectedIndex = 0 Then
                    lblError.Text = "Select Operations."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Operations.','', 'info');", True)
                    ddlMaster.Focus()
                    ddlUsers.SelectedIndex = 0 : txtFromDate.Text = "" : txtToDate.Text = ""
                    gvGeneral.DataSource = Nothing
                    gvGeneral.DataBind()
                    Exit Sub
                End If

                If txtFromDate.Text.Trim() = "" Then
                    lblError.Text = "Enter From Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter To Date','', 'info');", True)
                    txtFromDate.Focus()
                    Exit Sub
                End If
                If txtToDate.Text.Trim() = "" Then
                    lblError.Text = "Enter To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter To Date','', 'info');", True)
                    txtToDate.Focus()
                    Exit Sub
                End If
                If (txtFromDate.Text.Trim() <> "" And txtToDate.Text.Trim() <> "") Then
                    Dim dFDate As Date = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim dTDate As Date = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Dim l As Integer
                    l = DateDiff(DateInterval.Day, dFDate, dTDate)
                    If l < 0 Then
                        lblError.Text = "To Date(" & txtToDate.Text & ") should be greater than or equal to From Date(" & txtFromDate.Text & ")."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('To Date(" & txtToDate.Text & ") should be greater than or equal to From Date(" & txtFromDate.Text & ").','', 'error');", True)
                        txtToDate.Focus()
                        Exit Sub
                    End If

                    If ddlMaster.SelectedIndex > 0 And ddlUsers.SelectedIndex > 0 Then
                        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, ddlUsers.SelectedValue, txtFromDate.Text.Trim, txtToDate.Text.Trim)
                        gvAuditLog.DataBind()
                    ElseIf ddlUsers.SelectedIndex = 0 Then
                        gvAuditLog.DataSource = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, 0, txtFromDate.Text.Trim, txtToDate.Text.Trim)
                        gvAuditLog.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function LoadDynamicDetails() As DataTable
        Dim dt As New DataTable
        Dim UserID As Integer = 0
        Try
            If ddlUsers.SelectedIndex > 0 Then
                UserID = ddlUsers.SelectedValue
            End If

            If rboModule.Checked = True Then
                dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, UserID, txtFromDate.Text, txtToDate.Text)
            End If
            gvGeneral.DataSource = dt
            gvGeneral.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDynamicDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim iUserID As Integer = 0
        Try
            ReportViewer1.Reset()
            If ddlUsers.SelectedIndex > 0 Then
                iUserID = ddlUsers.SelectedValue
            End If
            If rboModule.Checked = True Then
                dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, iUserID, txtFromDate.Text, txtToDate.Text)
            End If
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/DynamicReport.rdlc")

            Dim TDate As ReportParameter() = New ReportParameter() {New ReportParameter("TDate", DateTime.Now.ToShortDateString)}
            ReportViewer1.LocalReport.SetParameters(TDate)

            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=LogReport" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
    '    Dim mimeType As String = Nothing
    '    Dim dt As New DataTable
    '    Dim iUserID As Integer = 0
    '    Try
    '        ReportViewer1.Reset()
    '        If ddlUsers.SelectedIndex > 0 Then
    '            iUserID = ddlUsers.SelectedValue
    '        End If
    '        If rboModule.Checked = True Then
    '            dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, iUserID, txtFromDate.Text, txtToDate.Text)
    '        End If
    '        If dt.Rows.Count = 0 Then
    '            lblError.Text = "No Data."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
    '            Exit Sub
    '        End If
    '        Dim rds As New ReportDataSource("DataSet1", dt)
    '        ReportViewer1.LocalReport.DataSources.Add(rds)
    '        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/DynamicReport.rdlc")
    '        Dim TDate As ReportParameter() = New ReportParameter() {New ReportParameter("TDate", DateTime.Now.ToShortDateString)}
    '        ReportViewer1.LocalReport.SetParameters(TDate)
    '        Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
    '        Response.Buffer = True
    '        Response.Clear()
    '        Response.ContentType = mimeType
    '        Response.AddHeader("content-disposition", "attachment; filename=DynamicReport" + ".pdf")
    '        Response.BinaryWrite(pdfViewer)
    '        Response.Flush()
    '        Response.End()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim iUserID As Integer = 0
        Try
            ReportViewer1.Reset()
            If ddlUsers.SelectedIndex > 0 Then
                iUserID = ddlUsers.SelectedValue
            End If
            If rboModule.Checked = True Then
                dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, iUserID, txtFromDate.Text, txtToDate.Text)
            ElseIf rboMaster.Checked = True Then
                dt = objclsAuditLog.LoadAuditLogDetails(sSession.AccessCode, sSession.AccessCodeID, ddlMaster.SelectedValue, ddlMaster.SelectedItem.Text, ddlUsers.SelectedValue, txtFromDate.Text.Trim, txtToDate.Text.Trim)
            End If
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If

            If rboModule.Checked = True Then
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/DynamicReport.rdlc")
                Dim TDate As ReportParameter() = New ReportParameter() {New ReportParameter("TDate", DateTime.Now.ToShortDateString)}
                ReportViewer1.LocalReport.SetParameters(TDate)
                Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=LogReport" + ".pdf")
                Response.BinaryWrite(pdfViewer)
                Response.Flush()
                Response.End()
            ElseIf rboMaster.Checked = True Then
                Dim rds As New ReportDataSource("DataSet1", dt)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/DynamicSubModule.rdlc")
                ' Dim TDate As ReportParameter() = New ReportParameter() {New ReportParameter("TDate", DateTime.Now.ToShortDateString)}
                'ReportViewer1.LocalReport.SetParameters(TDate)
                Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=DynamicSubModule" + ".pdf")
                Response.BinaryWrite(pdfViewer)
                Response.Flush()
                Response.End()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
