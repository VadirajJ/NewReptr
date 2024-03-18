Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports DatabaseLayer
Imports System.IO
Imports System.Web.Services.Description

Public Class AccountPolicies
    Inherits System.Web.UI.Page
    Private sFormName As String = "AccountPolicies"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGRACePermission As New clsGRACePermission
    Private objDBHelper As New DBHelper
    Private sSession As AllSession
    'Private Shared sSave As String
    Private Shared dtDisplay2 As New DataTable
    Private objAccountpolicies As New clsAccountpolicies
    Private objclsReportContentMaster As New clsReportContentMaster
    Dim objUT As New ClsUploadTailBal
    Dim objclsReport As New clsReport
    Dim objclsSchduleReport As New clsScheduleReport

    Private Shared ipkid As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then

                ' imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnReport.Visible = False

                'sSave = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MRTRCM", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        sSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If
                ipkid = 0
                LoadExistingCustomer()
                BindReportType() : BindYearMaster()
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustName.SelectedValue = sSession.CustomerID
                        ddlCustName_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustName.SelectedValue = sSession.CustomerID
                        If ddlCustName.SelectedIndex > 0 Then
                            ddlCustName_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
                'ReportContentClientSideValidation()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadExistingCustomer()
        Try
            ddlCustName.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataTextField = "Cust_Name"
            ddlCustName.DataValueField = "Cust_Id"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Public Sub BindReportType()
        Try
            ddlReportType.Items.Add(New ListItem("Select Report Type", "0"))
            ddlReportType.Items.Add(New ListItem("Report on the standalone Financial Statements", "1"))
            ddlReportType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindReportType" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub ReportContentClientSideValidation()
    '    Try
    '        RFVFunction.ErrorMessage = "Select Report Type." : RFVFunction.InitialValue = 0
    '        RFVHeading.ErrorMessage = "Enter Heading." : REVHeading.ValidationExpression = "^[\s\S]{0,500}$" : REVHeading.ErrorMessage = "Heading exceeded maximum size(max 500 character)."
    '        RFVDescription.ErrorMessage = "Enter Description." : REVDescription.ValidationExpression = "^[\s\S]{0,5000}$" : REVDescription.ErrorMessage = "Description exceeded maximum size(max 5000 character)."
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ReportContentClientSideValidation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub gvReportContentMaster_PreRender(sender As Object, e As EventArgs) Handles gvReportContentMaster.PreRender
        Dim dt As New DataTable
        Try
            If gvReportContentMaster.Rows.Count > 0 Then
                gvReportContentMaster.UseAccessibleHeader = True
                gvReportContentMaster.HeaderRow.TableSection = TableRowSection.TableHeader
                gvReportContentMaster.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReportContentMaster_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Masters/ReportContentMaster.aspx?"), False)
            dtDisplay2.Clear()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr As Array
        Try
            If ipkid = 0 Then
                objAccountpolicies.RCM_Id = 0
            Else
                objAccountpolicies.RCM_Id = ipkid
            End If

            Dim bCheck As Boolean = objAccountpolicies.CheckReportHeadingExisting(sSession.AccessCode, sSession.AccessCodeID, 1, ipkid, objclsGRACeGeneral.SafeSQL(txtEnterHeading.Text.Trim))
            If bCheck = True Then
                lblReportValidationMsg.Text = "Entered Heading already exist." : lblError.Text = "Entered Heading already exist."
                txtEnterHeading.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                Exit Sub
            End If

            objAccountpolicies.RCM_ReportName = ddlReportType.SelectedItem.Text

            If txtEnterHeading.Text = "" Then
                objAccountpolicies.RCM_Heading = ""
            Else
                objAccountpolicies.RCM_Heading = txtEnterHeading.Text
            End If
            If ddlReportType.SelectedValue = 0 Then
                objAccountpolicies.RCM_ReportId = 0
            Else
                objAccountpolicies.RCM_ReportId = ddlReportType.SelectedIndex
            End If
            If txtEnterDescription.Text = "" Then
                objAccountpolicies.RCM_Description = ""
            Else
                objAccountpolicies.RCM_Description = txtEnterDescription.Text
            End If
            objAccountpolicies.RCM_Delflag = "X"
            objAccountpolicies.RCM_Status = "A"
            objAccountpolicies.RCM_CrBy = sSession.UserID
            objAccountpolicies.RCM_CrOn = DateTime.Today
            objAccountpolicies.RCM_UpdatedBy = sSession.UserID
            objAccountpolicies.RCM_UpdatedOn = DateTime.Today
            objAccountpolicies.RCM_IPAddress = sSession.IPAddress
            objAccountpolicies.RCM_CompID = sSession.AccessCodeID
            objAccountpolicies.RCM_Yearid = sSession.YearID

            Arr = objAccountpolicies.SaveReportContentMaster(sSession.AccessCode, sSession.AccessCodeID, objAccountpolicies)
            ipkid = 0
            BindDetails(ddlReportType.SelectedIndex)

            If Arr(0) = "2" Then
                lblReportValidationMsg.Text = "Successfully Updated"
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content Master", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
            ElseIf Arr(0) = "3" Then
                lblReportValidationMsg.Text = "Successfully Saved"
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content Master", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
            End If
            txtEnterHeading.Text = "" : txtEnterDescription.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnsave_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019hrow
        End Try
    End Sub
    Private Sub BindDetails(ByVal iReportID As Integer)
        Try
            'dtDisplay2 = objAccountpolicies.BinALLDetailsExisting(sSession.AccessCode, sSession.AccessCodeID, iReportID, ddlCustName.SelectedValue, Ddlbranch.SelectedValue, ddlFinancialYear.SelectedValue)
            If dtDisplay2.Rows.Count = 0 Then
                dtDisplay2 = objAccountpolicies.BinALLDetails(sSession.AccessCode, sSession.AccessCodeID, iReportID)
            End If
            If dtDisplay2.Rows.Count > 0 Then
                gvReportContentMaster.DataSource = dtDisplay2
                gvReportContentMaster.DataBind()
            Else
                gvReportContentMaster.DataSource = Nothing
                gvReportContentMaster.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDetails" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try

    End Sub
    Private Sub gvReportContentMaster_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvReportContentMaster.RowDataBound

    End Sub
    Private Sub gvReportContentMaster_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles gvReportContentMaster.RowEditing

    End Sub
    Private Sub gvReportContentMaster_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvReportContentMaster.RowCommand
        Dim dt As New DataTable, dt1 As New DataTable, dtHeading As New DataTable
        Dim drNew As DataRow
        Dim lblReportType As New Label
        Dim lblPKID As New Label
        Dim lblDescription As New Label
        Dim lblHeading As New LinkButton
        Dim lblReportID As New Label
        Dim sColumnName As String = ""
        Try
            lblError.Text = ""
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblPKID = DirectCast(clickedRow.FindControl("lblPKID"), Label)
                ipkid = lblPKID.Text
                lblReportType = DirectCast(clickedRow.FindControl("lblReportType"), Label)
                lblReportID = DirectCast(clickedRow.FindControl("lblReportID"), Label)
                lblHeading = DirectCast(clickedRow.FindControl("lblHeading"), LinkButton)
                lblDescription = DirectCast(clickedRow.FindControl("lblDescription"), Label)

                txtEnterHeading.Enabled = True
                ddlReportType.SelectedValue = lblReportID.Text
                If (ddlReportType.SelectedIndex = 5 And (lblHeading.Text = "Responsibilities of the Auditor" Or lblHeading.Text = "The objective and scope of the audit" Or lblHeading.Text = "Reporting" Or lblHeading.Text = "General" Or
                    lblHeading.Text = "The responsibilities of management and identification of the applicable financial reporting framework" Or lblHeading.Text = "Non Disclosure Of Confidential Information")) Then
                    txtEnterHeading.Enabled = False
                End If
                txtEnterHeading.Text = lblHeading.Text
                txtEnterDescription.Text = lblDescription.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReportTemplate_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub ddlReportType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlReportType.SelectedIndexChanged
        Try
            lblError.Text = ""
            txtEnterDescription.Text = ""
            txtEnterHeading.Text = ""
            txtEnterHeading.Enabled = True
            BindDetails(ddlReportType.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlReportType_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dSubItemId As String
        Dim dt, dtcustdetails, dtcompanydetails, dtpartners, dtstatue As New DataTable
        Dim objDBL As New DBHelper
        Dim sOrgType As String

        Try
            If ddlReportType.SelectedIndex > 0 Then
                dt = objAccountpolicies.LoadReportContentToGrid(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedValue)
            End If
            dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue)
            dSubItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select CUSt_BranchId  from SAD_CUSTOMER_MASTER where CUST_Id=" & ddlCustName.SelectedValue & "")
            dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue)
            dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)

            sOrgType = objclsSchduleReport.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue)
            If sOrgType = "Partnership firms" Then
                dtstatue = objclsSchduleReport.Loadpartner1(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, "", "AccPoli")
            Else
                dtstatue = objclsSchduleReport.LoadDirector1(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, "", "AccPoli")
            End If
            If dt.Rows.Count = 0 Then
                lblReportValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalReportValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dtstatue)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/AccountingPolicies.rdlc")
            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Company_name", dtcustdetails.Rows(0)("CUST_NAME").ToString)}
            ReportViewer1.LocalReport.SetParameters(Comp_Name)
            Dim CinNum As ReportParameter() = New ReportParameter() {New ReportParameter("CIN_Number", "abcd")}
            ReportViewer1.LocalReport.SetParameters(CinNum)
            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
            ReportViewer1.LocalReport.SetParameters(Company_Address)
            Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
            ReportViewer1.LocalReport.SetParameters(companydetailsName)
            Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
            ReportViewer1.LocalReport.SetParameters(Company_Code)
            Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "PARTNER")}
            ReportViewer1.LocalReport.SetParameters(Partners_Name)
            Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
            ReportViewer1.LocalReport.SetParameters(CompRegNo)
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FinStatements", "Accounting Policies", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=Accounting Policies" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dSubItemId As String
        Dim dt, dtcustdetails, dtcompanydetails, dtpartners, dtstatue As New DataTable
        Dim objDBL As New DBHelper
        Dim sOrgType As String

        Try
            If ddlReportType.SelectedIndex > 0 Then
                dt = objAccountpolicies.LoadReportContentToGrid(sSession.AccessCode, sSession.AccessCodeID, ddlReportType.SelectedValue)
            End If
            dtcustdetails = objclsSchduleReport.LoadCustomerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue)
            dSubItemId = objDBL.SQLExecuteScalar(sSession.AccessCode, " select CUSt_BranchId  from SAD_CUSTOMER_MASTER where CUST_Id=" & ddlCustName.SelectedValue & "")
            dtcompanydetails = objclsSchduleReport.LoadCompanydetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue)
            dtpartners = objclsSchduleReport.LoadCustomerpartners(sSession.AccessCode, sSession.AccessCodeID, 0)

            sOrgType = objclsSchduleReport.LoadOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue)
            If sOrgType = "Partnership firms" Then
                dtstatue = objclsSchduleReport.Loadpartner1(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, "", "AccPoli")
            Else
                dtstatue = objclsSchduleReport.LoadDirector1(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue, "", "AccPoli")
            End If
            If dt.Rows.Count = 0 Then
                lblReportValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalReportValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            Dim rds1 As New ReportDataSource("DataSet2", dtstatue)
            ReportViewer1.LocalReport.DataSources.Add(rds1)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/AccountingPolicies.rdlc")
            Dim Comp_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Company_name", dtcustdetails.Rows(0)("CUST_NAME").ToString)}
            ReportViewer1.LocalReport.SetParameters(Comp_Name)
            Dim CinNum As ReportParameter() = New ReportParameter() {New ReportParameter("CIN_Number", "abcd")}
            ReportViewer1.LocalReport.SetParameters(CinNum)
            Dim Company_Address As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Address", dtcustdetails.Rows(0)("CUST_COMM_ADDRESS").ToString)}
            ReportViewer1.LocalReport.SetParameters(Company_Address)
            Dim companydetailsName As ReportParameter() = New ReportParameter() {New ReportParameter("companydetailsName", dtcompanydetails.Rows(0)("Company_Name").ToString)}
            ReportViewer1.LocalReport.SetParameters(companydetailsName)
            Dim Company_Code As ReportParameter() = New ReportParameter() {New ReportParameter("Company_Code", dtcompanydetails.Rows(0)("Company_Code").ToString)}
            ReportViewer1.LocalReport.SetParameters(Company_Code)
            Dim Partners_Name As ReportParameter() = New ReportParameter() {New ReportParameter("Partners_Name", "PARTNER")}
            ReportViewer1.LocalReport.SetParameters(Partners_Name)
            Dim CompRegNo As ReportParameter() = New ReportParameter() {New ReportParameter("CompRegNo", dSubItemId)}
            ReportViewer1.LocalReport.SetParameters(CompRegNo)
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "FinStatements", "Accounting Policies", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=Accounting Policies" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim dtbranch As New DataTable
        Try
            If ddlCustName.SelectedIndex > 0 Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustName.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                sSession.CustomerID = ddlCustName.SelectedValue
                ddlCustName.SelectedValue = sSession.CustomerID
                Session("AllSession") = sSession
                'lbllmtcomp.Visible = True
                'lblNonlmtcomp.Visible = True
                'lbllmtcomp.Visible = True
                'lblNonlmtcomp.Visible = True
                'chklmtcomp.Visible = True
                'chkNonlmtcomp.Visible = True


                dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtbranch.Rows.Count > 0 Then
                    Ddlbranch.DataSource = dtbranch
                    Ddlbranch.DataTextField = "BranchName"
                    Ddlbranch.DataValueField = "Branchid"
                    Ddlbranch.DataBind()
                    Ddlbranch.Items.Insert(0, "Select Branch Name")
                    If sSession.ScheduleBranchId <> 0 Then
                        Ddlbranch.SelectedValue = sSession.ScheduleBranchId
                        Ddlbranch_SelectedIndexChanged(sender, e)
                    Else
                        sSession.ScheduleBranchId = 0
                    End If
                    Session("AllSession") = sSession
                Else
                    lblReportValidationMsg.Text = "Customer should have atleast one Branch, Please add"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    lblError.Text = lblReportValidationMsg.Text
                    Ddlbranch.DataSource = Nothing
                    Ddlbranch.DataTextField = "BranchName"
                    Ddlbranch.DataValueField = "Branchid"
                    Ddlbranch.DataBind()
                    Ddlbranch.Items.Insert(0, "Select Branch Name")
                    Exit Sub
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub Ddlbranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Ddlbranch.SelectedIndexChanged
        Try
            BindDetails(1)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub BtnAddDetails_Click(sender As Object, e As EventArgs) Handles BtnAddDetails.Click
        Dim dtDisplay As New DataTable
        Dim i As Integer = 0
        Dim dRow As DataRow
        Dim Arr As Array
        Try
            lblError.Text = ""
            If ipkid = 0 Then
                If ddlReportType.SelectedIndex = 0 Then
                    lblError.Text = "Select Report Type."
                    lblReportValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    ddlReportType.Focus()
                    Exit Sub
                End If

                If txtEnterHeading.Text = "" Then
                    lblError.Text = "Enter Heading."
                    lblReportValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    txtEnterHeading.Focus()
                    Exit Sub
                End If

                Dim bCheck As Boolean = objAccountpolicies.CheckReportHeadingExisting(sSession.AccessCode, sSession.AccessCodeID, 1, ipkid, objclsGRACeGeneral.SafeSQL(txtEnterHeading.Text.Trim))
                If bCheck = True Then
                    lblReportValidationMsg.Text = "Entered Heading already exist." : lblError.Text = "Entered Heading already exist."
                    txtEnterHeading.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    Exit Sub
                End If

                If txtEnterDescription.Text = "" Then
                    lblError.Text = "Enter Description."
                    lblReportValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    txtEnterDescription.Focus()
                    Exit Sub
                End If

                dtDisplay.Columns.Add("SrNo")
                dtDisplay.Columns.Add("ReportType")
                dtDisplay.Columns.Add("Heading")
                dtDisplay.Columns.Add("PKID")
                dtDisplay.Columns.Add("ReportID")
                dtDisplay.Columns.Add("Description")

                dRow = dtDisplay.NewRow
                dRow("SrNo") = i + 1
                dRow("ReportType") = ddlReportType.SelectedItem.Text
                dRow("Heading") = txtEnterHeading.Text
                dRow("PKID") = 0
                dRow("ReportID") = ddlReportType.SelectedIndex
                dRow("Description") = Replace(txtEnterDescription.Text, " ", "")
                dtDisplay.Rows.Add(dRow)

                dtDisplay2.Merge(dtDisplay)
                gvReportContentMaster.DataSource = dtDisplay2
                gvReportContentMaster.DataBind()
            Else
                If ipkid = 0 Then
                    objAccountpolicies.RCM_Id = 0
                Else
                    objAccountpolicies.RCM_Id = ipkid
                End If

                Dim bCheck As Boolean = objAccountpolicies.CheckReportHeadingExisting(sSession.AccessCode, sSession.AccessCodeID, 1, ipkid, objclsGRACeGeneral.SafeSQL(txtEnterHeading.Text.Trim))
                If bCheck = True Then
                    lblReportValidationMsg.Text = "Entered Heading already exist." : lblError.Text = "Entered Heading already exist."
                    txtEnterHeading.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                    Exit Sub
                End If

                objAccountpolicies.RCM_ReportName = ddlReportType.SelectedItem.Text

                If txtEnterHeading.Text = "" Then
                    objAccountpolicies.RCM_Heading = ""
                Else
                    objAccountpolicies.RCM_Heading = txtEnterHeading.Text
                End If
                If ddlReportType.SelectedValue = 0 Then
                    objAccountpolicies.RCM_ReportId = 0
                Else
                    objAccountpolicies.RCM_ReportId = ddlReportType.SelectedIndex
                End If
                If txtEnterDescription.Text = "" Then
                    objAccountpolicies.RCM_Description = ""
                Else
                    objAccountpolicies.RCM_Description = txtEnterDescription.Text
                End If
                objAccountpolicies.RCM_Delflag = "X"
                objAccountpolicies.RCM_Status = "A"
                objAccountpolicies.RCM_CrBy = sSession.UserID
                objAccountpolicies.RCM_CrOn = DateTime.Today
                objAccountpolicies.RCM_UpdatedBy = sSession.UserID
                objAccountpolicies.RCM_UpdatedOn = DateTime.Today
                objAccountpolicies.RCM_IPAddress = sSession.IPAddress
                objAccountpolicies.RCM_CompID = sSession.AccessCodeID
                objAccountpolicies.RCM_Yearid = sSession.YearID

                Arr = objAccountpolicies.SaveReportContentMaster(sSession.AccessCode, sSession.AccessCodeID, objAccountpolicies)
                ipkid = 0
                BindDetails(ddlReportType.SelectedIndex)

                If Arr(0) = "2" Then
                    lblReportValidationMsg.Text = "Successfully Updated"
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content Master", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                ElseIf Arr(0) = "3" Then
                    lblReportValidationMsg.Text = "Successfully Saved"
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Report Content Master", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalReportValidation').modal('show');", True)
                End If
                txtEnterHeading.Text = "" : txtEnterDescription.Text = ""
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnAddDetails_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class