Imports BusinesLayer
Imports System.Data
Imports System.IO
Partial Class Audit
    Inherits System.Web.UI.MasterPage
    Private Shared sFormName As String = "Audit Masterpage"
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
            liMaster.Attributes.Remove("class")
            liAuditLog.Attributes.Remove("class")
            lnkbtnGeneralMaster.Font.Italic = False : lnkbtnGeneralMaster.Font.Bold = False
            lnkbtnExcelUpload.Font.Italic = False : lnkbtnExcelUpload.Font.Bold = False
            '   liRCSA.Attributes.Remove("class")


            If sSession.SubMenu = "SuperMaster" Then
                liMaster.Attributes.Add("class", "open")
                If sSession.Form = "GeneralMaster" Then
                    lnkbtnGeneralMaster.Font.Italic = True : lnkbtnGeneralMaster.Font.Bold = True
                ElseIf sSession.Form = "ExcelUpload" Then
                    lnkbtnExcelUpload.Font.Italic = True : lnkbtnExcelUpload.Font.Bold = True
                End If
            ElseIf sSession.SubMenu = "RCSA" Then
                'liRCSA.Attributes.Add("class", "open")
                'If sSession.Form = "RCSADashboard" Then
                '    lnkbtnRCSADashboard.Font.Italic = True : lnkbtnRCSADashboard.Font.Bold = True
                'ElseIf sSession.Form = "RCSAAssign" Then
                '    lnkbtnRCSAAssign.Font.Italic = True : lnkbtnRCSAAssign.Font.Bold = True
                'ElseIf sSession.Form = "RCSATracker" Then
                '    lnkbtnRCSATracker.Font.Italic = True : lnkbtnRCSATracker.Font.Bold = True
                'ElseIf sSession.Form = "RCSAMonitor" Then
                '    lnkbtnRCSAMonitor.Font.Italic = True : lnkbtnRCSAMonitor.Font.Bold = True
                'End If
            ElseIf sSession.SubMenu = "Report" Then

            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub GetClickedURL(ByVal sForm As String)
        Try
            If sForm = "RiskMaster" Then 'Risk
                sSession.SubMenu = "Risk" : sSession.Form = "RiskMaster"
                Response.Redirect("~/Audit/RiskMaster.aspx", False)
            ElseIf sForm = "RiskGeneralMaster" Then
                sSession.SubMenu = "Risk" : sSession.Form = "RiskGeneralMaster"
                Response.Redirect("~/Audit/RiskGeneralMaster.aspx", False)
            ElseIf sForm = "ControlMaster" Then
                sSession.SubMenu = "Risk" : sSession.Form = "ControlMaster"
                Response.Redirect("~/Audit/ControlMaster.aspx", False)
            ElseIf sForm = "HeatMap" Then
                sSession.SubMenu = "Risk" : sSession.Form = "HeatMap"
                Response.Redirect("~/Audit/HeatMap.aspx", False)
            ElseIf sForm = "Function" Then 'Process
                sSession.SubMenu = "Process" : sSession.Form = "Function"
                Response.Redirect("~/Audit/FunctionMaster.aspx", False)
            ElseIf sForm = "SubFunction" Then
                sSession.SubMenu = "Process" : sSession.Form = "SubFunction"
                Response.Redirect("~/Audit/SubFunctionMaster.aspx", False)
            ElseIf sForm = "Process" Then
                sSession.SubMenu = "Process" : sSession.Form = "Process"
                Response.Redirect("~/Audit/ProcessMaster.aspx", False)
            ElseIf sForm = "SubProcess" Then
                sSession.SubMenu = "Process" : sSession.Form = "SubProcess"
                Response.Redirect("~/Audit/SubProcessMaster.aspx", False)
            ElseIf sForm = "ReportTemplateMaster" Then 'Report
                sSession.SubMenu = "ReportMaster" : sSession.Form = "ReportTemplateMaster"
                Response.Redirect("~/Masters/ReportTemplate.aspx", False)
            ElseIf sForm = "ReportContentMaster" Then
                sSession.SubMenu = "ReportMaster" : sSession.Form = "ReportContentMaster"
                Response.Redirect("~/Masters/ReportContent.aspx", False)
            ElseIf sForm = "GeneralMaster" Then 'Super Master
                sSession.SubMenu = "SuperMaster" : sSession.Form = "GeneralMaster"
                Response.Redirect("~/Audit/GeneralMaster.aspx", False)
            ElseIf sForm = "MappingOfMaster" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "MappingOfMaster"
                Response.Redirect("~/Audit/MappingOfMaster.aspx", False)
            ElseIf sForm = "ExcelUpload" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "ExcelUpload"
                Response.Redirect("~/Audit/ExcelUpload.aspx", False)
            ElseIf sForm = "ServiceDashboard" Then
                sSession.SubMenu = "SuperMaster" : sSession.Form = "ServiceDashboard"
                Response.Redirect("~/Audit/ServiceMaster.aspx", False)
            ElseIf sForm = "RCSADashboard" Then 'RCSA
                sSession.SubMenu = "RCSA" : sSession.Form = "RCSADashboard"
                Response.Redirect("~/Audit/RCSASummarySheet.aspx", False)
            ElseIf sForm = "RCSAAssign" Then
                sSession.SubMenu = "RCSA" : sSession.Form = "RCSAAssign"
                Response.Redirect("~/Audit/RCSAAssign.aspx", False)
            ElseIf sForm = "RCSATracker" Then
                sSession.SubMenu = "RCSA" : sSession.Form = "RCSATracker"
                Response.Redirect("~/Audit/RCSADashboard.aspx", False)
            ElseIf sForm = "RCSAMonitor" Then
                sSession.SubMenu = "RCSA" : sSession.Form = "RCSAMonitor"
                Response.Redirect("~/Audit/RCSAMonitor.aspx", False)
            ElseIf sForm = "RiskAssessment" Then 'RA
                sSession.SubMenu = "RA" : sSession.Form = "RiskAssessment"
                Response.Redirect("~/Audit/RADashboard.aspx", False)
            ElseIf sForm = "RAConduct" Then
                sSession.SubMenu = "RA" : sSession.Form = "RAConduct"
                Response.Redirect("~/Audit/RAConduct.aspx", False)
            ElseIf sForm = "RAViewSummarySheet" Then
                sSession.SubMenu = "RA" : sSession.Form = "RAViewSummarySheet"
                Response.Redirect("~/Audit/RASummarySheet.aspx", False)
            ElseIf sForm = "RAMonitor" Then
                sSession.SubMenu = "RA" : sSession.Form = "RAConduct"
                Response.Redirect("~/Audit/RAMonitor.aspx", False)
            ElseIf sForm = "FRRVDashboard" Then 'FRRV
                sSession.SubMenu = "FRRV" : sSession.Form = "FRRVDashboard"
                Response.Redirect("~/Audit/FRRDashboard.aspx", False)
            ElseIf sForm = "FRRPlanningScheduling" Then
                sSession.SubMenu = "FRRV" : sSession.Form = "FRRPlanningScheduling"
                Response.Redirect("~/Audit/FRRPlanningDashboard.aspx", False)
            ElseIf sForm = "BRRVDashboard" Then 'BRRV
                sSession.SubMenu = "BRRV" : sSession.Form = "BRRVDashboard"
                Response.Redirect("~/Audit/BranchDashboard.aspx", False)
            ElseIf sForm = "BRRPlanning" Then
                sSession.SubMenu = "BRRV" : sSession.Form = "BRRPlanning"
                Response.Redirect("~/Audit/BRRPlanning.aspx", False)
            ElseIf sForm = "BRRScheduling" Then
                sSession.SubMenu = "BRRV" : sSession.Form = "BRRScheduling"
                Response.Redirect("~/Audit/BRRSchedulingDashboard.aspx", False)
            ElseIf sForm = "BRRPConduct" Then
                sSession.SubMenu = "BRRV" : sSession.Form = "BRRPConduct"
                Response.Redirect("~/Audit/BRRChecklist.aspx", False)
            ElseIf sForm = "BRRIssueTracker" Then
                sSession.SubMenu = "BRRV" : sSession.Form = "BRRIssueTracker"
                Response.Redirect("~/Audit/BRRIssueTracker.aspx", False)
            ElseIf sForm = "BRRReport" Then
                sSession.SubMenu = "BRRV" : sSession.Form = "BRRReport"
                Response.Redirect("~/Audit/BRRReport.aspx", False)
            ElseIf sForm = "FRRPConduct" Then
                sSession.SubMenu = "FRRV" : sSession.Form = "FRRPConduct"
                Response.Redirect("~/Audit/FRRConductDashboard.aspx", False)
            ElseIf sForm = "FRRIssueTracker" Then
                sSession.SubMenu = "FRRV" : sSession.Form = "FRRIssueTracker"
                Response.Redirect("~/Audit/FRRITDashboard.aspx", False)
            ElseIf sForm = "BranchMaster" Then 'Branch
                sSession.SubMenu = "Branch" : sSession.Form = "BranchMaster"
                Response.Redirect("~/Audit/BranchMaster.aspx", False)
            ElseIf sForm = "BranchAuditChecklist" Then
                sSession.SubMenu = "Branch" : sSession.Form = "BranchAuditChecklist"
                Response.Redirect("~/Audit/BranchAuditChecklist.aspx", False)
            ElseIf sForm = "GeneralCMaster" Then 'Corporate
                sSession.SubMenu = "CorporateManagementAudit" : sSession.Form = "GeneralCMaster"
                Response.Redirect("~/Audit/GeneralCMaster.aspx", False)
            ElseIf sForm = "CorporateMaster" Then 'Branch
                sSession.SubMenu = "CorporateManagementAudit" : sSession.Form = "CorporateMaster"
                Response.Redirect("~/Audit/CorporateMaster.aspx", False)
            ElseIf sForm = "CorporateAuditChecklist" Then
                sSession.SubMenu = "CorporateManagementAudit" : sSession.Form = "CorporateAuditChecklist"
                Response.Redirect("~/Audit/CorporateAuditChecklist.aspx", False)
            ElseIf sForm = "CAIQGenMas" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQGenMas"
                Response.Redirect("~/Audit/CAIQGeneralMaster.aspx", False)
            ElseIf sForm = "CAIQDesc" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQDesc"
                Response.Redirect("~/Audit/CAIQdetails.aspx", False)
            ElseIf sForm = "CAIQAudUniverse" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQAudUniverse"
                Response.Redirect("~/Audit/CAIQAuditUniverse.aspx", False)
            ElseIf sForm = "CAIQExcelUpload" Then
                sSession.SubMenu = "CAIQAuditMaster" : sSession.Form = "CAIQExcelUpload"
                Response.Redirect("~/Audit/CAIQExcelUpload.aspx", False)
            ElseIf sForm = "Trace360" Then
                sSession.SubMenu = "TRACe360" : sSession.Form = "TRACe360"
                Response.Redirect("~/Audit/TRACe360.aspx", False)
            ElseIf sForm = "AnnualPlan" Then
                sSession.SubMenu = "AnnualPlan" : sSession.Form = "AnnualPlan"
                Response.Redirect("~/Audit/AnnualPlan.aspx", False)
            ElseIf sForm = "AdtPlnMm" Then
                sSession.SubMenu = "APM" : sSession.Form = "APM"
                Response.Redirect("~/Audit/APM.aspx", False)
            ElseIf sForm = "TimeBudget" Then
                sSession.SubMenu = "TimeBudget" : sSession.Form = "TimeBudget"
                Response.Redirect("~/Audit/TimeBudget.aspx", False)
            ElseIf sForm = "CostBudget" Then
                sSession.SubMenu = "CostBudget" : sSession.Form = "CostBudget"
                Response.Redirect("~/Audit/CostBudget.aspx", False)
            ElseIf sForm = "AdtPlnSignOff" Then
                sSession.SubMenu = "AuditPlanSignOff" : sSession.Form = "AuditPlanSignOff"
                Response.Redirect("~/Audit/AuditPlanSignOff.aspx", False)
            ElseIf sForm = "WorkPaper" Then
                sSession.SubMenu = "WorkPaper" : sSession.Form = "WorkPaper"
                Response.Redirect("~/Audit/WorkPaper.aspx", False)
            ElseIf sForm = "IssueTracker" Then
                sSession.SubMenu = "IssueTracker" : sSession.Form = "IssueTracker"
                Response.Redirect("~/Audit/IssueTracker.aspx", False)
            ElseIf sForm = "RiskAssment" Then
                sSession.SubMenu = "RiskAssessmentChecklist" : sSession.Form = "RiskAssessmentChecklist"
                Response.Redirect("~/Audit/RiskAssessmentChecklist.aspx", False)
            ElseIf sForm = "AdtProgress" Then
                sSession.SubMenu = "AuditProgress" : sSession.Form = "AuditProgress"
                Response.Redirect("~/Audit/AuditProgress.aspx", False)
            ElseIf sForm = "TimeSheetEntry" Then
                sSession.SubMenu = "TimeSheet" : sSession.Form = "TimeSheet"
                Response.Redirect("~/Audit/TimeSheet.aspx", False)
            ElseIf sForm = "TimeSheetVar" Then
                sSession.SubMenu = "TimeSheetVariance" : sSession.Form = "TimeSheetVariance"
                Response.Redirect("~/Audit/TimeSheetVariance.aspx", False)
            ElseIf sForm = "CostSheetEntry" Then
                sSession.SubMenu = "CostSheet" : sSession.Form = "CostSheet"
                Response.Redirect("~/Audit/CostSheet.aspx", False)
            ElseIf sForm = "CostSheetVar" Then
                sSession.SubMenu = "CostSheetVariance" : sSession.Form = "CostSheetVariance"
                Response.Redirect("~/Audit/CostSheetVariance.aspx", False)
            ElseIf sForm = "ARRep" Then
                sSession.SubMenu = "ReportGeneration" : sSession.Form = "ReportGeneration"
                Response.Redirect("~/Audit/ReportGeneration.aspx", False)
            ElseIf sForm = "ARExecSum" Then
                sSession.SubMenu = "ExecutiveSummary" : sSession.Form = "ExecutiveSummary"
                Response.Redirect("~/Audit/ExecutiveSummary.aspx", False)
            ElseIf sForm = "AudClosure" Then
                sSession.SubMenu = "AuidtClosure" : sSession.Form = "AuidtClosure"
                Response.Redirect("~/Audit/AuidtClosure.aspx", False)
            ElseIf sForm = "DRLLog" Then
                sSession.SubMenu = "DRLLog" : sSession.Form = "DRLLog"
                Response.Redirect("~/Audit/DRLLog.aspx", False)
            ElseIf sForm = "DataCapt" Then
                sSession.SubMenu = "DataCaptureDashboard" : sSession.Form = "DataCaptureDashboard"
                Response.Redirect("~/Audit/DataCaptureDashboard.aspx", False)
            ElseIf sForm = "CBAGCMaster" Then
                sSession.SubMenu = "GeneralCMaster" : sSession.Form = "GeneralCMaster"
                Response.Redirect("~/Audit/GeneralCMaster.aspx", False)
            ElseIf sForm = "CBACSAuditMaster" Then
                sSession.SubMenu = "CorporateMaster" : sSession.Form = "CorporateMaster"
                Response.Redirect("~/Audit/CorporateMaster.aspx", False)
            ElseIf sForm = "CBAChkLstUpload" Then
                sSession.SubMenu = "CorporateAuditChecklist" : sSession.Form = "CorporateAuditChecklist"
                Response.Redirect("~/Audit/CorporateAuditChecklist.aspx", False)
            ElseIf sForm = "CBAAuditDashboard" Then
                sSession.SubMenu = "CBAAuditDashboard" : sSession.Form = "CBAAuditDashboard"
                Response.Redirect("~/Audit/CBAAuditDashboard.aspx", False)
            ElseIf sForm = "CBAAUDPlanSchedule" Then
                sSession.SubMenu = "CBAAUDPlanSchedule" : sSession.Form = "CBAAUDPlanSchedule"
                Response.Redirect("~/Audit/CBAAUDPlanSchedule.aspx", False)
            ElseIf sForm = "CBAAssignAudit" Then
                sSession.SubMenu = "CBAAssignAudit" : sSession.Form = "CBAAssignAudit"
                Response.Redirect("~/Audit/CBAAssignAudit.aspx", False)
            ElseIf sForm = "CBAIssueTrackerDashboard" Then
                sSession.SubMenu = "CBAIssueTrackerDashboard" : sSession.Form = "CBAIssueTrackerDashboard"
                Response.Redirect("~/Audit/CBAIssueTrackerDashboard.aspx", False)
            ElseIf sForm = "CBAFollowupDashboard" Then
                sSession.SubMenu = "CBAFollowupDashboard" : sSession.Form = "CBAFollowupDashboard"
                Response.Redirect("~/Audit/CBAFollowupDashboard.aspx", False)
            ElseIf sForm = "CBAAUDITCharts" Then
                sSession.SubMenu = "CBAAUDITCharts" : sSession.Form = "CBAAUDITCharts"
                Response.Redirect("~/Audit/CBAAUDITCharts.aspx", False)
            ElseIf sForm = "SADashboard" Then
                sSession.SubMenu = "SADashboard" : sSession.Form = "SADashboard"
                Response.Redirect("~/Audit/SADashboard.aspx", False)
            ElseIf sForm = "SAConduct" Then
                sSession.SubMenu = "SAConduct" : sSession.Form = "SAConduct"
                Response.Redirect("~/Audit/SAConduct.aspx", False)
            ElseIf sForm = "SAReport" Then
                sSession.SubMenu = "SAReport" : sSession.Form = "SAReport"
                Response.Redirect("~/Audit/SAReport.aspx", False)
            ElseIf sForm = "CAIQGeneralMaster" Then
                sSession.SubMenu = "CAIQGeneralMaster" : sSession.Form = "CAIQGeneralMaster"
                Response.Redirect("~/Audit/CAIQGeneralMaster.aspx", False)
            ElseIf sForm = "CAIQdetails" Then
                sSession.SubMenu = "CAIQdetails" : sSession.Form = "CAIQdetails"
                Response.Redirect("~/Audit/CAIQdetails.aspx", False)
            ElseIf sForm = "CAIQAuditUniverse" Then
                sSession.SubMenu = "CAIQAuditUniverse" : sSession.Form = "CAIQAuditUniverse"
                Response.Redirect("~/Audit/CAIQAuditUniverse.aspx", False)
            ElseIf sForm = "CAIQExcelUpload" Then
                sSession.SubMenu = "CAIQExcelUpload" : sSession.Form = "CAIQExcelUpload"
                Response.Redirect("~/Audit/CAIQExcelUpload.aspx", False)
            ElseIf sForm = "CCMAuditDashboard" Then
                sSession.SubMenu = "CCMAuditDashboard" : sSession.Form = "CCMAuditDashboard"
                Response.Redirect("~/Audit/CCMAuditDashboard.aspx", False)
            ElseIf sForm = "CCMAUDPlanSchedule" Then
                sSession.SubMenu = "CCMAUDPlanSchedule" : sSession.Form = "CCMAUDPlanSchedule"
                Response.Redirect("~/Audit/CCMAUDPlanSchedule.aspx", False)
            ElseIf sForm = "CCMAssignAudit" Then
                sSession.SubMenu = "CCMAssignAudit" : sSession.Form = "CCMAssignAudit"
                Response.Redirect("~/Audit/CCMAssignAudit.aspx", False)
            ElseIf sForm = "CCMIssueTrackerDashboard" Then
                sSession.SubMenu = "CCMIssueTrackerDashboard" : sSession.Form = "CCMIssueTrackerDashboard"
                Response.Redirect("~/Audit/CCMIssueTrackerDashboard.aspx", False)
            ElseIf sForm = "CCMFollowupDashboard" Then
                sSession.SubMenu = "CCMFollowupDashboard" : sSession.Form = "CCMFollowupDashboard"
                Response.Redirect("~/Audit/CCMFollowupDashboard.aspx", False)
            ElseIf sForm = "CCMAUDITCharts" Then
                sSession.SubMenu = "CCMAUDITCharts" : sSession.Form = "CCMAUDITCharts"
                Response.Redirect("~/Audit/CCMAUDITCharts.aspx", False)
            ElseIf sForm = "QAConductAudit" Then
                sSession.SubMenu = "QAConductAudit" : sSession.Form = "QAConductAudit"
                Response.Redirect("~/Audit/QAConductAudit.aspx", False)
            ElseIf sForm = "QAAuditClosure" Then
                sSession.SubMenu = "QAAuditClosure" : sSession.Form = "QAAuditClosure"
                Response.Redirect("~/Audit/QAAuditClosure.aspx", False)
            End If
            Session("AllSession") = sSession

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Protected Sub lnkbtnGeneralMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnGeneralMaster.Click
        Try
            GetClickedURL("GeneralMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGeneralMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnMappingOfMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnMappingOfMaster.Click
        Try
            GetClickedURL("MappingOfMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnMappingOfMaster_Click")
        End Try
    End Sub
    Protected Sub lnkbtnExcelUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnExcelUpload.Click
        Try
            GetClickedURL("ExcelUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcelUpload_Click")
        End Try
    End Sub
    Protected Sub lnkbtnServiceDashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnServiceDashboard.Click
        Try
            GetClickedURL("ServiceDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnServiceDashboard_Click")
        End Try
    End Sub
    Protected Sub lnkbtnFunction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFunction.Click
        Try
            GetClickedURL("Function")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFunction_Click")
        End Try
    End Sub
    Protected Sub lnkbtnSubFunction_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSubFunction.Click
        Try
            GetClickedURL("SubFunction")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSubFunction_Click")
        End Try
    End Sub
    Protected Sub lnkbtnProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnProcess.Click
        Try
            GetClickedURL("Process")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnProcess_Click")
        End Try
    End Sub
    Protected Sub lnkbtnSubProcess_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnSubProcess.Click
        Try
            GetClickedURL("SubProcess")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSubProcess_Click")
        End Try
    End Sub
    Protected Sub lnkbtnHeatMap_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHeatMap.Click
        Try
            GetClickedURL("HeatMap")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHeatMap_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnRiskGeneralMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRiskGeneralMaster.Click
        Try
            GetClickedURL("RiskGeneralMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRiskGeneralMaster_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub

    Protected Sub lnkbtnControlMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnControlMaster.Click
        Try
            GetClickedURL("ControlMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnControlMaster_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnRiskMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRiskMaster.Click
        Try
            GetClickedURL("RiskMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRiskMaster_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
        End Try
    End Sub
    'Protected Sub lnkbtnRCSADashboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRCSADashboard.Click
    '    Try
    '        GetClickedURL("RCSADashboard")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRCSADashboard_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRCSAAssign_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRCSAAssign.Click
    '    Try
    '        GetClickedURL("RCSAAssign")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRCSAAssign_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRCSATracker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRCSATracker.Click
    '    Try
    '        GetClickedURL("RCSATracker")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRCSATracker_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRCSAMonitor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRCSAMonitor.Click
    '    Try
    '        GetClickedURL("RCSAMonitor")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRCSAMonitor_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRiskAssessment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRiskAssessment.Click
    '    Try
    '        GetClickedURL("RiskAssessment")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRiskAssessment_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRAConduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRAConduct.Click
    '    Try
    '        GetClickedURL("RAConduct")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRAConduct_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRAViewSummarySheet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRAViewSummarySheet.Click
    '    Try
    '        GetClickedURL("RAViewSummarySheet")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRAViewSummarySheet_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnRAMonitor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnRAMonitor.Click
    '    Try
    '        GetClickedURL("RAMonitor")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnRAMonitor_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnFunctionRiskReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFunctionRiskReview.Click
    '    Try
    '        GetClickedURL("FRRVDashboard")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFunctionRiskReview_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnFRRPlanningScheduling_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFRRPlanningScheduling.Click
    '    Try
    '        GetClickedURL("FRRPlanningScheduling")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFRRPlanningScheduling_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnFRRPConduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFRRPConduct.Click
    '    Try
    '        GetClickedURL("FRRPConduct")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFRRPConduct_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnFRRIssueTracker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnFRRIssueTracker.Click
    '    Try
    '        GetClickedURL("FRRIssueTracker")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFRRIssueTracker_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    ''Protected Sub lnkbtnGeneralCMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnGeneralCMaster.Click
    ''    Try
    ''        GetClickedURL("GeneralCMaster")
    ''    Catch ex As Exception
    ''        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGeneralCMaster_Click")
    ''    End Try
    ''End Sub
    'Protected Sub lnkbtnCorporateMaster_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCorporateMaster.Click
    '    Try
    '        GetClickedURL("CorporateMaster")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCorporateMaster_Click")
    '    End Try
    'End Sub
    'Protected Sub lnkbtnCorporateAuditChecklist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnCorporateAuditChecklist.Click
    '    Try
    '        GetClickedURL("CorporateAuditChecklist")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCorporateAuditChecklist_Click")
    '    End Try
    'End Sub
    'Private Sub lnkbtnCAIQGenMas_Click(sender As Object, e As EventArgs) Handles lnkbtnCAIQGenMas.Click
    '    Try
    '        sSession.Menu = "CAIQAuditMaster" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Audit/CAIQGeneralMaster.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCAIQGenMas_Click")
    '    End Try
    'End Sub

    'Private Sub lnkbtnCAIQDesc_Click(sender As Object, e As EventArgs) Handles lnkbtnCAIQDesc.Click
    '    Try
    '        sSession.Menu = "CAIQAuditMaster" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Audit/CAIQdetails.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCAIQDesc_Click")
    '    End Try
    'End Sub

    'Private Sub lnkbtnCAIQAudUniverse_Click(sender As Object, e As EventArgs) Handles lnkbtnCAIQAudUniverse.Click
    '    Try
    '        sSession.Menu = "CAIQAuditMaster" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Audit/CAIQAuditUniverse.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCAIQAudUniverse_Click")
    '    End Try
    'End Sub

    'Private Sub lnkbtnCAIQExcelUpload_Click(sender As Object, e As EventArgs) Handles lnkbtnCAIQExcelUpload.Click
    '    Try
    '        sSession.Menu = "CAIQAuditMaster" : sSession.SubMenu = "" : sSession.Form = "" : Session("AllSession") = sSession
    '        Response.Redirect("~/Audit/CAIQExcelUpload.aspx", False)
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCAIQExcelUpload_Click")
    '    End Try
    'End Sub

    'Protected Sub lnkbtnBranchRiskReview_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBranchRiskReview.Click
    '    Try
    '        GetClickedURL("BRRVDashboard")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBranchRiskReview_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnBRRPlanning_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBRRPlanning.Click
    '    Try
    '        GetClickedURL("BRRPlanning")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBRRPlanning_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnBRRScheduling_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBRRScheduling.Click
    '    Try
    '        GetClickedURL("BRRScheduling")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBRRScheduling_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnBRRPConduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBRRPConduct.Click
    '    Try
    '        GetClickedURL("BRRPConduct")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBRRPConduct_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnBRRIssueTracker_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBRRIssueTracker.Click
    '    Try
    '        GetClickedURL("BRRIssueTracker")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBRRIssueTracker_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
    'Protected Sub lnkbtnBRRReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnBRRReport.Click
    '    Try
    '        GetClickedURL("BRRReport")
    '    Catch ex As Exception
    '        Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnBRRReport_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 05-08-2019
    '    End Try
    'End Sub
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

    Private Sub lnkBtnTrace360_Click(sender As Object, e As EventArgs) Handles lnkBtnTrace360.Click
        Try
            GetClickedURL("Trace360")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnTrace360_Click")
        End Try
    End Sub
    Private Sub lnkBtnAnnualPlan_Click(sender As Object, e As EventArgs) Handles lnkBtnAnnualPlan.Click
        Try
            GetClickedURL("AnnualPlan")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnAnnualPlan_Click")
        End Try
    End Sub
    Private Sub lnkbtnAdtPlnMm_Click(sender As Object, e As EventArgs) Handles lnkbtnAdtPlnMm.Click
        Try
            GetClickedURL("AdtPlnMm")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAdtPlnMm_Click")
        End Try
    End Sub
    Private Sub lnkbtnTimeBudget_Click(sender As Object, e As EventArgs) Handles lnkbtnTimeBudget.Click
        Try
            GetClickedURL("TimeBudget")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnTimeBudget_Click")
        End Try
    End Sub
    Private Sub lnkbtnCostBudget_Click(sender As Object, e As EventArgs) Handles lnkbtnCostBudget.Click
        Try
            GetClickedURL("CostBudget")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCostBudget_Click")
        End Try
    End Sub
    Private Sub lnkbtnAdtPlnSignOff_Click(sender As Object, e As EventArgs) Handles lnkbtnAdtPlnSignOff.Click
        Try
            GetClickedURL("AdtPlnSignOff")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAdtPlnSignOff_Click")
        End Try
    End Sub
    Private Sub lnkBtnWorkPaper_Click(sender As Object, e As EventArgs) Handles lnkBtnWorkPaper.Click
        Try
            GetClickedURL("WorkPaper")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnWorkPaper_Click")
        End Try
    End Sub
    Private Sub lnkBtnIssueTracker_Click(sender As Object, e As EventArgs) Handles lnkBtnIssueTracker.Click
        Try
            GetClickedURL("IssueTracker")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnIssueTracker_Click")
        End Try
    End Sub
    Private Sub lnkBtnRiskAssment_Click(sender As Object, e As EventArgs) Handles lnkBtnRiskAssment.Click
        Try
            GetClickedURL("RiskAssment")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnRiskAssment_Click")
        End Try
    End Sub
    Private Sub lnkBtnAdtProgress_Click(sender As Object, e As EventArgs) Handles lnkBtnAdtProgress.Click
        Try
            GetClickedURL("AdtProgress")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnAdtProgress_Click")
        End Try
    End Sub
    Private Sub lnkBtnTimeSheetEntry_Click(sender As Object, e As EventArgs) Handles lnkBtnTimeSheetEntry.Click
        Try
            GetClickedURL("TimeSheetEntry")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnTimeSheetEntry_Click")
        End Try
    End Sub
    Private Sub lnkbtnTimeSheetVar_Click(sender As Object, e As EventArgs) Handles lnkbtnTimeSheetVar.Click
        Try
            GetClickedURL("TimeSheetVar")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnTimeSheetVar_Click")
        End Try
    End Sub
    Private Sub lnkBtnCostSheetEntry_Click(sender As Object, e As EventArgs) Handles lnkBtnCostSheetEntry.Click
        Try
            GetClickedURL("CostSheetEntry")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCostSheetEntry_Click")
        End Try
    End Sub
    Private Sub lnkBtnCostSheetVar_Click(sender As Object, e As EventArgs) Handles lnkBtnCostSheetVar.Click
        Try
            GetClickedURL("CostSheetVar")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCostSheetVar_Click")
        End Try
    End Sub
    Private Sub lnkBtnARRep_Click(sender As Object, e As EventArgs) Handles lnkBtnARRep.Click
        Try
            GetClickedURL("ARRep")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnARRep_Click")
        End Try
    End Sub
    Private Sub lnkBtnARExecSum_Click(sender As Object, e As EventArgs) Handles lnkBtnARExecSum.Click
        Try
            GetClickedURL("ARExecSum")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnARExecSum_Click")
        End Try
    End Sub
    Private Sub lnkBtnAudClosure_Click(sender As Object, e As EventArgs) Handles lnkBtnAudClosure.Click
        Try
            GetClickedURL("AudClosure")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnAudClosure_Click")
        End Try
    End Sub
    Private Sub lnkBtnDRLLog_Click(sender As Object, e As EventArgs) Handles lnkBtnDRLLog.Click
        Try
            GetClickedURL("DRLLog")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnDRLLog_Click")
        End Try
    End Sub
    Private Sub lnkBtnDataCapt_Click(sender As Object, e As EventArgs) Handles lnkBtnDataCapt.Click
        Try
            GetClickedURL("DataCapt")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnDataCapt_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAGCMaster_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAGCMaster.Click
        Try
            GetClickedURL("CBAGCMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAGCMaster_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBACSAuditMaster_Click(sender As Object, e As EventArgs) Handles lnkBtnCBACSAuditMaster.Click
        Try
            GetClickedURL("CBACSAuditMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBACSAuditMaster_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAChkLstUpload_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAChkLstUpload.Click
        Try
            GetClickedURL("CBAChkLstUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAChkLstUpload_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAAuditDashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAAuditDashboard.Click
        Try
            GetClickedURL("CBAAuditDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAAuditDashboard_Click")
        End Try
    End Sub
    Private Sub lnkbtnCBAAUDPlanSchedule_Click(sender As Object, e As EventArgs) Handles lnkbtnCBAAUDPlanSchedule.Click
        Try
            GetClickedURL("CBAAUDPlanSchedule")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCBAAUDPlanSchedule_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAAssignAudit_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAAssignAudit.Click
        Try
            GetClickedURL("CBAAssignAudit")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAAssignAudit_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAIssueTrackerDashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAIssueTrackerDashboard.Click
        Try
            GetClickedURL("CBAIssueTrackerDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAIssueTrackerDashboard_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAFollowupDashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAFollowupDashboard.Click
        Try
            GetClickedURL("CBAFollowupDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAFollowupDashboard_Click")
        End Try
    End Sub
    Private Sub lnkBtnCBAAUDITCharts_Click(sender As Object, e As EventArgs) Handles lnkBtnCBAAUDITCharts.Click
        Try
            GetClickedURL("CBAAUDITCharts")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCBAAUDITCharts_Click")
        End Try
    End Sub
    Private Sub lnkBtnSADashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnSADashboard.Click
        Try
            GetClickedURL("SADashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnSADashboard_Click")
        End Try
    End Sub
    Private Sub lnkBtnSAConduct_Click(sender As Object, e As EventArgs) Handles lnkBtnSAConduct.Click
        Try
            GetClickedURL("SAConduct")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnSAConduct_Click")
        End Try
    End Sub
    Private Sub lnkBtnSAReport_Click(sender As Object, e As EventArgs) Handles lnkBtnSAReport.Click
        Try
            GetClickedURL("SAReport")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnSAReport_Click")
        End Try
    End Sub
    Private Sub lnkBtnCAIQGeneralMaster_Click(sender As Object, e As EventArgs) Handles lnkBtnCAIQGeneralMaster.Click
        Try
            GetClickedURL("CAIQGeneralMaster")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCAIQGeneralMaster_Click")
        End Try
    End Sub
    Private Sub lnkBtnCAIQdetails_Click(sender As Object, e As EventArgs) Handles lnkBtnCAIQdetails.Click
        Try
            GetClickedURL("CAIQdetails")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCAIQdetails_Click")
        End Try
    End Sub
    Private Sub lnkBtnCAIQAuditUniverse_Click(sender As Object, e As EventArgs) Handles lnkBtnCAIQAuditUniverse.Click
        Try
            GetClickedURL("CAIQAuditUniverse")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCAIQAuditUniverse_Click")
        End Try
    End Sub
    Private Sub lnkBtnCAIQExcelUpload_Click(sender As Object, e As EventArgs) Handles lnkBtnCAIQExcelUpload.Click
        Try
            GetClickedURL("CAIQExcelUpload")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCAIQExcelUpload_Click")
        End Try
    End Sub
    Private Sub lnkBtnCCMAuditDashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnCCMAuditDashboard.Click
        Try
            GetClickedURL("CCMAuditDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCCMAuditDashboard_Click")
        End Try
    End Sub
    Private Sub lnkBtnCCMAUDPlanSchedule_Click(sender As Object, e As EventArgs) Handles lnkBtnCCMAUDPlanSchedule.Click
        Try
            GetClickedURL("CCMAUDPlanSchedule")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCCMAUDPlanSchedule_Click")
        End Try
    End Sub
    Private Sub lnkBtnCCMAssignAudit_Click(sender As Object, e As EventArgs) Handles lnkBtnCCMAssignAudit.Click
        Try
            GetClickedURL("CCMAssignAudit")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCCMAssignAudit_Click")
        End Try
    End Sub
    Private Sub lnkBtnCCMIssueTrackerDashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnCCMIssueTrackerDashboard.Click
        Try
            GetClickedURL("CCMIssueTrackerDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCCMIssueTrackerDashboard_Click")
        End Try
    End Sub
    Private Sub lnkBtnCCMFollowupDashboard_Click(sender As Object, e As EventArgs) Handles lnkBtnCCMFollowupDashboard.Click
        Try
            GetClickedURL("CCMFollowupDashboard")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCCMFollowupDashboard_Click")
        End Try
    End Sub
    Private Sub lnkBtnCCMAUDITCharts_Click(sender As Object, e As EventArgs) Handles lnkBtnCCMAUDITCharts.Click
        Try
            GetClickedURL("CCMAUDITCharts")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnCCMAUDITCharts_Click")
        End Try
    End Sub
    Private Sub lnkBtnQAConductAudit_Click(sender As Object, e As EventArgs) Handles lnkBtnQAConductAudit.Click
        Try
            GetClickedURL("QAConductAudit")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnQAConductAudit_Click")
        End Try
    End Sub
    Private Sub lnkBtnQAAuditClosure_Click(sender As Object, e As EventArgs) Handles lnkBtnQAAuditClosure.Click
        Try
            GetClickedURL("QAAuditClosure")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkBtnQAAuditClosure_Click")
        End Try
    End Sub
End Class


