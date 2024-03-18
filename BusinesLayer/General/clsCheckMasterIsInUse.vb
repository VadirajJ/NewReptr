Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsCheckMasterIsInUse
    Private objDBL As New DatabaseLayer.DBHelper
    Public Function CheckOrganizationStructureIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iOrgnID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            'Organization Structure
            sSql = "Select Usr_ID From Sad_userDetails Where Usr_OrgnID=" & iOrgnID & " and Usr_CompID=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckEmployeeNameIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer) As Boolean
        Dim sSql As String = ""
        Dim dt As New DataTable
        Try
            'Function Owner
            sSql = "Select ENT_ID from MST_Entity_Master Where Ent_FunOwnerID=" & iUserID & " And ENT_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'Function Manager
            sSql = "Select ENT_ID from MST_Entity_Master Where Ent_FunManagerID=" & iUserID & " And ENT_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If

            'Function SPOC
            sSql = "Select ENT_ID from MST_Entity_Master Where Ent_FunSPOCID=" & iUserID & " And ENT_Compid=" & iACID & ""
            If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckGeneralMasters(ByVal sAc As String, ByVal iAcID As Integer, ByVal iMasterID As Integer, ByVal sTableName As String, ByVal sColumnName As String, ByVal sComapany As String) As Boolean
        'Dim sSql As String
        'Dim dt As New DataTable
        Try
            'General Masters
            'sSql = "Select * from " & sTableName & " where " & sColumnName & " = " & iMasterID & " and " & sComapany & " =  " & iAcID & ""
            'dt = objDBL.SQLExecuteDataTable(sAc, sSql)
            'If dt.Rows.Count > 0 Then
            '    Return True
            'Else
            Return False
            'End If
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckProcessIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iPMID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            ''Functional FieldWork DashBoard 
            'sSql = "Select AFW_ID from Audit_FieldWork Where AFW_ProcessID=" & iPMID & " and AFW_CompID=" & iACID & ""
            'If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '    Return True
            'End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubProcessIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iSPMID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            ''Functional FieldWork DashBoard 
            'sSql = "Select AFW_ID from Audit_FieldWork Where AFW_SubProcessID=" & iSPMID & " and AFW_CompID=" & iACID & ""
            'If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '    Return True
            'End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckRiskGeneralMasterIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iRGMID As Integer, ByVal sType As String, ByVal iYearID As Integer) As Boolean
        Dim sSql As String = ""
        Dim iScore As Integer = 0, iPKID As Integer = 0
        Try
            If sType = "RT" Then 'Risk Type
                'Risk Master
                sSql = "Select MRL_PKID from MST_RISK_Library Where MRL_RiskTypeID=" & iRGMID & " and MRL_CompID=" & iACID & ""
                If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    Return True
                End If

                'Functional RiskAssessment Checklist
                sSql = "Select ARAD_PKID from Audit_ARA_Details  Where ARAD_RiskTypeID=" & iRGMID & " and ARAD_CompID=" & iACID & ""
                iPKID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select ARA_PKID from Audit_ARA where ARA_PKID in(Select ARAD_ARAPKID from Audit_ARA_Details Where ARAD_PKID=" & iPKID & " ) And ARA_FinancialYear=" & iYearID & ""
                If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    Return True
                End If
            End If

            'Risk Impact,Risk Likelihood,Design Effectiveness Scores and Operational Effectiveness scores
            If sType = "RI" Or sType = "RL" Or sType = "DES" Or sType = "OES" Then
                'Functional FieldWork DashBoard 
                sSql = "Select ARAD_ARAPKID from Audit_ARA_Details Where ARAD_ImpactID=" & iRGMID & " and ARAD_CompID=" & iACID & ""
                If sType = "RI" Then
                    sSql = sSql & " And ARAD_ImpactID=" & iRGMID & ""
                ElseIf sType = "RL" Then
                    sSql = sSql & " And ARAD_LikelihoodID=" & iRGMID & ""
                ElseIf sType = "DES" Then
                    sSql = sSql & " And ARAD_DES=" & iRGMID & ""
                ElseIf sType = "OES" Then
                    sSql = sSql & " And ARAD_OES=" & iRGMID & ""
                End If
                iPKID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select ARA_PKID from Audit_ARA where ARA_PKID in(Select ARAD_ARAPKID from Audit_ARA_Details Where ARAD_PKID=" & iPKID & ") And ARA_FinancialYear=" & iYearID & ""
                If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    Return True
                End If
            End If

            'Gross Risk Score,Gross Control Score and Residual Risk Score
            If sType = "GRS" Or sType = "GCS" Or sType = "RRS" Then
                '3 Year Plan
                'sSql = "Select ARA_PKID from Audit_ARA Where ARA_CompID=" & iACID & " And ARA_FinancialYear=" & iYearID & ""
                'If sType = "RRS" Then
                '    sSql = sSql & " And ARA_NetScore=" & iScore & ""
                'End If
                'If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                '    Return True
                'End If

                'F Risk Assessment Checklist
                sSql = "Select ARAD_ARAPKID from Audit_ARA_Details Where  ARAD_CompID=" & iACID & ""
                If sType = "GRS" Then
                    sSql = sSql & " And ARAD_RiskRating=" & iScore & ""
                ElseIf sType = "GCS" Then
                    sSql = sSql & " And ARAD_ControlRating=" & iScore & ""
                ElseIf sType = "RRS" Then
                    sSql = sSql & " And ARAD_ResidualRiskRating=" & iScore & ""
                End If
                iPKID = objDBL.SQLExecuteScalarInt(sAC, sSql)
                sSql = "Select ARA_PKID from Audit_ARA where ARA_PKID in(Select ARAD_ARAPKID from Audit_ARA_Details Where ARAD_ARAPKID=" & iPKID & " ) And ARA_FinancialYear=" & iYearID & ""
                If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function CheckControlMasterIsInUse(ByVal sAC As String, ByVal iACID As Integer, ByVal iSPMID As Integer) As Boolean
        Dim sSql As String = ""
        Try
            'F FieldWork DashBoard 
            'sSql = "Select AFW_ID from Audit_FieldWork Where AFW_ControlID=" & iSPMID & " and AFW_CompID=" & iACID & ""
            'If objDBL.SQLCheckForRecord(sAC, sSql) = True Then
            '    Return True
            'End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
