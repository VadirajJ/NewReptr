Imports System
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports BusinesLayer
Imports Microsoft.Reporting.WebForms
Imports System.Web.Mail

Partial Class EProfile
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_EProfile"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAttachments As New clsAttachments
    Private objclsEmployeeMaster As New clsEmployeeMaster
    Private objclsEProfile As New clsEProfile
    Private objclsGRACePermission As New clsGRACePermission
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private sSession As AllSession
    Private Shared iAttachID As Integer
    Private Shared iResumeAttachID As Integer
    Private Shared iPhotoAttachID As Integer
    Private Shared iSignatureAttachID As Integer
    Private Shared iResumeDocID As Integer
    Private Shared iPhotoDocID As Integer
    Private Shared iSignatureDocID As Integer
    Private Shared iSignatureID As Integer

    Private Shared iEQAttachID As Integer
    Private Shared iEQDocID As Integer
    Private Shared iECSAttachID As Integer
    Private Shared iECSDocID As Integer
    Private Shared iEmpPEAttachID As Integer
    Private Shared iEmpPEDocID As Integer
    Private Shared iEmpALAttachID As Integer
    Private Shared iEmpALDocID As Integer

    Private Shared iPAAttachID As Integer
    Private Shared iPADocID As Integer
    Private Shared iEmpAPAttachID As Integer
    Private Shared iEmpAPDocID As Integer
    Private Shared iSMAttachID As Integer
    Private Shared iSMDocID As Integer

    Private Shared iEmpTFAttachID As Integer
    Private Shared iEmpTFDocID As Integer
    Private Shared iPOAAttachID As Integer
    Private Shared iPOADocID As Integer

    Private Shared sEmpDetailsAttachIDs As String
    Private Shared sHRAttachIDs As String
    Private Shared sArticleAttachIDs As String

    Private Shared sFullName As String
    Private Shared sLoginName As String
    Private Shared sPassword As String
    Private Shared sSAPCode As String
    Private Shared iDesignationID As Integer
    Private Shared iContactAddressPKID As Integer
    Private Shared iPermanentAddressPKID As Integer
    Private Shared iEmergencyContactPKID As Integer
    Private Shared iFMVContactPKID As Integer
    Private Shared lblEQPKID As New Label
    Private Shared lblECSPKID As New Label
    Private Shared lblPEPKID As New Label
    Private Shared lblALPKID As New Label

    Private Shared lblPAPKID As New Label
    Private Shared lblAPPKID As New Label
    Private Shared lblSMPKID As New Label

    Private Shared lblTFPKID As New Label
    Private Shared lblPOAPKID As New Label

    'Private Shared sEPSave As String
    Private Shared sEPFlag As String
    'Private Shared sEPReport As String
    Private Shared sStatus As String
    Private Shared dt As New DataTable
    Private Shared dtEmpDet As New DataTable
    Private Shared sEMDBackStatus As String
    Public sfilename As String

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnAdd.ImageUrl = "~/Images/AddUser24.png"
        imgbtnSave.ImageUrl = "~/Images/Save24.png"
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnEmpMasterSave.ImageUrl = "~/Images/Save24.png"
        imgbtnEmpMasterUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnEQAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnECSAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnPEAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnALAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnPAAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnAPAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnSMAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnTFAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnPOAAttachment.ImageUrl = "~/Images/Attachment16.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        imgbtnBack.ImageUrl = "~/Images/Backward24.png"
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim iEmpID As Integer = 0
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                iAttachID = 0 : iResumeDocID = 0 : iResumeAttachID = 0 : iSignatureAttachID = 0 : lblTab.Text = 1
                Tabs.Visible = True
                imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = False : imgbtnUpdate.Visible = False : imgbtnAddResume.Visible = True
                imgbtnReport.Visible = False
                VisibleFalseTrue("False")
                'sEPSave = "NO" : sEPReport = "NO"
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MPEMP", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        sEPSave = "YES"
                '        imgbtnSave.Visible = True
                '        imgbtnAddResume.Visible = True
                '        VisibleFalseTrue("True")
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        sEPReport = "YES"
                '    End If
                'End If
                'EmployeeBasicDetails
                LoadEmployeeBasicDetails()
                'Employee Master
                'DOB
                RFVDOB.ControlToValidate = "txtDOB" : RFVDOB.ErrorMessage = "Enter Date of Birth."
                REVDOB.ErrorMessage = "Enter valid Date of Birth." : REVDOB.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                'Children
                REVChildrenCount.ErrorMessage = "Only Integer." : REVChildrenCount.ValidationExpression = "^[0-9]{0,10000}$"

                'Contact Address
                RFVCAAddress.ControlToValidate = "txtCAAddress" : RFVCAAddress.ErrorMessage = "Enter Address."
                REVCAAddress.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVCAAddress.ValidationExpression = "^[\s\S]{0,200}$"
                REVCAAddress1.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVCAAddress1.ValidationExpression = "^[\s\S]{0,200}$"
                REVCAAddress2.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVCAAddress2.ValidationExpression = "^[\s\S]{0,200}$"
                'Pincode
                RFVCAPincode.ControlToValidate = "txtCAPincode" : RFVCAPincode.ErrorMessage = "Enter Pincode."
                REVCAPincode.ErrorMessage = "Enter valid Pincode." : REVCAPincode.ValidationExpression = "^[0-9]{0,15}$"
                'Mobile No
                RFVCAMobileNo.ControlToValidate = "txtCAMobileNo" : RFVCAMobileNo.ErrorMessage = "Enter Mobile No."
                REVCAMobileNo.ErrorMessage = "Enter valid 10 digit Mobile No." : REVCAMobileNo.ValidationExpression = "^[0-9]{10}$"
                'Telephone No
                REVCATelephoneNo.ErrorMessage = "Enter valid Telephone No." : REVCATelephoneNo.ValidationExpression = "^[0-9]{0,15}$"

                'Permanent Address
                RFVPAAddress.ControlToValidate = "txtPAAddress" : RFVPAAddress.ErrorMessage = "Enter Address."
                REVPAAddress.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVPAAddress.ValidationExpression = "^[\s\S]{0,200}$"
                REVPAAddress1.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVPAAddress1.ValidationExpression = "^[\s\S]{0,200}$"
                REVPAAddress2.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVPAAddress2.ValidationExpression = "^[\s\S]{0,200}$"
                'Pincode
                RFVPAPincode.ControlToValidate = "txtPAPincode" : RFVPAPincode.ErrorMessage = "Enter Pincode."
                REVPAPincode.ErrorMessage = "Enter valid Pincode." : REVPAPincode.ValidationExpression = "^[0-9]{0,6}$"
                'Mobile No
                RFVPAMobileNo.ControlToValidate = "txtPAMobileNo" : RFVPAMobileNo.ErrorMessage = "Enter Mobile No."
                REVPAMobileNo.ErrorMessage = "Enter valid 10 digit Mobile No." : REVPAMobileNo.ValidationExpression = "^[0-9]{10}$"
                'Telephone No
                REVPATelephoneNo.ErrorMessage = "Enter valid Telephone No." : REVPATelephoneNo.ValidationExpression = "^[0-9]{0,15}$"
                REVPATelephoneNo.ErrorMessage = "Telephone No exceeded maximum size(max 15 characters)." : REVPATelephoneNo.ValidationExpression = "^(.{0,15})$"

                'Emergency Contact
                'Name
                RFVECName.ControlToValidate = "txtECName" : RFVECName.ErrorMessage = "Enter Name."
                REVECName.ErrorMessage = "Name exceeded maximum size(max 50 characters)." : REVECName.ValidationExpression = "^(.{0,50})$"
                'Address
                RFVECAddress.ControlToValidate = "txtECAddress" : RFVECAddress.ErrorMessage = "Enter Address."
                REVECAddress.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVECAddress.ValidationExpression = "^[\s\S]{0,200}$"
                REVECAddress1.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVECAddress1.ValidationExpression = "^[\s\S]{0,200}$"
                REVECAddress2.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVECAddress2.ValidationExpression = "^[\s\S]{0,200}$"
                'Pincode
                RFVECPinCode.ControlToValidate = "txtECPinCode" : RFVECPinCode.ErrorMessage = "Enter Pincode."
                REVECPinCode.ErrorMessage = "Enter valid Pincode." : REVECPinCode.ValidationExpression = "^[0-9]{0,15}$"
                'Mobile No
                RFVECMobileNo.ControlToValidate = "txtECMobileNo" : RFVECMobileNo.ErrorMessage = "Enter Mobile No."
                REVECMobileNo.ErrorMessage = "Enter valid 10 digit Mobile No." : REVECMobileNo.ValidationExpression = "^[0-9]{10}$"
                'Telephone No
                REVECTelephoneNo.ErrorMessage = "Enter valid Telephone No." : REVECTelephoneNo.ValidationExpression = "^[0-9]{0,15}$"
                'EMail
                REVECEmailID.ErrorMessage = "Enter valid E-Mail." : REVECEmailID.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                'Relation
                RFVECRelation.ControlToValidate = "txtECRelation" : RFVECRelation.ErrorMessage = "Enter Relation."
                REVECRelation.ErrorMessage = "Relation exceeded maximum size(max 100 characters)." : REVECRelation.ValidationExpression = "^[\s\S]{0,100}$"

                'Father/Mother/Wife Contact
                'Name
                RFVMVName.ControlToValidate = "txtMVName" : RFVMVName.ErrorMessage = "Enter Name."
                REVMVName.ErrorMessage = "Name exceeded maximum size(max 50 characters)." : REVMVName.ValidationExpression = "^[\s\S]{0,50}$"
                'Address
                RFVMVAddress.ControlToValidate = "txtMVAddress" : RFVMVAddress.ErrorMessage = "Enter Address."
                REVMVAddress.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVMVAddress.ValidationExpression = "^[\s\S]{0,200}$"
                REVMVAddress1.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVMVAddress1.ValidationExpression = "^[\s\S]{0,200}$"
                REVMVAddress2.ErrorMessage = "Address exceeded maximum size(max 200 characters)." : REVMVAddress2.ValidationExpression = "^[\s\S]{0,200}$"
                'Pincode
                RFVMVPinCode.ControlToValidate = "txtMVPinCode" : RFVMVPinCode.ErrorMessage = "Enter Pincode."
                REVMVPinCode.ErrorMessage = "Enter valid Pincode." : REVMVPinCode.ValidationExpression = "^[0-9]{0,15}$"
                'Mobile No
                RFVMVMobileNo.ControlToValidate = "txtMVMobileNo" : RFVMVMobileNo.ErrorMessage = "Enter Mobile No."
                REVMVMobileNo.ErrorMessage = "Enter valid 10 digit Mobile No." : REVMVMobileNo.ValidationExpression = "^[0-9]{10}$"
                'Telephone No
                REVMVTelephoneNo.ErrorMessage = "Enter valid Telephone No." : REVMVTelephoneNo.ValidationExpression = "^[0-9]{0,15}$"
                'EMail
                REVMVEmailID.ErrorMessage = "Enter valid E-Mail." : REVMVEmailID.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                'Relation
                RFVMVEmailSelection.ControlToValidate = "ddlMVEmailSelection" : RFVMVEmailSelection.ErrorMessage = "Select Relation Type."

                'Qualification
                RFVEQEducation.ErrorMessage = "Enter Education." : RFVEQBoard.ErrorMessage = "Enter University/Board."
                RFVEQSchool.ErrorMessage = "Enter School/College." : RFVEQYear.ErrorMessage = "Enter Year."
                RFVEQMarks.ErrorMessage = "Enter Marks."

                REVEQEducation.ErrorMessage = "Education exceeded maximum size(max 1000 characters)." : REVEQEducation.ValidationExpression = "^[\s\S]{0,1000}$"
                REVEQBoard.ErrorMessage = "University/Board exceeded maximum size(max 1000 characters)." : REVEQBoard.ValidationExpression = "^[\s\S]{0,1000}$"
                REVEQSchool.ErrorMessage = "School/College exceeded maximum size(max 1000 characters)." : REVEQSchool.ValidationExpression = "^[\s\S]{0,1000}$"
                REVEQRemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVEQRemarks.ValidationExpression = "^[\s\S]{0,8000}$"
                REVEQYear.ErrorMessage = "Only Integer." : REVEQYear.ValidationExpression = "^[0-9]{0,10000}$"
                REVEQMarks.ErrorMessage = "Enter Valid Marks." : REVEQMarks.ValidationExpression = "(?!^0*$)(?!^0*\.0*$)^\d{1,2}(\.\d{1,2})|([0-9]{1,2}|[0-9]{1,2}\.0|[0-9]{1,2}\.00)?(100|100\.0|100\.00)?$"

                'Course
                RFVECSDate.ControlToValidate = "txtECSDate" : RFVECSDate.ErrorMessage = "Enter Date."
                REVECSDate.ErrorMessage = "Enter valid Date." : REVECSDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVSubject.ErrorMessage = "Enter Subject." : RFVECSFPEmployer.ErrorMessage = "Enter Fees Paid Employer."
                RFVECSFPEmployee.ErrorMessage = "Enter Fees Paid Employee." : RFVECSConductedBy.ErrorMessage = "Enter Conducted By."
                RFVECSCPEPoints.ErrorMessage = "Enter CPE Points." : RFVECSPapers.ErrorMessage = "Enter Papers Presented."
                RFVECSBriefDesc.ErrorMessage = "Enter Brief Description." : RFVECSFeedBack.ErrorMessage = "Enter FeedBack Report Reference."

                REVSubject.ErrorMessage = "Subject exceeded maximum size(max 1000 characters)." : REVSubject.ValidationExpression = "^[\s\S]{0,1000}$"
                REVECSFPEmployer.ErrorMessage = "Fees Paid Employer exceeded maximum size(max 1000 characters)." : REVECSFPEmployer.ValidationExpression = "^[\s\S]{0,1000}$"
                REVECSFPEmployee.ErrorMessage = "Fees Paid Employee exceeded maximum size(max 1000 characters)." : REVECSFPEmployee.ValidationExpression = "^[\s\S]{0,1000}$"
                REVECSConductedBy.ErrorMessage = "Conducted By exceeded maximum size(max 1000 characters)." : REVECSConductedBy.ValidationExpression = "^[\s\S]{0,1000}$"
                REVECSCPEPoints.ErrorMessage = "CPE Points exceeded maximum size(max 1000 characters)." : REVECSCPEPoints.ValidationExpression = "^[\s\S]{0,1000}$"
                REVECSPapers.ErrorMessage = "Papers Presented exceeded maximum size(max 8000 characters)." : REVECSPapers.ValidationExpression = "^[\s\S]{0,8000}$"
                REVECSBriefDesc.ErrorMessage = "Brief Description exceeded maximum size(max 8000 characters)." : REVECSBriefDesc.ValidationExpression = "^[\s\S]{0,8000}$"
                REVECSFeedBack.ErrorMessage = "FeedBack Report Reference exceeded maximum size(max 8000 characters)." : REVECSFeedBack.ValidationExpression = "^[\s\S]{0,8000}$"
                REVECSRemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVECSRemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                'Professional Experiance
                RFVEPEAssignment.ControlToValidate = "txtEPEAssignment" : RFVEPEAssignment.ErrorMessage = "Enter Assignment."
                REVEPEAssignment.ErrorMessage = "Assignment exceeded maximum size(max 500 characters)." : REVEPEAssignment.ValidationExpression = "^[\s\S]{0,500}$"

                RFVEPEReportingTo.ControlToValidate = "txtEPEReportingTo" : RFVEPEReportingTo.ErrorMessage = "Enter Reporting To."
                REVEPEReportingTo.ErrorMessage = "Reporting To exceeded maximum size(max 50 characters)." : REVEPEReportingTo.ValidationExpression = "^[\s\S]{0,50}$"

                RFVEPEFrom.ControlToValidate = "txtEPEFrom" : RFVEPEFrom.ErrorMessage = "Enter From."
                REVEPEFrom.ErrorMessage = "Only Integer." : REVEPEFrom.ValidationExpression = "^[0-9]{0,15}$"

                RFVEPETo.ControlToValidate = "txtEPETo" : RFVEPETo.ErrorMessage = "Enter To."
                REVEPETo.ErrorMessage = "Only Integer." : REVEPETo.ValidationExpression = "^[0-9]{0,15}$"

                RFVEPESalaryPerAnnum.ControlToValidate = "txtEPESalaryPerAnnum" : RFVEPESalaryPerAnnum.ErrorMessage = "Salary Per Annum."
                REVEPESalaryPerAnnum.ErrorMessage = "Enter valid Salary Per Annum." : REVEPESalaryPerAnnum.ValidationExpression = "^[0-9]+(\.[0-9]{1,2})?$"

                RFVEPEPositionHeld.ControlToValidate = "txtEPEPositionHeld" : RFVEPEPositionHeld.ErrorMessage = "Position Held."
                REVEPEPositionHeld.ErrorMessage = "Position Held exceeded maximum size(max 20 characters)." : REVEPEPositionHeld.ValidationExpression = "^[\s\S]{0,20}$"

                RFVEPERemarks.ControlToValidate = "txtEPERemarks" : RFVEPERemarks.ErrorMessage = "Remarks."
                REVEPERemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVEPERemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                'Assets Obtained On Loan
                RFVEATypeOfAsset.ControlToValidate = "txtEATypeOfAsset" : RFVEATypeOfAsset.ErrorMessage = "Enter Type Of Asset."
                REVEATypeOfAsset.ErrorMessage = "Type Of Asset exceeded maximum size(max 500 characters)." : REVEATypeOfAsset.ValidationExpression = "^[\s\S]{0,500}$"

                RFVEASerialNo.ControlToValidate = "txtEASerialNo" : RFVEASerialNo.ErrorMessage = "Enter Serial No."
                REVEASerialNo.ErrorMessage = "Serial No exceeded maximum size(max 500 characters)." : REVEASerialNo.ValidationExpression = "^[\s\S]{0,500}$"

                RFVEAApproValue.ControlToValidate = "txtEAApproValue" : RFVEAApproValue.ErrorMessage = "Enter Approximate Value."
                REVEAApproValue.ErrorMessage = "Enter valid Approximate Value." : REVEAApproValue.ValidationExpression = "^[0-9]{0,15}$"

                RFVEAIssueDate.ControlToValidate = "txtEAIssueDate" : RFVEAIssueDate.ErrorMessage = "Enter Issue Date."
                REVEAIssueDate.ErrorMessage = "Enter valid Issue Date." : REVEAIssueDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVEADueDate.ControlToValidate = "txtEADueDate" : RFVEADueDate.ErrorMessage = "Enter Due Date."
                REVEADueDate.ErrorMessage = "Enter valid Due Date." : REVEADueDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                REVEARecievedDate.ErrorMessage = "Enter valid Due Date." : REVEARecievedDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVEAConditionIssue.ControlToValidate = "txtEAConditionIssue" : RFVEAConditionIssue.ErrorMessage = "Enter Condition When Issued."
                REVEAConditionIssue.ErrorMessage = "Condition When Issued exceeded maximum size(max 500 characters)." : REVEAConditionIssue.ValidationExpression = "^[\s\S]{0,500}$"

                REVEAConditionReceipt.ErrorMessage = "Condition On Receipt exceeded maximum size(max 500 characters)." : REVEAConditionReceipt.ValidationExpression = "^[\s\S]{0,500}$"

                REVEARemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVEARemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                'Assessment
                RFVPAAssessmentDate.ControlToValidate = "txtPAAssessmentDate" : RFVPAAssessmentDate.ErrorMessage = "Enter Issue Date."
                REVPAAssessmentDate.ErrorMessage = "Enter valid Issue Date." : REVPAAssessmentDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVPARating.ErrorMessage = "Enter Rating." : RFVPAPerformanceAwardPaid.ErrorMessage = "Enter Performance Award Paid."
                RFVPAGradesPromotedFrom.ErrorMessage = "Enter Grade Promoted From." : RFVPAGradesPromotedTo.ErrorMessage = "Enter Grade Promoted To."
                REVPARating.ErrorMessage = "Rating exceeded maximum size(max 1000 characters)." : REVPARating.ValidationExpression = "^[\s\S]{0,1000}$"
                REVPAPerformanceAwardPaid.ErrorMessage = "Performance Award Paid exceeded maximum size(max 1000 characters)." : REVPAPerformanceAwardPaid.ValidationExpression = "^[\s\S]{0,1000}$"
                REVPAGradesPromotedFrom.ErrorMessage = "Grade Promoted From exceeded maximum size(max 1000 characters)." : REVPAGradesPromotedFrom.ValidationExpression = "^[\s\S]{0,1000}$"
                REVPAGradesPromotedTo.ErrorMessage = "Grade Promoted To exceeded maximum size(max 1000 characters)." : REVPAGradesPromotedTo.ValidationExpression = "^[\s\S]{0,1000}$"
                REVPARemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVPARemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                'Academic Progress
                RFVAPExamTaken.ControlToValidate = "txtAPExamTaken" : RFVAPExamTaken.ErrorMessage = "Enter Examination Taken."
                REVAPExamTaken.ErrorMessage = "Enter valid Issue Date." : REVAPExamTaken.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVAPLeaveGranted.ControlToValidate = "txtAPLeaveGranted" : RFVAPLeaveGranted.ErrorMessage = "Enter No. of Days Leave Granted."
                REVAPLeaveGranted.ErrorMessage = "Enter valid No. of Days Leave Granted." : REVAPLeaveGranted.ValidationExpression = "^[0-9]{0,15}$"

                RFVAPMonthExam.ControlToValidate = "ddlAPMonthExam" : RFVAPMonthExam.InitialValue = "0" : RFVAPMonthExam.ErrorMessage = "Enter Month."

                RFVAPGroups.ControlToValidate = "txtAPGroups" : RFVAPGroups.ErrorMessage = "Enter Groups."
                REVAPGroups.ErrorMessage = "Groups exceeded maximum size(max 1000 characters)." : REVAPGroups.ValidationExpression = "^[\s\S]{0,1000}$"

                RFVAPResult.ControlToValidate = "txtAPResult" : RFVAPResult.ErrorMessage = "Enter Result."
                REVAPResult.ErrorMessage = "Result exceeded maximum size(max 1000 characters)." : REVAPResult.ValidationExpression = "^[\s\S]{0,1000}$"

                RFVAPRemarks.ControlToValidate = "txtAPRemarks" : RFVAPRemarks.ErrorMessage = "Enter Remarks."
                REVAPRemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVAPRemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                'Special Mentions
                RFVSMDate.ControlToValidate = "txtSMDate" : RFVSMDate.ErrorMessage = "Enter Date."
                REVSMDate.ErrorMessage = "Enter valid Date." : REVSMDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVSMSpecialMention.ErrorMessage = "Enter Special Mentions." : RFVSMParticulars.ErrorMessage = "Enter Particulars."
                RFVSMDealtWith.ErrorMessage = "Enter How Dealt With."

                REVSMSpecialMention.ErrorMessage = "Special Mentions exceeded maximum size(max 1000 characters)." : REVSMSpecialMention.ValidationExpression = "^[\s\S]{0,1000}$"
                REVSMParticulars.ErrorMessage = "Particulars exceeded maximum size(max 1000 characters)." : REVSMParticulars.ValidationExpression = "^[\s\S]{0,1000}$"
                REVSMDealtWith.ErrorMessage = "How Dealt With From exceeded maximum size(max 1000 characters)." : REVSMDealtWith.ValidationExpression = "^[\s\S]{0,1000}$"

                'Transfers Within The Firm
                RFVTFEarlierPrinciple.ControlToValidate = "txtTFEarlierPrinciple" : RFVTFEarlierPrinciple.ErrorMessage = "Enter Earlier Principal."
                REVTFEarlierPrinciple.ErrorMessage = "Earlier Principal exceeded maximum size(max 1000 characters)." : REVTFEarlierPrinciple.ValidationExpression = "^[\s\S]{0,1000}$"

                RFVTENewPrinciple.ControlToValidate = "txtTENewPrinciple" : RFVTENewPrinciple.ErrorMessage = "Enter New Principal."
                REVTENewPrinciple.ErrorMessage = "New Principal exceeded maximum size(max 1000 characters)." : REVTENewPrinciple.ValidationExpression = "^[\s\S]{0,1000}$"

                RFVTFDateTransfer.ControlToValidate = "txtTFDateTransfer" : RFVTFDateTransfer.ErrorMessage = "Enter Date of Transfer."
                REVTFDateTransfer.ErrorMessage = "Enter valid Date of Transfer." : REVTFDateTransfer.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVTFDurationArticle.ControlToValidate = "txtTFDurationArticle" : RFVTFDurationArticle.ErrorMessage = "Enter Duration of Article with new Principal."
                REVTFDurationArticle.ErrorMessage = "Duration of Article with new Principal exceeded maximum size(max 1000 characters)." : REVTFDurationArticle.ValidationExpression = "^[\s\S]{0,1000}$"

                RFVTFCompletionDate.ControlToValidate = "txtTFCompletionDate" : RFVTFCompletionDate.ErrorMessage = "Enter Completion Date."
                REVTFCompletionDate.ErrorMessage = "Enter valid Completion Date." : REVTFCompletionDate.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVTFExtendedTo.ControlToValidate = "txtTFExtendedTo" : RFVTFExtendedTo.ErrorMessage = "Enter Extended To."
                REVTFExtendedTo.ErrorMessage = "Enter valid Date of Transfer." : REVTFExtendedTo.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVTFRemarks.ControlToValidate = "txtTFRemarks" : RFVTFRemarks.ErrorMessage = "Enter Remarks."
                REVTFRemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVTFRemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                'Particulars of Articles
                RFVPOAArticlesFrom.ControlToValidate = "txtPOAArticlesFrom" : RFVPOAArticlesFrom.ErrorMessage = "Enter  Period of Articles From Date."
                REVPOAArticlesFrom.ErrorMessage = "Enter valid Period of Articles From Date." : REVPOAArticlesFrom.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVPOAArticlesTo.ControlToValidate = "txtPOAArticlesTo" : RFVPOAArticlesTo.ErrorMessage = "Enter Period of Articles To Date."
                REVPOAArticlesTo.ErrorMessage = "Enter valid Period of Articles To Date." : REVPOAArticlesTo.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"
                RFVPOAArticlesExtendedTo.ControlToValidate = "txtPOAArticlesExtendedTo" : RFVPOAArticlesExtendedTo.ErrorMessage = "Enter Period of Articles Extended To Date."
                REVPOAArticlesExtendedTo.ErrorMessage = "Enter valid Period of Articles Extended To Date." : REVPOAArticlesExtendedTo.ValidationExpression = "(^(((0[1-9]|1[0-9]|2[0-8])[\/](0[1-9]|1[012]))|((29|30|31)[\/](0[13578]|1[02]))|((29|30)[\/](0[4,6,9]|11)))[\/](19|[2-9][0-9])\d\d$)|(^29[\/]02[\/](19|[2-9][0-9])(00|04|08|12|16|20|24|28|32|36|40|44|48|52|56|60|64|68|72|76|80|84|88|92|96)$)"

                RFVPOAPrincipleName.ErrorMessage = "Enter Name of the Principal." : RFVPOAArticleRegNo.ErrorMessage = "Enter Article Registration No."
                RFVPOAPracticeNo.ErrorMessage = "Enter Certificate of Practice No."

                REVPOAPrincipleName.ErrorMessage = "Name of the Principal exceeded maximum size(max 100 characters)." : REVPOAPrincipleName.ValidationExpression = "^[\s\S]{0,100}$"
                REVPOAArticleRegNo.ErrorMessage = "Article Registration No exceeded maximum size(max 100 characters)." : REVPOAArticleRegNo.ValidationExpression = "^[\s\S]{0,100}$"
                REVPOAPracticeNo.ErrorMessage = "Certificate of Practice No From exceeded maximum size(max 100 characters)." : REVPOAPracticeNo.ValidationExpression = "^[\s\S]{0,100}$"
                REVPOARemarks.ErrorMessage = "Remarks exceeded maximum size(max 8000 characters)." : REVPOARemarks.ValidationExpression = "^[\s\S]{0,8000}$"

                liEmpBasic.Attributes.Add("class", "active") : divEmpBasic.Attributes.Add("class", "tab-pane active")
                lblTab.Text = 1
                BindExistingEmployeeDB(0, 0, 0, 0, "")
                txtSAPCode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID)
                iEQDocID = 0 : iEQAttachID = 0 : iEmpPEAttachID = 0 : iEmpPEDocID = 0 : iEmpALAttachID = 0 : iEmpALDocID = 0 : iECSDocID = 0 : iECSAttachID = 0 : sEmpDetailsAttachIDs = ""
                lblEQBadgeCount.Text = 0 : lblECSBadgeCount.Text = 0 : lblBadgePECount.Text = 0 : lblBadgeALCount.Text = 0
                iPADocID = 0 : iPAAttachID = 0 : iEmpAPAttachID = 0 : iEmpAPDocID = 0 : iSMDocID = 0 : iSMAttachID = 0 : sHRAttachIDs = ""
                lblPABadgeCount.Text = 0 : lblBadgeAPCount.Text = 0 : lblSMBadgeCount.Text = 0
                iEmpTFAttachID = 0 : iEmpTFDocID = 0 : iPOAAttachID = 0 : iPOADocID = 0 : sArticleAttachIDs = ""
                lblBadgeTFCount.Text = 0 : lblPOABadgeCount.Text = 0

                lblEQSize.Text = "(Max " & sSession.FileSize & "MB)"
                lblECSSize.Text = "(Max " & sSession.FileSize & "MB)"
                lblPESize.Text = "(Max " & sSession.FileSize & "MB)"
                lblALSize.Text = "(Max " & sSession.FileSize & "MB)"

                lblPASize.Text = "(Max " & sSession.FileSize & "MB)"
                lblAPSize.Text = "(Max " & sSession.FileSize & "MB)"
                lblSMSize.Text = "(Max " & sSession.FileSize & "MB)"

                lblTFSize.Text = "(Max " & sSession.FileSize & "MB)"
                lblPOASize.Text = "(Max " & sSession.FileSize & "MB)"

                'imgbtnEQAttachment.Attributes.Add("OnClick", "$('#myModalEQAttachment').modal('show');return false;")
                'imgbtnECSAttachment.Attributes.Add("OnClick", "$('#myModalECSAttachment').modal('show');return false;")
                'imgbtnPEAttachment.Attributes.Add("OnClick", "$('#myModalProfessionalExperienceAttchment').modal('show');return false;")
                'imgbtnALAttachment.Attributes.Add("OnClick", "$('#myModalAsstesLoanAttchment').modal('show');return false;")

                'imgbtnPAAttachment.Attributes.Add("OnClick", "$('#myModalPAAttachment').modal('show');return false;")
                'imgbtnAPAttachment.Attributes.Add("OnClick", "$('#myModalAcademicProgressAttchment').modal('show');return false;")
                'imgbtnSMAttachment.Attributes.Add("OnClick", "$('#myModalSMAttachment').modal('show');return false;")

                'imgbtnTFAttachment.Attributes.Add("OnClick", "$('#myModalTransferFirmAttchment').modal('show');return false;")
                'imgbtnPOAAttachment.Attributes.Add("OnClick", "$('#myModalPOAAttachment').modal('show');return false;")

                If Request.QueryString("EmpID") IsNot Nothing Then
                    iEmpID = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("EmpID")))
                    If iEmpID > 0 Then
                        Dim liEmpID As ListItem = ddlExistingEmployee.Items.FindByValue(iEmpID)
                        If IsNothing(liEmpID) = False Then
                            ddlExistingEmployee.SelectedValue = iEmpID
                            ddlExistingEmployee_SelectedIndexChanged(sender, e)
                        End If
                    End If
                End If
                If Request.QueryString("StatusID") IsNot Nothing Then
                    sEMDBackStatus = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(Request.QueryString("StatusID")))
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadEmployeeBasicDetails()
        Try
            BindZone() : BindDesignationDB()
            BindRoleDB() : BindPermission() : BindModule()
            BindExistingEmployeeDB(0, 0, 0, 0, "")
            RFVZone.ErrorMessage = "Select Zone." : RFVZone.InitialValue = "Select Zone"

            RFVSAPCode.ErrorMessage = "Enter EMP Code." : REVSAPCode.ValidationExpression = "^[a-zA-Z0-9'@&amp;amp;#.\s]{0,10}$" : REVSAPCode.ErrorMessage = "Enter valid EMP Code."

            RFVEmpName.ErrorMessage = "Enter Employee Name." : REVEmpName.ErrorMessage = "Enter valid Employee Name." : REVEmpName.ValidationExpression = "^(.{0,50})$"

            RFVLoginName.ErrorMessage = "Enter Login Name." : REVLoginName.ErrorMessage = "Enter valid Login Name." : REVLoginName.ValidationExpression = "^[a-zA-Z0-9'@&amp;amp;#.\s]{0,25}$"

            RFVPasssword.ErrorMessage = "Enter Password." : RFVConfirmPassword.ErrorMessage = "Enter Confirm Password." : CVPassword.ErrorMessage = "Passwords does not match."

            RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" '"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            REVOffice.ErrorMessage = "Enter valid Office Phone No." : REVOffice.ValidationExpression = "^[0-9]{0,15}$"

            REVMobile.ErrorMessage = "Enter valid 10 digit Mobile No." : REVMobile.ValidationExpression = "^[0-9]{10}$"

            REVResidence.ErrorMessage = "Enter valid Residence Phone No." : REVResidence.ValidationExpression = "^[0-9]{0,15}$"

            RFVDesignation.ErrorMessage = "Select Designation." : RFVDesignation.InitialValue = "Select Designation"

            RFVModule.ErrorMessage = "Select Module." : RFVModule.InitialValue = "Select Module"

            RFVRole.ErrorMessage = "Select Role." : RFVRole.InitialValue = "Select Role"
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindRoleDB()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveRole(sSession.AccessCode, sSession.AccessCodeID)
            ddlRole.DataSource = dt
            ddlRole.DataTextField = "Mas_Description"
            ddlRole.DataValueField = "Mas_ID"
            ddlRole.DataBind()
            ddlRole.Items.Insert(0, "Select Role")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindDesignationDB()
        Dim dt As New DataTable
        Try
            dt = objclsAllActiveMaster.LoadActiveDesignation(sSession.AccessCode, sSession.AccessCodeID)
            ddlDesignation.DataSource = dt
            ddlDesignation.DataTextField = "Mas_Description"
            ddlDesignation.DataValueField = "Mas_ID"
            ddlDesignation.DataBind()
            ddlDesignation.Items.Insert(0, "Select Designation")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindZone()
        Dim dt As New DataTable
        Try
            dt = objclsEmployeeMaster.LoadZoneMaster(sSession.AccessCode, sSession.AccessCodeID)
            ddlZone.DataSource = dt
            ddlZone.DataTextField = "Org_Name"
            ddlZone.DataValueField = "org_node"
            ddlZone.DataBind()
            ddlZone.Items.Insert(0, "Select Zone")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindModule()
        Try
            ddlGroup.Items.Insert(0, "Select Module")
            ddlGroup.Items.Insert(1, "Master")
            ddlGroup.Items.Insert(2, "Audit")
            ddlGroup.Items.Insert(3, "Risk")
            ddlGroup.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindPermission()
        Try
            ddlPermission.Items.Insert(0, "Role based")
            ddlPermission.Items.Insert(1, "User based")
            ddlPermission.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearAllMaster()
        Try
            lblError.Text = "" : sEPFlag = "" : chkChangeLevel.Checked = False : chkChangeLevel.Visible = False : lblChangeLevel.Visible = False
            txtSAPCode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID) : txtEmployeeName.Text = "" : txtLoginName.Text = ""
            ddlDesignation.SelectedIndex = 0 : ddlRole.SelectedIndex = 0 : ddlGroup.SelectedIndex = 0 : ddlPermission.SelectedIndex = 0
            txtEmail.Text = "" : txtOffice.Text = "" : txtMobile.Text = "" : txtResidence.Text = ""
            txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
            txtPassword.Text = "" : txtConfirmPassword.Text = ""
            txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
            imgbtnAdd.Visible = True : imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            'If sEPSave = "YES" Then
            imgbtnSave.Visible = True
            'End If
            chkSendMail.Checked = False
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub VisibleFalseTrue(ByVal sBoolean As String)
        Try
            btnEQSave.Visible = sBoolean : btnECSSave.Visible = sBoolean : btnEPESave.Visible = sBoolean : btnALSave.Visible = sBoolean
            btnPASave.Visible = sBoolean : btnAPSave.Visible = sBoolean : btnSMSave.Visible = sBoolean
            btnTFSave.Visible = sBoolean : btnPOASave.Visible = sBoolean

            btnAddEQAttch.Visible = sBoolean : btnAddECSAttch.Visible = sBoolean : btnAddPEAttach.Visible = sBoolean : btnAddALAttach.Visible = sBoolean
            btnAddPAAttch.Visible = sBoolean : btnAddAPAttach.Visible = sBoolean : btnAddSMAttch.Visible = sBoolean
            btnAddTFAttach.Visible = sBoolean : btnAddPOAAttch.Visible = sBoolean
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindExistingEmployeeDB(ByVal iZoneID As Integer, ByVal iRegionID As Integer, ByVal iAreaID As Integer, iBranchID As Integer, ByVal sSearch As String)
        Try
            ddlExistingEmployee.DataSource = objclsEmployeeMaster.LoadExistingEmployee(sSession.AccessCode, sSession.AccessCodeID, iZoneID, iRegionID, iAreaID, iBranchID, sSearch)
            ddlExistingEmployee.DataTextField = "FullName"
            ddlExistingEmployee.DataValueField = "Usr_ID"
            ddlExistingEmployee.DataBind()
            ddlExistingEmployee.Items.Insert(0, "Select Existing Employee")
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ClearAll()
        Try
            lblError.Text = "" : txtDOB.Text = "" : txtCAAddress.Text = "" : txtCAAddress1.Text = "" : txtCAAddress2.Text = "" : txtCAPincode.Text = "" : txtCAMobileNo.Text = ""
            txtCATelephoneNo.Text = "" : ddlBloodGroup.SelectedIndex = 0 : txtPAAddress.Text = "" : txtPAAddress1.Text = "" : txtPAAddress2.Text = "" : txtPAPincode.Text = ""
            txtPAMobileNo.Text = "" : txtPATelephoneNo.Text = "" : txtChildrenCount.Text = "" : txtECName.Text = "" : txtECAddress.Text = "" : txtECAddress1.Text = ""
            txtECAddress2.Text = "" : txtECPinCode.Text = "" : txtECMobileNo.Text = "" : txtECTelephoneNo.Text = "" : txtECEmailID.Text = "" : txtECRelation.Text = ""
            txtMVName.Text = "" : txtMVAddress.Text = "" : txtMVAddress1.Text = "" : txtMVAddress2.Text = "" : txtMVPinCode.Text = ""
            txtMVMobileNo.Text = "" : txtMVTelephoneNo.Text = "" : txtMVEmailID.Text = "" : ddlMVEmailSelection.SelectedIndex = 1
            gvResumeAttach.DataSource = Nothing
            gvResumeAttach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ClearAttachDetails()
        Try
            iEQAttachID = 0 : iECSAttachID = 0 : iEmpPEAttachID = 0 : iEmpALAttachID = 0
            iPAAttachID = 0 : iEmpAPAttachID = 0 : iSMAttachID = 0 : iEmpTFAttachID = 0 : iPOAAttachID = 0
            iEQDocID = 0 : iECSDocID = 0 : iEmpPEDocID = 0 : iEmpALDocID = 0
            iPADocID = 0 : iEmpAPDocID = 0 : iSMDocID = 0 : iEmpTFDocID = 0 : iPOADocID = 0
            sEmpDetailsAttachIDs = "" : sHRAttachIDs = "" : sArticleAttachIDs = ""
            lblEQBadgeCount.Text = 0 : lblECSBadgeCount.Text = 0 : lblBadgePECount.Text = 0 : lblBadgeALCount.Text = 0
            lblPABadgeCount.Text = 0 : lblBadgeAPCount.Text = 0 : lblSMBadgeCount.Text = 0
            lblBadgeTFCount.Text = 0 : lblPOABadgeCount.Text = 0
            gvEmpQualification.DataSource = Nothing
            gvEmpQualification.DataBind()
            gvCourse.DataSource = Nothing
            gvCourse.DataBind()
            gvProfessionalExperience.DataSource = Nothing
            gvProfessionalExperience.DataBind()
            gvAssestsLoan.DataSource = Nothing
            gvAssestsLoan.DataBind()
            gvEmpDetailsAttach.DataSource = Nothing
            gvEmpDetailsAttach.DataBind()

            gvPerformanceAssessments.DataSource = Nothing
            gvPerformanceAssessments.DataBind()
            gvAcademicProgress.DataSource = Nothing
            gvAcademicProgress.DataBind()
            gvSpecialMentions.DataSource = Nothing
            gvSpecialMentions.DataBind()
            gvHRDetailsAttach.DataSource = Nothing
            gvHRDetailsAttach.DataBind()

            gvTransferswithintheFirm.DataSource = Nothing
            gvTransferswithintheFirm.DataBind()
            gvParticularsofArticles.DataSource = Nothing
            gvParticularsofArticles.DataBind()
            gvArticleAttach.DataSource = Nothing
            gvArticleAttach.DataBind()

            gvEQAttach.DataSource = Nothing
            gvEQAttach.DataBind()
            gvECSAttach.DataSource = Nothing
            gvECSAttach.DataBind()
            gvProfessionalExperienceAttach.DataSource = Nothing
            gvProfessionalExperienceAttach.DataBind()
            gvAsstesLoanAttach.DataSource = Nothing
            gvAsstesLoanAttach.DataBind()

            gvPAAttach.DataSource = Nothing
            gvPAAttach.DataBind()
            gvAcademicProgressAttach.DataSource = Nothing
            gvAcademicProgressAttach.DataBind()
            gvSMAttach.DataSource = Nothing
            gvSMAttach.DataBind()

            gvTransferFirmAttach.DataSource = Nothing
            gvTransferFirmAttach.DataBind()
            gvPOAAttach.DataSource = Nothing
            gvPOAAttach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub ddlExistingEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExistingEmployee.SelectedIndexChanged
        Dim dtTab As DataTable
        Dim sPaths As String, sDestFilePath As String, sDestFilePath2 As String
        Dim iGetZoneID As Integer, iGetRegionID As Integer, iGetAreaID As Integer
        Try
            ClearAll() : imgbtnReport.Visible = False : imgbtnSave.Visible = False : ClearAllMaster()
            ClearAttachDetails()
            RetrievePhotoUpload.ImageUrl = ""
            RetrieveSignatureUpload.ImageUrl = ""
            'RetrieveSignatureUpload1.ImageUrl = ""
            imgbtnReport.Visible = False
            ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            lnkbtnEmpBasicDetails_Click(sender, e)
            'If sEPSave = "True" Then
            imgbtnSave.Visible = True
            'End If
            If ddlExistingEmployee.SelectedIndex > 0 Then
                chkChangeLevel.Checked = True : chkChangeLevel.Visible = True : lblChangeLevel.Visible = True
                'If sEPReport = "YES" Then
                imgbtnReport.Visible = True
                'End If
                dt = objclsEProfile.LoadExistingEmployeeBasicDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue)
                If dt.Rows.Count > 0 Then
                    'Employee Basic Details
                    txtSAPCode.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Code")) = False Then
                        txtSAPCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_Code").ToString())
                    End If

                    txtEmployeeName.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_FullName")) = False Then
                        txtEmployeeName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_FullName").ToString())
                    End If

                    ddlDesignation.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_Designation")) = False Then
                        Dim liDesignationID As ListItem = ddlDesignation.Items.FindByValue(Val(dt.Rows(0).Item("Usr_Designation")))
                        If IsNothing(liDesignationID) = False Then
                            ddlDesignation.SelectedValue = Val(dt.Rows(0).Item("Usr_Designation"))
                        End If
                    End If

                    txtOffice.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_OfficePhone")) = False Then
                        txtOffice.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_OfficePhone").ToString())
                    End If

                    txtMobile.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_MobileNo").ToString()) = False Then
                        txtMobile.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_MobileNo").ToString())
                    End If

                    txtResidence.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_PhoneNo")) = False Then
                        txtResidence.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_PhoneNo").ToString())
                    End If

                    txtEmail.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Email")) = False Then
                        txtEmail.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_Email").ToString())
                    End If

                    chkSendMail.Checked = False
                    If IsDBNull(dt.Rows(0).Item("Usr_Category")) = False Then
                        If dt.Rows(0).Item("Usr_Category") = 1 Then
                            chkSendMail.Checked = True
                        End If
                    End If

                    chkIsPartner.Checked = False
                    'pnlsignature.Visible = False
                    If IsDBNull(dt.Rows(0).Item("Usr_Partner")) = False Then
                        If dt.Rows(0).Item("Usr_Partner") = 1 Then
                            chkIsPartner.Checked = True
                            'pnlsignature.Visible = True
                        End If
                    End If

                    ddlRole.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_Role")) = False Then
                        Dim liRoleID As ListItem = ddlRole.Items.FindByValue(Val(dt.Rows(0).Item("Usr_Role")))
                        If IsNothing(liRoleID) = False Then
                            ddlRole.SelectedValue = Val(dt.Rows(0).Item("Usr_Role"))
                        End If
                    End If

                    ddlGroup.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_LevelGrp")) = False Then
                        If ddlGroup.Items.Count >= dt.Rows(0).Item("Usr_LevelGrp") Then
                            ddlGroup.SelectedIndex = Val(dt.Rows(0).Item("Usr_LevelGrp"))
                        End If
                    End If

                    txtLoginName.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_LoginName")) = False Then
                        txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(0).Item("Usr_LoginName").ToString())
                    End If

                    txtPassword.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Password")) = False Then
                        txtPassword.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(dt.Rows(0).Item("Usr_Password").ToString()))
                        txtPassword.TextMode = TextBoxMode.SingleLine : txtPassword.TextMode = TextBoxMode.Password
                    End If

                    txtConfirmPassword.Text = ""
                    If IsDBNull(dt.Rows(0).Item("Usr_Password")) = False Then
                        txtConfirmPassword.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(dt.Rows(0).Item("Usr_Password").ToString()))
                        txtConfirmPassword.TextMode = TextBoxMode.SingleLine : txtConfirmPassword.TextMode = TextBoxMode.Password
                    End If

                    ddlPermission.SelectedIndex = 0
                    If IsDBNull(dt.Rows(0).Item("Usr_GrpOrUserLvlPerm")) = False Then
                        If ddlPermission.Items.Count >= dt.Rows(0).Item("Usr_GrpOrUserLvlPerm") Then
                            ddlPermission.SelectedIndex = Val(dt.Rows(0).Item("Usr_GrpOrUserLvlPerm"))
                        End If
                    End If

                    If IsDBNull(dt.Rows(0).Item("Usr_Delflag")) = False Then
                        sEPFlag = dt.Rows(0).Item("Usr_Delflag")
                        If dt.Rows(0).Item("Usr_Delflag").ToString() = "W" Then
                            lblError.Text = "Waiting for Approval."
                            sStatus = "W"
                            'If sEPSave = "YES" Then
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                            'Else
                            '    imgbtnUpdate.Visible = False
                            'End If
                        ElseIf dt.Rows(0).Item("Usr_Delflag").ToString() = "D" Then
                            lblError.Text = "De-Activated."
                            sStatus = "D"
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
                            VisibleFalseTrue("False")
                        Else
                            sStatus = "A"
                            'If sEPSave = "YES" Then
                            imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                            'Else
                            '    imgbtnUpdate.Visible = False
                            'End If
                        End If
                    End If

                    If IsDBNull(dt.Rows(0).Item("Usr_Node")) = False Then
                        'Zone Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "1" Then
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If

                        'Region Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "2" Then
                            iGetZoneID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("Usr_OrgnID").ToString())
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(iGetZoneID))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = iGetZoneID

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")

                                Dim liRegionID As ListItem = ddlRegion.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                                If IsNothing(liRegionID) = False Then
                                    ddlRegion.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")

                                    ddlArea.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                                    ddlArea.DataTextField = "Org_Name"
                                    ddlArea.DataValueField = "org_node"
                                    ddlArea.DataBind()
                                    ddlArea.Items.Insert(0, "Select Area")
                                Else
                                    ddlRegion.SelectedIndex = 0 : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                                End If
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If

                        'Area Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "3" Then
                            iGetRegionID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("Usr_OrgnID").ToString())
                            iGetZoneID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, iGetRegionID)
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(iGetZoneID))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = iGetZoneID

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")

                                Dim liRegionID As ListItem = ddlRegion.Items.FindByValue(Val(iGetRegionID))
                                If IsNothing(liRegionID) = False Then
                                    ddlRegion.SelectedValue = iGetRegionID

                                    ddlArea.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                                    ddlArea.DataTextField = "Org_Name"
                                    ddlArea.DataValueField = "org_node"
                                    ddlArea.DataBind()
                                    ddlArea.Items.Insert(0, "Select Area")

                                    Dim liAreaID As ListItem = ddlArea.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                                    If IsNothing(liAreaID) = False Then
                                        ddlArea.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")

                                        ddlBranch.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlArea.SelectedValue)
                                        ddlBranch.DataTextField = "Org_Name"
                                        ddlBranch.DataValueField = "org_node"
                                        ddlBranch.DataBind()
                                        ddlBranch.Items.Insert(0, "Select Branch")
                                    Else
                                        ddlArea.SelectedIndex = 0 : ddlBranch.Items.Clear()
                                    End If
                                Else
                                    ddlRegion.SelectedIndex = 0 : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                                End If
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If

                        'Branch Level
                        If dt.Rows(0).Item("Usr_Node").ToString() = "4" Then
                            iGetAreaID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, dt.Rows(0).Item("Usr_OrgnID").ToString())
                            iGetRegionID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, iGetAreaID)
                            iGetZoneID = objclsEmployeeMaster.GetParentID(sSession.AccessCode, sSession.AccessCodeID, iGetRegionID)
                            Dim liZoneID As ListItem = ddlZone.Items.FindByValue(Val(iGetZoneID))
                            If IsNothing(liZoneID) = False Then
                                ddlZone.SelectedValue = iGetZoneID

                                ddlRegion.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                                ddlRegion.DataTextField = "Org_Name"
                                ddlRegion.DataValueField = "org_node"
                                ddlRegion.DataBind()
                                ddlRegion.Items.Insert(0, "Select Region")

                                Dim liRegionID As ListItem = ddlRegion.Items.FindByValue(Val(iGetRegionID))
                                If IsNothing(liRegionID) = False Then
                                    ddlRegion.SelectedValue = iGetRegionID

                                    ddlArea.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                                    ddlArea.DataTextField = "Org_Name"
                                    ddlArea.DataValueField = "org_node"
                                    ddlArea.DataBind()
                                    ddlArea.Items.Insert(0, "Select Area")

                                    Dim liAreaID As ListItem = ddlArea.Items.FindByValue(Val(iGetAreaID))
                                    If IsNothing(liAreaID) = False Then
                                        ddlArea.SelectedValue = iGetAreaID

                                        ddlBranch.DataSource = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlArea.SelectedValue)
                                        ddlBranch.DataTextField = "Org_Name"
                                        ddlBranch.DataValueField = "org_node"
                                        ddlBranch.DataBind()
                                        ddlBranch.Items.Insert(0, "Select Branch")

                                        Dim liBranchID As ListItem = ddlBranch.Items.FindByValue(Val(dt.Rows(0).Item("Usr_OrgnID")))
                                        If IsNothing(liBranchID) = False Then
                                            ddlBranch.SelectedValue = dt.Rows(0).Item("Usr_OrgnID")
                                        Else
                                            ddlBranch.SelectedIndex = 0
                                        End If
                                    Else
                                        ddlArea.SelectedIndex = 0 : ddlBranch.Items.Clear()
                                    End If
                                Else
                                    ddlRegion.SelectedIndex = 0 : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                                End If
                            Else
                                ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
                            End If
                        End If
                    End If
                    dtEmpDet = objclsEProfile.LoadExistingEmployeeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue)
                    If dtEmpDet.Rows.Count > 0 Then
                        'Employee Details                                        
                        If IsDBNull(dtEmpDet.Rows(0).Item("CPKID")) = False Then
                            iContactAddressPKID = Val(dtEmpDet.Rows(0).Item("CPKID"))
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("PPKID")) = False Then
                            iPermanentAddressPKID = Val(dtEmpDet.Rows(0).Item("PPKID"))
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EPKID")) = False Then
                            iEmergencyContactPKID = Val(dtEmpDet.Rows(0).Item("EPKID"))
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVPKID")) = False Then
                            iFMVContactPKID = Val(dtEmpDet.Rows(0).Item("MFVPKID"))
                        End If
                        sFullName = dtEmpDet.Rows(0).Item("Usr_FullName")
                        sLoginName = dtEmpDet.Rows(0).Item("usr_LoginName")
                        sPassword = dtEmpDet.Rows(0).Item("usr_PassWord")
                        sSAPCode = dtEmpDet.Rows(0).Item("Usr_Code")
                        iDesignationID = dtEmpDet.Rows(0).Item("usr_Designation")
                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_DOB")) = False Then
                            If dtEmpDet.Rows(0).Item("usr_DOB") = "01/01/1900" Then
                                txtDOB.Text = ""
                            Else
                                txtDOB.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("usr_DOB").ToString())
                            End If
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_Gender")) = False Then
                            If dtEmpDet(0).Item("usr_Gender") = 1 Then
                                rboMale.Checked = True
                            Else
                                rboFemale.Checked = True
                            End If
                        End If

                        ddlBloodGroup.SelectedIndex = 0
                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_BloodGroup")) = False Then
                            If dtEmpDet.Rows(0).Item("usr_BloodGroup") <> "" Then
                                ddlBloodGroup.SelectedValue = dtEmpDet.Rows(0).Item("usr_BloodGroup")
                            End If
                        End If

                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_MaritalStatus")) = False Then
                            If dtEmpDet.Rows(0).Item("usr_MaritalStatus") = 1 Then
                                rboSingle.Checked = True
                            Else
                                rboMarried.Checked = True
                            End If
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_NoOfChildren")) = False Then
                            txtChildrenCount.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("usr_NoOfChildren").ToString())
                        End If
                        'Contact Address
                        If IsDBNull(dtEmpDet.Rows(0).Item("CAddress1")) = False Then
                            txtCAAddress.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("CAddress1").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("CAddress2")) = False Then
                            txtCAAddress1.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("CAddress2").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("CAddress3")) = False Then
                            txtCAAddress2.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("CAddress3").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("CPincode")) = False Then
                            txtCAPincode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("CPincode").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("CMobile")) = False Then
                            txtCAMobileNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("CMobile").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("CTelephone")) = False Then
                            txtCATelephoneNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("CTelephone").ToString())
                        End If

                        'Permanent Address
                        If IsDBNull(dtEmpDet.Rows(0).Item("PAddress1")) = False Then
                            txtPAAddress.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("PAddress1").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("PAddress2")) = False Then
                            txtPAAddress1.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("PAddress2").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("PAddress3")) = False Then
                            txtPAAddress2.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("PAddress3").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("PPincode")) = False Then
                            txtPAPincode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("PPincode").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("PMobile")) = False Then
                            txtPAMobileNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("PMobile").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("PTelephone")) = False Then
                            txtPATelephoneNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("PTelephone").ToString())
                        End If

                        'Emergency Contact
                        If IsDBNull(dtEmpDet.Rows(0).Item("EContactName")) = False Then
                            txtECName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EContactName").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EAddress1")) = False Then
                            txtECAddress.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EAddress1").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EAddress2")) = False Then
                            txtECAddress1.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EAddress2").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EAddress3")) = False Then
                            txtECAddress2.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EAddress3").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EPincode")) = False Then
                            txtECPinCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EPincode").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EMobile")) = False Then
                            txtECMobileNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EMobile").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("ETelephone")) = False Then
                            txtECTelephoneNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("ETelephone").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("EEmail")) = False Then
                            txtECEmailID.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("EEmail").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("ERelationType")) = False Then
                            txtECRelation.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("ERelationType").ToString())
                        End If

                        'Mother/Father/Wife Contact
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVContactName")) = False Then
                            txtMVName.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVContactName").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVAddress1")) = False Then
                            txtMVAddress.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVAddress1").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVAddress2")) = False Then
                            txtMVAddress1.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVAddress2").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVAddress3")) = False Then
                            txtMVAddress2.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVAddress3").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVPincode")) = False Then
                            txtMVPinCode.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVPincode").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVMobile")) = False Then
                            txtMVMobileNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVMobile").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVTelephone")) = False Then
                            txtMVTelephoneNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVTelephone").ToString())
                        End If
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVEmail")) = False Then
                            txtMVEmailID.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVEmail").ToString())
                        End If
                        ddlMVEmailSelection.SelectedIndex = 0
                        If IsDBNull(dtEmpDet.Rows(0).Item("MFVRelationType")) = False Then
                            ddlMVEmailSelection.SelectedValue = objclsGRACeGeneral.ReplaceSafeSQL(dtEmpDet.Rows(0).Item("MFVRelationType").ToString())
                        End If

                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_Signature")) = False Then
                            If dtEmpDet.Rows(0).Item("usr_Signature") > 0 Then
                                iSignatureDocID = objclsEProfile.GetPhotoDocID(sSession.AccessCode, sSession.AccessCodeID, dtEmpDet.Rows(0).Item("usr_Signature"))
                                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                                sDestFilePath2 = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, dtEmpDet.Rows(0).Item("usr_Signature"), iSignatureDocID)
                                'To Retrieve Image
                                '  If System.IO.Directory.Exists(sDestFilePath) = True Then  
                                If System.IO.File.Exists(sDestFilePath2) = True Then
                                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sDestFilePath2)
                                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                                    RetrieveSignatureUpload.ImageUrl = imageDataURL
                                    RetrieveSignatureUpload.Visible = True
                                    'RetrieveSignatureUpload1.ImageUrl = imageDataURL
                                    'RetrieveSignatureUpload1.Visible = True
                                End If
                            End If
                        End If

                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_Photo")) = False Then
                            If dtEmpDet.Rows(0).Item("usr_Photo") > 0 Then
                                iPhotoDocID = objclsEProfile.GetPhotoDocID(sSession.AccessCode, sSession.AccessCodeID, dtEmpDet.Rows(0).Item("usr_Photo"))
                                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, dtEmpDet.Rows(0).Item("usr_Photo"), iPhotoDocID)
                                'To Retrieve Image
                                If System.IO.File.Exists(sDestFilePath) = True Then
                                    Dim bytes As Byte() = System.IO.File.ReadAllBytes(sDestFilePath)
                                    Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                                    Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                                    RetrievePhotoUpload.ImageUrl = imageDataURL
                                    RetrievePhotoUpload.Visible = True
                                End If
                            End If
                        End If

                        If IsDBNull(dtEmpDet.Rows(0).Item("usr_Resume")) = False Then
                            iResumeAttachID = dtEmpDet.Rows(0).Item("usr_Resume")
                            If iResumeAttachID > 0 Then
                                BindAllAttachments(sSession.AccessCode, iResumeAttachID)
                            End If
                        End If
                    End If
                    'Employee Details
                    dtTab = objclsEProfile.LoadEMPQualification(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvEmpQualification.DataSource = dtTab
                        gvEmpQualification.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iEQAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    dtTab = objclsEProfile.LoadEMPCourse(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvCourse.DataSource = dtTab
                        gvCourse.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iECSAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    dtTab = objclsEProfile.LoadEmpProfessionalExperienceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvProfessionalExperience.DataSource = dtTab
                        gvProfessionalExperience.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iEmpPEAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    dtTab = objclsEProfile.LoadEmpAsstesLoanDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvAssestsLoan.DataSource = dtTab
                        gvAssestsLoan.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iEmpALAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    If (iEQAttachID > 0 Or iECSAttachID > 0 Or iEmpPEAttachID > 0 Or iEmpALAttachID > 0) Then
                        sEmpDetailsAttachIDs = iEQAttachID & "," & iECSAttachID & "," & iEmpPEAttachID & "," & iEmpALAttachID
                        BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
                    End If

                    'HR Details
                    dtTab = objclsEProfile.LoadEMPAssessment(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvPerformanceAssessments.DataSource = dtTab
                        gvPerformanceAssessments.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iPAAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    dtTab = objclsEProfile.LoadEmpAcademicProgressDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvAcademicProgress.DataSource = dtTab
                        gvAcademicProgress.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iEmpAPAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    dtTab = objclsEProfile.LoadEMPSpecialMention(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvSpecialMentions.DataSource = dtTab
                        gvSpecialMentions.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iSMAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    If (iPAAttachID > 0 Or iSMAttachID > 0 Or iEmpAPAttachID > 0) Then
                        sHRAttachIDs = iPAAttachID & "," & iSMAttachID & "," & iEmpAPAttachID
                        BindAllHRDetailsAttachments(sHRAttachIDs)
                    End If
                    'Articles Details
                    dtTab = objclsEProfile.LoadEmpTransferFirmDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvTransferswithintheFirm.DataSource = dtTab
                        gvTransferswithintheFirm.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iEmpTFAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    dtTab = objclsEProfile.LoadEMPParticularsofArticles(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                    If dtTab.Rows.Count > 0 Then
                        gvParticularsofArticles.DataSource = dtTab
                        gvParticularsofArticles.DataBind()
                        If IsDBNull(dtTab.Rows(0).Item("AttachID")) = False Then
                            iPOAAttachID = dtTab.Rows(0).Item("AttachID")
                        End If
                    End If
                    If (iPOAAttachID > 0 Or iEmpTFAttachID > 0) Then
                        sArticleAttachIDs = iPOAAttachID & "," & iEmpTFAttachID
                        BindAllArticleDetailsAttachments(sArticleAttachIDs)
                    End If
                End If
            Else
                BindExistingEmployeeDB(0, 0, 0, 0, "")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlExistingEmployee_SelectedIndexChanged" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnEmpBasicDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpBasicDetails.Click
        Try
            lblError.Text = ""
            lblTab.Text = 1
            imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = False
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If (sStatus <> "D") Then
                If dt.Rows.Count > 0 Then
                    imgbtnSave.Visible = False : imgbtnUpdate.Visible = True
                Else
                    imgbtnSave.Visible = True : imgbtnUpdate.Visible = False
                End If
            End If
            liEmpBasic.Attributes.Add("class", "active")
            liEmpMaster.Attributes.Remove("class")
            liEmpDetails.Attributes.Remove("class")
            liHRDetails.Attributes.Remove("class")
            liArticleClerck.Attributes.Remove("class")

            divEmpBasic.Attributes.Add("class", "tab-pane active")
            divEmpMaster.Attributes.Add("class", "tab-pane")
            divEmpDetails.Attributes.Add("class", "tab-pane")
            divEmpHRDetails.Attributes.Add("class", "tab-pane")
            divEmpArticleClerck.Attributes.Add("class", "tab-pane")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmpBasicDetails_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnEmpMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpMaster.Click
        Try
            lblTab.Text = 2 : lblError.Text = ""
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            If sStatus = "D" Then
                lblError.Text = "De-Activated."
            ElseIf sStatus = "W" Then
                lblError.Text = "Waiting for Approval."
            End If
            If (sStatus <> "D") Then
                If dtEmpDet.Rows.Count > 0 Then
                    imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = True
                Else
                    imgbtnEmpMasterSave.Visible = True : imgbtnEmpMasterUpdate.Visible = False
                End If
            Else
                imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = False
            End If
            If ddlExistingEmployee.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Existing Employee." : lblError.Text = "Select Existing Employee."
                ddlExistingEmployee.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show'); $('#ddlExistingEmployee').focus();", True)
                lnkbtnEmpBasicDetails_Click(sender, e)
                Exit Sub
            Else
                liEmpBasic.Attributes.Remove("class")
                liEmpMaster.Attributes.Add("class", "active")
                liEmpDetails.Attributes.Remove("class")
                liHRDetails.Attributes.Remove("class")
                liArticleClerck.Attributes.Remove("class")

                divEmpBasic.Attributes.Add("class", "tab-pane")
                divEmpMaster.Attributes.Add("class", "tab-pane active")
                divEmpDetails.Attributes.Add("class", "tab-pane")
                divEmpHRDetails.Attributes.Add("class", "tab-pane")
                divEmpArticleClerck.Attributes.Add("class", "tab-pane")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmpMaster_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnEmpDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnEmpDetails.Click
        Try
            lblTab.Text = 3 : lblError.Text = ""
            If sStatus = "D" Then
                lblError.Text = "De-Activated."
            ElseIf sStatus = "W" Then
                lblError.Text = "Waiting for Approval."
            End If
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = False
            If ddlExistingEmployee.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Existing Employee." : lblError.Text = "Select Existing Employee."
                ddlExistingEmployee.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show'); $('#ddlExistingEmployee').focus();", True)
                lnkbtnEmpBasicDetails_Click(sender, e)
                Exit Sub
            Else
                liEmpBasic.Attributes.Remove("class")
                liEmpMaster.Attributes.Remove("class")
                liEmpDetails.Attributes.Add("class", "active")
                liHRDetails.Attributes.Remove("class")
                liArticleClerck.Attributes.Remove("class")

                divEmpBasic.Attributes.Add("class", "tab-pane")
                divEmpMaster.Attributes.Add("class", "tab-pane")
                divEmpDetails.Attributes.Add("class", "tab-pane active")
                divEmpHRDetails.Attributes.Add("class", "tab-pane")
                divEmpArticleClerck.Attributes.Add("class", "tab-pane")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmpDetails_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnHRDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnHRDetails.Click
        Try
            lblTab.Text = 4 : lblError.Text = ""
            If sStatus = "D" Then
                lblError.Text = "De-Activated."
            ElseIf sStatus = "W" Then
                lblError.Text = "Waiting for Approval."
            End If
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = False
            If ddlExistingEmployee.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Existing Employee." : lblError.Text = "Select Existing Employee."
                ddlExistingEmployee.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show'); $('#ddlExistingEmployee').focus();", True)
                lnkbtnEmpBasicDetails_Click(sender, e)
                Exit Sub
            Else
                liEmpBasic.Attributes.Remove("class")
                liEmpMaster.Attributes.Remove("class")
                liEmpDetails.Attributes.Remove("class")
                liHRDetails.Attributes.Add("class", "active")
                liArticleClerck.Attributes.Remove("class")

                divEmpBasic.Attributes.Add("class", "tab-pane")
                divEmpMaster.Attributes.Add("class", "tab-pane")
                divEmpDetails.Attributes.Add("class", "tab-pane")
                divEmpHRDetails.Attributes.Add("class", "tab-pane active")
                divEmpArticleClerck.Attributes.Add("class", "tab-pane")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHRDetails_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub lnkbtnArticleClerck_Click(sender As Object, e As EventArgs) Handles lnkbtnArticleClerck.Click
        Try
            lblTab.Text = 5 : lblError.Text = ""
            If sStatus = "D" Then
                lblError.Text = "De-Activated."
            ElseIf sStatus = "W" Then
                lblError.Text = "Waiting for Approval."
            End If
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            imgbtnEmpMasterSave.Visible = False : imgbtnEmpMasterUpdate.Visible = False
            If ddlExistingEmployee.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Existing Employee." : lblError.Text = "Select Existing Employee."
                ddlExistingEmployee.Focus()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show'); $('#ddlExistingEmployee').focus();", True)
                lnkbtnEmpBasicDetails_Click(sender, e)
                Exit Sub
            Else
                liEmpBasic.Attributes.Remove("class")
                liEmpMaster.Attributes.Remove("class")
                liEmpDetails.Attributes.Remove("class")
                liHRDetails.Attributes.Remove("class")
                liArticleClerck.Attributes.Add("class", "active")

                divEmpBasic.Attributes.Add("class", "tab-pane")
                divEmpMaster.Attributes.Add("class", "tab-pane")
                divEmpDetails.Attributes.Add("class", "tab-pane")
                divEmpHRDetails.Attributes.Add("class", "tab-pane")
                divEmpArticleClerck.Attributes.Add("class", "tab-pane active")
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnArticleClerck_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvResumeAttach_PreRender(sender As Object, e As EventArgs) Handles gvResumeAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvResumeAttach.Rows.Count > 0 Then
                gvResumeAttach.UseAccessibleHeader = True
                gvResumeAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvResumeAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResumeAttach_PreRender" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub BindAllAttachments(ByVal sAC As String, ByVal iAttachID As Integer)
        Dim ds As New DataSet
        Try
            ds = objclsEProfile.LoadAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iAttachID)
            gvResumeAttach.DataSource = ds
            gvResumeAttach.DataBind()
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub imgbtnAddResume_Click(sender As Object, e As EventArgs) Handles imgbtnAddResume.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Try
            lblError.Text = ""
            If Not (txtfile.PostedFile Is Nothing) And txtfile.PostedFile.ContentLength > 0 Then
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sPaths = txtResumeUploadPath.Text
                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iResumeAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iResumeAttachID > 0 Then
                        BindAllAttachments(sSession.AccessCode, iResumeAttachID)
                    End If
                Else
                    lblError.Text = "No file to Attach."
                End If
            Else
                lblError.Text = "No file to Attach."
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAddResume_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvResumeAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvResumeAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "OPENPAGE" Then
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iResumeDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iResumeAttachID, iResumeDocID)
                DownloadMyFile(sDestFilePath)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvResumeAttach_RowCommand" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
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
            Throw
        End Try
    End Sub
    Private Sub SaveEmployeeDetails()
        Dim dt As New DataTable
        Dim Arr() As String
        Dim lSize As Long
        Dim sPaths As String, sFullFilePath As String, sFilesNames As String, sBloodGroup As String = ""
        Dim dDOBDate As Date
        Dim iGender As Integer = 0, iMaritalStatus As Integer = 0, iNoOfChildren As Integer = 0
        Try
            If ddlExistingEmployee.SelectedIndex > 0 Then
                dt = objclsEProfile.GetFolderNames(sSession.AccessCode, sSession.AccessCodeID)
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If dt.Rows(i).Item("FolderID") = 1 Then
                            If iContactAddressPKID > 0 Then
                                objclsEProfile.iSUAPKID = iContactAddressPKID
                            Else
                                objclsEProfile.iSUAPKID = 0
                            End If
                            objclsEProfile.iSUAUserEmpID = ddlExistingEmployee.SelectedValue
                            objclsEProfile.sSUAContactName = ""
                            objclsEProfile.sSUAAddress1 = objclsGRACeGeneral.SafeSQL(txtCAAddress.Text)
                            If txtCAAddress1.Text <> "" Then
                                objclsEProfile.sSUAAddress2 = objclsGRACeGeneral.SafeSQL(txtCAAddress1.Text)
                            Else
                                objclsEProfile.sSUAAddress2 = ""
                            End If
                            If txtCAAddress2.Text <> "" Then
                                objclsEProfile.sSUAAddress3 = objclsGRACeGeneral.SafeSQL(txtCAAddress2.Text)
                            Else
                                objclsEProfile.sSUAAddress3 = ""
                            End If
                            objclsEProfile.iSUAPincode = Val(txtCAPincode.Text)
                            objclsEProfile.sSUAMobile = objclsGRACeGeneral.SafeSQL(txtCAMobileNo.Text)
                            If txtCATelephoneNo.Text <> "" Then
                                objclsEProfile.sSUATelephone = objclsGRACeGeneral.SafeSQL(txtCATelephoneNo.Text)
                            Else
                                objclsEProfile.sSUATelephone = ""
                            End If
                            objclsEProfile.sSUAEmail = ""
                            objclsEProfile.sSUARelationType = ""
                            objclsEProfile.sSUAIPAddress = sSession.IPAddress
                            objclsEProfile.iSUACompId = sSession.AccessCodeID
                            Arr = objclsEProfile.SaveEmployeeAddressDetails(sSession.AccessCode, objclsEProfile, "CA")
                            iContactAddressPKID = Arr(1)
                        ElseIf dt.Rows(i).Item("FolderID") = 2 Then
                            If iPermanentAddressPKID > 0 Then
                                objclsEProfile.iSUAPKID = iPermanentAddressPKID
                            Else
                                objclsEProfile.iSUAPKID = 0
                            End If
                            objclsEProfile.iSUAUserEmpID = ddlExistingEmployee.SelectedValue
                            objclsEProfile.sSUAContactName = ""
                            objclsEProfile.sSUAAddress1 = objclsGRACeGeneral.SafeSQL(txtPAAddress.Text)
                            If txtPAAddress1.Text <> "" Then
                                objclsEProfile.sSUAAddress2 = objclsGRACeGeneral.SafeSQL(txtPAAddress1.Text)
                            Else
                                objclsEProfile.sSUAAddress2 = ""
                            End If
                            If txtPAAddress2.Text <> "" Then
                                objclsEProfile.sSUAAddress3 = objclsGRACeGeneral.SafeSQL(txtPAAddress2.Text)
                            Else
                                objclsEProfile.sSUAAddress3 = ""
                            End If
                            objclsEProfile.iSUAPincode = Val(txtPAPincode.Text)
                            objclsEProfile.sSUAMobile = objclsGRACeGeneral.SafeSQL(txtPAMobileNo.Text)
                            If txtPATelephoneNo.Text <> "" Then
                                objclsEProfile.sSUATelephone = objclsGRACeGeneral.SafeSQL(txtPATelephoneNo.Text)
                            Else
                                objclsEProfile.sSUATelephone = ""
                            End If
                            objclsEProfile.sSUAEmail = ""
                            objclsEProfile.sSUARelationType = ""
                            objclsEProfile.sSUAIPAddress = sSession.IPAddress
                            objclsEProfile.iSUACompId = sSession.AccessCodeID
                            Arr = objclsEProfile.SaveEmployeeAddressDetails(sSession.AccessCode, objclsEProfile, "PA")
                            iPermanentAddressPKID = Arr(1)
                        ElseIf dt.Rows(i).Item("FolderID") = 3 Then
                            If iEmergencyContactPKID > 0 Then
                                objclsEProfile.iSUAPKID = iEmergencyContactPKID
                            Else
                                objclsEProfile.iSUAPKID = 0
                            End If
                            objclsEProfile.iSUAUserEmpID = ddlExistingEmployee.SelectedValue
                            objclsEProfile.sSUAContactName = objclsGRACeGeneral.SafeSQL(txtECName.Text)
                            objclsEProfile.sSUAAddress1 = objclsGRACeGeneral.SafeSQL(txtECAddress.Text)
                            If txtECAddress1.Text <> "" Then
                                objclsEProfile.sSUAAddress2 = objclsGRACeGeneral.SafeSQL(txtECAddress1.Text)
                            Else
                                objclsEProfile.sSUAAddress2 = ""
                            End If
                            If txtECAddress2.Text <> "" Then
                                objclsEProfile.sSUAAddress3 = objclsGRACeGeneral.SafeSQL(txtECAddress2.Text)
                            Else
                                objclsEProfile.sSUAAddress3 = ""
                            End If
                            objclsEProfile.iSUAPincode = Val(txtECPinCode.Text)
                            objclsEProfile.sSUAMobile = objclsGRACeGeneral.SafeSQL(txtECMobileNo.Text)
                            If txtECTelephoneNo.Text <> "" Then
                                objclsEProfile.sSUATelephone = objclsGRACeGeneral.SafeSQL(txtECTelephoneNo.Text)
                            Else
                                objclsEProfile.sSUATelephone = ""
                            End If
                            If txtECEmailID.Text <> "" Then
                                objclsEProfile.sSUAEmail = objclsGRACeGeneral.SafeSQL(txtECEmailID.Text)
                            Else
                                objclsEProfile.sSUAEmail = ""
                            End If
                            objclsEProfile.sSUARelationType = objclsGRACeGeneral.SafeSQL(txtECRelation.Text)
                            objclsEProfile.sSUAIPAddress = sSession.IPAddress
                            objclsEProfile.iSUACompId = sSession.AccessCodeID
                            Arr = objclsEProfile.SaveEmployeeAddressDetails(sSession.AccessCode, objclsEProfile, "EC")
                            iEmergencyContactPKID = Arr(1)
                        ElseIf dt.Rows(i).Item("FolderID") = 4 Then
                            If iFMVContactPKID > 0 Then
                                objclsEProfile.iSUAPKID = iFMVContactPKID
                            Else
                                objclsEProfile.iSUAPKID = 0
                            End If
                            objclsEProfile.iSUAUserEmpID = ddlExistingEmployee.SelectedValue
                            objclsEProfile.sSUAContactName = objclsGRACeGeneral.SafeSQL(txtMVName.Text)
                            objclsEProfile.sSUAAddress1 = objclsGRACeGeneral.SafeSQL(txtMVAddress.Text)
                            If txtMVAddress1.Text <> "" Then
                                objclsEProfile.sSUAAddress2 = objclsGRACeGeneral.SafeSQL(txtMVAddress1.Text)
                            Else
                                objclsEProfile.sSUAAddress2 = ""
                            End If
                            If txtMVAddress2.Text <> "" Then
                                objclsEProfile.sSUAAddress3 = objclsGRACeGeneral.SafeSQL(txtMVAddress2.Text)
                            Else
                                objclsEProfile.sSUAAddress3 = ""
                            End If
                            objclsEProfile.iSUAPincode = Val(txtMVPinCode.Text)
                            objclsEProfile.sSUAMobile = objclsGRACeGeneral.SafeSQL(txtMVMobileNo.Text)
                            If txtMVTelephoneNo.Text <> "" Then
                                objclsEProfile.sSUATelephone = objclsGRACeGeneral.SafeSQL(txtMVTelephoneNo.Text)
                            Else
                                objclsEProfile.sSUATelephone = ""
                            End If
                            If txtMVEmailID.Text <> "" Then
                                objclsEProfile.sSUAEmail = objclsGRACeGeneral.SafeSQL(txtMVEmailID.Text)
                            Else
                                objclsEProfile.sSUAEmail = ""
                            End If
                            If ddlMVEmailSelection.SelectedIndex > 1 Then
                                objclsEProfile.sSUARelationType = ddlMVEmailSelection.SelectedValue
                            Else
                                objclsEProfile.sSUARelationType = 1
                            End If
                            objclsEProfile.sSUAIPAddress = sSession.IPAddress
                            objclsEProfile.iSUACompId = sSession.AccessCodeID
                            Arr = objclsEProfile.SaveEmployeeAddressDetails(sSession.AccessCode, objclsEProfile, "FMWC")
                            iFMVContactPKID = Arr(1)
                        End If
                    Next
                End If
                dDOBDate = Date.ParseExact(Trim(txtDOB.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If ddlBloodGroup.SelectedIndex > 0 Then
                    sBloodGroup = ddlBloodGroup.SelectedItem.Text
                Else
                    sBloodGroup = ""
                End If
                If rboMale.Checked = True Then
                    iGender = 1
                Else
                    iGender = 2
                End If
                If rboSingle.Checked = True Then
                    iMaritalStatus = 1
                Else
                    iMaritalStatus = 2
                End If
                iNoOfChildren = Val(txtChildrenCount.Text)
                If Not (fuSignatureUpload.PostedFile Is Nothing) And fuSignatureUpload.PostedFile.ContentLength > 0 Then
                    lSize = CType(fuSignatureUpload.PostedFile.ContentLength, Integer)
                    If (sSession.FileSize * 1024 * 1024) < lSize Then
                        lblError.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                        Exit Sub
                    End If
                    sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                    If sPaths.EndsWith("\") = True Then
                        sPaths = sPaths & "Uploads\"
                    Else
                        sPaths = sPaths & "\Uploads\"
                    End If
                    objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                    objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                    sFilesNames = System.IO.Path.GetFileName(fuSignatureUpload.PostedFile.FileName)
                    sFullFilePath = sPaths & sFilesNames
                    fuSignatureUpload.PostedFile.SaveAs(sFullFilePath)
                    If System.IO.File.Exists(sFullFilePath) = True Then
                        iSignatureAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                        If iSignatureAttachID > 0 Then
                            BindAllAttachments(sSession.AccessCode, iSignatureAttachID)
                        End If
                    End If
                End If
                If Not (fuPhotoUpload.PostedFile Is Nothing) And fuPhotoUpload.PostedFile.ContentLength > 0 Then
                    lSize = CType(fuPhotoUpload.PostedFile.ContentLength, Integer)
                    If (sSession.FileSize * 1024 * 1024) < lSize Then
                        lblError.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                        Exit Sub
                    End If
                    sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                    If sPaths.EndsWith("\") = True Then
                        sPaths = sPaths & "Uploads\"
                    Else
                        sPaths = sPaths & "\Uploads\"
                    End If
                    objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                    objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                    sFilesNames = System.IO.Path.GetFileName(fuPhotoUpload.PostedFile.FileName)
                    sFullFilePath = sPaths & sFilesNames
                    fuPhotoUpload.PostedFile.SaveAs(sFullFilePath)
                    If System.IO.File.Exists(sFullFilePath) = True Then
                        iPhotoAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                        If iPhotoAttachID > 0 Then
                            BindAllAttachments(sSession.AccessCode, iPhotoAttachID)
                        End If
                    End If
                End If
                If iPhotoAttachID = 0 Then
                    iPhotoAttachID = objclsEmployeeMaster.GetPhotoSignatureID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, "Photo")
                End If
                If iSignatureAttachID = 0 Then
                    iSignatureAttachID = objclsEmployeeMaster.GetPhotoSignatureID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, "Signature")
                End If
                objclsEmployeeMaster.iusrPhoto = iPhotoAttachID
                objclsEmployeeMaster.iusrSignature = iSignatureAttachID
                objclsEmployeeMaster.iusrCurWrkAddId = iContactAddressPKID
                objclsEmployeeMaster.iusrPermAddId = iPermanentAddressPKID
                objclsEmployeeMaster.iusrResAddId = iEmergencyContactPKID
                objclsEmployeeMaster.iusrOfficialAddId = iFMVContactPKID
                objclsEmployeeMaster.UpdateEmployeeDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, dDOBDate,
                                                           sBloodGroup, iGender, iMaritalStatus, iNoOfChildren, iResumeAttachID, iPhotoAttachID, iSignatureAttachID, iContactAddressPKID,
                                                            iPermanentAddressPKID, iEmergencyContactPKID, iFMVContactPKID)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub imgbtnEmpMasterSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEmpMasterSave.Click
        Dim dDate As Date, dSDate As Date

        Try
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim l As Integer
            l = DateDiff(DateInterval.Day, dDate, dSDate)
            If l > 0 Then
                lblEmpProfileValidationMsg.Text = "Date of Birth should be less than or equal to Current Date."
                lblError.Text = "Date of Birth should be less than or equal to Current Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show'); $('#txtDescription').focus();", True)
                txtDOB.Focus()
                Exit Sub
            End If
            SaveEmployeeDetails()
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lnkbtnEmpMaster_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile", "Saved", ddlExistingEmployee.SelectedValue, ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            lblEmpProfileValidationMsg.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpProfileValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEmpMasterSave_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub imgbtnEmpMasterUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnEmpMasterUpdate.Click
        Dim dDate As Date, dSDate As Date

        Try
            dDate = Date.ParseExact(objclsGeneralFunctions.GetCurrentDate(sSession.AccessCode), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dSDate = Date.ParseExact(txtDOB.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            Dim l As Integer
            l = DateDiff(DateInterval.Day, dDate, dSDate)
            If l > 0 Then
                lblEmpProfileValidationMsg.Text = "Date of Birth should be less than or equal to Current Date."
                lblError.Text = "Date of Birth should be less than or equal to Current Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show'); $('#txtDescription').focus();", True)
                txtDOB.Focus()
                Exit Sub
            End If
            SaveEmployeeDetails()
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lnkbtnEmpMaster_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile", "Updated", ddlExistingEmployee.SelectedValue, ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            lblEmpProfileValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpProfileValidation').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEmpMasterUpdate_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Education
    Private Sub btnEQNew_Click(sender As Object, e As EventArgs) Handles btnEQNew.Click
        Try
            lblError.Text = "" : lblEmpQualificationModelError.Text = "" : txtEQEducation.Text = "" : txtEQBoard.Text = "" : txtEQSchool.Text = ""
            txtEQYear.Text = "" : txtEQMarks.Text = "" : txtEQRemarks.Text = "" : btnEQSave.Visible = False : btnEQUpdate.Visible = False
            If (sStatus <> "D") Then
                btnEQSave.Visible = True
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpQualificationModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEQNew_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnEQSave_Click(sender As Object, e As EventArgs) Handles btnEQSave.Click
        Dim objQualification As New strUserEmp_QualificationDetails
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtEQEducation.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter Education."
                    txtEQEducation.Focus()
                    Exit Try
                End If
                If txtEQBoard.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter University/Board."
                    txtEQBoard.Focus()
                    Exit Try
                End If
                If txtEQSchool.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter School/College."
                    txtEQSchool.Focus()
                    Exit Try
                End If
                If txtEQYear.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter Year."
                    txtEQYear.Focus()
                    Exit Try
                End If
                If txtEQMarks.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter Marks."
                    txtEQMarks.Focus()
                    Exit Try
                End If

                If txtEQEducation.Text.Length > 1000 Then
                    lblEmpQualificationModelError.Text = "Education exceeded maximum size(max 1000 characters)."
                    txtEQEducation.Focus()
                    Exit Try
                End If
                If txtEQBoard.Text.Length > 1000 Then
                    lblEmpQualificationModelError.Text = "University/Board exceeded maximum size(max 1000 characters)."
                    txtEQBoard.Focus()
                    Exit Try
                End If
                If txtEQSchool.Text.Length > 1000 Then
                    lblEmpQualificationModelError.Text = "School/College exceeded maximum size(max 1000 characters)."
                    txtEQSchool.Focus()
                    Exit Try
                End If
                If IsNumeric(txtEQYear.Text) = False Then
                    lblEmpQualificationModelError.Text = "Enter valid Year(Only Number)."
                    txtEQYear.Focus()
                    Exit Try
                End If
                If IsNumeric(txtEQMarks.Text) = False Then
                    lblEmpQualificationModelError.Text = "Enter valid Marks percentage(Only Number)."
                    txtEQMarks.Focus()
                    Exit Try
                End If
                If Val(txtEQMarks.Text) > 100 Then
                    lblEmpQualificationModelError.Text = "Marks percentage should be less than or equal to 100."
                    txtEQMarks.Focus()
                    Exit Try
                End If
                If txtEQRemarks.Text.Length > 8000 Then
                    lblEmpQualificationModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    txtEQRemarks.Focus()
                    Exit Try
                End If
                objQualification.iSUQ_PKID = 0
                objQualification.iSUQ_UserEmpID = ddlExistingEmployee.SelectedValue
                objQualification.sSUQ_Education = objclsGRACeGeneral.SafeSQL(txtEQEducation.Text.Trim())
                objQualification.sSUQ_University = objclsGRACeGeneral.SafeSQL(txtEQBoard.Text.Trim())
                objQualification.sSUQ_School = objclsGRACeGeneral.SafeSQL(txtEQSchool.Text.Trim())
                objQualification.iSUQ_Year = txtEQYear.Text.Trim()
                objQualification.dSUQ_Marks = txtEQMarks.Text.Trim()
                objQualification.sSUQ_Remarks = objclsGRACeGeneral.SafeSQL(txtEQRemarks.Text.Trim())
                objQualification.iSUQ_AttachID = 0
                objQualification.iSUQ_CrBy = sSession.UserID
                objQualification.iSUQ_UpdatedBy = sSession.UserID
                objQualification.sSUQ_IPAddress = sSession.IPAddress
                objQualification.iSUQ_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpQualificationDetails(sSession.AccessCode, objQualification)
                If iEQAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEQAttachID, "EQ")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblEQPKID.Text = Arr(1)
                lnkbtnEmpDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Qualification)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpQualificationModelError.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpQualificationModal').modal('show');", True)
                btnEQSave.Visible = False : btnEQUpdate.Visible = True
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpQualificationModal').modal('show');", True)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEQSave_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnEQUpdate_Click(sender As Object, e As EventArgs) Handles btnEQUpdate.Click
        Dim objQualification As New strUserEmp_QualificationDetails
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtEQEducation.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter Education."
                    txtEQEducation.Focus()
                    Exit Try
                End If
                If txtEQBoard.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter University/Board."
                    txtEQBoard.Focus()
                    Exit Try
                End If
                If txtEQSchool.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter School/College."
                    txtEQSchool.Focus()
                    Exit Try
                End If
                If txtEQYear.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter Year."
                    txtEQYear.Focus()
                    Exit Try
                End If
                If txtEQMarks.Text = "" Then
                    lblEmpQualificationModelError.Text = "Enter Marks."
                    txtEQMarks.Focus()
                    Exit Try
                End If

                If txtEQEducation.Text.Length > 1000 Then
                    lblEmpQualificationModelError.Text = "Education exceeded maximum size(max 1000 characters)."
                    txtEQEducation.Focus()
                    Exit Try
                End If
                If txtEQBoard.Text.Length > 1000 Then
                    lblEmpQualificationModelError.Text = "University/Board exceeded maximum size(max 1000 characters)."
                    txtEQBoard.Focus()
                    Exit Try
                End If
                If txtEQSchool.Text.Length > 1000 Then
                    lblEmpQualificationModelError.Text = "School/College exceeded maximum size(max 1000 characters)."
                    txtEQSchool.Focus()
                    Exit Try
                End If
                If IsNumeric(txtEQYear.Text) = False Then
                    lblEmpQualificationModelError.Text = "Enter valid Year(Only Number)."
                    txtEQYear.Focus()
                    Exit Try
                End If
                If IsNumeric(txtEQMarks.Text) = False Then
                    lblEmpQualificationModelError.Text = "Enter valid Marks percentage(Only Number)."
                    txtEQMarks.Focus()
                    Exit Try
                End If
                If Val(txtEQMarks.Text) > 100 Then
                    lblEmpQualificationModelError.Text = "Marks percentage should be less than or equal to 100."
                    txtEQMarks.Focus()
                    Exit Try
                End If
                If txtEQRemarks.Text.Length > 8000 Then
                    lblEmpQualificationModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    txtEQRemarks.Focus()
                    Exit Try
                End If
                objQualification.iSUQ_PKID = Val(lblEQPKID.Text)
                objQualification.iSUQ_UserEmpID = ddlExistingEmployee.SelectedValue
                objQualification.sSUQ_Education = objclsGRACeGeneral.SafeSQL(txtEQEducation.Text.Trim())
                objQualification.sSUQ_University = objclsGRACeGeneral.SafeSQL(txtEQBoard.Text.Trim())
                objQualification.sSUQ_School = objclsGRACeGeneral.SafeSQL(txtEQSchool.Text.Trim())
                objQualification.iSUQ_Year = txtEQYear.Text.Trim()
                objQualification.dSUQ_Marks = txtEQMarks.Text.Trim()
                objQualification.sSUQ_Remarks = objclsGRACeGeneral.SafeSQL(txtEQRemarks.Text.Trim())
                objQualification.iSUQ_AttachID = 0
                objQualification.iSUQ_CrBy = sSession.UserID
                objQualification.iSUQ_UpdatedBy = sSession.UserID
                objQualification.sSUQ_IPAddress = sSession.IPAddress
                objQualification.iSUQ_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpQualificationDetails(sSession.AccessCode, objQualification)
                If iEQAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEQAttachID, "EQ")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblEQPKID.Text = Arr(1)
                lnkbtnEmpDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Qualification)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpQualificationModelError.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpQualificationModal').modal('show');", True)
            End If
        Catch ex As Exception
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpQualificationModal').modal('show');", True)
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEQUpdate_Click" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmpQualification_PreRender(sender As Object, e As EventArgs) Handles gvEmpQualification.PreRender
        Dim dt As New DataTable
        Try
            If gvEmpQualification.Rows.Count > 0 Then
                gvEmpQualification.UseAccessibleHeader = True
                gvEmpQualification.HeaderRow.TableSection = TableRowSection.TableHeader
                gvEmpQualification.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmpQualification_PreRender" & "Error_Line ='" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmpQualification_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEmpQualification.RowCommand
        Dim lblID As New Label, lblUserID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblUserID = DirectCast(clickedRow.FindControl("lblUserID"), Label)
                If lblID.Text > 0 And lblUserID.Text > 0 Then
                    dt = objclsEProfile.LoadEMPQualification(sSession.AccessCode, sSession.AccessCodeID, lblUserID.Text, lblID.Text)
                    If dt.Rows.Count > 0 Then
                        lblEQPKID.Text = lblID.Text
                        If IsDBNull(dt.Rows(0)("EmpEducation")) = False Then
                            txtEQEducation.Text = dt.Rows(0).Item("EmpEducation")
                        End If
                        If IsDBNull(dt.Rows(0)("EmpUniversity")) = False Then
                            txtEQBoard.Text = dt.Rows(0).Item("EmpUniversity")
                        End If
                        If IsDBNull(dt.Rows(0)("EmpCollege")) = False Then
                            txtEQSchool.Text = dt.Rows(0).Item("EmpCollege")
                        End If
                        If IsDBNull(dt.Rows(0)("EmpYear")) = False Then
                            txtEQYear.Text = dt.Rows(0).Item("EmpYear")
                        End If
                        If IsDBNull(dt.Rows(0)("EmpMarks")) = False Then
                            txtEQMarks.Text = dt.Rows(0).Item("EmpMarks")
                        End If
                        If IsDBNull(dt.Rows(0)("EmpRemarks")) = False Then
                            txtEQRemarks.Text = dt.Rows(0).Item("EmpRemarks")
                        End If
                        btnEQSave.Visible = False : btnEQUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnEQUpdate.Visible = True
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpQualificationModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmpQualification_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnEQCancel_Click(sender As Object, e As EventArgs) Handles btnEQCancel.Click
        Try
            lblError.Text = "" : lblEmpQualificationModelError.Text = "" : txtEQEducation.Text = "" : txtEQBoard.Text = "" : txtEQSchool.Text = ""
            txtEQYear.Text = "" : txtEQMarks.Text = "" : txtEQRemarks.Text = "" : btnEQSave.Visible = False : btnEQUpdate.Visible = False
            If (sStatus <> "D") Then
                btnEQSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEQCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Course
    Private Sub btnECSNew_Click(sender As Object, e As EventArgs) Handles btnECSNew.Click
        Try
            lblError.Text = "" : lblEmpCourseModelError.Text = "" : txtECSDate.Text = "" : txtSubject.Text = "" : txtECSFPEmployer.Text = "" : txtECSFPEmployee.Text = ""
            txtECSConductedBy.Text = "" : txtECSCPEPoints.Text = "" : txtECSPapers.Text = "" : txtECSBriefDesc.Text = ""
            txtECSFeedBack.Text = "" : txtECSRemarks.Text = "" : btnECSSave.Visible = False : btnECSUpdate.Visible = False
            If (sStatus <> "D") Then
                btnECSSave.Visible = True
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnECSNew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnECSCancel_Click(sender As Object, e As EventArgs) Handles btnECSCancel.Click
        Try
            lblError.Text = "" : lblEmpCourseModelError.Text = "" : txtECSDate.Text = "" : txtSubject.Text = "" : txtECSFPEmployer.Text = "" : txtECSFPEmployee.Text = ""
            txtECSConductedBy.Text = "" : txtECSCPEPoints.Text = "" : txtECSPapers.Text = "" : txtECSBriefDesc.Text = ""
            txtECSFeedBack.Text = "" : txtECSRemarks.Text = "" : btnECSSave.Visible = False : btnECSUpdate.Visible = False
            If (sStatus <> "D") Then
                btnECSSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnECSCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnECSSave_Click(sender As Object, e As EventArgs) Handles btnECSSave.Click
        Dim objCourse As New strUserEmp_CourseDetails
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtECSDate.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSDate.Focus()
                    Exit Sub
                End If
                If txtSubject.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Subject."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtSubject.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployer.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Fees Paid Employer."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployer.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployee.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Fees Paid Employee."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployee.Focus()
                    Exit Sub
                End If
                If txtECSConductedBy.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Conducted By."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSConductedBy.Focus()
                    Exit Sub
                End If
                If txtECSCPEPoints.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter CPE Points."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSCPEPoints.Focus()
                    Exit Sub
                End If
                If txtECSPapers.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Papers Presented."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSPapers.Focus()
                    Exit Sub
                End If
                If txtECSBriefDesc.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Brief Description."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSBriefDesc.Focus()
                    Exit Sub
                End If
                If txtECSFeedBack.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter FeedBack Report Reference."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFeedBack.Focus()
                    Exit Sub
                End If
                If txtSubject.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Subject exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtSubject.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployer.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Fees Paid Employer exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployer.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployee.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Fees Paid Employee exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployee.Focus()
                    Exit Sub
                End If
                If txtECSConductedBy.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Conducted By exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSConductedBy.Focus()
                    Exit Sub
                End If
                If txtECSCPEPoints.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "CPE Points exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSCPEPoints.Focus()
                    Exit Sub
                End If
                If txtECSPapers.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "Papers Presented exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSPapers.Focus()
                    Exit Sub
                End If
                If txtECSBriefDesc.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "Brief Description exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSBriefDesc.Focus()
                    Exit Sub
                End If
                If txtECSFeedBack.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "FeedBack Report Reference exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFeedBack.Focus()
                    Exit Sub
                End If
                If txtECSRemarks.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSRemarks.Focus()
                    Exit Sub
                End If
                objCourse.iSUC_PKID = 0
                objCourse.iSUC_UserEmpID = ddlExistingEmployee.SelectedValue
                objCourse.dSUC_Date = Date.ParseExact(Trim(txtECSDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objCourse.sSUC_Subject = objclsGRACeGeneral.SafeSQL(txtSubject.Text.Trim())
                objCourse.sSUC_FeeEmployer = objclsGRACeGeneral.SafeSQL(txtECSFPEmployer.Text.Trim())
                objCourse.sSUC_FeeEmployee = objclsGRACeGeneral.SafeSQL(txtECSFPEmployee.Text.Trim())
                objCourse.sSUC_ConductedBy = objclsGRACeGeneral.SafeSQL(txtECSConductedBy.Text.Trim())
                objCourse.sSUC_CPEPoints = objclsGRACeGeneral.SafeSQL(txtECSCPEPoints.Text.Trim())
                objCourse.sSUC_Papers = objclsGRACeGeneral.SafeSQL(txtECSPapers.Text.Trim())
                objCourse.sSUC_BriefDescription = objclsGRACeGeneral.SafeSQL(txtECSBriefDesc.Text.Trim())
                objCourse.sSUC_FeedBack = objclsGRACeGeneral.SafeSQL(txtECSFeedBack.Text.Trim())
                objCourse.sSUC_Remarks = objclsGRACeGeneral.SafeSQL(txtECSRemarks.Text.Trim())
                objCourse.iSUC_AttachID = 0
                objCourse.iSUC_CrBy = sSession.UserID
                objCourse.iSUC_UpdatedBy = sSession.UserID
                objCourse.sSUC_IPAddress = sSession.IPAddress
                objCourse.iSUC_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpCourseDetails(sSession.AccessCode, objCourse)
                If iECSAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iECSAttachID, "ECS")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblECSPKID.Text = Arr(1)
                lnkbtnEmpDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Conferences/Courses)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpCourseModelError.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpCourseModal').modal('show');", True)
                btnECSSave.Visible = False : btnECSUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnECSSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnECSUpdate_Click(sender As Object, e As EventArgs) Handles btnECSUpdate.Click
        Dim objCourse As New strUserEmp_CourseDetails
        Dim Arr() As String
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtECSDate.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSDate.Focus()
                    Exit Sub
                End If
                If txtSubject.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Subject."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtSubject.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployer.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Fees Paid Employer."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployer.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployee.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Fees Paid Employee."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployee.Focus()
                    Exit Sub
                End If
                If txtECSConductedBy.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Conducted By."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSConductedBy.Focus()
                    Exit Sub
                End If
                If txtECSCPEPoints.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter CPE Points."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSCPEPoints.Focus()
                    Exit Sub
                End If
                If txtECSPapers.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Papers Presented."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSPapers.Focus()
                    Exit Sub
                End If
                If txtECSBriefDesc.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter Brief Description."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSBriefDesc.Focus()
                    Exit Sub
                End If
                If txtECSFeedBack.Text = "" Then
                    lblEmpCourseModelError.Text = "Enter FeedBack Report Reference."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFeedBack.Focus()
                    Exit Sub
                End If
                If txtSubject.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Subject exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtSubject.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployer.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Fees Paid Employer exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployer.Focus()
                    Exit Sub
                End If
                If txtECSFPEmployee.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Fees Paid Employee exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFPEmployee.Focus()
                    Exit Sub
                End If
                If txtECSConductedBy.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "Conducted By exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSConductedBy.Focus()
                    Exit Sub
                End If
                If txtECSCPEPoints.Text.Length > 1000 Then
                    lblEmpCourseModelError.Text = "CPE Points exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSCPEPoints.Focus()
                    Exit Sub
                End If
                If txtECSPapers.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "Papers Presented exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSPapers.Focus()
                    Exit Sub
                End If
                If txtECSBriefDesc.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "Brief Description exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSBriefDesc.Focus()
                    Exit Sub
                End If
                If txtECSFeedBack.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "FeedBack Report Reference exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSFeedBack.Focus()
                    Exit Sub
                End If
                If txtECSRemarks.Text.Length > 8000 Then
                    lblEmpCourseModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                    txtECSRemarks.Focus()
                    Exit Sub
                End If
                objCourse.iSUC_PKID = Val(lblECSPKID.Text)
                objCourse.iSUC_UserEmpID = ddlExistingEmployee.SelectedValue
                objCourse.dSUC_Date = Date.ParseExact(Trim(txtECSDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objCourse.sSUC_Subject = objclsGRACeGeneral.SafeSQL(txtSubject.Text.Trim())
                objCourse.sSUC_FeeEmployer = objclsGRACeGeneral.SafeSQL(txtECSFPEmployer.Text.Trim())
                objCourse.sSUC_FeeEmployee = objclsGRACeGeneral.SafeSQL(txtECSFPEmployee.Text.Trim())
                objCourse.sSUC_ConductedBy = objclsGRACeGeneral.SafeSQL(txtECSConductedBy.Text.Trim())
                objCourse.sSUC_CPEPoints = objclsGRACeGeneral.SafeSQL(txtECSCPEPoints.Text.Trim())
                objCourse.sSUC_Papers = objclsGRACeGeneral.SafeSQL(txtECSPapers.Text.Trim())
                objCourse.sSUC_BriefDescription = objclsGRACeGeneral.SafeSQL(txtECSBriefDesc.Text.Trim())
                objCourse.sSUC_FeedBack = objclsGRACeGeneral.SafeSQL(txtECSFeedBack.Text.Trim())
                objCourse.sSUC_Remarks = objclsGRACeGeneral.SafeSQL(txtECSRemarks.Text.Trim())
                objCourse.iSUC_AttachID = 0
                objCourse.iSUC_CrBy = sSession.UserID
                objCourse.iSUC_UpdatedBy = sSession.UserID
                objCourse.sSUC_IPAddress = sSession.IPAddress
                objCourse.iSUC_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpCourseDetails(sSession.AccessCode, objCourse)
                If iECSAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iECSAttachID, "ECS")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblECSPKID.Text = Arr(1)
                lnkbtnEmpDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Conferences/Courses)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpCourseModelError.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpCourseModal').modal('show');", True)
                btnECSSave.Visible = False : btnECSUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnECSUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvCourse_PreRender(sender As Object, e As EventArgs) Handles gvCourse.PreRender
        Dim dt As New DataTable
        Try
            If gvCourse.Rows.Count > 0 Then
                gvCourse.UseAccessibleHeader = True
                gvCourse.HeaderRow.TableSection = TableRowSection.TableHeader
                gvCourse.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvCourse_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvCourse_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvCourse.RowCommand
        Dim lblID As New Label, lblUserID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblUserID = DirectCast(clickedRow.FindControl("lblUserID"), Label)
                If lblID.Text > 0 And lblUserID.Text > 0 Then
                    dt = objclsEProfile.LoadEMPCourse(sSession.AccessCode, sSession.AccessCodeID, lblUserID.Text, lblID.Text)
                    If dt.Rows.Count > 0 Then
                        lblECSPKID.Text = lblID.Text
                        If IsDBNull(dt.Rows(0)("ECSSubject")) = False Then
                            txtSubject.Text = dt.Rows(0).Item("ECSSubject")
                        End If
                        If IsDBNull(dt.Rows(0)("ECSDate")) = False Then
                            txtECSDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("ECSDate"), "D")
                        End If
                        If IsDBNull(dt.Rows(0)("ECSDescription")) = False Then
                            txtECSBriefDesc.Text = dt.Rows(0).Item("ECSDescription")
                        End If
                        If IsDBNull(dt.Rows(0)("PapersPresented")) = False Then
                            txtECSPapers.Text = dt.Rows(0).Item("PapersPresented")
                        End If
                        If IsDBNull(dt.Rows(0)("ConductedBy")) = False Then
                            txtECSConductedBy.Text = dt.Rows(0).Item("ConductedBy")
                        End If
                        If IsDBNull(dt.Rows(0)("FeesPaidEmployer")) = False Then
                            txtECSFPEmployer.Text = dt.Rows(0).Item("FeesPaidEmployer")
                        End If
                        If IsDBNull(dt.Rows(0)("FeesPaidEmployee")) = False Then
                            txtECSFPEmployee.Text = dt.Rows(0).Item("FeesPaidEmployee")
                        End If
                        If IsDBNull(dt.Rows(0)("CPEPoints")) = False Then
                            txtECSCPEPoints.Text = dt.Rows(0).Item("CPEPoints")
                        End If
                        If IsDBNull(dt.Rows(0)("FeedBack")) = False Then
                            txtECSFeedBack.Text = dt.Rows(0).Item("FeedBack")
                        End If
                        If IsDBNull(dt.Rows(0)("Remarks")) = False Then
                            txtECSRemarks.Text = dt.Rows(0).Item("Remarks")
                        End If
                        btnECSSave.Visible = False : btnECSUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnECSUpdate.Visible = True
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpCourseModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgCourse_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Professional Experiance
    Private Sub gvProfessionalExperience_PreRender(sender As Object, e As EventArgs) Handles gvProfessionalExperience.PreRender
        Dim dt As New DataTable
        Try
            If gvProfessionalExperience.Rows.Count > 0 Then
                gvProfessionalExperience.UseAccessibleHeader = True
                gvProfessionalExperience.HeaderRow.TableSection = TableRowSection.TableHeader
                gvProfessionalExperience.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvProfessionalExperience_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvProfessionalExperience_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvProfessionalExperience.RowCommand
        Dim dtProfExpDetails As New DataTable
        Try
            lblError.Text = "" : lblEmpProfessionalExperienceModelError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblPEPKID = DirectCast(clickedRow.FindControl("lblPEPKID"), Label)
                If ddlExistingEmployee.SelectedIndex > 0 Then
                    dtProfExpDetails = objclsEProfile.LoadEmpProfessionalExperienceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, lblPEPKID.Text)
                    If dtProfExpDetails.Rows.Count > 0 Then
                        btnEPESave.Visible = False : btnEPEUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnEPEUpdate.Visible = True
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("Assignment")) = False Then
                            txtEPEAssignment.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("Assignment").ToString())
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("ReportingTo")) = False Then
                            txtEPEReportingTo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("ReportingTo").ToString())
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("From")) = False Then
                            txtEPEFrom.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("From").ToString())
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("To")) = False Then
                            txtEPETo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("To").ToString())
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("SalaryPerAnnum")) = False Then
                            txtEPESalaryPerAnnum.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("SalaryPerAnnum").ToString())
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("PositionHeld")) = False Then
                            txtEPEPositionHeld.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("PositionHeld").ToString())
                        End If
                        If IsDBNull(dtProfExpDetails.Rows(0).Item("Remarks")) = False Then
                            txtEPERemarks.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtProfExpDetails.Rows(0).Item("Remarks").ToString())
                        End If

                        gvProfessionalExperience.DataSource = dtProfExpDetails
                        gvProfessionalExperience.DataBind()
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "dgProfessionalExperience_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnEPENew_Click(sender As Object, e As EventArgs) Handles btnEPENew.Click
        Try
            btnEPESave.Visible = False : btnEPEUpdate.Visible = False
            If (sStatus <> "D") Then
                btnEPESave.Visible = True
            End If
            lblEmpProfessionalExperienceModelError.Text = "" : txtEPEAssignment.Text = "" : txtEPEReportingTo.Text = "" : txtEPEFrom.Text = ""
            txtEPETo.Text = "" : txtEPESalaryPerAnnum.Text = "" : txtEPEPositionHeld.Text = "" : txtEPERemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
        Catch ex As Exception
            lblEmpProfessionalExperienceModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEPENew_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnEPECancel_Click(sender As Object, e As EventArgs) Handles btnEPECancel.Click
        Try
            btnEPESave.Visible = False : btnEPEUpdate.Visible = False
            If (sStatus <> "D") Then
                btnEPESave.Visible = True
            End If
            lblEmpProfessionalExperienceModelError.Text = "" : txtEPEAssignment.Text = "" : txtEPEReportingTo.Text = "" : txtEPEFrom.Text = ""
            txtEPETo.Text = "" : txtEPESalaryPerAnnum.Text = "" : txtEPEPositionHeld.Text = "" : txtEPERemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('hide');", True)
        Catch ex As Exception
            lblEmpProfessionalExperienceModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEPECancel_Click" & "Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnEPESave_Click(sender As Object, e As EventArgs) Handles btnEPESave.Click
        Dim Arr() As String
        Try
            lblError.Text = "" : lblEmpProfessionalExperienceModelError.Text = ""
            If txtEPEAssignment.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Assignment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEAssignment.Focus()
                Exit Sub
            End If
            If txtEPEAssignment.Text.Trim.Length > 500 Then
                lblEmpProfessionalExperienceModelError.Text = "Assignment exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEAssignment.Focus()
                Exit Sub
            End If
            If txtEPEReportingTo.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Reporting To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEReportingTo.Focus()
                Exit Sub
            End If
            If txtEPEReportingTo.Text.Trim.Length > 50 Then
                lblEmpProfessionalExperienceModelError.Text = "Assignment exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEReportingTo.Focus()
                Exit Sub
            End If
            If txtEPEFrom.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter From."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEFrom.Focus()
                Exit Sub
            End If
            If txtEPEFrom.Text.Trim.Length > 10 Then
                lblEmpProfessionalExperienceModelError.Text = "From exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEFrom.Focus()
                Exit Sub
            End If
            If txtEPETo.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPETo.Focus()
                Exit Sub
            End If
            If txtEPETo.Text.Trim.Length > 10 Then
                lblEmpProfessionalExperienceModelError.Text = "To exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPETo.Focus()
                Exit Sub
            End If
            If txtEPESalaryPerAnnum.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Salary Per Annum."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPESalaryPerAnnum.Focus()
                Exit Sub
            End If
            If txtEPEPositionHeld.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Position Held."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEPositionHeld.Focus()
                Exit Sub
            End If
            If txtEPEPositionHeld.Text.Trim.Length > 20 Then
                lblEmpProfessionalExperienceModelError.Text = "Position Held exceeded maximum size(max 20 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEPositionHeld.Focus()
                Exit Sub
            End If
            If txtEPERemarks.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Remarks."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPERemarks.Focus()
                Exit Sub
            End If
            If txtEPERemarks.Text.Trim.Length > 8000 Then
                lblEmpProfessionalExperienceModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPERemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUPPKID = 0
            objclsEProfile.iSUPUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.sSUPAssignment = objclsGRACeGeneral.SafeSQL(txtEPEAssignment.Text)
            objclsEProfile.sSUPReportingTo = objclsGRACeGeneral.SafeSQL(txtEPEReportingTo.Text)
            objclsEProfile.iSUPFrom = Val(txtEPEFrom.Text)
            objclsEProfile.iSUPTo = Val(txtEPETo.Text)
            objclsEProfile.dSUPSalaryPerAnnum = Val(txtEPESalaryPerAnnum.Text)
            objclsEProfile.sSUPPosition = objclsGRACeGeneral.SafeSQL(txtEPEPositionHeld.Text)
            objclsEProfile.sSUPRemarks = objclsGRACeGeneral.SafeSQL(txtEPERemarks.Text)
            objclsEProfile.iSUPAttachID = iEmpPEAttachID
            objclsEProfile.iSUPCrBy = sSession.UserID
            objclsEProfile.iSUPUpdatedBy = sSession.UserID
            objclsEProfile.sSUPIPAddress = sSession.IPAddress
            objclsEProfile.iSUPCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpProfessionalExperienceDetails(sSession.AccessCode, objclsEProfile)
            If iEmpPEAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpPEAttachID, "PE")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblPEPKID.Text = Arr(1)
            lnkbtnEmpDetails_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Professional Experiance)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
            lblEmpProfessionalExperienceModelError.Text = "Successfully Saved."
            btnEPESave.Visible = False : btnEPEUpdate.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEPESave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnEPEUpdate_Click(sender As Object, e As EventArgs) Handles btnEPEUpdate.Click
        Dim Arr() As String
        Try
            lblError.Text = "" : lblEmpProfessionalExperienceModelError.Text = ""
            If txtEPEAssignment.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Assignment."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEAssignment.Focus()
                Exit Sub
            End If
            If txtEPEAssignment.Text.Trim.Length > 500 Then
                lblEmpProfessionalExperienceModelError.Text = "Assignment exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEAssignment.Focus()
                Exit Sub
            End If
            If txtEPEReportingTo.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Reporting To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEReportingTo.Focus()
                Exit Sub
            End If
            If txtEPEReportingTo.Text.Trim.Length > 50 Then
                lblEmpProfessionalExperienceModelError.Text = "Assignment exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEReportingTo.Focus()
                Exit Sub
            End If
            If txtEPEFrom.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter From."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEFrom.Focus()
                Exit Sub
            End If
            If txtEPETo.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPETo.Focus()
                Exit Sub
            End If
            If txtEPEFrom.Text.Trim.Length > 10 Then
                lblEmpProfessionalExperienceModelError.Text = "From exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEFrom.Focus()
                Exit Sub
            End If
            If txtEPETo.Text.Trim.Length > 10 Then
                lblEmpProfessionalExperienceModelError.Text = "To exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPETo.Focus()
                Exit Sub
            End If
            If txtEPESalaryPerAnnum.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Salary Per Annum."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPESalaryPerAnnum.Focus()
                Exit Sub
            End If
            If txtEPEPositionHeld.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Position Held."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEPositionHeld.Focus()
                Exit Sub
            End If
            If txtEPEPositionHeld.Text.Trim.Length > 20 Then
                lblEmpProfessionalExperienceModelError.Text = "Position Held exceeded maximum size(max 20 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPEPositionHeld.Focus()
                Exit Sub
            End If
            If txtEPERemarks.Text = "" Then
                lblEmpProfessionalExperienceModelError.Text = "Enter Remarks."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPERemarks.Focus()
                Exit Sub
            End If
            If txtEPERemarks.Text.Trim.Length > 8000 Then
                lblEmpProfessionalExperienceModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
                txtEPERemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUPPKID = Val(lblPEPKID.Text)
            objclsEProfile.iSUPUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.sSUPAssignment = objclsGRACeGeneral.SafeSQL(txtEPEAssignment.Text)
            objclsEProfile.sSUPReportingTo = objclsGRACeGeneral.SafeSQL(txtEPEReportingTo.Text)
            objclsEProfile.iSUPFrom = Val(txtEPEFrom.Text)
            objclsEProfile.iSUPTo = Val(txtEPETo.Text)
            objclsEProfile.dSUPSalaryPerAnnum = Val(txtEPESalaryPerAnnum.Text)
            objclsEProfile.sSUPPosition = objclsGRACeGeneral.SafeSQL(txtEPEPositionHeld.Text)
            objclsEProfile.sSUPRemarks = objclsGRACeGeneral.SafeSQL(txtEPERemarks.Text)
            objclsEProfile.iSUPAttachID = iEmpPEAttachID
            objclsEProfile.iSUPCrBy = sSession.UserID
            objclsEProfile.iSUPUpdatedBy = sSession.UserID
            objclsEProfile.sSUPIPAddress = sSession.IPAddress
            objclsEProfile.iSUPCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpProfessionalExperienceDetails(sSession.AccessCode, objclsEProfile)
            If iEmpPEAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpPEAttachID, "PE")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblPEPKID.Text = Arr(1)
            lnkbtnEmpDetails_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Professional Experiance)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpProfessionalExperienceModal').modal('show');", True)
            lblEmpProfessionalExperienceModelError.Text = "Successfully Updated."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnEPEUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddPEAttach_Click(sender As Object, e As EventArgs) Handles btnAddPEAttach.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblPEMsg.Text = "" : iEmpPEDocID = 0
            If Not (txtPEfile.PostedFile Is Nothing) And txtPEfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtPEfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblPEMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalProfessionalExperienceAttchment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtPEfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtPEfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iEmpPEAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iEmpPEAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpPEAttachID, lblPE.Text)
                    If iEmpPEAttachID > 0 Then
                        sEmpDetailsAttachIDs = iEmpPEAttachID & "," & iEmpALAttachID
                        BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
                    End If
                Else
                    lblPEMsg.Text = "No file to Attach."
                End If
            Else
                lblPEMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalProfessionalExperienceAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddPEAttach_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvProfessionalExperienceAttach_PreRender(sender As Object, e As EventArgs) Handles gvProfessionalExperienceAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvProfessionalExperienceAttach.Rows.Count > 0 Then
                gvProfessionalExperienceAttach.UseAccessibleHeader = True
                gvProfessionalExperienceAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvProfessionalExperienceAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvProfessionalExperienceAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvProfessionalExperienceAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvProfessionalExperienceAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvProfessionalExperienceAttach.Columns(4).Visible = True
                'Else
                '    gvProfessionalExperienceAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvProfessionalExperienceAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvProfessionalExperienceAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvProfessionalExperienceAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblPEMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpPEDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpPEAttachID, iEmpPEDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpPEDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpPEAttachID, iEmpPEDocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpALAttachID, iEmpALDocID)
                BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalProfessionalExperienceAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvProfessionalExperienceAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Assets Obtained On Loan
    Private Sub gvAssestsLoan_PreRender(sender As Object, e As EventArgs) Handles gvAssestsLoan.PreRender
        Dim dt As New DataTable
        Try
            If gvAssestsLoan.Rows.Count > 0 Then
                gvAssestsLoan.UseAccessibleHeader = True
                gvAssestsLoan.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAssestsLoan.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssestsLoan_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAssestsLoan_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAssestsLoan.RowCommand
        Dim dtAssetsLoanDetails As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblALPKID = DirectCast(clickedRow.FindControl("lblALPKID"), Label)
                If ddlExistingEmployee.SelectedIndex > 0 Then
                    dtAssetsLoanDetails = objclsEProfile.LoadEmpAsstesLoanDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, lblALPKID.Text)
                    If dtAssetsLoanDetails.Rows.Count > 0 Then
                        btnALSave.Visible = False : btnALUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnALUpdate.Visible = True
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("TypeOfAsset")) = False Then
                            txtEATypeOfAsset.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAssetsLoanDetails.Rows(0).Item("TypeOfAsset").ToString())
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("SerialNo")) = False Then
                            txtEASerialNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAssetsLoanDetails.Rows(0).Item("SerialNo").ToString())
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("ApproxValue")) = False Then
                            txtEAApproValue.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAssetsLoanDetails.Rows(0).Item("ApproxValue").ToString())
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("IssueDate")) = False Then
                            txtEAIssueDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtAssetsLoanDetails.Rows(0).Item("IssueDate"), "D")
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("DueDate")) = False Then
                            txtEADueDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtAssetsLoanDetails.Rows(0).Item("DueDate"), "D")
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("RecievedDate")) = False Then
                            If dtAssetsLoanDetails.Rows(0).Item("RecievedDate") = "" Then
                                txtEARecievedDate.Text = ""
                            Else
                                txtEARecievedDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtAssetsLoanDetails.Rows(0).Item("RecievedDate"), "D")
                            End If
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("ConditionWhenIssued")) = False Then
                            txtEAConditionIssue.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAssetsLoanDetails.Rows(0).Item("ConditionWhenIssued").ToString())
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("ConditionOnReceipt")) = False Then
                            txtEAConditionReceipt.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAssetsLoanDetails.Rows(0).Item("ConditionOnReceipt").ToString())
                        End If
                        If IsDBNull(dtAssetsLoanDetails.Rows(0).Item("Remarks")) = False Then
                            txtEARemarks.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAssetsLoanDetails.Rows(0).Item("Remarks").ToString())
                        End If
                        gvAssestsLoan.DataSource = dtAssetsLoanDetails
                        gvAssestsLoan.DataBind()
                    End If
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAssestsLoan_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnALNew_Click(sender As Object, e As EventArgs) Handles btnALNew.Click
        Try
            btnALSave.Visible = False : btnALUpdate.Visible = False
            If (sStatus <> "D") Then
                btnALSave.Visible = True
            End If
            lblEmpAssetsModelError.Text = "" : txtEATypeOfAsset.Text = "" : txtEASerialNo.Text = "" : txtEAApproValue.Text = ""
            txtEAIssueDate.Text = "" : txtEADueDate.Text = "" : txtEAConditionIssue.Text = "" : txtEAConditionReceipt.Text = "" : txtEARemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
        Catch ex As Exception
            lblEmpAssetsModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnALNew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnALCancel_Click(sender As Object, e As EventArgs) Handles btnALCancel.Click
        Try
            btnALSave.Visible = False : btnALUpdate.Visible = False
            If (sStatus <> "D") Then
                btnALSave.Visible = True
            End If
            lblEmpAssetsModelError.Text = "" : txtEATypeOfAsset.Text = "" : txtEASerialNo.Text = "" : txtEAApproValue.Text = ""
            txtEAIssueDate.Text = "" : txtEADueDate.Text = "" : txtEAConditionIssue.Text = "" : txtEAConditionReceipt.Text = "" : txtEARemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('hide');", True)
        Catch ex As Exception
            lblEmpAssetsModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnALCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnALSave_Click(sender As Object, e As EventArgs) Handles btnALSave.Click
        Dim Arr() As String
        Try
            lblError.Text = "" : lblEmpAssetsModelError.Text = ""
            If txtEATypeOfAsset.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Type Of Asset."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEATypeOfAsset.Focus()
                Exit Sub
            End If
            If txtEATypeOfAsset.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Type Of Asset exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEATypeOfAsset.Focus()
                Exit Sub
            End If
            If txtEASerialNo.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Serial No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEASerialNo.Focus()
                Exit Sub
            End If
            If txtEASerialNo.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Serial No exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEASerialNo.Focus()
                Exit Sub
            End If
            If txtEAApproValue.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Approximate Value exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAApproValue.Focus()
                Exit Sub
            End If
            If txtEAIssueDate.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Issue Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAIssueDate.Focus()
                Exit Sub
            End If
            If txtEAIssueDate.Text <> "" Then
                Dim dIssueDate As Date
                Try
                    dIssueDate = DateTime.ParseExact(txtEAIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAssetsModelError.Text = "Enter valid Issue Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                    txtEAIssueDate.Focus()
                    Exit Sub
                End Try
            End If
            If txtEADueDate.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Due Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEADueDate.Focus()
                Exit Sub
            End If
            If txtEADueDate.Text <> "" Then
                Dim dDueDate As Date
                Try
                    dDueDate = DateTime.ParseExact(txtEADueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAssetsModelError.Text = "Enter valid Due Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                    txtEADueDate.Focus()
                    Exit Sub
                End Try
            End If
            If txtEARecievedDate.Text <> "" Then
                Dim dDueDate As Date
                Try
                    dDueDate = DateTime.ParseExact(txtEARecievedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAssetsModelError.Text = "Enter valid Recieved Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                    txtEARecievedDate.Focus()
                    Exit Sub
                End Try
            End If
            If txtEAConditionIssue.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Condition When Issued."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAConditionIssue.Focus()
                Exit Sub
            End If
            If txtEAConditionIssue.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Condition When Issued exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAConditionIssue.Focus()
                Exit Sub
            End If
            If txtEAConditionReceipt.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Condition On Receipt exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAConditionReceipt.Focus()
                Exit Sub
            End If
            If txtEARemarks.Text.Trim.Length > 8000 Then
                lblEmpAssetsModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEARemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUALPKID = 0
            objclsEProfile.iSUALUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.sSUALAssetType = objclsGRACeGeneral.SafeSQL(txtEATypeOfAsset.Text)
            objclsEProfile.sSUALSerialNo = objclsGRACeGeneral.SafeSQL(txtEASerialNo.Text)
            objclsEProfile.iSUALApproValue = Val(txtEAApproValue.Text)
            objclsEProfile.dSUALIssueDate = DateTime.ParseExact(txtEAIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.dSUALDueDate = DateTime.ParseExact(txtEADueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If txtEARecievedDate.Text = "" Then
                objclsEProfile.dSUALRecievedDate = "01/01/1900"
            Else
                objclsEProfile.dSUALRecievedDate = DateTime.ParseExact(txtEARecievedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objclsEProfile.sSUALConditionIssue = objclsGRACeGeneral.SafeSQL(txtEAConditionIssue.Text)
            objclsEProfile.sSUALConditionReceipt = objclsGRACeGeneral.SafeSQL(txtEAConditionReceipt.Text)
            objclsEProfile.sSUALRemarks = objclsGRACeGeneral.SafeSQL(txtEARemarks.Text)
            objclsEProfile.iSUALAttachID = iEmpALAttachID
            objclsEProfile.iSUALCrBy = sSession.UserID
            objclsEProfile.iSUALUpdatedBy = sSession.UserID
            objclsEProfile.sSUALIPAddress = sSession.IPAddress
            objclsEProfile.iSUALCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpAsstesLoanDetails(sSession.AccessCode, objclsEProfile)
            If iEmpALAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpALAttachID, "AL")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblALPKID.Text = Arr(1)
            lnkbtnEmpDetails_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Assets Obtained On Loan)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
            lblEmpAssetsModelError.Text = "Successfully Saved."
            btnALSave.Visible = False : btnALUpdate.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnALSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnALUpdate_Click(sender As Object, e As EventArgs) Handles btnALUpdate.Click
        Dim Arr() As String
        Try
            lblError.Text = "" : lblEmpAssetsModelError.Text = ""
            If txtEATypeOfAsset.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Type Of Asset."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEATypeOfAsset.Focus()
                Exit Sub
            End If
            If txtEATypeOfAsset.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Type Of Asset exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEATypeOfAsset.Focus()
                Exit Sub
            End If
            If txtEASerialNo.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Serial No."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEASerialNo.Focus()
                Exit Sub
            End If
            If txtEASerialNo.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Serial No exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEASerialNo.Focus()
                Exit Sub
            End If
            If txtEAIssueDate.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Issue Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAIssueDate.Focus()
                Exit Sub
            End If
            If txtEAIssueDate.Text <> "" Then
                Dim dIssueDate As Date
                Try
                    dIssueDate = DateTime.ParseExact(txtEAIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAssetsModelError.Text = "Enter valid Issue Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                    txtEAIssueDate.Focus()
                    Exit Sub
                End Try
            End If
            If txtEADueDate.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Due Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEADueDate.Focus()
                Exit Sub
            End If
            If txtEADueDate.Text <> "" Then
                Dim dDueDate As Date
                Try
                    dDueDate = DateTime.ParseExact(txtEADueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAssetsModelError.Text = "Enter valid Due Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                    txtEADueDate.Focus()
                    Exit Sub
                End Try
            End If
            If txtEARecievedDate.Text <> "" Then
                Dim dDueDate As Date
                Try
                    dDueDate = DateTime.ParseExact(txtEARecievedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAssetsModelError.Text = "Enter valid Recieved Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                    txtEARecievedDate.Focus()
                    Exit Sub
                End Try
            End If
            If txtEAConditionIssue.Text = "" Then
                lblEmpAssetsModelError.Text = "Enter Condition When Issued."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAConditionIssue.Focus()
                Exit Sub
            End If
            If txtEAConditionIssue.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Condition When Issued exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAConditionIssue.Focus()
                Exit Sub
            End If
            If txtEAConditionReceipt.Text.Trim.Length > 500 Then
                lblEmpAssetsModelError.Text = "Condition On Receipt exceeded maximum size(max 500 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEAConditionReceipt.Focus()
                Exit Sub
            End If
            If txtEARemarks.Text.Trim.Length > 8000 Then
                lblEmpAssetsModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
                txtEARemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUALPKID = Val(lblALPKID.Text)
            objclsEProfile.iSUALUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.sSUALAssetType = objclsGRACeGeneral.SafeSQL(txtEATypeOfAsset.Text)
            objclsEProfile.sSUALSerialNo = objclsGRACeGeneral.SafeSQL(txtEASerialNo.Text)
            objclsEProfile.iSUALApproValue = Val(txtEAApproValue.Text)
            objclsEProfile.dSUALIssueDate = DateTime.ParseExact(txtEAIssueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.dSUALDueDate = DateTime.ParseExact(txtEADueDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If txtEARecievedDate.Text = "" Then
                objclsEProfile.dSUALRecievedDate = "01/01/1900"
            Else
                objclsEProfile.dSUALRecievedDate = DateTime.ParseExact(txtEARecievedDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            End If
            objclsEProfile.sSUALConditionIssue = objclsGRACeGeneral.SafeSQL(txtEAConditionIssue.Text)
            objclsEProfile.sSUALConditionReceipt = objclsGRACeGeneral.SafeSQL(txtEAConditionReceipt.Text)
            objclsEProfile.sSUALRemarks = objclsGRACeGeneral.SafeSQL(txtEARemarks.Text)
            objclsEProfile.iSUALAttachID = iEmpALAttachID
            objclsEProfile.iSUALCrBy = sSession.UserID
            objclsEProfile.iSUALUpdatedBy = sSession.UserID
            objclsEProfile.sSUALIPAddress = sSession.IPAddress
            objclsEProfile.iSUALCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpAsstesLoanDetails(sSession.AccessCode, objclsEProfile)
            If iEmpALAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpALAttachID, "AL")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblALPKID.Text = Arr(1)
            lnkbtnEmpDetails_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Assets Obtained On Loan)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAssestsModal').modal('show');", True)
            lblEmpAssetsModelError.Text = "Successfully Updated."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnALUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddALAttach_Click(sender As Object, e As EventArgs) Handles btnAddALAttach.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblALMsg.Text = "" : iEmpALDocID = 0
            If Not (txtALfile.PostedFile Is Nothing) And txtALfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtALfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblALMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAsstesLoanAttchment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtALfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtALfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iEmpALAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iEmpALAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpALAttachID, lblAL.Text)
                    If iEmpALAttachID > 0 Then
                        sEmpDetailsAttachIDs = iEmpALAttachID
                        BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
                    End If
                Else
                    lblALMsg.Text = "No file to Attach."
                End If
            Else
                lblALMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAsstesLoanAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddALAttach_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAsstesLoanAttach_PreRender(sender As Object, e As EventArgs) Handles gvAsstesLoanAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvAsstesLoanAttach.Rows.Count > 0 Then
                gvAsstesLoanAttach.UseAccessibleHeader = True
                gvAsstesLoanAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAsstesLoanAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAsstesLoanAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAsstesLoanAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAsstesLoanAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvAsstesLoanAttach.Columns(4).Visible = True
                'Else
                '    gvAsstesLoanAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAsstesLoanAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAsstesLoanAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAsstesLoanAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblALMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpPEDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpPEAttachID, iEmpPEDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpPEDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpPEAttachID, iEmpPEDocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpALAttachID, iEmpALDocID)
                BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAsstesLoanAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAsstesLoanAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Assessment
    Private Sub btnPANew_Click(sender As Object, e As EventArgs) Handles btnPANew.Click
        Try
            lblError.Text = "" : lblEmpPerformanceAssessmentModelError.Text = "" : txtPAAssessmentDate.Text = "" : txtPARating.Text = ""
            txtPAPerformanceAwardPaid.Text = "" : txtPAGradesPromotedFrom.Text = "" : txtPAGradesPromotedTo.Text = "" : txtPARemarks.Text = ""
            btnPASave.Visible = False : btnPAUpdate.Visible = False
            If (sStatus <> "D") Then
                btnPASave.Visible = True
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPANew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnPACancel_Click(sender As Object, e As EventArgs) Handles btnPACancel.Click
        Try
            lblError.Text = "" : lblEmpPerformanceAssessmentModelError.Text = "" : txtPAAssessmentDate.Text = "" : txtPARating.Text = ""
            txtPAPerformanceAwardPaid.Text = "" : txtPAGradesPromotedFrom.Text = "" : txtPAGradesPromotedTo.Text = "" : txtPARemarks.Text = ""
            btnPASave.Visible = False : btnPAUpdate.Visible = False
            If (sStatus <> "D") Then
                btnPASave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPACancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnPASave_Click(sender As Object, e As EventArgs) Handles btnPASave.Click
        Dim objAssessment As New strUserEmp_AssessmentDetails
        Dim Arr() As String
        Dim dToDate As Date
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtPAAssessmentDate.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Issue Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAAssessmentDate.Focus()
                    Exit Sub
                Else
                    Try
                        dToDate = DateTime.ParseExact(txtPAAssessmentDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpPerformanceAssessmentModelError.Text = "Enter valid Issue Date." : lblError.Text = "Enter valid Issue Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                        txtPAAssessmentDate.Focus()
                        Exit Sub
                    End Try
                End If
                If txtPARating.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Rating."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPARating.Focus()
                    Exit Sub
                End If
                If txtPAPerformanceAwardPaid.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Performance Award Paid."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAPerformanceAwardPaid.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedFrom.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Grade Promoted From."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedFrom.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedTo.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Grade Promoted To."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedTo.Focus()
                    Exit Sub
                End If
                If txtPARating.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Rating exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPARating.Focus()
                    Exit Sub
                End If
                If txtPAPerformanceAwardPaid.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Performance Award Paid exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAPerformanceAwardPaid.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedFrom.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Grade Promoted From exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedFrom.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedTo.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Grade Promoted To exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedTo.Focus()
                    Exit Sub
                End If
                If txtPARemarks.Text.Length > 8000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPARemarks.Focus()
                    Exit Sub
                End If

                objAssessment.iSUA_PKID = 0
                objAssessment.iSUA_UserEmpID = ddlExistingEmployee.SelectedValue
                objAssessment.dSUA_IssueDate = Date.ParseExact(Trim(txtPAAssessmentDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objAssessment.sSUA_Rating = objclsGRACeGeneral.SafeSQL(txtPARating.Text.Trim())
                objAssessment.sSUA_PerformanceAwardPaid = objclsGRACeGeneral.SafeSQL(txtPAPerformanceAwardPaid.Text.Trim())
                objAssessment.sSUA_GradesPromotedFrom = objclsGRACeGeneral.SafeSQL(txtPAGradesPromotedFrom.Text.Trim())
                objAssessment.sSUA_GradesPromotedTo = objclsGRACeGeneral.SafeSQL(txtPAGradesPromotedTo.Text.Trim())
                objAssessment.sSUA_Remarks = objclsGRACeGeneral.SafeSQL(txtPARemarks.Text.Trim())
                objAssessment.iSUA_AttachID = 0
                objAssessment.iSUA_CrBy = sSession.UserID
                objAssessment.iSUA_UpdatedBy = sSession.UserID
                objAssessment.sSUA_IPAddress = sSession.IPAddress
                objAssessment.iSUA_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpAssessmentDetails(sSession.AccessCode, objAssessment)
                If iPAAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iPAAttachID, "PA")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblPAPKID.Text = Arr(1)
                lnkbtnHRDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Performance Assessments)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpPerformanceAssessmentModelError.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                btnPASave.Visible = False : btnPAUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPASave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnPAUpdate_Click(sender As Object, e As EventArgs) Handles btnPAUpdate.Click
        Dim objAssessment As New strUserEmp_AssessmentDetails
        Dim Arr() As String
        Dim dToDate As Date
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtPAAssessmentDate.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Issue Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAAssessmentDate.Focus()
                    Exit Sub
                Else
                    Try
                        dToDate = DateTime.ParseExact(txtPAAssessmentDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpPerformanceAssessmentModelError.Text = "Enter valid Issue Date." : lblError.Text = "Enter valid Issue Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                        txtPAAssessmentDate.Focus()
                        Exit Sub
                    End Try
                End If
                If txtPARating.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Rating."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPARating.Focus()
                    Exit Sub
                End If
                If txtPAPerformanceAwardPaid.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Performance Award Paid."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAPerformanceAwardPaid.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedFrom.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Grade Promoted From."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedFrom.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedTo.Text = "" Then
                    lblEmpPerformanceAssessmentModelError.Text = "Enter Grade Promoted To."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedTo.Focus()
                    Exit Sub
                End If
                If txtPARating.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Rating exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPARating.Focus()
                    Exit Sub
                End If
                If txtPAPerformanceAwardPaid.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Performance Award Paid exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAPerformanceAwardPaid.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedFrom.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Grade Promoted From exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedFrom.Focus()
                    Exit Sub
                End If
                If txtPAGradesPromotedTo.Text.Length > 1000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Grade Promoted To exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPAGradesPromotedTo.Focus()
                    Exit Sub
                End If
                If txtPARemarks.Text.Length > 8000 Then
                    lblEmpPerformanceAssessmentModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                    txtPARemarks.Focus()
                    Exit Sub
                End If

                objAssessment.iSUA_PKID = Val(lblPAPKID.Text)
                objAssessment.iSUA_UserEmpID = ddlExistingEmployee.SelectedValue
                objAssessment.dSUA_IssueDate = Date.ParseExact(Trim(txtPAAssessmentDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objAssessment.sSUA_Rating = objclsGRACeGeneral.SafeSQL(txtPARating.Text.Trim())
                objAssessment.sSUA_PerformanceAwardPaid = objclsGRACeGeneral.SafeSQL(txtPAPerformanceAwardPaid.Text.Trim())
                objAssessment.sSUA_GradesPromotedFrom = objclsGRACeGeneral.SafeSQL(txtPAGradesPromotedFrom.Text.Trim())
                objAssessment.sSUA_GradesPromotedTo = objclsGRACeGeneral.SafeSQL(txtPAGradesPromotedTo.Text.Trim())
                objAssessment.sSUA_Remarks = objclsGRACeGeneral.SafeSQL(txtPARemarks.Text.Trim())
                objAssessment.iSUA_AttachID = 0
                objAssessment.iSUA_CrBy = sSession.UserID
                objAssessment.iSUA_UpdatedBy = sSession.UserID
                objAssessment.sSUA_IPAddress = sSession.IPAddress
                objAssessment.iSUA_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpAssessmentDetails(sSession.AccessCode, objAssessment)
                If iPAAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iPAAttachID, "PA")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblPAPKID.Text = Arr(1)
                lnkbtnHRDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Performance Assessments)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpPerformanceAssessmentModelError.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                btnPASave.Visible = False : btnPAUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPAUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPerformanceAssessments_PreRender(sender As Object, e As EventArgs) Handles gvPerformanceAssessments.PreRender
        Dim dt As New DataTable
        Try
            If gvPerformanceAssessments.Rows.Count > 0 Then
                gvPerformanceAssessments.UseAccessibleHeader = True
                gvPerformanceAssessments.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPerformanceAssessments.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPerformanceAssessments_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPerformanceAssessments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPerformanceAssessments.RowCommand
        Dim lblID As New Label, lblUserID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblUserID = DirectCast(clickedRow.FindControl("lblUserID"), Label)
                If lblID.Text > 0 And lblUserID.Text > 0 Then
                    dt = objclsEProfile.LoadEMPAssessment(sSession.AccessCode, sSession.AccessCodeID, lblUserID.Text, lblID.Text)
                    If dt.Rows.Count > 0 Then
                        lblPAPKID.Text = lblID.Text
                        If IsDBNull(dt.Rows(0)("PARating")) = False Then
                            txtPARating.Text = dt.Rows(0).Item("PARating")
                        End If
                        If IsDBNull(dt.Rows(0)("AssessmentDate")) = False Then
                            txtPAAssessmentDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("AssessmentDate"), "D")
                        End If
                        If IsDBNull(dt.Rows(0)("PerformanceAwardPaid")) = False Then
                            txtPAPerformanceAwardPaid.Text = dt.Rows(0).Item("PerformanceAwardPaid")
                        End If
                        If IsDBNull(dt.Rows(0)("GradePromotedFrom")) = False Then
                            txtPAGradesPromotedFrom.Text = dt.Rows(0).Item("GradePromotedFrom")
                        End If
                        If IsDBNull(dt.Rows(0)("GradePromotedTo")) = False Then
                            txtPAGradesPromotedTo.Text = dt.Rows(0).Item("GradePromotedTo")
                        End If
                        If IsDBNull(dt.Rows(0)("Remarks")) = False Then
                            txtPARemarks.Text = dt.Rows(0).Item("Remarks")
                        End If
                        btnPASave.Visible = False : btnPAUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnPAUpdate.Visible = True
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpPerformanceAssessmentsModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPerformanceAssessments_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Academic Progress
    Private Sub gvAcademicProgress_PreRender(sender As Object, e As EventArgs) Handles gvAcademicProgress.PreRender
        Dim dt As New DataTable
        Try
            If gvAcademicProgress.Rows.Count > 0 Then
                gvAcademicProgress.UseAccessibleHeader = True
                gvAcademicProgress.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAcademicProgress.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAcademicProgress_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAcademicProgress_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAcademicProgress.RowCommand
        Dim dtAcademicProgressDetails As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblAPPKID = DirectCast(clickedRow.FindControl("lblAPPKID"), Label)
                If ddlExistingEmployee.SelectedIndex > 0 Then
                    dtAcademicProgressDetails = objclsEProfile.LoadEmpAcademicProgressDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, lblAPPKID.Text)
                    If dtAcademicProgressDetails.Rows.Count > 0 Then
                        btnAPSave.Visible = False : btnAPUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnAPUpdate.Visible = True
                        End If
                        If IsDBNull(dtAcademicProgressDetails.Rows(0).Item("ExamTaken")) = False Then
                            txtAPExamTaken.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtAcademicProgressDetails.Rows(0).Item("ExamTaken"), "D")
                        End If
                        If IsDBNull(dtAcademicProgressDetails.Rows(0).Item("NoOfDaysLeave")) = False Then
                            txtAPLeaveGranted.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAcademicProgressDetails.Rows(0).Item("NoOfDaysLeave").ToString())
                        End If
                        ddlAPMonthExam.SelectedValue = 0
                        If IsDBNull(dtAcademicProgressDetails.Rows(0).Item("MonthOfExam")) = False Then
                            ddlAPMonthExam.SelectedValue = dtAcademicProgressDetails.Rows(0).Item("MonthOfExamID")
                        End If
                        If IsDBNull(dtAcademicProgressDetails.Rows(0).Item("Groups")) = False Then
                            txtAPGroups.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAcademicProgressDetails.Rows(0).Item("Groups").ToString())
                        End If
                        If IsDBNull(dtAcademicProgressDetails.Rows(0).Item("Result")) = False Then
                            txtAPResult.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAcademicProgressDetails.Rows(0).Item("Result").ToString())
                        End If
                        If IsDBNull(dtAcademicProgressDetails.Rows(0).Item("Remarks")) = False Then
                            txtAPRemarks.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtAcademicProgressDetails.Rows(0).Item("Remarks").ToString())
                        End If
                        gvAcademicProgress.DataSource = dtAcademicProgressDetails
                        gvAcademicProgress.DataBind()
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAcademicProgress_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAPNew_Click(sender As Object, e As EventArgs) Handles btnAPNew.Click
        Try
            btnAPSave.Visible = False : btnAPUpdate.Visible = False
            If (sStatus <> "D") Then
                btnAPSave.Visible = True
            End If
            lblEmpAcademicProgressModelError.Text = "" : txtAPExamTaken.Text = "" : txtAPLeaveGranted.Text = "" : ddlAPMonthExam.SelectedValue = 0
            txtAPGroups.Text = "" : txtAPResult.Text = "" : txtAPRemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
        Catch ex As Exception
            lblEmpProfessionalExperienceModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAPNew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAPCancel_Click(sender As Object, e As EventArgs) Handles btnAPCancel.Click
        Try
            btnAPSave.Visible = False : btnAPUpdate.Visible = False
            If (sStatus <> "D") Then
                btnAPSave.Visible = True
            End If
            lblEmpAcademicProgressModelError.Text = "" : txtAPExamTaken.Text = "" : txtAPLeaveGranted.Text = "" : ddlAPMonthExam.SelectedValue = 0
            txtAPGroups.Text = "" : txtAPResult.Text = "" : txtAPRemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('hide');", True)
        Catch ex As Exception
            lblEmpProfessionalExperienceModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAPCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnAPSave_Click(sender As Object, e As EventArgs) Handles btnAPSave.Click
        Dim Arr() As String
        Try
            lblError.Text = "" : lblEmpAcademicProgressModelError.Text = ""
            If txtAPExamTaken.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Examination Taken."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPExamTaken.Focus()
                Exit Sub
            End If
            If txtAPExamTaken.Text <> "" Then
                Dim dExaminationTaken As Date
                Try
                    dExaminationTaken = DateTime.ParseExact(txtAPExamTaken.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAcademicProgressModelError.Text = "Enter valid Examination Taken."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                    txtAPExamTaken.Focus()
                    Exit Sub
                End Try
            End If
            If txtAPLeaveGranted.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter No. of Days Leave Granted."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPLeaveGranted.Focus()
                Exit Sub
            End If
            If txtAPLeaveGranted.Text.Trim.Length > 10 Then
                lblEmpAcademicProgressModelError.Text = "No. of Days Leave Granted exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPLeaveGranted.Focus()
                Exit Sub
            End If
            If ddlAPMonthExam.SelectedIndex = 0 Then
                lblEmpAcademicProgressModelError.Text = "Select Month of Exam."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                ddlAPMonthExam.Focus()
                Exit Sub
            End If
            If txtAPGroups.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Groups."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPGroups.Focus()
                Exit Sub
            End If
            If txtAPGroups.Text.Trim.Length > 1000 Then
                lblEmpAcademicProgressModelError.Text = "Groups exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPGroups.Focus()
                Exit Sub
            End If
            If txtAPResult.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Result."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPResult.Focus()
                Exit Sub
            End If
            If txtAPResult.Text.Trim.Length > 1000 Then
                lblEmpAcademicProgressModelError.Text = "Result exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPResult.Focus()
                Exit Sub
            End If
            If txtAPRemarks.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Remarks."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPRemarks.Focus()
                Exit Sub
            End If
            If txtAPRemarks.Text.Trim.Length > 8000 Then
                lblEmpAcademicProgressModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPRemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUAPPKID = 0
            objclsEProfile.iSUAPUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.dSUAPExamTakenOn = DateTime.ParseExact(txtAPExamTaken.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.iSUAPLeaveGranted = Val(txtAPLeaveGranted.Text)
            objclsEProfile.iSUAPMonthofExam = ddlAPMonthExam.SelectedValue
            objclsEProfile.sSUAPGroups = objclsGRACeGeneral.SafeSQL(txtAPGroups.Text)
            objclsEProfile.sSUAPResult = objclsGRACeGeneral.SafeSQL(txtAPResult.Text)
            objclsEProfile.sSUAPRemarks = objclsGRACeGeneral.SafeSQL(txtAPRemarks.Text)
            objclsEProfile.iSUAPAttachID = iEmpAPAttachID
            objclsEProfile.iSUAPCrBy = sSession.UserID
            objclsEProfile.iSUAPUpdatedBy = sSession.UserID
            objclsEProfile.sSUAPIPAddress = sSession.IPAddress
            objclsEProfile.iSUAPCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpAcademicProgressDetails(sSession.AccessCode, objclsEProfile)
            If iEmpAPAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpAPAttachID, "AP")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblAPPKID.Text = Arr(1)
            lnkbtnHRDetails_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Academic Progress)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
            lblEmpAcademicProgressModelError.Text = "Successfully Saved."
            btnAPSave.Visible = False : btnAPUpdate.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAPSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnAPUpdate_Click(sender As Object, e As EventArgs) Handles btnAPUpdate.Click
        Dim Arr() As String
        Try
            lblError.Text = "" : lblEmpAcademicProgressModelError.Text = ""
            If txtAPExamTaken.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Examination Taken."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPExamTaken.Focus()
                Exit Sub
            End If
            If txtAPExamTaken.Text <> "" Then
                Dim dExaminationTaken As Date
                Try
                    dExaminationTaken = DateTime.ParseExact(txtAPExamTaken.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpAcademicProgressModelError.Text = "Enter valid Examination Taken."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                    txtAPExamTaken.Focus()
                    Exit Sub
                End Try
            End If
            If txtAPLeaveGranted.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter No. of Days Leave Granted."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPLeaveGranted.Focus()
                Exit Sub
            End If
            If txtAPLeaveGranted.Text.Trim.Length > 10 Then
                lblEmpAcademicProgressModelError.Text = "No. of Days Leave Granted exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPLeaveGranted.Focus()
                Exit Sub
            End If
            If ddlAPMonthExam.SelectedIndex = 0 Then
                lblEmpAcademicProgressModelError.Text = "Select Month of Exam."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                ddlAPMonthExam.Focus()
                Exit Sub
            End If
            If txtAPGroups.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Groups."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPGroups.Focus()
                Exit Sub
            End If
            If txtAPGroups.Text.Trim.Length > 1000 Then
                lblEmpAcademicProgressModelError.Text = "Groups exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPGroups.Focus()
                Exit Sub
            End If
            If txtAPResult.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Result."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPResult.Focus()
                Exit Sub
            End If
            If txtAPResult.Text.Trim.Length > 1000 Then
                lblEmpAcademicProgressModelError.Text = "Result exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPResult.Focus()
                Exit Sub
            End If
            If txtAPRemarks.Text = "" Then
                lblEmpAcademicProgressModelError.Text = "Enter Remarks."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPRemarks.Focus()
                Exit Sub
            End If
            If txtAPRemarks.Text.Trim.Length > 8000 Then
                lblEmpAcademicProgressModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
                txtAPRemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUAPPKID = Val(lblAPPKID.Text)
            objclsEProfile.iSUAPUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.dSUAPExamTakenOn = DateTime.ParseExact(txtAPExamTaken.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.iSUAPLeaveGranted = Val(txtAPLeaveGranted.Text)
            objclsEProfile.iSUAPMonthofExam = ddlAPMonthExam.SelectedValue
            objclsEProfile.sSUAPGroups = objclsGRACeGeneral.SafeSQL(txtAPGroups.Text)
            objclsEProfile.sSUAPResult = objclsGRACeGeneral.SafeSQL(txtAPResult.Text)
            objclsEProfile.sSUAPRemarks = objclsGRACeGeneral.SafeSQL(txtAPRemarks.Text)
            objclsEProfile.iSUAPAttachID = iEmpAPAttachID
            objclsEProfile.iSUAPCrBy = sSession.UserID
            objclsEProfile.iSUAPUpdatedBy = sSession.UserID
            objclsEProfile.sSUAPIPAddress = sSession.IPAddress
            objclsEProfile.iSUAPCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpAcademicProgressDetails(sSession.AccessCode, objclsEProfile)
            If iEmpAPAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpAPAttachID, "AP")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblAPPKID.Text = Arr(1)
            lnkbtnHRDetails_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Academic Progress)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpAcademicProgressModal').modal('show');", True)
            lblEmpAcademicProgressModelError.Text = "Successfully Updated."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAPUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddAPAttach_Click(sender As Object, e As EventArgs) Handles btnAddAPAttach.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblAPMsg.Text = "" : iEmpAPDocID = 0
            If Not (txtAPfile.PostedFile Is Nothing) And txtAPfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtAPfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblAPMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAcademicProgressAttchment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtAPfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtAPfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iEmpAPAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iEmpAPAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpAPAttachID, lblAP.Text)
                    If iEmpAPAttachID > 0 Then
                        sHRAttachIDs = iPAAttachID & "," & iEmpAPAttachID & "," & iSMAttachID
                        BindAllHRDetailsAttachments(sHRAttachIDs)
                    End If
                Else
                    lblAPMsg.Text = "No file to Attach."
                End If
            Else
                lblAPMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAcademicProgressAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddAPAttach_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAcademicProgressAttach_PreRender(sender As Object, e As EventArgs) Handles gvAcademicProgressAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvAcademicProgressAttach.Rows.Count > 0 Then
                gvAcademicProgressAttach.UseAccessibleHeader = True
                gvAcademicProgressAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvAcademicProgressAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAcademicProgressAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAcademicProgressAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvAcademicProgressAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvAcademicProgressAttach.Columns(4).Visible = True
                'Else
                '    gvAcademicProgressAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAcademicProgressAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvAcademicProgressAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvAcademicProgressAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblAPMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpAPDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpAPAttachID, iEmpAPDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpAPDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpAPAttachID, iEmpAPDocID)
                BindAllHRDetailsAttachments(sHRAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAcademicProgressAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvAcademicProgressAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Special Mentions
    Private Sub btnSMNew_Click(sender As Object, e As EventArgs) Handles btnSMNew.Click
        Try
            lblError.Text = "" : lblEmpSpecialMentionsModelError.Text = "" : txtSMSpecialMention.Text = "" : txtSMDate.Text = ""
            txtSMParticulars.Text = "" : txtSMDealtWith.Text = ""
            btnSMSave.Visible = False : btnSMUpdate.Visible = False
            If (sStatus <> "D") Then
                btnSMSave.Visible = True
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSMNew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnSMCancel_Click(sender As Object, e As EventArgs) Handles btnSMCancel.Click
        Try
            lblError.Text = "" : lblEmpSpecialMentionsModelError.Text = "" : txtSMSpecialMention.Text = "" : txtSMDate.Text = ""
            txtSMParticulars.Text = "" : txtSMDealtWith.Text = ""
            btnSMSave.Visible = False : btnSMUpdate.Visible = False
            If (sStatus <> "D") Then
                btnSMSave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSMCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnSMSave_Click(sender As Object, e As EventArgs) Handles btnSMSave.Click
        Dim objSpecialMention As New strUserEmp_SpecialMentionDetails
        Dim Arr() As String
        Dim dToDate As Date
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtSMSpecialMention.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter Special Mentions."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMSpecialMention.Focus()
                    Exit Sub
                End If
                If txtSMDate.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMDate.Focus()
                    Exit Sub
                Else
                    Try
                        dToDate = DateTime.ParseExact(txtSMDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpSpecialMentionsModelError.Text = "Enter valid Date." : lblError.Text = "Enter valid Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                        txtSMDate.Focus()
                        Exit Sub
                    End Try
                End If
                If txtSMParticulars.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter Particulars."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMParticulars.Focus()
                    Exit Sub
                End If
                If txtSMDealtWith.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter How Dealt Wit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMDealtWith.Focus()
                    Exit Sub
                End If
                If txtSMSpecialMention.Text.Length > 1000 Then
                    lblEmpSpecialMentionsModelError.Text = "Special Mentions exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMSpecialMention.Focus()
                    Exit Sub
                End If
                If txtSMParticulars.Text.Length > 1000 Then
                    lblEmpSpecialMentionsModelError.Text = "Particulars exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMParticulars.Focus()
                    Exit Sub
                End If
                If txtSMDealtWith.Text.Length > 1000 Then
                    lblEmpSpecialMentionsModelError.Text = "How Dealt Wit exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMDealtWith.Focus()
                    Exit Sub
                End If

                objSpecialMention.iSUS_PKID = 0
                objSpecialMention.iSUS_UserEmpID = ddlExistingEmployee.SelectedValue
                objSpecialMention.sSUS_SpecialMention = objclsGRACeGeneral.SafeSQL(txtSMSpecialMention.Text.Trim())
                objSpecialMention.dSUS_Date = Date.ParseExact(Trim(txtSMDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSpecialMention.sSUS_Particulars = objclsGRACeGeneral.SafeSQL(txtSMParticulars.Text.Trim())
                objSpecialMention.sSUS_DealtWith = objclsGRACeGeneral.SafeSQL(txtSMDealtWith.Text.Trim())
                objSpecialMention.iSUS_AttachID = 0
                objSpecialMention.iSUS_CrBy = sSession.UserID
                objSpecialMention.iSUS_UpdatedBy = sSession.UserID
                objSpecialMention.sSUS_IPAddress = sSession.IPAddress
                objSpecialMention.iSUS_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpSpecialMentionDetails(sSession.AccessCode, objSpecialMention)
                If iSMAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iSMAttachID, "SM")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblSMPKID.Text = Arr(1)
                lnkbtnHRDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Special Mentions)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpSpecialMentionsModelError.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpSpecialMentionsModal').modal('show');", True)
                btnSMSave.Visible = False : btnSMUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSMSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnSMUpdate_Click(sender As Object, e As EventArgs) Handles btnSMUpdate.Click
        Dim objSpecialMention As New strUserEmp_SpecialMentionDetails
        Dim Arr() As String
        Dim dToDate As Date
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtSMSpecialMention.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter Special Mentions."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMSpecialMention.Focus()
                    Exit Sub
                End If
                If txtSMDate.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMDate.Focus()
                    Exit Sub
                Else
                    Try
                        dToDate = DateTime.ParseExact(txtSMDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpSpecialMentionsModelError.Text = "Enter valid Date." : lblError.Text = "Enter valid Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                        txtSMDate.Focus()
                        Exit Sub
                    End Try
                End If
                If txtSMParticulars.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter Particulars."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMParticulars.Focus()
                    Exit Sub
                End If
                If txtSMDealtWith.Text = "" Then
                    lblEmpSpecialMentionsModelError.Text = "Enter How Dealt Wit."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMDealtWith.Focus()
                    Exit Sub
                End If
                If txtSMSpecialMention.Text.Length > 1000 Then
                    lblEmpSpecialMentionsModelError.Text = "Special Mentions exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMSpecialMention.Focus()
                    Exit Sub
                End If
                If txtSMParticulars.Text.Length > 1000 Then
                    lblEmpSpecialMentionsModelError.Text = "Particulars exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMParticulars.Focus()
                    Exit Sub
                End If
                If txtSMDealtWith.Text.Length > 1000 Then
                    lblEmpSpecialMentionsModelError.Text = "How Dealt Wit exceeded maximum size(max 1000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                    txtSMDealtWith.Focus()
                    Exit Sub
                End If

                objSpecialMention.iSUS_PKID = Val(lblSMPKID.Text)
                objSpecialMention.iSUS_UserEmpID = ddlExistingEmployee.SelectedValue
                objSpecialMention.sSUS_SpecialMention = objclsGRACeGeneral.SafeSQL(txtSMSpecialMention.Text.Trim())
                objSpecialMention.dSUS_Date = Date.ParseExact(Trim(txtSMDate.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objSpecialMention.sSUS_Particulars = objclsGRACeGeneral.SafeSQL(txtSMParticulars.Text.Trim())
                objSpecialMention.sSUS_DealtWith = objclsGRACeGeneral.SafeSQL(txtSMDealtWith.Text.Trim())
                objSpecialMention.iSUS_AttachID = 0
                objSpecialMention.iSUS_CrBy = sSession.UserID
                objSpecialMention.iSUS_UpdatedBy = sSession.UserID
                objSpecialMention.sSUS_IPAddress = sSession.IPAddress
                objSpecialMention.iSUS_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpSpecialMentionDetails(sSession.AccessCode, objSpecialMention)
                If iSMAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iSMAttachID, "SM")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblSMPKID.Text = Arr(1)
                lnkbtnHRDetails_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Special Mentions)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpSpecialMentionsModelError.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpSpecialMentionsModal').modal('show');", True)
                btnSMSave.Visible = False : btnSMUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnSMUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvSpecialMentions_PreRender(sender As Object, e As EventArgs) Handles gvSpecialMentions.PreRender
        Dim dt As New DataTable
        Try
            If gvSpecialMentions.Rows.Count > 0 Then
                gvSpecialMentions.UseAccessibleHeader = True
                gvSpecialMentions.HeaderRow.TableSection = TableRowSection.TableHeader
                gvSpecialMentions.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSpecialMentions_PreRender")
        End Try
    End Sub
    Private Sub gvSpecialMentions_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSpecialMentions.RowCommand
        Dim lblID As New Label, lblUserID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblUserID = DirectCast(clickedRow.FindControl("lblUserID"), Label)
                If lblID.Text > 0 And lblUserID.Text > 0 Then
                    dt = objclsEProfile.LoadEMPSpecialMention(sSession.AccessCode, sSession.AccessCodeID, lblUserID.Text, lblID.Text)
                    If dt.Rows.Count > 0 Then
                        lblSMPKID.Text = lblID.Text
                        If IsDBNull(dt.Rows(0)("SpecialMentions")) = False Then
                            txtSMSpecialMention.Text = dt.Rows(0).Item("SpecialMentions")
                        End If
                        If IsDBNull(dt.Rows(0)("SMDate")) = False Then
                            txtSMDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("SMDate"), "D")
                        End If
                        If IsDBNull(dt.Rows(0)("SMParticulars")) = False Then
                            txtSMParticulars.Text = dt.Rows(0).Item("SMParticulars")
                        End If
                        If IsDBNull(dt.Rows(0)("SMHowDealtWith")) = False Then
                            txtSMDealtWith.Text = dt.Rows(0).Item("SMHowDealtWith")
                        End If
                        btnSMSave.Visible = False : btnSMUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnSMUpdate.Visible = True
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpSpecialMentionsModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSpecialMentions_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Transfers Within The Firm
    Private Sub gvTransferswithintheFirm_PreRender(sender As Object, e As EventArgs) Handles gvTransferswithintheFirm.PreRender
        Dim dt As New DataTable
        Try
            If gvTransferswithintheFirm.Rows.Count > 0 Then
                gvTransferswithintheFirm.UseAccessibleHeader = True
                gvTransferswithintheFirm.HeaderRow.TableSection = TableRowSection.TableHeader
                gvTransferswithintheFirm.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTransferswithintheFirm_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvTransferswithintheFirm_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTransferswithintheFirm.RowCommand
        Dim dtTransferFirmDetails As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblTFPKID = DirectCast(clickedRow.FindControl("lblTFPKID"), Label)
                If ddlExistingEmployee.SelectedIndex > 0 Then
                    dtTransferFirmDetails = objclsEProfile.LoadEmpTransferFirmDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, lblTFPKID.Text)
                    If dtTransferFirmDetails.Rows.Count > 0 Then
                        btnTFSave.Visible = False : btnTFUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnTFUpdate.Visible = True
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("EarlierPrinciple")) = False Then
                            txtTFEarlierPrinciple.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtTransferFirmDetails.Rows(0).Item("EarlierPrinciple").ToString())
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("NewPrinciple")) = False Then
                            txtTENewPrinciple.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtTransferFirmDetails.Rows(0).Item("NewPrinciple").ToString())
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("DateofTransfer")) = False Then
                            txtTFDateTransfer.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtTransferFirmDetails.Rows(0).Item("DateofTransfer"), "D")
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("DurationWithNewPrinciple")) = False Then
                            txtTFDurationArticle.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtTransferFirmDetails.Rows(0).Item("DurationWithNewPrinciple").ToString())
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("CompletionDate")) = False Then
                            txtTFCompletionDate.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtTransferFirmDetails.Rows(0).Item("CompletionDate"), "D")
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("ExtendedTo")) = False Then
                            txtTFExtendedTo.Text = objclsGRACeGeneral.FormatDtForRDBMS(dtTransferFirmDetails.Rows(0).Item("ExtendedTo"), "D")
                        End If
                        If IsDBNull(dtTransferFirmDetails.Rows(0).Item("Remarks")) = False Then
                            txtTFRemarks.Text = objclsGRACeGeneral.ReplaceSafeSQL(dtTransferFirmDetails.Rows(0).Item("Remarks").ToString())
                        End If

                        gvTransferswithintheFirm.DataSource = dtTransferFirmDetails
                        gvTransferswithintheFirm.DataBind()
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTransferswithintheFirm_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnTFNew_Click(sender As Object, e As EventArgs) Handles btnTFNew.Click
        Try
            btnTFSave.Visible = False : btnTFUpdate.Visible = False
            If (sStatus <> "D") Then
                btnTFSave.Visible = True
            End If
            lblEmpTransferFirmModelError.Text = "" : txtTFEarlierPrinciple.Text = "" : txtTENewPrinciple.Text = "" : txtTFDateTransfer.Text = ""
            txtTFDurationArticle.Text = "" : txtTFCompletionDate.Text = "" : txtTFExtendedTo.Text = "" : txtTFRemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
        Catch ex As Exception
            lblEmpProfessionalExperienceModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnTFNew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnTFCancel_Click(sender As Object, e As EventArgs) Handles btnTFCancel.Click
        Try
            btnTFSave.Visible = False : btnTFUpdate.Visible = False
            If (sStatus <> "D") Then
                btnTFSave.Visible = True
            End If
            lblEmpTransferFirmModelError.Text = "" : txtTFEarlierPrinciple.Text = "" : txtTENewPrinciple.Text = "" : txtTFDateTransfer.Text = ""
            txtTFDurationArticle.Text = "" : txtTFCompletionDate.Text = "" : txtTFExtendedTo.Text = "" : txtTFRemarks.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('hide');", True)
        Catch ex As Exception
            lblEmpProfessionalExperienceModelError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnTFCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnTFSave_Click(sender As Object, e As EventArgs) Handles btnTFSave.Click
        Dim Arr() As String
        Dim dDFDate As Date, dDCDate As Date, dTFDate As Date, dTEDate As Date
        Try
            lblError.Text = "" : lblEmpTransferFirmModelError.Text = ""
            If txtTFEarlierPrinciple.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Earlier Principal."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFEarlierPrinciple.Focus()
                Exit Sub
            End If
            If txtTFEarlierPrinciple.Text.Trim.Length > 1000 Then
                lblEmpTransferFirmModelError.Text = "Earlier Principal exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFEarlierPrinciple.Focus()
                Exit Sub
            End If
            If txtTENewPrinciple.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter New Principal."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTENewPrinciple.Focus()
                Exit Sub
            End If
            If txtTENewPrinciple.Text.Trim.Length > 1000 Then
                lblEmpTransferFirmModelError.Text = "New Principal exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTENewPrinciple.Focus()
                Exit Sub
            End If
            If txtTFDateTransfer.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Date of Transfer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFDateTransfer.Focus()
                Exit Sub
            End If
            If txtTFDurationArticle.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Duration With New Principal."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFDurationArticle.Focus()
                Exit Sub
            End If
            If txtTFDurationArticle.Text.Trim.Length > 1000 Then
                lblEmpTransferFirmModelError.Text = "Duration With New Principal exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFDurationArticle.Focus()
                Exit Sub
            End If
            If txtTFCompletionDate.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Completion Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFCompletionDate.Focus()
                Exit Sub
            End If
            If txtTFCompletionDate.Text <> "" Then
                Dim dCompletionDate As Date
                Try
                    dCompletionDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpTransferFirmModelError.Text = "Enter valid Completion Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                    txtTFCompletionDate.Focus()
                    Exit Sub
                End Try
            End If
            dDFDate = DateTime.ParseExact(txtTFDateTransfer.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dDCDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dDFDate > dDCDate Then
                txtTFCompletionDate.Focus()
                'lblEmpParticularsofArticlesModelError.Text = "Date of Transfer (" & txtTFDateTransfer.Text & ") should be greater than or equal to Completion Date (" & txtTFCompletionDate.Text & ")."
                'lblError.Text = "Date of Transfer (" & txtTFDateTransfer.Text & ") should be greater than or equal to Period of Completion Date (" & txtTFCompletionDate.Text & ")."
                lblEmpTransferFirmModelError.Text = "Completion Date (" & txtTFCompletionDate.Text & ") should be greater than or equal to Date of Transfer (" & txtTFDateTransfer.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                Exit Sub
            End If
            If txtTFExtendedTo.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Extended To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFExtendedTo.Focus()
                Exit Sub
            End If
            If txtTFExtendedTo.Text <> "" Then
                Dim dCompletionDate As Date
                Try
                    dCompletionDate = DateTime.ParseExact(txtTFExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpTransferFirmModelError.Text = "Enter valid Extended To."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                    txtTFExtendedTo.Focus()
                    Exit Sub
                End Try
            End If
            dTFDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dTEDate = DateTime.ParseExact(txtTFExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dTFDate > dTEDate Then
                txtTFExtendedTo.Focus()
                lblEmpTransferFirmModelError.Text = "Extended To (" & txtTFExtendedTo.Text & ") should be greater than or equal to Completion Date (" & txtTFCompletionDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                Exit Sub
            End If
            If txtTFRemarks.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Remarks."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFRemarks.Focus()
                Exit Sub
            End If
            If txtTFRemarks.Text.Trim.Length > 8000 Then
                lblEmpTransferFirmModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFRemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUTFPKID = 0
            objclsEProfile.iSUTFUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.sSUTFEarlierPrinciple = objclsGRACeGeneral.SafeSQL(txtTFEarlierPrinciple.Text)
            objclsEProfile.sSUTFNewPrinciple = objclsGRACeGeneral.SafeSQL(txtTENewPrinciple.Text)
            objclsEProfile.dSUTFDateofTransfer = DateTime.ParseExact(txtTFDateTransfer.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.sSUTFDurationWithNewPrinciple = objclsGRACeGeneral.SafeSQL(txtTFDurationArticle.Text)
            objclsEProfile.dSUTFCompletionDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.dSUTFExtendedTo = DateTime.ParseExact(txtTFExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.sSUTFRemarks = objclsGRACeGeneral.SafeSQL(txtTFRemarks.Text)
            objclsEProfile.iSUTFAttachID = iEmpTFAttachID
            objclsEProfile.iSUTFCrBy = sSession.UserID
            objclsEProfile.iSUTFUpdatedBy = sSession.UserID
            objclsEProfile.sSUTFIPAddress = sSession.IPAddress
            objclsEProfile.iSUTFCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpTransferFirmDetails(sSession.AccessCode, objclsEProfile)
            If iEmpTFAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpTFAttachID, "TF")
            End If
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            lblTFPKID.Text = Arr(1)
            lnkbtnArticleClerck_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Transfers Within The Firm)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
            lblEmpTransferFirmModelError.Text = "Successfully Saved."
            btnTFSave.Visible = False : btnTFUpdate.Visible = True
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnTFSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub btnTFUpdate_Click(sender As Object, e As EventArgs) Handles btnTFUpdate.Click
        Dim Arr() As String
        Dim dDFDate As Date, dDCDate As Date, dTFDate As Date, dTEDate As Date
        Try
            lblError.Text = "" : lblEmpTransferFirmModelError.Text = ""
            If txtTFEarlierPrinciple.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Earlier Principal."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFEarlierPrinciple.Focus()
                Exit Sub
            End If
            If txtTFEarlierPrinciple.Text.Trim.Length > 1000 Then
                lblEmpTransferFirmModelError.Text = "Earlier Principal exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFEarlierPrinciple.Focus()
                Exit Sub
            End If
            If txtTENewPrinciple.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter New Principal."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTENewPrinciple.Focus()
                Exit Sub
            End If
            If txtTENewPrinciple.Text.Trim.Length > 1000 Then
                lblEmpTransferFirmModelError.Text = "New Principal exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTENewPrinciple.Focus()
                Exit Sub
            End If
            If txtTFDateTransfer.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Date of Transfer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFDateTransfer.Focus()
                Exit Sub
            End If
            If txtTFDurationArticle.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Duration With New Principal."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFDurationArticle.Focus()
                Exit Sub
            End If
            If txtTFDurationArticle.Text.Trim.Length > 1000 Then
                lblEmpTransferFirmModelError.Text = "Duration With New Principal exceeded maximum size(max 1000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFDurationArticle.Focus()
                Exit Sub
            End If
            If txtTFCompletionDate.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Completion Date."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFCompletionDate.Focus()
                Exit Sub
            End If
            If txtTFCompletionDate.Text <> "" Then
                Dim dCompletionDate As Date
                Try
                    dCompletionDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpTransferFirmModelError.Text = "Enter valid Completion Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                    txtTFCompletionDate.Focus()
                    Exit Sub
                End Try
            End If
            dDFDate = DateTime.ParseExact(txtTFDateTransfer.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dDCDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dDFDate > dDCDate Then
                txtTFCompletionDate.Focus()
                'lblEmpParticularsofArticlesModelError.Text = "Date of Transfer (" & txtTFDateTransfer.Text & ") should be greater than or equal to Completion Date (" & txtTFCompletionDate.Text & ")."
                'lblError.Text = "Date of Transfer (" & txtTFDateTransfer.Text & ") should be greater than or equal to Period of Completion Date (" & txtTFCompletionDate.Text & ")."
                lblEmpTransferFirmModelError.Text = "Completion Date (" & txtTFCompletionDate.Text & ") should be greater than or equal to Date of Transfer (" & txtTFDateTransfer.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                Exit Sub
            End If
            If txtTFExtendedTo.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Extended To."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFExtendedTo.Focus()
                Exit Sub
            End If
            If txtTFExtendedTo.Text <> "" Then
                Dim dCompletionDate As Date
                Try
                    dCompletionDate = DateTime.ParseExact(txtTFExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                Catch ex As Exception
                    lblEmpTransferFirmModelError.Text = "Enter valid Extended To."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                    txtTFExtendedTo.Focus()
                    Exit Sub
                End Try
            End If
            dTFDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            dTEDate = DateTime.ParseExact(txtTFExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            If dTFDate > dTEDate Then
                txtTFExtendedTo.Focus()
                lblEmpTransferFirmModelError.Text = "Extended To (" & txtTFExtendedTo.Text & ") should be greater than or equal to Completion Date (" & txtTFCompletionDate.Text & ")."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                Exit Sub
            End If
            If txtTFRemarks.Text = "" Then
                lblEmpTransferFirmModelError.Text = "Enter Remarks."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFRemarks.Focus()
                Exit Sub
            End If
            If txtTFRemarks.Text.Trim.Length > 8000 Then
                lblEmpTransferFirmModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
                txtTFRemarks.Focus()
                Exit Sub
            End If
            objclsEProfile.iSUTFPKID = Val(lblTFPKID.Text)
            objclsEProfile.iSUTFUserEmpID = ddlExistingEmployee.SelectedValue
            objclsEProfile.sSUTFEarlierPrinciple = objclsGRACeGeneral.SafeSQL(txtTFEarlierPrinciple.Text)
            objclsEProfile.sSUTFNewPrinciple = objclsGRACeGeneral.SafeSQL(txtTENewPrinciple.Text)
            objclsEProfile.dSUTFDateofTransfer = DateTime.ParseExact(txtTFDateTransfer.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.sSUTFDurationWithNewPrinciple = objclsGRACeGeneral.SafeSQL(txtTFDurationArticle.Text)
            objclsEProfile.dSUTFCompletionDate = DateTime.ParseExact(txtTFCompletionDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.dSUTFExtendedTo = DateTime.ParseExact(txtTFExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
            objclsEProfile.sSUTFRemarks = objclsGRACeGeneral.SafeSQL(txtTFRemarks.Text)
            objclsEProfile.iSUTFAttachID = iEmpTFAttachID
            objclsEProfile.iSUTFCrBy = sSession.UserID
            objclsEProfile.iSUTFUpdatedBy = sSession.UserID
            objclsEProfile.sSUTFIPAddress = sSession.IPAddress
            objclsEProfile.iSUTFCompId = sSession.AccessCodeID
            Arr = objclsEProfile.SaveEmpTransferFirmDetails(sSession.AccessCode, objclsEProfile)
            If iEmpTFAttachID > 0 Then
                objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iEmpTFAttachID, "TF")
            End If
            lblTFPKID.Text = Arr(1)
            lnkbtnArticleClerck_Click(sender, e)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Transfers Within The Firm)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpTransferswithintheFirmModal').modal('show');", True)
            lblEmpTransferFirmModelError.Text = "Successfully Updated."
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnTFUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddTFAttach_Click(sender As Object, e As EventArgs) Handles btnAddTFAttach.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblTFMsg.Text = "" : iEmpTFDocID = 0
            If Not (txtTFfile.PostedFile Is Nothing) And txtTFfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtTFfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblTFMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalProfessionalExperienceAttchment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtTFfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtTFfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iEmpTFAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iEmpTFAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpTFAttachID, lblTF.Text)
                    If iEmpTFAttachID > 0 Then
                        sArticleAttachIDs = iPOAAttachID & "," & iEmpTFAttachID
                        BindAllArticleDetailsAttachments(sArticleAttachIDs)
                    End If
                Else
                    lblTFMsg.Text = "No file to Attach."
                End If
            Else
                lblTFMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalTransferFirmAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddTFAttach_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvTransferFirmAttach_PreRender(sender As Object, e As EventArgs) Handles gvTransferFirmAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvTransferFirmAttach.Rows.Count > 0 Then
                gvTransferFirmAttach.UseAccessibleHeader = True
                gvTransferFirmAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvTransferFirmAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTransferFirmAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvTransferFirmAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvTransferFirmAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvTransferFirmAttach.Columns(4).Visible = True
                'Else
                '    gvTransferFirmAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTransferFirmAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvTransferFirmAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvTransferFirmAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblPEMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpTFDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpTFAttachID, iEmpTFDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEmpTFDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpTFAttachID, iEmpTFDocID)
                BindAllArticleDetailsAttachments(sArticleAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalTransferFirmAttchment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvTransferFirmAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Particulars of Articles
    Private Sub btnPOANew_Click(sender As Object, e As EventArgs) Handles btnPOANew.Click
        Try
            lblError.Text = "" : lblEmpParticularsofArticlesModelError.Text = "" : txtPOAPrincipleName.Text = "" : txtPOAArticleRegNo.Text = ""
            txtPOAPracticeNo.Text = "" : txtPOAArticlesFrom.Text = "" : txtPOAArticlesTo.Text = "" : txtPOAArticlesExtendedTo.Text = "" : txtPOARemarks.Text = ""
            btnPOASave.Visible = False : btnPOAUpdate.Visible = False
            If (sStatus <> "D") Then
                btnPOASave.Visible = True
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPOANew_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnPOACancel_Click(sender As Object, e As EventArgs) Handles btnPOACancel.Click
        Try
            lblError.Text = "" : lblEmpParticularsofArticlesModelError.Text = "" : txtPOAPrincipleName.Text = "" : txtPOAArticleRegNo.Text = ""
            txtPOAPracticeNo.Text = "" : txtPOAArticlesFrom.Text = "" : txtPOAArticlesTo.Text = "" : txtPOAArticlesExtendedTo.Text = "" : txtPOARemarks.Text = ""
            btnPOASave.Visible = False : btnPOAUpdate.Visible = False
            If (sStatus <> "D") Then
                btnPOASave.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPOACancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnPOASave_Click(sender As Object, e As EventArgs) Handles btnPOASave.Click
        Dim objParticularsofArticles As New strUserEmp_ParticularsofArticlesDetails
        Dim Arr() As String
        Dim dFromDate As Date, dToDate As Date, dExToDate As Date, dSSDate As Date, dSCDate As Date, dSDate As Date, dEDate As Date
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtPOAPrincipleName.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Name of the Principal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPrincipleName.Focus()
                    Exit Sub
                End If
                If txtPOAArticleRegNo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Article Registration No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticleRegNo.Focus()
                    Exit Sub
                End If
                If txtPOAPracticeNo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Certificate of Practice No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPracticeNo.Focus()
                    Exit Sub
                End If
                If txtPOAArticlesFrom.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Period of Articles From Date."
                    txtPOAArticlesFrom.Focus()
                    Exit Sub
                Else
                    Try
                        dFromDate = DateTime.ParseExact(txtPOAArticlesFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpParticularsofArticlesModelError.Text = "Enter valid Period of Articles From Date." : lblError.Text = "Enter valid Period of Articles From Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                        txtPOAArticlesFrom.Focus()
                        Exit Sub
                    End Try
                End If

                If txtPOAArticlesTo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Period of Articles To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticlesTo.Focus()
                    Exit Sub
                Else
                    Try
                        dToDate = DateTime.ParseExact(txtPOAArticlesTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpParticularsofArticlesModelError.Text = "Enter valid Period of Articles To Date." : lblError.Text = "Enter valid Period of Articles To Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                        txtPOAArticlesTo.Focus()
                        Exit Sub
                    End Try
                End If

                dSSDate = DateTime.ParseExact(txtPOAArticlesFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSCDate = DateTime.ParseExact(txtPOAArticlesTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If dSSDate > dSCDate Then
                    txtPOAArticlesTo.Focus()
                    lblEmpParticularsofArticlesModelError.Text = "Period of Articles To Date (" & txtPOAArticlesTo.Text & ") should be greater than or equal to Period of Articles From Date (" & txtPOAArticlesFrom.Text & ")."
                    lblError.Text = "Period of Articles To Date (" & txtPOAArticlesTo.Text & ") should be greater than or equal to Period of Articles From Date (" & txtPOAArticlesFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    Exit Sub
                End If

                If txtPOAArticlesExtendedTo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Period of Articles Extended To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticlesExtendedTo.Focus()
                    Exit Sub
                Else
                    Try
                        dExToDate = DateTime.ParseExact(txtPOAArticlesExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpParticularsofArticlesModelError.Text = "Enter valid Period of Articles Extended To Date." : lblError.Text = "Enter valid Period of Articles Extended To Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                        txtPOAArticlesExtendedTo.Focus()
                        Exit Sub
                    End Try
                End If


                dSDate = DateTime.ParseExact(txtPOAArticlesTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dEDate = DateTime.ParseExact(txtPOAArticlesExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If dSDate > dEDate Then
                    txtPOAArticlesTo.Focus()
                    lblEmpParticularsofArticlesModelError.Text = "Period of Articles Extended To Date (" & txtPOAArticlesExtendedTo.Text & ") should be greater than or equal to Period of Articles To Date (" & txtPOAArticlesTo.Text & ")."
                    lblError.Text = "Period of Articles Extended To Date (" & txtPOAArticlesExtendedTo.Text & ") should be greater than or equal to Period of Articles To Date (" & txtPOAArticlesTo.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    Exit Sub
                End If

                If txtPOAPrincipleName.Text.Length > 100 Then
                    lblEmpParticularsofArticlesModelError.Text = "Name of the Principal exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPrincipleName.Focus()
                    Exit Sub
                End If
                If txtPOAArticleRegNo.Text.Length > 100 Then
                    lblEmpParticularsofArticlesModelError.Text = "Article Registration No exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticleRegNo.Focus()
                    Exit Sub
                End If
                If txtPOAPracticeNo.Text.Length > 100 Then
                    lblEmpParticularsofArticlesModelError.Text = "Certificate of Practice No exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPracticeNo.Focus()
                    Exit Sub
                End If
                If txtPOARemarks.Text.Length > 8000 Then
                    lblEmpParticularsofArticlesModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOARemarks.Focus()
                    Exit Sub
                End If
                objParticularsofArticles.iSUP_PKID = 0
                objParticularsofArticles.iSUP_UserEmpID = ddlExistingEmployee.SelectedValue
                objParticularsofArticles.sSUP_PrincipleName = objclsGRACeGeneral.SafeSQL(txtPOAPrincipleName.Text.Trim())
                objParticularsofArticles.sSUP_RegistrationNo = objclsGRACeGeneral.SafeSQL(txtPOAArticleRegNo.Text.Trim())
                objParticularsofArticles.sSUP_PracticeNo = objclsGRACeGeneral.SafeSQL(txtPOAPracticeNo.Text.Trim())
                objParticularsofArticles.dSUP_ArticlesFrom = Date.ParseExact(Trim(txtPOAArticlesFrom.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objParticularsofArticles.dSUP_ArticlesTo = Date.ParseExact(Trim(txtPOAArticlesTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objParticularsofArticles.dSUP_ExtendedTo = Date.ParseExact(Trim(txtPOAArticlesExtendedTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objParticularsofArticles.sSUP_Remarks = objclsGRACeGeneral.SafeSQL(txtPOARemarks.Text.Trim())
                objParticularsofArticles.iSUP_AttachID = 0
                objParticularsofArticles.iSUP_CrBy = sSession.UserID
                objParticularsofArticles.iSUP_UpdatedBy = sSession.UserID
                objParticularsofArticles.sSUP_IPAddress = sSession.IPAddress
                objParticularsofArticles.iSUP_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpParticularsofArticlesDetails(sSession.AccessCode, objParticularsofArticles)
                If iPOAAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iPOAAttachID, "POA")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblPOAPKID.Text = Arr(1)
                lnkbtnArticleClerck_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Particulars of Articles)", "Saved", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpParticularsofArticlesModelError.Text = "Successfully Saved." : lblError.Text = "Successfully Saved."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                btnPOASave.Visible = False : btnPOAUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPOASave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnPOAUpdate_Click(sender As Object, e As EventArgs) Handles btnPOAUpdate.Click
        Dim objParticularsofArticles As New strUserEmp_ParticularsofArticlesDetails
        Dim Arr() As String
        Dim dFromDate As Date, dToDate As Date, dExToDate As Date, dSSDate As Date, dSCDate As Date, dSDate As Date, dEDate As Date
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex > 0 Then
                If txtPOAPrincipleName.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Name of the Principal."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPrincipleName.Focus()
                    Exit Sub
                End If
                If txtPOAArticleRegNo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Article Registration No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticleRegNo.Focus()
                    Exit Sub
                End If
                If txtPOAPracticeNo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Certificate of Practice No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPracticeNo.Focus()
                    Exit Sub
                End If
                If txtPOAArticlesFrom.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Period of Articles From Date."
                    txtPOAArticlesFrom.Focus()
                    Exit Sub
                Else
                    Try
                        dFromDate = DateTime.ParseExact(txtPOAArticlesFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpParticularsofArticlesModelError.Text = "Enter valid Period of Articles From Date." : lblError.Text = "Enter valid Period of Articles From Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                        txtPOAArticlesFrom.Focus()
                        Exit Sub
                    End Try
                End If

                If txtPOAArticlesTo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Period of Articles To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticlesTo.Focus()
                    Exit Sub
                Else
                    Try
                        dToDate = DateTime.ParseExact(txtPOAArticlesTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpParticularsofArticlesModelError.Text = "Enter valid Period of Articles To Date." : lblError.Text = "Enter valid Period of Articles To Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                        txtPOAArticlesTo.Focus()
                        Exit Sub
                    End Try
                End If

                dSSDate = DateTime.ParseExact(txtPOAArticlesFrom.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dSCDate = DateTime.ParseExact(txtPOAArticlesTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If dSSDate > dSCDate Then
                    txtPOAArticlesTo.Focus()
                    lblEmpParticularsofArticlesModelError.Text = "Period of Articles To Date (" & txtPOAArticlesTo.Text & ") should be greater than or equal to Period of Articles From Date (" & txtPOAArticlesFrom.Text & ")."
                    lblError.Text = "Period of Articles To Date (" & txtPOAArticlesTo.Text & ") should be greater than or equal to Period of Articles From Date (" & txtPOAArticlesFrom.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    Exit Sub
                End If

                If txtPOAArticlesExtendedTo.Text = "" Then
                    lblEmpParticularsofArticlesModelError.Text = "Enter Period of Articles Extended To Date."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticlesExtendedTo.Focus()
                    Exit Sub
                Else
                    Try
                        dExToDate = DateTime.ParseExact(txtPOAArticlesExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    Catch ex As Exception
                        lblEmpParticularsofArticlesModelError.Text = "Enter valid Period of Articles Extended To Date." : lblError.Text = "Enter valid Period of Articles Extended To Date."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                        txtPOAArticlesExtendedTo.Focus()
                        Exit Sub
                    End Try
                End If


                dSDate = DateTime.ParseExact(txtPOAArticlesTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                dEDate = DateTime.ParseExact(txtPOAArticlesExtendedTo.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                If dSDate > dEDate Then
                    txtPOAArticlesTo.Focus()
                    lblEmpParticularsofArticlesModelError.Text = "Period of Articles Extended To Date (" & txtPOAArticlesExtendedTo.Text & ") should be greater than or equal to Period of Articles To Date (" & txtPOAArticlesTo.Text & ")."
                    lblError.Text = "Period of Articles Extended To Date (" & txtPOAArticlesExtendedTo.Text & ") should be greater than or equal to Period of Articles To Date (" & txtPOAArticlesTo.Text & ")."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    Exit Sub
                End If

                If txtPOAPrincipleName.Text.Length > 100 Then
                    lblEmpParticularsofArticlesModelError.Text = "Name of the Principal exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPrincipleName.Focus()
                    Exit Sub
                End If
                If txtPOAArticleRegNo.Text.Length > 100 Then
                    lblEmpParticularsofArticlesModelError.Text = "Article Registration No exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAArticleRegNo.Focus()
                    Exit Sub
                End If
                If txtPOAPracticeNo.Text.Length > 100 Then
                    lblEmpParticularsofArticlesModelError.Text = "Certificate of Practice No exceeded maximum size(max 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOAPracticeNo.Focus()
                    Exit Sub
                End If
                If txtPOARemarks.Text.Length > 8000 Then
                    lblEmpParticularsofArticlesModelError.Text = "Remarks exceeded maximum size(max 8000 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                    txtPOARemarks.Focus()
                    Exit Sub
                End If
                objParticularsofArticles.iSUP_PKID = Val(lblPOAPKID.Text)
                objParticularsofArticles.iSUP_UserEmpID = ddlExistingEmployee.SelectedValue
                objParticularsofArticles.sSUP_PrincipleName = objclsGRACeGeneral.SafeSQL(txtPOAPrincipleName.Text.Trim())
                objParticularsofArticles.sSUP_RegistrationNo = objclsGRACeGeneral.SafeSQL(txtPOAArticleRegNo.Text.Trim())
                objParticularsofArticles.sSUP_PracticeNo = objclsGRACeGeneral.SafeSQL(txtPOAPracticeNo.Text.Trim())
                objParticularsofArticles.dSUP_ArticlesFrom = Date.ParseExact(Trim(txtPOAArticlesFrom.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objParticularsofArticles.dSUP_ArticlesTo = Date.ParseExact(Trim(txtPOAArticlesTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objParticularsofArticles.dSUP_ExtendedTo = Date.ParseExact(Trim(txtPOAArticlesExtendedTo.Text), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentCulture)
                objParticularsofArticles.sSUP_Remarks = objclsGRACeGeneral.SafeSQL(txtPOARemarks.Text.Trim())
                objParticularsofArticles.iSUP_AttachID = 0
                objParticularsofArticles.iSUP_CrBy = sSession.UserID
                objParticularsofArticles.iSUP_UpdatedBy = sSession.UserID
                objParticularsofArticles.sSUP_IPAddress = sSession.IPAddress
                objParticularsofArticles.iSUP_CompID = sSession.AccessCodeID
                Arr = objclsEProfile.SaveEmpParticularsofArticlesDetails(sSession.AccessCode, objParticularsofArticles)
                If iPOAAttachID > 0 Then
                    objclsEProfile.UpdateAttachID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, iPOAAttachID, "POA")
                End If
                ddlExistingEmployee_SelectedIndexChanged(sender, e)
                lblPOAPKID.Text = Arr(1)
                lnkbtnArticleClerck_Click(sender, e)
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile(Particulars of Articles)", "Updated", Arr(1), ddlExistingEmployee.SelectedItem.Text, 0, "", sSession.IPAddress)
                lblEmpParticularsofArticlesModelError.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#myEmpParticularsofArticlesModal').modal('show');", True)
                btnPOASave.Visible = False : btnPOAUpdate.Visible = True
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPOAUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvParticularsofArticles_PreRender(sender As Object, e As EventArgs) Handles gvParticularsofArticles.PreRender
        Dim dt As New DataTable
        Try
            If gvParticularsofArticles.Rows.Count > 0 Then
                gvParticularsofArticles.UseAccessibleHeader = True
                gvParticularsofArticles.HeaderRow.TableSection = TableRowSection.TableHeader
                gvParticularsofArticles.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvParticularsofArticles_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvParticularsofArticles_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvParticularsofArticles.RowCommand
        Dim lblID As New Label, lblUserID As New Label
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
            If e.CommandName = "Select" Then
                lblID = DirectCast(clickedRow.FindControl("lblID"), Label)
                lblUserID = DirectCast(clickedRow.FindControl("lblUserID"), Label)
                If lblID.Text > 0 And lblUserID.Text > 0 Then
                    dt = objclsEProfile.LoadEMPParticularsofArticles(sSession.AccessCode, sSession.AccessCodeID, lblUserID.Text, lblID.Text)
                    If dt.Rows.Count > 0 Then
                        lblPOAPKID.Text = lblID.Text
                        If IsDBNull(dt.Rows(0)("NameOfThePrinciple")) = False Then
                            txtPOAPrincipleName.Text = dt.Rows(0).Item("NameOfThePrinciple")
                        End If
                        If IsDBNull(dt.Rows(0)("ArticleRegistrationNo")) = False Then
                            txtPOAArticleRegNo.Text = dt.Rows(0).Item("ArticleRegistrationNo")
                        End If
                        If IsDBNull(dt.Rows(0)("CertificateOfParticleNo")) = False Then
                            txtPOAPracticeNo.Text = dt.Rows(0).Item("CertificateOfParticleNo")
                        End If
                        If IsDBNull(dt.Rows(0)("PeriodOfArticlesFrom")) = False Then
                            txtPOAArticlesFrom.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("PeriodOfArticlesFrom"), "D")
                        End If
                        If IsDBNull(dt.Rows(0)("PeriodOfArticlesTo")) = False Then
                            txtPOAArticlesTo.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("PeriodOfArticlesTo"), "D")
                        End If
                        If IsDBNull(dt.Rows(0)("PeriodOfArticlesExtendedTo")) = False Then
                            txtPOAArticlesExtendedTo.Text = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(0).Item("PeriodOfArticlesExtendedTo"), "D")
                        End If
                        If IsDBNull(dt.Rows(0)("PeriodOfArticlesRemarks")) = False Then
                            txtPOARemarks.Text = dt.Rows(0).Item("PeriodOfArticlesRemarks")
                        End If
                        btnPOASave.Visible = False : btnPOAUpdate.Visible = False
                        If (sStatus <> "D") Then
                            btnPOAUpdate.Visible = True
                        End If
                    End If
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myEmpParticularsofArticlesModal').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvParticularsofArticles_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    'Others
    Private Sub BindAllEmpDetailsAttachments(ByVal sEmpDetailsAttachIDs As String)
        Dim ds As New DataSet
        Try
            If sEmpDetailsAttachIDs <> "" Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, sEmpDetailsAttachIDs)
                gvEmpDetailsAttach.DataSource = ds
                gvEmpDetailsAttach.DataBind()
            End If
            If iEQAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iEQAttachID)
                gvEQAttach.DataSource = ds
                gvEQAttach.DataBind()
            End If
            lblEQBadgeCount.Text = gvEQAttach.Rows.Count
            If iECSAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iECSAttachID)
                gvECSAttach.DataSource = ds
                gvECSAttach.DataBind()
            End If
            lblECSBadgeCount.Text = gvECSAttach.Rows.Count
            If iEmpPEAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iEmpPEAttachID)
                gvProfessionalExperienceAttach.DataSource = ds
                gvProfessionalExperienceAttach.DataBind()
            End If
            lblBadgePECount.Text = gvProfessionalExperienceAttach.Rows.Count
            If iEmpALAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iEmpALAttachID)
                gvAsstesLoanAttach.DataSource = ds
                gvAsstesLoanAttach.DataBind()
            End If
            lblBadgeALCount.Text = gvAsstesLoanAttach.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllEmpDetailsAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub btnAddEQAttch_Click(sender As Object, e As EventArgs) Handles btnAddEQAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblEQMsg.Text = "" : iEQDocID = 0
            If Not (txtEQfile.PostedFile Is Nothing) And txtEQfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtEQfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblEQMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalEQAttachment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtEQfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtEQfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iEQAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iEQAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEQAttachID, lblEQ.Text)
                    If iEQAttachID > 0 Then
                        sEmpDetailsAttachIDs = iEQAttachID & "," & iECSAttachID
                        BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
                    End If
                Else
                    lblEQMsg.Text = "No file to Attach."
                End If
            Else
                lblEQMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalEQAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddEQAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmpDetailsAttach_PreRender(sender As Object, e As EventArgs) Handles gvEmpDetailsAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvEmpDetailsAttach.Rows.Count > 0 Then
                gvEmpDetailsAttach.UseAccessibleHeader = True
                gvEmpDetailsAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvEmpDetailsAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmpDetailsAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmpDetailsAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvEmpDetailsAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvEmpDetailsAttach.Columns(4).Visible = True
                'Else
                '    gvEmpDetailsAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmpDetailsAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEmpDetailsAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEmpDetailsAttach.RowCommand
        Dim sPaths As String, sDestEQFilePath As String = "", sDestESCFilePath As String = "", sDestPEFilePath As String = "", sDestALFilePath As String = ""
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iECSDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestEQFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEQAttachID, iECSDocID)
                sDestESCFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iECSAttachID, iECSDocID)
                sDestPEFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpPEAttachID, iECSDocID)
                sDestALFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpALAttachID, iECSDocID)
                If sDestEQFilePath <> "" Then
                    DownloadMyFile(sDestEQFilePath)
                ElseIf sDestESCFilePath <> "" Then
                    DownloadMyFile(sDestESCFilePath)
                ElseIf sDestPEFilePath <> "" Then
                    DownloadMyFile(sDestPEFilePath)
                ElseIf sDestALFilePath <> "" Then
                    DownloadMyFile(sDestALFilePath)
                End If
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEQDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEQAttachID, iEQDocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iECSAttachID, iEQDocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpPEAttachID, iEQDocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpALAttachID, iEQDocID)
                BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEmpDetailsAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEQAttach_PreRender(sender As Object, e As EventArgs) Handles gvEQAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvEQAttach.Rows.Count > 0 Then
                gvEQAttach.UseAccessibleHeader = True
                gvEQAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvEQAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEQAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEQAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvEQAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvEQAttach.Columns(4).Visible = True
                'Else
                '    gvEQAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEQAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvEQAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvEQAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEQDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEQAttachID, iEQDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iEQDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEQAttachID, iEQDocID)
                BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalEQAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvEQAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvECSAttach_PreRender(sender As Object, e As EventArgs) Handles gvECSAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvECSAttach.Rows.Count > 0 Then
                gvECSAttach.UseAccessibleHeader = True
                gvECSAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvECSAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvECSAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvECSAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvECSAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvECSAttach.Columns(4).Visible = True
                'Else
                '    gvECSAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvECSAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvECSAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvECSAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iECSDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iECSAttachID, iECSDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iECSDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iECSAttachID, iECSDocID)
                BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalECSAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvECSAttach_ItemCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddECSAttch_Click(sender As Object, e As EventArgs) Handles btnAddECSAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblECSMsg.Text = "" : iECSDocID = 0
            If Not (txtECSfile.PostedFile Is Nothing) And txtECSfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtECSfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblECSMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalECSAttachment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtECSfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtECSfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iECSAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iECSAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iECSAttachID, lblECS.Text)
                    If iECSAttachID > 0 Then
                        sEmpDetailsAttachIDs = iEQAttachID & "," & iECSAttachID
                        BindAllEmpDetailsAttachments(sEmpDetailsAttachIDs)
                    End If
                Else
                    lblECSMsg.Text = "No file to Attach."
                End If
            Else
                lblECSMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalECSAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddECSAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub BindAllHRDetailsAttachments(ByVal sHRAttachIDs As String)
        Dim ds As New DataSet
        Try
            If sHRAttachIDs <> "" Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, sHRAttachIDs)
                gvHRDetailsAttach.DataSource = ds
                gvHRDetailsAttach.DataBind()
            End If
            If iPAAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iPAAttachID)
                gvPAAttach.DataSource = ds
                gvPAAttach.DataBind()
            End If
            lblPABadgeCount.Text = gvPAAttach.Rows.Count
            If iEmpAPAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iEmpAPAttachID)
                gvAcademicProgressAttach.DataSource = ds
                gvAcademicProgressAttach.DataBind()
            End If
            lblBadgeAPCount.Text = gvAcademicProgressAttach.Rows.Count
            If iSMAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iSMAttachID)
                gvSMAttach.DataSource = ds
                gvSMAttach.DataBind()
            End If
            lblSMBadgeCount.Text = gvSMAttach.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllHRDetailsAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub gvHRDetailsAttach_PreRender(sender As Object, e As EventArgs) Handles gvHRDetailsAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvHRDetailsAttach.Rows.Count > 0 Then
                gvHRDetailsAttach.UseAccessibleHeader = True
                gvHRDetailsAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvHRDetailsAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvHRDetailsAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvHRDetailsAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvHRDetailsAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvHRDetailsAttach.Columns(4).Visible = True
                'Else
                '    gvHRDetailsAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvHRDetailsAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvHRDetailsAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvHRDetailsAttach.RowCommand
        Dim sPaths As String, sDestPAFilePath As String = "", sDestAPFilePath As String = "", sDestSMFilePath As String = ""
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPADocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestPAFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iPAAttachID, iPADocID)
                sDestAPFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpAPAttachID, iPADocID)
                sDestSMFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iSMAttachID, iPADocID)
                If sDestPAFilePath <> "" Then
                    DownloadMyFile(sDestPAFilePath)
                ElseIf sDestAPFilePath <> "" Then
                    DownloadMyFile(sDestAPFilePath)
                ElseIf sDestSMFilePath <> "" Then
                    DownloadMyFile(sDestSMFilePath)
                End If
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPADocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iPAAttachID, iPADocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpAPAttachID, iPADocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iSMAttachID, iPADocID)
                BindAllHRDetailsAttachments(sHRAttachIDs)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvHRDetailsAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPAAttach_PreRender(sender As Object, e As EventArgs) Handles gvPAAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvPAAttach.Rows.Count > 0 Then
                gvPAAttach.UseAccessibleHeader = True
                gvPAAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPAAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPAAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPAAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPAAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvPAAttach.Columns(4).Visible = True
                'Else
                '    gvPAAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPAAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPAAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPAAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPADocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iPAAttachID, iPADocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPADocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iPAAttachID, iPADocID)
                BindAllHRDetailsAttachments(sHRAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPAAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPAAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvSMAttach_PreRender(sender As Object, e As EventArgs) Handles gvSMAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvSMAttach.Rows.Count > 0 Then
                gvSMAttach.UseAccessibleHeader = True
                gvSMAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvSMAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSMAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvSMAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvSMAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvSMAttach.Columns(4).Visible = True
                'Else
                '    gvSMAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSMAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvSMAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvSMAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iSMDocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iSMAttachID, iSMDocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iSMDocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iSMAttachID, iSMDocID)
                BindAllHRDetailsAttachments(sHRAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSMAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvSMAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddPAAttch_Click(sender As Object, e As EventArgs) Handles btnAddPAAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblPAMsg.Text = "" : iPADocID = 0
            If Not (txtPAfile.PostedFile Is Nothing) And txtPAfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtPAfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblPAMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPAAttachment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtPAfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtPAfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iPAAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iPAAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iPAAttachID, lblPA.Text)
                    If iPAAttachID > 0 Then
                        sHRAttachIDs = iPAAttachID & "," & iEmpAPAttachID & "," & iSMAttachID
                        BindAllHRDetailsAttachments(sHRAttachIDs)
                    End If
                Else
                    lblPAMsg.Text = "No file to Attach."
                End If
            Else
                lblPAMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPAAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddPAAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddSMAttch_Click(sender As Object, e As EventArgs) Handles btnAddSMAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblSMMsg.Text = "" : iSMDocID = 0
            If Not (txtSMfile.PostedFile Is Nothing) And txtSMfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtSMfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblSMMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSMAttachment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtSMfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtSMfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iSMAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iSMAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iSMAttachID, lblSM.Text)
                    If iSMAttachID > 0 Then
                        sHRAttachIDs = iPAAttachID & "," & iEmpAPAttachID & "," & iSMAttachID
                        BindAllHRDetailsAttachments(sHRAttachIDs)
                    End If
                Else
                    lblSMMsg.Text = "No file to Attach."
                End If
            Else
                lblSMMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSMAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddSMAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub BindAllArticleDetailsAttachments(ByVal sArticleAttachID As String)
        Dim ds As New DataSet
        Try
            If sArticleAttachID <> "" Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, sArticleAttachID)
                gvArticleAttach.DataSource = ds
                gvArticleAttach.DataBind()
            End If
            If iEmpTFAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iEmpTFAttachID)
                gvTransferFirmAttach.DataSource = ds
                gvTransferFirmAttach.DataBind()
            End If
            lblBadgeTFCount.Text = gvTransferFirmAttach.Rows.Count
            If iPOAAttachID > 0 Then
                ds = objclsEProfile.LoadEmpDetAttachments(103, sSession.AccessCode, sSession.AccessCodeID, iPOAAttachID)
                gvPOAAttach.DataSource = ds
                gvPOAAttach.DataBind()
            End If
            lblPOABadgeCount.Text = gvPOAAttach.Rows.Count
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "BindAllArticleDetailsAttachments" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Sub
    Private Sub gvArticleAttach_PreRender(sender As Object, e As EventArgs) Handles gvArticleAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvArticleAttach.Rows.Count > 0 Then
                gvArticleAttach.UseAccessibleHeader = True
                gvArticleAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvArticleAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvArticleAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvArticleAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvArticleAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvArticleAttach.Columns(4).Visible = True
                'Else
                '    gvArticleAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvArticleAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvArticleAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvArticleAttach.RowCommand
        Dim sPaths As String, sDestTFFilePath As String, sDestPOAFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPOADocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestTFFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iEmpTFAttachID, iPOADocID)
                sDestPOAFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iPOAAttachID, iPOADocID)
                If sDestTFFilePath <> "" Then
                    DownloadMyFile(sDestTFFilePath)
                ElseIf sDestPOAFilePath <> "" Then
                    DownloadMyFile(sDestPOAFilePath)
                End If
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPOADocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iEmpTFAttachID, iPOADocID)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iPOAAttachID, iPOADocID)
                BindAllArticleDetailsAttachments(sArticleAttachIDs)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvArticleAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPOAAttach_PreRender(sender As Object, e As EventArgs) Handles gvPOAAttach.PreRender
        Dim dt As New DataTable
        Try
            If gvPOAAttach.Rows.Count > 0 Then
                gvPOAAttach.UseAccessibleHeader = True
                gvPOAAttach.HeaderRow.TableSection = TableRowSection.TableHeader
                gvPOAAttach.FooterRow.TableSection = TableRowSection.TableFooter
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPOAAttach_PreRender" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPOAAttach_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvPOAAttach.RowDataBound
        Dim lblStatus As New Label
        Dim imgbtnRemove As New ImageButton
        Try
            lblError.Text = ""
            If e.Row.RowType = DataControlRowType.DataRow Then
                imgbtnRemove = CType(e.Row.FindControl("imgbtnRemove"), ImageButton)
                imgbtnRemove.ImageUrl = "~/Images/Trash16.png"
                'If sEPSave = "YES" Then
                gvPOAAttach.Columns(4).Visible = True
                'Else
                '    gvPOAAttach.Columns(4).Visible = False
                'End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPOAAttach_RowDataBound" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub gvPOAAttach_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvPOAAttach.RowCommand
        Dim sPaths As String, sDestFilePath As String
        Dim lblAtchDocID As New Label, lblFDescription As New Label
        Try
            lblError.Text = "" : lblEQMsg.Text = ""
            If e.CommandName = "OPENPAGE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, LinkButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPOADocID = Val(lblAtchDocID.Text)
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
                sDestFilePath = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, iPOAAttachID, iPOADocID)
                DownloadMyFile(sDestFilePath)
            End If
            If e.CommandName = "REMOVE" Then
                Dim clickedRow As GridViewRow = TryCast(DirectCast(e.CommandSource, ImageButton).NamingContainer, GridViewRow)
                lblAtchDocID = DirectCast(clickedRow.FindControl("lblAtchDocID"), Label)
                iPOADocID = Val(lblAtchDocID.Text)
                objclsAttachments.RemoveSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iPOAAttachID, iPOADocID)
                BindAllArticleDetailsAttachments(sArticleAttachIDs)
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPOAAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "gvPOAAttach_RowCommand" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Sub btnAddPOAAttch_Click(sender As Object, e As EventArgs) Handles btnAddPOAAttch.Click
        Dim sPaths As String, sFullFilePath As String
        Dim sFilesNames As String
        Dim lSize As Long
        Try
            lblError.Text = "" : lblPOAMsg.Text = "" : iPOADocID = 0
            If Not (txtPOAfile.PostedFile Is Nothing) And txtPOAfile.PostedFile.ContentLength > 0 Then
                lSize = CType(txtPOAfile.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblPOAMsg.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPOAAttachment').modal('show');", True)
                    Exit Sub
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(txtPOAfile.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                txtPOAfile.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iPOAAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iPOAAttachID)
                    objclsEProfile.UpdateDescSelectedDoc(sSession.AccessCode, sSession.AccessCodeID, iPOAAttachID, lblPOA.Text)
                    If iPOAAttachID > 0 Then
                        sArticleAttachIDs = iPOAAttachID & "," & iEmpTFAttachID
                        BindAllArticleDetailsAttachments(sArticleAttachIDs)
                    End If
                Else
                    lblPOAMsg.Text = "No file to Attach."
                End If
            Else
                lblPOAMsg.Text = "No file to Attach."
            End If
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPOAAttachment').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnAddPOAAttch_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlZone_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlZone.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            If ddlZone.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlZone.SelectedValue)
                ddlRegion.DataSource = dt
                ddlRegion.DataTextField = "Org_Name"
                ddlRegion.DataValueField = "org_node"
                ddlRegion.DataBind()
                ddlRegion.Items.Insert(0, "Select Region")
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(ddlZone.SelectedValue, 0, 0, 0, "")
                End If
            Else
                If chkChangeLevel.Checked = False Then
                    ClearAll()
                    BindExistingEmployeeDB(0, 0, 0, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlZone_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnAdd_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnAdd.Click
        Try
            lblError.Text = "" : sEPFlag = "" : chkChangeLevel.Checked = False : chkChangeLevel.Visible = False : lblChangeLevel.Visible = False
            txtLoginName.Text = "" : txtEmployeeName.Text = ""
            txtSAPCode.Text = objclsEProfile.GetMaxEmployeeCode(sSession.AccessCode, sSession.AccessCodeID)
            ddlExistingEmployee.SelectedIndex = 0 : ddlZone.SelectedIndex = 0 : ddlRegion.Items.Clear() : ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            ddlDesignation.SelectedIndex = 0 : ddlRole.SelectedIndex = 0 : ddlGroup.SelectedIndex = 0 : ddlPermission.SelectedIndex = 0
            txtEmail.Text = "" : txtOffice.Text = "" : txtMobile.Text = "" : txtResidence.Text = ""
            txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
            txtPassword.Text = "" : txtConfirmPassword.Text = ""
            txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
            chkIsPartner.Checked = False : chkSendMail.Checked = False
            'pnlsignature.Visible = False
            RetrieveSignatureUpload.ImageUrl = ""
            ClearAll() : ClearAttachDetails()
            lnkbtnEmpBasicDetails_Click(sender, e)
            imgbtnSave.Visible = False : imgbtnUpdate.Visible = False
            'If sEPSave = "YES" Then
            imgbtnSave.Visible = True
            'End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAdd_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlRegion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRegion.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlArea.Items.Clear() : ddlBranch.Items.Clear()
            If ddlRegion.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlRegion.SelectedValue)
                ddlArea.DataSource = dt
                ddlArea.DataTextField = "Org_Name"
                ddlArea.DataValueField = "org_node"
                ddlArea.DataBind()
                ddlArea.Items.Insert(0, "Select Area")
                If chkChangeLevel.Checked = False Then
                    ClearAllMaster()
                    BindExistingEmployeeDB(0, ddlRegion.SelectedValue, 0, 0, "")
                End If
            Else
                If chkChangeLevel.Checked = False Then
                    ClearAllMaster()
                    BindExistingEmployeeDB(ddlZone.SelectedValue, 0, 0, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlRegion_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlArea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlArea.SelectedIndexChanged
        Dim dt As New DataTable
        Try
            lblError.Text = ""
            ddlBranch.Items.Clear()
            If ddlArea.SelectedIndex > 0 Then
                dt = objclsEmployeeMaster.LoadRegioAreaBranchMaster(sSession.AccessCode, sSession.AccessCodeID, ddlArea.SelectedValue)
                ddlBranch.DataSource = dt
                ddlBranch.DataTextField = "Org_Name"
                ddlBranch.DataValueField = "org_node"
                ddlBranch.DataBind()
                ddlBranch.Items.Insert(0, "Select Branch")
                If chkChangeLevel.Checked = False Then
                    ClearAllMaster()
                    BindExistingEmployeeDB(0, 0, ddlArea.SelectedValue, 0, "")
                End If
            Else
                If chkChangeLevel.Checked = False Then
                    ClearAllMaster()
                    BindExistingEmployeeDB(0, ddlRegion.SelectedValue, 0, 0, "")
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlArea_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub ddlBranch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlBranch.SelectedIndexChanged
        Try
            lblError.Text = ""
            If chkChangeLevel.Checked = False Then
                If ddlBranch.SelectedIndex > 0 Then
                    If chkChangeLevel.Checked = False Then
                        ClearAllMaster()
                        BindExistingEmployeeDB(0, 0, 0, ddlBranch.SelectedValue, "")
                    End If
                Else
                    If chkChangeLevel.Checked = False Then
                        ClearAllMaster()
                        BindExistingEmployeeDB(0, 0, ddlArea.SelectedValue, 0, "")
                    End If
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "ddlBranch_SelectedIndexChanged" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnSave_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnSave.Click
        Dim Arr() As String
        Dim sChangedPwd As String, iIsPasswordReset As Integer = 0
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex = 0 Then
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, UCase(txtSAPCode.Text)) = True Then
                    lblEmpProfileValidationMsg.Text = "EMP Code already exist." : lblError.Text = "EMP Code already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtSAPCode.Focus()
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, UCase(txtLoginName.Text)) = True Then
                    lblEmpProfileValidationMsg.Text = "Login Name already exist." : lblError.Text = "Login Name already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtLoginName.Focus()
                    Exit Sub
                End If
            Else
                sChangedPwd = objclsGRACeGeneral.DecryptPassword(objclsEmployeeMaster.GetUesrPassword(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue))
                If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then '
                    lblEmpProfileValidationMsg.Text = "Entered Password and Confirm Password does not match." : lblError.Text = "Entered Password and Confirm Password does not match."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
                    txtPassword.Text = "" : txtConfirmPassword.Text = ""
                    txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
                    Exit Sub
                ElseIf ((Trim(txtPassword.Text) <> sChangedPwd) And Trim(txtConfirmPassword.Text) <> sChangedPwd) Then
                    iIsPasswordReset = 1
                End If
            End If
            If objclsEmployeeMaster.GetTRACeCustomerUserCount(sSession.AccessCode, sSession.AccessCodeID) >= sSession.NumberOfUsers Then
                lblEmpProfileValidationMsg.Text = "User/Employee limit exceeded in TRACe application. Please contact Administrator." : lblError.Text = "User/Employee limit exceeded in TRACe application. Please contact Administrator."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                Exit Sub
            End If
            If txtSAPCode.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter EMP Code." : lblError.Text = "Enter EMP Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If
            If txtSAPCode.Text.Trim.Length > 10 Then
                lblEmpProfileValidationMsg.Text = "EMP Code exceeded maximum size(max 10 characters)." : lblError.Text = "EMP Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If

            If txtEmployeeName.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Employee Name." : lblError.Text = "Enter Employee Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If
            If txtEmployeeName.Text.Trim.Length > 50 Then
                lblEmpProfileValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters)." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If

            If txtLoginName.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Login Name." : lblError.Text = "Enter Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If
            If txtLoginName.Text.Trim.Length > 25 Then
                lblEmpProfileValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters)." : lblError.Text = "Login Name exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Password." : lblError.Text = "Enter Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtPassword.Focus()
                Exit Sub
            End If

            If txtConfirmPassword.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Confirm Password." : lblError.Text = "Enter Confirm Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                lblEmpProfileValidationMsg.Text = " Entered Password & Confirm Password doesn't match." : lblError.Text = " Entered Password & Confirm Password doesn't match."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtEmail.Text.Trim.Length > 50 Then
                lblEmpProfileValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters)." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtEmail.Focus()
                Exit Sub
            End If

            If txtOffice.Text.Trim.Length > 15 Then
                lblEmpProfileValidationMsg.Text = "Office Phone No. exceeded maximum size(max 20 numbers)." : lblError.Text = "Office Phone No. exceeded maximum size(max 20 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtOffice.Focus()
                Exit Sub
            End If

            If txtMobile.Text.Trim <> "" Then
                If txtMobile.Text.Trim.Length > 10 Then
                    lblEmpProfileValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers).'" : lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If

                If txtMobile.Text.Trim.Length <> 10 Then
                    lblEmpProfileValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If
            End If

            If txtResidence.Text.Trim.Length > 15 Then
                lblEmpProfileValidationMsg.Text = "Residence No. exceeded maximum size(max 15 numbers)." : lblError.Text = "Residence No. exceeded maximum size(max 15 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtResidence.Focus()
                Exit Sub
            End If

            If ddlDesignation.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Designation." : lblError.Text = "Select Designation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                ddlDesignation.Focus()
                Exit Sub
            End If

            If ddlRole.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Role." : lblError.Text = "Select Role."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                ddlRole.Focus()
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Module." : lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                ddlGroup.Focus()
                Exit Sub
            End If

            If iIsPasswordReset = 1 Then
                objclsEmployeeMaster.UpdatePasswordReset(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 1)
            End If
            Arr = SaveEmployeeBasicDetails()
            Dim sPassword As String = txtPassword.Text
            BindExistingEmployeeDB(iZoneID, iRegionID, iAreaID, iBranchID, "")
            ddlExistingEmployee.SelectedValue = Arr(1)
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            If Arr(0) = "3" Then
                If chkSendMail.Checked = True Then
                    Dim myMail As New System.Web.Mail.MailMessage()
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "10.1.28.84")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
                    ''myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "karthikprasad@mmcspl.com")
                    ''myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "9738860458@Raje")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
                    'myMail.From = "tracepwdrecovery@bandhanbank.com"
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "Trace@mmcspl.com")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "Trjune@23")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
                    myMail.From = "Trace@mmcspl.com"

                    myMail.Bcc = txtEmail.Text.Trim ' To email id
                    myMail.Subject = "Trace user created" & txtEmployeeName.Text.Trim & "-" & txtLoginName.Text.Trim
                    myMail.BodyFormat = MailFormat.Html
                    myMail.Body = "Dear   " & "  " & txtEmployeeName.Text.Trim & ",Welcome to TRACE pa! User has been successfully created, Here are some key details of the user : </b>' <br> <br>" &
                    " User name:" & txtEmployeeName.Text.Trim & "<br>" &
                    " Login name:" & txtLoginName.Text.Trim & "<br>" &
                    " Password:" & sPassword & "<br>" &
                    "<p style='color:red'>Please note that For security reasons, it Is recommended that to change your password As soon As possible after logging In"
                    myMail.BodyEncoding = System.Text.Encoding.UTF8
                    System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
                    System.Web.Mail.SmtpMail.Send(myMail)
                End If
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Saved", Arr(1), txtEmployeeName.Text.Trim, 0, "", sSession.IPAddress)
                lblEmpProfileValidationMsg.Text = "Successfully Saved & Waiting for Approval." : lblError.Text = "Successfully Saved & Waiting for Approval."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpProfileValidation').modal('show');", True)
                sEMDBackStatus = 4
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSave_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Private Function SaveEmployeeBasicDetails() As Array
        Dim Arr() As String ', Arr1() As String
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer
        Dim lSize As Long
        Dim sPaths As String, sFullFilePath As String, sFilesNames As String, sBloodGroup As String = ""
        Try
            If ddlZone.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlZone.SelectedValue
                objclsEmployeeMaster.iUsrNode = 1
                iZoneID = ddlZone.SelectedValue
            End If

            If ddlRegion.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlRegion.SelectedValue
                objclsEmployeeMaster.iUsrNode = 2
                iZoneID = 0
                iRegionID = ddlRegion.SelectedValue
            End If

            If ddlArea.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlArea.SelectedValue
                objclsEmployeeMaster.iUsrNode = 3
                iZoneID = 0 : iRegionID = 0
                iAreaID = ddlArea.SelectedValue
            End If

            If ddlBranch.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUsrOrgID = ddlBranch.SelectedValue
                objclsEmployeeMaster.iUsrNode = 4
                iZoneID = 0 : iRegionID = 0 : iAreaID = 0
                iBranchID = ddlBranch.SelectedValue
            End If

            If ddlExistingEmployee.SelectedIndex > 0 Then
                objclsEmployeeMaster.iUserID = ddlExistingEmployee.SelectedValue
                objclsEmployeeMaster.sUsrStatus = "U"
            Else
                objclsEmployeeMaster.iUserID = 0
                objclsEmployeeMaster.sUsrStatus = "C"
            End If
            If chkSendMail.Checked = True Then
                objclsEmployeeMaster.iUsrSentMail = 1
            Else
                objclsEmployeeMaster.iUsrSentMail = 0
            End If
            objclsEmployeeMaster.sUsrCode = objclsGRACeGeneral.SafeSQL(txtSAPCode.Text.Trim)
            objclsEmployeeMaster.sUsrFullName = objclsGRACeGeneral.SafeSQL(txtEmployeeName.Text.Trim)
            objclsEmployeeMaster.sUsrLoginName = objclsGRACeGeneral.SafeSQL(txtLoginName.Text.Trim)
            objclsEmployeeMaster.sUsrPassword = objclsGRACeGeneral.EncryptPassword(txtPassword.Text)
            objclsEmployeeMaster.sUsrEmail = objclsGRACeGeneral.SafeSQL(txtEmail.Text.Trim)

            objclsEmployeeMaster.sUsrDutyStatus = "W"
            objclsEmployeeMaster.sUsrType = "U"
            objclsEmployeeMaster.sUsrPhoneNo = objclsGRACeGeneral.SafeSQL(txtResidence.Text.Trim)
            objclsEmployeeMaster.sUsrMobileNo = objclsGRACeGeneral.SafeSQL(txtMobile.Text.Trim)
            objclsEmployeeMaster.sUsrOfficePhone = objclsGRACeGeneral.SafeSQL(txtOffice.Text.Trim)
            objclsEmployeeMaster.sUsrOffPhExtn = ""
            objclsEmployeeMaster.iUsrDesignation = ddlDesignation.SelectedValue
            objclsEmployeeMaster.iUsrCompanyID = 0
            objclsEmployeeMaster.iUsrRole = ddlRole.SelectedValue
            objclsEmployeeMaster.iUsrLevelGrp = ddlGroup.SelectedIndex
            objclsEmployeeMaster.iUsrGrpOrUserLvlPerm = ddlPermission.SelectedIndex
            objclsEmployeeMaster.sUsrFlag = "W"
            objclsEmployeeMaster.iUsrCompID = sSession.AccessCodeID
            objclsEmployeeMaster.iUsrCreatedBy = sSession.UserID
            objclsEmployeeMaster.sUsrIPAdress = sSession.IPAddress
            objclsEmployeeMaster.iUsrMasterModule = 0 : objclsEmployeeMaster.iUsrAuditModule = 0
            objclsEmployeeMaster.iUsrRiskModule = 0 : objclsEmployeeMaster.iUsrComplianceModule = 0
            objclsEmployeeMaster.iUsrBCMmodule = 0

            objclsEmployeeMaster.iUsrMasterRole = 0 : objclsEmployeeMaster.iUsrAuditRole = 0
            objclsEmployeeMaster.iUsrRiskRole = 0 : objclsEmployeeMaster.iUsrComplianceRole = 0
            objclsEmployeeMaster.iUsrBCMRole = 0
            If ddlGroup.SelectedIndex = 1 Then 'Master
                objclsEmployeeMaster.iUsrMasterModule = 1
                objclsEmployeeMaster.iUsrMasterRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 2 Then 'Audit
                objclsEmployeeMaster.iUsrAuditModule = 1
                objclsEmployeeMaster.iUsrAuditRole = ddlRole.SelectedValue
            ElseIf ddlGroup.SelectedIndex = 3 Then 'Risk
                objclsEmployeeMaster.iUsrRiskModule = 1
                objclsEmployeeMaster.iUsrRiskRole = ddlRole.SelectedValue
            End If
            If chkIsPartner.Checked = False Then
                objclsEmployeeMaster.iUsrPartner = 0
            Else
                objclsEmployeeMaster.iUsrPartner = 1
            End If
            Arr = objclsEmployeeMaster.SaveEmployeeDetails(sSession.AccessCode, objclsEmployeeMaster)

            'If Not (fuSignatureUpload1.PostedFile Is Nothing) And fuSignatureUpload1.PostedFile.ContentLength > 0 Then
            '    lSize = CType(fuSignatureUpload1.PostedFile.ContentLength, Integer)
            '    If (sSession.FileSize * 1024 * 1024) < lSize Then
            '        lblError.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
            '        Exit Function
            '    End If
            '    sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

            '    If sPaths.EndsWith("\") = True Then
            '        sPaths = sPaths & "Uploads\"
            '    Else
            '        sPaths = sPaths & "\Uploads\"
            '    End If
            '    objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
            '    objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
            '    sFilesNames = System.IO.Path.GetFileName(fuSignatureUpload1.PostedFile.FileName)
            '    sFullFilePath = sPaths & sFilesNames
            '    fuSignatureUpload1.PostedFile.SaveAs(sFullFilePath)
            '    If System.IO.File.Exists(sFullFilePath) = True Then
            '        iSignatureID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
            '        If iSignatureID > 0 Then
            '            BindAllAttachments(sSession.AccessCode, iSignatureID)
            '        End If
            '    End If
            'End If

            'If iSignatureID = 0 Then
            '    iSignatureID = objclsEmployeeMaster.GetPhotoSignatureID(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, "Signature")
            'End If

            'objclsEmployeeMaster.iusrSignature = iSignatureID
            'objclsEmployeeMaster.UpdateEmployeeSign(sSession.AccessCode, sSession.AccessCodeID, Arr(1), iSignatureID)

            'SaveEmployeeSign(Arr(1))
            '        Dim sfile As Boolean
            '        sfilename = Path.GetFileName(txtfile1.PostedFile.FileName)
            '        Dim sSize As Integer = txtfile1.PostedFile.ContentLength
            '        Dim fileExtension As String = UCase(Path.GetExtension(txtfile1.FileName))
            '        Dim sFilePath As String = objclsEmployeeMaster.GetImagePath(sSession.AccessCode)

            '        If sfilename <> "" Then

            '            objclsEmployeeMaster.iEmp_Id = 0
            '            objclsEmployeeMaster.iEmp_MasterID = Arr(1)
            '            objclsEmployeeMaster.iEmp_FileSize = sSize
            '            objclsEmployeeMaster.sEmp_FileName = sfilename
            '            objclsEmployeeMaster.sEmp_SignatureExt = fileExtension
            '            objclsEmployeeMaster.sEmp_Delflag = "A"
            '            objclsEmployeeMaster.sEmp_IPAddress = sSession.IPAddress
            '            objclsEmployeeMaster.iEmp_CompId = sSession.AccessCodeID
            '            Arr1 = objclsEmployeeMaster.SaveEmployeeSign(sSession.AccessCode, objclsEmployeeMaster)

            '            sFilePath = sFilePath & "\EmpSignature\" & Arr(1) \ 301 & "\"
            '            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sFilePath)
            '            sFilePath = sFilePath & fileExtension   'Actual File Name

            'End If

            If Not (fuSignatureUpload.PostedFile Is Nothing) And fuSignatureUpload.PostedFile.ContentLength > 0 Then
                lSize = CType(fuSignatureUpload.PostedFile.ContentLength, Integer)
                If (sSession.FileSize * 1024 * 1024) < lSize Then
                    lblError.Text = "File size exceeded maximum size(max " & ((lSize / 1024) / 1024) & " MB)."
                    Exit Function
                End If
                sPaths = objclsGeneralFunctions.CreateWorkingDir(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)

                If sPaths.EndsWith("\") = True Then
                    sPaths = sPaths & "Uploads\"
                Else
                    sPaths = sPaths & "\Uploads\"
                End If
                objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sPaths)
                objclsGeneralFunctions.ClearBrowseDirectory(sPaths)
                sFilesNames = System.IO.Path.GetFileName(fuSignatureUpload.PostedFile.FileName)
                sFullFilePath = sPaths & sFilesNames
                fuSignatureUpload.PostedFile.SaveAs(sFullFilePath)
                If System.IO.File.Exists(sFullFilePath) = True Then
                    iSignatureAttachID = objclsAttachments.SaveAttachments(sSession.AccessCode, sSession.AccessCodeID, sFullFilePath, sSession.UserID, iAttachID)
                    If iSignatureAttachID > 0 Then
                        BindAllAttachments(sSession.AccessCode, iSignatureAttachID)
                    End If
                End If
            End If

            If iSignatureAttachID = 0 Then
                iSignatureAttachID = objclsEmployeeMaster.GetPhotoSignatureID(sSession.AccessCode, sSession.AccessCodeID, Arr(1), "Signature")
            End If
            'objclsEmployeeMaster.iusrSignature = iSignatureAttachID
            objclsEmployeeMaster.UpdateEmployeeSign(sSession.AccessCode, sSession.AccessCodeID, Arr(1), iSignatureAttachID)

            Return Arr
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeBasicDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
            'Throw
        End Try
    End Function

    'Public Sub SaveEmployeeSign(ByVal id As Integer)
    '    Dim sFilePath As String, sFilesNames As String, sFileName As String, sISDB As String, sPath As String = ""
    '    Dim Arr() As String
    '    Dim i As Integer = 0
    '    Dim Extension As String

    '    Try
    '        lblError.Text = ""
    '        Dim hfc As HttpFileCollection = Request.Files
    '        If hfc.Count > 0 Then
    '            Dim hpf As HttpPostedFile = hfc(i)
    '            If hpf.ContentLength > 0 Then
    '                sFilesNames = System.IO.Path.GetFileName(hpf.FileName)
    '                hpf.SaveAs(Server.MapPath(".") & "\Images\" & sFilesNames)

    '                sFilePath = Server.MapPath(".") & "\Images\" & sFilesNames
    '                sFileName = System.IO.Path.GetFileNameWithoutExtension(hpf.FileName)
    '                Extension = System.IO.Path.GetExtension(hpf.FileName)

    '                sISDB = objclsEmployeeMaster.ISFileinDB(sSession.AccessCode, sSession.AccessCodeID)
    '                EmployeeSign(id, sFilePath, UCase(sISDB))
    '            End If
    '        End If

    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "SaveEmployeeSign")
    '        'Throw
    '    End Try
    'End Sub
    'Public Function EmployeeSign(ByVal ID As Long, ByVal sFilePath As String, ByVal sFileInDB As String) As Boolean
    '    Dim sImagePath As String
    '    Dim sExt As String

    '    Try
    '        sExt = System.IO.Path.GetExtension(sFilePath)
    '        If sFileInDB = "FALSE" Then
    '            sImagePath = objclsEmployeeMaster.GetImagePath(sSession.AccessCode)
    '            sImagePath = sImagePath & "\Signature\" & ID \ 301 & "\"
    '            objclsGeneralFunctions.CheckAndCreateWorkingDirFromPath(sSession.AccessCode, sImagePath)
    '            sImagePath = sImagePath & ID & sExt   'Actual File Name
    '            If System.IO.File.Exists(sImagePath) = False Then
    '                FileCopy(sFilePath, sImagePath)
    '                EmployeeSign = True
    '            End If
    '            gtViewer(ID, sImagePath)
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Function
    'Public Sub gtViewer(ByVal iID As Integer, ByVal sImagePaths As String)
    '    Dim sTempFilePath As String, sImagePath As String = ""
    '    Try
    '        sImagePath = Server.MapPath("~/Images/" + sImagePaths)

    '        If File.Exists(sImagePath) = True Then
    '            sTempFilePath = ConfigurationManager.AppSettings("VSPath") & "TempImage\"
    '            If Directory.Exists(sTempFilePath) = False Then
    '                Directory.CreateDirectory(sTempFilePath)
    '            End If
    '            sTempFilePath = sTempFilePath & sSession.UserID & "_" & Path.GetFileName(sImagePath)


    '            If File.Exists(sTempFilePath) = True Then
    '                Try
    '                    File.Delete(sTempFilePath)
    '                    File.Copy(sImagePath, sTempFilePath)
    '                Catch ex As Exception
    '                End Try
    '            Else
    '                File.Copy(sImagePath, sTempFilePath)
    '            End If

    '            Dim bytes As Byte() = System.IO.File.ReadAllBytes(sTempFilePath)
    '            Dim imageBase64Data As String = Convert.ToBase64String(bytes)
    '            Dim imageDataURL1 As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
    '            ImgSignature.ImageUrl = imageDataURL1

    '        End If
    '    Catch ex As Exception
    '        lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Vintaviewer")
    '    End Try
    'End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim Arr() As String
        Dim sChangedPwd As String, iIsPasswordReset As Integer = 0
        Dim iZoneID As Integer, iRegionID As Integer, iAreaID As Integer, iBranchID As Integer
        Try
            lblError.Text = ""
            If ddlExistingEmployee.SelectedIndex = 0 Then
                If objclsEmployeeMaster.CheckSAPCode(sSession.AccessCode, sSession.AccessCodeID, txtSAPCode.Text) = True Then
                    lblEmpProfileValidationMsg.Text = "EMP Code already exist." : lblError.Text = "EMP Code already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtSAPCode.Focus()
                    Exit Sub
                End If
                If objclsEmployeeMaster.CheckForLoginName(sSession.AccessCode, sSession.AccessCodeID, txtLoginName.Text) = True Then
                    lblEmpProfileValidationMsg.Text = "Login Name already exist." : lblError.Text = "Login Name already exist."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtLoginName.Focus()
                    Exit Sub
                End If
            Else
                sChangedPwd = objclsGRACeGeneral.DecryptPassword(objclsEmployeeMaster.GetUesrPassword(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue))
                If Trim(txtPassword.Text) <> Trim(txtConfirmPassword.Text) Then '
                    lblEmpProfileValidationMsg.Text = "Entered Password and Confirm Password does not match." : lblError.Text = "Entered Password and Confirm Password does not match."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtPassword.Text = String.Empty : txtConfirmPassword.Text = String.Empty
                    txtPassword.Text = "" : txtConfirmPassword.Text = ""
                    txtPassword.Attributes("value") = "" : txtConfirmPassword.Attributes("value") = ""
                    Exit Sub
                ElseIf ((Trim(txtPassword.Text) <> sChangedPwd) And Trim(txtConfirmPassword.Text) <> sChangedPwd) Then
                    iIsPasswordReset = 1
                End If
            End If

            If txtSAPCode.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter EMP Code." : lblError.Text = "Enter EMP Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If
            If txtSAPCode.Text.Trim.Length > 10 Then
                lblEmpProfileValidationMsg.Text = "EMP Code exceeded maximum size(max 10 characters)." : lblError.Text = "EMP Code exceeded maximum size(max 10 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtSAPCode.Focus()
                Exit Sub
            End If

            If txtEmployeeName.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Employee Name." : lblError.Text = "Enter Employee Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If
            If txtEmployeeName.Text.Trim.Length > 50 Then
                lblEmpProfileValidationMsg.Text = "Employee Name exceeded maximum size(max 50 characters)." : lblError.Text = "Employee Name exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtEmployeeName.Focus()
                Exit Sub
            End If

            If txtLoginName.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Login Name." : lblError.Text = "Enter Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If
            If txtLoginName.Text.Trim.Length > 25 Then
                lblEmpProfileValidationMsg.Text = "Login Name exceeded maximum size(max 25 characters)." : lblError.Text = "Login Name exceeded maximum size(max 25 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtLoginName.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Password." : lblError.Text = "Enter Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtPassword.Focus()
                Exit Sub
            End If

            If txtConfirmPassword.Text.Trim = "" Then
                lblEmpProfileValidationMsg.Text = "Enter Confirm Password." : lblError.Text = "Enter Confirm Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalEmpProfileValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtPassword.Text.Trim <> txtConfirmPassword.Text.Trim Then
                lblEmpProfileValidationMsg.Text = " Entered Password & Confirm Password doesn't match." : lblError.Text = " Entered Password & Confirm Password doesn't match."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtConfirmPassword.Focus()
                Exit Sub
            End If

            If txtEmail.Text.Trim.Length > 50 Then
                lblEmpProfileValidationMsg.Text = "E-Mail exceeded maximum size(max 50 characters)." : lblError.Text = "E-Mail exceeded maximum size(max 50 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtEmail.Focus()
                Exit Sub
            End If

            If txtOffice.Text.Trim.Length > 15 Then
                lblEmpProfileValidationMsg.Text = "Office Phone No. exceeded maximum size(max 20 numbers)." : lblError.Text = "Office Phone No. exceeded maximum size(max 20 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtOffice.Focus()
                Exit Sub
            End If

            If txtMobile.Text.Trim <> "" Then
                If txtMobile.Text.Trim.Length > 10 Then
                    lblEmpProfileValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers).'" : lblError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If

                If txtMobile.Text.Trim.Length <> 10 Then
                    lblEmpProfileValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                    txtMobile.Focus()
                    Exit Sub
                End If
            End If

            If txtResidence.Text.Trim.Length > 15 Then
                lblEmpProfileValidationMsg.Text = "Residence No. exceeded maximum size(max 15 numbers)." : lblError.Text = "Residence No. exceeded maximum size(max 15 numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                txtResidence.Focus()
                Exit Sub
            End If

            If ddlDesignation.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Designation." : lblError.Text = "Select Designation."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                ddlDesignation.Focus()
                Exit Sub
            End If

            If ddlRole.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Role." : lblError.Text = "Select Role."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                ddlRole.Focus()
                Exit Sub
            End If

            If ddlGroup.SelectedIndex = 0 Then
                lblEmpProfileValidationMsg.Text = "Select Module." : lblError.Text = "Select Module."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalEmpProfileValidation').modal('show');", True)
                ddlGroup.Focus()
                Exit Sub
            End If

            If iIsPasswordReset = 1 Then
                objclsEmployeeMaster.UpdatePasswordReset(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 1)
            End If
            Arr = SaveEmployeeBasicDetails()
            Dim sPassword As String = txtPassword.Text
            BindExistingEmployeeDB(iZoneID, iRegionID, iAreaID, iBranchID, "")
            ddlExistingEmployee.SelectedValue = Arr(1)
            ddlExistingEmployee_SelectedIndexChanged(sender, e)
            If Arr(0) = "2" Then
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Employee Master", "Updated", Arr(1), txtEmployeeName.Text.Trim, 0, "", sSession.IPAddress)
                If chkSendMail.Checked = True Then
                    Dim myMail As New System.Web.Mail.MailMessage()
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "10.1.28.84")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "25")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
                    ''myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "karthikprasad@mmcspl.com")
                    ''myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "9738860458@Raje")
                    'myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
                    'myMail.From = "tracepwdrecovery@bandhanbank.com"
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", "smtp.gmail.com")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", "2")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "Trace@mmcspl.com")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "Trjune@23")
                    myMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true")
                    myMail.From = "Trace@mmcspl.com"

                    myMail.Bcc = txtEmail.Text.Trim ' To email id
                    myMail.Subject = "Trace user created" & txtEmployeeName.Text.Trim & "-" & txtLoginName.Text.Trim
                    myMail.BodyFormat = MailFormat.Html
                    myMail.Body = "Dear   " & "  " & txtEmployeeName.Text.Trim & ",Welcome to TRACEpa! User has been successfully created, and you're now part of our community. Here are some key details about your account: </b>' <br> <br>" &
                    " User name:" & txtEmployeeName.Text.Trim & "<br>" &
                    " Login name:" & txtLoginName.Text.Trim & "<br>" &
                    " Password:" & sPassword & "<br>" &
                    "<p style='color:red'>Please note that For security reasons, it Is recommended that to change your password As soon As possible after logging In"
                    myMail.BodyEncoding = System.Text.Encoding.UTF8
                    System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
                    System.Web.Mail.SmtpMail.Send(myMail)
                End If
                If sEPFlag = "W" Then
                    lblEmpProfileValidationMsg.Text = "Successfully Updated & Waiting for Approval." : lblError.Text = "Successfully Updated & Waiting for Approval."
                Else
                    lblEmpProfileValidationMsg.Text = "Successfully Updated." : lblError.Text = "Successfully Updated."
                End If
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalEmpProfileValidation').modal('show');", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Dim dtEBD As New DataTable, dt As New DataTable, dtEQ As New DataTable, dtECS As New DataTable, dtPE As New DataTable, dtAL As New DataTable
        Dim dtPA As New DataTable, dtAP As New DataTable, dtSM As New DataTable, dtTF As New DataTable, dtPOA As New DataTable

        Try
            If ddlExistingEmployee.SelectedIndex > 0 Then
                dtEBD = objclsEProfile.LoadAllEmpBasicDetailsToReport(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue)
                dt = objclsEProfile.LoadExistingEmployeeDetailsToReport(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue)
                dtEQ = objclsEProfile.LoadEMPQualification(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtECS = objclsEProfile.LoadEMPCourse(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtPE = objclsEProfile.LoadEmpProfessionalExperienceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtAL = objclsEProfile.LoadEmpAsstesLoanDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtPA = objclsEProfile.LoadEMPAssessment(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtAP = objclsEProfile.LoadEmpAcademicProgressDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtSM = objclsEProfile.LoadEMPSpecialMention(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtTF = objclsEProfile.LoadEmpTransferFirmDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtPOA = objclsEProfile.LoadEMPParticularsofArticles(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
            End If
            If ((dtEBD.Rows.Count = 0) Or (dt.Rows.Count = 0 And dtEQ.Rows.Count = 0 And dtECS.Rows.Count = 0 And dtPE.Rows.Count = 0 And dtAL.Rows.Count = 0 And dtPA.Rows.Count = 0 And dtAP.Rows.Count = 0 And dtSM.Rows.Count = 0 And dtTF.Rows.Count = 0 And dtPOA.Rows.Count = 0)) Then
                lblEmpProfileValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpProfileValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            Dim rdsEQ As New ReportDataSource("DataSet2", dtEQ)
            Dim rdsECS As New ReportDataSource("DataSet3", dtECS)
            Dim rdsPE As New ReportDataSource("DataSet4", dtPE)
            Dim rdsAL As New ReportDataSource("DataSet5", dtAL)
            Dim rdsPA As New ReportDataSource("DataSet6", dtPA)
            Dim rdsAP As New ReportDataSource("DataSet7", dtAP)
            Dim rdsSM As New ReportDataSource("DataSet8", dtSM)
            Dim rdsTF As New ReportDataSource("DataSet9", dtTF)
            Dim rdsPOA As New ReportDataSource("DataSet10", dtPOA)
            Dim rdsEBD As New ReportDataSource("DataSet11", dtEBD)

            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rdsEQ)
            ReportViewer1.LocalReport.DataSources.Add(rdsECS)
            ReportViewer1.LocalReport.DataSources.Add(rdsPE)
            ReportViewer1.LocalReport.DataSources.Add(rdsAL)
            ReportViewer1.LocalReport.DataSources.Add(rdsPA)
            ReportViewer1.LocalReport.DataSources.Add(rdsAP)
            ReportViewer1.LocalReport.DataSources.Add(rdsSM)
            ReportViewer1.LocalReport.DataSources.Add(rdsTF)
            ReportViewer1.LocalReport.DataSources.Add(rdsPOA)
            ReportViewer1.LocalReport.DataSources.Add(rdsEBD)

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/EProfileEmpMaster.rdlc")

            Dim EmployeeName As ReportParameter() = New ReportParameter() {New ReportParameter("EmployeeName", ddlExistingEmployee.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(EmployeeName)
            ReportViewer1.LocalReport.Refresh()

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=EProfile" + ".pdf")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnPDF_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnExcel_Click(sender As Object, e As EventArgs) Handles lnkbtnExcel.Click
        Dim mimeType As String = Nothing
        Dim dtEBD As New DataTable, dt As New DataTable, dtEQ As New DataTable, dtECS As New DataTable, dtPE As New DataTable, dtAL As New DataTable
        Dim dtPA As New DataTable, dtAP As New DataTable, dtSM As New DataTable, dtTF As New DataTable, dtPOA As New DataTable
        Try
            If ddlExistingEmployee.SelectedIndex > 0 Then
                dtEBD = objclsEProfile.LoadAllEmpBasicDetailsToReport(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue)
                dt = objclsEProfile.LoadExistingEmployeeDetailsToReport(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue)
                dtEQ = objclsEProfile.LoadEMPQualification(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtECS = objclsEProfile.LoadEMPCourse(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtPE = objclsEProfile.LoadEmpProfessionalExperienceDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtAL = objclsEProfile.LoadEmpAsstesLoanDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtPA = objclsEProfile.LoadEMPAssessment(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtAP = objclsEProfile.LoadEmpAcademicProgressDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtSM = objclsEProfile.LoadEMPSpecialMention(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtTF = objclsEProfile.LoadEmpTransferFirmDetails(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
                dtPOA = objclsEProfile.LoadEMPParticularsofArticles(sSession.AccessCode, sSession.AccessCodeID, ddlExistingEmployee.SelectedValue, 0)
            End If
            If ((dtEBD.Rows.Count = 0) Or (dt.Rows.Count = 0 And dtEQ.Rows.Count = 0 And dtECS.Rows.Count = 0 And dtPE.Rows.Count = 0 And dtAL.Rows.Count = 0 And dtPA.Rows.Count = 0 And dtAP.Rows.Count = 0 And dtSM.Rows.Count = 0 And dtTF.Rows.Count = 0 And dtPOA.Rows.Count = 0)) Then
                lblEmpProfileValidationMsg.Text = "No Data." : lblError.Text = "No Data."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-info');$('#ModalEmpProfileValidation').modal('show');", True)
                Exit Sub
            End If
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dt)
            Dim rdsEQ As New ReportDataSource("DataSet2", dtEQ)
            Dim rdsECS As New ReportDataSource("DataSet3", dtECS)
            Dim rdsPE As New ReportDataSource("DataSet4", dtPE)
            Dim rdsAL As New ReportDataSource("DataSet5", dtAL)
            Dim rdsPA As New ReportDataSource("DataSet6", dtPA)
            Dim rdsAP As New ReportDataSource("DataSet7", dtAP)
            Dim rdsSM As New ReportDataSource("DataSet8", dtSM)
            Dim rdsTF As New ReportDataSource("DataSet9", dtTF)
            Dim rdsPOA As New ReportDataSource("DataSet10", dtPOA)
            Dim rdsEBD As New ReportDataSource("DataSet11", dtEBD)

            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.DataSources.Add(rdsEQ)
            ReportViewer1.LocalReport.DataSources.Add(rdsECS)
            ReportViewer1.LocalReport.DataSources.Add(rdsPE)
            ReportViewer1.LocalReport.DataSources.Add(rdsAL)
            ReportViewer1.LocalReport.DataSources.Add(rdsPA)
            ReportViewer1.LocalReport.DataSources.Add(rdsAP)
            ReportViewer1.LocalReport.DataSources.Add(rdsSM)
            ReportViewer1.LocalReport.DataSources.Add(rdsTF)
            ReportViewer1.LocalReport.DataSources.Add(rdsPOA)
            ReportViewer1.LocalReport.DataSources.Add(rdsEBD)

            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/EProfileEmpMaster.rdlc")

            Dim EmployeeName As ReportParameter() = New ReportParameter() {New ReportParameter("EmployeeName", ddlExistingEmployee.SelectedItem.Text)}
            ReportViewer1.LocalReport.SetParameters(EmployeeName)
            ReportViewer1.LocalReport.Refresh()

            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "E-Profile", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=EProfile" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub imgbtnBack_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnBack.Click
        Dim oStatus As Object
        Try
            lblError.Text = ""
            oStatus = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(Val(sEMDBackStatus)))
            Response.Redirect(String.Format("~/Masters/EmployeeMaster.aspx?StatusID={0}", oStatus), False) 'Masters/EmployeeMaster
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnBack_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub

    Protected Sub imgbtnEQAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddEQAttch.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalEQAttachment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnEQAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnECSAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddECSAttch.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalECSAttachment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnECSAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnPEAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddPEAttach.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalProfessionalExperienceAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPEAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnALAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddALAttach.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAsstesLoanAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnALAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnPAAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddPAAttch.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPAAttachment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPAAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnAPAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddAPAttach.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalAcademicProgressAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnAPAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnSMAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddSMAttch.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalSMAttachment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnSMAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnTFAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddTFAttach.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalTransferFirmAttchment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnTFAttachment_Click")
        End Try
    End Sub

    Protected Sub imgbtnPOAAttachment_Click(sender As Object, e As ImageClickEventArgs)
        Try
            btnAddPOAAttch.Visible = True
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#myModalPOAAttachment').modal('show');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnPOAAttachment_Click")
        End Try
    End Sub

    'Private Sub chkIsPartner_CheckedChanged(sender As Object, e As EventArgs) Handles chkIsPartner.CheckedChanged
    '    Try
    '        RetrieveSignatureUpload.ImageUrl = ""
    '        If chkIsPartner.Checked = True Then
    '            pnlsignature.Visible = True
    '        Else
    '            pnlsignature.Visible = False
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
End Class