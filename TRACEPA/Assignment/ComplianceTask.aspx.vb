Imports System
Imports System.Data
Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class ComplianceTask
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_ComplianceTask"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAdminMaster As New clsAdminMaster
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
                BindCustomers() : BindPartner()
                BindComplianceTaskDetailsForGraph()
                rboOverdue_CheckedChanged(sender, e)
                RFVFromDate.ControlToValidate = "txtFromDate" : RFVFromDate.ErrorMessage = "Enter From Date."
                REVFromDate.ErrorMessage = "Enter valid From Date." : REVFromDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVToDate.ControlToValidate = "txtToDate" : RFVToDate.ErrorMessage = "Enter To Date."
                REVToDate.ErrorMessage = "Enter valid To Date." : REVToDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomers()
        Try
            ddlCustomer.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomer.DataTextField = "CUST_Name"
            ddlCustomer.DataValueField = "CUST_ID"
            ddlCustomer.DataBind()
            ddlCustomer.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPartner()
        Try
            ddlPartner.DataSource = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlPartner.DataTextField = "USr_FullName"
            ddlPartner.DataValueField = "USR_ID"
            ddlPartner.DataBind()
            ddlPartner.Items.Insert(0, "Select Partner")
            'If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
            '    ddlPartner.SelectedValue = sSession.UserID
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartner" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindComplianceTaskDetailsForGraph()
        Dim iCustomerID As Integer = 0, iPartnerID As Integer = 0, iOnTimeComplianceCount As Integer = 0, iDelayedComplianceCount As Integer = 0, iNonComplianceCount As Integer = 0, iTotalCount As Integer = 0
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                iCustomerID = ddlCustomer.SelectedValue
            End If
            If ddlPartner.SelectedIndex > 0 Then
                iPartnerID = ddlPartner.SelectedValue
            End If
            Dim dt As DataTable = objclsAuditAssignment.GetComplianceMonthlyTaskDetailsForGraph(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, sSession.YearName, iCustomerID, iPartnerID)
            For i = 0 To dt.Rows.Count - 1
                ComplianceStackChart.Series("On-Time Compliance").Points.Add(New DataPoint(i, Integer.Parse(dt.Rows(i)("On-TimeCompliance"))))
                ComplianceStackChart.Series("Delayed Compliance").Points.Add(New DataPoint(i, Integer.Parse(dt.Rows(i)("DelayedCompliance"))))
                ComplianceStackChart.Series("Non Compliance").Points.Add(New DataPoint(i, Integer.Parse(dt.Rows(i)("NonCompliance"))))
                ComplianceStackChart.Series(0).Points(i).AxisLabel = dt.Rows(i)("Month")

                iOnTimeComplianceCount = iOnTimeComplianceCount + Integer.Parse(dt.Rows(i)("On-TimeCompliance"))
                iDelayedComplianceCount = iDelayedComplianceCount + Integer.Parse(dt.Rows(i)("DelayedCompliance"))
                iNonComplianceCount = iNonComplianceCount + Integer.Parse(dt.Rows(i)("NonCompliance"))
            Next

            iTotalCount = iOnTimeComplianceCount + iDelayedComplianceCount + iNonComplianceCount

            Dim yValues As Double() = {(iOnTimeComplianceCount / iTotalCount) * 100, (iDelayedComplianceCount / iTotalCount) * 100, (iNonComplianceCount / iTotalCount) * 100}
            Dim xValues As String() = {"On-Time Compliance", "Delayed Compliance", "Non Compliance"}
            CompliancePieChart.Series("Default").Points.DataBindXY(xValues, yValues)
            CompliancePieChart.Series("Default").Points(0).Color = ColorTranslator.FromHtml("#4BC0C0")
            CompliancePieChart.Series("Default").Points(1).Color = ColorTranslator.FromHtml("#87C7F2")
            CompliancePieChart.Series("Default").Points(2).Color = ColorTranslator.FromHtml("#FF6D80")
            CompliancePieChart.Series("Default").ChartType = SeriesChartType.Pie
            CompliancePieChart.Series("Default")("PieLabelStyle") = "Disabled"
            CompliancePieChart.Legends(0).Enabled = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnLoad_Click(sender As Object, e As EventArgs) Handles imgbtnLoad.Click
        Try
            BindComplianceTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnLoad_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDetails_PreRender(sender As Object, e As EventArgs) Handles gvDetails.PreRender
        Try
            If gvDetails.Rows.Count > 0 Then
                gvDetails.UseAccessibleHeader = True
                gvDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDetails_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindComplianceTaskDetails()
        Dim dFromDate As Date, dToDate As Date
        Dim dtTab As New DataTable, dt As New DataTable
        Dim iCustomerID As Integer, iType As Integer, iPartnerID As Integer = 0, iWIPId As Integer = 0
        Try
            If ddlCustomer.SelectedIndex > 0 Then
                iCustomerID = ddlCustomer.SelectedValue
            End If
            If ddlPartner.SelectedIndex > 0 Then
                iPartnerID = ddlPartner.SelectedValue
            End If

            If rboOverdue.Checked = True Or rboUpcoming.Checked = True Or rboWIP.Checked = True Then
                iType = 0
            End If
            If rboCompletedTasks.Checked = True Then
                iType = 1
            End If
            If rboWIP.Checked = True Then
                iWIPId = objclsAuditAssignment.GetWIPIdFromMaster(sSession.AccessCode, sSession.AccessCodeID)
            End If

            dFromDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dToDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim d1 As Integer
            d1 = DateDiff(DateInterval.Day, dFromDate, dToDate)
            If d1 < 0 Then
                lblError.Text = "To Date should be greater than or equal to From Date." : lblComplianceTaskValidationMsg.Text = "To Date should be greater than or equal to From Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAAValidation').modal('show');", True)
                txtFromDate.Focus()
                Exit Try
            End If

            dtTab = objclsAuditAssignment.LoadComplianceTaskDetails(sSession.AccessCode, sSession.AccessCodeID, iCustomerID, iPartnerID, iType, iWIPId, dFromDate, dToDate)
            gvDetails.DataSource = dtTab
            gvDetails.DataBind()

            dt = objclsAuditAssignment.LoadComplianceTaskCounts(sSession.AccessCode, sSession.AccessCodeID, iCustomerID, iPartnerID, iType, iWIPId, dFromDate, dToDate)
            If dt.Rows.Count > 0 Then
                lblAllTasks.Text = dt.Rows(0)("AllTasks")
                lblYetToStartTasks.Text = dt.Rows(0)("YetToStartTasks")
                lblInProgressTasks.Text = dt.Rows(0)("InProgressTasks")
                lblCompletedTasks.Text = dt.Rows(0)("CompletedTasks")
                lblOverDueTasks.Text = dt.Rows(0)("OverDueTasks")
            End If

            BindComplianceTaskDetailsForGraph()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSelectedDateScheduledAssignmentEmpWise" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Protected Sub ddlCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomer.SelectedIndexChanged
    '    Try
    '        lblError.Text = ""
    '        BindComplianceTaskDetails()
    '        BindComplianceTaskDetailsForGraph()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomer_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Protected Sub rboOverdue_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboOverdue.CheckedChanged
        Try
            lblError.Text = ""
            Dim dDate As Date = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dDate.Month > 3 Then
                txtFromDate.Text = "01/04/" & dDate.Year.ToString()
            Else
                txtFromDate.Text = "01/04/" & dDate.Year - 1
            End If
            txtToDate.Text = dDate.Date.AddDays(-1).Day.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.AddDays(-1).Month.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.AddDays(-1).Year.ToString()
            BindComplianceTaskDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboOverdue_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboUpcoming_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboUpcoming.CheckedChanged
        Try
            lblError.Text = ""
            Dim dDate As Date = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            txtFromDate.Text = dDate.Date.Day.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.Month.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.Year.ToString()
            txtToDate.Text = dDate.Date.AddMonths(1).Day.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.AddMonths(1).Month.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.AddMonths(1).Year.ToString()
            BindComplianceTaskDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboUpcoming_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboCompletedTasks_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboCompletedTasks.CheckedChanged
        Try
            lblError.Text = ""
            Dim dDate As Date = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dDate.Month > 3 Then
                txtFromDate.Text = "01/04/" & dDate.Year.ToString()
            Else
                txtFromDate.Text = "01/04/" & dDate.Year - 1
            End If
            txtToDate.Text = dDate.Date.Day.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.Month.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.Year.ToString()
            BindComplianceTaskDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboCompletedTasks_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboWIP_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboWIP.CheckedChanged
        Try
            lblError.Text = ""
            Dim dDate As Date = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dDate.Month > 3 Then
                txtFromDate.Text = "01/04/" & dDate.Year.ToString()
            Else
                txtFromDate.Text = "01/04/" & dDate.Year - 1
            End If
            txtToDate.Text = dDate.Date.Day.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.Month.ToString().PadLeft(2, "0"c) + "/" + dDate.Date.Year.ToString()
            BindComplianceTaskDetails()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboWIP_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvDetails.RowDataBound
        Try
            If e.Row.RowType = ListItemType.Header Then
                Dim lblHExpectedCompletionDate As New Label
                lblHExpectedCompletionDate = e.Row.FindControl("lblHExpectedCompletionDate")
                If rboOverdue.Checked = True Or rboUpcoming.Checked = True Or rboWIP.Checked = True Then
                    lblHExpectedCompletionDate.Text = "Completion Date"
                End If
                If rboCompletedTasks.Checked = True Then
                    lblHExpectedCompletionDate.Text = "Completed Date"
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDetails_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtdetails As New DataTable
        Try
            'dtdetails = objclsAuditAssignment.LoadSelectedDateScheduledAssignmentEmpWise(sSession.AccessCode, sSession.AccessCodeID, dDate)
            If dtdetails.Rows.Count = 0 Then
                lblComplianceTaskValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalComplianceTaskValidation').modal('show');", True)
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
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Compliance Task", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("ComplianceTask", "\s", "")
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
        Try
            'dtdetails = objclsAuditAssignment.LoadSelectedDateScheduledAssignmentEmpWise(sSession.AccessCode, sSession.AccessCodeID, dDate)
            If dtdetails.Rows.Count = 0 Then
                lblComplianceTaskValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalComplianceTaskValidation').modal('show');", True)
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
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Compliance Task", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Dim sFileName As String = Regex.Replace("ComplianceTask", "\s", "")
            Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class