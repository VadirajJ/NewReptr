Imports System
Imports System.IO
Imports BusinesLayer
Imports System.Net
Imports System.Web
Imports System.Diagnostics
Imports System.Net.Dns
Imports System.Security.Cryptography
Imports System.Web.Configuration
Imports System.Configuration
Imports System.Web.Mail
Partial Class LoginPage
    Inherits System.Web.UI.Page
    Private sFormName As String = "LoginPage"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeCustomerModules As New clsGRACeCustomerModules
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsLogin As New clsLogin
    Private objclsCPFP As New clsCPFP
    Private Shared sSession As AllSession
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)

        imgbtnLoginLog.ImageUrl = "Images/user.png"
        'Me.Form.DefaultButton = Me.imgbtnLogin.UniqueID
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                btnOKtoCP.Attributes.Add("OnClick", "$('#ModalValidation').modal('hide');$('#ModalChangePassword').modal('show');")
                btnPEAYes.Attributes.Add("OnClick", "$('#ModalValidation').modal('hide');$('#ModalChangePassword').modal('show');")
                Try
                    If IsNothing(Request.Cookies("ASP.NET_SessionId")) = False Then
                        Response.Cookies("ASP.NET_SessionId").Value = String.Empty
                        Response.Cookies("ASP.NET_SessionId").Expires = DateTime.Now.AddMonths(-60)
                    End If
                    Session.Clear() : Session.Abandon() : Session.RemoveAll()
                    If IsNothing(Request.Cookies("AuthToken")) = False Then
                        Response.Cookies("AuthToken").Value = String.Empty
                        Response.Cookies("AuthToken").Expires = DateTime.Now.AddMonths(-60)
                    End If
                Catch ex As Exception
                End Try

                If (Request.IsAuthenticated) Then
                    'ident = (CustomIdentity)this.Page.User.Identity;
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim sConStr() As String, sCheckCRconStr() As String, sPassword As String
        Dim iUserID As Integer, iAccessCodeID As Integer
        Dim sAccessCode As String, sUserName As String, sIPAddress As String

        Try
            lblError.Text = ""
            sCheckCRconStr = System.Configuration.ConfigurationManager.AppSettings.GetValues("MMCSPLCR")
            If IsNothing(sCheckCRconStr) = True Then
                lblValidationMsg.Text = "Customer Registration details not found. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
                Exit Sub
            End If

            sAccessCode = objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)
            If objclsGRACeCustomerModules.CheckCustomerRegAccessCode(sAccessCode) = False Then
                txtAccessCode.Focus()
                lblValidationMsg.Text = "Access Code not valid. Please enter valid Access Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            Try
                sConStr = System.Configuration.ConfigurationManager.AppSettings.GetValues(sAccessCode)
                If IsNothing(sConStr) = True Then
                    Dim sProvider As String = ConfigurationManager.AppSettings("Provider")
                    Dim sDataSource As String = ConfigurationManager.AppSettings("DataSource")
                    Dim sUserID As String = ConfigurationManager.AppSettings("UserID")
                    Dim spwd As String = ConfigurationManager.AppSettings("Password")
                    Dim sTrustedConnection As String = ConfigurationManager.AppSettings("TrustedConnection")

                    Dim connectionConfiguration As Configuration = WebConfigurationManager.OpenWebConfiguration("~")
                    connectionConfiguration.AppSettings.Settings.Add(sAccessCode, "Provider=" & sProvider & ";Data Source=" & sDataSource & ";Initial Catalog=" & sAccessCode & ";User Id=" & sUserID & ";Password=" & spwd & ";TRUSTED_CONNECTION=" & sTrustedConnection & "")
                    connectionConfiguration.Save(ConfigurationSaveMode.Modified)
                    ConfigurationManager.RefreshSection("AppSettings")

                    lblValidationMsg.Text = "Please login again."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                    Exit Sub
                End If
            Catch ex As Exception
                lblValidationMsg.Text = "Invalid Access Code."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End Try

            If objclsGRACeCustomerModules.CheckDatabaseExists("MMCSPLCR", sAccessCode) = False Then
                lblValidationMsg.Text = "Invalid Database. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            ' iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            iAccessCodeID = objclsGeneralFunctions.CheckAddGetAccessCodeID(sAccessCode, Server.MapPath("~"))
            If iAccessCodeID = 0 Then
                txtAccessCode.Focus()
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            Try
                Dim sPaths As String = Server.MapPath("~\" & sAccessCode & "")
                If Not Directory.Exists(sPaths) Then
                    Directory.CreateDirectory(sPaths)
                End If

                Dim dInfo As DirectoryInfo = New DirectoryInfo(sPaths)
                Dim dDataFolderSize As Double = Math.Round((CDbl(DirectorySize(dInfo)) / (1024.0 * 1024.0)), 2)
                Dim dCustDataFolderSize As Double = objclsGRACeCustomerModules.GetCustomerFolderDatasize(sAccessCode)
                If dCustDataFolderSize < dDataFolderSize Then
                    txtAccessCode.Focus()
                    lblValidationMsg.Text = "The data size limit has been exceeded in the TRACe application. Please contact Administrator."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                    Exit Sub
                End If
            Catch ex As Exception
            End Try

            sUserName = objclsGRACeGeneral.SafeSQL(txtUserName.Text.Trim)
            sPassword = objclsGRACeGeneral.SafeSQL(txtActualPassword.Value)
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            If InStr(txtUserName.Text, "'", CompareMethod.Text) <> 0 Then
                txtUserName.Focus()
                lblValidationMsg.Text = "Enter valid Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            ElseIf InStr(txtActualPassword.Value, "'", CompareMethod.Text) <> 0 Then
                txtPassword.Focus()
                lblValidationMsg.Text = "Enter valid Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtPassword').focus();", True)
                Exit Sub
            End If

            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            If iAccessCodeID = 0 Then
                txtAccessCode.Focus()
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(sAccessCode, iAccessCodeID, sUserName)
            If iUserID = 0 Then
                txtUserName.Text = String.Empty : txtPassword.Text = String.Empty
                objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, iAccessCodeID, 0, sUserName, "Invalid login name.", sIPAddress, sPassword)
                txtUserName.Focus()
                lblValidationMsg.Text = "Invalid Login Name/Password."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            Else
                objclsGRACeCustomerModules.CheckAndAddBasicCustomerDetails(sAccessCode, iAccessCodeID, iUserID, sIPAddress)
                'Check UserID(LoginID) is Approved or not.
                If objclsLogin.CheckUserApprovedOrNot(sAccessCode, iAccessCodeID, sUserName) = True Then
                    lblValidationMsg.Text = "Your Account not yet approved. Please contact system admin."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                    objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, iAccessCodeID, iUserID, sUserName, "Your account not yet approved. Please contact system admin.", sIPAddress, sPassword)
                    Exit Sub
                End If
                EnterLogin(sAccessCode, iAccessCodeID, sUserName, iUserID, sPassword, sIPAddress)
            End If

            'Dim sPaths As String = Server.MapPath("~\" & sAccessCode & "")
            'If Not Directory.Exists(sPaths) Then
            '    Directory.CreateDirectory(sPaths)
            'End If

        Catch ex As Exception
            If ex.Message.ToString.Contains("requested by the login. The login failed.") = True Then
                lblValidationMsg.Text = "Invalid database. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("SQL Server does not exist or access denied.") = True Then
                lblValidationMsg.Text = "Invalid SQL server name. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("Login failed for user") = True Then
                lblValidationMsg.Text = "Invalid SQL login/password. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            Else
                'lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnLogin_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            ' Components.AppException.LogError(sSession.AccessCode, objclsGeneralFunctions.GetLineNumber(ex), sFormName, "btnLogin_Click")
        End Try
    End Sub
    Private Sub EnterLogin(ByVal sAccessCode As String, ByVal iAccessCodeID As Integer, ByVal sUserName As String, ByVal iUserID As Integer, ByVal sPassword As String, ByVal sIPAddress As String)
        Dim iExpDay As Integer, iDays As Integer, iAlertDays As Integer, iMinPassword As Integer, iMaxPassword As Integer
        Dim sMsg As String
        Try
            Using rijAlg As New RijndaelManaged()
                rijAlg.Mode = CipherMode.CBC
                rijAlg.Padding = PaddingMode.PKCS7
                rijAlg.FeedbackSize = 128
                rijAlg.Key = Encoding.UTF8.GetBytes("8080808080808080")
                rijAlg.IV = Encoding.UTF8.GetBytes("8080808080808080")
                Dim decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV)
                Using msDecrypt As New MemoryStream(Convert.FromBase64String(sPassword))
                    Using csDecrypt As New CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)
                        Using srDecrypt As New StreamReader(csDecrypt)
                            sPassword = srDecrypt.ReadToEnd()
                        End Using
                    End Using
                End Using
            End Using
            sPassword = objclsGRACeGeneral.EncryptPassword(sPassword)
            'Dim sDPassword = objclsGRACeGeneral.DecryptPassword("Y/dxFOBvX4eROhl8mnqqZK2hUlR+KdXiXro5ZrTygWI=")
            objclsLogin = objclsLogin.CheckUserIsValid(sAccessCode, iAccessCodeID, sUserName, sPassword, sIPAddress, "NO", "NO")
            If objclsLogin.Login = True Then
                txtCurrentPasssword.Text = "" : txtConfirmPassword.Text = "" : txtNewPassword.Text = ""
                iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sAccessCode, iAccessCodeID, "Min")
                iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sAccessCode, iAccessCodeID, "Max")
                RegExpNewPwd.ValidationExpression = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{" & iMinPassword & "," & iMaxPassword & "}"
                lblCONote.Text = "Password must contain minimum " & iMinPassword & " characters, maximum " & iMaxPassword & " characters, atleast 1 uppercase alphabet, 1 lowercase alphabet, 1 number, 1 special character."
                CVCurrentPasssword.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sPassword)

                sSession.AccessCode = sAccessCode
                sSession.AccessCodeID = iAccessCodeID
                sSession.EncryptPassword = sPassword
                sSession.YearID = 0
                sSession.AuditCodeID = 0
                sSession.IPAddress = sIPAddress
                sSession.UserID = iUserID
                sSession.UserLoginName = sUserName
                sSession.UserFullName = objclsGeneralFunctions.GetUserFullNameFromUserID(sAccessCode, iAccessCodeID, iUserID)
                sSession.UserFullNameCode = objclsGeneralFunctions.GetUserNameAndCodeFromPKID(sAccessCode, iAccessCodeID, iUserID)
                sSession.LastLoginDate = objclsLogin.GetLastLoginDate(sAccessCode, iAccessCodeID, iUserID)
                sSession.MaxPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")
                sSession.MinPasswordCharacter = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                sSession.NoOfUnSucsfAtteptts = objclsLogin.GetNoOfUnSuccssfulAttempts(sAccessCode, iAccessCodeID, iUserID)
                sSession.FileSize = objclsGeneralFunctions.GetGRACeSettingValue(sAccessCode, iAccessCodeID, "FileSize")
                sSession.TimeOut = objclsGeneralFunctions.GetGRACeSettingValue(sAccessCode, iAccessCodeID, "TimeOut") * 60000
                sSession.TimeOutWarning = objclsGeneralFunctions.GetGRACeSettingValue(sAccessCode, iAccessCodeID, "TimeOutWarning") * 60000
                sSession.NumberOfUsers = objclsGRACeCustomerModules.GetCustomerNumberOfUsers(sAccessCode)
                sSession.NumberOfCustomers = objclsGRACeCustomerModules.GetCustomerNumberFromReg(sAccessCode)
                sSession.UsrDeptID = objclsGeneralFunctions.GetUserDeptID(sAccessCode, iAccessCodeID, iUserID)
                sSession.UsrCompanyID = objclsGeneralFunctions.GetCompanyID(sAccessCode, iAccessCodeID, iUserID)

                'sSession.TimeOut=120000 : sSession.TimeOutWarning=60000
                sSession.Modules = objclsGRACeCustomerModules.GetModules(sAccessCode)
                sSession.Menu = "HOME"
                sSession.ScreenWidth = Val(txtScreenWidth.Value)
                sSession.ScreenHeight = Val(txtScreenHeight.Value)
                sSession.StartDate = "01/01/1900"
                sSession.EndDate = "01/01/1900"
                sSession.BrowserName = Request.Browser.Browser.ToString
                Session("AllSession") = sSession
                Dim guid__1 As String = Guid.NewGuid().ToString()
                Session("AuthToken") = guid__1
                Response.Cookies.Add(New HttpCookie("AuthToken", guid__1))

                '-------  Check For First Attempt Login ----------
                If objclsLogin.CheckForFirstAttempt(sAccessCode, iAccessCodeID, iUserID) = True Then
                    lblOKtoCP.Text = "Please change the Password on first time login."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divOKtoCP').addClass('alert alert-warning');$('#ModalValidation').modal('hide');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('show');$('#ModalPEAYesNo').modal('hide');", True)
                    Exit Sub
                End If

                '-------  Check For Password Reset ----------
                If objclsLogin.CheckForResetPassword(sAccessCode, iAccessCodeID, iUserID) = True Then
                    lblOKtoCP.Text = "Please change the Password."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divOKtoCP').addClass('alert alert-warning');$('#ModalValidation').modal('hide');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('show');$('#ModalPEAYesNo').modal('hide');", True)
                    Exit Sub
                End If

                '------- CheckFor Not Login ---------
                iExpDay = objclsLogin.CheckForLastLogin(sAccessCode, iAccessCodeID, iUserID)
                iDays = objclsLogin.GetNotLoginDays(sAccessCode, iAccessCodeID, iUserID)
                If iExpDay >= iDays Then
                    objclsLogin.UpdateDutyStatusLock(sAccessCode, iAccessCodeID, iUserID, sIPAddress)
                    objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, iAccessCodeID, iUserID, sUserName, "Account Locked. Please contact system admin.", sIPAddress, sPassword)
                    lblValidationMsg.Text = "Account Locked. Because you have not logged into GRACe from long time. Please contact system admin."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('hide');$('#ModalPEAYesNo').modal('hide');$('#ModalForgotPassword').modal('hide');", True)
                    Exit Sub
                End If

                '------- Check for Password Expire -------
                If objclsLogin.CheckForPwdExpiry(sAccessCode, iAccessCodeID, iUserID) = False Then
                    lblOKtoCP.Text = "Your Password has expired. Please change it now."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divOKtoCP').addClass('alert alert-warning');$('#ModalValidation').modal('hide');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('show');$('#ModalPEAYesNo').modal('hide');$('#ModalForgotPassword').modal('hide');", True)
                    Exit Sub
                End If
                'objclsGeneralFunctions.CreateWorkingDir(sAccessCode, iAccessCodeID, sUserName)
                If objclsLogin.GetUserIsLogin(sAccessCode, iAccessCodeID, iUserID, sIPAddress, sSession.BrowserName) = False Then
                    sMsg = "<script language=Javascript> CheckUserLoginSystem();</script>"
                    ClientScript.RegisterStartupScript(Me.GetType(), "Msg", sMsg)
                    Exit Sub
                Else
                    objclsLogin.UpdateLoginWithStatus(sAccessCode, iAccessCodeID, iUserID, sIPAddress, sSession.BrowserName, "YES")
                    objclsLogin.UpdateLogin(sAccessCode, iAccessCodeID, iUserID, sIPAddress)
                    objclsGeneralFunctions.SaveUserLogOperations(sAccessCode, iAccessCodeID, iUserID, sUserName, "Logged In", sIPAddress, sPassword)
                    sSession.UserLoginLogPKID = objclsLogin.SaveAuditLog(sAccessCode, iAccessCodeID, iUserID)
                    sSession.UserLoginCustID = objclsLogin.GetUserCustID(sAccessCode, iAccessCodeID, iUserID)
                    Session("AllSession") = sSession
                    '------- Check for Password Expire Alert -------
                    iExpDay = objclsLogin.CheckForExpireAlert(sAccessCode, iAccessCodeID, iUserID)
                    iAlertDays = objclsLogin.GetAlertDays(sAccessCode, iAccessCodeID)
                    If (iExpDay <= iAlertDays) And (iExpDay > 0) Then
                        lblPEAYesNoMsg.Text = "Your Password will expire in " & iExpDay & " days, Do you want to change your password now?"
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divPEAYesNoMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('hide');$('#ModalYesNo').modal('hide');$('#ModalChangePassword').modal('hide');$('#ModalOKtoCP').modal('hide');$('#ModalPEAYesNo').modal('show');$('#ModalForgotPassword').modal('hide');", True)
                        Exit Sub
                    End If
                    Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppAccesscode")
                    AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                    AppAccesscodeCookie = New HttpCookie("AppLoginname")
                    AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                    AppAccesscodeCookie = New HttpCookie("ApploginID")
                    AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
                    AppAccesscodeCookie = New HttpCookie("AppAccesscode")
                    Dim sensitiveValue As String = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)))
                    AppAccesscodeCookie.Value = sensitiveValue.ToString()
                    AppAccesscodeCookie.Secure = True
                    AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                    Response.Cookies.Add(AppAccesscodeCookie)
                    AppAccesscodeCookie = New HttpCookie("AppLoginname")
                    Dim AppLoginname As String = objclsGRACeGeneral.SafeSQL(txtUserName.Text.Trim)
                    AppAccesscodeCookie.Value = AppLoginname.ToString()
                    AppAccesscodeCookie.Secure = True
                    AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                    Response.Cookies.Add(AppAccesscodeCookie)
                    AppAccesscodeCookie = New HttpCookie("ApploginID")
                    Dim ApploginID As String = iUserID
                    AppAccesscodeCookie.Value = ApploginID.ToString()
                    AppAccesscodeCookie.Secure = True
                    AppAccesscodeCookie.SameSite = SameSiteMode.Lax
                    Response.Cookies.Add(AppAccesscodeCookie)
                    Dim sUserType As String = objclsGeneralFunctions.GetUserType(sAccessCode, iAccessCodeID, iUserID)
                    If (sUserType = "U" Or sUserType = "") Then
                        Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
                    ElseIf (sUserType = "C") Then
                        Response.Redirect("~/HomePages/Cust_User_Homepage.aspx", False) 'HomePages/Home
                    Else
                        Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
                    End If
                    ' Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
                End If
            Else
                txtUserName.Text = String.Empty : txtPassword.Text = String.Empty
                txtUserName.Focus()
                lblValidationMsg.Text = objclsLogin.ErrorInLogin
                If lblValidationMsg.Text = "Invalid Login Name/Password." Then
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
                End If
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message) 'changes done on 02-08-19
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "EnterLogin" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
            'Throw
        End Try
    End Sub
    Protected Sub btnPEAYes_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalValidation').modal('hide');$('#ModalChangePassword').modal('show');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPEAYes_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnPEANo_Click(sender As Object, e As EventArgs)
        Try
            lblError.Text = ""
            Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppAccesscode")
            AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
            AppAccesscodeCookie = New HttpCookie("AppAccesscode")
            Dim sensitiveValue As String = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)))
            AppAccesscodeCookie.Value = sensitiveValue.ToString()
            AppAccesscodeCookie.Secure = True
            AppAccesscodeCookie.SameSite = SameSiteMode.Lax
            Response.Cookies.Add(AppAccesscodeCookie)
            AppAccesscodeCookie = New HttpCookie("AppLoginname")
            Dim AppLoginname As String = objclsGRACeGeneral.SafeSQL(txtUserName.Text.Trim)
            AppAccesscodeCookie.Value = AppLoginname.ToString()
            AppAccesscodeCookie.Secure = True
            AppAccesscodeCookie.SameSite = SameSiteMode.Lax
            Response.Cookies.Add(AppAccesscodeCookie)
            AppAccesscodeCookie = New HttpCookie("ApploginID")
            Dim ApploginID As String = sSession.UserID
            AppAccesscodeCookie.Value = ApploginID.ToString()
            AppAccesscodeCookie.Secure = True
            AppAccesscodeCookie.SameSite = SameSiteMode.Lax
            Response.Cookies.Add(AppAccesscodeCookie)
            Dim sUserType As String = objclsGeneralFunctions.GetUserType(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            If (sUserType = "U" Or sUserType = "") Then
                Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
            ElseIf (sUserType = "C") Then
                Response.Redirect("~/HomePages/Cust_User_Homepage.aspx", False) 'HomePages/Home
            Else
                Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
            End If

        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnPEANo_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnCPCancel_Click(sender As Object, e As EventArgs)
        Try
            txtCurrentPasssword.Text = "" : txtNewPassword.Text = "" : txtConfirmPassword.Text = ""
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalChangePassword').modal('hide');$('#myProfileModal').modal('hide');$('#ModalPassword').modal('hide');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCPCancel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")  'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub btnCPUpdate_Click(sender As Object, e As EventArgs)
        Dim iMinPassword As Integer, iMaxPassword As Integer
        Try
            lblError.Text = ""
            If txtNewPassword.Text.Equals(txtConfirmPassword.Text) Then
                If (objclsGRACeGeneral.DecryptPassword(sSession.EncryptPassword) <> txtCurrentPasssword.Text) Then
                    txtCurrentPasssword.Focus()
                    lblValidationMsg.Text = "Invalid Old Passsword."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
                End If

                iMinPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Min")
                iMaxPassword = objclsCPFP.GetPasswordMinMaxCharacter(sSession.AccessCode, sSession.AccessCodeID, "Max")

                If iMinPassword > txtNewPassword.Text.Length Then
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Password must have at least " & iMinPassword & " characters."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                If iMaxPassword < txtNewPassword.Text.Length Then
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Password is less than " & iMaxPassword & " characters."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                If objclsCPFP.checkForPasswordAlreadyExit(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID) = False Then ' txtNewPwd Replaced with sPwd
                    txtNewPassword.Focus()
                    lblValidationMsg.Text = "Enter New Passsword, different than your previous 5 passwords."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtNewPassword').focus();", True)
                    Exit Try
                End If

                objclsCPFP.SaveOldPwdHistory(sSession.AccessCode, sSession.AccessCodeID, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.UserID)
                objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
                objclsCPFP.UpdatedPasswordDetails(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text), sSession.IPAddress)
                objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserLoginName, "Password Changed.", sSession.IPAddress, objclsGRACeGeneral.EncryptPassword(txtNewPassword.Text))
                lblValidationMsg.Text = "Password Successfully Changed."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
            Else
                txtCurrentPasssword.Focus()
                lblValidationMsg.Text = "Invalid Old Passsword."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtCurrentPasssword').focus();", True)
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnCPUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19 (imgbtnUpdateChagePwd_Click)
        End Try
    End Sub
    Protected Sub btnGetPassword_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetPassword.Click
        Dim iBret As Integer, iUserID As Integer
        Dim sIPAddress As String, sPassWord As String, sAccessCode As String, iAccessCodeID As Integer
        Dim sUseremail As String
        Try
            lblError.Text = "" : lblPWD.Text = ""
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            sAccessCode = objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)
            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            If iAccessCodeID = 0 Then
                txtAccessCode.Focus()
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If
            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(sAccessCode, iAccessCodeID, objclsGRACeGeneral.SafeSQL(lblFPLogin.Text.Trim))
            iBret = objclsCPFP.CheckAnswer(sAccessCode, iAccessCodeID, objclsGRACeGeneral.EncryptPassword(objclsGRACeGeneral.SafeSQL(txtAnswer.Text)), iUserID)
            If iBret = 0 Then
                lblPWD.Text = "" : txtAnswer.Text = "" : txtAnswer.Focus()
                lblValidationMsg.Text = "Invalid Answer."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAnswer').focus();", True)
                Exit Sub
            ElseIf iBret = 1 Then
                sPassWord = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "PassWord")
                lblPWD.Text = objclsGRACeGeneral.DecryptPassword(sPassWord)
                sAccessCode = objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)
                sSession.BrowserName = Request.Browser.Browser.ToString
                sSession.UserFullName = objclsLogin.GetUserFullName(sAccessCode, iAccessCodeID, txtUserName.Text)
                sSession.UserFullNameCode = objclsLogin.GetUserNameAndCodeFromPKID(sAccessCode, iAccessCodeID, iUserID)
                sUseremail = objclsLogin.GetUserEmailid(sAccessCode, iAccessCodeID, txtUserName.Text)
                Session("AllSession") = sSession

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

                myMail.Bcc = sUseremail ' To email id
                myMail.Subject = "Trace Password for the user:" & sSession.UserFullNameCode
                myMail.BodyFormat = MailFormat.Html
                myMail.Body = "Dear   " & "  " & sSession.UserFullName & ", Your password is <b>'" & lblPWD.Text & "</b>' <br> <br>" &
                "<p style='color:red'>Please note that For security reasons, it Is recommended that to change your password As soon As possible after logging In"
                myMail.BodyEncoding = System.Text.Encoding.UTF8
                System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com" + ":" + "465"
                System.Web.Mail.SmtpMail.Send(myMail)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalForgotPassword').modal('show');", False)

                lblValidationMsg.Text = "Password has been sent to your Email"
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-success');$('#ModalValidation').modal('show');", True)
                objclsLogin.UpdateLogin(sAccessCode, iAccessCodeID, iUserID, sIPAddress)
                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalForgotPassword').modal('show');", True)
                Exit Sub
            End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "btnGetPassword_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnForgotPassword_Click(sender As Object, e As EventArgs)
        Dim sConStr() As String, sStatus As String
        Dim iUserID As Integer, iAccessCodeID As Integer
        Dim sAccessCode As String, sUserName As String, sIPAddress As String, sAnswer As String
        Try
            lblError.Text = "" : lblPWD.Text = "" : txtAnswer.Text = ""
            sAccessCode = objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)
            sUserName = objclsGRACeGeneral.SafeSQL(txtUserName.Text.Trim)
            sIPAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString()
            sConStr = System.Configuration.ConfigurationManager.AppSettings.GetValues(sAccessCode)
            If IsNothing(sConStr) = True Then
                txtAccessCode.Focus()
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If
            If InStr(txtUserName.Text, "'", CompareMethod.Text) <> 0 Then
                txtUserName.Focus()
                lblValidationMsg.Text = "Enter valid Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            End If

            iAccessCodeID = objclsGeneralFunctions.GetAccessCodeID(sAccessCode)
            If iAccessCodeID = 0 Then
                txtAccessCode.Focus()
                lblValidationMsg.Text = "Invalid Access Code. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtAccessCode').focus();", True)
                Exit Sub
            End If

            iUserID = objclsGeneralFunctions.GetUserIDFromLoginName(sAccessCode, iAccessCodeID, sUserName)
            If iUserID = 0 Then
                txtUserName.Text = String.Empty : txtPassword.Text = String.Empty
                txtUserName.Focus()
                lblValidationMsg.Text = "Invalid Login Name."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                Exit Sub
            Else
                'Check UserID(LoginID) is Approved or not.
                If objclsLogin.CheckUserApprovedOrNot(sAccessCode, iAccessCodeID, sUserName) = True Then
                    lblValidationMsg.Text = "Your Account not yet Approved. Please contact system admin."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-warning');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                    Exit Sub
                Else
                    sStatus = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "UserStatus")
                    If sStatus = "D" Then
                        lblValidationMsg.Text = "Account De-Activated. Please contact system admin."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                        Exit Sub
                    ElseIf sStatus = "B" Then
                        lblValidationMsg.Text = "Account Blocked. Please contact system admin."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                        Exit Sub
                    ElseIf sStatus = "L" Then
                        lblValidationMsg.Text = "Account Locked. Please contact system admin."
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');$('#txtUserName').focus();", True)
                        Exit Sub
                    Else
                        lblFPLogin.Text = "" : lblPWD.Text = "" : lblQue.Text = "" : txtAnswer.Text = ""
                        lblQue.Text = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "Question")
                        If lblQue.Text = "" Then
                            lblValidationMsg.Text = "Security Questions not available. Please contact system admin."
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
                            Exit Sub
                        End If
                        lblFPLogin.Text = sUserName
                        sAnswer = objclsCPFP.GetQuestionPassWordStatus(sAccessCode, iAccessCodeID, iUserID, "Answer")
                        CVAnswer.ValueToCompare = objclsGRACeGeneral.DecryptPassword(sAnswer)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#ModalForgotPassword').modal('show');$('#txtAnswer').focus();", True)
                    End If
                End If
            End If
        Catch ex As Exception
            If ex.Message.ToString.Contains("requested by the login. The login failed.") = True Then
                lblValidationMsg.Text = "Invalid database. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("SQL Server does not exist or access denied.") = True Then
                lblValidationMsg.Text = "Invalid SQL server name. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            ElseIf ex.Message.ToString.Contains("Login failed for user") = True Then
                lblValidationMsg.Text = "Invalid SQL login/password. Please contact system admin."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Modal", "$('#divMsgType').addClass('alert alert-danger');$('#ModalValidation').modal('show');", True)
            Else
                lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            End If
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnForgotPassword_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes done on 02-08-19
        End Try
    End Sub
    Protected Sub lnkbtnHomepage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtnHomepage.Click
        Try
            objclsLogin.UpdateLoginWithStatus(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress, sSession.BrowserName, "YES")
            objclsLogin.UpdateLogin(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.IPAddress)
            objclsGeneralFunctions.SaveUserLogOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, sSession.UserFullName, "Logged In", sSession.IPAddress, sSession.EncryptPassword)
            sSession.UserLoginLogPKID = objclsLogin.SaveAuditLog(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            sSession.UserLoginCustID = objclsLogin.GetUserCustID(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            Session("AntiXsrfToken") = Guid.NewGuid().ToString("N")
            Dim responseCookie As HttpCookie
            responseCookie = New HttpCookie("__AntiXsrfToken")
            responseCookie.HttpOnly = True
            responseCookie.Value = Session("AntiXsrfToken")
            Request.Cookies("ASP.NET_SessionId").Path = "/GenAudit/"
            Request.Cookies("ASP.NET_SessionId").Domain = "MMCSPL.Com"
            Response.Cookies.Add(responseCookie)
            Session("AllSession") = sSession
            Dim AppAccesscodeCookie As HttpCookie = New HttpCookie("AppAccesscode")
            AppAccesscodeCookie.Expires = DateTime.Now.AddDays(-1)
            AppAccesscodeCookie = New HttpCookie("AppAccesscode")
            Dim sensitiveValue As String = HttpUtility.UrlEncode(objclsGRACeGeneral.EncryptQueryString(objclsGRACeGeneral.SafeSQL(txtAccessCode.Text.Trim)))
            AppAccesscodeCookie.Value = sensitiveValue.ToString()
            AppAccesscodeCookie.Secure = True
            AppAccesscodeCookie.SameSite = SameSiteMode.Lax
            Response.Cookies.Add(AppAccesscodeCookie)
            AppAccesscodeCookie = New HttpCookie("AppLoginname")
            Dim AppLoginname As String = objclsGRACeGeneral.SafeSQL(txtUserName.Text.Trim)
            AppAccesscodeCookie.Value = AppLoginname.ToString()
            AppAccesscodeCookie.Secure = True
            AppAccesscodeCookie.SameSite = SameSiteMode.Lax
            Response.Cookies.Add(AppAccesscodeCookie)
            AppAccesscodeCookie = New HttpCookie("ApploginID")
            Dim ApploginID As String = sSession.UserID
            AppAccesscodeCookie.Value = ApploginID.ToString()
            AppAccesscodeCookie.Secure = True
            AppAccesscodeCookie.SameSite = SameSiteMode.Lax
            Response.Cookies.Add(AppAccesscodeCookie)
            Dim sUserType As String = objclsGeneralFunctions.GetUserType(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID)
            If (sUserType = "U" Or sUserType = "") Then
                Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
            ElseIf (sUserType = "C") Then
                Response.Redirect("~/HomePages/Cust_User_Homepage.aspx", False) 'HomePages/Home
            Else
                Response.Redirect("~/HomePages/Home.aspx", False) 'HomePages/Home
            End If
            'Response.Redirect("~/HomePages/Home.aspx", False)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnHomepage_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "")
        End Try
    End Sub
    Public Shared Function DirectorySize(ByVal dInfo As DirectoryInfo) As Long
        Dim totalSize As Long = 0
        Try
            totalSize = dInfo.EnumerateFiles().Sum(Function(file) file.Length)
            totalSize += dInfo.EnumerateDirectories().Sum(Function(dir) DirectorySize(dir))
        Catch ex As Exception
        End Try
        Return totalSize
    End Function
End Class
