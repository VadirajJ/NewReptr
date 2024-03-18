Imports DatabaseLayer
Public Structure strRisk_IT
    Dim iRIT_PKID As Integer
    Dim sRIT_IssueNo As String
    Dim iRIT_AsgNo As Integer
    Dim sRIT_RReferenceNo As String
    Dim iRIT_SubFunID As String
    Dim iRIT_FinancialYear As Integer
    Dim iRIT_CustID As Integer
    Dim iRIT_FunID As Integer
    Dim sRIT_IssueHeading As String
    Dim sRIT_Issue_Desc As String
    Dim iRIT_RiskID As Integer
    Dim iRIT_RiskTypeID As Integer
    Dim iRIT_ControlID As Integer
    Dim sRIT_ActualLoss As String
    Dim sRIT_ProbableLoss As String
    Dim sRIT_ActionPlan As String
    Dim dRIT_TargetDate As Date
    Dim iRIT_OpenCloseStatus As Integer
    Dim sRIT_Remaks As String
    Dim iRIT_ManagerResponsibleID As Integer
    Dim iRIT_IndividualResponsibleID As Integer
    Dim iRIT_AttchID As Integer
    Dim iRIT_CrBy As Integer
    Dim iRIT_UpdatedBy As Integer
    Dim sRIT_Status As String
    Dim sRIT_IPAddress As String
    Dim iRIT_CompID As Integer
    Dim iRIHT_PKID As Integer
    Public Property iRIHTPKID() As Integer
        Get
            Return (iRIHT_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRIHT_PKID = Value
        End Set
    End Property
    Public Property iRFSTPKID() As Integer
        Get
            Return (iRIT_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_PKID = Value
        End Set
    End Property
    Public Property sRFSTIssueNo() As String
        Get
            Return (sRIT_IssueNo)
        End Get
        Set(ByVal Value As String)
            sRIT_IssueNo = Value
        End Set
    End Property
    Public Property iRFSTSubFunID() As Integer
        Get
            Return (iRIT_SubFunID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_SubFunID = Value
        End Set
    End Property

    Public Property sRFSTRReferenceNo() As String
        Get
            Return (sRIT_RReferenceNo)
        End Get
        Set(ByVal Value As String)
            sRIT_RReferenceNo = Value
        End Set
    End Property
    Public Property iRFSTAsgNo() As Integer
        Get
            Return (iRIT_AsgNo)
        End Get
        Set(ByVal Value As Integer)
            iRIT_AsgNo = Value
        End Set
    End Property
    Public Property iRFSTFinancialYear() As Integer
        Get
            Return (iRIT_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iRIT_FinancialYear = Value
        End Set
    End Property
    Public Property iRFSTCustID() As Integer
        Get
            Return (iRIT_CustID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_CustID = Value
        End Set
    End Property
    Public Property iRFSTFunID() As Integer
        Get
            Return (iRIT_FunID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_FunID = Value
        End Set
    End Property
    Public Property sRFSTIssueHeading() As String
        Get
            Return (sRIT_IssueHeading)
        End Get
        Set(ByVal Value As String)
            sRIT_IssueHeading = Value
        End Set
    End Property
    Public Property sRFSTIssueDesc() As String
        Get
            Return (sRIT_Issue_Desc)
        End Get
        Set(ByVal Value As String)
            sRIT_Issue_Desc = Value
        End Set
    End Property
    Public Property iRFSTRiskID() As Integer
        Get
            Return (iRIT_RiskID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_RiskID = Value
        End Set
    End Property
    Public Property iRFSTRiskTypeID() As Integer
        Get
            Return (iRIT_RiskTypeID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_RiskTypeID = Value
        End Set
    End Property
    Public Property iRFSTControlID() As Integer
        Get
            Return (iRIT_ControlID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_ControlID = Value
        End Set
    End Property
    Public Property sRFSTActualLoss() As String
        Get
            Return (sRIT_ActualLoss)
        End Get
        Set(ByVal Value As String)
            sRIT_ActualLoss = Value
        End Set
    End Property
    Public Property sRFSTProbableLoss() As String
        Get
            Return (sRIT_ProbableLoss)
        End Get
        Set(ByVal Value As String)
            sRIT_ProbableLoss = Value
        End Set
    End Property
    Public Property sRFSTActionPlan() As String
        Get
            Return (sRIT_ActionPlan)
        End Get
        Set(ByVal Value As String)
            sRIT_ActionPlan = Value
        End Set
    End Property
    Public Property dRFSTTargetDate() As Date
        Get
            Return (dRIT_TargetDate)
        End Get
        Set(ByVal Value As Date)
            dRIT_TargetDate = Value
        End Set
    End Property
    Public Property iRFSTOpenCloseStatus() As Integer
        Get
            Return (iRIT_OpenCloseStatus)
        End Get
        Set(ByVal Value As Integer)
            iRIT_OpenCloseStatus = Value
        End Set
    End Property
    Public Property iRITManagerResponsibleID() As Integer
        Get
            Return (iRIT_ManagerResponsibleID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_ManagerResponsibleID = Value
        End Set
    End Property
    Public Property iRITIndividualResponsibleID() As Integer
        Get
            Return (iRIT_IndividualResponsibleID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_IndividualResponsibleID = Value
        End Set
    End Property
    Public Property sRFSTRemaks() As String
        Get
            Return (sRIT_Remaks)
        End Get
        Set(ByVal Value As String)
            sRIT_Remaks = Value
        End Set
    End Property
    Public Property iRFSTAttchID() As Integer
        Get
            Return (iRIT_AttchID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_AttchID = Value
        End Set
    End Property
    Public Property iRFSTCrBy() As Integer
        Get
            Return (iRIT_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iRIT_CrBy = Value
        End Set
    End Property
    Public Property iRFSTUpdatedBy() As Integer
        Get
            Return (iRIT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iRIT_UpdatedBy = Value
        End Set
    End Property
    Public Property sRFSTStatus() As String
        Get
            Return (sRIT_Status)
        End Get
        Set(ByVal Value As String)
            sRIT_Status = Value
        End Set
    End Property
    Public Property sRFSTIPAddress() As String
        Get
            Return (sRIT_IPAddress)
        End Get
        Set(ByVal Value As String)
            sRIT_IPAddress = Value
        End Set
    End Property
    Public Property iRFSTCompID() As Integer
        Get
            Return (iRIT_CompID)
        End Get
        Set(ByVal Value As Integer)
            iRIT_CompID = Value
        End Set
    End Property
End Structure
Public Class clsRiskIssueTracker
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadAllFRRKCCIssueTrackerNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer, ByVal iCustID As Integer, ByVal iSubFunID As Integer, ByVal sSourceName As String) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RIT_PKID,RIT_IssueNo From Risk_IssueTracker Where RIT_CustID=" & iCustID & " And RIT_Source='" & sSourceName & "' And RIT_AsgNo=" & iPKID & ""
            sSql = sSql & " And RIT_FinancialYear=" & iYearID & " And RIT_SubFunID =" & iSubFunID & " And RIT_CompID=" & iACID & " Order by RIT_PKID Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFRRFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And ENT_ID in "
            sSql = sSql & " (Select RPD_FunID from Risk_RRF_PlanningSchecduling_Details where RPD_Status='Submitted' And RPD_YearID = " & iYearID & " And RPD_CompID = " & iACID & ")"
            sSql = sSql & " Order by Ent_Entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFRRSubFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_Name from MST_SUBENTITY_MASTER Where SEM_compid=" & iACID & " "
            If iFunID > 0 Then
                sSql = sSql & " And SEM_ID In (Select RPD_SubFunID from Risk_RRF_PlanningSchecduling_Details where RPD_Status='Submitted' And RPD_FunID =" & iFunID & "  And RPD_YearID=" & iYearID & " And RPD_CompID=" & iACID & ")"
            End If
            sSql = sSql & " Order by SEM_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllFRRNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal sStatus As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select RPD_AsgNo,RPD_PKID From Risk_RRF_PlanningSchecduling_Details where RPD_YearID=" & iYearID & " And RPD_CompID=" & iACID & " And RPD_CustID=" & iCustID & ""
            If iFunID > 0 Then
                sSql = sSql & " And RPD_FunID=" & iFunID & ""
            End If
            If sStatus <> "" Then
                sSql = sSql & " And RPD_Status='" & sStatus & "'"
            End If
            sSql = sSql & " order by RPD_PKID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateFRRRiskReportRefNo(ByVal sAC As String, ByVal iACID As Integer, ByVal sRefNO As String, ByVal iPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_RRF_PlanningSchecduling_Details set  RPD_RefNO='" & sRefNO & "' Where RPD_PKID=" & iPKID & " and RPD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetRiskIssueTrackerSelectedStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer, ByVal iITID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select RIT_Status From Risk_IssueTracker Where RIT_CustID=" & iCustID & " And RIT_AsgNO=" & iAuditID & " And RIT_PKID=" & iITID & " And RIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllIssueTrackerGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iRRFPKID As Integer, ByVal sSourceName As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("IssueTrackerNoID")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("IssueTrackerNo")
            dt.Columns.Add("IssueHeading")
            dt.Columns.Add("IssueDescription")
            dt.Columns.Add("RiskDetails")
            dt.Columns.Add("RiskType")
            dt.Columns.Add("Control")
            dt.Columns.Add("ActualLoss")
            dt.Columns.Add("ProbableLoss")
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("OpenCloseStatus")
            dt.Columns.Add("Status")
            dt.Columns.Add("Remaks")

            sSql = "Select RIT_PKID,RIT_IssueNo,RIT_IssueHeading,RIT_Issue_Desc,MRL_RiskName,RAM_Name,MCL_ControlName,RIT_ActualLoss,RIT_ActionPlan,RIT_ProbableLoss,RIT_Status,"
            sSql = sSql & " RIT_Remaks,SEM_Name,RIT_TargetDate,Convert(Varchar(10),RIT_TargetDate,103)TargetDate,RIT_OpenCloseStatus from Risk_IssueTracker"
            sSql = sSql & " Left join MST_Entity_Master On Ent_ID=RIT_FunID And ENT_Branch='F' And Ent_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_SUBENTITY_MASTER on SEM_ID=RIT_SubFunID And SEM_compid=" & iACID & ""
            sSql = sSql & " Left join MSt_Risk_library On MRL_PKID=RIT_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left join MSt_Control_library On MCL_PKID=RIT_ControlID And MCL_CompID=" & iACID & ""
            sSql = sSql & " Left join Risk_GeneralMaster On RAM_PKID=RIT_RiskTypeID And RAM_CompID=" & iACID & ""
            sSql = sSql & "  Where RIT_Source='" & sSourceName & "' And RIT_CompID=" & iACID & " And RIT_AsgNo=" & iRRFPKID & " order by RIT_IssueNo"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("IssueTrackerNoID") = dtDetails.Rows(i)("RIT_PKID")
                    If IsDBNull(dtDetails.Rows(i)("RIT_IssueNo")) = False Then
                        dRow("IssueTrackerNo") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_IssueNo"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_IssueHeading")) = False Then
                        dRow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_IssueHeading"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_Issue_Desc")) = False Then
                        dRow("IssueDescription") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_Issue_Desc"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("MRL_RiskName")) = False Then
                        dRow("RiskDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("MRL_RiskName"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RAM_Name")) = False Then
                        dRow("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RAM_Name"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("MCL_ControlName")) = False Then
                        dRow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("MCL_ControlName"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_ActualLoss")) = False Then
                        dRow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_ActualLoss"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_ActionPlan")) = False Then
                        dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_ActionPlan"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_ProbableLoss")) = False Then
                        dRow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_ProbableLoss"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_Remaks")) = False Then
                        dRow("Remaks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RIT_Remaks"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("SEM_Name")) = False Then
                        dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("SEM_Name"))
                    End If
                    If IsDBNull(dtDetails.Rows(i)("TargetDate")) = False Then
                        If objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("TargetDate"), "D").Contains("1900") = False Then
                            dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtDetails.Rows(i)("RIT_TargetDate"), "F")
                        End If
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RIT_OpenCloseStatus")) = False Then
                        If dtDetails.Rows(i)("RIT_OpenCloseStatus") = 1 Then
                            dRow("OpenCloseStatus") = "Open"
                        ElseIf dtDetails.Rows(i)("RIT_OpenCloseStatus") = 2 Then
                            dRow("OpenCloseStatus") = "Closed"
                        End If
                    End If
                    dRow("Status") = dtDetails.Rows(i)("RIT_Status")
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllKCCNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunID As Integer, ByVal sStatus As String, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable
        Try
            sSql = "Select KCC_AsgNo,KCC_PKID From Risk_KCC_PlanningSchecduling_Details where KCC_YearID=" & iYearID & " And KCC_CompID=" & iACID & ""
            If iCustID > 0 Then
                sSql = sSql & " And KCC_CustID=" & iCustID & ""
            End If
            If iFunID > 0 Then
                sSql = sSql & " And KCC_FunID=" & iFunID & ""
            End If
            If sStatus <> "" Then
                sSql = sSql & " And KCC_Status='" & sStatus & "'"
            End If
            sSql = sSql & " order by KCC_PKID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllKCCFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And ENT_ID in "
            sSql = sSql & " (Select KCC_FunID from Risk_KCC_PlanningSchecduling_Details where KCC_Status='Submitted' And KCC_YearID = " & iYearID & ""
            sSql = sSql & " And KCC_CompID = " & iACID & ")"
            sSql = sSql & " Order by Ent_Entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadKCCSubFunctions(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iSubFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_Name from MST_SUBENTITY_MASTER Where SEM_compid=" & iACID & " "
            If iSubFunID > 0 Then
                sSql = sSql & " And SEM_ID In (Select KCC_SubFunID from Risk_KCC_PlanningSchecduling_Details where KCC_Status='Submitted' And KCC_FunID =" & iSubFunID & "  And KCC_YearID=" & iYearID & " And KCC_CompID=" & iACID & ")"
            End If
            sSql = sSql & " Order by SEM_Name"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateKCCRiskReportRefNo(ByVal sAC As String, ByVal iACID As Integer, ByVal sRefNO As String, ByVal iPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_KCC_PlanningSchecduling_Details set  KCC_RiskReportReferenceNo='" & sRefNO & "' Where KCC_PKID=" & iPKID & " and KCC_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckFRRKCCIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAsgNo As Integer, ByVal sName As String, ByVal iPKID As Integer, ByVal sForm As String, ByVal iCustID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select RIT_PKID From Risk_IssueTracker Where RIT_AsgNo=" & iAsgNo & " And RIT_IssueHeading='" & sName & "' And RIT_FinancialYear=" & iYearID & " and RIT_CompID=" & iACID & " And RIT_Source='" & sForm & "' And RIT_CustID=" & iCustID & ""
            If iPKID > 0 Then
                sSql = sSql & " And RIT_PKID <>" & iPKID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveRiskIssueTracker(ByVal sAC As String, ByVal objstrRiskIT As strRisk_IT, ByVal sSourceName As String) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(28) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_Source", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = sSourceName
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_IssueNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTIssueNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_AsgNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTAsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ReferenceNo", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTRReferenceNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTFinancialYear
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_CustID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTCustID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_FunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_SubFunID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTSubFunID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_IssueHeading", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTIssueHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_Issue_Desc", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTIssueDesc
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_RiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTRiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_RiskTypeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTRiskTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ControlID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ActualLoss", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTActualLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ProbableLoss", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTProbableLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ActionPlan", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrRiskIT.dRFSTTargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_OpenCloseStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTOpenCloseStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ManagerResponsible", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_IndividualResponsible", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = 0
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_Remaks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTRemaks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_AttchID", OleDb.OleDbType.Integer, 1)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTAttchID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_IssueTracker", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskITMaxID(ByVal sAC As String, ByVal iPKID As Integer, ByVal sSourceName As String) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*)+1 from Risk_IssueTracker where RIT_AsgNo=" & iPKID & " And RIT_Source='" & sSourceName & "'"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRiskITHistoryMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal iAssignmentID As Integer, ByVal iIssueTrackerID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Max(RITH_PKID) From Risk_IssueTracker_History Where RITH_RITPKID=" & iIssueTrackerID & " And RITH_AsgNo=" & iAssignmentID & " And RITH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFRRKCCActionPlanGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iRRFPKID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Try
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("ResponsibleFunction")
            dt.Columns.Add("OwnerName")
            dt.Columns.Add("Remarks")

            sSql = "Select RITH_PKID,RITH_ActionPlan,RITH_OpenCloseStatus,RITH_Remaks,Convert(Varchar(10),RITH_TargetDate,103)RITH_TargetDate,RITH_ManagerResponsible,"
            sSql = sSql & " RITH_IndividualResponsible,ENT_ENTITYName,usr_FullName From Risk_IssueTracker_History"
            sSql = sSql & " Left Join MST_Entity_master On RITH_ManagerResponsible=ENT_ID And ENT_CompId=" & iACID & " And ENT_Branch='F'"
            sSql = sSql & " Left Join Sad_UserDetails On RITH_IndividualResponsible=usr_Id And usr_CompId=" & iACID & ""
            sSql = sSql & " Where RITH_CompID = " & iACID & " And RITH_RITPKID = " & iRRFPKID & " Order By RITH_PKID Desc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RITH_ActionPlan"))
                    dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("RITH_Remaks"))
                    dRow("TargetDate") = dtDetails.Rows(i)("RITH_TargetDate")
                    If dtDetails.Rows(i)("RITH_OpenCloseStatus") = 1 Then
                        dRow("Status") = "Open"
                    ElseIf dtDetails.Rows(i)("RITH_OpenCloseStatus") = 2 Then
                        dRow("Status") = "Closed"
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RITH_IndividualResponsible")) = False Then
                        dRow("ResponsibleFunction") = dtDetails.Rows(i)("ENT_ENTITYName")
                    End If
                    If IsDBNull(dtDetails.Rows(i)("RITH_ManagerResponsible")) = False Then
                        dRow("OwnerName") = dtDetails.Rows(i)("usr_FullName")
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitRiskITDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iPKID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_IssueTracker Set RIT_Status='Submitted',RIT_SubmittedBy=" & iUserID & ",RIT_SubmittedOn=GetDate() Where RIT_PKID=" & iPKID & " And RIT_FinancialYear=" & iYearID & " And RIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveRiskIssueTrackerHistory(ByVal sAC As String, ByVal objstrRiskIT As strRisk_IT, ByVal sSourceName As String, ByVal iRITPKID As Integer) As String()
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(14) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RITH_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRIHTPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RITH_RITPKI", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iRITPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RITH_AsgNo", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTAsgNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_ActionPlan", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objstrRiskIT.dRFSTTargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_OpenCloseStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTOpenCloseStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RITH_ManagerResponsible", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRITManagerResponsibleID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RITH_IndividualResponsible", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objstrRiskIT.iRITIndividualResponsibleID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_Remaks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTRemaks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_CrBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTCrBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTUpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objstrRiskIT.sRFSTIPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@RIT_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objstrRiskIT.iRFSTCompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_IssueTracker_History", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRiskITSelectedData(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iITPkID As Integer, ByVal iPKID As Integer, ByVal iSubFunID As Integer, ByVal sSourceName As String) As DataTable
        Dim sSql As String = ""
        Try
            sSql = "Select RIT_PKID,RIT_IssueNo,RIT_IssueHeading,RIT_Issue_Desc,RIT_RiskID,RIT_RiskTypeID,RIT_RiskID,RIT_ControlID,RIT_ActualLoss,RIT_ProbableLoss,RIT_PGEDetailId,"
            sSql = sSql & " RIT_AttchID,RIT_ActionPlan,RIT_ManagerResponsible,RIT_IndividualResponsible,RIT_TargetDate,RIT_Remaks,RIT_OpenCloseStatus,RIT_Status,"
            sSql = sSql & " MCL_ControlName,MRL_RiskName from Risk_IssueTracker Left Join MST_Control_Library On RIT_ControlID=MCL_PKID And MCl_CompID=" & iACID & ""
            sSql = sSql & " Left Join MSt_Risk_library On RIT_RiskID=MRL_PKID And MRL_CompID=" & iACID & " Where RIT_CompID=" & iACID & " And RIT_PKID= " & iITPkID & ""
            sSql = sSql & " And RIT_AsgNo=" & iPKID & " And RIT_FinancialYear=" & iYearID & " And RIT_SubFunID=" & iSubFunID & " And RIT_Source='" & sSourceName & "'"
            sSql = sSql & " And RIT_CustID=" & iCustID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iFunctionID As Integer, ByVal iAuditID As Integer, ByVal iIssueID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RIT_PGEDetailId From Risk_IssueTracker Where RIT_FinancialYear=" & iYearID & " And"
            If iFunctionID > 0 Then
                sSql = sSql & " RIT_FunID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " RIT_AsgNo=" & iAuditID & " And"
            End If
            If iIssueID > 0 Then
                sSql = sSql & " RIT_PKID=" & iIssueID & " And"
            End If
            sSql = sSql & " RIT_CustID=" & iCustID & " And RIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer, ByVal iIssueID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_IssueTracker Set RIT_AttchID=" & iAttachID & ",RIT_PGEDetailId=" & iPGDetailID & " Where "
            If iFunctionID > 0 Then
                sSql = sSql & " RIT_FunID=" & iFunctionID & " And"
            End If
            If iAuditID > 0 Then
                sSql = sSql & " RIT_AsgNo=" & iAuditID & " And"
            End If
            If iIssueID > 0 Then
                sSql = sSql & " RIT_PKID=" & iIssueID & " And"
            End If
            sSql = sSql & " RIT_FinancialYear=" & iYearID & " And RIT_CustID=" & iCustID & " And RIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
