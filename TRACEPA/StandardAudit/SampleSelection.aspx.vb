Imports System
Imports System.Data
Imports BusinesLayer
Imports System.IO
Imports DatabaseLayer
Imports System.Text
Imports System.Web
Partial Class SampleSelection
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_SampleSelection"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditGeneral As New clsAuditGeneral
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAttachments As New clsAttachments
    Private objclsSampling As New clsSampling
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsStandardAudit As New clsStandardAudit
    Private objDBL As New DBHelper

    Private sSession As AllSession
    Private Shared iDocID As Integer
    Private Shared iDRLAttachID As Integer
    Private Shared iAuditID As Integer = 0
    Private Shared iCheckPointID As Integer = 0
    Private Shared dtOriginalExcel As New DataTable
    Private Shared dtExcelColumns As New DataTable
    Private Shared dtFinalWithPKID As New DataTable
    Private Shared dtNSTWithPKID As New DataTable
    Private Shared dtSTWithPKID As New DataTable
    Private Shared dtRRWithPKID As New DataTable
    Private Shared sOriginalExcelPath As String
    Private Shared sDestFilePath As String
    Private Shared sNSTPKID As String
    Private Shared sSTPKID As String
    Private Shared sRRPKID As String
    Private Shared iSSAtchID As Integer = 0
    Private Shared sSave As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnNSTFilter.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnSTFilter.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnRR.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnNSTAddToFinal.ImageUrl = "~/Images/Add24.png"
        imgbtnSTAddToFinal.ImageUrl = "~/Images/Add24.png"
        imgbtnRRFilter.ImageUrl = "~/Images/Add24.png"
        imgbtnFinalSave.ImageUrl = "~/Images/Save24.png"
        imgbtnCheckDuplicate.ImageUrl = "~/Images/Submit24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Public Function LoadExcelSheetNames(ByVal sPath As String) As DataTable
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try
            XLCon = MSAccessOpenConnection(sPath)
            sOriginalExcelPath = sPath
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
            Throw
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
            Throw
        End Try
    End Function
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            lblError.Text = ""
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnNSTFilter.Visible = True : imgbtnSTFilter.Visible = True : imgbtnRR.Visible = True : imgbtnNSTAddToFinal.Visible = True
                imgbtnSTAddToFinal.Visible = True : imgbtnRRFilter.Visible = True : imgbtnFinalSave.Visible = True : imgbtnCheckDuplicate.Visible = True

                sNSTPKID = "" : sSTPKID = "" : sRRPKID = ""
                dtOriginalExcel = New DataTable : dtExcelColumns = New DataTable : dtFinalWithPKID = New DataTable
                dtNSTWithPKID = New DataTable : dtSTWithPKID = New DataTable : dtRRWithPKID = New DataTable

                If Request.QueryString("AuditID") IsNot Nothing Then
                    iAuditID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AuditID")))
                End If
                If Request.QueryString("DocID") IsNot Nothing Then
                    iDocID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("DocID")))
                End If
                If Request.QueryString("AttachID") IsNot Nothing Then
                    iDRLAttachID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AttachID")))
                End If
                If Request.QueryString("CheckPointID") IsNot Nothing Then
                    iCheckPointID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CheckPointID")))
                End If
                If iAuditID > 0 And iDRLAttachID > 0 Then
                    LoadExcelSheetDetails()
                Else
                    lblError.Text = "Documents not available. Please attach Documents in DRL Log."
                End If
                iSSAtchID = objclsStandardAudit.GetSamplingAuditAttachmentID(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointID)
                If iSSAtchID > 0 Then
                    BindSamplingAttachments(iAuditID, iSSAtchID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oAuditID As Object, oCheckPointID As Object
        Try
            oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iAuditID))
            oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCheckPointID))
            Response.Redirect(String.Format("~/StandardAudit/DRLSampling.aspx?AuditID={0}&CheckPointID={1}", oAuditID, oCheckPointID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub LoadExcelSheetDetails()
        Dim dtExcelSheet As New DataTable
        Dim sPaths As String
        Try
            sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
            sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iDRLAttachID, iDocID)
            lblDocName.Text = objclsSampling.GetDocumentName(sSession.AccessCode, sSession.AccessCodeID, iDRLAttachID, iDocID)
            dtExcelSheet = LoadExcelSheetNames(sDestFilePath)
            ddlExcelSheet.DataSource = dtExcelSheet
            ddlExcelSheet.DataTextField = "Name"
            ddlExcelSheet.DataValueField = "ID"
            ddlExcelSheet.DataBind()
            ddlExcelSheet.Items.Insert(0, "Select Sheet Name")
            ddlExcelSheet.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExcelSheetDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlExcelSheet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExcelSheet.SelectedIndexChanged
        Dim dtColumns As New DataTable
        Try
            lblError.Text = ""
            If ddlExcelSheet.SelectedIndex > 0 Then
                If File.Exists(sDestFilePath) = False Then
                    Dim sPaths As String = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                    sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iDRLAttachID, iDocID)
                End If
                dtOriginalExcel = objDBL.ReadExcel("Select * from [" & Trim(ddlExcelSheet.SelectedItem.Text) & "] ", sDestFilePath)
                dtOriginalExcel.AsEnumerable().Where(Function(row) row.ItemArray.All(Function(field) field Is Nothing Or field Is DBNull.Value Or field.Equals(""))).ToList().ForEach(Sub(row) row.Delete())
                dtOriginalExcel.AcceptChanges()

                If dtOriginalExcel.Rows.Count > 0 Then
                    lblNoofRows.Text = Val(dtOriginalExcel.Rows.Count)
                Else
                    lblNoofRows.Text = 0
                End If

                dtColumns = LoadColumnDDL(dtOriginalExcel)

                ddlColumns.DataSource = dtColumns
                ddlColumns.DataValueField = "HeaderID"
                ddlColumns.DataTextField = "HeaderName"
                ddlColumns.DataBind()
                ddlColumns.Items.Insert(0, "Select Column")
                ddlColumns.SelectedIndex = 0
            Else
                ddlColumns.SelectedIndex = 0 : ddlFilter.SelectedIndex = 0 : txtFrmVal.Visible = False : txtTo.Visible = False : lblNoofRows.Text = ""
                dgNST.DataSource = Nothing
                dgNST.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExcelSheet_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadColumnDDL(ByVal dt As DataTable) As DataTable
        Dim i As Integer
        Dim drow As DataRow
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
    Private Sub ddlFilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFilter.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblFromValue.Visible = False : txtFrmVal.Visible = False : lblTo.Visible = False : txtTo.Visible = False : ddlSelValue.Visible = False
            If ddlColumns.SelectedIndex > 0 Then
                If ddlFilter.SelectedValue = 1 Then '-ve Value
                    lblFromValue.Visible = False : txtFrmVal.Visible = False : lblTo.Visible = False : txtTo.Visible = False : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 2 Then '5 From High Value
                    lblFromValue.Visible = False : txtFrmVal.Visible = False : lblTo.Visible = False : txtTo.Visible = False : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 3 Then 'High Value between
                    lblFromValue.Visible = True : txtFrmVal.Visible = True : lblTo.Visible = True : txtTo.Visible = True : lblFromValue.Text = "From" : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 4 Then 'Top 10 Values
                    lblFromValue.Visible = False : txtFrmVal.Visible = False : lblTo.Visible = False : txtTo.Visible = False : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 5 Then 'Least 10 Values
                    lblFromValue.Visible = False : txtFrmVal.Visible = False : lblTo.Visible = False : txtTo.Visible = False : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 6 Then 'Greater than or Equal
                    lblFromValue.Visible = True : txtFrmVal.Visible = True : lblTo.Visible = False : txtTo.Visible = False : lblFromValue.Text = "Value" : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 7 Then 'Equal
                    lblFromValue.Visible = True : txtFrmVal.Visible = True : lblTo.Visible = False : txtTo.Visible = False : lblFromValue.Text = "Value" : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 8 Then 'Not Equal
                    lblFromValue.Visible = True : txtFrmVal.Visible = True : lblTo.Visible = False : txtTo.Visible = False : lblFromValue.Text = "Value" : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 9 Then 'Less than or Equal
                    lblFromValue.Visible = True : txtFrmVal.Visible = True : lblTo.Visible = False : txtTo.Visible = False : lblFromValue.Text = "Value" : ddlSelValue.Visible = False
                ElseIf ddlFilter.SelectedValue = 11 Then 'Selected value
                    BindSelectedValue()
                    lblFromValue.Visible = True : txtFrmVal.Visible = False : lblTo.Visible = False : txtTo.Visible = False : lblFromValue.Text = "Value" : ddlSelValue.Visible = True
                End If
            Else
                lblError.Text = "Select Column Name." : lblSampleMsg.Text = "Select Column Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                ddlColumns.Focus()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFilter_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub BindSelectedValue()
        Dim dtExcelSheet As New DataTable
        Try
            dtExcelSheet = LoadColumnDeatils()
            ddlSelValue.DataSource = dtExcelSheet
            ddlSelValue.DataTextField = "ColumnName"
            ddlSelValue.DataValueField = "ColumnID"
            ddlSelValue.DataBind()
            ddlSelValue.Items.Insert(0, "Select Value")
            ddlSelValue.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSelectedValue" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadColumnDeatils() As DataTable
        Dim i As Integer = 1
        Dim drow As DataRow
        Dim dtExcelColumns As New DataTable
        Dim sName As String = "", sValue As String = "", sColumnValue() As String
        Dim iCount As Integer = 0
        Try
            dtExcelColumns.Columns.Add("ColumnID")
            dtExcelColumns.Columns.Add("ColumnName")
            If dtOriginalExcel.Columns.Count > 0 Then
                For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                    drow = dtExcelColumns.NewRow
                    drow("ColumnID") = i
                    If IsDBNull(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = False Then
                        sName = dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)
                        If sName <> "" Then
                            If sValue Is "" Then
                                sValue = sValue & ";"
                            End If
                            sColumnValue = sValue.Split(";")
                            If sColumnValue.Length > 0 Then
                                For j = 0 To sColumnValue.Length - 1
                                    If sColumnValue(j) IsNot "" Then
                                        If (UCase(sColumnValue(j)) = UCase(sName)) Then
                                            iCount = 0
                                            GoTo Skip
                                        Else
                                            iCount = 1
                                        End If
                                    ElseIf sColumnValue(1) Is "" Then
                                        iCount = 1
                                    End If
                                Next
                                If iCount = 1 Then
                                    drow("ColumnName") = sName
                                    sValue = sValue & drow("ColumnName") & ";"
                                    dtExcelColumns.Rows.Add(drow)
                                    i = i + 1
                                End If
                            End If
                        End If
                    End If

Skip:           Next
            End If
            Return dtExcelColumns
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadColumnDeatils" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Function
    Private Sub imgbtnNSTFilter_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNSTFilter.Click
        Dim iOrginalExcel As Integer = 0, iAllColumns As Integer = 0, iRowsCount As Integer = 0, iFilter As Integer = 0, iCheckRow As Integer = 0, iSlNo As Integer = 0
        Dim iValue As Integer = 0
        Dim sColumnNames As String = "", sSelectedValue As String = ""
        Dim dtTempExcel As New DataTable, dtFilter As New DataTable
        Dim aClomuns As String()
        Dim drow As DataRow
        Dim dvOriginalExcel As New DataView
        Try
            lblError.Text = "" : iCheckRow = 0
            dgNST.DataSource = Nothing
            dgNST.DataBind()
            If ddlColumns.SelectedIndex = 0 Then
                lblError.Text = "Select Column Name." : lblSampleMsg.Text = "Select Column Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                ddlColumns.Focus()
                Exit Try
            End If

            If ddlFilter.SelectedIndex = 0 Then
                lblError.Text = "Select Filter Type." : lblSampleMsg.Text = "Select Filter Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                ddlFilter.Focus()
                Exit Try
            End If
            dtNSTWithPKID = New DataTable
            If IsNothing(dtNSTWithPKID) = False Then
                If dtNSTWithPKID.Columns.Count = 0 Then
                    dtNSTWithPKID.Columns.Add("Sl.No")
                    dtNSTWithPKID.Columns.Add("PKID")
                    dtNSTWithPKID.Columns.Add("FilterTypes")
                    For iAllColumns = 0 To dtExcelColumns.Rows.Count - 1
                        dtNSTWithPKID.Columns.Add(dtExcelColumns.Rows(iAllColumns)(1).ToString())
                    Next
                End If
            End If
            iSlNo = dtNSTWithPKID.Rows.Count

            If ddlColumns.SelectedIndex > 0 Then
                sColumnNames = "Sl.No,PKID,FilterTypes"
                dtFilter.Columns.Add("Sl.No")
                dtFilter.Columns.Add("PKID")
                dtFilter.Columns.Add("FilterTypes")

                For iAllColumns = 0 To dtExcelColumns.Rows.Count - 1
                    dtFilter.Columns.Add(dtExcelColumns.Rows(iAllColumns)(1).ToString())
                    sColumnNames = sColumnNames & "," & dtExcelColumns.Rows(iAllColumns)(1).ToString()
                Next
                aClomuns = sColumnNames.Split(",")

                If ddlFilter.SelectedValue = 1 Then '-ve Value
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        If IsDBNull(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                            Continue For
                        End If
                        If (dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString.Contains("-")) Then
                            If IsNumeric(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                                lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                Exit Try
                            End If
                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                        '  drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "-ve Value"
                                    Else
                                        drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            iCheckRow = 1
                            dtFilter.Rows.Add(drow)
                        End If
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No -ve Values For selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No -ve Values For selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 2 Then '5 From High Value
                    dvOriginalExcel = dtOriginalExcel.DefaultView
                    dvOriginalExcel.Sort = ddlColumns.SelectedItem.Text & " ASC"
                    dtTempExcel = dvOriginalExcel.ToTable
                    If dtTempExcel.Rows.Count > 5 Then
                        iRowsCount = (dtTempExcel.Rows.Count - 5)
                        For iOrginalExcel = iRowsCount To dtTempExcel.Rows.Count - 1
                            If IsDBNull(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                                Continue For
                            End If
                            If IsNumeric(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                                lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                Exit Try
                            End If
                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                'If (aClomuns(j) <> "") Then
                                '    drow(aClomuns(j))=dtTempExcel.Rows(iOrginalExcel)(iFilter)
                                '    iFilter=iFilter + 1
                                'End If
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                        ' drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "5 From High Value"
                                    Else
                                        drow(aClomuns(j)) = dtTempExcel.Rows(iOrginalExcel)(iFilter) 'dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                            iRowsCount = iRowsCount + 1
                        Next
                    ElseIf dtTempExcel.Rows.Count > 0 Then
                        For iOrginalExcel = 0 To dtTempExcel.Rows.Count - 1
                            If IsDBNull(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                                Continue For
                            End If
                            If IsNumeric(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                                lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                Exit Try
                            End If
                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                        'drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "5 From High Value"
                                    Else
                                        drow(aClomuns(j)) = dtTempExcel.Rows(iOrginalExcel)(iFilter) 'dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                        Next
                    Else
                        lblError.Text = "No High Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No High Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 3 Then 'High values between 
                    If txtFrmVal.Text = "" Then
                        lblError.Text = "Enter From Value for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter From Value for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    If txtTo.Text = "" Then
                        lblError.Text = "Enter To Value for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter To Value for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtTo.Focus()
                        Exit Try
                    End If
                    If IsNumeric(txtFrmVal.Text) = False Then
                        lblError.Text = "Enter Integer Value in From text field for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter Integer Value in From text field for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                    If IsNumeric(txtTo.Text) = False Then
                        lblError.Text = "Enter Integer Value in To text field for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter Integer Value in To text field for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        If IsDBNull(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                            Continue For
                        End If
                        If IsNumeric(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                            lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                            Exit Try
                        End If
                        Try
                            iValue = Convert.ToDouble(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text))
                            If ((Convert.ToDouble(txtFrmVal.Text) <= iValue) And (Convert.ToDouble(txtTo.Text) >= iValue)) Then
                                drow = dtFilter.NewRow
                                iFilter = 0
                                For j = 0 To aClomuns.Length - 1
                                    If (aClomuns(j) <> "") Then
                                        If aClomuns(j) = "Sl.No" And j = 0 Then
                                            iSlNo = iSlNo + 1
                                            drow(aClomuns(j)) = iSlNo
                                            '  drow(aClomuns(j))=dtNST.Rows.Count + 1
                                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                            drow(aClomuns(j)) = iOrginalExcel
                                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                            drow(aClomuns(j)) = "High values between"
                                        Else
                                            drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                            iFilter = iFilter + 1
                                        End If
                                    End If
                                Next
                                iCheckRow = 1
                                dtFilter.Rows.Add(drow)
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No Between Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Between Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 4 Then 'Top 10 Values
                    dvOriginalExcel = dtOriginalExcel.DefaultView
                    dvOriginalExcel.Sort = ddlColumns.SelectedItem.Text & " ASC"
                    dtTempExcel = dvOriginalExcel.ToTable
                    If dtTempExcel.Rows.Count > 10 Then
                        iRowsCount = (dtTempExcel.Rows.Count - 10)
                        For iOrginalExcel = iRowsCount To dtTempExcel.Rows.Count - 1
                            If IsDBNull(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                                Continue For
                            End If
                            If IsNumeric(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                                lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                Exit Try
                            End If

                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "Top 10 Values"
                                    Else
                                        drow(aClomuns(j)) = dtTempExcel.Rows(iOrginalExcel)(iFilter) 'dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                            iRowsCount = iRowsCount + 1
                        Next
                    ElseIf dtTempExcel.Rows.Count > 0 Then
                        For iOrginalExcel = 0 To dtTempExcel.Rows.Count - 1
                            If IsDBNull(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                                Continue For
                            End If
                            If IsNumeric(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                                lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                Exit Try
                            End If

                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                        '  drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "Top 10 Values"
                                    Else
                                        drow(aClomuns(j)) = dtTempExcel.Rows(iOrginalExcel)(iFilter) 'dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                        Next
                    Else
                        lblError.Text = "No Top 10 Values For selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Top 10 Values For selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 5 Then 'Least 10 Values
                    dvOriginalExcel = dtOriginalExcel.DefaultView
                    dvOriginalExcel.Sort = ddlColumns.SelectedItem.Text & " Desc"
                    dtTempExcel = dvOriginalExcel.ToTable
                    If dtTempExcel.Rows.Count > 10 Then
                        iRowsCount = (dtTempExcel.Rows.Count - 10)
                        For iOrginalExcel = iRowsCount To dtTempExcel.Rows.Count - 1
                            'If IsNumeric(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                            '    lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                            '    Exit Try
                            'End If
                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                        ' drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "Least 10 Values"
                                    Else
                                        drow(aClomuns(j)) = dtTempExcel.Rows(iOrginalExcel)(iFilter) 'dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                            iRowsCount = iRowsCount + 1
                        Next
                    ElseIf dtTempExcel.Rows.Count > 0 Then
                        For iOrginalExcel = 0 To dtTempExcel.Rows.Count - 1
                            If IsDBNull(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                                Continue For
                            End If
                            If IsNumeric(dtTempExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                                lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                Exit Try
                            End If
                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                        'drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = iOrginalExcel
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        drow(aClomuns(j)) = "Least 10 Values"
                                    Else
                                        drow(aClomuns(j)) = dtTempExcel.Rows(iOrginalExcel)(iFilter) 'dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                        Next
                    Else
                        lblError.Text = "No Least 10 Values For selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Least 10 Values For selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 6 Then 'Greater than or equal
                    If txtFrmVal.Text = "" Then
                        lblError.Text = "Enter Value for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter Value for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    If IsNumeric(txtFrmVal.Text) = False Then
                        lblError.Text = "Enter number in From text field for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter number in From text field for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        If IsDBNull(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                            Continue For
                        End If
                        If IsNumeric(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                            lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                            Exit Try
                        End If
                        Try
                            iValue = Convert.ToDouble(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text))
                            If (Convert.ToDouble(txtFrmVal.Text) <= iValue) Then
                                drow = dtFilter.NewRow
                                iFilter = 0
                                For j = 0 To aClomuns.Length - 1
                                    If (aClomuns(j) <> "") Then
                                        If aClomuns(j) = "Sl.No" And j = 0 Then
                                            iSlNo = iSlNo + 1
                                            drow(aClomuns(j)) = iSlNo
                                            '  drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                            drow(aClomuns(j)) = iOrginalExcel
                                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                            drow(aClomuns(j)) = "Greater than or equal"
                                        Else
                                            drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                            iFilter = iFilter + 1
                                        End If
                                    End If
                                Next
                                iCheckRow = 1
                                dtFilter.Rows.Add(drow)
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No Greater than or Equal Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Greater than or Equal Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 7 Then 'Equal Values
                    If txtFrmVal.Text = "" Then
                        lblError.Text = "Enter Value to compare for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter Value to compare for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        Try
                            iValue = Convert.ToDouble(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text))
                            If (Convert.ToDouble(txtFrmVal.Text) = iValue) Then
                                drow = dtFilter.NewRow
                                iFilter = 0
                                For j = 0 To aClomuns.Length - 1
                                    If (aClomuns(j) <> "") Then
                                        If aClomuns(j) = "Sl.No" And j = 0 Then
                                            iSlNo = iSlNo + 1
                                            drow(aClomuns(j)) = iSlNo
                                            ' drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                            drow(aClomuns(j)) = iOrginalExcel
                                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                            drow(aClomuns(j)) = "Equal Values"
                                        Else
                                            drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                            iFilter = iFilter + 1
                                        End If
                                    End If
                                Next
                                iCheckRow = 1
                                dtFilter.Rows.Add(drow)
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No Equal Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Equal Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 8 Then 'Not Equal Values
                    If txtFrmVal.Text = "" Then
                        lblError.Text = "Enter Value to compare for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter Value to compare for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        Try
                            iValue = Convert.ToDouble(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text))
                            If (Convert.ToDouble(txtFrmVal.Text) <> iValue) Then
                                drow = dtFilter.NewRow
                                iFilter = 0
                                For j = 0 To aClomuns.Length - 1
                                    If (aClomuns(j) <> "") Then
                                        If aClomuns(j) = "Sl.No" And j = 0 Then
                                            iSlNo = iSlNo + 1
                                            drow(aClomuns(j)) = iSlNo
                                            '  drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                            drow(aClomuns(j)) = iOrginalExcel
                                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                            drow(aClomuns(j)) = "Not Equal Values"
                                        Else
                                            drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                            iFilter = iFilter + 1
                                        End If
                                    End If
                                Next
                                iCheckRow = 1
                                dtFilter.Rows.Add(drow)
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No Not Equal Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Not Equal Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 9 Then 'Less than or equal
                    If txtFrmVal.Text = "" Then
                        lblError.Text = "Enter Value for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter Value for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    If IsNumeric(txtFrmVal.Text) = False Then
                        lblError.Text = "Enter number in From text field for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Enter number in From text field for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        If IsDBNull(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)) = True Then
                            Continue For
                        End If
                        If IsNumeric(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text).ToString()) = False Then
                            lblError.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column." : lblSampleMsg.Text = "The selected column " & ddlColumns.SelectedItem.Text & " has a string values . Please select the numeric value column."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                            Exit Try
                        End If
                        Try
                            iValue = Convert.ToDouble(dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text))
                            If (Convert.ToDouble(txtFrmVal.Text) >= iValue) Then
                                drow = dtFilter.NewRow
                                iFilter = 0
                                For j = 0 To aClomuns.Length - 1
                                    If (aClomuns(j) <> "") Then
                                        If aClomuns(j) = "Sl.No" And j = 0 Then
                                            iSlNo = iSlNo + 1
                                            drow(aClomuns(j)) = iSlNo
                                            '   drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                            drow(aClomuns(j)) = iOrginalExcel
                                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                            drow(aClomuns(j)) = "Less than or equal"
                                        Else
                                            drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                            iFilter = iFilter + 1
                                        End If
                                    End If
                                Next
                                iCheckRow = 1
                                dtFilter.Rows.Add(drow)
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No Less than or Equal Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Less than or Equal Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If

                If ddlFilter.SelectedValue = 10 Then 'Missing Nos
                    Dim iStartValue As Integer, iEndValue As Integer, iCountRows As Integer
                    Dim sMissingNos As String = ""

                    dvOriginalExcel = dtOriginalExcel.DefaultView
                    Dim sStrAsc As String = ddlColumns.SelectedItem.Text & " Asc"
                    dvOriginalExcel.Sort = sStrAsc
                    dtTempExcel = dvOriginalExcel.ToTable

                    If dtTempExcel.Rows.Count > 0 Then
                        Try
                            iStartValue = Convert.ToDouble(dtTempExcel.Rows(0)(ddlColumns.SelectedItem.Text))

                            iCountRows = dtTempExcel.Rows.Count
CheckNextMinus:             Try
                                If iCountRows > 0 Then
                                    iEndValue = Convert.ToDouble(dtTempExcel.Rows(iCountRows)(ddlColumns.SelectedItem.Text))
                                Else
                                    lblError.Text = "Not a valid Data." : lblSampleMsg.Text = "Not a valid Data."
                                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                                    Exit Try
                                End If
                            Catch ex As Exception
                                iCountRows = iCountRows - 1
                                GoTo CheckNextMinus
                            End Try

                            If iEndValue > iStartValue Then
                                'iFilter=0
                                For iFilter = iStartValue To iEndValue
                                    Dim dtSort As New DataTable
                                    Dim dvSortExcel As New DataView

                                    dvSortExcel = dtTempExcel.DefaultView
                                    'Dim sStr As String=ddlColumns.SelectedItem.Text & "='" & iFilter & "'"
                                    Dim sStr As String = ddlColumns.SelectedItem.Text & "='" & iFilter & "'"
                                    Dim sStr1 As String = ddlColumns.SelectedItem.Text & "='" & iFilter & "'"
                                    Dim sStr2 As String = ddlColumns.SelectedItem.Text & "='" & iFilter & "'"
                                    Dim sStr3 As String = ddlColumns.SelectedItem.Text & "='" & iFilter & "'"
                                    dvSortExcel.RowFilter = sStr
                                    dtSort = dvSortExcel.ToTable

                                    If dtSort.Rows.Count = 0 Then
                                        sMissingNos = sMissingNos & "," & iFilter & ","
                                    End If
                                Next
                                If sMissingNos.StartsWith(",") Then
                                    sMissingNos = sMissingNos.Remove(0, 1)
                                End If
                                If sMissingNos.EndsWith(",") Then
                                    sMissingNos = sMissingNos.Remove(Len(sMissingNos) - 1, 1)
                                End If
                                If sMissingNos = "" Then
                                    lblError.Text = "There are no missing numbers in the column"
                                    Exit Sub
                                Else
                                    lblMissingNos.Text = "The missing Nos are : " & sMissingNos
                                End If
                            Else
                                lblError.Text = "No Data found"
                            End If
                        Catch ex As Exception
                            lblError.Text = "Not a valid Data." : lblSampleMsg.Text = "Not a valid Data."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                            Exit Try
                        End Try
                    Else
                        lblError.Text = "No Data found"
                    End If

                End If

                If ddlFilter.SelectedValue = 11 Then 'Selected Value
                    If ddlSelValue.SelectedIndex = 0 Then
                        lblError.Text = "Select Value for selected column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "Select Value for selected column  " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        txtFrmVal.Focus()
                        Exit Try
                    End If
                    For iOrginalExcel = 0 To dtOriginalExcel.Rows.Count - 1
                        Try
                            sSelectedValue = dtOriginalExcel.Rows(iOrginalExcel)(ddlColumns.SelectedItem.Text)
                            If (UCase(ddlSelValue.SelectedItem.Text) = UCase(sSelectedValue)) Then
                                drow = dtFilter.NewRow
                                iFilter = 0
                                For j = 0 To aClomuns.Length - 1
                                    If (aClomuns(j) <> "") Then
                                        If aClomuns(j) = "Sl.No" And j = 0 Then
                                            iSlNo = iSlNo + 1
                                            drow(aClomuns(j)) = iSlNo
                                            '   drow(aClomuns(j))=dtFilter.Rows.Count + 1
                                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                            drow(aClomuns(j)) = iOrginalExcel
                                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                            drow(aClomuns(j)) = "Selected Value"
                                        Else
                                            drow(aClomuns(j)) = dtOriginalExcel.Rows(iOrginalExcel)(iFilter)
                                            iFilter = iFilter + 1
                                        End If
                                    End If
                                Next
                                iCheckRow = 1
                                dtFilter.Rows.Add(drow)
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    If iCheckRow = 0 Then
                        lblError.Text = "No Less than or Equal Values for selected Column " & ddlColumns.SelectedItem.Text & "." : lblSampleMsg.Text = "No Less than or Equal Values for selected Coulmn " & ddlColumns.SelectedItem.Text & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                        Exit Try
                    End If
                End If
                dtNSTWithPKID.Merge(dtFilter)
                Dim dtNST As New DataTable
                dtNST = dtNSTWithPKID.Copy()
                If dtNST.Rows.Count > 0 Then
                    dtNST.Columns.Remove("PKID")
                    If dtNST.Rows.Count > 10 Then
                        divNST.Style.Item("Width") = "100%"
                    Else
                        divNST.Style.Item("Width") = "auto"
                    End If
                    dgNST.DataSource = dtNST
                    dgNST.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNSTFilter_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnNSTAddToFinal_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnNSTAddToFinal.Click
        Dim dtNSTFilter As New DataTable
        Dim dvNSTFilter As New DataView
        Dim dvFinal As New DataView
        Dim chkNST As New CheckBox
        Dim i As Integer
        Dim sSlNO As String = ""
        Try
            If dtNSTWithPKID.Rows.Count > 0 Then
                For i = 0 To dgNST.Items.Count - 1
                    chkNST = dgNST.Items(i).FindControl("chkNST")
                    If chkNST.Checked = True Then
                        If sNSTPKID.Contains("," & dtNSTWithPKID.Rows(i)("Sl.No") & ",") = False Then
                            sSlNO = sSlNO & "," & dtNSTWithPKID.Rows(i)("Sl.No")
                        End If
                    End If
                Next
                If sSlNO.StartsWith(",") Then
                    sSlNO = sSlNO.Remove(0, 1)
                End If
                If sSlNO.EndsWith(",") Then
                    sSlNO = sSlNO.Remove(Len(sSlNO) - 1, 1)
                End If
                If sSlNO <> "" Then
                    sNSTPKID = sNSTPKID & "," & sSlNO & ","
                    If sNSTPKID.StartsWith(",") = False Then
                        sNSTPKID = "," & sNSTPKID
                    End If
                    If sNSTPKID.EndsWith(",") = False Then
                        sNSTPKID = sNSTPKID & ","
                    End If

                    dvNSTFilter = dtNSTWithPKID.DefaultView
                    dvNSTFilter.RowFilter = "Sl.No  IN(" & sSlNO & ")"
                    dtNSTFilter = dvNSTFilter.ToTable
                    dtFinalWithPKID.Merge(dtNSTFilter)
                    dvFinal = dtFinalWithPKID.DefaultView

                    Dim dtFinal As New DataTable
                    dtFinal = dtFinalWithPKID.Copy()
                    If dtFinal.Rows.Count > 0 Then
                        dtFinal.Columns.Remove("PKID")
                        For i = 0 To dtFinal.Rows.Count - 1
                            dtFinal.Rows(i)("Sl.No") = i + 1
                        Next
                        dtFinal.AcceptChanges()
                        dgFinalData.DataSource = dtFinal
                        dgFinalData.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnNSTAddToFinal_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSTAddToFinal_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSTAddToFinal.Click
        Dim chkST As New CheckBox
        Dim sSlNO As String = ""
        Dim dvSTFilter As New DataView
        Dim dtSTFilter As New DataTable
        Try
            If dtSTWithPKID.Rows.Count > 0 Then
                For i = 0 To dgST.Items.Count - 1
                    chkST = dgST.Items(i).FindControl("chkST")
                    If chkST.Checked = True Then
                        If sSTPKID.Contains("," & dgST.Items(i).Cells(1).Text & ",") = False Then
                            sSlNO = sSlNO & "," & dgST.Items(i).Cells(1).Text
                        End If
                    End If
                Next
                If sSlNO.StartsWith(",") Then
                    sSlNO = sSlNO.Remove(0, 1)
                End If
                If sSlNO.EndsWith(",") Then
                    sSlNO = sSlNO.Remove(Len(sSlNO) - 1, 1)
                End If
                If sSlNO <> "" Then
                    sSTPKID = sSTPKID & "," & sSlNO & ","
                    If sSTPKID.StartsWith(",") = False Then
                        sSTPKID = "," & sSTPKID
                    End If
                    If sSTPKID.EndsWith(",") = False Then
                        sSTPKID = sSTPKID & ","
                    End If

                    dvSTFilter = dtSTWithPKID.DefaultView
                    dvSTFilter.RowFilter = "Sl.No IN(" & sSlNO & ")"
                    dtSTFilter = dvSTFilter.ToTable
                    dtFinalWithPKID.Merge(dtSTFilter)

                    Dim dtFinal As New DataTable
                    dtFinal = dtFinalWithPKID.Copy()
                    If dtFinal.Rows.Count > 0 Then
                        dtFinal.Columns.Remove("PKID")
                        dgFinalData.DataSource = dtFinal
                        dgFinalData.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSTAddToFinal_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlColumns_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlColumns.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlFilter.SelectedIndex > 0 Then
                dgNST.DataSource = Nothing
                dgNST.DataBind()
            End If
            ddlFilter.SelectedIndex = 0
            ddlFilter_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlColumns_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub rboSystematic_CheckedChanged(sender As Object, e As EventArgs) Handles rboSystematic.CheckedChanged
        Try
            lblError.Text = ""
            If rboSystematic.Checked = True Then
                If sSave = "YES" Then
                    imgbtnSTFilter.Visible = True
                End If
                txtRows.Visible = True
                lblDesc1.Text = "Select Every "
                lblDesc2.Text = "th/rd row from the Excel."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboSystematic_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub rboSatisfied_CheckedChanged(sender As Object, e As EventArgs) Handles rboSatisfied.CheckedChanged
        Try
            lblError.Text = ""
            If rboSatisfied.Checked = True Then
                If sSave = "YES" Then
                    imgbtnSTFilter.Visible = True
                End If
                txtRows.Visible = True
                lblDesc1.Text = "Select "
                lblDesc2.Text = "% of rows from total no of rows."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboSatisfied_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnSTFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles imgbtnSTFilter.Click
        Try
            If rboSatisfied.Checked = False And rboSystematic.Checked = False Then
                lblError.Text = "Select any one of the Sampling method." : lblSampleMsg.Text = "Select any one of the Sampling method."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                Exit Try
            End If
            If rboSatisfied.Checked = True Or rboSystematic.Checked = True Then
                If txtRows.Text = "" Then
                    lblError.Text = "Enter Rows." : lblSampleMsg.Text = "Enter Rows."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                    Exit Try
                End If
                If IsNumeric(txtRows.Text) = False Then
                    lblError.Text = "Enter the valid Rows."
                    txtRows.Focus()
                    Exit Sub
                End If
            End If
            If rboSystematic.Checked = True Then
                LoadSystematicInterval()
            ElseIf rboSatisfied.Checked = True Then
                LoadSatisfiedInterval()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSTFilter" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadSystematicInterval()
        Dim j As Integer, iCheck As Integer, iExcelCount As Integer, iSlNo As Integer = 0, iFilter As Integer = 0
        Dim dvRows As New DataView
        Dim sRows As String = ""
        Dim txtValue As New TextBox
        Dim drow As DataRow
        Dim aRows As String(), aClomuns As String(), sColumnNames As String = ""
        Try
            'Dim dVoriginalExcel As New DataView(dtOriginalExcel)
            'dVoriginalExcel.Sort=dtOriginalExcel.Columns(0).ColumnName & " ASC"
            'dtExcel=dVoriginalExcel.ToTable()
            dtSTWithPKID = New DataTable
            sColumnNames = "Sl.No,PKID,FilterTypes"
            dtSTWithPKID.Columns.Add("Sl.No")
            dtSTWithPKID.Columns.Add("PKID")
            dtSTWithPKID.Columns.Add("FilterTypes")
            For iAllColumns = 0 To dtExcelColumns.Rows.Count - 1
                dtSTWithPKID.Columns.Add(dtExcelColumns.Rows(iAllColumns)(1).ToString())
                sColumnNames = sColumnNames & "," & dtExcelColumns.Rows(iAllColumns)(1).ToString()
            Next
            aClomuns = sColumnNames.Split(",")

            iCheck = Val(txtRows.Text)
            iExcelCount = dtOriginalExcel.Rows.Count - 1
            txtValue.Text = Val(txtRows.Text)

Check:      If iCheck < iExcelCount Then
                sRows = sRows & "," & iCheck
                iCheck = iCheck + Val(txtValue.Text)
                GoTo Check
            End If

            If sRows.StartsWith(",") Then
                sRows = sRows.Remove(0, 1)
            End If
            If sRows.EndsWith(",") Then
                sRows = sRows.Remove(Len(sRows) - 1, 1)
            End If
            aRows = sRows.Split(",")

            For iSIExcel = 0 To aRows.Length - 1
                drow = dtSTWithPKID.NewRow
                iFilter = 0
                For j = 0 To aClomuns.Length - 1
                    If (aClomuns(j) <> "") Then
                        If aClomuns(j) = "Sl.No" And j = 0 Then
                            iSlNo = iSlNo + 1
                            drow(aClomuns(j)) = iSlNo
                        ElseIf aClomuns(j) = "PKID" And j = 1 Then
                            drow(aClomuns(j)) = aRows(iSIExcel)
                        ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                            drow(aClomuns(j)) = "Systematic Interval"
                        Else
                            drow(aClomuns(j)) = dtOriginalExcel.Rows(aRows(iSIExcel))(iFilter)
                            iFilter = iFilter + 1
                        End If
                    End If
                Next
                dtSTWithPKID.Rows.Add(drow)
            Next

            If dtSTWithPKID.Rows.Count > 0 Then
                If dtSTWithPKID.Rows.Count > 10 Then
                    divST.Style.Item("Width") = "100%"
                Else
                    divST.Style.Item("Width") = "auto"
                End If
                dgST.DataSource = dtSTWithPKID
                dgST.DataBind()
            Else
                lblError.Text = "No Rows to Display." : lblSampleMsg.Text = "No Rows to Display."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                Exit Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSystematicInterval" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub LoadSatisfiedInterval()
        Dim ioriginalExcelCount As Integer, iRows As Integer, istart As Integer = 0, iEnd As Integer = 0, newRows As Integer
        Try
            iRows = txtRows.Text
            ioriginalExcelCount = dtOriginalExcel.Rows.Count
            newRows = ((ioriginalExcelCount * iRows) / 100)
            newRows = Math.Round(newRows)
            iEnd = ioriginalExcelCount
            LoadRandomVal(newRows, istart, iEnd, "ST")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSatisfiedInterval" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Public Sub LoadRandomVal(ByVal count As Integer, ByVal istart As Integer, ByVal iEnd As Integer, ByVal sType As String)
        Dim i As Integer, iSlNo As Integer, iFilter As Integer = 0
        Dim dtRN As New DataTable, dtFilter As New DataTable
        Dim sColumnNames As String
        Dim aClomuns As String()
        Dim drow As DataRow
        Try
            Dim iDiff As Double
            iDiff = Val(iEnd) - Val(istart)
            If iDiff >= Val(count) Then
                Dim UB, LB As Double
                Dim S, M As String
                LB = Val(istart)
                UB = Val(iEnd)
                S = ";" : M = ""
                For i = 0 To Val(count) - 1
                    Randomize()
                    M = CInt(Int((UB - LB + 1) * Rnd()) + LB)
                    M = ";" & M & ";"
                    If InStr(S, M, CompareMethod.Text) <> 0 Then     'to check the random number is previously generated or not
                        i = i - 1  ' the variable 'i' idicates number of random umbers generated
                    Else
                        Dim spltm() As String
                        spltm = Split(M, ";")
                        M = spltm(1) & ";"
                        S = S & M  ' add the random number to set of random numbers
                    End If
                Next
                S = Right(S, S.Length() - 1)
                Dim myArray() As String
                myArray = Split(S, ";")  ' devide the random numbers in the form of string into array
                Dim j, temp As Integer
                For i = 0 To UBound(myArray)  ' to display the random numbers in the order of Ascending
                    For j = 0 To UBound(myArray) - 1
                        If (Val(myArray(j)) > Val(myArray(j + 1))) Then
                            temp = myArray(j)
                            myArray(j) = myArray(j + 1)
                            myArray(j + 1) = temp
                        End If
                    Next
                Next

                sColumnNames = "Sl.No,PKID,FilterTypes"
                dtFilter.Columns.Add("Sl.No")
                dtFilter.Columns.Add("PKID")
                dtFilter.Columns.Add("FilterTypes")

                For iAllColumns = 0 To dtExcelColumns.Rows.Count - 1
                    dtFilter.Columns.Add(dtExcelColumns.Rows(iAllColumns)(1).ToString())
                    sColumnNames = sColumnNames & "," & dtExcelColumns.Rows(iAllColumns)(1).ToString()
                Next
                aClomuns = sColumnNames.Split(",")

                Try
                    For i = 0 To UBound(myArray)
                        If myArray(i) <> String.Empty Then
                            drow = dtFilter.NewRow
                            iFilter = 0
                            For j = 0 To aClomuns.Length - 1
                                If (aClomuns(j) <> "") Then
                                    If aClomuns(j) = "Sl.No" And j = 0 Then
                                        iSlNo = iSlNo + 1
                                        drow(aClomuns(j)) = iSlNo
                                    ElseIf aClomuns(j) = "PKID" And j = 1 Then
                                        drow(aClomuns(j)) = myArray(i)
                                    ElseIf aClomuns(j) = "FilterTypes" And j = 2 Then
                                        If sType = "ST" Then
                                            drow(aClomuns(j)) = "Stratified Sampling"
                                        Else
                                            drow(aClomuns(j)) = "Random Rows"
                                        End If
                                    Else
                                        drow(aClomuns(j)) = dtOriginalExcel.Rows(myArray(i))(iFilter)
                                        iFilter = iFilter + 1
                                    End If
                                End If
                            Next
                            dtFilter.Rows.Add(drow)
                        End If
                    Next
                Catch ex As Exception
                End Try

                If sType = "ST" Then
                    dtSTWithPKID = dtFilter
                    If dtSTWithPKID.Rows.Count > 10 Then
                        divST.Style.Item("Width") = "100%"
                    Else
                        divST.Style.Item("Width") = "auto"
                    End If
                    dgST.DataSource = dtSTWithPKID
                    dgST.DataBind()
                ElseIf sType = "RR" Then
                    dtRRWithPKID = dtFilter
                    If dtRRWithPKID.Rows.Count > 10 Then
                        divRandom.Style.Item("Width") = "100%"
                    Else
                        divRandom.Style.Item("Width") = "auto"
                    End If
                    dgRandom.DataSource = dtRRWithPKID
                    dgRandom.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadRandomVal" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnRR_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRR.Click
        Try
            dgRandom.DataSource = Nothing
            dgRandom.DataBind()
            If IsNumeric(txtSS.Text) = False Then
                lblError.Text = "Enter Valid Sample Size." : lblSampleMsg.Text = "Enter Valid Sample Size."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                txtSS.Focus()
                Exit Sub
            End If
            If IsNumeric(txtFrom.Text) = False Then
                lblError.Text = "Enter From Value." : lblSampleMsg.Text = "Enter From Value."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                txtFrom.Focus()
                Exit Sub
            End If
            If IsNumeric(txtRRTO.Text) = False Then
                lblError.Text = "Enter To value." : lblSampleMsg.Text = "Enter To value."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                txtRRTO.Focus()
                Exit Sub
            End If
            If Val(txtFrom.Text) >= Val(txtRRTO.Text) Then
                lblError.Text = "From Value should not be greater than To value." : lblSampleMsg.Text = "From Value should not be greater than To value."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaISS').modal('show');", True)
                txtFrom.Focus()
                Exit Sub
            End If
            LoadRandomVal(txtSS.Text, txtFrom.Text, txtRRTO.Text, "RR")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRR_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnRRFilter_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRRFilter.Click
        Dim chkRR As New CheckBox
        Dim sSlNO As String = ""
        Dim dvRRFilter As New DataView
        Dim dtRRFilter As New DataTable
        Try
            If dtRRWithPKID.Rows.Count > 0 Then
                For i = 0 To dgRandom.Items.Count - 1
                    chkRR = dgRandom.Items(i).FindControl("chkRR")
                    If chkRR.Checked = True Then
                        If sRRPKID.Contains("," & dgRandom.Items(i).Cells(1).Text & ",") = False Then
                            sSlNO = sSlNO & "," & dgRandom.Items(i).Cells(1).Text
                        End If
                    End If
                Next
                If sSlNO.StartsWith(",") Then
                    sSlNO = sSlNO.Remove(0, 1)
                End If
                If sSlNO.EndsWith(",") Then
                    sSlNO = sSlNO.Remove(Len(sSlNO) - 1, 1)
                End If
                If sSlNO <> "" Then
                    sRRPKID = sRRPKID & "," & sSlNO & ","
                    If sRRPKID.StartsWith(",") = False Then
                        sRRPKID = "," & sRRPKID
                    End If
                    If sRRPKID.EndsWith(",") = False Then
                        sRRPKID = sRRPKID & ","
                    End If

                    dvRRFilter = dtRRWithPKID.DefaultView
                    dvRRFilter.RowFilter = "Sl.No IN(" & sSlNO & ")"
                    dtRRFilter = dvRRFilter.ToTable
                    dtFinalWithPKID.Merge(dtRRFilter)

                    Dim dtFinal As New DataTable
                    dtFinal = dtFinalWithPKID.Copy()
                    If dtFinal.Rows.Count > 0 Then
                        dtFinal.Columns.Remove("PKID")
                        If dtFinal.Rows.Count > 10 Then
                            divFinalData.Style.Item("Width") = "100%"
                        Else
                            divFinalData.Style.Item("Width") = "auto"
                        End If
                        dgFinalData.DataSource = dtFinal
                        dgFinalData.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRRFilter_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnFinalSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnFinalSave.Click
        Try
            If dgFinalData.Items.Count > 0 Then
                If dtFinalWithPKID.Rows.Count > 0 Then
                    ExportoExcelandAttach(dtFinalWithPKID)
                End If
                LoadExcelSheetDetails()
                ddlExcelSheet_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnFinalSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub dgAttach_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgAttach.ItemCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = ""
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = e.Item.FindControl("lblAtchDocID")
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iSSAtchID, iDocID)
                DownloadMyFile(sDestFilePath)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgAttach_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub ExportoExcelandAttach(ByVal dt1 As DataTable)
        Dim excel As New Microsoft.Office.Interop.Excel.Application
        Dim wBook As Microsoft.Office.Interop.Excel.Workbook
        Dim wSheet As Microsoft.Office.Interop.Excel.Worksheet
        Dim dt As System.Data.DataTable
        Dim dr As System.Data.DataRow
        Dim colIndex As Integer = 0, rowIndex As Integer = 0
        Dim sPath As String, strFileNameFullPath As String, strFileNamePath As String
        Dim i As Integer
        Dim sTaskCode As String
        Try
            If dt1.Rows.Count > 0 Then
                dt = dt1
                sPath = Server.MapPath("../") & "ExcelUploads\Excel.xlsx"
                wBook = excel.Workbooks.Add(sPath)
                wSheet = wBook.ActiveSheet()
                For i = 0 To dt.Columns.Count - 1
                    colIndex = colIndex + 1
                    excel.Cells(1, colIndex) = dt.Columns(i).ColumnName
                    excel.Cells(1, colIndex).Font.Bold = True
                Next
                'Add Observation cloumn
                excel.Cells(1, dt.Columns.Count + 1) = "Observation"
                excel.Cells(1, dt.Columns.Count + 1).Font.Bold = True

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
                sTaskCode = objclsSampling.GetTaskCode(sSession.AccessCode, sSession.AccessCodeID, iAuditID)
                Dim iCount As Integer = 0
                If dgAttach.Items.Count > 0 Then
                    iCount = iCount + dgAttach.Items.Count
                    strFileNameFullPath = strFileNamePath & "\" & Replace(sTaskCode, "/", "-") & "-" & (iCount + 1) & ".xlsx"
                Else
                    strFileNameFullPath = strFileNamePath & "\" & Replace(sTaskCode, "/", "-") & "-" & iCount & ".xlsx"
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
                ' DownloadFile(strFileNameFullPath)

                'Attach
                If System.IO.File.Exists(strFileNameFullPath) = True Then
                    If iSSAtchID = 0 Then
                        iSSAtchID = objclsAttachments.SaveSamplingAttachments(sSession.AccessCode, sSession.AccessCodeID, strFileNameFullPath, sSession.UserID, iSSAtchID, "From Sampling")
                        objclsSampling.SaveSamplingAttachment(sSession.AccessCode, sSession.AccessCodeID, iSSAtchID, iAuditID, iCheckPointID)
                        Dim iCAAttachID As Integer = objclsStandardAudit.GetConductAuditAttachmentID(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointID)
                        Dim iDRLAttachID As Integer = objclsStandardAudit.GetDRLAttachmentID(sSession.AccessCode, sSession.AccessCodeID, iAuditID, iCheckPointID)
                        If iCAAttachID = 0 Or iDRLAttachID = 0 Then
                            objclsStandardAudit.SaveDRLConductAuditAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iSSAtchID, iAuditID, iCheckPointID, "CA")
                            objclsStandardAudit.SaveDRLConductAuditAttachmentInAudit(sSession.AccessCode, sSession.AccessCodeID, iSSAtchID, iAuditID, iCheckPointID, "DRL")
                        Else
                            objclsAttachments.SaveSamplingAttachments(sSession.AccessCode, sSession.AccessCodeID, strFileNameFullPath, sSession.UserID, iCAAttachID, "From Sampling")
                        End If
                    Else
                        iSSAtchID = objclsAttachments.SaveSamplingAttachments(sSession.AccessCode, sSession.AccessCodeID, strFileNameFullPath, sSession.UserID, iSSAtchID, "From Sampling")
                    End If
                    If iSSAtchID > 0 Then
                        BindSamplingAttachments(iAuditID, iSSAtchID)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExportoExcelandAttach" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindSamplingAttachments(ByVal iAuditID As Integer, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            dgAttach.CurrentPageIndex = 0
            dgAttach.PageSize = 1000
            ds = objclsAttachments.LoadSamplingAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID, iAuditID)
            If ds.Tables(0).Rows.Count > dgAttach.PageSize Then
                dgAttach.AllowPaging = True
            Else
                dgAttach.AllowPaging = False
            End If
            dgAttach.DataSource = ds
            dgAttach.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSamplingAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadFile" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnCheckDuplicate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCheckDuplicate.Click
        Dim dvDuplicate As New DataView
        Dim i As Integer, j As Integer, k As Integer
        Dim sPKIDs As String = "", sFilterType As String = ""
        Dim aPPKID() As String
        Dim dtDuplicateTemp As New DataTable, dtDuplicate As New DataTable
        Try
            If dtFinalWithPKID.Rows.Count > 0 Then
                dtDuplicate = dtFinalWithPKID

                For i = 0 To dtFinalWithPKID.Rows.Count - 1
                    If sPKIDs.Contains("," & dtFinalWithPKID.Rows(i)("PKID") & ",") = False Then
                        sPKIDs = sPKIDs & "," & dtFinalWithPKID.Rows(i)("PKID") & ","
                    End If
                Next
                dtFinalWithPKID = Nothing
                If sPKIDs.Contains(",,") = True Then
                    sPKIDs = sPKIDs.Replace(",,", ",")
                End If

                aPPKID = sPKIDs.Split(",")
                For j = 0 To UBound(aPPKID) - 1
                    dvDuplicate = dtDuplicate.DefaultView
                    If aPPKID(j) <> "" Then
                        dvDuplicate.RowFilter = "PKID=" & aPPKID(j) & ""
                        dtDuplicateTemp = dvDuplicate.ToTable


                        If dtDuplicateTemp.Rows.Count > 1 Then
                            For k = 0 To dtDuplicateTemp.Rows.Count - 1
                                sFilterType = sFilterType & "," & dtDuplicateTemp.Rows(k)("FilterTypes")
                            Next
                            If sFilterType.StartsWith(",") Then
                                sFilterType = sFilterType.Remove(0, 1)
                            End If
                            If sFilterType.EndsWith(",") Then
                                sFilterType = sFilterType.Remove(Len(sFilterType) - 1, 1)
                            End If
                            dtDuplicateTemp.Rows(0)("FilterTypes") = sFilterType
                            dtDuplicateTemp.AcceptChanges()
                            If dtDuplicateTemp.Rows.Count > 1 Then
                                dtDuplicateTemp.Rows(1).Delete()
                                dtDuplicateTemp.AcceptChanges()
                            End If
                        End If
                        If IsNothing(dtFinalWithPKID) = True Then
                            dtFinalWithPKID = dtDuplicateTemp.Copy()
                        Else
                            If dtFinalWithPKID.Rows.Count = 0 Then
                                dtFinalWithPKID = dtDuplicateTemp.Copy()
                            Else
                                dtFinalWithPKID.Merge(dtDuplicateTemp)
                            End If
                        End If
                    End If
                Next
                Dim dtFinalWithOutPKID As New DataTable
                dtFinalWithOutPKID = dtFinalWithPKID.Copy()
                If dtFinalWithOutPKID.Rows.Count > 0 Then
                    dtFinalWithOutPKID.Columns.Remove("PKID")
                    dgFinalData.DataSource = dtFinalWithOutPKID
                    dgFinalData.DataBind()
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCheckDuplicate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class