
Imports System
Imports System.Data
Imports DatabaseLayer
Public Class clsCPFP
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Public Structure UserProfile
        Dim iUsr_ID As Integer
        Dim sUsr_Code As String
        Dim sUsr_Team As String
        Dim sUsr_fullName As String
        Dim sUsr_LoginName As String
        Dim sUsr_Password As String
        Dim sUsr_Email As String
        Dim sUsr_LevelGrp As String
        Dim sUsr_GrpOrUserLvlPerm As String
        Dim sUsr_Designation As String
        Dim sUsr_ReportingTo As String
        Dim sUsr_ReportingID As Integer
        Dim sUsr_MobileNo As String
        Dim sUsr_SkillSet As String
        Dim sUsr_ConPassword As String
        Dim iUsr_Experience As Integer
        Dim sUsr_Qualification As String
        Dim sUsr_Others As String
        Dim sUsr_SecurityQuestion As String
        Dim sUsr_Answer As String
    End Structure
    '============================================ Change Password ============================================
    Public Function GetPasswordMinMaxCharacter(ByVal sAC As String, ByVal iACID As Integer, ByVal sType As String) As Integer
        Dim sSql As String = ""
        Try
            If sType = "Min" Then
                sSql = "Select MPS_MinimumChar from MST_Password_Setting where MPS_CompID=" & iACID & ""
            ElseIf sType = "Max" Then
                sSql = "Select MPS_MaximumChar from MST_Password_Setting where MPS_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function checkForPasswordAlreadyExit(ByVal sAC As String, ByVal iACID As Integer, ByVal sEncrptPWD As String, ByVal iUserID As Integer) As Boolean
        Dim i As Integer
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select Top 5 * from  SAD_UserPassword_History Where USP_CompId=" & iACID & " And  USP_UserId=" & iUserID & " Order by USP_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                If dt.Rows(i)("USP_PASSWORD").ToString() = sEncrptPWD Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveOldPwdHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal sOldPwd As String, ByVal iUserId As Integer)
        Dim sSql As String
        Dim iPKID As Integer
        Try
            sSql = "Select IsNull(max(Usp_id),0)+1  from SAD_UserPassword_History Where USP_CompId=" & iACID & ""
            iPKID = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "insert into SAD_UserPassword_History(usp_id,usp_Userid,usp_Password,usp_date,USP_CompId)values(" & iPKID & "," & iUserId & ",'" & sOldPwd & "',GetDate(), " & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdatedPasswordDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sLoginName As String, ByVal sNewPWD As String, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update Sad_Userdetails set Usr_Status='P',Usr_IPAddress='" & sIPAddress & "',USR_UpdatedBy=" & iUserID & ","
            sSql = sSql & " USR_UpdatedOn=GetDate(),usr_PassWord='" & objclsGRACeGeneral.SafeSQL(sNewPWD) & "'"
            sSql = sSql & " Where Usr_CompId=" & iACID & " And usr_LoginName='" & objclsGRACeGeneral.SafeSQL(sLoginName) & "' "
            sSql = sSql & " and usr_Id=" & iUserID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub UpdateLogin(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer)
        Dim sSql As String
        Dim iCount As Integer
        Try
            iCount = objDBL.SQLExecuteScalarInt(sAC, "Select USR_NoOfLogin from sad_userdetails where Usr_CompId=" & iACID & " And usr_Id=" & iUserID & "")
            sSql = "Update Sad_Userdetails set Usr_Status='N',USR_NoOfLogin=" & iCount + 1 & ", USR_LastLoginDate=GetDate() where Usr_CompId=" & iACID & " And usr_Id=" & iUserID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    '============================================ Forgot Password ============================================
    Public Function GetQuestionPassWordStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Question" Then
                sSql = " Select Case When usr_Que Is Null Then '' Else usr_Que End As usr_Que from Sad_Userdetails where Usr_CompID=" & iACID & " And usr_Id=" & iUserID & ""
            ElseIf sType = "UserStatus" Then
                sSql = " Select Case When Usr_DutyStatus Is Null Then '' Else Usr_DutyStatus End As Usr_DutyStatus from Sad_Userdetails where Usr_CompID=" & iACID & " And usr_Id=" & iUserID & ""
            ElseIf sType = "PassWord" Then
                sSql = " Select Case When usr_Password Is Null Then '' Else usr_Password End As usr_Password from Sad_Userdetails where Usr_CompID=" & iACID & " And usr_Id=" & iUserID & ""
            ElseIf sType = "Answer" Then
                sSql = " Select Case When Usr_Ans Is Null Then '' Else Usr_Ans End As Usr_Ans from Sad_Userdetails where Usr_CompID=" & iACID & " And usr_Id=" & iUserID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAnswer(ByVal sAC As String, ByVal iACID As Integer, ByVal sAnswer As String, ByVal iUserID As Integer) As Integer
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from Sad_Userdetails where Usr_CompID=" & iACID & " And usr_Ans='" & sAnswer & "' and usr_Id=" & iUserID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return 1
            End If
            Return 0
        Catch ex As Exception
            Throw
        End Try
    End Function
    '=================================================== User Profile ======================================================
    Public Function CheckUserPWD(ByVal sAC As String, ByVal iACID As Integer, ByVal sLoginName As String, ByVal sPwd As String) As Boolean
        Dim sSql As String
        Try
            sSql = "Select usr_id from Sad_userDetails where Usr_CompId=" & iACID & " And usr_LoginName='" & sLoginName & "' and usr_Password='" & sPwd & "'"
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadUserprofile(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As Integer) As Object
        Dim sSql As String = ""
        Dim dr As OleDb.OleDbDataReader
        Dim dt As New DataTable
        Dim objUser As New UserProfile
        Try
            sSql = "Select * from sad_userDetails where usr_id = " & iUserId & " and Usr_CompID = " & iACID & ""
            dr = objDBL.SQLDataReader(sAC, sSql)
            If dr.HasRows = True Then
                dr.Read()
                If IsDBNull(dr("Usr_Code")) = False Then
                    objUser.sUsr_Code = dr("Usr_Code")
                End If

                If IsDBNull(dr("Usr_OrgnID")) = False Then
                    sSql = "Select org_name from Sad_Org_Structure where org_node = " & dr("Usr_OrgnID") & " and Org_CompId = " & iACID & ""
                    objUser.sUsr_Team = objDBL.SQLExecuteScalar(sAC, sSql)
                End If

                If IsDBNull(dr("Usr_fullName")) = False Then
                    objUser.sUsr_fullName = dr("Usr_fullName")
                End If

                If IsDBNull(dr("Usr_LoginName")) = False Then
                    objUser.sUsr_LoginName = dr("Usr_LoginName")
                End If

                If IsDBNull(dr("Usr_Password")) = False Then
                    objUser.sUsr_Password = dr("Usr_Password")
                End If

                If IsDBNull(dr("Usr_Email")) = False Then
                    objUser.sUsr_Email = dr("Usr_Email")
                End If

                If IsDBNull(dr("usr_LevelGrp")) = False Then
                    objUser.sUsr_LevelGrp = objDBL.SQLGetDescription(sAC, "Select mas_Description from sad_grporlvl_general_master where mas_ID = " & dr("usr_LevelGrp") & " and mas_CompID = " & iACID & "")
                End If

                If IsDBNull(dr("Usr_GrpOrUserLvlPerm")) = False Then
                    objUser.sUsr_GrpOrUserLvlPerm = dr("Usr_GrpOrUserLvlPerm")
                End If

                If IsDBNull(dr("Usr_Designation")) = False Then
                    objUser.sUsr_Designation = objDBL.SQLGetDescription(sAC, "Select mas_Description from SAD_GRPDESGN_General_Master where mas_id = " & dr("Usr_Designation") & " and Mas_CompID = " & iACID & "")
                End If

                If IsDBNull(dr("Usr_MobileNo")) = False Then
                    objUser.sUsr_MobileNo = dr("Usr_MobileNo")
                End If

                If IsDBNull(dr("Usr_SkillSet")) = False Then
                    objUser.sUsr_SkillSet = dr("Usr_SkillSet")
                End If

                If IsDBNull(dr("usr_Experience")) = False Then
                    objUser.iUsr_Experience = dr("usr_Experience")
                End If

                If IsDBNull(dr("usr_Que")) = False Then
                    objUser.sUsr_SecurityQuestion = dr("usr_Que")
                End If

                If IsDBNull(dr("Usr_Ans")) = False Then
                    objUser.sUsr_Answer = dr("Usr_Ans")
                Else
                    objUser.sUsr_Answer = ""
                End If

                If IsDBNull(dr("usr_Qualification")) = False Then
                    objUser.sUsr_Qualification = dr("usr_Qualification")
                Else
                    objUser.sUsr_Qualification = ""
                End If

                If IsDBNull(dr("usr_othersQualification")) = False Then
                    objUser.sUsr_Others = dr("usr_othersQualification")
                Else
                    objUser.sUsr_Others = ""
                End If
            End If
            Return objUser
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateUserProfile(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserId As Integer, ByVal sMobileNo As String, ByVal sSkill As String, ByVal iExperience As Integer,
                                          ByVal sQaulification As String, ByVal sOthers As String, ByVal sQue As String, ByVal sAns As String, ByVal sEmail As String, ByVal sIPAddress As String)
        Dim sSql As String
        Try
            sSql = "Update sad_userDetails set Usr_Status='U',Usr_IPAddress='" & sIPAddress & "',usr_MobileNo = '" & sMobileNo & "',Usr_SkillSet = '" & objclsGRACeGeneral.SafeSQL(sSkill) & "',"
            sSql = sSql & "usr_Experience = " & iExperience & ",USR_Que='" & sQue & "',USR_Ans='" & sAns & "',USR_Email='" & sEmail & "', "
            sSql = sSql & "usr_Qualification = '" & sQaulification & "',usr_othersQualification = '" & objclsGRACeGeneral.SafeSQL(sOthers) & "' where Usr_id = " & iUserId & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class

