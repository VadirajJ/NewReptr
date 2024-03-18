Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class Master
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Master Masterpage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsLogin As New clsLogin
    Private objclsCPFP As New clsCPFP
    Private objUser As New clsCPFP.UserProfile
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnTRACeLog.ImageUrl = "Images/logo.png"
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Page.ViewStateUserKey = Session.SessionID
        If Session("AuthToken") IsNot Nothing AndAlso Request.Cookies("AuthToken") IsNot Nothing Then
            If Not Session("AuthToken").ToString().Equals(Request.Cookies("AuthToken").Value) Then
                Response.Redirect("~/LoginPage.aspx", False)
                Exit Sub
            End If
        End If
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim intSessionTimeOut As Integer, intSessionTimeOutWarning As Integer
        Try
            sSession = Session("AllSession")
            'mainForm.Action=Request.RawUrl
            intSessionTimeOut = sSession.TimeOut
            intSessionTimeOutWarning = sSession.TimeOutWarning
            lblTimeOutWarning.Text = "Your TRACe session will expire in " & (sSession.TimeOutWarning / 60000) & " mins! Please Save the data before the session expires."
            bdyProgramMaster.Attributes.Add("onload", "javascript:return checkTime(" + intSessionTimeOut.ToString + "," + intSessionTimeOutWarning.ToString + ");")
            lblUserName.Text = "Welcome" & " " & sSession.UserFullNameCode
            sSession.StartDate = objclsGRACeGeneral.FormatDtForRDBMS(objclsGRACeGeneral.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
            sSession.EndDate = objclsGRACeGeneral.FormatDtForRDBMS(objclsGRACeGeneral.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
            RegExpNewPwd.ValidationExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" & sSession.MinPasswordCharacter & "," & sSession.MaxPasswordCharacter & "}"
            lblCONote.Text = "Password must contain minimum " & sSession.MinPasswordCharacter & " characters, maximum " & sSession.MaxPasswordCharacter & " characters, atleast 1 uppercase alphabet, 1 lowercase alphabet, 1 number, 1 special character."
            CVCurrentPasssword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword)

            CVCheckPassword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword)

            REVMobNo.ErrorMessage = "Enter valid Mobile No." : REVMobNo.ValidationExpression = "^[0-9]{10}$"

            RFVEmail.ErrorMessage = "Enter E-Mail." : REVEmail.ErrorMessage = "Enter valid E-Mail." : REVEmail.ValidationExpression = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"

            RFVSecurityQuestion.ErrorMessage = "Enter Security Question." : REVSecurityQuestion.ValidationExpression = "^(.{0,250})$"
            REVSecurityQuestion.ErrorMessage = "Security Question exceeded maximum size(max 250 characters)."

            RFVAnswer.ErrorMessage = "Enter Answer." : REVAnswer.ValidationExpression = "^(.{0,250})$"
            REVAnswer.ErrorMessage = "Answer exceeded maximum size(max 250 characters)."

            REVExperiencesummary.ValidationExpression = "^(.{0,8000})$" : REVExperiencesummary.ErrorMessage = "Experience Summary exceeded maximum size(max 8000 characters)."

            REVOthers.ValidationExpression = "^(.{0,5000})$" : REVOthers.ErrorMessage = "Other qualification exceeded maximum size(max 5000 characters)."

            lnkbtnMyProfile.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('show');$('#txtCheckPassword').focus();return false;")
            lnkbtnChangePassword.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('show');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');return false;")
            If sSession.Menu = "MASTER" Then
                GetSubMenuOpen()
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Public Sub BindExperience()
        Try
            ddlExperience.Items.Clear()
            ddlExperience.Items.Insert(0, "0")
            ddlExperience.Items.Insert(1, "1")
            ddlExperience.Items.Insert(2, "2")
            ddlExperience.Items.Insert(3, "3")
            ddlExperience.Items.Insert(4, "4")
            ddlExperience.Items.Insert(5, "5")
            ddlExperience.Items.Insert(6, "6")
            ddlExperience.Items.Insert(7, "7")
            ddlExperience.Items.Insert(8, "8")
            ddlExperience.Items.Insert(9, "9")
            ddlExperience.Items.Insert(10, "10")
            ddlExperience.Items.Insert(11, "11")
            ddlExperience.Items.Insert(12, "12")
            ddlExperience.Items.Insert(13, "13")
            ddlExperience.Items.Insert(14, "14")
            ddlExperience.Items.Insert(15, "15")
            ddlExperience.Items.Insert(16, "16")
            ddlExperience.Items.Insert(17, "17")
            ddlExperience.Items.Insert(18, "18")
            ddlExperience.Items.Insert(19, "19")
            ddlExperience.Items.Insert(20, "20")
            ddlExperience.Items.Insert(21, "21")
            ddlExperience.Items.Insert(22, "22")
            ddlExperience.Items.Insert(23, "23")
            ddlExperience.Items.Insert(24, "24")
            ddlExperience.Items.Insert(25, "25")
            ddlExperience.Items.Insert(26, "26")
            ddlExperience.Items.Insert(27, "27")
            ddlExperience.Items.Insert(28, "28")
            ddlExperience.Items.Insert(29, "29")
            ddlExperience.Items.Insert(30, "30")
            ddlExperience.Items.Insert(31, "31")
            ddlExperience.Items.Insert(32, "32")
            ddlExperience.Items.Insert(33, "33")
            ddlExperience.Items.Insert(34, "34")
            ddlExperience.Items.Insert(35, "35")
            ddlExperience.SelectedIndex = 0
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub BindQualification()
        Try
            cblQualification.Items.Clear()
            cblQualification.Items.Add(New ListItem("Bachelor Degree", "1"))
            cblQualification.Items.Add(New ListItem("Master Degree", "2"))
            cblQualification.Items.Add(New ListItem("PG", "3"))
            cblQualification.Items.Add(New ListItem("Chartered Accountant", "4"))
            cblQualification.Items.Add(New ListItem("CIA Part1", "5"))
            cblQualification.Items.Add(New ListItem("CIA Part2", "6"))
            cblQualification.Items.Add(New ListItem("CIA Part3", "7"))
            cblQualification.Items.Add(New ListItem("ICWA", "8"))
            cblQualification.Items.Add(New ListItem("CISA", "9"))
            cblQualification.Items.Add(New ListItem("CISSP", "10"))
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetSubMenuOpen()
        Try
            liRLIC.Attributes.Remove("class") : liPeople.Attributes.Remove("class")
            liSuperMaster.Attributes.Remove("class")
            liAuditLog.Attributes.Remove("class")

            lnkbtnGRACeSettings.Font.Italic = False : lnkbtnGRACeSettings.Font.Bold = False
            lnkbtnOrganizationStructure.Font.Italic = False : lnkbtnOrganizationStructure.Font.Bold = False
            lnkbtnHolidayMaster.Font.Italic = False : lnkbtnHolidayMaster.Font.Bold = False
            lnkCompanyDetails.Font.Italic = False : lnkCompanyDetails.Font.Bold = False

            lnkbtnEmployeeMaster.Font.Italic = False : lnkbtnEmployeeMaster.Font.Bold = False
            lnkbtnUserMaster.Font.Italic = False : lnkbtnUserMaster.Font.Bold = False
            lnkbtnEProfile.Font.Italic = False : lnkbtnEProfile.Font.Bold = False
            lnkbtnLeaveDetails.Font.Italic = False : lnkbtnLeaveDetails.Font.Bold = False

            lnkbtnCustomerMaster.Font.Italic = False : lnkbtnCustomerMaster.Font.Bold = False
            lnkbtnGeneralMaster.Font.Italic = False : lnkbtnGeneralMaster.Font.Bold = False
            lnkbtnExcelUpload.Font.Italic = False : lnkbtnExcelUpload.Font.Bold = False
            lnkbtnAssignmentMaster.Font.Italic = False : lnkbtnAssignmentMaster.Font.Bold = False

            lnkbtnAuditLog.Font.Italic = False : lnkbtnAuditLog.Font.Bold = False

            If sSession.SubMenu = "TRACe" Then
                liRLIC.Attributes.Add("class", "open")
                If sSession.Form = "GRACeSettings" Then
                    lnkbtnGRACeSettings.Font.Italic = True : lnkbtnGRACeSettings.Font.Bold = True
                ElseIf sSession.Form = "OrganisationStructure" Then
                    lnkbtnOrganizationStructure.Font.Italic = True : lnkbtnOrganizationStructure.Font.Bold = True
                ElseIf sSession.Form = "HolidayMaster" Then
                    lnkbtnHolidayMaster.Font.Italic = True : lnkbtnHolidayMaster.Font.Bold = True
                ElseIf sSession.Form = "Company Details" Then
                    lnkCompanyDetails.Font.Italic = True : lnkCompanyDetails.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "People" Then
                liPeople.Attributes.Add("class", "open")
                If sSession.Form = "EmployeeMaster" Then
                    lnkbtnEmployeeMaster.Font.Italic = True : lnkbtnEmployeeMaster.Font.Bold = True
                ElseIf sSession.Form = "UserMaster" Then
                    lnkbtnUserMaster.Font.Italic = True : lnkbtnUserMaster.Font.Bold = True
                ElseIf sSession.Form = "CustomerMaster" Then
                    lnkbtnCustomerMaster.Font.Italic = True : lnkbtnCustomerMaster.Font.Bold = True
                ElseIf sSession.Form = "EProfile" Then
                    lnkbtnEProfile.Font.Italic = True : lnkbtnEProfile.Font.Bold = True
                ElseIf sSession.Form = "LeaveDetails" Then
                    lnkbtnLeaveDetails.Font.Italic = True : lnkbtnLeaveDetails.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "SuperMaster" Then
                liSuperMaster.Attributes.Add("class", "open")
                If sSession.Form = "GeneralMaster" Then
                    lnkbtnGeneralMaster.Font.Italic = True : lnkbtnGeneralMaster.Font.Bold = True
                ElseIf sSession.Form = "ExcelUpload" Then
                    lnkbtnExcelUpload.Font.Italic = True : lnkbtnExcelUpload.Font.Bold = True
                ElseIf sSession.Form = "AssignmentMaster" Then
                    lnkbtnAssignmentMaster.Font.Italic = True : lnkbtnAssignmentMaster.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Permission" Then
                liPermission.Attributes.Add("class", "open")
                If sSession.Form = "ModulePermission" Then
                    lnkPermission.Font.Italic = True : lnkPermission.Font.Bold = True
                ElseIf sSession.Form = "ModuleSplPermission" Then
                    lnkModPermission.Font.Italic = True : lnkModPermission.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "Report" Then
                liAuditLog.Attributes.Add("class", "open")
                If sSession.Form = "AuditLog" Then
                    lnkbtnAuditLog.Font.Italic = True : lnkbtnAuditLog.Font.Bold = True
                ElseIf sSession.Form = "DynamicReport" Then
                    lnkbtnDynamiclog.Font.Italic = True : lnkbtnDynamiclog.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "GRACeSettings" Then 'RLIC
                sSession.SubMenu = "TRACe" : sSession.Form = "GRACeSettings"
                Response.Redirect("~/Masters/GRACeSettings.aspx", False)
            ElseIf sForm = "OrganisationStructure" Then
                sSession.SubMenu = "TRACe" : sSession.Form = "OrganisationStructure"
                Response.Redirect("~/Masters/OrganizationStructure.aspx", False)
            ElseIf sForm = "HolidayMaster" Then
                sSession.SubMenu = "TRACe" : sSession.Form = "HolidayMaster"
                Response.Redirect("~/Masters/HolidayMaster.aspx", False)
            ElseIf sForm = "CompanyDetails" Then
                sSession.SubMenu = "TRACe" : sSession.Form = "CompanyDetails"
                Response.Redirect("~/Masters/CompanyDetails.aspx", False)
            ElseIf sForm = "EmployeeMaster" Then 'People
                sSession.SubMenu = "People" : sSession.Form = "EmployeeMaster"
                Response.Redirect("~/Masters/EmployeeMaster.aspx", False)
            ElseIf sForm = "UserMaster" Then
                sSession.SubMenu = "People" : sSession.Form = "UserMaster"
                Response.Redirect("~/Masters/UserMaster.aspx", False)
            ElseIf sForm = "CustomerMaster" Then
                sSession.SubMenu = "People" : sSession.Form = "CustomerMaster"
                Response.Redirect("~/Masters/CustomerMaster.aspx", False)
            ElseIf sForm = "EProfile" Then
                sSession.SubMenu = "People" : sSession.Form = "EProfile"
                Response.Redirect("~/HomePages/Master.aspx", False)
            ElseIf sForm = "LeaveDetails" Then
                sSession.SubMenu = "People" : sSession.Form = "LeaveDetails"
                Response.Redirect("~/Masters/LeaveDetails.aspx", False)
            ElseIf sForm = "RiskMaster" Then 'Risk
                sSession.SubMenu = "Risk" : sSession.Form = "RiskMaster"
                Response.Redirect("~/Masters/RiskMaster.aspx", False)
            ElseIf sForm = "RiskGeneralMaster" Then
                sSession.SubMenu = "Risk" : sSession.Form = "RiskGeneralMaster"
                Response.Redirect("~/Masters/RiskGeneralMaster.aspx", False)
            ElseIf sForm = "ControlMaster" Then
                sSession.SubMenu = "Risk" : sSession.Form = "ControlMaster"
                Response.Redirect("~/Masters/ControlMaster.aspx", False)
            ElseIf sForm = "HeatMap" Then
                sSession.SubMenu = "Risk" : sSession.Form = "HeatMap"
                Response.Redirect("~/Masters/HeatMap.aspx", False)
            ElseIf sForm = "Function" Then 'Process
                sSession.SubMenu = "Process" : sSession.Form = "Function"
                Response.Redirect("~/Masters/FunctionMaster.aspx", False)
            ElseIf sForm = "SubFunction" Then
                sSession.SubMenu = "Process" : sSession.Form = "SubFunction"
                Response.Redirect("~/Masters/SubFunctionMaster.aspx", False)
            ElseIf sForm = "Process" Then
                sSession.SubMenu = "Process" : sSession.Form = "Process"
                Response.Redirect("~/Masters/ProcessMaster.aspx", False)
            ElseIf sForm = "SubProcess" Then
                sSession.SubMenu = "Process" : sSession.Form = "SubProcess"
                Response.Redirect("~/Masters/SubProcessMaster.aspx", False)
            ElseIf sForm = "ReportTemplateMaster" Then 'Report
                sSession.SubMenu = "ReportMaster" : sSession.Form = "ReportTemplateMaster"
                Response.Redirect("~/Masters/ReportTemplate.aspx", False)
            ElseIf sForm = "ReportContentMaster" Then
                sSession.SubMenu = "ReportMaster" : sSession.Form = "ReportContentMaster"
                Response.Redirect("~/Masters/ReportContent.aspx", False)
            ElseIf sForm = "GeneralMaster" Then 'Super Master
                sSession.SubMenu = "SuperMaster" : sSession.Form = "GeneralMaster"
                Response.Redirect("~/Masters/GeneralMaster.aspx", False)
            ElseIf sForm = "MappingOfMaster" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "MappingOfMaster"
                Response.Redirect("~/Masters/MappingOfMaster.aspx", False)
            ElseIf sForm = "ExcelUpload" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "ExcelUpload"
                Response.Redirect("~/Masters/ExcelUpload.aspx", False)
            ElseIf sForm = "ServiceDashboard" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "ServiceDashboard"
                Response.Redirect("~/Masters/ServiceMaster.aspx", False)
            ElseIf sForm = "AssignmentMaster" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "AssignmentMaster"
                Response.Redirect("~/Assignment/AssignmentMaster.aspx", False)
            ElseIf sForm = "ModulePermission" Then
                sSession.SubMenu = "Permission" : sSession.Form = "ModulePermission"
                Response.Redirect("~/Masters/ModulePermission.aspx", False)
            ElseIf sForm = "ModuleSplPermission" Then
                sSession.SubMenu = "Permission" : sSession.Form = "ModuleSplPermission"
                Response.Redirect("~/Masters/ModuleSplPermission.aspx", False)
            ElseIf sForm = "IssueManagement" Then
                sSession.SubMenu = "TRACeKnowledgeBase" : sSession.Form = "IssueManagement"
                Response.Redirect("~/Masters/IssueKnowledgeBaseMaster.aspx", False)
            ElseIf sForm = "Publications" Then
                sSession.SubMenu = "TRACeKnowledgeBase" : sSession.Form = "Publications"
                Response.Redirect("~/Masters/Publications.aspx", False)
            ElseIf sForm = "AuditHistory" Then
                sSession.SubMenu = "TRACeKnowledgeBase" : sSession.Form = "AuditHistory"
                Response.Redirect("~/Masters/TRACeKnowledge.aspx", False)
            ElseIf sForm = "AuditLog" Then
                sSession.SubMenu = "AuditLog" : sSession.Form = "AuditLog"
                Response.Redirect("~/Masters/AuditLog.aspx", False)
            ElseIf sForm = "DynamicReport" Then
                sSession.SubMenu = "DynamicReport" : sSession.Form = "DynamicReport"
                Response.Redirect("~/Masters/DynamicReport.aspx", False)
            ElseIf sForm = "BranchMaster" Then 'Branch
                sSession.SubMenu = "Branch" : sSession.Form = "BranchMaster"
                Response.Redirect("~/Masters/BranchMaster.aspx", False)
            ElseIf sForm = "BranchAuditChecklist" Then
                sSession.SubMenu = "Branch" : sSession.Form = "BranchAuditChecklist"
                Response.Redirect("~/Masters/BranchAuditChecklist.aspx", False)
            ElseIf sForm = "GeneralCMaster" Then 'Corporate
                sSession.SubMenu = "CorporateManagementAudit" : sSession.Form = "GeneralCMaster"
                Response.Redirect("~/Masters/GeneralCMaster.aspx", False)
            ElseIf sForm = "CorporateMaster" Then 'Branch
                sSession.SubMenu = "CorporateManagementAudit" : sSession.Form = "CorporateMaster"
                Response.Redirect("~/Masters/CorporateMaster.aspx", False)
            ElseIf sForm = "CorporateAuditChecklist" Then
                sSession.SubMenu = "CorporateManagementAudit" : sSession.Form = "CorporateAuditChecklist"
                Response.Redirect("~/Masters/CorporateAuditChecklist.aspx", False)
            ElseIf sForm = "CAIQGenMas" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQGenMas"
                Response.Redirect("~/Masters/CAIQGeneralMaster.aspx", False)
            ElseIf sForm = "CAIQDesc" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQDesc"
                Response.Redirect("~/Masters/CAIQdetails.aspx", False)
            ElseIf sForm = "CAIQAudUniverse" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQAudUniverse"
                Response.Redirect("~/Masters/CAIQAuditUniverse.aspx", False)
            ElseIf sForm = "CAIQExcelUpload" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQExcelUpload"
                Response.Redirect("~/Masters/CAIQExcelUpload.aspx", False)
            End If
            Session("AllSession") = sSession
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub lnkbtnAuditLog_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAuditLog.Click
        Try
            GetClickedURL("AuditLog")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAuditLog_Click")
        End Try
    End Sub
    Private Sub lnkbtnDynamiclog_Click(sender As Object, e As EventArgs) Handles lnkbtnDynamiclog.Click
        Try
            GetClickedURL("DynamicReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDynamiclog_Click")
        End Try
    End Sub
    Protected Sub lnkbtnGRACeSettings_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnGRACeSettings.Click
        Try
            GetClickedURL("GRACeSettings")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGRACeSettings_Click")
        End Try
    End Sub
    Protected Sub lnkbtnOrganizationStructure_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnOrganizationStructure.Click
        Try
            GetClickedURL("OrganisationStructure")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOrganizationStructure_Click")
        End Try
    End Sub
    Protected Sub lnkbtnHolidayMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHolidayMaster.Click
        Try
            GetClickedURL("HolidayMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHolidayMaster_Click")
        End Try
    End Sub
    Protected Sub lnkCompanyDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCompanyDetails.Click
        Try
            GetClickedURL("CompanyDetails")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkCompanyDetails_Click")
        End Try
    End Sub
    Protected Sub lnkbtnEmployeeMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnEmployeeMaster.Click
        Try
            GetClickedURL("EmployeeMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmployeeMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnUserMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnUserMaster.Click
        Try
            GetClickedURL("UserMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnUserMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnCustomerMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCustomerMaster.Click
        Try
            GetClickedURL("CustomerMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnLeaveDetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLeaveDetails.Click 'EProfile
        Try
            GetClickedURL("LeaveDetails")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLeaveDetails_Click")
        End Try
    End Sub
    Protected Sub lnkbtnEProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnEProfile.Click 'EProfile
        Try
            GetClickedURL("EProfile")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEProfile_Click")
        End Try
    End Sub
    Protected Sub lnkbtnGeneralMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnGeneralMaster.Click
        Try
            GetClickedURL("GeneralMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGeneralMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcelUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnExcelUpload.Click
        Try
            GetClickedURL("ExcelUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcelUpload_Click")
        End Try
    End Sub
    Protected Sub lnkbtnAssignmentMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAssignmentMaster.Click
        Try
            GetClickedURL("AssignmentMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssignmentMaster_Click")
        End Try
    End Sub
    Protected Sub lnkPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkPermission.Click
        Try
            GetClickedURL("ModulePermission")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkPermission_Click")
        End Try
    End Sub
    Protected Sub lnkModPermission_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModPermission.Click
        Try
            GetClickedURL("ModuleSplPermission")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkModPermission_Click")
        End Try
    End Sub
    Protected Sub btnCheckPwd_Click(sender As Object, e As EventArgs)
        Dim bFlag As Boolean
        Try
            bFlag = objclsCPFP.CheckUserPWD(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtCheckPassword.Text))
            If bFlag = True Then
                BindExperience() : BindQualification() : LoadUserProfile()
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('show');$('#ModalPassword').modal('hide');", True)
            Else
                lblValidationMsg.Text = "Invalid Passsword."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
            End If
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckPwd_Click")
        End Try
    End Sub
    Protected Sub btnCheckCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCheckPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCheckCancel_Click")
        End Try
    End Sub
    Protected Sub btnCPCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCurrentPasssword.Text = "" : txtNewPassword.Text = "" : txtConfirmPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCPCancel_Click")
        End Try
    End Sub
    Protected Sub btnCPUpdate_Click(sender As Object, e As EventArgs)
        Dim iMinPassword As Integer, iMaxPassword As Integer
        Try
            lblCPError.Text = "" : lblUPError.Text = ""
            If txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
                If (objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword) <> txtCurrentPasssword.Text) Then
                    txtCurrentPasssword.Focus()
                    lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
                    Exit Try
                End If

                iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")

                If iMinPassword > txtNewPassword.Text.Length Then
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Password must have at least " & iMinPassword & " characters." : lblCPError.Text = "Password must have at least " & iMinPassword & " characters."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                If iMaxPassword < txtNewPassword.Text.Length Then
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Password is less than " & iMaxPassword & " characters." : lblCPError.Text = "Password is less than " & iMaxPassword & " characters."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                If objclsCPFP.checkForPasswordAlreadyExit(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID) = False Then ' txtNewPwd Replaced with sPwd
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Enter New Password, different than your previous 5 passwords." : lblCPError.Text = "Enter New Password, different than your previous 5 passwords."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                objclsCPFP.SaveOldPwdHistory(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID)
                objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
                objclsCPFP.UpdatedPasswordDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.IPAddress)
                objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, "Password Changed.", sSession.IPAddress, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text))
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "Change Password", "Password Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
                lblValidationMsg.Text = "Password Successfully Changed." : lblCPError.Text = "Password Successfully Changed."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            Else
                txtCurrentPasssword.Focus()
                lblValidationMsg.Text = "Invalid Old Passsword." : lblCPError.Text = "Invalid Old Passsword."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
            End If
        Catch ex As Exception
            lblCPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdateChagePwd_Click")
        End Try
    End Sub
    Public Sub LoadUserProfile()
        Dim sArray As Array
        Dim j As Integer
        Try
            objUser = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_LoginName)
            txtSAPcode.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Code)
            txtEmpName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_fullName)
            txtMail.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Email)
            If objUser.sUsr_MobileNo = "&nbsp;" Then
                txtMobNo.Text = ""
            Else
                txtMobNo.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_MobileNo)
            End If

            txtDesignation.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Designation)
            If objUser.sUsr_GrpOrUserLvlPerm = 0 Then
                txtPermission.Text = "Role based"
            Else
                txtPermission.Text = "User based"
            End If
            txtRole.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_LevelGrp)
            txtSecurityQuestion.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_SecurityQuestion)

            If objUser.sUsr_Answer <> "" Then
                txtAnswer.Attributes.Add("value", objclsGRACeGeneral.DecryptPassword(objUser.sUsr_Answer))
            End If
            txtExperiencesummary.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_SkillSet)
            ddlExperience.SelectedIndex = objUser.iUsr_Experience
            txtOthers.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_Others)

            If objUser.sUsr_Qualification.Contains(",") = True Then
                sArray = objUser.sUsr_Qualification.Split(",")
                For i = 0 To sArray.Length - 1
                    If sArray(i) <> "" Then
                        For j = 0 To cblQualification.Items.Count - 1
                            If cblQualification.Items(j).Value = sArray(i) Then
                                cblQualification.Items(j).Selected = True
                            End If
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Protected Sub btnUpdateUserProfile_Click(sender As Object, e As EventArgs)
        Dim sQual As String = "", sSecurityAnswer As String
        Try
            lblCPError.Text = "" : lblUPError.Text = ""
            If txtMobNo.Text.Trim <> "" Then
                If txtMobNo.Text.Trim.Length > 10 Then
                    txtMobNo.Focus()
                    lblValidationMsg.Text = "Mobile No. exceeded maximum size(max 10 numbers)." : lblUPError.Text = "Mobile No. exceeded maximum size(max 10 numbers).'"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If

                If txtMobNo.Text.Trim.Length <> 10 Then
                    txtMobNo.Focus()
                    lblValidationMsg.Text = "Enter valid 10 digits Mobile No." : lblUPError.Text = "Enter valid 10 digits Mobile No."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                    Exit Sub
                End If
            End If
            If txtMail.Text.Trim = "" Then
                txtMail.Focus()
                lblValidationMsg.Text = "Enter E-Mail." : lblUPError.Text = "Enter E-Mail."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtSecurityQuestion.Text.Trim = "" Then
                txtSecurityQuestion.Focus()
                lblValidationMsg.Text = "Enter Security Question." : lblUPError.Text = "Enter Security Question."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtSecurityQuestion.Text.Trim.Length > 250 Then
                txtSecurityQuestion.Focus()
                lblValidationMsg.Text = "Security Question exceeded maximum size(max 250 characters)." : lblUPError.Text = "Security Question exceeded maximum size(max 250 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAnswer.Text.Trim = "" Then
                txtAnswer.Focus()
                lblValidationMsg.Text = "Enter Answer." : lblUPError.Text = "Enter Answer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtAnswer.Text.Trim.Length > 250 Then
                txtAnswer.Focus()
                lblValidationMsg.Text = "Answer exceeded maximum size(max 250 characters)." : lblUPError.Text = "Answer exceeded maximum size(max 250 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If
            If txtExperiencesummary.Text.Trim.Length > 8000 Then
                txtExperiencesummary.Focus()
                lblValidationMsg.Text = "Experience Summary exceeded maximum size(max 8000 characters)." : lblUPError.Text = "Experience Summary exceeded maximum size(max 8000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

            objUser = objclsCPFP.LoadUserprofile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            txtLoginName.Text = objclsGRACeGeneral.ReplaceSafeSQL(objUser.sUsr_LoginName)
            sSecurityAnswer = objclsGRACeGeneral.EncryptPassword(Trim(txtAnswer.Text))
            For i = 0 To cblQualification.Items.Count - 1
                If cblQualification.Items(i).Selected = True Then
                    sQual = sQual & "," & cblQualification.Items(i).Value
                End If
            Next

            If txtOthers.Text.Trim.Length > 5000 Then
                lblValidationMsg.Text = "Others Details exceeded maximum size(max 5000 characters)." : lblUPError.Text = "Others Details exceeded maximum size(max 5000 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');", True)
                txtOthers.Focus()
                Exit Sub
            End If
            objclsCPFP.UpdateUserProfile(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, objclsGRACeGeneral.SafeSQL(txtMobNo.Text), objclsGRACeGeneral.SafeSQL(txtExperiencesummary.Text), ddlExperience.SelectedIndex, sQual, objclsGRACeGeneral.SafeSQL(txtOthers.Text), objclsGRACeGeneral.SafeSQL(txtSecurityQuestion.Text), sSecurityAnswer, objclsGRACeGeneral.SafeSQL(txtMail.Text), sSession.IPAddress)
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Master", "User Profile", "Profile Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
            lblValidationMsg.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblUPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateUserProfile_Click")
        End Try
    End Sub
    Protected Sub lnkbtnHOME_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHOME.Click
        Try
            sSession.Menu = "HOME" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHOME_Click")
        End Try
    End Sub
    Protected Sub lnkbtnMASTERS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnMASTERS.Click
        Try
            sSession.Menu = "MASTER" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Master.aspx", False) 'HomePages/Master
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnMASTERS_Click")
        End Try
    End Sub
    Protected Sub lnkbtnAUDIT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnAUDIT.Click
        Try
            sSession.Menu = "AUDIT" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Audit.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAUDIT_Click")
        End Try
    End Sub
    Protected Sub lnkbtnDigital_AuditOffice_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnDigital_AuditOffice.Click
        Try
            sSession.Menu = "Digital_AuditOffice" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Digital_AuditOfficeHome.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDigital_AuditOffice_Click")
        End Try
    End Sub
    Protected Sub lnkbtnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLogout.Click
        Try
            If (sSession.UserID) <> 0 Then
                '  objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-60)
            End If
            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLogout_Click")
        End Try
    End Sub
    Protected Sub btnLogOut_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            If (sSession.UserID) <> 0 Then
                ' objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.UserID)
            End If
            If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-60)
            End If

            If IsNothing(Request.Cookies("AuthToken")) = False Then
                Response.Cookies("AuthToken").Value = String.Empty
                Response.Cookies("AuthToken").Expires = DateTime.Now.AddMonths(-60)
            End If

            Session.Clear() : Session.Abandon() : Session.RemoveAll()
            Response.Redirect("~/Loginpage.aspx", False) 'Loginpage
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogOut_Click")
        End Try
    End Sub
End Class