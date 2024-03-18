Imports DatabaseLayer
Public Structure strC_IssueTracker
    Private CIT_PKID As Integer
    Private CITH_PKID As Integer
    Private CIT_YearID As Integer
    Private CIT_CustomerID As Integer
    Private CIT_ComplianceCodeID As Integer
    Private CIT_ChecklistID As Integer
    Private CIT_IssueJobNo As String
    Private CIT_FunctionID As Integer
    Private CIT_SubFunctionID As Integer
    Private CIT_ProcessID As Integer
    Private CIT_SubProcessID As Integer
    Private CIT_RiskID As Integer
    Private CIT_RiskTypeID As String
    Private CIT_ControlID As Integer
    Private CIT_CheckID As Integer
    Private CIT_IssueHeading As String
    Private CIT_IssueDetails As String
    Private CIT_Impact As String
    Private CIT_ActionPlan As String
    Private CIT_IssueRatingID As Integer
    Private CIT_ActualLoss As String
    Private CIT_ProbableLoss As String
    Private CIT_TargetDate As Date
    Private CIT_ResponsibleFunctionID As Integer
    Private CIT_FunctionManagerID As Integer
    Private CIT_Remarks As String
    Private CIT_IssueStatus As Integer
    Private CIT_CreatedBy As Integer
    Private CIT_CreatedOn As Date
    Private CIT_UpdatedBy As Integer
    Private CIT_UpdatedOn As Date
    Private CIT_SubmittedBy As Integer
    Private CIT_SubmittedOn As Date
    Private CIT_Status As String
    Private CIT_CompID As Integer
    Private CIT_IPAddress As String
    Private CIT_AttachID As Integer
    Public Property iCIT_PKID() As Integer
        Get
            Return (CIT_PKID)
        End Get
        Set(ByVal Value As Integer)
            CIT_PKID = Value
        End Set
    End Property
    Public Property iCIT_AttachID() As Integer
        Get
            Return (CIT_AttachID)
        End Get
        Set(ByVal Value As Integer)
            CIT_AttachID = Value
        End Set
    End Property
    Public Property iCITH_PKID() As Integer
        Get
            Return (CITH_PKID)
        End Get
        Set(ByVal Value As Integer)
            CITH_PKID = Value
        End Set
    End Property
    Public Property iCIT_YearID() As Integer
        Get
            Return (CIT_YearID)
        End Get
        Set(ByVal Value As Integer)
            CIT_YearID = Value
        End Set
    End Property
    Public Property iCIT_CustomerID() As Integer
        Get
            Return (CIT_CustomerID)
        End Get
        Set(ByVal Value As Integer)
            CIT_CustomerID = Value
        End Set
    End Property
    Public Property iCIT_ComplianceCodeID() As Integer
        Get
            Return (CIT_ComplianceCodeID)
        End Get
        Set(ByVal Value As Integer)
            CIT_ComplianceCodeID = Value
        End Set
    End Property
    Public Property iCIT_ChecklistID() As Integer
        Get
            Return (CIT_ChecklistID)
        End Get
        Set(ByVal Value As Integer)
            CIT_ChecklistID = Value
        End Set
    End Property
    Public Property sCIT_IssueJobNo() As String
        Get
            Return (CIT_IssueJobNo)
        End Get
        Set(ByVal Value As String)
            CIT_IssueJobNo = Value
        End Set
    End Property
    Public Property iCIT_FunctionID() As Integer
        Get
            Return (CIT_FunctionID)
        End Get
        Set(ByVal Value As Integer)
            CIT_FunctionID = Value
        End Set
    End Property
    Public Property iCIT_SubFunctionID() As Integer
        Get
            Return (CIT_SubFunctionID)
        End Get
        Set(ByVal Value As Integer)
            CIT_SubFunctionID = Value
        End Set
    End Property
    Public Property iCIT_ProcessID() As Integer
        Get
            Return (CIT_ProcessID)
        End Get
        Set(ByVal Value As Integer)
            CIT_ProcessID = Value
        End Set
    End Property
    Public Property iCIT_SubProcessID() As Integer
        Get
            Return (CIT_SubProcessID)
        End Get
        Set(ByVal Value As Integer)
            CIT_SubProcessID = Value
        End Set
    End Property
    Public Property iCIT_RiskID() As Integer
        Get
            Return (CIT_RiskID)
        End Get
        Set(ByVal Value As Integer)
            CIT_RiskID = Value
        End Set
    End Property
    Public Property sCIT_RiskTypeID() As String
        Get
            Return (CIT_RiskTypeID)
        End Get
        Set(ByVal Value As String)
            CIT_RiskTypeID = Value
        End Set
    End Property

    Public Property iCIT_ControlID() As Integer
        Get
            Return (CIT_ControlID)
        End Get
        Set(ByVal Value As Integer)
            CIT_ControlID = Value
        End Set
    End Property
    Public Property iCIT_CheckID() As Integer
        Get
            Return (CIT_CheckID)
        End Get
        Set(ByVal Value As Integer)
            CIT_CheckID = Value
        End Set
    End Property
    Public Property sCIT_IssueHeading() As String
        Get
            Return (CIT_IssueHeading)
        End Get
        Set(ByVal Value As String)
            CIT_IssueHeading = Value
        End Set
    End Property
    Public Property sCIT_IssueDetails() As String
        Get
            Return (CIT_IssueDetails)
        End Get
        Set(ByVal Value As String)
            CIT_IssueDetails = Value
        End Set
    End Property
    Public Property sCIT_Impact() As String
        Get
            Return (CIT_Impact)
        End Get
        Set(ByVal Value As String)
            CIT_Impact = Value
        End Set
    End Property
    Public Property sCIT_ActionPlan() As String
        Get
            Return (CIT_ActionPlan)
        End Get
        Set(ByVal Value As String)
            CIT_ActionPlan = Value
        End Set
    End Property
    Public Property iCIT_IssueRatingID() As Integer
        Get
            Return (CIT_IssueRatingID)
        End Get
        Set(ByVal Value As Integer)
            CIT_IssueRatingID = Value
        End Set
    End Property
    Public Property sCIT_ActualLoss() As String
        Get
            Return (CIT_ActualLoss)
        End Get
        Set(ByVal Value As String)
            CIT_ActualLoss = Value
        End Set
    End Property
    Public Property sCIT_ProbableLoss() As String
        Get
            Return (CIT_ProbableLoss)
        End Get
        Set(ByVal Value As String)
            CIT_ProbableLoss = Value
        End Set
    End Property
    Public Property dCIT_TargetDate() As Date
        Get
            Return (CIT_TargetDate)
        End Get
        Set(ByVal Value As Date)
            CIT_TargetDate = Value
        End Set
    End Property
    Public Property iCIT_ResponsibleFunctionID() As Integer
        Get
            Return (CIT_ResponsibleFunctionID)
        End Get
        Set(ByVal Value As Integer)
            CIT_ResponsibleFunctionID = Value
        End Set
    End Property
    Public Property iCIT_FunctionManagerID() As Integer
        Get
            Return (CIT_FunctionManagerID)
        End Get
        Set(ByVal Value As Integer)
            CIT_FunctionManagerID = Value
        End Set
    End Property
    Public Property sCIT_Remarks() As String
        Get
            Return (CIT_Remarks)
        End Get
        Set(ByVal Value As String)
            CIT_Remarks = Value
        End Set
    End Property
    Public Property iCIT_IssueStatus() As Integer
        Get
            Return (CIT_IssueStatus)
        End Get
        Set(ByVal Value As Integer)
            CIT_IssueStatus = Value
        End Set
    End Property
    Public Property iCIT_CreatedBy() As Integer
        Get
            Return (CIT_CreatedBy)
        End Get
        Set(ByVal Value As Integer)
            CIT_CreatedBy = Value
        End Set
    End Property
    Public Property dCIT_CreatedOn() As Date
        Get
            Return (CIT_CreatedOn)
        End Get
        Set(ByVal Value As Date)
            CIT_CreatedOn = Value
        End Set
    End Property
    Public Property iCIT_UpdatedBy() As Integer
        Get
            Return (CIT_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            CIT_UpdatedBy = Value
        End Set
    End Property
    Public Property dCIT_UpdatedOn() As Date
        Get
            Return (CIT_UpdatedOn)
        End Get
        Set(ByVal Value As Date)
            CIT_UpdatedOn = Value
        End Set
    End Property
    Public Property iCIT_SubmittedBy() As Integer
        Get
            Return (CIT_SubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            CIT_SubmittedBy = Value
        End Set
    End Property
    Public Property dCIT_SubmittedOn() As Date
        Get
            Return (CIT_SubmittedOn)
        End Get
        Set(ByVal Value As Date)
            CIT_SubmittedOn = Value
        End Set
    End Property
    Public Property sCIT_Status() As String
        Get
            Return (CIT_Status)
        End Get
        Set(ByVal Value As String)
            CIT_Status = Value
        End Set
    End Property
    Public Property iCIT_CompID() As Integer
        Get
            Return (CIT_CompID)
        End Get
        Set(ByVal Value As Integer)
            CIT_CompID = Value
        End Set
    End Property
    Public Property sCIT_IPAddress() As String
        Get
            Return (CIT_IPAddress)
        End Get
        Set(ByVal Value As String)
            CIT_IPAddress = Value
        End Set
    End Property
End Structure
Public Class clsCRIssueTracker
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsComplianceGeneral As New clsComplianceGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function LoadComplianceCodeFromChecklist(ByVal sAC As String, ByVal iACID As Integer, ByVal iFUNID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CP_ID,CP_ComplianceCode From Compliance_Plan Where  CP_CompID=" & iACID & " and  CP_YearID=" & iYearID & " and CP_ID in"
            sSql = sSql & " (Select CRCM_JobID from Compliance_Checklist_Mas where CRCM_CustID=" & iCustID & " And CRCM_Status ='Submitted' and CRCM_YearID=" & iYearID & ""
            sSql = sSql & " And CRCM_CompID = " & iACID & ""
            If iFUNID > 0 Then
                sSql = sSql & " and CP_functionID=" & iFUNID & ""
            End If
            sSql = sSql & " )"
            sSql = sSql & " Order by CP_ID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFunctionFromChecklist(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select ENT_ID,ENT_ENTITYName from MST_Entity_master where ENT_Branch='F' And ENT_compid=" & iACID & " And ENT_ID in (Select CRCM_FunID from"
            sSql = sSql & " Compliance_Checklist_Mas where CRCM_CustID=" & iCustID & " And CRCM_Status ='Submitted' and CRCM_YearID=" & iYearID & ""
            sSql = sSql & " And CRCM_CompID = " & iACID & ") order by ENT_ENTITYName"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadSubFunFromFieldWork(ByVal sAC As String, ByVal iACID As Integer, ByVal iFuncID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select SEM_ID,SEM_NAME from MST_SUBENTITY_MASTER where SEM_COMPID=" & iACID & " and SEM_ID in(Select CFW_SubFunctionID from Compliance_FieldWork where  CFW_CompID=" & iACID & " and (CFW_Status='Saved' OR CFW_Status='Updated' OR CFW_Status='Submitted') and CFW_YearID=" & iYearID & " and CFW_functionID=" & iFuncID & " ) order by SEM_NAME"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCRChecklistandIssuesDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer, iFunID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable, dtWP As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Dim iSlNo As Integer = 0
        Dim sRiskType As String(), sRisk As String, sRiskName As String
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ChecklistID")
            dt.Columns.Add("ComplianceID")
            dt.Columns.Add("SubFunction")
            dt.Columns.Add("Process")
            dt.Columns.Add("SubProcess")
            dt.Columns.Add("IssueHeading")
            dt.Columns.Add("IssueDetails")
            dt.Columns.Add("IssueRating")
            dt.Columns.Add("IssueRatingColor")
            dt.Columns.Add("RiskDetails")
            dt.Columns.Add("Impact")
            dt.Columns.Add("RiskType")
            dt.Columns.Add("Control")
            dt.Columns.Add("ActualLoss")
            dt.Columns.Add("ProbableLoss")
            dt.Columns.Add("ActionPlan")
            'dt.Columns.Add("FunctionResponsible")
            'dt.Columns.Add("FunctionManager")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Remarks")
            dt.Columns.Add("IssueStatus")
            dt.Columns.Add("IssueStatusID")
            dt.Columns.Add("Status")
            dt.Columns.Add("IssueID")

            sSql = " Select CRCM_JobID,CRCD_PKID as ChecklistID,CRCD_SubFunID,CRCD_Risk,CRCD_RiskID,SEM_Name,CRCD_Control,CRCD_PID,CRCD_CheckRemarks,"
            sSql = sSql & " CRCD_SubPID,a.PM_name,b.SPM_Name,d.MRL_RiskTypeID,c.RAM_name From Compliance_Checklist"
            sSql = sSql & " left Join Compliance_Checklist_Mas On CRCM_ID=CRCD_MasID And CRCM_CustID=" & iCustID & " And CRCM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_SUBENTITY_MASTER On SEM_ID=CRCD_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " left join mst_process_master a On a.PM_ID=CRCD_PID And a.PM_CompID=" & iACID & ""
            sSql = sSql & " left join mst_Subprocess_master b On b.SPM_ID=CRCD_SubPID And b.SPM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_RISK_Library d On d.MRL_PKID=CRCD_RiskID And d.MRL_CompID=" & iACID & ""
            sSql = sSql & " left join Risk_GeneralMaster c On c.RAM_PKID=d.MRL_RiskTypeID and c.RAM_Category='RT' And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where CRCD_CompID =" & iACID & " And CRCM_JobID =" & iComplianceID & " And CRCM_FunID=" & iFunID & " And CRCM_Status ='Submitted'"
            sSql = sSql & " And CRCD_CertID=2 order by CRCD_PkID"
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            For j = 0 To dtTab.Rows.Count - 1
                iSlNo = iSlNo + 1
                drow = dt.NewRow
                drow("SrNo") = iSlNo
                drow("IssueHeading") = "" : drow("IssueRating") = "" : drow("Impact") = "" : drow("ActualLoss") = "" : drow("ProbableLoss") = ""
                drow("ActionPlan") = "" : drow("TargetDate") = ""
                drow("Remarks") = "" : drow("IssueStatus") = "" : drow("Status") = "" : drow("IssueID") = 0

                If IsDBNull(dtTab.Rows(j)("ChecklistID")) = False Then
                    drow("ChecklistID") = dtTab.Rows(j)("ChecklistID")
                End If
                If IsDBNull(dtTab.Rows(j)("CRCM_JobID")) = False Then
                    drow("ComplianceID") = dtTab.Rows(j)("CRCM_JobID")
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_SubFunID")) = False Then
                    drow("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SEM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_PID")) = False Then
                    drow("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("PM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_SubPID")) = False Then
                    drow("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SPM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_RiskID")) = False Then
                    drow("RiskDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Risk"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_Control")) = False Then
                    drow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Control"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_CheckRemarks")) = False Then
                    drow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_CheckRemarks"))
                End If
                'Work Paper
                sSql = "" : sSql = "Select CIT_PKID,CIT_RiskTypeID,CIT_IssueJobNo,CIT_Status,CIT_IssueHeading,CIT_IssueDetails,CIT_Impact,CIT_ActionPlan,CIT_IssueRatingID,"
                sSql = sSql & " CIT_ProbableLoss, CIT_ActualLoss,CIT_Impact, CIT_Remarks, CIT_IssueStatus, Convert(Varchar(10), CIT_TargetDate, 103)CIT_TargetDate,"
                sSql = sSql & " d.MIM_Name,d.MIM_Color from Compliance_IssueTracker_details "
                sSql = sSql & " Left join MST_InherentRisk_Master d On d.MIM_ID=CIT_IssueRatingID and d.MIM_CompID=" & iACID & ""
                sSql = sSql & " where CIT_ChecklistID = " & dtTab.Rows(j)("ChecklistID") & ""
                dtWP = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtWP.Rows.Count = 0 Then
                    drow("IssueHeading") = "" : drow("IssueRating") = "" : drow("Impact") = "" : drow("ActualLoss") = "" : drow("ProbableLoss") = ""
                    drow("ActionPlan") = "" : drow("TargetDate") = "" : drow("Remarks") = ""
                    drow("IssueStatus") = "" : drow("IssueStatusID") = "" : drow("Status") = "" : drow("IssueID") = 0 : drow("RiskType") = ""
                    dt.Rows.Add(drow)
                Else
                    Dim k As Integer
                    For k = 0 To dtWP.Rows.Count - 1
                        If k <> 0 Then
                            iSlNo = iSlNo + 1
                            drow = dt.NewRow
                            drow("SrNo") = iSlNo
                            drow("ChecklistID") = dtTab.Rows(j)("ChecklistID")
                            drow("ComplianceID") = dtTab.Rows(j)("CRCM_JobID")
                            drow("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("PM_Name"))
                            drow("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SPM_Name"))
                            drow("RiskDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Risk"))
                            drow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Control"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_RiskTypeID")) = False Then
                            If dtWP.Rows(k)("CIT_RiskTypeID") <> "" Then
                                sRiskType = dtWP.Rows(k)("CIT_RiskTypeID").Split(",")
                                If sRiskType.Length > 0 Then
                                    sRisk = ""
                                    For i = 1 To sRiskType.Length - 2
                                        sRiskName = LoadRiskNamefromID(sAC, iACID, iYearID, sRiskType(i))
                                        sRisk = sRisk & ", " & sRiskName
                                    Next
                                    If sRisk.StartsWith(",") Then
                                        sRisk = sRisk.Remove(0, 1)
                                    End If
                                    drow("RiskType") = sRisk
                                    End If
                                End If
                        End If
                        drow("IssueID") = dtWP.Rows(k)("CIT_PKID")
                        drow("IssueHeading") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_IssueHeading"))
                        drow("IssueDetails") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_IssueDetails"))
                        If IsDBNull(dtWP.Rows(k)("CIT_IssueRatingID")) = False Then
                            drow("IssueRating") = dtWP.Rows(k)("MIM_Name")
                            drow("IssueRatingColor") = dtWP.Rows(k)("MIM_Color")
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_Impact")) = False Then
                            drow("Impact") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_Impact"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ActualLoss")) = False Then
                            drow("ActualLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ActualLoss"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ProbableLoss")) = False Then
                            drow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ProbableLoss"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ActionPlan")) = False Then
                            drow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ActionPlan"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ProbableLoss")) = False Then
                            drow("ProbableLoss") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ProbableLoss"))
                        End If
                        'If IsDBNull(dtWP.Rows(k)("ENT_EntityName")) = False Then
                        '    drow("FunctionResponsible") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("ENT_EntityName"))
                        'End If
                        'If IsDBNull(dtWP.Rows(k)("Manager")) = False Then
                        '    drow("FunctionManager") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("Manager"))
                        'End If
                        If IsDBNull(dtWP.Rows(k)("CIT_TargetDate")) = False Then
                            drow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dtWP.Rows(k)("CIT_TargetDate"), "F")
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_Remarks")) = False Then
                            If dtWP.Rows(k)("CIT_Remarks") <> "&nbsp: " Then
                                If dtWP.Rows(k)("CIT_Remarks") <> "&nbsp;" Then
                                    drow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_Remarks"))
                                Else
                                    drow("Remarks") = ""
                                End If
                            End If
                        End If
                        If dtWP.Rows(k)("CIT_IssueStatus") = 1 Then
                            drow("IssueStatus") = "Open"
                            drow("IssueStatusID") = 1
                        ElseIf dtWP.Rows(k)("CIT_IssueStatus") = 2 Then
                            drow("IssueStatus") = "Closed"
                            drow("IssueStatusID") = 2
                        ElseIf dtWP.Rows(k)("CIT_IssueStatus") = 3 Then
                            drow("IssueStatus") = "Ongoing"
                            drow("IssueStatusID") = 3
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_Status")) = False Then
                            drow("Status") = dtWP.Rows(k)("CIT_Status")
                        End If
                        dt.Rows.Add(drow)
                    Next
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadAllIssueTrackerJobCode(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer, ByVal iChecklistID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CIT_PKID,CIT_IssueJobNo From Compliance_IssueTracker_details Where CIT_ComplianceCodeID=" & iComplianceID & " And CIT_CustomerID=" & iCustID & ""
            sSql = sSql & " and CIT_YearID=" & iYearID & " And CIT_ChecklistID=" & iChecklistID & " And CIT_CompID=" & iACID & " Order by CIT_PKID Desc"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckComplianceIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAsgNo As Integer, ByVal iChecklistID As Integer, ByVal sName As String, ByVal iIssueNO As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CIT_PKID From Compliance_IssueTracker_details Where CIT_ComplianceCodeID=" & iAsgNo & " And CIT_IssueHeading='" & sName & "' And CIT_ChecklistID=" & iChecklistID & " And CIT_CompID=" & iACID & ""
            If iIssueNO > 0 Then
                sSql = sSql & " And CIT_PKID <>" & iIssueNO & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCITMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iAsgNo As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*)+1 from Compliance_IssueTracker_details where CIT_CustomerID=" & iCustID & " And CIT_ComplianceCodeID=" & iAsgNo & " And CIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCITHistoryMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal iWPID As Integer, ByVal iIssueTrackerID As Integer, ByVal iComplianceID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Max(CITH_PKID) From Compliance_IssueTracker_details_History Where CITH_CITPKID=" & iIssueTrackerID & " And CITH_ComplianceCodeID=" & iComplianceID & " and CITH_WorkPaperID=" & iWPID & " And CITH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadIssueTrackerDetail(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer, ByVal iFunID As Integer, ByVal iYearID As Integer, ByVal sYearName As String) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable, dtWP As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("ComplianceCode")
            dt.Columns.Add("Function")
            dt.Columns.Add("FinancialYear")
            sSql = "Select CP_ComplianceCode,CP_ReportTitle,a.ENT_ENTITYName AS Functions,a.ENT_ID AS FunctionID, CFW_YearID from Compliance_FieldWork"
            sSql = sSql & " Left Join Compliance_Plan On CP_ID=CFW_ComplianceCodeID "
            sSql = sSql & " Left Join MST_Entity_master a On a.ENT_ID=CFW_FunctionID"
            sSql = sSql & " where CFW_ComplianceCodeID=" & iComplianceID & " And CFW_FunctionID=" & iFunID & ""
            sSql = sSql & " And CFW_CompID=" & iACID & " And CFW_YearID=" & iYearID & ""
            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dtTab.Rows.Count - 1
                drow = dt.NewRow
                drow("SrNo") = i + 1

                If IsDBNull(dtTab.Rows(i)("CP_ComplianceCode")) = False Then
                    drow("ComplianceCode") = dtTab.Rows(i)("CP_ComplianceCode")
                End If

                If IsDBNull(dtTab.Rows(i)("Functions")) = False Then
                    drow("Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("Functions"))
                End If

                If IsDBNull(dtTab.Rows(i)("CFW_YearID")) = False Then
                    drow("FinancialYear") = sYearName
                End If
                drow("Auditors") = objclsComplianceGeneral.GetCRCPMAuditTeam(sAC, iACID, iComplianceID)
                If IsDBNull(dtTab.Rows(i)("CP_ReportTitle")) = False Then
                    drow("ReportTitle") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(i)("CP_ReportTitle"))
                End If
                dt.Rows.Add(drow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadChecklistDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer, ByVal iChecklistID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select CRCD_PKID,CIT_RiskTypeID,CRCM_JobID,g.CP_ComplianceCode,CRCM_FunID,e.ENT_EntityName,CRCD_SubFunID,f.SEM_Name,CRCD_CheckRemarks,"
            sSql = sSql & " CRCD_Risk,CRCD_CheckID,CRCD_CheckDesc,CRCD_RiskID,CRCD_ControlID,CRCD_Control,CRCD_PID,CRCD_SubPID,"
            sSql = sSql & " a.PM_name,b.SPM_Name,d.MRL_RiskTypeID,c.RAM_name From Compliance_Checklist"
            sSql = sSql & " left join Compliance_Checklist_Mas On CRCM_ID=CRCD_MasID And CRCM_CustID=" & iCustID & " And CRCM_CompID=" & iACID & " And CRCM_YearID=" & iYearID & ""
            sSql = sSql & " left join mst_process_master a On a.PM_ID=CRCD_PID And PM_CompID=" & iACID & ""
            sSql = sSql & " left join mst_Subprocess_master b On b.SPM_ID=CRCD_SubPID And SPM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_RISK_Library d On d.MRL_PKID=CRCD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " left join Risk_GeneralMaster c On c.RAM_Category='RT' and c.RAM_PKID=d.MRL_RiskTypeID And c.RAM_YearID=" & iYearID & " And RAM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_entity_master e On e.ENT_ID=CRCM_FunID And ENT_CompId=" & iACID & ""
            sSql = sSql & " left join mst_subEntity_master f on f.SEM_ID=CRCD_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Compliance_Plan g on g.CP_ID=CRCM_JobID And g.CP_YearID=" & iYearID & " And CP_CompID=" & iACID & ""
            sSql = sSql & " Left Join Compliance_issuetracker_details On CIT_ComplianceCodeID=CRCM_JobID And CIT_YearID=" & iYearID & " And CIT_CompID=" & iACID & ""
            sSql = sSql & " Where CRCD_PKID =" & iChecklistID & " And CRCM_JobID=" & iComplianceID & " and CRCD_CompID=" & iACID & " And CRCD_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveComplianceIssueTracker(ByVal sAC As String, ByVal objCITDetails As strC_IssueTracker)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(32) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_YearID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_YearID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_CustomerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CustomerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ComplianceCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ComplianceCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ChecklistID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ChecklistID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_IssueJobNo", OleDb.OleDbType.VarChar, 50)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_IssueJobNo
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_FunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_FunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_SubFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_SubFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_SubProcessID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_SubProcessID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_RiskID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_RiskID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_RiskTypeID", OleDb.OleDbType.VarChar, 100)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_RiskTypeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ControlID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ControlID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_CheckID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CheckID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_IssueHeading", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_IssueHeading
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_IssueDetails", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_IssueDetails
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_Impact", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_Impact
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ActionPlan", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_ActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_IssueRatingID ", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_IssueRatingID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ActualLoss", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_ActualLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ProbableLoss", OleDb.OleDbType.VarChar, 2000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_ProbableLoss
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objCITDetails.dCIT_TargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_ResponsibleFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ResponsibleFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_FunctionManagerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_FunctionManagerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_IssueStatus", OleDb.OleDbType.VarChar, 5)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_IssueStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_CreatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_UpdatedBy", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CIT_AttachID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_AttachID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCompliance_IssueTracker_details", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCRIssueTrackerDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iITPkID As Integer, ByVal iComplianceID As Integer, ByVal iWPID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select *,MIM_Name,MIM_Color from Compliance_IssueTracker_details Left join MST_InherentRisk_Master On MIM_ID=CIT_IssueRatingID and"
            sSql = sSql & " MIM_CompID=" & iACID & " Where CIT_CompID=" & iACID & " And CIT_CustomerID=" & iCustID & " And CIT_PKID= " & iITPkID & " "
            sSql = sSql & " And  CIT_ComplianceCodeID=" & iComplianceID & " And CIT_ChecklistID=" & iWPID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCIssueTrackerSelectedStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer, ByVal iChecklistID As Integer, ByVal iITID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select CIT_Status From Compliance_IssueTracker_details Where CIT_CustomerID=" & iCustID & " And CIT_ComplianceCodeID=" & iComplianceID & ""
            sSql = sSql & " And CIT_ChecklistID=" & iChecklistID & " And CIT_PKID=" & iITID & " And CIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SubmitCIssueTracker(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iUserID As Integer, ByVal iCustID As Integer, ByVal iITID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Compliance_IssueTracker_details Set CIT_Status='Submitted',CIT_SubmittedBy=" & iUserID & ",CIT_SubmittedOn=GetDate()"
            sSql = sSql & " Where CIT_CustomerID=" & iCustID & " And CIT_PKID=" & iITID & " And CIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadCITActionPlanGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iITID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtDetails As New DataTable
        Dim dRow As DataRow
        Dim i As Integer
        Try
            dt.Columns.Add("ActionPlan")
            dt.Columns.Add("TargetDate")
            dt.Columns.Add("Status")
            dt.Columns.Add("Remarks")

            sSql = "Select CITH_PKID,CITH_ActionPlan,CITH_IssueStatus,CITH_Remarks,Convert(Varchar(10),CITH_TargetDate,103)CITH_TargetDate,CITH_ResponsibleFunctionID,"
            sSql = sSql & " CITH_FunctionManagerID,ENT_ENTITYName,usr_FullName From Compliance_IssueTracker_details_History"
            sSql = sSql & " Left Join MST_Entity_master On CITH_ResponsibleFunctionID=ENT_ID And ENT_CompId=" & iACID & " And ENT_Branch='F'"
            sSql = sSql & " Left Join Sad_UserDetails On CITH_FunctionManagerID=usr_Id And usr_CompId=" & iACID & ""
            sSql = sSql & "  Where CITH_CompID=" & iACID & " And CITH_CITPKID=" & iITID & " And CITH_CustomerID=" & iCustID & " Order by CITH_PKID Desc"
            dtDetails = objDBL.SQLExecuteDataTable(sAC, sSql)

            If dtDetails.Rows.Count > 0 Then
                For i = 0 To dtDetails.Rows.Count - 1
                    dRow = dt.NewRow
                    If IsDBNull(dtDetails.Rows(i)("CITH_ActionPlan")) = False Then
                        dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("CITH_ActionPlan").ToString)
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CITH_Remarks")) = False Then
                        dRow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtDetails.Rows(i)("CITH_Remarks").ToString)
                    End If
                    If IsDBNull(dtDetails.Rows(i)("CITH_TargetDate")) = False Then
                        dRow("TargetDate") = dtDetails.Rows(i)("CITH_TargetDate")
                    End If
                    If dtDetails.Rows(i)("CITH_IssueStatus") = 1 Then
                        dRow("Status") = "Open"
                    ElseIf dtDetails.Rows(i)("CITH_IssueStatus") = 2 Then
                        dRow("Status") = "Closed"
                    ElseIf dtDetails.Rows(i)("CITH_IssueStatus") = 3 Then
                        dRow("Status") = "Ongoing"
                    End If
                    'If IsDBNull(dtDetails.Rows(i)("CITH_ResponsibleFunctionID")) = False Then
                    '    dRow("ResponsibleFunction") = dtDetails.Rows(i)("ENT_ENTITYName")
                    'End If
                    'If IsDBNull(dtDetails.Rows(i)("CITH_FunctionManagerID")) = False Then
                    '    dRow("OwnerName") = dtDetails.Rows(i)("usr_FullName")
                    'End If

                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetCIssueTrackerHistoryMaxID(ByVal sAC As String, ByVal iACID As Integer, ByVal iWPID As Integer, ByVal iIssueTrackerID As Integer, ByVal iComplianceID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Max(CITH_PKID) From Compliance_IssueTracker_details_History Where CITH_CITPKID=" & iIssueTrackerID & " And CITH_ComplianceCodeID=" & iComplianceID & " and CITH_WorkPaperID=" & iWPID & " And CITH_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveCIssueTrackerHistory(ByVal sAC As String, ByVal objCITDetails As strC_IssueTracker, ByVal iCITPKID As Integer)
        Dim ObjParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(16) {}
        Dim iParamCount As Integer
        Dim Arr(1) As String
        Try
            iParamCount = 0
            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_PKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCITH_PKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_CITPKID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = iCITPKID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_CustomerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CustomerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_ComplianceCodeID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ComplianceCodeID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_ChecklistID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ChecklistID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_ActionPlan", OleDb.OleDbType.VarChar, 5000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_ActionPlan
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_TargetDate", OleDb.OleDbType.Date, 8)
            ObjParam(iParamCount).Value = objCITDetails.dCIT_TargetDate
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_IssueStatus", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_IssueStatus
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_ResponsibleFunctionID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_ResponsibleFunctionID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_FunctionManagerID", OleDb.OleDbType.Integer, 4)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_FunctionManagerID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_Remarks", OleDb.OleDbType.VarChar, 8000)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_Remarks
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_CreatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CreatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_UpdatedBy", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_UpdatedBy
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_CompID", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Value = objCITDetails.iCIT_CompID
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@CITH_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjParam(iParamCount).Value = objCITDetails.sCIT_IPAddress
            ObjParam(iParamCount).Direction = ParameterDirection.Input
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            iParamCount += 1

            ObjParam(iParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjParam(iParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spCompliance_IssueTracker_details_History", 1, Arr, ObjParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadCRIssuesDetailsToExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer, iFunID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable, dtWP As New DataTable
        Dim drow As DataRow
        Dim sSql As String
        Dim iSlNo As Integer = 0, iChecksID As Integer = 0
        Try
            dt.Columns.Add("Sub Function")
            dt.Columns.Add("Process")
            dt.Columns.Add("Sub Process")
            dt.Columns.Add("Risk")
            dt.Columns.Add("Control")
            dt.Columns.Add("Checks")
            dt.Columns.Add("Issue Heading")
            dt.Columns.Add("Issue Details")
            dt.Columns.Add("Issue Rating")
            dt.Columns.Add("Impact")
            dt.Columns.Add("Actual Loss")
            dt.Columns.Add("Probable Loss")
            dt.Columns.Add("Action Plan")
            dt.Columns.Add("Target Date")
            dt.Columns.Add("Status")
            dt.Columns.Add("Remarks")

            sSql = " Select CRCM_JobID,CRCD_PKID as ChecklistID,CRCD_SubFunID,CRCD_Risk,CRCD_RiskID,SEM_Name As SubFunctions,CRCD_ControlID,CRCD_Control,CRCD_PID,CRCD_CheckRemarks,"
            sSql = sSql & " CRCD_CheckID,CRCD_SubPID,CRCD_CheckRemarks,CRCD_CheckDesc,a.PM_name,b.SPM_Name,d.MRL_RiskTypeID,c.RAM_name From Compliance_Checklist"
            sSql = sSql & " left Join Compliance_Checklist_Mas On CRCM_ID=CRCD_MasID And CRCM_CustID=" & iCustID & " And CRCM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_SUBENTITY_MASTER On SEM_ID=CRCD_SubFunID And SEM_CompID=" & iACID & ""
            sSql = sSql & " left join mst_process_master a On a.PM_ID=CRCD_PID And a.PM_CompID=" & iACID & ""
            sSql = sSql & " left join mst_Subprocess_master b On b.SPM_ID=CRCD_SubPID And b.SPM_CompID=" & iACID & ""
            sSql = sSql & " left join MST_RISK_Library d On d.MRL_PKID=CRCD_RiskID And d.MRL_CompID=" & iACID & ""
            sSql = sSql & " left join Risk_GeneralMaster c On c.RAM_PKID=d.MRL_RiskTypeID and c.RAM_Category='RT' And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where CRCD_CompID =" & iACID & " And CRCM_JobID =" & iComplianceID & " And CRCM_FunID=" & iFunID & " And CRCM_Status ='Submitted'"
            sSql = sSql & " And CRCD_CertID=2 order by CRCD_PkID"

            dtTab = objDBL.SQLExecuteDataTable(sAC, sSql)
            For j = 0 To dtTab.Rows.Count - 1
                iSlNo = iSlNo + 1
                drow = dt.NewRow
                drow("Issue Heading") = "" : drow("Issue Rating") = "" : drow("Impact") = "" : drow("Actual Loss") = "" : drow("Probable Loss") = ""
                drow("Action Plan") = "" : drow("Target Date") = "" : drow("Sub Function") = "" : drow("Checks") = ""
                drow("Remarks") = "" : drow("Status") = ""
                iChecksID = 0
                If IsDBNull(dtTab.Rows(j)("ChecklistID")) = False Then
                    iChecksID = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("ChecklistID"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_SubFunID")) = False Then
                    drow("Sub Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SubFunctions"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_PID")) = False Then
                    drow("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("PM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_SubPID")) = False Then
                    drow("Sub Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SPM_Name"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_RiskID")) = False Then
                    drow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Risk"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_ControlID")) = False Then
                    drow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Control"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_CheckID")) = False Then
                    drow("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_CheckDesc"))
                End If
                If IsDBNull(dtTab.Rows(j)("CRCD_CheckRemarks")) = False Then
                    drow("Issue Details") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_CheckRemarks"))
                End If

                'Work Paper
                sSql = "" : sSql = "Select CIT_PKID,CIT_IssueJobNo,CIT_Status,CIT_IssueHeading,CIT_IssueDetails,CIT_Impact,CIT_ActionPlan,CIT_IssueRatingID,"
                sSql = sSql & " CIT_ProbableLoss, CIT_ActualLoss,CIT_Impact, CIT_Remarks, CIT_IssueStatus, Convert(Varchar(10), CIT_TargetDate, 103)CIT_TargetDate"
                sSql = sSql & " from Compliance_IssueTracker_details"
                sSql = sSql & " where CIT_ChecklistID = " & dtTab.Rows(j)("ChecklistID") & " And CIT_YearID=" & iYearID & ""
                dtWP = objDBL.SQLExecuteDataTable(sAC, sSql)
                If dtWP.Rows.Count = 0 Then
                    drow("Issue Heading") = "" : drow("Issue Rating") = "" : drow("Impact") = "" : drow("Actual Loss") = "" : drow("Probable Loss") = ""
                    drow("Action Plan") = "" : drow("Target Date") = ""
                    drow("Remarks") = "" : drow("Status") = ""
                    dt.Rows.Add(drow)
                    If IsDBNull(dtTab.Rows(j)("CRCD_CheckRemarks")) = False Then
                        drow("Issue Details") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_CheckRemarks"))
                    End If
                Else
                    Dim k As Integer
                    For k = 0 To dtWP.Rows.Count - 1
                        If k <> 0 Then
                            drow = dt.NewRow
                            drow("Sub Function") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SubFunctions"))
                            drow("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("PM_Name"))
                            drow("Sub Process") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("SPM_Name"))
                            drow("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Risk"))
                            drow("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_Control"))
                            drow("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_CheckDesc"))
                            drow("Issue Details") = objclsGRACeGeneral.ReplaceSafeSQL(dtTab.Rows(j)("CRCD_CheckRemarks"))
                        End If
                        drow("Issue Heading") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_IssueHeading"))
                        drow("Issue Details") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_IssueDetails"))
                        If dtWP.Rows(k)("CIT_IssueRatingID") = 1 Then
                            drow("Issue Rating") = "High"
                        ElseIf dtWP.Rows(k)("CIT_IssueRatingID") = 2 Then
                            drow("Issue Rating") = "Medium"
                        ElseIf dtWP.Rows(k)("CIT_IssueRatingID") = 3 Then
                            drow("Issue Rating") = "Low"
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_Impact")) = False Then
                            drow("Impact") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_Impact"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ActualLoss")) = False Then
                            drow("Actual Loss") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ActualLoss"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ProbableLoss")) = False Then
                            drow("Probable Loss") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ProbableLoss"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_ActionPlan")) = False Then
                            drow("Action Plan") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_ActionPlan"))
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_TargetDate")) = False Then
                            drow("Target Date") = objclsGRACeGeneral.FormatDtForRDBMS(dtWP.Rows(k)("CIT_TargetDate"), "F")
                        End If
                        If IsDBNull(dtWP.Rows(k)("CIT_Remarks")) = False Then
                            If dtWP.Rows(k)("CIT_Remarks") <> "&nbsp: " Then
                                If dtWP.Rows(k)("CIT_Remarks") <> "&nbsp;" Then
                                    drow("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dtWP.Rows(k)("CIT_Remarks"))
                                Else
                                    drow("Remarks") = ""
                                End If
                            End If
                        End If
                        If dtWP.Rows(k)("CIT_IssueStatus") = 1 Then
                            drow("Status") = "Open"
                        ElseIf dtWP.Rows(k)("CIT_IssueStatus") = 2 Then
                            drow("Status") = "Closed"
                        ElseIf dtWP.Rows(k)("CIT_IssueStatus") = 3 Then
                            drow("Status") = "Ongoing"
                        End If
                        dt.Rows.Add(drow)
                    Next
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadActiveRiskTypeName(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select RAM_PKID,RAM_Name from Risk_generalMaster Where RAM_YearID=" & iYearID & " And  RAM_Category='RT' And RAM_DelFlag='A' And RAM_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
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
    Public Function GetFieldWorkIDForFollowUP(ByVal sAC As String, ByVal iACID As Integer, ByVal iComplianceID As Integer, ByVal iIssueTrackerID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CIT_WorkPaperID from Compliance_IssueTracker_details where CIT_ComplianceCodeID=" & iComplianceID & " And CIT_PKID=" & iIssueTrackerID & " And CIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetNoOfChecklist(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) From Compliance_Checklist Where CRCD_CertID=2 And CRCD_CompID=" & iACID & " And CRCD_MasID IN(Select CRCM_ID From Compliance_Checklist_Mas "
            sSql = sSql & " Where CRCM_CustID=" & iCustID & " And CRCM_JobID=" & iComplianceID & " And CRCM_Status='Submitted' And CRCM_YearID=" & iYearID & ")"
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerStarted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(CIT_PKID) from Compliance_IssueTracker_details Where CIT_CompID=" & iACID & " And CIT_ComplianceCodeID=" & iComplianceID & ""
            sSql = sSql & " and CIT_Status <>'Submitted' And CIT_CustomerID=" & iCustID & " And CIT_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueTrackerCompleted(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer) As Integer
        Dim sSql As String : Dim dt As New DataTable
        Try
            sSql = "Select Count(CIT_PKID) from Compliance_IssueTracker_details Where CIT_CompID =" & iACID & " And CIT_ComplianceCodeID=" & iComplianceID & " and"
            sSql = sSql & " CIT_Status='Submitted' And CIT_CustomerID=" & iCustID & " And CIT_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetTotalSeverity(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select Count(*) from Compliance_IssueTracker_details Where CIT_CustomerID=" & iCustID & " And CIT_CompID =" & iACID & " And CIT_ComplianceCodeID=" & iComplianceID & " And CIT_YearID=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GeIssueRatingChart(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iComplianceID As Integer, ByVal iYearID As Integer) As DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Dim dt As New DataTable, dtTab As New DataTable, dtRating As New DataTable, dtMaster As New DataTable
        Dim i As Integer
        Try
            dt.Columns.Add("Count")
            dt.Columns.Add("Name")
            dt.Columns.Add("Color")
            sSql = "Select Distinct(CIT_IssueRatingID),CIT_PKID from Compliance_IssueTracker_Details Left Join Compliance_Checklist on CRCD_SubFunID=CIT_SubFunctionID And"
            sSql = sSql & " CRCD_PID= CIT_ProcessID And CRCD_SubPID=CIT_SubProcessID And CRCD_RiskID=CIT_RiskID And CRCD_ControlID=CIT_ControlID And CRCD_CertID=2 And "
            sSql = sSql & " CRCD_CheckID=CRCD_CheckID And CRCD_MasID IN(Select CRCM_ID From Compliance_Checklist_Mas Where CRCM_JobID=CIT_ComplianceCodeID"
            sSql = sSql & " And CRCM_FunID=CIT_FunctionID And CRCM_CustID=" & iCustID & " And CRCM_YearID=" & iYearID & " And CRCM_Status='Submitted')"
            sSql = sSql & " And CRCD_CompID=" & iACID & " Where CIT_CompID=" & iACID & " And CIT_ComplianceCodeID=" & iComplianceID & " And CIT_YearID=" & iYearID & ""
            sSql = sSql & " And CIT_CustomerID=" & iCustID & ""
            dtMaster = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)

            dtRating = GetIssueRatingName(sAC, iACID)
            For i = 0 To dtRating.Rows.Count - 1
                dRow = dt.NewRow
                Dim DVdtMaster As New DataView(dtMaster)
                DVdtMaster.RowFilter = "CIT_IssueRatingID =" & dtRating.Rows(i)("MIM_ID") & ""
                dtTab = DVdtMaster.ToTable
                dRow("Count") = dtTab.Rows.Count
                dRow("Name") = dtRating.Rows(i)("MIM_Name")
                dRow("Color") = dtRating.Rows(i)("MIM_Color")
                dt.Rows.Add(dRow)
            Next
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueRatingName(ByVal sAC As String, ByVal iACID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select MIM_ID,MIM_Name,MIM_Color from MST_InherentRisk_Master where MIM_CompID=" & iACID & " order by MIM_ID"
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetDocIDDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iAuditID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select CIT_PGEDetailId From Compliance_IssueTracker_details Where CIT_YearID=" & iYearID & " And"
            If iAuditID > 0 Then
                sSql = sSql & " CIT_ComplianceCodeID=" & iAuditID & " And"
            End If
            sSql = sSql & " CIT_CustomerID=" & iCustID & " And CIT_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateAttachmentID(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer,
                                           ByVal iAuditID As Integer, ByVal iAttachID As Integer, ByVal iPGDetailID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Compliance_IssueTracker_details Set CIT_AttachID=" & iAttachID & ",CIT_PGEDetailId=" & iPGDetailID & " Where"
            If iAuditID > 0 Then
                sSql = sSql & " CIT_ComplianceCodeID=" & iAuditID & " And"
            End If
            sSql = sSql & " CIT_YearID=" & iYearID & " And CIT_CustomerID=" & iCustID & " And CIT_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function CheckComplianceIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAsgNo As Integer, ByVal sName As String, ByVal iPKID As Integer, ByVal iCustID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select CIT_PKID From Compliance_IssueTracker_details Where CIT_ComplianceCodeID=" & iAsgNo & " And CIT_IssueHeading='" & sName & "'"
            sSql = sSql & " And CIT_YearID=" & iYearID & " and CIT_CompID=" & iACID & " And CIT_CustomerID=" & iCustID & ""
            If iPKID > 0 Then
                sSql = sSql & " And CIT_PKID <>" & iPKID & ""
            End If
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetIssueReviewNo(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal IReviewID As Integer, ByVal iCustID As Integer) As String
        Dim sSql As String = "" : Dim ChkRec As String = 0
        Try
            sSql = "Select CP_ComplianceCode from Compliance_Plan where CP_ID=" & IReviewID & " and CP_CompID=" & iACID & " and CP_YearID=" & iYearID & " And CP_CustomerID=" & iCustID & ""
            ChkRec = objDBL.SQLExecuteScalar(sAC, sSql)
            Return ChkRec
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class