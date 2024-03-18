Imports System
Imports System.Data
Imports System.Drawing
Imports System.Web.UI.DataVisualization.Charting
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Public Class ComplianceAsgtasknew
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_ComplianceAsgTask"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAdminMaster As New clsAdminMaster
    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared chkAssignbtn As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAssign.ImageUrl = "~/Images/Edit24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                chkAssignbtn = 0
                bLoginUserIsPartner = False
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                LoadFinalcialYear(sSession.AccessCode) : BindCustomers() : BindFrequency()
                BindPartner() : BindEmployees() : BindActDetails()
                BindWorkStatus()

                rboUnAssigned_CheckedChanged(sender, e)
                RFVTaskPartner.InitialValue = "Select Partner" : RFVTaskPartner.ErrorMessage = "Select Partner."
                RFVTaskEmployee.InitialValue = "Select Employee" : RFVTaskEmployee.ErrorMessage = "Select Employee."
                RFVTaskWorkStatus.InitialValue = "Select Work Status" : RFVTaskWorkStatus.ErrorMessage = "Select Work Status."
                RFVDueDate.ControlToValidate = "txtDueDate" : RFVDueDate.ErrorMessage = "Enter Start Date."
                REVDueDate.ErrorMessage = "Enter valid Date." : REVDueDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVExpectedCompletionDate.ControlToValidate = "txtExpectedCompletionDate" : RFVExpectedCompletionDate.ErrorMessage = "Enter Expected Completion Date."
                REVExpectedCompletionDate.ErrorMessage = "Enter valid Date." : REVExpectedCompletionDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadFinalcialYear(ByVal sAC As String)
        Dim iYearID As Integer
        Try
            ddlComplianceFinancialYear.DataSource = objclsAuditAssignment.GetAddYearTo2DigitFinancialYear(sAC, sSession.AccessCodeID, 0)
            ddlComplianceFinancialYear.DataTextField = "YMS_ID"
            ddlComplianceFinancialYear.DataValueField = "YMS_YearID"
            ddlComplianceFinancialYear.DataBind()
            Try
                If sSession.YearID = 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sAC, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlComplianceFinancialYear.SelectedValue = iYearID
                    Else
                        ddlComplianceFinancialYear.SelectedValue = 0
                    End If
                Else
                    ddlComplianceFinancialYear.SelectedValue = sSession.YearID
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindCustomers()
        Try
            ddlComplianceCustomerName.DataSource = objclsAllActiveMaster.LoadActiveCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlComplianceCustomerName.DataTextField = "CUST_Name"
            ddlComplianceCustomerName.DataValueField = "CUST_ID"
            ddlComplianceCustomerName.DataBind()
            ddlComplianceCustomerName.Items.Insert(0, "Select Customer")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomers" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindFrequency()
        Try
            ddlFrequency.Items.Add(New ListItem("Select Frequency", "0"))
            ddlFrequency.Items.Add(New ListItem("Yearly", "1"))
            ddlFrequency.Items.Add(New ListItem("Quarterly", "4"))
            ddlFrequency.Items.Add(New ListItem("Monthly", "2"))
            'ddlFrequency.Items.Add(New ListItem("Once", "3"))
            ddlFrequency.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindFrequency" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindWorkStatus()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
            ddlTaskWorkStatus.DataSource = dt
            ddlTaskWorkStatus.DataTextField = "Name"
            ddlTaskWorkStatus.DataValueField = "PKID"
            ddlTaskWorkStatus.DataBind()
            ddlTaskWorkStatus.Items.Insert(0, "Select Work Status")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFrequency_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFrequency.SelectedIndexChanged
        Try
            lblError.Text = ""
            divFrequenctDetails.Visible = False
            ddlFrequencyDetails.Items.Clear()

            If ddlFrequency.SelectedValue = 1 Then
                BindComplianceAsgTaskDetails()
            ElseIf ddlFrequency.SelectedValue > 1 Then
                divFrequenctDetails.Visible = True
                BindQuarterlyMonth(ddlFrequency.SelectedValue)
                BindComplianceAsgTaskDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFrequency_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindQuarterlyMonth(ByVal iFrequency As Integer)
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim iCurrentMonth As Integer
        Try
            dt.Columns.Add("ID")
            dt.Columns.Add("Name")

            Dim dDate As DateTime
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'If rboAssigned.Checked = True Then
            iCurrentMonth = 4
            'Else
            '    iCurrentMonth = dDate.Month
            'End If
            If iFrequency = 4 Then
                lblHFrequenctDetails.Text = "Quarterly"
                If iCurrentMonth = 4 Or iCurrentMonth = 5 Or iCurrentMonth = 6 Then
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "April,May,June-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "July,August,September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "October,November,December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "January,February,March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 7 Or iCurrentMonth = 8 Or iCurrentMonth = 9 Then
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "July,August,September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "October,November,December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "January,February,March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 10 Or iCurrentMonth = 11 Or iCurrentMonth = 12 Then
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "October,November,December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "January,February,March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 1 Or iCurrentMonth = 2 Or iCurrentMonth = 3 Then
                    dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "January,February,March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
            ElseIf iFrequency = 2 Then
                lblHFrequenctDetails.Text = "Monthly"
                If iCurrentMonth = 3 Then
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 2 Then
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 1 Then
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 12 Then
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 11 Then
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 10 Then
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 9 Then
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 8 Then
                    dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 7 Then
                    dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 6 Then
                    dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 5 Then
                    dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
                If iCurrentMonth = 4 Then
                    dr = dt.NewRow() : dr("ID") = "04" : dr("Name") = "April-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "05" : dr("Name") = "May-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "06" : dr("Name") = "June-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "07" : dr("Name") = "July-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "08" : dr("Name") = "August-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "09" : dr("Name") = "September-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "10" : dr("Name") = "October-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "11" : dr("Name") = "November-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "12" : dr("Name") = "December-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "01" : dr("Name") = "January-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "02" : dr("Name") = "February-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                    dr = dt.NewRow() : dr("ID") = "03" : dr("Name") = "March-" + ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4) : dt.Rows.Add(dr)
                End If
            End If
            ddlFrequencyDetails.DataSource = dt
            ddlFrequencyDetails.DataTextField = "Name"
            ddlFrequencyDetails.DataValueField = "ID"
            ddlFrequencyDetails.DataBind()
            If iFrequency = 4 Then
                ddlFrequencyDetails.Items.Insert(0, "Select Quarterly")
            ElseIf iFrequency = 2 Then
                ddlFrequencyDetails.Items.Insert(0, "Select Monthly")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindPartner()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompliancePartner.DataSource = dt
            ddlCompliancePartner.DataTextField = "USr_FullName"
            ddlCompliancePartner.DataValueField = "USR_ID"
            ddlCompliancePartner.DataBind()
            ddlCompliancePartner.Items.Insert(0, "Select Partner")
            'If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
            '    ddlCompliancePartner.SelectedValue = sSession.UserID
            'End If

            ddlTaskPartner.DataSource = dt
            ddlTaskPartner.DataTextField = "USr_FullName"
            ddlTaskPartner.DataValueField = "USR_ID"
            ddlTaskPartner.DataBind()
            ddlTaskPartner.Items.Insert(0, "Select Partner")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindPartner" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindEmployees()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveEmployeesUsers(sSession.AccessCode, sSession.AccessCodeID)

            ddlComplainceEmployee.DataSource = dt
            ddlComplainceEmployee.DataTextField = "FullName"
            ddlComplainceEmployee.DataValueField = "Usr_ID"
            ddlComplainceEmployee.DataBind()
            ddlComplainceEmployee.Items.Insert(0, "Select Employee")

            ddlTaskEmployee.DataSource = dt
            ddlTaskEmployee.DataTextField = "FullName"
            ddlTaskEmployee.DataValueField = "Usr_ID"
            ddlTaskEmployee.DataBind()
            ddlTaskEmployee.Items.Insert(0, "Select Employee")

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
    'Public Sub BindWorkStatus()
    '    Try
    '        ddlTaskWorkStatus.DataSource = objclsAdminMaster.LoadAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, "WS")
    '        ddlTaskWorkStatus.DataTextField = "Name"
    '        ddlTaskWorkStatus.DataValueField = "PKID"
    '        ddlTaskWorkStatus.DataBind()
    '        ddlTaskWorkStatus.Items.Insert(0, "Select Work Status")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Sub BindActDetails()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadAct(sSession.AccessCode, sSession.AccessCodeID)
            ddlAct.DataSource = dt
            ddlAct.DataTextField = "CMM_Act"
            ddlAct.DataBind()
            ddlAct.Items.Insert(0, "Select Act")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindActDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvUnAssigned_PreRender(sender As Object, e As EventArgs) Handles gvUnAssigned.PreRender
        Try
            If gvUnAssigned.Rows.Count > 0 Then
                gvUnAssigned.UseAccessibleHeader = True
                gvUnAssigned.HeaderRow.TableSection = TableRowSection.TableHeader
                gvUnAssigned.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUnAssigned_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssigned_PreRender(sender As Object, e As EventArgs) Handles gvAssigned.PreRender
        Try
            If gvAssigned.Rows.Count > 0 Then
                gvAssigned.UseAccessibleHeader = True
                gvAssigned.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssigned.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssigned_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub BindComplianceAsgTaskDetails()
        Dim dtTab As New DataTable
        Dim iFinancialYearID As Integer, iCustomerID As Integer = 0, iPartnerID As Integer = 0, iEmployeeID As Integer = 0, iFrequencyID As Integer = 0, iFrequencyDetailsID As Integer = 0
        Dim sAct As String = ""
        Try
            lblError.Text = ""
            gvUnAssigned.DataSource = Nothing
            gvUnAssigned.DataBind()
            gvAssigned.DataSource = Nothing
            gvAssigned.DataBind()

            iFinancialYearID = ddlComplianceFinancialYear.SelectedValue
            If ddlComplianceCustomerName.SelectedIndex > 0 Then
                iCustomerID = ddlComplianceCustomerName.SelectedValue
            End If
            If ddlCompliancePartner.SelectedIndex > 0 Then
                iPartnerID = ddlCompliancePartner.SelectedValue
            End If
            If ddlComplainceEmployee.SelectedIndex > 0 Then
                iEmployeeID = ddlComplainceEmployee.SelectedValue
            End If
            If ddlFrequency.SelectedIndex > 0 Then
                iFrequencyID = ddlFrequency.SelectedValue
            End If
            If ddlFrequency.SelectedIndex > 1 And ddlFrequencyDetails.SelectedIndex > 0 Then
                iFrequencyDetailsID = ddlFrequencyDetails.SelectedValue
            End If
            If ddlAct.SelectedIndex > 0 Then
                sAct = ddlAct.SelectedItem.Text
            End If

            If rboUnAssigned.Checked = True And ddlFrequency.SelectedValue > 1 And ddlFrequencyDetails.SelectedIndex > 0 Then
                dtTab = objclsAuditAssignment.LoadUnAssignedTaskDetails(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iFrequencyID, iFrequencyDetailsID, sAct)
                gvUnAssigned.DataSource = dtTab
                gvUnAssigned.DataBind()
            ElseIf rboUnAssigned.Checked = True And ddlFrequency.SelectedValue = 1 Then
                dtTab = objclsAuditAssignment.LoadUnAssignedTaskDetails(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iFrequencyID, 0, sAct)
                gvUnAssigned.DataSource = dtTab
                gvUnAssigned.DataBind()
            ElseIf rboAssigned.Checked = True Then
                dtTab = objclsAuditAssignment.LoadAssignedTaskDetails(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCustomerID, iPartnerID, sAct, iEmployeeID, iFrequencyID, iFrequencyDetailsID, bLoginUserIsPartner, sSession.UserID)
                gvAssigned.DataSource = dtTab
                gvAssigned.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSelectedDateScheduledAssignmentEmpWise" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlComplianceFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlComplianceFinancialYear.SelectedIndexChanged
        Try
            ddlFrequency_SelectedIndexChanged(sender, e)
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlComplianceFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlFrequencyDetails_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFrequencyDetails.SelectedIndexChanged
        Try
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFrequencyDetails_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlComplianceCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlComplianceCustomerName.SelectedIndexChanged
        Try
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlComplianceCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlAct_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAct.SelectedIndexChanged
        Try
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAct_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCompliancePartner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompliancePartner.SelectedIndexChanged
        Try
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompliancePartner_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlComplainceEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlComplainceEmployee.SelectedIndexChanged
        Try
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlComplainceEmployee_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboUnAssigned_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboUnAssigned.CheckedChanged
        Try
            lblError.Text = ""
            chkAssignbtn = 0
            divAssigned.Visible = False : divFrequenctDetails.Visible = False
            If ddlFrequency.SelectedIndex > 0 Then
                If ddlFrequency.SelectedValue > 1 Then
                    divFrequenctDetails.Visible = True
                End If
                'ddlFrequency_SelectedIndexChanged(sender, e)
                BindComplianceAsgTaskDetails()
            End If
            imgbtnAssign.Visible = False
            If rboUnAssigned.Checked = True Then
                imgbtnAssign.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboUnAssigned_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub rboAssigned_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rboAssigned.CheckedChanged
        Try
            lblError.Text = ""
            chkAssignbtn = 0
            divAssigned.Visible = True : divFrequenctDetails.Visible = False
            If ddlFrequency.SelectedValue > 1 Then
                divFrequenctDetails.Visible = True
            End If
            'ddlFrequency_SelectedIndexChanged(sender, e)
            BindComplianceAsgTaskDetails()
            imgbtnAssign.Visible = False
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "rboAssigned_CheckedChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvUnAssigned_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvUnAssigned.RowDataBound
        Dim imgbtnEdit As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnEdit = CType(e.Row.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
            End If
        Catch ex As Exception
            lblError.Text = ex.Message
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvUnAssigned_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvUnAssigned_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvUnAssigned.RowCommand
        Dim lblTaskID As New Label, lblCustomerID As New Label
        Try
            lblError.Text = "" : lblATError.Text = ""
            ddlTaskPartner.SelectedIndex = 0 : ddlTaskEmployee.SelectedIndex = 0 : ddlTaskWorkStatus.SelectedIndex = 0
            txtDueDate.Text = "" : txtExpectedCompletionDate.Text = ""
            gvSubTask.DataSource = Nothing
            gvSubTask.DataBind()
            If e.CommandName = "AssignRow" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblTaskID = DirectCast(clickedRow.FindControl("lblTaskID"), Label)
                lblCustomerID = DirectCast(clickedRow.FindControl("lblCustomerID"), Label)
                lblSelectedCustID.Text = Val(lblCustomerID.Text)
                lblSelectedTaskID.Text = Val(lblTaskID.Text)
                chkAssignbtn = 1
                BindSubTaskGrid(Val(lblSelectedTaskID.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvGeneralMaster_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub BindSubTaskGrid(ByVal iTaskID As Integer)
        Dim dtSubTask As New DataTable
        Try
            dtSubTask = objclsAdminMaster.LoadAuditAssignmentSubTask(sSession.AccessCode, sSession.AccessCodeID, iTaskID)
            If dtSubTask.Rows.Count > 6 Then
                divST.Style.Item("Height") = "172px"
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
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAllSubTask_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnAssignTask_Click(sender As Object, e As EventArgs)
        Dim chkSelectSubTask As New CheckBox
        Dim iCheckSubTask As Integer = 0
        Dim sDate As String = ""
        Try
            lblError.Text = ""
            If ddlFrequency.SelectedValue = 1 Then
                sDate = String.Format("{0}/{1}/{2}", "01", "04", ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4))
            ElseIf ddlFrequency.SelectedValue = 2 Then
                If ddlFrequencyDetails.SelectedValue = 1 Or ddlFrequencyDetails.SelectedValue = 2 Or ddlFrequencyDetails.SelectedValue = 3 Then
                    sDate = String.Format("{0}/{1}/{2}", "01", ddlFrequencyDetails.SelectedValue.ToString().PadLeft(2, "0"c), ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4))
                Else
                    sDate = String.Format("{0}/{1}/{2}", "01", ddlFrequencyDetails.SelectedValue.ToString().PadLeft(2, "0"c), ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4))
                End If
            ElseIf ddlFrequency.SelectedValue = 4 Then
                If ddlFrequencyDetails.SelectedValue = 4 Then
                    sDate = String.Format("{0}/{1}/{2}", "01", "01", ddlComplianceFinancialYear.SelectedItem.Text.Substring(5, 4))
                ElseIf ddlFrequencyDetails.SelectedValue = 1 Then
                    sDate = String.Format("{0}/{1}/{2}", "01", "04", ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4))
                ElseIf ddlFrequencyDetails.SelectedValue = 2 Then
                    sDate = String.Format("{0}/{1}/{2}", "01", "07", ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4))
                ElseIf ddlFrequencyDetails.SelectedValue = 3 Then
                    sDate = String.Format("{0}/{1}/{2}", "01", "10", ddlComplianceFinancialYear.SelectedItem.Text.Substring(0, 4))
                End If
            Else
                sDate = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
            End If

            Dim dDate As DateTime = Date.ParseExact(sDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim dDueDate As DateTime = Date.ParseExact(txtDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim d1 As Integer
            d1 = DateDiff(DateInterval.Day, dDate, dDueDate)
            If d1 < 0 Then
                lblATError.Text = "Start Date should be greater than or equal to " & sDate & "."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)
                txtDueDate.Focus()
                Exit Try
            End If

            Dim dExpectedCompletionDate As DateTime = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim d2 As Integer
            d2 = DateDiff(DateInterval.Day, dDueDate, dExpectedCompletionDate)
            If d2 < 0 Then
                lblATError.Text = "Expected Completion Date should be greater than or equal to Start Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)
                txtExpectedCompletionDate.Focus()
                Exit Try
            End If
            If ddlFrequency.SelectedIndex = 2 And d2 > 180 Then
                lblATError.Text = "Expected Completion Date should be less than or equal to 180 days of Start Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)
                txtExpectedCompletionDate.Focus()
                Exit Try
            End If

            For i = 0 To gvSubTask.Rows.Count - 1
                chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                If chkSelectSubTask.Checked = True Then
                    iCheckSubTask = 1
                End If
            Next
            If iCheckSubTask = 0 Then
                lblATError.Text = "Select Sub Task."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)
                Exit Try
            End If
            If chkAssignbtn = 1 Then
                SaveScheduleAssignmentsDetails(Val(lblSelectedCustID.Text), Val(lblSelectedTaskID.Text))
            ElseIf chkAssignbtn = 2 Then
                Dim chkSelectTask As New CheckBox
                Dim lblCustomerID As New Label
                Dim lblTaskID As New Label
                For i = 0 To gvUnAssigned.Rows.Count - 1
                    chkSelectTask = gvUnAssigned.Rows(i).FindControl("chkSelectTask")
                    If chkSelectTask.Checked = True Then
                        lblCustomerID = gvUnAssigned.Rows(i).FindControl("lblCustomerID")
                        lblTaskID = gvUnAssigned.Rows(i).FindControl("lblTaskID")
                        SaveScheduleAssignmentsDetails(Val(lblCustomerID.Text), Val(lblTaskID.Text))
                    End If
                Next
            End If
            BindComplianceAsgTaskDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAssignTask_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub SaveScheduleAssignmentsDetails(ByVal iCustID As Integer, ByVal iTaskID As Integer)
        Dim ArrAAS() As String, ArrAAST() As String
        Dim chkSelectSubTask As New CheckBox
        Dim lblSubTaskID As New Label
        Dim dDueDate As DateTime, dExpectedCompletionDate As DateTime
        Dim sYearName As String
        Dim objAAEST As New strAuditAssignment_EmpSubTask
        Dim Arr() As String
        Try
            dDueDate = Date.ParseExact(txtDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dExpectedCompletionDate = Date.ParseExact(txtExpectedCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            Dim objAAS As New strAuditAssignment_Schedule
            objAAS.iAAS_ID = 0
            objAAS.iAAS_CustID = iCustID
            objAAS.sAAS_AssignmentNo = ""
            objAAS.iAAS_PartnerID = ddlTaskPartner.SelectedValue
            sYearName = ddlComplianceFinancialYear.SelectedItem.Text
            If ddlFrequency.SelectedValue = 1 Or ddlFrequency.SelectedValue = 4 Then
                objAAS.iAAS_YearID = ddlComplianceFinancialYear.SelectedValue
                objAAS.iAAS_MonthID = dDueDate.Month
            ElseIf ddlFrequency.SelectedValue = 2 Then
                objAAS.iAAS_YearID = ddlComplianceFinancialYear.SelectedValue
                objAAS.iAAS_MonthID = ddlFrequencyDetails.SelectedValue
                sYearName = ddlComplianceFinancialYear.SelectedItem.Text
            End If
            objAAS.iAAS_TaskID = iTaskID
            objAAS.iAAS_Status = 0
            objAAS.iAAS_AdvancePartialBilling = 0
            objAAS.iAAS_BillingType = 0
            objAAS.sAAS_AssessmentYearID = ""
            objAAS.iAAS_AttachID = 0
            objAAS.iAAS_CrBy = sSession.UserID
            objAAS.sAAS_IPAddress = sSession.IPAddress
            objAAS.iAAS_CompID = sSession.AccessCodeID
            objAAS.iAAS_IsComplianceAsg = 1
            ArrAAS = objclsAuditAssignment.SaveScheduleAssignmentsDetails(sSession.AccessCode, objAAS, sYearName)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Dashboard", "Compliance Assignment Saved", ddlComplianceFinancialYear.SelectedValue, Val(lblSelectedCustID.Text), Val(lblSelectedTaskID.Text), ArrAAS(1), sSession.IPAddress)
            For i = 0 To gvSubTask.Rows.Count - 1
                chkSelectSubTask = gvSubTask.Rows(i).FindControl("chkSelectSubTask")
                lblSubTaskID = gvSubTask.Rows(i).FindControl("lblSubTaskID")
                If chkSelectSubTask.Checked = True Then
                    Dim objAAST As New strAuditAssignment_SubTask
                    objAAST.iAAST_AAS_ID = ArrAAS(1)
                    objAAST.iAAST_SubTaskID = Val(lblSubTaskID.Text)
                    objAAST.iAAST_EmployeeID = ddlTaskEmployee.SelectedValue
                    objAAST.sAAST_AssistedByEmployeesID = GetSelectedAssistedByEmployeeIDs()
                    objAAST.sAAST_Desc = ""
                    objAAST.iAAST_FrequencyID = ddlFrequency.SelectedValue
                    If ddlFrequency.SelectedValue = 1 Then
                        objAAST.iAAST_YearOrMonthID = ddlComplianceFinancialYear.SelectedValue
                    ElseIf ddlFrequency.SelectedValue = 2 Or ddlFrequency.SelectedValue = 4 Then
                        objAAST.iAAST_YearOrMonthID = ddlFrequencyDetails.SelectedValue
                    End If
                    objAAST.dAAST_DueDate = dDueDate
                    objAAST.dAAST_ExpectedCompletionDate = dExpectedCompletionDate
                    objAAST.iAAST_WorkStatusID = ddlTaskWorkStatus.SelectedValue
                    objAAST.iAAST_CrBy = sSession.UserID
                    objAAST.sAAST_IPAddress = sSession.IPAddress
                    objAAST.iAAST_CompID = sSession.AccessCodeID
                    ArrAAST = objclsAuditAssignment.SaveAuditAssignmentEmpSubTask(sSession.AccessCode, objAAST)

                    If ddlTaskWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                        objAAEST.iAAEST_ID = 0
                        objAAEST.iAAEST_AAS_ID = ArrAAS(1)
                        objAAEST.iAAEST_AAST_ID = ArrAAST(1)
                        objAAEST.iAAEST_WorkStatusID = ddlTaskWorkStatus.SelectedValue
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
            If ddlTaskWorkStatus.SelectedItem.Text.ToLower() = "completed" Then
                objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, ArrAAS(1), 1)
                objclsAuditAssignment.UpdateScheduledStatusAndFolderPath(sSession.AccessCode, sSession.AccessCodeID, "", ArrAAS(1), 0)
                objclsAuditAssignment.SaveAuditAssignmentUserLogDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginLogPKID, sSession.UserID, ArrAAS(1))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveScheduleAssignmentsDetails" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Protected Sub chkSelectTask_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim chkAll As New CheckBox, chkSelectTask As New CheckBox
        Try
            lblError.Text = ""
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For i = 0 To gvUnAssigned.Rows.Count - 1
                    chkSelectTask = gvUnAssigned.Rows(i).FindControl("chkSelectTask")
                    chkSelectTask.Checked = True
                Next
            Else
                For i = 0 To gvUnAssigned.Rows.Count - 1
                    chkSelectTask = gvUnAssigned.Rows(i).FindControl("chkSelectTask")
                    chkSelectTask.Checked = False
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectTask_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAssign_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAssign.Click
        Dim chkSelectTask As New CheckBox
        Dim lblTaskID As New Label
        Dim iTaskID As Integer, iCheckAsg As Integer = 0
        Try
            lblError.Text = ""
            If gvUnAssigned.Rows.Count = 0 Then
                lblError.Text = "No Assignment/Task details to assign." : lblComplianceAsgTaskValidationMsg.Text = "No Assignment/Task details to assign."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalComplianceAsgTaskValidation').modal('show');", True)
                Exit Sub
            End If
            For j = 0 To gvUnAssigned.Rows.Count - 1
                chkSelectTask = gvUnAssigned.Rows(j).FindControl("chkSelectTask")
                If chkSelectTask.Checked = True Then
                    iCheckAsg = 1
                End If
            Next
            If iCheckAsg = 0 Then
                lblError.Text = "Select Assignment/Task details to assign." : lblComplianceAsgTaskValidationMsg.Text = "Select Assignment/Task details to assign."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalComplianceAsgTaskValidation').modal('show');", True)
                Exit Try
            End If
            For i = 0 To gvUnAssigned.Rows.Count - 1
                chkSelectTask = gvUnAssigned.Rows(i).FindControl("chkSelectTask")
                If chkSelectTask.Checked = True Then
                    lblTaskID = gvUnAssigned.Rows(i).FindControl("lblTaskID")
                    If iTaskID > 0 And Val(lblTaskID.Text) <> iTaskID Then
                        lblError.Text = "Select same Assignment/Task." : lblComplianceAsgTaskValidationMsg.Text = "Select same Assignment/Task."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalComplianceAsgTaskValidation').modal('show');", True)
                        Exit Sub
                    End If
                    iTaskID = Val(lblTaskID.Text)
                End If
            Next
            BindSubTaskGrid(iTaskID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAssignModal').modal('show')", True)

            chkAssignbtn = 2
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAssign_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class