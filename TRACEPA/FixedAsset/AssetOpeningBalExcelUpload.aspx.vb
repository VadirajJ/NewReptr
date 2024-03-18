Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports System.Globalization
Partial Class AssetOpeningBalExcelUpload
    Inherits System.Web.UI.Page

    Private Shared sFormName As String = "AssetOpeningBalExcelUpload"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private Shared sIKBBackStatus As String
    Dim objOPExcel As New ClsAssetOpeningBalExcelUpload
    Private Shared sSession As AllSession
    Dim objClsFASGnrl As New clsGRACeGeneral
    Private Shared sFile As String
    Dim dtExcel As New DataTable
    Private Shared dttable As New DataTable
    Private Shared objFxdAsst As New ClsFexedAsst
    Private objAsst As New ClsAssetMaster

    Private Shared FStartDate As Date
    Private Shared FEndDate As Date
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtUpload.ImageUrl = "~/Images/Upload24.png"
        ImgbtnUpload.ImageUrl = "~/Images/Upload24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        ImgBtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                dttable = Nothing
                LoadCustomer()

                BindYearMaster()
                ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                ddlFinancialYear_SelectedIndexChanged(sender, e)


                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        ddlCustomerName_SelectedIndexChanged(sender, e)
                    End If
                End If
                LoadZone()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            FStartDate = objClsFASGnrl.FormatDtForRDBMS(objClsFASGnrl.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
            FEndDate = objClsFASGnrl.FormatDtForRDBMS(objClsFASGnrl.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Try
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function LoadCustomer() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objAsst.LoadCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataSource = dt
            ddlCustomerName.DataTextField = "CUST_NAME"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub LoadZone()
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccZone(sSession.AccessCode, sSession.AccessCodeID)
            ddlAccZone.DataTextField = "org_name"
            ddlAccZone.DataValueField = "org_node"
            ddlAccZone.DataSource = dt
            ddlAccZone.DataBind()
            ddlAccZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadZone" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadRegion(ByVal iAccZone As Integer)
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccRgn(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
            ddlAccRgn.DataTextField = "org_name"
            ddlAccRgn.DataValueField = "org_node"
            ddlAccRgn.DataSource = dt
            ddlAccRgn.DataBind()
            ddlAccRgn.Items.Insert(0, "Select Region")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadRegion" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAccZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccZone.SelectedIndexChanged
        Try
            If ddlAccZone.SelectedIndex > 0 Then
                LoadRegion(ddlAccZone.SelectedValue)
            Else
                ddlAccRgn.Items.Clear() : ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccZone_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadArea(ByVal iAccRgn As Integer)
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccArea(sSession.AccessCode, sSession.AccessCodeID, iAccRgn)
            ddlAccArea.DataTextField = "org_name"
            ddlAccArea.DataValueField = "org_node"
            ddlAccArea.DataSource = dt
            ddlAccArea.DataBind()
            ddlAccArea.Items.Insert(0, "Select Area")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadArea" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAccRgn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccRgn.SelectedIndexChanged
        Try
            If ddlAccRgn.SelectedIndex > 0 Then
                LoadArea(ddlAccRgn.SelectedValue)
            Else
                ddlAccArea.Items.Clear() : ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccRgn_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadAccBrnch(ByVal iAccarea As Integer)
        Dim dt As New DataTable
        Try
            dt = objOPExcel.LoadAccBrnch(sSession.AccessCode, sSession.AccessCodeID, iAccarea)
            ddlAccBrnch.DataTextField = "org_name"
            ddlAccBrnch.DataValueField = "org_node"
            ddlAccBrnch.DataSource = dt
            ddlAccBrnch.DataBind()
            ddlAccBrnch.Items.Insert(0, "Select Branch")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAccBrnch" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAccArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAccArea.SelectedIndexChanged
        Try
            If ddlAccArea.SelectedIndex > 0 Then
                LoadAccBrnch(ddlAccArea.SelectedValue)
            Else
                ddlAccBrnch.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAccArea_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
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
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            GvOPExcel.DataSource = Nothing
            GvOPExcel.DataBind()

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
                    lblError.Text = "Select Excel file only." : lblFXOPBalExcelMsg.Text = "Select Excel file only."
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblFXOPBalExcelMsg.Text = "Select Excel file."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOk_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Protected Sub lnDown_Click(sender As Object, e As EventArgs) Handles lnDown.Click
        Try
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=AssetMaster-Upload.xlsx")
            Response.TransmitFile(Server.MapPath("~\SampleExcels\AssetMaster-Upload.xlsx"))
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnDown_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Dim sDateofPur As String = ""
    '    Dim sAmt As String
    '    Dim sString, sCode As String
    '    Dim bCheck As Boolean
    '    Dim iSupId As Integer
    '    Try
    '        dt.Columns.Add("Slno")
    '        dt.Columns.Add("AssetTransfer")
    '        dt.Columns.Add("CurrencyTypes")
    '        dt.Columns.Add("currencyAmount")
    '        dt.Columns.Add("ActualLocation")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("TransactionType")
    '        dt.Columns.Add("SupplierName")
    '        dt.Columns.Add("supplierCode")
    '        dt.Columns.Add("AssetType")
    '        dt.Columns.Add("AssetRefNo")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("ItemCode")
    '        dt.Columns.Add("ItemDescription")
    '        dt.Columns.Add("Quantity")
    '        dt.Columns.Add("DateofPurchase")
    '        dt.Columns.Add("DateOfCommission")
    '        dt.Columns.Add("Amount")
    '        dt.Columns.Add("Depreciation")
    '        If ddlSheetName.SelectedIndex > 0 Then
    '            dtExcel = LoadExcel(sFile)
    '            If dtExcel.Rows.Count > 0 Then
    '                For i = 0 To dtExcel.Rows.Count - 1
    '                    Dim dRow As DataRow
    '                    dRow = dt.NewRow
    '                    If IsDBNull(dtExcel.Rows(i).Item("Slno")) = False Then
    '                        If dtExcel.Rows(i).Item("Slno").ToString <> "&nbsp;" Then
    '                            dRow("Slno") = dtExcel.Rows(i).Item("Slno")
    '                        Else
    '                            dRow("Slno") = 0
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("AssetTransfer").ToString() = "" Then
    '                        dRow("AssetTransfer") = ""
    '                    Else
    '                        sString = UCase(dtExcel.Rows(i).Item("AssetTransfer"))
    '                        If sString = "LOCAL" Then
    '                            If Stringcheck(sString) = False Then
    '                                lblError.Text = "Enter Valid AssetTransfer"
    '                                Exit Sub
    '                            Else
    '                                dRow("AssetTransfer") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetTransfer"))
    '                            End If
    '                        ElseIf sString = "IMPORTED" Then
    '                            If Stringcheck(sString) = False Then
    '                                lblError.Text = "Enter Valid AssetTransfer"
    '                                Exit Sub
    '                            Else
    '                                dRow("AssetTransfer") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetTransfer"))
    '                            End If
    '                        Else
    '                            lblError.Text = "Asset Transfer Not Matched"
    '                            Exit Sub
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("currencyAmount").ToString() = "" Then
    '                        dRow("currencyAmount") = "0.00"
    '                    Else
    '                        sAmt = dtExcel.Rows(i).Item("currencyAmount")
    '                        If Amountcheck(sAmt) = False Then
    '                            lblError.Text = "Enter Valid currencyAmount"
    '                            Exit Sub
    '                        Else
    '                            dRow("currencyAmount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("currencyAmount"))
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("CurrencyTypes").ToString() = "" Then
    '                        dRow("CurrencyTypes") = ""
    '                    Else
    '                        sString = dtExcel.Rows(i).Item("CurrencyTypes")
    '                        sCode = objOPExcel.LoadCurrencyName(sSession.AccessCode, sSession.AccessCodeID, sString)
    '                        If sCode <> "" Then
    '                            If CurrencyCode(sCode) = False Then
    '                                lblError.Text = "Enter Valid CurrencyTypes"
    '                                Exit Sub
    '                            Else
    '                                dRow("CurrencyTypes") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("CurrencyTypes"))
    '                            End If
    '                        Else
    '                            lblError.Text = "Currency name not Matched"
    '                            Exit Sub
    '                        End If
    '                    End If

    '                    If dtExcel.Rows(i).Item("ActualLocation").ToString() = "" Then
    '                        dRow("ActualLocation") = ""
    '                    Else
    '                        sString = dtExcel.Rows(i).Item("ActualLocation")
    '                        If CurrencyCheck1(sString) = False Then
    '                            lblError.Text = "Enter Valid ActualLocation"
    '                            Exit Sub
    '                        Else
    '                            dRow("ActualLocation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("ActualLocation"))
    '                        End If
    '                    End If

    '                    If dtExcel.Rows(i).Item("AssetAge").ToString() = "" Then
    '                        dRow("AssetAge") = "0.00"
    '                    Else
    '                        sAmt = dtExcel.Rows(i).Item("AssetAge")
    '                        If Amountcheck(sAmt) = False Then
    '                            lblError.Text = "Enter Valid AssetAge"
    '                            Exit Sub
    '                        Else
    '                            dRow("AssetAge") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetAge"))
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("TransactionType").ToString() = "" Then
    '                        dRow("TransactionType") = ""
    '                    Else
    '                        sString = UCase(dtExcel.Rows(i).Item("TransactionType"))
    '                        If sString = "ADDITION" Then
    '                            If Stringcheck(sString) = False Then
    '                                lblError.Text = "Enter Valid TransactionType"
    '                                Exit Sub
    '                            Else
    '                                dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
    '                            End If
    '                        ElseIf sString = "TRANSFERS" Then
    '                            If Stringcheck(sString) = False Then
    '                                lblError.Text = "Enter Valid TransactionType"
    '                                Exit Sub
    '                            Else
    '                                dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
    '                            End If
    '                        ElseIf sString = "REVALUATION" Then
    '                            If Stringcheck(sString) = False Then
    '                                lblError.Text = "Enter Valid TransactionType"
    '                                Exit Sub
    '                            Else
    '                                dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
    '                            End If
    '                        ElseIf sString = "FOREIGN EXCHANGE" Then
    '                            If Stringcheck(sString) = False Then
    '                                lblError.Text = "Enter Valid TransactionType"
    '                                Exit Sub
    '                            Else
    '                                dRow("TransactionType") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("TransactionType")))
    '                            End If
    '                        Else
    '                            lblError.Text = "Transaction Type Not Matched"
    '                            Exit Sub
    '                        End If
    '                    End If

    '                    If dtExcel.Rows(i).Item("SupplierName").ToString() = "" Then
    '                        dRow("SupplierName") = ""
    '                    Else
    '                        sString = dtExcel.Rows(i).Item("SupplierName")
    '                        iSupId = objOPExcel.GetSupplierName(sSession.AccessCode, sSession.AccessCodeID, sString)
    '                        If iSupId > 0 Then
    '                            If SupplierNamecheck(iSupId) = False Then
    '                                lblError.Text = "Enter Valid SupplierName"
    '                                Exit Sub
    '                            Else
    '                                dRow("SupplierName") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("SupplierName"))
    '                            End If
    '                        Else
    '                            lblError.Text = "Supplier name not matched,Create in supplier Master form"
    '                            Exit Sub
    '                        End If
    '                    End If

    '                    If dtExcel.Rows(i).Item("supplierCode").ToString() = "" Then
    '                        dRow("supplierCode") = ""
    '                    Else
    '                        sAmt = dtExcel.Rows(i).Item("supplierCode")
    '                        iSupId = objOPExcel.GetSupplierID1(sSession.AccessCode, sSession.AccessCodeID, dtExcel.Rows(i).Item("SupplierName"), sAmt)
    '                        If iSupId > 0 Then
    '                            'If SupplierCodeCheck(sAmt) = False Then
    '                            '    lblError.Text = "Enter Valid supplierCode"
    '                            '    Exit Sub
    '                            'Else
    '                            dRow("supplierCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("supplierCode"))
    '                            'End If
    '                            'Else
    '                            '    lblError.Text = "Supplier Code Matched for Given Supplier,Create in Supplier Master from"
    '                            '    Exit Sub
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("AssetType").ToString() = "" Then
    '                        lblError.Text = "AssetType Can not be blank"
    '                        Exit Sub
    '                    Else
    '                        sString = dtExcel.Rows(i).Item("AssetType")
    '                        bCheck = objOPExcel.GetAssetType1(sSession.AccessCode, sSession.AccessCodeID, sString)
    '                        If bCheck = True Then
    '                            If StringcheckArea(sString) = False Then
    '                                lblError.Text = "Enter Valid Asset Type"
    '                                Exit Sub
    '                            Else
    '                                dRow("AssetType") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetType"))
    '                            End If
    '                        Else
    '                            lblError.Text = "AssetType not matched,Create in Chart of Accounts"
    '                            Exit Sub
    '                        End If
    '                    End If

    '                    If IsDBNull(dtExcel.Rows(i).Item("AssetRefNo")) = True Then
    '                        lblError.Text = "Asset Reference no Can not be blank"
    '                        Exit Sub
    '                    Else
    '                        If dtExcel.Rows(i).Item("AssetRefNo").ToString <> "&nbsp;" Then
    '                            dRow("AssetRefNo") = dtExcel.Rows(i).Item("AssetRefNo")
    '                        End If
    '                    End If

    '                    If IsDBNull(dtExcel.Rows(i).Item("Description")) = False Then
    '                        If dtExcel.Rows(i).Item("Description").ToString <> "&nbsp;" Then
    '                            dRow("Description") = dtExcel.Rows(i).Item("Description")
    '                        Else
    '                            dRow("Description") = ""
    '                        End If
    '                    End If

    '                    If IsDBNull(dtExcel.Rows(i).Item("ItemCode")) = False Then
    '                        If dtExcel.Rows(i).Item("ItemCode").ToString <> "&nbsp;" Then
    '                            dRow("ItemCode") = dtExcel.Rows(i).Item("ItemCode")
    '                        Else
    '                            dRow("ItemCode") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item("ItemDescription")) = False Then
    '                        If dtExcel.Rows(i).Item("ItemDescription").ToString <> "&nbsp;" Then
    '                            dRow("ItemDescription") = dtExcel.Rows(i).Item("ItemDescription")
    '                        Else
    '                            dRow("ItemDescription") = ""
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("Quantity").ToString() = "" Then
    '                        dRow("Quantity") = ""
    '                    Else
    '                        sAmt = dtExcel.Rows(i).Item("Quantity")
    '                        If Amountcheck(sAmt) = False Then
    '                            lblError.Text = "Enter Valid Quantity"
    '                            Exit Sub
    '                        Else
    '                            dRow("Quantity") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Quantity"))
    '                        End If
    '                    End If
    '                    If Trim(dtExcel.Rows(i).Item("DateofPurchase").ToString()) = "" Then
    '                        dRow("DateofPurchase") = ""
    '                    Else
    '                        sDateofPur = dtExcel.Rows(i).Item("DateofPurchase")
    '                        If Datecheck(sDateofPur) = False Then
    '                            lblError.Text = "Enter Valid Date"
    '                            Exit Sub
    '                        Else
    '                            dRow("DateofPurchase") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("DateofPurchase"))
    '                        End If
    '                    End If

    '                    If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
    '                        dRow("DateOfCommission") = ""
    '                    Else
    '                        sDateofPur = dtExcel.Rows(i).Item("DateOfCommission")
    '                        If Datecheck(sDateofPur) = False Then
    '                            lblError.Text = "Enter Valid Date"
    '                            Exit Sub
    '                        Else
    '                            dRow("DateOfCommission") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("DateOfCommission"))
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("Amount").ToString() = "" Then
    '                        lblError.Text = "Amount  Can not be blank"
    '                        Exit Sub
    '                    Else
    '                        sAmt = dtExcel.Rows(i).Item("Amount")
    '                        If Amountcheck(sAmt) = False Then
    '                            lblError.Text = "Enter Valid Amount"
    '                            Exit Sub
    '                        Else
    '                            dRow("Amount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Amount"))
    '                        End If
    '                    End If
    '                    If dtExcel.Rows(i).Item("Depreciation").ToString() = "" Then
    '                        lblError.Text = "Amount  Can not be blank"
    '                        Exit Sub
    '                    Else
    '                        sAmt = dtExcel.Rows(i).Item("Depreciation")
    '                        If Amountcheck(sAmt) = False Then
    '                            lblError.Text = "Enter Valid Amount"
    '                            Exit Sub
    '                        Else
    '                            dRow("Depreciation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Depreciation"))
    '                        End If
    '                    End If
    '                    dt.Rows.Add(dRow)
    '                Next
    '                If IsNothing(dt) = True Then
    '                    Exit Sub
    '                End If
    '                GvOPExcel.DataSource = dt
    '                GvOPExcel.DataBind()
    '                dttable = dt.Copy
    '            Else
    '                lblError.Text = "No Data"
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
    '    End Try
    'End Sub
    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sDateofPur As String = ""
        Dim sAmt As String
        Dim sString, sCode As String
        Dim bCheck As Boolean
        Dim iSupId As Integer
        Dim lblAssetClass As New Label
        Dim iAssetType As Integer
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("DateOfCommission")
            dt.Columns.Add("UnitsofMeasurement")
            dt.Columns.Add("AssetAge")

            If ddlSheetName.SelectedIndex > 0 Then
                dtExcel = LoadExcel(sFile)
                If dtExcel.Rows.Count > 0 Then
                    For i = 0 To dtExcel.Rows.Count - 1
                        Dim dRow As DataRow
                        dRow = dt.NewRow
                        If IsDBNull(dtExcel.Rows(i).Item("Slno")) = False Then
                            If dtExcel.Rows(i).Item("Slno").ToString <> "&nbsp;" Then
                                dRow("Slno") = dtExcel.Rows(i).Item("Slno")
                            Else
                                dRow("Slno") = 0
                            End If
                        End If

                        'If IsDBNull(dtExcel.Rows(i).Item("Location")) = False Then
                        If dtExcel.Rows(i).Item("Location").ToString() = "" Then
                            lblError.Text = "Location Can not be blank - Line No" & i + 1
                            Exit Sub
                        Else
                            sString = dtExcel.Rows(i).Item("Location")
                            bCheck = objOPExcel.GetLocation(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                            If bCheck = True Then
                                dRow("Location") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Location"))
                            Else
                                lblError.Text = "( " & sString & ") not matched,Create in Location SetUp- Line No" & i + 1
                                Exit Sub
                            End If
                        End If
                        'End If

                        If IsDBNull(dtExcel.Rows(i).Item("Division")) = False Then
                            If dtExcel.Rows(i).Item("Division").ToString() = "" Then
                                lblError.Text = "Division Can not be blank - Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("Division")
                                bCheck = objOPExcel.GetLocation(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                If bCheck = True Then
                                    dRow("Division") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Division"))
                                Else
                                    lblError.Text = "( " & sString & ") not matched,Create in Location SetUp- Line No" & i + 1
                                    Exit Sub
                                End If
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item("Department")) = False Then
                            If dtExcel.Rows(i).Item("Department").ToString() = "" Then
                                lblError.Text = "Department Can not be blank - Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("Department")
                                bCheck = objOPExcel.GetLocation(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                If bCheck = True Then
                                    dRow("Department") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Department"))
                                Else
                                    lblError.Text = "( " & sString & ") not matched,Create in Location SetUp- Line No" & i + 1
                                    Exit Sub
                                End If
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item("Bay")) = False Then
                            If dtExcel.Rows(i).Item("Bay").ToString() = "" Then
                                lblError.Text = "Bay Can not be blank - Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("Bay")
                                bCheck = objOPExcel.GetLocation(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                If bCheck = True Then
                                    dRow("Bay") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Bay"))
                                Else
                                    lblError.Text = "( " & sString & ") not matched,Create in Location SetUp- Line No" & i + 1
                                    Exit Sub
                                End If
                            End If
                        End If

                        'If IsDBNull(dtExcel.Rows(i).Item("Location")) = False Then
                        '    If dtExcel.Rows(i).Item("Location").ToString <> "&nbsp;" Then
                        '        dRow("Location") = dtExcel.Rows(i).Item("Location")
                        '    Else
                        '        dRow("Location") = ""
                        '    End If
                        'End If

                        'If IsDBNull(dtExcel.Rows(i).Item("Division")) = False Then
                        '    If dtExcel.Rows(i).Item("Division").ToString <> "&nbsp;" Then
                        '        dRow("Division") = dtExcel.Rows(i).Item("Division")
                        '    Else
                        '        dRow("Division") = ""
                        '    End If
                        'End If

                        'If IsDBNull(dtExcel.Rows(i).Item("Department")) = False Then
                        '    If dtExcel.Rows(i).Item("Department").ToString <> "&nbsp;" Then
                        '        dRow("Department") = dtExcel.Rows(i).Item("Department")
                        '    Else
                        '        dRow("Department") = ""
                        '    End If
                        'End If

                        'If IsDBNull(dtExcel.Rows(i).Item("Bay")) = False Then
                        '    If dtExcel.Rows(i).Item("Bay").ToString <> "&nbsp;" Then
                        '        dRow("Bay") = dtExcel.Rows(i).Item("Bay")
                        '    Else
                        '        dRow("Bay") = ""
                        '    End If
                        'End If

                        'If IsDBNull(dtExcel.Rows(i).Item("AssetClass")) = False Then
                        If dtExcel.Rows(i).Item("AssetClass").ToString() = "" Then
                            lblError.Text = "Asset Class Can not be blank - Line No" & i + 1
                            Exit Sub
                        Else
                            sString = dtExcel.Rows(i).Item("AssetClass")
                            bCheck = objOPExcel.GetAssetType1(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                            If bCheck = True Then
                                dRow("AssetClass") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetClass"))
                            Else
                                lblError.Text = "( " & sString & ") not matched,Create in Asset Class Master- Line No" & i + 1
                                Exit Sub
                            End If
                        End If
                        'End If

                        If IsDBNull(dtExcel.Rows(i).Item("AssetCode")) = False Then
                            If dtExcel.Rows(i).Item("AssetCode").ToString <> "&nbsp;" Then
                                dRow("AssetCode") = dtExcel.Rows(i).Item("AssetCode")
                            Else
                                dRow("AssetCode") = ""
                            End If
                        End If

                        sString = dtExcel.Rows(i).Item("AssetClass")
                        iAssetType = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)

                        'If IsDBNull(dtExcel.Rows(i).Item("AssetDescription")) = False Then
                        If dtExcel.Rows(i).Item("AssetDescription").ToString() = "" Then
                            lblError.Text = "Asset Can not be blank - Line No" & i + 1
                            Exit Sub
                        Else
                            sString = dtExcel.Rows(i).Item("AssetDescription")
                            bCheck = objOPExcel.GetAsset1(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue, iAssetType)
                            If bCheck = False Then
                                dRow("AssetDescription") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetDescription"))
                            Else
                                lblError.Text = "( " & sString & ") is Already Exist, Please Enter different Asset - Line No" & i + 1
                                Exit Sub
                            End If
                        End If
                        'End If

                        'If IsDBNull(dtExcel.Rows(i).Item("AssetDescription")) = False Then
                        '    If dtExcel.Rows(i).Item("AssetDescription").ToString <> "&nbsp;" Then
                        '        dRow("AssetDescription") = dtExcel.Rows(i).Item("AssetDescription")
                        '    Else
                        '        dRow("AssetDescription") = ""
                        '    End If
                        'End If

                        If IsDBNull(dtExcel.Rows(i).Item("Quantity")) = False Then
                            If dtExcel.Rows(i).Item("Quantity").ToString() = "" Then
                                dRow("Quantity") = ""
                            Else
                                sAmt = dtExcel.Rows(i).Item("Quantity")
                                If Amountcheck(sAmt) = False Then
                                    lblError.Text = "Enter Valid Quantity - " & dtExcel.Rows(i).Item("Quantity").ToString()
                                    Exit Sub
                                Else
                                    dRow("Quantity") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Quantity"))
                                End If
                            End If
                        End If


                        'If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
                        '    dRow("DateOfCommission") = ""
                        'Else
                        '    'sDateofPur = dtExcel.Rows(i).Item("DateOfCommission")
                        '    'If Datecheck(sDateofPur) = False Then
                        '    '    lblError.Text = "Enter Valid Date"
                        '    '    Exit Sub
                        '    'Else
                        '    Dim ddate As Date = dtExcel.Rows(i).Item("DateOfCommission")
                        '    dRow("DateOfCommission") = Date.ParseExact(ddate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                        '    'End If
                        'End If

                        'If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
                        '    dRow("DateOfCommission") = ""
                        'Else
                        '    Dim isValidDate As Boolean = IsDate(dtExcel.Rows(i).Item("DateOfCommission"))
                        '    ' Dim sss As String = dtExcel.Rows(i).Item("DateOfCommission")
                        '    ' Dim sdate As DateTime = DateTime.Parse(sss)
                        '    '     Dim srr As String = sdate.ToString("dd/MM/yyyy")
                        '    ' sDateofPur = sdate
                        '    'If Datecheck(sDateofPur) = False Then
                        '    '    lblError.Text = "Enter Valid Date"
                        '    '    Exit Sub
                        '    'Else
                        '    Try
                        '        If isValidDate = True Then
                        '            dRow("DateOfCommission") = dtExcel.Rows(i).Item("DateOfCommission")
                        '        End If
                        '    Catch ex As Exception
                        '        lblError.Text = "Enter Valid Date"
                        '        Exit Sub
                        '    End Try
                        'End If

                        'If IsDBNull(dt.Rows(i)("Acc_PM_ChequeDate")) = False Then  ' Modified by darshan on 02/06/2022
                        '    dRow("Acc_PM_ChequeDate") = FASGeneral.FormatDtForRDBMS(dt.Rows(i)("Acc_PM_ChequeDate"), "D")
                        'Else
                        '    dRow("Acc_PM_ChequeDate") = "01/01/1900"
                        '    'FASGeneral.FormatDtForRDBMS(dt.Rows(i)("Acc_RM_ChequeDate"), "D")
                        'End If

                        If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
                            lblError.Text = "Date of Put to Use Can not be blank - Line No" & i + 1
                            Exit Sub
                        Else
                            sDateofPur = dtExcel.Rows(i).Item("DateOfCommission")
                            If Datecheck(sDateofPur) = False Then
                                lblError.Text = "Enter Valid Date"
                                Exit Sub
                            Else
                                Try
                                    '' Dim ddate As Date = dtExcel.Rows(i).Item("DateOfCommission")
                                    'dRow("DateOfCommission") = Date.ParseExact(ddate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                                    Dim ddate As Date = dtExcel.Rows(i).Item("DateOfCommission")
                                    dRow("DateOfCommission") = ddate.ToShortDateString()
                                Catch ex As Exception
                                    lblError.Text = "Invalid Date of Put to Use - Line No" & i + 1
                                    Exit Sub
                                End Try
                            End If
                        End If



                        'If IsDBNull(dtExcel.Rows(i).Item("DateOfCommission")) = False Then
                        '    If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
                        '        dRow("DateOfCommission") = ""
                        '    Else
                        '        ' sDateofPur = dtExcel.Rows(i).Item("DateOfCommission")
                        '        'sDateofPur = objClsFASGnrl.FormatDtForRDBMS(dtExcel.Rows(i).Item("DateOfCommission"), "D")
                        '        Dim sdate As DateTime = DateTime.ParseExact(dtExcel.Rows(i).Item("DateOfCommission"), "dd/MM/yyyy", Nothing)
                        '        dRow("DateOfCommission") = sdate.ToString("dd/MM/yyyy")
                        '        'Date.ParseExact(dtStock.Rows(i).Item("TxnDate"), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '        'If Datecheck(sDateofPur) = False Then
                        '        '    lblError.Text = "Enter Valid Date Of put to use"
                        '        '    Exit Sub
                        '        'Else

                        '        'End If
                        '    End If
                        'End If

                        'If Trim(dtExcel.Rows(i).Item("DateOfCommission").ToString()) = "" Then
                        '    dRow("DateOfCommission") = ""
                        'Else
                        '    dRow("DateOfCommission") = dtExcel.Rows(i).Item("DateOfCommission")
                        'End If

                        ' If Trim(dtExcel.Rows(i).Item("DateofPurchase").ToString()) = "" Then
                        '                        dRow("DateofPurchase") = ""
                        '                    Else
                        '                        sDateofPur = dtExcel.Rows(i).Item("DateofPurchase")
                        '                        If Datecheck(sDateofPur) = False Then
                        '                            lblError.Text = "Enter Valid Date"
                        '                            Exit Sub
                        '                        Else
                        '                            dRow("DateofPurchase") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("DateofPurchase"))
                        '                        End If
                        '                    End If



                        If IsDBNull(dtExcel.Rows(i).Item("UnitsofMeasurement")) = False Then
                            If dtExcel.Rows(i).Item("UnitsofMeasurement").ToString <> "&nbsp;" Then
                                dRow("UnitsofMeasurement") = dtExcel.Rows(i).Item("UnitsofMeasurement")
                            Else
                                dRow("UnitsofMeasurement") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item("AssetAge")) = False Then
                            If dtExcel.Rows(i).Item("AssetAge").ToString() = "" Then
                                dRow("AssetAge") = "0.00"
                            Else
                                sAmt = dtExcel.Rows(i).Item("AssetAge")
                                If Amountcheck(sAmt) = False Then
                                    lblError.Text = "Enter Valid AssetAge - " & dtExcel.Rows(i).Item("AssetAge")
                                    Exit Sub
                                Else
                                    dRow("AssetAge") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetAge"))
                                End If
                            End If
                        End If
                        'If dtExcel.Rows(i).Item("Amount").ToString() = "" Then
                        '    lblError.Text = "Amount  Can not be blank"
                        '    Exit Sub
                        'Else
                        '    sAmt = dtExcel.Rows(i).Item("Amount")
                        '    If Amountcheck(sAmt) = False Then
                        '        lblError.Text = "Enter Valid Amount"
                        '        Exit Sub
                        '    Else
                        '        dRow("Amount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Amount"))
                        '    End If
                        'End If

                        dt.Rows.Add(dRow)
                    Next
                    If IsNothing(dt) = True Then
                        Exit Sub
                    End If
                    GvOPExcel.DataSource = dt
                    GvOPExcel.DataBind()
                    dttable = dt.Copy
                Else
                    lblError.Text = "No Data"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function Datecheck(ByVal sDateofPur As String) As Boolean

        Try
            Dim pattern As String = "^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
            Dim DateMatch As Match = Regex.Match(sDateofPur, pattern)
            If DateMatch.Success Then
                Datecheck = True
            Else
                Datecheck = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Datecheck" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function Stringcheck(ByVal sStringm As String) As Boolean

        Try
            Dim pattern As String = "^[a-zA-Z]+(\s[a-zA-Z]+)?$"
            Dim StringMatch As Match = Regex.Match(sStringm, pattern)
            If StringMatch.Success Then
                Stringcheck = True
            Else
                Stringcheck = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Stringcheck" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function CurrencyCheck1(ByVal sStringm As String) As Boolean

        Try
            Dim pattern As String = "^[a-zA-Z]{0,100}?$"
            Dim CurrencyMatch As Match = Regex.Match(sStringm, pattern)
            If CurrencyMatch.Success Then
                CurrencyCheck1 = True
            Else
                CurrencyCheck1 = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CurrencyCheck1" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function CurrencyCode(ByVal sCode As String) As Boolean

        Try
            Dim pattern As String = "^[a-zA-Z]*$"
            Dim CCodeMatch As Match = Regex.Match(sCode, pattern)
            If CCodeMatch.Success Then
                CurrencyCode = True
            Else
                CurrencyCode = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CurrencyCode" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function SupplierNamecheck(ByVal sStringm As String) As Boolean

        Try
            Dim pattern As String = "^[0-9]*$"
            Dim StringMatch As Match = Regex.Match(sStringm, pattern)
            If StringMatch.Success Then
                SupplierNamecheck = True
            Else
                SupplierNamecheck = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SupplierNamecheck" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Private Function StringcheckArea(ByVal sStringArea As String) As Boolean

        Try
            Dim pattern As String = "^[(a-zA-Z)\s(a-zA-Z)/(a-zA-Z)]*$"
            Dim StringMatchArea As Match = Regex.Match(sStringArea, pattern)
            If StringMatchArea.Success Then
                StringcheckArea = True
            Else
                StringcheckArea = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "StringcheckArea" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function Amountcheck(ByVal sAmt As String) As Boolean
        Dim pattern As String = "^[0-9]\d*(\.\d+)?$"
        Dim AmountMatch As Match = Regex.Match(sAmt, pattern)
        If AmountMatch.Success Then
            Amountcheck = True
        Else
            Amountcheck = False
        End If
    End Function
    Private Function SupplierCodeCheck(ByVal sCode As String) As Boolean

        Try
            Dim pattern As String = "^[(0-9)]?$"
            Dim CodeMatch As Match = Regex.Match(sCode, pattern)
            If CodeMatch.Success Then
                SupplierCodeCheck = True
            Else
                SupplierCodeCheck = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SupplierCodeCheck" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    'Private Function LoadExcel(ByVal sFile As String) As DataTable
    '    Dim dbhelper As New DBHelper
    '    Dim dt As New DataTable
    '    Dim val As Integer = 0
    '    Try
    '        dt.Columns.Add("Slno")
    '        dt.Columns.Add("AssetTransfer")
    '        dt.Columns.Add("CurrencyTypes")
    '        dt.Columns.Add("currencyAmount")
    '        dt.Columns.Add("ActualLocation")
    '        dt.Columns.Add("AssetAge")
    '        dt.Columns.Add("TransactionType")
    '        dt.Columns.Add("SupplierName")
    '        dt.Columns.Add("supplierCode")
    '        dt.Columns.Add("AssetType")
    '        dt.Columns.Add("AssetRefNo")
    '        dt.Columns.Add("Description")
    '        dt.Columns.Add("ItemCode")
    '        dt.Columns.Add("ItemDescription")
    '        dt.Columns.Add("Quantity")
    '        dt.Columns.Add("DateofPurchase")
    '        dt.Columns.Add("DateOfCommission")
    '        dt.Columns.Add("Amount")
    '        dt.Columns.Add("Depreciation")
    '        dtExcel = dbhelper.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
    '        If IsNothing(dtExcel) = True Then
    '            Return dtExcel
    '        End If

    '        For i = 0 To dtExcel.Rows.Count - 1
    '            Dim dRow As DataRow
    '            dRow = dt.NewRow
    '            If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
    '                If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                    If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
    '                        If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
    '                            dRow("Slno") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(0))
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(1)) = False Then
    '                        If dtExcel.Rows(i).Item(1).ToString <> "&nbsp;" Then
    '                            dRow("AssetTransfer") = UCase(objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(1)))
    '                        Else
    '                            dRow("AssetTransfer") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(2)) = False Then
    '                        If dtExcel.Rows(i).Item(2).ToString <> "&nbsp;" Then
    '                            dRow("CurrencyTypes") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(2))
    '                        Else
    '                            dRow("CurrencyTypes") = ""
    '                        End If
    '                    End If

    '                    If String.IsNullOrEmpty(dtExcel.Rows(i).Item(3).ToString) = False Then
    '                        dRow("currencyAmount") = Convert.ToDecimal(dtExcel.Rows(i).Item(3).ToString()).ToString("#,##0.00")
    '                    Else
    '                        dRow("currencyAmount") = ""
    '                    End If

    '                    If IsDBNull(dtExcel.Rows(i).Item(4)) = False Then
    '                        If dtExcel.Rows(i).Item(4).ToString <> "&nbsp;" Then
    '                            dRow("ActualLocation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(4))
    '                        Else
    '                            dRow("ActualLocation") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(5)) = False Then
    '                        If dtExcel.Rows(i).Item(5).ToString <> "&nbsp;" Then
    '                            dRow("AssetAge") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(5))
    '                        Else
    '                            dRow("AssetAge") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(6)) = False Then
    '                        If dtExcel.Rows(i).Item(6).ToString <> "&nbsp;" Then
    '                            dRow("TransactionType") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(6))
    '                        Else
    '                            dRow("TransactionType") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(7)) = False Then
    '                        If dtExcel.Rows(i).Item(7).ToString <> "&nbsp;" Then
    '                            dRow("SupplierName") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(7))
    '                        Else
    '                            dRow("SupplierName") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(8)) = False Then
    '                        If dtExcel.Rows(i).Item(8).ToString <> "&nbsp;" Then
    '                            dRow("supplierCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(8))
    '                        Else
    '                            dRow("supplierCode") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(9)) = False Then
    '                        If dtExcel.Rows(i).Item(9).ToString <> "&nbsp;" Then
    '                            dRow("AssetType") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(9))
    '                        Else
    '                            dRow("AssetType") = ""
    '                        End If
    '                    End If

    '                    If IsDBNull(dtExcel.Rows(i).Item(10)) = False Then
    '                        If dtExcel.Rows(i).Item(10).ToString <> "&nbsp;" Then
    '                            dRow("AssetRefNo") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(10))
    '                        Else
    '                            dRow("AssetRefNo") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(11)) = False Then
    '                        If dtExcel.Rows(i).Item(11).ToString <> "&nbsp;" Then
    '                            dRow("Description") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(11))
    '                        Else
    '                            dRow("Description") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(12)) = False Then
    '                        If dtExcel.Rows(i).Item(12).ToString <> "&nbsp;" Then
    '                            dRow("ItemCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(12))
    '                        Else
    '                            dRow("ItemCode") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(13)) = False Then
    '                        If dtExcel.Rows(i).Item(13).ToString <> "&nbsp;" Then
    '                            dRow("ItemDescription") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(13))
    '                        Else
    '                            dRow("ItemDescription") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(14)) = False Then
    '                        If dtExcel.Rows(i).Item(14).ToString <> "&nbsp;" Then
    '                            dRow("Quantity") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(14))
    '                        Else
    '                            dRow("Quantity") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(15)) = False Then
    '                        If dtExcel.Rows(i).Item(15).ToString <> "&nbsp;" Then
    '                            dRow("DateofPurchase") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(15))
    '                        Else
    '                            dRow("DateofPurchase") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(16)) = False Then
    '                        If dtExcel.Rows(i).Item(16).ToString <> "&nbsp;" Then
    '                            dRow("DateOfCommission") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(16))
    '                        Else
    '                            dRow("DateOfCommission") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(17)) = False Then
    '                        If dtExcel.Rows(i).Item(17).ToString <> "&nbsp;" Then
    '                            dRow("Amount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(17))
    '                        Else
    '                            dRow("Amount") = ""
    '                        End If
    '                    End If
    '                    If IsDBNull(dtExcel.Rows(i).Item(18)) = False Then
    '                        If dtExcel.Rows(i).Item(18).ToString <> "&nbsp;" Then
    '                            dRow("Depreciation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(18))
    '                        Else
    '                            dRow("Depreciation") = "0.00"
    '                        End If
    '                    End If
    '                End If
    '                End If
    '            dt.Rows.Add(dRow)
    '        Next
    '        Return dt
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function

    Private Function LoadExcel(ByVal sFile As String) As DataTable
        Dim dbhelper As New DBHelper
        Dim dt As New DataTable
        Dim val As Integer = 0
        Try
            dt.Columns.Add("Slno")
            dt.Columns.Add("Location")
            dt.Columns.Add("Division")
            dt.Columns.Add("Department")
            dt.Columns.Add("Bay")
            dt.Columns.Add("AssetClass")
            dt.Columns.Add("AssetCode")
            dt.Columns.Add("AssetDescription")
            dt.Columns.Add("Quantity")
            dt.Columns.Add("DateOfCommission")
            dt.Columns.Add("UnitsofMeasurement")
            dt.Columns.Add("AssetAge")


            dtExcel = dbhelper.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtExcel) = True Then
                Return dtExcel
            End If

            For i = 0 To dtExcel.Rows.Count - 1
                Dim dRow As DataRow
                dRow = dt.NewRow
                'If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                'If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                    If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        dRow("Slno") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(0))
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(1)) = False Then
                    If dtExcel.Rows(i).Item(1).ToString <> "&nbsp;" Then
                        dRow("Location") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(1))
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(2)) = False Then
                    If dtExcel.Rows(i).Item(2).ToString <> "&nbsp;" Then
                        dRow("Division") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(2))
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(3)) = False Then
                    If dtExcel.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        dRow("Department") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(3))
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(4)) = False Then
                    If dtExcel.Rows(i).Item(4).ToString <> "&nbsp;" Then
                        dRow("Bay") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(4))
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(5)) = False Then
                    If dtExcel.Rows(i).Item(5).ToString <> "&nbsp;" Then
                        dRow("AssetClass") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(5))
                    Else
                        dRow("AssetClass") = ""
                    End If
                End If

                If IsDBNull(dtExcel.Rows(i).Item(6)) = False Then
                    If dtExcel.Rows(i).Item(6).ToString <> "&nbsp;" Then
                        dRow("AssetCode") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(6))
                    Else
                        dRow("AssetCode") = ""
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(7)) = False Then
                    If dtExcel.Rows(i).Item(7).ToString <> "&nbsp;" Then
                        dRow("AssetDescription") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(7))
                    Else
                        dRow("AssetDescription") = ""
                    End If
                End If
                If IsDBNull(dtExcel.Rows(i).Item(8)) = False Then
                    If dtExcel.Rows(i).Item(8).ToString <> "&nbsp;" Then
                        dRow("Quantity") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(8))
                    Else
                        dRow("Quantity") = ""
                    End If
                End If

                If IsDBNull(dtExcel.Rows(i).Item(9)) = False Then
                    If dtExcel.Rows(i).Item(9).ToString <> "&nbsp;" Then
                        dRow("DateOfCommission") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(9))
                    Else
                        dRow("DateOfCommission") = ""
                    End If
                End If

                If IsDBNull(dtExcel.Rows(i).Item(10)) = False Then
                    If dtExcel.Rows(i).Item(10).ToString <> "&nbsp;" Then
                        dRow("UnitsofMeasurement") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(10))
                    Else
                        dRow("UnitsofMeasurement") = ""
                    End If
                End If

                If IsDBNull(dtExcel.Rows(i).Item(11)) = False Then
                    If dtExcel.Rows(i).Item(11).ToString <> "&nbsp;" Then
                        dRow("AssetAge") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(11))
                    Else
                        dRow("AssetAge") = ""
                    End If
                End If


                'End If
                'End If
                dt.Rows.Add(dRow)
            Next
            Dim filteredRows As DataTable = dt.Rows.Cast(Of DataRow)().Where(Function(row) row.ItemArray.Any(Function(field) Not (TypeOf field Is System.DBNull))).CopyToDataTable()
            Return filteredRows
            'Return dt
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExcel" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Private Sub GvOPExcel_PreRender(sender As Object, e As EventArgs) Handles GvOPExcel.PreRender
        Try
            If GvOPExcel.Rows.Count > 0 Then
                GvOPExcel.UseAccessibleHeader = True
                GvOPExcel.HeaderRow.TableSection = TableRowSection.TableHeader
                GvOPExcel.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvOPExcel_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    ' Private Sub imgbtUpload_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtUpload.Click
    'Dim Arr As Array
    'Dim lblSrNo, lblAssetTransfer, lblCurrencyTypes, lblcurrencyAmount As New Label
    'Dim lblActualLocation, lblAssetAge, lblTransactionType, lblSupplierName, lblsupplierCode, lblAssetType, lblAssetRefNo, lblDescription As New Label
    'Dim lblItemCode, lblItemDescription, lblQuantity, lblDateofPurchase, lblDateOfCommission, lblAmount, lblDeprcn As New Label
    'Dim iSupplierID As Integer
    'Dim iAssetType As Integer
    'Dim AssetTyp, RefNo As New DataTable
    'Dim iCount As Integer
    'Dim AssetLen As String
    'Dim ilen As Integer : Dim increment As Integer = 0
    'Dim bCheck As Boolean
    'Dim dt As New DataTable
    'Try
    '    lblError.Text = ""
    '    If GvOPExcel.Rows.Count > 0 Then
    '        For i = 0 To dttable.Rows.Count - 1
    '            objOPExcel.iAFAA_ID = 0
    '            lblAssetTransfer = GvOPExcel.Rows(i).FindControl("lblAssetTransfer")

    '            If lblAssetTransfer.Text = "LOCAL" Then
    '                If lblAssetTransfer.Text <> "" Then
    '                    objOPExcel.iAFAA_AssetTrType = 1
    '                Else
    '                    objOPExcel.iAFAA_AssetTrType = 0
    '                End If
    '            ElseIf lblAssetTransfer.Text = "IMPORTED" Then
    '                If lblAssetTransfer.Text <> "" Then
    '                    objOPExcel.iAFAA_AssetTrType = 2
    '                End If
    '            Else
    '                objOPExcel.iAFAA_AssetTrType = 0
    '            End If
    '            lblCurrencyTypes = GvOPExcel.Rows(i).FindControl("lblCurrencyTypes")

    '            Dim iCurnyID As Integer = objOPExcel.LoadCurrencyID(sSession.AccessCode, sSession.AccessCodeID, lblCurrencyTypes.Text)
    '            If iCurnyID > 0 Then
    '                objOPExcel.iAFAA_CurrencyType = iCurnyID
    '            Else
    '                objOPExcel.iAFAA_CurrencyType = 0
    '            End If
    '            lblcurrencyAmount = GvOPExcel.Rows(i).FindControl("lblcurrencyAmount")
    '            If lblcurrencyAmount.Text <> "" Then
    '                objOPExcel.dAFAA_CurrencyAmnt = lblcurrencyAmount.Text
    '            Else
    '                objOPExcel.dAFAA_CurrencyAmnt = "0.00"
    '            End If
    '            If ddlAccZone.SelectedIndex > 0 Then
    '                objOPExcel.iAFAA_Zone = ddlAccZone.SelectedValue
    '            Else
    '                objOPExcel.iAFAA_Zone = 0
    '            End If
    '            If ddlAccRgn.SelectedIndex > 0 Then
    '                objOPExcel.iAFAA_Region = ddlAccRgn.SelectedValue
    '            Else
    '                objOPExcel.iAFAA_Region = 0
    '            End If
    '            If ddlAccArea.SelectedIndex > 0 Then
    '                objOPExcel.iAFAA_Area = ddlAccArea.SelectedValue
    '            Else
    '                objOPExcel.iAFAA_Area = 0
    '            End If
    '            If ddlAccBrnch.SelectedIndex > 0 Then
    '                objOPExcel.iAFAA_Branch = ddlAccBrnch.SelectedValue
    '            Else
    '                objOPExcel.iAFAA_Branch = 0
    '            End If
    '            lblActualLocation = GvOPExcel.Rows(i).FindControl("lblActualLocation")
    '            If lblActualLocation.Text <> "" Then
    '                objOPExcel.sAFAA_ActualLocn = lblActualLocation.Text
    '            Else
    '                objOPExcel.sAFAA_ActualLocn = ""
    '            End If
    '            lblAssetAge = GvOPExcel.Rows(i).FindControl("lblAssetAge")
    '            If lblAssetAge.Text <> "" Then
    '                objOPExcel.dAFAA_AssetAge = lblAssetAge.Text
    '            Else
    '                objOPExcel.dAFAA_AssetAge = "0.00"
    '            End If
    '            lblTransactionType = GvOPExcel.Rows(i).FindControl("lblTransactionType")
    '            If lblTransactionType.Text = "ADDITION" Then
    '                objOPExcel.iAFAA_TrType = 1
    '            ElseIf lblTransactionType.Text = "TRANSFERS" Then
    '                objOPExcel.iAFAA_TrType = 2
    '            ElseIf lblTransactionType.Text = "REVALUATION" Then
    '                objOPExcel.iAFAA_TrType = 3
    '            ElseIf lblTransactionType.Text = "FOREIGN EXCHANGE" Then
    '                objOPExcel.iAFAA_TrType = 4
    '            Else
    '                objOPExcel.iAFAA_TrType = 0
    '            End If
    '            lblSupplierName = GvOPExcel.Rows(i).FindControl("lblSupplierName")
    '            iSupplierID = objOPExcel.GetSupplierID(sSession.AccessCode, sSession.AccessCodeID, lblSupplierName.Text)
    '            If iSupplierID > 0 Then
    '                objOPExcel.iAFAA_SupplierName = iSupplierID
    '            Else
    '                objOPExcel.iAFAA_SupplierName = 0
    '            End If
    '            lblsupplierCode = GvOPExcel.Rows(i).FindControl("lblsupplierCode")
    '            If lblsupplierCode.Text <> "" Then
    '                objOPExcel.iAFAA_SupplierCode = lblsupplierCode.Text
    '            Else
    '                objOPExcel.iAFAA_SupplierCode = 0
    '            End If

    '            lblAssetType = GvOPExcel.Rows(i).FindControl("lblAssetType")
    '            iAssetType = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)

    '            If iAssetType > 0 Then
    '                objOPExcel.sAFAA_AssetType = iAssetType
    '            Else
    '                objOPExcel.sAFAA_AssetType = ""
    '            End If

    '            iCount = objOPExcel.GetGLID(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)
    '            If iCount > 0 Then
    '                AssetLen = objOPExcel.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)
    '                objOPExcel.sAFAA_AssetNo = AssetLen & iCount.ToString()
    '            Else
    '                AssetLen = objOPExcel.LoadAssetNo(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text)
    '                ilen = AssetLen.Length
    '                If ilen = 9 Then
    '                    increment = increment + 1
    '                    objOPExcel.sAFAA_AssetNo = AssetLen & increment.ToString()
    '                End If
    '            End If
    '            lblAssetRefNo = GvOPExcel.Rows(i).FindControl("lblAssetRefNo")
    '            If lblAssetRefNo.Text <> "" Then
    '                objOPExcel.sAFAA_AssetRefNo = lblAssetRefNo.Text
    '            Else
    '                objOPExcel.sAFAA_AssetRefNo = ""
    '            End If
    '            objOPExcel.sAFAA_DelnType = ""

    '            bCheck = objOPExcel.CheckExistorNot(sSession.AccessCode, sSession.AccessCodeID, iAssetType, lblAssetRefNo.Text)
    '            If bCheck = True Then
    '                objOPExcel.sAFAA_AddnType = "O"
    '            Else
    '                objOPExcel.sAFAA_AddnType = "N"
    '            End If

    '            lblDescription = GvOPExcel.Rows(i).FindControl("lblDescription")
    '            If lblDescription.Text <> "" Then
    '                objOPExcel.sAFAA_Description = lblDescription.Text
    '            Else
    '                objOPExcel.sAFAA_Description = ""
    '            End If
    '            lblItemCode = GvOPExcel.Rows(i).FindControl("lblItemCode")
    '            If lblItemCode.Text <> "" Then
    '                objOPExcel.sAFAA_ItemCode = lblItemCode.Text
    '            Else
    '                objOPExcel.sAFAA_ItemCode = ""
    '            End If
    '            lblItemDescription = GvOPExcel.Rows(i).FindControl("lblItemDescription")
    '            If lblItemDescription.Text <> "" Then
    '                objOPExcel.sAFAA_ItemDescription = lblItemDescription.Text
    '            Else
    '                objOPExcel.sAFAA_ItemDescription = ""
    '            End If
    '            lblQuantity = GvOPExcel.Rows(i).FindControl("lblQuantity")
    '            If lblQuantity.Text <> "" Then
    '                objOPExcel.iAFAA_Quantity = lblQuantity.Text
    '            Else
    '                objOPExcel.iAFAA_Quantity = 0
    '            End If
    '            lblDateofPurchase = GvOPExcel.Rows(i).FindControl("lblDateofPurchase")
    '            If lblDateofPurchase.Text <> "" Then
    '                objOPExcel.dAFAA_PurchaseDate = lblDateofPurchase.Text
    '            Else
    '                objOPExcel.dAFAA_PurchaseDate = "01/01/1991"
    '            End If
    '            lblDateOfCommission = GvOPExcel.Rows(i).FindControl("lblDateOfCommission")
    '            If lblDateOfCommission.Text <> "" Then
    '                objOPExcel.dAFAA_CommissionDate = lblDateOfCommission.Text
    '            Else
    '                objOPExcel.dAFAA_CommissionDate = "01/01/1991"
    '            End If
    '            lblAmount = GvOPExcel.Rows(i).FindControl("lblAmount")
    '            If lblAmount.Text <> "" Then
    '                objOPExcel.dAFAA_AssetAmount = lblAmount.Text
    '            Else
    '                objOPExcel.dAFAA_AssetAmount = "0.00"
    '            End If
    '            lblDeprcn = GvOPExcel.Rows(i).FindControl("lblDeprcn")
    '            If lblDeprcn.Text <> "" Then
    '                objOPExcel.dAFAA_Depreciation = lblDeprcn.Text
    '            Else
    '                objOPExcel.dAFAA_Depreciation = "0.00"
    '            End If
    '            objOPExcel.iAFAA_AssetDelID = 0
    '            objOPExcel.dAFAA_AssetDelDate = Nothing
    '            objOPExcel.dAFAA_AssetDeletionDate = Nothing
    '            objOPExcel.dAFAA_AddtnDate = Nothing

    '            objOPExcel.dAFAA_Assetvalue = "0.00"
    '            objOPExcel.sAFAA_AssetDesc = ""
    '            objOPExcel.iAFAA_CreatedBy = sSession.UserID
    '            objOPExcel.dAFAA_CreatedOn = DateTime.Today
    '            objOPExcel.iAFAA_UpdatedBy = sSession.UserID
    '            objOPExcel.dAFAA_UpdatedOn = DateTime.Today
    '            objOPExcel.sAFAA_Delflag = "X"
    '            objOPExcel.sAFAA_Status = "W"
    '            objOPExcel.sAFAA_Operation = "C"
    '            objOPExcel.sAFAA_IPAddress = sSession.IPAddress
    '            objOPExcel.iAFAA_YearID = ddlFinancialYear.SelectedValue 
    '            objOPExcel.iAFAA_CompID = sSession.AccessCodeID

    '            objOPExcel.AFAA_AssetCode = objFxdAsst.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue )
    '            objOPExcel.AFAA_PurchaseAmount = "0.00"
    '            objOPExcel.AFAA_PolicyNo = ""
    '            objOPExcel.AFAA_Amount = "0.00"
    '            objOPExcel.AFAA_Date = "01/01/1900"
    '            objOPExcel.AFAA_Department = 0
    '            objOPExcel.AFAA_Employee = 0

    '            objOPExcel.AFAA_ContactPerson = ""
    '            objOPExcel.AFAA_Address = ""
    '            objOPExcel.AFAA_Phone = ""
    '            objOPExcel.AFAA_Fax = ""
    '            objOPExcel.AFAA_EmailID = ""
    '            objOPExcel.AFAA_Website = ""

    '            objOPExcel.AFAA_BrokerName = ""
    '            objOPExcel.AFAA_CompanyName = ""

    '            objOPExcel.AFAA_WrntyDesc = ""
    '            objOPExcel.AFAA_ContactPrsn = ""
    '            objOPExcel.AFAA_AMCFrmDate = "01/01/1900"
    '            objOPExcel.AFAA_AMCTo = "01/01/1900"
    '            objOPExcel.AFAA_Contprsn = ""
    '            objOPExcel.AFAA_PhoneNo = ""
    '            objOPExcel.AFAA_AMCCompanyName = ""
    '            objOPExcel.AFAA_ToDate = "01/01/1900"

    '            objOPExcel.AFAA_AssetDeletion = 0
    '            objOPExcel.AFAA_Remark = ""

    '            objOPExcel.AFAA_EMPCode = ""
    '            objOPExcel.AFAA_LToWhom = ""
    '            objOPExcel.AFAA_LAmount = "0.00"
    '            objOPExcel.AFAA_LAggriNo = ""
    '            objOPExcel.AFAA_LDate = "01/01/1900"
    '            objOPExcel.AFAA_LCurrencyType = 0
    '            objOPExcel.AFAA_LExchDate = "01/01/1900"

    '            Arr = objOPExcel.SaveFixedAssetMaster(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
    '            Arr = objOPExcel.SaveFixedAssetAddition(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
    '        Next
    '    Else
    '        lblError.Text = "No Data"
    '        Exit Sub
    '    End If
    '    If Arr(0) = "2" Then
    '        lblFXOPBalExcelMsg.Text = "Successfully Updated"
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDOpExcel').modal('show');", True)
    '    ElseIf Arr(0) = "3" Then
    '        lblFXOPBalExcelMsg.Text = "Successfully Saved"
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDOpExcel').modal('show');", True)
    '    End If
    '    imgbtnRefresh_Click(sender, e)
    'Catch ex As Exception
    '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtUpload_Click")
    'End Try
    ' End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            FULoad.Dispose() : txtPath.Text = "" : ddlSheetName.SelectedIndex = 0 : GvOPExcel.DataSource = Nothing : GvOPExcel.DataBind()
            'ddlAccZone.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccArea.SelectedIndex = 0 : ddlAccBrnch.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ImgBtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objClsFASGnrl.EncryptQueryString(Val(sIKBBackStatus)))
            Response.Redirect(String.Format("~/FixedAsset/AssetRegister.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ImgbtnUpload_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnUpload.Click
        Dim Arr As Array
        Dim lblSrNo As New Label
        Dim lblAssetAge, lblAssetType, lblDescription, lblAssetCode As New Label
        Dim lblItemCode, lblItemDescription, lblQuantity, lblDateofPurchase, lblUnitsofMeasurement As New Label, lblDateOfCommission As New Label
        Dim lblLocation, lblDivision, lbldepartment, lblbay As New Label
        Dim iAssetType As Integer
        Dim AssetTyp, RefNo As New DataTable
        Dim bCheck As Boolean
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If GvOPExcel.Rows.Count > 0 Then
                For i = 0 To dttable.Rows.Count - 1
                    objOPExcel.iAFAA_ID = 0

                    lblAssetType = GvOPExcel.Rows(i).FindControl("lblAssetType")
                    iAssetType = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, lblAssetType.Text, ddlCustomerName.SelectedValue)

                    If iAssetType > 0 Then
                        objOPExcel.sAFAA_AssetType = iAssetType
                    Else
                        objOPExcel.sAFAA_AssetType = ""
                    End If

                    objOPExcel.AFAA_AssetCode = objOPExcel.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    'lblAssetCode = GvOPExcel.Rows(i).FindControl("lblAssetCode")
                    'If lblAssetCode.Text <> "" Then
                    '    objOPExcel.AFAA_AssetCode = lblAssetCode.Text
                    'Else
                    '    objOPExcel.AFAA_AssetCode = ""
                    'End If

                    'objOPExcel.sAFAA_Description = ""

                    lblItemCode = GvOPExcel.Rows(i).FindControl("lblItemCode")

                    'bCheck = objFxdAsst.TocheckExistitemcode(sSession.AccessCode, sSession.AccessCodeID, iAssetType, lblItemCode.Text, ddlFinancialYear.SelectedValue )
                    'If bCheck = True Then
                    '    lblError.Text = "" & lblItemCode.Text & " is Already Exist, Please Enter Diffrent Asset Code"
                    '    Exit Sub
                    'End If

                    If lblItemCode.Text <> "" Then
                        objOPExcel.sAFAA_ItemCode = lblItemCode.Text
                    Else
                        objOPExcel.sAFAA_ItemCode = ""
                    End If

                    lblItemDescription = GvOPExcel.Rows(i).FindControl("lblItemDescription")
                    If lblItemDescription.Text <> "" Then
                        objOPExcel.sAFAA_ItemDescription = lblItemDescription.Text
                    Else
                        objOPExcel.sAFAA_ItemDescription = ""
                    End If


                    lblQuantity = GvOPExcel.Rows(i).FindControl("lblQuantity")
                    If lblQuantity.Text <> "" Then
                        objOPExcel.iAFAA_Quantity = lblQuantity.Text
                    Else
                        objOPExcel.iAFAA_Quantity = 0
                    End If

                    lblUnitsofMeasurement = GvOPExcel.Rows(i).FindControl("lblUnitsofMeasurement")
                    lblUnitsofMeasurement.Text = objOPExcel.LoadUnitsOfMeasurement(sSession.AccessCode, sSession.AccessCodeID, lblUnitsofMeasurement.Text)
                    If lblUnitsofMeasurement.Text <> "" Then
                        objOPExcel.iAFAM_Unit = lblUnitsofMeasurement.Text
                    Else
                        objOPExcel.iAFAM_Unit = 0
                    End If


                    lblLocation = GvOPExcel.Rows(i).FindControl("lblLocation")
                    lblLocation.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblLocation.Text, ddlCustomerName.SelectedValue)
                    If lblLocation.Text <> "" Then
                        objOPExcel.iAFAA_Location = lblLocation.Text
                    Else
                        objOPExcel.iAFAA_Location = 0
                    End If

                    lblDivision = GvOPExcel.Rows(i).FindControl("lblDivision")
                    lblDivision.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblDivision.Text, ddlCustomerName.SelectedValue)
                    If lblDivision.Text <> "" Then
                        objOPExcel.iAFAA_Division = lblDivision.Text
                    Else
                        objOPExcel.iAFAA_Division = 0
                    End If

                    lbldepartment = GvOPExcel.Rows(i).FindControl("lblDepartment")
                    lbldepartment.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lbldepartment.Text, ddlCustomerName.SelectedValue)
                    If lblDivision.Text <> "" Then
                        objOPExcel.iAFAA_Department = lbldepartment.Text
                    Else
                        objOPExcel.iAFAA_Department = 0
                    End If

                    lblbay = GvOPExcel.Rows(i).FindControl("lblBay")
                    lblbay.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblbay.Text, ddlCustomerName.SelectedValue)
                    If lblbay.Text <> "" Then
                        objOPExcel.iAFAA_Bay = lblbay.Text
                    Else
                        objOPExcel.iAFAA_Bay = 0
                    End If

                    Dim slocation, sDivision, sDeptmnt, sBay As String
                    If lblLocation.Text <> 0 Then
                        slocation = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, lblLocation.Text, ddlCustomerName.SelectedValue)
                    End If

                    If lblDivision.Text <> 0 Then
                        sDivision = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, lblDivision.Text, ddlCustomerName.SelectedValue)
                        sDivision = "/" & sDivision
                    End If
                    If lbldepartment.Text <> 0 Then
                        sDeptmnt = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, lbldepartment.Text, ddlCustomerName.SelectedValue)
                        sDeptmnt = "/" & sDeptmnt
                    End If

                    If lblbay.Text <> 0 Then
                        sBay = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, lblbay.Text, ddlCustomerName.SelectedValue)
                        sBay = "/" & sBay
                    End If

                    Dim sCode As String = slocation & sDivision & sDeptmnt & sBay & "/" & objOPExcel.AFAA_AssetCode
                    objOPExcel.sAFAA_Code = sCode

                    'lblDateofPurchase = GvOPExcel.Rows(i).FindControl("lblDateofPurchase")
                    'If lblDateofPurchase.Text <> "" Then
                    '    objOPExcel.dAFAA_CommissionDate = Date.ParseExact(lblDateofPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'Else
                    '    objOPExcel.dAFAA_CommissionDate = "01/01/1900"
                    'End If

                    'lblDateOfCommission = GvOPExcel.Rows(i).FindControl("lblDateOfCommission")
                    'If lblDateOfCommission.Text <> "" Then
                    '    objOPExcel.dAFAA_CommissionDate = lblDateOfCommission.Text
                    'Else
                    '    objOPExcel.dAFAA_CommissionDate = "01/01/1900"
                    'End If

                    'lblDateOfCommission = GvOPExcel.Rows(i).FindControl("lblDateOfCommission")
                    'If lblDateOfCommission.Text = "" Then
                    '    objOPExcel.dAFAA_CommissionDate = "01/01/1900"
                    'Else
                    '    'If Datecheck(lblDateOfCommission.Text) = False Then
                    '    '    lblError.Text = "Enter Valid Date Line no -" & i + 1
                    '    '    Exit Sub
                    '    'Else
                    '    Dim ddate As Date = lblDateOfCommission.Text
                    '    objOPExcel.dAFAA_CommissionDate = Date.ParseExact(ddate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    '    'End If
                    'End If
                    lblDateOfCommission = GvOPExcel.Rows(i).FindControl("lblDateOfCommission")
                    If lblDateOfCommission.Text <> "" Then
                        Try
                            objOPExcel.dAFAA_CommissionDate = Date.ParseExact(lblDateOfCommission.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Catch ex As Exception
                            Dim ddate As DateTime = lblDateOfCommission.Text
                            objOPExcel.dAFAA_CommissionDate = ddate.ToShortDateString()
                        End Try

                    Else
                        objOPExcel.dAFAA_CommissionDate = "01/01/1900"
                    End If


                    ' objOPExcel.dAFAA_CommissionDate = "01/01/1900"
                    objOPExcel.dAFAA_PurchaseDate = "01/01/1900"
                    ' lblDateofPurchase = GvOPExcel.Rows(i).FindControl("lblDateofPurchase")
                    'If lblDateofPurchase.Text <> "" Then
                    '    objOPExcel.dAFAA_CommissionDate = Date.ParseExact(lblDateofPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'Else
                    '    objOPExcel.dAFAA_CommissionDate = "01/01/1900"
                    'End If

                    lblAssetAge = GvOPExcel.Rows(i).FindControl("lblAssetAge")
                    If lblAssetAge.Text <> "" Then
                        objOPExcel.dAFAA_AssetAge = lblAssetAge.Text
                    Else
                        objOPExcel.dAFAA_AssetAge = "0.00"
                    End If

                    'lblAmount = GvOPExcel.Rows(i).FindControl("lblAmount")
                    'If lblAmount.Text <> "" Then
                    '    objOPExcel.dAFAA_AssetAmount = lblAmount.Text
                    'Else
                    '    objOPExcel.dAFAA_AssetAmount = "0.00"
                    'End If
                    objOPExcel.dAFAA_AssetAmount = "0.00"
                    objFxdAsst.AFAM_CreatedOn = Date.Today
                    objFxdAsst.AFAM_UpdatedBy = 0
                    objFxdAsst.AFAM_UpdatedOn = Date.Today
                    objOPExcel.iAFAA_CreatedBy = sSession.UserID
                    If lblDateOfCommission.Text <> "" Then
                        objOPExcel.sAFAA_Delflag = "A"
                        objOPExcel.sAFAA_Status = "A"
                    Else
                        objOPExcel.sAFAA_Delflag = "X"
                        objOPExcel.sAFAA_Status = "W"
                    End If

                    objOPExcel.iAFAA_YearID = ddlFinancialYear.SelectedValue
                    objOPExcel.iAFAA_CompID = sSession.AccessCodeID
                    objOPExcel.sAFAA_Operation = "C"
                    objOPExcel.sAFAA_IPAddress = sSession.IPAddress
                    objOPExcel.iAFAA_CustId = ddlCustomerName.SelectedValue
                    'objOPExcel.SaveFixedAssetMaster(sSession.AccessCode, sSession.AccessCodeID, objOPExcel.iAFAA_ID, objOPExcel.sAFAA_AssetType, objOPExcel.AFAA_AssetCode, objOPExcel.sAFAA_Description, objOPExcel.sAFAA_ItemCode, objOPExcel.sAFAA_ItemDescription, objOPExcel.iAFAA_Quantity, objOPExcel.dAFAA_PurchaseDate, objOPExcel.dAFAA_CommissionDate, objOPExcel.dAFAA_AssetAge, objOPExcel.dAFAA_AssetAmount, objOPExcel.iAFAA_CreatedBy, objOPExcel.iAFAA_UpdatedBy, objOPExcel.sAFAA_Delflag, objOPExcel.sAFAA_Status, objOPExcel.iAFAA_YearID, objOPExcel.iAFAA_CompID, objOPExcel.sAFAA_Operation, objOPExcel.sAFAA_IPAddress)
                    Arr = objOPExcel.SaveFixedAssetMaster(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
                    'Arr = objOPExcel.SaveFixedAssetAddition(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
                Next
            Else
                lblError.Text = "No Data"
                Exit Sub
            End If
            If Arr(0) = "2" Then
                lblFXOPBalExcelMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDOpExcel').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Creation", "Saved", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            ElseIf Arr(0) = "3" Then
                lblFXOPBalExcelMsg.Text = "Successfully Uploaded"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDOpExcel').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Creation", "Saved", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            End If
            imgbtnRefresh_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnUpload_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

End Class
