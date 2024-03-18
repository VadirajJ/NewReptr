Imports DatabaseLayer
Public Structure strFA_ARA
    Dim iARA_PKID As Integer
    Dim iARA_AuditCodeID As Integer
    Dim iARA_FinancialYear As Integer
    Dim iARA_FunID As Integer
    Dim iARA_CustID As Integer
    Dim iARA_NetScore As Integer
    Dim iARA_CrBy As Integer
    Dim iARA_UpdatedBy As Integer
    Dim iARA_SubmittedBy As Integer
    Dim sARA_Status As String
    Dim sARA_Comments As String
    Dim sARA_IPAddress As String
    Dim iARA_CompID As Integer
    Public Property iARAPKID() As Integer
        Get
            Return (iARA_PKID)
        End Get
        Set(ByVal Value As Integer)
            iARA_PKID = Value
        End Set
    End Property
    Public Property iARACustID() As Integer
        Get
            Return (iARA_CustID)
        End Get
        Set(ByVal Value As Integer)
            iARA_CustID = Value
        End Set
    End Property
    Public Property iARAAuditCodeID() As Integer
        Get
            Return (iARA_AuditCodeID)
        End Get
        Set(ByVal Value As Integer)
            iARA_AuditCodeID = Value
        End Set
    End Property
    Public Property iARAFinancialYear() As Integer
        Get
            Return (iARA_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iARA_FinancialYear = Value
        End Set
    End Property
    Public Property iARAFunID() As Integer
        Get
            Return (iARA_FunID)
        End Get
        Set(ByVal Value As Integer)
            iARA_FunID = Value
        End Set
    End Property

    Public Property iARANetScore() As Integer
        Get
            Return (iARA_NetScore)
        End Get
        Set(ByVal Value As Integer)
            iARA_NetScore = Value
        End Set
    End Property
    Public Property iARACrBy() As Integer
        Get
            Return (iARA_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iARA_CrBy = Value
        End Set
    End Property
    Public Property iARAUpdatedBy() As Integer
        Get
            Return (iARA_UpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iARA_UpdatedBy = Value
        End Set
    End Property
    Public Property sARAStatus() As String
        Get
            Return (sARA_Status)
        End Get
        Set(ByVal Value As String)
            sARA_Status = Value
        End Set
    End Property
    Public Property sARAComments() As String
        Get
            Return (sARA_Comments)
        End Get
        Set(ByVal Value As String)
            sARA_Comments = Value
        End Set
    End Property
    Public Property sARAIPAddress() As String
        Get
            Return (sARA_IPAddress)
        End Get
        Set(ByVal Value As String)
            sARA_IPAddress = Value
        End Set
    End Property
    Public Property iARACompID() As Integer
        Get
            Return (iARA_CompID)
        End Get
        Set(ByVal Value As Integer)
            iARA_CompID = Value
        End Set
    End Property
End Structure
Public Structure strFA_ARADetails
    Dim iARAD_PKID As Integer
    Dim iARAD_ARAPKID As Integer
    Dim iARAD_SEMID As Integer
    Dim iARAD_PMID As Integer
    Dim iARAD_SPMID As Integer
    Dim sARAD_IssueHeading As String
    Dim iARAD_RiskID As Integer
    Dim iARAD_RiskTypeID As Integer
    Dim iARAD_ImpactID As Integer
    Dim iARAD_LikelihoodID As Integer
    Dim iARAD_RiskRating As Integer
    Dim iARAD_ControlID As Integer
    Dim iARAD_OES As Integer
    Dim iARAD_DES As Integer
    Dim iARAD_ControlRating As Integer
    Dim iARAD_ChecksID As Integer
    Dim iARAD_ResidualRiskRating As Integer
    Dim sARAD_Remarks As String
    Dim sARAD_IPAddress As String
    Dim iARAD_CompID As Integer
    Public Property iARADPKID() As Integer
        Get
            Return (iARAD_PKID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_PKID = Value
        End Set
    End Property
    Public Property iARADARAPKID() As Integer
        Get
            Return (iARAD_ARAPKID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_ARAPKID = Value
        End Set
    End Property
    Public Property iARADSEMID() As Integer
        Get
            Return (iARAD_SEMID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_SEMID = Value
        End Set
    End Property
    Public Property iARADPMID() As Integer
        Get
            Return (iARAD_PMID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_PMID = Value
        End Set
    End Property
    Public Property iARADSPMID() As Integer
        Get
            Return (iARAD_SPMID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_SPMID = Value
        End Set
    End Property
    Public Property sARADIssueHeading() As String
        Get
            Return (sARAD_IssueHeading)
        End Get
        Set(ByVal Value As String)
            sARAD_IssueHeading = Value
        End Set
    End Property
    Public Property iARADRiskID() As Integer
        Get
            Return (iARAD_RiskID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_RiskID = Value
        End Set
    End Property
    Public Property iARADRiskTypeID() As Integer
        Get
            Return (iARAD_RiskTypeID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_RiskTypeID = Value
        End Set
    End Property
    Public Property iARADImpactID() As Integer
        Get
            Return (iARAD_ImpactID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_ImpactID = Value
        End Set
    End Property
    Public Property iARADLikelihoodID() As Integer
        Get
            Return (iARAD_LikelihoodID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_LikelihoodID = Value
        End Set
    End Property
    Public Property iARADRiskRating() As Integer
        Get
            Return (iARAD_RiskRating)
        End Get
        Set(ByVal Value As Integer)
            iARAD_RiskRating = Value
        End Set
    End Property
    Public Property iARADControlID() As Integer
        Get
            Return (iARAD_ControlID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_ControlID = Value
        End Set
    End Property
    Public Property iARADOES() As Integer
        Get
            Return (iARAD_OES)
        End Get
        Set(ByVal Value As Integer)
            iARAD_OES = Value
        End Set
    End Property
    Public Property iARADDES() As Integer
        Get
            Return (iARAD_DES)
        End Get
        Set(ByVal Value As Integer)
            iARAD_DES = Value
        End Set
    End Property
    Public Property iARADControlRating() As Integer
        Get
            Return (iARAD_ControlRating)
        End Get
        Set(ByVal Value As Integer)
            iARAD_ControlRating = Value
        End Set
    End Property
    Public Property iARADChecksID() As Integer
        Get
            Return (iARAD_ChecksID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_ChecksID = Value
        End Set
    End Property
    Public Property iARADResidualRiskRating() As Integer
        Get
            Return (iARAD_ResidualRiskRating)
        End Get
        Set(ByVal Value As Integer)
            iARAD_ResidualRiskRating = Value
        End Set
    End Property
    Public Property sARADRemarks() As String
        Get
            Return (sARAD_Remarks)
        End Get
        Set(ByVal Value As String)
            sARAD_Remarks = Value
        End Set
    End Property
    Public Property sARADIPAddress() As String
        Get
            Return (sARAD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sARAD_IPAddress = Value
        End Set
    End Property
    Public Property iARADCompID() As Integer
        Get
            Return (iARAD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iARAD_CompID = Value
        End Set
    End Property
End Structure
Public Class clsRAChecklist
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsAuditGeneral As New clsAuditGeneral
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Public Function CheckRecordsFAARA(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iFunID As Integer) As Boolean
        Dim sSql As String
        Try
            sSql = "Select ARA_PKID from Audit_ARA where ARA_AuditCodeID=" & iAuditCodeID & " and ARA_FunID=" & iFunID & " and ARA_CompID=" & iACID & " and ARA_FinancialYear=" & iYearID & ""
            Return objDBL.SQLCheckForRecord(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFAARADataGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("IssueIDs")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("SubProcessKey")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("RiskType")
            dtTab.Columns.Add("RiskKey")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("ControlKey")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("ChecksKey")
            dtTab.Columns.Add("Impact")
            dtTab.Columns.Add("ImpactID")
            dtTab.Columns.Add("Likelihood")
            dtTab.Columns.Add("LikelihoodID")
            dtTab.Columns.Add("RiskRating")
            dtTab.Columns.Add("RiskRatingColor")
            dtTab.Columns.Add("OE")
            dtTab.Columns.Add("OEID")
            dtTab.Columns.Add("DE")
            dtTab.Columns.Add("DEID")
            dtTab.Columns.Add("ControlRating")
            dtTab.Columns.Add("ControlRatingColor")
            dtTab.Columns.Add("ResidualRiskRating")
            dtTab.Columns.Add("ResidualRiskRatingColor")
            dtTab.Columns.Add("Remarks")

            dtTab.Columns.Add("PYImpact")
            dtTab.Columns.Add("PYLikelihood")
            dtTab.Columns.Add("PYRiskRating")
            dtTab.Columns.Add("PYRiskRatingColor")
            dtTab.Columns.Add("PYOESName")
            dtTab.Columns.Add("PYDESName")
            dtTab.Columns.Add("PYControlRating")
            dtTab.Columns.Add("PYControlRatingColor")
            dtTab.Columns.Add("PYResidualRiskRating")
            dtTab.Columns.Add("PYResidualRiskRatingColor")

            sSql = "Select MMM_ID,MMM_FunID,MMM_SEMID,MMM_PMID,PM_Name,MMM_SPMID,SPM_Name,MMM_SPMKey,MMM_RISKID,MMM_Risk,MMM_RiskKey,v.RAM_Name,SEM_Name,MMM_CONTROLID,"
            sSql = sSql & " MMM_Control, MMM_ControlKey, MMM_ChecksID, MMM_CHECKS, MMM_ChecksKey,"
            sSql = sSql & " ARAD_ImpactID As PYImpactID, ARAD_LikelihoodID As PYLikelihoodID, ARAD_OES As PYOESID, ARAD_DES As PYDESID,"
            sSql = sSql & " p.RAM_Name As PYImpact, q.RAM_Name As PYLikelihood, r.RAM_Name As PYOESName, s.RAM_Name As PYDESName,"
            sSql = sSql & " ARAD_RiskRating As PYRiskRating, ARAD_ControlRating As PYControlRating, ARAD_ResidualRiskRating As PYResidualRiskRating"
            sSql = sSql & " From MST_MAPPING_MASTER Left Join Audit_ARA On ARA_FunID=" & iFunctionID & " And ARA_FinancialYear=" & iYearID - 1 & " And ARA_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_ARA_Details On ARAD_ARAPKID=ARA_PKID And ARAD_PMID=MMM_PMID"
            sSql = sSql & " And ARAD_SPMID = MMM_SPMID And ARAD_RiskID = MMM_RISKID And ARAD_ControlID = MMM_CONTROLID And ARAD_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER  On Sem_ID=MMM_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=MMM_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=MMM_SPMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster v On v.RAM_PKID = (Select MRL_RiskTypeID from MST_RISK_Library where MRL_PKID=MMM_RISKID And MRL_CompID=" & iACID & ")"
            sSql = sSql & " Left Join Risk_GeneralMaster p On p.RAM_Category='RI' And p.RAM_YearID=" & iYearID - 1 & " And p.RAM_PKID=ARAD_ImpactID and p.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster q On q.RAM_Category='RL' And q.RAM_YearID=" & iYearID - 1 & " And q.RAM_PKID=ARAD_LikelihoodID and q.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster r On r.RAM_Category='OES' And r.RAM_YearID=" & iYearID - 1 & " And r.RAM_PKID=ARAD_OES and r.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster s On s.RAM_Category='DES' And s.RAM_YearID=" & iYearID - 1 & " And s.RAM_PKID=ARAD_DES and s.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where MMM_DelFlag ='A' And MMM_YearID=" & iYearID & " And MMM_Module='A' And MMM_FunID=" & iFunctionID & ""
            sSql = sSql & " And MMM_CompID=" & iACID & " Order by PM_Name, SPM_Name, MMM_Risk, MMM_Control, MMM_CHECKS"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("SubFunctionID") = dt.Rows(i)("MMM_SEMID")
                dr("ProcessID") = dt.Rows(i)("MMM_PMID")
                dr("SubProcessID") = dt.Rows(i)("MMM_SPMID")
                dr("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_Name"))
                dr("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_Name"))
                dr("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_Name"))
                If dt.Rows(i)("MMM_SPMKey") = 1 Then
                    dr("SubProcessKey") = "KEY"
                Else
                    dr("SubProcessKey") = "NON-KEY"
                End If
                dr("RisKID") = dt.Rows(i)("MMM_RISKID")
                dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MMM_Risk"))
                dr("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAM_Name"))
                If dt.Rows(i)("MMM_RiskKey") = 1 Then
                    dr("RiskKey") = "KEY"
                Else
                    dr("RiskKey") = "NON-KEY"
                End If
                dr("ControlID") = dt.Rows(i)("MMM_CONTROLID")
                dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MMM_Control"))
                If dt.Rows(i)("MMM_ControlKey") = 1 Then
                    dr("ControlKey") = "KEY"
                Else
                    dr("ControlKey") = "NON-KEY"
                End If
                dr("ChecksID") = dt.Rows(i)("MMM_ChecksID")
                If IsDBNull(dt.Rows(i)("MMM_CHECKS")) = False Then
                    dr("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MMM_CHECKS"))
                End If
                If dt.Rows(i)("MMM_ChecksKey") = 1 Then
                    dr("ChecksKey") = "KEY"
                Else
                    dr("ChecksKey") = "NON-KEY"
                End If

                'Previous Year Impact
                If IsDBNull(dt.Rows(i)("PYImpact")) = False Then
                    dr("PYImpact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYImpact"))
                End If
                'Previous Year Likelihood
                If IsDBNull(dt.Rows(i)("PYLikelihood")) = False Then
                    dr("PYLikelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYLikelihood"))
                End If
                'Previous Year RiskRating
                dr("PYRiskRating") = "" : dr("PYRiskRatingColor") = ""
                If IsDBNull(dr("PYImpact")) = False And IsDBNull(dr("PYLikelihood")) = False Then
                    If IsDBNull(dt.Rows(i)("PYRiskRating")) = False Then
                        If dt.Rows(i)("PYRiskRating") > 0 Then
                            dr("PYRiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYRiskRating"), "GRS", "Name")
                        Else
                            dr("PYRiskRating") = ""
                        End If
                        dr("PYRiskRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYRiskRating"), "GRS", "Color")
                    End If
                End If
                'Previous Year OESName
                If IsDBNull(dt.Rows(i)("PYOESName")) = False Then
                    dr("PYOESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYOESName"))
                End If
                'Previous Year DESName
                If IsDBNull(dt.Rows(i)("PYDESName")) = False Then
                    dr("PYDESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYDESName"))
                End If
                'Previous Year ControlRating
                If IsDBNull(dr("PYOESName")) = False And IsDBNull(dr("PYDESName")) = False Then
                    If IsDBNull(dt.Rows(i)("PYControlRating")) = False Then
                        If dt.Rows(i)("PYControlRating") > 0 Then
                            dr("PYControlRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYControlRating"), "GCS", "Name")
                        Else
                            dr("PYControlRating") = ""
                        End If
                        dr("PYControlRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYControlRating"), "GCS", "Color")
                    End If
                End If
                'Previous Year ResidualRiskRating
                dr("PYResidualRiskRating") = "" : dr("PYResidualRiskRatingColor") = ""
                If IsDBNull(dr("PYControlRating")) = False And IsDBNull(dr("PYControlRating")) = False Then
                    If dt.Rows(i)("PYRiskRating") > 0 And dt.Rows(i)("PYControlRating") > 0 Then
                        If dt.Rows(i)("PYResidualRiskRating") >= 0 Then
                            dr("PYResidualRiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYResidualRiskRating"), "RRS", "Name")
                            dr("PYResidualRiskRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYResidualRiskRating"), "RRS", "Color")
                        Else
                            dr("PYResidualRiskRating") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Name")
                            dr("PYResidualRiskRatingColor") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                        End If
                    End If
                End If

                dr("Impact") = "" : dr("ImpactID") = "0"
                dr("Likelihood") = "" : dr("LikelihoodID") = "0"
                dr("RiskRating") = "" : dr("RiskRatingColor") = ""
                dr("OE") = "" : dr("OEID") = "0"
                dr("DE") = "" : dr("DEID") = "0"
                dr("ControlRating") = "" : dr("ControlRatingColor") = ""
                dr("ResidualRiskRating") = "" : dr("ResidualRiskRatingColor") = ""
                dr("Remarks") = "" : dr("IssueIDs") = "0"
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFAARASavedGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim iSubProcessKey As Integer = 0, iRiskKey As Integer = 0, iChecksKey As Integer = 0
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("IssueIDs")
            dtTab.Columns.Add("SubFunctionID")
            dtTab.Columns.Add("ProcessID")
            dtTab.Columns.Add("SubProcessID")
            dtTab.Columns.Add("RisKID")
            dtTab.Columns.Add("ControlID")
            dtTab.Columns.Add("ChecksID")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("SubProcessKey")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("RiskType")
            dtTab.Columns.Add("RiskKey")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("ControlKey")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("ChecksKey")
            dtTab.Columns.Add("Impact")
            dtTab.Columns.Add("ImpactID")
            dtTab.Columns.Add("Likelihood")
            dtTab.Columns.Add("LikelihoodID")
            dtTab.Columns.Add("RiskRating")
            dtTab.Columns.Add("RiskRatingColor")
            dtTab.Columns.Add("OE")
            dtTab.Columns.Add("OEID")
            dtTab.Columns.Add("DE")
            dtTab.Columns.Add("DEID")
            dtTab.Columns.Add("ControlRating")
            dtTab.Columns.Add("ControlRatingColor")
            dtTab.Columns.Add("ResidualRiskRating")
            dtTab.Columns.Add("ResidualRiskRatingColor")
            dtTab.Columns.Add("Remarks")

            dtTab.Columns.Add("PYImpact")
            dtTab.Columns.Add("PYLikelihood")
            dtTab.Columns.Add("PYRiskRating")
            dtTab.Columns.Add("PYRiskRatingColor")
            dtTab.Columns.Add("PYOESName")
            dtTab.Columns.Add("PYDESName")
            dtTab.Columns.Add("PYControlRating")
            dtTab.Columns.Add("PYControlRatingColor")
            dtTab.Columns.Add("PYResidualRiskRating")
            dtTab.Columns.Add("PYResidualRiskRatingColor")

            sSql = "Select m.ARAD_SEMID,m.ARAD_PMID,m.ARAD_SPMID,m.ARAD_IssueHeading,m.ARAD_RiskID,m.ARAD_ImpactID,m.ARAD_LikelihoodID,m.ARAD_RiskRating,m.ARAD_ControlID,m.ARAD_OES,m.ARAD_DES,m.ARAD_ControlRating,"
            sSql = sSql & " m.ARAD_ChecksID,m.ARAD_ResidualRiskRating,m.ARAD_Remarks,SEM_NAME as SubFunction,PM_NAME as Process,SPM_NAME as SubProcess,SPM_IsKey as SubProcessKey,"
            sSql = sSql & " MRL_RiskName as Risk,a.RAM_Name as RiskType,MRL_IsKey as RiskKey,MCL_ControlName as Controls,MCL_IsKey as ControlKey,CHK_CheckName as Checks,"
            sSql = sSql & " b.RAM_Name As Impact,c.RAM_Name As Likelihood,d.RAM_Name As OE,e.RAM_Name As DE,"
            sSql = sSql & " RRPYD.ARAD_ImpactID As PYImpactID,RRPYD.ARAD_LikelihoodID As PYLikelihoodID,RRPYD.ARAD_OES As PYOESID,RRPYD.ARAD_DES As PYDESID,"
            sSql = sSql & " p.RAM_Name As PYImpact,q.RAM_Name As PYLikelihood,r.RAM_Name As PYOESName,s.RAM_Name As PYDESName,"
            sSql = sSql & " RRPYD.ARAD_RiskRating As PYRiskRating,RRPYD.ARAD_ControlRating As PYControlRating,RRPYD.ARAD_ResidualRiskRating As PYResidualRiskRating,"
            sSql = sSql & " CHK_IsKey as ChecksKey From Audit_ARA_Details m"
            sSql = sSql & " Left Join Audit_ARA RRPY on RRPY.ARA_FunID=" & iFunctionID & " And RRPY.ARA_FinancialYear=" & iYearID - 1 & " And RRPY.ARA_CompID=" & iACID & ""
            sSql = sSql & " Left Join Audit_ARA_Details RRPYD on RRPYD.ARAD_ARAPKID=RRPY.ARA_PKID  and m.ARAD_SEMID=RRPYD.ARAD_SEMID And m.ARAD_PMID=RRPYD.ARAD_PMID"
            sSql = sSql & " And m.ARAD_SPMID=RRPYD.ARAD_SPMID And m.ARAD_RiskID=RRPYD.ARAD_RiskID And m.ARAD_ControlID=RRPYD.ARAD_ControlID And RRPYD.ARAD_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_Ent_ID=" & iFunctionID & " And SEM_ID=m.ARAD_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ENT_ID=" & iFunctionID & "  And PM_ID=m.ARAD_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ENT_ID=" & iFunctionID & " And SPM_PM_ID=m.ARAD_PMID"
            sSql = sSql & " And SPM_ID=m.ARAD_SPMID And SPM_CompID=" & iACID & " Left Join MST_RISK_Library On MRL_PKID=m.ARAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster a On a.RAM_PKID=m.ARAD_RiskTypeID And a.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_CONTROL_Library On MCL_PKID=m.ARAD_ControlID And MCL_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Checks_Master On CHK_ControlID=m.ARAD_ControlID And CHK_ID=m.ARAD_ChecksID And CHK_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_Delflag='A' And b.RAM_Category='RI' And b.RAM_YearID=" & iYearID & " And b.RAM_PKID=m.ARAD_ImpactID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_Delflag='A' And c.RAM_Category='RL' And c.RAM_YearID=" & iYearID & " And c.RAM_PKID=m.ARAD_LikelihoodID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster d On d.RAM_Delflag='A' And d.RAM_Category='OES' And d.RAM_YearID=" & iYearID & " And d.RAM_PKID=m.ARAD_OES And d.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster e On e.RAM_Delflag='A' And e.RAM_Category='DES' And e.RAM_YearID=" & iYearID & " And e.RAM_PKID=m.ARAD_DES And e.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster p on p.RAM_Category='RI' And p.RAM_YearID=" & iYearID - 1 & " And p.RAM_PKID=RRPYD.ARAD_ImpactID and p.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster q On q.RAM_Category='RL' And q.RAM_YearID=" & iYearID - 1 & " And q.RAM_PKID=RRPYD.ARAD_LikelihoodID and q.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster r on r.RAM_Category='OES' And r.RAM_YearID=" & iYearID - 1 & " And r.RAM_PKID=RRPYD.ARAD_OES and r.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster s On s.RAM_Category='DES' And s.RAM_YearID=" & iYearID - 1 & " And s.RAM_PKID=RRPYD.ARAD_DES and s.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where m.ARAD_ARAPKID in (Select ARA_PKID From Audit_ARA Where ARA_AuditCodeID=" & iAuditID & " And ARA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And ARA_FunID=" & iFunctionID & "  and ARA_CompID=" & iACID & ") And m.ARAD_CompID =" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("ARAD_IssueHeading")) = False Then
                    dr("IssueIDs") = dt.Rows(i)("ARAD_IssueHeading")
                Else
                    dr("IssueIDs") = 0
                End If
                dr("SubFunctionID") = dt.Rows(i)("ARAD_SEMID")
                dr("ProcessID") = dt.Rows(i)("ARAD_PMID")
                dr("SubProcessID") = dt.Rows(i)("ARAD_SPMID")
                If IsDBNull(dt.Rows(i)("SubFunction")) = False Then
                    dr("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SubFunction"))
                End If

                If IsDBNull(dt.Rows(i)("Process")) = False Then
                    dr("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Process"))
                End If

                If IsDBNull(dt.Rows(i)("SubProcess")) = False Then
                    dr("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SubProcess"))
                End If
                iSubProcessKey = dt.Rows(i)("SubProcessKey")

                If iSubProcessKey = 1 Then
                    dr("SubProcessKey") = "KEY"
                Else
                    dr("SubProcessKey") = "NON-KEY"
                End If

                dr("RisKID") = dt.Rows(i)("ARAD_RiskID")
                If IsDBNull(dt.Rows(i)("Risk")) = False Then
                    dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Risk"))
                End If
                If IsDBNull(dt.Rows(i)("RiskType")) = False Then
                    dr("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RiskType"))
                End If

                iRiskKey = dt.Rows(i)("RiskKey")
                If iRiskKey = 1 Then
                    dr("RiskKey") = "KEY"
                Else
                    dr("RiskKey") = "NON-KEY"
                End If

                dr("ControlID") = dt.Rows(i)("ARAD_ControlID")
                If IsDBNull(dt.Rows(i)("Controls")) = False Then
                    dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Controls"))
                End If
                iRiskKey = dt.Rows(i)("ControlKey")

                If iRiskKey = 1 Then
                    dr("ControlKey") = "KEY"
                Else
                    dr("ControlKey") = "NON-KEY"
                End If
                dr("ChecksID") = dt.Rows(i)("ARAD_ChecksID")

                If IsDBNull(dt.Rows(i)("Checks")) = False Then
                    dr("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Checks"))
                End If
                If IsDBNull(dt.Rows(i)("ChecksKey")) = False Then
                    iChecksKey = dt.Rows(i)("ChecksKey")
                    If iChecksKey = 1 Then
                        dr("ChecksKey") = "KEY"
                    Else
                        dr("ChecksKey") = "NON-KEY"
                    End If
                End If

                If IsDBNull(dt.Rows(i)("Impact")) = False Then
                    dr("Impact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Impact"))
                End If
                dr("ImpactID") = dt.Rows(i)("ARAD_ImpactID")
                'Previous Year Impact
                If IsDBNull(dt.Rows(i)("PYImpact")) = False Then
                    dr("PYImpact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYImpact"))
                End If

                If IsDBNull(dt.Rows(i)("Likelihood")) = False Then
                    dr("Likelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Likelihood"))
                End If
                dr("LikelihoodID") = dt.Rows(i)("ARAD_LikelihoodID")
                'Previous Year Likelihood
                If IsDBNull(dt.Rows(i)("PYLikelihood")) = False Then
                    dr("PYLikelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYLikelihood"))
                End If

                dr("RiskRating") = "" : dr("RiskRatingColor") = ""
                If dt.Rows(i)("ARAD_RiskRating") > 0 Then
                    dr("RiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_RiskRating"), "GRS", "Name")
                Else
                    dr("RiskRating") = ""
                End If
                dr("RiskRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_RiskRating"), "GRS", "Color")
                'Previous Year RiskRating
                dr("PYRiskRating") = "" : dr("PYRiskRatingColor") = ""
                If IsDBNull(dr("PYImpact")) = False And IsDBNull(dr("PYLikelihood")) = False Then
                    If IsDBNull(dt.Rows(i)("PYRiskRating")) = False Then
                        If dt.Rows(i)("PYRiskRating") > 0 Then
                            dr("PYRiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYRiskRating"), "GRS", "Name")
                        Else
                            dr("PYRiskRating") = ""
                        End If
                        dr("PYRiskRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYRiskRating"), "GRS", "Color")
                    End If
                End If

                If IsDBNull(dt.Rows(i)("OE")) = False Then
                    dr("OE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("OE"))
                End If
                dr("OEID") = dt.Rows(i)("ARAD_OES")
                'Previous Year OESName
                If IsDBNull(dt.Rows(i)("PYOESName")) = False Then
                    dr("PYOESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYOESName"))
                End If

                If IsDBNull(dt.Rows(i)("DE")) = False Then
                    dr("DE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("DE"))
                End If
                dr("DEID") = dt.Rows(i)("ARAD_DES")
                'Previous Year DESName
                If IsDBNull(dt.Rows(i)("PYDESName")) = False Then
                    dr("PYDESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYDESName"))
                End If

                If dt.Rows(i)("ARAD_ControlRating") > 0 Then
                    dr("ControlRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_ControlRating"), "GCS", "Name")
                Else
                    dr("ControlRating") = ""
                End If
                dr("ControlRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_ControlRating"), "GCS", "Color")
                'Previous Year ControlRating
                If IsDBNull(dr("PYOESName")) = False And IsDBNull(dr("PYDESName")) = False Then
                    If IsDBNull(dt.Rows(i)("PYControlRating")) = False Then
                        If dt.Rows(i)("PYControlRating") > 0 Then
                            dr("PYControlRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYControlRating"), "GCS", "Name")
                        Else
                            dr("PYControlRating") = ""
                        End If
                        dr("PYControlRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYControlRating"), "GCS", "Color")
                    End If
                End If

                dr("ResidualRiskRating") = "" : dr("ResidualRiskRatingColor") = ""
                If dt.Rows(i)("ARAD_RiskRating") > 0 And dt.Rows(i)("ARAD_ControlRating") > 0 Then
                    If dt.Rows(i)("ARAD_ResidualRiskRating") >= 0 Then
                        dr("ResidualRiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_ResidualRiskRating"), "RRS", "Name")
                        dr("ResidualRiskRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_ResidualRiskRating"), "RRS", "Color")
                    Else
                        dr("ResidualRiskRating") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Name")
                        dr("ResidualRiskRatingColor") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                    End If
                End If
                'Previous Year ResidualRiskRating
                dr("PYResidualRiskRating") = "" : dr("PYResidualRiskRatingColor") = ""
                If IsDBNull(dr("PYControlRating")) = False And IsDBNull(dr("PYControlRating")) = False Then
                    If dt.Rows(i)("PYRiskRating") > 0 And dt.Rows(i)("PYControlRating") > 0 Then
                        If dt.Rows(i)("PYResidualRiskRating") >= 0 Then
                            dr("PYResidualRiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYResidualRiskRating"), "RRS", "Name")
                            dr("PYResidualRiskRatingColor") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYResidualRiskRating"), "RRS", "Color")
                        Else
                            dr("PYResidualRiskRating") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Name")
                            dr("PYResidualRiskRatingColor") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                        End If
                    End If
                End If
                dr("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ARAD_Remarks"))
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetFAARAComments(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String, sStr As String
        Try
            sSql = "Select ARA_Comments from Audit_ARA where ARA_AuditCodeID=" & iAuditCodeID & " And ARA_FunID=" & iFunID & " And ARA_CompID=" & iACID & " And ARA_FinancialYear=" & iYearID & ""
            sStr = objDBL.SQLExecuteScalar(sAC, sSql)
            sStr = objclsGRACeGeneral.ReplaceSafeSQL(sStr)
            Return sStr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFAARAStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iFunID As Integer) As String
        Dim sSql As String
        Try
            sSql = "Select ARA_Status from Audit_ARA where ARA_AuditCodeID=" & iAuditCodeID & " And ARA_FunID=" & iFunID & " And ARA_CompID=" & iACID & " And ARA_FinancialYear=" & iYearID & ""
            Return objDBL.SQLExecuteScalar(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFAAllIssueHeading(ByVal sAC As String, ByVal iACID As Integer, ByVal iAuditID As Integer, ByVal iFunID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer, ByVal iSubProcessID As Integer, ByVal iRisKID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select AIT_PKID,AIT_IssueName From Audit_IssueTracker_details Where AIT_CompID=" & iACID & " And AIT_FunctionID=" & iFunID & "  And AIT_SubFunctionID=" & iSubFunID & " And AIT_ProcessID= " & iProcessID & ""
            sSql = sSql & " And AIT_SubProcessID=" & iSubProcessID & " And  AIT_RiskID=" & iRisKID & " And AIT_ControlID=" & iControlID & " And AIT_CheckID=" & iChecksID & " And AIT_AuditCode=" & iAuditID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFAARAMaster(ByVal sAC As String, ByVal objFAARA As strFA_ARA) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(13) {}
        Dim iARAParamCount As Integer
        Dim Arr(1) As String
        Try
            iARAParamCount = 0
            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARAPKID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_AuditCodeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARAAuditCodeID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARAFinancialYear
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_FunID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARAFunID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARACustID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_NetScore", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARANetScore
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objFAARA.sARAStatus
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_Comments", OleDb.OleDbType.VarChar, 5000)
            ObjSFParam(iARAParamCount).Value = objFAARA.sARA_Comments
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_CrBy", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARACrBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_UpdatedBy", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARAUpdatedBy
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Value = objFAARA.iARACompID
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@ARA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARAParamCount).Value = objFAARA.sARAIPAddress
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Input
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            iARAParamCount += 1

            ObjSFParam(iARAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_ARA", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveFAARADetails(ByVal sAC As String, ByVal objFAARADetails As strFA_ARADetails) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(21) {}
        Dim iARADParamCount As Integer
        Dim Arr(1) As String
        Try
            iARADParamCount = 0
            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADPKID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_ARAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADARAPKID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_SEMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADSEMID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_PMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADPMID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_SPMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADSPMID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_IssueHeading", OleDb.OleDbType.VarChar, 500)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.sARAD_IssueHeading
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_RiskID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADRiskID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_RiskTypeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADRiskTypeID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_ImpactID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADImpactID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_LikelihoodID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADLikelihoodID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_RiskRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADRiskRating
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_ControlID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADControlID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_OES", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADOES
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_DES", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADDES
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_ControlRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADControlRating
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADChecksID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_ResidualRiskRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADResidualRiskRating
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_Remarks", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.sARADRemarks
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.sARADIPAddress
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@ARAD_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iARADParamCount).Value = objFAARADetails.iARADCompID
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Input
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Output
            iARADParamCount += 1

            ObjSFParam(iARADParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iARADParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"
            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spAudit_ARA_Details", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteFAARADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSCID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Audit_ARA_Details Where ARAD_ARAPKID=" & iRCSCID & " And ARAD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SubmitFAARADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iARAPKID As Integer)
        Dim sSql As String = ""
        Dim dNetScore As Double
        Try
            dNetScore = Math.Round(GeFAARAOverAllScore(sAC, iACID, iARAPKID), 2)
            sSql = "Update Audit_ARA Set ARA_NetScore=" & dNetScore & ",ARA_Status='Submited',ARA_SubmittedBy=" & iUserID & ","
            sSql = sSql & " ARA_SubmittedOn=GetDate() Where ARA_PKID=" & iARAPKID & " And ARA_FinancialYear=" & iYearID & " And ARA_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GeFAARAOverAllScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iARAPKID As Integer) As Object
        Dim sSql As String, dOverAllScore As Double
        Dim iCount As Integer, iSumOfARA As Integer
        Try
            sSql = "Select Sum(ARAD_ResidualRiskRating) From Audit_ARA_Details Where ARAD_ARAPKID=" & iARAPKID & " And ARAD_CompID=" & iACID & ""
            iSumOfARA = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "Select Count(*) From Audit_ARA_Details Where ARAD_ARAPKID=" & iARAPKID & " And ARAD_CompID=" & iACID & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)

            dOverAllScore = iSumOfARA / iCount
            Return dOverAllScore
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadFAARAReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String, sIssueHeading As String = ""
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("Issue")
            dtTab.Columns.Add("SubFunction")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("SubProcess")
            dtTab.Columns.Add("SubProcessKey")
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("RiskType")
            dtTab.Columns.Add("RiskKey")
            dtTab.Columns.Add("Control")
            dtTab.Columns.Add("ControlKey")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("ChecksKey")
            dtTab.Columns.Add("Impact")
            dtTab.Columns.Add("Likelihood")
            dtTab.Columns.Add("RiskRating")
            dtTab.Columns.Add("OE")
            dtTab.Columns.Add("DE")
            dtTab.Columns.Add("ControlRating")
            dtTab.Columns.Add("ResidualRiskRating")
            dtTab.Columns.Add("Remarks")

            sSql = "Select ARAD_IssueHeading,ARAD_RiskRating,ARAD_ControlRating,ARAD_ResidualRiskRating,ARAD_Remarks,SEM_NAME as SubFunction,PM_NAME as Process,SPM_NAME as SubProcess,"
            sSql = sSql & " MRL_RiskName as Risk,a.RAM_Name as RiskType,MCL_ControlName as Controls,CHK_CheckName as Checks,"
            sSql = sSql & " (b.RAM_Name + ' - ' + Convert(Varchar(10),b.RAM_Score)) As Impact,"
            sSql = sSql & " (c.RAM_Name + ' - ' + Convert(Varchar(10),c.RAM_Score)) As Likelihood, "
            sSql = sSql & " (d.RAM_Name + ' - ' + Convert(Varchar(10),d.RAM_Score)) As OE,"
            sSql = sSql & " (e.RAM_Name + ' - ' + Convert(Varchar(10),e.RAM_Score)) As DE"
            sSql = sSql & " From Audit_ARA_Details"
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_Ent_ID=" & iFunctionID & "  And SEM_ID=ARAD_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ENT_ID=" & iFunctionID & " And PM_ID=ARAD_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ENT_ID=" & iFunctionID & "  And SPM_PM_ID=ARAD_PMID"
            sSql = sSql & " And SPM_ID=ARAD_SPMID And SPM_CompID=" & iACID & " Left Join MST_RISK_Library On MRL_PKID=ARAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster a On a.RAM_PKID=ARAD_RiskTypeID And a.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_CONTROL_Library On MCL_PKID=ARAD_ControlID And MCL_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Checks_Master On CHK_ControlID=ARAD_ControlID And CHK_ID=ARAD_ChecksID And CHK_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster b On b.RAM_Delflag='A' And b.RAM_Category='RI' And b.RAM_YearID=17 And b.RAM_PKID=ARAD_ImpactID And b.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster c On c.RAM_Delflag='A' And c.RAM_Category='RL' And c.RAM_YearID=17 And c.RAM_PKID=ARAD_LikelihoodID And c.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster d On d.RAM_Delflag='A' And d.RAM_Category='OES' And d.RAM_YearID=17 And d.RAM_PKID=ARAD_OES And d.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster e On e.RAM_Delflag='A' And e.RAM_Category='DES' And e.RAM_YearID=17 And e.RAM_PKID=ARAD_DES And e.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where ARAD_ARAPKID in (Select ARA_PKID From Audit_ARA Where ARA_AuditCodeID=" & iAuditID & " And ARA_FinancialYear=" & iYearID & ""
            sSql = sSql & " And ARA_FunID=" & iFunctionID & " and ARA_CompID=" & iACID & ") And ARAD_CompID =" & iACID & ""
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                If IsDBNull(dt.Rows(i)("SubFunction")) = False Then
                    dr("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SubFunction"))
                End If

                If IsDBNull(dt.Rows(i)("Process")) = False Then
                    dr("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Process"))
                End If

                If IsDBNull(dt.Rows(i)("SubProcess")) = False Then
                    dr("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SubProcess"))
                End If

                If IsDBNull(dt.Rows(i)("Risk")) = False Then
                    dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Risk"))
                End If
                If IsDBNull(dt.Rows(i)("RiskType")) = False Then
                    dr("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RiskType"))
                End If

                If IsDBNull(dt.Rows(i)("Controls")) = False Then
                    dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Controls"))
                End If

                If IsDBNull(dt.Rows(i)("Checks")) = False Then
                    dr("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Checks"))
                End If

                If IsDBNull(dt.Rows(i)("Impact")) = False Then
                    dr("Impact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Impact"))
                End If

                If IsDBNull(dt.Rows(i)("Likelihood")) = False Then
                    dr("Likelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Likelihood"))
                End If

                dr("RiskRating") = ""

                If dt.Rows(i)("ARAD_RiskRating") > 0 Then
                    dr("RiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_RiskRating"), "GRS", "Name")
                Else
                    dr("RiskRating") = ""
                End If
                If IsDBNull(dt.Rows(i)("OE")) = False Then
                    dr("OE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("OE"))
                End If

                If IsDBNull(dt.Rows(i)("DE")) = False Then
                    dr("DE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("DE"))
                End If

                If dt.Rows(i)("ARAD_ControlRating") > 0 Then
                    dr("ControlRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_ControlRating"), "GCS", "Name")
                Else
                    dr("ControlRating") = ""
                End If

                dr("ResidualRiskRating") = ""
                If dt.Rows(i)("ARAD_RiskRating") > 0 And dt.Rows(i)("ARAD_ControlRating") > 0 Then
                    If dt.Rows(i)("ARAD_ResidualRiskRating") >= 0 Then
                        dr("ResidualRiskRating") = objclsAuditGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("ARAD_ResidualRiskRating"), "RRS", "Name")
                    Else
                        dr("ResidualRiskRating") = objclsAuditGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Name")
                    End If
                End If

                dr("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("ARAD_Remarks"))

                If IsDBNull(dt.Rows(i)("ARAD_IssueHeading")) = False Then
                    If dt.Rows(i)("ARAD_IssueHeading") > 0 Then
                        sIssueHeading = objDBL.SQLExecuteScalar(sAC, "Select AIT_IssueHeading from Audit_IssueTracker_details where AIT_PKID=" & dt.Rows(i)("ARAD_IssueHeading") & " and AIT_CompID= " & iACID & "")
                        dr("Issue") = objclsGRACeGeneral.ReplaceSafeSQL(sIssueHeading)
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Function LoadFAARANetScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ARA_NetScore from Audit_ARA where ARA_AuditCodeID=" & iAuditCodeID & " And ARA_FunID=" & iFunID & "  And ARA_CompID=" & iACID & " And ARA_FinancialYear=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetFAARAID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iAuditCodeID As Integer, ByVal iFunID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select ARA_PKID from Audit_ARA where ARA_AuditCodeID=" & iAuditCodeID & " and ARA_FunID=" & iFunID & "  and ARA_CompID=" & iACID & " and ARA_FinancialYear=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
