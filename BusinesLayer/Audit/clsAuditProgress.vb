Public Class clsAuditProgress
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetNoOfIssues(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_YearID=" & iYearID & ""
            sSql = sSql & " And AIT_FunctionID=" & iFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerStarted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " and AIT_Status<>'Submitted'"
            sSql = sSql & " And AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerCompleted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " and AIT_Status='Submitted'"
            sSql = sSql & " And AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalSeverity(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_YearID=" & iYearID & ""
            sSql = sSql & " And AIT_FunctionID=" & iFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSeverityChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtTab As New DataTable, dtRating As New DataTable, dtMaster As New DataTable
        Dim i As Integer
        Try
            dt.Columns.Add("Count")
            dt.Columns.Add("Name")
            dt.Columns.Add("Color")
            sSql = "Select AIT_SeverityID from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_YearID=" & iYearID & ""
            sSql = sSql & " And AIT_FunctionID=" & iFunID & ""
            dtMaster = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            dtRating = GetSeverityName(sAC, iACID, iYearID)
            For i = 0 To dtRating.Rows.Count - 1
                dRow = dt.NewRow
                Dim DVdtMaster As New DataView(dtMaster)
                DVdtMaster.RowFilter = "AIT_SeverityID =" & dtRating.Rows(i)("RAM_PKID") & ""
                dtTab = DVdtMaster.ToTable
                dRow("Count") = dtTab.Rows.Count
                dRow("Name") = dtRating.Rows(i)("RAM_Name")
                dRow("Color") = dtRating.Rows(i)("RAM_Color")
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetSeverityName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_Name,RAM_PKID,RAM_Color From Risk_GeneralMaster Where RAM_Category='RRS' And RAM_CompID=" & iACID & " And RAM_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
