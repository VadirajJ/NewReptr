Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class JournalEntryDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "JournalEntryDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objGen As New clsGRACeGeneral
    Private objJE As New clsJournalEntry
    Private objCOA As New clsChartOfAccounts
    Private Shared sSession As AllSession
    Public dtMerge As New DataTable
    Private Shared iDebitID As Integer = 0
    Private Shared iCreditID As Integer = 0
    Private Shared iJEPKID As Integer = 0
    Private Shared lblJEdetId As New Label
    Private Shared sCustomerAudit As String
    Private objUT As New ClsUploadTailBal
    Dim dt As New DataTable
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnApprove.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim sMasterType As String = ""
        Dim sMasterID As String = ""
        Dim spartyId As String = ""
        Dim sYearId As String = ""
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                LoadCustomers()
                BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                    Session("AllSession") = sSession
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                End If
                imgbtnUpdate.Visible = False ' imgbtnAdd.Visible = False : imgbtnSave.Visible = False :
                imgbtnAdd.Visible = True : imgbtnSave.Visible = True : imgbtnApprove.Visible = False
                divAdvance.Visible = False : divPayment.Visible = False : imgbtnHistory.Visible = False
                BinPartyOrCustomerORGL() : BindCustomerAuditorDetails()
                LoadBillType() : iJEPKID = 0

                Me.txtAdvancePayment.Attributes.Add("onblur", "return CheckAdvancePayment()")
                '     RFVCustomerParty.InitialValue = "Select Customer/GL" : RFVCustomerParty.ErrorMessage = "Select Customer/GL."
                RFVdbGL.InitialValue = "Select GL Code" : RFVdbGL.ErrorMessage = "Select General Ledger."
                RFVCrGL.InitialValue = "Select GL Code" : RFVCrGL.ErrorMessage = "Select General Ledger."
                RFVParty.InitialValue = "Select Customer/Party" : RFVParty.ErrorMessage = "Select Customer/Party."
                RFVBillType.InitialValue = "Select Voucher Type" : RFVBillType.ErrorMessage = "Select Voucher Type."
                REFBillDate.ValidationExpression = "^[0-3]?[0-9]\/[01]?[0-9]\/[12][90][0-9][0-9]$"
                REFBillDate.ErrorMessage = "Enter Valid Date Format."
                REVEBillAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                REVEBillAmount.ErrorMessage = "Enter Valid Bill Amount."
                REVAdvance.ValidationExpression = "^[0-9]\d*(\.\d+)?$"
                REVAdvance.ErrorMessage = "Enter Valid Advance Amount."
                RFVDbHead.InitialValue = "Select Head of Account" : RFVDbHead.ErrorMessage = "Select Head of Accounts."
                RFVdbGL.InitialValue = "Select GL Code" : RFVdbGL.ErrorMessage = "Select General Ledger."
                RFVDebitAmount.ErrorMessage = "Enter Debit Amount."
                RFVDesc.ErrorMessage = "Enter Description"
                REVDebitAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$" : REVDebitAmount.ErrorMessage = "Enter Valid Debit Amount."
                RFVCrHead.InitialValue = "Select Head of Account" : RFVCrHead.ErrorMessage = "Select Head of Accounts."
                RFVCrGL.InitialValue = "Select GL Code" : RFVCrGL.ErrorMessage = "Select General Ledger."
                RFVCreditAmount.ErrorMessage = "Enter Credit Amount."
                REVCreditAmount.ValidationExpression = "^[0-9]\d*(\.\d+)?$" : REVCreditAmount.ErrorMessage = "Enter Valid Credit Amount."
                RFVComments.ErrorMessage = "Enter Comments."
                REVComments.ValidationExpression = "^[\s\S]{0,5000}$" : REVComments.ErrorMessage = "Comments exceeded maximum size(max 5000 character)."
                sMasterID = Request.QueryString("MasterID")
                spartyId = Request.QueryString("PartyID")
                sYearId = Request.QueryString("StatusID")
                Session("DataTable") = Nothing
                If sMasterID <> "" Then
                    ddlFinancialYear.SelectedValue = objGen.DecryptQueryString(Request.QueryString("StatusID"))
                    ddlParty.SelectedValue = objGen.DecryptQueryString(Request.QueryString("PartyID"))
                    If ddlParty.SelectedIndex <> 0 Then
                        ddlParty_SelectedIndexChanged(sender, e)
                    End If
                    LoadExistingJEs(ddlParty.SelectedValue, sCustomerAudit, sSession.ScheduleBranchId)
                    ddlExistJE.SelectedValue = objGen.DecryptQueryString(Request.QueryString("MasterID"))
                    ddlBranch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("BranchID"))
                    sSession.ScheduleBranchId = ddlBranch.SelectedValue
                    Session("AllSession") = sSession
                    'ddlBranch.SelectedValue = objGen.DecryptQueryString(Request.QueryString("BranchID"))
                    ddlExistJE_SelectedIndexChanged(sender, e)
                Else
                    '       ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    If sSession.CustomerID <> 0 Then
                        ddlParty.SelectedValue = sSession.CustomerID
                        If ddlParty.SelectedIndex > 0 Then
                            ddlParty_SelectedIndexChanged(sender, e)
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
    Public Sub BinPartyOrCustomerORGL()
        Try
            'ddlCustomerParty.Items.Insert(0, "Select Customer/GL")
            'ddlCustomerParty.Items.Insert(1, "Customer")
            'ddlCustomerParty.Items.Insert(2, "General Ledger")
            'ddlCustomerParty.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BinPartyOrCustomerORGL")
            'Throw
        End Try
    End Sub
    Public Function LoadBillType()
        Try
            ddlBillType.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "JE")
            ddlBillType.DataTextField = "cmm_Desc"
            ddlBillType.DataValueField = "cmm_ID"
            ddlBillType.DataBind()
            ddlBillType.Items.Insert(0, "Select JE Type")
        Catch ex As Exception

        End Try
    End Function
    Public Sub BindHeadofAccounts()
        Dim dt As DataTable
        Try

            dt = objJE.LoadDeschead(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
            ddldbHead.DataSource = dt
            ddldbHead.DataTextField = "ATBU_Description"
            ddldbHead.DataValueField = "ATBU_ID"
            ddldbHead.DataBind()
            ddldbHead.Items.Insert(0, New ListItem("Select Head of Account"))
            ddldbGL.DataSource = dt
            ddldbGL.DataTextField = "ATBU_Description"
            ddldbGL.DataValueField = "ATBU_ID"
            ddldbGL.DataBind()
            ddldbGL.Items.Insert(0, New ListItem("Select Description"))
            ddlCrHead.DataSource = dt
            ddlCrHead.DataTextField = "ATBU_Description"
            ddlCrHead.DataValueField = "ATBU_ID"
            ddlCrHead.DataBind()
            ddlCrHead.Items.Insert(0, New ListItem("Select Head of Account"))
            ddlCrGL.DataSource = dt
            ddlCrGL.DataTextField = "ATBU_Description"
            ddlCrGL.DataValueField = "ATBU_ID"
            ddlCrGL.DataBind()
            ddlCrGL.Items.Insert(0, New ListItem("Select Description"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindHeadofAccounts")
            'Throw
        End Try
    End Sub
    Private Sub LoadSubGL()
        Try
            ddlCrSubGL.DataSource = objJE.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
            ddlCrSubGL.DataTextField = "GlDesc"
            ddlCrSubGL.DataValueField = "gl_Id"
            ddlCrSubGL.DataBind()
            ddlCrSubGL.Items.Insert(0, "Select SubGL Code")

            ddldbSubGL.DataSource = objJE.LoadSubGLDetails(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)
            ddldbSubGL.DataTextField = "GlDesc"
            ddldbSubGL.DataValueField = "gl_Id"
            ddldbSubGL.DataBind()
            ddldbSubGL.Items.Insert(0, "Select SubGL Code")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSubGL")
            'Throw
        End Try
    End Sub
    Private Sub LoadExistingJEs(ByVal id As Integer, ByVal sCustomerAudit As String, ByVal iBranch As Integer)
        Try
            ddlExistJE.DataSource = objJE.LoadExistingVoucherNos(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sCustomerAudit, id, iBranch)
            ddlExistJE.DataTextField = "Acc_JE_TransactionNo"
            ddlExistJE.DataValueField = "Acc_JE_ID"
            ddlExistJE.DataBind()
            ddlExistJE.Items.Insert(0, "Existing JE Voucher")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingJEs")
            'Throw
        End Try
    End Sub

    Private Sub LoadExistingJE()
        Try
            ddlExistJE.DataSource = objJE.LoadExistingVoucherNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, sCustomerAudit)
            ddlExistJE.DataTextField = "Acc_JE_TransactionNo"
            ddlExistJE.DataValueField = "Acc_JE_ID"
            ddlExistJE.DataBind()
            ddlExistJE.Items.Insert(0, "Existing JE Voucher")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingJE")
            'Throw
        End Try
    End Sub
    'Private Sub LoadBillType()
    '    Try
    '        ddlBillType.DataSource = objJE.LoadBillType(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlBillType.DataTextField = "CMM_Desc"
    '        ddlBillType.DataValueField = "CMM_ID"
    '        ddlBillType.DataBind()
    '        ddlBillType.Items.Insert(0, "Select Bill Type")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBillType")
    '        'Throw
    '    End Try
    'End Sub
    'Private Sub LoadBillType()
    '    Try
    '        ddlBillType.Items.Insert(0, "Select JE Type")
    '        ddlBillType.Items.Insert(1, "Payment")
    '        ddlBillType.Items.Insert(2, "Receipt")
    '        ddlBillType.Items.Insert(3, "Pettty Cash")
    '        ddlBillType.Items.Insert(4, "Purchase")
    '        ddlBillType.Items.Insert(5, "Sales")
    '        ddlBillType.Items.Insert(6, "Others")
    '        ddlBillType.SelectedIndex = 0
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub

    'Private Sub LoadBillType()
    '    Try
    '        ddlBillType.DataSource = objJE.LoadBillType(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlBillType.DataTextField = "CMM_Desc"
    '        ddlBillType.DataValueField = "CMM_ID"
    '        ddlBillType.DataBind()
    '        ddlBillType.Items.Insert(0, "Select Bill Type")
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadBillType")
    '        'Throw
    '    End Try
    'End Sub
    Private Sub LoadParty()
        Try
            ddlParty.DataSource = objJE.LoadParty(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "ACM_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer/Party")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadParty")
            'Throw
        End Try
    End Sub
    Private Sub LoadCustomers()
        Try
            ddlParty.DataSource = objJE.LoadCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlParty.DataTextField = "Name"
            ddlParty.DataValueField = "CUST_ID"
            ddlParty.DataBind()
            ddlParty.Items.Insert(0, "Select Customer/Party")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomers")
            'Throw
        End Try
    End Sub
    Private Sub LoadParty(ByVal iType As Integer)
        Try
            If iType = 3 Then
                ddlParty.DataSource = objJE.LoadAllGLCodes(sSession.AccessCode, sSession.AccessCodeID)
                ddlParty.DataTextField = "GlDesc"
                ddlParty.DataValueField = "gl_Id"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Party")
            ElseIf iType = 1 Then
                ddlParty.DataSource = objJE.LoadParty(sSession.AccessCode, sSession.AccessCodeID, iType)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "ACM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Party")
            ElseIf iType = 2 Then
                ddlParty.DataSource = objJE.LoadParty(sSession.AccessCode, sSession.AccessCodeID, iType)
                ddlParty.DataTextField = "Name"
                ddlParty.DataValueField = "ACM_ID"
                ddlParty.DataBind()
                ddlParty.Items.Insert(0, "Select Customer/Party")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadParty")
            'Throw
        End Try
    End Sub

    Private Sub ddldbHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddldbHead.SelectedIndex > 0 Then
                'ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbHead.SelectedIndex)
                'ddldbGL.DataTextField = "GlDesc"
                'ddldbGL.DataValueField = "gl_Id"
                'ddldbGL.DataBind()
                'ddldbGL.Items.Insert(0, "Select GL Code")
                ddldbGL.SelectedValue = ddldbHead.SelectedValue
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddldbGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddldbGL.SelectedIndex > 0 Then
                'ddldbsUbGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddldbGL.SelectedValue)
                'ddldbsUbGL.DataTextField = "GlDesc"
                'ddldbsUbGL.DataValueField = "gl_Id"
                'ddldbsUbGL.DataBind()
                'ddldbsUbGL.Items.Insert(0, "Select SubGL Code")

                dt = objJE.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddldbHead.SelectedIndex, ddldbGL.SelectedValue, ddlParty.SelectedValue)
                ddldbSubGL.DataSource = dt
                ddldbSubGL.DataTextField = "CC_GLDesc"
                ddldbSubGL.DataValueField = "CC_GL"
                ddldbSubGL.DataBind()
                ddldbSubGL.Items.Insert(0, "Select SubGL Code")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbGL_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrHead_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrHead.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrHead.SelectedIndex > 0 Then
                'ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrHead.SelectedIndex)
                'ddlCrGL.DataTextField = "GlDesc"
                'ddlCrGL.DataValueField = "gl_Id"
                'ddlCrGL.DataBind()
                'ddlCrGL.Items.Insert(0, "Select GL Code")
                'If ddlParty.SelectedIndex > 0 Then
                '    dt = objJE.LoadGL(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlCrHead.SelectedIndex, ddlParty.SelectedValue)
                '    If dt.Rows.Count > 0 Then
                '        ddlCrGL.DataSource = dt
                '        ddlCrGL.DataTextField = "CC_GLDesc"
                '        ddlCrGL.DataValueField = "CC_GL"
                '        ddlCrGL.DataBind()
                '        ddlCrGL.Items.Insert(0, "Select GL Code")
                '    End If
                'Else
                '    lblError.Text = "Select Customer"
                '    Exit Sub
                'End If
                ddlCrGL.SelectedValue = ddlCrHead.SelectedValue
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrHead_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub ddlCrGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrGL.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If ddlCrGL.SelectedIndex > 0 Then
                'ddlCrSubGL.DataSource = objJE.LoadSubGLCodes(sSession.AccessCode, sSession.AccessCodeID, ddlCrGL.SelectedValue)
                'ddlCrSubGL.DataTextField = "GlDesc"
                'ddlCrSubGL.DataValueField = "gl_Id"
                'ddlCrSubGL.DataBind()
                'ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
                dt = objJE.LoadSubGL(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCrHead.SelectedIndex, ddlCrGL.SelectedValue, ddlParty.SelectedValue)
                ddlCrSubGL.DataSource = dt
                ddlCrSubGL.DataTextField = "CC_GLDesc"
                ddlCrSubGL.DataValueField = "CC_GL"
                ddlCrSubGL.DataBind()
                ddlCrSubGL.Items.Insert(0, "Select SubGL Code")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrGL_SelectedIndexChanged")
        End Try
    End Sub
    'Private Sub ddldbsUbGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbsUbGL.SelectedIndexChanged
    '    Dim iHead As Integer
    '    Try
    '        lblError.Text = ""
    '        If ddldbsUbGL.SelectedIndex > 0 Then
    '            'iHead = objJE.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddldbsUbGL.SelectedValue)
    '            'ddldbGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
    '            'ddldbGL.DataTextField = "GlDesc"
    '            'ddldbGL.DataValueField = "gl_Id"
    '            'ddldbGL.DataBind()
    '            'ddldbGL.Items.Insert(0, "Select GL Code")
    '            'ddldbGL.SelectedValue = objJE.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddldbSubGL.SelectedValue)
    '            'ddldbHead.SelectedIndex = iHead

    '            iHead = objJE.GetAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddldbSubGL.SelectedValue)
    '            ddldbGL.DataSource = objJE.LoadGL(sSession.AccessCode, sSession.AccessCodeID, iHead, ddlParty.SelectedValue)
    '            ddldbGL.DataTextField = "CC_GLDesc"
    '            ddldbGL.DataValueField = "CC_GL"
    '            ddldbGL.DataBind()
    '            ddldbGL.Items.Insert(0, "Select GL Code")
    '            ddldbGL.SelectedValue = objJE.GetCOAParent(sSession.AccessCode, sSession.AccessCodeID, ddldbSubGL.SelectedValue)
    '            ddldbHead.SelectedIndex = iHead
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbsUbGL_SelectedIndexChanged")
    '    End Try
    'End Sub
    'Private Sub ddlCrSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrSubGL.SelectedIndexChanged
    '    Dim iHead As Integer
    '    Try
    '        lblError.Text = ""
    '        If ddlCrSubGL.SelectedIndex > 0 Then
    '            iHead = objJE.GetChartOfAccountHead(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
    '            ddlCrGL.DataSource = objJE.LoadGLCodes(sSession.AccessCode, sSession.AccessCodeID, iHead)
    '            ddlCrGL.DataTextField = "GlDesc"
    '            ddlCrGL.DataValueField = "gl_Id"
    '            ddlCrGL.DataBind()
    '            ddlCrGL.Items.Insert(0, "Select GL Code")

    '            ddlCrGL.SelectedValue = objJE.GetParent(sSession.AccessCode, sSession.AccessCodeID, ddlCrSubGL.SelectedValue)
    '            ddlCrHead.SelectedIndex = iHead
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrSubGL_SelectedIndexChanged")
    '    End Try
    'End Sub
    'Private Sub ddlCustomerParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerParty.SelectedIndexChanged
    '    Try
    '        lblError.Text = ""
    '        If ddlCustomerParty.SelectedIndex = 1 Then
    '            LoadCustomers()
    '            lblParty.Text = "* Customer"
    '        ElseIf ddlCustomerParty.SelectedIndex = 2 Then
    '            lblParty.Text = "* General Ledger"
    '            LoadParty(3)
    '        Else ddlCustomerParty.SelectedIndex = 0
    '            ddlParty.Items.Clear()
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSearch_Click")
    '    End Try
    'End Sub
    Private Sub ddlExistJE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistJE.SelectedIndexChanged
        Try
            lblError.Text = ""
            If ddlExistJE.SelectedIndex > 0 Then
                imgbtnHistory.Visible = True
                BindTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue)
            Else
                'ClearAll()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistJE_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub BindCustomerAuditorDetails()
        Dim dt As New DataTable, dtUserDetails As New DataTable
        Try
            dtUserDetails = objJE.LoadCustomerEmpList(sSession.AccessCode, sSession.AccessCodeID)
            If dtUserDetails.Rows.Count > 0 Then
                Dim DVUserDetails As New DataView(dtUserDetails)
                DVUserDetails.RowFilter = "usr_Id='" & sSession.UserID & "'"
                DVUserDetails.Sort = "usr_Id ASC"
                dtUserDetails = DVUserDetails.ToTable
                If dtUserDetails.Rows.Count = 0 Then
                    imgbtnApprove.Visible = False
                Else
                    sCustomerAudit = "Customer"
                End If
            End If

            dt = objJE.GetAuditEmpList(sSession.AccessCode, sSession.AccessCodeID)
            If dt.Rows.Count > 0 Then
                Dim dtview As New DataView(dt)
                dtview.RowFilter = "usr_Id='" & sSession.UserID & "'"
                dtview.Sort = "usr_Id ASC"
                dt = dtview.ToTable
                If dt.Rows.Count = 0 Then
                    imgbtnApprove.Visible = False
                Else
                    sCustomerAudit = "Auditor"
                End If
            End If

            'If dt.Rows.Count = 0 And dtUserDetails.Rows.Count = 0 Then
            '    imgbtnAdd.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnApprove.Visible = False
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomerAuditorDetails")
            'Throw
        End Try
    End Sub
    Public Sub BindTransactionDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iJEPKID As Integer)
        Dim dt As New DataTable, dtGridDetails As New DataTable, dtJEHistory As New DataTable
        Try
            lblError.Text = "" : txtComments.Text = ""
            dt = objJE.GetPaymentTypeDetails(sAC, iACID, iJEPKID)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0)("Acc_JE_TransactionNo").ToString()) = False Then
                    txtTransactionNo.Text = dt.Rows(0)("Acc_JE_TransactionNo").ToString()
                Else
                    txtTransactionNo.Text = ""
                End If

                'If IsDBNull(dt.Rows(0)("Acc_JE_Location").ToString()) = False Then
                '    ddlCustomerParty.SelectedIndex = dt.Rows(0)("Acc_JE_Location").ToString()
                'Else
                '    ddlCustomerParty.SelectedIndex = 0
                'End If

                If IsDBNull(dt.Rows(0)("Acc_JE_Party").ToString()) = False Then
                    ddlParty.SelectedValue = dt.Rows(0)("Acc_JE_Party").ToString()
                Else
                    ddlParty.SelectedIndex = 0
                End If

                LoadBillType()
                If IsDBNull(dt.Rows(0)("Acc_JE_BillType").ToString()) = False Then
                    If dt.Rows(0)("Acc_JE_BillType") <> 0 Then
                        ddlBillType.SelectedValue = dt.Rows(0)("Acc_JE_BillType").ToString()
                    Else
                        ddlBillType.SelectedIndex = 0
                    End If
                Else
                    ddlBillType.SelectedIndex = 0
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_BillNo").ToString()) = False Then
                    txtBillNo.Text = dt.Rows(0)("Acc_JE_BillNo").ToString()
                Else
                    txtBillNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_BillDate").ToString()) = False Then
                    If dt.Rows(0)("Acc_JE_BillDate") = "01/01/1900" Then
                        txtBillDate.Text = ""
                    Else
                        txtBillDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_JE_BillDate").ToString(), "D")
                    End If
                Else
                    txtBillDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_BillAmount").ToString()) = False Then
                    If dt.Rows(0)("Acc_JE_BillAmount") = "0.00" Then
                        txtBillAmount.Text = ""
                    Else
                        txtBillAmount.Text = dt.Rows(0)("Acc_JE_BillAmount")
                    End If
                Else
                    txtBillAmount.Text = ""
                End If


                If IsDBNull(dt.Rows(0)("Acc_JE_AdvanceAmount").ToString()) = False Then
                    If dt.Rows(0)("Acc_JE_AdvanceAmount") <> "0.00" Then
                        txtNetAmount.Text = Convert.ToDecimal(txtBillAmount.Text - dt.Rows(0)("Acc_JE_AdvanceAmount").ToString())
                    Else
                        txtNetAmount.Text = ""
                    End If
                Else
                    txtNetAmount.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ACC_JE_ChequeNo").ToString()) = False Then
                    txtChequeNo.Text = dt.Rows(0)("ACC_JE_ChequeNo").ToString()
                Else
                    txtChequeNo.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_ChequeDate").ToString()) = False Then
                    If dt.Rows(0)("Acc_JE_ChequeDate") <> "01/01/1900" Then
                        txtChequeDate.Text = objGen.FormatDtForRDBMS(dt.Rows(0)("Acc_JE_ChequeDate").ToString(), "D")
                    Else
                        txtChequeDate.Text = ""
                    End If
                Else
                    txtChequeDate.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("Acc_JE_IFSCCode").ToString()) = False Then
                    txtIFSC.Text = dt.Rows(0)("Acc_JE_IFSCCode").ToString()
                Else
                    txtIFSC.Text = ""
                End If

                If IsDBNull(dt.Rows(0)("ACC_JE_BankName").ToString()) = False Then
                    txtBankName.Text = dt.Rows(0)("ACC_JE_BankName").ToString()
                Else
                    txtBankName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("Acc_JE_BranchName").ToString()) = False Then
                    txtBranchName.Text = dt.Rows(0)("Acc_JE_BranchName").ToString()
                Else
                    txtBranchName.Text = ""
                End If
                If IsDBNull(dt.Rows(0)("Acc_JE_Comnments").ToString()) = False Then
                    txtComments.Text = dt.Rows(0)("Acc_JE_Comnments").ToString()
                Else
                    txtComments.Text = ""
                End If
                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False : imgbtnApprove.Visible = False

                If dt.Rows(0)("Acc_JE_Status").ToString() = "S" Then
                    lblStatus.Text = "Completed"
                Else
                    If dt.Rows(0)("Acc_JE_Status").ToString() = "W" Then ' vijaylakshmi modified 24-01-2020  changed status 'WC' to 'W'
                        lblStatus.Text = "Waiting for Approval"
                        imgbtnAdd.Visible = True : imgbtnUpdate.Visible = True : imgbtnApprove.Visible = True
                        dgJEDetails.Enabled = True
                    ElseIf dt.Rows(0)("Acc_JE_Status").ToString() = "C" Then
                        lblStatus.Text = "Waiting for Approval(Auditor)"
                        imgbtnAdd.Visible = True
                        imgbtnUpdate.Visible = True : imgbtnApprove.Visible = True
                        imgbtnSave.Visible = False : dgJEDetails.Enabled = True
                    ElseIf dt.Rows(0)("Acc_JE_Status").ToString() = "A" Then
                        lblStatus.Text = "Approve"
                        imgbtnAdd.Visible = True : imgbtnUpdate.Visible = False : imgbtnApprove.Visible = False
                        imgbtnSave.Visible = False : dgJEDetails.Enabled = False
                    End If

                    'ElseIf sCustomerAudit = "Auditor" Then
                    '    If dt.Rows(0)("Acc_JE_Sta4tus").ToString() = "W" Then ' vijaylakshmi modified 24-01-2020  changed status 'WA' to 'W'
                    '        lblStatus.Text = "Waiting for Approval(Auditor)"
                    '        imgbtnAdd.Visible = True : imgbtnUpdate.Visible = True : imgbtnApprove.Visible = True
                    '    ElseIf dt.Rows(0)("Acc_JE_Status").ToString() = "A" Then
                    '        lblStatus.Text = "Waiting for Approval(Customer)"
                    '        imgbtnAdd.Visible = True
                    '    ElseIf dt.Rows(0)("Acc_JE_Status").ToString() = "C" Then
                    '        lblStatus.Text = "Waiting for Approval(Auditor)"
                    '        imgbtnAdd.Visible = True : imgbtnUpdate.Visible = True : imgbtnApprove.Visible = True
                    '    End If
                End If
            End If
            If ddlBranch.SelectedIndex > 0 Then
                dtGridDetails = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, ddlExistJE.SelectedValue, ddlBranch.SelectedValue)
            Else
                dtGridDetails = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, ddlExistJE.SelectedValue, 0)
            End If
            dgJEDetails.DataSource = dtGridDetails
            dgJEDetails.DataBind()

            Session("DataTable") = dtGridDetails

            'Journal Entry History
            dtJEHistory = objJE.GetJEHistory(sSession.AccessCode, sSession.AccessCodeID, iJEPKID)
            If dtJEHistory.Rows.Count > 0 Then
                dgHistory.DataSource = dtJEHistory
                dgHistory.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindTransactionDetails")
            'Throw
        End Try
    End Sub
    Public Sub ClearAll()
        Try
            lblError.Text = "" : txtTransactionNo.Text = "" : txtBillNo.Text = "" : txtBillDate.Text = "" : txtBillAmount.Text = ""
            txtAdvancePayment.Text = "" : txtBalanceAmount.Text = "" : txtNetAmount.Text = "" : lblStatus.Text = "" : txtComments.Text = "" : txtNarration.Text = ""
            ddlExistJE.SelectedIndex = 0 : ddlBillType.SelectedIndex = 0

            'Debit
            ddldbHead.SelectedIndex = 0 : ddldbGL.Items.Clear() : ddldbSubGL.Items.Clear() : txtDebitAmount.Text = ""
            'Credit
            ddlCrHead.SelectedIndex = 0 : ddlCrGL.Items.Clear() : ddlCrSubGL.Items.Clear() : txtCreditAmount.Text = ""

            imgbtnSave.Visible = True : imgbtnUpdate.Visible = False : imgbtnApprove.Visible = False : imgbtnHistory.Visible = False
            dgJEDetails.DataSource = Nothing
            dgJEDetails.DataBind()
            LoadSubGL()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAll")
        End Try
    End Sub
    Private Function CheckDebitAndCredit() As Integer
        Dim i As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Try
            For i = 0 To dgJEDetails.Items.Count - 1
                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    dDebit = dDebit + Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    dCredit = dCredit + Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                End If
            Next

            If dDebit <> dCredit Then
                Return 1  ' Debit and Credit amount not Matched
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Dim dt As New DataTable
        Try
            'ClearAll()
            'txtTransactionNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID)
            Session("DataTable") = Nothing
            btnAddCredit.Visible = True : btnAddDebit.Visible = True
            'BindCustomerAuditorDetails()
            Response.Redirect(String.Format("~/FIN Statement/JournalEntryDetails.aspx"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim iPaymentType As Integer = 0, iJEID As Integer = 0
        Dim dDebit As Double = 0, dCredit As Double = 0
        Dim iRet As Integer = 0
        Dim dt As New DataTable
        Dim ArrJE() As String, ArrTD() As String
        Dim iTranStat As Integer = 0, dTransAMt As Double = 0.0
        Try
            lblError.Text = ""
            If ddlParty.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlBillType.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Transaction Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            If ddlBranch.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            If dgJEDetails.Items.Count = 0 Then
                lblCustomerValidationMsg.Text = "Add Debit and Credit Details."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If
            iRet = CheckDebitAndCredit()

            If iRet = 1 Then
                lblCustomerValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 2 Then
                lblCustomerValidationMsg.Text = "Amount Not Matched with Advance Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            ElseIf iRet = 3 Then
                lblCustomerValidationMsg.Text = "Amount Not Matched with Payment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlExistJE.SelectedIndex > 0 Then
                objJE.iAcc_JE_ID = ddlExistJE.SelectedValue
            Else
                objJE.iAcc_JE_ID = 0
            End If

            objJE.sAJTB_TranscNo = txtTransactionNo.Text


            objJE.iAcc_JE_Location = 0

            If ddlParty.SelectedIndex > 0 Then
                objJE.iAcc_JE_Party = ddlParty.SelectedValue
            Else
                objJE.iAcc_JE_Party = 0
            End If

            If ddlBillType.SelectedIndex > 0 Then
                objJE.iAcc_JE_BillType = ddlBillType.SelectedValue
            Else
                objJE.iAcc_JE_BillType = 0
            End If

            If txtBillNo.Text <> "" Then
                objJE.sAcc_JE_BillNo = txtBillNo.Text
            Else
                objJE.sAcc_JE_BillNo = ""
            End If

            If txtBillDate.Text <> "" Then
                objJE.dAcc_JE_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Else
                objJE.dAcc_JE_BillDate = "01/01/1900"
            End If

            If txtBillAmount.Text <> "" Then
                objJE.dAcc_JE_BillAmount = txtBillAmount.Text
            Else
                objJE.dAcc_JE_BillAmount = "0.00"
            End If

            objJE.dAcc_JE_AdvanceAmount = "0.00" : objJE.sAcc_JE_AdvanceNaration = "" : objJE.dAcc_JE_BalanceAmount = "0.00" : objJE.dAcc_JE_NetAmount = "0.00"
            objJE.sAcc_JE_PaymentNarration = "" : objJE.sAcc_JE_ChequeNo = "" : objJE.dAcc_JE_ChequeDate = "01/01/1900"
            objJE.sAcc_JE_IFSCCode = "" : objJE.sAcc_JE_BankName = "" : objJE.sAcc_JE_BranchName = ""

            objJE.iAcc_JE_YearID = ddlFinancialYear.SelectedValue
            objJE.iAcc_JE_CompID = sSession.AccessCodeID

            If lblStatus.Text = "Approve" Then
                objJE.sAcc_JE_Status = "A"
            Else
                If sCustomerAudit = "Customer" Then
                    objJE.sAcc_JE_Status = "W"
                Else
                    objJE.sAcc_JE_Status = "W"
                End If
            End If
            objJE.iAcc_JE_CreatedBy = sSession.UserID
            objJE.sAcc_JE_Operation = "C"
            objJE.sAcc_JE_IPAddress = sSession.IPAddress
            objJE.dAcc_JE_BillCreatedDate = "01/01/1900"
            objJE.iacc_JE_BranchId = ddlBranch.SelectedValue
            objJE.sAcc_JE_Comments = objGen.SafeSQL(txtComments.Text.Trim)
            ArrJE = objJE.SaveJournalEntryMaster(sSession.AccessCode, objJE)
            iJEID = ArrJE(1)

            'objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iJEID, objGen.SafeSQL(txtComments.Text.Trim), "Saved(" & sCustomerAudit & ")", sSession.IPAddress)
            'objJE.DeletePaymentDetails(sSession.AccessCode, sSession.AccessCodeID, iJEID, "BILLID")

            For i = 0 To dgJEDetails.Items.Count - 1
                dTransAMt = 0
                If Val(dgJEDetails.Items(i).Cells(15).Text) = 0 Then
                    objJE.iAJTB_ID = 0
                Else
                    objJE.iAJTB_ID = Val(dgJEDetails.Items(i).Cells(15).Text)
                End If
                objJE.iAJTB_CustId = ddlParty.SelectedValue
                objJE.iAJTB_MAsID = iJEID
                'objJE.iATD_OrgType = objCOA.GetOrgTypeID(sSession.AccessCode, sSession.AccessCodeID, "ORG", ddlParty.SelectedValue)
                'objJE.dATD_TransactionDate = Date.Today
                'objJE.iATD_TrType = 4
                'objJE.iATD_BillId = iJEID
                'objJE.iATD_PaymentType = iPaymentType

                If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
                    objJE.iAJTB_Deschead = dgJEDetails.Items(i).Cells(1).Text
                Else
                    objJE.iAJTB_Deschead = 0
                End If

                If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
                    objJE.iAJTB_Desc = dgJEDetails.Items(i).Cells(2).Text
                Else
                    objJE.iAJTB_Desc = 0
                End If


                If (IsDBNull(dgJEDetails.Items(i).Cells(8).Text) = False) And (dgJEDetails.Items(i).Cells(8).Text <> "&nbsp;") Then
                    objJE.sAJTB_DescName = dgJEDetails.Items(i).Cells(8).Text
                Else
                    objJE.sAJTB_DescName = ""
                End If

                'If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
                '    objJE.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
                'Else
                '    objJE.iATD_SubGL = 0
                'End If
                Dim dTransDbAmt, dTransCrAmt As Double
                If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
                    objJE.dAJTB_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                    iTranStat = 0
                    dTransAMt = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                    dTransDbAmt = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
                Else
                    objJE.dAJTB_Debit = 0
                End If
                If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
                    objJE.dAJTB_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                    dTransAMt = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                    If Val(dTransAMt) <> 0 Then
                        iTranStat = 1
                        dTransCrAmt = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
                    Else
                        dTransCrAmt = 0.00
                    End If
                Else
                    objJE.dAJTB_Credit = 0
                End If
                objJE.iAJTB_CreatedBy = sSession.UserID
                objJE.iAJTB_UpdatedBy = sSession.UserID
                If lblStatus.Text = "Approve" Then
                    objJE.sAJTB_Status = "A"
                Else
                    objJE.sAJTB_Status = "W"
                End If

                objJE.iAJTB_YearID = ddlFinancialYear.SelectedValue
                objJE.iAJTB_CompID = sSession.AccessCodeID
                'objJE.sATD_Operation = "U"
                objJE.sAJTB_IPAddress = sSession.IPAddress
                objJE.iAJTB_BillType = ddlBillType.SelectedValue
                objJE.iAJTB_BranchId = ddlBranch.SelectedValue
                ArrTD = objJE.SaveTransactionDetails(sSession.AccessCode, objJE)
                If lblStatus.Text = "Approve" Then
                    objJE.UpdateJeDet(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objJE.iAJTB_Deschead, ddlParty.SelectedValue, iTranStat, dTransAMt, ddlBranch.SelectedValue, dTransDbAmt, dTransCrAmt)
                End If
                iJEPKID = ArrTD(1)
            Next
            If ArrTD(0) = 3 Then
                lblCustomerValidationMsg.Text = "Successfully Saved and Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            ElseIf ArrTD(0) = 2 Then
                lblCustomerValidationMsg.Text = "Successfully Updated and Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            End If


            ddlParty_SelectedIndexChanged(sender, e)
            LoadExistingJEs(ddlParty.SelectedValue, sCustomerAudit, ddlBranch.SelectedValue)
            ddlExistJE.SelectedValue = iJEID
            ddlExistJE_SelectedIndexChanged(sender, e)

            ddldbHead.SelectedIndex = 0 : ddldbGL.SelectedIndex = 0 : ddldbSubGL.Items.Clear() : txtDebitAmount.Text = ""
            ddlCrHead.SelectedIndex = 0 : ddlCrGL.SelectedIndex = 0 : ddlCrSubGL.Items.Clear() : txtCreditAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
    End Sub
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click")
        End Try
        'Dim iJEID As Integer = 0, iRet As Integer = 0, iJedetID As Integer = 0
        'Dim dDebit As Double = 0, dCredit As Double = 0
        'Dim dt As New DataTable
        'Dim ArrTD() As String
        'Try
        '    lblError.Text = ""
        '    If dgJEDetails.Items.Count = 0 Then
        '        lblCustomerValidationMsg.Text = "Add Debit and Credit Details."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
        '        Exit Sub
        '    End If
        '    iRet = CheckDebitAndCredit()

        '    If iRet = 1 Then
        '        lblCustomerValidationMsg.Text = "Debit Amount and Credit Amount Not Matched."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
        '        Exit Sub
        '    ElseIf iRet = 2 Then
        '        lblCustomerValidationMsg.Text = "Amount Not Matched with Advance Payment."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
        '        Exit Sub
        '    ElseIf iRet = 3 Then
        '        lblCustomerValidationMsg.Text = "Amount Not Matched with Payment."
        '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
        '        Exit Sub
        '    End If

        '    objJE.iAcc_JE_ID = ddlExistJE.SelectedValue
        '    objJE.sAcc_JE_TransactionNo = txtTransactionNo.Text


        '    objJE.iAcc_JE_Location = 0

        '    If ddlParty.SelectedIndex > 0 Then
        '        objJE.iAcc_JE_Party = ddlParty.SelectedValue
        '    Else
        '        objJE.iAcc_JE_Party = 0
        '    End If

        '    If ddlBillType.SelectedIndex > 0 Then
        '        objJE.iAcc_JE_BillType = ddlBillType.SelectedValue
        '    Else
        '        objJE.iAcc_JE_BillType = 0
        '    End If

        '    If txtBillNo.Text <> "" Then
        '        objJE.sAcc_JE_BillNo = txtBillNo.Text
        '    Else
        '        objJE.sAcc_JE_BillNo = ""
        '    End If

        '    If txtBillDate.Text <> "" Then
        '        objJE.dAcc_JE_BillDate = Date.ParseExact(txtBillDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
        '    Else
        '        objJE.dAcc_JE_BillDate = "01/01/1900"
        '    End If
        '    objJE.sAJTB_TranscNo = txtTransactionNo.Text
        '    If txtBillAmount.Text <> "" Then
        '        objJE.dAcc_JE_BillAmount = txtBillAmount.Text
        '    Else
        '        objJE.dAcc_JE_BillAmount = "0.00"
        '    End If

        '    objJE.iAcc_JE_YearID = ddlFinancialYear.SelectedValue

        '    If sCustomerAudit = "Customer" Then
        '        objJE.sAcc_JE_Status = "W"
        '    Else
        '        objJE.sAcc_JE_Status = "W"
        '    End If

        '    objJE.iAcc_JE_CreatedBy = sSession.UserID
        '    objJE.sAcc_JE_Operation = "U"
        '    objJE.sAcc_JE_IPAddress = sSession.IPAddress

        '    objJE.sAcc_JE_ChequeNo = ""
        '    objJE.sAcc_JE_IFSCCode = "" : objJE.sAcc_JE_BankName = "" : objJE.sAcc_JE_BranchName = ""

        '    iJEID = objJE.UpdatePaymentMaster(sSession.AccessCode, sSession.AccessCodeID, 0, objJE)
        '    objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iJEID, objGen.SafeSQL(txtComments.Text.Trim), "Updated(" & sCustomerAudit & ")", sSession.IPAddress)
        '    objJE.DeletePaymentDetails(sSession.AccessCode, sSession.AccessCodeID, iJEID, "BILLID")

        '    For i = 0 To dgJEDetails.Items.Count - 1

        '        objJE.iAJTB_ID = iJedetID
        '        objJE.iATD_TrType = 4
        '        objJE.iATD_BillId = iJEID
        '        objJE.iATD_PaymentType = 0

        '        If (IsDBNull(dgJEDetails.Items(i).Cells(1).Text) = False) And (dgJEDetails.Items(i).Cells(1).Text <> "&nbsp;") Then
        '            objJE.iATD_Head = dgJEDetails.Items(i).Cells(1).Text
        '        Else
        '            objJE.iATD_Head = 0
        '        End If

        '        If (IsDBNull(dgJEDetails.Items(i).Cells(2).Text) = False) And (dgJEDetails.Items(i).Cells(2).Text <> "&nbsp;") Then
        '            objJE.iATD_GL = dgJEDetails.Items(i).Cells(2).Text
        '        Else
        '            objJE.iATD_GL = 0
        '        End If

        '        If (IsDBNull(dgJEDetails.Items(i).Cells(3).Text) = False) And (dgJEDetails.Items(i).Cells(3).Text <> "&nbsp;") Then
        '            objJE.iATD_SubGL = dgJEDetails.Items(i).Cells(3).Text
        '        Else
        '            objJE.iATD_SubGL = 0
        '        End If

        '        If (IsDBNull(dgJEDetails.Items(i).Cells(12).Text) = False) And (dgJEDetails.Items(i).Cells(12).Text <> "&nbsp;") Then
        '            objJE.dATD_Debit = Convert.ToDouble(dgJEDetails.Items(i).Cells(12).Text)
        '        Else
        '            objJE.dATD_Debit = 0
        '        End If

        '        If (IsDBNull(dgJEDetails.Items(i).Cells(13).Text) = False) And (dgJEDetails.Items(i).Cells(13).Text <> "&nbsp;") Then
        '            objJE.dATD_Credit = Convert.ToDouble(dgJEDetails.Items(i).Cells(13).Text)
        '        Else
        '            objJE.dATD_Credit = 0
        '        End If

        '        objJE.iATD_CreatedBy = sSession.UserID
        '        objJE.iATD_UpdatedBy = sSession.UserID
        '        objJE.sATD_Status = "A"
        '        objJE.iATD_YearID = ddlFinancialYear.SelectedValue
        '        objJE.iATD_CompID = sSession.AccessCodeID
        '        objJE.sATD_Operation = "U"
        '        objJE.sATD_IPAddress = sSession.IPAddress
        '        ArrTD = objJE.SaveTransactionDetails(sSession.AccessCode, objJE)
        '    Next

        '    lblCustomerValidationMsg.Text = "Successfully Updated."
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)

        '    LoadExistingJE()
        '    ddlExistJE.SelectedValue = iJEID
        '    ddlExistJE_SelectedIndexChanged(sender, e)

        '    dt = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, ddlExistJE.SelectedValue)
        '    dgJEDetails.DataSource = dt
        '    dgJEDetails.DataBind()
        '    Session("DataTable") = dt

        '    ddldbHead.SelectedIndex = 0 : ddldbGL.Items.Clear() : ddldbSubGL.Items.Clear() : txtDebitAmount.Text = ""
        '    ddlCrHead.SelectedIndex = 0 : ddlCrGL.Items.Clear() : ddlCrSubGL.Items.Clear() : txtCreditAmount.Text = ""
        'Catch ex As Exception
        '    lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
        '    Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click")
        'End Try
    End Sub
    Private Sub imgbtnApprove_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnApprove.Click
        Dim sStatus As String = ""
        Try
            'sStatus = objJE.GetJEStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue)
            'objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, sStatus, sSession.UserID, sSession.IPAddress, sCustomerAudit)
            lblStatus.Text = "Approve"
            imgbtnSave_Click(sender, e)

            lblCustomerValidationMsg.Text = "Successfully Approved by." & sSession.UserFullName
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            'If sCustomerAudit = "Customer" Then
            '    If sStatus = "WC" Then
            '        objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "WC", sSession.UserID, sSession.IPAddress, sCustomerAudit)
            '        objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlExistJE.SelectedValue, objGen.SafeSQL(txtComments.Text.Trim), "Approved(Customer)", sSession.IPAddress)
            '        lblCustomerValidationMsg.Text = "Successfully Approved by Customer."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    ElseIf sStatus = "A" Then
            '        objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "A", sSession.UserID, sSession.IPAddress, sCustomerAudit)
            '        objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlExistJE.SelectedValue, objGen.SafeSQL(txtComments.Text.Trim), "Approve(Customer)", sSession.IPAddress)
            '        lblCustomerValidationMsg.Text = "Successfully Approved by Customer."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    End If
            'End If

            'If sCustomerAudit = "Auditor" Then
            '    If sStatus = "WA" Then
            '        objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "WA", sSession.UserID, sSession.IPAddress, sCustomerAudit)
            '        objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlExistJE.SelectedValue, objGen.SafeSQL(txtComments.Text.Trim), "Approved(Auditor)", sSession.IPAddress)
            '        lblCustomerValidationMsg.Text = "Successfully Approved by Auditor."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    ElseIf sStatus = "C" Then
            '        objJE.UpdateJEMasterStatus(sSession.AccessCode, sSession.AccessCodeID, ddlExistJE.SelectedValue, "C", sSession.UserID, sSession.IPAddress, sCustomerAudit)
            '        objJE.SaveJEHistory(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlExistJE.SelectedValue, objGen.SafeSQL(txtComments.Text.Trim), "Approved(Auditor)", sSession.IPAddress)
            '        lblCustomerValidationMsg.Text = "Successfully Approved by Auditor."
            '        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    End If
            'End If
            ddlExistJE_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnApprove_Click")
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim sStatus As String = ""
        Dim oStatus As New Object
        Dim iStatusId As Integer = 0
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("JournalEntry.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click")
        End Try
    End Sub
    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Try
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSC.Text = "" : txtBankName.Text = "" : txtBranchName.Text = "" : lblError.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNew_Click")
        End Try
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            lblError.Text = ""
            If ddlExistJE.SelectedIndex > 0 Then
                objJE.iAcc_JE_ID = ddlExistJE.SelectedValue
                objJE.sAcc_JE_ChequeNo = txtChequeNo.Text
                objJE.dAcc_JE_ChequeDate = txtChequeDate.Text
                objJE.sAcc_JE_IFSCCode = txtIFSC.Text
                objJE.sAcc_JE_BankName = txtBankName.Text
                objJE.sAcc_JE_BranchName = txtBranchName.Text
                objJE.sAcc_JE_BranchName = txtBranchName.Text
                objJE.SaveChequeDetails(sSession.AccessCode, sSession.AccessCodeID, objJE)
            End If
            lblCustomerValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSave_Click")
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            txtChequeNo.Text = "" : txtChequeDate.Text = "" : txtIFSC.Text = "" : txtBankName.Text = "" : txtBranchName.Text = "" : lblError.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModal').modal('');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnClose_Click")
        End Try
    End Sub
    Private Sub btnAddDebit_Click(sender As Object, e As EventArgs) Handles btnAddDebit.Click
        Dim dt As New DataTable, dtsample As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Try
            lblError.Text = ""
            If ddldbHead.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Head of Accounts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                ddldbHead.Focus()
                Exit Sub
            End If
            If ddldbGL.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                ddldbGL.Focus()
                Exit Sub
            End If
            If txtDebitAmount.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Debit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                txtDebitAmount.Focus()
                Exit Sub
            End If

            dtMerge = Session("DataTable")
            If IsNothing(dtMerge) = True Then
                dtMerge = dt
            End If

            If dtMerge.Rows.Count > 0 Then
                If iJEPKID > 0 Then
                    Dim DVZRBADetails As New DataView(dtMerge)
                    DVZRBADetails.RowFilter = "detID<>'" & iJEPKID & "'"
                    dtMerge = DVZRBADetails.ToTable
                End If

                Dim dtview As New DataView(dtMerge)
                sArray = ddldbHead.SelectedItem.Text.Split("-")
                Dim Glhead As Integer = 0
                If dgJEDetails.Items.Count > 0 Then
                    For i = 0 To dgJEDetails.Items.Count - 1
                        Glhead = dgJEDetails.Items(i).Cells(1).Text
                        If ddldbHead.SelectedValue = Glhead Then
                            lblCustomerValidationMsg.Text = "Selected combination not allowed please change."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                            Exit Sub
                        End If
                    Next
                End If
                'If ddldbSubGL.SelectedIndex > 0 Then
                '    dtview.RowFilter = "GLCode='" & sArray(0) & "' And GLDescription='" & sArray(1) & "' And SubGL='" & ddldbSubGL.SelectedItem.Text & "'"
                'Else
                '    dtview.RowFilter = "GLCode='" & sArray(0) & "' And GLDescription='" & sArray(1) & "'"
                'End If
                If ddldbSubGL.SelectedIndex > 0 Then
                        dtview.RowFilter = "GLDescription='" & sArray(0) & "' And SubGL='" & ddldbSubGL.SelectedItem.Text & "'"
                    Else
                        dtview.RowFilter = "GLDescription='" & sArray(0) & "'"
                    End If

                    dtview.Sort = "GLCode ASC"
                    dtsample = dtview.ToTable

                    'If dtsample.Rows.Count > 0 Then
                    '    lblCustomerValidationMsg.Text = "This combination already exists."
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                    '    txtCreditAmount.Focus()
                    '    Exit Sub
                    'End If
                End If

                dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")
            dt.Columns.Add("detID")
            dRow = dt.NewRow

            If ddldbHead.SelectedIndex > 0 Then
                dRow("ID") = dtMerge.Rows.Count + 1
                dRow("detID") = 0
                dRow("HeadID") = ddldbHead.SelectedValue
            End If
            If iJEPKID <> 0 Then
                dRow("detID") = iJEPKID
            Else
                dRow("detID") = 0
            End If
            If ddldbGL.SelectedIndex > 0 Then
                dRow("GLID") = ddldbGL.SelectedValue
            End If

            If ddldbSubGL.SelectedIndex > 0 Then
                dRow("SubGLID") = ddldbSubGL.SelectedValue
            End If

            dRow("SrNo") = dtMerge.Rows.Count + 1

            If ddldbHead.SelectedIndex > 0 Then
                'sArray = ddldbGL.SelectedItem.Text.Split("-")
                dRow("GLCode") = ""
                dRow("GLDescription") = ddldbHead.SelectedItem.Text
            End If

            If ddldbSubGL.SelectedIndex > 0 Then
                'sArray = ddldbSubGL.SelectedItem.Text.Split("-")
                dRow("SubGL") = ""
                dRow("SubGLDescription") = ddldbSubGL.SelectedItem.Text
            End If

            If txtDebitAmount.Text <> "" Then
                dRow("Debit") = txtDebitAmount.Text
            End If
            dt.Rows.Add(dRow)

            Session("DataTable") = dt

            'dtMerge.Merge(dt)
            'dtMerge.AcceptChanges()

            dt.Merge(dtMerge)
            dt.AcceptChanges()

            dt.DefaultView.Sort = "SrNo"

            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    dt.Rows(j)("SrNo") = j + 1
                    dt.Rows(j)("ID") = j + 1
                Next
                dt.AcceptChanges()
            End If

            ' dgJEDetails.DataSource = dtMerge
            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()

            iDebitID = 1
            ddldbHead.SelectedIndex = 0 : ddldbSubGL.Items.Clear() : txtDebitAmount.Text = "" : iJEPKID = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDebit_Click")
        End Try
    End Sub
    Private Sub btnAddCredit_Click(sender As Object, e As EventArgs) Handles btnAddCredit.Click
        Dim dt As New DataTable, dtsample As New DataTable
        Dim dRow As DataRow
        Dim sArray As Array
        Dim Glhead As Integer = 0

        Try
            lblError.Text = ""
            If ddlCrHead.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Head of Accounts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                ddlCrHead.Focus()
                Exit Sub
            End If
            If ddlCrGL.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select General Ledger."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                ddlCrGL.Focus()
                Exit Sub
            End If
            If txtCreditAmount.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Credit Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                txtCreditAmount.Focus()
                Exit Sub
            End If
            If dgJEDetails.Items.Count > 0 Then
                For i = 0 To dgJEDetails.Items.Count - 1
                    Glhead = dgJEDetails.Items(i).Cells(1).Text
                    If ddlCrHead.SelectedValue = Glhead Then
                        lblCustomerValidationMsg.Text = "Selected combination not allowed"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                        Exit Sub
                    End If
                Next
            End If
            dtMerge = Session("DataTable")
            If IsNothing(dtMerge) = True Then
                dtMerge = dt
            End If

            If dtMerge.Rows.Count > 0 Then
                If iJEPKID > 0 Then
                    Dim DVZRBADetails As New DataView(dtMerge)
                    DVZRBADetails.RowFilter = "detID<>'" & iJEPKID & "'"
                    dtMerge = DVZRBADetails.ToTable
                End If

                Dim dtview As New DataView(dtMerge)
                sArray = ddlCrGL.SelectedItem.Text.Split("-")

                'If ddlCrSubGL.SelectedIndex > 0 Then
                '    dtview.RowFilter = "GLCode='" & sArray(0) & "' And GLDescription='" & sArray(1) & "' And SubGL='" & ddlCrSubGL.SelectedItem.Text & "'"
                'Else
                '    dtview.RowFilter = "GLCode='" & sArray(0) & "' And GLDescription='" & sArray(1) & "'"
                'End If

                If ddlCrSubGL.SelectedIndex > 0 Then
                    dtview.RowFilter = "GLDescription='" & sArray(0) & "' And SubGL='" & ddlCrSubGL.SelectedItem.Text & "'"
                Else
                    dtview.RowFilter = "GLDescription='" & sArray(0) & "'"
                End If

                dtview.Sort = "GLCode ASC"
                dtsample = dtview.ToTable

                'If dtsample.Rows.Count > 0 Then
                '    lblCustomerValidationMsg.Text = "This combination already exists."
                '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                '    txtCreditAmount.Focus()
                '    Exit Sub
                'End If
            End If

            dt.Columns.Add("SrNo")
            dt.Columns.Add("ID")
            dt.Columns.Add("HeadID")
            dt.Columns.Add("GLID")
            dt.Columns.Add("SubGLID")
            dt.Columns.Add("PaymentID")
            dt.Columns.Add("Type")
            dt.Columns.Add("GLCode")
            dt.Columns.Add("GLDescription")
            dt.Columns.Add("SubGL")
            dt.Columns.Add("SubGLDescription")
            dt.Columns.Add("OpeningBalance")
            dt.Columns.Add("Debit")
            dt.Columns.Add("Credit")
            dt.Columns.Add("Balance")
            dt.Columns.Add("detID")
            dRow = dt.NewRow

            If ddlCrHead.SelectedIndex > 0 Then
                dRow("ID") = dtMerge.Rows.Count + 1
                dRow("HeadID") = ddlCrHead.SelectedValue
            End If
            If iJEPKID <> 0 Then
                dRow("detID") = iJEPKID
            Else
                dRow("detID") = 0
            End If
            If ddlCrGL.SelectedIndex > 0 Then
                dRow("GLID") = ddlCrGL.SelectedValue
            End If

            If ddlCrSubGL.SelectedIndex > 0 Then
                dRow("SubGLID") = ddlCrSubGL.SelectedValue
            End If

            dRow("SrNo") = dtMerge.Rows.Count + 1

            If ddlCrGL.SelectedIndex > 0 Then
                'sArray = ddlCrGL.SelectedItem.Text.Split("-")
                dRow("GLCode") = ""
                dRow("GLDescription") = ddlCrGL.SelectedItem.Text
            End If

            If ddlCrSubGL.SelectedIndex > 0 Then
                'sArray = ddlCrSubGL.SelectedItem.Text.Split("-")
                dRow("SubGL") = ""
                dRow("SubGLDescription") = ddlCrSubGL.SelectedItem.Text
            End If

            If txtCreditAmount.Text <> "" Then
                dRow("Credit") = txtCreditAmount.Text
            End If
            dt.Rows.Add(dRow)

            Session("DataTable") = dt

            'dtMerge.Merge(dt)
            'dtMerge.AcceptChanges()

            dt.Merge(dtMerge)
            dt.AcceptChanges()

            dt.DefaultView.Sort = "SrNo"

            If dt.Rows.Count > 0 Then
                For j = 0 To dt.Rows.Count - 1
                    dt.Rows(j)("SrNo") = j + 1
                    dt.Rows(j)("ID") = j + 1
                Next
                dt.AcceptChanges()
            End If

            'dgJEDetails.DataSource = dtMerge

            dgJEDetails.DataSource = dt
            dgJEDetails.DataBind()

            iCreditID = 2
            ddlCrHead.SelectedIndex = 0 : ddlCrSubGL.Items.Clear() : txtCreditAmount.Text = "" : iJEPKID = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddCredit_Click")
        End Try
    End Sub
    Private Sub dgJEDetails_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgJEDetails.ItemCommand
        Dim dt As New DataTable, dtDetails As New DataTable
        Try
            ' imgbtnUpdate.Visible = False
            lblError.Text = ""
            lblJEdetId.Text = ""
            If e.CommandName = "Delete" Then
                ' imgbtnUpdate.Visible = True
                lblJEdetId = e.Item.FindControl("lblId")
                dt = Session("DataTable")
                Dim DVZRBADetails As New DataView(dt)
                DVZRBADetails.RowFilter = "ID<>'" & lblJEdetId.Text & "'"
                dt = DVZRBADetails.ToTable

                If dt.Rows.Count > 0 Then
                    For j = 0 To dt.Rows.Count - 1
                        dt.Rows(j)("SrNo") = j + 1
                    Next
                    dt.AcceptChanges()
                End If
                dgJEDetails.DataSource = dt
                dgJEDetails.DataBind()
                Session("DataTable") = dt
            End If


            If e.CommandName = "Edit" Then
                '  imgbtnUpdate.Visible = True
                ddldbHead.SelectedIndex = 0 : ddldbSubGL.Items.Clear() : txtDebitAmount.Text = "" : iJEPKID = 0
                ddlCrHead.SelectedIndex = 0 : ddlCrSubGL.Items.Clear() : txtCreditAmount.Text = ""
                dt = Session("DataTable")
                dtDetails = dt.Copy
                lblJEdetId = e.Item.FindControl("lblId")
                If lblJEdetId.Text = 0 Then
                    lblCustomerValidationMsg.Text = "Save the Journal entry to  modify record"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                    Exit Sub
                End If
                iJEPKID = Val(lblJEdetId.Text)
                Dim DVZRBADetails As New DataView(dtDetails)
                DVZRBADetails.RowFilter = "detID='" & iJEPKID & "'"
                DVZRBADetails.Sort = "GLDescription ASC"
                dtDetails = DVZRBADetails.ToTable
                If dtDetails.Rows.Count > 0 Then
                    For j = 0 To dtDetails.Rows.Count - 1
                        dtDetails.Rows(j)("SrNo") = j + 1
                    Next
                    dtDetails.AcceptChanges()

                    If IsDBNull(dtDetails.Rows(0).Item("Credit")) = False Then
                        If dtDetails.Rows(0).Item("Credit") <> "" Then
                            'BindHeadofAccounts()
                            ddlCrHead.SelectedValue = dtDetails.Rows(0).Item("HeadID")
                            ddlCrHead_SelectedIndexChanged(source, e)
                            ddlCrGL.SelectedValue = ddlCrHead.SelectedValue

                            If IsDBNull(dtDetails.Rows(0).Item("SubGLID")) = False Then
                                If dtDetails.Rows(0).Item("SubGLID") <> 0 Then
                                    ddlCrGL_SelectedIndexChanged(source, e)
                                    ddlCrSubGL.SelectedValue = dtDetails.Rows(0).Item("SubGLID")
                                End If
                            End If
                            txtCreditAmount.Text = dtDetails.Rows(0).Item("Credit")
                        End If
                    End If

                    If IsDBNull(dtDetails.Rows(0).Item("Debit")) = False Then
                        If dtDetails.Rows(0).Item("Debit") <> "" Then
                            'BindHeadofAccounts()
                            ddldbHead.SelectedValue = dtDetails.Rows(0).Item("HeadID")
                            ddldbHead_SelectedIndexChanged(source, e)
                            ddldbGL.SelectedValue = ddldbHead.SelectedValue

                            If IsDBNull(dtDetails.Rows(0).Item("SubGLID")) = False Then
                                If dtDetails.Rows(0).Item("SubGLID") <> 0 Then
                                    ddldbGL_SelectedIndexChanged(source, e)
                                    ddldbSubGL.SelectedValue = dtDetails.Rows(0).Item("SubGLID")
                                End If
                            End If
                            txtDebitAmount.Text = dtDetails.Rows(0).Item("Debit")
                        End If
                    End If
                    lblJEdetId = e.Item.FindControl("lblId")
                    dt = Session("DataTable")
                    Dim DVZRBADetails1 As New DataView(dt)
                    DVZRBADetails1.RowFilter = "detID<>'" & lblJEdetId.Text & "'"
                    dt = DVZRBADetails1.ToTable

                    If dt.Rows.Count > 0 Then
                        For j = 0 To dt.Rows.Count - 1
                            dt.Rows(j)("SrNo") = j + 1
                        Next
                        dt.AcceptChanges()
                    End If
                    dgJEDetails.DataSource = dt
                    dgJEDetails.DataBind()
                    Session("DataTable") = dt
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJEDetails_ItemCommand")
        End Try
    End Sub
    Private Sub dgJEDetails_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgJEDetails.ItemDataBound
        Dim imgbtnDelete As New ImageButton, imgbtnEdit As New ImageButton
        Try
            lblError.Text = ""
            If e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer Then
                imgbtnDelete = CType(e.Item.FindControl("imgbtnDelete"), ImageButton)
                imgbtnEdit = CType(e.Item.FindControl("imgbtnedit"), ImageButton)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                imgbtnDelete.ImageUrl = "~/Images/4delete.gif"

                dgJEDetails.Columns(15).Visible = False : dgJEDetails.Columns(17).Visible = True : btnAddDebit.Visible = True : btnAddCredit.Visible = True

                If sCustomerAudit = "Customer" Then
                    If lblStatus.Text <> "" Then
                        If lblStatus.Text <> "Waiting for Approval(Customer)" Then
                            dgJEDetails.Columns(15).Visible = False : dgJEDetails.Columns(17).Visible = False : btnAddDebit.Visible = False : btnAddCredit.Visible = False
                        End If
                    End If
                End If

                If sCustomerAudit = "Auditor" Then
                    If lblStatus.Text <> "" Then
                        If lblStatus.Text <> "Waiting for Approval(Auditor)" Then
                            dgJEDetails.Columns(15).Visible = False : dgJEDetails.Columns(17).Visible = False : btnAddDebit.Visible = False : btnAddCredit.Visible = False
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgJEDetails_ItemDataBound")
        End Try
    End Sub
    Protected Sub ddlParty_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlParty.SelectedIndexChanged
        Dim dtbranch As New DataTable
        Try
            If (ddlParty.SelectedIndex > 0) Then
                Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppCustomerID")
                AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                AppAccesscodeCookie = New HttpCookie("AppCustomerID")
                Dim AppCustomerID As Integer = ddlParty.SelectedValue
                AppAccesscodeCookie.Value = AppCustomerID
                AppAccesscodeCookie.Secure = True
                AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                Response.Cookies.Add(AppAccesscodeCookie)

                sSession.CustomerID = ddlParty.SelectedValue
                sSession.YearID = ddlFinancialYear.SelectedValue
                dtbranch = objJE.LoadBranches(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, ddlFinancialYear.SelectedValue)
                If dtbranch.Rows.Count > 0 Then
                    ddlBranch.DataSource = dtbranch
                    ddlBranch.DataTextField = "BranchName"
                    ddlBranch.DataValueField = "Branchid"
                    ddlBranch.DataBind()
                    ddlBranch.Items.Insert(0, "Select Branch Name")
                    If objJE.iAJTB_BranchId > 0 Then
                        sSession.ScheduleBranchId = objJE.iAJTB_BranchId
                        ddlBranch.SelectedValue = sSession.ScheduleBranchId
                        ddlBranch_SelectedIndexChanged(sender, e)
                    End If
                    If sSession.ScheduleBranchId <> 0 Then
                        ddlBranch.SelectedValue = sSession.ScheduleBranchId
                        ddlBranch_SelectedIndexChanged(sender, e)
                    Else
                        sSession.ScheduleBranchId = 0
                    End If
                    Session("AllSession") = sSession
                Else
                    lblStatus.Text = "Customer should have atleast one Branch, Please add"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-warning');$('#ModalExcelValidation').modal('show');", True)
                    ddlBranch.DataSource = dtbranch
                    ddlBranch.DataTextField = "BranchName"
                    ddlBranch.DataValueField = "Branchid"
                    ddlBranch.DataBind()
                    ddlBranch.Items.Insert(0, "Select Branch Name")
                    Exit Sub
                    Exit Sub
                End If
                Session("AllSession") = sSession
                'LoadSubGL()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlParty_SelectedIndexChanged")
        End Try
    End Sub
    Private Sub imgbtnAddGL_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddGL.Click
        Try
            txtdescription.Text = "" : lblledgererrormsg.Text = "" : lblError.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalLedger').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddGL_Click")
        End Try
    End Sub
    Private Sub imgbtnGL_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnGL.Click
        Try
            txtdescription.Text = "" : lblledgererrormsg.Text = "" : lblError.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalLedger').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnGL_Click")
        End Try
    End Sub

    Private Sub ddlCrSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCrSubGL.SelectedIndexChanged
        Try
            dt = objJE.GetLedgerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, ddlCrSubGL.SelectedValue)
            ddlCrHead.SelectedIndex = dt.Rows(0).Item("CC_AccHead")
            ddlCrHead_SelectedIndexChanged(sender, e)
            ddlCrGL.SelectedValue = dt.Rows(0).Item("CC_Parent")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCrSubGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub ddldbSubGL_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddldbSubGL.SelectedIndexChanged
        Try
            dt = objJE.GetLedgerdetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, ddldbSubGL.SelectedValue)
            ddldbHead.SelectedIndex = dt.Rows(0).Item("CC_AccHead")
            ddldbHead_SelectedIndexChanged(sender, e)
            ddldbGL.SelectedValue = dt.Rows(0).Item("CC_Parent")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddldbSubGL_SelectedIndexChanged")
        End Try
    End Sub

    Private Sub btndescSave_Click(sender As Object, e As EventArgs) Handles btndescSave.Click
        Dim checkdata As Integer = 0
        Dim Arr() As String
        Try
            If ddlParty.SelectedIndex = 0 Then
                lblError.Text = "Please Select Customer"
                lblCustomerValidationMsg.Text = "Please Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalFASCompanyValidation').modal('show');", True)
                ddlParty.Focus()
                Exit Sub
            ElseIf txtdescription.Text = "" Then
                lblError.Text = "Please Enter Description"
                lblledgererrormsg.Text = "Please Enter Description"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalLedger').modal('show');", True)
                txtdescription.Focus()
                Exit Sub
            End If
            If ddlBranch.SelectedIndex < 1 Then
                lblError.Text = "Select Branch"
                lblledgererrormsg.Text = "Select Branch"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalLedger').modal('show');", True)
                ddlBranch.Focus()
                Exit Sub
            End If
            checkdata = objUT.Checkdata(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, txtdescription.Text, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
            If checkdata = 0 Then
                Dim iMaxCount = 0
                iMaxCount = objUT.getDescmaxid(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue, txtdescription.Text, ddlFinancialYear.SelectedValue, ddlBranch.SelectedValue)
                objUT.iATBU_ID = 0
                objUT.sATBU_CODE = "SCh00" & (iMaxCount + 1)
                objUT.sATBU_Description = txtdescription.Text
                objUT.iATBU_CustId = ddlParty.SelectedValue
                objUT.dATBU_Opening_Debit_Amount = Double.Parse(0)
                objUT.dATBU_Opening_Credit_Amount = Double.Parse(0)
                objUT.dATBU_TR_Debit_Amount = Double.Parse(0)
                objUT.dATBU_TR_Credit_Amount = Double.Parse(0)
                objUT.dATBU_Closing_Debit_Amount = Double.Parse(0)
                objUT.dATBU_Closing_Credit_Amount = Double.Parse(0)
                objUT.sATBU_DELFLG = "A"
                objUT.iATBU_CRBY = sSession.UserID
                objUT.sATBU_STATUS = "C"
                objUT.iATBU_UPDATEDBY = sSession.UserID
                objUT.sATBU_IPAddress = sSession.IPAddress
                objUT.iATBU_CompId = sSession.AccessCodeID
                objUT.iATBU_YEARId = ddlFinancialYear.SelectedValue
                objUT.iATBU_Branchname = ddlBranch.SelectedValue
                Arr = objUT.SaveTrailBalanceExcelUpload(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                objUT.iATBUD_ID = 0
                objUT.iATBUD_Masid = Arr(1)
                'objUT.iATBUD_ID = lblDescID.Text
                'objUT.iATBUD_Masid = lblDescdetails.Text

                objUT.sATBUD_CODE = "SCh00" & (iMaxCount + 1)
                objUT.sATBUD_Description = txtdescription.Text
                objUT.iATBUD_CustId = ddlParty.SelectedValue

                objUT.iATBUD_SChedule_Type = 0

                objUT.iATBUD_Branchname = ddlBranch.SelectedValue

                objUT.iATBUD_Company_Type = objUT.getOrgtype(sSession.AccessCode, sSession.AccessCodeID, ddlParty.SelectedValue)

                objUT.iATBUD_Headingid = 0

                objUT.iATBUD_Subheading = 0

                objUT.iATBUD_itemid = 0

                objUT.iATBUD_Subitemid = 0

                objUT.sATBUD_DELFLG = "A"
                objUT.iATBUD_CRBY = sSession.UserID
                objUT.sATBUD_STATUS = "C"
                objUT.sATBUD_Progress = "Uploaded"
                objUT.iATBUD_UPDATEDBY = sSession.UserID
                objUT.sATBUD_IPAddress = sSession.IPAddress
                objUT.iATBUD_CompId = sSession.AccessCodeID
                '      objUT.iATBUD_YEARId = sSession.YearID

                objUT.iATBUD_YEARId = ddlFinancialYear.SelectedValue
                Arr = objUT.SaveTrailBalanceExcelUploaddetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objUT)
                ddlParty_SelectedIndexChanged(sender, e)
                lblCustomerValidationMsg.Text = "Successfully Added." : lblError.Text = "Successfully Added."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            Else
                lblError.Text = "Description already exist"
                lblledgererrormsg.Text = "Description already exist"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalLedger').modal('show');", True)
                txtdescription.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btndescSave_Click")
        End Try
    End Sub

    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            If ddlFinancialYear.SelectedIndex > 0 Then
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
            End If
            If ddlParty.SelectedIndex > 0 Then
                ddlParty_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Dim iBranch As Integer = 0
        Try
            lblError.Text = ""
            If ddlBranch.SelectedIndex < 1 Then
                iBranch = 0
            Else
                iBranch = ddlBranch.SelectedValue
                sSession.ScheduleBranchId = ddlBranch.SelectedValue
                Session("AllSession") = sSession
            End If
            LoadExistingJEs(ddlParty.SelectedValue, sCustomerAudit, iBranch)
            txtTransactionNo.Text = objJE.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue)
            BindHeadofAccounts()
            If ddlExistJE.SelectedIndex > 0 Then
                dt = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, ddlExistJE.SelectedValue, iBranch)
            Else
                dt = objJE.LoadTransactionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlParty.SelectedValue, 0, iBranch)
            End If
            If dt.Rows.Count > 0 Then
                dgJEDetails.DataSource = dt
                dgJEDetails.DataBind()
                Session("DataTable") = dt
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
