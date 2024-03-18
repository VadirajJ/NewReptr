Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports DatabaseLayer
Imports BusinesLayer
Imports System.Configuration

Public Structure strLogin
    Dim iNoCases As Integer
    Dim sErrorInLogin As String
    Dim bLogin As Boolean
    Public Property NumberofCases() As Integer
        Get
            Return iNoCases
        End Get
        Set(ByVal Value As Integer)
            iNoCases = Value
        End Set
    End Property
    Public Property ErrorInLogin() As String
        Get
            ErrorInLogin = sErrorInLogin
        End Get
        Set(ByVal Value As String)
            sErrorInLogin = Value
        End Set
    End Property
    Public Property Login() As Boolean
        Get
            Login = bLogin
        End Get
        Set(ByVal Value As Boolean)
            bLogin = Value
        End Set
    End Property
End Structure
Public Class clsLogin
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private iNoCases As Integer
    Private sErrorInLogin As String
    Private bLogin As Boolean
    Public Property NumberofCases() As Integer
        Get
            Return iNoCases
        End Get
        Set(ByVal Value As Integer)
            iNoCases = Value
        End Set
    End Property
    Public Property ErrorInLogin() As String
        Get
            ErrorInLogin = sErrorInLogin
        End Get
        Set(ByVal Value As String)
            sErrorInLogin = Value
        End Set
    End Property
    Public Property Login() As String
        Get
            Login = bLogin
        End Get
        Set(ByVal Value As String)
            bLogin = Value
        End Set
    End Property
    Public Function CheckUserApprovedOrNot(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginUserName As String) As Boolean
        Dim sSql As String
        Dim sStatus As String
        Try
            sSql = "Select Usr_DutyStatus from Sad_Userdetails where Usr_loginname='" & sLoginUserName & "' And Usr_CompID = " & iACID & ""
            sStatus = objDBL.SQLExecuteScalar(sAC, sSql)
            If sStatus = "W" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckUserIsValid(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginUserName As String, ByVal sPassword As String, ByVal sIPAddress As String, ByVal sIsAD As String, sIsADValidPassword As String)
        Dim sDetails() As String
        Dim objLogin As New clsLogin
        Try
            sDetails = Split(CheckUserNameAndStatus(sAC, iACID, sLoginUserName, sPassword, sIPAddress, sIsAD, sIsADValidPassword), "|")
            Select Case sDetails(0)
                Case "0"    'Success 
                    objLogin.bLogin = True
                    objLogin.ErrorInLogin = ""
                Case "1"    'Invalid User
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "1"
                    objLogin.ErrorInLogin = "Invalid Login Name/Password."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "Invalid login name.", sIPAddress, sPassword)
                Case "2"    'Invalid Password
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "2"
                    objLogin.ErrorInLogin = "Invalid Login Name/Password."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "Invalid password.", sIPAddress, sPassword)
                Case "3"    'Blocked
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "3"
                    objLogin.ErrorInLogin = "Account Blocked. You have exceeded the number Of unsuccessful login attempts. Please contact system admin."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "Account Blocked. You have exceeded the number Of unsuccessful login attempts. Please contact system admin.", sIPAddress, sPassword)
                    If sDetails(2) = "NO" Then
                        UpdateDutyStatusBlock(sAC, iACID, sDetails(1), sIPAddress)
                    End If
                Case "4"    'De-Activated
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "4"
                    objLogin.ErrorInLogin = "Account De-Activated. Please contact system admin."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "Account Disabled. Please contact system admin.", sIPAddress, sPassword)
                Case "5"   'Waiting for Approval
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "5"
                    objLogin.ErrorInLogin = "Your Account not yet approved. Please contact system admin."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "User account Is Not approved. Please contact system admin.", sIPAddress, sPassword)
                Case "6"   'Deleted
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "6"
                    objLogin.ErrorInLogin = "Account Deleted."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "Account Deleted.", sIPAddress, sPassword)
                Case "7"   'Locked
                    objLogin.bLogin = False
                    objLogin.NumberofCases = "7"
                    objLogin.ErrorInLogin = "Account Locked. Because you have Not logged into TRACe from Long time. Please contact system admin."
                    objclsGeneralFunctions.SaveUserLogOperations(sAC, iACID, sDetails(1), sLoginUserName, "Account Locked. Please contact system admin.", sIPAddress, sPassword)
            End Select

            Return objLogin
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDutyStatusBlock(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update Sad_Userdetails Set Usr_Status='B',Usr_dutystatus ='B',Usr_IPAddress='" & sIPAddress & "',"
            sSql = sSql & "Usr_ReasonPwd_Block='Exceeded the number of unsuccessful login attempts.' where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckUserNameAndStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginUserName As String, ByVal sPassword As String, ByVal sIPAddress As String, sIsAD As String, sIsADValidPassword As String) As String
        Dim sSql As String, sDbPwd As String = "", sLoginStatus As String = "", sLogCreated As String = ""
        Dim iNoOfUnSuccessfullAttempts As Integer = 0, iUserID As Integer = 0, iIncrementAttempt As Integer = 0
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select MPS_UnSuccessfulAttempts from MST_Password_Setting Where MPS_CompID=" & iACID & ""
            iNoOfUnSuccessfullAttempts = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sLoginStatus = "0"
            sLogCreated = "YES"
            sSql = "" : sSql = "Select * from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_loginname='" & sLoginUserName & "'"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows Then
                dr.Read()
                If Not IsDBNull(dr("Usr_ID")) Then
                    iUserID = dr("Usr_ID")
                End If
                If IsDBNull(dr("Usr_NoOfUnSucsfAtteptts")) = False Then
                    iIncrementAttempt = dr("Usr_NoOfUnSucsfAtteptts") + 1
                Else
                    iIncrementAttempt = 1
                End If

                If Not IsDBNull(dr("Usr_NoOfUnSucsfAtteptts")) Then
                    If iIncrementAttempt >= iNoOfUnSuccessfullAttempts Then
                        sLoginStatus = "3"
                        sLogCreated = "NO"
                        GoTo ExitSecurity
                    End If
                End If

                If sIsAD = "YES" Then
                    If sIsADValidPassword = "YES" Then
                        sLoginStatus = "0"
                    ElseIf sIsADValidPassword = "NO" Then
                        sLoginStatus = "2"
                        GoTo ExitSecurity
                    End If
                ElseIf sIsAD = "NO" Then
                    If sPassword = dr("Usr_PassWord") Then
                        If Not IsDBNull(dr("Usr_DutyStatus")) Then
                            If dr("Usr_DutyStatus") = "B" Then 'User Blocked 
                                sLoginStatus = "3"
                                GoTo ExitSecurity
                            ElseIf dr("Usr_DutyStatus") = "D" Then 'User De-Activated 
                                sLoginStatus = "4"
                                GoTo ExitSecurity
                            ElseIf dr("Usr_DutyStatus") = "L" Then 'User Locked 
                                sLoginStatus = "7"
                                GoTo ExitSecurity
                            ElseIf dr("Usr_DutyStatus") = "W" Then 'User Waiting For Approval 
                                sLoginStatus = "5"
                                GoTo ExitSecurity
                            End If
                            sLoginStatus = "0"
                        End If
                    Else
                        sLoginStatus = "2"
                        GoTo ExitSecurity
                    End If
                Else
                    sLoginStatus = "1"
                    GoTo ExitSecurity
                End If
            End If
            dr.Close()

ExitSecurity: If sLoginStatus = "0" Then
                Return sLoginStatus & "|" & iUserID & "|" & sLogCreated
            Else
                If sLoginStatus <> "1" Then
                    sSql = "" : sSql = "Update Sad_Userdetails set Usr_Status='N',Usr_NoOfUnSucsfAtteptts=" & iIncrementAttempt & " where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
                    objDBL.SQLExecuteNonQuery(sAC, sSql)
                End If
            End If
            Return sLoginStatus & "|" & iUserID & "|" & sLogCreated
            dr = Nothing
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForFirstAttempt(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Boolean
        Dim iCount As Integer
        Dim sSql As String
        Try
            iCount = objDBL.SQLExecuteScalarInt(sAC, "Select Usr_NoOfLogin from Sad_Userdetails Where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & "")
            If iCount = 0 Then
                sSql = "Select Count(*) from Audit_log Where ADT_userID=" & iUserID & " And ADT_CompID =" & iACID & ""
                iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
                If iCount = 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForResetPassword(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Boolean
        Dim sSql As String
        Dim iCount As Integer
        Try
            sSql = "Select Usr_IsPasswordReset from Sad_Userdetails Where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)
            If iCount = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForLastLogin(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select DateDiff(D,Usr_LastLoginDate,GetDate()) as Day from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNotLoginDays(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select MPS_NotLoginDays from MST_Password_Setting Where MPS_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateDutyStatusLock(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update Sad_Userdetails set Usr_Status='L',Usr_dutystatus ='L',Usr_IPAddress='" & sIPAddress & "',"
            sSql = sSql & "Usr_ReasonPwd_Block='Not logged into TRACe from long time.' where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckForPwdExpiry(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Boolean
        Dim iExpiryDay As Integer, iDateDiff As Integer
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            iExpiryDay = objDBL.SQLExecuteScalarInt(sAC, "Select MPS_PasswordExpiryDays from MST_Password_Setting Where MPS_CompID=" & iACID & " ")
            sSql = "Select DateDiff(D,getdate(),USP_DATE + " & iExpiryDay & ") as DayDiff from Sad_UserPassword_History where USP_CompId=" & iACID & " And  USP_ID= (Select isnull(max(usp_ID)+0,0) from Sad_UserPassword_History where USP_CompId=" & iACID & " And  usp_UserID=" & iUserID & ")"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                While dr.Read()
                    If IsDBNull(dr("DayDiff")) = False Then
                        iDateDiff = dr("DayDiff")
                    End If
                    If iDateDiff > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End While
            Else
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetLastLoginDate(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Object
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select Usr_LastLoginDate from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                While dr.Read()
                    If IsDBNull(dr("Usr_LastLoginDate")) = False Then
                        Return objclsGRACeGeneral.FormatDtForRDBMS(dr("Usr_LastLoginDate"), "DT")
                    End If
                End While
            End If
            Return ""
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNoOfUnSuccssfulAttempts(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Usr_Noofunsucsfatteptts from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserFullName(ByVal sAC As String, ByVal iCompID As Integer, ByVal sLoginName As String) As String
        Dim sSql As String
        Dim userFullname As String = ""
        Try
            sSql = "Select Usr_FullName from Sad_Userdetails where usr_LoginName='" & sLoginName & "' And Usr_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserNameAndCodeFromPKID(ByVal sAC As String, ByVal iCompID As Integer, ByVal iUserID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_FullName + ' - ' + Usr_Code From Sad_UserDetails Where Usr_ID=" & iUserID & " And USR_CompID=" & iCompID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserEmailid(ByVal sAC As String, ByVal iCompID As Integer, ByVal sLoginName As String) As String
        Dim sSql As String
        Try
            sSql = "Select Usr_Email from Sad_Userdetails where usr_LoginName='" & sLoginName & "' And USR_CompID=" & iCompID & ""
            GetUserEmailid = objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateLogin(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Dim iCount As Integer
        Try
            iCount = objDBL.SQLExecuteScalarInt(sAC, "Select Usr_NoOfLogin from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & "")
            sSql = "Update Sad_Userdetails set Usr_Status='N',Usr_IPAddress='" & sIPAddress & "',Usr_NoOfUnSucsfAtteptts=0,Usr_IsPasswordReset=0,Usr_DutyStatus='A',Usr_NoOfLogin=" & iCount + 1 & ","
            sSql = sSql & "Usr_LastLoginDate=GetDate() where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveAuditLog(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Dim iADTKeyID As Integer
        Try
            sSql = "Select isnull(Max(adt_keyid),0)+1 from audit_log Where  adt_CompId=" & iACID & ""
            iADTKeyID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            sSql = "Insert into audit_log(adt_keyid,adt_userid,adt_login,ADT_CompId)values(" & iADTKeyID & "," & iUserID & ",Getdate()," & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
            Return iADTKeyID
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckForExpireAlert(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Dim iDateDiff As Integer, iExpiryDay As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            iExpiryDay = objDBL.SQLExecuteScalarInt(sAC, "Select MPS_PasswordExpiryDays from MST_Password_Setting Where MPS_CompID=" & iACID & "")
            sSql = "Select DateDiff(D,getdate(),USP_DATE + " & iExpiryDay & ") as DayDiff from Sad_UserPassword_History where USP_CompId=" & iACID & " And  USP_ID= (Select isnull(max(usp_ID)+0,0) from Sad_UserPassword_History where USP_CompId=" & iACID & " And  usp_UserID=" & iUserID & ")"
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                While dr.Read()
                    If IsDBNull(dr("DayDiff")) = False Then
                        iDateDiff = dr("DayDiff").ToString()
                    End If
                End While
            End If
            Return iDateDiff
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAlertDays(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String
        Dim iALertDays As Integer
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select MPS_PasswordExpiryAlertDays from MST_Password_Setting where MPS_CompID=" & iACID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                While dr.Read()
                    iALertDays = dr("MPS_PasswordExpiryAlertDays")
                End While
            Else
                iALertDays = 0
            End If
            Return iALertDays
        Catch ex As Exception
            Throw
        End Try
    End Function
    'Public Sub UpdateLogoff(ByVal sAC As String, ByVal iUserID As Integer)  '02/03/23 Kalyan prasad M
    '    Dim sSql As String
    '    Dim dr As OleDb.OleDbDataReader
    '    Try
    '        sSql = "Select Max(adt_keyID) As adt_keyID from audit_log where adt_userID = " & iUserID & ""
    '        dr = objDBL.SQLDataReader(sAC, sSql)
    '        If dr.HasRows = True Then
    '            dr.Read()
    '            sSql = "Update audit_log set ADT_LOGOUT=GetDate() where adt_userID = " & iUserID & " and adt_keyID=" & dr("adt_keyID") & ""
    '            objDBL.SQLExecuteNonQuery(sAC, sSql)
    '        End If
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub UpdateLogoff(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer)
        Dim sSql As String
        Dim dr As OleDb.OleDbDataReader
        Try
            sSql = "Select Max(adt_keyID) As adt_keyID from audit_log where adt_userID = " & iUserID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                sSql = "Update audit_log set ADT_LOGOUT=GetDate() where adt_userID = " & iUserID & " and adt_keyID=" & dr("adt_keyID") & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
                sSql = "" : sSql = "Update Sad_UserDetails set Usr_IsLogin='N' where USR_ID=" & iUserID & " And USR_CompanyID=" & iACID & ""
                objDBL.SQLExecuteNonQuery(sAC, sSql)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function UpdateLoginWithStatus(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal iUsrID As Integer, ByVal sIPAddress As String, ByVal sBrowser As String, ByVal IsLogin As String)
        Dim sSql As String
        Dim iCount As Integer
        Try
            iCount = objDBL.SQLExecuteScalarInt(sNameSpace, "Select USR_NoOfLogin from Sad_Userdetails where Usr_CompanyID='" & sNameSpace & "' And Usr_ID=" & iUsrID & "")
            iCount = iCount + 1
            sSql = "Update Sad_Userdetails set "
            If IsLogin = "YES" Then
                sSql = sSql & "Usr_IsLogin='Y',"
            End If
            sSql = sSql & "Usr_IPAddress='" & sIPAddress & "',Usr_Browser='" & sBrowser & "',usr_NoOfUnSucsfAtteptts=0,USR_DutyStatus='A',USR_NoOfLogin=" & iCount & ","
            sSql = sSql & "USR_LastLoginDate=GetDate() where Usr_CompID=" & iCompID & " And Usr_ID = " & iUsrID & ""
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetUserIsLogin(ByVal sNameSpace As String, ByVal iAC As Integer, ByVal iUsrID As Integer, ByVal sIPAddress As String, ByVal sBrowser As String) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select Usr_IsLogin,Usr_IPAddress,Usr_Browser From Sad_UserDetails where USR_ID=" & iUsrID & " And Usr_CompId=" & iAC & ""
            dt = objDBL.SQLExecuteDataSet(sNameSpace, sSql).Tables(0)
            If dt.Rows.Count = 1 Then
                If IsDBNull(dt.Rows(0)("Usr_IsLogin")) = False Then
                    If dt.Rows(0)("Usr_IsLogin") = "Y" Then
                        If IsDBNull(dt.Rows(0)("Usr_Browser")) = False Then
                            If dt.Rows(0)("Usr_IPAddress") = sIPAddress And dt.Rows(0)("Usr_Browser") = sBrowser Then
                                Return True
                            Else
                                Return False
                            End If
                        Else
                            Return False
                        End If

                    ElseIf dt.Rows(0)("Usr_IsLogin") = "N" Then
                        Return True
                    End If
                Else
                    Return True
                End If
            End If
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveUserLogOperations(ByVal sNameSpace As String, ByVal iCompID As Integer, ByVal sUserName As String, ByVal iUsrID As Integer, ByVal sPassword As String, ByVal sIPAddress As String)
        Dim sSql As String
        Dim iMaxID As Integer
        Try
            iMaxID = objDBL.SQLExecuteScalarInt(sNameSpace, "Select ISNULL(MAX(ALP_PKID)+1,1) From audit_log_operations")
            sSql = "Insert Into audit_log_operations (ALP_PKID,ALP_UserName,ALP_UserID,ALP_Password,ALP_Date,ALP_LogType,ALP_IPAddress,ALP_CompID)"
            sSql = sSql & "Values(" & iMaxID & ",'" & sUserName & "'," & iUsrID & ",'" & sPassword & "',GetDate(),'Logged In','" & sIPAddress & "'," & iCompID & ")"
            objDBL.SQLExecuteNonQuery(sNameSpace, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetUserCustID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As String) As String
        Dim sSql As String
        Try
            sSql = "Select usr_CompanyId from Sad_Userdetails where Usr_CompId=" & iACID & " And Usr_ID=" & iUserID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
