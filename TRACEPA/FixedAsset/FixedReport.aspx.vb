Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms

Public Class FixedReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "FXAPhysicalReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objPhyReport As New ClsFXAPhysicalReport
    Private Shared sSession As AllSession
    Dim objAsstTrn As New ClsAssetTransactionAddition
    Private objAsst As New ClsAssetMaster
    Dim objclsSchduleReport As New clsScheduleReport
    Dim objDynReport As New ClsFXADynamicReport
    Private objLocationSetup As New ClsLocationSetup
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            sSession = Session("AllSession")
            If IsPostBack = False Then
                ' BindYearMaster()
                LoadCustomer()
                LoadFinalcialYear(sSession.AccessCode)

                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        LoadLocation()
                        BindLocation() : loadAssetType()
                        BindLocationDynamic()
                        BindLocationInv()
                        loadAssetTypeInv()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYear(sAC, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.ScheduleYearId = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            If sSession.CustomerID <> 0 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    lblModal.Text = "Do you wish to change Customer?Click Yes to change."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                If ddlCustomerName.SelectedIndex > 0 Then
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    'BtnYES_Click(sender, e)

                End If
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

    Public Function LoadLocation() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objAsst.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objAsst.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            lstLocation.DataSource = dt
            lstLocation.DataTextField = "LS_Description"
            lstLocation.DataValueField = "LS_ID"
            lstLocation.DataBind()

            'lstLocation.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Function BindLocationDynamic() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            lstLocDynamic.DataSource = dt
            lstLocDynamic.DataTextField = "LS_Description"
            lstLocDynamic.DataValueField = "LS_ID"
            lstLocDynamic.DataBind()
            'lstLocation.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    'Public Sub BindYearMaster()
    '    Try
    '        ddlYear.DataSource = objPhyReport.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlYear.DataTextField = "Year"
    '        ddlYear.DataValueField = "YMS_ID"
    '        ddlYear.DataBind()
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlReportType.SelectedIndex = 1 Then
                lblLocation.Visible = True
                lstLocation.Visible = True
            ElseIf ddlReportType.SelectedIndex = 2 Then
                lblLocation.Visible = False
                lstLocation.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReportType_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub btnGoReport_Click(sender As Object, e As EventArgs) Handles btnGoReport.Click
        Dim dt, dt2 As New DataTable
        Dim iSelectedLocation As Integer
        Dim sSelectedProcess As String = ""
        Dim iSelectedL As Integer
        Dim sSelectedL As String = ""
        Dim sAddress As String = "", spincode As String = "", smob As String = "", sContactNo As String = ""
        Dim sEmail As String = "", swebsite As String = "", sCity As String = ""
        Dim dtCA As New DataTable
        Dim dtCusAmtType As DataTable
        Dim iAmtType As Integer = 0
        Dim iRoundOff As Integer = 0
        Try
            'For i = 0 To lstLocation.Items.Count - 1
            '    If lstLocation.Items(i).Selected = True Then
            '        iSelectedLocation = iSelectedLocation + 1
            '    End If
            'Next
            'If iSelectedLocation = 0 Then
            '    lblReportValidation.Text = "Select Location." : lblError.Text = "Select Location."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
            '    lstLocation.Focus()
            '    Exit Sub
            'End If
            'If sSession.AUDSchedule > 0 Then
            '    LoadAssessment()
            'End If
            If ddlReportType.SelectedIndex = 1 Then
                For i = 0 To lstLocation.Items.Count - 1
                    If lstLocation.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedProcess = sSelectedProcess & "," & lstLocation.Items(i).Value
                    End If
                Next
                If iSelectedLocation = 0 Then
                    lblReportValidation.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                    lstLocation.Focus()
                    Exit Sub
                End If
                'For i = 0 To lstProcess.Items.Count - 1
                '    If lstProcess.Items(i).Selected = True Then
                '        sSelectedProcess = sSelectedProcess & "," & lstProcess.Items(i).Value
                '    End If
                'Next
                If sSelectedProcess.StartsWith(",") Then
                    sSelectedProcess = sSelectedProcess.Remove(0, 1)
                End If
                If sSelectedProcess.EndsWith(",") Then
                    sSelectedProcess = sSelectedProcess.Remove(Len(sSelectedProcess) - 1, 1)
                End If

                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If

                dt = objPhyReport.LoadComnyAct(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "", 0, ddlCustomerName.SelectedValue, sSelectedProcess, iAmtType, iRoundOff)

                dtCA = objPhyReport.GetCompanyNameCity(sSession.AccessCode, sSession.AccessCodeID)

                sAddress = dtCA.Rows(0).Item("Company_Address")
                sCity = dtCA.Rows(0).Item("Company_City")
                spincode = dtCA.Rows(0).Item("Company_PinCode")
                smob = dtCA.Rows(0).Item("Company_MobileNo")
                sContactNo = dtCA.Rows(0).Item("Company_TelephoneNo")
                sEmail = dtCA.Rows(0).Item("Company_EmailID")
                swebsite = dtCA.Rows(0).Item("Company_WebSite")
                'sRegno = dt.Rows(0).Item("Company_Code")

                For i = 0 To lstLocation.Items.Count - 1
                    If lstLocation.Items(i).Selected = True Then
                        iSelectedL = iSelectedL + 1
                        sSelectedL = sSelectedL & "," & lstLocation.Items(i).Text
                    End If
                Next
                If sSelectedL.StartsWith(",") Then
                    sSelectedL = sSelectedL.Remove(0, 1)
                End If
                If sSelectedL.EndsWith(",") Then
                    sSelectedL = sSelectedL.Remove(Len(sSelectedL) - 1, 1)
                End If


                ReportViewer1.Reset()
                If dt.Rows.Count = 0 Then
                    ReportViewer1.LocalReport.Refresh()
                Else
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAssetReport.rdlc")
                    ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                    ReportViewer1.ZoomPercent = 125
                    ReportViewer1.LocalReport.Refresh()

                    Dim FY As ReportParameter() = New ReportParameter() {New ReportParameter("FY", sSession.YearName)}
                    ReportViewer1.LocalReport.SetParameters(FY)

                    Dim sAYear As String = objPhyReport.LoadAssesmentYear(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID)

                    'Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
                    Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", sAYear)}
                    ReportViewer1.LocalReport.SetParameters(AY)

                    Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(CustomerName)

                    Dim Address As ReportParameter() = New ReportParameter() {New ReportParameter("Address", sAddress + " " + sCity + " " + spincode)}
                    ReportViewer1.LocalReport.SetParameters(Address)

                    Dim Mob As ReportParameter() = New ReportParameter() {New ReportParameter("Mob", "Mob :" + smob + "/" + sContactNo + " " + "Email :" + sEmail + "/" + swebsite)}
                    ReportViewer1.LocalReport.SetParameters(Mob)

                    Dim NoteNo As ReportParameter() = New ReportParameter() {New ReportParameter("NoteNo", txtNoteNumber.Text)}
                    ReportViewer1.LocalReport.SetParameters(NoteNo)

                    Dim Location As ReportParameter() = New ReportParameter() {New ReportParameter("Location", sSelectedL)}
                    ReportViewer1.LocalReport.SetParameters(Location)
                End If

            ElseIf ddlReportType.SelectedIndex = 2 Then
                Dim sYear As String
                Dim Startdate As String
                Dim Enddate As String
                Dim sYear1 As String
                Dim ayear As Array
                ayear = ddlFinancialYear.SelectedItem.Text.Split("-")
                sYear1 = Trim(ayear(1))
                sYear = Trim(ayear(0))
                Dim Yearly As Integer
                Startdate = Date.ParseExact("01/04/" & sYear, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture) ' Yearly 
                Enddate = Date.ParseExact("31/03/" & sYear1, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                dt2 = objPhyReport.LoadITDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, "", 0, ddlCustomerName.SelectedValue)

                dtCA = objPhyReport.GetCompanyNameCity(sSession.AccessCode, sSession.AccessCodeID)

                sAddress = dtCA.Rows(0).Item("Company_Address")
                sCity = dtCA.Rows(0).Item("Company_City")
                spincode = dtCA.Rows(0).Item("Company_PinCode")
                smob = dtCA.Rows(0).Item("Company_MobileNo")
                sContactNo = dtCA.Rows(0).Item("Company_TelephoneNo")
                sEmail = dtCA.Rows(0).Item("Company_EmailID")
                swebsite = dtCA.Rows(0).Item("Company_WebSite")
                'sRegno = dt.Rows(0).Item("Company_Code")

                ReportViewer1.Reset()
                If dt2.Rows.Count = 0 Then
                    ReportViewer1.LocalReport.Refresh()
                Else
                    Dim rds As New ReportDataSource("DataSet1", dt2)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/AssetITReport.rdlc")
                    ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                    ReportViewer1.ZoomPercent = 125
                    ReportViewer1.LocalReport.Refresh()

                    'Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", sSession.YearName)}
                    'ReportViewer1.LocalReport.SetParameters(AnnualYear)

                    'Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
                    'ReportViewer1.LocalReport.SetParameters(AY)

                    Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(CustomerName)

                    'Dim Address As ReportParameter() = New ReportParameter() {New ReportParameter("Address", sAddress + " " + sCity + " " + spincode)}
                    'ReportViewer1.LocalReport.SetParameters(Address)

                    'Dim Mob As ReportParameter() = New ReportParameter() {New ReportParameter("Mob", "Mob :" + smob + "/" + sContactNo + " " + "Email :" + sEmail + "/" + swebsite)}
                    'ReportViewer1.LocalReport.SetParameters(Mob)

                    Dim NoteNo As ReportParameter() = New ReportParameter() {New ReportParameter("NoteNo", txtNoteNumber.Text)}
                    ReportViewer1.LocalReport.SetParameters(NoteNo)

                    Dim start As ReportParameter() = New ReportParameter() {New ReportParameter("start", Startdate)}
                    ReportViewer1.LocalReport.SetParameters(start)
                    Dim End1 As ReportParameter() = New ReportParameter() {New ReportParameter("End1", Enddate)}
                    ReportViewer1.LocalReport.SetParameters(End1)
                End If
            End If
            'Dim paramtr(1) As ReportParameter
            'paramtr(0) = New ReportParameter("sYear", "123")
            'paramtr(1) = New ReportParameter("sYEDate", "123")
            'ReportViewer1.LocalReport.SetParameters(paramtr)
            'ReportViewer1.LocalReport.Refresh()

            'Dim sYear As ReportParameter() = {New ReportParameter("sYear", "123")}
            'ReportViewer1.LocalReport.SetParameters(sYear)

            'Dim ToDate As String = objPhyReport.Loadtodate(sSession.AccessCode, sSession.AccessCodeID, ddlYear.SelectedValue)

            'Dim sYEDate As ReportParameter() = {New ReportParameter("sYEDate", "123")}
            'ReportViewer1.LocalReport.SetParameters(sYEDate)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGo_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            If ddlFinancialYear.SelectedIndex > 0 Then
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
                ReportViewer1.Reset()
                ReportViewer1.LocalReport.Refresh()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Public Function BindLocationInv() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            ddlLocatn.DataSource = dt
            ddlLocatn.DataTextField = "LS_Description"
            ddlLocatn.DataValueField = "LS_ID"
            ddlLocatn.DataBind()
            ddlLocatn.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub ddlLocatn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocatn.SelectedIndexChanged
        'If ddlLocatn.SelectedIndex > 0 Then
        '    loadDepartments(ddlLocatn.SelectedValue)
        'End If
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlLocatn.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, ddlLocatn.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlDivision.DataSource = dt
            ddlDivision.DataTextField = "LS_Description"
            ddlDivision.DataValueField = "LS_ID"
            ddlDivision.DataBind()
            ddlDivision.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLocatn_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivision.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDivision.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, ddlDivision.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If
            ddlDeptmnt.DataSource = dt
            ddlDeptmnt.DataTextField = "LS_Description"
            ddlDeptmnt.DataValueField = "LS_ID"
            ddlDeptmnt.DataBind()
            ddlDeptmnt.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub loadAssetTypeInv()
        Dim dtAsset As New DataTable
        Try
            dtAsset = objPhyReport.loadAssetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            ddlpAstypeInv.DataTextField = "AM_Description"
            ddlpAstypeInv.DataValueField = "AM_ID"
            ddlpAstypeInv.DataSource = dtAsset
            ddlpAstypeInv.DataBind()
            ddlpAstypeInv.Items.Insert(0, "All Asset Class")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlDeptmnt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeptmnt.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDeptmnt.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, ddlDeptmnt.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlBay.DataSource = dt
            ddlBay.DataTextField = "LS_Description"
            ddlBay.DataValueField = "LS_ID"
            ddlBay.DataBind()
            ddlBay.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDeptmnt_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlpAstypeInv_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlpAstypeInv.SelectedIndexChanged
        Try
            loadExistingItemCode()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlpAstype_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub loadExistingItemCode()
        Dim ilocation, idepartment, idevision, ibay As New Integer
        Try
            If ddlLocatn.SelectedIndex > 0 Then
                ilocation = ddlLocatn.SelectedValue
            Else
                ilocation = 0
            End If
            If ddlDivision.SelectedIndex > 0 Then
                idevision = ddlDivision.SelectedValue
            End If
            If ddlDeptmnt.SelectedIndex > 0 Then
                idepartment = ddlDeptmnt.SelectedValue
            End If
            If ddlBay.SelectedIndex > 0 Then
                ibay = ddlBay.SelectedValue
            End If
            ddlAssetItem.DataSource = objAsstTrn.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, ddlpAstypeInv.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ilocation, idevision, idepartment, ibay)
            ddlAssetItem.DataTextField = "AFAM_ItemDescription"
            ddlAssetItem.DataValueField = "AFAM_ID"
            ddlAssetItem.DataBind()
            ddlAssetItem.Items.Insert(0, "Select Asset")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingItemCode" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindLocation() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            lstLocation.DataSource = dt
            lstLocation.DataTextField = "LS_Description"
            lstLocation.DataValueField = "LS_ID"
            lstLocation.DataBind()
            'lstLocation.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub loadAssetType()
        Dim dtAsset As New DataTable
        Try
            dtAsset = objPhyReport.loadAssetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            ddlpAstype.DataTextField = "AM_Description"
            ddlpAstype.DataValueField = "AM_ID"
            ddlpAstype.DataSource = dtAsset
            ddlpAstype.DataBind()
            ddlpAstype.Items.Insert(0, "All Asset Class")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub lstLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLocation.SelectedIndexChanged
    '    Try
    '        Dim iSelectedLocation As Integer
    '        Dim sSelectedProcessL As String = ""

    '        Dim dt As New DataTable
    '        Dim sStatus As String = ""
    '        Try
    '            lblError.Text = ""

    '            'Location
    '            For i = 0 To lstLocation.Items.Count - 1
    '                If lstLocation.Items(i).Selected = True Then
    '                    iSelectedLocation = iSelectedLocation + 1
    '                    sSelectedProcessL = sSelectedProcessL & "," & lstLocation.Items(i).Value
    '                End If
    '            Next
    '            If iSelectedLocation = 0 Then
    '                lblReportValidation.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
    '                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
    '                lstLocation.Focus()
    '                Exit Sub
    '            End If

    '            If sSelectedProcessL.StartsWith(",") Then
    '                sSelectedProcessL = sSelectedProcessL.Remove(0, 1)
    '            End If
    '            If sSelectedProcessL.EndsWith(",") Then
    '                sSelectedProcessL = sSelectedProcessL.Remove(Len(sSelectedProcessL) - 1, 1)
    '            End If


    '            If lstLocation.SelectedIndex > 0 Then
    '                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, sSelectedProcessL, ddlCustomerName.SelectedValue)
    '            Else
    '                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
    '            End If

    '            If dt.Rows.Count > 0 Then
    '                lstDivision.DataSource = dt
    '                lstDivision.DataTextField = "LS_Description"
    '                lstDivision.DataValueField = "LS_ID"
    '                lstDivision.DataBind()
    '            Else
    '                lstDivision.Items.Clear()
    '            End If

    '            lstDepartment.Items.Clear()
    '            lstBay.Items.Clear()

    '        Catch ex As Exception
    '            Throw
    '        End Try
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstLocation_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub lstDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDivision.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Dim sStatus As String = ""

    '    Dim iSelectedDivision As Integer
    '    Dim sSelectedProcessD As String = ""

    '    Try
    '        lblError.Text = ""

    '        'Division

    '        For i = 0 To lstDivision.Items.Count - 1
    '            If lstDivision.Items(i).Selected = True Then
    '                iSelectedDivision = iSelectedDivision + 1
    '                sSelectedProcessD = sSelectedProcessD & "," & lstDivision.Items(i).Value
    '            End If
    '        Next
    '        If iSelectedDivision = 0 Then
    '            lblReportValidation.Text = "No data. Select Division." : lblError.Text = "No data. Select Division."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
    '            lstLocation.Focus()
    '            Exit Sub
    '        End If

    '        If sSelectedProcessD.StartsWith(",") Then
    '            sSelectedProcessD = sSelectedProcessD.Remove(0, 1)
    '        End If
    '        If sSelectedProcessD.EndsWith(",") Then
    '            sSelectedProcessD = sSelectedProcessD.Remove(Len(sSelectedProcessD) - 1, 1)
    '        End If


    '        If lstDivision.SelectedIndex > 0 Then
    '            dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, sSelectedProcessD, ddlCustomerName.SelectedValue)
    '        Else
    '            dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
    '        End If

    '        If dt.Rows.Count > 0 Then
    '            lstDepartment.DataSource = dt
    '            lstDepartment.DataTextField = "LS_Description"
    '            lstDepartment.DataValueField = "LS_ID"
    '            lstDepartment.DataBind()
    '        Else
    '            lstDepartment.Items.Clear()
    '        End If

    '        'lstDepartment.Items.Insert(0, "Select ")

    '        lstBay.Items.Clear()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub lstDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDepartment.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Dim sStatus As String = ""

    '    Dim iSelectedDepartment As Integer
    '    Dim sSelectedProcessDep As String = ""

    '    Try
    '        lblError.Text = ""

    '        'department

    '        For i = 0 To lstDepartment.Items.Count - 1
    '            If lstDepartment.Items(i).Selected = True Then
    '                iSelectedDepartment = iSelectedDepartment + 1
    '                sSelectedProcessDep = sSelectedProcessDep & "," & lstDepartment.Items(i).Value
    '            End If
    '        Next
    '        If iSelectedDepartment = 0 Then
    '            lblReportValidation.Text = "No data. Select Department." : lblError.Text = "No data. Select Department."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
    '            lstLocation.Focus()
    '            Exit Sub
    '        End If

    '        If sSelectedProcessDep.StartsWith(",") Then
    '            sSelectedProcessDep = sSelectedProcessDep.Remove(0, 1)
    '        End If
    '        If sSelectedProcessDep.EndsWith(",") Then
    '            sSelectedProcessDep = sSelectedProcessDep.Remove(Len(sSelectedProcessDep) - 1, 1)
    '        End If


    '        If lstDepartment.SelectedIndex = 0 Then
    '            dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, sSelectedProcessDep, ddlCustomerName.SelectedValue)
    '        Else
    '            dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
    '        End If

    '        If dt.Rows.Count > 0 Then
    '            lstBay.DataSource = dt
    '            lstBay.DataTextField = "LS_Description"
    '            lstBay.DataValueField = "LS_ID"
    '            lstBay.DataBind()
    '        Else
    '            lstBay.Items.Clear()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDepartment_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub BtnYES_Click(sender As Object, e As EventArgs) Handles btnYes.Click
    '    Try
    '        If ddlCustomerName.SelectedIndex > 0 Then
    '            sSession.CustomerID = ddlCustomerName.SelectedValue
    '            Session("AllSession") = sSession
    '            BindLocation() : loadAssetType()
    '            BindLocationInv() : loadAssetTypeInv()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            lblError.Text = ""
            If sSession.CustomerID <> 0 Then
                ddlCustomerName.SelectedValue = sSession.CustomerID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnGODynamic_Click(sender As Object, e As EventArgs) Handles btnGoDynamic.Click
        Dim dt, dt2 As New DataTable
        'Dim iCustId, iLocationId, iDivId, iDeptId, iBayId, iAsstCls As Integer
        Dim iCustId, iAsstCls As Integer

        Dim iSelectedLocation As Integer
        Dim sSelectedProcessL As String = ""

        Dim iSelectedDivision As Integer
        Dim sSelectedProcessD As String = ""

        Dim iSelectedDepartment As Integer
        Dim sSelectedProcessDep As String = ""

        Dim iSelectedBay As Integer
        Dim sSelectedProcessBay As String = ""

        Dim dtCusAmtType As DataTable
        Dim iAmtType As Integer = 0
        Dim iRoundOff As Integer = 0

        Try
            If ddlCustomerName.SelectedIndex > 0 Then

                iCustId = ddlCustomerName.SelectedValue

                'Location
                For i = 0 To lstLocDynamic.Items.Count - 1
                    If lstLocDynamic.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedProcessL = sSelectedProcessL & "," & lstLocDynamic.Items(i).Value
                    End If
                Next
                If iSelectedLocation = 0 Then
                    lblReportValidation.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                    lstLocDynamic.Focus()
                    Exit Sub
                End If

                If sSelectedProcessL.StartsWith(",") Then
                    sSelectedProcessL = sSelectedProcessL.Remove(0, 1)
                End If
                If sSelectedProcessL.EndsWith(",") Then
                    sSelectedProcessL = sSelectedProcessL.Remove(Len(sSelectedProcessL) - 1, 1)
                End If

                'Division

                For i = 0 To lstDivision.Items.Count - 1
                    If lstDivision.Items(i).Selected = True Then
                        iSelectedDivision = iSelectedDivision + 1
                        sSelectedProcessD = sSelectedProcessD & "," & lstDivision.Items(i).Value
                    End If
                Next

                If sSelectedProcessD.StartsWith(",") Then
                    sSelectedProcessD = sSelectedProcessD.Remove(0, 1)
                End If
                If sSelectedProcessD.EndsWith(",") Then
                    sSelectedProcessD = sSelectedProcessD.Remove(Len(sSelectedProcessD) - 1, 1)
                End If

                'department

                For i = 0 To lstDepartment.Items.Count - 1
                    If lstDepartment.Items(i).Selected = True Then
                        iSelectedDepartment = iSelectedDepartment + 1
                        sSelectedProcessDep = sSelectedProcessDep & "," & lstDepartment.Items(i).Value
                    End If
                Next

                If sSelectedProcessDep.StartsWith(",") Then
                    sSelectedProcessDep = sSelectedProcessDep.Remove(0, 1)
                End If
                If sSelectedProcessDep.EndsWith(",") Then
                    sSelectedProcessDep = sSelectedProcessDep.Remove(Len(sSelectedProcessDep) - 1, 1)
                End If

                'Bay

                For i = 0 To lstBay.Items.Count - 1
                    If lstBay.Items(i).Selected = True Then
                        iSelectedBay = iSelectedBay + 1
                        sSelectedProcessBay = sSelectedProcessBay & "," & lstBay.Items(i).Value
                    End If
                Next

                If sSelectedProcessBay.StartsWith(",") Then
                    sSelectedProcessBay = sSelectedProcessBay.Remove(0, 1)
                End If
                If sSelectedProcessBay.EndsWith(",") Then
                    sSelectedProcessBay = sSelectedProcessBay.Remove(Len(sSelectedProcessD) - 1, 1)
                End If

                If ddlpAstype.SelectedIndex > 0 Then
                    iAsstCls = ddlpAstype.SelectedValue
                Else
                    iAsstCls = 0
                End If

                'If ChkbxDistinguish.Checked = False Then
                '    dt = objPhyReport.LoadComnyAct1(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iAsstCls)
                '    ReportViewer1.Reset()
                '    If dt.Rows.Count = 0 Then
                '        ReportViewer1.LocalReport.Refresh()
                '    Else
                '        Dim rds As New ReportDataSource("DataSet1", dt)
                '        ReportViewer1.LocalReport.DataSources.Add(rds)
                '        ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAssetReport.rdlc")
                '        ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                '        ReportViewer1.ZoomPercent = 125
                '        ReportViewer1.LocalReport.Refresh()
                '        Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", sSession.YearName)}
                '        ReportViewer1.LocalReport.SetParameters(AnnualYear)
                '        Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                '        ReportViewer1.LocalReport.SetParameters(CustomerName)

                '    End If
                'Else

                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If

                dt = objPhyReport.LoadDynComnyDetailedAct(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, sSelectedProcessL, sSelectedProcessD, sSelectedProcessDep, sSelectedProcessBay, iAsstCls, ddlTransType.SelectedIndex, iAmtType, iRoundOff)
                ReportViewer2.Reset()
                If dt.Rows.Count = 0 Then
                    ReportViewer2.LocalReport.Refresh()
                Else
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer2.LocalReport.DataSources.Add(rds)
                    ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAssetReportDetailed.rdlc")
                    ReportViewer2.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                    ReportViewer2.ZoomPercent = 125
                    ReportViewer2.LocalReport.Refresh()
                    Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", ddlFinancialYear.SelectedItem.Text)}
                    ReportViewer2.LocalReport.SetParameters(AnnualYear)
                    Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
                    ReportViewer2.LocalReport.SetParameters(AY)
                    Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                    ReportViewer2.LocalReport.SetParameters(CustomerName)

                End If
            End If
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGO_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub ddlpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlpAstype.SelectedIndexChanged
    '    Try
    '        loadExistingItemCode()
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlpAstype_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub loadExistingItemCode()
    '    Dim ilocation, idepartment, idevision, ibay As New Integer
    '    Try
    '        If ddlLocatn.SelectedIndex > 0 Then
    '            ilocation = ddlLocatn.SelectedValue
    '        Else
    '            ilocation = 0
    '        End If
    '        If ddlDivision.SelectedIndex > 0 Then
    '            idevision = ddlDivision.SelectedValue
    '        End If
    '        If ddlDeptmnt.SelectedIndex > 0 Then
    '            idepartment = ddlDeptmnt.SelectedValue
    '        End If
    '        If ddlBay.SelectedIndex > 0 Then
    '            ibay = ddlBay.SelectedValue
    '        End If
    '        ddlAssetItem.DataSource = objAsstTrn.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, ddlpAstype.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ilocation, idevision, idepartment, ibay)
    '        ddlAssetItem.DataTextField = "AFAM_ItemDescription"
    '        ddlAssetItem.DataValueField = "AFAM_ID"
    '        ddlAssetItem.DataBind()
    '        ddlAssetItem.Items.Insert(0, "Select Asset")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingItemCode" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub ddlLocatn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocatn.SelectedIndexChanged
    '    'If ddlLocatn.SelectedIndex > 0 Then
    '    '    loadDepartments(ddlLocatn.SelectedValue)
    '    'End If
    '    Dim dt As New DataTable
    '    Dim sStatus As String = ""
    '    Try
    '        lblError.Text = ""
    '        If ddlLocatn.SelectedIndex > 0 Then
    '            dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, ddlLocatn.SelectedValue, ddlCustomerName.SelectedValue)
    '        Else
    '            dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
    '        End If

    '        ddlDivision.DataSource = dt
    '        ddlDivision.DataTextField = "LS_Description"
    '        ddlDivision.DataValueField = "LS_ID"
    '        ddlDivision.DataBind()
    '        ddlDivision.Items.Insert(0, "Select ")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLocatn_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub ddlDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivision.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Dim sStatus As String = ""
    '    Try
    '        lblError.Text = ""
    '        If ddlDivision.SelectedIndex > 0 Then
    '            dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, ddlDivision.SelectedValue, ddlCustomerName.SelectedValue)
    '        Else
    '            dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
    '        End If
    '        ddlDeptmnt.DataSource = dt
    '        ddlDeptmnt.DataTextField = "LS_Description"
    '        ddlDeptmnt.DataValueField = "LS_ID"
    '        ddlDeptmnt.DataBind()
    '        ddlDeptmnt.Items.Insert(0, "Select ")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub ddlDeptmnt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeptmnt.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Dim sStatus As String = ""
    '    Try
    '        lblError.Text = ""
    '        If ddlDeptmnt.SelectedIndex = 0 Then
    '            dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, ddlDeptmnt.SelectedValue, ddlCustomerName.SelectedValue)
    '        Else
    '            dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
    '        End If

    '        ddlBay.DataSource = dt
    '        ddlBay.DataTextField = "LS_Description"
    '        ddlBay.DataValueField = "LS_ID"
    '        ddlBay.DataBind()
    '        ddlBay.Items.Insert(0, "Select ")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDeptmnt_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Public Function BindLocationInv() As DataTable
    '    Dim dt As New DataTable
    '    Dim sStatus As String = ""
    '    Try
    '        lblError.Text = ""
    '        If ddlCustomerName.SelectedIndex = 0 Then
    '            dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
    '        Else
    '            dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '        End If
    '        ddlLocatn.DataSource = dt
    '        ddlLocatn.DataTextField = "LS_Description"
    '        ddlLocatn.DataValueField = "LS_ID"
    '        ddlLocatn.DataBind()
    '        ddlLocatn.Items.Insert(0, "Select ")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Function
    'Private Sub btnGOInv_Click(sender As Object, e As EventArgs) Handles btnGOInv.Click
    '    Dim dt, dt2 As New DataTable
    '    Dim iCustId, iLocationId, iDivId, iDeptId, iBayId, iAsstCls, iAsstItem, iTrandId As Integer
    '    Dim sAsstCls As String = ""
    '    Dim dtCusAmtType As DataTable
    '    Dim iAmtType As Integer = 0
    '    Dim iRoundOff As Integer = 0
    '    Try
    '        If ddlCustomerName.SelectedIndex > 0 Then
    '            iCustId = ddlCustomerName.SelectedValue
    '            If ddlLocatn.SelectedIndex > 0 Then
    '                iLocationId = ddlLocatn.SelectedValue
    '            Else
    '                iLocationId = 0
    '            End If
    '            If ddlDivision.SelectedIndex > 0 Then
    '                iDivId = ddlDivision.SelectedValue
    '            Else
    '                iDivId = 0
    '            End If
    '            If ddlDeptmnt.SelectedIndex > 0 Then
    '                iDeptId = ddlDeptmnt.SelectedValue
    '            Else
    '                iDeptId = 0
    '            End If
    '            If ddlBay.SelectedIndex > 0 Then
    '                iBayId = ddlBay.SelectedValue
    '            Else
    '                iBayId = 0
    '            End If


    '            If ddlpAstype.SelectedIndex > 0 Then
    '                sAsstCls = ddlpAstype.SelectedItem.Text
    '                iAsstCls = ddlpAstype.SelectedValue
    '            Else
    '                sAsstCls = "0"
    '                iAsstCls = 0
    '            End If

    '            If ddlAssetItem.SelectedIndex > 0 Then
    '                iAsstItem = ddlAssetItem.SelectedValue
    '            Else
    '                iAsstItem = 0
    '            End If

    '            If ddlTransType.SelectedIndex > 0 Then
    '                iTrandId = ddlTransType.SelectedIndex
    '            Else
    '                iTrandId = 0
    '            End If

    '            dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '            If (dtCusAmtType.Rows.Count > 0) Then
    '                iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
    '                iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
    '            Else
    '                iAmtType = 0
    '                iRoundOff = 0
    '            End If

    '            'dt = objPhyReport.LoadInvDetailed(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, ddlpAstype.SelectedItem.Text, iAsstItem, iTrandId, ddlAmountConvert.SelectedValue)
    '            'dt2 = objPhyReport.LoadInvAddition(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, ddlpAstype.SelectedItem.Text, iAsstItem, iTrandId)

    '            dt = objPhyReport.LoadInvDetailedNew(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, sAsstCls, iAsstCls, iAsstItem, iTrandId, iAmtType, iRoundOff)
    '            dt2 = objPhyReport.LoadInvAdditionNew(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, iAsstCls, iAsstItem, iTrandId)
    '            ReportViewer3.Reset()
    '            If dt.Rows.Count = 0 Then
    '                ReportViewer3.LocalReport.Refresh()
    '            Else
    '                Dim rds As New ReportDataSource("DataSet1", dt)
    '                ReportViewer3.LocalReport.DataSources.Add(rds)
    '                If dt2.Rows.Count > 0 Then
    '                Else

    '                End If
    '                Dim rds1 As New ReportDataSource("DataSet2", dt2)
    '                ReportViewer3.LocalReport.DataSources.Add(rds1)
    '                ReportViewer3.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAsstInvReport.rdlc")
    '                ReportViewer3.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
    '                ReportViewer3.ZoomPercent = 125
    '                ReportViewer3.LocalReport.Refresh()
    '                Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", sSession.YearName)}
    '                ReportViewer3.LocalReport.SetParameters(AnnualYear)
    '                Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
    '                ReportViewer3.LocalReport.SetParameters(AY)
    '                Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
    '                ReportViewer3.LocalReport.SetParameters(CustomerName)
    '            End If

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGO_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Public Sub loadAssetTypeInv()
    '    Dim dtAsset As New DataTable
    '    Try
    '        dtAsset = objPhyReport.loadAssetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
    '        ddlpAstype.DataTextField = "AM_Description"
    '        ddlpAstype.DataValueField = "AM_ID"
    '        ddlpAstype.DataSource = dtAsset
    '        ddlpAstype.DataBind()
    '        ddlpAstype.Items.Insert(0, "All Asset Class")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub

    Private Sub lnkbtnEmpBasicDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpBasicDetails.Click
        Try
            liEmpBasic.Attributes.Add("class", "active")
            liEmpMaster.Attributes.Remove("class")
            liEmpDetails.Attributes.Remove("class")
            divEmpBasic.Attributes.Add("class", "tab-pane active")
            divEmpMaster.Attributes.Add("class", "tab-pane")
            divEmpDetails.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmpBasicDetails_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnEmpMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpMaster.Click
        Try

            liEmpBasic.Attributes.Remove("class")
            liEmpMaster.Attributes.Add("class", "active")
            liEmpDetails.Attributes.Remove("class")

            divEmpBasic.Attributes.Add("class", "tab-pane")
            divEmpMaster.Attributes.Add("class", "tab-pane active")
            divEmpDetails.Attributes.Add("class", "tab-pane")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmpMaster_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnEmpDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpDetails.Click
        Try

            liEmpBasic.Attributes.Remove("class")
            liEmpMaster.Attributes.Remove("class")
            liEmpDetails.Attributes.Add("class", "active")

            divEmpBasic.Attributes.Add("class", "tab-pane")
            divEmpMaster.Attributes.Add("class", "tab-pane")
            divEmpDetails.Attributes.Add("class", "tab-pane active")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmpDetails_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Private Sub btnInv_Click(sender As Object, e As EventArgs) Handles btnInv.Click
        Dim dt, dt2 As New DataTable
        Dim iCustId, iLocationId, iDivId, iDeptId, iBayId, iAsstCls, iAsstItem, iTrandId As Integer
        Dim sAsstCls As String = ""
        Dim dtCusAmtType As DataTable
        Dim iAmtType As Integer = 0
        Dim iRoundOff As Integer = 0
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustId = ddlCustomerName.SelectedValue
                If ddlLocatn.SelectedIndex > 0 Then
                    iLocationId = ddlLocatn.SelectedValue
                Else
                    iLocationId = 0
                End If
                If ddlDivision.SelectedIndex > 0 Then
                    iDivId = ddlDivision.SelectedValue
                Else
                    iDivId = 0
                End If
                If ddlDeptmnt.SelectedIndex > 0 Then
                    iDeptId = ddlDeptmnt.SelectedValue
                Else
                    iDeptId = 0
                End If
                If ddlBay.SelectedIndex > 0 Then
                    iBayId = ddlBay.SelectedValue
                Else
                    iBayId = 0
                End If


                If ddlpAstypeInv.SelectedIndex > 0 Then
                    sAsstCls = ddlpAstypeInv.SelectedItem.Text
                    iAsstCls = ddlpAstypeInv.SelectedValue
                Else
                    sAsstCls = "0"
                    iAsstCls = 0
                End If

                If ddlAssetItem.SelectedIndex > 0 Then
                    iAsstItem = ddlAssetItem.SelectedValue
                Else
                    iAsstItem = 0
                End If

                If ddlTransType.SelectedIndex > 0 Then
                    iTrandId = ddlTransType.SelectedIndex
                Else
                    iTrandId = 0
                End If

                dtCusAmtType = objclsSchduleReport.GetCustomerAmountType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
                If (dtCusAmtType.Rows.Count > 0) Then
                    iAmtType = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_Amount_Type").ToString())
                    iRoundOff = Convert.ToInt32(dtCusAmtType.Rows(0)("CUST_RoundOff").ToString())
                Else
                    iAmtType = 0
                    iRoundOff = 0
                End If

                'dt = objPhyReport.LoadInvDetailed(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, ddlpAstype.SelectedItem.Text, iAsstItem, iTrandId, ddlAmountConvert.SelectedValue)
                'dt2 = objPhyReport.LoadInvAddition(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, ddlpAstype.SelectedItem.Text, iAsstItem, iTrandId)

                dt = objPhyReport.LoadInvDetailedNew(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, sAsstCls, iAsstCls, iAsstItem, iTrandId, iAmtType, iRoundOff)
                dt2 = objPhyReport.LoadInvAdditionNew(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, iLocationId, iDivId, iDeptId, iBayId, iAsstCls, iAsstItem, iTrandId)
                ReportViewer3.Reset()
                If dt.Rows.Count = 0 Then
                    ReportViewer3.LocalReport.Refresh()
                Else
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer3.LocalReport.DataSources.Add(rds)
                    If dt2.Rows.Count > 0 Then
                    Else

                    End If
                    Dim rds1 As New ReportDataSource("DataSet2", dt2)
                    ReportViewer3.LocalReport.DataSources.Add(rds1)
                    ReportViewer3.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAsstInvReport.rdlc")
                    ReportViewer3.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                    ReportViewer3.ZoomPercent = 125
                    ReportViewer3.LocalReport.Refresh()
                    Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", sSession.YearName)}
                    ReportViewer3.LocalReport.SetParameters(AnnualYear)
                    Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
                    ReportViewer3.LocalReport.SetParameters(AY)
                    Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                    ReportViewer3.LocalReport.SetParameters(CustomerName)
                End If

            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGO_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub lstLocDynamic_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLocDynamic.SelectedIndexChanged
        Try
            Dim iSelectedLocation As Integer
            Dim sSelectedProcessL As String = ""

            Dim dt As New DataTable
            Dim sStatus As String = ""
            Try
                lblError.Text = ""

                'Location
                For i = 0 To lstLocDynamic.Items.Count - 1
                    If lstLocDynamic.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedProcessL = sSelectedProcessL & "," & lstLocDynamic.Items(i).Value
                    End If
                Next
                If iSelectedLocation = 0 Then
                    lblReportValidation.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                    lstLocDynamic.Focus()
                    Exit Sub
                End If

                If sSelectedProcessL.StartsWith(",") Then
                    sSelectedProcessL = sSelectedProcessL.Remove(0, 1)
                End If
                If sSelectedProcessL.EndsWith(",") Then
                    sSelectedProcessL = sSelectedProcessL.Remove(Len(sSelectedProcessL) - 1, 1)
                End If


                If lstLocDynamic.SelectedIndex > 0 Then
                    dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, sSelectedProcessL, ddlCustomerName.SelectedValue)
                Else
                    dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
                End If

                If dt.Rows.Count > 0 Then
                    lstDivision.DataSource = dt
                    lstDivision.DataTextField = "LS_Description"
                    lstDivision.DataValueField = "LS_ID"
                    lstDivision.DataBind()
                Else
                    lstDivision.Items.Clear()
                End If

                lstDepartment.Items.Clear()
                lstBay.Items.Clear()

            Catch ex As Exception
                Throw
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstLocation_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lstDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDivision.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""

        Dim iSelectedDivision As Integer
        Dim sSelectedProcessD As String = ""

        Try
            lblError.Text = ""

            'Division

            For i = 0 To lstDivision.Items.Count - 1
                If lstDivision.Items(i).Selected = True Then
                    iSelectedDivision = iSelectedDivision + 1
                    sSelectedProcessD = sSelectedProcessD & "," & lstDivision.Items(i).Value
                End If
            Next
            If iSelectedDivision = 0 Then
                lblReportValidation.Text = "No data. Select Division." : lblError.Text = "No data. Select Division."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                lstLocation.Focus()
                Exit Sub
            End If

            If sSelectedProcessD.StartsWith(",") Then
                sSelectedProcessD = sSelectedProcessD.Remove(0, 1)
            End If
            If sSelectedProcessD.EndsWith(",") Then
                sSelectedProcessD = sSelectedProcessD.Remove(Len(sSelectedProcessD) - 1, 1)
            End If


            If lstDivision.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, sSelectedProcessD, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            If dt.Rows.Count > 0 Then
                lstDepartment.DataSource = dt
                lstDepartment.DataTextField = "LS_Description"
                lstDepartment.DataValueField = "LS_ID"
                lstDepartment.DataBind()
            Else
                lstDepartment.Items.Clear()
            End If

            'lstDepartment.Items.Insert(0, "Select ")

            lstBay.Items.Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lstDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstDepartment.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""

        Dim iSelectedDepartment As Integer
        Dim sSelectedProcessDep As String = ""

        Try
            lblError.Text = ""

            'department

            For i = 0 To lstDepartment.Items.Count - 1
                If lstDepartment.Items(i).Selected = True Then
                    iSelectedDepartment = iSelectedDepartment + 1
                    sSelectedProcessDep = sSelectedProcessDep & "," & lstDepartment.Items(i).Value
                End If
            Next
            If iSelectedDepartment = 0 Then
                lblReportValidation.Text = "No data. Select Department." : lblError.Text = "No data. Select Department."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                lstLocation.Focus()
                Exit Sub
            End If

            If sSelectedProcessDep.StartsWith(",") Then
                sSelectedProcessDep = sSelectedProcessDep.Remove(0, 1)
            End If
            If sSelectedProcessDep.EndsWith(",") Then
                sSelectedProcessDep = sSelectedProcessDep.Remove(Len(sSelectedProcessDep) - 1, 1)
            End If


            If lstDepartment.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, sSelectedProcessDep, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            If dt.Rows.Count > 0 Then
                lstBay.DataSource = dt
                lstBay.DataTextField = "LS_Description"
                lstBay.DataValueField = "LS_ID"
                lstBay.DataBind()
            Else
                lstBay.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstDepartment_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

End Class