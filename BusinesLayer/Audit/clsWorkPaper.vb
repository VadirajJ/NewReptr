Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web

Public Structure strWorkPaper
    Private AWP_PKID As Integer
    Private AWP_YearID As Integer
    Private AWP_CustID As Integer
    Private AWP_AuditCode As Integer
    Private AWP_FunctionID As Integer
    Private AWP_SubFunctionID As Integer
    Private AWP_ProcessID As Integer
    Private AWP_SubProcessID As Integer
    Private AWP_RiskID As Integer
    Private AWP_ControlID As Integer
    Private AWP_ChecksID As Integer
    Private AWP_WorkPaperNo As String
    Private AWP_TypeofTestID As Integer
    Private AWP_ConclusionID As Integer
    Private AWP_WorkPaperDone As String
    Private AWP_AuditorObservationName As String
    Private AWP_Note As String
    Private AWP_AuditeeResponseName As String
    Private AWP_Response As String
    Private AWP_AttachID As Integer
    Private AWP_CrBy As Integer
    Private AWP_UpdatedBy As Integer
    Private AWP_IPAddress As String
    Private AWP_CompID As Integer
    Private AWP_OpenCloseStatus As Integer
    Private AWP_AuditorRemarks As String
    Private AWP_ReviewerRemarks As String
    Public Property sAWP_AuditorRemarks() As String
        Get
            Return (AWP_AuditorRemarks)
        End Get
        Set(ByVal Value As String)
            AWP_AuditorRemarks = Value
        End Set
    End Property
    Public Property sAWP_ReviewerRemarks() As String
        Get
            Return (AWP_ReviewerRemarks)
        End Get
        Set(ByVal Value As String)
            AWP_ReviewerRemarks = Value
        End Set
    End Property
    Public Property iAWP_OpenCloseStatus() As Integer
        Get
            Return (AWP_OpenCloseStatus)
        End Get
        Set(ByVal Value As Integer)
            AWP_OpenCloseStatus = Value
        End Set
    End Property
    Public Property iAWP_PKID() As Integer
        Get
            Return (AWP_PKID)
        End Get
        Set(ByVal Value As Integer)
            AWP_PKID = Value
        End Set
    End Property
    Public Property iAWP_YearID() As Integer
        Get
            Return (AWP_YearID)
        End Get
        Set(ByVal Value As Integer)
            AWP_YearID = Value
        End Set
    End Property
    Public Property iAWP_CustID() As Integer
        Get
            Return (AWP_CustID)
        End Get
        Set(ByVal Value As Integer)
            AWP_CustID = Value
        End Set
    End Property
    Public Property iAWP_AuditCode() As Integer
        Get
            Return (AWP_AuditCode)
        End Get
        Set(ByVal Value As Integer)
            AWP_AuditCode = Value
        End Set
    End Property
    Public Property iAWP_FunctionID() As Integer
        Get
            Return (AWP_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            AWP_FunctionID = Value
        End Set
    End Property
    Public Property iAWP_SubFunctionID() As Integer
        Get
            Return (AWP_SubFunctionID)
        End Get
        Set(ByVal Value As Integer)
            AWP_SubFunctionID = Value
        End Set
    End Property
    Public Property iAWP_ProcessID() As Integer
        Get
            Return (AWP_ProcessID)
        End Get
        Set(ByVal Value As Integer)
            AWP_ProcessID = Value
        End Set
    End Property
    Public Property iAWP_SubProcessID() As Integer
        Get
            Return (AWP_SubProcessID)
        End Get
        Set(ByVal Value As Integer)
            AWP_SubProcessID = Value
        End Set
    End Property
    Public Property iAWP_ChecksID() As Integer
        Get
            Return (AWP_ChecksID)
        End Get
        Set(ByVal Value As Integer)
            AWP_ChecksID = Value
        End Set
    End Property
    Public Property iAWP_RiskID() As Integer
        Get
            Return (AWP_RiskID)
        End Get
        Set(ByVal Value As Integer)
            AWP_RiskID = Value
        End Set
    End Property
    Public Property iAWP_ControlID() As Integer
        Get
            Return (AWP_ControlID)
        End Get
        Set(ByVal Value As Integer)
            AWP_ControlID = Value
        End Set
    End Property
    Public Property sAWP_WorkPaperNo() As String
        Get
            Return (AWP_WorkPaperNo)
        End Get
        Set(ByVal Value As String)
            AWP_WorkPaperNo = Value
        End Set
    End Property
    Public Property iAWP_TypeofTestID() As Integer
        Get
            Return (AWP_TypeofTestID)
        End Get
        Set(ByVal Value As Integer)
            AWP_TypeofTestID = Value
        End Set
    End Property
    Public Property iAWP_ConclusionID() As Integer
        Get
            Return (AWP_ConclusionID)
        End Get
        Set(ByVal Value As Integer)
            AWP_ConclusionID = Value
        End Set
    End Property
    Public Property sAWP_WorkPaperDone() As String
        Get
            Return (AWP_WorkPaperDone)
        End Get
        Set(ByVal Value As String)
            AWP_WorkPaperDone = Value
        End Set
    End Property
    Public Property sAWP_AuditorObservationName() As String
        Get
            Return (AWP_AuditorObservationName)
        End Get
        Set(ByVal Value As String)
            AWP_AuditorObservationName = Value
        End Set
    End Property
    Public Property sAWP_Note() As String
        Get
            Return (AWP_Note)
        End Get
        Set(ByVal Value As String)
            AWP_Note = Value
        End Set
    End Property
    Public Property sAWP_AuditeeResponseName() As String
        Get
            Return (AWP_AuditeeResponseName)
        End Get
        Set(ByVal Value As String)
            AWP_AuditeeResponseName = Value
        End Set
    End Property
    Public Property sAWP_Response() As String
        Get
            Return (AWP_Response)
        End Get
        Set(ByVal Value As String)
            AWP_Response = Value
        End Set
    End Property
    Public Property iAWP_AttachID() As Integer
        Get
            Return (AWP_AttachID)
        End Get
        Set(ByVal Value As Integer)
            AWP_AttachID = Value
        End Set
    End Property
    Public Property iAWP_CrBy() As Integer
        Get
            Return (AWP_CrBy)
        End Get
        Set(ByVal Value As Integer)
            AWP_CrBy = Value
        End Set
    End Property
    Public Property iAWP_UpdatedBy() As Integer
        Get
            Return (AWP_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            AWP_UpdatedBy = Value
        End Set
    End Property
    Public Property sAWP_IPAddress() As String
        Get
            Return (AWP_IPAddress)
        End Get
        Set(ByVal Value As String)
            AWP_IPAddress = Value
        End Set
    End Property
    Public Property iAWP_CompID() As Integer
        Get
            Return (AWP_CompID)
        End Get
        Set(ByVal Value As Integer)
            AWP_CompID = Value
        End Set
    End Property
End Structure
Public Class clsWorkPaper
    Private objDBL As New DatabaseLayer.DBHelper
    Dim objclsGRACeGeneral As clsGRACeGeneral
    Dim objclsGeneralFunctions As clsGeneralFunctions
    Public Function GetWorkpaperMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(AWP_PKID)+1 From Audit_WorkPaper Where AWP_CompID=" & iACID & "  And AWP_AuditCode=" & iAuditID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRriskControlMatrixCount(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(APMCM_APMPKID) from Audit_APM_ChecksMatrix Where APMCM_APMPKID =" & iAuditID & " And APMCM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalWorkPapers(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String = ""
        Try
            sSql = "Select Count(APMCM_APMPKID) from Audit_APM_ChecksMatrix Where APMCM_APMPKID =" & iAuditID & " And APMCM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperNotStarted(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String, sSql1 As String
        Dim iPKID As Integer, iAPMPKID As Integer, iResult As Integer
        Try
            sSql1 = "Select Count(APMCM_APMPKID) from Audit_APM_ChecksMatrix Where APMCM_APMPKID =" & iAuditID & " And APMCM_CompID=" & iACID & ""
            iAPMPKID = objDBL.SQLExecuteScalarInt(sAC, sSql1)
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & ""
            iPKID = objDBL.SQLExecuteScalarInt(sAC, sSql)
            iResult = (iAPMPKID - iPKID)
            Return iResult
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperStarted(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & " and AWP_Status<>'Submitted'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperCompleted(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & " and AWP_Status='Submitted'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Charts Work Paper
    Public Function GetWorkPaperOpenChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & " And AWP_OpenCloseStatus=1 "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperClosedChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(AWP_PKID) from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & " And AWP_OpenCloseStatus=2 "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperNO(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select AWP_PKID,AWP_WorkPaperNo from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & "  Order by AWP_PKID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunSunProRiskContChecksIDFromWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select AWP_FunctionID,AWP_SubFunctionID,AWP_ProcessID,AWP_SubProcessID,AWP_RiskID,AWP_ControlID,AWP_ChecksID from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_PKID=" & iPKID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer,
                                  ByVal iSubFunctionID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, ByVal iRiskID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select AWP_PKID from Audit_WorkPaper Where AWP_CompID =" & iACID & " And AWP_AuditCode=" & iAuditID & " and AWP_FunctionID =" & iFunctionID & "  and AWP_CustID =" & iCustID & ""
            sSql = sSql & "And AWP_SubFunctionID=" & iSubFunctionID & " And AWP_ProcessID =" & iProcessID & " And AWP_SubProcessID =" & iSubProcessID & " And AWP_RiskID =" & iRiskID & " And AWP_ControlID=" & iControlID & " And AWP_ChecksID=" & iChecksID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetWorkPaperStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Audtior" Then
                sSql = "Select AWP_AuditorRemarks from Audit_WorkPaper where AWP_PKID=" & iPKID & " And AWP_CompID=" & iACID & ""
            ElseIf sType = "Reviewer" Then
                sSql = "Select AWP_ReviewerRemarks from Audit_WorkPaper where AWP_PKID=" & iPKID & " And AWP_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iWorkPaperID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AWP_PKID,AWP_WorkPaperNo,AWP_TypeofTestID,AWP_ConclusionID,AWP_WorkPaperDone,AWP_AuditorObservationName,AWP_Note,AWP_AuditeeResponseName,AWP_Response,AWP_PGEDetailId,"
            sSql = sSql & " AWP_AttachID,AWP_Status, AWP_ReviewerRemarks,AWP_AuditorRemarks,AWP_OpenCloseStatus from Audit_WorkPaper where AWP_PKID =" & iWorkPaperID & " And AWP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmittedWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_WorkPaper Set AWP_Status='Auditor Submitted',AWP_SubmittedBy=" & iUserID & ", AWP_SubmittedOn=GetDate()"
            sSql = sSql & " where AWP_YearID=" & iYearID & "And AWP_PKID =" & iWorkPaperID & " And AWP_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ReviewerSubmittedWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer,
                                                 ByVal iWorkPaperID As Integer, ByVal iOpenCloseStatus As Integer, ByVal sRemarks As String)
        Dim sSql As String
        Try
            sSql = "Update Audit_WorkPaper Set AWP_ReviewedBy=" & iUserID & ", AWP_ReviewedOn=GetDate(), AWP_OpenCloseStatus =" & iOpenCloseStatus & ",AWP_ReviewerRemarks='" & sRemarks & "'"
            If iOpenCloseStatus = 1 Then
                sSql = sSql & " ,AWP_Status='Rejected' "
            ElseIf iOpenCloseStatus = 2 Then
                sSql = sSql & " ,AWP_Status='Submitted'"
            End If
            sSql = sSql & " where AWP_YearID=" & iYearID & " And AWP_PKID =" & iWorkPaperID & " And AWP_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetWorkPaperDetailsHistoryReviewerID(ByVal sAC As String, ByVal iACID As Integer, ByVal iWorkPaperID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Max(AWPH_PKID) From Audit_WorkPaper_History Where AWPH_WPID=" & iWorkPaperID & " And AWPH_AuditID=" & iAuditID & " And "
            sSql = sSql & " AWPH_CustID=" & iCustID & " And AWPH_FunctionID=" & iFunction & " And AWPH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub AuditorSaveWorkPaperDetailsHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer,
                                                       ByVal sRemarks As String, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sIPAddress As String)
        Dim sSql As String
        Dim iAWPHIDMaxID As Integer
        Dim objclsGeneralFunctions As New clsGeneralFunctions
        Try
            iAWPHIDMaxID = objclsGeneralFunctions.GetMaxID(sAC, iACID, "Audit_WorkPaper_History", "AWPH_PKID", "AWPH_CompID")
            sSql = "Insert Into Audit_WorkPaper_History (AWPH_PKID,AWPH_WPID,AWPH_AuditID,AWPH_CustID,AWPH_FunctionID,AWPH_AuditorRemarks,"
            sSql = sSql & " AWPH_ARCrBy,AWPH_ARCrOn,AWPH_IPAddress,AWPH_CompID)"
            sSql = sSql & "Values(" & iAWPHIDMaxID & "," & iWorkPaperID & "," & iAuditID & "," & iCustID & "," & iFunction & ",'" & sRemarks & "',"
            sSql = sSql & "" & iUserID & ",GetDate(),'" & sIPAddress & "'," & iACID & ")"
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub ReviewerSaveWorkPaperDetailsHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer, ByVal sRemarks As String,
                                                    ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sIPAddress As String, ByVal iAWPHIDMaxID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_WorkPaper_History Set AWPH_ReviewerRemarks='" & sRemarks & "',AWPH_RRCrBy=" & iUserID & ",AWPH_RRCrOn=GetDate(),AWPH_IPAddress='" & sIPAddress & "'"
            sSql = sSql & "Where AWPH_PKID=" & iAWPHIDMaxID & " And AWPH_WPID=" & iWorkPaperID & " And AWPH_AuditID=" & iAuditID & " And "
            sSql = sSql & " AWPH_CustID=" & iCustID & " And AWPH_FunctionID=" & iFunction & " And AWPH_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadWorkPaperAPMDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer,
                                            ByVal iSubFuntionID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, iRiskID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer, ByVal iWorkPaperID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Distinct(APM_ID),APM_CustID,APM_AuditCode,APM_CustID,APM_FunctionID, APM_AuditorsRoleID,APM_AuditTeamsID,APM_AttachID,APM_APMTAStatus,"
            sSql = sSql & " ENT_ID,SEM_ID,PM_ID,SPM_ID,SPM_Name,SEM_Name,MMM_RISKID,MMM_Risk,APM_Objectives,PM_Name,MMM_FunID,MMM_SEMID,MMM_PMID,MMM_SPMID,"
            sSql = sSql & " APM_PartnersID, MMM_CONTROLID, MMM_Control, MMM_ChecksID, MMM_CHECKS,Ent_EntityName,Cust_Name,AWP_WorkPaperNo from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_APM_ChecksMatrix On APMCM_FunctionID=APM_FunctionID And APMCM_CompID=" & iACID & " And APMCM_CustID=" & iCustID & ""
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=APM_FunctionID and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_FunID=APM_FunctionID And MMM_SEMID=APMCM_SubFunctionID And MMM_PMID=APMCM_ProcessID And"
            sSql = sSql & " MMM_SPMID=APMCM_SubProcessID And MMM_RISKID=APMCM_RiskID and MMM_CONTROLID=APMCM_ControlID and MMM_ChecksID=APMCM_ChecksID"
            sSql = sSql & " And MMM_CustID=" & iCustID & " Left Join Audit_WorkPaper On AWP_AuditCode = APM_ID And AWP_CompID=" & iACID & " And AWP_CustID=" & iCustID & ""
            sSql = sSql & " where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & "and APM_ID =" & iAuditID & "and APM_CustID =" & iCustID & ""
            sSql = sSql & " and APM_FunctionID =" & iFunction & " And APMCM_SubFunctionID=" & iSubFuntionID & " And APMCM_ProcessID=" & iProcessID & ""
            sSql = sSql & " And APMCM_SubProcessID=" & iSubProcessID & " And APMCM_RiskID=" & iRiskID & " And APMCM_ControlID=" & iControlID & " And APMCM_ChecksID=" & iChecksID & ""
            If iWorkPaperID > 0 Then
                sSql = sSql & " And AWP_PKID=" & iWorkPaperID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function GetWorpPaperHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iWPPKID As Integer) As DataTable
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

            sSql = "Select AWPH_PKID,AWPH_WPID,AWPH_AuditID,AWPH_CustID,AWPH_FunctionID,AWPH_ReviewerRemarks,a.Usr_FullName as AuditorCreatedBy, b.Usr_FullName as ReviewerCreatedBy,"
            sSql = sSql & "AWPH_AuditorRemarks,AWPH_RRCrBy,AWPH_RRCrOn,AWPH_ARCrBy,AWPH_ARCrOn,AWPH_IPAddress,AWPH_CompID From Audit_WorkPaper_History"
            sSql = sSql & " Left Join Sad_userDetails a On a.Usr_ID=AWPH_ARCrBy And a.Usr_CompID=" & iACID & ""
            sSql = sSql & " Left Join Sad_userDetails b On b.Usr_ID=AWPH_RRCrBy  And b.Usr_CompID=" & iACID & ""
            sSql = sSql & " Where AWPH_FunctionID=" & iFunctionID & " And AWPH_AuditID=" & iAuditID & " And AWPH_CustID=" & iCustID & " And AWPH_WPID=" & iWPPKID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("AWPH_ReviewerRemarks")) = False Then
                    dr("ReviewerRemarks") = dt.Rows(i)("AWPH_ReviewerRemarks")
                End If
                If IsDBNull(dt.Rows(i)("AWPH_AuditorRemarks")) = False Then
                    dr("AuditorRemarks") = dt.Rows(i)("AWPH_AuditorRemarks")
                End If
                If IsDBNull(dt.Rows(i)("AuditorCreatedBy")) = False Then
                    dr("AuditorCrBy") = dt.Rows(i)("AuditorCreatedBy")
                End If
                If IsDBNull(dt.Rows(i)("AWPH_ARCrOn")) = False Then
                    dr("AuditorCrOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("AWPH_ARCrOn"), "D")
                End If
                If IsDBNull(dt.Rows(i)("AWPH_RRCrOn")) = False Then
                    dr("ReviewerCrOn") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("AWPH_RRCrOn"), "D")
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
    Public Function LoadMappedRiskControlMatrixinWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("MMMID")
            dtTab.Columns.Add("FunctionId")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("RisK")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("WorkPaperNo")
            dtTab.Columns.Add("WorkPaperID")
            dtTab.Columns.Add("Status")

            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,AWP_WorkPaperNo,AWP_Status,AWP_PKID,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & " And MMM_CustID=" & iCustID & ""
            sSql = sSql & " Left Join Audit_WorkPaper On AWP_AuditCode=APMCM_APMPKID And AWP_CompID=" & iACID & "And AWP_FunctionID=APMCM_FunctionID And AWP_SubFunctionID=APMCM_SubFunctionID And"
            sSql = sSql & " AWP_ProcessID=APMCM_ProcessID And AWP_SubProcessID=APMCM_SubProcessID And AWP_RiskID=APMCM_RiskID And AWP_ControlID=APMCM_ControlID And AWP_ChecksID=APMCM_ChecksID "
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & " And MMM_CustID=" & iCustID & " And MMM_Module='A'"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("MMMID") = 0 : dr("FunctionId") = 0 : dr("SubFunctionID") = 0 : dr("ProcessID") = 0 : dr("SubProcessID") = 0 : dr("RisKID") = 0 : dr("ControlID") = 0 : dr("ChecksID") = 0 : dr("WorkPaperID") = 0
                dr("SubFunction") = "" : dr("Process") = "" : dr("SubProcess") = "" : dr("Checks") = ""
                If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                    dr("MMMID") = dt.Rows(i)("MMM_ID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_FunctionID")) = False Then
                    dr("FunctionId") = dt.Rows(i)("APMCM_FunctionID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_SubFunctionID")) = False Then
                    dr("SubFunctionID") = dt.Rows(i)("APMCM_SubFunctionID")
                End If
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ProcessID")) = False Then
                    dr("ProcessID") = dt.Rows(i)("APMCM_ProcessID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_SubProcessID")) = False Then
                    dr("SubProcessID") = dt.Rows(i)("APMCM_SubProcessID")
                End If
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_RiskID")) = False Then
                    dr("RisKID") = dt.Rows(i)("APMCM_RiskID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_RiskID")) = False Then
                    dr("RisK") = dt.Rows(i)("MMM_Risk")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ControlID")) = False Then
                    dr("ControlID") = dt.Rows(i)("APMCM_ControlID")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ControlID")) = False Then
                    dr("Control") = dt.Rows(i)("MMM_Control")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ChecksID")) = False Then
                    dr("ChecksID") = dt.Rows(i)("APMCM_ChecksID")
                End If
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperNo")) = False Then
                    dr("WorkPaperNo") = dt.Rows(i)("AWP_WorkPaperNo")
                End If
                If IsDBNull(dt.Rows(i)("AWP_PKID")) = False Then
                    dr("WorkPaperID") = dt.Rows(i)("AWP_PKID")
                End If
                If IsDBNull(dt.Rows(i)("AWP_Status")) = False Then
                    dr("Status") = dt.Rows(i)("AWP_Status")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveWorkPaperDetails(ByVal sAC As String, ByVal objWorkPaper As strWorkPaper)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_AuditCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_AuditCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_SubFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_SubFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_ProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_ProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_SubProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_SubProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_RiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_RiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_ControlID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_ControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_ChecksID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_WorkPaperNo", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_WorkPaperNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_TypeofTestID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_TypeofTestID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_ConclusionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_ConclusionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_WorkPaperDone", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_WorkPaperDone
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_AuditorObservationName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_AuditorObservationName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_Note", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_Note
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_AuditeeResponseName", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_AuditeeResponseName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_Response", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_Response
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_AuditorRemarks", OleDb.OleDbType.VarChar, 500)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_AuditorRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_ReviewerRemarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_ReviewerRemarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_OpenCloseStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_OpenCloseStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objWorkPaper.sAWP_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@AWP_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objWorkPaper.iAWP_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_WorkPaper", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixinWorkPaperToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("WorkPaperNo")

            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,AWP_WorkPaperNo,AWP_Status,AWP_PKID,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_WorkPaper On AWP_AuditCode=APMCM_APMPKID And AWP_CompID=" & iACID & "And AWP_FunctionID=APMCM_FunctionID And AWP_SubFunctionID=APMCM_SubFunctionID And"
            sSql = sSql & " AWP_ProcessID=APMCM_ProcessID And AWP_SubProcessID=APMCM_SubProcessID And AWP_RiskID=APMCM_RiskID And AWP_ControlID=APMCM_ControlID And AWP_ChecksID=APMCM_ChecksID "
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SubFunction") = "" : dr("Process") = "" : dr("SubProcess") = "" : dr("Checks") = "" : dr("WorkPaperNo") = ""
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_RiskID")) = False Then
                    dr("RisK") = dt.Rows(i)("MMM_Risk")
                End If
                If IsDBNull(dt.Rows(i)("APMCM_ControlID")) = False Then
                    dr("Control") = dt.Rows(i)("MMM_Control")
                End If
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperNo")) = False Then
                    dr("WorkPaperNo") = dt.Rows(i)("AWP_WorkPaperNo")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GettFAFieldWorkWPNO(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iWPID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select AWP_WorkPaperNo from Audit_WorkPaper where AWP_AuditCode=" & iAuditID & " And AWP_PKID=" & iWPID & " And AWP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GettFAFieldWorkWPStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iWPID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select AWP_Status From Audit_WorkPaper Where AWP_AuditCode=" & iAuditID & " And AWP_PKID=" & iWPID & " And AWP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperToUpload(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Audit Code")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("Sub Function")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("Sub Process")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("Audit Checks")
            dtTab.Columns.Add("Work Paper Done")
            dtTab.Columns.Add("Type of Test")
            dtTab.Columns.Add("Conclusion")
            dtTab.Columns.Add("Auditor Observation Name")
            dtTab.Columns.Add("Note")
            dtTab.Columns.Add("Auditee Response")
            dtTab.Columns.Add("Response")
            dtTab.Columns.Add("AuditorRemarks")
            dtTab.Columns.Add("ReviewerRemarks")
            dtTab.Columns.Add("OpenCloseStatus")

            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,APM_AuditCode,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,AWP_WorkPaperNo,AWP_Status,AWP_PKID,"
            sSql = sSql & " AWP_WorkPaperDone, AWP_TypeofTestID, AWP_ConclusionID, AWP_AuditorObservationName, AWP_Note, AWP_AuditeeResponseName, AWP_Response, AWP_AuditorRemarks, AWP_ReviewerRemarks, AWP_OpenCloseStatus,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID, a.cmm_Desc as TypeOfTest, b.cmm_Desc As Conclusion From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join Audit_APM_Details On APM_ID=APMCM_APMPKID And APM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_WorkPaper On AWP_AuditCode=APMCM_APMPKID And AWP_CompID=" & iACID & "And AWP_FunctionID=APMCM_FunctionID And AWP_SubFunctionID=APMCM_SubFunctionID And"
            sSql = sSql & " AWP_ProcessID=APMCM_ProcessID And AWP_SubProcessID=APMCM_SubProcessID And AWP_RiskID=APMCM_RiskID And AWP_ControlID=APMCM_ControlID And AWP_ChecksID=APMCM_ChecksID "
            sSql = sSql & " Left Join Content_Management_Master a On a.cmm_ID= AWP_TypeofTestID"
            sSql = sSql & " Left Join Content_Management_Master b On b.cmm_ID= AWP_ConclusionID"
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("APM_AuditCode")) = False Then
                    dr("Audit Code") = dt.Rows(i)("APM_AuditCode")
                End If
                If IsDBNull(dt.Rows(i)("Ent_EntityName")) = False Then
                    dr("Function") = dt.Rows(i)("Ent_EntityName")
                End If
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("Sub Function") = dt.Rows(i)("SEM_Name")
                End If
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("Sub Process") = dt.Rows(i)("SPM_Name")
                End If
                If IsDBNull(dt.Rows(i)("MMM_Risk")) = False Then
                    dr("RisK") = dt.Rows(i)("MMM_Risk")
                End If
                If IsDBNull(dt.Rows(i)("MMM_Control")) = False Then
                    dr("Control") = dt.Rows(i)("MMM_Control")
                End If
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Audit Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                If IsDBNull(dt.Rows(i)("AWP_WorkPaperDone")) = False Then
                    dr("Work Paper Done") = dt.Rows(i)("AWP_WorkPaperDone")
                End If
                If IsDBNull(dt.Rows(i)("AWP_TypeofTestID")) = False Then
                    dr("Type of Test") = dt.Rows(i)("TypeOfTest")
                End If
                If IsDBNull(dt.Rows(i)("AWP_ConclusionID")) = False Then
                    dr("Conclusion") = dt.Rows(i)("Conclusion")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AuditorObservationName")) = False Then
                    dr("Auditor Observation Name") = dt.Rows(i)("AWP_AuditorObservationName")
                End If
                If IsDBNull(dt.Rows(i)("AWP_Note")) = False Then
                    dr("Note") = dt.Rows(i)("AWP_Note")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AuditeeResponseName")) = False Then
                    dr("Auditee Response") = dt.Rows(i)("AWP_AuditeeResponseName")
                End If
                If IsDBNull(dt.Rows(i)("AWP_Response")) = False Then
                    dr("Response") = dt.Rows(i)("AWP_Response")
                End If
                If IsDBNull(dt.Rows(i)("AWP_AuditorRemarks")) = False Then
                    dr("AuditorRemarks") = dt.Rows(i)("AWP_AuditorRemarks")
                End If
                If IsDBNull(dt.Rows(i)("AWP_ReviewerRemarks")) = False Then
                    dr("ReviewerRemarks") = dt.Rows(i)("AWP_ReviewerRemarks")
                End If
                If IsDBNull(dt.Rows(i)("AWP_OpenCloseStatus")) = False Then
                    If dt.Rows(i)("AWP_OpenCloseStatus") = 1 Then
                        dr("OpenCloseStatus") = "Open"
                    ElseIf dt.Rows(i)("AWP_OpenCloseStatus") = 2 Then
                        dr("OpenCloseStatus") = "Closed"
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select AWP_PGEDetailId From Audit_WorkPaper Where AWP_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " AWP_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " AWP_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " AWP_CustID=" & iCustID & " And AWP_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_WorkPaper Set AWP_AttachID=" & iAttachID & ",AWP_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " AWP_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " AWP_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " AWP_YearID=" & iYearID & " And AWP_CustID=" & iCustID & " And AWP_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class

