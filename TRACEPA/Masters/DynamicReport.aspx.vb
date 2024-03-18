Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.DataVisualization.Charting
Imports System.Drawing
Partial Class DynamicReport
    Inherits System.Web.UI.Page
    Private sFormName As String = "Master DynamicReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsDynamicReport As New clsDynamicReport
    Private sSession As AllSession
    Private objclsGeneralFunctions As New clsGeneralFunctions
    ' Dim objAccessRyt As New ClsAccessRights
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MR_DR", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Or sFormButtons = ",,,,,," Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Report,") = True Then
                '        ' imgbtnReport.Visible = True
                '    End If
                'End If
                BindModule()
                BindUsers()
                RFVModule.InitialValue = "Select Module" : RFVModule.ErrorMessage = "Select Module."
                REVFromDate.ErrorMessage = "Enter Valid From Date." : REVFromDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                REVToDate.ErrorMessage = "Enter Valid To Date." : REVToDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindModule()
        Try
            ddlModule.Items.Insert(0, "Select Module")
            ddlModule.Items.Insert(1, "User Details")
            ddlModule.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindModule" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindUsers()
        Try
            ddlUsers.DataSource = objclsDynamicReport.LoadAllUsers(sSession.AccessCode, sSession.AccessCodeID)
            ddlUsers.DataTextField = "Usr_FullName"
            ddlUsers.DataValueField = "Usr_ID"
            ddlUsers.DataBind()
            ddlUsers.Items.Insert(0, "Select Users")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindUsers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function LoadDynamicDetails() As DataTable
        Dim dt As New DataTable
        Dim UserID As Integer = 0
        Try
            If ddlUsers.SelectedIndex > 0 Then
                UserID = ddlUsers.SelectedValue
            End If

            If ddlModule.SelectedIndex = 1 Then
                dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, UserID, txtFrom.Text, txtTo.Text)
            End If
            gvGeneral.DataSource = dt
            gvGeneral.DataBind()
            Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDynamicDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
    Private Sub imgbtnSearch_Click(sender As Object, e As EventArgs) Handles imgbtnSearch.Click
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlModule.SelectedIndex = 0 Then
                lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Module','', 'info');", True)
                ddlModule.Focus()
                ddlUsers.SelectedIndex = 0 : txtFrom.Text = "" : txtTo.Text = ""
                gvGeneral.DataSource = Nothing
                gvGeneral.DataBind()
                Exit Sub
            End If
            If txtFrom.Text.Trim() = "" Then
                lblError.Text = "Enter From Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter To Date','', 'info');", True)
                txtFrom.Focus()
                Exit Sub
            End If
            If txtTo.Text.Trim() = "" Then
                lblError.Text = "Enter To Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter To Date','', 'info');", True)
                txtTo.Focus()
                Exit Sub
            End If
            If (txtFrom.Text.Trim() <> "" And txtTo.Text.Trim() <> "") Then
                Dim dFDate As Date = Date.ParseExact(txtFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dTDate As Date = Date.ParseExact(txtTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim l As Integer
                l = DateDiff(DateInterval.Day, dFDate, dTDate)
                If l < 0 Then
                    lblError.Text = "To Date(" & txtTo.Text & ") should be greater than or equal to From Date(" & txtFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('To Date(" & txtTo.Text & ") should be greater than or equal to From Date(" & txtFrom.Text & ").','', 'error');", True)
                    txtTo.Focus()
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
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim iUserID As Integer = 0
        Try
            ReportViewer1.Reset()
            If ddlUsers.SelectedIndex > 0 Then
                iUserID = ddlUsers.SelectedValue
            End If
            If ddlModule.SelectedIndex = 1 Then
                dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, iUserID, txtFrom.Text, txtTo.Text)
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
            Response.AddHeader("content-disposition", "attachment; filename=DynamicReport" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim iUserID As Integer = 0
        Try
            ReportViewer1.Reset()
            If ddlUsers.SelectedIndex > 0 Then
                iUserID = ddlUsers.SelectedValue
            End If
            If ddlModule.SelectedIndex = 1 Then
                dt = objclsDynamicReport.BindUserDetailsReports(sSession.AccessCode, sSession.AccessCodeID, iUserID, txtFrom.Text, txtTo.Text)
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
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=DynamicReport" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class