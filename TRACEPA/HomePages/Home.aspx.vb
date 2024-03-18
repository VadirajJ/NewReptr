Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms
Imports System.IO
Imports System.DirectoryServices
Partial Class Home1
    Inherits System.Web.UI.Page
    Private sFormName As String = "HomePages_Home"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclshome As New clsHomeDashboard
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsStandardAudit As clsStandardAudit
    Private objclsDRLLog As New clsDRLLog
    Private Shared sSession As AllSession
    Private Shared iYearID As Integer
    Private Shared bLoginUserIsPartner As Boolean
    Public Shared sFilePath As String = ""
    Public Shared strarray As Array = {(0), (1)}
    Private obclsUL As New clsUploadLedger

    Private objclsGRACePermission As New clsGRACePermission
    Dim objUT As New ClsUploadTailBal
    Dim objclsSchduleReport As New clsScheduleReport
    Private objclsScheduleTemplate As New clsScheduleTemplate
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            'Dim userNames As List(Of String) = CheckloginADUserNames()
            'CheckloginADUserNames()
            'Dim username As String = HttpContext.Current.User.Identity.Name
            'Dim Name As String = System.Security.Principal.WindowsIdentity.GetCurrent().Name
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

                LoadExistingCustomer()  'BindAuditNo(0)
                LoadReviewAssignmentTaskDetails()
                LoadCompletedAssignmentTaskDetails()
                LoadPendingAssignmentTaskDetails()
                LoadUnbilledAssignmentTaskDetails()
                LoadUserTimeLineDetails()
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
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Shared Function CheckloginADUserNames() As List(Of String)
        Dim userNames As New List(Of String)

        ' Replace "LDAP://yourdomain.com" with the actual LDAP path of your Active Directory domain
        Dim ldapPath As String = "LDAP://mmcspl.com"
        Try
            Using rootEntry As New DirectoryEntry(ldapPath)
                Using searcher As New DirectorySearcher(rootEntry)
                    ' Specify the filter to get user objects
                    searcher.Filter = "(&(objectClass=user)(objectCategory=person))"

                    ' Perform the search
                    Dim resultCollection As SearchResultCollection = searcher.FindAll()

                    ' Iterate through the search results and retrieve user names
                    For Each result As SearchResult In resultCollection
                        Dim userName As String = result.Properties("sAMAccountName")(0).ToString()
                        userNames.Add(userName)
                    Next
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions that may occur during the process
            ' For example, log the exception or display an error message
            ' based on your application's requirements.
        End Try

        Return userNames
    End Function
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
    Public Sub BindAuditNo(ByVal iCustID As Integer)
        Try
            ddlAuditNos.DataSource = objclshome.LoadScheduledAuditNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCustID, sSession.UserID, bLoginUserIsPartner)
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

            UlProgressbas.Visible = False
            lblError.Text = ""
            liCustAssgn.Attributes.Remove("class")
            liRpyFormat.Attributes.Remove("class")
            If ddlCustName.SelectedIndex > 0 Then
                GVCustremarks.DataSource = Nothing
                GVCustremarks.DataBind()
                BindAuditNo(ddlCustName.SelectedValue)
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
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustName.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                sSession.CustomerID = ddlCustName.SelectedValue
                sSession.CustomerID = ddlCustName.SelectedValue
                Session("AllSession") = sSession
                ProgStatus = ""
                If dt.Rows.Count > 0 Then
                    liCustAssgn.Attributes.Remove("class")
                    ProgStatus = "FormatC"
                    liRpyFormat.Attributes.Add("class", "is-active")
                Else
                    'lblError.Text = "No Data Found. Please create a format"
                    Exit Sub
                End If
                dt1 = objUT.GetCustCOAMasterDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedValue, sSession.YearID, 0, 0, "0")
                dt = dt1.Tables(0)
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
    Private Sub GVCustremarks_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GVCustremarks.RowDataBound
        Try
            Dim lblnotification, lblCommentsby As New Label
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblnotification = (TryCast(e.Row.FindControl("lblnotification"), Label))
                lblnotification.Visible = False
                lblCommentsby = (TryCast(e.Row.FindControl("lblCommentsby"), Label))
                If e.Row.RowIndex = 0 And lblCommentsby.Text = "Client" Then
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

    Private Sub ddlAuditNos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAuditNos.SelectedIndexChanged
        Try
            If ddlAuditNos.SelectedIndex > 0 Then
                LoadCustrecievedremarksHistory()

            Else
                GVCustremarks.DataSource = Nothing
                GVCustremarks.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAuditNos_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
