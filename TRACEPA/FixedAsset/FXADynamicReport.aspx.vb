Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Partial Class FXADynamicReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "FXADynamicReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objDynReport As New ClsFXADynamicReport
    Private Shared sSession As AllSession
    Private objLocationSetup As New ClsLocationSetup
    Private objAsst As New ClsAssetMaster
    Dim objPhyReport As New ClsFXAPhysicalReport
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
                BindYearMaster()
                LoadCustomer()
                LoadFinalcialYear(sSession.AccessCode)
                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        BindLocation() : loadAssetType()
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
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYears(sAC, sSession.AccessCodeID)
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
                sSession.CustomerID = ddlCustomerName.SelectedValue
                Session("AllSession") = sSession
                BindLocation() : loadAssetType()
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
    Public Sub lstLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstLocation.SelectedIndexChanged
        Try
            Dim iSelectedLocation As Integer
            Dim sSelectedProcessL As String = ""

            Dim dt As New DataTable
            Dim sStatus As String = ""
            Try
                lblError.Text = ""

                'Location
                For i = 0 To lstLocation.Items.Count - 1
                    If lstLocation.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedProcessL = sSelectedProcessL & "," & lstLocation.Items(i).Value
                    End If
                Next
                If iSelectedLocation = 0 Then
                    lblReportValidation.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                    lstLocation.Focus()
                    Exit Sub
                End If

                If sSelectedProcessL.StartsWith(",") Then
                    sSelectedProcessL = sSelectedProcessL.Remove(0, 1)
                End If
                If sSelectedProcessL.EndsWith(",") Then
                    sSelectedProcessL = sSelectedProcessL.Remove(Len(sSelectedProcessL) - 1, 1)
                End If


                If lstLocation.SelectedIndex > 0 Then
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
    Public Sub BindYearMaster()
        Try
            ddlYear.DataSource = objPhyReport.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlYear.DataTextField = "Year"
            ddlYear.DataValueField = "YMS_ID"
            ddlYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnGO_Click(sender As Object, e As EventArgs) Handles btnGO.Click
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
                For i = 0 To lstLocation.Items.Count - 1
                    If lstLocation.Items(i).Selected = True Then
                        iSelectedLocation = iSelectedLocation + 1
                        sSelectedProcessL = sSelectedProcessL & "," & lstLocation.Items(i).Value
                    End If
                Next
                If iSelectedLocation = 0 Then
                    lblReportValidation.Text = "No data. Select Location." : lblError.Text = "No data. Select Location."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalReportValidation').modal('show');", True)
                    lstLocation.Focus()
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
                ReportViewer1.Reset()
                If dt.Rows.Count = 0 Then
                    ReportViewer1.LocalReport.Refresh()
                Else
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAssetReportDetailed.rdlc")
                    ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                    ReportViewer1.ZoomPercent = 125
                    ReportViewer1.LocalReport.Refresh()
                    Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", ddlFinancialYear.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(AnnualYear)
                    Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(AY)
                    Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(CustomerName)

                End If
            End If
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGO_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub ChkbxDistinguish_CheckedChanged(sender As Object, e As EventArgs) Handles ChkbxDistinguish.CheckedChanged
    '    Try
    '        If ChkbxDistinguish.Checked = True Then
    '            pnlTrans.Enabled = True : pnlLocation.Visible = True
    '        Else
    '            pnlTrans.Enabled = False : ddlTransType.SelectedIndex = 0
    '            pnlLocation.Visible = False
    '        End If
    '        If ddlCustomerName.SelectedIndex > 0 Then
    '            '   btnGO_Click(sender, e)
    '        Else
    '            lblError.Text = "Select Customer" : ChkbxDistinguish.Checked = False
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub
End Class
