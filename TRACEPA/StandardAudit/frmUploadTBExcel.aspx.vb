Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Public Class frmUploadTBExcel
    Inherits System.Web.UI.Page
    Private sFormName As String = "frmUploadTBExcel"
    Private Shared sSession As AllSession
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsUTBE As New clsUploadTBExcel
    Private objclsOpeningBalance As New clsOpeningBalance
    Private objclsStandardAudit As New clsStandardAudit
    Private objclsExcelUpload As New clsExcelUpload
    Private Shared sExcelSave As String
    Private Shared sFile As String = ""
    Private Shared TotalOpeningCredit As Decimal = 0
    Private Shared TotalOpeningDebit As Double = 0
    Private Shared TOtaltrCredit As Double = 0
    Private Shared TOtaltrDebit As Double = 0
    Private Shared TOtalClosingCredit As Double = 0
    Private Shared TOtalClosingDebit As Double = 0

    Private Shared iFinancialYearID As Integer = 0
    Private Shared iCustID As Integer = 0
    Private Shared iAuditID As Integer = 0
    Private Shared iAuditTypeID As Integer = 0
    Private Shared iCheckPointID As Integer = 0
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        ImgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        'imgbtnSave.Visible = False
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim dtSampleFormat As New DataTable
        Try
            dgGeneral.Enabled = True
            ddlSheetName.Enabled = False
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'LoadFinalcialYear()
                dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 32)
                dgSampleFormat.DataSource = dtSampleFormat
                dgSampleFormat.DataBind()
                If Request.QueryString("FYID") IsNot Nothing Then
                    iFinancialYearID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FYID")))
                    lblFY.Text = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID)
                    'ddlFinancialYear_SelectedIndexChanged(sender, e)
                End If
                If Request.QueryString("AuditID") IsNot Nothing Then
                    iAuditID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                    BindScheduledDetails()
                End If
                If Request.QueryString("CheckPointID") IsNot Nothing Then
                    iCheckPointID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CheckPointID")))
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
                'iFinancialYearID = dt.Rows(0)("SA_YearID")
                'ddlFinancialYear.SelectedValue = dt.Rows(0)("SA_YearID")
                'lblFY.Text = dt.Rows(0)("FY")
                lblAuditNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("SA_AuditNo").ToString())
                iCustID = dt.Rows(0)("SA_CustID")
                lblCustomerName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("CustomerName").ToString())
                iAuditTypeID = dt.Rows(0)("SA_AuditTypeID")
                lblAuditType.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("AuditType").ToString())
            End If
            BindTrialBalanceDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oAuditID As Object, oCheckPointID As Object
        Try
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iAuditID))
            oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCheckPointID))
            Response.Redirect(String.Format("~/StandardAudit/frmAuditLedgerUpload.aspx?FYID={0}&AuditID={1}&CheckPointID={2}", HttpUtility.UrlDecode(Request.QueryString("FYID")), oAuditID, oCheckPointID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub LoadFinalcialYear()
    '    Try
    '        ddlFinancialYear.DataSource = objclsStandardAudit.GetAddYearTo2DigitFinancialYear(sSession.AccessCode, sSession.AccessCodeID, 0)
    '        ddlFinancialYear.DataTextField = "YMS_ID"
    '        ddlFinancialYear.DataValueField = "YMS_YearID"
    '        ddlFinancialYear.DataBind()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
    '    Try
    '        BindCustomerDetails()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Public Sub BindTrialBalanceDetails()
        Dim iCustYearApproveCount As Integer = 0
        Dim dtRL As New DataTable, dtTBtoRL As New DataTable
        Dim iYearId As Integer
        Try
            lblError.Text = ""
            imgbtnSave.Visible = True
            If rboCurrentFY.Checked = True Then
                iYearId = iFinancialYearID
            ElseIf rboPreviousFY.Checked = True Then
                iYearId = iFinancialYearID - 1
            End If
            iCustYearApproveCount = objclsUTBE.getApproveCustYearCount(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
            If iCustYearApproveCount > 0 Then
                lblError.Text = "Records are approved"
                ImgbtnApprove.Visible = False
            End If

            dtRL = objclsUTBE.getCustRLSelectedYear(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
            If dtRL.Rows.Count > 0 Then
                dgGeneral.Visible = True
                dgGeneral.DataSource = dtRL
                dgGeneral.DataBind()
                imgbtnSave.Visible = True
                ImgbtnApprove.Visible = True
            Else
                dtTBtoRL = objclsUTBE.getCustTBtoRLSelectedYear(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
                If dtTBtoRL.Rows.Count > 0 Then
                    dgGeneral.Visible = True
                    dgGeneral.DataSource = dtTBtoRL
                    dgGeneral.DataBind()
                    imgbtnSave.Visible = True
                    ImgbtnApprove.Visible = True
                    lblError.Text = "Data loaded from the Trial Balance for this customer."
                Else
                    dgGeneral.Visible = False
                    imgbtnSave.Visible = False
                    ImgbtnApprove.Visible = False
                    lblError.Text = "No data found. Please upload the trial balance for this customer."
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomerDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Try
            lblError.Text = ""
            dgGeneral.Visible = False
            imgbtnSave.Visible = True
            If ddlSheetName.SelectedIndex > 0 Then
                dttable = LoadTrialBalanceData(sFile)
                If IsNothing(dttable) Then
                    ddlSheetName.SelectedIndex = 0
                    lblError.Text = "Invalid Excel format detected in the selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format detected in the selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf dttable.Rows.Count = 0 Then
                    lblError.Text = "No Data." : lblExcelValidationMsg.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                dgGeneral.DataSource = dttable
                dgGeneral.DataBind()
                dgGeneral.Visible = True
            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlSheetName.SelectedIndex = 0
                imgbtnSave.Visible = False
                lblError.Text = "Invalid Excel format detected in the selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format detected in the selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
                imgbtnSave.Visible = False
            End If
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function LoadTrialBalanceData(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtDetails As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim dt As New DataTable
        Try
            dtTable.Columns.Add("SrNo")
            dtTable.Columns.Add("DescID")
            dtTable.Columns.Add("Description")
            dtTable.Columns.Add("OpeningDebit")
            dtTable.Columns.Add("OpeningCredit")
            dtTable.Columns.Add("TrDebit")
            dtTable.Columns.Add("TrCredit")
            dtTable.Columns.Add("ClosingDebit")
            dtTable.Columns.Add("ClosingCredit")

            dtDetails = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtDetails) = True Then
                lblError.Text = "Invalid Excel format detected in the selected sheet."
                lblExcelValidationMsg.Text = "Invalid Excel format detected in the selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtDetails
            End If

            For i = 0 To dtDetails.Rows.Count - 1
                If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                    dRow = dtTable.NewRow
                    dRow("SrNo") = i - 1
                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("Description") = UCase(objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(0)))
                        End If
                    End If

                    'dRow("OpDebit") = ""
                    If IsDBNull(dtDetails.Rows(i).Item(1)) = False Then
                        If dtDetails.Rows(i).Item(1).ToString <> "&nbsp;" Then
                            ' If Convert.ToString(dtDetails.Rows(i).Item(1).Text.EndsWith("Dr")) Then
                            'dRow("OpeningDebit") = dtDetails.Rows(i).Item(1).Text.Remove(Len(dtDetails.Rows(i).Item(1).Text - 2), 2)
                            'Else
                            'dRow("OpeningDebit") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(1))
                            'End If

                            Dim s As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(1)).Replace(",", "")
                            dRow("OpeningDebit") = s.Replace(",", ".")
                            '  dRow("OpeningDebit") = s.Replace(",", ".")
                        End If
                    End If
                    'dRow("OpCredit") = ""
                    If IsDBNull(dtDetails.Rows(i).Item(2)) = False Then
                        If dtDetails.Rows(i).Item(2).ToString <> "&nbsp;" Then
                            Dim s1 As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(2)).Replace(",", "")
                            dRow("OpeningCredit") = s1.Replace(",", ".")
                            'dRow("OpeningCredit") = (dtDetails.Rows(i).Item(2))
                        End If
                    End If

                    'dRow("TrDebit") = ""
                    If IsDBNull(dtDetails.Rows(i).Item(3)) = False Then
                        If dtDetails.Rows(i).Item(3).ToString <> "&nbsp;" Then
                            Dim s2 As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(3)).Replace(",", "")
                            dRow("TrDebit") = s2.Replace(",", ".")
                            ' dRow("TrDebit") = (dtDetails.Rows(i).Item(3))
                        End If
                    End If
                    'dRow("TrCredit") = ""
                    If IsDBNull(dtDetails.Rows(i).Item(4)) = False Then
                        If dtDetails.Rows(i).Item(4).ToString <> "&nbsp;" Then
                            Dim s3 As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(4)).Replace(",", "")
                            dRow("TrCredit") = s3.Replace(",", ".")
                            'dRow("TrCredit") = (dtDetails.Rows(i).Item(4))
                        End If
                    End If

                    'dRow("ClDebit") = ""   
                    If IsDBNull(dtDetails.Rows(i).Item(5)) = False Then
                        If dtDetails.Rows(i).Item(5).ToString <> "&nbsp;" Then
                            Dim s4 As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(5)).Replace(",", "")
                            dRow("ClosingDebit") = s4.Replace(",", ".")
                            'dRow("ClosingDebit") = dtDetails.Rows(i).Item(5)
                        End If
                    End If

                    If IsDBNull(dtDetails.Rows(i).Item(6)) = False Then
                        If dtDetails.Rows(i).Item(6).ToString <> "&nbsp;" Then
                            Dim s5 As String = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(6)).Replace(",", "")
                            dRow("ClosingCredit") = s5.Replace(",", ".")
                            'dRow("ClosingCredit") = (dtDetails.Rows(i).Item(6))
                        End If
                    End If
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadTrialBalanceData" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
            'dgGeneral.DataSource = Nothing
            'dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = True
            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
                imgbtnSave.Visible = True : ImgbtnApprove.Visible = True
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
                    ddlSheetName.SelectedValue = 1
                    ddlSheetName_SelectedIndexChanged(sender, e)

                    lblExcelValidationMsg.Text = "Total Items in Excel :  " & dgGeneral.Rows.Count & ", TOtal Items Uploaded : " & dgGeneral.Rows.Count &
                        ", Total opening Credit : " & TotalOpeningCredit & ", Total Opening Debit : " & TotalOpeningDebit & ", Total Tr Credit : " & TOtaltrCredit & ", Total tr Debit : " & TOtaltrDebit & ", Total Closing Credit : " & TOtalClosingCredit & ", Total Closing Debit : " & TOtalClosingDebit
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    lblError.Text = lblExcelValidationMsg.Text
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
    Private Sub dgGeneral_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgGeneral.RowDataBound
        Dim ddlGrdSubGroup As New DropDownList
        Dim lblslno As New Label
        Dim lblOpeningCredit As New Label
        Dim lblOpeningDebit As New Label
        Dim lblTrDebit As New Label
        Dim lblTrCredit As New Label
        Dim lblClosingDebit As New Label
        Dim lblClosingCredit As New Label
        Dim lblStatus As New Label
        Dim lblDescID As New Label
        Dim dDebtAmt As Double = 0.0
        Dim dCreAmt As Double = 0.0
        Dim dTotAmt As Double = 0.0
        Try
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                lblDescID = (TryCast(e.Row.FindControl("lblDescID"), Label))
                lblDescID = (TryCast(e.Row.FindControl("lblDescription"), Label))
                lblOpeningCredit = (TryCast(e.Row.FindControl("lblOpeningCredit"), Label))
                lblOpeningDebit = (TryCast(e.Row.FindControl("lblOpeningDebit"), Label))
                lblTrDebit = (TryCast(e.Row.FindControl("lblTrDebit"), Label))
                lblTrCredit = (TryCast(e.Row.FindControl("lblTrCredit"), Label))
                lblClosingDebit = (TryCast(e.Row.FindControl("lblClosingDebit"), Label))
                lblClosingCredit = (TryCast(e.Row.FindControl("lblClosingCredit"), Label))

                If lblTrDebit.Text = "" Then
                    lblTrDebit.Text = 0
                End If
                If lblTrCredit.Text = "" Then
                    lblTrCredit.Text = 0
                End If
                If lblClosingDebit.Text = "" Then
                    lblClosingDebit.Text = 0
                End If
                If lblClosingCredit.Text = "" Then
                    lblClosingCredit.Text = 0
                End If
                'dgGeneral.Columns(16).Visible = False : dgGeneral.Columns(12).Visible = False : dgGeneral.Columns(14).Visible = False : dgGeneral.Columns(10).Visible = False
                If lblOpeningCredit.Text = "" Or Nothing Or IsDBNull(lblOpeningCredit.Text) = True Then
                    lblOpeningCredit.Text = 0
                ElseIf Val(lblOpeningCredit.Text) <> 0 Then
                    lblOpeningCredit.Text = Math.Round(Convert.ToDouble(lblOpeningCredit.Text), 2)
                ElseIf Val(lblOpeningCredit.Text) = 0 Then
                    lblOpeningCredit.Text = "0.00"
                End If

                If lblOpeningDebit.Text = "" Or Nothing Or IsDBNull(lblOpeningDebit.Text) = True Then
                    lblOpeningDebit.Text = 0
                ElseIf Val(lblOpeningDebit.Text) <> 0 Then
                    lblOpeningDebit.Text = Math.Round(Convert.ToDouble(lblOpeningDebit.Text), 2)
                ElseIf Val(lblOpeningDebit.Text) = 0 Then
                    lblOpeningDebit.Text = "0.00"
                End If

                If lblClosingCredit.Text = "" Or Nothing Or IsDBNull(lblClosingCredit.Text) = True Then
                    lblClosingCredit.Text = 0
                ElseIf Val(lblClosingCredit.Text) <> 0 Then
                    lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblClosingCredit.Text), 2)
                ElseIf Val(lblClosingCredit.Text) = 0 Then
                    lblClosingCredit.Text = "0.00"
                End If

                If lblClosingDebit.Text = "" Or Nothing Or IsDBNull(lblClosingDebit.Text) = True Then
                    lblClosingDebit.Text = 0
                ElseIf Val(lblClosingDebit.Text) <> 0 Then
                    lblClosingDebit.Text = Math.Round(Convert.ToDouble(lblClosingDebit.Text), 2)
                ElseIf Val(lblClosingDebit.Text) = 0 Then
                    lblClosingDebit.Text = "0.00"
                End If

                If lblTrDebit.Text = "" Or Nothing Or IsDBNull(lblTrDebit.Text) = True Then
                    lblTrDebit.Text = 0
                ElseIf Val(lblTrDebit.Text) <> 0 Then
                    lblTrDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
                ElseIf Val(lblTrDebit.Text) = 0 Then
                    lblTrDebit.Text = "0.00"
                End If

                If lblTrCredit.Text = "" Or Nothing Or IsDBNull(lblTrCredit.Text) = True Then
                    lblTrCredit.Text = "0.00"
                ElseIf Val(lblTrCredit.Text) <> 0 Then
                    lblTrCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
                ElseIf Val(lblTrCredit.Text) = 0 Then
                    lblTrCredit.Text = "0.00"
                End If


                'If Val(lblClosingDebit.Text) <> 0 Then
                '    If lblTrCredit.Text = "" Or Nothing Or IsDBNull(lblTrCredit.Text) = True Then
                '        lblTrCredit.Text = 0
                '    ElseIf Val(lblTrCredit.Text) <> 0 Then
                '        lblTrCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
                '        'lblClosingDebit.Text = Val(lblOpeningCredit.Text) + Val(lblTrCredit.Text)
                '        lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblClosingCredit.Text), 2)
                '    ElseIf lblTrDebit.Text = "" Or Nothing Or IsDBNull(lblTrDebit.Text) = True Then
                '        lblTrDebit.Text = 0
                '    ElseIf Val(lblTrDebit.Text) <> 0 Then
                '        lblTrDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
                '        'lblClosingDebit.Text = Val(lblTrDebit.Text) - Val(lblOpeningDebit.Text)
                '        lblClosingDebit.Text = Math.Round(Convert.ToDouble(lblClosingDebit.Text), 2)
                '        'lblTrCredit.Text = lblTrCredit.Text + objUT.LoadItemsfromJECreditdebit(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, Val(lblitemid.Text), 0)
                '    End If
                'ElseIf lblTrCredit.Text <> 0 And Val(lblClosingDebit.Text) = 0 And Val(lblClosingCredit.Text) = 0 Then
                '    ' lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
                'ElseIf lblTrDebit.Text <> 0 And Val(lblClosingDebit.Text) = 0 And Val(lblClosingCredit.Text) = 0 Then
                '    ' lblClosingDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
                'End If
                'If Val(lblClosingCredit.Text) <> 0 Then
                '    If lblTrCredit.Text = "" Or Nothing Or IsDBNull(lblTrCredit.Text) = True Then
                '        lblTrCredit.Text = 0
                '    ElseIf Val(lblTrCredit.Text) <> 0 Then
                '        lblTrCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
                '        'lblClosingCredit.Text = Val(lblOpeningCredit.Text) + Val(lblTrCredit.Text)
                '        'lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblClosingCredit.Text), 2)
                '    ElseIf lblTrDebit.Text = "" Or Nothing Or IsDBNull(lblTrDebit.Text) = True Then
                '        lblTrDebit.Text = 0
                '    ElseIf Val(lblTrDebit.Text) <> 0 Then
                '        lblTrDebit.Text = Math.Round(Convert.ToDouble(lblTrDebit.Text), 2)
                '        'lblClosingCredit.Text = Val(lblOpeningCredit.Text) - Val(lblTrDebit.Text)
                '        'lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblClosingCredit.Text), 2)
                '    End If
                'ElseIf lblTrCredit.Text <> 0 Then
                '    'lblClosingCredit.Text = Math.Round(Convert.ToDouble(lblTrCredit.Text), 2)
                'End If

                TotalOpeningCredit = TotalOpeningCredit + Math.Abs(Convert.ToDouble(lblOpeningCredit.Text))
                'TotalOpeningCredit = Decimal.Negate(TotalOpeningCredit)
                TotalOpeningDebit = TotalOpeningDebit + Math.Abs(Convert.ToDouble(lblOpeningDebit.Text))
                TOtalClosingCredit = TOtalClosingCredit + Convert.ToDouble(lblClosingCredit.Text)
                TOtalClosingDebit = TOtalClosingDebit + Convert.ToDouble(lblClosingDebit.Text)
                TOtaltrCredit = TOtaltrCredit + Convert.ToDouble(lblTrCredit.Text)
                TOtaltrDebit = TOtaltrDebit + Convert.ToDouble(lblTrDebit.Text)

                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub dgGeneral_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dgGeneral.RowCommand
        Dim lblItemid As New Label
        Dim dt2 As DataTable
        Try
            lblError.Text = ""
            If e.CommandName = "EditRow" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblItemid = DirectCast(clickedRow.FindControl("lblDescID"), Label)
                dgGeneral.DataSource = dt2
                dgGeneral.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalJEItems').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            BindTrialBalanceDetails()
            For i = 0 To 10000
                i = i + 1
            Next
            lblError.Text = "File Uploaded Successfully"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpload_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim lbldescCode As New Label, lblOpeningDebit As New Label, lblOpeningCredit As New Label, lblTrDebit As New Label, lblTrCredit As New Label, lblClosingDebit As New Label, lblClosingCredit As New Label
        Dim lblDescription As New LinkButton
        Dim lblDescID As New Label
        Dim iCustYearCount As Integer = 0
        Dim iYearId As Integer
        Try
            lblError.Text = ""
            If dgGeneral.Rows.Count = 0 Then
                lblError.Text = "No Data."
                Exit Sub
            End If

            If rboCurrentFY.Checked = True Then
                iYearId = iFinancialYearID
            ElseIf rboPreviousFY.Checked = True Then
                iYearId = iFinancialYearID - 1
            End If

            iCustYearCount = objclsUTBE.getCustYearCount(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
            If iCustYearCount > 0 Then
                objclsUTBE.DeleteCustRecord(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
            End If
            For i = 0 To dgGeneral.Rows.Count - 1
                lblDescID = dgGeneral.Rows(i).FindControl("lblDescID")
                lblDescription = dgGeneral.Rows(i).FindControl("lblDescription")
                lblOpeningDebit = dgGeneral.Rows(i).FindControl("lblOpeningDebit")
                lblOpeningCredit = dgGeneral.Rows(i).FindControl("lblOpeningCredit")
                lblTrDebit = dgGeneral.Rows(i).FindControl("lblTrDebit")
                lblTrCredit = dgGeneral.Rows(i).FindControl("lblTrCredit")
                lblClosingDebit = dgGeneral.Rows(i).FindControl("lblClosingDebit")
                lblClosingCredit = dgGeneral.Rows(i).FindControl("lblClosingCredit")

                If Val(lblDescID.Text) = 0 Then
                    If Val(lblDescID.Text) <> 0 Then
                        objclsUTBE.iAEU_ID = lblDescID.Text
                    Else
                        objclsUTBE.iAEU_ID = 0
                    End If
                    objclsUTBE.sAEU_Description = UCase(lblDescription.Text)
                    objclsUTBE.iAEU_CustId = iCustID
                    objclsUTBE.iAEU_AuditId = iAuditID
                    objclsUTBE.iAEU_AuditTypeId = iAuditTypeID
                    objclsUTBE.dAEU_ODAmount = Double.Parse(lblOpeningDebit.Text)
                    objclsUTBE.dAEU_OCAmount = Double.Parse(lblOpeningCredit.Text)
                    objclsUTBE.dAEU_TRDAmount = Double.Parse(lblTrDebit.Text)
                    objclsUTBE.dAEU_TRCAmount = Double.Parse(lblTrCredit.Text)
                    objclsUTBE.dAEU_CDAmount = Double.Parse(lblClosingDebit.Text)
                    objclsUTBE.dAEU_CCAmount = Double.Parse(lblClosingCredit.Text)
                    objclsUTBE.sAEU_DELFLG = "A"
                    objclsUTBE.iAEU_CRBY = sSession.UserID
                    objclsUTBE.sAEU_STATUS = "C"
                    objclsUTBE.iAEU_UPDATEDBY = sSession.UserID
                    objclsUTBE.sAEU_IPAddress = sSession.IPAddress
                    objclsUTBE.iAEU_CompId = sSession.AccessCodeID
                    objclsUTBE.iAEU_YEARId = iYearId
                    Arr = objclsUTBE.SaveUploadTBExcel(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsUTBE)
                End If
            Next
            objclsStandardAudit.UpdateStandardAuditStatus(sSession.AccessCode, sSession.AccessCodeID, iAuditID, 3)
            ImgbtnApprove.Visible = True
            lblExcelValidationMsg.Text = "Successfully uploaded." : lblError.Text = "Successfully uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Try
            lblError.Text = ""
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & "AuditTrailBalance" & ".xlsx")
            Response.TransmitFile(Server.MapPath("../") & "SampleExcels\" & "AuditTrailBalance" & ".xlsx")
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkDownload_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ImgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnApprove.Click
        Dim iCustYearApproveCount As Integer = 0
        Dim iYearId As Integer
        Try
            lblError.Text = ""
            If dgGeneral.Rows.Count = 0 Then
                lblError.Text = "No Data."
                Exit Sub
            End If

            If rboCurrentFY.Checked = True Then
                iYearId = iFinancialYearID
            ElseIf rboPreviousFY.Checked = True Then
                iYearId = iFinancialYearID - 1
            End If

            iCustYearApproveCount = objclsUTBE.getApproveCustYearCount(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
            If iCustYearApproveCount > 0 Then
                lblError.Text = "Records are approved."
                Exit Sub
            Else
                objclsUTBE.ApproveCustomerStatus(sSession.AccessCode, sSession.AccessCodeID, iCustID, iYearId, iAuditID)
                BindTrialBalanceDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnApprove_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboCurrentFY_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboCurrentFY.CheckedChanged
        Try
            lblError.Text = ""
            BindTrialBalanceDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboCurrentFY_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboPreviousFY_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboPreviousFY.CheckedChanged
        Try
            lblError.Text = ""
            BindTrialBalanceDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboPreviousFY_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
