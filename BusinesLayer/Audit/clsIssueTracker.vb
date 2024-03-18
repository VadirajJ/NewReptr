Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web

Public Structure strAudit_IssueTracker_details
    Private AIT_PKID As Integer
    Private AIT_YearID As Integer
    Private AIT_CustID As Integer
    Private AIT_AuditCode As Integer
    Private AIT_WorkPaperID As Integer
    Private AIT_FunctionID As Integer
    Private AIT_SubFunctionID As Integer
    Private AIT_ProcessID As Integer
    Private AIT_SubProcessID As Integer
    Private AIT_RiskID As Integer
    Private AIT_ControlID As Integer
    Private AIT_CheckID As Integer
    Private AIT_IssueJobNo As String
    Private AIT_SeverityID As Integer
    Private AIT_RiskCategoryID As Integer
    Private AIT_IssueNameID As Integer
    Private AIT_IssueName As String
    Private AIT_Criteria As String
    Private AIT_Condition As String
    Private AIT_Details As String
    Private AIT_Impact As String
    Private AIT_RootCause As String
    Private AIT_SuggestedRemedies As String
    Private AIT_AttachID As Integer
    Private AIT_CreatedBy As Integer
    Private AIT_UpdatedBy As Integer
    Private AIT_CompID As Integer
    Private AIT_IPAddress As String
    Private AIT_OpenCloseStatus As Integer
    Private AIT_AuditorRemarks As String
    Private AIT_ReviewerRemarks As String

    Public Property iAIT_OpenCloseStatus() As Integer
        Get
            Return (AIT_OpenCloseStatus)
        End Get
        Set(ByVal Value As Integer)
            AIT_OpenCloseStatus = Value
        End Set
    End Property
    Public Property sAIT_AuditorRemarks() As String
        Get
            Return (AIT_AuditorRemarks)
        End Get
        Set(ByVal Value As String)
            AIT_AuditorRemarks = Value
        End Set
    End Property
    Public Property sAIT_ReviewerRemarks() As String
        Get
            Return (AIT_ReviewerRemarks)
        End Get
        Set(ByVal Value As String)
            AIT_ReviewerRemarks = Value
        End Set
    End Property

    Public Property iAIT_PKID() As Integer
    Get
            Return (AIT_PKID)
        End Get
        Set(ByVal Value As Integer)
            AIT_PKID = Value
        End Set
    End Property
    Public Property iAIT_AttachID() As Integer
        Get
            Return (AIT_AttachID)
        End Get
        Set(ByVal Value As Integer)
            AIT_AttachID = Value
        End Set
    End Property
    Public Property iAIT_YearID() As Integer
        Get
            Return (AIT_YearID)
        End Get
        Set(ByVal Value As Integer)
            AIT_YearID = Value
        End Set
    End Property
    Public Property iAIT_AuditCode() As Integer
        Get
            Return (AIT_AuditCode)
        End Get
        Set(ByVal Value As Integer)
            AIT_AuditCode = Value
        End Set
    End Property
    Public Property iAIT_WorkPaperID() As Integer
        Get
            Return (AIT_WorkPaperID)
        End Get
        Set(ByVal Value As Integer)
            AIT_WorkPaperID = Value
        End Set
    End Property
    Public Property iAIT_CustID() As Integer
        Get
            Return (AIT_CustID)
        End Get
        Set(ByVal Value As Integer)
            AIT_CustID = Value
        End Set
    End Property
    Public Property sAIT_IssueJobNo() As String
        Get
            Return (AIT_IssueJobNo)
        End Get
        Set(ByVal Value As String)
            AIT_IssueJobNo = Value
        End Set
    End Property
    Public Property iAIT_FunctionID() As Integer
        Get
            Return (AIT_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            AIT_FunctionID = Value
        End Set
    End Property
    Public Property iAIT_SubFunctionID() As Integer
        Get
            Return (AIT_SubFunctionID)
        End Get
        Set(ByVal Value As Integer)
            AIT_SubFunctionID = Value
        End Set
    End Property
    Public Property iAIT_ProcessID() As Integer
        Get
            Return (AIT_ProcessID)
        End Get
        Set(ByVal Value As Integer)
            AIT_ProcessID = Value
        End Set
    End Property
    Public Property iAIT_SubProcessID() As Integer
        Get
            Return (AIT_SubProcessID)
        End Get
        Set(ByVal Value As Integer)
            AIT_SubProcessID = Value
        End Set
    End Property
    Public Property iAIT_RiskID() As Integer
        Get
            Return (AIT_RiskID)
        End Get
        Set(ByVal Value As Integer)
            AIT_RiskID = Value
        End Set
    End Property
    Public Property iAIT_ControlID() As Integer
        Get
            Return (AIT_ControlID)
        End Get
        Set(ByVal Value As Integer)
            AIT_ControlID = Value
        End Set
    End Property
    Public Property iAIT_CheckID() As Integer
        Get
            Return (AIT_CheckID)
        End Get
        Set(ByVal Value As Integer)
            AIT_CheckID = Value
        End Set
    End Property
    Public Property iAIT_SeverityID() As Integer
        Get
            Return (AIT_SeverityID)
        End Get
        Set(ByVal Value As Integer)
            AIT_SeverityID = Value
        End Set
    End Property
    Public Property iAIT_RiskCategoryID() As Integer
        Get
            Return (AIT_RiskCategoryID)
        End Get
        Set(ByVal Value As Integer)
            AIT_RiskCategoryID = Value
        End Set
    End Property
    Public Property iAIT_IssueNameID() As Integer
        Get
            Return (AIT_IssueNameID)
        End Get
        Set(ByVal Value As Integer)
            AIT_IssueNameID = Value
        End Set
    End Property
    Public Property sAIT_IssueName() As String
        Get
            Return (AIT_IssueName)
        End Get
        Set(ByVal Value As String)
            AIT_IssueName = Value
        End Set
    End Property
    Public Property sAIT_Criteria() As String
        Get
            Return (AIT_Criteria)
        End Get
        Set(ByVal Value As String)
            AIT_Criteria = Value
        End Set
    End Property
    Public Property sAIT_Condition() As String
        Get
            Return (AIT_Condition)
        End Get
        Set(ByVal Value As String)
            AIT_Condition = Value
        End Set
    End Property
    Public Property sAIT_Details() As String
        Get
            Return (AIT_Details)
        End Get
        Set(ByVal Value As String)
            AIT_Details = Value
        End Set
    End Property
    Public Property sAIT_Impact() As String
        Get
            Return (AIT_Impact)
        End Get
        Set(ByVal Value As String)
            AIT_Impact = Value
        End Set
    End Property
    Public Property sAIT_RootCause() As String
        Get
            Return (AIT_RootCause)
        End Get
        Set(ByVal Value As String)
            AIT_RootCause = Value
        End Set
    End Property
    Public Property sAIT_SuggestedRemedies() As String
        Get
            Return (AIT_SuggestedRemedies)
        End Get
        Set(ByVal Value As String)
            AIT_SuggestedRemedies = Value
        End Set
    End Property

    Public Property iAIT_CreatedBy() As Integer
        Get
            Return (AIT_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            AIT_CreatedBy = Value
        End Set
    End Property

    Public Property iAIT_UpdatedBy() As Integer
        Get
            Return (AIT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            AIT_UpdatedBy = Value
        End Set
    End Property
    Public Property iAIT_CompID() As Integer
        Get
            Return (AIT_CompID)
        End Get
        Set(ByVal Value As Integer)
            AIT_CompID = Value
        End Set
    End Property
    Public Property sAIT_IPAddress() As String
        Get
            Return (AIT_IPAddress)
        End Get
        Set(ByVal Value As String)
            AIT_IPAddress = Value
        End Set
    End Property
End Structure
Public Class clsIssueTracker
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As New clsGRACeGeneral
    Dim objclsGeneralFunctions As New clsGeneralFunctions
    Public Function GetNoOfWorkpaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNoOfIssues(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_YearID=" & iYearID & ""
            sSql = sSql & " And AIT_FunctionID=" & iFunID & " And AIT_SubFunctionID=" & iSubFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerStarted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " and AIT_Status<>'Submitted'"
            sSql = sSql & " And AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunID & " And AIT_SubFunctionID=" & iSubFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerCompleted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " and AIT_Status='Submitted'"
            sSql = sSql & " And AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunID & " And AIT_SubFunctionID=" & iSubFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerOpenChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_OpenCloseStatus=1"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerClosedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AIT_PKID) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_OpenCloseStatus=2"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperNO(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer,
                                    ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select AWP_PKID,AWP_WorkPaperNo from Audit_WorkPaper Where AWP_YearID=" & iYearID & " And AWP_FunctionID=" & iFunID & " And"
            sSql = sSql & " AWP_AuditCode=" & iAuditID & " And AWP_CustID=" & iCustID & " And AWP_CompID=" & iACID & " and (AWP_Status='Saved' Or AWP_Status='Updated'"
            sSql = sSql & " Or AWP_Status='Submitted') And AWP_SubFunctionID=" & iSubFunID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueTrackerFromWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer, ByVal iWorkpaperID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AWP_PKID,ENT_ID,Ent_EntityName,SEM_ID,SEM_Name,PM_ID,PM_Name,SPM_ID,SPM_Name,MMM_RISKID,MMM_Risk,MMM_CONTROLID,MMM_Control, "
            sSql = sSql & " MMM_ChecksID, MMM_CHECKS,Cust_Name,AWP_WorkPaperNo,APM_AuditCode From Audit_WorkPaper"
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=AWP_FunctionID And ENT_CompID=" & iACID & " "
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=AWP_SubFunctionID And SEM_CompID=" & iACID & " "
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=AWP_ProcessID And PM_CompID=" & iACID & "  "
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=AWP_SubProcessID And SPM_CompID=" & iACID & " "
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_RISKID=AWP_RiskID And MMM_CONTROLID=AWP_ControlID And MMM_ChecksID=AWP_ChecksID"
            sSql = sSql & " Left Join Audit_APM_Details On  APM_ID=AWP_AuditCode And APM_CompID=" & iACID & " "
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=AWP_CustID And Cust_CompID=" & iACID & " "
            sSql = sSql & " Where AWP_CompID=" & iACID & "  and AWP_YearID=" & iYearID & " And AWP_PKID=" & iWorkpaperID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AWP_AuditCode = " & iAuditID & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " and AWP_CustID = " & iCustID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and AWP_FunctionID = " & iFunction & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueNO(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select AIT_PKID,AIT_IssueJobNo  from Audit_IssueTracker_Details Where AIT_CompID  =" & iACID & " And AIT_AuditCode =" & iAuditID & "  Order by AIT_PKID "
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditCodeFromWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select APM_ID,APM_AuditCode From Audit_APM_Details Where APM_CompID=" & iACID & " and APM_YearID=" & iYearID & " and APM_ID In (Select AWP_AuditCode From Audit_WorkPaper Where AWP_YearID=" & iYearID & " "
            If iFunID > 0 Then
                sSql = sSql & " And AWP_FunctionID=" & iFunID & ""
            End If
            If iCustID > 0 Then
                sSql = sSql & " And AWP_CustID=" & iCustID & ""
            End If
            sSql = sSql & " And AWP_CompID=" & iACID & ")"
            sSql = sSql & " Order by APM_ID Desc"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select APM_AuditCode From Audit_APM_Details Where APM_CompID=" & iACID & " and APM_YearID=" & iYearID & " And APM_ID=" & iAuditID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueTrackerdashBoard(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer,
                                              ByVal iCustID As Integer, ByVal iSubFunID As Integer, ByVal iWorkPaperID As Integer) As DataTable
        Dim sSql As String, sWorkPaperNo As String = "", sMaxID As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iIssuePKID As Integer
        Try
            dtTab.Columns.Add("IssueID")
            dtTab.Columns.Add("IssueNo")
            dtTab.Columns.Add("IssueName")
            dtTab.Columns.Add("Severity")
            dtTab.Columns.Add("RiskCategory")
            dtTab.Columns.Add("Status")
            dtTab.Columns.Add("WorkPaperID")

            sSql = "Select AIT_PKID,AIT_Status,AIT_IssueJobNo,AIT_IssueName,AIT_WorkPaperID,AIT_SeverityID,AIT_RiskCategoryID,b.RAM_Name as Severity,c.RAM_Name as"
            sSql = sSql & " RiskCategory,d.AWP_WorkPaperNo From Audit_IssueTracker_Details"
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=AIT_SeverityID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=AIT_RiskCategoryID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_WorkPaper d on d.AWP_AuditCode=AIT_AuditCode  And AIT_FunctionID=AWP_FunctionID"
            sSql = sSql & " And AIT_SubFunctionID=AWP_SubFunctionID And AIT_ProcessID=AWP_ProcessID And "
            sSql = sSql & " AIT_SubProcessID=AWP_SubProcessID And AIT_RiskID=AWP_RiskID And AIT_ControlID=AWP_ControlID  And d.AWP_CompID=" & iACID & ""
            sSql = sSql & " Where AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunctionID & " And AIT_AuditCode=" & iAuditID & " And AIT_CustID=" & iCustID & ""
            sSql = sSql & " And AIT_CompID=" & iACID & " And AIT_SubFunctionID=" & iSubFunID & ""
            If iWorkPaperID > 0 Then
                sSql = sSql & " And AWP_PKID=" & iWorkPaperID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("AIT_PKID")) = False Then
                    dr("IssueID") = dt.Rows(i)("AIT_PKID")
                End If
                If IsDBNull(dt.Rows(i)("AIT_WorkPaperID")) = False Then
                    dr("WorkPaperID") = dt.Rows(i)("AIT_WorkPaperID")
                End If
                If IsDBNull(dt.Rows(i)("AIT_IssueJobNo")) = False Then
                    iIssuePKID = dt.Rows(i)("AIT_PKID")
                    sWorkPaperNo = dt.Rows(i)("AWP_WorkPaperNo")
                    If iIssuePKID = 0 Then
                        sMaxID = "001"
                    ElseIf iIssuePKID > 0 And iIssuePKID < 10 Then
                        sMaxID = "00" & iIssuePKID
                    ElseIf iIssuePKID >= 10 And iIssuePKID < 100 Then
                        sMaxID = "0" & iIssuePKID
                    Else
                        sMaxID = iIssuePKID
                    End If
                    dr("IssueNo") = sWorkPaperNo & "/IT-" & sMaxID
                End If
                If IsDBNull(dt.Rows(i)("AIT_IssueName")) = False Then
                    dr("IssueName") = dt.Rows(i)("AIT_IssueName")
                End If
                If IsDBNull(dt.Rows(i)("Severity")) = False Then
                    dr("Severity") = dt.Rows(i)("Severity")
                End If
                If IsDBNull(dt.Rows(i)("RiskCategory")) = False Then
                    dr("RiskCategory") = dt.Rows(i)("RiskCategory")
                End If
                If IsDBNull(dt.Rows(i)("AIT_Status")) = False Then
                    dr("Status") = dt.Rows(i)("AIT_Status")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperAPMDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = " Select Ent_EntityName,Cust_Name,ENT_ID,SEM_ID,PM_ID,SPM_ID,SPM_Name,SEM_Name,MMM_RISKID,MMM_Risk,MMM_CONTROLID, MMM_Control,APM_AuditCode,APMCM_SubFunctionID,"
            sSql = sSql & " APMCM_ProcessID,APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APM_Objectives,PM_Name,MMM_ChecksID,MMM_CHECKS From  from Audit_WorkPaper"
            sSql = sSql & " Left Join Audit_APM_Details On APM_ID=AWP_AuditCode And APM_CompID=" & iACID & ""
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=AWP_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_APM_ChecksMatrix On APMCM_FunctionID=AWP_FunctionID And APMCM_CompID=" & iACID & ""
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=AWP_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_RISKID=APMCM_RiskID and MMM_CONTROLID=APMCM_ControlID and MMM_ChecksID=APMCM_ChecksID"
            sSql = sSql & " where AWP_Status='Submitted' OR AWP_Status='Submitted Auditor' And AWP_CompID=" & iACID & " and AWP_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and AWP_AuditCode =" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and AWP_FunctionID =" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and AWP_CustID =" & iCustID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iITPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Select AIT_PKID,AIT_YearID,AIT_CustID,AIT_AuditCode,AIT_WorkPaperID,AIT_FunctionID,AIT_SubFunctionID,AIT_ProcessID,AIT_SubProcessID,AIT_PGEDetailId,"
            sSql = sSql & "AIT_RiskID,AIT_ControlID,AIT_IssueNameID,AIT_CheckID,AIT_IssueJobNo,AIT_SeverityID,AIT_RiskCategoryID,AIT_IssueName,AIT_Criteria,AIT_Condition,"
            sSql = sSql & "AIT_Details,AIT_Impact,AIT_RootCause,AIT_SuggestedRemedies,AIT_AttachID,AIT_Status,AIT_OpenCloseStatus,AIT_AuditorRemarks,AIT_ReviewerRemarks From Audit_IssueTracker_Details"
            sSql = sSql & " Where  AIT_PKID=" & iITPKID & " And AIT_YearID=" & iYearID & " And AIT_CompID=" & iACID & ""
            If iFunctionID > 0 Then
                sSql = sSql & " And AIT_FunctionID =" & iFunctionID & ""
            End If
            If iAuditID > 0 Then
                sSql = sSql & " And AIT_AuditCode=" & iAuditID & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " And AIT_CustID=" & iCustID & " "
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveIssueTracker(ByVal sAC As String, ByVal objAITDetails As strAudit_IssueTracker_details)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AuditCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_AuditCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_WorkPaperID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_WorkPaperID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_SubFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_SubFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_ProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_SubProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_SubProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_RiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_RiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ControlID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_ControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CheckID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_CheckID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IssueJobNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_IssueJobNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_SeverityID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_SeverityID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_RiskCategoryID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_RiskCategoryID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IssueNameID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_IssueNameID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IssueName", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_IssueName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Criteria", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_Criteria
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Condition", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_Condition
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Details", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_Details
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_Impact ", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_Impact
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_RootCause", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_RootCause
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_SuggestedRemedies", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_SuggestedRemedies
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_OpenCloseStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_OpenCloseStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AuditorRemarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_AuditorRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_ReviewerRemarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_ReviewerRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objAITDetails.iAIT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objAITDetails.sAIT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_IssueTracker_details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmittedIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iIssueTrackerID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_IssueTracker_Details Set AIT_Status='Auditor Submitted',AIT_ReviewedBy=" & iUserID & ", AIT_ReviewedOn=GetDate()"
            sSql = sSql & " where AIT_YearID=" & iYearID & "And AIT_PKID =" & iIssueTrackerID & " And AIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetIssueTrackerStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iIssuePKID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Audtior" Then
                sSql = "Select AIT_AuditorRemarks from Audit_IssueTracker_Details where AIT_PKID=" & iIssuePKID & " And AIT_CompID=" & iACID & ""
            ElseIf sType = "Reviewer" Then
                sSql = "Select AIT_ReviewerRemarks from Audit_IssueTracker_Details where AIT_PKID=" & iIssuePKID & " And AIT_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerDetailsHistoryReviewerID(ByVal sAC As String, ByVal iACID As Integer, ByVal iIssuePKID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Max(AITH_PKID) From Audit_IssueTracker_History Where AITH_IssuePKID=" & iIssuePKID & " And AITH_AuditID=" & iAuditID & " And "
            sSql = sSql & " AITH_CustID=" & iCustID & " And AITH_FunctionID=" & iFunction & " And AITH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub AuditorSaveIssueTrackerDetailsHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iIssueTrackerID As Integer,
                                                     ByVal sRemarks As String, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Dim iMaxID As Integer
        Dim objclsGeneralFunctions As New clsGeneralFunctions
        Try
            iMaxID = objclsGeneralFunctions.GetMaxID(sAC, iACID, "Audit_IssueTracker_History", "AITH_PKID", "AITH_CompID")
            sSql = "Insert Into Audit_IssueTracker_History (AITH_PKID,AITH_IssuePKID,AITH_AuditID,AITH_CustID,AITH_FunctionID,AITH_AuditorRemarks,"
            sSql = sSql & " AITH_ARCrBy,AITH_ARCrOn,AITH_IPAddress,AITH_CompID)"
            sSql = sSql & "Values(" & iMaxID & "," & iIssueTrackerID & "," & iAuditID & "," & iCustID & "," & iFunction & ",'" & sRemarks & "',"
            sSql = sSql & "" & iUserID & ",GetDate(),'" & sIPAddress & "'," & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ReviewerSaveWorkPaperDetailsHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer, ByVal sRemarks As String,
                                                    ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sIPAddress As String, ByVal iAITHIDMaxID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_IssueTracker_History Set AITH_ReviewerRemarks='" & sRemarks & "',AITH_RRCrBy=" & iUserID & ",AITH_RRCrOn=GetDate(),AITH_IPAddress='" & sIPAddress & "'"
            sSql = sSql & "Where AITH_PKID=" & iAITHIDMaxID & " And AITH_IssuePKID=" & iWorkPaperID & " And AITH_AuditID=" & iAuditID & " And "
            sSql = sSql & " AITH_CustID=" & iCustID & " And AITH_FunctionID=" & iFunction & " And AITH_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ReviewerSubmittedIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer,
                                               ByVal iIssueTrackerID As Integer, ByVal iOpenCloseStatus As Integer, ByVal sRemarks As String)
        Dim sSql As String
        Try
            sSql = "Update Audit_IssueTracker_Details Set AIT_ReviewedBy=" & iUserID & ", AIT_ReviewedOn=GetDate(), AIT_OpenCloseStatus =" & iOpenCloseStatus & ",AIT_ReviewerRemarks='" & sRemarks & "'"
            If iOpenCloseStatus = 1 Then
                sSql = sSql & " ,AIT_Status='Rejected' "
            ElseIf iOpenCloseStatus = 2 Then
                sSql = sSql & " ,AIT_Status='Submitted'"
            End If
            sSql = sSql & " where AIT_YearID=" & iYearID & " And AIT_PKID =" & iIssueTrackerID & " And AIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetIssueTrackerHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iWPPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim objclsGRACeGeneral As New clsGRACeGeneral
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("AuditorCrBy")
            dtTab.Columns.Add("AuditorCrOn")
            dtTab.Columns.Add("AuditorRemarks")
            dtTab.Columns.Add("ReviewerCrOn")
            dtTab.Columns.Add("ReviewerCrBy")
            dtTab.Columns.Add("ReviewerRemarks")

            sSql = "Select AITH_PKID,AITH_IssuePKID,AITH_AuditID,AITH_CustID,AITH_FunctionID,AITH_ReviewerRemarks,a.Usr_FullName as AuditorCreatedBy, b.Usr_FullName as ReviewerCreatedBy,"
            sSql = sSql & "AITH_AuditorRemarks,AITH_RRCrBy,AITH_RRCrOn,AITH_ARCrBy,AITH_ARCrOn,AITH_IPAddress,AITH_CompID From Audit_IssueTracker_History"
            sSql = sSql & " Left Join Sad_userDetails a On a.Usr_ID=AITH_ARCrBy And a.Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_userDetails b On b.Usr_ID=AITH_RRCrBy  And b.Usr_CompID=" & iACID & ""
            sSql = sSql & " Where AITH_FunctionID=" & iFunctionID & " And AITH_AuditID=" & iAuditID & " And AITH_CustID=" & iCustID & " And AITH_IssuePKID=" & iWPPKID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("AITH_ReviewerRemarks")) = False Then
                    dr("ReviewerRemarks") = dt.Rows(i)("AITH_ReviewerRemarks")
                End If
                If IsDBNull(dt.Rows(i)("AITH_AuditorRemarks")) = False Then
                    dr("AuditorRemarks") = dt.Rows(i)("AITH_AuditorRemarks")
                End If
                If IsDBNull(dt.Rows(i)("AuditorCreatedBy")) = False Then
                    dr("AuditorCrBy") = dt.Rows(i)("AuditorCreatedBy")
                End If
                If IsDBNull(dt.Rows(i)("AITH_ARCrOn")) = False Then
                    dr("AuditorCrOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("AITH_ARCrOn"), "D")
                End If
                If IsDBNull(dt.Rows(i)("AITH_RRCrOn")) = False Then
                    dr("ReviewerCrOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("AITH_RRCrOn"), "D")
                End If
                If IsDBNull(dt.Rows(i)("ReviewerCreatedBy")) = False Then
                    dr("ReviewerCrBy") = dt.Rows(i)("ReviewerCreatedBy")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueTrackerdashBoardToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer,
                                                      ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iSubFunID As Integer, ByVal iWorkPaperID As Integer) As DataTable
        Dim sSql As String, sWorkPaperNo As String = "", sMaxID As String = ""
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Dim iIssuePKID As Integer
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("IssueNo")
            dtTab.Columns.Add("IssueName")
            dtTab.Columns.Add("Severity")
            dtTab.Columns.Add("RiskCategory")

            sSql = "Select AIT_PKID,AIT_Status,AIT_IssueJobNo,AIT_IssueName,AIT_WorkPaperID,AIT_SeverityID,AIT_RiskCategoryID,b.RAM_Name as Severity,c.RAM_Name as RiskCategory,d.AWP_WorkPaperNo From Audit_IssueTracker_Details"
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_PKID=AIT_SeverityID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_PKID=AIT_RiskCategoryID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_WorkPaper d on d.AWP_AuditCode=AIT_AuditCode  And AIT_FunctionID=AWP_FunctionID "
            sSql = sSql & " And AIT_SubFunctionID=AWP_SubFunctionID And AIT_ProcessID=AWP_ProcessID And "
            sSql = sSql & " AIT_SubProcessID=AWP_SubProcessID And AIT_RiskID=AWP_RiskID And AIT_ControlID=AWP_ControlID  And d.AWP_CompID=" & iACID & ""
            sSql = sSql & " Where AIT_YearID=" & iYearID & " And AIT_FunctionID=" & iFunctionID & " And AIT_AuditCode=" & iAuditID & " And AIT_CustID=" & iCustID & ""
            sSql = sSql & " And AIT_CompID=" & iACID & " And AIT_SubFunctionID=" & iSubFunID & ""
            If iWorkPaperID > 0 Then
                sSql = sSql & "And AWP_PKID=" & iWorkPaperID & ""
            End If
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("AIT_IssueJobNo")) = False Then
                    iIssuePKID = dt.Rows(i)("AIT_PKID")
                    sWorkPaperNo = dt.Rows(i)("AWP_WorkPaperNo")
                    If iIssuePKID = 0 Then
                        sMaxID = "001"
                    ElseIf iIssuePKID > 0 And iIssuePKID < 10 Then
                        sMaxID = "00" & iIssuePKID
                    ElseIf iIssuePKID >= 10 And iIssuePKID < 100 Then
                        sMaxID = "0" & iIssuePKID
                    Else
                        sMaxID = iIssuePKID
                    End If
                    dr("IssueNo") = sWorkPaperNo & "/IT-" & sMaxID
                End If
                If IsDBNull(dt.Rows(i)("AIT_IssueName")) = False Then
                    dr("IssueName") = dt.Rows(i)("AIT_IssueName")
                End If
                If IsDBNull(dt.Rows(i)("Severity")) = False Then
                    dr("Severity") = dt.Rows(i)("Severity")
                End If
                If IsDBNull(dt.Rows(i)("RiskCategory")) = False Then
                    dr("RiskCategory") = dt.Rows(i)("RiskCategory")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabinetID As Integer, ByVal iDocTypeID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select PGE_BASENAME,PGE_TITLE from edt_page Where PGE_CompID=" & iACID & " And PGE_Delflag='A'"
            If iCabinetID > 0 And iDocTypeID > 0 Then
                sSql = sSql & " And PGE_Cabinet=" & iCabinetID & " And PGE_DOCUMENT_TYPE=" & iDocTypeID & ""
            End If
            sSql = sSql & " Order by PGE_TITLE"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueRatingID(ByVal sAC As String, ByVal iACID As Integer, ByVal iID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select IKB_IssueRatingID from SAD_IssueKnowledgeBase_Master Where IKB_CompID=" & iACID & " And IKB_DelFlag='A'"
            sSql = sSql & " And IKB_ID=" & iID & " Order by IKB_ID"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
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
    Public Function GetSeverityChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtTab As New DataTable, dtRating As New DataTable, dtMaster As New DataTable
        Dim i As Integer
        Try
            dt.Columns.Add("Count")
            dt.Columns.Add("Name")
            dt.Columns.Add("Color")
            sSql = "Select AIT_SeverityID from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_YearID=" & iYearID & ""
            sSql = sSql & " And AIT_FunctionID=" & iFunID & " And AIT_SubFunctionID=" & iSubFunID & ""
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
    Public Function GetTotalSeverity(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) from Audit_IssueTracker_Details Where AIT_CompID =" & iACID & " And AIT_AuditCode=" & iAuditID & " And AIT_YearID=" & iYearID & ""
            sSql = sSql & " And AIT_FunctionID=" & iFunID & " And AIT_SubFunctionID=" & iSubFunID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckCabName(ByVal sAC As String, ByVal sCabName As String, ByVal iCabID As Integer, Optional ByVal iGrpId As Integer = 0) As Integer
        Dim sSql As String
        Try
            If (iGrpId = 0) Then
                'Assume cabinet name need to be checked for all groups
                sSql = "Select CBN_ID from edt_cabinet where CBN_Name='" & sCabName & "' and  CBN_ID <>" & iCabID & " and CBN_Parent=-1 and (CBN_DelStatus='A' or CBN_DelStatus='W')"
            Else
                'Check cabinet name only for that group
                sSql = "Select CBN_ID from edt_cabinet where CBN_Name='" & sCabName & "' and CBN_ParGrp=" & iGrpId & " and CBN_ID <> " & iCabID & "  and CBN_Parent=-1 and (CBN_DelStatus='A' or CBN_DelStatus='W')"
            End If
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckSubCabName(ByVal sAC As String, ByVal sCabName As String, ByVal iCabID As Integer, ByVal iCabNODE As Integer) As Integer
        Dim sSql As String
        Try
            'Check cabinet name only for that group
            sSql = "Select CBN_ID from edt_cabinet where CBN_Name='" & sCabName & "' and CBN_ID<>'" & iCabNODE & "'  and (CBN_DelStatus='A' or CBN_DelStatus='W') and CBN_Parent='" & iCabID & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckFoldersName(ByVal sAC As String, ByVal sFolName As String, ByVal iCabID As Integer, ByVal iFolID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select FOL_FOLID from edt_folder where FOL_NAME='" & sFolName & "' and FOL_CABINET='" & iCabID & "' and FOL_FOLID<>'" & iFolID & "'  and( FOL_STATUS='A'  or FOL_STATUS='W') "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAvailabilityDescName(ByVal sAC As String, ByVal sName As String, Optional ByVal iDescId As Int16 = 0) As Integer
        Dim sSql As String
        Dim iRet As Integer
        Try
            sSql = "Select DES_ID from EDT_DESCRIPTIOS where DESC_NAME='" & sName & "' and DES_ID<>" & iDescId & " "
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAvailabilityDocType(ByVal sAC As String, ByVal sDocTypeName As String, Optional ByVal iDocID As Int16 = 0) As Integer
        Dim sSql As String
        Dim iRet As Integer
        Try
            If iDocID <> 0 Then
                sSql = "Select DOT_DOCTYPEID From EDT_DOCUMENT_TYPE where Dot_Docname='" & sDocTypeName & "' And  dot_doctypeid<>" & iDocID & " "
            Else
                sSql = "Select DOT_DOCTYPEID From EDT_DOCUMENT_TYPE where Dot_Docname='" & sDocTypeName & "' "
            End If
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAvailabilityIssueName(ByVal sAC As String, ByVal iACID As Integer, ByVal iCabinetID As Integer, ByVal iDocTypeID As Integer, ByVal sIssueName As String) As Integer
        Dim sSql As String
        Dim iRet As Integer
        Try
            sSql = "Select PGE_BASENAME From edt_page where pge_cabinet=" & iCabinetID & " And  pge_document_type=" & iDocTypeID & " And PGE_CompID=" & iACID & ""
            sSql = sSql & " And PGE_TITLE='" & sIssueName & "'"
            iRet = objDBL.SQLExecuteScalarInt(sAC, sSql)
            Return iRet
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachmentPath(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAttachID As Integer, ByVal iAttachDocID As Integer) As String
        Dim con As New OleDb.OleDbConnection
        Dim Pdr As OleDb.OleDbDataReader
        Dim sSql As String, sDBPath As String, sDBFilePath As String
        Try
            sSql = "Select ATCH_DocId,ATCH_FNAME,atch_ext from EDT_ATTACHMENTS where ATCH_CompID=" & iACID & " And ATCH_ID = " & iAttachID & " And ATCH_DOCID = " & iAttachDocID & ""
            Pdr = objDBL.SQLDataReader(sAC, sSql)
            If Pdr.HasRows Then
                While Pdr.Read()
                    sDBPath = objclsGeneralFunctions.GetGRACeSettingValue(sAC, iACID, "FileInDBPath")
                    If sDBPath.EndsWith("\") = False Then
                        sDBPath = sDBPath & "\Attachments\" & Pdr("ATCH_DocId") \ 301
                    Else
                        sDBPath = sDBPath & "Attachments\" & Pdr("ATCH_DocId") \ 301
                    End If
                    If System.IO.Directory.Exists(sDBPath) = True Then
                        sDBFilePath = sDBPath & "\" & Pdr("ATCH_DocId") & "." & Pdr("atch_ext")
                        If System.IO.File.Exists(sDBFilePath) = True Then
                            Return sDBFilePath
                        Else
                            Return ""
                        End If
                    End If
                End While
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function InsertPageDetails(ByVal sAC As String, iACID As Integer, ByVal iCBN_NODE As Integer, ByVal iSCBN_NODE As Integer, ByVal iFol_ID As Integer, ByVal iPGE_DOCUMENT_TYPE As Integer, ByVal iUserID As Integer, ByVal sTitle As String, ByVal sPathName As String)
        Dim aSql As String
        Dim iMax As Integer = 0
        Try
            iMax = objclsGeneralFunctions.GetMaxID(sAC, iACID, "edt_page", "PGE_BASENAME", "PGE_Compid")
            If sPathName = "" Then
                aSql = "Insert Into edt_page (PGE_BASENAME,PGE_CABINET,PGE_SubCabinet,PGE_FOLDER,PGE_Details_ID,PGE_Compid,pge_Delflag,PGE_Date,PGE_CrBy,PGE_DOCUMENT_TYPE,PGE_Status,PGE_TITLE,PGE_ext,pge_size) 
            Values (" & iMax & "," & iCBN_NODE & "," & iSCBN_NODE & "," & iFol_ID & "," & iMax & "," & iACID & ",'A'," & Today.Date & "," & iUserID & "," & iPGE_DOCUMENT_TYPE & ",'A','" & sTitle & "','.xlsx',0)"
            Else
                aSql = "Insert Into edt_page (PGE_BASENAME,PGE_CABINET,PGE_SubCabinet,PGE_FOLDER,PGE_Details_ID,PGE_Compid,pge_Delflag,PGE_Date,PGE_CrBy,PGE_DOCUMENT_TYPE,PGE_Status,PGE_TITLE,PGE_ext,pge_size) 
            Values (" & iMax & "," & iCBN_NODE & "," & iSCBN_NODE & "," & iFol_ID & "," & iMax & "," & iACID & ",'A'," & Today.Date & "," & iUserID & "," & iPGE_DOCUMENT_TYPE & ",'A','" & sTitle & "','" & sPathName & "',0)"
            End If
            objDBL.SQLExecuteNonQuery(sAC, aSql)
            Return iMax
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAttachmentNames(ByVal sAC As String, ByVal iACID As Integer, ByVal iAttachID As Integer, ByVal iDocID As Integer) As String
        Dim sSql As String
        Dim dt As New DataTable
        Dim sAttachment As String = ""
        Try
            sSql = "Select (ATCH_FName + '.' + ATCH_EXT) as ATCH_FName from EDT_Attachments"
            sSql = sSql & " Where ATCH_ID=" & iAttachID & " And ATCH_DOCID=" & iDocID & " And ATCH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select AIT_PGEDetailId From Audit_IssueTracker_Details Where AIT_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " AIT_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " AIT_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " AIT_CustID=" & iCustID & " And AIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_IssueTracker_Details Set AIT_AttachID=" & iAttachID & ",AIT_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " AIT_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " AIT_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " AIT_YearID=" & iYearID & " And AIT_CustID=" & iCustID & " And AIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetSubFunctionFromWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select AWP_SubFunctionID from Audit_WorkPaper Where AWP_YearID=" & iYearID & " And AWP_FunctionID=" & iFunID & " And AWP_AuditCode=" & iAuditID & ""
            sSql = sSql & " And AWP_CustID=" & iCustID & "  And AWP_CompID=" & iACID & " and (AWP_Status='Saved' Or AWP_Status='Updated' Or AWP_Status='Submitted')"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
