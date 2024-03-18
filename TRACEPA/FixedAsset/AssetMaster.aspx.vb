Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class AssetMaster
    Inherits System.Web.UI.Page

    Private sFormName As String = "AssetMaster"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Dim objMaster As New clsGeneralFunctions
    Dim objGen As New clsGRACeGeneral
    Dim objGenFun As New clsGeneralFunctions
    Private Shared sSession As AllSession
    Dim objFxdAsst As New ClsFexedAsst
    Private objIndex As New clsIndexing
    Dim objGnrlFnction As New clsGeneralFunctions
    Private Shared iDocID As Integer
    Dim dt As New DataTable
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Private objAsst As New ClsAssetMaster
    Private objLocationSetup As New ClsLocationSetup
    Private Shared FStartDate As Date
    Private Shared FEndDate As Date
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsStandardAudit As New clsStandardAudit
    Private Shared bLoginUserIsPartner As Boolean
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        imgbtnAttachment.ImageUrl = "~/Images/Attachment24.png"
        imgbtnView.ImageUrl = "~/Images/View24.png"
        ImgBtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnAddSuplier.ImageUrl = "~/Images/Add16.png"
        imgbtnEditSuplier.ImageUrl = "~/Images/Edit16.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
    End Sub
    Protected Sub pageload(sender As Object, e As EventArgs) Handles Me.Load
        Dim sID As String = ""
        Dim sAssettype As String = ""
        Dim dt As New DataTable
        Try
            sSession = Session("AllSession")

            If IsPostBack = False Then

                Session("Attachment") = Nothing
                dt.Columns.Add("FilePath")
                dt.Columns.Add("FileName")
                dt.Columns.Add("Extension")
                dt.Columns.Add("CreatedOn")
                Session("Attachment") = dt

                lblDateDisplay.Text = objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode)
                imgbtnAttachment.Attributes.Add("OnClick", "$('#myAttchment').modal('show');return false;")

                LoadCustomer()

                BindYearMaster()
                If sSession.ScheduleYearId = 0 Then
                    ddlFinancialYear.SelectedValue = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    ddlFinancialYear_SelectedIndexChanged(sender, e)
                Else
                    ddlFinancialYear.SelectedValue = sSession.ScheduleYearId
                    ddlFinancialYear_SelectedIndexChanged(sender, e)
                End If

                If objclsStandardAudit.CheckLoginUserIsPartner(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID) = True Then
                    bLoginUserIsPartner = True
                Else
                    bLoginUserIsPartner = False
                End If
                If sSession.CustomerID <> 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        ddlCustomerName_SelectedIndexChanged(sender, e)
                    End If
                End If
                lisupplier_Detls.Attributes.Add("class", "active") : divSupDtls.Attributes.Add("class", "tab-pane active")
                lblTab.Text = 1
                lnkbtnSupDtls_Click(sender, e)

                'RFVdrpAstype.InitialValue = "Select AssetType" : RFVdrpAstype.ErrorMessage = "Select AssetType"
                loadAssetDeletion()
                'BindReasons()
                If ddlCustomerName.SelectedIndex > 0 Then
                    txtbxAstCode.Text = objFxdAsst.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    txtAssNo.Text = txtbxAstCode.Text
                Else
                    txtbxAstCode.Text = objFxdAsst.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, 0)
                    txtAssNo.Text = txtbxAstCode.Text
                End If

                ' loademployee()
                loadAssetType()
                'LoadZone()
                'LoadCurrency()
                loadUnits()
                loadExistingSupplier()
                sID = Request.QueryString("AFAM_ID")
                sAssettype = Request.QueryString("AFAM_AssetType")
                If sID <> "" Then
                    drpAstype.SelectedValue = objGen.DecryptQueryString(Request.QueryString("AFAM_AssetType"))
                    ddlAssClass.SelectedValue = objGen.DecryptQueryString(Request.QueryString("AFAM_AssetType"))
                    If drpAstype.SelectedValue = "Select Asset Class" Then

                    Else
                        loadExistingItemCode(drpAstype.SelectedValue)
                    End If
                    DrpItemCode.SelectedValue = objGen.DecryptQueryString(Request.QueryString("AFAM_ID"))
                        lblPkId.Text = objGen.DecryptQueryString(Request.QueryString("AFAM_ID"))
                        DrpItemCode_SelectedIndexChanged(sender, e)
                        lnkbtnSupDtls_Click(sender, e)
                    End If
                If lblPkId.Text = "" Then
                    btnchangeclass.Visible = False
                    drpAstype.Enabled = True
                    txtbxItmCode.Enabled = True
                    txtbxItmDecrtn.Enabled = True
                    txtbxQty.Enabled = True
                    ddlUnits.Enabled = True
                    txtbxDteCmmunictn.Enabled = True
                Else
                    btnchangeclass.Visible = True
                    drpAstype.Enabled = False
                    txtbxAstCode.Enabled = False
                    txtbxItmCode.Enabled = False
                    txtbxItmDecrtn.Enabled = False
                    txtbxQty.Enabled = False
                    ddlUnits.Enabled = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "pageload" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlFinancialYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFinancialYear.SelectedIndexChanged
        Try
            If ddlFinancialYear.SelectedIndex > 0 Then
                FStartDate = objclsGRACeGeneral.FormatDtForRDBMS(objclsGRACeGeneral.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
                FEndDate = objclsGRACeGeneral.FormatDtForRDBMS(objclsGRACeGeneral.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue), "D")
                sSession.ScheduleYearId = ddlFinancialYear.SelectedValue
                Session("AllSession") = sSession
                ddlCustomerName_SelectedIndexChanged(sender, e)
                clear()
                AssetNo()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFinancialYear_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub BindYearMaster()
        Try
            ddlFinancialYear.DataSource = objclsGeneralFunctions.LoadYears(sSession.AccessCode, sSession.AccessCodeID)
            ddlFinancialYear.DataTextField = "YMS_ID"
            ddlFinancialYear.DataValueField = "YMS_YEARID"
            ddlFinancialYear.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindYearMaster" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
            'Throw
        End Try
    End Sub
    Public Function BindLocation() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If

            ddlLocatn.DataSource = dt
            ddlLocatn.DataTextField = "LS_Description"
            ddlLocatn.DataValueField = "LS_ID"
            ddlLocatn.DataBind()
            ddlLocatn.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                Try
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    loadAssetType()
                    BindLocation()
                    clear()
                    AssetNo()
                Catch ex As Exception

                End Try
            End If
            btnchangeclass.Visible = False
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub clear()
        Try
            txtmasterid.Text = ""
            drpAstype.SelectedIndex = 0
            txtbxItmCode.Text = ""
            txtbxItmDecrtn.Text = ""
            ddlBay.SelectedIndex = 0
            ddlDeptmnt.SelectedIndex = 0
            txtbxQty.Text = ""
            ddlUnits.SelectedIndex = -1
            txtbxAstAge.Text = ""
            txtbxDteCmmunictn.Text = ""
            drpAstype.Enabled = True
            txtbxItmCode.Enabled = True
            txtbxItmDecrtn.Enabled = True
            txtbxQty.Enabled = True
            ddlUnits.Enabled = True
            txtbxAstAge.Enabled = True
            txtbxDteCmmunictn.Enabled = True
        Catch ex As Exception

        End Try
    End Sub
    Public Function LoadCustomer() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objAsst.LoadCustomer(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustomerName.DataSource = dt
            ddlCustomerName.DataTextField = "CUST_NAME"
            ddlCustomerName.DataValueField = "CUST_ID"
            ddlCustomerName.DataBind()
            ddlCustomerName.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustomer" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Sub loadUnits()
        Dim dt As New DataTable
        Try

            dt = objFxdAsst.LoadUnitsOfMeasur(sSession.AccessCode, sSession.AccessCodeID)
            ddlUnits.DataTextField = "cmm_Desc"
            ddlUnits.DataValueField = "cmm_ID"
            ddlUnits.DataSource = dt
            ddlUnits.DataBind()
            ddlUnits.Items.Insert(0, "Select Units")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadUnits" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub LoadZone()
    '    Dim dt As New DataTable
    '    Try
    '        dt = objFxdAsst.LoadLocationZone(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlLocatn.DataTextField = "org_name"
    '        ddlLocatn.DataValueField = "org_node"
    '        ddlLocatn.DataSource = dt
    '        ddlLocatn.DataBind()
    '        ddlLocatn.Items.Insert(0, "--- Select Location ---")

    '    Catch ex As Exception
    '        lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadZone")
    '    End Try
    'End Sub
    Public Sub loadAssetDeletion()
        Dim dt As New DataTable
        Try
            'dt = objFxdAsst.LoadAssetDeletion(sSession.AccessCode, sSession.AccessCodeID)
            'ddlDeletion.DataTextField = "Mas_Desc"
            'ddlDeletion.DataValueField = "Mas_Id"
            'ddlDeletion.DataSource = dt
            'ddlDeletion.DataBind()
            ddlDeletion.Items.Insert(0, "--- Asset Deletion ---")
            ddlDeletion.Items.Insert(1, "Sold")
            ddlDeletion.Items.Insert(2, "Transfer")
            ddlDeletion.Items.Insert(3, "Stolen")
            ddlDeletion.Items.Insert(4, "Destroyed")
            ddlDeletion.Items.Insert(5, "Absolute")
            ddlDeletion.Items.Insert(6, "Repair")
            ddlDeletion.SelectedIndex = 0


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetDeletion" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Public Sub BindReasons()
    '    Try
    '        ddlReason.Items.Insert(0, "Select")
    '        ddlReason.Items.Insert(1, "Damage")
    '        ddlReason.Items.Insert(2, "Transfered")
    '        ddlReason.Items.Insert(3, "Send for Repair/Upgradation")
    '        ddlReason.Items.Insert(3, "Lack of tool")
    '        ddlReason.SelectedIndex = 0
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub loadExistingItemCode(ByVal AFAM_AssetType As Integer)
        Dim dt As New DataTable
        Try

            dt = objFxdAsst.LoadExistingItemCode(sSession.AccessCode, sSession.AccessCodeID, AFAM_AssetType)
            DrpItemCode.DataTextField = "AFAM_ItemDescription"
            DrpItemCode.DataValueField = "AFAM_ID"
            DrpItemCode.DataSource = dt
            DrpItemCode.DataBind()
            DrpItemCode.Items.Insert(0, "Existing Asset")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingItemCode" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnAlrtDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnAlrtDtls.Click
        Try
            lblTab.Text = 2
            lialortment_Detls.Attributes.Add("class", "active")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            divAplertDtls.Attributes.Add("class", "tab-pane active")
            liWarantyAMC_Detls.Attributes.Remove("class")
            divWrntyAMCDtls.Attributes.Add("class", "tab-pane")
            liAsset_detetion.Attributes.Remove("class")
            divAssetDeletion.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
            divInsuranceDetails.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAlrtDtls_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnSupDtls_Click(sender As Object, e As EventArgs) Handles lnkbtnSupDtls.Click
        Try
            lblTab.Text = 1
            lisupplier_Detls.Attributes.Add("class", "active")
            lialortment_Detls.Attributes.Remove("class")
            liWarantyAMC_Detls.Attributes.Remove("class")
            liAsset_detetion.Attributes.Remove("class")
            liAsset_Loan.Attributes.Remove("class")

            divSupDtls.Attributes.Add("class", "tab-pane active")
            divAplertDtls.Attributes.Add("class", "tab-pane")
            divWrntyAMCDtls.Attributes.Add("class", "tab-pane")
            divAssetDeletion.Attributes.Add("class", "tab-pane")
            divLoanAsst.Attributes.Add("class", "tab-pane")
            divInsuranceDetails.Attributes.Add("class", "tab-pane")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSupDtls_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkWrntyAMCDtls_Click(sender As Object, e As EventArgs) Handles lnkWrntyAMCDtls.Click
        Try
            lblTab.Text = 3

            liWarantyAMC_Detls.Attributes.Add("class", "active")
            divWrntyAMCDtls.Attributes.Add("class", "tab-pane active")
            lisupplier_Detls.Attributes.Remove("class")
            lialortment_Detls.Attributes.Remove("class")
            divAplertDtls.Attributes.Add("class", "tab-pane")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liAsset_detetion.Attributes.Remove("class")
            divAssetDeletion.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
            divInsuranceDetails.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkWrntyAMCDtls_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnDeletion_Click(sender As Object, e As EventArgs) Handles lnkbtnDeletion.Click
        Try
            lblTab.Text = 4
            liAsset_detetion.Attributes.Add("class", "active")
            divAssetDeletion.Attributes.Add("class", "tab-pane active")
            lialortment_Detls.Attributes.Remove("class")
            divAplertDtls.Attributes.Add("class", "tab-pane")
            liWarantyAMC_Detls.Attributes.Remove("class")
            divWrntyAMCDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")
            liAsset_Loan.Attributes.Remove("class")
            divLoanAsst.Attributes.Add("class", "tab-pane")
            divInsuranceDetails.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDeletion_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnLoanAsst_click(sender As Object, e As EventArgs) Handles lnkbtnLoanAsst.Click
        Try
            lblTab.Text = 5
            liAsset_Loan.Attributes.Add("class", "active")
            divLoanAsst.Attributes.Add("class", "tab-pane active")
            divInsuranceDetails.Attributes.Add("class", "tab-pane")
            liAsset_detetion.Attributes.Remove("class")
            divAssetDeletion.Attributes.Add("class", "tab-pane")
            lialortment_Detls.Attributes.Remove("class")
            divAplertDtls.Attributes.Add("class", "tab-pane")
            liWarantyAMC_Detls.Attributes.Remove("class")
            divWrntyAMCDtls.Attributes.Add("class", "tab-pane")
            lisupplier_Detls.Attributes.Remove("class")
            divSupDtls.Attributes.Add("class", "tab-pane")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLoanAsst_click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub loadAssetType()
        Dim dt As New DataTable
        Try
            If ddlCustomerName.SelectedIndex = 0 Then
                dt = objFxdAsst.LoadFxdAssetType(sSession.AccessCode, sSession.AccessCodeID, 0)
            Else
                dt = objFxdAsst.LoadFxdAssetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If

            drpAstype.DataTextField = "AM_Description"
            drpAstype.DataValueField = "AM_ID"
            drpAstype.DataSource = dt
            drpAstype.DataBind()
            drpAstype.Items.Insert(0, "Select Asset Class")

            ddlAssClass.DataTextField = "AM_Description"
            ddlAssClass.DataValueField = "AM_ID"
            ddlAssClass.DataSource = dt
            ddlAssClass.DataBind()
            ddlAssClass.Items.Insert(0, "Select Asset Class")



        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadAssetType" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Public Sub loademployee()
    '    Dim dt As New DataTable
    '    Try
    '        dt = objFxdAsst.Loademployee(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlEmployee.DataTextField = "usr_FullName"
    '        ddlEmployee.DataValueField = "usr_Id"
    '        ddlEmployee.DataSource = dt
    '        ddlEmployee.DataBind()
    '        ddlEmployee.Items.Insert(0, "Select Employee")
    '    Catch ex As Exception
    '        lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loademployee")
    '    End Try
    'End Sub
    'Public Sub loadDepartments(ByVal iAccZone As Integer)
    '    Dim dt As New DataTable
    '    Try
    '        dt = objFxdAsst.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, iAccZone)
    '        ddlDeptmnt.DataTextField = "org_name"
    '        ddlDeptmnt.DataValueField = "org_node"
    '        ddlDeptmnt.DataSource = dt
    '        ddlDeptmnt.DataBind()
    '        ddlDeptmnt.Items.Insert(0, "--- Select Department ---")

    '    Catch ex As Exception
    '        lblErrorUp.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadDepartments")
    '    End Try
    'End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim objFxdAsst As New ClsFexedAsst
        Dim iFAID As Integer = 0
        Dim dt As New DataTable
        Dim Arr() As String
        Dim dDatel, dSDateo As Date
        Dim dDatel1, dSDateo1 As Date
        Dim dDatel2, dSDateo2 As Date
        Dim bCheck As Boolean
        Dim dDate, dSDate As Date : Dim m As Integer
        Dim dToDate As Date
        Dim dPuttouseDate As Date
        Try

            If drpAstype.SelectedIndex = 0 Then
                lblError.Text = "Select Asset Class."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAdditionValidation').modal('show');", True)
                drpAstype.Focus()
                Exit Sub
            End If

            'If txtCode.Text = "" Then
            '    lblErrorUp.Text = "Generate Code."
            '    lblCustomerValidationMsg.Text = lblErrorUp.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    txtCode.Focus()
            '    Exit Sub
            'End If

            If txtmasterid.Text = "" Then
                If txtbxItmCode.Text <> "" Then
                    bCheck = objFxdAsst.TocheckExistitemcode(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, txtbxItmCode.Text, ddlFinancialYear.SelectedValue)
                    If bCheck = True Then
                        lblError.Text = "Entred Asset Code is Already Exist, Please Enter Diffrent Asset Code"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
            End If

            If txtmasterid.Text = "" Then
                If txtbxItmDecrtn.Text <> "" Then
                    bCheck = objFxdAsst.TocheckExistAsset(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, txtbxItmDecrtn.Text, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                    If bCheck = True Then
                        lblError.Text = "Entred Asset is Already Exist, Please Enter Diffrent Asset"
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
            End If

            ' Asset code not mandatory ' 06_5_22
            'If txtbxItmCode.Text = "" Then
            '    lblErrorUp.Text = "Enter Asset Code."
            '    lblCustomerValidationMsg.Text = lblErrorUp.Text
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalFASCompanyValidation').modal('show');", True)
            '    txtbxItmCode.Focus()
            '    Exit Sub
            'End If

            If txtbxItmDecrtn.Text = "" Then
                lblError.Text = "Enter Asset Description."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                txtbxItmDecrtn.Focus()
                Exit Sub
            End If

            If txtbxQty.Text = "" Then
                lblError.Text = "Enter Quantity."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                txtbxQty.Focus()
                Exit Sub
            End If

            If txtbxAstAge.Text = "" Then
                lblError.Text = "Useful life of Asset."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                txtbxAstAge.Focus()
                Exit Sub
            End If



            If (txtbxDteCmmunictn.Text = "") Then
                lblError.Text = "Enter Date of Put to use."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If

            If (txtbxDteCmmunictn.Text <> "") Then
                Try
                    dToDate = Date.ParseExact(FEndDate, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    dPuttouseDate = Date.ParseExact(txtbxDteCmmunictn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    m = DateDiff(DateInterval.Day, dToDate, dPuttouseDate)
                    If m > 0 Then
                        lblError.Text = "Put to use Date  (" & txtbxDteCmmunictn.Text & ") should be Lesser than or equal to Financial Year End Date(" & FEndDate & ")."
                        lblCustomerValidationMsg.Text = lblError.Text
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                        txtbxDteCmmunictn.Focus()
                        Exit Sub
                    End If
                    'dDate = Date.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'dSDate = Date.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    'm = DateDiff(DateInterval.Day, dDate, dSDate)
                    'If m < 0 Then
                    '    lblError.Text = "To Date (" & txtToDate.Text & ") should be Greater than From Date(" & txtFromDate.Text & ")."
                    '    txtToDate.Focus()
                    '    Exit Sub
                    'End If
                Catch ex As Exception
                    lblError.Text = "Invalid Date of Put to use."
                    lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalAdditionValidation').modal('show');", True)
                    txtbxDteCmmunictn.Focus()
                    Exit Sub
                End Try

            End If

            If ddlLocatn.SelectedIndex = 0 Then
                lblError.Text = "Select Location."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                ddlLocatn.Focus()
                Exit Sub
            End If

            If drpAstype.SelectedIndex > 0 Then
                iFAID = drpAstype.SelectedValue
            Else
                iFAID = 0
            End If

            If txtmasterid.Text <> "" Then
                objFxdAsst.AFAM_ID = txtmasterid.Text
            Else
                objFxdAsst.AFAM_ID = 0
            End If


            objFxdAsst.AFAM_AssetType = iFAID
            objFxdAsst.AFAM_AssetCode = txtbxAstCode.Text
            objFxdAsst.AFAM_Description = drpAstype.SelectedItem.Text
            objFxdAsst.AFAM_ItemCode = txtbxItmCode.Text
            objFxdAsst.AFAM_ItemDescription = txtbxItmDecrtn.Text

            objFxdAsst.AFAM_Quantity = txtbxQty.Text
            objFxdAsst.AFAM_PurchaseDate = "01/01/1900"
            If (txtbxDteCmmunictn.Text = "") Then
                objFxdAsst.AFAM_CommissionDate = "01/01/1900"
            Else
                objFxdAsst.AFAM_CommissionDate = Date.ParseExact((txtbxDteCmmunictn.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            'dDatel = Date.ParseExact(txtbxDteofPurchase.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'dSDateo = Date.ParseExact(txtbxDteCmmunictn.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            'Dim f As Integer
            'f = DateDiff(DateInterval.Day, dDatel, dSDateo)
            'If f < 0 Then
            '    lblErrorUp.Text = "CommissionDate  (" & txtbxDteCmmunictn.Text & ") should be Greater than or equal to  PurchaseDate(" & txtbxDteofPurchase.Text & ")."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('CommissionDate (" & txtbxDteCmmunictn.Text & ") should be Greater than or equal to  PurchaseDate(" & txtbxDteofPurchase.Text & ").','', 'success');", True)
            '    txtbxAMCfrmDate.Focus()
            '    Exit Sub
            'End If



            objFxdAsst.AFAM_AssetAge = txtbxAstAge.Text

            'objFxdAsst.AFAM_Quantity = txtbxQty.Text
            'objFxdAsst.AFAM_PurchaseDate = txtbxDteofPurchase.Text
            'objFxdAsst.AFAM_CommissionDate = txtbxDteCmmunictn.Text
            'objFxdAsst.AFAM_AssetAge = txtbxAstAge.Text
            'If rboFreshPurchase.Checked = True Then
            '    objFxdAsst.AFAM_PurchaseType = 0
            'ElseIf rboOpeningBalance.Checked = True Then
            '    objFxdAsst.AFAM_PurchaseType = 1
            'End If

            objFxdAsst.AFAM_PurchaseAmount = 0
            objFxdAsst.AFAM_PolicyNo = txtbxPlyNo.Text
            If (txtbxAmt.Text = "") Then
                objFxdAsst.AFAM_Amount = 0
            Else
                objFxdAsst.AFAM_Amount = txtbxAmt.Text
            End If

            If (txtbxfrmDate.Text = "") Then
                txtbxfrmDate.Text = "01/01/1900"
                objFxdAsst.AFAM_Date = "01/01/1900"
            Else
                objFxdAsst.AFAM_Date = Date.ParseExact(objGen.SafeSQL(Trim(txtbxfrmDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If (txtbxtoDate.Text = "") Then
                txtbxtoDate.Text = "01/01/1900"
                objFxdAsst.AFAM_ToDate = "01/01/1900"
            Else
                objFxdAsst.AFAM_ToDate = Date.ParseExact(objGen.SafeSQL(Trim(txtbxtoDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            dDatel1 = Date.ParseExact(txtbxfrmDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDateo1 = Date.ParseExact(txtbxtoDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim res As Integer
            res = DateDiff(DateInterval.Day, dDatel1, dSDateo1)
            'If res < 0 Then
            '    lblErrorUp.Text = "ToDate  (" & txtbxtoDate.Text & ") should be Greater than or equal to FromDate(" & txtbxfrmDate.Text & ")."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('ToDate (" & txtbxtoDate.Text & ") should be Greater than or equal to  FromDate(" & txtbxfrmDate.Text & ").','', 'success');", True)
            '    txtbxAMCfrmDate.Focus()
            '    Exit Sub
            'End If
            If (ddlLocatn.SelectedIndex > 0) Then
                objFxdAsst.AFAM_Location = ddlLocatn.SelectedValue
            Else
                objFxdAsst.AFAM_Location = 0
            End If
            If (ddlDivision.SelectedIndex > 0) Then
                objFxdAsst.AFAM_Division = ddlDivision.SelectedValue
            Else
                objFxdAsst.AFAM_Division = 0
            End If
            If (ddlDeptmnt.SelectedIndex > 0) Then
                objFxdAsst.AFAM_Department = ddlDeptmnt.SelectedValue
            Else
                objFxdAsst.AFAM_Department = 0
            End If
            If (ddlBay.SelectedIndex > 0) Then
                objFxdAsst.AFAM_Bay = ddlBay.SelectedValue
            Else
                objFxdAsst.AFAM_Bay = 0
            End If
            If txtEmployeeName.Text <> "" Then
                objFxdAsst.AFAM_EmployeeName = txtEmployeeName.Text
            Else
                objFxdAsst.AFAM_EmployeeName = ""
            End If
            If txtEmpCode.Text <> "" Then
                objFxdAsst.AFAM_EmployeeCode = txtEmpCode.Text
            Else
                objFxdAsst.AFAM_EmployeeCode = ""
            End If


            '  objFxdAsst.AFAM_SuplierName = txtbxSname.Text
            objFxdAsst.AFAM_ContactPerson = txtbxConPerson.Text
            objFxdAsst.AFAM_Address = txtbxAddress.Text
            objFxdAsst.AFAM_Phone = txtbxPhoneNo.Text
            objFxdAsst.AFAM_Fax = txtbxFax.Text
            objFxdAsst.AFAM_EmailID = txtbxEmail.Text
            objFxdAsst.AFAM_Website = txtbxwebsite.Text
            objFxdAsst.AFAM_CreatedBy = sSession.UserID
            objFxdAsst.AFAM_UpdatedBy = sSession.UserID
            objFxdAsst.AFAM_DelFlag = "X"
            objFxdAsst.AFAM_Status = "W"
            objFxdAsst.AFAM_YearID = ddlFinancialYear.SelectedValue
            objFxdAsst.AFAM_CompID = sSession.AccessCodeID
            objFxdAsst.AFAM_Opeartion = "C"
            objFxdAsst.AFAM_IPAddress = sSession.IPAddress
            objFxdAsst.AFAM_BrokerName = txtbxBrkName.Text
            objFxdAsst.AFAM_CompanyName = txtCmpName.Text
            objFxdAsst.AFAM_WrntyDesc = txtWrntyDesc.Text
            objFxdAsst.AFAM_ContactPrsn = txtContperson.Text
            If (txtbxAMCfrmDate.Text = "") Then
                txtbxAMCfrmDate.Text = "01/01/1900"
                objFxdAsst.AFAM_AMCFrmDate = "01/01/1900"
            Else
                objFxdAsst.AFAM_AMCFrmDate = Date.ParseExact(objGen.SafeSQL(Trim(txtbxAMCfrmDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            If (txtbxAMCtoDate.Text = "") Then
                txtbxAMCtoDate.Text = "01/01/1900"
                objFxdAsst.AFAM_AMCTo = "01/01/1900"
            Else
                objFxdAsst.AFAM_AMCTo = Date.ParseExact(objGen.SafeSQL(Trim(txtbxAMCtoDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            dDatel2 = Date.ParseExact(txtbxAMCfrmDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDateo2 = Date.ParseExact(txtbxAMCtoDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim resl As Integer
            resl = DateDiff(DateInterval.Day, dDatel2, dSDateo2)
            'If resl < 0 Then
            '    lblErrorUp.Text = "CommissionDate  (" & txtbxAMCtoDate.Text & ") should be Greater than or equal to  PurchaseDate(" & txtbxAMCfrmDate.Text & ")."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('CommissionDate (" & txtbxAMCtoDate.Text & ") should be Greater than or equal to  PurchaseDate(" & txtbxAMCfrmDate.Text & ").','', 'success');", True)
            '    txtbxAMCfrmDate.Focus()
            '    Exit Sub
            'End If
            objFxdAsst.AFAM_Contprsn = txtbxContprsn.Text
            objFxdAsst.AFAM_PhoneNo = txtbxPhno.Text
            objFxdAsst.AFAM_AMCCompanyName = txtbxAMCompname.Text

            If (txtDlnDate.Text = "") Then
                objFxdAsst.AFAM_DlnDate = "01/01/1900"
            Else
                objFxdAsst.AFAM_DlnDate = Date.ParseExact(objGen.SafeSQL(Trim(txtDlnDate.Text)), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If

            If (ddlDeletion.SelectedIndex > 0) Then
                objFxdAsst.AFAM_AssetDeletion = ddlDeletion.SelectedIndex
            Else
                objFxdAsst.AFAM_AssetDeletion = 0
            End If


            objFxdAsst.AFAM_Remark = txtremark.Text
            If (txtbxValue.Text = "") Then
                objFxdAsst.AFAM_Value = 0
            Else
                objFxdAsst.AFAM_Value = txtbxValue.Text
            End If

            If (txtdeletionDate.Text = "") Then
                objFxdAsst.AFAM_DateOfDeletion = "01/01/1900"
            Else
                objFxdAsst.AFAM_DateOfDeletion = txtdeletionDate.Text
            End If

            ''*******
            If (txtloanWhome.Text = "") Then
                objFxdAsst.AFAM_LToWhom = ""
            Else
                objFxdAsst.AFAM_LToWhom = txtloanWhome.Text
            End If

            If (txtloanAmount.Text = "") Then
                objFxdAsst.AFAM_LAmount = 0.0
            Else
                objFxdAsst.AFAM_LAmount = txtloanAmount.Text
            End If
            If (txtloanAgrmnt.Text = "") Then
                objFxdAsst.AFAM_LAggriNo = ""
            Else
                objFxdAsst.AFAM_LAggriNo = txtloanAgrmnt.Text
            End If
            If (txtloandate.Text = "") Then
                objFxdAsst.AFAM_LDate = "01/01/1900"
            Else
                objFxdAsst.AFAM_LDate = txtloandate.Text
            End If
            'If (ddlCurrencytypeloan.SelectedIndex = 0) Then
            objFxdAsst.AFAM_LCurrencyType = 0
            'Else
            '    objFxdAsst.AFAM_LCurrencyType = ddlCurrencytypeloan.SelectedValue
            'End If
            If (txtLoanExcngDate.Text = "") Then
                objFxdAsst.AFAM_LExchDate = "01/01/1900"
            Else
                objFxdAsst.AFAM_LExchDate = txtLoanExcngDate.Text
            End If

            objFxdAsst.AFAM_EMPCode = txtEmpCode.Text

            If ddlUnits.SelectedIndex > 0 Then
                objFxdAsst.AFAM_Unit = ddlUnits.SelectedValue
            Else
                objFxdAsst.AFAM_Unit = 0
            End If

            objFxdAsst.AFAM_CustId = ddlCustomerName.SelectedValue

            objFxdAsst.AFAM_Code = txtCode.Text

            objFxdAsst.AFAM_SuplierName = ddlSuplierName.SelectedValue

            Arr = objFxdAsst.SaveFxedAsset(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objFxdAsst)

            'Dim iItemcode As Integer
            'iItemcode = Arr(1)

            If Arr(0) = "2" Then
                lblError.Text = "Successfully Updated"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Creation", "Updated", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            ElseIf Arr(0) = "3" Then
                lblError.Text = "Successfully Saved"
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Creation", "Saved", ddlFinancialYear.SelectedValue, sSession.YearName, 0, "", sSession.IPAddress)
            End If
            loadExistingItemCode(drpAstype.SelectedValue)
            DrpItemCode.SelectedValue = Arr(1)
            DrpItemCode_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try

            Response.Redirect(String.Format("~/FixedAsset/AssetMaster.aspx?"), False)
            If ddlCustomerName.SelectedIndex > 0 Then
                txtbxAstCode.Text = objFxdAsst.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                txtbxAstCode.Text = objFxdAsst.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, 0)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub DrpItemCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DrpItemCode.SelectedIndexChanged
        Dim dtMaster As New DataTable
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            If DrpItemCode.SelectedIndex > 0 Then
                txtmasterid.Text = DrpItemCode.SelectedValue
                dtMaster = objFxdAsst.showDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, DrpItemCode.SelectedValue)
                If dtMaster.Rows.Count > 0 Then
                    For i = 0 To dtMaster.Rows.Count - 1
                        drpAstype.SelectedValue = dtMaster.Rows(i)("AFAM_AssetType")
                        txtbxAstCode.Text = dtMaster.Rows(i)("AFAM_AssetCode")
                        txtbxDscrptn.Text = dtMaster.Rows(i)("AFAM_Description")
                        txtbxItmCode.Text = dtMaster.Rows(i)("AFAM_ItemCode")
                        txtAssCode.Text = dtMaster.Rows(i)("AFAM_ItemCode")
                        txtbxItmDecrtn.Text = dtMaster.Rows(i)("AFAM_ItemDescription")
                        txtAssDesc.Text = dtMaster.Rows(i)("AFAM_ItemDescription")
                        txtbxQty.Text = dtMaster.Rows(i)("AFAM_Quantity")

                        If dtMaster.Rows(i)("AFAM_Unit") = 0 Then
                            ddlUnits.SelectedIndex = 0
                        Else
                            ddlUnits.SelectedValue = dtMaster.Rows(i)("AFAM_Unit")
                        End If


                        If IsDBNull(dtMaster.Rows(i)("AFAM_PurchaseDate")) = False Then
                            If (dtMaster.Rows(i)("AFAM_PurchaseDate") <> "01/01/1900") Then
                                txtbxDteofPurchase.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_PurchaseDate"), "D")
                            Else
                                txtbxDteofPurchase.Text = ""
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i)("AFAM_CommissionDate")) = False Then
                            If (dtMaster.Rows(i)("AFAM_CommissionDate") <> "01/01/1900") Then
                                txtbxDteCmmunictn.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_CommissionDate"), "D")
                            Else
                                txtbxDteCmmunictn.Text = ""
                            End If
                        End If
                        txtbxAstAge.Text = dtMaster.Rows(i)("AFAM_AssetAge")
                        txtbxAstAgeOld.Text = txtbxAstAge.Text

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_PolicyNo")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxPlyNo.Text = dtMaster.Rows(i)("AFAM_PolicyNo")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Amount")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxAmt.Text = dtMaster.Rows(i)("AFAM_Amount")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i)("AFAM_Date")) = False Then
                            If (dtMaster.Rows(i)("AFAM_Date") <> "01/01/1900") Then
                                txtbxfrmDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_Date"), "D")
                            Else
                                txtbxfrmDate.Text = ""
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Location")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_Location") > 0) Then
                                    ddlLocatn.SelectedValue = dtMaster.Rows(i)("AFAM_Location")
                                Else
                                    ddlLocatn.SelectedIndex = -1
                                End If
                            End If
                        End If
                        If (dtMaster.Rows(i)("AFAM_Division") > 0) Then
                            ddlDivision.SelectedValue = dtMaster.Rows(i)("AFAM_Division")
                            ddlLocatn_SelectedIndexChanged(sender, e)
                        Else
                            ddlDivision.SelectedIndex = -1
                        End If
                        'loadDepartments(ddlLocatn.SelectedValue)
                        If (dtMaster.Rows(i)("AFAM_Department") > 0) Then
                            ddlDeptmnt.SelectedValue = dtMaster.Rows(i)("AFAM_Department")
                            ddlDivision_SelectedIndexChanged(sender, e)
                        Else
                            ddlDeptmnt.SelectedIndex = -1
                        End If
                        If (dtMaster.Rows(i)("AFAM_Bay") > 0) Then
                            ddlBay.SelectedValue = dtMaster.Rows(i)("AFAM_Bay")
                            ddlDeptmnt_SelectedIndexChanged(sender, e)
                        Else
                            ddlBay.SelectedIndex = -1
                        End If

                        ' txtCode.Text = objFxdAsst.GenerateCode(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue , ddlCustomerName.SelectedValue)
                        'If (dtMaster.Rows(i)("AFAM_Employee") > 0) Then
                        '    ddlEmployee.SelectedValue = dtMaster.Rows(i)("AFAM_Employee")
                        'Else
                        '    ddlEmployee.SelectedIndex = 0
                        'End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Code")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtCode.Text = dtMaster.Rows(i)("AFAM_Code")
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_EmployeeName")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtEmployeeName.Text = dtMaster.Rows(i)("AFAM_EmployeeName")
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_EmployeeCode")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtEmpCode.Text = dtMaster.Rows(i)("AFAM_EmployeeCode")
                            End If
                        End If
                        If ddlSuplierName.SelectedIndex <> 0 Then
                            If IsDBNull(dtMaster.Rows(i).Item("AFAM_SuplierName")) = False Then
                                If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                    ddlSuplierName.SelectedValue = dtMaster.Rows(i)("AFAM_SuplierName")
                                End If
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_ContactPerson")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxConPerson.Text = dtMaster.Rows(i)("AFAM_ContactPerson")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Address")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxAddress.Text = dtMaster.Rows(i)("AFAM_Address")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Phone")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxPhoneNo.Text = dtMaster.Rows(i)("AFAM_Phone")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Fax")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxFax.Text = dtMaster.Rows(i)("AFAM_Fax")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_EmailID")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxEmail.Text = dtMaster.Rows(i)("AFAM_EmailID")
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Website")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxwebsite.Text = dtMaster.Rows(i)("AFAM_Website")
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_TrAssetAge")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                GvChangeddetails.Visible = True
                                divcollapse.Visible = True
                                Dim dtChDetails As DataTable = objAsst.Changeddetails(sSession.AccessCode, sSession.AccessCodeID, txtmasterid.Text, ddlCustomerName.SelectedValue, ddlFinancialYear.SelectedValue)
                                GvChangeddetails.DataSource = dtChDetails
                                GvChangeddetails.DataBind()
                            End If
                        End If


                        If dtMaster.Rows(i)("AFAM_Status") = "W" Then
                            lblstatus.Text = "Waiting For Approval"
                            imgbtnWaiting.Visible = True
                            imgbtnSave.ImageUrl = "~/Images/Update24.png"
                            imgbtnSave.ToolTip = "Update"
                            imgbtnSave.Visible = True
                        ElseIf dtMaster.Rows(i)("AFAM_Status") = "A" Then
                            lblstatus.Text = "Approved"
                            ddlCurrencytypeloan.Enabled = False
                            imgbtnWaiting.Visible = False
                            imgbtnSave.Visible = False

                            If bLoginUserIsPartner = True Then
                                If dtMaster.Rows(0)("AFAM_Status") = "A" Then
                                    imgbtnSave.ImageUrl = "~/Images/Update24.png"
                                    imgbtnSave.ToolTip = "Update"
                                    imgbtnSave.Visible = True
                                Else
                                    imgbtnSave.ImageUrl = "~/Images/Update24.png"
                                    imgbtnSave.ToolTip = "Update"
                                    imgbtnSave.Visible = True
                                End If
                            Else
                                If dt.Rows(0)("AFAM_Status") <> "A" Then
                                    imgbtnSave.ImageUrl = "~/Images/Update24.png"
                                    imgbtnSave.ToolTip = "Update"
                                    imgbtnSave.Visible = True
                                Else
                                    imgbtnSave.ImageUrl = "~/Images/Update24.png"
                                    imgbtnSave.ToolTip = "Update"
                                    imgbtnSave.Visible = False
                                End If
                            End If



                        End If
                        'txtbxBrkName.Text = dtMaster.Rows(i)("AFAM_BrokerName")
                        'txtCmpName.Text = dtMaster.Rows(i)("AFAM_CompanyName")
                        'txtWrntyDesc.Text = dtMaster.Rows(i)("AFAM_WrntyDesc")
                        'txtContperson.Text = dtMaster.Rows(i)("AFAM_ContactPrsn")
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_AMCFrmDate")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_AMCFrmDate") <> "01/01/1900") Then
                                    txtbxAMCfrmDate.Text = dtMaster.Rows(i)("AFAM_AMCFrmDate")
                                Else
                                    txtbxAMCfrmDate.Text = ""
                                End If
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_AMCTo")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_AMCTo") <> "01/01/1900") Then
                                    txtbxAMCtoDate.Text = dtMaster.Rows(i)("AFAM_AMCTo")
                                Else
                                    txtbxAMCtoDate.Text = ""
                                End If
                            End If
                        End If
                        'txtbxContprsn.Text = dtMaster.Rows(i)("AFAM_Contprsn")
                        'txtbxPhno.Text = dtMaster.Rows(i)("AFAM_PhoneNo")
                        'txtbxAMCompname.Text = dtMaster.Rows(i)("AFAM_AMCCompanyName")
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_ToDate")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_ToDate") <> "01/01/1900") Then
                                    txtbxtoDate.Text = dtMaster.Rows(i)("AFAM_ToDate")
                                Else
                                    txtbxtoDate.Text = ""
                                End If
                            End If
                        End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_AssetDeletion")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_AssetDeletion") > 0) Then
                                    ddlDeletion.SelectedIndex = dtMaster.Rows(i)("AFAM_AssetDeletion")
                                Else
                                    ddlDeletion.SelectedIndex = 0
                                End If
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_DlnDate")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_DlnDate") <> "01/01/1900") Then
                                    txtDlnDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_DlnDate"), "D")
                                Else
                                    txtDlnDate.Text = ""
                                End If
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Remark")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtremark.Text = dtMaster.Rows(i)("AFAM_Remark")
                            End If
                        End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_Value")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                txtbxValue.Text = dtMaster.Rows(i)("AFAM_Value")
                            End If
                        End If
                        'If IsDBNull(dtMaster.Rows(i).Item("AFAM_ReasonDeletion")) = False Then
                        '    If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                        '        ddlReason.SelectedIndex = dtMaster.Rows(i)("AFAM_ReasonDeletion")
                        '    End If
                        'End If

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_DateOfDeletion")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_DateOfDeletion") <> "01/01/1900") Then
                                    txtdeletionDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_DateOfDeletion"), "D")
                                Else
                                    txtdeletionDate.Text = ""
                                End If
                            End If
                        End If

                        'txtloanWhome.Text = dtMaster.Rows(i)("AFAM_LToWhom")
                        'txtloanAmount.Text = dtMaster.Rows(i)("AFAM_LAmount")
                        'txtloanAgrmnt.Text = dtMaster.Rows(i)("AFAM_LAggriNo")

                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_LDate")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_LDate") <> "01/01/1900") Then
                                    txtloandate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_LDate"), "D")
                                Else
                                    txtloandate.Text = ""
                                End If
                            End If
                        End If
                        'If (dtMaster.Rows(i)("AFAM_LCurrencyType") > 0) Then
                        '    ddlCurrencytypeloan.SelectedValue = dtMaster.Rows(i)("AFAM_LCurrencyType")
                        'Else
                        '    ddlCurrencytypeloan.SelectedIndex = 0
                        'End If
                        If IsDBNull(dtMaster.Rows(i).Item("AFAM_LExchDate")) = False Then
                            If dtMaster.Rows(i).Item(0).ToString <> "&nbsp;" Then
                                If (dtMaster.Rows(i)("AFAM_LExchDate") <> "01/01/1900") Then
                                    txtLoanExcngDate.Text = objGen.FormatDtForRDBMS(dtMaster.Rows(i)("AFAM_LExchDate"), "D")
                                Else
                                    txtLoanExcngDate.Text = ""
                                End If
                            End If
                        End If

                    Next
                    GetAttachFile(DrpItemCode.SelectedItem.Text)
                    lblBadgeCount.Text = Convert.ToString(objFxdAsst.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, DrpItemCode.SelectedItem.Text))
                End If
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DrpItemCode_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub drpAstype_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpAstype.SelectedIndexChanged
        Try
            If drpAstype.SelectedIndex > 0 Then
                loadExistingItemCode(drpAstype.SelectedValue)
            Else
                DrpItemCode.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "drpAstype_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub AssetNo()
        Try
            If ddlCustomerName.SelectedIndex > 0 Then
                txtbxAstCode.Text = objFxdAsst.GenerateTransactionNo(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)

            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim sStatus As String = ""
        Try

            If txtbxDteCmmunictn.Text = "" Then
                lblError.Text = "Enter Date of Put to use Before Approve."
                lblCustomerValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                txtbxDteCmmunictn.Focus()
                Exit Sub
            End If

            If (drpAstype.SelectedIndex > 0) Then
                sStatus = objFxdAsst.GetStatus(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, DrpItemCode.SelectedValue, ddlFinancialYear.SelectedValue, ddlCustomerName.SelectedValue)
                If sStatus = "A" Then
                    lblError.Text = "This Asset type and Item Code Already Approved." : lblCustomerValidationMsg.Text = lblError.Text
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                ElseIf sStatus = "W" Then
                    objFxdAsst.StatusCheck(sSession.AccessCode, sSession.AccessCodeID, drpAstype.SelectedValue, DrpItemCode.SelectedValue, sSession.IPAddress, "W", ddlCustomerName.SelectedValue)
                    lblCustomerValidationMsg.Text = "Successfully Approved." : lblError.Text = "Successfully Approved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
                End If
                imgbtnSave.Visible = False : imgbtnWaiting.Visible = False
            ElseIf drpAstype.SelectedIndex = 0 Then
                lblError.Text = "Select Asset Class"
                Exit Sub
            End If
            DrpItemCode_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlLocatn_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocatn.SelectedIndexChanged
        'If ddlLocatn.SelectedIndex > 0 Then
        '    loadDepartments(ddlLocatn.SelectedValue)
        'End If
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlLocatn.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, ddlLocatn.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlDivision.DataSource = dt
            ddlDivision.DataTextField = "LS_Description"
            ddlDivision.DataValueField = "LS_ID"
            ddlDivision.DataBind()
            ddlDivision.Items.Insert(0, "Select ")


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLocatn_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Private Sub ddlEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEmployee.SelectedIndexChanged
    '    Try
    '        If ddlEmployee.SelectedIndex > 0 Then
    '            txtEmpCode.Text = objFxdAsst.GetEmpCode(sSession.AccessCode, sSession.AccessCodeID, ddlEmployee.SelectedValue)
    '        End If
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            drpAstype.SelectedIndex = 0 : DrpItemCode.SelectedIndex = 0 : txtbxAstCode.Text = "" : txtbxDscrptn.Text = ""
            txtbxItmCode.Text = "" : txtbxItmDecrtn.Text = "" : txtbxQty.Text = "" : txtbxDteofPurchase.Text = "" : txtbxDteCmmunictn.Text = ""
            txtbxAstAge.Text = "" : txtbxamount.Text = "" : txtbxPlyNo.Text = "" : txtbxAmt.Text = "" : txtbxBrkName.Text = "" : txtCmpName.Text = ""
            txtbxfrmDate.Text = "" : txtbxtoDate.Text = "" : txtbxConPerson.Text = "" : txtbxAddress.Text = "" : txtbxPhoneNo.Text = ""
            txtbxFax.Text = "" : txtbxEmail.Text = "" : txtbxwebsite.Text = "" : ddlLocatn.SelectedIndex = 0 : ddlDeptmnt.SelectedIndex = 0
            ddlDivision.SelectedIndex = 0 : ddlBay.SelectedIndex = 0
            txtEmployeeName.Text = "" : txtEmpCode.Text = "" : txtWrntyDesc.Text = "" : txtContperson.Text = "" : txtbxAMCompname.Text = ""
            txtbxAMCfrmDate.Text = "" : txtbxAMCtoDate.Text = "" : txtbxContprsn.Text = "" : txtbxPhno.Text = "" : ddlDeletion.SelectedIndex = 0
            txtDlnDate.Text = "" : txtdeletionDate.Text = "" : txtbxValue.Text = "" : txtremark.Text = "" : txtmasterid.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub LoadCurrency()
        Try
            ddlCurrencytypeloan.DataSource = objFxdAsst.LoadCurrency(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurrencytypeloan.DataTextField = "CUR_CountryName"
            ddlCurrencytypeloan.DataValueField = "CUR_ID"
            ddlCurrencytypeloan.DataBind()
            ddlCurrencytypeloan.Items.Insert(0, "Select Currency")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCurrency" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlCurrencytypeloan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCurrencytypeloan.SelectedIndexChanged
        Try

        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnIndex_Click(sender As Object, e As EventArgs) Handles btnIndex.Click
        Dim objBatch As clsIndexing.BatchScan
        Dim Arr() As String
        Try
            If gvattach.Rows.Count > 0 Then
                AutomaticIndexing()
                GetAttachFile(DrpItemCode.SelectedItem.Text)
                gvattach.Visible = True
                '  gvattach.DataBind()
                lblBadgeCount.Text = Convert.ToString(objFxdAsst.BindAttachFilesCount(sSession.AccessCode, sSession.AccessCodeID, DrpItemCode.SelectedItem.Text))
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
            Else
                lblError.Text = "Add the files before index"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnIndex_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub AutomaticIndexing()
        Dim icabinetID As Integer = 0, iSubCabinet As Integer = 0, iFolder As Integer = 0, iType As Integer = 0, iPageDetailsid As Integer = 0, iPageID As Integer = 0, j As Integer
        Dim chkSelect As New CheckBox
        Dim sKeywords As String = "", sPageExt As String, sFilePath As String, sFileName As String, sISDB As String
        Dim Arr() As String
        Dim dDate As Date
        Dim txtKeywords As New TextBox, txtValues As New TextBox
        Dim lblPath As New Label, lblDescriptorID As New Label
        'Dim iCabinet As Integer
        'Dim dt As New DataTable, dt2 As New DataTable, dt4 As New DataTable, dt6 As New DataTable
        Dim bCheckCabinet As Boolean

        Try
            If drpAstype.SelectedIndex = 0 Then
                lblError.Text = "Select Branch."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                drpAstype.Focus()
                Exit Sub
            Else
                icabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, drpAstype.SelectedItem.Text)
            End If

            iSubCabinet = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, icabinetID, "Fixed Asset Master")

            If DrpItemCode.SelectedIndex = 0 Then
                lblError.Text = "Select Asset Item Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
                drpAstype.Focus()
                Exit Sub

            Else
                iFolder = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinet, DrpItemCode.SelectedItem.Text)
            End If

            iType = objIndex.GetDOCTYPEID(sSession.AccessCode, sSession.AccessCodeID)

            'If ddlType.SelectedIndex = 0 Then
            '    lblModelError.Text = "Select Type."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalIndex').modal('show');", True)
            '    ddlType.Focus()
            '    Exit Sub
            'Else
            '    iType = ddlType.SelectedValue
            'End If

            If icabinetID > 0 And iSubCabinet > 0 And iFolder > 0 And iType > 0 Then
                If gvattach.Rows.Count > 0 Then
                    For i = 0 To gvattach.Rows.Count - 1
                        iPageDetailsid = 0
                        chkSelect = gvattach.Rows(i).FindControl("chkSelect")
                        lblPath = gvattach.Rows(i).FindControl("lblPath")
                        If chkSelect.Checked = True Then
                            sPageExt = UCase(gvattach.Rows(i).Cells(3).Text)
                            sFilePath = lblPath.Text
                            sFileName = gvattach.Rows(i).Cells(2).Text
                            objIndex.iPGEBASENAME = objGnrlFnction.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_BASENAME", "Pge_CompID")
                            objIndex.iPGEFOLDER = iFolder
                            objIndex.iPGECABINET = icabinetID
                            objIndex.iPGEDOCUMENTTYPE = iType
                            objIndex.sPGETITLE = objGen.SafeSQL(txtTitle.Text.Trim)
                            dDate = Date.ParseExact(lblDateDisplay.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                            objIndex.dPGEDATE = dDate
                            If iPageDetailsid = 0 Then
                                iPageDetailsid = objIndex.iPGEBASENAME
                                objIndex.iPgeDETAILSID = iPageDetailsid
                            End If
                            objIndex.iPgeCreatedBy = sSession.UserID
                            objIndex.iPGEPAGENO = objGnrlFnction.GetMaxID(sSession.AccessCode, sSession.AccessCodeID, "edt_page", "PGE_PAGENO", "Pge_CompID")
                            objIndex.sPGEEXT = sPageExt
                            If gvKeywords.Rows.Count > 0 Then

                                For k = 0 To gvKeywords.Rows.Count - 1
                                    txtKeywords = gvKeywords.Rows(k).FindControl("txtKeywords")
                                    If txtKeywords.Text <> "" Then
                                        sKeywords = sKeywords & "," & txtKeywords.Text
                                    End If
                                Next
                            End If
                            If sKeywords.StartsWith(",") = True Then
                                sKeywords = sKeywords.Remove(0, 1)
                            End If
                            If sKeywords.EndsWith(",") = True Then
                                sKeywords = sKeywords.Remove(Len(sKeywords) - 1, 1)
                            End If
                            objIndex.sPGEKeyWORD = objGen.SafeSQL(sKeywords)
                            objIndex.sPGEOCRText = ""
                            objIndex.iPGESIZE = 0
                            objIndex.iPGECURRENT_VER = 0
                            Select Case UCase(sPageExt)
                                Case "TIF", "TIFF", "JPG", "JPEG", "BMP", "BRK", "CAL", "CLP", "DCX", "EPS", "ICO", "IFF", "IMT", "ICA", "PCT", "PCX", "PNG", "PSD", "RAS", "SGI", "TGA", "XBM", "XPM", "XWD"
                                    objIndex.sPGEOBJECT = "IMAGE"
                                Case Else
                                    objIndex.sPGEOBJECT = "OLE"
                            End Select
                            objIndex.sPGESTATUS = "A"
                            objIndex.iPGESubCabinet = iSubCabinet
                            objIndex.iPgeUpdatedBy = sSession.UserID

                            objIndex.spgeDelflag = "A"
                            objIndex.iPGEQCUsrGrpId = 0
                            objIndex.sPGEFTPStatus = "F"
                            objIndex.iPGEbatchname = objIndex.iPGEBASENAME
                            objIndex.spgeOrignalFileName = objGen.SafeSQL(sFileName)
                            objIndex.iPGEBatchID = 0
                            objIndex.iPGEOCRDelFlag = 0
                            objIndex.iPgeCompID = sSession.AccessCodeID
                            Arr = objIndex.SavePage(sSession.AccessCode, sSession.AccessCodeID, objIndex)
                            sISDB = objIndex.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
                            FilePageInEdict(objIndex.iPGEBASENAME, sFilePath, UCase(sISDB))
                            objIndex.UpdateImageSettings(sSession.AccessCode, sSession.AccessCodeID, objIndex.iPGEBASENAME, iPageID)

                            If gvDocumentType.Rows.Count > 0 Then
                                For j = 0 To gvDocumentType.Rows.Count - 1
                                    lblDescriptorID = gvDocumentType.Rows(j).FindControl("lblDescriptorID")
                                    txtValues = gvDocumentType.Rows(j).FindControl("txtValues")
                                    If objIndex.iPGEBASENAME = iPageDetailsid Then
                                        objIndex.SavePageDetails(sSession.AccessCode, sSession.AccessCodeID, iPageDetailsid, iType, lblDescriptorID.Text, objIndex.sPGEKeyWORD, txtValues.Text)
                                    End If
                                Next
                            End If
                        End If
                    Next

                    If Arr(0) = "3" Then
                        lblError.Text = "Successfully Indexed."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalUserMasterDetailsValidation').modal('show');", True)

                        gvattach.DataSource = Nothing
                        gvattach.DataBind()
                        gvattach.Visible = False
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "AutomaticIndexing" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function FilePageInEdict(ByVal iBaseName As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
        Dim sImagePath As String
        Dim sExt As String
        Try
            sExt = System.IO.Path.GetExtension(sFilePath)
            If sFileInDB = "FALSE" Then
                sImagePath = objIndex.GetImagePath(sSession.AccessCode)
                sImagePath = sImagePath & "\BITMAPS\" & iBaseName \ 301 & "\"
                objGnrlFnction.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
                sImagePath = sImagePath & iBaseName & sExt   'Actual File Name
                If System.IO.File.Exists(sImagePath) = False Then
                    FileCopy(sFilePath, sImagePath)
                    FilePageInEdict = True
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "FilePageInEdict" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub btnAttch_Click(sender As Object, e As EventArgs) Handles btnAttch.Click
        Dim fileBasePath As String = "", fileName As String = "", fullFilePath As String = ""
        Dim dRow As DataRow
        Dim sFilesNames As String
        Dim i As Integer = 0
        Try
            lblError.Text = "" : iDocID = 0

            If DrpItemCode.SelectedIndex > 0 Then
            Else
                lblError.Text = "Select Asset Item Code."
                DrpItemCode.Focus()
                Exit Sub
            End If

            Dim hfc As HttpFileCollection = Request.Files

            If hfc.Count > 0 Then
                For i = 0 To hfc.Count - 1
                    Dim hpf As HttpPostedFile = hfc(i)
                    If hpf.ContentLength > 0 Then
                        dRow = dt.NewRow()
                        sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                        dt = Session("Attachment")
                        If dt.Rows.Count = 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)

                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        ElseIf dt.Rows.Count > 0 Then
                            sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
                            hpf.SaveAs(Server.MapPath(".") & "/Images/" & sFilesNames)
                            dRow = dt.NewRow()
                            dRow("FilePath") = Server.MapPath(".") & "/Images/" & sFilesNames
                            dRow("FileName") = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
                            dRow("Extension") = System.IO.Path.GetExtension(hpf.FileName)
                            dRow("CreatedOn") = objGnrlFnction.GetCurrentDate(sSession.AccessCode)
                            dt.Rows.Add(dRow)
                            Dim dvAttach As New DataView(dt)
                            dvAttach.Sort = "FileName Desc"
                            dt = dvAttach.ToTable
                            Session("Attachment") = dt
                        End If
                    End If
                Next
            End If

            If dt.Rows.Count = 0 Then
                lblError.Text = "No file to Attach."
            End If

            Session("Attachment") = dt
            gvattach.DataSource = dt
            gvattach.DataBind()

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAttch_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvattach_PreRender(sender As Object, e As EventArgs) Handles gvattach.PreRender
        Try
            If gvattach.Rows.Count > 0 Then
                gvattach.UseAccessibleHeader = True
                gvattach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvattach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvattach_PreRender" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub chkSelectAll_CheckedChanged(sender As Object, e As EventArgs)
        Dim chkField As New CheckBox, chkAll As New CheckBox
        Dim iIndx As Integer
        Try
            chkAll = CType(sender, CheckBox)
            If chkAll.Checked = True Then
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = True
                Next
            Else
                For iIndx = 0 To gvattach.Rows.Count - 1
                    chkField = gvattach.Rows(iIndx).FindControl("chkSelect")
                    chkField.Checked = False
                Next
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "chkSelectAll_CheckedChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub GetAttachFile(ByVal sTrNo As String)
        Dim dRow As DataRow
        Dim dt, dt1 As New DataTable
        Try
            dt.Columns.Add("FilePath")
            dt.Columns.Add("FileName")
            dt.Columns.Add("Extension")
            dt.Columns.Add("CreatedOn")

            dt1 = objFxdAsst.BindAttachFiles(sSession.AccessCode, sSession.AccessCodeID, sTrNo)
            If dt1.Rows.Count > 0 Then
                For i = 0 To dt1.Rows.Count - 1
                    dRow = dt.NewRow()
                    dRow("FilePath") = ""
                    dRow("FileName") = dt1.Rows(i)("pge_Orignalfilename")
                    dRow("Extension") = dt1.Rows(i)("pge_ext")
                    dRow("CreatedOn") = objGen.FormatDtForRDBMS(dt1.Rows(i)("pge_createdon"), "D")
                    dt.Rows.Add(dRow)
                Next
            End If

            gvattach.DataSource = dt
            gvattach.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetAttachFile" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnView_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnView.Click
        Dim iCabinetID, iSubCabinetID, iFolderID As Integer
        Dim oSelectedCabID, oSelectedSubCabID, oSelectedFolID, oSelectedChecksIDs, oSelectedIndexID As Object
        Dim sSelectedChecksIDs As String = ""
        Dim dt As New DataTable
        Try
            If DrpItemCode.SelectedIndex > 0 Then
                If gvattach.Rows.Count > 0 Then
                    iCabinetID = objIndex.GetCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, drpAstype.SelectedItem.Text)
                    iSubCabinetID = objIndex.GetSubCabinetID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iCabinetID, "Fixed Asset Master")
                    iFolderID = objIndex.GetFolderID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, iSubCabinetID, DrpItemCode.SelectedItem.Text)

                    dt = objFxdAsst.GetBaseID(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iCabinetID, iSubCabinetID, iFolderID)
                    If dt.Rows.Count > 0 Then
                        For i = 0 To dt.Rows.Count - 1
                            sSelectedChecksIDs = sSelectedChecksIDs & "," & dt.Rows(i)("PGE_BASENAME")
                        Next
                    End If

                    If sSelectedChecksIDs.StartsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(0, 1)
                    End If
                    If sSelectedChecksIDs.EndsWith(",") Then
                        sSelectedChecksIDs = sSelectedChecksIDs.Remove(Len(sSelectedChecksIDs) - 1, 1)
                    End If

                    oSelectedCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iCabinetID))
                    oSelectedSubCabID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iSubCabinetID))
                    oSelectedFolID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(iFolderID))
                    oSelectedChecksIDs = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(sSelectedChecksIDs))
                    oSelectedIndexID = HttpUtility.UrlDecode(objclsEDICTGeneral.EncryptQueryString(0))

                    Response.Redirect(String.Format("~/Viewer/ImageView.aspx?ImagePath={0}&SelId={1}&SelectedChecksIDs={2}&SelectedCabID={3}&SelectedSubCabID={4}&SelectedFolID={5}&SelectedDocTypeID={6}&SelectedKWID={7}&SelectedDescID={8}&SelectedFrmtID={9}&SelectedCrByID={10}&SelectedIndexID={11}", "", "", oSelectedChecksIDs, oSelectedCabID, oSelectedSubCabID, oSelectedFolID, "", "", "", "", "", oSelectedIndexID), False)
                Else
                    lblError.Text = "No Attachments to view"
                    Exit Sub
                End If
            Else
                lblError.Text = "Select Existing Asset Item COde No"
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnView_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ImgBtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles ImgBtnBack.Click
        Try
            lblError.Text = ""

            Response.Redirect(String.Format("~/FixedAsset/AssetRegister.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ImgBtnBack_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlDivision_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDivision.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDivision.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, ddlDivision.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlDeptmnt.DataSource = dt
            ddlDeptmnt.DataTextField = "LS_Description"
            ddlDeptmnt.DataValueField = "LS_ID"
            ddlDeptmnt.DataBind()
            ddlDeptmnt.Items.Insert(0, "Select ")


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlDeptmnt_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDeptmnt.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDeptmnt.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, ddlDeptmnt.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlBay.DataSource = dt
            ddlBay.DataTextField = "LS_Description"
            ddlBay.DataValueField = "LS_ID"
            ddlBay.DataBind()
            ddlBay.Items.Insert(0, "Select ")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDeptmnt_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnGo_Click(sender As Object, e As EventArgs) Handles btnGo.Click
        Dim slocation, sDivision, sDeptmnt, sBay As String
        If ddlLocatn.SelectedIndex > 0 Then
            slocation = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, ddlLocatn.SelectedValue, ddlCustomerName.SelectedValue)
        End If

        If ddlDivision.SelectedIndex > 0 Then
            sDivision = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, ddlDivision.SelectedValue, ddlCustomerName.SelectedValue)
            sDivision = "/" & sDivision
        End If
        If ddlDeptmnt.SelectedIndex > 0 Then
            sDeptmnt = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, ddlDeptmnt.SelectedValue, ddlCustomerName.SelectedValue)
            sDeptmnt = "/" & sDeptmnt
        End If

        If ddlBay.SelectedIndex > 0 Then
            sBay = objFxdAsst.LoadLevelCode(sSession.AccessCode, sSession.AccessCodeID, ddlBay.SelectedValue, ddlCustomerName.SelectedValue)
            sBay = "/" & sBay
        End If
        Dim sCode As String = slocation & sDivision & sDeptmnt & sBay & "/" & txtbxAstCode.Text
        txtCode.Text = sCode
        Try

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGo_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnAddSuplier_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddSuplier.Click
        Try
            ' txtname.Text = ""
            lblid.Text = 0
            'lblheadingtext.Text = "Heading"
            ' lblname.Text = "Heading name"
            lblModelError.Text = ""
            'iseletedvalue = 1
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddSuplier_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditSuplier_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditSuplier.Click
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            '  txtname.Text = ""
            If ddlSuplierName.SelectedIndex > 0 Then
                'txtname.Text = ddlSuplierName.SelectedItem.Text
                lblid.Text = ddlSuplierName.SelectedValue
                ' lblname.Text = "Heading name"

            Else
                lblCustomerValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
                lblModelError.Text = lblCustomerValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditSuplier_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSavedetails_Click(sender As Object, e As EventArgs) Handles btnSavedetails.Click

        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""

            If txtSupplierName.Text.Trim <> "" Then

                If lblid.Text = 0 Then
                    If txtSupplierName.Text <> "" Then
                        bCheck = objFxdAsst.LoadSupplier(sSession.AccessCode, sSession.AccessCodeID, txtSupplierName.Text)
                        If bCheck = True Then
                            lblError.Text = "Entred Supplier is Already Exist, Please Enter Diffrent Supplier"
                            Exit Sub
                        End If
                    End If
                End If

                If Val(lblid.Text) <> 0 Then
                    objFxdAsst.SUP_ID = lblid.Text
                Else
                    objFxdAsst.SUP_ID = 0
                End If

                objFxdAsst.SUP_Name = txtSupplierName.Text
                objFxdAsst.SUP_Code = ""
                objFxdAsst.SUP_ContactPerson = txtContactPerson.Text
                objFxdAsst.SUP_Address = txtAddress.Text
                objFxdAsst.SUP_PhoneNo = txtPhoneNo.Text
                objFxdAsst.SUP_Fax = txtFAX.Text
                objFxdAsst.SUP_Email = txtEmail.Text
                objFxdAsst.SUP_Fax = txtFAX.Text
                objFxdAsst.SUP_Website = txtWebsite.Text
                objFxdAsst.SUP_CRBY = sSession.UserID
                objFxdAsst.SUP_CRON = DateTime.Today
                objFxdAsst.SUP_STATUS = "W"
                objFxdAsst.SUP_CompID = sSession.AccessCodeID
                objFxdAsst.SUP_IPAddress = sSession.IPAddress

                Arr = objFxdAsst.SaveSupplierDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, objFxdAsst)
                lblCustomerValidationMsg.Text = "Successfully Saved." : lblError.Text = lblCustomerValidationMsg.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalAdditionValidation').modal('show');", True)
            End If

            loadExistingSupplier()
            ddlSuplierName_SelectedIndexChanged(sender, e)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSavedetails_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub loadExistingSupplier()
        Dim dt As New DataTable
        Try

            dt = objFxdAsst.loadExistingSupplier(sSession.AccessCode, sSession.AccessCodeID, 0)

            ddlSuplierName.DataTextField = "SUP_Name"
            ddlSuplierName.DataValueField = "SUP_ID"
            ddlSuplierName.DataSource = dt
            ddlSuplierName.DataBind()
            ddlSuplierName.Items.Insert(0, "Existing Supplier")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "loadExistingSupplier" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlSuplierName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSuplierName.SelectedIndexChanged
        Dim dtSupDetails As New DataTable
        Try
            lblError.Text = ""
            If ddlSuplierName.SelectedIndex > 0 Then
                dtSupDetails = objFxdAsst.showSupDetails(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, ddlSuplierName.SelectedValue)
            End If

            If dtSupDetails.Rows.Count > 0 Then
                For i = 0 To dtSupDetails.Rows.Count - 1

                    If dtSupDetails.Rows(i)("SUP_ContactPerson") = "" Then
                        txtbxConPerson.Text = ""
                    Else
                        txtbxConPerson.Text = dtSupDetails.Rows(i)("SUP_ContactPerson")
                    End If

                    If dtSupDetails.Rows(i)("SUP_Address") = "" Then
                        txtbxAddress.Text = ""
                    Else
                        txtbxAddress.Text = dtSupDetails.Rows(i)("SUP_Address")
                    End If

                    If dtSupDetails.Rows(i)("SUP_PhoneNo") = "" Then
                        txtbxPhoneNo.Text = ""
                    Else
                        txtbxPhoneNo.Text = dtSupDetails.Rows(i)("SUP_PhoneNo")
                    End If

                    If dtSupDetails.Rows(i)("SUP_Fax") = "" Then
                        txtbxFax.Text = ""
                    Else
                        txtbxFax.Text = dtSupDetails.Rows(i)("SUP_Fax")
                    End If

                    If dtSupDetails.Rows(i)("SUP_Email") = "" Then
                        txtbxEmail.Text = ""
                    Else
                        txtbxEmail.Text = dtSupDetails.Rows(i)("SUP_Email")
                    End If

                    If dtSupDetails.Rows(i)("SUP_Website") = "" Then
                        txtbxwebsite.Text = ""
                    Else
                        txtbxwebsite.Text = dtSupDetails.Rows(i)("SUP_Website")
                    End If

                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlSuplierName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub lnkbtnInsuranceDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnInsuranceDetails.Click
        Try
            lblTab.Text = 5
            liAsset_InsuranceDetails.Attributes.Add("class", "active")
            lisupplier_Detls.Attributes.Remove("class")
            lialortment_Detls.Attributes.Remove("class")
            liWarantyAMC_Detls.Attributes.Remove("class")
            liAsset_detetion.Attributes.Remove("class")
            liAsset_Loan.Attributes.Remove("class")

            divInsuranceDetails.Attributes.Add("class", "tab-pane active")
            divAplertDtls.Attributes.Add("class", "tab-pane")
            divWrntyAMCDtls.Attributes.Add("class", "tab-pane")
            divAssetDeletion.Attributes.Add("class", "tab-pane")
            divLoanAsst.Attributes.Add("class", "tab-pane")
            divSupDtls.Attributes.Add("class", "tab-pane")

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInsuranceDetails_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub btnchangeclass_ServerClick(sender As Object, e As EventArgs) Handles btnchangeclass.ServerClick
        Dim dt As New DataTable
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangeClass').modal('show');", True)
            dt = objFxdAsst.loadAAsetType(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue, ddlAssClass.SelectedItem.Text)
            ddlChangeClass.DataTextField = "AM_Description"
            ddlChangeClass.DataValueField = "AM_ID"
            ddlChangeClass.DataSource = dt
            ddlChangeClass.DataBind()
            ddlChangeClass.Items.Insert(0, "Select Asset Class")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnUpdateClass_Click(sender As Object, e As EventArgs) Handles btnUpdateClass.Click
        Dim iChangeClassId As New Integer
        Try
            lblmodalError.Text = ""
            If ddlChangeClass.SelectedIndex = 0 And txtchangeAstAge.Text = "" Then
                lblmodalError.Text = "Select Asset Class. Or Enter Useful life of Asset"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangeClass').modal('show');", True)
                Exit Sub
            End If
            If txtRemarks.Text = "" Then
                lblmodalError.Text = "Enter Remark"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangeClass').modal('show');", True)
                txtRemarks.Focus()
                Exit Sub
            End If
            If ddlChangeClass.SelectedIndex = 0 Then
                iChangeClassId = ddlAssClass.SelectedValue
            Else
                iChangeClassId = ddlChangeClass.SelectedValue
            End If
            objFxdAsst.UpdateClass(sSession.AccessCode, sSession.AccessCodeID, ddlFinancialYear.SelectedValue, iChangeClassId, txtRemarks.Text, lblPkId.Text, ddlFinancialYear.SelectedValue, ddlAssClass.SelectedValue, txtbxAstAgeOld.Text, txtchangeAstAge.Text, sSession.UserID)
            lblCustomerValidationMsg.Text = "Successfully Updated" : lblError.Text = lblCustomerValidationMsg.Text
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalAdditionValidation').modal('show');", True)
            ddlChangeClass.SelectedIndex = 0 : txtRemarks.Text = "" : txtchangeAstAge.Text = ""
            DrpItemCode_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateClass_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlAssClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssClass.SelectedIndexChanged
        Try
            AssetNo()

        Catch ex As Exception

        End Try
    End Sub
End Class
