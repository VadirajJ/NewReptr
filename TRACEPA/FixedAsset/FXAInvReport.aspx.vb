Imports System.Data
Imports BusinesLayer
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Public Class FXAInvReport
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "IndReport"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objDynReport As New ClsFXADynamicReport
    Private Shared sSession As AllSession
    Private objLocationSetup As New ClsLocationSetup
    Private objAsst As New ClsAssetMaster
    Dim objPhyReport As New ClsFXAPhysicalReport
    Dim objAsstTrn As New ClsAssetTransactionAddition
    Dim objclsSchduleReport As New clsScheduleReport
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
    Private Sub ddlpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlpAstype.SelectedIndexChanged
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
            ddlAssetItem.DataSource = objAsstTrn.ExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, ddlpAstype.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue, ilocation, idevision, idepartment, ibay)
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


                If ddlpAstype.SelectedIndex > 0 Then
                    sAsstCls = ddlpAstype.SelectedItem.Text
                    iAsstCls = ddlpAstype.SelectedValue
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
                ReportViewer1.Reset()
                If dt.Rows.Count = 0 Then
                    ReportViewer1.LocalReport.Refresh()
                Else
                    Dim rds As New ReportDataSource("DataSet1", dt)
                    ReportViewer1.LocalReport.DataSources.Add(rds)
                    If dt2.Rows.Count > 0 Then
                    Else

                    End If
                    Dim rds1 As New ReportDataSource("DataSet2", dt2)
                    ReportViewer1.LocalReport.DataSources.Add(rds1)
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FixedAsset/FixedAsstInvReport.rdlc")
                    ReportViewer1.ZoomMode = Microsoft.Reporting.WebForms.ZoomMode.Percent
                    ReportViewer1.ZoomPercent = 125
                    ReportViewer1.LocalReport.Refresh()
                    Dim AnnualYear As ReportParameter() = New ReportParameter() {New ReportParameter("AnnualYear", sSession.YearName)}
                    ReportViewer1.LocalReport.SetParameters(AnnualYear)
                    Dim AY As ReportParameter() = New ReportParameter() {New ReportParameter("AY", ddlFinancialYear.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(AY)
                    Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustomerName.SelectedItem.Text)}
                    ReportViewer1.LocalReport.SetParameters(CustomerName)
                End If

            End If
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

        End Try
    End Sub
End Class