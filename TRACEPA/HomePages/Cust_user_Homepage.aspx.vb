Imports BusinesLayer
Imports System.Security.Cryptography.X509Certificates

Public Class Cust_user_Homepage
    Inherits System.Web.UI.Page
    Private sFormName As String = "HomePages_Cust_user_Homepage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclshome As New clsHomeDashboard
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsStandardAudit As clsStandardAudit
    Private objclsDRLLog As New clsDRLLog
    Private Shared sSession As AllSession
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private Shared iYearID As Integer
    Private Shared bLoginUserIsPartner As Boolean
    Public Shared sFilePath As String = ""
    Public Shared strarray As Array = {(0), (1)}
    Private obclsUL As New clsUploadLedger

    Private objclsGRACePermission As New clsGRACePermission
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleReport As New clsScheduleReport
    Private objclsScheduleTemplate As New clsScheduleTemplate
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If
                LoadFinalcialYear(sSession.AccessCode)
                BindCustomerName(sender, e) : BindAuditNo(0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
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
                sSession.YearID = ddlFinancialYear.SelectedValue
                sSession.YearName = ddlFinancialYear.SelectedItem.Text
                Session("AllSession") = sSession
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomerName(sender As Object, e As EventArgs)
        Try
            ddlCustName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataTextField = "CUST_Name"
            ddlCustName.DataValueField = "CUST_ID"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name")
            If sSession.UserLoginCustID > 0 Then
                ddlCustName.SelectedValue = sSession.UserLoginCustID
                ddlCustName.Enabled = False
                ddlCustName_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Public Sub BindAuditNo(ByVal iCustID As Integer)
        Try
            If ddlCustName.SelectedIndex > 0 Then
                iCustID = ddlCustName.SelectedValue
            End If
            ddlAuditNos.DataSource = objclshome.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, sSession.UserID, True)
            ddlAuditNos.DataTextField = "SA_AuditNo"
            ddlAuditNos.DataValueField = "SA_ID"
            ddlAuditNos.DataBind()
            ddlAuditNos.Items.Insert(0, "Select Audit No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAuditNo" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim Orgtype As Integer = 0
        Dim ProgStatus As String = ""
        Dim dt1 As New System.Data.DataSet
        Try

            If ddlCustName.SelectedIndex > 0 Then
                BindAuditNo(ddlCustName.SelectedValue)
            Else
                lblError.Text = "Select Customer"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlAuditNos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNos.SelectedIndexChanged
        Try
            If ddlAuditNos.SelectedIndex > 0 Then
                LoadCustrecievedremarksHistory()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNos_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadCustrecievedremarksHistory()
        Try
            Dim dt1, dt2, dt3 As New DataSet
            Dim dt As New DataTable
            dt2 = objclsDRLLog.LoadCustrecievedremarksHistory(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlAuditNos.SelectedValue, ddlFinancialYear.SelectedValue)
            dt1 = obclsUL.LoadLedgerObservationsCommentsHomepage(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, ddlAuditNos.SelectedValue, ddlFinancialYear.SelectedValue)
            dt1.Merge(dt2, True)
            GVCustremarks.DataSource = dt1
            GVCustremarks.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
            'Throw
        End Try
    End Sub
    Private Sub GVCustremarks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GVCustremarks.RowDataBound
        Try
            Dim lblnotification, lblCommentsby As New Label
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblnotification = (TryCast(e.Row.FindControl("lblnotification"), Label))
                lblnotification.Visible = False
                lblCommentsby = (TryCast(e.Row.FindControl("lblCommentsby"), Label))
                If e.Row.RowIndex = 0 And lblCommentsby.Text = "Auditor" Then
                    'e.Row.Style.Add("height", "min-content")
                    lblnotification.Visible = True
                    'lblnotificationHeader.Visible = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVCustremarks_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub GVCustremarks_PreRender(sender As Object, e As EventArgs) Handles GVCustremarks.PreRender
        Dim dt As New DataTable
        Try
            If GVCustremarks.Rows.Count > 0 Then
                GVCustremarks.UseAccessibleHeader = True
                GVCustremarks.HeaderRow.TableSection = TableRowSection.TableHeader
                GVCustremarks.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GVCustremarks_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class