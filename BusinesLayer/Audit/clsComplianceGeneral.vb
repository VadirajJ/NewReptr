Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web
Public Class clsComplianceGeneral
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral

    Public Function GetCRCPMAuditTeam(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String, sAuditors As String = ""
        Dim dt As New DataTable
        Try
            sSql = "SELECT CAT_Name FROM Compliance_CPM_Team WHERE CAT_ComplianceCodeID= " & iAuditID & " and CAT_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    sAuditors = sAuditors & ", " & objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i).Item("CAT_Name"))
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
    Public Function LoadCompliancePlannedSubFunFromPlan(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_NAME from MST_SUBENTITY_MASTER where SEM_COMPID=" & iACID & " and SEM_ID in(Select CP_SubFunctionID from Compliance_Plan where CP_CompID=" & iACID & " and CP_ScheduleStatus='Submitted' and CP_YearID=" & iYearID & " and CP_IsCurrentYear=1 and CP_functionID=" & iFuncID & " And CP_ScheduledMonthID > 0) order by SEM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadComplianceCodeFromPlanWithFunSFID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFUNID As Integer, ByVal iYearID As Integer, ByVal iAgencyUserID As Integer, ByVal iFUNUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CP_ID,CP_ComplianceCode From Compliance_Plan Where CP_IsCurrentYear=1 and CP_CompID=" & iACID & " and CP_ScheduleStatus='Submitted' and CP_YearID=" & iYearID & " And CP_ScheduledMonthID > 0 "
            If iFUNID > 0 Then
                sSql = sSql & " and CP_functionID=" & iFUNID & ""
            End If
            If iAgencyUserID > 0 Then
                sSql = sSql & " and CP_ComplianceAgencyID=" & iAgencyUserID & ""
            End If
            If iFUNUserID > 0 Then
                sSql = sSql & " And CP_functionID in (Select ENT_ID From mst_Entity_master Where (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ") and ENT_CompID=" & iACID & ")"
            End If
            sSql = sSql & " Order by CP_ID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompliancePlannedFunctionFromPlan(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAgencyUserID As Integer, ByVal iFUNUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And ENT_ID in"
            sSql = sSql & " (Select CP_functionID from Compliance_Plan where CP_ScheduleStatus='Submitted' And CP_YearID = " & iYearID & " And CP_ScheduledMonthID>0 "
            If iAgencyUserID > 0 Then
                sSql = sSql & " And CP_ComplianceAgencyID=" & iAgencyUserID & ""
            End If
            sSql = sSql & " And CP_CompID = " & iACID & ")"
            If iFUNUserID > 0 Then
                sSql = sSql & " And (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ")"
            End If
            sSql = sSql & " order by ENT_ENTITYName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunctionFromComplianceID(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CP_functionID from Compliance_Plan where CP_ID=" & iComplianceID & " and CP_CompID=" & iACID & " and CP_ScheduleStatus='Submitted' and CP_YearID=" & iYearID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunctionIDFromWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CFW_SubFunctionID from Compliance_FieldWork where CFW_ComplianceCodeID=" & iComplianceID & " and CFW_CompID=" & iACID & " and (CFW_status='Submitted' OR CFW_status='Saved' OR CFW_status='Updated') and CFW_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSubFunctionIDFromComplianceID(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CP_SubFunctionID from Compliance_Plan where CP_ID=" & iComplianceID & " and CP_CompID=" & iACID & " and CP_ScheduleStatus='Submitted' and CP_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceAgencyNameFromComplianceID(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select Com_name from sad_companyDetails where Com_companyID=" & iACID & " and Com_ID in (Select CP_ComplianceAgencyID from Compliance_Plan where CP_ID= " & iComplianceID & " and CP_CompID=" & iACID & ")"
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceAgencyIDFromComplianceID(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CP_ComplianceAgencyID from Compliance_Plan where CP_ID= " & iComplianceID & " and CP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceCodeIDFromFunSFID(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CP_ID from Compliance_Plan where CP_functionID=" & iFunID & " and CP_CompID=" & iACID & " and CP_ScheduleStatus='Submitted' and CP_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompliancePlannedFunctionFromCPM(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, iAgencyUserID As Integer, iFUNUserID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And ENT_ID in(Select CPM_functionID from Compliance_CPM_Details where CPM_Status='Submitted' and CPM_YearID=" & iYearID & " "
            If iAgencyUserID > 0 Then
                sSql = sSql & " And CPM_ComplianceCodeID in (Select CP_ID From Compliance_Plan Where CP_CompID=" & iACID & " And CP_YearID=" & iYearID & " and CP_ScheduleStatus='Submitted' and CP_ComplianceAgencyID=" & iAgencyUserID & ")"
            End If
            sSql = sSql & " And CPM_CompID = " & iACID & ")"
            If iFUNUserID > 0 Then
                sSql = sSql & " And (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ")"
            End If
            sSql = sSql & " order by ENT_ENTITYName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCompliancePlannedSubFunFromCPM(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_NAME from MST_SUBENTITY_MASTER where SEM_COMPID=" & iACID & " and SEM_ID in(Select CPM_SubFunctionID from Compliance_CPM_Details where CPM_CompID=" & iACID & " and CPM_Status='Submitted' and CPM_YearID=" & iYearID & " and CPM_functionID=" & iFuncID & ") order by SEM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceReportTitleFromComplianceID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgID As Integer) As String
        Dim sSql As String, sStr As String = ""
        Try
            sSql = "Select Case When CP_ReportTitle Is Null Then '' Else CP_ReportTitle End As CP_ReportTitle from Compliance_Plan where CP_compID=" & iACID & " and CP_ID =" & iAsgID & ""
            sStr = objDBL.SQLExecuteScalar(sAC, sSql)
            sStr = objclsGRACeGeneral.ReplaceSafeSQL(sStr)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iAsgID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CP_ComplianceCode from Compliance_Plan where CP_compID=" & iACID & " and CP_ID =" & iAsgID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetComplianceCodeReportTitle(ByVal sAC As String, ByVal iACID As Integer, ByVal i3YPKID As Integer, ByVal sType As String) As String
        Dim sSql As String = "", sStr As String = ""
        Try
            If sType = "ComplianceCode" Then
                sSql = " Select Case When CP_ComplianceCode Is Null Then '' Else CP_ComplianceCode End As CP_ComplianceCode "
            ElseIf sType = "ReportTitle" Then
                sSql = "Select Case When CP_ReportTitle Is Null Then '' Else CP_ReportTitle End As CP_ReportTitle "
            End If
            sSql = sSql & " From Compliance_Plan Where CP_ID=" & i3YPKID & " And CP_CompID=" & iACID & ""
            sStr = objDBL.SQLExecuteScalar(sAC, sSql)
            sStr = objclsGRACeGeneral.ReplaceSafeSQL(sStr)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetScheduleMonthFromComplianceID(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CP_ScheduledMonthID From Compliance_Plan Where CP_ID=" & iComplianceID & " and CP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
