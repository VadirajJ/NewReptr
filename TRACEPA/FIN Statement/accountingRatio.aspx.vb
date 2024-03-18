Imports System
Imports System.Data
Imports System.IO
Imports BusinesLayer
Imports System.Net.Mail
Imports DatabaseLayer
Imports Microsoft.Office.Interop
Imports ExcelDataReader
Imports Microsoft.Reporting.WebForms
Public Class accountingRatio
    Inherits System.Web.UI.Page
    Private sFormName As String = "accountingRatio"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsUSEntry As New clsUploadStockEntry
    Dim objUT As New ClsUploadTailBal
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
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Private Sub gvAccRatio_PreRender(sender As Object, e As EventArgs) Handles gvAccRatio.PreRender
        Try
            If gvAccRatio.Rows.Count > 0 Then
                gvAccRatio.UseAccessibleHeader = True
                gvAccRatio.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAccRatio.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAccRatio_PreRender")
        End Try

    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim lblslno As New Label, lblSglDes As New Label, lblOBDebit As New Label, lblOBCredit As New Label, lblTrDebit As New Label, lblTrCredit As New Label, lblCBDebit As New Label, lblCBCredit As New Label
        Dim lblglTot As New Label, lblsgTt As New Label, lblGroupTot As New Label, lblHeadTot As New Label, lblGroup As New Label, lblHead As New Label
        Dim dtbranch As New DataTable
        Dim sOrgType As String = ""
        Dim dtRatios As New DataTable
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustomerName.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                If ddlFinancialYear.SelectedIndex > 0 Then

                    dtRatios = objclsAccRatios.LoadAccRatio(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    gvAccRatio.DataSource = dtRatios
                    gvAccRatio.DataBind()

                    dtRatios = objclsAccRatios.LoadAccRatioFormula(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    gvAccRatioFormula.DataSource = dtRatios
                    gvAccRatioFormula.DataBind()

                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    Session("AllSession") = sSession
                    sOrgType = objUT.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)

                    dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                    If dtbranch.Rows.Count > 0 Then
                        ddlbranchName.DataSource = dtbranch
                        ddlbranchName.DataTextField = "BranchName"
                        ddlbranchName.DataValueField = "Branchid"
                        ddlbranchName.DataBind()
                        ddlbranchName.Items.Insert(0, "Select Branch Name")

                        If sSession.ScheduleBranchId <> 0 Then
                            ddlbranchName.SelectedValue = sSession.ScheduleBranchId
                            'ddlbranchName_SelectedIndexChanged(sender, e)
                        Else
                            sSession.ScheduleBranchId = 0
                        End If
                        Session("AllSession") = sSession
                    Else
                        lblExcelValidationMsg.Text = "Customer should have atleast one Branch, Please add"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                        lblError.Text = lblExcelValidationMsg.Text
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
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("UploadTrailbalanceSchedule.aspx?"), False)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Dim dtRatios As New DataTable
        Try
            dtRatios = objclsAccRatios.LoadAccRatio(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            gvAccRatio.DataSource = dtRatios
            gvAccRatio.DataBind()

            dtRatios = objclsAccRatios.LoadAccRatioFormula(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            gvAccRatioFormula.DataSource = dtRatios
            gvAccRatioFormula.DataBind()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim dtRatios As New DataTable
        Dim dtRatios1 As New DataTable
        Dim mimeType As String = Nothing
        Try
            dtRatios = objclsAccRatios.LoadAccRatio(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            dtRatios1 = objclsAccRatios.LoadAccRatioFormula(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtRatios)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dtRatios1)
            ReportViewer1.LocalReport.DataSources.Add(rds1)


            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/AccountingRatio.rdlc")
            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)
            Dim Year As ReportParameter() = New ReportParameter() {New ReportParameter("Year", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Year)
            If ddlbranchName.SelectedIndex = 1 Then
                Dim Branch As ReportParameter() = New ReportParameter() {New ReportParameter("Branch", ddlbranchName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Branch)
            Else
                Dim Branch As ReportParameter() = New ReportParameter() {New ReportParameter("Branch", "None")}
                ReportViewer1.LocalReport.SetParameters(Branch)

            End If


            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AccRatio" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim dtRatios As New DataTable
        Dim dtRatios1 As New DataTable
        Dim mimeType As String = Nothing
        Try
            dtRatios = objclsAccRatios.LoadAccRatio(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
            dtRatios1 = objclsAccRatios.LoadAccRatioFormula(sSession.AccessCode, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtRatios)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dtRatios1)
            ReportViewer1.LocalReport.DataSources.Add(rds1)


            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/AccountingRatio.rdlc")
            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomerName.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)
            Dim Year As ReportParameter() = New ReportParameter() {New ReportParameter("Year", ddlFinancialYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Year)

            If ddlbranchName.SelectedIndex = 1 Then
                Dim Branch As ReportParameter() = New ReportParameter() {New ReportParameter("Branch", ddlbranchName.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Branch)
            Else
                Dim Branch As ReportParameter() = New ReportParameter() {New ReportParameter("Branch", "None")}
                ReportViewer1.LocalReport.SetParameters(Branch)

            End If

            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=AccRatio" + ".xls")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception

        End Try
    End Sub
End Class