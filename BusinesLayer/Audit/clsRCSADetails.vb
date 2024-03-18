Imports DatabaseLayer
Public Structure strRCSA_Assess
    Dim iRCSA_PKID As Integer
    Dim iRCSA_FinancialYear As Integer
    Dim iRCSA_CustID As Integer
    Dim iRCSA_FunID As Integer
    Dim iRCSA_OwnerID As Integer
    Dim dRCSA_TargetDate As Date
    Dim sRCSA_ActionPlan As String
    Dim sRCSA_FactorIncrease As String
    Dim sRCSA_FactorDecrease As String
    Dim sRCSA_Comments As String
    Dim iRCSA_CrBy As Integer
    Dim iRCSA_RUpdatedBy As Integer
    Dim iRCSA_RSubmittedBy As Integer
    Dim iRCSA_BUpdatedBy As Integer
    Dim iRCSA_BSubmittedBy As Integer
    Dim iRCSA_ReAssignBy As Integer
    Dim iRCSA_ApproveBy As Integer
    Dim sRCSA_Status As String
    Dim sRCSA_IPAddress As String
    Dim iRCSA_CompID As Integer

    Dim iRAH_PKID As Integer
    Dim iRAH_RCSAPKID As Integer
    Dim iRAH_CustID As Integer
    Dim iRAH_FUNID As Integer
    Dim iRAH_FinancialYear As Integer
    Dim sRAH_FactorIncrease As String
    Dim sRAH_FactorDecrease As String
    Dim sRAH_ActionPlan As String
    Dim dRAH_TargetDate As Date
    Dim iRAH_CrBy As Integer
    Dim iRAH_CompID As Integer

    Public Property iRCSAPKID() As Integer
        Get
            Return (iRCSA_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_PKID = Value
        End Set
    End Property
    Public Property iRCSAFinancialYear() As Integer
        Get
            Return (iRCSA_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_FinancialYear = Value
        End Set
    End Property
    Public Property iRCSACustID() As Integer
        Get
            Return (iRCSA_CustID)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_CustID = Value
        End Set
    End Property
    Public Property iRCSAFunID() As Integer
        Get
            Return (iRCSA_FunID)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_FunID = Value
        End Set
    End Property
    Public Property iRCSAOwnerID() As Integer
        Get
            Return (iRCSA_OwnerID)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_OwnerID = Value
        End Set
    End Property
    Public Property dRCSAAssignTargetDate() As Date
        Get
            Return (dRCSA_TargetDate)
        End Get
        Set(ByVal Value As Date)
            dRCSA_TargetDate = Value
        End Set
    End Property
    Public Property sRCSAActionPlan() As String
        Get
            Return (sRCSA_ActionPlan)
        End Get
        Set(ByVal Value As String)
            sRCSA_ActionPlan = Value
        End Set
    End Property
    Public Property sRCSAFactorIncrease() As String
        Get
            Return (sRCSA_FactorIncrease)
        End Get
        Set(ByVal Value As String)
            sRCSA_FactorIncrease = Value
        End Set
    End Property
    Public Property sRCSAFactorDecrease() As String
        Get
            Return (sRCSA_FactorDecrease)
        End Get
        Set(ByVal Value As String)
            sRCSA_FactorDecrease = Value
        End Set
    End Property
    Public Property sRCSAComments() As String
        Get
            Return (sRCSA_Comments)
        End Get
        Set(ByVal Value As String)
            sRCSA_Comments = Value
        End Set
    End Property
    Public Property iRCSACrBy() As Integer
        Get
            Return (iRCSA_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_CrBy = Value
        End Set
    End Property
    Public Property iRCSARUpdatedBy() As Integer
        Get
            Return (iRCSA_RUpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_RUpdatedBy = Value
        End Set
    End Property
    Public Property iRCSARSubmittedBy() As Integer
        Get
            Return (iRCSA_RSubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_RSubmittedBy = Value
        End Set
    End Property
    Public Property iRCSABUpdatedBy() As Integer
        Get
            Return (iRCSA_BUpdatedBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_BUpdatedBy = Value
        End Set
    End Property
    Public Property iRCSABSubmittedBy() As Integer
        Get
            Return (iRCSA_BSubmittedBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_BSubmittedBy = Value
        End Set
    End Property
    Public Property iRCSAReAssignBy() As Integer
        Get
            Return (iRCSA_ReAssignBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_ReAssignBy = Value
        End Set
    End Property
    Public Property iRCSAApproveBy() As Integer
        Get
            Return (iRCSA_ApproveBy)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_ApproveBy = Value
        End Set
    End Property
    Public Property sRCSAStatus() As String
        Get
            Return (sRCSA_Status)
        End Get
        Set(ByVal Value As String)
            sRCSA_Status = Value
        End Set
    End Property
    Public Property sRCSAIPAddress() As String
        Get
            Return (sRCSA_IPAddress)
        End Get
        Set(ByVal Value As String)
            sRCSA_IPAddress = Value
        End Set
    End Property
    Public Property iRCSACompID() As Integer
        Get
            Return (iRCSA_CompID)
        End Get
        Set(ByVal Value As Integer)
            iRCSA_CompID = Value
        End Set
    End Property
    Public Property iRAHPKID() As Integer
        Get
            Return (iRAH_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_PKID = Value
        End Set
    End Property
    Public Property iRAHRCSAPKID() As Integer
        Get
            Return (iRAH_RCSAPKID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_RCSAPKID = Value
        End Set
    End Property
    Public Property iRAHCustID() As Integer
        Get
            Return (iRAH_CustID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_CustID = Value
        End Set
    End Property
    Public Property iRAHFunID() As Integer
        Get
            Return (iRAH_FUNID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_FUNID = Value
        End Set
    End Property
    Public Property iRAHFinancialYear() As Integer
        Get
            Return (iRAH_FinancialYear)
        End Get
        Set(ByVal Value As Integer)
            iRAH_FinancialYear = Value
        End Set
    End Property
    Public Property sRAHFactorIncrease() As String
        Get
            Return (sRAH_FactorIncrease)
        End Get
        Set(ByVal Value As String)
            sRAH_FactorIncrease = Value
        End Set
    End Property
    Public Property sRAHFactorDecrease() As String
        Get
            Return (sRAH_FactorDecrease)
        End Get
        Set(ByVal Value As String)
            sRAH_FactorDecrease = Value
        End Set
    End Property
    Public Property sRAHActionPlan() As String
        Get
            Return (sRAH_ActionPlan)
        End Get
        Set(ByVal Value As String)
            sRAH_ActionPlan = Value
        End Set
    End Property
    Public Property dRAHTargetDate() As Date
        Get
            Return (dRAH_TargetDate)
        End Get
        Set(ByVal Value As Date)
            dRAH_TargetDate = Value
        End Set
    End Property
    Public Property iRAHCrBy() As Integer
        Get
            Return (iRAH_CrBy)
        End Get
        Set(ByVal Value As Integer)
            iRAH_CrBy = Value
        End Set
    End Property
    Public Property iRAHCompID() As Integer
        Get
            Return (iRAH_CompID)
        End Get
        Set(ByVal Value As Integer)
            iRAH_CompID = Value
        End Set
    End Property
End Structure
Public Structure strRCSA_AssessDetails
    Dim iRCSAD_PKID As Integer
    Dim iRCSAD_RCSAPKID As Integer
    Dim iRCSAD_SEMID As Integer
    Dim iRCSAD_PMID As Integer
    Dim iRCSAD_SPMID As Integer
    Dim iRCSAD_RiskID As Integer
    Dim iRCSAD_RiskTypeID As Integer
    Dim iRCSAD_ImpactID As Integer
    Dim iRCSAD_LikelihoodID As Integer
    Dim iRCSAD_RiskRating As Integer
    Dim iRCSAD_ControlID As Integer
    Dim iRCSAD_OES As Integer
    Dim iRCSAD_DES As Integer
    Dim iRCSAD_ControlRating As Integer
    Dim iRCSAD_ChecksID As Integer
    Dim iRCSAD_ResidualRiskRating As Integer
    Dim sRCSAD_Remarks As String
    Dim sRCSAD_IPAddress As String
    Dim iRCSAD_CompID As Integer
    Public Property iRCSADPKID() As Integer
        Get
            Return (iRCSAD_PKID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_PKID = Value
        End Set
    End Property
    Public Property iRCSADRCSAPKID() As Integer
        Get
            Return (iRCSAD_RCSAPKID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_RCSAPKID = Value
        End Set
    End Property
    Public Property iRCSADSEMID() As Integer
        Get
            Return (iRCSAD_SEMID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_SEMID = Value
        End Set
    End Property
    Public Property iRCSADPMID() As Integer
        Get
            Return (iRCSAD_PMID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_PMID = Value
        End Set
    End Property
    Public Property iRCSADSPMID() As Integer
        Get
            Return (iRCSAD_SPMID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_SPMID = Value
        End Set
    End Property
    Public Property iRCSADRiskID() As Integer
        Get
            Return (iRCSAD_RiskID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_RiskID = Value
        End Set
    End Property
    Public Property iRCSADRiskTypeID() As Integer
        Get
            Return (iRCSAD_RiskTypeID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_RiskTypeID = Value
        End Set
    End Property
    Public Property iRCSADImpactID() As Integer
        Get
            Return (iRCSAD_ImpactID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_ImpactID = Value
        End Set
    End Property
    Public Property iRCSADLikelihoodID() As Integer
        Get
            Return (iRCSAD_LikelihoodID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_LikelihoodID = Value
        End Set
    End Property
    Public Property iRCSADRiskRating() As Double
        Get
            Return (iRCSAD_RiskRating)
        End Get
        Set(ByVal Value As Double)
            iRCSAD_RiskRating = Value
        End Set
    End Property
    Public Property iRCSADControlID() As Integer
        Get
            Return (iRCSAD_ControlID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_ControlID = Value
        End Set
    End Property
    Public Property iRCSADOES() As Double
        Get
            Return (iRCSAD_OES)
        End Get
        Set(ByVal Value As Double)
            iRCSAD_OES = Value
        End Set
    End Property
    Public Property iRCSADDES() As Double
        Get
            Return (iRCSAD_DES)
        End Get
        Set(ByVal Value As Double)
            iRCSAD_DES = Value
        End Set
    End Property
    Public Property iRCSADControlRating() As Double
        Get
            Return (iRCSAD_ControlRating)
        End Get
        Set(ByVal Value As Double)
            iRCSAD_ControlRating = Value
        End Set
    End Property
    Public Property iRCSADChecksID() As Integer
        Get
            Return (iRCSAD_ChecksID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_ChecksID = Value
        End Set
    End Property
    Public Property iRCSADResidualRiskRating() As Double
        Get
            Return (iRCSAD_ResidualRiskRating)
        End Get
        Set(ByVal Value As Double)
            iRCSAD_ResidualRiskRating = Value
        End Set
    End Property
    Public Property sRCSADIPAddress() As String
        Get
            Return (sRCSAD_IPAddress)
        End Get
        Set(ByVal Value As String)
            sRCSAD_IPAddress = Value
        End Set
    End Property
    Public Property sRCSADRemarks() As String
        Get
            Return (sRCSAD_Remarks)
        End Get
        Set(ByVal Value As String)
            sRCSAD_Remarks = Value
        End Set
    End Property
    Public Property iRCSADCompID() As Integer
        Get
            Return (iRCSAD_CompID)
        End Get
        Set(ByVal Value As Integer)
            iRCSAD_CompID = Value
        End Set
    End Property
End Structure
Public Class clsRCSADetails
    Private objDBL As New DBHelper
    Private objclsGeneralFunctions As New clsGeneralFunctions
    Private objclsGRACeGeneral As New clsGRACeGeneral
    Private objclsAllActiveMaster As New clsAllActiveMaster
    Private objclsRiskGeneral As New clsRiskGeneral
    Public Function LoadRCSADashboardDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim sSql As String
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Function")
            dt.Columns.Add("RCSAAssigned")
            dt.Columns.Add("RCSAReassigned")
            dt.Columns.Add("PendingAtRisk")
            dt.Columns.Add("RCSACompleted")
            dt.Columns.Add("Status")
            dt.Columns.Add("NetResidualRiskScoreCY")
            dt.Columns.Add("NetResidualRiskRatingCY")
            dt.Columns.Add("NetResidualRiskColorCY")
            dt.Columns.Add("NetResidualRiskScorePY")
            dt.Columns.Add("NetResidualRiskRatingPY")
            dt.Columns.Add("NetResidualRiskColorPY")

            sSql = "Select Ent_ID,Ent_EntityName,a.RCSA_Status,a.RCSA_NetScore As CurrentYearNetScore,b.RCSA_NetScore As PreviousYearNetScore,"
            sSql = sSql & " a.RCSA_RSubmittedOn As AssignedDate,a.RCSA_ReAssignOn As ReAssignedDate,a.RCSA_BSubmittedOn As PendingatRiskDate,a.RCSA_ApprovedOn As CompletedDate From MST_Entity_Master"
            sSql = sSql & " Left join Risk_RCSA a On a.RCSA_CustID=" & iCustID & " And a.RCSA_FunID=Ent_ID And a.RCSA_FinancialYear=" & iYearID & " And a.RCSA_CompID=" & iACID & ""
            sSql = sSql & " Left join Risk_RCSA b On b.RCSA_CustID=" & iCustID & " And b.RCSA_FunID=Ent_ID And b.RCSA_FinancialYear=" & iYearID - 1 & " And b.RCSA_CompID=" & iACID & ""
            sSql = sSql & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & " "
            sSql = sSql & " order by Ent_EntityName"
            dtdetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("Function") = dtdetails.Rows(i)("Ent_EntityName")
                    If IsDBNull(dtdetails.Rows(i)("AssignedDate")) = False Then
                        dRow("RCSAAssigned") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("AssignedDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("ReAssignedDate")) = False Then
                        dRow("RCSAReassigned") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("ReAssignedDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("PendingatRiskDate")) = False Then
                        dRow("PendingAtRisk") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("PendingatRiskDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("CompletedDate")) = False Then
                        dRow("RCSACompleted") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("CompletedDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("RCSA_Status")) = False Then
                        If dtdetails.Rows(i)("RCSA_Status") = "Approved" Then
                            dRow("Status") = "Completed"
                        ElseIf dtdetails.Rows(i)("RCSA_Status") <> "Saved(Risk Team)" Then
                            dRow("Status") = "In Progress"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("CurrentYearNetScore")) = False Then
                        If (dtdetails.Rows(i)("RCSA_Status") = "Submitted(Business Team)") Or (dtdetails.Rows(i)("RCSA_Status") = "Re-Assigned") Or (dtdetails.Rows(i)("RCSA_Status") = "Approved") Then
                            dRow("NetResidualRiskScoreCY") = dtdetails.Rows(i)("CurrentYearNetScore")
                            If dtdetails.Rows(i)("CurrentYearNetScore") > 0 Then
                                dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Desc")
                                dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Color")
                            Else
                                dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                                dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                            End If
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("PreviousYearNetScore")) = False Then
                        dRow("NetResidualRiskScorePY") = dtdetails.Rows(i)("PreviousYearNetScore")
                        If dtdetails.Rows(i)("PreviousYearNetScore") > 0 Then
                            dRow("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dtdetails.Rows(i)("PreviousYearNetScore"), "Desc")
                            dRow("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dtdetails.Rows(i)("PreviousYearNetScore"), "Color")
                        Else
                            dRow("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                            dRow("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRCSAReportDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim dt As New DataTable, dtdetails As New DataTable
        Dim sSql As String, sStrCurrentYear As String, sStrPreviousYear As String
        Dim dRow As DataRow
        Try
            dt.Columns.Add("SrNo")
            dt.Columns.Add("Function")
            dt.Columns.Add("RCSAAssigned")
            dt.Columns.Add("RCSAReassigned")
            dt.Columns.Add("PendingAtRisk")
            dt.Columns.Add("RCSACompleted")
            dt.Columns.Add("Status")
            dt.Columns.Add("NetResidualRiskScoreCY")
            dt.Columns.Add("NetResidualRiskColorCY")
            dt.Columns.Add("NetResidualRiskScorePY")
            dt.Columns.Add("NetResidualRiskRatingPY")
            dt.Columns.Add("NetResidualRiskColorPY")
            dt.Columns.Add("NetResidualRiskRatingCY")
            dt.Columns.Add("NetRiskCYScoreYear")
            dt.Columns.Add("NetRiskPYScoreYear")
            dt.Columns.Add("NetRiskCYRatingYear")

            sSql = "Select Ent_ID,Ent_EntityName,a.RCSA_Status,a.RCSA_NetScore As CurrentYearNetScore,b.RCSA_NetScore As PreviousYearNetScore,"
            sSql = sSql & " a.RCSA_RSubmittedOn As AssignedDate,a.RCSA_ReAssignOn As ReAssignedDate,a.RCSA_BSubmittedOn As PendingatRiskDate,a.RCSA_ApprovedOn As CompletedDate From MST_Entity_Master"
            sSql = sSql & " Left join Risk_RCSA a On a.RCSA_CustID=" & iCustID & " And a.RCSA_FunID=Ent_ID And a.RCSA_FinancialYear=" & iYearID & " And a.RCSA_CompID=" & iACID & ""
            sSql = sSql & " Left join Risk_RCSA b On b.RCSA_CustID=" & iCustID & " And b.RCSA_FunID=Ent_ID And b.RCSA_FinancialYear=" & iYearID - 1 & " And b.RCSA_CompID=" & iACID & ""
            sSql = sSql & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & " "
            sSql = sSql & " order by Ent_EntityName"
            dtdetails = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
            sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
            If dtdetails.Rows.Count > 0 Then
                For i = 0 To dtdetails.Rows.Count - 1
                    dRow = dt.NewRow
                    dRow("SrNo") = i + 1
                    dRow("NetRiskCYScoreYear") = "FY " & sStrCurrentYear & ""
                    dRow("NetRiskCYRatingYear") = "Net Residual Risk Rating FY " & sStrCurrentYear & ""
                    dRow("NetRiskPYScoreYear") = "FY " & sStrPreviousYear & ""
                    dRow("Function") = dtdetails.Rows(i)("Ent_EntityName")
                    If IsDBNull(dtdetails.Rows(i)("AssignedDate")) = False Then
                        dRow("RCSAAssigned") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("AssignedDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("ReAssignedDate")) = False Then
                        dRow("RCSAReassigned") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("ReAssignedDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("PendingatRiskDate")) = False Then
                        dRow("PendingAtRisk") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("PendingatRiskDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("CompletedDate")) = False Then
                        dRow("RCSACompleted") = objclsGRACeGeneral.FormatDtForRDBMS(dtdetails.Rows(i)("CompletedDate"), "F")
                    End If
                    If IsDBNull(dtdetails.Rows(i)("RCSA_Status")) = False Then
                        If dtdetails.Rows(i)("RCSA_Status") = "Approved" Then
                            dRow("Status") = "Completed"
                        ElseIf dtdetails.Rows(i)("RCSA_Status") <> "Saved(Risk Team)" Then
                            dRow("Status") = "In Progress"
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("CurrentYearNetScore")) = False Then
                        If (dtdetails.Rows(i)("RCSA_Status") = "Submitted(Business Team)") Or (dtdetails.Rows(i)("RCSA_Status") = "Re-Assigned") Or (dtdetails.Rows(i)("RCSA_Status") = "Approved") Then
                            dRow("NetResidualRiskScoreCY") = dtdetails.Rows(i)("CurrentYearNetScore")

                            If dtdetails.Rows(i)("CurrentYearNetScore") > 0 Then
                                dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Desc")
                                dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dtdetails.Rows(i)("CurrentYearNetScore"), "Color")
                            Else
                                dRow("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                                dRow("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                            End If
                        End If
                    End If
                    If IsDBNull(dtdetails.Rows(i)("PreviousYearNetScore")) = False Then
                        dRow("NetResidualRiskScorePY") = dtdetails.Rows(i)("PreviousYearNetScore")
                        If dtdetails.Rows(i)("PreviousYearNetScore") > 0 Then
                            dRow("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dtdetails.Rows(i)("PreviousYearNetScore"), "Desc")
                            dRow("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dtdetails.Rows(i)("PreviousYearNetScore"), "Color")
                        Else
                            dRow("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                            dRow("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                        End If
                    End If
                    dt.Rows.Add(dRow)
                Next
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRCSASummarySheet(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("NetResidualRiskScoreCY")
            dtTab.Columns.Add("NetResidualRiskRatingCY")
            dtTab.Columns.Add("NetResidualRiskColorCY")
            dtTab.Columns.Add("Status")
            dtTab.Columns.Add("NetResidualRiskScorePY")
            dtTab.Columns.Add("NetResidualRiskRatingPY")
            dtTab.Columns.Add("NetResidualRiskColorPY")

            sSql = "Select Ent_ID,Ent_EntityName,a.RCSA_Status,a.RCSA_NetScore As CurrentYearNetScore,b.RCSA_NetScore As PreviousYearNetScore From MST_Entity_Master "
            sSql = sSql & " Left join Risk_RCSA a On a.RCSA_FunID=Ent_ID And a.RCSA_FinancialYear=" & iYearID & " And a.RCSA_CompID=" & iACID & ""
            sSql = sSql & " And a.RCSA_CustID = " & iCustID & " And (a.RCSA_Status<>'Saved(Risk Team)' or a.RCSA_Status<>'Submitted(Risk Team)')"
            sSql = sSql & " Left join Risk_RCSA b On b.RCSA_FunID=Ent_ID And b.RCSA_FinancialYear=" & iYearID - 1 & " And b.RCSA_CompID=" & iACID & ""
            sSql = sSql & " And b.RCSA_CustID = " & iCustID & " And (b.RCSA_Status<>'Saved(Risk Team)' or b.RCSA_Status<>'Submitted(Risk Team)')"
            sSql = sSql & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & " "
            sSql = sSql & " order by Ent_EntityName"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("Function") = dt.Rows(i)("Ent_EntityName")
                dr("NetResidualRiskScorePY") = dt.Rows(i)("PreviousYearNetScore")
                dr("NetResidualRiskRatingPY") = dt.Rows(i)("PreviousYearNetScore")
                If IsDBNull(dt.Rows(i)("RCSA_Status")) = False Then
                    If (dt.Rows(i)("RCSA_Status") = "Submitted(Business Team)") Or (dt.Rows(i)("RCSA_Status") = "Re-Assigned") Or (dt.Rows(i)("RCSA_Status") = "Approved") Then
                        If IsDBNull(dt.Rows(i)("CurrentYearNetScore")) = False Then
                            dr("NetResidualRiskScoreCY") = dt.Rows(i)("CurrentYearNetScore")
                            If dt.Rows(i)("CurrentYearNetScore") > 0 Then
                                dr("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("CurrentYearNetScore"), "Desc")
                                dr("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("CurrentYearNetScore"), "Color")
                            Else
                                dr("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                                dr("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                            End If
                        End If
                    End If
                End If

                If IsDBNull(dt.Rows(i)("RCSA_Status")) = False Then
                    If (dt.Rows(i)("RCSA_Status") = "Submitted(Risk Team)") Then
                        dr("Status") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Saved(Business Team)" Then
                        dr("Status") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Submitted(Business Team)" Then
                        dr("Status") = "Pending at Risk Team"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Re-Assigned" Then
                        dr("Status") = "Re-Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Approved" Then
                        dr("Status") = "Completed"
                    End If
                End If

                If IsDBNull(dt.Rows(i)("PreviousYearNetScore")) = False Then
                    dr("NetResidualRiskScorePY") = dt.Rows(i)("PreviousYearNetScore")
                    If dt.Rows(i)("PreviousYearNetScore") > 0 Then
                        dr("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("PreviousYearNetScore"), "Desc")
                        dr("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("PreviousYearNetScore"), "Color")
                    Else
                        dr("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                        dr("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRCSASummarySheetReport(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer) As DataTable
        Dim sSql As String, sStrCurrentYear As String, sStrPreviousYear As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("NetResidualRiskScoreCY")
            dtTab.Columns.Add("NetResidualRiskRatingCY")
            dtTab.Columns.Add("NetResidualRiskColorCY")
            dtTab.Columns.Add("Status")
            dtTab.Columns.Add("NetResidualRiskScorePY")
            dtTab.Columns.Add("NetResidualRiskRatingPY")
            dtTab.Columns.Add("NetResidualRiskColorPY")
            dtTab.Columns.Add("NetRiskCY")
            dtTab.Columns.Add("NetRiskPY")
            sSql = "Select Ent_ID,Ent_EntityName,a.RCSA_Status,a.RCSA_NetScore As CurrentYearNetScore,b.RCSA_NetScore As PreviousYearNetScore From MST_Entity_Master "
            sSql = sSql & " Left join Risk_RCSA a On a.RCSA_FunID=Ent_ID And a.RCSA_FinancialYear=" & iYearID & " And a.RCSA_CompID=" & iACID & ""
            sSql = sSql & " And a.RCSA_CustID=" & iCustID & " And (a.RCSA_Status<>'Saved(Risk Team)' or a.RCSA_Status<>'Submitted(Risk Team)')"
            sSql = sSql & " Left join Risk_RCSA b On b.RCSA_FunID=Ent_ID And b.RCSA_FinancialYear=" & iYearID - 1 & " And b.RCSA_CompID=" & iACID & ""
            sSql = sSql & " And b.RCSA_CustID=" & iCustID & " And (b.RCSA_Status<>'Saved(Risk Team)' or b.RCSA_Status<>'Submitted(Risk Team)')"
            sSql = sSql & " Where ENT_Branch='F' And ENT_Delflg='A' And Ent_CompID=" & iACID & " "
            sSql = sSql & " order by Ent_EntityName"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            sStrCurrentYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID))
            sStrPreviousYear = objclsGeneralFunctions.Get2DigitFinancialYearName(sAC, iACID, (iYearID) - 1)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("NetRiskCY") = "FY " & sStrCurrentYear & ""
                dr("NetRiskPY") = "FY " & sStrPreviousYear & ""
                dr("Function") = dt.Rows(i)("Ent_EntityName")
                dr("NetResidualRiskScorePY") = dt.Rows(i)("PreviousYearNetScore")
                dr("NetResidualRiskRatingPY") = dt.Rows(i)("PreviousYearNetScore")
                If IsDBNull(dt.Rows(i)("RCSA_Status")) = False Then
                    If (dt.Rows(i)("RCSA_Status") = "Submitted(Business Team)") Or (dt.Rows(i)("RCSA_Status") = "Re-Assigned") Or (dt.Rows(i)("RCSA_Status") = "Approved") Then
                        If IsDBNull(dt.Rows(i)("CurrentYearNetScore")) = False Then
                            dr("NetResidualRiskScoreCY") = dt.Rows(i)("CurrentYearNetScore")
                            If dt.Rows(i)("CurrentYearNetScore") > 0 Then
                                dr("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("CurrentYearNetScore"), "Desc")
                                dr("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", dt.Rows(i)("CurrentYearNetScore"), "Color")
                            Else
                                dr("NetResidualRiskRatingCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Desc")
                                dr("NetResidualRiskColorCY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                            End If
                        End If
                    End If
                End If
                If IsDBNull(dt.Rows(i)("RCSA_Status")) = False Then
                    If (dt.Rows(i)("RCSA_Status") = "Submitted(Risk Team)") Then
                        dr("Status") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Saved(Business Team)" Then
                        dr("Status") = "Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Submitted(Business Team)" Then
                        dr("Status") = "Pending at Risk Team"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Re-Assigned" Then
                        dr("Status") = "Re-Assigned"
                    ElseIf dt.Rows(i)("RCSA_Status") = "Approved" Then
                        dr("Status") = "Completed"
                    End If
                End If
                If IsDBNull(dt.Rows(i)("PreviousYearNetScore")) = False Then
                    dr("NetResidualRiskScorePY") = dt.Rows(i)("PreviousYearNetScore")
                    If dt.Rows(i)("PreviousYearNetScore") > 0 Then
                        dr("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("PreviousYearNetScore"), "Desc")
                        dr("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", dt.Rows(i)("PreviousYearNetScore"), "Color")
                    Else
                        dr("NetResidualRiskRatingPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Desc")
                        dr("NetResidualRiskColorPY") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                    End If
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRCSASubmittedSubFunction(ByVal sAC As String, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iEntID As Integer) As DataTable
        Dim dtTab As New DataTable, dt As New DataTable
        Dim sSql As String, sSubFunID As String, sSubFunIDs As String = ""
        Try
            sSubFunID = "Select Distinct(RCSAD_SEMID) from Risk_RCSA_Details where RCSAD_RCSAPKID in (Select RCSA_PKID from Risk_RCSA where RCSA_CustID=" & iCustID & " And RCSA_FunID=" & iEntID & ")"
            dtTab = objDBL.SQLExecuteDataSet(sAC, sSubFunID).Tables(0)
            For i = 0 To dtTab.Rows.Count - 1
                sSubFunIDs = sSubFunIDs & "," & dtTab.Rows(i)("RCSAD_SEMID")
            Next
            If sSubFunIDs.StartsWith(",") = True Then
                sSubFunIDs = sSubFunIDs.Remove(0, 1)
            End If
            If sSubFunIDs.EndsWith(",") = True Then
                sSubFunIDs = sSubFunIDs.Remove(Len(sSubFunIDs) - 1, 1)
            End If
            If dtTab.Rows.Count > 0 Then
                sSql = "select SEM_ID,SEM_NAME from MST_SUBENTITY_MASTER Where SEM_Ent_ID=" & iEntID & " "
                sSql = sSql & " And SEM_CompID=" & iACID & " And SEM_ID in (" & sSubFunIDs & ") order by SEM_NAME"
                dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            End If
            Return dt
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRCSADataGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal sSubFunctionID As String) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
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

            sSql = "Select MMM_ID,MMM_FunID,MMM_SEMID,SEM_Name,MMM_PMID,PM_Name,MMM_SPMID,SPM_Name,MMM_SPMKey,MMM_RISKID,MMM_Risk,MMM_RiskKey,RAM_Name,"
            sSql = sSql & " MMM_CONTROLID,MMM_Control,MMM_ControlKey,MMM_ChecksID,MMM_CHECKS,MMM_ChecksKey From MST_MAPPING_MASTER "
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER On SEM_ID=MMM_SEMID And SEM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_PROCESS_MASTER On PM_ID=MMM_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER On SPM_ID=MMM_SPMID And SPM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster On RAM_PKID = (Select MRL_RiskTypeID from MST_RISK_Library where MRL_PKID=MMM_RISKID And MRL_CompID=" & iACID & ") And RAM_CompID=" & iACID & ""
            sSql = sSql & " Where MMM_DelFlag ='A' And MMM_YearID=" & iYearID & " And MMM_Module='R' And MMM_CUSTID=" & iCustID & " And MMM_FunID=" & iFunctionID & ""
            If sSubFunctionID <> "" Then
                sSql = sSql & " And MMM_SEMID In (" & sSubFunctionID & ")"
            End If
            sSql = sSql & " And MMM_CompID = " & iACID & " Order by SEM_Name,PM_Name,SPM_Name,MMM_Risk,MMM_Control,MMM_CHECKS"
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
                dr("RiskType") = dt.Rows(i)("RAM_Name")
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
                dr("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MMM_CHECKS"))
                If dt.Rows(i)("MMM_ChecksKey") = 1 Then
                    dr("ChecksKey") = "KEY"
                Else
                    dr("ChecksKey") = "NON-KEY"
                End If

                dr("Impact") = "" : dr("ImpactID") = "0"
                dr("Likelihood") = "" : dr("LikelihoodID") = "0"
                dr("RiskRating") = "" : dr("RiskRatingColor") = ""
                dr("OE") = "" : dr("OEID") = "0"
                dr("DE") = "" : dr("DEID") = "0"
                dr("ControlRating") = "" : dr("ControlRatingColor") = ""
                dr("ResidualRiskRating") = "" : dr("ResidualRiskRatingColor") = ""
                dr("Remarks") = ""
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function LoadRCSASavedGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer, ByVal sSubFunctionID As String) As DataTable
        Dim iSubProcessKey As Integer, iRiskKey As Integer, iChecksKey As Integer
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("SrNo")
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
            dtTab.Columns.Add("Remarks")
            dtTab.Columns.Add("RemarksRT")

            sSql = "Select m.RCSAD_SEMID,a.SEM_NAME,m.RCSAD_PMID,b.PM_NAME,m.RCSAD_SPMID,c.SPM_NAME,c.SPM_Iskey,m.RCSAD_RiskID,d.MRL_IsKey,d.MRL_RiskName,m.RCSAD_RiskTypeID,"
            sSql = sSql & " e.RAM_Name As RiskType,m.RCSAD_ImpactID,m.RCSAD_LikelihoodID,m.RCSAD_OES,m.RCSAD_DES,h.RAM_Name As Impact,i.RAM_Name As Likelihood,"
            sSql = sSql & " j.RAM_Name As OESName,k.RAM_Name As DESName,m.RCSAD_RiskRating,m.RCSAD_ControlID,f.MCL_IsKey,f.MCL_ControlName,m.RCSAD_ControlRating,"
            sSql = sSql & " m.RCSAD_ChecksID, g.CHK_IsKey, g.CHK_CheckName, m.RCSAD_ResidualRiskRating, m.RCSAD_Remarks,"
            sSql = sSql & " Case When m.RCSAD_RemarksRT Is NULL Then '' else m.RCSAD_RemarksRT End As RemarksRT,"
            sSql = sSql & " RRPYD.RCSAD_ImpactID As PYImpactID,RRPYD.RCSAD_LikelihoodID As PYLikelihoodID,RRPYD.RCSAD_OES As PYOESID,RRPYD.RCSAD_DES As PYDESID,"
            sSql = sSql & " p.RAM_Name As PYImpact,q.RAM_Name As PYLikelihood,r.RAM_Name As PYOESName,s.RAM_Name As PYDESName,"
            sSql = sSql & " RRPYD.RCSAD_RiskRating As PYRiskRating,RRPYD.RCSAD_ControlRating As PYControlRating,RRPYD.RCSAD_ResidualRiskRating As PYResidualRiskRating"
            sSql = sSql & " From Risk_RCSA_Details m Left Join Risk_RCSA RRPY on RRPY.RCSA_CustID=" & iCustID & " And RRPY.RCSA_FunID=" & iFunctionID & ""
            sSql = sSql & " And RRPY.RCSA_FinancialYear=" & iYearID - 1 & " And RRPY.RCSA_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_RCSA_Details RRPYD on RRPYD.RCSAD_RCSAPKID=RRPY.RCSA_PKID And m.RCSAD_SEMID=RRPYD.RCSAD_SEMID And m.RCSAD_PMID=RRPYD.RCSAD_PMID"
            sSql = sSql & " And m.RCSAD_SPMID=RRPYD.RCSAD_SPMID And m.RCSAD_RiskID=RRPYD.RCSAD_RiskID And m.RCSAD_ControlID=RRPYD.RCSAD_ControlID And RRPYD.RCSAD_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBENTITY_MASTER a On a.SEM_ID=m.RCSAD_SEMID And SEM_CompID=" & iACID & " Left Join MST_PROCESS_MASTER b On b.PM_ID=m.RCSAD_PMID And PM_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_SUBPROCESS_MASTER c on c.SPM_ID=m.RCSAD_SPMID And SPM_CompID=" & iACID & " Left Join MST_RISK_Library d on d.MRL_PKID=m.RCSAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster e On e.RAM_PKID=m.RCSAD_RiskTypeID And RAM_CompID=" & iACID & " Left Join MST_CONTROL_Library f On f.MCL_PKID=m.RCSAD_ControlID And MCL_CompID=" & iACID & ""
            sSql = sSql & " Left Join MST_Checks_Master g on g.CHK_ControlID=m.RCSAD_ControlID And g.CHK_ID=m.RCSAD_ChecksID And g.CHK_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster h on h.RAM_Category='RI' And h.RAM_YearID=" & iYearID & " And h.RAM_PKID=m.RCSAD_ImpactID and h.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster i On i.RAM_Category='RL' And i.RAM_YearID=" & iYearID & " And i.RAM_PKID=m.RCSAD_LikelihoodID and i.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster j on j.RAM_Category='OES' And j.RAM_YearID=" & iYearID & " And j.RAM_PKID=m.RCSAD_OES and j.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster k On k.RAM_Category='DES' And k.RAM_YearID=" & iYearID & " And k.RAM_PKID=m.RCSAD_DES and k.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster p on p.RAM_Category='RI' And p.RAM_YearID=" & iYearID - 1 & " And p.RAM_PKID=RRPYD.RCSAD_ImpactID and p.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster q On q.RAM_Category='RL' And q.RAM_YearID=" & iYearID - 1 & " And q.RAM_PKID=RRPYD.RCSAD_LikelihoodID and q.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster r on r.RAM_Category='OES' And r.RAM_YearID=" & iYearID - 1 & " And r.RAM_PKID=RRPYD.RCSAD_OES and r.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster s On s.RAM_Category='DES' And s.RAM_YearID=" & iYearID - 1 & " And s.RAM_PKID=RRPYD.RCSAD_DES and s.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where m.RCSAD_RCSAPKID In (Select RCSA_PKID from Risk_RCSA where RCSA_FinancialYear=" & iYearID & " And RCSA_CustID=" & iCustID & ""
            sSql = sSql & " And RCSA_FunID=" & iFunctionID & " And RCSA_CompID=" & iACID & ")"
            If sSubFunctionID <> "" Then
                sSql = sSql & " And RCSAD_SEMID In (" & sSubFunctionID & ")"
            End If
            sSql = sSql & " And m.RCSAD_CompID=" & iACID & " Order by m.RCSAD_SEMID, m.RCSAD_PMID, m.RCSAD_SPMID, m.RCSAD_RiskID, m.RCSAD_ControlID, m.RCSAD_ChecksID"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("SrNo") = i + 1
                dr("SubFunctionID") = dt.Rows(i)("RCSAD_SEMID")
                dr("ProcessID") = dt.Rows(i)("RCSAD_PMID")
                dr("SubProcessID") = dt.Rows(i)("RCSAD_SPMID")
                dr("SubFunction") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SEM_NAME"))
                dr("Process") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PM_NAME"))
                dr("SubProcess") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("SPM_NAME"))
                iSubProcessKey = dt.Rows(i)("SPM_Iskey")
                If iSubProcessKey = 1 Then
                    dr("SubProcessKey") = "KEY"
                Else
                    dr("SubProcessKey") = "NON-KEY"
                End If
                dr("RisKID") = dt.Rows(i)("RCSAD_RiskID")
                If IsDBNull(dt.Rows(i)("MRL_RiskName")) = False Then
                    dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MRL_RiskName"))
                End If
                If IsDBNull(dt.Rows(i)("RiskType")) = False Then
                    dr("RiskType") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RiskType"))
                End If
                iRiskKey = dt.Rows(i)("MRL_IsKey")
                If iRiskKey = 1 Then
                    dr("RiskKey") = "KEY"
                Else
                    dr("RiskKey") = "NON-KEY"
                End If
                If IsDBNull(dt.Rows(i)("RCSAD_ControlID")) = False Then
                    dr("ControlID") = dt.Rows(i)("RCSAD_ControlID")
                End If
                If IsDBNull(dt.Rows(i)("MCL_ControlName")) = False Then
                    dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MCL_ControlName"))
                End If

                iRiskKey = dt.Rows(i)("MCL_IsKey")
                If iRiskKey = 1 Then
                    dr("ControlKey") = "KEY"
                Else
                    dr("ControlKey") = "NON-KEY"
                End If
                dr("ChecksID") = dt.Rows(i)("RCSAD_ChecksID")
                If IsDBNull(dt.Rows(i)("CHK_CheckName")) = False Then
                    dr("Checks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("CHK_CheckName"))
                End If
                If IsDBNull(dt.Rows(i)("CHK_IsKey")) = False Then
                    iChecksKey = dt.Rows(i)("CHK_IsKey")
                    If iChecksKey = 1 Then
                        dr("ChecksKey") = "KEY"
                    Else
                        dr("ChecksKey") = "NON-KEY"
                    End If
                End If
                If IsDBNull(dt.Rows(i)("Impact")) = False Then
                    dr("Impact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Impact"))
                End If
                dr("ImpactID") = dt.Rows(i)("RCSAD_ImpactID")
                'Previous Year Impact
                If IsDBNull(dt.Rows(i)("PYImpact")) = False Then
                    dr("PYImpact") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYImpact"))
                End If

                If IsDBNull(dt.Rows(i)("Likelihood")) = False Then
                    dr("Likelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Likelihood"))
                End If
                dr("LikelihoodID") = dt.Rows(i)("RCSAD_LikelihoodID")
                'Previous Year Likelihood
                If IsDBNull(dt.Rows(i)("PYLikelihood")) = False Then
                    dr("PYLikelihood") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYLikelihood"))
                End If

                dr("RiskRating") = "" : dr("RiskRatingColor") = ""
                If dt.Rows(i)("RCSAD_RiskRating") > 0 Then
                    dr("RiskRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RCSAD_RiskRating"), "GRS", "Name")
                Else
                    dr("RiskRating") = ""
                End If
                dr("RiskRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RCSAD_RiskRating"), "GRS", "Color")
                'Previous Year RiskRating
                dr("PYRiskRating") = "" : dr("PYRiskRatingColor") = ""
                If IsDBNull(dr("PYImpact")) = False And IsDBNull(dr("PYLikelihood")) = False Then
                    If IsDBNull(dt.Rows(i)("PYRiskRating")) = False Then
                        If dt.Rows(i)("PYRiskRating") > 0 Then
                            dr("PYRiskRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYRiskRating"), "GRS", "Name")
                        Else
                            dr("PYRiskRating") = ""
                        End If
                        dr("PYRiskRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYRiskRating"), "GRS", "Color")
                    End If
                End If

                If IsDBNull(dt.Rows(i)("OESName")) = False Then
                    dr("OE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("OESName"))
                End If
                dr("OEID") = dt.Rows(i)("RCSAD_OES")
                'Previous Year OESName
                If IsDBNull(dt.Rows(i)("PYOESName")) = False Then
                    dr("PYOESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYOESName"))
                End If

                If IsDBNull(dt.Rows(i)("DESName")) = False Then
                    dr("DE") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("DESName"))
                End If
                dr("DEID") = dt.Rows(i)("RCSAD_DES")
                'Previous Year DESName
                If IsDBNull(dt.Rows(i)("PYDESName")) = False Then
                    dr("PYDESName") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("PYDESName"))
                End If

                If dt.Rows(i)("RCSAD_ControlRating") > 0 Then
                    dr("ControlRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RCSAD_ControlRating"), "GCS", "Name")
                Else
                    dr("ControlRating") = ""
                End If
                dr("ControlRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RCSAD_ControlRating"), "GCS", "Color")
                'Previous Year ControlRating
                If IsDBNull(dr("PYOESName")) = False And IsDBNull(dr("PYDESName")) = False Then
                    If IsDBNull(dt.Rows(i)("PYControlRating")) = False Then
                        If dt.Rows(i)("PYControlRating") > 0 Then
                            dr("PYControlRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYControlRating"), "GCS", "Name")
                        Else
                            dr("PYControlRating") = ""
                        End If
                        dr("PYControlRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYControlRating"), "GCS", "Color")
                    End If
                End If

                dr("ResidualRiskRating") = "" : dr("ResidualRiskRatingColor") = ""
                If dt.Rows(i)("RCSAD_RiskRating") > 0 And dt.Rows(i)("RCSAD_ControlRating") > 0 Then
                    If dt.Rows(i)("RCSAD_ResidualRiskRating") >= 0 Then
                        dr("ResidualRiskRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RCSAD_ResidualRiskRating"), "RRS", "Name")
                        dr("ResidualRiskRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID, dt.Rows(i)("RCSAD_ResidualRiskRating"), "RRS", "Color")
                    Else
                        dr("ResidualRiskRating") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Name")
                        dr("ResidualRiskRatingColor") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID, "RRS", 0, "Color")
                    End If
                End If
                'Previous Year ResidualRiskRating
                dr("PYResidualRiskRating") = "" : dr("PYResidualRiskRatingColor") = ""
                If IsDBNull(dr("PYControlRating")) = False And IsDBNull(dr("PYControlRating")) = False Then
                    If dt.Rows(i)("PYRiskRating") > 0 And dt.Rows(i)("PYControlRating") > 0 Then
                        If dt.Rows(i)("PYResidualRiskRating") >= 0 Then
                            dr("PYResidualRiskRating") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYResidualRiskRating"), "RRS", "Name")
                            dr("PYResidualRiskRatingColor") = objclsRiskGeneral.GetNameColorFromScoreRiskMaster(sAC, iACID, iYearID - 1, dt.Rows(i)("PYResidualRiskRating"), "RRS", "Color")
                        Else
                            dr("PYResidualRiskRating") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Name")
                            dr("PYResidualRiskRatingColor") = objclsRiskGeneral.GetRRNameColorFromRangeRiskMaster(sAC, iACID, iYearID - 1, "RRS", 0, "Color")
                        End If
                    End If
                End If

                dr("Remarks") = ""
                dr("Remarks") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RCSAD_Remarks"))
                dr("RemarksRT") = ""
                If IsDBNull(dt.Rows(i)("RemarksRT")) = False Then
                    dr("RemarksRT") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RemarksRT"))
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRCSAFunID(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSAID As Integer, ByVal iYearID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RCSA_FunID From Risk_RCSA where RCSA_PKID=" & iRCSAID & " and RCSA_COmpID=" & iACID & " And RCSA_FinancialYear=" & iYearID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function CheckAndGetRCSAID(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As Integer
        Dim sSql As String
        Try
            sSql = "Select RCSA_PKID from Risk_RCSA where RCSA_FinancialYear=" & iYearID & " And RCSA_CustID=" & iCustID & " And RCSA_FunID=" & iFunctionID & " And RCSA_CompID=" & iACID & ""
            Return objDBL.SQLExecuteScalarInt(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub DeleteRCSADetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSCID As Integer)
        Dim sSql As String
        Try
            sSql = "Delete From Risk_RCSA_Details Where RCSAD_RCSAPKID=" & iRCSCID & " And RCSAD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function SaveRCSAMaster(ByVal sAC As String, ByVal objRCSA As strRCSA_Assess, ByVal iCustID As Integer, ByVal iFunctionId As Integer, ByVal sFormType As String, ByVal sYearName As String) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(12) {}
        Dim iRCSAParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSAParamCount = 0
            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_PKID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAPKID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_AsgNo", OleDb.OleDbType.VarChar, 50)
            ObjSFParam(iRCSAParamCount).Value = objclsGeneralFunctions.GetAllModuleJobCode(sAC, objRCSA.iRCSACompID, sFormType, objRCSA.iRCSAFinancialYear, sYearName, iCustID)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_FinancialYear", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAFinancialYear
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_CustID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSACustID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_FunID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAFunID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_OwnerID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSAOwnerID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.sRCSAComments
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_CrBy", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSACrBy
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.iRCSACompID
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.sRCSAIPAddress
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@RCSA_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSAParamCount).Value = objRCSA.sRCSAStatus
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Input
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            iRCSAParamCount += 1

            ObjSFParam(iRCSAParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSAParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_RCSA", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function SaveRCSADetails(ByVal sAC As String, ByVal objRCSAD As strRCSA_AssessDetails) As Array
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(20) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSADParamCount = 0
            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADPKID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_RCSAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRCSAPKID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_SEMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADSEMID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_PMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADPMID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_SPMID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADSPMID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_RiskID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRiskID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_RiskTypeID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRiskTypeID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_ImpactID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADImpactID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_LikelihoodID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADLikelihoodID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_RiskRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADRiskRating
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_ControlID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADControlID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_OES", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADOES
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_DES", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADDES
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_ControlRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADControlRating
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_ChecksID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADChecksID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_ResidualRiskRating", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADResidualRiskRating
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_Remarks", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.sRCSADRemarks
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.sRCSADIPAddress
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAD_CompID", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSADParamCount).Value = objRCSAD.iRCSADCompID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@iUpdateOrSave", OleDb.OleDbType.Numeric)
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Output
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@iOper", OleDb.OleDbType.Integer)
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Output
            Arr(0) = "@iUpdateOrSave"
            Arr(1) = "@iOper"

            Arr = objDBL.ExecuteSPForInsertARR(sAC, "spRisk_RCSA_Details", 1, Arr, ObjSFParam)
            Return Arr
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub SaveRCSAAssignHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iRCSAAPKID As Integer, ByVal sComments As String, ByVal Status As String, ByVal sIPAddress As String)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(6) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSADParamCount = 0
            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = 0
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_RCSAAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = iRCSAAPKID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_Comments", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRCSADParamCount).Value = sComments
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_UserID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = iUserID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_Status", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSADParamCount).Value = Status
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_IPAddress", OleDb.OleDbType.VarChar, 25)
            ObjSFParam(iRCSADParamCount).Value = sIPAddress
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RCSAAH_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = iACID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "SpRisk_RCSA_Assign_History", ObjSFParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Sub SaveRCSAActionPlanHistory(ByVal sAC As String, ByVal objRCSA As strRCSA_Assess)
        Dim ObjSFParam() As OleDb.OleDbParameter = New OleDb.OleDbParameter(10) {}
        Dim iRCSADParamCount As Integer
        Dim Arr(1) As String
        Try
            iRCSADParamCount = 0
            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_PKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = 0
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_RCSAPKID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.iRAHRCSAPKID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_CUSTID", OleDb.OleDbType.Integer, 15)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.iRAHCustID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_FUNID", OleDb.OleDbType.Integer, 15)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.iRAHFunID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_FinancialYear", OleDb.OleDbType.Integer, 15)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.iRAHFinancialYear
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_FactorIncrease", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.sRAHFactorIncrease
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_FactorDecrease", OleDb.OleDbType.VarChar, 2000)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.sRAHFactorDecrease
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_ActionPlan", OleDb.OleDbType.VarChar, 8000)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.sRAHActionPlan
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_TargetDate", OleDb.OleDbType.Date, 50)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.dRAHTargetDate
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1


            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_CrBy", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.iRAHCrBy
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1


            ObjSFParam(iRCSADParamCount) = New OleDb.OleDbParameter("@RAH_CompID", OleDb.OleDbType.Integer, 4)
            ObjSFParam(iRCSADParamCount).Value = objRCSA.iRAHCompID
            ObjSFParam(iRCSADParamCount).Direction = ParameterDirection.Input
            iRCSADParamCount += 1

            objDBL.ExecuteSPForInsertNoOutput(sAC, "spRisk_RCSA_ActionPlan_History", ObjSFParam)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function GetRCSAAssessDetails(ByVal sAC As String, ByVal iYearID As Integer, ByVal iACID As Integer, ByVal iCustID As Integer, ByVal iFunID As Integer) As DataTable
        Dim sSql As String
        Try
            sSql = "Select * from Risk_RCSA where RCSA_CustID=" & iCustID & " And RCSA_FunID=" & iFunID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            Return objDBL.SQLExecuteDataTable(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRCSAAssignHistory(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSAID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Date")
            dtTab.Columns.Add("User")
            dtTab.Columns.Add("Comments")
            dtTab.Columns.Add("Status")

            sSql = "Select RCSAAH_Date,RCSAAH_Comments,Usr_FullName,RCSAAH_Status from Risk_RCSA_Assign_History,Sad_UserDetails Where "
            sSql = sSql & " RCSAAH_UserID=Usr_ID And RCSAAH_RCSAAPKID=" & iRCSAID & " And RCSAAH_CompID=" & iACID & " Order by RCSAAH_PKID Desc"
            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("RCSAAH_Date")) = False Then
                    dr("Date") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("RCSAAH_Date"), "F")
                End If
                dr("User") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("Usr_FullName"))
                dr("Comments") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RCSAAH_Comments"))
                dr("Status") = dt.Rows(i)("RCSAAH_Status")
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function GetRCSAOverAllScore(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSAPKID As Integer) As Object
        Dim sSql As String, dOverAllScore As Double
        Dim iCount As Integer, iSumOfRCSA As Integer
        Try
            sSql = "Select Sum(RCSAD_ResidualRiskRating) From Risk_RCSA_Details Where RCSAD_RCSAPKID=" & iRCSAPKID & " And RCSAD_CompID=" & iACID & ""
            iSumOfRCSA = objDBL.SQLExecuteScalarInt(sAC, sSql)

            sSql = "Select Count(*) From Risk_RCSA_Details Where RCSAD_RCSAPKID=" & iRCSAPKID & " And RCSAD_CompID=" & iACID & ""
            iCount = objDBL.SQLExecuteScalarInt(sAC, sSql)

            dOverAllScore = iSumOfRCSA / iCount
            Return dOverAllScore
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateStatusRCSAStatus(ByVal sAC As String, ByVal iACID As Integer, ByVal iUserID As Integer, ByVal iYearID As Integer, ByVal iRCSAID As Integer, ByVal sStatus As String, ByVal sComment As String)
        Dim sSql As String = ""
        Dim dNetScore As Double
        Try
            dNetScore = Math.Round(GetRCSAOverAllScore(sAC, iACID, iRCSAID), 2)
            If sStatus = "Saved(Risk Team)" Then
                sSql = "Update Risk_RCSA Set RCSA_Status='Saved(Risk Team)',RCSA_RUpdatedBy=" & iUserID & ",RCSA_RUpdatedOn=GetDate() Where RCSA_PKID=" & iRCSAID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            ElseIf sStatus = "Submitted(Risk Team)" Then
                sSql = "Update Risk_RCSA Set RCSA_Status='Submitted(Risk Team)',RCSA_RSubmittedBy=" & iUserID & ",RCSA_RSubmittedOn=GetDate() Where RCSA_PKID=" & iRCSAID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            ElseIf sStatus = "Saved(Business Team)" Then
                sSql = "Update Risk_RCSA Set RCSA_NetScore=" & dNetScore & ",RCSA_Status='Saved(Business Team)',RCSA_BUpdatedBy=" & iUserID & ",RCSA_BUpdatedOn=GetDate() Where RCSA_PKID=" & iRCSAID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            ElseIf sStatus = "Submitted(Business Team)" Then
                sSql = "Update Risk_RCSA Set RCSA_NetScore=" & dNetScore & ",RCSA_Status='Submitted(Business Team)',RCSA_BSubmittedBy=" & iUserID & ",RCSA_BSubmittedOn=GetDate() Where RCSA_PKID=" & iRCSAID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            ElseIf sStatus = "Re-Assigned" Then
                sSql = "Update Risk_RCSA Set RCSA_NetScore=" & dNetScore & ",RCSA_Status='Re-Assigned',RCSA_ReAssignBy=" & iUserID & ",RCSA_ReAssignOn=GetDate() ,RCSA_CommentS='" & sComment & "' Where RCSA_PKID=" & iRCSAID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            ElseIf sStatus = "Approved" Then
                sSql = "Update Risk_RCSA Set RCSA_NetScore=" & dNetScore & ",RCSA_Status='Approved',RCSA_ApprovedBy=" & iUserID & ",RCSA_ApprovedOn=GetDate(),RCSA_CommentS='" & sComment & "' Where RCSA_PKID=" & iRCSAID & " And RCSA_FinancialYear=" & iYearID & " And RCSA_CompID=" & iACID & ""
            End If
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadRCSAActionPlanDetails(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim sSql As String
        Dim dt As New DataTable, dtTab As New DataTable
        Dim dRow As DataRow
        Try
            dtTab.Columns.Add("RAH_PKID")
            dtTab.Columns.Add("RAH_RCSAPKID")
            dtTab.Columns.Add("FactorsIncreasing")
            dtTab.Columns.Add("FactorsDecreasing")
            dtTab.Columns.Add("ActionPlan")
            dtTab.Columns.Add("TargetDate")
            sSql = "Select RAH_PKID,RAH_RCSAPKID,RAH_FactorIncrease,RAH_FactorDecrease,RAH_ActionPlan,RAH_TargetDate from Risk_RCSA_ActionPlan_History "
            sSql = sSql & " where RAH_CUSTID=" & iCustID & " And RAH_FUNID=" & iFunctionID & " And RAH_FinancialYear=" & iYearID & " And RAH_CompID=" & iACID & ""
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            If dt.Rows.Count > 0 Then
                For i = 0 To dt.Rows.Count - 1
                    dRow = dtTab.NewRow
                    dRow("RAH_PKID") = dt.Rows(i)("RAH_PKID")
                    dRow("RAH_RCSAPKID") = dt.Rows(i)("RAH_RCSAPKID")
                    If IsDBNull(dt.Rows(i)("RAH_FactorIncrease")) = False Then
                        dRow("FactorsIncreasing") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAH_FactorIncrease"))
                    End If
                    If IsDBNull(dt.Rows(i)("RAH_FactorDecrease")) = False Then
                        dRow("FactorsDecreasing") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAH_FactorDecrease"))
                    End If
                    If IsDBNull(dt.Rows(i)("RAH_ActionPlan")) = False Then
                        dRow("ActionPlan") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("RAH_ActionPlan"))
                    End If
                    If IsDBNull(dt.Rows(i)("RAH_TargetDate")) = False Then
                        dRow("TargetDate") = objclsGRACeGeneral.FormatDtForRDBMS(dt.Rows(i)("RAH_TargetDate"), "F")
                    End If
                    dtTab.Rows.Add(dRow)
                Next
            End If
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateActionPlanToRCSA(ByVal sAC As String, ByVal iACID As Integer, ByVal sRCSAFI As String, ByVal sRCSAFD As String, ByVal sRCSAAP As String, ByVal dRCSATD As Date, ByVal iRCSAID As Integer)
        Dim sSql As String
        Try
            sSql = "Update Risk_RCSA Set RCSA_FactorIncrease='" & sRCSAFI & "', RCSA_FactorDecrease='" & sRCSAFD & "', RCSA_ActionPlan='" & sRCSAAP & "', RCSA_TargetDate=" & dRCSATD & ""
            sSql = sSql & "  Where RCSA_PKID=" & iRCSAID & " And RCSA_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadRCSADetailsToExcel(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal iCustID As Integer, ByVal iFunctionID As Integer) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Function")
            dtTab.Columns.Add("Sub Function")
            dtTab.Columns.Add("Process")
            dtTab.Columns.Add("Sub Process")
            dtTab.Columns.Add("Risk Details")
            dtTab.Columns.Add("Risk Type")
            dtTab.Columns.Add("Impact")
            dtTab.Columns.Add("Likelihood")
            dtTab.Columns.Add("Control Details")
            dtTab.Columns.Add("Operating Efficiency")
            dtTab.Columns.Add("Design Efficiency")
            dtTab.Columns.Add("Checks")
            dtTab.Columns.Add("Remarks by Business")

            sSql = "Select x.ENT_EntityName,RCSAD_SEMID,a.SEM_NAME,RCSAD_PMID,b.PM_NAME,RCSAD_SPMID,c.SPM_NAME,RCSAD_RiskID,d.MRL_RiskName,RCSAD_RiskTypeID,e.RAM_Name,"
            sSql = sSql & " RCSAD_ControlID,f.MCL_ControlName,RCSAD_ChecksID,g.CHK_CheckName,h.RAM_Name As Impact,i.RAM_Name As Likelihood, "
            sSql = sSql & " j.RAM_Name As OESName,k.RAM_Name As DESName,RCSAD_Remarks From Risk_RCSA_Details"
            sSql = sSql & " Left join MST_ENTITY_MASTER x on x.Ent_ID=" & iFunctionID & " and ENT_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_SUBENTITY_MASTER a on a.SEM_ID=RCSAD_SEMID and SEM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_PROCESS_MASTER b on b.PM_ID=RCSAD_PMID and  PM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_SUBPROCESS_MASTER c on c.SPM_ID=RCSAD_SPMID and SPM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_RISK_Library d on d.MRL_PKID=RCSAD_RiskID and MRL_CompID=" & iACID & ""
            sSql = sSql & " Left join Risk_GeneralMaster e on e.RAM_PKID=RCSAD_RiskTypeID and RAM_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_CONTROL_Library f on f.MCL_PKID=RCSAD_ControlID and MCL_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_Checks_Master g on g.CHK_ControlID=RCSAD_ControlID And g.CHK_ID=RCSAD_ChecksID And g.CHK_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster h on h.RAM_Category='RI' And h.RAM_YearID=" & iYearID & " And h.RAM_PKID=RCSAD_ImpactID and h.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster i On i.RAM_Category='RL' And i.RAM_YearID=" & iYearID & " And i.RAM_PKID=RCSAD_LikelihoodID and i.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster j on j.RAM_Category='OES' And j.RAM_YearID=" & iYearID & " And j.RAM_PKID=RCSAD_OES and j.RAM_CompID=" & iACID & ""
            sSql = sSql & " Left Join Risk_GeneralMaster k On k.RAM_Category='DES' And k.RAM_YearID=" & iYearID & " And k.RAM_PKID=RCSAD_DES and k.RAM_CompID=" & iACID & ""
            sSql = sSql & " Where RCSAD_RCSAPKID In (Select RCSA_PKID from Risk_RCSA where RCSA_FinancialYear=" & iYearID & " And RCSA_CustID=" & iCustID & ""
            sSql = sSql & " And RCSA_FunID=" & iFunctionID & " And RCSA_CompID=" & iACID & ") And RCSAD_CompID=" & iACID & ""
            sSql = sSql & " Order by RCSAD_SEMID,RCSAD_PMID,RCSAD_SPMID,RCSAD_RiskID,RCSAD_ControlID,RCSAD_ChecksID"

            dt = objDBL.SQLExecuteDataSet(sAC, sSql).Tables(0)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                dr("Function") = dt.Rows(i)("ENT_EntityName")
                dr("Sub Function") = dt.Rows(i)("SEM_NAME")
                dr("Process") = dt.Rows(i)("PM_NAME")
                dr("Sub Process") = dt.Rows(i)("SPM_NAME")
                dr("Risk Details") = dt.Rows(i)("MRL_RiskName")
                dr("Risk Type") = dt.Rows(i)("RAM_Name")
                dr("Impact") = "" : dr("Likelihood") = "" : dr("Operating Efficiency") = "" : dr("Design Efficiency") = ""
                If IsDBNull(dt.Rows(i)("Impact")) = False Then
                    dr("Impact") = dt.Rows(i)("Impact")
                End If
                If IsDBNull(dt.Rows(i)("Likelihood")) = False Then
                    dr("Likelihood") = dt.Rows(i)("Likelihood")
                End If
                dr("Control Details") = dt.Rows(i)("MCL_ControlName")
                If IsDBNull(dt.Rows(i)("OESName")) = False Then
                    dr("Operating Efficiency") = dt.Rows(i)("OESName")
                End If
                If IsDBNull(dt.Rows(i)("DESName")) = False Then
                    dr("Design Efficiency") = dt.Rows(i)("DESName")
                End If
                dr("Checks") = dt.Rows(i)("CHK_CheckName")
                If IsDBNull(dt.Rows(i)("RCSAD_Remarks")) = False Then
                    dr("Remarks by Business") = dt.Rows(i)("RCSAD_Remarks")
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Sub UpdateRCSARTRemarks(ByVal sAC As String, ByVal iACID As Integer, ByVal iRCSAID As Integer, ByVal iSubFunID As Integer, ByVal iProcessID As Integer,
            ByVal iSubProcessID As Integer, ByVal iRiskID As Integer, ByVal iRiskTypeID As Integer, ByVal iControlID As Integer, ByVal iChecksID As Integer, sRemarks As String)
        Dim sSql As String = ""
        Try
            sSql = "Update Risk_RCSA_Details Set RCSAD_RemarksRT='" & sRemarks & "' Where RCSAD_RCSAPKID=" & iRCSAID & " And RCSAD_SEMID=" & iSubFunID & ""
            sSql = sSql & " And RCSAD_PMID=" & iProcessID & " And RCSAD_SPMID=" & iSubProcessID & " And RCSAD_RiskID=" & iRiskID & " And RCSAD_RiskTypeID=" & iRiskTypeID & ""
            sSql = sSql & " And RCSAD_ControlID=" & iControlID & " And RCSAD_ChecksID=" & iChecksID & " And RCSAD_CompID=" & iACID & ""
            objDBL.SQLExecuteNonQuery(sAC, sSql)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Public Function LoadRCSAHeatMapGrid(ByVal sAC As String, ByVal iACID As Integer, ByVal iYearID As Integer, ByVal sPKID As String) As DataTable
        Dim dt As New DataTable, dtTab As New DataTable
        Dim sSql As String
        Dim dr As DataRow
        Try
            dtTab.Columns.Add("Risk")
            dtTab.Columns.Add("Control")
            sSql = "Select RCSAD_PKID,RCSAD_RiskID,RCSAD_ControlID,MRL_RiskName,MCL_ControlName From Risk_RCSA_Details"
            sSql = sSql & " Left join MST_RISK_Library On MRL_PKID=RCSAD_RiskID And MRL_CompID=" & iACID & ""
            sSql = sSql & " Left join MST_CONTROL_Library On MCL_PKID=RCSAD_ControlID And MCL_CompID=" & iACID & ""
            sSql = sSql & " Where RCSAD_PKID In (" & sPKID & ")"
            sSql = sSql & " And RCSAD_CompID=" & iACID & " Order by RCSAD_RiskID,RCSAD_ControlID"
            dt = objDBL.SQLExecuteDataTable(sAC, sSql)
            For i = 0 To dt.Rows.Count - 1
                dr = dtTab.NewRow
                If IsDBNull(dt.Rows(i)("MRL_RiskName")) = False Then
                    dr("Risk") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MRL_RiskName"))
                End If
                If IsDBNull(dt.Rows(i)("MCL_ControlName")) = False Then
                    dr("Control") = objclsGRACeGeneral.ReplaceSafeSQL(dt.Rows(i)("MCL_ControlName"))
                End If
                dtTab.Rows.Add(dr)
            Next
            Return dtTab
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
