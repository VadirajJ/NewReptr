Imports System
Imports System.Data
Imports BusinesLayer
Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports Microsoft.Office.Interop
Imports Microsoft.Reporting.WebForms
Partial Class GRACeSettings
    Inherits System.Web.UI.Page
    Private sFormName As String = "Masters_GRACeSettingssss"
    Private objErrorClass As New BusinesLayer.Components.ErrorClass
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsApplicationSettings As New clsApplicationSettings
    Private objclsGRACePermission As New clsGRACePermission

    Private Shared sSession As AllSession
    Private Shared dtApp As New DataTable
    Private Shared dtPswd As New DataTable
    Private Shared dtES As New DataTable
    Private Shared dtGSReport As New DataTable
    Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
        MyBase.OnPreRender(e)
        Dim strDisAbleBackButton As String
        strDisAbleBackButton = "<script language=javascript>window.history.forward(1);</script>"
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "clientScript", strDisAbleBackButton)
        imgbtnUpdate.ImageUrl = "~/Images/Update24.png"
        imgbtnReport.Src = "~/Images/Download24.png"
        Me.Form.DefaultButton = Me.imgbtnUpdate.UniqueID
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Dim sFormButtons As String
        Try
            sSession = Session("AllSession")
            If IsPostBack = False Then
                'imgbtnUpdate.Visible = False : imgbtnReport.Visible = False
                'sFormButtons = objclsGRACePermission.GetLoginUserPermission(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "MRTS", 1)
                'If sFormButtons = "False" Or sFormButtons = "" Then
                '    Response.Redirect("~/Permissions/SysAdminPermission.aspx", False) 'Permissions/SysAdminPermission
                '    Exit Sub
                'Else
                '    If sFormButtons.Contains(",Save/Update,") = True Then
                '        imgbtnUpdate.Visible = True
                '    End If
                '    If sFormButtons.Contains(",Report,") = True Then
                '        imgbtnReport.Visible = True
                '    End If
                'End If

                LoadCurrencyTypeDB()
                LoadFileSize() : LoadSessionTimeOut() : LoadSessionTimeOutWarning() : LoadDateFormat() : LoadFilesDB() : LoadChkPasswordContains()
                dtApp = objclsApplicationSettings.GetApplicationSettingDetails(sSession.AccessCode, sSession.AccessCodeID)
                dtPswd = objclsApplicationSettings.GetPasswordSettings(sSession.AccessCode, sSession.AccessCodeID)
                dtES = objclsApplicationSettings.GetEmailsettings(sSession.AccessCode, sSession.AccessCodeID)
                GetPasswordDetails() : GetAppSettings() : GetEmailSettings()

                dtGSReport = objclsApplicationSettings.SettingsReport(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)

                Me.ddlFilesDB.Attributes.Add("onchange", "javascript:return ValidateFilesDB();")
                RFVImagePath.ErrorMessage = "Enter Image Path."
                    RFVErrorLog.ErrorMessage = "Enter Error Log Path."
                    RFVApplicationtempDirectory.ErrorMessage = "Enter Application Temp Directory."
                    RFVFTP.ErrorMessage = "Enter FTP Server."
                    RFVHTP.ErrorMessage = "Enter HTTP."

                    REVFileInDBPath.ValidationExpression = "^[\s\S]{0,100}$"
                    REVFileInDBPath.ErrorMessage = "Attachments File Path exceeded maximum size(only 100 characters)."

                    RFVMinNoPwdChar.ErrorMessage = "Enter Min Password Character."
                    REVMinNoPwdChar.ValidationExpression = "^[0-9]{1,10}$"
                    REVMinNoPwdChar.ErrorMessage = "Enter valid Minimum Password Character."

                    RFVMaxNoPwdChar.ErrorMessage = "Enter Max Password Character."
                    REVMaxNoPwdChar.ValidationExpression = "^[0-9]{1,100}$"
                    REVMaxNoPwdChar.ErrorMessage = "Enter valid Max Password Character."
                    CVMaxNoPwdChar.ErrorMessage = "Max Password Character should be greater than Min Password Character."

                    RFVRecovryAttempts.ErrorMessage = "Enter No. of Recovery Attempts."
                    REVRecovryAttempts.ValidationExpression = "^[0-9]{1,10}$"
                    REVRecovryAttempts.ErrorMessage = "Enter valid No. of Recovery Attempts."

                    RFVUnSuccAttempt.ErrorMessage = "Enter Unsuccessful Attempts."
                    REVUnSuccAttempt.ValidationExpression = "^[0-9]{1,10}$"
                    REVUnSuccAttempt.ErrorMessage = "Enter valid Unsuccessful Attempts."

                    RFVPasswordExpiry.ErrorMessage = "Enter Password Expiry Days."
                    REVPasswordExpiry.ValidationExpression = "^[0-9]{1,500}$"
                    REVPasswordExpiry.ErrorMessage = "Enter valid Password Expiry Days."

                    RFVAlertDays.ErrorMessage = "Enter Password Expiry Alert Days."
                    REVAlertDays.ValidationExpression = "^[0-9]{1,50}$"
                    REVAlertDays.ErrorMessage = "Enter valid Password Expiry Alert Days."

                    RFVNumberofLogin.ErrorMessage = "Enter Dormant(Not Login) Days."
                    REVNumberofLogin.ValidationExpression = "^[0-9]{1,500}$"
                    REVNumberofLogin.ErrorMessage = "Enter valid Dormant(Not Login) Days."

                    RFVIPAddress.ErrorMessage = "Enter SMTP Address."
                    REVIPAddress.ValidationExpression = "\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                    REVIPAddress.ErrorMessage = "Enter valid SMTP Address."

                    RFVSenerEID.ErrorMessage = "Enter Sender E-Mail ID."
                    REVSenerEID.ValidationExpression = "^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"
                    REVSenerEID.ErrorMessage = "Enter valid Sender E-Mail ID."

                    RFVPort.ErrorMessage = "Enter Port Number."
                    REVPort.ValidationExpression = "^[0-9]{1,4}$"
                    REVPort.ErrorMessage = "Enter valid Port Number."

                    RFVSMS.ErrorMessage = "Enter SMS Sender ID."
                    REVSMS.ValidationExpression = "\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b"
                    REVSMS.ErrorMessage = "Enter valid SMS Sender ID."

                End If
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "Page_Load" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Public Sub LoadFileSize()
        Try
            ddlFileSize.Items.Add(New ListItem("1 MB", "1"))
            ddlFileSize.Items.Add(New ListItem("2 MB", "2"))
            ddlFileSize.Items.Add(New ListItem("3 MB", "3"))
            ddlFileSize.Items.Add(New ListItem("4 MB", "4"))
            ddlFileSize.Items.Add(New ListItem("5 MB", "5"))
            ddlFileSize.Items.Add(New ListItem("6 MB", "6"))
            ddlFileSize.Items.Add(New ListItem("7 MB", "7"))
            ddlFileSize.Items.Add(New ListItem("8 MB", "8"))
            ddlFileSize.Items.Add(New ListItem("9 MB", "9"))
            ddlFileSize.Items.Add(New ListItem("10 MB", "10"))
            ddlFileSize.Items.Add(New ListItem("11 MB", "11"))
            ddlFileSize.Items.Add(New ListItem("12 MB", "12"))
            ddlFileSize.Items.Add(New ListItem("13 MB", "13"))
            ddlFileSize.Items.Add(New ListItem("14 MB", "14"))
            ddlFileSize.Items.Add(New ListItem("15 MB", "15"))
            ddlFileSize.Items.Add(New ListItem("16 MB", "16"))
            ddlFileSize.Items.Add(New ListItem("17 MB", "17"))
            ddlFileSize.Items.Add(New ListItem("18 MB", "18"))
            ddlFileSize.Items.Add(New ListItem("19 MB", "19"))
            ddlFileSize.Items.Add(New ListItem("20 MB", "20"))
            ddlFileSize.Items.Add(New ListItem("21 MB", "21"))
            ddlFileSize.Items.Add(New ListItem("22 MB", "22"))
            ddlFileSize.Items.Add(New ListItem("23 MB", "23"))
            ddlFileSize.Items.Add(New ListItem("24 MB", "24"))
            ddlFileSize.Items.Add(New ListItem("25 MB", "25"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFileSize" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub LoadSessionTimeOut()
        Try
            ddlSessionTimeOut.Items.Add(New ListItem("10 min", "10"))
            ddlSessionTimeOut.Items.Add(New ListItem("15 min", "15"))
            ddlSessionTimeOut.Items.Add(New ListItem("20 min", "20"))
            ddlSessionTimeOut.Items.Add(New ListItem("25 min", "25"))
            ddlSessionTimeOut.Items.Add(New ListItem("30 min", "30"))
            ddlSessionTimeOut.Items.Add(New ListItem("35 min", "35"))
            ddlSessionTimeOut.Items.Add(New ListItem("40 min", "40"))
            ddlSessionTimeOut.Items.Add(New ListItem("45 min", "45"))
            ddlSessionTimeOut.Items.Add(New ListItem("50 min", "50"))
            ddlSessionTimeOut.Items.Add(New ListItem("55 min", "55"))
            ddlSessionTimeOut.Items.Add(New ListItem("60 min", "60"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSessionTimeOut" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub LoadSessionTimeOutWarning()
        Try
            ddlSessionTimeOutWarning.Items.Add(New ListItem("1 min", "1"))
            ddlSessionTimeOutWarning.Items.Add(New ListItem("2 min", "2"))
            ddlSessionTimeOutWarning.Items.Add(New ListItem("3 min", "3"))
            ddlSessionTimeOutWarning.Items.Add(New ListItem("4 min", "4"))
            ddlSessionTimeOutWarning.Items.Add(New ListItem("5 min", "5"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadSessionTimeOutWarning" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub LoadDateFormat()
        Try
            ddlDateFormat.Items.Add(New ListItem("dd-mmm-yy", "1"))
            ddlDateFormat.Items.Add(New ListItem("dd/mm/yyyy", "2"))
            ddlDateFormat.Items.Add(New ListItem("mm/dd/yyyy", "3"))
            ddlDateFormat.Items.Add(New ListItem("yyyy/mm/dd", "4"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadDateFormat" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub LoadFilesDB()
        Try
            ddlFilesDB.Items.Add(New ListItem("True", "0"))
            ddlFilesDB.Items.Add(New ListItem("False", "1"))
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadFilesDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub LoadChkPasswordContains()
        Dim i As Integer
        Try
            ChkPasswordContains.Items.Add(New ListItem("Capital Letter(A-Z)", "1"))
            ChkPasswordContains.Items.Add(New ListItem("Small Letter(a-z)", "2"))
            ChkPasswordContains.Items.Add(New ListItem("Special Symbol", "3"))
            ChkPasswordContains.Items.Add(New ListItem("Integer(0-9)", "4"))

            For i = 0 To ChkPasswordContains.Items.Count - 1
                ChkPasswordContains.Items(i).Selected = True
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadChkPasswordContains" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub GetAppSettings()            'Application Settings 
        Dim DVAppADetails As New DataView(dtApp)
        Try
            DVAppADetails.Sort = "sad_Config_Key"
            Dim sImgPath As String = DVAppADetails.Find("ImgPath")
            txtImgPath.Text = DVAppADetails(sImgPath)("sad_Config_Value")
            Dim sExcelPath As String = DVAppADetails.Find("ExcelPath")
            txtExcelPath.Text = DVAppADetails(sExcelPath)("sad_Config_Value")
            Dim sFilesInDB As String = DVAppADetails.Find("FilesInDB")
            Dim sFileInDB As String = DVAppADetails(sFilesInDB)("sad_Config_Value")
            If (sFileInDB = "True") Then
                ddlFilesDB.SelectedIndex = 0
                txtFileInDBPath.Enabled = False
            ElseIf (sFileInDB = "False") Then
                ddlFilesDB.SelectedIndex = 1
                txtFileInDBPath.Enabled = True
            Else
                ddlFilesDB.SelectedIndex = 0
                txtFileInDBPath.Enabled = False
            End If
            Dim sHTP As String = DVAppADetails.Find("HTP")
            txtHTP.Text = DVAppADetails(sHTP)("sad_Config_Value")

            Dim sFtpServer As String = DVAppADetails.Find("FtpServer")
            txtFTPServer.Text = DVAppADetails(sFtpServer)("sad_Config_Value")

            Dim iCurrency As Integer = DVAppADetails.Find("Currency")
            ddlCurrency.SelectedValue = DVAppADetails(iCurrency)("sad_Config_Value")

            Dim sErrorLog As String = DVAppADetails.Find("ErrorLog")
            txtErrorLog.Text = DVAppADetails(sErrorLog)("sad_Config_Value")

            Dim dDateFormat As Integer = DVAppADetails.Find("DateFormat")
            ddlDateFormat.SelectedValue = DVAppADetails(dDateFormat)("sad_Config_Value")

            Dim iFileSize As Integer = DVAppADetails.Find("FileSize")
            ddlFileSize.SelectedValue = DVAppADetails(iFileSize)("sad_Config_Value")

            Dim iTimeOut As Integer = DVAppADetails.Find("TimeOut")
            ddlSessionTimeOut.SelectedValue = DVAppADetails(iTimeOut)("sad_Config_Value")

            Dim iTimeOutWarning As Integer = DVAppADetails.Find("TimeOutWarning")
            ddlSessionTimeOutWarning.SelectedValue = DVAppADetails(iTimeOutWarning)("sad_Config_Value")

            Dim sFileInDBPath As String = DVAppADetails.Find("FileInDBPath")
            txtFileInDBPath.Text = DVAppADetails(sFileInDBPath)("sad_Config_Value")

            Dim sFileInDBOutlookEMail As String = DVAppADetails.Find("OutlookEMail")
            txtOutLook.Text = DVAppADetails(sFileInDBOutlookEMail)("sad_Config_Value")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetAppSettings" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub GetPasswordDetails()
        Dim DVPSWDDetails As New DataView(dtPswd)
        Dim sSplitAry() As String
        Try
            txtMinNoPwdChar.Text = DVPSWDDetails(0)("MPS_MinimumChar")
            txtMaxNoPwdChar.Text = DVPSWDDetails(0)("MPS_MaximumChar")
            txtRecovryAttempts.Text = DVPSWDDetails(0)("MPS_RecoveryAttempts")
            txtUnSuccAttempt.Text = DVPSWDDetails(0)("MPS_UnSuccessfulAttempts")
            txtPasswordExpiry.Text = DVPSWDDetails(0)("MPS_PasswordExpiryDays")
            txtNumberofLogin.Text = DVPSWDDetails(0)("MPS_NotLoginDays")
            txtAlertDays.Text = DVPSWDDetails(0)("MPS_PasswordExpiryAlertDays")
            For j = 0 To ChkPasswordContains.Items.Count - 1
                sSplitAry = DVPSWDDetails(0)("MPS_Password_Contains").Split(",")
                For iIndxAry = 0 To sSplitAry.Length - 1
                    If (ChkPasswordContains.Items.Item(j).Value = sSplitAry(iIndxAry)) Then
                        ChkPasswordContains.Items(j).Selected = True
                    End If
                Next
            Next
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetPasswordDetails" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Private Sub LoadCurrencyTypeDB()
        Dim dt As New DataTable
        Try
            dt = objclsApplicationSettings.BindCurrencyType(sSession.AccessCode, sSession.AccessCodeID)
            ddlCurrency.DataSource = dt
            ddlCurrency.DataTextField = "CUR_CODE"
            ddlCurrency.DataValueField = "CUR_ID"
            ddlCurrency.DataBind()
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "LoadCurrencyTypeDB" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Public Sub GetEmailSettings()
        Dim DVESDetails As New DataView(dtES)
        Dim dt As New DataTable
        Try
            txtIPAddress.Text = DVESDetails(0)("conf_IPAddress")
            txtPort.Text = DVESDetails(0)("Conf_port")
            txtSenerEID.Text = DVESDetails(0)("Conf_From")
            txtSMS.Text = DVESDetails(0)("Conf_SenderID")
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "GetEmailSettings" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019

        End Try
    End Sub
    Protected Sub imgbtnUpdate_Click(sender As Object, e As ImageClickEventArgs) Handles imgbtnUpdate.Click
        Dim sPasswordContains As String = ""
        Dim i As Integer
        Try
            lblError.Text = ""
            If txtImgPath.Text.Trim = "" Then
                lblError.Text = "Enter Image Path."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Image Path','', 'warning');", True)
                txtImgPath.Focus()
                Exit Sub
            End If

            If txtErrorLog.Text.Trim = "" Then
                lblError.Text = "Enter Error Log Path."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Error Log Path','', 'warning');", True)
                txtErrorLog.Focus()
                Exit Sub
            End If

            If txtExcelPath.Text.Trim = "" Then
                lblError.Text = "Enter Application Temp Directory."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Application Temp Directory','', 'warning');", True)
                txtExcelPath.Focus()
                Exit Sub
            End If

            If txtFTPServer.Text.Trim = "" Then
                lblError.Text = "Enter FTP Server."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter FTP Server','', 'warning');", True)
                txtFTPServer.Focus()
                Exit Sub
            End If

            If txtHTP.Text.Trim = "" Then
                lblError.Text = "Enter HTTP." : lblError.Text = "Enter HTTP."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter HTTP','', 'warning');", True)
                txtHTP.Focus()
                Exit Sub
            End If

            If ddlFilesDB.SelectedValue = 1 Then
                txtFileInDBPath.Enabled = True
                If txtFileInDBPath.Text.Trim = "" Then
                    lblError.Text = "Enter Attachment File Path."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Attachment File Path','', 'warning');", True)
                    txtFileInDBPath.Focus()
                    Exit Sub
                ElseIf txtFileInDBPath.Text.Trim.Length > 100 Then
                    lblError.Text = "Attachments File Path exceeded maximum size(only 100 characters)."
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Attachments File Path exceeded maximum size(only 100 characters)','', 'error');", True)
                    txtFileInDBPath.Focus()
                    Exit Sub
                End If
            End If

            If txtMinNoPwdChar.Text.Trim = "" Then
                lblError.Text = "Enter Min Password Character."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Min Password Character','', 'warning');", True)
                txtMinNoPwdChar.Focus()
                Exit Sub
            ElseIf IsNumeric(txtMinNoPwdChar.Text) = False Then
                lblError.Text = "Enter valid Min Password Character(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Min Password Character(only numbers)','', 'error');", True)
                txtMinNoPwdChar.Focus()
                Exit Sub
            ElseIf txtMinNoPwdChar.Text < 4 Then
                lblError.Text = "Min Password Character should be greater than or equal 4."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Min Password Character should be greater than or equal 4','', 'error');", True)
                txtMinNoPwdChar.Focus()
                Exit Sub
            ElseIf Val(txtMinNoPwdChar.Text) > 10 Then
                lblError.Text = "Min Password Character should be less than or equal 10."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Min Password Character should be less than or equal 10','', 'error');", True)
                txtMinNoPwdChar.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_MinimumChar = objclsGRACeGeneral.SafeSQL(txtMinNoPwdChar.Text.Trim)
            End If

            If txtMaxNoPwdChar.Text.Trim = "" Then
                lblError.Text = "Enter Max Password Character."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Max Password Character','', 'warning');", True)
                txtMaxNoPwdChar.Focus()
                Exit Sub
            ElseIf IsNumeric(txtMaxNoPwdChar.Text) = False Then
                lblError.Text = "Enter valid Max Password Character(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Max Password Character(only numbers)','', 'error');", True)
                txtMaxNoPwdChar.Focus()
                Exit Sub
            ElseIf txtMaxNoPwdChar.Text = "0" Then
                lblError.Text = "Max Password Character should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Max Password Character should be greater than zero','', 'error');", True)
                txtMaxNoPwdChar.Focus()
                Exit Sub
            ElseIf Val(txtMaxNoPwdChar.Text) > 100 Then
                lblError.Text = "Max Password Character should be less than or equal 100."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Max Password Character should be less than or equal 100','', 'error');", True)
                txtMaxNoPwdChar.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_MaximumChar = objclsGRACeGeneral.SafeSQL(txtMaxNoPwdChar.Text.Trim)
            End If

            If Val(txtMinNoPwdChar.Text) > Val(txtMaxNoPwdChar.Text) Then
                lblError.Text = "Max Password Character should be greater than Minimum Password Character."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Max Password Character should be greater than Minimum Password Character','', 'error');", True)
                txtMaxNoPwdChar.Focus()
                Exit Sub
            End If

            If txtRecovryAttempts.Text.Trim = "" Then
                lblError.Text = "Enter No. of Recovery Attempts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter No. of Recovery Attempts','', 'warning');", True)
                txtRecovryAttempts.Focus()
                Exit Sub
            ElseIf IsNumeric(txtRecovryAttempts.Text) = False Then
                lblError.Text = "Enter valid No. of Recovery Attempts(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid No. of Recovery Attempts(only numbers)','', 'error');", True)
                txtRecovryAttempts.Focus()
                Exit Sub
            ElseIf txtRecovryAttempts.Text = "0" Then
                lblError.Text = "No. of Recovery Attempts should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No. of Recovery Attempts should be greater than zero','', 'error');", True)
                txtRecovryAttempts.Focus()
                Exit Sub
            ElseIf Val(txtRecovryAttempts.Text) > 10 Then
                lblError.Text = "No. of Recovery Attempts should be less than or equal 10."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('No. of Recovery Attempts should be less than or equal 10.','', 'error');", True)
                txtRecovryAttempts.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_RecoveryAttempts = objclsGRACeGeneral.SafeSQL(txtRecovryAttempts.Text.Trim)
            End If

            If txtUnSuccAttempt.Text.Trim = "" Then
                lblError.Text = "Enter Unsuccessful Attempts."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Unsuccessful Attempts','', 'warning');", True)
                txtUnSuccAttempt.Focus()
                Exit Sub
            ElseIf IsNumeric(txtUnSuccAttempt.Text) = False Then
                lblError.Text = "Enter valid Unsuccessful Attempts(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Unsuccessful Attempts(only numbers)','', 'error');", True)
                txtUnSuccAttempt.Focus()
                Exit Sub
            ElseIf txtUnSuccAttempt.Text = "0" Then
                lblError.Text = "Unsuccessful Attempts should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Unsuccessful Attempts should be greater than zero','', 'error');", True)
                txtMaxNoPwdChar.Focus()
                Exit Sub
            ElseIf Val(txtUnSuccAttempt.Text) > 10 Then
                lblError.Text = "Unsuccessful Attempts should be less than or equal 10."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Unsuccessful Attempts should be less than or equal 10','', 'error');", True)
                txtUnSuccAttempt.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_UnsuccessfulAttempts = objclsGRACeGeneral.SafeSQL(txtUnSuccAttempt.Text.Trim)
            End If

            If txtPasswordExpiry.Text.Trim = "" Then
                lblError.Text = "Enter Password Expiry Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Password Expiry Days','', 'warning');", True)
                txtPasswordExpiry.Focus()
                Exit Sub
            ElseIf IsNumeric(txtPasswordExpiry.Text.Trim) = False Then
                lblError.Text = "Enter valid Password Expiry Days (only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Password Expiry Days (only numbers)','', 'error');", True)
                txtPasswordExpiry.Focus()
                Exit Sub
            ElseIf txtPasswordExpiry.Text.Trim = "0" Then
                lblError.Text = "Password Expiry Days should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Password Expiry Days should be greater than zero','', 'error');", True)
                txtPasswordExpiry.Focus()
                Exit Sub
            ElseIf Val(txtPasswordExpiry.Text.Trim) > 500 Then
                lblError.Text = "Password Expiry Days should be less than or equal 500."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Password Expiry Days should be less than or equal 500','', 'error');", True)
                txtPasswordExpiry.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_PasswordExpiryDays = objclsGRACeGeneral.SafeSQL(txtPasswordExpiry.Text.Trim)
            End If

            If txtAlertDays.Text.Trim = "" Then
                lblError.Text = "Enter Password Expiry Alert Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Password Expiry Alert Days','', 'warning');", True)
                txtAlertDays.Focus()
                Exit Sub
            ElseIf IsNumeric(txtAlertDays.Text.Trim) = False Then
                lblError.Text = "Enter Password Expiry Alert Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Password Expiry Alert Days','', 'error');", True)
                txtAlertDays.Focus()
                Exit Sub
            ElseIf txtAlertDays.Text.Trim = "0" Then
                lblError.Text = "Password Expiry Alert Days should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Password Expiry Alert Days should be greater than zero','', 'error');", True)
                txtAlertDays.Focus()
                Exit Sub
            ElseIf Val(txtAlertDays.Text.Trim) > 50 Then
                lblError.Text = "Password Expiry Alert Days should be less than or equal 50."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Password Expiry Alert Days should be less than or equal 50','', 'error');", True)
                txtAlertDays.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_PasswordExpiryAlertDays = objclsGRACeGeneral.SafeSQL(txtAlertDays.Text.Trim)
            End If

            If txtNumberofLogin.Text.Trim = "" Then
                lblError.Text = "Enter Dormant(Not Login) Days."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Dormant(Not Login) Days','', 'warning');", True)
                txtNumberofLogin.Focus()
                Exit Sub
            ElseIf IsNumeric(txtNumberofLogin.Text.Trim) = False Then
                lblError.Text = "Enter valid Dormant(Not Login) Days(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Dormant(Not Login) Days(only numbers)','', 'error');", True)
                txtNumberofLogin.Focus()
                Exit Sub
            ElseIf txtNumberofLogin.Text.Trim = "0" Then
                lblError.Text = "Password Dormant(Not Login) Days should be greater than zero."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Password Dormant(Not Login) Days should be greater than zero','', 'error');", True)
                txtNumberofLogin.Focus()
                Exit Sub
            ElseIf Val(txtNumberofLogin.Text.Trim) > 500 Then
                lblError.Text = "Password Dormant(Not Login) Days should be less than or equal 500."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Password Dormant(Not Login) Days should be less than or equal 500','', 'error');", True)
                txtNumberofLogin.Focus()
                Exit Sub
            Else
                objclsApplicationSettings.iMPS_NotLoginDays = objclsGRACeGeneral.SafeSQL(txtNumberofLogin.Text.Trim)
            End If

            For i = 0 To ChkPasswordContains.Items.Count - 1
                If ChkPasswordContains.Items(i).Selected = True Then
                    sPasswordContains = sPasswordContains & "," & ChkPasswordContains.Items(i).Value
                End If
            Next

            If txtIPAddress.Text.Trim = "" Then
                lblError.Text = "Enter SMTP Address."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter SMTP Address','', 'warning');", True)
                txtIPAddress.Focus()
                Exit Sub
            ElseIf txtIPAddress.Text.Trim.Length > 15 Then
                lblError.Text = "SMTP Address exceeded maximum size(only 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('SMTP Address exceeded maximum size(only 15 characters)','', 'error');", True)
                txtIPAddress.Focus()
                Exit Sub
            End If
            If txtSenerEID.Text.Trim = "" Then
                lblError.Text = "Enter Sender E-Mail ID."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter Sender E-Mail ID','', 'warning');", True)
                txtSenerEID.Focus()
                Exit Sub
            ElseIf txtSenerEID.Text.Trim.Length > 200 Then
                lblError.Text = "Sender E-Mail ID exceeded maximum size(only 200 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Sender E-Mail ID exceeded maximum size(only 200 characters)','', 'error');", True)
                txtSenerEID.Focus()
                Exit Sub
            End If
            If txtPort.Text.Trim = "" Then
                lblError.Text = "Enter Port Number."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Port Number','', 'warning');", True)
                txtPort.Focus()
                Exit Sub
            ElseIf IsNumeric(txtPort.Text.Trim) = False Then
                lblError.Text = "Enter valid Port Number(only numbers)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter valid Port Number(only numbers)','', 'error');", True)
                txtPort.Focus()
                Exit Sub
            ElseIf txtPort.Text.Trim.Length > 4 Then
                lblError.Text = "Port Number exceeded maximum size(only 4 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Port Number exceeded maximum size(only 4 characters)','', 'error');", True)
                txtPort.Focus()
                Exit Sub
            End If
            If txtSMS.Text.Trim = "" Then
                lblError.Text = "Enter SMS Sender ID."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Enter SMS Sender ID','', 'warning');", True)
                txtSMS.Focus()
                Exit Sub
            ElseIf txtSMS.Text.Trim.Length > 15 Then
                lblError.Text = "SMS Sender ID exceeded maximum size(only 15 characters)."
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('SMS Sender ID exceeded maximum size(only 15 characters)','', 'error');", True)
                txtSMS.Focus()
                Exit Sub
            End If

            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "ImgPath", objclsGRACeGeneral.SafeSQL(Trim(txtImgPath.Text)), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "ErrorLog", objclsGRACeGeneral.SafeSQL(Trim(txtErrorLog.Text)), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "ExcelPath", objclsGRACeGeneral.SafeSQL(Trim(txtExcelPath.Text)), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FtpServer", objclsGRACeGeneral.SafeSQL(Trim(txtFTPServer.Text)), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "AppName", objclsGRACeGeneral.SafeSQL("TRACe"), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "RDBMS", objclsGRACeGeneral.SafeSQL("SQL"), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "HTP", objclsGRACeGeneral.SafeSQL(Trim(txtHTP.Text)), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "Currency", ddlCurrency.SelectedValue, sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "DateFormat", ddlDateFormat.SelectedValue, sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FileSize", ddlFileSize.SelectedValue, sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "TimeOut", ddlSessionTimeOut.SelectedValue, sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "TimeOutWarning", ddlSessionTimeOutWarning.SelectedValue, sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FilesInDB", ddlFilesDB.SelectedItem.Text, sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "FileInDBPath", objclsGRACeGeneral.SafeSQL(Trim(txtFileInDBPath.Text)), sSession.IPAddress, sSession.UserID, "U")
            objclsApplicationSettings.SaveApplicationSettings(sSession.AccessCode, sSession.AccessCodeID, "OutlookEMail", objclsGRACeGeneral.SafeSQL(Trim(txtOutLook.Text)), sSession.IPAddress, sSession.UserID, "U")

            sSession.FileSize = ddlFileSize.SelectedValue
            sSession.TimeOut = ddlSessionTimeOut.SelectedValue * 60000
            sSession.TimeOutWarning = ddlSessionTimeOutWarning.SelectedIndex * 60000
            ' objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Application Settings", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)

            objclsApplicationSettings.sMPS_Password_Contains = sPasswordContains
            objclsApplicationSettings.sMPS_IPAddress = sSession.IPAddress
            objclsApplicationSettings.iMPS_UpdatedBy = sSession.UserID
            objclsApplicationSettings.sMPS_Operation = "U"
            objclsApplicationSettings.iMPS_CompID = sSession.AccessCodeID
            objclsApplicationSettings.SavePasswordDetails(sSession.AccessCode, objclsApplicationSettings)
            'objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Password Management Settings", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)

            objclsApplicationSettings.sConf_IPAddress = objclsGRACeGeneral.SafeSQL(txtIPAddress.Text.Trim)
            objclsApplicationSettings.iconf_Port = objclsGRACeGeneral.SafeSQL(txtPort.Text.Trim)
            objclsApplicationSettings.sConf_From = objclsGRACeGeneral.SafeSQL(txtSenerEID.Text.Trim)
            objclsApplicationSettings.sconf_SenderID = objclsGRACeGeneral.SafeSQL(txtSMS.Text.Trim)
            objclsApplicationSettings.iconf_UpdatedBy = sSession.UserID
            objclsApplicationSettings.iconf_CompID = sSession.AccessCodeID
            objclsApplicationSettings.sConf_Status = "U"
            objclsApplicationSettings.sConf_INS_IPAddress = sSession.IPAddress
            objclsApplicationSettings.SaveEmailSettings(sSession.AccessCode, objclsApplicationSettings)
            ' objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "Email Settings", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            dtGSReport = objclsApplicationSettings.SettingsReport(sSession.AccessCode, sSession.AccessCodeID, ddlCurrency.SelectedValue)
            sSession.MaxPasswordCharacter = Val(txtMaxNoPwdChar.Text)
            sSession.MinPasswordCharacter = Val(txtMinNoPwdChar.Text)
            sSession.TimeOut = (ddlSessionTimeOut.SelectedValue) * 60000
            sSession.TimeOutWarning = (ddlSessionTimeOutWarning.SelectedValue) * 60000
            Session("AllSession") = sSession
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "TRACe Settings", "Updated", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            lblError.Text = "Successfully Updated."
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "swal('Successfully Updated','', 'success');", True)
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "imgbtnUpdate_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
    Protected Sub lnkbtnPDF_Click(sender As Object, e As EventArgs) Handles lnkbtnPDF.Click
        Dim mimeType As String = Nothing
        Try
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtGSReport)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GRACeSettings.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("PDF")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "TRACe Settings", "PDF", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=TRACeSettings" + ".pdf")
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
        Try
            ReportViewer1.Reset()
            Dim rds As New ReportDataSource("DataSet1", dtGSReport)
            ReportViewer1.LocalReport.DataSources.Add(rds)
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Master/GRACeSettings.rdlc")
            Dim RptViewer As Byte() = ReportViewer1.LocalReport.Render("Excel")
            Response.Buffer = True
            Response.Clear()
            Response.ContentType = mimeType
            objclsGeneralFunctions.SaveGRACeFormOperations(sSession.AccessCode, sSession.AccessCodeID, sSession.UserID, "Masters", "TRACe Settings", "Excel", sSession.YearID, sSession.YearName, 0, "", sSession.IPAddress)
            Response.AddHeader("content-disposition", "attachment; filename=TRACeSettings" + ".xls")
            Response.BinaryWrite(RptViewer)
            Response.Flush()
            Response.End()
            'HttpContext.Current.Response.Flush() 'Sends all currently buffered output To the client.
            'HttpContext.Current.Response.SuppressContent = True 'Gets Or sets a value indicating whether To send HTTP content To the client.
            'HttpContext.Current.ApplicationInstance.CompleteRequest() 'Causes ASP.NET To bypass all events And filtering In the HTTP pipeline chain Of execution And directly execute the EndRequest Event.
        Catch ex As Exception
            lblError.Text = objErrorClass.GetErrorMessages(sSession.AccessCode, ex.Message)
            Components.AppException.LogError(sSession.AccessCode, ex.Message, sFormName, "lnkbtnExcel_Click" & " & Error_Line = '" & objclsGeneralFunctions.GetLineNumber(ex) & "'" & "") 'changes 06-08-2019
        End Try
    End Sub
End Class
