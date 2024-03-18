Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Class clsAuditGeneral
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Public Function LoadAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APM_ID,APM_AuditCode From Audit_APM_Details Where APM_AuditCode <>'' and APM_APMTAStatus='Submitted' And APM_CompID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iCustID > 0 Then
                sSql = sSql & " And APM_CustID=" & iCustID & " "
            End If
            If iFunID > 0 Then
                sSql = sSql & " And APM_FunctionID=" & iFunID & " "
            End If
            sSql = sSql & " Order by APM_ID Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionOwnerHODNameFromFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select USr_FullName from sad_userdetails  where usr_compID=" & iACID & "  and usr_ID in (Select ENT_FunownerID from mst_Entity_master where ENT_ID= " & iFunID & " and ENT_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionCodeName(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As String) As String
        Dim sSql As String
        Try
            sSql = "Select ENT_Code + ' - ' + ENT_ENTITYName from MST_Entity_master where ent_compid=" & iACID & " And ENT_ID In(" & iFunID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAPMAuditTeam(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sUsers As String = ""
        Dim sSql As String, sSql1 As String, sAuditors As String = ""
        Dim dt As New DataTable
        Try
            sSql = "Select APM_AuditTeamsID From Audit_APM_Details Where APM_ID=" & iAuditID & " And APM_CompID=" & iACID & ""
            sUsers = objDBL.SQLExecuteScalar(sAC, sSql)
            If IsNothing(sUsers) = False Then
                If sUsers.StartsWith(",") Then
                    sUsers = sUsers.Remove(0, 1)
                End If
                If sUsers.EndsWith(",") Then
                    sUsers = sUsers.Remove(Len(sUsers) - 1, 1)
                End If
                If sUsers <> "" Then
                    sSql1 = "Select Usr_FullName From Sad_UserDetails Where Usr_ID IN (" & sUsers & ") And Usr_CompID=" & iACID & ""
                    dt = objDBL.SQLExecuteDataTable(sAC, sSql1)
                End If
                For i = 0 To dt.Rows.Count - 1
                    If dt.Rows.Count > 0 Then
                        sAuditors = sAuditors & ", " & dt.Rows(i).Item("Usr_FullName")
                    End If
                Next
                If sAuditors.StartsWith(", ") Then
                    sAuditors = sAuditors.Remove(0, 2)
                End If
                If sAuditors.EndsWith(", ") Then
                    sAuditors = sAuditors.Remove(Len(sAuditors) - 2, 1)
                End If
                Return sAuditors
            End If
            Return sAuditors
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetPartnersAuditorsTeam(ByVal sAC As String, ByVal iACID As Integer, ByVal iAPMPKID As Integer, ByVal sType As String) As String
        Dim sUsers As String = "", sSql As String = "", sTeamsID As String
        Dim sArray As Array
        Try
            If sType = "Team" Then
                sSql = "Select APM_AuditTeamsID From Audit_APM_Details Where APM_ID=" & iAPMPKID & " And APM_CompID=" & iACID & ""
            ElseIf sType = "Partner" Then
                sSql = "Select APM_PartnersID From Audit_APM_Details Where APM_ID=" & iAPMPKID & " And APM_CompID=" & iACID & ""
            End If
            sTeamsID = objDBL.SQLExecuteScalar(sAC, sSql)
            If sTeamsID.StartsWith(",") = True Then
                sTeamsID = sTeamsID.Remove(0, 1)
            End If
            If sTeamsID.EndsWith(",") = True Then
                sTeamsID = sTeamsID.Remove(Len(sTeamsID) - 1, 1)
            End If
            If sTeamsID <> "" Then
                sArray = sTeamsID.Split(",")
                For k = 0 To sArray.Length - 1
                    If sArray(k) <> "" Then
                        sUsers = sUsers & "," & objDBL.SQLExecuteScalar(sAC, "Select Usr_FullName from Sad_UserDetails where Usr_ID=" & sArray(k) & "")
                    End If
                Next
            End If
            If sUsers.StartsWith(",") = True Then
                sUsers = sUsers.Remove(0, 1)
            End If
            If sUsers.EndsWith(",") = True Then
                sUsers = sUsers.Remove(Len(sTeamsID) - 1, 1)
            End If
            Return sUsers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionFromAuditID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select APM_FunctionID from Audit_APM_Details where APM_ID=" & iAuditID & " and APM_CompID=" & iACID & " And APM_CustID=" & iCustID & " and APM_APMCRStatus='Submitted' and APM_YearID=" & iYearID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFAuditIDFromunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select APM_ID from Audit_APM_Details where APM_FunctionID=" & iFunctionID & " and APM_CompID=" & iACID & " And APM_CustID=" & iCustID & " and APM_APMCRStatus='Submitted' and APM_YearID=" & iYearID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTypeofTestConclusion(ByVal sAC As String, ByVal iACID As Integer, ByVal sCategory As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select cmm_ID,cmm_Desc from Content_Management_Master Where Cmm_Category='" & sCategory & "' and cmm_Delflag='A' and CMM_CompID=" & iACID & " order by Cmm_ID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionIDFromAuditID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select APM_FunctionID from Audit_APM_Details where APM_ID=" & iAuditID & " and APM_CompID=" & iACID & "  And APM_CustID=" & iCustID & " and APM_APMTAstatus='Submitted' and APM_YearID=" & iYearID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRRNameColorFromRangeRiskMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal dValueID As Double, ByVal sType As String) As String
        Dim sSql As String
        Try
            sSql = "Select"
            If sType = "Desc" Then
                sSql = sSql & " RAm_Remarks "
            ElseIf sType = "Name" Then
                sSql = sSql & " RAM_Name "
            ElseIf sType = "Color" Then
                sSql = sSql & " RAM_Color "
            ElseIf sType = "ID" Then
                sSql = sSql & " RAM_PKID"
            End If
            sSql = sSql & " From Risk_GeneralMaster Where RAM_DelFlag='A' And RAM_StartValue<=round(" & dValueID & ",2) AND RAM_EndValue>=round(" & dValueID & ",2) And RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNameColorFromScoreRiskMaster(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iScore As Integer, ByVal sCategory As String, ByVal sType As String) As String
        Dim dt As New DataTable
        Dim sSql As String
        Try
            sSql = "Select"
            If sType = "Name" Then
                sSql = sSql & " RAM_Name "
            ElseIf sType = "Color" Then
                sSql = sSql & " RAM_Color "
            ElseIf sType = "NameScore" Then
                sSql = sSql & " (RAM_Name + ' - ' + Convert(Varchar(10),RAM_Score)) As RAM_Name "
            End If
            sSql = sSql & " From Risk_GeneralMaster Where RAM_DelFlag='A' And RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_Score=" & iScore & " And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetImpactLikelihoodOESDESScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sCategory As String, ByVal iPKID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RAM_Score from Risk_GeneralMaster Where RAM_PKID=" & iPKID & " And RAM_Category='" & sCategory & "' And RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetControlScoreFromMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iDES As Integer, ByVal iOES As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GG_ControlScore from GRACe_GrossControlScore where GG_DE=" & iDES & " And GG_OE=" & iOES & " And GG_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskControlScoreFromMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iRisk As Integer, ByVal iControl As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GG_RiskControlScore from GRACe_RiskControlMatrix where GG_Risk=" & iRisk & " And GG_Controls=" & iControl & " And GG_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskTypeIDFromRiskID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRiskID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select MRL_RiskTypeID from MST_RISK_Library where MRL_PKID=" & iRiskID & " and MRL_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskScoreFromMatrix(ByVal sAC As String, ByVal iACID As Integer, ByVal iImpact As Integer, ByVal iLiklihood As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select GG_RiskScore from GRACe_GrossRiskScore where GG_Impact=" & iImpact & " And GG_Likelihood=" & iLiklihood & " And GG_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFAAPMFunHODName(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer) As String
        Dim sSql As String, sStr As String = ""
        Try
            sSql = "Select Usr_FullName From Sad_UserDetails Where Usr_CompID=" & iACID & " And Usr_ID=(Select APM_FunctionHODID From Audit_APM_Details Where APM_AuditCodeID=" & iAuditID & " And APM_CompID=" & iACID & " And APM_YearID=" & iYearID & ")"
            sStr = objDBL.SQLExecuteScalar(sAC, sSql)
            sStr = objclsGRACeGeneral.ReplaceSafeSQL(sStr)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFAAPMAuditTeam(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String, sAuditors As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT AAt_Name FROM Audit_APM_Team WHERE aat_AuditCodeID= " & iAuditID & " and AAT_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sAuditors = sAuditors & ", " & objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("AAt_Name"))
                Next
                If sAuditors.StartsWith(",") Then
                    sAuditors = sAuditors.Remove(0, 1)
                End If
                If sAuditors.EndsWith(",") Then
                    sAuditors = sAuditors.Remove(Len(sAuditors) - 1, 1)
                End If
            Else
                sAuditors = ""
            End If
            Return sAuditors
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
