Imports System
Imports System.Data
Imports BusinesLayer
Public Class LocationSetUp
    Inherits System.Web.UI.Page
    Private sFormName As String = "LocationSetUp"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private Shared sSession As AllSession
    Private objAsst As New ClsAssetMaster
    Private Shared iseletedvalue As Integer = 0
    Private objLocationSetup As New ClsLocationSetup
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"
        'ImgbtnAdd.ImageUrl = "~/Images/Add24.png"

        imgbtnAddLocation.ImageUrl = "~/Images/Add16.png"
        imgbtnAddDepartment.ImageUrl = "~/Images/Add16.png"
        imgbtnDivision.ImageUrl = "~/Images/Add16.png"
        imgbtnBayi.ImageUrl = "~/Images/Add16.png"

        imgbtnEditLocation.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditDepartment.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditDivision.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditBayi.ImageUrl = "~/Images/Edit16.png"

        'Asset Classification
        imgbtnAddHeadng.ImageUrl = "~/Images/Add16.png"
        imgbtnAddSubHeadng.ImageUrl = "~/Images/Add16.png"
        ImgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnItems.ImageUrl = "~/Images/Add16.png"
        imgbtnWaiting.ImageUrl = "~/Images/Checkmark24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnRefresh.ImageUrl = "~/Images/Reresh24.png"

        imgbtnEditHeadng.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditSubHeadng.ImageUrl = "~/Images/Edit16.png"
        imgbtnEditItems.ImageUrl = "~/Images/Edit16.png"
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                imgbtnSave.ImageUrl = "~/Images/Save24.png"
                LoadCustomer()
                If sSession.CustomerID > 0 Then
                    ddlCustomerName.SelectedValue = sSession.CustomerID
                    If ddlCustomerName.SelectedIndex > 0 Then
                        btnYes_Click(sender, e)
                    End If
                End If
                BindLocation()
                BindHeading()
                '    RFVWDVAmountITAct.ControlToValidate = "txtWDVAmountITAct" : RFVWDVAmountITAct.ValidationExpression = "^[0-9]{0,10}$" : RFVWDVAmountITAct.ErrorMessage = "Numbers Only!."
                '    RFVIncmTax.ControlToValidate = "TxtIncmTax" : RFVIncmTax.ValidationExpression = "^[0-9]{0,10}$" : RFVIncmTax.ErrorMessage = "Numbers Only!."
                '    RFVResidualValue.ControlToValidate = "txtResidualValue" : RFVResidualValue.ValidationExpression = "^[0-9]{0,10}$" : RFVResidualValue.ErrorMessage = "Numbers Only!."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Function BindHeading() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                dt = objAsst.LoadHeading(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            ddlHeading.DataSource = dt
            ddlHeading.DataTextField = "AM_Description"
            ddlHeading.DataValueField = "AM_ID"
            ddlHeading.DataBind()
            ddlHeading.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindHeading" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Function BindLocation() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlCustomerName.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadLocation(sSession.AccessCode, sSession.AccessCodeID, ddlCustomerName.SelectedValue)
            End If
            ddlLocation.DataSource = dt
            ddlLocation.DataTextField = "LS_Description"
            ddlLocation.DataValueField = "LS_ID"
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocation" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
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
    Public Function BindDepartment() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objLocationSetup.LoadDepartment(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            ddlDepartment.DataSource = dt
            ddlDepartment.DataTextField = "LS_Description"
            ddlDepartment.DataValueField = "LS_ID"
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDepartment" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Function BindDivision() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            ddlDivision.DataSource = dt
            ddlDivision.DataTextField = "LS_Description"
            ddlDivision.DataValueField = "LS_ID"
            ddlDivision.DataBind()
            ddlDivision.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindDivision" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Function BindBayi() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            ddlBayi.DataSource = dt
            ddlBayi.DataTextField = "LS_Description"
            ddlBayi.DataValueField = "LS_ID"
            ddlBayi.DataBind()
            ddlBayi.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindBayi" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Private Sub btnSavedetails_Click(sender As Object, e As EventArgs) Handles btnSavedetails.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""

            'Location
            If iseletedvalue = 1 Then

                If txtname.Text.Trim <> "" Then

                    If lblid.Text = 0 Then
                        If txtname.Text <> "" Then
                            bCheck = objLocationSetup.TocheckExistLocation(sSession.AccessCode, sSession.AccessCodeID, txtname.Text, sSession.YearID, ddlCustomerName.SelectedValue, 0)
                            If bCheck = True Then

                                lblLocationSetupValidationMsg.Text = "Entered Location is Already Exist, Please Enter different Location"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblid.Text) <> 0 Then
                        objLocationSetup.LS_ID = lblid.Text
                    Else
                        objLocationSetup.LS_ID = 0
                    End If

                    objLocationSetup.LS_Description = txtname.Text
                    objLocationSetup.LS_DescCode = txtCode.Text
                    objLocationSetup.LS_Code = 0
                    objLocationSetup.LS_LevelCode = 0
                    objLocationSetup.LS_ParentID = 0

                    objLocationSetup.LS_CreatedBy = sSession.UserID
                    objLocationSetup.LS_CreatedOn = DateTime.Today
                    objLocationSetup.LS_UpdatedBy = sSession.UserID
                    objLocationSetup.LS_UpdatedOn = DateTime.Today
                    objLocationSetup.LS_ApprovedBy = sSession.UserID
                    objLocationSetup.LS_ApprovedOn = DateTime.Today
                    objLocationSetup.LS_DelFlag = "X"
                    objLocationSetup.LS_Status = "W"
                    objLocationSetup.LS_YearID = sSession.YearID
                    objLocationSetup.LS_CompID = sSession.AccessCodeID
                    objLocationSetup.LS_Opeartion = "C"
                    objLocationSetup.LS_IPAddress = sSession.IPAddress
                    objLocationSetup.LS_CustId = ddlCustomerName.SelectedValue

                    Arr = objLocationSetup.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objLocationSetup)
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                If Val(lblid.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                    lblLocationSetupValidationMsg.Text = "Location Succesfully Updated"
                    lblModelError.Text = lblLocationSetupValidationMsg.Text
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                    lblLocationSetupValidationMsg.Text = "Location Succesfully Saved"
                    lblModelError.Text = lblLocationSetupValidationMsg.Text
                End If

                BindLocation()
                ddlLocation.SelectedValue = Arr(1)

                ddlLocation_SelectedIndexChanged(sender, e)

                ' Division
            ElseIf iseletedvalue = 2 Then

                If txtname.Text.Trim <> "" Then

                    If lblid.Text = 0 Then
                        If txtname.Text <> "" Then
                            bCheck = objLocationSetup.TocheckExistLocation(sSession.AccessCode, sSession.AccessCodeID, txtname.Text, sSession.YearID, ddlCustomerName.SelectedValue, ddlLocation.SelectedValue)
                            If bCheck = True Then
                                lblLocationSetupValidationMsg.Text = "Entered Division is Already Exist, Please Enter different Division"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblid.Text) <> 0 Then
                        objLocationSetup.LS_ID = lblid.Text
                    Else
                        objLocationSetup.LS_ID = 0
                    End If
                    objLocationSetup.LS_Description = txtname.Text
                    objLocationSetup.LS_DescCode = txtCode.Text
                    objLocationSetup.LS_Code = 0
                    objLocationSetup.LS_LevelCode = 1
                    If ddlLocation.SelectedIndex = 0 Then
                        objLocationSetup.LS_ParentID = 0
                    Else
                        objLocationSetup.LS_ParentID = ddlLocation.SelectedValue
                    End If
                    objLocationSetup.LS_CreatedBy = sSession.UserID
                    objLocationSetup.LS_CreatedOn = DateTime.Today
                    objLocationSetup.LS_UpdatedBy = sSession.UserID
                    objLocationSetup.LS_UpdatedOn = DateTime.Today
                    objLocationSetup.LS_ApprovedBy = sSession.UserID
                    objLocationSetup.LS_ApprovedOn = DateTime.Today
                    objLocationSetup.LS_DelFlag = "X"
                    objLocationSetup.LS_Status = "W"
                    objLocationSetup.LS_YearID = sSession.YearID
                    objLocationSetup.LS_CompID = sSession.AccessCodeID
                    objLocationSetup.LS_Opeartion = "C"
                    objLocationSetup.LS_IPAddress = sSession.IPAddress
                    objLocationSetup.LS_CustId = ddlCustomerName.SelectedValue
                    Arr = objLocationSetup.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objLocationSetup)
                End If
                If Val(lblid.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                    lblLocationSetupValidationMsg.Text = "Division Succesfully Updated"
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                    lblLocationSetupValidationMsg.Text = "Division Succesfully Saved"
                End If

                BindDivision()
                ddlLocation_SelectedIndexChanged(sender, e)
                ddlDivision.SelectedValue = Arr(1)

                ddlDivision_SelectedIndexChanged(sender, e)

                'Department
            ElseIf iseletedvalue = 3 Then

                If txtname.Text.Trim <> "" Then

                    If lblid.Text = 0 Then
                        If txtname.Text <> "" Then
                            bCheck = objLocationSetup.TocheckExistLocation(sSession.AccessCode, sSession.AccessCodeID, txtname.Text, sSession.YearID, ddlCustomerName.SelectedValue, ddlDivision.SelectedValue)
                            If bCheck = True Then
                                lblLocationSetupValidationMsg.Text = "Entered Department is Already Exist, Please Enter different Department"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblid.Text) <> 0 Then
                        objLocationSetup.LS_ID = lblid.Text
                    Else
                        objLocationSetup.LS_ID = 0
                    End If

                    objLocationSetup.LS_Description = txtname.Text
                    objLocationSetup.LS_DescCode = txtCode.Text
                    objLocationSetup.LS_Code = 0
                    objLocationSetup.LS_LevelCode = 2
                    If ddlDivision.SelectedIndex = 0 Then
                        objLocationSetup.LS_ParentID = 0
                    Else
                        objLocationSetup.LS_ParentID = ddlDivision.SelectedValue
                    End If

                    objLocationSetup.LS_CreatedBy = sSession.UserID
                    objLocationSetup.LS_CreatedOn = DateTime.Today
                    objLocationSetup.LS_UpdatedBy = sSession.UserID
                    objLocationSetup.LS_UpdatedOn = DateTime.Today
                    objLocationSetup.LS_ApprovedBy = sSession.UserID
                    objLocationSetup.LS_ApprovedOn = DateTime.Today
                    objLocationSetup.LS_DelFlag = "X"
                    objLocationSetup.LS_Status = "W"
                    objLocationSetup.LS_YearID = sSession.YearID
                    objLocationSetup.LS_CompID = sSession.AccessCodeID
                    objLocationSetup.LS_Opeartion = "C"
                    objLocationSetup.LS_IPAddress = sSession.IPAddress
                    objLocationSetup.LS_CustId = ddlCustomerName.SelectedValue
                    Arr = objLocationSetup.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objLocationSetup)
                End If
                If Val(lblid.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                    lblLocationSetupValidationMsg.Text = "Department Succesfully Updated"
                    lblModelError.Text = lblLocationSetupValidationMsg.Text
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                    lblLocationSetupValidationMsg.Text = "Department Succesfully Saved"
                End If
                lblModelError.Text = lblLocationSetupValidationMsg.Text

                BindDepartment()
                ddlDivision_SelectedIndexChanged(sender, e)
                ddlDepartment.SelectedValue = Arr(1)
                ddlDepartment_SelectedIndexChanged(sender, e)

                'Bayi
            ElseIf iseletedvalue = 4 Then

                If txtname.Text.Trim <> "" Then

                    If lblid.Text = 0 Then
                        If txtname.Text <> "" Then
                            bCheck = objLocationSetup.TocheckExistLocation(sSession.AccessCode, sSession.AccessCodeID, txtname.Text, sSession.YearID, ddlCustomerName.SelectedValue, ddlDepartment.SelectedValue)
                            If bCheck = True Then
                                lblLocationSetupValidationMsg.Text = "Entered Bay is Already Exist, Please Enter different Bay"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblid.Text) <> 0 Then
                        objLocationSetup.LS_ID = lblid.Text
                    Else
                        objLocationSetup.LS_ID = 0
                    End If
                    objLocationSetup.LS_Description = txtname.Text
                    objLocationSetup.LS_DescCode = txtCode.Text
                    objLocationSetup.LS_Code = 0
                    objLocationSetup.LS_LevelCode = 3
                    If ddlDepartment.SelectedIndex = 0 Then
                        objLocationSetup.LS_ParentID = 0
                    Else
                        objLocationSetup.LS_ParentID = ddlDepartment.SelectedValue
                    End If
                    objLocationSetup.LS_CreatedBy = sSession.UserID
                    objLocationSetup.LS_CreatedOn = DateTime.Today
                    objLocationSetup.LS_UpdatedBy = sSession.UserID
                    objLocationSetup.LS_UpdatedOn = DateTime.Today
                    objLocationSetup.LS_ApprovedBy = sSession.UserID
                    objLocationSetup.LS_ApprovedOn = DateTime.Today
                    objLocationSetup.LS_DelFlag = "X"
                    objLocationSetup.LS_Status = "W"
                    objLocationSetup.LS_YearID = sSession.YearID
                    objLocationSetup.LS_CompID = sSession.AccessCodeID
                    objLocationSetup.LS_Opeartion = "C"
                    objLocationSetup.LS_IPAddress = sSession.IPAddress
                    objLocationSetup.LS_CustId = ddlCustomerName.SelectedValue
                    Arr = objLocationSetup.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objLocationSetup)

                    If Val(lblid.Text) <> 0 Then
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                        lblLocationSetupValidationMsg.Text = "Bay Succesfully Updated"
                    Else
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                        lblLocationSetupValidationMsg.Text = "Bay Succesfully Saved"
                    End If

                    BindBayi()
                    ddlDepartment_SelectedIndexChanged(sender, e)
                    ddlBayi.SelectedValue = Arr(1)
                End If
            End If
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Location Setup", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSavedetails_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCustomerName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCustomerName.SelectedIndexChanged
        Try
            If sSession.CustomerID <> 0 Then
                If ddlCustomerName.SelectedIndex > 0 Then
                    lblModal.Text = "Do you wish to change Customer?Click Yes to change."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            Else
                If ddlCustomerName.SelectedIndex > 0 Then
                    sSession.CustomerID = ddlCustomerName.SelectedValue
                    Session("AllSession") = sSession
                    btnYes_Click(sender, e)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustomerName_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
        Try
            sSession.CustomerID = ddlCustomerName.SelectedValue
            Session("AllSession") = sSession
            ddlDepartment.Items.Clear() : ddlDivision.Items.Clear() : ddlBayi.Items.Clear()
            BindLocation()
            BindHeading()
            'ddlLocation_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
        Try
            lblError.Text = ""
            If sSession.CustomerID <> 0 Then
                ddlCustomerName.SelectedValue = sSession.CustomerID
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnAddLocation_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddLocation.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            lblid.Text = 0
            txtname.Text = ""
            txtCode.Text = ""
            lblheadingtext.Text = "Location"
            lblname.Text = "Location name"
            lblCode.Text = "Location Code"
            lblModelError.Text = ""
            iseletedvalue = 1
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddLocation_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnAddDepartment_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddDepartment.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlLocation.SelectedIndex = 0 Then
                lblError.Text = "Select Location"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDivision.SelectedIndex = 0 Then
                lblError.Text = "Select Division"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            lblid.Text = 0
            txtname.Text = ""
            txtCode.Text = ""
            lblheadingtext.Text = "Department"
            lblname.Text = "Department name"
            lblCode.Text = "Department Code"
            lblModelError.Text = ""
            iseletedvalue = 3
            If ddlLocation.SelectedIndex <> 0 Then
                lblHeading.Text = ddlLocation.SelectedItem.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddDepartment_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnDivision_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnDivision.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlLocation.SelectedIndex = 0 Then
                lblError.Text = "Select Location"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            lblid.Text = 0
            txtname.Text = ""
            txtCode.Text = ""
            lblheadingtext.Text = "Division"
            lblname.Text = "Division Name"
            lblCode.Text = "Division Code"
            lblModelError.Text = ""
            iseletedvalue = 2
            If ddlLocation.SelectedIndex > 0 Then
                lblHeading.Text = ddlLocation.SelectedItem.Text
            End If
            If ddlDepartment.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDepartment.SelectedItem.Text
            End If
            If ddlDivision.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDepartment.SelectedItem.Text + ddlDivision.SelectedItem.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnDivision_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnBayi_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBayi.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlLocation.SelectedIndex = 0 Then
                lblError.Text = "Select Location"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDivision.SelectedIndex = 0 Then
                lblError.Text = "Select Division"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlDepartment.SelectedIndex = 0 Then
                lblError.Text = "Select Department"
                lblLocationSetupValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalLocationSetupValidation').modal('show');", True)
                Exit Sub
            End If


            lblid.Text = 0
            txtname.Text = ""
            txtCode.Text = ""
            lblheadingtext.Text = "Bay"
            lblname.Text = "Bay Name"
            lblCode.Text = "Bay Code"
            lblModelError.Text = ""
            iseletedvalue = 4
            If ddlLocation.SelectedIndex > 0 Then
                lblHeading.Text = ddlLocation.SelectedItem.Text
            End If
            If ddlDepartment.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDepartment.SelectedItem.Text
            End If
            If ddlDivision.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDivision.SelectedItem.Text
            End If

            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBayi_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLocation.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlLocation.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadDivision(sSession.AccessCode, sSession.AccessCodeID, ddlLocation.SelectedValue, ddlCustomerName.SelectedValue)
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLocation_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlDepartment_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDepartment.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlDepartment.SelectedIndex > 0 Then
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, ddlDepartment.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objLocationSetup.LoadBayi(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlBayi.DataSource = dt
            ddlBayi.DataTextField = "LS_Description"
            ddlBayi.DataValueField = "LS_ID"
            ddlBayi.DataBind()
            ddlBayi.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDepartment_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
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

            ddlDepartment.DataSource = dt
            ddlDepartment.DataTextField = "LS_Description"
            ddlDepartment.DataValueField = "LS_ID"
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDivision_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    'Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
    '    Dim chkSelect As New CheckBox
    '    Dim iCount As Integer
    '    Dim lblDescID As New Label
    '    Dim sstatus As String
    '    Try

    '        lblError.Text = ""

    '        If txtResidualValue.Text = "" Then
    '            lblError.Text = "Enter Residual Value(%)"
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Residual Value(%)','', 'success');", True)
    '            Exit Sub
    '        End If
    '        If ddlItems.SelectedIndex > 0 Then
    '            objAsst.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, ddlItems.SelectedValue, "W", sSession.UserID, sSession.IPAddress)
    '            lblError.Text = "Successfully Approved."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
    '        Else
    '            lblError.Text = "Select Asset Type."
    '            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Asset Type','', 'info');", True)
    '        End If

    '        sstatus = objAsst.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlItems.SelectedValue, ddlCustomerName.SelectedValue)
    '        If sstatus <> "" Then
    '            If sstatus = "X" Then
    '                lblStatus.Text = "Waiting For Approval"
    '                'imgbtnWaiting.Visible = True
    '                imgbtnSave.Visible = True
    '            End If
    '            If sstatus = "A" Then
    '                lblStatus.Text = "Approved"
    '                imgbtnWaiting.Visible = False
    '                imgbtnSave.Visible = False
    '            End If
    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click")
    '    End Try
    'End Sub

    Private Sub imgbtnEditLocation_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditLocation.Click
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            txtname.Text = ""
            If ddlLocation.SelectedIndex > 0 Then
                txtname.Text = ddlLocation.SelectedItem.Text
                lblid.Text = ddlLocation.SelectedValue
                txtCode.Text = objLocationSetup.LoadCode(sSession.AccessCode, sSession.AccessCodeID, ddlLocation.SelectedValue)
                lblname.Text = "Location name"
                lblCode.Text = "Location Code"
                iseletedvalue = 1
            Else
                lblLocationSetupValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalLocationSetupValidation').modal('show');", True)
                lblModelError.Text = lblLocationSetupValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditLocation_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditDivision_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditDivision.Click
        Try
            lblError.Text = ""
            txtname.Text = ""
            lblModelError.Text = ""
            lblheadingtext.Text = "Division"
            lblname.Text = "Division name"
            lblCode.Text = "Division Code"
            If ddlLocation.SelectedIndex > 0 Then
                lblHeading.Text = ddlLocation.SelectedItem.Text
            End If
            If ddlDivision.SelectedIndex > 0 Then
                txtname.Text = ddlDivision.SelectedItem.Text
                lblid.Text = ddlDivision.SelectedValue
                txtCode.Text = objLocationSetup.LoadCode(sSession.AccessCode, sSession.AccessCodeID, ddlDivision.SelectedValue)
                iseletedvalue = 2
            Else
                lblLocationSetupValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalLocationSetupValidation').modal('show');", True)
                lblModelError.Text = lblLocationSetupValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditDivision_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditDepartment_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditDepartment.Click
        Try
            lblError.Text = ""
            txtname.Text = ""
            lblModelError.Text = ""
            lblheadingtext.Text = "Department"
            lblname.Text = "Department name"
            lblCode.Text = "Department Code"
            If ddlLocation.SelectedIndex > 0 Then
                lblHeading.Text = ddlLocation.SelectedItem.Text
            End If
            If ddlDivision.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDivision.SelectedItem.Text
            End If
            If ddlDepartment.SelectedIndex > 0 Then
                txtname.Text = ddlDepartment.SelectedItem.Text
                lblid.Text = ddlDepartment.SelectedValue
                txtCode.Text = objLocationSetup.LoadCode(sSession.AccessCode, sSession.AccessCodeID, ddlDepartment.SelectedValue)
                iseletedvalue = 3
            Else
                lblLocationSetupValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalLocationSetupValidation').modal('show');", True)
                lblModelError.Text = lblLocationSetupValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditDepartment_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditBayi_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditBayi.Click
        Try
            lblError.Text = ""
            lblModelError.Text = ""
            txtname.Text = ""
            lblheadingtext.Text = "Bay"
            lblname.Text = "Bay name"
            lblCode.Text = "Bay Code"
            If ddlLocation.SelectedIndex > 0 Then
                lblHeading.Text = ddlLocation.SelectedItem.Text
            End If
            If ddlDivision.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDivision.SelectedItem.Text
            End If
            If ddlDepartment.SelectedIndex > 0 Then
                lblHeading.Text = lblHeading.Text + ddlDepartment.SelectedItem.Text
            End If
            If ddlBayi.SelectedIndex > 0 Then
                txtname.Text = ddlBayi.SelectedItem.Text
                lblid.Text = ddlBayi.SelectedValue
                txtCode.Text = objLocationSetup.LoadCode(sSession.AccessCode, sSession.AccessCodeID, ddlBayi.SelectedValue)
                iseletedvalue = 4
            Else
                lblLocationSetupValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalLocationSetupValidation').modal('show');", True)
                lblModelError.Text = lblLocationSetupValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheading').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditBayi_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    'Asset Classification
    Public Function BindSubHeading() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objAsst.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            ddlsubheading.DataSource = dt
            ddlsubheading.DataTextField = "AM_Description"
            ddlsubheading.DataValueField = "AM_ID"
            ddlsubheading.DataBind()
            ddlsubheading.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindSubHeading" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    Public Function BindItems() As DataTable
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            dt = objAsst.LoadItems(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            ddlItems.DataSource = dt
            ddlItems.DataTextField = "AM_Description"
            ddlItems.DataValueField = "AM_ID"
            ddlItems.DataBind()
            ddlItems.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindItems" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Function
    'Private Sub LoadAssetTypes()
    '    Dim dt As New DataTable
    '    Try
    '  dt = objAsst.LoadAssets(sSession.AccessCode, sSession.AccessCodeID)
    '        ddlAssetType.DataSource = dt
    '        ddlAssetType.DataTextField = "GL_Desc"
    '        ddlAssetType.DataValueField = "GL_ID"
    '        ddlAssetType.DataBind()
    '        ddlAssetType.Items.Insert(0, "Select AssetType")
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub


    'Private Sub ddlAssetType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAssetType.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Try
    '        lblError.Text = ""
    '        If ddlAssetType.SelectedIndex > 0 Then
    '            dt = objAsst.AssetRetrieve(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, ddlAssetType.SelectedValue)
    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    ddlAssetType.SelectedValue = dt.Rows(i)("AM_AssetID")
    '                    txtdeprcnrate.Text = dt.Rows(i)("AM_Deprate")
    '                    If IsDBNull(dt.Rows(0).Item("AM_ITRate")) = False Then
    '                        TxtIncmTax.Text = dt.Rows(i)("AM_ITRate")
    '                    Else
    '                        TxtIncmTax.Text = ""
    '                        txtdeprcnrate.Text = ""
    '                    End If
    '                    txtResidualValue.Text = dt.Rows(i)("AM_ResidualValue")
    '                    imgbtnSave.ImageUrl = "~/Images/Update24.png"
    '                Next
    '            Else
    '                TxtIncmTax.Text = ""
    '                txtdeprcnrate.Text = ""
    '                'txtResidualValue.Text = ""
    '            End If
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub




    'Protected Sub ddlDesc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDesc.SelectedIndexChanged
    '    Dim dt As New DataTable
    '    Try
    '        lblError.Text = "" : lblGeneralMasterStatus.Text = "" : txtDesc.Text = "" : txtNotes.Text = ""
    '        If ddlDesc.SelectedIndex > 0 Then

    '            If sMasterName = 30 Then
    '                dt = objMaster.GetDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName, 1)
    '                txtdeprcnrate.Visible = True
    '                lblDepRate.Visible = True
    '                txtDesc.Text = ""
    '                txtNotes.Text = ""

    '                txtDesc.Text = ddlDesc.SelectedValue

    '            Else
    '       dt = objMaster.GetDescriptionDetails(sSession.AccessCode, sSession.AccessCodeID, ddlDesc.SelectedValue, sMasterName, 0)
    '                txtdeprcnrate.Visible = False
    '                lblDepRate.Visible = False
    '                If dt.Rows.Count > 0 Then
    '                    If IsDBNull(dt.Rows(0).Item("Mas_Desc")) = False Then
    '                        txtDesc.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Desc")))
    '                    End If
    '                    If IsDBNull(dt.Rows(0).Item("Mas_Remarks")) = False Then
    '                        txtNotes.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Remarks")))
    '                    End If
    '                    If IsDBNull(dt.Rows(0).Item("Mas_DepRate")) = False Then
    '                        txtdeprcnrate.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_DepRate")))
    '                    End If
    '                    If IsDBNull(dt.Rows(0).Item("Mas_DelFlag")) = False Then
    '                        sGMFlag = dt.Rows(0).Item("Mas_DelFlag")
    '                    End If
    '                End If
    '            End If


    '            'If dt.Rows(0).Item("Mas_master") = 30 Then
    '            '    txtdeprcnrate.Visible = True
    '            '    lblDepRate.Visible = True
    '            'Else
    '            '    txtdeprcnrate.Visible = False
    '            '            lblDepRate.Visible = False
    '            '        End If
    '            '        If dt.Rows.Count > 0 Then
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_Desc")) = False Then
    '            '                txtDesc.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Desc")))
    '            '            End If
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_Remarks")) = False Then
    '            '                txtNotes.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_Remarks")))
    '            '            End If
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_DepRate")) = False Then
    '            '                txtdeprcnrate.Text = objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("Mas_DepRate")))
    '            '            End If
    '            '            If IsDBNull(dt.Rows(0).Item("Mas_DelFlag")) = False Then
    '            '                sGMFlag = dt.Rows(0).Item("Mas_DelFlag")
    '            '            End If
    '            '        End If


    '            If sGMFlag = "W" Then
    '                lblGeneralMasterStatus.Text = "Waiting for Approval"
    '                'If sGMSave = "YES" Then
    '                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
    '                'End If
    '            ElseIf sGMFlag = "D" Then
    '                lblGeneralMasterStatus.Text = "De-Activated"
    '                imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
    '            Else
    '                lblGeneralMasterStatus.Text = "Activated"
    '                ' If sGMSave = "YES" Then
    '                imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
    '                'End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw
    '        'lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        'Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlDesc_SelectedIndexChanged")
    '    End Try
    'End Sub
    Private Sub imgbtnAddHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddHeadng.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            txtnames.Text = ""
            lblids.Text = 0
            lblheadingtexts.Text = "Heading"
            lblnames.Text = "Heading name"
            lblModelErrors.Text = ""
            iseletedvalue = 1
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheadings').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddHeadng_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnAddSubHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAddSubHeadng.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlHeading.SelectedIndex = 0 Then
                lblError.Text = "Select Heading"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            lblids.Text = 0
            txtnames.Text = ""
            lblheadingtexts.Text = "Sub Heading"
            lblnames.Text = "Sub Heading name"
            lblModelErrors.Text = ""
            iseletedvalue = 2
            If ddlHeading.SelectedIndex > 0 Then
                lblHeadings.Text = ddlHeading.SelectedItem.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheadings').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddSubHeadng_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnItems_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnItems.Click
        Try
            lblError.Text = ""

            If ddlCustomerName.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlHeading.SelectedIndex = 0 Then
                lblError.Text = "Select Heading"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If

            If ddlsubheading.SelectedIndex = 0 Then
                lblError.Text = "Select Sub Heading Name"
                lblGeneralMasterDetailsValidationMsg.Text = lblError.Text
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                Exit Sub
            End If
            txtWDVAmountITAct.Text = ""
            TxtIncmTax.Text = ""
            txtResidualValue.Text = ""
            lblids.Text = 0

            txtnames.Text = ""
            lblheadingtexts.Text = "Asset"
            lblnames.Text = "Asset Class"
            lblModelErrors.Text = ""
            iseletedvalue = 3
            If ddlHeading.SelectedIndex > 0 Then
                lblHeadings.Text = ddlHeading.SelectedItem.Text
            End If
            If ddlsubheading.SelectedIndex > 0 Then
                lblHeadings.Text = lblHeadings.Text + ddlsubheading.SelectedItem.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheadings').modal('show');", True)

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnItems_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlItems_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlItems.SelectedIndexChanged
        Dim dt As New DataTable
        Dim objGen As New clsGRACeGeneral
        Dim sstatus As String = ""
        Try
            lblError.Text = ""
            If ddlItems.SelectedIndex > 0 Then
                dt = objAsst.GetItemDetails(sSession.AccessCode, sSession.AccessCodeID, ddlItems.SelectedValue, ddlCustomerName.SelectedValue)
            End If
            If ddlItems.SelectedIndex > 0 Then
                pnlRate.Visible = True
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0).Item("AM_WDVITAct")) = False Then
                        If Val(objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_WDVITAct")))) = 0 Then
                            txtWDVAmountITAct.Text = ""
                        Else
                            txtWDVAmountITAct.Text = Val(objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_WDVITAct"))))
                        End If
                    End If
                    If IsDBNull(dt.Rows(0).Item("AM_ITRate")) = False Then
                        If Val(objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_ITRate")))) = 0 Then
                            TxtIncmTax.Text = ""
                        Else
                            TxtIncmTax.Text = Val(objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_ITRate"))))
                        End If
                    End If
                    If IsDBNull(dt.Rows(0).Item("AM_ResidualValue")) = False Then
                        If Val(objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_ResidualValue")))) = 0 Then
                            txtResidualValue.Text = ""
                        Else
                            txtResidualValue.Text = Val(objGen.ReplaceSafeSQL(Trim(dt.Rows(0).Item("AM_ResidualValue"))))
                        End If
                    End If
                    If TxtIncmTax.Text <> "" And txtResidualValue.Text <> "" Then
                        imgbtnWaiting.Visible = True
                    End If

                Else
                    txtWDVAmountITAct.Text = ""
                    TxtIncmTax.Text = ""
                    txtResidualValue.Text = ""
                    imgbtnWaiting.Visible = False
                End If
            Else
                pnlRate.Visible = False
            End If
            If ddlItems.SelectedIndex > 0 Then
                sstatus = objAsst.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlItems.SelectedValue, ddlCustomerName.SelectedValue)
            End If

            'If TxtIncmTax.Text <> "" And txtResidualValue.Text <> "" Then
            If sstatus = "X" Then
                lblStatus.Text = "Waiting For Approval"
            End If
            If sstatus = "A" Then
                lblStatus.Text = "Approved"
            End If
            'End If

            If sstatus <> "" Then
                If sstatus = "X" Then
                    '  lblStatus.Text = "Waiting For Approval"
                    'imgbtnWaiting.Visible = True
                    imgbtnSave.Visible = True
                End If
                If sstatus = "A" Then
                    ' lblStatus.Text = "Approved"
                    imgbtnWaiting.Visible = False
                    imgbtnSave.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlItems_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlHeading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlHeading.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlHeading.SelectedIndex > 0 Then
                dt = objAsst.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, ddlHeading.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objAsst.LoadSUbHeading(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlsubheading.DataSource = dt
            ddlsubheading.DataTextField = "AM_Description"
            ddlsubheading.DataValueField = "AM_ID"
            ddlsubheading.DataBind()
            ddlsubheading.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlHeading_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub ddlsubheading_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlsubheading.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sStatus As String = ""
        Try
            lblError.Text = ""
            If ddlsubheading.SelectedIndex > 0 Then
                dt = objAsst.LoadItems(sSession.AccessCode, sSession.AccessCodeID, ddlsubheading.SelectedValue, ddlCustomerName.SelectedValue)
            Else
                dt = objAsst.LoadItems(sSession.AccessCode, sSession.AccessCodeID, 0, ddlCustomerName.SelectedValue)
            End If

            ddlItems.DataSource = dt
            ddlItems.DataTextField = "AM_Description"
            ddlItems.DataValueField = "AM_ID"
            ddlItems.DataBind()
            ddlItems.Items.Insert(0, "Select ")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlsubheading_SelectedIndexChanged" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnWaiting_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnWaiting.Click
        Dim chkSelect As New CheckBox
        Dim iCount As Integer
        Dim lblDescID As New Label
        Dim sstatus As String
        Try

            lblError.Text = ""

            If txtResidualValue.Text = "" Then
                lblError.Text = "Enter Residual Value(%)"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Residual Value(%)','', 'success');", True)
                Exit Sub
            End If
            If ddlItems.SelectedIndex > 0 Then
                objAsst.UpdateStatus(sSession.AccessCode, sSession.AccessCodeID, ddlItems.SelectedValue, "W", sSession.UserID, sSession.IPAddress)
                lblError.Text = "Successfully Approved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Approved','', 'info');", True)
            Else
                lblError.Text = "Select Asset Type."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Asset Type','', 'info');", True)
            End If

            sstatus = objAsst.GetStatus(sSession.AccessCode, sSession.AccessCodeID, ddlItems.SelectedValue, ddlCustomerName.SelectedValue)
            If sstatus <> "" Then
                If sstatus = "X" Then
                    lblStatus.Text = "Waiting For Approval"
                    'imgbtnWaiting.Visible = True
                    imgbtnSave.Visible = True
                End If
                If sstatus = "A" Then
                    lblStatus.Text = "Approved"
                    imgbtnWaiting.Visible = False
                    imgbtnSave.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnWaiting_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Try
            lblError.Text = ""

            If txtResidualValue.Text = "" Then
                lblError.Text = "Enter Residual Value(%)"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Residual Value(%)','', 'success');", True)
                txtResidualValue.Focus() : Exit Sub
            End If
            If TxtIncmTax.Text = "" Then
                lblError.Text = "Enter Depreciation Rate per IncomeTax(%)"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Depreciation Rate per IncomeTax(%)','', 'success');", True)
                TxtIncmTax.Focus() : Exit Sub
            End If
            If txtWDVAmountITAct.Text <> "" Then
                objAsst.AM_WDVITAct = txtWDVAmountITAct.Text
            Else
                objAsst.AM_WDVITAct = 0
            End If
            If TxtIncmTax.Text <> "" Then
                objAsst.AM_ITRate = TxtIncmTax.Text
            Else
                objAsst.AM_ITRate = 0
            End If
            If txtResidualValue.Text <> "" Then
                objAsst.AM_ResidualValue = txtResidualValue.Text
            Else
                objAsst.AM_ResidualValue = 0
            End If
            objAsst.UpdateDetails(sSession.AccessCode, sSession.AccessCodeID, objAsst.AM_WDVITAct, objAsst.AM_ITRate, objAsst.AM_ResidualValue, ddlItems.SelectedValue, ddlCustomerName.SelectedValue)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Asset Classification", "Saved", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            lblError.Text = "Succesfully Saved"
            lblGeneralMasterDetailsValidationMsg.Text = "Succesfully Saved"
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
            lblStatus.Text = "Waiting For Approval"
            imgbtnWaiting.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnRefresh_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnRefresh.Click
        Try
            lblError.Text = ""
            Response.Redirect(String.Format("~/FixedAsset/LocationSetUp.aspx?"), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnRefresh_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditHeadng.Click
        Try
            lblError.Text = ""
            lblModelErrors.Text = ""
            txtnames.Text = ""
            If ddlHeading.SelectedIndex > 0 Then
                txtnames.Text = ddlHeading.SelectedItem.Text
                lblids.Text = ddlHeading.SelectedValue
                lblnames.Text = "Heading name"
                iseletedvalue = 1
            Else
                lblGeneralMasterDetailsValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelErrors.Text = lblGeneralMasterDetailsValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheadings').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditHeadng_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub

    Private Sub imgbtnEditSubHeadng_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditSubHeadng.Click
        Try
            lblError.Text = ""
            txtnames.Text = ""
            lblModelErrors.Text = ""
            lblheadingtexts.Text = "Sub Heading"
            lblnames.Text = "Sub Heading name"
            If ddlHeading.SelectedIndex > 0 Then
                lblHeadings.Text = ddlHeading.SelectedItem.Text
            End If
            If ddlsubheading.SelectedIndex > 0 Then
                txtnames.Text = ddlsubheading.SelectedItem.Text
                lblids.Text = ddlsubheading.SelectedValue
                iseletedvalue = 2
            Else
                lblGeneralMasterDetailsValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelErrors.Text = lblGeneralMasterDetailsValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheadings').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditSubHeadng_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub imgbtnEditItems_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEditItems.Click
        Try
            lblError.Text = ""
            lblModelErrors.Text = ""
            txtnames.Text = ""
            lblheadingtexts.Text = "Items"
            lblnames.Text = "Items name"
            If ddlHeading.SelectedIndex > 0 Then
                lblHeadings.Text = ddlHeading.SelectedItem.Text
            End If
            If ddlsubheading.SelectedIndex > 0 Then
                lblHeadings.Text = lblHeadings.Text + ddlsubheading.SelectedItem.Text
            End If
            If ddlItems.SelectedIndex > 0 Then
                txtnames.Text = ddlItems.SelectedItem.Text
                lblids.Text = ddlItems.SelectedValue
                iseletedvalue = 3
            Else
                lblGeneralMasterDetailsValidationMsg.Text = "Nothing to Edit"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-danger');$('#ModalScheduleValidation').modal('show');", True)
                lblModelErrors.Text = lblGeneralMasterDetailsValidationMsg.Text
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#Modalheadings').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEditItems_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub btnSaveClass_Click(sender As Object, e As EventArgs) Handles btnSaveClass.Click
        Dim Arr() As String
        Dim bCheck As Boolean
        Try
            lblError.Text = ""
            'Heading
            If iseletedvalue = 1 Then
                If txtnames.Text.Trim <> "" Then

                    If lblids.Text = 0 Then
                        If txtnames.Text <> "" Then
                            bCheck = objAsst.LoadAsset(sSession.AccessCode, sSession.AccessCodeID, txtnames.Text, ddlCustomerName.SelectedValue, 0)
                            If bCheck = True Then
                                lblGeneralMasterDetailsValidationMsg.Text = "Entered Heading is Already Exist, Please Enter different Heading"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblids.Text) <> 0 Then
                        objAsst.AM_ID = lblids.Text
                    Else
                        objAsst.AM_ID = 0
                    End If

                    objAsst.AM_Description = txtnames.Text
                    objAsst.AM_Code = 0
                    objAsst.AM_LevelCode = 0
                    objAsst.AM_ParentID = 0

                    If txtWDVAmountITAct.Text = "" Then
                        objAsst.AM_WDVITAct = 0
                    Else
                        objAsst.AM_WDVITAct = txtWDVAmountITAct.Text
                    End If

                    If TxtIncmTax.Text = "" Then
                        objAsst.AM_ITRate = 0
                    Else
                        objAsst.AM_ITRate = TxtIncmTax.Text
                    End If

                    If txtResidualValue.Text = "" Then
                        objAsst.AM_ResidualValue = 0
                    Else
                        objAsst.AM_ResidualValue = txtResidualValue.Text
                    End If

                    objAsst.AM_CreatedBy = sSession.UserID
                    objAsst.AM_CreatedOn = DateTime.Today
                    objAsst.AM_UpdatedBy = sSession.UserID
                    objAsst.AM_UpdatedOn = DateTime.Today
                    objAsst.AM_ApprovedBy = sSession.UserID
                    objAsst.AM_ApprovedOn = DateTime.Today
                    objAsst.AM_DelFlag = "X"
                    objAsst.AM_Status = "W"
                    objAsst.AM_YearID = sSession.YearID
                    objAsst.AM_CompID = sSession.AccessCodeID
                    objAsst.AM_Opeartion = "C"
                    objAsst.AM_IPAddress = sSession.IPAddress
                    objAsst.AM_CustId = ddlCustomerName.SelectedValue

                    Arr = objAsst.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objAsst)
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                If Val(lblids.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    lblGeneralMasterDetailsValidationMsg.Text = "Heading Succesfully Updated"
                    lblModelError.Text = lblGeneralMasterDetailsValidationMsg.Text
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    lblGeneralMasterDetailsValidationMsg.Text = "Heading Succesfully Saved"
                    lblModelError.Text = lblGeneralMasterDetailsValidationMsg.Text
                End If

                BindHeading()
                ddlHeading.SelectedValue = Arr(1)

                ddlHeading_SelectedIndexChanged(sender, e)
                'Sub Heading
            ElseIf iseletedvalue = 2 Then
                If txtnames.Text.Trim <> "" Then

                    If lblids.Text = 0 Then
                        If txtnames.Text <> "" Then
                            bCheck = objAsst.LoadAsset(sSession.AccessCode, sSession.AccessCodeID, txtnames.Text, ddlCustomerName.SelectedValue, ddlHeading.SelectedValue)
                            If bCheck = True Then
                                lblGeneralMasterDetailsValidationMsg.Text = "Entered Sub Heading is Already Exist, Please Enter different Sub Heading"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblids.Text) <> 0 Then
                        objAsst.AM_ID = lblids.Text
                    Else
                        objAsst.AM_ID = 0
                    End If

                    objAsst.AM_Description = txtnames.Text
                    objAsst.AM_Code = 0
                    objAsst.AM_LevelCode = 1
                    If ddlHeading.SelectedIndex = 0 Then
                        objAsst.AM_ParentID = 0
                    Else
                        objAsst.AM_ParentID = ddlHeading.SelectedValue
                    End If

                    If txtWDVAmountITAct.Text = "" Then
                        objAsst.AM_WDVITAct = 0
                    Else
                        objAsst.AM_WDVITAct = txtWDVAmountITAct.Text
                    End If

                    If TxtIncmTax.Text = "" Then
                        objAsst.AM_ITRate = 0
                    Else
                        objAsst.AM_ITRate = TxtIncmTax.Text
                    End If

                    If txtResidualValue.Text = "" Then
                        objAsst.AM_ResidualValue = 0
                    Else
                        objAsst.AM_ResidualValue = txtResidualValue.Text
                    End If

                    objAsst.AM_CreatedBy = sSession.UserID
                    objAsst.AM_CreatedOn = DateTime.Today
                    objAsst.AM_UpdatedBy = sSession.UserID
                    objAsst.AM_UpdatedOn = DateTime.Today
                    objAsst.AM_ApprovedBy = sSession.UserID
                    objAsst.AM_ApprovedOn = DateTime.Today
                    objAsst.AM_DelFlag = "X"
                    objAsst.AM_Status = "W"
                    objAsst.AM_YearID = sSession.YearID
                    objAsst.AM_CompID = sSession.AccessCodeID
                    objAsst.AM_Opeartion = "C"
                    objAsst.AM_IPAddress = sSession.IPAddress
                    objAsst.AM_CustId = ddlCustomerName.SelectedValue
                    Arr = objAsst.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objAsst)
                End If
                If Val(lblids.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    lblGeneralMasterDetailsValidationMsg.Text = "Sub Heading Succesfully Updated"
                    lblModelError.Text = lblGeneralMasterDetailsValidationMsg.Text
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    lblGeneralMasterDetailsValidationMsg.Text = "Sub Heading Succesfully Saved"
                End If
                lblModelError.Text = lblGeneralMasterDetailsValidationMsg.Text

                BindSubHeading()
                ddlHeading_SelectedIndexChanged(sender, e)
                ddlsubheading.SelectedValue = Arr(1)
                ddlsubheading_SelectedIndexChanged(sender, e)

                'Asset Class
            ElseIf iseletedvalue = 3 Then

                If txtnames.Text.Trim <> "" Then

                    If lblids.Text = 0 Then
                        If txtnames.Text <> "" Then
                            bCheck = objAsst.LoadAsset(sSession.AccessCode, sSession.AccessCodeID, txtnames.Text, ddlCustomerName.SelectedValue, ddlsubheading.SelectedValue)
                            If bCheck = True Then
                                lblGeneralMasterDetailsValidationMsg.Text = "Entered Asset Class is Already Exist, Please Enter different Asset Class"
                                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                                Exit Sub
                            End If
                        End If
                    End If

                    If Val(lblids.Text) <> 0 Then
                        objAsst.AM_ID = lblids.Text
                    Else
                        objAsst.AM_ID = 0
                    End If
                    objAsst.AM_Description = txtnames.Text
                    objAsst.AM_Code = 0
                    objAsst.AM_LevelCode = 2
                    If ddlsubheading.SelectedIndex = 0 Then
                        objAsst.AM_ParentID = 0
                    Else
                        objAsst.AM_ParentID = ddlsubheading.SelectedValue
                    End If

                    If txtWDVAmountITAct.Text = "" Then
                        objAsst.AM_WDVITAct = 0
                    Else
                        objAsst.AM_WDVITAct = txtWDVAmountITAct.Text
                    End If

                    If TxtIncmTax.Text = "" Then
                        objAsst.AM_ITRate = 0
                    Else
                        objAsst.AM_ITRate = TxtIncmTax.Text
                    End If

                    If txtResidualValue.Text = "" Then
                        objAsst.AM_ResidualValue = 0
                    Else
                        objAsst.AM_ResidualValue = txtResidualValue.Text
                    End If

                    objAsst.AM_CreatedBy = sSession.UserID
                    objAsst.AM_CreatedOn = DateTime.Today
                    objAsst.AM_UpdatedBy = sSession.UserID
                    objAsst.AM_UpdatedOn = DateTime.Today
                    objAsst.AM_ApprovedBy = sSession.UserID
                    objAsst.AM_ApprovedOn = DateTime.Today
                    objAsst.AM_DelFlag = "X"
                    objAsst.AM_Status = "W"
                    objAsst.AM_YearID = sSession.YearID
                    objAsst.AM_CompID = sSession.AccessCodeID
                    objAsst.AM_Opeartion = "C"
                    objAsst.AM_IPAddress = sSession.IPAddress
                    objAsst.AM_CustId = ddlCustomerName.SelectedValue
                    Arr = objAsst.SaveAsset(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID, objAsst)
                End If
                If Val(lblids.Text) <> 0 Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    lblGeneralMasterDetailsValidationMsg.Text = "Asset Class Succesfully Updated"
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divExcelMsgType').addClass('alert alert-success');$('#ModalGeneralMasterDetailsValidation').modal('show');", True)
                    lblGeneralMasterDetailsValidationMsg.Text = "Asset Class Succesfully Saved"
                End If

                BindItems()
                ddlsubheading_SelectedIndexChanged(sender, e)
                ddlItems.SelectedValue = Arr(1)

                ddlItems_SelectedIndexChanged(sender, e)

                If ddlItems.SelectedIndex > 0 Then
                    pnlRate.Visible = True
                    'imgbtnSave.Visible = True : imgbtnWaiting.Visible = True
                Else
                    pnlRate.Visible = False
                    ' imgbtnSave.Visible = False : imgbtnWaiting.Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveClass_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub


End Class