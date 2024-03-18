Imports System
Imports System.Data
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Partial Class CustomerDetails
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_UserMasterDetails"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAttachments As New clsAttachments
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsAuditAssignment As New clsAuditAssignment
    Dim objclsEDICTGeneral As New clsEDICTGeneral
    Dim objCust As New clsCustDetails
    Dim objclsCabinet As New clsCabinet
    Dim objclsSubCabinet As New clsSubCabinet
    Dim objclsFolders As New clsFolders
    Private sSession As AllSession
    Private objclsOrgStructure As New clsOrgStructure
    Private Shared iAttachID As Integer
    Private Shared iDocID As Integer
    Private Shared sBackStatus As String
    'Private Shared sCDSave As String
    'Private Shared sCDReport As String
    Private Shared iCBN_NODE As Integer = 0
    Private Shared iCustCompPKId As Integer
    Private Shared iCustDirectorPKId As Integer
    Private Shared iCustPartnerPKId As Integer
    Private objclsAdminMaster As New clsAdminMaster
    Private objclsReportTemplate As New clsReportTemplate
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnAdd.ImageUrl = "~/Images/Add24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnSaveLocation.ImageUrl = "~/Images/Save24.png"
        imgbtnSaveOther.ImageUrl = "~/Images/Update24.png"
        imgbtnSaveLOE.ImageUrl = "~/Images/Save24.png"
        imgbtnSaveLOETemp.ImageUrl = "~/Images/Save24.png"
        imgbtnSaveCompliance.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdateLoction.ImageUrl = "~/Images/Update24.png"
        imgbtnUpdateLOE.ImageUrl = "~/Images/Update24.png"
        imgbtnUpdateCompliance.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnLoad.ImageUrl = "~/Images/Load24.png"
        imgbtnCustReport.Src = "~/Images/Download24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iAttachID = 0 : iCBN_NODE = 0 : iCustCompPKId = 0 : iCustDirectorPKId = 0 : iCustPartnerPKId = 0
                imgbtnSaveLocation.Visible = False : imgbtnSaveOther.Visible = False : imgbtnSaveLOE.Visible = False : imgbtnSaveLOETemp.Visible = False
                imgbtnUpdate.Visible = False : imgbtnUpdateLoction.Visible = False : imgbtnUpdateLOE.Visible = False : imgbtnReport.Visible = False
                imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

                btnAddAttch.Visible = True : btnStatutoryAdd.Visible = True
                'sCDSave = "NO" : sCDReport = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPEMP", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnSave.Visible = True : btnReAdd.Visible = True : btnAddCatList.Visible = True : btnAdd.Visible = True
                '        btnAddAttch.Visible = True : btnStatutoryAdd.Visible = True
                '        sCDSave = "YES"
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        sCDReport = "YES"
                '    End If
                'End If
                liCust.Attributes.Add("class", "active") : divCustomerDetails.Attributes.Add("class", "tab-pane active")
                Me.ddlGroup.Attributes.Add("onchange", "javascript:return ValidateGroup();")
                Me.ddlExistingGroup.Attributes.Add("onchange", "javascript:return ValidateExistingGroup();")
                lblTab.Text = 1
                txtCustCode.Text = objCust.GetLatestCustomerCode(sSession.AccessCode, sSession.AccessCodeID)
                BindCustOtherDetails() : BindCustLocation() : BindLOECustomers()
                LoadExistingCust()
                LoadAllDropDown()
                Bindcustumstyles()
                BindAsgFinancialYear()
                If Request.QueryString("CustomerID") IsNot Nothing Then
                    ddlCustName.SelectedValue = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("CustomerID")))
                    ddlOtherDetailsCust.SelectedValue = ddlCustName.SelectedValue
                    ddlLocationCust.SelectedValue = ddlCustName.SelectedValue
                    ddlLOECustomers.SelectedValue = ddlCustName.SelectedValue
                    ddlLOETemplateCustomers.SelectedValue = ddlCustName.SelectedValue
                    ddlCompExistingCustomer.SelectedValue = ddlCustName.SelectedValue
                    ddlAsgExistingCustomer.SelectedValue = ddlCustName.SelectedValue
                    ddlCustName_SelectedIndexChanged(sender, e)
                    ddlOtherDetailsCust_SelectedIndexChanged(sender, e)
                    ddlLocationCust_SelectedIndexChanged(sender, e)
                    ddlLOECustomers_SelectedIndexChanged(sender, e)
                    ddlLOETemplateCustomers_SelectedIndexChanged(sender, e)
                    ddlCompExistingCustomer_SelectedIndexChanged(sender, e)
                    ddlAsgExistingCustomer_SelectedIndexChanged(sender, e)
                    lnkbtnCustomer_Click(sender, e)
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
                CustomerDetailsClientsSideValidation()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub CustomerDetailsClientsSideValidation()
        Try
            RFVICustomerName.ErrorMessage = "Enter Customer Name."
            RFVICustomerCode.ErrorMessage = "Enter Customer Code."
            RFVIndustry.ErrorMessage = "Select Industry Type." : RFVIndustry.InitialValue = "Select Industry Type"
            REVEMail.ErrorMessage = "Enter Valid E-mail." : REVEMail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            RFVDate.ErrorMessage = "Enter Date."
            REVDate.ErrorMessage = "Enter Valid Date." : REVDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            'RFVManagement.ErrorMessage = "Select Management Type." : RFVManagement.InitialValue = "Select Management Type"
            'RFVGroup.ErrorMessage = "Select Group." : RFVGroup.InitialValue = "0"
            RFVOrganization.ErrorMessage = "Select org. Type." : RFVOrganization.InitialValue = "Select Organization Type"
            REVCustRegNo.ErrorMessage = "Enter Registration No."
            'RFVBoardOfDirectors.ErrorMessage = "Select Board of Directors/Partners."
            REVCommEmail.ErrorMessage = "Enter Valid E-mail." : REVCommEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            REVEmailId.ErrorMessage = "Enter Valid E-mail." : REVEmailId.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            REVCommTele.ErrorMessage = "Enter Valid Telephone No." : REVCommTele.ValidationExpression = "^[0-9]{0,15}$"
            REVCommFax.ErrorMessage = "Enter Valid Fax No." : REVCommFax.ValidationExpression = "^[0-9]{0,15}$"
            REVCommPin.ErrorMessage = "Enter Valid Postal Code." : REVCommPin.ValidationExpression = "^[0-9]{0,15}$"
            REVTele.ErrorMessage = "Enter Valid Telephone No." : REVTele.ValidationExpression = "^[0-9]{0,15}$"
            REVFax.ErrorMessage = "Enter Valid Fax No." : REVFax.ValidationExpression = "^[0-9]{0,15}$"
            REVPin.ErrorMessage = "Enter Valid Postal Code." : REVPin.ValidationExpression = "^[0-9]{0,15}$"
            RFVStatutoryReferences.ErrorMessage = "Select Statutory References." : RFVStatutoryReferences.InitialValue = "0"
            RFVStatutoryValue.ErrorMessage = "Enter Reference."

            RFVLocationName.ErrorMessage = "Enter Location Name."
            RFVContactPerson.ErrorMessage = "Enter Contact Person Name."
            RFVLocationAddress.ErrorMessage = "Enter Location Address."
            REVContactEmail.ErrorMessage = "Enter Valid E-mail." : REVContactEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            REVContactLandLineNo.ErrorMessage = "Enter Valid Telephone No." : REVContactLandLineNo.ValidationExpression = "^[0-9]{0,15}$"
            REVContactMobileNo.ErrorMessage = "Enter Valid 10 Digit Mobile No." : REVContactMobileNo.ValidationExpression = "^[0-9]{10}$"

            RFVFrequency.ErrorMessage = "Select Frequency." : RFVFrequency.InitialValue = "Select Frequency"
            RFVTask.ErrorMessage = "Select Task." : RFVTask.InitialValue = "Select Task"
            RFVStartDate.ErrorMessage = "Enter Start Date."
            RFVDueDate.ErrorMessage = "Enter Due Date."
            REVStartDate.ErrorMessage = "Enter Valid Date." : REVStartDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            REVDueDate.ErrorMessage = "Enter Valid Date." : REVDueDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            RFVddlFunction.ErrorMessage = "Select Assignments/Tasks." : RFVddlFunction.InitialValue = "Select Assignments/Tasks"
            'RFVSubFunction2.ErrorMessage = "Select Sub Tasks." : RFVSubFunction2.InitialValue = "Select Sub Tasks"
            'RFVSubFunction.ErrorMessage = "Select Sub Tasks." : RFVSubFunction.InitialValue = ""

            RFVCatList.ErrorMessage = "Select Other Expenses." : RFVCatList.InitialValue = "Select Other Expenses"
            RFVCatCode.ErrorMessage = "Enter Code."
            REVCatCode.ErrorMessage = "Enter Valid Code." : REVCatCode.ValidationExpression = "^\d+$"
            RFVResources.ErrorMessage = "Enter Amount."
            REVResources.ErrorMessage = "Enter Valid Amount." : REVResources.ValidationExpression = "^\d+$"
            RFVCat.ErrorMessage = "Select Categories." : RFVCat.InitialValue = " Select Categories "
            RFVdays.ErrorMessage = "Enter NO. of Days."
            RFVNR.ErrorMessage = "Enter No Of Resources."
            REVReAmt.ErrorMessage = "Enter Valid Amount." : REVReAmt.ValidationExpression = "^\d+$"
            REVdays.ErrorMessage = "Only Numbers." : REVdays.ValidationExpression = "^\d+$"
            REVNR.ErrorMessage = "Only Numbers." : REVNR.ValidationExpression = "^\d+$"

            RFVGeneral.ErrorMessage = "Enter General."
            RFVDeliverable.ErrorMessage = "Enter The objective and scope of the audit."

            REVStdIntAudit.ValidationExpression = "^[\s\S]{0,8000}$" : REVStdIntAudit.ErrorMessage = "Responsibilities of the Auditor exceeded maximum size(max 8000 characters)."
            REVDeliverable.ValidationExpression = "^[\s\S]{0,8000}$" : REVDeliverable.ErrorMessage = "The objective and scope of the audit exceeded maximum size(max 8000 characters)."
            REVRoles.ValidationExpression = "^[\s\S]{0,8000}$" : REVRoles.ErrorMessage = "Reporting exceeded maximum size(max 8000 characters)."
            REVInfrastructure.ValidationExpression = "^[\s\S]{0,8000}$" : REVInfrastructure.ErrorMessage = "The responsibilities of management and identification of the applicable financial reporting framework exceeded maximum size(max 8000 characters)."
            REVGeneral.ValidationExpression = "^[\s\S]{0,8000}$" : REVGeneral.ErrorMessage = "General exceeded maximum size(max 8000 characters)."
            REVConfidential.ValidationExpression = "^[\s\S]{0,8000}$" : REVConfidential.ErrorMessage = "Non Disclosure Of Confidential Information exceeded maximum size(max 8000 characters)."

            RFVCompExistingCustomer.ErrorMessage = "Select Existing Customer" : RFVCompExistingCustomer.InitialValue = "Select Customer Name"
            RFVCompTask.ErrorMessage = "Select Types Of Service/Tasks." : RFVCompTask.InitialValue = "Select Task"
            RFVCompFrequency.ErrorMessage = "Select Frequency." : RFVCompFrequency.InitialValue = "0"
            REVCompMobileNo.ErrorMessage = "Enter Valid 10 Digit Mobile No." : REVCompMobileNo.ValidationExpression = "^[0-9]{10}$"
            REVCompEmailId.ErrorMessage = "Enter Valid E-mail." : REVCompEmailId.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            RFVDirectorName.ErrorMessage = "Enter Director Name."
            RFVDirectorDOB.ErrorMessage = "Enter DOB." : REVDirectorDOB.ErrorMessage = "Enter Valid DOB." : REVDirectorDOB.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            REVDirectorMobileNo.ErrorMessage = "Enter Valid 10 Digit Mobile No." : REVDirectorMobileNo.ValidationExpression = "^[0-9]{10}$"
            REVDirectorEmailId.ErrorMessage = "Enter Valid E-mail." : REVDirectorEmailId.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            RFVPartnerName.ErrorMessage = "Enter Partner Name."
            REVPartnerName.ValidationExpression = "^[\s\S]{0,100}$" : REVInfrastructure.ErrorMessage = "Partner Name exceeded maximum size(max 100 characters)."
            RFVPartnerDOJ.ErrorMessage = "Enter Date of Joining." : REVPartnerDOJ.ErrorMessage = "Enter Valid Date of Joining." : REVPartnerDOJ.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
            RFVPartnerPAN.ErrorMessage = "Enter Partner PAN."
            REVPartnerPAN.ValidationExpression = "^[\s\S]{0,25}$" : REVPartnerPAN.ErrorMessage = "Partner PAN exceeded maximum size(max 25 characters)."
            RFVShareOfProfit.ErrorMessage = "Enter Share Of Profit."
            'REVShareOfProfit.ValidationExpression = "^0*(100(\.0{1,2})?|[1-9][0-9]?(\.[0-9]{1,2})?|0\.(0[1-9]|[1-9][0-9]?))$" : REVShareOfProfit.ErrorMessage = "Share Of Profit shouild be less than 100."
            REVShareOfProfit.ValidationExpression = "^(100(\.00?)?|\d{1,2}(\.\d{1,2})?|0\.\d{1,3})$" : REVShareOfProfit.ErrorMessage = "Share Of Profit shouild be 0 to 100."
            RFVCapitalAmount.ErrorMessage = "Enter Capital Amount."
            REVCapitalAmount.ValidationExpression = "^[1-9]\d*(\.\d{1,2})?$" : REVCapitalAmount.ErrorMessage = "Enter valid Capital Amount."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "CustomerDetailsClientsSideValidation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub lnkbtnCustomer_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomer.Click
        Try
            lblError.Text = ""
            lblTab.Text = 1
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If ddlCustName.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnUpdate.Visible = True
                'End If
                ddlCustName_SelectedIndexChanged(sender, e)
            Else
                'If sCDSave = "YES" Then
                imgbtnSave.Visible = True
                'End If
            End If
            imgbtnSaveOther.Visible = False
            imgbtnSaveLocation.Visible = False : imgbtnUpdateLoction.Visible = False
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            imgbtnSaveLOETemp.Visible = False
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

            liCust.Attributes.Add("class", "active") : divCustomerDetails.Attributes.Add("class", "tab-pane active")
            liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            liLOE.Attributes.Remove("class") : divLOE.Attributes.Add("class", "tab-pane")
            liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomer_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnOtherDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnOtherDetails.Click
        Try
            lblError.Text = ""
            If ddlCustName.SelectedIndex = 0 AndAlso txtCustName.Text <> "" Then
                lnkbtnCustomer_Click(sender, e)
                lblError.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                lblCustomerValidationMsg.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCustName').focus();", True)
                Exit Sub
            End If
            lblTab.Text = 2
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnSaveOther.Visible = False
            If ddlOtherDetailsCust.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnSaveOther.Visible = True
                'End If
                ddlOtherDetailsCust_SelectedIndexChanged(sender, e)
            End If
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnSaveLocation.Visible = False : imgbtnUpdateLoction.Visible = False
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            imgbtnSaveLOETemp.Visible = False
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

            liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            liOther.Attributes.Add("class", "active") : divOther.Attributes.Add("class", "tab-pane active")
            liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            liLOE.Attributes.Remove("class") : divLOE.Attributes.Add("class", "tab-pane")
            liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOtherDetails_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnLocations_Click(sender As Object, e As EventArgs) Handles lnkbtnLocations.Click
        Try
            lblError.Text = ""
            If ddlCustName.SelectedIndex = 0 AndAlso txtCustName.Text <> "" Then
                lnkbtnCustomer_Click(sender, e)
                lblError.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                lblCustomerValidationMsg.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCustName').focus();", True)
                Exit Sub
            End If
            lblTab.Text = 3
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnSaveLocation.Visible = False
            If ddlLocationCust.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnSaveLocation.Visible = True
                'End If
                ddlLocationCust_SelectedIndexChanged(sender, e)
            End If
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnSaveOther.Visible = False
            imgbtnUpdateLoction.Visible = False
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            imgbtnSaveLOETemp.Visible = False
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

            liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            liLocations.Attributes.Add("class", "active") : divLocation.Attributes.Add("class", "tab-pane active")
            liLOE.Attributes.Remove("class") : divLOE.Attributes.Add("class", "tab-pane")
            liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLocations_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnLOE_Click(sender As Object, e As EventArgs) Handles lnkbtnLOE.Click
        Dim iLOESelectID As Integer
        Try
            lblError.Text = "" : iLOESelectID = 0 : iAttachID = 0
            If ddlExistingLOETemplate.SelectedIndex > 0 Then
                iLOESelectID = ddlExistingLOETemplate.SelectedValue
            End If

            If ddlCustName.SelectedIndex = 0 AndAlso txtCustName.Text <> "" Then
                lnkbtnCustomer_Click(sender, e)
                lblError.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                lblCustomerValidationMsg.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCustName').focus();", True)
                Exit Sub
            End If
            lblTab.Text = 4
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnSaveLOE.Visible = False
            LoadFinalcialYear()
            If ddlLOECustomers.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnSaveLOE.Visible = True
                'End If
                ddlLOECustomers_SelectedIndexChanged(sender, e)
            End If

            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnSaveOther.Visible = False
            imgbtnSaveLocation.Visible = False : imgbtnUpdateLoction.Visible = False
            imgbtnUpdateLOE.Visible = False
            imgbtnSaveLOETemp.Visible = False
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

            liCust.Attributes.Remove("class")
            liOther.Attributes.Remove("class")
            liLocations.Attributes.Remove("class")
            liLOETemplater.Attributes.Remove("class")

            liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            liLOE.Attributes.Add("class", "active") : divLOE.Attributes.Add("class", "tab-pane active")
            liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")

            If ddlLOECustomers.SelectedIndex > 0 AndAlso iLOESelectID > 0 Then
                ddlExistingLOE.SelectedValue = iLOESelectID
                ddlExistingLOE_SelectedIndexChanged(sender, e)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLOE_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnLOETemplate_Click(sender As Object, e As EventArgs) Handles lnkbtnLOETemplate.Click
        Dim iLOESelectID As Integer
        Try
            lblError.Text = "" : iLOESelectID = 0 : iAttachID = 0
            If ddlExistingLOE.SelectedIndex > 0 Then
                iLOESelectID = ddlExistingLOE.SelectedValue
            End If

            If ddlCustName.SelectedIndex = 0 AndAlso txtCustName.Text <> "" Then
                lnkbtnCustomer_Click(sender, e)
                lblError.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                lblCustomerValidationMsg.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCustName').focus();", True)
                Exit Sub
            End If
            lblTab.Text = 5
            LoadFinalcialYear()
            imgbtnAdd.Visible = False
            'If ddlExistingLOE.SelectedIndex = 0 Then
            '    lblCustomerValidationMsg.Text = "Select Existing LOE." : lblError.Text = "Select Existing LOE."
            '    ddlExistingLOE.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlExistingLOE').focus();", True)
            '    liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            '    liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            '    liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            '    liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            '    liLOE.Attributes.Add("class", "active") : divLOE.Attributes.Add("class", "tab-pane active")
            '    liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            '    liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")
            '    Exit Sub
            'End If
            'If ddlExistingLOE.SelectedIndex > 0 Then
            '    LoadText(ddlExistingLOE.SelectedValue)
            'End If

            If ddlLOETemplateCustomers.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnSaveLOE.Visible = True
                'End If
                ddlLOETemplateCustomers_SelectedIndexChanged(sender, e)
            End If

            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnSaveLocation.Visible = False : imgbtnUpdateLoction.Visible = False
            imgbtnSaveOther.Visible = False
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

            liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            liLOE.Attributes.Remove("class") : divLOE.Attributes.Add("class", "tab-pane")
            liLOETemplater.Attributes.Add("class", "active") : divLOETemplate.Attributes.Add("class", "tab-pane active")
            liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")

            If ddlLOETemplateCustomers.SelectedIndex > 0 AndAlso iLOESelectID > 0 Then
                ddlExistingLOETemplate.SelectedValue = iLOESelectID
                ddlExistingLOETemplate_SelectedIndexChanged(sender, e)
            End If
            If ddlExistingLOE.SelectedIndex = 0 AndAlso ddlExistingLOETemplate.SelectedIndex = 0 Then
                ClearAllLOETemplate()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLOETemplate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub LoadExistingCust()
        Dim dt As New DataTable
        Try
            dt = objCust.LoadAllCustomers(sSession.AccessCode, sSession.AccessCodeID)
            ddlCustName.DataSource = dt
            ddlCustName.DataTextField = "Cust_Name"
            ddlCustName.DataValueField = "Cust_Id"
            ddlCustName.DataBind()
            ddlCustName.Items.Insert(0, "Select Customer Name ")

            ddlOtherDetailsCust.DataSource = dt
            ddlOtherDetailsCust.DataTextField = "Cust_Name"
            ddlOtherDetailsCust.DataValueField = "Cust_Id"
            ddlOtherDetailsCust.DataBind()
            ddlOtherDetailsCust.Items.Insert(0, "Select Customer Name")

            ddlLocationCust.DataSource = dt
            ddlLocationCust.DataTextField = "Cust_Name"
            ddlLocationCust.DataValueField = "Cust_Id"
            ddlLocationCust.DataBind()
            ddlLocationCust.Items.Insert(0, "Select Customer Name")

            ddlLOECustomers.DataSource = dt
            ddlLOECustomers.DataTextField = "Cust_Name"
            ddlLOECustomers.DataValueField = "Cust_Id"
            ddlLOECustomers.DataBind()
            ddlLOECustomers.Items.Insert(0, "Select Customer Name")

            ddlLOETemplateCustomers.DataSource = dt
            ddlLOETemplateCustomers.DataTextField = "Cust_Name"
            ddlLOETemplateCustomers.DataValueField = "Cust_Id"
            ddlLOETemplateCustomers.DataBind()
            ddlLOETemplateCustomers.Items.Insert(0, "Select Customer Name")

            ddlCompExistingCustomer.DataSource = dt
            ddlCompExistingCustomer.DataTextField = "Cust_Name"
            ddlCompExistingCustomer.DataValueField = "Cust_Id"
            ddlCompExistingCustomer.DataBind()
            ddlCompExistingCustomer.Items.Insert(0, "Select Customer Name")

            ddlAsgExistingCustomer.DataSource = dt
            ddlAsgExistingCustomer.DataTextField = "Cust_Name"
            ddlAsgExistingCustomer.DataValueField = "Cust_Id"
            ddlAsgExistingCustomer.DataBind()
            ddlAsgExistingCustomer.Items.Insert(0, "Select Customer Name")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingCust" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub LoadExistingGroup()
        Try
            ddlExistingGroup.DataSource = objCust.LoadExistingGroups(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistingGroup.DataTextField = "CUST_GROUPNAME"
            ddlExistingGroup.DataBind()
            ddlExistingGroup.Items.Insert(0, "Select")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingGroup" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub LoadAllDropDown()
        Try
            ddlOrganization.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "ORG")
            ddlOrganization.DataTextField = "cmm_Desc"
            ddlOrganization.DataValueField = "cmm_ID"
            ddlOrganization.DataBind()
            ddlOrganization.Items.Insert(0, "Select Organization Type")

            ddlIndustry.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "IND")
            ddlIndustry.DataTextField = "cmm_Desc"
            ddlIndustry.DataValueField = "cmm_ID"
            ddlIndustry.DataBind()
            ddlIndustry.Items.Insert(0, "Select Industry Type")

            ddlManagement.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "MNG")
            ddlManagement.DataTextField = "cmm_Desc"
            ddlManagement.DataValueField = "cmm_ID"
            ddlManagement.DataBind()
            ddlManagement.Items.Insert(0, "Select Management Type")

            'ddlBranch.DataSource = objCust.LoadBranch(sSession.AccessCode, sSession.AccessCodeID)
            'ddlBranch.DataTextField = "Branch_NAme"
            'ddlBranch.DataValueField = "Branch_Id"
            'ddlBranch.DataBind()
            'ddlBranch.Items.Insert(0, "Select Branch")

            chkboxTask.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "AT")
            chkboxTask.DataTextField = "cmm_Desc"
            chkboxTask.DataValueField = "cmm_ID"
            chkboxTask.DataBind()

            ddlTask.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlTask.DataTextField = "cmm_Desc"
            ddlTask.DataValueField = "cmm_ID"
            ddlTask.DataBind()
            ddlTask.Items.Insert(0, "Select Task")

            ddlGroup.Items.Clear()
            ddlGroup.Items.Add(New ListItem("Select Group", "0"))
            ddlGroup.Items.Add(New ListItem("Yes", "1"))
            ddlGroup.Items.Add(New ListItem("No", "2"))
            ddlGroup.SelectedIndex = 0

            ddlCat.DataSource = objclsAllActiveMaster.LoadActiveDesignation(sSession.AccessCode, sSession.AccessCodeID)
            ddlCat.DataTextField = "Mas_Description"
            ddlCat.DataValueField = "Mas_Id"
            ddlCat.DataBind()
            ddlCat.Items.Insert(0, " Select Categories ")

            ddlFunction.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlFunction.DataTextField = "cmm_Desc"
            ddlFunction.DataValueField = "cmm_ID"
            ddlFunction.DataBind()
            ddlFunction.Items.Insert(0, "Select Assignments/Tasks")

            ddlFrequency.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "FRE")
            ddlFrequency.DataTextField = "cmm_Desc"
            ddlFrequency.DataValueField = "cmm_ID"
            ddlFrequency.DataBind()
            ddlFrequency.Items.Insert(0, "Select Frequency")

            cboReExp.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "LE")
            cboReExp.DataTextField = "cmm_Desc"
            cboReExp.DataValueField = "cmm_ID"
            cboReExp.DataBind()
            cboReExp.Items.Insert(0, " Select Reimbursement")

            cboCatList.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "OE")
            cboCatList.DataTextField = "cmm_Desc"
            cboCatList.DataValueField = "cmm_ID"
            cboCatList.DataBind()
            cboCatList.Items.Insert(0, "Select Other Expenses")

            'Compliance
            ddlCompTask.DataSource = objclsGeneralFunctions.LoadComplianceTask(sSession.AccessCode, sSession.AccessCodeID, "AT")
            ddlCompTask.DataTextField = "cmm_Desc"
            ddlCompTask.DataValueField = "cmm_ID"
            ddlCompTask.DataBind()
            ddlCompTask.Items.Insert(0, "Select Task")

            'ddlCompFrequency.DataSource = objclsGeneralFunctions.LoadGeneralMasters(sSession.AccessCode, sSession.AccessCodeID, "FRE")
            'ddlCompFrequency.DataTextField = "cmm_Desc"
            'ddlCompFrequency.DataValueField = "cmm_ID"
            'ddlCompFrequency.DataBind()
            'ddlCompFrequency.Items.Insert(0, "Select Frequency")

            ddlCompFrequency.Items.Add(New ListItem("Select Frequency", "0"))
            ddlCompFrequency.Items.Add(New ListItem("Yearly", "1"))
            ddlCompFrequency.Items.Add(New ListItem("Quarterly", "4"))
            ddlCompFrequency.Items.Add(New ListItem("Monthly", "2"))
            'ddlFrequency.Items.Add(New ListItem("Once", "3"))
            ddlCompFrequency.SelectedIndex = 0

            ddlCompAccountDetails.Items.Clear()
            ddlCompAccountDetails.Items.Add(New ListItem("Select Account Details", "0"))
            ddlCompAccountDetails.Items.Add(New ListItem("Yes", "1"))
            ddlCompAccountDetails.Items.Add(New ListItem("No", "2"))
            ddlCompAccountDetails.SelectedIndex = 0

            ddlStatutoryReferences.Items.Add(New ListItem("Select Statutory References", "0"))
            ddlStatutoryReferences.Items.Add(New ListItem("CIN", "1"))
            ddlStatutoryReferences.Items.Add(New ListItem("TAN", "2"))
            ddlStatutoryReferences.Items.Add(New ListItem("PAN", "3"))
            ddlStatutoryReferences.Items.Add(New ListItem("GSTIN", "4"))
            ddlStatutoryReferences.Items.Add(New ListItem("Import Export Code Number (IEC)", "5"))
            ddlStatutoryReferences.Items.Add(New ListItem("ESI", "6"))
            ddlStatutoryReferences.Items.Add(New ListItem("PF", "7"))
            ddlStatutoryReferences.Items.Add(New ListItem("S&E", "8"))
            ddlStatutoryReferences.Items.Add(New ListItem("Pollution Control Board", "9"))
            ddlStatutoryReferences.Items.Add(New ListItem("Inspector of Factories", "10"))
            ddlStatutoryReferences.Items.Add(New ListItem("Central Excise Registration Numbers", "11"))
            ddlStatutoryReferences.Items.Add(New ListItem("Service Tax Registration Numbers", "12"))
            ddlStatutoryReferences.Items.Add(New ListItem("VAT Registration Numbers", "13"))
            ddlStatutoryReferences.Items.Add(New ListItem("Others", "14"))
            ddlStatutoryReferences.SelectedIndex = 0

            LoadExistingGroup()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadAllDropDown" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub Bindcustumstyles()
        Try
            ddlcustomFontstyle.DataSource = objCust.LoadCustomfontstyle(sSession.AccessCode, sSession.AccessCodeID)
            ddlcustomFontstyle.DataTextField = "CF_name"
            ddlcustomFontstyle.DataValueField = "CF_ID"
            ddlcustomFontstyle.DataBind()
            ddlcustomFontstyle.Items.Insert(0, "Select")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadExistingGroup" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlCustName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCustName.SelectedIndexChanged
        Dim sflag As String = ""
        Try
            lblError.Text = ""
            ClearAll()
            If ddlCustName.SelectedIndex > 0 Then
                BindCustDetails(ddlCustName.SelectedValue)

                ddlOtherDetailsCust.SelectedValue = ddlCustName.SelectedValue
                ddlLocationCust.SelectedValue = ddlCustName.SelectedValue
                ddlLOECustomers.SelectedValue = ddlCustName.SelectedValue
                ddlLOETemplateCustomers.SelectedValue = ddlCustName.SelectedValue
                ddlCompExistingCustomer.SelectedValue = ddlCustName.SelectedValue
                ddlAsgExistingCustomer.SelectedValue = ddlCustName.SelectedValue
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCustName_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            lblError.Text = ""
            iCBN_NODE = 0
            txtCommEmail.Text = ""
            txtEmailId.Text = ""
            txtDate.Text = ""
            txtCustName.Text = ""
            txtCustCode.Text = objCust.GetLatestCustomerCode(sSession.AccessCode, sSession.AccessCodeID)
            ddlGroup.SelectedValue = 0 : LoadExistingGroup() : txtGroupName.Text = "" : txtCommAdd.Text = ""
            ddlIndustry.SelectedIndex = 0 : ddlOrganization.SelectedIndex = 0 : ddlManagement.SelectedIndex = 0 : txtCustomerRegistrationNo.Text = ""
            txtCommCity.Text = "" : txtOffAdd.Text = "" : chkSameAddress.Checked = False
            txtCommPin.Text = "" : txtCommState.Text = "" : txtCommCountry.Text = ""
            txtCommEmail.Text = "" : txtCommFax.Text = "" : txtCommTele.Text = ""
            txtPin.Text = "" : txtCity.Text = "" : txtState.Text = "" : txtCompanyURL.Text = ""
            txtCountry.Text = "" : txtFax.Text = "" : txtEMail.Text = ""
            txtTele.Text = "" : txtLglAdvisor.Text = ""
            txtBoardOfDirectors.Text = ""
            lblStatus.Text = ""
            For j = 0 To chkboxTask.Items.Count - 1
                chkboxTask.Items(j).Selected = False
            Next
            imgbtnUpdate.Visible = False : imgbtnSave.Visible = False
            'If sCDSave = "YES" Then
            imgbtnSave.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " ClearAll" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindCustDetails(ByVal iCustID As Integer)
        Dim dt As New DataTable
        Dim sSplitAry As String()
        Try
            imgbtnUpdate.Visible = False : imgbtnSave.Visible = False
            'If sCDSave = "YES" Then
            imgbtnSave.Visible = True
            'End If

            dt = objCust.LoadCustomerMaster(sSession.AccessCode, sSession.AccessCodeID, iCustID)
            If dt.Rows.Count > 0 Then
                imgbtnUpdate.Visible = False : imgbtnSave.Visible = False
                'If sCDSave = "YES" Then
                imgbtnUpdate.Visible = True
                'End If
                For i = 0 To dt.Rows.Count - 1
                    ddlCustName.SelectedValue = iCustID
                    If IsDBNull(dt.Rows(i)("CUST_NAME")) = False Then
                        txtCustName.Text = dt.Rows(i)("CUST_NAME")
                        iCBN_NODE = objCust.GetCabinetPKID(sSession.AccessCode, sSession.AccessCodeID, txtCustName.Text)
                    End If
                    If IsDBNull(dt.Rows(i)("CUST_CODE")) = False Then
                        txtCustCode.Text = dt.Rows(i)("CUST_CODE")
                    End If
                    If IsDBNull(dt.Rows(i)("CUST_WEBSITE")) = False Then
                        txtCompanyURL.Text = dt.Rows(i)("CUST_WEBSITE")
                    End If
                    If IsDBNull(dt.Rows(i)("CUST_EMAIL")) = False Then
                        txtEMail.Text = dt.Rows(i)("CUST_EMAIL")
                    End If

                    Try
                        If dt.Rows(i)("CUST_GROUPINDIVIDUAL") = 0 Then
                            ddlGroup.SelectedIndex = 0
                            txtGroupName.Enabled = False
                            ddlExistingGroup.Enabled = False
                        Else
                            ddlGroup.SelectedValue = dt.Rows(i)("CUST_GROUPINDIVIDUAL")
                            If dt.Rows(i)("CUST_GROUPINDIVIDUAL") = 1 Then
                                If IsDBNull(dt.Rows(i)("CUST_GROUPNAME")) = False Then
                                    txtGroupName.Text = dt.Rows(i)("CUST_GROUPNAME")
                                End If
                                txtGroupName.Enabled = True
                                ddlExistingGroup.Enabled = True
                            End If
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If dt.Rows(i)("CUST_ORGTYPEID") = 0 Then
                            ddlOrganization.SelectedIndex = 0
                        Else
                            Dim liOrganization As ListItem = ddlOrganization.Items.FindByValue(dt.Rows(i)("CUST_ORGTYPEID"))
                            If IsNothing(liOrganization) = False Then
                                ddlOrganization.SelectedValue = dt.Rows(i)("CUST_ORGTYPEID")
                            End If
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If dt.Rows(i)("CUST_INDTYPEID") = 0 Then
                            ddlIndustry.SelectedIndex = 0
                        Else
                            ddlIndustry.SelectedValue = dt.Rows(i)("CUST_INDTYPEID")
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        If dt.Rows(i)("CUST_MGMTTYPEID") = 0 Then
                            ddlManagement.SelectedIndex = 0
                        Else
                            ddlManagement.SelectedValue = dt.Rows(i)("CUST_MGMTTYPEID")
                        End If
                    Catch ex As Exception

                    End Try

                    Try
                        txtCustomerRegistrationNo.Text = dt.Rows(i)("CUSt_BranchID")
                    Catch ex As Exception

                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_CommitmentDate")) = False Then
                            txtDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("CUST_CommitmentDate"), "D")
                        End If
                    Catch ex As Exception

                    End Try

                    Try

                        If IsDBNull(dt.Rows(i)("CUST_COMM_ADDRESS")) = False Then
                            txtCommAdd.Text = dt.Rows(i)("CUST_COMM_ADDRESS")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_CITY")) = False Then
                            txtCommCity.Text = dt.Rows(i)("CUST_COMM_CITY")
                        End If
                    Catch ex As Exception
                    End Try


                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_PIN")) = False Then
                            txtCommPin.Text = dt.Rows(i)("CUST_COMM_PIN")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_STATE")) = False Then
                            txtCommState.Text = dt.Rows(i)("CUST_COMM_STATE")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_COUNTRY")) = False Then
                            txtCommCountry.Text = dt.Rows(i)("CUST_COMM_COUNTRY")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_FAX")) = False Then
                            txtCommFax.Text = dt.Rows(i)("CUST_COMM_FAX")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_TEL")) = False Then
                            txtCommTele.Text = dt.Rows(i)("CUST_COMM_TEL")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COMM_Email")) = False Then
                            txtCommEmail.Text = dt.Rows(i)("CUST_COMM_Email")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_ADDRESS")) = False Then
                            txtOffAdd.Text = dt.Rows(i)("CUST_ADDRESS")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_CITY")) = False Then
                            txtCity.Text = dt.Rows(i)("CUST_CITY")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_PIN")) = False Then
                            txtPin.Text = dt.Rows(i)("CUST_PIN")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_STATE")) = False Then
                            txtState.Text = dt.Rows(i)("CUST_STATE")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_COUNTRY")) = False Then
                            txtCountry.Text = dt.Rows(i)("CUST_COUNTRY")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_FAX")) = False Then
                            txtFax.Text = dt.Rows(i)("CUST_FAX")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_TELPHONE")) = False Then
                            txtTele.Text = dt.Rows(i)("CUST_TELPHONE")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_ConEmailID")) = False Then
                            txtEmailId.Text = dt.Rows(i)("CUST_ConEmailID")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_BOARDOFDIRECTORS")) = False Then
                            txtBoardOfDirectors.Text = dt.Rows(i)("CUST_BOARDOFDIRECTORS")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_TASKS")) = False Then
                            sSplitAry = dt.Rows(i)("CUST_TASKS").Split(",")
                            For j = 0 To chkboxTask.Items.Count - 1
                                If sSplitAry.Contains(chkboxTask.Items(j).Value) = True Then
                                    chkboxTask.Items(j).Selected = True
                                End If
                            Next
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_Amount_Type")) = False Then
                            ddlAmountConvert.SelectedValue = dt.Rows(i)("CUST_Amount_Type")
                        End If
                    Catch ex As Exception
                    End Try

                    Try
                        If IsDBNull(dt.Rows(i)("CUST_RoundOff")) = False Then
                            txtRoundOff.Text = dt.Rows(i)("CUST_RoundOff")
                        End If
                    Catch ex As Exception
                    End Try
                    Try
                        If IsDBNull(dt.Rows(i)("Cust_fontstyleid")) = False Then
                            ddlcustomFontstyle.SelectedValue = dt.Rows(i)("Cust_fontstyleid")
                        Else
                            ddlcustomFontstyle.SelectedIndex = 0
                        End If
                    Catch ex As Exception
                    End Try
                    'If IsDBNull(dt.Rows(i)("CUST_DEPMETHOD")) = False Then
                    '    ddlMethod.SelectedValue = dt.Rows(i)("CUST_DEPMETHOD")
                    'End If

                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " BindCustDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = ""
            LoadExistingCust()
            If lblTab.Text = "1" Then
                ClearAll()
            ElseIf lblTab.Text = "2" Then
                ClearOtherDetails()
            ElseIf lblTab.Text = "3" Then
                ClearLocation() : lstboxLocation.Items.Clear()
            ElseIf lblTab.Text = "4" Then
                ClearLOE() : ddlLOELocation.SelectedIndex = 0 : ddlTask.SelectedIndex = 0 : ddlExistingLOE.SelectedIndex = 0 : ddlExistingLOETemplate.SelectedIndex = 0
            ElseIf lblTab.Text = "5" Then
                ClearAllLOETemplate()
            ElseIf lblTab.Text = "6" Then
                ClearAllComplaince()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, " imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim sTasks As String = ""
        Dim i As Integer, iCabinetRet As Integer, iSubCabinetRet As Integer, iFolderRet As Integer, iSubCabinetPKID As Integer
        Dim CommitmentDate As Date, dSDate As Date, dDate As Date
        Dim objstrCust As New strCustMaster
        Dim Arr() As String, ArrCabinet() As String, ArrSubCabinet() As String, ArrFolder() As String, ArrSubCabinets As New ArrayList, ArrFolders As New ArrayList
        Dim iCount As Integer = 0
        Dim iDeptID As Integer = 0
        Try
            lblError.Text = ""
            'iCount = objCust.GetCount(sSession.AccessCode, sSession.AccessCodeID)
            'If iCount >= 5 Then
            '    lblCustomerValidationMsg.Text = "Please Contact System Admin to Add more Customers." : lblError.Text = "Please Contact System Admin to Add more Customers."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustName').focus();", True)
            '    Exit Sub
            'End If
            If objCust.GetTRACeCustomerCount(sSession.AccessCode, sSession.AccessCodeID) >= sSession.NumberOfCustomers Then
                lblCustomerValidationMsg.Text = "Customer limit exceeded in TRACe application. Please contact Administrator" : lblError.Text = "Customer limit exceeded in TRACe application. Please contact Administrator."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show'); $('#txtCustName').focus();", True)
                Exit Sub
            End If
            If txtCustName.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Customer Name." : lblError.Text = "Enter Customer Name."
                txtCustName.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustName').focus();", True)
                Exit Sub
            End If
            If txtCustCode.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Customer Code." : lblError.Text = "Enter Customer Code."
                txtCustCode.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustCode').focus();", True)
                Exit Sub
            End If
            If ddlIndustry.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Industry Type." : lblError.Text = "Select Industry Type."
                ddlIndustry.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlIndustry').focus();", True)
                Exit Sub
            End If
            If txtDate.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Business Reltn. Start Date." : lblError.Text = "Enter Business Reltn. Start Date."
                txtDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDate').focus();", True)
                Exit Sub
            End If
            'If ddlManagement.SelectedIndex = 0 Then
            '    lblCustomerValidationMsg.Text = "Select Management Type." : lblError.Text = "Select Management Type."
            '    ddlManagement.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlManagement').focus();", True)
            '    Exit Sub
            'End If
            'If ddlGroup.SelectedIndex = 0 Then
            '    lblCustomerValidationMsg.Text = "Select Group." : lblError.Text = "Select Group."
            '    ddlGroup.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlGroup').focus();", True)
            '    Exit Sub
            'End If
            If ddlOrganization.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Organization Type." : lblError.Text = "Select Organization Type."
                ddlOrganization.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlOrganization').focus();", True)
                Exit Sub
            End If
            If txtCustName.Text.Trim.Length > 150 Then
                lblCustomerValidationMsg.Text = "Customer Name exceeded maximum size(max 150 characters)." : lblError.Text = "Customer Name exceeded maximum size(max 150 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustName').focus();", True)
                txtCustName.Focus()
                Exit Sub
            End If
            If txtCustCode.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Customer Code exceeded maximum size(max 50 characters)." : lblError.Text = "Customer Code exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustCode').focus();", True)
                txtCustCode.Focus()
                Exit Sub
            End If
            If txtCompanyURL.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Company URL exceeded maximum size(max 50 characters)." : lblError.Text = "Company URL exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCompanyURL').focus();", True)
                txtCompanyURL.Focus()
                Exit Sub
            End If
            If txtContactEmail.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Company Email exceeded maximum size(max 50 characters)." : lblError.Text = "Company Email exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtContactEmail').focus();", True)
                txtContactEmail.Focus()
                Exit Sub
            End If
            If txtGroupName.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Group Name exceeded maximum size(max 50 characters)." : lblError.Text = "Group Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtGroupName').focus();", True)
                txtGroupName.Focus()
                Exit Sub
            End If
            If txtBoardOfDirectors.Text.Trim.Length > 255 Then
                lblCustomerValidationMsg.Text = "Board Of Directors/Partners exceeded maximum size(max 255 characters)." : lblError.Text = "Board Of Directors/Partners exceeded maximum size(max 255 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtBoardOfDirectors').focus();", True)
                txtBoardOfDirectors.Focus()
                Exit Sub
            End If
            If Len(Trim(txtCommAdd.Text)) > 1000 Then
                lblCustomerValidationMsg.Text = " Contact Address exceeded maximum size(max 1000 characters)." : lblError.Text = " Contact Address exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommAdd').focus();", True)
                txtCommAdd.Focus()
                Exit Try
            End If
            If Len(Trim(txtCommCity.Text)) > 100 Then
                lblCustomerValidationMsg.Text = " Contact City exceeded maximum size(max 100 characters)." : lblError.Text = " Contact City exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommCity').focus();", True)
                txtCommCity.Focus()
                Exit Try
            End If
            If Len(Trim(txtCommState.Text)) > 100 Then
                lblCustomerValidationMsg.Text = " Contact State exceeded maximum size(max 100 characters)." : lblError.Text = " Contact State exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommState').focus();", True)
                txtCommState.Focus()
                Exit Try
            End If
            If Len(Trim(txtCommCountry.Text)) > 100 Then
                lblCustomerValidationMsg.Text = " Contact Country exceeded maximum size(max 100 characters)." : lblError.Text = " Contact Country exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommCountry').focus();", True)
                txtCommCountry.Focus()
                Exit Try
            End If
            If Len(Trim(txtPin.Text)) > 6 Then
                lblCustomerValidationMsg.Text = " Postal Code exceeded maximum size(max 6 characters)." : lblError.Text = " Postal Code exceeded maximum size(max 6 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtPin').focus();", True)
                txtPin.Focus()
                Exit Try
            End If
            If txtCommFax.Text.Trim.Length > 15 Then
                lblCustomerValidationMsg.Text = "Contact Fax exceeded maximum size(max 15 characters)." : lblError.Text = "Contact Fax exceeded maximum size(max 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommFax').focus();", True)
                txtCommFax.Focus()
                Exit Sub
            End If
            If txtCommTele.Text.Trim.Length > 15 Then
                lblCustomerValidationMsg.Text = "Contact Telephone exceeded maximum size(max 15 characters)." : lblError.Text = "Contact Telephone exceeded maximum size(max 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommTele').focus();", True)
                txtCommTele.Focus()
                Exit Sub
            End If
            If txtCommEmail.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Contact Email exceeded maximum size(max 50 characters)." : lblError.Text = "Contact Email exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommEmail').focus();", True)
                txtCommEmail.Focus()
                Exit Sub
            End If
            If Len(Trim(txtOffAdd.Text)) > 1000 Then
                lblCustomerValidationMsg.Text = " Office Address exceeded maximum size(max 1000 characters)." : lblError.Text = " Office Address exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtOffAdd').focus();", True)
                txtOffAdd.Focus()
                Exit Try
            End If
            If Len(Trim(txtCity.Text)) > 100 Then
                lblCustomerValidationMsg.Text = " Office City exceeded maximum size(max 100 characters)." : lblError.Text = " Office City exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCity').focus();", True)
                txtCity.Focus()
                Exit Try
            End If
            If Len(Trim(txtState.Text)) > 100 Then
                lblCustomerValidationMsg.Text = " Office State exceeded maximum size(max 100 characters)." : lblError.Text = " Office State exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtState').focus();", True)
                txtState.Focus()
                Exit Try
            End If
            If Len(Trim(txtCountry.Text)) > 100 Then
                lblCustomerValidationMsg.Text = " Office Country exceeded maximum size(max 100 characters)." : lblError.Text = " Office Country exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCountry').focus();", True)
                txtCountry.Focus()
                Exit Try
            End If
            If Len(Trim(txtCommPin.Text)) > 10 Then
                lblCustomerValidationMsg.Text = " Postal Code exceeded maximum size(max 10 characters)." : lblError.Text = " Postal Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCommPin').focus();", True)
                txtCommPin.Focus()
                Exit Try
            End If
            If txtFax.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Office Fax exceeded maximum size(max 50 characters)." : lblError.Text = "Office Fax exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtFax').focus();", True)
                txtFax.Focus()
                Exit Sub
            End If
            If txtTele.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Office Telephone exceeded maximum size(max 50 characters)." : lblError.Text = "Office Telephone exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtTele').focus();", True)
                txtTele.Focus()
                Exit Sub
            End If
            If txtEmailId.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Office Email exceeded maximum size(max 50 characters)." : lblError.Text = "Office Email exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtEmailId').focus();", True)
                txtEmailId.Focus()
                Exit Sub
            End If

            'If ddlAmountConvert.SelectedIndex = 0 Then
            '    lblCustomerValidationMsg.Text = "Select Amount Type." : lblError.Text = "Select Amount Type."
            '    ddlOrganization.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlOrganization').focus();", True)
            '    Exit Sub
            'End If

            'If txtRoundOff.Text = "" Then
            '    lblCustomerValidationMsg.Text = "Enter Round Off." : lblError.Text = "Enter Round Off."
            '    txtCustCode.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCustCode').focus();", True)
            '    Exit Sub
            'End If

            If chkboxTask.Items.Count > 0 Then
                For i = 0 To chkboxTask.Items.Count - 1
                    If chkboxTask.Items(i).Selected = True Then
                        sTasks = sTasks & "," & chkboxTask.Items(i).Value
                    End If
                Next
                If sTasks.StartsWith(",") Then
                    sTasks = sTasks.Remove(0, 1)
                End If
                If sTasks.EndsWith(",") Then
                    sTasks = sTasks.Remove(Len(sTasks) - 1, 1)
                End If
            End If
            If sTasks = "" Then
                lblCustomerValidationMsg.Text = "Select Professional Services Offered." : lblError.Text = "Select Professional Services Offered."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#chkboxTask').focus();", True)
                Exit Sub
            End If
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim l As Integer
            l = DateDiff(DateInterval.Day, dDate, dSDate)
            If l > 0 Then
                lblCustomerValidationMsg.Text = "Business Reltn. Start Date should be less than or equal to Current Date."
                lblError.Text = "Business Reltn. Start Date should be less than or equal to Current Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDate').focus();", True)
                txtDate.Focus()
                Exit Sub
            End If

            'If ddlMethod.SelectedIndex = 0 Then
            '    lblCustomerValidationMsg.Text = "Select Depreciation Method." : lblError.Text = "Select Depreciation Method."
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtEmailId').focus();", True)
            '    ddlMethod.Focus()
            '    Exit Sub
            'End If

            CommitmentDate = Date.ParseExact(txtDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)

            If ddlCustName.SelectedIndex > 0 Then
                objstrCust.CUST_ID = ddlCustName.SelectedValue
            Else
                objstrCust.CUST_ID = 0
            End If
            objstrCust.CUST_NAME = txtCustName.Text
            objstrCust.CUST_CODE = txtCustCode.Text
            objstrCust.CUST_WEBSITE = txtCompanyURL.Text
            objstrCust.CUST_EMAIL = txtEMail.Text
            objstrCust.CUST_GROUPNAME = txtGroupName.Text
            If ddlGroup.SelectedIndex > 0 Then
                objstrCust.CUST_GROUPINDIVIDUAL = ddlGroup.SelectedValue
            Else
                objstrCust.CUST_GROUPINDIVIDUAL = 0
            End If
            objstrCust.CUST_ORGTYPEID = ddlOrganization.SelectedValue
            objstrCust.CUST_INDTYPEID = ddlIndustry.SelectedValue
            If ddlManagement.SelectedIndex > 0 Then
                objstrCust.CUST_MGMTTYPEID = ddlManagement.SelectedValue
            Else
                objstrCust.CUST_MGMTTYPEID = 0
            End If
            objstrCust.CUST_CommitmentDate = CommitmentDate
            objstrCust.CUSt_BranchId = txtCustomerRegistrationNo.Text
            objstrCust.CUST_COMM_ADDRESS = txtCommAdd.Text
            objstrCust.CUST_COMM_CITY = txtCommCity.Text
            objstrCust.CUST_COMM_PIN = txtCommPin.Text
            objstrCust.CUST_COMM_STATE = txtCommState.Text
            objstrCust.CUST_COMM_COUNTRY = txtCommCountry.Text
            objstrCust.CUST_COMM_FAX = txtCommFax.Text
            objstrCust.CUST_COMM_TEL = txtCommTele.Text
            objstrCust.CUST_COMM_Email = txtCommEmail.Text
            objstrCust.CUST_ADDRESS = txtOffAdd.Text
            objstrCust.CUST_CITY = txtCity.Text
            objstrCust.CUST_PIN = txtPin.Text
            objstrCust.CUST_STATE = txtState.Text
            objstrCust.CUST_COUNTRY = txtCountry.Text
            objstrCust.CUST_FAX = txtFax.Text
            objstrCust.CUST_TELPHONE = txtTele.Text
            objstrCust.CUST_ConEmailID = txtEmailId.Text
            objstrCust.CUST_LOCATIONID = ""
            objstrCust.CUST_TASKS = sTasks
            objstrCust.CUST_ORGID = ddlOrganization.Text
            objstrCust.CUST_DELFLG = "W"
            objstrCust.CUST_CRBY = sSession.UserID
            objstrCust.CUST_UpdatedBy = sSession.UserID
            objstrCust.CUST_BOARDOFDIRECTORS = txtBoardOfDirectors.Text
            objstrCust.CUST_STATUS = "W"
            objstrCust.CUST_IPAddress = sSession.IPAddress
            objstrCust.CUST_CompID = sSession.AccessCodeID

            objstrCust.CUST_Amount_Type = ddlAmountConvert.SelectedValue
            If (txtRoundOff.Text = "") Then
                objstrCust.CUST_RoundOff = 0
            Else
                objstrCust.CUST_RoundOff = txtRoundOff.Text
            End If


            'Steffi 22062023
            If ddlCustName.SelectedIndex = 0 Then
                If (objCust.CheckCustomerAlreadyExists(sSession.AccessCode, sSession.AccessCodeID, txtCustName.Text) = True) Then
                    lblError.Text = "Customer Name already Exists."
                    lblCustomerValidationMsg.Text = "Customer Name already Exists."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                    Return
                End If
            End If


            Arr = objCust.SaveCustomerMaster(sSession.AccessCode, objstrCust)
            If ddlcustomFontstyle.SelectedIndex > 0 Then
                objCust.UpdateCustfontstyle(sSession.AccessCode, sSession.AccessCodeID, objstrCust.CUST_ID, ddlcustomFontstyle.SelectedValue)
            End If

            'Commented by steffi on 03-08-2023 becuase of pramthi database giving error (In pramthi database using cbn_node, but we are using cbn_id)


            'Create Organisation Structure - Steffi 22-06-2023
            iDeptID = CreateCustomerOrgStructure()

            'Update Department ID to Customer Table based on CustomerID
            objCust.UpdateDepartmentIdToCustomerTable(sSession.AccessCode, sSession.AccessCodeID, iDeptID, Arr(1))

            'Cabinet  - Steffi 22-06-2023
            If iCBN_NODE > 0 Then
                'iCabinetRet = objclsCabinet.CheckCabName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(txtCustName.Text), iCBN_NODE, iDeptID)
                iCabinetRet = 1
            Else
                iCabinetRet = objclsCabinet.CheckCabName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(txtCustName.Text), 0, iDeptID)
            End If

            If iCabinetRet = 0 Then
                If IsDBNull(txtCustName.Text) = False Then
                    objclsCabinet.sCBN_Name = objclsEDICTGeneral.SafeSQL(txtCustName.Text)
                Else
                    objclsCabinet.sCBN_Name = ""
                End If

                If IsDBNull(txtCustName.Text) = False Then
                    objclsCabinet.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtCustName.Text)
                Else
                    objclsCabinet.sCBN_Note = ""
                End If
                objclsCabinet.iCBN_ID = 0
                objclsCabinet.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtCustName.Text)
                objclsCabinet.iCBN_Parent = "-1"
                objclsCabinet.iCBN_UserID = sSession.UserID
                objclsCabinet.iCBN_Department = iDeptID
                objclsCabinet.iCBN_SubCabCount = 0
                objclsCabinet.iCBN_FolderCount = 0
                objclsCabinet.iCBN_CreatedBy = sSession.UserID
                objclsCabinet.sCBN_Status = "A"
                objclsCabinet.sCBN_DelFlag = "A"
                ArrCabinet = objclsCabinet.SaveCabDetails(sSession.AccessCode, sSession.AccessCodeID, objclsCabinet)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Customer Master(Cabinet)", "Saved", ArrCabinet(1), txtCustName.Text, 0, "", sSession.IPAddress)
            Else

                If IsDBNull(txtCustName.Text) = False Then
                    objclsCabinet.sCBN_Name = objclsEDICTGeneral.SafeSQL(txtCustName.Text)
                Else
                    objclsCabinet.sCBN_Name = ""
                End If

                If IsDBNull(txtCustName.Text) = False Then
                    objclsCabinet.sCBN_Note = objclsEDICTGeneral.SafeSQL(txtCustName.Text)
                Else
                    objclsCabinet.sCBN_Note = ""
                End If
                objclsCabinet.iCBN_ID = iCBN_NODE
                objclsCabinet.iCBN_Department = iDeptID
                objclsCabinet.sCBN_Status = "A"
                objclsCabinet.sCBN_DelFlag = "A"
                objclsCabinet.iCBN_Parent = "-1"
                objclsCabinet.iCBN_UserID = sSession.UserID
                objclsCabinet.iCBN_CreatedBy = sSession.UserID
                objclsCabinet.iCBN_UpdatedBy = sSession.UserID

                objclsCabinet.iCBN_UserID = sSession.UserID

                ArrCabinet = objclsCabinet.SaveCabDetails(sSession.AccessCode, sSession.AccessCodeID, objclsCabinet)
                objclsCabinet.UpdateCabDetails(sSession.AccessCode, 0, iCBN_NODE)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Customer Master(Cabinet)", "Updated", ArrCabinet(1), txtCustName.Text, 0, "", sSession.IPAddress)
            End If

            'Sub Cabinet  - Steffi 22-06-2023
            ArrSubCabinets.Add("Reference Documents") : ArrSubCabinets.Add("Vouchers") : ArrSubCabinets.Add("Audit Related")

            For i = 0 To ArrSubCabinets.Count - 1
                iSubCabinetRet = objclsSubCabinet.CheckSubCabName(sSession.AccessCode, objclsEDICTGeneral.SafeSQL(ArrSubCabinets(i)), 0, iCBN_NODE)

                If iSubCabinetRet = 0 Then
                    If IsDBNull(ArrSubCabinets(i)) = False Then
                        objclsSubCabinet.sCBN_Name = objclsEDICTGeneral.SafeSQL(ArrSubCabinets(i))
                    Else
                        objclsSubCabinet.sCBN_Name = ""
                    End If
                    If IsDBNull(ArrSubCabinets(i)) = False Then
                        objclsSubCabinet.sCBN_Note = objclsEDICTGeneral.SafeSQL(ArrSubCabinets(i))
                    Else
                        objclsSubCabinet.sCBN_Note = ""
                    End If

                    objclsSubCabinet.iCBN_ID = 0
                    objclsSubCabinet.iCBN_Parent = ArrCabinet(1)
                    objclsSubCabinet.iCBN_UserID = sSession.UserID
                    objclsSubCabinet.iCBN_Department = iDeptID
                    objclsSubCabinet.iCBN_SubCabCount = 0
                    objclsSubCabinet.iCBN_FolderCount = 0
                    objclsSubCabinet.iCBN_CreatedBy = sSession.UserID
                    objclsSubCabinet.sCBN_Status = "A"
                    objclsSubCabinet.sCBN_DelFlag = "A"

                    ArrSubCabinet = objclsSubCabinet.SaveSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, objclsSubCabinet)
                    iSubCabinetPKID = ArrSubCabinet(1)
                    objclsSubCabinet.UpdateSubCabDetails(sSession.AccessCode, sSession.AccessCodeID, iDeptID, ArrCabinet(1))
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Customer Master(Sub Cabinet)", "Saved", ArrCabinet(1), txtCustName.Text, ArrCabinet(1), ArrSubCabinets(i), sSession.IPAddress)
                Else
                    iSubCabinetPKID = objclsSubCabinet.GetSubCabID(sSession.AccessCode, objclsEDICTGeneral.SafeSQL(ArrSubCabinets(i)), iCBN_NODE)
                End If

                ''Folder  - Steffi 22-06-2023
                If ArrSubCabinets(i) = "Vouchers" Then
                    ArrFolders.Add("Payments") : ArrFolders.Add("Receipts") : ArrFolders.Add("Agreements")

                    For j = 0 To ArrFolders.Count - 1
                        iFolderRet = objclsFolders.CheckFoldersName(sSession.AccessCode, sSession.AccessCodeID, objclsEDICTGeneral.SafeSQL(ArrFolders(j)), iSubCabinetPKID, 0)

                        If iFolderRet = 0 Then
                            If IsDBNull(ArrFolders(j)) = False Then
                                objclsFolders.sFol_Name = objclsEDICTGeneral.SafeSQL(ArrFolders(j))
                            Else
                                objclsFolders.sFol_Name = ""
                            End If
                            If IsDBNull(ArrFolders(j)) = False Then
                                objclsFolders.sFol_Notes = objclsEDICTGeneral.SafeSQL(ArrFolders(j))
                            Else
                                objclsFolders.sFol_Notes = ""
                            End If

                            objclsFolders.iFol_Id = 0
                            objclsFolders.iFol_Cab = iSubCabinetPKID
                            objclsFolders.sFol_Delflag = "A"
                            objclsFolders.sFol_Status = "A"
                            objclsFolders.iFol_Crby = sSession.UserID
                            objclsFolders.iFol_UpdatedBy = sSession.UserID
                            objclsFolders.iFol_CompId = sSession.AccessCodeID

                            ArrFolder = objclsFolders.SaveFolderDetails(sSession.AccessCode, sSession.AccessCodeID, objclsFolders)
                            objclsFolders.UpdateFolderCount(sSession.AccessCode, sSession.AccessCodeID, ArrCabinet(1), iSubCabinetPKID)
                            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Customer Master(Folder)", "Saved", ArrSubCabinet(1), ArrSubCabinets(i), ArrFolder(1), ArrFolders(j), sSession.IPAddress)
                        End If
                    Next
                End If
            Next


            LoadExistingCust()
            ddlCustName.SelectedValue = Arr(1)
            BindCustDetails(Arr(1))
            If Arr(0) = 2 Then
                If sBackStatus = 2 Then
                    lblError.Text = "Successfully Updated and Waiting for Approval."
                    lblCustomerValidationMsg.Text = "Successfully Updated and Waiting for Approval."
                Else
                    lblError.Text = "Successfully Updated."
                    lblCustomerValidationMsg.Text = "Successfully Updated."
                End If
                LoadExistingGroup()
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Customer Master", "Updated", Arr(1), ddlCustName.SelectedItem.Text, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved and Waiting for Approval."
                lblCustomerValidationMsg.Text = "Successfully Saved and Waiting for Approval."
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Customer Master", "Saved", Arr(1), ddlCustName.SelectedItem.Text, 0, "", sSession.IPAddress)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                sBackStatus = 2
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    'Private Function CreateCustomerOrgStructure()
    '    Dim Arr() As String
    '    Try
    '        objclsOrgStructure.iOrgnode = 0
    '        objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(txtCustName.Text)
    '        objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(txtCustName.Text)
    '        objclsOrgStructure.sOrgSalesUnitCode = ""
    '        objclsOrgStructure.sOrgBranchCode = ""
    '        objclsOrgStructure.iOrgAppStrength = 0
    '        objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(txtCustName.Text)
    '        objclsOrgStructure.iOrgParent = 92
    '        objclsOrgStructure.iOrgLevelCode = 3
    '        objclsOrgStructure.sOrgDelflag = "A"
    '        objclsOrgStructure.sOrgStatus = "A"
    '        objclsOrgStructure.iOrgCreatedBy = sSession.UserID
    '        objclsOrgStructure.dOrgCreatedOn = Date.Today
    '        objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
    '        Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    Private Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Try
            lblError.Text = ""
            imgbtnSave_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindCustOtherDetails()
        Try

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustOtherDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlOtherDetailsCust_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOtherDetailsCust.SelectedIndexChanged
        Try
            lblError.Text = "" : imgbtnSaveOther.Visible = False
            ClearOtherDetails()
            iAttachID = 0
            If ddlOtherDetailsCust.SelectedIndex > 0 Then
                BindCustomerOtherDetails(ddlOtherDetailsCust.SelectedValue)

                ddlCustName.SelectedValue = ddlOtherDetailsCust.SelectedValue
                ddlLocationCust.SelectedValue = ddlOtherDetailsCust.SelectedValue
                ddlLOECustomers.SelectedValue = ddlOtherDetailsCust.SelectedValue
                ddlLOETemplateCustomers.SelectedValue = ddlOtherDetailsCust.SelectedValue
                ddlCompExistingCustomer.SelectedValue = ddlOtherDetailsCust.SelectedValue
                ddlAsgExistingCustomer.SelectedValue = ddlCustName.SelectedValue
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlOtherDetailsCust_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub ClearOtherDetails()
        Try
            txtLglAdvisor.Text = "" : txtFile.Text = ""
            lblError.Text = "" : txtProdManufactured.Text = "" : txturnover.Text = ""
            txtForeignCollaboration.Text = "" : txtStandingInIndustry.Text = "" : txtServiceOff.Text = ""
            txtProfit.Text = "" : txtEmpStrength.Text = "" : txtPerceptionInPublic.Text = "" : txtStatutoryValue.Text = ""
            txtGatheredByFirm.Text = "" : txtPerceptionInGovt.Text = "" : txtlegalIssues.Text = ""
            imgbtnSaveOther.Visible = False
            'If sCDSave = "YES" Then
            imgbtnSaveOther.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearOtherDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            Throw
        End Try
    End Sub
    Public Sub BindCustomerOtherDetails(ByVal iCustID As Integer)
        Dim dt As New DataTable
        Try
            dt = objCust.LoadCustomersDetails(sSession.AccessCode, sSession.AccessCodeID, iCustID)
            If dt.Rows.Count > 0 Then
                imgbtnSaveOther.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveOther.Visible = True
                'End If
                ddlOtherDetailsCust.SelectedValue = iCustID
                For i = 0 To dt.Rows.Count - 1
                    If IsDBNull(dt.Rows(i)("CDET_PRODUCTSMANUFACTURED")) = False Then
                        txtProdManufactured.Text = dt.Rows(i)("CDET_PRODUCTSMANUFACTURED")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_TURNOVER")) = False Then
                        txturnover.Text = dt.Rows(i)("CDET_TURNOVER")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_FOREIGNCOLLABORATIONS")) = False Then
                        txtForeignCollaboration.Text = dt.Rows(i)("CDET_FOREIGNCOLLABORATIONS")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_STANDINGININDUSTRY")) = False Then
                        txtStandingInIndustry.Text = dt.Rows(i)("CDET_STANDINGININDUSTRY")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_FileNo")) = False Then
                        txtFile.Text = dt.Rows(i)("CDET_FileNo")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_SERVICESOFFERED")) = False Then
                        txtServiceOff.Text = dt.Rows(i)("CDET_SERVICESOFFERED")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_PROFITABILITY")) = False Then
                        txtProfit.Text = dt.Rows(i)("CDET_PROFITABILITY")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_EMPLOYEESTRENGTH")) = False Then
                        txtEmpStrength.Text = dt.Rows(i)("CDET_EMPLOYEESTRENGTH")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_PUBLICPERCEPTION")) = False Then
                        txtPerceptionInPublic.Text = dt.Rows(i)("CDET_PUBLICPERCEPTION")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_GATHEREDBYAUDITFIRM")) = False Then
                        txtGatheredByFirm.Text = dt.Rows(i)("CDET_GATHEREDBYAUDITFIRM")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_GOVTPERCEPTION")) = False Then
                        txtPerceptionInGovt.Text = dt.Rows(i)("CDET_GOVTPERCEPTION")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_LITIGATIONISSUES")) = False Then
                        txtlegalIssues.Text = dt.Rows(i)("CDET_LITIGATIONISSUES")
                    End If
                    If IsDBNull(dt.Rows(i)("CDET_LEGALADVISORS")) = False Then
                        txtLglAdvisor.Text = dt.Rows(i)("CDET_LEGALADVISORS")
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustomerOtherDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindStatutoryRef(ByVal iLocationCustID As Integer, ByVal iLocationId As Integer)
        Dim dt As DataTable
        Try
            dt = objCust.BindStatutoryRef(sSession.AccessCode, sSession.AccessCodeID, iLocationCustID, iLocationId)
            gvStatutoryRef.DataSource = dt
            gvStatutoryRef.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatutoryRef" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub imgbtnSaveOther_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveOther.Click
        Dim txtSN, txtSV As New TextBox
        Dim objstrCustDetails As New strCustDetails
        Dim objstrCUSTAccountingTemplate As New strCUSTAccountingTemplate
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlOtherDetailsCust.SelectedIndex > 0 Then
                If txtLglAdvisor.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Legal Advisors exceeded maximum size(max 255 characters)." : lblError.Text = "Legal Advisors exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtLglAdvisor').focus();", True)
                    txtLglAdvisor.Focus()
                    Exit Sub
                End If
                If txturnover.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Turnover exceeded maximum size(max 255 characters)." : lblError.Text = "Turnover exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txturnover').focus();", True)
                    txturnover.Focus()
                    Exit Sub
                End If
                If txtProfit.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Profitability exceeded maximum size(max 255 characters)." : lblError.Text = "Profitability exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtProfit').focus();", True)
                    txtProfit.Focus()
                    Exit Sub
                End If
                If txtProdManufactured.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Products Manufactured exceeded maximum size(max 255 characters)." : lblError.Text = "Products Manufactured exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtProdManufactured').focus();", True)
                    txtProdManufactured.Focus()
                    Exit Sub
                End If
                If txtServiceOff.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Services Offered exceeded maximum size(max 255 characters)." : lblError.Text = "Services Offered exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtServiceOff').focus();", True)
                    txtServiceOff.Focus()
                    Exit Sub
                End If
                If txtStandingInIndustry.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Standing In Industry exceeded maximum size(max 255 characters)." : lblError.Text = "Standing In Industry exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtStandingInIndustry').focus();", True)
                    txtStandingInIndustry.Focus()
                    Exit Sub
                End If
                If txtForeignCollaboration.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Foreign Collaboration exceeded maximum size(max 255 characters)." : lblError.Text = "Foreign Collaboration exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtForeignCollaboration').focus();", True)
                    txtForeignCollaboration.Focus()
                    Exit Sub
                End If
                If txtEmpStrength.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Employee Strength exceeded maximum size(max 255 characters)." : lblError.Text = "Employee Strength exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtEmpStrength').focus();", True)
                    txtEmpStrength.Focus()
                    Exit Sub
                End If
                If txtFile.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "File No exceeded maximum size(max 500 characters)." : lblError.Text = "File No exceeded maximum size(max 500 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtFile').focus();", True)
                    txtFile.Focus()
                    Exit Sub
                End If

                If txtGatheredByFirm.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Gathered by the Audit Firm exceeded maximum size(max 255 characters)." : lblError.Text = "Gathered by the Audit Firm exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtGatheredByFirm').focus();", True)
                    txtGatheredByFirm.Focus()
                    Exit Sub
                End If
                If txtlegalIssues.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Major Litigation Issues if any exceeded maximum size(max 255 characters)." : lblError.Text = "Major Litigation Issues if any exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtlegalIssues').focus();", True)
                    txtlegalIssues.Focus()
                    Exit Sub
                End If
                If txtPerceptionInPublic.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Public Perception of the Org. exceeded maximum size(max 255 characters)." : lblError.Text = "Public Perception of the Org. exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtPerceptionInPublic').focus();", True)
                    txtPerceptionInPublic.Focus()
                    Exit Sub
                End If
                If txtPerceptionInGovt.Text.Trim.Length > 255 Then
                    lblCustomerValidationMsg.Text = "Government Perception of the Org. exceeded maximum size(max 255 characters)." : lblError.Text = "Government Perception of the Org. exceeded maximum size(max 255 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtPerceptionInGovt').focus();", True)
                    txtPerceptionInGovt.Focus()
                    Exit Sub
                End If
                objstrCustDetails.CDET_ID = 0
                objstrCustDetails.CDET_CUSTID = ddlOtherDetailsCust.SelectedValue
                objstrCustDetails.CDET_STANDINGININDUSTRY = txtStandingInIndustry.Text
                objstrCustDetails.CDET_PUBLICPERCEPTION = txtPerceptionInPublic.Text
                objstrCustDetails.CDET_GOVTPERCEPTION = txtPerceptionInGovt.Text
                objstrCustDetails.CDET_LITIGATIONISSUES = txtlegalIssues.Text
                objstrCustDetails.CDET_PRODUCTSMANUFACTURED = txtProdManufactured.Text
                objstrCustDetails.CDET_SERVICESOFFERED = txtServiceOff.Text
                objstrCustDetails.CDET_TURNOVER = txturnover.Text
                objstrCustDetails.CDET_PROFITABILITY = txtProfit.Text
                objstrCustDetails.CDET_FOREIGNCOLLABORATIONS = txtForeignCollaboration.Text
                objstrCustDetails.CDET_EMPLOYEESTRENGTH = txtEmpStrength.Text
                objstrCustDetails.CDET_PROFESSIONALSERVICES = ""
                objstrCustDetails.CDET_GATHEREDBYAUDITFIRM = txtGatheredByFirm.Text
                objstrCustDetails.CDET_LEGALADVISORS = txtLglAdvisor.Text
                objstrCustDetails.CDET_AUDITINCHARGE = ""
                objstrCustDetails.CDET_FileNo = txtFile.Text
                objstrCustDetails.CDET_CRBY = sSession.UserID
                objstrCustDetails.CDET_UpdatedBy = sSession.UserID
                objstrCustDetails.CDET_STATUS = "A"
                objstrCustDetails.CDET_IPAddress = sSession.IPAddress
                objstrCustDetails.CDET_CompID = sSession.AccessCodeID

                Arr = objCust.SaveCustomerDetails(sSession.AccessCode, objstrCustDetails)

                If Arr(0) = 2 Then
                    lblError.Text = "Successfully Updated."
                    lblCustomerValidationMsg.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                If Arr(0) = 3 Then
                    lblError.Text = "Successfully Saved."
                    lblCustomerValidationMsg.Text = "Successfully Saved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                If ddlOtherDetailsCust.SelectedIndex > 0 Then
                    'objCust.DeleteStatutoryRef(sSession.AccessCode, sSession.AccessCodeID, ddlOtherDetailsCust.SelectedValue, 0)
                    'If gvStatutoryRef.Rows.Count > 0 Then
                    '    For i = 0 To gvStatutoryRef.Rows.Count - 1
                    '        txtSN.Text = gvStatutoryRef.Rows(i).Cells(0).Text
                    '        txtSV.Text = gvStatutoryRef.Rows(i).Cells(1).Text
                    '        objstrCUSTAccountingTemplate.iCust_PKID = 0
                    '        objstrCUSTAccountingTemplate.iCust_ID = ddlOtherDetailsCust.SelectedValue
                    '        objstrCUSTAccountingTemplate.sCust_Desc = txtSN.Text
                    '        objstrCUSTAccountingTemplate.sCust_Value = txtSV.Text
                    '        objstrCUSTAccountingTemplate.sCust_Delflag = "A"
                    '        objstrCUSTAccountingTemplate.sCust_Status = "A"
                    '        objstrCUSTAccountingTemplate.iCust_AttchID = iAttachID
                    '        objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
                    '        objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
                    '        objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
                    '        objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
                    '        objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
                    '    Next
                    'End If
                    BindCustomerOtherDetails(ddlOtherDetailsCust.SelectedValue)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveOther_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub SaveStatutoryDetails()
        Dim objstrCUSTAccountingTemplate As New strCUSTAccountingTemplate
        Try
            objstrCUSTAccountingTemplate.iCust_PKID = 0
            objstrCUSTAccountingTemplate.iCust_ID = ddlLocationCust.SelectedValue
            objstrCUSTAccountingTemplate.sCust_Desc = ddlStatutoryReferences.SelectedItem.Text.Trim
            objstrCUSTAccountingTemplate.sCust_Value = txtStatutoryValue.Text.Trim
            objstrCUSTAccountingTemplate.sCust_Delflag = "A"
            objstrCUSTAccountingTemplate.sCust_Status = "A"
            objstrCUSTAccountingTemplate.iCust_AttchID = iAttachID
            objstrCUSTAccountingTemplate.iCust_CrBy = sSession.UserID
            objstrCUSTAccountingTemplate.iCust_UpdatedBy = sSession.UserID
            objstrCUSTAccountingTemplate.sCust_IPAddress = sSession.IPAddress
            objstrCUSTAccountingTemplate.iCust_Compid = sSession.AccessCodeID
            objstrCUSTAccountingTemplate.iCust_LocationId = lstboxLocation.SelectedValue
            objCust.SaveStatutoryRef(sSession.AccessCode, objstrCUSTAccountingTemplate)
            BindStatutoryRef(ddlLocationCust.SelectedValue, lstboxLocation.SelectedValue)
            ddlStatutoryReferences.SelectedIndex = 0
            txtStatutoryValue.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindStatutoryRef" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Protected Sub btnConfirmAdd_Click(sender As Object, e As EventArgs)
        Try
            SaveStatutoryDetails()
            lblCustomerValidationMsg.Text = "Successfully Added PAN details."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnConfirmDelete_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub btnStatutoryAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnStatutoryAdd.Click
        Try
            lblError.Text = ""
            If ddlLocationCust.SelectedIndex > 0 Then
                If lstboxLocation.SelectedIndex > -1 Then
                    If ddlStatutoryReferences.SelectedIndex = 0 Then
                        lblCustomerValidationMsg.Text = "Select Statutory References." : lblError.Text = "Select Statutory References."
                        ddlStatutoryReferences.Focus()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlStatutoryReferences').focus();", True)
                        Exit Sub
                    End If
                    If txtStatutoryValue.Text = "" Then
                        lblCustomerValidationMsg.Text = "Enter Reference." : lblError.Text = "Enter Reference."
                        txtStatutoryValue.Focus()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtStatutoryValue').focus();", True)
                        Exit Sub
                    End If
                    If txtStatutoryValue.Text.Trim.Length > 100 Then
                        lblCustomerValidationMsg.Text = "Reference exceeded maximum size(max 100 characters)." : lblError.Text = "Reference exceeded maximum size(max 255 characters)."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtStatutoryValue').focus();", True)
                        txtStatutoryValue.Focus()
                        Exit Sub
                    End If
                    'If objCust.CheckStatutory(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryName.Text, ddlLocationCust.SelectedValue) <> 0 Then
                    '    lblCustomerValidationMsg.Text = "Name Already exist." : lblError.Text = "Name Already exist."
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtStatutoryName').focus();", True)
                    '    txtStatutoryName.Focus()
                    '    Exit Sub
                    'End If
                    'If txtStatutoryName.Text.Trim = "PAN" And objCust.CheckPANWithOtherCust(sSession.AccessCode, sSession.AccessCodeID, txtStatutoryValue.Text.Trim, ddlLocationCust.SelectedValue) Then
                    '    lblConfirmAdd.Text = "Customer with this PAN already exists, Do you want to add same PAN?"
                    '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divAddConfirm').addClass('alert alert-warning');$('#ModalAddConfirmation').modal('show');", True)
                    '    Exit Sub
                    'End If
                    SaveStatutoryDetails()
                Else
                    lblCustomerValidationMsg.Text = "Select Existing Location." : lblError.Text = "Select Existing Location."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#lstboxLocation').focus();", True)
                    lstboxLocation.Focus()
                    Exit Sub
                End If
            Else
                lblCustomerValidationMsg.Text = "Select Customer." : lblError.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlOtherDeddlLocationCusttailsCust').focus();", True)
                ddlLocationCust.Focus()
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnStatutoryAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvStatutoryRef_PreRender(sender As Object, e As EventArgs) Handles gvStatutoryRef.PreRender
        Dim dt As New DataTable
        Try
            If gvStatutoryRef.Rows.Count > 0 Then
                gvStatutoryRef.UseAccessibleHeader = True
                gvStatutoryRef.HeaderRow.TableSection = TableRowSection.TableHeader
                gvStatutoryRef.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvStatutoryRef_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvStatutoryRef_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvStatutoryRef.RowCommand
        Dim dt As New DataTable
        Dim lblCustLOEPKID As New Label, lblCustLOEStatutoryRefAttachmentPKID As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            lblCustLOEPKID = DirectCast(clickedRow.FindControl("lblCustLOEPKID"), Label)
            lblCustLOEStatutoryRefAttachmentPKID = DirectCast(clickedRow.FindControl("lblCustLOEStatutoryRefAttachmentPKID"), Label)
            If ddlLocationCust.SelectedIndex > 0 Then
                If e.CommandName = "DeleteRow" Then
                    objCust.DeleteStatutoryRef(sSession.AccessCode, sSession.AccessCodeID, 0, lblCustLOEPKID.Text)
                    BindStatutoryRef(ddlLocationCust.SelectedValue, lstboxLocation.SelectedValue)
                End If
                If e.CommandName = "Attachment" Then
                    iAttachID = lblCustLOEStatutoryRefAttachmentPKID.Text
                    BindAllAttachments(sSession.AccessCode, iAttachID)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvStatutoryRef_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvStatutoryRef_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvStatutoryRef.RowDataBound
        Dim imgbtnAttachmentStatutoryReferences As New ImageButton, imgbtnStatutoryRefDelete As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatutoryRefDelete = CType(e.Row.FindControl("imgbtnStatutoryRefDelete"), ImageButton)
                imgbtnStatutoryRefDelete.ImageUrl = "~/Images/Trash16.png"
                imgbtnAttachmentStatutoryReferences = CType(e.Row.FindControl("imgbtnAttachmentStatutoryReferences"), ImageButton)
                imgbtnAttachmentStatutoryReferences.ImageUrl = "~/Images/Attachment16.png"
                'If sCDSave = "YES" Then
                gvStatutoryRef.Columns(2).Visible = True
                'Else
                '    gvStatutoryRef.Columns(2).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvStatutoryRef_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    '====================location======================
    Public Sub BindCustLocation()
        Try

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustLocation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlLocationCust_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocationCust.SelectedIndexChanged
        Try
            lblError.Text = ""
            ClearLocation()
            If ddlLocationCust.SelectedIndex > 0 Then
                BindLocations()
            Else
                lstboxLocation.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLocationCust_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub ClearLocation()
        Try
            lblError.Text = "" : txtLocationName.Text = "" : txtLocationAddress.Text = "" : txtContactPerson.Text = ""
            txtContactEmail.Text = "" : txtContactMobileNo.Text = "" : txtContactLandLineNo.Text = "" : txtDesignation.Text = ""
            imgbtnUpdateLoction.Visible = False : imgbtnSaveLocation.Visible = False
			ddlStatutoryReferences.SelectedIndex = 0 : txtStatutoryValue.Text = ""
            gvStatutoryRef.DataSource = Nothing
            gvStatutoryRef.DataBind()
            'If sCDSave = "YES" Then
            imgbtnSaveLocation.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearLocation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Public Sub BindLocations()
        Try
            lstboxLocation.DataSource = objCust.GetCustLocation(sSession.AccessCode, sSession.AccessCodeID, ddlLocationCust.SelectedValue)
            lstboxLocation.DataTextField = "Mas_Description"
            lstboxLocation.DataValueField = "Mas_Id"
            lstboxLocation.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindLocations" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lstboxLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstboxLocation.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ClearLocation()
            If ddlLocationCust.SelectedIndex > 0 Then
                If lstboxLocation.SelectedIndex > -1 Then
                    imgbtnUpdateLoction.Visible = False : imgbtnSaveLocation.Visible = False
                    'If sCDSave = "YES" Then
                    imgbtnUpdateLoction.Visible = True
                    'End If
                    txtLocationName.Text = lstboxLocation.SelectedItem.Text
                    dt = objCust.LoadLocationDetails(sSession.AccessCode, sSession.AccessCodeID, lstboxLocation.SelectedValue, ddlLocationCust.SelectedValue)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("Mas_Contact_Person")) = False Then
                            txtContactPerson.Text = dt.Rows(0)("Mas_Contact_Person")
                        End If
                        If IsDBNull(dt.Rows(0)("Mas_Contact_MobileNo")) = False Then
                            txtContactMobileNo.Text = dt.Rows(0)("Mas_Contact_MobileNo")
                        End If
                        If IsDBNull(dt.Rows(0)("Mas_Contact_LandLineNo")) = False Then
                            txtContactLandLineNo.Text = dt.Rows(0)("Mas_Contact_LandLineNo")
                        End If
                        If IsDBNull(dt.Rows(0)("Mas_Contact_Email")) = False Then
                            txtContactEmail.Text = dt.Rows(0)("Mas_Contact_Email")
                        End If
                        If IsDBNull(dt.Rows(0)("mas_Designation")) = False Then
                            txtDesignation.Text = dt.Rows(0)("mas_Designation")
                        End If
                        If IsDBNull(dt.Rows(0)("Mas_Loc_Address")) = False Then
                            txtLocationAddress.Text = dt.Rows(0)("Mas_Loc_Address")
                        End If
                    End If
					BindStatutoryRef(ddlLocationCust.SelectedValue, lstboxLocation.SelectedValue)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lstboxLocation_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnSaveLocation_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveLocation.Click
        Dim sCodeSave As String : Dim Arr() As String
        Dim objsCustLocation As New strCustLocation
        Try
            lblError.Text = ""
            If ddlLocationCust.SelectedIndex > 0 Then
                If txtLocationName.Text = "" Then
                    lblCustomerValidationMsg.Text = "Enter Loaction Name." : lblError.Text = "Enter Loaction Name."
                    txtLocationName.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtLocationName').focus();", True)
                    Exit Sub
                End If
                If txtContactPerson.Text = "" Then
                    lblCustomerValidationMsg.Text = "Enter Contact Person Name." : lblError.Text = "Enter Contact Person Name."
                    txtContactPerson.Focus()
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtContactPerson').focus();", True)
                    Exit Sub
                End If
                If lstboxLocation.SelectedIndex < 0 Then
                    If objCust.CheckCustLocation(sSession.AccessCode, sSession.AccessCodeID, Trim(txtLocationName.Text), ddlLocationCust.SelectedValue) <> 0 Then
                        lblError.Text = "This Location already exist."
                        lblCustomerValidationMsg.Text = "This Location already exist."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                        Exit Sub
                    End If
                End If
                If txtContactPerson.Text.Trim.Length > 50 Then
                    lblCustomerValidationMsg.Text = "Contact Person exceeded maximum size(max 50 characters)." : lblError.Text = "Contact Person exceeded maximum size(max 50 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtContactPerson').focus();", True)
                    txtContactPerson.Focus()
                    Exit Sub
                End If
                If txtLocationName.Text.Trim.Length > 100 Then
                    lblCustomerValidationMsg.Text = "Location Name exceeded maximum size(max 100 characters)." : lblError.Text = "Location Name exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtLocationName').focus();", True)
                    txtLocationName.Focus()
                    Exit Sub
                End If
                If txtContactMobileNo.Text.Trim.Length > 15 Then
                    lblCustomerValidationMsg.Text = "Contact Mobile number exceeded maximum size(max 15 characters)." : lblError.Text = "Contact Mobile number exceeded maximum size(max 15 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtContactMobileNo').focus();", True)
                    txtContactMobileNo.Focus()
                    Exit Sub
                End If
                If txtDesignation.Text.Trim.Length > 500 Then
                    lblCustomerValidationMsg.Text = "Designation exceeded maximum size(max 500 characters)." : lblError.Text = "Designation exceeded maximum size(max 500 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDesignation').focus();", True)
                    txtDesignation.Focus()
                    Exit Sub
                End If
                If txtContactLandLineNo.Text.Trim.Length > 50 Then
                    lblCustomerValidationMsg.Text = "Contact LandLine number exceeded maximum size(max 50 characters)." : lblError.Text = "Contact LandLine number exceeded maximum size(max 50 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtContactLandLineNo').focus();", True)
                    txtContactLandLineNo.Focus()
                    Exit Sub
                End If
                If txtContactEmail.Text.Trim.Length > 100 Then
                    lblCustomerValidationMsg.Text = "E-Mailexceeded maximum size(max 100 characters)." : lblError.Text = "E-Mail exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtContactEmail').focus();", True)
                    txtContactEmail.Focus()
                    Exit Sub
                End If
                If txtLocationAddress.Text.Trim.Length > 500 Then
                    lblCustomerValidationMsg.Text = "Address exceeded maximum size(max 500 characters)." : lblError.Text = "Address Name exceeded maximum size(max 500 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtLocationAddress').focus();", True)
                    txtLocationAddress.Focus()
                    Exit Sub
                End If
                sCodeSave = UCase(txtLocationName.Text.Substring(0, 2))
                If lstboxLocation.SelectedIndex > -1 Then
                    objsCustLocation.Mas_Id = lstboxLocation.SelectedValue
                Else
                    objsCustLocation.Mas_Id = 0
                End If
                objsCustLocation.Mas_code = sCodeSave
                objsCustLocation.Mas_Description = txtLocationName.Text
                objsCustLocation.Mas_DelFlag = "A"
                objsCustLocation.Mas_CustID = ddlLocationCust.SelectedValue
                objsCustLocation.Mas_Loc_Address = txtLocationAddress.Text
                objsCustLocation.Mas_Contact_Person = txtContactPerson.Text
                objsCustLocation.Mas_Contact_MobileNo = txtContactMobileNo.Text
                objsCustLocation.Mas_Contact_LandLineNo = txtContactLandLineNo.Text
                objsCustLocation.Mas_Contact_Email = txtContactEmail.Text
                objsCustLocation.mas_Designation = txtDesignation.Text
                objsCustLocation.Mas_CRBY = sSession.UserID
                objsCustLocation.Mas_UpdatedBy = sSession.UserID
                objsCustLocation.Mas_STATUS = "A"
                objsCustLocation.Mas_IPAddress = sSession.IPAddress
                objsCustLocation.Mas_CompID = sSession.AccessCodeID

                Arr = objCust.SaveCustomerLocation(sSession.AccessCode, objsCustLocation)
                If Arr(0) = 3 Then
                    lblError.Text = "Successfully Saved."
                    lblCustomerValidationMsg.Text = "Successfully Saved."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                If Arr(0) = 2 Then
                    lblError.Text = "Successfully Updated"
                    lblCustomerValidationMsg.Text = "Successfully Updated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                SaveLoactionID(ddlLocationCust.SelectedValue)
                BindLocations()
                lstboxLocation.SelectedValue = Arr(1)
                lstboxLocation_SelectedIndexChanged(sender, e)
            Else
                lblError.Text = "Select Customer"
                lblCustomerValidationMsg.Text = "Select Customer"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveLocation_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnUpdateLoction_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdateLoction.Click
        Try
            lblError.Text = ""
            imgbtnSaveLocation_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateLoction_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub SaveLoactionID(ByVal iCustID As Integer)
        Dim sLocIds As String = ""
        Dim dt As DataTable
        Dim iLocId As String
        Dim i As Integer
        Try
            dt = objCust.GetLocIDs(sSession.AccessCode, sSession.AccessCodeID, iCustID)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    iLocId = dt.Rows(i)("Mas_ID").ToString
                    sLocIds = sLocIds & "," & iLocId
                Next
            End If
            If sLocIds.StartsWith(",") Then
                sLocIds = sLocIds.Remove(0, 1)
            End If
            If sLocIds.EndsWith(",") Then
                sLocIds = sLocIds.Remove(Len(sLocIds) - 1, 1)
            End If
            objCust.UpdateCustMaster(sSession.AccessCode, sSession.AccessCodeID, iCustID, sLocIds)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateLoction_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub

    '=======================LOE=========================================
    Public Sub BindLOECustomers()
        Try

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindCustLocation" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadFinalcialYear()
        Dim iYearID As Integer
        Try
            ddlYear.DataSource = objclsGeneralFunctions.GetAddYearTo2DigitFinancialYear(sSession.AccessCode, sSession.AccessCodeID, 0)
            ddlYear.DataTextField = "YMS_ID"
            ddlYear.DataValueField = "YMS_YearID"
            ddlYear.DataBind()
            Try
                If ddlYear.SelectedValue > 0 Then
                    iYearID = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                    If iYearID > 0 Then
                        ddlYear.SelectedValue = iYearID
                    Else
                        ddlYear.SelectedIndex = 0
                    End If
                End If
            Catch ex As Exception
            End Try
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFinalcialYear" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnNewLOE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewLOE.Click
        Try
            lblError.Text = ""
            ddlExistingLOE.SelectedIndex = 0
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False : ddlTask.SelectedIndex = 0
            ClearLOE()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewLOE_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadLoc()
        Try
            ddlLOELocation.DataSource = objCust.GetCustLocation(sSession.AccessCode, sSession.AccessCodeID, ddlLOECustomers.SelectedValue)
            ddlLOELocation.DataTextField = "Mas_Description"
            ddlLOELocation.DataValueField = "Mas_Id"
            ddlLOELocation.DataBind()
            ddlLOELocation.Items.Insert(0, "All Locations")
            ddlLOELocation.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadLoc" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlFunction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlFunction.SelectedIndexChanged
        Try
            If ddlFunction.SelectedIndex > 0 Then
                lstSubFunction.Enabled = True
                LoadSubFunction()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlFunction_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadSubFunction()
        Try
            If ddlFunction.SelectedIndex > 0 Then
                lstSubFunction.DataSource = objclsAdminMaster.LoadAuditAssignmentSubTask(sSession.AccessCode, sSession.AccessCodeID, ddlFunction.SelectedValue)
                lstSubFunction.DataTextField = "AM_Name"
                lstSubFunction.DataValueField = "AM_ID"
                lstSubFunction.DataBind()
                lstSubFunction.Items.Insert(0, "Select Sub Tasks")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSubFunction" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindExistingLOE(ByVal iCustomerId As Integer)
        Try
            ddlExistingLOE.DataSource = objCust.LoadLOE(sSession.AccessCode, sSession.AccessCodeID, iCustomerId)
            ddlExistingLOE.DataTextField = "LOE_Name"
            ddlExistingLOE.DataValueField = "LOE_Id"
            ddlExistingLOE.DataBind()
            ddlExistingLOE.Items.Insert(0, "Select LOE")

            ddlExistingLOETemplate.DataSource = objCust.LoadLOE(sSession.AccessCode, sSession.AccessCodeID, iCustomerId)
            ddlExistingLOETemplate.DataTextField = "LOE_Name"
            ddlExistingLOETemplate.DataValueField = "LOE_Id"
            ddlExistingLOETemplate.DataBind()
            ddlExistingLOETemplate.Items.Insert(0, "Select LOE")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindExistingLOE" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub ddlLOECustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLOECustomers.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False : ddlTask.SelectedIndex = 0
            ClearLOE()
            If ddlLOECustomers.SelectedIndex > 0 Then
                BindExistingLOE(ddlLOECustomers.SelectedValue)

                lstboxLocation_SelectedIndexChanged(sender, e)
                imgbtnSaveLocation.Visible = False : imgbtnSaveOther.Visible = False : imgbtnUpdate.Visible = False
                imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOE.Visible = True
                'End If
                LoadLoc()

                ddlCustName.SelectedValue = ddlLOECustomers.SelectedValue
                ddlOtherDetailsCust.SelectedValue = ddlLOECustomers.SelectedValue
                ddlLocationCust.SelectedValue = ddlLOECustomers.SelectedValue
                ddlCompExistingCustomer.SelectedValue = ddlLOECustomers.SelectedValue
                ddlAsgExistingCustomer.SelectedValue = ddlLOECustomers.SelectedValue
                ddlLOETemplateCustomers.SelectedValue = ddlLOECustomers.SelectedValue
            Else
                ddlTask.SelectedIndex = 0 : ddlLOELocation.Items.Clear()
                cboCatList.SelectedIndex = 0 : cboReExp.SelectedIndex = 0 : ddlFunction.SelectedIndex = 0
                ddlFrequency.SelectedIndex = 0 : lstSubFunction.Items.Clear() : ddlExistingLOE.Items.Clear()

                ddlExistingLOETemplate.Items.Clear()
                BindAllAttachments(sSession.AccessCode, 0)
                lblLOETemplateFunName.Text = "" : lblLOETemplateFrequency.Text = "" : lblLOETemplateProfessionalFee.Text = ""
                lstScopeSubFun.Items.Clear() : lblFunId.Text = ""
                txtDeliverable.Text = "" : txtGeneral.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLOECustomers_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub ddlLOETemplateCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlLOETemplateCustomers.SelectedIndexChanged
        Try
            lblError.Text = ""
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False : ddlTask.SelectedIndex = 0
            ClearLOE()
            If ddlLOETemplateCustomers.SelectedIndex > 0 Then
                BindExistingLOE(ddlLOETemplateCustomers.SelectedValue)
                lstboxLocation_SelectedIndexChanged(sender, e)
                imgbtnSaveLocation.Visible = False : imgbtnSaveOther.Visible = False : imgbtnUpdate.Visible = False
                imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOE.Visible = True
                'End If
                LoadLoc()

                ddlCustName.SelectedValue = ddlLOETemplateCustomers.SelectedValue
                ddlOtherDetailsCust.SelectedValue = ddlLOETemplateCustomers.SelectedValue
                ddlLocationCust.SelectedValue = ddlLOETemplateCustomers.SelectedValue
                ddlCompExistingCustomer.SelectedValue = ddlLOETemplateCustomers.SelectedValue
                ddlAsgExistingCustomer.SelectedValue = ddlLOETemplateCustomers.SelectedValue
                ddlLOECustomers.SelectedValue = ddlLOETemplateCustomers.SelectedValue
            Else
                ddlTask.SelectedIndex = 0 : ddlLOELocation.Items.Clear()
                cboCatList.SelectedIndex = 0 : cboReExp.SelectedIndex = 0 : ddlFunction.SelectedIndex = 0
                ddlFrequency.SelectedIndex = 0 : lstSubFunction.Items.Clear() : ddlExistingLOE.Items.Clear()

                ddlExistingLOETemplate.Items.Clear()
                BindAllAttachments(sSession.AccessCode, 0)
                lblLOETemplateFunName.Text = "" : lblLOETemplateFrequency.Text = "" : lblLOETemplateProfessionalFee.Text = ""
                lstScopeSubFun.Items.Clear() : lblFunId.Text = ""
                txtDeliverable.Text = "" : txtGeneral.Text = ""
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlLOETemplateCustomers_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlExistingLOETemplate_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExistingLOETemplate.SelectedIndexChanged
        Try
            lblError.Text = ""
            lblLOETemplateFunName.Text = "" : lblLOETemplateFrequency.Text = "" : lblLOETemplateProfessionalFee.Text = ""
            lstScopeSubFun.Items.Clear() : lblFunId.Text = ""
            txtDeliverable.Text = "" : txtGeneral.Text = ""
            BindAllAttachments(sSession.AccessCode, 0)
            If ddlExistingLOETemplate.SelectedIndex > 0 Then
                LoadText(ddlExistingLOETemplate.SelectedValue)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingLOETemplate_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlExistingLOE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlExistingLOE.SelectedIndexChanged
        Dim dt As New DataTable
        Dim sListValue As String
        Dim aStr As Array
        Dim i As Integer, j As Integer
        Try
            lblError.Text = ""
            ClearLOE()
            If ddlExistingLOE.SelectedIndex > 0 Then
                imgbtnSaveLocation.Visible = False : imgbtnSaveOther.Visible = False : imgbtnUpdate.Visible = False
                imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
                'If sCDSave = "YES" Then
                imgbtnUpdateLOE.Visible = True
                'End If
                dt = objCust.DiplayLOE(sSession.AccessCode, sSession.AccessCodeID, ddlYear.SelectedValue, ddlExistingLOE.SelectedValue)
                If dt.Rows.Count > 0 Then
                    If IsDBNull(dt.Rows(0)("LOE_YearID")) = False Then
                        ddlYear.SelectedValue = dt.Rows(0)("LOE_YearID")
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_ServiceTypeID")) = False Then
                        ddlTask.SelectedValue = objclsGRACeGeneral.ReplaceSafeSQL((dt.Rows(0)("LOE_ServiceTypeID")))
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_NatureOfService")) = False Then
                        If dt.Rows(0)("LOE_NatureOfService") <> "" Then
                            txtNS.Text = dt.Rows(0)("LOE_NatureOfService")
                        End If
                    End If
                    LoadLoc()
                    If IsDBNull(dt.Rows(0)("LOE_LocationIds")) = False Then
                        If dt.Rows(0)("LOE_LocationIds") > 0 Then
                            ddlLOELocation.SelectedValue = dt.Rows(0)("LOE_LocationIds")
                        End If
                    End If
                    If (dt.Rows(0)("LOE_Frequency")) <> 0 Then
                        ddlFrequency.SelectedValue = dt.Rows(0)("LOE_Frequency")
                    End If
                    If dt.Rows(0)("LOE_FunctionId") <> 0 Then
                        ddlFunction.SelectedValue = dt.Rows(0)("LOE_FunctionId")
                    End If
                    LoadSubFunction()
                    If IsDBNull(dt.Rows(0)("LOE_SubFunctionId")) = False Then
                        sListValue = dt.Rows(0)("LOE_SubFunctionId")
                        If sListValue <> "" Then
                            aStr = sListValue.Split(",")
                            For i = 0 To UBound(aStr)
                                For j = 0 To lstSubFunction.Items.Count - 1
                                    If lstSubFunction.Items(j).Value = aStr(i) Then
                                        lstSubFunction.Items(j).Selected = True
                                    End If
                                Next
                            Next
                            lstSubFunction.Enabled = True
                        End If
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_Milestones")) = False Then
                        txtMs.Text = dt.Rows(0)("LOE_Milestones").ToString
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_TimeSchedule")) = False Then
                        txtStartDate.Text = dt.Rows(0)("LOE_TimeSchedule")
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_ReportDueDate")) = False Then
                        txtDueDate.Text = dt.Rows(0)("LOE_ReportDueDate")
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_ProfessionalFees")) = False Then
                        txtPFee.Text = dt.Rows(0)("LOE_ProfessionalFees").ToString
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_OtherFees")) = False Then
                        txtPExp.Text = dt.Rows(0)("LOE_OtherFees").ToString
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_ServiceTax")) = False Then
                        txtServiceTax.Text = dt.Rows(0)("LOE_ServiceTax").ToString
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_RembFilingFee")) = False Then
                        txtReambessFee.Text = dt.Rows(0)("LOE_RembFilingFee").ToString
                    End If
                    If IsDBNull(dt.Rows(0)("LOE_Total")) = False Then
                        txtTotalAmt.Text = dt.Rows(0)("LOE_Total").ToString
                    End If
                    gvResource.DataSource = objCust.LoadCategoryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOE.SelectedValue)
                    gvResource.DataBind()
                    gvCatRes.DataSource = objCust.LoadCategoryCodeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOE.SelectedValue)
                    gvCatRes.DataBind()
                    gvReAmbess.DataSource = objCust.LoadReambersmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOE.SelectedValue)
                    gvReAmbess.DataBind()

                    gvLOEDetails.DataSource = objCust.LoadLOEDetails(sSession.AccessCode, sSession.AccessCodeID, ddlLOECustomers.SelectedValue, ddlExistingLOE.SelectedValue, sSession.YearID)
                    gvLOEDetails.DataBind()
                End If
            Else
                imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False : ddlExistingLOE.SelectedIndex = 0
                'If sCDSave = "YES" Then
                imgbtnSaveLOE.Visible = True
                'End If
                ddlTask.SelectedIndex = 0 : ddlLOELocation.SelectedIndex = 0
                cboCatList.SelectedIndex = 0 : cboReExp.SelectedIndex = 0 : ddlFunction.SelectedIndex = 0
                ddlFrequency.SelectedIndex = 0 : lstSubFunction.Items.Clear()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingLOE_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub ClearLOE()
        Try
            lstSubFunction.Items.Clear() : ddlFrequency.SelectedIndex = 0 : ddlFunction.SelectedIndex = 0
            txtNS.Text = "" : txtMs.Text = "" : txtStartDate.Text = "" : txtDueDate.Text = "" : txtPFee.Text = "" : txtPExp.Text = ""
            txtReambessFee.Text = "" : txtTotalAmt.Text = "" : txtNR.Text = "" : txtServiceTax.Text = "" : txtdays.Text = "" : txtCatCode.Text = ""
            txtResources.Text = "" : txtServiceTax.Text = ""
            gvResource.DataSource = Nothing
            gvResource.DataBind()
            gvCatRes.DataSource = Nothing
            gvCatRes.DataBind()
            gvReAmbess.DataSource = Nothing
            gvReAmbess.DataBind()
            gvLOEDetails.DataSource = Nothing
            gvLOEDetails.DataBind()
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            'If sCDSave = "YES" Then
            imgbtnSaveLOE.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearLOE" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub gvLOEDetails_PreRender(sender As Object, e As EventArgs) Handles gvLOEDetails.PreRender
        Dim dt As New DataTable
        Try
            If gvLOEDetails.Rows.Count > 0 Then
                gvLOEDetails.UseAccessibleHeader = True
                gvLOEDetails.HeaderRow.TableSection = TableRowSection.TableHeader
                gvLOEDetails.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLOEDetails_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvLOEDetails_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvLOEDetails.RowCommand
        Dim dt As New DataTable
        Dim sListValue As String
        Dim aStr As Array
        Dim i As Integer, j As Integer, iLOEID As Integer
        Dim lblLOEID As New Label
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            lblLOEID = DirectCast(clickedRow.FindControl("lblLOEID"), Label)
            If e.CommandName = "Select" Then
                If ddlLocationCust.SelectedIndex > 0 Then
                    dt = objCust.LodeLOEDetails(sSession.AccessCode, sSession.AccessCodeID, ddlYear.SelectedValue, lblLOEID.Text)
                    If dt.Rows.Count > 0 Then
                        If IsDBNull(dt.Rows(0)("LOE_YearID")) = False Then
                            ddlYear.SelectedValue = dt.Rows(0)("LOE_YearID")
                        End If
                        BindExistingLOE(ddlLOECustomers.SelectedValue)
                        If IsDBNull(dt.Rows(0)("LOE_Id")) = False Then
                            ddlExistingLOE.SelectedValue = dt.Rows(0)("LOE_Id")
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_Id")) = False Then
                            iLOEID = dt.Rows(0)("LOE_Id")
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_ServiceTypeID")) = False Then
                            ddlTask.SelectedValue = objclsGRACeGeneral.ReplaceSafeSQL((dt.Rows(0)("LOE_ServiceTypeID")))
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_NatureOfService")) = False Then
                            txtNS.Text = dt.Rows(0)("LOE_NatureOfService")
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_LocationIds")) = False Then
                            If dt.Rows(0)("LOE_LocationIds") = 0 Then
                                ddlLOELocation.SelectedItem.Text = "All Locations"
                            Else
                                ddlLOELocation.SelectedValue = dt.Rows(0)("LOE_LocationIds")
                            End If
                        End If
                        If (dt.Rows(0)("LOE_Frequency")) <> 0 Then
                            ddlFrequency.SelectedValue = dt.Rows(0)("LOE_Frequency")
                        End If
                        If dt.Rows(0)("LOE_FunctionId") <> 0 Then
                            ddlFunction.SelectedValue = dt.Rows(0)("LOE_FunctionId")
                        End If
                        LoadSubFunction()
                        If IsDBNull(dt.Rows(0)("LOE_SubFunctionId")) = False Then
                            sListValue = dt.Rows(0)("LOE_SubFunctionId")
                            If sListValue <> "" Then
                                aStr = sListValue.Split(",")
                                For i = 0 To UBound(aStr)
                                    For j = 0 To lstSubFunction.Items.Count - 1
                                        If lstSubFunction.Items(j).Value = aStr(i) Then
                                            lstSubFunction.Items(j).Selected = True
                                        End If
                                    Next
                                Next
                                lstSubFunction.Enabled = True
                            End If
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_Milestones")) = False Then
                            txtMs.Text = dt.Rows(0)("LOE_Milestones").ToString
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_TimeSchedule")) = False Then
                            txtStartDate.Text = dt.Rows(0)("LOE_TimeSchedule")
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_ReportDueDate")) = False Then
                            txtDueDate.Text = dt.Rows(0)("LOE_ReportDueDate")
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_ProfessionalFees")) = False Then
                            txtPFee.Text = dt.Rows(0)("LOE_ProfessionalFees").ToString
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_OtherFees")) = False Then
                            txtPExp.Text = dt.Rows(0)("LOE_OtherFees").ToString
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_ServiceTax")) = False Then
                            txtServiceTax.Text = dt.Rows(0)("LOE_ServiceTax").ToString
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_RembFilingFee")) = False Then
                            txtReambessFee.Text = dt.Rows(0)("LOE_RembFilingFee").ToString
                        End If
                        If IsDBNull(dt.Rows(0)("LOE_Total")) = False Then
                            txtTotalAmt.Text = dt.Rows(0)("LOE_Total").ToString
                        End If
                        gvResource.DataSource = objCust.LoadCategoryDetails(sSession.AccessCode, sSession.AccessCodeID, iLOEID)
                        gvResource.DataBind()
                        gvCatRes.DataSource = objCust.LoadCategoryCodeDetails(sSession.AccessCode, sSession.AccessCodeID, iLOEID)
                        gvCatRes.DataBind()
                        gvReAmbess.DataSource = objCust.LoadReambersmentDetails(sSession.AccessCode, sSession.AccessCodeID, iLOEID)
                        gvReAmbess.DataBind()
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvLOEDetails_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnSaveLOE_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveLOE.Click
        Dim RetVal, iValue As Integer
        Dim dRDate As Date, dTDate As Date, dDate As Date, dSDate As Date
        Dim sListValues As String = "" : Dim sLOE_IDNAme As String
        Dim Arr() As String, ArrResource() As String, ArrAdditionalFees() As String
        Dim objstrLOE As New strLOE
        Dim objstrLOEResources As New strLOEResources
        Dim objstrLOEAdditionalFees As New strLOEAdditionalFees
        Dim objstrLOEReAmbersment As New strLOEReAmbersment
        Dim lblId As New Label, lblCategory As New Label, lblResource As New Label, lbldays As New Label, lblCharges As New Label, lblTotal As New Label
        Dim lblCatId As New Label, lblCatCategory As New Label, lblCatCode As New Label, lblCatRes As New Label
        Dim lblReAmbess As New Label, lblReambersment As New Label, lblReAmount As New Label
        Try
            lblError.Text = ""
            If ddlLOECustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                Exit Sub
            End If
            If ddlTask.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Task." : lblError.Text = "Select Task."
                ddlTask.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlTask').focus();", True)
                Exit Sub
            End If
            If ddlFrequency.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = " Select Frequency ." : lblError.Text = " Select Frequency ."
                ddlFrequency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlFrequency').focus();", True)
                Exit Sub
            End If
            If txtStartDate.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Start date." : lblError.Text = "Enter Start date ."
                txtStartDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtStartDate').focus();", True)
                Exit Sub
            End If
            If txtDueDate.Text = "" Then
                lblCustomerValidationMsg.Text = "Enter Due date for Report ." : lblError.Text = "Enter Due date for Report."
                txtDueDate.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDueDate').focus();", True)
                Exit Sub
            End If
            If ddlFunction.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Function." : lblError.Text = "Select Function."
                ddlFunction.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlFunction').focus();", True)
                Exit Sub
            End If
            'If lstSubFunction.SelectedIndex = 0 Then
            '    lblCustomerValidationMsg.Text = " Select Sub Function ." : lblError.Text = " Select Sub Function."
            '    lstSubFunction.Focus()
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#lstSubFunction').focus();", True)
            '    Exit Sub
            'End If

            If txtNS.Text.Trim.Length > 200 Then
                lblCustomerValidationMsg.Text = "Nature of Services exceeded maximum size(max 200 characters)." : lblError.Text = "Nature of Services exceeded maximum size(max 200 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtNS').focus();", True)
                txtNS.Focus()
                Exit Sub
            End If
            If txtMs.Text.Trim.Length > 100 Then
                lblCustomerValidationMsg.Text = "Milestones exceeded maximum size(max 100 characters)." : lblError.Text = "Milestones Code exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtMs').focus();", True)
                txtMs.Focus()
                Exit Sub
            End If
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim l As Integer
            l = DateDiff(DateInterval.Day, dDate, dSDate)
            If l < 0 Then
                lblCustomerValidationMsg.Text = "Start Date should be greater than or equal to Current Date."
                lblError.Text = "Start Date should be greater than or equal to Current Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtStartDate').focus();", True)
                txtStartDate.Focus()
                Exit Sub
            End If

            dTDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dRDate = DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim x As Integer = DateDiff(DateInterval.Day, dTDate, dRDate)
            If x < 0 Then
                lblError.Text = "Due date for Report(" & txtDueDate.Text & ") should be greater than or equal to Start date(" & txtStartDate.Text & ")."
                lblCustomerValidationMsg.Text = "Due date for Report(" & txtDueDate.Text & ") should be greater than or equal to Start date(" & txtStartDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                txtDueDate.Focus()
                Exit Sub
            End If
            sListValues = sGetListSubFunction()
            'If sListValues = "" Then
            '    lblError.Text = "Select Sub Function"
            '    lblCustomerValidationMsg.Text = "Select Sub Function"
            '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
            '    lstSubFunction.Focus()
            '    Exit Sub
            'End If

            lnkbtnFee_Click(sender, e)

            sLOE_IDNAme = objclsGeneralFunctions.GetAllModuleJobCode(sSession.AccessCode, sSession.AccessCodeID, "LOE", sSession.YearID, sSession.YearName, ddlLOECustomers.SelectedValue)

            If IsNumeric(txtServiceTax.Text.Trim) = False Then
                lblError.Text = "Enter valid Service TAX(only numbers)."
                lblCustomerValidationMsg.Text = "Enter valid Service TAX(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                txtServiceTax.Focus()
                Exit Sub
            End If
            If ddlExistingLOE.SelectedIndex > 0 Then
                objstrLOE.LOE_Id = ddlExistingLOE.SelectedValue
            Else
                objstrLOE.LOE_Id = 0
            End If
            objstrLOE.LOE_YearId = ddlYear.SelectedValue
            objstrLOE.LOE_CustomerId = ddlLOECustomers.SelectedValue
            objstrLOE.LOE_ServiceTypeId = ddlTask.SelectedValue
            objstrLOE.LOE_NatureOfService = txtNS.Text
            If ddlLOELocation.SelectedIndex > 0 Then
                objstrLOE.LOE_LocationIds = ddlLOELocation.SelectedValue
            Else
                objstrLOE.LOE_LocationIds = 0
            End If
            objstrLOE.LOE_Milestones = txtMs.Text
            objstrLOE.LOE_TimeSchedule = txtStartDate.Text
            objstrLOE.LOE_ReportDueDate = txtDueDate.Text
            objstrLOE.LOE_ProfessionalFees = txtPFee.Text
            objstrLOE.LOE_OtherFees = txtPExp.Text
            objstrLOE.LOE_ServiceTax = txtServiceTax.Text
            objstrLOE.LOE_RembFilingFee = txtReambessFee.Text
            objstrLOE.LOE_CrBy = sSession.UserID
            objstrLOE.LOE_Total = txtTotalAmt.Text
            objstrLOE.LOE_Name = sLOE_IDNAme
            objstrLOE.LOE_Frequency = ddlFrequency.SelectedValue
            objstrLOE.LOE_FunctionId = ddlFunction.SelectedValue
            objstrLOE.LOE_SubFunctionId = sListValues
            objstrLOE.LOE_UpdatedBy = sSession.UserID
            objstrLOE.LOE_STATUS = "A"
            objstrLOE.LOE_IPAddress = sSession.IPAddress
            objstrLOE.LOE_CompID = sSession.AccessCodeID

            Arr = objCust.SaveCustomerLOE(sSession.AccessCode, objstrLOE)
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved."
                lblCustomerValidationMsg.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            If Arr(0) = 2 Then
                lblError.Text = "Successfully Updated"
                lblCustomerValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            RetVal = Arr(1)
            If gvResource.Rows.Count >= 0 Then
                If Arr(0) <> 3 Then
                    objCust.DeleteResourceDetailsIfExists(sSession.AccessCode, sSession.AccessCodeID, RetVal)
                End If
                For i As Integer = 0 To gvResource.Rows.Count - 1
                    lblId = gvResource.Rows(i).FindControl("lblID")
                    lblCategory = gvResource.Rows(i).FindControl("lblCategory")
                    lblResource = gvResource.Rows(i).FindControl("lblResource")
                    lbldays = gvResource.Rows(i).FindControl("lbldays")
                    lblCharges = gvResource.Rows(i).FindControl("lblCharges")
                    lblTotal = gvResource.Rows(i).FindControl("lblTotal")
                    If lblTotal.Text = "" Then
                        iValue = 0
                    Else
                        iValue = lblTotal.Text
                    End If
                    objstrLOEResources.iLOER_ID = 0
                    objstrLOEResources.iLOER_LOEID = RetVal
                    objstrLOEResources.iLOER_CategoryID = lblId.Text
                    objstrLOEResources.iLOER_NoResources = lblResource.Text
                    objstrLOEResources.iLOER_ChargesPerDay = lblCharges.Text
                    objstrLOEResources.sLOER_CategoryName = objclsGRACeGeneral.SafeSQL(lblCategory.Text)
                    objstrLOEResources.iLOER_NoDays = lbldays.Text
                    objstrLOEResources.iLOER_ResTotal = iValue
                    objstrLOEResources.sLOER_Delflag = "A"
                    objstrLOEResources.sLOER_STATUS = "C"
                    objstrLOEResources.iLOER_CrBy = sSession.UserID
                    objstrLOEResources.iLOER_UpdatedBy = sSession.UserID
                    objstrLOEResources.sLOER_IPAddress = sSession.IPAddress
                    objstrLOEResources.iLOER_CompID = sSession.AccessCodeID
                    ArrResource = objCust.SaveResourceDetails(sSession.AccessCode, objstrLOEResources)
                Next
            End If
            If gvCatRes.Rows.Count >= 0 Then
                If Arr(0) <> 3 Then
                    objCust.DeleteExpensesDetailsIfExists(sSession.AccessCode, sSession.AccessCodeID, RetVal)
                End If
                For i As Integer = 0 To gvCatRes.Rows.Count - 1
                    lblCatId = gvCatRes.Rows(i).FindControl("lblID")
                    lblCatCategory = gvCatRes.Rows(i).FindControl("lblCategory")
                    lblCatCode = gvCatRes.Rows(i).FindControl("lblCode")
                    lblCatRes = gvCatRes.Rows(i).FindControl("lblAmount")

                    objstrLOEAdditionalFees.iLAF_ID = 0
                    objstrLOEAdditionalFees.iLAF_LOEID = RetVal
                    objstrLOEAdditionalFees.iLAF_OtherExpensesID = lblCatId.Text
                    objstrLOEAdditionalFees.iLAF_Charges = lblCatRes.Text
                    objstrLOEAdditionalFees.sLAF_CODE = objclsGRACeGeneral.SafeSQL(lblCatCode.Text)
                    objstrLOEAdditionalFees.sLAF_OtherExpensesName = objclsGRACeGeneral.SafeSQL(lblCatCategory.Text)
                    objstrLOEAdditionalFees.sLAF_Delflag = "A"
                    objstrLOEAdditionalFees.sLAF_STATUS = "C"
                    objstrLOEAdditionalFees.iLAF_CrBy = sSession.UserID
                    objstrLOEAdditionalFees.iLAF_UpdatedBy = sSession.UserID
                    objstrLOEAdditionalFees.sLAF_IPAddress = sSession.IPAddress
                    objstrLOEAdditionalFees.iLAF_CompID = sSession.AccessCodeID
                    ArrAdditionalFees = objCust.SaveCategoryDetails(sSession.AccessCode, objstrLOEAdditionalFees)
                Next
            End If
            If gvReAmbess.Rows.Count >= 0 Then
                If Arr(0) <> 3 Then
                    objCust.DeleteReambersementDetailsIfExists(sSession.AccessCode, sSession.AccessCodeID, RetVal)
                End If
                For i As Integer = 0 To gvReAmbess.Rows.Count - 1
                    lblReAmbess = gvReAmbess.Rows(i).FindControl("lblID")
                    lblReambersment = gvReAmbess.Rows(i).FindControl("lblReambersment")
                    lblReAmount = gvReAmbess.Rows(i).FindControl("lblReAmount")

                    objstrLOEReAmbersment.iLAR_ID = 0
                    objstrLOEReAmbersment.iLAR_LOEID = RetVal
                    objstrLOEReAmbersment.iLAR_ReambersmentID = lblReAmbess.Text
                    objstrLOEReAmbersment.iLAR_Charges = lblReAmount.Text
                    objstrLOEReAmbersment.sLAR_ReambName = objclsGRACeGeneral.SafeSQL(lblReambersment.Text)
                    objstrLOEReAmbersment.sLAR_Delflag = "A"
                    objstrLOEReAmbersment.sLAR_STATUS = "C"
                    objstrLOEReAmbersment.iLAR_CrBy = sSession.UserID
                    objstrLOEReAmbersment.iLAR_UpdatedBy = sSession.UserID
                    objstrLOEReAmbersment.sLAR_IPAddress = sSession.IPAddress
                    objstrLOEReAmbersment.iLAR_CompID = sSession.AccessCodeID
                    objCust.SaveReambersmentDetails(sSession.AccessCode, objstrLOEReAmbersment)
                Next
            End If
            BindExistingLOE(ddlLOECustomers.SelectedValue)
            ddlExistingLOE.SelectedValue = Arr(1)
            ddlExistingLOE_SelectedIndexChanged(sender, e)


        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveLOE_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnUpdateLOE_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdateLOE.Click
        Try
            lblError.Text = ""
            imgbtnSaveLOE_Click(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateLOE_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Function sGetListSubFunction() As String
        Dim sList As String = ""
        Try
            For i = 0 To lstSubFunction.Items.Count - 1
                If lstSubFunction.Items(i).Selected = True Then
                    sList = sList & "," & lstSubFunction.Items(i).Value
                End If
            Next
            If sList.StartsWith(",") Then
                sList = sList.Remove(0, 1)
            End If
            Return sList
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "sGetListSubFunction" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Function
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim iRate As Integer
        Dim dRow As DataRow
        Dim bflag As Boolean = True
        Dim lblID As New Label, lblCategory As New Label, lblResource As New Label, lblCharges As New Label, lbldays As New Label, lblTotal As New Label
        Try
            lblError.Text = ""
            If ddlLOECustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer" : lblCustomerValidationMsg.Text = "Select Customer"
                ddlLOECustomers.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlLOECustomers').focus();", True)
                Exit Sub
            End If
            If ddlCat.SelectedIndex <> 0 Then
                Dim dtRes As New DataTable
                dtRes.Columns.Add("Id")
                dtRes.Columns.Add("Cat")
                dtRes.Columns.Add("Res")
                dtRes.Columns.Add("days")
                dtRes.Columns.Add("Charge")
                dtRes.Columns.Add("Total")

                For i = 0 To gvResource.Rows.Count - 1
                    lblID = gvResource.Rows(i).FindControl("lblID")
                    lblCategory = gvResource.Rows(i).FindControl("lblCategory")
                    lblResource = gvResource.Rows(i).FindControl("lblResource")
                    lblCharges = gvResource.Rows(i).FindControl("lblCharges")
                    lbldays = gvResource.Rows(i).FindControl("lbldays")
                    lblTotal = gvResource.Rows(i).FindControl("lblTotal")

                    If lblID.Text = ddlCat.SelectedValue Then
                        dRow = dtRes.NewRow
                        dRow("Id") = ddlCat.SelectedValue
                        dRow("Cat") = ddlCat.SelectedItem.Text
                        dRow("Res") = Val(txtNR.Text)
                        dRow("days") = Val(txtdays.Text)
                        iRate = objCust.GetLOERate(sSession.AccessCode, sSession.AccessCodeID, ddlCat.SelectedValue)
                        dRow("Charge") = iRate
                        dRow("Total") = (dRow("days") * iRate) * Val(txtNR.Text)
                        bflag = False
                    Else
                        dRow = dtRes.NewRow
                        dRow("Id") = lblID.Text
                        dRow("Cat") = lblCategory.Text
                        dRow("Res") = lblResource.Text
                        dRow("days") = lbldays.Text
                        dRow("Charge") = lblCharges.Text
                        dRow("Total") = lblTotal.Text
                    End If
                    dtRes.Rows.Add(dRow)
                Next
                If bflag = True Then
                    dRow = dtRes.NewRow
                    dRow("Id") = ddlCat.SelectedValue
                    dRow("Cat") = ddlCat.SelectedItem.Text
                    dRow("Res") = Val(txtNR.Text)
                    dRow("days") = Val(txtdays.Text)
                    iRate = objCust.GetLOERate(sSession.AccessCode, sSession.AccessCodeID, ddlCat.SelectedValue)
                    dRow("Charge") = iRate
                    dRow("Total") = (dRow("days") * iRate) * Val(txtNR.Text)
                    dtRes.Rows.Add(dRow)
                End If
                gvResource.DataSource = dtRes
                gvResource.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#catModal').modal('show');", True)
            End If
            txtNR.Text = "" : txtdays.Text = ""
            ddlCat.SelectedIndex = 0
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnAddCatList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddCatList.Click
        Dim dtcat As New DataTable
        Dim dItem As DataGridItem
        Dim dr As DataRow
        Dim sFlag As Boolean = False
        Dim iCount As Integer
        Try
            lblError.Text = ""
            If ddlLOECustomers.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer." : lblError.Text = "Select Customer."
                ddlLOECustomers.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlLOECustomers').focus();", True)
                Exit Sub
            End If
            dtcat.Columns.Add("ID")
            dtcat.Columns.Add("Category")
            dtcat.Columns.Add("CatCode")
            dtcat.Columns.Add("CatRes")

            If gvCatRes.Rows.Count > 0 Then
                For Each dItem In gvCatRes.Rows
                    dr = dtcat.NewRow
                    dr("ID") = dItem.Cells(0).Text
                    dr("Category") = dItem.Cells(1).Text
                    dr("CatCode") = dItem.Cells(2).Text
                    dr("CatRes") = dItem.Cells(3).Text
                    dtcat.Rows.Add(dr)
                Next
                For i As Integer = 0 To dtcat.Rows.Count - 1
                    If dtcat.Rows(i).Item(0).ToString = cboCatList.SelectedItem.Value Then
                        sFlag = True
                        iCount = i
                    End If
                Next
                If sFlag = True Then
                    dtcat.Rows(iCount).Delete()
                End If
                dr = dtcat.NewRow
                dr("ID") = cboCatList.SelectedItem.Value
                dr("Category") = cboCatList.SelectedItem.Text
                dr("CatCode") = txtCatCode.Text
                If txtResources.Text = "" Then
                    dr("CatRes") = 0
                Else
                    dr("CatRes") = txtResources.Text
                End If
                dtcat.Rows.Add(dr)
            Else
                dr = dtcat.NewRow
                dr("ID") = cboCatList.SelectedItem.Value
                dr("Category") = cboCatList.SelectedItem.Text
                dr("CatCode") = txtCatCode.Text
                If txtResources.Text = "" Then
                    dr("CatRes") = 0
                Else
                    dr("CatRes") = txtResources.Text
                End If
                dtcat.Rows.Add(dr)
            End If
            gvCatRes.DataSource = dtcat
            gvCatRes.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#otherModal').modal('show');", True)
            cboCatList.SelectedIndex = 0
            txtCatCode.Text = "" : txtResources.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddCatList_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnReAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReAdd.Click
        Dim dtcat As New DataTable
        Dim dItem As DataGridItem
        Dim dr As DataRow
        Dim sFlag As Boolean = False
        Dim iCount As Integer
        Dim lblID As New Label, lblReambersment As New Label, lblReAmount As New Label
        Try
            lblError.Text = ""
            If ddlLOECustomers.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer." : lblError.Text = "Select Customer."
                ddlLOECustomers.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlLOECustomers').focus();", True)
                Exit Sub
            End If
            dtcat.Columns.Add("ID")
            dtcat.Columns.Add("Reambersment")
            dtcat.Columns.Add("ReAmount")
            If gvReAmbess.Rows.Count > 0 Then
                For i = 0 To gvReAmbess.Rows.Count - 1
                    dr = dtcat.NewRow
                    lblID = gvReAmbess.Rows(i).FindControl("lblID")
                    dr("ID") = lblID.Text
                    lblReambersment = gvReAmbess.Rows(i).FindControl("lblReambersment")
                    dr("Reambersment") = lblReambersment.Text
                    lblReAmount = gvReAmbess.Rows(i).FindControl("lblReAmount")
                    dr("ReAmount") = lblReAmount.Text
                    dtcat.Rows.Add(dr)
                Next
                'For Each dItem In gvReAmbess.Rows
                '    dr = dtcat.NewRow
                '    dr("ID") = dItem.Cells(0).Text
                '    dr("Reambersment") = dItem.Cells(1).Text
                '    dr("ReAmount") = dItem.Cells(2).Text
                '    dtcat.Rows.Add(dr)
                'Next
                For i As Integer = 0 To dtcat.Rows.Count - 1
                    If dtcat.Rows(i).Item(0).ToString = cboReExp.SelectedItem.Value Then
                        sFlag = True
                        iCount = i
                    End If
                Next
                If sFlag = True Then
                    dtcat.Rows(iCount).Delete()
                End If
                dr = dtcat.NewRow
                dr("ID") = cboReExp.SelectedItem.Value
                dr("Reambersment") = cboReExp.SelectedItem.Text
                If txtReAmt.Text = "" Then
                    dr("ReAmount") = 0
                Else
                    dr("ReAmount") = txtReAmt.Text
                End If
                dtcat.Rows.Add(dr)
            Else
                dr = dtcat.NewRow
                dr("ID") = cboReExp.SelectedItem.Value
                dr("Reambersment") = cboReExp.SelectedItem.Text
                If txtReAmt.Text = "" Then
                    dr("ReAmount") = 0
                Else
                    dr("ReAmount") = txtReAmt.Text
                End If
                dtcat.Rows.Add(dr)
            End If
            gvReAmbess.DataSource = dtcat
            gvReAmbess.DataBind()
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ReAmbessModal').modal('show');", True)
            cboReExp.SelectedIndex = 0
            txtReAmt.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnReAdd_Click" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvDet_PreRender(sender As Object, e As EventArgs) Handles gvDet.PreRender
        Dim dt As New DataTable
        Try
            If gvDet.Rows.Count > 0 Then
                gvDet.UseAccessibleHeader = True
                gvDet.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDet.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDet_PreRender" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub lnkbtnLoadGrid_Click(sender As Object, e As EventArgs) Handles lnkbtnLoadGrid.Click
        Dim dRow As DataRow
        Dim sRes As String = ""
        Dim lblCategory As New Label, lblResource As New Label
        Try
            lblError.Text = ""
            gvDet.DataSource = Nothing
            gvDet.DataBind()
            If ddlLOECustomers.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Customer." : lblError.Text = "Select Customer."
                ddlLOECustomers.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlLOECustomers').focus();", True)
                Exit Sub
            End If
            If ddlTask.SelectedIndex <> 0 Then
                Dim dtDet As New DataTable
                dtDet.Columns.Add("CId")
                dtDet.Columns.Add("Task")
                dtDet.Columns.Add("NServ")
                dtDet.Columns.Add("Loc")
                dtDet.Columns.Add("MileStone")
                dtDet.Columns.Add("STime")
                dtDet.Columns.Add("DDate")
                dtDet.Columns.Add("PFee")
                dtDet.Columns.Add("PExp")
                dtDet.Columns.Add("Tax")
                dtDet.Columns.Add("Resource")

                dRow = dtDet.NewRow
                dRow("CId") = ddlCustName.SelectedValue
                dRow("Task") = ddlTask.SelectedItem.Text
                dRow("NServ") = txtNS.Text
                dRow("Loc") = ddlLOELocation.SelectedItem.Text
                dRow("MileStone") = txtMs.Text
                dRow("STime") = txtStartDate.Text
                dRow("DDate") = txtDueDate.Text
                dRow("PFee") = Val(txtPFee.Text)
                dRow("PExp") = Val(txtPExp.Text)
                dRow("Tax") = Val(txtServiceTax.Text)
                For i = 0 To gvResource.Rows.Count - 1
                    lblCategory = gvResource.Rows(i).FindControl("lblCategory")
                    lblResource = gvResource.Rows(i).FindControl("lblResource")
                    sRes = lblCategory.Text & "-" & lblResource.Text & "</br>" & sRes
                Next
                dRow("Resource") = sRes
                dtDet.Rows.Add(dRow)

                gvDet.DataSource = dtDet
                gvDet.DataBind()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#gridModal').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLoadGrid_Click" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvCatRes_PreRender(sender As Object, e As EventArgs) Handles gvCatRes.PreRender
        Dim dt As New DataTable
        Try
            If gvCatRes.Rows.Count > 0 Then
                gvCatRes.UseAccessibleHeader = True
                gvCatRes.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCatRes.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCatRes_PreRender" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnFee_Click(sender As Object, e As EventArgs) Handles lnkbtnFee.Click
        Dim sSql As String = "" : Dim iProf As Double : Dim iCharge, i, iRate As Integer : Dim FeeAmt As Integer = 0 : Dim TotAmt As Integer = 0 : Dim FeeAmts As Integer = 0
        Dim TotVal As Integer = 0 : Dim FeeAmtss As Integer = 0
        Dim lblId As New Label, lblResource As New Label, lblTotal As New Label, lblCatRes As New Label, lblReAmount As New Label
        Try
            lblError.Text = ""
            If ddlLOECustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer"
                Exit Sub
            End If
            If txtServiceTax.Text = "" Then
                txtServiceTax.Text = 0
            End If
            If txtNR.Text <> "" And ddlCat.SelectedIndex <> 0 Then
                If gvResource.Rows.Count <> 0 Then
                    For i = 0 To gvResource.Rows.Count - 1
                        lblId = gvResource.Rows(i).FindControl("lblID")
                        lblResource = gvResource.Rows(i).FindControl("lblResource")
                        iRate = lblId.Text
                        iCharge = objCust.GetLOERate(sSession.AccessCode, sSession.AccessCodeID, iRate)
                        iProf = iCharge * Val(lblResource.Text)
                        txtPFee.Text = iProf + Val(txtPFee.Text)
                    Next
                End If
            End If
            For i = 0 To gvResource.Rows.Count - 1
                lblTotal = gvResource.Rows(i).FindControl("lblTotal")
                TotVal = TotVal + lblTotal.Text
            Next

            txtPFee.Text = TotVal
            For i = 0 To gvCatRes.Rows.Count - 1
                lblCatRes = gvCatRes.Rows(i).FindControl("lblAmount")
                FeeAmt = FeeAmt + lblCatRes.Text
            Next
            txtPExp.Text = FeeAmt
            For i = 0 To gvReAmbess.Rows.Count - 1
                lblReAmount = gvReAmbess.Rows(i).FindControl("lblReAmount")
                FeeAmts = FeeAmts + lblReAmount.Text
            Next
            txtReambessFee.Text = FeeAmts
            txtTotalAmt.Text = Val(txtPFee.Text) + Val(txtPExp.Text) + Val(txtReambessFee.Text) + Val(txtServiceTax.Text)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFee_Click" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvResource_PreRender(sender As Object, e As EventArgs) Handles gvResource.PreRender
        Dim dt As New DataTable
        Try
            If gvResource.Rows.Count > 0 Then
                gvResource.UseAccessibleHeader = True
                gvResource.HeaderRow.TableSection = TableRowSection.TableHeader
                gvResource.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResource_PreRender" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvResource_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvResource.RowCommand
        Dim dtDRL As New DataTable
        Dim r As Integer
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblID As Label = DirectCast(clickedRow.FindControl("lblID"), Label)
            If e.CommandName = "DeleteRow" Then
                If ddlExistingLOE.SelectedIndex > 0 Then
                    dtDRL = objCust.LoadCategoryDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOE.SelectedValue)
                    For r = 0 To dtDRL.Rows.Count - 1
                        If dtDRL.Rows(r).Item("Id") = lblID.Text Then
                            dtDRL.Rows(r).Delete()
                            dtDRL.AcceptChanges()
                            gvResource.DataSource = dtDRL
                            gvResource.DataBind()
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#catModal').modal('show');", True)
                            Exit Sub
                        End If
                    Next
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#catModal').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResource_RowCommand" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvReAmbess_PreRender(sender As Object, e As EventArgs) Handles gvReAmbess.PreRender
        Dim dt As New DataTable
        Try
            If gvReAmbess.Rows.Count > 0 Then
                gvReAmbess.UseAccessibleHeader = True
                gvReAmbess.HeaderRow.TableSection = TableRowSection.TableHeader
                gvReAmbess.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReAmbess_PreRender" & " & Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvReAmbess_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvReAmbess.RowCommand
        Dim dtDRL As New DataTable
        Dim r As Integer
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblID As Label = DirectCast(clickedRow.FindControl("lblID"), Label)
            If e.CommandName = "DeleteRow" Then
                dtDRL = objCust.LoadReambersmentDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOE.SelectedValue)
                For r = 0 To dtDRL.Rows.Count - 1
                    If dtDRL.Rows(r).Item("Id") = lblID.Text Then
                        dtDRL.Rows(r).Delete()
                        dtDRL.AcceptChanges()
                        gvReAmbess.DataSource = dtDRL
                        gvReAmbess.DataBind()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ReAmbessModal').modal('show');", True)
                        Exit Sub
                    End If
                Next
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReAmbess_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvCatRes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCatRes.RowCommand
        Dim dtDRL As New DataTable
        Dim r As Integer
        Try
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblID As Label = DirectCast(clickedRow.FindControl("lblID"), Label)
            If e.CommandName = "DeleteRow" Then
                dtDRL = objCust.LoadCategoryCodeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOE.SelectedValue)
                For r = 0 To dtDRL.Rows.Count - 1
                    If dtDRL.Rows(r).Item("Id") = lblID.Text Then
                        dtDRL.Rows(r).Delete()
                        dtDRL.AcceptChanges()
                        gvCatRes.DataSource = dtDRL
                        gvCatRes.DataBind()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#otherModal').modal('show');", True)
                        Exit Sub
                    End If
                Next
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCatRes_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Public Sub LoadText(ByVal iExistingLOEId As Integer)
        Dim sStdIntAudit, sRoles, sInfrastructure, sConfidential As String
        Dim dt As New DataTable
        Dim sListValue As String
        Dim aStr As Array
        Try
            lblError.Text = "" : iAttachID = 0
            txtStdIntAudit.Text = objclsReportTemplate.GetReportTemplateForLOE(sSession.AccessCode, sSession.AccessCodeID, 5, "Responsibilities of the Auditor")
            txtDeliverable.Text = objclsReportTemplate.GetReportTemplateForLOE(sSession.AccessCode, sSession.AccessCodeID, 5, "The objective and scope of the audit")
            txtRoles.Text = objclsReportTemplate.GetReportTemplateForLOE(sSession.AccessCode, sSession.AccessCodeID, 5, "Reporting")
            txtInfrastructure.Text = objclsReportTemplate.GetReportTemplateForLOE(sSession.AccessCode, sSession.AccessCodeID, 5, "The responsibilities of management And identification of the applicable financial reporting framework")
            txtGeneral.Text = objclsReportTemplate.GetReportTemplateForLOE(sSession.AccessCode, sSession.AccessCodeID, 5, "General")
            txtConfidential.Text = objclsReportTemplate.GetReportTemplateForLOE(sSession.AccessCode, sSession.AccessCodeID, 5, "Non Disclosure Of Confidential Information")

            'txtStdIntAudit.Text = "Adequate number of qualified staff, trained in internal audit along with a senior partner should conduct the internal audit. "
            'txtStdIntAudit.Text = txtStdIntAudit.Text & "It is expected that the internal auditors would maintain high standards of auditing, integrity and transparency in reporting.  The external auditors would review the reports of the internal audit in the statutory audit process."
            'txtRoles.Text = "Internal auditors have the full powers of verification and access to information as is available to the statutory auditors. They are also free to approach the Chief Executive Officer or Chief Financial Officer, should the circumstances warrant. "
            'txtRoles.Text = txtRoles.Text & " The final internal audit report will be Shared With the Board Of Directors/Partners. We encourage the internal auditors To participate In the meeting With the Board And communicate With the members In executive sessions."
            'txtInfrastructure.Text = "Required number Of systems In a separate And exclusive working area To accommodate an audit team Of 3 To 4 personnel will be provided."
            'txtConfidential.Text = "Possesses competitively valuable Confidential Information regarding its past, current And future services And products, research And development, customers, business plans, software And general business operations."
            'txtConfidential.Text = txtConfidential.Text & "•  The recipient may be given access To the Companys Confidential Information or to create a new Confidential Information for the Company.Restrictions on use"
            'txtConfidential.Text = txtConfidential.Text & "•	The recipient will not disclose any Confidential Information to third parties for any purpose without the prior written consent of the Company.  However, where the recipient is required to disclose Confidential Information in accordance with judicial or other governmental action, the recipient will give the Company reasonable prior notice. "
            'txtConfidential.Text = txtConfidential.Text & "•	The recipient will not use any Confidential Information for any purpose except those expressly contemplated or authorised by the Company. "
            'txtConfidential.Text = txtConfidential.Text & "•	The recipient will take the same reasonable security precautions as it takes to safeguard its own confidential information. "
            'txtConfidential.Text = txtConfidential.Text & "•	The recipient undertakes to impose the confidentiality obligations on all directors/partners, officer and employees or other persons who work for the recipient or under its direction and control. "
            'txtConfidential.Text = txtConfidential.Text & "•	The recipient will return all originals, copies, reproductions and summaries of Confidential Information in its control, or confirm its destruction as requested by the Company."
            dt = objCust.LoadExistingItems(sSession.AccessCode, sSession.AccessCodeID, iExistingLOEId)
            If dt.Rows.Count > 0 Then
                lblLOETemplateFunName.Text = dt.Rows(0)("LOE_FuncationName")
                lblFunId.Text = dt.Rows(0)("LOE_FunctionId")
                lblLOETemplateFrequency.Text = dt.Rows(0)("LOE_Frequency")
                lblLOETemplateProfessionalFee.Text = dt.Rows(0)("LOE_TemplateProfessionalFee")
                If IsDBNull(dt.Rows(0)("LOET_StdsInternalAudit")) = False Then
                    sStdIntAudit = dt.Rows(0)("LOET_StdsInternalAudit")
                    If sStdIntAudit <> "" Then
                        txtStdIntAudit.Text = sStdIntAudit
                    End If
                End If
                If IsDBNull(dt.Rows(0)("LOET_Responsibilities")) = False Then
                    sRoles = dt.Rows(0)("LOET_Responsibilities")
                    If sRoles <> "" Then
                        txtRoles.Text = sRoles
                    End If
                End If
                If IsDBNull(dt.Rows(0)("LOET_Infrastructure")) = False Then
                    sInfrastructure = dt.Rows(0)("LOET_Infrastructure")
                    If sInfrastructure <> "" Then
                        txtInfrastructure.Text = sInfrastructure
                    End If
                End If
                If IsDBNull(dt.Rows(0)("LOET_NDA")) = False Then
                    sConfidential = dt.Rows(0)("LOET_NDA")
                    If sConfidential <> "" Then
                        txtConfidential.Text = sConfidential
                    End If
                End If
                If IsDBNull(dt.Rows(0)("LOET_Deliverable")) = False Then
                    If dt.Rows(0)("LOET_Deliverable") <> "" Then
                        txtDeliverable.Text = dt.Rows(0)("LOET_Deliverable")
                    End If
                End If
                If IsDBNull(dt.Rows(0)("LOE_General")) = False Then
                    If dt.Rows(0)("LOE_General") <> "" Then
                        txtGeneral.Text = dt.Rows(0)("LOE_General")
                    End If
                End If
                lstScopeSubFun.DataSource = objclsAdminMaster.LoadAuditAssignmentSubTask(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0)("LOE_FunctionId"))
                lstScopeSubFun.DataTextField = "AM_Name"
                lstScopeSubFun.DataValueField = "AM_ID"
                lstScopeSubFun.DataBind()
                sListValue = dt.Rows(0)("LOE_SubFunctionId")
                If sListValue <> "" Then
                    aStr = sListValue.Split(",")
                    For i = 0 To UBound(aStr)
                        For j = 0 To lstScopeSubFun.Items.Count - 1
                            If lstScopeSubFun.Items(j).Value = aStr(i) Then
                                lstScopeSubFun.Items(j).Selected = True
                            End If
                        Next
                    Next
                    lstSubFunction.Enabled = True
                End If
                If objCust.CheckLOEtemp(sSession.AccessCode, sSession.AccessCodeID, iExistingLOEId) = 0 Then
                    imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                    'If sCDSave = "YES" Then
                    imgbtnSaveLOETemp.Visible = True
                    'End If
                Else
                    imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                    'If sCDReport = "YES" Then
                    imgbtnReport.Visible = True
                    'End If
                End If
                iAttachID = 0 : lblBadgeCount.Text = 0
                gvAttach.DataSource = Nothing
                gvAttach.DataBind()
                If IsDBNull(dt.Rows(0).Item("LOE_AttachID")) = False Then
                    iAttachID = dt.Rows(0).Item("LOE_AttachID")
                    BindAllAttachments(sSession.AccessCode, iAttachID)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadText" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Function sGetListLOESubFunction() As String
        Dim i As Integer
        Dim sList As String = ""
        Try
            For i = 0 To lstScopeSubFun.Items.Count - 1
                If lstScopeSubFun.Items(i).Selected = True Then
                    sList = sList & "," & lstScopeSubFun.Items(i).Value
                End If
            Next
            If sList.StartsWith(",") Then
                sList = sList.Remove(0, 1)
            End If
            Return sList
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "sGetListLOESubFunction" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Function
    Private Sub imgbtnSaveLOETemp_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveLOETemp.Click
        Dim objstrLOETemplate As New strLOETemplate
        Dim Arr() As String
        Dim sListValues As String
        Try
            lblError.Text = ""
            If ddlLOETemplateCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer." : lblCustomerValidationMsg.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
                Exit Sub
            End If
            If ddlExistingLOETemplate.SelectedIndex = 0 Then
                lblError.Text = "Select Existing LOE." : lblCustomerValidationMsg.Text = "Select Existing LOE."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If
            If objCust.CheckLOEtemp(sSession.AccessCode, sSession.AccessCodeID, ddlExistingLOETemplate.SelectedValue) <> 0 Then
                lblError.Text = "Already Saved." : lblCustomerValidationMsg.Text = "Already Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                Exit Sub
            End If
            If txtStdIntAudit.Text.Length > 8000 Then
                lblError.Text = "Responsibilities of the Auditor exceeded maximum size(max 8000 characters)."
                lblCustomerValidationMsg.Text = "Responsibilities of the Auditor exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                txtStdIntAudit.Focus()
                Exit Sub
            End If
            If txtDeliverable.Text.Length > 8000 Then
                lblError.Text = "The objective and scope of the audit exceeded maximum size(max 8000 characters)."
                lblCustomerValidationMsg.Text = "The objective and scope of the audit exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                txtDeliverable.Focus()
                Exit Sub
            End If
            If txtRoles.Text.Length > 8000 Then
                lblError.Text = "Reporting exceeded maximum size(max 8000 characters)."
                lblCustomerValidationMsg.Text = "Reporting exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                txtRoles.Focus()
                Exit Sub
            End If
            If txtInfrastructure.Text.Length > 8000 Then
                lblError.Text = "The responsibilities of management and identification of the applicable financial reporting framework exceeded maximum size(max 8000 characters)."
                lblCustomerValidationMsg.Text = "The responsibilities of management and identification of the applicable financial reporting framework exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                txtInfrastructure.Focus()
                Exit Sub
            End If
            If txtGeneral.Text.Length > 8000 Then
                lblError.Text = "General exceeded maximum size(max 8000 characters)."
                lblCustomerValidationMsg.Text = "General exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                txtGeneral.Focus()
                Exit Sub
            End If
            If txtConfidential.Text.Length > 8000 Then
                lblError.Text = "Non Disclosure Of Confidential Information exceeded maximum size(max 8000 characters)."
                lblCustomerValidationMsg.Text = "Non Disclosure Of Confidential Information exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show');", True)
                imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
                'If sCDSave = "YES" Then
                imgbtnSaveLOETemp.Visible = True
                'End If
                txtConfidential.Focus()
                Exit Sub
            End If
            sListValues = sGetListLOESubFunction()
            objstrLOETemplate.iLOET_Id = 0
            objstrLOETemplate.iLOET_LOEID = ddlExistingLOETemplate.SelectedValue
            objstrLOETemplate.iLOET_CustomerId = ddlLOETemplateCustomers.SelectedValue
            objstrLOETemplate.iLOET_FunctionId = lblFunId.Text
            objstrLOETemplate.sLOET_ScopeOfWork = sListValues
            objstrLOETemplate.sLOET_Frequency = objclsGRACeGeneral.SafeSQL(lblLOETemplateFrequency.Text.Trim())
            objstrLOETemplate.sLOET_Deliverable = objclsGRACeGeneral.SafeSQL(txtDeliverable.Text.Trim())
            objstrLOETemplate.sLOET_ProfessionalFees = objclsGRACeGeneral.SafeSQL(lblLOETemplateProfessionalFee.Text.Trim())
            objstrLOETemplate.sLOET_StdsInternalAudit = objclsGRACeGeneral.SafeSQL(txtStdIntAudit.Text.Trim())
            objstrLOETemplate.sLOET_Responsibilities = objclsGRACeGeneral.SafeSQL(txtRoles.Text.Trim())
            objstrLOETemplate.sLOET_Infrastructure = objclsGRACeGeneral.SafeSQL(txtInfrastructure.Text.Trim())
            objstrLOETemplate.sLOET_NDA = objclsGRACeGeneral.SafeSQL(txtConfidential.Text.Trim())
            objstrLOETemplate.sLOET_General = objclsGRACeGeneral.SafeSQL(txtGeneral.Text.Trim())
            objstrLOETemplate.sLOET_Delflag = "A"
            objstrLOETemplate.sLOET_STATUS = "C"
            objstrLOETemplate.iLOE_AttachID = iAttachID
            objstrLOETemplate.iLOET_CrBy = sSession.UserID
            objstrLOETemplate.iLOET_UpdatedBy = sSession.UserID
            objstrLOETemplate.sLOET_IPAddress = sSession.IPAddress
            objstrLOETemplate.iLOET_CompID = sSession.AccessCodeID
            Arr = objCust.SaveLOETemplateDetails(sSession.AccessCode, objstrLOETemplate)
            lblError.Text = "Successfully Saved."
            lblCustomerValidationMsg.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            imgbtnSaveLOETemp.Visible = False : imgbtnReport.Visible = False
            'If sCDReport = "YES" Then
            imgbtnReport.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveLOETemp_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Public Sub ClearAllLOETemplate()
        Try
            lblLOETemplateFunName.Text = "" : lblLOETemplateFrequency.Text = "" : lblLOETemplateProfessionalFee.Text = ""
            lstScopeSubFun.Items.Clear() : lblFunId.Text = ""
            txtDeliverable.Text = "" : txtGeneral.Text = ""
            iAttachID = 0
            imgbtnSaveLOETemp.Visible = False
            'If sCDSave = "YES" Then
            imgbtnSaveLOETemp.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAllLOETemplate" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
            'Throw
        End Try
    End Sub
    Public Sub ClearAllComplaince()
        Try
            iCustCompPKId = 0 : iCustDirectorPKId = 0 : iCustPartnerPKId = 0
            ddlCompTask.SelectedIndex = 0 : ddlCompFrequency.SelectedIndex = 0
            ddlCompTask.Enabled = True : ddlCompFrequency.Enabled = True
            txtCompLoginName.Text = "" : txtCompPassword.Text = "" : txtCompEmail.Text = "" : txtCompMobileNo.Text = ""
            ddlCompAccountDetails.SelectedIndex = 0 : txtCompAadhaarAuthen.Text = "" : txtCompRegNo.Text = "" : txtRemarks.Text = ""
            'If ddlCompExistingCustomer.SelectedIndex > 0 Then
            '    txtCompGSTINPAN.Text = objCust.LoadGSTINPAN(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, "GSTIN")
            'End If
            gvCompliance.DataSource = Nothing
            gvCompliance.DataBind()

            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False
            'If sCDSave = "YES" Then
            imgbtnSaveCompliance.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ClearAllComplaince" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
            'Throw
        End Try
    End Sub
    Private Sub lnkbtnWord_Click(sender As Object, e As EventArgs) Handles lnkbtnWord.Click
        Dim mimeType As String = Nothing
        Dim dtReport As New DataTable
        Try
            lblError.Text = ""
            If ddlLOETemplateCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Customer','', 'info');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/LOETemp.rdlc")

            Dim RefNo As ReportParameter() = New ReportParameter() {New ReportParameter("RefNo", ddlExistingLOETemplate.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(RefNo)

            'If txtBoardOfDirectors.Text = "" Then
            '    Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", " ")}
            '    ReportViewer1.LocalReport.SetParameters(Director)
            'Else
            '    Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", txtBoardOfDirectors.Text)}
            '    ReportViewer1.LocalReport.SetParameters(Director)
            'End If
            Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", ddlLOETemplateCustomers.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Director)

            Dim Function1 As ReportParameter() = New ReportParameter() {New ReportParameter("Function1", lblLOETemplateFunName.Text)}
            ReportViewer1.LocalReport.SetParameters(Function1)

            Dim Year As ReportParameter() = New ReportParameter() {New ReportParameter("Year", ddlYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Year)

            Dim Fees As ReportParameter() = New ReportParameter() {New ReportParameter("Fees", lblLOETemplateProfessionalFee.Text)}
            ReportViewer1.LocalReport.SetParameters(Fees)

            If txtStdIntAudit.Text = "" Then
                Dim ResponsibilitiesOftheAuditor As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOftheAuditor", " ")}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOftheAuditor)
            Else
                Dim ResponsibilitiesOftheAuditor As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOftheAuditor", txtStdIntAudit.Text)}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOftheAuditor)
            End If

            If txtDeliverable.Text = "" Then
                Dim ObjectiveAndScopeOfAudit As ReportParameter() = New ReportParameter() {New ReportParameter("ObjectiveAndScopeOfAudit", " ")}
                ReportViewer1.LocalReport.SetParameters(ObjectiveAndScopeOfAudit)
            Else
                Dim ObjectiveAndScopeOfAudit As ReportParameter() = New ReportParameter() {New ReportParameter("ObjectiveAndScopeOfAudit", txtDeliverable.Text)}
                ReportViewer1.LocalReport.SetParameters(ObjectiveAndScopeOfAudit)
            End If

            If txtRoles.Text = "" Then
                Dim Reporting As ReportParameter() = New ReportParameter() {New ReportParameter("Reporting", " ")}
                ReportViewer1.LocalReport.SetParameters(Reporting)
            Else
                Dim Reporting As ReportParameter() = New ReportParameter() {New ReportParameter("Reporting", txtRoles.Text)}
                ReportViewer1.LocalReport.SetParameters(Reporting)
            End If

            If txtInfrastructure.Text = "" Then
                Dim ResponsibilitiesOfManagement As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOfManagement", " ")}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOfManagement)
            Else
                Dim ResponsibilitiesOfManagement As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOfManagement", txtInfrastructure.Text)}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOfManagement)
            End If

            If txtGeneral.Text = "" Then
                Dim General As ReportParameter() = New ReportParameter() {New ReportParameter("General", " ")}
                ReportViewer1.LocalReport.SetParameters(General)
            Else
                Dim General As ReportParameter() = New ReportParameter() {New ReportParameter("General", txtGeneral.Text)}
                ReportViewer1.LocalReport.SetParameters(General)
            End If

            If txtConfidential.Text = "" Then
                Dim NonDisclousure As ReportParameter() = New ReportParameter() {New ReportParameter("NonDisclousure", " ")}
                ReportViewer1.LocalReport.SetParameters(NonDisclousure)
            Else
                Dim NonDisclousure As ReportParameter() = New ReportParameter() {New ReportParameter("NonDisclousure", txtConfidential.Text)}
                ReportViewer1.LocalReport.SetParameters(NonDisclousure)
            End If

            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlLOETemplateCustomers.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)

            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)

            ReportViewer1.LocalReport.Refresh()
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("Word")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=LOE" + ".doc")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnWord_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtReport As New DataTable
        Try
            lblError.Text = ""
            If ddlLOETemplateCustomers.SelectedIndex = 0 Then
                lblError.Text = "Select Customer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Select Customer','', 'info');", True)
                Exit Sub
            End If

            ReportViewer1.Reset()
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/LOETemp.rdlc")

            Dim RefNo As ReportParameter() = New ReportParameter() {New ReportParameter("RefNo", ddlExistingLOETemplate.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(RefNo)

            'If txtBoardOfDirectors.Text = "" Then
            '    Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", " ")}
            '    ReportViewer1.LocalReport.SetParameters(Director)
            'Else
            '    Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", txtBoardOfDirectors.Text)}
            '    ReportViewer1.LocalReport.SetParameters(Director)
            'End If
            Dim Director As ReportParameter() = New ReportParameter() {New ReportParameter("Director", ddlLOETemplateCustomers.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Director)

            Dim Function1 As ReportParameter() = New ReportParameter() {New ReportParameter("Function1", lblLOETemplateFunName.Text)}
            ReportViewer1.LocalReport.SetParameters(Function1)

            Dim Year As ReportParameter() = New ReportParameter() {New ReportParameter("Year", ddlYear.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Year)

            Dim Fees As ReportParameter() = New ReportParameter() {New ReportParameter("Fees", lblLOETemplateProfessionalFee.Text)}
            ReportViewer1.LocalReport.SetParameters(Fees)

            If txtStdIntAudit.Text = "" Then
                Dim ResponsibilitiesOftheAuditor As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOftheAuditor", " ")}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOftheAuditor)
            Else
                Dim ResponsibilitiesOftheAuditor As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOftheAuditor", txtStdIntAudit.Text)}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOftheAuditor)
            End If

            If txtDeliverable.Text = "" Then
                Dim ObjectiveAndScopeOfAudit As ReportParameter() = New ReportParameter() {New ReportParameter("ObjectiveAndScopeOfAudit", " ")}
                ReportViewer1.LocalReport.SetParameters(ObjectiveAndScopeOfAudit)
            Else
                Dim ObjectiveAndScopeOfAudit As ReportParameter() = New ReportParameter() {New ReportParameter("ObjectiveAndScopeOfAudit", txtDeliverable.Text)}
                ReportViewer1.LocalReport.SetParameters(ObjectiveAndScopeOfAudit)
            End If

            If txtRoles.Text = "" Then
                Dim Reporting As ReportParameter() = New ReportParameter() {New ReportParameter("Reporting", " ")}
                ReportViewer1.LocalReport.SetParameters(Reporting)
            Else
                Dim Reporting As ReportParameter() = New ReportParameter() {New ReportParameter("Reporting", txtRoles.Text)}
                ReportViewer1.LocalReport.SetParameters(Reporting)
            End If

            If txtInfrastructure.Text = "" Then
                Dim ResponsibilitiesOfManagement As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOfManagement", " ")}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOfManagement)
            Else
                Dim ResponsibilitiesOfManagement As ReportParameter() = New ReportParameter() {New ReportParameter("ResponsibilitiesOfManagement", txtInfrastructure.Text)}
                ReportViewer1.LocalReport.SetParameters(ResponsibilitiesOfManagement)
            End If

            If txtGeneral.Text = "" Then
                Dim General As ReportParameter() = New ReportParameter() {New ReportParameter("General", " ")}
                ReportViewer1.LocalReport.SetParameters(General)
            Else
                Dim General As ReportParameter() = New ReportParameter() {New ReportParameter("General", txtGeneral.Text)}
                ReportViewer1.LocalReport.SetParameters(General)
            End If

            If txtConfidential.Text = "" Then
                Dim NonDisclousure As ReportParameter() = New ReportParameter() {New ReportParameter("NonDisclousure", " ")}
                ReportViewer1.LocalReport.SetParameters(NonDisclousure)
            Else
                Dim NonDisclousure As ReportParameter() = New ReportParameter() {New ReportParameter("NonDisclousure", txtConfidential.Text)}
                ReportViewer1.LocalReport.SetParameters(NonDisclousure)
            End If

            Dim Customer As ReportParameter() = New ReportParameter() {New ReportParameter("Customer", ddlLOETemplateCustomers.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(Customer)

            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)

            ReportViewer1.LocalReport.Refresh()
            Dim pdfViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            Response.AddHeader("content-disposition", "attachment; filename=LOE" + ".pdf")
            Response.BinaryWrite(pdfViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(sBackStatus)))
            Response.Redirect(String.Format("CustomerMaster.aspx?StatusID={0}", oStatus), False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            gvAttach.PageSize = 1000
            ds = objclsAttachments.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            gvAttach.DataSource = ds
            gvAttach.DataBind()
            lblBadgeCount.Text = gvAttach.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
            'Throw
        End Try
    End Sub
    Private Sub btnaddAttch_Click(sender As Object, e As EventArgs) Handles btnAddAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblMsg.Text = "" : iDocID = 0
            If Not (txtfileAttach.PostedFile Is Nothing) And txtfileAttach.PostedFile.ContentLength > 0 Then
                lSize = CType(txtfileAttach.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                    Exit Sub
                End If
                lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtfileAttach.PostedFile.FileName)

                sFullFilePath = sPaths & sFilesNames
                txtfileAttach.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iAttachID > 0 Then
                        BindAllAttachments(sSession.AccessCode, iAttachID)
                    End If
                Else
                    lblMsg.Text = "No file to Attach."
                End If
            Else
                lblMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnaddAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvAttach_PreRender(sender As Object, e As EventArgs) Handles gvAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvAttach.Rows.Count > 0 Then
                gvAttach.UseAccessibleHeader = True
                gvAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnAdd As New ImageButton, imgbtnRemove As New ImageButton
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnAdd = CType(e.Row.FindControl("imgbtnAdd"), ImageButton)
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnAdd.ImageUrl = "~/Images/Edit16.png"
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sCDSave = "YES" Then
                gvAttach.Columns(4).Visible = True
                'Else
                gvAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblMsg.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iAttachID, iDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID)
                BindAllAttachments(sSession.AccessCode, iAttachID)
            End If
            If e.CommandName = "ADDDESC" Then
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iDocID = Val(lblAtchDocID.Text)
                lblFDescription = DirectCast(clickedRow.FindControl("lblFDescription"), Label)
                lblHeadingDescription.Visible = True : txtDescription.Text = "" : txtDescription.Visible = True : btnAddDesc.Visible = True
                txtDescription.Text = lblFDescription.Text
                txtDescription.Focus()
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub DownloadMyFile(ByVal pstrFileNameAndPath As String)
        Dim file As System.IO.FileInfo
        Try
            file = New System.IO.FileInfo(pstrFileNameAndPath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(file.FullName)
                Response.End()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "DownloadMyFile" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
            'Throw
        End Try
    End Sub
    Protected Sub btnAddDesc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDesc.Click
        Try
            lblError.Text = "" : lblMsg.Text = ""
            If txtDescription.Text.Trim.Length > 1000 Then
                lblMsg.Text = "Description exceeded maximum size(max 1000 characters)."
                txtDescription.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
                Exit Try
            End If
            objclsAttachments.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iAttachID, iDocID, Replace(txtDescription.Text.Trim, "'", "`"))
            lblHeadingDescription.Visible = False : txtDescription.Text = "" : txtDescription.Visible = False : btnAddDesc.Visible = False
            iDocID = 0
            BindAllAttachments(sSession.AccessCode, iAttachID)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            lblMsg.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddDesc_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvResource_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvResource.RowDataBound
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("btnDelete"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                If ddlExistingLOE.SelectedIndex > 0 Then
                    gvResource.Columns(6).Visible = True
                Else
                    gvResource.Columns(6).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResource_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvReAmbess_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvReAmbess.RowDataBound
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("btnDeleteRe"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                If ddlExistingLOE.SelectedIndex > 0 Then
                    gvReAmbess.Columns(3).Visible = True
                Else
                    gvReAmbess.Columns(3).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvReAmbess_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub gvCatRes_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvCatRes.RowDataBound
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("btnDeleteCa"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                If ddlExistingLOE.SelectedIndex > 0 Then
                    gvCatRes.Columns(4).Visible = True
                Else
                    gvCatRes.Columns(4).Visible = False
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCatRes_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019 
        End Try
    End Sub
    Private Sub lnkbtnCompliance_Click(sender As Object, e As EventArgs) Handles lnkbtnCompliance.Click
        Try
            lblError.Text = ""
            If ddlCustName.SelectedIndex = 0 AndAlso txtCustName.Text <> "" Then
                lnkbtnCustomer_Click(sender, e)
                lblError.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                lblCustomerValidationMsg.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlLOECustomers').focus();", True)
                Exit Sub
            End If
            lblTab.Text = 6
            imgbtnAdd.Visible = True : imgbtnReport.Visible = False : imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False
            If ddlCompExistingCustomer.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnSaveCompliance.Visible = True
                'End If
                'txtCompGSTIN.Text = objCust.LoadGSTINPAN(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, "GSTIN")
                ddlCompExistingCustomer_SelectedIndexChanged(sender, e)
            End If

            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnSaveOther.Visible = False
            imgbtnSaveLocation.Visible = False : imgbtnUpdateLoction.Visible = False
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            imgbtnSaveLOETemp.Visible = False

            liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            liLOE.Attributes.Remove("class") : divLOE.Attributes.Add("class", "tab-pane")
            liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            liCompliance.Attributes.Add("class", "active") : divCompliance.Attributes.Add("class", "tab-pane active")
            liAssignment.Attributes.Remove("class") : divAssignment.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCompliance_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvCompliance_PreRender(sender As Object, e As EventArgs) Handles gvCompliance.PreRender
        Dim dt As New DataTable
        Try
            If gvCompliance.Rows.Count > 0 Then
                gvCompliance.UseAccessibleHeader = True
                gvCompliance.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCompliance.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadCustAllComplianceDetails()
        Dim dt As New DataTable
        Try
            gvCompliance.DataSource = Nothing
            gvCompliance.DataBind()
            dt = objCust.GetCustomerCompDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, 0)
            If dt.Rows.Count > 0 Then
                gvCompliance.DataSource = dt
                gvCompliance.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustAllComplianceDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadCustSelectedComplianceDetails()
        Dim dt As New DataTable
        Try
            ddlCompTask.Enabled = True : ddlCompFrequency.Enabled = True
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False
            dt = objCust.GetCustomerCompDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, iCustCompPKId)
            If dt.Rows.Count > 0 Then
                'If sCDSave = "YES" Then
                imgbtnUpdateCompliance.Visible = True
                'End If
                ddlCompTask.Enabled = False : ddlCompFrequency.Enabled = False
                ddlCompTask.SelectedValue = dt.Rows(0)("ServiceTaskId")
                ddlCompFrequency.SelectedValue = dt.Rows(0)("FrequencyId")
                txtCompLoginName.Text = dt.Rows(0)("LoginName")
                txtCompPassword.Text = dt.Rows(0)("Password")
                txtCompEmail.Text = dt.Rows(0)("Email")
                txtCompMobileNo.Text = dt.Rows(0)("MobileNo")
                ddlCompAccountDetails.SelectedIndex = dt.Rows(0)("AccountDetailID")
                txtCompAadhaarAuthen.Text = dt.Rows(0)("AadhaarAuthentication")
                txtCompRegNo.Text = dt.Rows(0)("RegNo")
                txtRemarks.Text = dt.Rows(0)("Remarks")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustSelectedComplianceDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub imgbtnSaveCompliance_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSaveCompliance.Click, imgbtnUpdateCompliance.Click
        Dim objsCompliance As New strCompliance
        Dim Arr As Array
        Try
            If ddlCompExistingCustomer.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Existing Customer." : lblError.Text = "Select Existing Customer."
                ddlCompExistingCustomer.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCompExistingCustomer').focus();", True)
                Exit Sub
            End If
            If ddlCompTask.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Task." : lblError.Text = "Select Task."
                ddlCompTask.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCompTask').focus();", True)
                Exit Sub
            End If
            If ddlCompFrequency.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = " Select Frequency." : lblError.Text = " Select Frequency."
                ddlCompFrequency.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCompFrequency').focus();", True)
                Exit Sub
            End If
            If txtCompEmail.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Email exceeded maximum size(max 50 characters)." : lblError.Text = "Email exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCompEmail').focus();", True)
                txtCompEmail.Focus()
                Exit Sub
            End If
            If txtCompMobileNo.Text.Trim <> "" And txtCompMobileNo.Text.Trim.Length > 15 Then
                lblCustomerValidationMsg.Text = "Mobile number exceeded maximum size(max 15 characters)." : lblError.Text = "Mobile number exceeded maximum size(max 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtCompMobileNo').focus();", True)
                txtContactMobileNo.Focus()
                Exit Sub
            End If
            If objCust.CheckCustComplianceTask(sSession.AccessCode, sSession.AccessCodeID, iCustCompPKId, ddlCompExistingCustomer.SelectedValue, ddlCompTask.SelectedValue, ddlCompFrequency.SelectedValue) = True Then
                lblCustomerValidationMsg.Text = "Selected Types Of Service/Tasks & Frequency details already exists." : lblError.Text = "Selected Types Of Service/Tasks & Frequency details already exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                Exit Sub
            End If

            objsCompliance.iComp_Id = iCustCompPKId
            objsCompliance.iComp_CustID = ddlCompExistingCustomer.SelectedValue
            objsCompliance.iComp_Task = ddlCompTask.SelectedValue
            objsCompliance.iComp_Frequency = ddlCompFrequency.SelectedValue
            objsCompliance.sComp_LoginName = txtCompLoginName.Text.Trim
            objsCompliance.sComp_Password = txtCompPassword.Text
            objsCompliance.sComp_Email = txtCompEmail.Text.Trim
            If txtCompMobileNo.Text <> "" Then
                objsCompliance.sComp_MobileNo = txtCompMobileNo.Text
            Else
                objsCompliance.sComp_MobileNo = 0
            End If
            objsCompliance.iComp_Accountdetails = ddlCompAccountDetails.SelectedIndex
            objsCompliance.sComp_AadhaarAuthen = txtCompAadhaarAuthen.Text.Trim
            objsCompliance.sComp_GSTIN = txtCompRegNo.Text.Trim
            objsCompliance.sComp_Remarks = txtRemarks.Text.Trim
            objsCompliance.iComp_CRBY = sSession.UserID
            objsCompliance.dComp_CRON = DateTime.Today
            objsCompliance.iComp_UpdatedBy = sSession.UserID
            objsCompliance.dComp_UpdatedOn = DateTime.Today
            objsCompliance.sComp_IPAddress = sSession.IPAddress
            objsCompliance.iComp_CompID = sSession.AccessCodeID
            objsCompliance.sComp_STATUS = "C"
            objsCompliance.sComp_DelFlag = "A"

            Arr = objCust.SaveCustomerComplaince(sSession.AccessCode, objsCompliance)
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved."
                lblCustomerValidationMsg.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            If Arr(0) = 2 Then
                lblError.Text = "Successfully Updated."
                lblCustomerValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            ddlCompExistingCustomer_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSaveCompliance_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlCompExistingCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompExistingCustomer.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = "" : iCustCompPKId = 0 : iCustDirectorPKId = 0 : iCustPartnerPKId = 0
            ddlCompTask.SelectedIndex = 0 : ddlCompFrequency.SelectedIndex = 0
            ddlCompTask.Enabled = True : ddlCompFrequency.Enabled = True
            txtCompLoginName.Text = "" : txtCompPassword.Text = "" : txtCompEmail.Text = "" : txtCompMobileNo.Text = ""
            ddlCompAccountDetails.SelectedIndex = 0 : txtCompAadhaarAuthen.Text = "" : txtCompRegNo.Text = "" : txtRemarks.Text = ""
            gvCompliance.DataSource = Nothing
            gvCompliance.DataBind()
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False : btnSaveDirector.Text = "Save Director Details"
            txtDirectorName.Text = "" : txtDirectorDOB.Text = "" : txtDirectorDIN.Text = "" : txtDirectorMobileNo.Text = "" : txtDirectorEmail.Text = "" : txtDirectorRemarks.Text = ""
            btnSavePartner.Text = "Save Partner Details"
            txtPartnerName.Text = "" : txtPartnerPAN.Text = "" : txtPartnerDOJ.Text = "" : txtShareOfProfit.Text = "" : txtCapitalAmount.Text = ""
            If ddlCompExistingCustomer.SelectedIndex > 0 Then
                'If sCDSave = "YES" Then
                imgbtnSaveCompliance.Visible = True
                'End If
                LoadCustAllComplianceDetails()
                LoadCustAllStatutoryDirectorDetails()
                LoadCustAllStatutoryPartnerDetails()
                'txtCompGSTINPAN.Text = objCust.LoadGSTINPAN(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, "GSTIN")

                ddlCustName.SelectedValue = ddlCompExistingCustomer.SelectedValue
                ddlOtherDetailsCust.SelectedValue = ddlCompExistingCustomer.SelectedValue
                ddlLocationCust.SelectedValue = ddlCompExistingCustomer.SelectedValue
                ddlLOECustomers.SelectedValue = ddlCompExistingCustomer.SelectedValue
                ddlLOETemplateCustomers.SelectedValue = ddlCompExistingCustomer.SelectedValue
                ddlAsgExistingCustomer.SelectedValue = ddlCustName.SelectedValue
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlCompExistingCustomer_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvCompliance_RowCommand(sender As Object, e As GridViewRowEventArgs) Handles gvCompliance.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Dim lblStatus As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnStatus.ToolTip = "Edit"
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                lblStatus = DirectCast(e.Row.FindControl("lblStatus"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                If lblStatus.Text = "A" Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                Else
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvCompliance_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCompliance.RowCommand
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblCompPkID As Label = DirectCast(clickedRow.FindControl("lblCompPkID"), Label)
            Dim lblStatus As Label = DirectCast(clickedRow.FindControl("lblStatus"), Label)
            iCustCompPKId = 0
            If e.CommandName = "EditRow" Then
                iCustCompPKId = Val(lblCompPkID.Text)
                LoadCustSelectedComplianceDetails()
            End If
            If e.CommandName = "Status" Then
                If lblStatus.Text = "A" Then
                    objCust.CustComplianceApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCompExistingCustomer.SelectedValue, Val(lblCompPkID.Text), sSession.IPAddress, "DeActivated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Compliance Master", "De-Activated", ddlCompExistingCustomer.SelectedValue, "", lblCompPkID.Text, "", sSession.IPAddress)
                    lblError.Text = "Successfully De-Activated." : lblCustomerValidationMsg.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                ElseIf lblStatus.Text = "D" Then
                    objCust.CustComplianceApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCompExistingCustomer.SelectedValue, Val(lblCompPkID.Text), sSession.IPAddress, "Activated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Compliance Master", "Activated", ddlCompExistingCustomer.SelectedValue, "", lblCompPkID.Text, "", sSession.IPAddress)
                    lblError.Text = "Successfully Activated." : lblCustomerValidationMsg.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                LoadCustAllComplianceDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCompliance_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Public Sub LoadCustAllStatutoryDirectorDetails()
        Dim dt As New DataTable
        Try
            gvDirector.DataSource = Nothing
            gvDirector.DataBind()
            dt = objCust.GetCustomerDirectorDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, 0)
            If dt.Rows.Count > 0 Then
                gvDirector.DataSource = dt
                gvDirector.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustAllStatutoryDirectorDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadCustSelectedStatutoryDirectorDetails()
        Dim dt As New DataTable
        Try
            dt = objCust.GetCustomerDirectorDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, iCustDirectorPKId)
            If dt.Rows.Count > 0 Then
                'If sCDSave = "YES" Then
                btnSaveDirector.Text = "Update Director Details"
                'End If
                txtDirectorName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Name"))
                txtDirectorDOB.Text = dt.Rows(0)("DOB")
                txtDirectorDIN.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("DIN"))
                txtDirectorMobileNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("MobileNo"))
                txtDirectorEmail.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Email"))
                txtDirectorRemarks.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Remarks"))
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustSelectedStatutoryDirectorDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnNewDirector_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewDirector.Click
        Try
            btnSaveDirector.Text = "Save Director Details"
            txtDirectorName.Text = "" : txtDirectorDOB.Text = "" : txtDirectorDIN.Text = ""
            txtDirectorMobileNo.Text = "" : txtDirectorEmail.Text = "" : txtDirectorRemarks.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewDirector_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSaveDirector_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveDirector.Click
        Dim objsStatutoryDirector As New strStatutoryDirector
        Dim Arr As Array
        Try
            If ddlCompExistingCustomer.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Existing Customer." : lblError.Text = "Select Existing Customer."
                ddlCompExistingCustomer.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCompExistingCustomer').focus();", True)
                Exit Sub
            End If
            If txtDirectorMobileNo.Text.Trim <> "" And txtDirectorMobileNo.Text.Trim.Length > 15 Then
                lblCustomerValidationMsg.Text = "Director Mobile number exceeded maximum size(max 15 characters)." : lblError.Text = "Director Mobile number exceeded maximum size(max 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDirectorMobileNo').focus();", True)
                txtDirectorMobileNo.Focus()
                Exit Sub
            End If
            If txtDirectorEmail.Text.Trim.Length > 50 Then
                lblCustomerValidationMsg.Text = "Director Email exceeded maximum size(max 50 characters)." : lblError.Text = "Director Email exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDirectorEmail').focus();", True)
                txtDirectorEmail.Focus()
                Exit Sub
            End If

            objsStatutoryDirector.iSSD_Id = iCustDirectorPKId
            objsStatutoryDirector.iSSD_CustID = ddlCompExistingCustomer.SelectedValue
            objsStatutoryDirector.sSSD_DirectorName = objclsGRACeGeneral.SafeSQL(txtDirectorName.Text.Trim)
            objsStatutoryDirector.dSSD_DOB = Date.ParseExact(txtDirectorDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objsStatutoryDirector.sSSD_DIN = objclsGRACeGeneral.SafeSQL(txtDirectorDIN.Text.Trim)
            objsStatutoryDirector.sSSD_MobileNo = objclsGRACeGeneral.SafeSQL(txtDirectorMobileNo.Text)
            objsStatutoryDirector.sSSD_Email = objclsGRACeGeneral.SafeSQL(txtDirectorEmail.Text.Trim)
            objsStatutoryDirector.sSSD_Remarks = objclsGRACeGeneral.SafeSQL(txtDirectorRemarks.Text.Trim)
            objsStatutoryDirector.iSSD_CRBY = sSession.UserID
            objsStatutoryDirector.dSSD_CRON = DateTime.Today
            objsStatutoryDirector.iSSD_UpdatedBy = sSession.UserID
            objsStatutoryDirector.dSSD_UpdatedOn = DateTime.Today
            objsStatutoryDirector.sSSD_IPAddress = sSession.IPAddress
            objsStatutoryDirector.iSSD_CompID = sSession.AccessCodeID
            objsStatutoryDirector.sSSD_STATUS = "C"
            objsStatutoryDirector.sSSD_DelFlag = "A"

            Arr = objCust.SaveCustomerStatutoryDirector(sSession.AccessCode, objsStatutoryDirector)
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved."
                lblCustomerValidationMsg.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            If Arr(0) = 2 Then
                lblError.Text = "Successfully Updated."
                lblCustomerValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            ddlCompExistingCustomer_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSaveDirector_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvDirector_RowCommand(sender As Object, e As GridViewRowEventArgs) Handles gvDirector.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Dim lblStatus As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnStatus.ToolTip = "Edit"
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                lblStatus = DirectCast(e.Row.FindControl("lblStatus"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                If lblStatus.Text = "A" Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                Else
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDirector_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvDirector_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDirector.RowCommand
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
            Dim lblDirectorPkID As Label = DirectCast(clickedRow.FindControl("lblDirectorPkID"), Label)
            Dim lblStatus As Label = DirectCast(clickedRow.FindControl("lblStatus"), Label)
            iCustDirectorPKId = 0
            If e.CommandName = "EditRow" Then
                iCustDirectorPKId = Val(lblDirectorPkID.Text)
                LoadCustSelectedStatutoryDirectorDetails()
            End If
            If e.CommandName = "Status" Then
                If lblStatus.Text = "A" Then
                    objCust.CustDirectorApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCompExistingCustomer.SelectedValue, Val(lblDirectorPkID.Text), sSession.IPAddress, "DeActivated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Director Master", "De-Activated", ddlCompExistingCustomer.SelectedValue, "", lblDirectorPkID.Text, "", sSession.IPAddress)
                    lblError.Text = "Successfully De-Activated." : lblCustomerValidationMsg.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                ElseIf lblStatus.Text = "D" Then
                    objCust.CustDirectorApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCompExistingCustomer.SelectedValue, Val(lblDirectorPkID.Text), sSession.IPAddress, "Activated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Director Master", "Activated", ddlCompExistingCustomer.SelectedValue, "", lblDirectorPkID.Text, "", sSession.IPAddress)
                    lblError.Text = "Successfully Activated." : lblCustomerValidationMsg.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                LoadCustAllStatutoryDirectorDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDirector_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
        End Try
    End Sub
    Private Sub gvDirector_PreRender(sender As Object, e As EventArgs) Handles gvDirector.PreRender
        Dim dt As New DataTable
        Try
            If gvDirector.Rows.Count > 0 Then
                gvDirector.UseAccessibleHeader = True
                gvDirector.HeaderRow.TableSection = TableRowSection.TableHeader
                gvDirector.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvDirector_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadCustAllStatutoryPartnerDetails()
        Dim dt As New DataTable
        Try
            gvPartner.DataSource = Nothing
            gvPartner.DataBind()
            dt = objCust.GetCustomerPartnerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, 0)
            If dt.Rows.Count > 0 Then
                gvPartner.DataSource = dt
                gvPartner.DataBind()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustAllStatutoryPartnerDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadCustSelectedStatutoryPartnerDetails()
        Dim dt As New DataTable
        Try
            dt = objCust.GetCustomerPartnerDetails(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, iCustPartnerPKId)
            If dt.Rows.Count > 0 Then
                'If sCDSave = "YES" Then
                btnSavePartner.Text = "Update Partner Details"
                'End If
                txtPartnerName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("Name"))
                txtPartnerDOJ.Text = dt.Rows(0)("DOJ")
                txtPartnerPAN.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0)("PAN"))
                txtShareOfProfit.Text = dt.Rows(0)("ShareOfProfit")
                txtCapitalAmount.Text = dt.Rows(0)("CapitalAmount")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustSelectedStatutoryPartnerDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnNewPartner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewPartner.Click
        Try
            iCustPartnerPKId = 0
            btnSavePartner.Text = "Save Partner Details"
            txtPartnerName.Text = "" : txtPartnerDOJ.Text = "" : txtPartnerPAN.Text = ""
            txtShareOfProfit.Text = "" : txtCapitalAmount.Text = ""
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnNewPartner_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub btnSavePartner_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavePartner.Click
        Dim objsStatutoryPartner As New strStatutoryPartner
        Dim Arr As Array
        Try
            If ddlCompExistingCustomer.SelectedIndex = 0 Then
                lblCustomerValidationMsg.Text = "Select Existing Customer." : lblError.Text = "Select Existing Customer."
                ddlCompExistingCustomer.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlCompExistingCustomer').focus();", True)
                Exit Sub
            End If
            If txtPartnerName.Text.Trim.Length > 100 Then
                lblCustomerValidationMsg.Text = "Partner Name exceeded maximum size(max 100 characters)." : lblError.Text = "Partner Name exceeded maximum size(max 100 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDirectorMobileNo').focus();", True)
                txtPartnerName.Focus()
                Exit Sub
            End If
            If (objCust.CheckCustPartnerName(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, objclsGRACeGeneral.SafeSQL(txtPartnerName.Text.Trim()), iCustPartnerPKId) = True) Then
                lblCustomerValidationMsg.Text = "Partner Name already Exists." : lblError.Text = "Partner Name already Exists."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                Return
            End If
            If txtPartnerPAN.Text.Trim.Length > 25 Then
                lblCustomerValidationMsg.Text = "Partner PAN exceeded maximum size(max 25 characters)." : lblError.Text = "Partner PAN exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtDirectorEmail').focus();", True)
                txtPartnerPAN.Focus()
                Exit Sub
            End If
            If txtShareOfProfit.Text.Trim.Length > 6 Then
                lblCustomerValidationMsg.Text = "Share Of Profit exceeded maximum size(max 6 characters)." : lblError.Text = "Share Of Profit exceeded maximum size(max 6 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtShareOfProfit').focus();", True)
                txtShareOfProfit.Focus()
                Exit Sub
            End If

            Dim dTotalShareOfProfit As Decimal = objCust.GetTotalShareOfProfit(sSession.AccessCode, sSession.AccessCodeID, ddlCompExistingCustomer.SelectedValue, iCustPartnerPKId)
            If dTotalShareOfProfit >= 100 Then
                lblCustomerValidationMsg.Text = "The sum of the share of profit should be within or equal to 100%." : lblError.Text = "The sum of the share of profit should be within or equal to 100%."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtShareOfProfit').focus();", True)
                txtShareOfProfit.Focus()
                Exit Sub
            End If
            Dim dRemaining = 100 - (dTotalShareOfProfit + Val(txtShareOfProfit.Text.Trim))
            If dRemaining < 0 Then
                lblCustomerValidationMsg.Text = "Share of profit should be less than or equal to " + (100 - dTotalShareOfProfit).ToString() + "%." : lblError.Text = "Share of profit should be less than or equal to " + (100 - dTotalShareOfProfit).ToString() + "%."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#txtShareOfProfit').focus();", True)
                txtShareOfProfit.Focus()
                Exit Sub
            End If
            If txtCapitalAmount.Text.Trim() <> "" And IsNumeric(txtCapitalAmount.Text) = False Then
                lblCustomerValidationMsg.Text = "Enter valid Capital Amount." : lblError.Text = "Enter valid Capital Amount."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModaCustomerValidation').modal('show'); $('#txtCapitalAmount').focus();", True)
                Exit Sub
            End If

            objsStatutoryPartner.iSSP_Id = iCustPartnerPKId
            objsStatutoryPartner.iSSP_CustID = ddlCompExistingCustomer.SelectedValue
            objsStatutoryPartner.sSSP_PartnerName = objclsGRACeGeneral.SafeSQL(txtPartnerName.Text.Trim)
            objsStatutoryPartner.dSSP_DOJ = Date.ParseExact(txtPartnerDOJ.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objsStatutoryPartner.sSSP_PAN = objclsGRACeGeneral.SafeSQL(txtPartnerPAN.Text.Trim)
            objsStatutoryPartner.dSSP_ShareOfProfit = String.Format("{0:0.00}", Convert.ToDecimal(txtShareOfProfit.Text))
            objsStatutoryPartner.dSSP_CapitalAmount = String.Format("{0:0.00}", Convert.ToDecimal(txtCapitalAmount.Text))
            objsStatutoryPartner.iSSP_CRBY = sSession.UserID
            objsStatutoryPartner.dSSP_CRON = DateTime.Today
            objsStatutoryPartner.iSSP_UpdatedBy = sSession.UserID
            objsStatutoryPartner.dSSP_UpdatedOn = DateTime.Today
            objsStatutoryPartner.sSSP_IPAddress = sSession.IPAddress
            objsStatutoryPartner.iSSP_CompID = sSession.AccessCodeID
            objsStatutoryPartner.sSSP_STATUS = "C"
            objsStatutoryPartner.sSSP_DelFlag = "A"

            Arr = objCust.SaveCustomerStatutoryPartner(sSession.AccessCode, objsStatutoryPartner)
            If Arr(0) = 3 Then
                lblError.Text = "Successfully Saved."
                lblCustomerValidationMsg.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            If Arr(0) = 2 Then
                lblError.Text = "Successfully Updated."
                lblCustomerValidationMsg.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
            End If
            ddlCompExistingCustomer_SelectedIndexChanged(sender, e)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSavePartner_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvPartner_RowCommand(sender As Object, e As GridViewRowEventArgs) Handles gvPartner.RowDataBound
        Dim imgbtnStatus As New ImageButton, imgbtnEdit As New ImageButton
        Dim lblStatus As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnStatus = CType(e.Row.FindControl("imgbtnStatus"), ImageButton)
                imgbtnStatus.ToolTip = "Edit"
                imgbtnEdit = CType(e.Row.FindControl("imgbtnEdit"), ImageButton)
                lblStatus = DirectCast(e.Row.FindControl("lblStatus"), Label)
                imgbtnEdit.ImageUrl = "~/Images/Edit16.png"
                If lblStatus.Text = "A" Then
                    imgbtnStatus.ImageUrl = "~/Images/DeActivate16.png" : imgbtnStatus.ToolTip = "De-Activate"
                Else
                    imgbtnStatus.ImageUrl = "~/Images/Activate16.png" : imgbtnStatus.ToolTip = "Activate"
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartner_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
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
                    objCust.CustPartnerApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCompExistingCustomer.SelectedValue, Val(lblPartnerPkID.Text), sSession.IPAddress, "DeActivated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Partner Master", "De-Activated", ddlCompExistingCustomer.SelectedValue, "", lblPartnerPkID.Text, "", sSession.IPAddress)
                    lblError.Text = "Successfully De-Activated." : lblCustomerValidationMsg.Text = "Successfully De-Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                ElseIf lblStatus.Text = "D" Then
                    objCust.CustPartnerApproveStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, ddlCompExistingCustomer.SelectedValue, Val(lblPartnerPkID.Text), sSession.IPAddress, "Activated")
                    objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Partner Master", "Activated", ddlCompExistingCustomer.SelectedValue, "", lblPartnerPkID.Text, "", sSession.IPAddress)
                    lblError.Text = "Successfully Activated." : lblCustomerValidationMsg.Text = "Successfully Activated."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModaCustomerValidation').modal('show');", True)
                End If
                LoadCustAllStatutoryPartnerDetails()
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartner_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 07-08-2019
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
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPartner_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnAssignment_Click(sender As Object, e As EventArgs) Handles lnkbtnAssignment.Click
        Try
            lblError.Text = ""
            If ddlCustName.SelectedIndex = 0 AndAlso txtCustName.Text <> "" Then
                lnkbtnCustomer_Click(sender, e)
                lblError.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                lblCustomerValidationMsg.Text = "Please select Existing Customer or save entered data to go to the next tabs."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModaCustomerValidation').modal('show'); $('#ddlLOECustomers').focus();", True)
                Exit Sub
            End If
            lblTab.Text = 7
            imgbtnAdd.Visible = False : imgbtnReport.Visible = False : imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False
            If ddlAsgExistingCustomer.SelectedIndex > 0 Then
                ddlAsgExistingCustomer_SelectedIndexChanged(sender, e)
            End If

            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnSaveOther.Visible = False
            imgbtnSaveLocation.Visible = False : imgbtnUpdateLoction.Visible = False
            imgbtnSaveLOE.Visible = False : imgbtnUpdateLOE.Visible = False
            imgbtnSaveLOETemp.Visible = False
            imgbtnSaveCompliance.Visible = False : imgbtnUpdateCompliance.Visible = False

            liCust.Attributes.Remove("class") : divCustomerDetails.Attributes.Add("class", "tab-pane")
            liOther.Attributes.Remove("class") : divOther.Attributes.Add("class", "tab-pane")
            liLocations.Attributes.Remove("class") : divLocation.Attributes.Add("class", "tab-pane")
            liLOE.Attributes.Remove("class") : divLOE.Attributes.Add("class", "tab-pane")
            liLOETemplater.Attributes.Remove("class") : divLOETemplate.Attributes.Add("class", "tab-pane")
            liCompliance.Attributes.Remove("class") : divCompliance.Attributes.Add("class", "tab-pane")
            liAssignment.Attributes.Add("class", "active") : divAssignment.Attributes.Add("class", "tab-pane active")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssignment_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub BindAsgFinancialYear()
        Dim dt As New DataTable
        Dim iYearID As Integer
        Try
            dt = objclsAuditAssignment.GetAddYearTo2DigitFinancialYear(sSession.AccessCode, sSession.AccessCodeID, 0)
            lstFinancialYear.DataSource = dt
            lstFinancialYear.DataTextField = "YMS_ID"
            lstFinancialYear.DataValueField = "YMS_YearID"
            lstFinancialYear.DataBind()
            If sSession.YearID = 0 Then
                iYearID = objclsGeneralFunctions.GetDefaultYear(sSession.AccessCode, sSession.AccessCodeID)
                If iYearID > 0 Then
                    For j = 0 To lstFinancialYear.Items.Count - 1
                        If lstFinancialYear.Items(j).Value = iYearID Then
                            lstFinancialYear.Items(j).Selected = True
                        End If
                    Next
                End If
            Else
                For j = 0 To lstFinancialYear.Items.Count - 1
                    If lstFinancialYear.Items(j).Value = sSession.YearID Then
                        lstFinancialYear.Items(j).Selected = True
                    End If
                Next
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindEmployees" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub ddlAsgExistingCustomer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAsgExistingCustomer.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            gvAssignment.DataSource = Nothing
            gvAssignment.DataBind()
            chkInvoice.Checked = False
            If ddlAsgExistingCustomer.SelectedIndex > 0 Then
                LoadCustAllAssignmentDetails()

                ddlCustName.SelectedValue = ddlAsgExistingCustomer.SelectedValue
                ddlOtherDetailsCust.SelectedValue = ddlAsgExistingCustomer.SelectedValue
                ddlLocationCust.SelectedValue = ddlAsgExistingCustomer.SelectedValue
                ddlLOECustomers.SelectedValue = ddlAsgExistingCustomer.SelectedValue
                ddlLOETemplateCustomers.SelectedValue = ddlAsgExistingCustomer.SelectedValue
                ddlCompExistingCustomer.SelectedValue = ddlAsgExistingCustomer.SelectedValue
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlAsgExistingCustomer_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub gvAssignment_PreRender(sender As Object, e As EventArgs) Handles gvAssignment.PreRender
        Dim dt As New DataTable
        Try
            If gvAssignment.Rows.Count > 0 Then
                gvAssignment.UseAccessibleHeader = True
                gvAssignment.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssignment.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignment_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub gvAssignment_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Dim chkSelectAsgSubTask As New CheckBox
        Dim lblClosed As New Label, lblWorkStatus As New Label, lblCustomerName As New Label, lblCustomerFullName As New Label
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                lblClosed = CType(e.Row.FindControl("lblClosed"), Label)
                lblWorkStatus = CType(e.Row.FindControl("lblWorkStatus"), Label)
                lblWorkStatus.Font.Bold = False
                If Val(lblClosed.Text) = 1 Then
                    lblWorkStatus.Font.Bold = True
                    lblWorkStatus.ForeColor = Drawing.Color.Green
                End If
                lblCustomerName = CType(e.Row.FindControl("lblCustomerName"), Label)
                lblCustomerFullName = CType(e.Row.FindControl("lblCustomerFullName"), Label)
                lblCustomerName.ToolTip = lblCustomerFullName.Text
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssignment_RowDataBound" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Protected Sub imgbtnLoad_Click(sender As Object, e As EventArgs) Handles imgbtnLoad.Click
        Try
            LoadCustAllAssignmentDetails()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnLoad_Click" & " Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Sub LoadCustAllAssignmentDetails()
        Dim dt As New DataTable
        Dim sFinancialYearID As String = ""
        Try
            gvAssignment.DataSource = Nothing
            gvAssignment.DataBind()
            For i = 0 To lstFinancialYear.Items.Count - 1
                If lstFinancialYear.Items(i).Selected = True Then
                    sFinancialYearID = sFinancialYearID & "," & lstFinancialYear.Items(i).Value
                End If
            Next
            If sFinancialYearID.StartsWith(",") Then
                sFinancialYearID = sFinancialYearID.Remove(0, 1)
            End If
            If sFinancialYearID.EndsWith(",") Then
                sFinancialYearID = sFinancialYearID.Remove(Len(sFinancialYearID) - 1, 1)
            End If
            dt = objclsAuditAssignment.LoadCustomerAllScheduledAssignmentDetails(sSession.AccessCode, sSession.AccessCodeID, sFinancialYearID, ddlAsgExistingCustomer.SelectedValue, 0, 0, 0, "", "", True, sSession.UserID)
            If dt.Rows.Count > 0 Then
                gvAssignment.DataSource = dt
                gvAssignment.DataBind()
            End If
            If chkInvoice.Checked = True Then
                gvAssignment.Columns(10).Visible = True
            Else
                gvAssignment.Columns(10).Visible = False
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCustAllComplianceDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnCustPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnCustPDF.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objCust.LoadCustInformationAuditeeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearName, ddlCustName.SelectedValue, ddlCustName.SelectedItem.Text, ddlOrganization.SelectedItem.Text, txtDate.Text, txtProdManufactured.Text)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/CustInformationAuditee.rdlc")
            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "PDF", sSession.YearID, sSession.YearName, ddlCustName.SelectedValue, ddlCustName.SelectedItem.Text, sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustInformationAuditee" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Sub lnkbtnCustWord_Click(sender As Object, e As EventArgs) Handles lnkbtnCustWord.Click
        Dim mimeType As String = Nothing
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            dt = objCust.LoadCustInformationAuditeeDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.YearName, ddlCustName.SelectedValue, ddlCustName.SelectedItem.Text, ddlOrganization.SelectedItem.Text, txtDate.Text, txtProdManufactured.Text)
            If dt.Rows.Count = 0 Then
                lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No Data','', 'info');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/CustInformationAuditee.rdlc")
            Dim CompanyName As ReportParameter() = New ReportParameter() {New ReportParameter("CompanyName", objCust.GetAllCompanyDetails(sSession.AccessCode, sSession.AccessCodeID))}
            ReportViewer1.LocalReport.SetParameters(CompanyName)
            ReportViewer1.LocalReport.Refresh()
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Word")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Customer Master", "PDF", sSession.YearID, sSession.YearName, ddlCustName.SelectedValue, ddlCustName.SelectedItem.Text, sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=CustInformationAuditee" + ".doc")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustWord_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Private Function CreateCustomerOrgStructure() As String
        Dim Arr() As String
        Try
            If (ddlCustName.SelectedIndex > 0) Then
                objclsOrgStructure.iOrgnode = objclsOrgStructure.CheckOrgStructureData(sSession.AccessCode, sSession.AccessCodeID, ddlCustName.SelectedItem.Text)
            Else
                objclsOrgStructure.iOrgnode = 0
            End If

            objclsOrgStructure.sOrgIRDAcode = objclsGRACeGeneral.SafeSQL(txtCustName.Text)
            objclsOrgStructure.sOrgName = objclsGRACeGeneral.SafeSQL(txtCustName.Text)
            objclsOrgStructure.sOrgSalesUnitCode = ""
            objclsOrgStructure.sOrgBranchCode = ""
            objclsOrgStructure.iOrgAppStrength = 0
            objclsOrgStructure.sOrgNote = objclsGRACeGeneral.SafeSQL(txtCustName.Text)
            objclsOrgStructure.iOrgParent = 3
            objclsOrgStructure.iOrgLevelCode = 3
            objclsOrgStructure.sOrgDelflag = "A"
            objclsOrgStructure.sOrgStatus = "A"
            objclsOrgStructure.iOrgCreatedBy = sSession.UserID
            objclsOrgStructure.dOrgCreatedOn = Date.Today
            objclsOrgStructure.iOrgCompID = sSession.AccessCodeID
            Arr = objclsOrgStructure.SaveOrgStructure(sSession.AccessCode, objclsOrgStructure, sSession.IPAddress)
            Return Arr(1)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Protected Sub imgbtnAttachment_Click(sender As Object, e As EventArgs)
        Try
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAttachment_Click")
        End Try
    End Sub
End Class
