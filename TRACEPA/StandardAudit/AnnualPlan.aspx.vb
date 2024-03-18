Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Partial Class AnnualPlan
    Inherits System.Web.UI.Page
    Private sFormName As String = "StandardAudit_AnnualPlan"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditChecklist As New clsAuditChecklist
    Private objclsStandardAudit As New clsStandardAudit

    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iAuditID As Integer
    Private Shared iAuditStatusID As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If
                LoadFinalcialYear(sSession.AccessCode)
                BindAllStandardAuditScheduled(ddlFinancialYear.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsStandardAudit.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YearID"
            ddlFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlFinancialYear.SelectedValue = iYearID
                    Else
                        ddlFinancialYear.SelectedIndex = 0
                    End If
                Else
                    ddlFinancialYear.SelectedValue = sSession.YearID
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            BindAllStandardAuditScheduled(ddlFinancialYear.SelectedValue)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindAllStandardAuditScheduled(ByVal iFinancialYearID As Integer)
        Dim dt As New DataTable
        Try
            dt = objclsStandardAudit.LoadPanAndSheduleAudit(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID)
            gvPlanShedule.DataSource = dt
            gvPlanShedule.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllStandardAuditScheduled" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPlanShedule_PreRender(sender As Object, e As EventArgs) Handles gvPlanShedule.PreRender
        Try
            If gvPlanShedule.Rows.Count > 0 Then
                gvPlanShedule.UseAccessibleHeader = True
                gvPlanShedule.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPlanShedule.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPlanShedule_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvPlanShedule_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim chkSelectAsgSubTask As New CheckBox
        Dim lblCustomerName As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblCustomerName = CType(e.Row.FindControl("lblCustomerName"), Label)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPlanShedule_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPlanShedule_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPlanShedule.RowCommand
        Dim lnkCustomerName As New LinkButton
        Dim lblCustomerID As New Label : Dim lblCheckPointID As New Label : Dim lblAuditID As New Label
        Dim oCustomerID As Object, oAuditID As Object, oCheckPointID As Object
        Try
            If e.CommandName = "Select" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblCustomerID = CType(clickedRow.FindControl("lblCustomerID"), Label)
                lblCheckPointID = CType(clickedRow.FindControl("lblCheckPointID"), Label)
                lblAuditID = CType(clickedRow.FindControl("lblAuditID"), Label)
                oAuditID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAuditID.Text)))
                oCheckPointID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblCheckPointID.Text)))
                oCustomerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblCustomerID.Text)))
                Response.Redirect(String.Format("~/StandardAudit/DashboardAndSchedule.aspx?AuditID={0}&CheckPointID={1}&CustID={2}", oAuditID, oCheckPointID, oCustomerID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPlanShedule_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class