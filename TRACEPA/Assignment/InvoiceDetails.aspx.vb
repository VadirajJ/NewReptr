Imports System
Imports System.Data
Imports System.Drawing
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class InvoiceDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Assignment_InvoiceDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditAssignment As New clsAuditAssignment
    Private objclsAdminMaster As New clsAdminMaster

    Private sSession As AllSession
    Private Shared dAmount As Decimal
    Private Shared bLoginUserIsPartner As Boolean
    Private Shared iFinancialYearID As Integer
    Private Shared iCompanyID As Integer
    Private Shared iWorkStatusID As Integer
    Private Shared iCustomerID As Integer
    Private Shared iInvoiceTypeID As Integer
    Private Shared sPKIds As String
    Private Shared sAsgIds As String
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                dAmount = 0
                bLoginUserIsPartner = False
                BindEmpAuthorizedSignatory() : BindTaxType1() : BindTaxType2("", "")
                'RFVTaxType1.InitialValue = "Select Tax Type" : RFVTaxType1.ErrorMessage = "Select Tax Type."
                'RFVTaxType2.InitialValue = "Select Tax Type" : RFVTaxType2.ErrorMessage = "Select Tax Type."
                RFVSignature.InitialValue = "Select Authorized Signatory" : RFVSignature.ErrorMessage = "Select Authorized Signatory."
                'RFVNotes.ControlToValidate = "txtNotes" : RFVNotes.ErrorMessage = "Enter Notes to Invoice."
                'RFVTaxType1.ValidationGroup = False : RFVTaxType1.ValidationGroup = ""
                RFVTaxType2.InitialValue = "Select Tax Type" : RFVTaxType2.ErrorMessage = "Select Tax Type."
                RFVTaxType2.ValidationGroup = False : RFVTaxType2.ValidationGroup = "" : lblHTax2.Text = "Tax Type 2"
                RFVSignature.ValidationGroup = False : RFVSignature.ValidationGroup = ""
                'RFVNotes.ValidationGroup = False : RFVNotes.ValidationGroup = ""
                If objclsAuditAssignment.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                End If
                If Request.QueryString("FinancialYearID") IsNot Nothing Then
                    iFinancialYearID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("FinancialYearID")))
                    lblFY.Text = objclsGeneralFunctions.GetFinancialYearName(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID)
                End If
                If Request.QueryString("CompanyID") IsNot Nothing Then
                    iCompanyID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CompanyID")))
                    lblCompanyName.Text = objclsGeneralFunctions.GetCompanyName(sSession.AccessCode, sSession.AccessCodeID, iCompanyID)
                End If
                If Request.QueryString("WorkStatusID") IsNot Nothing Then
                    iWorkStatusID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("WorkStatusID")))
                End If
                If Request.QueryString("CustomerID") IsNot Nothing Then
                    iCustomerID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustomerID")))
                    lblCustomerName.Text = objclsGeneralFunctions.GetCustomerName(sSession.AccessCode, sSession.AccessCodeID, iCustomerID)
                End If
                If Request.QueryString("InvoiceTypeID") IsNot Nothing Then
                    iInvoiceTypeID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("InvoiceTypeID")))
                    If iInvoiceTypeID = 1 Then
                        lblIT.Text = "Proforma Invoice"
                    ElseIf iInvoiceTypeID = 2 Then
                        lblIT.Text = "Tax Invoice"
                    End If
                End If
                If Request.QueryString("PKIds") IsNot Nothing Then
                    sPKIds = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("PKIds")))
                End If
                If Request.QueryString("AsgIds") IsNot Nothing Then
                    sAsgIds = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("AsgIds")))
                End If
                BindInvoiceDetailsForCust(iFinancialYearID, iCustomerID, iCompanyID, sPKIds, sAsgIds)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindTaxType1()
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadInvoiceTaxTypesDetails(sSession.AccessCode, sSession.AccessCodeID, "", "")
            ddlTaxType1.Items.Clear()
            If dt.Rows.Count > 0 Then
                ddlTaxType1.DataSource = dt
                ddlTaxType1.DataTextField = "Name"
                ddlTaxType1.DataValueField = "PKID"
                ddlTaxType1.DataBind()
            End If
            ddlTaxType1.Items.Insert(0, "Select Tax Type")
            ddlTaxType1.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindTaxType2(ByVal sType As String, ByVal sTax1Percentage As String)
        Dim dt As New DataTable
        Try
            dt = objclsAdminMaster.LoadInvoiceTaxTypesDetails(sSession.AccessCode, sSession.AccessCodeID, sType, sTax1Percentage)
            ddlTaxType2.Items.Clear()
            If dt.Rows.Count > 0 Then
                ddlTaxType2.DataSource = dt
                ddlTaxType2.DataTextField = "Name"
                ddlTaxType2.DataValueField = "PKID"
                ddlTaxType2.DataBind()
            End If
            ddlTaxType2.Items.Insert(0, "Select Tax Type")
            ddlTaxType2.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindWorkStatus" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindEmpAuthorizedSignatory()
        Try
            'ddlSignature.DataSource = objclsEmployeeMaster.LoadEmpAuthorizedSignatory(sSession.AccessCode, sSession.AccessCodeID)
            ddlSignature.DataSource = objclsAllActiveMaster.LoadActivePartners(sSession.AccessCode, sSession.AccessCodeID)
            ddlSignature.DataTextField = "Usr_FullName"
            ddlSignature.DataValueField = "Usr_ID"
            ddlSignature.DataBind()
            ddlSignature.Items.Insert(0, "Select Authorized Signatory")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindOnlyEmpAuthorizedSignatory" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvInvoiceDetails_PreRender(sender As Object, e As EventArgs) Handles gvInvoiceDetails.PreRender
        Dim dt As New DataTable
        Try
            If gvInvoiceDetails.Rows.Count > 0 Then
                gvInvoiceDetails.UseAccessibleHeader = True
                gvInvoiceDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvInvoiceDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvInvoiceDetails_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub gvInvoiceDetails_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim lblAmount As New Label, lblTotalAmount As New Label, lblIsTaxable As New Label, btnCalculate As New Button
        Dim chkIsTaxable As New CheckBox
        Dim txtDescription As New TextBox, txtQuantity As New TextBox, txtPricePerUnit As New TextBox 'txtHSNSAC As New TextBox
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                If e.Row.RowIndex Mod 2 = 0 Then
                    e.Row.BackColor = ColorTranslator.FromHtml("#FCFCFC")
                Else
                    e.Row.BackColor = ColorTranslator.FromHtml("#F5F5F5")
                End If
                lblAmount = e.Row.FindControl("lblAmount")
                If Val(lblAmount.Text) > 0 Then
                    dAmount = dAmount + Val(lblAmount.Text)
                End If
                txtDescription = e.Row.FindControl("txtDescription")
                'txtHSNSAC = e.Row.FindControl("txtHSNSAC")
                txtQuantity = e.Row.FindControl("txtQuantity")
                txtPricePerUnit = e.Row.FindControl("txtPricePerUnit")
                txtDescription.Enabled = False : txtQuantity.Enabled = False : txtPricePerUnit.Enabled = False 'txtHSNSAC.Enabled = False :
                If bLoginUserIsPartner = True Then
                    txtDescription.Enabled = True : txtQuantity.Enabled = True : txtPricePerUnit.Enabled = True 'txtHSNSAC.Enabled = True
                End If
                lblIsTaxable = e.Row.FindControl("lblIsTaxable")
                chkIsTaxable = e.Row.FindControl("chkIsTaxable")
                If lblIsTaxable.Text = "1" Then
                    chkIsTaxable.Checked = True
                End If
            End If
            If e.Row.RowType = DataControlRowType.Footer Then
                lblTotalAmount = e.Row.FindControl("lblTotalAmount")
                If (dAmount > 0) Then
                    lblTotalAmount.Text = String.Format("{0:0.00}", dAmount)
                End If
                e.Row.BackColor = ColorTranslator.FromHtml("#E6F0FB")
                btnCalculate = e.Row.FindControl("btnCalculate")
                btnCalculate.Enabled = False
                If bLoginUserIsPartner = True Then
                    btnCalculate.Enabled = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvInvoiceDetails_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindInvoiceDetailsForCust(ByVal iFYID As Integer, ByVal iCustId As Integer, ByVal iCompanyID As Integer, ByVal sPKIds As String, ByVal sAsgIds As String)
        Dim dt As New DataTable, dtCompany As New DataTable
        Try
            divInvoiceDetails.Visible = True
            divTaxes.Visible = False
            lblPKID.Text = 0
            btnSavePreviewReport.Visible = False
            ReportViewer1.Visible = False
            'btnGenerateReport.Visible = False
            dt = objclsAuditAssignment.LoadInvoiceDetailsForCust(sSession.AccessCode, sSession.AccessCodeID, iFYID, iCustId, iCompanyID, sPKIds, sAsgIds)
            gvInvoiceDetails.DataSource = dt
            gvInvoiceDetails.DataBind()

            dtCompany = objclsAuditAssignment.LoadCompanyDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iCompanyID)
            lblPaymentTerms.Text = dtCompany.Rows(0)("Company_Paymentterms").ToString()
            lblConditions.Text = dtCompany.Rows(0)("Company_Conditions").ToString()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindInvoiceDetailsForCust" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnCalculate_Click(sender As Object, e As EventArgs)
        Dim lblSrNo As New Label, lblAssignmentID As New Label, lblAmount As New Label, lblTotalAmount As New Label
        Dim chkIsTaxable As New CheckBox
        Dim txtQuantity As New TextBox, txtPricePerUnit As New TextBox, txtDescription As New TextBox 'txtHSNSAC As New TextBox,
        Dim iCheck As Integer = 0, dTotal As Decimal = 0, dWithTaxTotal As Decimal = 0, dWithOutTaxTotal As Decimal = 0
        Try
            lblError.Text = ""
            ddlTaxType1.SelectedIndex = 0
            ddlTaxType2.SelectedIndex = 0
            For i = 0 To gvInvoiceDetails.Rows.Count - 1
                iCheck = 0
                lblSrNo = gvInvoiceDetails.Rows(i).FindControl("lblSrNo")
                chkIsTaxable = gvInvoiceDetails.Rows(i).FindControl("chkIsTaxable")
                txtDescription = gvInvoiceDetails.Rows(i).FindControl("txtDescription")
                'txtHSNSAC = gvInvoiceDetails.Rows(i).FindControl("txtHSNSAC")
                txtQuantity = gvInvoiceDetails.Rows(i).FindControl("txtQuantity")
                txtPricePerUnit = gvInvoiceDetails.Rows(i).FindControl("txtPricePerUnit")
                lblAmount = gvInvoiceDetails.Rows(i).FindControl("lblAmount")
                lblAssignmentID = gvInvoiceDetails.Rows(i).FindControl("lblAssignmentID")
                If txtQuantity.Text <> "" Or txtPricePerUnit.Text <> "" Then 'txtDescription.Text <> "" Or txtHSNSAC.Text <> ""
                    'If txtHSNSAC.Text = "" Then
                    '    lblInvoiceValidationMsg.Text = "Please enter HSN/SAC - Line No. " & lblSrNo.Text : lblError.Text = "Please enter HSN/SAC - Line No. " & lblSrNo.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                    '    txtHSNSAC.Focus()
                    '    Exit Sub
                    'End If
                    If txtQuantity.Text = "" Then
                        lblInvoiceValidationMsg.Text = "Please enter Quantity - Line No. " & lblSrNo.Text : lblError.Text = "Please enter Quantity - Line No. " & lblSrNo.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                        txtQuantity.Focus()
                        Exit Sub
                    End If
                    If txtPricePerUnit.Text = "" Then
                        lblInvoiceValidationMsg.Text = "Please enter Price Per Unit - Line No. " & lblSrNo.Text : lblError.Text = "Please enter Price Per Unit - Line No. " & lblSrNo.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                        txtPricePerUnit.Focus()
                        Exit Sub
                    End If
                    If txtDescription.Text = "" Then
                        lblInvoiceValidationMsg.Text = "Please enter Description - Line No. " & lblSrNo.Text : lblError.Text = "Please enter Description - Line No. " & lblSrNo.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                        txtDescription.Focus()
                        Exit Sub
                    End If
                    lblAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(Val(txtQuantity.Text) * Val(txtPricePerUnit.Text)))
                    dTotal = dTotal + Val(lblAmount.Text)
                    If chkIsTaxable.Checked = True Then
                        dWithTaxTotal = dWithTaxTotal + Val(lblAmount.Text)
                    Else
                        dWithOutTaxTotal = dWithOutTaxTotal + Val(lblAmount.Text)
                    End If
                End If
            Next
            lblTotalAmount = gvInvoiceDetails.FooterRow.FindControl("lblTotalAmount")
            lblTotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dTotal))
            'If iInvoiceTypeID = 2 Then
            divTaxes.Visible = True
            'RFVTaxType1.ValidationGroup = True : RFVTaxType1.ValidationGroup = "Validate"
            'RFVTaxType2.ValidationGroup = True : RFVTaxType2.ValidationGroup = "Validate"
            RFVSignature.ValidationGroup = True : RFVSignature.ValidationGroup = "Validate"
            'RFVNotes.ValidationGroup = True : RFVNotes.ValidationGroup = "Validate"
            lblTotalBeforeTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(dTotal))
            lblTotalBeforeWithTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(dWithTaxTotal))
            lblTotalBeforeWithOutTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(dWithOutTaxTotal))
            'End If

            lblTax1Name.Text = "0" : lblTax2Name.Text = "0"
            lblTotalAfterTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(Val(lblTotalBeforeTaxValue.Text)))
            lblTotalAfterWithTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(Val(lblTotalBeforeWithTaxValue.Text)))
            lblTotalAfterWithOutTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(Val(lblTotalBeforeWithOutTaxValue.Text)))
            lblAdvancePaid.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
            lblBalance.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblTotalAfterTaxValue.Text))

            If bLoginUserIsPartner = True And Val(lblTotalAmount.Text) > 0 Then
                btnSavePreviewReport.Visible = True
                ReportViewer1.Visible = False
                'btnGenerateReport.Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCalculate_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlTaxType1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTaxType1.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dTax1Percentage As Decimal = 0
        Try
            lblError.Text = ""
            lblTax1Name.Text = "0" : lblTax1Percentage.Text = "0"
            lblTax2Name.Text = "0" : lblTax2Percentage.Text = "0"
            lblTotalAfterTaxValue.Text = "" : lblAdvancePaid.Text = "" : lblBalance.Text = ""
            ddlTaxType2.Enabled = False : ddlTaxType2.SelectedIndex = 0
            RFVTaxType2.ValidationGroup = False : RFVTaxType2.ValidationGroup = "" : lblHTax2.Text = "Tax Type 2"
            If ddlTaxType1.SelectedIndex > 0 Then
                Dim s As String = ddlTaxType1.SelectedItem.Text.ToString()
                If ddlTaxType1.SelectedItem.Text.ToString().Contains("IGST") = False Then
                    RFVTaxType2.ValidationGroup = True : RFVTaxType2.ValidationGroup = "Validate" : lblHTax2.Text = "* Tax Type 2"
                End If
                dt = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, ddlTaxType1.SelectedValue, "TM")
                lblTax1Percentage.Text = dt.Rows(0)("CMM_Rate")
                If Val(lblTax1Percentage.Text) > 0 Then
                    dTax1Percentage = (Val(lblTax1Percentage.Text) * Val(lblTotalBeforeWithTaxValue.Text) / 100)
                End If
                lblTax1Name.Text = dt.Rows(0)("CMM_Desc") & " @ " & dt.Rows(0)("CMM_Rate") & "% = " & String.Format("{0:0.00}", Convert.ToDecimal(dTax1Percentage))
                ddlTaxType2.Enabled = True
                BindTaxType2(ddlTaxType1.SelectedItem.Text, lblTax1Percentage.Text.ToString())
            End If

            lblTotalAfterTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(dTax1Percentage + Val(lblTotalBeforeWithTaxValue.Text) + Val(lblTotalBeforeWithOutTaxValue.Text)))
            lblAdvancePaid.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
            lblBalance.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblTotalAfterTaxValue.Text))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTaxType1_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub ddlTaxType2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTaxType2.SelectedIndexChanged
        Dim dt As New DataTable
        Dim dTax1Percentage As Decimal = 0
        Dim dTax2Percentage As Decimal = 0
        Try
            lblError.Text = ""
            lblTax2Name.Text = "0" : lblTax2Percentage.Text = "0"
            lblTotalAfterTaxValue.Text = "" : lblAdvancePaid.Text = "" : lblBalance.Text = ""
            If ddlTaxType1.SelectedIndex > 0 Then
                dt = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, ddlTaxType1.SelectedValue, "TM")
                lblTax1Percentage.Text = dt.Rows(0)("CMM_Rate")
                If Val(lblTax1Percentage.Text) > 0 Then
                    dTax1Percentage = (Val(lblTax1Percentage.Text) * Val(lblTotalBeforeWithTaxValue.Text) / 100)
                End If
            End If
            If ddlTaxType2.SelectedIndex > 0 Then
                dt = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, ddlTaxType2.SelectedValue, "TM")
                lblTax2Percentage.Text = dt.Rows(0)("CMM_Rate")
                If Val(lblTax2Percentage.Text) > 0 Then
                    dTax2Percentage = (Val(lblTax2Percentage.Text) * Val(lblTotalBeforeWithTaxValue.Text) / 100)
                End If
                lblTax2Name.Text = dt.Rows(0)("CMM_Desc") & " @ " & dt.Rows(0)("CMM_Rate") & "% = " & String.Format("{0:0.00}", Convert.ToDecimal(dTax2Percentage))
            End If
            lblTotalAfterTaxValue.Text = String.Format("{0:0.00}", Convert.ToDecimal(dTax1Percentage + dTax2Percentage + Val(lblTotalBeforeWithTaxValue.Text) + Val(lblTotalBeforeWithOutTaxValue.Text)))
            lblAdvancePaid.Text = String.Format("{0:0.00}", Convert.ToDecimal(0))
            lblBalance.Text = String.Format("{0:0.00}", Convert.ToDecimal(lblTotalAfterTaxValue.Text))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlTaxType1_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSavePreviewReport_Click(sender As Object, e As EventArgs) Handles btnSavePreviewReport.Click
        Dim Arr() As String
        Dim lblSrNo As New Label, lblAssignmentID As New Label, lblAmount As New Label, lblTotalAmount As New Label
        Dim txtQuantity As New TextBox, txtPricePerUnit As New TextBox, txtDescription As New TextBox 'txtHSNSAC As New TextBox,
        Dim iCheck As Integer = 0, dTotal As Decimal = 0
        Try
            lblError.Text = ""
            If gvInvoiceDetails.Rows.Count = 0 Then
                lblInvoiceValidationMsg.Text = "No billing details to save." : lblError.Text = "No billing details to save."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                Exit Sub
            End If
            For i = 0 To gvInvoiceDetails.Rows.Count - 1
                iCheck = 0
                lblSrNo = gvInvoiceDetails.Rows(i).FindControl("lblSrNo")
                txtDescription = gvInvoiceDetails.Rows(i).FindControl("txtDescription")
                'txtHSNSAC = gvInvoiceDetails.Rows(i).FindControl("txtHSNSAC")
                txtQuantity = gvInvoiceDetails.Rows(i).FindControl("txtQuantity")
                txtPricePerUnit = gvInvoiceDetails.Rows(i).FindControl("txtPricePerUnit")
                lblAmount = gvInvoiceDetails.Rows(i).FindControl("lblAmount")
                lblAssignmentID = gvInvoiceDetails.Rows(i).FindControl("lblAssignmentID")
                If txtQuantity.Text <> "" Or txtPricePerUnit.Text <> "" Then 'txtDescription.Text <> "" Or txtHSNSAC.Text <> ""
                    'If txtHSNSAC.Text = "" Then
                    '    lblInvoiceValidationMsg.Text = "Please enter HSN/SAC - Line No. " & lblSrNo.Text : lblError.Text = "Please enter HSN/SAC - Line No. " & lblSrNo.Text
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                    '    txtHSNSAC.Focus()
                    '    Exit Sub
                    'End If
                    If txtQuantity.Text = "" Then
                        lblInvoiceValidationMsg.Text = "Please enter Quantity - Line No. " & lblSrNo.Text : lblError.Text = "Please enter Quantity - Line No. " & lblSrNo.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                        txtQuantity.Focus()
                        Exit Sub
                    End If
                    If txtPricePerUnit.Text = "" Then
                        lblInvoiceValidationMsg.Text = "Please enter Price Per Unit - Line No. " & lblSrNo.Text : lblError.Text = "Please enter Price Per Unit - Line No. " & lblSrNo.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                        txtPricePerUnit.Focus()
                        Exit Sub
                    End If
                    If txtDescription.Text = "" Then
                        lblInvoiceValidationMsg.Text = "Please enter Description - Line No. " & lblSrNo.Text : lblError.Text = "Please enter Description - Line No. " & lblSrNo.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                        txtDescription.Focus()
                        Exit Sub
                    End If
                    lblAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(Val(txtQuantity.Text) * Val(txtPricePerUnit.Text)))
                    dTotal = dTotal + Val(lblAmount.Text)
                End If
            Next
            lblTotalAmount = gvInvoiceDetails.FooterRow.FindControl("lblTotalAmount")
            lblTotalAmount.Text = String.Format("{0:0.00}", Convert.ToDecimal(dTotal))

            If Val(lblPKID.Text) = 0 Then
                Arr = SaveInvoice()
                'divInvoiceDetails.Visible = False
                'divTaxes.Visible = False
                'gvInvoiceDetails.DataSource = Nothing
                'gvInvoiceDetails.DataBind()
                'btnGenerateReport.Visible = True
                lblPKID.Text = Arr(1)
                btnSavePreviewReport.Visible = False
                lblError.Text = "Successfully Saved Invoice Report." : lblInvoiceValidationMsg.Text = "Successfully Saved Invoice Report."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalInvoiceValidation').modal('show');", True)
                GenerateInvoice(Arr(1), "NO")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSavePreviewReport_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Protected Sub btnGenerateReport_Click(sender As Object, e As EventArgs) Handles btnGenerateReport.Click
    '    Try
    '        lblError.Text = ""
    '        If Val(lblPKID.Text) > 0 Then
    '            GenerateInvoice(Val(lblPKID.Text), "YES")
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGenerateReport_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Private Function SaveInvoice() As Array
        Dim objAAI As New strAuditAssignment_Invoice
        Dim objAAID As New strAuditAssignment_InvoiceDetails
        Dim dtCompany As New DataTable
        Dim Arr() As String
        Dim sNo As String
        Dim iTaxType1 As Integer = 0, iTaxType2 As Integer = 0
        Dim dTaxType1Percentage As Decimal = 0.00, dTaxType2Percentage As Decimal = 0.00
        Dim lblAssignmentID As New Label, lblCustomerID As New Label, lblAmount As New Label ', lblHSNSAC As New Label
        Dim chkIsTaxable As New CheckBox
        Dim txtQuantity As New TextBox, txtPricePerUnit As New TextBox, txtDescription As New TextBox 'txtHSNSAC As New TextBox,
        Try
            If ddlTaxType1.SelectedIndex > 0 Then
                iTaxType1 = ddlTaxType1.SelectedValue
                dTaxType1Percentage = lblTax1Percentage.Text
            End If
            If ddlTaxType2.SelectedIndex > 0 Then
                iTaxType2 = ddlTaxType2.SelectedValue
                dTaxType2Percentage = lblTax2Percentage.Text
            End If

            sNo = objclsAuditAssignment.GenerateNewInvoiceNo(sSession.AccessCode, sSession.AccessCodeID, iFinancialYearID, iCompanyID, iInvoiceTypeID)
            dtCompany = objclsAuditAssignment.LoadCompanyDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iCompanyID)

            objAAI.iAAI_ID = 0
            objAAI.iAAI_YearID = iFinancialYearID
            objAAI.iAAI_Cust_ID = iCustomerID
            objAAI.iAAI_BillingEntity_ID = iCompanyID
            If iInvoiceTypeID = 1 Then
                objAAI.sAAI_InvoiceNo = dtCompany.Rows(0)("Company_Code") & "/" & lblFY.Text & "/PI/" & sNo
            ElseIf iInvoiceTypeID = 2 Then
                objAAI.sAAI_InvoiceNo = dtCompany.Rows(0)("Company_Code") & "/" & lblFY.Text & "/TI/" & sNo
            End If
            objAAI.iAAI_InvoiceTypeID = iInvoiceTypeID
            objAAI.iAAI_TaxType1 = iTaxType1
            objAAI.dAAI_TaxType1Percentage = dTaxType1Percentage
            objAAI.iAAI_TaxType2 = iTaxType2
            objAAI.dAAI_TaxType2Percentage = dTaxType2Percentage
            objAAI.sAAI_Notes = objclsGRACeGeneral.SafeSQL(txtNotes.Text)
            If ddlSignature.SelectedIndex = 0 Then
                objAAI.iAAI_AuthorizedSignatory = 0
            Else
                objAAI.iAAI_AuthorizedSignatory = ddlSignature.SelectedValue
            End If
            objAAI.iAAI_CrBy = sSession.UserID
            objAAI.sAAI_IPAddress = sSession.IPAddress
            objAAI.iAAI_CompID = sSession.AccessCodeID
            Arr = objclsAuditAssignment.SaveInvoice(sSession.AccessCode, objAAI)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Invoice", "Saved", iFinancialYearID, iCustomerID, Arr(1), objAAI.sAAI_InvoiceNo, sSession.IPAddress)

            For i = 0 To gvInvoiceDetails.Rows.Count - 1
                chkIsTaxable = gvInvoiceDetails.Rows(i).FindControl("chkIsTaxable")
                txtDescription = gvInvoiceDetails.Rows(i).FindControl("txtDescription")
                'lblHSNSAC = gvInvoiceDetails.Rows(i).FindControl("lblHSNSAC")
                txtQuantity = gvInvoiceDetails.Rows(i).FindControl("txtQuantity")
                txtPricePerUnit = gvInvoiceDetails.Rows(i).FindControl("txtPricePerUnit")
                lblAssignmentID = gvInvoiceDetails.Rows(i).FindControl("lblAssignmentID")
                lblCustomerID = gvInvoiceDetails.Rows(i).FindControl("lblCustomerID")
                lblAmount = gvInvoiceDetails.Rows(i).FindControl("lblAmount")
                If Val(lblCustomerID.Text) > 0 And Val(lblAmount.Text) > 0 Then 'Val(lblAssignmentID.Text) > 0 And
                    If iInvoiceTypeID = 1 And Val(lblAssignmentID.Text) > 0 Then
                        objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, Val(lblAssignmentID.Text), 2)
                    ElseIf iInvoiceTypeID = 2 And Val(lblAssignmentID.Text) > 0 Then
                        objclsAuditAssignment.UpdateScheduledAsgBillingTypeDetails(sSession.AccessCode, sSession.AccessCodeID, Val(lblAssignmentID.Text), 3)
                    End If
                    objAAID.iAAID_ID = 0
                    objAAID.iAAID_AAI_ID = Arr(1)
                    objAAID.iAAID_AAS_ID = Val(lblAssignmentID.Text)
                    If chkIsTaxable.Checked = True Then
                        objAAID.iAAID_IsTaxable = 1
                    Else
                        objAAID.iAAID_IsTaxable = 0
                    End If
                    objAAID.sAAID_Desc = objclsGRACeGeneral.SafeSQL(txtDescription.Text.Trim)
                    objAAID.iAAID_HSNSAC = 0 'objclsGRACeGeneral.SafeSQL(lblHSNSAC.Text)
                    objAAID.iAAID_Quantity = Val(txtQuantity.Text)
                    objAAID.dAAID_PricePerUnit = String.Format("{0:0.00}", Convert.ToDecimal(txtPricePerUnit.Text))
                    objAAID.iAAID_CrBy = sSession.UserID
                    objAAID.sAAID_IPAddress = sSession.IPAddress
                    objAAID.iAAID_CompID = sSession.AccessCodeID
                    objclsAuditAssignment.SaveInvoiceDetails(sSession.AccessCode, objAAID)
                End If
            Next
            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveInvoice" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Protected Sub GenerateInvoice(ByVal iPKID As Integer, ByVal sIsDownload As String)
        Dim mimeType As String = Nothing
        Dim dtInvoice As New DataTable, dtCompany As New DataTable, dtCustomer As New DataTable, dtSubTasks As New DataTable, dtTaxType1 As New DataTable, dtTaxType2 As New DataTable
        Dim dTotal As Decimal, dSGST As Decimal, dCGST As Decimal, dFinalTotal As Decimal, sReportNo As String, sReportName As String
        Try
            dtInvoice = objclsAuditAssignment.GetInvoiceDetailsForReport(sSession.AccessCode, sSession.AccessCodeID, iPKID)
            dTotal = objclsAuditAssignment.GetInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID)
            Dim sLogoName As String = "" : Dim imageBase64DataLogoString As String = ""
            Dim objclsCompanyDetails As New clsCompanyDetails
            sLogoName = objclsCompanyDetails.getCompanyImageName(sSession.AccessCode, dtInvoice.Rows(0)("AAI_BillingEntity_ID"), "A")
            If sLogoName <> "" And sLogoName <> "." Then
                Dim imageDataURL As String = Server.MapPath("~/Images/" + sLogoName)
                If System.IO.File.Exists(imageDataURL) = True Then
                    Dim logoInBytes As Byte() = System.IO.File.ReadAllBytes(imageDataURL)
                    imageBase64DataLogoString = Convert.ToBase64String(logoInBytes)
                End If
            End If
            Dim imageBase64DataSignatureString As String = ""
            Dim iSignatureID As Integer = objclsAuditAssignment.GetUserSignatureID(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0).Item("AAI_AuthorizedSignatory"))
            If iSignatureID > 0 Then
                Dim objclsEProfile As New clsEProfile
                Dim objclsAttachments As New clsAttachments
                Dim iSignatureDocID As Integer = objclsEProfile.GetPhotoDocID(sSession.AccessCode, sSession.AccessCodeID, iSignatureID)
                Dim sPaths As String = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                Dim sDestFilePath As String = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iSignatureID, iSignatureDocID)
                If System.IO.Directory.Exists(sPaths) = True And System.IO.File.Exists(sDestFilePath) = True Then
                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sDestFilePath)
                    imageBase64DataSignatureString = Convert.ToBase64String(bytes)
                End If
            End If
            dtCompany = objclsAuditAssignment.LoadCompanyLogoSignatureDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_BillingEntity_ID"), imageBase64DataLogoString, imageBase64DataSignatureString)
            dtCustomer = objclsAuditAssignment.LoadCustomerDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iCustomerID)
            dtSubTasks = objclsAuditAssignment.LoadSubTaskDetailsForInvoice(sSession.AccessCode, sSession.AccessCodeID, iPKID)
            If dtCompany.Rows.Count = 0 Then
                lblInvoiceValidationMsg.Text = "Please update Company details in Master module." : lblError.Text = "Please update Company details in Master module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                Exit Sub
            End If
            If dtCustomer.Rows.Count = 0 Then
                lblInvoiceValidationMsg.Text = "Please update Customer details in Master module." : lblError.Text = "Please update Customer details in Master module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalInvoiceValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Visible = True
            ReportViewer1.Reset()
            Dim rdCompany As New ReportDataSource("DataSet1", dtCompany)
            Dim rdCustomer As New ReportDataSource("DataSet2", dtCustomer)
            Dim rdSubTasks As New ReportDataSource("DataSet3", dtSubTasks)
            ReportViewer1.LocalReport.DataSources.Add(rdCompany)
            ReportViewer1.LocalReport.DataSources.Add(rdCustomer)
            ReportViewer1.LocalReport.DataSources.Add(rdSubTasks)

            sReportNo = dtInvoice.Rows(0)("AAI_InvoiceNo")
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Assignment/TaxInvoice.rdlc")

            Dim ReportTitle As ReportParameter()
            Dim InvoiceNo As ReportParameter()

            If dtInvoice.Rows(0)("AAI_InvoiceTypeID") = 1 Then
                ReportTitle = New ReportParameter() {New ReportParameter("ReportTitle", "Proforma Invoice")}
                InvoiceNo = New ReportParameter() {New ReportParameter("InvoiceNo", "PI No: " & sReportNo)}
                sReportName = "ProformaInvoice"
            Else
                ReportTitle = New ReportParameter() {New ReportParameter("ReportTitle", "Tax Invoice")}
                InvoiceNo = New ReportParameter() {New ReportParameter("InvoiceNo", "Invoice No: " & sReportNo)}
                sReportName = "TaxInvoice"
            End If

            Dim TaxType1 As ReportParameter()
            Dim TaxType1Details As ReportParameter()
            If dtInvoice.Rows(0)("AAI_TaxType1") > 0 Then
                dSGST = objclsAuditAssignment.GetTaxInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID, dtInvoice.Rows(0)("AAI_TaxType1Percentage"))
                TaxType1 = New ReportParameter() {New ReportParameter("TaxType1", "₹" & String.Format("{0:0.00}", dSGST))}
                dtTaxType1 = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_TaxType1"), "TM")
                TaxType1Details = New ReportParameter() {New ReportParameter("TaxType1Details", (dtTaxType1.Rows(0)("CMM_Desc") & "@" & dtTaxType1.Rows(0)("CMM_Rate") & "%").ToString())}
            Else
                TaxType1 = New ReportParameter() {New ReportParameter("TaxType1", " ")}
                TaxType1Details = New ReportParameter() {New ReportParameter("TaxType1Details", " ")}
            End If

            Dim TaxType2 As ReportParameter()
            Dim TaxType2Details As ReportParameter()
            If dtInvoice.Rows(0)("AAI_TaxType2") > 0 Then
                dCGST = objclsAuditAssignment.GetTaxInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID, dtInvoice.Rows(0)("AAI_TaxType2Percentage"))
                TaxType2 = New ReportParameter() {New ReportParameter("TaxType2", "₹" & String.Format("{0:0.00}", dCGST))}

                dtTaxType2 = objclsAdminMaster.GetAdminMasterOtherDetails(sSession.AccessCode, sSession.AccessCodeID, dtInvoice.Rows(0)("AAI_TaxType2"), "TM")
                TaxType2Details = New ReportParameter() {New ReportParameter("TaxType2Details", (dtTaxType2.Rows(0)("CMM_Desc") & "@" & dtTaxType2.Rows(0)("CMM_Rate") & "%").ToString())}
            Else
                TaxType2 = New ReportParameter() {New ReportParameter("TaxType2", " ")}
                TaxType2Details = New ReportParameter() {New ReportParameter("TaxType2Details", " ")}
            End If

            dFinalTotal = objclsAuditAssignment.GetFinalInvoiceTotal(sSession.AccessCode, sSession.AccessCodeID, iPKID, dtInvoice.Rows(0)("AAI_TaxType1Percentage"), dtInvoice.Rows(0)("AAI_TaxType2Percentage"))
            Dim FinalTotal As ReportParameter() = New ReportParameter() {New ReportParameter("FinalTotal", "₹" & String.Format("{0:0.00}", dFinalTotal))}
            Dim AmountInWords As ReportParameter() = New ReportParameter() {New ReportParameter("AmountInWords", objclsAuditAssignment.NumberToWord(String.Format("{0:0.00}", dFinalTotal)) & " Only")}
            Dim Notes As ReportParameter()
            If dtInvoice.Rows(0)("AAI_Notes").ToString() = "" Then
                Notes = New ReportParameter() {New ReportParameter("Notes", " ")}
            Else
                Notes = New ReportParameter() {New ReportParameter("Notes", dtInvoice.Rows(0)("AAI_Notes").ToString())}
            End If

            ReportViewer1.LocalReport.SetParameters(ReportTitle)
            ReportViewer1.LocalReport.SetParameters(InvoiceNo)
            ReportViewer1.LocalReport.SetParameters(TaxType1)
            ReportViewer1.LocalReport.SetParameters(TaxType2)
            ReportViewer1.LocalReport.SetParameters(FinalTotal)
            ReportViewer1.LocalReport.SetParameters(AmountInWords)
            ReportViewer1.LocalReport.SetParameters(TaxType1Details)
            ReportViewer1.LocalReport.SetParameters(TaxType2Details)
            ReportViewer1.LocalReport.SetParameters(Notes)

            Dim CurrentDate As ReportParameter() = New ReportParameter() {New ReportParameter("CurrentDate", "Date:" & objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode))}
            Dim SubTotal As ReportParameter() = New ReportParameter() {New ReportParameter("SubTotal", "₹" & String.Format("{0:0.00}", dTotal))}
            Dim Total As ReportParameter() = New ReportParameter() {New ReportParameter("Total", "₹" & String.Format("{0:0.00}", dTotal))}
            ReportViewer1.LocalReport.SetParameters(CurrentDate)
            ReportViewer1.LocalReport.SetParameters(SubTotal)
            ReportViewer1.LocalReport.SetParameters(Total)
            ReportViewer1.LocalReport.Refresh()

            If sIsDownload = "YES" Then
                Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Assignments", "Invoice", "PDF", sSession.YearID, "", iPKID, "", sSession.IPAddress)
                HttpContext.Current.Response.Buffer = True
                HttpContext.Current.Response.Clear()
                HttpContext.Current.Response.ContentType = mimeType
                Dim sFileName As String = Regex.Replace(sReportName, " \ s", "")
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" & sFileName & "_" & iPKID & ".pdf")
                HttpContext.Current.Response.BinaryWrite(RptViewer)
                HttpContext.Current.Response.Flush()
                'HttpContext.Current.Response.End()
                HttpContext.Current.Response.SuppressContent = True
                HttpContext.Current.ApplicationInstance.CompleteRequest()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GenerateInvoice" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oFinancialYearID As New Object, oCompanyID As New Object, oWorkStatusID As New Object, oCustomerID As New Object, oInvoiceTypeID As New Object, oPKIds As New Object, oAsgIds As New Object
        Try
            lblError.Text = ""
            oFinancialYearID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iFinancialYearID))
            oCompanyID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCompanyID))
            oWorkStatusID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iWorkStatusID))
            oCustomerID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iCustomerID))
            oInvoiceTypeID = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(iInvoiceTypeID))
            Response.Redirect(String.Format("~/Assignment/Invoice.aspx?FinancialYearID={0}&CompanyID={1}&WorkStatusID={2}&CustomerID={3}&InvoiceTypeID={4}", oFinancialYearID, oCompanyID, oWorkStatusID, oCustomerID, oInvoiceTypeID), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class