Imports System
Imports System.Data
Imports System.IO
Imports DatabaseLayer
Public Class clsQuickAudit
    Private objDBL As New DatabaseLayer.DBHelper

    Private QA_PKID As Integer
    Private QA_Code As String
    Private QA_FinancialYear As Integer
    Private QA_CUSTID As Integer
    Private QA_FUNID As Integer
    Private QA_StartDate As Date
    Private QA_EndDate As Date
    Private QA_AUDITORTEAM As String
    Private QA_AUDITTITLE As String
    Private QA_Delflag As String
    Private QA_CrBy As Integer
    Private QA_UpdatedBy As Integer
    Private QA_SavedBy As Integer
    Private QA_AUDStatus As String
    Private QA_WPStatus As String
    Private QA_IPAddress As String
    Private QA_CompID As Integer

    Public Property iQA_PKID() As Integer
        Get
            Return (QA_PKID)
        End Get
        Set(ByVal Value As Integer)
            QA_PKID = Value
        End Set
    End Property
    Public Property sQA_Code() As String
        Get
            Return (QA_Code)
        End Get
        Set(ByVal Value As String)
            QA_Code = Value
        End Set
    End Property
    Public Property iQA_FinancialYear() As Integer
        Get
            Return (QA_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            QA_FinancialYear = Value
        End Set
    End Property
    Public Property iQA_CUSTID() As Integer
        Get
            Return (QA_CUSTID)
        End Get
        Set(ByVal Value As Integer)
            QA_CUSTID = Value
        End Set
    End Property
    Public Property iQA_FUNID() As Integer
        Get
            Return (QA_FUNID)
        End Get
        Set(ByVal Value As Integer)
            QA_FUNID = Value
        End Set
    End Property
    Public Property dQA_StartDate() As Date
        Get
            Return (QA_StartDate)
        End Get
        Set(ByVal Value As Date)
            QA_StartDate = Value
        End Set
    End Property
    Public Property dQA_EndDate() As Date
        Get
            Return (QA_EndDate)
        End Get
        Set(ByVal Value As Date)
            QA_EndDate = Value
        End Set
    End Property
    Public Property sQA_AUDITORTEAM() As String
        Get
            Return (QA_AUDITORTEAM)
        End Get
        Set(ByVal Value As String)
            QA_AUDITORTEAM = Value
        End Set
    End Property
    Public Property sQA_AUDITTITLE() As String
        Get
            Return (QA_AUDITTITLE)
        End Get
        Set(ByVal Value As String)
            QA_AUDITTITLE = Value
        End Set
    End Property
    Public Property sQA_Delflag() As String
        Get
            Return (QA_Delflag)
        End Get
        Set(ByVal Value As String)
            QA_Delflag = Value
        End Set
    End Property
    Public Property iQA_CrBy() As Integer
        Get
            Return (QA_CrBy)
        End Get
        Set(ByVal Value As Integer)
            QA_CrBy = Value
        End Set
    End Property
    Public Property iQA_UpdatedBy() As Integer
        Get
            Return (QA_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            QA_UpdatedBy = Value
        End Set
    End Property
    Public Property iQA_SavedBy() As Integer
        Get
            Return (QA_SavedBy)
        End Get
        Set(ByVal Value As Integer)
            QA_SavedBy = Value
        End Set
    End Property
    Public Property sQA_AUDStatus() As String
        Get
            Return (QA_AUDStatus)
        End Get
        Set(ByVal Value As String)
            QA_AUDStatus = Value
        End Set
    End Property
    Public Property sQA_WPStatus() As String
        Get
            Return (QA_WPStatus)
        End Get
        Set(ByVal Value As String)
            QA_WPStatus = Value
        End Set
    End Property
    Public Property sQA_IPAddress() As String
        Get
            Return (QA_IPAddress)
        End Get
        Set(ByVal Value As String)
            QA_IPAddress = Value
        End Set
    End Property
    Public Property iQA_CompID() As Integer
        Get
            Return (QA_CompID)
        End Get
        Set(ByVal Value As Integer)
            QA_CompID = Value
        End Set
    End Property
    Private QAM_PKID As Integer
    Private QAM_QAPKID As Integer
    Private QAM_YearID As Integer
    Private QAM_CustID As Integer
    Private QAM_FunctionID As Integer
    Private QAM_SubFunctionID As Integer
    Private QAM_ProcessID As Integer
    Private QAM_SubProcessID As Integer
    Private QAM_RiskID As Integer
    Private QAM_ControlID As Integer
    Private QAM_ChecksID As Integer
    Private QAM_MMMID As Integer
    Private QAM_Status As String
    Private QAM_IPAddress As String
    Private QAM_CompID As Integer
    Public Property iQAM_PKID() As Integer
        Get
            Return (QAM_PKID)
        End Get
        Set(ByVal Value As Integer)
            QAM_PKID = Value
        End Set
    End Property
    Public Property iQAM_QAPKID() As Integer
        Get
            Return (QAM_QAPKID)
        End Get
        Set(ByVal Value As Integer)
            QAM_QAPKID = Value
        End Set
    End Property
    Public Property iQAM_YearID() As Integer
        Get
            Return (QAM_YearID)
        End Get
        Set(ByVal Value As Integer)
            QAM_YearID = Value
        End Set
    End Property
    Public Property iQAM_CustID() As Integer
        Get
            Return (QAM_CustID)
        End Get
        Set(ByVal Value As Integer)
            QAM_CustID = Value
        End Set
    End Property
    Public Property iQAM_SubFunctionID() As Integer
        Get
            Return (QAM_SubFunctionID)
        End Get
        Set(ByVal Value As Integer)
            QAM_SubFunctionID = Value
        End Set
    End Property
    Public Property iQAM_FunctionID() As Integer
        Get
            Return (QAM_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            QAM_FunctionID = Value
        End Set
    End Property
    Public Property iQAM_ProcessID() As Integer
        Get
            Return (QAM_ProcessID)
        End Get
        Set(ByVal Value As Integer)
            QAM_ProcessID = Value
        End Set
    End Property
    Public Property iQAM_SubProcessID() As Integer
        Get
            Return (QAM_SubProcessID)
        End Get
        Set(ByVal Value As Integer)
            QAM_SubProcessID = Value
        End Set
    End Property
    Public Property iQAM_RiskID() As Integer
        Get
            Return (QAM_RiskID)
        End Get
        Set(ByVal Value As Integer)
            QAM_RiskID = Value
        End Set
    End Property
    Public Property iQAM_ControlID() As Integer
        Get
            Return (QAM_ControlID)
        End Get
        Set(ByVal Value As Integer)
            QAM_ControlID = Value
        End Set
    End Property
    Public Property iQAM_ChecksID() As Integer
        Get
            Return (QAM_ChecksID)
        End Get
        Set(ByVal Value As Integer)
            QAM_ChecksID = Value
        End Set
    End Property
    Public Property iQAM_MMMID() As Integer
        Get
            Return (QAM_MMMID)
        End Get
        Set(ByVal Value As Integer)
            QAM_MMMID = Value
        End Set
    End Property
    Public Property sQAM_Status() As String
        Get
            Return (QAM_Status)
        End Get
        Set(ByVal Value As String)
            QAM_Status = Value
        End Set
    End Property
    Public Property sQAM_IPAddress() As String
        Get
            Return (QAM_IPAddress)
        End Get
        Set(ByVal Value As String)
            QAM_IPAddress = Value
        End Set
    End Property
    Public Property iQAM_CompID() As Integer
        Get
            Return (QAM_CompID)
        End Get
        Set(ByVal Value As Integer)
            QAM_CompID = Value
        End Set
    End Property
    Public Function CheckForExistingAudit(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As Boolean
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select QA_PKID, QA_Code From QA_assessment Where QA_Code <>'' and QA_CompID=" & iACID & " and QA_FinancialYear=" & iYearID & ""
            If iCustID > 0 Then
                sSql = sSql & " And QA_CustID=" & iCustID & " "
            End If
            If iFunID > 0 Then
                sSql = sSql & " And QA_FUNID=" & iFunID & ""
            End If
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFunctionDetails(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And Ent_DelFlg='A'"
            'If iFUNUserID > 0 Then
            '    sSql = sSql & " And (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ")"
            'End If
            sSql = sSql & " Order by Ent_Entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetAUDTitleTeam(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAPMid As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select * from QA_Assessment where QA_PKid=" & iAPMid & " and QA_CompID=" & iACID & " And QA_financialYear=" & iYearID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadQAAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iQAid As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select QA_PKid, QA_Code From QA_Assessment Where QA_Code <>''  And QA_CompID=" & iACID & " and QA_financialYear=" & iYearID & " "
            If iCustID > 0 Then
                sSql = sSql & " And QA_CustId=" & iCustID & " "
            End If
            If iFunID > 0 Then
                sSql = sSql & " And QA_FUNID=" & iFunID & " "
            End If
            sSql = sSql & " Order by QA_PKid Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFAuditIDFromFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iFunctionID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select QA_PKid from QA_Assessment where QA_FUNID=" & iFunctionID & " and QA_CompID=" & iACID & " And QA_CustId=" & iCustID & " and QA_financialYear=" & iYearID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetQAAuditorTeam(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sUsers As String = ""
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "select QA_AUDITORTEAM from QA_Assessment Where QA_PKID=" & iAuditID & " And QA_CompID=" & iACID & ""
            sUsers = objDBL.SQLExecuteScalar(sAC, sSql)
            Return sUsers
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetQAAuditTitle(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select QA_AUDITTitle From QA_Assessment Where QA_PKID=" & iAuditID & " And QA_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFuncFromQAAuditID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select QA_FUNID from QA_Assessment where QA_PKID=" & iAuditID & " and QA_CompID=" & iACID & " And QA_CUSTID=" & iCustID & " "
            sSql = sSql & " and QA_AUDStatus='Submitted' and QA_FinancialYear=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadQAAuditCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select QA_PKid, QA_Code From QA_Assessment Where QA_CompID=" & iACID & " and QA_financialYear=" & iYearID & ""
            If iCustID > 0 Then
                sSql = sSql & " And QA_CustId=" & iCustID & " "
            End If
            If iFunID > 0 Then
                sSql = sSql & " And QA_FUNID=" & iFunID & " "
            End If
            sSql = sSql & " Order by QA_PKid Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixinWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer,
                                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer,
                                                           ByVal iUsrId As Integer) As DataTable
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

            If iAuditID > 0 Then
                sSql = "Select QAM_PKID,QAM_QAPKID,QAM_YearID,QAM_CustID,QAM_FunctionID,QAM_SubFunctionID,QAM_ProcessID, "
                sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,QAW_WorkPaperNo,QAW_Status,QAW_PKID,"
                sSql = sSql & " QAM_SubProcessID,QAM_RiskID,QAM_ControlID,QAM_ChecksID,QAM_MMMID From QAA_ChecksMatrix "
                sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=QAM_FunctionID And ENT_CompID=" & iACID & ""
                sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=QAM_SubFunctionID And SEM_CompID=" & iACID & ""
                sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=QAM_ProcessID And PM_CompID=" & iACID & ""
                sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=QAM_SubProcessID And SPM_CompID=" & iACID & ""
                sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=QAM_MMMID And SPM_CompID=" & iACID & " And MMM_CustID=" & iCustID & ""
                sSql = sSql & " Left Join QA_WorkPaper On QAW_AuditCode=QAM_QAPKID And QAW_CompID=" & iACID & " And QAW_FunctionID=QAM_FunctionID And QAW_SubFunctionID=QAM_SubFunctionID and"
                sSql = sSql & " QAW_ProcessID=QAM_ProcessID And QAW_SubProcessID=QAM_SubProcessID  And QAW_RiskID=QAM_RiskID And QAW_ControlID=QAM_ControlID And QAW_ChecksID=QAM_ChecksID  "
                sSql = sSql & " Where QAM_YearID=" & iYearID & " And QAM_FunctionID=" & iFunctionID & "  And QAM_CustID=" & iCustID & " And MMM_CustID=" & iCustID & " And MMM_Module='A' And QAM_QAPKID=" & iAuditID & ""

                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("MMMID") = 0 : dr("FunctionId") = 0 : dr("SubFunctionID") = 0 : dr("ProcessID") = 0 : dr("SubProcessID") = 0 : dr("RisKID") = 0 : dr("ControlID") = 0 : dr("ChecksID") = 0 : dr("WorkPaperID") = 0
                    dr("SubFunction") = "" : dr("Process") = "" : dr("SubProcess") = "" : dr("Checks") = ""
                    If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                        dr("MMMID") = dt.Rows(i)("MMM_ID")
                    End If
                    If IsDBNull(dt.Rows(i)("QAm_FunctionID")) = False Then
                        dr("FunctionId") = dt.Rows(i)("QAm_FunctionID")
                    End If
                    If IsDBNull(dt.Rows(i)("QAM_SubFunctionID")) = False Then
                        dr("SubFunctionID") = dt.Rows(i)("QAM_SubFunctionID")
                    End If
                    If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                        dr("SubFunction") = dt.Rows(i)("SEM_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("QAM_ProcessID")) = False Then
                        dr("ProcessID") = dt.Rows(i)("QAM_ProcessID")
                    End If
                    If IsDBNull(dt.Rows(i)("QAM_SubProcessID")) = False Then
                        dr("SubProcessID") = dt.Rows(i)("QAM_SubProcessID")
                    End If
                    If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                        dr("Process") = dt.Rows(i)("PM_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                        dr("SubProcess") = dt.Rows(i)("SPM_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("QAM_RiskID")) = False Then
                        dr("RisKID") = dt.Rows(i)("QAM_RiskID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_Risk")) = False Then
                        dr("RisK") = dt.Rows(i)("MMM_Risk")
                    End If
                    If IsDBNull(dt.Rows(i)("QAM_ControlID")) = False Then
                        dr("ControlID") = dt.Rows(i)("QAM_ControlID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_Control")) = False Then
                        dr("Control") = dt.Rows(i)("MMM_Control")
                    End If
                    If IsDBNull(dt.Rows(i)("QAM_ChecksID")) = False Then
                        dr("ChecksID") = dt.Rows(i)("QAM_ChecksID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                        dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                    End If
                    If IsDBNull(dt.Rows(i)("QAW_WorkPaperNo")) = False Then
                        dr("WorkPaperNo") = dt.Rows(i)("QAW_WorkPaperNo")
                    End If
                    If IsDBNull(dt.Rows(i)("QAW_PKID")) = False Then
                        dr("WorkPaperID") = dt.Rows(i)("QAW_PKID")
                    End If
                    If IsDBNull(dt.Rows(i)("QAW_Status")) = False Then
                        dr("Status") = dt.Rows(i)("QAW_Status")
                    End If
                    dtTab.Rows.Add(dr)
                Next
            ElseIf iAuditID = 0 Then
                sSql = " select MMM_ID,MMM_FunId,MMM_SEMID,MMM_PMID,MMM_SPMID,MMM_RiskID,MMM_controlID,MMM_ChecksiD,SEM_Name,PM_Name,SPM_Name,MMM_Risk,MMM_Control,MMM_CHECKS from MST_MAPPING_MASTER "
                sSql = sSql & "Left Join MSt_Entity_Master On ENT_ID=MMM_FunID And ENT_CompID=1 "
                sSql = sSql & "Left Join MST_SUBENTITY_MASTER On SEM_ID=MMM_SEMID And SEM_CompID=1 "
                sSql = sSql & "Left Join MST_PROCESS_MASTER On PM_ID=MMM_PMID And PM_CompID=1 "
                sSql = sSql & "Left Join MST_SUBPROCESS_MASTER On SPM_ID=mmm_Spmid And SPM_CompID=1  "
                sSql = sSql & " Where mmm_funid = " & iFunctionID & " And MMM_CUSTID = " & iCustID & " And MMM_Module ='A' and MMM_CompID=" & iACID & ""
                dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
                For i = 0 To dt.Rows.Count - 1
                    dr = dtTab.NewRow
                    dr("MMMID") = 0 : dr("FunctionId") = 0 : dr("SubFunctionID") = 0 : dr("ProcessID") = 0 : dr("SubProcessID") = 0 : dr("RisKID") = 0 : dr("ControlID") = 0 : dr("ChecksID") = 0 : dr("WorkPaperID") = 0
                    dr("SubFunction") = "" : dr("Process") = "" : dr("SubProcess") = "" : dr("Checks") = ""
                    If IsDBNull(dt.Rows(i)("MMM_ID")) = False Then
                        dr("MMMID") = dt.Rows(i)("MMM_ID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_FunId")) = False Then
                        dr("FunctionId") = dt.Rows(i)("MMM_FunId")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_SEMID")) = False Then
                        dr("SubFunctionID") = dt.Rows(i)("MMM_SEMID")
                    End If
                    If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                        dr("SubFunction") = dt.Rows(i)("SEM_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_PMID")) = False Then
                        dr("ProcessID") = dt.Rows(i)("MMM_PMID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_SPMID")) = False Then
                        dr("SubProcessID") = dt.Rows(i)("MMM_SPMID")
                    End If
                    If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                        dr("Process") = dt.Rows(i)("PM_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                        dr("SubProcess") = dt.Rows(i)("SPM_Name")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_RiskID")) = False Then
                        dr("RisKID") = dt.Rows(i)("MMM_RiskID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_Risk")) = False Then
                        dr("RisK") = dt.Rows(i)("MMM_Risk")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_ControlID")) = False Then
                        dr("ControlID") = dt.Rows(i)("MMM_ControlID")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_Control")) = False Then
                        dr("Control") = dt.Rows(i)("MMM_Control")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_ChecksiD")) = False Then
                        dr("ChecksID") = dt.Rows(i)("MMM_ChecksiD")
                    End If
                    If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                        dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                    End If
                    dr("WorkPaperNo") = 0
                    dr("WorkPaperID") = 0
                    dr("Status") = "A"

                    dtTab.Rows.Add(dr)
                Next
            End If

            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadWorkPaperNO(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select QAW_PKID,QAW_WorkPaperNo from QA_Workpaper Where QAW_CompID =" & iACID & " And QAW_AuditCode=" & iAuditID & "  Order by QAW_PKID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadWorkPaperAPMDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer,
                                           ByVal iSubFuntionID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, iRiskID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer, ByVal iWorkPaperID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select Distinct(APM_ID),APM_CustID,APM_AuditCode,APM_CustID,APM_FunctionID, APM_AuditorsRoleID,APM_AuditTeamsID,APM_AttachID,APM_APMTAStatus,"
            sSql = sSql & " ENT_ID,SEM_ID,PM_ID,SPM_ID,SPM_Name,SEM_Name,MMM_RISKID,MMM_Risk,APM_Objectives,PM_Name,MMM_FunID,MMM_SEMID,MMM_PMID,MMM_SPMID,"
            sSql = sSql & " APM_PartnersID, MMM_CONTROLID, MMM_Control, MMM_ChecksID, MMM_CHECKS,Ent_EntityName,Cust_Name,QAW_WorkPaperNo from Audit_APM_Details"
            sSql = sSql & " Left Join SAD_CUSTOMER_MASTER On Cust_Id=APM_CustID And Cust_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_APM_ChecksMatrix On APMCM_FunctionID=APM_FunctionID And APMCM_CompID=" & iACID & " And APMCM_CustID=" & iCustID & ""
            sSql = sSql & " left Join MST_Entity_Master On ENT_ID=APM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_FunID=APM_FunctionID And MMM_SEMID=APMCM_SubFunctionID And MMM_PMID=APMCM_ProcessID And"
            sSql = sSql & " MMM_SPMID=APMCM_SubProcessID And MMM_RISKID=APMCM_RiskID And MMM_CONTROLID=APMCM_ControlID And MMM_ChecksID=APMCM_ChecksID"
            sSql = sSql & " And MMM_CustID=" & iCustID & " Left Join QA_Workpaper On QAW_AuditCode = APM_ID And QAW_CompID=" & iACID & " And QAW_CustID=" & iCustID & ""
            sSql = sSql & " where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & "and APM_ID =" & iAuditID & "and APM_CustID =" & iCustID & ""
            sSql = sSql & " and APM_FunctionID =" & iFunction & " And APMCM_SubFunctionID=" & iSubFuntionID & " And APMCM_ProcessID=" & iProcessID & ""
            sSql = sSql & " And APMCM_SubProcessID=" & iSubProcessID & " And APMCM_RiskID=" & iRiskID & " And APMCM_ControlID=" & iControlID & " And APMCM_ChecksID=" & iChecksID & ""
            If iWorkPaperID > 0 Then
                sSql = sSql & " And QAW_PKID=" & iWorkPaperID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iWorkPaperID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select QAW_PKID,QAW_WorkPaperNo,QAW_TypeofTestID,QAW_ConclusionID,QAW_WorkPaperDone,QAW_AuditorObservationName,QAW_Note,QAW_AuditeeResponseName,QAW_Response,QAW_PGEDetailId,"
            sSql = sSql & " QAW_AttachID,QAW_Status, QAW_ReviewerRemarks,QAW_AuditorRemarks,QAW_OpenCloseStatus from QA_Workpaper where QAW_PKID =" & iWorkPaperID & " And QAW_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFunSunProRiskContChecksIDFromWorkPaper(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer) As DataTable
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select QAW_FunctionID,QAW_SubFunctionID,QAW_ProcessID,QAW_SubProcessID,QAW_RiskID,QAW_ControlID,QAW_ChecksID from QA_Workpaper Where QAW_CompID =" & iACID & " And QAW_PKID=" & iPKID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkpaperMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(QAW_PKID)+1 From QA_Workpaper Where QAW_CompID=" & iACID & "  And QAW_AuditCode=" & iAuditID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetWorkPaperStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iPKID As Integer, ByVal sType As String) As String
        Dim sSql As String = ""
        Try
            If sType = "Audtior" Then
                sSql = "Select QAW_AuditorRemarks from QA_Workpaper where QAW_PKID=" & iPKID & " And QAW_CompID=" & iACID & ""
            ElseIf sType = "Reviewer" Then
                sSql = "Select QAW_ReviewerRemarks from QA_Workpaper where QAW_PKID=" & iPKID & " And QAW_CompID=" & iACID & ""
            End If
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmittedWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer)
        Dim sSql As String
        Try
            sSql = "Update QA_Workpaper Set QAW_Status='Submitted',QAW_SubmittedBy=" & iUserID & ", QAW_SubmittedOn=GetDate()"
            sSql = sSql & " where QAW_YearID=" & iYearID & "And QAW_PKID =" & iWorkPaperID & " And QAW_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    'Public Sub AuditorSaveWorkPaperDetailsHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer,
    '                                                    ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sIPAddress As String)
    '    Dim sSql As String
    '    Dim iAWPHIDMaxID As Integer
    '    Dim objclsGeneralFunctions As New clsGeneralFunctions
    '    Try
    '        iAWPHIDMaxID = objclsGeneralFunctions.GetMaxID(sAC, iACID, "QA_Workpaper_History", "AWPH_PKID", "AWPH_CompID")
    '        sSql = "Insert Into QA_Workpaper_History (AWPH_PKID,AWPH_WPID,AWPH_AuditID,AWPH_CustID,AWPH_FunctionID,"
    '        sSql = sSql & " AWPH_ARCrBy,AWPH_ARCrOn,AWPH_IPAddress,AWPH_CompID)"
    '        sSql = sSql & "Values(" & iAWPHIDMaxID & "," & iWorkPaperID & "," & iAuditID & "," & iCustID & "," & iFunction & ","
    '        sSql = sSql & "" & iUserID & ",GetDate(),'" & sIPAddress & "'," & iACID & ")"
    '        objDBL.SQLExecuteNonQuery(sAC, sSql)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub
    Public Sub ReviewerSubmittedWorkPaperDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer,
                                                 ByVal iWorkPaperID As Integer, ByVal iOpenCloseStatus As Integer)
        Dim sSql As String
        Try
            sSql = "Update QA_Workpaper Set QAW_ReviewedBy=" & iUserID & ", QAW_ReviewedOn=GetDate(), QAW_OpenCloseStatus =" & iOpenCloseStatus & ""
            If iOpenCloseStatus = 1 Then
                sSql = sSql & " ,QAW_Status='Rejected' "
            ElseIf iOpenCloseStatus = 2 Then
                sSql = sSql & " ,QAW_Status='Submitted'"
            End If
            sSql = sSql & " where QAW_YearID=" & iYearID & " And QAW_PKID =" & iWorkPaperID & " And QAW_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetWorkPaperDetailsHistoryReviewerID(ByVal sAC As String, ByVal iACID As Integer, ByVal iWorkPaperID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Max(AWPH_PKID) From QA_Workpaper_History Where AWPH_WPID=" & iWorkPaperID & " And AWPH_AuditID=" & iAuditID & " And "
            sSql = sSql & " AWPH_CustID=" & iCustID & " And AWPH_FunctionID=" & iFunction & " And AWPH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub ReviewerSaveWorkPaperDetailsHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iWorkPaperID As Integer,
                                                    ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iFunction As Integer, ByVal sIPAddress As String, ByVal iAWPHIDMaxID As Integer)
        Dim sSql As String
        Try
            sSql = "Update QA_Workpaper_History Set AWPH_RRCrBy=" & iUserID & ",AWPH_RRCrOn=GetDate(),AWPH_IPAddress='" & sIPAddress & "'"
            sSql = sSql & "Where AWPH_PKID=" & iAWPHIDMaxID & " And AWPH_WPID=" & iWorkPaperID & " And AWPH_AuditID=" & iAuditID & " And "
            sSql = sSql & " AWPH_CustID=" & iCustID & " And AWPH_FunctionID=" & iFunction & " And AWPH_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
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
            sSql = sSql & "AWPH_AuditorRemarks,AWPH_RRCrBy,AWPH_RRCrOn,AWPH_ARCrBy,AWPH_ARCrOn,AWPH_IPAddress,AWPH_CompID From QA_Workpaper_History"
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
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select QAW_PGEDetailId From QA_Workpaper Where QAW_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " QAW_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " QAW_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " QAW_CustID=" & iCustID & " And QAW_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update QA_Workpaper Set QAW_AttachID=" & iAttachID & ",QAW_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " QAW_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " QAW_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " QAW_YearID=" & iYearID & " And QAW_CustID=" & iCustID & " And QAW_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetQAMaxID(ByVal sAC As String, ByVal iACID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(QA_PKID)+1 From QA_Assessment Where QA_CompID=" & iACID & " "
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveQAADetails(ByVal sAC As String, ByVal objclsQuickAudit As clsQuickAudit)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(18) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_Code", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_Code
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_FinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_CUSTID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_CUSTID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_FUNID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_FUNID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_StartDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsQuickAudit.dQA_StartDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_EndDate", OleDb.OleDbType.Date)
            ObjParam(iParamCount).Value = objclsQuickAudit.dQA_EndDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_AUDITORTEAM", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_AUDITORTEAM
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_AUDITTITLE", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_AUDITTITLE
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_Delflag", OleDb.OleDbType.VarChar, 2)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_Delflag
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_SavedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_SavedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_AUDStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_AUDStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_WPStatus", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_WPStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQA_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QA_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spQA_Assessment", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveQAChecksMatrix(ByVal sAC As String, ByVal objclsQuickAudit As clsQuickAudit)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_QAPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_QAPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQA_FinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_SubFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_SubFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_ProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_ProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_SubProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_SubProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_RiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_RiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_ControlID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_ControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_ChecksID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_MMMID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_MMMID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_Status", OleDb.OleDbType.VarChar, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQAM_Status
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_IPAddress", OleDb.OleDbType.VarChar, 20)
            ObjParam(iParamCount).Value = objclsQuickAudit.sQAM_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@QAM_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objclsQuickAudit.iQAM_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spQAA_ChecksMatrix", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
