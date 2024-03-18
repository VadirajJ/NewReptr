Imports BusinesLayer
Imports System.Data
Imports System.IO
Public Class SiteMaster
    Inherits MasterPage
    Private Shared sFormName As String = "Home Masterpage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsLogin As New clsLogin
    Private Shared iPhotoAttachID As Integer
    Private Shared iPhotoDocID As Integer
    Private objclsEProfile As New clsEProfile
    Private Shared iAttachID As Integer
    Private objclsAttachments As New clsAttachments
    Private objclsGRACeCustomerModules As New clsGRACeCustomerModules
    Private objclsCPFP As New clsCPFP
    Private objUser As New clsCPFP.UserProfile
    Private Shared sSession As AllSession

    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnTRACeLog.ImageUrl = "Images/TRACe PA-Logo.svg"
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim intSessionTimeOut As Integer, intSessionTimeOutWarning As Integer
        Dim Modules() As String, ModulesList As List(Of String)
        Dim sPaths As String, sDestFilePath As String, sDestFilePath2 As String
        Dim UserLoginname As String = ""
        Try
            sSession = Session("AllSession")
            'mainForm.Action=Request.RawUrl
            intSessionTimeOut = sSession.TimeOut
            intSessionTimeOutWarning = sSession.TimeOutWarning
            lblTimeOutWarning.Text = "Your TRACe session will expire in " & (sSession.TimeOutWarning / 60000) & " mins! Please Save the data before the session expires."
            bdyProgramMaster.Attributes.Add("onload", "javascript:return checkTime(" + intSessionTimeOut.ToString + "," + intSessionTimeOutWarning.ToString + ");")
            lblUserName.Text = "User name: " & " " & sSession.UserFullNameCode

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
            sSession.Modules = sSession.Modules.TrimStart(",")
            Modules = sSession.Modules.Split(",")
            ModulesList = Modules.ToList()
            divMasters.Visible = False
            divDigitalOffice.Visible = False
            divAssignmentsMain.Visible = False
            divFA.Visible = False
            divFOA.Visible = False
            divStandAudit.Visible = False
            If ModulesList.Count > 0 Then
                If ModulesList.Contains("Masters", StringComparer.CurrentCultureIgnoreCase) = True Then
                    divMasters.Visible = True
                End If
                If ModulesList.Contains("Digital Audit Office - Financial Audit", StringComparer.CurrentCultureIgnoreCase) = True Then
                    divFOA.Visible = True
                End If
                If ModulesList.Contains("Digital Audit Office - Assignments", StringComparer.CurrentCultureIgnoreCase) = True Then
                    divAssignmentsMain.Visible = True
                End If
                If ModulesList.Contains("Audit", StringComparer.CurrentCultureIgnoreCase) = True Then
                    divStandAudit.Visible = True
                End If
                If ModulesList.Contains("Digital Audit Office - Fixed Asset", StringComparer.CurrentCultureIgnoreCase) = True Then
                    divFA.Visible = True
                End If
                If ModulesList.Contains("Digital Office", StringComparer.CurrentCultureIgnoreCase) = True Then
                    divDigitalOffice.Visible = True
                End If
            End If
            Dim AppAccesscode As HttpCookie = New HttpCookie("AppAccesscode")
            AppAccesscode = Request.Cookies("AppAccesscode")
            Dim AppLoginname As HttpCookie = New HttpCookie("AppLoginname")
            AppLoginname = Request.Cookies("AppLoginname")
            Dim ApploginID As HttpCookie = New HttpCookie("ApploginID")
            ApploginID = Request.Cookies("ApploginID")

            If sSession.AccessCode = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(AppAccesscode.Value)) Then
                If sSession.UserID = ApploginID.Value Then
                Else
                    lblUserName.Text = ""
                    sSession.AccessCode = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(AppAccesscode.Value))
                    Session("AllSession") = sSession
                    sSession.AccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sSession.AccessCode)
                    sSession.EncryptPassword = objclsCPFP.GetQuestionPassWordStatus(sSession.AccessCode, sSession.AccessCodeID, ApploginID.Value, "PassWord")
                    sSession.AuditCodeID = 0
                    sSession.IPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
                    sSession.UserID = objclsGeneralFunctions.GetUserIDFromLoginName(sSession.AccessCode, sSession.AccessCodeID, AppLoginname.Value)
                    sSession.UserLoginName = AppLoginname.Value
                    sSession.UserFullName = objclsGeneralFunctions.GetUserFullNameFromUserID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                    sSession.UserFullNameCode = objclsGeneralFunctions.GetUserNameAndCodeFromPKID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                    sSession.LastLoginDate = objclsLogin.GetLastLoginDate(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                    sSession.MaxPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")
                    sSession.MinPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                    sSession.NoOfUnSucsfAtteptts = objclsLogin.GetNoOfUnSuccssfulAttempts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                    sSession.FileSize = objclsGeneralFunctions.GetGRACeSettingValue(sSession.AccessCode, sSession.AccessCodeID, "FileSize")
                    sSession.TimeOut = objclsGeneralFunctions.GetGRACeSettingValue(sSession.AccessCode, sSession.AccessCodeID, "TimeOut") * 60000
                    sSession.TimeOutWarning = objclsGeneralFunctions.GetGRACeSettingValue(sSession.AccessCode, sSession.AccessCodeID, "TimeOutWarning") * 60000
                    sSession.NumberOfUsers = objclsGRACeCustomerModules.GetCustomerNumberOfUsers(sSession.AccessCode)
                    sSession.NumberOfCustomers = objclsGRACeCustomerModules.GetCustomerNumberFromReg(sSession.AccessCode)
                    sSession.UsrDeptID = objclsGeneralFunctions.GetUserDeptID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                    sSession.UsrCompanyID = objclsGeneralFunctions.GetCompanyID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)

                    'sSession.TimeOut=120000 : sSession.TimeOutWarning=60000
                    sSession.Modules = objclsGRACeCustomerModules.GetModules(sSession.AccessCode)
                    sSession.StartDate = "01/01/1900"
                    sSession.EndDate = "01/01/1900"
                    sSession.BrowserName = Request.Browser.Browser.ToString
                    Session("AllSession") = sSession
                    lblUserName.Text = "User name" & " " & sSession.UserFullNameCode
                End If
            Else
                lblUserName.Text = ""
                sSession.AccessCode = objclsGRACeGeneral.DecryptQueryString(HttpUtility.UrlDecode(AppAccesscode.Value))
                Session("AllSession") = sSession
                sSession.AccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sSession.AccessCode)
                sSession.EncryptPassword = objclsCPFP.GetQuestionPassWordStatus(sSession.AccessCode, sSession.AccessCodeID, ApploginID.Value, "PassWord")
                sSession.AuditCodeID = 0
                sSession.IPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
                sSession.UserID = objclsGeneralFunctions.GetUserIDFromLoginName(sSession.AccessCode, sSession.AccessCodeID, AppLoginname.Value)
                sSession.UserLoginName = AppLoginname.Value
                sSession.UserFullName = objclsGeneralFunctions.GetUserFullNameFromUserID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                sSession.UserFullNameCode = objclsGeneralFunctions.GetUserNameAndCodeFromPKID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                sSession.LastLoginDate = objclsLogin.GetLastLoginDate(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                sSession.MaxPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")
                sSession.MinPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                sSession.NoOfUnSucsfAtteptts = objclsLogin.GetNoOfUnSuccssfulAttempts(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                sSession.FileSize = objclsGeneralFunctions.GetGRACeSettingValue(sSession.AccessCode, sSession.AccessCodeID, "FileSize")
                sSession.TimeOut = objclsGeneralFunctions.GetGRACeSettingValue(sSession.AccessCode, sSession.AccessCodeID, "TimeOut") * 60000
                sSession.TimeOutWarning = objclsGeneralFunctions.GetGRACeSettingValue(sSession.AccessCode, sSession.AccessCodeID, "TimeOutWarning") * 60000
                sSession.NumberOfUsers = objclsGRACeCustomerModules.GetCustomerNumberOfUsers(sSession.AccessCode)
                sSession.NumberOfCustomers = objclsGRACeCustomerModules.GetCustomerNumberFromReg(sSession.AccessCode)
                sSession.UsrDeptID = objclsGeneralFunctions.GetUserDeptID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
                sSession.UsrCompanyID = objclsGeneralFunctions.GetCompanyID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)

                'sSession.TimeOut=120000 : sSession.TimeOutWarning=60000
                sSession.Modules = objclsGRACeCustomerModules.GetModules(sSession.AccessCode)
                sSession.StartDate = "01/01/1900"
                sSession.EndDate = "01/01/1900"
                sSession.BrowserName = Request.Browser.Browser.ToString
                Session("AllSession") = sSession
                lblUserName.Text = "User name" & " " & sSession.UserFullNameCode
            End If

            Dim UserImg As String = ""
            UserImg = objclsEProfile.LoadExistingEmployeeDetailsPhotoId(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
            iPhotoDocID = objclsEProfile.GetPhotoDocID(sSession.AccessCode, sSession.AccessCodeID, UserImg)
            sPaths = objclsGeneralFunctions.CreateWorkingDirImg(sSession.AccessCode, sSession.AccessCodeID, sSession.UserLoginName)
            sDestFilePath2 = objclsAttachments.GetDocumentPath(sSession.AccessCode, sSession.AccessCodeID, sPaths, UserImg, iPhotoDocID)
            If File.Exists(sDestFilePath2) Then
                Dim bytes As Byte() = System.IO.File.ReadAllBytes(sDestFilePath2)
                Dim imageBase64Data As String = Convert.ToBase64String(bytes)
                Dim imageDataURL As String = String.Format("data:image/png;base64,{0}", imageBase64Data)
                UserPhoto.ImageUrl = imageDataURL
                RetrievePhotoUpload.ImageUrl = imageDataURL
                ImagePhoto.ImageUrl = imageDataURL
            Else
                UserPhoto.ImageUrl = "~/Images/NoPhoto.jpg"
                ImagePhoto.ImageUrl = "~/Images/NoPhoto.jpg"
                RetrievePhotoUpload.ImageUrl = "~/Images/NoPhoto.jpg"
                'UserPhoto.Attributes.Add("style", "width:25px")
            End If

            sSession.StartDate = objclsGRACeGeneral.FormatDtForRDBMS(objclsGRACeGeneral.GetStartDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
            sSession.EndDate = objclsGRACeGeneral.FormatDtForRDBMS(objclsGRACeGeneral.GetEndDate(sSession.AccessCode, sSession.AccessCodeID, sSession.YearID), "D")
            Session("AllSession") = sSession
            GetSubMenuOpen()
            If objclsLogin.GetUserIsLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, sSession.BrowserName) = False Then
                logoutmsg.Text = "You have already logged into another session, please try to log in again! "
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgTypeLogout').addClass('alert alert-info');$('#LogoutModal').modal('show');$('#txtCurrentPasssword').focus();", True)
                Exit Sub
                'Response.Redirect("~/ConcurrentLogin.aspx", False)
                'Exit Sub
            End If
            'lnkbtnMyProfile.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('show');$('#txtCheckPassword').focus();return false;")
            'lnkbtnChangePassword.Attributes.Add("OnClick", "$('#ModalChangePassword').modal('show');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');return false;")
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load")
        End Try
    End Sub
    Private Sub GetSubMenuOpen()
        Try
            'divDigitalAudit.Attributes.Remove("class")
            'digitalauditoffice.Attributes.Remove("class")
            'fmaster.Attributes.Remove("class")
            'lnknavSchedule.Attributes.Remove("class")
            'financialaudit.Attributes.Remove("class")
            If sSession.SubMenu = "Schedules" Then
                If sSession.Form = "ScheduleFormat" Then
                    'divDigitalAudit.Attributes.Add("class", "nav-link collapsing-show")
                    'digitalauditoffice.Attributes.Add("class", "nav-link collapsing-show")
                    fmaster.Attributes.Add("class", "collapsing-show")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    lnkbtnSchedule.Attributes.Add("style", "color:#d9e8eb")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                ElseIf sSession.Form = "ScheduleDashboard" Then
                    fmaster.Attributes.Add("class", "collapsing-show")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnScheduleDashboard.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ScheduleUploadTrailbalance" Then
                    fmaster.Attributes.Add("class", "collapsing-show")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnScheduleUploadTrailbalance.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ScheduleUploadStockEntry" Then
                    fmaster.Attributes.Add("class", "collapsing-show")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnUploadStockEntry.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "Schedulenotes" Then
                    fmaster.Attributes.Add("class", "collapsing-show")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnSchedulenote.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ScheduleReport" Then
                    fmaster.Attributes.Add("class", "collapsing-show")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnScheduleReport.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "JournalEntry" Then
                    'divDigitalAudit.Attributes.Add("class", "nav-link collapsed")
                    'digitalauditoffice.Attributes.Add("class", "nav-link collapse")
                    fmaster.Attributes.Add("class", "collapse")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsing-show")
                    financialaudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnJournalEntry.Attributes.Add("style", "color:#d9e8eb")
                Else
                    'divDigitalAudit.Attributes.Add("class", "nav-link collapsed")
                    'digitalauditoffice.Attributes.Add("class", "nav-link collapse")
                    fmaster.Attributes.Add("class", "collapse")
                    lnknavSchedule.Attributes.Add("class", "nav-link collapsed")
                    financialaudit.Attributes.Add("class", "collapse")
                    lnkbtnScheduleReport.Attributes.Remove("style")
                    lnkbtnSchedulenote.Attributes.Remove("style")
                    lnkbtnUploadStockEntry.Attributes.Remove("style")
                    lnkbtnScheduleUploadTrailbalance.Attributes.Remove("style")
                    lnkbtnScheduleDashboard.Attributes.Remove("style")
                    lnkbtnSchedule.Attributes.Remove("style")
                End If
            ElseIf sSession.SubMenu = "Masters" Then
                If sSession.Form = "GRACeSettings" Then
                    'divDigitalAudit.Attributes.Add("class", "nav-link collapsing-show")
                    'digitalauditoffice.Attributes.Add("class", "nav-link collapsing-show")
                    divtrace.Attributes.Add("class", "collapsing-show")
                    lnknavTrace.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnGRACeSettings.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "OrganizationStructure" Then
                    divtrace.Attributes.Add("class", "collapsing-show")
                    lnknavTrace.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnOrganizationStructure.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "CalendarMaster" Then
                    divtrace.Attributes.Add("class", "collapsing-show")
                    lnknavTrace.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnHolidayMaster.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "CompanyDetails" Then
                    divtrace.Attributes.Add("class", "collapsing-show")
                    lnknavTrace.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnCompanyDetails.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "EmployeeMaster" Then
                    'professionals.Attributes.Add("class", "collapsing-show")
                    'lnknavProfessionals.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnEmployeeMaster.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "CustomerMaster" Then
                    'professionals.Attributes.Add("class", "collapsing-show")
                    'lnknavProfessionals.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnCustomerMaster.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "UserMaster" Then
                    'professionals.Attributes.Add("class", "collapsing-show")
                    'lnknavProfessionals.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnUserMaster.Attributes.Add("style", "color:#d9e8eb")


                ElseIf sSession.Form = "GeneralMaster" Then
                    supermaster.Attributes.Add("class", "collapsing-show")
                    lnknavSuperMaster.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnGeneralMaster.Attributes.Add("style", "color:#d9e8eb")

                ElseIf sSession.Form = "ExcelUpload" Then
                    supermaster.Attributes.Add("class", "collapsing-show")
                    lnknavSuperMaster.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnExcelUpload.Attributes.Add("style", "color:#d9e8eb")

                ElseIf sSession.Form = "AssignmentMaster" Then
                    supermaster.Attributes.Add("class", "collapsing-show")
                    lnknavSuperMaster.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnAssignmentMaster.Attributes.Add("style", "color:#d9e8eb")

                ElseIf sSession.Form = "AuditChecklist" Then
                    supermaster.Attributes.Add("class", "collapsing-show")
                    lnknavSuperMaster.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnAuditChecklist.Attributes.Add("style", "color:#d9e8eb")

                ElseIf sSession.Form = "AuditLog" Then
                    divMasters.Attributes.Add("class", "nav-link collapsing-show")
                    lnknavTrace.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnLogReport.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ReportTemplateMaster" Then
                    ReportTemplate.Attributes.Add("class", "collapsing-show")
                    lnknavRptTemp.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnReportTemplateMaster.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ReportContentMaster" Then
                    ReportTemplate.Attributes.Add("class", "collapsing-show")
                    lnknavRptTemp.Attributes.Add("class", "nav-link collapsing-show")
                    master.Attributes.Add("class", "collapsing-show")
                    lnkbtnReportContentMaster.Attributes.Add("style", "color:#d9e8eb")
                End If
            ElseIf sSession.SubMenu = "FixedAssets" Then
                If sSession.Form = "AssetSetUp" Then
                    'divDigitalAudit.Attributes.Add("class", "nav-link collapsing-show")
                    'digitalauditoffice.Attributes.Add("class", "nav-link collapsing-show")
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    mastersettings.Attributes.Add("class", "collapsing-show")
                    lnkbtnLocationSetUp.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "AssetCreation" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    mastersettings.Attributes.Add("class", "collapsing-show")
                    lnkbtnAssetRegister.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "AssetAddition/Revaluation" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    assettransaction.Attributes.Add("class", "collapsing-show")
                    lnkbtnAssetAdditionDashBoard.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "AssetDeletion" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    assettransaction.Attributes.Add("class", "collapsing-show")
                    lnkbtnAssetDeletionDashboard.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "DepreciationComputation" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    lnkbtnDepreciationComputation.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "Report" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    freports.Attributes.Add("class", "collapsing-show")
                    lnkbtnFXAPhysicalReport.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "DynamicReport" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    freports.Attributes.Add("class", "collapsing-show")
                    lnkbtnFXADynamicReport.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "AssetwiseReport" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    freports.Attributes.Add("class", "collapsing-show")
                    lnkbtnFXAInvReport.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ConsolidatedReport" Then
                    divFA.Attributes.Add("class", "nav-link collapsing-show")
                    lnkNavFixedAsset.Attributes.Add("class", "nav-link collapsing-show")
                    fixedasset.Attributes.Add("class", "collapsing-show")
                    freports.Attributes.Add("class", "collapsing-show")
                    lnkConsolidatedReport.Attributes.Add("style", "color:#d9e8eb")
                End If

            ElseIf sSession.SubMenu = "StandardAudit" Then
                If sSession.Form = "AnnualPlan" Then
                    'divDigitalAudit.Attributes.Add("class", "nav-link collapsing-show")
                    'digitalauditoffice.Attributes.Add("class", "nav-link collapsing-show")
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnAnnualPlan.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "SADashboard" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnSADashboard.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "SAScheduleAudit" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnSAScheduleAudit.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "DRL" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtSamplings.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ConductAudit" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnCA.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "TrialBalanceReview" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnTrialBalanceReview.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "AuditSummary" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnAuditSummary.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "DigitalfillingRptGen" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnDigitalfillingRptGen.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "FinalAuditReport" Then
                    StandAudit.Attributes.Add("class", "collapsing-show")
                    lnkbtnFinalAuditReport.Attributes.Add("style", "color:#d9e8eb")
                End If

            ElseIf sSession.SubMenu = "Assignment" Then
                If sSession.Form = "AssignmentsDashboard" Then
                    Assignments.Attributes.Add("class", "collapsing-show")
                    lnkbtnAssignmentDashboard.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ComplianceAsgtask" Then
                    Assignments.Attributes.Add("class", "collapsing-show")
                    lnkbtnComplianceAsgtask.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ScheduleAssignments" Then
                    Assignments.Attributes.Add("class", "collapsing-show")
                    lnkbtnScheduleAssignments.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "ITReturnsFiling" Then
                    Assignments.Attributes.Add("class", "collapsing-show")
                    lnkbtnITReturnsFiling.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "DynamicReports" Then
                    Assignments.Attributes.Add("class", "collapsing-show")
                    lnkbtnDynamicReports.Attributes.Add("style", "color:#d9e8eb")
                ElseIf sSession.Form = "Invoice" Then
                    Assignments.Attributes.Add("class", "collapsing-show")
                    lnkbtnInvoice.Attributes.Add("style", "color:#d9e8eb")
                End If
            ElseIf sSession.SubMenu = "Homepage" Then
                lnkbtnHomePages.Attributes.Add("style", "color:#d9e8eb")
            End If
        Catch ex As Exception
            Throw
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
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Home", "User Profile", "Profile Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
            lblValidationMsg.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
        Catch ex As Exception
            lblUPError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnUpdateUserProfile_Click")
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
                objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Home", "Change Password", "Password Updated", sSession.UserID, sSession.UserFullName, 0, "", sSession.IPAddress)
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
    Protected Sub lnkbtnLogout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnLogout.Click
        Try
            If (sSession.UserID) <> 0 Then
                objclsLogin.UpdateLogoff(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
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

    Private Sub lnkbtnSchedule_Click(sender As Object, e As EventArgs) Handles lnkbtnSchedule.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "ScheduleFormat"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/Schedules.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSchedule_Click")
        End Try
    End Sub

    Private Sub lnkbtnScheduleDashboard_Click(sender As Object, e As EventArgs) Handles lnkbtnScheduleDashboard.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "ScheduleDashboard"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/ScheduleDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleDashboard_Click")
        End Try
    End Sub

    Private Sub lnkbtnScheduleUploadTrailbalance_Click(sender As Object, e As EventArgs) Handles lnkbtnScheduleUploadTrailbalance.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "ScheduleUploadTrailbalance"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/UploadTrailbalanceSchedule.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleUploadTrailbalance_Click")
        End Try
    End Sub

    Private Sub lnkbtnUploadStockEntry_Click(sender As Object, e As EventArgs) Handles lnkbtnUploadStockEntry.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "ScheduleUploadStockEntry"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/UploadStockEntry.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnUploadStockEntry_Click")
        End Try
    End Sub

    Private Sub lnkbtnSchedulenote_Click(sender As Object, e As EventArgs) Handles lnkbtnSchedulenote.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "Schedulenotes"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/Schedulenote.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSchedulenote_Click")
        End Try
    End Sub

    Private Sub lnkbtnScheduleReport_Click(sender As Object, e As EventArgs) Handles lnkbtnScheduleReport.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "ScheduleReport"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/ScheduleReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSchedulenote_Click")
        End Try
    End Sub
    Private Sub lnkbtnGRACeSettings_Click(sender As Object, e As EventArgs) Handles lnkbtnGRACeSettings.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "GRACeSettings"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/GRACeSettings.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGRACeSettings")
        End Try
    End Sub
    Private Sub lnkbtnOrganizationStructure_Click(sender As Object, e As EventArgs) Handles lnkbtnOrganizationStructure.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "OrganizationStructure"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/OrganizationStructure.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnOrganizationStructure_Click")
        End Try
    End Sub
    Private Sub lnkbtnHolidayMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnHolidayMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "CalendarMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/HolidayMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHolidayMaster_Click")
        End Try
    End Sub
    Private Sub lnkbtnCompanyDetails_Click(sender As Object, e As EventArgs) Handles lnkbtnCompanyDetails.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "CompanyDetails"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/CompanyDetails.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCompanyDetails_Click")
        End Try
    End Sub

    Private Sub lnkbtnHomePages_Click(sender As Object, e As EventArgs) Handles lnkbtnHomePages.Click
        Try
            sSession.SubMenu = "Homepage" : sSession.Form = "Homepage"
            Session("AllSession") = sSession
            Response.Redirect("~/HomePages/Home.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHomePages_Click")
        End Try
    End Sub

    Private Sub lnkbtnEmployeeMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnEmployeeMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "EmployeeMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/EmployeeMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnEmployeeMaster_Click")
        End Try
    End Sub

    Private Sub lnkbtnCustomerMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnCustomerMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "CustomerMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/CustomerMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCustomerMaster_Click")
        End Try
    End Sub

    Private Sub lnkbtnUserMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnUserMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "UserMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/UserMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnUserMaster_Click")
        End Try
    End Sub
    Private Sub lnkbtnLocationSetUp_Click(sender As Object, e As EventArgs) Handles lnkbtnLocationSetUp.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "AssetSetUp"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/LocationSetUp.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLocationSetUp_Click")
        End Try
    End Sub

    Private Sub lnkbtnAssetRegister_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetRegister.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "AssetCreation"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetRegister.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetRegister_Click")
        End Try
    End Sub

    Private Sub lnkbtnAssetAdditionDashBoard_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetAdditionDashBoard.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "AssetAddition/Revaluation"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetAdditionDashBoard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetAdditionDashBoard_Click")
        End Try
    End Sub

    Private Sub lnkbtnAssetDeletionDashboard_Click(sender As Object, e As EventArgs) Handles lnkbtnAssetDeletionDashboard.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "AssetDeletion"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/AssetDeletionDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssetDeletionDashboard_Click")
        End Try
    End Sub

    Private Sub lnkbtnDepreciationComputation_Click(sender As Object, e As EventArgs) Handles lnkbtnDepreciationComputation.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "DepreciationComputation"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/DepreciationComputation.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDepreciationComputation_Click")
        End Try
    End Sub

    Private Sub lnkbtnFXAPhysicalReport_Click(sender As Object, e As EventArgs) Handles lnkbtnFXAPhysicalReport.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "Report"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXAPhysicalReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFXAPhysicalReport_Click")
        End Try
    End Sub

    Private Sub lnkbtnFXADynamicReport_Click(sender As Object, e As EventArgs) Handles lnkbtnFXADynamicReport.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "DynamicReport"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXADynamicReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFXADynamicReport_Click")
        End Try
    End Sub

    Private Sub lnkbtnFXAInvReport_Click(sender As Object, e As EventArgs) Handles lnkbtnFXAInvReport.Click
        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "AssetwiseReport"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FXAInvReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFXAInvReport_Click")
        End Try
    End Sub

    Private Sub lnkConsolidatedReport_Click(sender As Object, e As EventArgs) Handles lnkConsolidatedReport.Click

        Try
            sSession.SubMenu = "FixedAssets" : sSession.Form = "ConsolidatedReport"
            Session("AllSession") = sSession
            Response.Redirect("~/FixedAsset/FixedReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkConsolidatedReport_Click")
        End Try
    End Sub

    Private Sub lnkbtnLogReport_Click(sender As Object, e As EventArgs) Handles lnkbtnLogReport.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "AssetwiseReport"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/AuditLog.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnLogReport_Click")
        End Try
    End Sub


    Private Sub lnkbtnGeneralMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnGeneralMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "GeneralMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/GeneralMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnGeneralMaster_Click")
        End Try
    End Sub

    Private Sub lnkbtnExcelUpload_Click(sender As Object, e As EventArgs) Handles lnkbtnExcelUpload.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "ExcelUpload"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/ExcelUpload.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcelUpload_Click")
        End Try
    End Sub

    Private Sub lnkbtnAssignmentMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnAssignmentMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "AssignmentMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/AssignmentMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssignmentMaster_Click")
        End Try
    End Sub

    Private Sub lnkbtnAuditChecklist_Click(sender As Object, e As EventArgs) Handles lnkbtnAuditChecklist.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "AuditChecklist"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/AuditChecklist.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAuditChecklist_Click")
        End Try
    End Sub
    Private Sub lnkbtnReportContentMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnReportContentMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "ReportContentMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/ReportContentMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnReportContentMaster_Click")
        End Try
    End Sub
    Private Sub lnkbtnReportTemplateMaster_Click(sender As Object, e As EventArgs) Handles lnkbtnReportTemplateMaster.Click
        Try
            sSession.SubMenu = "Masters" : sSession.Form = "ReportTemplateMaster"
            Session("AllSession") = sSession
            Response.Redirect("~/Masters/ReportTemplateMaster.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnReportTemplateMaster_Click")
        End Try
    End Sub
    Private Sub lnkbtnSADashboard_Click(sender As Object, e As EventArgs) Handles lnkbtnSADashboard.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "SADashboard"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/DashboardAndSchedule.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSADashboard_Click")
        End Try
    End Sub
    Private Sub lnkbtnSAScheduleAudit_Click(sender As Object, e As EventArgs) Handles lnkbtnSAScheduleAudit.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "SADashboard"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/DashboardAndScheduleDeatils.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnSAScheduleAudit_Click")
        End Try
    End Sub
    Private Sub lnkbtnAnnualPlan_Click(sender As Object, e As EventArgs) Handles lnkbtnAnnualPlan.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "AnnualPlan"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/AnnualPlan.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAnnualPlan_Click")
        End Try
    End Sub
    Private Sub lnkbtnTrialBalanceReview_Click(sender As Object, e As EventArgs) Handles lnkbtnTrialBalanceReview.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "TrialBalanceReview"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/frmAuditLedgerUpload.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnTrialBalanceReview_Click")
        End Try
    End Sub
    Private Sub lnkbtnCA_Click(sender As Object, e As EventArgs) Handles lnkbtnCA.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "ConductAudit"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/ConductAudit.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCA_Click")
        End Try
    End Sub

    Private Sub lnkbtSamplings_Click(sender As Object, e As EventArgs) Handles lnkbtSamplings.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "DRL"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/DRLSampling.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtSamplings_Click")
        End Try
    End Sub
    Private Sub lnkbtnJournalEntry_Click(sender As Object, e As EventArgs) Handles lnkbtnJournalEntry.Click
        Try
            sSession.SubMenu = "Schedules" : sSession.Form = "JournalEntry"
            Session("AllSession") = sSession
            Response.Redirect("~/FIN Statement/JournalEntry.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnJournalEntry_Click")
        End Try
    End Sub
    Private Sub lnkbtnAuditSummary_Click(sender As Object, e As EventArgs) Handles lnkbtnAuditSummary.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "AuditSummary"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/AuditSummary.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAuditSummary_Click")
        End Try
    End Sub
    Private Sub lnkbtnDigitalfillingRptGen_Click(sender As Object, e As EventArgs) Handles lnkbtnDigitalfillingRptGen.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "DigitalfillingRptGen"
            Session("AllSession") = sSession
            Response.Redirect("~/DigitalFilling/ReportGeneration.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDigitalfillingRptGen_Click")
        End Try
    End Sub
    Private Sub lnkbtnFinalAuditReport_Click(sender As Object, e As EventArgs) Handles lnkbtnFinalAuditReport.Click
        Try
            sSession.SubMenu = "StandardAudit" : sSession.Form = "FinalAuditReport"
            Session("AllSession") = sSession
            Response.Redirect("~/StandardAudit/FinalAuditReport.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnFinalAuditReport_Click")
        End Try
    End Sub
    Private Sub lnkbtnAssignmentDashboard_Click(sender As Object, e As EventArgs) Handles lnkbtnAssignmentDashboard.Click
        Try
            sSession.SubMenu = "Assignment" : sSession.Form = "AssignmentsDashboard"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/AssignmentsDashboard.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnAssignmentDashboard_Click")
        End Try
    End Sub
    Private Sub lnkbtnComplianceAsgtask_Click(sender As Object, e As EventArgs) Handles lnkbtnComplianceAsgtask.Click
        Try
            sSession.SubMenu = "Assignment" : sSession.Form = "ComplianceAsgtask"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/ComplianceAsgtask.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnCompdashboard_Click")
        End Try
    End Sub
    Private Sub lnkbtnScheduleAssignments_Click(sender As Object, e As EventArgs) Handles lnkbtnScheduleAssignments.Click
        Try
            sSession.SubMenu = "Assignment" : sSession.Form = "ScheduleAssignments"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/ScheduleAssignments.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnScheduleAssignments_Click")
        End Try
    End Sub
    Private Sub lnkbtnITReturnsFiling_Click(sender As Object, e As EventArgs) Handles lnkbtnITReturnsFiling.Click
        Try
            sSession.SubMenu = "Assignment" : sSession.Form = "ITReturnsFiling"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/ITReturnsFiling.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnITReturnsFiling_Click")
        End Try
    End Sub
    Private Sub lnkbtnDynamicReports_Click(sender As Object, e As EventArgs) Handles lnkbtnDynamicReports.Click
        Try
            sSession.SubMenu = "Assignment" : sSession.Form = "DynamicReports"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/DynamicReports.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnDynamicReports_Click")
        End Try
    End Sub
    Private Sub lnkbtnInvoice_Click(sender As Object, e As EventArgs) Handles lnkbtnInvoice.Click
        Try
            sSession.SubMenu = "Assignment" : sSession.Form = "Invoice"
            Session("AllSession") = sSession
            Response.Redirect("~/Assignment/Invoice.aspx", False)
        Catch ex As Exception
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnInvoice_Click")
        End Try
    End Sub

    Private Sub btnlogout_ServerClick(sender As Object, e As EventArgs) Handles btnlogout.ServerClick
        Try
            Response.Redirect("~/Loginpage.aspx", False)
        Catch ex As Exception

        End Try
    End Sub
End Class