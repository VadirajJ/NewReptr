Imports DatabaseLayer
Public Class clsFRRKCCReport
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsAuditGeneral As New clsAuditGeneral
    Public Function LoadActiveFRRKCCAgencyFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And Ent_DelFlg='A'"
            'If iFUNUserID > 0 Then
            '    sSql = sSql & " And (ENT_FunownerID=" & iFUNUserID & " Or Ent_FunManagerID= " & iFUNUserID & " Or Ent_FunSPOCID= " & iFUNUserID & ")"
            'End If
            'If iFRRAgencyID > 0 Then
            '    sSql = sSql & " And ENT_ID in (Select RPD_FunID from Risk_RRF_PlanningSchecduling_Details where RPD_ConductingStatus='Submitted' And RPD_YearID = " & iYearID & " And RPD_ReviewerTypeID=" & iFRRAgencyID & ")"
            'End If
            'If iKCCAgencyID > 0 Then
            '    sSql = sSql & " And ENT_ID in (Select KCC_FunID from Risk_KCC_PlanningSchecduling_Details where KCC_ConductingStatus='Submitted' And KCC_YearID = " & iYearID & " And KCC_ReviewerTypeID=" & iKCCAgencyID & ")"
            'End If
            sSql = sSql & " Order by Ent_Entityname"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllITtoFRRDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSourceofIssue As Integer, ByVal iStatus As Integer) As DataTable
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim iFRRCount As Integer = 0
        Dim sRiskType As String(), sRisk As String, sRiskName As String
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("IssueTrackerNoID")
            dt.Columns.Add("FunID")
            dt.Columns.Add("AsgID")
            dt.Columns.Add("SubFunID")
            dt.Columns.Add("Function")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("IssueHeading")
            dt.Columns.Add("IssueDetails")
            dt.Columns.Add("Risk")
            dt.Columns.Add("RiskType")
            dt.Columns.Add("Control")
            dt.Columns.Add("ActualLoss")
            dt.Columns.Add("ProbableLoss")
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("SourceofIssue")
            'dt.Columns.Add("FunctionOwner")
            'dt.Columns.Add("Owner")
            dt.Columns.Add("RiskReportNO")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("AttachID")

            If iSourceofIssue = "1" Or iSourceofIssue = "0" Then 'FRR
                sSql = "Select x.Ent_ID,x.Ent_EntityName,SEM_Name,RIT_SubFunID,RIT_Remaks,RIT_PKID,RIT_AsgNo,RIT_FunID,RIT_IssueHeading,RIT_ReferenceNo,RIT_Issue_Desc,"
                sSql = sSql & " MRL_RiskName,RAM_Name,MCL_ControlName,a.Ent_EntityName as FunctionOwner,b.Usr_FullName as EOwner,RIT_ActualLoss,RIT_ProbableLoss,RIT_ActionPlan,"
                sSql = sSql & " RIT_TargetDate,RIT_OpenCloseStatus,RIT_ManagerResponsible,RIT_IndividualResponsible,RPD_ReviewerTypeID From Risk_IssueTracker "
                sSql = sSql & " Left join MST_Entity_Master x On x.Ent_ID=RIT_FunID And x.ENT_Branch='F' And x.ENT_Delflg='A' And x.Ent_CompID=" & iACID & ""
                sSql = sSql & " Left join MST_SUBENTITY_MASTER On SEM_ID=RIT_SubFunID And SEM_CompID=" & iACID & ""
                sSql = sSql & " Left join MSt_Risk_library On MRL_PKID=RIT_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " Left join MSt_Control_library On MCL_PKID=RIT_ControlID And MCL_CompID=" & iACID & ""
                sSql = sSql & " Left join Risk_GeneralMaster On RAM_PKID=RIT_RiskTypeID And RAM_CompID=" & iACID & ""
                sSql = sSql & " Left join Risk_RRF_PlanningSchecduling_Details On RPD_CustID=" & iCustID & " And RPD_PKID=RIT_AsgNo And RPD_CompID=" & iACID & ""
                sSql = sSql & " And RPD_YearID = " & iYearID & " Left Join MST_Entity_Master a On a.Ent_ID=RIT_ManagerResponsible And a.Ent_CompID=" & iACID & ""
                sSql = sSql & " Left Join sad_userdetails b On b.Usr_ID=RIT_IndividualResponsible And b.Usr_CompID=" & iACID & " Where RIT_CustID=" & iCustID & " And"
                sSql = sSql & "  RIT_Source='Function Risk Review' And RIT_CompID=" & iACID & " And RIT_FinancialYear=" & iYearID & " And RIT_Status='Submitted'"
                If iFunID > 0 Then
                    sSql = sSql & " And x.Ent_ID=" & iFunID & ""
                End If
                If iStatus > 0 Then
                    sSql = sSql & " And RIT_OpenCloseStatus=" & iStatus & ""
                End If
                sSql = sSql & " Order by Ent_EntityName,RIT_IssueHeading"
                dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtdetails.Rows.Count > 0 Then
                    iFRRCount = dtdetails.Rows.Count
                    For i = 0 To dtdetails.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("SrNo") = i + 1
                        dRow("IssueTrackerNoID") = dtdetails.Rows(i)("RIT_PKID")
                        dRow("FunID") = dtdetails.Rows(i)("RIT_FunID")
                        dRow("AsgID") = dtdetails.Rows(i)("RIT_AsgNo")
                        dRow("SubFunID") = dtdetails.Rows(i)("RIT_SubFunID")
                        If IsDBNull(dtdetails.Rows(i)("Ent_EntityName")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("Ent_EntityName"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("SEM_Name")) = False Then
                            dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("SEM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_IssueHeading")) = False Then
                            dRow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_IssueHeading"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_Issue_Desc")) = False Then
                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_Issue_Desc"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("MRL_RiskName")) = False Then
                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("MRL_RiskName"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RAM_Name")) = False Then
                            dRow("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RAM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("MCL_ControlName")) = False Then
                            dRow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("MCL_ControlName"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ActualLoss")) = False Then
                            dRow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ActualLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ProbableLoss")) = False Then
                            dRow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ProbableLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ActionPlan")) = False Then
                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ActionPlan"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_TargetDate")) = False Then
                            If objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("RIT_TargetDate"), "D").Contains("1900") = False Then
                                dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("RIT_TargetDate"), "F")
                            End If
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_OpenCloseStatus")) = False Then
                            If dtdetails.Rows(i)("RIT_OpenCloseStatus") = 1 Then
                                dRow("Status") = "Open"
                            ElseIf dtdetails.Rows(i)("RIT_OpenCloseStatus") = 2 Then
                                dRow("Status") = "Closed"
                            End If
                        End If
                        dRow("SourceofIssue") = "Function Risk Review"
                        'dRow("FunctionOwner") = "" : dRow("Owner") = ""
                        'If IsDBNull(dtdetails.Rows(i)("RIT_ManagerResponsible")) = False Then
                        '    dRow("FunctionOwner") = dtdetails.Rows(i)("FunctionOwner")
                        'End If
                        'If IsDBNull(dtdetails.Rows(i)("RIT_IndividualResponsible")) = False Then
                        '    dRow("Owner") = dtdetails.Rows(i)("EOwner")
                        'End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ReferenceNo")) = False Then
                            dRow("RiskReportNO") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ReferenceNo"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_Remaks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_Remaks"))
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If
            If iSourceofIssue = "2" Or iSourceofIssue = "0" Then
                sSql = "Select AIT_FunctionID,z.ENT_EntityName as FunctionName,AIT_SubFunctionID,SEM_NAME,AIT_RiskID,AFW_Risk,AFW_Control,"
                sSql = sSql & " AIT_PKID,AIT_RiskTypeID,AIT_IssueJobNo,AIT_Status,AIT_AuditCodeID,AIT_IssueHeading,AIT_IssueDetails,AIT_ActionPlan,AIT_IssueRatingID,"
                sSql = sSql & " AIT_Impact,AIT_ProbableLoss, AIT_ActualLoss, b.Usr_FullName As HOD, c.usr_FullName As Manager, AIT_ResponsibleFunctionID, a.ENT_EntityName,"
                sSql = sSql & " AIT_FunctionHODID, AIT_FunctionManagerID, AIT_Impact, AIT_Remarks, AIT_IssueStatus,"
                sSql = sSql & " Convert(Varchar(10), AIT_TargetDate, 103)AIT_TargetDate,d.MIM_Name,d.MIM_Color from Audit_IssueTracker_details"
                sSql = sSql & " Left Join mst_entity_master z on z.ENT_ID=AIT_FunctionID And z.ENT_CompID=" & iACID & ""
                sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=AIT_SubFunctionID And SEM_CompID=" & iACID & ""
                sSql = sSql & " Left Join mst_entity_master a on a.ENT_ID=AIT_ResponsibleFunctionID And a.ENT_CompID=" & iACID & ""
                sSql = sSql & " Left Join sad_userdetails b On b.Usr_ID=AIT_FunctionHODID And b.Usr_CompID=" & iACID & ""
                sSql = sSql & " Left Join sad_userdetails c on c.Usr_ID=AIT_FunctionManagerID And c.Usr_CompID=" & iACID & ""
                sSql = sSql & " Left Join Audit_FieldWork On AFW_RiskID=AIT_RiskID And AFW_CompID=" & iACID & " And AFW_YearID=" & iYearID & ""
                sSql = sSql & " Left join MST_InherentRisk_Master d On d.MIM_ID=AIT_IssueRatingID and d.MIM_CompID=" & iACID & ""
                sSql = sSql & " where AIT_Status='Submitted' And AIT_CompID=" & iACID & " And AIT_YearID=" & iYearID & ""
                If iFunID > 0 Then
                    sSql = sSql & " And AIT_FunctionID=" & iFunID & ""
                End If
                If iStatus > 0 Then
                    sSql = sSql & " And AIT_IssueStatus=" & iStatus & ""
                End If
                sSql = sSql & " Order by ENT_EntityName,AIT_IssueHeading"
                dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtdetails.Rows.Count > 0 Then
                    For j = 0 To dtdetails.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("SrNo") = j + 1 + iFRRCount
                        dRow("IssueTrackerNoID") = dtdetails.Rows(j)("AIT_PKID")
                        dRow("FunID") = dtdetails.Rows(j)("AIT_FunctionID")
                        dRow("AsgID") = dtdetails.Rows(j)("AIT_AuditCodeID")
                        dRow("SubFunID") = dtdetails.Rows(j)("AIT_SubFunctionID")
                        If IsDBNull(dtdetails.Rows(j)("FunctionName")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("FunctionName"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("SEM_Name")) = False Then
                            dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("SEM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_IssueHeading")) = False Then
                            dRow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AIT_IssueHeading"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_IssueDetails")) = False Then
                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AIT_IssueDetails"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AFW_Risk")) = False Then
                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AFW_Risk"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_RiskTypeID")) = False Then
                            sRiskType = dtdetails.Rows(j)("AIT_RiskTypeID").Split(",")
                            If sRiskType.Length > 0 Then
                                sRisk = ""
                                For i = 1 To sRiskType.Length - 2
                                    sRiskName = LoadRiskNamefromID(sAC, iACID, iYearID, sRiskType(i))
                                    sRisk = sRisk & ", " & sRiskName
                                Next
                                sRisk = sRisk.Remove(0, 2)
                                dRow("RiskType") = sRisk
                            End If
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AFW_Control")) = False Then
                            dRow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AFW_Control"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_ActualLoss")) = False Then
                            dRow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AIT_ActualLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_ProbableLoss")) = False Then
                            dRow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AIT_ProbableLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_ActionPlan")) = False Then
                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AIT_ActionPlan"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_TargetDate")) = False Then
                            dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(j)("AIT_TargetDate"), "F")
                        End If
                        If IsDBNull(dtdetails.Rows(j)("AIT_IssueStatus")) = False Then
                            If dtdetails.Rows(j)("AIT_IssueStatus") = 1 Then
                                dRow("Status") = "Open"
                            ElseIf dtdetails.Rows(j)("AIT_IssueStatus") = 2 Then
                                dRow("Status") = "Closed"
                            ElseIf dtdetails.Rows(j)("AIT_IssueStatus") = 3 Then
                                dRow("Status") = "Ongoing"
                            End If
                        End If
                        dRow("SourceofIssue") = "Functional Audit"
                        'dRow("FunctionOwner") = "" : dRow("Owner") = ""
                        'If IsDBNull(dtdetails.Rows(j)("ENT_EntityName")) = False Then
                        '    dRow("FunctionOwner") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("ENT_EntityName"))
                        'End If
                        'If IsDBNull(dtdetails.Rows(j)("Manager")) = False Then
                        '    dRow("Owner") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("Manager"))
                        'End If
                        dRow("RiskReportNO") = ""
                        If IsDBNull(dtdetails.Rows(j)("AIT_Remarks")) = False Then
                            If dtdetails.Rows(j)("AIT_Remarks") <> "&nbsp: " Then
                                If dtdetails.Rows(j)("AIT_Remarks") <> "&nbsp;" Then
                                    dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("AIT_Remarks"))
                                Else
                                    dRow("Remarks") = ""
                                End If
                            End If
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllITtoFFupDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer, ByVal iSourceofIssue As Integer) As DataTable
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim iFRRCount As Integer = 0
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("IssueTrackerNoID")
            dt.Columns.Add("FunID")
            dt.Columns.Add("AsgID")
            dt.Columns.Add("SubFunID")
            dt.Columns.Add("Function")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("IssueHeading")
            dt.Columns.Add("IssueDetails")
            dt.Columns.Add("Risk")
            dt.Columns.Add("RiskType")
            dt.Columns.Add("Control")
            dt.Columns.Add("ActualLoss")
            dt.Columns.Add("ProbableLoss")
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("SourceofIssue")
            dt.Columns.Add("RiskReportNO")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("AttachID")

            If iSourceofIssue = "1" Or iSourceofIssue = "0" Then '1-FRR 3-ALL
                sSql = "Select x.Ent_ID,x.Ent_EntityName,SEM_Name,RIT_SubFunID,RIT_PKID,RIT_AsgNo,RIT_FunID,RIT_ReferenceNo,RIT_IssueHeading,RIT_Issue_Desc,MRL_RiskName,RAM_Name,"
                sSql = sSql & " MCL_ControlName,a.Ent_EntityName as FunctionOwner,b.Usr_FullName as EOwner,RIT_ActualLoss,RIT_ProbableLoss,RIT_ActionPlan,RIT_AttchID,RIT_TargetDate,"
                sSql = sSql & " RIT_OpenCloseStatus,RIT_Remaks,RIT_ManagerResponsible,RIT_IndividualResponsible,RPD_ReviewerTypeID From Risk_IssueTracker "
                sSql = sSql & " Left join MST_Entity_Master x On x.Ent_ID=RIT_FunID And x.ENT_Branch='F' And x.ENT_Delflg='A' And x.Ent_CompID=" & iACID & ""
                sSql = sSql & " Left join MST_SUBENTITY_MASTER On SEM_ID=RIT_SubFunID And SEM_CompID=" & iACID & ""
                sSql = sSql & " Left join MSt_Risk_library On MRL_PKID=RIT_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " Left join MSt_Control_library On MCL_PKID=RIT_ControlID And MCL_CompID=" & iACID & ""
                sSql = sSql & " Left join Risk_GeneralMaster On RAM_PKID=RIT_RiskTypeID And RAM_CompID=" & iACID & ""
                sSql = sSql & " Left join Risk_RRF_PlanningSchecduling_Details On RPD_CustID=" & iCustID & " And RPD_PKID=RIT_AsgNo And RPD_CompID=" & iACID & ""
                sSql = sSql & " And RPD_YearID = " & iYearID & " Left Join MST_Entity_Master a On a.Ent_ID=RIT_ManagerResponsible And a.Ent_CompID=" & iACID & ""
                sSql = sSql & " Left Join sad_userdetails b On b.Usr_ID=RIT_IndividualResponsible And b.Usr_CompID=" & iACID & " Where RIT_Source='Function Risk Review'"
                sSql = sSql & " And RIT_CustID=" & iCustID & " And RIT_OpenCloseStatus=1 and RIT_CompID=" & iACID & " And RIT_FinancialYear=" & iYearID & " And RIT_Status='Submitted'"
                If iFunID > 0 Then
                    sSql = sSql & " And x.Ent_ID = " & iFunID & ""
                End If
                sSql = sSql & " Order by Ent_EntityName,RIT_IssueHeading"
                dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtdetails.Rows.Count > 0 Then
                    iFRRCount = dtdetails.Rows.Count
                    For i = 0 To dtdetails.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("SrNo") = i + 1
                        dRow("IssueTrackerNoID") = dtdetails.Rows(i)("RIT_PKID")
                        dRow("FunID") = dtdetails.Rows(i)("RIT_FunID")
                        dRow("AsgID") = dtdetails.Rows(i)("RIT_AsgNo")
                        dRow("SubFunID") = dtdetails.Rows(i)("RIT_SubFunID")
                        If IsDBNull(dtdetails.Rows(i)("Ent_EntityName")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("Ent_EntityName"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("SEM_Name")) = False Then
                            dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("SEM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_IssueHeading")) = False Then
                            dRow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_IssueHeading"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_Issue_Desc")) = False Then
                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_Issue_Desc"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("MRL_RiskName")) = False Then
                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("MRL_RiskName"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RAM_Name")) = False Then
                            dRow("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RAM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("MCL_ControlName")) = False Then
                            dRow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("MCL_ControlName"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ActualLoss")) = False Then
                            dRow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ActualLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ProbableLoss")) = False Then
                            dRow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ProbableLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_ActionPlan")) = False Then
                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ActionPlan"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_TargetDate")) = False Then
                            If objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("RIT_TargetDate"), "D").Contains("1900") = False Then
                                dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("RIT_TargetDate"), "F")
                            End If
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_OpenCloseStatus")) = False Then
                            If dtdetails.Rows(i)("RIT_OpenCloseStatus") = 1 Then
                                dRow("Status") = "Open"
                            ElseIf dtdetails.Rows(i)("RIT_OpenCloseStatus") = 2 Then
                                dRow("Status") = "Close"
                            End If
                        End If
                        dRow("SourceofIssue") = "Function Risk Review"
                        If IsDBNull(dtdetails.Rows(i)("RIT_ReferenceNo")) = False Then
                            dRow("RiskReportNO") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_ReferenceNo"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_Remaks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(i)("RIT_Remaks"))
                        End If
                        If IsDBNull(dtdetails.Rows(i)("RIT_AttchID")) = False Then
                            dRow("AttachID") = dtdetails.Rows(i)("RIT_AttchID")
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If

            If iSourceofIssue = "2" Or iSourceofIssue = "0" Then '2 - KCC 3- All
                sSql = "Select x.Ent_ID,x.Ent_EntityName,SEM_Name,RIT_PKID,RIT_AsgNo,RIT_FunID,RIT_SubFunID,RIT_IssueHeading,RIT_ReferenceNo,RIT_Issue_Desc,MRL_RiskName,RAM_Name,"
                sSql = sSql & " MCL_ControlName, RIT_ActualLoss,RIT_ProbableLoss,RIT_ActionPlan,RIT_TargetDate,RIT_AttchID,"
                sSql = sSql & " RIT_OpenCloseStatus,RIT_Remaks,RIT_ManagerResponsible,RIT_IndividualResponsible,KCC_ReviewerTypeID From Risk_IssueTracker "
                sSql = sSql & " Left join MST_Entity_Master x On x.Ent_ID=RIT_FunID And x.ENT_Branch='F' And x.ENT_Delflg='A' And x.Ent_CompID=" & iACID & ""
                sSql = sSql & " Left join MST_SUBENTITY_MASTER On SEM_ID=RIT_SubFunID And SEM_CompID=" & iACID & ""
                sSql = sSql & " Left join MSt_Risk_library On MRL_PKID=RIT_RiskID And MRL_CompID=" & iACID & ""
                sSql = sSql & " Left join MSt_Control_library On MCL_PKID=RIT_ControlID And MCL_CompID=" & iACID & ""
                sSql = sSql & " Left join Risk_GeneralMaster On RAM_PKID=RIT_RiskTypeID And RAM_CompID=" & iACID & ""
                sSql = sSql & " Left join Risk_KCC_PlanningSchecduling_Details On KCC_CustID=" & iCustID & " And KCC_PKID=RIT_AsgNo And KCC_CompID=" & iACID & ""
                sSql = sSql & " And KCC_YearID = " & iYearID & " Left Join MST_Entity_Master a On a.Ent_ID=RIT_ManagerResponsible And a.Ent_CompID=" & iACID & ""
                sSql = sSql & " Left Join sad_userdetails b On b.Usr_ID=RIT_IndividualResponsible And b.Usr_CompID=" & iACID & ""
                sSql = sSql & " Where RIT_CustID=" & iCustID & " And RIT_Source='Key Control Checks' And RIT_OpenCloseStatus=1 And RIT_CompID=" & iACID & " And RIT_FinancialYear=" & iYearID & " "
                If iFunID > 0 Then
                    sSql = sSql & " And x.Ent_ID=" & iFunID & ""
                End If
                sSql = sSql & " Order by Ent_EntityName,RIT_IssueHeading"
                dtdetails = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtdetails.Rows.Count > 0 Then
                    For j = 0 To dtdetails.Rows.Count - 1
                        dRow = dt.NewRow
                        dRow("SrNo") = j + 1 + iFRRCount
                        dRow("IssueTrackerNoID") = dtdetails.Rows(j)("RIT_PKID")
                        dRow("FunID") = dtdetails.Rows(j)("RIT_FunID")
                        dRow("AsgID") = dtdetails.Rows(j)("RIT_AsgNo")
                        dRow("SubFunID") = dtdetails.Rows(j)("RIT_SubFunID")
                        If IsDBNull(dtdetails.Rows(j)("Ent_EntityName")) = False Then
                            dRow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("Ent_EntityName"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("SEM_Name")) = False Then
                            dRow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("SEM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_IssueHeading")) = False Then
                            dRow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_IssueHeading"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_Issue_Desc")) = False Then
                            dRow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_Issue_Desc"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("MRL_RiskName")) = False Then
                            dRow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("MRL_RiskName"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RAM_Name")) = False Then
                            dRow("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RAM_Name"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("MCL_ControlName")) = False Then
                            dRow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("MCL_ControlName"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_ActualLoss")) = False Then
                            dRow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_ActualLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_ProbableLoss")) = False Then
                            dRow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_ProbableLoss"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_ActionPlan")) = False Then
                            dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_ActionPlan"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_TargetDate")) = False Then
                            If objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(j)("RIT_TargetDate"), "D").Contains("1900") = False Then
                                dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(j)("RIT_TargetDate"), "F")
                            End If
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_OpenCloseStatus")) = False Then
                            If dtdetails.Rows(j)("RIT_OpenCloseStatus") = 1 Then
                                dRow("Status") = "Open"
                            ElseIf dtdetails.Rows(j)("RIT_OpenCloseStatus") = 2 Then
                                dRow("Status") = "Close"
                            End If
                        End If
                        dRow("SourceofIssue") = "Key Control Checks"
                        'dRow("FunctionOwner") = "" : dRow("Owner") = ""
                        'If IsDBNull(dtdetails.Rows(j)("RIT_ManagerResponsible")) = False Then
                        '    dRow("FunctionOwner") = dtdetails.Rows(j)("FunctionOwner")
                        'End If
                        'If IsDBNull(dtdetails.Rows(j)("RIT_IndividualResponsible")) = False Then
                        '    dRow("Owner") = dtdetails.Rows(j)("EOwner")
                        'End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_ReferenceNo")) = False Then
                            dRow("RiskReportNO") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_ReferenceNo"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_Remaks")) = False Then
                            dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtdetails.Rows(j)("RIT_Remaks"))
                        End If
                        If IsDBNull(dtdetails.Rows(j)("RIT_AttchID")) = False Then
                            dRow("AttachID") = dtdetails.Rows(j)("RIT_AttchID")
                        End If
                        dt.Rows.Add(dRow)
                    Next
                End If
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRiskNamefromID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iPKID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select RAM_Name from Risk_generalMaster Where RAM_YearID=" & iYearID & " And  RAM_Category='RT' And RAM_DelFlag='A' And RAM_CompID=" & iACID & " And RAM_PKID=" & iPKID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
