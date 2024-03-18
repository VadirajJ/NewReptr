Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Partial Class FXAPhysicalReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "FXAPhysicalReport"
    'Private Shared sFormName As String = "FixedAsset\FXAPhysicalReport.aspx"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objPhyReport As New ClsFXAPhysicalReport
    Private Shared sSession As AllSession
    Private objAsst As New ClsAssetMaster
    Dim objclsSchduleReport As New clsScheduleReport
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
                    BtnYES_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BtnYES_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Try
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    LoadLocation()
                Catch ex As Exception

                End Try
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
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

    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
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
    'Private Sub ddlYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlYear.SelectedIndexChanged
    '    Dim dt, dt2 As New DataTable
    '    Try
    '        If ddlReportType.SelectedIndex = 1 Then
    '            dt = objPhyReport.LoadCADetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlYear.SelectedItem.Text, ddlYear.SelectedValue)
    '            ReportViewer1.Reset()
    '            If dt.Rows.Count = 0 Then
    '                ReportViewer1.LocalReport.Refresh()
    '            Else
    '                Dim rds As New ReportDataSource("DataSet1", dt)
    '                ReportViewer1.LocalReport.DataSources.Add(rds)
    '                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/FixedAssetCAReport.rdlc")
    '                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
    '                ReportViewer1.ZoomPercent = 125
    '                ReportViewer1.LocalReport.Refresh()
    '            End If

    '        ElseIf ddlReportType.SelectedIndex = 2 Then
    '            dt2 = objPhyReport.LoadITDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlYear.SelectedItem.Text, ddlYear.SelectedValue, ddlCustomerName.SelectedValue)
    '            ReportViewer1.Reset()
    '            If dt2.Rows.Count = 0 Then
    '                ReportViewer1.LocalReport.Refresh()
    '            Else
    '                Dim rds As New ReportDataSource("DataSet1", dt2)
    '                ReportViewer1.LocalReport.DataSources.Add(rds)
    '                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/rdlc/fixedasset/AssetITReport.rdlc")
    '                ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
    '                ReportViewer1.ZoomPercent = 125
    '                ReportViewer1.LocalReport.Refresh()
    '            End If
    '        End If


    '        'Dim paramtr(1) As ReportParameter
    '        'paramtr(0) = New ReportParameter("sYear", "123")
    '        'paramtr(1) = New ReportParameter("sYEDate", "123")
    '        'ReportViewer1.LocalReport.SetParameters(paramtr)
    '        'ReportViewer1.LocalReport.Refresh()

    '        'Dim sYear As ReportParameter() = {New ReportParameter("sYear", "123")}
    '        'ReportViewer1.LocalReport.SetParameters(sYear)

    '        'Dim ToDate As String = objPhyReport.Loadtodate(sSession.AccessCode, sSession.AccessCodeID, ddlYear.SelectedValue)

    '        'Dim sYEDate As ReportParameter() = {New ReportParameter("sYEDate", "123")}
    '        'ReportViewer1.LocalReport.SetParameters(sYEDate)

    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
End Class
