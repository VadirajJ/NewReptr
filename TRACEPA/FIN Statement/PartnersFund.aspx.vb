Imports System
Imports System.Data
Imports System.Collections
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class PartnersFund
    Inherits System.Web.UI.Page
    Private sFormName As String = "PartnersFund"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsPartnerFund As New clsPartnerFund
    Private objCust As New clsCustDetails
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private Shared iCustPartnerPKId As Integer
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dtSampleFormat As New DataTable
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                divCustPartner.Visible = False : lblPartnershipFirmId.Text = 0 : lblShareOfProfitPercentage.Text = ""
                LoadExistingCustomerName() : LoadExistingFinancialYear()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYearSchedTemp.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    ddlFinancialYearSchedTemp.SelectedValue = ddlFinancialYearSchedTemp.SelectedValue
                    sSession.ScheduleYearId = ddlFinancialYearSchedTemp.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYearSchedTemp.SelectedValue = sSession.ScheduleYearId
                End If
                If sSession.CustomerID <> 0 Then
                    Dim AppCustomerID As HttpCookie = New HttpCookie("AppCustomerID")
                    AppCustomerID = Request.Cookies("AppCustomerID")
                    If sSession.CustomerID <> AppCustomerID.Value Then
                        sSession.CustomerID = AppCustomerID.Value
                        ddlCustNameSchedTemp.SelectedValue = sSession.CustomerID
                        ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
                    Else
                        ddlCustNameSchedTemp.SelectedValue = sSession.CustomerID
                        If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                            ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
                BindCustPartner()

                RFVPartnerName.ErrorMessage = "Enter Partner Name."
                REVPartnerName.ValidationExpression = "^[\s\S]{0,100}$" : REVPartnerName.ErrorMessage = "Partner Name exceeded maximum size(max 100 characters)."
                RFVPartnerDOJ.ErrorMessage = "Enter Date of Joining." : REVPartnerDOJ.ErrorMessage = "Enter Valid Date of Joining." : REVPartnerDOJ.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVPartnerPAN.ErrorMessage = "Enter Partner PAN."
                REVPartnerPAN.ValidationExpression = "^[\s\S]{0,25}$" : REVPartnerPAN.ErrorMessage = "Partner PAN exceeded maximum size(max 25 characters)."
                RFVPartnerShareOfProfit.ErrorMessage = "Enter Share Of Profit."
                'REVShareOfProfit.ValidationExpression = "^0*(100(\.0{1,2})?|[1-9][0-9]?(\.[0-9]{1,2})?|0\.(0[1-9]|[1-9][0-9]?))$" : REVShareOfProfit.ErrorMessage = "Share Of Profit shouild be less than 100."
                REVPartnerShareOfProfit.ValidationExpression = "^(100(\.00?)?|\d{1,2}(\.\d{1,2})?|0\.\d{1,3})$" : REVShareOfprofit.ErrorMessage = "Share Of Profit shouild be 0 to 100."
                RFVPartnerCapitalAmount.ErrorMessage = "Enter Capital Amount."
                REVPartnerCapitalAmount.ValidationExpression = "^[1-9]\d*(\.\d{1,2})?$" : REVPartnerCapitalAmount.ErrorMessage = "Enter valid Capital Amount."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Try
            Response.Redirect(String.Format("~/FIN Statement/UploadTrailbalanceSchedule.aspx?"), False)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub LoadExistingCustomerName()
        Try
            ddlCustNameSchedTemp.DataSource = objclsPartnerFund.LoadExistingCustomerName(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustNameSchedTemp.DataTextField = "Cust_Name"
            ddlCustNameSchedTemp.DataValueField = "Cust_Id"
            ddlCustNameSchedTemp.DataBind()
            ddlCustNameSchedTemp.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingCustomerName")
            'Throw
        End Try
    End Sub
    Public Sub LoadExistingFinancialYear()
        Try
            ddlFinancialYearSchedTemp.DataSource = objclsPartnerFund.LoadExistingFinancialYear(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYearSchedTemp.DataTextField = "YMS_ID"
            ddlFinancialYearSchedTemp.DataValueField = "YMS_YEARID"
            ddlFinancialYearSchedTemp.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingFinancialYear")
            'Throw
        End Try
    End Sub

    'Public Sub LoadExistingBranchName()
    '    Try
    '        ddlbranchSchedTemp.DataSource = objclsPartnerFund.LoadExistingBranchName(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlbranchSchedTemp.DataTextField = "Cust_Name"
    '        ddlbranchSchedTemp.DataValueField = "Cust_Id"
    '        ddlbranchSchedTemp.DataBind()
    '        ddlbranchSchedTemp.Items.Insert(0, "Select Customer Name")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
    '        'Throw
    '    End Try
    'End Sub
    Public Sub BindCustPartner()
        Dim iCustId As Integer = 0
        Try
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                iCustId = ddlCustNameSchedTemp.SelectedValue
            End If
            ddlCustPartner.DataSource = objclsPartnerFund.LoadCustPartner(sSession.AccessCode, sSession.AccessCodeID, iCustId)
            ddlCustPartner.DataTextField = "Name"
            ddlCustPartner.DataValueField = "Id"
            ddlCustPartner.DataBind()
            ddlCustPartner.Items.Insert(0, "Select Partner")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustPartner")
            'Throw
        End Try
    End Sub
    Private Sub ddlCustNameSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustNameSchedTemp.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModal.Text = ""
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            divCustPartner.Visible = False : lblPartnershipFirmId.Text = 0 : lblShareOfProfitPercentage.Text = ""
            gvPartnershipFirms.DataSource = Nothing
            gvPartnershipFirms.DataBind()
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlCustNameSchedTemp.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)
                BindCustPartner()
                BindAllPartnershipFirms()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustNameSchedTemp_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlFinancialYearSchedTemp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYearSchedTemp.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModal.Text = ""
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            divCustPartner.Visible = False : lblPartnershipFirmId.Text = 0 : lblShareOfProfitPercentage.Text = ""
            gvPartnershipFirms.DataSource = Nothing
            gvPartnershipFirms.DataBind()
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                BindAllPartnershipFirms()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustNameSchedTemp_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCustPartner_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustPartner.SelectedIndexChanged
        Try
            lblError.Text = "" : lblModal.Text = ""
            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
            divCustPartner.Visible = False : lblPartnershipFirmId.Text = 0 : lblShareOfProfitPercentage.Text = ""
            txtOpeningBalance.Text = "" : txtUnsecuredLoanTreatedAsCapital.Text = ""
            txtInterestOnCapital.Text = "" : txtPartnersSalary.Text = ""
            txtShareOfprofit.Text = "" : lblAddTotal.Text = "" : lblLessTotal.Text = "" : txtCapitalAmount.Text = "" : txtShareOfprofit.Text = ""
            txtTransferToFixedCapital.Text = "" : txtDrawings.Text = "" : txtLessOthers.Text = "" : txtAddOthers.Text = ""
            If ddlCustPartner.SelectedIndex > 0 Then
                lblShareOfProfitPercentage.Text = objclsPartnerFund.GetCustShareOfProfitPercentage(sSession.AccessCode, sSession.AccessCodeID, ddlCustPartner.SelectedValue)
                ' txtShareOfprofit.Text = objclsPartnerFund.GetCustCapitalAmount(sSession.AccessCode, sSession.AccessCodeID, ddlCustPartner.SelectedValue)
                divCustPartner.Visible = True
                Dim iPKId As Integer = objclsPartnerFund.GetSelectedPartnershipFirmsIdFromPartnerFY(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYearSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, ddlCustPartner.SelectedValue)
                If (iPKId > 0) Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                    lblPartnershipFirmId.Text = iPKId
                    LoadSelectedPartnershipFirms(Val(lblPartnershipFirmId.Text))
                End If
            End If
            BindAllPartnershipFirms()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustPartner_SelectedIndexChanged")
        End Try
    End Sub
    Protected Sub btnAddCalculate_Click(sender As Object, e As EventArgs) Handles btnAddCalculate.Click
        Dim dOpeningBalance As Decimal, dUnsecuredLoanTreatedAsCapital As Decimal, dInterestOnCapital As Decimal, dPartnersSalary As Decimal, dShareOfprofit As Decimal, dAddOthers As Decimal
        Try
            lblError.Text = "" : lblModal.Text = ""
            lblAddTotal.Text = "" : lblLessTotal.Text = ""
            If txtOpeningBalance.Text.Trim() <> "" And IsNumeric(txtOpeningBalance.Text) = False Then
                lblError.Text = "Enter valid numeric values for Opening Balance." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtUnsecuredLoanTreatedAsCapital.Text.Trim() <> "" And IsNumeric(txtUnsecuredLoanTreatedAsCapital.Text) = False Then
                lblError.Text = "Enter valid numeric values for Capital Introduced - Unsecured Loan treated as Capital." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtInterestOnCapital.Text.Trim() <> "" And IsNumeric(txtInterestOnCapital.Text) = False Then
                lblError.Text = "Enter valid numeric values for Interest On Capital." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtPartnersSalary.Text.Trim() <> "" And IsNumeric(txtPartnersSalary.Text) = False Then
                lblError.Text = "Enter valid numeric values for Partner's Salary." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtShareOfprofit.Text.Trim() <> "" And IsNumeric(txtShareOfprofit.Text) = False Then
                lblError.Text = "Enter valid numeric values for Share Of Profit." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAddOthers.Text.Trim() <> "" And IsNumeric(txtAddOthers.Text) = False Then
                lblError.Text = "Enter valid numeric values for Others." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtOpeningBalance.Text.Trim() = "" Then dOpeningBalance = 0 Else dOpeningBalance = txtOpeningBalance.Text.Trim()
            If txtUnsecuredLoanTreatedAsCapital.Text.Trim() = "" Then dUnsecuredLoanTreatedAsCapital = 0 Else dUnsecuredLoanTreatedAsCapital = txtUnsecuredLoanTreatedAsCapital.Text.Trim()
            If txtInterestOnCapital.Text.Trim() = "" Then dInterestOnCapital = 0 Else dInterestOnCapital = txtInterestOnCapital.Text.Trim()
            If txtPartnersSalary.Text.Trim() = "" Then dPartnersSalary = 0 Else dPartnersSalary = txtPartnersSalary.Text.Trim()
            If txtShareOfprofit.Text.Trim() = "" Then dShareOfprofit = 0 Else dShareOfprofit = txtShareOfprofit.Text.Trim()
            If txtAddOthers.Text.Trim() = "" Then dAddOthers = 0 Else dAddOthers = txtAddOthers.Text.Trim()
            lblAddTotal.Text = dOpeningBalance + dUnsecuredLoanTreatedAsCapital + dInterestOnCapital + dPartnersSalary + dShareOfprofit + dAddOthers
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddCalculate_Click")
        End Try
    End Sub
    Protected Sub btnLessCalculate_Click(sender As Object, e As EventArgs) Handles btnLessCalculate.Click
        Dim dOpeningBalance As Decimal, dUnsecuredLoanTreatedAsCapital As Decimal, dInterestOnCapital As Decimal, dPartnersSalary As Decimal, dShareOfprofit As Decimal, dAddOthers As Decimal
        Dim dTransferToFixedCapital As Decimal, dDrawings As Decimal, dLessOthers As Decimal, dCapitalAmount As Decimal
        Try
            lblError.Text = "" : lblModal.Text = ""
            lblAddTotal.Text = "" : lblLessTotal.Text = ""
            If txtOpeningBalance.Text.Trim() <> "" And IsNumeric(txtOpeningBalance.Text) = False Then
                lblError.Text = "Enter valid numeric values for Opening Balance." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtUnsecuredLoanTreatedAsCapital.Text.Trim() <> "" And IsNumeric(txtUnsecuredLoanTreatedAsCapital.Text) = False Then
                lblError.Text = "Enter valid numeric values for Capital Introduced - Unsecured Loan treated as Capital." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtInterestOnCapital.Text.Trim() <> "" And IsNumeric(txtInterestOnCapital.Text) = False Then
                lblError.Text = "Enter valid numeric values for Interest On Capital." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtPartnersSalary.Text.Trim() <> "" And IsNumeric(txtPartnersSalary.Text) = False Then
                lblError.Text = "Enter valid numeric values for Partner's Salary." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtShareOfprofit.Text.Trim() <> "" And IsNumeric(txtShareOfprofit.Text) = False Then
                lblError.Text = "Enter valid numeric values for Share Of Profit." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAddOthers.Text.Trim() <> "" And IsNumeric(txtAddOthers.Text) = False Then
                lblError.Text = "Enter valid numeric values for Others." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtOpeningBalance.Text.Trim() = "" Then dOpeningBalance = 0 Else dOpeningBalance = txtOpeningBalance.Text.Trim()
            If txtUnsecuredLoanTreatedAsCapital.Text.Trim() = "" Then dUnsecuredLoanTreatedAsCapital = 0 Else dUnsecuredLoanTreatedAsCapital = txtUnsecuredLoanTreatedAsCapital.Text.Trim()
            If txtInterestOnCapital.Text.Trim() = "" Then dInterestOnCapital = 0 Else dInterestOnCapital = txtInterestOnCapital.Text.Trim()
            If txtPartnersSalary.Text.Trim() = "" Then dPartnersSalary = 0 Else dPartnersSalary = txtPartnersSalary.Text.Trim()
            If txtShareOfprofit.Text.Trim() = "" Then dShareOfprofit = 0 Else dShareOfprofit = txtShareOfprofit.Text.Trim()
            If txtAddOthers.Text.Trim() = "" Then dAddOthers = 0 Else dAddOthers = txtAddOthers.Text.Trim()
            lblAddTotal.Text = dOpeningBalance + dUnsecuredLoanTreatedAsCapital + dInterestOnCapital + dPartnersSalary + dShareOfprofit + dAddOthers

            If txtTransferToFixedCapital.Text.Trim() <> "" And IsNumeric(txtTransferToFixedCapital.Text) = False Then
                lblError.Text = "Enter valid numeric values for Transfer To Fixed Capital." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtDrawings.Text.Trim() <> "" And IsNumeric(txtDrawings.Text) = False Then
                lblError.Text = "Enter valid numeric values for Drawings." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtLessOthers.Text.Trim() <> "" And IsNumeric(txtLessOthers.Text) = False Then
                lblError.Text = "Enter valid numeric values for Others." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

            If txtTransferToFixedCapital.Text.Trim() = "" Then
                dTransferToFixedCapital = 0
            Else
                dTransferToFixedCapital = txtTransferToFixedCapital.Text.Trim()
            End If
            If txtCapitalAmount.Text.Trim() = "" Then
                dCapitalAmount = 0
            Else
                dCapitalAmount = txtCapitalAmount.Text.Trim()
            End If
            If txtDrawings.Text.Trim() = "" Then dDrawings = 0 Else dDrawings = txtDrawings.Text.Trim()
            If txtLessOthers.Text.Trim() = "" Then dLessOthers = 0 Else dLessOthers = txtLessOthers.Text.Trim()

            lblLessTotal.Text = dTransferToFixedCapital + dDrawings + dLessOthers
            'lblTotalHeading.Text = "Total"
            'lblTotal.Text = Val(lblAddTotal.Text) - Val(lblLessTotal.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLessCalculate_Click")
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim dOpeningBalance As Decimal, dUnsecuredLoanTreatedAsCapital As Decimal, dInterestOnCapital As Decimal, dPartnersSalary As Decimal, dShareOfprofit As Decimal, dAddOthers As Decimal
        Dim dTransferToFixedCapital As Decimal, dDrawings As Decimal, dLessOthers As Decimal
        Dim objPF As New strPartnership_Firms
        Dim Arr() As String
        Try
            lblError.Text = "" : lblModal.Text = ""
            If ddlCustNameSchedTemp.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlCustPartner.SelectedIndex = 0 Then
                lblError.Text = "Select Partner Name." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            'If Val(lblTotal.Text) = 0 Then
            '    lblError.Text = "Please Calculate Partner's - Current Capital A/C details" : lblModal.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            '    Exit Sub
            'End If

            If txtOpeningBalance.Text.Trim() = "" Then dOpeningBalance = 0 Else dOpeningBalance = txtOpeningBalance.Text.Trim()
            If txtUnsecuredLoanTreatedAsCapital.Text.Trim() = "" Then dUnsecuredLoanTreatedAsCapital = 0 Else dUnsecuredLoanTreatedAsCapital = txtUnsecuredLoanTreatedAsCapital.Text.Trim()
            If txtInterestOnCapital.Text.Trim() = "" Then dInterestOnCapital = 0 Else dInterestOnCapital = txtInterestOnCapital.Text.Trim()
            If txtPartnersSalary.Text.Trim() = "" Then dPartnersSalary = 0 Else dPartnersSalary = txtPartnersSalary.Text.Trim()
            If txtShareOfprofit.Text.Trim() = "" Then dShareOfprofit = 0 Else dShareOfprofit = txtShareOfprofit.Text.Trim()
            If txtAddOthers.Text.Trim() = "" Then dAddOthers = 0 Else dAddOthers = txtAddOthers.Text.Trim()
            If txtTransferToFixedCapital.Text.Trim() = "" Then dTransferToFixedCapital = 0 Else dTransferToFixedCapital = txtTransferToFixedCapital.Text.Trim()
            If txtDrawings.Text.Trim() = "" Then dDrawings = 0 Else dDrawings = txtDrawings.Text.Trim()
            If txtLessOthers.Text.Trim() = "" Then dLessOthers = 0 Else dLessOthers = txtLessOthers.Text.Trim()

            objPF.iAPF_ID = 0
            objPF.iAPF_YearID = ddlFinancialYearSchedTemp.SelectedValue
            objPF.iAPF_Cust_ID = ddlCustNameSchedTemp.SelectedValue
            objPF.iAPF_Branch_ID = 0
            'If ddlbranchSchedTemp.Items.Count > 0 Then
            '    If ddlbranchSchedTemp.SelectedIndex > 0 Then
            '        objPF.iAPF_Branch_ID = ddlbranchSchedTemp.SelectedValue
            '    End If
            'End If
            objPF.iAPF_Partner_ID = ddlCustPartner.SelectedValue
            objPF.dAPF_OpeningBalance = String.Format("{0:0.00}", Convert.ToDecimal(dOpeningBalance))
            objPF.dAPF_UnsecuredLoanTreatedAsCapital = String.Format("{0:0.00}", Convert.ToDecimal(dUnsecuredLoanTreatedAsCapital))
            objPF.dAPF_InterestOnCapital = String.Format("{0:0.00}", Convert.ToDecimal(dInterestOnCapital))
            objPF.dAPF_PartnersSalary = String.Format("{0:0.00}", Convert.ToDecimal(dPartnersSalary))
            objPF.dAPF_ShareOfprofit = String.Format("{0:0.00}", Convert.ToDecimal(dShareOfprofit))
            objPF.dAPF_TransferToFixedCapital = String.Format("{0:0.00}", Convert.ToDecimal(dTransferToFixedCapital))
            objPF.dAPF_Drawings = String.Format("{0:0.00}", Convert.ToDecimal(dDrawings))
            objPF.dAPF_AddOthers = String.Format("{0:0.00}", Convert.ToDecimal(dAddOthers))
            objPF.dAPF_LessOthers = String.Format("{0:0.00}", Convert.ToDecimal(dLessOthers))
            objPF.sAPF_CapitalAmount = txtCapitalAmount.Text
            objPF.iAPF_CrBy = sSession.UserID
            objPF.sAPF_IPAddress = sSession.IPAddress
            objPF.iAPF_CompID = sSession.AccessCodeID
            Arr = objclsPartnerFund.SavePartnershipFirms(sSession.AccessCode, objPF)

            ddlCustPartner.SelectedIndex = 0
            ddlCustPartner_SelectedIndexChanged(sender, e)

            lblError.Text = "Successfully Saved" : lblModal.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim dOpeningBalance As Decimal, dUnsecuredLoanTreatedAsCapital As Decimal, dInterestOnCapital As Decimal, dPartnersSalary As Decimal, dShareOfprofit As Decimal, dAddOthers As Decimal
        Dim dTransferToFixedCapital As Decimal, dDrawings As Decimal, dLessOthers As Decimal
        Dim objPF As New strPartnership_Firms
        Dim Arr() As String
        Try
            lblError.Text = "" : lblModal.Text = ""
            If ddlCustNameSchedTemp.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlCustPartner.SelectedIndex = 0 Then
                lblError.Text = "Select Partner Name." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            'If Val(lblTotal.Text) = 0 Then
            '    lblError.Text = "Please Calculate Partner's - Current Capital A/C details" : lblModal.Text = lblError.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
            '    Exit Sub
            'End If

            If txtOpeningBalance.Text.Trim() = "" Then dOpeningBalance = 0 Else dOpeningBalance = txtOpeningBalance.Text.Trim()
            If txtUnsecuredLoanTreatedAsCapital.Text.Trim() = "" Then dUnsecuredLoanTreatedAsCapital = 0 Else dUnsecuredLoanTreatedAsCapital = txtUnsecuredLoanTreatedAsCapital.Text.Trim()
            If txtInterestOnCapital.Text.Trim() = "" Then dInterestOnCapital = 0 Else dInterestOnCapital = txtInterestOnCapital.Text.Trim()
            If txtPartnersSalary.Text.Trim() = "" Then dPartnersSalary = 0 Else dPartnersSalary = txtPartnersSalary.Text.Trim()
            If txtShareOfprofit.Text.Trim() = "" Then dShareOfprofit = 0 Else dShareOfprofit = txtShareOfprofit.Text.Trim()
            If txtAddOthers.Text.Trim() = "" Then dAddOthers = 0 Else dAddOthers = txtAddOthers.Text.Trim()
            If txtTransferToFixedCapital.Text.Trim() = "" Then dTransferToFixedCapital = 0 Else dTransferToFixedCapital = txtTransferToFixedCapital.Text.Trim()
            If txtDrawings.Text.Trim() = "" Then dDrawings = 0 Else dDrawings = txtDrawings.Text.Trim()
            If txtLessOthers.Text.Trim() = "" Then dLessOthers = 0 Else dLessOthers = txtLessOthers.Text.Trim()

            If Val(lblPartnershipFirmId.Text) > 0 Then
                objPF.iAPF_ID = Val(lblPartnershipFirmId.Text)
            Else
                objPF.iAPF_ID = 0
            End If
            objPF.iAPF_YearID = ddlFinancialYearSchedTemp.SelectedValue
            objPF.iAPF_Cust_ID = ddlCustNameSchedTemp.SelectedValue
            objPF.iAPF_Branch_ID = 0
            'If ddlbranchSchedTemp.Items.Count > 0 Then
            '    If ddlbranchSchedTemp.SelectedIndex > 0 Then
            '        objPF.iAPF_Branch_ID = ddlbranchSchedTemp.SelectedValue
            '    End If
            'End If
            objPF.iAPF_Partner_ID = ddlCustPartner.SelectedValue
            objPF.dAPF_OpeningBalance = String.Format("{0:0.00}", Convert.ToDecimal(dOpeningBalance))
            objPF.dAPF_UnsecuredLoanTreatedAsCapital = String.Format("{0:0.00}", Convert.ToDecimal(dUnsecuredLoanTreatedAsCapital))
            objPF.dAPF_InterestOnCapital = String.Format("{0:0.00}", Convert.ToDecimal(dInterestOnCapital))
            objPF.dAPF_PartnersSalary = String.Format("{0:0.00}", Convert.ToDecimal(dPartnersSalary))
            objPF.dAPF_ShareOfprofit = String.Format("{0:0.00}", Convert.ToDecimal(dShareOfprofit))
            objPF.dAPF_TransferToFixedCapital = String.Format("{0:0.00}", Convert.ToDecimal(dTransferToFixedCapital))
            objPF.dAPF_Drawings = String.Format("{0:0.00}", Convert.ToDecimal(dDrawings))
            objPF.dAPF_AddOthers = String.Format("{0:0.00}", Convert.ToDecimal(dAddOthers))
            objPF.dAPF_LessOthers = String.Format("{0:0.00}", Convert.ToDecimal(dLessOthers))
            objPF.sAPF_CapitalAmount = txtCapitalAmount.Text
            objPF.iAPF_CrBy = sSession.UserID
            objPF.sAPF_IPAddress = sSession.IPAddress
            objPF.iAPF_CompID = sSession.AccessCodeID
            Arr = objclsPartnerFund.SavePartnershipFirms(sSession.AccessCode, objPF)

            ddlCustPartner.SelectedIndex = 0
            ddlCustPartner_SelectedIndexChanged(sender, e)

            lblError.Text = "Successfully Updated" : lblModal.Text = lblError.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        End Try
    End Sub
    Private Sub LoadSelectedPartnershipFirms(ByVal iPartnershipFirmId As Integer)
        Dim dOpeningBalance As Decimal = 0, dUnsecuredLoanTreatedAsCapital As Decimal = 0, dInterestOnCapital As Decimal = 0, dPartnersSalary As Decimal = 0, dShareOfprofit As Decimal = 0, dAddOthers As Decimal = 0
        Dim dTransferToFixedCapital As Decimal = 0, dDrawings As Decimal = 0, dLessOthers As Decimal = 0
        Dim dt As New DataTable
        Dim dCapitalAmount As Decimal = 0
        Try
            dt = objclsPartnerFund.GetSelectedPartnershipFirms(sSession.AccessCode, sSession.AccessCodeID, iPartnershipFirmId)
            If dt.Rows.Count > 0 Then
                lblPartnershipFirmId.Text = iPartnershipFirmId
                If IsDBNull(dt.Rows(0)("APF_Partner_ID")) = False Then
                    ddlCustPartner.SelectedValue = dt.Rows(0)("APF_Partner_ID")
                    lblShareOfProfitPercentage.Text = objclsPartnerFund.GetCustShareOfProfitPercentage(sSession.AccessCode, sSession.AccessCodeID, ddlCustPartner.SelectedValue)
                End If
                If IsDBNull(dt.Rows(0)("APF_OpeningBalance")) = False Then
                    txtOpeningBalance.Text = dt.Rows(0)("APF_OpeningBalance")
                    dOpeningBalance = dt.Rows(0)("APF_OpeningBalance")
                End If
                If IsDBNull(dt.Rows(0)("APF_UnsecuredLoanTreatedAsCapital")) = False Then
                    txtUnsecuredLoanTreatedAsCapital.Text = dt.Rows(0)("APF_UnsecuredLoanTreatedAsCapital")
                    dUnsecuredLoanTreatedAsCapital = dt.Rows(0)("APF_UnsecuredLoanTreatedAsCapital")
                End If
                If IsDBNull(dt.Rows(0)("APF_InterestOnCapital")) = False Then
                    txtInterestOnCapital.Text = dt.Rows(0)("APF_InterestOnCapital")
                    dInterestOnCapital = dt.Rows(0)("APF_InterestOnCapital")
                End If
                If IsDBNull(dt.Rows(0)("APF_PartnersSalary")) = False Then
                    txtPartnersSalary.Text = dt.Rows(0)("APF_PartnersSalary")
                    dPartnersSalary = dt.Rows(0)("APF_PartnersSalary")
                End If
                If IsDBNull(dt.Rows(0)("APF_ShareOfprofit")) = False Then
                    txtShareOfprofit.Text = dt.Rows(0)("APF_ShareOfprofit")
                    dShareOfprofit = dt.Rows(0)("APF_ShareOfprofit")
                End If
                If IsDBNull(dt.Rows(0)("APF_AddOthers")) = False Then
                    txtAddOthers.Text = dt.Rows(0)("APF_AddOthers")
                    dAddOthers = dt.Rows(0)("APF_AddOthers")
                End If
                lblAddTotal.Text = dOpeningBalance + dUnsecuredLoanTreatedAsCapital + dInterestOnCapital + dPartnersSalary + dShareOfprofit + dAddOthers

                If IsDBNull(dt.Rows(0)("APF_TransferToFixedCapital")) = False Then
                    txtTransferToFixedCapital.Text = dt.Rows(0)("APF_TransferToFixedCapital")
                    dTransferToFixedCapital = dt.Rows(0)("APF_TransferToFixedCapital")
                End If
                If IsDBNull(dt.Rows(0)("APF_Drawings")) = False Then
                    txtDrawings.Text = dt.Rows(0)("APF_Drawings")
                    dDrawings = dt.Rows(0)("APF_Drawings")
                End If
                If IsDBNull(dt.Rows(0)("APF_LessOthers")) = False Then
                    txtLessOthers.Text = dt.Rows(0)("APF_LessOthers")
                    dLessOthers = dt.Rows(0)("APF_LessOthers")
                End If
                If IsDBNull(dt.Rows(0)("APF_CapitalAmount")) = False Then
                    txtCapitalAmount.Text = dt.Rows(0)("APF_CapitalAmount")
                    dCapitalAmount = dt.Rows(0)("APF_CapitalAmount")
                Else
                    txtCapitalAmount.Text = "0"
                    dCapitalAmount = "0"
                End If
                lblLessTotal.Text = dTransferToFixedCapital + dDrawings + dLessOthers + dCapitalAmount
                'lblTotalHeading.Text = "Total"
                'lblTotal.Text = Val(lblAddTotal.Text) - Val(lblLessTotal.Text)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    Private Sub BindAllPartnershipFirms()
        Dim dt As New DataTable
        Try
            If ddlCustNameSchedTemp.SelectedIndex > 0 Then
                dt = objclsPartnerFund.LoadAllPartnershipFirms(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYearSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(5, 4), ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(0, 4), "No")
                If dt.Rows.Count > 0 Then
                    gvPartnershipFirms.DataSource = dt
                    gvPartnershipFirms.DataBind()
                Else
                    If ddlCustPartner.SelectedIndex <> 0 Then
                        dt = objclsPartnerFund.GetPandLAmt(sSession.AccessCode, ddlFinancialYearSchedTemp.SelectedValue, ddlCustPartner.SelectedValue, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(5, 4))
                        If dt.Columns.Contains("PandLAmt") AndAlso Not IsDBNull(dt.Rows(0)("PandLAmt")) Then
                            txtShareOfprofit.Text = dt.Rows(0)("PandLAmt")
                        Else
                            txtShareOfprofit.Text = ""
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub gvPartnershipFirms_PreRender(sender As Object, e As EventArgs) Handles gvPartnershipFirms.PreRender
        Try
            If gvPartnershipFirms.Rows.Count > 0 Then
                gvPartnershipFirms.UseAccessibleHeader = True
                gvPartnershipFirms.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPartnershipFirms.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartnershipFirms_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub gvPartnershipFirms_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPartnershipFirms.RowCommand
    '    Dim lblPFId As New Label
    '    Try
    '        Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
    '        lblPFId = CType(clickedRow.FindControl("lblPFId"), Label)
    '        If e.CommandName = "Select" Then
    '            divCustPartner.Visible = True
    '            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
    '            lblPartnershipFirmId.Text = Val(lblPFId.Text)
    '            LoadSelectedPartnershipFirms(Val(lblPartnershipFirmId.Text))
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartnershipFirms_RowCommand" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
    '    End Try
    'End Sub
    Protected Sub gvPartnershipFirms_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPartnershipFirms.RowDataBound
        Dim lblSlNo As New Label, lblPARTICULARS As New Label, lblFYCData As New Label, lblFYPData As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblSlNo = CType(e.Row.FindControl("lblSlNo"), Label)
                lblPARTICULARS = CType(e.Row.FindControl("lblPARTICULARS"), Label)
                lblFYCData = CType(e.Row.FindControl("lblFYCData"), Label)
                lblFYPData = CType(e.Row.FindControl("lblFYPData"), Label)
                If lblSlNo.Text <> "" Then
                    lblSlNo.Font.Bold = True : lblPARTICULARS.Font.Bold = True : lblFYCData.Font.Bold = True : lblFYPData.Font.Bold = True
                End If
                If lblPARTICULARS.Text = "Total" Or lblPARTICULARS.Text = "Add Total" Or lblPARTICULARS.Text = "Less Total" Or lblPARTICULARS.Text = "PARTICULARS" Or lblPARTICULARS.Text = "TOTAL - CURRENT A/C CAPITAL" Or lblPARTICULARS.Text = "PARTNER'S FIXED CAPITAL" Or lblPARTICULARS.Text = "Total Capital" Then
                    'lblPARTICULARS.ForeColor = System.Drawing.Color.Transparent
                    lblPARTICULARS.Font.Bold = True : lblFYCData.Font.Bold = True : lblFYPData.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartnershipFirms_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblModal.Text = ""
            If ddlCustNameSchedTemp.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If gvPartnershipFirms.Rows.Count = 0 Then
                lblError.Text = "No Partnership Firms details for this Customer." : lblModal.Text = "No Partnership Firms details for this Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                Exit Try
            End If
            dt = objclsPartnerFund.LoadAllPartnershipFirms(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYearSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(5, 4), ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(0, 4), "Yes")
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/PartnershipFirms.rdlc")
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustNameSchedTemp.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim FYCheaderData As ReportParameter() = New ReportParameter() {New ReportParameter("FYCheaderData", "As at 31st March " & ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(5, 4))}
            ReportViewer1.LocalReport.SetParameters(FYCheaderData)
            Dim FYPheaderData As ReportParameter() = New ReportParameter() {New ReportParameter("FYPheaderData", "As at 31st March " & ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(0, 4))}
            ReportViewer1.LocalReport.SetParameters(FYPheaderData)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Schedules", "Partnership Firms", "PDF", ddlFinancialYearSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, 0, 0, sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=PartnershipFirms" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = "" : lblModal.Text = ""
            If ddlCustNameSchedTemp.SelectedIndex = 0 Then
                lblError.Text = "Select Customer Name." : lblModal.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If gvPartnershipFirms.Rows.Count = 0 Then
                lblError.Text = "No Partnership Firms details for this Customer." : lblModal.Text = "No Partnership Firms details for this Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                Exit Try
            End If
            dt = objclsPartnerFund.LoadAllPartnershipFirms(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYearSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(5, 4), ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(0, 4), "Yes")
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FINStatement/PartnershipFirms.rdlc")
            Dim CustomerName As ReportParameter() = New ReportParameter() {New ReportParameter("CustomerName", ddlCustNameSchedTemp.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(CustomerName)
            Dim FYCheaderData As ReportParameter() = New ReportParameter() {New ReportParameter("FYCheaderData", "As at 31st March " & ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(5, 4))}
            ReportViewer1.LocalReport.SetParameters(FYCheaderData)
            Dim FYPheaderData As ReportParameter() = New ReportParameter() {New ReportParameter("FYPheaderData", "As at 31st March " & ddlFinancialYearSchedTemp.SelectedItem.Text.Substring(0, 4))}
            ReportViewer1.LocalReport.SetParameters(FYPheaderData)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Finalisation Of Account", "Partnership Firms", "Excel", ddlFinancialYearSchedTemp.SelectedValue, ddlCustNameSchedTemp.SelectedValue, 0, 0, sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=PartnershipFirms" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnCustPartner_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnCustPartner.Click
        Try
            lblError.Text = "" : lblPartnerError.Text = ""
            iCustPartnerPKId = 0
            btnSavePartner.Text = "Save Partner Details"
            txtPartnerName.Text = "" : txtPartnerDOJ.Text = "" : txtPartnerPAN.Text = ""
            txtPartnerShareOfProfit.Text = "" : txtPartnerCapitalAmount.Text = ""
            LoadCustAllStatutoryPartnerDetails()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnCustPartner_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnNewPartner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewPartner.Click
        Try
            lblError.Text = "" : lblPartnerError.Text = ""
            iCustPartnerPKId = 0
            btnSavePartner.Text = "Save Partner Details"
            txtPartnerName.Text = "" : txtPartnerDOJ.Text = "" : txtPartnerPAN.Text = ""
            txtPartnerShareOfProfit.Text = "" : txtPartnerCapitalAmount.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewPartner_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSavePartner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavePartner.Click
        Dim objsStatutoryPartner As New strStatutoryPartner
        Dim Arr As Array
        Try
            If ddlCustNameSchedTemp.SelectedIndex = 0 Then
                lblPartnerError.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Exit Sub
            End If
            If txtPartnerName.Text.Trim.Length > 100 Then
                lblPartnerError.Text = "Partner Name exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                txtPartnerName.Focus()
                Exit Sub
            End If
            If (objCust.CheckCustPartnerName(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, objclsGRACeGeneral.SafeSQL(txtPartnerName.Text.Trim()), iCustPartnerPKId) = True) Then
                lblPartnerError.Text = "Partner Name already Exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Return
            End If
            If txtPartnerPAN.Text.Trim.Length > 25 Then
                lblPartnerError.Text = "Partner PAN exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Exit Sub
            End If
            If txtPartnerShareOfProfit.Text.Trim.Length > 6 Then
                lblPartnerError.Text = "Share Of Profit exceeded maximum size(max 6 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Exit Sub
            End If

            Dim dTotalShareOfProfit As Decimal = objCust.GetTotalShareOfProfit(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, iCustPartnerPKId)
            If dTotalShareOfProfit >= 100 Then
                lblPartnerError.Text = "The sum of the share of profit should be within or equal to 100%."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Exit Sub
            End If
            Dim dRemaining = 100 - (dTotalShareOfProfit + Val(txtPartnerShareOfProfit.Text.Trim))
            If dRemaining < 0 Then
                lblPartnerError.Text = "Share of profit should be less than or equal to " + (100 - dTotalShareOfProfit).ToString() + "%."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Exit Sub
            End If
            If txtPartnerCapitalAmount.Text.Trim() <> "" And IsNumeric(txtPartnerCapitalAmount.Text) = False Then
                lblPartnerError.Text = "Enter valid Capital Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
                Exit Sub
            End If

            objsStatutoryPartner.iSSP_Id = iCustPartnerPKId
            objsStatutoryPartner.iSSP_CustID = ddlCustNameSchedTemp.SelectedValue
            objsStatutoryPartner.sSSP_PartnerName = objclsGRACeGeneral.SafeSQL(txtPartnerName.Text.Trim)
            objsStatutoryPartner.dSSP_DOJ = Date.ParseExact(txtPartnerDOJ.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objsStatutoryPartner.sSSP_PAN = objclsGRACeGeneral.SafeSQL(txtPartnerPAN.Text.Trim)
            objsStatutoryPartner.dSSP_ShareOfProfit = String.Format("{0:0.00}", Convert.ToDecimal(txtPartnerShareOfProfit.Text))
            objsStatutoryPartner.dSSP_CapitalAmount = String.Format("{0:0.00}", Convert.ToDecimal(txtPartnerCapitalAmount.Text))
            objsStatutoryPartner.iSSP_CRBY = sSession.UserID
            objsStatutoryPartner.dSSP_CRON = DateTime.Today
            objsStatutoryPartner.iSSP_UpdatedBy = sSession.UserID
            objsStatutoryPartner.dSSP_UpdatedOn = DateTime.Today
            objsStatutoryPartner.sSSP_IPAddress = sSession.IPAddress
            objsStatutoryPartner.iSSP_CompID = sSession.AccessCodeID
            objsStatutoryPartner.sSSP_STATUS = "C"
            objsStatutoryPartner.sSSP_DelFlag = "A"

            Arr = objCust.SaveCustomerStatutoryPartner(sSession.AccessCode, objsStatutoryPartner)
            ddlCustNameSchedTemp_SelectedIndexChanged(sender, e)
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved." : lblModal.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If
            If Arr(0) = 2 Then
                lblError.Text = "Successfully Updated." : lblModal.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSavePartner_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadCustAllStatutoryPartnerDetails()
        Dim dt As New DataTable
        Try
            gvPartner.DataSource = Nothing
            gvPartner.DataBind()
            dt = objCust.GetCustomerPartnerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, 0)
            If dt.Rows.Count > 0 Then
                gvPartner.DataSource = dt
                gvPartner.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustAllStatutoryPartnerDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPartner_RowCommand(sender As Object, e As GridViewRowEventArgs) Handles gvPartner.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Dim lblStatus As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                'imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                'imgbtnStatus.ToolTip = "Edit"
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                lblStatus = DirectCast(e.Row.FindControl("lblStatus"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                'If lblStatus.Text = "A" Then
                '    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                'Else
                '    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartner_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPartner_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPartner.RowCommand
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblPartnerPkID As Label = DirectCast(clickedRow.FindControl("lblPartnerPkID"), Label)
            Dim lblStatus As Label = DirectCast(clickedRow.FindControl("lblStatus"), Label)
            iCustPartnerPKId = 0
            If e.CommandName = "EditRow" Then
                iCustPartnerPKId = Val(lblPartnerPkID.Text)
                LoadCustSelectedStatutoryPartnerDetails()
            End If
            If e.CommandName = "Status" Then
                If lblStatus.Text = "A" Then
                    objCust.CustPartnerApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCustNameSchedTemp.SelectedValue, Val(lblPartnerPkID.Text), sSession.IPAddress, "DeActivated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Partner Master", "De-Activated", ddlCustNameSchedTemp.SelectedValue, "", lblPartnerPkID.Text, "", sSession.IPAddress)
                    lblPartnerError.Text = "Successfully De-Activated."
                ElseIf lblStatus.Text = "D" Then
                    objCust.CustPartnerApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCustNameSchedTemp.SelectedValue, Val(lblPartnerPkID.Text), sSession.IPAddress, "Activated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Partner Master", "Activated", ddlCustNameSchedTemp.SelectedValue, "", lblPartnerPkID.Text, "", sSession.IPAddress)
                    lblPartnerError.Text = "Successfully Activated."
                End If
                LoadCustAllStatutoryPartnerDetails()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myPartnerModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartner_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPartner_PreRender(sender As Object, e As EventArgs) Handles gvPartner.PreRender
        Dim dt As New DataTable
        Try
            If gvPartner.Rows.Count > 0 Then
                gvPartner.UseAccessibleHeader = True
                gvPartner.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPartner.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartner_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadCustSelectedStatutoryPartnerDetails()
        Dim dt As New DataTable
        Try
            dt = objCust.GetCustomerPartnerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCustNameSchedTemp.SelectedValue, iCustPartnerPKId)
            If dt.Rows.Count > 0 Then
                btnSavePartner.Text = "Update Partner Details"
                txtPartnerName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Name"))
                txtPartnerDOJ.Text = dt.Rows(0)("DOJ")
                txtPartnerPAN.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("PAN"))
                txtPartnerShareOfProfit.Text = dt.Rows(0)("ShareOfProfit")
                txtPartnerCapitalAmount.Text = dt.Rows(0)("CapitalAmount")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustSelectedStatutoryPartnerDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
End Class
