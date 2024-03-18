Imports System
Imports System.Data
Imports System.IO
Imports System.Text
Imports System.Web

Public Structure strAuditPlanSignOff
    Private APSO_ID As Integer
    Private APSO_YearID As Integer
    Private APSO_AuditCode As Integer
    Private APSO_CustID As Integer
    Private APSO_FunctionID As Integer
    Private APSO_AuditReview As Integer
    Private APSO_AuditPlanStatus As Integer
    Private APSO_Remarks As String
    Private APSO_CrBy As Integer
    Private APSO_UpdatedBy As Integer
    Private APSO_IPAddress As String
    Private APSO_AttachID As Integer
    Private APSO_CompID As Integer
    Public Property iAPSO_ID() As Integer
        Get
            Return (APSO_ID)
        End Get
        Set(ByVal Value As Integer)
            APSO_ID = Value
        End Set
    End Property
    Public Property iAPSO_YearID() As Integer
        Get
            Return (APSO_YearID)
        End Get
        Set(ByVal Value As Integer)
            APSO_YearID = Value
        End Set
    End Property
    Public Property iAPSO_AuditCode() As Integer
        Get
            Return (APSO_AuditCode)
        End Get
        Set(ByVal Value As Integer)
            APSO_AuditCode = Value
        End Set
    End Property
    Public Property iAPSO_CustID() As Integer
        Get
            Return (APSO_CustID)
        End Get
        Set(ByVal Value As Integer)
            APSO_CustID = Value
        End Set
    End Property
    Public Property iAPSO_FunctionID() As Integer
        Get
            Return (APSO_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            APSO_FunctionID = Value
        End Set
    End Property
    Public Property iAPSO_AuditReview() As Integer
        Get
            Return (APSO_AuditReview)
        End Get
        Set(ByVal Value As Integer)
            APSO_AuditReview = Value
        End Set
    End Property
    Public Property iAPSO_AuditPlanStatus() As Integer
        Get
            Return (APSO_AuditPlanStatus)
        End Get
        Set(ByVal Value As Integer)
            APSO_AuditPlanStatus = Value
        End Set
    End Property
    Public Property sAPSO_Remarks() As String
        Get
            Return (APSO_Remarks)
        End Get
        Set(ByVal Value As String)
            APSO_Remarks = Value
        End Set
    End Property
    Public Property iAPSO_CrBy() As Integer
        Get
            Return (APSO_CrBy)
        End Get
        Set(ByVal Value As Integer)
            APSO_CrBy = Value
        End Set
    End Property
    Public Property iAPSO_UpdatedBy() As Integer
        Get
            Return (APSO_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            APSO_UpdatedBy = Value
        End Set
    End Property
    Public Property iAPSO_CompID() As Integer
        Get
            Return (APSO_CompID)
        End Get
        Set(ByVal Value As Integer)
            APSO_CompID = Value
        End Set
    End Property
    Public Property iAPSO_AttachID() As Integer
        Get
            Return (APSO_AttachID)
        End Get
        Set(ByVal Value As Integer)
            APSO_AttachID = Value
        End Set
    End Property
    Public Property sAPSO_IPAddress() As String
        Get
            Return (APSO_IPAddress)
        End Get
        Set(ByVal Value As String)
            APSO_IPAddress = Value
        End Set
    End Property
End Structure

Public Class clsAuditPlanSignOff
    Private objDBL As New DatabaseLayer.DBHelper
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAuditGeneral As New clsAuditGeneral

    Public Function LoadAuditPlanSignOffAPMDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APM_ID,APM_CustID, APM_AuditorsRoleID,APM_AuditTeamsID,APM_AttachID,APM_APMTAStatus,APM_Objectives,APM_PartnersID from  Audit_APM_Details"
            sSql = sSql & " where APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APM_ID =" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APM_FunctionID =" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APM_CustID =" & iCustID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditTeams(ByVal sAC As String, ByVal iACID As Integer, ByVal sAuditTeamsID As String) As String
        Dim sSql As String = ""
        Dim sPartners As String = ""
        Dim dt As DataTable
        Try
            If sAuditTeamsID.StartsWith(",") = True Then
                sAuditTeamsID = sAuditTeamsID.Remove(0, 1)
            End If
            If sAuditTeamsID.EndsWith(",") = True Then
                sAuditTeamsID = sAuditTeamsID.Remove(Len(sAuditTeamsID) - 1, 1)
            End If
            If sAuditTeamsID > 0 Then
                sSql = "Select Usr_FullName from Sad_Userdetails Where Usr_CompID=" & iACID & " And Usr_Id In (" & sAuditTeamsID & ") Order By Usr_FullName"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
                For i = 0 To dt.Rows.Count - 1
                    sPartners = sPartners & "," & dt.Rows(i)("Usr_FullName")
                Next
            End If
            If sPartners.StartsWith(",") Then
                sPartners = sPartners.Remove(0, 1)
            End If
            If sPartners.EndsWith(",") Then
                sPartners = sPartners.Remove(Len(sPartners) - 1, 1)
            End If
            Return sPartners
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditPlanSignOffDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select APSO_AuditReview,APSO_AuditPlanStatus,APSO_Remarks,APSO_Status,APSO_AttachID,APSO_PGEDetailId from Audit_PlanSignOff"
            sSql = sSql & " where APSO_CompID=" & iACID & " and APSO_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APSO_AuditCode =" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APSO_FunctionID =" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APSO_CustID =" & iCustID & ""
            End If
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveAuditPlanSignOff_Details(ByVal sAC As String, ByVal ObjAuditPlanSignOff As strAuditPlanSignOff)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_ID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_ID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_AuditCode", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_AuditCode
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_CustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_AuditReview", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_AuditReview
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_AuditPlanStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_AuditPlanStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_Remarks", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.sAPSO_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_CrBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_CrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("APSO_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.sAPSO_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@APSO_CompID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = ObjAuditPlanSignOff.iAPSO_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_PlanSignOff", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub SubmitPlanSignOff(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iUserID As Integer, ByVal iAuditID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_PlanSignOff set APSO_AppBy=" & iUserID & ",APSO_AppOn =Getdate(),APSO_Status='Submitted' where APSO_AuditCode=" & iAuditID & " And APSO_CustID=" & iCustID & " And APSO_FunctionID=" & iFunID & " and APSO_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadMappedRiskControlMatrixinAuditPlanSignOff(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
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
            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("MMMID") = 0 : dr("FunctionId") = 0 : dr("SubFunctionID") = 0 : dr("ProcessID") = 0 : dr("SubProcessID") = 0 : dr("RisKID") = 0 : dr("ControlID") = 0 : dr("ChecksID") = 0
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
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadMappedRiskControlMatrixToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iCustID As Integer, ByVal iUsrId As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("Checks")

            sSql = "Select APMCM_PKID,APMCM_APMPKID,APMCM_YearID,APMCM_CustID,APMCM_FunctionID,APMCM_SubFunctionID,APMCM_ProcessID,"
            sSql = sSql & " Ent_EntityName,PM_Name,SPM_Name,SEM_Name,MMM_Risk,MMM_Control,MMM_ID,MMM_CHECKS,"
            sSql = sSql & " APMCM_SubProcessID,APMCM_RiskID,APMCM_ControlID,APMCM_ChecksID,APMCM_MMMID From Audit_APM_ChecksMatrix"
            sSql = sSql & " Left Join MSt_Entity_Master On ENT_ID=APMCM_FunctionID And ENT_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=APMCM_SubFunctionID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=APMCM_ProcessID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=APMCM_SubProcessID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_MAPPING_MASTER On MMM_ID=APMCM_MMMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Where APMCM_YearID=" & iYearID & " And APMCM_FunctionID=" & iFunctionID & " And APMCM_APMPKID=" & iAuditID & " And APMCM_CustID=" & iCustID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("SubFunction") = ""
                If IsDBNull(dt.Rows(i)("SEM_Name")) = False Then
                    dr("SubFunction") = dt.Rows(i)("SEM_Name")
                End If
                dr("Process") = ""
                If IsDBNull(dt.Rows(i)("PM_Name")) = False Then
                    dr("Process") = dt.Rows(i)("PM_Name")
                End If
                dr("SubProcess") = ""
                If IsDBNull(dt.Rows(i)("SPM_Name")) = False Then
                    dr("SubProcess") = dt.Rows(i)("SPM_Name")
                End If
                dr("Checks") = ""
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = dt.Rows(i)("MMM_CHECKS")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAuditPlanSignOffDetailsToReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunction As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim drRow As DataRow
        Dim dt, dtAP As New DataTable
        Try
            dt.Columns.Add("Objectives")
            dt.Columns.Add("AuditTeam")
            dt.Columns.Add("Partners")
            dt.Columns.Add("AuditReview")
            dt.Columns.Add("AuditStatus")
            dt.Columns.Add("Remarks")

            sSql = "Select APM_ID,APSO_AuditReview,APSO_AuditPlanStatus,APSO_Remarks,APM_Objectives,APM_PartnersID,APM_AuditTeamsID from Audit_PlanSignOff"
            sSql = sSql & " Left Join Audit_APM_Details On APM_APMTAStatus='Submitted' And APM_compID=" & iACID & " and APM_YearID=" & iYearID & ""
            sSql = sSql & " And APM_CustID=APSO_CustID And APM_FunctionID=APSO_FunctionID And APM_ID=APSO_AuditCode"
            sSql = sSql & " Where APSO_CompID=" & iACID & " and APSO_YearID=" & iYearID & ""
            If iAuditID > 0 Then
                sSql = sSql & " and APSO_AuditCode =" & iAuditID & ""
            End If
            If iFunction > 0 Then
                sSql = sSql & " and APSO_FunctionID =" & iFunction & " "
            End If
            If iCustID > 0 Then
                sSql = sSql & " and APSO_CustID =" & iCustID & ""
            End If
            dtAP = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtAP.Rows.Count - 1
                drRow = dt.NewRow
                If IsDBNull(dtAP.Rows(i)("APM_Objectives")) = False Then
                    drRow("Objectives") = objclsGRACeGeneral.ReplaceSafeSQL(dtAP.Rows(i)("APM_Objectives"))
                End If
                If IsDBNull(dtAP.Rows(i)("APM_PartnersID")) = False Then
                    drRow("AuditTeam") = LoadAuditTeams(sAC, iACID, dtAP.Rows(i).Item("APM_AuditTeamsID"))
                End If
                If IsDBNull(dtAP.Rows(i)("APM_AuditTeamsID")) = False Then
                    drRow("Partners") = objclsAuditGeneral.GetPartnersAuditorsTeam(sAC, iACID, dtAP.Rows(i).Item("APM_ID"), "Partner")
                End If
                If IsDBNull(dtAP.Rows(i)("APSO_AuditReview")) = False Then
                    If dtAP.Rows(i)("APSO_AuditReview") = 1 Then
                        drRow("AuditReview") = "No"
                    Else
                        drRow("AuditReview") = "Yes"
                    End If
                End If
                If IsDBNull(dtAP.Rows(i)("APSO_AuditPlanStatus")) = False Then
                    If dtAP.Rows(i)("APSO_AuditPlanStatus") = 1 Then
                        drRow("AuditStatus") = "Approve"
                    Else
                        drRow("AuditStatus") = "Reject"
                    End If
                End If
                If IsDBNull(dtAP.Rows(i)("APSO_Remarks")) = False Then
                    drRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtAP.Rows(i)("APSO_Remarks"))
                End If
                dt.Rows.Add(drRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select APSO_PGEDetailId From Audit_PlanSignOff Where APSO_YearID=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " APSO_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " APSO_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " APSO_CustID=" & iCustID & " And APSO_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                            ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Audit_PlanSignOff Set APSO_AttachID=" & iAttachID & ",APSO_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " APSO_FunctionID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " APSO_AuditCode=" & iAuditID & " And"
            End If
            sSql = sSql & " APSO_YearID=" & iYearID & " And APSO_CustID=" & iCustID & " And APSO_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub

End Class
