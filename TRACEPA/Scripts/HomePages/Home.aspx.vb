Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Partial Class Home1
    Inherits System.Web.UI.Page
    Private sFormName As String = "HomePages_Home"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private Shared sSession As AllSession
    Private Shared iYearID As Integer
    Private Shared bLoginUserIsPartner As Boolean
    Public Shared sFilePath As String = ""
    Public Shared strarray As Array = {(0), (1)}

    Private objclsGRACePermission As New clsGRACePermission
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleReport As New clsScheduleReport
    Private objclsScheduleTemplate As New clsScheduleTemplate
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                bLoginUserIsPartner = False
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                txtCompletionDate.Text = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                txtFromDate.Text = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                RFVCompletionDate.ControlToValidate = "txtCompletionDate" : RFVCompletionDate.ErrorMessage = "Enter Date."
                REVCompletionDate.ErrorMessage = "Enter valid Date." : REVCompletionDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                LoadFinalcialYear(sSession.AccessCode)
                BindMonths() : LoadExistingCustomer()
                LoadReviewAssignmentTaskDetails()
                LoadCompletedAssignmentTaskDetails()
                LoadPendingAssignmentTaskDetails()
                LoadUnbilledAssignmentTaskDetails()
                LoadRevenueAssignmentTaskDetails()
                LoadMonthlyPerformanceAssignmentTaskDetails()
                LoadUserTimeLineDetails()

                If sSession.CustomerID <> 0 Then
                    ddlCustName.SelectedValue = sSession.CustomerID
                    If ddlCustName.SelectedIndex > 0 Then
                        ddlCustName_SelectedIndexChanged(sender, e)
                    End If
                End If
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
    Private Sub BindMonths()
        Dim dDate As DateTime
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim iCurrentMonth As Integer = 3
        Try
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If iYearID = ddlFinancialYear.SelectedValue Then
                iCurrentMonth = dDate.Month
            End If

            dt.Columns.Add("ID")
            dt.Columns.Add("Name")
            If iCurrentMonth = 4 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 5 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 6 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 7 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 8 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 9 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 10 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 11 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 12 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 1 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 2 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If
            If iCurrentMonth = 3 Then
                dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
            End If

            ddlMonthRevenue.DataSource = dt
            ddlMonthRevenue.DataTextField = "Name"
            ddlMonthRevenue.DataValueField = "ID"
            ddlMonthRevenue.DataBind()
            ddlMonthRevenue.SelectedValue = dDate.Month

            ddlMonthlyPerformance.DataSource = dt
            ddlMonthlyPerformance.DataTextField = "Name"
            ddlMonthlyPerformance.DataValueField = "ID"
            ddlMonthlyPerformance.DataBind()
            ddlMonthlyPerformance.SelectedValue = dDate.Month
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            sSession.YearID = ddlFinancialYear.SelectedValue
            sSession.YearName = ddlFinancialYear.SelectedItem.Text
            Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Home.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvReviewAssignment_PreRender(sender As Object, e As EventArgs) Handles gvReviewAssignment.PreRender
        Dim dt As New DataTable
        Try
            If gvReviewAssignment.Rows.Count > 0 Then
                gvReviewAssignment.UseAccessibleHeader = True
                gvReviewAssignment.HeaderRow.TableSection = TableRowSection.TableHeader
                gvReviewAssignment.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReviewAssignment_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvReviewAssignment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvReviewAssignment.RowCommand
        Dim lblAssignmentID As New Label, lblTaskID As New Label
        Dim oAssignmentID As New Object, oFinancialYearID As New Object
        Dim oPartnerID As New Object, oTaskID As New Object, oBackID As New Object
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblAssignmentID = CType(clickedRow.FindControl("lblAssignmentID"), Label)
            lblTaskID = CType(clickedRow.FindControl("lblTaskID"), Label)
            If e.CommandName = "Select" Then
                oAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAssignmentID.Text)))
                oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlFinancialYear.SelectedValue))
                oPartnerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sSession.UserID))
                oTaskID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblTaskID.Text)))
                oBackID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                Response.Redirect(String.Format("~/Assignment/EmpAssignmentSubTask.aspx?AssignmentID={0}&FinancialYearID={1}&PartnerID={2}&TaskID={3}&BackID={4}", oAssignmentID, oFinancialYearID, oPartnerID, oTaskID, oBackID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReviewAssignment_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadReviewAssignmentTaskDetails()
        Dim dt As New DataTable
        Try
            gvReviewAssignment.DataSource = Nothing
            gvReviewAssignment.DataBind()
            dt = objclsAuditAssignment.LoadReviewAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, bLoginUserIsPartner)
            If dt.Rows.Count > 0 Then
                gvReviewAssignment.DataSource = dt
                gvReviewAssignment.DataBind()
            End If
            lblWIP.Text = "- " & dt.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadReviewAssignmentTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCompletionAssignment_PreRender(sender As Object, e As EventArgs) Handles gvCompletionAssignment.PreRender
        Dim dt As New DataTable
        Try
            If gvCompletionAssignment.Rows.Count > 0 Then
                gvCompletionAssignment.UseAccessibleHeader = True
                gvCompletionAssignment.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCompletionAssignment.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompletionAssignment_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCompletionAssignment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCompletionAssignment.RowCommand
        Dim lblAssignmentID As New Label, lblTaskID As New Label
        Dim oAssignmentID As New Object, oFinancialYearID As New Object
        Dim oPartnerID As New Object, oTaskID As New Object, oBackID As New Object
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblAssignmentID = CType(clickedRow.FindControl("lblAssignmentID"), Label)
            lblTaskID = CType(clickedRow.FindControl("lblTaskID"), Label)
            If e.CommandName = "Select" Then
                oAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAssignmentID.Text)))
                oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlFinancialYear.SelectedValue))
                oPartnerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sSession.UserID))
                oTaskID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblTaskID.Text)))
                oBackID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                Response.Redirect(String.Format("~/Assignment/EmpAssignmentSubTask.aspx?AssignmentID={0}&FinancialYearID={1}&PartnerID={2}&TaskID={3}&BackID={4}", oAssignmentID, oFinancialYearID, oPartnerID, oTaskID, oBackID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompletionAssignment_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            LoadCompletedAssignmentTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLoad_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadCompletedAssignmentTaskDetails()
        Dim dt As New DataTable
        Dim dCompletionDate As Date
        Try
            dCompletionDate = Date.ParseExact(txtCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dt = objclsAuditAssignment.LoadCompletedAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, dCompletionDate, bLoginUserIsPartner)
            gvCompletionAssignment.DataSource = dt
            gvCompletionAssignment.DataBind()
            lblCompAssgn.Text = "- " & dt.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCompletedAssignmentTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPendingAssignment_PreRender(sender As Object, e As EventArgs) Handles gvPendingAssignment.PreRender
        Dim dt As New DataTable
        Try
            If gvPendingAssignment.Rows.Count > 0 Then
                gvPendingAssignment.UseAccessibleHeader = True
                gvPendingAssignment.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPendingAssignment.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPendingAssignment_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPendingAssignment_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPendingAssignment.RowCommand
        Dim lblAssignmentID As New Label, lblTaskID As New Label
        Dim oAssignmentID As New Object, oFinancialYearID As New Object
        Dim oPartnerID As New Object, oTaskID As New Object, oBackID As New Object
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblAssignmentID = CType(clickedRow.FindControl("lblAssignmentID"), Label)
            lblTaskID = CType(clickedRow.FindControl("lblTaskID"), Label)
            If e.CommandName = "Select" Then
                oAssignmentID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblAssignmentID.Text)))
                oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlFinancialYear.SelectedValue))
                oPartnerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sSession.UserID))
                oTaskID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(lblTaskID.Text)))
                oBackID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(1))
                Response.Redirect(String.Format("~/Assignment/EmpAssignmentSubTask.aspx?AssignmentID={0}&FinancialYearID={1}&PartnerID={2}&TaskID={3}&BackID={4}", oAssignmentID, oFinancialYearID, oPartnerID, oTaskID, oBackID), False)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPendingAssignment_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadPendingAssignmentTaskDetails()
        Dim dt As New DataTable
        Dim dtPendingChartdetails As New DataTable
        Try
            dt = objclsAuditAssignment.LoadPendingAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, bLoginUserIsPartner)
            gvPendingAssignment.DataSource = dt
            gvPendingAssignment.DataBind()

            dtPendingChartdetails = objclsAuditAssignment.LoadPendingAssignmentTaskChartDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, bLoginUserIsPartner)
            sFilePath = ""
            'If dtPendingChartdetails.Rows.Count > 0 Then
            Try
                sFilePath = Server.MapPath("../") & "Json\PendingTask"
                If Directory.Exists(sFilePath) = False Then
                    Directory.CreateDirectory(sFilePath)
                End If

                Dim files() As String = Directory.GetFileSystemEntries(sFilePath)
                For Each element As String In files
                    If System.IO.File.Exists(element) = True Then
                        Try
                            File.Delete(element)
                        Catch ex As Exception
                        End Try
                    End If
                Next

                Dim fs As FileStream = File.Create(sFilePath + "/jsonPendingBARChart.json")
                Dim jsonstring As String = DataTableToJSONWithStringBuilder(dtPendingChartdetails)
                Dim info As Byte() = New UTF8Encoding(True).GetBytes(jsonstring.ToString)
                fs.Write(info, 0, info.Length)
                fs.Close()
                'System.IO.File.WriteAllText(sFilePath + "/jsonBARChart1.json", jsonstring.ToString)
                'lblYES.Visible = True
                'lblNote.Visible = True
                'lblYES.Text = ""
                'For i = -0 To dtRatingNames.Rows.Count - 1
                '    lblYES.Text = lblYES.Text & dtRatingNames.Rows(i)("Rating_Name") & "=" & dtRatingNames.Rows(i)("Id") & vbCrLf
                'Next
                strarray = New String() {ddlMonthlyPerformance.SelectedItem.Text}
            Catch ex As Exception
                Throw
            End Try
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadPendingAssignmentTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvUnbilledAssignment_PreRender(sender As Object, e As EventArgs) Handles gvUnbilledAssignment.PreRender
        Dim dt As New DataTable
        Try
            If gvUnbilledAssignment.Rows.Count > 0 Then
                gvUnbilledAssignment.UseAccessibleHeader = True
                gvUnbilledAssignment.HeaderRow.TableSection = TableRowSection.TableHeader
                gvUnbilledAssignment.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUnbilledAssignment_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadUnbilledAssignmentTaskDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAuditAssignment.LoadUnbilledAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, bLoginUserIsPartner)
            gvUnbilledAssignment.DataSource = dt
            gvUnbilledAssignment.DataBind()
            lblUnBilledTask.Text = "- " & dt.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadUnbilledAssignmentTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvRevenue_PreRender(sender As Object, e As EventArgs) Handles gvRevenue.PreRender
        Dim dt As New DataTable
        Try
            If gvRevenue.Rows.Count > 0 Then
                gvRevenue.UseAccessibleHeader = True
                gvRevenue.HeaderRow.TableSection = TableRowSection.TableHeader
                gvRevenue.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvRevenue_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadRevenueAssignmentTaskDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAuditAssignment.LoadRevenueAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, ddlMonthRevenue.SelectedIndex + 1, bLoginUserIsPartner)
            gvRevenue.DataSource = dt
            gvRevenue.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadRevenueAssignmentTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function DataTableToJSONWithStringBuilder(ByVal table As DataTable) As String
        Dim JSONString = New StringBuilder()

        If table.Rows.Count > 0 Then
            JSONString.Append("[")

            For i As Integer = 0 To table.Rows.Count - 1
                JSONString.Append("{")

                For j As Integer = 0 To table.Columns.Count - 1

                    If j < table.Columns.Count - 1 Then
                        JSONString.Append("""" & table.Columns(j).ColumnName.ToString() & """:" & """" + table.Rows(i)(j).ToString() & """,")
                    ElseIf j = table.Columns.Count - 1 Then
                        JSONString.Append("""" & table.Columns(j).ColumnName.ToString() & """:" & """" + table.Rows(i)(j).ToString() & """")
                    End If
                Next

                If i = table.Rows.Count - 1 Then
                    JSONString.Append("}")
                Else
                    JSONString.Append("},")
                End If
            Next

            JSONString.Append("]")
        End If

        Return JSONString.ToString()
    End Function
    Protected Sub ddlMonthRevenue_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonthRevenue.SelectedIndexChanged
        Try
            LoadRevenueAssignmentTaskDetails()
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMonthRevenue_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvMonthlyPerformance_PreRender(sender As Object, e As EventArgs) Handles gvMonthlyPerformance.PreRender
        Dim dt As New DataTable
        Try
            If gvMonthlyPerformance.Rows.Count > 0 Then
                gvMonthlyPerformance.UseAccessibleHeader = True
                gvMonthlyPerformance.HeaderRow.TableSection = TableRowSection.TableHeader
                gvMonthlyPerformance.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvMonthlyPerformance_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadMonthlyPerformanceAssignmentTaskDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAuditAssignment.LoadMonthlyPerformanceAssignmentTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sSession.UserID, ddlMonthlyPerformance.SelectedIndex + 1, bLoginUserIsPartner)
            gvMonthlyPerformance.DataSource = dt
            gvMonthlyPerformance.DataBind()
            sFilePath = ""
            'If dt.Rows.Count > 0 Then
            Try
                sFilePath = Server.MapPath("../") & "Json"
                If Directory.Exists(sFilePath) = False Then
                    Directory.CreateDirectory(sFilePath)
                End If

                Dim files() As String = Directory.GetFileSystemEntries(sFilePath)
                For Each element As String In files
                    If System.IO.File.Exists(element) = True Then
                        Try
                            File.Delete(element)
                        Catch ex As Exception
                        End Try
                    End If
                Next
                Dim fs As FileStream = File.Create(sFilePath + "/jsonBARChart.json")
                Dim jsonstring As String = DataTableToJSONWithStringBuilder(dt)
                Dim info As Byte() = New UTF8Encoding(True).GetBytes(jsonstring.ToString)
                fs.Write(info, 0, info.Length)
                fs.Close()
                'System.IO.File.WriteAllText(sFilePath + "/jsonBARChart1.json", jsonstring.ToString)
                'lblYES.Visible = True
                'lblNote.Visible = True
                'lblYES.Text = ""
                'For i = -0 To dtRatingNames.Rows.Count - 1
                '    lblYES.Text = lblYES.Text & dtRatingNames.Rows(i)("Rating_Name") & "=" & dtRatingNames.Rows(i)("Id") & vbCrLf
                'Next
                strarray = New String() {ddlMonthlyPerformance.SelectedItem.Text}
            Catch ex As Exception
                Throw
            End Try
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadMonthlyPerformanceAssignmentTaskDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlMonthlyPerformance_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonthlyPerformance.SelectedIndexChanged
        Try
            LoadMonthlyPerformanceAssignmentTaskDetails()
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMonthlyPerformance_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPendingAssignment_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPendingAssignment.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex = 0 Then
                    'e.Row.Style.Add("height", "min-content")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPendingAssignment_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub LoadUserTimeLineDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAuditAssignment.GetUserTimelineData(sSession.AccessCode, sSession.AccessCodeID, txtFromDate.Text, sSession.UserID, bLoginUserIsPartner)
            gvUserTimeline.DataSource = dt
            gvUserTimeline.DataBind()
            lblUsertimeln.Text = "- " & dt.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadUserTimeLineDetails" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub txtFromDate_TextChanged(sender As Object, e As EventArgs) Handles txtFromDate.TextChanged
        Dim dt As New DataTable
        Try
            dt = objclsAuditAssignment.GetUserTimelineData(sSession.AccessCode, sSession.AccessCodeID, txtFromDate.Text, sSession.UserID, bLoginUserIsPartner)
            gvUserTimeline.DataSource = dt
            gvUserTimeline.DataBind()
            lblUsertimeln.Text = "- " & dt.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "txtFromDate_TextChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvUserTimeline_PreRender(sender As Object, e As EventArgs) Handles gvUserTimeline.PreRender
        Dim dt As New DataTable
        Try
            If gvUserTimeline.Rows.Count > 0 Then
                gvUserTimeline.UseAccessibleHeader = True
                gvUserTimeline.HeaderRow.TableSection = TableRowSection.TableHeader
                gvUserTimeline.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUserTimeline_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim dt As New DataTable
        Dim Orgtype As Integer = 0
        Dim ProgStatus As String = ""
        Try
            UlProgressbas.Visible = False
            lblError.Text = ""
            liCustAssgn.Attributes.Remove("class")
            liRpyFormat.Attributes.Remove("class")
            If ddlCustName.SelectedIndex > 0 Then
                Orgtype = objclsSchduleReport.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue)
                If Orgtype = 0 Then
                    lblError.Text = "Please assign Customer type to the custmer"
                    Exit Sub
                Else
                    ProgStatus = "AssignC"
                    liCustAssgn.Attributes.Add("class", "is-active")
                End If
                dt = objclsScheduleTemplate.LoadSchedulegrid(sSession.AccessCode, sSession.AccessCodeID, 0, Orgtype)
                UlProgressbas.Visible = True
                sSession.CustomerID = ddlCustName.SelectedValue
                Session("AllSession") = sSession
                ProgStatus = ""
                If dt.Rows.Count > 0 Then
                    liCustAssgn.Attributes.Remove("class")
                    ProgStatus = "FormatC"
                    liRpyFormat.Attributes.Add("class", "is-active")
                Else
                    lblError.Text = "No Data Found. Please create a format"
                    Exit Sub
                End If
                dt = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID, 0, 0)
                If dt.Rows.Count > 0 Then
                    liCustAssgn.Attributes.Remove("class")
                    liRpyFormat.Attributes.Remove("class")
                    ProgStatus = "UplaodSchedule"
                    lirptgen.Attributes.Remove("class")
                Else
                    lblError.Text = "No Data Found. Upload Trail Balance For this Customer"
                    Exit Sub
                End If
                dt = objUT.LoadItemsfromJE(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID)
                If dt.Rows.Count > 0 Then
                    ProgStatus = "JeExist"
                    'lirptJe.Attributes.Add("class", "is-active")
                    lirptDownload.Attributes.Remove("class")
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Private Sub imgbtnSKUser_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKUser.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Masters/EProfile.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKUser_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKCustomer_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKCustomer.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Masters/CustomerDetails.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKCustomer_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKAsgDashboard_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKAsgDashboard.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Assignment/AssignmentsDashboard.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKAsgDashboard_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKSchedule_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKSchedule.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Assignment/ScheduleAssignments.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKSchedule_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKTasks_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKTasks.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Assignment/EmpAssignmentSubTask.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKTasks_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKReports_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKReports.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Assignment/DynamicReports.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKReports_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKCompliance_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKCompliance.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Assignment/ComplianceAsgTask.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKCompliance_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    'Private Sub imgbtnSKInvoice_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSKInvoice.Click
    '    Try
    '        lblError.Text = ""
    '        Response.Redirect(String.Format("~/Assignment/Invoice.aspx"), False)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSKInvoice_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
End Class
