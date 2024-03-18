Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Partial Class ScheduleAssignments
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_ScheduleAssignments"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = False Then
                '    Response.Redirect("~/Assignment/EmpAssignmentSubTask.aspx", False)
                'End If

                LoadFinalcialYear(sSession.AccessCode) : BindAssessmentYear()
                BindCustomers() : BindPartners() : BindTasks()
                BindEmployees() : BindFrequency() : BindWorkStatus()
                BindScheduledAssignment()

                txtDueDate.Text = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                RFVCustomerName.InitialValue = "Select Customer" : RFVCustomerName.ErrorMessage = "Select Customer."
                RFVPartner.InitialValue = "Select Partner" : RFVPartner.ErrorMessage = "Select Partner."
                RFVDueDate.ControlToValidate = "txtDueDate" : RFVDueDate.ErrorMessage = "Enter Start Date."
                REVDueDate.ErrorMessage = "Enter valid Date." : REVDueDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVExpectedCompletionDate.ControlToValidate = "txtExpectedCompletionDate" : RFVExpectedCompletionDate.ErrorMessage = "Enter Expected Completion Date."
                REVExpectedCompletionDate.ErrorMessage = "Enter valid Date." : REVExpectedCompletionDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVWorkStatus.InitialValue = "Select Work Status" : RFVWorkStatus.ErrorMessage = "Select Work Status."

                RFVTask.InitialValue = "Select Assignment/Task" : RFVTask.ErrorMessage = "Select Assignment/Task."
                RFVFrequency.InitialValue = "0" : RFVFrequency.ErrorMessage = "Select Frequency."
                RFVEmployee.InitialValue = "Select Employee" : RFVEmployee.ErrorMessage = "Select Employee."
                RFVDescription.ControlToValidate = "txtDescription" : RFVDescription.ErrorMessage = "Enter Description."
                REVDescription.ErrorMessage = "Description exceeded maximum size(max 2000 characters)." : REVDescription.ValidationExpression = "^[\s\S]{0,2000}$"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/Assignment/AssignmentsDashboard.aspx"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlFinancialYear.DataSource = objclsAuditAssignment.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
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
    Private Sub BindAssessmentYear()
        Try
            lstAY.DataSource = objclsAuditAssignment.GetPreviousFinancialYears(sSession.AccessCode, sSession.AccessCodeID, 1)
            lstAY.DataTextField = "Name"
            lstAY.DataValueField = "ID"
            lstAY.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomers()
        Try
            ddlCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataTextField = "CUST_Name"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPartners()
        Try
            ddlPartner.DataSource = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlPartner.DataTextField = "USr_FullName"
            ddlPartner.DataValueField = "USR_ID"
            ddlPartner.DataBind()
            ddlPartner.Items.Insert(0, "Select Partner")
            If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                ddlPartner.SelectedValue = sSession.UserID
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartners" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindFrequency()
        Try
            ddlFrequency.Items.Add(New ListItem("Select Frequency", "0"))
            ddlFrequency.Items.Add(New ListItem("Yearly", "1"))
            ddlFrequency.Items.Add(New ListItem("Monthly", "2"))
            ddlFrequency.Items.Add(New ListItem("Once", "3"))
            'ddlFrequency.Items.Add(New ListItem("Quarterly", "4"))

            ddlFrequency.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFrequency" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindYearMonth(ByVal iFrequency As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim iCurrentMonth As Integer
        Try
            If iFrequency = 1 Then
                dt = objclsAuditAssignment.GetUpcomingFinancialYears(sSession.AccessCode, sSession.AccessCodeID, 0)
            ElseIf iFrequency = 2 Then
                Dim dDate As DateTime

                dt.Columns.Add("ID")
                dt.Columns.Add("Name")

                dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                iCurrentMonth = 4 'dDate.Month

                If iCurrentMonth = 3 Then
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 2 Then
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 1 Then
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 12 Then
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 11 Then
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 10 Then
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 9 Then
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 8 Then
                    dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 7 Then
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
                If iCurrentMonth = 6 Then
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
                If iCurrentMonth = 5 Then
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
                If iCurrentMonth = 4 Then
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
            End If
            If dt.Rows.Count > 6 Then
                divYearMonth.Style.Item("Height") = "157px"
            Else
                divYearMonth.Style.Item("Height") = "auto"
            End If
            gvYearMonth.DataSource = dt
            gvYearMonth.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindScheduledAssignment()
        Try
            ddlAssignmentNo.DataSource = objclsAuditAssignment.LoadScheduledAssignment(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, 0, 0, 0, 1)
            ddlAssignmentNo.DataTextField = "AAS_AssignmentNo"
            ddlAssignmentNo.DataValueField = "AAS_ID"
            ddlAssignmentNo.DataBind()
            ddlAssignmentNo.Items.Insert(0, "Select Assignment No")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindScheduledAssignment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlAssignmentNo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssignmentNo.SelectedIndexChanged
        Try
            ddlCustomerName.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            If ddlAssignmentNo.SelectedIndex > 0 Then
                CheckAndLoadScheduledAssignment()
                chckAdvancePartialBilling.AutoPostBack = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAssignmentNo_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            ddlTask.SelectedIndex = 0
            Clear()
            BindScheduledAssignment()
            CheckAndLoadScheduledAssignment()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            ddlAssignmentNo.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            If ddlCustomerName.SelectedIndex > 0 Then
                CheckAndLoadScheduledAssignment()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlPartner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPartner.SelectedIndexChanged
        Try
            ddlAssignmentNo.SelectedIndex = 0 : ddlTask.SelectedIndex = 0
            Clear()
            If ddlPartner.SelectedIndex > 0 Then
                CheckAndLoadScheduledAssignment()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlPartner_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFrequency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFrequency.SelectedIndexChanged
        Try
            gvYearMonth.DataSource = Nothing
            gvYearMonth.DataBind()
            If ddlFrequency.SelectedIndex > 0 And ddlFrequency.SelectedIndex <> 3 Then
                BindYearMonth(ddlFrequency.SelectedIndex)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFrequency_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlTask_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTask.SelectedIndexChanged
        Try
            ddlAssignmentNo.SelectedIndex = 0
            Clear()
            CheckAndLoadScheduledAssignment()
            If ddlTask.SelectedIndex > 0 Then
                BindSubTaskGrid(ddlTask.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTask_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub Clear()
        Try
            lblError.Text = ""
            ddlWorkStatus.SelectedIndex = 0 : txtDescription.Text = ""
            txtDueDate.Text = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            txtExpectedCompletionDate.Text = ""
            ddlFrequency.SelectedIndex = 0
            gvSubTask.DataSource = Nothing
            gvSubTask.DataBind()
            gvYearMonth.DataSource = Nothing
            gvYearMonth.DataBind()
            'gvEmployeeSubTask.DataSource = Nothing
            'gvEmployeeSubTask.DataBind()
            BindEmployees()
            chckAdvancePartialBilling.Checked = False
            chckAdvancePartialBilling.AutoPostBack = False
            For i = 0 To lstAY.Items.Count - 1
                lstAY.Items(i).Selected = False
            Next
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub CheckAndLoadScheduledAssignment()
        Dim iFinancialYearID As Integer, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iTaskID As Integer = 0
        Dim sAYID As String
        Dim dt As New DataTable
        Try
            iFinancialYearID = ddlFinancialYear.SelectedValue
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlCustomerName.SelectedValue
            End If
            If ddlPartner.SelectedIndex > 0 Then
                iPartnerID = ddlPartner.SelectedValue
            End If
            If ddlTask.SelectedIndex > 0 Then
                iTaskID = ddlTask.SelectedValue
            End If
            If ddlAssignmentNo.SelectedIndex > 0 Then
                dt = objclsAuditAssignment.GetScheduledAssignmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue)
                If dt.Rows.Count > 0 Then
                    ddlFinancialYear.SelectedValue = dt.Rows(0).Item("AAS_YearID")
                    ddlCustomerName.SelectedValue = dt.Rows(0).Item("AAS_CustID")
                    ddlPartner.SelectedValue = dt.Rows(0).Item("AAS_PartnerID")
                    ddlTask.SelectedValue = dt.Rows(0).Item("AAS_TaskID")
                    If ddlTask.SelectedIndex > 0 Then
                        BindSubTaskGrid(ddlTask.SelectedValue)
                    End If
                    If dt.Rows(0).Item("AAS_AdvancePartialBilling") = 1 Then
                        chckAdvancePartialBilling.Checked = True
                    End If
                    If IsDBNull(dt.Rows(0).Item("AAS_AssessmentYearID")) = False Then
                        sAYID = dt.Rows(0).Item("AAS_AssessmentYearID")
                        If sAYID.StartsWith(",") = False Then
                            sAYID = "," & sAYID
                        End If
                        If sAYID.EndsWith(",") = False Then
                            sAYID = sAYID & ","
                        End If
                        For j = 0 To lstAY.Items.Count - 1
                            If sAYID.Contains("," & lstAY.Items(j).Value & ",") = True Then
                                lstAY.Items(j).Selected = True
                            End If
                        Next
                    End If
                End If
                'gvEmployeeSubTask.DataSource = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, 0, 0, 0, 0)
                'gvEmployeeSubTask.DataBind()
            Else
                'gvEmployeeSubTask.DataSource = objclsAuditAssignment.LoadAssignmentSubTaskDetails(sSession.AccessCode, sSession.AccessCodeID, 0, iFinancialYearID, iCustomerID, iPartnerID, iTaskID)
                'gvEmployeeSubTask.DataBind()
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CheckAndLoadScheduledAssignment" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindTasks()
        Try
            ddlTask.DataSource = objclsGeneralFunctions.LoadNonComplianceOrAssignmentTask(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlTask.DataTextField = "cmm_Desc"
            ddlTask.DataValueField = "cmm_ID"
            ddlTask.DataBind()
            ddlTask.Items.Insert(0, "Select Assignment/Task")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTasks" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindEmployees()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)
            ddlEmployee.DataSource = dt
            ddlEmployee.DataTextField = "FullName"
            ddlEmployee.DataValueField = "Usr_ID"
            ddlEmployee.DataBind()
            ddlEmployee.Items.Insert(0, "Select Employee")

            If dt.Rows.Count > 6 Then
                divAssistedByEmployees.Style.Item("Height") = "157px"
            Else
                divAssistedByEmployees.Style.Item("Height") = "auto"
            End If
            gvAssistedByEmployees.DataSource = dt
            gvAssistedByEmployees.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindEmployees" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindWorkStatus()
        Try
            ddlWorkStatus.DataSource = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
            ddlWorkStatus.DataTextField = "Name"
            ddlWorkStatus.DataValueField = "PKID"
            ddlWorkStatus.DataBind()
            ddlWorkStatus.Items.Insert(0, "Select Work Status")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindSubTaskGrid(ByVal iTaskID As Integer)
        Dim dtSubTask As New DataTable
        Try
            dtSubTask = objclsAdminMaster.LoadAuditAssignmentSubTask(sSession.AccessCode, sSession.AccessCodeID, iTaskID)
            If dtSubTask.Rows.Count > 6 Then
                divST.Style.Item("Height") = "157px"
            Else
                divST.Style.Item("Height") = "auto"
            End If
            gvSubTask.DataSource = dtSubTask
            gvSubTask.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSubTaskGrid" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvSubTask_PreRender(sender As Object, e As EventArgs) Handles gvSubTask.PreRender
        Try
            If gvSubTask.Rows.Count > 0 Then
                gvSubTask.UseAccessibleHeader = True
                gvSubTask.HeaderRow.TableSection = TableRowSection.TableHeader
                gvSubTask.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSubTask_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub gvEmployeeSubTask_PreRender(sender As Object, e As EventArgs) Handles gvEmployeeSubTask.PreRender
    '    Try
    '        If gvEmployeeSubTask.Rows.Count > 0 Then
    '            gvEmployeeSubTask.UseAccessibleHeader = True
    '            gvEmployeeSubTask.HeaderRow.TableSection = TableRowSection.TableHeader
    '            gvEmployeeSubTask.FooterRow.TableSection = TableRowSection.TableFooter
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeSubTask_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub gvAssistedByEmployees_PreRender(sender As Object, e As EventArgs) Handles gvAssistedByEmployees.PreRender
        Try
            If gvAssistedByEmployees.Rows.Count > 0 Then
                gvAssistedByEmployees.UseAccessibleHeader = True
                gvAssistedByEmployees.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssistedByEmployees.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssistedByEmployees_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvYearMonth_PreRender(sender As Object, e As EventArgs) Handles gvYearMonth.PreRender
        Try
            If gvYearMonth.Rows.Count > 0 Then
                gvYearMonth.UseAccessibleHeader = True
                gvYearMonth.HeaderRow.TableSection = TableRowSection.TableHeader
                gvYearMonth.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvYearMonth_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.Header Then
            Dim cell As TableCell = e.Row.Cells(1)
            If ddlFrequency.SelectedIndex = 1 Then
                cell.Text = "Financial Year"
            End If
            If ddlFrequency.SelectedIndex = 2 Then
                cell.Text = "Month"
            End If
        End If
    End Sub
    Protected Sub chkSelectAllSubTask_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectSubTask As New CheckBox
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To gvSubTask.Rows.Count - 1
                    chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                    chkSelectSubTask.Checked = True
                Next
            Else
                For i = 0 To gvSubTask.Rows.Count - 1
                    chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                    chkSelectSubTask.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllSubTask_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAssistedByEmployees_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectAssistedByEmployees As New CheckBox
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To gvAssistedByEmployees.Rows.Count - 1
                    chkSelectAssistedByEmployees = gvAssistedByEmployees.Rows(i).FindControl("chkSelectAssistedByEmployees")
                    chkSelectAssistedByEmployees.Checked = True
                Next
            Else
                For i = 0 To gvAssistedByEmployees.Rows.Count - 1
                    chkSelectAssistedByEmployees = gvAssistedByEmployees.Rows(i).FindControl("chkSelectAssistedByEmployees")
                    chkSelectAssistedByEmployees.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAssistedByEmployees_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAllYearMonth_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectYearMonth As New CheckBox
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To gvYearMonth.Rows.Count - 1
                    chkSelectYearMonth = gvYearMonth.Rows(i).FindControl("chkSelectYearMonth")
                    chkSelectYearMonth.Checked = True
                Next
            Else
                For i = 0 To gvYearMonth.Rows.Count - 1
                    chkSelectYearMonth = gvYearMonth.Rows(i).FindControl("chkSelectYearMonth")
                    chkSelectYearMonth.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllYearMonth_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As EventArgs) Handles imgbtnSave.Click
        Dim chkSelectSubTask As New CheckBox, chkSelectYearMonth As New CheckBox
        Dim iCheckSubTask As Integer = 0, iCheckYearMonth As Integer = 0, iCheckAY As Integer = 0
        Dim Array() As String
        Try
            lblError.Text = ""
            For i = 0 To gvSubTask.Rows.Count - 1
                chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                If chkSelectSubTask.Checked = True Then
                    iCheckSubTask = 1
                End If
            Next
            If iCheckSubTask = 0 Then
                lblError.Text = "Select Sub Task." : lblAAValidationMsg.Text = "Select Sub Task."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAAValidation').modal('show');", True)
                Exit Try
            End If

            For i = 0 To gvYearMonth.Rows.Count - 1
                chkSelectYearMonth = gvYearMonth.Rows(i).FindControl("chkSelectYearMonth")
                If chkSelectYearMonth.Checked = True Then
                    iCheckYearMonth = 1
                End If
            Next
            If iCheckYearMonth = 0 Then
                If ddlFrequency.SelectedIndex = 1 Then
                    lblError.Text = "Select Year." : lblAAValidationMsg.Text = "Select Year."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAAValidation').modal('show');", True)
                    Exit Try
                ElseIf ddlFrequency.SelectedIndex = 2 Then
                    lblError.Text = "Select Month." : lblAAValidationMsg.Text = "Select Month."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAAValidation').modal('show');", True)
                    Exit Try
                End If
            End If
            Dim dDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dDueDate As DateTime = Date.ParseExact(txtDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim d1 As Integer
            d1 = DateDiff(DateInterval.Day, dDate, dDueDate)
            If d1 < 0 Then
                lblError.Text = "Start Date should be greater than or equal to Current Date."
                lblAAValidationMsg.Text = "Start Date should be greater than or equal to Current Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAAValidation').modal('show');", True)
                txtDueDate.Focus()
                Exit Try
            End If

            Dim dExpectedCompletionDate As DateTime = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim d2 As Integer
            d2 = DateDiff(DateInterval.Day, dDueDate, dExpectedCompletionDate)
            If d2 < 0 Then
                lblError.Text = "Expected Completion Date should be greater than or equal to Start Date."
                lblAAValidationMsg.Text = "Expected Completion Date should be greater than or equal to Start Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAAValidation').modal('show');", True)
                txtExpectedCompletionDate.Focus()
                Exit Try
            End If
            If ddlFrequency.SelectedIndex = 2 And d2 > 180 Then
                lblError.Text = "Expected Completion Date should be less than or equal to 180 days of Start Date."
                lblAAValidationMsg.Text = "Expected Completion Date should be less than or equal to 180 days of Start Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAAValidation').modal('show');", True)
                txtExpectedCompletionDate.Focus()
                Exit Try
            End If
            If ddlFrequency.SelectedIndex = 3 Then
                'For i = 0 To lstAY.Items.Count - 1
                '    If lstAY.Items(i).Selected = True Then
                '        iCheckAY = 1
                '    End If
                'Next
                'If iCheckAY = 0 Then
                '    lblError.Text = "Select atleast one Assessment Year." : lblAAValidationMsg.Text = "Select atleast one Assessment Year."
                '    lstAY.Focus()
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAAValidation').modal('show');", True)
                '    Exit Try
                'End If
                Array = SaveScheduleAssignmentsDetailsForAY()
            Else
                Array = SaveScheduleAssignmentsDetails()
            End If
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Schedule", "Assignment Saved", sSession.YearID, ddlCustomerName.SelectedValue, ddlTask.SelectedValue, Array(1), sSession.IPAddress)

            lblError.Text = "Successfully Saved." : lblAAValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)

            Clear()
            BindScheduledAssignment()
            CheckAndLoadScheduledAssignment()
            If ddlTask.SelectedIndex > 0 Then
                BindSubTaskGrid(ddlTask.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function GetSelectedAssistedByEmployeeIDs() As String
        Dim i As Integer
        Dim chkSelectAssistedByEmployees As New CheckBox
        Dim lblAssistedByEmployeeID As New Label
        Dim sAssistedByEmployeeIDs As String = ""
        Try
            For i = 0 To gvAssistedByEmployees.Rows.Count - 1
                chkSelectAssistedByEmployees = gvAssistedByEmployees.Rows(i).FindControl("chkSelectAssistedByEmployees")
                lblAssistedByEmployeeID = gvAssistedByEmployees.Rows(i).FindControl("lblAssistedByEmployeeID")
                If chkSelectAssistedByEmployees.Checked = True Then
                    sAssistedByEmployeeIDs = sAssistedByEmployeeIDs & "," & lblAssistedByEmployeeID.Text
                End If
            Next
            sAssistedByEmployeeIDs = sAssistedByEmployeeIDs & ","
            Return sAssistedByEmployeeIDs
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSelectedResourceIDs" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Function
    Private Function GetSelectedAYIDs() As String
        Dim i As Integer
        Dim sAYIDs As String = ""
        Try
            For i = 0 To lstAY.Items.Count - 1
                If lstAY.Items(i).Selected = True Then
                    sAYIDs = sAYIDs & "," & lstAY.Items(i).Value
                End If
            Next
            sAYIDs = sAYIDs & ","
            Return sAYIDs
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetSelectedResourceIDs" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Function
    Private Function SaveScheduleAssignmentsDetailsForAY() As Array
        Dim ArrAAS() As String, ArrAAST() As String
        Dim chkSelectSubTask As New CheckBox
        Dim lblSubTaskID As New Label
        Dim i As Integer
        Dim dDueDate As DateTime, dExpectedCompletionDate As DateTime
        Dim objAAEST As New strAuditAssignment_EmpSubTask
        Dim Arr() As String
        Try
            dDueDate = Date.ParseExact(txtDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dExpectedCompletionDate = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim objAAS As New strAuditAssignment_Schedule
            objAAS.iAAS_ID = 0
            objAAS.iAAS_CustID = ddlCustomerName.SelectedValue
            objAAS.sAAS_AssignmentNo = ""
            objAAS.iAAS_PartnerID = ddlPartner.SelectedValue
            objAAS.iAAS_YearID = ddlFinancialYear.SelectedValue
            objAAS.iAAS_MonthID = dDueDate.Month
            objAAS.iAAS_TaskID = ddlTask.SelectedValue
            objAAS.iAAS_Status = 0
            If chckAdvancePartialBilling.Checked = True Then
                objAAS.iAAS_AdvancePartialBilling = 1
            Else
                objAAS.iAAS_AdvancePartialBilling = 0
            End If
            objAAS.iAAS_BillingType = 0
            objAAS.sAAS_AssessmentYearID = GetSelectedAYIDs()
            objAAS.iAAS_AttachID = 0
            objAAS.iAAS_CrBy = sSession.UserID
            objAAS.sAAS_IPAddress = sSession.IPAddress
            objAAS.iAAS_CompID = sSession.AccessCodeID
            objAAS.iAAS_IsComplianceAsg = 0
            ArrAAS = objclsAuditAssignment.SaveScheduleAssignmentsDetails(sSession.AccessCode, objAAS, ddlFinancialYear.SelectedItem.Text)
            objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, ArrAAS(1))

            For i = 0 To gvSubTask.Rows.Count - 1
                chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                lblSubTaskID = gvSubTask.Rows(i).FindControl("lblSubTaskID")
                If chkSelectSubTask.Checked = True Then
                    Dim objAAST As New strAuditAssignment_SubTask
                    objAAST.iAAST_AAS_ID = ArrAAS(1)
                    objAAST.iAAST_SubTaskID = Val(lblSubTaskID.Text)
                    objAAST.iAAST_EmployeeID = ddlEmployee.SelectedValue
                    objAAST.sAAST_AssistedByEmployeesID = GetSelectedAssistedByEmployeeIDs()
                    objAAST.sAAST_Desc = objclsGRACeGeneral.SafeSQL(txtDescription.Text.Trim)
                    objAAST.iAAST_FrequencyID = ddlFrequency.SelectedIndex
                    objAAST.iAAST_YearOrMonthID = dDueDate.Month
                    objAAST.dAAST_DueDate = dDueDate
                    objAAST.dAAST_ExpectedCompletionDate = dExpectedCompletionDate
                    objAAST.iAAST_WorkStatusID = ddlWorkStatus.SelectedValue
                    objAAST.iAAST_CrBy = sSession.UserID
                    objAAST.sAAST_IPAddress = sSession.IPAddress
                    objAAST.iAAST_CompID = sSession.AccessCodeID
                    ArrAAST = objclsAuditAssignment.SaveAuditAssignmentEmpSubTask(sSession.AccessCode, objAAST)

                    If ddlWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                        objAAEST.iAAEST_ID = 0
                        objAAEST.iAAEST_AAS_ID = ArrAAS(1)
                        objAAEST.iAAEST_AAST_ID = ArrAAST(1)
                        objAAEST.iAAEST_WorkStatusID = ddlWorkStatus.SelectedValue
                        objAAEST.iAAST_Closed = 1
                        objAAEST.iAAST_Review = 0
                        objAAEST.sAAEST_Comments = "Auto Completed"
                        objAAEST.iAAEST_AttachID = 0
                        objAAEST.iAAEST_CrBy = sSession.UserID
                        objAAEST.dAAEST_CrOn = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        objAAEST.sAAEST_IPAddress = sSession.IPAddress
                        objAAEST.iAAEST_CompID = sSession.AccessCodeID
                        Arr = objclsAuditAssignment.SaveEmployeeSubTaskDetails(sSession.AccessCode, objAAEST)
                    End If
                End If
            Next
            If ddlWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, ArrAAS(1), 1)
                objclsAuditAssignment.UpdateScheduledStatusAndFolderPath(sSession.AccessCode, sSession.AccessCodeID, "", ArrAAS(1), 0)
                objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, ArrAAS(1))
            End If
            Return ArrAAS
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveScheduleAssignmentsDetails" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Function SaveScheduleAssignmentsDetails() As Array
        Dim ArrAAS() As String, ArrAAST() As String
        Dim chkSelectSubTask As New CheckBox, chkSelectYearMonth As New CheckBox
        Dim lblSubTaskID As New Label, lblYearMonthID As New Label, lblYearMonth As New Label
        Dim i As Integer, j As Integer, iLastDayOfMonth As Integer
        Dim dDueDate As DateTime, dExpectedCompletionDate As DateTime
        Dim sNewDueDate As String, sNewExpectedCompletionDate As String, sYearName As String
        Dim objAAEST As New strAuditAssignment_EmpSubTask
        Dim Arr() As String
        Try
            Dim dDate As DateTime = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            For j = 0 To gvYearMonth.Rows.Count - 1
                chkSelectYearMonth = gvYearMonth.Rows(j).FindControl("chkSelectYearMonth")
                lblYearMonthID = gvYearMonth.Rows(j).FindControl("lblYearMonthID")
                lblYearMonth = gvYearMonth.Rows(j).FindControl("lblYearMonth")
                If chkSelectYearMonth.Checked = True Then
                    dDueDate = Date.ParseExact(txtDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dExpectedCompletionDate = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                    Dim objAAS As New strAuditAssignment_Schedule
                    objAAS.iAAS_ID = 0
                    objAAS.iAAS_CustID = ddlCustomerName.SelectedValue
                    objAAS.sAAS_AssignmentNo = ""
                    objAAS.iAAS_PartnerID = ddlPartner.SelectedValue
                    sYearName = sSession.YearName
                    If ddlFrequency.SelectedIndex = 1 Then
                        objAAS.iAAS_YearID = Convert.ToInt32(lblYearMonthID.Text)
                        objAAS.iAAS_MonthID = dDueDate.Month
                        sYearName = lblYearMonth.Text
                    ElseIf ddlFrequency.SelectedIndex = 2 Then
                        objAAS.iAAS_YearID = ddlFinancialYear.SelectedValue
                        objAAS.iAAS_MonthID = Convert.ToInt32(lblYearMonthID.Text)
                        sYearName = ddlFinancialYear.SelectedItem.Text
                    End If
                    objAAS.iAAS_TaskID = ddlTask.SelectedValue
                    objAAS.iAAS_Status = 0
                    If chckAdvancePartialBilling.Checked = True Then
                        objAAS.iAAS_AdvancePartialBilling = 1
                    Else
                        objAAS.iAAS_AdvancePartialBilling = 0
                    End If
                    objAAS.iAAS_BillingType = 0
                    objAAS.sAAS_AssessmentYearID = ""
                    objAAS.iAAS_AttachID = 0
                    objAAS.iAAS_CrBy = sSession.UserID
                    objAAS.sAAS_IPAddress = sSession.IPAddress
                    objAAS.iAAS_CompID = sSession.AccessCodeID
                    objAAS.iAAS_IsComplianceAsg = 0
                    ArrAAS = objclsAuditAssignment.SaveScheduleAssignmentsDetails(sSession.AccessCode, objAAS, sYearName)
                    objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, ArrAAS(1))

                    If ddlFrequency.SelectedIndex = 1 Then
                        If dDueDate.Month = 1 Or dDueDate.Month = 2 Or dDueDate.Month = 3 Then
                            sNewDueDate = String.Format("{0}/{1}/{2}", dDueDate.Day.ToString().PadLeft(2, "0"c), dDueDate.Month.ToString().PadLeft(2, "0"c), lblYearMonth.Text.Substring(5, 4))
                            dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", dExpectedCompletionDate.Day.ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Month.ToString().PadLeft(2, "0"c), lblYearMonth.Text.Substring(5, 4))
                            Try
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Catch ex As Exception
                                iLastDayOfMonth = Date.DaysInMonth(lblYearMonth.Text.Substring(5, 4), dExpectedCompletionDate.Month)
                                sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth.ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Month.ToString().PadLeft(2, "0"c), lblYearMonth.Text.Substring(5, 4))
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End Try
                        Else
                            sNewDueDate = String.Format("{0}/{1}/{2}", dDueDate.Day.ToString().PadLeft(2, "0"c), dDueDate.Month.ToString().PadLeft(2, "0"c), lblYearMonth.Text.Substring(0, 4))
                            dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

                            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", dExpectedCompletionDate.Day.ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Month.ToString().PadLeft(2, "0"c), lblYearMonth.Text.Substring(0, 4))
                            Try
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Catch ex As Exception
                                iLastDayOfMonth = Date.DaysInMonth(lblYearMonth.Text.Substring(0, 4), dExpectedCompletionDate.Month)
                                sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth.ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Month.ToString().PadLeft(2, "0"c), lblYearMonth.Text.Substring(0, 4))
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End Try
                        End If
                    ElseIf ddlFrequency.SelectedIndex = 2 Then
                        Dim NumOfMonths As Long = DateDiff(DateInterval.Month, dDueDate, dExpectedCompletionDate)

                        If lblYearMonthID.Text = "01" Or lblYearMonthID.Text = "02" Or lblYearMonthID.Text = "03" Then
                            sNewDueDate = String.Format("{0}/{1}/{2}", dDueDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), ddlFinancialYear.SelectedItem.Text.Substring(5, 4))
                            Try
                                dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Catch ex As Exception
                                iLastDayOfMonth = Date.DaysInMonth(ddlFinancialYear.SelectedItem.Text.Substring(5, 4), Convert.ToInt32(lblYearMonthID.Text))
                                sNewDueDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, lblYearMonthID.Text.ToString(), ddlFinancialYear.SelectedItem.Text.Substring(5, 4))
                                dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End Try

                            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", dExpectedCompletionDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), ddlFinancialYear.SelectedItem.Text.Substring(5, 4))
                            Try
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).AddMonths(NumOfMonths)
                            Catch ex As Exception
                                If (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) > 12 Then
                                    iLastDayOfMonth = Date.DaysInMonth(ddlFinancialYear.SelectedItem.Text.Substring(5, 4), (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12)
                                    sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, ((Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12).ToString().PadLeft(2, "0"c), ddlFinancialYear.SelectedItem.Text.Substring(5, 4))
                                Else
                                    iLastDayOfMonth = Date.DaysInMonth(ddlFinancialYear.SelectedItem.Text.Substring(5, 4), Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths)
                                    sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths).ToString().PadLeft(2, "0"c), ddlFinancialYear.SelectedItem.Text.Substring(5, 4))
                                End If
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End Try
                        Else
                            sNewDueDate = String.Format("{0}/{1}/{2}", dDueDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), ddlFinancialYear.SelectedItem.Text.Substring(0, 4))
                            Try
                                dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            Catch ex As Exception
                                iLastDayOfMonth = Date.DaysInMonth(ddlFinancialYear.SelectedItem.Text.Substring(0, 4), Convert.ToInt32(lblYearMonthID.Text))
                                sNewDueDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, lblYearMonthID.Text.ToString(), ddlFinancialYear.SelectedItem.Text.Substring(0, 4))
                                dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End Try
                            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", dExpectedCompletionDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), ddlFinancialYear.SelectedItem.Text.Substring(0, 4))
                            Try
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).AddMonths(NumOfMonths)
                            Catch ex As Exception
                                If (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) > 12 Then
                                    iLastDayOfMonth = Date.DaysInMonth(ddlFinancialYear.SelectedItem.Text.Substring(0, 4) + 1, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12)
                                    sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, ((Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12).ToString().PadLeft(2, "0"c), ddlFinancialYear.SelectedItem.Text.Substring(0, 4) + 1)
                                Else
                                    iLastDayOfMonth = Date.DaysInMonth(ddlFinancialYear.SelectedItem.Text.Substring(0, 4), Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths)
                                    sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths).ToString().PadLeft(2, "0"c), ddlFinancialYear.SelectedItem.Text.Substring(0, 4))
                                End If
                                dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            End Try
                        End If

                        'Dim NumOfMonths As Long = DateDiff(DateInterval.Month, dDueDate, dExpectedCompletionDate)
                        'Dim iAddYearForDueDate As Integer = 0
                        'If dDueDate.Month >= 3 And dDate.Year < dDueDate.Year Then
                        '    iAddYearForDueDate = 1
                        'End If
                        'Dim iAddYearForExpectedCompletionDate As Integer = 0
                        'If dExpectedCompletionDate.Month >= 3 And dDueDate.Year < dExpectedCompletionDate.Year Then
                        '    iAddYearForExpectedCompletionDate = 1
                        'End If

                        'If lblYearMonthID.Text = "01" Or lblYearMonthID.Text = "02" Or lblYearMonthID.Text = "03" Then
                        '    sNewDueDate = String.Format("{0}/{1}/{2}", dDueDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), dDueDate.Year + iAddYearForDueDate)
                        '    Try
                        '        dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '    Catch ex As Exception
                        '        iLastDayOfMonth = Date.DaysInMonth(dExpectedCompletionDate.Year, Convert.ToInt32(lblYearMonthID.Text))
                        '        sNewDueDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, lblYearMonthID.Text.ToString(), dDueDate.Year + iAddYearForDueDate)
                        '        dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '    End Try

                        '    sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", dExpectedCompletionDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), dExpectedCompletionDate.Year + iAddYearForExpectedCompletionDate)
                        '    Try
                        '        dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).AddMonths(NumOfMonths)
                        '    Catch ex As Exception
                        '        If (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) > 12 Then
                        '            iLastDayOfMonth = Date.DaysInMonth(dExpectedCompletionDate.Year + iAddYearForExpectedCompletionDate, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12)
                        '            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, ((Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12).ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Year + iAddYearForExpectedCompletionDate)
                        '        Else
                        '            iLastDayOfMonth = Date.DaysInMonth(dExpectedCompletionDate.Year, Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths)
                        '            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths).ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Year.ToString())
                        '        End If
                        '        dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '    End Try
                        'Else
                        '    sNewDueDate = String.Format("{0}/{1}/{2}", dDueDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), dDueDate.Year.ToString())
                        '    Try
                        '        dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '    Catch ex As Exception
                        '        iLastDayOfMonth = Date.DaysInMonth(dExpectedCompletionDate.Year, Convert.ToInt32(lblYearMonthID.Text))
                        '        sNewDueDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, lblYearMonthID.Text.ToString(), dDueDate.Year.ToString())
                        '        dDueDate = Date.ParseExact(sNewDueDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '    End Try
                        '    sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", dExpectedCompletionDate.Day.ToString().PadLeft(2, "0"c), lblYearMonthID.Text.ToString(), dExpectedCompletionDate.Year.ToString())
                        '    Try
                        '        dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture).AddMonths(NumOfMonths)
                        '    Catch ex As Exception
                        '        If (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) > 12 Then
                        '            iLastDayOfMonth = Date.DaysInMonth(dExpectedCompletionDate.Year + 1, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12)
                        '            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, ((Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths) - 12).ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Year + 1)
                        '        Else
                        '            iLastDayOfMonth = Date.DaysInMonth(dExpectedCompletionDate.Year, Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths)
                        '            sNewExpectedCompletionDate = String.Format("{0}/{1}/{2}", iLastDayOfMonth, (Convert.ToInt32(lblYearMonthID.Text) + NumOfMonths).ToString().PadLeft(2, "0"c), dExpectedCompletionDate.Year.ToString())
                        '        End If
                        '        dExpectedCompletionDate = Date.ParseExact(sNewExpectedCompletionDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                        '    End Try
                        'End If
                    End If

                    For i = 0 To gvSubTask.Rows.Count - 1
                        chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                        lblSubTaskID = gvSubTask.Rows(i).FindControl("lblSubTaskID")
                        If chkSelectSubTask.Checked = True Then
                            Dim objAAST As New strAuditAssignment_SubTask
                            objAAST.iAAST_AAS_ID = ArrAAS(1)
                            objAAST.iAAST_SubTaskID = Val(lblSubTaskID.Text)
                            objAAST.iAAST_EmployeeID = ddlEmployee.SelectedValue
                            objAAST.sAAST_AssistedByEmployeesID = GetSelectedAssistedByEmployeeIDs()
                            objAAST.sAAST_Desc = objclsGRACeGeneral.SafeSQL(txtDescription.Text.Trim)
                            objAAST.iAAST_FrequencyID = ddlFrequency.SelectedIndex
                            objAAST.iAAST_YearOrMonthID = Convert.ToInt32(lblYearMonthID.Text)
                            objAAST.dAAST_DueDate = dDueDate
                            objAAST.dAAST_ExpectedCompletionDate = dExpectedCompletionDate
                            objAAST.iAAST_WorkStatusID = ddlWorkStatus.SelectedValue
                            objAAST.iAAST_CrBy = sSession.UserID
                            objAAST.sAAST_IPAddress = sSession.IPAddress
                            objAAST.iAAST_CompID = sSession.AccessCodeID
                            ArrAAST = objclsAuditAssignment.SaveAuditAssignmentEmpSubTask(sSession.AccessCode, objAAST)

                            If ddlWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                                objAAEST.iAAEST_ID = 0
                                objAAEST.iAAEST_AAS_ID = ArrAAS(1)
                                objAAEST.iAAEST_AAST_ID = ArrAAST(1)
                                objAAEST.iAAEST_WorkStatusID = ddlWorkStatus.SelectedValue
                                objAAEST.iAAST_Closed = 1
                                objAAEST.iAAST_Review = 0
                                objAAEST.sAAEST_Comments = "Auto Completed"
                                objAAEST.iAAEST_AttachID = 0
                                objAAEST.iAAEST_CrBy = sSession.UserID
                                objAAEST.dAAEST_CrOn = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                                objAAEST.sAAEST_IPAddress = sSession.IPAddress
                                objAAEST.iAAEST_CompID = sSession.AccessCodeID
                                Arr = objclsAuditAssignment.SaveEmployeeSubTaskDetails(sSession.AccessCode, objAAEST)
                            End If
                        End If
                    Next
                    If ddlWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                        objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, ArrAAS(1), 1)
                        objclsAuditAssignment.UpdateScheduledStatusAndFolderPath(sSession.AccessCode, sSession.AccessCodeID, "", ArrAAS(1), 0)
                        objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, ArrAAS(1))
                    End If
                End If
            Next
            Return ArrAAS
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveScheduleAssignmentsDetails" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    'Private Sub gvEmployeeSubTask_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvEmployeeSubTask.RowDeleting
    '    Dim lblDBpkId As Label, lblWorkStatus As New Label
    '    Try
    '        lblError.Text = ""
    '        Dim index As Integer = Convert.ToInt32(e.RowIndex)
    '        lblDBpkId = gvEmployeeSubTask.Rows(index).FindControl("lblDBpkId")
    '        lblWorkStatus = gvEmployeeSubTask.Rows(index).FindControl("lblWorkStatus")
    '        If lblWorkStatus.Text = "Completed" Then
    '            lblError.Text = "Cannot delete Completed status." : lblAAValidationMsg.Text = "Cannot delete Completed status."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
    '            Exit Sub
    '        End If
    '        If Val(lblDBpkId.Text) > 0 Then
    '            objclsAuditAssignment.DeletedScheduledAsgEmpSubTask(sSession.AccessCode, sSession.AccessCodeID, Val(lblDBpkId.Text))
    '            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Schedule", "Sub Task Deleted", Val(lblDBpkId.Text), "", 0, "", sSession.IPAddress)
    '        End If
    '        ddlTask_SelectedIndexChanged(sender, e)
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmployeeSubTask_RowDeleting" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Protected Sub chckAdvancePartialBilling_CheckedChanged(sender As Object, e As EventArgs) Handles chckAdvancePartialBilling.CheckedChanged
        Dim iAdvancePartialBilling As Integer = 0
        Try
            lblError.Text = ""
            If ddlAssignmentNo.SelectedIndex > 0 Then
                If chckAdvancePartialBilling.Checked = True Then
                    iAdvancePartialBilling = 1
                End If
                objclsAuditAssignment.UpdateScheduledAsgAdvancePartialBillingDetails(sSession.AccessCode, sSession.AccessCodeID, ddlAssignmentNo.SelectedValue, iAdvancePartialBilling)
                lblError.Text = "Successfully updated Advance/Partial Billing details." : lblAAValidationMsg.Text = "Successfully updated Advance/Partial Billing details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAAValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chckAdvancePartialBilling_CheckedChanged" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/Masters/CustomerDetails.aspx"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class