Imports BusinesLayer
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.UI.ScriptManager
Imports System.Drawing.FontStyle
Imports System.EnterpriseServices
Imports System.Security.Policy
Imports WebGrease
Imports System.Net.Http
Imports Newtonsoft.Json.Linq


Public Class CashFlow
    Inherits System.Web.UI.Page
    Private Shared sFormName As String = "Cashflow"
    Dim sSession As New AllSession
    Dim objclsReport As New clsReport
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Dim objclsOpeningBalance As New clsOpeningBalance
    Dim objgenfunc As New clsGeneralFunctions
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleNote As New clsScheduleNote
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsCashFlow As New clsCashFlow
    Public Shared strarray As Array = {(0)}
    Public Shared dt1 As New DataTable, dt3 As New DataTable, dt4 As New DataTable, dt5 As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'BindCompanytype()
                LoadExistingCustomerSchedTemp()
                BindYearMaster()
                'LoadCountriesData()
                strarray = New String() {sSession.AccessCodeID}
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                If sSession.CustomerID <> 0 Then
                    ddlCustomers.SelectedValue = sSession.CustomerID
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustomers.SelectedValue = sSession.CustomerID
                        ddlCustomers_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustomers.SelectedValue = sSession.CustomerID
                        If ddlCustomers.SelectedIndex > 0 Then
                            ddlCustomers_SelectedIndexChanged(sender, e)
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
    Public Sub LoadExistingCustomerSchedTemp()
        Try
            ddlCustomers.DataSource = objUT.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomers.DataTextField = "Cust_Name"
            ddlCustomers.DataValueField = "Cust_Id"
            ddlCustomers.DataBind()
            ddlCustomers.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Private Sub ddlCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomers.SelectedIndexChanged
        Dim dt As New DataTable, dtbranch As New DataTable
        Dim lblParticulars, lblpkid As New Label
        Dim TotaladjustmentsCurrent As Decimal = 0
        Dim TotaladjustmentsPrev As Decimal = 0
        Dim TotalManualCurrent As Decimal = 0
        Dim CurrentAmmount As Decimal
        Dim TotalManualPrev As Decimal = 0
        Dim PrevAmount As Decimal
        Dim TotalCurrentOperativeActivities As Decimal = 0
        Dim TotalPrevOperativeActivities As Decimal = 0
        Dim TotalCurrentInvestingActivities As Decimal = 0
        Dim TotalPrevInvestingActivities As Decimal = 0
        Dim TotalCurrentFinanceActivities As Decimal = 0
        Dim TotalPrevFinanceActivities As Decimal = 0
        Dim TotalCurrentcashequivalents As Decimal = 0
        Dim TotalPrevcashequivalents As Decimal = 0
        Dim TotalCurrentexchange_rate_changes As Decimal = 0
        Dim TotalPrevexchange_rate_changes As Decimal = 0
        Dim TotalCurrentbegining_of_the_year As Decimal = 0
        Dim TotalPrevbegining_of_the_year As Decimal = 0
        Dim TotalCurrentABC As Decimal = 0
        Dim TotalPrevABC As Decimal = 0
        Dim lblCurrenttotaladjustmentwork As Double
        Try
            lblError.Text = ""
            txtParticularsCategory1.Text = ""
            txtParticularsCategory1.Text = ""
            txtCurrentAmountCategory1.Text = ""
            txtPreviesAmountCategory1.Text = ""
            txtParticularsCategory3.Text = ""
            txtCurrentAmountCategory3.Text = ""
            txtPreviesAmountCategory3.Text = ""
            txtParticularsCategory4.Text = ""
            txtCurrentAmountCategory4.Text = ""
            txtPreviesAmountCategory4.Text = ""
            If ddlCustomers.SelectedIndex > 0 Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustomers.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                sSession.CustomerID = ddlCustomers.SelectedValue
                Session("AllSession") = sSession
                dtbranch = objUT.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtbranch.Rows.Count > 0 Then
                    ddlbranch.DataSource = dtbranch
                    ddlbranch.DataTextField = "BranchName"
                    ddlbranch.DataValueField = "Branchid"
                    ddlbranch.DataBind()
                    ddlbranch.Items.Insert(0, "Select Branch Name")
                    If sSession.ScheduleBranchId <> 0 Then
                        ddlbranch.SelectedValue = sSession.ScheduleBranchId
                        ddlbranch_SelectedIndexChanged(sender, e)
                    Else
                        sSession.ScheduleBranchId = 0
                    End If
                    Session("AllSession") = sSession
                Else
                    lblExcelValidationMsg.Text = "Customer should have atleast one Branch, Please add"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    lblError.Text = lblExcelValidationMsg.Text
                    ddlbranch.DataSource = Nothing
                    ddlbranch.DataTextField = "BranchName"
                    ddlbranch.DataValueField = "Branchid"
                    ddlbranch.DataBind()
                    ddlbranch.Items.Insert(0, "Select Branch Name")
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Select Customer"
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomers_SelectedIndexChanged")
        End Try
    End Sub


    Private Sub Gvcategory1_PreRender(sender As Object, e As EventArgs) Handles Gvcategory1.PreRender
        Dim dt As New DataTable
        Try
            If Gvcategory1.Rows.Count > 0 Then
                Gvcategory1.UseAccessibleHeader = True
                Gvcategory1.HeaderRow.TableSection = TableRowSection.TableHeader
                Gvcategory1.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Gvcategory1_PreRender")
        End Try
    End Sub
    Private Sub GrdviewTotalAmount_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GrdviewTotalAmount.RowDataBound
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox
        Dim lblParticulars As New Label
        Try
            lblError.Text = ""
            txtCurrentAmmount.Enabled = False
            txtPreviesAmount.Enabled = False
            txtCurrentAmmount.Visible = True
            txtPreviesAmount.Visible = True
            lblParticulars = CType(e.Row.FindControl("lblParticulars"), Label)
            txtCurrentAmmount = CType(e.Row.FindControl("txtCurrentAmmount"), TextBox)
            txtPreviesAmount = CType(e.Row.FindControl("txtPreviesAmount"), TextBox)
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then

                If IsDBNull(lblParticulars.Text) = False Then
                    If lblParticulars.Text = "Cash generated from operations" Then
                        txtCurrentAmmount.ToolTip = "Total Cash generated from operations"
                    End If
                    If lblParticulars.Text = "Net income tax (paid) / refunds (net)" Then
                        txtCurrentAmmount.Enabled = True
                        txtPreviesAmount.Enabled = False
                    End If
                    If lblParticulars.Text = "Net cash generated from/ (used in) operating activities" Then
                        txtCurrentAmmount.ToolTip = "(Cash generated from operations) + (Net income tax (paid) / refunds (net))"
                        txtPreviesAmount.ToolTip = "(Cash generated from operations) + (Net income tax (paid) / refunds (net))"
                    End If
                    If lblParticulars.Text = "Adjustments for increase / (decrease) in operating liabilities:" Then
                        txtCurrentAmmount.Visible = False
                        txtPreviesAmount.Visible = False
                        lblParticulars.Attributes.Add("style", "font-style:italic")
                    End If
                    If lblParticulars.Text = "Changes in working capital:" Then
                        txtCurrentAmmount.Visible = False
                        txtPreviesAmount.Visible = False
                        lblParticulars.Attributes.Add("style", "font-style:italic")
                    End If
                    If lblParticulars.Text = "Adjustments for (increase) / decrease in operating assets:" Then
                        txtCurrentAmmount.Visible = False
                        txtPreviesAmount.Visible = False
                        lblParticulars.Attributes.Add("style", "font-style:italic")
                    End If
                Else
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GrdviewTotalAmount_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub GrdviewTotalAmount_PreRender(sender As Object, e As EventArgs) Handles GrdviewTotalAmount.PreRender
        Dim dt As New DataTable
        Try
            If GrdviewTotalAmount.Rows.Count > 0 Then
                GrdviewTotalAmount.UseAccessibleHeader = True
                GrdviewTotalAmount.HeaderRow.TableSection = TableRowSection.TableHeader
                GrdviewTotalAmount.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GrdviewTotalAmount_PreRender")
        End Try
    End Sub

    Private Sub grdCategory3_PreRender(sender As Object, e As EventArgs) Handles grdCategory3.PreRender
        Dim dt As New DataTable
        Try
            If grdCategory3.Rows.Count > 0 Then
                grdCategory3.UseAccessibleHeader = True
                grdCategory3.HeaderRow.TableSection = TableRowSection.TableHeader
                grdCategory3.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory3_PreRender")
        End Try
    End Sub

    Private Sub grdCategory4_PreRender(sender As Object, e As EventArgs) Handles grdCategory4.PreRender
        Dim dt As New DataTable
        Try
            If grdCategory4.Rows.Count > 0 Then
                grdCategory4.UseAccessibleHeader = True
                grdCategory4.HeaderRow.TableSection = TableRowSection.TableHeader
                grdCategory4.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory4_PreRender")
        End Try
    End Sub



    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("UploadTrailbalanceSchedule.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub

    '    Private Sub btnSavecategory2_Click(sender As Object, e As EventArgs) Handles btnSavecategory2.Click
    '        Dim arr() As String
    '        Dim lblParticulars, lblpkid As New Label
    '        Dim txtCurrentAmmount As New TextBox
    '        Dim txtPreviesAmount As New TextBox
    '        Try
    '            If GrdviewTotalAmount.Rows.Count > 0 Then
    '                For i = 0 To GrdviewTotalAmount.Rows.Count - 1

    '                    lblpkid = GrdviewTotalAmount.Rows(i).FindControl("lblpkid")
    '                    lblParticulars = GrdviewTotalAmount.Rows(i).FindControl("lblParticulars")
    '                    txtCurrentAmmount = GrdviewTotalAmount.Rows(i).FindControl("txtCurrentAmmount")
    '                    txtPreviesAmount = GrdviewTotalAmount.Rows(i).FindControl("txtPreviesAmount")

    '                    If IsDBNull(lblpkid.Text) = False Then
    '                        objclsCashFlow.iACF_pkid = Val(lblpkid.Text)
    '                    Else
    '                        objclsCashFlow.iACF_pkid = 0
    '                    End If

    '                    If IsDBNull(lblParticulars.Text) = False Then
    '                        objclsCashFlow.sACF_Description = lblParticulars.Text
    '                    Else
    '                        objclsCashFlow.sACF_Description = ""
    '                    End If
    '                    If IsDBNull(txtCurrentAmmount.Text.Trim) = False Then
    '                        If txtCurrentAmmount.Text.Trim = "" Then
    '                            objclsCashFlow.dACF_Current_Amount = 0
    '                        Else
    '                            objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(txtCurrentAmmount.Text.ToString())
    '                        End If
    '                    Else
    '                        objclsCashFlow.dACF_Current_Amount = 0
    '                    End If
    '                    If IsDBNull(txtPreviesAmount.Text.Trim) = False Then
    '                        If txtPreviesAmount.Text.Trim = "" Then
    '                            objclsCashFlow.dACF_Prev_Amount = 0
    '                        Else
    '                            objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(txtPreviesAmount.Text.ToString())
    '                        End If
    '                    Else
    '                        objclsCashFlow.dACF_Prev_Amount = 0
    '                    End If
    '                    If i > 13 Then
    '                        objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
    '                    Else
    '                        GoTo skip
    '                    End If
    '                    objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
    '                    objclsCashFlow.sACF_Status = "C"
    '                    objclsCashFlow.iACF_Crby = 0
    '                    objclsCashFlow.iACF_Updatedby = 0
    '                    objclsCashFlow.iACF_Compid = sSession.AccessCodeID
    '                    objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
    '                    objclsCashFlow.iACF_Catagary = 2
    '                    arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
    'skip:
    '                Next
    '            End If
    '            ddlbranch_SelectedIndexChanged(sender, e)
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
    '            lblExcelValidationMsg.Text = "Data Succesfully Updated"
    '        Catch ex As Exception
    '            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSavecategory2_Click")
    '        End Try
    '    End Sub

    'Private Sub btnSaveCategory5_Click(sender As Object, e As EventArgs) Handles btnSaveCategory5.Click
    '    Dim arr() As String
    '    Dim lblParticulars, lblpkid As New Label
    '    Dim lblCurrentAmmount As New Label
    '    Dim lblPreviesAmount As New Label
    '    Try
    '        If grdCategory5.Rows.Count > 0 Then
    '            For i = 0 To grdCategory5.Rows.Count - 1
    '                lblpkid = grdCategory5.Rows(i).FindControl("lblpkid")
    '                lblParticulars = grdCategory5.Rows(i).FindControl("lblParticulars")
    '                lblCurrentAmmount = grdCategory5.Rows(i).FindControl("lblCurrentAmmount")
    '                lblPreviesAmount = grdCategory5.Rows(i).FindControl("lblPreviesAmount")

    '                If IsDBNull(lblpkid.Text) = False Then
    '                    objclsCashFlow.iACF_pkid = Val(lblpkid.Text)
    '                    objclsCashFlow.iACF_Updatedby = sSession.UserID
    '                Else
    '                    objclsCashFlow.iACF_pkid = 0
    '                    objclsCashFlow.iACF_Updatedby = 0
    '                End If

    '                If IsDBNull(lblParticulars.Text) = False Then
    '                    objclsCashFlow.sACF_Description = lblParticulars.Text
    '                Else
    '                    objclsCashFlow.sACF_Description = ""
    '                End If
    '                If IsDBNull(lblPreviesAmount.Text.Trim) = False Then
    '                    objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(lblPreviesAmount.Text.ToString())
    '                Else
    '                    objclsCashFlow.dACF_Current_Amount = 0
    '                End If
    '                If IsDBNull(lblPreviesAmount.Text.Trim) = False Then
    '                    objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(lblPreviesAmount.Text.ToString())
    '                Else
    '                    objclsCashFlow.dACF_Prev_Amount = 0
    '                End If
    '                objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
    '                objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
    '                objclsCashFlow.sACF_Status = "C"
    '                objclsCashFlow.iACF_Crby = sSession.UserID

    '                objclsCashFlow.iACF_Compid = sSession.AccessCodeID
    '                objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
    '                objclsCashFlow.iACF_Catagary = 5
    '                arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
    '            Next
    '        End If
    '        ddlbranch_SelectedIndexChanged(sender, e)
    '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
    '        lblExcelValidationMsg.Text = "Data Succesfully Updated"
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveCategory5_Click")
    '    End Try
    'End Sub

    Private Sub Gvcategory1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles Gvcategory1.RowDataBound
        Dim imgbtndelete As New ImageButton
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox, lblParticulars As New Label
        Try

            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/4delete.gif"
                txtCurrentAmmount = CType(e.Row.FindControl("txtCurrentAmmount"), TextBox)
                txtPreviesAmount = CType(e.Row.FindControl("txtPreviesAmount"), TextBox)
                txtCurrentAmmount.Enabled = False
                txtPreviesAmount.Enabled = False
                lblParticulars = CType(e.Row.FindControl("lblParticulars"), Label)
                If Gvcategory1.Rows.Count <= 7 Then
                    imgbtndelete.Visible = False
                    txtCurrentAmmount.Enabled = False
                    txtPreviesAmount.Enabled = False
                Else
                    txtCurrentAmmount.Enabled = True
                    txtPreviesAmount.Enabled = False
                    imgbtndelete.Visible = True
                End If
                'If lblParticulars.Text.Trim = "Interest income" Then
                '    txtCurrentAmmount.Enabled = True
                'End If
                'If lblParticulars.Text.Trim = "Effect on exchange rate changes" Then
                '    txtCurrentAmmount.Enabled = True
                'End If
                'If lblParticulars.Text.Trim = "Income Tax Refund Received" Then
                '    txtCurrentAmmount.Enabled = True
                'End If
                'If lblParticulars.Text.Trim = "Preliminary Expenses written off" Then
                '    txtCurrentAmmount.Enabled = True
                'End If
                If lblParticulars.Text.Trim = "Adjustment for:" Or lblParticulars.Text.Trim = "A.Cash flow from operating activities" Then
                    txtCurrentAmmount.Visible = False
                    txtPreviesAmount.Visible = False
                    lblParticulars.Attributes.Add("style", "font-style:italic")
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJE_RowDataBound")
        End Try
    End Sub

    Private Sub Gvcategory1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles Gvcategory1.RowCommand
        Dim lblParticulars As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblParticulars = DirectCast(clickedRow.FindControl("lblParticulars"), Label)
            Dim iPkid As Integer = 0
            iPkid = objclsCashFlow.getCashFlowParticularsID(sSession.AccessCode, sSession.AccessCodeID, lblParticulars.Text, ddlCustomers.SelectedValue, ddlbranch.SelectedValue)
            If e.CommandName.Equals("Delete") Then
                objclsCashFlow.DeleteCashflowCategory1(sSession.AccessCode, sSession.AccessCodeID, iPkid, ddlCustomers.SelectedValue)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Deleted"
            ddlbranch_SelectedIndexChanged(sender, e)
            Gvcategory1.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Gvcategory1_RowCommand")
        End Try
    End Sub

    Private Sub Gvcategory1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles Gvcategory1.RowDeleting
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Gvcategory1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles Gvcategory1.RowEditing
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCategory3.RowDataBound
        Dim imgbtndelete As New ImageButton
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox
        Dim lblParticulars As New Label
        Try
            lblError.Text = ""
            txtCurrentAmmount.Enabled = False
            txtPreviesAmount.Enabled = False
            lblParticulars = CType(e.Row.FindControl("lblParticulars"), Label)
            txtCurrentAmmount = CType(e.Row.FindControl("txtCurrentAmmount"), TextBox)
            txtPreviesAmount = CType(e.Row.FindControl("txtPreviesAmount"), TextBox)
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/4delete.gif"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory3_RowDataBound")
        End Try
    End Sub

    Private Sub grdCategory3_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdCategory3.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory3_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdCategory3.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory3_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCategory3.RowCommand
        Dim lblParticulars As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblParticulars = DirectCast(clickedRow.FindControl("lblParticulars"), Label)
            Dim iPkid As Integer = 0
            iPkid = objclsCashFlow.getCashFlowParticularsID(sSession.AccessCode, sSession.AccessCodeID, lblParticulars.Text, ddlCustomers.SelectedValue, ddlbranch.SelectedValue)
            If e.CommandName.Equals("Delete") Then
                objclsCashFlow.DeleteCashflowCategory1(sSession.AccessCode, sSession.AccessCodeID, iPkid, ddlCustomers.SelectedValue)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Deleted"
            ddlbranch_SelectedIndexChanged(sender, e)
            grdCategory3.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory3_RowCommand")
        End Try
    End Sub

    Private Sub grdCategory4_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCategory4.RowDataBound
        Dim imgbtndelete As New ImageButton
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox
        Dim lblParticulars As New Label
        Try
            lblError.Text = ""
            txtCurrentAmmount.Enabled = False
            txtPreviesAmount.Enabled = False
            lblParticulars = CType(e.Row.FindControl("lblParticulars"), Label)
            txtCurrentAmmount = CType(e.Row.FindControl("txtCurrentAmmount"), TextBox)
            txtPreviesAmount = CType(e.Row.FindControl("txtPreviesAmount"), TextBox)
            If e.Row.RowType <> ListItemType.Header And e.Row.RowType <> ListItemType.Footer Then
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/4delete.gif"
                If grdCategory4.Rows.Count < 8 Then
                    imgbtndelete.Visible = False
                Else
                    imgbtndelete.Visible = True
                End If
                If lblParticulars.Text = "Proceeds from issue of equity shares" Then
                    txtCurrentAmmount.Visible = False
                    txtPreviesAmount.Visible = False
                End If
                If lblParticulars.Text = "Share application money received / (refunded)" Then
                    txtCurrentAmmount.Visible = False
                    txtPreviesAmount.Visible = False
                End If
                If lblParticulars.Text = "Increase / (Decrease) in Long Term Borrowings" Then
                    txtCurrentAmmount.Enabled = False
                End If
                If lblParticulars.Text = "Increase / (Decrease) in Short Term Borrowings" Then
                    txtCurrentAmmount.Enabled = False
                End If
                If lblParticulars.Text = "Interest Paid" Then
                    txtCurrentAmmount.Enabled = False
                End If
                If lblParticulars.Text = "Increase / (Decrease) in Long Term Provisions" Then
                    txtCurrentAmmount.Enabled = False
                End If

            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory4_RowDataBound")
        End Try
    End Sub

    Private Sub grdCategory4_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdCategory4.RowDeleting
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory4_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdCategory4.RowEditing
        Try

        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory4_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCategory4.RowCommand
        Dim lblParticulars As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblParticulars = DirectCast(clickedRow.FindControl("lblParticulars"), Label)
            Dim iPkid As Integer = 0
            iPkid = objclsCashFlow.getCashFlowParticularsID(sSession.AccessCode, sSession.AccessCodeID, lblParticulars.Text, ddlCustomers.SelectedValue, ddlbranch.SelectedValue)
            If e.CommandName.Equals("Delete") Then
                objclsCashFlow.DeleteCashflowCategory1(sSession.AccessCode, sSession.AccessCodeID, iPkid, ddlCustomers.SelectedValue)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Deleted"
            ddlbranch_SelectedIndexChanged(sender, e)
            grdCategory4.Focus()
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory4_RowCommand")
        End Try
    End Sub

    Private Sub grdCategory5_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdCategory5.RowDataBound
        Dim imgbtndelete As New ImageButton
        Dim lblCurrentAmmount As New Label
        Dim lblPreviesAmount As New Label
        Dim lblParticulars As New Label
        Try
            lblError.Text = ""
            lblCurrentAmmount.Visible = True
            lblPreviesAmount.Visible = True
            lblParticulars = CType(e.Row.FindControl("lblParticulars"), Label)
            lblCurrentAmmount = CType(e.Row.FindControl("lblCurrentAmmount"), Label)
            lblPreviesAmount = CType(e.Row.FindControl("lblPreviesAmount"), Label)
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtndelete = CType(e.Row.FindControl("imgbtnDelete"), ImageButton)
                imgbtndelete.ImageUrl = "~/Images/4delete.gif"
                If grdCategory5.Rows.Count > 0 Then
                    If lblParticulars.Text = "* Comprises:" Then
                        lblCurrentAmmount.Visible = False
                        lblPreviesAmount.Visible = False
                        lblParticulars.Attributes.Add("style", "font-style:italic")
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory5_RowDataBound")
        End Try
    End Sub

    Private Sub grdCategory5_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles grdCategory5.RowDeleting
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory5_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles grdCategory5.RowEditing
        Try
        Catch ex As Exception

        End Try
    End Sub

    Private Sub grdCategory5_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles grdCategory5.RowCommand
        Dim lblParticulars As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblParticulars = DirectCast(clickedRow.FindControl("lblParticulars"), Label)
            Dim iPkid As Integer = 0
            iPkid = objclsCashFlow.getCashFlowParticularsID(sSession.AccessCode, sSession.AccessCodeID, lblParticulars.Text, ddlCustomers.SelectedValue, ddlbranch.SelectedValue)
            If e.CommandName.Equals("Delete") Then
                objclsCashFlow.DeleteCashflowCategory1(sSession.AccessCode, sSession.AccessCodeID, iPkid, ddlCustomers.SelectedValue)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Deleted"
            ddlbranch_SelectedIndexChanged(sender, e)
            grdCategory4.Focus()

        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "grdCategory5_RowCommand")
        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ddlbranch_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim lblParticulars, lblpkid As New Label
        Dim TotaladjustmentsCurrent As Decimal = 0
        Dim TotaladjustmentsPrev As Decimal = 0
        Dim TotalManualCurrent As Decimal = 0
        Dim CurrentAmmount As Decimal
        Dim TotalManualPrev As Decimal = 0
        Dim TotalCurrentOperativeActivities As Decimal = 0
        Dim TotalPrevOperativeActivities As Decimal = 0
        Dim TotalCurrentInvestingActivities As Decimal = 0
        Dim TotalPrevInvestingActivities As Decimal = 0
        Dim TotalCurrentFinanceActivities As Decimal = 0
        Dim TotalPrevFinanceActivities As Decimal = 0
        Dim TotalCurrentcashequivalents As Decimal = 0
        Dim TotalPrevcashequivalents As Decimal = 0
        Dim TotalCurrentexchange_rate_changes As Decimal = 0
        Dim TotalPrevexchange_rate_changes As Decimal = 0
        Dim TotalCurrentbegining_of_the_year As Decimal = 0
        Dim TotalPrevbegining_of_the_year As Decimal = 0
        Dim TotalCurrentABC As Decimal = 0
        Dim TotalPrevABC As Decimal = 0
        Dim dtCash As New DataTable
        Dim dtCash1 As New DataTable
        Dim dtCash2 As New DataTable
        Dim dtCash3 As New DataTable
        Dim dtCash4 As New DataTable
        Try
            If ddlCustomers.SelectedIndex > 0 Then
                dtCash = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 1, ddlFinancialYear.SelectedValue)
                Dim PrevAmount As Decimal = 0
                Dim lblTotalCurrentcategory1 As New Label
                Dim lblTotalPrevcategory1 As New Label
                If dtCash.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblTotalCurrentcategory1.Text = CurrentAmmount
                    lblTotalPrevcategory1.Text = PrevAmount
                    dtCash(0)("Particulers") = "<b>A.Cash flow from operating activities</b>"
                    dtCash(2)("Particulers") = "<i>Adjustment for:</i>"
                End If
                Dim lblCurrentOpratingTotal As New Label
                Dim lblPrevOpratingTotal As New Label
                dtCash1 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 2, ddlFinancialYear.SelectedValue)
                If dtCash1.Rows.Count > 0 Then
                    dtCash1(0)("Particulers") = "<i>Changes in working capital:</i>"
                    dtCash1(1)("Particulers") = "<i>Adjustments for (increase) / decrease in operating assets:</i>"
                    For i = 0 To dtCash1.Rows.Count - 1
                        If IsDBNull(dtCash1.Rows(i)("CurrentAmmount")) = False Then
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + dtCash1.Rows(i)("CurrentAmmount")
                        Else
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + 0
                        End If
                        If IsDBNull(dtCash1.Rows(i)("PreviesAmount")) = False Then
                            TotaladjustmentsPrev = TotaladjustmentsPrev + dtCash1.Rows(i)("PreviesAmount")
                        Else
                            TotaladjustmentsPrev = TotaladjustmentsPrev + 0
                        End If
                        If dtCash1(i)("Particulers") = "Adjustments for increase / (decrease) in operating liabilities:" Then
                            dtCash1(i)("Particulers") = "<i>Adjustments for increase / (decrease) in operating liabilities:</i>"
                        End If
                        If dtCash1.Rows(i)("Particulers") = "Operating profit / (loss) after working capital changes" Then
                            dtCash1.Rows(i)("CurrentAmmount") = TotaladjustmentsCurrent + Val(lblTotalCurrentcategory1.Text)
                            dtCash1.Rows(i)("PreviesAmount") = TotaladjustmentsPrev + Val(lblTotalPrevcategory1.Text)
                        ElseIf dtCash1(i)("Particulers") = "Cash generated from operations" Then
                            TotalCurrentOperativeActivities = TotaladjustmentsCurrent + CurrentAmmount
                            TotalPrevOperativeActivities = TotaladjustmentsPrev + PrevAmount
                            dtCash1.Rows(i)("CurrentAmmount") = TotalCurrentOperativeActivities
                            dtCash1.Rows(i)("PreviesAmount") = TotalPrevOperativeActivities
                        ElseIf dtCash1(i)("Particulers") = "Net income tax (paid) / refunds (net)" Then
                            TotalManualCurrent = dtCash1.Rows(i)("CurrentAmmount")
                            TotalManualPrev = dtCash1.Rows(i)("PreviesAmount")
                            'ElseIf dt1(i)("Particulers") = "Net cash generated from/ (used in) operating activities" Then
                            '    dt1.Rows(i)("CurrentAmmount") = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                            '    dt1.Rows(i)("PreviesAmount") = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                        End If


                    Next
                    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                End If
                Dim lblCurrentinvestingactivities As New Label
                Dim lblprevinvestingactivities As New Label
                dtCash2 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 3, ddlFinancialYear.SelectedValue)
                If dtCash2.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash2.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash2.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentinvestingactivities.Text = CurrentAmmount
                    lblprevinvestingactivities.Text = PrevAmount
                End If

                Dim lblCurrentfinancingactivities As New Label
                Dim lblPrevfinancingactivities As New Label
                dtCash3 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 4, ddlFinancialYear.SelectedValue)
                If dtCash3.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash3.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash3.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentfinancingactivities.Text = CurrentAmmount
                    lblPrevfinancingactivities.Text = PrevAmount
                End If
                dtCash4 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 5, ddlFinancialYear.SelectedValue)
                If dtCash4.Rows.Count > 0 Then
                    For i = 0 To dtCash4.Rows.Count - 1
                        If dtCash4.Rows(i)("Particulers") = "Net increase (derease) in cash and cash equivalents befor effect of exchange rate changes" Then
                            TotalCurrentexchange_rate_changes = TotalCurrentOperativeActivities + TotalCurrentInvestingActivities + TotalCurrentFinanceActivities
                            TotalPrevexchange_rate_changes = TotalPrevOperativeActivities + TotalPrevInvestingActivities + TotalPrevFinanceActivities
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentexchange_rate_changes
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevexchange_rate_changes
                        ElseIf dtCash4(i)("Particulers") = "Net increase / (decrease) in Cash and cash equivalents (A+B+C)" Then
                            TotalCurrentABC = Math.Round(Convert.ToDouble(Val(lblCurrentOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentfinancingactivities.Text)), 2)
                            TotalPrevABC = Math.Round(Convert.ToDouble(Val(lblPrevOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblprevinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblPrevfinancingactivities.Text)), 2)
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentABC
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at begining of the year" Then
                            TotalCurrentbegining_of_the_year = Val(dtCash4.Rows(i)("CurrentAmmount")) + TotalCurrentABC
                            TotalPrevbegining_of_the_year = Val(dtCash4.Rows(i)("PreviesAmount")) + TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at Closing of the year" Then
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentbegining_of_the_year
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevbegining_of_the_year
                            'dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                            'dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)
                            'If dtIncome.Rows.Count > 0 Then
                            '    dCYProfiTAmt = dtIncome(0)("Dc1")
                            '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                            'Else
                            '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            'End If
                            'Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            '    Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        End If
                    Next
                End If
                ReportViewer1.Reset()
                Dim rds5 As New ReportDataSource("DataSet1", dtCash)
                ReportViewer1.LocalReport.DataSources.Add(rds5)
                Dim rds6 As New ReportDataSource("DataSet2", dtCash1)
                ReportViewer1.LocalReport.DataSources.Add(rds6)
                Dim rds7 As New ReportDataSource("DataSet3", dtCash2)
                ReportViewer1.LocalReport.DataSources.Add(rds7)
                Dim rds8 As New ReportDataSource("DataSet4", dtCash3)
                ReportViewer1.LocalReport.DataSources.Add(rds8)
                Dim rds9 As New ReportDataSource("DataSet5", dtCash4)
                ReportViewer1.LocalReport.DataSources.Add(rds9)

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/CashFlow.rdlc")
                Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomers.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Customer)
                Dim FYear As ReportParameter() = New ReportParameter() {New ReportParameter("FYear", ddlFinancialYear.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(FYear)


                Dim Current As ReportParameter() = New ReportParameter() {New ReportParameter("Current", lblTotalCurrentcategory1.Text)}
                ReportViewer1.LocalReport.SetParameters(Current)
                Dim PrevAmount1 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevAmount", lblTotalPrevcategory1.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevAmount1)


                'Category operational activities
                Dim CurrentTotalCt1 As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentTotalCt1", lblCurrentOpratingTotal.Text)}
                ReportViewer1.LocalReport.SetParameters(CurrentTotalCt1)
                Dim PrevTotalCt1 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevTotalCt1", lblPrevOpratingTotal.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevTotalCt1)

                'Category INVESTING ACTIVITIES 
                Dim CurrentTotalct2 As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentTotalct2", lblCurrentinvestingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(CurrentTotalct2)
                Dim PrevTotalCt2 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevTotalCt2", lblprevinvestingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevTotalCt2)

                'Category financing Activities 
                Dim CurrentTotalct3 As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentTotalct3", lblCurrentfinancingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(CurrentTotalct3)
                Dim PrevTotalCt3 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevTotalCt3", lblPrevfinancingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevTotalCt3)


                Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=CashFlow" + ".pdf")
                Response.BinaryWrite(pdfViewer)
                Response.Flush()
                Response.End()
            Else
                lblError.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Select Customer"
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Dim lblParticulars, lblpkid As New Label
        Dim TotaladjustmentsCurrent As Decimal = 0
        Dim TotaladjustmentsPrev As Decimal = 0
        Dim TotalManualCurrent As Decimal = 0
        Dim CurrentAmmount As Decimal
        Dim TotalManualPrev As Decimal = 0
        Dim TotalCurrentOperativeActivities As Decimal = 0
        Dim TotalPrevOperativeActivities As Decimal = 0
        Dim TotalCurrentInvestingActivities As Decimal = 0
        Dim TotalPrevInvestingActivities As Decimal = 0
        Dim TotalCurrentFinanceActivities As Decimal = 0
        Dim TotalPrevFinanceActivities As Decimal = 0
        Dim TotalCurrentcashequivalents As Decimal = 0
        Dim TotalPrevcashequivalents As Decimal = 0
        Dim TotalCurrentexchange_rate_changes As Decimal = 0
        Dim TotalPrevexchange_rate_changes As Decimal = 0
        Dim TotalCurrentbegining_of_the_year As Decimal = 0
        Dim TotalPrevbegining_of_the_year As Decimal = 0
        Dim TotalCurrentABC As Decimal = 0
        Dim TotalPrevABC As Decimal = 0
        Dim dtCash As New DataTable
        Dim dtCash1 As New DataTable
        Dim dtCash2 As New DataTable
        Dim dtCash3 As New DataTable
        Dim dtCash4 As New DataTable
        Try
            If ddlCustomers.SelectedIndex > 0 Then
                dtCash = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 1, ddlFinancialYear.SelectedValue)
                Dim PrevAmount As Decimal = 0
                Dim lblTotalCurrentcategory1 As New Label
                Dim lblTotalPrevcategory1 As New Label
                If dtCash.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblTotalCurrentcategory1.Text = CurrentAmmount
                    lblTotalPrevcategory1.Text = PrevAmount
                    dtCash(0)("Particulers") = "<b>A.Cash flow from operating activities</b>"
                    dtCash(2)("Particulers") = "<i>Adjustment for:</i>"
                End If
                Dim lblCurrentOpratingTotal As New Label
                Dim lblPrevOpratingTotal As New Label
                dtCash1 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 2, ddlFinancialYear.SelectedValue)
                If dtCash1.Rows.Count > 0 Then
                    dtCash1(0)("Particulers") = "<i>Changes in working capital:</i>"
                    dtCash1(1)("Particulers") = "<i>Adjustments for (increase) / decrease in operating assets:</i>"
                    For i = 0 To dtCash1.Rows.Count - 1
                        If IsDBNull(dtCash1.Rows(i)("CurrentAmmount")) = False Then
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + dtCash1.Rows(i)("CurrentAmmount")
                        Else
                            TotaladjustmentsCurrent = TotaladjustmentsCurrent + 0
                        End If
                        If IsDBNull(dtCash1.Rows(i)("PreviesAmount")) = False Then
                            TotaladjustmentsPrev = TotaladjustmentsPrev + dtCash1.Rows(i)("PreviesAmount")
                        Else
                            TotaladjustmentsPrev = TotaladjustmentsPrev + 0
                        End If
                        If dtCash1(i)("Particulers") = "Adjustments for increase / (decrease) in operating liabilities:" Then
                            dtCash1(i)("Particulers") = "<i>Adjustments for increase / (decrease) in operating liabilities:</i>"
                        End If
                        If dtCash1.Rows(i)("Particulers") = "Operating profit / (loss) after working capital changes" Then
                            dtCash1.Rows(i)("CurrentAmmount") = TotaladjustmentsCurrent + Val(lblTotalCurrentcategory1.Text)
                            dtCash1.Rows(i)("PreviesAmount") = TotaladjustmentsPrev + Val(lblTotalPrevcategory1.Text)
                        ElseIf dtCash1(i)("Particulers") = "Cash generated from operations" Then
                            TotalCurrentOperativeActivities = TotaladjustmentsCurrent + CurrentAmmount
                            TotalPrevOperativeActivities = TotaladjustmentsPrev + PrevAmount
                            dtCash1.Rows(i)("CurrentAmmount") = TotalCurrentOperativeActivities
                            dtCash1.Rows(i)("PreviesAmount") = TotalPrevOperativeActivities
                        ElseIf dtCash1(i)("Particulers") = "Net income tax (paid) / refunds (net)" Then
                            TotalManualCurrent = dtCash1.Rows(i)("CurrentAmmount")
                            TotalManualPrev = dtCash1.Rows(i)("PreviesAmount")
                            'ElseIf dt1(i)("Particulers") = "Net cash generated from/ (used in) operating activities" Then
                            '    dt1.Rows(i)("CurrentAmmount") = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                            '    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                            '    dt1.Rows(i)("PreviesAmount") = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                        End If


                    Next
                    lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                    lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                End If
                Dim lblCurrentinvestingactivities As New Label
                Dim lblprevinvestingactivities As New Label
                dtCash2 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 3, ddlFinancialYear.SelectedValue)
                If dtCash2.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash2.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash2.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentinvestingactivities.Text = CurrentAmmount
                    lblprevinvestingactivities.Text = PrevAmount
                End If

                Dim lblCurrentfinancingactivities As New Label
                Dim lblPrevfinancingactivities As New Label
                dtCash3 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 4, ddlFinancialYear.SelectedValue)
                If dtCash3.Rows.Count > 0 Then
                    CurrentAmmount = Convert.ToDecimal(dtCash3.Compute("SUM(CurrentAmmount1)", String.Empty))
                    PrevAmount = Convert.ToDecimal(dtCash3.Compute("SUM(PreviesAmount1)", String.Empty))
                    lblCurrentfinancingactivities.Text = CurrentAmmount
                    lblPrevfinancingactivities.Text = PrevAmount
                End If
                dtCash4 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 5, ddlFinancialYear.SelectedValue)
                If dtCash4.Rows.Count > 0 Then
                    For i = 0 To dtCash4.Rows.Count - 1
                        If dtCash4.Rows(i)("Particulers") = "Net increase (derease) in cash and cash equivalents befor effect of exchange rate changes" Then
                            TotalCurrentexchange_rate_changes = TotalCurrentOperativeActivities + TotalCurrentInvestingActivities + TotalCurrentFinanceActivities
                            TotalPrevexchange_rate_changes = TotalPrevOperativeActivities + TotalPrevInvestingActivities + TotalPrevFinanceActivities
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentexchange_rate_changes
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevexchange_rate_changes
                        ElseIf dtCash4(i)("Particulers") = "Net increase / (decrease) in Cash and cash equivalents (A+B+C)" Then
                            TotalCurrentABC = Math.Round(Convert.ToDouble(Val(lblCurrentOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentfinancingactivities.Text)), 2)
                            TotalPrevABC = Math.Round(Convert.ToDouble(Val(lblPrevOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblprevinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblPrevfinancingactivities.Text)), 2)
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentABC
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at begining of the year" Then
                            TotalCurrentbegining_of_the_year = Val(dtCash4.Rows(i)("CurrentAmmount")) + TotalCurrentABC
                            TotalPrevbegining_of_the_year = Val(dtCash4.Rows(i)("PreviesAmount")) + TotalPrevABC
                        ElseIf dtCash4(i)("Particulers") = "Cash and cash equivalents at Closing of the year" Then
                            dtCash4.Rows(i)("CurrentAmmount") = TotalCurrentbegining_of_the_year
                            dtCash4.Rows(i)("PreviesAmount") = TotalPrevbegining_of_the_year
                            'dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                            'dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)
                            'If dtIncome.Rows.Count > 0 Then
                            '    dCYProfiTAmt = dtIncome(0)("Dc1")
                            '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                            'Else
                            '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                            'End If
                            'Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                            '    Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                        End If
                    Next
                End If
                ReportViewer1.Reset()
                Dim rds5 As New ReportDataSource("DataSet1", dtCash)
                ReportViewer1.LocalReport.DataSources.Add(rds5)
                Dim rds6 As New ReportDataSource("DataSet2", dtCash1)
                ReportViewer1.LocalReport.DataSources.Add(rds6)
                Dim rds7 As New ReportDataSource("DataSet3", dtCash2)
                ReportViewer1.LocalReport.DataSources.Add(rds7)
                Dim rds8 As New ReportDataSource("DataSet4", dtCash3)
                ReportViewer1.LocalReport.DataSources.Add(rds8)
                Dim rds9 As New ReportDataSource("DataSet5", dtCash4)
                ReportViewer1.LocalReport.DataSources.Add(rds9)

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/CashFlow.rdlc")
                Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlCustomers.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(Customer)
                Dim FYear As ReportParameter() = New ReportParameter() {New ReportParameter("FYear", ddlFinancialYear.SelectedItem.Text)}
                ReportViewer1.LocalReport.SetParameters(FYear)


                Dim Current As ReportParameter() = New ReportParameter() {New ReportParameter("Current", lblTotalCurrentcategory1.Text)}
                ReportViewer1.LocalReport.SetParameters(Current)
                Dim PrevAmount1 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevAmount", lblTotalPrevcategory1.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevAmount1)


                'Category operational activities
                Dim CurrentTotalCt1 As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentTotalCt1", lblCurrentOpratingTotal.Text)}
                ReportViewer1.LocalReport.SetParameters(CurrentTotalCt1)
                Dim PrevTotalCt1 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevTotalCt1", lblPrevOpratingTotal.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevTotalCt1)

                'Category INVESTING ACTIVITIES 
                Dim CurrentTotalct2 As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentTotalct2", lblCurrentinvestingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(CurrentTotalct2)
                Dim PrevTotalCt2 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevTotalCt2", lblprevinvestingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevTotalCt2)

                'Category financing Activities 
                Dim CurrentTotalct3 As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentTotalct3", lblCurrentfinancingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(CurrentTotalct3)
                Dim PrevTotalCt3 As ReportParameter() = New ReportParameter() {New ReportParameter("PrevTotalCt3", lblPrevfinancingactivities.Text)}
                ReportViewer1.LocalReport.SetParameters(PrevTotalCt3)

                Dim ExcelViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
                Response.Buffer = True
                Response.Clear()
                Response.ContentType = mimeType
                Response.AddHeader("content-disposition", "attachment; filename=CashFlow" + ".xls; sheetname=cashflow")
                Response.BinaryWrite(ExcelViewer)
                Response.Flush()
                Response.End()
            Else
                lblError.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Select Customer"
                Exit Sub
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Private Sub btnAddcategory1_Click(sender As Object, e As EventArgs) Handles btnAddcategory1.Click
        Dim arr() As String
        Try
            objclsCashFlow.iACF_pkid = 0
            objclsCashFlow.sACF_Description = txtParticularsCategory1.Text
            objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
            objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
            If Val(txtCurrentAmountCategory1.Text) <> 0 Then
                objclsCashFlow.dACF_Current_Amount = txtCurrentAmountCategory1.Text
            Else
                objclsCashFlow.dACF_Current_Amount = 0
            End If
            If Val(txtPreviesAmountCategory1.Text) <> 0 Then
                objclsCashFlow.dACF_Prev_Amount = txtPreviesAmountCategory1.Text
            Else
                objclsCashFlow.dACF_Prev_Amount = 0
            End If

            objclsCashFlow.sACF_Status = "C"
            objclsCashFlow.iACF_Crby = 0
            objclsCashFlow.iACF_Updatedby = 0
            objclsCashFlow.iACF_Compid = sSession.AccessCodeID
            objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
            objclsCashFlow.iACF_Catagary = 1
            objclsCashFlow.iACF_Yearid = ddlFinancialYear.SelectedValue
            arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
            ddlbranch_SelectedIndexChanged(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Updated"
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub bntCategory3_Click(sender As Object, e As EventArgs) Handles bntCategory3.Click
        Dim arr() As String
        Try
            objclsCashFlow.iACF_pkid = 0
            objclsCashFlow.sACF_Description = txtParticularsCategory3.Text
            objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
            objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
            If Val(txtCurrentAmountCategory3.Text) <> 0 Then
                objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(txtCurrentAmountCategory3.Text.ToString())
            Else
                objclsCashFlow.dACF_Current_Amount = 0
            End If
            If Val(txtPreviesAmountCategory3.Text) <> 0 Then
                objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(txtPreviesAmountCategory3.Text.ToString())
            Else
                objclsCashFlow.dACF_Prev_Amount = 0
            End If
            objclsCashFlow.sACF_Status = "C"
            objclsCashFlow.iACF_Crby = 0
            objclsCashFlow.iACF_Updatedby = 0
            objclsCashFlow.iACF_Compid = sSession.AccessCodeID
            objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
            objclsCashFlow.iACF_Catagary = 3
            objclsCashFlow.iACF_Yearid = ddlFinancialYear.SelectedValue
            arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
            ddlbranch_SelectedIndexChanged(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Updated"
            grdCategory3.Focus()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "bntCategory3_Click")
        End Try
    End Sub

    Private Sub btnAddCategory4_Click(sender As Object, e As EventArgs) Handles btnAddCategory4.Click
        Dim arr() As String
        Try
            objclsCashFlow.iACF_pkid = 0
            objclsCashFlow.sACF_Description = txtParticularsCategory4.Text
            objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
            objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
            If Val(txtCurrentAmountCategory4.Text) <> 0 Then
                objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(txtCurrentAmountCategory4.Text.ToString())
            Else
                objclsCashFlow.dACF_Current_Amount = 0
            End If
            If Val(txtPreviesAmountCategory4.Text) <> 0 Then
                objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(txtPreviesAmountCategory4.Text.ToString())
            Else
                objclsCashFlow.dACF_Prev_Amount = 0
            End If
            objclsCashFlow.sACF_Status = "C"
            objclsCashFlow.iACF_Crby = 0
            objclsCashFlow.iACF_Updatedby = 0
            objclsCashFlow.iACF_Compid = sSession.AccessCodeID
            objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
            objclsCashFlow.iACF_Catagary = 4
            objclsCashFlow.iACF_Yearid = ddlFinancialYear.SelectedValue
            arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
            ddlbranch_SelectedIndexChanged(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Updated"
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddCategory4_Click")
        End Try
    End Sub

    Private Sub btnUpdateGvcategory1_Click(sender As Object, e As EventArgs) Handles btnUpdateGvcategory1.Click
        Dim arr() As String
        Dim lblParticulars As New Label
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox
        Dim checkdata As Integer = 0
        Try
            For i = 0 To Gvcategory1.Rows.Count - 1
                If i > 7 Then
                    lblParticulars = Gvcategory1.Rows(i).FindControl("lblParticulars")
                    'lblpkid = Gvcategory1.Rows(i).FindControl("lblpkid")
                    lblParticulars = Gvcategory1.Rows(i).FindControl("lblParticulars")
                    txtCurrentAmmount = Gvcategory1.Rows(i).FindControl("txtCurrentAmmount")
                    txtPreviesAmount = Gvcategory1.Rows(i).FindControl("txtPreviesAmount")
                    checkdata = objclsCashFlow.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, lblParticulars.Text, ddlFinancialYear.SelectedValue, ddlbranch.SelectedValue)
                    If checkdata = 0 Then
                        objclsCashFlow.iACF_pkid = 0
                    Else
                        objclsCashFlow.iACF_pkid = checkdata
                    End If

                    If IsDBNull(lblParticulars.Text.Trim) = False Then
                        objclsCashFlow.sACF_Description = lblParticulars.Text.Trim
                    Else
                        objclsCashFlow.sACF_Description = ""
                    End If
                    If IsDBNull(txtCurrentAmmount.Text) = False Then
                        objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(txtCurrentAmmount.Text.ToString())
                    Else
                        objclsCashFlow.dACF_Current_Amount = 0
                    End If
                    If IsDBNull(txtPreviesAmount.Text) = False Then
                        objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(txtPreviesAmount.Text.ToString())
                    Else
                        objclsCashFlow.dACF_Prev_Amount = 0
                    End If
                    objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
                    objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
                    objclsCashFlow.sACF_Status = "U"
                    objclsCashFlow.iACF_Crby = sSession.UserID
                    objclsCashFlow.iACF_Updatedby = sSession.UserID
                    objclsCashFlow.iACF_Compid = sSession.AccessCodeID
                    objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
                    objclsCashFlow.iACF_Catagary = 1
                    objclsCashFlow.iACF_Yearid = ddlFinancialYear.SelectedValue
                    arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
                End If
            Next
            ddlbranch_SelectedIndexChanged(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Updated"
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnsaveCategory3_Click(sender As Object, e As EventArgs) Handles btnsaveCategory3.Click
        Dim arr() As String
        Dim lblParticulars, lblpkid As New Label
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox
        Dim checkdata As Integer = 0
        Try
            For i = 0 To grdCategory3.Rows.Count - 1

                lblParticulars = grdCategory3.Rows(i).FindControl("lblParticulars")
                txtCurrentAmmount = grdCategory3.Rows(i).FindControl("txtCurrentAmmount")
                txtPreviesAmount = grdCategory3.Rows(i).FindControl("txtPreviesAmount")

                checkdata = objclsCashFlow.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, lblParticulars.Text, ddlFinancialYear.SelectedValue, ddlbranch.SelectedValue)
                If checkdata = 0 Then
                    objclsCashFlow.iACF_pkid = 0
                Else
                    objclsCashFlow.iACF_pkid = checkdata
                End If

                If IsDBNull(lblParticulars.Text.Trim) = False Then
                    objclsCashFlow.sACF_Description = lblParticulars.Text.Trim
                Else
                    objclsCashFlow.sACF_Description = ""
                End If
                If IsDBNull(txtCurrentAmmount.Text) = False Then
                    objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(Val(txtCurrentAmmount.Text).ToString())
                Else
                    objclsCashFlow.dACF_Current_Amount = 0
                End If
                If IsDBNull(txtPreviesAmount.Text) = False Then
                    objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(Val(txtPreviesAmount.Text).ToString())
                Else
                    objclsCashFlow.dACF_Prev_Amount = 0
                End If
                objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
                objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
                objclsCashFlow.sACF_Status = "U"
                objclsCashFlow.iACF_Crby = sSession.UserID
                objclsCashFlow.iACF_Updatedby = sSession.UserID
                objclsCashFlow.iACF_Compid = sSession.AccessCodeID
                objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
                objclsCashFlow.iACF_Catagary = 3
                objclsCashFlow.iACF_Yearid = ddlFinancialYear.SelectedValue
                arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
            Next
            ddlbranch_SelectedIndexChanged(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Updated"
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub btnsaveCategory4_Click(sender As Object, e As EventArgs) Handles btnsaveCategory4.Click
        Dim arr() As String
        Dim lblParticulars As New Label
        Dim txtCurrentAmmount As New TextBox
        Dim txtPreviesAmount As New TextBox
        Dim checkdata As Integer = 0
        Try
            For i = 0 To grdCategory4.Rows.Count - 1
                If i > 7 Then
                    'lblpkid = grdCategory4.Rows(i).FindControl("lblpkid")
                    lblParticulars = grdCategory4.Rows(i).FindControl("lblParticulars")
                    txtCurrentAmmount = grdCategory4.Rows(i).FindControl("txtCurrentAmmount")
                    txtPreviesAmount = grdCategory4.Rows(i).FindControl("txtPreviesAmount")

                    checkdata = objclsCashFlow.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, lblParticulars.Text, ddlFinancialYear.SelectedValue, ddlbranch.SelectedValue)
                    If checkdata = 0 Then
                        objclsCashFlow.iACF_pkid = 0
                    Else
                        objclsCashFlow.iACF_pkid = checkdata
                    End If

                    If IsDBNull(lblParticulars.Text.Trim) = False Then
                        objclsCashFlow.sACF_Description = lblParticulars.Text.Trim
                    Else
                        objclsCashFlow.sACF_Description = ""
                    End If
                    If IsDBNull(txtCurrentAmmount.Text) = False Then
                        objclsCashFlow.dACF_Current_Amount = Convert.ToDouble(Val(txtCurrentAmmount.Text).ToString())
                    Else
                        objclsCashFlow.dACF_Current_Amount = 0
                    End If
                    If IsDBNull(txtPreviesAmount.Text) = False Then
                        objclsCashFlow.dACF_Prev_Amount = Convert.ToDouble(Val(txtPreviesAmount.Text).ToString())
                    Else
                        objclsCashFlow.dACF_Prev_Amount = 0
                    End If
                    objclsCashFlow.iACF_Custid = ddlCustomers.SelectedValue
                    objclsCashFlow.iACF_Branchid = ddlbranch.SelectedValue
                    objclsCashFlow.sACF_Status = "U"
                    objclsCashFlow.iACF_Crby = sSession.UserID
                    objclsCashFlow.iACF_Updatedby = sSession.UserID
                    objclsCashFlow.iACF_Compid = sSession.AccessCodeID
                    objclsCashFlow.sACF_Ipaddress = sSession.IPAddress
                    objclsCashFlow.iACF_Catagary = 4
                    objclsCashFlow.iACF_Yearid = ddlFinancialYear.SelectedValue
                    arr = objclsCashFlow.SaveCashFlow(sSession.AccessCode, sSession.AccessCodeID, objclsCashFlow)
                End If
            Next
            ddlbranch_SelectedIndexChanged(sender, e)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalScheduleValidation').modal('show');", True)
            lblExcelValidationMsg.Text = "Data Succesfully Updated"
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub ddlbranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlbranch.SelectedIndexChanged
        Dim dt As New DataTable, dtbranch As New DataTable

        Dim lblParticulars, lblpkid As New Label
        Dim TotaladjustmentsCurrent As Decimal = 0
        Dim TotaladjustmentsPrev As Decimal = 0
        Dim TotalManualCurrent As Decimal = 0
        Dim CurrentAmmount As Decimal
        Dim TotalManualPrev As Decimal = 0
        Dim PrevAmount As Decimal
        Dim TotalCurrentOperativeActivities As Decimal = 0
        Dim TotalPrevOperativeActivities As Decimal = 0
        Dim TotalCurrentInvestingActivities As Decimal = 0
        Dim TotalPrevInvestingActivities As Decimal = 0
        Dim TotalCurrentFinanceActivities As Decimal = 0
        Dim TotalPrevFinanceActivities As Decimal = 0
        Dim TotalCurrentcashequivalents As Decimal = 0
        Dim TotalPrevcashequivalents As Decimal = 0
        Dim TotalCurrentexchange_rate_changes As Decimal = 0
        Dim TotalPrevexchange_rate_changes As Decimal = 0
        Dim TotalCurrentbegining_of_the_year As Decimal = 0
        Dim TotalPrevbegining_of_the_year As Decimal = 0
        Dim TotalCurrentABC As Decimal = 0
        Dim TotalPrevABC As Decimal = 0
        Dim lblCurrenttotaladjustmentwork As Double
        Try
            lblError.Text = ""
            txtParticularsCategory1.Text = ""
            txtCurrentAmountCategory1.Text = ""
            txtPreviesAmountCategory1.Text = ""
            txtParticularsCategory3.Text = ""
            txtCurrentAmountCategory3.Text = ""
            txtPreviesAmountCategory3.Text = ""
            txtParticularsCategory4.Text = ""
            txtCurrentAmountCategory4.Text = ""
            txtPreviesAmountCategory4.Text = ""
            If ddlCustomers.SelectedIndex > 0 Then
                If ddlbranch.SelectedIndex > 0 Then
                    dt = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 1, ddlFinancialYear.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        CurrentAmmount = Convert.ToDecimal(dt.Compute("SUM(CurrentAmmount1)", String.Empty))
                        PrevAmount = Convert.ToDecimal(dt.Compute("SUM(PreviesAmount1)", String.Empty))
                        lblTotalCurrentcategory1.Text = CurrentAmmount
                        lblTotalPrevcategory1.Text = PrevAmount
                    End If

                    dt1 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 2, ddlFinancialYear.SelectedValue)
                    If dt1.Rows.Count > 0 Then
                        For i = 0 To dt1.Rows.Count - 1
                            If IsDBNull(dt1.Rows(i)("CurrentAmmount")) = False Then
                                TotaladjustmentsCurrent = TotaladjustmentsCurrent + dt1.Rows(i)("CurrentAmmount")
                            Else
                                TotaladjustmentsCurrent = TotaladjustmentsCurrent + 0
                            End If
                            If IsDBNull(dt1.Rows(i)("PreviesAmount")) = False Then
                                TotaladjustmentsPrev = TotaladjustmentsPrev + dt1.Rows(i)("PreviesAmount")
                            Else
                                TotaladjustmentsPrev = TotaladjustmentsPrev + 0
                            End If
                            If dt1.Rows(i)("Particulers") = "Operating profit / (loss) after working capital changes" Then
                                dt1.Rows(i)("CurrentAmmount") = TotaladjustmentsCurrent + Val(lblTotalCurrentcategory1.Text)
                                dt1.Rows(i)("PreviesAmount") = TotaladjustmentsPrev + Val(lblTotalPrevcategory1.Text)
                            ElseIf dt1(i)("Particulers") = "Cash generated from operations" Then
                                TotalCurrentOperativeActivities = TotaladjustmentsCurrent + CurrentAmmount
                                TotalPrevOperativeActivities = TotaladjustmentsPrev + PrevAmount
                                dt1.Rows(i)("CurrentAmmount") = TotalCurrentOperativeActivities
                                dt1.Rows(i)("PreviesAmount") = TotalPrevOperativeActivities
                            ElseIf dt1(i)("Particulers") = "Net income tax (paid) / refunds (net)" Then
                                TotalManualCurrent = dt1.Rows(i)("CurrentAmmount")
                                TotalManualPrev = dt1.Rows(i)("PreviesAmount")
                            End If
                        Next
                        lblCurrentOpratingTotal.Text = (TotaladjustmentsCurrent + CurrentAmmount) - TotalManualCurrent
                        lblPrevOpratingTotal.Text = (TotaladjustmentsPrev + PrevAmount) - TotalManualPrev
                    End If
                    dt3 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 3, ddlFinancialYear.SelectedValue)
                    If dt3.Rows.Count > 0 Then
                        CurrentAmmount = Convert.ToDecimal(dt3.Compute("SUM(CurrentAmmount1)", String.Empty))
                        PrevAmount = Convert.ToDecimal(dt3.Compute("SUM(PreviesAmount1)", String.Empty))
                        lblCurrentinvestingactivities.Text = CurrentAmmount
                        lblprevinvestingactivities.Text = PrevAmount
                    Else
                        grdCategory3.DataSource = Nothing
                    End If
                    dt4 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 4, ddlFinancialYear.SelectedValue)
                    If dt4.Rows.Count > 0 Then
                        CurrentAmmount = Convert.ToDecimal(dt4.Compute("SUM(CurrentAmmount1)", String.Empty))
                        PrevAmount = Convert.ToDecimal(dt4.Compute("SUM(PreviesAmount1)", String.Empty))
                        lblCurrentfinancingactivities.Text = CurrentAmmount
                        lblPrevfinancingactivities.Text = PrevAmount
                    End If
                    dt5 = objclsCashFlow.getCashFlowDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustomers.SelectedValue, ddlbranch.SelectedValue, 5, ddlFinancialYear.SelectedValue)

                    If dt5.Rows.Count > 0 Then
                        For i = 0 To dt5.Rows.Count - 1
                            If dt5.Rows(i)("Particulers") = "Net increase (derease) in cash and cash equivalents befor effect of exchange rate changes" Then
                                TotalCurrentexchange_rate_changes = TotalCurrentOperativeActivities + TotalCurrentInvestingActivities + TotalCurrentFinanceActivities
                                TotalPrevexchange_rate_changes = TotalPrevOperativeActivities + TotalPrevInvestingActivities + TotalPrevFinanceActivities
                                dt5.Rows(i)("CurrentAmmount") = TotalCurrentexchange_rate_changes
                                dt5.Rows(i)("PreviesAmount") = TotalPrevexchange_rate_changes
                            ElseIf dt5(i)("Particulers") = "Net increase / (decrease) in Cash and cash equivalents (A+B+C)" Then
                                TotalCurrentABC = Math.Round(Convert.ToDouble(Val(lblCurrentOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblCurrentfinancingactivities.Text)), 2)
                                TotalPrevABC = Math.Round(Convert.ToDouble(Val(lblPrevOpratingTotal.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblprevinvestingactivities.Text)), 2) + Math.Round(Convert.ToDouble(Val(lblPrevfinancingactivities.Text)), 2)
                                dt5.Rows(i)("CurrentAmmount") = TotalCurrentABC
                                dt5.Rows(i)("PreviesAmount") = TotalPrevABC
                            ElseIf dt5(i)("Particulers") = "Cash and cash equivalents at begining of the year" Then
                                TotalCurrentbegining_of_the_year = Val(dt5.Rows(i)("CurrentAmmount")) + TotalCurrentABC
                                TotalPrevbegining_of_the_year = Val(dt5.Rows(i)("PreviesAmount")) + TotalPrevABC
                            ElseIf dt5(i)("Particulers") = "Cash and cash equivalents at Closing of the year" Then
                                dt5.Rows(i)("CurrentAmmount") = TotalCurrentbegining_of_the_year
                                dt5.Rows(i)("PreviesAmount") = TotalPrevbegining_of_the_year
                                'dtIncome = getcashEquivalentCY(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)  '(d) Cash and cash equivalents Closing of the Year
                                'dtExpenses = getcashEquivalentPy(sAC, iCompID, ICustid, iBranchid, iYearid, 4, 147)
                                'If dtIncome.Rows.Count > 0 Then
                                '    dCYProfiTAmt = dtIncome(0)("Dc1")
                                '    dPYProfiTAmt = dtExpenses(0)("Dc1")
                                'Else
                                '    dCYProfiTAmt = 0 : dPYProfiTAmt = 0
                                'End If
                                'Cash.Rows(i)("CurrentAmmount") = Convert.ToDecimal(dCYProfiTAmt).ToString("#,##0.00")
                                '    Cash.Rows(i)("PreviesAmount") = Convert.ToDecimal(dPYProfiTAmt).ToString("#,##0.00")
                            End If
                        Next
                        CurrentAmmount = Convert.ToDecimal(dt5.Compute("SUM(CurrentAmmount)", String.Empty))
                        PrevAmount = Convert.ToDecimal(dt5.Compute("SUM(PreviesAmount)", String.Empty))
                        'lblGrandtotalcurrent.Text = CurrentAmmount
                        'lblPrevfinancingactivities.Text = PrevAmount
                    End If
                Else
                    lblError.Text = "Select Branch"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                    lblExcelValidationMsg.Text = "Select Branch"
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblExcelValidationMsg.Text = "Select Customer"
                Exit Sub
            End If

            If dt3.Rows.Count > 0 Then
                grdCategory3.DataSource = dt3
                grdCategory3.DataBind()
            Else
                grdCategory3.DataSource = Nothing
                grdCategory3.DataBind()
            End If
            If dt.Rows.Count > 0 Or dt1.Rows.Count > 0 Then
                Gvcategory1.DataSource = dt
                Gvcategory1.DataBind()
                GrdviewTotalAmount.DataSource = dt1
                GrdviewTotalAmount.DataBind()
                grdCategory4.DataSource = dt4
                grdCategory4.DataBind()
                grdCategory5.DataSource = dt5
                grdCategory5.DataBind()
            Else
                Gvcategory1.DataSource = Nothing
                Gvcategory1.DataBind()
                GrdviewTotalAmount.DataSource = Nothing
                GrdviewTotalAmount.DataBind()
                grdCategory3.DataSource = Nothing
                grdCategory3.DataBind()
                grdCategory4.DataSource = Nothing
                grdCategory4.DataBind()
                grdCategory5.DataSource = Nothing
                grdCategory5.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlbranch_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub LoadCountriesData()
    '    Dim dt As New DataTable
    '    Try
    '        Dim apiUrl As String = "https://restcountries.com/v3.1/all"
    '        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
    '        Using client As New HttpClient()
    '            Dim response As HttpResponseMessage = client.GetAsync(apiUrl).Result
    '            If response.IsSuccessStatusCode Then
    '                Dim jsonResponse As String = response.Content.ReadAsStringAsync().Result
    '                Dim countriesArray As JArray = JArray.Parse(jsonResponse)
    '                Dim dtCountries As New DataTable()
    '                dtCountries.Columns.Add("Name")
    '                dtCountries.Columns.Add("Capital")
    '                dtCountries.Columns.Add("Population")
    '                For Each country As JObject In countriesArray
    '                    Dim name As String = GetTokenValue(country, "name.common")
    '                    Dim capital As String = GetTokenValue(country, "capital")
    '                    Dim population As String = GetTokenValue(country, "population")
    '                    dtCountries.Rows.Add(name, capital, population)
    '                Next
    '                gvCountries.DataSource = dtCountries
    '                gvCountries.DataBind()
    '            End If
    '        End Using
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCountriesData" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Function GetTokenValue(country As JObject, path As String) As String
        Dim token As JToken = country.SelectToken(path)
        If token IsNot Nothing Then
            Return token.ToString()
        Else
            Return String.Empty
        End If
    End Function
End Class
