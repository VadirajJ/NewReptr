Imports System
Imports System.Data
Imports System.Drawing
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class Invoice
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_Invoice"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsCompanyDetails As New clsCompanyDetails
    Private objclsEmployeeMaster As New clsEmployeeMaster

    Private sSession As AllSession
    Private Shared bLoginUserIsPartner As Boolean
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
                LoadFinalcialYear(sSession.AccessCode)
                BindBillingEntity() : BindCustomers()
                BindInvoiceTypes() : BindWorkStatus()
                BindMonth()
                RFVInvoiceType.InitialValue = "0" : RFVInvoiceType.ErrorMessage = "Select Invoice Type."
                'RFVCustomerName.InitialValue = "Select Customer" : RFVCustomerName.ErrorMessage = "Select Customer."
                RFVCompanyName.InitialValue = "Select Billing Entity" : RFVCompanyName.ErrorMessage = "Select Billing Entity."
                RFVWorkStatus.InitialValue = "Select Work Status" : RFVWorkStatus.ErrorMessage = "Select Work Status."
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If

                If Request.QueryString("FinancialYearID") IsNot Nothing Then
                    ddlFinancialYear.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FinancialYearID")))
                End If
                If Request.QueryString("CompanyID") IsNot Nothing Then
                    ddlCompanyName.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CompanyID")))
                End If
                If Request.QueryString("WorkStatusID") IsNot Nothing Then
                    ddlWorkStatus.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("WorkStatusID")))
                End If
                If Request.QueryString("CustomerID") IsNot Nothing Then
                    ddlCustomerName.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustomerID")))
                End If
                If Request.QueryString("InvoiceTypeID") IsNot Nothing Then
                    ddlInvoiceType.SelectedIndex = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("InvoiceTypeID")))
                End If
                LoadAssignmentDetailsForCust()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindMonth()
        Dim dt As New DataTable
        Dim dr As DataRow
        Dim dDate As DateTime
        Dim iCurrentMonth As Integer
        Try
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            iCurrentMonth = dDate.Month

            dt.Columns.Add("ID")
            dt.Columns.Add("Name")

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

            ddlMonth.DataSource = dt
            ddlMonth.DataTextField = "Name"
            ddlMonth.DataValueField = "ID"
            ddlMonth.DataBind()
            ddlMonth.Items.Insert(0, "Select Month")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMonth" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Private Sub BindBillingEntity()
        Try
            ddlCompanyName.DataSource = objclsCompanyDetails.LoadCompanyDetails(sSession.AccessCode, sSession.AccessCodeID)
            ddlCompanyName.DataTextField = "Company_Name"
            ddlCompanyName.DataValueField = "Company_ID"
            ddlCompanyName.DataBind()
            ddlCompanyName.Items.Insert(0, "Select Billing Entity")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingCompanyName" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Private Sub BindInvoiceTypes()
        Try
            ddlInvoiceType.Items.Add(New ListItem("Select Invoice Type", "0"))
            ddlInvoiceType.Items.Add(New ListItem("Proforma Invoice", "1"))
            ddlInvoiceType.Items.Add(New ListItem("Tax Invoice", "2"))
            ddlInvoiceType.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTypes" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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
    Protected Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            lblError.Text = ""
            ddlCompanyName.SelectedIndex = 0
            ddlWorkStatus.SelectedIndex = 0
            ddlCustomerName.SelectedIndex = 0
            ddlInvoiceType.SelectedIndex = 0
            gvAssignmentDetails.DataSource = Nothing
            gvAssignmentDetails.DataBind()
            BindMonth()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMonth.SelectedIndexChanged
        Try
            lblError.Text = ""
            'ddlInvoiceType.SelectedIndex = 0
            gvAssignmentDetails.DataSource = Nothing
            gvAssignmentDetails.DataBind()
            LoadAssignmentDetailsForCust()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlMonth_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCompanyName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompanyName.SelectedIndexChanged
        Try
            lblError.Text = ""
            'ddlInvoiceType.SelectedIndex = 0
            gvAssignmentDetails.DataSource = Nothing
            gvAssignmentDetails.DataBind()
            LoadAssignmentDetailsForCust()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompanyName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            lblError.Text = ""
            'ddlInvoiceType.SelectedIndex = 0
            gvAssignmentDetails.DataSource = Nothing
            gvAssignmentDetails.DataBind()
            LoadAssignmentDetailsForCust()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlWorkStatus_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlWorkStatus.SelectedIndexChanged
        Try
            lblError.Text = ""
            'ddlInvoiceType.SelectedIndex = 0
            gvAssignmentDetails.DataSource = Nothing
            gvAssignmentDetails.DataBind()
            LoadAssignmentDetailsForCust()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlWorkStatus_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadAssignmentDetailsForCust()
        Dim iCustID As Integer = 0
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                iCustID = ddlCustomerName.SelectedValue
            End If
            If ddlCompanyName.SelectedIndex > 0 And ddlWorkStatus.SelectedIndex > 0 Then
                BindAssignmentDetailsForCust(ddlCompanyName.SelectedValue, iCustID, ddlWorkStatus.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssignmentDetails_PreRender(sender As Object, e As EventArgs) Handles gvAssignmentDetails.PreRender
        Dim dt As New DataTable
        Try
            If gvAssignmentDetails.Rows.Count > 0 Then
                gvAssignmentDetails.UseAccessibleHeader = True
                gvAssignmentDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssignmentDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignmentDetails_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindAssignmentDetailsForCust(ByVal iCompanyId As Integer, ByVal iCustId As Integer, ByVal iWorkStatusID As Integer)
        Dim dt As New DataTable
        Dim iStatusId As Integer
        Dim iMonthID As Integer
        Try
            Dim sWSyetToStart As String = ",yet to start,"
            Dim sWSwip As String = ",wip,work in progress,"
            Dim sWScompleted As String = ",completed,close,closed,"

            If sWSyetToStart.Contains("," & ddlWorkStatus.SelectedItem.Text.ToLower.ToString() & ",") = True Then
                iStatusId = 0
            End If
            If sWSwip.Contains("," & ddlWorkStatus.SelectedItem.Text.ToLower.ToString() & ",") = True Then
                iStatusId = 2
            End If
            If sWScompleted.Contains("," & ddlWorkStatus.SelectedItem.Text.ToLower.ToString() & ",") = True Then
                iStatusId = 1
            End If
            If ddlMonth.SelectedIndex > 0 Then
                iMonthID = ddlMonth.SelectedValue
            End If

            dt = objclsAuditAssignment.LoadAssignmentDetailsForCust(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCompanyId, iCustId, iWorkStatusID, iStatusId, iMonthID)
            gvAssignmentDetails.DataSource = dt
            gvAssignmentDetails.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAssignmentDetailsForCust" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim chkAsg As New CheckBox
        Dim oFinancialYearID As New Object, oCompanyID As New Object, oWorkStatusID As New Object, oCustomerID As New Object
        Dim oInvoiceTypeID As New Object, oPKIds As New Object, oAsgIds As New Object
        Dim lblPKID As New Label, lblAssignmentID As New Label, lblAssignmentNo As New Label
        Dim sPKIds As String = "", sAsgIds As String = ""
        Dim iCompanyID As Integer = 0
        Dim iCustomerID As Integer = 0, lblCustomerID As New Label
        Dim iWorkStatusID As Integer = 0
        Dim iInvoiceTypeID As Integer = 0
        Try
            lblError.Text = ""
            If ddlCompanyName.SelectedIndex = 0 Then
                lblInvoiceValidationMsg.Text = "Select Billing Entity." : lblError.Text = "Select Billing Entity."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                ddlCompanyName.Focus()
                Exit Sub
            End If
            If ddlInvoiceType.SelectedIndex = 0 Then
                lblInvoiceValidationMsg.Text = "Select Invoice Type." : lblError.Text = "Select Invoice Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                ddlInvoiceType.Focus()
                Exit Sub
            End If
            If gvAssignmentDetails.Rows.Count = 0 Then
                lblInvoiceValidationMsg.Text = "No Assignments to create " + ddlInvoiceType.SelectedItem.Text : lblError.Text = "No Assignments to create " + ddlInvoiceType.SelectedItem.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                ddlInvoiceType.Focus()
                Exit Sub
            End If
            For i = 0 To gvAssignmentDetails.Rows.Count - 1
                chkAsg = gvAssignmentDetails.Rows(i).FindControl("chkAsg")
                If chkAsg.Checked = True Then
                    lblCustomerID = gvAssignmentDetails.Rows(i).FindControl("lblCustomerID")
                    If iCustomerID > 0 And Val(lblCustomerID.Text) <> iCustomerID Then
                        lblError.Text = "Select one Customer." : lblInvoiceValidationMsg.Text = "Select one Customer."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalInvoiceValidation').modal('show');", True)
                        Exit Sub
                    End If
                    iCustomerID = Val(lblCustomerID.Text)

                    lblAssignmentID = gvAssignmentDetails.Rows(i).FindControl("lblAssignmentID")
                    lblAssignmentNo = gvAssignmentDetails.Rows(i).FindControl("lblAssignmentNo")
                    lblPKID = gvAssignmentDetails.Rows(i).FindControl("lblPKID")
                    If Val(lblPKID.Text) > 0 And ddlInvoiceType.SelectedIndex = 1 Then
                        lblError.Text = "Already Proforma Invoice generated for selected Assignment(" & lblAssignmentNo.Text & ")." : lblInvoiceValidationMsg.Text = "Already Proforma Invoice generated for selected Assignment(" & lblAssignmentNo.Text & ")"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalInvoiceValidation').modal('show');", True)
                        Exit Sub
                    End If
                    If Val(lblPKID.Text) > 0 Then
                        sPKIds = sPKIds & "," & lblPKID.Text
                    Else
                        sAsgIds = sAsgIds & "," & lblAssignmentID.Text
                    End If
                End If
            Next
            If sPKIds.StartsWith(",") Then
                sPKIds = sPKIds.Remove(0, 1)
            End If
            If sPKIds.EndsWith(",") Then
                sPKIds = sPKIds.Remove(Len(sPKIds) - 1, 1)
            End If
            If sAsgIds.StartsWith(",") Then
                sAsgIds = sAsgIds.Remove(0, 1)
            End If
            If sAsgIds.EndsWith(",") Then
                sAsgIds = sAsgIds.Remove(Len(sAsgIds) - 1, 1)
            End If
            If sAsgIds = "" And sPKIds = "" Then
                lblError.Text = "Select Assignment." : lblInvoiceValidationMsg.Text = "Select Assignment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalInvoiceValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlCompanyName.SelectedIndex > 0 Then
                iCompanyID = ddlCompanyName.SelectedValue
            End If
            If ddlWorkStatus.SelectedIndex > 0 Then
                iWorkStatusID = ddlWorkStatus.SelectedValue
            End If
            If ddlInvoiceType.SelectedIndex > 0 Then
                iInvoiceTypeID = ddlInvoiceType.SelectedValue
            End If

            oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(ddlFinancialYear.SelectedValue))
            oCompanyID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCompanyID))
            oWorkStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iWorkStatusID))
            oCustomerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCustomerID))
            oInvoiceTypeID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iInvoiceTypeID))
            oPKIds = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sPKIds))
            oAsgIds = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(sAsgIds))
            Response.Redirect(String.Format("~/Assignment/InvoiceDetails.aspx?FinancialYearID={0}&CompanyID={1}&WorkStatusID={2}&CustomerID={3}&InvoiceTypeID={4}&PKIds={5}&AsgIds={6}", oFinancialYearID, oCompanyID, oWorkStatusID, oCustomerID, oInvoiceTypeID, oPKIds, oAsgIds), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGo_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class