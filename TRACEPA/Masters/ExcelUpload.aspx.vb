Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Partial Class ExcelUpload
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_ExcelUploads"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsYearMaster As New clsHolidayMaster
    Private objclsExcelUpload As New clsExcelUpload
    Private objclsControlLibrary As New clsControlLibrary
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsHolidayMaster As New clsHolidayMaster
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsOrgStructure As New clsOrgStructure
    Private objCust As New clsCustDetails
    Private objclsCustomerMaster As New clsCustomerMaster
    Private objclsEProfile As New clsEProfile

    Private Shared sSession As AllSession
    Private Shared sMasterStatus As String
    'Private Shared sExcelSave As String
    Private Shared sFile As String
    Private Shared iFormID As Integer = 0

    ' 1 = Organisation Structure
    ' 2 = Holiday Master
    ' 3 = Employee Master
    ' 4 = Customer Master
    ' 5 = User Master
    ' 6 = Service & Conveyance Master
    ' 7 = Audit Universe
    ' 8 = Risk General Master
    ' 9 = Risk Master
    '10 = Control Master
    '11 = General Master
    '12 = Mapping of Master
    '13 = Work Paper Upload
    '14 = Trial Balance
    '15 = Branch Checklist
    '16 = Audit Checklist
    '17 = Risk Checklist

    'Risk
    '18 = Risk Review Function Issue Tracker
    '19 = Key Control Check Issue Tracker
    '20 = Key Risk Indicators
    '21 = Key Investigation Risk Tracker
    '22 = BRR Issue Tracker
    '23 = BRR Checklist
    '24 = RCSA
    '25 = RA

    'Compliance
    '26= Compliance Issue Tracker
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
        If Request.QueryString("FormID") IsNot Nothing Then
            iFormID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FormID")))
            If iFormID >= 13 And iFormID <= 14 And iFormID >= 18 Then
                If iFormID = 13 And iFormID = 14 Then
                    Me.MasterPageFile = "~/Audit.master"
                ElseIf iFormID = 18 Or iFormID = 19 Or iFormID = 20 Or iFormID = 21 Or iFormID = 22 Or iFormID = 23 Or iFormID = 24 Or iFormID = 25 Then
                    Me.MasterPageFile = "~/Risk.master"
                ElseIf iFormID = 26 Then
                    Me.MasterPageFile = "~/Compliance.master"
                End If
                sMasterStatus = "ALL"
            End If
        Else
            sMasterStatus = "A"
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iFormID As Integer = 0
        'Dim sFormButtons As String
        Dim dtSampleFormat As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                FULoad.Visible = False : divcollapseRRIT.Visible = False : lnkDownload.Visible = False
                lblSelectFile.Visible = False : lblSheetName.Visible = False : btnOk.Visible = False : ddlSheetName.Visible = False
                imgbtnBack.Visible = False

                lblCustName.Visible = False : ddlCustName.Visible = False

                'If Request.QueryString("FormID") IsNot Nothing Then
                '    iFormID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FormID")))
                '    If iFormID > 0 Then
                '        sExcelSave = "YES"
                '    End If
                'Else
                '    sExcelSave = "NO"
                '    sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MSEU", 1)
                '    If sFormButtons = "False" Or sFormButtons = "" Then
                '        Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '        Exit Sub
                '    Else
                '        If sFormButtons.Contains(",Save,") = True Then
                '            sExcelSave = "YES"
                '        End If
                '    End If
                'End If
                LoadMasters()
                    If Request.QueryString("FormID") IsNot Nothing Then
                        iFormID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FormID")))
                        If iFormID = 13 Then
                            ddlMasterName.SelectedValue = 13
                            ddlMasterName_SelectedIndexChanged(sender, e)
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 13)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 18 Then
                            ddlMasterName.SelectedValue = 18
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 18)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 19 Then
                            ddlMasterName.SelectedValue = 19
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 19)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 20 Then
                            ddlMasterName.SelectedValue = 20
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 20)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 21 Then
                            ddlMasterName.SelectedValue = 21
                        End If
                        If iFormID = 22 Then
                            ddlMasterName.SelectedValue = 22
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 22)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 23 Then
                            ddlMasterName.SelectedValue = 23
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 23)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 24 Then
                            ddlMasterName.SelectedValue = 24
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 24)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 25 Then
                            ddlMasterName.SelectedValue = 25
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 25)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        If iFormID = 26 Then
                            ddlMasterName.SelectedValue = 26
                            dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, 26)
                            dgSampleFormat.DataSource = dtSampleFormat
                            dgSampleFormat.DataBind()
                        End If
                        ddlMasterName.Enabled = False
                        FULoad.Enabled = True : FULoad.Visible = True : btnOk.Visible = True : lblSelectFile.Visible = True
                        divcollapseRRIT.Visible = True : lnkDownload.Visible = True
                    End If
                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadMasters()
        Try
            ddlMasterName.DataSource = objclsExcelUpload.LoadExcelMasters(sSession.AccessCode, sSession.AccessCodeID, sMasterStatus)
            ddlMasterName.DataTextField = "GEU_MasterName"
            ddlMasterName.DataValueField = "GEU_Pk_Id"
            ddlMasterName.DataBind()
            ddlMasterName.Items.Insert(0, New ListItem("Select Master Type", "0"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadMasters" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExcelSheetNames" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MSAccessOpenConnection" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub LoadExistingCustomer()
        Try
            ddlCustName.DataSource = objCust.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataTextField = "Cust_Name"
            ddlCustName.DataValueField = "Cust_Id"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlMasterName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMasterName.SelectedIndexChanged
        Dim dtSampleFormat As New DataTable
        Try
            lblError.Text = ""
            ddlSheetName.Items.Clear()
            dgSampleFormat.DataSource = Nothing
            dgSampleFormat.DataBind()
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            FULoad.Visible = False : divcollapseRRIT.Visible = False : lnkDownload.Visible = False : imgbtnSave.Visible = False
            lblSelectFile.Visible = False : lblSheetName.Visible = False : btnOk.Visible = False : ddlSheetName.Visible = False
            lblCustName.Visible = False : ddlCustName.Visible = False
            If ddlMasterName.SelectedValue > 0 Then
                If ddlMasterName.SelectedValue = 14 Then
                    lblCustName.Visible = True : ddlCustName.Visible = True
                    LoadExistingCustomer()
                End If
                If ddlMasterName.SelectedValue <> 21 Then
                    FULoad.Visible = True : divcollapseRRIT.Visible = True : lnkDownload.Visible = True
                    lblSelectFile.Visible = True : btnOk.Visible = True
                    dtSampleFormat = objclsExcelUpload.LoadAllFields(sSession.AccessCode, sSession.AccessCodeID, ddlMasterName.SelectedValue)
                    dgSampleFormat.DataSource = dtSampleFormat
                    dgSampleFormat.DataBind()
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toggle", "$('#collapseRRIT').collapse('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMasterName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = False
            If FULoad.FileName <> String.Empty Then
                lblSheetName.Visible = True : ddlSheetName.Visible = True
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")

        End Try
    End Sub
    Protected Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dttable As New DataTable
        Dim sStr As String = "", sYear As String
        Dim iYearID As Integer, iCheckMasterCounts As Integer = 0
        Try
            lblError.Text = ""
            dgGeneral.DataSource = Nothing
            dgGeneral.DataBind()
            dgGeneral.Visible = False
            imgbtnSave.Visible = False
            If ddlSheetName.SelectedIndex > 0 Then
                If ddlMasterName.SelectedValue = 1 Then 'Organisation Structure
                    dttable = LoadOrganisationStructureDetails(sFile)
                End If
                If ddlMasterName.SelectedValue = 2 Then 'Holiday Master
                    dttable = LoadHolidayDetails(sFile)
                End If
                If ddlMasterName.SelectedValue = 3 Then 'Employee Structure
                    dttable = LoadEmployeeDetails(sFile)
                End If
                If ddlMasterName.SelectedValue = 4 Then 'Customer Structure                   
                    dttable = LoadCustomerMasters(sFile)
                End If
                If ddlMasterName.SelectedValue = 5 Then 'Customer User Master
                    dttable = LoadCustomerUserDetails(sFile)
                End If
                If ddlMasterName.SelectedValue = 6 Then 'Service & Conveyance Master
                End If
                If ddlMasterName.SelectedValue = 7 Then 'Audit Universe
                End If
                If ddlMasterName.SelectedValue = 8 Then 'Risk General Master
                End If
                If ddlMasterName.SelectedValue = 9 Then 'Risk Master
                End If
                If ddlMasterName.SelectedValue = 10 Then 'Control Master
                End If
                If ddlMasterName.SelectedValue = 11 Then 'General Master
                    dttable = LoadGeneralMaster(sFile)
                End If
                If ddlMasterName.SelectedValue = 12 Then 'Mapping of Master
                End If
                If ddlMasterName.SelectedValue = 13 Then 'Work Paper
                End If
                If ddlMasterName.SelectedValue = 14 Then 'Trial Balance
                End If
                If ddlMasterName.SelectedValue = 15 Then
                    iYearID = objclsExcelUpload.CheckAuditCheckListYearIDExists(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "BCM")
                    If iYearID > 0 Then
                        sYear = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iYearID)
                        lblError.Text = "CheckList Masters has been uploaded for '" & sYear & "'." : lblExcelValidationMsg.Text = "CheckList Masters has been uploaded for '" & sYear & "'."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                    dttable = LoadBranchExcelTable(sFile)
                End If
                If ddlMasterName.SelectedValue = 16 Then
                    iYearID = objclsExcelUpload.CheckAuditCheckListYearIDExists(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "BIA")
                    If iYearID > 0 Then
                        sYear = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iYearID)
                        lblError.Text = "CheckList Masters has been uploaded for '" & sYear & "'." : lblExcelValidationMsg.Text = "CheckList Masters has been uploaded for '" & sYear & "'."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                    dttable = LoadBranchExcelTable(sFile)
                End If
                If ddlMasterName.SelectedValue = 17 Then
                    iYearID = objclsExcelUpload.CheckAuditCheckListYearIDExists(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, "BRR")
                    If iYearID > 0 Then
                        sYear = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iYearID)
                        lblError.Text = "CheckList Masters has been uploaded for '" & sYear & "'." : lblExcelValidationMsg.Text = "CheckList Masters has been uploaded for '" & sYear & "'."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                    dttable = LoadBranchExcelTable(sFile)
                End If
                If ddlMasterName.SelectedValue = 18 Or ddlMasterName.SelectedValue = 19 Then
                End If
                If ddlMasterName.SelectedValue = 20 Then
                End If
                If ddlMasterName.SelectedValue = 21 Then
                End If
                If ddlMasterName.SelectedValue = 22 Then
                End If
                If ddlMasterName.SelectedValue = 23 Then
                    'check
                    iCheckMasterCounts = objclsExcelUpload.GetRiskMasterCheckCount(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                    If iCheckMasterCounts = 0 Then
                        sYear = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)
                        lblExcelValidationMsg.Text = "Upload Risk Checklist for the year " & sYear & "." : lblError.Text = "Upload Risk Checklist for the year " & sYear & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    ElseIf dttable.Rows.Count <> iCheckMasterCounts Then
                        lblExcelValidationMsg.Text = "Number of checkpoints displayed in the grid " & (dttable.Rows.Count) & "does not tally with total number Of checkpoints in the master " & (iCheckMasterCounts) & "." : lblError.Text = "Number of checkpoints displayed in the grid " & (dttable.Rows.Count) & "does not tally with total number Of checkpoints in the master " & (iCheckMasterCounts) & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    End If
                End If
                If ddlMasterName.SelectedValue = 24 Then
                End If
                If ddlMasterName.SelectedValue = 25 Then
                End If
                If ddlMasterName.SelectedValue = 26 Then
                End If
                If ddlMasterName.SelectedValue = 29 Then
                    dttable = LoadComplianceTask(sFile)
                End If
                If ddlMasterName.SelectedValue = 31 Then
                    dttable = LoadAuditChecklistMaster(sFile)
                End If
                If IsNothing(dttable) Then
                    ddlSheetName.SelectedIndex = 0
                    lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format in selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf dttable.Rows.Count = 0 Then
                    lblError.Text = "No Data." : lblExcelValidationMsg.Text = "No Data."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'If sExcelSave = "YES" Then
                imgbtnSave.Visible = True
                'End If
                dgGeneral.DataSource = dttable
                dgGeneral.DataBind()
                dgGeneral.Visible = True
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadFile" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    '1 Organisation Structure
    Private Function LoadOrganisationStructureDetails(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Zone")
            dtTable.Columns.Add("Zone Code")
            dtTable.Columns.Add("Region")
            dtTable.Columns.Add("Region Code")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("Area Code")
            dtTable.Columns.Add("Branch")
            dtTable.Columns.Add("Branch Code")
            dtTable.Columns.Add("Note/Address/Remarks")
            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Zone") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Zone Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Region") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("Region Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("Area") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("Area Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                    If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("Branch") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                    If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                        dRow("Branch Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                    If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
                        dRow("Note/Address/Remarks") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(8))
                    End If
                End If
                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadOrganisationStructureDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    '2 Holiday Master 
    Private Function LoadHolidayDetails(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Financial Year")
            dtTable.Columns.Add("Date")
            dtTable.Columns.Add("Occasion")
            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Financial Year") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Date") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Occasion") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadHolidayDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    '3 Employee Master
    'Private Function LoadEmployeeDetails(ByVal sFile As String) As DataTable
    '    Dim dtTable As New DataTable, dtStock As New DataTable
    '    Dim objDBL As New DBHelper
    '    Dim dRow As DataRow
    '    Dim i As Integer
    '    Dim sCommodity As String = ""
    '    Try
    '        dtTable.Columns.Add("Sr.No")
    '        dtTable.Columns.Add("Zone")
    '        dtTable.Columns.Add("Region")
    '        dtTable.Columns.Add("Area")
    '        dtTable.Columns.Add("Branch")
    '        dtTable.Columns.Add("EMP Code")
    '        dtTable.Columns.Add("Employee Name")
    '        dtTable.Columns.Add("Login Name")
    '        dtTable.Columns.Add("E-Mail")
    '        dtTable.Columns.Add("Office Phone No")
    '        dtTable.Columns.Add("Designation")
    '        dtTable.Columns.Add("Role")
    '        dtTable.Columns.Add("Module")
    '        dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
    '        If IsNothing(dtStock) = True Then
    '            lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '            ddlSheetName.Items.Clear()
    '            Return dtStock
    '        End If

    '        For i = 0 To dtStock.Rows.Count - 1
    '            dRow = dtTable.NewRow
    '            dRow("Sr.No") = i + 1
    '            dRow("Zone") = "" : dRow("Region") = "" : dRow("Area") = "" : dRow("Branch") = ""
    '            dRow("EMP Code") = "" : dRow("Employee Name") = "" : dRow("Login Name") = ""
    '            dRow("E-Mail") = "" : dRow("Office Phone No") = "" : dRow("Designation") = ""
    '            dRow("Role") = "" : dRow("Module") = ""
    '            If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
    '                If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                    dRow("Zone") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
    '                If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
    '                    dRow("Region") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
    '                If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
    '                    dRow("Area") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
    '                If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
    '                    dRow("Branch") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
    '                End If
    '            End If

    '            If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
    '                If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
    '                    dRow("EMP Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
    '                If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
    '                    dRow("Employee Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
    '                If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
    '                    dRow("Login Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
    '                If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
    '                    dRow("E-Mail") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
    '                If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
    '                    dRow("Office Phone No") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(8))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
    '                If dtStock.Rows(i).Item(9).ToString <> "&nbsp;" Then
    '                    dRow("Designation") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(9))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
    '                If dtStock.Rows(i).Item(10).ToString <> "&nbsp;" Then
    '                    dRow("Role") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(10))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
    '                If dtStock.Rows(i).Item(11).ToString <> "&nbsp;" Then
    '                    dRow("Module") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(11))
    '                End If
    '            End If
    '            dtTable.Rows.Add(dRow)
    '        Next
    '        Return dtTable
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadEmployeeDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Function

    '3 Employee Master  '' Change by Steffi on 01-01-2024
    Private Function LoadEmployeeDetails(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("EMP Code")
            dtTable.Columns.Add("Employee Name")
            dtTable.Columns.Add("Login Name")
            dtTable.Columns.Add("E-Mail")
            dtTable.Columns.Add("Office Phone No")
            dtTable.Columns.Add("Designation")
            dtTable.Columns.Add("Role")
            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If

            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1
                dRow("EMP Code") = "" : dRow("Employee Name") = "" : dRow("Login Name") = ""
                dRow("E-Mail") = "" : dRow("Office Phone No") = "" : dRow("Designation") = ""
                dRow("Role") = ""


                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("EMP Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Employee Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Login Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("E-Mail") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("Office Phone No") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("Designation") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                    If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("Role") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
                    End If
                End If

                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadEmployeeDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    '5 User Master
    'Private Function LoadUserDetails(ByVal sFile As String) As DataTable
    '    Dim dtTable As New DataTable, dtStock As New DataTable
    '    Dim objDBL As New DBHelper
    '    Dim dRow As DataRow
    '    Dim i As Integer
    '    Dim sCommodity As String = ""
    '    Try
    '        dtTable.Columns.Add("Sr.No")
    '        dtTable.Columns.Add("Vendor")
    '        dtTable.Columns.Add("EMP Code")
    '        dtTable.Columns.Add("User Name")
    '        dtTable.Columns.Add("Login Name")
    '        dtTable.Columns.Add("E-Mail")
    '        dtTable.Columns.Add("Office Phone No")
    '        dtTable.Columns.Add("Designation")
    '        dtTable.Columns.Add("Role")
    '        dtTable.Columns.Add("Module")
    '        dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
    '        If IsNothing(dtStock) = True Then
    '            lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '            ddlSheetName.Items.Clear()
    '            Return dtStock
    '        End If
    '        For i = 0 To dtStock.Rows.Count - 1
    '            dRow = dtTable.NewRow
    '            dRow("Sr.No") = i + 1

    '            dRow("Vendor") = "" : dRow("EMP Code") = "" : dRow("User Name") = ""
    '            dRow("Login Name") = "" : dRow("E-Mail") = "" : dRow("Office Phone No") = ""
    '            dRow("Designation") = "" : dRow("Role") = "" : dRow("Module") = ""
    '            If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
    '                If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                    dRow("Vendor") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
    '                End If
    '            End If

    '            If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
    '                If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
    '                    dRow("EMP Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
    '                If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
    '                    dRow("User Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
    '                If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
    '                    dRow("Login Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
    '                If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
    '                    dRow("E-Mail") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
    '                If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
    '                    dRow("Office Phone No") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
    '                If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
    '                    dRow("Designation") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
    '                If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
    '                    dRow("Role") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
    '                End If
    '            End If
    '            If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
    '                If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
    '                    dRow("Module") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(8))
    '                End If
    '            End If
    '            dtTable.Rows.Add(dRow)
    '        Next
    '        Return dtTable
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadUserDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Function

    ' 5- Customer User Master
    Private Function LoadCustomerUserDetails(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Customer Name")
            dtTable.Columns.Add("EMP Code")
            dtTable.Columns.Add("User Name")
            dtTable.Columns.Add("Login Name")
            dtTable.Columns.Add("E-Mail")
            dtTable.Columns.Add("Phone No")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1

                dRow("Customer Name") = "" : dRow("EMP Code") = "" : dRow("User Name") = ""
                dRow("Login Name") = "" : dRow("E-Mail") = "" : dRow("Phone No") = ""

                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Customer Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If

                'If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                '    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                '        dRow("EMP Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                '    End If
                'End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("User Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("Login Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("E-Mail") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("Phone No") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                    End If
                End If

                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomerUserDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    '11 General Master
    Private Function LoadGeneralMaster(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Type")
            dtTable.Columns.Add("Name")
            dtTable.Columns.Add("Code")
            dtTable.Columns.Add("Notes")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Type") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Code") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("Notes") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadGeneralMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    'Checklist
    Private Function LoadBranchExcelTable(ByVal sFilePath As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer, iYearID As Integer
        Dim sYear As String
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Customer")
            dtTable.Columns.Add("Function")
            dtTable.Columns.Add("Area")
            dtTable.Columns.Add("Check Point No.")
            dtTable.Columns.Add("Check")
            dtTable.Columns.Add("Risk Category")
            dtTable.Columns.Add("Methodology")
            dtTable.Columns.Add("Sample Size")
            dtTable.Columns.Add("Status")
            dtTable.Columns.Add("Function Type")

            iYearID = objclsExcelUpload.CheckAuditCheckListYearIDExists(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlMasterName.SelectedIndex)
            If iYearID > 0 Then
                sYear = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iYearID)
                lblExcelValidationMsg.Text = "CheckList Masters has been uploaded for '" & sYear & "'." : lblError.Text = "CheckList Masters has been uploaded for '" & sYear & "'."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                Exit Function
            End If

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If

            For i = 0 To dtStock.Rows.Count - 1
                If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" And dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                    dRow = dtTable.NewRow
                    dRow("Customer") = ""
                    If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                        If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("Customer") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                        End If
                    End If
                    dRow("Function") = ""
                    If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                        If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                            dRow("Function") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                        End If
                    End If

                    dRow("Area") = ""
                    If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                        If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                            dRow("Area") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                        End If
                    End If

                    dRow("Check Point No.") = ""
                    If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                        If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                            dRow("Check Point No.") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                        End If
                    End If

                    dRow("Check") = ""
                    If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                        If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                            dRow("Check") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
                        End If
                    End If

                    dRow("Risk Category") = ""
                    If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                        If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                            dRow("Risk Category") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                        End If
                    End If

                    dRow("Methodology") = ""
                    If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                        If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                            dRow("Methodology") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
                        End If
                    End If

                    dRow("Sample Size") = ""
                    If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                        If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                            dRow("Sample Size") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
                        End If
                    End If

                    dRow("Status") = ""
                    If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                        If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
                            dRow("Status") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(8))
                        End If
                    End If

                    dRow("Function Type") = ""
                    If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                        If dtStock.Rows(i).Item(9).ToString <> "&nbsp;" Then
                            dRow("Function Type") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(9))
                        End If
                    End If

                    dRow("Sr.No") = i + 1
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBranchExcelTable" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Protected Sub dgGeneral_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgGeneral.ItemDataBound
        Try
            If (e.Item.ItemType <> ListItemType.Header) And (e.Item.ItemType <> ListItemType.Footer) Then
                If ddlMasterName.SelectedValue = 1 Then 'Organisation Structure
                    Dim lblZone As New Label, lblRegion As New Label, lblArea As New Label, lblBranch As New Label, lblZoneIRDACode As New Label, lblRegionIRDACode As New Label
                    Dim lblAreaIRDACode As New Label, lblBranchIRDACode As New Label, lblNote As New Label
                    Dim lblParentID As New Label, lblCurrentID As New Label, lblDepthID As New Label
                    Dim iParentID As Integer, iCurrentID As Integer, iDepthID As Integer

                    lblZone.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    lblZoneIRDACode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    lblRegion.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    lblRegionIRDACode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    lblArea.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                    lblAreaIRDACode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(6).Text)
                    lblBranch.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(7).Text)
                    lblBranchIRDACode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(8).Text)
                    lblNote.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(9).Text)

                    lblParentID.Text = 1 : lblCurrentID.Text = 0 : lblDepthID.Text = 0
                    iParentID = Val(lblParentID.Text) : iCurrentID = Val(lblCurrentID.Text)
                    If iCurrentID = 0 Then
                        iDepthID = Val(lblDepthID.Text) + 1
                    Else
                        iDepthID = Val(lblDepthID.Text)
                    End If
                    'Zone
                    If lblZone.Text.Trim = "" Or lblZone.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Zone. (Red color indicates invalid data In below grid)."
                    ElseIf lblZone.Text.Trim.Length > 1000 Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Zone exceeded maximum size(max 1000 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If lblZoneIRDACode.Text.Trim = "" Or lblZoneIRDACode.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Zone Code. (Red color indicates invalid data In below grid)."
                    ElseIf lblZoneIRDACode.Text.Trim.Length > 10 Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Zone Code exceeded maximum size(max 10 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblZone.Text.Trim)), iParentID, iCurrentID) = True Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Zone Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ". (Red color indicates invalid data In below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblZoneIRDACode.Text.Trim)), iCurrentID) = True Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Zone Code already exists. (Red color indicates invalid data In below grid)."
                    End If

                    'Region
                    If lblRegion.Text.Trim = "" Or lblRegion.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Region. (Red color indicates invalid data In below grid)."
                    ElseIf lblRegion.Text.Trim.Length > 1000 Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Region exceeded maximum size(max 1000 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If lblRegionIRDACode.Text.Trim = "" Or lblRegionIRDACode.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Region Code. (Red color indicates invalid data In below grid)."
                    ElseIf lblRegionIRDACode.Text.Trim.Length > 10 Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Region Code exceeded maximum size(max 10 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblRegion.Text.Trim)), iParentID, iCurrentID) = True Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Region Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ". (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblRegionIRDACode.Text.Trim)), iCurrentID) = True Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Region Code already exists. (Red color indicates invalid data in below grid)."
                    End If

                    'Area
                    If lblArea.Text.Trim = "" Or lblArea.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Area. (Red color indicates invalid data In below grid)."
                    ElseIf lblArea.Text.Trim.Length > 1000 Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Area exceeded maximum size(max 1000 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If lblAreaIRDACode.Text.Trim = "" Or lblAreaIRDACode.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Area Code. (Red color indicates invalid data In below grid)."
                    ElseIf lblAreaIRDACode.Text.Trim.Length > 10 Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Area Code exceeded maximum size(max 10 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblArea.Text.Trim)), iParentID, iCurrentID) = True Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Area Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ". (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblAreaIRDACode.Text.Trim)), iCurrentID) = True Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Area Code already exists. (Red color indicates invalid data in below grid)."
                    End If

                    'Branch
                    If lblBranch.Text.Trim = "" Or lblBranch.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Branch. (Red color indicates invalid data In below grid)."
                    ElseIf lblBranch.Text.Trim.Length > 1000 Then
                        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                        lblError.Text = "Branch exceeded maximum size(max 1000 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If lblBranchIRDACode.Text.Trim = "" Or lblBranchIRDACode.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Branch Code. (Red color indicates invalid data In below grid)."
                    ElseIf lblBranchIRDACode.Text.Trim.Length > 10 Then
                        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                        lblError.Text = "Branch Code exceeded maximum size(max 10 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblBranch.Text.Trim)), iParentID, iCurrentID) = True Then
                        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                        lblError.Text = "Branch Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ". (Red color indicates invalid data in below grid)."
                    End If
                    If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblBranchIRDACode.Text.Trim)), iCurrentID) = True Then
                        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                        lblError.Text = "Branch Code already exists. (Red color indicates invalid data in below grid)."
                    End If
                    If lblNote.Text.Trim.Length > 2000 Then
                        e.Item.Cells(9).Font.Bold = True : e.Item.Cells(9).ForeColor = Drawing.Color.Red
                        lblError.Text = "Note/Address/Remarks exceeded maximum size(max 2000 characters). (Red color indicates invalid data in below grid)."
                    End If
                End If

                If ddlMasterName.SelectedValue = 2 Then 'Holiday Master
                    Dim lblDate As New Label, lblYear As New Label, lblOccasion As New Label
                    Dim dHdate As Date
                    Dim bYear As Boolean

                    lblYear.Text = e.Item.Cells(1).Text
                    lblDate.Text = e.Item.Cells(2).Text
                    lblOccasion.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    If lblYear.Text.Trim = "" Or lblYear.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Financial Year. (Red color indicates invalid data In below grid)."
                    Else
                        bYear = objclsExcelUpload.CheckFinancialYear(sSession.AccessCode, sSession.AccessCodeID, lblYear.Text)
                        If bYear = False Then
                            e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                            lblError.Text = "Invalid Financial Year " & lblYear.Text.Trim & "'. Year should be YYYY-YYYY. (Red color indicates invalid data In below grid)."
                        End If
                        If lblDate.Text.Trim = "" Or lblDate.Text.Trim = "&nbsp:" Then
                            e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                            lblError.Text = "Enter valid Holiday Date(dd/MM/yyyy). (Red color indicates invalid data In below grid)."
                        ElseIf lblDate.Text <> "" Then
                            Try
                                dHdate = Date.ParseExact(lblDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Catch ex As Exception
                                e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                                lblError.Text = "Invalid Date Format - '" & lblDate.Text.Trim & "'. Year date be dd/MM/YYYY. (Red color indicates invalid data In below grid)."
                            End Try
                        End If
                    End If
                    If lblOccasion.Text.Trim = "" Or lblOccasion.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Occasion. (Red color indicates invalid data In below grid)."
                    End If
                End If

                'If ddlMasterName.SelectedValue = 3 Then 'Employee Master
                '    Dim lblZone As New Label, lblRegion As New Label, lblArea As New Label, lblBranch As New Label, lblSAPcode As New Label, lblEmployeeName As New Label
                '    Dim lblLoginName As New Label, lblEmail As New Label, lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label, lblModule As New Label
                '    lblZone.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                '    lblRegion.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                '    lblArea.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                '    lblBranch.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                '    lblSAPcode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                '    lblEmployeeName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(6).Text)
                '    lblLoginName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(7).Text)
                '    lblEmail.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(8).Text)
                '    lblOfficePhoneNo.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(9).Text)
                '    lblDesignation.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(10).Text)
                '    lblRole.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(11).Text)
                '    lblModule.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(12).Text)
                '    If lblSAPcode.Text.Trim = "" Or lblSAPcode.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter EMP Code. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                '        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                '        lblError.Text = "EMP Code '" & e.Item.Cells(5).Text & "' already exist. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblLoginName.Text.Trim = "" Or lblLoginName.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Login Name. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
                '        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Login Name '" & e.Item.Cells(7).Text & "' already exist. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblEmail.Text.Trim = "" Or lblEmail.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter E-Mail. (Red color indicates invalid data in below grid)."
                '    ElseIf Regex.IsMatch(lblEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then
                '        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid E-Mail '" & lblEmail.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblEmail.Text.Trim.Length > 50 Then
                '        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                '        lblError.Text = "E-Mail exceeded maximum size(max 50 characters). (Red color indicates invalid data in below grid)."
                '    End If
                '    If Regex.IsMatch(lblOfficePhoneNo.Text, "^[0-9]*$") = False Then
                '        e.Item.Cells(9).Font.Bold = True : e.Item.Cells(9).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Office Phone No. " & lblOfficePhoneNo.Text & ". (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblOfficePhoneNo.Text.Trim.Length > 15 Then
                '        e.Item.Cells(9).Font.Bold = True : e.Item.Cells(9).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Office Phone No. " & lblOfficePhoneNo.Text & "exceeded maximum size(max 15 numbers). (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblDesignation.Text.Trim = "" Or lblDesignation.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(10).Font.Bold = True : e.Item.Cells(10).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Designation. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text) = 0 Then
                '        e.Item.Cells(10).Font.Bold = True : e.Item.Cells(10).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Designation '" & lblDesignation.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblRole.Text.Trim = "" Or lblRole.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(11).Font.Bold = True : e.Item.Cells(11).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Role. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text) = 0 Then
                '        e.Item.Cells(11).Font.Bold = True : e.Item.Cells(11).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Role '" & lblRole.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblModule.Text.Trim = "" Or lblModule.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(12).Font.Bold = True : e.Item.Cells(12).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Module. (Red color indicates invalid data in below grid)."
                '    ElseIf UCase(lblModule.Text.Trim) <> "MASTER" And UCase(lblModule.Text.Trim) <> "AUDIT" And UCase(lblModule.Text.Trim) <> "RISK" And UCase(lblModule.Text.Trim) <> "COMPLIANCE" Then
                '        e.Item.Cells(12).Font.Bold = True : e.Item.Cells(12).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Module '" & lblModule.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblZone.Text.Trim = "" Or lblZone.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Zone. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckZone(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text) = 0 Then
                '        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Zone '" & lblZone.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblRegion.Text.Trim = "" Or lblRegion.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Region. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckRegion(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text, lblRegion.Text) = 0 Then
                '        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Region '" & lblRegion.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    'If lblArea.Text.Trim = "" Or lblArea.Text.Trim = "&nbsp:" Then
                '    '    e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                '    '    lblError.Text = "Enter Area. (Red color indicates invalid data in below grid)."
                '    'ElseIf objclsExcelUpload.CheckArea(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text, lblRegion.Text, lblArea.Text) = 0 Then
                '    '    e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                '    '    lblError.Text = "Invalid Area '" & lblArea.Text & "'. (Red color indicates invalid data in below grid)."
                '    'End If
                '    'If lblBranch.Text.Trim = "" Or lblBranch.Text.Trim = "&nbsp:" Then
                '    '    e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                '    '    lblError.Text = "Enter Branch. (Red color indicates invalid data in below grid)."
                '    'ElseIf objclsExcelUpload.CheckBranch(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text, lblRegion.Text, lblArea.Text, lblBranch.Text) = 0 Then
                '    '    e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                '    '    lblError.Text = "Invalid Branch '" & lblBranch.Text & "'. (Red color indicates invalid data in below grid)."
                '    'End If
                'End If

                'Added by steffi on 02-01-2024
                If ddlMasterName.SelectedValue = 3 Then 'Employee Master
                    Dim lblSAPcode As New Label, lblEmployeeName As New Label
                    Dim lblLoginName As New Label, lblEmail As New Label, lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label

                    lblSAPcode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    lblEmployeeName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    lblLoginName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    lblEmail.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    lblOfficePhoneNo.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                    lblDesignation.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(6).Text)
                    lblRole.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(7).Text)

                    'If lblSAPcode.Text.Trim = "" Or lblSAPcode.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter EMP Code. (Red color indicates invalid data in below grid)."
                    '    Return
                    'ElseIf objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                    '    e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "EMP Code '" & e.Item.Cells(1).Text & "' already exist. (Red color indicates invalid data in below grid)."
                    '    Return
                    'End If

                    If lblSAPcode.Text.Trim = "" Or lblSAPcode.Text.Trim = "&nbsp:" Then
                    Else
                        If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                            e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                            lblError.Text = "EMP Code '" & e.Item.Cells(1).Text & "' already exist. (Red color indicates invalid data in below grid)."
                            Return
                        End If
                    End If

                    If lblEmployeeName.Text.Trim = "" Or lblEmployeeName.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Employee Name. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblEmployeeName.Text) = True Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Employee Name '" & e.Item.Cells(2).Text & "' already exist. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblLoginName.Text.Trim = "" Or lblLoginName.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Login Name. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Login Name '" & e.Item.Cells(3).Text & "' already exist. (Red color indicates invalid data in below grid)."
                        Return
                    End If
                    If lblEmail.Text.Trim = "" Or lblEmail.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter E-Mail. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf Regex.IsMatch(lblEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid E-Mail '" & lblEmail.Text & "'. (Red color indicates invalid data in below grid)."
                        Return
                    End If
                    If lblEmail.Text.Trim.Length > 50 Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "E-Mail exceeded maximum size(max 50 characters). (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblOfficePhoneNo.Text.Trim <> "" And lblOfficePhoneNo.Text.Trim <> "&nbsp:" Then
                        If Regex.IsMatch(lblOfficePhoneNo.Text, "^[0-9]*$") = False Then
                            e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                            lblError.Text = "Invalid Office Phone No. " & lblOfficePhoneNo.Text & ". (Red color indicates invalid data in below grid)."
                            Return
                        End If
                        If lblOfficePhoneNo.Text.Trim.Length > 15 Then
                            e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                            lblError.Text = "Office Phone No. " & lblOfficePhoneNo.Text & "exceeded maximum size(max 15 numbers). (Red color indicates invalid data in below grid)."
                            Return
                        End If
                    End If


                    If lblDesignation.Text.Trim = "" Or lblDesignation.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Designation. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text) = 0 Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Designation '" & lblDesignation.Text & "'. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblRole.Text.Trim = "" Or lblRole.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Partner. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblRole.Text = "Yes" Then
                        lblRole.Text = "Partner"
                    ElseIf lblRole.Text = "No" Then
                        lblRole.Text = "Audit Assistant"
                    Else
                        lblRole.Text = "Audit Assistant"
                    End If

                    If objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text) = 0 Then
                        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Partner '" & lblRole.Text & "'. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                End If

                If ddlMasterName.SelectedValue = 4 Then
                    Dim lblCustomerName As New Label : Dim lblOrgType As New Label : Dim lblAddress As New Label
                    Dim lblCity As New Label : Dim lblContactPerson As New Label : Dim lblMobileNumber As New Label : Dim lblEmail As New Label
                    Dim lblbusiness As New Label
                    Dim lblCustomer As New Label
                    Dim lblIndType As New Label
                    Dim lblRegNo As New Label
                    Dim lblProffServiceOff1 As New Label
                    Dim lblProffServiceOff2 As New Label
                    Dim lbllocation1 As New Label
                    Dim lblContactPers1 As New Label
                    Dim lblAddress1 As New Label
                    Dim lblCIN1 As New Label
                    Dim lblTAN1 As New Label
                    Dim lblGST1 As New Label
                    Dim lbllocation2 As New Label
                    Dim lblContactPers2 As New Label
                    Dim lblAddress2 As New Label
                    Dim lblCIN2 As New Label
                    Dim lblTAN2 As New Label
                    Dim lblGST2 As New Label
                    Dim lbllocation3 As New Label
                    Dim lblContactPers3 As New Label
                    Dim lblAddress3 As New Label
                    Dim lblCIN3 As New Label
                    Dim lblTAN3 As New Label
                    Dim lblGST3 As New Label
                    Dim lbllocation4 As New Label
                    Dim lblContactPers4 As New Label
                    Dim lblAddress4 As New Label
                    Dim lblCIN4 As New Label
                    Dim lblTAN4 As New Label
                    Dim lblGST4 As New Label
                    Dim lbllocation5 As New Label
                    Dim lblContactPers5 As New Label
                    Dim lblAddress5 As New Label
                    Dim lblCIN5 As New Label
                    Dim lblTAN5 As New Label
                    Dim lblGST5 As New Label
                    Dim lbldirectName1 As New Label
                    Dim Din1 As New Label
                    Dim lbldirectName2 As New Label
                    Dim Din2 As New Label

                    lblCustomerName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    lblOrgType.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    lblAddress.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    lblCity.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    lblEmail.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                    lblMobileNumber.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(6).Text)
                    lblbusiness.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(7).Text)
                    lblIndType.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(8).Text)
                    lblRegNo.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(9).Text)
                    lblProffServiceOff1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(10).Text)
                    lblProffServiceOff2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(11).Text)
                    lbllocation1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(12).Text)
                    lblContactPers1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(13).Text)
                    lblAddress1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(14).Text)
                    lblCIN1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(15).Text)
                    lblTAN1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(16).Text)
                    lblGST1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(17).Text)
                    lbllocation2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(18).Text)
                    lblContactPers2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(19).Text)
                    lblAddress2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(20).Text)
                    lblCIN2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(21).Text)
                    lblTAN2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(22).Text)
                    lblGST2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(23).Text)
                    lbllocation3.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(24).Text)
                    lblContactPers3.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(25).Text)
                    lblAddress3.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(26).Text)
                    lblCIN3.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(27).Text)
                    lblTAN3.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(28).Text)
                    lblGST3.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(29).Text)
                    lbllocation4.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(30).Text)
                    lblContactPers4.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(31).Text)
                    lblAddress4.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(32).Text)
                    lblCIN4.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(33).Text)
                    lblTAN4.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(34).Text)
                    lblGST4.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(35).Text)
                    lbllocation5.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(36).Text)
                    lblContactPers5.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(37).Text)
                    lblAddress5.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(38).Text)
                    lblCIN5.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(39).Text)
                    lblTAN5.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(40).Text)
                    lblGST5.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(41).Text)
                    lbldirectName1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(42).Text)
                    Din1.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(43).Text)
                    lbldirectName2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(44).Text)
                    Din2.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(45).Text)


                    If lblCustomerName.Text.Trim = "" Or lblCustomerName.Text.Trim = "&nbsp:" Then
                        lblError.Text = "Enter Customer Name. (Red color indicates invalid data in below grid)."
                    ElseIf objclsCustomerMaster.CheckCustType(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCustomerName.Text.Trim)) = True Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Customer Name already exist. '" & e.Item.Cells(1).Text & "'(Red color indicates invalid data in below grid)."
                    End If

                    If lblOrgType.Text.Trim = "" Or lblOrgType.Text.Trim = "&nbsp:" Then
                        lblError.Text = "Enter Orgnisation Type. (Red color indicates invalid data in below grid)."
                    ElseIf objclsCustomerMaster.CheckOrganisationType(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblOrgType.Text.Trim)) = False Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Orgnisation Type not exists. (Red color indicates invalid data in below grid)."
                    End If

                    If lblAddress.Text.Trim = "" Or lblAddress.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Address. (Red color indicates invalid data in below grid)."
                    End If

                    If lblCity.Text.Trim = "" Or lblCity.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter City. (Red color indicates invalid data in below grid)."
                    End If

                    If Regex.IsMatch(lblEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid E-Mail '" & lblEmail.Text & "'. (Red color indicates invalid data in below grid)."
                    End If

                    If lblEmail.Text.Trim.Length > 50 Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "E-Mail exceeded maximum size(max 50 characters). (Red color indicates invalid data in below grid)."
                    End If

                    'If lblContactPerson.Text.Trim = "" Or lblContactPerson.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Contact person. " & lblCity.Text & ". (Red color indicates invalid data in below grid)."
                    'End If

                    If Regex.IsMatch(lblMobileNumber.Text, "^[0-9]*$") = False Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Mobile No. " & lblMobileNumber.Text & ". (Red color indicates invalid data in below grid)."
                    End If
                    If lblMobileNumber.Text.Trim.Length <> 10 Then
                        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid MobileNo. '" & lblMobileNumber.Text & "' Enter valid 10 digits Mobile No. (Red color indicates invalid data in below grid)."
                    End If
                    If lblbusiness.Text.Trim = "" Or lblbusiness.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Business Reltn. Start Date. (Red color indicates invalid data in below grid)."
                    End If
                    If lblIndType.Text.Trim = "" Or lblIndType.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Industry Type. (Red color indicates invalid data in below grid)."
                    ElseIf objclsCustomerMaster.CheckIndustryType(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblIndType.Text.Trim)) = False Then
                        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                        lblError.Text = "Industry Type not exists. (Red color indicates invalid data in below grid)."
                    End If
                    'If lblRegNo.Text.Trim = "" Or lblRegNo.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(9).Font.Bold = True : e.Item.Cells(9).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter * Registration No. (Red color indicates invalid data in below grid)."
                    'End If
                    If lblProffServiceOff1.Text.Trim = "" Or lblProffServiceOff1.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(10).Font.Bold = True : e.Item.Cells(10).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter * Professional Services Offered 1. (Red color indicates invalid data in below grid)."
                    ElseIf objclsCustomerMaster.CheckProfessionalServicesOffered(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblProffServiceOff1.Text.Trim)) = False Then
                        e.Item.Cells(10).Font.Bold = True : e.Item.Cells(10).ForeColor = Drawing.Color.Red
                        lblError.Text = "Professional Services not exists. (Red color indicates invalid data in below grid)."
                    End If
                    'If lblProffServiceOff2.Text.Trim = "" Or lblProffServiceOff2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(11).Font.Bold = True : e.Item.Cells(11).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter * Professional Services Offered 2. (Red color indicates invalid data in below grid)."
                    'ElseIf objclsCustomerMaster.CheckProfessionalServicesOffered(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblProffServiceOff2.Text.Trim)) = False Then
                    '    e.Item.Cells(12).Font.Bold = True : e.Item.Cells(11).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Orgnisation Type not exists. (Red color indicates invalid data in below grid)."
                    'End If
                    If lbllocation1.Text.Trim = "" Or lbllocation1.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(12).Font.Bold = True : e.Item.Cells(12).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Location Name1. (Red color indicates invalid data in below grid)."
                    End If
                    If lblContactPers1.Text.Trim = "" Or lblContactPers1.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(13).Font.Bold = True : e.Item.Cells(13).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Cantact Person 1. (Red color indicates invalid data in below grid)."
                    End If
                    'If lblAddress1.Text.Trim = "" Or lblAddress1.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(14).Font.Bold = True : e.Item.Cells(14).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Address 1. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblCIN1.Text.Trim = "" Or lblCIN1.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(15).Font.Bold = True : e.Item.Cells(15).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter CIN1. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblTAN1.Text.Trim = "" Or lblTAN1.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(16).Font.Bold = True : e.Item.Cells(16).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter TAN1. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblGST1.Text.Trim = "" Or lblGST1.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(17).Font.Bold = True : e.Item.Cells(17).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter GST1. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lbllocation2.Text.Trim = "" Or lbllocation2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(18).Font.Bold = True : e.Item.Cells(18).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Loaction Name 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblContactPers2.Text.Trim = "" Or lblContactPers2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(19).Font.Bold = True : e.Item.Cells(19).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Contact Person 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblAddress2.Text.Trim = "" Or lblAddress2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(20).Font.Bold = True : e.Item.Cells(20).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Address 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblCIN2.Text.Trim = "" Or lblCIN2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(21).Font.Bold = True : e.Item.Cells(21).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter CIN 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblTAN2.Text.Trim = "" Or lblTAN2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(22).Font.Bold = True : e.Item.Cells(22).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter TAN 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblGST2.Text.Trim = "" Or lblGST2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(23).Font.Bold = True : e.Item.Cells(23).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter GST 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lbllocation3.Text.Trim = "" Or lbllocation3.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(24).Font.Bold = True : e.Item.Cells(24).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Loaction Name 3. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblContactPers3.Text.Trim = "" Or lblContactPers3.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(25).Font.Bold = True : e.Item.Cells(25).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Contact Person 3. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblAddress3.Text.Trim = "" Or lblAddress3.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(26).Font.Bold = True : e.Item.Cells(26).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Address 3. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblCIN3.Text.Trim = "" Or lblCIN3.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(27).Font.Bold = True : e.Item.Cells(27).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter CIN 3. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblTAN3.Text.Trim = "" Or lblTAN3.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(28).Font.Bold = True : e.Item.Cells(28).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter TAN 3. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblGST3.Text.Trim = "" Or lblGST3.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(29).Font.Bold = True : e.Item.Cells(29).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter GST 3. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lbllocation4.Text.Trim = "" Or lbllocation4.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(30).Font.Bold = True : e.Item.Cells(30).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Location Name 4. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblContactPers4.Text.Trim = "" Or lblContactPers4.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(31).Font.Bold = True : e.Item.Cells(31).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Contact Person 4. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblAddress4.Text.Trim = "" Or lblAddress4.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(32).Font.Bold = True : e.Item.Cells(32).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Address 4. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblCIN4.Text.Trim = "" Or lblCIN4.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(33).Font.Bold = True : e.Item.Cells(33).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter CIN 4. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblTAN4.Text.Trim = "" Or lblTAN4.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(34).Font.Bold = True : e.Item.Cells(34).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter TAN 4. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblGST4.Text.Trim = "" Or lblGST4.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(35).Font.Bold = True : e.Item.Cells(35).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter GST 4. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lbllocation5.Text.Trim = "" Or lbllocation5.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(36).Font.Bold = True : e.Item.Cells(36).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Location Name 5. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblContactPers5.Text.Trim = "" Or lblContactPers5.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(37).Font.Bold = True : e.Item.Cells(37).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Contact Person 5. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblAddress5.Text.Trim = "" Or lblAddress5.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(38).Font.Bold = True : e.Item.Cells(38).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Address 5. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblCIN5.Text.Trim = "" Or lblCIN5.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(39).Font.Bold = True : e.Item.Cells(39).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter CIN 5. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblTAN5.Text.Trim = "" Or lblTAN5.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(40).Font.Bold = True : e.Item.Cells(40).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter TAN 5. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lblGST5.Text.Trim = "" Or lblGST5.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(41).Font.Bold = True : e.Item.Cells(41).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter GST 5. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lbldirectName1.Text.Trim = "" Or lbldirectName1.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(42).Font.Bold = True : e.Item.Cells(42).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Director Name 1. (Red color indicates invalid data in below grid)."
                    'End If
                    'If Din1.Text.Trim = "" Or Din1.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(43).Font.Bold = True : e.Item.Cells(43).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter DIN 1. (Red color indicates invalid data in below grid)."
                    'End If
                    'If lbldirectName2.Text.Trim = "" Or lbldirectName2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(44).Font.Bold = True : e.Item.Cells(44).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter Director Name 2. (Red color indicates invalid data in below grid)."
                    'End If
                    'If Din2.Text.Trim = "" Or Din2.Text.Trim = "&nbsp;" Then
                    '    e.Item.Cells(45).Font.Bold = True : e.Item.Cells(45).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter DIN 2. (Red color indicates invalid data in below grid)."
                    'End If

                End If

                'If ddlMasterName.SelectedValue = 5 Then 'User Master
                '    Dim lblVendor As New Label, lblSAPcode As New Label, lblEmployeeName As New Label, lblLoginName As New Label, lblEmail As New Label
                '    Dim lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label, lblModule As New Label
                '    lblVendor.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                '    lblSAPcode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                '    lblEmployeeName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                '    lblLoginName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                '    lblEmail.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                '    lblOfficePhoneNo.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(6).Text)
                '    lblDesignation.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(7).Text)
                '    lblRole.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(8).Text)
                '    lblModule.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(9).Text)
                '    If lblVendor.Text.Trim = "" Or lblVendor.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Vendor. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckVendor(sSession.AccessCode, sSession.AccessCodeID, lblVendor.Text) = 0 Then
                '        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Vendor '" & lblVendor.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblSAPcode.Text.Trim = "" Or lblSAPcode.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter EMP Code. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                '        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                '        lblError.Text = "EMP Code '" & lblSAPcode.Text & "' already exist. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblEmployeeName.Text.Trim = "" Or lblEmployeeName.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter User Name. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblLoginName.Text.Trim = "" Or lblLoginName.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Login Name. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
                '        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Login Name '" & lblLoginName.Text & "' already exist. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblEmail.Text.Trim = "" Or lblEmail.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter E-Mail. (Red color indicates invalid data in below grid)."
                '    ElseIf Regex.IsMatch(lblEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then
                '        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid E-Mail '" & lblEmail.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblEmail.Text.Trim.Length > 50 Then
                '        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                '        lblError.Text = "E-Mail exceeded maximum size(max 50 characters). (Red color indicates invalid data in below grid)."
                '    End If
                '    If Regex.IsMatch(lblOfficePhoneNo.Text, "^[0-9]*$") = False Then
                '        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Office Phone No. " & lblOfficePhoneNo.Text & ". (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblOfficePhoneNo.Text.Trim.Length > 15 Then
                '        e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Office Phone No. " & lblOfficePhoneNo.Text & "exceeded maximum size(max 15 numbers). (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblDesignation.Text.Trim = "" Or lblDesignation.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Designation. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text) = 0 Then
                '        e.Item.Cells(7).Font.Bold = True : e.Item.Cells(7).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Designation '" & lblDesignation.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblRole.Text.Trim = "" Or lblRole.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Role. (Red color indicates invalid data in below grid)."
                '    ElseIf objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text) = 0 Then
                '        e.Item.Cells(8).Font.Bold = True : e.Item.Cells(8).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Role '" & lblRole.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                '    If lblModule.Text.Trim = "" Or lblModule.Text.Trim = "&nbsp:" Then
                '        e.Item.Cells(9).Font.Bold = True : e.Item.Cells(9).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Enter Module. (Red color indicates invalid data in below grid)."
                '    ElseIf UCase(lblModule.Text.Trim) <> "MASTER" And UCase(lblModule.Text.Trim) <> "AUDIT" Then
                '        e.Item.Cells(9).Font.Bold = True : e.Item.Cells(9).ForeColor = Drawing.Color.Red
                '        lblError.Text = "Invalid Module '" & lblModule.Text & "'. (Red color indicates invalid data in below grid)."
                '    End If
                'End If

                'Added by Steffi on 02-01-2024
                If ddlMasterName.SelectedValue = 5 Then 'Customer User Master
                    Dim lblCustomerName As New Label, lblSAPcode As New Label, lblEmployeeName As New Label, lblLoginName As New Label, lblEmail As New Label
                    Dim lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label, lblModule As New Label
                    lblCustomerName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    lblSAPcode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    lblEmployeeName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    lblLoginName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    lblEmail.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                    lblOfficePhoneNo.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(6).Text)
                    'lblDesignation.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(7).Text)
                    'lblRole.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(8).Text)
                    'lblModule.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(9).Text)


                    If lblCustomerName.Text.Trim = "" Or lblCustomerName.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Customer Name. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf objCust.CheckCustomerExists(sSession.AccessCode, sSession.AccessCodeID, lblCustomerName.Text) = False Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Customer Name is not valid. '" & lblCustomerName.Text & "'. (Red color indicates invalid data in below grid)."
                        Return

                    End If

                    'If lblSAPcode.Text.Trim = "" Or lblSAPcode.Text.Trim = "&nbsp:" Then
                    '    e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "Enter EMP Code. (Red color indicates invalid data in below grid)."
                    '    Return
                    'ElseIf objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                    '    e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                    '    lblError.Text = "EMP Code '" & lblSAPcode.Text & "' already exist. (Red color indicates invalid data in below grid)."
                    '    Return
                    'End If

                    If lblSAPcode.Text.Trim = "" Or lblSAPcode.Text.Trim = "&nbsp:" Then

                    ElseIf objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "EMP Code '" & lblSAPcode.Text & "' already exist. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblEmployeeName.Text.Trim = "" Or lblEmployeeName.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter User Name. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblLoginName.Text.Trim = "" Or lblLoginName.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Login Name. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Login Name '" & lblLoginName.Text & "' already exist. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblEmail.Text.Trim = "" Or lblEmail.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter E-Mail. (Red color indicates invalid data in below grid)."
                        Return
                    ElseIf Regex.IsMatch(lblEmail.Text, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$") = False Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid E-Mail '" & lblEmail.Text & "'. (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblEmail.Text.Trim.Length > 50 Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "E-Mail exceeded maximum size(max 50 characters). (Red color indicates invalid data in below grid)."
                        Return
                    End If

                    If lblOfficePhoneNo.Text.Trim <> "" And lblOfficePhoneNo.Text.Trim <> "&nbsp:" Then
                        If Regex.IsMatch(lblOfficePhoneNo.Text, "^[0-9]*$") = False Then
                            e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                            lblError.Text = "Invalid Phone No. " & lblOfficePhoneNo.Text & ". (Red color indicates invalid data in below grid)."
                        End If
                        If lblOfficePhoneNo.Text.Trim.Length > 15 Then
                            e.Item.Cells(6).Font.Bold = True : e.Item.Cells(6).ForeColor = Drawing.Color.Red
                            lblError.Text = "Phone No. " & lblOfficePhoneNo.Text & "exceeded maximum size(max 15 numbers). (Red color indicates invalid data in below grid)."
                        End If
                    End If
                End If

                If ddlMasterName.SelectedValue = 6 Then 'Service & Conveyance Master
                    Dim lblDesignation As New Label, lblPerDayCharges As New Label, lblNoOfHoursPerDay As New Label, lblPerKmCharges As New Label, lblRemarks As New Label
                    lblDesignation.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    lblPerDayCharges.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    lblNoOfHoursPerDay.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    lblPerKmCharges.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    lblRemarks.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)
                    If lblDesignation.Text.Trim = "" Or lblDesignation.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Designation. (Red color indicates invalid data in below grid)."
                    ElseIf objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text) = 0 Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Designation '" & lblDesignation.Text & "'. (Red color indicates invalid data in below grid)."
                    End If
                    If lblPerDayCharges.Text.Trim = "" Or lblPerDayCharges.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Per Day Charges. (Red color indicates invalid data in below grid)."
                    ElseIf Regex.IsMatch(lblPerDayCharges.Text, "^[0-9]\d*(\.\d+)?$") = False Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Per Day Charges '" & lblPerDayCharges.Text & "'. (Red color indicates invalid data in below grid)."
                    ElseIf lblPerDayCharges.Text.Length > 500 Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Per Day Charges exceeded maximum size(max 500 characters). (Red color indicates invalid data in below grid)."
                    End If
                    If lblNoOfHoursPerDay.Text.Trim = "" Or lblNoOfHoursPerDay.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter No. of Hours Per Day. (Red color indicates invalid data in below grid)."
                    ElseIf Regex.IsMatch(lblNoOfHoursPerDay.Text, "^[0-9]\d*(\.\d+)?$") = False Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid No. of Hours Per Day '" & lblNoOfHoursPerDay.Text & "'. (Red color indicates invalid data in below grid)."
                    ElseIf lblNoOfHoursPerDay.Text.Length > 2 Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid No. of Hours Per Day '" & lblNoOfHoursPerDay.Text & "'. (Red color indicates invalid data in below grid)."
                    ElseIf lblNoOfHoursPerDay.Text.Trim > 24 Or lblNoOfHoursPerDay.Text.Length > 2 Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid No. of Hours Per Day '" & lblNoOfHoursPerDay.Text & "'. (Red color indicates invalid data in below grid)."
                    End If
                    If lblPerKmCharges.Text.Trim = "" Or lblPerKmCharges.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Per Km Charges. (Red color indicates invalid data in below grid)."
                    ElseIf Regex.IsMatch(lblPerKmCharges.Text, "^\d+(\.\d{1,2})?$") = False Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Per Km Charges '" & lblPerKmCharges.Text & "'. (Red color indicates invalid data in below grid)."
                    ElseIf lblPerKmCharges.Text.Length > 500 Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Per Km Charges exceeded maximum size(max 500 characters). (Red color indicates invalid data in below grid)."
                    End If

                    If lblRemarks.Text.Trim.Length > 500 Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Remarks exceeded maximum size(max 500 characters). (Red color indicates invalid data in below grid)."
                    End If
                End If

                If ddlMasterName.SelectedValue = 7 Then 'Audit Universe
                    Dim lblFunction As New Label, lblSubFunction As New Label, lblProcess As New Label, lblSubProcess As New Label, lblSubProcessKey As New Label
                    Dim iFunctonId As Integer = 0, iSubFunctonId As Integer = 0, iProcessID As Integer = 0, iSubProcessID As Integer = 0

                    lblFunction.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    iFunctonId = objclsExcelUpload.GetFunctionID(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblFunction.Text.Trim))
                    lblSubFunction.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    iSubFunctonId = objclsExcelUpload.GetSubFunctionID(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblSubFunction.Text), iFunctonId)
                    lblProcess.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    iProcessID = objclsExcelUpload.GetProcessID(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblProcess.Text), iSubFunctonId)
                    lblSubProcess.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    lblSubProcessKey.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(5).Text)

                    If lblFunction.Text.Trim = "" Or lblFunction.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Function. (Red color indicates invalid data in below grid)."
                    End If
                    If lblSubFunction.Text.Trim = "" Or lblSubFunction.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Sub Function. (Red color indicates invalid data in below grid)."
                    End If
                    If lblProcess.Text.Trim = "" Or lblProcess.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Process. (Red color indicates invalid data in below grid)."
                    End If
                    If lblSubProcess.Text.Trim = "" Or lblSubProcess.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(4).Font.Bold = True : e.Item.Cells(4).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Sub Process. (Red color indicates invalid data in below grid)."
                    End If
                    If lblSubProcessKey.Text.Trim = "" Or lblSubProcessKey.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Sub Process Key. (Red color indicates invalid data in below grid)."
                    End If
                    If lblSubProcessKey.Text = "0" Or lblSubProcessKey.Text = "1" Then
                    Else
                        e.Item.Cells(5).Font.Bold = True : e.Item.Cells(5).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Sub Process Key. Key should be 0/1. (Red color indicates invalid data in below grid)."
                    End If
                End If

                If ddlMasterName.SelectedValue = 11 Then 'General Master
                    Dim lblType As New Label, lblName As New Label, lblCode As New Label, lblNote As New Label, lblMasterHead As New Label
                    Dim bCheck As Boolean
                    Dim sTableName As String = "", sType As String = ""
                    lblType.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(1).Text)
                    lblName.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(2).Text)
                    lblCode.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(3).Text)
                    lblNote.Text = objclsGRACeGeneral.SafeSQL(e.Item.Cells(4).Text)
                    If lblType.Text.Trim = "" Or lblType.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Type. (Red color indicates invalid data in below grid)."
                    End If
                    If lblName.Text.Trim = "" Or lblName.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Name. (Red color indicates invalid data in below grid)."
                    End If
                    If lblCode.Text.Trim = "" Or lblCode.Text.Trim = "&nbsp:" Then
                        e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                        lblError.Text = "Enter Code. (Red color indicates invalid data in below grid)."
                    End If
                    If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Or UCase(lblType.Text) = "AUDIT CHECK POINT" Or UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Or
                        UCase(lblType.Text) = "AUDIT SIGN OFF" Or UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Or UCase(lblType.Text) = "EXPENSES CHARGES" Or
                        UCase(lblType.Text) = "FREQUENCY" Or UCase(lblType.Text) = "INDUSTRY TYPE" Or UCase(lblType.Text) = "MANAGEMENT" Or
                        UCase(lblType.Text) = "NON-AUDIT TASK" Or UCase(lblType.Text) = "ORGANIZATION TYPE" Or UCase(lblType.Text) = "OTHER EXPENSES" Or
                        UCase(lblType.Text) = "REIMBURSEMENT" Or UCase(lblType.Text) = "TYPE OF TEST" Or UCase(lblType.Text) = "CONCLUSION" Then
                        If UCase(lblType.Text) = "DESIGNATION" Then
                            sTableName = "SAD_GRPDESGN_General_Master"
                            lblMasterHead.Text = "Designation Name '" & lblName.Text & "'"
                        ElseIf UCase(lblType.Text) = "ROLE" Then
                            sTableName = "SAD_GrpOrLvl_General_Master"
                            lblMasterHead.Text = "Role Name '" & lblName.Text & "'"
                        ElseIf UCase(lblType.Text) = "AUDIT CHECK POINT" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Audit Check Point '" & lblName.Text & "'"
                            sType = "AP"
                        ElseIf UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Audit Task/Assignments '" & lblName.Text & "'"
                            sType = "AT"
                        ElseIf UCase(lblType.Text) = "AUDIT SIGN OFF" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Audit Sign Off '" & lblName.Text & "'"
                            sType = "ASF"
                        ElseIf UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Document Request List '" & lblName.Text & "'"
                            sType = "DRL"
                        ElseIf UCase(lblType.Text) = "EXPENSES CHARGES" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Expenses Charges '" & lblName.Text & "'"
                            sType = "EC"
                        ElseIf UCase(lblType.Text) = "FREQUENCY" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Frequency '" & lblName.Text & "'"
                            sType = "FRE"
                        ElseIf UCase(lblType.Text) = "INDUSTRY TYPE" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Industry Type '" & lblName.Text & "'"
                            sType = "IND"
                        ElseIf UCase(lblType.Text) = "MANAGEMENT" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Management '" & lblName.Text & "'"
                            sType = "MNG"
                        ElseIf UCase(lblType.Text) = "NON-AUDIT TASK" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Non-Audit Task '" & lblName.Text & "'"
                            sType = "NAT"
                        ElseIf UCase(lblType.Text) = "ORGANIZATION TYPE" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Organization Type '" & lblName.Text & "'"
                            sType = "ORG"
                        ElseIf UCase(lblType.Text) = "OTHER EXPENSES" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Other Expenses '" & lblName.Text & "'"
                            sType = "OE"
                        ElseIf UCase(lblType.Text) = "REIMBURSEMENT" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Reimbursement '" & lblName.Text & "'"
                            sType = "LE"
                        ElseIf UCase(lblType.Text) = "TYPE OF TEST" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Type of Test '" & lblName.Text & "'"
                            sType = "TOT"
                        ElseIf UCase(lblType.Text) = "CONCLUSION" Then
                            sTableName = "Content_Management_Master"
                            lblMasterHead.Text = "Conclusion '" & lblName.Text & "'"
                            sType = "WPC"
                        End If
                    Else
                        e.Item.Cells(1).Font.Bold = True : e.Item.Cells(1).ForeColor = Drawing.Color.Red
                        lblError.Text = "Invalid Master Type  '" & lblType.Text & "' Type. (Red color indicates invalid data in below grid)."
                    End If
                    If sTableName <> "" Then
                        If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Then 'Designation & Role
                            bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "Mas_Code", 0, "DESGROLE")
                        Else
                            bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "CMM_Code", 0, sType)
                        End If
                        If bCheck = True Then
                            e.Item.Cells(3).Font.Bold = True : e.Item.Cells(3).ForeColor = Drawing.Color.Red
                            lblError.Text = "The Code " & lblCode.Text & " already exist. (Red color indicates invalid data in below grid)."
                        End If

                        If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Then 'Designation & Role
                            bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "Mas_Description", 0, "DESGROLE")
                        Else
                            bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblName.Text.Trim), sTableName, "CMM_Desc", 0, sType)
                        End If
                        If bCheck = True Then
                            e.Item.Cells(2).Font.Bold = True : e.Item.Cells(2).ForeColor = Drawing.Color.Red
                            lblError.Text = "The " & lblMasterHead.Text & " already exist. (Red color indicates invalid data in below grid)."
                        End If
                    End If
                End If
            End If
            imgbtnSave.Visible = False
            If lblError.Text = "" Then
                imgbtnSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgGeneral_ItemDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            If dgGeneral.Items.Count = 0 Then
                lblExcelValidationMsg.Text = "No data to Save." : lblError.Text = "No data to Save."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlMasterName.SelectedValue = 1 Then 'Organisation Structure
                SaveOrganisationStructure()
            End If
            If ddlMasterName.SelectedValue = 2 Then 'Holiday Master
                SaveHolidayMaster()
            End If
            If ddlMasterName.SelectedValue = 3 Then 'Employee Master
                SaveEmployeeDetails()
            End If
            If ddlMasterName.SelectedValue = 4 Then 'Customer Master               
                SaveCustomerMasterDetails()
            End If
            If ddlMasterName.SelectedValue = 5 Then 'Customer User Master            
                SaveCustomerUserDetails()
            End If
            If ddlMasterName.SelectedValue = 6 Then 'Service & Conveyance Master
            End If
            If ddlMasterName.SelectedValue = 7 Then 'Audit Universe
            End If
            If ddlMasterName.SelectedValue = 8 Then 'Risk General Master
            End If
            If ddlMasterName.SelectedValue = 9 Then 'Risk Master
            End If
            If ddlMasterName.SelectedValue = 10 Then 'Control Master
            End If
            If ddlMasterName.SelectedValue = 11 Then 'General Master
                SaveGeneralMaster()
            End If
            If ddlMasterName.SelectedValue = 12 Then 'Mapping of Master
            End If
            If ddlMasterName.SelectedValue = 13 Then 'Work Paper
            End If
            If ddlMasterName.SelectedValue = 14 Then 'Trial Balance
            End If
            If ddlMasterName.SelectedValue = 15 Or ddlMasterName.SelectedValue = 16 Or ddlMasterName.SelectedValue = 17 Then
            End If
            If ddlMasterName.SelectedValue = 18 Or ddlMasterName.SelectedValue = 19 Then
            End If
            If ddlMasterName.SelectedValue = 20 Then
            End If
            If ddlMasterName.SelectedValue = 21 Then
            End If
            If ddlMasterName.SelectedValue = 22 Then
            End If
            If ddlMasterName.SelectedValue = 23 Then
            End If
            If ddlMasterName.SelectedValue = 24 Then
            End If
            If ddlMasterName.SelectedValue = 25 Then
            End If
            If ddlMasterName.SelectedValue = 26 Then
            End If
            If ddlMasterName.SelectedValue = 29 Then
                SaveComplianceTask()
            End If
            If ddlMasterName.SelectedValue = 31 Then
                SaveAuditChecklistMaster()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub SaveOrganisationStructure()
        Dim lblZone As New Label, lblRegion As New Label, lblArea As New Label, lblBranch As New Label, lblZoneIRDACode As New Label, lblRegionIRDACode As New Label
        Dim lblAreaIRDACode As New Label, lblBranchIRDACode As New Label, lblNote As New Label
        Dim lblParentID As New Label, lblCurrentID As New Label, lblDepthID As New Label
        Dim iParentID As Integer, iCurrentID As Integer, iDepthID As Integer
        Dim iErrorLine As Integer = 0
        Dim Arr() As String
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblZone.Text = "" : lblRegion.Text = "" : lblArea.Text = "" : lblBranch.Text = "" : lblZoneIRDACode.Text = "" : lblRegionIRDACode.Text = ""
                lblAreaIRDACode.Text = "" : lblBranchIRDACode.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblZone.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblZoneIRDACode.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblRegion.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblRegionIRDACode.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                    lblArea.Text = dgGeneral.Items(i).Cells(5).Text
                End If
                If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
                    lblAreaIRDACode.Text = dgGeneral.Items(i).Cells(6).Text
                End If
                If dgGeneral.Items(i).Cells(7).Text <> "&nbsp;" Then
                    lblBranch.Text = dgGeneral.Items(i).Cells(7).Text
                End If
                If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
                    lblBranchIRDACode.Text = dgGeneral.Items(i).Cells(8).Text
                End If
                If dgGeneral.Items(i).Cells(9).Text <> "&nbsp;" Then
                    lblNote.Text = dgGeneral.Items(i).Cells(9).Text
                End If
                lblParentID.Text = 1 : lblCurrentID.Text = 0 : lblDepthID.Text = 0
                iParentID = Val(lblParentID.Text) : iCurrentID = Val(lblCurrentID.Text)
                If iCurrentID = 0 Then
                    iDepthID = Val(lblDepthID.Text) + 1
                Else
                    iDepthID = Val(lblDepthID.Text)
                End If
                'Zone
                If lblZone.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Zone. Line No " & iErrorLine & "." : lblError.Text = "Enter Zone. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf lblZone.Text.Trim.Length > 1000 Then
                    lblExcelValidationMsg.Text = "Zone exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "." : lblError.Text = "Zone exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblZoneIRDACode.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Zone Code. Line No " & iErrorLine & "." : lblError.Text = "Enter Zone Code. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblZoneIRDACode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "Zone Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "Zone Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblZoneIRDACode.Text.Trim)), iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Zone Code already exists." : lblError.Text = "Zone Code already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblZone.Text.Trim)), iParentID, iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Zone Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    lblError.Text = "Zone Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'Region
                If lblRegion.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Region. Line No " & iErrorLine & "." : lblError.Text = "Enter Region. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf lblRegion.Text.Trim.Length > 1000 Then
                    lblExcelValidationMsg.Text = "Region exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "." : lblError.Text = "Region exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblRegionIRDACode.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Region Code. Line No " & iErrorLine & "." : lblError.Text = "Enter Region Code. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblRegionIRDACode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "Region Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "Region Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblRegionIRDACode.Text.Trim)), iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Region Code already exists." : lblError.Text = "Region Code already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblRegion.Text.Trim)), iParentID, iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Region Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    lblError.Text = "Region Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'Area
                If lblArea.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Area. Line No " & iErrorLine & "." : lblError.Text = "Enter Area. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf lblArea.Text.Trim.Length > 1000 Then
                    lblExcelValidationMsg.Text = "Area exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "." : lblError.Text = "Area exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblAreaIRDACode.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Area Code. Line No " & iErrorLine & "." : lblError.Text = "Enter Area Code. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblAreaIRDACode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "Area Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "Area Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblAreaIRDACode.Text.Trim)), iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Area Code already exists." : lblError.Text = "Area Code already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblArea.Text.Trim)), iParentID, iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Area Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    lblError.Text = "Area Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'Branch
                If lblBranch.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Branch. Line No " & iErrorLine & "." : lblError.Text = "Enter Branch. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                ElseIf lblBranch.Text.Trim.Length > 1000 Then
                    lblExcelValidationMsg.Text = "Branch exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "." : lblError.Text = "Branch exceeded maximum size(max 1000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblBranchIRDACode.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Branch Code. Line No " & iErrorLine & "." : lblError.Text = "Enter Branch Code. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblBranchIRDACode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "Branch Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "Branch Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgCodeExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblBranchIRDACode.Text.Trim)), iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Branch Code already exists." : lblError.Text = "Branch Code already exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsOrgStructure.CheckOrgNameExistOrNot(sSession.AccessCode, sSession.AccessCodeID, UCase(objclsGRACeGeneral.SafeSQL(lblBranch.Text.Trim)), iParentID, iCurrentID) = True Then
                    lblExcelValidationMsg.Text = "Branch Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    lblError.Text = "Branch Name already exists under - " & objclsOrgStructure.GetParentName(sSession.AccessCode, sSession.AccessCodeID, iParentID) & ""
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblNote.Text.Trim.Length > 2000 Then
                    lblExcelValidationMsg.Text = "Note/Address/Remarks exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "."
                    lblError.Text = "Note/Address/Remarks exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Next

            For i = 0 To dgGeneral.Items.Count - 1
                lblParentID.Text = 1 : lblCurrentID.Text = 0 : lblDepthID.Text = 0
                lblZone.Text = dgGeneral.Items(i).Cells(1).Text
                lblZoneIRDACode.Text = dgGeneral.Items(i).Cells(2).Text
                lblRegion.Text = dgGeneral.Items(i).Cells(3).Text
                lblRegionIRDACode.Text = dgGeneral.Items(i).Cells(4).Text
                lblArea.Text = dgGeneral.Items(i).Cells(5).Text
                lblAreaIRDACode.Text = dgGeneral.Items(i).Cells(6).Text
                lblBranch.Text = dgGeneral.Items(i).Cells(7).Text
                lblBranchIRDACode.Text = dgGeneral.Items(i).Cells(8).Text
                lblNote.Text = dgGeneral.Items(i).Cells(9).Text

                iParentID = Val(lblParentID.Text) : iCurrentID = Val(lblCurrentID.Text)
                If iCurrentID = 0 Then
                    iDepthID = Val(lblDepthID.Text) + 1
                Else
                    iDepthID = Val(lblDepthID.Text)
                End If
                'Zone
                objclsOrgStructure.iOrgnode = 0
                objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(lblZoneIRDACode.Text)
                objclsOrgStructure.sOrgSalesUnitCode = ""
                objclsOrgStructure.sOrgBranchCode = ""
                objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(lblZone.Text)
                objclsOrgStructure.iOrgAppStrength = 0
                If lblNote.Text <> "" Then
                    objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(lblNote.Text)
                Else
                    objclsOrgStructure.sOrgNote = ""
                End If
                objclsOrgStructure.iOrgParent = iParentID
                objclsOrgStructure.iOrgLevelCode = iDepthID
                objclsOrgStructure.sOrgDelflag = "A"
                objclsOrgStructure.sOrgStatus = "A"
                objclsOrgStructure.iOrgCreatedBy = sSession.UserID
                objclsOrgStructure.dOrgCreatedOn = Date.Today
                objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
                Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
                lblParentID.Text = Arr(1) : lblCurrentID.Text = 0 : lblDepthID.Text = iDepthID
                If iCurrentID = 0 Then
                    iDepthID = Val(lblDepthID.Text) + 1
                Else
                    iDepthID = Val(lblDepthID.Text)
                End If
                'Region
                objclsOrgStructure.iOrgnode = 0
                objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(lblRegionIRDACode.Text)
                objclsOrgStructure.sOrgSalesUnitCode = ""
                objclsOrgStructure.sOrgBranchCode = ""
                objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(lblRegion.Text)
                objclsOrgStructure.iOrgAppStrength = 0
                If lblNote.Text <> "" Then
                    objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(lblNote.Text)
                Else
                    objclsOrgStructure.sOrgNote = ""
                End If
                objclsOrgStructure.iOrgParent = Val(lblParentID.Text)
                objclsOrgStructure.iOrgLevelCode = Val(lblDepthID.Text)
                objclsOrgStructure.sOrgDelflag = "A"
                objclsOrgStructure.sOrgStatus = "A"
                objclsOrgStructure.iOrgCreatedBy = sSession.UserID
                objclsOrgStructure.dOrgCreatedOn = Date.Today
                objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
                Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
                lblParentID.Text = Arr(1) : lblCurrentID.Text = 0 : lblDepthID.Text = iDepthID
                If iCurrentID = 0 Then
                    iDepthID = Val(lblDepthID.Text) + 1
                Else
                    iDepthID = Val(lblDepthID.Text)
                End If
                'Area
                objclsOrgStructure.iOrgnode = 0
                objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(lblAreaIRDACode.Text)
                objclsOrgStructure.sOrgSalesUnitCode = ""
                objclsOrgStructure.sOrgBranchCode = ""
                objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(lblArea.Text)
                objclsOrgStructure.iOrgAppStrength = 0
                If lblNote.Text <> "" Then
                    objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(lblNote.Text)
                Else
                    objclsOrgStructure.sOrgNote = ""
                End If
                objclsOrgStructure.iOrgParent = Val(lblParentID.Text)
                objclsOrgStructure.iOrgLevelCode = Val(lblDepthID.Text)
                objclsOrgStructure.sOrgDelflag = "A"
                objclsOrgStructure.sOrgStatus = "A"
                objclsOrgStructure.iOrgCreatedBy = sSession.UserID
                objclsOrgStructure.dOrgCreatedOn = Date.Today
                objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
                Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
                lblParentID.Text = Arr(1) : lblCurrentID.Text = 0 : lblDepthID.Text = iDepthID
                If iCurrentID = 0 Then
                    iDepthID = Val(lblDepthID.Text) + 1
                Else
                    iDepthID = Val(lblDepthID.Text)
                End If
                'Branch
                objclsOrgStructure.iOrgnode = 0
                objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(lblBranchIRDACode.Text)
                objclsOrgStructure.sOrgSalesUnitCode = ""
                objclsOrgStructure.sOrgBranchCode = ""
                objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(lblBranch.Text)
                objclsOrgStructure.iOrgAppStrength = 0
                If lblNote.Text <> "" Then
                    objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(lblNote.Text)
                Else
                    objclsOrgStructure.sOrgNote = ""
                End If
                objclsOrgStructure.iOrgParent = Val(lblParentID.Text)
                objclsOrgStructure.iOrgLevelCode = Val(lblDepthID.Text)
                objclsOrgStructure.sOrgDelflag = "A"
                objclsOrgStructure.sOrgStatus = "A"
                objclsOrgStructure.iOrgCreatedBy = sSession.UserID
                objclsOrgStructure.dOrgCreatedOn = Date.Today
                objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
                Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
                lblParentID.Text = Arr(1) : lblCurrentID.Text = Arr(1) : lblDepthID.Text = iDepthID
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Organization Structure", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveOrganisationStructure" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub SaveHolidayMaster()
        Dim dDate As Date, dFD As Date, dTD As Date
        Dim l As Integer, m As Integer, iErrorLine As Integer = 0
        Dim lblDate As New Label, lblYear As New Label, lblOccasion As New Label
        Dim Fromdate As String = "", Todate As String = ""
        Dim Arr() As String
        Dim iYearID As Integer
        Dim ds As New DataSet
        Dim dtHolidayDetails As New DataTable, dt As New DataTable
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblYear.Text = "" : lblDate.Text = "" : lblOccasion.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblYear.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                lblDate.Text = dgGeneral.Items(i).Cells(2).Text
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblOccasion.Text = objclsGRACeGeneral.SafeSQL(dgGeneral.Items(i).Cells(3).Text)
                End If
                iYearID = objclsExcelUpload.GetYearID(sSession.AccessCode, sSession.AccessCodeID, lblYear.Text)
                If iYearID = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Year. Line No " & iErrorLine & "." : lblError.Text = "Invalid Year. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                ds = objclsYearMaster.BindYearsDetails(sSession.AccessCode, sSession.AccessCodeID, 103, iYearID)
                If ds.Tables(0).Rows.Count <> 0 Then
                    If IsDBNull(ds.Tables(0).Rows(0).Item("YMS_FROMDATE")) = False Then
                        Fromdate = objclsGRACeGeneral.SafeSQL(ds.Tables(0).Rows(0).Item("YMS_FROMDATE"))
                    End If

                    If IsDBNull(ds.Tables(0).Rows(0).Item("YMS_TODATE")) = False Then
                        Todate = objclsGRACeGeneral.SafeSQL(ds.Tables(0).Rows(0).Item("YMS_TODATE"))
                    End If
                End If
                If lblOccasion.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Occasion. Line No " & iErrorLine & "." : lblError.Text = "Enter Occasion. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblOccasion.Text.Trim.Length > 500 Then
                    lblExcelValidationMsg.Text = "Occasion exceeded maximum size(max 500 characters). Line No " & iErrorLine & "." : lblError.Text = "Occasion exceeded maximum size(max 500 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                Dim SDate As String
                Try
                    SDate = Date.ParseExact(lblDate.Text.Trim, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblExcelValidationMsg.Text = "Enter valid Holiday Date(dd/MM/yyyy). Line No " & iErrorLine & "." : lblError.Text = "Enter valid Holiday Date(dd/MM/yyyy). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End Try

                dDate = Date.ParseExact(Trim(lblDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dFD = Date.ParseExact(Trim(Fromdate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dTD = Date.ParseExact(Trim(Todate), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                l = DateDiff(DateInterval.Day, dFD, dTD)
                If l < 0 Then
                    lblExcelValidationMsg.Text = "Holiday Date should be greater than From Date(" & Fromdate & "). Line No " & iErrorLine & "." : lblError.Text = "Holiday Date should be greater than From Date(" & Fromdate & "). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                m = DateDiff(DateInterval.Day, dDate, dTD)
                If m < 0 Then
                    lblExcelValidationMsg.Text = "Holiday Date should be less than To Date(" & Todate & "). Line No " & iErrorLine & "." : lblError.Text = "Holiday Date should be less than To Date(" & Todate & "). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Next

            For i = 0 To dgGeneral.Items.Count - 1
                lblYear.Text = dgGeneral.Items(i).Cells(1).Text
                lblDate.Text = dgGeneral.Items(i).Cells(2).Text
                lblOccasion.Text = objclsGRACeGeneral.SafeSQL(dgGeneral.Items(i).Cells(3).Text)
                iYearID = objclsExcelUpload.GetYearID(sSession.AccessCode, sSession.AccessCodeID, lblYear.Text)

                dDate = Date.ParseExact(Trim(lblDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dtHolidayDetails = objclsHolidayMaster.HolidayMasterDetails(sSession.AccessCode, sSession.AccessCodeID, 103, iYearID)
                Dim DVHolidayDetails As New DataView(dtHolidayDetails)
                DVHolidayDetails.RowFilter = "HDFormat='" & Trim(lblDate.Text) & "'"
                dt = DVHolidayDetails.ToTable
                If dt.Rows.Count = 0 Then
                    objclsHolidayMaster.iHolYearId = iYearID
                    objclsHolidayMaster.dHoldate = dDate
                    objclsHolidayMaster.sHolRemarks = lblOccasion.Text
                    objclsHolidayMaster.iHolCreatedby = sSession.UserID
                    objclsHolidayMaster.dHolCreatedOn = Date.Now()
                    objclsHolidayMaster.iHolUpdatedBy = sSession.UserID
                    objclsHolidayMaster.dHolUpdatedOn = Date.Now()
                    objclsHolidayMaster.sHolDelflag = "A"
                    objclsHolidayMaster.sHolStatus = "C"
                    objclsHolidayMaster.sHolIPAddress = sSession.IPAddress
                    objclsHolidayMaster.iHolCompID = sSession.AccessCodeID
                    Arr = objclsYearMaster.SaveHolidayDetails(sSession.AccessCode, objclsHolidayMaster)
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Heat Map", "Uploaded", iYearID, lblYear.Text, 0, dDate, sSession.IPAddress)
                End If
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveHolidayMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub SaveEmployeeDetails()
    '    Dim lblZone As New Label, lblRegion As New Label, lblArea As New Label, lblBranch As New Label, lblSAPcode As New Label, lblEmployeeName As New Label
    '    Dim lblLoginName As New Label, lblEmail As New Label, lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label, lblModule As New Label
    '    Dim Arr() As String
    '    Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer, ModuleId As Integer = 0, iErrorLine As Integer = 0
    '    Try
    '        lblError.Text = ""
    '        For i = 0 To dgGeneral.Items.Count - 1
    '            iErrorLine = iErrorLine + 1
    '            lblZone.Text = "" : lblRegion.Text = "" : lblArea.Text = ""
    '            lblBranch.Text = "" : lblSAPcode.Text = "" : lblEmployeeName.Text = ""
    '            lblLoginName.Text = "" : lblEmail.Text = "" : lblOfficePhoneNo.Text = ""
    '            lblDesignation.Text = "" : lblRole.Text = "" : lblModule.Text = ""
    '            If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
    '                lblZone.Text = dgGeneral.Items(i).Cells(1).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
    '                lblRegion.Text = dgGeneral.Items(i).Cells(2).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
    '                lblArea.Text = dgGeneral.Items(i).Cells(3).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
    '                lblBranch.Text = dgGeneral.Items(i).Cells(4).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
    '                lblSAPcode.Text = dgGeneral.Items(i).Cells(5).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
    '                lblEmployeeName.Text = dgGeneral.Items(i).Cells(6).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(7).Text <> "&nbsp;" Then
    '                lblLoginName.Text = dgGeneral.Items(i).Cells(7).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
    '                lblEmail.Text = dgGeneral.Items(i).Cells(8).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(9).Text <> "&nbsp;" Then
    '                lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(9).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(10).Text <> "&nbsp;" Then
    '                lblDesignation.Text = dgGeneral.Items(i).Cells(10).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(11).Text <> "&nbsp;" Then
    '                lblRole.Text = dgGeneral.Items(i).Cells(11).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(12).Text <> "&nbsp;" Then
    '                lblModule.Text = dgGeneral.Items(i).Cells(12).Text
    '            End If
    '            If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
    '                lblExcelValidationMsg.Text = "EMP Code already exist. Line No " & iErrorLine & "." : lblError.Text = "EMP Code already exist. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
    '                lblExcelValidationMsg.Text = "Login Name already exist. Line No " & iErrorLine & "." : lblError.Text = "Login Name already exist. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblSAPcode.Text.Trim = "" Then
    '                lblExcelValidationMsg.Text = "Enter EMP Code. Line No " & iErrorLine & "." : lblError.Text = "Enter EMP Code. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblSAPcode.Text.Trim.Length > 10 Then
    '                lblExcelValidationMsg.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblEmployeeName.Text.Trim = "" Then
    '                lblExcelValidationMsg.Text = "Enter Employee Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Employee Name. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblEmployeeName.Text.Trim.Length > 50 Then
    '                lblExcelValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblLoginName.Text.Trim = "" Then
    '                lblExcelValidationMsg.Text = "Enter Login Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Login Name. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblLoginName.Text.Trim.Length > 25 Then
    '                lblExcelValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "." : lblError.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblEmail.Text.Trim.Length > 50 Then
    '                lblExcelValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblOfficePhoneNo.Text.Trim.Length > 15 Then
    '                lblExcelValidationMsg.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "." : lblError.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblDesignation.Text = "" Then
    '                lblExcelValidationMsg.Text = "Select Designation. Line No " & iErrorLine & "." : lblError.Text = "Select Designation. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblRole.Text = "" Then
    '                lblExcelValidationMsg.Text = "Select Role. Line No " & iErrorLine & "." : lblError.Text = "Select Role. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If UCase(lblModule.Text) = "MASTER" Then 'Master
    '                ModuleId = 1
    '            ElseIf UCase(lblModule.Text) = "AUDIT" Then 'Audit
    '                ModuleId = 2
    '            ElseIf UCase(lblModule.Text) = "RISK" Then 'Risk
    '                ModuleId = 3
    '            ElseIf UCase(lblModule.Text) = "COMPLIANCE" Then 'Compliance
    '                ModuleId = 4
    '            Else
    '                lblExcelValidationMsg.Text = "Enter valid Module. Line No " & iErrorLine & "." : lblError.Text = "Enter valid Module. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '        Next

    '        For i = 0 To dgGeneral.Items.Count - 1
    '            lblZone.Text = dgGeneral.Items(i).Cells(1).Text
    '            lblRegion.Text = dgGeneral.Items(i).Cells(2).Text
    '            lblArea.Text = dgGeneral.Items(i).Cells(3).Text
    '            lblBranch.Text = dgGeneral.Items(i).Cells(4).Text
    '            lblSAPcode.Text = dgGeneral.Items(i).Cells(5).Text
    '            lblEmployeeName.Text = dgGeneral.Items(i).Cells(6).Text
    '            lblLoginName.Text = dgGeneral.Items(i).Cells(7).Text
    '            lblEmail.Text = dgGeneral.Items(i).Cells(8).Text
    '            lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(9).Text
    '            lblDesignation.Text = dgGeneral.Items(i).Cells(10).Text
    '            lblRole.Text = dgGeneral.Items(i).Cells(11).Text
    '            lblModule.Text = dgGeneral.Items(i).Cells(12).Text

    '            If lblZone.Text <> "" Then
    '                iZoneID = objclsExcelUpload.CheckZone(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text)
    '                objclsEmployeeMaster.iUsrOrgID = iZoneID
    '                objclsEmployeeMaster.iUsrNode = 1
    '            End If

    '            If lblRegion.Text <> "" Then
    '                iRegionID = objclsExcelUpload.CheckRegion(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text, lblRegion.Text)
    '                objclsEmployeeMaster.iUsrOrgID = iRegionID
    '                objclsEmployeeMaster.iUsrNode = 2
    '                iZoneID = 0
    '            End If

    '            If (lblArea.Text <> "" And lblArea.Text <> "&nbsp;") Then
    '                iAreaID = objclsExcelUpload.CheckArea(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text, lblRegion.Text, lblArea.Text)
    '                objclsEmployeeMaster.iUsrOrgID = iAreaID
    '                objclsEmployeeMaster.iUsrNode = 3
    '                iZoneID = 0 : iRegionID = 0
    '            End If

    '            If (lblBranch.Text <> "" And lblBranch.Text <> "&nbsp;") Then
    '                iBranchID = objclsExcelUpload.CheckBranch(sSession.AccessCode, sSession.AccessCodeID, lblZone.Text, lblRegion.Text, lblArea.Text, lblBranch.Text)
    '                objclsEmployeeMaster.iUsrOrgID = iBranchID
    '                objclsEmployeeMaster.iUsrNode = 4
    '                iZoneID = 0 : iRegionID = 0 : iAreaID = 0
    '            End If
    '            objclsEmployeeMaster.iUserID = 0
    '            objclsEmployeeMaster.sUsrStatus = "C"
    '            objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(lblSAPcode.Text.Trim)
    '            objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(lblEmployeeName.Text.Trim)
    '            objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(lblLoginName.Text.Trim)
    '            objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword("a")
    '            objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(lblEmail.Text.Trim)
    '            objclsEmployeeMaster.sUsrDutyStatus = "W"
    '            objclsEmployeeMaster.sUsrPhoneNo = ""
    '            objclsEmployeeMaster.sUsrMobileNo = ""
    '            objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(lblOfficePhoneNo.Text.Trim)
    '            objclsEmployeeMaster.sUsrOffPhExtn = ""
    '            Dim DesignationID As Integer = objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text)
    '            objclsEmployeeMaster.iUsrDesignation = DesignationID
    '            objclsEmployeeMaster.iUsrCompanyID = 0
    '            Dim RoleID As Integer = objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text)
    '            objclsEmployeeMaster.iUsrRole = RoleID
    '            objclsEmployeeMaster.iUsrLevelGrp = ModuleId
    '            objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = 0
    '            objclsEmployeeMaster.sUsrFlag = "W"
    '            objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
    '            objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
    '            objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
    '            objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
    '            objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
    '            objclsEmployeeMaster.iUsrBCMmodule = 0
    '            objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
    '            objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
    '            objclsEmployeeMaster.iUsrBCMRole = 0
    '            If UCase(lblModule.Text) = "MASTER" Then 'Master
    '                objclsEmployeeMaster.iUsrMasterModule = 1
    '                objclsEmployeeMaster.iUsrMasterRole = ModuleId
    '            ElseIf UCase(lblModule.Text) = "AUDIT" Then 'Audit
    '                objclsEmployeeMaster.iUsrAuditModule = 1
    '                objclsEmployeeMaster.iUsrAuditRole = ModuleId
    '            ElseIf UCase(lblModule.Text) = "RISK" Then 'Audit
    '                objclsEmployeeMaster.iUsrRiskModule = 1
    '                objclsEmployeeMaster.iUsrRiskRole = ModuleId
    '            ElseIf UCase(lblModule.Text) = "COMPLIANCE" Then 'Audit
    '                objclsEmployeeMaster.iUsrComplianceModule = 1
    '                objclsEmployeeMaster.iUsrComplianceRole = ModuleId
    '            End If
    '            objclsEmployeeMaster.iUsrPartner = 0
    '            If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = False Then
    '                Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
    '                objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Arr(1), sSession.IPAddress, "Created")
    '                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Uploaded", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
    '            End If
    '        Next
    '        lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub


    'Changed by Steffi on 01-01-2024
    Public Sub SaveEmployeeDetails()
        Dim lblSAPcode As New Label, lblEmployeeName As New Label
        Dim lblLoginName As New Label, lblEmail As New Label, lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label
        Dim Arr() As String
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer, ModuleId As Integer = 0, iErrorLine As Integer = 0
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblSAPcode.Text = "" : lblEmployeeName.Text = ""
                lblLoginName.Text = "" : lblEmail.Text = "" : lblOfficePhoneNo.Text = ""
                lblDesignation.Text = "" : lblRole.Text = ""

                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblSAPcode.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblEmployeeName.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblLoginName.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblEmail.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                    lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(5).Text
                End If
                If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
                    lblDesignation.Text = dgGeneral.Items(i).Cells(6).Text
                End If
                If dgGeneral.Items(i).Cells(7).Text <> "&nbsp;" Then
                    lblRole.Text = dgGeneral.Items(i).Cells(7).Text
                End If

                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                    lblExcelValidationMsg.Text = "EMP Code already exist. Line No " & iErrorLine & "." : lblError.Text = "EMP Code already exist. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
                    lblExcelValidationMsg.Text = "Login Name already exist. Line No " & iErrorLine & "." : lblError.Text = "Login Name already exist. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'If lblSAPcode.Text.Trim = "" Then
                '    lblExcelValidationMsg.Text = "Enter EMP Code. Line No " & iErrorLine & "." : lblError.Text = "Enter EMP Code. Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'End If
                If lblSAPcode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblEmployeeName.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Employee Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Employee Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblEmployeeName.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblLoginName.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Login Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Login Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblLoginName.Text.Trim.Length > 25 Then
                    lblExcelValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "." : lblError.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblEmail.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblOfficePhoneNo.Text.Trim.Length > 15 Then
                    lblExcelValidationMsg.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "." : lblError.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblDesignation.Text = "" Then
                    lblExcelValidationMsg.Text = "Select Designation. Line No " & iErrorLine & "." : lblError.Text = "Select Designation. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblRole.Text = "" Then
                    lblExcelValidationMsg.Text = "Select Role. Line No " & iErrorLine & "." : lblError.Text = "Select Role. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                ModuleId = 1
            Next

            For i = 0 To dgGeneral.Items.Count - 1

                If dgGeneral.Items(i).Cells(1).Text = "" Or dgGeneral.Items(i).Cells(1).Text = "&nbsp;" Then
                    lblSAPcode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID)
                Else
                    lblSAPcode.Text = dgGeneral.Items(i).Cells(1).Text
                End If

                lblEmployeeName.Text = dgGeneral.Items(i).Cells(2).Text
                lblLoginName.Text = dgGeneral.Items(i).Cells(3).Text
                lblEmail.Text = dgGeneral.Items(i).Cells(4).Text
                lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(5).Text
                lblDesignation.Text = dgGeneral.Items(i).Cells(6).Text
                lblRole.Text = dgGeneral.Items(i).Cells(7).Text

                iZoneID = 0 : iRegionID = 0 : iAreaID = 0


                objclsEmployeeMaster.iUserID = 0
                objclsEmployeeMaster.sUsrStatus = "C"
                objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(lblSAPcode.Text.Trim)
                objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(lblEmployeeName.Text.Trim)
                objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(lblLoginName.Text.Trim)
                objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword("a")
                objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(lblEmail.Text.Trim)
                objclsEmployeeMaster.sUsrDutyStatus = "W"
                objclsEmployeeMaster.sUsrPhoneNo = ""
                objclsEmployeeMaster.sUsrMobileNo = ""
                objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(lblOfficePhoneNo.Text.Trim)
                objclsEmployeeMaster.sUsrOffPhExtn = ""
                Dim DesignationID As Integer = objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text)
                objclsEmployeeMaster.iUsrDesignation = DesignationID
                objclsEmployeeMaster.iUsrCompanyID = 0
                objclsEmployeeMaster.sUsrType = "U"
                objclsEmployeeMaster.iUsrNode = 1
                objclsEmployeeMaster.iUsrOrgID = 1

                If lblRole.Text = "Yes" Then
                    lblRole.Text = "Partner"
                ElseIf lblRole.Text = "No" Then
                    lblRole.Text = "Audit Assistant"
                Else
                    lblRole.Text = "Audit Assistant"
                End If
                Dim RoleID As Integer = objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text)
                objclsEmployeeMaster.iUsrRole = RoleID
                objclsEmployeeMaster.iUsrLevelGrp = ModuleId
                objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = 0
                objclsEmployeeMaster.sUsrFlag = "W"
                objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
                objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
                objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
                objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
                objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
                objclsEmployeeMaster.iUsrBCMmodule = 0
                objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
                objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
                objclsEmployeeMaster.iUsrBCMRole = 0

                objclsEmployeeMaster.iUsrPartner = 0
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = False Then
                    Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Arr(1), sSession.IPAddress, "Created")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Uploaded", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                End If
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub SaveUserDetails()
    '    Dim lblVendor As New Label, lblSAPcode As New Label, lblUserName As New Label, lblLoginName As New Label, lblEmail As New Label
    '    Dim lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label, lblModule As New Label
    '    Dim ModuleId As Integer = 0, iErrorLine As Integer = 0
    '    Dim Arr() As String
    '    Try
    '        lblError.Text = ""
    '        For i = 0 To dgGeneral.Items.Count - 1
    '            iErrorLine = iErrorLine + 1
    '            lblVendor.Text = "" : lblSAPcode.Text = "" : lblUserName.Text = ""
    '            lblLoginName.Text = "" : lblEmail.Text = "" : lblOfficePhoneNo.Text = ""
    '            lblDesignation.Text = "" : lblRole.Text = "" : lblModule.Text = ""
    '            If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
    '                lblVendor.Text = dgGeneral.Items(i).Cells(1).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
    '                lblSAPcode.Text = dgGeneral.Items(i).Cells(2).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
    '                lblUserName.Text = dgGeneral.Items(i).Cells(3).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
    '                lblLoginName.Text = dgGeneral.Items(i).Cells(4).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
    '                lblEmail.Text = dgGeneral.Items(i).Cells(5).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
    '                lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(6).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(7).Text <> "&nbsp;" Then
    '                lblDesignation.Text = dgGeneral.Items(i).Cells(7).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
    '                lblRole.Text = dgGeneral.Items(i).Cells(8).Text
    '            End If
    '            If dgGeneral.Items(i).Cells(9).Text <> "&nbsp;" Then
    '                lblModule.Text = dgGeneral.Items(i).Cells(9).Text
    '            End If

    '            If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
    '                lblExcelValidationMsg.Text = "EMP Code already exist. Line No " & iErrorLine & "." : lblError.Text = "EMP Code already exist. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
    '                lblExcelValidationMsg.Text = "Login Name already exist. Line No " & iErrorLine & "." : lblError.Text = "Login Name already exist. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)

    '                Exit Sub
    '            End If
    '            If lblSAPcode.Text.Trim = "" Then
    '                lblExcelValidationMsg.Text = "Enter EMP Code. Line No " & iErrorLine & "." : lblError.Text = "Enter EMP Code. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)

    '                Exit Sub
    '            End If
    '            If lblSAPcode.Text.Trim.Length > 10 Then
    '                lblExcelValidationMsg.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblUserName.Text.Trim = "" Then
    '                lblExcelValidationMsg.Text = "Enter User Name. Line No " & iErrorLine & "." : lblError.Text = "Enter User Name. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblUserName.Text.Trim.Length > 50 Then
    '                lblExcelValidationMsg.Text = "User Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "User Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblLoginName.Text.Trim = "" Then
    '                lblExcelValidationMsg.Text = "Enter Login Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Login Name. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '            If lblLoginName.Text.Trim.Length > 25 Then
    '                lblExcelValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "." : lblError.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If


    '            If lblEmail.Text.Trim.Length > 50 Then
    '                lblExcelValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblOfficePhoneNo.Text.Trim.Length > 15 Then
    '                lblExcelValidationMsg.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "." : lblError.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblDesignation.Text = "" Then
    '                lblExcelValidationMsg.Text = "Select Designation. Line No " & iErrorLine & "." : lblError.Text = "Select Designation. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If lblRole.Text = "" Then
    '                lblExcelValidationMsg.Text = "Select Role. Line No " & iErrorLine & "." : lblError.Text = "Select Role. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If

    '            If UCase(lblModule.Text) = "MASTER" Then 'Master
    '                ModuleId = 1
    '            ElseIf UCase(lblModule.Text) = "AUDIT" Then 'Audit
    '                ModuleId = 2
    '            Else
    '                lblExcelValidationMsg.Text = "Enter valid Module. Line No " & iErrorLine & "." : lblError.Text = "Enter valid Module. Line No " & iErrorLine & "."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
    '                Exit Sub
    '            End If
    '        Next

    '        For i = 0 To dgGeneral.Items.Count - 1
    '            lblVendor.Text = dgGeneral.Items(i).Cells(1).Text
    '            lblSAPcode.Text = dgGeneral.Items(i).Cells(2).Text
    '            lblUserName.Text = dgGeneral.Items(i).Cells(3).Text
    '            lblLoginName.Text = dgGeneral.Items(i).Cells(4).Text
    '            lblEmail.Text = dgGeneral.Items(i).Cells(5).Text
    '            lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(6).Text
    '            lblDesignation.Text = dgGeneral.Items(i).Cells(7).Text
    '            lblRole.Text = dgGeneral.Items(i).Cells(8).Text
    '            lblModule.Text = dgGeneral.Items(i).Cells(9).Text

    '            objclsEmployeeMaster.iUserID = 0
    '            objclsEmployeeMaster.sUsrStatus = "C"
    '            objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(lblSAPcode.Text.Trim)
    '            objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(lblUserName.Text.Trim)
    '            objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(lblLoginName.Text.Trim)
    '            objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword("a")
    '            objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(lblEmail.Text.Trim)
    '            objclsEmployeeMaster.iUsrLevelGrp = ModuleId
    '            objclsEmployeeMaster.sUsrDutyStatus = "W"
    '            objclsEmployeeMaster.sUsrPhoneNo = ""
    '            objclsEmployeeMaster.sUsrMobileNo = ""
    '            objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(lblOfficePhoneNo.Text.Trim)
    '            objclsEmployeeMaster.sUsrOffPhExtn = ""
    '            objclsEmployeeMaster.iUsrDesignation = objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text)
    '            objclsEmployeeMaster.iUsrCompanyID = objclsExcelUpload.CheckVendor(sSession.AccessCode, sSession.AccessCodeID, lblVendor.Text)
    '            objclsEmployeeMaster.iUsrRole = objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text)
    '            objclsEmployeeMaster.sUsrFlag = "W"
    '            objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
    '            objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = 0
    '            objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
    '            objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
    '            objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
    '            objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
    '            objclsEmployeeMaster.iUsrBCMmodule = 0

    '            objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
    '            objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
    '            objclsEmployeeMaster.iUsrBCMRole = 0
    '            If UCase(lblModule.Text) = "MASTER" Then 'Master
    '                objclsEmployeeMaster.iUsrMasterModule = 1
    '                objclsEmployeeMaster.iUsrMasterRole = ModuleId
    '            ElseIf UCase(lblModule.Text) = "AUDIT" Then 'Audit
    '                objclsEmployeeMaster.iUsrAuditModule = 1
    '                objclsEmployeeMaster.iUsrAuditRole = ModuleId
    '            End If
    '            objclsEmployeeMaster.iUsrPartner = 0
    '            If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = False Then
    '                Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
    '                objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Arr(1), sSession.IPAddress, "Created")
    '                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "User Master", "Uploaded", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
    '            End If
    '        Next
    '        lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveUserDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub

    'Changed by Steffi on 01-01-2024
    Private Sub SaveCustomerUserDetails()
        Dim lblVendor As New Label, lblSAPcode As New Label, lblUserName As New Label, lblLoginName As New Label, lblEmail As New Label
        Dim lblOfficePhoneNo As New Label, lblDesignation As New Label, lblRole As New Label, lblModule As New Label
        Dim ModuleId As Integer = 0, iErrorLine As Integer = 0
        Dim Arr() As String
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblVendor.Text = "" : lblSAPcode.Text = "" : lblUserName.Text = ""
                lblLoginName.Text = "" : lblEmail.Text = "" : lblOfficePhoneNo.Text = ""
                lblDesignation.Text = "" : lblRole.Text = "" : lblModule.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblVendor.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblSAPcode.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblUserName.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblLoginName.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                    lblEmail.Text = dgGeneral.Items(i).Cells(5).Text
                End If
                If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
                    lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(6).Text
                End If

                If lblVendor.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Customer Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Customer Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = True Then
                    lblExcelValidationMsg.Text = "EMP Code already exist. Line No " & iErrorLine & "." : lblError.Text = "EMP Code already exist. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, lblLoginName.Text) = True Then
                    lblExcelValidationMsg.Text = "Login Name already exist. Line No " & iErrorLine & "." : lblError.Text = "Login Name already exist. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)

                    Exit Sub
                End If
                'If lblSAPcode.Text.Trim = "" Then
                '    lblExcelValidationMsg.Text = "Enter EMP Code. Line No " & iErrorLine & "." : lblError.Text = "Enter EMP Code. Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)

                '    Exit Sub
                'End If
                If lblSAPcode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "EMP Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblUserName.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter User Name. Line No " & iErrorLine & "." : lblError.Text = "Enter User Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblUserName.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "User Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "User Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblLoginName.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Login Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Login Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblLoginName.Text.Trim.Length > 25 Then
                    lblExcelValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "." : lblError.Text = "Login Name exceeded maximum size(max 25 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If


                If lblEmail.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblOfficePhoneNo.Text.Trim.Length > 15 Then
                    lblExcelValidationMsg.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "." : lblError.Text = "Office Phone No. exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                ModuleId = 1

            Next

            For i = 0 To dgGeneral.Items.Count - 1
                lblVendor.Text = dgGeneral.Items(i).Cells(1).Text

                If dgGeneral.Items(i).Cells(2).Text = "" Or dgGeneral.Items(i).Cells(2).Text = "&nbsp;" Then
                    lblSAPcode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID)
                Else
                    lblSAPcode.Text = dgGeneral.Items(i).Cells(2).Text
                End If

                lblUserName.Text = dgGeneral.Items(i).Cells(3).Text
                lblLoginName.Text = dgGeneral.Items(i).Cells(4).Text
                lblEmail.Text = dgGeneral.Items(i).Cells(5).Text
                lblOfficePhoneNo.Text = dgGeneral.Items(i).Cells(6).Text

                objclsEmployeeMaster.iUserID = 0
                objclsEmployeeMaster.sUsrStatus = "C"
                objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(lblSAPcode.Text.Trim)
                objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(lblUserName.Text.Trim)
                objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(lblLoginName.Text.Trim)
                objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword("a")
                objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(lblEmail.Text.Trim)
                objclsEmployeeMaster.iUsrLevelGrp = ModuleId
                objclsEmployeeMaster.sUsrDutyStatus = "W"
                objclsEmployeeMaster.sUsrPhoneNo = ""
                objclsEmployeeMaster.sUsrMobileNo = ""
                objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(lblOfficePhoneNo.Text.Trim)
                objclsEmployeeMaster.sUsrOffPhExtn = ""
                objclsEmployeeMaster.iUsrDesignation = 0
                ' objclsEmployeeMaster.iUsrDesignation = objclsExcelUpload.CheckDesignation(sSession.AccessCode, sSession.AccessCodeID, lblDesignation.Text)
                objclsEmployeeMaster.iUsrCompanyID = objclsExcelUpload.CheckVendor(sSession.AccessCode, sSession.AccessCodeID, lblVendor.Text)
                'objclsEmployeeMaster.iUsrRole = objclsExcelUpload.CheckRole(sSession.AccessCode, sSession.AccessCodeID, lblRole.Text)
                objclsEmployeeMaster.iUsrRole = 0
                objclsEmployeeMaster.sUsrFlag = "W"
                objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
                objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = 0
                objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
                objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
                objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
                objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
                objclsEmployeeMaster.iUsrBCMmodule = 0
                objclsEmployeeMaster.iUsrNode = 0
                objclsEmployeeMaster.iUsrOrgID = 0

                objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
                objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
                objclsEmployeeMaster.iUsrBCMRole = 0
                'If UCase(lblModule.Text) = "MASTER" Then 'Master
                '    objclsEmployeeMaster.iUsrMasterModule = 1
                '    objclsEmployeeMaster.iUsrMasterRole = ModuleId
                'ElseIf UCase(lblModule.Text) = "AUDIT" Then 'Audit
                '    objclsEmployeeMaster.iUsrAuditModule = 1
                '    objclsEmployeeMaster.iUsrAuditRole = ModuleId
                'End If
                objclsEmployeeMaster.iUsrMasterModule = 1
                objclsEmployeeMaster.iUsrMasterRole = ModuleId
                objclsEmployeeMaster.iUsrPartner = 0
                objclsEmployeeMaster.sUsrType = "C"

                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, lblSAPcode.Text) = False Then
                    Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, Arr(1), sSession.IPAddress, "Created")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "User Master", "Uploaded", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                End If
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveUserDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub SaveGeneralMaster()
        Dim lblType As New Label, lblName As New Label, lblCode As New Label, lblNote As New Label
        Dim sTableName As String = "", sHeadingName As String = "", sType As String = ""
        Dim Arr() As String, bCheck As Boolean
        Dim iErrorLine As Integer = 0
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblType.Text = "" : lblName.Text = "" : lblCode.Text = "" : lblNote.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblType.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblName.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblCode.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblNote.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Or UCase(lblType.Text) = "AUDIT CHECK POINT" Or UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Or
                        UCase(lblType.Text) = "AUDIT SIGN OFF" Or UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Or UCase(lblType.Text) = "EXPENSES CHARGES" Or
                        UCase(lblType.Text) = "FREQUENCY" Or UCase(lblType.Text) = "INDUSTRY TYPE" Or UCase(lblType.Text) = "MANAGEMENT" Or
                        UCase(lblType.Text) = "NON-AUDIT TASK" Or UCase(lblType.Text) = "ORGANIZATION TYPE" Or UCase(lblType.Text) = "OTHER EXPENSES" Or
                        UCase(lblType.Text) = "REIMBURSEMENT" Or UCase(lblType.Text) = "TYPE OF TEST" Or UCase(lblType.Text) = "CONCLUSION" Then
                    If UCase(lblType.Text) = "DESIGNATION" Then
                        sTableName = "SAD_GRPDESGN_General_Master"
                        sHeadingName = "Designation Name '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "ROLE" Then
                        sTableName = "SAD_GrpOrLvl_General_Master"
                        sHeadingName = "Role Name '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "AUDIT CHECK POINT" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Audit Check Point '" & lblName.Text & "'"
                        sType = "AP"
                    ElseIf UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Audit Task/Assignments '" & lblName.Text & "'"
                        sType = "AT"
                    ElseIf UCase(lblType.Text) = "AUDIT SIGN OFF" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Audit Sign Off '" & lblName.Text & "'"
                        sType = "ASF"
                    ElseIf UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Document Request List '" & lblName.Text & "'"
                        sType = "DRL"
                    ElseIf UCase(lblType.Text) = "EXPENSES CHARGES" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Expenses Charges '" & lblName.Text & "'"
                        sType = "EC"
                    ElseIf UCase(lblType.Text) = "FREQUENCY" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Frequency '" & lblName.Text & "'"
                        sType = "FRE"
                    ElseIf UCase(lblType.Text) = "INDUSTRY TYPE" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Industry Type '" & lblName.Text & "'"
                        sType = "IND"
                    ElseIf UCase(lblType.Text) = "MANAGEMENT" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Management '" & lblName.Text & "'"
                        sType = "MNG"
                    ElseIf UCase(lblType.Text) = "NON-AUDIT TASK" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Non-Audit Task '" & lblName.Text & "'"
                        sType = "NAT"
                    ElseIf UCase(lblType.Text) = "ORGANIZATION TYPE" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Organization Type '" & lblName.Text & "'"
                        sType = "ORG"
                    ElseIf UCase(lblType.Text) = "OTHER EXPENSES" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Other Expenses '" & lblName.Text & "'"
                        sType = "OE"
                    ElseIf UCase(lblType.Text) = "REIMBURSEMENT" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Reimbursement '" & lblName.Text & "'"
                        sType = "LE"
                    ElseIf UCase(lblType.Text) = "TYPE OF TEST" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Type of Test '" & lblName.Text & "'"
                        sType = "TOT"
                    ElseIf UCase(lblType.Text) = "CONCLUSION" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Conclusion '" & lblName.Text & "'"
                        sType = "WPC"
                    End If
                Else
                    lblType.Font.Bold = True : lblType.ForeColor = Drawing.Color.Red
                    lblExcelValidationMsg.Text = "Invalid Master Type '" & lblType.Text & "' Type. Line No " & iErrorLine & "."
                    lblError.Text = "Invalid Master Type '" & lblType.Text & "' Type . Line No " & iErrorLine & ".(Red color indicates invalid data In below grid)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblCode.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Code. Line No " & iErrorLine & "." : lblError.Text = "Enter Code. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblCode.Text.Trim.Length > 10 Then
                    lblExcelValidationMsg.Text = "Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "." : lblError.Text = "Code exceeded maximum size(max 10 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "Mas_Code", 0, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "CMM_Code", 0, sType)
                End If
                If bCheck = True Then
                    lblCode.Font.Bold = True : lblCode.ForeColor = Drawing.Color.Red
                    lblError.Text = "The Code " & lblCode.Text & "already exist. (Red color indicates invalid data in below grid)."
                    Exit Sub
                End If

                If lblName.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter " & lblType.Text & "Name. Line No " & iErrorLine & "." : lblError.Text = "Enter " & lblType.Text & "Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblName.Text.Trim.Length > 100 Then
                    lblExcelValidationMsg.Text = lblType.Text & "Name exceeded maximum size(Max 100 characters). Line No " & iErrorLine & "." : lblError.Text = lblType.Text & "Name exceeded maximum size(Max 100 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Then 'Designation & Role
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "Mas_Description", 0, "DESGROLE")
                Else
                    bCheck = objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblName.Text.Trim), sTableName, "CMM_Desc", 0, sType)
                End If
                If bCheck = True Then
                    lblName.Font.Bold = True : lblName.ForeColor = Drawing.Color.Red
                    lblError.Text = "The " & sHeadingName & "already exist. (Red color indicates invalid data in below grid)."
                    Exit Sub
                End If
                If lblNote.Text.Trim.Length > 100 Then
                    lblExcelValidationMsg.Text = "Notes exceeded maximum size(max 100 characters). Line No " & iErrorLine & "." : lblError.Text = "Notes exceeded maximum size(max 100 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Next

            For i = 0 To dgGeneral.Items.Count - 1
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblType.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblName.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblCode.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblNote.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Or UCase(lblType.Text) = "AUDIT CHECK POINT" Or UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Or
                        UCase(lblType.Text) = "AUDIT SIGN OFF" Or UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Or UCase(lblType.Text) = "EXPENSES CHARGES" Or
                        UCase(lblType.Text) = "FREQUENCY" Or UCase(lblType.Text) = "INDUSTRY TYPE" Or UCase(lblType.Text) = "MANAGEMENT" Or
                        UCase(lblType.Text) = "NON-AUDIT TASK" Or UCase(lblType.Text) = "ORGANIZATION TYPE" Or UCase(lblType.Text) = "OTHER EXPENSES" Or
                        UCase(lblType.Text) = "REIMBURSEMENT" Or UCase(lblType.Text) = "TYPE OF TEST" Or UCase(lblType.Text) = "CONCLUSION" Then
                    If UCase(lblType.Text) = "DESIGNATION" Then
                        sTableName = "SAD_GRPDESGN_General_Master"
                        sHeadingName = "Designation Name '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "ROLE" Then
                        sTableName = "SAD_GrpOrLvl_General_Master"
                        sHeadingName = "Role Name '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "AUDIT CHECK POINT" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Audit Check Point '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Audit Task/Assignments '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "AUDIT SIGN OFF" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Audit Sign Off '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Document Request List '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "EXPENSES CHARGES" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Expenses Charges '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "FREQUENCY" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Frequency '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "INDUSTRY TYPE" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Industry Type '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "MANAGEMENT" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Management '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "NON-AUDIT TASK" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Non-Audit Task '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "ORGANIZATION TYPE" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Organization Type '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "OTHER EXPENSES" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Other Expenses '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "REIMBURSEMENT" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Reimbursement '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "TYPE OF TEST" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Type of Test '" & lblName.Text & "'"
                    ElseIf UCase(lblType.Text) = "CONCLUSION" Then
                        sTableName = "Content_Management_Master"
                        sHeadingName = "Conclusion '" & lblName.Text & "'"
                    End If
                End If
                If UCase(lblType.Text) = "DESIGNATION" Or UCase(lblType.Text) = "ROLE" Then 'Designation & Role
                    If objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "Mas_Code", 0, "DESGROLE") = False Then
                        If objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblName.Text.Trim), sTableName, "Mas_Description", 0, "DESGROLE") = False Then
                            Arr = objclsAdminMaster.SaveOrUpdateDtls(sSession.AccessCode, sSession.AccessCodeID, 0, Trim(objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim)), Trim(objclsGRACeGeneral.SafeSQL(lblName.Text.Trim)), Trim(objclsGRACeGeneral.SafeSQL(lblNote.Text.Trim)), sTableName, sSession.UserID, sSession.IPAddress)
                            objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, Arr(1), sTableName, sSession.UserID, sSession.IPAddress, "W", "DESGROLE")
                            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Uploaded", sSession.YearID, sSession.YearName, 0, ddlMasterName.SelectedItem.Text, sSession.IPAddress)
                        End If
                    End If
                Else
                    If UCase(lblType.Text) = "AUDIT CHECK POINT" Then
                        objclsAdminMaster.sCategory = "AP"
                    ElseIf UCase(lblType.Text) = "AUDIT TASK/ASSIGNMENTS" Then
                        objclsAdminMaster.sCategory = "AT"
                    ElseIf UCase(lblType.Text) = "AUDIT SIGN OFF" Then
                        objclsAdminMaster.sCategory = "ASF"
                    ElseIf UCase(lblType.Text) = "DOCUMENT REQUEST LIST" Then
                        objclsAdminMaster.sCategory = "DRL"
                    ElseIf UCase(lblType.Text) = "EXPENSES CHARGES" Then
                        objclsAdminMaster.sCategory = "EC"
                    ElseIf UCase(lblType.Text) = "FREQUENCY" Then
                        objclsAdminMaster.sCategory = "FRE"
                    ElseIf UCase(lblType.Text) = "INDUSTRY TYPE" Then
                        objclsAdminMaster.sCategory = "IND"
                    ElseIf UCase(lblType.Text) = "MANAGEMENT" Then
                        objclsAdminMaster.sCategory = "MNG"
                    ElseIf UCase(lblType.Text) = "NON-AUDIT TASK" Then
                        objclsAdminMaster.sCategory = "NAT"
                    ElseIf UCase(lblType.Text) = "ORGANIZATION TYPE" Then
                        objclsAdminMaster.sCategory = "ORG"
                    ElseIf UCase(lblType.Text) = "OTHER EXPENSES" Then
                        objclsAdminMaster.sCategory = "OE"
                    ElseIf UCase(lblType.Text) = "REIMBURSEMENT" Then
                        objclsAdminMaster.sCategory = "LE"
                    ElseIf UCase(lblType.Text) = "TYPE OF TEST" Then
                        objclsAdminMaster.sCategory = "TOT"
                    ElseIf UCase(lblType.Text) = "CONCLUSION" Then
                        objclsAdminMaster.sCategory = "WPC"
                    End If
                    objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                    objclsAdminMaster.sCode = objclsAdminMaster.sCategory & "_" & objclsAdminMaster.iID
                    objclsAdminMaster.iRiskCategory = 0
                    objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(lblName.Text.Trim)
                    objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(lblNote.Text.Trim)
                    objclsAdminMaster.iKeyComponent = 0
                    objclsAdminMaster.sModule = "A"
                    objclsAdminMaster.sDelflag = "W"
                    objclsAdminMaster.sStatus = "C"
                    objclsAdminMaster.iCrBy = sSession.UserID
                    objclsAdminMaster.iUpdatedBy = sSession.UserID
                    objclsAdminMaster.sIpAddress = sSession.IPAddress
                    objclsAdminMaster.iCompId = sSession.AccessCodeID
                    If objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCode.Text.Trim), sTableName, "CMM_Code", 0, objclsAdminMaster.sCategory) = False Then
                        If objclsAdminMaster.CheckExistingDetails(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblName.Text.Trim), sTableName, "CMM_Desc", 0, objclsAdminMaster.sCategory) = False Then
                            Arr = objclsAdminMaster.SaveMasterDetails(sSession.AccessCode, objclsAdminMaster)
                            objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, Arr(1), sTableName, sSession.UserID, sSession.IPAddress, "W", "OTHERS")
                            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Uploaded", sSession.YearID, sSession.YearName, 0, ddlMasterName.SelectedItem.Text, sSession.IPAddress)
                        End If
                    End If
                End If
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveGeneralMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            lblError.Text = "" : imgbtnSave.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnClose_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oYearID As New Object, oAssignmentID As New Object, oBCMAssignmentID As New Object, oCustID As New Object
        Dim oFunID As New Object, oSFID As New Object, oAStartDateID As New Object, oAEndDateID As New Object, oAReportTitleID As New Object, oBranchID As New Object
        Dim iFormID As Integer
        Try
            If Request.QueryString("FormID") IsNot Nothing Then
                iFormID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FormID")))
            End If
            If Request.QueryString("YearID") IsNot Nothing Then
                oYearID = HttpUtility.UrlDecode(Request.QueryString("YearID"))
            End If
            If Request.QueryString("AssignmentID") IsNot Nothing Then
                oAssignmentID = HttpUtility.UrlDecode(Request.QueryString("AssignmentID"))
            End If
            If Request.QueryString("CustomerID") IsNot Nothing Then
                oCustID = HttpUtility.UrlDecode(Request.QueryString("CustomerID"))
            End If
            If Request.QueryString("FunctionID") IsNot Nothing Then
                oFunID = HttpUtility.UrlDecode(Request.QueryString("FunctionID"))
            End If
            If Request.QueryString("SubFunctionID") IsNot Nothing Then
                oSFID = HttpUtility.UrlDecode(Request.QueryString("SubFunctionID"))
            End If
            If Request.QueryString("AStartDate") IsNot Nothing Then
                oAStartDateID = HttpUtility.UrlDecode(Request.QueryString("AStartDate"))
            End If
            If Request.QueryString("AEndDate") IsNot Nothing Then
                oAEndDateID = HttpUtility.UrlDecode(Request.QueryString("AEndDate"))
            End If
            If Request.QueryString("AReportTitle") IsNot Nothing Then
                oAReportTitleID = HttpUtility.UrlDecode(Request.QueryString("AReportTitle"))
            End If
            If Request.QueryString("BranchID") IsNot Nothing Then
                oBranchID = HttpUtility.UrlDecode(Request.QueryString("BranchID"))
            End If

            If iFormID = 18 Then
                Response.Redirect(String.Format("~/Risk/FRRITDashboard.aspx?CustomerID={0}&AssignmentID={1}&FunctionID={2}&SubFunctionID={3}", oCustID, oAssignmentID, oFunID, oSFID), False) 'Risk/FRRITDashboard
            End If
            If iFormID = 19 Then
                Response.Redirect(String.Format("~/Risk/KCCITDashboard.aspx?CustomerID={0}&AssignmentID={1}&FunctionID={2}&SubFunctionID={3}", oCustID, oAssignmentID, oFunID, oSFID), False) 'Risk/KCCITDashboard
            End If
            If iFormID = 20 Then
                Response.Redirect(String.Format("~/Risk/KRIStatus.aspx"), False) 'Risk/KRIStatus
            End If
            If iFormID = 21 Then
                Response.Redirect(String.Format("~/Risk/KIRTracker.aspx"), False) 'Risk/KIRTracker
            End If
            If iFormID = 22 Then
                Response.Redirect(String.Format("~/Risk/BRRIssueTracker.aspx?CustomerID={0}&AssignmentID={1}", oCustID, oAssignmentID), False) 'Risk/BRRIssueTracker
            End If
            If iFormID = 23 Then
                Response.Redirect(String.Format("~/Risk/BRRChecklist.aspx?CustomerID={0}&AssignmentID={1}&AStartDate={2}&AEndDate={3}&AReportTitle={4}", oCustID, oAssignmentID, oAStartDateID, oAEndDateID, oAReportTitleID), False) 'Risk/BRRChecklist
            End If
            If iFormID = 24 Then
                Response.Redirect(String.Format("~/Risk/RCSAAssign.aspx?YearID={0}&CustomerID={1}&FunctionID={2}", oYearID, oCustID, oFunID), False) 'Risk/RCSAAssign
            End If
            If iFormID = 25 Then
                Response.Redirect(String.Format("~/Risk/RAConduct.aspx?YearID={0}&CustomerID={1}&FunctionID={2}", oYearID, oCustID, oFunID), False) 'Risk/RAConduct
            End If
            If iFormID = 26 Then
                Response.Redirect(String.Format("~/Compliance/CRCITDashboard.aspx?CustomerID={1}&AssignmentID={0}", oCustID, oAssignmentID), False) 'Compliance/CRCITDashboard
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Function LoadComplianceTask(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Customer Name")
            dtTable.Columns.Add("Partner")
            dtTable.Columns.Add("Organization Type")
            dtTable.Columns.Add("Financial Year")
            dtTable.Columns.Add("Employee")
            dtTable.Columns.Add("Assignment/Task")
            dtTable.Columns.Add("Sub Task")
            dtTable.Columns.Add("Start Date")
            dtTable.Columns.Add("Completed Date")
            dtTable.Columns.Add("Work Status")
            dtTable.Columns.Add("Comments")
            dtTable.Columns.Add("Billing Status")
            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Customer Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Partner") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Organization Type") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("Financial Year") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("Employee") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("Assignment/Task") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                    If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("Sub Task") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
                    Else
                        If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                            If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                                dRow("Sub Task") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                            End If
                        End If
                    End If
                Else
                    If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                        If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                            dRow("Sub Task") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                        End If
                    End If
                End If
                Dim sCurrentDate = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                    If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                        dRow("Start Date") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
                    Else
                        dRow("Start Date") = sCurrentDate
                    End If
                Else
                    dRow("Start Date") = sCurrentDate
                End If
                If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                    If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
                        dRow("Completed Date") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(8))
                    Else
                        If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                            If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                                dRow("Completed Date") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
                            Else
                                dRow("Completed Date") = sCurrentDate
                            End If
                        Else
                            dRow("Completed Date") = sCurrentDate
                        End If
                    End If
                Else
                    If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                        If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                            dRow("Completed Date") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
                        Else
                            dRow("Completed Date") = sCurrentDate
                        End If
                    Else
                        dRow("Completed Date") = sCurrentDate
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                    If dtStock.Rows(i).Item(9).ToString <> "&nbsp;" Then
                        dRow("Work Status") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(9))
                        If objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(9).Trim.ToUpper()) = "COMPLETED" Then
                            dRow("Billing Status") = "Billable"
                        Else
                            dRow("Billing Status") = "Non-Billable"
                        End If
                    Else
                        dRow("Billing Status") = "Non-Billable"
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                    If dtStock.Rows(i).Item(10).ToString <> "&nbsp;" Then
                        dRow("Comments") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(10))
                    End If
                End If
                'If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
                '    If dtStock.Rows(i).Item(11).ToString <> "&nbsp;" Then
                '        dRow("Billing Status") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(11))
                '    End If
                'End If
                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadComplianceTask" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub SaveComplianceTask()
        Dim lblCustomerName As New Label, lblPartner As New Label, lblOrganizationType As New Label, lblFinancialYear As New Label, lblEmployee As New Label, lblTask As New Label
        Dim lblSubTask As New Label, lblStartDate As New Label, lblCompletedDate As New Label, lblWorkStatus As New Label, lblComments As New Label, lblBillingStatus As New Label
        Dim objstrCust As New strCustMaster, objclsCustDashbord As New clsCustDashbord
        Dim iCustomerId As Integer = 0, iOrganizationTypeId As Integer = 0, iFinancialYearId As Integer = 0, iPartnerID As Integer, iEmployeeId As Integer = 0
        Dim iTaskID As Integer, iSubTaskId As Integer = 0, iWorkStatusId As Integer = 0, iBillingStatusId As Integer = 0, iErrorLine As Integer = 0
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblCustomerName.Text = "" : lblPartner.Text = "" : lblOrganizationType.Text = ""
                lblFinancialYear.Text = "" : lblEmployee.Text = "" : lblTask.Text = ""
                lblSubTask.Text = "" : lblStartDate.Text = "" : lblCompletedDate.Text = ""
                lblWorkStatus.Text = "" : lblComments.Text = "" : lblBillingStatus.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblCustomerName.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblPartner.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblOrganizationType.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblFinancialYear.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                    lblEmployee.Text = dgGeneral.Items(i).Cells(5).Text
                End If
                If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
                    lblTask.Text = dgGeneral.Items(i).Cells(6).Text
                End If
                If dgGeneral.Items(i).Cells(7).Text <> "&nbsp;" Then
                    lblSubTask.Text = dgGeneral.Items(i).Cells(7).Text
                End If
                If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
                    lblStartDate.Text = dgGeneral.Items(i).Cells(8).Text
                End If
                If dgGeneral.Items(i).Cells(9).Text <> "&nbsp;" Then
                    lblCompletedDate.Text = dgGeneral.Items(i).Cells(9).Text
                End If
                If dgGeneral.Items(i).Cells(10).Text <> "&nbsp;" Then
                    lblWorkStatus.Text = dgGeneral.Items(i).Cells(10).Text
                End If
                If dgGeneral.Items(i).Cells(11).Text <> "&nbsp;" Then
                    lblComments.Text = dgGeneral.Items(i).Cells(11).Text
                End If
                If dgGeneral.Items(i).Cells(12).Text <> "&nbsp;" Then
                    lblBillingStatus.Text = dgGeneral.Items(i).Cells(12).Text
                End If

                If lblCustomerName.Text.Trim = "" Or lblCustomerName.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Customer Name. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Customer Name. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                If lblCustomerName.Text.Trim.Length > 150 Then
                    lblExcelValidationMsg.Text = "Customer Name exceeded maximum size(max 150 characters). Line No " & iErrorLine & "." : lblError.Text = "Customer Name exceeded maximum size(max 150 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblPartner.Text.Trim = "" Or lblPartner.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Partner Name. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Partner Name. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                If lblPartner.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "Partner Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "Partner Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblOrganizationType.Text.Trim = "" Or lblOrganizationType.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Organization Type. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Organization Type. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                iOrganizationTypeId = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblOrganizationType.Text.Trim), "ORG")
                If iOrganizationTypeId = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Organization Type, Please add it in General Masters. Line No " & iErrorLine & "." : lblError.Text = "Invalid Organization Type, Please add it in General Masters. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblFinancialYear.Text.Trim = "" Or lblFinancialYear.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Financial Year. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Financial Year. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                iFinancialYearId = objclsExcelUpload.GetYearID(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblFinancialYear.Text.Trim))
                If iFinancialYearId = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Financial Year. Line No " & iErrorLine & "." : lblError.Text = "Invalid Financial Year. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblEmployee.Text.Trim = "" Or lblEmployee.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Employee Name. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Employee Name. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                If lblEmployee.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblTask.Text.Trim = "" Or lblTask.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Audit Task/Assignment. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Audit Task/Assignment. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                iTaskID = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblTask.Text.Trim), "AT")
                If iTaskID = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Audit Task/Assignment, Please add it in General Masters. Line No " & iErrorLine & "." : lblError.Text = "Invalid Audit Task/Assignment, Please add it in General Masters. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblTask.Text.Trim.Length > 2000 Then
                    lblExcelValidationMsg.Text = "Task Name exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "." : lblError.Text = "Task Name exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblSubTask.Text.Trim = "" Or lblSubTask.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Sub Task. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Sub Task. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                iSubTaskId = objclsExcelUpload.CheckAndGetSubTaskIdByTask(sSession.AccessCode, sSession.AccessCodeID, iTaskID, objclsGRACeGeneral.SafeSQL(lblSubTask.Text.Trim))
                If iSubTaskId = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Sub Task, Please add it in Assignment Master. Line No " & iErrorLine & "." : lblError.Text = "Invalid Sub Task, Please add it in Assignment Master. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblSubTask.Text.Trim.Length > 5000 Then
                    lblExcelValidationMsg.Text = "Sub Task Name exceeded maximum size(max 5000 characters). Line No " & iErrorLine & "." : lblError.Text = "Sub Task Name exceeded maximum size(max 5000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblStartDate.Text.Trim = "" Or lblStartDate.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Start Date. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Start Date. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                Try
                    DateTime.ParseExact(lblStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblExcelValidationMsg.Text = "Enter valid Start Date. Line No '" & iErrorLine & "'." : lblError.Text = "Enter valid Start Date. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End Try
                If lblCompletedDate.Text.Trim = "" Or lblCompletedDate.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Completed Date. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Completed Date. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                Try
                    DateTime.ParseExact(lblCompletedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblExcelValidationMsg.Text = "Enter valid Completed Date. Line No '" & iErrorLine & "'." : lblError.Text = "Enter valid Completed Date. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End Try
                If lblWorkStatus.Text.Trim = "" Or lblWorkStatus.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Work Status. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Work Status. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                iWorkStatusId = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblWorkStatus.Text.Trim), "WS")
                If iWorkStatusId = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Work Status, Please add it in General Masters. Line No " & iErrorLine & "." : lblError.Text = "Invalid Work Status, Please add it in General Masters. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'If lblComments.Text.Trim = "" Or lblComments.Text.Trim = "&nbsp;" Then
                '    lblExcelValidationMsg.Text = "Enter Comments. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Comments. Line No '" & iErrorLine & "'."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Try
                'End If
                If lblComments.Text.Trim.Length > 2000 Then
                    lblExcelValidationMsg.Text = "Comments Name exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "." : lblError.Text = "Comments Name exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                'If lblBillingStatus.Text.Trim = "" Or lblBillingStatus.Text.Trim = "&nbsp;" Then
                '    lblExcelValidationMsg.Text = "Enter Billing Status. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Billing Status. Line No '" & iErrorLine & "'."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Try
                'End If
            Next

            For i = 0 To dgGeneral.Items.Count - 1
                lblCustomerName.Text = dgGeneral.Items(i).Cells(1).Text
                lblPartner.Text = dgGeneral.Items(i).Cells(2).Text
                lblOrganizationType.Text = dgGeneral.Items(i).Cells(3).Text
                lblFinancialYear.Text = dgGeneral.Items(i).Cells(4).Text
                lblEmployee.Text = dgGeneral.Items(i).Cells(5).Text
                lblTask.Text = dgGeneral.Items(i).Cells(6).Text
                lblSubTask.Text = dgGeneral.Items(i).Cells(7).Text
                lblStartDate.Text = dgGeneral.Items(i).Cells(8).Text
                lblCompletedDate.Text = dgGeneral.Items(i).Cells(9).Text
                lblWorkStatus.Text = dgGeneral.Items(i).Cells(10).Text
                lblComments.Text = dgGeneral.Items(i).Cells(11).Text
                lblBillingStatus.Text = dgGeneral.Items(i).Cells(12).Text

                iOrganizationTypeId = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblOrganizationType.Text.Trim), "ORG")
                iFinancialYearId = objclsExcelUpload.GetYearID(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblFinancialYear.Text.Trim))
                iWorkStatusId = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblWorkStatus.Text.Trim), "WS")
                iCustomerId = objclsExcelUpload.CheckAndGetCustIdByCustName(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblCustomerName.Text.Trim))
                If iCustomerId = 0 Then
                    objstrCust.CUST_ID = 0
                    objstrCust.CUST_NAME = objclsGRACeGeneral.SafeSQL(lblCustomerName.Text.Trim)
                    objstrCust.CUST_CODE = objCust.GetLatestCustomerCode(sSession.AccessCode, sSession.AccessCodeID)
                    objstrCust.CUST_WEBSITE = ""
                    objstrCust.CUST_EMAIL = ""
                    objstrCust.CUST_GROUPNAME = ""
                    objstrCust.CUST_GROUPINDIVIDUAL = 0
                    objstrCust.CUST_ORGTYPEID = iOrganizationTypeId
                    objstrCust.CUST_INDTYPEID = 0
                    objstrCust.CUST_MGMTTYPEID = 0
                    objstrCust.CUST_CommitmentDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objstrCust.CUSt_BranchId = ""
                    objstrCust.CUST_COMM_ADDRESS = ""
                    objstrCust.CUST_COMM_CITY = ""
                    objstrCust.CUST_COMM_PIN = ""
                    objstrCust.CUST_COMM_STATE = ""
                    objstrCust.CUST_COMM_COUNTRY = ""
                    objstrCust.CUST_COMM_FAX = ""
                    objstrCust.CUST_COMM_TEL = ""
                    objstrCust.CUST_COMM_Email = ""
                    objstrCust.CUST_ADDRESS = ""
                    objstrCust.CUST_CITY = ""
                    objstrCust.CUST_PIN = ""
                    objstrCust.CUST_STATE = ""
                    objstrCust.CUST_COUNTRY = ""
                    objstrCust.CUST_FAX = ""
                    objstrCust.CUST_TELPHONE = ""
                    objstrCust.CUST_ConEmailID = ""
                    objstrCust.CUST_LOCATIONID = ""
                    objstrCust.CUST_TASKS = ""
                    objstrCust.CUST_ORGID = 0
                    objstrCust.CUST_DELFLG = "W"
                    objstrCust.CUST_CRBY = sSession.UserID
                    objstrCust.CUST_UpdatedBy = sSession.UserID
                    objstrCust.CUST_BOARDOFDIRECTORS = ""
                    objstrCust.CUST_STATUS = "W"
                    objstrCust.CUST_IPAddress = sSession.IPAddress
                    objstrCust.CUST_CompID = sSession.AccessCodeID
                    Dim ArrCust() As String
                    ArrCust = objCust.SaveCustomerMaster(sSession.AccessCode, objstrCust)
                    iCustomerId = ArrCust(1)
                    objclsCustDashbord.ApproveCustomerDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCustomerId, "W", sSession.IPAddress)
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "Uploaded", iCustomerId, objclsGRACeGeneral.SafeSQL(lblCustomerName.Text.Trim), 0, "", sSession.IPAddress)
                End If

                iPartnerID = objclsEmployeeMaster.CheckAndGetUserIdByUserName(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(Regex.Replace(lblPartner.Text.Trim, "\s", "")))
                If iPartnerID = 0 Then
                    objclsEmployeeMaster.iUserID = 0
                    objclsEmployeeMaster.sUsrStatus = "C"
                    objclsEmployeeMaster.iUsrOrgID = objclsExcelUpload.CheckBranch(sSession.AccessCode, sSession.AccessCodeID, "India", "South", "Karnataka", "Bangalore")
                    objclsEmployeeMaster.iUsrNode = 4
                    objclsEmployeeMaster.sUsrCode = "EMP" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Sad_UserDetails", "Usr_ID", "Usr_CompId")
                    objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(lblPartner.Text.Trim)
                    objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(Regex.Replace(lblPartner.Text.Trim, "\s", ""))
                    objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword("a")
                    objclsEmployeeMaster.sUsrEmail = ""
                    objclsEmployeeMaster.sUsrDutyStatus = "W"
                    objclsEmployeeMaster.sUsrType = "U"
                    objclsEmployeeMaster.sUsrPhoneNo = ""
                    objclsEmployeeMaster.sUsrMobileNo = ""
                    objclsEmployeeMaster.sUsrOfficePhone = ""
                    objclsEmployeeMaster.sUsrOffPhExtn = ""
                    objclsEmployeeMaster.iUsrDesignation = 0
                    objclsEmployeeMaster.iUsrCompanyID = 0
                    objclsEmployeeMaster.iUsrRole = 0
                    objclsEmployeeMaster.iUsrLevelGrp = 0
                    objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = 0
                    objclsEmployeeMaster.sUsrFlag = "W"
                    objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
                    objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
                    objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
                    objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
                    objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
                    objclsEmployeeMaster.iUsrBCMmodule = 0
                    objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
                    objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
                    objclsEmployeeMaster.iUsrBCMRole = 0
                    objclsEmployeeMaster.iUsrMasterModule = 0
                    objclsEmployeeMaster.iUsrMasterRole = 0
                    objclsEmployeeMaster.iUsrPartner = 1
                    Dim ArrPartner() As String
                    ArrPartner = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
                    iPartnerID = ArrPartner(1)
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ArrPartner(1), sSession.IPAddress, "Created")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Uploaded", sSession.YearID, sSession.YearName, iPartnerID, objclsGRACeGeneral.SafeSQL(lblPartner.Text.Trim), sSession.IPAddress)
                End If

                iEmployeeId = objclsEmployeeMaster.CheckAndGetUserIdByUserName(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(Regex.Replace(lblEmployee.Text.Trim, "\s", "")))
                If iEmployeeId = 0 Then
                    objclsEmployeeMaster.iUserID = 0
                    objclsEmployeeMaster.sUsrStatus = "C"
                    objclsEmployeeMaster.iUsrOrgID = objclsExcelUpload.CheckBranch(sSession.AccessCode, sSession.AccessCodeID, "India", "South", "Karnataka", "Bangalore")
                    objclsEmployeeMaster.iUsrNode = 4
                    objclsEmployeeMaster.sUsrCode = "EMP" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Sad_UserDetails", "Usr_ID", "Usr_CompId")
                    objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(lblEmployee.Text.Trim)
                    objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(Regex.Replace(lblEmployee.Text.Trim, "\s", ""))
                    objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword("a")
                    objclsEmployeeMaster.sUsrEmail = ""
                    objclsEmployeeMaster.sUsrDutyStatus = "W"
                    objclsEmployeeMaster.sUsrType = "U"
                    objclsEmployeeMaster.sUsrPhoneNo = ""
                    objclsEmployeeMaster.sUsrMobileNo = ""
                    objclsEmployeeMaster.sUsrOfficePhone = ""
                    objclsEmployeeMaster.sUsrOffPhExtn = ""
                    objclsEmployeeMaster.iUsrDesignation = 0
                    objclsEmployeeMaster.iUsrCompanyID = 0
                    objclsEmployeeMaster.iUsrRole = 0
                    objclsEmployeeMaster.iUsrLevelGrp = 0
                    objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = 0
                    objclsEmployeeMaster.sUsrFlag = "W"
                    objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
                    objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
                    objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
                    objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
                    objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
                    objclsEmployeeMaster.iUsrBCMmodule = 0
                    objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
                    objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
                    objclsEmployeeMaster.iUsrBCMRole = 0
                    objclsEmployeeMaster.iUsrMasterModule = 0
                    objclsEmployeeMaster.iUsrMasterRole = 0
                    objclsEmployeeMaster.iUsrPartner = 0
                    Dim ArrEmp() As String
                    ArrEmp = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)
                    iEmployeeId = ArrEmp(1)
                    objclsEmployeeMaster.EmployeeApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iEmployeeId, sSession.IPAddress, "Created")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Uploaded", sSession.YearID, sSession.YearName, iEmployeeId, objclsGRACeGeneral.SafeSQL(lblEmployee.Text.Trim), sSession.IPAddress)
                End If

                iTaskID = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblTask.Text.Trim), "AT")
                If iTaskID = 0 Then
                    objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "Content_Management_Master", "cmm_ID", "Cmm_CompID")
                    objclsAdminMaster.sCode = "AT_" & objclsAdminMaster.iID
                    objclsAdminMaster.sCategory = "AT"
                    objclsAdminMaster.iRiskCategory = 0
                    objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(lblTask.Text.Trim)
                    objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(lblTask.Text.Trim)
                    objclsAdminMaster.iKeyComponent = 0
                    objclsAdminMaster.sModule = "A"
                    objclsAdminMaster.sDelflag = "W"
                    objclsAdminMaster.sStatus = "C"
                    objclsAdminMaster.iCrBy = sSession.UserID
                    objclsAdminMaster.iUpdatedBy = sSession.UserID
                    objclsAdminMaster.sIpAddress = sSession.IPAddress
                    objclsAdminMaster.iCompId = sSession.AccessCodeID
                    Dim ArrTask() As String
                    ArrTask = objclsAdminMaster.SaveMasterDetails(sSession.AccessCode, objclsAdminMaster)
                    iTaskID = ArrTask(1)
                    objclsAdminMaster.UpdateGeneralMasterStatus(sSession.AccessCode, sSession.AccessCodeID, iTaskID, "Content_Management_Master", sSession.UserID, sSession.IPAddress, "W", "OTHERS")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "General Master", "Uploaded", sSession.YearID, sSession.YearName, iTaskID, objclsGRACeGeneral.SafeSQL(lblTask.Text.Trim), sSession.IPAddress)
                End If
                Dim iIsComplianceAsgID = objclsExcelUpload.GetTaskIsCompliance(sSession.AccessCode, sSession.AccessCodeID, iTaskID)

                iSubTaskId = objclsExcelUpload.CheckAndGetSubTaskIdByTask(sSession.AccessCode, sSession.AccessCodeID, iTaskID, objclsGRACeGeneral.SafeSQL(lblSubTask.Text.Trim))
                If iSubTaskId = 0 Then
                    objclsAdminMaster.iID = objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditAssignmentSubTask_Master", "AM_ID", "AM_CompId")
                    objclsAdminMaster.sCode = "AAST_" & objclsAdminMaster.iID
                    objclsAdminMaster.iID = 0
                    objclsAdminMaster.sDesc = objclsGRACeGeneral.SafeSQL(lblSubTask.Text.Trim)
                    objclsAdminMaster.iAuditAssignment = iTaskID
                    objclsAdminMaster.sRemarks = objclsGRACeGeneral.SafeSQL(lblSubTask.Text.Trim)
                    'If lblBillingStatus.Text.Trim.ToUpper = "NON BILLABLE" Then
                    '    objclsAdminMaster.iBillingType = 1
                    'Else
                    objclsAdminMaster.iBillingType = 0
                    'End If
                    objclsAdminMaster.sDelflag = "W"
                    objclsAdminMaster.sStatus = "C"
                    objclsAdminMaster.iCrBy = sSession.UserID
                    objclsAdminMaster.iUpdatedBy = sSession.UserID
                    objclsAdminMaster.sIpAddress = sSession.IPAddress
                    objclsAdminMaster.iCompId = sSession.AccessCodeID
                    Dim ArrSubTask() As String
                    ArrSubTask = objclsAdminMaster.SaveAssignmentSubTaskMasterDetails(sSession.AccessCode, objclsAdminMaster)
                    iSubTaskId = ArrSubTask(1)
                    objclsAdminMaster.UpdateAuditAssignmentSTStatus(sSession.AccessCode, sSession.AccessCodeID, iSubTaskId, sSession.UserID, sSession.IPAddress, "W")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Assignment Master", "Uploaded", iTaskID, objclsGRACeGeneral.SafeSQL(lblTask.Text.Trim), iSubTaskId, objclsGRACeGeneral.SafeSQL(lblSubTask.Text.Trim), sSession.IPAddress)
                End If

                Dim dStartDate As DateTime = Date.ParseExact(lblStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Dim dCompletedDate As DateTime = Date.ParseExact(lblCompletedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                Dim objAAS As New strAuditAssignment_Schedule
                Dim objAAST As New strAuditAssignment_SubTask
                Dim objclsAuditAssignment As New clsAuditAssignment
                Dim objAAEST As New strAuditAssignment_EmpSubTask

                objAAS.iAAS_ID = 0
                objAAS.iAAS_CustID = iCustomerId
                objAAS.sAAS_AssignmentNo = ""
                objAAS.iAAS_PartnerID = iPartnerID
                objAAS.iAAS_YearID = iFinancialYearId
                objAAS.iAAS_MonthID = dStartDate.Month
                objAAS.iAAS_TaskID = iTaskID
                objAAS.iAAS_Status = 0
                objAAS.iAAS_AdvancePartialBilling = 0
                If objclsGRACeGeneral.SafeSQL(lblWorkStatus.Text.Trim.ToUpper()) = "COMPLETED" Then
                    objAAS.iAAS_BillingType = 1
                Else
                    objAAS.iAAS_BillingType = 0
                End If
                objAAS.sAAS_AssessmentYearID = ""
                objAAS.iAAS_AttachID = 0
                objAAS.iAAS_CrBy = sSession.UserID
                objAAS.iAAS_UpdatedBy = sSession.UserID
                objAAS.sAAS_IPAddress = sSession.IPAddress
                objAAS.iAAS_CompID = sSession.AccessCodeID
                objAAS.iAAS_IsComplianceAsg = iIsComplianceAsgID
                Dim ArrAAS() As String
                ArrAAS = objclsAuditAssignment.SaveScheduleAssignmentsDetails(sSession.AccessCode, objAAS, lblFinancialYear.Text.Trim)

                objAAST.iAAST_AAS_ID = ArrAAS(1)
                objAAST.iAAST_SubTaskID = iSubTaskId
                objAAST.iAAST_EmployeeID = iEmployeeId
                objAAST.sAAST_AssistedByEmployeesID = ","
                If lblComments.Text.Trim = "" Or lblComments.Text.Trim = "&nbsp;" Then
                    objAAST.sAAST_Desc = ""
                Else
                    objAAST.sAAST_Desc = objclsGRACeGeneral.SafeSQL(lblComments.Text.Trim)
                End If
                objAAST.iAAST_FrequencyID = 2
                objAAST.iAAST_YearOrMonthID = dStartDate.Month
                objAAST.dAAST_DueDate = dStartDate
                objAAST.dAAST_ExpectedCompletionDate = dCompletedDate
                objAAST.iAAST_WorkStatusID = iWorkStatusId
                objAAST.iAAST_CrBy = sSession.UserID
                objAAST.sAAST_IPAddress = sSession.IPAddress
                objAAST.iAAST_CompID = sSession.AccessCodeID
                Dim ArrAAST() As String
                ArrAAST = objclsAuditAssignment.SaveAuditAssignmentEmpSubTask(sSession.AccessCode, objAAST)

                objAAEST.iAAEST_ID = 0
                objAAEST.iAAEST_AAS_ID = ArrAAS(1)
                objAAEST.iAAEST_AAST_ID = ArrAAST(1)
                objAAEST.iAAEST_WorkStatusID = iWorkStatusId
                If objclsGRACeGeneral.SafeSQL(lblWorkStatus.Text.Trim.ToUpper()) = "COMPLETED" Then
                    objAAEST.iAAST_Closed = 1
                    objAAEST.dAAEST_CrOn = dCompletedDate
                Else
                    objAAEST.iAAST_Closed = 0
                    objAAEST.dAAEST_CrOn = dStartDate
                End If
                If lblComments.Text.Trim = "" Or lblComments.Text.Trim = "&nbsp;" Then
                    If objclsGRACeGeneral.SafeSQL(lblWorkStatus.Text.Trim.ToUpper()) = "COMPLETED" Then
                        objAAEST.sAAEST_Comments = "Auto Completed(Excel)"

                        objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, ArrAAS(1), 1)
                        objclsAuditAssignment.UpdateScheduledStatusAndFolderPath(sSession.AccessCode, sSession.AccessCodeID, "", ArrAAS(1), 0)
                        objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, ArrAAS(1))
                    Else
                        objAAEST.sAAEST_Comments = ""
                    End If
                Else
                    objAAEST.sAAEST_Comments = objclsGRACeGeneral.SafeSQL(lblComments.Text.Trim)
                End If

                objAAEST.iAAEST_AttachID = 0
                objAAEST.iAAEST_CrBy = sSession.UserID
                objAAEST.sAAEST_IPAddress = sSession.IPAddress
                objAAEST.iAAEST_CompID = sSession.AccessCodeID
                objclsAuditAssignment.SaveEmployeeSubTaskDetails(sSession.AccessCode, objAAEST)
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveComplianceTask" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function LoadAuditChecklistMaster(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dt As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("Audit Type")
            dtTable.Columns.Add("Heading")
            dtTable.Columns.Add("Checkpoint")
            dt = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dt) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dt
            End If
            For i = 0 To dt.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1
                If IsDBNull(dt.Rows(i).Item(0)) = False Then
                    If dt.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Audit Type") = objclsGRACeGeneral.SafeSQL(dt.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dt.Rows(i).Item(1)) = False Then
                    If dt.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Heading") = objclsGRACeGeneral.SafeSQL(dt.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dt.Rows(i).Item(2)) = False Then
                    If dt.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Checkpoint") = objclsGRACeGeneral.SafeSQL(dt.Rows(i).Item(2))
                    End If
                End If
                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAuditChecklistMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub SaveAuditChecklistMaster()
        Dim lblAuditType As New Label, lblHeading As New Label, lblCheckpoint As New Label
        Dim objclsAuditChecklist As New clsAuditChecklist
        Dim iAuditTypeID As Integer, iErrorLine As Integer = 0
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblAuditType.Text = "" : lblHeading.Text = "" : lblCheckpoint.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblAuditType.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblHeading.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblCheckpoint.Text = dgGeneral.Items(i).Cells(3).Text
                End If

                If lblAuditType.Text.Trim = "" Or lblAuditType.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Audit Type. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Audit Type. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                iAuditTypeID = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblAuditType.Text.Trim), "AT")

                If iAuditTypeID = 0 Then
                    lblExcelValidationMsg.Text = "Invalid Audit Type, Please add it in Masters -> Super Master -> General Master. Line No " & iErrorLine & "." : lblError.Text = "Invalid Audit Type, Please add it in Masters -> Super Master -> General Master. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblHeading.Text.Trim.Length > 2000 Then
                    lblExcelValidationMsg.Text = "Heading exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "." : lblError.Text = "Heading exceeded maximum size(max 2000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblCheckpoint.Text.Trim = "" Or lblCheckpoint.Text.Trim = "&nbsp;" Then
                    lblExcelValidationMsg.Text = "Enter Checkpoint. Line No '" & iErrorLine & "'." : lblError.Text = "Enter Checkpoint. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Try
                End If
                If lblCheckpoint.Text.Trim.Length > 8000 Then
                    lblExcelValidationMsg.Text = "Checkpoint exceeded maximum size(max 8000 characters). Line No " & iErrorLine & "." : lblError.Text = "Checkpoint exceeded maximum size(max 8000 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                Dim bCheck As Boolean = objclsAuditChecklist.CheckAuditTypeChecklistExistingDetails(sSession.AccessCode, sSession.AccessCodeID, iAuditTypeID, objclsGRACeGeneral.SafeSQL(lblCheckpoint.Text.Trim), "ACM_Checkpoint", iAuditTypeID)
                If bCheck = True Then
                    lblExcelValidationMsg.Text = "" & lblCheckpoint.Text.Trim & " Checkpoint Name already exist. Line No '" & iErrorLine & "'." : lblError.Text = "" & lblCheckpoint.Text.Trim & " Checkpoint Name already exist. Line No '" & iErrorLine & "'."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Next

            For i = 0 To dgGeneral.Items.Count - 1
                lblAuditType.Text = dgGeneral.Items(i).Cells(1).Text
                lblHeading.Text = dgGeneral.Items(i).Cells(2).Text
                lblCheckpoint.Text = dgGeneral.Items(i).Cells(3).Text

                iAuditTypeID = objclsExcelUpload.CheckMasters(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblAuditType.Text.Trim), "AT")
                If iAuditTypeID > 0 Then
                    objclsAuditChecklist.iID = 0
                    objclsAuditChecklist.sCode = "ACP_" & objclsGeneralFunctions.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "AuditType_Checklist_Master", "ACM_ID", "ACM_CompId")
                    objclsAuditChecklist.iAuditTypeID = iAuditTypeID
                    objclsAuditChecklist.sHeading = objclsGRACeGeneral.SafeSQL(lblHeading.Text.Trim)
                    objclsAuditChecklist.sCheckpoint = objclsGRACeGeneral.SafeSQL(lblCheckpoint.Text.Trim)
                    objclsAuditChecklist.sDelflag = "W"
                    objclsAuditChecklist.sStatus = "C"
                    objclsAuditChecklist.iCrBy = sSession.UserID
                    objclsAuditChecklist.iUpdatedBy = sSession.UserID
                    objclsAuditChecklist.sIpAddress = sSession.IPAddress
                    objclsAuditChecklist.iCompId = sSession.AccessCodeID
                    Dim Arr() As String = objclsAuditChecklist.SaveAuditTypeChecklistMasterDetails(sSession.AccessCode, objclsAuditChecklist)
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Audit Checklis Master", "Uploaded", iAuditTypeID, objclsGRACeGeneral.SafeSQL(lblAuditType.Text), Arr(1), objclsGRACeGeneral.SafeSQL(lblCheckpoint.Text.Trim), sSession.IPAddress)
                    objclsAuditChecklist.UpdateAuditTypeCheckpointStatus(sSession.AccessCode, sSession.AccessCodeID, Arr(1), sSession.UserID, sSession.IPAddress, "W")
                End If
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveAuditChecklistMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Dim sPath As String = ""
        'Dim objEApp As Excel.Application
        Dim dt As New DataTable
        Dim iAssignmentID As Integer = 0, iCustID As Integer = 0, iYearID As Integer = 0, iFunctionID As Integer = 0
        Try
            lblError.Text = ""
            'objEApp = DirectCast(CreateObject("Excel.Application"), Excel.Application)
            'If ddlMasterName.SelectedValue = 14 Then

            'ElseIf ddlMasterName.SelectedValue = 22 Then

            'ElseIf ddlMasterName.SelectedValue = 23 Then

            'ElseIf ddlMasterName.SelectedValue = 24 Then

            'ElseIf ddlMasterName.SelectedValue = 25 Then

            'ElseIf ddlMasterName.SelectedValue = 26 Then

            'End If
            'If (String.Compare(objEApp.Version, "12.0") >= 0) Then
            'sPath = Server.MapPath("../") & "ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "") & ".xlsx"
            'Else
            '    sPath = Server.MapPath("../") & "Reports\ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "") & ".xls"
            'End If
            'sPath = Server.MapPath("../") & "Reports\ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "")

            If (ddlMasterName.SelectedItem.Text = "Trial Balance/Opening Balance") Then
                sPath = Server.MapPath("../") & "ExcelUploads\" & Regex.Replace("TrialBalance", "\s", "") & ".xlsx"
            Else
                sPath = Server.MapPath("../") & "ExcelUploads\" & Regex.Replace(ddlMasterName.SelectedItem.Text, "\s", "") & ".xlsx"
            End If

            DownloadFile(sPath)
        Catch ex As Exception
            lblError.Text = ex.Message 'objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkDownload_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub


    Private Function LoadCustomerMasters(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtStock As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim i As Integer
        Dim sCommodity As String = ""
        Try
            dtTable.Columns.Add("Sr.No")
            dtTable.Columns.Add("* Customer Name")
            dtTable.Columns.Add("* Organisation Type")
            dtTable.Columns.Add("* Address")
            dtTable.Columns.Add("* City")
            dtTable.Columns.Add("* E-Mail")
            dtTable.Columns.Add("* Mobile No")
            dtTable.Columns.Add("* Business Reltn. Start Date")
            dtTable.Columns.Add("* Industry Type")
            dtTable.Columns.Add("Registration No")
            dtTable.Columns.Add("* Professional Services Offered 1")
            dtTable.Columns.Add("Professional Services Offered 2")
            dtTable.Columns.Add("* Location Name 1")
            dtTable.Columns.Add("* Contact Person 1")
            dtTable.Columns.Add("* Address 1")
            dtTable.Columns.Add("CIN 1")
            dtTable.Columns.Add("TAN 1")
            dtTable.Columns.Add("GST 1")
            dtTable.Columns.Add("Location Name 2")
            dtTable.Columns.Add("Contact Person 2")
            dtTable.Columns.Add("Address 2")
            dtTable.Columns.Add("CIN 2")
            dtTable.Columns.Add("TAN 2")
            dtTable.Columns.Add("GST 2")
            dtTable.Columns.Add("Location Name 3")
            dtTable.Columns.Add("Contact Person 3")
            dtTable.Columns.Add("Address 3")
            dtTable.Columns.Add("CIN 3")
            dtTable.Columns.Add("TAN 3")
            dtTable.Columns.Add("GST 3")
            dtTable.Columns.Add("Location Name 4")
            dtTable.Columns.Add("Contact Person 4")
            dtTable.Columns.Add("Address 4")
            dtTable.Columns.Add("CIN 4")
            dtTable.Columns.Add("TAN 4")
            dtTable.Columns.Add("GST 4")
            dtTable.Columns.Add("Location Name 5")
            dtTable.Columns.Add("Contact Person  5")
            dtTable.Columns.Add("Address 5")
            dtTable.Columns.Add("CIN 5")
            dtTable.Columns.Add("TAN 5")
            dtTable.Columns.Add("GST 5")
            dtTable.Columns.Add("Director Name1")
            dtTable.Columns.Add("DIN 1")
            dtTable.Columns.Add("Director Name 2")
            dtTable.Columns.Add("DIN 2")

            dtStock = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtStock) = True Then
                lblError.Text = "Invalid Excel format in selected sheet." : lblExcelValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetName.Items.Clear()
                Return dtStock
            End If
            For i = 0 To dtStock.Rows.Count - 1
                dRow = dtTable.NewRow
                dRow("Sr.No") = i + 1

                dRow("* Customer Name") = "" : dRow("* Organisation Type") = "" : dRow("* Address") = ""
                dRow("* City") = "" : dRow("* E-Mail") = ""
                dRow("* Mobile No") = ""
                If IsDBNull(dtStock.Rows(i).Item(0)) = False Then
                    If dtStock.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("* Customer Name") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(1)) = False Then
                    If dtStock.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("* Organisation Type") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(2)) = False Then
                    If dtStock.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("* Address") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(3)) = False Then
                    If dtStock.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("* City") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(4)) = False Then
                    If dtStock.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("* E-Mail") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(4))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(5)) = False Then
                    If dtStock.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("* Mobile No") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(5))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(6)) = False Then
                    If dtStock.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("* Business Reltn. Start Date") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(6))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(7)) = False Then
                    If dtStock.Rows(i).Item(7).ToString <> "&nbsp;" Then
                        dRow("* Industry Type") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(7))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(8)) = False Then
                    If dtStock.Rows(i).Item(8).ToString <> "&nbsp;" Then
                        dRow("Registration No") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(8))
                    Else
                        dRow("Registration No") = ""
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(9)) = False Then
                    If dtStock.Rows(i).Item(9).ToString <> "&nbsp;" Then
                        dRow("* Professional Services Offered 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(9))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(10)) = False Then
                    If dtStock.Rows(i).Item(10).ToString <> "&nbsp;" Then
                        dRow("Professional Services Offered 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(10))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(11)) = False Then
                    If dtStock.Rows(i).Item(11).ToString <> "&nbsp;" Then
                        dRow("* Location Name 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(11))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(12)) = False Then
                    If dtStock.Rows(i).Item(12).ToString <> "&nbsp;" Then
                        dRow("* Contact Person 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(12))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(13)) = False Then
                    If dtStock.Rows(i).Item(13).ToString <> "&nbsp;" Then
                        dRow("* Address 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(13))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(14)) = False Then
                    If dtStock.Rows(i).Item(14).ToString <> "&nbsp;" Then
                        dRow("CIN 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(14))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(15)) = False Then
                    If dtStock.Rows(i).Item(15).ToString <> "&nbsp;" Then
                        dRow("TAN 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(15))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(16)) = False Then
                    If dtStock.Rows(i).Item(16).ToString <> "&nbsp;" Then
                        dRow("GST 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(16))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(17)) = False Then
                    If dtStock.Rows(i).Item(17).ToString <> "&nbsp;" Then
                        dRow("Location Name 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(17))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(18)) = False Then
                    If dtStock.Rows(i).Item(18).ToString <> "&nbsp;" Then
                        dRow("Contact Person 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(18))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(19)) = False Then
                    If dtStock.Rows(i).Item(19).ToString <> "&nbsp;" Then
                        dRow("Address 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(19))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(20)) = False Then
                    If dtStock.Rows(i).Item(20).ToString <> "&nbsp;" Then
                        dRow("CIN 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(20))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(21)) = False Then
                    If dtStock.Rows(i).Item(21).ToString <> "&nbsp;" Then
                        dRow("TAN 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(21))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(22)) = False Then
                    If dtStock.Rows(i).Item(22).ToString <> "&nbsp;" Then
                        dRow("GST 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(22))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(23)) = False Then
                    If dtStock.Rows(i).Item(23).ToString <> "&nbsp;" Then
                        dRow("Location Name 3") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(23))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(24)) = False Then
                    If dtStock.Rows(i).Item(24).ToString <> "&nbsp;" Then
                        dRow("Contact Person 3") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(24))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(25)) = False Then
                    If dtStock.Rows(i).Item(25).ToString <> "&nbsp;" Then
                        dRow("Address 3") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(25))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(26)) = False Then
                    If dtStock.Rows(i).Item(26).ToString <> "&nbsp;" Then
                        dRow("CIN 3") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(26))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(27)) = False Then
                    If dtStock.Rows(i).Item(27).ToString <> "&nbsp;" Then
                        dRow("TAN 3") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(27))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(28)) = False Then
                    If dtStock.Rows(i).Item(28).ToString <> "&nbsp;" Then
                        dRow("GST 3") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(28))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(29)) = False Then
                    If dtStock.Rows(i).Item(29).ToString <> "&nbsp;" Then
                        dRow("Location Name 4") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(29))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(30)) = False Then
                    If dtStock.Rows(i).Item(30).ToString <> "&nbsp;" Then
                        dRow("Contact Person 4") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(30))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(31)) = False Then
                    If dtStock.Rows(i).Item(31).ToString <> "&nbsp;" Then
                        dRow("Address 4") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(31))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(32)) = False Then
                    If dtStock.Rows(i).Item(32).ToString <> "&nbsp;" Then
                        dRow("CIN 4") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(32))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(33)) = False Then
                    If dtStock.Rows(i).Item(33).ToString <> "&nbsp;" Then
                        dRow("TAN 4") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(33))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(34)) = False Then
                    If dtStock.Rows(i).Item(34).ToString <> "&nbsp;" Then
                        dRow("GST 4") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(34))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(35)) = False Then
                    If dtStock.Rows(i).Item(35).ToString <> "&nbsp;" Then
                        dRow("Location Name 5") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(35))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(36)) = False Then
                    If dtStock.Rows(i).Item(36).ToString <> "&nbsp;" Then
                        dRow("Contact Person  5") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(36))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(37)) = False Then
                    If dtStock.Rows(i).Item(37).ToString <> "&nbsp;" Then
                        dRow("Address 5") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(37))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(38)) = False Then
                    If dtStock.Rows(i).Item(38).ToString <> "&nbsp;" Then
                        dRow("CIN 5") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(38))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(39)) = False Then
                    If dtStock.Rows(i).Item(39).ToString <> "&nbsp;" Then
                        dRow("TAN 5") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(39))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(40)) = False Then
                    If dtStock.Rows(i).Item(40).ToString <> "&nbsp;" Then
                        dRow("GST 5") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(40))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(41)) = False Then
                    If dtStock.Rows(i).Item(41).ToString <> "&nbsp;" Then
                        dRow("Director Name1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(41))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(42)) = False Then
                    If dtStock.Rows(i).Item(42).ToString <> "&nbsp;" Then
                        dRow("DIN 1") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(42))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(43)) = False Then
                    If dtStock.Rows(i).Item(43).ToString <> "&nbsp;" Then
                        dRow("Director Name 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(43))
                    End If
                End If
                If IsDBNull(dtStock.Rows(i).Item(44)) = False Then
                    If dtStock.Rows(i).Item(44).ToString <> "&nbsp;" Then
                        dRow("DIN 2") = objclsGRACeGeneral.SafeSQL(dtStock.Rows(i).Item(44))
                    End If
                End If

                dtTable.Rows.Add(dRow)
            Next
            Return dtTable
        Catch ex As Exception
            Throw
        End Try
    End Function

    Private Sub SaveCustomerMasterDetails()
        Dim objstrCust As New strCustMaster
        Dim lblCustomerName As New Label : Dim lblOrgType As New Label : Dim lblAddress As New Label : Dim lblEmail As New Label
        Dim lblCity As New Label : Dim lblContactPerson As New Label : Dim lblMobileNumber As New Label : Dim Arr() As String
        Dim iErrorLine As Integer = 0
        Dim lblbusiness As New Label
        Dim lblCustomer As New Label
        Dim lblIndType As New Label
        Dim lblRegNo As New Label
        Dim lblProffServiceOff1 As New Label
        Dim lblProffServiceOff2 As New Label
        Dim lbllocation1 As New Label
        Dim lblContactPers1 As New Label
        Dim lblAddress1 As New Label
        Dim lblCIN1 As New Label
        Dim lblTAN1 As New Label
        Dim lblGST1 As New Label
        Dim lbllocation2 As New Label
        Dim lblContactPers2 As New Label
        Dim lblAddress2 As New Label
        Dim lblCIN2 As New Label
        Dim lblTAN2 As New Label
        Dim lblGST2 As New Label
        Dim lbllocation3 As New Label
        Dim lblContactPers3 As New Label
        Dim lblAddress3 As New Label
        Dim lblCIN3 As New Label
        Dim lblTAN3 As New Label
        Dim lblGST3 As New Label
        Dim lbllocation4 As New Label
        Dim lblContactPers4 As New Label
        Dim lblAddress4 As New Label
        Dim lblCIN4 As New Label
        Dim lblTAN4 As New Label
        Dim lblGST4 As New Label
        Dim lbllocation5 As New Label
        Dim lblContactPers5 As New Label
        Dim lblAddress5 As New Label
        Dim lblCIN5 As New Label
        Dim lblTAN5 As New Label
        Dim lblGST5 As New Label
        Dim lbldirectName1 As New Label
        Dim Din1 As New Label
        Dim lbldirectName2 As New Label
        Dim Din2 As New Label
        Dim objstrCUSTAccountingTemplate As New strCUSTAccountingTemplate
        Dim objsCustLocation As New strCustLocation
        Dim LocArr() As String
        Dim DirectArr() As String
        Dim objsStatutoryDirector As New strStatutoryDirector
        Try
            lblError.Text = ""
            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblCustomerName.Text = "" : lblOrgType.Text = "" : lblAddress.Text = ""
                lblCity.Text = "" : lblEmail.Text = ""
                lblContactPerson.Text = "" : lblMobileNumber.Text = ""
                If dgGeneral.Items(i).Cells(1).Text <> "&nbsp;" Then
                    lblCustomerName.Text = dgGeneral.Items(i).Cells(1).Text
                End If
                If dgGeneral.Items(i).Cells(2).Text <> "&nbsp;" Then
                    lblOrgType.Text = dgGeneral.Items(i).Cells(2).Text
                End If
                If dgGeneral.Items(i).Cells(3).Text <> "&nbsp;" Then
                    lblAddress.Text = dgGeneral.Items(i).Cells(3).Text
                End If
                If dgGeneral.Items(i).Cells(4).Text <> "&nbsp;" Then
                    lblCity.Text = dgGeneral.Items(i).Cells(4).Text
                End If
                If dgGeneral.Items(i).Cells(5).Text <> "&nbsp;" Then
                    lblEmail.Text = dgGeneral.Items(i).Cells(5).Text
                End If
                If dgGeneral.Items(i).Cells(6).Text <> "&nbsp;" Then
                    lblMobileNumber.Text = dgGeneral.Items(i).Cells(6).Text
                End If
                If dgGeneral.Items(i).Cells(7).Text <> "&nbsp;" Then
                    lblbusiness.Text = dgGeneral.Items(i).Cells(7).Text
                End If
                If dgGeneral.Items(i).Cells(8).Text <> "&nbsp;" Then
                    lblIndType.Text = dgGeneral.Items(i).Cells(8).Text
                End If
                If dgGeneral.Items(i).Cells(9).Text <> "&nbsp;" Then
                    lblRegNo.Text = dgGeneral.Items(i).Cells(9).Text
                Else
                    lblRegNo.Text = ""
                End If
                If dgGeneral.Items(i).Cells(10).Text <> "&nbsp;" Then
                    lblProffServiceOff1.Text = dgGeneral.Items(i).Cells(10).Text
                End If
                If dgGeneral.Items(i).Cells(11).Text <> "&nbsp;" Then
                    lblProffServiceOff2.Text = dgGeneral.Items(i).Cells(11).Text
                End If
                If dgGeneral.Items(i).Cells(12).Text <> "&nbsp;" Then
                    lbllocation1.Text = dgGeneral.Items(i).Cells(12).Text
                End If
                If dgGeneral.Items(i).Cells(13).Text <> "&nbsp;" Then
                    lblContactPers1.Text = dgGeneral.Items(i).Cells(13).Text
                End If
                If dgGeneral.Items(i).Cells(14).Text <> "&nbsp;" Then
                    lblAddress1.Text = dgGeneral.Items(i).Cells(14).Text
                End If
                If dgGeneral.Items(i).Cells(15).Text <> "&nbsp;" Then
                    lblCIN1.Text = dgGeneral.Items(i).Cells(15).Text
                End If
                If dgGeneral.Items(i).Cells(16).Text <> "&nbsp;" Then
                    lblTAN1.Text = dgGeneral.Items(i).Cells(16).Text
                End If
                If dgGeneral.Items(i).Cells(17).Text <> "&nbsp;" Then
                    lblGST1.Text = dgGeneral.Items(i).Cells(17).Text
                End If
                If dgGeneral.Items(i).Cells(18).Text <> "&nbsp;" Then
                    lbllocation2.Text = dgGeneral.Items(i).Cells(18).Text
                End If
                If dgGeneral.Items(i).Cells(19).Text <> "&nbsp;" Then
                    lblContactPers2.Text = dgGeneral.Items(i).Cells(19).Text
                End If
                If dgGeneral.Items(i).Cells(20).Text <> "&nbsp;" Then
                    lblAddress2.Text = dgGeneral.Items(i).Cells(20).Text
                End If
                If dgGeneral.Items(i).Cells(21).Text <> "&nbsp;" Then
                    lblCIN2.Text = dgGeneral.Items(i).Cells(21).Text
                End If
                If dgGeneral.Items(i).Cells(22).Text <> "&nbsp;" Then
                    lblTAN2.Text = dgGeneral.Items(i).Cells(22).Text
                End If
                If dgGeneral.Items(i).Cells(23).Text <> "&nbsp;" Then
                    lblGST2.Text = dgGeneral.Items(i).Cells(23).Text
                End If
                If dgGeneral.Items(i).Cells(24).Text <> "&nbsp;" Then
                    lbllocation3.Text = dgGeneral.Items(i).Cells(24).Text
                End If
                If dgGeneral.Items(i).Cells(25).Text <> "&nbsp;" Then
                    lblContactPers3.Text = dgGeneral.Items(i).Cells(25).Text
                End If
                If dgGeneral.Items(i).Cells(26).Text <> "&nbsp;" Then
                    lblAddress3.Text = dgGeneral.Items(i).Cells(26).Text
                End If
                If dgGeneral.Items(i).Cells(27).Text <> "&nbsp;" Then
                    lblCIN3.Text = dgGeneral.Items(i).Cells(27).Text
                End If
                If dgGeneral.Items(i).Cells(28).Text <> "&nbsp;" Then
                    lblTAN3.Text = dgGeneral.Items(i).Cells(28).Text
                End If
                If dgGeneral.Items(i).Cells(29).Text <> "&nbsp;" Then
                    lblGST3.Text = dgGeneral.Items(i).Cells(29).Text
                End If
                If dgGeneral.Items(i).Cells(30).Text <> "&nbsp;" Then
                    lbllocation4.Text = dgGeneral.Items(i).Cells(30).Text
                End If
                If dgGeneral.Items(i).Cells(31).Text <> "&nbsp;" Then
                    lblContactPers4.Text = dgGeneral.Items(i).Cells(31).Text
                End If
                If dgGeneral.Items(i).Cells(32).Text <> "&nbsp;" Then
                    lblAddress4.Text = dgGeneral.Items(i).Cells(32).Text
                End If
                If dgGeneral.Items(i).Cells(33).Text <> "&nbsp;" Then
                    lblCIN4.Text = dgGeneral.Items(i).Cells(33).Text
                End If
                If dgGeneral.Items(i).Cells(34).Text <> "&nbsp;" Then
                    lblTAN4.Text = dgGeneral.Items(i).Cells(34).Text
                End If
                If dgGeneral.Items(i).Cells(35).Text <> "&nbsp;" Then
                    lblGST4.Text = dgGeneral.Items(i).Cells(35).Text
                End If
                If dgGeneral.Items(i).Cells(36).Text <> "&nbsp;" Then
                    lbllocation5.Text = dgGeneral.Items(i).Cells(36).Text
                End If
                If dgGeneral.Items(i).Cells(37).Text <> "&nbsp;" Then
                    lblContactPers5.Text = dgGeneral.Items(i).Cells(37).Text
                End If
                If dgGeneral.Items(i).Cells(38).Text <> "&nbsp;" Then
                    lblAddress5.Text = dgGeneral.Items(i).Cells(38).Text
                End If
                If dgGeneral.Items(i).Cells(39).Text <> "&nbsp;" Then
                    lblCIN5.Text = dgGeneral.Items(i).Cells(39).Text
                End If
                If dgGeneral.Items(i).Cells(40).Text <> "&nbsp;" Then
                    lblTAN5.Text = dgGeneral.Items(i).Cells(40).Text
                End If
                If dgGeneral.Items(i).Cells(41).Text <> "&nbsp;" Then
                    lblGST5.Text = dgGeneral.Items(i).Cells(41).Text
                End If
                If dgGeneral.Items(i).Cells(42).Text <> "&nbsp;" Then
                    lbldirectName1.Text = dgGeneral.Items(i).Cells(42).Text
                End If
                If dgGeneral.Items(i).Cells(43).Text <> "&nbsp;" Then
                    Din1.Text = dgGeneral.Items(i).Cells(43).Text
                End If
                If dgGeneral.Items(i).Cells(44).Text <> "&nbsp;" Then
                    lbldirectName2.Text = dgGeneral.Items(i).Cells(44).Text
                End If
                If dgGeneral.Items(i).Cells(45).Text <> "&nbsp;" Then
                    Din2.Text = dgGeneral.Items(i).Cells(45).Text
                End If

                If lblCustomerName.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Customer Name. Line No " & iErrorLine & "." : lblError.Text = "Enter Customer Name. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblCustomerName.Text.Trim.Length > 500 Then
                    lblExcelValidationMsg.Text = "Customer Name exceeded maximum size(max 500 characters). Line No " & iErrorLine & "." : lblError.Text = "Customer Name exceeded maximum size(max 500 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblOrgType.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Organisation Type. Line No " & iErrorLine & "." : lblError.Text = "Enter Organisation Type. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblOrgType.Text.Trim.Length > 50 Then
                    lblExcelValidationMsg.Text = "Organisation Type exceeded maximum size(max 50 characters). Line No " & iErrorLine & "." : lblError.Text = "Organisation Type exceeded maximum size(max 50 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                If lblAddress.Text.Trim = "" Then
                    lblExcelValidationMsg.Text = "Enter Address. Line No " & iErrorLine & "." : lblError.Text = "Enter Address. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblAddress.Text.Trim.Length > 500 Then
                    lblExcelValidationMsg.Text = "Address exceeded maximum size(max 500 characters). Line No " & iErrorLine & "." : lblError.Text = "Address exceeded maximum size(max 500 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblCity.Text.Trim.Length > 15 Then
                    lblExcelValidationMsg.Text = "City exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "." : lblError.Text = "City exceeded maximum size(max 15 numbers). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If

                'If lblContactPerson.Text.Trim = "" Then
                '    lblExcelValidationMsg.Text = "Enter Contact Person. Line No " & iErrorLine & "." : lblError.Text = "Enter Contact Person. Line No " & iErrorLine & "."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                '    Exit Sub
                'End If
                If lblContactPerson.Text.Trim.Length > 500 Then
                    lblExcelValidationMsg.Text = "Contact Person exceeded maximum size(max 500 characters). Line No " & iErrorLine & "." : lblError.Text = "Contact Person exceeded maximum size(max 500 characters). Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
                If lblMobileNumber.Text.Trim <> "" Then
                    If lblMobileNumber.Text.Trim.Length > 10 Then
                        lblExcelValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers). Line No " & iErrorLine & "." : lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers). Line No " & iErrorLine & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If lblMobileNumber.Text.Trim.Length <> 10 Then
                        lblExcelValidationMsg.Text = "Enter valid 10 digits Mobile No. Line No " & iErrorLine & "." : lblError.Text = "Enter valid 10 digits Mobile No. Line No " & iErrorLine & "."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
                If objclsCustomerMaster.CheckOrganisationType(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.SafeSQL(lblOrgType.Text.Trim)) = False Then
                    lblExcelValidationMsg.Text = "Orgnisation Type not exists. Line No " & iErrorLine & "." : lblError.Text = "Orgnisation Type not exists. Line No " & iErrorLine & "."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                    Exit Sub
                End If
            Next

            For i = 0 To dgGeneral.Items.Count - 1
                iErrorLine = iErrorLine + 1
                lblCustomerName.Text = dgGeneral.Items(i).Cells(1).Text
                lblOrgType.Text = dgGeneral.Items(i).Cells(2).Text
                lblAddress.Text = dgGeneral.Items(i).Cells(3).Text
                lblCity.Text = dgGeneral.Items(i).Cells(4).Text
                lblEmail.Text = dgGeneral.Items(i).Cells(5).Text
                lblMobileNumber.Text = dgGeneral.Items(i).Cells(6).Text
                lblbusiness.Text = dgGeneral.Items(i).Cells(7).Text
                lblIndType.Text = dgGeneral.Items(i).Cells(8).Text
                lblRegNo.Text = dgGeneral.Items(i).Cells(9).Text
                lblProffServiceOff1.Text = dgGeneral.Items(i).Cells(10).Text
                lblProffServiceOff2.Text = dgGeneral.Items(i).Cells(11).Text
                lbllocation1.Text = dgGeneral.Items(i).Cells(12).Text
                lblContactPers1.Text = dgGeneral.Items(i).Cells(13).Text
                lblAddress1.Text = dgGeneral.Items(i).Cells(14).Text
                lblCIN1.Text = dgGeneral.Items(i).Cells(15).Text
                lblTAN1.Text = dgGeneral.Items(i).Cells(16).Text
                lblGST1.Text = dgGeneral.Items(i).Cells(17).Text
                lbllocation2.Text = dgGeneral.Items(i).Cells(18).Text
                lblContactPers2.Text = dgGeneral.Items(i).Cells(19).Text
                lblAddress2.Text = dgGeneral.Items(i).Cells(20).Text
                lblCIN2.Text = dgGeneral.Items(i).Cells(21).Text
                lblTAN2.Text = dgGeneral.Items(i).Cells(22).Text
                lblGST2.Text = dgGeneral.Items(i).Cells(23).Text
                lbllocation3.Text = dgGeneral.Items(i).Cells(24).Text
                lblContactPers3.Text = dgGeneral.Items(i).Cells(25).Text
                lblAddress3.Text = dgGeneral.Items(i).Cells(26).Text
                lblCIN3.Text = dgGeneral.Items(i).Cells(27).Text
                lblTAN3.Text = dgGeneral.Items(i).Cells(28).Text
                lblGST3.Text = dgGeneral.Items(i).Cells(29).Text
                lbllocation4.Text = dgGeneral.Items(i).Cells(30).Text
                lblContactPers4.Text = dgGeneral.Items(i).Cells(31).Text
                lblAddress4.Text = dgGeneral.Items(i).Cells(32).Text
                lblCIN4.Text = dgGeneral.Items(i).Cells(33).Text
                lblTAN4.Text = dgGeneral.Items(i).Cells(34).Text
                lblGST4.Text = dgGeneral.Items(i).Cells(35).Text
                lbllocation5.Text = dgGeneral.Items(i).Cells(36).Text
                lblContactPers5.Text = dgGeneral.Items(i).Cells(37).Text
                lblAddress5.Text = dgGeneral.Items(i).Cells(38).Text
                lblCIN5.Text = dgGeneral.Items(i).Cells(39).Text
                lblTAN5.Text = dgGeneral.Items(i).Cells(40).Text
                lblGST5.Text = dgGeneral.Items(i).Cells(41).Text
                lbldirectName1.Text = dgGeneral.Items(i).Cells(42).Text
                Din1.Text = dgGeneral.Items(i).Cells(43).Text
                lbldirectName2.Text = dgGeneral.Items(i).Cells(44).Text
                Din2.Text = dgGeneral.Items(i).Cells(45).Text




                objclsCustomerMaster.iCust_ID = 0
                objstrCust.CUST_NAME = objclsGRACeGeneral.SafeSQL(lblCustomerName.Text.Trim)
                objstrCust.CUST_WEBSITE = ""
                objstrCust.CUST_CITY = objclsGRACeGeneral.SafeSQL(lblCity.Text.Trim)
                objstrCust.CUST_ADDRESS = objclsGRACeGeneral.SafeSQL(lblAddress.Text.Trim)
                objstrCust.CUST_COMM_PIN = ""
                objstrCust.CUST_COMM_STATE = ""
                objstrCust.CUST_COMM_COUNTRY = ""
                objstrCust.CUST_COMM_FAX = ""
                objstrCust.CUST_COMM_TEL = ""
                objstrCust.CUST_COMM_Email = ""
                objstrCust.CUST_GROUPNAME = ""
                objstrCust.CUST_GROUPINDIVIDUAL = 0
                objstrCust.CUST_COMM_ADDRESS = ""
                objstrCust.CUST_EMAIL = objclsGRACeGeneral.SafeSQL(lblEmail.Text.Trim)
                objstrCust.CUST_COMM_CITY = ""
                objstrCust.CUST_PIN = ""
                objstrCust.CUST_STATE = ""
                objstrCust.CUST_COUNTRY = ""
                objstrCust.CUST_FAX = ""
                objstrCust.CUST_TELPHONE = ""
                objstrCust.CUST_ConEmailID = ""
                objstrCust.CUST_LOCATIONID = ""
                objstrCust.CUST_ORGID = 0
                objstrCust.CUST_UpdatedBy = sSession.UserID
                objstrCust.CUST_BOARDOFDIRECTORS = ""
                objstrCust.CUST_RoundOff = 0
                objstrCust.CUST_CommitmentDate = objclsGRACeGeneral.SafeSQL(lblbusiness.Text.Trim)
                objstrCust.CUST_INDTYPEID = objclsCustomerMaster.GetIndType(sSession.AccessCode, sSession.AccessCodeID, lblIndType.Text)
                If lblRegNo.Text = "&nbsp;" Then
                    lblRegNo.Text = ""
                End If
                objstrCust.CUSt_BranchId = objclsGRACeGeneral.SafeSQL(lblRegNo.Text.Trim)

                Dim iServiceOffr1 As String = 0
                If lblProffServiceOff1.Text.Trim <> "" Then
                    iServiceOffr1 = objclsCustomerMaster.GetProfServiceOffType(sSession.AccessCode, sSession.AccessCodeID, lblProffServiceOff1.Text)
                End If
                Dim iServiceOffr2 As String = 0
                If lblProffServiceOff2.Text <> "" Then
                    iServiceOffr2 = objclsCustomerMaster.GetProfServiceOffType(sSession.AccessCode, sSession.AccessCodeID, lblProffServiceOff2.Text)
                    iServiceOffr1 = iServiceOffr1 & "," & iServiceOffr2
                End If
                objstrCust.CUST_TASKS = iServiceOffr1

                objstrCust.CUST_CODE = objclsCustomerMaster.GetCustomerCode(sSession.AccessCode, sSession.AccessCodeID)
                objstrCust.CUST_ORGTYPEID = objclsCustomerMaster.GetOrgType(sSession.AccessCode, sSession.AccessCodeID, lblOrgType.Text)
                objstrCust.CUST_STATUS = "A"
                objstrCust.CUST_DELFLG = "A"
                objstrCust.CUST_CRBY = sSession.UserID
                objstrCust.CUST_APPROVEDBY = sSession.UserID
                objstrCust.CUST_IPAddress = sSession.IPAddress
                objstrCust.CUST_CompID = sSession.AccessCodeID
                'Arr = objclsCustomerMaster.SaveCustomerMasters(sSession.AccessCode, objclsCustomerMaster)
                Arr = objCust.SaveCustomerMaster(sSession.AccessCode, objstrCust)
                objCust.UpdateSatus(sSession.AccessCode, sSession.AccessCodeID, Arr(1))


                If lbllocation1.Text <> "" And lbllocation1.Text <> "&nbsp;" Then


                    objsCustLocation.Mas_Id = 0
                    objsCustLocation.Mas_code = lblCity.Text.Substring(0, 2)
                    objsCustLocation.Mas_Description = objclsGRACeGeneral.SafeSQL(lbllocation1.Text.Trim)
                    objsCustLocation.Mas_DelFlag = "A"
                    objsCustLocation.Mas_CustID = Arr(1)
                    objsCustLocation.Mas_Loc_Address = objclsGRACeGeneral.SafeSQL(lblAddress1.Text.Trim)
                    objsCustLocation.Mas_Contact_Person = objclsGRACeGeneral.SafeSQL(lblContactPers1.Text.Trim)
                    objsCustLocation.Mas_Contact_MobileNo = lblMobileNumber.Text
                    objsCustLocation.Mas_Contact_Email = lblEmail.Text
                    objsCustLocation.Mas_Contact_LandLineNo = ""
                    objsCustLocation.mas_Designation = ""
                    objsCustLocation.Mas_CRBY = sSession.UserID
                    objsCustLocation.Mas_STATUS = "A"
                    objsCustLocation.Mas_IPAddress = sSession.IPAddress
                    objsCustLocation.Mas_CompID = sSession.AccessCodeID
                    LocArr = objCust.SaveCustomerLocation(sSession.AccessCode, objsCustLocation)


                    If lblCIN1.Text <> "" And lblCIN1.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "CIN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblCIN1.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblTAN1.Text <> "" And lblTAN1.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "TAN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblTAN1.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblGST1.Text <> "" And lblGST1.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "GST"
                        objstrCUSTAccountingTemplate.sCust_Value = lblGST1.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                End If


                If lbllocation2.Text <> "" And lbllocation2.Text <> "&nbsp;" Then
                    objsCustLocation.Mas_Id = 0
                    objsCustLocation.Mas_code = lblCity.Text.Substring(0, 2)
                    objsCustLocation.Mas_Description = objclsGRACeGeneral.SafeSQL(lbllocation2.Text.Trim)
                    objsCustLocation.Mas_DelFlag = "A"
                    objsCustLocation.Mas_CustID = Arr(1)
                    objsCustLocation.Mas_Loc_Address = objclsGRACeGeneral.SafeSQL(lblAddress2.Text.Trim)
                    objsCustLocation.Mas_Contact_Person = objclsGRACeGeneral.SafeSQL(lblContactPers2.Text.Trim)
                    objsCustLocation.Mas_Contact_MobileNo = lblMobileNumber.Text
                    objsCustLocation.Mas_Contact_Email = lblEmail.Text
                    objsCustLocation.Mas_Contact_LandLineNo = ""
                    objsCustLocation.mas_Designation = ""
                    objsCustLocation.Mas_CRBY = sSession.UserID
                    objsCustLocation.Mas_STATUS = "A"
                    objsCustLocation.Mas_IPAddress = sSession.IPAddress
                    objsCustLocation.Mas_CompID = sSession.AccessCodeID
                    LocArr = objCust.SaveCustomerLocation(sSession.AccessCode, objsCustLocation)
                    If lblCIN2.Text <> "" And lblCIN2.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "CIN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblCIN2.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblTAN2.Text <> "" And lblTAN2.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "TAN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblTAN2.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblGST2.Text <> "" And lblGST2.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "GST"
                        objstrCUSTAccountingTemplate.sCust_Value = lblGST2.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                End If
                If lbllocation3.Text <> "" And lbllocation3.Text <> "&nbsp;" Then
                    objsCustLocation.Mas_Id = 0
                    objsCustLocation.Mas_code = lblCity.Text.Substring(0, 2)
                    objsCustLocation.Mas_Description = objclsGRACeGeneral.SafeSQL(lbllocation3.Text.Trim)
                    objsCustLocation.Mas_DelFlag = "A"
                    objsCustLocation.Mas_CustID = Arr(1)
                    objsCustLocation.Mas_Loc_Address = objclsGRACeGeneral.SafeSQL(lblAddress3.Text.Trim)
                    objsCustLocation.Mas_Contact_Person = objclsGRACeGeneral.SafeSQL(lblContactPers3.Text.Trim)
                    objsCustLocation.Mas_Contact_MobileNo = lblMobileNumber.Text
                    objsCustLocation.Mas_Contact_Email = lblEmail.Text
                    objsCustLocation.Mas_Contact_LandLineNo = ""
                    objsCustLocation.mas_Designation = ""
                    objsCustLocation.Mas_CRBY = sSession.UserID
                    objsCustLocation.Mas_STATUS = "A"
                    objsCustLocation.Mas_IPAddress = sSession.IPAddress
                    objsCustLocation.Mas_CompID = sSession.AccessCodeID
                    LocArr = objCust.SaveCustomerLocation(sSession.AccessCode, objsCustLocation)
                    If lblCIN3.Text <> "" And lblCIN3.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "CIN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblCIN3.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblTAN3.Text <> "" And lblTAN3.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "TAN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblTAN3.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblGST3.Text <> "" And lblGST3.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "GST"
                        objstrCUSTAccountingTemplate.sCust_Value = lblGST3.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                End If
                If lbllocation4.Text <> "" And lbllocation4.Text <> "&nbsp;" Then
                    objsCustLocation.Mas_Id = 0
                    objsCustLocation.Mas_code = lblCity.Text.Substring(0, 2)
                    objsCustLocation.Mas_Description = objclsGRACeGeneral.SafeSQL(lbllocation4.Text.Trim)
                    objsCustLocation.Mas_DelFlag = "A"
                    objsCustLocation.Mas_CustID = Arr(1)
                    objsCustLocation.Mas_Loc_Address = objclsGRACeGeneral.SafeSQL(lblAddress4.Text.Trim)
                    objsCustLocation.Mas_Contact_Person = objclsGRACeGeneral.SafeSQL(lblContactPers4.Text.Trim)
                    objsCustLocation.Mas_Contact_MobileNo = lblMobileNumber.Text
                    objsCustLocation.Mas_Contact_Email = lblEmail.Text
                    objsCustLocation.Mas_Contact_LandLineNo = ""
                    objsCustLocation.mas_Designation = ""
                    objsCustLocation.Mas_CRBY = sSession.UserID
                    objsCustLocation.Mas_STATUS = "A"
                    objsCustLocation.Mas_IPAddress = sSession.IPAddress
                    objsCustLocation.Mas_CompID = sSession.AccessCodeID
                    LocArr = objCust.SaveCustomerLocation(sSession.AccessCode, objsCustLocation)
                    If lblCIN4.Text <> "" And lblCIN4.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "CIN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblCIN4.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblTAN4.Text <> "" And lblTAN4.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "TAN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblTAN4.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblGST4.Text <> "" And lblGST4.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "GST"
                        objstrCUSTAccountingTemplate.sCust_Value = lblGST4.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                End If
                If lbllocation5.Text <> "" And lbllocation5.Text <> "&nbsp;" Then
                    objsCustLocation.Mas_Id = 0
                    objsCustLocation.Mas_code = lblCity.Text.Substring(0, 2)
                    objsCustLocation.Mas_Description = objclsGRACeGeneral.SafeSQL(lbllocation5.Text.Trim)
                    objsCustLocation.Mas_DelFlag = "A"
                    objsCustLocation.Mas_CustID = Arr(1)
                    objsCustLocation.Mas_Loc_Address = objclsGRACeGeneral.SafeSQL(lblAddress5.Text.Trim)
                    objsCustLocation.Mas_Contact_Person = objclsGRACeGeneral.SafeSQL(lblContactPers5.Text.Trim)
                    objsCustLocation.Mas_Contact_MobileNo = lblMobileNumber.Text
                    objsCustLocation.Mas_Contact_Email = lblEmail.Text
                    objsCustLocation.Mas_Contact_LandLineNo = ""
                    objsCustLocation.mas_Designation = ""
                    objsCustLocation.Mas_CRBY = sSession.UserID
                    objsCustLocation.Mas_STATUS = "A"
                    objsCustLocation.Mas_IPAddress = sSession.IPAddress
                    objsCustLocation.Mas_CompID = sSession.AccessCodeID
                    LocArr = objCust.SaveCustomerLocation(sSession.AccessCode, objsCustLocation)
                    If lblCIN5.Text <> "" And lblCIN5.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "CIN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblCIN5.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblTAN5.Text <> "" And lblTAN5.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "TAN"
                        objstrCUSTAccountingTemplate.sCust_Value = lblTAN5.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                    If lblGST5.Text <> "" And lblGST5.Text <> "&nbsp;" Then
                        objstrCUSTAccountingTemplate.iCust_PKID = 0
                        objstrCUSTAccountingTemplate.iCust_ID = Arr(1)
                        objstrCUSTAccountingTemplate.sCust_Desc = "GST"
                        objstrCUSTAccountingTemplate.sCust_Value = lblGST5.Text
                        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                        objstrCUSTAccountingTemplate.sCust_Status = "A"
                        objstrCUSTAccountingTemplate.iCust_AttchID = 0
                        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                        objstrCUSTAccountingTemplate.iCust_LocationId = LocArr(1)
                        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    End If
                End If

                If lbldirectName1.Text <> "" And lbldirectName1.Text <> "&nbsp;" Then
                    objsStatutoryDirector.iSSD_Id = 0
                    objsStatutoryDirector.iSSD_CustID = Arr(1)
                    objsStatutoryDirector.sSSD_DirectorName = objclsGRACeGeneral.SafeSQL(lbldirectName1.Text.Trim)
                    objsStatutoryDirector.dSSD_DOB = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objsStatutoryDirector.sSSD_DIN = objclsGRACeGeneral.SafeSQL(Din1.Text.Trim)
                    objsStatutoryDirector.sSSD_MobileNo = ""
                    objsStatutoryDirector.sSSD_Email = ""
                    objsStatutoryDirector.sSSD_Remarks = ""
                    objsStatutoryDirector.iSSD_CRBY = sSession.UserID
                    objsStatutoryDirector.dSSD_CRON = DateTime.Today
                    objsStatutoryDirector.iSSD_UpdatedBy = sSession.UserID
                    objsStatutoryDirector.dSSD_UpdatedOn = DateTime.Today
                    objsStatutoryDirector.sSSD_IPAddress = sSession.IPAddress
                    objsStatutoryDirector.iSSD_CompID = sSession.AccessCodeID
                    objsStatutoryDirector.sSSD_STATUS = "C"
                    objsStatutoryDirector.sSSD_DelFlag = "A"

                    DirectArr = objCust.SaveCustomerStatutoryDirector(sSession.AccessCode, objsStatutoryDirector)
                End If
                If lbldirectName2.Text <> "" And lbldirectName2.Text <> "&nbsp;" Then
                    objsStatutoryDirector.iSSD_Id = 0
                    objsStatutoryDirector.iSSD_CustID = Arr(1)
                    objsStatutoryDirector.sSSD_DirectorName = objclsGRACeGeneral.SafeSQL(lbldirectName2.Text.Trim)
                    objsStatutoryDirector.dSSD_DOB = Date.ParseExact("01/01/1900", "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    objsStatutoryDirector.sSSD_DIN = objclsGRACeGeneral.SafeSQL(Din2.Text.Trim)
                    objsStatutoryDirector.sSSD_MobileNo = ""
                    objsStatutoryDirector.sSSD_Email = ""
                    objsStatutoryDirector.sSSD_Remarks = ""
                    objsStatutoryDirector.iSSD_CRBY = sSession.UserID
                    objsStatutoryDirector.dSSD_CRON = DateTime.Today
                    objsStatutoryDirector.iSSD_UpdatedBy = sSession.UserID
                    objsStatutoryDirector.dSSD_UpdatedOn = DateTime.Today
                    objsStatutoryDirector.sSSD_IPAddress = sSession.IPAddress
                    objsStatutoryDirector.iSSD_CompID = sSession.AccessCodeID
                    objsStatutoryDirector.sSSD_STATUS = "C"
                    objsStatutoryDirector.sSSD_DelFlag = "A"

                    Arr = objCust.SaveCustomerStatutoryDirector(sSession.AccessCode, objsStatutoryDirector)
                End If

                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "Uploaded", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Next
            lblExcelValidationMsg.Text = "Successfully Uploaded." : lblError.Text = "Successfully Uploaded."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalExcelValidation').modal('show');", True)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class