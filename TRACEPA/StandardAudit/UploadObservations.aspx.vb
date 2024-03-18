Imports BusinesLayer
Public Class UploadObservations
    Inherits System.Web.UI.Page
    Private sFormName As String = "UploadObservations"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsAttachments As New clsAttachments
    Private Shared sFile As String = ""
    Private Shared iFinancialYearID As Integer = 0
    Private Shared iAuditID As Integer = 0
    Private Shared iCheckPointID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtSampleFormat As New DataTable
        Try
            dgGeneral.Enabled = True
            sSession = Session("AllSession")
            If IsPostBack = False Then
                If Request.QueryString("FYID") IsNot Nothing Then
                    iFinancialYearID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FYID")))
                    lblFY.Text = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID)
                End If
                If Request.QueryString("AuditID") IsNot Nothing Then
                    iAuditID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                    BindScheduledDetails()
                End If
                If Request.QueryString("CheckPointID") IsNot Nothing Then
                    iCheckPointID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CheckPointID")))
                    lblAuditProcedure.Text = objclsStandardAudit.GetSelectedScheduleCheckPointDetails(sSession.AccessCode, sSession.AccessCodeID, iCheckPointID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindScheduledDetails()
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.GetSelectedScheduleDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
            If dt.Rows.Count = 1 Then
                lblAuditNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SA_AuditNo").ToString())
                lblCustomerName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("CustomerName").ToString())
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
            End If
            lblAuditProcedure.Text = objclsStandardAudit.GetSelectedScheduleCheckPointDetails(sSession.AccessCode, sSession.AccessCodeID, iCheckPointID)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/StandardAudit/ConductAudit.aspx?AuditID={0}", HttpUtility.UrlDecode(Request.QueryString("AuditID"))))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dtDetails As New DataTable
        Dim objDBL As New DBHelper
        Try
            lblError.Text = ""
            dgGeneral.Visible = False
            imgbtnSave.Visible = False
            If ddlSheetName.SelectedIndex > 0 Then
                dtDetails = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                dtDetails.AsEnumerable().Where(Function(row) row.ItemArray.All(Function(field) field Is Nothing Or field Is DBNull.Value Or field.Equals(""))).ToList().ForEach(Sub(row) row.Delete())
                dtDetails.AcceptChanges()

                If IsNothing(dtDetails) Then
                    ddlSheetName.SelectedIndex = 0
                    lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf dtDetails.Rows.Count = 0 Then
                    lblError.Text = "No Data." : lblExcelValidationMsg.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                dgGeneral.Visible = True
                dgGeneral.DataSource = dtDetails
                dgGeneral.DataBind()

                Dim iGetLastRow = dtDetails.Columns.Count - 1
                If Trim(dtDetails.Columns(iGetLastRow).ColumnName).ToLower() <> "observation" Then
                    lblError.Text = "Invalid Excel sheet selected(No Observation column in excel sheet)." : lblExcelValidationMsg.Text = "Invalid Excel sheet selected(No Observation column in excel sheet)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlSheetName.SelectedIndex = 0
                imgbtnSave.Visible = False
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
                imgbtnSave.Visible = False
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = True
            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
                imgbtnSave.Visible = True
                sExt = IO.Path.GetExtension(FULoad.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoad.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If
                    FULoad.PostedFile.SaveAs(sFile)
                    ddlSheetName.Items.Clear()
                    dt = ExcelSheetNames(sFile)
                    ddlSheetName.DataSource = dt
                    ddlSheetName.DataTextField = "Name"
                    ddlSheetName.DataValueField = "ID"
                    ddlSheetName.DataBind()
                    ddlSheetName.Items.Insert(0, "Select Sheet")
                Else
                    lblError.Text = "Select Excel file only." : lblExcelValidationMsg.Text = "Select Excel file only."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblExcelValidationMsg.Text = "Select Excel file."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub dgGeneral_PreRender(sender As Object, e As EventArgs) Handles dgGeneral.PreRender
        Try
            If dgGeneral.Rows.Count > 0 Then
                dgGeneral.UseAccessibleHeader = True
                dgGeneral.HeaderRow.TableSection = TableRowSection.TableHeader
                dgGeneral.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            If dgGeneral.Rows.Count = 0 Then
                lblError.Text = "No Data."
                Exit Sub
            Else
                objclsStandardAudit.DeletecheduleCheckPointObservations(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointID)
            End If
            For i = 0 To dgGeneral.Rows.Count - 1
                Dim sObservation As String = dgGeneral.Rows(i).Cells(dgGeneral.Rows(0).Cells.Count - 1).Text.Trim()
                If IsDBNull(sObservation) = False Then
                    If sObservation <> "&nbsp;" Then
                        If sObservation <> "" Then
                            objclsStandardAudit.SaveScheduleCheckPointObservations(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointID, objclsGRACeGeneral.ReplaceSafeSQL(sObservation), sSession.UserID, sSession.IPAddress)
                        End If
                    End If
                End If
            Next
            Dim iSSAtchID As Integer = objclsStandardAudit.GetSamplingAuditAttachmentID(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointID)
            iSSAtchID = objclsAttachments.SaveSamplingAttachments(sSession.AccessCode, sSession.AccessCodeID, sFile, sSession.UserID, iSSAtchID, "Uploaded with Observations")
            lblExcelValidationMsg.Text = "Successfully Observations uploaded." : lblError.Text = "Successfully Observations uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
