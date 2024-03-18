Imports System.Data
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports System.Globalization
Partial Class AssetAdditionExcelUpload
    Inherits System.Web.UI.Page

    Private Shared sFormName As String = "AssetAdditionExcelUpload"
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
    Dim objAsstTrn As New ClsAssetTransactionAddition
    Private objAsst As New ClsAssetMaster

    Private Shared FStartDate As Date
    Private Shared FEndDate As Date
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        'imgbtUpload.ImageUrl = "~/Images/Upload24.png"
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
                TransactionType()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            FStartDate = objClsFASGnrl.FormatDtForRDBMS(objClsFASGnrl.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
            FEndDate = objClsFASGnrl.FormatDtForRDBMS(objClsFASGnrl.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Public Sub TransactionType()
        Try
            ddlTransactionType.Items.Insert(0, "Select Transaction Type")
            ddlTransactionType.Items.Insert(1, "Opening Balance")
            ddlTransactionType.Items.Insert(2, "Addition")
            ddlTransactionType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "TransactionType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""
            GvAdditionExcel.DataSource = Nothing
            GvAdditionExcel.DataBind()

            If ddlTransactionType.SelectedIndex = 0 Then
                lblError.Text = "Select Transaction Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Transaction Type','', 'success');", True)
                Exit Sub
            End If

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
                    lblError.Text = "Select Excel file only." : lblFXAdditionExcelMsg.Text = "Select Excel file only."
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblFXAdditionExcelMsg.Text = "Select Excel file."
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
    Protected Sub lnDown_Click(sender As Object, e As EventArgs) Handles lnDown.Click
        Try
            If ddlTransactionType.SelectedIndex = 0 Then
                lblError.Text = "Select Transaction Type"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Transaction Type','', 'success');", True)
                Exit Sub
            End If

            If ddlTransactionType.SelectedIndex = 1 Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", "attachment; filename=AssetOPB-Upload.xlsx")
                Response.TransmitFile(Server.MapPath("~\SampleExcels\AssetOPB-Upload.xlsx"))
                Response.End()
            ElseIf ddlTransactionType.SelectedIndex = 2 Then
                Response.ContentType = "application/vnd.ms-excel"
                Response.AppendHeader("Content-Disposition", "attachment; filename=AssetAddition-Upload.xlsx")
                Response.TransmitFile(Server.MapPath("~\SampleExcels\AssetAddition-Upload.xlsx"))
                Response.End()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnDown_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlSheetName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sDateofPur As Date
        Dim sAmt As String
        Dim sString, sCode As String
        Dim bCheck As Boolean
        Dim iSupId As Integer
        Dim iAssetClass As Integer
        Try
            If ddlTransactionType.SelectedIndex = 1 Then
                dt.Columns.Add("Slno")
                dt.Columns.Add("Location")
                dt.Columns.Add("Division")
                dt.Columns.Add("Department")
                dt.Columns.Add("Bay")
                dt.Columns.Add("AssetClass")
                dt.Columns.Add("Asset")
                'dt.Columns.Add("AssetLocation")
                dt.Columns.Add("DateOfPurchase")
                dt.Columns.Add("OriginalCost")
                dt.Columns.Add("WDVOpeningValue")
                dt.Columns.Add("DepForthePeriod")

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


                            'If IsDBNull(dtExcel.Rows(i).Item("AssetClass")) = False Then
                            If dtExcel.Rows(i).Item("AssetClass").ToString() = "" Then
                                lblError.Text = "AssetType Can not be blank- Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("AssetClass")
                                iAssetClass = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                bCheck = objOPExcel.GetAssetType1(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                If bCheck = True Then
                                    'If StringcheckArea(sString) = False Then
                                    '    lblError.Text = "Enter Valid Asset Type"
                                    '    Exit Sub
                                    'Else
                                    dRow("AssetClass") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetClass"))
                                    'End If
                                Else
                                    lblError.Text = "( " & sString & ") not matched,Create in Asset Master"
                                    Exit Sub
                                End If
                            End If
                            'End If

                            Dim dt1 As New DataTable
                            'If IsDBNull(dtExcel.Rows(i).Item("Asset")) = False Then
                            If dtExcel.Rows(i).Item("Asset").ToString() = "" Then
                                lblError.Text = "Asset Can not be blank - Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("Asset")
                                dt1 = objFxdAsst.TocheckExistAssetDescription(sSession.AccessCode, sSession.AccessCodeID, iAssetClass, sString, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                                If dt1.Rows.Count > 0 Then
                                    If dt1.Rows(0)("AFAM_ItemDescription") = "" Then
                                        lblError.Text = "( " & sString & ") not matched,Create in Asset Master - Line No" & i + 1
                                        Exit Sub
                                    End If
                                    If dt1.Rows(0)("AFAM_DelFlag") = "S" Then
                                        lblError.Text = "( " & sString & ") is Already Sold"
                                        Exit Sub
                                    ElseIf dt1.Rows(0)("AFAM_DelFlag") = "T" Then
                                        lblError.Text = "( " & sString & ") is Transfered to other location"
                                        Exit Sub
                                    ElseIf dt1.Rows(0)("AFAM_DelFlag") = "St" Then
                                        lblError.Text = "( " & sString & ") is Already Stolen"
                                        Exit Sub
                                    ElseIf dt1.Rows(0)("AFAM_DelFlag") = "D" Then
                                        lblError.Text = "( " & sString & ") is Destroyed"
                                        Exit Sub
                                    ElseIf dt1.Rows(0)("AFAM_DelFlag") = "O" Then
                                        lblError.Text = "( " & sString & ") is Obsolete"
                                        Exit Sub
                                    Else
                                        dRow("Asset") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Asset"))
                                    End If
                                Else
                                    lblError.Text = "( " & sString & ") not matched,Create in Asset Master - Line No" & i + 1
                                    Exit Sub
                                End If
                            End If
                            'End If

                            ' lblAsset = GvAdditionExcel.Rows(i).FindControl("lblAsset")
                            Dim iAsset As Integer
                            iAsset = objOPExcel.GetAsset1(sSession.AccessCode, sSession.AccessCodeID, dRow("Asset"), ddlCustomerName.SelectedValue, iAssetClass)

                            Dim iAssetCount As Integer
                            iAssetCount = objAsstTrn.GetAssetOPB(sSession.AccessCode, sSession.AccessCodeID, iAssetClass, iAsset, ddlCustomerName.SelectedValue, 1)
                            If iAssetCount > 0 Then
                                lblFXAdditionExcelMsg.Text = (dRow("Asset")) & "- This Asset Already had Opening Balance"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDAdditionExcel').modal('show');", True)
                                Exit Sub
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("DateOfPurchase")) = False Then
                                If Trim(dtExcel.Rows(i).Item("DateOfPurchase").ToString()) = "" Then
                                    dRow("DateOfPurchase") = ""
                                Else
                                    Dim sdate As DateTime = DateTime.ParseExact(dtExcel.Rows(i).Item("DateOfPurchase"), "dd/MM/yyyy", Nothing)
                                    dRow("DateOfPurchase") = sdate.ToString("dd/MM/yyyy")
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("OriginalCost")) = False Then
                                If dtExcel.Rows(i).Item("OriginalCost").ToString <> "&nbsp;" Then
                                    dRow("OriginalCost") = dtExcel.Rows(i).Item("OriginalCost")
                                Else
                                    dRow("OriginalCost") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("WDVOpeningValue")) = False Then
                                If dtExcel.Rows(i).Item("WDVOpeningValue").ToString <> "&nbsp;" Then
                                    Dim damount As Decimal = dtExcel.Rows(i).Item("WDVOpeningValue")
                                    dRow("WDVOpeningValue") = Convert.ToDecimal(Math.Round(damount))
                                Else
                                    dRow("WDVOpeningValue") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("DepForthePeriod")) = False Then
                                If dtExcel.Rows(i).Item("DepForthePeriod").ToString <> "&nbsp;" Then
                                    dRow("DepForthePeriod") = dtExcel.Rows(i).Item("DepForthePeriod")
                                Else
                                    dRow("DepForthePeriod") = dRow("OriginalCost") - dRow("WDVOpeningValue")
                                End If
                            Else
                                dRow("DepForthePeriod") = dRow("OriginalCost") - dRow("WDVOpeningValue")
                            End If


                            dt.Rows.Add(dRow)
                        Next
                    End If

                    If IsNothing(dt) = True Then
                        Exit Sub
                    End If
                    GvAdditionExcel.DataSource = dt
                    GvAdditionExcel.DataBind()
                    dttable = dt.Copy
                Else
                    lblError.Text = "No Data"
                End If
            End If

            If ddlTransactionType.SelectedIndex = 2 Then
                dt.Columns.Add("Slno")
                dt.Columns.Add("Location")
                dt.Columns.Add("Division")
                dt.Columns.Add("Department")
                dt.Columns.Add("Bay")
                dt.Columns.Add("AssetClass")
                dt.Columns.Add("Asset")
                'dt.Columns.Add("AssetLocation")
                dt.Columns.Add("SupplierName")
                dt.Columns.Add("Particulars")
                dt.Columns.Add("DocNo")
                dt.Columns.Add("DocDate")
                dt.Columns.Add("BasicCost")
                dt.Columns.Add("TaxAmount")
                dt.Columns.Add("Cost")
                dt.Columns.Add("Total")
                dt.Columns.Add("AssetValue")

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

                            'If IsDBNull(dtExcel.Rows(i).Item("AssetClass")) = False Then
                            If dtExcel.Rows(i).Item("AssetClass").ToString() = "" Then
                                lblError.Text = "Asset Class Can not be blank- Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("AssetClass")
                                iAssetClass = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                bCheck = objOPExcel.GetAssetType1(sSession.AccessCode, sSession.AccessCodeID, sString, ddlCustomerName.SelectedValue)
                                If bCheck = True Then
                                    'If StringcheckArea(sString) = False Then
                                    '    lblError.Text = "Enter Valid Asset Type"
                                    '    Exit Sub
                                    'Else
                                    dRow("AssetClass") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("AssetClass"))
                                    'End If
                                Else
                                    lblError.Text = "( " & sString & ") not matched,Create in Asset Master"
                                    Exit Sub
                                End If
                            End If
                            'End If

                            Dim dt1 As New DataTable
                            'If IsDBNull(dtExcel.Rows(i).Item("Asset")) = False Then
                            If dtExcel.Rows(i).Item("Asset").ToString() = "" Then
                                lblError.Text = "Asset Can not be blank - Line No" & i + 1
                                Exit Sub
                            Else
                                sString = dtExcel.Rows(i).Item("Asset")
                                dt1 = objFxdAsst.TocheckExistAssetDescription(sSession.AccessCode, sSession.AccessCodeID, iAssetClass, sString, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                                If dt1.Rows.Count > 0 Then


                                    If IsDBNull(dt1.Rows(0)("AFAM_ItemDescription")) = True Then
                                        lblError.Text = "( " & sString & ") not matched,Create in Asset Master - Line No" & i + 1
                                        Exit Sub
                                    End If
                                    If dt1.Rows.Count > 0 Then
                                        If dt1.Rows(0)("AFAM_DelFlag") = "S" Then
                                            lblError.Text = "( " & sString & ") is Already Sold"
                                            Exit Sub
                                        ElseIf dt1.Rows(0)("AFAM_DelFlag") = "T" Then
                                            lblError.Text = "( " & sString & ") is Transfered to other location"
                                            Exit Sub
                                        ElseIf dt1.Rows(0)("AFAM_DelFlag") = "St" Then
                                            lblError.Text = "( " & sString & ") is Already Stolen"
                                            Exit Sub
                                        ElseIf dt1.Rows(0)("AFAM_DelFlag") = "D" Then
                                            lblError.Text = "( " & sString & ") is Destroyed"
                                            Exit Sub
                                        ElseIf dt1.Rows(0)("AFAM_DelFlag") = "O" Then
                                            lblError.Text = "( " & sString & ") is Obsolete"
                                            Exit Sub
                                        Else
                                            dRow("Asset") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item("Asset"))
                                        End If
                                    End If
                                End If
                            End If


                            If IsDBNull(dtExcel.Rows(i).Item("SupplierName")) = False Then
                                If dtExcel.Rows(i).Item("SupplierName").ToString <> "&nbsp;" Then
                                    dRow("SupplierName") = dtExcel.Rows(i).Item("SupplierName")
                                Else
                                    dRow("SupplierName") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("Particulars")) = False Then
                                If dtExcel.Rows(i).Item("Particulars").ToString <> "&nbsp;" Then
                                    dRow("Particulars") = dtExcel.Rows(i).Item("Particulars")
                                Else
                                    dRow("Particulars") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("DocNo")) = False Then
                                If dtExcel.Rows(i).Item("DocNo").ToString <> "&nbsp;" Then
                                    dRow("DocNo") = dtExcel.Rows(i).Item("DocNo")
                                Else
                                    dRow("DocNo") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("DocDate")) = False Then
                                If Trim(dtExcel.Rows(i).Item("DocDate").ToString()) = "" Then
                                    dRow("DocDate") = ""
                                Else
                                    Dim sdate As DateTime = DateTime.ParseExact(dtExcel.Rows(i).Item("DocDate"), "dd/MM/yyyy", Nothing)
                                    dRow("DocDate") = sdate.ToString("dd/MM/yyyy")
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("BasicCost")) = False Then
                                If dtExcel.Rows(i).Item("BasicCost").ToString <> "&nbsp;" Then
                                    dRow("BasicCost") = dtExcel.Rows(i).Item("BasicCost")
                                Else
                                    dRow("BasicCost") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("TaxAmount")) = False Then
                                If dtExcel.Rows(i).Item("TaxAmount").ToString <> "&nbsp;" Then
                                    dRow("TaxAmount") = dtExcel.Rows(i).Item("TaxAmount")
                                Else
                                    dRow("TaxAmount") = ""
                                End If
                            End If


                            If IsDBNull(dtExcel.Rows(i).Item("Cost")) = False Then
                                If dtExcel.Rows(i).Item("Cost").ToString <> "&nbsp;" Then
                                    dRow("Cost") = dtExcel.Rows(i).Item("Cost")
                                Else
                                    dRow("Cost") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("Total")) = False Then
                                If dtExcel.Rows(i).Item("Total").ToString <> "&nbsp;" Then
                                    dRow("Total") = dtExcel.Rows(i).Item("Total")
                                Else
                                    dRow("Total") = ""
                                End If
                            End If

                            If IsDBNull(dtExcel.Rows(i).Item("AssetValue")) = False Then
                                If dRow("Cost") = "Yes" Then
                                    If IsDBNull(dtExcel.Rows(i).Item("AssetValue")) = False Then
                                        If dtExcel.Rows(i).Item("AssetValue").ToString <> "&nbsp;" Then
                                            dRow("AssetValue") = dtExcel.Rows(i).Item("BasicCost") + dtExcel.Rows(i).Item("TaxAmount")
                                        Else
                                            dRow("AssetValue") = ""
                                        End If
                                    End If
                                Else
                                    dRow("AssetValue") = dtExcel.Rows(i).Item("BasicCost")
                                End If
                            End If

                            dt.Rows.Add(dRow)
                        Next
                    End If

                    If IsNothing(dt) = True Then
                        Exit Sub
                    End If
                    gvAssetAddition.DataSource = dt
                    gvAssetAddition.DataBind()
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
        Try
            Dim pattern As String = "^[0-9]\d*(\.\d+)?$"
            Dim AmountMatch As Match = Regex.Match(sAmt, pattern)
            If AmountMatch.Success Then
                Amountcheck = True
            Else
                Amountcheck = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Amountcheck" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
    Private Function LoadExcel(ByVal sFile As String) As DataTable
        Dim dbhelper As New DBHelper
        Dim dt As New DataTable
        Dim val As Integer = 0
        Try

            If ddlTransactionType.SelectedIndex = 1 Then
                dt.Columns.Add("Slno")
                dt.Columns.Add("Location")
                dt.Columns.Add("Division")
                dt.Columns.Add("Department")
                dt.Columns.Add("Bay")
                dt.Columns.Add("AssetClass")
                dt.Columns.Add("Asset")
                'dt.Columns.Add("AssetLocation")
                dt.Columns.Add("DateOfPurchase")
                dt.Columns.Add("OriginalCost")
                dt.Columns.Add("WDVOpeningValue")
                dt.Columns.Add("DepForthePeriod")

                dtExcel = dbhelper.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                'If IsNothing(dtExcel) = True Then
                '    Return dtExcel
                'End If
                If dt.Columns.Count <> dtExcel.Columns.Count Then
                    lblError.Text = "Invalid Excel format in selected sheet."
                    lblFXAdditionExcelMsg.Text = "Invalid Excel format In selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASFXDAdditionExcel').modal('show');", True)
                    ddlSheetName.Items.Clear()
                    Return dtExcel
                End If

                For i = 0 To dtExcel.Rows.Count - 1
                    Dim dRow As DataRow
                    dRow = dt.NewRow
                    'If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                    If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                            If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                dRow("Slno") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(0))
                            End If

                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(1)) = False Then
                            If dtExcel.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("Location") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(1))
                            Else
                                dRow("Location") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(2)) = False Then
                            If dtExcel.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("Division") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(2))
                            Else
                                dRow("Division") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(3)) = False Then
                            If dtExcel.Rows(i).Item(3).ToString <> "&nbsp;" Then
                                dRow("Department") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(3))
                            Else
                                dRow("Department") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(4)) = False Then
                            If dtExcel.Rows(i).Item(4).ToString <> "&nbsp;" Then
                                dRow("Bay") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(4))
                            Else
                                dRow("Bay") = ""
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
                                dRow("Asset") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(6))
                            Else
                                dRow("Asset") = ""
                            End If
                        End If
                        'If IsDBNull(dtExcel.Rows(i).Item(3)) = False Then
                        '    If dtExcel.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        '        dRow("AssetLocation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(3))
                        '    Else
                        '        dRow("AssetLocation") = ""
                        '    End If
                        'End If
                        If IsDBNull(dtExcel.Rows(i).Item(7)) = False Then
                            If dtExcel.Rows(i).Item(7).ToString <> "&nbsp;" Then
                                dRow("DateOfPurchase") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(7))
                            Else
                                dRow("DateOfPurchase") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(8)) = False Then
                            If dtExcel.Rows(i).Item(8).ToString <> "&nbsp;" Then
                                dRow("OriginalCost") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(8))
                            Else
                                dRow("OriginalCost") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(9)) = False Then
                            If dtExcel.Rows(i).Item(9).ToString <> "&nbsp;" Then
                                dRow("WDVOpeningValue") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(9))
                            Else
                                dRow("WDVOpeningValue") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(10)) = False Then
                            If dtExcel.Rows(i).Item(10).ToString <> "&nbsp;" Then
                                dRow("DepForthePeriod") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(10))
                            Else
                                dRow("DepForthePeriod") = ""
                            End If
                        End If

                    End If
                    'End If
                    dt.Rows.Add(dRow)
                Next
            End If

            If ddlTransactionType.SelectedIndex = 2 Then
                dt.Columns.Add("Slno")
                dt.Columns.Add("Location")
                dt.Columns.Add("Division")
                dt.Columns.Add("Department")
                dt.Columns.Add("Bay")
                dt.Columns.Add("AssetClass")
                dt.Columns.Add("Asset")
                'dt.Columns.Add("AssetLocation")
                dt.Columns.Add("SupplierName")
                dt.Columns.Add("Particulars")
                dt.Columns.Add("DocNo")
                dt.Columns.Add("DocDate")
                dt.Columns.Add("BasicCost")
                dt.Columns.Add("TaxAmount")
                dt.Columns.Add("Cost")
                dt.Columns.Add("Total")
                dt.Columns.Add("AssetValue")

                dtExcel = dbhelper.ReadExcel("Select * from [" & Trim(ddlSheetName.SelectedItem.Text) & "] ", sFile)
                'If IsNothing(dtExcel) = True Then
                '    Return dtExcel
                'End If

                If dt.Columns.Count <> dtExcel.Columns.Count Then
                    lblError.Text = "Invalid Excel format in selected sheet."
                    lblFXAdditionExcelMsg.Text = "Invalid Excel format In selected sheet."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalFASFXDAdditionExcel').modal('show');", True)
                    ddlSheetName.Items.Clear()
                    Return dtExcel
                End If

                For i = 0 To dtExcel.Rows.Count - 1
                    Dim dRow As DataRow
                    dRow = dt.NewRow
                    'If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                    If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        If IsDBNull(dtExcel.Rows(i).Item(0)) = False Then
                            If dtExcel.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                dRow("Slno") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(0))
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(1)) = False Then
                            If dtExcel.Rows(i).Item(1).ToString <> "&nbsp;" Then
                                dRow("Location") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(1))
                            Else
                                dRow("Location") = ""
                            End If

                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(2)) = False Then
                            If dtExcel.Rows(i).Item(2).ToString <> "&nbsp;" Then
                                dRow("Division") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(2))
                            Else
                                dRow("Division") = ""
                            End If

                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(3)) = False Then
                            If dtExcel.Rows(i).Item(3).ToString <> "&nbsp;" Then
                                dRow("Department") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(3))
                            Else
                                dRow("Department") = ""
                            End If

                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(4)) = False Then
                            If dtExcel.Rows(i).Item(4).ToString <> "&nbsp;" Then
                                dRow("Bay") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(4))
                            Else
                                dRow("Bay") = ""
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
                                dRow("Asset") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(6))
                            Else
                                dRow("Asset") = ""
                            End If
                        End If
                        'If IsDBNull(dtExcel.Rows(i).Item(3)) = False Then
                        '    If dtExcel.Rows(i).Item(3).ToString <> "&nbsp;" Then
                        '        dRow("AssetLocation") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(3))
                        '    Else
                        '        dRow("AssetLocation") = ""
                        '    End If
                        'End If
                        If IsDBNull(dtExcel.Rows(i).Item(7)) = False Then
                            If dtExcel.Rows(i).Item(7).ToString <> "&nbsp;" Then
                                dRow("SupplierName") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(7))
                            Else
                                dRow("SupplierName") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(8)) = False Then
                            If dtExcel.Rows(i).Item(8).ToString <> "&nbsp;" Then
                                dRow("Particulars") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(8))
                            Else
                                dRow("Particulars") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(9)) = False Then
                            If dtExcel.Rows(i).Item(9).ToString <> "&nbsp;" Then
                                dRow("DocNo") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(9))
                            Else
                                dRow("DocNo") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(10)) = False Then
                            If dtExcel.Rows(i).Item(10).ToString <> "&nbsp;" Then
                                dRow("DocDate") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(10))
                            Else
                                dRow("DocDate") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(11)) = False Then
                            If dtExcel.Rows(i).Item(11).ToString <> "&nbsp;" Then
                                dRow("BasicCost") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(11))
                            Else
                                dRow("BasicCost") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(12)) = False Then
                            If dtExcel.Rows(i).Item(12).ToString <> "&nbsp;" Then
                                dRow("TaxAmount") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(12))
                            Else
                                dRow("TaxAmount") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(13)) = False Then
                            If dtExcel.Rows(i).Item(13).ToString <> "&nbsp;" Then
                                dRow("Cost") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(13))
                            Else
                                dRow("Cost") = ""
                            End If
                        End If

                        If IsDBNull(dtExcel.Rows(i).Item(14)) = False Then
                            If dtExcel.Rows(i).Item(14).ToString <> "&nbsp;" Then
                                dRow("Total") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(14))
                            Else
                                dRow("Total") = ""
                            End If
                        End If
                        If IsDBNull(dtExcel.Rows(i).Item(15)) = False Then
                            If dtExcel.Rows(i).Item(15).ToString <> "&nbsp;" Then
                                dRow("AssetValue") = objClsFASGnrl.SafeSQL(dtExcel.Rows(i).Item(15))
                            Else
                                dRow("AssetValue") = ""
                            End If
                        End If

                    End If
                    'End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Dim filteredRows As DataTable = dt.Rows.Cast(Of DataRow)().Where(Function(row) row.ItemArray.Any(Function(field) Not (TypeOf field Is System.DBNull))).CopyToDataTable()
            Return filteredRows
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExcel" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function

    Private Sub GvAdditionExcel_PreRender(sender As Object, e As EventArgs) Handles GvAdditionExcel.PreRender
        Try
            If GvAdditionExcel.Rows.Count > 0 Then
                GvAdditionExcel.UseAccessibleHeader = True
                GvAdditionExcel.HeaderRow.TableSection = TableRowSection.TableHeader
                GvAdditionExcel.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GvAdditionExcel_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            FULoad.Dispose() : txtPath.Text = "" : ddlSheetName.SelectedIndex = 0 : GvAdditionExcel.DataSource = Nothing : GvAdditionExcel.DataBind()
            gvAssetAddition.DataSource = Nothing : gvAssetAddition.DataBind()
            'ddlAccZone.SelectedIndex = 0 : ddlAccRgn.SelectedIndex = 0 : ddlAccArea.SelectedIndex = 0 : ddlAccBrnch.SelectedIndex = 0
            'Response.Redirect(String.Format("~/FixedAsset/AssetAdditionExcelUpload.aspx?"), False)
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
            Response.Redirect(String.Format("~/FixedAsset/AssetAdditionDashBoard.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ImgbtnUpload_Click(sender As Object, e As ImageClickEventArgs) Handles ImgbtnUpload.Click
        Dim Arr As Array
        Dim lblSrNo As New Label
        Dim lblAssetClass, lblAsset, lblAssetLocation, lblDateOfPurchase As New Label
        Dim lblOriginalCost, lblWDVOpeningValue, lblDepForthePeriod As New Label
        Dim iAssetClass, iAsset As Integer
        Dim bCheck As Boolean
        'Details
        Dim lblPKID As New Label, lblParticulars As New Label, lblDocDate As New Label, lblBasicCost As New Label
        Dim lblTaxAmount As New Label, lblTotal As New Label, lblAssetValue As New Label
        Dim lblDocNo As Label, lblSupplierName As New Label
        Dim iMasterID As Integer = 0

        Dim AssetTyp, RefNo As New DataTable
        Dim dt As New DataTable
        Dim lblLocation, lblDivision, lbldepartment, lblbay As New Label
        Dim lblLocation1, lblDivision1, lbldepartment1, lblbay1 As New Label
        Try
            lblError.Text = ""

            If ddlTransactionType.SelectedIndex = 1 Then

                If GvAdditionExcel.Rows.Count > 0 Then
                    For i = 0 To dttable.Rows.Count - 1
                        objOPExcel.iAFAA_ID = 0

                        lblLocation = GvAdditionExcel.Rows(i).FindControl("lblLocation")
                        lblLocation.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblLocation.Text, ddlCustomerName.SelectedValue)
                        If lblLocation.Text <> "" Then
                            objOPExcel.iAFAA_Location = lblLocation.Text
                        Else
                            objOPExcel.iAFAA_Location = 0
                        End If

                        lblDivision = GvAdditionExcel.Rows(i).FindControl("lblDivision")
                        lblDivision.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblDivision.Text, ddlCustomerName.SelectedValue)
                        If lblDivision.Text <> "" Then
                            objOPExcel.iAFAA_Division = lblDivision.Text
                        Else
                            objOPExcel.iAFAA_Division = 0
                        End If

                        lbldepartment = GvAdditionExcel.Rows(i).FindControl("lblDepartment")
                        lbldepartment.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lbldepartment.Text, ddlCustomerName.SelectedValue)
                        If lblDivision.Text <> "" Then
                            objOPExcel.iAFAA_Department = lbldepartment.Text
                        Else
                            objOPExcel.iAFAA_Department = 0
                        End If

                        lblbay = GvAdditionExcel.Rows(i).FindControl("lblBay")
                        lblbay.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblbay.Text, ddlCustomerName.SelectedValue)
                        If lblbay.Text <> "" Then
                            objOPExcel.iAFAA_Bay = lblbay.Text
                        Else
                            objOPExcel.iAFAA_Bay = 0
                        End If

                        lblAssetClass = GvAdditionExcel.Rows(i).FindControl("lblAssetClass")
                        iAssetClass = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, lblAssetClass.Text, ddlCustomerName.SelectedValue)

                        If iAssetClass > 0 Then
                            objOPExcel.sAFAA_AssetType = iAssetClass
                        Else
                            objOPExcel.sAFAA_AssetType = ""
                        End If

                        If iAssetClass <> 0 Then
                            Dim AssetLen As String = objAsstTrn.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, iAssetClass, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                            objOPExcel.sAFAA_AssetNo = AssetLen
                        End If

                        lblAsset = GvAdditionExcel.Rows(i).FindControl("lblAsset")
                        iAsset = objOPExcel.GetAsset1(sSession.AccessCode, sSession.AccessCodeID, lblAsset.Text, ddlCustomerName.SelectedValue, iAssetClass)

                        'bCheck = objAsstTrn.TocheckExistAsset(sSession.AccessCode, sSession.AccessCodeID, iAssetClass, iAsset, ddlFinancialYear.SelectedValue , ddlCustomerName.SelectedValue, ddlTransactionType.SelectedIndex)
                        'If bCheck = True Then
                        '    lblError.Text = "This is Already Exist, Please Enter Diffrent Asset Line No-" & i + 1
                        '    Exit Sub
                        'End If

                        If iAsset > 0 Then
                            objOPExcel.iAFAA_ItemType = iAsset
                        Else
                            objOPExcel.iAFAA_ItemType = 0
                        End If

                        'lblAssetLocation = GvAdditionExcel.Rows(i).FindControl("lblAssetLocation")
                        'If lblAssetLocation.Text <> "" Then
                        '    objOPExcel.sAFAA_ActualLocn = lblAssetLocation.Text
                        'Else
                        '    objOPExcel.sAFAA_ActualLocn = ""
                        'End If

                        'lblDateOfPurchase = GvAdditionExcel.Rows(i).FindControl("lblDateofPurchase")
                        'If lblDateOfPurchase.Text <> "" Then
                        '    objOPExcel.dAFAA_PurchaseDate = Date.ParseExact(lblDateOfPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        'Else
                        '    objOPExcel.dAFAA_PurchaseDate = "01/01/1900"
                        'End If

                        lblDateOfPurchase = GvAdditionExcel.Rows(i).FindControl("lblDateofPurchase")
                        If lblDateOfPurchase.Text <> "" Then
                            objOPExcel.dAFAA_PurchaseDate = Date.ParseExact(lblDateOfPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objOPExcel.dAFAA_PurchaseDate = "01/01/1900"
                        End If

                        lblOriginalCost = GvAdditionExcel.Rows(i).FindControl("lblOriginalCost")
                        If lblOriginalCost.Text <> "" Then
                            objOPExcel.dAFAA_AssetAmount = lblOriginalCost.Text
                        Else
                            objOPExcel.dAFAA_AssetAmount = "0.00"
                        End If

                        lblWDVOpeningValue = GvAdditionExcel.Rows(i).FindControl("lblWDVOpeningValue")
                        If lblWDVOpeningValue.Text <> "" Then
                            objOPExcel.dAFAA_FYAmount = lblWDVOpeningValue.Text
                        Else
                            objOPExcel.dAFAA_FYAmount = "0.00"
                        End If

                        lblDepForthePeriod = GvAdditionExcel.Rows(i).FindControl("lblDepForthePeriod")
                        If lblDepForthePeriod.Text <> "" Then
                            objOPExcel.dAFAA_DepreAmount = lblDepForthePeriod.Text
                        Else
                            objOPExcel.dAFAA_DepreAmount = "0.00"
                        End If

                        objFxdAsst.AFAM_CreatedOn = Date.Today
                        objFxdAsst.AFAM_UpdatedBy = 0
                        objFxdAsst.AFAM_UpdatedOn = Date.Today
                        objOPExcel.iAFAA_CreatedBy = sSession.UserID
                        objOPExcel.sAFAA_Delflag = "A"
                        objOPExcel.sAFAA_Status = "A"
                        objOPExcel.iAFAA_YearID = ddlFinancialYear.SelectedValue
                        objOPExcel.iAFAA_CompID = sSession.AccessCodeID
                        objOPExcel.sAFAA_Operation = "C"
                        objOPExcel.sAFAA_IPAddress = sSession.IPAddress
                        objOPExcel.iAFAA_CustId = ddlCustomerName.SelectedValue
                        objOPExcel.iAFAA_TrType = 1
                        Arr = objOPExcel.SaveFixedAssetAddition(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
                    Next
                Else
                    lblError.Text = "No Data"
                    Exit Sub
                End If
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Addition/Revalution", "Saved", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            End If

            If ddlTransactionType.SelectedIndex = 2 Then

                If gvAssetAddition.Rows.Count > 0 Then
                    For i = 0 To gvAssetAddition.Rows.Count - 1

                        objOPExcel.iAFAA_ID = 0

                        lblLocation = gvAssetAddition.Rows(i).FindControl("lblLocation")
                        lblLocation.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblLocation.Text, ddlCustomerName.SelectedValue)
                        If lblLocation.Text <> "" Then
                            objOPExcel.iAFAA_Location = lblLocation.Text
                        Else
                            objOPExcel.iAFAA_Location = 0
                        End If

                        lblDivision = gvAssetAddition.Rows(i).FindControl("lblDivision")
                        lblDivision.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblDivision.Text, ddlCustomerName.SelectedValue)
                        If lblDivision.Text <> "" Then
                            objOPExcel.iAFAA_Division = lblDivision.Text
                        Else
                            objOPExcel.iAFAA_Division = 0
                        End If

                        lbldepartment = gvAssetAddition.Rows(i).FindControl("lblDepartment")
                        lbldepartment.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lbldepartment.Text, ddlCustomerName.SelectedValue)
                        If lblDivision.Text <> "" Then
                            objOPExcel.iAFAA_Department = lbldepartment.Text
                        Else
                            objOPExcel.iAFAA_Department = 0
                        End If

                        lblbay = gvAssetAddition.Rows(i).FindControl("lblBay")
                        lblbay.Text = objOPExcel.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, lblbay.Text, ddlCustomerName.SelectedValue)
                        If lblbay.Text <> "" Then
                            objOPExcel.iAFAA_Bay = lblbay.Text
                        Else
                            objOPExcel.iAFAA_Bay = 0
                        End If

                        lblAssetClass = gvAssetAddition.Rows(i).FindControl("lblAssetClass")
                        iAssetClass = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, lblAssetClass.Text, ddlCustomerName.SelectedValue)

                        If iAssetClass > 0 Then
                            objOPExcel.sAFAA_AssetType = iAssetClass
                        Else
                            objOPExcel.sAFAA_AssetType = ""
                        End If

                        If iAssetClass <> 0 Then
                            Dim AssetLen As String = objAsstTrn.GetAssetTypeNo(sSession.AccessCode, sSession.AccessCodeID, iAssetClass, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                            objOPExcel.sAFAA_AssetNo = AssetLen
                        End If

                        lblAsset = gvAssetAddition.Rows(i).FindControl("lblAsset")
                        iAsset = objOPExcel.GetAsset1(sSession.AccessCode, sSession.AccessCodeID, lblAsset.Text, ddlCustomerName.SelectedValue, iAssetClass)

                        If iAsset > 0 Then
                            objOPExcel.iAFAA_ItemType = iAsset
                        Else
                            objOPExcel.iAFAA_ItemType = 0
                        End If

                        'lblAssetLocation = gvAssetAddition.Rows(i).FindControl("lblAssetLocation")
                        'If lblAssetLocation.Text <> "" Then
                        '    objOPExcel.sAFAA_ActualLocn = lblAssetLocation.Text
                        'Else
                        '    objOPExcel.sAFAA_ActualLocn = ""
                        'End If

                        objOPExcel.dAFAA_PurchaseDate = "01/01/1900"
                        objOPExcel.dAFAA_AssetAmount = "0.00"
                        objOPExcel.dAFAA_FYAmount = "0.00"
                        objOPExcel.dAFAA_DepreAmount = "0.00"


                        objFxdAsst.AFAM_CreatedOn = Date.Today
                        objFxdAsst.AFAM_UpdatedBy = 0
                        objFxdAsst.AFAM_UpdatedOn = Date.Today
                        objOPExcel.iAFAA_CreatedBy = sSession.UserID
                        objOPExcel.sAFAA_Delflag = "A"
                        objOPExcel.sAFAA_Status = "A"
                        objOPExcel.iAFAA_YearID = ddlFinancialYear.SelectedValue
                        objOPExcel.iAFAA_CompID = sSession.AccessCodeID
                        objOPExcel.sAFAA_Operation = "C"
                        objOPExcel.sAFAA_IPAddress = sSession.IPAddress
                        objOPExcel.iAFAA_CustId = ddlCustomerName.SelectedValue
                        objOPExcel.iAFAA_TrType = 2
                        Arr = objOPExcel.SaveFixedAssetAddition(sSession.AccessCode, sSession.AccessCodeID, objOPExcel)
                        iMasterID = Arr(1)

                        ''details

                        objAsstTrn.iFAAD_PKID = 0

                        objAsstTrn.iFAAD_MasID = iMasterID

                        lblLocation1 = gvAssetAddition.Rows(i).FindControl("lblLocation")
                        lblLocation1.Text = objOPExcel.LoadLocation1(sSession.AccessCode, sSession.AccessCodeID, lblLocation1.Text, ddlCustomerName.SelectedValue)
                        If lblLocation1.Text <> "" Then
                            objAsstTrn.iFAAD_Location = lblLocation1.Text
                        Else
                            objAsstTrn.iFAAD_Location = 0
                        End If

                        lblDivision1 = gvAssetAddition.Rows(i).FindControl("lblDivision")
                        lblDivision1.Text = objOPExcel.LoadLocation1(sSession.AccessCode, sSession.AccessCodeID, lblDivision1.Text, ddlCustomerName.SelectedValue)
                        If lblDivision1.Text <> "" Then
                            objAsstTrn.iFAAD_Division = lblDivision1.Text
                        Else
                            objAsstTrn.iFAAD_Division = 0
                        End If

                        lbldepartment1 = gvAssetAddition.Rows(i).FindControl("lblDepartment")
                        lbldepartment1.Text = objOPExcel.LoadLocation1(sSession.AccessCode, sSession.AccessCodeID, lbldepartment1.Text, ddlCustomerName.SelectedValue)
                        If lbldepartment1.Text <> "" Then
                            objAsstTrn.iFAAD_Department = lbldepartment.Text
                        Else
                            objAsstTrn.iFAAD_Department = 0
                        End If

                        lblbay1 = gvAssetAddition.Rows(i).FindControl("lblBay")
                        lblbay1.Text = objOPExcel.LoadLocation1(sSession.AccessCode, sSession.AccessCodeID, lblbay1.Text, ddlCustomerName.SelectedValue)
                        If lblbay1.Text <> "" Then
                            objAsstTrn.iFAAD_Bay = lblbay.Text
                        Else
                            objAsstTrn.iFAAD_Bay = 0
                        End If

                        lblSupplierName = gvAssetAddition.Rows(i).FindControl("lblSupplierName")
                        If lblSupplierName.Text = "" Then
                            objAsstTrn.sFAAD_SupplierName = ""
                        Else
                            objAsstTrn.sFAAD_SupplierName = lblSupplierName.Text
                        End If

                        lblParticulars = gvAssetAddition.Rows(i).FindControl("lblParticulars")
                        If lblParticulars.Text = "" Then
                            objAsstTrn.sFAAD_Particulars = ""
                        Else
                            objAsstTrn.sFAAD_Particulars = lblParticulars.Text
                        End If

                        lblDocNo = gvAssetAddition.Rows(i).FindControl("lblDocNo")
                        If lblDocNo.Text = "" Then
                            objAsstTrn.sFAAD_DocNo = ""
                        Else
                            objAsstTrn.sFAAD_DocNo = lblDocNo.Text
                        End If

                        'lblDocDate = gvAssetAddition.Rows(i).FindControl("lblDocDate")

                        '    If lblDocDate.Text <> "" Then
                        '        objAsstTrn.dFAAD_DocDate = lblDocDate.Text
                        '    End If

                        'lblDocDate = gvAssetAddition.Rows(i).FindControl("lblDocDate")
                        'If lblDocDate.Text <> "" Then
                        '    objAsstTrn.dFAAD_DocDate = Date.ParseExact(lblDocDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        'Else
                        '    objAsstTrn.dFAAD_DocDate = "01/01/1900"
                        'End If

                        lblDocDate = gvAssetAddition.Rows(i).FindControl("lblDocDate")
                        If lblDocDate.Text <> "" Then
                            objAsstTrn.dFAAD_DocDate = Date.ParseExact(lblDocDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        Else
                            objAsstTrn.dFAAD_DocDate = "01/01/1900"
                        End If

                        lblBasicCost = gvAssetAddition.Rows(i).FindControl("lblBasicCost")
                        If lblBasicCost.Text = "" Then
                            objAsstTrn.dFAAD_BasicCost = 0
                        Else
                            objAsstTrn.dFAAD_BasicCost = lblBasicCost.Text
                        End If

                        lblTaxAmount = gvAssetAddition.Rows(i).FindControl("lblTaxAmount")
                        If lblTaxAmount.Text = "" Then
                            objAsstTrn.dFAAD_TaxAmount = 0
                        Else
                            objAsstTrn.dFAAD_TaxAmount = lblTaxAmount.Text
                        End If

                        lblTotal = gvAssetAddition.Rows(i).FindControl("lblTotal")
                        If lblTotal.Text = "" Then
                            objAsstTrn.dFAAD_Total = 0
                        Else
                            objAsstTrn.dFAAD_Total = lblTotal.Text
                        End If

                        lblAssetValue = gvAssetAddition.Rows(i).FindControl("lblAssetValue")
                        If lblAssetValue.Text = "" Then
                            objAsstTrn.dFAAD_AssetValue = 0
                        Else
                            objAsstTrn.dFAAD_AssetValue = lblAssetValue.Text
                        End If

                        objAsstTrn.iFAAD_chkCost = 0
                        If ddlTransactionType.SelectedIndex = 1 Then
                            objAsstTrn.iFAAD_OtherTrType = 1
                        ElseIf ddlTransactionType.SelectedIndex = 2 Then
                            objAsstTrn.iFAAD_OtherTrType = 2
                        End If
                        objAsstTrn.sFAAD_OtherAmount = 0
                        objAsstTrn.iFAAD_CreatedBy = sSession.UserID
                        objAsstTrn.dFAAD_CreatedOn = DateTime.Today
                        objAsstTrn.iFAAD_UpdatedBy = sSession.UserID
                        objAsstTrn.sFAAD_IPAddress = sSession.IPAddress
                        objAsstTrn.iFAAD_CompID = sSession.AccessCodeID
                        objAsstTrn.sFAAD_Delflag = "A"
                        objAsstTrn.sFAAD_Status = "C"
                        objAsstTrn.iFAAD_YearID = ddlFinancialYear.SelectedValue

                        lblAssetClass = gvAssetAddition.Rows(i).FindControl("lblAssetClass")
                        iAssetClass = objOPExcel.GetAssetType(sSession.AccessCode, sSession.AccessCodeID, lblAssetClass.Text, ddlCustomerName.SelectedValue)

                        If iAssetClass > 0 Then
                            objAsstTrn.iFAAD_AssetType = iAssetClass
                        Else
                            objAsstTrn.iFAAD_AssetType = ""
                        End If

                        lblAsset = gvAssetAddition.Rows(i).FindControl("lblAsset")
                        iAsset = objOPExcel.GetAsset1(sSession.AccessCode, sSession.AccessCodeID, lblAsset.Text, ddlCustomerName.SelectedValue, iAssetClass)

                        If iAsset > 0 Then
                            objAsstTrn.iFAAD_ItemType = iAsset
                        Else
                            objAsstTrn.iFAAD_ItemType = 0
                        End If
                        objAsstTrn.iFAAD_CustId = ddlCustomerName.SelectedValue
                        Arr = objAsstTrn.SaveFixedAssetAdditionDetails(sSession.AccessCode, sSession.AccessCodeID, objAsstTrn)
                    Next
                Else
                    lblError.Text = "No Data"
                    Exit Sub
                End If
            End If
            If Arr(0) = "2" Then
                lblFXAdditionExcelMsg.Text = "Successfully Updated"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDAdditionExcel').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Addition/Revalution", "Updated", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            ElseIf Arr(0) = "3" Then
                lblFXAdditionExcelMsg.Text = "Successfully Uploaded"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASFXDAdditionExcel').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Addition/Revalution", "Saved", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            End If

            imgbtnRefresh_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgbtnUpload_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssetAddition_PreRender(sender As Object, e As EventArgs) Handles gvAssetAddition.PreRender
        Try
            If gvAssetAddition.Rows.Count > 0 Then
                gvAssetAddition.UseAccessibleHeader = True
                gvAssetAddition.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssetAddition.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssetAddition_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlTransactionType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTransactionType.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlTransactionType.SelectedIndex = 1 Then
                GvAdditionExcel.Visible = True
                gvAssetAddition.Visible = False
            ElseIf ddlTransactionType.SelectedIndex = 2 Then
                gvAssetAddition.Visible = True
                GvAdditionExcel.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTransactionType_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
