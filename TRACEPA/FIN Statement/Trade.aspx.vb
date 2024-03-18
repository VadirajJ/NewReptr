Imports System
Imports System.Data
Imports System.IO
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Imports ExcelDataReader
Imports Microsoft.Reporting.WebForms
Imports Microsoft.ReportingServices.Rendering.ExcelRenderer
Imports System.ComponentModel

Public Class Traid
    Inherits System.Web.UI.Page
    Private sFormName As String = "Trade"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsUSEntry As New clsUploadStockEntry
    Dim objUT As New ClsTrade
    Private objCGLLink As New ClsCustomerGLLink
    Private objclsOpeningBalance As New clsOpeningBalance
    Private Shared sExcelSave As String
    Private Shared sFile As String = ""
    Private Shared TotalOpeningCredit As Decimal = 0
    Private Shared TotalOpeningDebit As Double = 0
    Private Shared TOtaltrCredit As Double = 0
    Private Shared TOtaltrDebit As Double = 0
    Private Shared TOtalClosingCredit As Double = 0
    Private Shared TOtalClosingDebit As Double = 0
    Private Shared Unmapped As Integer = 0
    Private objclsAccRatios As New clsAccountingRatios
    Dim dLessmonth As Double = 0, dmorethen6 As Double = 0, d1year As Double = 0, d2year As Double = 0,
        d3year As Double = 0, dMorethen As Double = 0, dTotalAmount As Double = 0

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iFormID As Integer = 0
        Dim sFormButtons As String
        Dim dtSampleFormat As New DataTable
        Try
            dgGeneralSchedTemp.Enabled = True
            ddlSheetNameSchedTemp.Enabled = False

            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadExistingCustomer() : BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustomerName.SelectedValue = sSession.CustomerID
                        ddlCustomerName_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustomerName.SelectedValue = sSession.CustomerID
                        If ddlCustomerName.SelectedIndex > 0 Then
                            ddlCustomerName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
                If ddlcategory.SelectedIndex > 0 Then
                    ddlcategory_SelectedIndexChanged(sender, e)
                End If
            End If




        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub LoadExistingCustomer()
        Try
            ddlCustomerName.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "Cust_Name"
            ddlCustomerName.DataValueField = "Cust_Id"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingCustomer")
            'Throw
        End Try
    End Sub
    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objUT.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim lblslno As New Label, Label2 As New Label, lblLessmonth As New Label, lblmorethen6 As New Label, lbl1year As New Label, lbl2year As New Label, lbl3year As New Label, lblMorethen As New Label
        Dim lblTotalAmount As New Label
        Dim dtbranch As New DataTable
        Dim sOrgType As String = ""
        Dim dtRatios As New DataTable
        Try
            lblError.Text = ""


            If ddlCustomerName.SelectedIndex > 0 Then
                If ddlFinancialYear.SelectedIndex > 0 Then
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    Session("AllSession") = sSession

                    ddlbranchName.Visible = True
                    LblBranchName.Visible = True
                    ddlcategory.SelectedIndex = 0
                    dgGeneralSchedTemp.Visible = False
                    dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If dtbranch.Rows.Count > 0 Then
                        ddlbranchName.DataSource = dtbranch
                        ddlbranchName.DataTextField = "BranchName"
                        ddlbranchName.DataValueField = "Branchid"
                        ddlbranchName.DataBind()
                        ddlbranchName.Items.Insert(0, "Select Branch Name")

                        If sSession.ScheduleBranchId <> 0 Then
                            ddlbranchName.SelectedValue = sSession.ScheduleBranchId
                            DdlbranchSchedTemp_SelectedIndexChanged(sender, e)
                        Else
                            sSession.ScheduleBranchId = 0
                        End If
                        Session("AllSession") = sSession
                    Else
                        lblModalValidationMsg.Text = "Customer should have atleast one Branch, Please add"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                        lblError.Text = lblModalValidationMsg.Text
                        ddlbranchName.DataSource = dtbranch
                        ddlbranchName.DataTextField = "BranchName"
                        ddlbranchName.DataValueField = "Branchid"
                        ddlbranchName.DataBind()
                        ddlbranchName.Items.Insert(0, "Select Branch Name")
                        Exit Sub
                    End If
                End If
                '   dt =
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub btnOkSchedTemp_Click(sender As Object, e As EventArgs) Handles btnOkSchedTemp.ServerClick
        Dim dt As New DataTable
        Dim sFileName As String, sExt As String, sPath As String
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblModalValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlbranchName.SelectedIndex = 0 Then
                lblError.Text = "Select Branch." : lblModalValidationMsg.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlcategory.SelectedIndex = 0 Then
                lblError.Text = "Select Category." : lblModalValidationMsg.Text = "Select Category."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlOthType.SelectedIndex = 0 Then
                lblError.Text = "Select Other Type." : lblModalValidationMsg.Text = "Select Other Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

            dgGeneralSchedTemp.Visible = False
            If FULoadSchedTemp.FileName <> String.Empty Then
                lblSheetNameSchedTemp.Visible = True : ddlSheetNameSchedTemp.Visible = True
                sExt = IO.Path.GetExtension(FULoadSchedTemp.PostedFile.FileName)
                Session("sExt") = sExt
                If UCase(sExt) = ".XLS" Or UCase(sExt) = ".XLSX" Then
                    sFileName = System.IO.Path.GetFileName(FULoadSchedTemp.PostedFile.FileName)
                    Session("sFileName") = sFileName
                    sPath = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                    If sPath.EndsWith("\") = False Then
                        sFile = sPath & "\" & sFileName
                    Else
                        sFile = sPath & sFileName
                    End If
                    FULoadSchedTemp.PostedFile.SaveAs(sFile)
                    ddlSheetNameSchedTemp.Items.Clear()

                    dt = ExcelSheetNamesSchedTemp(sFile)
                    ddlSheetNameSchedTemp.DataSource = dt
                    ddlSheetNameSchedTemp.DataTextField = "Name"
                    ddlSheetNameSchedTemp.DataValueField = "ID"
                    ddlSheetNameSchedTemp.DataBind()
                    ddlSheetNameSchedTemp.Items.Insert(0, "Select Sheet")
                    ddlSheetNameSchedTemp.SelectedValue = 1



                    ddlSheetNameSchedTemp_SelectedIndexChanged(sender, e)


                Else
                    lblError.Text = "Select Excel file only." : lblModalValidationMsg.Text = "Select Excel file only."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Excel file." : lblModalValidationMsg.Text = "Select Excel file."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnOkSchedTemp_Click")
        End Try
    End Sub
    Public Function ExcelSheetNamesSchedTemp(ByVal sPath As String) As DataTable
        Dim dt As New DataTable
        Dim XLCon As OleDb.OleDbConnection
        Dim dtTab As New DataTable
        Dim drow As DataRow
        Dim i As Integer
        Try

            'Dim xlWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
            'Dim xlApp As New Microsoft.Office.Interop.Excel.Application

            'xlApp.Workbooks.Open(sPath, 0, True)

            '' For the first sheet in an excel spreadsheet
            'xlWorkSheet = CType(xlApp.Sheets(1),
            '        Microsoft.Office.Interop.Excel.Worksheet)
            'Dim strSheetName As New List(Of String)
            'If xlApp.Sheets.Count > 0 Then
            '    strSheetName.Add(xlWorkSheet.Name)
            '    dtTab.Columns.Add("ID")
            '    dtTab.Columns.Add("Name")
            '    For Each xlWorkSheet In xlApp.Sheets
            '        drow = dtTab.NewRow
            '        drow("ID") = i + 1
            '        drow("Name") = xlWorkSheet.Name
            '        dtTab.Rows.Add(drow)
            '    Next
            'End If

            XLCon = MSAccessOpenConnectionSchedTemp(sPath)
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ExcelSheetNamesSchedTemp")
            Throw
        End Try
    End Function
    Private Function MSAccessOpenConnectionSchedTemp(ByVal sFile As String) As OleDb.OleDbConnection
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "MSAccessOpenConnectionSchedTemp")
            'Throw
        End Try
    End Function
    Protected Sub ddlSheetNameSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSheetNameSchedTemp.SelectedIndexChanged
        Dim dttable As New DataTable
        Dim sStr As String = "", sYear As String
        Dim iYearID As Integer, iCheckMasterCounts As Integer = 0
        Dim Arr() As String
        Dim LabelName, lblLessmonth, lblmorethen6, lbl1year, lbl2year, lbl3year, lblMorethen, lblTotalAmount As New Label
        Try
            lblError.Text = ""

            dgGeneralSchedTemp.Visible = False

            If ddlSheetNameSchedTemp.SelectedIndex > 0 Then

                dttable = LoadTrialBalanceDataSchedTemp(sFile)

                dgGeneralSchedTemp.DataSource = dttable
                dgGeneralSchedTemp.DataBind()
                dgGeneralSchedTemp.Visible = True
                SaveTradeScheduleSchedTemp()

            End If
        Catch ex As Exception
            If ex.Message.Contains("Cannot find column ") = True Then
                ddlSheetNameSchedTemp.SelectedIndex = 0
                lblError.Text = "Invalid Excel format in selected sheet." : lblModalValidationMsg.Text = "Invalid Excel format in selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
            Else
                lblError.Text = ex.Message
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSheetName_SelectedIndexChanged")
        End Try
    End Sub
    Private Function LoadTrialBalanceDataSchedTemp(ByVal sFile As String) As DataTable
        Dim dtTable As New DataTable, dtDetails As New DataTable, dtdescDetails As New DataTable
        Dim objDBL As New DBHelper
        Dim dRow As DataRow
        Dim dtotal As Decimal
        Dim morethnsixmtotal As Decimal
        Dim oneytotal As Decimal
        Dim twoytotal As Decimal
        Dim treeytotal As Decimal
        Dim morethntotal As Decimal
        Dim tdtotal As Decimal
        Dim i As Integer
        Dim dt As New DataTable
        Dim dtAccnts As New DataTable
        Dim orgTypeId As Integer = 0
        Dim dSubItemId As New Integer : Dim dItemId As Integer = 0
        Dim dSubHeadingId As Integer = 0 : Dim dHeadingId As Integer = 0
        Dim dtgroup As New DataTable

        Try
            dtTable.Columns.Add("Srno")
            dtTable.Columns.Add("ATU_Name")
            dtTable.Columns.Add("ATU_Less_than_six_Month")
            dtTable.Columns.Add("ATU_More_than_six_Month")
            dtTable.Columns.Add("ATU_One_Year")
            dtTable.Columns.Add("ATU_Two_Year")
            dtTable.Columns.Add("ATU_Three_Year")
            dtTable.Columns.Add("ATU_More_than")
            dtTable.Columns.Add("ATU_Total_Amount")
            dtDetails = objDBL.ReadExcel("Select * from [" & Trim(ddlSheetNameSchedTemp.SelectedItem.Text) & "] ", sFile)
            If IsNothing(dtDetails) = True Then
                lblError.Text = "Invalid Excel format in selected sheet."
                lblModalValidationMsg.Text = "Invalid Excel format In selected sheet."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalExcelValidation').modal('show');", True)
                ddlSheetNameSchedTemp.Items.Clear()
                Return dtDetails
            End If
            For i = 0 To dtDetails.Rows.Count - 1
                If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                    dRow = dtTable.NewRow

                    dRow("Srno") = i + 1

                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then
                            dRow("ATU_Name") = objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(0))


                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_Less_than_six_Month") = Convert.ToDecimal(dtDetails.Rows(i).Item(1).ToString()).ToString("#,##0.00")
                            dtotal = dtotal + objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(1))
                            dtotal = Convert.ToDecimal(dtotal).ToString("#,##0.00")


                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_More_than_six_Month") = Convert.ToDecimal(dtDetails.Rows(i).Item(2).ToString()).ToString("#,##0.00")
                            morethnsixmtotal = morethnsixmtotal + objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(2))
                            '            morethnsixmtotal = Convert.ToDecimal(morethnsixmtotal).ToString("#,##0.00")

                        End If
                    End If

                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_One_Year") = Convert.ToDecimal(dtDetails.Rows(i).Item(3).ToString()).ToString("#,##0.00")
                            oneytotal = oneytotal + objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(3))

                            '         oneytotal = Convert.ToDecimal(oneytotal).ToString("#,##0.00")
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_Two_Year") = Convert.ToDecimal(dtDetails.Rows(i).Item(4).ToString()).ToString("#,##0.00")
                            twoytotal = twoytotal + objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(4))
                            '             twoytotal = Convert.ToDecimal(twoytotal).ToString("#,##0.00")
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_Three_Year") = Convert.ToDecimal(dtDetails.Rows(i).Item(5).ToString()).ToString("#,##0.00")
                            treeytotal = treeytotal + objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(5))
                            '           treeytotal = Convert.ToDecimal(treeytotal).ToString("#,##0.00")
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_More_than") = Convert.ToDecimal(dtDetails.Rows(i).Item(6).ToString()).ToString("#,##0.00")

                            morethntotal = morethntotal + objclsGRACeGeneral.SafeSQL(dtDetails.Rows(i).Item(6))
                            '       morethntotal = Convert.ToDecimal(morethntotal).ToString("#,##0.00")
                        End If
                    End If

                    If IsDBNull(dtDetails.Rows(i).Item(0)) = False Then
                        If dtDetails.Rows(i).Item(0).ToString <> "&nbsp;" Then

                            dRow("ATU_Total_Amount") = Convert.ToDecimal(dtDetails.Rows(i).Item(1) + dtDetails.Rows(i).Item(2) + dtDetails.Rows(i).Item(3) + dtDetails.Rows(i).Item(4) + dtDetails.Rows(i).Item(5) + dtDetails.Rows(i).Item(6).ToString()).ToString("#,##0.00")
                            tdtotal = tdtotal + dRow("ATU_Total_Amount")
                            '          tdtotal = Convert.ToDecimal(tdtotal).ToString("#,##0.00")
                        End If
                    End If
                    dtTable.Rows.Add(dRow)
                End If
            Next
            Return dtTable
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadTrialBalanceDataSchedTemp")
            'Throw
        End Try
    End Function
    Private Sub DdlbranchSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlbranchName.SelectedIndexChanged
        Dim dtTable As New DataTable, dtDetails As New DataTable, dtdescDetails As New DataTable
        Dim objDBL As New DBHelper
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlbranchName.SelectedIndex > 0 Then
                dgGeneralSchedTemp.Visible = False
                ddlcategory.SelectedIndex = 0
                ddlOthType.SelectedIndex = 0

                'Dim dt As DataSet = objUT.GetTrDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlbranchName.SelectedValue)
                'If dt IsNot Nothing AndAlso dt.Tables.Count > 0 Then
                '    Dim dt1 As DataTable = dt.Tables(0)

                'Else
                '    lblError.Text = "No data found."
                '    dgGeneralSchedTemp.Visible = False
                'End If
            Else
                lblError.Text = "Select the Branch"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DdlbranchSchedTemp_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("UploadTrailbalanceSchedule.aspx?"), False)
        Catch ex As Exception

        End Try
    End Sub



    Private Sub SaveTradeScheduleSchedTemp()
        Dim Arr() As String
        Dim LabelName, lblmorethen6, lbl1year, lbl2year, lbl3year, lblMorethen, lblTotalAmount As Label
        Dim lblLessmonth As Label
        Dim lblDescID As New Label
        Dim TRChecksdata As Integer = 0
        Try

            TRChecksdata = objUT.TRChecksdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlbranchName.SelectedValue, ddlcategory.SelectedIndex, ddlOthType.SelectedIndex)
            For i As Integer = 0 To dgGeneralSchedTemp.Rows.Count - 1
                LabelName = CType(dgGeneralSchedTemp.Rows(i).FindControl("LabelName"), Label)
                lblLessmonth = CType(dgGeneralSchedTemp.Rows(i).FindControl("lblLessmonth"), Label)
                lblmorethen6 = CType(dgGeneralSchedTemp.Rows(i).FindControl("lblmorethen6"), Label)
                lbl1year = CType(dgGeneralSchedTemp.Rows(i).FindControl("lbl1year"), Label)
                lbl2year = CType(dgGeneralSchedTemp.Rows(i).FindControl("lbl2year"), Label)
                lbl3year = CType(dgGeneralSchedTemp.Rows(i).FindControl("lbl3year"), Label)
                lblMorethen = CType(dgGeneralSchedTemp.Rows(i).FindControl("lblMorethen"), Label)
                lblTotalAmount = CType(dgGeneralSchedTemp.Rows(i).FindControl("lblTotalAmount"), Label)

                '        lblDescID.Text = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, "", ddlFinancialYearSch.SelectedValue, DdlbranchSchedTemp.SelectedValue)


                If String.IsNullOrEmpty(LabelName.Text) Then
                    LabelName.Text = "-"
                End If
                If String.IsNullOrEmpty(lblLessmonth.Text) Then
                    lblLessmonth.Text = "0"
                End If
                If String.IsNullOrEmpty(lblmorethen6.Text) Then
                    lblmorethen6.Text = "0"
                End If
                If String.IsNullOrEmpty(lbl1year.Text) Then
                    lbl1year.Text = "0"
                End If
                If String.IsNullOrEmpty(lbl2year.Text) Then
                    lbl2year.Text = "0"
                End If
                If String.IsNullOrEmpty(lbl3year.Text) Then
                    lbl3year.Text = "0"
                End If
                If String.IsNullOrEmpty(lblMorethen.Text) Then
                    lblMorethen.Text = "0"
                End If

                If String.IsNullOrEmpty(lblTotalAmount.Text) Then
                    lblTotalAmount.Text = "0"
                End If

                objUT.iATU_ID = 0
                objUT.iATU_CustId = ddlCustomerName.SelectedValue
                objUT.iATU_Category = ddlcategory.SelectedIndex
                objUT.iATU_OtherType = ddlOthType.SelectedIndex
                objUT.sATU_Name = LabelName.Text
                objUT.dATU_Less_than_six_Month = Double.Parse(lblLessmonth.Text)
                objUT.dATU_More_than_six_Month = Double.Parse(lblmorethen6.Text)
                objUT.dATU_One_Year = Double.Parse(lbl1year.Text)
                objUT.dATU_Two_Year = Double.Parse(lbl2year.Text)
                objUT.dATU_Three_Year = Double.Parse(lbl3year.Text)
                objUT.dATU_More_than = Double.Parse(lblMorethen.Text)
                objUT.dATU_Total_Amount = Double.Parse(lblTotalAmount.Text)
                objUT.iATU_CRBY = sSession.UserID
                objUT.iATU_YEARId = ddlFinancialYear.SelectedValue
                objUT.iATU_Branchname = ddlbranchName.SelectedValue
                objUT.iATU_UPDATEDBY = sSession.UserID
                objUT.sATU_IPAddress = sSession.IPAddress

                objUT.iATU_YEARId = ddlFinancialYear.SelectedValue
                Arr = objUT.SaveTradeExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
            Next
            lblError.Text = "Successfully Uploaded." : lblModalValidationMsg.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveTradeScheduleSchedTemp")
        End Try
    End Sub


    Private Sub ddlcategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlcategory.SelectedIndexChanged
        Dim Dataset As New DataSet

        Try
            If ddlbranchName.SelectedIndex = 0 Then
                lblError.Text = "Select Branch." : lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlcategory_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlFinancialYear.SelectedValue = ddlFinancialYear.SelectedValue
            sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
            Session("AllSession") = sSession
            If ddlCustomerName.SelectedIndex > 0 Then
                ddlCustomerName_SelectedIndexChanged(sender, e)
                ddlcategory.SelectedIndex = 0
                dgGeneralSchedTemp.Visible = False

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgGeneralSchedTemp_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles dgGeneralSchedTemp.RowDataBound
        Dim lblLessmonth As New Label
        Dim lblmorethen6 As New Label
        Dim lbl1year As New Label
        Dim lbl2year As New Label
        Dim lbl3year As New Label
        Dim lblMorethen As New Label
        Dim lblTotalAmount As New Label

        Dim lblTotalLessmonth As New Label
        Dim lblTotalmorethen6 As New Label
        Dim lblTotal1year As New Label
        Dim lblTotal2year As New Label
        Dim lblTotal3year As New Label
        Dim lblTotalMorethen As New Label
        Dim lblfinalTotalAmount As New Label
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                lblLessmonth = e.Row.FindControl("lblLessmonth")
                If lblLessmonth.Text <> "" Then
                    dLessmonth = dLessmonth + Convert.ToDecimal(lblLessmonth.Text)
                End If

                lblmorethen6 = e.Row.FindControl("lblmorethen6")
                If lblmorethen6.Text <> "" Then
                    dmorethen6 = dmorethen6 + Convert.ToDecimal(lblmorethen6.Text)
                End If

                lbl1year = e.Row.FindControl("lbl1year")
                If lbl1year.Text <> "" Then
                    d1year = d1year + Convert.ToDecimal(lbl1year.Text)
                End If

                lbl2year = e.Row.FindControl("lbl2year")
                If lbl2year.Text <> "" Then
                    d2year = d2year + Convert.ToDecimal(lbl2year.Text)
                End If
                lbl3year = e.Row.FindControl("lbl3year")
                If lbl3year.Text <> "" Then
                    d3year = d3year + Convert.ToDecimal(lbl3year.Text)
                End If


                lblMorethen = e.Row.FindControl("lblMorethen")
                If lblMorethen.Text <> "" Then
                    dMorethen = dMorethen + Convert.ToDecimal(lblMorethen.Text)
                End If
                lblTotalAmount = e.Row.FindControl("lblTotalAmount")
                If lblTotalAmount.Text <> "" Then
                    dTotalAmount = dTotalAmount + Convert.ToDecimal(lblTotalAmount.Text)
                End If

            End If

            If e.Row.RowType = DataControlRowType.Footer Then
                lblTotalLessmonth = e.Row.FindControl("lblTotalLessmonth")
                lblTotalLessmonth.Text = Convert.ToDecimal(dLessmonth).ToString("#,##0")

                lblTotalmorethen6 = e.Row.FindControl("lblTotalmorethen6")
                lblTotalmorethen6.Text = Convert.ToDecimal(dmorethen6).ToString("#,##0")

                lblTotal1year = e.Row.FindControl("lblTotal1year")
                lblTotal1year.Text = Convert.ToDecimal(d1year).ToString("#,##0")

                lblTotal2year = e.Row.FindControl("lblTotal2year")
                lblTotal2year.Text = Convert.ToDecimal(d2year).ToString("#,##0")

                lblTotal3year = e.Row.FindControl("lblTotal3year")
                lblTotal3year.Text = Convert.ToDecimal(d3year).ToString("#,##0")

                lblTotalMorethen = e.Row.FindControl("lblTotalMorethen")
                lblTotalMorethen.Text = Convert.ToDecimal(dMorethen).ToString("#,##0")

                lblfinalTotalAmount = e.Row.FindControl("lblfinalTotalAmount")
                lblfinalTotalAmount.Text = Convert.ToDecimal(dTotalAmount).ToString("#,##0")

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub lnkDownload_Click(sender As Object, e As EventArgs) Handles lnkDownload.Click
        Dim sPath As String = ""
        Try

            sPath = Server.MapPath("../") & "ExcelUploads\" & Regex.Replace("Tade receivables", "\s", "") & ".xlsx"

            DownloadFile(sPath)
        Catch ex As Exception

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
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt1 As DataTable
        Dim dt2 As DataTable
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                lblModalValidationMsg.Text = "Select Customer Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlbranchName.SelectedIndex = 0 Then
                lblModalValidationMsg.Text = "Select Branch Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            Dim dataset As DataSet = objUT.GetTotal(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlbranchName.SelectedValue)
            'Dim dtTrade As DataTable = objUT.GetCTrDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlcategory.SelectedIndex, ddlbranchName.SelectedValue, ddlOthType.SelectedIndex)
            dt1 = dataset.Tables(0)
            dt2 = dataset.Tables(1)
            If dt1.Rows.Count = 0 And dt2.Rows.Count = 0 Then
                lblModalValidationMsg.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            Else
                Dim rds As New ReportDataSource("DataSet1", dt1)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds1 As New ReportDataSource("DataSet2", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/RptTrade.rdlc")
                Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustomerName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Customer_Name)
                Dim Finance_year As ReportParameter() = New ReportParameter() {New ReportParameter("Finance_year", ddlFinancialYear.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Finance_year)
                Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", ddlbranchName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Branch_Name)
                Dim Category As ReportParameter() = New ReportParameter() {New ReportParameter("Category", ddlcategory.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Category)
            End If
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("pdf")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Tade receivables" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt1 As DataTable
        Dim dt2 As DataTable
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                lblModalValidationMsg.Text = "Select Customer Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlbranchName.SelectedIndex = 0 Then
                lblModalValidationMsg.Text = "Select Branch Name"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            Dim dataset As DataSet = objUT.GetTotal(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlbranchName.SelectedValue)
            'Dim dtTrade As DataTable = objUT.GetCTrDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlcategory.SelectedIndex, ddlbranchName.SelectedValue, ddlOthType.SelectedIndex)
            dt1 = dataset.Tables(0)
            dt2 = dataset.Tables(1)
            If dt1.Rows.Count = 0 And dt2.Rows.Count = 0 Then
                lblModalValidationMsg.Text = "No Data"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                Exit Sub
            Else
                Dim rds As New ReportDataSource("DataSet1", dt1)
                ReportViewer1.LocalReport.DataSources.Add(rds)
                Dim rds1 As New ReportDataSource("DataSet2", dt2)
                ReportViewer1.LocalReport.DataSources.Add(rds1)

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/RptTrade.rdlc")
                Dim Customer_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Customer_Name", ddlCustomerName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Customer_Name)
                Dim Finance_year As ReportParameter() = New ReportParameter() {New ReportParameter("Finance_year", ddlFinancialYear.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Finance_year)
                Dim Branch_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Branch_Name", ddlbranchName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Branch_Name)
                Dim Category As ReportParameter() = New ReportParameter() {New ReportParameter("Category", ddlcategory.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Category)
            End If
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=Tade receivables" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()

        Catch ex As Exception

        End Try
    End Sub



    Private Sub dgGeneralSchedTemp_DataBound(sender As Object, e As EventArgs) Handles dgGeneralSchedTemp.DataBound
        dgGeneralSchedTemp.Columns(8).ItemStyle.HorizontalAlign = HorizontalAlign.Right
    End Sub

    Private Sub ddlOthType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlOthType.SelectedIndexChanged
        Try
            If ddlcategory.SelectedIndex = 0 Then
                lblError.Text = "Select Branch." : lblModalValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            lblError.Text = ""
            If ddlOthType.SelectedIndex > 0 Then

                Dim dt As DataTable = objUT.GetCTrDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue, ddlcategory.SelectedIndex, ddlbranchName.SelectedValue, ddlOthType.SelectedIndex)


                If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then

                    dgGeneralSchedTemp.Visible = True
                    dgGeneralSchedTemp.DataSource = dt
                    dgGeneralSchedTemp.DataBind()
                Else
                    lblModalValidationMsg.Text = "No data found."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-info');$('#ModalValidation').modal('show');", True)
                    dgGeneralSchedTemp.Visible = False
                End If
            Else
                lblError.Text = "Select the Other Type "
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class